using CommonHelper.Helper.Config;
using CommonHelper.Setup.Download;
using CommonHelper.Setup.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory.Base
{
    public abstract class BaseLocalDriverFactory : BaseDriverFactory
    {

        protected BaseLocalDriverFactory(LocalizationType localizationType) : base(localizationType)
        {
        }

        protected readonly Lazy<string> downloadLocation = new Lazy<string>(() => DownloadLocation.CreateWebDriverDirectory(ToolConfigReader.ToolConfigMembers.Browser.ToString() + ToolConfigReader.ToolConfigMembers.ExecutionType.ToString(), ToolConfigReader.ToolConfigMembers.RootDownloadLocation));
        protected readonly Lazy<UploadLocation> uploadLocation = new Lazy<UploadLocation>(() => UploadLocation.Create(ToolConfigReader.ToolConfigMembers.Browser.ToString() + ToolConfigReader.ToolConfigMembers.ExecutionType.ToString(), true,ToolConfigReader.ToolConfigMembers.RootUploadLocation));

    }
}
