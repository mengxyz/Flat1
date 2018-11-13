Imports System.Data.SqlClient
Imports System.IO
Public Class frmMain
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim pk, patchpic As String
    Dim a As Integer
    Dim save_sta As Integer
    Dim pscha As Integer
    Dim picdata() As Byte
    Sub hide()
        PaEmployee.Visible = False
        paEmAdmin.Visible = False
        paCate.Visible = False
    End Sub
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        paSubItem.Visible = False
        If User_Status = "0" Then
            paAdmin.Visible = True
            paUser.Visible = False
        Else
            paUser.Visible = True
            paAdmin.Visible = False
        End If
        hide()
    End Sub

    Sub clear(ByRef x As Integer)
        If x = 1 Then
            txtNa.Text = ""
            txtLname.Text = ""
            txtPhone.Text = ""
            txtAddress.Text = ""
            rdbAdmin.Checked = False
            rdbMen.Checked = False
            rdbUser.Checked = False
            rdbWomen.Checked = False
            txtUser.Text = "username"
            txtPass.Text = "Password"
            PicUser.Image = Nothing
        End If
    End Sub

    Sub empPic(ByVal a As String)
        Dim sql As String
        Dim da As SqlDataAdapter
        If a = "1" Then
            sql = "SELECT E_Pic from Employee where E_User = '" & txtEmpUser.Text & "'"
        Else
            sql = "SELECT E_Pic from Employee where E_User = '" & User_Na & "'"
        End If
        Module1.Connect()
        Dim tb As New DataTable
        da = New SqlDataAdapter(sql, Conn)
        da.Fill(tb)
        Dim img() As Byte
        img = tb.Rows(0)(0)
        picdata = img
        Dim ms As New MemoryStream(img)
        If a = "1" Then
            PicEmp.Image = Image.FromStream(ms)
        Else
            PicUser.Image = Image.FromStream(ms)
        End If
        ms.Close()
    End Sub
    Sub empshodata()
        Module1.Connect()
        Dim sql As String
        If User_Status = 0 Then
            sql = "select E_User,E_Fname,E_Lname,E_Tel,E_Sex,E_Status from Employee"  'where E_User = '" & "meng0052" & "'
        Else
            sql = "select E_User,E_Pass,E_Fname,E_Lname,E_Tel,E_Sex,E_Status,E_Add from Employee where E_User = '" & User_Na & "'"
            Dim sqlcmd As New SqlCommand(sql, Conn)
            Dim dr As SqlDataReader
            dr = sqlcmd.ExecuteReader
            dr.Read()
            txtUser.Text = dr.Item(0).ToString
            txtPass.Text = dr.Item(1).ToString
            txtNa.Text = dr.Item(2).ToString
            txtLname.Text = dr.Item(3).ToString
            txtPhone.Text = dr.Item(4).ToString
            txtAddress.Text = dr.Item(7).ToString
            If dr.Item(5) = 0 Then
                rdbMen.Checked = True
            Else
                rdbWomen.Checked = True
            End If
            If dr.Item(6) = 0 Then
                rdbAdmin.Checked = True
            Else
                rdbUser.Checked = True
            End If
            empPic("2")
            Conn.Close()
            Exit Sub
        End If
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "Emp")
        dgvEmp.ReadOnly = True
        dgvEmp.DataSource = ds.Tables("Emp")
        With dgvEmp
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "ชื่อ"
            .Columns(2).HeaderText = "นามสกุล"
            .Columns(3).HeaderText = "เบอร็โทร"
            .Columns(4).HeaderText = "เพศ"
            .Columns(5).HeaderText = "ระดับ"
        End With
    End Sub

    Private Sub dgvEmp_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEmp.CellClick
        If e.RowIndex > dgvEmp.RowCount - 2 Then
            txtEmpUser.Text = ""
            PicEmp.Image = Nothing
        ElseIf e.RowIndex > -1 Then
            pk = dgvEmp.Rows(e.RowIndex).Cells(0).Value
            txtEmpUser.Text = dgvEmp.Rows(e.RowIndex).Cells(0).Value
            a = e.RowIndex
            empPic("1")
        End If
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
        hide()
        PaEmployee.Visible = True
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
        If MetroFramework.MetroMessageBox.Show(Me, "ต้องการออกจากโปรแกรมใช่หรือไม่", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = DialogResult.OK Then
            Application.Exit()
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If MetroFramework.MetroMessageBox.Show(Me, "ต้องการออกจากโปรแกรมใช่หรือไม่", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = DialogResult.OK Then
            Application.Exit()
        End If
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



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If User_Status = "0" Then
            hide()
            paEmAdmin.Visible = True
            empshodata()
        Else
            hide()
            PaEmployee.Visible = True
            empshodata()
        End If
        txtEmpUser.Text = ""
        PicEmp.Image = Nothing
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnItem.Click
        If paSubItem.Visible = True Then
            paSubItem.Visible = False
        ElseIf paSubItem.Visible = False Then
            paSubItem.Visible = True
        End If
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
        Emp(save_sta)
        clear(1)
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Cateshodata()
        hide()
        paCate.Visible = True
        paSubItem.Visible = False
        txtCateNa.Enabled = False
    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btnEmpAedit.Click
        Module1.Connect()
        Dim sql As String = "select E_Pass,E_Add from Employee where E_User = '" & dgvEmp.Rows(a).Cells(0).Value & "'"
        Dim sqlDr As SqlDataReader
        Dim sqlcmd As New SqlCommand(sql, Conn)
        sqlDr = sqlcmd.ExecuteReader
        If sqlDr.Read Then
            txtPass.Text = sqlDr.Item(0).ToString
            txtAddress.Text = sqlDr.Item(1).ToString
        End If
        Conn.Close()
        If txtEmpUser.Text = "" Then
            ' MetroFramework.MetroMessageBox.Show(Me, "", "เลือกข้อมูลที่ต้องการเเก้ไข", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
            Exit Sub
        End If
        txtUser.Text = dgvEmp.Rows(a).Cells(0).Value
        txtNa.Text = dgvEmp.Rows(a).Cells(1).Value
        txtLname.Text = dgvEmp.Rows(a).Cells(2).Value
        txtPhone.Text = dgvEmp.Rows(a).Cells(3).Value
        If dgvEmp.Rows(a).Cells(4).Value = 0 Then
            rdbMen.Checked = True
        Else
            rdbWomen.Checked = True
        End If
        If dgvEmp.Rows(a).Cells(5).Value = 0 Then
            rdbAdmin.Checked = True
        Else
            rdbUser.Checked = True
        End If
        hide()
        PaEmployee.Visible = True
        empPic("2")
        save_sta = 2
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        PaEmployee.Visible = False
    End Sub

    Private Sub btnCateSave_Click(sender As Object, e As EventArgs) Handles btnCateSave.Click
        Cate(save_sta)
    End Sub

    Sub Emp(ByVal x As Integer)
        Dim sql As String
        Dim sex, status As Integer
        Dim sqlCmd As SqlCommand
        Dim img As Byte() = Nothing
        Dim fs As FileStream
        Dim br As BinaryReader
        If patchpic <> Nothing Then
            fs = New FileStream(patchpic, FileMode.Open, FileAccess.Read)
            br = New BinaryReader(fs)
            img = br.ReadBytes(CInt(fs.Length))
        End If
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
        MessageBox.Show(x)
        If x <> 2 Then
            sql = "insert into Employee (E_User,E_Pass,E_Fname,E_Lname,E_Add,E_Tel,E_Sex,E_Status,E_Pic) values ('" & txtUser.Text & "','" & txtPass.Text & "','" & txtNa.Text & "','" & txtLname.Text & "','" & txtAddress.Text & "','" & txtPhone.Text & "','" & sex & "','" & status & "',@pic)"
        ElseIf x = 2 Then
            If patchpic = Nothing Then
                MessageBox.Show("00000000")
                MessageBox.Show(pk)
                sql = "update Employee set E_User = '" & txtUser.Text & "',E_Pass = '" & txtPass.Text & "',E_Fname = '" & txtNa.Text & "',E_Lname = '" & txtLname.Text & "',E_Add = '" & txtAddress.Text & "',E_Tel = '" & txtPhone.Text & "',E_Sex = '" & sex & "',E_Status = '" & status & "',E_Pic = @pic where E_User = '" & pk & "'"
                sqlCmd = New SqlCommand(sql, Conn)
                sqlCmd.Parameters.Add(New SqlParameter("@pic", picdata))
                sqlCmd.ExecuteNonQuery()
                MessageBox.Show("Save Complete")
                Conn.Close()
                Exit Sub
            Else
                sql = "update Employee set E_User = '" & txtUser.Text & "',E_Pass = '" & txtPass.Text & "',E_Fname = '" & txtNa.Text & "',E_Lname = '" & txtLname.Text & "',E_Add = '" & txtAddress.Text & "',E_Tel = '" & txtPhone.Text & "',E_Sex = '" & sex & "',E_Status = '" & status & "',E_Pic = @pic where E_User = '" & pk & "'"
            End If
        End If
        sqlCmd = New SqlCommand(sql, Conn)
        sqlCmd.Parameters.Add(New SqlParameter("@pic", img))
        sqlCmd.ExecuteNonQuery()
        MessageBox.Show("Save Complete")
        Conn.Close()
    End Sub

    Sub Cate(ByVal x As Integer)
        Module1.Connect()
        Dim sql As String
        If x = 1 Then
            sql = "insert into Cate (C_Na) values ('" & txtCateNa.Text & "')"
        ElseIf x = 2 Then
            sql = "update Cate set C_Na = '" & txtCateNa.Text & "' where C_ID = '" & dgvCate.Rows(a).Cells(0).Value & "' "
        ElseIf x = 3 Then
            sql = "delete from Cate where C_ID = '" & dgvCate.Rows(a).Cells(0).Value & "'"
        Else
            Exit Sub
        End If
        Dim sqlCmd As New SqlCommand(sql, Conn)
        sqlCmd.ExecuteNonQuery()
        Conn.Close()
        txtCateNa.Text = ""
        txtCateNa.Enabled = False
        If x = 1 Then
            MetroFramework.MetroMessageBox.Show(Me, "", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Question)
        ElseIf x = 2 Then
            MetroFramework.MetroMessageBox.Show(Me, "", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Question)
        ElseIf x = 3 Then
            MetroFramework.MetroMessageBox.Show(Me, "", "Delete Complete", MessageBoxButtons.OK, MessageBoxIcon.Question)
        End If
        Cateshodata()
    End Sub

    Sub Cateshodata()
        Module1.Connect()
        Dim sql As String = "select * from Cate"  'where E_User = '" & "meng0052" & "'
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "cate")
        dgvCate.ReadOnly = True
        dgvCate.DataSource = ds.Tables("cate")
        dgvCate.Columns(0).HeaderText = "รหัสหมวดหมู่"
        dgvCate.Columns(0).Width = 100
        dgvCate.Columns(1).HeaderText = "ชื่อหมวดหมู่"
        dgvCate.Columns(1).Width = 200
    End Sub

    Private Sub dgvCate_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCate.CellContentClick
        If e.RowIndex > dgvCate.RowCount - 2 Then
            txtCateNa.Text = ""
        ElseIf e.RowIndex > -1 Then
            txtCateNa.Text = dgvCate.Rows(e.RowIndex).Cells(1).Value
        End If
    End Sub

    Private Sub dgvCate_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvCate.RowHeaderMouseClick
        a = e.RowIndex
        If e.RowIndex > dgvCate.RowCount - 2 Then
            txtCateNa.Text = ""
        Else
            txtCateNa.Text = dgvCate.Rows(e.RowIndex).Cells(1).Value
        End If
    End Sub

    Private Sub btnCateAdd_Click(sender As Object, e As EventArgs) Handles btnCateAdd.Click
        save_sta = 1
        txtCateNa.Text = ""
        txtCateNa.Enabled = True
    End Sub

    Private Sub btnCateEdit_Click(sender As Object, e As EventArgs) Handles btnCateEdit.Click
        save_sta = 2
        txtCateNa.Enabled = True
    End Sub

    Private Sub btnCateDelete_Click(sender As Object, e As EventArgs) Handles btnCateDelete.Click
        Cate(3)
        txtCateNa.Text = ""
    End Sub

    Private Sub btnEmpAdelete_Click(sender As Object, e As EventArgs) Handles btnEmpAdelete.Click
        If txtCateNa.Text = "" Then
            Exit Sub
        End If
        Dim Sql As String = ("delete Employee where E_User = '" & txtEmpUser.Text & "'")
        Dim sqlCmd As New SqlCommand(Sql, Conn)
        sqlCmd.ExecuteNonQuery()
        Conn.Close()
        MetroFramework.MetroMessageBox.Show(Me, "", "Delete Complete", MessageBoxButtons.OK, MessageBoxIcon.Question)
        empshodata()
    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        If txtPass.PasswordChar = "#" Then
            txtPass.PasswordChar = ""
        ElseIf txtPass.PasswordChar = ControlChars.NullChar Then
            txtPass.PasswordChar = "#"
        End If
    End Sub

    Private Sub btnEmpAadd_Click(sender As Object, e As EventArgs) Handles btnEmpAadd.Click
        hide()
        PaEmployee.Visible = True
        save_sta = 1
    End Sub

    Private Sub btnCateCancle_Click(sender As Object, e As EventArgs) Handles btnCateCancle.Click
        txtCateNa.Text = ""
        txtCateNa.Enabled = False
    End Sub

    Private Sub btnEmpClose_Click(sender As Object, e As EventArgs) Handles btnEmpClose.Click
        PaEmployee.Visible = False
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        paEmAdmin.Visible = False
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Me.Close()
        Login.txtPass.PasswordChar = ""
        Login.txtUser.Text = "username"
        Login.txtPass.Text = "password"
        Login.Show()
    End Sub
End Class