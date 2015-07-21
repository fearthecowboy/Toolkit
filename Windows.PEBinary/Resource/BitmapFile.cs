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
    using System.IO;
    using System.Runtime.InteropServices;
    using Structures;

    /// <summary>
    ///     A bitmap file in a .bmp format.
    /// </summary>
    public class BitmapFile {
        private DeviceIndependentBitmap _bitmap;
        private BitmapFileHeader _header;

        /// <summary>
        ///     An existing bitmap file.
        /// </summary>
        /// <param name="filename">A file in a .bmp format.</param>
        public BitmapFile(string filename) {
            var data = File.ReadAllBytes(filename);

            var pFileHeaderData = Marshal.AllocHGlobal(Marshal.SizeOf(_header));
            try {
                Marshal.Copy(data, 0, pFileHeaderData, Marshal.SizeOf(_header));
                _header = (BitmapFileHeader)Marshal.PtrToStructure(pFileHeaderData, typeof (BitmapFileHeader));
            } finally {
                Marshal.FreeHGlobal(pFileHeaderData);
            }

            var size = data.Length - Marshal.SizeOf(_header);
            var bitmapData = new byte[size];
            Buffer.BlockCopy(data, Marshal.SizeOf(_header), bitmapData, 0, size);
            _bitmap = new DeviceIndependentBitmap(bitmapData);
        }

        /// <summary>
        ///     Device independent bitmap.
        /// </summary>
        public DeviceIndependentBitmap Bitmap {
            get {
                return _bitmap;
            }
        }
    }
}