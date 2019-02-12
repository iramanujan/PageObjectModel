using CommonHelper.Helper.Config;
using CommonHelper.Setup.Upload;
using OpenQA.Selenium;
using System;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory.Base
{
    public abstract class BaseDriverFactory
    {
        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        protected readonly TimeSpan commandTimeout = TimeSpan.FromMilliseconds(toolConfigMember.CommandTimeout);

        public abstract string DownloadLocationPath { get; }

        public abstract UploadLocation UploadLocation { get; }

        public LocalizationType Localization { get; }

        protected BaseDriverFactory(LocalizationType localizationType) => Localization = localizationType;

        protected abstract void BeforeWebDriverSetupSetps();

        protected abstract IWebDriver WebDriverSetupSetps();

        protected abstract void AfterWebDriverSetupSetps();

        protected void SetTimeOut(IWebDriver webDriver)
        {
            var timeouts = webDriver.Manage().Timeouts();
            timeouts.AsynchronousJavaScript = TimeSpan.FromSeconds(120);
            timeouts.ImplicitWait = TimeSpan.FromSeconds(1);
            timeouts.PageLoad = TimeSpan.FromSeconds(toolConfigMember.PageLoadWait);
        }

        protected void MaximizeBrowser(IWebDriver webDriver)
        {
            webDriver.Manage().Window.Maximize();
        }

    }
}
