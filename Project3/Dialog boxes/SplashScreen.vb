Public NotInheritable Class SplashScreen


    Public Sub setProgress()
        If ProgressBar1.Value < ProgressBar1.Maximum Then
            Me.ProgressBar1.Value += 5
        End If
    End Sub
    Public Sub resetProgressbar()
        Me.ProgressBar1.Value = 0
    End Sub
End Class