using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace CS_Callping
{
    class Program
    {
        //C# CALL PING
        //https://stackoverflow.com/questions/41895971/c-sharp-output-command-prompt-in-real-time-to-a-text-box?noredirect=1&lq=1
        static void pause()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }
        static string Cmd(string[] cmd)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = cmd[0];
            proc.StartInfo.Arguments = cmd[1];
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;

            proc.Start();

            string stdout = "";
            using (StreamReader reader = proc.StandardOutput)
            {
                stdout += reader.ReadToEnd();
            }

            proc.WaitForExit();

            return stdout;
        }
        static void Main(string[] args)
        {
            string[] cmd = {"ping","8.8.8.8 -n 5"};
            string data = Cmd(cmd);
            Console.WriteLine(data);
            pause();
        }
    }
}
