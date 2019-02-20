using CommonHelper.Helper.Config;
using CommonHelper.Helper.Log;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using WebDriverHelper.JScript;

namespace WebDriverHelper.Synchronization.BrowserFactory
{
    public class PageSynchronization : JavaScript
    {
        public readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        private IWebDriver webDriver;


        public PageSynchronization()
        {
        }

        public PageSynchronization(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        #region Wait Methods Wait, WaitTillAjaxLoad and WaitTillPageLoad
        public WebDriverWait Wait()
        {
            return new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(toolConfigMember.ObjectWait));
        }

        public WebDriverWait Wait(int numberOfSeconds)
        {
            return new WebDriverWait(webDriver, TimeSpan.FromSeconds(numberOfSeconds));
        }

        public bool WaitTillAjaxLoad()
        {
            return WaitTillAjaxLoad(toolConfigMember.ObjectWait / 1000);
        }

        public bool WaitTillAjaxLoad(int numberOfSeconds = -1)
        {
            bool isAjaxLoad = false;
            try
            {
                Wait(numberOfSeconds == -1 ? toolConfigMember.ObjectWait / 1000 : numberOfSeconds).Until((driver) =>
                {
                    try
                    {
                        isAjaxLoad = (bool)ExecuteScript(JScriptType.AjaxLoad, webDriver);
                        return isAjaxLoad;
                    }
                    catch (Exception e)
                    {
                        if (e is InvalidOperationException)
                        {
                            Logger.Log("WaitTillAjaxLoad threw InvalidOperationException with message '{0}'", e.Message);
                        }
                        else
                        {
                            Logger.Log(e.Message);
                        }
                        return isAjaxLoad;
                    }
                });
            }
            catch (WebDriverTimeoutException e)
            {
                Logger.Log(e.Message);
            }
            return isAjaxLoad;
        }

        public bool WaitTillPageLoad()
        {
            return WaitTillPageLoad(toolConfigMember.ObjectWait / 1000);
        }

        public bool WaitTillPageLoad(int numberOfSeconds)
        {
            bool isPageLoadCompletely = false;
            try
            {
                Wait(numberOfSeconds).Until((driver) =>
                {
                    try
                    {
                        isPageLoadCompletely = ExecuteScript(JScriptType.PageLoad, webDriver).ToString().Contains("complete");
                        return isPageLoadCompletely;
                    }
                    catch (Exception)
                    {
                        return isPageLoadCompletely;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                return isPageLoadCompletely;
            }
            return isPageLoadCompletely;
        }
        #endregion
    }
}
