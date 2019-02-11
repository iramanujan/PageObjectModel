using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverHelper.DriverFactory.Base
{
    public abstract class BaseLocalDriverFactory
    {

        protected BaseLocalDriverFactory(BrowserLocalization localization) : base(localization) { }

        private readonly Lazy<WebDriverUploadDirectory> uploadLocation =
            new Lazy<WebDriverUploadDirectory>(
                () =>
                    WebDriverUploadDirectory.Create(ToolConfig.Browser + ToolConfig.ExecutionType.ToString(), true,
                        ToolConfig.RootUploadLocation));

        public override WebDriverUploadDirectory UploadLocation => uploadLocation.Value;
    }
}
