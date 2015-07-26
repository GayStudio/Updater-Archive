<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateHelper
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
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

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.UpdateProgress = New System.Windows.Forms.ProgressBar()
        Me.LogList = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'UpdateProgress
        '
        Me.UpdateProgress.Location = New System.Drawing.Point(12, 267)
        Me.UpdateProgress.Name = "UpdateProgress"
        Me.UpdateProgress.Size = New System.Drawing.Size(450, 23)
        Me.UpdateProgress.TabIndex = 0
        Me.UpdateProgress.Value = 10
        '
        'LogList
        '
        Me.LogList.FormattingEnabled = True
        Me.LogList.ItemHeight = 17
        Me.LogList.Location = New System.Drawing.Point(12, 42)
        Me.LogList.Name = "LogList"
        Me.LogList.Size = New System.Drawing.Size(450, 208)
        Me.LogList.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "进度："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UpdateHelper
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(474, 311)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LogList)
        Me.Controls.Add(Me.UpdateProgress)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "UpdateHelper"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "正在更新MoeCraft更新器"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UpdateProgress As ProgressBar
    Friend WithEvents LogList As ListBox
    Friend WithEvents Label1 As Label
End Class
