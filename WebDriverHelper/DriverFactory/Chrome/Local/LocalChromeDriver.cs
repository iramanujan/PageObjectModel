using CommonHelper.Helper.Files;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverHelper.DriverFactory.Base;
using WebDriverHelper.DriverFactory.Chrome.Options;

namespace WebDriverHelper.DriverFactory.Chrome.Local
{
    class LocalChromeDriver : BaseLocalDriverFactory
    {
        private IWebDriver webDriver = null;
        private ChromeOptions chromeOptions = null;
        private ChromeDriverService chromeDriverService = null;

        protected void BeforeWebDriverSetupSetps()
        {
            this.chromeOptions = ChromeDriverOptions.CreateDefaultChromeOptions();
            this.chromeDriverService = ChromeDriverService.CreateDefaultService(FileHelper.GetCurrentlyExecutingDirectory());
        }

        protected IWebDriver WebDriverSetupSetps()
        {
            webDriver = new ChromeDriver(chromeDriverService, chromeOptions, TimeSpan.FromSeconds(toolConfigMember.CommandTimeout));
            return this.webDriver;
        }
    }
}
