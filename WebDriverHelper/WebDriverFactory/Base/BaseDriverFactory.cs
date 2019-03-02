﻿using CommonHelper.Helper.Config;
using OpenQA.Selenium;

namespace WebDriverHelper.WebDriverFactory.Base
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