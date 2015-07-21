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

namespace Toolkit.Windows.Structures {
    using System;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using Enumerations;

    /// <summary>
    ///     A bitmap info header. See http://msdn.microsoft.com/en-us/library/ms532290.aspx for more information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct BitmapInfoHeader {
        /// <summary>
        ///     Bitmap information size.
        /// </summary>
        public UInt32 biSize;

        /// <summary>
        ///     Bitmap width.
        /// </summary>
        public Int32 biWidth;

        /// <summary>
        ///     Bitmap height.
        /// </summary>
        public Int32 biHeight;

        /// <summary>
        ///     Number of logical planes.
        /// </summary>
        public UInt16 biPlanes;

        /// <summary>
        ///     Bitmap bitrate.
        /// </summary>
        public UInt16 biBitCount;

        /// <summary>
        ///     Bitmap compression.
        /// </summary>
        public UInt32 biCompression;

        /// <summary>
        ///     Image size.
        /// </summary>
        public UInt32 biSizeImage;

        /// <summary>
        ///     Horizontal pixel resolution.
        /// </summary>
        public Int32 biXPelsPerMeter;

        /// <summary>
        ///     Vertical pixel resolution.
        /// </summary>
        public Int32 biYPelsPerMeter;

        /// <summary>
        /// </summary>
        public UInt32 biClrUsed;

        /// <summary>
        /// </summary>
        public UInt32 biClrImportant;

        /// <summary>
        ///     Returns the current bitmap compression.
        /// </summary>
        public BitmapCompression BitmapCompression {
            get {
                return (BitmapCompression)biCompression;
            }
        }

        /// <summary>
        ///     Bitmap pixel format.
        /// </summary>
        public PixelFormat PixelFormat {
            get {
                switch (biBitCount) {
                    case 1:
                        return PixelFormat.Format1bppIndexed;
                    case 4:
                        return PixelFormat.Format4bppIndexed;
                    case 8:
                        return PixelFormat.Format8bppIndexed;
                    case 16:
                        return PixelFormat.Format16bppRgb565;
                    case 24:
                        return PixelFormat.Format24bppRgb;
                    case 32:
                        return PixelFormat.Format32bppArgb;
                    default:
                        return PixelFormat.Undefined;
                }
            }
        }

        /// <summary>
        ///     Bitmap pixel format English standard string.
        /// </summary>
        public string PixelFormatString {
            get {
                switch (PixelFormat) {
                    case PixelFormat.Format1bppIndexed:
                        return "1-bit B/W";
                    case PixelFormat.Format24bppRgb:
                        return "24-bit True Colors";
                    case PixelFormat.Format32bppArgb:
                    case PixelFormat.Format32bppRgb:
                        return "32-bit Alpha Channel";
                    case PixelFormat.Format8bppIndexed:
                        return "8-bit 256 Colors";
                    case PixelFormat.Format4bppIndexed:
                        return "4-bit 16 Colors";
                }
                return "Unknown";
            }
        }
    }
}