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

namespace FearTheCowboy.Windows.Enumerations {
    /// <summary>
    ///     Enumeration for Console Event types
    /// </summary>
    public enum ConsoleEvents {
        /// <summary>
        ///     Event type for when Ctrl-C is pressed
        /// </summary>
        CTRL_C_EVENT = 0,

        /// <summary>
        ///     Event type for when Ctrl-Break is pressed
        /// </summary>
        CTRL_BREAK_EVENT = 1,

        /// <summary>
        ///     Event type for when the application is closed
        /// </summary>
        CTRL_CLOSE_EVENT = 2,

        /// <summary>
        ///     Event type for when the user logs off
        /// </summary>
        CTRL_LOGOFF_EVENT = 5,

        /// <summary>
        ///     Event type for when the system shuts down.
        /// </summary>
        CTRL_SHUTDOWN_EVENT = 6
    }
}