Imports MongoDB.Bson
Imports MongoDB.Driver

Public Class emp_login

    Dim watermark As String = "Yes"
    Dim watermark1 As String = "Yes"

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Invalid Username and Password", MsgBoxStyle.SystemModal, "System warning")
        Else
            Dim mongoDB As MongoServer = MongoServer.Create()
            Dim Flag = 0
            mongoDB.Connect()

            Dim db = mongoDB.GetDatabase("mobile_shop")

            Dim collection = db.GetCollection(Of BsonDocument)("emp")
            
            For Each item As BsonDocument In collection.FindAll()
                Dim username As BsonElement = item.GetElement("username")
                Dim pass As BsonElement = item.GetElement("password")

                If (TextBox1.Text = username.Value) And (TextBox2.Text = pass.Value) Then
                    Flag = 1


                End If
            Next

            If Flag <> 1 Then


                MsgBox("Please enter correct User ID & Password")
            Else

                MsgBox("Login Successful!!!")
                emp_control.Show()
                Me.Hide()
            End If
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        Me.Close()
        Login.Show()
    End Sub


    Private Sub TextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Enter
        If watermark = "Yes" Then
            TextBox1.Clear()
            TextBox1.ForeColor = Color.Black

            watermark = "No"
        Else

        End If
    End Sub

    Private Sub TextBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Enter
        If watermark1 = "Yes" Then
            TextBox2.Clear()
            TextBox2.ForeColor = Color.Black
            TextBox2.PasswordChar = "*"
            watermark = "No"
        End If
    End Sub


    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        emp_forgetpass.Show()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar >= "a" And e.KeyChar <= "z" Then
            e.Handled = False
        ElseIf e.KeyChar >= "0" And e.KeyChar <= "9" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

End Class