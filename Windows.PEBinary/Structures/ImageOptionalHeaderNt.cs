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
    // -----------------------------------------------------------------------
    // Original Code: 
    // Copyright (C) 2000-2002 Lutz Roeder. All rights reserved.
    // http://www.aisto.com/roeder
    // roeder@aisto.com
    using System;

    public class ImageOptionalHeaderNt {
        public uint AddressOfEntryPoint;
        public uint BaseOfCode;
        public uint BaseOfData_32bit; // 32bit only 
        public ushort DllFlags;
        public uint FileAlignment;
        public uint FileChecksum;
        public uint HeaderSize;
        public uint HeapCommitSize_32bit;
        public UInt64 HeapCommitSize_64bit;
        public uint HeapReserveSize_32bit;
        public UInt64 HeapReserveSize_64bit;
        public uint ImageBase_32bit;
        public UInt64 ImageBase_64bit;
        public uint ImageSize;
        public uint LoaderFlags;
        public ushort Magic;
        public byte MajorLinkerVersion;
        public byte MinorLinkerVersion;
        public uint NumberOfDataDirectories;
        public ushort OsMajor;
        public ushort OsMinor;
        public uint Reserved;
        public uint SectionAlignment;
        public uint SizeOfCode;
        public uint SizeOfInitializedData;
        public uint SizeOfUninitializedData;
        public uint StackCommitSize_32bit;
        public UInt64 StackCommitSize_64bit;
        public uint StackReserveSize_32bit;
        public UInt64 StackReserveSize_64bit;
        public ushort SubSysMajor;
        public ushort SubSysMinor;
        public ushort SubSystem;
        public ushort UserMajor;
        public ushort UserMinor;
    }
}