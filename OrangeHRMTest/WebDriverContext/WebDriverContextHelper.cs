using CommonHelper.Helper.Config;
using WebDriverHelper.WebBrowserFactory;
using WebDriverHelper.WebDriverFactory;
using WebDriverHelper.Interfaces.DriverFactory;

namespace OrangeHRMTest.WebDriverContext
{
    public class WebDriverContextHelper
    {
        public readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        private IWebDriverFactory webDriverFactory;
        public BrowserFactory BrowserFactory { get; }

        public WebDriverContextHelper()
        {
            webDriverFactory = new WebDriverFactory().CreateWebDriverFactory(ToolConfigReader.GetToolConfig().Browser, ToolConfigReader.GetToolConfig().ExecutionType);
            BrowserFactory = new BrowserFactory(webDriverFactory);
        }
    }
}
