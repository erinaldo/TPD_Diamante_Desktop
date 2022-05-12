<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintUbicaciones
 Inherits System.Windows.Forms.Form

 'Form reemplaza a Dispose para limpiar la lista de componentes.
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

 'Requerido por el Diseñador de Windows Forms
 Private components As System.ComponentModel.IContainer

 'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
 'Se puede modificar usando el Diseñador de Windows Forms.  
 'No lo modifique con el editor de código.
 <System.Diagnostics.DebuggerStepThrough()> _
 Private Sub InitializeComponent()
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.btnGenerar = New System.Windows.Forms.Button()
  Me.txtSCC = New System.Windows.Forms.TextBox()
  Me.Label9 = New System.Windows.Forms.Label()
  Me.txtSPCIO = New System.Windows.Forms.TextBox()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.txtNVL = New System.Windows.Forms.TextBox()
  Me.Label7 = New System.Windows.Forms.Label()
  Me.txtRCK = New System.Windows.Forms.TextBox()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.txtBLQ = New System.Windows.Forms.TextBox()
  Me.txtCopias = New System.Windows.Forms.TextBox()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.Button1 = New System.Windows.Forms.Button()
  Me.pbBarCode = New System.Windows.Forms.PictureBox()
  Me.Panel1.SuspendLayout()
  CType(Me.pbBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'Panel1
  '
  Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel1.Controls.Add(Me.btnGenerar)
  Me.Panel1.Controls.Add(Me.txtSCC)
  Me.Panel1.Controls.Add(Me.Label9)
  Me.Panel1.Controls.Add(Me.txtSPCIO)
  Me.Panel1.Controls.Add(Me.Label8)
  Me.Panel1.Controls.Add(Me.txtNVL)
  Me.Panel1.Controls.Add(Me.Label7)
  Me.Panel1.Controls.Add(Me.txtRCK)
  Me.Panel1.Controls.Add(Me.Label6)
  Me.Panel1.Controls.Add(Me.txtBLQ)
  Me.Panel1.Controls.Add(Me.txtCopias)
  Me.Panel1.Controls.Add(Me.Label5)
  Me.Panel1.Controls.Add(Me.Label3)
  Me.Panel1.Controls.Add(Me.Button1)
  Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
  Me.Panel1.Location = New System.Drawing.Point(0, 0)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(309, 168)
  Me.Panel1.TabIndex = 2
  '
  'btnGenerar
  '
  Me.btnGenerar.Location = New System.Drawing.Point(204, 9)
  Me.btnGenerar.Name = "btnGenerar"
  Me.btnGenerar.Size = New System.Drawing.Size(75, 23)
  Me.btnGenerar.TabIndex = 6
  Me.btnGenerar.Text = "&Generar"
  Me.btnGenerar.UseVisualStyleBackColor = True
  '
  'txtSCC
  '
  Me.txtSCC.Location = New System.Drawing.Point(107, 41)
  Me.txtSCC.MaxLength = 2
  Me.txtSCC.Name = "txtSCC"
  Me.txtSCC.Size = New System.Drawing.Size(42, 20)
  Me.txtSCC.TabIndex = 2
  '
  'Label9
  '
  Me.Label9.Location = New System.Drawing.Point(10, 44)
  Me.Label9.Name = "Label9"
  Me.Label9.Size = New System.Drawing.Size(91, 13)
  Me.Label9.TabIndex = 18
  Me.Label9.Text = "SECCION:"
  Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'txtSPCIO
  '
  Me.txtSPCIO.Location = New System.Drawing.Point(107, 125)
  Me.txtSPCIO.MaxLength = 3
  Me.txtSPCIO.Name = "txtSPCIO"
  Me.txtSPCIO.Size = New System.Drawing.Size(42, 20)
  Me.txtSPCIO.TabIndex = 5
  '
  'Label8
  '
  Me.Label8.Location = New System.Drawing.Point(10, 128)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(91, 13)
  Me.Label8.TabIndex = 16
  Me.Label8.Text = "ESPACIO:"
  Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'txtNVL
  '
  Me.txtNVL.Location = New System.Drawing.Point(107, 97)
  Me.txtNVL.MaxLength = 1
  Me.txtNVL.Name = "txtNVL"
  Me.txtNVL.Size = New System.Drawing.Size(42, 20)
  Me.txtNVL.TabIndex = 4
  '
  'Label7
  '
  Me.Label7.Location = New System.Drawing.Point(10, 100)
  Me.Label7.Name = "Label7"
  Me.Label7.Size = New System.Drawing.Size(91, 13)
  Me.Label7.TabIndex = 14
  Me.Label7.Text = "NIVEL:"
  Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'txtRCK
  '
  Me.txtRCK.Location = New System.Drawing.Point(107, 71)
  Me.txtRCK.MaxLength = 1
  Me.txtRCK.Name = "txtRCK"
  Me.txtRCK.Size = New System.Drawing.Size(42, 20)
  Me.txtRCK.TabIndex = 3
  '
  'Label6
  '
  Me.Label6.Location = New System.Drawing.Point(10, 74)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(91, 13)
  Me.Label6.TabIndex = 12
  Me.Label6.Text = "RACK:"
  Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'txtBLQ
  '
  Me.txtBLQ.Location = New System.Drawing.Point(107, 12)
  Me.txtBLQ.MaxLength = 2
  Me.txtBLQ.Name = "txtBLQ"
  Me.txtBLQ.Size = New System.Drawing.Size(42, 20)
  Me.txtBLQ.TabIndex = 1
  '
  'txtCopias
  '
  Me.txtCopias.Location = New System.Drawing.Point(214, 89)
  Me.txtCopias.Name = "txtCopias"
  Me.txtCopias.Size = New System.Drawing.Size(46, 20)
  Me.txtCopias.TabIndex = 7
  '
  'Label5
  '
  Me.Label5.AutoSize = True
  Me.Label5.Location = New System.Drawing.Point(218, 67)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(42, 13)
  Me.Label5.TabIndex = 10
  Me.Label5.Text = "Copias:"
  '
  'Label3
  '
  Me.Label3.Location = New System.Drawing.Point(10, 15)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(91, 13)
  Me.Label3.TabIndex = 6
  Me.Label3.Text = "BLOQUE:"
  Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Button1
  '
  Me.Button1.Enabled = False
  Me.Button1.Location = New System.Drawing.Point(204, 122)
  Me.Button1.Name = "Button1"
  Me.Button1.Size = New System.Drawing.Size(75, 23)
  Me.Button1.TabIndex = 8
  Me.Button1.Text = "Imprimir"
  Me.Button1.UseVisualStyleBackColor = True
  '
  'pbBarCode
  '
  Me.pbBarCode.Location = New System.Drawing.Point(0, 174)
  Me.pbBarCode.Name = "pbBarCode"
  Me.pbBarCode.Size = New System.Drawing.Size(309, 103)
  Me.pbBarCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
  Me.pbBarCode.TabIndex = 3
  Me.pbBarCode.TabStop = False
  Me.pbBarCode.Tag = ""
  '
  'frmPrintUbicaciones
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(309, 287)
  Me.Controls.Add(Me.pbBarCode)
  Me.Controls.Add(Me.Panel1)
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.Name = "frmPrintUbicaciones"
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Impresión de ubicaciones"
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  CType(Me.pbBarCode, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)

 End Sub

 Friend WithEvents pbBarCode As PictureBox
 Friend WithEvents Panel1 As Panel
 Friend WithEvents txtSCC As TextBox
 Friend WithEvents Label9 As Label
 Friend WithEvents txtSPCIO As TextBox
 Friend WithEvents Label8 As Label
 Friend WithEvents txtNVL As TextBox
 Friend WithEvents Label7 As Label
 Friend WithEvents txtRCK As TextBox
 Friend WithEvents Label6 As Label
 Friend WithEvents txtBLQ As TextBox
 Friend WithEvents txtCopias As TextBox
 Friend WithEvents Label5 As Label
 Friend WithEvents Label3 As Label
 Friend WithEvents Button1 As Button
 Friend WithEvents btnGenerar As Button
End Class
