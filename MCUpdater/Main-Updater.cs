﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.IO.Compression;
using System.Collections.Generic;

namespace MCUpdater
{
    public partial class Main : Form
    {
        private int nowUpdate = 0;
        private XmlElement xn;
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
            /*
            Thread th = new Thread(() => {
                try
                {
                    var wb = new WebClient();
                    result = wb.DownloadString(server);
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                }
            });
            th.Start();
            while (!th.Join(x.sleep))
            {
                if(nowUpdate == 2)
                {
                    th.Abort();
                    return;
                }
                Application.DoEvents();
            }*/
            startUpdateDownload(server, "update.xml");
            result = File.ReadAllText(x.path + x.updpath + x.dlpath + "update.xml");
            if (!string.IsNullOrEmpty(errorMsg))
            {
                error(errorMsg,"获取更新数据失败");
                endUpdateAction();
                return;
            }
            updateAction.Text = "获取本版更新信息成功。点击开始更新按钮可以运行更新。你可以在右侧选择要强制更新哪些组件。";
            readUpdateInfo(result);
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
                updateLog.Text  = xn.SelectSingleNode("desc").InnerText;
                updateLog.Text += "\r\n---------------------------------------------------------------------------\r\n";
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
                i++;
            }
            updateLog.Text += "\r\n在右侧选择你想要更新的内容（已自动勾选可以更新的内容），然后点击 确认更新\r\n";
            forceUpdate.Enabled = true;
            updateServer.Enabled = true;
            updateButton.Text = "确认更新 (&C)";
            nowUpdate = 1;
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
            for (int need = 0; need < forceUpdate.Items.Count; need++)
            {
                if(forceUpdate.GetItemChecked(need))
                {
                    bool updateFatalError = false;
                    XmlElement var  = (XmlElement)xn.SelectSingleNode("libs").ChildNodes[need];
                    updateLog.Text += "正在更新：" + var.GetAttribute("name") + " >> " + var.GetAttribute("ver") + "\r\n";
                    if (!Directory.Exists(x.path + var.GetAttribute("path")))
                    {
                        Directory.CreateDirectory(x.path + var.GetAttribute("path"));
                    }
                    XmlElement down = (XmlElement)var.SelectSingleNode("download");
                    foreach (XmlElement downvar in down)
                    {
                        startUpdateDownload((downvar.InnerText).Replace("{{url}}", cdnc.get(updateServer.SelectedIndex)["url"]), downvar.GetAttribute("name"));
                        if(!File.Exists(x.path + x.updpath + x.dlpath + downvar.GetAttribute("name")) || (new FileInfo(x.path + x.updpath + x.dlpath + downvar.GetAttribute("name"))).Length <= 0)
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
                                    updateAction.Text = "正在解压缩文件：" + packageName;
                                    updateLog.AppendText(updateAction.Text + "\r\n");
                                    Process p = new Process();
                                    p.StartInfo.FileName = x.path + x.updpath + "7z.exe";
                                    p.StartInfo.Arguments = "x -y -o\"" + x.path + var.GetAttribute("path") + "\" \"" + x.path + x.updpath + x.dlpath + packageName + "\"";
                                    p.StartInfo.UseShellExecute = false;
                                    p.StartInfo.CreateNoWindow = true;
                                    p.StartInfo.RedirectStandardOutput = true;
                                    p.Start();
                                    p.BeginOutputReadLine();
                                    p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                                    while (!p.WaitForExit(x.sleep))
                                    {
                                        Application.DoEvents();
                                    }
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
                            updateAction.Text = "运行安装程序";
                            updateLog.AppendText(updateAction.Text + "\r\n");
                            string batPath = x.path + var.GetAttribute("path") + "\\install.cmd";
                            string batText = "@echo off\r\n" + bat.InnerText
                            .Replace("{{7z}}",x.path + x.updpath + "7z.exe")
                            .Replace("{{Root}}", x.path)
                            .Replace("{{DLDir}}", x.path + x.updpath + x.dlpath)
                            .Replace("{{UpdPath}}", x.path + x.updpath)
                            .Replace("{{Path}}", x.path + var.GetAttribute("path") + "\\");
                            File.WriteAllText(batPath, batText);
                            Process pb = new Process();
                            pb.StartInfo.FileName = batPath;
                            pb.StartInfo.UseShellExecute = false;
                            pb.StartInfo.CreateNoWindow = true;
                            pb.StartInfo.RedirectStandardOutput = true;
                            pb.Start();
                            pb.BeginOutputReadLine();
                            pb.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                            while (!pb.WaitForExit(x.sleep))
                            {
                                Application.DoEvents();
                            }
                        }
                        catch (Exception ex)
                        {
                            DialogResult error = MessageBox.Show(ex.Message + "\r\n\r\n" + x.path + var.GetAttribute("path"), "写入安装脚本失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            if (error == DialogResult.Retry)
                            {
                                goto writeBat;
                            } else
                            {
                                updateFatalError = true;
                            }
                        }
                    }
                    writeVersionFile:
                    try
                    {
                        if(!updateFatalError)
                        {
                            File.WriteAllText(x.path + var.GetAttribute("path") + "\\" + var.Name + ".version", var.GetAttribute("ver"));
                        }
                        else
                        {
                            updateLog.Text += "更新失败：" + var.GetAttribute("name") + "\r\n";
                        }
                    }
                    catch (Exception ex)
                    {
                        DialogResult error = MessageBox.Show(ex.Message + "\r\n\r\n" + x.path + var.GetAttribute("path"), "写入版本信息失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (error == DialogResult.Retry)
                        {
                            goto writeVersionFile;
                        } else
                        {
                            updateFatalError = true;
                        }
                    }
                }
            }
            updateAction.Text = "更新完成";
            updateLog.AppendText("******更新完成******\r\n");
            log("更新完成");
            endUpdateAction();
        }

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
        #region 判断是否需要强制更新
        private bool isForceUpdate(string v)
        {
            return false;
        }
        #endregion

        #region 处理压缩包

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // 这里仅做输出的示例，实际上您可以根据情况取消获取命令行的内容  
            // 参考：process.CancelOutputRead()  

            if (string.IsNullOrEmpty(e.Data) == false)
            {
                if (e.Data.IndexOf("Error:") != -1)
                {
                    updateError = e.Data;
                }
                AppendText(e.Data + "\r\n");
            }

        }

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
        /// 
        protected void startUpdateDownload(string url, string file)
        {
            try
            {

                WebClient wc = new WebClient();
                wc.DownloadFileAsync(new Uri(url), x.path + x.updpath + x.dlpath + file);
                wc.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) =>
                {
                    string speedText = e.ProgressPercentage.ToString() + "%";
                    updateThisProgressText.Text = speedText;
                    updateThisProgressBar.Value = (int)e.ProgressPercentage;
                };
                wc.DownloadFileCompleted += (object sender, AsyncCompletedEventArgs e) =>
                {
                    if (e.Error != null)
                    {
                        DialogResult error = MessageBox.Show(e.Error.Message + "\r\n\r\n" + url, "下载失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (error == DialogResult.Retry)
                        {
                            updateLog.Text += "下载失败，重试下载：" + url + "\r\n";
                            startUpdateDownload(url, file);
                        }
                        else
                        {
                            updateLog.Text += "下载失败并取消重试：" + url + "\r\n";
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
                            startUpdateDownload(url, file);
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
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                DialogResult error = MessageBox.Show(ex.Message, "下载失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (error == DialogResult.Retry)
                {
                    startUpdateDownload(url, file);
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
