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
    using System.Runtime.InteropServices;
    using Enumerations;

    /// <summary>
    ///     A resource Id.
    ///     There're two types of resource Ids, reserved integer numbers (eg. RT_ICON) and custom string names (eg. "CUSTOM").
    /// </summary>
    public class ResourceId {
        private IntPtr _name = IntPtr.Zero;

        /// <summary>
        ///     A resource identifier.
        /// </summary>
        /// <param name="value">A integer or string resource id.</param>
        public ResourceId(IntPtr value) {
            Id = value;
        }

        /// <summary>
        ///     A resource identifier.
        /// </summary>
        /// <param name="value">A integer resource id.</param>
        public ResourceId(uint value) {
            Id = new IntPtr(value);
        }

        /// <summary>
        ///     A well-known resource-type identifier.
        /// </summary>
        /// <param name="value">A well known resource type.</param>
        public ResourceId(ResourceTypes value) {
            Id = (IntPtr)value;
        }

        /// <summary>
        ///     A custom resource identifier.
        /// </summary>
        /// <param name="value"></param>
        public ResourceId(string value) {
            Name = value;
        }

        /// <summary>
        ///     Resource Id.
        /// </summary>
        /// <remarks>
        ///     If the resource Id is a string, it will be copied.
        /// </remarks>
        public IntPtr Id {get {return _name;} set {_name = IsIntResource(value) ? value : Marshal.StringToHGlobalUni(Marshal.PtrToStringUni(value));}}

        /// <summary>
        ///     String representation of a resource type name.
        /// </summary>
        public string TypeName {get {return IsIntResource() ? ResourceType.ToString() : Name;}}

        /// <summary>
        ///     An enumerated resource type for built-in resource types only.
        /// </summary>
        public ResourceTypes ResourceType {
            get {
                if (IsIntResource()) {
                    return (ResourceTypes)_name;
                }
                return ResourceTypes.RT_OTHER;
                // throw new InvalidCastException(string.Format("Resource {0} is not of built-in type.", Name));
            }
            set {_name = (IntPtr)value;}
        }

        /// <summary>
        ///     Resource Id in a string format.
        /// </summary>
        public string Name {get {return IsIntResource() ? _name.ToString() : Marshal.PtrToStringUni(_name);} set {_name = Marshal.StringToHGlobalUni(value);}}

        /// <summary>
        ///     Returns true if the resource is an integer resource.
        /// </summary>
        public bool IsIntResource() {
            return IsIntResource(_name);
        }

        /// <summary>
        ///     Returns true if the resource is an integer resource.
        /// </summary>
        /// <param name="value">Resource pointer.</param>
        internal static bool IsIntResource(IntPtr value) {
            return (uint)value <= UInt16.MaxValue;
        }

        /// <summary>
        ///     String representation of the resource Id.
        /// </summary>
        /// <returns>Resource name.</returns>
        public override string ToString() {
            return Name;
        }

        /// <summary>
        ///     Resource Id hash code.
        ///     Resource Ids of the same type have the same hash code.
        /// </summary>
        /// <returns>Resource Id.</returns>
        public override int GetHashCode() {
            return IsIntResource() ? Id.ToInt32() : Name.GetHashCode();
        }

        /// <summary>
        ///     Compares two resource Ids by value.
        /// </summary>
        /// <param name="obj">Resource Id.</param>
        /// <returns>True if both resource Ids represent the same resource.</returns>
        public override bool Equals(object obj) {
            if (obj is ResourceId && obj == this) {
                return true;
            }

            if (obj is ResourceId && (obj as ResourceId).GetHashCode() == GetHashCode()) {
                return true;
            }

            return false;
        }
    }
}