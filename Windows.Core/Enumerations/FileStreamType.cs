//-----------------------------------------------------------------------
// <copyright company="CoApp Project">
//     Changes Copyright (c) 2011 Garrett Serack . All rights reserved.
//     AlternateStreams Original Code from http://www.codeproject.com/KB/cs/ntfsstreams.aspx
// </copyright>
// <license>
//     The software is licensed under the Apache 2.0 License (the "License")
//     You may not use the software except in compliance with the License. 
// </license>
//-----------------------------------------------------------------------

namespace Toolkit.Windows.Enumerations {
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