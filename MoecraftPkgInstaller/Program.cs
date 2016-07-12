﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace MoecraftPkgInstaller
{
    static class Program
    {
        public static string path = "";
        public static bool auto = false;
        public static string updater = "..\\MCUpdater.exe";

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            if(!File.Exists(updater))
            {
                if (!File.Exists(Application.StartupPath + "\\updater.name"))
                {
                    MessageBox.Show("找不到 MoeCraft Toolbox，请确保你已将本程序放置于 MoeCraft Toolbox 所在目录下的 updater 文件夹，并运行过 MoeCraft Toolbox ( V2.4 以上版本 )", "Moecraft Package Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(2);
                }
                updater = Application.StartupPath + "\\..\\" + File.ReadAllText("updater.name");
                if (!File.Exists(updater))
                {
                    MessageBox.Show("找不到 MoeCraft Toolbox ( updater.name 所指示的路径无效 )，请确保你已将本程序放置于 MoeCraft Toolbox 所在目录下的 updater 文件夹，并运行过 MoeCraft Toolbox ( V2.4 以上版本 )", "Moecraft Package Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(3);
                }
            }
            if (args.Length > 0)
            {
                foreach (string arg in args)
                {
                    switch (arg)
                    {
                        case "/auto":
                        case "-auto":
                            auto = true;
                            break;

                        case "/setass":
                        case "-setass":
                            try
                            {
                                string text = Properties.Resources.setass.Replace("{selfpath}", Application.ExecutablePath.Replace(@"\",@"\\"));
                                string path = Application.StartupPath + "\\" + "SetAssociation.reg";
                                File.WriteAllText(path, text);
                                Process.Start("regedit.exe", "/s \""+path+"\"");
                            }
                            catch(Exception ex)
                            {
                                if(!auto) MessageBox.Show("设置文件关联失败：" + ex.ToString(), "Moecraft Package Installer - 文件关联", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Environment.Exit(1);
                            }
                            if (!auto) MessageBox.Show("设置文件关联成功", "Moecraft Package Installer - 文件关联", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Environment.Exit(0);
                            break;

                        case "/unsetass":
                        case "-unsetass":
                            try
                            {
                                string text = Properties.Resources.unsetass.Replace("{selfpath}", Application.ExecutablePath.Replace(@"\",@"\\"));
                                string path = Application.StartupPath + "\\" + "UnSetAssociation.reg";
                                File.WriteAllText(path, text);
                                Process.Start("regedit.exe", "/s \"" + path + "\"");
                            }
                            catch (Exception ex)
                            {
                                if (!auto) MessageBox.Show("取消设置文件关联失败：" + ex.ToString(), "Moecraft Package Installer - 文件关联", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Environment.Exit(1);
                            }
                            if (!auto) MessageBox.Show("取消设置文件关联成功", "Moecraft Package Installer - 文件关联", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Environment.Exit(0);
                            break;
                    }
                }
                path = args[0];
            }
            else
            {
                MessageBox.Show("Moecraft 包安装程序用法：\r\nMoecraftPkgInstaller.exe [/auto] [/exit] [/setass] <包文件>\r\n\r\n/auto -- 在扫描完成后自动安装，无需用户同意\r\n/setass -- 设置文件关联并退出，仅可以与 /auto 连用表示静默设置。通过检查 ERRORLEVEL 变量，可以确定执行结果，1为失败，0为成功\r\n/unsetass -- 取消设置文件关联，其他同上。", "Moecraft Package Installer",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new main());
        }
    }
}
