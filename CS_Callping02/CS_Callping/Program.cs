using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;

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
            string[] cmd = { "ping", "8.8.8.8 -n 5" };
            string data = Cmd(cmd);
            Console.WriteLine(data);

            //---
            //C# 原生PING元件
            //https://blog.csdn.net/Andrew_wx/article/details/6628501
            //构造Ping实例
            Ping pingSender = new Ping();
            //Ping 选项设置
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            //测试数据
            string data01 = "test data abcabc";
            byte[] buffer = Encoding.ASCII.GetBytes(data01);
            //设置超时时间
            int timeout = 120;
            //调用同步 send 方法发送数据,将返回结果保存至PingReply实例
            PingReply reply = pingSender.Send("8.8.8.8", timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("答復的主機地址：" + reply.Address.ToString());
                Console.WriteLine("往返時間：" + reply.RoundtripTime);
                Console.WriteLine("生存時間（TTL）：" + reply.Options.Ttl);
                Console.WriteLine("是否控制數據包的分段：" + reply.Options.DontFragment);
                Console.WriteLine("緩沖區大小：" + reply.Buffer.Length);
            }
            else
            {
                Console.WriteLine(reply.Status.ToString());
            }

            //---C# 原生PING元件

            pause();
        }
    }
}