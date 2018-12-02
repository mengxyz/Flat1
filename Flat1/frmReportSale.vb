Imports System.Data.SqlClient
Public Class frmReportSale
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
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

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Sub Orderload()
        Module1.Connect()
        Dim sql As String = "select OR_ID from [Order]"
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "Order")
        cmbSale.DataSource = ds.Tables("Order")
        cmbSale.DisplayMember = "OR_ID"
        cmbSale.ValueMember = "OR_ID"
        cmbSale.SelectedIndex = -1
        Conn.Close()
    End Sub

    Private Sub frmReportSale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Orderload()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql As String = "select r.OR_ID,(c.Fname + ' ' + c.Lname ) as Cname ,c.Tel ,c.[Add] ,(u.Fname + ' ' + u.Lname ) as Uname,p.Name,rd.Num ,rd.Price ,rd.Total_Price ,r.Net ,r.[Date] from [Order] r,Customer c, [User] u,Product p,Order_Detail rd where r.OR_ID = '" & cmbSale.SelectedValue & "' and r.C_ID = c.C_ID and r.Username = u.Username and r.OR_ID = rd.OR_ID and p.P_ID = rd.P_ID "
        Dim sqlCmd As SqlCommand
        Dim sqlDr As SqlDataReader
        Dim TbCtr As DataTable
        Dim currentReport As New ctrReporttSale1
        Module1.Connect()
        sqlCmd = New SqlCommand(sql, Conn)
        sqlDr = sqlCmd.ExecuteReader
        TbCtr = New DataTable
        TbCtr.Load(sqlDr)
        currentReport.SetDataSource(TbCtr)
        ctrv1.ReportSource = currentReport
        Me.WindowState = FormWindowState.Maximized
    End Sub
End Class