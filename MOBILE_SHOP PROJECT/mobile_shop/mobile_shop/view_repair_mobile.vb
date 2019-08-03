Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class view_repair_mobile

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        emp_control.Show()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListView1.Items.Clear()
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("repair_mobile")



        If ComboBox1.Text = "" Then
            MsgBox("Please Check.")
        Else
            Dim query = New QueryDocument("Type", ComboBox1.Text)
            For Each item As BsonDocument In collection.Find(query)

                Dim temp As BsonElement = item.GetElement("Type")

                If ComboBox1.Text = temp.Value Then
                    Dim ob As ListViewItem


                    ob = New ListViewItem(item("c_id"))
                    ob.SubItems.Add(item("name"))
                    ob.SubItems.Add(item("Type"))
                    ob.SubItems.Add(item("model"))
                    ob.SubItems.Add(item("date"))
                    ob.SubItems.Add(item("price"))
                    ob.SubItems.Add(item("status"))

                    ListView1.Items.Add(ob)


                End If
                

            Next
            

        End If
    End Sub

  

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        ListView1.Items.Clear()

        Dim mongo As MongoServer = MongoServer.Create()
        Dim update As IMongoUpdate
        Dim query As IMongoQuery
        mongo.Connect()
       

        If TextBox1.Text = "" Then
            MsgBox("Please Enter for search")

        ElseIf TextBox1.Text <> "" Then
            Dim db = mongo.GetDatabase("mobile_shop")
            Dim collection = db.GetCollection(Of BsonDocument)("repair_mobile")
            Dim query1 As QueryDocument = New QueryDocument("c_id", TextBox1.Text)
            Dim Flag = 0
          

            For Each item As BsonDocument In collection.Find(query1)

                Dim cid As BsonElement = item.GetElement("c_id")
                If (TextBox1.Text = cid.Value) Then

                    Flag = 1

                    query = Builders.Query.EQ("c_id", TextBox1.Text)
                    update = Builders.Update.Set("status", "yes")
                    collection.Update(query, update)
                    MsgBox("Record Modified")
                    'ComboBox1.SelectedIndex = -1

                    TextBox1.Text = ""

                    Dim query2 = New QueryDocument("Type", ComboBox1.Text)
                    For Each item1 As BsonDocument In collection.Find(query2)

                        Dim temp As BsonElement = item.GetElement("Type")

                        If ComboBox1.Text = temp.Value Then
                            Dim ob As ListViewItem
                            ob = New ListViewItem(item1("c_id"))
                            ob.SubItems.Add(item1("name"))
                            ob.SubItems.Add(item1("Type"))
                            ob.SubItems.Add(item1("model"))
                            ob.SubItems.Add(item1("date"))
                            ob.SubItems.Add(item1("price"))
                            ob.SubItems.Add(item1("status"))
                        End If

                    Next
                    ComboBox1.SelectedIndex = -1

                End If


            Next

            If Flag <> 1 Then

                TextBox1.Text = ""
                MsgBox("Enter existing customer id")
            End If

        End If

    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub view_repair_mobile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class