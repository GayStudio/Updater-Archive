namespace MCUpdater
{
    partial class EX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EX));
            this.EXMsg = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendMail = new System.Windows.Forms.Button();
            this.openBlog = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.openForum = new System.Windows.Forms.Button();
            this.copy = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EXMsg
            // 
            this.EXMsg.Location = new System.Drawing.Point(14, 53);
            this.EXMsg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EXMsg.Multiline = true;
            this.EXMsg.Name = "EXMsg";
            this.EXMsg.ReadOnly = true;
            this.EXMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.EXMsg.Size = new System.Drawing.Size(639, 322);
            this.EXMsg.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "MoeCraft 更新程序遇到了无法处理的异常，操作已取消。";
            // 
            // sendMail
            // 
            this.sendMail.Location = new System.Drawing.Point(143, 383);
            this.sendMail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sendMail.Name = "sendMail";
            this.sendMail.Size = new System.Drawing.Size(123, 33);
            this.sendMail.TabIndex = 2;
            this.sendMail.Text = "发送邮件 (&M)";
            this.sendMail.UseVisualStyleBackColor = true;
            this.sendMail.Click += new System.EventHandler(this.button1_Click);
            // 
            // openBlog
            // 
            this.openBlog.Location = new System.Drawing.Point(14, 382);
            this.openBlog.Name = "openBlog";
            this.openBlog.Size = new System.Drawing.Size(123, 34);
            this.openBlog.TabIndex = 3;
            this.openBlog.Text = "Kenvix 的博客 (&K)";
            this.openBlog.UseVisualStyleBackColor = true;
            this.openBlog.Click += new System.EventHandler(this.openBlog_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(356, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "请将以下错误信息复制并发送给我我们，以便于我们修复这些问题";
            // 
            // openForum
            // 
            this.openForum.Location = new System.Drawing.Point(272, 382);
            this.openForum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.openForum.Name = "openForum";
            this.openForum.Size = new System.Drawing.Size(123, 34);
            this.openForum.TabIndex = 4;
            this.openForum.Text = "MoeClub (&O)";
            this.openForum.UseVisualStyleBackColor = true;
            this.openForum.Click += new System.EventHandler(this.openForum_Click);
            // 
            // copy
            // 
            this.copy.Location = new System.Drawing.Point(401, 382);
            this.copy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(123, 34);
            this.copy.TabIndex = 5;
            this.copy.Text = "复制 (&C)";
            this.copy.UseVisualStyleBackColor = true;
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(530, 383);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "关闭 (&L)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // EX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 429);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.copy);
            this.Controls.Add(this.openForum);
            this.Controls.Add(this.openBlog);
            this.Controls.Add(this.sendMail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EXMsg);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(622, 468);
            this.Name = "EX";
            this.Text = "MoeCraft 更新程序遇到了无法处理的异常";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox EXMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendMail;
        private System.Windows.Forms.Button openBlog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button openForum;
        private System.Windows.Forms.Button copy;
        private System.Windows.Forms.Button button1;
    }
}