Public Class Form1

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click, Label4.Click, Label6.Click, Label5.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If FX.Text Is "" Or FZ.Text Is "" Or FF.Text Is "" Or SX.Text Is "" Or SZ.Text Is "" Or SF.Text Is "" Then
            MessageBox.Show("你必须填写所有信息才能进行计算", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Try
            '根据原始数据求出第一个要塞的地址
            Const pi = 3.14159265358979
            Dim k1, k2 As Double
            Dim answer1x, answer1z As Double
            k1 = Math.Tan(FF.Text * (-1) / 180 * pi)
            k2 = Math.Tan(SF.Text * (-1) / 180 * pi)
            answer1z = (k1 * FZ.Text - k2 * SZ.Text + SX.Text - FX.Text) / (k1 - k2)
            answer1x = k1 * (answer1z - FZ.Text) + FX.Text
            '根据第一个要塞的地址求出其他两个的地址
            Dim answer2x, answer2z As Double
            Dim answer3x, answer3z As Double
            Dim size = Math.Sqrt(answer1x * answer1x + answer1z * answer1z)
            Dim alpha As Double
            alpha = Math.Atan(answer1x / answer1z)
            answer2z = size * Math.Cos(alpha + pi * 2 / 3)
            answer2x = size * Math.Sin(alpha + pi * 2 / 3)
            answer3z = size * Math.Cos(alpha + pi * 4 / 3)
            answer3x = size * Math.Sin(alpha + pi * 4 / 3)
            ansout.Text = "X:" & answer1x & vbCrLf & "Y:" & answer1z & vbCrLf
            ansout.Text = ansout.Text & "X:" & answer2x & vbCrLf & "Y:" & answer2z & vbCrLf
            ansout.Text = ansout.Text & "X:" & answer3x & vbCrLf & "Y:" & answer3z & vbCrLf
        Catch ex As Exception
            MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub url(url)
        Try
            Process.Start(url)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub canUrl_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles canUrl.LinkClicked
        url("http://xcgx.me")
    End Sub

    Private Sub moeUrl_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles moeUrl.LinkClicked
        url("http://www.moecraft.net")
    End Sub
End Class
