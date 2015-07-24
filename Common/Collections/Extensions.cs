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

namespace FearTheCowboy.Common.Collections {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class Extensions {
        private static readonly MethodInfo _castMethod = typeof (Enumerable).GetMethod("Cast");
        private static readonly MethodInfo _toArrayMethod = typeof (Enumerable).GetMethod("ToArray");
        private static readonly IDictionary<Type, MethodInfo> _castMethods = new Dictionary<Type, MethodInfo>();
        private static readonly IDictionary<Type, MethodInfo> _toArrayMethods = new Dictionary<Type, MethodInfo>();

        /// <summary>
        ///     Returns a ReEnumerable wrapper around the collection which timidly (cautiously) pulls items
        ///     but still allows you to to re-enumerate without re-running the query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static MutableEnumerable<T> ReEnumerable<T>(this IEnumerable<T> collection) {
            if (collection == null) {
                return new ReEnumerable<T>(Enumerable.Empty<T>());
            }
            return collection as MutableEnumerable<T> ?? new ReEnumerable<T>(collection);
        }

        public static IEnumerable<TSource> FilterWithFinalizer<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, Action<TSource> onFilterAction) {
            foreach (var i in source) {
                if (predicate(i)) {
                    onFilterAction(i);
                } else {
                    yield return i;
                }
            }
        }

        /// <summary>
        ///     Determines whether the collection object is either null or an empty collection.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <returns>
        ///     <c>true</c> if [is null or empty] [the specified collection]; otherwise, <c>false</c> .
        /// </returns>
        /// <remarks>
        /// </remarks>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) {
            return collection == null || !collection.Any();
        }

        public static TValue AddOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) {
            lock (dictionary) {
                if (dictionary.ContainsKey(key)) {
                    dictionary[key] = value;
                } else {
                    dictionary.Add(key, value);
                }
            }
            return value;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFunction) {
            lock (dictionary) {
                return dictionary.ContainsKey(key) ? dictionary[key] : dictionary.AddOrSet(key, valueFunction());
            }
        }

        /// <summary>
        ///     Concatenates a single item to an IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<T> ConcatSingleItem<T>(this IEnumerable<T> enumerable, T item) {
            return enumerable.Concat(new[] {
                item
            });
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> enumerable) {
            return enumerable == null ? Enumerable.Empty<T>() : enumerable.Where(each => (object)each != null);
        }

        public static IEnumerable<T> ToIEnumerable<T>(this IEnumerator<T> enumerator) {
            if (enumerator != null) {
                while (enumerator.MoveNext()) {
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> set1, IEnumerator<T> set2) {
            var s1 = set1 ?? Enumerable.Empty<T>();
            var s2 = set2 == null ? Enumerable.Empty<T>() : set2.ToIEnumerable();
            return s1.Concat(s2);
        }

        public static IEnumerable<T> SingleItemAsEnumerable<T>(this T item) {
            return new T[] {
                item
            };
        }

        /// <summary>
        ///     returns the index position where the predicate matches the value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int IndexWhere<T>(this IEnumerable<T> source, Func<T, int, bool> predicate) {
            if (source != null && predicate != null) {
                var index = -1;
                if (source.Any(element => predicate(element, ++index))) {
                    return index;
                }
            }
            return -1;
        }

        /// <summary>
        ///     returns the index position where the predicate matches the value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int IndexWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            if (source != null && predicate != null) {
                var index = -1;
                if (source.Any(element => {
                    ++index;
                    return predicate(element);
                })) {
                    return index;
                }
            }
            return -1;
        }

        public static void AddRangeLocked<T>(this List<T> list, IEnumerable<T> items) {
            if (list == null) {
                throw new ArgumentNullException("list");
            }
            lock (list) {
                list.AddRange(items);
            }
        }

        public static object ToIEnumerableT(this IEnumerable<object> enumerable, Type elementType) {
            return _castMethods.GetOrAdd(elementType, () => _castMethod.MakeGenericMethod(elementType)).Invoke(null, new object[] {enumerable});
        }

        public static object ToArrayT(this IEnumerable<object> enumerable, Type elementType) {
            return _toArrayMethods.GetOrAdd(elementType, () => _toArrayMethod.MakeGenericMethod(elementType)).Invoke(null, new[] {enumerable.ToIEnumerableT(elementType)});
        }
    }
}