﻿// 
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
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using Structures;

    /// <summary>
    ///     A font directory entry.
    /// </summary>
    public class FontDirectoryEntry {
        /// <summary>
        ///     Font ordinal.
        /// </summary>
        public UInt16 FontOrdinal {get; set;}

        /// <summary>
        ///     Typeface name of the font.
        /// </summary>
        public string FaceName {get; set;}

        /// <summary>
        ///     Specifies the name of the device if this font file is designated for a specific device.
        /// </summary>
        public string DeviceName {get; set;}

        /// <summary>
        ///     Font information.
        /// </summary>
        public FontDirEntry Font {get; set;}

        /// <summary>
        ///     Read the font directory entry.
        /// </summary>
        /// <param name="lpRes">Pointer in memory.</param>
        /// <returns>Pointer to the end of the font directory entry.</returns>
        internal IntPtr Read(IntPtr lpRes) {
            var lpHead = lpRes;

            FontOrdinal = (UInt16)Marshal.ReadInt16(lpRes);
            lpRes = new IntPtr(lpRes.ToInt32() + 2);

            Font = (FontDirEntry)Marshal.PtrToStructure(lpRes, typeof (FontDirEntry));

            lpRes = new IntPtr(lpRes.ToInt32() + Marshal.SizeOf(Font));

            DeviceName = Marshal.PtrToStringAnsi(lpRes);
            lpRes = new IntPtr(lpRes.ToInt32() + DeviceName.Length + 1);

            FaceName = Marshal.PtrToStringAnsi(lpRes);
            lpRes = new IntPtr(lpRes.ToInt32() + FaceName.Length + 1);

            return lpRes;
        }

        /// <summary>
        ///     Write the font directory entry to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        public void Write(BinaryWriter w) {
            w.Write(FontOrdinal);
            w.Write(ResourceUtil.GetBytes(Font));

            // device name
            if (string.IsNullOrEmpty(DeviceName)) {
                w.Write((byte)0);
            } else {
                w.Write(Encoding.ASCII.GetBytes(DeviceName));
            }

            // face name
            if (string.IsNullOrEmpty(FaceName)) {
                w.Write((byte)0);
            } else {
                w.Write(Encoding.ASCII.GetBytes(FaceName));
            }
        }
    }
}