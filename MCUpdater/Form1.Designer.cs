namespace MCUpdater
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lastLog = new System.Windows.Forms.ToolStripStatusLabel();
            this.mcLauncher = new System.Windows.Forms.TabPage();
            this.regLink = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.debugMode = new System.Windows.Forms.CheckBox();
            this.playerFS = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.playerHeight = new System.Windows.Forms.TextBox();
            this.playerWidth = new System.Windows.Forms.TextBox();
            this.playerArgs = new System.Windows.Forms.TextBox();
            this.playerJRE = new System.Windows.Forms.TextBox();
            this.playerMem = new System.Windows.Forms.TextBox();
            this.playerName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.playerClose = new System.Windows.Forms.CheckBox();
            this.playerRun = new System.Windows.Forms.Button();
            this.playerSave = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.playerJREBrowser = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.aboutTab = new System.Windows.Forms.TabPage();
            this.cleanDownloadCache = new System.Windows.Forms.Button();
            this.launcherButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.version = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.logTab = new System.Windows.Forms.TabPage();
            this.logBox = new System.Windows.Forms.TextBox();
            this.offlineModsTab = new System.Windows.Forms.TabPage();
            this.refreshMods = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.offlineModList = new System.Windows.Forms.CheckedListBox();
            this.saveOfflineMod = new System.Windows.Forms.Button();
            this.checkUpdate = new System.Windows.Forms.TabPage();
            this.updateForce = new System.Windows.Forms.CheckBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.progressGroupBox = new System.Windows.Forms.GroupBox();
            this.updateLog = new System.Windows.Forms.TextBox();
            this.updateAction = new System.Windows.Forms.Label();
            this.updateThisProgressText = new System.Windows.Forms.Label();
            this.updateThisProgressBar = new System.Windows.Forms.ProgressBar();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.statusStrip1.SuspendLayout();
            this.mcLauncher.SuspendLayout();
            this.aboutTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.logTab.SuspendLayout();
            this.offlineModsTab.SuspendLayout();
            this.checkUpdate.SuspendLayout();
            this.progressGroupBox.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lastLog});
            this.statusStrip1.Location = new System.Drawing.Point(0, 403);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(778, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lastLog
            // 
            this.lastLog.Name = "lastLog";
            this.lastLog.Size = new System.Drawing.Size(56, 17);
            this.lastLog.Text = "准备就绪";
            // 
            // mcLauncher
            // 
            this.mcLauncher.Controls.Add(this.regLink);
            this.mcLauncher.Controls.Add(this.label8);
            this.mcLauncher.Controls.Add(this.debugMode);
            this.mcLauncher.Controls.Add(this.playerFS);
            this.mcLauncher.Controls.Add(this.label12);
            this.mcLauncher.Controls.Add(this.playerHeight);
            this.mcLauncher.Controls.Add(this.playerWidth);
            this.mcLauncher.Controls.Add(this.playerArgs);
            this.mcLauncher.Controls.Add(this.playerJRE);
            this.mcLauncher.Controls.Add(this.playerMem);
            this.mcLauncher.Controls.Add(this.playerName);
            this.mcLauncher.Controls.Add(this.label11);
            this.mcLauncher.Controls.Add(this.playerClose);
            this.mcLauncher.Controls.Add(this.playerRun);
            this.mcLauncher.Controls.Add(this.playerSave);
            this.mcLauncher.Controls.Add(this.label10);
            this.mcLauncher.Controls.Add(this.playerJREBrowser);
            this.mcLauncher.Controls.Add(this.label9);
            this.mcLauncher.Controls.Add(this.label7);
            this.mcLauncher.Controls.Add(this.label6);
            this.mcLauncher.Controls.Add(this.label13);
            this.mcLauncher.Location = new System.Drawing.Point(4, 26);
            this.mcLauncher.Name = "mcLauncher";
            this.mcLauncher.Padding = new System.Windows.Forms.Padding(3);
            this.mcLauncher.Size = new System.Drawing.Size(745, 357);
            this.mcLauncher.TabIndex = 7;
            this.mcLauncher.Text = "启动器";
            this.mcLauncher.UseVisualStyleBackColor = true;
            this.mcLauncher.Click += new System.EventHandler(this.mcLauncher_Click);
            // 
            // regLink
            // 
            this.regLink.ActiveLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.regLink.AutoSize = true;
            this.regLink.BackColor = System.Drawing.Color.Transparent;
            this.regLink.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.regLink.Location = new System.Drawing.Point(613, 20);
            this.regLink.Name = "regLink";
            this.regLink.Size = new System.Drawing.Size(116, 17);
            this.regLink.TabIndex = 27;
            this.regLink.TabStop = true;
            this.regLink.Tag = "";
            this.regLink.Text = "没有账号？点击注册";
            this.regLink.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.regLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.regLink_LinkClicked);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(407, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(200, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = "请填写您在用户中心填写的游戏名称";
            // 
            // debugMode
            // 
            this.debugMode.AutoSize = true;
            this.debugMode.Location = new System.Drawing.Point(654, 123);
            this.debugMode.Name = "debugMode";
            this.debugMode.Size = new System.Drawing.Size(75, 21);
            this.debugMode.TabIndex = 25;
            this.debugMode.Text = "调试模式";
            this.debugMode.UseVisualStyleBackColor = true;
            // 
            // playerFS
            // 
            this.playerFS.AutoSize = true;
            this.playerFS.Location = new System.Drawing.Point(678, 52);
            this.playerFS.Name = "playerFS";
            this.playerFS.Size = new System.Drawing.Size(51, 21);
            this.playerFS.TabIndex = 24;
            this.playerFS.Text = "全屏";
            this.playerFS.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(444, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 17);
            this.label12.TabIndex = 23;
            this.label12.Text = "x";
            // 
            // playerHeight
            // 
            this.playerHeight.Location = new System.Drawing.Point(460, 52);
            this.playerHeight.MaxLength = 6;
            this.playerHeight.Name = "playerHeight";
            this.playerHeight.Size = new System.Drawing.Size(61, 23);
            this.playerHeight.TabIndex = 22;
            this.playerHeight.Text = "768";
            // 
            // playerWidth
            // 
            this.playerWidth.Location = new System.Drawing.Point(381, 52);
            this.playerWidth.MaxLength = 6;
            this.playerWidth.Name = "playerWidth";
            this.playerWidth.Size = new System.Drawing.Size(61, 23);
            this.playerWidth.TabIndex = 21;
            this.playerWidth.Text = "1024";
            // 
            // playerArgs
            // 
            this.playerArgs.Location = new System.Drawing.Point(19, 150);
            this.playerArgs.Multiline = true;
            this.playerArgs.Name = "playerArgs";
            this.playerArgs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.playerArgs.Size = new System.Drawing.Size(710, 114);
            this.playerArgs.TabIndex = 14;
            // 
            // playerJRE
            // 
            this.playerJRE.Location = new System.Drawing.Point(108, 86);
            this.playerJRE.Name = "playerJRE";
            this.playerJRE.Size = new System.Drawing.Size(513, 23);
            this.playerJRE.TabIndex = 11;
            // 
            // playerMem
            // 
            this.playerMem.AllowDrop = true;
            this.playerMem.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.playerMem.Location = new System.Drawing.Point(86, 52);
            this.playerMem.MaxLength = 10;
            this.playerMem.Name = "playerMem";
            this.playerMem.Size = new System.Drawing.Size(87, 23);
            this.playerMem.TabIndex = 2;
            this.playerMem.Text = "1024";
            // 
            // playerName
            // 
            this.playerName.AllowDrop = true;
            this.playerName.Location = new System.Drawing.Point(86, 17);
            this.playerName.MaxLength = 20;
            this.playerName.Name = "playerName";
            this.playerName.Size = new System.Drawing.Size(315, 23);
            this.playerName.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(322, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 17);
            this.label11.TabIndex = 20;
            this.label11.Text = "分辨率：";
            // 
            // playerClose
            // 
            this.playerClose.AutoSize = true;
            this.playerClose.Location = new System.Drawing.Point(268, 310);
            this.playerClose.Name = "playerClose";
            this.playerClose.Size = new System.Drawing.Size(147, 21);
            this.playerClose.TabIndex = 19;
            this.playerClose.Text = "启动游戏后退出本程序";
            this.playerClose.UseVisualStyleBackColor = true;
            // 
            // playerRun
            // 
            this.playerRun.Location = new System.Drawing.Point(512, 303);
            this.playerRun.Name = "playerRun";
            this.playerRun.Size = new System.Drawing.Size(217, 33);
            this.playerRun.TabIndex = 17;
            this.playerRun.Text = "启动 MoeCraft (&R)";
            this.playerRun.UseVisualStyleBackColor = true;
            this.playerRun.Click += new System.EventHandler(this.playerRun_Click);
            // 
            // playerSave
            // 
            this.playerSave.Location = new System.Drawing.Point(20, 303);
            this.playerSave.Name = "playerSave";
            this.playerSave.Size = new System.Drawing.Size(145, 33);
            this.playerSave.TabIndex = 16;
            this.playerSave.Text = "保存设置 (&S)";
            this.playerSave.UseVisualStyleBackColor = true;
            this.playerSave.Click += new System.EventHandler(this.playerSave_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(200, 17);
            this.label10.TabIndex = 15;
            this.label10.Text = "启动参数（高级，一般无需在意）：";
            // 
            // playerJREBrowser
            // 
            this.playerJREBrowser.Location = new System.Drawing.Point(627, 86);
            this.playerJREBrowser.Name = "playerJREBrowser";
            this.playerJREBrowser.Size = new System.Drawing.Size(102, 23);
            this.playerJREBrowser.TabIndex = 13;
            this.playerJREBrowser.Text = "浏览 ... (&B)";
            this.playerJREBrowser.UseVisualStyleBackColor = true;
            this.playerJREBrowser.Click += new System.EventHandler(this.playerJREBrowser_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "JRE安装目录：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 5;
            this.label7.Text = "启动内存：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(175, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "MB";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 17);
            this.label13.TabIndex = 0;
            this.label13.Text = "游戏昵称：";
            // 
            // aboutTab
            // 
            this.aboutTab.Controls.Add(this.cleanDownloadCache);
            this.aboutTab.Controls.Add(this.launcherButton);
            this.aboutTab.Controls.Add(this.label5);
            this.aboutTab.Controls.Add(this.linkLabel2);
            this.aboutTab.Controls.Add(this.label4);
            this.aboutTab.Controls.Add(this.pictureBox1);
            this.aboutTab.Controls.Add(this.linkLabel1);
            this.aboutTab.Controls.Add(this.label3);
            this.aboutTab.Controls.Add(this.version);
            this.aboutTab.Controls.Add(this.label2);
            this.aboutTab.Location = new System.Drawing.Point(4, 22);
            this.aboutTab.Name = "aboutTab";
            this.aboutTab.Padding = new System.Windows.Forms.Padding(3);
            this.aboutTab.Size = new System.Drawing.Size(745, 361);
            this.aboutTab.TabIndex = 5;
            this.aboutTab.Text = "关于";
            this.aboutTab.UseVisualStyleBackColor = true;
            // 
            // cleanDownloadCache
            // 
            this.cleanDownloadCache.Location = new System.Drawing.Point(413, 299);
            this.cleanDownloadCache.Name = "cleanDownloadCache";
            this.cleanDownloadCache.Size = new System.Drawing.Size(133, 34);
            this.cleanDownloadCache.TabIndex = 9;
            this.cleanDownloadCache.Text = "清除下载缓存 (&C)";
            this.cleanDownloadCache.UseVisualStyleBackColor = true;
            this.cleanDownloadCache.Click += new System.EventHandler(this.cleanDownloadCache_Click);
            // 
            // launcherButton
            // 
            this.launcherButton.Location = new System.Drawing.Point(584, 299);
            this.launcherButton.Name = "launcherButton";
            this.launcherButton.Size = new System.Drawing.Size(133, 34);
            this.launcherButton.TabIndex = 8;
            this.launcherButton.Text = "MoeCraft 启动器 (&L)";
            this.launcherButton.UseVisualStyleBackColor = true;
            this.launcherButton.Click += new System.EventHandler(this.launcherButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Copyright © MoeCraft.All Rights Reserved.";
            // 
            // linkLabel2
            // 
            this.linkLabel2.ActiveLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel2.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.linkLabel2.Location = new System.Drawing.Point(263, 133);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(113, 17);
            this.linkLabel2.TabIndex = 6;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Tag = "";
            this.linkLabel2.Text = "StusGame GROUP";
            this.linkLabel2.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "@";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.Location = new System.Drawing.Point(18, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(699, 99);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.linkLabel1.Location = new System.Drawing.Point(205, 133);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(45, 17);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "";
            this.linkLabel1.Text = "Kenvix";
            this.linkLabel1.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "作者：";
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.Location = new System.Drawing.Point(126, 133);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(33, 17);
            this.version.TabIndex = 1;
            this.version.Text = "V1.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "MoeCraft 更新程序";
            // 
            // logTab
            // 
            this.logTab.Controls.Add(this.logBox);
            this.logTab.Location = new System.Drawing.Point(4, 22);
            this.logTab.Name = "logTab";
            this.logTab.Padding = new System.Windows.Forms.Padding(3);
            this.logTab.Size = new System.Drawing.Size(745, 361);
            this.logTab.TabIndex = 4;
            this.logTab.Text = "操作日志";
            this.logTab.UseVisualStyleBackColor = true;
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(7, 7);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(732, 334);
            this.logBox.TabIndex = 0;
            // 
            // offlineModsTab
            // 
            this.offlineModsTab.Controls.Add(this.refreshMods);
            this.offlineModsTab.Controls.Add(this.label1);
            this.offlineModsTab.Controls.Add(this.offlineModList);
            this.offlineModsTab.Controls.Add(this.saveOfflineMod);
            this.offlineModsTab.Location = new System.Drawing.Point(4, 22);
            this.offlineModsTab.Name = "offlineModsTab";
            this.offlineModsTab.Padding = new System.Windows.Forms.Padding(3);
            this.offlineModsTab.Size = new System.Drawing.Size(745, 361);
            this.offlineModsTab.TabIndex = 3;
            this.offlineModsTab.Text = "单机Mod管理";
            this.offlineModsTab.UseVisualStyleBackColor = true;
            // 
            // refreshMods
            // 
            this.refreshMods.Location = new System.Drawing.Point(21, 317);
            this.refreshMods.Name = "refreshMods";
            this.refreshMods.Size = new System.Drawing.Size(87, 34);
            this.refreshMods.TabIndex = 4;
            this.refreshMods.Text = "刷新 (&R)";
            this.refreshMods.UseVisualStyleBackColor = true;
            this.refreshMods.Click += new System.EventHandler(this.refreshMods_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "请选择要操作的Mod，勾选表示启用，不选表示禁用";
            // 
            // offlineModList
            // 
            this.offlineModList.FormattingEnabled = true;
            this.offlineModList.Location = new System.Drawing.Point(21, 37);
            this.offlineModList.Name = "offlineModList";
            this.offlineModList.Size = new System.Drawing.Size(706, 274);
            this.offlineModList.TabIndex = 2;
            this.offlineModList.SelectedIndexChanged += new System.EventHandler(this.offlineModList_SelectedIndexChanged);
            // 
            // saveOfflineMod
            // 
            this.saveOfflineMod.Location = new System.Drawing.Point(594, 317);
            this.saveOfflineMod.Name = "saveOfflineMod";
            this.saveOfflineMod.Size = new System.Drawing.Size(133, 34);
            this.saveOfflineMod.TabIndex = 1;
            this.saveOfflineMod.Text = "保存更改 (&S)";
            this.saveOfflineMod.UseVisualStyleBackColor = true;
            this.saveOfflineMod.Click += new System.EventHandler(this.saveOfflineMod_Click);
            // 
            // checkUpdate
            // 
            this.checkUpdate.Controls.Add(this.updateForce);
            this.checkUpdate.Controls.Add(this.updateButton);
            this.checkUpdate.Controls.Add(this.progressGroupBox);
            this.checkUpdate.Location = new System.Drawing.Point(4, 22);
            this.checkUpdate.Name = "checkUpdate";
            this.checkUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.checkUpdate.Size = new System.Drawing.Size(745, 361);
            this.checkUpdate.TabIndex = 2;
            this.checkUpdate.Text = "检查更新";
            this.checkUpdate.UseVisualStyleBackColor = true;
            // 
            // updateForce
            // 
            this.updateForce.AutoSize = true;
            this.updateForce.Location = new System.Drawing.Point(24, 322);
            this.updateForce.Name = "updateForce";
            this.updateForce.Size = new System.Drawing.Size(311, 21);
            this.updateForce.TabIndex = 11;
            this.updateForce.Text = "强制更新 [ 如果更新后仍无法启动游戏，请选中这里 ]";
            this.updateForce.UseVisualStyleBackColor = true;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(593, 322);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(146, 29);
            this.updateButton.TabIndex = 1;
            this.updateButton.Text = "检查更新 (&C)";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // progressGroupBox
            // 
            this.progressGroupBox.Controls.Add(this.updateLog);
            this.progressGroupBox.Controls.Add(this.updateAction);
            this.progressGroupBox.Controls.Add(this.updateThisProgressText);
            this.progressGroupBox.Controls.Add(this.updateThisProgressBar);
            this.progressGroupBox.Location = new System.Drawing.Point(6, 20);
            this.progressGroupBox.Name = "progressGroupBox";
            this.progressGroupBox.Size = new System.Drawing.Size(733, 296);
            this.progressGroupBox.TabIndex = 10;
            this.progressGroupBox.TabStop = false;
            this.progressGroupBox.Text = "操作进度";
            this.progressGroupBox.Enter += new System.EventHandler(this.progressGroupBox_Enter);
            // 
            // updateLog
            // 
            this.updateLog.Location = new System.Drawing.Point(18, 80);
            this.updateLog.Multiline = true;
            this.updateLog.Name = "updateLog";
            this.updateLog.ReadOnly = true;
            this.updateLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.updateLog.Size = new System.Drawing.Size(700, 210);
            this.updateLog.TabIndex = 7;
            // 
            // updateAction
            // 
            this.updateAction.AutoSize = true;
            this.updateAction.Location = new System.Drawing.Point(15, 24);
            this.updateAction.Name = "updateAction";
            this.updateAction.Size = new System.Drawing.Size(272, 17);
            this.updateAction.TabIndex = 6;
            this.updateAction.Text = "若要检查更新，请点击右下角的 [ 检查更新 ] 按钮";
            this.updateAction.Click += new System.EventHandler(this.updateAction_Click);
            // 
            // updateThisProgressText
            // 
            this.updateThisProgressText.AutoSize = true;
            this.updateThisProgressText.Location = new System.Drawing.Point(662, 24);
            this.updateThisProgressText.Name = "updateThisProgressText";
            this.updateThisProgressText.Size = new System.Drawing.Size(56, 17);
            this.updateThisProgressText.TabIndex = 5;
            this.updateThisProgressText.Text = "准备就绪";
            // 
            // updateThisProgressBar
            // 
            this.updateThisProgressBar.Location = new System.Drawing.Point(18, 51);
            this.updateThisProgressBar.Name = "updateThisProgressBar";
            this.updateThisProgressBar.Size = new System.Drawing.Size(700, 23);
            this.updateThisProgressBar.TabIndex = 3;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.mcLauncher);
            this.mainTabControl.Controls.Add(this.checkUpdate);
            this.mainTabControl.Controls.Add(this.offlineModsTab);
            this.mainTabControl.Controls.Add(this.logTab);
            this.mainTabControl.Controls.Add(this.aboutTab);
            this.mainTabControl.Location = new System.Drawing.Point(13, 13);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(753, 387);
            this.mainTabControl.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(778, 425);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainTabControl);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "MoeCraft 更新程序";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mcLauncher.ResumeLayout(false);
            this.mcLauncher.PerformLayout();
            this.aboutTab.ResumeLayout(false);
            this.aboutTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.logTab.ResumeLayout(false);
            this.logTab.PerformLayout();
            this.offlineModsTab.ResumeLayout(false);
            this.offlineModsTab.PerformLayout();
            this.checkUpdate.ResumeLayout(false);
            this.checkUpdate.PerformLayout();
            this.progressGroupBox.ResumeLayout(false);
            this.progressGroupBox.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lastLog;
        private System.Windows.Forms.TabPage mcLauncher;
        private System.Windows.Forms.CheckBox debugMode;
        private System.Windows.Forms.CheckBox playerFS;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox playerHeight;
        private System.Windows.Forms.TextBox playerWidth;
        private System.Windows.Forms.TextBox playerArgs;
        private System.Windows.Forms.TextBox playerJRE;
        private System.Windows.Forms.TextBox playerMem;
        private System.Windows.Forms.TextBox playerName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox playerClose;
        private System.Windows.Forms.Button playerRun;
        private System.Windows.Forms.Button playerSave;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button playerJREBrowser;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage aboutTab;
        private System.Windows.Forms.Button cleanDownloadCache;
        private System.Windows.Forms.Button launcherButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label version;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage logTab;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.TabPage offlineModsTab;
        private System.Windows.Forms.Button refreshMods;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox offlineModList;
        private System.Windows.Forms.Button saveOfflineMod;
        private System.Windows.Forms.TabPage checkUpdate;
        private System.Windows.Forms.CheckBox updateForce;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.GroupBox progressGroupBox;
        private System.Windows.Forms.TextBox updateLog;
        private System.Windows.Forms.Label updateAction;
        private System.Windows.Forms.Label updateThisProgressText;
        private System.Windows.Forms.ProgressBar updateThisProgressBar;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.LinkLabel regLink;
        private System.Windows.Forms.Label label8;
    }
}