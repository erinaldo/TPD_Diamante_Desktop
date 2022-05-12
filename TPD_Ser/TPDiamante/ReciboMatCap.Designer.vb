<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReciboMatCap
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
        Me.TxtFchDoc = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtFchEnt = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtOrdCompra = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtIdProv = New System.Windows.Forms.TextBox()
        Me.TxtNomProv = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnGuardar = New System.Windows.Forms.Button()
        Me.TxtComentario = New System.Windows.Forms.TextBox()
        Me.TxtIdRecibo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TxtFchDoc
        '
        Me.TxtFchDoc.Enabled = False
        Me.TxtFchDoc.Location = New System.Drawing.Point(299, 34)
        Me.TxtFchDoc.Name = "TxtFchDoc"
        Me.TxtFchDoc.ReadOnly = True
        Me.TxtFchDoc.Size = New System.Drawing.Size(80, 20)
        Me.TxtFchDoc.TabIndex = 29
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(188, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(112, 13)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Fecha del Documento"
        '
        'TxtFchEnt
        '
        Me.TxtFchEnt.Enabled = False
        Me.TxtFchEnt.Location = New System.Drawing.Point(299, 60)
        Me.TxtFchEnt.Name = "TxtFchEnt"
        Me.TxtFchEnt.ReadOnly = True
        Me.TxtFchEnt.Size = New System.Drawing.Size(80, 20)
        Me.TxtFchEnt.TabIndex = 27
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(187, 63)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Fecha de Entrega"
        '
        'TxtOrdCompra
        '
        Me.TxtOrdCompra.Enabled = False
        Me.TxtOrdCompra.Location = New System.Drawing.Point(71, 35)
        Me.TxtOrdCompra.Name = "TxtOrdCompra"
        Me.TxtOrdCompra.ReadOnly = True
        Me.TxtOrdCompra.Size = New System.Drawing.Size(79, 20)
        Me.TxtOrdCompra.TabIndex = 25
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(2, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Ord. Compra"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(2, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Comentario"
        '
        'TxtIdProv
        '
        Me.TxtIdProv.Enabled = False
        Me.TxtIdProv.Location = New System.Drawing.Point(71, 62)
        Me.TxtIdProv.Name = "TxtIdProv"
        Me.TxtIdProv.ReadOnly = True
        Me.TxtIdProv.Size = New System.Drawing.Size(79, 20)
        Me.TxtIdProv.TabIndex = 18
        '
        'TxtNomProv
        '
        Me.TxtNomProv.Enabled = False
        Me.TxtNomProv.Location = New System.Drawing.Point(71, 89)
        Me.TxtNomProv.Name = "TxtNomProv"
        Me.TxtNomProv.ReadOnly = True
        Me.TxtNomProv.Size = New System.Drawing.Size(308, 20)
        Me.TxtNomProv.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(2, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Nombre"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Id Proveedor"
        '
        'BtnGuardar
        '
        Me.BtnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGuardar.ForeColor = System.Drawing.Color.Navy
        Me.BtnGuardar.Location = New System.Drawing.Point(190, 217)
        Me.BtnGuardar.Name = "BtnGuardar"
        Me.BtnGuardar.Size = New System.Drawing.Size(65, 40)
        Me.BtnGuardar.TabIndex = 21
        Me.BtnGuardar.Text = "Agregar Recibo"
        Me.BtnGuardar.UseVisualStyleBackColor = True
        '
        'TxtComentario
        '
        Me.TxtComentario.Location = New System.Drawing.Point(71, 118)
        Me.TxtComentario.MaxLength = 250
        Me.TxtComentario.Multiline = True
        Me.TxtComentario.Name = "TxtComentario"
        Me.TxtComentario.Size = New System.Drawing.Size(308, 85)
        Me.TxtComentario.TabIndex = 20
        '
        'TxtIdRecibo
        '
        Me.TxtIdRecibo.Enabled = False
        Me.TxtIdRecibo.Location = New System.Drawing.Point(71, 9)
        Me.TxtIdRecibo.Name = "TxtIdRecibo"
        Me.TxtIdRecibo.ReadOnly = True
        Me.TxtIdRecibo.Size = New System.Drawing.Size(35, 20)
        Me.TxtIdRecibo.TabIndex = 32
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "Recibo"
        '
        'ReciboMatCap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(388, 264)
        Me.Controls.Add(Me.TxtIdRecibo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtFchDoc)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtFchEnt)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtOrdCompra)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtIdProv)
        Me.Controls.Add(Me.TxtNomProv)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtnGuardar)
        Me.Controls.Add(Me.TxtComentario)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ReciboMatCap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Captura de comentarios"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtFchDoc As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtFchEnt As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtOrdCompra As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtIdProv As System.Windows.Forms.TextBox
    Friend WithEvents TxtNomProv As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BtnGuardar As System.Windows.Forms.Button
    Friend WithEvents TxtComentario As System.Windows.Forms.TextBox
    Friend WithEvents TxtIdRecibo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
