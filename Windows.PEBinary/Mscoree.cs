//-----------------------------------------------------------------------
// <copyright company="CoApp Project">
//     Copyright (c) 2010-2013 Garrett Serack and CoApp Contributors. 
//     Contributors can be discovered using the 'git log' command.
//     All rights reserved.
// </copyright>
// <license>
//     The software is licensed under the Apache 2.0 License (the "License")
//     You may not use the software except in compliance with the License. 
// </license>
//-----------------------------------------------------------------------

namespace Toolkit.Windows {
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