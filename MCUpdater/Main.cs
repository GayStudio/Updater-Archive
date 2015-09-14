﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Data;

namespace MCUpdater
{
    public partial class Main : Form
    {
        private a conn;
        private WebClient w;
        private bool updateFlag = false;
        private string updateError;
        public Main()
        {
            try
            {
                conn = new a();
            }
            catch (Exception ex)
            {
                MessageBox.Show("致命错误：启动数据库失败：\r\n" + ex.Message,"初始化失败",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            InitializeComponent();
#if !DEBUG
            mainTabControl.TabPages.Remove(mainTabControl.TabPages[5]); //隐藏启动器页面
#endif
            version.Text = "V"+x.ver;
            if (!Directory.Exists(x.updpath))
            {
                Directory.CreateDirectory(x.updpath);
                log("初始化：创建目录：" + x.updpath);
            }
            if (!Directory.Exists( x.path + x.updpath + x.dlpath))
            {
                Directory.CreateDirectory(x.path + x.updpath + x.dlpath);
                log("初始化：创建目录：" + x.path + x.updpath + x.dlpath);
            }
            /*
            if (string.IsNullOrEmpty(playerJRE.Text))
            {
                string jrePath = @"SOFTWARE\JavaSoft";
                var jre = Registry.LocalMachine.OpenSubKey(jrePath, true).GetSubKeyNames();
                error(jre[0]);
                //error(jre);
                try
                {
                    RegistryKey key = Registry.LocalMachine;
                    string jre = key.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment").GetValue("CurrentVersion").ToString();
                    error(jre);
                }
                catch (Exception)
                {
                    log("未能确定 JRE 安装目录，请手动指定");
                }
            }
            */
            DataSet d = conn.fetch("select * from `cdn`");
            foreach(DataRow cdn in d.Tables[0].Rows)
            {
                updateServer.Items.Add(cdn["desc"]);
            }
            updateServer.SelectedIndex = 0;
            getModList();
            /*
            playerName.Text   = conn.getOpt("playerName");
            playerWidth.Text  = conn.getOpt("playerWidth");
            playerHeight.Text = conn.getOpt("playerHeight");
            playerJRE.Text    = conn.getOpt("playerJRE");
            playerArgs.Text   = conn.getOpt("playerArgs");
            string playerMode = conn.getOpt("playerMode");
            if (conn.getOpt("playerFS") == "1")
            {
                playerFS.Checked = true;
            }
            else
            {
                playerFS.Checked = false;
            }

            if (playerMode == "1")
            {
                playerMode1.Checked = true;
            }
            else if (playerMode == "2")
            {
                playerMode2.Checked = true;
            }
            else if (playerMode == "3")
            {
                playerMode3.Checked = true;
            }

            if (conn.getOpt("playerClose") == "1")
            {
                playerClose.Checked = true;
            }
            else
            {
                playerClose.Checked = false;
            }
            log("读取用户设置成功");
             */
            if (conn.getOpt("disMcCheck") == "0" && !Directory.Exists(x.mcLibPath))
            {
                disMcCheck.Checked = false;
                if (MessageBox.Show("你尚未安装 "+x.name+"，无法进行游戏，是否要现在安装？\r\n你可以稍候在 检查更新 页面安装\r\n如果你不想看到此提示，可以在 关于 页面禁用", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    doUpdate();
                    forceUpdateAssets.Checked = true;
                    forceUpdateConfig.Checked = true;
                    forceUpdateCore.Checked = true;
                    forceUpdateMods.Checked = true;
                    forceUpdateOmods.Checked = true;
                    forceUpdateRoot.Checked = true;
                }
            }
            else
            {
                disMcCheck.Checked = true;
            }
            log("启动成功: " + " V" + x.ver + " | " + System.Environment.OSVersion);
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log">日志</param>
        public void log(string log)
        {
            logBox.Text += "[" + DateTime.Now.ToString("H:m:s") + "] " + log + "\r\n";
            if (log.Length > 80)
            {
                lastLog.Text = "[" + DateTime.Now.ToString("H:m:s") + "] " + log.Substring(0, 80);
            }
            else
            {
                lastLog.Text = "[" + DateTime.Now.ToString("H:m:s") + "] " + log;
            }
        }
        public void error(string msg, string title = "错误")
        {
            log(title + "：" + msg.Replace("\r\n", " "));
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void success(string msg, string title = "操作完成")
        {
            log(title + "：" + msg.Replace("\r\n", " "));
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void listMCLib()
        {

        }

        private void playerJRE_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void progressGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void updateAction_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://zhizhe8.net/");
            }
            catch (Exception ex)
            {
                error(ex.Message);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://www.stus8.com");
            }
            catch (Exception ex)
            {
                error(ex.Message);
            }
        }

        private void launcherButton_Click(object sender, EventArgs e)
        {
            if(!File.Exists(x.path + conn.getOpt("launcherPath"))) {
                error("未找到启动器！请确保你已经更新启动器到最新版本，且启动器没有改名。请手动打开启动器", "打开启动器失败");
            }
            else
            {
                try
                {
                    Process.Start(x.path + conn.getOpt("launcherPath"));
                }
                catch(Exception ex)
                {
                    error(ex.Message, "打开启动器失败");
                }
            }
        }

        private void cleanDownloadCache_Click(object sender, EventArgs e)
        {
            if(Directory.Exists(x.path + x.updpath + x.dlpath)) {
                try
                {
                   
                    Directory.CreateDirectory(x.path + x.updpath + x.dlpath);
                }
                catch (Exception ex)
                {
                    error(ex.Message,"清除下载缓存失败");
                }
            }
            log("清除下载缓存成功");
            success("清除下载缓存成功","清除下载缓存成功");
        }
        
        private void regLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://accounts.moecraft.net/index.php?m=home&c=user&a=reg");
            }
            catch (Exception ex)
            {
                error(ex.Message + "\r\n请手动打开：https://accounts.moecraft.net");
            }
        }

        private void mcLauncher_Click(object sender, EventArgs e)
        {
            
        }

        private void updateThisProgressText_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void checkUpdate_Click(object sender, EventArgs e)
        {

        }

        private void kenvixUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://zhizhe8.net");
        }

        private void stusUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.stus8.com");
        }

        private void accountsUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://accounts.moecraft.net");
        }

        private void joinGroupUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://jq.qq.com/?_wv=1027&k=ewYmnq");
        }

        private void bbsUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://moeclub.net");
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void regionCalculatorButton_Click(object sender, EventArgs e)
        {
            regionCalculator rc = new regionCalculator();
            rc.Show();
        }

        private void disMcCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(disMcCheck.Checked)
            {
                conn.setOpt("disMcCheck","1");
            }
            else
            {
                conn.setOpt("disMcCheck","0");
            }
        }

        private void updateServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.setOpt("updateServer",updateServer.SelectedIndex.ToString());
        }
    }
}