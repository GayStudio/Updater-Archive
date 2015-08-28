Imports System.IO

Public Class UpdateHelper
    Declare Function TerminateProcess Lib "coredll.dll" (ByVal processIdOrHandle As IntPtr, ByVal exitCode As IntPtr) As Integer

    Dim MyPath As String = IIf(Strings.Right(My.Application.Info.DirectoryPath, 1) = "\", My.Application.Info.DirectoryPath, My.Application.Info.DirectoryPath & "\")

    Private Sub Log(Info As String)
        LogList.Items.Add(Info)
        LogList.SelectedItem = Info
    End Sub

    Private Sub Fail()
        LogList.Items.Add("更新失败")
        ExitBtn.Enabled = True
    End Sub

    Private Sub FileReplace(NewFilePath)
        File.Delete(MyPath & "..\更新器.exe")
        File.Copy(NewFilePath, MyPath & "..\更新器.exe")
    End Sub

    Private Sub UpdateHelper_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Log("更新器已启动")

        Dim Args() As String = System.Environment.GetCommandLineArgs

        If UBound(Args) <= 0 Then
            Log("启动参数错误，更新中止")
            Fail()
            GoTo F
        End If

        Dim FileName As String = Args(0)
        If Strings.Right(FileName, 4) <> ".zip" Then
            Log("文件名错误，更新中止")
            Fail()
            GoTo F
        End If

        Dim FilePath As String = MyPath & FileName

        If File.Exists(FilePath) = False Then
            Log("未找到文件，更新中止")
            Fail()
            GoTo F
        End If

        'ExtractZip(FilePath)
        'Log("文件解压完成")

        Dim PID As Integer
        If UBound(Args) >= 2 Then
            PID = Args(1)
            Try
                TerminateProcess(PID, 0)
            Catch ex As Exception
                Log("终止进程失败，错误的PID")
                Fail()
                GoTo F
            End Try
            Log("已终止MoeCraft更新器进程")
        Else
            Log("未提供MoeCraft更新器进程ID，跳过")
        End If

        'FileReplace(FilePath)
        Log("文件替换完成")

        Log("更新完成，启动MoeCraft更新器并退出更新助手")
        If File.Exists("..\更新器.exe") Then
            Process.Start("..\更新器.exe")
            Application.Exit()
        Else
            Log("启动更新器失败！未知错误")
        End If

F:

    End Sub

    Private Sub ExitBtn_Click(sender As Object, e As EventArgs) Handles ExitBtn.Click
        Application.Exit()
    End Sub
End Class
