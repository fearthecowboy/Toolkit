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

namespace Toolkit.Windows.Structures {
    using System.Runtime.InteropServices;
    using Enumerations;

    [StructLayout(LayoutKind.Sequential)]
    public struct ReparseData {
        /// <summary>
        ///     Reparse point tag.
        /// </summary>
        public IoReparseTag ReparseTag;

        /// <summary>
        ///     Size, in bytes, of the data after the Reserved member. This can be calculated by: (4 * sizeof(ushort)) + SubstituteNameLength + PrintNameLength + (namesAreNullTerminated ? 2 * sizeof(char) : 0);
        /// </summary>
        public ushort ReparseDataLength;

        /// <summary>
        ///     Reserved. do not use.
        /// </summary>
        public ushort Reserved;

        /// <summary>
        ///     Offset, in bytes, of the substitute name string in the PathBuffer array.
        /// </summary>
        public ushort SubstituteNameOffset;

        /// <summary>
        ///     Length, in bytes, of the substitute name string. If this string is null-terminated, SubstituteNameLength does not include space for the null character.
        /// </summary>
        public ushort SubstituteNameLength;

        /// <summary>
        ///     Offset, in bytes, of the print name string in the PathBuffer array.
        /// </summary>
        public ushort PrintNameOffset;

        /// <summary>
        ///     Length, in bytes, of the print name string. If this string is null-terminated, PrintNameLength does not include space for the null character.
        /// </summary>
        public ushort PrintNameLength;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x3FF0)]
        public byte[] PathBuffer;
    }
}