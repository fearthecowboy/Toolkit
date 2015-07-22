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

namespace FearTheCowboy.CommandLine {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using Windows;
    using Common.Core;

    public static class E {
        public static R With<T, R>(this T t, Func<T, R> fn) {
            return fn(t);
        }
    }

    /// <summary>
    /// </summary>
    /// <remarks>
    ///     NOTE: Explicity Ignore, testing this will produce no discernable value, and will only lead to heartbreak.
    /// </remarks>
    public static class CommandLineExtensions {
        /// <summary>
        /// </summary>
        public const string HelpConfigSyntax =
            @"
Advanced Command Line Configuration Files 
-----------------------------------------
You may pass any double-dashed command line options in a configuration file 
that is loaded with --load-config=<file>.

Inside the configuration file, omit the double dash prefix; simply put 
each option on a seperate line.

On the command line:

    --some-option=foo 

would become the following inside the configuration file: 

    some-option=foo

Additionally, options in the configuration file can be grouped together in 
categories. The category is simply syntatic sugar for simplifying the command
line.

Categories are declared with the square brackets: [] 

The category is appended to options that follow its declaration.

A configuration file expressed as:

source-option=foo
source-option=bar
source-option=bin
source-add=baz
source-ignore=bug

can also be expressed as:

[source]
option=foo
option=bar
option=bin
add=baz
ignore=bug
";

        /// <summary>
        ///     Gets the parameters for switch.
        /// </summary>
        /// <param name="args"> The args. </param>
        /// <param name="key"> The key. </param>
        /// <returns> </returns>
        /// <remarks>
        /// </remarks>
        public static IEnumerable<string> GetParametersForSwitch(this IEnumerable<string> args, string key) {
            return ParsedCommandLine.Parse(args).With(p => p.Switches.ContainsKey(key) ? p.Switches[key] : Enumerable.Empty<string>());
        }

        /// <summary>
        ///     Switches the value.
        /// </summary>
        /// <param name="args"> The args. </param>
        /// <param name="key"> The key. </param>
        /// <returns> </returns>
        /// <remarks>
        /// </remarks>
        public static string SwitchValue(this IEnumerable<string> args, string key) {
            var p = ParsedCommandLine.Parse(args);
            return p.Switches.ContainsKey(key) ? p.Switches[key].FirstOrDefault() : null;
        }

        public static IEnumerable<string> SplitArgs(string unsplitArgumentLine) {
#if USES_WIN32_API
            int numberOfArgs;

            var ptrToSplitArgs = Shell32.CommandLineToArgvW(unsplitArgumentLine, out numberOfArgs);

            // CommandLineToArgvW returns NULL upon failure.
            if (ptrToSplitArgs == IntPtr.Zero) {
                throw new ArgumentException("Unable to split argument.", new Win32Exception());
            }

            // Make sure the memory ptrToSplitArgs to is freed, even upon failure.
            try {
                var splitArgs = new string[numberOfArgs];

                // ptrToSplitArgs is an array of pointers to null terminated Unicode strings.
                // Copy each of these strings into our split argument array.
                for (var i = 0; i < numberOfArgs; i++) {
                    splitArgs[i] = Marshal.PtrToStringUni(Marshal.ReadIntPtr(ptrToSplitArgs, i*IntPtr.Size));
                }

                return splitArgs;
            } finally {
                // Free memory obtained by CommandLineToArgW.
                Kernel32.LocalFree(ptrToSplitArgs);
            }
#else
            throw new Exception("PLATFORM NOT SUPPORTED");
#endif
        }

        /// <summary>
        ///     Switcheses the specified args.
        /// </summary>
        /// <param name="args"> The args. </param>
        /// <returns> </returns>
        /// <remarks>
        /// </remarks>
        public static IDictionary<string, List<string>> Switches(this IEnumerable<string> args) {
            return ParsedCommandLine.Parse(args).Switches;
        }

        // handles complex option switches
        // RX for splitting comma seperated values:
        //  http://dotnetslackers.com/Regex/re-19977_Regex_This_regex_splits_comma_or_semicolon_separated_lists_of_optionally_quoted_strings_It_hand.aspx
        //      @"\s*[;,]\s*(?!(?<=(?:^|[;,])\s*""(?:[^""]|""""|\\"")*[;,])(?:[^""]|""""|\\"")*""\s*(?:[;,]|$))"
        //  http://regexlib.com/REDetails.aspx?regexp_id=621
        //      @",(?!(?<=(?:^|,)\s*\x22(?:[^\x22]|\x22\x22|\\\x22)*,)(?:[^\x22]|\x22\x22|\\\x22)*\x22\s*(?:,|$))"
        /// <summary>
        ///     Gets the complex options.
        /// </summary>
        /// <param name="rawParameterList"> The raw parameter list. </param>
        /// <returns> </returns>
        /// <remarks>
        /// </remarks>
        public static IEnumerable<ComplexOption> GetComplexOptions(this IEnumerable<string> rawParameterList) {
            var optionList = new List<ComplexOption>();
            foreach (var p in rawParameterList) {
                var m = Regex.Match(p, @"\[(?>\"".*?\""|\[(?<DEPTH>)|\](?<-DEPTH>)|[^[]]?)*\](?(DEPTH)(?!))");
                if (m.Success) {
                    var co = new ComplexOption();
                    var v = m.Groups[0].Value;
                    var len = v.Length;
                    co.WholePrefix = v.Substring(1, len - 2);
                    co.WholeValue = p.Substring(len);

                    var parameterStrings = Regex.Split(co.WholePrefix, @",(?!(?<=(?:^|,)\s*\x22(?:[^\x22]|\x22\x22|\\\x22)*,)(?:[^\x22]|\x22\x22|\\\x22)*\x22\s*(?:,|$))");
                    foreach (var q in parameterStrings) {
                        v = q.Trim();
                        if (v[0] == '"' && v[v.Length - 1] == '"') {
                            v = v.Trim('"');
                        }
                        co.PrefixParameters.Add(v);
                    }

                    var values = co.WholeValue.Split('&');
                    foreach (var q in values) {
                        var pos = q.IndexOf('=');
                        if (pos > -1 && pos < q.Length - 1) {
                            co.Values.Add(q.Substring(0, pos).UrlDecode(), q.Substring(pos + 1).UrlDecode());
                        } else {
                            co.Values.Add(q.Trim('='), "");
                        }
                    }
                    optionList.Add(co);
                }
            }
            return optionList;
        }

        /// <summary>
        ///     Parameterses the specified args.
        /// </summary>
        /// <param name="args"> The args. </param>
        /// <returns> </returns>
        /// <remarks>
        /// </remarks>
        public static IEnumerable<string> Parameters(this IEnumerable<string> args) {
            return ParsedCommandLine.Parse(args).Parameters;
        }

        public static string ToCommandLine(this IEnumerable<string> args) {
            return args.Aggregate((current, each) => current + string.Format(@"{0}{1}{2}{3}",
                current.Length > 0 ? " " : "",
                each.IndexOf(' ') > -1 ? @"""" : "",
                each,
                current.Length > 0 ? " " : ""
                ));
        }
    }
}