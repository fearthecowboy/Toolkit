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

    public class Tasks {
        private readonly List<Task> _tasks = new List<Task>();
        public TaskAwaiter GetAwaiter() => Task.WhenAll(_tasks).GetAwaiter();

        public static Tasks operator +(Tasks tasks, Task task) {
            tasks._tasks.Add(task);
            return tasks;
        }

        /// <summary>
        ///     Starts the action as a background task, and adds it to the collection
        ///     of tasks.
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="action"></param>
        /// Example:
        /// var tasks = new Tasks();
        /// tasks += () => {
        /// // this runs in an alternate thread!
        /// Thread.Sleep(1000);
        /// };
        /// 
        /// await tasks;
        /// <returns></returns>
        public static Tasks operator +(Tasks tasks, Action action) {
            tasks._tasks.Add(Task.Factory.StartNew(action));
            return tasks;
        }
    }
}