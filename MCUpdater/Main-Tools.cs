//主页：工具箱部分

using System;
using System.IO;
using System.Windows.Forms;

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
                    if (fileInfo.Name.Substring(0, black.Length) != black)
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
            }
            catch (Exception ex)
            {
                error("激活或禁用Mod失败：" + ex.Message + "\r\n请确保你已经关闭了 MineCraft", "激活或禁用Mod失败");
            }
        }
        #endregion

        #region 备份键位
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
    }
}
