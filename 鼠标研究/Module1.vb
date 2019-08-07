Imports System.Drawing
Imports System.Windows.Forms

Module Moudule1
    Public Declare Function GetWindowDC Lib "user32" (ByVal hwnd As Long) As Long '获得整个屏幕绘制
    Public Declare Function GetDesktopWindow Lib "user32" () As Long '获得整个桌面绘制
    Public Declare Function SetCursorPos Lib "user32" (ByVal x As Integer, ByVal y As Integer) As Integer '设置鼠标位置。
    Sub Main()
        Dim where
        Dim newthread As New Threading.Thread(AddressOf Bug)
        newthread.Start()
        Do
            where = System.Windows.Forms.Control.MousePosition '获得当前鼠标位置
            SetCursorPos(CDbl(where.x) + 5 * (-1) ^ Int(1 + 2 * Rnd()), CDbl(where.y) + 5 * (-1) ^ Int(1 + 2 * Rnd())) 'Set mouse position
            Threading.Thread.Sleep(50) 'stop 0.05 second
        Loop
    End Sub
    Public Sub Bug()
again:
        Dim ico As Bitmap = My.Resources.Resource1.tast.ToBitmap 'add resources in exe
        Dim screen As Graphics = Graphics.FromHdc(GetWindowDC(GetDesktopWindow())) 'get desktop，还要每一个窗口
        Do
            Try
                screen.DrawImage(ico, Windows.Forms.Control.MousePosition)
                Threading.Thread.Sleep(1)
            Catch ex As Exception '如果超出内存则运行下面的
                screen.Dispose() '释放内存
                Threading.Thread.Sleep(10000) 'stop 10 second
                GoTo again 'jump to again
            End Try
        Loop
    End Sub
End Module