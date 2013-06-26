using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WatiN.Core;

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

            _siteUrl = siteUrl;

            try
            {
                WriteStartTest(htmlLogFile, logMsg);
                using (IE browser = new IE(_siteUrl))
                {
                    try
                    {
                        ClearCache(browser);
                        browser.WaitForComplete();

                        //                        TestData data = new TestData(TestDataEnum.BarneyRubble);
                        string searchFor = "giving100";

                        SetTextFieldByName(browser, TestData.SearchBox, searchFor);

                        browser.Button(Find.ById(TestData.DuckSubmit)).Click();
                        Assert(browser, searchFor);

                    }
                    catch (COMException ex)
                    {
                        ShowError(logMsg + ", COMException: " + ex.Message, ex);
                    }
                    catch (Exception ex)
                    {
                        WriteScreenShot(browser, ex.Message);
                        ShowError(logMsg + ", EXCEPTION (With Screen Capture) " + ex.Message, ex);
                    }
                }
                WriteEndTest();
            }
            catch (Exception ex)
            { ShowError(logMsg + ", EXCEPTION (Without Screen Capture) " + ex.Message, ex); }
        }

    }
}
