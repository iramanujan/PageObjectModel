using WebDriverHelper.WebDriverFactory.Chrome.Local;
using WebDriverHelper.WebDriverFactory.Chrome.Remote;
using WebDriverHelper.WebDriverFactory.FireFox.Local;
using WebDriverHelper.WebDriverFactory.FireFox.Remote;
using WebDriverHelper.WebDriverFactory.InternetExplorer.Local;
using WebDriverHelper.WebDriverFactory.InternetExplorer.Remote;
using WebDriverHelper.Interfaces.DriverFactory;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace ConsoleApp1
{
    public static class WebDriverType
    {

        public static IWebDriverFactory CreateWebDriverFactory(BrowserType browserType, WebDriverExecutionType webDriverExecutionType)
        {
            IWebDriverFactory webDriverFactory = null;

            if (browserType.Equals(BrowserType.Chrome) && webDriverExecutionType.Equals(WebDriverExecutionType.Local))
                webDriverFactory = new LocalChromeDriver();

            if (browserType.Equals(BrowserType.Firefox) && webDriverExecutionType.Equals(WebDriverExecutionType.Local))
                webDriverFactory = new LocalFireFoxDriver();

            if (browserType.Equals(BrowserType.IE) && webDriverExecutionType.Equals(WebDriverExecutionType.Local))
                webDriverFactory = new LocalInternetExplorerDriver();

            if (browserType.Equals(BrowserType.Chrome) && webDriverExecutionType.Equals(WebDriverExecutionType.Grid))
                webDriverFactory = new RemoteChromeDriver();

            if (browserType.Equals(BrowserType.Firefox) && webDriverExecutionType.Equals(WebDriverExecutionType.Grid))
                webDriverFactory = new RemoteFireFoxDriver();

            if (browserType.Equals(BrowserType.IE) && webDriverExecutionType.Equals(WebDriverExecutionType.Grid))
                webDriverFactory = new RemoteInternetExplorerDriver();

            return webDriverFactory;
        }

    }
}
