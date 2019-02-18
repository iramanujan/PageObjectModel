using CommonHelper.Helper.Log;
using CommonHelper.Helper.Wait;
using System;
using System.Diagnostics;
using System.Linq;

namespace CommonHelper.Utils
{
    public class ProcessUtils
    {
        public static Process[] GetCurrentSessionProcessesByName(string name)
        {
            var currentSessionId = Process.GetCurrentProcess().SessionId;
            return Process.GetProcessesByName(name).Where(x => x.SessionId == currentSessionId).ToArray();
        }

        public static void KillProcesses(string processName)
        {
            Logger.LogExecute("Kill processes if any by name {0}", processName);
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(processName);
            Logger.LogExecute("{0} {1} processes found", processes.Length, processName);
            if (processes.Length == 0)
            {
                return;
            }
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                    //do nothing 
                }
            }
            WaitForProcessNotRunning(processName);
        }

        public static void WaitForProcessNotRunning(string processName)
        {
            Waiter.SpinWaitEnsureSatisfied(
                () => System.Diagnostics.Process.GetProcessesByName(processName).Length == 0, TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(1), "The process '" + processName + "' still running");
        }

        public static void WaitForProcessRunning(string processName)
        {
            Waiter.SpinWaitEnsureSatisfied(
                () => Process.GetProcessesByName(processName).Length > 0, TimeSpan.FromSeconds(30),
                TimeSpan.FromSeconds(1), "The process '" + processName + "' still not running");
        }

        public static void KillProcessByCmd(string processName)
        {
            Logger.LogExecute("Kill processes if any by name {0}", processName);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C taskkill /F /IM " + processName + " /T";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}
