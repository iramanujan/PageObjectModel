using System;
using System.Globalization;
using System.Reflection;
using OpenQA.Selenium;

namespace WebDriverHelper.WebElementFactory.CustomHtmlElements
{
    public enum How
    {
        Id,
        Name,
        TagName,
        ClassName,
        CssSelector,
        LinkText,
        PartialLinkText,
        XPath,
        Custom
    }

    public static class ByExtension
    {
        public static By GetBy(How how, string searchContext)
        {
            switch (how)
            {
                case How.Id:
                    return By.Id(searchContext);
                case How.Name:
                    return By.Name(searchContext);
                case How.TagName:
                    return By.TagName(searchContext);
                case How.ClassName:
                    return By.ClassName(searchContext);
                case How.CssSelector:
                    return By.CssSelector(searchContext);
                case How.LinkText:
                    return By.LinkText(searchContext);
                case How.PartialLinkText:
                    return By.PartialLinkText(searchContext);
                case How.XPath:
                    return By.XPath(searchContext);
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Did not know how to construct How from how {0}, using {1}", how, searchContext));
            }
        }
    }
}
