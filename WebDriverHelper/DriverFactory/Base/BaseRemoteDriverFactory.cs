using CommonHelper.Helper.Config;
using CommonHelper.Helper.Log;
using CommonHelper.Setup.Download;
using CommonHelper.Setup.Upload;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using WebDriverHelper.Grid;
using WebDriverHelper.Interfaces.DriverFactory;

namespace WebDriverHelper.DriverFactory.Base
{
    public abstract class BaseRemoteDriverFactory : IWebDriverFactory
    {
        private IWebDriver webDriver;

        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        protected Lazy<string> downloadLocation;
        protected Lazy<UploadLocation> uploadLocation;

        private string browserName = toolConfigMember.Browser.ToString();
        private string gridUrl = toolConfigMember.GridUrl.ToString();
        private int commandTimeout = toolConfigMember.CommandTimeout;
        private string gridHost = toolConfigMember.GridHost.ToString();

        protected abstract ICapabilities Capabilities { get; }

        protected void BeforeWebDriverSetupSetps()
        {
            downloadLocation = new Lazy<string>(() => DownloadLocation.CreateWebDriverDirectory(toolConfigMember.Browser.ToString() + toolConfigMember.ExecutionType.ToString(), toolConfigMember.RootDownloadLocation));
            uploadLocation = new Lazy<UploadLocation>(() => UploadLocation.Create(toolConfigMember.Browser.ToString() + toolConfigMember.ExecutionType.ToString(), true, toolConfigMember.RootUploadLocation));
            GridConfigHelper.WaitForFreeSlotOnHubForBrowser(toolConfigMember.GridHost, TimeSpan.FromMilliseconds(toolConfigMember.WaitForFreeSlotOnHubTimeout), toolConfigMember.Browser.ToString());
        }

        public IWebDriver InitializeWebDriver()
        {
            Logger.LogExecute($"ATTEMPT TO CREATE REMOTE {browserName.ToUpper()} DRIVER");
            var remoteWebDriver = new RemoteWebDriver(new Uri(gridUrl), Capabilities, TimeSpan.FromMilliseconds(commandTimeout));
            Logger.LogExecute($"CREATED REMOTE {browserName.ToUpper()} DRIVER ON HOST {GridConfigHelper.GetRemoteDriverHostName(remoteWebDriver, gridHost)}");
            webDriver = remoteWebDriver;
            return webDriver;
        }

    }
}
