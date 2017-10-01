using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallerAgent
{
    class Program
    {
        static int Main(string[] args)
        {
            if(args.Length < 1)
            {
                Environment.Exit(10);
            }
            string file = args[0];
            if(File.Exists(file))
            {
                Process ps = new Process();
                ps.StartInfo.FileName = "cmd.exe";
                ps.StartInfo.Arguments = "/c call \"" + args[0] + "\"";
                ps.StartInfo.UseShellExecute = false;
                ps.StartInfo.CreateNoWindow = true;
                ps.StartInfo.RedirectStandardOutput = true;
                ps.StartInfo.Verb = "runas";
                ps.Start();
                return 0;
            } else
            {
                return 200;
            }
        }
    }
}
