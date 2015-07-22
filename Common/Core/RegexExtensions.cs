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
    using System.Text.RegularExpressions;

    /// <summary>
    /// 
    /// </summary>
    public static class RegexExtensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <param name="group"></param>
        /// <param name="_default"></param>
        /// <returns></returns>
        public static string GetValue(this Match match, string group, string _default = null) {
            return (match.Groups[@group].Success ? match.Groups[@group].Captures[0].Value : _default ?? string.Empty).Trim('-', ' ');
        }
    }
}