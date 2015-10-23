namespace MCUpdater
{
    partial class logViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.selectAllButton = new System.Windows.Forms.ToolStripMenuItem();
            this.copyButton = new System.Windows.Forms.ToolStripMenuItem();
            this.fontButton = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox = new System.Windows.Forms.TextBox();
            this.font = new System.Windows.Forms.FontDialog();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.资源管理器EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllButton,
            this.copyButton,
            this.fontButton,
            this.打开OToolStripMenuItem,
            this.保存VToolStripMenuItem,
            this.资源管理器EToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(836, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // selectAllButton
            // 
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(60, 21);
            this.selectAllButton.Text = "全选(&A)";
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // copyButton
            // 
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(60, 21);
            this.copyButton.Text = "复制(&C)";
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // fontButton
            // 
            this.fontButton.Name = "fontButton";
            this.fontButton.Size = new System.Drawing.Size(62, 21);
            this.fontButton.Text = "字体(&N)";
            this.fontButton.Click += new System.EventHandler(this.fontButton_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(12, 28);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(812, 423);
            this.textBox.TabIndex = 1;
            // 
            // font
            // 
            this.font.FontMustExist = true;
            this.font.ShowColor = true;
            // 
            // ofd
            // 
            this.ofd.Filter = "日志文件 (*.log)|*.log|所有文件|*.*";
            this.ofd.SupportMultiDottedExtensions = true;
            this.ofd.Title = "打开 MineCraft 日志";
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(62, 21);
            this.打开OToolStripMenuItem.Text = "打开(&O)";
            this.打开OToolStripMenuItem.Click += new System.EventHandler(this.打开OToolStripMenuItem_Click);
            // 
            // 保存VToolStripMenuItem
            // 
            this.保存VToolStripMenuItem.Name = "保存VToolStripMenuItem";
            this.保存VToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.保存VToolStripMenuItem.Text = "保存(&S)";
            this.保存VToolStripMenuItem.Click += new System.EventHandler(this.保存VToolStripMenuItem_Click);
            // 
            // 资源管理器EToolStripMenuItem
            // 
            this.资源管理器EToolStripMenuItem.Name = "资源管理器EToolStripMenuItem";
            this.资源管理器EToolStripMenuItem.Size = new System.Drawing.Size(95, 21);
            this.资源管理器EToolStripMenuItem.Text = "资源管理器(&E)";
            this.资源管理器EToolStripMenuItem.Click += new System.EventHandler(this.资源管理器EToolStripMenuItem_Click);
            // 
            // logViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 463);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.menuStrip);
            this.Name = "logViewer";
            this.ShowIcon = false;
            this.Text = "日志查看器";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyButton;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ToolStripMenuItem selectAllButton;
        private System.Windows.Forms.ToolStripMenuItem fontButton;
        private System.Windows.Forms.FontDialog font;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存VToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 资源管理器EToolStripMenuItem;
    }
}