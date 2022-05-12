<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmadjuntar
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtfactura = New System.Windows.Forms.TextBox()
        Me.btnadjuntar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(61, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Factura:"
        '
        'txtfactura
        '
        Me.txtfactura.Location = New System.Drawing.Point(113, 52)
        Me.txtfactura.Name = "txtfactura"
        Me.txtfactura.Size = New System.Drawing.Size(100, 20)
        Me.txtfactura.TabIndex = 1
        '
        'btnadjuntar
        '
        Me.btnadjuntar.Location = New System.Drawing.Point(219, 50)
        Me.btnadjuntar.Name = "btnadjuntar"
        Me.btnadjuntar.Size = New System.Drawing.Size(75, 23)
        Me.btnadjuntar.TabIndex = 2
        Me.btnadjuntar.Text = "Adjuntar"
        Me.btnadjuntar.UseVisualStyleBackColor = True
        '
        'frmadjuntar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(340, 260)
        Me.Controls.Add(Me.btnadjuntar)
        Me.Controls.Add(Me.txtfactura)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmadjuntar"
        Me.Text = "Adjuntar"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtfactura As TextBox
    Friend WithEvents btnadjuntar As Button
End Class
