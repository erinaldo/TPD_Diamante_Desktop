<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Devoluciones
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Devoluciones))
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.CmbArticulo = New System.Windows.Forms.ComboBox()
        Me.CmbGrupoArticulo = New System.Windows.Forms.ComboBox()
        Me.CmbCliente = New System.Windows.Forms.ComboBox()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.CBAlmacen = New System.Windows.Forms.ComboBox()
        Me.TBDocNum = New System.Windows.Forms.TextBox()
        Me.CBDocumento = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.DGDevoluciones = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Estatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiasTot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FecSuc = New TPDiamante.CalendarColumn()
        Me.FecAlm = New TPDiamante.CalendarColumn()
        Me.Factura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FecFac = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiasTransFactRecep = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cardcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CardName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sucursal = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Almacen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItmsGrpNam = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Proveedor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BSave = New System.Windows.Forms.Button()
        Me.CBEstatus = New System.Windows.Forms.ComboBox()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DGDevoluciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(352, 114)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 13)
        Me.Label10.TabIndex = 346
        Me.Label10.Text = "Estatus"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.AliceBlue
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button1.Location = New System.Drawing.Point(1169, 15)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(86, 44)
        Me.Button1.TabIndex = 345
        Me.Button1.Text = "Agregar devolución"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.AliceBlue
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button2.Location = New System.Drawing.Point(528, 59)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 39)
        Me.Button2.TabIndex = 343
        Me.Button2.Text = "Consultar"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'CmbArticulo
        '
        Me.CmbArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbArticulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbArticulo.FormattingEnabled = True
        Me.CmbArticulo.Location = New System.Drawing.Point(307, 70)
        Me.CmbArticulo.Name = "CmbArticulo"
        Me.CmbArticulo.Size = New System.Drawing.Size(191, 21)
        Me.CmbArticulo.TabIndex = 342
        '
        'CmbGrupoArticulo
        '
        Me.CmbGrupoArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbGrupoArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbGrupoArticulo.FormattingEnabled = True
        Me.CmbGrupoArticulo.Location = New System.Drawing.Point(307, 39)
        Me.CmbGrupoArticulo.Name = "CmbGrupoArticulo"
        Me.CmbGrupoArticulo.Size = New System.Drawing.Size(154, 21)
        Me.CmbGrupoArticulo.TabIndex = 341
        '
        'CmbCliente
        '
        Me.CmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbCliente.FormattingEnabled = True
        Me.CmbCliente.Location = New System.Drawing.Point(306, 9)
        Me.CmbCliente.Name = "CmbCliente"
        Me.CmbCliente.Size = New System.Drawing.Size(191, 21)
        Me.CmbCliente.TabIndex = 340
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtpFechaTer.Location = New System.Drawing.Point(227, 108)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(98, 20)
        Me.DtpFechaTer.TabIndex = 339
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtpFechaIni.Location = New System.Drawing.Point(72, 109)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(98, 20)
        Me.DtpFechaIni.TabIndex = 338
        Me.DtpFechaIni.Value = New Date(2015, 1, 1, 0, 0, 0, 0)
        '
        'CBAlmacen
        '
        Me.CBAlmacen.FormattingEnabled = True
        Me.CBAlmacen.Items.AddRange(New Object() {"PUEBLA", "MÉRIDA", "TUXTLA GTZ", "TODOS"})
        Me.CBAlmacen.Location = New System.Drawing.Point(102, 70)
        Me.CBAlmacen.Name = "CBAlmacen"
        Me.CBAlmacen.Size = New System.Drawing.Size(121, 21)
        Me.CBAlmacen.TabIndex = 337
        '
        'TBDocNum
        '
        Me.TBDocNum.Location = New System.Drawing.Point(102, 39)
        Me.TBDocNum.Name = "TBDocNum"
        Me.TBDocNum.Size = New System.Drawing.Size(98, 20)
        Me.TBDocNum.TabIndex = 336
        '
        'CBDocumento
        '
        Me.CBDocumento.FormattingEnabled = True
        Me.CBDocumento.Items.AddRange(New Object() {"Factura", "Nota de crédito"})
        Me.CBDocumento.Location = New System.Drawing.Point(103, 9)
        Me.CBDocumento.Name = "CBDocumento"
        Me.CBDocumento.Size = New System.Drawing.Size(98, 21)
        Me.CBDocumento.TabIndex = 335
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(254, 42)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 13)
        Me.Label9.TabIndex = 334
        Me.Label9.Text = "Línea"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.DGDevoluciones)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 142)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1245, 322)
        Me.GroupBox2.TabIndex = 333
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Devoluciones"
        '
        'DGDevoluciones
        '
        Me.DGDevoluciones.AllowUserToAddRows = False
        Me.DGDevoluciones.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGDevoluciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGDevoluciones.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.Estatus, Me.DiasTot, Me.FecSuc, Me.FecAlm, Me.Factura, Me.FecFac, Me.DiasTransFactRecep, Me.cardcode, Me.CardName, Me.Sucursal, Me.Almacen, Me.Cantidad, Me.ItemCode, Me.ItemName, Me.ItmsGrpNam, Me.Proveedor})
        Me.DGDevoluciones.Location = New System.Drawing.Point(15, 16)
        Me.DGDevoluciones.Name = "DGDevoluciones"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGDevoluciones.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DGDevoluciones.Size = New System.Drawing.Size(1214, 299)
        Me.DGDevoluciones.TabIndex = 226
        '
        'Id
        '
        Me.Id.DataPropertyName = "Id"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Id.DefaultCellStyle = DataGridViewCellStyle1
        Me.Id.HeaderText = "No. Linea"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.Visible = False
        Me.Id.Width = 50
        '
        'Estatus
        '
        Me.Estatus.DataPropertyName = "Estado"
        Me.Estatus.HeaderText = "Estatus"
        Me.Estatus.Name = "Estatus"
        Me.Estatus.ReadOnly = True
        Me.Estatus.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Estatus.Width = 130
        '
        'DiasTot
        '
        Me.DiasTot.DataPropertyName = "DiasTransTot"
        Me.DiasTot.HeaderText = "Dias Trans. Totales"
        Me.DiasTot.Name = "DiasTot"
        Me.DiasTot.Width = 60
        '
        'FecSuc
        '
        Me.FecSuc.DataPropertyName = "FecSuc"
        Me.FecSuc.HeaderText = "Fecha recepción Mercancía"
        Me.FecSuc.Name = "FecSuc"
        Me.FecSuc.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FecSuc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.FecSuc.Width = 85
        '
        'FecAlm
        '
        Me.FecAlm.DataPropertyName = "FecAlm"
        Me.FecAlm.HeaderText = "Fecha rececpción Almacén"
        Me.FecAlm.Name = "FecAlm"
        Me.FecAlm.Width = 85
        '
        'Factura
        '
        Me.Factura.DataPropertyName = "Factura"
        Me.Factura.HeaderText = "Factura"
        Me.Factura.Name = "Factura"
        Me.Factura.ReadOnly = True
        Me.Factura.Width = 65
        '
        'FecFac
        '
        Me.FecFac.DataPropertyName = "FecFac"
        Me.FecFac.HeaderText = "Fecha Factura"
        Me.FecFac.Name = "FecFac"
        Me.FecFac.ReadOnly = True
        Me.FecFac.Width = 85
        '
        'DiasTransFactRecep
        '
        Me.DiasTransFactRecep.DataPropertyName = "DiasTransFecFacFecRecAlm"
        Me.DiasTransFactRecep.HeaderText = "Dias Transcurridos"
        Me.DiasTransFactRecep.Name = "DiasTransFactRecep"
        Me.DiasTransFactRecep.Width = 60
        '
        'cardcode
        '
        Me.cardcode.DataPropertyName = "CardCode"
        Me.cardcode.HeaderText = "Cliente"
        Me.cardcode.Name = "cardcode"
        Me.cardcode.ReadOnly = True
        Me.cardcode.Width = 65
        '
        'CardName
        '
        Me.CardName.DataPropertyName = "CardName"
        Me.CardName.HeaderText = "Nombre"
        Me.CardName.Name = "CardName"
        Me.CardName.ReadOnly = True
        Me.CardName.Width = 135
        '
        'Sucursal
        '
        Me.Sucursal.DataPropertyName = "Sucursal"
        Me.Sucursal.HeaderText = "Sucursal"
        Me.Sucursal.Items.AddRange(New Object() {"PUEBLA", "MÉRIDA", "TUXTLA GTZ"})
        Me.Sucursal.Name = "Sucursal"
        Me.Sucursal.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Sucursal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Sucursal.Width = 90
        '
        'Almacen
        '
        Me.Almacen.DataPropertyName = "Almacen"
        Me.Almacen.HeaderText = "Almacén"
        Me.Almacen.Name = "Almacen"
        Me.Almacen.ReadOnly = True
        Me.Almacen.Width = 60
        '
        'Cantidad
        '
        Me.Cantidad.DataPropertyName = "Cantidad"
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Width = 60
        '
        'ItemCode
        '
        Me.ItemCode.DataPropertyName = "ItemCode"
        Me.ItemCode.HeaderText = "Artículo"
        Me.ItemCode.Name = "ItemCode"
        Me.ItemCode.ReadOnly = True
        '
        'ItemName
        '
        Me.ItemName.DataPropertyName = "ItemName"
        Me.ItemName.HeaderText = "Descripción"
        Me.ItemName.Name = "ItemName"
        Me.ItemName.ReadOnly = True
        Me.ItemName.Width = 170
        '
        'ItmsGrpNam
        '
        Me.ItmsGrpNam.DataPropertyName = "ItmsGrpNam"
        Me.ItmsGrpNam.HeaderText = "Línea"
        Me.ItmsGrpNam.Name = "ItmsGrpNam"
        Me.ItmsGrpNam.ReadOnly = True
        Me.ItmsGrpNam.Width = 105
        '
        'Proveedor
        '
        Me.Proveedor.DataPropertyName = "Proveedor"
        Me.Proveedor.HeaderText = "Proveedor"
        Me.Proveedor.Name = "Proveedor"
        Me.Proveedor.Width = 125
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(254, 73)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 13)
        Me.Label8.TabIndex = 332
        Me.Label8.Text = "Artículo"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(192, 111)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 331
        Me.Label6.Text = "Final"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(32, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 13)
        Me.Label5.TabIndex = 330
        Me.Label5.Text = "Inicial"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(32, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 329
        Me.Label4.Text = "Almacen"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(254, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 328
        Me.Label3.Text = "Cliente"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 327
        Me.Label2.Text = "Fecha Almacen"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(32, 26)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.TabIndex = 326
        Me.Label7.Text = "Buscar por:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1167, 127)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 325
        Me.Label1.Text = "Guardar Cambios"
        '
        'BSave
        '
        Me.BSave.Image = CType(resources.GetObject("BSave.Image"), System.Drawing.Image)
        Me.BSave.Location = New System.Drawing.Point(1189, 89)
        Me.BSave.Name = "BSave"
        Me.BSave.Size = New System.Drawing.Size(43, 35)
        Me.BSave.TabIndex = 344
        Me.BSave.UseVisualStyleBackColor = True
        '
        'CBEstatus
        '
        Me.CBEstatus.FormattingEnabled = True
        Me.CBEstatus.Items.AddRange(New Object() {"NO EMPEZADA", "RECIBIDA", "EN REVISIÓN", "PENDIENTE AUTORIZACIÓN", "APROBADA", "RECHAZADA"})
        Me.CBEstatus.Location = New System.Drawing.Point(411, 111)
        Me.CBEstatus.Name = "CBEstatus"
        Me.CBEstatus.Size = New System.Drawing.Size(186, 21)
        Me.CBEstatus.TabIndex = 372
        '
        'Devoluciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1280, 473)
        Me.Controls.Add(Me.CBEstatus)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BSave)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.CmbArticulo)
        Me.Controls.Add(Me.CmbGrupoArticulo)
        Me.Controls.Add(Me.CmbCliente)
        Me.Controls.Add(Me.DtpFechaTer)
        Me.Controls.Add(Me.DtpFechaIni)
        Me.Controls.Add(Me.CBAlmacen)
        Me.Controls.Add(Me.TBDocNum)
        Me.Controls.Add(Me.CBDocumento)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Devoluciones"
        Me.Text = "Devoluciones"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DGDevoluciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label10 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents BSave As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents CmbArticulo As ComboBox
    Friend WithEvents CmbGrupoArticulo As ComboBox
    Friend WithEvents CmbCliente As ComboBox
    Friend WithEvents DtpFechaTer As DateTimePicker
    Friend WithEvents DtpFechaIni As DateTimePicker
    Friend WithEvents CBAlmacen As ComboBox
    Friend WithEvents TBDocNum As TextBox
    Friend WithEvents CBDocumento As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents DGDevoluciones As DataGridView
    Friend WithEvents Id As DataGridViewTextBoxColumn
    Friend WithEvents Estatus As DataGridViewTextBoxColumn
    Friend WithEvents DiasTot As DataGridViewTextBoxColumn
    Friend WithEvents FecSuc As CalendarColumn
    Friend WithEvents FecAlm As CalendarColumn
    Friend WithEvents Factura As DataGridViewTextBoxColumn
    Friend WithEvents FecFac As DataGridViewTextBoxColumn
    Friend WithEvents DiasTransFactRecep As DataGridViewTextBoxColumn
    Friend WithEvents cardcode As DataGridViewTextBoxColumn
    Friend WithEvents CardName As DataGridViewTextBoxColumn
    Friend WithEvents Sucursal As DataGridViewComboBoxColumn
    Friend WithEvents Almacen As DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As DataGridViewTextBoxColumn
    Friend WithEvents ItemCode As DataGridViewTextBoxColumn
    Friend WithEvents ItemName As DataGridViewTextBoxColumn
    Friend WithEvents ItmsGrpNam As DataGridViewTextBoxColumn
    Friend WithEvents Proveedor As DataGridViewTextBoxColumn
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents CBEstatus As ComboBox
End Class
