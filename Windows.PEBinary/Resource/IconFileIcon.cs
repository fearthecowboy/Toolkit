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

namespace Toolkit.Windows.Resource {
    using System;
    using System.Runtime.InteropServices;
    using Structures;

    /// <summary>
    ///     This structure depicts the organization of icon data in a .ico file.
    /// </summary>
    public class IconFileIcon {
        private FileGrpIconDirEntry _header;
        private DeviceIndependentBitmap _image = new DeviceIndependentBitmap();

        /// <summary>
        ///     Icon header.
        /// </summary>
        public FileGrpIconDirEntry Header {
            get {
                return _header;
            }
        }

        /// <summary>
        ///     Icon bitmap.
        /// </summary>
        public DeviceIndependentBitmap Image {
            get {
                return _image;
            }
            set {
                _image = value;
            }
        }

        /// <summary>
        ///     Icon width.
        /// </summary>
        public Byte Width {
            get {
                return _header.bWidth;
            }
        }

        /// <summary>
        ///     Icon height.
        /// </summary>
        public Byte Height {
            get {
                return _header.bHeight;
            }
        }

        /// <summary>
        ///     Image size in bytes.
        /// </summary>
        public UInt32 ImageSize {
            get {
                return _header.dwImageSize;
            }
        }

        /// <summary>
        ///     Read a single icon (.ico).
        /// </summary>
        /// <param name="lpData">Pointer to the beginning of this icon's data.</param>
        /// <param name="lpAllData">Pointer to the beginning of all icon data.</param>
        /// <returns>Pointer to the end of this icon's data.</returns>
        internal IntPtr Read(IntPtr lpData, IntPtr lpAllData) {
            _header = (FileGrpIconDirEntry)Marshal.PtrToStructure(lpData, typeof (FileGrpIconDirEntry));

            var lpImage = new IntPtr(lpAllData.ToInt32() + _header.dwFileOffset);
            _image.Read(lpImage, _header.dwImageSize);

            return new IntPtr(lpData.ToInt32() + Marshal.SizeOf(_header));
        }

        /// <summary>
        ///     Icon size as a string.
        /// </summary>
        /// <returns>Icon size in the width x height format.</returns>
        public override string ToString() {
            return string.Format("{0}x{1}", Width, Height);
        }
    }
}