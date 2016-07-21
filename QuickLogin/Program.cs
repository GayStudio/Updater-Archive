using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace QuickLogin
{
    class Program
    {
        public const string xpath = "./updater/";

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("MoeCraft Quick Login // 作者：Kenvix [ http://kenvix.com ]");
                Console.Title = "MoeCraft Quick Login";
                if (!File.Exists(xpath + "quicklogin.info"))
                {
                    pause("错误：未能找到快捷登录配置文件，请到 用户中心->两步验证 下载此文件，放置到本程序所在的 updater 目录下，并改名为 quicklogin.info");
                    Environment.Exit(1);
                }
                string code = File.ReadAllText(xpath + "quicklogin.info");
                Console.Write("正在获取本机外网IP地址：");
                string ip = (new WebClient()).DownloadString("http://members.3322.org/dyndns/getip");
                Console.Write(ip + "\r\n");
                Console.WriteLine("正在准备快捷登录：");
                WebClient wc = new WebClient();
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                string postString = "code=" + code + "&ip=" + ip;
                byte[] postData = Encoding.UTF8.GetBytes(postString);
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] responseData = wc.UploadData("https://accounts.moecraft.net/index.php?m=home&c=mc&a=gameLoginAPI", "POST", postData);
                Console.Write(Encoding.UTF8.GetString(responseData) + "\r\n");
                pause("快捷登陆完成，按任意键退出");
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                pause("\r\n快捷登录遇到了不可恢复的错误。请将上述错误告诉我们，按任意键退出");
            }
        }

        private static void pause(string text = "请按任意键继续...")
        {
            Console.WriteLine(text);
            Console.ReadKey(true);
        }
    }
}
