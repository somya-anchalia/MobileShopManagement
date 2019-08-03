Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class create_bill
    'Cancel button
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        emp_control.Show()
    End Sub
    'search button
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

                    If ComboBox3.Text = temp1.Value And ComboBox1.Text = temp.Value Then
                        ListView1.Items.Clear()

                        Dim ob As ListViewItem = New ListViewItem(item("Type"))
                        ob.SubItems.Add(item("Model"))
                        ob.SubItems.Add(item("Price"))
                        ob.SubItems.Add(item("Quantity"))
                        ob.SubItems.Add(item("specifications"))

                        ListView1.Items.Add(ob)

                    End If
                Next

            Next
        End If
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

        TextBox1.Text = ((Val(TextBox3.Text) * Val(TextBox4.Text)) + (Val(TextBox3.Text) * Val(TextBox4.Text) * 0.14))
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        TextBox2.Text = ListView1.SelectedItems.Item(0).SubItems(1).Text
        TextBox3.Text = ListView1.SelectedItems.Item(0).SubItems(2).Text
        TextBox4.Text = ListView1.SelectedItems.Item(0).SubItems(3).Text
        TextBox5.Text = ListView1.SelectedItems.Item(0).SubItems(4).Text
        TextBox6.Text = ComboBox1.Text
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

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub

    Private Sub create_bill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
        TextBox7.Text = ""
        Dim mongo As MongoServer = MongoServer.Create()
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim last As Integer = 0

        Dim collection = db.GetCollection(Of BsonDocument)("customer_info")

        For Each item As BsonDocument In collection.FindAll()

            If last < Val(item("bno")) Then             'always initially last=0 because at line number 98 we have intialized last=0 
                last = Val(item("bno"))
            End If
        Next
        TextBox7.Text = last + 1

    End Sub
    'Next Button
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Then
            MsgBox("Please fill all the entries")
        Else
            btype = TextBox6.Text
            bmodel = TextBox2.Text
            bprice = TextBox3.Text
            bno = TextBox7.Text
            bquantity = TextBox4.Text
            totalAmout = TextBox1.Text

            Me.Hide()
            customer_info.Show()

            ComboBox1.Text = ""
            ComboBox3.Text = ""
            TextBox3.Text = ""
            TextBox2.Text = ""
            TextBox6.Text = ""
            TextBox5.Text = ""
            TextBox1.Text = ""

            TextBox4.Text = ""
        End If
    End Sub

End Class