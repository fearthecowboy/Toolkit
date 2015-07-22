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
    ///     Native function calls using NTDLL
    /// </summary>
    public static class Ntdll {
        [DllImport("ntdll.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 RtlAcquirePrivilege(ref UInt32 Privilege, UInt32 NumPriv, UInt32 Flags, ref IntPtr ReturnedState);

        [DllImport("ntdll.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 RtlReleasePrivilege(IntPtr ReturnedState);

        [DllImport("ntdll.dll")]
        //public static extern UInt32 RtlCreateUserThread(SafeProcessHandle processHandle, IntPtr lpThreadSecurity, bool createSuspended, int stackZeroBits, IntPtr stackReserved, IntPtr stackCommit, IntPtr startAddress, IntPtr parameter, out uint threadId, out IntPtr clientId);
        public static extern UInt32 RtlCreateUserThread(SafeProcessHandle processHandle, IntPtr lpThreadSecurity, bool createSuspended, int stackZeroBits, IntPtr stackReserved, IntPtr stackCommit, IntPtr startAddress, IntPtr parameter, out uint threadId,
            IntPtr clientId);

        [DllImport("ntdll.dll")]
        public static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref ParentProcess processInformation, int processInformationLength, out int returnLength);
    }
}