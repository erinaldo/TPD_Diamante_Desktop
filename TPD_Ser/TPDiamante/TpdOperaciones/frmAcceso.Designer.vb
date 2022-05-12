<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAcceso
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAcceso))
  Me.btnCancelar = New System.Windows.Forms.Button()
  Me.btnAceptar = New System.Windows.Forms.Button()
  Me.txtPreClave = New System.Windows.Forms.TextBox()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.txtClave = New System.Windows.Forms.TextBox()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.SuspendLayout()
  '
  'btnCancelar
  '
  Me.btnCancelar.Location = New System.Drawing.Point(233, 87)
  Me.btnCancelar.Name = "btnCancelar"
  Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
  Me.btnCancelar.TabIndex = 4
  Me.btnCancelar.Text = "Cancelar"
  Me.btnCancelar.UseVisualStyleBackColor = True
  '
  'btnAceptar
  '
  Me.btnAceptar.Location = New System.Drawing.Point(135, 87)
  Me.btnAceptar.Name = "btnAceptar"
  Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
  Me.btnAceptar.TabIndex = 3
  Me.btnAceptar.Text = "Aceptar"
  Me.btnAceptar.UseVisualStyleBackColor = True
  '
  'txtPreClave
  '
  Me.txtPreClave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
  Me.txtPreClave.Location = New System.Drawing.Point(164, 24)
  Me.txtPreClave.MaxLength = 8
  Me.txtPreClave.Name = "txtPreClave"
  Me.txtPreClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
  Me.txtPreClave.Size = New System.Drawing.Size(144, 20)
  Me.txtPreClave.TabIndex = 1
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(27, 25)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(134, 16)
  Me.Label2.TabIndex = 12
  Me.Label2.Text = "Clave de preAcceso:"
  '
  'txtClave
  '
  Me.txtClave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
  Me.txtClave.Location = New System.Drawing.Point(164, 54)
  Me.txtClave.MaxLength = 8
  Me.txtClave.Name = "txtClave"
  Me.txtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
  Me.txtClave.Size = New System.Drawing.Size(143, 20)
  Me.txtClave.TabIndex = 2
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(48, 55)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(113, 16)
  Me.Label1.TabIndex = 10
  Me.Label1.Text = "Clave de acceso:"
  '
  'frmAcceso
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
  Me.ClientSize = New System.Drawing.Size(364, 139)
  Me.Controls.Add(Me.txtPreClave)
  Me.Controls.Add(Me.Label2)
  Me.Controls.Add(Me.txtClave)
  Me.Controls.Add(Me.Label1)
  Me.Controls.Add(Me.btnAceptar)
  Me.Controls.Add(Me.btnCancelar)
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.MaximizeBox = False
  Me.Name = "frmAcceso"
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "frmAcceso"
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub
 Friend WithEvents btnCancelar As Button
 Friend WithEvents btnAceptar As Button
 Friend WithEvents txtPreClave As TextBox
 Friend WithEvents Label2 As Label
 Friend WithEvents txtClave As TextBox
 Friend WithEvents Label1 As Label
End Class
