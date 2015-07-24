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

namespace FearTheCowboy.Common.Core {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Collections;

    public static class StringExtensions {
        // ReSharper disable InconsistentNaming
        public static IEnumerable<string> Quote(this IEnumerable<string> items) {
            return items.Select(each => "'{each}'");
        }

        public static string JoinWithComma(this IEnumerable<string> items) {
            return items.JoinWith(",");
        }

        public static string JoinWith(this IEnumerable<string> items, string delimiter) {
            return items.SafeAggregate((current, each) => current + delimiter + each);
        }

        public static TSource SafeAggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func) {
            var src = source.ReEnumerable();
            if (source != null && src.Any()) {
                return src.Aggregate(func);
            }
            return default(TSource);
        }

        public static bool ContainsIgnoreCase(this IEnumerable<string> collection, string value) {
            if (collection == null) {
                return false;
            }
            return collection.Any(s => s.EqualsIgnoreCase(value));
        }

        public static bool ContainsAnyOfIgnoreCase(this IEnumerable<string> collection, params object[] values) {
            return collection.ContainsAnyOfIgnoreCase(values.Select(value => value == null ? null : value.ToString()));
        }

        public static bool ContainsAnyOfIgnoreCase(this IEnumerable<string> collection, IEnumerable<string> values) {
            if (collection == null) {
                return false;
            }
            var set = values.ReEnumerable();

            return collection.Any(set.ContainsIgnoreCase);
        }

        public static bool IsTrue(this string text) {
            return !string.IsNullOrWhiteSpace(text) && text.Equals("true", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool? IsTruePreserveNull(this string text) {
            if (text == null) {
                return null;
            }
            return !string.IsNullOrWhiteSpace(text) && text.Equals("true", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool EqualsIgnoreCase(this string str, string str2) {
            if (str == null && str2 == null) {
                return true;
            }

            if (str == null || str2 == null) {
                return false;
            }

            return str.Equals(str2, StringComparison.OrdinalIgnoreCase);
        }

        public static string MakeSafeFileName(this string input) {
            return new Regex(@"-+").Replace(new Regex(@"[^\d\w\[\]_\-\.\ ]").Replace(input, "-"), "-").Replace(" ", "");
        }
    }
}