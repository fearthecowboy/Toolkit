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
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    ///     Dialog template utility functions.
    /// </summary>
    internal abstract class DialogTemplateUtil {
        /// <summary>
        ///     Read a dialog resource id.
        /// </summary>
        /// <param name="lpRes">Address in memory.</param>
        /// <param name="rc">Resource read.</param>
        /// <returns></returns>
        internal static IntPtr ReadResourceId(IntPtr lpRes, out ResourceId rc) {
            rc = null;

            switch ((UInt16)Marshal.ReadInt16(lpRes)) {
                case 0x0000: // no predefined resource
                    lpRes = new IntPtr(lpRes.ToInt32() + 2);
                    break;
                case 0xFFFF: // one additional element that specifies the ordinal value of the resource
                    lpRes = new IntPtr(lpRes.ToInt32() + 2);
                    rc = new ResourceId((UInt16)Marshal.ReadInt16(lpRes));
                    lpRes = new IntPtr(lpRes.ToInt32() + 2);
                    break;
                default: // null-terminated Unicode string that specifies the name of the resource
                    rc = new ResourceId(Marshal.PtrToStringUni(lpRes));
                    lpRes = new IntPtr(lpRes.ToInt32() + (rc.Name.Length + 1)*Marshal.SystemDefaultCharSize);
                    break;
            }

            return lpRes;
        }

        internal static void WriteResourceId(BinaryWriter w, ResourceId rc) {
            if (rc == null) {
                w.Write((UInt16)0);
            } else if (rc.IsIntResource()) {
                w.Write((UInt16)0xFFFF);
                w.Write((UInt16)rc.Id);
            } else {
                ResourceUtil.PadToWORD(w);
                w.Write(Encoding.Unicode.GetBytes(rc.Name));
                w.Write((UInt16)0);
            }
        }

        /// <summary>
        ///     String representation of the dialog or control style of two types.
        /// </summary>
        /// <param name="style">Dialog or control style.</param>
        /// <returns>String in the "s1 | s2 | ... | s3" format.</returns>
        internal static string StyleToString<W, D>(UInt32 style) {
            var styles = new List<string>();
            styles.AddRange(ResourceUtil.FlagsToList<W>(style));
            styles.AddRange(ResourceUtil.FlagsToList<D>(style));
            return String.Join(" | ", styles.ToArray());
        }

        /// <summary>
        ///     String representation of the dialog or control styles of two types.
        /// </summary>
        /// <param name="style">Dialog or control style.</param>
        /// <param name="exstyle">Dialog or control extended style.</param>
        /// <returns>String in the "s1 | s2 | ... | s3" format.</returns>
        internal static string StyleToString<W, D>(UInt32 style, UInt32 exstyle) {
            var styles = new List<string>();
            styles.AddRange(ResourceUtil.FlagsToList<W>(style));
            styles.AddRange(ResourceUtil.FlagsToList<D>(exstyle));
            return String.Join(" | ", styles.ToArray());
        }

        /// <summary>
        ///     String representation of the dialog or control style of one type.
        /// </summary>
        /// <param name="style">Dialog or control style.</param>
        /// <returns>String in the "s1 | s2 | ... | s3" format.</returns>
        internal static string StyleToString<W>(UInt32 style) {
            var styles = new List<string>();
            styles.AddRange(ResourceUtil.FlagsToList<W>(style));
            return String.Join(" | ", styles.ToArray());
        }
    }
}