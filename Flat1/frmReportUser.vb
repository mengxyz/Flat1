Imports System.Data.SqlClient
Public Class frmReportUser
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Private Sub frmReportUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Module1.Connect()
        Dim sql As String = "select Username,(Fname + ' ' + Lname ) as Name,str(Sex) as Sex,Tel,Str(Status) as Status from [User]"
        Dim sqCmd As New SqlCommand(sql, Conn)
        Dim sqlDr As SqlDataReader = sqCmd.ExecuteReader
        Dim TbCtr As New DataTable
        Conn.Close()
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(sql, Conn)
        da.Fill(ds, "User")
        For i As Integer = 0 To ds.Tables("User").Rows.Count - 1
            If ds.Tables("User").Rows(i).Item(2) = 0 Then
                ds.Tables("User").Rows(i).Item(2) = "ชาย"
            Else
                ds.Tables("USer").Rows(i).Item(2) = "หญิง"
            End If
        Next
        For i As Integer = 0 To ds.Tables("User").Rows.Count - 1
            If ds.Tables("User").Rows(i).Item(4) = 0 Then
                ds.Tables("User").Rows(i).Item(4) = "ผู้ดูแลระบบ"
            Else
                ds.Tables("USer").Rows(i).Item(4) = "พนักงาน"
            End If
        Next
        Dim currentreport As New ctrReportUser
        TbCtr.Load(sqlDr)
        currentreport.SetDataSource(ds.Tables("User"))
        ctrv1.ReportSource = currentreport
        Me.WindowState = FormWindowState.Maximized
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
End Class