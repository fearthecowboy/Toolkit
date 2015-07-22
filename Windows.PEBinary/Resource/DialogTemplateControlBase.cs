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

namespace FearTheCowboy.Windows.Resource {
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
    using System.IO;
    using System.Runtime.InteropServices;
    using Structures;

    /// <summary>
    ///     A dialog template.
    /// </summary>
    public abstract class DialogTemplateControlBase {
        private ResourceId _captionId;
        private ResourceId _controlClassId;

        /// <summary>
        ///     X-coordinate, in dialog box units, of the upper-left corner of the dialog box.
        /// </summary>
        public abstract Int16 x {get; set;}

        /// <summary>
        ///     Y-coordinate, in dialog box units, of the upper-left corner of the dialog box.
        /// </summary>
        public abstract Int16 y {get; set;}

        /// <summary>
        ///     Width, in dialog box units, of the dialog box.
        /// </summary>
        public abstract Int16 cx {get; set;}

        /// <summary>
        ///     Height, in dialog box units, of the dialog box.
        /// </summary>
        public abstract Int16 cy {get; set;}

        /// <summary>
        ///     Style of the dialog box.
        /// </summary>
        public abstract UInt32 Style {get; set;}

        /// <summary>
        ///     Optional extended style of the dialog box.
        /// </summary>
        public abstract UInt32 ExtendedStyle {get; set;}

        /// <summary>
        ///     Dialog caption.
        /// </summary>
        public ResourceId CaptionId
        {
            get
            {
                return _captionId;
            }
            set
            {
                _captionId = value;
            }
        }

        /// <summary>
        ///     Window class Id.
        /// </summary>
        public ResourceId ControlClassId
        {
            get
            {
                return _controlClassId;
            }
            set
            {
                _controlClassId = value;
            }
        }

        /// <summary>
        ///     Window class of the control.
        /// </summary>
        public DialogItemClass ControlClass
        {
            get
            {
                return (DialogItemClass)ControlClassId.Id;
            }
        }

        /// <summary>
        ///     Additional creation data.
        /// </summary>
        public byte[] CreationData {get; set;}

        internal virtual IntPtr Read(IntPtr lpRes) {
            // control class
            lpRes = DialogTemplateUtil.ReadResourceId(lpRes, out _controlClassId);
            // caption
            lpRes = DialogTemplateUtil.ReadResourceId(lpRes, out _captionId);

            // optional/additional creation data
            switch ((UInt16)Marshal.ReadInt16(lpRes)) {
                case 0x0000: // no data
                    lpRes = new IntPtr(lpRes.ToInt32() + 2);
                    break;
                default:
                    var size = (UInt16)Marshal.ReadInt16(lpRes);
                    CreationData = new byte[size];
                    Marshal.Copy(lpRes, CreationData, 0, CreationData.Length);
                    lpRes = new IntPtr(lpRes.ToInt32() + size);
                    break;
            }

            return lpRes;
        }

        /// <summary>
        ///     Write the dialog control to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        public virtual void Write(BinaryWriter w) {
            // control class
            DialogTemplateUtil.WriteResourceId(w, _controlClassId);
            // caption
            DialogTemplateUtil.WriteResourceId(w, _captionId);

            if (CreationData == null) {
                w.Write((UInt16)0);
            } else {
                ResourceUtil.PadToWORD(w);
                w.Write((UInt16)CreationData.Length);
                w.Write(CreationData);
            }
        }
    }
}