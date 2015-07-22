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

namespace FearTheCowboy.Windows.Structures {
    //-----------------------------------------------------------------------
    //     ResourceLib Original Code from http://resourcelib.codeplex.com
    //     Original Copyright (c) 2008-2009 Vestris Inc.
    // <license>
    // MIT License
    // You may freely use and distribute this software under the terms of the following license agreement.
    // 
    // Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
    // documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
    // the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
    // to permit persons to whom the Software is furnished to do so, subject to the following conditions:
    // 
    // The above copyright notice and this permission notice shall be included in all copies or substantial portions of 
    // the Software.
    // 
    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
    // THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
    // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
    // TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
    // </license>
    //-----------------------------------------------------------------------
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     An extended dialog box template begins with a DIALOGEXTEMPLATE header that describes the dialog box and specifies
    ///     the number of controls in the dialog box. For each control in a dialog box, an extended dialog box template has a
    ///     block of data that uses the DIALOGEXITEMTEMPLATE format to describe the control.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct DialogExTemplate {
        /// <summary>
        ///     Specifies the version number of the extended dialog box template. This member must be 1.
        /// </summary>
        public UInt16 dlgVer;

        /// <summary>
        ///     Indicates whether a template is an extended dialog box template.
        /// </summary>
        public UInt16 signature;

        /// <summary>
        ///     Specifies the help context identifier for the dialog box window. When the system sends a WM_HELP message, it passes
        ///     this value in the wContextId member of the HELPINFO structure.
        /// </summary>
        public UInt32 helpID;

        /// <summary>
        ///     Specifies extended windows styles.
        /// </summary>
        public UInt32 exStyle;

        /// <summary>
        ///     Specifies the style of the dialog box.
        /// </summary>
        public UInt32 style;

        /// <summary>
        ///     Specifies the number of controls in the dialog box.
        /// </summary>
        public UInt16 cDlgItems;

        /// <summary>
        ///     Specifies the x-coordinate, in dialog box units, of the upper-left corner of the dialog box.
        /// </summary>
        public Int16 x;

        /// <summary>
        ///     Specifies the y-coordinate, in dialog box units, of the upper-left corner of the dialog box.
        /// </summary>
        public Int16 y;

        /// <summary>
        ///     Specifies the width, in dialog box units, of the dialog box.
        /// </summary>
        public Int16 cx;

        /// <summary>
        ///     Specifies the height, in dialog box units, of the dialog box.
        /// </summary>
        public Int16 cy;
    }
}