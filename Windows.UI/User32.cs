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

namespace FearTheCowboy.Windows {
    using System;
    using System.Runtime.InteropServices;
    using Structures;

    /// <summary>
    ///     Summary description for Win32.
    /// </summary>
    public static class User32 {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32.dll")]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        ///     Sends the specified message to a window or windows. The function calls the window procedure for the specified
        ///     window and does not return until the window procedure has processed the message.
        /// </summary>
        /// <param name="hWnd"> Handle to the window whose window procedure will receive the message. </param>
        /// <param name="Msg"> Specifies the message to be sent. </param>
        /// <param name="wParam"> Specifies additional message-specific information. </param>
        /// <param name="lParam"> Specifies additional message-specific information. </param>
        /// <returns> </returns>
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool IsWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hwnd, Int32 x, Int32 y, Int32 h, Int32 w, bool repaint);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr child, IntPtr newParent);

        [DllImport("user32.dll")]
        public static extern bool GetWindowInfo(IntPtr hwnd, out WindowInfo wi);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hwnd, Int32 msg, Int32 wparam, Int32 lparam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, Int32 msg, Int32 wparam, Int32 lparam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(Int32 hwnd, Int32 msg, Int32 wparam, Int32 lparam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(Int32 hwnd, Int32 msg, Int32 wparam, [MarshalAs(UnmanagedType.LPStr)] string lparam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageTimeout(Int32 hwnd, Int32 msg, Int32 wparam, [MarshalAs(UnmanagedType.LPStr)] string lparam, Int32 fuFlags,
            Int32 timeout, IntPtr result);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern bool RemoveMenu(IntPtr hMenu, Int32 nPosition, Int32 nFlags);

        [DllImport("user32.dll")]
        public static extern Int32 GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetMenuItemInfo(IntPtr hMenu, Int32 item, bool bByPosition, [MarshalAs(UnmanagedType.LPStruct)] [In] [Out] MenuItemInfo mii);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, Int32 X, Int32 Y, Int32 cx, Int32 cy, Int32 uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        /// <summary>
        ///     Retrieve a handle to a device context (DC) for the client area of a specified window or for the entire screen.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose DC is to be retrieved. If this value is NULL, GetDC retrieves the DC
        ///     for the entire screen.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is a handle to the DC for the specified window's client area. If
        ///     the function fails, the return value is NULL.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        ///     Releases a device context (DC), freeing it for use by other applications.
        /// </summary>
        /// <param name="hWnd"> A handle to the window whose DC is to be released. </param>
        /// <param name="hDC"> A handle to the DC to be released. </param>
        /// <returns>
        ///     The return value indicates whether the DC was released. If the DC was released, the return value is 1. If the
        ///     DC was not released, the return value is zero.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        ///     Destroys an icon and frees any memory the icon occupied.
        /// </summary>
        /// <param name="hIcon"> Handle to the icon to be destroyed. </param>
        /// <returns> If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int DestroyIcon(IntPtr hIcon);

        /// <summary>
        ///     Creates an icon or cursor from an ICONINFO structure.
        /// </summary>
        /// <param name="piconInfo"> Pointer to an ICONINFO structure the function uses to create the icon or cursor. </param>
        /// <returns>
        ///     If the function succeeds, the return value is a handle to the icon or cursor that is created. If the function
        ///     fails, the return value is NULL.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CreateIconIndirect(ref Iconinfo piconInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
    }
}