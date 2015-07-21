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

    /// <summary>
    ///     A resource load exception.
    /// </summary>
    public class LoadException : Exception {
        private Exception _outerException;

        /// <summary>
        ///     A new resource load exception.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="innerException">The inner exception thrown within a single resource.</param>
        /// <param name="outerException">The outer exception from the Win32 API.</param>
        public LoadException(string message, Exception innerException, Exception outerException) : base(message, innerException) {
            _outerException = outerException;
        }

        /// <summary>
        ///     The Win32 exception from a resource enumeration function.
        /// </summary>
        public Exception OuterException {
            get {
                return _outerException;
            }
        }

        /// <summary>
        ///     A combined message of the inner and outer exception.
        /// </summary>
        public override string Message {
            get {
                return _outerException != null ? string.Format("{0} {1}.", base.Message, _outerException.Message) : base.Message;
            }
        }
    }
}