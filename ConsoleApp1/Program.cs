using CommonHelper.Helper.Config;
using CommonHelper.Utils;
using WebDriverHelper.DriverFactory;

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
