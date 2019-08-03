Public Class print_bill


    Private Sub print_bill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblDate.Text = dat
        lblCName.Text = cname
        lblAddress.Text = caddress
        lblMob.Text = cmbl
        Label1.Text = bquantity
        Label2.Text = ctyp & "-" & cmodel
        Label3.Text = cprice
        Label4.Text = cprice * bquantity
        lblTotalAmount1.Text = ctotal
        Label15.Text = Label4.Text * 0.14
        Label8.Text = create_bill.TextBox7.Text
    End Sub
End Class