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
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using Enumerations;
    using Structures;

    /// <summary>
    ///     A device-independent image consists of a BITMAPINFOHEADER where
    ///     bmWidth is the width of the image andbmHeight is double the height
    ///     of the image, followed by the bitmap color table, followed by the image
    ///     pixels, followed by the mask pixels.
    /// </summary>
    public class DeviceIndependentBitmap {
        private Bitmap _color;
        private byte[] _data;
        private Bitmap _image;
        private Bitmap _mask;

        /// <summary>
        ///     A new icon image.
        /// </summary>
        public DeviceIndependentBitmap() {
        }

        /// <summary>
        ///     A device-independent bitmap.
        /// </summary>
        /// <param name="data">Bitmap data.</param>
        public DeviceIndependentBitmap(byte[] data) {
            Data = data;
        }

        /// <summary>
        ///     Create a copy of an image.
        /// </summary>
        /// <param name="image">Source image.</param>
        public DeviceIndependentBitmap(DeviceIndependentBitmap image) {
            _data = new byte[image._data.Length];
            Buffer.BlockCopy(image._data, 0, _data, 0, image._data.Length);
            Header = image.Header;
        }

        /// <summary>
        ///     Raw image data.
        /// </summary>
        public byte[] Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;

                var pData = Marshal.AllocHGlobal(Marshal.SizeOf(Header));
                try {
                    Marshal.Copy(_data, 0, pData, Marshal.SizeOf(Header));
                    Header = (BitmapInfoHeader)Marshal.PtrToStructure(pData, typeof (BitmapInfoHeader));
                } finally {
                    Marshal.FreeHGlobal(pData);
                }
            }
        }

        /// <summary>
        ///     Bitmap info header.
        /// </summary>
        public BitmapInfoHeader Header {get; private set;}

        /// <summary>
        ///     Bitmap size in bytes.
        /// </summary>
        public int Size
        {
            get
            {
                return _data.Length;
            }
        }

        /// <summary>
        ///     Size of the image mask.
        /// </summary>
        private Int32 MaskImageSize
        {
            get
            {
                return (Header.biHeight/2*GetDIBRowWidth(Header.biWidth));
            }
        }

        private Int32 XorImageSize
        {
            get
            {
                return (Header.biHeight/2*GetDIBRowWidth(Header.biWidth*Header.biBitCount*Header.biPlanes));
            }
        }

        /// <summary>
        ///     Position of the DIB bitmap bits within a DIB bitmap array.
        /// </summary>
        private Int32 XorImageIndex
        {
            get
            {
                return (Int32)(Marshal.SizeOf(Header) + ColorCount*Marshal.SizeOf(new RgbQuad()));
            }
        }

        /// <summary>
        ///     Number of colors in the palette.
        /// </summary>
        private UInt32 ColorCount
        {
            get
            {
                if (Header.biClrUsed != 0) {
                    return Header.biClrUsed;
                }

                if (Header.biBitCount <= 8) {
                    return (UInt32)(1 << Header.biBitCount);
                }

                return 0;
            }
        }

        private Int32 MaskImageIndex
        {
            get
            {
                return XorImageIndex + XorImageSize;
            }
        }

        /// <summary>
        ///     Bitmap monochrome mask.
        /// </summary>
        public Bitmap Mask
        {
            get
            {
                if (_mask == null) {
                    var hdc = IntPtr.Zero;
                    var hBmp = IntPtr.Zero;
                    var hBmpOld = IntPtr.Zero;
                    var bitsInfo = IntPtr.Zero;
                    var bits = IntPtr.Zero;

                    try {
                        // extract monochrome mask
                        hdc = Gdi32.CreateCompatibleDC(IntPtr.Zero);
                        if (hdc == null) {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        hBmp = Gdi32.CreateCompatibleBitmap(hdc, Header.biWidth, Header.biHeight/2);
                        if (hBmp == null) {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        hBmpOld = Gdi32.SelectObject(hdc, hBmp);

                        // prepare BitmapInfoHeader for mono bitmap:
                        var monoBmHdrSize = (int)Header.biSize + Marshal.SizeOf(new RgbQuad())*2;

                        bitsInfo = Marshal.AllocCoTaskMem(monoBmHdrSize);
                        Marshal.WriteInt32(bitsInfo, Marshal.SizeOf(Header));
                        Marshal.WriteInt32(bitsInfo, 4, Header.biWidth);
                        Marshal.WriteInt32(bitsInfo, 8, Header.biHeight/2);
                        Marshal.WriteInt16(bitsInfo, 12, 1);
                        Marshal.WriteInt16(bitsInfo, 14, 1);
                        Marshal.WriteInt32(bitsInfo, 16, (Int32)BitmapCompression.BI_RGB);
                        Marshal.WriteInt32(bitsInfo, 20, 0);
                        Marshal.WriteInt32(bitsInfo, 24, 0);
                        Marshal.WriteInt32(bitsInfo, 28, 0);
                        Marshal.WriteInt32(bitsInfo, 32, 0);
                        Marshal.WriteInt32(bitsInfo, 36, 0);
                        // black and white color indices
                        Marshal.WriteInt32(bitsInfo, 40, 0);
                        Marshal.WriteByte(bitsInfo, 44, 255);
                        Marshal.WriteByte(bitsInfo, 45, 255);
                        Marshal.WriteByte(bitsInfo, 46, 255);
                        Marshal.WriteByte(bitsInfo, 47, 0);
                        // prepare mask bits
                        bits = Marshal.AllocCoTaskMem(MaskImageSize);
                        Marshal.Copy(_data, MaskImageIndex, bits, MaskImageSize);

                        if (0 ==
                            Gdi32.SetDIBitsToDevice(hdc, 0, 0, (UInt32)Header.biWidth, (UInt32)Header.biHeight/2, 0, 0, 0, (UInt32)Header.biHeight/2, bits,
                                bitsInfo, (UInt32)DIBColors.DIB_RGB_COLORS)) {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        _mask = System.Drawing.Image.FromHbitmap(hBmp);
                    } finally {
                        if (bits != IntPtr.Zero) {
                            Marshal.FreeCoTaskMem(bits);
                        }
                        if (bitsInfo != IntPtr.Zero) {
                            Marshal.FreeCoTaskMem(bitsInfo);
                        }
                        if (hdc != IntPtr.Zero) {
                            Gdi32.SelectObject(hdc, hBmpOld);
                        }
                        if (hdc != IntPtr.Zero) {
                            Gdi32.DeleteObject(hdc);
                        }
                    }
                }

                return _mask;
            }
        }

        /// <summary>
        ///     Bitmap color (XOR) part of the image.
        /// </summary>
        public Bitmap Color
        {
            get
            {
                if (_color == null) {
                    var hdcDesktop = IntPtr.Zero;
                    var hdc = IntPtr.Zero;
                    var hBmp = IntPtr.Zero;
                    var hBmpOld = IntPtr.Zero;
                    var bitsInfo = IntPtr.Zero;
                    var bits = IntPtr.Zero;

                    try {
                        hdcDesktop = User32.GetDC(IntPtr.Zero); // Gdi32.CreateDC("DISPLAY", null, null, IntPtr.Zero);
                        if (hdcDesktop == null) {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        hdc = Gdi32.CreateCompatibleDC(hdcDesktop);
                        if (hdc == null) {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        hBmp = Gdi32.CreateCompatibleBitmap(hdcDesktop, Header.biWidth, Header.biHeight/2);
                        if (hBmp == null) {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        hBmpOld = Gdi32.SelectObject(hdc, hBmp);

                        // bitmap header
                        bitsInfo = Marshal.AllocCoTaskMem(XorImageIndex);
                        if (bitsInfo == null) {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        Marshal.Copy(_data, 0, bitsInfo, XorImageIndex);
                        // fix the height
                        Marshal.WriteInt32(bitsInfo, 8, Header.biHeight/2);
                        // XOR bits
                        bits = Marshal.AllocCoTaskMem(XorImageSize);
                        Marshal.Copy(_data, XorImageIndex, bits, XorImageSize);

                        if (0 ==
                            Gdi32.SetDIBitsToDevice(hdc, 0, 0, (UInt32)Header.biWidth, (UInt32)Header.biHeight/2, 0, 0, 0, (UInt32)(Header.biHeight/2),
                                bits, bitsInfo, (Int32)DIBColors.DIB_RGB_COLORS)) {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        _color = System.Drawing.Image.FromHbitmap(hBmp);
                    } finally {
                        if (hdcDesktop != IntPtr.Zero) {
                            Gdi32.DeleteDC(hdcDesktop);
                        }
                        if (bits != IntPtr.Zero) {
                            Marshal.FreeCoTaskMem(bits);
                        }
                        if (bitsInfo != IntPtr.Zero) {
                            Marshal.FreeCoTaskMem(bitsInfo);
                        }
                        if (hdc != IntPtr.Zero) {
                            Gdi32.SelectObject(hdc, hBmpOld);
                        }
                        if (hdc != IntPtr.Zero) {
                            Gdi32.DeleteObject(hdc);
                        }
                    }
                }

                return _color;
            }
        }

        /// <summary>
        ///     Complete image.
        /// </summary>
        public Bitmap Image
        {
            get
            {
                if (_image == null) {
                    var hDCScreen = IntPtr.Zero;
                    var bits = IntPtr.Zero;
                    var hDCScreenOUTBmp = IntPtr.Zero;
                    var hBitmapOUTBmp = IntPtr.Zero;

                    try {
                        hDCScreen = User32.GetDC(IntPtr.Zero);
                        if (hDCScreen == IntPtr.Zero) {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }

                        // Image
                        var bitmapInfo = new BitmapInfo();
                        bitmapInfo.bmiHeader = Header;
                        // bitmapInfo.bmiColors = Tools.StandarizePalette(mEncoder.Colors);
                        hDCScreenOUTBmp = Gdi32.CreateCompatibleDC(hDCScreen);
                        hBitmapOUTBmp = Gdi32.CreateDIBSection(hDCScreenOUTBmp, ref bitmapInfo, 0, out bits, IntPtr.Zero, 0);
                        Marshal.Copy(_data, XorImageIndex, bits, XorImageSize);
                        _image = System.Drawing.Image.FromHbitmap(hBitmapOUTBmp);
                    } finally {
                        if (hDCScreen != IntPtr.Zero) {
                            User32.ReleaseDC(IntPtr.Zero, hDCScreen);
                        }
                        if (hBitmapOUTBmp != IntPtr.Zero) {
                            Gdi32.DeleteObject(hBitmapOUTBmp);
                        }
                        if (hDCScreenOUTBmp != IntPtr.Zero) {
                            Gdi32.DeleteDC(hDCScreenOUTBmp);
                        }
                    }
                }

                return _image;
            }
        }

        /// <summary>
        ///     Read icon data.
        /// </summary>
        /// <param name="lpData">Pointer to the beginning of icon data.</param>
        /// <param name="size">Icon data size.</param>
        internal void Read(IntPtr lpData, uint size) {
            Header = (BitmapInfoHeader)Marshal.PtrToStructure(lpData, typeof (BitmapInfoHeader));

            _data = new byte[size];
            Marshal.Copy(lpData, _data, 0, _data.Length);
        }

        /// <summary>
        ///     Returns the width of a row in a DIB Bitmap given the number of bits. DIB Bitmap rows always align on a DWORD
        ///     boundary.
        /// </summary>
        /// <param name="width">Number of bits.</param>
        /// <returns>Width of a row in bytes.</returns>
        private Int32 GetDIBRowWidth(int width) {
            return ((width + 31)/32)*4;
        }
    }
}