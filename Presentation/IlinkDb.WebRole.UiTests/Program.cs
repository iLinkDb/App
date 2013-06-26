using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppCommon;

using WatiN.Core;

namespace IlinkDb.WebRole.UiTests
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                TestRunner.Begin("E:\\AppTests\\WebRole", args);
            }
            else
            {
                TestRunner.ShowHelp();
            }
        }
    }
}
