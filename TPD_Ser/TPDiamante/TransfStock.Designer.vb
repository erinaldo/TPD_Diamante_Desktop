<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TransfStock
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
        Me.TBAlmOri = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TBComentarios = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TBAgente = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TBDestinatario = New System.Windows.Forms.TextBox()
        Me.TBPerCon = New System.Windows.Forms.TextBox()
        Me.TBNomCli = New System.Windows.Forms.TextBox()
        Me.TBCliente = New System.Windows.Forms.TextBox()
        Me.TBFecDoc = New System.Windows.Forms.TextBox()
        Me.TBDocDate = New System.Windows.Forms.TextBox()
        Me.TBSerie = New System.Windows.Forms.TextBox()
        Me.TBDocNum = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DGDetalle = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.TBListaPre = New System.Windows.Forms.TextBox()
        Me.TBComentarios2 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TBDocEntry = New System.Windows.Forms.TextBox()
        CType(Me.DGDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TBAlmOri
        '
        Me.TBAlmOri.Location = New System.Drawing.Point(851, 115)
        Me.TBAlmOri.Name = "TBAlmOri"
        Me.TBAlmOri.Size = New System.Drawing.Size(112, 20)
        Me.TBAlmOri.TabIndex = 216
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.AliceBlue
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button2.Location = New System.Drawing.Point(979, 21)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 26)
        Me.Button2.TabIndex = 214
        Me.Button2.Text = "Consultar"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(25, 465)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(164, 15)
        Me.Label12.TabIndex = 204
        Me.Label12.Text = "Empleado del departamento"
        '
        'TBComentarios
        '
        Me.TBComentarios.Location = New System.Drawing.Point(152, 509)
        Me.TBComentarios.MaxLength = 275
        Me.TBComentarios.Multiline = True
        Me.TBComentarios.Name = "TBComentarios"
        Me.TBComentarios.Size = New System.Drawing.Size(184, 51)
        Me.TBComentarios.TabIndex = 202
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(29, 510)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 15)
        Me.Label5.TabIndex = 203
        Me.Label5.Text = "Comentarios"
        '
        'TBAgente
        '
        Me.TBAgente.Location = New System.Drawing.Point(195, 465)
        Me.TBAgente.Name = "TBAgente"
        Me.TBAgente.Size = New System.Drawing.Size(141, 20)
        Me.TBAgente.TabIndex = 200
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(722, 87)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 13)
        Me.Label6.TabIndex = 193
        Me.Label6.Text = "Fecha de documento"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(722, 57)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(125, 13)
        Me.Label8.TabIndex = 191
        Me.Label8.Text = "Fecha de contabilizacion"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(722, 34)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 13)
        Me.Label9.TabIndex = 190
        Me.Label9.Text = "Serie"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(722, 12)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 13)
        Me.Label10.TabIndex = 189
        Me.Label10.Text = "Número"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 188
        Me.Label4.Text = "Destinatario"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 13)
        Me.Label3.TabIndex = 187
        Me.Label3.Text = "Persona de contacto"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 186
        Me.Label2.Text = "Nombre"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 185
        Me.Label1.Text = "Socio de negocios"
        '
        'TBDestinatario
        '
        Me.TBDestinatario.Location = New System.Drawing.Point(191, 112)
        Me.TBDestinatario.Name = "TBDestinatario"
        Me.TBDestinatario.Size = New System.Drawing.Size(112, 20)
        Me.TBDestinatario.TabIndex = 183
        '
        'TBPerCon
        '
        Me.TBPerCon.Location = New System.Drawing.Point(191, 80)
        Me.TBPerCon.Name = "TBPerCon"
        Me.TBPerCon.Size = New System.Drawing.Size(112, 20)
        Me.TBPerCon.TabIndex = 182
        '
        'TBNomCli
        '
        Me.TBNomCli.Location = New System.Drawing.Point(191, 50)
        Me.TBNomCli.Name = "TBNomCli"
        Me.TBNomCli.Size = New System.Drawing.Size(112, 20)
        Me.TBNomCli.TabIndex = 181
        '
        'TBCliente
        '
        Me.TBCliente.Location = New System.Drawing.Point(191, 21)
        Me.TBCliente.Name = "TBCliente"
        Me.TBCliente.Size = New System.Drawing.Size(112, 20)
        Me.TBCliente.TabIndex = 180
        '
        'TBFecDoc
        '
        Me.TBFecDoc.Location = New System.Drawing.Point(851, 84)
        Me.TBFecDoc.Name = "TBFecDoc"
        Me.TBFecDoc.Size = New System.Drawing.Size(112, 20)
        Me.TBFecDoc.TabIndex = 179
        '
        'TBDocDate
        '
        Me.TBDocDate.Location = New System.Drawing.Point(851, 54)
        Me.TBDocDate.Name = "TBDocDate"
        Me.TBDocDate.Size = New System.Drawing.Size(112, 20)
        Me.TBDocDate.TabIndex = 177
        '
        'TBSerie
        '
        Me.TBSerie.Location = New System.Drawing.Point(851, 32)
        Me.TBSerie.Name = "TBSerie"
        Me.TBSerie.Size = New System.Drawing.Size(112, 20)
        Me.TBSerie.TabIndex = 176
        '
        'TBDocNum
        '
        Me.TBDocNum.Location = New System.Drawing.Point(851, 9)
        Me.TBDocNum.Name = "TBDocNum"
        Me.TBDocNum.Size = New System.Drawing.Size(112, 20)
        Me.TBDocNum.TabIndex = 174
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(722, 118)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 221
        Me.Label7.Text = "Almacén origen"
        '
        'DGDetalle
        '
        Me.DGDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGDetalle.Location = New System.Drawing.Point(3, 3)
        Me.DGDetalle.Name = "DGDetalle"
        Me.DGDetalle.Size = New System.Drawing.Size(1165, 290)
        Me.DGDetalle.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.DGDetalle)
        Me.Panel1.Location = New System.Drawing.Point(28, 163)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1175, 295)
        Me.Panel1.TabIndex = 215
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(722, 140)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(81, 13)
        Me.Label22.TabIndex = 223
        Me.Label22.Text = "Lista de precios"
        '
        'TBListaPre
        '
        Me.TBListaPre.Location = New System.Drawing.Point(851, 137)
        Me.TBListaPre.Name = "TBListaPre"
        Me.TBListaPre.Size = New System.Drawing.Size(112, 20)
        Me.TBListaPre.TabIndex = 222
        '
        'TBComentarios2
        '
        Me.TBComentarios2.Location = New System.Drawing.Point(1014, 482)
        Me.TBComentarios2.MaxLength = 275
        Me.TBComentarios2.Multiline = True
        Me.TBComentarios2.Name = "TBComentarios2"
        Me.TBComentarios2.Size = New System.Drawing.Size(184, 78)
        Me.TBComentarios2.TabIndex = 224
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(891, 483)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 15)
        Me.Label11.TabIndex = 225
        Me.Label11.Text = "Comentarios"
        '
        'TBDocEntry
        '
        Me.TBDocEntry.Location = New System.Drawing.Point(1086, 133)
        Me.TBDocEntry.Name = "TBDocEntry"
        Me.TBDocEntry.Size = New System.Drawing.Size(112, 20)
        Me.TBDocEntry.TabIndex = 226
        Me.TBDocEntry.Visible = False
        '
        'TransfStock
        '
        Me.AcceptButton = Me.Button2
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1229, 679)
        Me.Controls.Add(Me.TBDocEntry)
        Me.Controls.Add(Me.TBComentarios2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.TBListaPre)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TBAlmOri)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TBComentarios)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TBAgente)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TBDestinatario)
        Me.Controls.Add(Me.TBPerCon)
        Me.Controls.Add(Me.TBNomCli)
        Me.Controls.Add(Me.TBCliente)
        Me.Controls.Add(Me.TBFecDoc)
        Me.Controls.Add(Me.TBDocDate)
        Me.Controls.Add(Me.TBSerie)
        Me.Controls.Add(Me.TBDocNum)
        Me.Name = "TransfStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transferencia de stock"
        CType(Me.DGDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TBAlmOri As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TBComentarios As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TBAgente As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TBDestinatario As System.Windows.Forms.TextBox
    Friend WithEvents TBPerCon As System.Windows.Forms.TextBox
    Friend WithEvents TBNomCli As System.Windows.Forms.TextBox
    Friend WithEvents TBCliente As System.Windows.Forms.TextBox
    Friend WithEvents TBFecDoc As System.Windows.Forms.TextBox
    Friend WithEvents TBDocDate As System.Windows.Forms.TextBox
    Friend WithEvents TBSerie As System.Windows.Forms.TextBox
    Friend WithEvents TBDocNum As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DGDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TBListaPre As System.Windows.Forms.TextBox
    Friend WithEvents TBComentarios2 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TBDocEntry As System.Windows.Forms.TextBox
End Class
