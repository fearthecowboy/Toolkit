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
    ///     A resource table header.
    /// </summary>
    public class ResourceTableHeader {
        /// <summary>
        ///     Resource table header.
        /// </summary>
        protected ResourceHeader _header;

        /// <summary>
        ///     Resource table key.
        /// </summary>
        protected string _key;

        /// <summary>
        ///     A new resource table header.
        /// </summary>
        public ResourceTableHeader() {
        }

        /// <summary>
        ///     An resource table header with a specific key.
        /// </summary>
        /// <param name="key">resource key</param>
        public ResourceTableHeader(string key) {
            _key = key;
        }

        /// <summary>
        ///     An existing resource table.
        /// </summary>
        /// <param name="lpRes">Pointer to resource table data.</param>
        internal ResourceTableHeader(IntPtr lpRes) {
            Read(lpRes);
        }

        /// <summary>
        ///     Resource table key.
        /// </summary>
        public string Key {
            get {
                return _key;
            }
        }

        /// <summary>
        ///     Resource header.
        /// </summary>
        public ResourceHeader Header {
            get {
                return _header;
            }
            set {
                _header = value;
            }
        }

        /// <summary>
        ///     Read the resource header, return a pointer to the end of it.
        /// </summary>
        /// <param name="lpRes">Top of header.</param>
        /// <returns>End of header, after the key, aligned at a 32 bit boundary.</returns>
        internal virtual IntPtr Read(IntPtr lpRes) {
            _header = (ResourceHeader)Marshal.PtrToStructure(lpRes, typeof (ResourceHeader));

            var pBlockKey = new IntPtr(lpRes.ToInt32() + Marshal.SizeOf(_header));
            _key = Marshal.PtrToStringUni(pBlockKey);

            return ResourceUtil.Align(pBlockKey.ToInt32() + (_key.Length + 1)*Marshal.SystemDefaultCharSize);
        }

        /// <summary>
        ///     Write the resource table.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal virtual void Write(BinaryWriter w) {
            // wLength
            w.Write(_header.wLength);
            // wValueLength
            w.Write(_header.wValueLength);
            // wType
            w.Write(_header.wType);
            // write key
            w.Write(Encoding.Unicode.GetBytes(_key));
            // null-terminator
            w.Write((UInt16)0);
            // pad fixed info
            ResourceUtil.PadToDWORD(w);
        }

        /// <summary>
        ///     String representation.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() {
            return ToString(0);
        }

        /// <summary>
        ///     String representation.
        /// </summary>
        /// <param name="indent">Indent.</param>
        /// <returns>String representation.</returns>
        public virtual string ToString(int indent) {
            return base.ToString();
        }
    }
}