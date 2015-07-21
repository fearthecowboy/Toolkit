//-----------------------------------------------------------------------
// <copyright company="CoApp Project">
//     ResourceLib Original Code from http://resourcelib.codeplex.com
//     Original Copyright (c) 2008-2009 Vestris Inc.
//     Changes Copyright (c) 2011 Garrett Serack . All rights reserved.
// </copyright>
// <license>
// MIT License
// You may freely use and distribute this software under the terms of the following license agreement.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of 
// the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
// </license>
//-----------------------------------------------------------------------

namespace Toolkit.Windows.Structures {
    using System;
    using System.Runtime.InteropServices;
    using Enumerations;

    /// <summary>
    ///     This structure contains version information about a file. This information is language- and code page–independent. http://msdn.microsoft.com/en-us/library/ms647001.aspx
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct VsFixedfileinfo {
        /// <summary>
        ///     Contains the value 0xFEEF04BD. This is used with the szKey member of the VS_VERSIONINFO structure when searching a file for the VS_FIXEDFILEINFO structure.
        /// </summary>
        public UInt32 dwSignature;

        /// <summary>
        ///     Specifies the binary version number of this structure. The high-order word of this member contains the major version number, and the low-order word contains the minor version number.
        /// </summary>
        public UInt32 dwStrucVersion;

        /// <summary>
        ///     Specifies the most significant 32 bits of the file's binary version number. This member is used with dwFileVersionLS to form a 64-bit value used for numeric comparisons.
        /// </summary>
        public UInt32 dwFileVersionMS;

        /// <summary>
        ///     Specifies the least significant 32 bits of the file's binary version number. This member is used with dwFileVersionMS to form a 64-bit value used for numeric comparisons.
        /// </summary>
        public UInt32 dwFileVersionLS;

        /// <summary>
        ///     Specifies the most significant 32 bits of the binary version number of the product with which this file was distributed. This member is used with dwProductVersionLS to form a 64-bit value used for numeric comparisons.
        /// </summary>
        public UInt32 dwProductVersionMS;

        /// <summary>
        ///     Specifies the least significant 32 bits of the binary version number of the product with which this file was distributed. This member is used with dwProductVersionMS to form a 64-bit value used for numeric comparisons.
        /// </summary>
        public UInt32 dwProductVersionLS;

        /// <summary>
        ///     Contains a bitmask that specifies the valid bits in dwFileFlags. A bit is valid only if it was defined when the file was created.
        /// </summary>
        public UInt32 dwFileFlagsMask;

        /// <summary>
        ///     Contains a bitmask that specifies the Boolean attributes of the file.
        /// </summary>
        public UInt32 dwFileFlags;

        /// <summary>
        ///     Specifies the operating system for which this file was designed.
        /// </summary>
        public UInt32 dwFileOS;

        /// <summary>
        ///     Specifies the general type of file.
        /// </summary>
        public UInt32 dwFileType;

        /// <summary>
        ///     Specifies the function of the file.
        /// </summary>
        public UInt32 dwFileSubtype;

        /// <summary>
        ///     Specifies the most significant 32 bits of the file's 64-bit binary creation date and time stamp.
        /// </summary>
        public UInt32 dwFileDateMS;

        /// <summary>
        ///     Specifies the least significant 32 bits of the file's 64-bit binary creation date and time stamp.
        /// </summary>
        public UInt32 dwFileDateLS;

        /// <summary>
        ///     Creates a default Windows VS_FIXEDFILEINFO structure.
        /// </summary>
        /// <returns> A default Windows VS_FIXEDFILEINFO. </returns>
        public static VsFixedfileinfo GetWindowsDefault() {
            var fixedFileInfo = new VsFixedfileinfo();
            fixedFileInfo.dwSignature = Winver.VS_FFI_SIGNATURE;
            fixedFileInfo.dwStrucVersion = Winver.VS_FFI_STRUCVERSION;
            fixedFileInfo.dwFileFlagsMask = Winver.VS_FFI_FILEFLAGSMASK;
            fixedFileInfo.dwFileOS = (uint)Winver.FileOs.VOS__WINDOWS32;
            fixedFileInfo.dwFileSubtype = (uint)Winver.FileSubType.VFT2_UNKNOWN;
            fixedFileInfo.dwFileType = (uint)Winver.FileType.VFT_DLL;
            return fixedFileInfo;
        }
    }
}