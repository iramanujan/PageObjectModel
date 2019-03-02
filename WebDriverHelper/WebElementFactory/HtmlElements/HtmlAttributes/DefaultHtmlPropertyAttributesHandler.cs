using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Reflection;

namespace WebDriverHelper.WebElementFactory.HtmlElements.HtmlAttributes
{
    public class DefaultHtmlPropertyAttributesHandler : HtmlAttributesHandler
    {

        public PropertyInfo Property { get; private set; }

        public DefaultHtmlPropertyAttributesHandler(PropertyInfo property)
        {
            Property = property;
        }

        public override bool ShouldCache()
        {
            return Property.GetCustomAttribute<CacheLookupAttribute>(false) != null;
        }

        public override By BuildBy()
        {
            By ans = null;

            FindsByAttribute[] findBys =
                (FindsByAttribute[])Property.GetCustomAttributes(typeof(FindsByAttribute), false);
            if (findBys.Length > 0)
            {
                ans = BuildBy(findBys[0]);
            }

            if (ans == null)
            {
                ans = BuildByFromDefault();
            }

            if (ans == null)
            {
                throw new ArgumentException("Cannot determine how to locate element " + Property);
            }

            return ans;
        }

        protected virtual By BuildByFromDefault()
        {
            return new ByChained(By.Id(Property.Name), By.Name(Property.Name));
        }

    }
}
