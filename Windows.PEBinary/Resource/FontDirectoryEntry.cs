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
    using System.Text;
    using Structures;

    /// <summary>
    ///     A font directory entry.
    /// </summary>
    public class FontDirectoryEntry {
        private string _deviceName;
        private string _faceName;
        private FontDirEntry _font;
        private UInt16 _fontOrdinal;

        /// <summary>
        ///     Font ordinal.
        /// </summary>
        public UInt16 FontOrdinal {
            get {
                return _fontOrdinal;
            }
            set {
                _fontOrdinal = value;
            }
        }

        /// <summary>
        ///     Typeface name of the font.
        /// </summary>
        public string FaceName {
            get {
                return _faceName;
            }
            set {
                _faceName = value;
            }
        }

        /// <summary>
        ///     Specifies the name of the device if this font file is designated for a specific device.
        /// </summary>
        public string DeviceName {
            get {
                return _deviceName;
            }
            set {
                _deviceName = value;
            }
        }

        /// <summary>
        ///     Font information.
        /// </summary>
        public FontDirEntry Font {
            get {
                return _font;
            }
            set {
                _font = value;
            }
        }

        /// <summary>
        ///     Read the font directory entry.
        /// </summary>
        /// <param name="lpRes">Pointer in memory.</param>
        /// <returns>Pointer to the end of the font directory entry.</returns>
        internal IntPtr Read(IntPtr lpRes) {
            var lpHead = lpRes;

            _fontOrdinal = (UInt16)Marshal.ReadInt16(lpRes);
            lpRes = new IntPtr(lpRes.ToInt32() + 2);

            _font = (FontDirEntry)Marshal.PtrToStructure(lpRes, typeof (FontDirEntry));

            lpRes = new IntPtr(lpRes.ToInt32() + Marshal.SizeOf(_font));

            _deviceName = Marshal.PtrToStringAnsi(lpRes);
            lpRes = new IntPtr(lpRes.ToInt32() + _deviceName.Length + 1);

            _faceName = Marshal.PtrToStringAnsi(lpRes);
            lpRes = new IntPtr(lpRes.ToInt32() + _faceName.Length + 1);

            return lpRes;
        }

        /// <summary>
        ///     Write the font directory entry to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        public void Write(BinaryWriter w) {
            w.Write(_fontOrdinal);
            w.Write(ResourceUtil.GetBytes(_font));

            // device name
            if (string.IsNullOrEmpty(_deviceName)) {
                w.Write((byte)0);
            } else {
                w.Write(Encoding.ASCII.GetBytes(_deviceName));
            }

            // face name
            if (string.IsNullOrEmpty(_faceName)) {
                w.Write((byte)0);
            } else {
                w.Write(Encoding.ASCII.GetBytes(_faceName));
            }
        }
    }
}