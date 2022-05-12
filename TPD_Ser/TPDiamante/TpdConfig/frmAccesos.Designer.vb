<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAccesos
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
    Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
    Me.GrabarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.lblUsuario = New System.Windows.Forms.Label()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.TreeView1 = New System.Windows.Forms.TreeView()
    Me.Panel3 = New System.Windows.Forms.Panel()
    Me.ChLBoxEspeciales = New System.Windows.Forms.CheckedListBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.chkMenuEspecial = New System.Windows.Forms.CheckBox()
    Me.lblcveuser = New System.Windows.Forms.Label()
    Me.MenuStrip1.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.Panel3.SuspendLayout()
    Me.SuspendLayout()
    '
    'MenuStrip1
    '
    Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
    Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GrabarToolStripMenuItem, Me.SalirToolStripMenuItem})
    Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
    Me.MenuStrip1.Name = "MenuStrip1"
    Me.MenuStrip1.Size = New System.Drawing.Size(669, 29)
    Me.MenuStrip1.TabIndex = 5
    Me.MenuStrip1.Text = "MenuStrip1"
    '
    'GrabarToolStripMenuItem
    '
    Me.GrabarToolStripMenuItem.Name = "GrabarToolStripMenuItem"
    Me.GrabarToolStripMenuItem.Size = New System.Drawing.Size(73, 25)
    Me.GrabarToolStripMenuItem.Text = "&Grabar"
    '
    'SalirToolStripMenuItem
    '
    Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
    Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(56, 25)
    Me.SalirToolStripMenuItem.Text = "&Salir"
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.lblUsuario)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
    Me.Panel1.Location = New System.Drawing.Point(0, 29)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(669, 34)
    Me.Panel1.TabIndex = 9
    '
    'lblUsuario
    '
    Me.lblUsuario.AutoSize = True
    Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblUsuario.Location = New System.Drawing.Point(17, 5)
    Me.lblUsuario.Name = "lblUsuario"
    Me.lblUsuario.Size = New System.Drawing.Size(188, 24)
    Me.lblUsuario.TabIndex = 7
    Me.lblUsuario.Text = "PERMISOS PARA: "
    '
    'Panel2
    '
    Me.Panel2.Controls.Add(Me.TreeView1)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
    Me.Panel2.Location = New System.Drawing.Point(0, 63)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(335, 715)
    Me.Panel2.TabIndex = 10
    '
    'TreeView1
    '
    Me.TreeView1.CheckBoxes = True
    Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TreeView1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TreeView1.Location = New System.Drawing.Point(0, 0)
    Me.TreeView1.Name = "TreeView1"
    Me.TreeView1.Size = New System.Drawing.Size(335, 715)
    Me.TreeView1.TabIndex = 5
    '
    'Panel3
    '
    Me.Panel3.Controls.Add(Me.ChLBoxEspeciales)
    Me.Panel3.Controls.Add(Me.Label1)
    Me.Panel3.Controls.Add(Me.chkMenuEspecial)
    Me.Panel3.Controls.Add(Me.lblcveuser)
    Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
    Me.Panel3.Location = New System.Drawing.Point(341, 63)
    Me.Panel3.Name = "Panel3"
    Me.Panel3.Size = New System.Drawing.Size(328, 715)
    Me.Panel3.TabIndex = 11
    '
    'ChLBoxEspeciales
    '
    Me.ChLBoxEspeciales.FormattingEnabled = True
    Me.ChLBoxEspeciales.Location = New System.Drawing.Point(10, 57)
    Me.ChLBoxEspeciales.Name = "ChLBoxEspeciales"
    Me.ChLBoxEspeciales.Size = New System.Drawing.Size(309, 604)
    Me.ChLBoxEspeciales.TabIndex = 12
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(8, 7)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(46, 13)
    Me.Label1.TabIndex = 11
    Me.Label1.Text = "Usuario:"
    '
    'chkMenuEspecial
    '
    Me.chkMenuEspecial.AutoSize = True
    Me.chkMenuEspecial.Location = New System.Drawing.Point(10, 30)
    Me.chkMenuEspecial.Name = "chkMenuEspecial"
    Me.chkMenuEspecial.Size = New System.Drawing.Size(96, 17)
    Me.chkMenuEspecial.TabIndex = 10
    Me.chkMenuEspecial.Text = "Menú Especial"
    Me.chkMenuEspecial.UseVisualStyleBackColor = True
    '
    'lblcveuser
    '
    Me.lblcveuser.AutoSize = True
    Me.lblcveuser.Location = New System.Drawing.Point(60, 7)
    Me.lblcveuser.Name = "lblcveuser"
    Me.lblcveuser.Size = New System.Drawing.Size(55, 13)
    Me.lblcveuser.TabIndex = 9
    Me.lblcveuser.Text = "lblcveuser"
    '
    'frmAccesos
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(669, 778)
    Me.ControlBox = False
    Me.Controls.Add(Me.Panel3)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.MenuStrip1)
    Me.MainMenuStrip = Me.MenuStrip1
    Me.Name = "frmAccesos"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Configuración de permisos para sistema TPD"
    Me.MenuStrip1.ResumeLayout(False)
    Me.MenuStrip1.PerformLayout()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.Panel3.ResumeLayout(False)
    Me.Panel3.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents MenuStrip1 As MenuStrip
  Friend WithEvents GrabarToolStripMenuItem As ToolStripMenuItem
  Friend WithEvents SalirToolStripMenuItem As ToolStripMenuItem
  Friend WithEvents Panel1 As Panel
  Friend WithEvents lblUsuario As Label
  Friend WithEvents Panel2 As Panel
  Friend WithEvents TreeView1 As TreeView
  Friend WithEvents Panel3 As Panel
  Friend WithEvents Label1 As Label
  Friend WithEvents chkMenuEspecial As CheckBox
  Friend WithEvents lblcveuser As Label
  Friend WithEvents ChLBoxEspeciales As CheckedListBox
End Class
