namespace MoecraftPkgInstaller
{
    partial class main
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pkgName = new System.Windows.Forms.TextBox();
            this.pkgVer = new System.Windows.Forms.TextBox();
            this.pkgDesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pkgPath = new System.Windows.Forms.TextBox();
            this.tipLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.installPath = new System.Windows.Forms.TextBox();
            this.installButton = new System.Windows.Forms.Button();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.loadingLabel = new System.Windows.Forms.Label();
            this.pkgUnpack = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pkgBat = new System.Windows.Forms.TextBox();
            this.pkgOutput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.openOutputDir = new System.Windows.Forms.LinkLabel();
            this.openInstallDir = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "包ID：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "包自述：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "版本：";
            // 
            // pkgName
            // 
            this.pkgName.Location = new System.Drawing.Point(90, 10);
            this.pkgName.Name = "pkgName";
            this.pkgName.ReadOnly = true;
            this.pkgName.Size = new System.Drawing.Size(224, 21);
            this.pkgName.TabIndex = 3;
            // 
            // pkgVer
            // 
            this.pkgVer.Location = new System.Drawing.Point(390, 10);
            this.pkgVer.Name = "pkgVer";
            this.pkgVer.ReadOnly = true;
            this.pkgVer.Size = new System.Drawing.Size(103, 21);
            this.pkgVer.TabIndex = 5;
            // 
            // pkgDesc
            // 
            this.pkgDesc.Location = new System.Drawing.Point(90, 39);
            this.pkgDesc.Name = "pkgDesc";
            this.pkgDesc.ReadOnly = true;
            this.pkgDesc.Size = new System.Drawing.Size(403, 21);
            this.pkgDesc.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "包路径：";
            // 
            // pkgPath
            // 
            this.pkgPath.Location = new System.Drawing.Point(90, 68);
            this.pkgPath.Name = "pkgPath";
            this.pkgPath.ReadOnly = true;
            this.pkgPath.Size = new System.Drawing.Size(403, 21);
            this.pkgPath.TabIndex = 8;
            // 
            // tipLabel
            // 
            this.tipLabel.AutoSize = true;
            this.tipLabel.Location = new System.Drawing.Point(12, 221);
            this.tipLabel.Name = "tipLabel";
            this.tipLabel.Size = new System.Drawing.Size(269, 12);
            this.tipLabel.TabIndex = 9;
            this.tipLabel.Text = "以上是该包的信息，点击 确定安装 来安装该包。";
            this.tipLabel.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "安装到：";
            // 
            // installPath
            // 
            this.installPath.Location = new System.Drawing.Point(90, 185);
            this.installPath.Name = "installPath";
            this.installPath.ReadOnly = true;
            this.installPath.Size = new System.Drawing.Size(403, 21);
            this.installPath.TabIndex = 11;
            // 
            // installButton
            // 
            this.installButton.Enabled = false;
            this.installButton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.installButton.Location = new System.Drawing.Point(335, 218);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(158, 40);
            this.installButton.TabIndex = 13;
            this.installButton.Text = "确定安装 (&I)";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(14, 216);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(300, 22);
            this.progress.TabIndex = 14;
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.Location = new System.Drawing.Point(12, 247);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(65, 12);
            this.loadingLabel.TabIndex = 15;
            this.loadingLabel.Text = "正在读取包";
            // 
            // pkgUnpack
            // 
            this.pkgUnpack.AutoSize = true;
            this.pkgUnpack.Enabled = false;
            this.pkgUnpack.Location = new System.Drawing.Point(90, 130);
            this.pkgUnpack.Name = "pkgUnpack";
            this.pkgUnpack.Size = new System.Drawing.Size(144, 16);
            this.pkgUnpack.TabIndex = 16;
            this.pkgUnpack.Text = "该包需要运行解包程序";
            this.pkgUnpack.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "安装程序：";
            // 
            // pkgBat
            // 
            this.pkgBat.Location = new System.Drawing.Point(90, 99);
            this.pkgBat.Name = "pkgBat";
            this.pkgBat.ReadOnly = true;
            this.pkgBat.Size = new System.Drawing.Size(403, 21);
            this.pkgBat.TabIndex = 17;
            // 
            // pkgOutput
            // 
            this.pkgOutput.Location = new System.Drawing.Point(90, 154);
            this.pkgOutput.Name = "pkgOutput";
            this.pkgOutput.ReadOnly = true;
            this.pkgOutput.Size = new System.Drawing.Size(403, 21);
            this.pkgOutput.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "输出目录：";
            // 
            // openOutputDir
            // 
            this.openOutputDir.ActiveLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.openOutputDir.AutoSize = true;
            this.openOutputDir.BackColor = System.Drawing.Color.Transparent;
            this.openOutputDir.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.openOutputDir.Location = new System.Drawing.Point(416, 131);
            this.openOutputDir.Name = "openOutputDir";
            this.openOutputDir.Size = new System.Drawing.Size(77, 12);
            this.openOutputDir.TabIndex = 21;
            this.openOutputDir.TabStop = true;
            this.openOutputDir.Tag = "";
            this.openOutputDir.Text = "打开输出目录";
            this.openOutputDir.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.openOutputDir.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openOutputDir_LinkClicked);
            // 
            // openInstallDir
            // 
            this.openInstallDir.ActiveLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.openInstallDir.AutoSize = true;
            this.openInstallDir.BackColor = System.Drawing.Color.Transparent;
            this.openInstallDir.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.openInstallDir.Location = new System.Drawing.Point(323, 131);
            this.openInstallDir.Name = "openInstallDir";
            this.openInstallDir.Size = new System.Drawing.Size(77, 12);
            this.openInstallDir.TabIndex = 22;
            this.openInstallDir.TabStop = true;
            this.openInstallDir.Tag = "";
            this.openInstallDir.Text = "打开安装目录";
            this.openInstallDir.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.openInstallDir.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openInstallDir_LinkClicked);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 271);
            this.Controls.Add(this.openInstallDir);
            this.Controls.Add(this.openOutputDir);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.pkgOutput);
            this.Controls.Add(this.pkgBat);
            this.Controls.Add(this.pkgUnpack);
            this.Controls.Add(this.loadingLabel);
            this.Controls.Add(this.installButton);
            this.Controls.Add(this.installPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pkgPath);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pkgDesc);
            this.Controls.Add(this.pkgVer);
            this.Controls.Add(this.pkgName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tipLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main";
            this.Text = "MoeCraft Package Installer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.main_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pkgName;
        private System.Windows.Forms.TextBox pkgVer;
        private System.Windows.Forms.TextBox pkgDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pkgPath;
        private System.Windows.Forms.Label tipLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox installPath;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label loadingLabel;
        private System.Windows.Forms.CheckBox pkgUnpack;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox pkgBat;
        private System.Windows.Forms.TextBox pkgOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel openOutputDir;
        private System.Windows.Forms.LinkLabel openInstallDir;
    }
}

