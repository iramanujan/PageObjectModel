using CommonHelper.Helper.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommonHelper.Helper.Config.ToolConfigMember;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ToolConfigReader.GetToolConfig().Browser);
            Console.WriteLine(ToolConfigReader.GetToolConfig().Tool);
            Console.ReadKey();
        }
    }
}
