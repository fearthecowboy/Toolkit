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
    using Structures;

    /// <summary>
    ///     This structure depicts the organization of data in a .ico file.
    /// </summary>
    public class IconFile {
        #region GroupType enum

        /// <summary>
        ///     Resource type.
        /// </summary>
        public enum GroupType {
            /// <summary>
            ///     Icon.
            /// </summary>
            Icon = 1,

            /// <summary>
            ///     Cursor.
            /// </summary>
            Cursor = 2
        }

        #endregion

        private FileGrpIconDir _header;

        /// <summary>
        ///     An existing .ico file.
        /// </summary>
        /// <param name="filename">An existing icon (.ico) file.</param>
        public IconFile(string filename) {
            LoadFrom(filename);
        }

        /// <summary>
        ///     Type of the group icon resource.
        /// </summary>
        public GroupType Type
        {
            get
            {
                return (GroupType)_header.wType;
            }
            set
            {
                _header.wType = (byte)value;
            }
        }

        /// <summary>
        ///     Collection of icons in an .ico file.
        /// </summary>
        public List<IconFileIcon> Icons {get; set;} = new List<IconFileIcon>();

        /// <summary>
        ///     Load from a .ico file.
        /// </summary>
        /// <param name="filename">An existing icon (.ico) file.</param>
        public void LoadFrom(string filename) {
            var data = File.ReadAllBytes(filename);

            var lpData = Marshal.AllocHGlobal(data.Length);
            try {
                Marshal.Copy(data, 0, lpData, data.Length);
                Read(lpData);
            } finally {
                Marshal.FreeHGlobal(lpData);
            }
        }

        /// <summary>
        ///     Read icons.
        /// </summary>
        /// <param name="lpData">Pointer to the beginning of a FILEGRPICONDIR structure.</param>
        /// <returns>Pointer to the end of a FILEGRPICONDIR structure.</returns>
        internal IntPtr Read(IntPtr lpData) {
            Icons.Clear();

            _header = (FileGrpIconDir)Marshal.PtrToStructure(lpData, typeof (FileGrpIconDir));

            var lpEntry = new IntPtr(lpData.ToInt32() + Marshal.SizeOf(_header));

            for (var i = 0; i < _header.wCount; i++) {
                var iconFileIcon = new IconFileIcon();
                lpEntry = iconFileIcon.Read(lpEntry, lpData);
                Icons.Add(iconFileIcon);
            }

            return lpEntry;
        }
    }
}