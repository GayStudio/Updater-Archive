using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MCUpdater
{
    public partial class Form1 : Form
    {
        private a conn;
        private WebClient w;
        private bool updateFlag = false;
        public Form1()
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
            if (!Directory.Exists(x.path + x.dlpath))
            {
                Directory.CreateDirectory(x.path + x.dlpath);
                log("初始化：创建目录：" + x.path + x.dlpath);
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
            if (!Directory.Exists(x.mcLibPath))
            {
                if (MessageBox.Show("你尚未安装 "+x.name+"，无法进行游戏，是否要现在安装？\r\n你可以稍候在 检查更新 页面安装", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    doUpdate();
                }
            }
            log("启动成功");
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
        public void error(string msg , string title = "错误")
        {
            log(title + "：" +msg.Replace("\r\n"," "));
            MessageBox.Show(msg , title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void getModList()
        {
            string black = "CodeChickenLib";
            if(!Directory.Exists(x.path + x.moddir)) {
                return;
            }
            try
            {
                offlineModList.Items.Clear();
                foreach (string mod in Directory.GetFiles(x.path + x.moddir, "*", SearchOption.AllDirectories))
                {
                    FileInfo fileInfo = new FileInfo(mod);
                    if(fileInfo.Name.Substring(0,black.Length) != black) {
                        if (fileInfo.Extension == ".jar" || fileInfo.Extension == ".zip")
                        {
                            int key = offlineModList.Items.Add(fileInfo.Name);
                            offlineModList.SetItemChecked(key , true);
                        } else if(fileInfo.Extension == ".disabled") {
                            string fileName = fileInfo.Name;
                            int key = offlineModList.Items.Add(fileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log("列出Mod失败：" + ex.Message);
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
                foreach(string mod in offlineModList.Items) {
                    FileInfo fileInfo = new FileInfo(x.path + x.moddir + mod);
                    if(offlineModList.GetItemChecked(key) && fileInfo.Extension == ".disabled") {
                        File.Move(fileInfo.FullName, x.path + x.moddir + mod.Substring(0, fileInfo.Name.LastIndexOf(fileInfo.Extension)));
                    }
                    else if(!offlineModList.GetItemChecked(key) && fileInfo.Extension != ".disabled")
                    {
                        File.Move(x.path + x.moddir + mod, x.path + x.moddir + mod + ".disabled");
                    }
                    key++;
                }
                getModList();
            }
            catch(Exception ex)
            {
                error("激活或禁用Mod失败：" + ex.Message, "激活或禁用Mod失败");
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            doUpdate();
        }

        void doUpdate()
        {
            updateButton.Enabled = false;
            log("启动检查更新");
            progressGroupBox.Visible = true;
            updateAction.Text = "正在获取更新信息";
            updateLog.AppendText(updateAction.Text + "\r\n");
            w = new WebClient();
            w.DownloadStringCompleted += w_DownloadStringCompleted;
            try
            {
                w.DownloadStringAsync(new Uri(conn.getOpt("updatePath")));
            }
            catch (Exception ex)
            {
                error(ex.Message, "获取更新数据失败");
                return;
            }
        }

        void w_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Result))
            {
                error("服务器未返回数据，请重试操作", "获取更新数据失败");
            }
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(e.Result);
            }
            catch (Exception ex)
            {
                error(ex.Message + "\r\n" + e.Result, "解析XML失败");
                return;
            }
            XmlNode      xn = doc.SelectSingleNode("root");
            XmlNodeList xnl = xn.ChildNodes;
            foreach(XmlNode val in xnl) {
               XmlElement updateList = (XmlElement)val;
               Dictionary<string,string> info = conn.getLib(updateList.GetAttribute("id"));
               updateLog.AppendText("---------------------------------------------------------------------------\r\n检查更新：" + info["desc"] + "\r\n");
               double thisVer = 0.0;
               if (Directory.Exists(x.path + x.binpath + info["path"]))
               {
                   thisVer = double.Parse(info["ver"]);
               }
               if(updateForce.Checked || thisVer < float.Parse(updateList.GetAttribute("ver"))) {
                   updateLog.AppendText("发现更新：" + info["desc"] + "\r\n");
                    updateAction.Text = "正在获取：" + updateList.InnerText;
                    updateLog.AppendText(updateAction.Text + "\r\n");
                    updateFlag = false;
                    string fileName = info["id"] + "_" + Path.GetRandomFileName() + ".tmp";
                    startUpdateDownload(updateList.InnerText, fileName);
                    while(!updateFlag) {
                        Application.DoEvents();
                        Thread.Sleep(20);
                    }
                    updateLog.AppendText("正在安装文件\r\n");
                    Process p = new Process();
                    p.StartInfo.FileName = x.path + x.updpath + "7z.exe";
                    p.StartInfo.Arguments = "x -y -o\"" + x.path + x.binpath +  info["path"] + "\" \"" + x.path + x.updpath + x.dlpath + fileName + "\"";
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
                    }
                    catch (Exception ex)
                    {
                        error(ex.Message,"解包数据失败");
                        return;
                    }
                    p.BeginOutputReadLine();

                    // 为异步获取订阅事件  
                    p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                    conn.setLibVer(updateList.GetAttribute("id") , updateList.GetAttribute("ver"));
               }
               else
               {
                    updateLog.AppendText("已是最新版本：" + info["desc"] + "\r\n");
               }
            }
            updateAction.Text = "检查更新完成";
            updateLog.AppendText("检查更新完成");
            log("检查更新完成");
            updateButton.Enabled = true;
        }


        #region 处理压缩包

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // 这里仅做输出的示例，实际上您可以根据情况取消获取命令行的内容  
            // 参考：process.CancelOutputRead()  

            if (String.IsNullOrEmpty(e.Data) == false)
                this.AppendText(e.Data + "\r\n");
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
                wc.DownloadFileAsync(new Uri(url), x.path +  x.updpath + x.dlpath + file);
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += wc_DownloadFileCompleted;
                if(url.Length > 80) {
                    url = url.Substring(0,80);
                }
                updateAction.Text = "正在下载：" + url;
            }
            catch (Exception ex)
            {
                DialogResult error = MessageBox.Show(ex.Message, "下载失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (error == DialogResult.Retry)
                {
                    startUpdateDownload(url,file);
                }
                else
                {
                    return;
                }
            }
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if(!e.Cancelled) {
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

        private void playerRun_Click(object sender, EventArgs e)
        {
            if(!Directory.Exists(x.mcLibPath)) {
                if (MessageBox.Show("你尚未安装 Minecraft，无法进行游戏，是否要现在安装？\r\n你可以稍候在 检查更新 页面安装", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    mainTabControl.SelectedIndex = 1;
                    doUpdate();
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
                string jarPath  = conn.getOpt("jarPath");
                string mcPath   = x.path + x.binpath + @"versions\" + jarPath + @"\";
                if (!Directory.Exists(mcPath) || !File.Exists(mcPath + jarPath + ".json"))
                {
                    error("缺少必要的文件，请重新更新MC", "启动失败");
                    return;
                }
                StreamReader jsr = new StreamReader(mcPath + jarPath + ".json");
                string  jsonText = jsr.ReadToEnd().Trim();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var json        = serializer.Deserialize<forgeJsonData>(jsonText);
                string UUID = FormsAuthentication.HashPasswordForStoringInConfigFile(playerName.Text, "MD5")
                                .ToLower().Insert(8,"-").Insert(13,"-").Insert(18,"-");
                log(UUID);
                var args = json.minecraftArguments
                                      .Replace("${auth_player_name}", playerName.Text)
                                      .Replace("${version_name}", jarPath)
                                      .Replace("${game_directory}", x.path + x.binpath)
                                      .Replace("${assets_root}", x.path + x.binpath + @"\assets")
                                      .Replace("${assets_index_name}", json.assetIndex)
                                      .Replace("${auth_uuid}", UUID)
                                      .Replace("${user_properties}", "{}")
                                      .Replace("${user_type}", "Legacy");
                ps.StartInfo.Arguments += "-Xincgc -Xmx"+playerMem.Text+"M ";
                var libdir = Directory.GetFiles(x.path + x.binpath + @"\libraries", "*.jar", SearchOption.AllDirectories);
                var cp = "";
            log(json.libraries);
                foreach(string libs in libdir)
                {
                    if(libs.IndexOf(@"libraries\net\minecraftforge\forge") < 0)
                    {
                      cp += libs + ";";
                    }
                }
                cp = cp.Replace(@"\\",@"\").Trim(';');
                log(cp);
                /*
                ps.StartInfo.Arguments += "-Djava.library.path=\"" + x.path + x.binpath + @"bin\natives" + "\" ";
                ps.StartInfo.Arguments += "-cp " + x.path + x.binpath + @"bin\minecraft.jar;";
                ps.StartInfo.Arguments += x.path + x.binpath + @"bin\jinput.jar;";
                ps.StartInfo.Arguments += x.path + x.binpath + @"bin\lwjgl.jar;";
                ps.StartInfo.Arguments += x.path + x.binpath + @"bin\lwjgl_util.jar ";
                ps.StartInfo.Arguments += "";
                ps.StartInfo.Arguments += "";
                */
                ps.StartInfo.Arguments += args + " ";
                ps.StartInfo.Arguments += "--height " + playerHeight.Text + " --width " + playerWidth.Text + " ";
                ps.StartInfo.Arguments += playerArgs.Text;
                if(playerClose.Checked) {
                    Application.Exit();
                }
            //}
            //catch (Exception ex)
            //{
            //    error(ex.Message,"启动失败");
            //    return;
            //}
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

        private void playerSave_Click(object sender, EventArgs e)
        {
            conn.setOpt("playerName", playerName.Text);
            conn.setOpt("playerMem", playerMem.Text);
            conn.setOpt("playerWidth", playerWidth.Text);
            conn.setOpt("playerHeight", playerHeight.Text);
            conn.setOpt("playerJRE", playerJRE.Text);
            conn.setOpt("playerArgs", playerArgs.Text);
            if (playerFS.Checked)
            {
                conn.setOpt("playerFS", "1");
            }
            else
            {
                conn.setOpt("playerFS", "0");
            }
            if (debugMode.Checked)
            {
                conn.setOpt("debugMode", "1");
            }
            else
            {
                conn.setOpt("debugMode", "2");
            }

            if (playerClose.Checked)
            {
                conn.setOpt("playerClose", "1");
            }
            else
            {
                conn.setOpt("playerClose", "0");
            }
            log("设置保存成功");
        }

        private void updateAction_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://zhizhe8.net/");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.stus8.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void launcherButton_Click(object sender, EventArgs e)
        {
            if(!File.Exists(x.path + conn.getOpt("launcherPath"))) {
                MessageBox.Show("未找到启动器！请确保你已经更新启动器到最新版本，且启动器没有改名。请手动打开启动器", "打开启动器失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    System.Diagnostics.Process.Start(x.path + conn.getOpt("launcherPath"));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "打开启动器失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(ex.Message,"清除下载缓存失败",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            log("清除下载缓存成功");
            MessageBox.Show("清除下载缓存成功","清除下载缓存成功",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void regLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://accounts.moecraft.net/index.php?m=home&c=user&a=reg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n请手动打开：https://accounts.moecraft.net", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void mcLauncher_Click(object sender, EventArgs e)
        {

        }
    }
}
