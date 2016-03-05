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
        public const string startString = "%*$@#!";
        public const string endString = "!#@$*%";
        public main()
        {
            InitializeComponent();
            pkgPath.Text = Program.path;
            Thread th = new Thread(() =>
            {
                try
                {
                    var fs = new FileStream(Program.path, FileMode.Open);
                    //尚未读取的文件内容长度  
                    long left = fs.Length;
                    setMaxProgress((int)left);
                    //存储读取结果  
                    byte[] bytes = new byte[1048576];
                    //每次读取长度  
                    int maxLength = bytes.Length;
                    //读取位置  
                    int start = 0;
                    //实际返回结果长度  
                    int num = 0;
                    //当文件未读取长度大于0时，不断进行读取  
                    string text;
                    int sp;
                    int ep;
                    string data = "";
                    while (left > 0)
                    {
                        setprogress(Math.Abs((int)(left - fs.Length)));
                        fs.Position = start;
                        num = 0;
                        if (left < maxLength)
                            num = fs.Read(bytes, 0, Convert.ToInt32(left));
                        else
                            num = fs.Read(bytes, 0, maxLength);
                        if (num == 0)
                            break;
                        start += num;
                        left -= num;
                        text = Encoding.UTF8.GetString(bytes);
                        sp = text.IndexOf(startString); //开始字符串
                        ep = text.IndexOf(endString); //结束字符串
                        if (sp != -1 && ep != -1)
                        {
                            data = text.Substring(sp + startString.Length);
                            break;
                        }
                        else if (sp != -1 && ep == -1)
                        {
                            data = text.Substring(sp + startString.Length);
                            break;
                        }
                        else if (sp == -1 && ep != -1)
                        {
                            data = text.Substring(0, ep - endString.Length);
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(data))
                    {
                        error("找不到包自述，因此无法安装该包", "解析包失败");
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
                            error(ex.ToString(), "解析包信息失败");
                        }
                    }
                }
                catch (Exception ex)
                {
                    error(ex.ToString(), "打开文件失败");
                }
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
    }
}
