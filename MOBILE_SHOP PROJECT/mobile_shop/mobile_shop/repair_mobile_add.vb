Imports MongoDB.Bson
Imports MongoDB.Driver

Public Class repair_mobile_add

    Private Sub repair_mobile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MonthCalendar1_DateSelected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateSelected

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Me.MonthCalendar1.Visible = True
    End Sub

    Private Sub MonthCalendar1_DateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        TextBox3.Text = Me.MonthCalendar1.SelectionStart.Date

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim collection = db.GetCollection(Of BsonDocument)("repair_mobile")
        If ComboBox2.Text = "" Or TextBox3.Text = "" Or TextBox2.Text = "" Or TextBox1.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or ComboBox1.Text = "" Then
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
                user.Add("c_id", TextBox1.Text)
                user.Add("name", TextBox2.Text)
                user.Add("Type", ComboBox1.Text)
                user.Add("date", TextBox3.Text)
                user.Add("model", ComboBox2.Text)
                user.Add("price", TextBox5.Text)
                user.Add("query", TextBox6.Text)
                user.Add("status", "no")

                collection.Insert(user)
                MsgBox("Data successfully entered!")

                ComboBox1.Text = ""
                ComboBox2.Text = ""
                TextBox3.Text = ""
                TextBox2.Text = ""
                TextBox6.Text = ""
                TextBox5.Text = ""
                TextBox1.Text = ""
            Else
                MsgBox("Device all already exist")

            End If
        End If
        mongo.Disconnect()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        emp_control.Show()
    End Sub

    Private Sub TextBox2_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar >= ":" And e.KeyChar <= "Z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "0" And e.KeyChar <= "9" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar >= ":" And e.KeyChar <= "Z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "a" And e.KeyChar <= "z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox5_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar >= ":" And e.KeyChar <= "Z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "a" And e.KeyChar <= "z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox6_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar >= ":" And e.KeyChar <= "Z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "0" And e.KeyChar <= "9" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
       
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox2.Items.Clear()
        Dim mongo As MongoServer = MongoServer.Create()
        Dim flag As Integer
        flag = 0
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        If ComboBox1.Text = "ANDROID" Then

            Dim collection = db.GetCollection(Of BsonDocument)("mob_repair")

            Dim query = New QueryDocument("type", ComboBox1.Text)
            For Each item As BsonDocument In collection.Find(query)
                Dim temp As BsonElement = item.GetElement("type")
                Dim temp1 As BsonElement = item.GetElement("ANDROID")
                If ComboBox1.Text = temp.Value Then
                    ComboBox2.Items.Add(temp1.Value)
                End If
            Next
        End If
        If ComboBox1.Text = "APPLE" Then

            Dim collection = db.GetCollection(Of BsonDocument)("mob_repair_apple")

            Dim query = New QueryDocument("type", ComboBox1.Text)
            For Each item As BsonDocument In collection.Find(query)
                Dim temp As BsonElement = item.GetElement("type")
                Dim temp1 As BsonElement = item.GetElement("APPLE")
                If ComboBox1.Text = temp.Value Then
                    ComboBox2.Items.Add(temp1.Value)
                End If
            Next
        End If
    End Sub

    Private Sub TextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Click

        Dim mongo As MongoServer = MongoServer.Create()
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim last As Integer = 0

        Dim collection = db.GetCollection(Of BsonDocument)("repair_mobile")

        For Each item As BsonDocument In collection.FindAll()

            If last < Val(item("c_id")) Then
                last = Val(item("c_id"))
            End If
        Next
        TextBox1.Text = last + 1
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class