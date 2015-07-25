using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCUpdater
{
    public partial class EX : Form
    {
        public EX(Exception ex)
        {
            InitializeComponent();
            EXMsg.Text  = x.name + " V" + x.ver + " | " + System.Environment.OSVersion;
            EXMsg.Text += "\r\n异常描述：" + ex.Message;
            EXMsg.Text += "\r\n产生时间：" + DateTime.Now.ToLocalTime().ToString();
            EXMsg.Text += "\r\n源：" + ex.Source;
            EXMsg.Text += "\r\n================================================\r\n";
            EXMsg.Text += ex.StackTrace;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("mailto:kenvix@vip.qq.com");
            } catch(Exception ex) {
                MessageBox.Show(ex.Message , "系统错误", MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
            
        }

        private void openBlog_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://zhizhe8.net/");
            } catch(Exception ex) {
                MessageBox.Show(ex.Message , "系统错误", MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
        }
    }
}
