using CommonHelper.Helper.Config;
using CommonHelper.Setup.Download;
using CommonHelper.Setup.Upload;
using System;

namespace WebDriverHelper.WebDriverFactory.Base
{
    public class BaseLocalDriverFactory
    {
        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        protected Lazy<string> downloadLocation = new Lazy<string>(() => DownloadLocation.CreateWebDriverDirectory(toolConfigMember.Browser.ToString() + toolConfigMember.ExecutionType.ToString(), toolConfigMember.RootDownloadLocation));
        protected Lazy<UploadLocation> uploadLocation = new Lazy<UploadLocation>(() => UploadLocation.Create(toolConfigMember.Browser.ToString() + toolConfigMember.ExecutionType.ToString(), true, toolConfigMember.RootUploadLocation));
    }
}
