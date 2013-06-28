using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppCommon;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace IlinkDb.WebRole.UiTests
{
   public class DuckDuckSelenium : TestBaseSelenium, ITestBase
   {
      private string _siteUrl = "http://www.duckduckgo.com";

      public void Begin(HtmlLogFile htmlLogFile)
      {
         Begin(htmlLogFile, _siteUrl);
      }

      public void Begin(HtmlLogFile htmlLogFile, string siteUrl)
      {
         string logMsg = "DuckDuckSelenium/Begin";

         Console.WriteLine("Url: {0}" , siteUrl);

         try
         {
             TestEngineSelenium test = new TestEngineSelenium(siteUrl, htmlLogFile, logMsg);

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
