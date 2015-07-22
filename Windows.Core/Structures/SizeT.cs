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

namespace FearTheCowboy.Windows.Structures {
    using System;

    public struct SizeT {
        private UIntPtr _value;

        public SizeT(ulong size) {
            _value = new UIntPtr(size);
        }

        public SizeT(uint size) {
            _value = new UIntPtr(size);
        }

        public static implicit operator ulong(SizeT size) {
            return (ulong)size._value;
        }

        public static implicit operator uint(SizeT size) {
            return (uint)size._value;
        }

        public static implicit operator SizeT(ulong size) {
            return new SizeT(size);
        }

        public static implicit operator SizeT(uint size) {
            return new SizeT(size);
        }

        public static bool operator ==(SizeT a, SizeT b) {
            return a._value == b._value;
        }

        public static bool operator !=(SizeT a, SizeT b) {
            return a._value == b._value;
        }

        public static bool operator <(SizeT a, SizeT b) {
            return (ulong)a._value < (ulong)b._value;
        }

        public static bool operator >(SizeT a, SizeT b) {
            return (ulong)a._value > (ulong)b._value;
        }

        public static bool operator <=(SizeT a, SizeT b) {
            return (ulong)a._value <= (ulong)b._value;
        }

        public static bool operator >=(SizeT a, SizeT b) {
            return (ulong)a._value >= (ulong)b._value;
        }

        public bool Equals(SizeT other) {
            return other._value == _value;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (obj.GetType() != typeof (SizeT)) {
                return false;
            }
            return Equals((SizeT)obj);
        }

        public override int GetHashCode() {
            return _value.GetHashCode();
        }
    }
}