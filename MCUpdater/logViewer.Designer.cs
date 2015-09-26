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
            this.listButton = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileButton = new System.Windows.Forms.ToolStripMenuItem();
            this.explorerButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.关闭LToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllButton = new System.Windows.Forms.ToolStripMenuItem();
            this.copyButton = new System.Windows.Forms.ToolStripMenuItem();
            this.fontButton = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox = new System.Windows.Forms.TextBox();
            this.font = new System.Windows.Forms.FontDialog();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listButton,
            this.selectAllButton,
            this.copyButton,
            this.fontButton});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(836, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // listButton
            // 
            this.listButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileButton,
            this.explorerButton,
            this.toolStripSeparator1,
            this.saveButton,
            this.toolStripSeparator2,
            this.关闭LToolStripMenuItem});
            this.listButton.Name = "listButton";
            this.listButton.Size = new System.Drawing.Size(58, 21);
            this.listButton.Text = "文件(&F)";
            this.listButton.Click += new System.EventHandler(this.文件FToolStripMenuItem_Click);
            // 
            // openFileButton
            // 
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(199, 22);
            this.openFileButton.Text = "打开文件(&O)";
            this.openFileButton.Click += new System.EventHandler(this.sToolStripMenuItem_Click);
            // 
            // explorerButton
            // 
            this.explorerButton.Name = "explorerButton";
            this.explorerButton.Size = new System.Drawing.Size(199, 22);
            this.explorerButton.Text = "在资源管理器中查看(&E)";
            this.explorerButton.Click += new System.EventHandler(this.explorerButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
            // 
            // saveButton
            // 
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(199, 22);
            this.saveButton.Text = "保存(&A)";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(196, 6);
            // 
            // 关闭LToolStripMenuItem
            // 
            this.关闭LToolStripMenuItem.Name = "关闭LToolStripMenuItem";
            this.关闭LToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.关闭LToolStripMenuItem.Text = "关闭(&L)";
            this.关闭LToolStripMenuItem.Click += new System.EventHandler(this.关闭LToolStripMenuItem_Click);
            // 
            // selectAllButton
            // 
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(59, 21);
            this.selectAllButton.Text = "全选(&S)";
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
            this.font.Apply += new System.EventHandler(this.font_Apply);
            // 
            // ofd
            // 
            this.ofd.Filter = "日志文件 (*.log)|*.log|所有文件|*.*";
            this.ofd.SupportMultiDottedExtensions = true;
            this.ofd.Title = "打开 MineCraft 日志";
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
        private System.Windows.Forms.ToolStripMenuItem listButton;
        private System.Windows.Forms.ToolStripMenuItem openFileButton;
        private System.Windows.Forms.ToolStripMenuItem explorerButton;
        private System.Windows.Forms.ToolStripMenuItem 关闭LToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontButton;
        private System.Windows.Forms.FontDialog font;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}