<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSolicitaFirma
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
  Me.txtClave = New System.Windows.Forms.TextBox()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.btnAceptar = New System.Windows.Forms.Button()
  Me.btnCancelar = New System.Windows.Forms.Button()
  Me.lblmsgfirma = New System.Windows.Forms.Label()
  Me.SuspendLayout()
  '
  'txtClave
  '
  Me.txtClave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
  Me.txtClave.Location = New System.Drawing.Point(151, 47)
  Me.txtClave.MaxLength = 8
  Me.txtClave.Name = "txtClave"
  Me.txtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
  Me.txtClave.Size = New System.Drawing.Size(143, 20)
  Me.txtClave.TabIndex = 11
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(35, 48)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(113, 16)
  Me.Label1.TabIndex = 14
  Me.Label1.Text = "Clave de acceso:"
  '
  'btnAceptar
  '
  Me.btnAceptar.Location = New System.Drawing.Point(87, 86)
  Me.btnAceptar.Name = "btnAceptar"
  Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
  Me.btnAceptar.TabIndex = 12
  Me.btnAceptar.Text = "Aceptar"
  Me.btnAceptar.UseVisualStyleBackColor = True
  '
  'btnCancelar
  '
  Me.btnCancelar.Location = New System.Drawing.Point(185, 86)
  Me.btnCancelar.Name = "btnCancelar"
  Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
  Me.btnCancelar.TabIndex = 13
  Me.btnCancelar.Text = "Cancelar"
  Me.btnCancelar.UseVisualStyleBackColor = True
  '
  'lblmsgfirma
  '
  Me.lblmsgfirma.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblmsgfirma.Location = New System.Drawing.Point(16, 7)
  Me.lblmsgfirma.Name = "lblmsgfirma"
  Me.lblmsgfirma.Size = New System.Drawing.Size(312, 32)
  Me.lblmsgfirma.TabIndex = 15
  '
  'frmSolicitaFirma
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(344, 139)
  Me.ControlBox = False
  Me.Controls.Add(Me.lblmsgfirma)
  Me.Controls.Add(Me.txtClave)
  Me.Controls.Add(Me.Label1)
  Me.Controls.Add(Me.btnAceptar)
  Me.Controls.Add(Me.btnCancelar)
  Me.Name = "frmSolicitaFirma"
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Solicitud de Firma"
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub

 Friend WithEvents txtClave As TextBox
 Friend WithEvents Label1 As Label
 Friend WithEvents btnAceptar As Button
 Friend WithEvents btnCancelar As Button
 Friend WithEvents lblmsgfirma As Label
End Class
