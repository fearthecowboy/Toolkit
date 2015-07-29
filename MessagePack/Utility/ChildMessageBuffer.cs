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
    using System.Threading.Tasks;

    public class ChildMessageBuffer : MessageBuffer {
        private readonly Message _parent;

        public ChildMessageBuffer(Message parent) {
            _parent = parent;
        }

        protected internal override byte[] Data => _parent.Buffer.Data;
        protected internal override int BaseOffset => _parent.Buffer.BaseOffset;

        protected internal override async Task<bool> Require(int bytes) {
            return await _parent.Buffer.Require(bytes);
        }
    }
}