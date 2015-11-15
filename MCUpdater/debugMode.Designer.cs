namespace MCUpdater
{
    partial class debugMode
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
            this.text = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.复制CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全选AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空LToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.结束进程KToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // text
            // 
            this.text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.text.Location = new System.Drawing.Point(0, 25);
            this.text.Multiline = true;
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text.Size = new System.Drawing.Size(866, 453);
            this.text.TabIndex = 0;
            this.text.TextChanged += new System.EventHandler(this.text_TextChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制CToolStripMenuItem,
            this.全选AToolStripMenuItem,
            this.清空LToolStripMenuItem,
            this.结束进程KToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(866, 25);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // 复制CToolStripMenuItem
            // 
            this.复制CToolStripMenuItem.Name = "复制CToolStripMenuItem";
            this.复制CToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.复制CToolStripMenuItem.Text = "复制(&C)";
            this.复制CToolStripMenuItem.Click += new System.EventHandler(this.复制CToolStripMenuItem_Click);
            // 
            // 全选AToolStripMenuItem
            // 
            this.全选AToolStripMenuItem.Name = "全选AToolStripMenuItem";
            this.全选AToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.全选AToolStripMenuItem.Text = "全选(&A)";
            this.全选AToolStripMenuItem.Click += new System.EventHandler(this.全选AToolStripMenuItem_Click);
            // 
            // 清空LToolStripMenuItem
            // 
            this.清空LToolStripMenuItem.Name = "清空LToolStripMenuItem";
            this.清空LToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.清空LToolStripMenuItem.Text = "清空(&L)";
            this.清空LToolStripMenuItem.Click += new System.EventHandler(this.清空LToolStripMenuItem_Click);
            // 
            // 结束进程KToolStripMenuItem
            // 
            this.结束进程KToolStripMenuItem.Name = "结束进程KToolStripMenuItem";
            this.结束进程KToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.结束进程KToolStripMenuItem.Text = "结束进程(&K)";
            this.结束进程KToolStripMenuItem.Click += new System.EventHandler(this.结束进程KToolStripMenuItem_Click);
            // 
            // debugMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(866, 478);
            this.Controls.Add(this.text);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "debugMode";
            this.Text = "MineCraft - Debug Mode - Standard Output";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 复制CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全选AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空LToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 结束进程KToolStripMenuItem;
    }
}