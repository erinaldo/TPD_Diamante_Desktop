<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PagosRealizados
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
  Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Me.ChkUSD = New System.Windows.Forms.CheckBox()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.CmbProveedor = New System.Windows.Forms.ComboBox()
  Me.CmbGasto = New System.Windows.Forms.ComboBox()
  Me.Label7 = New System.Windows.Forms.Label()
  Me.RadioButton1 = New System.Windows.Forms.RadioButton()
  Me.RdBCompras = New System.Windows.Forms.RadioButton()
  Me.Button2 = New System.Windows.Forms.Button()
  Me.ChkVisDisa = New System.Windows.Forms.CheckBox()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.BtnDetalle = New System.Windows.Forms.Button()
  Me.Button1 = New System.Windows.Forms.Button()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.DgFactProv = New System.Windows.Forms.DataGridView()
  Me.FchDoc = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DiasCred = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.Factura = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.FactProv = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
  Me.TxtTotEnPesos = New System.Windows.Forms.TextBox()
  Me.LblTotal = New System.Windows.Forms.Label()
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
  CType(Me.DgFactProv, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'ChkUSD
  '
  Me.ChkUSD.AutoSize = True
  Me.ChkUSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.ChkUSD.Location = New System.Drawing.Point(796, 68)
  Me.ChkUSD.Name = "ChkUSD"
  Me.ChkUSD.Size = New System.Drawing.Size(114, 21)
  Me.ChkUSD.TabIndex = 181
  Me.ChkUSD.Text = "Ver Solo USD"
  Me.ChkUSD.UseVisualStyleBackColor = True
  '
  'Label8
  '
  Me.Label8.AutoSize = True
  Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label8.Location = New System.Drawing.Point(438, 38)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(74, 17)
  Me.Label8.TabIndex = 180
  Me.Label8.Text = "Proveedor"
  Me.Label8.Visible = False
  '
  'CmbProveedor
  '
  Me.CmbProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbProveedor.FormattingEnabled = True
  Me.CmbProveedor.Location = New System.Drawing.Point(518, 37)
  Me.CmbProveedor.Name = "CmbProveedor"
  Me.CmbProveedor.Size = New System.Drawing.Size(515, 21)
  Me.CmbProveedor.TabIndex = 179
  Me.CmbProveedor.Visible = False
  '
  'CmbGasto
  '
  Me.CmbGasto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbGasto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbGasto.FormattingEnabled = True
  Me.CmbGasto.Location = New System.Drawing.Point(518, 12)
  Me.CmbGasto.Name = "CmbGasto"
  Me.CmbGasto.Size = New System.Drawing.Size(515, 21)
  Me.CmbGasto.TabIndex = 178
  Me.CmbGasto.Visible = False
  '
  'Label7
  '
  Me.Label7.AutoSize = True
  Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label7.Location = New System.Drawing.Point(14, 18)
  Me.Label7.Name = "Label7"
  Me.Label7.Size = New System.Drawing.Size(96, 15)
  Me.Label7.TabIndex = 177
  Me.Label7.Text = "Tipo de Pago:"
  '
  'RadioButton1
  '
  Me.RadioButton1.AutoSize = True
  Me.RadioButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.RadioButton1.Location = New System.Drawing.Point(256, 16)
  Me.RadioButton1.Name = "RadioButton1"
  Me.RadioButton1.Size = New System.Drawing.Size(135, 20)
  Me.RadioButton1.TabIndex = 176
  Me.RadioButton1.Text = "Gastos Generales"
  Me.RadioButton1.UseVisualStyleBackColor = True
  '
  'RdBCompras
  '
  Me.RdBCompras.AutoSize = True
  Me.RdBCompras.Checked = True
  Me.RdBCompras.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.RdBCompras.Location = New System.Drawing.Point(116, 17)
  Me.RdBCompras.Name = "RdBCompras"
  Me.RdBCompras.Size = New System.Drawing.Size(134, 20)
  Me.RdBCompras.TabIndex = 175
  Me.RdBCompras.TabStop = True
  Me.RdBCompras.Text = "Compras Directas"
  Me.RdBCompras.UseVisualStyleBackColor = True
  '
  'Button2
  '
  Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
  Me.Button2.Location = New System.Drawing.Point(908, 93)
  Me.Button2.Name = "Button2"
  Me.Button2.Size = New System.Drawing.Size(117, 42)
  Me.Button2.TabIndex = 174
  Me.Button2.Text = "Descuentos de proveedores"
  Me.Button2.UseVisualStyleBackColor = True
  Me.Button2.Visible = False
  '
  'ChkVisDisa
  '
  Me.ChkVisDisa.AutoSize = True
  Me.ChkVisDisa.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.ChkVisDisa.Location = New System.Drawing.Point(627, 68)
  Me.ChkVisDisa.Name = "ChkVisDisa"
  Me.ChkVisDisa.Size = New System.Drawing.Size(151, 21)
  Me.ChkVisDisa.TabIndex = 172
  Me.ChkVisDisa.Text = "Visualizar Disa y PJ"
  Me.ChkVisDisa.UseVisualStyleBackColor = True
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(61, 69)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(29, 15)
  Me.Label2.TabIndex = 171
  Me.Label2.Text = "Del"
  '
  'CmbAgteVta
  '
  Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbAgteVta.FormattingEnabled = True
  Me.CmbAgteVta.Location = New System.Drawing.Point(92, 101)
  Me.CmbAgteVta.Name = "CmbAgteVta"
  Me.CmbAgteVta.Size = New System.Drawing.Size(515, 21)
  Me.CmbAgteVta.TabIndex = 164
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(14, 102)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(74, 17)
  Me.Label1.TabIndex = 169
  Me.Label1.Text = "Proveedor"
  '
  'Label4
  '
  Me.Label4.AutoSize = True
  Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label4.Location = New System.Drawing.Point(771, 102)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(52, 17)
  Me.Label4.TabIndex = 167
  Me.Label4.Text = "Detalle"
  '
  'BtnDetalle
  '
  Me.BtnDetalle.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
  Me.BtnDetalle.Location = New System.Drawing.Point(729, 93)
  Me.BtnDetalle.Name = "BtnDetalle"
  Me.BtnDetalle.Size = New System.Drawing.Size(36, 34)
  Me.BtnDetalle.TabIndex = 166
  Me.BtnDetalle.UseVisualStyleBackColor = True
  '
  'Button1
  '
  Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
  Me.Button1.Location = New System.Drawing.Point(627, 96)
  Me.Button1.Name = "Button1"
  Me.Button1.Size = New System.Drawing.Size(75, 31)
  Me.Button1.TabIndex = 165
  Me.Button1.Text = "Consultar"
  Me.Button1.UseVisualStyleBackColor = True
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(338, 69)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(19, 15)
  Me.Label3.TabIndex = 170
  Me.Label3.Text = "Al"
  '
  'DtpFechaTer
  '
  Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DtpFechaTer.Location = New System.Drawing.Point(363, 64)
  Me.DtpFechaTer.Name = "DtpFechaTer"
  Me.DtpFechaTer.Size = New System.Drawing.Size(240, 23)
  Me.DtpFechaTer.TabIndex = 163
  Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
  '
  'Label5
  '
  Me.Label5.AutoSize = True
  Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label5.Location = New System.Drawing.Point(14, 40)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(114, 15)
  Me.Label5.TabIndex = 168
  Me.Label5.Text = "Periodo de Pago"
  '
  'DtpFechaIni
  '
  Me.DtpFechaIni.CustomFormat = "yyyy-MM-dd"
  Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DtpFechaIni.Location = New System.Drawing.Point(92, 64)
  Me.DtpFechaIni.Name = "DtpFechaIni"
  Me.DtpFechaIni.Size = New System.Drawing.Size(240, 23)
  Me.DtpFechaIni.TabIndex = 162
  Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
  '
  'Label6
  '
  Me.Label6.AutoSize = True
  Me.Label6.BackColor = System.Drawing.Color.White
  Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label6.ForeColor = System.Drawing.Color.Black
  Me.Label6.Location = New System.Drawing.Point(17, 130)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(168, 17)
  Me.Label6.TabIndex = 184
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
  Me.DgFactProv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FchDoc, Me.DiasCred, Me.Factura, Me.FactProv, Me.IdProved, Me.Proveedor, Me.NombreGasto, Me.TotFactura, Me.Moneda, Me.Pagado, Me.SaldoPendiente, Me.Moneda2, Me.DiasAtraso, Me.FchVen, Me.SaldoPesos, Me.Referencia, Me.Obrserv, Me.Comentarios})
  Me.DgFactProv.Location = New System.Drawing.Point(17, 152)
  Me.DgFactProv.Name = "DgFactProv"
  Me.DgFactProv.ReadOnly = True
  Me.DgFactProv.RowHeadersWidth = 25
  DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DgFactProv.RowsDefaultCellStyle = DataGridViewCellStyle2
  Me.DgFactProv.Size = New System.Drawing.Size(1710, 582)
  Me.DgFactProv.TabIndex = 183
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
  'TxtTotEnPesos
  '
  Me.TxtTotEnPesos.Anchor = System.Windows.Forms.AnchorStyles.Bottom
  Me.TxtTotEnPesos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.TxtTotEnPesos.Location = New System.Drawing.Point(1267, 737)
  Me.TxtTotEnPesos.Name = "TxtTotEnPesos"
  Me.TxtTotEnPesos.ReadOnly = True
  Me.TxtTotEnPesos.Size = New System.Drawing.Size(103, 21)
  Me.TxtTotEnPesos.TabIndex = 185
  Me.TxtTotEnPesos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'LblTotal
  '
  Me.LblTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom
  Me.LblTotal.AutoSize = True
  Me.LblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.LblTotal.Location = New System.Drawing.Point(1008, 737)
  Me.LblTotal.Name = "LblTotal"
  Me.LblTotal.Size = New System.Drawing.Size(257, 17)
  Me.LblTotal.TabIndex = 186
  Me.LblTotal.Text = "Total pagado en moneda nacional MXP"
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
  Me.DataGridViewTextBoxColumn3.HeaderText = "Doc. Sap"
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
  Me.DataGridViewTextBoxColumn4.Width = 75
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
  Me.DataGridViewTextBoxColumn6.Width = 236
  '
  'DataGridViewTextBoxColumn7
  '
  Me.DataGridViewTextBoxColumn7.DataPropertyName = "TipoGasto"
  Me.DataGridViewTextBoxColumn7.HeaderText = "Tipo de Gasto"
  Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
  Me.DataGridViewTextBoxColumn7.ReadOnly = True
  Me.DataGridViewTextBoxColumn7.Visible = False
  Me.DataGridViewTextBoxColumn7.Width = 170
  '
  'DataGridViewTextBoxColumn8
  '
  Me.DataGridViewTextBoxColumn8.DataPropertyName = "TotFactura"
  Me.DataGridViewTextBoxColumn8.HeaderText = "$ Total Factura"
  Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
  Me.DataGridViewTextBoxColumn8.ReadOnly = True
  Me.DataGridViewTextBoxColumn8.Width = 80
  '
  'DataGridViewTextBoxColumn9
  '
  Me.DataGridViewTextBoxColumn9.DataPropertyName = "Moneda"
  Me.DataGridViewTextBoxColumn9.HeaderText = "Mnd"
  Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
  Me.DataGridViewTextBoxColumn9.ReadOnly = True
  Me.DataGridViewTextBoxColumn9.Width = 35
  '
  'DataGridViewTextBoxColumn10
  '
  Me.DataGridViewTextBoxColumn10.DataPropertyName = "Pagado"
  Me.DataGridViewTextBoxColumn10.HeaderText = "$ Importe Aplicado"
  Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
  Me.DataGridViewTextBoxColumn10.ReadOnly = True
  Me.DataGridViewTextBoxColumn10.Width = 80
  '
  'DataGridViewTextBoxColumn11
  '
  Me.DataGridViewTextBoxColumn11.DataPropertyName = "SaldoPendiente"
  Me.DataGridViewTextBoxColumn11.HeaderText = "$ Saldo Pendiente"
  Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
  Me.DataGridViewTextBoxColumn11.ReadOnly = True
  Me.DataGridViewTextBoxColumn11.Width = 80
  '
  'DataGridViewTextBoxColumn12
  '
  Me.DataGridViewTextBoxColumn12.DataPropertyName = "Moneda2"
  Me.DataGridViewTextBoxColumn12.HeaderText = "Mnd"
  Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
  Me.DataGridViewTextBoxColumn12.ReadOnly = True
  Me.DataGridViewTextBoxColumn12.Width = 35
  '
  'DataGridViewTextBoxColumn13
  '
  Me.DataGridViewTextBoxColumn13.DataPropertyName = "DiasAtraso"
  Me.DataGridViewTextBoxColumn13.HeaderText = "Dias Atraso"
  Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
  Me.DataGridViewTextBoxColumn13.ReadOnly = True
  Me.DataGridViewTextBoxColumn13.Width = 40
  '
  'DataGridViewTextBoxColumn14
  '
  Me.DataGridViewTextBoxColumn14.DataPropertyName = "FchVen"
  Me.DataGridViewTextBoxColumn14.HeaderText = "Fch. de Vencimiento"
  Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
  Me.DataGridViewTextBoxColumn14.ReadOnly = True
  Me.DataGridViewTextBoxColumn14.Width = 70
  '
  'DataGridViewTextBoxColumn15
  '
  Me.DataGridViewTextBoxColumn15.DataPropertyName = "SaldoPesos"
  Me.DataGridViewTextBoxColumn15.HeaderText = "$ Saldo MXP"
  Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
  Me.DataGridViewTextBoxColumn15.ReadOnly = True
  Me.DataGridViewTextBoxColumn15.Width = 101
  '
  'DataGridViewTextBoxColumn16
  '
  Me.DataGridViewTextBoxColumn16.DataPropertyName = "Referencia"
  Me.DataGridViewTextBoxColumn16.HeaderText = "Referencia"
  Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
  Me.DataGridViewTextBoxColumn16.ReadOnly = True
  '
  'DataGridViewTextBoxColumn17
  '
  Me.DataGridViewTextBoxColumn17.DataPropertyName = "Obrserv"
  Me.DataGridViewTextBoxColumn17.HeaderText = "Observaciones"
  Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
  Me.DataGridViewTextBoxColumn17.ReadOnly = True
  Me.DataGridViewTextBoxColumn17.Width = 184
  '
  'DataGridViewTextBoxColumn18
  '
  Me.DataGridViewTextBoxColumn18.DataPropertyName = "Coment"
  Me.DataGridViewTextBoxColumn18.HeaderText = "Comentarios"
  Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
  Me.DataGridViewTextBoxColumn18.ReadOnly = True
  '
  'PagosRealizados
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1464, 759)
  Me.Controls.Add(Me.TxtTotEnPesos)
  Me.Controls.Add(Me.LblTotal)
  Me.Controls.Add(Me.Label6)
  Me.Controls.Add(Me.DgFactProv)
  Me.Controls.Add(Me.ChkUSD)
  Me.Controls.Add(Me.Label8)
  Me.Controls.Add(Me.CmbProveedor)
  Me.Controls.Add(Me.CmbGasto)
  Me.Controls.Add(Me.Label7)
  Me.Controls.Add(Me.RadioButton1)
  Me.Controls.Add(Me.RdBCompras)
  Me.Controls.Add(Me.Button2)
  Me.Controls.Add(Me.ChkVisDisa)
  Me.Controls.Add(Me.Label2)
  Me.Controls.Add(Me.CmbAgteVta)
  Me.Controls.Add(Me.Label1)
  Me.Controls.Add(Me.Label4)
  Me.Controls.Add(Me.BtnDetalle)
  Me.Controls.Add(Me.Button1)
  Me.Controls.Add(Me.Label3)
  Me.Controls.Add(Me.DtpFechaTer)
  Me.Controls.Add(Me.Label5)
  Me.Controls.Add(Me.DtpFechaIni)
  Me.Name = "PagosRealizados"
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Pagos Realizados"
  Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
  CType(Me.DgFactProv, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub
 Friend WithEvents ChkUSD As CheckBox
 Friend WithEvents Label8 As Label
 Friend WithEvents CmbProveedor As ComboBox
 Friend WithEvents CmbGasto As ComboBox
 Friend WithEvents Label7 As Label
 Friend WithEvents RadioButton1 As RadioButton
 Friend WithEvents RdBCompras As RadioButton
 Friend WithEvents Button2 As Button
 Friend WithEvents ChkVisDisa As CheckBox
 Friend WithEvents Label2 As Label
 Friend WithEvents CmbAgteVta As ComboBox
 Friend WithEvents Label1 As Label
 Friend WithEvents Label4 As Label
 Friend WithEvents BtnDetalle As Button
 Friend WithEvents Button1 As Button
 Friend WithEvents Label3 As Label
 Friend WithEvents DtpFechaTer As DateTimePicker
 Friend WithEvents Label5 As Label
 Friend WithEvents DtpFechaIni As DateTimePicker
 Friend WithEvents Label6 As Label
 Friend WithEvents DgFactProv As DataGridView
 Friend WithEvents TxtTotEnPesos As TextBox
 Friend WithEvents LblTotal As Label
 Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn12 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn13 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn14 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn15 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn16 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn17 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn18 As DataGridViewTextBoxColumn
 Friend WithEvents FchDoc As DataGridViewTextBoxColumn
 Friend WithEvents DiasCred As DataGridViewTextBoxColumn
 Friend WithEvents Factura As DataGridViewTextBoxColumn
 Friend WithEvents FactProv As DataGridViewTextBoxColumn
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
End Class
