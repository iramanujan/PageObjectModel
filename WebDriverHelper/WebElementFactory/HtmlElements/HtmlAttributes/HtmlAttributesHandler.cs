using System;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace WebDriverHelper.WebElementFactory.HtmlElements.HtmlAttributes
{
    public abstract class HtmlAttributesHandler
    {
        public abstract By BuildBy();

        public abstract bool ShouldCache();

        protected virtual By BuildByFromFindsByValues(FindsByAttribute[] findByValues)
        {
            AssertValidFindBys(findByValues);

            By[] byArray = new By[findByValues.Length];
            for (int i = 0; i < findByValues.Length; i++)
            {
                byArray[i] = BuildByFromFindsBy(findByValues[i]);
            }

            return new ByChained(byArray);
        }

        protected virtual By BuildByFromFindsBy(FindsByAttribute findsBy)
        {
            AssertValidFindBy(findsBy);
            return BuildBy(findsBy);
        }

        protected virtual By BuildBy(FindsByAttribute findsBy)
        {
            How how = findsBy.How;
            string usingStr = findsBy.Using;
            Type customType = findsBy.CustomFinderType;
            return ByExtension.FindElementBy(how, usingStr, customType);
        }

        private void AssertValidFindBys(FindsByAttribute[] findBys)
        {
            foreach (FindsByAttribute findBy in findBys)
            {
                AssertValidFindBy(findBy);
            }
        }

        private void AssertValidFindBy(FindsByAttribute findBy)
        {
            if (findBy.How == How.Custom)
            {
                if (findBy.CustomFinderType == null)
                {
                    throw new ArgumentException(
                        "If you set the 'How' property to 'Custom' value, you must also set 'CustomFinderType'");
                }
                if (!findBy.CustomFinderType.IsSubclassOf(typeof(By)))
                {
                    throw new ArgumentException("Custom finder type must be a descendent of the 'By' class");
                }

                ConstructorInfo ctor = findBy.CustomFinderType.GetConstructor(new Type[] { typeof(string) });
                if (ctor == null)
                {
                    throw new ArgumentException(
                        "Custom finder type must expose a public constructor with a string argument");
                }
            }
        }
    }
}
