Imports System.Data.SqlClient
Imports System.Data
Public Class Sale
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim row, am As Integer
    Dim sum As Double
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
        MemberSelect.Show()
    End Sub

    Sub Save()
        Module1.Connect()
        Dim sql As String
        Dim sqlCmd As SqlCommand
        Dim Orderdate As String = Today.ToString("s")
        sql = "insert into [Order] (Date,Net,C_ID,Username) values ('" & CDate(Orderdate) & "','" & CDbl(lblSum.Text) & "','" & txtC_ID.Text & "','" & cmbUser.SelectedValue & "')"
        sqlCmd = New SqlCommand(sql, Conn)
        sqlCmd.ExecuteNonQuery()
        sql = "select max(OR_ID) from [Order]"
        sqlCmd = New SqlCommand(sql, Conn)
        Dim dr As SqlDataReader = sqlCmd.ExecuteReader
        dr.Read()
        Dim OR_ID As String = CStr(dr.Item(0))
        dr.Close()
        For i As Integer = 0 To dgvOrder.RowCount - 1
            Dim P_ID As String = dgvOrder.Rows(i).Cells(0).Value
            Dim Num As Integer = CInt(dgvOrder.Rows(i).Cells(3).Value)
            Dim Price As Double = CDbl(dgvOrder.Rows(i).Cells(2).Value)
            Dim Total_Price As Double = CDbl(dgvOrder.Rows(i).Cells(4).Value)
            sql = "Insert into Order_Detail (OR_ID,P_ID,Num,Price,Total_Price) values ('" & OR_ID & "','" & P_ID & "','" & Num & "','" & Price & "','" & Total_Price & "')"
            sqlCmd = New SqlCommand(sql, Conn)
            sqlCmd.ExecuteNonQuery()
        Next
        For i As Integer = 0 To dgvProduct.RowCount - 2
            sql = "update Product set Amount = '" & CInt(dgvProduct.Rows(i).Cells(2).Value) & "' where P_ID = '" & dgvProduct.Rows(i).Cells(0).Value & "'"
            sqlCmd = New SqlCommand(sql, Conn)
            sqlCmd.ExecuteNonQuery()
        Next
        txtID.Text = OR_ID
        MetroFramework.MetroMessageBox.Show(Me, "", "บันทึกข้อมูลเรียบร้อย", MessageBoxButtons.OK, MessageBoxIcon.Question)
    End Sub

    Sub UserLoad()
        Module1.Connect()
        Dim sql As String = "select E_User,(E_Fname +' '+ E_Lname) as name from Employee"
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "User")
        If ds.Tables("User").Rows.Count <> 0 Then
            cmbUser.DataSource = ds.Tables("User")
            cmbUser.ValueMember = "E_User"
            cmbUser.DisplayMember = "name"
        End If
        Conn.Close()
    End Sub

    Private Sub Sale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        showdata()
        UserLoad()
        clear()
        dgvProduct.Enabled = False
    End Sub
    Sub showdata()
        Module1.Connect()
        Dim sql As String = "select P_ID,Name,Amount from Product"
        Dim da As New SqlDataAdapter(sql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "Pro")
        dgvProduct.ReadOnly = True
        dgvOrder.ReadOnly = True
        dgvProduct.DataSource = ds.Tables("Pro")
        With dgvProduct
            .Columns(0).HeaderText = "รหัสสินค้า"
            .Columns(0).Width = 100
            .Columns(1).HeaderText = "ชื่อสินค้า"
            .Columns(1).Width = 210
            .Columns(2).HeaderText = "จำนวนคงเหลือ"
            .Columns(2).Width = 100
        End With
        Conn.Close()
    End Sub
    Sub clear()
        txtAmount.Text = Nothing
        'txtC_ID.Text = Nothing
        'txtCNa.Text = Nothing
        txtNum.Text = Nothing
        txtNum.Enabled = False
        txtP_ID.Text = Nothing
        txtPname.Text = Nothing
        txtPrice.Text = Nothing
        btnremove.Enabled = False
        btnremove.BackColor = Color.FromArgb(170, 166, 157)
        btAdd.Enabled = False
        btAdd.BackColor = Color.FromArgb(170, 166, 157)
        btnSave.Enabled = False
        btnSave.BackColor = Color.FromArgb(170, 166, 157)
        btnPrint.Enabled = False
        btnPrint.BackColor = Color.FromArgb(170, 166, 157)
    End Sub

    Private Sub dgvProduct_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProduct.CellContentClick
        Module1.Connect()
        row = e.RowIndex
        txtP_ID.Text = dgvProduct.Rows(e.RowIndex).Cells(0).Value
        txtPname.Text = dgvProduct.Rows(e.RowIndex).Cells(1).Value
        txtAmount.Text = dgvProduct.Rows(e.RowIndex).Cells(2).Value
        Dim sql As String = "select Price from Product where P_ID = '" & CStr(dgvProduct.Rows(e.RowIndex).Cells(0).Value) & "'"
        Dim dr As SqlDataReader
        Dim sqlcmd As New SqlCommand(sql, Conn)
        dr = sqlcmd.ExecuteReader
        If dr.Read Then
            txtPrice.Text = CInt(dr.Item(0))
        End If
        btAdd.Enabled = True
        btAdd.BackColor = Color.FromArgb(52, 172, 224)
        txtNum.Enabled = True
        Conn.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btAdd.Click
        If txtC_ID.Text = Nothing Then
            MetroFramework.MetroMessageBox.Show(Me, "", "กรุณาเลือกลูกค้า", MessageBoxButtons.OK, MessageBoxIcon.Question)
            Exit Sub
        ElseIf txtNum.Text = Nothing Then
            MetroFramework.MetroMessageBox.Show(Me, "", "กรุณาใส่จำนวนที่ขาย", MessageBoxButtons.OK, MessageBoxIcon.Question)
            Exit Sub
        ElseIf CInt(txtNum.Text) > CInt(dgvProduct.Rows(row).Cells(2).Value) Then
            MetroFramework.MetroMessageBox.Show(Me, "", "จำนวนคงเหลือไม่พอ", MessageBoxButtons.OK, MessageBoxIcon.Question)
            Exit Sub
        End If
        Dim Gname, GID As String
        Dim Gnum, GAmount As Integer
        Dim Gsum, Gprice As Double
        GID = txtP_ID.Text
        Gname = txtPname.Text
        Gprice = txtPrice.Text
        Gnum = txtNum.Text
        GAmount = CInt(txtAmount.Text)
        Gsum = CInt(txtPrice.Text) * CInt(txtNum.Text)

        If dgvOrder.RowCount - 2 >= 0 Then
            Dim a As Integer = 0
            While dgvOrder.Rows(a).Cells(0).Value <> dgvProduct.Rows(row).Cells(0).Value And a <= dgvOrder.RowCount - 2
                a += 1
            End While
            If dgvOrder.Rows(a).Cells(0).Value = dgvProduct.Rows(row).Cells(0).Value Then
                MessageBox.Show("Found Diff : " & dgvOrder.Rows(a).Cells(0).Value)
                dgvOrder.Rows(a).Cells(3).Value = CInt(dgvOrder.Rows(a).Cells(3).Value) + CInt(txtNum.Text)
                dgvOrder.Rows(a).Cells(4).Value = CInt(dgvOrder.Rows(a).Cells(4).Value) + CInt(txtNum.Text) * CInt(txtPrice.Text)
                dgvProduct.Rows(row).Cells(2).Value = CInt(txtAmount.Text) - CInt(txtNum.Text)
                sum = 0
                For i As Integer = 0 To dgvOrder.RowCount - 2
                    sum += CInt(dgvOrder.Rows(i).Cells(4).Value)
                Next
                lblSum.Text = sum.ToString("#,##.00")
                clear()
                dgvProduct.Rows(row).Cells(2).Value = GAmount - Gnum
                btnSave.Enabled = True
                btnSave.BackColor = Color.FromArgb(52, 172, 224)
                Exit Sub
            End If
        End If

        dgvOrder.Rows.Add(GID, Gname, Gprice, Gnum, Gsum)
        sum = 0
        For i As Integer = 0 To dgvOrder.RowCount - 2
            sum += CInt(dgvOrder.Rows(i).Cells(4).Value)
        Next
        lblSum.Text = sum.ToString("#,##.00")
        clear()
        dgvProduct.Rows(row).Cells(2).Value = GAmount - Gnum
        btnSave.Enabled = True
        btnSave.BackColor = Color.FromArgb(52, 172, 224)
    End Sub

    Private Sub dgvOrder_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrder.CellContentClick
        row = e.RowIndex
        btnremove.Enabled = True
        btnremove.BackColor = Color.FromArgb(52, 172, 224)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnremove.Click
        Dim a As Integer = 0
        While dgvProduct.Rows(a).Cells(0).Value <> dgvOrder.Rows(row).Cells(0).Value
            a += 1
        End While
        dgvProduct.Rows(a).Cells(2).Value = CInt(dgvProduct.Rows(a).Cells(2).Value) + CInt(dgvOrder.Rows(row).Cells(3).Value)
        If dgvOrder.RowCount = 2 Then
            sum = 0
            lblSum.Text = "0.00"
            dgvOrder.Rows.Remove(dgvOrder.CurrentRow)
        Else
            sum -= CInt(dgvOrder.Rows(row).Cells(4).Value)
            dgvOrder.Rows.Remove(dgvOrder.CurrentRow)
            lblSum.Text = sum.ToString("#,##.00")
        End If

        btnremove.Enabled = False
        btnremove.BackColor = Color.FromArgb(170, 166, 157)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        MessageBox.Show(dgvOrder.RowCount - 1)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        btnAdd.Enabled = False
        btnAdd.BackColor = Color.FromArgb(170, 166, 157)
        dgvProduct .Enabled = True 
    End Sub

    Private Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        clear()
        dgvOrder.Rows.Clear()
        btnAdd.Enabled = True
        btnAdd.BackColor = Color.FromArgb(52, 172, 224)
        sum = 0
        lblSum.Text = "0.00"
        dgvProduct.Enabled = False
        showdata()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
        clear()
        btnPrint.Enabled = True
        btnPrint.BackColor = Color.FromArgb(52, 172, 224)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        clear()
        dgvOrder.Rows.Clear()
        btnAdd.Enabled = True
        btnAdd.BackColor = Color.FromArgb(52, 172, 224)
        dgvProduct.Enabled = False
        sum = 0
        lblSum.Text = "0.00"
        txtID.Text = Nothing
        showdata()
    End Sub
End Class