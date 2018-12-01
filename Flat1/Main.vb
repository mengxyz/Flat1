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
    Sub eng(e As KeyPressEventArgs)
        Select Case Asc(e.KeyChar)
            Case 48 To 122
                e.Handled = False
            Case 8, 13, 45
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub
    Sub num_Tel(e As KeyPressEventArgs)
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 8, 13, 45
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub
    Sub num_only(e As KeyPressEventArgs)
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 8, 13
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub
    Sub Th_eng(e As KeyPressEventArgs)
        Select Case Asc(e.KeyChar)
            Case 58 To 122
                e.Handled = False
            Case 8, 13, 32
                e.Handled = False
            Case 161 To 240
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub
    Sub Th_eng_num(e As KeyPressEventArgs)
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 58 To 122
                e.Handled = False
            Case 8, 13, 32
                e.Handled = False
            Case 161 To 240
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Sub hide()
        PaEmployee.Visible = False
        paUserAdmin.Visible = False
        paCate.Visible = False
        paCustomer.Visible = False
        paCustomerberview.Visible = False
        paCate.Visible = False
        paProduct.Visible = False
        paProductView.Visible = False
    End Sub
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Sub UserPic(ByVal a As String)
        Dim sql As String
        Dim da As SqlDataAdapter
        If a = "1" Then
            sql = "SELECT Pic from [User] where Username = '" & txtEmpUser.Text & "'"
        Else
            sql = "SELECT Pic from [User] where Username = '" & User_Na & "'"
        End If
        Module1.Connect()
        Dim tb As New DataTable
        da = New SqlDataAdapter(sql, Conn)
        da.Fill(tb)
        Dim img() As Byte
        If tb.Rows(0)(0) Is DBNull.Value And a <> "2" Then
            PicEmp.Image = Nothing
            Exit Sub
        End If
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
    Sub Usershodata()
        Module1.Connect()
        Dim sql As String
        If User_Status = 0 Then
            sql = "select Username,Fname,Lname,Tel,STR(Sex) as Sex ,STR(Status) as Status from [User]"  'where E_User = '" & "meng0052" & "'
        Else
            sql = "select Username,Pass,Fname,Lname,Tel,STR(Sex) as Sex ,STR(Status) as Status,[Add] from [User] where Username = '" & User_Na & "'"
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
            UserPic("2")
            Conn.Close()
            Exit Sub
        End If
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "Emp")
        dgvUser.ReadOnly = True
        For i As Integer = 0 To ds.Tables("emp").Rows.Count - 1
            If ds.Tables("emp").Rows(i).Item(4) = 0 Then
                ds.Tables("emp").Rows(i).Item(4) = CStr("ชาย")
            Else
                ds.Tables("emp").Rows(i).Item(4) = "หญิง"
            End If
            If ds.Tables("emp").Rows(i).Item(5) = 0 Then
                ds.Tables("emp").Rows(i).Item(5) = "ผู้ดูแลระบบ"
            Else
                ds.Tables("emp").Rows(i).Item(5) = "พนักงาน"
            End If
        Next
        dgvUser.DataSource = ds.Tables("Emp")
        With dgvUser
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "ชื่อ"
            .Columns(1).Width = 125
            .Columns(2).HeaderText = "นามสกุล"
            .Columns(2).Width = 125
            .Columns(3).HeaderText = "เบอร็โทร"
            .Columns(3).Width = 120
            .Columns(4).HeaderText = "เพศ"
            .Columns(4).Width = 100
            .Columns(5).HeaderText = "ระดับ"
            .Columns(5).Width = 100
        End With
    End Sub
    Sub Customer(ByVal x As Integer)
        Module1.Connect()
        Dim sql As String
        Dim sqlcmd As SqlCommand
        If x = 0 Then
            sql = "insert into Customer (C_ID,Fname,Lname,Tel,[Add]) values ('" & txtMID.Text & "','" & txtMfNa.Text & "','" & txtMlNa.Text & "','" & txtMTel.Text & "','" & txtMAdd.Text & "') "
        ElseIf x = 1 Then
            sql = "Update Customer set Fname = '" & txtMfNa.Text & "',Lname = '" & txtMlNa.Text & "',Tel = '" & txtMTel.Text & "',[Add] = '" & txtMAdd.Text & "' where C_ID = '" & txtMID.Text & "'"
        ElseIf x = 2 Then
            sql = "delete from Customer where C_ID = '" & CStr(dgvMember.Rows(a).Cells(0).Value) & "'"
        End If
        sqlcmd = New SqlCommand(sql, Conn)
        sqlcmd.ExecuteNonQuery()
        If x = 0 Then
            MetroFramework.MetroMessageBox.Show(Me, "บันทึกเสร็จเรียบร้อย", "", MessageBoxButtons.OK, MessageBoxIcon.Question)
        ElseIf x = 1 Then
            MetroFramework.MetroMessageBox.Show(Me, "แก้ไขเสร็จเรียบร้อย", "", MessageBoxButtons.OK, MessageBoxIcon.Question)
        ElseIf x = 2 Then
            MetroFramework.MetroMessageBox.Show(Me, "`ลบเสร็จเรียบร้อย", "", MessageBoxButtons.OK, MessageBoxIcon.Question)
        End If
        Conn.Close()
    End Sub

    Sub Product(ByRef x As Integer)
        Module1.Connect()
        Dim sql As String
        Dim sqlcmd As SqlCommand
        If x = 0 Then
            sql = "insert into Product (P_ID,Name,Brand,Price,Amount,Ca_ID) values ('" & txtPID.Text & "','" & txtPName.Text & "','" & txtPBrand.Text & "','" & txtPPrice.Text & "','" & txtPAmount.Text & "','" & cmbCate.SelectedValue & "') "
        Else
            sql = "update Product set Name = '" & txtPName.Text & "',Brand = '" & txtPBrand.Text & "',Price = '" & txtPPrice.Text & "',Amount = '" & txtPAmount.Text & "',Ca_ID = '" & cmbCate.SelectedValue & "' where P_ID = '" & txtPID.Text & "'"
        End If
        sqlcmd = New SqlCommand(sql, Conn)
        sqlcmd.ExecuteNonQuery()
        If x = 0 Then
            MetroFramework.MetroMessageBox.Show(Me, "", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Question)
        ElseIf x = 2 Then
            MetroFramework.MetroMessageBox.Show(Me, "", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Question)
        End If
        Conn.Close()
    End Sub
    Sub productshowdata()
        Module1.Connect()
        Dim sql As String = "select p.P_ID,p.Name,p.Brand,p.Amount,p.Price,c.Name from Product p,Category c where p.Ca_ID = c.Ca_ID"
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(sql, Conn)
        da.Fill(ds, "Pro")
        dgvProduct.ReadOnly = True
        dgvProduct.DataSource = ds.Tables("Pro")
        With dgvProduct
            .Columns(0).HeaderText = "รหัสสินค้า"
            .Columns(0).Width = 100
            .Columns(1).HeaderText = "ชื่อสินค้า"
            .Columns(1).Width = 150
            .Columns(2).HeaderText = "ยี่ห้อ"
            .Columns(4).HeaderText = "ราคา"
            .Columns(3).HeaderText = "จำนวนคงเหลือ"
            .Columns(3).Width = 70
            .Columns(5).HeaderText = "ประเภท"
        End With
    End Sub
    Sub Customershowdata()
        Module1.Connect()
        Dim sql As String = "select C_ID,Fname,Lname,Tel from Customer"
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter(sql, Conn)
        da.Fill(ds, "Cus")
        dgvMember.ReadOnly = True
        dgvMember.DataSource = ds.Tables("Cus")
        dgvMember.Columns(0).HeaderText = "รหัส"
        dgvMember.Columns(0).Width = 100
        dgvMember.Columns(1).HeaderText = "ชื่อ"
        dgvMember.Columns(1).Width = 195
        dgvMember.Columns(2).HeaderText = "นามสกุล"
        dgvMember.Columns(2).Width = 195
        dgvMember.Columns(3).HeaderText = "เบอร์โทร"
        dgvMember.Columns(3).Width = 130
        Conn.Close()
    End Sub

    Sub keygen(ByVal z As Integer)
        Dim Sql As String
        Dim key_Gen As String = ""
        Dim x As String
        Dim x1 As String
        Dim n1 As Integer = 4
        Module1.Connect()
        Dim sqlDr As SqlDataReader
        Dim sqlCmd As SqlCommand
        If z = 0 Then
            x = "C"
            x1 = "C0001"
            Sql = "SELECT MAX (C_ID) FROM Customer"
        ElseIf z = 1 Then
            x = "P"
            x1 = "P0001"
            Sql = "SELECT MAX (P_ID) FROM Product"
        ElseIf z = 2 Then
            x = "CA"
            x1 = "CA001"
            Sql = "SELECT MAX (Ca_ID) FROM Category"
            n1 = 3
        End If
        sqlCmd = New SqlCommand(Sql, Conn)
        sqlDr = sqlCmd.ExecuteReader
        If sqlDr.Read() Then
            If sqlDr.IsDBNull(0) Then
                key_Gen = x1
            Else
                key_Gen = sqlDr.Item(0)
                key_Gen = Trim(key_Gen)
                key_Gen = Strings.Right(key_Gen, n1)
                key_Gen = CInt(key_Gen) + 1
                key_Gen = Strings.Right(("000" & key_Gen), n1)
                key_Gen = x & key_Gen
            End If
        End If
        sqlDr.Close()
        txtCateCID.Text = key_Gen
        txtPID.Text = key_Gen
        txtMID.Text = key_Gen
    End Sub

    Private Sub dgvEmp_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUser.CellClick
        If e.RowIndex > dgvUser.RowCount - 2 Then
            txtEmpUser.Text = ""
            PicEmp.Image = Nothing
        ElseIf e.RowIndex > -1 Then
            pk = dgvUser.Rows(e.RowIndex).Cells(0).Value
            txtEmpUser.Text = dgvUser.Rows(e.RowIndex).Cells(0).Value
            a = e.RowIndex
            UserPic("1")
            btnUserAadd.Enabled = False
            btnUserAadd.BackColor = Color.FromArgb(170, 166, 157)
            btnUserAedit.Enabled = True
            btnUserAdelete.Enabled = True
            btnUserAedit.BackColor = Color.FromArgb(52, 172, 224)
            btnUserAdelete.BackColor = Color.FromArgb(52, 172, 224)
        End If
    End Sub

    Private Sub dgvEmp_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUser.CellContentDoubleClick
        txtNa.Text = dgvUser.Rows(e.RowIndex).Cells(1).Value
        txtLname.Text = dgvUser.Rows(e.RowIndex).Cells(2).Value
        txtPhone.Text = dgvUser.Rows(e.RowIndex).Cells(3).Value
        If dgvUser.Rows(e.RowIndex).Cells(4).Value = 0 Then
            rdbMen.Checked = True
        Else
            rdbWomen.Checked = True
        End If
        If dgvUser.Rows(e.RowIndex).Cells(5).Value = 0 Then
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

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles paMenuTap.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles paMenuTap.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles paMenuTap.MouseUp
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
            paUserAdmin.Visible = True
            Usershodata()
            btnUserAadd.Enabled = True
            btnUserAadd.BackColor = Color.FromArgb(52, 172, 224)
            btnUserAedit.Enabled = False
            btnUserAdelete.Enabled = False
            btnUserAedit.BackColor = Color.FromArgb(170, 166, 157)
            btnUserAdelete.BackColor = Color.FromArgb(170, 166, 157)
        Else
            hide()
            PaEmployee.Visible = True
            Usershodata()
        End If
        txtEmpUser.Text = "Username"
        PicEmp.Image = Nothing
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
        User(save_sta)
        clear(1)
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs)
        Cateshodata()
        hide()
        paCate.Visible = True
        txtCateNa.Enabled = False
    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btnUserAedit.Click
        Module1.Connect()
        Dim sql As String = "select Pass,[Add] from [User] where Username = '" & dgvUser.Rows(a).Cells(0).Value & "'"
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
        txtUser.Text = dgvUser.Rows(a).Cells(0).Value
        txtNa.Text = dgvUser.Rows(a).Cells(1).Value
        txtLname.Text = dgvUser.Rows(a).Cells(2).Value
        txtPhone.Text = dgvUser.Rows(a).Cells(3).Value
        If dgvUser.Rows(a).Cells(4).Value = "ชาย" Then
            rdbMen.Checked = True
        Else
            rdbWomen.Checked = True
        End If
        If dgvUser.Rows(a).Cells(5).Value = "ผู้ดูแลระบบ" Then
            rdbAdmin.Checked = True
        Else
            rdbUser.Checked = True
        End If
        hide()
        PaEmployee.Visible = True
        If PicEmp.Image Is Nothing Then
            PicUser.Image = Nothing
            patchpic = Nothing
        Else
            UserPic("2")
        End If
        save_sta = 2
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        PaEmployee.Visible = False
    End Sub

    Private Sub btnCateSave_Click(sender As Object, e As EventArgs) Handles btnCateSave.Click
        Cate(save_sta)
        txtCateCID.Text = Nothing
    End Sub

    Sub User(ByVal x As Integer)
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
            If patchpic = Nothing Then
                sql = "insert into [User] (Username,Pass,Fname,Lname,[Add],Tel,Sex,Status) values ('" & txtUser.Text & "','" & txtPass.Text & "','" & txtNa.Text & "','" & txtLname.Text & "','" & txtAddress.Text & "','" & txtPhone.Text & "','" & sex & "','" & status & "')"
            Else
                sql = "insert into [User] (Username,Pass,Fname,Lname,[Add],Tel,Sex,Status,Pic) values ('" & txtUser.Text & "','" & txtPass.Text & "','" & txtNa.Text & "','" & txtLname.Text & "','" & txtAddress.Text & "','" & txtPhone.Text & "','" & sex & "','" & status & "',@pic)"
            End If
        ElseIf x = 2 Then
            If patchpic = Nothing Then
                sql = "update [User] set Username = '" & txtUser.Text & "',Pass = '" & txtPass.Text & "',Fname = '" & txtNa.Text & "',Lname = '" & txtLname.Text & "',[Add] = '" & txtAddress.Text & "',Tel = '" & txtPhone.Text & "',Sex = '" & sex & "',Status = '" & status & "' where Username = '" & pk & "'"
                sqlCmd = New SqlCommand(sql, Conn)
                sqlCmd.ExecuteNonQuery()
                MessageBox.Show("Save Complete")
                Conn.Close()
                Exit Sub
            Else
                sql = "update [User] set Username = '" & txtUser.Text & "',Pass = '" & txtPass.Text & "',Fname = '" & txtNa.Text & "',Lname = '" & txtLname.Text & "',[Add] = '" & txtAddress.Text & "',Tel = '" & txtPhone.Text & "',Sex = '" & sex & "',Status = '" & status & "' ,Pic = @picwhere Username = '" & pk & "'"
            End If
        End If
        sqlCmd = New SqlCommand(sql, Conn)
        If patchpic <> Nothing Then
            sqlCmd.Parameters.Add(New SqlParameter("@pic", img))
        End If
        sqlCmd.ExecuteNonQuery()
        MessageBox.Show("Save Complete")
        Conn.Close()
    End Sub

    Sub Cate(ByVal x As Integer)
        Module1.Connect()
        Dim sql As String
        If x = 1 Then
            sql = "insert into Category (Ca_ID,Name) values ('" & txtCateCID.Text & "','" & txtCateNa.Text & "')"
        ElseIf x = 2 Then
            sql = "update Category set Name = '" & txtCateNa.Text & "' where Ca_ID = '" & dgvCate.Rows(a).Cells(0).Value & "' "
        ElseIf x = 3 Then
            sql = "delete from Category where Ca_ID = '" & dgvCate.Rows(a).Cells(0).Value & "'"
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
    Sub cateCmbLoad()
        Dim sql As String
        Dim da As SqlDataAdapter
        Dim ds As New DataSet
        Module1.Connect()
        sql = "SELECT Ca_ID,Name FROM Category ORDER BY Ca_ID"
        da = New SqlDataAdapter(sql, Conn)
        da.Fill(ds, "Category")
        If ds.Tables("Category").Rows.Count <> 0 Then
            cmbCate.DataSource = ds.Tables("Category")
            cmbCate.ValueMember = "Ca_ID"
            cmbCate.DisplayMember = "Name"
        End If
        Conn.Close()
    End Sub
    Sub Cateshodata()
        Module1.Connect()
        Dim sql As String = "select * from Category"  'where E_User = '" & "meng0052" & "'
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "cate")
        dgvCate.ReadOnly = True
        dgvCate.DataSource = ds.Tables("cate")
        dgvCate.Columns(0).HeaderText = "รหัสหมวดหมู่"
        dgvCate.Columns(0).Width = 120
        dgvCate.Columns(1).HeaderText = "ชื่อหมวดหมู่"
        dgvCate.Columns(1).Width = 250
    End Sub

    Private Sub dgvCate_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCate.CellContentClick
        If e.RowIndex > dgvCate.RowCount - 2 Then
            txtCateNa.Text = ""
        ElseIf e.RowIndex > -1 Then
            txtCateCID.Text = dgvCate.Rows(e.RowIndex).Cells(0).Value
            txtCateNa.Text = dgvCate.Rows(e.RowIndex).Cells(1).Value
            btnCateAdd.Enabled = False
            btnCateEdit.Enabled = True
            btnCateDelete.Enabled = True
            btnCateAdd.BackColor = Color.FromArgb(170, 166, 157)
            btnCateDelete.BackColor = Color.FromArgb(52, 172, 224)
            btnCateEdit.BackColor = Color.FromArgb(52, 172, 224)
        End If
    End Sub

    Private Sub dgvCate_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvCate.RowHeaderMouseClick
        a = e.RowIndex
        If e.RowIndex > dgvCate.RowCount - 2 Then
            txtCateNa.Text = ""
        Else
            txtCateCID.Text = dgvCate.Rows(e.RowIndex).Cells(0).Value
            txtCateNa.Text = dgvCate.Rows(e.RowIndex).Cells(1).Value
        End If
    End Sub

    Private Sub btnCateAdd_Click(sender As Object, e As EventArgs) Handles btnCateAdd.Click
        keygen(2)
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
        txtCateCID.Text = Nothing
    End Sub

    Private Sub btnEmpAdelete_Click(sender As Object, e As EventArgs) Handles btnUserAdelete.Click
        If txtEmpUser.Text = "" Then
            Exit Sub
        End If
        Dim Sql As String = ("delete [User] where Username = '" & txtEmpUser.Text & "'")
        Dim sqlCmd As New SqlCommand(Sql, Conn)
        sqlCmd.ExecuteNonQuery()
        Conn.Close()
        MetroFramework.MetroMessageBox.Show(Me, "", "Delete Complete", MessageBoxButtons.OK, MessageBoxIcon.Question)
        Usershodata()
        PicEmp.Image = Nothing
        txtEmpUser.Text = "Username"
    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        If txtPass.PasswordChar = "#" Then
            txtPass.PasswordChar = ""
        ElseIf txtPass.PasswordChar = ControlChars.NullChar Then
            txtPass.PasswordChar = "#"
        End If
    End Sub

    Sub clear()
        txtNa.Text = Nothing
        txtLname.Text = Nothing
        txtPhone.Text = Nothing
        txtAddress.Text = Nothing
        rdbMen.Checked = False
        rdbWomen.Checked = False
        rdbAdmin.Checked = False
        rdbUser.Checked = False
        txtUser.Text = "Username"
        txtPass.Text = "Password"
        PicUser.Image = Nothing
    End Sub

    Private Sub btnEmpAadd_Click(sender As Object, e As EventArgs) Handles btnUserAadd.Click
        hide()
        clear()
        PaEmployee.Visible = True
        save_sta = 1
    End Sub

    Private Sub btnCateCancle_Click(sender As Object, e As EventArgs) Handles btnCateCancle.Click
        txtCateNa.Text = ""
        txtCateCID.Text = ""
        btnCateAdd.Enabled = True
        btnCateAdd.BackColor = Color.FromArgb(52, 172, 224)
        btnCateEdit.Enabled = False
        btnCateDelete.Enabled = False
        btnCateSave.Enabled = False
        btnCateSave.BackColor = Color.FromArgb(170, 166, 157)
        btnCateDelete.BackColor = Color.FromArgb(170, 166, 157)
        btnCateEdit.BackColor = Color.FromArgb(170, 166, 157)
        txtCateNa.Enabled = False
    End Sub

    Private Sub btnEmpClose_Click(sender As Object, e As EventArgs) Handles btnEmpClose.Click
        PaEmployee.Visible = False
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        PicEmp.Image = Nothing
        txtEmpUser.Text = Nothing
        btnUserAadd.Enabled = True
        btnUserAadd.BackColor = Color.FromArgb(52, 172, 224)
        btnUserAedit.Enabled = False
        btnUserAdelete.Enabled = False
        btnUserAedit.BackColor = Color.FromArgb(170, 166, 157)
        btnUserAdelete.BackColor = Color.FromArgb(170, 166, 157)
        paUserAdmin.Visible = False
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs)
        Me.Close()
        Login.txtPass.PasswordChar = ""
        Login.txtUser.Text = "username"
        Login.txtPass.Text = "password"
        Login.Show()
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        hide()
        Customershowdata()
        paCustomerberview.Visible = True
        btnMEdit.Enabled = False
        btnMDelete.Enabled = False
        btnMEdit.BackColor = Color.FromArgb(170, 166, 157)
        btnMDelete.BackColor = Color.FromArgb(170, 166, 157)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        hide()
        btnCateAdd.Enabled = True
        btnCateAdd.BackColor = Color.FromArgb(52, 172, 224)
        btnCateEdit.Enabled = False
        btnCateDelete.Enabled = False
        btnCateSave.Enabled = False
        btnCateSave.BackColor = Color.FromArgb(170, 166, 157)
        btnCateDelete.BackColor = Color.FromArgb(170, 166, 157)
        btnCateEdit.BackColor = Color.FromArgb(170, 166, 157)
        paCate.Visible = True
        Cateshodata()
    End Sub

    Private Sub btnMadd_Click(sender As Object, e As EventArgs) Handles btnMadd.Click
        paCustomer.Visible = True
        save_sta = 0
        keygen(0)
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles btnCuSave.Click
        Customer(save_sta)
        paCustomer.Visible = False
        paCustomerberview.Visible = True
        Customershowdata()
    End Sub

    Private Sub btnPAdd_Click(sender As Object, e As EventArgs) Handles btnPAdd.Click
        save_sta = 0
        keygen(1)
        cateCmbLoad()
        cmbCate.SelectedIndex = -1
        paProduct.Visible = True
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        hide()
        paProductView.Visible = True

    End Sub

    Private Sub btnEmpAcancle_Click(sender As Object, e As EventArgs) Handles btnUserAcancle.Click
        PicEmp.Image = Nothing
        txtEmpUser.Text = Nothing
        btnUserAadd.Enabled = True
        btnUserAadd.BackColor = Color.FromArgb(52, 172, 224)
        btnUserAedit.Enabled = False
        btnUserAdelete.Enabled = False
        btnUserAedit.BackColor = Color.FromArgb(170, 166, 157)
        btnUserAdelete.BackColor = Color.FromArgb(170, 166, 157)
    End Sub

    Private Sub btnMEdit_Click(sender As Object, e As EventArgs) Handles btnMEdit.Click
        Module1.Connect()
        Dim sql As String = "select C_ID,Fname,Lname,Tel,[Add] from Customer where C_ID = '" & dgvMember.Rows(a).Cells(0).Value & "'"
        Dim sqlDr As SqlDataReader
        Dim sqlcmd As New SqlCommand(sql, Conn)
        sqlDr = sqlcmd.ExecuteReader
        If sqlDr.Read Then
            txtMID.Text = sqlDr.Item(0).ToString
            txtMfNa.Text = sqlDr.Item(1).ToString
            txtMlNa.Text = sqlDr.Item(2).ToString
            txtMTel.Text = sqlDr.Item(3).ToString
            txtMAdd.Text = sqlDr.Item(4).ToString
        End If
        Conn.Close()
        paCustomer.Show()
        hide()
        paCustomer.Visible = True
        save_sta = 1
    End Sub

    Private Sub dgvMember_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMember.CellContentClick
        If e.RowIndex > -1 Then
            pk = dgvMember.Rows(e.RowIndex).Cells(0).Value
            a = e.RowIndex
            btnMEdit.Enabled = True
            btnMDelete.Enabled = True
            btnMadd.Enabled = False
            btnMadd.BackColor = Color.FromArgb(170, 166, 157)
            btnMEdit.BackColor = Color.FromArgb(52, 172, 224)
            btnMDelete.BackColor = Color.FromArgb(52, 172, 224)
        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        paCustomer.Visible = False
    End Sub

    Private Sub btnCuCancle_Click(sender As Object, e As EventArgs) Handles btnCuCancle.Click
        paCustomer.Visible = False
        paCustomerberview.Visible = True
        btnMEdit.BackColor = Color.FromArgb(170, 166, 157)
        btnMDelete.BackColor = Color.FromArgb(170, 166, 157)
        btnMadd.Enabled = True
        btnMEdit.Enabled = False
        btnMDelete.Enabled = False
        btnMadd.BackColor = Color.FromArgb(52, 172, 224)
        Customershowdata()
    End Sub

    Private Sub btnItem_Click(sender As Object, e As EventArgs) Handles btnItem.Click
        hide()
        btnPAdd.Enabled = True
        btnPAdd.BackColor = Color.FromArgb(52, 172, 224)
        paProductView.Visible = True
        btnPEdit.Enabled = False
        btnPDelete.Enabled = False
        btnPEdit.BackColor = Color.FromArgb(170, 166, 157)
        btnPDelete.BackColor = Color.FromArgb(170, 166, 157)
        productshowdata()
    End Sub

    Private Sub btnPSave_Click(sender As Object, e As EventArgs) Handles btnPSave.Click
        Product(save_sta)
        paProduct.Visible = False
        paProductView.Visible = True
        productshowdata()
    End Sub

    Private Sub btnMCancle_Click(sender As Object, e As EventArgs) Handles btnMCancle.Click
        btnMEdit.BackColor = Color.FromArgb(170, 166, 157)
        btnMDelete.BackColor = Color.FromArgb(170, 166, 157)
        btnMadd.Enabled = True
        btnMEdit.Enabled = False
        btnMDelete.Enabled = False
        btnMadd.BackColor = Color.FromArgb(52, 172, 243)
    End Sub

    Private Sub btnPCancle_Click(sender As Object, e As EventArgs) Handles btnPCancle.Click
        btnPEdit.BackColor = Color.FromArgb(170, 166, 157)
        btnPDelete.BackColor = Color.FromArgb(170, 166, 157)
        btnPAdd.Enabled = True
        btnPEdit.Enabled = False
        btnPDelete.Enabled = False
        btnPAdd.BackColor = Color.FromArgb(52, 172, 224)
    End Sub

    Private Sub dgvProduct_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProduct.CellContentClick
        If e.RowIndex > -1 Then
            pk = dgvProduct.Rows(e.RowIndex).Cells(0).Value
            a = e.RowIndex
            btnPEdit.Enabled = True
            btnPDelete.Enabled = True
            btnPAdd.Enabled = False
            btnPAdd.BackColor = Color.FromArgb(170, 166, 157)
            btnPEdit.BackColor = Color.FromArgb(52, 172, 224)
            btnPDelete.BackColor = Color.FromArgb(52, 172, 224)
        End If
    End Sub

    Private Sub btnPEdit_Click(sender As Object, e As EventArgs) Handles btnPEdit.Click
        cateCmbLoad()
        Module1.Connect()
        Dim sql As String = "select p.P_ID,p.Name,p.Brand,p.Price,p.Amount,c.Ca_ID from Product p,Category c where p.P_ID = '" & dgvProduct.Rows(a).Cells(0).Value & "' and p.Ca_ID = c.Ca_ID"
        Dim sqlDr As SqlDataReader
        Dim sqlcmd As New SqlCommand(sql, Conn)
        sqlDr = sqlcmd.ExecuteReader
        If sqlDr.Read Then
            txtPID.Text = sqlDr.Item(0).ToString
            txtPName.Text = sqlDr.Item(1).ToString
            txtPBrand.Text = sqlDr.Item(2).ToString
            txtPPrice.Text = sqlDr.Item(3).ToString
            txtPAmount.Text = sqlDr.Item(4).ToString
            cmbCate.SelectedValue = sqlDr.Item(5)
        End If
        Conn.Close()
        hide()
        paProduct.Visible = True
        save_sta = 2
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Sale.Show()
    End Sub

    Private Sub txtCateNa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCateNa.KeyPress
        Th_eng_num(e)
    End Sub

    Private Sub txtMfNa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMfNa.KeyPress
        Th_eng(e)
    End Sub

    Private Sub txtMlNa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMlNa.KeyPress
        Th_eng(e)
    End Sub

    Private Sub txtMTel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMTel.KeyPress
        num_Tel(e)
    End Sub

    Private Sub txtNa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNa.KeyPress
        Th_eng(e)
    End Sub

    Private Sub txtLname_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLname.KeyPress
        Th_eng(e)
    End Sub

    Private Sub txtUser_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUser.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 65 To 90
                e.Handled = False
            Case 97 To 122
                e.Handled = False
            Case 8, 46
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub txtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhone.KeyPress
        num_Tel(e)
    End Sub

    Private Sub txtPName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPName.KeyPress
        Th_eng_num(e)
    End Sub

    Private Sub txtPBrand_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPBrand.KeyPress
        Th_eng_num(e)
    End Sub

    Private Sub txtPPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPPrice.KeyPress
        num_only(e)
    End Sub

    Private Sub txtPAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPAmount.KeyPress
        num_only(e)
    End Sub

    Private Sub btnMDelete_Click(sender As Object, e As EventArgs) Handles btnMDelete.Click
        Customer(2)
        Customershowdata()
        btnMEdit.BackColor = Color.FromArgb(170, 166, 157)
        btnMDelete.BackColor = Color.FromArgb(170, 166, 157)
        btnMadd.Enabled = True
        btnMEdit.Enabled = False
        btnMDelete.Enabled = False
        btnMadd.BackColor = Color.FromArgb(52, 172, 243)
    End Sub
End Class