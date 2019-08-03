Imports MongoDB.Driver
Imports MongoDB.Bson

Public Class modify_device

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        emp_control.Show()
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
        TextBox3.ReadOnly = True
        TextBox4.ReadOnly = False
        TextBox5.ReadOnly = False
        TextBox6.ReadOnly = True
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If TextBox4.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Fill all the records", MsgBoxStyle.OkOnly)
        Else
            Dim mongo As MongoServer = MongoServer.Create()
            Dim update As IMongoUpdate
            Dim query As IMongoQuery
            mongo.Connect()
            Dim db = mongo.GetDatabase("mobile_shop")
            Dim collection = db.GetCollection(Of BsonDocument)("add_device")
         
            query = Builders.Query.EQ("Model", ComboBox3.Text)
            update = Builders.Update.Set("Price", TextBox4.Text).Set("Quantity", TextBox5.Text)
            collection.Update(query, update)
            MsgBox("Record Modified")
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""




        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("add_device")
        If ComboBox3.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Then
            MsgBox("Please Check.")
            mongo.Disconnect()
        Else
            Dim query = New QueryDocument("Type", ComboBox1.Text)
            Dim query1 = New QueryDocument("Model", TextBox3.Text)
            For Each item As BsonDocument In collection.Find(query)
                For Each item2 As BsonDocument In collection.Find(query1)
                    Dim temp As BsonElement = item.GetElement("Type")
                    Dim temp1 As BsonElement = item.GetElement("Model")
                    If ComboBox1.Text = temp.Value And TextBox3.Text = temp1.Value Then
                        flag = 1
                        Dim type As BsonElement = item.GetElement("Type")
                        Dim model As BsonElement = item.GetElement("Model")
                        Dim price As BsonElement = item.GetElement("Price")
                        Dim quantity As BsonElement = item.GetElement("Quantity")
                        Dim specification As BsonElement = item.GetElement("specifications")
                        ComboBox1.Text = type.Value
                        TextBox3.Text = model.Value
                        TextBox4.Text = price.Value
                        TextBox5.Text = quantity.Value
                        TextBox6.Text = specification.Value
                        collection.Remove(query1)
                        MsgBox("Deletion Successful")
                        TextBox3.Text = ""
                        TextBox4.Text = ""
                        TextBox5.Text = ""
                        TextBox6.Text = ""
                    End If
                Next
            Next
        End If
        If (flag = 1) Then

        Else
            MsgBox("Device not found")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
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
                    Dim temp3 As BsonElement = item.GetElement("Quantity")
                    Dim temp4 As BsonElement = item.GetElement("specifications")

                    If ComboBox3.Text = temp1.Value And ComboBox1.Text = temp.Value Then

                        TextBox3.Text = temp1.Value
                        TextBox4.Text = temp2.Value
                        TextBox5.Text = temp3.Value
                        TextBox6.Text = temp4.Value

                        TextBox3.ReadOnly = True
                        TextBox4.ReadOnly = False
                        TextBox5.ReadOnly = False
                        TextBox6.ReadOnly = True
                    End If
                Next

            Next
        End If
    End Sub

End Class