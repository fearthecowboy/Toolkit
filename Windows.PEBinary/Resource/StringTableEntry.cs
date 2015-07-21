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
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using Structures;

    /// <summary>
    ///     This structure depicts the organization of data in a file-version resource. It contains a string
    ///     that describes a specific aspect of a file, such as a file's version, its copyright notices,
    ///     or its trademarks.
    ///     http://msdn.microsoft.com/en-us/library/aa909025.aspx
    /// </summary>
    public class StringTableEntry {
        private ResourceHeader _header;
        private string _key;

        /// <summary>
        ///     The value is always stored double-null-terminated.
        /// </summary>
        private string _value;

        /// <summary>
        ///     A new string resource.
        /// </summary>
        /// <param name="key">Key.</param>
        public StringTableEntry(string key) {
            _key = key;
            _header.wType = 1;
            _header.wLength = 0;
            _header.wValueLength = 0;
        }

        /// <summary>
        ///     An existing string resource.
        /// </summary>
        /// <param name="lpRes">Pointer to the beginning of a string resource.</param>
        internal StringTableEntry(IntPtr lpRes) {
            Read(lpRes);
        }

        /// <summary>
        ///     String resource header.
        /// </summary>
        public ResourceHeader Header {
            get {
                return _header;
            }
        }

        /// <summary>
        ///     Key.
        /// </summary>
        public string Key {
            get {
                return _key;
            }
        }

        /// <summary>
        ///     String value (removing the double-null-terminator).
        /// </summary>
        public string StringValue {
            get {
                if (_value == null) {
                    return _value;
                }

                return _value.Substring(0, _value.Length - 1);
            }
        }

        /// <summary>
        ///     Value.
        /// </summary>
        public string Value {
            get {
                return _value;
            }
            set {
                if (value == null) {
                    _value = null;
                    _header.wValueLength = 0;
                } else {
                    if (value.Length == 0 || value[value.Length - 1] != '\0') {
                        _value = value + '\0';
                    } else {
                        _value = value;
                    }

                    _header.wValueLength = (UInt16)_value.Length;
                }
            }
        }

        /// <summary>
        ///     Read a string resource.
        /// </summary>
        /// <param name="lpRes">Pointer to the beginning of a string resource.</param>
        internal void Read(IntPtr lpRes) {
            _header = (ResourceHeader)Marshal.PtrToStructure(lpRes, typeof (ResourceHeader));

            var pKey = new IntPtr(lpRes.ToInt32() + Marshal.SizeOf(_header));
            _key = Marshal.PtrToStringUni(pKey);

            var pValue = ResourceUtil.Align(pKey.ToInt32() + (_key.Length + 1)*Marshal.SystemDefaultCharSize);
            _value = ((_header.wValueLength > 0) ? Marshal.PtrToStringUni(pValue, _header.wValueLength) : null);
        }

        /// <summary>
        ///     Write a string resource to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal void Write(BinaryWriter w) {
            // write the block info
            var headerPos = w.BaseStream.Position;
            // wLength
            w.Write(_header.wLength);
            // wValueLength
            w.Write(_header.wValueLength);
            // wType
            w.Write(_header.wType);
            // szKey
            w.Write(Encoding.Unicode.GetBytes(_key));
            // null terminator
            w.Write((UInt16)0);
            // pad fixed info
            ResourceUtil.PadToDWORD(w);
            var valuePos = w.BaseStream.Position;
            if (_value != null) {
                // value (always double-null-terminated)
                w.Write(Encoding.Unicode.GetBytes(_value));
            }
            // wValueLength
            ResourceUtil.WriteAt(w, (w.BaseStream.Position - valuePos)/Marshal.SystemDefaultCharSize, headerPos + 2);
            // wLength
            ResourceUtil.WriteAt(w, w.BaseStream.Position - headerPos, headerPos);
        }
    }
}