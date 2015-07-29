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
    using Types;

    public abstract class MessageBuffer {
        protected internal abstract byte[] Data {get;}
        protected internal abstract int BaseOffset {get;}
        protected internal abstract Task<bool> Require(int bytes);

        internal async Task<Message> CreateMessage(MessageBuffer buffer, int offset) {
            await Require(offset + 1);
            var key = (Format)buffer.Data[buffer.BaseOffset + offset];

            /* fixed, single byte */
            switch (key) {
                case Format.Nil:
                    return new Nil(buffer, offset);

                case Format.Unused:
                    return new Unused(buffer, offset);

                case Format.False:
                case Format.True:
                    return new Boolean(buffer, offset);
            }

            /* fixed size message.  */
            var message = CreateFixedMessage(key, buffer, offset);
            if (message != null) {
                await buffer.Require(message.MessageLength);
                return message;
            }

            /* dynamic sized message */
            message = CreateDynamicMessage(key, buffer, offset);
            if (message != null) {
                // ensure we have enough bytes for header
                await buffer.Require(message.HeaderLength);

                // esure we have enough bytes for entire content
                await buffer.Require(message.MessageLength);
                return message;
            }

            /* Messages that contain more messages */
            var containermessage = CreateContainerMessage(key, buffer, offset);
            if (containermessage != null) {
                // ensure we have enough bytes for header
                await buffer.Require(containermessage.HeaderLength);

                // create the child messages 
                await containermessage.GetChildren();

                return containermessage;
            }

            return null;
        }

        private Message CreateDynamicMessage(Format key, MessageBuffer buffer, int offset) {
            switch (key) {
                case Format.Extended8:
                case Format.Extended16:
                case Format.Extended32:
                case Format.FixExtended1:
                case Format.FixExtended2:
                case Format.FixExtended4:
                case Format.FixExtended8:
                case Format.FixExtended16:
                    return new ExtBinary(buffer, offset);

                case Format.String8:
                case Format.String16:
                case Format.String32:
                    return new String(buffer, offset);

                case Format.Binary8:
                case Format.Binary16:
                case Format.Binary32:
                    return new Binary(buffer, offset);
            }
            if (key >= Format.FixStringMin && key <= Format.FixStringMax) {
                return new String(buffer, offset);
            }

            return null;
        }

        private ContainerMessage CreateContainerMessage(Format key, MessageBuffer buffer, int offset) {
            switch (key) {
                case Format.Array16:
                case Format.Array32:
                    return new Array(buffer, offset);

                case Format.Map32:
                case Format.Map16:
                    return new Map(buffer, offset);

                default:
                    if (key >= Format.FixArrayMin && key <= Format.FixArrayMax) {
                        return new Array(buffer, offset);
                    }

                    if (key >= Format.FixMapMin && key <= Format.FixMapMax) {
                        return new Map(buffer, offset);
                    }
                    break;
            }
            return null;
        }

        private Message CreateFixedMessage(Format key, MessageBuffer buffer, int offset) {
            switch (key) {
                case Format.Float32:
                    return new Float32(buffer, offset);

                case Format.Float64:
                    return new Float64(buffer, offset);

                case Format.UInt8:
                    return new UInt8(buffer, offset);

                case Format.UInt16:
                    return new UInt16(buffer, offset);

                case Format.UInt32:
                    return new UInt32(buffer, offset);

                case Format.UInt64:
                    return new UInt64(buffer, offset);

                case Format.Int8:
                    return new Int8(buffer, offset);

                case Format.Int16:
                    return new Int16(buffer, offset);

                case Format.Int32:
                    return new Int32(buffer, offset);

                case Format.Int64:
                    return new Int64(buffer, offset);

                default:
                    if (key <= Format.NegativeFixIntMax && key >= Format.NegativeFixIntMin) {
                        return new Int8(buffer, offset);
                    }
                    if (key <= Format.PositiveFixIntMax && key >= Format.PositiveFixIntMin) {
                        return new Int8(buffer, offset);
                    }

                    break;
            }
            return null;
        }
    }
}