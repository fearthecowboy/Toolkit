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
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DigitalSignExtendedInfo {
        public uint dwSize;
        public uint dwAttrFlagsNotUsed;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwszDescription;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwszMoreInfoLocation;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string pszHashAlg;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwszSigningCertDisplayString;

        public IntPtr hAdditionalCertStore;

        public IntPtr psAuthenticated;

        public IntPtr psUnauthenticated;

        public DigitalSignExtendedInfo(string description = null, string moreInfoUrl = null, string hashAlgorithm = null) {
            dwSize = (uint)Marshal.SizeOf(typeof (DigitalSignExtendedInfo));
            dwAttrFlagsNotUsed = 0;
            pwszSigningCertDisplayString = null;
            hAdditionalCertStore = IntPtr.Zero;
            psAuthenticated = IntPtr.Zero;
            psUnauthenticated = IntPtr.Zero;

            pwszDescription = string.IsNullOrEmpty(description) ? null : description;
            pwszMoreInfoLocation = string.IsNullOrEmpty(moreInfoUrl) ? null : moreInfoUrl;
            pszHashAlg = string.IsNullOrEmpty(hashAlgorithm) ? null : hashAlgorithm;
        }
    }
}