using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverHelper.WebElement
{
    public static class HtmlElementFactoryExtension
    {

        /// <summary>
        /// Creates the HTML element instance.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        public static HtmlElement CreateHtmlElementInstance(Type elementType)
        {
            if (typeof(HtmlElement).IsAssignableFrom(elementType))
            {
                return HtmlElementUtils.NewInstance<HtmlElement>(elementType);
            }
            throw new HtmlElementsException(string.Format(CultureInfo.InvariantCulture,
                "Type '{0}' is not a derivative type of 'HtmlElement'", elementType));
        }

        /// <summary>
        /// Creates the typified element instance.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        /// <param name="elementToWrap">The element to wrap.</param>
        public static TypifiedElement CreateTypifiedElementInstance(Type elementType, IWebElement elementToWrap)
        {
            if (typeof(TypifiedElement).IsAssignableFrom(elementType))
            {
                return HtmlElementUtils.NewInstance<TypifiedElement>(elementType, elementToWrap);
            }
            throw new HtmlElementsException(string.Format(CultureInfo.InvariantCulture,
                "Type '{0}' isn't a derivative type of 'TypifiedElement'", elementType));
        }


        public static object CreatePageObjectInstance(Type type, IWebDriver driver)
        {
            return HtmlElementUtils.NewInstance<object>(type, driver);
        }


        public static IWebElement CreateNamedProxyForWebElement(IElementLocator locator, string elementName)
        {
            return WebElementNamedProxyHandler.NewInstance(locator, elementName);
        }


        internal static object CreateNamedProxyForWebElementList(IElementLocator locator, string listName)
        {
            return WebElementListNamedProxyHandler.NewInstance(locator, listName);
        }

        internal static object CreateNamedProxyForTypifiedElementList(Type listType, Type elementType,
            IElementLocator locator, string listName)
        {
            return TypifiedElementListNamedProxyHandler.NewInstance(listType, elementType, locator, listName);
        }

        internal static object CreateNamedProxyForHtmlElementList(Type listType, Type elementType,
            IElementLocator locator, string listName)
        {
            return HtmlElementListNamedProxyHandler.NewInstance(listType, elementType, locator, listName);
        }
    }
}
