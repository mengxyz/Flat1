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
    End Sub

    Sub dayctr()
        Dim sql As String = "select distinct r.OR_ID,(u.Fname + u.Lname)as Name,r.Net as Total_Price from [Order] r , [User] u ,Order_Detail rd,Category c where r.[Date] = '" & cmbSale.SelectedValue & "' and r.Username = u.Username"
        Dim sqlCmd As SqlCommand
        Dim sqlDr As SqlDataReader
        Dim TbCtr As DataTable
        Dim currentReport As New ctrReportDay
        Module1.Connect()
        sqlCmd = New SqlCommand(sql, Conn)
        sqlDr = sqlCmd.ExecuteReader
        TbCtr = New DataTable
        TbCtr.Load(sqlDr)
        currentReport.SetDataSource(TbCtr)
        ctrv1.ReportSource = currentReport
        currentReport.SetParameterValue("Date1", CStr(cmbSale.SelectedValue))
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Sub monthctr()
        Dim sql As String = "select day(r.Date) as Date,sum(Net ) as Total_Price from [Order] r where (cast(month(Date) as varchar) +'/'+ cast(year(Date) as varchar)) = '" & cmbSale.SelectedValue & "' group by day(r.Date)"
        Dim sqlCmd As SqlCommand
        Dim sqlDr As SqlDataReader
        Dim TbCtr As DataTable
        Dim currentReport As New ctrReportMont
        Module1.Connect()
        sqlCmd = New SqlCommand(sql, Conn)
        sqlDr = sqlCmd.ExecuteReader
        TbCtr = New DataTable
        TbCtr.Load(sqlDr)
        currentReport.SetDataSource(TbCtr)
        ctrv1.ReportSource = currentReport
        currentReport.SetParameterValue("Date", CStr(cmbSale.Text))
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Sub yearctr()
        Module1.Connect()
        Dim sql As String = "select convert(int,month(Date)) as Price,SUM(Net) as Total_Price,(select SUM(Net)from [Order] where YEAR(Date) = '" & cmbSale.SelectedValue & "') as Net from [Order] where YEAR(Date) = '" & cmbSale.SelectedValue & "' group by MONTH(Date)"
        Dim sqCmd As New SqlCommand(sql, Conn)
        Dim sqlDr As SqlDataReader = sqCmd.ExecuteReader
        Dim TbCtr As New DataTable
        Conn.Close()
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(sql, Conn)
        da.Fill(ds, "emp")
        For i As Integer = 0 To ds.Tables("emp").Rows.Count - 1
            ds.Tables("emp").Rows(i).Item(0) = MonthName(ds.Tables("emp").Rows(i).Item(0))
        Next
        Dim currentreport As New ctrReportYear
        TbCtr.Load(sqlDr)
        currentreport.SetDataSource(ds.Tables("emp"))
        ctrv1.ReportSource = currentreport
        currentreport.SetParameterValue("Date", CStr(cmbSale.SelectedValue))
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If rdbMonth.Checked = True Then
            monthctr()
        ElseIf rdbDay.Checked = True Then
            dayctr()
        ElseIf rdbYear.Checked = True Then
            yearctr()
        End If
    End Sub

    Sub dateload()
        Module1.Connect()
        Dim sql As String = "select distinct Date,(cast(day(Date) as varchar) +'/'+cast(month(Date) as varchar) +'/'+ cast(year(Date) as varchar)) as Date1 from [Order]"
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "Date")
        cmbSale.DataSource = ds.Tables("Date")
        cmbSale.DisplayMember = "Date1"
        cmbSale.ValueMember = "Date"
        cmbSale.SelectedIndex = -1
        Conn.Close()
    End Sub

    Sub yearload()
        Module1.Connect()
        Dim sql As String = "select distinct convert(int,year(Date)) as Date from [Order]"
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "Date")
        cmbSale.DataSource = ds.Tables("Date")
        cmbSale.DisplayMember = "Date"
        cmbSale.ValueMember = "Date"
        cmbSale.SelectedIndex = -1
        Conn.Close()
    End Sub

    Sub monthload()
        Module1.Connect()
        Dim sql As String = "  select distinct (cast(month(Date) as varchar) +'/'+ cast(year(Date) as varchar)) as Month from [Order]"
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "Date")
        cmbSale.DataSource = ds.Tables("Date")
        cmbSale.DisplayMember = "Month"
        cmbSale.ValueMember = "Month"
        cmbSale.SelectedIndex = -1
        Conn.Close()
    End Sub

    Private Sub rdbDay_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDay.CheckedChanged
        dateload()
        cmbSale.Enabled = True
        Dim a As String = "  select month(r.Date),r.Net as Total_Price,(select sum(Net) from [Order]) as Net from [Order] r where (cast(month(Date) as varchar) +'/'+ cast(year(Date) as varchar)) = '12/2018'"
    End Sub

    Private Sub rdbYear_CheckedChanged(sender As Object, e As EventArgs) Handles rdbYear.CheckedChanged
        yearload()
        cmbSale.Enabled = True
    End Sub

    Private Sub rdbMonth_CheckedChanged(sender As Object, e As EventArgs) Handles rdbMonth.CheckedChanged
        monthload()
        cmbSale.Enabled = True
    End Sub
End Class