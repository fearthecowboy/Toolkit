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

namespace Toolkit.Windows.Flags {
    using System;

    [Flags]
    public enum CreateRemoteThreadFlags {
        None = 0,
        Suspended = 0x00000004,
        StackSizeIsaReservation = 0x00010000,
    }
}