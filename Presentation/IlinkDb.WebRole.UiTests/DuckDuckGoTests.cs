﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppCommon;

namespace IlinkDb.WebRole.UiTests
{
    public class DuckDuckGoTests : TestBase, ITestBase
    {
        private string _siteUrl = "http://www.duckduckgo.com";

        public void Begin(HtmlLogFile htmlLogFile)
        {
            Begin(htmlLogFile, _siteUrl);
        }

        public void Begin(HtmlLogFile htmlLogFile, string siteUrl)
        {
            string logMsg = "DuckDuckGoTests/Begin";

            Console.WriteLine("Url: {0}", siteUrl);

            try
            {
                TestEngineSelenium chromeTest = new TestEngineSelenium(siteUrl, htmlLogFile, "DuckDuck-Chrome");
                RunTest(chromeTest);

                TestEngineWatin ieTest = new TestEngineWatin(siteUrl, htmlLogFile, "DuckDuck-IE");
                RunTest(ieTest);
            }
            catch (Exception ex)
            {
                string errMsg = logMsg + ", EXCEPTION " + ex.Message;
                Console.WriteLine(errMsg);
                Logging.LogError(errMsg, ex);
            }
        }

        private void RunTest(ITestEngine test)
        {
            string logMsg = "DuckDuckGoTests/RunTest";

            try
            {
                try
                {
                    string searchFor = "giving100";

                    test.SetTextFieldByName(TestData.SearchBox, searchFor);

                    test.ButtonClickById(TestData.DuckSubmit);

                    test.Assert(searchFor);

                    test.Cleanup();
                }
                catch (COMException ex)
                {
                    test.ShowError(logMsg + ", COMException: " + ex.Message, ex);
                }
                catch (Exception ex)
                {
                    test.WriteScreenShot(ex.Message);
                    test.ShowError(logMsg + ", EXCEPTION (With Screen Capture) " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                string errMsg = logMsg + ", EXCEPTION " + ex.Message;
                Console.WriteLine(errMsg);
                Logging.LogError(errMsg, ex);
            }
        }
    }
}
