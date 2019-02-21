using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverHelper.Helper.HtmlElements.HtmlElement
{
    public class HtmlCheckBox : BaseHtmlElement
    {
        public HtmlCheckBox(IWebElement htmlElement) : base(htmlElement)
        {

        }

        public IWebElement Label
        {
            get
            {
                try
                {
                    return this.HtmlWebElement.FindElement(By.XPath("following-sibling::label|following-sibling::span[@class='checkbox-label']"));
                }
                catch
                {
                    return null;
                }
            }
        }

        public string LabelText
        {
            get
            {
                IWebElement label = this.Label;
                return label == null ? null : label.Text;
            }
        }

        public virtual void Select()
        {
            if (!this.IsSelected)
            {
                Click();
            }
        }

        public virtual void Deselect()
        {
            if (this.IsSelected)
            {
                Click();
            }
        }

        public void Set(bool value)
        {
            if (value)
            {
                this.Select();
            }
            else
            {
                this.Deselect();
            }
        }
    }
}

