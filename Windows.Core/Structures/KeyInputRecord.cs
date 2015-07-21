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
    public class KeyInputRecord {
        public Int16 EventType = ConsoleEventTypes.KeyEvent; //this is only one of several possible cases of the INPUT_RECORD structure

        //the rest of fields are from KEY_EVENT_RECORD :
        public bool bKeyDown;
        public Int16 wRepeatCount;
        public Int16 wVirtualKeyCode;
        public Int16 wVirtualScanCode;
        public char UnicodeChar;
        public Int32 dwControlKeyState;
    }
}