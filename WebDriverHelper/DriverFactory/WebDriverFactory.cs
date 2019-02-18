using WebDriverHelper.DriverFactory.Chrome.Local;
using WebDriverHelper.DriverFactory.Chrome.Remote;
using WebDriverHelper.DriverFactory.FireFox.Local;
using WebDriverHelper.DriverFactory.FireFox.Remote;
using WebDriverHelper.DriverFactory.InternetExplorer.Local;
using WebDriverHelper.DriverFactory.InternetExplorer.Remote;
using WebDriverHelper.Interfaces.DriverFactory;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory
{
    public static class WebDriverFactory
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
