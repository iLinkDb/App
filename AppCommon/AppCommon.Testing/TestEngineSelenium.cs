using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace AppCommon
{
    public class TestEngineSelenium : TestBase, ITestEngine
    {
        private string _siteUrl;
        private IWebDriver _browser;

        public TestEngineSelenium(string siteUrl, HtmlLogFile htmlLogFile, string logMsg)
        {
            string driverName = "chromedriver.exe";
            string driverPath = Environment.CurrentDirectory + "\\drivers";

            _siteUrl = siteUrl;

            if (File.Exists(Path.Combine(driverPath, driverName)))
            {
                ChromeOptions options = new ChromeOptions();
                //options.AddArgument("start-maximized");
                _browser = new ChromeDriver(driverPath, options);
                _browser.Navigate().GoToUrl(_siteUrl);
            }
            else
            { throw new FileNotFoundException("ChromeDriver.exe not found", driverPath); }

            WriteStartTest(htmlLogFile, logMsg);

            ClearCache();
        }

        public void Cleanup()
        {
            WriteEndTest();
            _browser.Quit();
        }

        public void SetTextFieldById(string fieldName, string value)
        { SetTextField(fieldName, value, SelectTextFieldEnum.Id); }

        public void SetTextFieldByName(string fieldName, string value)
        { SetTextField(fieldName, value, SelectTextFieldEnum.Name); }

        #region Private Methods

        private void SetTextField(string fieldName, string value, SelectTextFieldEnum selectBy)
        {
            string msg = string.Format("SetTextFieldBy{0} for {1} to: <b>{2}</b>",
                   selectBy, fieldName, value);

            IWebElement field;
            if (selectBy == SelectTextFieldEnum.Id)
            { field = _browser.FindElement(By.Name(fieldName)); }
            else
            { field = _browser.FindElement(By.Name(fieldName)); }

            if (field == null )
            {
                 ShowError(msg); 
            }
            else
            {
                field.SendKeys(value);
                ShowSuccess(msg);
            }
        }

        #endregion

        public bool Assert(string lookFor)
        {
            bool retVal = _browser.PageSource.Contains(lookFor);

            //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            //return wait.Until(drv => drv.FindElement(by));

            if (retVal)
            { ShowSuccess("Assert-Success, found: <b>" + lookFor + "</b>"); }
            else
            { WriteScreenShot("Assert-Failed, Did NOT find: <b>" + lookFor + "</b>"); }

            return retVal;
        }

        public void ButtonClickById(string fieldName)
        {
            IWebElement element = _browser.FindElement(By.Id(fieldName));
            element.Submit();
        }

        public void ClearCache()
        {
            // Not sure what to do here yet...
        }

        public void ClickLinkByText(string fieldName)
        {
            throw new NotImplementedException();
        }

        public void SetFocusToBody()
        {
            throw new NotImplementedException();
        }

        public void SetRadioByName(string name, int index)
        {
            throw new NotImplementedException();
        }

        public void SetSelectListById(string fieldName, string value)
        {
            throw new NotImplementedException();
        }

        public void SetSelectListByName(string fieldName, string value)
        {
            throw new NotImplementedException();
        }
    }
}
