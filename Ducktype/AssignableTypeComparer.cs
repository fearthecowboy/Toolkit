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

namespace FearTheCowboy.Ducktype {
    using System;
    using System.Collections.Generic;

    internal class AssignableTypeComparer : IEqualityComparer<Type> {
        public static readonly AssignableTypeComparer Instance = new AssignableTypeComparer();

        public bool Equals(Type x, Type y) {
            return IsAssignableOrCompatible(x, y);
        }

        public int GetHashCode(Type obj) {
            // unused.
            return -1;
        }

        public static bool IsAssignableOrCompatible(Type x, Type y) {
            if (x == null) {
                return y == null;
            }
            // adding support to see if we can duck-type the target type to the correct type.
            return x == y || x.IsAssignableFrom(y) || x.CanDynamicCastFrom(y);
        }
    }
}