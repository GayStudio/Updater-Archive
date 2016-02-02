using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Security;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections;

namespace MCUpdater
{
    public partial class Main : Form
    {
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
            foreach (Dictionary<string,object> lib in json.libraries)
            {
                ps.StartInfo.Arguments += getMcLibInfo(lib["name"].ToString(), libdir);
            }
            if(string.IsNullOrEmpty(json.jar))
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

        private string getMcLibInfo(string v,string libpath)
        {
            string[] s = v.Split(':');
            string path  = libpath + "\\" + s[0].Replace('.','\\') + "\\";
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
            if(conn.pget("fs") == "1")
            {
                playerFS.Checked = true;
            }
            if(conn.pget("debug") == "1")
            {
                debugMode.Checked = true;
            }
            if(conn.pget("fs") == "1")
            {
                playerFS.Checked = true;
            }
            if(string.IsNullOrEmpty(playerJRE.Text))
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
            catch(Exception ex)
            {
                log("获取JRE路径失败：" + ex.Message);
                return "";
            }
        }
    }
}
