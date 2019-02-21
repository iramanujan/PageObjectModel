using CommonHelper.Helper.Config;
using CommonHelper.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WebDriverHelper.Interfaces.DriverFactory;
using WebDriverHelper.JScript;
using CommonHelper.Helper.Log;
using static CommonHelper.Helper.Config.ToolConfigMember;
using WebDriverHelper.Synchronization.Page;

namespace WebDriverHelper.BrowserFactory
{
    public class BrowserFactory : PageSynch
    {
        private readonly IWebDriverFactory webDriverFactory;
        private IWebDriver objWebDriver;
        private IWebDriver webDriver => objWebDriver ?? (objWebDriver = webDriverFactory.InitializeWebDriver());

        #region Constructor`
        public BrowserFactory(IWebDriverFactory webDriverFactory)
        {
            this.webDriverFactory = webDriverFactory;
        }

        public BrowserFactory(IWebDriver webDriver):base(webDriver)
        {
            this.objWebDriver = webDriver;
        }
        #endregion

        #region Public Methods
        public IOptions Manage()
        {
            return webDriver.Manage();
        }

        public INavigation Navigate()
        {
            return webDriver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return webDriver.SwitchTo();
        }

        public BrowserFactory Back()
        {
            webDriver.Navigate().Back();
            return this;
        }

        public BrowserFactory Forward()
        {
            webDriver.Navigate().Forward();
            return this;
        }

        public BrowserFactory Refresh()
        {
            webDriver.Navigate().Refresh();
            ExecuteScript(JScriptType.PageLoad, this.webDriver);
            return this;
        }

        public BrowserFactory ClearCookies()
        {
            webDriver.Manage().Cookies.DeleteAllCookies();
            return this;
        }

        public BrowserFactory Open()
        {
            webDriver.Navigate().GoToUrl(toolConfigMember.PageUrls);
            WaitUntilPageLoad();
            webDriver.SwitchTo().DefaultContent();
            return this;
        }

        public void Close()
        {
            webDriver.Close();
        }

        public void Quit()
        {
            if (this.webDriver != null)
            {
                try
                {
                    foreach (var window in this.webDriver.WindowHandles)
                    {
                        SwitchToWindowHandle(window);
                        Close();
                    }
                    this.webDriver.Quit();
                }

                catch (Exception ex)
                {
                    Logger.Error($"Unable to Quit the browser. Reason: {ex.Message}");
                    switch (ToolConfigReader.GetToolConfig().Browser)
                    {
                        case BrowserType.IE:
                            ProcessUtils.KillProcesses("iexplore");
                            ProcessUtils.KillProcesses("IEDriverServer");
                            break;
                        case BrowserType.Chrome:
                            ProcessUtils.KillProcesses("chrome.exe");
                            ProcessUtils.KillProcesses("chromedriver.exe");
                            break;
                        case BrowserType.Firefox:
                            ProcessUtils.KillProcesses("firefox.exe");
                            ProcessUtils.KillProcesses("geckodriver.exe");
                            break;
                    }
                }

                finally
                {
                    this.objWebDriver = null;
                }
            }
        }

        public void CloseCurrentTab()
        {
            webDriver.Close();
        }

        public void SwitchToWindowHandle(string windowHandle)
        {
            webDriver.SwitchTo().Window(windowHandle);
        }

        public void SwitchHandleToNewWindowByPartialUrl(string urlPart)
        {

            if (GetDecodedUrl().Contains(urlPart)) { return; }
            ReadOnlyCollection<string> handles = WindowHandles;
            foreach (string handle in handles.Reverse())
            {
                SwitchTo().Window(handle);
                if (GetDecodedUrl().Contains(urlPart))
                {
                    WaitUntilPageLoad();
                    WaitUntilAjaxLoad();
                    return;
                }
            }

        }

        public string GetDecodedUrl()
        {
            var url = webDriver.Url;
            return HttpUtility.UrlDecode(url);
        }

        public BrowserFactory Maximize()
        {
            Manage().Window.Maximize();
            return this;
        }

        public Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot)webDriver).GetScreenshot();
        }

        #endregion

        #region Page Scroll Methods
        public void ScrollTop()
        {
            Logger.LogExecute("Scroll To Page Top");
            ExecuteScript(JScriptType.ScrollTop, webDriver);
            WaitUntilAjaxLoad();
        }

        public void ScrollBottom()
        {
            Logger.LogExecute("Scroll To Page Bottom");
            ExecuteScript(JScriptType.ScrollBottom, webDriver);
            WaitUntilAjaxLoad();
        }
        #endregion

        #region Public Property
        public string Url
        {
            get { return this.webDriver.Url; }
            set { webDriver.Url = value; }
        }

        public string Title
        {
            get { return webDriver.Title; }
        }

        public string PageSource
        {
            get { return webDriver.PageSource; }
        }

        public string CurrentWindowHandle
        {
            get { return webDriver.CurrentWindowHandle; }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get { return webDriver.WindowHandles; }
        }

        public string DownloadLocation
        {
            get { return toolConfigMember.RootDownloadLocation.ToString(); }
        }

        public string UploadLocation
        {
            get { return toolConfigMember.RootUploadLocation.ToString(); }
        }
        #endregion

    }
}
