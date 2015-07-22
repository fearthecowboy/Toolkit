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

namespace FearTheCowboy.Windows.Resource {
    //-----------------------------------------------------------------------
    //     ResourceLib Original Code from http://resourcelib.codeplex.com
    //     Original Copyright (c) 2008-2009 Vestris Inc.
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
    using System;
    using System.Runtime.InteropServices;
    using Structures;

    /// <summary>
    ///     This structure depicts the organization of icon data in a .ico file.
    /// </summary>
    public class IconFileIcon {
        /// <summary>
        ///     Icon header.
        /// </summary>
        public FileGrpIconDirEntry Header {get; private set;}

        /// <summary>
        ///     Icon bitmap.
        /// </summary>
        public DeviceIndependentBitmap Image {get; set;} = new DeviceIndependentBitmap();

        /// <summary>
        ///     Icon width.
        /// </summary>
        public Byte Width
        {
            get
            {
                return Header.bWidth;
            }
        }

        /// <summary>
        ///     Icon height.
        /// </summary>
        public Byte Height
        {
            get
            {
                return Header.bHeight;
            }
        }

        /// <summary>
        ///     Image size in bytes.
        /// </summary>
        public UInt32 ImageSize
        {
            get
            {
                return Header.dwImageSize;
            }
        }

        /// <summary>
        ///     Read a single icon (.ico).
        /// </summary>
        /// <param name="lpData">Pointer to the beginning of this icon's data.</param>
        /// <param name="lpAllData">Pointer to the beginning of all icon data.</param>
        /// <returns>Pointer to the end of this icon's data.</returns>
        internal IntPtr Read(IntPtr lpData, IntPtr lpAllData) {
            Header = (FileGrpIconDirEntry)Marshal.PtrToStructure(lpData, typeof (FileGrpIconDirEntry));

            var lpImage = new IntPtr(lpAllData.ToInt32() + Header.dwFileOffset);
            Image.Read(lpImage, Header.dwImageSize);

            return new IntPtr(lpData.ToInt32() + Marshal.SizeOf(Header));
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