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
    using System.Text;
    using Enumerations;
    using Structures;

    /// <summary>
    ///     A container for a control within a dialog template.
    /// </summary>
    public class DialogTemplateControl : DialogTemplateControlBase {
        private DialogItemTemplate _header;

        /// <summary>
        ///     X-coordinate, in dialog box units, of the upper-left corner of the dialog box.
        /// </summary>
        public override Int16 x
        {
            get
            {
                return _header.x;
            }
            set
            {
                _header.x = value;
            }
        }

        /// <summary>
        ///     Y-coordinate, in dialog box units, of the upper-left corner of the dialog box.
        /// </summary>
        public override Int16 y
        {
            get
            {
                return _header.y;
            }
            set
            {
                _header.y = value;
            }
        }

        /// <summary>
        ///     Width, in dialog box units, of the dialog box.
        /// </summary>
        public override Int16 cx
        {
            get
            {
                return _header.cx;
            }
            set
            {
                _header.cx = value;
            }
        }

        /// <summary>
        ///     Height, in dialog box units, of the dialog box.
        /// </summary>
        public override Int16 cy
        {
            get
            {
                return _header.cy;
            }
            set
            {
                _header.cy = value;
            }
        }

        /// <summary>
        ///     Dialog style.
        /// </summary>
        public override UInt32 Style
        {
            get
            {
                return _header.style;
            }
            set
            {
                _header.style = value;
            }
        }

        /// <summary>
        ///     Extended dialog style.
        /// </summary>
        public override UInt32 ExtendedStyle
        {
            get
            {
                return _header.dwExtendedStyle;
            }
            set
            {
                _header.dwExtendedStyle = value;
            }
        }

        /// <summary>
        ///     Control identifier.
        /// </summary>
        public Int16 Id
        {
            get
            {
                return _header.id;
            }
            set
            {
                _header.id = value;
            }
        }

        internal override IntPtr Read(IntPtr lpRes) {
            _header = (DialogItemTemplate)Marshal.PtrToStructure(lpRes, typeof (DialogItemTemplate));

            lpRes = new IntPtr(lpRes.ToInt32() + 18); // Marshal.SizeOf(_header)
            lpRes = base.Read(lpRes);

            return lpRes;
        }

        /// <summary>
        ///     Write the dialog control to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        public override void Write(BinaryWriter w) {
            w.Write(_header.style);
            w.Write(_header.dwExtendedStyle);
            w.Write(_header.x);
            w.Write(_header.y);
            w.Write(_header.cx);
            w.Write(_header.cy);
            w.Write(_header.id);
            base.Write(w);
        }

        /// <summary>
        ///     String represetnation of a control.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            var sb = new StringBuilder();

            sb.AppendFormat("{0} \"{1}\" {2}, {3}, {4}, {5}, {6}, {7}", ControlClass, CaptionId, Id, x, y, cx, cy,
                DialogTemplateUtil.StyleToString<WindowStyles, DialogStyles>(Style));

            switch (ControlClass) {
                case DialogItemClass.Button:
                    sb.AppendFormat("| {0}", (ButtonControlStyles)(Style & 0xFFFF));
                    break;
                case DialogItemClass.Edit:
                    sb.AppendFormat("| {0}", DialogTemplateUtil.StyleToString<EditControlStyles>(Style & 0xFFFF));
                    break;
            }

            return sb.ToString();
        }
    }
}