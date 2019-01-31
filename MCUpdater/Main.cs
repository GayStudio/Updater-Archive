using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using Microsoft.Win32;
using System.Reflection;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MCUpdater
{
    public partial class Main : Form
    {
        private config conn;
        private cdn cdnc;
        private string updateError;
        public bool inited = false;
        private int nowUpdate = 0;
        private XmlElement xn;
        public bool updateFlag;

        public Main()
        {
            try
            {
                conn = new config();
                cdnc = new cdn();
            }
            catch (Exception ex)
            {
                MessageBox.Show("致命错误：无法打开配置文件：\r\n" + ex.Message + "\r\n" + ex.StackTrace, x.pname + " 初始化失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            if (conn.get("disVisualStyles") == "1" || p.arg.Contains("/ugly") || p.arg.Contains("-ugly"))
            {
                Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;
                
            }
            InitializeComponent();
            #region UI
            label2.Text = x.pname;
            this.Text = x.pname + " Ver." + Application.ProductVersion;
            if (p.pkgInstall)
            {
                Text += " - StandAlone Package Install Mode";
                mainTabControl.SelectedIndex = 1;
            }
            version.Text = x.ver;
            diyDialog.HelpRequest += (object sender, EventArgs e) =>
            {
                MessageBox.Show("这个功能并没有什么用\r\n尽管现在已经可以保存这个设置了", "所谓的帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
#if !DEBUG
            mainTabControl.TabPages.Remove(mainTabControl.TabPages[6]); //隐藏启动器页面
#endif
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
            var cdnListVar = cdnc.list();
            foreach (System.Xml.XmlElement cdnInfo in cdnListVar)
            {
                updateServer.Items.Add(cdnInfo.GetAttribute("desc"));
            }
            getModList();
            /*
            playerName.Text   = conn.get("playerName");
            playerWidth.Text  = conn.get("playerWidth");
            playerHeight.Text = conn.get("playerHeight");
            playerJRE.Text    = conn.get("playerJRE");
            playerArgs.Text   = conn.get("playerArgs");
            string playerMode = conn.get("playerMode");
            if (conn.get("playerFS") == "1")
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

            if (conn.get("playerClose") == "1")
            {
                playerClose.Checked = true;
            }
            else
            {
                playerClose.Checked = false;
            }
            log("读取用户设置成功");
             */
            if (conn.get("disMcCheck") == "0" && p.pkgInstall != false)
            {
                disMcCheck.Checked = false;
                if (!Directory.Exists(x.mcLibPath))
                {
                    if (MessageBox.Show("你尚未安装 " + x.name + "，无法进行游戏，是否要现在安装？\r\n将使用默认节点下载安装。你可以稍候在 检查更新 页面安装\r\n\r\n如果你不想看到此提示，可以在 关于 页面禁用", x.pname, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        doCheckUpdate();

                    }
                }
            }
            else
            {
                disMcCheck.Checked = true;
            }
            var ass = Registry.ClassesRoot.OpenSubKey("MoecraftUpdater.Pkg\\shell\\open");
            if (ass == null)
            {
                success("你尚未设置 MoeCraft Package ( .moecraftpkg ) 文件关联，稍后将设置关联\r\n\r\n如果你要移动 MoeCraft 到其他目录，请到 关于 页面重新设置关联\r\n如果你要删除 MoeCraft，请到 关于 页面取消关联后再卸载", x.pname);
                try
                {
                    Process.Start(x.path + x.updpath + "MoecraftPkgInstaller.exe", "-setass");
                }
                catch (Exception) { }
            }
            loadPlayerSettings();
            #endregion
            updateServer.SelectedIndex = 0;
            log("Process Command Line: " + Environment.CommandLine);
            log("Process Current Directory: " + Environment.CurrentDirectory);
            log("User@Machine: " + Environment.UserName +  "@" + Environment.MachineName);
            log("启动成功: " + " V" + x.ver + " | (" + x.osver.ToString() + ") " + Environment.OSVersion);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openUrl("http://kenvix.com/");
        }

        private void launcherButton_Click(object sender, EventArgs e)
        {
            if(!File.Exists(x.path + conn.get("launcherPath"))) {
                error("未找到启动器！请确保你已经更新启动器到最新版本，且启动器没有改名。请手动打开启动器", "打开启动器失败");
            }
            else
            {
                try
                {
                    Process.Start(x.path + conn.get("launcherPath"));
                }
                catch(Exception ex)
                {
                    error(ex.Message, "打开启动器失败");
                }
            }
        }

        /// <summary>
        /// 清除下载缓存。多线程使用范例
        /// </summary>
        private void cleanDownloadCache_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            if(Directory.Exists(x.path + x.updpath + x.dlpath)) {
                Thread th = new Thread(() => //直接用lambda表达式声明一个匿名委托。
                {
                    try
                    {
                        Directory.Delete(x.path + x.updpath + x.dlpath, true);
                        Directory.CreateDirectory(x.path + x.updpath + x.dlpath);
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                    }
                });
                th.Start(); //启动线程
                while (!th.Join(x.sleep)) //在25ms(x类常量)内等待线程完成并阻塞主线程
                {
                    Application.DoEvents(); //处理消息保证用户察觉不到
                }
                //循环结束，说明线程已经完成了工作
            }
            if(string.IsNullOrEmpty(errorMsg))
            {
                success("清除下载缓存成功", "清除下载缓存成功");
            }
            else
            {
                error(errorMsg, "清除下载缓存失败");
            }
        }
        
        private void regLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openUrl("https://accounts.moecraft.net/index.php");
        }

        private void kenvixUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openUrl("http://kenvix.com");
        }

        private void stusUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openUrl("http://www.stus8.com");
        }

        private void accountsUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openUrl("https://accounts.moecraft.net");
        }

        private void joinGroupUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openUrl("http://jq.qq.com/?_wv=1027&k=ewYmnq");
        }

        private void bbsUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openUrl("https://telegram.me/moecraftnews");
        }

        private void regionCalculatorButton_Click(object sender, EventArgs e)
        {
            regionCalculator rc = new regionCalculator();
            rc.Show();
        }

        private void disMcCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(inited)
            {
                if (disMcCheck.Checked)
                {
                    conn.set("disMcCheck", "1");
                }
                else
                {
                    conn.set("disMcCheck", "0");
                }
            }
        }

        private void updateServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.set("updateServer",updateServer.SelectedIndex.ToString());
            if(nowUpdate != 0)
            {
                updateButton.Text = "检查更新 (&C)";
                updateAction.Text = "节点已切换。需要重新检查更新，请点击右下角的 [ 检查更新 ] 按钮";
                updateThisProgressBar.Value = 0;
                updateThisProgressText.Text = "准备就绪";
                nowUpdate = 0;
            }
        }

        #region 查看MC崩溃日志
        private void crashReportViewerButton_Click(object sender, EventArgs e)
        {
            if(!Directory.Exists(x.path + x.binpath + @"crash-reports"))
            {
                error("没有最近的崩溃日志可供查看\r\n崩溃日志目录不存在");
                return;
            }
            Thread th = new Thread(() =>
            {
                var logList = Directory.GetFiles(x.path + x.binpath + @"crash-reports", "*.txt", SearchOption.TopDirectoryOnly);
                logViewer lv = new logViewer(logList.Last(), "gb2312");
                Application.Run(lv);
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        #endregion

        private void gameLogViewerButton_Click(object sender, EventArgs e)
        {
            var f = x.path + x.binpath + @"logs\latest.log";
            if (File.Exists(f))
            {
                logViewer lv = new logViewer(f);
                lv.Show();
            }
            else
            {
                error("没有最近的日志可供查看");
            }
        }

        private void updateLogButtom_Click(object sender, EventArgs e)
        {
            var f = x.path + x.updpath + @"new.txt";
            if (File.Exists(f))
            {
                logViewer lv = new logViewer(f);
                lv.Show();
            }
        }

        private void runStrongholdsCalculator_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(x.path + x.updpath + "StrongholdsCalculator.exe");
            }
            catch(Exception ex)
            {
                error(ex.Message,"启动失败");
            }
        }

        private void diyButton_Click(object sender, EventArgs e)
        {
            if(diyDialog.ShowDialog() == DialogResult.OK)
            {
                Font = diyDialog.Font;
                ForeColor = diyDialog.Color;
                Properties.Settings.Default.Font = Font;
                Properties.Settings.Default.ForeColor = ForeColor;
                Properties.Settings.Default.Save();
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("强制更新，即无论组件是否需要更新，都尝试下载并去更新它\r\n一般情况下并不需要强制更新\r\n\r\n如果更新器提示已经为最新版本，但是仍然无法进入游戏，可以考虑使用强制更新（如果能打开游戏界面，一般只需更新模组和配置文件即可）", "强制更新功能帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void openOmodsDir_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Directory.Exists(x.moddir))
                {
                    Directory.CreateDirectory(x.moddir);
                }
                Process.Start("explorer.exe", "\"" + x.moddir + "\"");
            }
            catch (Exception ex)
            {
                error(ex.Message, "启动失败");
            }
        }

        private void launcherButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                if(File.Exists(x.path + "Launcher.exe"))
                {
                    Process.Start(x.path + "Launcher.exe");
                }
                else if (File.Exists(x.path + "MCLauncher.exe"))
                {
                    Process.Start(x.path + "MCLauncher.exe");
                }
                else
                {
                    Process.Start(x.path + "启动器.exe");
                }
            }
            catch (Exception ex)
            {
                error(ex.Message, "启动失败");
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            if(!inited)
            {
                if (p.pkgInstall)
                {
                    SAUpdate();
                }
                inited = true;
            }
        }

        private void setAss_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(x.path + x.updpath + "MoecraftPkgInstaller.exe", "-setass");
            }
            catch (Exception ex)
            {
                error(ex.Message, "启动失败");
            }
        }

        private void unsetAss_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(x.path + x.updpath + "MoecraftPkgInstaller.exe", "-unsetass");
            }
            catch (Exception ex)
            {
                error(ex.Message, "启动失败");
            }
        }

        private void installPkg1_Click(object sender, EventArgs e)
        {
            selectPkg();
        }

        void selectPkg()
        {
            try
            {
                Process.Start(x.path + x.updpath + "MoecraftPkgInstaller.exe");
            }
            catch (Exception ex)
            {
                error(ex.Message, "启动失败");
            }
        }

        private void installPkg2_Click(object sender, EventArgs e)
        {
            selectPkg();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
           Type g = (new Properties.Resources()).GetType();
           ggimg.Image = (System.Drawing.Image)g.InvokeMember("gg_" + (new Random()).Next(0,6), BindingFlags.GetProperty, null, (g.InvokeMember(null,
           BindingFlags.DeclaredOnly |
           BindingFlags.Public | BindingFlags.NonPublic | 
           BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null)), null);
        }

        private void bignews_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            bignews.Visible = true;
            bignewsinit.Hide();
        }

        private void bignews_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            log("正在导航到网页：" + e.Url);
        }

        private void bignews_FileDownload(object sender, EventArgs e)
        {
            try
            {
                var urlobj = bignews.Document.ActiveElement;
                var url = urlobj.GetAttribute("href");
                if (string.IsNullOrEmpty(url)) return;
                string filename = url.Split('/')[url.Split('/').Length - 1];
                var moeset = urlobj.GetAttribute("moecraft");
                bool isquiet = false;
                bool ispkginstaller = false;
                bool isopen = false;
                bool isexec = false;
                bool iscontinue = false;
                bool isdownload = true;
                if (!string.IsNullOrEmpty(moeset))
                {
                    log("文件下载请求：" + url);
                    var moeres = moeset.Split(' ');
                    foreach(string moe in moeres)
                    {
                        switch(moe)
                        {
                            case "quiet": isquiet = true; break;
                            case "pkginstaller": ispkginstaller = true; break;
                            case "exec": isexec = true; break;
                            case "open": isopen = true; break;
                            case "continue": iscontinue = true; break;
                            case "nodownload": isdownload = false; break;
                        }
                    }
                } else
                {
                    return;
                }
                if(!iscontinue)
                {
                    urlobj.RemoveFocus();
                    bignews.Stop();
                }
                if (!isquiet && MessageBox.Show("检测到文件下载请求，你想要下载此文件吗？\r\n文件名：" + filename + "\r\n位于：" + url, "下载文件 - MoeCraft Toolbox Inner Browser", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) != DialogResult.OK)
                {
                    return;
                }
                logbreak();
                updateLog.Text += "检测到 MoeCraft 数据下载请求\r\n";
                mainTabControl.SelectedIndex = 1;
                if (isdownload)
                {
                    startUpdateDownload(url, filename);
                    if(isexec)
                    {
                        try
                        {
                            updateLog.Text += "运行程序："+ filename +"\r\n";
                            Process.Start(x.path + x.updpath + x.dlpath + filename);
                        }
                        catch (Exception) { }
                    }
                    if (isopen)
                    {
                        try
                        {
                            updateLog.Text += "打开文件：" + filename + "\r\n";
                            Process.Start("explorer.exe", "\"" + x.path + x.updpath + x.dlpath + filename + "\"");
                        }
                        catch (Exception) { }
                    }
                    if (ispkginstaller)
                    {
                        try
                        {
                            updateLog.Text += "运行 MoeCraft 包安装程序：" + filename + "\r\n";
                            Process.Start(x.path + x.updpath + "MoecraftPkgInstaller.exe", "\"" + x.path + x.updpath + x.dlpath + filename + "\" /auto /exit");
                        }
                        catch (Exception ex) {
                            error("启动包安装程序失败：" + ex.ToString());
                        }
                    }
                }
            }
            catch (NullReferenceException) { return; }
            catch (Exception ex) {
                log("无法处理文件下载请求：" + ex.ToString());
            }
        }

        private void updateLog_TextChanged(object sender, EventArgs e)
        {
            updateLog.ScrollToCaret();
        }

        private void logBox_TextChanged(object sender, EventArgs e)
        {
            logBox.ScrollToCaret();
        }

        private void bignews_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openUrl("https://telegram.me/moecraftbot");
        }

        void openUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                try
                {
                    Process.Start("explorer.exe \"" + url + "\"");
                }
                catch(Exception ex2)
                {
                    error("调用浏览器失败：\r\n" + ex2.Message + "\r\n请手动打开：" + url);
                }
            }
        }

        private void offlineModList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }

        private void offlineModList_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string filepath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                FileInfo fi = new FileInfo(filepath);
                File.Copy(filepath, x.path  + x.moddir + fi.Name);
                log("已添加 Mod: " + fi.Name);
            }
            catch (Exception ex)
            {
                log("添加 Mod 失败：" + ex.Message);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (nowUpdate == 2)
            {
                endUpdateAction();
                updateAction.Text = "更新已取消";
                updateLog.AppendText(updateAction.Text);
            }
            else if (nowUpdate == 1)
            {
                startUpdateAction();
            }
            else
            {
                doCheckUpdate();
            }
        }

        void doCheckUpdate()
        {
            nowUpdate = 2;
            var cdn = cdnc.get(updateServer.SelectedIndex);
            var server = cdn["url"] + cdn["xml"];
            updateAction.Text = "正在获取更新信息: " + server;
            updateLog.Text = updateAction.Text + "\r\n如果长时间无法获取到更新信息，可以先取消更新，再检查更新\r\n";
            updateButton.Text = "取消检查更新";
            string errorMsg = "";
            string result = "";
            startUpdateDownload(server, "update.xml", cdnc.getHeaders(updateServer.SelectedIndex));
            result = File.ReadAllText(x.path + x.updpath + x.dlpath + "update.xml");
            if (!string.IsNullOrEmpty(errorMsg))
            {
                error(errorMsg, "获取更新数据失败");
                endUpdateAction();
                return;
            }
            updateAction.Text = "获取本版更新信息成功。点击开始更新按钮可以运行更新。你可以在右侧选择要更新哪些组件。";
            readUpdateInfo(result);
        }

        bool platformShouldSkip(string platform)
        {
            if (!string.IsNullOrEmpty(platform))
            {
                string[] platformList = platform.Split(' ');
                foreach (string xplatform in platformList)
                {
                    if (xplatform == "all" || xplatform == x.platformFlag)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void readUpdateInfo(string result)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(result);
            }
            catch (Exception ex)
            {
                error(ex.Message + "\r\n" + result, "解析XML失败");
                endUpdateAction();
                return;
            }
            xn = doc.DocumentElement;
            if (xn.SelectSingleNode("desc") != null)
            {
                updateLog.Text = xn.SelectSingleNode("desc").InnerText.Replace("<br/>", "\r\n").Replace("<br>", "\r\n");
                logbreak();
                updateLog.Text += "以下内容可以更新：\r\n";
            }
            else
            {
                updateLog.Text = "暂无更新日志，以下内容可以更新：\r\n";
            }
            forceUpdate.Items.Clear();
            int i = 0;
            foreach (XmlElement val in xn.SelectSingleNode("libs"))
            {
                if (!platformShouldSkip(val.GetAttribute("platform")))
                {
                    forceUpdate.Items.Add(val.GetAttribute("name"));
                    if (!File.Exists(x.path + val.GetAttribute("path") + "\\" + val.Name + ".version"))
                    {
                        forceUpdate.SetItemChecked(i, true);
                        updateLog.Text += val.GetAttribute("name") + "：当前未安装，最新版本：" + val.GetAttribute("ver") + "\r\n";
                    }
                    else
                    {
                        string thisVer = File.ReadAllText(x.path + val.GetAttribute("path") + "\\" + val.Name + ".version");
                        if (float.Parse(thisVer) < float.Parse(val.GetAttribute("ver")))
                        {
                            forceUpdate.SetItemChecked(i, true);
                            updateLog.Text += val.GetAttribute("name") + "：当前版本：" + thisVer + "，最新版本：" + val.GetAttribute("ver") + "\r\n";
                        }
                    }
                }
                i++;
            }
            updateLog.Text += "\r\n在右侧选择你想要更新的内容（已自动勾选可以更新的内容），然后点击 确认更新\r\n";
            forceUpdate.Enabled = true;
            updateServer.Enabled = true;
            updateButton.Text = "确认更新 (&C)";
            nowUpdate = 1;
        }

        private void logbreak()
        {
            updateLog.Text += "\r\n---------------------------------------------------------------------------\r\n";
        }

        private void startUpdateAction()
        {
            if (xn == null)
            {
                error("用法错误");
                endUpdateAction();
                return;
            }
            nowUpdate = 2;
            forceUpdate.Enabled = false;
            updateServer.Enabled = false;
            updateButton.Text = "取消更新";
            log("启动更新");
            updateLog.Text += "---------------------------------------------------------------------------\r\n";
            int itemsCount = forceUpdate.Items.Count;
            for (int need = 0; need < itemsCount; need++)
            {
                if (forceUpdate.GetItemChecked(need))
                {
                    bool updateFatalError = false;
                    parseRequiredUpdateItems:
                    XmlElement var = (XmlElement)xn.SelectSingleNode("libs").ChildNodes[need];
                    if (!platformShouldSkip(var.GetAttribute("platform")))
                    {
                        updateLog.Text += "正在更新：" + var.GetAttribute("name") + " >> " + var.GetAttribute("ver") + "\r\n";
                        string installPath = var.GetAttribute("path").Replace('/', '\\');
                        if (!Directory.Exists(x.path + installPath))
                        {
                            Directory.CreateDirectory(x.path + installPath);
                        }
                        XmlElement down = (XmlElement)var.SelectSingleNode("download");
                        foreach (XmlElement downvar in down)
                        {
                            startUpdateDownload((downvar.InnerText).Replace("{{url}}", cdnc.get(updateServer.SelectedIndex)["url"]), downvar.GetAttribute("name"), cdnc.getHeaders(updateServer.SelectedIndex));
                            if (nowUpdate == 0) return;
                            if (!File.Exists(x.path + x.updpath + x.dlpath + downvar.GetAttribute("name")) || (new FileInfo(x.path + x.updpath + x.dlpath + downvar.GetAttribute("name"))).Length <= 0)
                            {
                                updateFatalError = true;
                            }
                            else
                            {
                                if (downvar.GetAttribute("unpack") != null && downvar.GetAttribute("unpack") == "1")
                                {
                                    string packageName;
                                    if (downvar.GetAttribute("name") != null)
                                    {
                                        packageName = downvar.GetAttribute("name");
                                    }
                                    else
                                    {
                                        packageName = var.Name + ".pkg";
                                    }
                                    doUnpack:
                                    try
                                    {
                                        runUnpack(x.path + x.updpath + x.dlpath + packageName, installPath);
                                    }
                                    catch (Exception ex)
                                    {
                                        DialogResult error = MessageBox.Show(ex.Message + "\r\n\r\n" + packageName, "解包数据失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                                        if (error == DialogResult.Retry)
                                        {
                                            goto doUnpack;
                                        }
                                        else
                                        {
                                            updateFatalError = true;
                                        }
                                    }
                                }
                            }
                        }
                        XmlElement bat = (XmlElement)var.SelectSingleNode("script");
                        if (bat != null)
                        {
                            writeBat:
                            try
                            {
                                runInstaller(bat.InnerText, installPath);
                            }
                            catch (Exception ex)
                            {
                                DialogResult error = MessageBox.Show(ex.Message + "\r\n\r\n" + x.path + installPath, "写入安装脚本失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                                if (error == DialogResult.Retry)
                                {
                                    goto writeBat;
                                }
                                else
                                {
                                    updateFatalError = true;
                                }
                            }
                        }
                        writeVersionFile:
                        try
                        {
                            if (!updateFatalError)
                            {
                                File.WriteAllText(x.path + installPath + "\\" + var.Name + ".version", var.GetAttribute("ver"));
                            }
                            else
                            {
                                updateLog.Text += "更新失败：" + var.GetAttribute("name") + "\r\n";
                            }
                        }
                        catch (Exception ex)
                        {
                            DialogResult error = MessageBox.Show(ex.Message + "\r\n\r\n" + x.path + installPath, "写入版本信息失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            if (error == DialogResult.Retry)
                            {
                                goto writeVersionFile;
                            }
                            else
                            {
                                updateFatalError = true;
                            }
                        }
                    }
                    else
                    {
                        need++;
                        itemsCount++;
                        if (itemsCount <= forceUpdate.Items.Count)
                        {
                            goto parseRequiredUpdateItems;
                        }
                    }
                }
            }
            updateAction.Text = "更新完成";
            updateLog.AppendText("******更新完成******\r\n");
            log("更新完成");
            endUpdateAction();
        }

        /// <summary>
        /// 解压缩更新包
        /// </summary>
        /// <param name="packageName">包的完整路径</param>
        /// <param name="installPath">安装路径</param>
        private void runUnpack(string packageName, string installPath)
        {
            updateAction.Text = "正在解压缩文件：" + packageName;
            updateLog.AppendText(updateAction.Text + "\r\n");
            Process p = new Process();
            p.StartInfo.FileName = x.path + x.updpath + "7z.exe";
            p.StartInfo.Arguments = "x -y -o\"" + x.path + installPath + "\" \"" + packageName + "\"";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.Verb = "runas";
            p.Start();
            string latest_info = "";
            string latest_error = "";
            p.OutputDataReceived += new DataReceivedEventHandler((object sender, DataReceivedEventArgs e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    latest_info = e.Data;
                    AppendText("[INFO] " + e.Data + "\r\n");
                }
            });
            p.ErrorDataReceived += new DataReceivedEventHandler((object sender, DataReceivedEventArgs e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    latest_error = e.Data;
                    AppendText("[ERROR] " + e.Data + "\r\n");
                }
            });
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            while (!p.WaitForExit(x.sleep))
            {
                Application.DoEvents();
            }
            Application.DoEvents();
            x.AsyncSleep(600);
            p.CancelOutputRead();
            p.CancelErrorRead();
            updateLog.Text += "7z 退出代码：" + p.ExitCode + "\r\n";
            if (p.ExitCode != 0)
            {
                if (MessageBox.Show("解压缩文件出错！退出码：" + p.ExitCode + "\r\n信息：" + latest_info + "\r\n错误：" + latest_error + "\r\n\r\n你想要重试解压缩吗？", "解压缩文件出错", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    runUnpack(packageName, installPath);
                }
            }
        }

        /// <summary>
        /// 运行BAT安装程序
        /// </summary>
        /// <param name="batOriginText">BAT原始内容</param>
        /// <param name="installPath">安装路径</param>
        /// <param name="sainstall">独立包安装时，填写包所在的目录</param>
        private void runInstaller(string batOriginText, string installPath, string sainstall = null)
        {
            updateAction.Text = "运行安装程序";
            updateLog.AppendText(updateAction.Text + "\r\n");
            string batPath = x.path + installPath + "\\install.bat";
            string batText = ("@echo off\n" +
                                "cd /D \"" + x.path + installPath + "\"\n" + batOriginText
                                .Replace("{{7z}}", x.path + x.updpath + "7z.exe")
                                .Replace("{{Root}}", x.path)
                                .Replace("{{DLDir}}", sainstall == null ? x.path + x.updpath + x.dlpath : sainstall)
                                .Replace("{{UpdPath}}", x.path + x.updpath)
                                .Replace("{{Path}}", x.path + installPath + "\\")).Replace("\n", "\r\n");
            File.WriteAllText(batPath, batText, System.Text.Encoding.GetEncoding("gb2312"));
            Process pb = new Process();
            pb.StartInfo.FileName = "cmd.exe";
            pb.StartInfo.Arguments = "/c call \"" + batPath + "\"";
            //pb.StartInfo.FileName = batPath;
            pb.StartInfo.UseShellExecute = false;
            pb.StartInfo.CreateNoWindow = true;
            pb.StartInfo.RedirectStandardOutput = true;
            pb.StartInfo.RedirectStandardError = true;
            pb.StartInfo.WorkingDirectory = Application.StartupPath;
            pb.StartInfo.Verb = "runas";
            pb.Start();
            string latest_info = "";
            string latest_error = "";
            pb.OutputDataReceived += new DataReceivedEventHandler((object sender, DataReceivedEventArgs e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    latest_info = e.Data;
                    AppendText("[INFO] " + e.Data + "\r\n");
                }
            });
            pb.ErrorDataReceived += new DataReceivedEventHandler((object sender, DataReceivedEventArgs e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    latest_error = e.Data;
                    AppendText("[ERROR] " + e.Data + "\r\n");
                }
            });
            pb.BeginOutputReadLine();
            pb.BeginErrorReadLine();
            while (!pb.WaitForExit(x.sleep) || !pb.HasExited)
            {
                Application.DoEvents();
            }
            Application.DoEvents();
            x.AsyncSleep(600);
            pb.CancelOutputRead();
            pb.CancelErrorRead();
            updateLog.Text += "安装程序退出代码：" + pb.ExitCode + "\r\n";
            if (pb.ExitCode != 0)
            {
                //针对Windows7错误码2的暴力修复
                //如果错误码2，且windows7，则explorer "install.bat"
                //Windows 10	10.0*
                //Windows 8.1 6.3 *
                //Windows 8   6.2
                //Windows 7   6.1
                if (x.osver <= 6.1 && pb.ExitCode == 1)
                {
                    updateLog.Text += "安装程序出错且错误码为1，正在使用对 Windows7 兼容方案\r\n";
                    Process pbc = new Process();
                    pbc.StartInfo.FileName = "explorer.exe";
                    pbc.StartInfo.Arguments = "\"" + batPath + "\"";
                    pbc.StartInfo.WorkingDirectory = Application.StartupPath;
                    pbc.StartInfo.Verb = "runas";
                    pbc.Start();
                    while (!pb.WaitForExit(x.sleep))
                    {
                        Application.DoEvents();
                    }
                    Application.DoEvents();
                    updateLog.Text += "[兼容方案] 安装程序退出代码：" + pb.ExitCode + "\r\n";
                    if (MessageBox.Show("使用兼容方案后安装程序脚本仍然执行出错！退出码：" + pb.ExitCode + "\r\n请勿中途关闭弹出的安装窗口，你想要重试执行安装吗？", "安装程序出错", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        runInstaller(batOriginText, installPath, sainstall);
                    }
                }
                else if (MessageBox.Show("安装程序脚本执行出错！退出码：" + pb.ExitCode + "\r\n信息：" + latest_info + "\r\n错误：" + latest_error + "\r\n\r\n你想要重试执行安装吗？", "安装程序出错", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    runInstaller(batOriginText, installPath, sainstall);
                }
            }
        }

        /// <summary>
        /// 结束更新界面操作
        /// </summary>
        private void endUpdateAction()
        {
            nowUpdate = 0;
            updateThisProgressBar.Style = ProgressBarStyle.Blocks;
            updateThisProgressBar.Value = 0;
            updateThisProgressText.Text = "";
            forceUpdate.Enabled = true;
            updateServer.Enabled = true;
            updateButton.Text = "检查更新 (&C)";
            updateButton.Enabled = true;
        }
        /// <summary>
        /// 判断是否需要强制更新
        /// </summary>
        private bool isForceUpdate(string v)
        {
            return false;
        }

        /// <summary>
        /// 独立更新
        /// </summary>
        private void SAUpdate()
        {
            try
            {
                string[] arg;
                bool unpack = false;
                bool exit = false;
                string name = null;
                string path = "";
                string to = "";
                string ver = "1.0";
                string desc = null;
                string batpath = null;
                foreach (string value in p.arg)
                {
                    string av = value.Substring(0, 1);
                    if (av == "/" || av == "-")
                    {
                        arg = value.Split('=');
                        string ak = arg[0].Substring(1);
                        string ad = arg.Length >= 2 ? arg[1] : null;
                        switch (ak)
                        {
                            case "unpack":
                                unpack = true;
                                break;
                            case "exit":
                                exit = true;
                                var tabs = mainTabControl.TabPages[1];
                                mainTabControl.Controls.Clear();
                                mainTabControl.Controls.Add(tabs);
                                updateButton.Enabled = false;
                                updateButton.Text = "正在安装";
                                updateServer.Items.Clear();
                                updateServer.Items.Add("独立包安装程序");
                                updateServer.SelectedIndex = 0;
                                forceUpdate.Items.Clear();
                                forceUpdate.Items.Add("ID: " + name);
                                forceUpdate.Items.Add(desc);
                                forceUpdate.Items.Add("Version: " + ver);
                                ControlBox = false;
                                break;
                            case "name":
                                if (ad != null)
                                {
                                    name = ad;
                                }
                                break;
                            case "path":
                                if (ad != null)
                                {
                                    path = ad.Replace("\"", "");
                                }
                                break;
                            case "to":
                                if (ad != null)
                                {
                                    to = ad.Replace("\"", "");
                                }
                                break;
                            case "ver":
                                if (ad != null)
                                {
                                    ver = ad;
                                }
                                break;
                            case "desc":
                                if (ad != null)
                                {
                                    desc = ad.Replace("\"", "");
                                }
                                break;
                            case "batpath":
                                if (ad != null)
                                {
                                    batpath = ad.Replace("\"", "");
                                }
                                break;
                        }
                    }
                }
                if (name == null) return;
                if (desc == null) desc = name;
                if (!string.IsNullOrEmpty(to) && !Directory.Exists(x.path + to))
                {
                    Directory.CreateDirectory(x.path + to);
                }
                updateLog.Text = "进入包独立安装模式\r\n-----------------------------------------------------\r\n";
                updateAction.Text = "正在安装包：" + desc + " V" + ver;
                updateLog.AppendText(updateAction.Text + "\r\n");
                updateLog.AppendText("输出目录：" + x.path + to + "\r\n");
                if (unpack && !string.IsNullOrEmpty(path)) runUnpack(path, to);
                if (!string.IsNullOrEmpty(batpath) && !string.IsNullOrEmpty(to) && File.Exists(batpath))
                {
                    runInstaller(File.ReadAllText(batpath), to, (new FileInfo(path).DirectoryName));
                }
                File.WriteAllText(x.path + to + "\\" + name + ".version", ver);
                updateAction.Text = "已成功安装包：" + desc + " V" + ver;
                if (exit)
                {
                    //fucking exit bug
                    Process.Start("taskkill", "/f " + Process.GetCurrentProcess().Id);
                    Environment.Exit(0);
                }
                else
                {
                    updateLog.AppendText("\r\n" + updateAction.Text + "\r\n您可以继续使用 " + x.pname + "，或者关闭它\r\n");
                }
            }
            catch (Exception ex)
            {
                error(ex.ToString(), "独立包安装程序无法安装此包");
            }
        }

        #region 处理压缩包

        public delegate void AppendTextCallback(string text);

        public void AppendText(string text)
        {
            if (updateLog.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(AppendText);
                updateLog.Invoke(d, text);
            }
            else
            {
                updateLog.AppendText(text);
            }
        }

        #endregion
        /// <summary>
        /// 开始下载
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="file">保存文件名</param>
        /// <param name="headers">头</param>
        protected void startUpdateDownload(string url, string file, Dictionary<string, string> headers = null)
        {
            try
            {
                WebClient wc = new WebClient();
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> xhead in headers)
                    {
                        wc.Headers.Set(xhead.Key, xhead.Value);
                    }
                }
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                wc.DownloadFileAsync(new Uri(url), x.path + x.updpath + x.dlpath + file);
                string fileSizeTotal = "--";
                string fileSizeReceived = "--";
                wc.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) =>
                {
                    try
                    {
                        fileSizeReceived = Math.Round((double)e.BytesReceived / 1048576, 2).ToString();
                    }
                    catch (Exception) { }
                    try
                    {
                        fileSizeTotal = Math.Round((double)e.TotalBytesToReceive / 1048576, 2).ToString();
                        if (fileSizeTotal == "0")
                        {
                            fileSizeTotal = "--";
                            if (updateThisProgressBar.Style == ProgressBarStyle.Blocks)
                            {
                                updateThisProgressBar.Style = ProgressBarStyle.Marquee;
                            }
                        }
                        else
                        {
                            if (updateThisProgressBar.Style == ProgressBarStyle.Marquee)
                            {
                                updateThisProgressBar.Style = ProgressBarStyle.Blocks;
                            }
                        }
                    }
                    catch (Exception) { }
                    string speedText = e.ProgressPercentage.ToString() + "%" + " |  " + fileSizeReceived + " MB / " + fileSizeTotal + " MB";
                    updateThisProgressText.Text = speedText;
                    updateThisProgressBar.Value = (int)e.ProgressPercentage;
                };
                wc.DownloadFileCompleted += (object sender, AsyncCompletedEventArgs e) =>
                {
                    if (updateThisProgressBar.Style == ProgressBarStyle.Marquee)
                    {
                        updateThisProgressBar.Style = ProgressBarStyle.Blocks;
                    }
                    if (e.Error != null)
                    {
                        if (nowUpdate == 0) return;
                        DialogResult error = MessageBox.Show(e.Error.Message + "\r\n\r\n" + url, "下载失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (error == DialogResult.Retry)
                        {
                            updateLog.Text += "下载失败，重试下载：" + url + "\r\n";
                            startUpdateDownload(url, file, cdnc.getHeaders(updateServer.SelectedIndex));
                        }
                        else
                        {
                            updateLog.Text += "下载失败：" + url + "\r\n";
                            endUpdateAction();
                            return;
                        }
                    }
                    if (!e.Cancelled)
                    {
                        updateFlag = true;
                    }
                    else
                    {
                        DialogResult error = MessageBox.Show(e.Error.Message + "\r\n\r\n" + url, "下载过程中出错", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (error == DialogResult.Retry)
                        {
                            updateLog.Text += "下载过程中出错，重试下载：" + url + "\r\n";
                            startUpdateDownload(url, file, cdnc.getHeaders(updateServer.SelectedIndex));
                        }
                        else
                        {
                            updateLog.Text += "下载过程中出错并取消重试：" + url + "\r\n";
                            return;
                        }
                    }
                };

                if (url.Length > 80)
                {
                    url = url.Substring(0, 80);
                }
                updateAction.Text = "正在下载：" + url;
                updateLog.Text += updateAction.Text + "\r\n";

                while (wc.IsBusy)
                {
                    Thread.Sleep(x.sleep);
                    if (nowUpdate == 0)
                    {
                        wc.CancelAsync();
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                DialogResult error = MessageBox.Show(ex.Message, "下载失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (error == DialogResult.Retry)
                {
                    startUpdateDownload(url, file, cdnc.getHeaders(updateServer.SelectedIndex));
                }
                else
                {
                    return;
                }
            }
        }

        private void playerRun_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(x.mcLibPath))
            {
                if (MessageBox.Show("你尚未安装 Minecraft，无法进行游戏，是否要现在安装？\r\n你可以稍候在 检查更新 页面安装", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    mainTabControl.SelectedIndex = 1;
                    doCheckUpdate();
                    return;
                }
                else
                {
                    return;
                }
            }
            /*
            if (string.IsNullOrEmpty(playerName.Text))
            {
                error("游戏昵称不能为空", "启动失败");
                return;
            }
            if (!File.Exists(playerJRE.Text + "\\javaw.exe") || !File.Exists(playerJRE.Text + "\\java.exe"))
            {
                error("无法在指定的JRE目录找到 javaw.exe 和 java.exe\r\n请确保选择的JRE安装目录正确", "启动失败");
                return;
            }
             * */
            //try
            //{
            Process ps = new Process();
            /*
            if (playerMode1.Checked)
            {
                ps.StartInfo.FileName = "javaw.exe";
                ps.StartInfo.Arguments = "";
            }
            else if (playerMode2.Checked)
            {
                ps.StartInfo.FileName = "java.exe";
                ps.StartInfo.Arguments = "";
            }
            else if (playerMode3.Checked)
            {
                ps.StartInfo.FileName = "cmd.exe";
                ps.StartInfo.Arguments = "/k title "+x.name+"&java.exe ";
            }
            */
            string jarPath = "1.7.10-Forge10.13.4.1564-1.7.10";
            string mcPath = x.path + x.binpath + @"versions\" + jarPath + @"\";
            if (!Directory.Exists(mcPath) || !File.Exists(mcPath + jarPath + ".json"))
            {
                error("缺少必要的文件，请重新更新MC", "启动失败");
                return;
            }
            StreamReader jsr = new StreamReader(mcPath + jarPath + ".json");
            string jsonText = jsr.ReadToEnd().Trim();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.Deserialize<forgeJsonData>(jsonText);
            #region 传入参数P1
            string UUID = FormsAuthentication.HashPasswordForStoringInConfigFile(playerName.Text, "MD5")
                                .ToLower().Insert(8, "-").Insert(13, "-").Insert(18, "-");
            log(UUID);
            var args = json.minecraftArguments
                                  .Replace("${auth_player_name}", playerName.Text)
                                  .Replace("${version_name}", jarPath)
                                  .Replace("${game_directory}", "./.minecraft")
                                  .Replace("${assets_root}", "assets")
                                  .Replace("${assets_index_name}", json.assetIndex)
                                  .Replace("${auth_uuid}", UUID)
                                  .Replace("${user_properties}", "{}")
                                  .Replace("${user_type}", "Legacy");
            ps.StartInfo.Arguments += " -Xmx" + playerMem.Text + "M";
            ps.StartInfo.Arguments += " -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true ";
            #endregion
            var libdir = x.path + x.binpath + @"\libraries";
            ps.StartInfo.Arguments += "-Djava.library.path=\"" + x.path + x.binpath + "versions\\" + jarPath + "\\" + jarPath + "-natives" + "\" ";
            ps.StartInfo.Arguments += "-cp \"";
            if (!string.IsNullOrEmpty(json.inheritsFrom))
            {
                ps.StartInfo.Arguments += parseInheritsFrom(json.inheritsFrom, x.path + x.binpath + @"versions\", libdir);
            }
            foreach (Dictionary<string, object> lib in json.libraries)
            {
                ps.StartInfo.Arguments += getMcLibInfo(lib["name"].ToString(), libdir);
            }
            if (string.IsNullOrEmpty(json.jar))
            {
                ps.StartInfo.Arguments += mcPath + jarPath + ".jar";
            }
            else
            {
                ps.StartInfo.Arguments += x.path + x.binpath + @"versions\" + json.jar + "\\" + json.jar + ".jar";
            }
            ps.StartInfo.Arguments += "\" " + json.mainClass + " " + args + " ";
            ps.StartInfo.Arguments += "--height " + playerHeight.Text + " --width " + playerWidth.Text + " ";
            ps.StartInfo.Arguments += playerArgs.Text;
            ps.StartInfo.FileName = playerJRE.Text + @"\bin\java.exe";
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.CreateNoWindow = true;
            new debugMode(ps).Show();
            if (playerClose.Checked)
            {
                Application.Exit();
            }
            //}
            //catch (Exception ex)
            //{
            //    error(ex.Message,"启动失败");
            //    return;
            //}
        }

        private string parseInheritsFrom(string v, string mcPath, string libdir)
        {
            string r = "";
            StreamReader jsr = new StreamReader(mcPath + v + "\\" + v + ".json");
            string jsonText = jsr.ReadToEnd().Trim();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.Deserialize<forgeJsonData>(jsonText);
            if (!string.IsNullOrEmpty(json.inheritsFrom))
            {
                r += parseInheritsFrom(json.inheritsFrom, mcPath, libdir);
            }
            foreach (Dictionary<string, object> lib in json.libraries)
            {
                r += getMcLibInfo(lib["name"].ToString(), libdir);
            }
            return r;
        }

        private string getMcLibInfo(string v, string libpath)
        {
            string[] s = v.Split(':');
            string path = libpath + "\\" + s[0].Replace('.', '\\') + "\\";
            path += s[1] + "\\";
            path += s[2] + "\\";
            path += s[1] + "-";
            path += s[2] + ".jar;";
            if (s[0] == "tv.twitch")
            {
                path.Replace("${arch}", Environment.Is64BitOperatingSystem ? "64" : "32");
            }
            return path;
        }

        private void playerSave_Click(object sender, EventArgs e)
        {
            conn.pset("name", playerName.Text);
            conn.pset("mem", playerMem.Text);
            conn.pset("width", playerWidth.Text);
            conn.pset("height", playerHeight.Text);
            conn.pset("jre", playerJRE.Text);
            conn.pset("args", playerArgs.Text);
            if (playerFS.Checked)
            {
                conn.pset("fs", "1");
            }
            else
            {
                conn.pset("fs", "0");
            }
            if (debugMode.Checked)
            {
                conn.pset("debug", "1");
            }
            else
            {
                conn.pset("debug", "0");
            }

            if (playerClose.Checked)
            {
                conn.pset("close", "1");
            }
            else
            {
                conn.pset("close", "0");
            }
            log("设置保存成功");
        }

        private void playerJREBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdd = new FolderBrowserDialog();
            fdd.Description = "选取 Java Runtime (JRE) 安装目录 \r\n64位用户请优先选择64位JRE，否则无法使用超过2G的内存";
            fdd.ShowNewFolderButton = false;
            if (fdd.ShowDialog() == DialogResult.OK)
            {
                playerJRE.Text = fdd.SelectedPath;
            }
        }

        void loadPlayerSettings()
        {
            playerName.Text = conn.pget("name");
            playerArgs.Text = conn.pget("args");
            playerMem.Text = conn.pget("mem");
            playerWidth.Text = conn.pget("width");
            playerHeight.Text = conn.pget("height");
            playerJRE.Text = conn.pget("jre");
            if (conn.pget("fs") == "1")
            {
                playerFS.Checked = true;
            }
            if (conn.pget("debug") == "1")
            {
                debugMode.Checked = true;
            }
            if (conn.pget("fs") == "1")
            {
                playerFS.Checked = true;
            }
            if (string.IsNullOrEmpty(playerJRE.Text))
            {
                playerJRE.Text = getJavaPath();
            }
        }

        public String getJavaPath()
        {
            try
            {
                String javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment";
                using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(javaKey))
                {
                    String currentVersion = baseKey.GetValue("CurrentVersion").ToString();
                    using (var homeKey = baseKey.OpenSubKey(currentVersion))
                        return homeKey.GetValue("JavaHome").ToString();
                }
            }
            catch (Exception ex)
            {
                log("获取JRE路径失败：" + ex.Message);
                return "";
            }
        }

        #region 单机MOD管理器
        public void getModList()
        {
            string black = "CodeChickenLib";
            if (!Directory.Exists(x.path + x.moddir))
            {
                return;
            }
            try
            {
                offlineModList.Items.Clear();
                foreach (string mod in Directory.GetFiles(x.path + x.moddir, "*", SearchOption.AllDirectories))
                {
                    FileInfo fileInfo = new FileInfo(mod);
                    if (fileInfo.Name.Length < black.Length || fileInfo.Name.Substring(0, black.Length) != black)
                    {
                        if (fileInfo.Extension == ".jar" || fileInfo.Extension == ".zip")
                        {
                            int key = offlineModList.Items.Add(fileInfo.Name);
                            offlineModList.SetItemChecked(key, true);
                        }
                        else if (fileInfo.Extension == ".disabled")
                        {
                            string fileName = fileInfo.Name;
                            int key = offlineModList.Items.Add(fileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log("列出Mod失败：" + ex.ToString());
                if (MessageBox.Show(ex.Message, "列出Mod失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    getModList();
                }
            }
        }

        private void offlineModList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void refreshMods_Click(object sender, EventArgs e)
        {
            getModList();
        }

        private void saveOfflineMod_Click(object sender, EventArgs e)
        {
            try
            {
                int key = 0;
                foreach (string mod in offlineModList.Items)
                {
                    FileInfo fileInfo = new FileInfo(x.path + x.moddir + mod);
                    if (offlineModList.GetItemChecked(key) && fileInfo.Extension == ".disabled")
                    {
                        File.Move(fileInfo.FullName, x.path + x.moddir + mod.Substring(0, fileInfo.Name.LastIndexOf(fileInfo.Extension)));
                    }
                    else if (!offlineModList.GetItemChecked(key) && fileInfo.Extension != ".disabled")
                    {
                        File.Move(x.path + x.moddir + mod, x.path + x.moddir + mod + ".disabled");
                    }
                    key++;
                }
                getModList();
                log("已更改可选Mod");
            }
            catch (Exception ex)
            {
                error("激活或禁用Mod失败：" + ex.Message + "\r\n请确保你已经关闭了 MineCraft", "激活或禁用Mod失败");
            }
        }
        #endregion

        #region 备份和恢复键位
        private void bakMcOpt_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string file in x.bakList)
                {
                    if (File.Exists(x.path + x.binpath + file))
                    {
                        if (File.Exists(x.path + x.binpath + file))
                        {
                            File.Delete(x.path + x.updpath + "backup." + file);
                        }
                        File.Copy(x.path + x.binpath + file, x.path + x.updpath + "backup." + file);
                    }
                }
                success("已成功备份键位和服务器列表数据");
            }
            catch (Exception ex)
            {
                error(ex.Message, "备份失败");
            }
        }

        private void recMcOpt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要恢复以前备份的设置吗？\r\n这将覆盖现有的键位、服务器列表等数据", "确实要恢复设置吗", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    foreach (string file in x.bakList)
                    {
                        if (File.Exists(x.path + x.updpath + "backup." + file))
                        {
                            if (File.Exists(x.path + x.binpath + file))
                            {
                                File.Delete(x.path + x.binpath + file);
                            }
                            File.Copy(x.path + x.updpath + "backup." + file, x.path + x.binpath + file);
                        }
                    }
                    success("已成功恢复键位和服务器列表数据\r\n重新启动游戏后即可生效");
                }
                catch (Exception ex)
                {
                    error(ex.Message, "恢复失败");
                }
            }
        }
        #endregion

        #region forge进度条
        private void disableForgeProgress_Click(object sender, EventArgs e)
        {
            var v = x.path + x.binpath + @"\Config\splash.properties";
            if (!File.Exists(v))
            {
                error("你还未安装 " + x.name, "操作失败");
                return;
            }
            if (MessageBox.Show("你确定要关闭启动进度条吗？\r\n当关闭启动进度条后，在启动游戏的过程中，游戏将一直不会响应，你将需要等待很长时间\r\n因此，不建议没有启动崩溃问题的电脑关闭启动进度条", "确实要关闭启动进度条吗", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string text = File.ReadAllText(v);
                    File.WriteAllText(v, text.Replace("enabled=true", "enabled=false"));
                    log("已成功关闭启动进度条");
                    success("已成功关闭启动进度条", "操作成功");
                }
                catch (Exception ex)
                {
                    error(ex.Message);
                    return;
                }
            }
        }

        private void enableForgeProgress_Click(object sender, EventArgs e)
        {
            var v = x.path + x.binpath + @"\Config\splash.properties";
            if (!File.Exists(v))
            {
                error("你还未安装 " + x.name);
                return;
            }
            if (MessageBox.Show("你确定要恢复启动进度条吗？\r\n当恢复启动进度条后，如果你的电脑有问题，你将不能启动游戏", "确实要恢复启动进度条吗", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string text = File.ReadAllText(v);
                    File.WriteAllText(v, text.Replace("enabled=false", "enabled=true"));
                    log("已成功恢复启动进度条");
                    success("已成功恢复启动进度条", "操作成功");
                }
                catch (Exception ex)
                {
                    error(ex.Message, "操作失败");
                    return;
                }
            }
        }
        #endregion

        private void installPatch_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确实要安装低配补丁吗？\r\n安装低配补丁后，FPS(帧率)将会大幅提升，但是画质将会降低、大量特效将被关闭\r\n低配补丁还需要Optifine MOD支持（默认已开启）\r\n安装前请先备份游戏内设置。\r\n如果安装后FPS仍然很低，请尝试调高游戏内存上限", "确实要安装低配补丁吗", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                #region 低配补丁
                XmlDocument xml;
                XmlElement root;
                XmlElement ofnode;
                XmlNodeList of;
                XmlElement opnode;
                XmlNodeList op;
                try
                {
                    xml = new XmlDocument();
                    xml.LoadXml(File.ReadAllText(x.path + x.updpath + "mcpatch.xml"));
                    root = xml.DocumentElement;
                    //optionof.txt
                    ofnode = (XmlElement)root.SelectSingleNode("optionof");
                    of = ofnode.ChildNodes;
                    //option.txt
                    opnode = (XmlElement)root.SelectSingleNode("option");
                    op = opnode.ChildNodes;
                }
                catch (Exception ex)
                {
                    error(ex.Message, "加载低配补丁失败");
                    return;
                }
                var opfile = x.path + x.binpath + @"options.txt";
                var offile = x.path + x.binpath + @"optionsof.txt";
                var encode = Encoding.GetEncoding("gb2312");
                Regex regex;
                if (!File.Exists(opfile))
                {
                    error("你还未安装 " + x.name);
                    return;
                }
                try
                {
                    var opdata = File.ReadAllText(opfile, encode);
                    foreach (XmlNode od in op)
                    {
                        regex = new Regex(od.Name + @"\:" + "(.*)");
                        opdata = regex.Replace(opdata, od.Name + ":" + od.InnerText);
                    }
                    File.WriteAllText(opfile, opdata, encode);
                    if (File.Exists(offile))
                    {
                        var ofdata = File.ReadAllText(offile, encode);
                        foreach (XmlNode fd in of)
                        {
                            regex = new Regex(fd.Name + @"\:" + "(.*)");
                            ofdata = regex.Replace(ofdata, fd.Name + ":" + fd.InnerText);
                        }
                        File.WriteAllText(offile, ofdata, encode);
                    }
                }
                catch (Exception ex)
                {
                    error(ex.Message, "在应用低配补丁时出错");
                    return;
                }
                #endregion
                success("已安装低配补丁。重启游戏后生效\r\n如果安装后FPS仍然很低，请尝试调高游戏内存上限");
            }
        }
    }
}
