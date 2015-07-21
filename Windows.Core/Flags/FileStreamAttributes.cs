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

namespace Toolkit.Windows.Flags {
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