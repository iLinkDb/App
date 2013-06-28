using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AppCommon
{
    public abstract class TestBase
    {
        private HtmlLogFile _html;

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

        public void ShowError(string message)
        {
            Console.WriteLine("ERROR: " + message);
            Logging.LogError(message);
            _html.WriteRow(message, "error");
        }

        public void ShowError(string message, Exception ex)
        {
            Console.WriteLine(message);
            Logging.LogError(message, ex);
            _html.WriteRow(message, "error");
        }

        public void ShowErrorLink(string message, string link)
        {
            Console.WriteLine("ERROR: " + message);
            Logging.LogError(message);
            _html.WriteRow(link, "error");
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

        public void WriteScreenShot(string message)
        {
            string imgName = "img" + DateTime.Now.Ticks.ToString();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<a href='file://{0}' ", Path.Combine(_html.FilePath, imgName + ".jpg"));
            sb.AppendFormat(" target='_blank' >{0} ({1}.jpg)", message, imgName);
            sb.Append("</a>");
            ShowErrorLink(message, sb.ToString());

//            browser.BringToFront();
            _html.CaptureScreen(imgName);
        }


    }
}
