using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace WebDriverHelper.Interfaces.HtmlElements
{
    public interface IHtmlElementLocator
    {
        IWebElement FindElement();
        ReadOnlyCollection<IWebElement> FindElements();
    }
}
