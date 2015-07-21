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
    public class ImageCor20Header {
        public ImageDataDirectory CodeManagerTable;
        public uint EntryPointToken;
        public ImageDataDirectory ExportAddressTableJumps;
        public uint Flags;
        public ushort MajorRuntimeVersion;
        public ImageDataDirectory MetaData;
        public ushort MinorRuntimeVersion;
        public ImageDataDirectory Resources;
        public uint Size;
        public ImageDataDirectory StrongNameSignature;
        public ImageDataDirectory VTableFixups;
    }
}