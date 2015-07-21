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

    /// <summary>
    ///     Hardware-independent icon directory entry in an .ico file.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct FileGrpIconDirEntry {
        /// <summary>
        ///     Icon width.
        /// </summary>
        public Byte bWidth;

        /// <summary>
        ///     Icon height.
        /// </summary>
        public Byte bHeight;

        /// <summary>
        ///     Colors; 0 means 256 or more.
        /// </summary>
        public Byte bColors;

        /// <summary>
        ///     Reserved.
        /// </summary>
        public Byte bReserved;

        /// <summary>
        ///     Number of bitmap planes for icons. Horizontal hotspot for cursors.
        /// </summary>
        public UInt16 wPlanes;

        /// <summary>
        ///     Bits per pixel for icons. Vertical hostpot for cursors.
        /// </summary>
        public UInt16 wBitsPerPixel;

        /// <summary>
        ///     Image size in bytes.
        /// </summary>
        public UInt32 dwImageSize;

        /// <summary>
        ///     Offset of bitmap data from the beginning of the file.
        /// </summary>
        public UInt32 dwFileOffset;
    }
}