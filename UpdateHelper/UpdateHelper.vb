Imports System.IO

Public Class UpdateHelper
    Declare Function TerminateProcess Lib "coredll.dll" (ByVal processIdOrHandle As IntPtr, ByVal exitCode As IntPtr) As Integer

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
            'Application.Exit()
        End If

        Dim FileName As String = Args(0)
        If Strings.Right(FileName, 4) <> ".zip" Then
            Log("文件名错误，更新中止")
            'Application.Exit()
        End If

        Dim FilePath As String = MyPath & FileName

        If File.Exists(FilePath) = False Then
            Log("未找到文件，更新中止")
            'Application.Exit()
        End If

        'ExtractZip(FilePath)
        'Log("文件解压完成")

        Dim Kill As Boolean = True
        Dim PID As Integer
        If UBound(Args) < 2 Then
            Kill = False
        End If
        If Kill Then
            PID = Args(1)
            TerminateProcess(PID, 0)
            Log("已终止MoeCraft更新器进程")
        Else
            Log("未提供MoeCraft更新器进程ID，跳过")
        End If

        'FileReplace(FilePath)
        Log("文件替换完成")

        Log("更新完成，启动MoeCraft更新器并退出更新助手")

        'Application.Exit()

    End Sub
End Class
