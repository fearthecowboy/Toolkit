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

namespace FearTheCowboy.Windows.Resource {
    //-----------------------------------------------------------------------
    //     ResourceLib Original Code from http://resourcelib.codeplex.com
    //     Original Copyright (c) 2008-2009 Vestris Inc.
    // <license>
    // MIT License
    // You may freely use and distribute this software under the terms of the following license agreement.
    // 
    // Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
    // documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
    // the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
    // to permit persons to whom the Software is furnished to do so, subject to the following conditions:
    // 
    // The above copyright notice and this permission notice shall be included in all copies or substantial portions of 
    // the Software.
    // 
    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
    // THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
    // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
    // TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
    // </license>
    //-----------------------------------------------------------------------
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using Enumerations;

    /// <summary>
    ///     A string, RT_STRING resource.
    ///     Each string resource block has 16 strings, each represented as an ordered pair
    ///     (length, text). Length is a WORD that specifies the size, in terms of the number of characters,
    ///     in the text that follows. Text follows length and contains the string in Unicode without the
    ///     NULL terminating character. There may be no characters in text, in which case length is zero.
    /// </summary>
    public class StringResource : Resource {
        /// <summary>
        ///     A new string resource.
        /// </summary>
        public StringResource() : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(ResourceTypes.RT_STRING), null, ResourceUtil.NEUTRALLANGID, 0) {
        }

        /// <summary>
        ///     A new string resource of a given block id.
        /// </summary>
        /// <param name="blockId">Block id.</param>
        public StringResource(ResourceId blockId)
            : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(ResourceTypes.RT_STRING), blockId, ResourceUtil.NEUTRALLANGID, 0) {
        }

        /// <summary>
        ///     A new string resource of a given block id.
        /// </summary>
        /// <param name="blockId">Block id.</param>
        public StringResource(UInt16 blockId) : this(new ResourceId(blockId)) {
        }

        /// <summary>
        ///     An existing string resource.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResource">Resource ID.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="language">Language ID.</param>
        /// <param name="size">Resource size.</param>
        public StringResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, UInt16 language, int size)
            : base(hModule, hResource, type, name, language, size) {
        }

        /// <summary>
        ///     String collection in this resource.
        /// </summary>
        public IDictionary<UInt16, string> Strings {get; set;} = new Dictionary<UInt16, string>();

        /// <summary>
        ///     Returns a string of a given Id.
        /// </summary>
        /// <param name="id">String Id.</param>
        /// <returns>A string of a given Id.</returns>
        public string this[UInt16 id] {get {return Strings[id];} set {Strings[id] = value;}}

        /// <summary>
        ///     String table block id.
        /// </summary>
        public UInt16 BlockId {get {return (UInt16)Name.Id.ToInt32();} set {Name = new ResourceId(value);}}

        /// <summary>
        ///     A string with ID, stringId, is located in the block with ID given by the following formula.
        ///     http://support.microsoft.com/kb/q196774/
        /// </summary>
        public static UInt16 GetBlockId(int stringId) {
            return (UInt16)((stringId/16) + 1);
        }

        /// <summary>
        ///     Read the strings.
        /// </summary>
        /// <param name="hModule">Handle to a module.</param>
        /// <param name="lpRes">Pointer to the beginning of the string table.</param>
        /// <returns>Address of the end of the string table.</returns>
        internal override IntPtr Read(IntPtr hModule, IntPtr lpRes) {
            for (var i = 0; i < 16; i++) {
                var len = (UInt16)Marshal.ReadInt16(lpRes);
                if (len != 0) {
                    var id = (UInt16)((BlockId - 1)*16 + i);
                    var lpString = new IntPtr(lpRes.ToInt32() + 2);
                    var s = Marshal.PtrToStringUni(lpString, len);
                    Strings.Add(id, s);
                }
                lpRes = new IntPtr(lpRes.ToInt32() + 2 + (len*Marshal.SystemDefaultCharSize));
            }

            return lpRes;
        }

        internal override void Write(BinaryWriter w) {
            for (var i = 0; i < 16; i++) {
                var id = (UInt16)((BlockId - 1)*16 + i);
                string s = null;
                if (Strings.TryGetValue(id, out s)) {
                    w.Write((UInt16)s.Length);
                    w.Write(Encoding.Unicode.GetBytes(s));
                } else {
                    w.Write((UInt16)0);
                }
            }
        }

        /// <summary>
        ///     String representation of the strings resource.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine("STRINGTABLE");
            sb.AppendLine("BEGIN");
            var stringEnumerator = Strings.GetEnumerator();
            while (stringEnumerator.MoveNext()) {
                sb.AppendLine($" {stringEnumerator.Current.Key} {stringEnumerator.Current.Value}");
            }
            sb.AppendLine("END");
            return sb.ToString();
        }
    }
}