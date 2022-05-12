<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReciboMatSeg
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
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BtnConsulta = New System.Windows.Forms.Button()
        Me.CmbProv = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ChkVerTodasOrdC = New System.Windows.Forms.CheckBox()
        Me.TxtTotEnPesos = New System.Windows.Forms.TextBox()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DgvOrdCompra = New System.Windows.Forms.DataGridView()
        Me.DgvDetOComp = New System.Windows.Forms.DataGridView()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DgvFactEnt = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.LblMensajeCon = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DgvRecibos = New System.Windows.Forms.DataGridView()
        Me.BtnMenos = New System.Windows.Forms.Button()
        Me.BtnMas = New System.Windows.Forms.Button()
        Me.BtnFacturas = New System.Windows.Forms.Button()
        Me.BtnRecibos = New System.Windows.Forms.Button()
        Me.BtnDetalle = New System.Windows.Forms.Button()
        Me.BtnOCompra = New System.Windows.Forms.Button()
        Me.DgvRecOrdF = New System.Windows.Forms.DataGridView()
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
        Me.Label13 = New System.Windows.Forms.Label()
        CType(Me.DgvOrdCompra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvDetOComp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvFactEnt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvRecibos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvRecOrdF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1032, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "O. Compra"
        Me.Label4.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(5, 67)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(134, 17)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Ordenes de compra"
        '
        'BtnConsulta
        '
        Me.BtnConsulta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnConsulta.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnConsulta.Location = New System.Drawing.Point(685, 21)
        Me.BtnConsulta.Name = "BtnConsulta"
        Me.BtnConsulta.Size = New System.Drawing.Size(75, 31)
        Me.BtnConsulta.TabIndex = 1
        Me.BtnConsulta.Text = "Consultar"
        Me.BtnConsulta.UseVisualStyleBackColor = True
        '
        'CmbProv
        '
        Me.CmbProv.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbProv.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbProv.FormattingEnabled = True
        Me.CmbProv.Location = New System.Drawing.Point(75, 42)
        Me.CmbProv.Name = "CmbProv"
        Me.CmbProv.Size = New System.Drawing.Size(584, 21)
        Me.CmbProv.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 17)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Proveedor"
        '
        'ChkVerTodasOrdC
        '
        Me.ChkVerTodasOrdC.AutoSize = True
        Me.ChkVerTodasOrdC.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkVerTodasOrdC.Location = New System.Drawing.Point(677, 47)
        Me.ChkVerTodasOrdC.Name = "ChkVerTodasOrdC"
        Me.ChkVerTodasOrdC.Size = New System.Drawing.Size(247, 21)
        Me.ChkVerTodasOrdC.TabIndex = 149
        Me.ChkVerTodasOrdC.Text = "Ver Todas las Ordenes de Compra"
        Me.ChkVerTodasOrdC.UseVisualStyleBackColor = True
        Me.ChkVerTodasOrdC.Visible = False
        '
        'TxtTotEnPesos
        '
        Me.TxtTotEnPesos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotEnPesos.Location = New System.Drawing.Point(1238, 548)
        Me.TxtTotEnPesos.Name = "TxtTotEnPesos"
        Me.TxtTotEnPesos.ReadOnly = True
        Me.TxtTotEnPesos.Size = New System.Drawing.Size(80, 21)
        Me.TxtTotEnPesos.TabIndex = 150
        Me.TxtTotEnPesos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTotEnPesos.Visible = False
        '
        'LblTotal
        '
        Me.LblTotal.AutoSize = True
        Me.LblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal.Location = New System.Drawing.Point(1152, 550)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.Size = New System.Drawing.Size(89, 17)
        Me.LblTotal.TabIndex = 151
        Me.LblTotal.Text = "Total MXP  $"
        Me.LblTotal.Visible = False
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaTer.Location = New System.Drawing.Point(395, 12)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(264, 23)
        Me.DtpFechaTer.TabIndex = 7
        Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(25, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 17)
        Me.Label5.TabIndex = 155
        Me.Label5.Text = "Desde"
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaIni.Location = New System.Drawing.Point(75, 15)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(265, 23)
        Me.DtpFechaIni.TabIndex = 6
        Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(353, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 17)
        Me.Label3.TabIndex = 156
        Me.Label3.Text = "Hasta"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(4, -1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(154, 17)
        Me.Label7.TabIndex = 157
        Me.Label7.Text = "Fecha Contabilización  "
        '
        'DgvOrdCompra
        '
        Me.DgvOrdCompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvOrdCompra.Location = New System.Drawing.Point(4, 85)
        Me.DgvOrdCompra.Name = "DgvOrdCompra"
        Me.DgvOrdCompra.Size = New System.Drawing.Size(734, 329)
        Me.DgvOrdCompra.TabIndex = 2
        '
        'DgvDetOComp
        '
        Me.DgvDetOComp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DgvDetOComp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvDetOComp.Location = New System.Drawing.Point(4, 436)
        Me.DgvDetOComp.Name = "DgvDetOComp"
        Me.DgvDetOComp.Size = New System.Drawing.Size(734, 222)
        Me.DgvDetOComp.TabIndex = 163
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(3, 416)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(189, 17)
        Me.Label9.TabIndex = 164
        Me.Label9.Text = "Detalle de Orden de Compra"
        Me.Label9.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(998, 347)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(228, 17)
        Me.Label10.TabIndex = 166
        Me.Label10.Text = "Facturas / Entradas de Mercancias"
        Me.Label10.Visible = False
        '
        'DgvFactEnt
        '
        Me.DgvFactEnt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvFactEnt.Location = New System.Drawing.Point(999, 369)
        Me.DgvFactEnt.Name = "DgvFactEnt"
        Me.DgvFactEnt.Size = New System.Drawing.Size(319, 178)
        Me.DgvFactEnt.TabIndex = 165
        Me.DgvFactEnt.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1145, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 17)
        Me.Label2.TabIndex = 170
        Me.Label2.Text = "Detalle"
        Me.Label2.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1236, 21)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(59, 17)
        Me.Label11.TabIndex = 172
        Me.Label11.Text = "Recibos"
        Me.Label11.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(1332, 21)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(63, 17)
        Me.Label12.TabIndex = 174
        Me.Label12.Text = "Facturas"
        Me.Label12.Visible = False
        '
        'LblMensajeCon
        '
        Me.LblMensajeCon.AutoSize = True
        Me.LblMensajeCon.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMensajeCon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMensajeCon.Location = New System.Drawing.Point(764, 29)
        Me.LblMensajeCon.Name = "LblMensajeCon"
        Me.LblMensajeCon.Size = New System.Drawing.Size(178, 17)
        Me.LblMensajeCon.TabIndex = 175
        Me.LblMensajeCon.Text = "Ejecutando Consulta. . . . . "
        Me.LblMensajeCon.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(791, 416)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(133, 17)
        Me.Label8.TabIndex = 177
        Me.Label8.Text = "Recibos de Material"
        '
        'DgvRecibos
        '
        Me.DgvRecibos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DgvRecibos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvRecibos.Location = New System.Drawing.Point(777, 436)
        Me.DgvRecibos.Name = "DgvRecibos"
        Me.DgvRecibos.Size = New System.Drawing.Size(541, 222)
        Me.DgvRecibos.TabIndex = 3
        '
        'BtnMenos
        '
        Me.BtnMenos.BackColor = System.Drawing.SystemColors.Control
        Me.BtnMenos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMenos.Image = Global.TPDiamante.My.Resources.Resources.Minus__6_
        Me.BtnMenos.Location = New System.Drawing.Point(739, 480)
        Me.BtnMenos.Name = "BtnMenos"
        Me.BtnMenos.Size = New System.Drawing.Size(38, 38)
        Me.BtnMenos.TabIndex = 5
        Me.BtnMenos.UseVisualStyleBackColor = False
        '
        'BtnMas
        '
        Me.BtnMas.BackColor = System.Drawing.SystemColors.Control
        Me.BtnMas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMas.Image = Global.TPDiamante.My.Resources.Resources.Plus__6_
        Me.BtnMas.Location = New System.Drawing.Point(739, 436)
        Me.BtnMas.Name = "BtnMas"
        Me.BtnMas.Size = New System.Drawing.Size(38, 38)
        Me.BtnMas.TabIndex = 4
        Me.BtnMas.UseVisualStyleBackColor = False
        '
        'BtnFacturas
        '
        Me.BtnFacturas.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnFacturas.Location = New System.Drawing.Point(1297, 14)
        Me.BtnFacturas.Name = "BtnFacturas"
        Me.BtnFacturas.Size = New System.Drawing.Size(36, 34)
        Me.BtnFacturas.TabIndex = 173
        Me.BtnFacturas.UseVisualStyleBackColor = True
        Me.BtnFacturas.Visible = False
        '
        'BtnRecibos
        '
        Me.BtnRecibos.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnRecibos.Location = New System.Drawing.Point(1201, 14)
        Me.BtnRecibos.Name = "BtnRecibos"
        Me.BtnRecibos.Size = New System.Drawing.Size(36, 34)
        Me.BtnRecibos.TabIndex = 171
        Me.BtnRecibos.UseVisualStyleBackColor = True
        Me.BtnRecibos.Visible = False
        '
        'BtnDetalle
        '
        Me.BtnDetalle.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnDetalle.Location = New System.Drawing.Point(1110, 14)
        Me.BtnDetalle.Name = "BtnDetalle"
        Me.BtnDetalle.Size = New System.Drawing.Size(36, 34)
        Me.BtnDetalle.TabIndex = 169
        Me.BtnDetalle.UseVisualStyleBackColor = True
        Me.BtnDetalle.Visible = False
        '
        'BtnOCompra
        '
        Me.BtnOCompra.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnOCompra.Location = New System.Drawing.Point(997, 14)
        Me.BtnOCompra.Name = "BtnOCompra"
        Me.BtnOCompra.Size = New System.Drawing.Size(36, 34)
        Me.BtnOCompra.TabIndex = 6
        Me.BtnOCompra.UseVisualStyleBackColor = True
        Me.BtnOCompra.Visible = False
        '
        'DgvRecOrdF
        '
        Me.DgvRecOrdF.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DgvRecOrdF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvRecOrdF.Location = New System.Drawing.Point(744, 85)
        Me.DgvRecOrdF.Name = "DgvRecOrdF"
        Me.DgvRecOrdF.Size = New System.Drawing.Size(574, 328)
        Me.DgvRecOrdF.TabIndex = 179
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
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(744, 67)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(276, 17)
        Me.Label13.TabIndex = 180
        Me.Label13.Text = "Recibos de Material Ordenados por Fecha"
        '
        'ReciboMatSeg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1326, 664)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.DgvRecOrdF)
        Me.Controls.Add(Me.BtnMenos)
        Me.Controls.Add(Me.BtnMas)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.DgvRecibos)
        Me.Controls.Add(Me.LblMensajeCon)
        Me.Controls.Add(Me.BtnFacturas)
        Me.Controls.Add(Me.BtnRecibos)
        Me.Controls.Add(Me.BtnDetalle)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.DgvFactEnt)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.DgvDetOComp)
        Me.Controls.Add(Me.DgvOrdCompra)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.DtpFechaTer)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DtpFechaIni)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtTotEnPesos)
        Me.Controls.Add(Me.ChkVerTodasOrdC)
        Me.Controls.Add(Me.CmbProv)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.BtnOCompra)
        Me.Controls.Add(Me.BtnConsulta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblTotal)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Name = "ReciboMatSeg"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Recibo de Materiales"
        CType(Me.DgvOrdCompra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvDetOComp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvFactEnt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvRecibos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvRecOrdF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BtnOCompra As System.Windows.Forms.Button
    Friend WithEvents BtnConsulta As System.Windows.Forms.Button
    Friend WithEvents CmbProv As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ChkVerTodasOrdC As System.Windows.Forms.CheckBox
    Friend WithEvents TxtTotEnPesos As System.Windows.Forms.TextBox
    Friend WithEvents LblTotal As System.Windows.Forms.Label
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
    Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DgvOrdCompra As System.Windows.Forms.DataGridView
    Friend WithEvents DgvDetOComp As System.Windows.Forms.DataGridView
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DgvFactEnt As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BtnDetalle As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents BtnRecibos As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents BtnFacturas As System.Windows.Forms.Button
    Friend WithEvents LblMensajeCon As System.Windows.Forms.Label
    Friend WithEvents BtnMenos As System.Windows.Forms.Button
    Friend WithEvents BtnMas As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DgvRecibos As System.Windows.Forms.DataGridView
    Friend WithEvents DgvRecOrdF As System.Windows.Forms.DataGridView
    Friend WithEvents Label13 As System.Windows.Forms.Label
End Class
