using CommonHelper.Helper.Log;
using CommonHelper.Setup.Download;
using CommonHelper.Setup.Upload;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using WebDriverHelper.WebDriverFactory.Base;
using WebDriverHelper.WebDriverFactory.InternetExplorer.Options;
using WebDriverHelper.Grid;
using WebDriverHelper.Interfaces.DriverFactory;

namespace WebDriverHelper.WebDriverFactory.InternetExplorer.Remote
{
    public class RemoteInternetExplorerDriver : BaseRemoteDriverFactory, IWebDriverFactory
    {
        private IWebDriver webDriver = null;

        protected override ICapabilities Capabilities => InternetExplorerDriverOptions.GetInternetExplorerOptions().ToCapabilities();

        private void BeforeWebDriverSetupSetps()
        {
            downloadLocation = new Lazy<string>(() => DownloadLocation.CreateWebDriverDirectory(toolConfigMember.Browser.ToString() + toolConfigMember.ExecutionType.ToString(), toolConfigMember.RootDownloadLocation));
            uploadLocation = new Lazy<UploadLocation>(() => UploadLocation.Create(toolConfigMember.Browser.ToString() + toolConfigMember.ExecutionType.ToString(), true, toolConfigMember.RootUploadLocation));
        }

        public IWebDriver InitializeWebDriver()
        {
            BeforeWebDriverSetupSetps();
            Logger.LogExecute($"ATTEMPT TO CREATE REMOTE {browserName.ToUpper()} DRIVER");
            var remoteWebDriver = new RemoteWebDriver(new Uri(gridUrl), Capabilities, TimeSpan.FromMilliseconds(commandTimeout));
            Logger.LogExecute($"CREATED REMOTE {browserName.ToUpper()} DRIVER ON HOST {GridConfigHelper.GetRemoteDriverHostName(remoteWebDriver, gridHost)}");
            webDriver = remoteWebDriver;
            return webDriver;
        }
    }
}
