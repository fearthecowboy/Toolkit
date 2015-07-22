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
    using System.Text;
    using Enumerations;

    /// <summary>
    ///     This structure depicts the organization of data in a file-version resource. It contains language
    ///     and code page formatting information for the strings. A code page is an ordered character set.
    ///     See http://msdn.microsoft.com/en-us/library/aa909192.aspx for more information.
    /// </summary>
    public class StringTable : ResourceTableHeader {
        /// <summary>
        ///     A new string table.
        /// </summary>
        public StringTable() {
        }

        /// <summary>
        ///     A new string table.
        /// </summary>
        /// <param name="key">String table key.</param>
        public StringTable(string key) : base(key) {
            _header.wType = (UInt16)ResourceHeaderType.StringData;
        }

        /// <summary>
        ///     An existing string table.
        /// </summary>
        /// <param name="lpRes">Pointer to the beginning of the table.</param>
        internal StringTable(IntPtr lpRes) {
            Read(lpRes);
        }

        /// <summary>
        ///     Resource strings.
        /// </summary>
        public IDictionary<string, StringTableEntry> Strings {get;} = new Dictionary<string, StringTableEntry>();

        /// <summary>
        ///     The four most significant digits of the key represent the language identifier.
        ///     Each Microsoft Standard Language identifier contains two parts: the low-order 10 bits
        ///     specify the major language, and the high-order 6 bits specify the sublanguage.
        /// </summary>
        public UInt16 LanguageID
        {
            get
            {
                if (string.IsNullOrEmpty(_key)) {
                    return 0;
                }

                return Convert.ToUInt16(_key.Substring(0, 4), 16);
            }
            set
            {
                _key = string.Format("{0:x4}{1:x4}", value, CodePage);
            }
        }

        /// <summary>
        ///     The four least significant digits of the key represent the code page for which the data is formatted.
        /// </summary>
        public UInt16 CodePage
        {
            get
            {
                if (string.IsNullOrEmpty(_key)) {
                    return 0;
                }

                return Convert.ToUInt16(_key.Substring(4, 4), 16);
            }
            set
            {
                _key = string.Format("{0:x4}{1:x4}", LanguageID, value);
            }
        }

        /// <summary>
        ///     Returns an entry within the string table.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>An entry within the string table.</returns>
        public string this[string key]
        {
            get
            {
                var v = Strings[key];
                return v == null ? null : Strings[key].Value;
            }
            set
            {
                StringTableEntry sr = null;
                if (!Strings.TryGetValue(key, out sr)) {
                    sr = new StringTableEntry(key);
                    Strings.Add(key, sr);
                }

                sr.Value = value;
            }
        }

        /// <summary>
        ///     Read a string table.
        /// </summary>
        /// <param name="lpRes">Pointer to the beginning of the string table.</param>
        /// <returns>Pointer to the end of the string table.</returns>
        internal override IntPtr Read(IntPtr lpRes) {
            Strings.Clear();
            var pChild = base.Read(lpRes);

            while (pChild.ToInt32() < (lpRes.ToInt32() + _header.wLength)) {
                var res = new StringTableEntry(pChild);
                Strings.Add(res.Key, res);
                pChild = ResourceUtil.Align(pChild.ToInt32() + res.Header.wLength);
            }

            return new IntPtr(lpRes.ToInt32() + _header.wLength);
        }

        /// <summary>
        ///     Write the string table to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        /// <returns>Last unpadded position.</returns>
        internal override void Write(BinaryWriter w) {
            var headerPos = w.BaseStream.Position;
            base.Write(w);

            var total = Strings.Count;
            var stringsEnum = Strings.GetEnumerator();
            while (stringsEnum.MoveNext()) {
                stringsEnum.Current.Value.Write(w);
                ResourceUtil.WriteAt(w, w.BaseStream.Position - headerPos, headerPos);
                // total parent structure size must not include padding
                if (-- total != 0) {
                    ResourceUtil.PadToDWORD(w);
                }
            }
        }

        /// <summary>
        ///     String representation of the string table.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>String representation of the strings table.</returns>
        public override string ToString(int indent) {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0}BEGIN", new String(' ', indent)));
            sb.AppendLine(string.Format("{0}BLOCK \"{1}\"", new String(' ', indent + 1), _key));
            sb.AppendLine(string.Format("{0}BEGIN", new String(' ', indent + 1)));
            foreach (var stringResource in Strings.Values) {
                sb.AppendLine(string.Format("{0}VALUE \"{1}\", \"{2}\"", new String(' ', indent + 2), stringResource.Key, stringResource.StringValue));
            }
            sb.AppendLine(string.Format("{0}END", new String(' ', indent + 1)));
            sb.AppendLine(string.Format("{0}END", new String(' ', indent)));
            return sb.ToString();
        }
    }
}