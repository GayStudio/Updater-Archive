using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Security;

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
            string jarPath = conn.get("jarPath");
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
                                  .Replace("${game_directory}", x.path + x.binpath)
                                  .Replace("${assets_root}", x.path + x.binpath + @"\assets")
                                  .Replace("${assets_index_name}", json.assetIndex)
                                  .Replace("${auth_uuid}", UUID)
                                  .Replace("${user_properties}", "{}")
                                  .Replace("${user_type}", "Legacy");
            ps.StartInfo.Arguments += "-Xincgc -Xmx" + playerMem.Text + "M ";
            #endregion
            var libdir = Directory.GetFiles(x.path + x.binpath + @"\libraries", "*.jar", SearchOption.AllDirectories);
            var cp = "";
            //log(json.libraries);
            foreach (string libs in libdir)
            {
                if (libs.IndexOf(@"libraries\net\minecraftforge\forge") < 0)
                {
                    cp += libs + ";";
                }
            }
            cp = cp.Replace(@"\\", @"\").Trim(';');
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

        private void playerSave_Click(object sender, EventArgs e)
        {
            conn.set("playerName", playerName.Text);
            conn.set("playerMem", playerMem.Text);
            conn.set("playerWidth", playerWidth.Text);
            conn.set("playerHeight", playerHeight.Text);
            conn.set("playerJRE", playerJRE.Text);
            conn.set("playerArgs", playerArgs.Text);
            if (playerFS.Checked)
            {
                conn.set("playerFS", "1");
            }
            else
            {
                conn.set("playerFS", "0");
            }
            if (debugMode.Checked)
            {
                conn.set("debugMode", "1");
            }
            else
            {
                conn.set("debugMode", "2");
            }

            if (playerClose.Checked)
            {
                conn.set("playerClose", "1");
            }
            else
            {
                conn.set("playerClose", "0");
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
    }
}
