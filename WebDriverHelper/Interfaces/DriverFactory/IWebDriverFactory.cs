using OpenQA.Selenium;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.Interfaces.DriverFactory
{
    interface IWebDriverFactory
    {
        IWebDriver Create();

        string DownloadLocation { get; }
        //WebDriverUploadDirectory UploadLocation { get; }
        LocalizationType Localization { get; }
    }
}
