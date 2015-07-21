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
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    ///     SafeHandleBase implements ReleaseHandle method for all our Safe Handle classes. The purpose of the safe handle class is to get away from having IntPtr objects for handles coming back from Kernel APIs, and instead provide a type-safe wrapper that prohibits the accidental use of one handle type where another should be. We create a common base class so that the release semantics are implemented the same.
    /// </summary>
    public class AutoSafeHandle : SafeHandleZeroOrMinusOneIsInvalid {
        protected AutoSafeHandle() : base(true) {
        }

        protected AutoSafeHandle(IntPtr handle) : base(true) {
            SetHandle(handle);
        }

        /// <summary>
        ///     When overridden in a derived class, executes the code required to free the handle.
        /// </summary>
        /// <returns> true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant. </returns>
        protected override bool ReleaseHandle() {
            return Kernel32.CloseHandle(handle);
        }
    }

    /// <summary>
    ///     Represents a wrapper class for a token handle.
    /// </summary>
    public class SafeTokenHandle : AutoSafeHandle {
        public static SafeTokenHandle InvalidHandle = new SafeTokenHandle(IntPtr.Zero);

        public SafeTokenHandle() {
        }

        public SafeTokenHandle(IntPtr handle) : base(handle) {
        }
    }

    public sealed class SafeProcessHandle : AutoSafeHandle {
        public static SafeProcessHandle InvalidHandle = new SafeProcessHandle(IntPtr.Zero);

        public SafeProcessHandle() {
        }

        internal SafeProcessHandle(IntPtr handle) : base(handle) {
        }
    }

    public sealed class SafeThreadHandle : AutoSafeHandle {
        internal static SafeThreadHandle InvalidHandle = new SafeThreadHandle(IntPtr.Zero);

        public SafeThreadHandle() {
        }

        internal SafeThreadHandle(IntPtr handle) : base(handle) {
        }
    }

    public sealed class SafeModuleHandle : SafeHandleZeroOrMinusOneIsInvalid {
        internal static SafeModuleHandle InvalidHandle = new SafeModuleHandle(IntPtr.Zero);

        public SafeModuleHandle() : base(true) {
        }

        public SafeModuleHandle(IntPtr handle) : base(true) {
            SetHandle(handle);
        }

        /// <summary>
        ///     When overridden in a derived class, executes the code required to free the handle.
        /// </summary>
        /// <returns> true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant. </returns>
        protected override bool ReleaseHandle() {
            return true;
        }
    }
}