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
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Common.Collections;
    using Common.Core;

    public class ParsedCommandLine
    {
        /// <summary>
        ///     Collection to store the cached set of parsed command lines
        /// </summary>
        private static readonly Dictionary<IEnumerable<string>, ParsedCommandLine> _cache =
            new Dictionary<IEnumerable<string>, ParsedCommandLine>(new Common.Core.EqualityComparer<IEnumerable<string>>((a, b) => a.SequenceEqual(b), strings => {
                unchecked
                {
                    return strings.Aggregate(0, (current, next) => (current * 419) ^ (next ?? new object()).GetHashCode());
                }
            }));

        /// <summary>
        ///     Parses the command line and returns a ParsedCommandLine
        ///     Multiple calls to this method will return the identical object (it's cached and keyed against the collection of items in the command line)
        /// </summary>
        /// <param name="args">the items in the command line to parse</param>
        /// <returns></returns>
        public static ParsedCommandLine Parse(IEnumerable<string> args)
        {
            return args.ReEnumerable().With(lazy => _cache.GetOrAdd(lazy, () => new ParsedCommandLine(lazy)));
        }

        /// <summary>
        ///     the parsed switches from the arguments
        /// </summary>
        private readonly IDictionary<string, List<string>> _switches;

        /// <summary>
        ///     the parameters that are left after parsing the switches
        /// </summary>
        private readonly IEnumerable<string> _parameters;

        public static IEnumerable<string> SplitArgs(string unsplitArgumentLine)
        {
            return CommandLineExtensions.SplitArgs(unsplitArgumentLine);
        }

        protected ParsedCommandLine(IEnumerable<string> args)
        {
            var assemblypath = Assembly.GetEntryAssembly().Location;

            _switches = new Dictionary<string, List<string>>();

            var v = Environment.GetEnvironmentVariable("_" + Path.GetFileNameWithoutExtension(assemblypath) + "_");
            if (!string.IsNullOrEmpty(v))
            {
                var extraSwitches = SplitArgs(v).Where(each => each.StartsWith("--"));
                if (!args.IsNullOrEmpty())
                {
                    args = args.Concat(extraSwitches);
                }
            }

            // load a <exe>.properties file in the same location as the executing assembly.

            var propertiespath = "{0}\\{1}.properties".format(Path.GetDirectoryName(assemblypath), Path.GetFileNameWithoutExtension(assemblypath));
            if (File.Exists(propertiespath))
            {
                LoadConfiguration(propertiespath);
            }

            var argEnumerator = args.GetEnumerator();
            //while(firstarg < args.Length && args[firstarg].StartsWith("--")) {
            while (argEnumerator.MoveNext() && argEnumerator.Current.StartsWith("--"))
            {
                var arg = argEnumerator.Current.Substring(2).ToLower();
                var param = "";
                int pos;

                if ((pos = arg.IndexOf("=")) > -1)
                {
                    param = argEnumerator.Current.Substring(pos + 3);
                    arg = arg.Substring(0, pos);
                    /*
                    if(string.IsNullOrEmpty(param) || string.IsNullOrEmpty(arg)) {
                        "Invalid Option :{0}".Print(argEnumerator.Current.Substring(2).ToLower());
                        switches.Clear();
                        switches.Add("help", new List<string>());
                        return switches;
                    } */
                }
                if (arg.Equals("load-config"))
                {
                    // loads the config file, and then continues parsing this line.
                    LoadConfiguration(param);
                    // firstarg++;
                    continue;
                }

                if (!_switches.ContainsKey(arg))
                {
                    _switches.Add(arg, new List<string>());
                }

                _switches[arg].Add(param);
                // firstarg++;
            }
            _parameters = args.Where(argument => !(argument.StartsWith("--")));
        }

        /// <summary>
        ///     Loads the configuration.
        /// </summary>
        /// <param name="file"> The file. </param>
        /// <remarks>
        /// </remarks>
        public void LoadConfiguration(string file)
        {
            var param = "";
            var category = "";

            if (File.Exists(file))
            {
                var lines = File.ReadAllLines(file);
                for (var ln = 0; ln < lines.Length; ln++)
                {
                    var line = lines[ln].Trim();
                    while (line.EndsWith("\\") && ln < lines.Length)
                    {
                        line = line.Substring(0, line.Length - 1);
                        if (++ln < lines.Length)
                        {
                            line += lines[ln].Trim();
                        }
                    }
                    var arg = line;

                    param = "";

                    if (arg.IndexOf("[") == 0)
                    {
                        // category 
                        category = arg.Substring(1, arg.IndexOf(']') - 1).Trim();
                        continue;
                    }

                    if (string.IsNullOrEmpty(arg) || arg.StartsWith(";") || arg.StartsWith("#")) // comments
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(category))
                    {
                        arg = "{0}-{1}".format(category, arg);
                    }

                    int pos;
                    if ((pos = arg.IndexOf("=")) > -1)
                    {
                        param = arg.Substring(pos + 1);
                        arg = arg.Substring(0, pos).ToLower();

                        if (string.IsNullOrEmpty(param) || string.IsNullOrEmpty(arg))
                        {
                            Console.WriteLine("Invalid Option in config file [{0}]: {1}", file, line.Trim());
                            _switches.Add("help", new List<string>());
                            return;
                        }
                    }

                    if (!_switches.ContainsKey(arg))
                    {
                        _switches.Add(arg, new List<string>());
                    }

                    (_switches[arg]).Add(param);
                }
            }
            else
            {
                Console.WriteLine("Unable to find configuration file [{0}]", param);
            }
        }

        public IEnumerable<string> Parameters
        {
            get
            {
                return _parameters;
            }
        }

        public IDictionary<string, List<string>> Switches
        {
            get
            {
                return _switches;
            }
        }

        public IEnumerable<string> GetParametersForSwitchOrNull(string key)
        {
            if (_switches.ContainsKey(key))
            {
                return _switches[key];
            }

            return null;
        }
    }
}