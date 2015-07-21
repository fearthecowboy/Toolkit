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

namespace Toolkit.Windows.Enumerations {
    /// <summary>
    ///     Edit control styles. http://msdn.microsoft.com/en-us/library/bb775464(VS.85).aspx
    /// </summary>
    public enum EditControlStyles : uint {
        /// <summary>
        ///     Aligns text with the left margin.
        /// </summary>
        ES_LEFT = 0x0000,

        /// <summary>
        ///     Windows 98/Me, Windows 2000/XP: Centers text in a single-line or multiline edit control. Windows 95, Windows NT 4.0 and earlier: Centers text in a multiline edit control.
        /// </summary>
        ES_CENTER = 0x0001,

        /// <summary>
        ///     Windows 98/Me, Windows 2000/XP: Right-aligns text in a single-line or multiline edit control. Windows 95, Windows NT 4.0 and earlier: Right aligns text in a multiline edit control.
        /// </summary>
        ES_RIGHT = 0x0002,

        /// <summary>
        ///     Designates a multiline edit control. The default is single-line edit control.
        /// </summary>
        ES_MULTILINE = 0x0004,

        /// <summary>
        ///     Converts all characters to uppercase as they are typed into the edit control.
        /// </summary>
        ES_UPPERCASE = 0x0008,

        /// <summary>
        ///     Converts all characters to lowercase as they are typed into the edit control.
        /// </summary>
        ES_LOWERCASE = 0x0010,

        /// <summary>
        ///     Displays an asterisk (*) for each character typed into the edit control. This style is valid only for single-line edit controls.
        /// </summary>
        ES_PASSWORD = 0x0020,

        /// <summary>
        ///     Automatically scrolls text up one page when the user presses the ENTER key on the last line.
        /// </summary>
        ES_AUTOVSCROLL = 0x0040,

        /// <summary>
        ///     Automatically scrolls text to the right by 10 characters when the user types a character at the end of the line. When the user presses the ENTER key, the control scrolls all text back to position zero.
        /// </summary>
        ES_AUTOHSCROLL = 0x0080,

        /// <summary>
        ///     Negates the default behavior for an edit control.
        /// </summary>
        ES_NOHIDESEL = 0x0100,

        /// <summary>
        ///     Converts text entered in the edit control.
        /// </summary>
        ES_OEMCONVERT = 0x0400,

        /// <summary>
        ///     Prevents the user from typing or editing text in the edit control.
        /// </summary>
        ES_READONLY = 0x0800,

        /// <summary>
        ///     Specifies that a carriage return be inserted when the user presses the ENTER key while entering text into a multiline edit control in a dialog box. If you do not specify this style, pressing the ENTER key has the same effect as pressing the dialog box's default push button. This style has no effect on a single-line edit control.
        /// </summary>
        ES_WANTRETURN = 0x1000,

        /// <summary>
        ///     Allows only digits to be entered into the edit control.
        /// </summary>
        ES_NUMBER = 0x2000,
    }
}