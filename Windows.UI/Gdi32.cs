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

namespace Toolkit.Windows {
    using System;
    using System.Runtime.InteropServices;
    using Structures;

    /// <summary>
    ///     Gdi32.dll interop functions.
    /// </summary>
    public class Gdi32 {
        /// <summary>
        ///     Set the pixels in the specified rectangle on the device that is associated with the destination device context using color data from a DIB, JPEG, or PNG image. http://msdn.microsoft.com/en-us/library/dd162974(VS.85).aspx
        /// </summary>
        /// <param name="hdc"> A handle to the device context. </param>
        /// <param name="XDest"> The x-coordinate, in logical units, of the upper-left corner of the destination rectangle. </param>
        /// <param name="YDest"> The y-coordinate, in logical units, of the upper-left corner of the destination rectangle. </param>
        /// <param name="dwWidth"> The width, in logical units, of the image. </param>
        /// <param name="dwHeight"> The height, in logical units, of the image. </param>
        /// <param name="XSrc"> The x-coordinate, in logical units, of the lower-left corner of the image. </param>
        /// <param name="YSrc"> The y-coordinate, in logical units, of the lower-left corner of the image. </param>
        /// <param name="uStartScan"> The starting scan line in the image. </param>
        /// <param name="cScanLines"> The number of DIB scan lines contained in the array pointed to by the lpvBits parameter. </param>
        /// <param name="lpvBits"> A pointer to the color data stored as an array of bytes. </param>
        /// <param name="lpbmi"> A pointer to a BITMAPINFOHEADER structure that contains information about the DIB. </param>
        /// <param name="fuColorUse"> Indicates whether the bmiColors member of the BITMAPINFOHEADER structure contains explicit red, green, blue (RGB) values or indexes into a palette. </param>
        /// <returns> If the function succeeds, the return value is the number of scan lines set. If zero scan lines are set (such as when dwHeight is 0) or the function fails, the function returns zero. If the driver cannot support the JPEG or PNG file image passed to SetDIBitsToDevice, the function will fail and return GDI_ERROR. </returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern int SetDIBitsToDevice(IntPtr hdc, Int32 XDest, Int32 YDest, UInt32 dwWidth, UInt32 dwHeight, Int32 XSrc, Int32 YSrc,
            UInt32 uStartScan, UInt32 cScanLines, byte[] lpvBits, [In] ref BitmapInfo lpbmi, UInt32 fuColorUse);

        /// <summary>
        ///     Set the pixels in the specified rectangle on the device that is associated with the destination device context using color data from a DIB, JPEG, or PNG image. http://msdn.microsoft.com/en-us/library/dd162974(VS.85).aspx
        /// </summary>
        /// <param name="hdc"> A handle to the device context. </param>
        /// <param name="XDest"> The x-coordinate, in logical units, of the upper-left corner of the destination rectangle. </param>
        /// <param name="YDest"> The y-coordinate, in logical units, of the upper-left corner of the destination rectangle. </param>
        /// <param name="dwWidth"> The width, in logical units, of the image. </param>
        /// <param name="dwHeight"> The height, in logical units, of the image. </param>
        /// <param name="XSrc"> The x-coordinate, in logical units, of the lower-left corner of the image. </param>
        /// <param name="YSrc"> The y-coordinate, in logical units, of the lower-left corner of the image. </param>
        /// <param name="uStartScan"> The starting scan line in the image. </param>
        /// <param name="cScanLines"> The number of DIB scan lines contained in the array pointed to by the lpvBits parameter. </param>
        /// <param name="lpvBits"> A pointer to the color data stored as an array of bytes. </param>
        /// <param name="lpbmi"> A pointer to a BITMAPINFOHEADER structure that contains information about the DIB. </param>
        /// <param name="fuColorUse"> Indicates whether the bmiColors member of the BITMAPINFOHEADER structure contains explicit red, green, blue (RGB) values or indexes into a palette. </param>
        /// <returns> If the function succeeds, the return value is the number of scan lines set. If zero scan lines are set (such as when dwHeight is 0) or the function fails, the function returns zero. If the driver cannot support the JPEG or PNG file image passed to SetDIBitsToDevice, the function will fail and return GDI_ERROR. </returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern int SetDIBitsToDevice(IntPtr hdc, Int32 XDest, Int32 YDest, UInt32 dwWidth, UInt32 dwHeight, Int32 XSrc, Int32 YSrc,
            UInt32 uStartScan, UInt32 cScanLines, IntPtr lpvBits, IntPtr lpbmi, UInt32 fuColorUse);

        /// <summary>
        ///     Retrieves the bits of the specified compatible bitmap and copies them into a buffer as a DIB using the specified format
        /// </summary>
        /// <param name="hdc"> A handle to the device context. </param>
        /// <param name="hbmp"> A handle to the bitmap. This must be a compatible bitmap (DDB). </param>
        /// <param name="uStartScan"> The first scan line to retrieve. </param>
        /// <param name="cScanLines"> The number of scan lines to retrieve. </param>
        /// <param name="lpvBits"> A pointer to a buffer to receive the bitmap data. </param>
        /// <param name="lpbmi"> A pointer to a BITMAPINFO structure that specifies the desired format for the DIB data. </param>
        /// <param name="uUsage"> The format of the bmiColors member of the BITMAPINFO structure. </param>
        /// <returns> If the lpvBits parameter is non-NULL and the function succeeds, the return value is the number of scan lines copied from the bitmap. If the lpvBits parameter is NULL and GetDIBits successfully fills the BITMAPINFO structure, the return value is non-zero. If the function fails, the return value is zero. </returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern int GetDIBits(IntPtr hdc, IntPtr hbmp, uint uStartScan, uint cScanLines, [Out] byte[] lpvBits, [In] ref BitmapInfo lpbmi,
            uint uUsage);

        /// <summary>
        ///     Create a DIB that applications can write to directly. The function gives you a pointer to the location of the bitmap bit values. You can supply a handle to a file-mapping object that the function will use to create the bitmap, or you can let the system allocate the memory for the bitmap.
        /// </summary>
        /// <param name="hdc"> Handle to a device context. </param>
        /// <param name="pbmi"> A pointer to a BITMAPINFO structure that specifies various attributes of the DIB, including the bitmap dimensions and colors. </param>
        /// <param name="iUsage"> The type of data contained in the bmiColors array member of the BITMAPINFO structure pointed to by pbmi (either logical palette indexes or literal RGB values). </param>
        /// <param name="ppvBits"> A pointer to a variable that receives a pointer to the location of the DIB bit values. </param>
        /// <param name="hSection"> A handle to a file-mapping object that the function will use to create the DIB. This parameter can be NULL. </param>
        /// <param name="dwOffset"> The offset from the beginning of the file-mapping object referenced by hSection where storage for the bitmap bit values is to begin. </param>
        /// <returns> If the function succeeds, the return value is a handle to the newly created DIB, and *ppvBits points to the bitmap bit values. If the function fails, the return value is NULL, and *ppvBits is NULL. </returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BitmapInfo pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        /// <summary>
        ///     Creates a memory device context (DC) compatible with the specified device.
        /// </summary>
        /// <param name="hdc"> Handle to an existing device context. </param>
        /// <returns> The handle to a memory device context indicates success. NULL indicates failure. </returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        /// <summary>
        /// </summary>
        /// <param name="lpDriverName"> Specifies either DISPLAY or the name of a specific display device or the name of a print provider, which is usually WINSPOOL. </param>
        /// <param name="lpDeviceName"> Specifies the name of the specific output device being used, as shown by the Print Manager (for example, Epson FX-80). </param>
        /// <param name="lpOutput"> This parameter is ignored and should be set to NULL. It is provided only for compatibility with 16-bit Windows. </param>
        /// <param name="lpInitData"> A pointer to a DEVMODE structure containing device-specific initialization data for the device driver. </param>
        /// <returns> </returns>
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateDC(string lpDriverName, string lpDeviceName, string lpOutput, IntPtr lpInitData);

        /// <summary>
        ///     Creates a bitmap compatible with the device that is associated with the specified device context.
        /// </summary>
        /// <param name="hdc"> A handle to a device context. </param>
        /// <param name="nWidth"> The bitmap width, in pixels. </param>
        /// <param name="nHeight"> The bitmap height, in pixels. </param>
        /// <returns> If the function succeeds, the return value is a handle to the compatible bitmap (DDB). If the function fails, the return value is NULL. </returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, Int32 nWidth, Int32 nHeight);

        /// <summary>
        ///     Selects an object into a specified device context. The new object replaces the previous object of the same type.
        /// </summary>
        /// <param name="hdc"> Handle to the device context. </param>
        /// <param name="hgdiobj"> Handle to the object to be selected. </param>
        /// <returns> If the selected object is not a region, the handle of the object being replaced indicates success. If the selected object is a region, one of the following values indicates success. </returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        /// <summary>
        ///     Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object.
        /// </summary>
        /// <param name="hObject"> Handle to a logical pen, brush, font, bitmap, region, or palette. </param>
        /// <returns> Nonzero indicates success. Zero indicates that the specified handle is not valid or that the handle is currently selected into a device context. </returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern int DeleteObject(IntPtr hObject);

        /// <summary>
        ///     Deletes the specified device context.
        /// </summary>
        /// <param name="hdc"> A handle to the device context. </param>
        /// <returns> If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. </returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern int DeleteDC(IntPtr hdc);
    }
}