using System;
using System.IO;
using System.Windows.Forms;

namespace MCUpdater
{
    static class x
    {
        public const string name = "MoeCraft";
        public const string updpath = @"updater\";
        public const string dlpath = @"downloads\";
        public const string platformFlag = "windows";
        public static string pathConfigTxtDir = path + updpath + "paths\\";
        public const int sleep = 25;
        public static string ver = Application.ProductVersion;
        public static string pname = Application.ProductName;
        //public static string pname = "MoeCraft 更新程序";
        public static string path   = Application.StartupPath + "\\"; //{{updaterpath}}
        public static float osver = float.Parse(Environment.OSVersion.Version.Major + "." + Environment.OSVersion.Version.Minor);
        public static string binpath = getPathFromConfigTxt("binpath");
        public static string moddir  = getPathFromConfigTxt("moddir");
        public static string mcLibPath = getPathFromConfigTxt("mcLibPath");
        public static string[] bakList = getPathFromConfigTxt("bakList").Replace("\r","").Split('\n');

        public static void AsyncSleep(int time)
        {
            int count = time / sleep;
            for(int i = 0; i < count; i++)
            {
                System.Threading.Thread.Sleep(sleep);
                Application.DoEvents();
            }
        }

        public static string getPathFromConfigTxt(string name)
        {
            if (!Directory.Exists(pathConfigTxtDir)) Directory.CreateDirectory(pathConfigTxtDir);
            if (File.Exists(pathConfigTxtDir + name  + ".txt"))
            {
                return File.ReadAllText(pathConfigTxtDir + name + ".txt")
                    .Replace("{{binpath}}",binpath)
                    .Replace("{{path}}",path)
                    .Replace("{{updpath}}", updpath);
            }
            else
            {
                File.WriteAllText(pathConfigTxtDir + name + ".txt", "");
                return "";
            }
        }
    }
}
