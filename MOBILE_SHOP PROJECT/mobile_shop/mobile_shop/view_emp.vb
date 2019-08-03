Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class view_emp
    Sub fillgrid()
        Dim mongo As MongoServer = MongoServer.Create
        mongo.Connect()

        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("add_emp")
        Dim ob As ListViewItem
        ListView1.Items.Clear()
        For Each item As BsonDocument In collection.FindAll()
            ob = New ListViewItem(item("Name"))
            ob.SubItems.Add(item("DOB"))
            ob.SubItems.Add(item("Adharno"))
            ob.SubItems.Add(item("Mobileno"))
            ob.SubItems.Add(item("Address"))
            ListView1.Items.Add(ob)
        Next
    End Sub


    Private Sub view_emp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillgrid()
        ListView1.Enabled = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        admin_control.Show()
        Me.Close()

    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
        admin_control.Show()
    End Sub
End Class