<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PagoCapCom
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
        Me.TxtComentario = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtNomProv = New System.Windows.Forms.TextBox()
        Me.TxtIdProv = New System.Windows.Forms.TextBox()
        Me.TxtSaldo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtFactura1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtFchVenc = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtFchDoc = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtDiasAtraso = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.LblRow = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TxtComentario
        '
        Me.TxtComentario.Location = New System.Drawing.Point(73, 117)
        Me.TxtComentario.MaxLength = 250
        Me.TxtComentario.Multiline = True
        Me.TxtComentario.Name = "TxtComentario"
        Me.TxtComentario.Size = New System.Drawing.Size(270, 85)
        Me.TxtComentario.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(178, 209)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(65, 32)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Guardar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Saldo MXP"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Id Proveedor"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Nombre"
        '
        'TxtNomProv
        '
        Me.TxtNomProv.Enabled = False
        Me.TxtNomProv.Location = New System.Drawing.Point(73, 88)
        Me.TxtNomProv.Name = "TxtNomProv"
        Me.TxtNomProv.ReadOnly = True
        Me.TxtNomProv.Size = New System.Drawing.Size(271, 20)
        Me.TxtNomProv.TabIndex = 2
        '
        'TxtIdProv
        '
        Me.TxtIdProv.Enabled = False
        Me.TxtIdProv.Location = New System.Drawing.Point(73, 61)
        Me.TxtIdProv.Name = "TxtIdProv"
        Me.TxtIdProv.ReadOnly = True
        Me.TxtIdProv.Size = New System.Drawing.Size(80, 20)
        Me.TxtIdProv.TabIndex = 0
        '
        'TxtSaldo
        '
        Me.TxtSaldo.Enabled = False
        Me.TxtSaldo.Location = New System.Drawing.Point(73, 35)
        Me.TxtSaldo.Name = "TxtSaldo"
        Me.TxtSaldo.ReadOnly = True
        Me.TxtSaldo.Size = New System.Drawing.Size(80, 20)
        Me.TxtSaldo.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Comentario"
        '
        'TxtFactura1
        '
        Me.TxtFactura1.Enabled = False
        Me.TxtFactura1.Location = New System.Drawing.Point(73, 9)
        Me.TxtFactura1.Name = "TxtFactura1"
        Me.TxtFactura1.ReadOnly = True
        Me.TxtFactura1.Size = New System.Drawing.Size(80, 20)
        Me.TxtFactura1.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Factura"
        '
        'TxtFchVenc
        '
        Me.TxtFchVenc.Enabled = False
        Me.TxtFchVenc.Location = New System.Drawing.Point(271, 33)
        Me.TxtFchVenc.Name = "TxtFchVenc"
        Me.TxtFchVenc.ReadOnly = True
        Me.TxtFchVenc.Size = New System.Drawing.Size(80, 20)
        Me.TxtFchVenc.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(159, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(113, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Fecha de Vencimiento"
        '
        'TxtFchDoc
        '
        Me.TxtFchDoc.Enabled = False
        Me.TxtFchDoc.Location = New System.Drawing.Point(271, 5)
        Me.TxtFchDoc.Name = "TxtFchDoc"
        Me.TxtFchDoc.ReadOnly = True
        Me.TxtFchDoc.Size = New System.Drawing.Size(80, 20)
        Me.TxtFchDoc.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(160, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(112, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Fecha del Documento"
        '
        'TxtDiasAtraso
        '
        Me.TxtDiasAtraso.Enabled = False
        Me.TxtDiasAtraso.Location = New System.Drawing.Point(271, 59)
        Me.TxtDiasAtraso.Name = "TxtDiasAtraso"
        Me.TxtDiasAtraso.ReadOnly = True
        Me.TxtDiasAtraso.Size = New System.Drawing.Size(80, 20)
        Me.TxtDiasAtraso.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(161, 62)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Dias atraso"
        '
        'LblRow
        '
        Me.LblRow.AutoSize = True
        Me.LblRow.Location = New System.Drawing.Point(268, 209)
        Me.LblRow.Name = "LblRow"
        Me.LblRow.Size = New System.Drawing.Size(60, 13)
        Me.LblRow.TabIndex = 17
        Me.LblRow.Text = "Dias atraso"
        Me.LblRow.Visible = False
        '
        'PagoCapCom
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(361, 262)
        Me.Controls.Add(Me.LblRow)
        Me.Controls.Add(Me.TxtDiasAtraso)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtFchDoc)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtFchVenc)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtFactura1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtSaldo)
        Me.Controls.Add(Me.TxtIdProv)
        Me.Controls.Add(Me.TxtNomProv)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TxtComentario)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PagoCapCom"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Captura de comentarios"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtComentario As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtNomProv As System.Windows.Forms.TextBox
    Friend WithEvents TxtIdProv As System.Windows.Forms.TextBox
    Friend WithEvents TxtSaldo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtFactura1 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtFchVenc As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtFchDoc As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtDiasAtraso As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents LblRow As System.Windows.Forms.Label
End Class
