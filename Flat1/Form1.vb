Imports System.IO
Imports System.Data.SqlClient

Public Class Form1
    Dim patchpic As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim opf As New OpenFileDialog
        opf.Filter = "Choose Image (*.jpg)|*.jpg"
        If opf.ShowDialog = DialogResult.OK Then
            patchpic = opf.FileName
            PictureBox1.ImageLocation = patchpic
        Else
            PictureBox1.Image = Nothing
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim img As Byte() = Nothing
        Dim fs As New FileStream(patchpic, FileMode.Open, FileAccess.Read)
        Dim br As New BinaryReader(fs)
        img = br.ReadBytes(CInt(fs.Length))
        Module1.Connect()
        Dim sql As String = "insert into Pic(Image) values (@pic)"
        Dim sqlCmd As New SqlCommand(sql, Conn)
        sqlCmd.Parameters.Add(New SqlParameter("@pic", img))
        sqlCmd.ExecuteNonQuery()
        MessageBox.Show("Save Complete")
        Conn.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim sql As String
        Dim da As SqlDataAdapter
        sql = "SELECT * FROM Pic where ID = '" & CInt(TextBox1.Text) & "'"
        Module1.Connect()
        Dim tb As New DataTable
        da = New SqlDataAdapter(sql, Conn)
        da.Fill(tb)
        TextBox2.Text = tb.Rows(0)(0).ToString()
        Dim img() As Byte
        img = tb.Rows(0)(1)
        Dim ms As New MemoryStream(img)
        PictureBox1.Image = Image.FromStream(ms)
        ms.Close()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        MetroFramework.MetroMessageBox.Show(Me, "รหัสผ่านถูกต้อง", "รายงาน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub
End Class