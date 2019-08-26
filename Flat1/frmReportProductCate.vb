Imports System.Data.SqlClient
Public Class frmReportProductCate
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

    Sub cateload()
        Module1.Connect()
        Dim sql As String = "select Ca_ID,Name from Category"
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "Cate")
        cmbCate.DataSource = ds.Tables("Cate")
        cmbCate.DisplayMember = "Name"
        cmbCate.ValueMember = "Ca_ID"
        cmbCate.SelectedIndex = -1
        Conn.Close()
    End Sub

    Private Sub frmReportProductCate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cateload()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql As String = "select P_ID,Name,Price,Amount from Product where Ca_ID = '" & cmbCate.SelectedValue & "'"
        Dim sqlCmd As SqlCommand
        Dim sqlDr As SqlDataReader
        Dim TbCtr As DataTable
        Dim currentReport As New ctrReportProductCate
        Module1.Connect()
        sqlCmd = New SqlCommand(sql, Conn)
        sqlDr = sqlCmd.ExecuteReader
        TbCtr = New DataTable
        TbCtr.Load(sqlDr)
        currentReport.SetDataSource(TbCtr)
        ctrv1.ReportSource = currentReport
        currentReport.SetParameterValue("Cate", CStr(cmbCate.Text))
        Me.WindowState = FormWindowState.Maximized
    End Sub
End Class