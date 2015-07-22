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

namespace FearTheCowboy.Windows.Structures {
    // -----------------------------------------------------------------------
    // Original Code: 
    // Copyright (C) 2000-2002 Lutz Roeder. All rights reserved.
    // http://www.aisto.com/roeder
    // roeder@aisto.com

    public struct ImageSectionHeader {
        public uint PointerToRawData;
        public uint SizeOfRawData;
        public uint VirtualAddress;
    }
}