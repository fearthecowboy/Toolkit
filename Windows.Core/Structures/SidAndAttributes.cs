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

    /// <summary>
    ///     The structure represents a security identifier (SID) and its attributes. SIDs are used to uniquely identify users
    ///     or groups.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SidAndAttributes {
        public IntPtr Sid;
        public Int32 Attributes;
    }
}