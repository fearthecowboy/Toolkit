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

namespace Toolkit.Windows.Enumerations {
    /// <summary>
    ///     Enumeration for Console Event types
    /// </summary>
    public enum ConsoleEvents {
        /// <summary>
        ///     Event type for when Ctrl-C is pressed
        /// </summary>
        CTRL_C_EVENT = 0,

        /// <summary>
        ///     Event type for when Ctrl-Break is pressed
        /// </summary>
        CTRL_BREAK_EVENT = 1,

        /// <summary>
        ///     Event type for when the application is closed
        /// </summary>
        CTRL_CLOSE_EVENT = 2,

        /// <summary>
        ///     Event type for when the user logs off
        /// </summary>
        CTRL_LOGOFF_EVENT = 5,

        /// <summary>
        ///     Event type for when the system shuts down.
        /// </summary>
        CTRL_SHUTDOWN_EVENT = 6
    }
}