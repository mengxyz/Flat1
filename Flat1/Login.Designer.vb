<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape2 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button8 = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape2, Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(351, 432)
        Me.ShapeContainer1.TabIndex = 0
        Me.ShapeContainer1.TabStop = False
        '
        'RectangleShape2
        '
        Me.RectangleShape2.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(157, Byte), Integer))
        Me.RectangleShape2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.RectangleShape2.BorderColor = System.Drawing.Color.Gray
        Me.RectangleShape2.BorderWidth = 5
        Me.RectangleShape2.CornerRadius = 34
        Me.RectangleShape2.FillGradientColor = System.Drawing.Color.Black
        Me.RectangleShape2.Location = New System.Drawing.Point(37, 152)
        Me.RectangleShape2.Name = "RectangleShape2"
        Me.RectangleShape2.Size = New System.Drawing.Size(279, 68)
        '
        'RectangleShape1
        '
        Me.RectangleShape1.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(157, Byte), Integer))
        Me.RectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.RectangleShape1.BorderColor = System.Drawing.Color.Gray
        Me.RectangleShape1.BorderWidth = 5
        Me.RectangleShape1.CornerRadius = 34
        Me.RectangleShape1.FillGradientColor = System.Drawing.Color.Black
        Me.RectangleShape1.Location = New System.Drawing.Point(39, 251)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(279, 68)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("FC Active", 50.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(109, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(143, 65)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Login"
        '
        'Button1
        '
        Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(112, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("FC Active", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(120, 347)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(112, 39)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "ตกลง"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'txtUser
        '
        Me.txtUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(157, Byte), Integer))
        Me.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUser.Font = New System.Drawing.Font("FC Active", 25.0!)
        Me.txtUser.ForeColor = System.Drawing.Color.White
        Me.txtUser.Location = New System.Drawing.Point(68, 170)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(219, 33)
        Me.txtUser.TabIndex = 3
        Me.txtUser.Text = "username"
        Me.txtUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPass
        '
        Me.txtPass.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(157, Byte), Integer))
        Me.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPass.Font = New System.Drawing.Font("FC Active", 25.0!)
        Me.txtPass.ForeColor = System.Drawing.Color.White
        Me.txtPass.Location = New System.Drawing.Point(68, 271)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.Size = New System.Drawing.Size(219, 33)
        Me.txtPass.TabIndex = 4
        Me.txtPass.Text = "password"
        Me.txtPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.Flat1.My.Resources.Resources.icons8_close_window_48
        Me.PictureBox1.Location = New System.Drawing.Point(319, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(25, 25)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(157, Byte), Integer))
        Me.Button8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button8.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(157, Byte), Integer))
        Me.Button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(157, Byte), Integer))
        Me.Button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(157, Byte), Integer))
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Font = New System.Drawing.Font("FC Active", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.Image = Global.Flat1.My.Resources.Resources.icons8_hide_24
        Me.Button8.Location = New System.Drawing.Point(275, 271)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(30, 30)
        Me.Button8.TabIndex = 26
        Me.Button8.UseVisualStyleBackColor = False
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(351, 432)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtPass)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Login"
        Me.Text = "Form1"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtPass As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button8 As System.Windows.Forms.Button
End Class
