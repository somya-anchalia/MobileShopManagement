Imports MongoDB.Driver
Imports MongoDB.Bson


Public Class margin_view
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        admin_control.Show()
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox2.Items.Clear()
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("add_margin")

        Dim query = New QueryDocument("Type", ComboBox1.Text)
        For Each item As BsonDocument In collection.Find(query)
            Dim temp As BsonElement = item.GetElement("Type")
            Dim temp1 As BsonElement = item.GetElement("Model")
            If ComboBox1.Text = temp.Value Then
                ComboBox2.Items.Add(temp1.Value)
            End If
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("add_margin")
        If ComboBox2.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Please Check.")
        Else
            Dim query = New QueryDocument("Type", ComboBox1.Text)
            Dim query1 = New QueryDocument("Model", ComboBox2.Text)
            For Each item As BsonDocument In collection.Find(query)
                For Each item1 As BsonDocument In collection.Find(query1)

                    Dim temp As BsonElement = item.GetElement("Type")
                    Dim temp1 As BsonElement = item.GetElement("Model")

                    If ComboBox2.Text = temp1.Value And ComboBox1.Text = temp.Value Then
                        Dim ob As ListViewItem
                        ListView1.Items.Clear()

                        ob = New ListViewItem(item("Type"))
                        ob.SubItems.Add(item("Model"))
                        ob.SubItems.Add(item("cp"))
                        ob.SubItems.Add(item("sp"))
                        ob.SubItems.Add(item("margin"))
                        ListView1.Items.Add(ob)

                    End If
                Next

            Next
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ListView1.Items.Clear()
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("add_margin")
        If ComboBox1.Text = "" Then
            MsgBox("Please Check.")
        Else
            Dim query = New QueryDocument("Type", ComboBox1.Text)
            For Each item As BsonDocument In collection.Find(query)

                    Dim temp As BsonElement = item.GetElement("Type")

                If ComboBox1.Text = temp.Value Then
                    Dim ob As ListViewItem


                    ob = New ListViewItem(item("Type"))
                    ob.SubItems.Add(item("Model"))
                    ob.SubItems.Add(item("cp"))
                    ob.SubItems.Add(item("sp"))
                    ob.SubItems.Add(item("margin"))
                    ListView1.Items.Add(ob)

                End If
                Next


        End If

    End Sub
End Class