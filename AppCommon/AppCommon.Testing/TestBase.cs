using System;
using System.Text;
using System.IO;
using WatiN.Core;

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

      public void ClearCache(IE browser)
      {
         browser.ClearCache();
         browser.WaitForComplete();
         ShowTrace("Clearing Cache");

         browser.ClearCookies();
         browser.WaitForComplete();
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
            field.WaitForComplete();
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
            field.WaitForComplete();
            ShowSuccess(msg);
         }
      }

      public void SetSelectListById(IE browser, string fieldName, string value)
      {
         string msg = string.Format("SetSelectListById for {0} to: <b>{1}</b> (by Id)",
            fieldName, value);
         SelectList list = browser.SelectList(Find.ById(fieldName));
         if (list == null)
         { ShowError(msg); }
         else
         {
            list.Select(value);
            list.Change();
            ShowSuccess(msg);
         }
      }

      public void SetSelectListByName(IE browser, string fieldName, string value)
      {
         string msg = string.Format("SetSelectListByName for {0} to: <b>{1}</b> (by Id)",
            fieldName, value);
         SelectList list = browser.SelectList(Find.ByName(fieldName));
         if (list == null)
         { ShowError(msg); }
         else
         {
            list.Select(value);
            list.WaitForComplete();

            try
            { list.Change(); }
            catch (UnauthorizedAccessException ex)
            {
               //
               // http://stackoverflow.com/questions/959150/access-denied-error-visual-studio-and-watin
               // http://stackoverflow.com/questions/4120547/watin-unauthorizedaccessexception-errors
               //
            }

            ShowSuccess(msg);
         }
      }

      public void SetRadioByName(IE browser, string name, int index)
      {
         //
         // http://stackoverflow.com/questions/6687783/watin-how-to-check-one-radio-button-out-of-a-radio-button-group
         //
         string msg = string.Format("SetRadioByName for {0} index: <b>{1}</b> (by Name)",
                name, index);
         RadioButton radio = browser.RadioButton(Find.ByName(name) && Find.ByIndex(index));
         if (radio == null)
         { ShowError(msg); }
         else
         {
            radio.Click();
            radio.WaitForComplete();
            ShowSuccess(msg);
         }
         browser.WaitForComplete();
      }

      public bool Assert(IE browser, string lookFor)
      {
         bool retVal = browser.ContainsText(lookFor);

         // If not found, check frames too
         if (retVal == false)
         {
            foreach (Frame frame in browser.Frames)
            {
               if (frame.ContainsText(lookFor))
               {
                  retVal = true;
                  break;
               }
            }
         }

         if (retVal)
         { ShowSuccess("Assert-Success, found: <b>" + lookFor + "</b>"); }
         else
         { WriteScreenShot(browser, "Assert-Failed, Did NOT find: <b>" + lookFor + "</b>"); }

         return retVal;
      }

      public void SetFocusToBody(IE browser)
      {
         browser.Body.Focus();
         browser.WaitForComplete();
      }

      public void ClickButtonById(IE browser, string fieldName)
      {
         string msg = string.Format("ClickButtonById for {0} (by Id)",
                fieldName);
         Button field = browser.Button(Find.ById(fieldName));

         if (field == null)
         { ShowError(msg); }
         else
         {
            field.Click();
            field.WaitForComplete();
            ShowTrace(msg);
         }

      }

      public void ClickLinkByText(IE browser, string fieldName)
      {
         string msg = string.Format("ClickLinkByText for {0} (by Id)",
                fieldName);
         Link field = browser.Link(Find.ByText(fieldName));

         if (field == null)
         { ShowError(msg); }
         else
         {
            field.Click();
            field.WaitForComplete();
            ShowTrace(msg);
         }

      }
   }
}
