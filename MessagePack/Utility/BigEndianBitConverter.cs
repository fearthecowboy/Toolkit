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

namespace FearTheCowboy.MessagePack.Utility {
    using System;

    internal static class BigEndianBitConverter {
        public static Int16 ToInt16(byte[] bytes, int index) {
            return (Int16)((((Int16)bytes[index++] << 8)) | bytes[index]);
        }

        public static Int32 ToInt32(byte[] bytes, int index) {
            return (bytes[index++] << 24) | (bytes[index++] << 16) | (bytes[index++] << 8) | bytes[index];
        }

        public static Int64 ToInt64(byte[] bytes, int index) {
            return ((Int64)bytes[index++] << 56) | ((Int64)bytes[index++] << 48) | ((Int64)bytes[index++] << 40) | ((Int64)bytes[index++] << 32) | ((Int64)bytes[index++] << 24) | ((Int64)bytes[index++] << 16) |
                   ((Int64)bytes[index++] << 8) | (Int64)bytes[index];
        }

        public static UInt16 ToUInt16(byte[] bytes, int index) {
            return (UInt16)(((UInt16)bytes[index++] << 8) | (UInt16)bytes[index]);
        }

        public static UInt32 ToUInt32(byte[] bytes, int index) {
            return (UInt32)(((UInt32)bytes[index++] << 24) | ((UInt32)bytes[index++] << 16) | ((UInt32)bytes[index++] << 8) | (UInt32)bytes[index]);
        }

        public static UInt64 ToUInt64(byte[] bytes, int index) {
            return ((UInt64)bytes[index++] << 56) | ((UInt64)bytes[index++] << 48) | ((UInt64)bytes[index++] << 40) | ((UInt64)bytes[index++] << 32) | ((UInt64)bytes[index++] << 24) | ((UInt64)bytes[index++] << 16) |
                   ((UInt64)bytes[index++] << 8) |
                   (UInt64)bytes[index];
        }

        public static float ToSingle(byte[] bytes, int index) {
            return BitConverter.ToSingle(new[] {bytes[index + 3], bytes[index + 2], bytes[index + 1], bytes[index]}, 0);
        }

        public static double ToDouble(byte[] bytes, int index) {
            return BitConverter.ToDouble(new[] {bytes[index + 7], bytes[index + 6], bytes[index + 5], bytes[index + 4], bytes[index + 3], bytes[index + 2], bytes[index + 1], bytes[index]}, 0);
        }
    }
}