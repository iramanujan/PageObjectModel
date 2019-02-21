using OpenQA.Selenium;

namespace WebDriverHelper.Helper.HtmlElements.HtmlElement
{
    public class HtmlButton : BaseHtmlElement
    {
        public HtmlButton(IWebElement htmlElement) : base(htmlElement)
        {
        }
        public void Submit()
        {
            htmlElement.Submit();
        }
    }
}
