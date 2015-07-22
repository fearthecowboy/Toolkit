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
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;
    using Enumerations;

    /// <summary>
    ///     An embedded SxS manifest.
    /// </summary>
    public class ManifestResource : Resource {
        private static byte[] utf8_bom = {
            0xef, 0xbb, 0xbf
        };

        private byte[] _data;
        private XmlDocument _manifest;

        /// <summary>
        ///     An existing embedded manifest resource.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResource">Resource ID.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="language">Language ID.</param>
        /// <param name="size">Resource size.</param>
        public ManifestResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, UInt16 language, int size)
            : base(hModule, hResource, type, name, language, size) {
        }

        /// <summary>
        ///     A new executable CreateProcess manifest.
        /// </summary>
        public ManifestResource() : this(ManifestType.CreateProcess) {
        }

        /// <summary>
        ///     A new executable manifest.
        /// </summary>
        /// <param name="manifestType">Manifest type.</param>
        public ManifestResource(ManifestType manifestType)
            : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(ResourceTypes.RT_MANIFEST), new ResourceId((uint)manifestType), Kernel32.Constants.LANG_NEUTRAL, 0
                ) {
            _manifest = new XmlDocument();
            _manifest.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
                              "<assembly xmlns=\"urn:schemas-microsoft-com:asm.v1\" manifestVersion=\"1.0\" />");
            _size = Encoding.UTF8.GetBytes(_manifest.OuterXml).Length;
        }

        /// <summary>
        ///     Embedded XML manifest.
        /// </summary>
        public XmlDocument Manifest
        {
            get
            {
                if (_manifest == null && _data != null) {
                    var unicodeBOM = (_data.Length >= 3 && _data[0] == utf8_bom[0] && _data[1] == utf8_bom[1] && _data[2] == utf8_bom[2]);

                    var manifestXml = Encoding.UTF8.GetString(_data, unicodeBOM ? 3 : 0, unicodeBOM ? _data.Length - 3 : _data.Length);

                    _manifest = new XmlDocument();
                    _manifest.LoadXml(manifestXml);
                }

                return _manifest;
            }
            set
            {
                _manifest = value;
                _data = null;
                _size = PrettyManifestXml().Length;
            }
        }

        /// <summary>
        ///     Embedded XML manifest.
        /// </summary>
        public string ManifestText
        {
            get
            {
                return Manifest.OuterXml;
            }
            set
            {
                _manifest = new XmlDocument();
                _manifest.LoadXml(value);
                _data = null;
                _size = PrettyManifestXml().Length;
            }
        }

        /// <summary>
        ///     Manifest type.
        /// </summary>
        public ManifestType ManifestType
        {
            get
            {
                return (ManifestType)_name.Id;
            }
            set
            {
                _name = new ResourceId((IntPtr)value);
            }
        }

        /// <summary>
        ///     Read the resource.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="lpRes">Pointer to the beginning of a resource.</param>
        /// <returns>Pointer to the end of the resource.</returns>
        internal override IntPtr Read(IntPtr hModule, IntPtr lpRes) {
            if (_size > 0) {
                _manifest = null;
                _data = new byte[_size];
                Marshal.Copy(lpRes, _data, 0, _data.Length);
            }

            return new IntPtr(lpRes.ToInt32() + _size);
        }

        /// <summary>
        ///     Write the resource to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal override void Write(BinaryWriter w) {
            if (_manifest != null) {
                w.Write(PrettyManifestXml());
            } else if (_data != null) {
                w.Write(_data);
            }
        }

        // GS02: Make the XML look pretty ;)
        private byte[] PrettyManifestXml() {
            if (_manifest != null) {
                using (var memoryStream = new MemoryStream()) {
                    using (var xw = XmlWriter.Create(memoryStream, new XmlWriterSettings {
                        ConformanceLevel = ConformanceLevel.Document,
                        Encoding = new UTF8Encoding(false),
                        OmitXmlDeclaration = false,
                        Indent = true
                    })) {
                        _manifest.WriteTo(xw);
                    }
                    return memoryStream.GetBuffer();
                }
            }
            return _data ?? new byte[0];
        }

        /// <summary>
        ///     Load a manifest resource from an executable file.
        /// </summary>
        /// <param name="filename">Name of an executable file (.exe or .dll).</param>
        /// <param name="manifestType">Manifest resource type.</param>
        public void LoadFrom(string filename, ManifestType manifestType) {
            base.LoadFrom(filename, new ResourceId(ResourceTypes.RT_MANIFEST), new ResourceId((uint)manifestType), Kernel32.Constants.LANG_NEUTRAL);
        }
    }
}