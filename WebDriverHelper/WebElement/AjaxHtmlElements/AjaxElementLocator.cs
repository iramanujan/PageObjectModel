using CommonHelper.Helper.Config;
using Microsoft.CSharp.RuntimeBinder;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using WebDriverHelper.Synchronization.Ajax;
using WebDriverHelper.WebElement;
using WebDriverHelper.WebElement.HtmlElements.HtmlAttributes;

namespace WebDriverHelper.AjaxHtmlElements
{

    public delegate IWebElement FindElement();
    public delegate ReadOnlyCollection<IWebElement> FindElements();
    public delegate bool IsElementUsable(IWebElement element);


    public class AjaxElementLocator : DefaultElementLocator
    {
        public readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();

        private IClock clock;

        private TimeSpan sleepInterval;

        public int TimeoutInSeconds { get; set; }

        public AjaxElementLocator(ISearchContext searchContext, By locator) : base(searchContext, locator)
        {
            RestoreDefaults();
        }

        public AjaxElementLocator(ISearchContext searchContext, HtmlAttributesHandler htmlAttributesHandler) : base(searchContext, htmlAttributesHandler)
        {
            RestoreDefaults();
        }

        public override IWebElement FindElement()
        {
            AjaxSync loadingElement = new AjaxSync(clock, TimeoutInSeconds, SleepInterval, this);
            try
            {
                return loadingElement.Load().HtmlElement;
            }
            catch (Exception exception)
            {
                if (exception is WebDriverException || exception is RuntimeBinderException)
                {
                    throw new NoSuchElementException(
                        string.Format("Element {0} timed out after {1} seconds. {2}", ByLocator, TimeoutInSeconds,
                            exception.Message),
                        exception.InnerException);
                }
                throw;
            }
        }

        public override ReadOnlyCollection<IWebElement> FindElements()
        {
            AjaxSync loadingElement = new AjaxSync(clock, TimeoutInSeconds, SleepInterval, this);
            try
            {
                return loadingElement.Load().HtmlElements;
            }
            catch (Exception exception)
            {
                if (exception is WebDriverException || exception is RuntimeBinderException)
                {
                    if (exception is WebDriverTimeoutException)
                    {
                        //If elements not appeared after time out then return empty collection
                        return DefaultFindElements();
                    }
                    throw new NoSuchElementException(
                        string.Format("Element list {0} timed out after {1} seconds. {2}", ByLocator, TimeoutInSeconds,
                            exception.Message),
                        exception.InnerException);
                }
                throw;
            }
        }

        public IWebElement DefaultFindelement()
        {
            return base.FindElement();
        }

        public ReadOnlyCollection<IWebElement> DefaultFindElements()
        {
            return base.FindElements();
        }

        public void RestoreDefaults()
        {
            this.clock = new SystemClock();
            this.TimeoutInSeconds = toolConfigMember.ObjectWait / 1000;
            this.sleepInterval = TimeSpan.FromMilliseconds(toolConfigMember.PollTime);
        }

        protected TimeSpan SleepInterval
        {
            get { return sleepInterval; }
        }

        public bool IsElementUsable(IWebElement htmlElement)
        {
            return true;
        }
    }
}
