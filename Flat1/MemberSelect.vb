Imports System.Data.SqlClient
Public Class MemberSelect
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        drag = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Module1.Connect()
        Dim s As String = txtSearch.Text
        Dim sql As String = "  select C_ID,Fname,Lname,Tel,[Add] from Customer where Fname like '%" + s + "%' or Lname like '%" + s + "%'"
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(sql, Conn)
        da.Fill(ds, "Cus")
        dgvCustomer.ReadOnly = True
        dgvCustomer.DataSource = ds.Tables("Cus")
        With dgvCustomer
            .Columns(0).HeaderText = "รหัสลูกค้า"
            .Columns(0).Width = 100
            .Columns(1).HeaderText = "ชื่อ"
            .Columns(1).Width = 150
            .Columns(2).HeaderText = "นามสกุล"
            .Columns(2).Width = 150
            .Columns(3).HeaderText = "เบอร็โทร"
            .Columns(3).Width = 100
            .Columns(4).HeaderText = "ที่อยู่"
            .Columns(4).Width = 100
        End With
        Conn.Close()
    End Sub

    Private Sub MemberSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Module1.Connect()
        Dim sql As String = "select * from Customer"
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(sql, Conn)
        da.Fill(ds, "Cus")
        dgvCustomer.ReadOnly = True
        dgvCustomer.DataSource = ds.Tables("Cus")
        With dgvCustomer
            .Columns(0).HeaderText = "รหัสลูกค้า"
            .Columns(0).Width = 100
            .Columns(1).HeaderText = "ชื่อ"
            .Columns(1).Width = 150
            .Columns(2).HeaderText = "นามสกุล"
            .Columns(2).Width = 150
            .Columns(3).HeaderText = "เบอร็โทร"
            .Columns(3).Width = 100
            .Columns(4).HeaderText = "ที่อยู่"
            .Columns(4).Width = 100
        End With
        Conn.Close()
    End Sub

    Private Sub dgvCustomer_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCustomer.CellContentDoubleClick
        Sale.txtC_ID.Text = dgvCustomer.Rows(e.RowIndex).Cells(0).Value
        Sale.txtCNa.Text = CStr(dgvCustomer.Rows(e.RowIndex).Cells(1).Value) & " " & CStr(dgvCustomer.Rows(e.RowIndex).Cells(2).Value)
        Me.Close()
    End Sub
End Class