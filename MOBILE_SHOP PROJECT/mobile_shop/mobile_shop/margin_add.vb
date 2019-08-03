Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class margin_add


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("add_margin")
        If TextBox4.Text = "" Or TextBox3.Text = "" Or TextBox2.Text = "" Or TextBox1.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Please Check.")
        Else

            Dim query = New QueryDocument("Type", ComboBox1.Text)
            Dim query1 = New QueryDocument("Model", TextBox4.Text)

            For Each item As BsonDocument In collection.Find(query)

                For Each item1 As BsonDocument In collection.Find(query1)
                    Dim temp As BsonElement = item.GetElement("Type")
                    Dim temp1 As BsonElement = item.GetElement("Model")
                    If ComboBox1.Text = temp.Value And TextBox4.Text = temp1.Value Then
                        flag = 1
                    End If


                Next
            Next
            If flag = 0 Then
                Dim user As BsonDocument = New BsonDocument()
                user.Add("Type", ComboBox1.Text)
                user.Add("Model", TextBox4.Text)
                user.Add("cp", TextBox3.Text)
                user.Add("sp", TextBox2.Text)
                user.Add("margin", TextBox1.Text)
                collection.Insert(user)
                MsgBox("Data successfully entered!")

                ComboBox1.Text = ""
                TextBox4.Text = ""
                TextBox3.Text = ""
                TextBox2.Text = ""
                TextBox1.Text = ""
            Else
                MsgBox("Device all already exist")

            End If
        End If
        mongo.Disconnect()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        admin_control.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox1.Text = TextBox2.Text - TextBox3.Text
        If (TextBox1.Text < 0) Then
            MsgBox("Please Enter Correct Cost Price")
        Else
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("add_device")
        If ComboBox3.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Please Check.")
        Else
            Dim query = New QueryDocument("Type", ComboBox1.Text)
            Dim query1 = New QueryDocument("Model", ComboBox3.Text)
            For Each item As BsonDocument In collection.Find(query)
                For Each item1 As BsonDocument In collection.Find(query1)

                    Dim temp As BsonElement = item.GetElement("Type")
                    Dim temp1 As BsonElement = item.GetElement("Model")
                    Dim temp2 As BsonElement = item.GetElement("Price")

                    If ComboBox3.Text = temp1.Value And ComboBox1.Text = temp.Value Then
                        ComboBox2.Text = temp.Value
                        TextBox4.Text = temp1.Value
                        TextBox2.Text = temp2.Value
                        ComboBox2.Enabled = True
                        TextBox3.ReadOnly = False
                        TextBox4.ReadOnly = True
                        TextBox1.ReadOnly = True

                    End If
                Next

            Next
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox3.Items.Clear()
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("add_device")

        Dim query = New QueryDocument("Type", ComboBox1.Text)
        For Each item As BsonDocument In collection.Find(query)
            Dim temp As BsonElement = item.GetElement("Type")
            Dim temp1 As BsonElement = item.GetElement("Model")
            If ComboBox1.Text = temp.Value Then
                ComboBox3.Items.Add(temp1.Value)
            End If
        Next
    End Sub

    Private Sub margin_add_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox3_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar >= ":" And e.KeyChar <= "Z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "a" And e.KeyChar <= "z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub
End Class
