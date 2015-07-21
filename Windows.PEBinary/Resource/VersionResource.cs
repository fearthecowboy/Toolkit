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
    using Structures;

    /// <summary>
    ///     VS_VERSIONINFO
    ///     This structure depicts the organization of data in a file-version resource. It is the root structure
    ///     that contains all other file-version information structures.
    ///     http://msdn.microsoft.com/en-us/library/aa914916.aspx
    /// </summary>
    public class VersionResource : Resource {
        private FixedFileInfo _fixedfileinfo = new FixedFileInfo();
        private ResourceTableHeader _header = new ResourceTableHeader("VS_VERSION_INFO");
        private IDictionary<string, ResourceTableHeader> _resources = new Dictionary<string, ResourceTableHeader>();

        /// <summary>
        ///     An existing version resource.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResource">Resource ID.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="language">Language ID.</param>
        /// <param name="size">Resource size.</param>
        public VersionResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, UInt16 language, int size)
            : base(hModule, hResource, type, name, language, size) {
        }

        /// <summary>
        ///     A new language-netural version resource.
        /// </summary>
        public VersionResource()
            : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(ResourceTypes.RT_VERSION), new ResourceId(1), ResourceUtil.USENGLISHLANGID, 0) {
            _header.Header = new ResourceHeader(_fixedfileinfo.Size);
        }

        /// <summary>
        ///     The resource header.
        /// </summary>
        public ResourceTableHeader Header {
            get {
                return _header;
            }
        }

        /// <summary>
        ///     A dictionary of resource tables.
        /// </summary>
        public IDictionary<string, ResourceTableHeader> Resources {
            get {
                return _resources;
            }
        }

        /// <summary>
        ///     String representation of the file version.
        /// </summary>
        public string FileVersion {
            get {
                return _fixedfileinfo.FileVersion;
            }
            set {
                _fixedfileinfo.FileVersion = value;
            }
        }

        /// <summary>
        ///     String representation of the protect version.
        /// </summary>
        public string ProductVersion {
            get {
                return _fixedfileinfo.ProductVersion;
            }
            set {
                _fixedfileinfo.ProductVersion = value;
            }
        }

        /// <summary>
        ///     Returns an entry within this resource table.
        /// </summary>
        /// <param name="key">Entry key.</param>
        /// <returns>A resource table.</returns>
        public ResourceTableHeader this[string key] {
            get {
                return Resources[key];
            }
            set {
                Resources[key] = value;
            }
        }

        /// <summary>
        ///     Read a version resource from a previously loaded module.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="lpRes">Pointer to the beginning of the resource.</param>
        /// <returns>Pointer to the end of the resource.</returns>
        internal override IntPtr Read(IntPtr hModule, IntPtr lpRes) {
            _resources.Clear();

            var pFixedFileInfo = _header.Read(lpRes);

            if (_header.Header.wValueLength != 0) {
                _fixedfileinfo = new FixedFileInfo();
                _fixedfileinfo.Read(pFixedFileInfo);
            }

            var pChild = ResourceUtil.Align(pFixedFileInfo.ToInt32() + _header.Header.wValueLength);

            while (pChild.ToInt32() < (lpRes.ToInt32() + _header.Header.wLength)) {
                var rc = new ResourceTableHeader(pChild);
                switch (rc.Key) {
                    case "StringFileInfo":
                        var sr = new StringFileInfo(pChild);
                        rc = sr;
                        break;
                    default:
                        rc = new VarFileInfo(pChild);
                        break;
                }

                _resources.Add(rc.Key, rc);
                pChild = ResourceUtil.Align(pChild.ToInt32() + rc.Header.wLength);
            }

            return new IntPtr(lpRes.ToInt32() + _header.Header.wLength);
        }

        /// <summary>
        ///     Write this version resource to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal override void Write(BinaryWriter w) {
            var headerPos = w.BaseStream.Position;
            _header.Write(w);

            if (_fixedfileinfo != null) {
                _fixedfileinfo.Write(w);
            }

            var resourceEnum = _resources.GetEnumerator();
            while (resourceEnum.MoveNext()) {
                resourceEnum.Current.Value.Write(w);
            }

            ResourceUtil.WriteAt(w, w.BaseStream.Position - headerPos, headerPos);
        }

        /// <summary>
        ///     Return string representation of the version resource.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            var sb = new StringBuilder();
            if (_fixedfileinfo != null) {
                sb.Append(_fixedfileinfo);
            }
            sb.AppendLine("BEGIN");
            var resourceEnum = _resources.GetEnumerator();
            while (resourceEnum.MoveNext()) {
                sb.Append(resourceEnum.Current.Value.ToString(1));
            }
            sb.AppendLine("END");
            return sb.ToString();
        }
    }
}