Imports MongoDB.Bson
Imports MongoDB.Driver


Public Class add_emp

    Dim x As String
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
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim ch As Integer
        ch = Asc(e.KeyChar)
        If ch >= 48 Then
            If ch <= 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim ch As Integer
        ch = Asc(e.KeyChar)
        If ch >= 48 Then
            If ch <= 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar >= ":" And e.KeyChar <= "Z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "a" And e.KeyChar <= "z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmRegisterEmpl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillgrid()
        ListView1.Enabled = False
        TextBox1.ReadOnly = True
        TextBox3.ReadOnly = True
        TextBox4.ReadOnly = True
        TextBox5.ReadOnly = True
        TextBox2.ReadOnly = True


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox2.Text = ""


        x = "Add"


        TextBox1.ReadOnly = False
        TextBox3.ReadOnly = False
        TextBox4.ReadOnly = False
        TextBox5.ReadOnly = False
        TextBox2.ReadOnly = False


    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click


        If (x <> "Update") Then

            If TextBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
                MsgBox("InComplete information plz fill all the informantion", MsgBoxStyle.OkOnly)
            Else
                Dim flag = 0

                Dim mongo As MongoServer = MongoServer.Create()
                mongo.Connect()
                Dim db = mongo.GetDatabase("mobile_shop")
                Dim Collection1 = db.GetCollection(Of BsonDocument)("add_emp")

                ''Validation
                If TextBox3.TextLength <> 10 Then
                    MsgBox("Please Enter the Correct Mobile number")
                    TextBox3.Text = ""
                ElseIf TextBox5.TextLength <> 12 Then
                    MsgBox("please Check the Adhar card number")
                    TextBox5.Text = ""

                Else
                    ''Database
                    Dim collection = db.GetCollection(Of BsonDocument)("add_emp")
                    Dim query = New QueryDocument("Name", TextBox1.Text)
                    For Each item As BsonDocument In collection.Find(query)
                        flag = 1
                    Next
                    If flag = 0 Then
                        Dim user As BsonDocument = New BsonDocument()
                        user.Add("Name", TextBox1.Text)
                        user.Add("DOB", TextBox2.Text)
                        user.Add("Adharno", TextBox5.Text)
                        user.Add("Mobileno", TextBox3.Text)
                        user.Add("Address", TextBox4.Text)
                        Collection1.Insert(user)


                        fillgrid()


                        TextBox1.Text = ""

                        TextBox3.Text = ""
                        TextBox4.Text = ""
                        TextBox5.Text = ""
                        TextBox2.Text = ""
                        TextBox2.ReadOnly = True
                        TextBox1.ReadOnly = True
                        TextBox3.ReadOnly = True
                        TextBox4.ReadOnly = True
                        TextBox5.ReadOnly = True



                        mongo.Disconnect()
                    Else
                        MsgBox("Username already exist..", MsgBoxStyle.OkOnly)

                    End If
                End If
            End If
        ElseIf (x <> "Add") Then
            If TextBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
                MsgBox("Fill all the records", MsgBoxStyle.OkOnly)
            Else
                Dim mongo As MongoServer = MongoServer.Create()
                Dim update As IMongoUpdate
                Dim query As IMongoQuery
                mongo.Connect()
                Dim db = mongo.GetDatabase("mobile_shop")
                Dim collection = db.GetCollection(Of BsonDocument)("add_emp")

                query = Builders.Query.EQ("Name", TextBox1.Text)
                update = Builders.Update.Set("DOB", MonthCalendar1.Text).Set("Adharno", TextBox5.Text).Set("Mobileno", TextBox3.Text).Set("Address", TextBox4.Text)
                collection.Update(query, update)

                fillgrid()
                TextBox1.ReadOnly = True
                TextBox3.ReadOnly = True
                TextBox4.ReadOnly = True
                TextBox5.ReadOnly = True
                TextBox2.ReadOnly = True


                TextBox2.Text = ""
                TextBox1.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                mongo.Disconnect()
            End If
        End If

    End Sub

    Private Sub TextBox1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim ch As Integer
        ch = Asc(e.KeyChar)
        If ch >= 48 Then
            If ch <= 57 Then
                e.Handled = True
            End If
        End If

        If e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
        If e.KeyChar >= ":" And e.KeyChar < "A" Then
            e.Handled = True
        End If
    End Sub


    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar >= ":" And e.KeyChar <= "Z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "a" And e.KeyChar <= "z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown

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

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        x = "Update"
        GroupBox1.Enabled = True
        TextBox1.ReadOnly = False
        TextBox3.ReadOnly = False
        TextBox4.ReadOnly = False
        TextBox5.ReadOnly = False
        TextBox2.ReadOnly = True

        ListView1.Enabled = False



    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ListView1.Enabled = True
        TextBox1.ReadOnly = True
        TextBox3.ReadOnly = True
        TextBox4.ReadOnly = True
        TextBox5.ReadOnly = True
        TextBox2.ReadOnly = True


        ListView1.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
    End Sub

    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        TextBox1.Text = ListView1.SelectedItems.Item(0).SubItems(0).Text
        MonthCalendar1.Text = ListView1.SelectedItems.Item(0).SubItems(1).Text
        TextBox5.Text = ListView1.SelectedItems.Item(0).SubItems(2).Text
        TextBox3.Text = ListView1.SelectedItems.Item(0).SubItems(3).Text
        TextBox4.Text = ListView1.SelectedItems.Item(0).SubItems(4).Text

        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("add_emp")
        If TextBox1.Text = "" Or MonthCalendar1.Text = "" Then
            Exit Sub

        End If
    End Sub


    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click

        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox2.Text = ""
        MonthCalendar1.Text = ""

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("add_emp")
        If TextBox1.Text = "" Then
            MsgBox("Please Check.")
            mongo.Disconnect()
        Else
            Dim query = New QueryDocument("Name", TextBox1.Text)
            For Each item As BsonDocument In collection.Find(query)
                flag = 1
            Next
        End If
        If (flag = 1) Then
            'MsgBox("Roll Number Found")
            Dim query = New QueryDocument("Name", TextBox1.Text)
            For Each item As BsonDocument In collection.Find(query)
                Dim uname As BsonElement = item.GetElement("Name")
                Dim dob As BsonElement = item.GetElement("DOB")
                Dim adhar As BsonElement = item.GetElement("Adharno")
                Dim mobileno As BsonElement = item.GetElement("Mobileno")
                Dim address As BsonElement = item.GetElement("Address")

                TextBox1.Text = uname.Value
                TextBox2.Text = dob.Value
                TextBox5.Text = adhar.Value
                TextBox3.Text = mobileno.Value
                TextBox4.Text = address.Value


                collection.Remove(query)
                MsgBox("Deletion Successful")
                TextBox1.Text = ""
                TextBox5.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                fillgrid()
            Next
        Else
            MsgBox("Username not found")
        End If
    End Sub


    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Me.Close()
        admin_control.Show()

    End Sub

    Private Sub MonthCalendar1_DateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        TextBox2.Text = Me.MonthCalendar1.SelectionStart.Date
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Me.MonthCalendar1.Visible = True
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
