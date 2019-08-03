Imports MongoDB.Bson
Imports MongoDB.Driver


Public Class admin_login
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MessageBox.Show("Invalid Username and Password")
        Else
            Dim mongoDB As MongoServer = MongoServer.Create()
            Dim Flag = 0 'username & password not matched
            mongoDB.Connect()

            Dim db = mongoDB.GetDatabase("mobile_shop")

            Dim collection = db.GetCollection(Of BsonDocument)("admin")
            Dim query As QueryDocument = New QueryDocument("username", TextBox1.Text)

            For Each item As BsonDocument In collection.FindAll
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
                admin_control.Show()
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
    Private Sub TextBox1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar >= "a" And e.KeyChar <= "z" Then
            e.Handled = False                'e.Handled = False => it means that the only symbols between a AND z are accepted
            '   ElseIf e.KeyChar >= "0" And e.KeyChar <= "9" Then
            '      e.Handled = True          'e.Handled = True => it means that the only symbols between 0 AND 9 are not accepted
            ' ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            '    e.Handled = True
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        admin_forgetpass.Show()
    End Sub
End Class