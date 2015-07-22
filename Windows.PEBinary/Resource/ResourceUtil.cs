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

    /// <summary>
    ///     Resource utilities.
    /// </summary>
    public abstract class ResourceUtil {
        /// <summary>
        ///     Neutral language ID.
        /// </summary>
        public static UInt16 NEUTRALLANGID
        {
            get
            {
                return MAKELANGID(Kernel32.Constants.LANG_NEUTRAL, Kernel32.Constants.SUBLANG_NEUTRAL);
            }
        }

        /// <summary>
        ///     US-English language ID.
        /// </summary>
        public static UInt16 USENGLISHLANGID
        {
            get
            {
                return MAKELANGID(Kernel32.Constants.LANG_ENGLISH, Kernel32.Constants.SUBLANG_ENGLISH_US);
            }
        }

        /// <summary>
        ///     Align an address to a 4-byte boundary.
        /// </summary>
        /// <param name="p">Address in memory.</param>
        /// <returns>4-byte aligned pointer.</returns>
        internal static IntPtr Align(Int32 p) {
            return new IntPtr((p + 3) & ~3);
        }

        /// <summary>
        ///     Align a pointer to a 4-byte boundary.
        /// </summary>
        /// <param name="p">Pointer to an address in memory.</param>
        /// <returns>4-byte aligned pointer.</returns>
        internal static IntPtr Align(IntPtr p) {
            return Align(p.ToInt32());
        }

        /// <summary>
        ///     Pad data to a WORD.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        /// <returns>New position within the binary stream.</returns>
        internal static long PadToWORD(BinaryWriter w) {
            var pos = w.BaseStream.Position;

            if (pos%2 != 0) {
                var count = 2 - pos%2;
                Pad(w, (UInt16)count);
                pos += count;
            }

            return pos;
        }

        /// <summary>
        ///     Pad data to a DWORD.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        /// <returns>New position within the binary stream.</returns>
        internal static long PadToDWORD(BinaryWriter w) {
            var pos = w.BaseStream.Position;

            if (pos%4 != 0) {
                var count = 4 - pos%4;
                Pad(w, (UInt16)count);
                pos += count;
            }

            return pos;
        }

        /// <summary>
        ///     Returns the high WORD from a DWORD value.
        /// </summary>
        /// <param name="value">WORD value.</param>
        /// <returns>High WORD.</returns>
        internal static UInt16 HiWord(UInt32 value) {
            return (UInt16)((value & 0xFFFF0000) >> 16);
        }

        /// <summary>
        ///     Returns the high WORD from a DWORD value.
        /// </summary>
        /// <param name="value">WORD value.</param>
        /// <returns>High WORD.</returns>
        internal static UInt16 LoWord(UInt32 value) {
            return (UInt16)(value & 0x0000FFFF);
        }

        /// <summary>
        ///     Write a value at a given position.
        ///     Used to write a size of data in an earlier located header.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        /// <param name="value">Value to write.</param>
        /// <param name="address">Address to write the value at.</param>
        internal static void WriteAt(BinaryWriter w, long value, long address) {
            var cur = w.BaseStream.Position;
            w.Seek((int)address, SeekOrigin.Begin);
            w.Write((UInt16)value);
            w.Seek((int)cur, SeekOrigin.Begin);
        }

        /// <summary>
        ///     Pad bytes.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        /// <param name="len">Number of bytes to write.</param>
        /// <returns>New position within the stream.</returns>
        internal static long Pad(BinaryWriter w, UInt16 len) {
            while (len-- > 0) {
                w.Write((byte)0);
            }
            return w.BaseStream.Position;
        }

        /// <summary>
        ///     Make a language ID from a primary language ID (low-order 10 bits) and a sublanguage (high-order 6 bits).
        /// </summary>
        /// <param name="primary">Primary language ID.</param>
        /// <param name="sub">Sublanguage ID.</param>
        /// <returns>Microsoft language ID.</returns>
        public static UInt16 MAKELANGID(int primary, int sub) {
            return (UInt16)((((UInt16)sub) << 10) | ((UInt16)primary));
        }

        /// <summary>
        ///     Return the primary language ID from a Microsoft language ID.
        /// </summary>
        /// <param name="lcid">Microsoft language ID</param>
        /// <returns>primary language ID (low-order 10 bits)</returns>
        public static UInt16 PRIMARYLANGID(UInt16 lcid) {
            return (UInt16)((lcid) & 0x3ff);
        }

        /// <summary>
        ///     Return the sublanguage ID from a Microsoft language ID.
        /// </summary>
        /// <param name="lcid">Microsoft language ID.</param>
        /// <returns>Sublanguage ID (high-order 6 bits).</returns>
        public static UInt16 SUBLANGID(UInt16 lcid) {
            return (UInt16)((lcid) >> 10);
        }

        /// <summary>
        ///     Returns the memory representation of an object.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="anything">Data.</param>
        /// <returns>Object's representation in memory.</returns>
        internal static byte[] GetBytes<T>(T anything) {
            var rawsize = Marshal.SizeOf(anything);
            var buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(anything, buffer, false);
            var rawdatas = new byte[rawsize];
            Marshal.Copy(buffer, rawdatas, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return rawdatas;
        }

        /// <summary>
        ///     Get a collection of flags from a flag value.
        /// </summary>
        /// <typeparam name="T">Flag collection type.</typeparam>
        /// <param name="flagValue">Flag value.</param>
        /// <returns>Collection of flags.</returns>
        internal static List<string> FlagsToList<T>(UInt32 flagValue) {
            var flags = new List<string>();

            foreach (T f in Enum.GetValues(typeof (T))) {
                var f_ui = Convert.ToUInt32(f);
                if ((flagValue & f_ui) > 0 || flagValue == f_ui) {
                    flags.Add(f.ToString());
                }
            }

            return flags;
        }

        /// <summary>
        ///     Get a string representation of flags.
        /// </summary>
        /// <typeparam name="T">Flag collection type.</typeparam>
        /// <param name="flagValue">Flag vlaue</param>
        /// <returns>String representation of flags in the f1 | ... | fn format.</returns>
        internal static string FlagsToString<T>(UInt32 flagValue) {
            var flags = new List<string>();
            flags.AddRange(FlagsToList<T>(flagValue));
            return String.Join(" | ", flags.ToArray());
        }
    }
}