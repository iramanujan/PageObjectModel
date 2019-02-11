using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory.Chrome.Local
{
    class ChromeDriverLocal
    {
        public ChromeDriverLocal(LocalizationType localizationType) : base(localizationType)
        {
        }

        protected override IWebDriver CreateDriver()
        {
            var options = ChromeDriverOptions.CreateDefaultChromeOptions(DownloadLocation, localizationType);
            var service = ChromeDriverService.CreateDefaultService(FileUtils.GetCurrentlyExecutingDirectory());
            var driver = new ChromeDriver(service, options, commandTimeout);
            return driver;
        }

        private readonly Lazy<string> downloadLocation =
            new Lazy<string>(
                () => WebDriverInitUtils.CreateWebDriverDirectory(ToolConfig.Browser + ToolConfig.ExecutionType.ToString(), ToolConfig.RootDownloadLocation));

        public override string DownloadLocation => downloadLocation.Value;
    }
}
