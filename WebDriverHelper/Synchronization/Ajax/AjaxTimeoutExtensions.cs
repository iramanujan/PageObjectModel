using ImpromptuInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebDriverHelper.AjaxHtmlElements;
using WebDriverHelper.Interfaces.HtmlElements;

namespace WebDriverHelper.Synchronization.Ajax
{
    public static class AjaxTimeoutExtensions
    {
        public static void WithTemporaryTimeout<T>(this T element, TimeSpan timeSpan, Action<T> elementAction) where T : IWrapsElement
        {
            element.ChangeTimeout(timeSpan);
            try
            {
                elementAction(element);
            }
            catch
            {
                element.RestoreDefaultTimeout();
                throw;
            }
            finally
            {
                element.RestoreDefaultTimeout();
            }
        }

        public static TResult WithTemporaryTimeout<T, TResult>(this T element, TimeSpan timeSpan, Func<T, TResult> elementAction)
            where T : IWrapsElement
        {
            element.ChangeTimeout(timeSpan);
            try
            {
                return elementAction(element);
            }
            catch
            {
                element.RestoreDefaultTimeout();
                throw;
            }
            finally
            {
                element.RestoreDefaultTimeout();
            }
        }

        private static IWebElement ChangeTimeout(this IWebElement element, TimeSpan time)
        {
            ChangeTimeoutForProxy(element, time);
            return element;
        }

        private static IWebElement RestoreDefaultTimeout(this IWebElement element)
        {
            RestoreDefaultTimeoutForProxy(element);
            return element;
        }

        public static IList<IWebElement> ChangeTimeout(this ReadOnlyCollection<IWebElement> elements, TimeSpan time)
        {
            ChangeTimeoutForProxy(elements, time);
            return elements;
        }

        public static IList<IWebElement> RestoreDefaultTimeout(this ReadOnlyCollection<IWebElement> elements)
        {
            RestoreDefaultTimeoutForProxy(elements);
            return elements;
        }

        private static T ChangeTimeout<T>(this T element, TimeSpan time) where T : IWrapsElement
        {
            ChangeTimeoutForProxy(element.WrappedElement, time);
            return element;
        }

        private static T RestoreDefaultTimeout<T>(this T element) where T : IWrapsElement
        {
            RestoreDefaultTimeoutForProxy(element.WrappedElement);
            return element;
        }

        public static IList<T> ChangeTimeout<T>(this IList<T> elements, TimeSpan time) where T : IWrapsElement
        {
            ChangeTimeoutForProxy(elements, time);
            return elements;
        }

        public static IList<T> RestoreDefaultTimeout<T>(this IList<T> elements) where T : IWrapsElement
        {
            RestoreDefaultTimeoutForProxy(elements);
            return elements;
        }

        private static void ChangeTimeoutForProxy(object proxy, TimeSpan time)
        {
            var proxyHandler = proxy.UndoActLike() as INamedHtmlElementLocatorHandler;
            Assert.IsNotNull(proxyHandler, "Your element was created without proxy, method applied only for proxied elements");
            var ajaxElementLocator = (AjaxElementLocator)proxyHandler.HtmlElementLocator;
            ajaxElementLocator.TimeoutInSeconds = (int)time.TotalSeconds;
        }

        private static void RestoreDefaultTimeoutForProxy(object proxy)
        {
            var proxyHandler = proxy.UndoActLike() as INamedHtmlElementLocatorHandler;
            Assert.IsNotNull(proxyHandler, "Your element was created without proxy, method applied only for proxied elements");
            var ajaxElementLocator = (AjaxElementLocator)proxyHandler.HtmlElementLocator;
            ajaxElementLocator.RestoreDefaults();
        }
    }
}