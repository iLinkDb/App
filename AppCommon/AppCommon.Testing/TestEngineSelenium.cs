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

        public bool Assert(string lookFor)
        {
            const int WAIT_SECONDS = 15;
            WebDriverWait wait = new WebDriverWait(_browser, new TimeSpan(0, 0, WAIT_SECONDS));

            bool retVal = wait.Until(d => d.PageSource.Contains(lookFor));

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

        #region Links

        public void LinkClickById(string linkId)
        { LinkClick(linkId, SelectByEnum.Id); }

        public void LinkClickByText(string linkText)
        { LinkClick(linkText, SelectByEnum.Name); }

        private void LinkClick(string linkFind, SelectByEnum selectBy)
        {
            string msg = string.Format("LinkClick{0} for {1}",
                   selectBy, linkFind);

            IWebElement element;
            if (selectBy == SelectByEnum.Id)
            { element = _browser.FindElement(By.Id(linkFind)); }
            else
            { element = _browser.FindElement(By.LinkText(linkFind)); }

            if (element == null)
            {
                ShowError(msg);
            }
            else
            {
                element.Click();
                ShowSuccess(msg);
            }
        }

        #endregion

        #region Text Input

        public void SetTextFieldById(string fieldName, string value)
        { SetTextField(fieldName, value, SelectByEnum.Id); }

        public void SetTextFieldByName(string fieldName, string value)
        { SetTextField(fieldName, value, SelectByEnum.Name); }

        private void SetTextField(string fieldName, string value, SelectByEnum selectBy)
        {
            string msg = string.Format("SetTextFieldBy{0} for {1} to: <b>{2}</b>",
                   selectBy, fieldName, value);

            try
            {
                IWebElement element;
                if (selectBy == SelectByEnum.Id)
                { element = _browser.FindElement(By.Name(fieldName)); }
                else
                { element = _browser.FindElement(By.Name(fieldName)); }

                if (element == null)
                {
                    ShowError(msg);
                }
                else
                {
                    element.SendKeys(value);
                    ShowSuccess(msg);
                }
            }
            catch (Exception ex)
            {
                ShowError(msg);
            }
        }

        #endregion

    }
}
