﻿// 
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
    using System;
    using System.Runtime.InteropServices;
    using Enumerations;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class MenuItemInfo {
        public readonly Int32 cbSize = Marshal.SizeOf(typeof (MenuItemInfo));
        public Miim fMask;
        public Int32 fType;
        public Int32 fState;
        public Int32 wID;
        public IntPtr hSubMenu;
        public IntPtr hbmpChecked;
        public IntPtr hbmpUnchecked;
        public IntPtr dwItemData;

        [MarshalAs(UnmanagedType.LPWStr, SizeConst = 255)]
        public String dwTypeData = new String(' ', 256);

        public readonly Int32 cch = 255;
        public IntPtr hbmpItem;
    }
}