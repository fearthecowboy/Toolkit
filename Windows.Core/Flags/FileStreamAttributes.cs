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

namespace FearTheCowboy.Windows.Flags {
    using System;

    /// <summary>
    ///     Represents the attributes of a file stream.
    /// </summary>
    [Flags]
    public enum FileStreamAttributes {
        /// <summary>
        ///     No attributes.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Set if the stream contains data that is modified when read.
        /// </summary>
        ModifiedWhenRead = 1,

        /// <summary>
        ///     Set if the stream contains security data.
        /// </summary>
        ContainsSecurity = 2,

        /// <summary>
        ///     Set if the stream contains properties.
        /// </summary>
        ContainsProperties = 4,

        /// <summary>
        ///     Set if the stream is sparse.
        /// </summary>
        Sparse = 8,
    }
}