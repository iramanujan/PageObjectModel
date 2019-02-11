using CommonHelper.Helper.Log;
using OpenQA.Selenium;
using System.Linq;

namespace WebDriverHelper.Helper.HtmlElements.HtmlElement
{
    public class HtmlTextBox : BaseHtmlElement
    {

        public HtmlTextBox(IWebElement htmlElement) : base(htmlElement)
        {

        }

        private void ClearUsingKeyAndSendKeys(string keys, string clearKey)
        {
            Enumerable.Range(0, this.Text.Length).ToList().ForEach(arg => htmlElement.SendKeys(clearKey));

            if (this.Text.Length > 0)
            {
                htmlElement.Clear();
            }
            htmlElement.SendKeys(keys);
        }

        private void ClearUsingKeyAndSendKeysWithSubmit(string keys, string clearKey)
        {
            Enumerable.Range(0, this.Text.Length).ToList().ForEach(arg => htmlElement.SendKeys(clearKey));

            if (this.Text.Length > 0)
            {
                htmlElement.Clear();
            }
            htmlElement.SendKeys(keys);
            htmlElement.SendKeys(Keys.Tab);
        }

        public void ClearUsingBackspace()
        {
            Enumerable.Range(0, Text.Length).ToList().ForEach(arg => htmlElement.SendKeys(Keys.Backspace));
        }

        public virtual void SendKeys(string keys)
        {
            htmlElement.SendKeys(keys);
        }

        public virtual void ClearAndSendKeys(string keys)
        {
            htmlElement.Clear();
            htmlElement.SendKeys(keys);
        }

        public virtual void ClearUsingBackspaceAndSendKeys(string keys)
        {
            this.ClearUsingKeyAndSendKeys(keys, Keys.Backspace);
        }

        public virtual void ClearUsingDeleteAndSendKeys(string keys)
        {
            this.ClearUsingKeyAndSendKeys(keys, Keys.Delete);
        }

        public void EnterAndSetText(string keys)
        {
            this.ClearUsingKeyAndSendKeys(keys, Keys.Enter);
        }

        public void EnterSetTextAndSubmit(string keys)
        {
            this.ClearUsingKeyAndSendKeysWithSubmit(keys, Keys.Enter);
        }

        public void SetTextAndClickEnter(string keys)
        {
            Logger.LogExecute($"Set text '{keys}' and click Enter to element");
            htmlElement.SendKeys(keys);
            htmlElement.SendKeys(Keys.Enter);
        }

        public new string Text
        {
            get
            {
                if ("textarea" == (htmlElement.TagName))
                {
                    return htmlElement.Text;
                }

                var enteredText = htmlElement.GetAttribute("value");
                if (enteredText == null)
                {
                    return string.Empty;
                }
                return enteredText;
            }
        }
    }
}
