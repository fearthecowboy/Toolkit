﻿// 
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
    using Enumerations;
    using Microsoft.Win32.SafeHandles;

    public class ImageHlp {
        [DllImport("imagehlp.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ImageRemoveCertificate(SafeFileHandle fileHandle, uint index);

        [DllImport("Imagehlp.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ImageEnumerateCertificates(SafeFileHandle fileHandle, CertSectionType wTypeFilter, out uint dwCertCount, IntPtr pIndices, int pIndexCount);
    }
}