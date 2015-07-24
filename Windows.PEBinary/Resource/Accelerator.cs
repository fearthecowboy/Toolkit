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
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using Enumerations;
    using Flags;
    using Structures;

    /// <summary>
    ///     Standard accelerator.
    /// </summary>
    public class Accelerator {
        private Accel _accel;

        /// <summary>
        ///     String representation of the accelerator key.
        /// </summary>
        public string Key {
            get {
                var key = Enum.GetName(typeof (VirtualKeys), _accel.key);
                return string.IsNullOrEmpty(key) ? ((char)_accel.key).ToString() : key;
            }
        }

        /// <summary>
        ///     An unsigned integer value that identifies the accelerator.
        /// </summary>
        public UInt32 Command {get {return _accel.cmd;} set {_accel.cmd = value;}}

        /// <summary>
        ///     Read the accelerator.
        /// </summary>
        /// <param name="lpRes">Address in memory.</param>
        internal IntPtr Read(IntPtr lpRes) {
            _accel = (Accel)Marshal.PtrToStructure(lpRes, typeof (Accel));

            return new IntPtr(lpRes.ToInt32() + Marshal.SizeOf(_accel));
        }

        /// <summary>
        ///     Write accelerator to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal void Write(BinaryWriter w) {
            w.Write(_accel.fVirt);
            w.Write(_accel.key);
            w.Write(_accel.cmd);
            ResourceUtil.PadToWORD(w);
        }

        /// <summary>
        ///     String representation of the accelerator.
        /// </summary>
        /// <returns>String representation of the accelerator.</returns>
        public override string ToString() {
            return $"{Key}, {Command}, {ResourceUtil.FlagsToString<AcceleratorVirtualKey>(_accel.fVirt).Replace(" |", ",")}";
        }
    }
}