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

namespace FearTheCowboy.MessagePack.Types {
    using System;
    using System.Linq;
    using System.Text;
    using Utility;

    public class Binary : Message<byte[]> {
        protected byte[] _value;

        internal Binary(MessageBuffer buffer, int offset) : base(buffer, offset) {
        }

        protected internal override int HeaderLength =>
            Key == Format.Binary8 ? 1 :
                Key == Format.Binary16 ? 2 :
                    4;

        protected override int PayloadLength =>
            Key == Format.Binary8 ? Buffer.Data[HeaderOffset] :
                Key == Format.Binary16 ? BigEndianBitConverter.ToInt16(Buffer.Data, HeaderOffset) :
                    BigEndianBitConverter.ToInt32(Buffer.Data, HeaderOffset);

        public override byte[] Value => _value ?? (_value = Buffer.Data.Copy(PayloadOffset, PayloadLength));

        public override string ToString() {
            var subset = Value.Take(20).ToArray();
            return BitConverter.ToString(subset).Replace("-", " ") + " " + Encoding.UTF8.GetString(subset);
        }
    }
}