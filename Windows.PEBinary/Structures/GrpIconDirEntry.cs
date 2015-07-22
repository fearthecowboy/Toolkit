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
    ///     Hardware-independent icon directory entry. See http://msdn.microsoft.com/en-us/library/ms997538.aspx for more
    ///     information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct GrpIconDirEntry {
        /// <summary>
        ///     Width of the image. Starting with Windows 95 a value of 0 represents width of 256.
        /// </summary>
        public Byte bWidth;

        /// <summary>
        ///     Height of the image. Starting with Windows 95 a value of 0 represents height of 256.
        /// </summary>
        public Byte bHeight;

        /// <summary>
        ///     Number of colors in the image. bColors = 1  &lt;&lt;(wBitsPerPixel * wPlanes)
        ///     If wBitsPerPixel* wPlanes is greater orequal to 8, then bColors= 0 .
        /// </summary>
        public Byte bColors;

        /// <summary>
        ///     Reserved.
        /// </summary>
        public Byte bReserved;

        /// <summary>
        ///     Number of bitmap planes. 1: monochrome bitmap
        /// </summary>
        public UInt16 wPlanes;

        /// <summary>
        ///     Bits per pixel. 1: monochrome bitmap
        /// </summary>
        public UInt16 wBitsPerPixel;

        /// <summary>
        ///     Image size in bytes.
        /// </summary>
        public UInt32 dwImageSize;

        /// <summary>
        ///     Icon ID.
        /// </summary>
        public UInt16 nID;
    }
}