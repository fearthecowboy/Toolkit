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

    public class Int8 : Message<sbyte> {
        internal Int8(MessageBuffer buffer, int offset) : base(buffer, offset) {
        }

        // If the key is less than 0x7f, the VALUE is just the key
        // if the key is greater than 0xe0, the VALUE is the negative (Key-0xe0)
        // otherwise the next byte is the VALUE.
        public override sbyte Value =>
            Key <= Format.PositiveFixIntMax ? (sbyte)Key :
                Key >= Format.NegativeFixIntMin ? (sbyte)-((byte)Key - 0xe0) :
                    (sbyte)Buffer.Data[PayloadOffset];

        protected override int PayloadLength =>
            Key <= Format.PositiveFixIntMax || Key >= Format.NegativeFixIntMin ? 0 : 1;
    }
}