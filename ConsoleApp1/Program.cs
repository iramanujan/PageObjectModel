using CommonHelper.Helper.Config;
using WebDriverHelper.DriverFactory.Chrome.Local;
using WebDriverHelper.DriverFactory.FireFox.Local;
using WebDriverHelper.DriverFactory.InternetExplorer.Local;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = ToolConfigReader.GetToolConfig().PageUrls;

            var ObjLocalChromeDriver = new LocalChromeDriver().InitializeWebDriver();
            ObjLocalChromeDriver.Navigate().GoToUrl(url);
            ObjLocalChromeDriver.Close();
            ObjLocalChromeDriver.Quit();

            var ObjLocalFireFoxDriverFactory = new LocalFireFoxDriverFactory().InitializeWebDriver();
            ObjLocalFireFoxDriverFactory.Navigate().GoToUrl(url);
            ObjLocalFireFoxDriverFactory.Close();
            ObjLocalFireFoxDriverFactory.Quit();

            var ObjLocalInternetExplorerDriverFactory = new LocalInternetExplorerDriverFactory().InitializeWebDriver();
            ObjLocalInternetExplorerDriverFactory.Navigate().GoToUrl(url);
            ObjLocalInternetExplorerDriverFactory.Close();
            ObjLocalInternetExplorerDriverFactory.Quit();


            //Console.WriteLine(ToolConfigReader.GetToolConfig().Browser);
            //Console.WriteLine(ToolConfigReader.GetToolConfig().Tool);
            //Console.ReadKey();
        }
    }
}
