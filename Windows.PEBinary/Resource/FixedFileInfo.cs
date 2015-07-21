//-----------------------------------------------------------------------
// <copyright company="CoApp Project">
//     ResourceLib Original Code from http://resourcelib.codeplex.com
//     Original Copyright (c) 2008-2009 Vestris Inc.
//     Changes Copyright (c) 2011 Garrett Serack . All rights reserved.
// </copyright>
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

namespace Toolkit.Windows.Resource {
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using Enumerations;
    using Structures;

    /// <summary>
    ///     Fixed file information.
    /// </summary>
    public class FixedFileInfo {
        private VsFixedfileinfo _fixedfileinfo = VsFixedfileinfo.GetWindowsDefault();

        /// <summary>
        ///     Fixed file info structure.
        /// </summary>
        public VsFixedfileinfo Value {
            get {
                return _fixedfileinfo;
            }
        }

        /// <summary>
        ///     String representation of the file version.
        /// </summary>
        public string FileVersion {
            get {
                return string.Format("{0}.{1}.{2}.{3}", ResourceUtil.HiWord(_fixedfileinfo.dwFileVersionMS), ResourceUtil.LoWord(_fixedfileinfo.dwFileVersionMS),
                    ResourceUtil.HiWord(_fixedfileinfo.dwFileVersionLS), ResourceUtil.LoWord(_fixedfileinfo.dwFileVersionLS));
            }
            set {
                UInt32 major = 0,
                    minor = 0,
                    build = 0,
                    release = 0;
                var version_s = value.Split(".".ToCharArray(), 4);
                if (version_s.Length >= 1) {
                    major = UInt32.Parse(version_s[0]);
                }
                if (version_s.Length >= 2) {
                    minor = UInt32.Parse(version_s[1]);
                }
                if (version_s.Length >= 3) {
                    build = UInt32.Parse(version_s[2]);
                }
                if (version_s.Length >= 4) {
                    release = UInt32.Parse(version_s[3]);
                }
                _fixedfileinfo.dwFileVersionMS = (major << 16) + minor;
                _fixedfileinfo.dwFileVersionLS = (build << 16) + release;
            }
        }

        /// <summary>
        ///     String representation of the protect version.
        /// </summary>
        public string ProductVersion {
            get {
                return string.Format("{0}.{1}.{2}.{3}", ResourceUtil.HiWord(_fixedfileinfo.dwProductVersionMS),
                    ResourceUtil.LoWord(_fixedfileinfo.dwProductVersionMS), ResourceUtil.HiWord(_fixedfileinfo.dwProductVersionLS),
                    ResourceUtil.LoWord(_fixedfileinfo.dwProductVersionLS));
            }
            set {
                UInt32 major = 0,
                    minor = 0,
                    build = 0,
                    release = 0;
                var version_s = value.Split(".".ToCharArray(), 4);
                if (version_s.Length >= 1) {
                    major = UInt32.Parse(version_s[0]);
                }
                if (version_s.Length >= 2) {
                    minor = UInt32.Parse(version_s[1]);
                }
                if (version_s.Length >= 3) {
                    build = UInt32.Parse(version_s[2]);
                }
                if (version_s.Length >= 4) {
                    release = UInt32.Parse(version_s[3]);
                }
                _fixedfileinfo.dwProductVersionMS = (major << 16) + minor;
                _fixedfileinfo.dwProductVersionLS = (build << 16) + release;
            }
        }

        /// <summary>
        ///     Size of the VS_FIXEDFILEINFO structure.
        /// </summary>
        public UInt16 Size {
            get {
                return (UInt16)Marshal.SizeOf(_fixedfileinfo);
            }
        }

        /// <summary>
        ///     Read the fixed file  information structure.
        /// </summary>
        /// <param name="lpRes">Address in memory.</param>
        internal void Read(IntPtr lpRes) {
            _fixedfileinfo = (VsFixedfileinfo)Marshal.PtrToStructure(lpRes, typeof (VsFixedfileinfo));
        }

        /// <summary>
        ///     Write fixed file information to a binary stream.
        /// </summary>
        /// <param name="w">Binary stream.</param>
        public void Write(BinaryWriter w) {
            w.Write(ResourceUtil.GetBytes(_fixedfileinfo));
            ResourceUtil.PadToDWORD(w);
        }

        /// <summary>
        ///     String representation of the fixed file info.
        /// </summary>
        /// <returns>String representation of the fixed file info.</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("FILEVERSION {0},{1},{2},{3}", ResourceUtil.HiWord(_fixedfileinfo.dwFileVersionMS),
                ResourceUtil.LoWord(_fixedfileinfo.dwFileVersionMS), ResourceUtil.HiWord(_fixedfileinfo.dwFileVersionLS),
                ResourceUtil.LoWord(_fixedfileinfo.dwFileVersionLS)));
            sb.AppendLine(string.Format("PRODUCTVERSION {0},{1},{2},{3}", ResourceUtil.HiWord(_fixedfileinfo.dwProductVersionMS),
                ResourceUtil.LoWord(_fixedfileinfo.dwProductVersionMS), ResourceUtil.HiWord(_fixedfileinfo.dwProductVersionLS),
                ResourceUtil.LoWord(_fixedfileinfo.dwProductVersionLS)));
            if (_fixedfileinfo.dwFileFlagsMask == Winver.VS_FFI_FILEFLAGSMASK) {
                sb.AppendLine("FILEFLAGSMASK VS_FFI_FILEFLAGSMASK");
            } else {
                sb.AppendLine(string.Format("FILEFLAGSMASK 0x{0:x}", _fixedfileinfo.dwFileFlagsMask.ToString()));
            }
            sb.AppendLine(string.Format("FILEFLAGS {0}",
                _fixedfileinfo.dwFileFlags == 0 ? "0" : ResourceUtil.FlagsToString<Winver.FileFlags>(_fixedfileinfo.dwFileFlags)));
            sb.AppendLine(string.Format("FILEOS {0}", ResourceUtil.FlagsToString<Winver.FileOs>(_fixedfileinfo.dwFileFlags)));
            sb.AppendLine(string.Format("FILETYPE {0}", ResourceUtil.FlagsToString<Winver.FileType>(_fixedfileinfo.dwFileType)));
            sb.AppendLine(string.Format("FILESUBTYPE {0}", ResourceUtil.FlagsToString<Winver.FileSubType>(_fixedfileinfo.dwFileSubtype)));
            return sb.ToString();
        }
    }
}