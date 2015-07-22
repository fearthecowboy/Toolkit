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
    public enum ControlCodes : uint {
        SetReparsePoint = 0x000900A4, // Command to set the reparse point data block.
        GetReparsePoint = 0x000900A8, // Command to get the reparse point data block.
        DeleteReparsePoint = 0x000900AC // Command to delete the reparse point data base.
    }
}