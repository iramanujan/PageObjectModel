using System;
using CommonHelper.Helper.Files;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverHelper.WebDriverFactory.Base;
using WebDriverHelper.WebDriverFactory.FireFox.Profile;
using WebDriverHelper.Interfaces.DriverFactory;

namespace WebDriverHelper.WebDriverFactory.FireFox.Local
{
    public class LocalFireFoxDriver : BaseLocalDriverFactory, IWebDriverFactory
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
            var firefoxDriver = new FirefoxDriver(firefoxDriverService, firefoxOptions, TimeSpan.FromSeconds(toolConfigMember.CommandTimeout));
            return firefoxDriver;
        }
    }
}
