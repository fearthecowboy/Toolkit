// 
//  Copyright (c) Microsoft Corporation. All rights reserved. 
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

namespace Common.Collections {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Extensions {
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
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static TValue AddOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            lock (dictionary)
            {
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key] = value;
                }
                else
                {
                    dictionary.Add(key, value);
                }
            }
            return value;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFunction)
        {
            lock (dictionary)
            {
                return dictionary.ContainsKey(key) ? dictionary[key] : dictionary.AddOrSet(key, valueFunction());
            }
        }
    }
}