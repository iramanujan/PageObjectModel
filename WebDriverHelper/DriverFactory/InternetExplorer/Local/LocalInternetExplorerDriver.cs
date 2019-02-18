using CommonHelper.Helper.Files;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;
using WebDriverHelper.DriverFactory.Base;
using WebDriverHelper.DriverFactory.InternetExplorer.Options;
using WebDriverHelper.Interfaces.DriverFactory;

namespace WebDriverHelper.DriverFactory.InternetExplorer.Local
{
    public class LocalInternetExplorerDriver : BaseLocalDriverFactory, IWebDriverFactory
    {
        private IWebDriver webDriver = null;
        private InternetExplorerOptions internetExplorerOptions = null;
        private InternetExplorerDriverService internetExplorerDriverService = null;


        private void BeforeWebDriverSetupSetps()
        {
            this.internetExplorerOptions = InternetExplorerDriverOptions.GetInternetExplorerOptions();
            this.internetExplorerDriverService = InternetExplorerDriverService.CreateDefaultService(FileHelper.GetCurrentlyExecutingDirectory());

        }

        public IWebDriver InitializeWebDriver()
        {
            BeforeWebDriverSetupSetps();
            webDriver = new InternetExplorerDriver(internetExplorerDriverService, internetExplorerOptions, TimeSpan.FromSeconds(toolConfigMember.CommandTimeout));
            return this.webDriver;
        }
    }
}
