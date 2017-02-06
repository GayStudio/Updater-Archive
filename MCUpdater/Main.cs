using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using Microsoft.Win32;
using System.Security.Principal;
using System.Reflection;

namespace MCUpdater
{
    public partial class Main : Form
    {
        private config conn;
        private cdn cdnc;
        private string updateError;
        public bool inited = false;
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
            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    log("当前正以系统管理员身份启动 " + x.pname);
                }
                else
                {
                    error("请注意：您并非以系管理员身份运行 " + x.pname + " !\r\n这可能会导致各种问题，强烈建议下次运行本程序右键选择 [以管理员身份运行]");
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.UseShellExecute = true;
                    startInfo.WorkingDirectory = Environment.CurrentDirectory;
                    startInfo.FileName = Application.ExecutablePath;
                    foreach (string a in p.arg)
                    {
                        startInfo.Arguments += a + " ";
                    }
                    //设置启动动作,确保以管理员身份运行
                    startInfo.Verb = "runas";
                    Environment.Exit(254);
                }
            }
            catch (Exception ex)
            {
                error(ex.ToString(), "检查权限失败");
            }
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
                MessageBox.Show("这个功能并没有什么卵用\r\n下次启动更新器后无效", "所谓的帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            openUrl("http://moeclub.net");
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
                if(!Directory.Exists(x.path + x.binpath + "mods\\1.7.10"))
                {
                    Directory.CreateDirectory(x.path + x.binpath + "mods\\1.7.10");
                }
                Process.Start("explorer.exe", "\"" + x.path + x.binpath + "mods\\1.7.10\"");
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
                } else
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
                log("文件下载请求：" + url);
                var moeset = urlobj.GetAttribute("moecraft");
                bool isquiet = false;
                bool ispkginstaller = false;
                bool isopen = false;
                bool isexec = false;
                bool iscontinue = false;
                bool isdownload = true;
                if (!string.IsNullOrEmpty(moeset))
                {
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
            openUrl("https://telegram.me/moecraft");
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

    }
}
