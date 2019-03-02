using CommonHelper.Helper.Files;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverHelper.WebDriverFactory.Base;
using WebDriverHelper.WebDriverFactory.Chrome.Options;
using WebDriverHelper.Interfaces.DriverFactory;

namespace WebDriverHelper.WebDriverFactory.Chrome.Local
{
    public class LocalChromeDriver : BaseLocalDriverFactory, IWebDriverFactory
    {
        private IWebDriver webDriver = null;
        private ChromeOptions chromeOptions = null;
        private ChromeDriverService chromeDriverService = null;

        private void BeforeWebDriverSetupSetps()
        {
            this.chromeOptions = ChromeDriverOptions.CreateDefaultChromeOptions();
            this.chromeDriverService = ChromeDriverService.CreateDefaultService(FileHelper.GetCurrentlyExecutingDirectory());
        }

        public IWebDriver InitializeWebDriver()
        {
            BeforeWebDriverSetupSetps();
            webDriver = new ChromeDriver(chromeDriverService, chromeOptions, TimeSpan.FromSeconds(toolConfigMember.CommandTimeout));
            return this.webDriver;
        }
    }
}
