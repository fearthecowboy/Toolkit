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
    using System.Text;
    using Utility;

    public class String : Message<string> {
        private string _value;

        internal String(MessageBuffer buffer, int offset) : base(buffer, offset) {
        }

        protected internal override int HeaderLength =>
            Key == Format.String8 ? 1 :
                Key == Format.String16 ? 2 :
                    Key == Format.String32 ? 4 :
                        0;

        protected override int PayloadLength =>
            Key == Format.String8 ? Buffer.Data[HeaderOffset] :
                Key == Format.String16 ? BigEndianBitConverter.ToInt16(Buffer.Data, HeaderOffset) :
                    Key == Format.String32 ? BigEndianBitConverter.ToInt32(Buffer.Data, HeaderOffset) :
                        (byte)Key - (byte)Format.FixStringMin;

        public override string Value => _value ?? (_value = Encoding.UTF8.GetString(Buffer.Data, PayloadOffset, PayloadLength));
    }
}