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

namespace FearTheCowboy.Windows.Enumerations {
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

    /// <summary>
    ///     Bitmap compression options.
    /// </summary>
    public enum BitmapCompression {
        /// <summary>
        ///     An uncompressed format.
        /// </summary>
        BI_RGB = 0,

        /// <summary>
        ///     A run-length encoded (RLE) format for bitmaps with 8 bpp. The compression format is a 2-byte format consisting of a
        ///     count byte followed by a byte containing a color index. For more information, see Bitmap Compression.
        /// </summary>
        BI_RLE8 = 1,

        /// <summary>
        ///     An RLE format for bitmaps with 4 bpp. The compression format is a 2-byte format consisting of a count byte followed
        ///     by two word-length color indexes. For more information, see Bitmap Compression.
        /// </summary>
        BI_RLE4 = 2,

        /// <summary>
        ///     Specifies that the bitmap is not compressed and that the color table consists of three DWORD color masks that
        ///     specify the red, green, and blue components, respectively, of each pixel. This is valid when used with 16- and
        ///     32-bpp bitmaps.
        /// </summary>
        BI_BITFIELDS = 3,

        /// <summary>
        ///     Windows 98/Me, Windows 2000/XP: Indicates that the image is a JPEG image.
        /// </summary>
        BI_JPEG = 4,

        /// <summary>
        ///     Windows 98/Me, Windows 2000/XP: Indicates that the image is a PNG image.
        /// </summary>
        BI_PNG = 5,
    }
}