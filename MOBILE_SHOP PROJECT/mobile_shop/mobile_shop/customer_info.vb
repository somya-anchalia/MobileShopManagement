Imports MongoDB.Bson
Imports MongoDB.Driver
Public Class customer_info

    Private Sub customer_info_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label5.Text = Date.Today
    End Sub
    'cancel
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
        emp_control.Show()
    End Sub

    Private Sub TextBox7_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar >= ":" And e.KeyChar <= "Z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "a" And e.KeyChar <= "z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub


    Private Sub TextBox8_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar >= ":" And e.KeyChar <= "Z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "0" And e.KeyChar <= "9" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button2_Click_1(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Me.Close()
        create_bill.Show()
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim mongo As MongoServer = MongoServer.Create()
        mongo.Connect()
        Dim db = mongo.GetDatabase("mobile_shop")
        Dim Collection1 = db.GetCollection(Of BsonDocument)("customer_info")
        Dim user As BsonDocument = New BsonDocument()
        user.Add("Date", Label5.Text)
        user.Add("Type", btype)
        user.Add("Model", bmodel)
        user.Add("Price", bprice)
        user.Add("Quantity", bquantity)
        user.Add("CName", TextBox8.Text)
        user.Add("CMobile", TextBox7.Text)
        user.Add("CAddress", TextBox6.Text)
        user.Add("CCity", ComboBox1.Text)
        user.Add("Total", totalAmout)
        user.Add("bno", bno)
        Collection1.Insert(user)

        dat = Label5.Text
        cname = TextBox8.Text
        caddress = TextBox6.Text
        cmbl = TextBox7.Text
        cqty = bquantity
        ctyp = btype
        cmodel = bmodel
        cprice = bprice
        ctotal = totalAmout


        Dim r As DialogResult
        r = MsgBox("Data successfully entered!..Do you Want to Print", MsgBoxStyle.YesNo, "Print Message")
        If r = Windows.Forms.DialogResult.Yes Then
            print_bill.Show()

        Else
            Me.Close()
            emp_control.Show()

        End If

    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar >= ":" And e.KeyChar <= "Z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "a" And e.KeyChar <= "z" Then
            e.Handled = True
        ElseIf e.KeyChar >= "!" And e.KeyChar <= "/" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox7_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
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

    Private Sub TextBox8_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged

    End Sub
End Class