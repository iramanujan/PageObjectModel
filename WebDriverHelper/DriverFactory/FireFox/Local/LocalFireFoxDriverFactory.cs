using System;
using CommonHelper.Helper.Files;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverHelper.DriverFactory.Base;
using WebDriverHelper.DriverFactory.FireFox.Profile;
using WebDriverHelper.Interfaces.DriverFactory;

namespace WebDriverHelper.DriverFactory.FireFox.Local
{
    public class LocalFireFoxDriverFactory : BaseLocalDriverFactory, IWebDriverFactory
    {
        
        private FirefoxOptions firefoxOptions = null;
        private FirefoxDriverService firefoxDriverService = null;
        private FirefoxProfile firefoxProfile = null;

        protected void BeforeWebDriverSetupSetps()
        {
            firefoxProfile = FireFoxDriverProfile.CreateProfile();
            firefoxDriverService = FirefoxDriverService.CreateDefaultService(FileHelper.GetCurrentlyExecutingDirectory());
            firefoxOptions = new FirefoxOptions();
            firefoxOptions.Profile = firefoxProfile;
            firefoxOptions.LogLevel = FirefoxDriverLogLevel.Info;
        }

        public IWebDriver InitializeWebDriver()
        {
            BeforeWebDriverSetupSetps();
            var firefoxDriver = new FirefoxDriver(firefoxDriverService, firefoxOptions, TimeSpan.FromSeconds(30));
            return firefoxDriver;
        }
    }
}
