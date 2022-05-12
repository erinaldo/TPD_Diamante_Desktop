<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConsultaModDetalle
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
        Me.Label13 = New System.Windows.Forms.Label()
        Me.gbSugerencia = New System.Windows.Forms.GroupBox()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.cmbPack = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblPaquete = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnDescartar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtCommentStatus = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTrnspName = New System.Windows.Forms.TextBox()
        Me.txtDocDueDate = New System.Windows.Forms.TextBox()
        Me.txtDocDate = New System.Windows.Forms.TextBox()
        Me.txtCardName = New System.Windows.Forms.TextBox()
        Me.txtCardCode = New System.Windows.Forms.TextBox()
        Me.lblDocNum = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvDetalle = New System.Windows.Forms.DataGridView()
        Me.LineNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dscription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItmsGrpCod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Peso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PesoxUnidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OnHand = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Surtido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheckList = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.lblSurtidor = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lbltarimas = New System.Windows.Forms.Label()
        Me.txtSugCajas = New System.Windows.Forms.TextBox()
        Me.lblBaseK = New System.Windows.Forms.Label()
        Me.lblcajas = New System.Windows.Forms.Label()
        Me.txtPeso = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.gbSugerencia.SuspendLayout()
        CType(Me.dgvDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label13.Location = New System.Drawing.Point(1046, 275)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(287, 13)
        Me.Label13.TabIndex = 48
        Me.Label13.Text = "________________________________________"
        '
        'gbSugerencia
        '
        Me.gbSugerencia.Controls.Add(Me.txtQuantity)
        Me.gbSugerencia.Controls.Add(Me.cmbPack)
        Me.gbSugerencia.Controls.Add(Me.Label11)
        Me.gbSugerencia.Controls.Add(Me.lblPaquete)
        Me.gbSugerencia.Location = New System.Drawing.Point(1027, 435)
        Me.gbSugerencia.Name = "gbSugerencia"
        Me.gbSugerencia.Size = New System.Drawing.Size(322, 153)
        Me.gbSugerencia.TabIndex = 33
        Me.gbSugerencia.TabStop = False
        Me.gbSugerencia.Text = "Sugerencia de empaquetado"
        '
        'txtQuantity
        '
        Me.txtQuantity.Location = New System.Drawing.Point(140, 66)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(130, 20)
        Me.txtQuantity.TabIndex = 3
        '
        'cmbPack
        '
        Me.cmbPack.FormattingEnabled = True
        Me.cmbPack.Items.AddRange(New Object() {"CAJA", "TARIMA"})
        Me.cmbPack.Location = New System.Drawing.Point(140, 36)
        Me.cmbPack.Name = "cmbPack"
        Me.cmbPack.Size = New System.Drawing.Size(130, 21)
        Me.cmbPack.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(74, 69)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Cantidad:"
        '
        'lblPaquete
        '
        Me.lblPaquete.AutoSize = True
        Me.lblPaquete.Location = New System.Drawing.Point(74, 39)
        Me.lblPaquete.Name = "lblPaquete"
        Me.lblPaquete.Size = New System.Drawing.Size(50, 13)
        Me.lblPaquete.TabIndex = 0
        Me.lblPaquete.Text = "Paquete:"
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(1274, 619)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelar.TabIndex = 38
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnDescartar
        '
        Me.btnDescartar.Enabled = False
        Me.btnDescartar.Location = New System.Drawing.Point(1182, 619)
        Me.btnDescartar.Name = "btnDescartar"
        Me.btnDescartar.Size = New System.Drawing.Size(75, 23)
        Me.btnDescartar.TabIndex = 36
        Me.btnDescartar.Text = "Descartar"
        Me.btnDescartar.UseVisualStyleBackColor = True
        Me.btnDescartar.Visible = False
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(1091, 619)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 23)
        Me.btnGuardar.TabIndex = 35
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'txtCommentStatus
        '
        Me.txtCommentStatus.Location = New System.Drawing.Point(1138, 337)
        Me.txtCommentStatus.Multiline = True
        Me.txtCommentStatus.Name = "txtCommentStatus"
        Me.txtCommentStatus.Size = New System.Drawing.Size(211, 76)
        Me.txtCommentStatus.TabIndex = 30
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(1024, 340)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(108, 13)
        Me.Label9.TabIndex = 47
        Me.Label9.Text = "Comentario de Status"
        '
        'cmbStatus
        '
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(1138, 309)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(211, 21)
        Me.cmbStatus.TabIndex = 28
        Me.cmbStatus.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1024, 309)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 13)
        Me.Label8.TabIndex = 46
        Me.Label8.Text = "Status:"
        Me.Label8.Visible = False
        '
        'txtComment
        '
        Me.txtComment.Enabled = False
        Me.txtComment.Location = New System.Drawing.Point(1128, 199)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ReadOnly = True
        Me.txtComment.Size = New System.Drawing.Size(221, 63)
        Me.txtComment.TabIndex = 44
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(1024, 202)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 13)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "Comentarios:"
        '
        'txtTrnspName
        '
        Me.txtTrnspName.Enabled = False
        Me.txtTrnspName.Location = New System.Drawing.Point(1128, 173)
        Me.txtTrnspName.Name = "txtTrnspName"
        Me.txtTrnspName.ReadOnly = True
        Me.txtTrnspName.Size = New System.Drawing.Size(221, 20)
        Me.txtTrnspName.TabIndex = 43
        '
        'txtDocDueDate
        '
        Me.txtDocDueDate.Enabled = False
        Me.txtDocDueDate.Location = New System.Drawing.Point(1128, 146)
        Me.txtDocDueDate.Name = "txtDocDueDate"
        Me.txtDocDueDate.ReadOnly = True
        Me.txtDocDueDate.Size = New System.Drawing.Size(221, 20)
        Me.txtDocDueDate.TabIndex = 42
        '
        'txtDocDate
        '
        Me.txtDocDate.Enabled = False
        Me.txtDocDate.Location = New System.Drawing.Point(1128, 120)
        Me.txtDocDate.Name = "txtDocDate"
        Me.txtDocDate.ReadOnly = True
        Me.txtDocDate.Size = New System.Drawing.Size(221, 20)
        Me.txtDocDate.TabIndex = 41
        '
        'txtCardName
        '
        Me.txtCardName.Enabled = False
        Me.txtCardName.Location = New System.Drawing.Point(1128, 95)
        Me.txtCardName.Name = "txtCardName"
        Me.txtCardName.ReadOnly = True
        Me.txtCardName.Size = New System.Drawing.Size(221, 20)
        Me.txtCardName.TabIndex = 40
        '
        'txtCardCode
        '
        Me.txtCardCode.Enabled = False
        Me.txtCardCode.Location = New System.Drawing.Point(1128, 70)
        Me.txtCardCode.Name = "txtCardCode"
        Me.txtCardCode.ReadOnly = True
        Me.txtCardCode.Size = New System.Drawing.Size(221, 20)
        Me.txtCardCode.TabIndex = 39
        '
        'lblDocNum
        '
        Me.lblDocNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocNum.ForeColor = System.Drawing.Color.Blue
        Me.lblDocNum.Location = New System.Drawing.Point(1182, 14)
        Me.lblDocNum.Name = "lblDocNum"
        Me.lblDocNum.Size = New System.Drawing.Size(134, 23)
        Me.lblDocNum.TabIndex = 37
        Me.lblDocNum.Text = "Label7"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1060, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 13)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Orden de Venta:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(1024, 177)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Clase de entrega:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1024, 149)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Fecha de entrega:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1024, 123)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Fecha:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1024, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Nombre:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1024, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Cliente:"
        '
        'dgvDetalle
        '
        Me.dgvDetalle.AllowUserToAddRows = False
        Me.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetalle.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LineNum, Me.ItemCode, Me.Dscription, Me.ItmsGrpCod, Me.Peso, Me.PesoxUnidad, Me.OnHand, Me.Quantity, Me.Surtido, Me.CheckList})
        Me.dgvDetalle.GridColor = System.Drawing.SystemColors.Control
        Me.dgvDetalle.Location = New System.Drawing.Point(14, 16)
        Me.dgvDetalle.Name = "dgvDetalle"
        Me.dgvDetalle.RowHeadersWidth = 20
        Me.dgvDetalle.Size = New System.Drawing.Size(1004, 572)
        Me.dgvDetalle.StandardTab = True
        Me.dgvDetalle.TabIndex = 49
        '
        'LineNum
        '
        Me.LineNum.HeaderText = "#Partida"
        Me.LineNum.Name = "LineNum"
        Me.LineNum.ReadOnly = True
        Me.LineNum.Width = 60
        '
        'ItemCode
        '
        Me.ItemCode.HeaderText = "Codigo"
        Me.ItemCode.Name = "ItemCode"
        Me.ItemCode.ReadOnly = True
        Me.ItemCode.Width = 140
        '
        'Dscription
        '
        Me.Dscription.HeaderText = "Descripción"
        Me.Dscription.Name = "Dscription"
        Me.Dscription.ReadOnly = True
        Me.Dscription.Width = 300
        '
        'ItmsGrpCod
        '
        Me.ItmsGrpCod.HeaderText = "Grupo"
        Me.ItmsGrpCod.Name = "ItmsGrpCod"
        Me.ItmsGrpCod.ReadOnly = True
        Me.ItmsGrpCod.Width = 110
        '
        'Peso
        '
        Me.Peso.HeaderText = "Peso (Kg)"
        Me.Peso.Name = "Peso"
        Me.Peso.ReadOnly = True
        Me.Peso.Width = 75
        '
        'PesoxUnidad
        '
        Me.PesoxUnidad.HeaderText = "Peso x Pza. (Kg)"
        Me.PesoxUnidad.Name = "PesoxUnidad"
        Me.PesoxUnidad.ReadOnly = True
        Me.PesoxUnidad.Width = 75
        '
        'OnHand
        '
        Me.OnHand.HeaderText = "Stock"
        Me.OnHand.Name = "OnHand"
        Me.OnHand.ReadOnly = True
        Me.OnHand.Width = 50
        '
        'Quantity
        '
        Me.Quantity.HeaderText = "Cantidad"
        Me.Quantity.Name = "Quantity"
        Me.Quantity.ReadOnly = True
        Me.Quantity.Width = 60
  '
  'Surtido
  '
  Me.Surtido.HeaderText = "Surtido / Real"
  Me.Surtido.Name = "Surtido"
        '
        'CheckList
        '
        Me.CheckList.HeaderText = "Check List"
        Me.CheckList.Name = "CheckList"
        Me.CheckList.Visible = False
        Me.CheckList.Width = 50
        '
        'lblSurtidor
        '
        Me.lblSurtidor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSurtidor.Location = New System.Drawing.Point(78, 606)
        Me.lblSurtidor.Name = "lblSurtidor"
        Me.lblSurtidor.Size = New System.Drawing.Size(210, 23)
        Me.lblSurtidor.TabIndex = 51
        Me.lblSurtidor.Text = "Label13"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(18, 606)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 16)
        Me.Label12.TabIndex = 50
        Me.Label12.Text = "Surtidor:"
        '
        'lbltarimas
        '
        Me.lbltarimas.AutoSize = True
        Me.lbltarimas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltarimas.ForeColor = System.Drawing.Color.Blue
        Me.lbltarimas.Location = New System.Drawing.Point(750, 637)
        Me.lbltarimas.Name = "lbltarimas"
        Me.lbltarimas.Size = New System.Drawing.Size(164, 16)
        Me.lbltarimas.TabIndex = 57
        Me.lbltarimas.Text = "Sugerencia de tarimas"
        Me.lbltarimas.Visible = False
        '
        'txtSugCajas
        '
        Me.txtSugCajas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSugCajas.Location = New System.Drawing.Point(921, 638)
        Me.txtSugCajas.Name = "txtSugCajas"
        Me.txtSugCajas.ReadOnly = True
        Me.txtSugCajas.Size = New System.Drawing.Size(97, 22)
        Me.txtSugCajas.TabIndex = 56
        Me.txtSugCajas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBaseK
        '
        Me.lblBaseK.AutoSize = True
        Me.lblBaseK.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseK.Location = New System.Drawing.Point(749, 652)
        Me.lblBaseK.Name = "lblBaseK"
        Me.lblBaseK.Size = New System.Drawing.Size(111, 16)
        Me.lblBaseK.TabIndex = 55
        Me.lblBaseK.Text = "con base a Kg."
        '
        'lblcajas
        '
        Me.lblcajas.AutoSize = True
        Me.lblcajas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcajas.Location = New System.Drawing.Point(750, 635)
        Me.lblcajas.Name = "lblcajas"
        Me.lblcajas.Size = New System.Drawing.Size(151, 16)
        Me.lblcajas.TabIndex = 54
        Me.lblcajas.Text = "Sugerencia de cajas"
        '
        'txtPeso
        '
        Me.txtPeso.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPeso.Location = New System.Drawing.Point(921, 610)
        Me.txtPeso.Name = "txtPeso"
        Me.txtPeso.ReadOnly = True
        Me.txtPeso.Size = New System.Drawing.Size(97, 22)
        Me.txtPeso.TabIndex = 53
        Me.txtPeso.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(750, 613)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(117, 16)
        Me.Label14.TabIndex = 52
        Me.Label14.Text = "Peso Neto (Kg):"
        '
        'frmConsultaModDetalle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1378, 674)
        Me.Controls.Add(Me.lbltarimas)
        Me.Controls.Add(Me.txtSugCajas)
        Me.Controls.Add(Me.lblBaseK)
        Me.Controls.Add(Me.lblcajas)
        Me.Controls.Add(Me.txtPeso)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.lblSurtidor)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.dgvDetalle)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.gbSugerencia)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnDescartar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtCommentStatus)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cmbStatus)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtTrnspName)
        Me.Controls.Add(Me.txtDocDueDate)
        Me.Controls.Add(Me.txtDocDate)
        Me.Controls.Add(Me.txtCardName)
        Me.Controls.Add(Me.txtCardCode)
        Me.Controls.Add(Me.lblDocNum)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmConsultaModDetalle"
        Me.Text = "frmConsultaModDetalle"
        Me.gbSugerencia.ResumeLayout(False)
        Me.gbSugerencia.PerformLayout()
        CType(Me.dgvDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label13 As Label
    Friend WithEvents gbSugerencia As GroupBox
    Friend WithEvents txtQuantity As TextBox
    Friend WithEvents cmbPack As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents lblPaquete As Label
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnDescartar As Button
    Friend WithEvents btnGuardar As Button
    Friend WithEvents txtCommentStatus As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents cmbStatus As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtComment As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtTrnspName As TextBox
    Friend WithEvents txtDocDueDate As TextBox
    Friend WithEvents txtDocDate As TextBox
    Friend WithEvents txtCardName As TextBox
    Friend WithEvents txtCardCode As TextBox
    Friend WithEvents lblDocNum As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvDetalle As DataGridView
    Friend WithEvents LineNum As DataGridViewTextBoxColumn
    Friend WithEvents ItemCode As DataGridViewTextBoxColumn
    Friend WithEvents Dscription As DataGridViewTextBoxColumn
    Friend WithEvents ItmsGrpCod As DataGridViewTextBoxColumn
    Friend WithEvents Peso As DataGridViewTextBoxColumn
    Friend WithEvents PesoxUnidad As DataGridViewTextBoxColumn
    Friend WithEvents OnHand As DataGridViewTextBoxColumn
    Friend WithEvents Quantity As DataGridViewTextBoxColumn
    Friend WithEvents Surtido As DataGridViewTextBoxColumn
    Friend WithEvents CheckList As DataGridViewCheckBoxColumn
    Friend WithEvents lblSurtidor As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents lbltarimas As Label
    Friend WithEvents txtSugCajas As TextBox
    Friend WithEvents lblBaseK As Label
    Friend WithEvents lblcajas As Label
    Friend WithEvents txtPeso As TextBox
    Friend WithEvents Label14 As Label
End Class
