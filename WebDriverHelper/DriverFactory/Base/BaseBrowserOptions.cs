using CommonHelper.Helper.Attributes;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.ComponentModel;

namespace WebDriverHelper.DriverFactory.Base
{
    public class BaseBrowserOptions
    {
        public enum BrowserLocalizationType
        {
            [Description("en")]
            en,
            [Description("de")]
            de,
            [Description("es")]
            es,
            [Description("fr")]
            fr,
            [Description("it")]
            it
        }
        

    }
}
