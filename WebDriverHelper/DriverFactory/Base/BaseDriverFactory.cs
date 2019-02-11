using CommonHelper.Helper.Config;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverHelper.Interfaces.DriverFactory;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory.Base
{
    public abstract class BaseDriverFactory : IWebDriverFactory
    {
        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.ToolConfigMembers;
        protected readonly TimeSpan commandTimeout = TimeSpan.FromMilliseconds(toolConfigMember.CommandTimeout);

        protected BaseDriverFactory(LocalizationType localizationType)
        {
            Localization = localizationType;
        }



        public abstract string DownloadLocation { get; }
        //public abstract WebDriverUploadDirectory UploadLocation { get; }


        public IWebDriver Create()
        {
            //BeforeWebDriverCreationAction()
            //WebDriverCreationAction()
            //AfterWebDriverCreationAction()
            PreCreateActions();
            var driver = CreateDriverAction();
            PostCreateActions(driver);
            return driver;
        }

        protected abstract IWebDriver CreateDriverAction();


        protected virtual void PreCreateActions()
        {

        }


        protected virtual void PostCreateActions(IWebDriver driver)
        {
            SetTimeOut(driver);
            MaximizeBrowser(driver);
        }

        private void SetTimeOut(IWebDriver driver)
        {
            var timeouts = driver.Manage().Timeouts();
            timeouts.AsynchronousJavaScript = TimeSpan.FromSeconds(120);
            timeouts.ImplicitWait = TimeSpan.FromSeconds(1);
            timeouts.PageLoad = TimeSpan.FromSeconds(toolConfigMember.PageLoadWait);
        }

        private void MaximizeBrowser(IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
        }

        public LocalizationType Localization { get; }
    }
}
