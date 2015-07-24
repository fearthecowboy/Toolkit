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
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using Enumerations;

    /// <summary>
    ///     A dialog template.
    /// </summary>
    public abstract class DialogTemplateBase {
        private ResourceId _menuId;
        private ResourceId _windowClassId;

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
        ///     Number of items in this structure.
        /// </summary>
        public abstract UInt16 ControlCount {get;}

        /// <summary>
        ///     The name of the typeface for the font.
        /// </summary>
        public String TypeFace {get; set;}

        /// <summary>
        ///     Point size of the font to use for the text in the dialog box and its controls.
        /// </summary>
        public UInt16 PointSize {get; set;}

        /// <summary>
        ///     Dialog caption.
        /// </summary>
        public string Caption {get; set;}

        /// <summary>
        ///     Menu resource Id.
        /// </summary>
        public ResourceId MenuId {get {return _menuId;} set {_menuId = value;}}

        /// <summary>
        ///     Window class Id.
        /// </summary>
        public ResourceId WindowClassId {get {return _windowClassId;} set {_windowClassId = value;}}

        /// <summary>
        ///     Controls within this dialog.
        /// </summary>
        public List<DialogTemplateControlBase> Controls {get; set;} = new List<DialogTemplateControlBase>();

        /// <summary>
        ///     Dialog template representation in a standard text format.
        /// </summary>
        /// <returns>Multiline string.</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine($"{x}, {y}, {x + cx}, {y + cy}");

            var style = DialogTemplateUtil.StyleToString<WindowStyles, DialogStyles>(Style);
            if (!string.IsNullOrEmpty(style)) {
                sb.AppendLine($"STYLE {style}");
            }

            var exstyle = DialogTemplateUtil.StyleToString<WindowStyles, ExtendedDialogStyles>(ExtendedStyle);
            if (!string.IsNullOrEmpty(exstyle)) {
                sb.AppendLine($"EXSTYLE {exstyle}");
            }

            sb.AppendLine($"CAPTION \"{Caption}\"");
            sb.AppendLine($"FONT {PointSize}, \"{TypeFace}\"");

            if (Controls.Count > 0) {
                sb.AppendLine("{");
                foreach (var control in Controls) {
                    sb.AppendLine($" {control}");
                }
                sb.AppendLine("}");
            }

            return sb.ToString();
        }

        /// <summary>
        ///     String represetnation of a control.
        /// </summary>
        /// <returns></returns>
        public virtual string ToControlString() {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} \"{1}\" {2}, {3}, {4}, {5}", WindowClassId, Caption, x, y, cx, cy);
            return sb.ToString();
        }

        internal virtual IntPtr Read(IntPtr lpRes) {
            // menu
            lpRes = DialogTemplateUtil.ReadResourceId(lpRes, out _menuId);
            // window class
            lpRes = DialogTemplateUtil.ReadResourceId(lpRes, out _windowClassId);
            // caption
            Caption = Marshal.PtrToStringUni(lpRes);
            lpRes = new IntPtr(lpRes.ToInt32() + (Caption.Length + 1)*Marshal.SystemDefaultCharSize);

            if ((Style & (uint)DialogStyles.DS_SETFONT) > 0 || (Style & (uint)DialogStyles.DS_SHELLFONT) > 0) {
                // point size
                PointSize = (UInt16)Marshal.ReadInt16(lpRes);
                lpRes = new IntPtr(lpRes.ToInt32() + 2);
            }

            return lpRes;
        }

        internal abstract IntPtr AddControl(IntPtr lpRes);

        internal IntPtr ReadControls(IntPtr lpRes) {
            for (var i = 0; i < ControlCount; i++) {
                lpRes = ResourceUtil.Align(lpRes);
                lpRes = AddControl(lpRes);
            }

            return lpRes;
        }

        internal void WriteControls(BinaryWriter w) {
            foreach (var control in Controls) {
                ResourceUtil.PadToDWORD(w);
                control.Write(w);
            }
        }

        /// <summary>
        ///     Write the resource to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        public virtual void Write(BinaryWriter w) {
            // menu
            DialogTemplateUtil.WriteResourceId(w, _menuId);
            // window class
            DialogTemplateUtil.WriteResourceId(w, _windowClassId);
            // caption
            w.Write(Encoding.Unicode.GetBytes(Caption));
            w.Write((UInt16)0);
            // point size
            if ((Style & (uint)DialogStyles.DS_SETFONT) > 0 || (Style & (uint)DialogStyles.DS_SHELLFONT) > 0) {
                w.Write(PointSize);
            }
        }
    }
}