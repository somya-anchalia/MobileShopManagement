Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class add_device


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim mongoDB As MongoServer = MongoServer.Create()
        Dim flag = 0
        mongoDB.Connect()

        Dim db = mongoDB.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("add_device")
        If ComboBox2.Text = "" Or TextBox3.Text = "" Or NumericUpDown1.Text = "" Or TextBox1.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Please Check.")
        Else

            Dim query = New QueryDocument("Type", ComboBox1.Text)
            Dim query1 = New QueryDocument("Model", ComboBox2.Text)

            For Each item As BsonDocument In collection.Find(query)

                For Each item1 As BsonDocument In collection.Find(query1)
                    Dim temp As BsonElement = item.GetElement("Type")
                    Dim temp1 As BsonElement = item.GetElement("Model")
                    If ComboBox1.Text = temp.Value And ComboBox2.Text = temp1.Value Then
                        flag = 1
                    End If


                Next
            Next
            If flag = 0 Then
                Dim user As BsonDocument = New BsonDocument()
                user.Add("Type", ComboBox1.Text)
                user.Add("Model", ComboBox2.Text)
                user.Add("Price", TextBox3.Text)
                user.Add("Quantity", NumericUpDown1.Text)
                user.Add("specifications", TextBox1.Text)
                user.Add("Image", OpenFileDialog1.FileName)
                collection.Insert(user)
                MsgBox("Data successfully entered!")

                ComboBox1.Text = ""
                ComboBox2.Text = ""
                TextBox3.Text = ""
                NumericUpDown1.Text = ""
                TextBox1.Text = ""
                PictureBox3.Image = Image.FromFile("D:\MOBILE_SHOP PROJECT\mobile_shop\images\adddd.png")
            Else
                MsgBox("Device all already exist")

            End If
        End If
        mongoDB.Disconnect()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        emp_control.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        NumericUpDown1.Text = ""
        TextBox3.Text = ""
        ComboBox2.Text = ""
        PictureBox3.Image = Image.FromFile("D:\MOBILE_SHOP PROJECT\mobile_shop\images\adddd.png")
    End Sub

    Private Sub PictureBox3_VisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.VisibleChanged
        PictureBox3.Image = Image.FromFile("D:\MOBILE_SHOP PROJECT\mobile_shop\images\adddd.png")
    End Sub

    Private Sub btnCamera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            OpenFileDialog1.Filter = "jpg (*.jpg)|*.jpg| png(*.png)|*.png"
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                PictureBox3.Image = Image.FromFile(OpenFileDialog1.FileName)

            End If
        Catch ex As Exception
            MsgBox("Error : " & ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical)
            Me.Close()
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox2.Items.Clear()
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        If ComboBox1.Text = "mobile" Then

            Dim collection = db.GetCollection(Of BsonDocument)("mob")

            Dim query = New QueryDocument("type", ComboBox1.Text)
            For Each item As BsonDocument In collection.Find(query)
                Dim temp As BsonElement = item.GetElement("type")
                Dim temp1 As BsonElement = item.GetElement("mobile")
                If ComboBox1.Text = temp.Value Then
                    ComboBox2.Items.Add(temp1.Value)
                End If
            Next
        End If
        If ComboBox1.Text = "accessories" Then
            Dim collection1 = db.GetCollection(Of BsonDocument)("access")

            Dim query1 = New QueryDocument("type", ComboBox1.Text)
            For Each item As BsonDocument In collection1.Find(query1)
                Dim temp As BsonElement = item.GetElement("type")
                Dim temp1 As BsonElement = item.GetElement("accessories")
                If ComboBox1.Text = temp.Value Then
                    ComboBox2.Items.Add(temp1.Value)
                End If
            Next
        End If

    End Sub

    

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar >= "0" And e.KeyChar <= "9" Then
            e.Handled = False
        ElseIf e.KeyChar >= "a" And e.KeyChar <= "z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

    
End Class

