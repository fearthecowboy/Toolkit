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
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    ///     A version resource.
    /// </summary>
    public abstract class Resource {
        /// <summary>
        ///     Loaded binary nodule.
        /// </summary>
        protected IntPtr _hModule = IntPtr.Zero;

        /// <summary>
        ///     Pointer to the resource.
        /// </summary>
        protected IntPtr _hResource = IntPtr.Zero;

        /// <summary>
        ///     Resource language.
        /// </summary>
        protected UInt16 _language;

        /// <summary>
        ///     Resource name.
        /// </summary>
        protected ResourceId _name;

        /// <summary>
        ///     Resource size.
        /// </summary>
        protected int _size;

        /// <summary>
        ///     Resource type.
        /// </summary>
        protected ResourceId _type;

        /// <summary>
        ///     A new resource.
        /// </summary>
        internal Resource() {
        }

        /// <summary>
        ///     A structured resource embedded in an executable module.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResource">Resource handle.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="language">Language ID.</param>
        /// <param name="size">Resource size.</param>
        internal Resource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, UInt16 language, int size) {
            _hModule = hModule;
            _type = type;
            _name = name;
            _language = language;
            _hResource = hResource;
            _size = size;

            LockAndReadResource(hModule, hResource);
        }

        /// <summary>
        ///     Version resource size in bytes.
        /// </summary>
        public int Size
        {
            get
            {
                return _size;
            }
        }

        /// <summary>
        ///     Language ID.
        /// </summary>
        public UInt16 Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
            }
        }

        /// <summary>
        ///     Resource type.
        /// </summary>
        public ResourceId Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        ///     String representation of the resource type.
        /// </summary>
        public string TypeName
        {
            get
            {
                return _type.TypeName;
            }
        }

        /// <summary>
        ///     Resource name.
        /// </summary>
        public ResourceId Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        ///     Lock and read the resource.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResource">Resource handle.</param>
        internal void LockAndReadResource(IntPtr hModule, IntPtr hResource) {
            if (hResource == IntPtr.Zero) {
                return;
            }

            var lpRes = Kernel32.LockResource(hResource);

            if (lpRes == IntPtr.Zero) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            Read(hModule, lpRes);
        }

        /// <summary>
        ///     Load a resource from an executable (.exe or .dll) file.
        /// </summary>
        /// <param name="filename">An executable (.exe or .dll) file.</param>
        public virtual void LoadFrom(string filename) {
            LoadFrom(filename, _type, _name, _language);
        }

        /// <summary>
        ///     Load a resource from an executable (.exe or .dll) file.
        /// </summary>
        /// <param name="filename">An executable (.exe or .dll) file.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="lang">Resource language.</param>
        internal void LoadFrom(string filename, ResourceId type, ResourceId name, UInt16 lang) {
            var hModule = IntPtr.Zero;

            try {
                hModule = Kernel32.LoadLibraryEx(filename, IntPtr.Zero, Kernel32.Constants.DONT_RESOLVE_DLL_REFERENCES | Kernel32.Constants.LOAD_LIBRARY_AS_DATAFILE);

                LoadFrom(hModule, type, name, lang);
            } finally {
                if (hModule != IntPtr.Zero) {
                    Kernel32.FreeLibrary(hModule);
                }
            }
        }

        /// <summary>
        ///     Load a resource from an executable (.exe or .dll) module.
        /// </summary>
        /// <param name="hModule">An executable (.exe or .dll) module.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="lang">Resource language.</param>
        internal void LoadFrom(IntPtr hModule, ResourceId type, ResourceId name, UInt16 lang) {
            if (IntPtr.Zero == hModule) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var hRes = Kernel32.FindResourceEx(hModule, type.Id, name.Id, lang);
            if (IntPtr.Zero == hRes) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var hGlobal = Kernel32.LoadResource(hModule, hRes);
            if (IntPtr.Zero == hGlobal) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var lpRes = Kernel32.LockResource(hGlobal);

            if (lpRes == IntPtr.Zero) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            _size = Kernel32.SizeofResource(hModule, hRes);
            if (_size <= 0) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            _type = type;
            _name = name;
            _language = lang;

            Read(hModule, lpRes);
        }

        /// <summary>
        ///     Read a resource from a previously loaded module.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="lpRes">Pointer to the beginning of the resource.</param>
        /// <returns>Pointer to the end of the resource.</returns>
        internal abstract IntPtr Read(IntPtr hModule, IntPtr lpRes);

        /// <summary>
        ///     Write the resource to a memory stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        internal abstract void Write(BinaryWriter w);

        /// <summary>
        ///     Return resource data.
        /// </summary>
        /// <returns>Resource data.</returns>
        public byte[] WriteAndGetBytes() {
            var ms = new MemoryStream();
            var w = new BinaryWriter(ms, Encoding.Default);
            Write(w);
            w.Close();
            return ms.ToArray();
        }

        /// <summary>
        ///     Save a resource.
        /// </summary>
        /// <param name="filename">Name of an executable file (.exe or .dll).</param>
        public virtual void SaveTo(string filename) {
            SaveTo(filename, _type, _name, _language);
        }

        /// <summary>
        ///     Save a resource to an executable (.exe or .dll) file.
        /// </summary>
        /// <param name="filename">Path to an executable file.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="langid">Language id.</param>
        internal void SaveTo(string filename, ResourceId type, ResourceId name, UInt16 langid) {
            var data = WriteAndGetBytes();
            SaveTo(filename, type, name, langid, data);
        }

        /// <summary>
        ///     Delete a resource from an executable (.exe or .dll) file.
        /// </summary>
        /// <param name="filename">Path to an executable file.</param>
        public virtual void DeleteFrom(string filename) {
            Delete(filename, _type, _name, _language);
        }

        /// <summary>
        ///     Delete a resource from an executable (.exe or .dll) file.
        /// </summary>
        /// <param name="filename">Path to an executable file.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="lang">Resource language.</param>
        public static void Delete(string filename, ResourceId type, ResourceId name, UInt16 lang) {
            SaveTo(filename, type, name, lang, null);
        }

        /// <summary>
        ///     Save a resource to an executable (.exe or .dll) file.
        /// </summary>
        /// <param name="filename">Path to an executable file.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="lang">Resource language.</param>
        /// <param name="data">Resource data.</param>
        internal static void SaveTo(string filename, ResourceId type, ResourceId name, UInt16 lang, byte[] data) {
            var h = Kernel32.BeginUpdateResource(filename, false);

            if (h == IntPtr.Zero) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (!Kernel32.UpdateResource(h, type.Id, name.Id, lang, data, (data == null ? 0 : (uint)data.Length))) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (!Kernel32.EndUpdateResource(h, false)) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}