//-----------------------------------------------------------------------
// <copyright company="CoApp Project">
//     Copyright (c) 2010-2013 Garrett Serack and CoApp Contributors. 
//     Contributors can be discovered using the 'git log' command.
//     All rights reserved.
// </copyright>
// <license>
//     The software is licensed under the Apache 2.0 License (the "License")
//     You may not use the software except in compliance with the License. 
// </license>
//-----------------------------------------------------------------------

namespace Toolkit.Windows.Structures {
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