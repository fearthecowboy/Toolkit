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
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using Enumerations;

    /// <summary>
    ///     A font directory, RT_FONTDIR resource.
    /// </summary>
    public class FontDirectoryResource : Resource {
        private List<FontDirectoryEntry> _fonts = new List<FontDirectoryEntry>();
        private byte[] _reserved;

        /// <summary>
        ///     A new font resource.
        /// </summary>
        public FontDirectoryResource() : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(ResourceTypes.RT_FONTDIR), null, ResourceUtil.NEUTRALLANGID, 0) {
        }

        /// <summary>
        ///     An existing font resource.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResource">Resource ID.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="language">Language ID.</param>
        /// <param name="size">Resource size.</param>
        public FontDirectoryResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, UInt16 language, int size)
            : base(hModule, hResource, type, name, language, size) {
        }

        /// <summary>
        ///     Number of fonts in this directory.
        /// </summary>
        public List<FontDirectoryEntry> Fonts {
            get {
                return _fonts;
            }
            set {
                _fonts = value;
            }
        }

        /// <summary>
        ///     Read the font resource.
        /// </summary>
        /// <param name="hModule">Handle to a module.</param>
        /// <param name="lpRes">Pointer to the beginning of the font structure.</param>
        /// <returns>Address of the end of the font structure.</returns>
        internal override IntPtr Read(IntPtr hModule, IntPtr lpRes) {
            var lpHead = lpRes;

            var count = (UInt16)Marshal.ReadInt16(lpRes);
            lpRes = new IntPtr(lpRes.ToInt32() + 2);

            for (var i = 0; i < count; i++) {
                var fontEntry = new FontDirectoryEntry();
                lpRes = fontEntry.Read(lpRes);
                _fonts.Add(fontEntry);
            }

            var reservedLen = _size - (lpRes.ToInt32() - lpHead.ToInt32() - 1);
            _reserved = new byte[reservedLen];
            Marshal.Copy(lpRes, _reserved, 0, reservedLen);

            return lpRes;
        }

        /// <summary>
        ///     Write the font directory to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal override void Write(BinaryWriter w) {
            w.Write((UInt16)_fonts.Count);
            foreach (var fontEntry in _fonts) {
                fontEntry.Write(w);
            }
            w.Write(_reserved);
        }
    }
}