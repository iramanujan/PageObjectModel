using OpenQA.Selenium.Support.PageObjects;
using WebDriverHelper.WebElementFactory.HtmlElements.HtmlElement;

namespace OrangeHRM.PageEntity.Login
{
    public class LoginPage
    {

        [FindsBy(How = How.CssSelector, Using = "#txtUsername")]
        public HtmlTextBox Username { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#txtPassword")]
        public HtmlTextBox Password { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#btnLogin")]
        public HtmlButton Login { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#spanMessage")]
        public HtmlTextBox Message { get; set; }

        public string ErrorMessage => Message.Text.Trim();

    }
}