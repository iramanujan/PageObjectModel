﻿using CommonHelper.Helper.Log;
using Microsoft.CSharp.RuntimeBinder;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using WebDriverHelper.Interfaces.HtmlElements;

namespace WebDriverHelper.Helper.HtmlElements
{
    public class BaseHtmlElement : IHtmlElement
    {
        public IWebElement htmlElement;
        private TimeSpan defaultWaitTimeout;
        private IWebDriver webDriver;

        public BaseHtmlElement()
        {

        }

        public BaseHtmlElement(IWebElement htmlElement)
        {
            this.htmlElement = htmlElement;
        }

        public BaseHtmlElement(IWebDriver webDriver, IWebElement htmlElement)
        {
            this.htmlElement = htmlElement;
            this.webDriver = webDriver;
        }

        public BaseHtmlElement(IWebElement htmlElement, TimeSpan defaultWaitTimeout)
        {
            this.htmlElement = htmlElement;
            this.defaultWaitTimeout = defaultWaitTimeout;
        }

        public IWebElement HtmlWebElement
        {
            get { return this.htmlElement; }
        }

        public bool IsDisplayed
        {
            get
            {
                try
                {
                    return this.htmlElement.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
                catch (WebDriverTimeoutException)
                {
                    return false;
                }
                catch (RuntimeBinderException)
                {
                    return false;
                }
            }
        }

        public bool IsEnabled
        {
            get { return this.htmlElement.Enabled; }
        }

        public bool IsEnabledByClass
        {
            get { return !this.htmlElement.GetAttribute("class").Contains("disabled"); }
        }

        public bool IsSelected
        {
            get { return this.htmlElement.Selected; }
        }

        public string Value
        {
            get { return GetAttribute("value"); }
        }

        public bool IsActiveByClass
        {
            get { return this.htmlElement.GetAttribute("class").Contains("active"); }
        }

        public string TagName
        {
            get { return this.htmlElement.TagName; }
        }

        public string Text
        {
            get { return this.htmlElement.Text; }
        }

        public Point Location
        {
            get { return this.htmlElement.Location; }
        }

        public Size Size
        {
            get { return this.htmlElement.Size; }
        }

        public Guid GuId
        {
            get { return new Guid(this.Value); }
        }

        public void Click()
        {
            try
            {
                this.htmlElement.Click();
            }
            catch (RuntimeBinderException e)
            {
                Logger.Log($@"'RuntimeBinderException - {e.Message}' caught. Execute scrollIntoView js and click once again.");
                //((((IWrapsDriver)OriginalWebElement).WrappedDriver) as IJavaScriptExecutor).ExecuteScript("arguments[0].scrollIntoView(false)", OriginalWebElement);
                //Thread.Sleep(500);
                //this.htmlElement.Click();
            }
        }

        public virtual void Click(IWebElement htmlElement)
        {
            htmlElement.Click();
        }

        public void Clear()
        {
            this.htmlElement.Clear();
        }

        public virtual void Clear(IWebElement htmlElement)
        {
            htmlElement.Clear();
        }

        public void ClickByJavaScript()
        {
            throw new NotImplementedException();
        }

        public string GetAttribute(string attributeName)
        {
            return this.htmlElement.GetAttribute(attributeName);
        }

        public Dictionary<string, object> GetAllAttribute()
        {
            IJavaScriptExecutor javascriptDriver = (IJavaScriptExecutor)this.webDriver;
            Dictionary<string, object> attributes = javascriptDriver.ExecuteScript("var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;", this.htmlElement) as Dictionary<string, object>;
            return attributes;
        }

        public string GetCssValue(string cssValue)
        {
            return this.htmlElement.GetCssValue(cssValue);
        }

        public string GetProperty(string propertyName)
        {
            return this.htmlElement.GetProperty(propertyName);
        }

        public void Focus(IWebElement htmlElement)
        {
            htmlElement.SendKeys(Keys.Tab);
        }

        public void WaitForAttributeContainsValue(string attribute, string value)
        {
            //var pollWait = TimeSpan.FromSeconds(3);
            //this.WithTemporaryTimeout(pollWait, _ =>
            //       Waiter.SpinWaitEnsureSatisfied(() => _.GetAttribute(attribute).Contains(value),
            //         defaultElementWaitTimeout, TimeSpan.FromSeconds(1),
            //       $"Element attribute '{attribute}' still not contains value'{value}"));
        }

        public void WaitForAttributeContainsValue(string attribute, string value, TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public void WaitForDisappearence()
        {
            throw new NotImplementedException();
        }

        public void WaitForDisappearence(TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public void WaitForDisplayed()
        {
            throw new NotImplementedException();
        }

        public void WaitForDisplayed(TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public void WaitForEnablling()
        {
            throw new NotImplementedException();
        }

        public void WaitForEnablling(TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public void WaitForExistance()
        {
            throw new NotImplementedException();
        }

        public void WaitForExistance(TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }
    }
}