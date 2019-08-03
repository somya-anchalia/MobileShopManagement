Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class admin_id
    Public x As String

    Sub FillGrid()
        Dim mongo As MongoServer = MongoServer.Create
        mongo.Connect()

        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("admin")
        Dim ob As ListViewItem
        ListView1.Items.Clear()
        For Each item As BsonDocument In collection.FindAll()
            ob = New ListViewItem(item("username"))
            ob.SubItems.Add(item("password"))
            ob.SubItems.Add(item("hint"))
            ListView1.Items.Add(ob)
        Next
    End Sub

    Private Sub Addn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Addn.Click
        x = "Add"
        ListView1.Enabled = False
        TextBox1.ReadOnly = False
        TextBox2.ReadOnly = False
        TextBox3.ReadOnly = False
        TextBox4.ReadOnly = False
    End Sub

    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        TextBox1.Text = ListView1.SelectedItems.Item(0).SubItems(0).Text
        TextBox2.Text = ListView1.SelectedItems.Item(0).SubItems(1).Text
        TextBox3.Text = ListView1.SelectedItems.Item(0).SubItems(1).Text
        TextBox4.Text = ListView1.SelectedItems.Item(0).SubItems(2).Text
        Label4.Visible = False
    End Sub

    Private Sub selct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selct.Click
        ListView1.Enabled = True
    End Sub

    Private Sub Updte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Updte.Click
        x = "Update"
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = False
        TextBox3.ReadOnly = False
        TextBox4.ReadOnly = False
    End Sub

    Private Sub Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete.Click
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("admin")
        If TextBox1.Text = "" Then
            MsgBox("Please Check.")
            mongo.Disconnect()
        Else
            Dim query = New QueryDocument("username", TextBox1.Text)
            For Each item As BsonDocument In collection.Find(query)
                flag = 1
            Next
        End If
        If (flag = 1) Then
            'MsgBox("Roll Number Found")
            Dim query = New QueryDocument("username", TextBox1.Text)
            For Each item As BsonDocument In collection.Find(query)
                Dim uname As BsonElement = item.GetElement("username")
                Dim pass As BsonElement = item.GetElement("password")
                Dim hint As BsonElement = item.GetElement("hint")
                TextBox1.Text = uname.Value
                TextBox2.Text = pass.Value
                TextBox4.Text = hint.Value
                collection.Remove(query)
                MsgBox("Deletion Successful")
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                FillGrid()
            Next
        Else
            MsgBox("username not found")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If (x <> "Update") Then

            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
                MsgBox("InComplete information plz fill all the informantion", MsgBoxStyle.OkOnly)
                TextBox2.Text = ""
                TextBox3.Text = ""

            Else
                Dim flag = 0

                Dim mongo As MongoServer = MongoServer.Create()
                mongo.Connect()
                Dim db = mongo.GetDatabase("mobile_shop")
                Dim Collection1 = db.GetCollection(Of BsonDocument)("admin")

                ''Validation
                If TextBox2.TextLength = 0 Then
                    MsgBox("Please Enter the password")
                    TextBox3.Text = ""
                    mongo.Disconnect()

                ElseIf TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
                    MsgBox("FIll all the records", MsgBoxStyle.OkOnly, "Security message")
                    mongo.Disconnect()

                Else
                    ''Database
                    Dim collection = db.GetCollection(Of BsonDocument)("admin")
                    Dim query = New QueryDocument("username", TextBox1.Text)
                    For Each item As BsonDocument In collection.Find(query)
                        flag = 1
                    Next
                    If flag = 0 Then
                        Dim user As BsonDocument = New BsonDocument()
                        user.Add("username", TextBox1.Text)

                        If Label4.Text <> "password does not match" Then
                            user.Add("password", TextBox2.Text)
                            user.Add("hint", TextBox4.Text)
                            Collection1.Insert(user)
                            MsgBox("Data successfully entered!")
                            FillGrid()
                        Else
                            MsgBox("password does not match", MsgBoxStyle.OkOnly)
                        End If

                        TextBox1.Text = ""
                        TextBox2.Text = ""
                        TextBox3.Text = ""
                        TextBox4.Text = ""
                        TextBox1.ReadOnly = True
                        TextBox2.ReadOnly = True
                        TextBox3.ReadOnly = True
                        TextBox4.ReadOnly = True
                        mongo.Disconnect()
                    Else
                        MsgBox("username already exist..", MsgBoxStyle.OkOnly)

                    End If
                End If
            End If


        ElseIf (x <> "Add") Then
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Fill all the records", MsgBoxStyle.OkOnly)
            Else
                Dim mongo As MongoServer = MongoServer.Create()
                Dim update As IMongoUpdate
                Dim query As IMongoQuery
                mongo.Connect()
                Dim db = mongo.GetDatabase("mobile_shop")
                Dim collection = db.GetCollection(Of BsonDocument)("admin")

                If Label4.Text <> "password does not match" Then
                    query = Builders.Query.EQ("username", TextBox1.Text)
                    update = Builders.Update.Set("password", TextBox2.Text).Set("hint", TextBox4.Text)
                    collection.Update(query, update)
                    MsgBox("Record Modified")
                    FillGrid()
                    TextBox1.ReadOnly = True
                    TextBox2.ReadOnly = True
                    TextBox3.ReadOnly = True
                    TextBox4.ReadOnly = True
                Else
                    MsgBox("Paasword does not match", MsgBoxStyle.OkOnly)
                End If

                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                mongo.Disconnect()
            End If
        End If
    End Sub

    Private Sub frmadminPass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label4.Visible = False
        FillGrid()
        ListView1.Enabled = False
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        TextBox3.ReadOnly = True
        TextBox4.ReadOnly = True
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim ch As Integer
        ch = Asc(e.KeyChar)
        If ch >= 48 Then
            If ch <= 57 Then
                e.Handled = True
            End If
        End If
        If e.KeyChar >= ":" And e.KeyChar < "A" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Label4.Visible = True
        If TextBox2.Text = TextBox3.Text Then
            Label4.Text = "password match"
            Label4.ForeColor = Color.Green
        Else
            Label4.Text = "password does not match"
            Label4.ForeColor = Color.Red
        End If

        If TextBox3.Text = "" Or TextBox3.Text = "" Then
            Label4.Visible = False
        ElseIf TextBox2.Text = "" Then
            Label4.Visible = False
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        admin_control.Show()
    End Sub
End Class