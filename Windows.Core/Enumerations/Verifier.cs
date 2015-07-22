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

namespace FearTheCowboy.Windows.Enumerations {
    using System;
    using System.Runtime.InteropServices;

    public enum WinTrustDataUIChoice : uint {
        All = 1,
        None = 2,
        NoBad = 3,
        NoGood = 4
    }

    public enum WinTrustDataRevocationChecks : uint {
        None = 0x00000000,
        WholeChain = 0x00000001
    }

    public enum WinTrustDataChoice : uint {
        File = 1,
        Catalog = 2,
        Blob = 3,
        Signer = 4,
        Certificate = 5
    }

    public enum WinTrustDataStateAction : uint {
        Ignore = 0x00000000,
        Verify = 0x00000001,
        Close = 0x00000002,
        AutoCache = 0x00000003,
        AutoCacheFlush = 0x00000004
    }

    [Flags]
    public enum WinTrustDataProvFlags : uint {
        UseIe4TrustFlag = 0x00000001,
        NoIe4ChainFlag = 0x00000002,
        NoPolicyUsageFlag = 0x00000004,
        RevocationCheckNone = 0x00000010,
        RevocationCheckEndCert = 0x00000020,
        RevocationCheckChain = 0x00000040,
        RevocationCheckChainExcludeRoot = 0x00000080,
        SaferFlag = 0x00000100,
        HashOnlyFlag = 0x00000200,
        UseDefaultOsverCheck = 0x00000400,
        LifetimeSigningFlag = 0x00000800,
        CacheOnlyUrlRetrieval = 0x00001000 // affects CRL retrieval and AIA retrieval
    }

    public enum WinTrustDataUIContext : uint {
        Execute = 0,
        Install = 1
    }

    public enum WinVerifyTrustResult : uint {
        Success = 0,
        ProviderUnknown = 0x800b0001, // The trust provider is not recognized on this system
        ActionUnknown = 0x800b0002, // The trust provider does not support the specified action
        SubjectFormUnknown = 0x800b0003, // The trust provider does not support the form specified for the subject
        SubjectNotTrusted = 0x800b0004, // The subject failed the specified verification action
        UntrustedRootCert = 0x800B0109 //A certificate chain processed, but terminated in a root certificate which is not trusted by the trust provider. 
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class WinTrustFileInfo {
        private UInt32 StructSize = (UInt32)Marshal.SizeOf(typeof (WinTrustFileInfo));
        private IntPtr FilePath; // required, file name to be verified
        private IntPtr hFile = IntPtr.Zero; // optional, open handle to FilePath
        private IntPtr pgKnownSubject = IntPtr.Zero; // optional, subject type if it is known

        public WinTrustFileInfo(String _filePath) {
            FilePath = Marshal.StringToCoTaskMemAuto(_filePath);
        }

        ~WinTrustFileInfo() {
            Marshal.FreeCoTaskMem(FilePath);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class WinTrustData {
        private UInt32 StructSize = (UInt32)Marshal.SizeOf(typeof (WinTrustData));
        private IntPtr PolicyCallbackData = IntPtr.Zero;
        private IntPtr SIPClientData = IntPtr.Zero;
        // required: UI choice
        private WinTrustDataUIChoice UIChoice = WinTrustDataUIChoice.None;
        // required: certificate revocation check options
        private WinTrustDataRevocationChecks RevocationChecks = WinTrustDataRevocationChecks.None;
        // required: which structure is being passed in?
        private WinTrustDataChoice UnionChoice = WinTrustDataChoice.File;
        // individual file
        private IntPtr FileInfoPtr;
        private WinTrustDataStateAction StateAction = WinTrustDataStateAction.Ignore;
        private IntPtr StateData = IntPtr.Zero;
        private String URLReference;
        private WinTrustDataProvFlags ProvFlags = WinTrustDataProvFlags.SaferFlag;
        private WinTrustDataUIContext UIContext = WinTrustDataUIContext.Execute;

        // constructor for silent WinTrustDataChoice.File check
        public WinTrustData(String filename) {
            var wtfiData = new WinTrustFileInfo(filename);
            FileInfoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof (WinTrustFileInfo)));
            Marshal.StructureToPtr(wtfiData, FileInfoPtr, false);
        }

        ~WinTrustData() {
            Marshal.FreeCoTaskMem(FileInfoPtr);
        }
    }
}