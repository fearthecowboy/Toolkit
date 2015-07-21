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
    using Enumerations;

    /// <summary>
    ///     A generic resource.
    /// </summary>
    public class MenuResource : Resource {
        private MenuTemplateBase _menu;

        /// <summary>
        ///     A structured menu resource.
        /// </summary>
        public MenuResource() : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(ResourceTypes.RT_MENU), null, ResourceUtil.NEUTRALLANGID, 0) {
        }

        /// <summary>
        ///     A structured menu resource embedded in an executable module.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResource">Resource handle.</param>
        /// <param name="type">Type of resource.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="language">Language id.</param>
        /// <param name="size">Resource size.</param>
        public MenuResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, UInt16 language, int size)
            : base(hModule, hResource, type, name, language, size) {
        }

        /// <summary>
        ///     Menu template.
        /// </summary>
        public MenuTemplateBase Menu {
            get {
                return _menu;
            }
            set {
                _menu = value;
            }
        }

        /// <summary>
        ///     Read a menu resource.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="lpRes">Pointer to the beginning of a resource.</param>
        /// <returns>Pointer to the end of the resource.</returns>
        internal override IntPtr Read(IntPtr hModule, IntPtr lpRes) {
            var version = (UInt16)Marshal.ReadInt16(lpRes);
            switch (version) {
                case 0:
                    _menu = new MenuTemplate();
                    break;
                case 1:
                    _menu = new MenuExTemplate();
                    break;
                default:
                    throw new NotSupportedException(string.Format("Unexpected menu header version {0}", version));
            }

            return _menu.Read(lpRes);
        }

        /// <summary>
        ///     Write the menu resource to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal override void Write(BinaryWriter w) {
            _menu.Write(w);
        }

        /// <summary>
        ///     String representation of the menu resource in the MENU format.
        /// </summary>
        /// <returns>String representation of the menu resource.</returns>
        public override string ToString() {
            return string.Format("{0} {1}", Name, Menu);
        }
    }
}