using CommonHelper.Helper.Config;
using WebDriverHelper.DriverFactory;
using WebDriverHelper.DriverFactory.Chrome.Local;
using WebDriverHelper.DriverFactory.FireFox.Local;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            LocalChromeDriver ObjLocalChromeDriver = new LocalChromeDriver();
            var obj = ObjLocalChromeDriver.InitializeWebDriver();
            obj.Navigate().GoToUrl(ToolConfigReader.GetToolConfig().PageUrls);
            obj.Close();
            obj.Quit();

            var firObj = new LocalFireFoxDriverFactory().InitializeWebDriver();

            firObj.Navigate().GoToUrl(ToolConfigReader.GetToolConfig().PageUrls);
            firObj.Close();
            firObj.Quit();

            //Console.WriteLine(ToolConfigReader.GetToolConfig().Browser);
            //Console.WriteLine(ToolConfigReader.GetToolConfig().Tool);
            //Console.ReadKey();
        }
    }
}
