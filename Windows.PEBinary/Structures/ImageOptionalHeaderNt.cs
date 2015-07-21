//-----------------------------------------------------------------------
// <copyright company="CoApp Project">
//     Original Copyright (C) 2000-2002 Lutz Roeder. All rights reserved.
//     Changes Copyright (c) 2011  Garrett Serack. All rights reserved.
// </copyright>
// <license>
//     The software is licensed under the Apache 2.0 License (the "License")
//     You may not use the software except in compliance with the License. 
// </license>
//-----------------------------------------------------------------------

// -----------------------------------------------------------------------
// Original Code: 
// Copyright (C) 2000-2002 Lutz Roeder. All rights reserved.
// http://www.aisto.com/roeder
// roeder@aisto.com

namespace Toolkit.Windows.Structures {
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