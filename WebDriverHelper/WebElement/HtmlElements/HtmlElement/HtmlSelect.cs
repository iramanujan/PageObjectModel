using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebDriverHelper.Helper.HtmlElements.HtmlElement
{
    public class HtmlSelect : BaseHtmlElement
    {
        public HtmlSelect(IWebElement htmlElement) : base(htmlElement)
        {
        }

        public bool IsMultiple
        {
            get { return GetSelect().IsMultiple; }
        }

        public IList<IWebElement> Options
        {
            get { return GetSelect().Options; }
        }

        public IList<IWebElement> AllSelectedOptions
        {
            get { return GetSelect().AllSelectedOptions; }
        }

        public IWebElement SelectedOption
        {
            get { return GetSelect().SelectedOption; }
        }

        public bool HasSelectedOption()
        {
            foreach (IWebElement option in Options)
            {
                if (option.Selected)
                {
                    return true;
                }
            }
            return false;
        }

        public void SelectByText(String text)
        {
            GetSelect().SelectByText(text);
        }

        public void SelectByPartialText(String text)
        {
            string fullOptionText = GetSelect().Options.Select(opt => opt.Text).First(x => x.Contains(text));
            GetSelect().SelectByText(fullOptionText);
        }

        public void SelectByIndex(int index)
        {
            GetSelect().SelectByIndex(index);
        }

        public void SelectByValue(string value)
        {
            GetSelect().SelectByValue(value);
        }

        public void DeselectAll()
        {
            GetSelect().DeselectAll();
        }

        public void DeselectByText(String text)
        {
            GetSelect().DeselectByText(text);
        }

        public void DeselectByIndex(int index)
        {
            GetSelect().DeselectByIndex(index);
        }

        public void DeselectByValue(String value)
        {
            GetSelect().DeselectByValue(value);
        }

        protected SelectElement GetSelect()
        {
            return new SelectElement(htmlElement);
        }
    }
}