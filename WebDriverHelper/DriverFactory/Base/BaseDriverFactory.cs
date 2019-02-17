using CommonHelper.Helper.Config;
using CommonHelper.Setup.Upload;
using OpenQA.Selenium;
using System;
using WebDriverHelper.Interfaces.DriverFactory;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory.Base
{
    public abstract class BaseDriverFactory 
    {
        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();


        public abstract IWebDriver InitializeWebDriver();

        public IWebDriver WebDriverSetupSetps()
        {
            return InitializeWebDriver();
        }
    }
}
