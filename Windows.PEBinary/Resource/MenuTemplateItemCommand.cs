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
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using Enumerations;
    using Structures;

    /// <summary>
    ///     A command menu item.
    /// </summary>
    public class MenuTemplateItemCommand : MenuTemplateItem {
        /// <summary>
        ///     Command menu id.
        /// </summary>
        public UInt16 MenuId {get; set;}

        /// <summary>
        ///     Returns true if the item is a separator.
        /// </summary>
        public bool IsSeparator
        {
            get
            {
                return ((_header.mtOption & (uint)MenuFlags.MF_SEPARATOR) > 0) || (_header.mtOption == 0 && _menuString == null && MenuId == 0);
            }
        }

        /// <summary>
        ///     Read a command menu item.
        /// </summary>
        /// <param name="lpRes">Address in memory.</param>
        /// <returns>End of the menu item structure.</returns>
        internal override IntPtr Read(IntPtr lpRes) {
            _header = (MenuItemTemplate)Marshal.PtrToStructure(lpRes, typeof (MenuItemTemplate));

            lpRes = new IntPtr(lpRes.ToInt32() + Marshal.SizeOf(_header));

            MenuId = (UInt16)Marshal.ReadInt16(lpRes);
            lpRes = new IntPtr(lpRes.ToInt32() + 2);

            lpRes = base.Read(lpRes);

            return lpRes;
        }

        /// <summary>
        ///     Write menu item to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal override void Write(BinaryWriter w) {
            w.Write(_header.mtOption);
            w.Write(MenuId);
            base.Write(w);
        }

        /// <summary>
        ///     String representation in the MENU format.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>String representation.</returns>
        public override string ToString(int indent) {
            var sb = new StringBuilder();
            if (IsSeparator) {
                sb.AppendLine(string.Format("{0}MENUITEM SEPARATOR", new String(' ', indent)));
            } else {
                sb.AppendLine(string.Format("{0}MENUITEM \"{1}\", {2}", new String(' ', indent),
                    _menuString == null ? string.Empty : _menuString.Replace("\t", @"\t"), MenuId));
            }
            return sb.ToString();
        }
    }
}