Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class view_bill
    Sub FillGrid()
        Dim mongo As MongoServer = MongoServer.Create
        mongo.Connect()

        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("customer_info")
        Dim ob As ListViewItem
        ListView1.Items.Clear()
        For Each item As BsonDocument In collection.FindAll()
            ob = New ListViewItem(item("Date"))
            ob.SubItems.Add(item("CName"))
            ob.SubItems.Add(item("CMobile"))
            ob.SubItems.Add(item("Type"))
            ob.SubItems.Add(item("Model"))
            ob.SubItems.Add(item("Quantity"))
            ob.SubItems.Add(item("Price"))
            ob.SubItems.Add(item("Total"))
            ob.SubItems.Add(item("CAddress"))

            ob.SubItems.Add(item("bno"))
            ListView1.Items.Add(ob)
        Next
    End Sub
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        dat = ListView1.SelectedItems.Item(0).SubItems(0).Text
        cname = ListView1.SelectedItems.Item(0).SubItems(1).Text
        cmbl = ListView1.SelectedItems.Item(0).SubItems(2).Text
        ctyp = ListView1.SelectedItems.Item(0).SubItems(3).Text
        cmodel = ListView1.SelectedItems.Item(0).SubItems(4).Text
        cqty = ListView1.SelectedItems.Item(0).SubItems(5).Text
        cprice = ListView1.SelectedItems.Item(0).SubItems(6).Text
        ctotal = ListView1.SelectedItems.Item(0).SubItems(7).Text
        caddress = ListView1.SelectedItems.Item(0).SubItems(8).Text
        Dim x As New print_bill
        x.MdiParent = emp_control
        x.Show()
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Hide()
        emp_control.Show()
    End Sub

    Private Sub Button3_Click_1(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Me.Hide()
        emp_control.Show()
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        FillGrid()
    End Sub

End Class