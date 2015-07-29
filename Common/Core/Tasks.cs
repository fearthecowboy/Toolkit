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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class Tasks {
        private ConcurrentBag<Task> _tasks = new ConcurrentBag<Task>();
        public TaskAwaiter GetAwaiter() => Task.WhenAll(_tasks).GetAwaiter();

        public static Tasks operator +(Tasks tasks, Task task) {
            tasks._tasks.Add(task);
            tasks.Collect();
            return tasks;
        }

        public static Tasks operator +(Tasks tasks, Action action) {
            tasks._tasks.Add(Task.Run(action));
            tasks.Collect();
            return tasks;
        }

        private void Collect() {
            // if there are more than 10 tasks, let's clean it up.
            if(_tasks.Count > 10) {
                // create a new bag.
                var newtasks = new ConcurrentBag<Task>();
                var oldTasks = _tasks;
                newtasks.Add(Task.Run(() => {
                    // copy items over from the old bag that are still running.
                    foreach(var t in oldTasks.Where(each => !each.IsCompleted)) {
                        newtasks.Add(t);
                    }
                }));
                // use the new bag from here on.
                _tasks = newtasks;
            }
        }
    }
}