Imports System.Data.SqlClient
Imports System.IO
Public Class frmMain
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim patchpic As String

    Sub empshodata()
        Module1.Connect()
        Dim sql As String = "select E_User,E_Fname,E_Lname,E_Tel,E_Sex,E_Status from Employee"  'where E_User = '" & "meng0052" & "'
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "Emp")
        dgvEmp.ReadOnly = True
        dgvEmp.DataSource = ds.Tables("Emp")
        With dgvEmp
            .Columns(1).HeaderText = "ชื่อ"
            .Columns(2).HeaderText = "นามสกุล"
            .Columns(3).HeaderText = "เบอร็โทร"
            .Columns(4).HeaderText = "เพศ"
            .Columns(5).HeaderText = "ระดับ"
        End With
    End Sub

    Private Sub dgvEmp_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEmp.CellContentDoubleClick
        txtNa.Text = dgvEmp.Rows(e.RowIndex).Cells(1).Value
        txtLname.Text = dgvEmp.Rows(e.RowIndex).Cells(2).Value
        txtPhone.Text = dgvEmp.Rows(e.RowIndex).Cells(3).Value
        If dgvEmp.Rows(e.RowIndex).Cells(4).Value = 0 Then
            rdbMen.Checked = True
        Else
            rdbWomen.Checked = True
        End If
        If dgvEmp.Rows(e.RowIndex).Cells(5).Value = 0 Then
            rdbAdmin.Checked = True
        Else
            rdbUser.Checked = True
        End If
        PaEmployee.Visible = True
        paEmAdmin.Visible = False
    End Sub

    Private Sub Panel3_MouseDown(sender As Object, e As MouseEventArgs) Handles paUser.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Panel3_MouseMove(sender As Object, e As MouseEventArgs) Handles paUser.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Panel3_MouseUp(sender As Object, e As MouseEventArgs) Handles paUser.MouseUp
        drag = False
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
        Application.Exit()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub

    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles paAdmin.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs) Handles paAdmin.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Panel2_MouseUp(sender As Object, e As MouseEventArgs) Handles paAdmin.MouseUp
        drag = False
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If User_Status = "0" Then
            paAdmin.Visible = True
            paUser.Visible = False
        Else
            paUser.Visible = True
            paAdmin.Visible = False
        End If
        PaEmployee.Visible = False
        paEmAdmin.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If User_Status = "0" Then
            PaEmployee.Visible = False
            paEmAdmin.Visible = True
            empshodata()
        Else
            PaEmployee.Visible = True
            paEmAdmin.Visible = False
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        empshodata()
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        Dim opf As New OpenFileDialog
        opf.Filter = "Choose Image (*.jpg)|*.jpg"
        If opf.ShowDialog = DialogResult.OK Then
            patchpic = opf.FileName
            PicUser.ImageLocation = patchpic
        Else
            PicUser.Image = Nothing
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim sql As String
        Dim sex, status As Integer
        Dim img As Byte() = Nothing
        Dim fs As New FileStream(patchpic, FileMode.Open, FileAccess.Read)
        Dim br As New BinaryReader(fs)
        img = br.ReadBytes(CInt(fs.Length))
        Module1.Connect()

        If rdbAdmin.Checked = True Then
            status = 0
        Else
            status = 1
        End If
        If rdbMen.Checked = True Then
            sex = 0
        Else
            sex = 1
        End If
        sql = "insert into Employee (E_User,E_Pass,E_Fname,E_Lname,E_Add,E_Tel,E_Sex,E_Status,E_Pic) values ('" & txtUser.Text & "','" & txtPass.Text & "','" & txtNa.Text & "','" & txtLname.Text & "','" & txtAddress.Text & "','" & txtPhone.Text & "','" & sex & "','" & status & "',@pic)"
        Dim sqlCmd As New SqlCommand(sql, Conn)
        sqlCmd.Parameters.Add(New SqlParameter("@pic", img))
        sqlCmd.ExecuteNonQuery()
        MessageBox.Show("Save Complete")
        Conn.Close()
    End Sub


End Class