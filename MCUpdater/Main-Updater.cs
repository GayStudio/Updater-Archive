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
        private bool nowUpdate = false;

        private void updateButton_Click(object sender, EventArgs e)
        {
            if(nowUpdate)
            {
                endUpdateAction();
                updateAction.Text = "更新已取消";
                updateLog.AppendText(updateAction.Text);
            }
            else
            {
                doUpdate();
            }
        }

        void doUpdate()
        {
            var cdn = cdnc.get(updateServer.SelectedIndex);
            var server = cdn["url"] + cdn["xml"];
            updateAction.Text = "正在获取更新信息: " + server;
            startUpdateAction();
            updateLog.AppendText(updateAction.Text + "\r\n");
            string errorMsg = "";
            string result = "";
            Thread th = new Thread(() => {
                try
                {
                    var wb = WebRequest.Create(server);
                    var ws = wb.GetResponse();
                    //MessageBox.Show(ws.ContentLength.ToString());
                    result = new StreamReader(ws.GetResponseStream()).ReadToEnd();
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                }
            });
            th.Start();
            while (!th.Join(x.sleep))
            {
                if(!nowUpdate)
                {
                    th.Abort();
                    return;
                }
                Application.DoEvents();
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                error(errorMsg,"获取更新数据失败");
                endUpdateAction();
                return;
            }
            downloadUpdateInfoCompleted(result);
        }

        private void startUpdateAction()
        {
            updateLog.Text = "";
            nowUpdate = true;
            forceUpdateAssets.Enabled = false;
            forceUpdateConfig.Enabled = false;
            forceUpdateCore.Enabled = false;
            forceUpdateMods.Enabled = false;
            forceUpdateOmods.Enabled = false;
            forceUpdateRoot.Enabled = false;
            updateServer.Enabled = false;
            updateButton.Text = "取消更新";
            log("启动检查更新");
        }

        void downloadUpdateInfoCompleted(string Result)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(Result);
            }
            catch (Exception ex)
            {
                error(ex.Message + "\r\n" + Result, "解析XML失败");
                endUpdateAction();
                return;
            }
            XmlNode xn = doc.SelectSingleNode("root");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode val in xnl)
            {
                XmlElement updateList = (XmlElement)val;
                var info = conn.getLib(updateList.GetAttribute("id"));
                updateLog.AppendText("---------------------------------------------------------------------------\r\n检查更新：" + info["desc"] + " [ 本地版本: V" + info["ver"] + " ]\r\n");
                double thisVer = 0.0;
                if (Directory.Exists(x.path + x.binpath + info["path"]))
                {
                    thisVer = double.Parse(info["ver"]);
                }
                if (thisVer < float.Parse(updateList.GetAttribute("ver")) || isForceUpdate(info["id"]))
                {
                    updateLog.AppendText("发现更新：" + info["desc"] + " [ 最新版本: V" + updateList.GetAttribute("ver") + " ]\r\n");
                    string downUrl;
                    if (updateList.InnerText.IndexOf("{{url}}") != -1)
                    {
                        downUrl = updateList.InnerText.Replace("{{url}}", info["url"]);
                    }
                    else
                    {
                        downUrl = updateList.InnerText;
                    }
                    updateAction.Text = "正在获取：" + downUrl;
                    updateLog.AppendText(updateAction.Text + "\r\n");
                    updateFlag = false;
                    string fileName = new Random().Next().ToString() + info["id"] + ".zip";
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    startUpdateDownload(downUrl, fileName);
                    while (!updateFlag)
                    {
                        Application.DoEvents();
                        Thread.Sleep(20);
                    }
                    #region 解包更新文件
                    updateThisProgressText.Text = "安装中";
                    updateThisProgressBar.Value = 0;
                    updateThisProgressBar.Style = ProgressBarStyle.Marquee;
                    updateAction.Text = "正在安装：" + info["desc"];
                    updateLog.AppendText("正在安装文件：" + info["desc"] + "\r\n");
                    Process p = new Process();
                    p.StartInfo.FileName = x.path + x.updpath + "7z.exe";
                    p.StartInfo.Arguments = "x -y -o\"" + x.path + info["path"] + "\" \"" + x.path + x.updpath + x.dlpath + fileName + "\"";
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    try
                    {
                        if (updateList.GetAttribute("clear") == "1" && Directory.Exists(x.path + x.binpath + info["path"]))
                        {
                            string[] clearFileList = Directory.GetFiles(x.path + x.binpath + info["path"]);
                            foreach (string clearFileName in clearFileList)
                            {
                                File.Delete(clearFileName);
                            }
                        }
                        if (updateList.GetAttribute("clear") == "2" && Directory.Exists(x.path + x.binpath + info["path"]))
                        {
                            Directory.Delete(x.path + x.binpath + info["path"], true);
                        }
                        if (!Directory.Exists(x.path + x.binpath + info["path"]))
                        {
                            Directory.CreateDirectory(x.path + x.binpath + info["path"]);
                        }
                        p.Start();
                        p.BeginOutputReadLine();
                        p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                        conn.setLibVer(updateList.GetAttribute("id"), updateList.GetAttribute("ver"));
                        while (!p.WaitForExit(25))
                        {
                            Application.DoEvents();
                        }

                    }
                    catch (Exception ex)
                    {
                        error(ex.Message, "解包数据失败");
                        updateAction.Text = "检查更新失败";
                        updateLog.AppendText("检查更新失败");
                        log("检查更新失败");
                        endUpdateAction();
                        return;
                    }
                    if (!string.IsNullOrEmpty(updateError))
                    {
                        MessageBox.Show("解包数据失败，请重新尝试更新\r\n" + updateError,
                            "更新失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        updateAction.Text = "检查更新失败";
                        updateLog.AppendText("检查更新失败");
                        log("检查更新失败");
                        endUpdateAction();
                        return;
                    }
                    else
                    {
                        updateThisProgressBar.Style = ProgressBarStyle.Blocks;
                        updateLog.AppendText("已成功更新：" + info["desc"] + "\r\n");
                    }
                    #endregion
                }
                else
                {
                    updateLog.AppendText("已是最新版本：" + info["desc"] + "\r\n");
                }
            }
            updateAction.Text = "检查更新完成";
            updateLog.AppendText("检查更新完成");
            log("检查更新完成");
            endUpdateAction();
        }

        private void endUpdateAction()
        {
            nowUpdate = false;
            updateThisProgressBar.Style = ProgressBarStyle.Blocks;
            updateThisProgressBar.Value = 0;
            updateThisProgressText.Text = "";
            forceUpdateAssets.Enabled = true;
            forceUpdateConfig.Enabled = true;
            forceUpdateCore.Enabled = true;
            forceUpdateMods.Enabled = true;
            forceUpdateOmods.Enabled = true;
            forceUpdateRoot.Enabled = true;
            updateServer.Enabled = true;
            updateButton.Text = "检查更新 (&C)";
        }
        #region 判断是否需要强制更新
        private bool isForceUpdate(string v)
        {
            switch (v)
            {
                case "mods":
                    if (forceUpdateMods.Checked)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case "config":
                    if (forceUpdateConfig.Checked)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case "omods":
                    if (forceUpdateOmods.Checked)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case "root":
                    if (forceUpdateRoot.Checked)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case "main":
                    if (forceUpdateCore.Checked)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case "asset":
                    if (forceUpdateAssets.Checked)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    return false;
            }
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
        /// <summary>
        /// 开始下载
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="file">保存文件名</param>
        /// 
        protected void startUpdateDownload(string url, string file)
        {
            updateButton.Enabled = false;
            try
            {

                WebClient wc = new WebClient();
                wc.DownloadFileAsync(new Uri(url), x.path + x.updpath + x.dlpath + file);
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += wc_DownloadFileCompleted;
                if (url.Length > 80)
                {
                    url = url.Substring(0, 80);
                }
                updateAction.Text = "正在下载：" + url;
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

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                error(e.Error.Message, "下载更新数据失败");
                endUpdateAction();
                return;
            }
            if (!e.Cancelled)
            {
                updateFlag = true;
            }
            else
            {
                DialogResult error = MessageBox.Show(e.Error.Message, "下载失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (error == DialogResult.Retry)
                {
                    doUpdate();
                }
                else
                {
                    return;
                }
            }

        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string speedText = e.ProgressPercentage.ToString() + "%";
            updateThisProgressText.Text = speedText;
            updateThisProgressBar.Value = (int)e.ProgressPercentage;
        }
    }
}
