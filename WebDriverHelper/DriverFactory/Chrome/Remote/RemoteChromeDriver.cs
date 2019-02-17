using OpenQA.Selenium;
using WebDriverHelper.DriverFactory.Base;
using WebDriverHelper.DriverFactory.Chrome.Options;

namespace WebDriverHelper.DriverFactory.Chrome.Remote
{
    public class RemoteChromeDriver : BaseRemoteDriverFactory
    {
        private IWebDriver webDriver = null;

        protected override ICapabilities Capabilities => ChromeDriverOptions.CreateDefaultChromeOptions().ToCapabilities();
        
    }
}
