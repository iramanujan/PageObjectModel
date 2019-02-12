using CommonHelper.Helper.Config;
using OpenQA.Selenium.Chrome;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory.Chrome.Options
{
    class ChromeDriverOptions
    {
        public static ChromeOptions CreateDefaultChromeOptions(string downloadLocation, LocalizationType localizationType)
        {
            var options = new ChromeOptions();

            options.AddUserProfilePreference("safebrowsing.enabled", true);
            options.AddUserProfilePreference("download.default_directory", downloadLocation);

            options.AddArguments("--test-type");
            options.AddArguments("--no-sandbox");
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--incognito");
            options.AddArgument("--enable-precise-memory-info");
            options.AddArgument("--disable-default-apps");
            options.AddArgument("test-type=browser");
            options.AddArgument("disable-infobars");

            if (ToolConfigReader.ToolConfigMembers.NoCache)
            {
                options.AddArguments("--incognito");
            }

            options.AddArguments($"--lang={localizationType.ToString()}");

            return options;
        }
    }
}
