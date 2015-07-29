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

namespace FearTheCowboy.MessagePack {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using Common.Persistence;
    using Types;
    using Utility;

    public abstract class Message {
        protected readonly int Offset;
        private object _dynamicValue;
        private int? _hashcode;
        protected internal MessageBuffer Buffer;

        protected Message(MessageBuffer buffer, int offset) {
            Buffer = buffer;
            Offset = offset;
        }

        internal Format Key => (Format)Buffer.Data[Buffer.BaseOffset + Offset];

        /// <summary>
        ///     The length of the key (Always just one byte in MsgPack)
        /// </summary>
        protected virtual int KeyLength => sizeof (Format);

        /// <summary>
        ///     The length of the bytes that 'describe' the 'data' of the Message
        /// </summary>
        protected internal virtual int HeaderLength => 0;

        /// <summary>
        ///     The length of the data itself in the Message.
        /// </summary>
        protected virtual int PayloadLength => 0;

        protected virtual int ContentLength => HeaderLength + PayloadLength;
        public virtual int MessageLength => KeyLength + ContentLength;
        protected virtual int HeaderOffset => Buffer.BaseOffset + Offset + KeyLength;
        protected virtual int PayloadOffset => HeaderOffset + HeaderLength;
        public object DynamicValue => _dynamicValue ?? (_dynamicValue = ((dynamic)this).Value);

        public static Message Unpack(byte[] bytes) {
            try {
                var buf = new StaticMessageBuffer(bytes, 0, bytes.Length);
                return buf.CreateMessage(buf, 0).Result;
            } catch (Exception e) {
                // not valid?
                var x = e.Message;
            }
            return null;
        }

        public static IMessageReader Unpack(Stream byteStream) {
            return new MessageStream(byteStream);
        }

        public byte[] GetBinaryMessage() {
            return Buffer.Data.Copy(Offset, MessageLength);
        }

        public override int GetHashCode() {
            return _hashcode ?? (_hashcode = (DynamicValue ?? "null").GetHashCode()) ?? 99;
        }

        public override string ToString() {
            return (DynamicValue ?? "null").ToString();
        }

        public virtual bool DeserializeInto(ref object instance) {
            return false;
        }

        internal virtual bool DeserializeToProperty(object instance, PersistablePropertyInformation property) {
            
            return false;
        }
    }

    public abstract class Message<T> : Message {
        protected Message(MessageBuffer buffer, int offset) : base(buffer, offset) {
        }

        public abstract T Value {get;}

        public override bool DeserializeInto(ref object instance) {
            if(instance.GetType().IsAssignableFrom(typeof(T))) {
                instance = Value;
                return true;
            }
            return false;
        }

        internal override bool DeserializeToProperty(object instance, PersistablePropertyInformation property) {
            if(property.Type.IsAssignableFrom(typeof(T)) ) {
                property.SetValue(instance,Value);
                return true;
            }
            return false;
        }
    }
}