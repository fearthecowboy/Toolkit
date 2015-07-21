//-----------------------------------------------------------------------
// <copyright company="CoApp Project">
//     ResourceLib Original Code from http://resourcelib.codeplex.com
//     Original Copyright (c) 2008-2009 Vestris Inc.
//     Changes Copyright (c) 2011 Garrett Serack . All rights reserved.
// </copyright>
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

namespace Toolkit.Windows.Resource {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Enumerations;

    /// <summary>
    ///     This structure depicts the organization of data in a file-version resource.
    ///     It contains version information that can be displayed for a particular language and code page.
    ///     http://msdn.microsoft.com/en-us/library/aa908808.aspx
    /// </summary>
    public class StringFileInfo : ResourceTableHeader {
        private IDictionary<string, StringTable> _strings = new Dictionary<string, StringTable>();

        /// <summary>
        ///     A new string file-version resource.
        /// </summary>
        public StringFileInfo() : base("StringFileInfo") {
            _header.wType = (UInt16)ResourceHeaderType.StringData;
        }

        /// <summary>
        ///     An existing string file-version resource.
        /// </summary>
        /// <param name="lpRes">Pointer to the beginning of a string file-version resource.</param>
        internal StringFileInfo(IntPtr lpRes) {
            Read(lpRes);
        }

        /// <summary>
        ///     Resource strings.
        /// </summary>
        public IDictionary<string, StringTable> Strings {
            get {
                return _strings;
            }
        }

        /// <summary>
        ///     Default (first) string table.
        /// </summary>
        public StringTable Default {
            get {
                var iter = _strings.GetEnumerator();
                if (iter.MoveNext()) {
                    return iter.Current.Value;
                }
                return null;
            }
        }

        /// <summary>
        ///     Indexed string table.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>A string table at a given index.</returns>
        public string this[string key] {
            get {
                return Default[key];
            }
            set {
                Default[key] = value;
            }
        }

        /// <summary>
        ///     Read an existing string file-version resource.
        /// </summary>
        /// <param name="lpRes">Pointer to the beginning of a string file-version resource.</param>
        /// <returns>Pointer to the end of the string file-version resource.</returns>
        internal override IntPtr Read(IntPtr lpRes) {
            _strings.Clear();
            var pChild = base.Read(lpRes);

            while (pChild.ToInt32() < (lpRes.ToInt32() + _header.wLength)) {
                var res = new StringTable(pChild);
                _strings.Add(res.Key, res);
                pChild = ResourceUtil.Align(pChild.ToInt32() + res.Header.wLength);
            }

            return new IntPtr(lpRes.ToInt32() + _header.wLength);
        }

        /// <summary>
        ///     Write the string file-version resource to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal override void Write(BinaryWriter w) {
            var headerPos = w.BaseStream.Position;
            base.Write(w);

            var stringsEnum = _strings.GetEnumerator();
            while (stringsEnum.MoveNext()) {
                stringsEnum.Current.Value.Write(w);
            }

            ResourceUtil.WriteAt(w, w.BaseStream.Position - headerPos, headerPos);
            ResourceUtil.PadToDWORD(w);
        }

        /// <summary>
        ///     String representation of StringFileInfo.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>String in the StringFileInfo format.</returns>
        public override string ToString(int indent) {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0}BEGIN", new String(' ', indent)));
            sb.AppendLine(string.Format("{0}BLOCK \"{1}\"", new String(' ', indent + 1), _key));
            foreach (var stringTable in _strings.Values) {
                sb.Append(stringTable.ToString(indent + 1));
            }
            sb.AppendLine(string.Format("{0}END", new String(' ', indent)));
            return sb.ToString();
        }
    }
}