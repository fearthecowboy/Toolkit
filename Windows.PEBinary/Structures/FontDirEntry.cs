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
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Contains information about an individual font in a font resource group.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct FontDirEntry {
        /// <summary>
        ///     Specifies a user-defined version number for the resource data that tools can use to read and write resource files.
        /// </summary>
        public UInt16 dfVersion;

        /// <summary>
        ///     Specifies the size of the file, in bytes.
        /// </summary>
        public UInt32 dfSize;

        /// <summary>
        ///     Contains a 60-character string with the font supplier's copyright information.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
        public string dfCopyright;

        /// <summary>
        ///     Specifies the type of font file.
        /// </summary>
        public UInt16 dfType;

        /// <summary>
        ///     Specifies the point size at which this character set looks best.
        /// </summary>
        public UInt16 dfPoints;

        /// <summary>
        ///     Specifies the vertical resolution, in dots per inch, at which this character set was digitized.
        /// </summary>
        public UInt16 dfVertRes;

        /// <summary>
        ///     Specifies the horizontal resolution, in dots per inch, at which this character set was digitized.
        /// </summary>
        public UInt16 dfHorizRes;

        /// <summary>
        ///     Specifies the distance from the top of a character definition cell to the baseline of the typographical font.
        /// </summary>
        public UInt16 dfAscent;

        /// <summary>
        ///     Specifies the amount of leading inside the bounds set by the dfPixHeight member. Accent marks and other diacritical characters can occur in this area.
        /// </summary>
        public UInt16 dfInternalLeading;

        /// <summary>
        ///     Specifies the amount of extra leading that the application adds between rows.
        /// </summary>
        public UInt16 dfExternalLeading;

        /// <summary>
        ///     Specifies an italic font if not equal to zero.
        /// </summary>
        public byte dfItalic;

        /// <summary>
        ///     Specifies an underlined font if not equal to zero.
        /// </summary>
        public byte dfUnderline;

        /// <summary>
        ///     Specifies a strikeout font if not equal to zero.
        /// </summary>
        public byte dfStrikeOut;

        /// <summary>
        ///     Specifies the weight of the font in the range 0 through 1000. For example, 400 is roman and 700 is bold. If this value is zero, a default weight is used.
        /// </summary>
        public UInt16 dfWeight;

        /// <summary>
        ///     Specifies the character set of the font.
        /// </summary>
        public byte dfCharSet;

        /// <summary>
        ///     Specifies the width of the grid on which a vector font was digitized. For raster fonts, if the member is not equal to zero, it represents the width for all the characters in the bitmap. If the member is equal to zero, the font has variable-width characters.
        /// </summary>
        public UInt16 dfPixWidth;

        /// <summary>
        ///     Specifies the height of the character bitmap for raster fonts or the height of the grid on which a vector font was digitized.
        /// </summary>
        public UInt16 dfPixHeight;

        /// <summary>
        ///     Specifies the pitch and the family of the font.
        /// </summary>
        public byte dfPitchAndFamily;

        /// <summary>
        ///     Specifies the average width of characters in the font (generally defined as the width of the letter x). This value does not include the overhang required for bold or italic characters.
        /// </summary>
        public UInt16 dfAvgWidth;

        /// <summary>
        ///     Specifies the width of the widest character in the font.
        /// </summary>
        public UInt16 dfMaxWidth;

        /// <summary>
        ///     Specifies the first character code defined in the font.
        /// </summary>
        public byte dfFirstChar;

        /// <summary>
        ///     Specifies the last character code defined in the font.
        /// </summary>
        public byte dfLastChar;

        /// <summary>
        ///     Specifies the character to substitute for characters not in the font.
        /// </summary>
        public byte dfDefaultChar;

        /// <summary>
        ///     Specifies the character that will be used to define word breaks for text justification.
        /// </summary>
        public byte dfBreakChar;

        /// <summary>
        ///     Specifies the number of bytes in each row of the bitmap. This value is always even so that the rows start on word boundaries. For vector fonts, this member has no meaning.
        /// </summary>
        public UInt16 dfWidthBytes;

        /// <summary>
        ///     Specifies the offset in the file to a null-terminated string that specifies a device name. For a generic font, this value is zero.
        /// </summary>
        public UInt32 dfDevice;

        /// <summary>
        ///     Specifies the offset in the file to a null-terminated string that names the typeface.
        /// </summary>
        public UInt32 dfFace;

        /// <summary>
        ///     This member is reserved.
        /// </summary>
        public UInt32 dfReserved;
    };
}