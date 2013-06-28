using System;
using System.Text;
using System.IO;

using WatiN.Core;

namespace AppCommon
{
    public class TestEngineWatin : TestBase, ITestEngine
    {
        private string _siteUrl;
        private IE _browser;

        public TestEngineWatin(string siteUrl, HtmlLogFile htmlLogFile, string logMsg)
        {
            _siteUrl = siteUrl;
            _browser = new IE(_siteUrl);
            WriteStartTest(htmlLogFile, logMsg);

            ClearCache();
        }

        public void Cleanup()
        {
            WriteEndTest();
            _browser.Close();
        }

        public void ClearCache()
        {
            _browser.ClearCache();
            _browser.WaitForComplete();
            ShowTrace("Clearing Cache");

            _browser.ClearCookies();
            _browser.WaitForComplete();
            ShowTrace("Clearing Cookies");
        }

        public void SetTextFieldById(string fieldName, string value)
        { SetTextField(fieldName, value, SelectTextFieldEnum.Id); }

        public void SetTextFieldByName(string fieldName, string value)
        { SetTextField(fieldName, value, SelectTextFieldEnum.Name); }

        private void SetTextField(string fieldName, string value, SelectTextFieldEnum selectBy)
        {
            string msg = string.Format("SetTextFieldBy{0} for {1} to: <b>{2}</b>",
                   selectBy, fieldName, value);

            TextField field;
            if (selectBy == SelectTextFieldEnum.Id)
            { field = _browser.TextField(Find.ById(fieldName)); }
            else
            { field = _browser.TextField(Find.ByName(fieldName)); }

            if (field == null || (!field.Exists))
            {
                // If we didn't find the text field, search the Elements collection.
                // (In jQuery Mobile, numeric text fields are of type 'tel')
                Element element;

                if (selectBy == SelectTextFieldEnum.Id)
                { element = _browser.Element(Find.ById(fieldName)); }
                else
                { element = _browser.Element(Find.ByName(fieldName)); }

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

        public void SetSelectListById(string fieldName, string value)
        {
            string msg = string.Format("SetSelectListById for {0} to: <b>{1}</b> (by Id)",
               fieldName, value);
            SelectList list = _browser.SelectList(Find.ById(fieldName));
            if (list == null)
            { ShowError(msg); }
            else
            {
                list.Select(value);
                list.Change();
                ShowSuccess(msg);
            }
        }

        public void SetSelectListByName(string fieldName, string value)
        {
            string msg = string.Format("SetSelectListByName for {0} to: <b>{1}</b> (by Id)",
               fieldName, value);
            SelectList list = _browser.SelectList(Find.ByName(fieldName));
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

        public void SetRadioByName(string name, int index)
        {
            //
            // http://stackoverflow.com/questions/6687783/watin-how-to-check-one-radio-button-out-of-a-radio-button-group
            //
            string msg = string.Format("SetRadioByName for {0} index: <b>{1}</b> (by Name)",
                   name, index);
            RadioButton radio = _browser.RadioButton(Find.ByName(name) && Find.ByIndex(index));
            if (radio == null)
            { ShowError(msg); }
            else
            {
                radio.Click();
                radio.WaitForComplete();
                ShowSuccess(msg);
            }
            _browser.WaitForComplete();
        }

        public bool Assert(string lookFor)
        {
            bool retVal = _browser.ContainsText(lookFor);

            // If not found, check frames too
            if (retVal == false)
            {
                foreach (Frame frame in _browser.Frames)
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

        public void SetFocusToBody()
        {
            _browser.Body.Focus();
            _browser.WaitForComplete();
        }

        public void ButtonClickById(string fieldName)
        {
            string msg = string.Format("ClickButtonById for {0} (by Id)",
                   fieldName);
            Button field = _browser.Button(Find.ById(fieldName));

            if (field == null)
            { ShowError(msg); }
            else
            {
                field.Click();
                field.WaitForComplete();
                ShowTrace(msg);
            }
        }

        public void ClickLinkByText(string fieldName)
        {
            string msg = string.Format("ClickLinkByText for {0} (by Id)",
                   fieldName);
            Link field = _browser.Link(Find.ByText(fieldName));

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
