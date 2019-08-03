Public Class splash_form
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If ProgressBar1.Value <> ProgressBar1.Maximum Then
            ProgressBar1.Value += 100
            Select Case ProgressBar1.Value
                Case 1 To 20 : Label1.Text = "Loading Application..."
                Case 21 To 45 : Label1.Text = "Establishing Database Connection..."
                Case 46 To 75 : Label1.Text = "Loading Database Table..."
                Case 76 To 90 : Label1.Text = "Loading Login Form..."
                Case 91 To 98 : Label1.Text = "Wait..."
                Case 100 : Threading.Thread.Sleep(1000)
            End Select
            Label2.Text = ProgressBar1.Value & "%"
        Else
            Timer1.Enabled = False
            Login.Show()
            Me.Hide()
        End If
    End Sub
End Class
