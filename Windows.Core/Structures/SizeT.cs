//-----------------------------------------------------------------------
// <copyright company="CoApp Project">
//     Copyright (c) 2010-2013 Garrett Serack and CoApp Contributors. 
//     Contributors can be discovered using the 'git log' command.
//     All rights reserved.
// </copyright>
// <license>
//     The software is licensed under the Apache 2.0 License (the "License")
//     You may not use the software except in compliance with the License. 
// </license>
//-----------------------------------------------------------------------

namespace Toolkit.Windows.Structures {
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