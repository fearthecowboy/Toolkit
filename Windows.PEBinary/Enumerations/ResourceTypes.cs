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

    /// <summary>
    ///     Predefined resource types.
    /// </summary>
    public enum ResourceTypes {
        /// <summary>
        ///     For resource types not covered by the standard integer types. (binres?)
        /// </summary>
        RT_OTHER = 0,

        /// <summary>
        ///     Hardware-dependent cursor resource.
        /// </summary>
        RT_CURSOR = 1,

        /// <summary>
        ///     Bitmap resource.
        /// </summary>
        RT_BITMAP = 2,

        /// <summary>
        ///     Hardware-dependent icon resource.
        /// </summary>
        RT_ICON = 3,

        /// <summary>
        ///     Menu resource.
        /// </summary>
        RT_MENU = 4,

        /// <summary>
        ///     Dialog box.
        /// </summary>
        RT_DIALOG = 5,

        /// <summary>
        ///     String-table entry.
        /// </summary>
        RT_STRING = 6,

        /// <summary>
        ///     Font directory resource.
        /// </summary>
        RT_FONTDIR = 7,

        /// <summary>
        ///     Font resource.
        /// </summary>
        RT_FONT = 8,

        /// <summary>
        ///     Accelerator table.
        /// </summary>
        RT_ACCELERATOR = 9,

        /// <summary>
        ///     Application-defined resource (raw data).
        /// </summary>
        RT_RCDATA = 10,

        /// <summary>
        ///     Message-table entry.
        /// </summary>
        RT_MESSAGETABLE = 11,

        /// <summary>
        ///     Hardware-independent cursor resource.
        /// </summary>
        RT_GROUP_CURSOR = 12,

        /// <summary>
        ///     Hardware-independent icon resource.
        /// </summary>
        RT_GROUP_ICON = 14,

        /// <summary>
        ///     Version resource.
        /// </summary>
        RT_VERSION = 16,

        /// <summary>
        ///     Allows a resource editing tool to associate a string with an .rc file.
        /// </summary>
        RT_DLGINCLUDE = 17,

        /// <summary>
        ///     Plug and Play resource.
        /// </summary>
        RT_PLUGPLAY = 19,

        /// <summary>
        ///     VXD.
        /// </summary>
        RT_VXD = 20,

        /// <summary>
        ///     Animated cursor.
        /// </summary>
        RT_ANICURSOR = 21,

        /// <summary>
        ///     Animated icon.
        /// </summary>
        RT_ANIICON = 22,

        /// <summary>
        ///     HTML.
        /// </summary>
        RT_HTML = 23,

        /// <summary>
        ///     Microsoft Windows XP: Side-by-Side Assembly XML Manifest.
        /// </summary>
        RT_MANIFEST = 24,
    }
}