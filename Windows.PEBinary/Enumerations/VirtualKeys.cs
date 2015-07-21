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
    ///     Virtual key definitions.
    /// </summary>
    public enum VirtualKeys : uint {
        /// <summary>
        ///     Standard virtual left mouse button.
        /// </summary>
        VK_LBUTTON = 0x01,

        /// <summary>
        ///     Standard virtual right mouse button.
        /// </summary>
        VK_RBUTTON = 0x02,

        /// <summary>
        ///     Ctrl-Break / Ctrl-C.
        /// </summary>
        VK_CANCEL = 0x03,

        /// <summary>
        ///     Standard virtual middle mouse button.
        /// </summary>
        VK_MBUTTON = 0x04,

        /// <summary>
        /// </summary>
        VK_XBUTTON1 = 0x05,

        /// <summary>
        /// </summary>
        VK_XBUTTON2 = 0x06,

        /// <summary>
        ///     Backspace.
        /// </summary>
        VK_BACK = 0x08,

        /// <summary>
        ///     Tab.
        /// </summary>
        VK_TAB = 0x09,

        /// <summary>
        ///     Delete.
        /// </summary>
        VK_CLEAR = 0x0C,

        /// <summary>
        ///     Return.
        /// </summary>
        VK_RETURN = 0x0D,

        /// <summary>
        ///     Shift.
        /// </summary>
        VK_SHIFT = 0x10,

        /// <summary>
        ///     Control.
        /// </summary>
        VK_CONTROL = 0x11,

        /// <summary>
        ///     Menu.
        /// </summary>
        VK_MENU = 0x12,

        /// <summary>
        ///     Pause.
        /// </summary>
        VK_PAUSE = 0x13,

        /// <summary>
        ///     Caps lock.
        /// </summary>
        VK_CAPITAL = 0x14,

        /// <summary>
        /// </summary>
        VK_KANA = 0x15,

        /// <summary>
        /// </summary>
        VK_JUNJA = 0x17,

        /// <summary>
        /// </summary>
        VK_FINAL = 0x18,

        /// <summary>
        /// </summary>
        VK_KANJI = 0x19,

        /// <summary>
        ///     Escape.
        /// </summary>
        VK_ESCAPE = 0x1B,

        /// <summary>
        /// </summary>
        VK_CONVERT = 0x1C,

        /// <summary>
        /// </summary>
        VK_NONCONVERT = 0x1D,

        /// <summary>
        /// </summary>
        VK_ACCEPT = 0x1E,

        /// <summary>
        /// </summary>
        VK_MODECHANGE = 0x1F,

        /// <summary>
        ///     Space.
        /// </summary>
        VK_SPACE = 0x20,

        /// <summary>
        /// </summary>
        VK_PRIOR = 0x21,

        /// <summary>
        /// </summary>
        VK_NEXT = 0x22,

        /// <summary>
        ///     End.
        /// </summary>
        VK_END = 0x23,

        /// <summary>
        ///     Home.
        /// </summary>
        VK_HOME = 0x24,

        /// <summary>
        ///     Left arrow.
        /// </summary>
        VK_LEFT = 0x25,

        /// <summary>
        ///     Up arrow.
        /// </summary>
        VK_UP = 0x26,

        /// <summary>
        ///     Right arrow.
        /// </summary>
        VK_RIGHT = 0x27,

        /// <summary>
        ///     Down arrow.
        /// </summary>
        VK_DOWN = 0x28,

        /// <summary>
        /// </summary>
        VK_SELECT = 0x29,

        /// <summary>
        ///     Print Screen.
        /// </summary>
        VK_PRINT = 0x2A,

        /// <summary>
        /// </summary>
        VK_EXECUTE = 0x2B,

        /// <summary>
        /// </summary>
        VK_SNAPSHOT = 0x2C,

        /// <summary>
        ///     Insert.
        /// </summary>
        VK_INSERT = 0x2D,

        /// <summary>
        ///     Delete.
        /// </summary>
        VK_DELETE = 0x2E,

        /// <summary>
        /// </summary>
        VK_HELP = 0x2F,

        /// <summary>
        /// </summary>
        VK_LWIN = 0x5B,

        /// <summary>
        /// </summary>
        VK_RWIN = 0x5C,

        /// <summary>
        /// </summary>
        VK_APPS = 0x5D,

        /// <summary>
        /// </summary>
        VK_SLEEP = 0x5F,

        /// <summary>
        /// </summary>
        VK_NUMPAD0 = 0x60,

        /// <summary>
        /// </summary>
        VK_NUMPAD1 = 0x61,

        /// <summary>
        /// </summary>
        VK_NUMPAD2 = 0x62,

        /// <summary>
        /// </summary>
        VK_NUMPAD3 = 0x63,

        /// <summary>
        /// </summary>
        VK_NUMPAD4 = 0x64,

        /// <summary>
        /// </summary>
        VK_NUMPAD5 = 0x65,

        /// <summary>
        /// </summary>
        VK_NUMPAD6 = 0x66,

        /// <summary>
        /// </summary>
        VK_NUMPAD7 = 0x67,

        /// <summary>
        /// </summary>
        VK_NUMPAD8 = 0x68,

        /// <summary>
        /// </summary>
        VK_NUMPAD9 = 0x69,

        /// <summary>
        /// </summary>
        VK_MULTIPLY = 0x6A,

        /// <summary>
        /// </summary>
        VK_ADD = 0x6B,

        /// <summary>
        /// </summary>
        VK_SEPARATOR = 0x6C,

        /// <summary>
        /// </summary>
        VK_SUBTRACT = 0x6D,

        /// <summary>
        /// </summary>
        VK_DECIMAL = 0x6E,

        /// <summary>
        /// </summary>
        VK_DIVIDE = 0x6F,

        /// <summary>
        /// </summary>
        VK_F1 = 0x70,

        /// <summary>
        /// </summary>
        VK_F2 = 0x71,

        /// <summary>
        /// </summary>
        VK_F3 = 0x72,

        /// <summary>
        /// </summary>
        VK_F4 = 0x73,

        /// <summary>
        /// </summary>
        VK_F5 = 0x74,

        /// <summary>
        /// </summary>
        VK_F6 = 0x75,

        /// <summary>
        /// </summary>
        VK_F7 = 0x76,

        /// <summary>
        /// </summary>
        VK_F8 = 0x77,

        /// <summary>
        /// </summary>
        VK_F9 = 0x78,

        /// <summary>
        /// </summary>
        VK_F10 = 0x79,

        /// <summary>
        /// </summary>
        VK_F11 = 0x7A,

        /// <summary>
        /// </summary>
        VK_F12 = 0x7B,

        /// <summary>
        /// </summary>
        VK_F13 = 0x7C,

        /// <summary>
        /// </summary>
        VK_F14 = 0x7D,

        /// <summary>
        /// </summary>
        VK_F15 = 0x7E,

        /// <summary>
        /// </summary>
        VK_F16 = 0x7F,

        /// <summary>
        /// </summary>
        VK_F17 = 0x80,

        /// <summary>
        /// </summary>
        VK_F18 = 0x81,

        /// <summary>
        /// </summary>
        VK_F19 = 0x82,

        /// <summary>
        /// </summary>
        VK_F20 = 0x83,

        /// <summary>
        /// </summary>
        VK_F21 = 0x84,

        /// <summary>
        /// </summary>
        VK_F22 = 0x85,

        /// <summary>
        /// </summary>
        VK_F23 = 0x86,

        /// <summary>
        /// </summary>
        VK_F24 = 0x87,

        /// <summary>
        /// </summary>
        VK_NUMLOCK = 0x90,

        /// <summary>
        /// </summary>
        VK_SCROLL = 0x91,

        /// <summary>
        ///     NEC PC-9800 keyboard '=' key on numpad.
        /// </summary>
        VK_OEM_NEC_EQUAL = 0x92,

        /// <summary>
        ///     Fujitsu/OASYS keyboard 'Dictionary' key.
        /// </summary>
        VK_OEM_FJ_JISHO = 0x92,

        /// <summary>
        ///     Fujitsu/OASYS keyboard 'Unregister word' key.
        /// </summary>
        VK_OEM_FJ_MASSHOU = 0x93,

        /// <summary>
        ///     Fujitsu/OASYS keyboard 'Register word' key.
        /// </summary>
        VK_OEM_FJ_TOUROKU = 0x94,

        /// <summary>
        ///     Fujitsu/OASYS keyboard 'Left OYAYUBI' key.
        /// </summary>
        VK_OEM_FJ_LOYA = 0x95,

        /// <summary>
        ///     Fujitsu/OASYS keyboard 'Right OYAYUBI' key.
        /// </summary>
        VK_OEM_FJ_ROYA = 0x96,

        /// <summary>
        /// </summary>
        VK_LSHIFT = 0xA0,

        /// <summary>
        /// </summary>
        VK_RSHIFT = 0xA1,

        /// <summary>
        /// </summary>
        VK_LCONTROL = 0xA2,

        /// <summary>
        /// </summary>
        VK_RCONTROL = 0xA3,

        /// <summary>
        /// </summary>
        VK_LMENU = 0xA4,

        /// <summary>
        /// </summary>
        VK_RMENU = 0xA5,

        /// <summary>
        /// </summary>
        VK_BROWSER_BACK = 0xA6,

        /// <summary>
        /// </summary>
        VK_BROWSER_FORWARD = 0xA7,

        /// <summary>
        /// </summary>
        VK_BROWSER_REFRESH = 0xA8,

        /// <summary>
        /// </summary>
        VK_BROWSER_STOP = 0xA9,

        /// <summary>
        /// </summary>
        VK_BROWSER_SEARCH = 0xAA,

        /// <summary>
        /// </summary>
        VK_BROWSER_FAVORITES = 0xAB,

        /// <summary>
        /// </summary>
        VK_BROWSER_HOME = 0xAC,

        /// <summary>
        /// </summary>
        VK_VOLUME_MUTE = 0xAD,

        /// <summary>
        /// </summary>
        VK_VOLUME_DOWN = 0xAE,

        /// <summary>
        /// </summary>
        VK_VOLUME_UP = 0xAF,

        /// <summary>
        /// </summary>
        VK_MEDIA_NEXT_TRACK = 0xB0,

        /// <summary>
        /// </summary>
        VK_MEDIA_PREV_TRACK = 0xB1,

        /// <summary>
        /// </summary>
        VK_MEDIA_STOP = 0xB2,

        /// <summary>
        /// </summary>
        VK_MEDIA_PLAY_PAUSE = 0xB3,

        /// <summary>
        /// </summary>
        VK_LAUNCH_MAIL = 0xB4,

        /// <summary>
        /// </summary>
        VK_LAUNCH_MEDIA_SELECT = 0xB5,

        /// <summary>
        /// </summary>
        VK_LAUNCH_APP1 = 0xB6,

        /// <summary>
        /// </summary>
        VK_LAUNCH_APP2 = 0xB7,

        /// <summary>
        ///     ';:' for US
        /// </summary>
        VK_OEM_1 = 0xBA,

        /// <summary>
        ///     '+' any country
        /// </summary>
        VK_OEM_PLUS = 0xBB,

        /// <summary>
        ///     ',' any country
        /// </summary>
        VK_OEM_COMMA = 0xBC,

        /// <summary>
        ///     '-' any country
        /// </summary>
        VK_OEM_MINUS = 0xBD,

        /// <summary>
        ///     '.' any country
        /// </summary>
        VK_OEM_PERIOD = 0xBE,

        /// <summary>
        ///     '/?' for US
        /// </summary>
        VK_OEM_2 = 0xBF,

        /// <summary>
        ///     '`~' for US
        /// </summary>
        VK_OEM_3 = 0xC0,

        /// <summary>
        ///     '[{' for US
        /// </summary>
        VK_OEM_4 = 0xDB,

        /// <summary>
        ///     '\|' for US
        /// </summary>
        VK_OEM_5 = 0xDC,

        /// <summary>
        ///     ']}' for US
        /// </summary>
        VK_OEM_6 = 0xDD,

        /// <summary>
        ///     ''"' for US
        /// </summary>
        VK_OEM_7 = 0xDE,

        /// <summary>
        /// </summary>
        VK_OEM_8 = 0xDF,

        /// <summary>
        ///     'AX' key on Japanese AX kbd
        /// </summary>
        VK_OEM_AX = 0xE1,

        /// <summary>
        ///     "&lt;&gt;" or "\|" on RT 102-key kbd.
        /// </summary>
        VK_OEM_102 = 0xE2,

        /// <summary>
        ///     Help key on ICO
        /// </summary>
        VK_ICO_HELP = 0xE3,

        /// <summary>
        ///     00 key on ICO
        /// </summary>
        VK_ICO_00 = 0xE4,

        /// <summary>
        /// </summary>
        VK_PROCESSKEY = 0xE5,

        /// <summary>
        /// </summary>
        VK_ICO_CLEAR = 0xE6,

        /// <summary>
        /// </summary>
        VK_PACKET = 0xE7,

        /// <summary>
        /// </summary>
        VK_OEM_RESET = 0xE9,

        /// <summary>
        /// </summary>
        VK_OEM_JUMP = 0xEA,

        /// <summary>
        /// </summary>
        VK_OEM_PA1 = 0xEB,

        /// <summary>
        /// </summary>
        VK_OEM_PA2 = 0xEC,

        /// <summary>
        /// </summary>
        VK_OEM_PA3 = 0xED,

        /// <summary>
        /// </summary>
        VK_OEM_WSCTRL = 0xEE,

        /// <summary>
        /// </summary>
        VK_OEM_CUSEL = 0xEF,

        /// <summary>
        /// </summary>
        VK_OEM_ATTN = 0xF0,

        /// <summary>
        /// </summary>
        VK_OEM_FINISH = 0xF1,

        /// <summary>
        /// </summary>
        VK_OEM_COPY = 0xF2,

        /// <summary>
        /// </summary>
        VK_OEM_AUTO = 0xF3,

        /// <summary>
        /// </summary>
        VK_OEM_ENLW = 0xF4,

        /// <summary>
        /// </summary>
        VK_OEM_BACKTAB = 0xF5,

        /// <summary>
        /// </summary>
        VK_ATTN = 0xF6,

        /// <summary>
        /// </summary>
        VK_CRSEL = 0xF7,

        /// <summary>
        /// </summary>
        VK_EXSEL = 0xF8,

        /// <summary>
        /// </summary>
        VK_EREOF = 0xF9,

        /// <summary>
        /// </summary>
        VK_PLAY = 0xFA,

        /// <summary>
        /// </summary>
        VK_ZOOM = 0xFB,

        /// <summary>
        /// </summary>
        VK_NONAME = 0xFC,

        /// <summary>
        /// </summary>
        VK_PA1 = 0xFD,

        /// <summary>
        /// </summary>
        VK_OEM_CLEAR = 0xFE
    }
}