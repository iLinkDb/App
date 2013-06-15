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
            string logMsg = "Program/Main";

            string htmlFileName = "";
            try
            {
                HtmlLogFile html = new HtmlLogFile("E:\\PayWebRoleTest");
                htmlFileName = html.FileName;

                try
                {
                    // Each of the tests should be in this Try block.
                    DuckDuckGoTests duck = new DuckDuckGoTests();
                    duck.Begin(html);

                    //TenantTests tenantTests = new TenantTests();
                    //tenantTests.Begin(html);

                }
                catch (Exception ex)
                { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

                // After all Tests, close the file.
                html.WritePageFooter();
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }
            finally
            {
                System.Threading.Thread.Sleep(2000);
                Process someProcess = new Process();
                someProcess.StartInfo.FileName = htmlFileName;
                someProcess.Start();
            }
        }
    }
}
