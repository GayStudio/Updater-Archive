using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MCUpdater
{
    public partial class logViewer : Form
    {
        FileInfo fn;
        string path;
        Encoding encoding = Encoding.GetEncoding("utf-8");
        public logViewer(string f = "", string encode = "utf-8")
        {
            InitializeComponent();
            ofd.InitialDirectory = x.path + x.binpath;
            if(!string.IsNullOrEmpty(f))
            {
                openFile(f , encode);
            }
        }

        private void openFile(string f , string encode = "utf-8")
        {
            if (File.Exists(f))
            {
                try
                {
                    encoding = Encoding.GetEncoding(encode);
                    textBox.Text = File.ReadAllText(f, encoding).Replace("\n", "\r\n");
                    path = f;
                    fn = new FileInfo(f);
                    Text = "日志查看器 - " + fn.Name;
                }
                catch (Exception ex)
                {
                    error(ex.Message);
                }
            }
        }

        public void error(string msg, string title = "错误")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(textBox.Text);
            }
            catch(Exception ex)
            {
                error(ex.Message);
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            textBox.SelectAll();
        }

        private void 关闭LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fontButton_Click(object sender, EventArgs e)
        {
            if(font.ShowDialog() == DialogResult.OK)
            {
                textBox.Font      = font.Font;
                textBox.ForeColor = font.Color;
            }
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                openFile(ofd.FileName);
            }
        }

        private void 保存VToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    File.WriteAllText(path, textBox.Text, encoding);
                }
                catch (Exception ex)
                {
                    error(ex.Message);
                }
            }
        }

        private void 资源管理器EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe","/select,"+path);
            }
            catch (Exception ex)
            {
                error(ex.Message);
            }
        }
    }
}
