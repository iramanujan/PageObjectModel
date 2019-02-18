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
    public abstract class BaseRemoteDriverFactory
    {
        protected abstract ICapabilities Capabilities { get; }

        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        protected Lazy<string> downloadLocation;
        protected Lazy<UploadLocation> uploadLocation;

        protected string browserName = toolConfigMember.Browser.ToString();
        protected string gridUrl = toolConfigMember.GridUrl.ToString();
        protected int commandTimeout = toolConfigMember.CommandTimeout;
        protected string gridHost = toolConfigMember.GridHost.ToString();

    }
}
