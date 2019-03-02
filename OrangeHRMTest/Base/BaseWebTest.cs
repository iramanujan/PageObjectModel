using NUnit.Framework;
using OrangeHRMTest.WebDriverContext;

namespace OrangeHRMTest.Base
{
    public class BaseWebTest
    {
        public WebDriverContextHelper WebDriverContext = null;

        [SetUp]
        public void BaseTestOneTimeSetUp()
        {
            WebDriverContext = new WebDriverContextHelper();
            WebDriverContext.BrowserFactory.WaitUntilPageLoad();
            WebDriverContext.BrowserFactory.ClearCookies();
            WebDriverContext.BrowserFactory.Maximize();
        }

        [TearDown]
        public void BaseTestOneTimeTearDown()
        {
            WebDriverContext.BrowserFactory.Quit();
        }
    }
}
