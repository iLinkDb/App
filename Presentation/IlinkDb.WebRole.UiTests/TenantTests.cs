using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using WatiN.Core;

using AppCommon;

namespace IlinkDb.WebRole.UiTests
{
    public class TenantTests : TestBase
    {
        public void Begin(HtmlLogFile htmlLogFile)
        {
            string logMsg = "DesktopMainHappyPathTest/Begin";

            try
            {
                WriteStartTest(htmlLogFile, logMsg);
                using (IE browser = new IE(_siteUrl))
                {
                    try
                    {
                        ClearCache(browser);

                        TestData data = new TestData(TestDataEnum.BarneyRubble);

                        FillForm(browser, data);

                        SubmitForm(browser);
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

        private void FillForm(IE browser, TestData data)
        {
            SetTextFieldByName(browser, HtmlControls.CardNumber, data.CardNumber);

            SetTextFieldByName(browser, HtmlControls.CardName, data.CardHolderName);

            SetSelectListById(browser, HtmlControls.CardMonth, data.CardMonth);

            SetSelectListById(browser, HtmlControls.CardYear, data.CardYear);

            SetTextFieldByName(browser, HtmlControls.CardCode, data.CardCode);

            SetTextFieldByName(browser, HtmlControls.CardZip, data.CardZip);

            SetTextFieldByName(browser, HtmlControls.Name, data.ClientName);

            SetTextFieldById(browser, HtmlControls.Inv1Number, data.Inv1Number);

            SetTextFieldById(browser, HtmlControls.Inv1Amount, data.Inv1Amount);

            SetTextFieldByName(browser, HtmlControls.Comment, data.Comment);
        }

        private void SubmitForm(IE browser)
        {
            ShowTrace("DesktopMainTest/SubmitForm");

            browser.Button(Find.ById(HtmlControls.Submit)).Click();
            browser.WaitForComplete();
            browser.WaitUntilContainsText("Confirm Payment");
        }
    }
}
