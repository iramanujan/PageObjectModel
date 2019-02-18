using CommonHelper.Helper.Config;
using OpenQA.Selenium;
using System;
using WebDriverHelper.Interfaces.DriverFactory;
using WebDriverHelper.JScript;

namespace WebDriverHelper.BrowserFactory
{
    public class Browser
    {
        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        private readonly IWebDriverFactory webDriverFactory;
        private IWebDriver objWebDriver;

        private IWebDriver webDriver => webDriver ?? (objWebDriver = webDriverFactory.InitializeWebDriver());

        public Browser(IWebDriverFactory webDriverFactory)
        {
            this.webDriverFactory = webDriverFactory;
        }

        public Browser(IWebDriver webDriver)
        {
            this.objWebDriver = webDriver;
        }

        public void GetDownloadLocation()
        {
            toolConfigMember.RootDownloadLocation.ToString();
        }

        public void GetUploadLocation()
        {
            toolConfigMember.RootUploadLocation.ToString();
        }

        public Browser Back()
        {
            webDriver.Navigate().Back();
            return this;
        }

        public Browser Forward()
        {
            webDriver.Navigate().Forward();
            return this;
        }

        public Browser Refresh()
        {
            webDriver.Navigate().Refresh();
            JavaScript.ExecuteScript(JScriptType.PageLoad, this.webDriver);
            return this;
        }

        public bool WaitTillPageLoad(int numberOfSeconds)
        {
            //try
            //{
            //    Wait(numberOfSeconds).Until((driver) =>
            //    {
            //        try
            //        {
            //            return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString().Contains("complete");
            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine(e);
            //            return false;
            //        }
            //    });
            //}
            //catch (WebDriverTimeoutException)
            //{
            //    // If timeout, then page is not loaded. For all other exceptions, do not catch.
            //}
            return false;
        }
    }
}
