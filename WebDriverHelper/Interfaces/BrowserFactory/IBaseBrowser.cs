using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace WebDriverHelper.Interfaces.BrowserFactory
{
    public interface IBaseBrowser
    {

        void Close();
        void Quit();
        void Dispose();
        void CloseCurrentTab();

        IBaseBrowser Back();
        IBaseBrowser Forward();
        IBaseBrowser Refresh();
        IBaseBrowser OpenUrl(string url);
        IBaseBrowser ClearCookies();
        IBaseBrowser Maximize();

        void ScrollToTop();
        void ScrollToBottom();

        string GetUrl();
        string GetDecodedUrl();


        ReadOnlyCollection<string> GetWindowHandles();
        void SwitchToWindowHandle(string windowHandle);
        void SwitchHandleToNewWindowByPartialUrl(string urlPart);
        string CurrentWindowHandle { get; }
        ReadOnlyCollection<string> WindowHandles{ get; }

        string Url { get; set; }

        string Title { get; }

        string PageSource { get; }


        IOptions Manage();

        INavigation Navigate();

        ITargetLocator SwitchTo();

        bool WaitTillPageLoad();
        bool WaitTillPageLoad(int numberOfSeconds);
        WebDriverWait Wait(int numberOfSeconds);
        WebDriverWait Wait();
        bool WaitTillAjaxLoad();
        bool WaitTillAjaxLoad(int numberOfSeconds);
        bool WaitTillAjaxLoadIfExists(int numberOfSeconds = -1);

    }
}
