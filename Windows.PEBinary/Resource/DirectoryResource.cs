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
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using Enumerations;
    using Structures;

    /// <summary>
    ///     This structure depicts the organization of data in a hardware-independent icon resource.
    /// </summary>
    public class DirectoryResource<ImageResourceType> : Resource where ImageResourceType : IconImageResource, new() {
        private GrpIconDir _header;

        /// <summary>
        ///     A hardware-independent icon resource.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResource">Resource ID.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="language">Language ID.</param>
        /// <param name="size">Resource size.</param>
        internal DirectoryResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, UInt16 language, int size)
            : base(hModule, hResource, type, name, language, size) {
        }

        /// <summary>
        ///     A new hardware-independent icon resource.
        /// </summary>
        public DirectoryResource(ResourceTypes resourceType)
            : base(
                IntPtr.Zero, IntPtr.Zero, new ResourceId(resourceType), new ResourceId(1), ResourceUtil.NEUTRALLANGID,
                Marshal.SizeOf(typeof (GrpIconDir))) {
            switch (resourceType) {
                case ResourceTypes.RT_GROUP_CURSOR:
                    _header.wType = 2;
                    break;
                case ResourceTypes.RT_GROUP_ICON:
                    _header.wType = 1;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        ///     Returns the type of the resource in this group.
        /// </summary>
        public ResourceTypes ResourceType
        {
            get
            {
                switch (_header.wType) {
                    case 1:
                        return ResourceTypes.RT_ICON;
                    case 2:
                        return ResourceTypes.RT_CURSOR;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        ///     Icons contained in this hardware-independent icon resource.
        /// </summary>
        public List<ImageResourceType> Icons {get; set;} = new List<ImageResourceType>();

        /// <summary>
        ///     Save a hardware-independent icon resource to an executable file.
        /// </summary>
        /// <param name="filename">Name of an executable file (.exe or .dll).</param>
        public override void SaveTo(string filename) {
            base.SaveTo(filename);

            foreach (var icon in Icons) {
                icon.SaveIconTo(filename);
            }
        }

        /// <summary>
        ///     Read a hardware-independent icon resource from a loaded module.
        /// </summary>
        /// <param name="hModule">Loaded executable module.</param>
        /// <param name="lpRes">Pointer to the beginning of a hardware-independent icon resource.</param>
        /// <returns>Pointer to the end of the hardware-independent icon resource.</returns>
        internal override IntPtr Read(IntPtr hModule, IntPtr lpRes) {
            Icons.Clear();

            _header = (GrpIconDir)Marshal.PtrToStructure(lpRes, typeof (GrpIconDir));

            var pEntry = new IntPtr(lpRes.ToInt32() + Marshal.SizeOf(_header));

            for (UInt16 i = 0; i < _header.wImageCount; i++) {
                var iconImageResource = new ImageResourceType();
                pEntry = iconImageResource.Read(hModule, pEntry);
                Icons.Add(iconImageResource);
            }

            return pEntry;
        }

        /// <summary>
        ///     Write a hardware-independent icon resource to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal override void Write(BinaryWriter w) {
            w.Write(_header.wReserved);
            w.Write(_header.wType);
            w.Write((UInt16)Icons.Count);
            ResourceUtil.PadToWORD(w);
            foreach (var icon in Icons) {
                icon.Write(w);
            }
        }
    }
}