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

namespace FearTheCowboy.Windows {
    using System;
    using System.Runtime.InteropServices;

    public class Mscoree {
        [DllImport("mscoree.dll", EntryPoint = "StrongNameKeyDelete", CharSet = CharSet.Auto)]
        public static extern bool StrongNameKeyDelete(string wszKeyContainer);

        [DllImport("mscoree.dll", EntryPoint = "StrongNameKeyInstall", CharSet = CharSet.Auto)]
        public static extern bool StrongNameKeyInstall([MarshalAs(UnmanagedType.LPWStr)] string wszKeyContainer,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2, SizeConst = 0)] byte[] pbKeyBlob, int arg0);

        [DllImport("mscoree.dll", EntryPoint = "StrongNameSignatureGeneration", CharSet = CharSet.Auto)]
        public static extern bool StrongNameSignatureGeneration(string wszFilePath, string wszKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob, int ppbSignatureBlob, int pcbSignatureBlob);

        [DllImport("mscoree.dll", EntryPoint = "StrongNameErrorInfo", CharSet = CharSet.Auto)]
        public static extern uint StrongNameErrorInfo();

        [DllImport("mscoree.dll", EntryPoint = "StrongNameTokenFromPublicKey", CharSet = CharSet.Auto)]
        public static extern bool StrongNameTokenFromPublicKey(byte[] pbPublicKeyBlob, int cbPublicKeyBlob, out IntPtr ppbStrongNameToken, out int pcbStrongNameToken);

        [DllImport("mscoree.dll", EntryPoint = "StrongNameFreeBuffer", CharSet = CharSet.Auto)]
        public static extern void StrongNameFreeBuffer(IntPtr pbMemory);
    }
}