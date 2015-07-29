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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public interface IMessageReader {
        Task<Message> Next();
    }

    public class MessageStream : MessageBuffer ,IMessageReader {
        private readonly Stream _stream;
        private byte[] _buffer = new byte[128*1024];
        private int _head = 0;
        private int _tail = 0;

        public MessageStream(Stream bytestream) {
            _stream = bytestream;
        }

        private int Used => _tail - _head;
        private int Free => _buffer.Length - _tail;
        private int Dead => _buffer.Length - _head;
        protected internal override byte[] Data => _buffer;
        protected internal override int BaseOffset => _head;

        private void Resize() {
            Array.Resize(ref _buffer, _buffer.Length*2);
        }

        private void Compact() {
            var used = Used;
            Array.Copy(_buffer, _head, _buffer, 0, used);
            _head = 0;
            _tail = used;
        }

        private Message Commit(Message message) {
            var l = message.MessageLength;
            if (l > Used) {
                // this doesn't make a lot of sense. We really shouldn't see this. 
                // this means that at some point we haven't 'required' enough bytes
                // and the committer is asking for more than was parsed for.
                Require(l).Wait();
            }
            if (l <= Used) {
                message.Buffer = new StaticMessageBuffer(_buffer, _head, l);
                _head += l;
                return message;
            }

            throw new Exception($"ERROR: \r\n Message Type [{message.GetType().Name}] size: {l} Exceeds Available bytes in buffer {Used}");
        }

        protected internal override async Task<bool> Require(int minimumBytes) {
            while (true) {
                if (Used > minimumBytes) {
                    return true;
                }

                if (Free < minimumBytes) {
                    if (Dead >= minimumBytes) {
                        Compact();
                    } else {
                        Resize();
                        continue;
                    }
                }

                var bytesRead = 0;
                bytesRead = await _stream.ReadAsync(_buffer, _tail, Math.Min(32768, Free));
                if (bytesRead == 0) {
                    return false;
                }

                _tail += bytesRead;
                if (Used < minimumBytes) {
                    continue;
                }
                return true;
            }
        }

        public async Task<Message> Next() {
            if (await Require(1)) {
                return Commit(await CreateMessage(this, 0));
            }
            return null;
        }

    }
}