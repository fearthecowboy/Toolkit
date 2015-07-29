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

    public class UInt16 : Message<ushort> {
        internal UInt16(MessageBuffer buffer, int offset) : base(buffer, offset) {
        }

        public override ushort Value => BigEndianBitConverter.ToUInt16(Buffer.Data, PayloadOffset);
        protected override int PayloadLength => 2;
    }
}