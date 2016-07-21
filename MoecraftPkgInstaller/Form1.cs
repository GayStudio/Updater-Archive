using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Threading;
using System.Diagnostics;

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
                try
                {
                    var fn = new FileInfo(Program.path);
                    var fs = new FileStream(Program.path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    int fsize = (int)fn.Length;
                    byte[] bytes = new byte[4194304]; //存储读取结果  
                    fs.Seek(fsize - bytes.Length, SeekOrigin.Begin);
                    fs.Read(bytes, 0, 4194304);
                    int start = bytesIndexOf(bytes, 0, startString);
                    string data = convertBytesToSting(bytes,start).Substring(startString.Length);
                    data = data.Substring(0, data.Length - endString.Length);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    var json = serializer.Deserialize<pkgJsonData>(data);
                    fs.Close();
                    setName(json.name);
                    setVer(json.ver.ToString());
                    setDesc(json.desc);
                    setInstallPath(json.path);
                    setUnpack(json.unpack);
                    if(!string.IsNullOrEmpty(json.script))
                    {
                        string batTemp = Path.GetTempFileName();
                        File.WriteAllText(batTemp, json.script);
                        setBat(batTemp);
                    }
                    setFinish();
                }
                catch (Exception ex)
                {
                    error(ex.ToString(), "解析包信息失败");
                    Environment.Exit(4);
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

        public delegate void pkgBatInvoke(string v);
        public void setBat(string v)
        {
            if (pkgBat.InvokeRequired)
            {
                var li = new pkgBatInvoke(setBat);
                Invoke(li, v);
            }
            else
            {
                pkgBat.Text = v;
            }
        }

        public delegate void pkgUnpackInvoke(bool v);
        public void setUnpack(bool v)
        {
            if (pkgUnpack.InvokeRequired)
            {
                var li = new pkgUnpackInvoke(setUnpack);
                Invoke(li, v);
            }
            else
            {
                pkgUnpack.Checked = v;
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
            Process ps = new Process();
            ps.StartInfo.FileName = Program.updater;
            ps.StartInfo.Arguments = "pkginstall";
            ps.StartInfo.Arguments += " -name=" + pkgName.Text;
            ps.StartInfo.Arguments += " -to=\"" + installPath.Text + "\"";
            ps.StartInfo.Arguments += " -desc=\"" + pkgDesc.Text + "\"";
            ps.StartInfo.Arguments += " -ver=" + pkgVer.Text;
            ps.StartInfo.Arguments += " -path=\"" + pkgPath.Text + "\"";
            if (pkgUnpack.Checked)
            {
                ps.StartInfo.Arguments += " -unpack";
            }
            if (!string.IsNullOrEmpty(pkgBat.Text))
            {
                ps.StartInfo.Arguments += " -batpath=\"" + pkgBat.Text + "\"";
            }
            ps.Start();
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

        /// <summary>
        /// 查找字节数组中字数组的位置
        /// </summary>
        /// <param name="src"></param>
        /// <param name="offset"></param>
        /// <param name="needFind"></param>
        /// <returns></returns>
        public static int bytesIndexOf(byte[] src, int offset, byte[] needFind)
        {
            for (int i = offset; i < src.Length - offset - needFind.Length; i++)
            {
                bool isValid=true;
                for (int j = 0; j < needFind.Length; j++)
                {
                    if (src[i + j] != needFind[j])
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int bytesIndexOf(byte[] src, int offset, string needFind)
        {
            return bytesIndexOf(src,offset, Encoding.UTF8.GetBytes(needFind));
        }

        public static string convertBytesToSting(byte[] src, int offset = 0)
        {
            string r = "";
            for (int i = offset; i < src.Length; i++)
            {
                r += Convert.ToString(Char.ConvertFromUtf32(src[i]));
            }
            return r;
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(pkgBat.Text))
            {
                File.Delete(pkgBat.Text);
            }
        }
    }
}
