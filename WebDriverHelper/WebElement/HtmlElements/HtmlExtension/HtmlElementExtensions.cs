using CommonHelper.Extensions;
using CommonHelper.Helper.Log;
using CommonHelper.Helper.Wait;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using WebDriverHelper.Helper.HtmlElements.HtmlElement;
using WebDriverHelper.Interfaces.HtmlElements;
using ImpromptuInterface;
using FluentAssertions;
using CommonHelper.Utils;
using System.Threading;
using WebDriverHelper.Synchronization.Ajax;
using CommonHelper.Helper.Config;
using WebDriverHelper.JScript;

namespace WebDriverHelper.HtmlElements.HtmlExtension
{
    public static class HtmlElementExtensions
    {
        private static JavaScript ObjJavaScript;
        public readonly static ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();

        public static void SelectByTextUsingEnumStringValueAttribute(this HtmlSelect select, Enum enumvalue)
        {
            string value = enumvalue.ToStringValue();
            select.SelectByText(value);
        }

        public static void SelectByTextUsingEnumToString(this HtmlSelect select, Enum enumvalue)
        {
            string value = enumvalue.ToString();
            select.SelectByText(value);
        }

        public static IList<T> GetOptionsUsingEnumStringValue<T>(this HtmlSelect select) where T : struct, IComparable, IFormattable, IConvertible
        {
            return select.Options.Select(option => option.Text.ToEnum<T>()).ToList();
        }

        public static IList<T> GetOptionsUsingEnumToString<T>(this HtmlSelect select) where T : struct, IComparable, IFormattable, IConvertible
        {
            IList<T> enumOptions = new List<T>();
            var options = select.Options;
            foreach (var option in options)
            {
                T enumValue;
                if (Enum.TryParse(option.Text.Trim(), true, out enumValue))
                {
                    enumOptions.Add(enumValue);
                }
            }
            return enumOptions;
        }

        public static T WaitForAppearence<T>(this T element) where T : IWrapsElement, INamed
        {
            Logger.LogExecute($"Wait for {element.Name} element is displayed");
            element.WithTemporaryTimeout(TimeSpan.FromSeconds(3),
                _ => Waiter.SpinWaitEnsureSatisfied(() => _.WrappedElement.Displayed, TimeSpan.FromMilliseconds(toolConfigMember.ObjectWait),
                    TimeSpan.FromSeconds(1), $"Element: '{element.Name}' still not visible, but should be"));
            return element;
        }

        public static T Hover<T>(this T element) where T : IWrapsElement
        {
            IWebElement webElement = (element.WrappedElement.UndoActLike() as INamedHtmlElementLocatorHandler)?.HtmlElementLocator.FindElement() ?? element.WrappedElement;
            new Actions(((IWrapsDriver)webElement).WrappedDriver).MoveToElement(webElement).Build().Perform();
            return element;
        }

        public static void ValidateDisplayedWithText<T>(this IEnumerable<T> elements, IEnumerable<string> texts, bool expectedDisplayed) where T : IWrapsElement
        {
            foreach (string text in texts)
            {
                Logger.LogValidate($"Validate element with innerText equals '{text.Trim()}' {(expectedDisplayed ? "" : "not ")}displayed");
                var element = elements.SingleOrDefault(e => e.WrappedElement.Text.Equals(text.Trim()));

                if (expectedDisplayed)
                {
                    element.WrappedElement.Displayed.Should().BeTrue();
                }
                else
                {
                    element?.WrappedElement.Displayed.Should().BeFalse();
                }
            }
        }

        public static void WaitForAttributeNoChange(this IWebElement element, string attribute, string exceptionMessage = null, TimeSpan? timeout = null, int count = 10)
        {
            Waiter.SpinWaitEnsureSatisfied(() => CollectionUtils.Repeat(() =>
            {
                Waiter.Wait(timeout ?? TimeSpan.FromMilliseconds(100));
                return element.GetAttribute(attribute);
            }, count)
            .All(_ => _.Equals(element.GetAttribute(attribute))),
            exceptionMessage ?? $"{attribute} attribute is still changing.");
        }

        public static void RemoveFocus(this IWebElement webElement)
        {
            var webDriver = ((IWrapsDriver)webElement).WrappedDriver;
            ObjJavaScript = new JavaScript();
            ObjJavaScript.ExecuteScript(JScriptType.RemoveFocus, webDriver);
        }

        public static void SetFocus(this IWebElement webElement)
        {
            var webDriver = ((IWrapsDriver)webElement).WrappedDriver;
            ObjJavaScript = new JavaScript();
            ObjJavaScript.ExecuteScript(JScriptType.Focus, webDriver);
        }

    }
}
