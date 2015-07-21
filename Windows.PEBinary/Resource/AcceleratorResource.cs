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
    using Enumerations;
    using Structures;

    /// <summary>
    ///     An accelerator, RT_ACCELERATOR resource.
    ///     An accelerator provides the user with access to an application's command set.
    /// </summary>
    public class AcceleratorResource : Resource {
        private List<Accelerator> _accelerators = new List<Accelerator>();

        /// <summary>
        ///     A new accelerator resource.
        /// </summary>
        public AcceleratorResource()
            : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(ResourceTypes.RT_ACCELERATOR), null, ResourceUtil.NEUTRALLANGID, 0) {
        }

        /// <summary>
        ///     An existing accelerator resource.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResource">Resource ID.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="language">Language ID.</param>
        /// <param name="size">Resource size.</param>
        public AcceleratorResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, UInt16 language, int size)
            : base(hModule, hResource, type, name, language, size) {
        }

        /// <summary>
        ///     Accelerator keys.
        /// </summary>
        public List<Accelerator> Accelerators {
            get {
                return _accelerators;
            }
            set {
                _accelerators = value;
            }
        }

        /// <summary>
        ///     Read the accelerators table.
        /// </summary>
        /// <param name="hModule">Handle to a module.</param>
        /// <param name="lpRes">Pointer to the beginning of the accelerator table.</param>
        /// <returns>Address of the end of the accelerator table.</returns>
        internal override IntPtr Read(IntPtr hModule, IntPtr lpRes) {
            var count = _size/Marshal.SizeOf(typeof (Accel));
            for (var i = 0; i < count; i++) {
                var accelerator = new Accelerator();
                lpRes = accelerator.Read(lpRes);
                _accelerators.Add(accelerator);
            }
            return lpRes;
        }

        internal override void Write(BinaryWriter w) {
            foreach (var accelerator in _accelerators) {
                accelerator.Write(w);
            }
        }

        /// <summary>
        ///     String representation of the accelerators resource.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} ACCELERATORS", Name));
            sb.AppendLine("BEGIN");
            foreach (var accelerator in _accelerators) {
                sb.AppendLine(string.Format(" {0}", accelerator));
            }
            sb.AppendLine("END");
            return sb.ToString();
        }
    }
}