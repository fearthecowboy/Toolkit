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
    ///     Push button styles. http://msdn.microsoft.com/en-us/library/bb775951(VS.85).aspx
    /// </summary>
    public enum ButtonControlStyles : uint {
        /// <summary>
        ///     Creates a push button that posts a WM_COMMAND message to the owner window when the user selects the button.
        /// </summary>
        BS_PUSHBUTTON = 0x00000000,

        /// <summary>
        ///     Creates a push button that behaves like a BS_PUSHBUTTON style button, but has a distinct appearance. If the button is in a dialog box, the user can select the button by pressing the ENTER key, even when the button does not have the input focus. This style is useful for enabling the user to quickly select the most likely (default) option.
        /// </summary>
        BS_DEFPUSHBUTTON = 0x00000001,

        /// <summary>
        ///     Creates a small, empty check box with text. By default, the text is displayed to the right of the check box. To display the text to the left of the check box, combine this flag with the BS_LEFTTEXT style (or with the equivalent BS_RIGHTBUTTON style).
        /// </summary>
        BS_CHECKBOX = 0x00000002,

        /// <summary>
        ///     Creates a button that is the same as a check box, except that the check state automatically toggles between checked and cleared each time the user selects the check box.
        /// </summary>
        BS_AUTOCHECKBOX = 0x00000003,

        /// <summary>
        ///     Creates a small circle with text. By default, the text is displayed to the right of the circle. To display the text to the left of the circle, combine this flag with the BS_LEFTTEXT style (or with the equivalent BS_RIGHTBUTTON style). Use radio buttons for groups of related, but mutually exclusive choices.
        /// </summary>
        BS_RADIOBUTTON = 0x00000004,

        /// <summary>
        ///     Creates a button that is the same as a check box, except that the box can be grayed as well as checked or cleared. Use the grayed state to show that the state of the check box is not determined.
        /// </summary>
        BS_3STATE = 0x00000005,

        /// <summary>
        ///     Creates a button that is the same as a three-state check box, except that the box changes its state when the user selects it. The state cycles through checked, indeterminate, and cleared.
        /// </summary>
        BS_AUTO3STATE = 0x00000006,

        /// <summary>
        ///     Creates a rectangle in which other controls can be grouped. Any text associated with this style is displayed in the rectangle's upper left corner.
        /// </summary>
        BS_GROUPBOX = 0x00000007,

        /// <summary>
        ///     Obsolete, but provided for compatibility with 16-bit versions of Windows. Applications should use BS_OWNERDRAW instead.
        /// </summary>
        BS_USERBUTTON = 0x00000008,

        /// <summary>
        ///     Creates a button that is the same as a radio button, except that when the user selects it, the system automatically sets the button's check state to checked and automatically sets the check state for all other buttons in the same group to cleared.
        /// </summary>
        BS_AUTORADIOBUTTON = 0x00000009,

        /// <summary>
        /// </summary>
        BS_PUSHBOX = 0x0000000A,

        /// <summary>
        ///     Creates an owner-drawn button. The owner window receives a WM_DRAWITEM message when a visual aspect of the button has changed. Do not combine the BS_OWNERDRAW style with any other button styles.
        /// </summary>
        BS_OWNERDRAW = 0x0000000B,

        /// <summary>
        ///     Microsoft Windows 2000: A composite style bit that results from using the OR operator on BS_* style bits. It can be used to mask out valid BS_* bits from a given bitmask.
        /// </summary>
        BS_TYPEMASK = 0x0000000F,

        /// <summary>
        ///     Places text on the left side of the radio button or check box when combined with a radio button or check box style. Same as the BS_RIGHTBUTTON style.
        /// </summary>
        BS_LEFTTEXT = 0x00000020,

        /// <summary>
        ///     Specifies that the button displays text.
        /// </summary>
        BS_TEXT = 0x00000000,

        /// <summary>
        ///     Specifies that the button displays an icon.
        /// </summary>
        BS_ICON = 0x00000040,

        /// <summary>
        ///     Specifies that the button displays a bitmap. See the Remarks section for its interaction with BS_ICON.
        /// </summary>
        BS_BITMAP = 0x00000080,

        /// <summary>
        ///     Left-justifies the text in the button rectangle. However, if the button is a check box or radio button that does not have the BS_RIGHTBUTTON style, the text is left justified on the right side of the check box or radio button.
        /// </summary>
        BS_LEFT = 0x00000100,

        /// <summary>
        ///     Right-justifies text in the button rectangle. However, if the button is a check box or radio button that does not have the BS_RIGHTBUTTON style, the text is right justified on the right side of the check box or radio button.
        /// </summary>
        BS_RIGHT = 0x00000200,

        /// <summary>
        ///     Centers text horizontally in the button rectangle.
        /// </summary>
        BS_CENTER = 0x00000300,

        /// <summary>
        ///     Places text at the top of the button rectangle.
        /// </summary>
        BS_TOP = 0x00000400,

        /// <summary>
        ///     Places text at the bottom of the button rectangle.
        /// </summary>
        BS_BOTTOM = 0x00000800,

        /// <summary>
        ///     Places text in the middle (vertically) of the button rectangle.
        /// </summary>
        BS_VCENTER = 0x00000C00,

        /// <summary>
        ///     Makes a button (such as a check box, three-state check box, or radio button) look and act like a push button. The button looks raised when it isn't pushed or checked, and sunken when it is pushed or checked.
        /// </summary>
        BS_PUSHLIKE = 0x00001000,

        /// <summary>
        ///     Wraps the button text to multiple lines if the text string is too long to fit on a single line in the button rectangle.
        /// </summary>
        BS_MULTILINE = 0x00002000,

        /// <summary>
        ///     Enables a button to send BN_KILLFOCUS and BN_SETFOCUS notification messages to its parent window.
        /// </summary>
        BS_NOTIFY = 0x00004000,

        /// <summary>
        ///     Specifies that the button is two-dimensional; it does not use the default shading to create a 3-D image.
        /// </summary>
        BS_FLAT = 0x00008000,

        /// <summary>
        ///     Microsoft Windows Vista and Version 6.00. Creates a split button that behaves like a BS_PUSHBUTTON style button, but also has a distinctive appearance. If the split button is in a dialog box, the user can select the split button by pressing the ENTER key, even when the split button does not have the input focus. This style is useful for enabling the user to quickly select the most likely (default) option.
        /// </summary>
        BS_DEFSPLITBUTTON = 0x0000000D,

        /// <summary>
        ///     Microsoft Windows Vista and Version 6.00. Creates a command link button that behaves like a BS_PUSHBUTTON style button, but the command link button has a green arrow on the left pointing to the button text. A caption for the button text can be set by sending the BCM_SETNOTE message to the button.
        /// </summary>
        BS_COMMANDLINK = 0x0000000E,

        /// <summary>
        ///     Microsoft Windows Vista and Version 6.00. Creates a command link button that behaves like a BS_PUSHBUTTON style button. If the button is in a dialog box, the user can select the command link button by pressing the ENTER key, even when the command link button does not have the input focus. This style is useful for enabling the user to quickly select the most likely (default) option.
        /// </summary>
        BS_DEFCOMMANDLINK = 0x0000000F,
    }
}