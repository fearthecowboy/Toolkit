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
    using Enumerations;

    /// <summary>
    ///     This structure depicts the organization of data in a hardware-independent cursor resource.
    /// </summary>
    public class CursorDirectoryResource : DirectoryResource<CursorResource> {
        /// <summary>
        ///     A hardware-independent cursor resource.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResource">Resource ID.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="language">Language ID.</param>
        /// <param name="size">Resource size.</param>
        internal CursorDirectoryResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, UInt16 language, int size)
            : base(hModule, hResource, type, name, language, size) {
        }

        /// <summary>
        ///     A new hardware-independent cursor resource.
        /// </summary>
        public CursorDirectoryResource() : base(ResourceTypes.RT_GROUP_CURSOR) {
        }

        /// <summary>
        ///     A new collection of cursors that can be embedded into an executable file.
        /// </summary>
        public CursorDirectoryResource(IconFile iconFile) : base(ResourceTypes.RT_GROUP_CURSOR) {
            for (UInt16 id = 0; id < iconFile.Icons.Count; id++) {
                var cursorResource = new CursorResource(iconFile.Icons[id], new ResourceId(id), _language);
                // cursor structure abuses planes and bits per pixel for cursor data
                cursorResource.HotspotX = iconFile.Icons[id].Header.wPlanes;
                cursorResource.HotspotY = iconFile.Icons[id].Header.wBitsPerPixel;
                Icons.Add(cursorResource);
            }
        }
    }
}