<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
  Inherits System.Windows.Forms.Form

  'Form reemplaza a Dispose para limpiar la lista de componentes.
  <System.Diagnostics.DebuggerNonUserCode()>
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Requerido por el Diseñador de Windows Forms
  Private components As System.ComponentModel.IContainer

  'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
  'Se puede modificar usando el Diseñador de Windows Forms.  
  'No lo modifique con el editor de código.
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
    Me.TxtUsuario = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.TxtPw = New System.Windows.Forms.TextBox()
    Me.btinicio = New System.Windows.Forms.Button()
    Me.PictureBox2 = New System.Windows.Forms.PictureBox()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TxtUsuario
    '
    Me.TxtUsuario.ForeColor = System.Drawing.SystemColors.GrayText
    Me.TxtUsuario.Location = New System.Drawing.Point(88, 69)
    Me.TxtUsuario.Name = "TxtUsuario"
    Me.TxtUsuario.Size = New System.Drawing.Size(141, 20)
    Me.TxtUsuario.TabIndex = 9
    Me.TxtUsuario.Text = "Usuario"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.Red
    Me.Label1.Location = New System.Drawing.Point(55, 30)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(174, 15)
    Me.Label1.TabIndex = 6
    Me.Label1.Text = "BIENVENIDO AL SISTEMA TPD"
    '
    'TxtPw
    '
    Me.TxtPw.ForeColor = System.Drawing.SystemColors.GrayText
    Me.TxtPw.Location = New System.Drawing.Point(88, 106)
    Me.TxtPw.Name = "TxtPw"
    Me.TxtPw.Size = New System.Drawing.Size(141, 20)
    Me.TxtPw.TabIndex = 12
    Me.TxtPw.Text = "Contraseña"
    '
    'btinicio
    '
    Me.btinicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btinicio.ForeColor = System.Drawing.Color.DodgerBlue
    Me.btinicio.Location = New System.Drawing.Point(88, 151)
    Me.btinicio.Name = "btinicio"
    Me.btinicio.Size = New System.Drawing.Size(132, 32)
    Me.btinicio.TabIndex = 16
    Me.btinicio.Text = "INICIAR SESION"
    Me.btinicio.UseVisualStyleBackColor = True
    '
    'PictureBox2
    '
    Me.PictureBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
    Me.PictureBox2.InitialImage = Nothing
    Me.PictureBox2.Location = New System.Drawing.Point(48, 99)
    Me.PictureBox2.Name = "PictureBox2"
    Me.PictureBox2.Size = New System.Drawing.Size(34, 31)
    Me.PictureBox2.TabIndex = 19
    Me.PictureBox2.TabStop = False
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.InitialImage = Nothing
    Me.PictureBox1.Location = New System.Drawing.Point(48, 62)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(34, 31)
    Me.PictureBox1.TabIndex = 18
    Me.PictureBox1.TabStop = False
    '
    'Login
    '
    Me.AcceptButton = Me.btinicio
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.ClientSize = New System.Drawing.Size(288, 227)
    Me.Controls.Add(Me.PictureBox2)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.btinicio)
    Me.Controls.Add(Me.TxtPw)
    Me.Controls.Add(Me.TxtUsuario)
    Me.Controls.Add(Me.Label1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "Login"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = " "
    Me.TopMost = True
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TxtUsuario As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents CachedCrListaPrecio31 As TPDiamante.CachedCrListaPrecio3
  Friend WithEvents CachedCrListaPrecio32 As TPDiamante.CachedCrListaPrecio3
  Friend WithEvents CachedCrListaPrecio33 As TPDiamante.CachedCrListaPrecio3
  Friend WithEvents CachedCrListaPrecio34 As TPDiamante.CachedCrListaPrecio3
  Friend WithEvents CachedCrListaPrecio35 As TPDiamante.CachedCrListaPrecio3
  Friend WithEvents CachedCrListaPrecio36 As TPDiamante.CachedCrListaPrecio3
  Friend WithEvents TxtPw As System.Windows.Forms.TextBox
  Friend WithEvents btinicio As System.Windows.Forms.Button
  Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
