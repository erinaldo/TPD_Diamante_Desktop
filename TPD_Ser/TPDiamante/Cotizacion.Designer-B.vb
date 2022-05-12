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
        CType(Me.DGVCap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TBOtro
        '
        Me.TBOtro.Location = New System.Drawing.Point(97, 22)
        Me.TBOtro.Name = "TBOtro"
        Me.TBOtro.Size = New System.Drawing.Size(304, 20)
        Me.TBOtro.TabIndex = 148
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(735, 148)
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
        Me.BtnEmail.Location = New System.Drawing.Point(664, 140)
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
        Me.BtnGuardar.Location = New System.Drawing.Point(591, 140)
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
        Me.LblError.Location = New System.Drawing.Point(589, 119)
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
        Me.LblMensaje.Location = New System.Drawing.Point(586, 119)
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
        Me.BtnNvo.Location = New System.Drawing.Point(553, 350)
        Me.BtnNvo.Name = "BtnNvo"
        Me.BtnNvo.Size = New System.Drawing.Size(38, 43)
        Me.BtnNvo.TabIndex = 141
        Me.BtnNvo.Text = "&Nvo"
        Me.BtnNvo.UseVisualStyleBackColor = False
        '
        'TxtComentario
        '
        Me.TxtComentario.Location = New System.Drawing.Point(591, 9)
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
        Me.Label8.Location = New System.Drawing.Point(491, 23)
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
        Me.BtnMenos.Location = New System.Drawing.Point(553, 267)
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
        Me.BtnMas.Location = New System.Drawing.Point(553, 183)
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
        Me.DGVCap.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Cantidad, Me.Articulo, Me.Descripcion, Me.Precio, Me.Descuento, Me.Importe, Me.Price, Me.Expand, Me.Linea, Me.Linea2})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVCap.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVCap.Location = New System.Drawing.Point(591, 183)
        Me.DGVCap.Name = "DGVCap"
        Me.DGVCap.RowHeadersVisible = False
        Me.DGVCap.RowTemplate.Height = 21
        Me.DGVCap.Size = New System.Drawing.Size(463, 412)
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
        'Precio
        '
        Me.Precio.HeaderText = "Precio"
        Me.Precio.Name = "Precio"
        Me.Precio.ReadOnly = True
        '
        'Descuento
        '
        Me.Descuento.HeaderText = "Desc. Prom."
        Me.Descuento.Name = "Descuento"
        Me.Descuento.ReadOnly = True
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
        Me.CmbLinea.Location = New System.Drawing.Point(401, 139)
        Me.CmbLinea.Name = "CmbLinea"
        Me.CmbLinea.Size = New System.Drawing.Size(152, 21)
        Me.CmbLinea.TabIndex = 127
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(176, 124)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 134
        Me.Label6.Text = "Descripción"
        '
        'TxtDes
        '
        Me.TxtDes.Location = New System.Drawing.Point(179, 141)
        Me.TxtDes.Name = "TxtDes"
        Me.TxtDes.Size = New System.Drawing.Size(209, 20)
        Me.TxtDes.TabIndex = 126
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(398, 123)
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
        Me.DGVArt.Location = New System.Drawing.Point(14, 183)
        Me.DGVArt.Name = "DGVArt"
        Me.DGVArt.ReadOnly = True
        Me.DGVArt.RowTemplate.Height = 21
        Me.DGVArt.Size = New System.Drawing.Size(539, 412)
        Me.DGVArt.TabIndex = 128
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 133
        Me.Label4.Text = "Artículo"
        '
        'TxtArticulo
        '
        Me.TxtArticulo.Location = New System.Drawing.Point(14, 141)
        Me.TxtArticulo.Name = "TxtArticulo"
        Me.TxtArticulo.Size = New System.Drawing.Size(151, 20)
        Me.TxtArticulo.TabIndex = 125
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 106)
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
        Me.CmbCliente.Location = New System.Drawing.Point(96, 22)
        Me.CmbCliente.Name = "CmbCliente"
        Me.CmbCliente.Size = New System.Drawing.Size(342, 21)
        Me.CmbCliente.TabIndex = 123
        '
        'BtnImprimir
        '
        Me.BtnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprimir.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnImprimir.Image = Global.TPDiamante.My.Resources.Resources.printer
        Me.BtnImprimir.Location = New System.Drawing.Point(553, 438)
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
        Me.LblExito.Location = New System.Drawing.Point(586, 104)
        Me.LblExito.Name = "LblExito"
        Me.LblExito.Size = New System.Drawing.Size(288, 15)
        Me.LblExito.TabIndex = 136
        Me.LblExito.Text = "Cotizacion Creada y Enviada Exitosamente !"
        Me.LblExito.Visible = False
        '
        'TxtCorreoAd
        '
        Me.TxtCorreoAd.Location = New System.Drawing.Point(799, 159)
        Me.TxtCorreoAd.Name = "TxtCorreoAd"
        Me.TxtCorreoAd.Size = New System.Drawing.Size(256, 20)
        Me.TxtCorreoAd.TabIndex = 151
        '
        'TxtCorreoC
        '
        Me.TxtCorreoC.Location = New System.Drawing.Point(799, 137)
        Me.TxtCorreoC.Name = "TxtCorreoC"
        Me.TxtCorreoC.Size = New System.Drawing.Size(255, 20)
        Me.TxtCorreoC.TabIndex = 150
        '
        'CkBCliente
        '
        Me.CkBCliente.AutoSize = True
        Me.CkBCliente.Location = New System.Drawing.Point(18, 12)
        Me.CkBCliente.Name = "CkBCliente"
        Me.CkBCliente.Size = New System.Drawing.Size(58, 17)
        Me.CkBCliente.TabIndex = 152
        Me.CkBCliente.Text = "Cliente"
        Me.CkBCliente.UseVisualStyleBackColor = True
        '
        'CkBOtro
        '
        Me.CkBOtro.AutoSize = True
        Me.CkBOtro.Location = New System.Drawing.Point(18, 35)
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
        Me.ComboBox1.Location = New System.Drawing.Point(46, 72)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(44, 21)
        Me.ComboBox1.TabIndex = 154
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 155
        Me.Label1.Text = "LP:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(180, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 13)
        Me.Label2.TabIndex = 156
        Me.Label2.Text = "Tipo de cambio al día"
        '
        'txttipo_cambio
        '
        Me.txttipo_cambio.Enabled = False
        Me.txttipo_cambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttipo_cambio.ForeColor = System.Drawing.Color.Blue
        Me.txttipo_cambio.Location = New System.Drawing.Point(294, 77)
        Me.txttipo_cambio.Name = "txttipo_cambio"
        Me.txttipo_cambio.Size = New System.Drawing.Size(100, 20)
        Me.txttipo_cambio.TabIndex = 157
        Me.txttipo_cambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(180, 83)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(111, 13)
        Me.Label7.TabIndex = 158
        Me.Label7.Text = "en linea GOODYEAR:"
        '
        'Cotizacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1065, 611)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txttipo_cambio)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.CkBOtro)
        Me.Controls.Add(Me.CkBCliente)
        Me.Controls.Add(Me.TxtCorreoAd)
        Me.Controls.Add(Me.TxtCorreoC)
        Me.Controls.Add(Me.BtnImprimir)
        Me.Controls.Add(Me.TBOtro)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.BtnEmail)
        Me.Controls.Add(Me.BtnGuardar)
        Me.Controls.Add(Me.LblExito)
        Me.Controls.Add(Me.LblError)
        Me.Controls.Add(Me.LblMensaje)
        Me.Controls.Add(Me.BtnNvo)
        Me.Controls.Add(Me.TxtComentario)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.BtnMenos)
        Me.Controls.Add(Me.BtnMas)
        Me.Controls.Add(Me.DGVCap)
        Me.Controls.Add(Me.CmbLinea)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtDes)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DGVArt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtArticulo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CmbCliente)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Cotizacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cotizacion"
        CType(Me.DGVCap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents Cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Articulo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Precio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descuento As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Importe As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Expand As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Linea As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Linea2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TxtCorreoAd As System.Windows.Forms.TextBox
    Friend WithEvents TxtCorreoC As System.Windows.Forms.TextBox
    Friend WithEvents CkBCliente As System.Windows.Forms.CheckBox
    Friend WithEvents CkBOtro As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txttipo_cambio As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
