using log4net;
using System;
using System.Linq;
using System.Text;

namespace CommonHelper.Helper.Log
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        public static void Debug(string message, params object[] args)
        {
            var msg = args.Any() ? String.Format(message, args) : message;
            Console.WriteLine(msg);
            log.Debug(msg);
        }

        public static void Debug(string message, char seperator, int repeat = 40, params object[] args)
        {
            var sectionDivider = new string(seperator, repeat); ;
            Debug("");
            Debug(sectionDivider);
            Debug(message, args);
            Debug(sectionDivider);
        }

        public static void Error(string message, params object[] args)
        {
            var msg = args.Any() ? string.Format(message, args) : message;
            Console.WriteLine("Error: " + msg);
            log.Error(msg);
        }

        public static void Fatal(string message, params object[] args)
        {
            var msg = string.Format(message, args);
            Console.WriteLine(msg);
            log.Fatal(msg);
        }

        public static void Info(string message, params object[] args)
        {
            var msg = string.Format(message, args);
            LogToConsoleWithTimeStamp(msg);
            log.Info(msg);
        }

        public static void Log(string message, params object[] args)
        {
            Debug(message, args);
        }

        public static void LogExecute(string message, params object[] args)
        {
            var executeMsg = string.Format("EXECUTE: {0}", message);
            Info(executeMsg, args);
        }

        public static void LogValidate(string message, params object[] args)
        {
            var validateMsg = string.Format("VALIDATE: {0}", message);
            Info(validateMsg, args);
        }

        public static void Warn(string message, params object[] args)
        {
            var msg = string.Format(message, args);
            Console.WriteLine(msg);
            log.Warn(msg);
        }

        private static void LogToConsoleWithTimeStamp(string value)
        {
            Console.WriteLine("[{0}] {1}", DateTime.UtcNow, value);
        }

        public static string ReplaceCurlyBraces(string testMessage)
        {
            StringBuilder sb = new StringBuilder(testMessage);
            sb.Replace("{", "{{").Replace("}", "}}");
            return sb.ToString();
        }

    }
}
