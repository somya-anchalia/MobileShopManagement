Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class emp_forgetpass
    Dim flag As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If TextBox1.Text = "" Then
            MsgBox("Please provide username")

        ElseIf TextBox1.Text <> "" Then

            Dim mongoDB As MongoServer = MongoServer.Create()
            Dim Flag = 0
            mongoDB.Connect()
            Dim db = mongoDB.GetDatabase("mobile_shop")

            Dim collection = db.GetCollection(Of BsonDocument)("emp")
            Dim query As QueryDocument = New QueryDocument("username", TextBox1.Text)

            For Each item As BsonDocument In collection.FindAll
                Dim username As BsonElement = item.GetElement("username")
                Dim Hint As BsonElement = item.GetElement("hint")


                If (TextBox1.Text = username.Value) Then

                    Label3.Text = Hint.ToString
                    Flag = 1

                End If


            Next

            If Flag <> 1 Then
                MsgBox("Please enter Existing Username", MsgBoxStyle.Critical)

                Label3.Text = ""
                TextBox1.Text = ""

            End If





        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()

    End Sub

    Private Sub TextBox1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar >= ":" And e.KeyChar <= "Z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "0" And e.KeyChar <= "9" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

End Class