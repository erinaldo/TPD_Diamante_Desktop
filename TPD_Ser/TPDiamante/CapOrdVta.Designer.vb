<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CapOrdVta
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
  Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Me.CmbCliente = New System.Windows.Forms.ComboBox()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.CmbEnvio = New System.Windows.Forms.ComboBox()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.TxtArticulo = New System.Windows.Forms.TextBox()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.DGVArt = New System.Windows.Forms.DataGridView()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.TxtDes = New System.Windows.Forms.TextBox()
  Me.CmbLinea = New System.Windows.Forms.ComboBox()
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
  Me.BtnMenos = New System.Windows.Forms.Button()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.TxtComentario = New System.Windows.Forms.TextBox()
  Me.TxtCorreoC = New System.Windows.Forms.TextBox()
  Me.BtnImprimir = New System.Windows.Forms.Button()
  Me.BtnMas = New System.Windows.Forms.Button()
  Me.BtnNvo = New System.Windows.Forms.Button()
  Me.LblMensaje = New System.Windows.Forms.Label()
  Me.LblError = New System.Windows.Forms.Label()
  Me.LblExito = New System.Windows.Forms.Label()
  Me.BtnGuardar = New System.Windows.Forms.Button()
  Me.BtnEmail = New System.Windows.Forms.Button()
  Me.TxtCorreoAd = New System.Windows.Forms.TextBox()
  Me.Label10 = New System.Windows.Forms.Label()
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
  Me.Label7 = New System.Windows.Forms.Label()
  Me.txttipo_cambio = New System.Windows.Forms.TextBox()
  Me.Label9 = New System.Windows.Forms.Label()
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.Panel2 = New System.Windows.Forms.Panel()
  Me.Panel3 = New System.Windows.Forms.Panel()
  Me.Panel4 = New System.Windows.Forms.Panel()
  CType(Me.DGVArt, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.DGVCap, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.Panel1.SuspendLayout()
  Me.Panel2.SuspendLayout()
  Me.Panel3.SuspendLayout()
  Me.Panel4.SuspendLayout()
  Me.SuspendLayout()
  '
  'CmbCliente
  '
  Me.CmbCliente.DropDownHeight = 90
  Me.CmbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.CmbCliente.FormattingEnabled = True
  Me.CmbCliente.IntegralHeight = False
  Me.CmbCliente.ItemHeight = 13
  Me.CmbCliente.Location = New System.Drawing.Point(56, 4)
  Me.CmbCliente.Name = "CmbCliente"
  Me.CmbCliente.Size = New System.Drawing.Size(535, 21)
  Me.CmbCliente.TabIndex = 0
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(10, 7)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(46, 13)
  Me.Label2.TabIndex = 18
  Me.Label2.Text = "Cliente"
  '
  'CmbEnvio
  '
  Me.CmbEnvio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbEnvio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbEnvio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.CmbEnvio.FormattingEnabled = True
  Me.CmbEnvio.Location = New System.Drawing.Point(669, 4)
  Me.CmbEnvio.Name = "CmbEnvio"
  Me.CmbEnvio.Size = New System.Drawing.Size(174, 21)
  Me.CmbEnvio.TabIndex = 1
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(597, 5)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(72, 15)
  Me.Label1.TabIndex = 19
  Me.Label1.Text = "Enviar por"
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(16, 89)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(72, 13)
  Me.Label3.TabIndex = 17
  Me.Label3.Text = "Buscar por:"
  '
  'TxtArticulo
  '
  Me.TxtArticulo.Location = New System.Drawing.Point(20, 124)
  Me.TxtArticulo.Name = "TxtArticulo"
  Me.TxtArticulo.Size = New System.Drawing.Size(151, 20)
  Me.TxtArticulo.TabIndex = 3
  '
  'Label4
  '
  Me.Label4.AutoSize = True
  Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label4.Location = New System.Drawing.Point(18, 107)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(52, 13)
  Me.Label4.TabIndex = 12
  Me.Label4.Text = "Artículo"
  '
  'DGVArt
  '
  Me.DGVArt.AllowUserToAddRows = False
  Me.DGVArt.AllowUserToDeleteRows = False
  DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
  DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
  DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
  DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
  DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
  DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
  DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
  Me.DGVArt.DefaultCellStyle = DataGridViewCellStyle1
  Me.DGVArt.Dock = System.Windows.Forms.DockStyle.Fill
  Me.DGVArt.Location = New System.Drawing.Point(0, 0)
  Me.DGVArt.Name = "DGVArt"
  Me.DGVArt.ReadOnly = True
  Me.DGVArt.RowTemplate.Height = 21
  Me.DGVArt.Size = New System.Drawing.Size(667, 427)
  Me.DGVArt.TabIndex = 6
  '
  'Label5
  '
  Me.Label5.AutoSize = True
  Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label5.Location = New System.Drawing.Point(460, 108)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(38, 13)
  Me.Label5.TabIndex = 14
  Me.Label5.Text = "Linea"
  '
  'Label6
  '
  Me.Label6.AutoSize = True
  Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label6.Location = New System.Drawing.Point(182, 107)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(74, 13)
  Me.Label6.TabIndex = 13
  Me.Label6.Text = "Descripción"
  '
  'TxtDes
  '
  Me.TxtDes.Location = New System.Drawing.Point(185, 124)
  Me.TxtDes.Name = "TxtDes"
  Me.TxtDes.Size = New System.Drawing.Size(272, 20)
  Me.TxtDes.TabIndex = 4
  '
  'CmbLinea
  '
  Me.CmbLinea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbLinea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbLinea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.CmbLinea.FormattingEnabled = True
  Me.CmbLinea.Location = New System.Drawing.Point(463, 124)
  Me.CmbLinea.Name = "CmbLinea"
  Me.CmbLinea.Size = New System.Drawing.Size(152, 21)
  Me.CmbLinea.TabIndex = 5
  '
  'DGVCap
  '
  Me.DGVCap.AllowUserToAddRows = False
  Me.DGVCap.AllowUserToDeleteRows = False
  DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
  DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
  DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
  DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
  DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
  DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
  Me.DGVCap.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
  Me.DGVCap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.DGVCap.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Cantidad, Me.Articulo, Me.Descripcion, Me.Precio, Me.Descuento, Me.Importe, Me.Price, Me.Expand, Me.Linea, Me.Linea2})
  DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
  DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
  DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
  DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
  DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
  DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
  DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
  Me.DGVCap.DefaultCellStyle = DataGridViewCellStyle4
  Me.DGVCap.Dock = System.Windows.Forms.DockStyle.Fill
  Me.DGVCap.Location = New System.Drawing.Point(0, 0)
  Me.DGVCap.Name = "DGVCap"
  Me.DGVCap.RowHeadersVisible = False
  Me.DGVCap.RowTemplate.Height = 21
  Me.DGVCap.Size = New System.Drawing.Size(503, 431)
  Me.DGVCap.TabIndex = 9
  '
  'Cantidad
  '
  DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  DataGridViewCellStyle3.Format = "N0"
  DataGridViewCellStyle3.NullValue = Nothing
  Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle3
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
  Me.Price.Visible = False
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
  'BtnMenos
  '
  Me.BtnMenos.BackColor = System.Drawing.SystemColors.Control
  Me.BtnMenos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnMenos.Image = Global.TPDiamante.My.Resources.Resources.Minus__6_
  Me.BtnMenos.Location = New System.Drawing.Point(4, 77)
  Me.BtnMenos.Name = "BtnMenos"
  Me.BtnMenos.Size = New System.Drawing.Size(38, 43)
  Me.BtnMenos.TabIndex = 12
  Me.BtnMenos.UseVisualStyleBackColor = False
  '
  'Label8
  '
  Me.Label8.AutoSize = True
  Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label8.Location = New System.Drawing.Point(854, 5)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(81, 15)
  Me.Label8.TabIndex = 20
  Me.Label8.Text = "Comentario"
  '
  'TxtComentario
  '
  Me.TxtComentario.Location = New System.Drawing.Point(941, 4)
  Me.TxtComentario.MaxLength = 275
  Me.TxtComentario.Multiline = True
  Me.TxtComentario.Name = "TxtComentario"
  Me.TxtComentario.Size = New System.Drawing.Size(277, 78)
  Me.TxtComentario.TabIndex = 2
  '
  'TxtCorreoC
  '
  Me.TxtCorreoC.Enabled = False
  Me.TxtCorreoC.Location = New System.Drawing.Point(855, 109)
  Me.TxtCorreoC.Name = "TxtCorreoC"
  Me.TxtCorreoC.ReadOnly = True
  Me.TxtCorreoC.Size = New System.Drawing.Size(363, 20)
  Me.TxtCorreoC.TabIndex = 8
  '
  'BtnImprimir
  '
  Me.BtnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnImprimir.ForeColor = System.Drawing.Color.MediumBlue
  Me.BtnImprimir.Image = Global.TPDiamante.My.Resources.Resources.printer
  Me.BtnImprimir.Location = New System.Drawing.Point(4, 256)
  Me.BtnImprimir.Name = "BtnImprimir"
  Me.BtnImprimir.Size = New System.Drawing.Size(38, 43)
  Me.BtnImprimir.TabIndex = 10
  Me.BtnImprimir.Text = "            "
  Me.BtnImprimir.UseVisualStyleBackColor = True
  '
  'BtnMas
  '
  Me.BtnMas.BackColor = System.Drawing.SystemColors.Control
  Me.BtnMas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnMas.Image = Global.TPDiamante.My.Resources.Resources.Plus__6_
  Me.BtnMas.Location = New System.Drawing.Point(4, 4)
  Me.BtnMas.Name = "BtnMas"
  Me.BtnMas.Size = New System.Drawing.Size(38, 43)
  Me.BtnMas.TabIndex = 7
  Me.BtnMas.UseVisualStyleBackColor = False
  '
  'BtnNvo
  '
  Me.BtnNvo.BackColor = System.Drawing.SystemColors.Control
  Me.BtnNvo.Enabled = False
  Me.BtnNvo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnNvo.Location = New System.Drawing.Point(4, 157)
  Me.BtnNvo.Name = "BtnNvo"
  Me.BtnNvo.Size = New System.Drawing.Size(38, 43)
  Me.BtnNvo.TabIndex = 87
  Me.BtnNvo.Text = "&Nvo"
  Me.BtnNvo.UseVisualStyleBackColor = False
  '
  'LblMensaje
  '
  Me.LblMensaje.AutoSize = True
  Me.LblMensaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.LblMensaje.ForeColor = System.Drawing.SystemColors.HotTrack
  Me.LblMensaje.Location = New System.Drawing.Point(664, 94)
  Me.LblMensaje.Name = "LblMensaje"
  Me.LblMensaje.Size = New System.Drawing.Size(412, 15)
  Me.LblMensaje.TabIndex = 88
  Me.LblMensaje.Text = "Creando Orden de Venta y Enviando Correo Electronico. . . . . . ."
  Me.LblMensaje.Visible = False
  '
  'LblError
  '
  Me.LblError.AutoSize = True
  Me.LblError.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.LblError.ForeColor = System.Drawing.Color.Red
  Me.LblError.Location = New System.Drawing.Point(667, 94)
  Me.LblError.Name = "LblError"
  Me.LblError.Size = New System.Drawing.Size(337, 15)
  Me.LblError.TabIndex = 89
  Me.LblError.Text = "No fue posible enviar EMails. Intentelo nuevamente"
  Me.LblError.Visible = False
  '
  'LblExito
  '
  Me.LblExito.AutoSize = True
  Me.LblExito.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.LblExito.ForeColor = System.Drawing.Color.Green
  Me.LblExito.Location = New System.Drawing.Point(667, 79)
  Me.LblExito.Name = "LblExito"
  Me.LblExito.Size = New System.Drawing.Size(260, 15)
  Me.LblExito.TabIndex = 15
  Me.LblExito.Text = "Orden Creada y Enviada Exitosamente !"
  Me.LblExito.Visible = False
  '
  'BtnGuardar
  '
  Me.BtnGuardar.BackColor = System.Drawing.Color.LightBlue
  Me.BtnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnGuardar.Location = New System.Drawing.Point(669, 115)
  Me.BtnGuardar.Name = "BtnGuardar"
  Me.BtnGuardar.Size = New System.Drawing.Size(64, 37)
  Me.BtnGuardar.TabIndex = 16
  Me.BtnGuardar.Text = "Guardar y Enviar "
  Me.BtnGuardar.UseVisualStyleBackColor = False
  '
  'BtnEmail
  '
  Me.BtnEmail.BackColor = System.Drawing.SystemColors.Control
  Me.BtnEmail.Enabled = False
  Me.BtnEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnEmail.ForeColor = System.Drawing.Color.Red
  Me.BtnEmail.Location = New System.Drawing.Point(742, 115)
  Me.BtnEmail.Name = "BtnEmail"
  Me.BtnEmail.Size = New System.Drawing.Size(65, 37)
  Me.BtnEmail.TabIndex = 17
  Me.BtnEmail.Text = "Enviar EMails"
  Me.BtnEmail.UseVisualStyleBackColor = False
  '
  'TxtCorreoAd
  '
  Me.TxtCorreoAd.Location = New System.Drawing.Point(856, 131)
  Me.TxtCorreoAd.Name = "TxtCorreoAd"
  Me.TxtCorreoAd.Size = New System.Drawing.Size(362, 20)
  Me.TxtCorreoAd.TabIndex = 90
  '
  'Label10
  '
  Me.Label10.AutoSize = True
  Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label10.Location = New System.Drawing.Point(809, 132)
  Me.Label10.Name = "Label10"
  Me.Label10.Size = New System.Drawing.Size(44, 15)
  Me.Label10.TabIndex = 91
  Me.Label10.Text = "EMail"
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
  Me.DataGridViewTextBoxColumn4.HeaderText = "Precio"
  Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
  Me.DataGridViewTextBoxColumn4.ReadOnly = True
  '
  'DataGridViewTextBoxColumn5
  '
  Me.DataGridViewTextBoxColumn5.HeaderText = "Desc. Prom."
  Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
  Me.DataGridViewTextBoxColumn5.ReadOnly = True
  '
  'DataGridViewTextBoxColumn6
  '
  Me.DataGridViewTextBoxColumn6.HeaderText = "Importe"
  Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
  Me.DataGridViewTextBoxColumn6.ReadOnly = True
  '
  'DataGridViewTextBoxColumn7
  '
  Me.DataGridViewTextBoxColumn7.HeaderText = "Price"
  Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
  Me.DataGridViewTextBoxColumn7.Visible = False
  '
  'DataGridViewTextBoxColumn8
  '
  Me.DataGridViewTextBoxColumn8.HeaderText = "Expand"
  Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
  Me.DataGridViewTextBoxColumn8.Visible = False
  '
  'DataGridViewTextBoxColumn9
  '
  Me.DataGridViewTextBoxColumn9.HeaderText = "Linea"
  Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
  Me.DataGridViewTextBoxColumn9.Visible = False
  '
  'DataGridViewTextBoxColumn10
  '
  Me.DataGridViewTextBoxColumn10.HeaderText = "Linea2"
  Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
  Me.DataGridViewTextBoxColumn10.Visible = False
  '
  'Label7
  '
  Me.Label7.AutoSize = True
  Me.Label7.Location = New System.Drawing.Point(17, 55)
  Me.Label7.Name = "Label7"
  Me.Label7.Size = New System.Drawing.Size(111, 13)
  Me.Label7.TabIndex = 161
  Me.Label7.Text = "en linea GOODYEAR:"
  '
  'txttipo_cambio
  '
  Me.txttipo_cambio.Enabled = False
  Me.txttipo_cambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txttipo_cambio.ForeColor = System.Drawing.Color.Blue
  Me.txttipo_cambio.Location = New System.Drawing.Point(131, 49)
  Me.txttipo_cambio.Name = "txttipo_cambio"
  Me.txttipo_cambio.Size = New System.Drawing.Size(100, 20)
  Me.txttipo_cambio.TabIndex = 160
  Me.txttipo_cambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'Label9
  '
  Me.Label9.AutoSize = True
  Me.Label9.Location = New System.Drawing.Point(17, 37)
  Me.Label9.Name = "Label9"
  Me.Label9.Size = New System.Drawing.Size(110, 13)
  Me.Label9.TabIndex = 159
  Me.Label9.Text = "Tipo de cambio al día"
  '
  'Panel1
  '
  Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel1.Controls.Add(Me.Label2)
  Me.Panel1.Controls.Add(Me.Label7)
  Me.Panel1.Controls.Add(Me.CmbCliente)
  Me.Panel1.Controls.Add(Me.txttipo_cambio)
  Me.Panel1.Controls.Add(Me.Label1)
  Me.Panel1.Controls.Add(Me.Label9)
  Me.Panel1.Controls.Add(Me.CmbEnvio)
  Me.Panel1.Controls.Add(Me.TxtCorreoAd)
  Me.Panel1.Controls.Add(Me.Label3)
  Me.Panel1.Controls.Add(Me.Label10)
  Me.Panel1.Controls.Add(Me.TxtArticulo)
  Me.Panel1.Controls.Add(Me.BtnEmail)
  Me.Panel1.Controls.Add(Me.Label4)
  Me.Panel1.Controls.Add(Me.BtnGuardar)
  Me.Panel1.Controls.Add(Me.Label5)
  Me.Panel1.Controls.Add(Me.LblExito)
  Me.Panel1.Controls.Add(Me.TxtDes)
  Me.Panel1.Controls.Add(Me.LblError)
  Me.Panel1.Controls.Add(Me.Label6)
  Me.Panel1.Controls.Add(Me.LblMensaje)
  Me.Panel1.Controls.Add(Me.CmbLinea)
  Me.Panel1.Controls.Add(Me.Label8)
  Me.Panel1.Controls.Add(Me.TxtCorreoC)
  Me.Panel1.Controls.Add(Me.TxtComentario)
  Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
  Me.Panel1.Location = New System.Drawing.Point(0, 0)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(1223, 159)
  Me.Panel1.TabIndex = 162
  '
  'Panel2
  '
  Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel2.Controls.Add(Me.DGVArt)
  Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
  Me.Panel2.Location = New System.Drawing.Point(0, 159)
  Me.Panel2.Name = "Panel2"
  Me.Panel2.Size = New System.Drawing.Size(671, 431)
  Me.Panel2.TabIndex = 163
  '
  'Panel3
  '
  Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel3.Controls.Add(Me.BtnMas)
  Me.Panel3.Controls.Add(Me.BtnMenos)
  Me.Panel3.Controls.Add(Me.BtnNvo)
  Me.Panel3.Controls.Add(Me.BtnImprimir)
  Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
  Me.Panel3.Location = New System.Drawing.Point(671, 159)
  Me.Panel3.Name = "Panel3"
  Me.Panel3.Size = New System.Drawing.Size(49, 431)
  Me.Panel3.TabIndex = 164
  '
  'Panel4
  '
  Me.Panel4.Controls.Add(Me.DGVCap)
  Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Panel4.Location = New System.Drawing.Point(720, 159)
  Me.Panel4.Name = "Panel4"
  Me.Panel4.Size = New System.Drawing.Size(503, 431)
  Me.Panel4.TabIndex = 166
  '
  'CapOrdVta
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1223, 590)
  Me.Controls.Add(Me.Panel4)
  Me.Controls.Add(Me.Panel3)
  Me.Controls.Add(Me.Panel2)
  Me.Controls.Add(Me.Panel1)
  Me.Name = "CapOrdVta"
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Captura de Orden de Venta"
  CType(Me.DGVArt, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.DGVCap, System.ComponentModel.ISupportInitialize).EndInit()
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  Me.Panel2.ResumeLayout(False)
  Me.Panel3.ResumeLayout(False)
  Me.Panel4.ResumeLayout(False)
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents CmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CmbEnvio As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtArticulo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DGVArt As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtDes As System.Windows.Forms.TextBox
    Friend WithEvents CmbLinea As System.Windows.Forms.ComboBox
    Friend WithEvents DGVCap As System.Windows.Forms.DataGridView
    Friend WithEvents BtnMas As System.Windows.Forms.Button
    Friend WithEvents BtnMenos As System.Windows.Forms.Button
    Friend WithEvents BtnImprimir As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtComentario As System.Windows.Forms.TextBox
    Friend WithEvents TxtCorreoC As System.Windows.Forms.TextBox
    Friend WithEvents BtnNvo As System.Windows.Forms.Button
    Friend WithEvents LblMensaje As System.Windows.Forms.Label
    Friend WithEvents LblError As System.Windows.Forms.Label
    Friend WithEvents LblExito As System.Windows.Forms.Label
    Friend WithEvents BtnGuardar As System.Windows.Forms.Button
    Friend WithEvents BtnEmail As System.Windows.Forms.Button
    Friend WithEvents TxtCorreoAd As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
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
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txttipo_cambio As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
End Class
