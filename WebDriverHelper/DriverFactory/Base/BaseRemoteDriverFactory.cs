using CommonHelper.Helper.Config;
using CommonHelper.Helper.Log;
using CommonHelper.Setup.Download;
using CommonHelper.Setup.Upload;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected readonly Lazy<string> downloadLocation = new Lazy<string>(() => DownloadLocation.CreateWebDriverDirectory(ToolConfigReader.ToolConfigMembers.Browser.ToString() + ToolConfigReader.ToolConfigMembers.ExecutionType.ToString(), ToolConfigReader.ToolConfigMembers.RootDownloadLocation));
        protected readonly Lazy<UploadLocation> uploadLocation = new Lazy<UploadLocation>(() => UploadLocation.Create(ToolConfigReader.ToolConfigMembers.Browser.ToString() + ToolConfigReader.ToolConfigMembers.ExecutionType.ToString(), true, ToolConfigReader.ToolConfigMembers.RootUploadLocation));


        protected override void BeforeWebDriverSetupSetps()
        {
            GridConfigHelper.WaitForFreeSlotOnHubForBrowser(ToolConfigReader.ToolConfigMembers.GridHost, TimeSpan.FromMilliseconds(ToolConfigReader.ToolConfigMembers.WaitForFreeSlotOnHubTimeout), ToolConfigReader.ToolConfigMembers.Browser.ToString());
        }

        protected override IWebDriver WebDriverSetupSetps()
        {
            Logger.LogExecute($"ATTEMPT TO CREATE REMOTE {ToolConfigReader.ToolConfigMembers.Browser.ToString().ToUpper()} DRIVER");
            var remoteWebDriver = new RemoteWebDriver(new Uri(ToolConfigReader.ToolConfigMembers.GridUrl), Capabilities, TimeSpan.FromMilliseconds(ToolConfigReader.ToolConfigMembers.CommandTimeout));
            Logger.LogExecute($"CREATED REMOTE {ToolConfigReader.ToolConfigMembers.Browser.ToString().ToUpper()} DRIVER ON HOST {GridConfigHelper.GetRemoteDriverHostName(remoteWebDriver, ToolConfigReader.ToolConfigMembers.GridHost)}");
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
