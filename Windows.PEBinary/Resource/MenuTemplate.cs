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

    /// <summary>
    ///     Standard menu template.
    /// </summary>
    public class MenuTemplate : MenuTemplateBase {
        private Structures.MenuTemplate _header;
        private MenuTemplateItemCollection _menuItems = new MenuTemplateItemCollection();

        /// <summary>
        ///     Menu items.
        /// </summary>
        public MenuTemplateItemCollection MenuItems {
            get {
                return _menuItems;
            }
            set {
                _menuItems = value;
            }
        }

        /// <summary>
        ///     Read the menu template.
        /// </summary>
        /// <param name="lpRes">Address in memory.</param>
        internal override IntPtr Read(IntPtr lpRes) {
            _header = (Structures.MenuTemplate)Marshal.PtrToStructure(lpRes, typeof (Structures.MenuTemplate));

            var lpMenuItem = new IntPtr(lpRes.ToInt32() + Marshal.SizeOf(_header) + _header.wOffset);
            return _menuItems.Read(lpMenuItem);
        }

        /// <summary>
        ///     Write menu template to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal override void Write(BinaryWriter w) {
            w.Write(_header.wVersion);
            w.Write(_header.wOffset);
            ResourceUtil.Pad(w, _header.wOffset);
            _menuItems.Write(w);
        }

        /// <summary>
        ///     String representation of the menu in the MENU format.
        /// </summary>
        /// <returns>String representation of the menu.</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine("MENU");
            sb.Append(_menuItems);
            return sb.ToString();
        }
    }
}