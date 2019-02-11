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
            //Console.Write(ToolConfigReader.ToolConfigMembers.Browser.ToString());
            foo(LocalizationType.en);

            Console.ReadKey();
        }

        public static void foo(LocalizationType localizationType)
        {
            Console.Write(localizationType.ToString());
        }
    }
}
