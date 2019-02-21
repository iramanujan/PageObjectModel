using OpenQA.Selenium;
using System.Collections.ObjectModel;
using WebDriverHelper.Interfaces.HtmlElements;
using WebDriverHelper.WebElement.HtmlElements.HtmlAttributes;

namespace WebDriverHelper.WebElement
{
    public class DefaultElementLocator : IHtmlElementLocator
    {
        private readonly ISearchContext searchContext;
        private readonly bool shouldCache;
        public By ByLocator { get; set; }
        private IWebElement cachedElement;
        private ReadOnlyCollection<IWebElement> cachedElementList;


        public DefaultElementLocator(ISearchContext searchContext, HtmlAttributesHandler htmlAttributesHandler)
        {
            this.searchContext = searchContext;
            this.shouldCache = htmlAttributesHandler.ShouldCache();
            this.ByLocator = htmlAttributesHandler.BuildBy();
        }

        public DefaultElementLocator(ISearchContext searchContext, By byLocator)
        {
            this.searchContext = searchContext;
            this.shouldCache = false;
            this.ByLocator = byLocator;
        }

        public virtual IWebElement FindElement()
        {
            if (cachedElement != null && shouldCache)
            {
                return cachedElement;
            }

            IWebElement element = searchContext.FindElement(ByLocator);
            if (shouldCache)
            {
                cachedElement = element;
            }

            return element;
        }

        public virtual ReadOnlyCollection<IWebElement> FindElements()
        {
            if (cachedElementList != null && shouldCache)
            {
                return cachedElementList;
            }

            ReadOnlyCollection<IWebElement> elements = searchContext.FindElements(ByLocator);
            if (shouldCache)
            {
                cachedElementList = elements;
            }

            return elements;
        }
    }
}
