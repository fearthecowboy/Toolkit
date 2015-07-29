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

    public class Map : ContainerMessage<IDictionary<Message, Message>>, IDictionary<Message, Message> {
        private Dictionary<Message, Message> _dictionary;

        internal Map(MessageBuffer buffer, int offset) : base(buffer, offset) {
        }

        protected internal override int HeaderLength =>
            Key == Format.Map16 ? 2 :
                Key == Format.Map32 ? 4 :
                    0;

        private int ElementCount =>
            Key == Format.Map16 ? BigEndianBitConverter.ToInt16(Buffer.Data, HeaderOffset) :
                Key == Format.Map32 ? BigEndianBitConverter.ToInt32(Buffer.Data, HeaderOffset) :
                    (byte)Key - (byte)Format.FixMapMin;

        protected override int PayloadLength {
            get {
                if (_dictionary == null) {
                    throw new Exception("Can't get length before reading.");
                }
                return _dictionary.Sum(each => each.Key.MessageLength + each.Value.MessageLength);
            }
        }

        public override IDictionary<Message, Message> Value => this;

        public IEnumerator<KeyValuePair<Message, Message>> GetEnumerator() {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public bool Contains(KeyValuePair<Message, Message> item) {
            return _dictionary.Contains(item);
        }

        public int Count => ElementCount;
        public bool IsReadOnly => true;

        public bool ContainsKey(Message key) {
            return _dictionary.ContainsKey(key);
        }

        public bool TryGetValue(Message key, out Message value) {
            return _dictionary.TryGetValue(key, out value);
        }

        public Message this[Message key] {get {return _dictionary[key];} set {throw new NotImplementedException();}}
        public ICollection<Message> Keys => _dictionary.Keys;
        public ICollection<Message> Values => _dictionary.Values;

        protected internal override async Task GetChildren() {
            if (_dictionary != null) {
                return;
            }

            _dictionary = new Dictionary<Message, Message>();
            var offset = HeaderLength + KeyLength + Offset;
            var elementCount = ElementCount;
            var count = 0;
            var buffer = new ChildMessageBuffer(this);

            for (var i = 0; i < elementCount; i++) {
                var key = await Buffer.CreateMessage(buffer, offset + count);
                count += key.MessageLength;
                var value = await Buffer.CreateMessage(buffer, offset + count);
                count += value.MessageLength;
                _dictionary.Add(key, value);
            }
        }

        public override string ToString() {
            return $"Map [{_dictionary.Keys.Count}] => {{{_dictionary.Keys.Aggregate("", (current, i) => current + "\r\n  " + i.ToString() + " : " + (_dictionary[i].ToString().Replace("\r\n", "\r\n  ")))}\r\n}}";
        }

        #region NotImplemented

        public void Add(KeyValuePair<Message, Message> item) {
            throw new NotImplementedException();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<Message, Message>[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<Message, Message> item) {
            throw new NotImplementedException();
        }

        public void Add(Message key, Message value) {
            throw new NotImplementedException();
        }

        public bool Remove(Message key) {
            throw new NotImplementedException();
        }

        #endregion

        internal override bool DeserializeToProperty(object instance, PersistablePropertyInformation property) {
            // is the target type a dictionary of some kind?
            if (property.PersistableInfo.PersistableCategory == PersistableCategory.Dictionary || property.PersistableInfo.PersistableCategory == PersistableCategory.Other) {
                // the target object is something we can use.
                var target = property.GetValue(instance);

                if (target == null) {
                    // create the target object if we can
                    target = property.CreateInstance();
                    if (target == null) {
                        return false;
                    }
                    // and set the actual property 
                    property.SetValue(instance, target);
                }

                // and fill it in.
                return DeserializeInto(ref target);
            }

            // if the property is a string, let's just set the value to the ToString of the map.
            // it's not really correct, but will help us figure out what to do next.
            if (property.PersistableInfo.PersistableCategory == PersistableCategory.String) {
                property.SetValue(instance, ToString());
            }

            return false;
        }

        public override bool DeserializeInto(ref object target) {
            if(target == null) {
                return false;
            }
            var success = false;
            var pi = target.GetType().GetPersistableInfo();

            if (pi.PersistableCategory == PersistableCategory.Dictionary) {
                var dict = (IDictionary)target;
                
                // it's a dictionary 
                // do a key :: key mapping 
                foreach (var key in Keys) {
                    object k = key.DynamicValue;
                    if (pi.DictionaryKeyType.IsInstanceOfType(k)) {
                        var v = this[key];
                        if (pi.DictionaryValueType.IsInstanceOfType(v)) {
                            dict.Add( k,v);
                            success = true;
                            continue;
                        }

                        // the target type isn't compatible?
                        // can we do a DeserializeTo() on the target 
                        var valueInstance = Activator.CreateInstance(pi.DictionaryValueType);
                        if (valueInstance != null) {
                            if (v.DeserializeInto(ref valueInstance)) {
                                dict.Add(k, valueInstance);
                                success = true;
                                continue;
                            }
                        }
                        // we couldn't create a value for the target. 
                        // uh?
                        Debug.WriteLine($"ERROR IN MAP DESERIALIZATION: Value not assignable-IDictionary:{pi.DictionaryKeyType.Name}/{pi.DictionaryValueType.Name} with value of type {v.GetType().Name} ");
                        continue;
                    }
                    Debug.WriteLine($"ERROR IN MAP DESERIALIZATION: Key not assignable-IDictionary:{pi.DictionaryKeyType.Name}/{pi.DictionaryValueType.Name} with key of type {key.GetType().Name} ");
                    // the key is not assignable... 
                    // uh?
                } 

                return success;
            }

            // it's an object.
            // loop thru the members in this object
            // and attempt to set the values on the target object
            // based on the keys here.
            
            var map = Keys.ToDictionary(each => each.ToString(),each => this[each],StringComparer.OrdinalIgnoreCase);
            
            var members = target.GetReadWriteMembers();
            foreach (var m in members) {
                if (map.ContainsKey(m.Name)) {
                    // see if the member can be set
                    var v = map[m.Name];
                    if (!map[m.Name].DeserializeToProperty(target, m)) {
                        Debug.WriteLine($"ERROR IN MAP DESERIALIZATION: Deserialization to object:{m.Name} with value of type {v.GetType().Name} ");
                    } else {
                        success = true;
                    }
                }
            }
            

            return success;
        }
    }
}