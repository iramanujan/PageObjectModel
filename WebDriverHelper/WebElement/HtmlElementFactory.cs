using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WebDriverHelper.AjaxHtmlElements;

namespace WebDriverHelper.WebElement
{
    public static class HtmlElementFactory
    {

        public static IList<T> CreateParametrizedElementsList<T>(string elementName, ISearchContext searchContext,
            How how,
            string locator, params object[] locatorArgs) where T : class
        {
            var byLocator = BuildByParametrized(how, locator, locatorArgs);
            return CreateElementsList<T>(searchContext, byLocator, elementName);
        }

        public static T CreateParametrizedElement<T>(string elementName, ISearchContext searchContext, How how,
            string locator, params object[] locatorArgs) where T : class
        {
            var byLocator = BuildByParametrized(how, locator, locatorArgs);
            return CreateElement<T>(searchContext, byLocator, elementName);
        }

        public static T CreateElement<T>(ISearchContext searchContext, By byLocator, string elementName) where T : class
        {
            Type elementType = typeof(T);

            if (HtmlElementUtils.IsHtmlElement(elementType))
            {
                return CreateHtmlElement(elementType, searchContext, byLocator, elementName) as T;
            }

            if (HtmlElementUtils.IsTypifiedElement(elementType))
            {
                return CreateTypifiedElement(elementType, searchContext, byLocator, elementName) as T;
            }

            if (HtmlElementUtils.IsWebElement(elementType))
            {
                return CreateWebElement(searchContext, byLocator, elementName) as T;
            }
            throw new HtmlElementsException(
                string.Format(
                    "Incorrect {0} to create element is used. Valid types are {1}, derived types of {2}, derived types of {3}",
                    elementType, typeof(IWebElement), typeof(TypifiedElement), typeof(HtmlElement)));
        }

        public static IList<T> CreateElementsList<T>(ISearchContext searchContext, By byLocator, string elementName)
            where T : class
        {
            Type listType = typeof(IList<T>);
            Type elementType = typeof(T);

            if (HtmlElementUtils.IsHtmlElement(elementType))
            {
                return (IList<T>)CreateHtmlElementsList(listType, elementType, searchContext, byLocator, elementName);
            }

            if (HtmlElementUtils.IsTypifiedElement(elementType))
            {
                return
                    (IList<T>)CreateTypifiedElementsList(listType, elementType, searchContext, byLocator, elementName);
            }

            if (HtmlElementUtils.IsWebElement(elementType))
            {
                return (IList<T>)CreateWebElementsList(searchContext, byLocator, elementName);
            }
            throw new HtmlElementsException(
                string.Format(
                    "Incorrect element {0} to create list is used. Valid element types are {1}, derived types of {2}, derived types of {3}",
                    elementType, typeof(IWebElement), typeof(TypifiedElement), typeof(HtmlElement)));
        }


        private static HtmlElement CreateHtmlElement(Type elementType, ISearchContext searchContext, By byLocator,
            string elementName)
        {
            var ajaxElementLocator = new AjaxElementLocator(searchContext, byLocator);
            IWebElement elementToWrap = HtmlElementFactory.CreateNamedProxyForWebElement(ajaxElementLocator, elementName);
            HtmlElement htmlElementInstance = HtmlElementFactory.CreateHtmlElementInstance(elementType);
            htmlElementInstance.WrappedElement = elementToWrap;
            htmlElementInstance.Name = elementName;
            // Recursively initialize elements of the complex html element
            PageFactory.InitElements(new HtmlElementDecorator(elementToWrap), htmlElementInstance);
            return htmlElementInstance;
        }

        private static TypifiedElement CreateTypifiedElement(Type type, ISearchContext searchContext, By byLocator,
            string elementName)
        {
            var ajaxElementLocator = new AjaxElementLocator(searchContext, byLocator);
            IWebElement elementToWrap = HtmlElementFactory.CreateNamedProxyForWebElement(ajaxElementLocator, elementName);
            TypifiedElement typifiedElementInstance = HtmlElementFactory.CreateTypifiedElementInstance(type,
                elementToWrap);
            typifiedElementInstance.Name = elementName;
            return typifiedElementInstance;
        }

        private static IWebElement CreateWebElement(ISearchContext searchContext, By byLocator, string elementName)
        {
            var ajaxElementLocator = new AjaxElementLocator(searchContext, byLocator);
            return HtmlElementFactory.CreateNamedProxyForWebElement(ajaxElementLocator, elementName);
        }

        private static object CreateHtmlElementsList(Type listType, Type elementType, ISearchContext searchContext,
            By byLocator, string listName)
        {
            var ajaxElementLocator = new AjaxElementLocator(searchContext, byLocator);
            return HtmlElementFactory.CreateNamedProxyForHtmlElementList(listType, elementType, ajaxElementLocator,
                listName);
        }

        private static object CreateTypifiedElementsList(Type listType, Type elementType, ISearchContext searchContext,
            By byLocator, string listName)
        {
            var ajaxElementLocator = new AjaxElementLocator(searchContext, byLocator);
            return HtmlElementFactory.CreateNamedProxyForTypifiedElementList(listType, elementType, ajaxElementLocator,
                listName);
        }

        private static object CreateWebElementsList(ISearchContext searchContext, By byLocator, string listName)
        {
            var ajaxElementLocator = new AjaxElementLocator(searchContext, byLocator);
            return HtmlElementFactory.CreateNamedProxyForWebElementList(ajaxElementLocator, listName);
        }

        private static By BuildByParametrized(How how, string locator, params object[] locatorArgs)
        {
            if (locatorArgs == null || locatorArgs.Length == 0)
            {
                throw new ArgumentException("Please specify the locator arguments");
            }
            return ByExtension.FindElementBy(how, string.Format(locator, locatorArgs), null);
        }
    }
}
