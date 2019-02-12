using CommonHelper.Helper.Config;
using CommonHelper.Helper.Files;
using CommonHelper.Setup.Download;
using CommonHelper.Setup.Upload;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverHelper.DriverFactory.Base;
using WebDriverHelper.DriverFactory.Chrome.Options;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory.Chrome.Local
{
    class ChromeDriverLocal : BaseLocalDriverFactory
    {
        private IWebDriver webDriver = null;
        private ChromeOptions chromeOptions = null;
        private ChromeDriverService chromeDriverService = null;

        public override String DownloadLocationPath => downloadLocation.Value;

        public override UploadLocation UploadLocation => uploadLocation.Value;

        public ChromeDriverLocal(LocalizationType localizationType) : base(localizationType)
        {
        }

        protected override void BeforeWebDriverSetupSetps()
        {
            this.chromeOptions = ChromeDriverOptions.CreateDefaultChromeOptions(DownloadLocationPath, Localization);
            this.chromeDriverService = ChromeDriverService.CreateDefaultService(FileHelper.GetCurrentlyExecutingDirectory());
        }

        protected override IWebDriver WebDriverSetupSetps()
        {
            this.webDriver = new ChromeDriver(this.chromeDriverService, this.chromeOptions, commandTimeout);
            return this.webDriver;
        }

        protected override void AfterWebDriverSetupSetps()
        {
            SetTimeOut(this.webDriver);
            MaximizeBrowser(this.webDriver);
        }

    }
}
