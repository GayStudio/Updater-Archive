namespace MCUpdater
{
    partial class regionCalculator
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.typeB = new System.Windows.Forms.RadioButton();
            this.Z = new System.Windows.Forms.TextBox();
            this.typeA = new System.Windows.Forms.RadioButton();
            this.X = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rF = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rZ = new System.Windows.Forms.TextBox();
            this.rX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.getMcrInfo = new System.Windows.Forms.Button();
            this.getPosInfo = new System.Windows.Forms.Button();
            this.stusUrl = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.kenvixUrl = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.typeB);
            this.groupBox1.Controls.Add(this.Z);
            this.groupBox1.Controls.Add(this.typeA);
            this.groupBox1.Controls.Add(this.X);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(242, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "坐标信息";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "坐标类型";
            // 
            // typeB
            // 
            this.typeB.AutoSize = true;
            this.typeB.Location = new System.Drawing.Point(159, 63);
            this.typeB.Name = "typeB";
            this.typeB.Size = new System.Drawing.Size(74, 21);
            this.typeB.TabIndex = 6;
            this.typeB.TabStop = true;
            this.typeB.Text = "地域坐标";
            this.typeB.UseVisualStyleBackColor = true;
            // 
            // Z
            // 
            this.Z.Location = new System.Drawing.Point(158, 30);
            this.Z.Name = "Z";
            this.Z.Size = new System.Drawing.Size(69, 23);
            this.Z.TabIndex = 5;
            // 
            // typeA
            // 
            this.typeA.AutoSize = true;
            this.typeA.Checked = true;
            this.typeA.Location = new System.Drawing.Point(78, 63);
            this.typeA.Name = "typeA";
            this.typeA.Size = new System.Drawing.Size(74, 21);
            this.typeA.TabIndex = 4;
            this.typeA.TabStop = true;
            this.typeA.Text = "区块坐标";
            this.typeA.UseVisualStyleBackColor = true;
            // 
            // X
            // 
            this.X.Location = new System.Drawing.Point(38, 30);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(69, 23);
            this.X.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Z";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rF);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.rZ);
            this.groupBox2.Controls.Add(this.rX);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(260, 13);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(242, 94);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "区域文件信息";
            // 
            // rF
            // 
            this.rF.Location = new System.Drawing.Point(78, 62);
            this.rF.Name = "rF";
            this.rF.ReadOnly = true;
            this.rF.Size = new System.Drawing.Size(149, 23);
            this.rF.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "区域文件";
            // 
            // rZ
            // 
            this.rZ.Location = new System.Drawing.Point(158, 30);
            this.rZ.Name = "rZ";
            this.rZ.Size = new System.Drawing.Size(69, 23);
            this.rZ.TabIndex = 5;
            // 
            // rX
            // 
            this.rX.Location = new System.Drawing.Point(38, 30);
            this.rX.Name = "rX";
            this.rX.Size = new System.Drawing.Size(69, 23);
            this.rX.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Z";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "X";
            // 
            // getMcrInfo
            // 
            this.getMcrInfo.Location = new System.Drawing.Point(12, 114);
            this.getMcrInfo.Name = "getMcrInfo";
            this.getMcrInfo.Size = new System.Drawing.Size(242, 36);
            this.getMcrInfo.TabIndex = 9;
            this.getMcrInfo.Text = "用坐标计算区域文件信息 (&M)";
            this.getMcrInfo.UseVisualStyleBackColor = true;
            this.getMcrInfo.Click += new System.EventHandler(this.getMcrInfo_Click);
            // 
            // getPosInfo
            // 
            this.getPosInfo.Location = new System.Drawing.Point(260, 114);
            this.getPosInfo.Name = "getPosInfo";
            this.getPosInfo.Size = new System.Drawing.Size(242, 36);
            this.getPosInfo.TabIndex = 10;
            this.getPosInfo.Text = "用区域文件信息计算坐标信息 (&P)";
            this.getPosInfo.UseVisualStyleBackColor = true;
            this.getPosInfo.Click += new System.EventHandler(this.getPosInfo_Click);
            // 
            // stusUrl
            // 
            this.stusUrl.ActiveLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.stusUrl.AutoSize = true;
            this.stusUrl.BackColor = System.Drawing.Color.Transparent;
            this.stusUrl.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.stusUrl.Location = new System.Drawing.Point(109, 159);
            this.stusUrl.Name = "stusUrl";
            this.stusUrl.Size = new System.Drawing.Size(113, 17);
            this.stusUrl.TabIndex = 17;
            this.stusUrl.TabStop = true;
            this.stusUrl.Tag = "";
            this.stusUrl.Text = "StusGame GROUP";
            this.stusUrl.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(92, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "@";
            // 
            // kenvixUrl
            // 
            this.kenvixUrl.ActiveLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.kenvixUrl.AutoSize = true;
            this.kenvixUrl.BackColor = System.Drawing.Color.Transparent;
            this.kenvixUrl.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.kenvixUrl.Location = new System.Drawing.Point(51, 159);
            this.kenvixUrl.Name = "kenvixUrl";
            this.kenvixUrl.Size = new System.Drawing.Size(45, 17);
            this.kenvixUrl.TabIndex = 15;
            this.kenvixUrl.TabStop = true;
            this.kenvixUrl.Tag = "";
            this.kenvixUrl.Text = "Kenvix";
            this.kenvixUrl.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "作者：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(320, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 17);
            this.label9.TabIndex = 18;
            this.label9.Text = "区域文件计算器";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(418, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 17);
            this.label10.TabIndex = 19;
            this.label10.Text = "V1.0";
            // 
            // regionCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 185);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.stusUrl);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.kenvixUrl);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.getPosInfo);
            this.Controls.Add(this.getMcrInfo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "regionCalculator";
            this.ShowIcon = false;
            this.Text = "区域文件 (MC Region) 计算器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton typeB;
        private System.Windows.Forms.TextBox Z;
        private System.Windows.Forms.RadioButton typeA;
        private System.Windows.Forms.TextBox X;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox rF;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox rZ;
        private System.Windows.Forms.TextBox rX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button getMcrInfo;
        private System.Windows.Forms.Button getPosInfo;
        private System.Windows.Forms.LinkLabel stusUrl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel kenvixUrl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}