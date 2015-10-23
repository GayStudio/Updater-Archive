using System.Windows.Forms;

namespace MCUpdater
{
    static class x
    {
        public const string name = "MoeCraft";
        public static string ver = Application.ProductVersion;
        public static string pname = Application.ProductName;
        //public static string pname = "MoeCraft 更新程序";
        public static string path   = Application.StartupPath + "\\";
        public const string binpath = @".minecraft\";
        public const string moddir  = binpath + @"mods\1.7.10\";
        public const string updpath = @"updater\";
        public const string dlpath  = @"downloads\";
        public const string db = "db.db";
        public static string mcLibPath = path + binpath + @"versions\1.7.10\";
        public static string[] bakList = { "options.txt", "optionsof.txt", "servers.dat" };
    }
}
