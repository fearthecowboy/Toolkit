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
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class TaskExtensions {
        public static TaskAwaiter GetAwaiter(this IEnumerable<Task> tasks) => Task.WhenAll(tasks).GetAwaiter();
        public static TaskAwaiter GetAwaiter(this Action action) => Task.Run(action).GetAwaiter();
        public static TaskAwaiter<T> GetAwaiter<T>(this Func<T> function) => Task.Run(function).GetAwaiter();
        public static Task<T> ToResultTask<T>(this T obj) => Task.FromResult(obj);
    }
}