<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FirstPosition = New System.Windows.Forms.GroupBox()
        Me.FF = New System.Windows.Forms.TextBox()
        Me.FZ = New System.Windows.Forms.TextBox()
        Me.FX = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SecondPosition = New System.Windows.Forms.GroupBox()
        Me.SF = New System.Windows.Forms.TextBox()
        Me.SZ = New System.Windows.Forms.TextBox()
        Me.SX = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.S = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Result = New System.Windows.Forms.GroupBox()
        Me.ansout = New System.Windows.Forms.Label()
        Me.howto = New System.Windows.Forms.TextBox()
        Me.moeUrl = New System.Windows.Forms.LinkLabel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.canUrl = New System.Windows.Forms.LinkLabel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.FirstPosition.SuspendLayout()
        Me.SecondPosition.SuspendLayout()
        Me.Result.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 17)
        Me.Label1.TabIndex = 0
        '
        'FirstPosition
        '
        Me.FirstPosition.Controls.Add(Me.FF)
        Me.FirstPosition.Controls.Add(Me.FZ)
        Me.FirstPosition.Controls.Add(Me.FX)
        Me.FirstPosition.Controls.Add(Me.Label4)
        Me.FirstPosition.Controls.Add(Me.Label3)
        Me.FirstPosition.Controls.Add(Me.Label2)
        Me.FirstPosition.Location = New System.Drawing.Point(17, 13)
        Me.FirstPosition.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FirstPosition.Name = "FirstPosition"
        Me.FirstPosition.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FirstPosition.Size = New System.Drawing.Size(168, 154)
        Me.FirstPosition.TabIndex = 1
        Me.FirstPosition.TabStop = False
        Me.FirstPosition.Text = "第一次坐标"
        '
        'FF
        '
        Me.FF.Location = New System.Drawing.Point(64, 103)
        Me.FF.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FF.Name = "FF"
        Me.FF.Size = New System.Drawing.Size(88, 23)
        Me.FF.TabIndex = 1
        '
        'FZ
        '
        Me.FZ.Location = New System.Drawing.Point(64, 67)
        Me.FZ.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FZ.Name = "FZ"
        Me.FZ.Size = New System.Drawing.Size(88, 23)
        Me.FZ.TabIndex = 1
        '
        'FX
        '
        Me.FX.Location = New System.Drawing.Point(64, 28)
        Me.FX.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FX.Name = "FX"
        Me.FX.Size = New System.Drawing.Size(88, 23)
        Me.FX.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 17)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "F角度"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Z坐标"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "X坐标"
        '
        'SecondPosition
        '
        Me.SecondPosition.Controls.Add(Me.SF)
        Me.SecondPosition.Controls.Add(Me.SZ)
        Me.SecondPosition.Controls.Add(Me.SX)
        Me.SecondPosition.Controls.Add(Me.Label5)
        Me.SecondPosition.Controls.Add(Me.Label6)
        Me.SecondPosition.Controls.Add(Me.S)
        Me.SecondPosition.Location = New System.Drawing.Point(17, 175)
        Me.SecondPosition.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SecondPosition.Name = "SecondPosition"
        Me.SecondPosition.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SecondPosition.Size = New System.Drawing.Size(171, 154)
        Me.SecondPosition.TabIndex = 1
        Me.SecondPosition.TabStop = False
        Me.SecondPosition.Text = "第二次坐标"
        '
        'SF
        '
        Me.SF.Location = New System.Drawing.Point(64, 103)
        Me.SF.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SF.Name = "SF"
        Me.SF.Size = New System.Drawing.Size(88, 23)
        Me.SF.TabIndex = 1
        '
        'SZ
        '
        Me.SZ.Location = New System.Drawing.Point(64, 67)
        Me.SZ.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SZ.Name = "SZ"
        Me.SZ.Size = New System.Drawing.Size(88, 23)
        Me.SZ.TabIndex = 1
        '
        'SX
        '
        Me.SX.Location = New System.Drawing.Point(64, 28)
        Me.SX.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SX.Name = "SX"
        Me.SX.Size = New System.Drawing.Size(88, 23)
        Me.SX.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 108)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 17)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "F角度"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 71)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 17)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Z坐标"
        '
        'S
        '
        Me.S.AutoSize = True
        Me.S.Location = New System.Drawing.Point(9, 37)
        Me.S.Name = "S"
        Me.S.Size = New System.Drawing.Size(40, 17)
        Me.S.TabIndex = 0
        Me.S.Text = "X坐标"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(17, 338)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(171, 33)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "计算 (&C)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Result
        '
        Me.Result.Controls.Add(Me.ansout)
        Me.Result.Location = New System.Drawing.Point(196, 175)
        Me.Result.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Result.Name = "Result"
        Me.Result.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Result.Size = New System.Drawing.Size(298, 154)
        Me.Result.TabIndex = 3
        Me.Result.TabStop = False
        Me.Result.Text = "计算结果"
        '
        'ansout
        '
        Me.ansout.AutoSize = True
        Me.ansout.Location = New System.Drawing.Point(17, 25)
        Me.ansout.Name = "ansout"
        Me.ansout.Size = New System.Drawing.Size(0, 17)
        Me.ansout.TabIndex = 0
        '
        'howto
        '
        Me.howto.Location = New System.Drawing.Point(196, 14)
        Me.howto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.howto.Multiline = True
        Me.howto.Name = "howto"
        Me.howto.ReadOnly = True
        Me.howto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.howto.Size = New System.Drawing.Size(298, 153)
        Me.howto.TabIndex = 4
        Me.howto.Text = "使用姿势：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "先按F3打开调试界面， 在原地扔出一颗末影之眼， 同时十字准心对准扔出去的末影之眼， 记录调试信息的X、Z、F（-180~180）值 " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "然后往左或" &
    "右（垂直与你所在的方向）移动30-50格，重复一次刚才的动作" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "将两次抛出的记录填进程序里， 点击计算即可" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "第一组坐标为精确值，第二、三组为近似值（偏差较大）" &
    ""
        '
        'moeUrl
        '
        Me.moeUrl.ActiveLinkColor = System.Drawing.SystemColors.MenuHighlight
        Me.moeUrl.AutoSize = True
        Me.moeUrl.BackColor = System.Drawing.Color.Transparent
        Me.moeUrl.LinkColor = System.Drawing.SystemColors.MenuHighlight
        Me.moeUrl.Location = New System.Drawing.Point(431, 346)
        Me.moeUrl.Name = "moeUrl"
        Me.moeUrl.Size = New System.Drawing.Size(63, 17)
        Me.moeUrl.TabIndex = 17
        Me.moeUrl.TabStop = True
        Me.moeUrl.Tag = ""
        Me.moeUrl.Text = "MoeCraft"
        Me.moeUrl.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(416, 346)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(20, 17)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "@"
        '
        'canUrl
        '
        Me.canUrl.ActiveLinkColor = System.Drawing.SystemColors.MenuHighlight
        Me.canUrl.AutoSize = True
        Me.canUrl.BackColor = System.Drawing.Color.Transparent
        Me.canUrl.LinkColor = System.Drawing.SystemColors.MenuHighlight
        Me.canUrl.Location = New System.Drawing.Point(347, 346)
        Me.canUrl.Name = "canUrl"
        Me.canUrl.Size = New System.Drawing.Size(75, 17)
        Me.canUrl.TabIndex = 15
        Me.canUrl.TabStop = True
        Me.canUrl.Tag = ""
        Me.canUrl.Text = "CancerGary"
        Me.canUrl.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(307, 346)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 17)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "作者："
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 380)
        Me.Controls.Add(Me.moeUrl)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.canUrl)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.howto)
        Me.Controls.Add(Me.Result)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.SecondPosition)
        Me.Controls.Add(Me.FirstPosition)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Form1"
        Me.ShowIcon = False
        Me.Text = "要塞坐标计算器"
        Me.FirstPosition.ResumeLayout(False)
        Me.FirstPosition.PerformLayout()
        Me.SecondPosition.ResumeLayout(False)
        Me.SecondPosition.PerformLayout()
        Me.Result.ResumeLayout(False)
        Me.Result.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents FirstPosition As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents FF As TextBox
    Friend WithEvents FZ As TextBox
    Friend WithEvents FX As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents SecondPosition As GroupBox
    Friend WithEvents SF As TextBox
    Friend WithEvents SZ As TextBox
    Friend WithEvents SX As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents S As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Result As GroupBox
    Friend WithEvents ansout As Label
    Friend WithEvents howto As TextBox
    Private WithEvents moeUrl As LinkLabel
    Private WithEvents Label7 As Label
    Private WithEvents canUrl As LinkLabel
    Private WithEvents Label8 As Label
End Class
