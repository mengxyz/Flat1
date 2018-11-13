Imports System.Data.SqlClient
Public Class Login
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        drag = False
    End Sub

    Private Sub Label1_MouseDown(sender As Object, e As MouseEventArgs) Handles Label1.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub


    Private Sub Label1_MouseUp(sender As Object, e As MouseEventArgs) Handles Label1.MouseUp
        drag = False
    End Sub

    Private Sub Label1_MouseMove(sender As Object, e As MouseEventArgs) Handles Label1.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub txtUser_MouseDown(sender As Object, e As MouseEventArgs) Handles txtUser.MouseDown
        txtUser.Text = ""
    End Sub

    Private Sub txtPass_MouseDown(sender As Object, e As MouseEventArgs) Handles txtPass.MouseDown
        txtPass.Text = ""
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Module1.Connect()
        Dim sql As String = "select E_User,E_Status from Employee where E_User = '" & txtUser.Text & "' and E_Pass = '" & txtPass.Text & "'"
        Dim sqlCmd As New SqlCommand(sql, Conn)
        Dim dr As SqlDataReader = sqlCmd.ExecuteReader
        If dr.Read Then
            User_Na = dr.Item(0)
            User_Status = dr.Item(1)
        Else
            MetroFramework.MetroMessageBox.Show(Me, "Username หรือ Password ไม่ถูกต้อง", "ตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            dr.Close()
            Exit Sub
        End If
        dr.Close()
        Conn.Close()
        If User_Status = "0" Or User_Status = "1" Then
            Me.Hide()
            If MetroFramework.MetroMessageBox.Show(Me, "PASSED", "ตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Question) = DialogResult.OK Then
                frmMain.Show()
            End If
        End If
    End Sub

    Private Sub txtPass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPass.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub txtPass_Click(sender As Object, e As EventArgs) Handles txtPass.Click
        txtPass.PasswordChar = "*"
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If txtPass.PasswordChar = "*" Then
            txtPass.PasswordChar = ""
        ElseIf txtPass.PasswordChar = ControlChars.NullChar Then
            txtPass.PasswordChar = "*"
        End If
    End Sub
End Class