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
    using System.Runtime.InteropServices;
    using Enumerations;

    [StructLayout(LayoutKind.Sequential)]
    public struct ReparseData {
        /// <summary>
        ///     Reparse point tag.
        /// </summary>
        public IoReparseTag ReparseTag;

        /// <summary>
        ///     Size, in bytes, of the data after the Reserved member. This can be calculated by: (4 * sizeof(ushort)) +
        ///     SubstituteNameLength + PrintNameLength + (namesAreNullTerminated ? 2 * sizeof(char) : 0);
        /// </summary>
        public ushort ReparseDataLength;

        /// <summary>
        ///     Reserved. do not use.
        /// </summary>
        public ushort Reserved;

        /// <summary>
        ///     Offset, in bytes, of the substitute name string in the PathBuffer array.
        /// </summary>
        public ushort SubstituteNameOffset;

        /// <summary>
        ///     Length, in bytes, of the substitute name string. If this string is null-terminated, SubstituteNameLength does not
        ///     include space for the null character.
        /// </summary>
        public ushort SubstituteNameLength;

        /// <summary>
        ///     Offset, in bytes, of the print name string in the PathBuffer array.
        /// </summary>
        public ushort PrintNameOffset;

        /// <summary>
        ///     Length, in bytes, of the print name string. If this string is null-terminated, PrintNameLength does not include
        ///     space for the null character.
        /// </summary>
        public ushort PrintNameLength;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x3FF0)]
        public byte[] PathBuffer;
    }
}