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

namespace FearTheCowboy.Windows.Structures {
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

    /// <summary>
    ///     Contains information about an icon or a cursor.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Iconinfo {
        /// <summary>
        ///     Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies an icon; FALSE specifies a
        ///     cursor.
        /// </summary>
        public bool IsIcon;

        /// <summary>
        ///     Specifies the x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot spot is always in the
        ///     center of the icon, and this member is ignored.
        /// </summary>
        public int xHotspot;

        /// <summary>
        ///     Specifies the y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot spot is always in
        ///     the center of the icon, and this member is ignored.
        /// </summary>
        public int yHotspot;

        /// <summary>
        ///     Specifies the icon bitmask bitmap.
        /// </summary>
        public IntPtr MaskBitmap;

        /// <summary>
        ///     Handle to the icon color bitmap.
        /// </summary>
        public IntPtr ColorBitmap;
    }
}