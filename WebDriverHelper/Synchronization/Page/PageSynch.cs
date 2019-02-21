using CommonHelper.Helper.Config;
using CommonHelper.Helper.Log;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using WebDriverHelper.JScript;

namespace WebDriverHelper.Synchronization.Page
{
    public class PageSynch : JavaScript
    {
        public readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        private IWebDriver webDriver;

        public PageSynch()
        {
        }

        public PageSynch(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        #region Wait Methods Wait, WaitUntilAjaxLoad and WaitUntilPageLoad
        public WebDriverWait Wait()
        {
            return new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(toolConfigMember.ObjectWait));
        }

        public WebDriverWait Wait(int numberOfSeconds)
        {
            return new WebDriverWait(webDriver, TimeSpan.FromSeconds(numberOfSeconds));
        }

        public bool WaitUntilAjaxLoad()
        {
            return WaitUntilAjaxLoad(toolConfigMember.ObjectWait / 1000);
        }

        public bool WaitUntilAjaxLoad(int numberOfSeconds = -1)
        {
            bool isAjaxLoad = false;
            try
            {
                Wait(numberOfSeconds == -1 ? toolConfigMember.ObjectWait / 1000 : numberOfSeconds).Until((webDriver) =>
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
                Logger.Log("WaitTillAjaxLoad threw WebDriverTimeoutException with message '{0}'", e.Message);
                return isAjaxLoad;
            }
            return isAjaxLoad;
        }

        public bool WaitUntilPageLoad()
        {
            return WaitUntilPageLoad(toolConfigMember.ObjectWait / 1000);
        }

        public bool WaitUntilPageLoad(int numberOfSeconds)
        {
            bool isPageLoadCompletely = false;
            try
            {
                Wait(numberOfSeconds).Until((webDriver) =>
                {
                    try
                    {
                        isPageLoadCompletely = ExecuteScript(JScriptType.PageLoad, webDriver).ToString().Contains("complete");
                        return isPageLoadCompletely;
                    }
                    catch (Exception exception)
                    {
                        Logger.Log("WaitTillPageLoad threw Exception with message '{0}'", exception.Message);
                        return isPageLoadCompletely;
                    }
                });
            }
            catch (WebDriverTimeoutException exception)
            {
                Logger.Log("WaitTillPageLoad threw WebDriverTimeoutException with message '{0}'", exception.Message);
                return isPageLoadCompletely;
            }
            return isPageLoadCompletely;
        }
        #endregion
    }
}
