﻿using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace MCUpdater
{
    static class p
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        ///
        [STAThread]
        static void Main()
        {
#if !DEBUG
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);
            //处理UI线程异常
            Application.ThreadException += new ThreadExceptionEventHandler((object sender, ThreadExceptionEventArgs e) =>
            {
                EX ex = new EX(e.Exception);
                ex.ShowDialog();
            });
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((object sender, UnhandledExceptionEventArgs e) =>
            {
                EX ex = new EX(e.ExceptionObject);
                ex.ShowDialog();
            });
#endif
            initResFile();
            Application.EnableVisualStyles();
            Application.Run(new Main());
        }

        /// <summary>
        /// 释放资源文件
        /// </summary>
        private static void initResFile()
        {
            if (!Directory.Exists(x.updpath))
            {
                Directory.CreateDirectory(x.updpath);
            }
            if (!Directory.Exists(x.path + x.updpath + x.dlpath))
            {
                Directory.CreateDirectory(x.path + x.updpath + x.dlpath);
            }
            if(!File.Exists(x.path + x.updpath + "config.xml"))
            {
                File.WriteAllBytes(x.path + x.updpath + "config.xml", Properties.Resources.config);
            }
            File.WriteAllText(x.path + x.updpath + "updater.name", Application.ExecutablePath.Substring(Application.StartupPath.Length + 1));
        }
    }
}
