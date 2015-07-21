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

namespace Toolkit.Windows {
    using System;
    using System.Runtime.InteropServices;
    using Flags;
    using Structures;

    public class CryptUi {
        [DllImport("Cryptui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CryptUIWizDigitalSign(DigitalSignFlags dwFlags, IntPtr hwndParent, string pwszWizardTitle, ref DigitalSignInfo pDigitalSignInfo, ref IntPtr ppSignContext);

        [DllImport("Cryptui.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptUIWizFreeDigitalSignContext(IntPtr pSignContext);
    }
}