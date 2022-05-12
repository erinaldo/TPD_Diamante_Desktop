<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BackOrderVtas
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
    Me.pEncabezado = New System.Windows.Forms.Panel()
    Me.CmbCliente = New System.Windows.Forms.ComboBox()
    Me.lAlm = New System.Windows.Forms.Label()
    Me.cmbAlmacen = New System.Windows.Forms.ComboBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.CmbArticulo = New System.Windows.Forms.ComboBox()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.CkCteProp = New System.Windows.Forms.CheckBox()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.CmbGrupoArticulo = New System.Windows.Forms.ComboBox()
    Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
    Me.BtnClientes = New System.Windows.Forms.Button()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.BtnAgentes = New System.Windows.Forms.Button()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.BtnArticulo = New System.Windows.Forms.Button()
    Me.BtnLinea = New System.Windows.Forms.Button()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Panel4 = New System.Windows.Forms.Panel()
    Me.DgArticulos = New System.Windows.Forms.DataGridView()
    Me.Panel3 = New System.Windows.Forms.Panel()
    Me.DgLineas = New System.Windows.Forms.DataGridView()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.DgClientes = New System.Windows.Forms.DataGridView()
    Me.Panel5 = New System.Windows.Forms.Panel()
    Me.DgVtaAgte = New System.Windows.Forms.DataGridView()
    Me.pEncabezado.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.Panel4.SuspendLayout()
    CType(Me.DgArticulos, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel3.SuspendLayout()
    CType(Me.DgLineas, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel2.SuspendLayout()
    CType(Me.DgClientes, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel5.SuspendLayout()
    CType(Me.DgVtaAgte, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'pEncabezado
    '
    Me.pEncabezado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.pEncabezado.Controls.Add(Me.CmbCliente)
    Me.pEncabezado.Controls.Add(Me.lAlm)
    Me.pEncabezado.Controls.Add(Me.cmbAlmacen)
    Me.pEncabezado.Controls.Add(Me.Label9)
    Me.pEncabezado.Controls.Add(Me.Label5)
    Me.pEncabezado.Controls.Add(Me.Label10)
    Me.pEncabezado.Controls.Add(Me.CmbArticulo)
    Me.pEncabezado.Controls.Add(Me.Label13)
    Me.pEncabezado.Controls.Add(Me.CkCteProp)
    Me.pEncabezado.Controls.Add(Me.Label14)
    Me.pEncabezado.Controls.Add(Me.CmbGrupoArticulo)
    Me.pEncabezado.Controls.Add(Me.DtpFechaIni)
    Me.pEncabezado.Controls.Add(Me.Label3)
    Me.pEncabezado.Controls.Add(Me.CmbAgteVta)
    Me.pEncabezado.Controls.Add(Me.Label12)
    Me.pEncabezado.Controls.Add(Me.DtpFechaTer)
    Me.pEncabezado.Controls.Add(Me.BtnClientes)
    Me.pEncabezado.Controls.Add(Me.Button1)
    Me.pEncabezado.Controls.Add(Me.BtnAgentes)
    Me.pEncabezado.Controls.Add(Me.Label8)
    Me.pEncabezado.Controls.Add(Me.Label4)
    Me.pEncabezado.Controls.Add(Me.BtnArticulo)
    Me.pEncabezado.Controls.Add(Me.BtnLinea)
    Me.pEncabezado.Controls.Add(Me.Label7)
    Me.pEncabezado.Dock = System.Windows.Forms.DockStyle.Top
    Me.pEncabezado.Location = New System.Drawing.Point(0, 0)
    Me.pEncabezado.Name = "pEncabezado"
    Me.pEncabezado.Size = New System.Drawing.Size(1355, 96)
    Me.pEncabezado.TabIndex = 185
    '
    'CmbCliente
    '
    Me.CmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.CmbCliente.FormattingEnabled = True
    Me.CmbCliente.Location = New System.Drawing.Point(343, 37)
    Me.CmbCliente.Name = "CmbCliente"
    Me.CmbCliente.Size = New System.Drawing.Size(207, 21)
    Me.CmbCliente.TabIndex = 193
    '
    'lAlm
    '
    Me.lAlm.AutoSize = True
    Me.lAlm.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lAlm.Location = New System.Drawing.Point(514, 7)
    Me.lAlm.Name = "lAlm"
    Me.lAlm.Size = New System.Drawing.Size(62, 17)
    Me.lAlm.TabIndex = 191
    Me.lAlm.Text = "Almacen"
    '
    'cmbAlmacen
    '
    Me.cmbAlmacen.FormattingEnabled = True
    Me.cmbAlmacen.Location = New System.Drawing.Point(582, 5)
    Me.cmbAlmacen.Name = "cmbAlmacen"
    Me.cmbAlmacen.Size = New System.Drawing.Size(97, 21)
    Me.cmbAlmacen.TabIndex = 192
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label9.Location = New System.Drawing.Point(282, 65)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(55, 17)
    Me.Label9.TabIndex = 192
    Me.Label9.Text = "Artículo"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.Location = New System.Drawing.Point(3, 6)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(87, 17)
    Me.Label5.TabIndex = 172
    Me.Label5.Text = "Fecha Inicio:"
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label10.Location = New System.Drawing.Point(15, 66)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(43, 17)
    Me.Label10.TabIndex = 189
    Me.Label10.Text = "Línea"
    '
    'CmbArticulo
    '
    Me.CmbArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbArticulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.CmbArticulo.FormattingEnabled = True
    Me.CmbArticulo.Location = New System.Drawing.Point(343, 65)
    Me.CmbArticulo.Name = "CmbArticulo"
    Me.CmbArticulo.Size = New System.Drawing.Size(207, 21)
    Me.CmbArticulo.TabIndex = 191
    '
    'Label13
    '
    Me.Label13.AutoSize = True
    Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label13.Location = New System.Drawing.Point(286, 38)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(51, 17)
    Me.Label13.TabIndex = 190
    Me.Label13.Text = "Cliente"
    '
    'CkCteProp
    '
    Me.CkCteProp.AutoSize = True
    Me.CkCteProp.Checked = True
    Me.CkCteProp.CheckState = System.Windows.Forms.CheckState.Checked
    Me.CkCteProp.Location = New System.Drawing.Point(582, 56)
    Me.CkCteProp.Name = "CkCteProp"
    Me.CkCteProp.Size = New System.Drawing.Size(100, 17)
    Me.CkCteProp.TabIndex = 175
    Me.CkCteProp.Text = "Clientes propios"
    Me.CkCteProp.UseVisualStyleBackColor = True
    Me.CkCteProp.Visible = False
    '
    'Label14
    '
    Me.Label14.AutoSize = True
    Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label14.Location = New System.Drawing.Point(5, 38)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(53, 17)
    Me.Label14.TabIndex = 188
    Me.Label14.Text = "Agente"
    '
    'CmbGrupoArticulo
    '
    Me.CmbGrupoArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbGrupoArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbGrupoArticulo.FormattingEnabled = True
    Me.CmbGrupoArticulo.Location = New System.Drawing.Point(64, 65)
    Me.CmbGrupoArticulo.Name = "CmbGrupoArticulo"
    Me.CmbGrupoArticulo.Size = New System.Drawing.Size(207, 21)
    Me.CmbGrupoArticulo.TabIndex = 190
    '
    'DtpFechaIni
    '
    Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.DtpFechaIni.Location = New System.Drawing.Point(97, 4)
    Me.DtpFechaIni.Name = "DtpFechaIni"
    Me.DtpFechaIni.Size = New System.Drawing.Size(112, 23)
    Me.DtpFechaIni.TabIndex = 150
    Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label3.Location = New System.Drawing.Point(236, 6)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(107, 17)
    Me.Label3.TabIndex = 153
    Me.Label3.Text = "Fecha Término:"
    '
    'CmbAgteVta
    '
    Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbAgteVta.FormattingEnabled = True
    Me.CmbAgteVta.Location = New System.Drawing.Point(64, 38)
    Me.CmbAgteVta.Name = "CmbAgteVta"
    Me.CmbAgteVta.Size = New System.Drawing.Size(207, 21)
    Me.CmbAgteVta.TabIndex = 188
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label12.Location = New System.Drawing.Point(906, 56)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(58, 17)
    Me.Label12.TabIndex = 171
    Me.Label12.Text = "Clientes"
    '
    'DtpFechaTer
    '
    Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DtpFechaTer.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.DtpFechaTer.Location = New System.Drawing.Point(356, 4)
    Me.DtpFechaTer.Name = "DtpFechaTer"
    Me.DtpFechaTer.Size = New System.Drawing.Size(110, 23)
    Me.DtpFechaTer.TabIndex = 151
    Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
    '
    'BtnClientes
    '
    Me.BtnClientes.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
    Me.BtnClientes.Location = New System.Drawing.Point(965, 47)
    Me.BtnClientes.Name = "BtnClientes"
    Me.BtnClientes.Size = New System.Drawing.Size(36, 34)
    Me.BtnClientes.TabIndex = 170
    Me.BtnClientes.UseVisualStyleBackColor = True
    '
    'Button1
    '
    Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
    Me.Button1.Location = New System.Drawing.Point(728, 19)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(75, 31)
    Me.Button1.TabIndex = 152
    Me.Button1.Text = "Consultar"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'BtnAgentes
    '
    Me.BtnAgentes.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
    Me.BtnAgentes.Location = New System.Drawing.Point(965, 6)
    Me.BtnAgentes.Name = "BtnAgentes"
    Me.BtnAgentes.Size = New System.Drawing.Size(36, 34)
    Me.BtnAgentes.TabIndex = 155
    Me.BtnAgentes.UseVisualStyleBackColor = True
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label8.Location = New System.Drawing.Point(1007, 56)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(55, 17)
    Me.Label8.TabIndex = 164
    Me.Label8.Text = "Articulo"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label4.Location = New System.Drawing.Point(905, 14)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(44, 17)
    Me.Label4.TabIndex = 160
    Me.Label4.Text = "Agtes"
    '
    'BtnArticulo
    '
    Me.BtnArticulo.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
    Me.BtnArticulo.Location = New System.Drawing.Point(1068, 47)
    Me.BtnArticulo.Name = "BtnArticulo"
    Me.BtnArticulo.Size = New System.Drawing.Size(36, 34)
    Me.BtnArticulo.TabIndex = 163
    Me.BtnArticulo.UseVisualStyleBackColor = True
    '
    'BtnLinea
    '
    Me.BtnLinea.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
    Me.BtnLinea.Location = New System.Drawing.Point(1068, 7)
    Me.BtnLinea.Name = "BtnLinea"
    Me.BtnLinea.Size = New System.Drawing.Size(36, 34)
    Me.BtnLinea.TabIndex = 161
    Me.BtnLinea.UseVisualStyleBackColor = True
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label7.Location = New System.Drawing.Point(1019, 15)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(43, 17)
    Me.Label7.TabIndex = 162
    Me.Label7.Text = "Linea"
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Controls.Add(Me.Label11)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.Label6)
    Me.Panel1.Controls.Add(Me.Panel4)
    Me.Panel1.Controls.Add(Me.Panel3)
    Me.Panel1.Controls.Add(Me.Panel2)
    Me.Panel1.Controls.Add(Me.Panel5)
    Me.Panel1.Location = New System.Drawing.Point(1, 94)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(1354, 519)
    Me.Panel1.TabIndex = 186
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.BackColor = System.Drawing.Color.Gainsboro
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.ForeColor = System.Drawing.Color.Black
    Me.Label2.Location = New System.Drawing.Point(14, 307)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(98, 17)
    Me.Label2.TabIndex = 202
    Me.Label2.Text = "Ventas Lineas"
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.BackColor = System.Drawing.Color.Gainsboro
    Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label11.ForeColor = System.Drawing.Color.Black
    Me.Label11.Location = New System.Drawing.Point(661, 307)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(110, 17)
    Me.Label11.TabIndex = 203
    Me.Label11.Text = "Ventas Artículos"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.BackColor = System.Drawing.Color.White
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.Black
    Me.Label1.Location = New System.Drawing.Point(661, 4)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(110, 17)
    Me.Label1.TabIndex = 201
    Me.Label1.Text = "Ventas  Clientes"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.BackColor = System.Drawing.Color.White
    Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label6.ForeColor = System.Drawing.Color.Black
    Me.Label6.Location = New System.Drawing.Point(14, 4)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(105, 17)
    Me.Label6.TabIndex = 200
    Me.Label6.Text = "Ventas  Agente"
    '
    'Panel4
    '
    Me.Panel4.Controls.Add(Me.DgArticulos)
    Me.Panel4.Location = New System.Drawing.Point(659, 325)
    Me.Panel4.Name = "Panel4"
    Me.Panel4.Size = New System.Drawing.Size(693, 190)
    Me.Panel4.TabIndex = 199
    '
    'DgArticulos
    '
    Me.DgArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgArticulos.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DgArticulos.Location = New System.Drawing.Point(0, 0)
    Me.DgArticulos.Name = "DgArticulos"
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgArticulos.RowsDefaultCellStyle = DataGridViewCellStyle1
    Me.DgArticulos.Size = New System.Drawing.Size(693, 190)
    Me.DgArticulos.TabIndex = 185
    '
    'Panel3
    '
    Me.Panel3.Controls.Add(Me.DgLineas)
    Me.Panel3.Location = New System.Drawing.Point(2, 325)
    Me.Panel3.Name = "Panel3"
    Me.Panel3.Size = New System.Drawing.Size(651, 190)
    Me.Panel3.TabIndex = 198
    '
    'DgLineas
    '
    Me.DgLineas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgLineas.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DgLineas.Location = New System.Drawing.Point(0, 0)
    Me.DgLineas.Name = "DgLineas"
    DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgLineas.RowsDefaultCellStyle = DataGridViewCellStyle2
    Me.DgLineas.Size = New System.Drawing.Size(651, 190)
    Me.DgLineas.TabIndex = 181
    '
    'Panel2
    '
    Me.Panel2.Controls.Add(Me.DgClientes)
    Me.Panel2.Location = New System.Drawing.Point(659, 24)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(693, 279)
    Me.Panel2.TabIndex = 197
    '
    'DgClientes
    '
    Me.DgClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgClientes.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DgClientes.Location = New System.Drawing.Point(0, 0)
    Me.DgClientes.Name = "DgClientes"
    DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgClientes.RowsDefaultCellStyle = DataGridViewCellStyle3
    Me.DgClientes.Size = New System.Drawing.Size(693, 279)
    Me.DgClientes.TabIndex = 188
    '
    'Panel5
    '
    Me.Panel5.Controls.Add(Me.DgVtaAgte)
    Me.Panel5.Location = New System.Drawing.Point(2, 24)
    Me.Panel5.Name = "Panel5"
    Me.Panel5.Size = New System.Drawing.Size(651, 279)
    Me.Panel5.TabIndex = 196
    '
    'DgVtaAgte
    '
    Me.DgVtaAgte.AllowUserToAddRows = False
    Me.DgVtaAgte.AllowUserToDeleteRows = False
    Me.DgVtaAgte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgVtaAgte.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DgVtaAgte.Location = New System.Drawing.Point(0, 0)
    Me.DgVtaAgte.Name = "DgVtaAgte"
    DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgVtaAgte.RowsDefaultCellStyle = DataGridViewCellStyle4
    Me.DgVtaAgte.Size = New System.Drawing.Size(651, 279)
    Me.DgVtaAgte.TabIndex = 187
    '
    'BackOrderVtas
    '
    Me.AcceptButton = Me.Button1
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1355, 615)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.pEncabezado)
    Me.Name = "BackOrderVtas"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Back Order - Ventas"
    Me.pEncabezado.ResumeLayout(False)
    Me.pEncabezado.PerformLayout()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel4.ResumeLayout(False)
    CType(Me.DgArticulos, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel3.ResumeLayout(False)
    CType(Me.DgLineas, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel2.ResumeLayout(False)
    CType(Me.DgClientes, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel5.ResumeLayout(False)
    CType(Me.DgVtaAgte, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents pEncabezado As System.Windows.Forms.Panel
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label12 As System.Windows.Forms.Label
  Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
  Friend WithEvents BtnClientes As System.Windows.Forms.Button
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents BtnAgentes As System.Windows.Forms.Button
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents BtnArticulo As System.Windows.Forms.Button
  Friend WithEvents BtnLinea As System.Windows.Forms.Button
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents cmbAlmacen As System.Windows.Forms.ComboBox
  Friend WithEvents CmbArticulo As System.Windows.Forms.ComboBox
  Friend WithEvents CmbGrupoArticulo As System.Windows.Forms.ComboBox
  Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
  Friend WithEvents lAlm As System.Windows.Forms.Label
  Friend WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents Label13 As System.Windows.Forms.Label
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents CmbCliente As System.Windows.Forms.ComboBox
  Friend WithEvents CkCteProp As System.Windows.Forms.CheckBox
  Friend WithEvents Panel1 As Panel
  Friend WithEvents Label2 As Label
  Friend WithEvents Label11 As Label
  Friend WithEvents Label1 As Label
  Friend WithEvents Label6 As Label
  Friend WithEvents Panel4 As Panel
  Friend WithEvents DgArticulos As DataGridView
  Friend WithEvents Panel3 As Panel
  Friend WithEvents DgLineas As DataGridView
  Friend WithEvents Panel2 As Panel
  Friend WithEvents DgClientes As DataGridView
  Friend WithEvents Panel5 As Panel
  Friend WithEvents DgVtaAgte As DataGridView
End Class
