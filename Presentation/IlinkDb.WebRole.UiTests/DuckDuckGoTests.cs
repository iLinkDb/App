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
    public class DuckDuckGoTests : TestBase
    {
        public void Begin(HtmlLogFile htmlLogFile)
        {
            string logMsg = "GoogleTests/Begin";

            _siteUrl = "https://duckduckgo.com/";

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

                        SetTextFieldByName(browser, HtmlControls.SearchBox, searchFor);

                        browser.Button(Find.ById(HtmlControls.Submit)).Click();
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
