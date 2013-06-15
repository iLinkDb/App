using System;
using System.Text;
using System.IO;
using WatiN.Core;

namespace AppCommon
{
    public abstract class TestBase
    {
        private HtmlLogFile _html;
        public string _siteUrl = "http://localhost:51791/";

        public void WriteStartTest(HtmlLogFile html, string testName)
        {
            _html = html;
            _html.WriteStartTest(testName);
            Console.WriteLine("Beginning Test: " + testName);
        }

        public void WriteEndTest()
        {
            _html.WriteEndTest();
        }

        public void ClearCache(IE browser)
        {
            browser.ClearCache();
            ShowTrace("Clearing Cache");
            browser.ClearCookies();
            ShowTrace("Clearing Cookies");
        }

        public void WriteScreenShot(IE browser, string message)
        {
            string imgName = "img" + DateTime.Now.Ticks.ToString();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a href='file://{0}' ", Path.Combine(_html.FilePath, imgName + ".jpg"));
            sb.AppendFormat(" target='_blank' >{0} ({1}.jpg)", message, imgName);
            sb.Append("</a>");
            ShowErrorLink(message, sb.ToString());

            browser.BringToFront();
            _html.CaptureScreen(imgName);
        }

        public void ShowError(string message)
        {
            Console.WriteLine("ERROR: " + message);
            Logging.LogError(message);
            _html.WriteRow(message, "error");
        }

        public void ShowErrorLink(string message, string link)
        {
            Console.WriteLine("ERROR: " + message);
            Logging.LogError(message);
            _html.WriteRow(link, "error");
        }

        public void ShowError(string message, Exception ex)
        {
            Console.WriteLine(message);
            Logging.LogError(message, ex);
            _html.WriteRow(message, "error");
        }

        public void ShowTrace(string message)
        {
            Console.WriteLine(message);
            Logging.LogTrace(message);
            _html.WriteRow(message);
        }

        public void ShowSuccess(string message)
        {
            Console.WriteLine(message);
            Logging.LogInfo(message);
            _html.WriteRow(message, "success");
        }

        public void SetTextFieldById(IE browser, string fieldName, string value)
        {
            string msg = string.Format("SetTextFieldById for {0} to: <b>{1}</b> (by Id)",
                   fieldName, value);
            TextField field = browser.TextField(Find.ById(fieldName));
            if (field == null)
            { ShowError(msg); }
            else
            {
                field.TypeText(value);
                ShowSuccess(msg);
            }
        }

        public void SetTextFieldByName(IE browser, string fieldName, string value)
        {
            string msg = string.Format("SetTextFieldByName for {0} to: <b>{1}</b> (by Name)",
                   fieldName, value);
            TextField field = browser.TextField(Find.ByName(fieldName));
            if (field == null)
            { ShowError(msg); }
            else
            {
                field.TypeText(value);
                ShowSuccess(msg);
            }
        }

        public void SetSelectListById(IE browser, string fieldName, string value)
        {
            string msg = string.Format("SetSelectListById for {0} to: <b>{1}</b> (by Id)",
               fieldName, value);
            SelectList list = browser.SelectList(Find.ById(fieldName));
            if (list == null)
            {
                list.Select(value);
                ShowSuccess(msg);
            }
            else
            { ShowError(msg); }
        }

        public bool Assert(IE browser, string lookFor)
        {
            bool retVal = browser.ContainsText(lookFor);
            if (retVal)
            { ShowSuccess("Assert-Success, found: " + lookFor); }
            else
            {
                WriteScreenShot(browser, "Assert-Failed, Did NOT find: " + lookFor);
            }
            return retVal;
        }
    }
}
