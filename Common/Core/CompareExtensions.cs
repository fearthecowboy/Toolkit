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

namespace FearTheCowboy.Common.Core {
    using System;

    public static class CompareExtensions {
        public static bool WithCompareResult(this int result, Func<bool> onLess, Func<bool> onMore, Func<bool> onEqual) {
            return result < 0 ? onLess() : (result > 0 ? onMore() : onEqual());
        }

        public static bool WithCompareResult(this int result, bool lessResult, bool moreResult, bool equalResult) {
            return result < 0 ? lessResult : (result > 0 ? moreResult : equalResult);
        }

        public static int WithCompareResult(this int result, int lessResult, int moreResult, int equalResult) {
            return result < 0 ? lessResult : (result > 0 ? moreResult : equalResult);
        }
    }
}