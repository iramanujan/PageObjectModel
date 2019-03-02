using CommonHelper.Helper.Config;
using CommonHelper.Utils;
using WebDriverHelper.WebDriverFactory;
using WebDriverHelper.WebElementFactory.CustomHtmlElements;
using WebDriverHelper.WebElementFactory.HtmlElements.HtmlElement;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {

            var ObjLocalChromeDriver = new WebDriverFactory().CreateWebDriverFactory(ToolConfigReader.GetToolConfig().Browser, ToolConfigReader.GetToolConfig().ExecutionType).InitializeWebDriver();
            string url = ToolConfigReader.GetToolConfig().PageUrls;

            //var ObjLocalChromeDriver = new LocalChromeDriver().InitializeWebDriver();
            ObjLocalChromeDriver.Navigate().GoToUrl(url);

            System.Threading.Thread.Sleep(10 * 1000);
            var userName = CustomFindElement.FindHtmlElement(ObjLocalChromeDriver,How.XPath, "//*[@id='txtUsername']");
            userName.SendKeys("admin");
            


            var password = CustomFindElement.FindHtmlElement(ObjLocalChromeDriver, How.CssSelector, "#txtPassword");
            password.SendKeys("admin");


            CustomFindElement.FindHtmlElement(ObjLocalChromeDriver, How.CssSelector, "#btnLogin").Click();

            ObjLocalChromeDriver.Close();
            ObjLocalChromeDriver.Quit();
            ObjLocalChromeDriver.Dispose();



            ProcessUtils.KillProcessByCmd("iexplore.exe");
            ProcessUtils.KillProcesses("IEDriver.exe");
            
            //var ObjLocalFireFoxDriverFactory = new LocalFireFoxDriverFactory().InitializeWebDriver();
            //ObjLocalFireFoxDriverFactory.Navigate().GoToUrl(url);
            //ObjLocalFireFoxDriverFactory.Close();
            //ObjLocalFireFoxDriverFactory.Quit();

            //var ObjLocalInternetExplorerDriverFactory = new LocalInternetExplorerDriverFactory().InitializeWebDriver();
            //ObjLocalInternetExplorerDriverFactory.Navigate().GoToUrl(url);
            //ObjLocalInternetExplorerDriverFactory.Close();
            //ObjLocalInternetExplorerDriverFactory.Quit();


            //Console.WriteLine(ToolConfigReader.GetToolConfig().Browser);
            //Console.WriteLine(ToolConfigReader.GetToolConfig().Tool);
            //Console.ReadKey();
        }
    }
}
