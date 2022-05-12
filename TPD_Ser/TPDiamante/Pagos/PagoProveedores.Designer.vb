<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PagoProveedores
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DgFactProv = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.BtnDetalle = New System.Windows.Forms.Button()
        Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ChkVisDisa = New System.Windows.Forms.CheckBox()
        Me.TxtTotEnPesos = New System.Windows.Forms.TextBox()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.ChkVerPagadas = New System.Windows.Forms.CheckBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.RdBCompras = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CmbGasto = New System.Windows.Forms.ComboBox()
        Me.CmbProveedor = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ChkUSD = New System.Windows.Forms.CheckBox()
        Me.ckbFacturasCerradas = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Frozen = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Liberado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RegSel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.FchDoc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiasCred = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Factura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FactProv = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_IdLinea = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdProved = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Proveedor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NombreGasto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotFactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Moneda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pagado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SaldoPendiente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Moneda2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiasAtraso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FchVen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SaldoPesos = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Referencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Obrserv = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Comentarios = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descuento = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BancoOrigen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BancoDestino = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CuentaDestino = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgFactProv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(762, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Detalle"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(7, 135)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(168, 17)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Facturas de Proveedores"
        '
        'DgFactProv
        '
        Me.DgFactProv.AllowUserToAddRows = False
        Me.DgFactProv.AllowUserToDeleteRows = False
        Me.DgFactProv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgFactProv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgFactProv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Frozen, Me.Liberado, Me.RegSel, Me.FchDoc, Me.DiasCred, Me.Factura, Me.FactProv, Me.U_IdLinea, Me.IdProved, Me.Proveedor, Me.NombreGasto, Me.TotFactura, Me.Moneda, Me.Pagado, Me.SaldoPendiente, Me.Moneda2, Me.DiasAtraso, Me.FchVen, Me.SaldoPesos, Me.Referencia, Me.Obrserv, Me.Comentarios, Me.Descuento, Me.BancoOrigen, Me.BancoDestino, Me.CuentaDestino})
        Me.DgFactProv.Location = New System.Drawing.Point(4, 156)
        Me.DgFactProv.Name = "DgFactProv"
        Me.DgFactProv.ReadOnly = True
        Me.DgFactProv.RowHeadersWidth = 25
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgFactProv.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgFactProv.Size = New System.Drawing.Size(1511, 255)
        Me.DgFactProv.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button1.Location = New System.Drawing.Point(618, 92)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 31)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Consultar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(329, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(19, 15)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Al"
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaTer.Location = New System.Drawing.Point(354, 60)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(240, 23)
        Me.DtpFechaTer.TabIndex = 1
        Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(160, 15)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Periodo de Vencimiento"
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.CustomFormat = "yyyy-MM-dd"
        Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaIni.Location = New System.Drawing.Point(83, 60)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(240, 23)
        Me.DtpFechaIni.TabIndex = 0
        Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'BtnDetalle
        '
        Me.BtnDetalle.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnDetalle.Location = New System.Drawing.Point(720, 89)
        Me.BtnDetalle.Name = "BtnDetalle"
        Me.BtnDetalle.Size = New System.Drawing.Size(36, 34)
        Me.BtnDetalle.TabIndex = 6
        Me.BtnDetalle.UseVisualStyleBackColor = True
        '
        'CmbAgteVta
        '
        Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbAgteVta.FormattingEnabled = True
        Me.CmbAgteVta.Location = New System.Drawing.Point(83, 97)
        Me.CmbAgteVta.Name = "CmbAgteVta"
        Me.CmbAgteVta.Size = New System.Drawing.Size(515, 21)
        Me.CmbAgteVta.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 17)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Proveedor"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(52, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 15)
        Me.Label2.TabIndex = 148
        Me.Label2.Text = "Del"
        '
        'ChkVisDisa
        '
        Me.ChkVisDisa.AutoSize = True
        Me.ChkVisDisa.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkVisDisa.Location = New System.Drawing.Point(618, 64)
        Me.ChkVisDisa.Name = "ChkVisDisa"
        Me.ChkVisDisa.Size = New System.Drawing.Size(151, 21)
        Me.ChkVisDisa.TabIndex = 149
        Me.ChkVisDisa.Text = "Visualizar Disa y PJ"
        Me.ChkVisDisa.UseVisualStyleBackColor = True
        '
        'TxtTotEnPesos
        '
        Me.TxtTotEnPesos.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.TxtTotEnPesos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotEnPesos.Location = New System.Drawing.Point(986, 410)
        Me.TxtTotEnPesos.Name = "TxtTotEnPesos"
        Me.TxtTotEnPesos.ReadOnly = True
        Me.TxtTotEnPesos.Size = New System.Drawing.Size(103, 21)
        Me.TxtTotEnPesos.TabIndex = 150
        Me.TxtTotEnPesos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblTotal
        '
        Me.LblTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.LblTotal.AutoSize = True
        Me.LblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal.Location = New System.Drawing.Point(778, 414)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.Size = New System.Drawing.Size(205, 17)
        Me.LblTotal.TabIndex = 151
        Me.LblTotal.Text = "Total en moneda nacional MXP"
        '
        'ChkVerPagadas
        '
        Me.ChkVerPagadas.AutoSize = True
        Me.ChkVerPagadas.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkVerPagadas.Location = New System.Drawing.Point(775, 64)
        Me.ChkVerPagadas.Name = "ChkVerPagadas"
        Me.ChkVerPagadas.Size = New System.Drawing.Size(168, 21)
        Me.ChkVerPagadas.TabIndex = 152
        Me.ChkVerPagadas.Text = "Ver Facturas Pagadas"
        Me.ChkVerPagadas.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button2.Location = New System.Drawing.Point(835, 92)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(117, 42)
        Me.Button2.TabIndex = 153
        Me.Button2.Text = "Descuentos de proveedores"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'RdBCompras
        '
        Me.RdBCompras.AutoSize = True
        Me.RdBCompras.Checked = True
        Me.RdBCompras.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdBCompras.Location = New System.Drawing.Point(107, 13)
        Me.RdBCompras.Name = "RdBCompras"
        Me.RdBCompras.Size = New System.Drawing.Size(134, 20)
        Me.RdBCompras.TabIndex = 154
        Me.RdBCompras.TabStop = True
        Me.RdBCompras.Text = "Compras Directas"
        Me.RdBCompras.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.Location = New System.Drawing.Point(247, 12)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(135, 20)
        Me.RadioButton1.TabIndex = 155
        Me.RadioButton1.Text = "Gastos Generales"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(5, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 15)
        Me.Label7.TabIndex = 156
        Me.Label7.Text = "Tipo de Pago:"
        '
        'CmbGasto
        '
        Me.CmbGasto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbGasto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbGasto.FormattingEnabled = True
        Me.CmbGasto.Location = New System.Drawing.Point(509, 8)
        Me.CmbGasto.Name = "CmbGasto"
        Me.CmbGasto.Size = New System.Drawing.Size(515, 21)
        Me.CmbGasto.TabIndex = 157
        Me.CmbGasto.Visible = False
        '
        'CmbProveedor
        '
        Me.CmbProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbProveedor.FormattingEnabled = True
        Me.CmbProveedor.Location = New System.Drawing.Point(509, 33)
        Me.CmbProveedor.Name = "CmbProveedor"
        Me.CmbProveedor.Size = New System.Drawing.Size(515, 21)
        Me.CmbProveedor.TabIndex = 158
        Me.CmbProveedor.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(429, 34)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 17)
        Me.Label8.TabIndex = 159
        Me.Label8.Text = "Proveedor"
        Me.Label8.Visible = False
        '
        'ChkUSD
        '
        Me.ChkUSD.AutoSize = True
        Me.ChkUSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkUSD.Location = New System.Drawing.Point(949, 64)
        Me.ChkUSD.Name = "ChkUSD"
        Me.ChkUSD.Size = New System.Drawing.Size(114, 21)
        Me.ChkUSD.TabIndex = 160
        Me.ChkUSD.Text = "Ver Solo USD"
        Me.ChkUSD.UseVisualStyleBackColor = True
        '
        'ckbFacturasCerradas
        '
        Me.ckbFacturasCerradas.AutoSize = True
        Me.ckbFacturasCerradas.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbFacturasCerradas.Location = New System.Drawing.Point(1072, 14)
        Me.ckbFacturasCerradas.Name = "ckbFacturasCerradas"
        Me.ckbFacturasCerradas.Size = New System.Drawing.Size(170, 21)
        Me.ckbFacturasCerradas.TabIndex = 161
        Me.ckbFacturasCerradas.Text = "Ver Facturas Cerradas"
        Me.ckbFacturasCerradas.UseVisualStyleBackColor = True
        Me.ckbFacturasCerradas.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(1069, 64)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(144, 21)
        Me.CheckBox1.TabIndex = 162
        Me.CheckBox1.Text = "Facturas liberadas"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "FchDoc"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Fecha del Documento"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 70
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "DiasCred"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Dias de Credito"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Factura"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Doc Sap"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 40
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "FactProv"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Factura"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 50
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "IdProved"
        Me.DataGridViewTextBoxColumn5.HeaderText = "IdProv"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 50
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Proveedor"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Proveedor"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 260
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "TotFactura"
        Me.DataGridViewTextBoxColumn7.HeaderText = "$ Total Factura"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        Me.DataGridViewTextBoxColumn7.Width = 80
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Moneda"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Mnd"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 35
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Pagado"
        Me.DataGridViewTextBoxColumn9.HeaderText = "$ Importe Aplicado"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 80
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "SaldoPendiente"
        Me.DataGridViewTextBoxColumn10.HeaderText = "$ Saldo Pendiente"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Visible = False
        Me.DataGridViewTextBoxColumn10.Width = 85
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Moneda2"
        Me.DataGridViewTextBoxColumn11.HeaderText = "Mnd"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 35
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "DiasAtraso"
        Me.DataGridViewTextBoxColumn12.HeaderText = "Dias Atraso"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 40
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "FchVen"
        Me.DataGridViewTextBoxColumn13.HeaderText = "Fch. de Vencimiento"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Width = 70
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "SaldoPesos"
        Me.DataGridViewTextBoxColumn14.HeaderText = "$ Saldo MXP"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Width = 101
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Obrserv"
        Me.DataGridViewTextBoxColumn15.HeaderText = "Observaciones"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.Width = 184
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "Coment"
        Me.DataGridViewTextBoxColumn16.HeaderText = "Comentarios"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.Width = 265
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "SaldoPesos"
        Me.DataGridViewTextBoxColumn17.HeaderText = "$ Saldo MXP"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Width = 101
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "Obrserv"
        Me.DataGridViewTextBoxColumn18.HeaderText = "Observaciones"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        Me.DataGridViewTextBoxColumn18.Width = 184
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "Coment"
        Me.DataGridViewTextBoxColumn19.HeaderText = "Comentarios"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        Me.DataGridViewTextBoxColumn19.Width = 265
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.DataPropertyName = "Obrserv"
        Me.DataGridViewTextBoxColumn20.HeaderText = "Observaciones"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.Width = 184
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.DataPropertyName = "Coment"
        Me.DataGridViewTextBoxColumn21.HeaderText = "Comentarios"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.DataPropertyName = "CuentaDestino"
        Me.DataGridViewTextBoxColumn22.HeaderText = "Cuenta Destino"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        '
        'Frozen
        '
        Me.Frozen.HeaderText = "Bloq"
        Me.Frozen.Name = "Frozen"
        Me.Frozen.ReadOnly = True
        Me.Frozen.Width = 50
        '
        'Liberado
        '
        Me.Liberado.HeaderText = "Liberado"
        Me.Liberado.Name = "Liberado"
        Me.Liberado.ReadOnly = True
        '
        'RegSel
        '
        Me.RegSel.DataPropertyName = "RegSel"
        Me.RegSel.HeaderText = "Pagado"
        Me.RegSel.Name = "RegSel"
        Me.RegSel.ReadOnly = True
        Me.RegSel.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RegSel.Width = 50
        '
        'FchDoc
        '
        Me.FchDoc.DataPropertyName = "FchDoc"
        Me.FchDoc.HeaderText = "Fecha del Documento"
        Me.FchDoc.Name = "FchDoc"
        Me.FchDoc.ReadOnly = True
        Me.FchDoc.Width = 70
        '
        'DiasCred
        '
        Me.DiasCred.DataPropertyName = "DiasCred"
        Me.DiasCred.HeaderText = "Dias de Credito"
        Me.DiasCred.Name = "DiasCred"
        Me.DiasCred.ReadOnly = True
        Me.DiasCred.Width = 50
        '
        'Factura
        '
        Me.Factura.DataPropertyName = "Factura"
        Me.Factura.HeaderText = "Doc. Sap"
        Me.Factura.Name = "Factura"
        Me.Factura.ReadOnly = True
        Me.Factura.Width = 40
        '
        'FactProv
        '
        Me.FactProv.DataPropertyName = "FactProv"
        Me.FactProv.HeaderText = "Factura"
        Me.FactProv.Name = "FactProv"
        Me.FactProv.ReadOnly = True
        Me.FactProv.Width = 75
        '
        'U_IdLinea
        '
        Me.U_IdLinea.DataPropertyName = "U_IdLinea"
        Me.U_IdLinea.HeaderText = "Id. Línea"
        Me.U_IdLinea.Name = "U_IdLinea"
        Me.U_IdLinea.ReadOnly = True
        '
        'IdProved
        '
        Me.IdProved.DataPropertyName = "IdProved"
        Me.IdProved.HeaderText = "IdProv"
        Me.IdProved.Name = "IdProved"
        Me.IdProved.ReadOnly = True
        Me.IdProved.Width = 50
        '
        'Proveedor
        '
        Me.Proveedor.DataPropertyName = "Proveedor"
        Me.Proveedor.HeaderText = "Proveedor"
        Me.Proveedor.Name = "Proveedor"
        Me.Proveedor.ReadOnly = True
        Me.Proveedor.Width = 236
        '
        'NombreGasto
        '
        Me.NombreGasto.DataPropertyName = "TipoGasto"
        Me.NombreGasto.HeaderText = "Tipo de Gasto"
        Me.NombreGasto.Name = "NombreGasto"
        Me.NombreGasto.ReadOnly = True
        Me.NombreGasto.Visible = False
        Me.NombreGasto.Width = 170
        '
        'TotFactura
        '
        Me.TotFactura.DataPropertyName = "TotFactura"
        Me.TotFactura.HeaderText = "$ Total Factura"
        Me.TotFactura.Name = "TotFactura"
        Me.TotFactura.ReadOnly = True
        Me.TotFactura.Width = 80
        '
        'Moneda
        '
        Me.Moneda.DataPropertyName = "Moneda"
        Me.Moneda.HeaderText = "Mnd"
        Me.Moneda.Name = "Moneda"
        Me.Moneda.ReadOnly = True
        Me.Moneda.Width = 35
        '
        'Pagado
        '
        Me.Pagado.DataPropertyName = "Pagado"
        Me.Pagado.HeaderText = "$ Importe Aplicado"
        Me.Pagado.Name = "Pagado"
        Me.Pagado.ReadOnly = True
        Me.Pagado.Width = 80
        '
        'SaldoPendiente
        '
        Me.SaldoPendiente.DataPropertyName = "SaldoPendiente"
        Me.SaldoPendiente.HeaderText = "$ Saldo Pendiente"
        Me.SaldoPendiente.Name = "SaldoPendiente"
        Me.SaldoPendiente.ReadOnly = True
        Me.SaldoPendiente.Width = 80
        '
        'Moneda2
        '
        Me.Moneda2.DataPropertyName = "Moneda2"
        Me.Moneda2.HeaderText = "Mnd"
        Me.Moneda2.Name = "Moneda2"
        Me.Moneda2.ReadOnly = True
        Me.Moneda2.Width = 35
        '
        'DiasAtraso
        '
        Me.DiasAtraso.DataPropertyName = "DiasAtraso"
        Me.DiasAtraso.HeaderText = "Dias Atraso"
        Me.DiasAtraso.Name = "DiasAtraso"
        Me.DiasAtraso.ReadOnly = True
        Me.DiasAtraso.Width = 40
        '
        'FchVen
        '
        Me.FchVen.DataPropertyName = "FchVen"
        Me.FchVen.HeaderText = "Fch. de Vencimiento"
        Me.FchVen.Name = "FchVen"
        Me.FchVen.ReadOnly = True
        Me.FchVen.Width = 70
        '
        'SaldoPesos
        '
        Me.SaldoPesos.DataPropertyName = "SaldoPesos"
        Me.SaldoPesos.HeaderText = "$ Saldo MXP"
        Me.SaldoPesos.Name = "SaldoPesos"
        Me.SaldoPesos.ReadOnly = True
        Me.SaldoPesos.Width = 101
        '
        'Referencia
        '
        Me.Referencia.DataPropertyName = "Referencia"
        Me.Referencia.HeaderText = "Referencia"
        Me.Referencia.Name = "Referencia"
        Me.Referencia.ReadOnly = True
        '
        'Obrserv
        '
        Me.Obrserv.DataPropertyName = "Obrserv"
        Me.Obrserv.HeaderText = "Observaciones"
        Me.Obrserv.Name = "Obrserv"
        Me.Obrserv.ReadOnly = True
        Me.Obrserv.Width = 184
        '
        'Comentarios
        '
        Me.Comentarios.DataPropertyName = "Coment"
        Me.Comentarios.HeaderText = "Comentarios"
        Me.Comentarios.Name = "Comentarios"
        Me.Comentarios.ReadOnly = True
        '
        'Descuento
        '
        Me.Descuento.DataPropertyName = "Descuento"
        Me.Descuento.HeaderText = "Descuento"
        Me.Descuento.Name = "Descuento"
        Me.Descuento.ReadOnly = True
        '
        'BancoOrigen
        '
        Me.BancoOrigen.DataPropertyName = "BancoOrigen"
        Me.BancoOrigen.HeaderText = "Banco Origen"
        Me.BancoOrigen.Name = "BancoOrigen"
        Me.BancoOrigen.ReadOnly = True
        '
        'BancoDestino
        '
        Me.BancoDestino.DataPropertyName = "BancoDestino"
        Me.BancoDestino.HeaderText = "Banco Destino"
        Me.BancoDestino.Name = "BancoDestino"
        Me.BancoDestino.ReadOnly = True
        '
        'CuentaDestino
        '
        Me.CuentaDestino.DataPropertyName = "CuentaDestino"
        Me.CuentaDestino.HeaderText = "Cuenta Destino"
        Me.CuentaDestino.Name = "CuentaDestino"
        Me.CuentaDestino.ReadOnly = True
        '
        'PagoProveedores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1522, 435)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.ckbFacturasCerradas)
        Me.Controls.Add(Me.ChkUSD)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.CmbProveedor)
        Me.Controls.Add(Me.CmbGasto)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.RdBCompras)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TxtTotEnPesos)
        Me.Controls.Add(Me.ChkVerPagadas)
        Me.Controls.Add(Me.LblTotal)
        Me.Controls.Add(Me.ChkVisDisa)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CmbAgteVta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.BtnDetalle)
        Me.Controls.Add(Me.DgFactProv)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DtpFechaTer)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DtpFechaIni)
        Me.Name = "PagoProveedores"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pagos a Proveedores"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DgFactProv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BtnDetalle As System.Windows.Forms.Button
    Friend WithEvents DgFactProv As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ChkVisDisa As System.Windows.Forms.CheckBox
    Friend WithEvents TxtTotEnPesos As System.Windows.Forms.TextBox
    Friend WithEvents LblTotal As System.Windows.Forms.Label
    Friend WithEvents ChkVerPagadas As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents RdBCompras As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CmbGasto As System.Windows.Forms.ComboBox
    Friend WithEvents CmbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ChkUSD As System.Windows.Forms.CheckBox
    Friend WithEvents ckbFacturasCerradas As CheckBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents DataGridViewTextBoxColumn20 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As DataGridViewTextBoxColumn
    Friend WithEvents Frozen As DataGridViewCheckBoxColumn
    Friend WithEvents Liberado As DataGridViewCheckBoxColumn
    Friend WithEvents RegSel As DataGridViewCheckBoxColumn
    Friend WithEvents FchDoc As DataGridViewTextBoxColumn
    Friend WithEvents DiasCred As DataGridViewTextBoxColumn
    Friend WithEvents Factura As DataGridViewTextBoxColumn
    Friend WithEvents FactProv As DataGridViewTextBoxColumn
    Friend WithEvents U_IdLinea As DataGridViewTextBoxColumn
    Friend WithEvents IdProved As DataGridViewTextBoxColumn
    Friend WithEvents Proveedor As DataGridViewTextBoxColumn
    Friend WithEvents NombreGasto As DataGridViewTextBoxColumn
    Friend WithEvents TotFactura As DataGridViewTextBoxColumn
    Friend WithEvents Moneda As DataGridViewTextBoxColumn
    Friend WithEvents Pagado As DataGridViewTextBoxColumn
    Friend WithEvents SaldoPendiente As DataGridViewTextBoxColumn
    Friend WithEvents Moneda2 As DataGridViewTextBoxColumn
    Friend WithEvents DiasAtraso As DataGridViewTextBoxColumn
    Friend WithEvents FchVen As DataGridViewTextBoxColumn
    Friend WithEvents SaldoPesos As DataGridViewTextBoxColumn
    Friend WithEvents Referencia As DataGridViewTextBoxColumn
    Friend WithEvents Obrserv As DataGridViewTextBoxColumn
    Friend WithEvents Comentarios As DataGridViewTextBoxColumn
    Friend WithEvents Descuento As DataGridViewTextBoxColumn
    Friend WithEvents BancoOrigen As DataGridViewTextBoxColumn
    Friend WithEvents BancoDestino As DataGridViewTextBoxColumn
    Friend WithEvents CuentaDestino As DataGridViewTextBoxColumn
End Class
