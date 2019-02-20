using CommonHelper.Helper.Attributes;
using CommonHelper.Helper.Log;
using OpenQA.Selenium;
using System;
using System.ComponentModel;

namespace WebDriverHelper.JScript
{
    public enum JScriptType
    {
        [Description("var nodes = arguments[0].childNodes;" +
                     "var text = '';" +
                     "for (var i = 0; i < nodes.length; i++) {" +
                     "    if (nodes[i].nodeName === '#text') {" +
                     "        text += nodes[i].nodeValue; " +
                     "    }" +
                     "}" +
                     "return text;")]
        NodeTextWithoutChildren = 0,

        [Description("arguments[0].style.visibility='visible'; arguments[0].style.opacity = 1;")]
        ChooseFile = 1,

        [Description("arguments[0].scrollIntoView(false)")]
        ScrollToElementWithFalse = 2,

        [Description("return document.readyState")]
        PageLoad = 3,

        [Description("return (typeof jQuery != 'undefined') && (jQuery.active === 0)")]
        AjaxLoad = 4,

        [Description("$(window).scrollTop(0)")]
        ScrollTop = 5,

        [Description("$(window).scrollTop($(document).height())")]
        ScrollBottom = 6,

        [Description("arguments[0].scrollIntoView(true)")]
        ScrollToElementWithTrue = 7,
    }

    public class JavaScript
    {

        #region Execute Script
        public object ExecuteScript(JScriptType jScriptType, IWebDriver webDriver)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteScript(jScriptType.GetDescription());
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, jScriptType.GetDescription()));
            }
            return null;
        }

        public object ExecuteScript(JScriptType jScriptType, IWebDriver webDriver, IWebElement webElement)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteScript(jScriptType.GetDescription(), webElement);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, jScriptType.GetDescription()));
            }
            return null;
        }

        public object ExecuteScript(string javaScript, IWebDriver webDriver)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteScript(javaScript);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, javaScript));
            }
            return null;
        }

        public object ExecuteScript(string javaScript, IWebDriver webDriver, IWebElement webElement)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteScript(javaScript, webElement);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, javaScript));
            }
            return null;
        }
        #endregion

        #region Execute Async Script
        public object ExecuteAsyncScript(JScriptType jScriptType, IWebDriver webDriver)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteAsyncScript(jScriptType.GetDescription());
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, jScriptType.GetDescription()));
            }
            return null;
        }

        public object ExecuteAsyncScript(JScriptType jScriptType, IWebDriver webDriver, IWebElement webElement)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteAsyncScript(jScriptType.GetDescription(), webElement);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, jScriptType.GetDescription()));
            }
            return null;
        }

        public object ExecuteAsyncScript(string javaScript, IWebDriver webDriver)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteAsyncScript(javaScript);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, javaScript));
            }
            return null;
        }

        public object ExecuteAsyncScript(string javaScript, IWebDriver webDriver, IWebElement webElement)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteAsyncScript(javaScript, webElement);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, javaScript));
            }
            return null;
        }
        #endregion

        public string GetCookieNameByJavaScript(string cookieName, IWebDriver webDriver)
        {
            var script = $@"var n='{cookieName}'+'=';var cookies=decodeURIComponent(document.cookie).split(';');" +
                    @"for(var i=0;i<cookies.length;i++){var c=cookies[i];while (c.charAt(0)==' '){" +
                    @"c=c.substring(1);}if (c.indexOf(n)==0&&c.length!=n.length)" +
                    @"{return c.substring(n.length, c.length);}}return ''";
            return ExecuteScript(script, webDriver) as string;
        }

        public void ClearLocalStorageByJavaScript(string key, IWebDriver webDriver)
        {
            ExecuteScript($"delete localStorage['{key}']", webDriver);
        }

    }
}