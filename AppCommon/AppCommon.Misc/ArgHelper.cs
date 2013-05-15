using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace AppCommon
{
    public class ArgHelper
    {
        // Variables

        private StringDictionary _paramList;

        // Constructor
        public ArgHelper(string[] Args)
        {
            _paramList = new StringDictionary();

            Regex Spliter = new Regex(@"^-{1,2}|^/|=|:",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Regex Remover = new Regex(@"^['""]?(.*?)['""]?$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string parameter = null;
            string[] parts;

            // Valid parameters forms:

            // {-,/,--}param{ ,=,:}((",')value(",'))

            // Examples: 

            // -param1 value1 --param2 /param3:"Test-:-work" 

            //   /param4=happy -param5 '--=nice=--'

            foreach (string txt in Args)
            {
                // Look for new parameters (-,/ or --) and a

                // possible enclosed value (=,:)

                parts = Spliter.Split(txt, 3);

                switch (parts.Length)
                {
                    // Found a value (for the last parameter 

                    // found (space separator))

                    case 1:
                        if (parameter != null)
                        {
                            if (!_paramList.ContainsKey(parameter))
                            {
                                parts[0] =
                                    Remover.Replace(parts[0], "$1");

                                _paramList.Add(parameter, parts[0]);
                            }
                            parameter = null;
                        }
                        // else Error: no parameter waiting for a value (skipped)

                        break;

                    // Found just a parameter

                    case 2:
                        // The last parameter is still waiting. 

                        // With no value, set it to true.

                        if (parameter != null)
                        {
                            if (!_paramList.ContainsKey(parameter))
                                _paramList.Add(parameter, "true");
                        }
                        parameter = parts[1];
                        break;

                    // Parameter with enclosed value

                    case 3:
                        // The last parameter is still waiting. 

                        // With no value, set it to true.

                        if (parameter != null)
                        {
                            if (!_paramList.ContainsKey(parameter))
                                _paramList.Add(parameter, "true");
                        }

                        parameter = parts[1];

                        // Remove possible enclosing characters (",')

                        if (!_paramList.ContainsKey(parameter))
                        {
                            parts[2] = Remover.Replace(parts[2], "$1");
                            _paramList.Add(parameter, parts[2]);
                        }

                        parameter = null;
                        break;
                }
            }
            // In case a parameter is still waiting

            if (parameter != null)
            {
                if (!_paramList.ContainsKey(parameter))
                    _paramList.Add(parameter, "true");
            }
        }

        // Retrieve a parameter value if it exists 

        // (overriding C# indexer property)

        public string this[string Param]
        {
            get
            {
                return (_paramList[Param]);
            }
        }

        public string GetSvcNum()
        {
           string svcNum = "01";

           if (this["2"] != null)
           { svcNum = "02"; }
           else if (this["3"] != null)
           { svcNum = "03"; }
           else if (this["4"] != null)
           { svcNum = "04"; }
           else if (this["5"] != null)
           { svcNum = "05"; }
           else if (this["6"] != null)
           { svcNum = "06"; }
           else if (this["7"] != null)
           { svcNum = "07"; }
           else if (this["8"] != null)
           { svcNum = "08"; }
           else if (this["9"] != null)
           { svcNum = "09"; }
            else if (this["10"] != null)
            { svcNum = "10"; }
            else if (this["11"] != null)
            { svcNum = "11"; }
            else if (this["12"] != null)
            { svcNum = "12"; }

           return svcNum;
        }

    }

}
