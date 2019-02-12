
using CommonHelper.Setup.Upload;
using OpenQA.Selenium;
using WebDriverHelper.DriverFactory.Base;
using WebDriverHelper.DriverFactory.Chrome.Options;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory.Chrome.Remote
{
    public class ChromeDriverRemote : BaseRemoteDriverFactory
    {
        private IWebDriver webDriver = null;

        public override string DownloadLocationPath => downloadLocation.Value;

        public override UploadLocation UploadLocation => uploadLocation.Value;

        protected override ICapabilities Capabilities => ChromeDriverOptions.CreateDefaultChromeOptions(DownloadLocationPath, Localization).ToCapabilities();

        public ChromeDriverRemote(LocalizationType localizationType) : base(localizationType)
        {
            
        }

    }
}
