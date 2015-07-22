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
    using Enumerations;

    /// <summary>
    ///     This structure depicts the organization of data in a cursor resource.
    /// </summary>
    public class CursorResource : IconImageResource {
        /// <summary>
        ///     An existing cursor resource.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResource">Resource ID.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="language">Language ID.</param>
        /// <param name="size">Resource size.</param>
        internal CursorResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, UInt16 language, int size)
            : base(hModule, hResource, type, name, language, size) {
        }

        /// <summary>
        ///     A new cursor resource.
        /// </summary>
        public CursorResource() : base(new ResourceId(ResourceTypes.RT_CURSOR)) {
        }

        /// <summary>
        ///     Convert into an icon resource that can be written into an executable.
        /// </summary>
        /// <param name="icon">Icon image.</param>
        /// <param name="id">Icon Id.</param>
        /// <param name="language">Language.</param>
        /// <returns>An icon resource.</returns>
        public CursorResource(IconFileIcon icon, ResourceId id, UInt16 language) : base(icon, new ResourceId(ResourceTypes.RT_CURSOR), id, language) {
        }

        /// <summary>
        ///     Horizontal hotspot coordinate.
        ///     The hot spot of a cursor is the point to which Windows refers in tracking the cursor's position.
        /// </summary>
        public UInt16 HotspotX {get; set;}

        /// <summary>
        ///     Vertical hot spot coordinate.
        ///     The hot spot of a cursor is the point to which Windows refers in tracking the cursor's position.
        /// </summary>
        public UInt16 HotspotY {get; set;}

        /// <summary>
        ///     Write the cursor data to a file.
        /// </summary>
        /// <param name="filename">Target executable file.</param>
        public override void SaveIconTo(string filename) {
            var dataWithHotspot = new byte[Image.Data.Length + 4];
            Buffer.BlockCopy(Image.Data, 0, dataWithHotspot, 4, Image.Data.Length);
            dataWithHotspot[0] = (byte)(HotspotX & 0xFF);
            dataWithHotspot[1] = (byte)(HotspotX >> 8);
            dataWithHotspot[2] = (byte)(HotspotY & 0xFF);
            dataWithHotspot[3] = (byte)(HotspotY >> 8);

            SaveTo(filename, _type, new ResourceId(_header.nID), _language, dataWithHotspot);
        }

        /// <summary>
        ///     Read DIB image.
        /// </summary>
        /// <param name="dibBits">DIB bits.</param>
        /// <param name="size">DIB size.</param>
        internal override void ReadImage(IntPtr dibBits, UInt32 size) {
            HotspotX = (UInt16)Marshal.ReadInt16(dibBits);
            dibBits = new IntPtr(dibBits.ToInt32() + sizeof (UInt16));
            HotspotY = (UInt16)Marshal.ReadInt16(dibBits);
            dibBits = new IntPtr(dibBits.ToInt32() + sizeof (UInt16));

            base.ReadImage(dibBits, size - 2*sizeof (UInt16));
        }
    }
}