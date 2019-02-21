using NUnit.Framework;
using OrangeHRMTest.WebDriverContext;
using WebDriverHelper.BrowserFactory;

namespace OrangeHRMTest.Base
{
    public class BaseWebTest
    {
        public WebDriverContextHelper WebDriverContext => new WebDriverContextHelper();

        [SetUp]
        public void BaseTestOneTimeSetUp()
        {
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
