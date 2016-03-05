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
            this.selectInstallDir = new System.Windows.Forms.Button();
            this.installButton = new System.Windows.Forms.Button();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.loadingLabel = new System.Windows.Forms.Label();
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
            this.pkgName.Location = new System.Drawing.Point(74, 10);
            this.pkgName.Name = "pkgName";
            this.pkgName.ReadOnly = true;
            this.pkgName.Size = new System.Drawing.Size(240, 21);
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
            this.pkgDesc.Location = new System.Drawing.Point(74, 39);
            this.pkgDesc.Name = "pkgDesc";
            this.pkgDesc.ReadOnly = true;
            this.pkgDesc.Size = new System.Drawing.Size(419, 21);
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
            this.pkgPath.Location = new System.Drawing.Point(74, 68);
            this.pkgPath.Name = "pkgPath";
            this.pkgPath.ReadOnly = true;
            this.pkgPath.Size = new System.Drawing.Size(419, 21);
            this.pkgPath.TabIndex = 8;
            // 
            // tipLabel
            // 
            this.tipLabel.AutoSize = true;
            this.tipLabel.Location = new System.Drawing.Point(12, 132);
            this.tipLabel.Name = "tipLabel";
            this.tipLabel.Size = new System.Drawing.Size(161, 12);
            this.tipLabel.TabIndex = 9;
            this.tipLabel.Text = "点击 确定安装 来安装该包。";
            this.tipLabel.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "安装到：";
            // 
            // installPath
            // 
            this.installPath.Location = new System.Drawing.Point(74, 98);
            this.installPath.Name = "installPath";
            this.installPath.Size = new System.Drawing.Size(351, 21);
            this.installPath.TabIndex = 11;
            // 
            // selectInstallDir
            // 
            this.selectInstallDir.Location = new System.Drawing.Point(431, 98);
            this.selectInstallDir.Name = "selectInstallDir";
            this.selectInstallDir.Size = new System.Drawing.Size(62, 23);
            this.selectInstallDir.TabIndex = 12;
            this.selectInstallDir.Text = "浏览...";
            this.selectInstallDir.UseVisualStyleBackColor = true;
            this.selectInstallDir.Click += new System.EventHandler(this.selectInstallDir_Click);
            // 
            // installButton
            // 
            this.installButton.Enabled = false;
            this.installButton.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.installButton.Location = new System.Drawing.Point(366, 132);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(127, 37);
            this.installButton.TabIndex = 13;
            this.installButton.Text = "确定安装 (&I)";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(14, 129);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(300, 22);
            this.progress.TabIndex = 14;
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.Location = new System.Drawing.Point(12, 158);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(65, 12);
            this.loadingLabel.TabIndex = 15;
            this.loadingLabel.Text = "正在读取包";
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 181);
            this.Controls.Add(this.loadingLabel);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.installButton);
            this.Controls.Add(this.selectInstallDir);
            this.Controls.Add(this.installPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tipLabel);
            this.Controls.Add(this.pkgPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pkgDesc);
            this.Controls.Add(this.pkgVer);
            this.Controls.Add(this.pkgName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "main";
            this.Text = "MoeCraft Package Installer";
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
        private System.Windows.Forms.Button selectInstallDir;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label loadingLabel;
    }
}

