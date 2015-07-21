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
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using Enumerations;

    /// <summary>
    ///     Resource info manager.
    /// </summary>
    public class ResourceInfo : IEnumerable<Resource>, IDisposable {
        private IntPtr _hModule = IntPtr.Zero;
        private Exception _innerException;
        private List<ResourceId> _resourceTypes;
        private IDictionary<ResourceId, List<Resource>> _resources;

        /// <summary>
        ///     A dictionary of resources, the key is the resource type, eg. "REGISTRY" or "16" (version).
        /// </summary>
        public IDictionary<ResourceId, List<Resource>> Resources {
            get {
                return _resources;
            }
        }

        /// <summary>
        ///     A shortcut for available resource types.
        /// </summary>
        public List<ResourceId> ResourceTypes {
            get {
                return _resourceTypes;
            }
        }

        /// <summary>
        ///     A collection of resources.
        /// </summary>
        /// <param name="type">Resource type.</param>
        /// <returns>A collection of resources of a given type.</returns>
        public List<Resource> this[ResourceTypes type] {
            get {
                return _resources[new ResourceId(type)];
            }
            set {
                _resources[new ResourceId(type)] = value;
            }
        }

        /// <summary>
        ///     A collection of resources.
        /// </summary>
        /// <param name="type">Resource type.</param>
        /// <returns>A collection of resources of a given type.</returns>
        public List<Resource> this[string type] {
            get {
                return _resources[new ResourceId(type)];
            }
            set {
                _resources[new ResourceId(type)] = value;
            }
        }

        #region IDisposable Members

        /// <summary>
        ///     Dispose resource info object.
        /// </summary>
        public void Dispose() {
            Unload();
        }

        #endregion

        #region IEnumerable<Resource> Members

        /// <summary>
        ///     Enumerates all resources within this resource info collection.
        /// </summary>
        /// <returns>Resources enumerator.</returns>
        public IEnumerator<Resource> GetEnumerator() {
            var resourceTypesEnumerator = _resources.GetEnumerator();
            while (resourceTypesEnumerator.MoveNext()) {
                var resourceEnumerator = resourceTypesEnumerator.Current.Value.GetEnumerator();
                while (resourceEnumerator.MoveNext()) {
                    yield return resourceEnumerator.Current;
                }
            }
        }

        /// <summary>
        ///     Enumerates all resources within this resource info collection.
        /// </summary>
        /// <returns>Resources enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        #endregion

        /// <summary>
        ///     Unload the previously loaded module.
        /// </summary>
        public void Unload() {
            if (_hModule != IntPtr.Zero) {
                Kernel32.FreeLibrary(_hModule);
                _hModule = IntPtr.Zero;
            }

            _innerException = null;
        }

        /// <summary>
        ///     Load an executable or a DLL and read its resources.
        /// </summary>
        /// <param name="filename">Source filename.</param>
        public void Load(string filename) {
            Unload();

            _resourceTypes = new List<ResourceId>();
            _resources = new Dictionary<ResourceId, List<Resource>>();

            // load DLL
            _hModule = Kernel32.LoadLibraryEx(filename, IntPtr.Zero, Kernel32.Constants.DONT_RESOLVE_DLL_REFERENCES | Kernel32.Constants.LOAD_LIBRARY_AS_DATAFILE);

            if (IntPtr.Zero == _hModule) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            try {
                // enumerate resource types
                // for each type, enumerate resource names
                // for each name, enumerate resource languages
                // for each resource language, enumerate actual resources
                if (!Kernel32.EnumResourceTypes(_hModule, EnumResourceTypesImpl, IntPtr.Zero)) {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            } catch (Exception ex) {
                throw new LoadException(string.Format("Error loading '{0}'.", filename), _innerException, ex);
            }
        }

        /// <summary>
        ///     Enumerate resource types.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="lpszType">Resource type.</param>
        /// <param name="lParam">Additional parameter.</param>
        /// <returns>TRUE if successful.</returns>
        private bool EnumResourceTypesImpl(IntPtr hModule, IntPtr lpszType, IntPtr lParam) {
            var type = new ResourceId(lpszType);
            _resourceTypes.Add(type);

            // enumerate resource names
            if (!Kernel32.EnumResourceNames(hModule, lpszType, EnumResourceNamesImpl, IntPtr.Zero)) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return true;
        }

        /// <summary>
        ///     Enumerate resource names within a resource by type
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="lpszType">Resource type.</param>
        /// <param name="lpszName">Resource name.</param>
        /// <param name="lParam">Additional parameter.</param>
        /// <returns>TRUE if successful.</returns>
        private bool EnumResourceNamesImpl(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, IntPtr lParam) {
            if (!Kernel32.EnumResourceLanguages(hModule, lpszType, lpszName, EnumResourceLanguages, IntPtr.Zero)) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return true;
        }

        /// <summary>
        ///     Create a resource of a given type.
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="hResourceGlobal">Pointer to the resource in memory.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="wIDLanguage">Language ID.</param>
        /// <param name="size">Size of resource.</param>
        /// <returns>A specialized or a generic resource.</returns>
        protected Resource CreateResource(IntPtr hModule, IntPtr hResourceGlobal, ResourceId type, ResourceId name, UInt16 wIDLanguage, int size) {
            if (type.IsIntResource()) {
                switch (type.ResourceType) {
                    case Enumerations.ResourceTypes.RT_VERSION:
                        return new VersionResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
                    case Enumerations.ResourceTypes.RT_GROUP_CURSOR:
                        return new CursorDirectoryResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
                    case Enumerations.ResourceTypes.RT_GROUP_ICON:
                        return new IconDirectoryResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
                    case Enumerations.ResourceTypes.RT_MANIFEST:
                        return new ManifestResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
                    case Enumerations.ResourceTypes.RT_BITMAP:
                        return new BitmapResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
                    case Enumerations.ResourceTypes.RT_MENU:
                        return new MenuResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
                    case Enumerations.ResourceTypes.RT_DIALOG:
                        return new DialogResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
                    case Enumerations.ResourceTypes.RT_STRING:
                        return new StringResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
                    case Enumerations.ResourceTypes.RT_FONTDIR:
                        return new FontDirectoryResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
                    case Enumerations.ResourceTypes.RT_FONT:
                        return new FontResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
                    case Enumerations.ResourceTypes.RT_ACCELERATOR:
                        return new AcceleratorResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
                }
            }

            return new GenericResource(hModule, hResourceGlobal, type, name, wIDLanguage, size);
        }

        /// <summary>
        ///     Enumerate resource languages within a resource by name
        /// </summary>
        /// <param name="hModule">Module handle.</param>
        /// <param name="lpszType">Resource type.</param>
        /// <param name="lpszName">Resource name.</param>
        /// <param name="wIDLanguage">Language ID.</param>
        /// <param name="lParam">Additional parameter.</param>
        /// <returns>TRUE if successful.</returns>
        private bool EnumResourceLanguages(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, UInt16 wIDLanguage, IntPtr lParam) {
            List<Resource> resources = null;
            var type = new ResourceId(lpszType);
            if (!_resources.TryGetValue(type, out resources)) {
                resources = new List<Resource>();
                _resources[type] = resources;
            }

            var name = new ResourceId(lpszName);
            var hResource = Kernel32.FindResourceEx(hModule, lpszType, lpszName, wIDLanguage);
            var hResourceGlobal = Kernel32.LoadResource(hModule, hResource);
            var size = Kernel32.SizeofResource(hModule, hResource);

            try {
                resources.Add(CreateResource(hModule, hResourceGlobal, type, name, wIDLanguage, size));
            } catch (Exception ex) {
                _innerException = new Exception(string.Format("Error loading resource '{0}' {1} ({2}).", name, type.TypeName, wIDLanguage), ex);
                throw ex;
            }

            return true;
        }

        /// <summary>
        ///     Save resource to a file.
        /// </summary>
        /// <param name="filename">Target filename.</param>
        public void Save(string filename) {
            throw new NotImplementedException();
        }
    }
}