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

namespace FearTheCowboy.Windows.Enumerations {
    public static class ReparsePointError {
        /// <summary>
        ///     The file or directory is not a reparse point.
        /// </summary>
        public const int NotAReparsePoint = 4390;

        /// <summary>
        ///     The reparse point attribute cannot be set because it conflicts with an existing attribute.
        /// </summary>
        public const int ReparseAttributeConflict = 4391;

        /// <summary>
        ///     The data present in the reparse point buffer is invalid.
        /// </summary>
        public const int InvalidReparseData = 4392;

        /// <summary>
        ///     The tag present in the reparse point buffer is invalid.
        /// </summary>
        public const int ReparseTagInvalid = 4393;

        /// <summary>
        ///     There is a mismatch between the tag specified in the request and the tag present in the reparse point.
        /// </summary>
        public const int ReparseTagMismatch = 4394;
    }
}