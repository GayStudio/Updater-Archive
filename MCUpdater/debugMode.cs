using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCUpdater
{
    public partial class debugMode : Form
    {
        Process ps;
        public debugMode(Process px)
        {
            InitializeComponent();
            ps = px;
            log(ps.StartInfo.FileName + " " + ps.StartInfo.Arguments + "\r\n===============================================================================\r\n");
            ps.StartInfo.RedirectStandardOutput = true;
            ps.Exited += Ps_Exited;
            ps.Start();
            ps.BeginOutputReadLine();
            ps.OutputDataReceived += new DataReceivedEventHandler((object drsender, DataReceivedEventArgs dre) =>
            {
                log(dre.Data);
            });
        }

        private void Ps_Exited(object sender, EventArgs e)
        {
            Text += " - 游戏已退出";
        }

        public delegate void logInvoke(string v);

        public void error(string msg, string title = "错误")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void 清空LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            text.Text = "";
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            text.SelectAll();
        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(text.Text);
            }
            catch (Exception ex)
            {
                error(ex.Message);
            }
        }

        private void 结束进程KToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!ps.HasExited)
            {
                ps.Kill();
            }
        }

        public void log(string v)
        {
            if(text.InvokeRequired)
            {
                logInvoke li = new logInvoke(log);
                Invoke(li,v);
            }
            else
            {
                text.AppendText(v+"\r\n");
            }
        }

        private void text_TextChanged(object sender, EventArgs e)
        {
            text.ScrollToCaret();
        }
    }
}
