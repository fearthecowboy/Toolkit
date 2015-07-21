//-----------------------------------------------------------------------
// <copyright company="CoApp Project">
//     Copyright (c) 2010-2013 Garrett Serack and CoApp Contributors. 
//     Contributors can be discovered using the 'git log' command.
//     All rights reserved.
// </copyright>
// <license>
//     The software is licensed under the Apache 2.0 License (the "License")
//     You may not use the software except in compliance with the License. 
// </license>
//-----------------------------------------------------------------------

namespace Toolkit.Windows.Enumerations {
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