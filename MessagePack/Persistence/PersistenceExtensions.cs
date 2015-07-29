// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  

namespace FearTheCowboy.Common.Persistence {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Collections;

    public enum PersistableCategory {
        Parseable,
        Array,
        Enumerable,
        Dictionary,
        Nullable,
        String,
        Enumeration,
        Other
    }

    public class PersistableInfo {
        private static Type _iteratorType = Type.GetType("System.Linq.Enumerable.Iterator<>");

        internal PersistableInfo(Type type) {
            Type = type;

            if (type.IsEnum) {
                PersistableCategory = PersistableCategory.Enumeration;
                return;
            }

            if (type == typeof (string)) {
                PersistableCategory = PersistableCategory.String;
                return;
            }

            if (typeof (IDictionary).IsAssignableFrom(type)) {
                PersistableCategory = PersistableCategory.Dictionary;
                if (type.IsGenericType) {
                    var genericArguments = type.GetGenericArguments();
                    DictionaryKeyType = genericArguments[0];
                    DictionaryValueType = genericArguments[1];
                } else {
                    DictionaryKeyType = typeof (object);
                    DictionaryValueType = typeof (object);
                }
                return;
            }

            if (type.IsArray) {
                // an array of soemthing.
                PersistableCategory = PersistableCategory.Array;
                ElementType = type.GetElementType();
                return;
            }

            if (typeof (IEnumerable).IsAssignableFrom(type)) {
                PersistableCategory = PersistableCategory.Enumerable;
                if (type.IsGenericType) {
                    ElementType = type.GetGenericArguments().Last();
                    /* 
                     * scratch code to identify if we're looking at an iterator or somethig.  Don't think we need to get weird tho'
                     * 
                     * var et = type.GetGenericArguments().Last();
                    var It = type.IsAssignableFrom((Type)Activator.CreateInstance(IteratorType.MakeGenericType(type.GetGenericArguments().Last()), true));

                    var t = type;
                    Type[] genericArguments;

                    do {
                        if (t == typeof(object) || t == null) {
                            throw new ClrPlusException("Critical Failure in PersistableInfo/Enumerator [1].");
                        }
                        genericArguments = t.GetGenericArguments();
                        if (genericArguments.Length == 0) {
                            throw new ClrPlusException("Critical Failure in PersistableInfo/Enumerator [2].");
                        }
                        t = t.BaseType;
                    } while (genericArguments.Length > 1);
                    ElementType = genericArguments[0];
                     */
                } else {
                    ElementType = typeof (object);
                }
                return;
            }

            if (type.IsGenericType) {
                // better be Nullable
                switch (type.Name.Split('`')[0]) {
                    case "Nullable":
                        PersistableCategory = PersistableCategory.Nullable;
                        NullableType = type.GetGenericArguments()[0];
                        return;
                }
            }

            if (type.IsParsable()) {
                PersistableCategory = PersistableCategory.Parseable;
                return;
            }

            PersistableCategory = PersistableCategory.Other;
        }

        public PersistableCategory PersistableCategory {get; set;}
        public Type Type {get; set;}
        public Type ElementType {get; set;}
        public Type DictionaryKeyType {get; set;}
        public Type DictionaryValueType {get; set;}
        public Type NullableType {get; set;}
    }

    public class PersistablePropertyInformation {
        public DataMemberAttribute DataMember;

        public Func<object, object> GetValue;
        public Action<object, object> SetValue;
        public Type Type;
        public object CreateInstance() => Activator.CreateInstance(Type);
        private PersistableInfo _info;
        public PersistableInfo PersistableInfo => _info ??  (_info = Type.GetPersistableInfo());

        public string Name => DataMember.Name;
        public int Order => DataMember.Order;
    }

    public static class PersistenceExtensions {
        private static int counter = 0;
        private static readonly IDictionary<Type, MethodInfo> _tryParsers = new Dictionary<Type, MethodInfo>();
        private static readonly IDictionary<Type, ConstructorInfo> _tryStrings = new Dictionary<Type, ConstructorInfo>();
        private static readonly MethodInfo _castMethod = typeof (Enumerable).GetMethod("Cast");
        private static readonly MethodInfo _toArrayMethod = typeof (Enumerable).GetMethod("ToArray");
        private static readonly IDictionary<Type, MethodInfo> _castMethods = new Dictionary<Type, MethodInfo>();
        private static readonly IDictionary<Type, MethodInfo> _toArrayMethods = new Dictionary<Type, MethodInfo>();
        private static readonly Dictionary<Type, PersistableInfo> _piCache = new Dictionary<Type, PersistableInfo>();
        private static readonly Dictionary<Type, PersistablePropertyInformation[]> _ppiCache = new Dictionary<Type, PersistablePropertyInformation[]>();
        private static readonly Dictionary<Type, PersistablePropertyInformation[]> _readablePropertyCache = new Dictionary<Type, PersistablePropertyInformation[]>();

        public static PersistableInfo GetPersistableInfo(this Type t) {
            return _piCache.GetOrAdd(t, () => new PersistableInfo(t));
        }


        private static IEnumerable<PersistablePropertyInformation> GetReadOnlyProperties(Type type, bool hasDataContract) {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Select(each => GetReadOnlyAccessor(each, hasDataContract)).WhereNotNull();
        }

        private static IEnumerable<PersistablePropertyInformation> GetReadOnlyFields(Type type,bool hasDataContract) {
            return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Select(each => GetReadOnlyAccessor(each, hasDataContract)).WhereNotNull();
        }

        public static PersistablePropertyInformation[] GetReadOnlyMembers(this Type type) {
            var hasDataContract = type.GetCustomAttributes(typeof(DataContractAttribute)).Any();
            return _readablePropertyCache.GetOrAdd(type, () =>GetReadOnlyFields(type, hasDataContract).Union(GetReadOnlyProperties(type, hasDataContract)).OrderBy(each => each.DataMember.Order).ToArray());
        }

        public static PersistablePropertyInformation[] GetReadWriteMembers(this object instance) {
            if (instance == null) {
                return new PersistablePropertyInformation[0];
            }

            return GetReadWriteMembers(instance.GetType());
        }

        public static PersistablePropertyInformation[] GetReadWriteMembers(this Type type) {
            var hasDataContract = type.GetCustomAttributes(typeof (DataContractAttribute)).Any();
            return _ppiCache.GetOrAdd(type, () => GetReadWriteFields(type, hasDataContract).Union(GetReadWriteProperties(type, hasDataContract)).OrderBy(each => each.DataMember.Order).ToArray());
        }

        private static IEnumerable<PersistablePropertyInformation> GetReadWriteFields(Type type, bool hasDataContract) {
            return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Select(each => GetReadWriteAccessor(each,hasDataContract)).WhereNotNull();
        }


        private static PersistablePropertyInformation GetReadOnlyAccessor(PropertyInfo each, bool hasDataContract) {
            var getMethodInfo = each.GetGetMethod(true);
            if(getMethodInfo != null) {
                var attribute = GetDataMemberAttribute(each);
                if(IsReadable(each, attribute, hasDataContract)) {
                    return new PersistablePropertyInformation {
                        Type = each.PropertyType,
                        SetValue = null,
                        GetValue = each.GetValue,
                        DataMember = attribute ?? new DataMemberAttribute {
                            Name = each.Name,
                            Order = 1000 + counter++
                        }
                    };
                }
            }
            return null;
        }

        private static PersistablePropertyInformation GetReadOnlyAccessor(FieldInfo each, bool hasDataContract) {
            var attribute = GetDataMemberAttribute(each);
            if(IsReadable(each, attribute, hasDataContract)) {
                return new PersistablePropertyInformation {
                    Type = each.FieldType,
                    SetValue = null,
                    GetValue = (instance) => each.GetValue(instance),
                    DataMember = attribute ?? new DataMemberAttribute { Name = each.Name, Order = 1000 + counter++ }
                };
            }
            return null;
        }
        private static PersistablePropertyInformation GetReadWriteAccessor(FieldInfo each, bool hasDataContract) {
            var attribute = GetDataMemberAttribute(each);
            if (IsReadWritable(each, attribute, hasDataContract)) {
                return new PersistablePropertyInformation {
                    Type = each.FieldType,
                    SetValue = (instance, value) => each.SetValue(instance, value),
                    GetValue = (instance) => each.GetValue(instance),
                    DataMember = attribute ?? new DataMemberAttribute { Name = each.Name, Order = 1000 + counter++ }
                };
            }
            return null;
        }

        private static PersistablePropertyInformation GetReadWriteAccessor(PropertyInfo each, bool hasDataContract) {
            var setMethodInfo = each.GetSetMethod(true);
            var getMethodInfo = each.GetGetMethod(true);
            if (getMethodInfo != null && setMethodInfo != null) {
                var attribute = GetDataMemberAttribute(each);
                if (IsReadWriteable(each, attribute, hasDataContract)) {
                    return
                        new PersistablePropertyInformation {
                            Type = each.PropertyType,
                            SetValue = each.SetValue,
                            GetValue = each.GetValue,
                            DataMember = attribute ?? new DataMemberAttribute { Name = each.Name, Order = 1000 + counter++ }
                        };
                }
            }
            return null;
        }

        private static DataMemberAttribute GetDataMemberAttribute(MemberInfo each) {
            var result = (each.GetCustomAttributes(typeof (DataMemberAttribute), true).FirstOrDefault() as DataMemberAttribute);
            if (result != null) {
                if (result.Order == -1) {
                    result.Order = 1000 + counter++;
                }
                if (string.IsNullOrWhiteSpace(result.Name)) {
                    result.Name = each.Name;
                }
            }
            return result;
        }

        private static IEnumerable<PersistablePropertyInformation> GetReadWriteProperties(Type type, bool hasDataContract) {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Select(each => GetReadWriteAccessor(each, hasDataContract)).WhereNotNull();
        }

        private static bool IsReadable(PropertyInfo each, DataMemberAttribute persistableAttribute, bool hasDataContract) {
            return (!hasDataContract || persistableAttribute != null) && ((each.GetGetMethod(true).IsPublic) || persistableAttribute != null);
        }


        private static bool IsReadable(FieldInfo each, DataMemberAttribute persistableAttribute, bool hasDataContract) {
            return (!hasDataContract || persistableAttribute != null) &&  (each.IsPublic || persistableAttribute != null);
        }


        private static bool IsReadWritable(FieldInfo each, DataMemberAttribute persistableAttribute, bool hasDataContract) {
            return (!hasDataContract || persistableAttribute != null) && (!each.IsInitOnly && (each.IsPublic || persistableAttribute != null));
        }

        private static bool IsReadWriteable( PropertyInfo each, DataMemberAttribute persistableAttribute, bool hasDataContract ) {
            return (!hasDataContract || persistableAttribute != null) && ((each.GetSetMethod(true).IsPublic && each.GetGetMethod(true).IsPublic) || persistableAttribute != null);
        }

        public static object ToArrayOfType(this IEnumerable<object> enumerable, Type collectionType) {
            return _toArrayMethods.GetOrAdd(collectionType, () => _toArrayMethod.MakeGenericMethod(collectionType))
                .Invoke(null, new[] {
                    enumerable.CastToIEnumerableOfType(collectionType)
                });
        }

        public static object CastToIEnumerableOfType(this IEnumerable<object> enumerable, Type collectionType) {
            lock (collectionType) {
                return _castMethods.GetOrAdd(collectionType, () => _castMethod.MakeGenericMethod(collectionType)).Invoke(null, new object[] {
                    enumerable
                });
            }
        }

        private static MethodInfo GetTryParse(Type parsableType) {
            lock (_tryParsers) {
                if (!_tryParsers.ContainsKey(parsableType)) {
                    if (parsableType.IsPrimitive || parsableType.IsValueType || parsableType.GetConstructor(new Type[] {
                    }) != null) {
                        _tryParsers.Add(parsableType, parsableType.GetMethod("TryParse", new[] {
                            typeof (string), parsableType.MakeByRefType()
                        }));
                    } else {
                        // if they don't have a default constructor, 
                        // it's not going to be 'parsable'
                        _tryParsers.Add(parsableType, null);
                    }
                }
            }
            return _tryParsers[parsableType];
        }

        private static ConstructorInfo GetStringConstructor(Type parsableType) {
            lock (_tryStrings) {
                if (!_tryStrings.ContainsKey(parsableType)) {
                    _tryStrings.Add(parsableType, parsableType.GetConstructor(new[] {
                        typeof (string)
                    }));
                }
            }
            return _tryStrings[parsableType];
        }

        public static bool IsConstructableFromString(this Type stringableType) {
            return GetStringConstructor(stringableType) != null;
        }

        public static bool IsParsable(this Type parsableType) {
            if (parsableType.IsDictionary() || parsableType.IsArray) {
                return false;
            }
            return parsableType.IsEnum || parsableType == typeof (string) || GetTryParse(parsableType) != null || IsConstructableFromString(parsableType);
        }

        public static object ParseString(this Type parsableType, string value) {
            if (parsableType.IsEnum) {
                return Enum.Parse(parsableType, value);
            }

            if (parsableType == typeof (string)) {
                return value;
            }

            var tryParse = GetTryParse(parsableType);

            if (tryParse != null) {
                if (!string.IsNullOrEmpty(value)) {
                    var pz = new[] {
                        value, Activator.CreateInstance(parsableType)
                    };

                    // returns the default value if it's not successful.
                    tryParse.Invoke(null, pz);
                    return pz[1];
                }
                return Activator.CreateInstance(parsableType);
            }

            return string.IsNullOrEmpty(value) ? null : GetStringConstructor(parsableType).Invoke(new object[] {
                value
            });
        }

        public static bool IsDictionary(this Type dictionaryType) {
            return typeof (IDictionary).IsAssignableFrom(dictionaryType);
        }

        public static bool IsIEnumerable(this Type ienumerableType) {
            return typeof (IEnumerable).IsAssignableFrom(ienumerableType);
        }
    }
}