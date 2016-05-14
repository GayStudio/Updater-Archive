using System;
using System.Windows.Forms;

namespace MoecraftPkgInstaller
{
    static class Program
    {
        public static string path = "";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            if (args.Length > 0)
            {
                path = args[0];
            } else
            {
                MessageBox.Show("Moecraft 包安装程序用法：\r\nMoecraftPkgInstaller.exe <包文件> [/auto] [/exit]\r\n\r\n/auto -- 在扫描完成后自动安装，无需用户同意\r\n/exit -- 生成临时安装脚本，并在安装时关闭包安装器","Moecraft Package Installer",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new main());
        }
    }
}
