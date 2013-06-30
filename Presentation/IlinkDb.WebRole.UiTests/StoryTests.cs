using System;
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
    public class StoryTests : TestBase, ITestBase
    {
        private string _siteUrl = "http://localhost:21764/";

        public void Begin(HtmlLogFile htmlLogFile)
        {
            Begin(htmlLogFile, _siteUrl);
        }

        public void Begin(HtmlLogFile htmlLogFile, string siteUrl)
        {
            string logMsg = "StoryTests/Begin";

            Console.WriteLine("Url: {0}", siteUrl);

            try
            {
                TestEngineSelenium chromeTest = new TestEngineSelenium(siteUrl, htmlLogFile, "StoryTests-Chrome");
                RunTest(chromeTest);

//                TestEngineWatin ieTest = new TestEngineWatin(siteUrl, htmlLogFile, "StoryTests-IE");
//                RunTest(ieTest);
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
            string logMsg = "StoryTests/RunTest";

            try
            {
                try
                {
                    test.Assert("Stories");

                    test.LinkClickByText("Stories");

                    test.Assert("Everything below this line");

                    test.LinkClickById("msb_addStory");

                    test.SetTextFieldByName("Name", "Tom Tuttle");

                    test.SetTextFieldByName("Description", "Automated Test @ " + DateTime.Now.ToString());

                    test.SetTextFieldByName("Estimate", "0");

                    test.ButtonClickById("btnStorySubmit");
                    
                    System.Threading.Thread.Sleep(5000);
                    //test.SetTextFieldByName(TestData.SearchBox, searchFor);

                    //test.ButtonClickById(TestData.DuckSubmit);

                    //test.Assert(searchFor);
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
