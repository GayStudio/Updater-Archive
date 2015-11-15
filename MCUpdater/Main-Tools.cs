//主页：工具箱部分
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

namespace MCUpdater
{
    public partial class Main : Form
    {

        #region 单机MOD管理器
        public void getModList()
        {
            string black = "CodeChickenLib";
            if (!Directory.Exists(x.path + x.moddir))
            {
                return;
            }
            try
            {
                offlineModList.Items.Clear();
                foreach (string mod in Directory.GetFiles(x.path + x.moddir, "*", SearchOption.AllDirectories))
                {
                    FileInfo fileInfo = new FileInfo(mod);
                    if (fileInfo.Name.Length < black.Length || fileInfo.Name.Substring(0, black.Length) != black)
                    {
                        if (fileInfo.Extension == ".jar" || fileInfo.Extension == ".zip")
                        {
                            int key = offlineModList.Items.Add(fileInfo.Name);
                            offlineModList.SetItemChecked(key, true);
                        }
                        else if (fileInfo.Extension == ".disabled")
                        {
                            string fileName = fileInfo.Name;
                            int key = offlineModList.Items.Add(fileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log("列出Mod失败：" + ex.ToString());
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
                foreach (string mod in offlineModList.Items)
                {
                    FileInfo fileInfo = new FileInfo(x.path + x.moddir + mod);
                    if (offlineModList.GetItemChecked(key) && fileInfo.Extension == ".disabled")
                    {
                        File.Move(fileInfo.FullName, x.path + x.moddir + mod.Substring(0, fileInfo.Name.LastIndexOf(fileInfo.Extension)));
                    }
                    else if (!offlineModList.GetItemChecked(key) && fileInfo.Extension != ".disabled")
                    {
                        File.Move(x.path + x.moddir + mod, x.path + x.moddir + mod + ".disabled");
                    }
                    key++;
                }
                getModList();
                log("已更改可选Mod");
            }
            catch (Exception ex)
            {
                error("激活或禁用Mod失败：" + ex.Message + "\r\n请确保你已经关闭了 MineCraft", "激活或禁用Mod失败");
            }
        }
        #endregion

        #region 备份和恢复键位
        private void bakMcOpt_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string file in x.bakList)
                {
                    if (File.Exists(x.path + x.binpath + file))
                    {
                        if (File.Exists(x.path + x.binpath + file))
                        {
                            File.Delete(x.path + x.updpath + "backup." + file);
                        }
                        File.Copy(x.path + x.binpath + file, x.path + x.updpath + "backup." + file);
                    }
                }
                success("已成功备份键位和服务器列表数据");
            }
            catch (Exception ex)
            {
                error(ex.Message, "备份失败");
            }
        }

        private void recMcOpt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要恢复以前备份的设置吗？\r\n这将覆盖现有的键位、服务器列表等数据", "确实要恢复设置吗", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    foreach (string file in x.bakList)
                    {
                        if (File.Exists(x.path + x.updpath + "backup." + file))
                        {
                            if (File.Exists(x.path + x.binpath + file))
                            {
                                File.Delete(x.path + x.binpath + file);
                            }
                            File.Copy(x.path + x.updpath + "backup." + file, x.path + x.binpath + file);
                        }
                    }
                    success("已成功恢复键位和服务器列表数据\r\n重新启动游戏后即可生效");
                }
                catch (Exception ex)
                {
                    error(ex.Message, "恢复失败");
                }
            }
        }
        #endregion

        #region forge进度条
        private void disableForgeProgress_Click(object sender, EventArgs e)
        {
            var v = x.path + x.binpath + @"\Config\splash.properties";
            if (!File.Exists(v))
            {
                error("你还未安装 " + x.name, "操作失败");
                return;
            }
            if (MessageBox.Show("你确定要关闭启动进度条吗？\r\n当关闭启动进度条后，在启动游戏的过程中，游戏将一直不会响应，你将需要等待很长时间\r\n因此，不建议没有启动崩溃问题的电脑关闭启动进度条", "确实要关闭启动进度条吗", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string text = File.ReadAllText(v);
                    File.WriteAllText(v, text.Replace("enabled=true", "enabled=false"));
                    log("已成功关闭启动进度条");
                    success("已成功关闭启动进度条", "操作成功");
                }
                catch (Exception ex)
                {
                    error(ex.Message);
                    return;
                }
            }
        }

        private void enableForgeProgress_Click(object sender, EventArgs e)
        {
            var v = x.path + x.binpath + @"\Config\splash.properties";
            if (!File.Exists(v))
            {
                error("你还未安装 " + x.name);
                return;
            }
            if (MessageBox.Show("你确定要恢复启动进度条吗？\r\n当恢复启动进度条后，如果你的电脑有问题，你将不能启动游戏", "确实要恢复启动进度条吗", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string text = File.ReadAllText(v);
                    File.WriteAllText(v, text.Replace("enabled=false", "enabled=true"));
                    log("已成功恢复启动进度条");
                    success("已成功恢复启动进度条", "操作成功");
                }
                catch (Exception ex)
                {
                    error(ex.Message, "操作失败");
                    return;
                }
            }
        }
        #endregion

        private void installPatch_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确实要安装低配补丁吗？\r\n安装低配补丁后，FPS(帧率)将会大幅提升，但是画质将会降低、大量特效将被关闭\r\n低配补丁还需要Optifine MOD支持（默认已开启）\r\n安装前请先备份游戏内设置。\r\n如果安装后FPS仍然很低，请尝试调高游戏内存上限", "确实要安装低配补丁吗", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                #region 低配补丁
                XmlDocument xml;
                XmlElement root;
                XmlElement ofnode;
                XmlNodeList of;
                XmlElement opnode;
                XmlNodeList op;
                try
                {
                    xml = new XmlDocument();
                    xml.LoadXml(File.ReadAllText(x.path + x.updpath + "mcpatch.xml"));
                    root = xml.DocumentElement;
                    //optionof.txt
                    ofnode = (XmlElement)root.SelectSingleNode("optionof");
                    of = ofnode.ChildNodes;
                    //option.txt
                    opnode = (XmlElement)root.SelectSingleNode("option");
                    op = opnode.ChildNodes;
                }
                catch(Exception ex)
                {
                    error(ex.Message,"加载低配补丁失败");
                    return;
                }
                var opfile = x.path + x.binpath + @"options.txt";
                var offile = x.path + x.binpath + @"optionsof.txt";
                var encode = Encoding.GetEncoding("gb2312");
                Regex regex;
                if (!File.Exists(opfile))
                {
                    error("你还未安装 " + x.name);
                    return;
                }
                try
                {
                    var opdata = File.ReadAllText(opfile, encode);
                    foreach (XmlNode od in op)
                    {
                        regex = new Regex(od.Name + @"\:" + "(.*)");
                        opdata = regex.Replace(opdata, od.Name + ":" + od.InnerText);
                    }
                    File.WriteAllText(opfile, opdata, encode);
                    if(File.Exists(offile))
                    {
                        var ofdata = File.ReadAllText(offile, encode);
                        foreach (XmlNode fd in of)
                        {
                            regex = new Regex(fd.Name + @"\:" + "(.*)");
                            ofdata = regex.Replace(ofdata, fd.Name + ":" + fd.InnerText);
                        }
                        File.WriteAllText(offile, ofdata, encode);
                    }
                }
                catch (Exception ex)
                {
                    error(ex.Message, "在应用低配补丁时出错");
                    return;
                }
                #endregion
                success("已安装低配补丁。重启游戏后生效\r\n如果安装后FPS仍然很低，请尝试调高游戏内存上限");
            }
        }
    }
}
