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
    using System.Security.AccessControl;
    using Enumerations;
    using Structures;

    public static class Advapi32 {
        // Token Specific Access Rights

        public const UInt32 STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        public const UInt32 STANDARD_RIGHTS_READ = 0x00020000;
        public const UInt32 TOKEN_ASSIGN_PRIMARY = 0x0001;
        public const UInt32 TOKEN_DUPLICATE = 0x0002;
        public const UInt32 TOKEN_IMPERSONATE = 0x0004;
        public const UInt32 TOKEN_QUERY = 0x0008;
        public const UInt32 TOKEN_QUERY_SOURCE = 0x0010;
        public const UInt32 TOKEN_ADJUST_PRIVILEGES = 0x0020;
        public const UInt32 TOKEN_ADJUST_GROUPS = 0x0040;
        public const UInt32 TOKEN_ADJUST_DEFAULT = 0x0080;
        public const UInt32 TOKEN_ADJUST_SESSIONID = 0x0100;
        public const UInt32 TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);

        public const UInt32 TOKEN_ALL_ACCESS =
            (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY | TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE | TOKEN_ADJUST_PRIVILEGES |
             TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT | TOKEN_ADJUST_SESSIONID);

        public const Int32 ERROR_INSUFFICIENT_BUFFER = 122;
        // Integrity Levels

        public const Int32 SECURITY_MANDATORY_UNTRUSTED_RID = 0x00000000;
        public const Int32 SECURITY_MANDATORY_LOW_RID = 0x00001000;
        public const Int32 SECURITY_MANDATORY_MEDIUM_RID = 0x00002000;
        public const Int32 SECURITY_MANDATORY_HIGH_RID = 0x00003000;
        public const Int32 SECURITY_MANDATORY_SYSTEM_RID = 0x00004000;

        /// <summary>
        ///     Sets the elevation required state for a specified button or command link to display an elevated icon.
        /// </summary>
        public const UInt32 BCM_SETSHIELD = 0x160C;

        /// <summary>
        ///     The function opens the access token associated with a process.
        /// </summary>
        /// <param name="hProcess"> A handle to the process whose access token is opened. </param>
        /// <param name="desiredAccess"> Specifies an access mask that specifies the requested types of access to the access token. </param>
        /// <param name="hToken"> Outputs a handle that identifies the newly opened access token when the function returns. </param>
        /// <returns> </returns>
        [DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr hProcess, UInt32 desiredAccess, out SafeTokenHandle hToken);

        /// <summary>
        ///     The function creates a new access token that duplicates one already in existence.
        /// </summary>
        /// <param name="ExistingTokenHandle"> A handle to an access token opened with TOKEN_DUPLICATE access. </param>
        /// <param name="ImpersonationLevel">
        ///     Specifies a SECURITY_IMPERSONATION_LEVEL enumerated type that supplies the
        ///     impersonation level of the new token.
        /// </param>
        /// <param name="DuplicateTokenHandle"> Outputs a handle to the duplicate token. </param>
        /// <returns> </returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DuplicateToken(SafeTokenHandle ExistingTokenHandle, SecurityImpersonationLevel ImpersonationLevel,
            out SafeTokenHandle DuplicateTokenHandle);

        /// <summary>
        ///     The function retrieves a specified type of information about an access token. The calling process must have
        ///     appropriate access rights to obtain the information.
        /// </summary>
        /// <param name="hToken"> A handle to an access token from which information is retrieved. </param>
        /// <param name="tokenInfoClass">
        ///     Specifies a value from the TOKEN_INFORMATION_CLASS enumerated type to identify the type
        ///     of information the function retrieves.
        /// </param>
        /// <param name="pTokenInfo"> A pointer to a buffer the function fills with the requested information. </param>
        /// <param name="tokenInfoLength">
        ///     Specifies the size, in bytes, of the buffer pointed to by the TokenInformation
        ///     parameter.
        /// </param>
        /// <param name="returnLength">
        ///     A pointer to a variable that receives the number of bytes needed for the buffer pointed to
        ///     by the TokenInformation parameter.
        /// </param>
        /// <returns> </returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetTokenInformation(SafeTokenHandle hToken, TokenInformationClass tokenInfoClass, IntPtr pTokenInfo, Int32 tokenInfoLength,
            out Int32 returnLength);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetTokenInformation(IntPtr hToken, TokenInformationClass tokenInfoClass, IntPtr pTokenInfo, Int32 tokenInfoLength,
            out Int32 returnLength);

        /// <summary>
        ///     The function returns a pointer to a specified subauthority in a security identifier (SID). The subauthority value
        ///     is a relative identifier (RID).
        /// </summary>
        /// <param name="pSid"> A pointer to the SID structure from which a pointer to a subauthority is to be returned. </param>
        /// <param name="nSubAuthority">
        ///     Specifies an index value identifying the subauthority array element whose address the
        ///     function will return.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is a pointer to the specified SID subauthority. To get extended
        ///     error information, call GetLastError. If the function fails, the return value is undefined. The function fails if
        ///     the specified SID structure is not valid or if the index value specified by the nSubAuthority parameter is out of
        ///     bounds.
        /// </returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetSidSubAuthority(IntPtr pSid, UInt32 nSubAuthority);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool InitializeSecurityDescriptor(ref SECURITY_DESCRIPTOR sd, uint dwRevision);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool SetSecurityDescriptorDacl(ref SECURITY_DESCRIPTOR sd, bool daclPresent, IntPtr dacl, bool daclDefaulted);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool QueryServiceObjectSecurity(IntPtr serviceHandle, SecurityInfos secInfo, ref SECURITY_DESCRIPTOR lpSecDesrBuf, uint bufSize, out uint bufSizeNeeded);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool QueryServiceObjectSecurity(SafeHandle serviceHandle, SecurityInfos secInfo, byte[] lpSecDesrBuf, uint bufSize, out uint bufSizeNeeded);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool SetServiceObjectSecurity(SafeHandle serviceHandle, SecurityInfos secInfos, byte[] lpSecDesrBuf);

        [DllImport("advapi32.dll", EntryPoint = "AllocateAndInitializeSid")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocateAndInitializeSid([In] ref SidIdentifierAuthority pIdentifierAuthority, byte nSubAuthorityCount, uint nSubAuthority0, uint nSubAuthority1, uint nSubAuthority2, uint nSubAuthority3, uint nSubAuthority4,
            uint nSubAuthority5, int nSubAuthority6, uint nSubAuthority7, out IntPtr pSid);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool CheckTokenMembership(IntPtr TokenHandle, IntPtr SidToCheck, out bool IsMember);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SidIdentifierAuthority {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
        public byte[] Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_DESCRIPTOR {
        public byte revision;
        public byte size;
        public short control;
        public IntPtr owner;
        public IntPtr group;
        public IntPtr sacl;
        public IntPtr dacl;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_ATTRIBUTES {
        public int nLength;
        public IntPtr lpSecurityDescriptor;
        public int bInheritHandle;
    }
}