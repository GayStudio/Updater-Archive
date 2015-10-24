using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;

namespace MCUpdater
{
    static class p
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Main());
        }
    }
}
