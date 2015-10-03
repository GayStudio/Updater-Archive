using System.Reflection;

namespace MCUpdater
{
    static class x
    {
        public const string name = "MoeCraft";
        public static AssemblyName app = Assembly.GetExecutingAssembly().GetName();
        public static string ver = app.Version.ToString(); 
        public static string path   = System.IO.Directory.GetCurrentDirectory() + "\\";
        public const string binpath = @".minecraft\";
        public const string moddir  = binpath + @"mods\1.7.10\";
        public const string updpath = @"updater\";
        public const string dlpath  = @"downloads\";
        public const string db = "db.db";
        public static string mcLibPath = path + binpath + @"versions\1.7.10\";
        public static string[] bakList = { "options.txt", "optionsof.txt", "servers.dat" };
    }
}
