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

namespace FearTheCowboy.Windows.Structures {
    using System;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    ///     SafeHandleBase implements ReleaseHandle method for all our Safe Handle classes. The purpose of the safe handle
    ///     class is to get away from having IntPtr objects for handles coming back from Kernel APIs, and instead provide a
    ///     type-safe wrapper that prohibits the accidental use of one handle type where another should be. We create a common
    ///     base class so that the release semantics are implemented the same.
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
        /// <returns>
        ///     true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In
        ///     this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.
        /// </returns>
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
        /// <returns>
        ///     true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In
        ///     this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.
        /// </returns>
        protected override bool ReleaseHandle() {
            return true;
        }
    }
}