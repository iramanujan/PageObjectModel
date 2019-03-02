using CommonHelper.Helper.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverHelper.WebElementFactory.CustomHtmlElements
{
    public static class CustomFindElement
    {
        private static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        public static IWebElement FindHtmlElement(ISearchContext context, How how, string searchContext)
        {
            var wait = new DefaultWait<ISearchContext>(context);
            wait.Timeout = TimeSpan.FromSeconds(toolConfigMember.ObjectWait / 1000);
            wait.PollingInterval = TimeSpan.FromSeconds(toolConfigMember.PollTime / 1000);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            By by = ByExtension.GetBy(how: how, searchContext: searchContext);

            IWebElement element = wait.Until<IWebElement>((webDriver) =>
            {
                return webDriver.FindElement(by);
            });

            return element;
        }

        public static ReadOnlyCollection<IWebElement> FindHtmlElements(ISearchContext context, How how, string searchContext)
        {
            var wait = new DefaultWait<ISearchContext>(context);
            wait.Timeout = TimeSpan.FromSeconds(toolConfigMember.ObjectWait / 1000);
            wait.PollingInterval = TimeSpan.FromSeconds(toolConfigMember.PollTime / 1000);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            By by = ByExtension.GetBy(how: how, searchContext: searchContext);

            ReadOnlyCollection<IWebElement> elements = wait.Until<ReadOnlyCollection<IWebElement>>((webDriver) =>
            {
                return webDriver.FindElements(by);
            });

            return elements;
        }
    }
}
