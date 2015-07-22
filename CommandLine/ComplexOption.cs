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

namespace FearTheCowboy.CommandLine {
    using System.Collections.Generic;

    /// <summary>
    ///     Storage Class for complex options from the command line.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class ComplexOption {
        /// <summary>
        /// </summary>
        public List<string> PrefixParameters = new List<string>(); // individual items in the []

        /// <summary>
        /// </summary>
        public IDictionary<string, string> Values = new Dictionary<string, string>(); // individual key/values after the []

        /// <summary>
        /// </summary>
        public string WholePrefix; // stuff in the []

        /// <summary>
        /// </summary>
        public string WholeValue; // stuff after the []
    }
}