using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;

namespace MCUpdater
{
    public partial class Main : Form
    {
        private int nowUpdate = 0;
        private XmlElement xn;
        public bool updateFlag;
        private void updateButton_Click(object sender, EventArgs e)
        {
            if(nowUpdate == 2)
            {
                endUpdateAction();
                updateAction.Text = "更新已取消";
                updateLog.AppendText(updateAction.Text);
            }
            else if(nowUpdate == 1)
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
                error(errorMsg,"获取更新数据失败");
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
                if(!platformShouldSkip(val.GetAttribute("platform")))
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
            if(xn == null)
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
                if(forceUpdate.GetItemChecked(need))
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
                        if(itemsCount <= forceUpdate.Items.Count)
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
                if(!string.IsNullOrEmpty(e.Data))
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
                if (MessageBox.Show("解压缩文件出错！退出码：" + p.ExitCode + "\r\n信息：" + latest_info + "\r\n错误：" + latest_error +  "\r\n\r\n你想要重试解压缩吗？","解压缩文件出错", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
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
                                .Replace("{{Path}}", x.path + installPath + "\\")).Replace("\n","\r\n");
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
                    string av = value.Substring(0,1);
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
                if(exit)
                {
                    //fucking exit bug
                    Process.Start("taskkill", "/f " + Process.GetCurrentProcess().Id);
                    Environment.Exit(0);
                } else
                {
                    updateLog.AppendText("\r\n" + updateAction.Text + "\r\n您可以继续使用 " + x.pname + "，或者关闭它\r\n");
                }
            }
            catch (Exception ex)
            {
                error(ex.ToString(),"独立包安装程序无法安装此包");
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
        protected void startUpdateDownload(string url, string file, Dictionary<string,string> headers = null)
        {
            try
            {
                WebClient wc = new WebClient();
                if(headers != null)
                {
                    foreach(KeyValuePair<string, string> xhead in headers)
                    {
                        wc.Headers.Set(xhead.Key, xhead.Value);
                    }
                }
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                wc.DownloadFileAsync(new Uri(url), x.path + x.updpath + x.dlpath + file);
                string fileSizeTotal     = "--";
                string fileSizeReceived = "--";
                wc.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) =>
                {
                    try
                    {
                        fileSizeReceived = Math.Round((double)e.BytesReceived / 1048576,2).ToString();
                    }
                    catch (Exception) { }
                    try
                    {
                        fileSizeTotal = Math.Round((double)e.TotalBytesToReceive / 1048576,2).ToString();
                        if(fileSizeTotal == "0")
                        {
                            fileSizeTotal = "--";
                            if(updateThisProgressBar.Style == ProgressBarStyle.Blocks)
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
                    string speedText = e.ProgressPercentage.ToString() + "%" + " |  " + fileSizeReceived + " MB / "+ fileSizeTotal +" MB";
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
                    if(nowUpdate == 0)
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
        /*
        protected void startUpdateDownload(string url, string file)
        {
            string path = x.path + x.updpath + "downloads\\" + file;
            string errorMsg = "";
            long get = 0;
            Thread th = new Thread(() => {
                try
                {
                    int bufferLength = 0;
                    //File IO
                    FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    //Web Request
                    var wb = WebRequest.Create(url);
                    var ws = wb.GetResponse();
                    get = ws.ContentLength;
                    var wr = ws.GetResponseStream();
                    byte[] buffer = new byte[10240];
                    while(bufferLength > 0)
                    {
                        bufferLength = wr.Read(buffer, 0, buffer.Length);
                        get = get + buffer.Length;
                        fs.Write(buffer, 0, bufferLength);
                    }
                }
                catch (Exception ex)
                {
                    errorMsg = ex.ToString();
                }
            });
            th.Start();
            while (!th.Join(x.sleep))
            {
                if (!nowUpdate)
                {
                    th.Abort();
                    return;
                }
                updateThisProgressText.Text = (get / 1024)  + " KB";
                Application.DoEvents();
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                error(errorMsg, "获取更新数据失败");
                endUpdateAction();
                return;
            }
        }*/
    }
}
