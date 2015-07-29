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
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Persistence;
    using Utility;

    public class Array : ContainerMessage<IEnumerable<Message>>, IEnumerable<Message> {
        private Message[] _elements;

        internal Array(MessageBuffer buffer, int offset) : base(buffer, offset) {
        }

        protected override int PayloadLength {
            get {
                if (_elements == null) {
                    throw new Exception("Can't get length before reading.");
                }
                return _elements.Sum(each => each.MessageLength);
            }
        }

        protected internal override int HeaderLength =>
            Key == Format.Array16 ? 2 :
                Key == Format.Array32 ? 4 :
                    0;

        private int ElementCount =>
            Key == Format.Array16 ? BigEndianBitConverter.ToInt16(Buffer.Data, HeaderOffset) :
                Key == Format.Array32 ? BigEndianBitConverter.ToInt32(Buffer.Data, HeaderOffset) :
                    (byte)Key - (byte)Format.FixArrayMin;

        public override IEnumerable<Message> Value => this;

        public Message this[int index] {
            get {
                if (_elements.Length >= index) {
                    throw new ArgumentOutOfRangeException(nameof(index), $"Array contains {_elements.Length} elements, asked for {index}.");
                }
                return _elements[index];
            }
        }

        public int Length => _elements.Length;

        public IEnumerator<Message> GetEnumerator() {
            return ((IEnumerable<Message>)_elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        protected internal override async Task GetChildren() {
            if (_elements != null) {
                return;
            }

            _elements = new Message[ElementCount];
            var offset = HeaderLength + KeyLength + Offset;
            var count = 0;

            var messageBuffer = new ChildMessageBuffer(this);

            for (int i = 0; i < Length; i++) {
                var message = await Buffer.CreateMessage(messageBuffer, offset + count);
                count += message.MessageLength;
                _elements[i] = message;
            }
        }

        public override string ToString() {
            return $"Array [{_elements.Length}] => {{{_elements.Aggregate("", (current, i) => current + "\r\n  " + (i.ToString().Replace("\r\n", "\r\n  ")))}\r\n}}";
        }

        public override bool DeserializeInto(ref object instance) {
            if (instance == null) {
                return false;
            }
            var success = false;

            var pi = instance.GetType().GetPersistableInfo();
            if (pi.PersistableCategory == PersistableCategory.Array) {
                // it's an array, let's see if we can dump the elements into the array.
                return false;
            }

            if (pi.PersistableCategory == PersistableCategory.Enumerable) {
                // it's a collection of some sort. maybe this will work.
                return false;
            }

            if (pi.PersistableCategory == PersistableCategory.Other) {
                // it's an object of some kind.
                var members = pi.Type.GetReadWriteMembers();
                var max = Math.Min(members.Length, Length);
                for(var i = 0; i < max; i++) {
                    if (this[i].DeserializeToProperty(instance, members[i])) {
                        success = true;
                    } else {
                        Debug.WriteLine($"ERROR IN ARRAY DESERIALIZATION: Object could not assign member:{members[i].Name} with value of type {this[i].GetType().Name} ");
                    }
                }
            }

         
            return success;
        }
    }
}