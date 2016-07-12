using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Threading;

namespace MoecraftPkgInstaller
{
    public partial class main : Form
    {
        public const string startString = "------START-MOECRAFT-PKGINSTALLER-DATA------";
        public const string endString = "------END-MOECRAFT-PKGINSTALLER-DATA------";
        public main()
        {
            InitializeComponent();
            pkgPath.Text = Program.path;
            Thread th = new Thread(() =>
            {
                //try
                //{
                    var fs = new FileStream(Program.path, FileMode.Open);
                    var fn = new FileInfo(Program.path);
                    int fsize = (int)fn.Length;
                    Console.WriteLine(fsize);
                    byte[] bytes = new byte[4194304]; //存储读取结果  
                Console.WriteLine(bytes.Length);
                Console.WriteLine(fsize - bytes.Length);
                fs.Seek(fsize - bytes.Length, SeekOrigin.Begin);
                    fs.Read(bytes, 0, 4194304);
                Console.Write(bytes);
                    string text = Encoding.UTF8.GetString(bytes);
                    int start = text.IndexOf(startString);
                    int end = text.IndexOf(endString);
                    string data = text.Substring(start + startString.Length, end);
                    error(data);
                    if (string.IsNullOrEmpty(data))
                    {
                        error("找不到包自述，因此无法安装该包", "解析包失败");
                        Environment.Exit(2);
                    }
                    else
                    {
                        data = data.Remove(data.IndexOf(endString));
                        try
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            var json = serializer.Deserialize<pkgJsonData>(data);
                            fs.Close();
                            text = null;
                            bytes = null;
                            setName(json.name);
                            setVer(json.ver.ToString());
                            setDesc(json.desc);
                            setInstallPath(json.path);
                            setFinish();
                        }
                        catch (Exception ex)
                        {
                            error(ex.Message, "解析包信息失败");
                            Environment.Exit(3);
                        }
                    }
                //}
                //catch (Exception ex)
                //{
                //    error(ex.Message, "打开文件失败");
                //    Environment.Exit(1);
                //}
            });
            th.Start();
        }

        public delegate void pkgNameInvoke(string v);
        public void setName(string v)
        {
            if (pkgName.InvokeRequired)
            {
                var li = new pkgNameInvoke(setName);
                Invoke(li, v);
            }
            else
            {
                pkgName.Text = v;
            }
        }

        public delegate void pkgVerInvoke(string v);
        public void setVer(string v)
        {
            if (pkgName.InvokeRequired)
            {
                var li = new pkgVerInvoke(setVer);
                Invoke(li, v);
            }
            else
            {
                pkgVer.Text = v;
            }
        }

        public delegate void pkgDescInvoke(string v);
        public void setDesc(string v)
        {
            if (pkgDesc.InvokeRequired)
            {
                var li = new pkgDescInvoke(setDesc);
                Invoke(li, v);
            }
            else
            {
                pkgDesc.Text = v;
            }
        }

        public delegate void pkgInstallPathInvoke(string v);
        public void setInstallPath(string v)
        {
            if (installPath.InvokeRequired)
            {
                var li = new pkgInstallPathInvoke(setInstallPath);
                Invoke(li, v);
            }
            else
            {
                installPath.Text = v;
            }
        }

        public delegate void progressInvoke(int v);
        public void setprogress(int v)
        {
            if (progress.InvokeRequired)
            {
                var li = new progressInvoke(setprogress);
                Invoke(li, v);
            }
            else
            {
                progress.Value = v;
            }
        }

        public delegate void progressMaxInvoke(int v);
        public void setMaxProgress(int v)
        {
            if (progress.InvokeRequired)
            {
                var li = new progressMaxInvoke(setMaxProgress);
                Invoke(li, v);
            }
            else
            {
                progress.Maximum = v;
            }
        }

        public delegate void setFinishInvoke();
        public void setFinish()
        {
            if (progress.InvokeRequired)
            {
                var li = new setFinishInvoke(setFinish);
                Invoke(li);
            }
            else
            {
                installButton.Enabled = true;
                loadingLabel.Visible = false;
                progress.Visible = false;
                tipLabel.Visible = true;
            }
        }

        public void error(string msg, string title = "错误")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void selectInstallDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dg = new FolderBrowserDialog();
            dg.Description = "请选择要将这个包安装到哪里";
            if (dg.ShowDialog() == DialogResult.OK)
            {
                installPath.Text = dg.SelectedPath;
            }
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
