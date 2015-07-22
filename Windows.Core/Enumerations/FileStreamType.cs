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
    /// <summary>
    ///     Represents the type of data in a stream.
    /// </summary>
    public enum FileStreamType {
        /// <summary>
        ///     Unknown stream type.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Standard data.
        /// </summary>
        Data = 1,

        /// <summary>
        ///     Extended attribute data.
        /// </summary>
        ExtendedAttributes = 2,

        /// <summary>
        ///     Security data.
        /// </summary>
        SecurityData = 3,

        /// <summary>
        ///     Alternate data stream.
        /// </summary>
        AlternateDataStream = 4,

        /// <summary>
        ///     Hard link information.
        /// </summary>
        Link = 5,

        /// <summary>
        ///     Property data.
        /// </summary>
        PropertyData = 6,

        /// <summary>
        ///     Object identifiers.
        /// </summary>
        ObjectId = 7,

        /// <summary>
        ///     Reparse points.
        /// </summary>
        ReparseData = 8,

        /// <summary>
        ///     Sparse file.
        /// </summary>
        SparseBlock = 9,

        /// <summary>
        ///     Transactional data. (Undocumented - BACKUP_TXFS_DATA)
        /// </summary>
        TransactionData = 10,
    }
}