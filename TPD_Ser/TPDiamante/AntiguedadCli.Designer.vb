<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AntiguedadCli
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.CmbCliente = New System.Windows.Forms.ComboBox()
        Me.lAlm = New System.Windows.Forms.Label()
        Me.cmbAlmacen = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.CBCobranza = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DGClientes = New System.Windows.Forms.DataGridView()
        Me.DGDetalle = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.pEncabezado = New System.Windows.Forms.Panel()
        Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.BtnClientes = New System.Windows.Forms.Button()
        Me.dias0 = New System.Windows.Forms.Label()
        Me.dias31 = New System.Windows.Forms.Label()
        Me.dias61 = New System.Windows.Forms.Label()
        Me.dias91 = New System.Windows.Forms.Label()
        Me.dias121 = New System.Windows.Forms.Label()
        Me.abono = New System.Windows.Forms.Label()
        Me.saldo = New System.Windows.Forms.Label()
        Me.Montos = New System.Windows.Forms.Label()
        Me.l10 = New System.Windows.Forms.Label()
        Me.l11 = New System.Windows.Forms.Label()
        Me.l16 = New System.Windows.Forms.Label()
        Me.l15 = New System.Windows.Forms.Label()
        Me.l14 = New System.Windows.Forms.Label()
        Me.l13 = New System.Windows.Forms.Label()
        Me.l12 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DGClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pEncabezado.SuspendLayout()
        Me.SuspendLayout()
        '
        'CmbCliente
        '
        Me.CmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbCliente.FormattingEnabled = True
        Me.CmbCliente.Location = New System.Drawing.Point(132, 86)
        Me.CmbCliente.Name = "CmbCliente"
        Me.CmbCliente.Size = New System.Drawing.Size(245, 21)
        Me.CmbCliente.TabIndex = 199
        '
        'lAlm
        '
        Me.lAlm.AutoSize = True
        Me.lAlm.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lAlm.Location = New System.Drawing.Point(13, 13)
        Me.lAlm.Name = "lAlm"
        Me.lAlm.Size = New System.Drawing.Size(63, 17)
        Me.lAlm.TabIndex = 197
        Me.lAlm.Text = "Sucursal"
        '
        'cmbAlmacen
        '
        Me.cmbAlmacen.FormattingEnabled = True
        Me.cmbAlmacen.Location = New System.Drawing.Point(132, 13)
        Me.cmbAlmacen.Name = "cmbAlmacen"
        Me.cmbAlmacen.Size = New System.Drawing.Size(147, 21)
        Me.cmbAlmacen.TabIndex = 198
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(13, 86)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 17)
        Me.Label13.TabIndex = 196
        Me.Label13.Text = "Cliente"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(13, 38)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(118, 17)
        Me.Label14.TabIndex = 194
        Me.Label14.Text = "Agente Cobranza"
        '
        'CBCobranza
        '
        Me.CBCobranza.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBCobranza.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBCobranza.FormattingEnabled = True
        Me.CBCobranza.Location = New System.Drawing.Point(132, 38)
        Me.CBCobranza.Name = "CBCobranza"
        Me.CBCobranza.Size = New System.Drawing.Size(205, 21)
        Me.CBCobranza.TabIndex = 195
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Gainsboro
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(12, 419)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 17)
        Me.Label11.TabIndex = 201
        Me.Label11.Text = "Detalle"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Gainsboro
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(12, 142)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 17)
        Me.Label2.TabIndex = 200
        Me.Label2.Text = "Clientes"
        '
        'DGClientes
        '
        Me.DGClientes.AllowUserToAddRows = False
        Me.DGClientes.AllowUserToDeleteRows = False
        Me.DGClientes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGClientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.DGClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGClientes.EnableHeadersVisualStyles = False
        Me.DGClientes.Location = New System.Drawing.Point(12, 162)
        Me.DGClientes.Name = "DGClientes"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGClientes.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGClientes.Size = New System.Drawing.Size(1326, 220)
        Me.DGClientes.TabIndex = 202
        '
        'DGDetalle
        '
        Me.DGDetalle.AllowUserToAddRows = False
        Me.DGDetalle.AllowUserToDeleteRows = False
        Me.DGDetalle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGDetalle.EnableHeadersVisualStyles = False
        Me.DGDetalle.Location = New System.Drawing.Point(12, 439)
        Me.DGDetalle.Name = "DGDetalle"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGDetalle.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DGDetalle.Size = New System.Drawing.Size(1326, 220)
        Me.DGDetalle.TabIndex = 203
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button1.Location = New System.Drawing.Point(603, 33)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(93, 32)
        Me.Button1.TabIndex = 204
        Me.Button1.Text = "Consultar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(410, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 17)
        Me.Label1.TabIndex = 205
        Me.Label1.Text = "Intervalo Días"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(413, 48)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(35, 20)
        Me.TextBox1.TabIndex = 206
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(454, 48)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(35, 20)
        Me.TextBox2.TabIndex = 207
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(495, 48)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(35, 20)
        Me.TextBox3.TabIndex = 208
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(536, 48)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(35, 20)
        Me.TextBox4.TabIndex = 209
        '
        'pEncabezado
        '
        Me.pEncabezado.Controls.Add(Me.CmbAgteVta)
        Me.pEncabezado.Controls.Add(Me.Label4)
        Me.pEncabezado.Controls.Add(Me.Label3)
        Me.pEncabezado.Controls.Add(Me.Button2)
        Me.pEncabezado.Controls.Add(Me.Label12)
        Me.pEncabezado.Controls.Add(Me.BtnClientes)
        Me.pEncabezado.Controls.Add(Me.Label1)
        Me.pEncabezado.Controls.Add(Me.TextBox4)
        Me.pEncabezado.Controls.Add(Me.CBCobranza)
        Me.pEncabezado.Controls.Add(Me.TextBox3)
        Me.pEncabezado.Controls.Add(Me.Label14)
        Me.pEncabezado.Controls.Add(Me.TextBox2)
        Me.pEncabezado.Controls.Add(Me.Label13)
        Me.pEncabezado.Controls.Add(Me.TextBox1)
        Me.pEncabezado.Controls.Add(Me.cmbAlmacen)
        Me.pEncabezado.Controls.Add(Me.lAlm)
        Me.pEncabezado.Controls.Add(Me.Button1)
        Me.pEncabezado.Controls.Add(Me.CmbCliente)
        Me.pEncabezado.Location = New System.Drawing.Point(15, 15)
        Me.pEncabezado.Name = "pEncabezado"
        Me.pEncabezado.Size = New System.Drawing.Size(1083, 119)
        Me.pEncabezado.TabIndex = 210
        '
        'CmbAgteVta
        '
        Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbAgteVta.FormattingEnabled = True
        Me.CmbAgteVta.Location = New System.Drawing.Point(132, 62)
        Me.CmbAgteVta.Name = "CmbAgteVta"
        Me.CmbAgteVta.Size = New System.Drawing.Size(205, 21)
        Me.CmbAgteVta.TabIndex = 215
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 17)
        Me.Label4.TabIndex = 214
        Me.Label4.Text = "Agente Ventas"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(943, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 17)
        Me.Label3.TabIndex = 213
        Me.Label3.Text = "Detalle"
        '
        'Button2
        '
        Me.Button2.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button2.Location = New System.Drawing.Point(1001, 15)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(36, 34)
        Me.Button2.TabIndex = 212
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(821, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 17)
        Me.Label12.TabIndex = 211
        Me.Label12.Text = "Clientes"
        '
        'BtnClientes
        '
        Me.BtnClientes.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnClientes.Location = New System.Drawing.Point(883, 15)
        Me.BtnClientes.Name = "BtnClientes"
        Me.BtnClientes.Size = New System.Drawing.Size(36, 34)
        Me.BtnClientes.TabIndex = 210
        Me.BtnClientes.UseVisualStyleBackColor = True
        '
        'dias0
        '
        Me.dias0.AutoSize = True
        Me.dias0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dias0.Location = New System.Drawing.Point(753, 385)
        Me.dias0.Name = "dias0"
        Me.dias0.Size = New System.Drawing.Size(80, 13)
        Me.dias0.TabIndex = 211
        Me.dias0.Text = "Montos Totales"
        Me.dias0.Visible = False
        '
        'dias31
        '
        Me.dias31.AutoSize = True
        Me.dias31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dias31.Location = New System.Drawing.Point(844, 385)
        Me.dias31.Name = "dias31"
        Me.dias31.Size = New System.Drawing.Size(80, 13)
        Me.dias31.TabIndex = 212
        Me.dias31.Text = "Montos Totales"
        Me.dias31.Visible = False
        '
        'dias61
        '
        Me.dias61.AutoSize = True
        Me.dias61.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dias61.Location = New System.Drawing.Point(937, 385)
        Me.dias61.Name = "dias61"
        Me.dias61.Size = New System.Drawing.Size(80, 13)
        Me.dias61.TabIndex = 213
        Me.dias61.Text = "Montos Totales"
        Me.dias61.Visible = False
        '
        'dias91
        '
        Me.dias91.AutoSize = True
        Me.dias91.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dias91.Location = New System.Drawing.Point(1029, 385)
        Me.dias91.Name = "dias91"
        Me.dias91.Size = New System.Drawing.Size(80, 13)
        Me.dias91.TabIndex = 214
        Me.dias91.Text = "Montos Totales"
        Me.dias91.Visible = False
        '
        'dias121
        '
        Me.dias121.AutoSize = True
        Me.dias121.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dias121.Location = New System.Drawing.Point(1120, 385)
        Me.dias121.Name = "dias121"
        Me.dias121.Size = New System.Drawing.Size(80, 13)
        Me.dias121.TabIndex = 215
        Me.dias121.Text = "Montos Totales"
        Me.dias121.Visible = False
        '
        'abono
        '
        Me.abono.AutoSize = True
        Me.abono.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.abono.Location = New System.Drawing.Point(660, 385)
        Me.abono.Name = "abono"
        Me.abono.Size = New System.Drawing.Size(80, 13)
        Me.abono.TabIndex = 216
        Me.abono.Text = "Montos Totales"
        Me.abono.Visible = False
        '
        'saldo
        '
        Me.saldo.AutoSize = True
        Me.saldo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saldo.Location = New System.Drawing.Point(567, 385)
        Me.saldo.Name = "saldo"
        Me.saldo.Size = New System.Drawing.Size(80, 13)
        Me.saldo.TabIndex = 217
        Me.saldo.Text = "Montos Totales"
        Me.saldo.Visible = False
        '
        'Montos
        '
        Me.Montos.AutoSize = True
        Me.Montos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Montos.Location = New System.Drawing.Point(359, 395)
        Me.Montos.Name = "Montos"
        Me.Montos.Size = New System.Drawing.Size(120, 13)
        Me.Montos.TabIndex = 218
        Me.Montos.Text = "MONTOS TOTALES"
        Me.Montos.Visible = False
        '
        'l10
        '
        Me.l10.AutoSize = True
        Me.l10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l10.Location = New System.Drawing.Point(571, 408)
        Me.l10.Name = "l10"
        Me.l10.Size = New System.Drawing.Size(15, 13)
        Me.l10.TabIndex = 225
        Me.l10.Text = "%"
        Me.l10.Visible = False
        '
        'l11
        '
        Me.l11.AutoSize = True
        Me.l11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l11.Location = New System.Drawing.Point(663, 408)
        Me.l11.Name = "l11"
        Me.l11.Size = New System.Drawing.Size(15, 13)
        Me.l11.TabIndex = 224
        Me.l11.Text = "%"
        Me.l11.Visible = False
        '
        'l16
        '
        Me.l16.AutoSize = True
        Me.l16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l16.Location = New System.Drawing.Point(1120, 408)
        Me.l16.Name = "l16"
        Me.l16.Size = New System.Drawing.Size(15, 13)
        Me.l16.TabIndex = 223
        Me.l16.Text = "%"
        Me.l16.Visible = False
        '
        'l15
        '
        Me.l15.AutoSize = True
        Me.l15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l15.Location = New System.Drawing.Point(1029, 408)
        Me.l15.Name = "l15"
        Me.l15.Size = New System.Drawing.Size(15, 13)
        Me.l15.TabIndex = 222
        Me.l15.Text = "%"
        Me.l15.Visible = False
        '
        'l14
        '
        Me.l14.AutoSize = True
        Me.l14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l14.Location = New System.Drawing.Point(937, 408)
        Me.l14.Name = "l14"
        Me.l14.Size = New System.Drawing.Size(15, 13)
        Me.l14.TabIndex = 221
        Me.l14.Text = "%"
        Me.l14.Visible = False
        '
        'l13
        '
        Me.l13.AutoSize = True
        Me.l13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l13.Location = New System.Drawing.Point(845, 408)
        Me.l13.Name = "l13"
        Me.l13.Size = New System.Drawing.Size(15, 13)
        Me.l13.TabIndex = 220
        Me.l13.Text = "%"
        Me.l13.Visible = False
        '
        'l12
        '
        Me.l12.AutoSize = True
        Me.l12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l12.Location = New System.Drawing.Point(755, 408)
        Me.l12.Name = "l12"
        Me.l12.Size = New System.Drawing.Size(15, 13)
        Me.l12.TabIndex = 219
        Me.l12.Text = "%"
        Me.l12.Visible = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn2.HeaderText = "Column2"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn3.HeaderText = "Column3"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.Red
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn4.HeaderText = "Column4"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'AntiguedadCli
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1350, 668)
        Me.Controls.Add(Me.l10)
        Me.Controls.Add(Me.l11)
        Me.Controls.Add(Me.l16)
        Me.Controls.Add(Me.l15)
        Me.Controls.Add(Me.l14)
        Me.Controls.Add(Me.l13)
        Me.Controls.Add(Me.l12)
        Me.Controls.Add(Me.Montos)
        Me.Controls.Add(Me.saldo)
        Me.Controls.Add(Me.abono)
        Me.Controls.Add(Me.dias121)
        Me.Controls.Add(Me.dias91)
        Me.Controls.Add(Me.dias61)
        Me.Controls.Add(Me.dias31)
        Me.Controls.Add(Me.dias0)
        Me.Controls.Add(Me.pEncabezado)
        Me.Controls.Add(Me.DGDetalle)
        Me.Controls.Add(Me.DGClientes)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label2)
        Me.Name = "AntiguedadCli"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Antiguedad de saldos de Clientes"
        CType(Me.DGClientes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pEncabezado.ResumeLayout(False)
        Me.pEncabezado.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents lAlm As System.Windows.Forms.Label
    Friend WithEvents cmbAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents CBCobranza As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DGClientes As System.Windows.Forms.DataGridView
    Friend WithEvents DGDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents pEncabezado As System.Windows.Forms.Panel
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents BtnClientes As System.Windows.Forms.Button
    Friend WithEvents dias0 As System.Windows.Forms.Label
    Friend WithEvents dias31 As System.Windows.Forms.Label
    Friend WithEvents dias61 As System.Windows.Forms.Label
    Friend WithEvents dias91 As System.Windows.Forms.Label
    Friend WithEvents dias121 As System.Windows.Forms.Label
    Friend WithEvents abono As System.Windows.Forms.Label
    Friend WithEvents saldo As System.Windows.Forms.Label
    Friend WithEvents Montos As System.Windows.Forms.Label
    Friend WithEvents l10 As System.Windows.Forms.Label
    Friend WithEvents l11 As System.Windows.Forms.Label
    Friend WithEvents l16 As System.Windows.Forms.Label
    Friend WithEvents l15 As System.Windows.Forms.Label
    Friend WithEvents l14 As System.Windows.Forms.Label
    Friend WithEvents l13 As System.Windows.Forms.Label
    Friend WithEvents l12 As System.Windows.Forms.Label
    Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
