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

    /// <summary>
    ///     Extended menu template.
    /// </summary>
    public class MenuExTemplate : MenuTemplateBase {
        private Structures.MenuExTemplate _header;

        /// <summary>
        ///     Menu items.
        /// </summary>
        public MenuExTemplateItemCollection MenuItems {get; set;} = new MenuExTemplateItemCollection();

        /// <summary>
        ///     Read the menu template.
        /// </summary>
        /// <param name="lpRes">Address in memory.</param>
        internal override IntPtr Read(IntPtr lpRes) {
            _header = (Structures.MenuExTemplate)Marshal.PtrToStructure(lpRes, typeof (Structures.MenuExTemplate));

            var lpMenuItem = ResourceUtil.Align(lpRes.ToInt32() + Marshal.SizeOf(_header) + _header.wOffset);

            return MenuItems.Read(lpMenuItem);
        }

        /// <summary>
        ///     Write the menu template.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal override void Write(BinaryWriter w) {
            var head = w.BaseStream.Position;
            // write header
            w.Write(_header.wVersion);
            w.Write(_header.wOffset);
            // w.Write(_header.dwHelpId);
            // pad to match the offset value
            ResourceUtil.Pad(w, (UInt16)(_header.wOffset - 4));
            // seek to the beginning of the menu item per offset value
            // this may be behind, ie. the help id structure is part of the first popup menu
            w.BaseStream.Seek(head + _header.wOffset + 4, SeekOrigin.Begin);
            // write menu items
            MenuItems.Write(w);
        }

        /// <summary>
        ///     String representation of the menu in the MENUEX format.
        /// </summary>
        /// <returns>String representation of the menu.</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine("MENUEX");
            sb.Append(MenuItems);
            return sb.ToString();
        }
    }
}