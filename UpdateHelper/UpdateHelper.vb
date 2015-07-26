Imports System.IO

Public Class UpdateHelper
    Private Sub Log(Info As String)
        LogList.Items.Add(Info)
        LogList.SelectedItem = Info
    End Sub

    Private Sub UpdateHelper_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Log("更新器已启动")

        Dim MyPath As String = IIf(Strings.Right(My.Application.Info.DirectoryPath, 1) = "\", My.Application.Info.DirectoryPath, My.Application.Info.DirectoryPath & "\")
        Dim Args() As String = System.Environment.GetCommandLineArgs

        If UBound(Args) <= 0 Then
            Log("启动参数错误，更新中止")
            Application.Exit()
        End If

        Dim FileName As String = Args(0)
        If File.Exists(MyPath & FileName) = False Then

        End If
    End Sub
End Class
