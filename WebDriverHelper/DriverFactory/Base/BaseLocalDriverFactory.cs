using CommonHelper.Helper.Config;
using CommonHelper.Setup.Download;
using CommonHelper.Setup.Upload;
using System;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory.Base
{
    public abstract class BaseLocalDriverFactory : BaseDriverFactory
    {

        protected BaseLocalDriverFactory(LocalizationType localizationType) : base(localizationType)
        {
        }

        protected readonly Lazy<string> downloadLocation = new Lazy<string>(() => DownloadLocation.CreateWebDriverDirectory(ToolConfigReader.GetToolConfig().Browser.ToString() + ToolConfigReader.GetToolConfig().ExecutionType.ToString(), ToolConfigReader.GetToolConfig().RootDownloadLocation));
        protected readonly Lazy<UploadLocation> uploadLocation = new Lazy<UploadLocation>(() => UploadLocation.Create(ToolConfigReader.GetToolConfig().Browser.ToString() + ToolConfigReader.GetToolConfig().ExecutionType.ToString(), true,ToolConfigReader.GetToolConfig().RootUploadLocation));

    }
}
