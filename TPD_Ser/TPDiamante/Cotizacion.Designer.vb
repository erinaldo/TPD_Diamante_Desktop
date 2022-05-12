<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cotizacion
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
  Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Cotizacion))
  Me.TBOtro = New System.Windows.Forms.TextBox()
  Me.Label10 = New System.Windows.Forms.Label()
  Me.BtnEmail = New System.Windows.Forms.Button()
  Me.BtnGuardar = New System.Windows.Forms.Button()
  Me.LblError = New System.Windows.Forms.Label()
  Me.LblMensaje = New System.Windows.Forms.Label()
  Me.BtnNvo = New System.Windows.Forms.Button()
  Me.TxtComentario = New System.Windows.Forms.TextBox()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.BtnMenos = New System.Windows.Forms.Button()
  Me.BtnMas = New System.Windows.Forms.Button()
  Me.DGVCap = New System.Windows.Forms.DataGridView()
  Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.Articulo = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.ListaP = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.Precio = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.Descuento = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.Importe = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.Expand = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.Linea = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.Linea2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.CmbLinea = New System.Windows.Forms.ComboBox()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.TxtDes = New System.Windows.Forms.TextBox()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.DGVArt = New System.Windows.Forms.DataGridView()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.TxtArticulo = New System.Windows.Forms.TextBox()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.CmbCliente = New System.Windows.Forms.ComboBox()
  Me.BtnImprimir = New System.Windows.Forms.Button()
  Me.LblExito = New System.Windows.Forms.Label()
  Me.TxtCorreoAd = New System.Windows.Forms.TextBox()
  Me.TxtCorreoC = New System.Windows.Forms.TextBox()
  Me.CkBCliente = New System.Windows.Forms.CheckBox()
  Me.CkBOtro = New System.Windows.Forms.CheckBox()
  Me.ComboBox1 = New System.Windows.Forms.ComboBox()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.txttipo_cambio = New System.Windows.Forms.TextBox()
  Me.Label7 = New System.Windows.Forms.Label()
  Me.cbDolares = New System.Windows.Forms.CheckBox()
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
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.Panel2 = New System.Windows.Forms.Panel()
  Me.Splitter1 = New System.Windows.Forms.Splitter()
  Me.Panel3 = New System.Windows.Forms.Panel()
  Me.Splitter2 = New System.Windows.Forms.Splitter()
  Me.Panel4 = New System.Windows.Forms.Panel()
  CType(Me.DGVCap, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.DGVArt, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.Panel1.SuspendLayout()
  Me.Panel2.SuspendLayout()
  Me.Panel3.SuspendLayout()
  Me.Panel4.SuspendLayout()
  Me.SuspendLayout()
  '
  'TBOtro
  '
  Me.TBOtro.Location = New System.Drawing.Point(91, 20)
  Me.TBOtro.Name = "TBOtro"
  Me.TBOtro.Size = New System.Drawing.Size(500, 20)
  Me.TBOtro.TabIndex = 148
  '
  'Label10
  '
  Me.Label10.AutoSize = True
  Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label10.Location = New System.Drawing.Point(827, 145)
  Me.Label10.Name = "Label10"
  Me.Label10.Size = New System.Drawing.Size(61, 15)
  Me.Label10.TabIndex = 145
  Me.Label10.Text = "EMail(s)"
  '
  'BtnEmail
  '
  Me.BtnEmail.BackColor = System.Drawing.SystemColors.Control
  Me.BtnEmail.Enabled = False
  Me.BtnEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnEmail.ForeColor = System.Drawing.Color.Red
  Me.BtnEmail.Location = New System.Drawing.Point(756, 137)
  Me.BtnEmail.Name = "BtnEmail"
  Me.BtnEmail.Size = New System.Drawing.Size(65, 37)
  Me.BtnEmail.TabIndex = 139
  Me.BtnEmail.Text = "Enviar EMails"
  Me.BtnEmail.UseVisualStyleBackColor = False
  '
  'BtnGuardar
  '
  Me.BtnGuardar.BackColor = System.Drawing.Color.LightBlue
  Me.BtnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnGuardar.Location = New System.Drawing.Point(683, 137)
  Me.BtnGuardar.Name = "BtnGuardar"
  Me.BtnGuardar.Size = New System.Drawing.Size(64, 37)
  Me.BtnGuardar.TabIndex = 137
  Me.BtnGuardar.Text = "Guardar y Enviar "
  Me.BtnGuardar.UseVisualStyleBackColor = False
  '
  'LblError
  '
  Me.LblError.AutoSize = True
  Me.LblError.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.LblError.ForeColor = System.Drawing.Color.Red
  Me.LblError.Location = New System.Drawing.Point(681, 116)
  Me.LblError.Name = "LblError"
  Me.LblError.Size = New System.Drawing.Size(337, 15)
  Me.LblError.TabIndex = 143
  Me.LblError.Text = "No fue posible enviar EMails. Intentelo nuevamente"
  Me.LblError.Visible = False
  '
  'LblMensaje
  '
  Me.LblMensaje.AutoSize = True
  Me.LblMensaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.LblMensaje.ForeColor = System.Drawing.SystemColors.HotTrack
  Me.LblMensaje.Location = New System.Drawing.Point(678, 116)
  Me.LblMensaje.Name = "LblMensaje"
  Me.LblMensaje.Size = New System.Drawing.Size(380, 15)
  Me.LblMensaje.TabIndex = 142
  Me.LblMensaje.Text = "Creando Cotizacion y Enviando Correo Electronico. . . . . . ."
  Me.LblMensaje.Visible = False
  '
  'BtnNvo
  '
  Me.BtnNvo.BackColor = System.Drawing.SystemColors.Control
  Me.BtnNvo.Enabled = False
  Me.BtnNvo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnNvo.Location = New System.Drawing.Point(4, 231)
  Me.BtnNvo.Name = "BtnNvo"
  Me.BtnNvo.Size = New System.Drawing.Size(38, 43)
  Me.BtnNvo.TabIndex = 141
  Me.BtnNvo.Text = "&Nvo"
  Me.BtnNvo.UseVisualStyleBackColor = False
  '
  'TxtComentario
  '
  Me.TxtComentario.Location = New System.Drawing.Point(937, 8)
  Me.TxtComentario.MaxLength = 275
  Me.TxtComentario.Multiline = True
  Me.TxtComentario.Name = "TxtComentario"
  Me.TxtComentario.Size = New System.Drawing.Size(277, 78)
  Me.TxtComentario.TabIndex = 124
  '
  'Label8
  '
  Me.Label8.AutoSize = True
  Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label8.Location = New System.Drawing.Point(837, 22)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(81, 15)
  Me.Label8.TabIndex = 140
  Me.Label8.Text = "Comentario"
  '
  'BtnMenos
  '
  Me.BtnMenos.BackColor = System.Drawing.SystemColors.Control
  Me.BtnMenos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnMenos.Image = Global.TPDiamante.My.Resources.Resources.Minus__6_
  Me.BtnMenos.Location = New System.Drawing.Point(4, 148)
  Me.BtnMenos.Name = "BtnMenos"
  Me.BtnMenos.Size = New System.Drawing.Size(38, 43)
  Me.BtnMenos.TabIndex = 132
  Me.BtnMenos.UseVisualStyleBackColor = False
  '
  'BtnMas
  '
  Me.BtnMas.BackColor = System.Drawing.SystemColors.Control
  Me.BtnMas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnMas.Image = Global.TPDiamante.My.Resources.Resources.Plus__6_
  Me.BtnMas.Location = New System.Drawing.Point(4, 64)
  Me.BtnMas.Name = "BtnMas"
  Me.BtnMas.Size = New System.Drawing.Size(38, 43)
  Me.BtnMas.TabIndex = 129
  Me.BtnMas.UseVisualStyleBackColor = False
  '
  'DGVCap
  '
  Me.DGVCap.AllowUserToAddRows = False
  Me.DGVCap.AllowUserToDeleteRows = False
  DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
  DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
  DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
  DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
  DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
  DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
  Me.DGVCap.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
  Me.DGVCap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.DGVCap.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Cantidad, Me.Articulo, Me.Descripcion, Me.ListaP, Me.Precio, Me.Descuento, Me.Importe, Me.Price, Me.Expand, Me.Linea, Me.Linea2})
  DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
  DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
  DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
  DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
  DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
  DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
  DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
  Me.DGVCap.DefaultCellStyle = DataGridViewCellStyle3
  Me.DGVCap.Dock = System.Windows.Forms.DockStyle.Fill
  Me.DGVCap.Location = New System.Drawing.Point(0, 0)
  Me.DGVCap.Name = "DGVCap"
  Me.DGVCap.RowHeadersVisible = False
  Me.DGVCap.RowTemplate.Height = 21
  Me.DGVCap.Size = New System.Drawing.Size(646, 427)
  Me.DGVCap.TabIndex = 131
  '
  'Cantidad
  '
  DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  DataGridViewCellStyle2.Format = "N0"
  DataGridViewCellStyle2.NullValue = Nothing
  Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle2
  Me.Cantidad.HeaderText = "Cantidad"
  Me.Cantidad.Name = "Cantidad"
  '
  'Articulo
  '
  Me.Articulo.HeaderText = "Artículo"
  Me.Articulo.Name = "Articulo"
  Me.Articulo.ReadOnly = True
  '
  'Descripcion
  '
  Me.Descripcion.HeaderText = "Descripción"
  Me.Descripcion.Name = "Descripcion"
  Me.Descripcion.ReadOnly = True
  '
  'ListaP
  '
  Me.ListaP.HeaderText = "Lista"
  Me.ListaP.Name = "ListaP"
  '
  'Precio
  '
  Me.Precio.HeaderText = "Precio"
  Me.Precio.Name = "Precio"
  '
  'Descuento
  '
  Me.Descuento.HeaderText = "Desc. Prom."
  Me.Descuento.Name = "Descuento"
  '
  'Importe
  '
  Me.Importe.HeaderText = "Importe"
  Me.Importe.Name = "Importe"
  Me.Importe.ReadOnly = True
  '
  'Price
  '
  Me.Price.HeaderText = "Price"
  Me.Price.Name = "Price"
  Me.Price.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
  '
  'Expand
  '
  Me.Expand.HeaderText = "Expand"
  Me.Expand.Name = "Expand"
  Me.Expand.Visible = False
  '
  'Linea
  '
  Me.Linea.HeaderText = "Linea"
  Me.Linea.Name = "Linea"
  Me.Linea.Visible = False
  '
  'Linea2
  '
  Me.Linea2.HeaderText = "Linea2"
  Me.Linea2.Name = "Linea2"
  Me.Linea2.Visible = False
  '
  'CmbLinea
  '
  Me.CmbLinea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbLinea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbLinea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.CmbLinea.FormattingEnabled = True
  Me.CmbLinea.Location = New System.Drawing.Point(476, 153)
  Me.CmbLinea.Name = "CmbLinea"
  Me.CmbLinea.Size = New System.Drawing.Size(152, 21)
  Me.CmbLinea.TabIndex = 127
  '
  'Label6
  '
  Me.Label6.AutoSize = True
  Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label6.Location = New System.Drawing.Point(170, 138)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(74, 13)
  Me.Label6.TabIndex = 134
  Me.Label6.Text = "Descripción"
  '
  'TxtDes
  '
  Me.TxtDes.Location = New System.Drawing.Point(173, 155)
  Me.TxtDes.Name = "TxtDes"
  Me.TxtDes.Size = New System.Drawing.Size(285, 20)
  Me.TxtDes.TabIndex = 126
  '
  'Label5
  '
  Me.Label5.AutoSize = True
  Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label5.Location = New System.Drawing.Point(473, 137)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(38, 13)
  Me.Label5.TabIndex = 135
  Me.Label5.Text = "Linea"
  '
  'DGVArt
  '
  Me.DGVArt.AllowUserToAddRows = False
  Me.DGVArt.AllowUserToDeleteRows = False
  Me.DGVArt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
  DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
  DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
  DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
  DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
  DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
  DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
  Me.DGVArt.DefaultCellStyle = DataGridViewCellStyle4
  Me.DGVArt.Dock = System.Windows.Forms.DockStyle.Fill
  Me.DGVArt.Location = New System.Drawing.Point(0, 0)
  Me.DGVArt.Name = "DGVArt"
  Me.DGVArt.ReadOnly = True
  Me.DGVArt.RowTemplate.Height = 21
  Me.DGVArt.Size = New System.Drawing.Size(732, 423)
  Me.DGVArt.TabIndex = 128
  '
  'Label4
  '
  Me.Label4.AutoSize = True
  Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label4.Location = New System.Drawing.Point(6, 138)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(52, 13)
  Me.Label4.TabIndex = 133
  Me.Label4.Text = "Artículo"
  '
  'TxtArticulo
  '
  Me.TxtArticulo.Location = New System.Drawing.Point(8, 155)
  Me.TxtArticulo.Name = "TxtArticulo"
  Me.TxtArticulo.Size = New System.Drawing.Size(151, 20)
  Me.TxtArticulo.TabIndex = 125
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(4, 120)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(72, 13)
  Me.Label3.TabIndex = 138
  Me.Label3.Text = "Buscar por:"
  '
  'CmbCliente
  '
  Me.CmbCliente.DropDownHeight = 90
  Me.CmbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.CmbCliente.FormattingEnabled = True
  Me.CmbCliente.IntegralHeight = False
  Me.CmbCliente.ItemHeight = 13
  Me.CmbCliente.Location = New System.Drawing.Point(87, 19)
  Me.CmbCliente.Name = "CmbCliente"
  Me.CmbCliente.Size = New System.Drawing.Size(543, 21)
  Me.CmbCliente.TabIndex = 123
  '
  'BtnImprimir
  '
  Me.BtnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnImprimir.ForeColor = System.Drawing.Color.MediumBlue
  Me.BtnImprimir.Image = Global.TPDiamante.My.Resources.Resources.printer
  Me.BtnImprimir.Location = New System.Drawing.Point(4, 319)
  Me.BtnImprimir.Name = "BtnImprimir"
  Me.BtnImprimir.Size = New System.Drawing.Size(38, 43)
  Me.BtnImprimir.TabIndex = 149
  Me.BtnImprimir.Text = "            "
  Me.BtnImprimir.UseVisualStyleBackColor = True
  '
  'LblExito
  '
  Me.LblExito.AutoSize = True
  Me.LblExito.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.LblExito.ForeColor = System.Drawing.Color.Green
  Me.LblExito.Location = New System.Drawing.Point(678, 101)
  Me.LblExito.Name = "LblExito"
  Me.LblExito.Size = New System.Drawing.Size(288, 15)
  Me.LblExito.TabIndex = 136
  Me.LblExito.Text = "Cotizacion Creada y Enviada Exitosamente !"
  Me.LblExito.Visible = False
  '
  'TxtCorreoAd
  '
  Me.TxtCorreoAd.Location = New System.Drawing.Point(891, 156)
  Me.TxtCorreoAd.Name = "TxtCorreoAd"
  Me.TxtCorreoAd.Size = New System.Drawing.Size(323, 20)
  Me.TxtCorreoAd.TabIndex = 151
  '
  'TxtCorreoC
  '
  Me.TxtCorreoC.Location = New System.Drawing.Point(891, 134)
  Me.TxtCorreoC.Name = "TxtCorreoC"
  Me.TxtCorreoC.Size = New System.Drawing.Size(323, 20)
  Me.TxtCorreoC.TabIndex = 150
  '
  'CkBCliente
  '
  Me.CkBCliente.AutoSize = True
  Me.CkBCliente.Location = New System.Drawing.Point(12, 10)
  Me.CkBCliente.Name = "CkBCliente"
  Me.CkBCliente.Size = New System.Drawing.Size(58, 17)
  Me.CkBCliente.TabIndex = 152
  Me.CkBCliente.Text = "Cliente"
  Me.CkBCliente.UseVisualStyleBackColor = True
  '
  'CkBOtro
  '
  Me.CkBOtro.AutoSize = True
  Me.CkBOtro.Location = New System.Drawing.Point(12, 33)
  Me.CkBOtro.Name = "CkBOtro"
  Me.CkBOtro.Size = New System.Drawing.Size(46, 17)
  Me.CkBOtro.TabIndex = 153
  Me.CkBOtro.Text = "Otro"
  Me.CkBOtro.UseVisualStyleBackColor = True
  '
  'ComboBox1
  '
  Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
  Me.ComboBox1.FormattingEnabled = True
  Me.ComboBox1.Location = New System.Drawing.Point(40, 70)
  Me.ComboBox1.Name = "ComboBox1"
  Me.ComboBox1.Size = New System.Drawing.Size(44, 21)
  Me.ComboBox1.TabIndex = 154
  Me.ComboBox1.Visible = False
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Location = New System.Drawing.Point(9, 73)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(23, 13)
  Me.Label1.TabIndex = 155
  Me.Label1.Text = "LP:"
  Me.Label1.Visible = False
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Location = New System.Drawing.Point(127, 63)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(110, 13)
  Me.Label2.TabIndex = 156
  Me.Label2.Text = "Tipo de cambio al día"
  Me.Label2.Visible = False
  '
  'txttipo_cambio
  '
  Me.txttipo_cambio.Enabled = False
  Me.txttipo_cambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txttipo_cambio.ForeColor = System.Drawing.Color.Blue
  Me.txttipo_cambio.Location = New System.Drawing.Point(241, 75)
  Me.txttipo_cambio.Name = "txttipo_cambio"
  Me.txttipo_cambio.Size = New System.Drawing.Size(100, 20)
  Me.txttipo_cambio.TabIndex = 157
  Me.txttipo_cambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'Label7
  '
  Me.Label7.AutoSize = True
  Me.Label7.Location = New System.Drawing.Point(127, 81)
  Me.Label7.Name = "Label7"
  Me.Label7.Size = New System.Drawing.Size(111, 13)
  Me.Label7.TabIndex = 158
  Me.Label7.Text = "en linea GOODYEAR:"
  Me.Label7.Visible = False
  '
  'cbDolares
  '
  Me.cbDolares.AutoSize = True
  Me.cbDolares.Location = New System.Drawing.Point(395, 80)
  Me.cbDolares.Name = "cbDolares"
  Me.cbDolares.Size = New System.Drawing.Size(112, 17)
  Me.cbDolares.TabIndex = 159
  Me.cbDolares.Text = "Cotizar en Dolares"
  Me.cbDolares.UseVisualStyleBackColor = True
  Me.cbDolares.Visible = False
  '
  'DataGridViewTextBoxColumn1
  '
  DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  DataGridViewCellStyle5.Format = "N0"
  DataGridViewCellStyle5.NullValue = Nothing
  Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle5
  Me.DataGridViewTextBoxColumn1.HeaderText = "Cantidad"
  Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
  '
  'DataGridViewTextBoxColumn2
  '
  Me.DataGridViewTextBoxColumn2.HeaderText = "Artículo"
  Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
  Me.DataGridViewTextBoxColumn2.ReadOnly = True
  '
  'DataGridViewTextBoxColumn3
  '
  Me.DataGridViewTextBoxColumn3.HeaderText = "Descripción"
  Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
  Me.DataGridViewTextBoxColumn3.ReadOnly = True
  '
  'DataGridViewTextBoxColumn4
  '
  Me.DataGridViewTextBoxColumn4.HeaderText = "Lista"
  Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
  '
  'DataGridViewTextBoxColumn5
  '
  Me.DataGridViewTextBoxColumn5.HeaderText = "Precio"
  Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
  '
  'DataGridViewTextBoxColumn6
  '
  Me.DataGridViewTextBoxColumn6.HeaderText = "Desc. Prom."
  Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
  '
  'DataGridViewTextBoxColumn7
  '
  Me.DataGridViewTextBoxColumn7.HeaderText = "Importe"
  Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
  Me.DataGridViewTextBoxColumn7.ReadOnly = True
  '
  'DataGridViewTextBoxColumn8
  '
  Me.DataGridViewTextBoxColumn8.HeaderText = "Price"
  Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
  Me.DataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
  '
  'DataGridViewTextBoxColumn9
  '
  Me.DataGridViewTextBoxColumn9.HeaderText = "Expand"
  Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
  Me.DataGridViewTextBoxColumn9.Visible = False
  '
  'DataGridViewTextBoxColumn10
  '
  Me.DataGridViewTextBoxColumn10.HeaderText = "Linea"
  Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
  Me.DataGridViewTextBoxColumn10.Visible = False
  '
  'DataGridViewTextBoxColumn11
  '
  Me.DataGridViewTextBoxColumn11.HeaderText = "Linea2"
  Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
  Me.DataGridViewTextBoxColumn11.Visible = False
  '
  'Panel1
  '
  Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel1.Controls.Add(Me.CkBCliente)
  Me.Panel1.Controls.Add(Me.cbDolares)
  Me.Panel1.Controls.Add(Me.CmbCliente)
  Me.Panel1.Controls.Add(Me.Label7)
  Me.Panel1.Controls.Add(Me.Label3)
  Me.Panel1.Controls.Add(Me.txttipo_cambio)
  Me.Panel1.Controls.Add(Me.TxtArticulo)
  Me.Panel1.Controls.Add(Me.Label2)
  Me.Panel1.Controls.Add(Me.Label4)
  Me.Panel1.Controls.Add(Me.Label1)
  Me.Panel1.Controls.Add(Me.Label5)
  Me.Panel1.Controls.Add(Me.ComboBox1)
  Me.Panel1.Controls.Add(Me.TxtDes)
  Me.Panel1.Controls.Add(Me.CkBOtro)
  Me.Panel1.Controls.Add(Me.Label6)
  Me.Panel1.Controls.Add(Me.CmbLinea)
  Me.Panel1.Controls.Add(Me.TxtCorreoAd)
  Me.Panel1.Controls.Add(Me.Label8)
  Me.Panel1.Controls.Add(Me.TxtCorreoC)
  Me.Panel1.Controls.Add(Me.TxtComentario)
  Me.Panel1.Controls.Add(Me.LblMensaje)
  Me.Panel1.Controls.Add(Me.TBOtro)
  Me.Panel1.Controls.Add(Me.LblError)
  Me.Panel1.Controls.Add(Me.Label10)
  Me.Panel1.Controls.Add(Me.LblExito)
  Me.Panel1.Controls.Add(Me.BtnEmail)
  Me.Panel1.Controls.Add(Me.BtnGuardar)
  Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
  Me.Panel1.Location = New System.Drawing.Point(0, 0)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(1435, 184)
  Me.Panel1.TabIndex = 160
  '
  'Panel2
  '
  Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel2.Controls.Add(Me.DGVArt)
  Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
  Me.Panel2.Location = New System.Drawing.Point(0, 184)
  Me.Panel2.Name = "Panel2"
  Me.Panel2.Size = New System.Drawing.Size(736, 427)
  Me.Panel2.TabIndex = 161
  '
  'Splitter1
  '
  Me.Splitter1.Location = New System.Drawing.Point(736, 184)
  Me.Splitter1.Name = "Splitter1"
  Me.Splitter1.Size = New System.Drawing.Size(3, 427)
  Me.Splitter1.TabIndex = 162
  Me.Splitter1.TabStop = False
  '
  'Panel3
  '
  Me.Panel3.Controls.Add(Me.BtnNvo)
  Me.Panel3.Controls.Add(Me.BtnImprimir)
  Me.Panel3.Controls.Add(Me.BtnMenos)
  Me.Panel3.Controls.Add(Me.BtnMas)
  Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
  Me.Panel3.Location = New System.Drawing.Point(739, 184)
  Me.Panel3.Name = "Panel3"
  Me.Panel3.Size = New System.Drawing.Size(47, 427)
  Me.Panel3.TabIndex = 163
  '
  'Splitter2
  '
  Me.Splitter2.Location = New System.Drawing.Point(786, 184)
  Me.Splitter2.Name = "Splitter2"
  Me.Splitter2.Size = New System.Drawing.Size(3, 427)
  Me.Splitter2.TabIndex = 164
  Me.Splitter2.TabStop = False
  '
  'Panel4
  '
  Me.Panel4.Controls.Add(Me.DGVCap)
  Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Panel4.Location = New System.Drawing.Point(789, 184)
  Me.Panel4.Name = "Panel4"
  Me.Panel4.Size = New System.Drawing.Size(646, 427)
  Me.Panel4.TabIndex = 165
  '
  'Cotizacion
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1435, 611)
  Me.Controls.Add(Me.Panel4)
  Me.Controls.Add(Me.Splitter2)
  Me.Controls.Add(Me.Panel3)
  Me.Controls.Add(Me.Splitter1)
  Me.Controls.Add(Me.Panel2)
  Me.Controls.Add(Me.Panel1)
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Name = "Cotizacion"
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Cotizacion"
  CType(Me.DGVCap, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.DGVArt, System.ComponentModel.ISupportInitialize).EndInit()
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  Me.Panel2.ResumeLayout(False)
  Me.Panel3.ResumeLayout(False)
  Me.Panel4.ResumeLayout(False)
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents TBOtro As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents BtnEmail As System.Windows.Forms.Button
    Friend WithEvents BtnGuardar As System.Windows.Forms.Button
    Friend WithEvents LblError As System.Windows.Forms.Label
    Friend WithEvents LblMensaje As System.Windows.Forms.Label
    Friend WithEvents BtnNvo As System.Windows.Forms.Button
    Friend WithEvents TxtComentario As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents BtnMenos As System.Windows.Forms.Button
    Friend WithEvents BtnMas As System.Windows.Forms.Button
    Friend WithEvents DGVCap As System.Windows.Forms.DataGridView
    Friend WithEvents CmbLinea As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtDes As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DGVArt As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtArticulo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents BtnImprimir As System.Windows.Forms.Button
    Friend WithEvents LblExito As System.Windows.Forms.Label
    Friend WithEvents TxtCorreoAd As System.Windows.Forms.TextBox
    Friend WithEvents TxtCorreoC As System.Windows.Forms.TextBox
    Friend WithEvents CkBCliente As System.Windows.Forms.CheckBox
    Friend WithEvents CkBOtro As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txttipo_cambio As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbDolares As CheckBox
    Friend WithEvents Cantidad As DataGridViewTextBoxColumn
    Friend WithEvents Articulo As DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As DataGridViewTextBoxColumn
    Friend WithEvents ListaP As DataGridViewTextBoxColumn
    Friend WithEvents Precio As DataGridViewTextBoxColumn
    Friend WithEvents Descuento As DataGridViewTextBoxColumn
    Friend WithEvents Importe As DataGridViewTextBoxColumn
    Friend WithEvents Price As DataGridViewTextBoxColumn
    Friend WithEvents Expand As DataGridViewTextBoxColumn
    Friend WithEvents Linea As DataGridViewTextBoxColumn
    Friend WithEvents Linea2 As DataGridViewTextBoxColumn
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
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Splitter2 As Splitter
    Friend WithEvents Panel4 As Panel
End Class
