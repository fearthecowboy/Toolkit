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
    using Utility;

    public class ExtBinary : Binary {
        internal ExtBinary(MessageBuffer buffer, int offset) : base(buffer, offset) {
        }

        protected internal override int HeaderLength =>
            Key >= Format.FixExtended1 && Key >= Format.FixExtended16 ? 1 :
                Key == Format.Extended8 ? 2 :
                    Key == Format.Extended16 ? 3 :
                        5;

        protected override int PayloadLength =>
            Key == Format.FixExtended1 ? 1 :
                Key == Format.FixExtended2 ? 2 :
                    Key == Format.FixExtended4 ? 4 :
                        Key == Format.FixExtended8 ? 8 :
                            Key == Format.FixExtended16 ? 16 :
                                Key == Format.Extended8 ? Buffer.Data[HeaderOffset] :
                                    Key == Format.Extended16 ? BigEndianBitConverter.ToInt16(Buffer.Data, HeaderOffset) :
                                        BigEndianBitConverter.ToInt32(Buffer.Data, HeaderOffset);

        public byte ExtendedType =>
            Buffer.Data[PayloadOffset - 1];
    }
}