using CommonHelper.Helper.Config;
using CommonHelper.Helper.Log;
using CommonHelper.Setup.Download;
using CommonHelper.Setup.Upload;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using WebDriverHelper.Grid;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory.Base
{
    public abstract class BaseRemoteDriverFactory : BaseDriverFactory
    {
        private IWebDriver webDriver;
        protected BaseRemoteDriverFactory(LocalizationType localizationType) : base(localizationType)
        {
        }

        protected abstract ICapabilities Capabilities { get; }

        protected readonly Lazy<string> downloadLocation = new Lazy<string>(() => DownloadLocation.CreateWebDriverDirectory(ToolConfigReader.GetToolConfig().Browser.ToString() + ToolConfigReader.GetToolConfig().ExecutionType.ToString(), ToolConfigReader.GetToolConfig().RootDownloadLocation));
        protected readonly Lazy<UploadLocation> uploadLocation = new Lazy<UploadLocation>(() => UploadLocation.Create(ToolConfigReader.GetToolConfig().Browser.ToString() + ToolConfigReader.GetToolConfig().ExecutionType.ToString(), true, ToolConfigReader.GetToolConfig().RootUploadLocation));


        protected override void BeforeWebDriverSetupSetps()
        {
            GridConfigHelper.WaitForFreeSlotOnHubForBrowser(ToolConfigReader.GetToolConfig().GridHost, TimeSpan.FromMilliseconds(ToolConfigReader.GetToolConfig().WaitForFreeSlotOnHubTimeout), ToolConfigReader.GetToolConfig().Browser.ToString());
        }

        protected override IWebDriver WebDriverSetupSetps()
        {
            Logger.LogExecute($"ATTEMPT TO CREATE REMOTE {ToolConfigReader.GetToolConfig().Browser.ToString().ToUpper()} DRIVER");
            var remoteWebDriver = new RemoteWebDriver(new Uri(ToolConfigReader.GetToolConfig().GridUrl), Capabilities, TimeSpan.FromMilliseconds(ToolConfigReader.GetToolConfig().CommandTimeout));
            Logger.LogExecute($"CREATED REMOTE {ToolConfigReader.GetToolConfig().Browser.ToString().ToUpper()} DRIVER ON HOST {GridConfigHelper.GetRemoteDriverHostName(remoteWebDriver, ToolConfigReader.GetToolConfig().GridHost)}");
            this.webDriver = remoteWebDriver;
            return remoteWebDriver;
        }

        protected override void AfterWebDriverSetupSetps()
        {
            SetTimeOut(this.webDriver);
            MaximizeBrowser(this.webDriver);
        }
    }
}
