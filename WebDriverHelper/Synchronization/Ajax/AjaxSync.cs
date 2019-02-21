using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebDriverHelper.AjaxHtmlElements;

namespace WebDriverHelper.Synchronization.Ajax
{
    public class AjaxSync : SlowLoadableComponent<AjaxSync>
    {
        private readonly AjaxElementLocator ajaxElementLocator;
        private readonly string locatorName;

        public IWebElement HtmlElement { get; private set; }

        public ReadOnlyCollection<IWebElement> HtmlElements
        {
            get { return HtmlElements; }
        }

        public AjaxSync(IClock clock, int timeOutInSeconds, TimeSpan sleepInterval,AjaxElementLocator ajaxElementLocator): base(TimeSpan.FromSeconds(timeOutInSeconds), clock)
        {
            this.ajaxElementLocator = ajaxElementLocator;
            this.SleepInterval = sleepInterval;
            locatorName = string.IsNullOrWhiteSpace(ajaxElementLocator.ByLocator.ToString())
                ? string.Empty
                : ajaxElementLocator.ByLocator.ToString();
        }

        protected override bool EvaluateLoadedStatus()
        {

            try
            {
                HtmlElement = ajaxElementLocator.DefaultFindelement();
                if (!ajaxElementLocator.IsElementUsable(HtmlElement))
                {
                    throw new NoSuchElementException(string.Format("Element {0} is not usable", locatorName));
                }
                return true;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            catch (NoSuchElementException e)
            {
                throw new NoSuchElementException(string.Format("Unable to locate the element: {0}", locatorName), e);
            }
        }

        protected override void ExecuteLoad()
        {
            throw new NotImplementedException();
        }

    }
}