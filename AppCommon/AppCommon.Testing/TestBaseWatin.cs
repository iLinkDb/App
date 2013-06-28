using System;
using System.Text;
using System.IO;

using WatiN.Core;

namespace AppCommon
{
   internal enum SelectTextFieldEnum
   {
      Id,
      Name
   }

   public abstract class TestBaseWatin : TestBase
   {
      public void ClearCache(IE browser)
      {
         browser.ClearCache();
         browser.WaitForComplete();
         ShowTrace("Clearing Cache");

         browser.ClearCookies();
         browser.WaitForComplete();
         ShowTrace("Clearing Cookies");
      }

      public void SetTextFieldById(IE browser, string fieldName, string value)
      { SetTextField(browser, fieldName, value, SelectTextFieldEnum.Id); }

      public void SetTextFieldByName(IE browser, string fieldName, string value)
      { SetTextField(browser, fieldName, value, SelectTextFieldEnum.Name); }

      private void SetTextField(IE browser, string fieldName, string value, SelectTextFieldEnum selectBy)
      {
         string msg = string.Format("SetTextFieldBy{0} for {1} to: <b>{2}</b>",
                selectBy, fieldName, value);

         TextField field;
         if (selectBy == SelectTextFieldEnum.Id)
         { field = browser.TextField(Find.ById(fieldName)); }
         else
         { field = browser.TextField(Find.ByName(fieldName)); }

         if (field == null || (!field.Exists))
         {
            // If we didn't find the text field, search the Elements collection.
            // (In jQuery Mobile, numeric text fields are of type 'tel')
            Element element;

            if (selectBy == SelectTextFieldEnum.Id)
            { element = browser.Element(Find.ById(fieldName)); }
            else
            { element = browser.Element(Find.ByName(fieldName)); }

            if (element == null || (!element.Exists))
            { ShowError(msg); }
            else
            {
               element.SetAttributeValue("value", value);
               element.WaitForComplete();
               ShowSuccess(msg);
            }
         }
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
         { WriteScreenShot("Assert-Failed, Did NOT find: <b>" + lookFor + "</b>"); }

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
