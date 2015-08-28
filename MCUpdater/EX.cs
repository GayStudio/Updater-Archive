﻿using System;
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
        private object exceptionObject;

        public EX(object exceptionObject)
        {
            this.exceptionObject = exceptionObject;
        }

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

        void url(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            url("mailto:kenvix@vip.qq.com");
        }

        private void openBlog_Click(object sender, EventArgs e)
        {
            url("http://zhizhe8.net/");
        }

        private void openForum_Click(object sender, EventArgs e)
        {
            url("http://moeclub.net/");
        }

        private void copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(EXMsg.Text);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
