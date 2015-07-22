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

namespace FearTheCowboy.Common.Collections {
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     A Enumerable wrapper that optimistically caches elements when enumerated
    ///     Permits the re-enumeration without re-running the original query.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReEnumerable<T> : MutableEnumerable<T> {
        private readonly IEnumerable<T> _source;
        private IEnumerator<T> _sourceIterator;

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        public ReEnumerable(IEnumerable<T> source) {
            _source = source ?? new T[0];
        }

        /// <summary>
        /// </summary>
        /// <param name="sourceIterator"></param>
        public ReEnumerable(IEnumerator<T> sourceIterator) {
            _source = null;
            _sourceIterator = sourceIterator;
        }

        /// <summary>
        /// </summary>
        /// <param name="index"></param>
        public T this[int index]
        {
            get
            {
                if (ItemExists(index)) {
                    return List[index];
                }
                return default(T);
            }
        }

        /// <summary>
        /// </summary>
        public int Count
        {
            get
            {
                return this.Count();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override bool ItemExists(int index) {
            if (index < List.Count) {
                return true;
            }

            lock (this) {
                if (_sourceIterator == null) {
                    _sourceIterator = _source.GetEnumerator();
                }

                try {
                    while (_sourceIterator.MoveNext()) {
                        List.Add(_sourceIterator.Current);
                        if (index < List.Count) {
                            return true;
                        }
                    }
                } catch {
                    // if the _sourceIterator is cancelled
                    // then MoveNext() will throw; that's ok
                    // that just means we're done
                }
            }
            return false;
        }

        /// <summary>
        /// </summary>
        /// <param name="additionalItems"></param>
        /// <returns></returns>
        public MutableEnumerable<T> Concat(IEnumerable<T> additionalItems) {
            return Enumerable.Concat(this, additionalItems).ReEnumerable();
        }
    }
}