using OpenQA.Selenium;
using System;
using System.Drawing;

namespace WebDriverHelper.Interfaces.HtmlElements
{
    public interface IHtmlElement
    {
        Boolean IsDisplayed { get; }

        Boolean IsEnabled { get; }

        Boolean IsEnabledByClass { get; }

        Boolean IsSelected { get; }

        String Value { get; }

        Boolean IsActiveByClass { get; }

        String TagName { get; }

        String Text { get; }

        Point Location { get; }

        Size Size { get; }

        IWebElement HtmlWebElement { get; }

        Guid GuId { get; }

        String GetAttribute(String attributeName);

        String GetCssValue(String cssName);

        String GetProperty(string propertyName);

        void Click();
        void Click(IWebElement htmlElement);

        void ClickByJavaScript();

        void Clear();
        void Clear(IWebElement htmlElement);

        void Focus(IWebElement htmlElement);

        void WaitForDisappearence();
        void WaitForDisappearence(TimeSpan timeSpan);

        void WaitForExistance();
        void WaitForExistance(TimeSpan timeSpan);

        void WaitForEnablling();
        void WaitForEnablling(TimeSpan timeSpan);

        void WaitForDisplayed();
        void WaitForDisplayed(TimeSpan timeSpan);

        void WaitForAttributeContainsValue(string attribute, string value);
        void WaitForAttributeContainsValue(string attribute, string value, TimeSpan timeSpan);
    }
}
