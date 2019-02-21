using System;
using System.Globalization;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace WebDriverHelper.WebElement
{
    public static class ByExtension
    {

        public static By FindElementBy(How how, string locator, Type customBy)
        {
            switch (how)
            {
                case How.Id:
                    return By.Id(locator);
                case How.Name:
                    return By.Name(locator);
                case How.TagName:
                    return By.TagName(locator);
                case How.ClassName:
                    return By.ClassName(locator);
                case How.CssSelector:
                    return By.CssSelector(locator);
                case How.LinkText:
                    return By.LinkText(locator);
                case How.PartialLinkText:
                    return By.PartialLinkText(locator);
                case How.XPath:
                    return By.XPath(locator);
                case How.Custom:
                    ConstructorInfo constructorInfo = customBy.GetConstructor(new Type[] { typeof(string) });
                    By finder = constructorInfo.Invoke(new object[] { locator }) as By;
                    return finder;
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                        "Did not know how to construct How from how {0}, using {1}", how, locator));
            }
        }
    }
}
