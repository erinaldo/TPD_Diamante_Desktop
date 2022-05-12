<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVisita_Agente
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVisita_Agente))
        Me.tpUsuario = New System.Windows.Forms.TabPage()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.lblVisista = New System.Windows.Forms.Label()
        Me.DTPHORA = New System.Windows.Forms.DateTimePicker()
        Me.txtCardCode = New System.Windows.Forms.TextBox()
        Me.cmbAgente_Marketing = New System.Windows.Forms.ComboBox()
        Me.dgvDetalle = New System.Windows.Forms.DataGridView()
        Me.txtUbicacion = New System.Windows.Forms.TextBox()
        Me.txtComentario = New System.Windows.Forms.TextBox()
        Me.txtLocalidad = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbAgente_Ventas = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.gbxAcciones = New System.Windows.Forms.GroupBox()
        Me.btnVer = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.dtpFecha_Visita = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbCliente = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbxOtro = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbxCobranza = New System.Windows.Forms.CheckBox()
        Me.cbxPedido = New System.Windows.Forms.CheckBox()
        Me.tcVisitas = New System.Windows.Forms.TabControl()
        Me.tpSurpevisor = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnBuscar = New System.Windows.Forms.Button()
        Me.DTPTermino = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.DgvRegistroCom = New System.Windows.Forms.DataGridView()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.CBAgenteM = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.CBAgenteV = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DTPInicio = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TbNovisitados = New System.Windows.Forms.TabPage()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.CBagenteMARNO = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.CBAgenteVeNO = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.tpUsuario.SuspendLayout()
        CType(Me.dgvDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbxAcciones.SuspendLayout()
        Me.tcVisitas.SuspendLayout()
        Me.tpSurpevisor.SuspendLayout()
        CType(Me.DgvRegistroCom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TbNovisitados.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tpUsuario
        '
        Me.tpUsuario.Controls.Add(Me.ComboBox1)
        Me.tpUsuario.Controls.Add(Me.lblVisista)
        Me.tpUsuario.Controls.Add(Me.DTPHORA)
        Me.tpUsuario.Controls.Add(Me.txtCardCode)
        Me.tpUsuario.Controls.Add(Me.cmbAgente_Marketing)
        Me.tpUsuario.Controls.Add(Me.dgvDetalle)
        Me.tpUsuario.Controls.Add(Me.txtUbicacion)
        Me.tpUsuario.Controls.Add(Me.txtComentario)
        Me.tpUsuario.Controls.Add(Me.txtLocalidad)
        Me.tpUsuario.Controls.Add(Me.Label10)
        Me.tpUsuario.Controls.Add(Me.Label1)
        Me.tpUsuario.Controls.Add(Me.cmbAgente_Ventas)
        Me.tpUsuario.Controls.Add(Me.Label9)
        Me.tpUsuario.Controls.Add(Me.Label2)
        Me.tpUsuario.Controls.Add(Me.gbxAcciones)
        Me.tpUsuario.Controls.Add(Me.dtpFecha_Visita)
        Me.tpUsuario.Controls.Add(Me.Label6)
        Me.tpUsuario.Controls.Add(Me.Label8)
        Me.tpUsuario.Controls.Add(Me.cmbCliente)
        Me.tpUsuario.Controls.Add(Me.Label7)
        Me.tpUsuario.Controls.Add(Me.Label3)
        Me.tpUsuario.Controls.Add(Me.Label4)
        Me.tpUsuario.Controls.Add(Me.cbxOtro)
        Me.tpUsuario.Controls.Add(Me.Label5)
        Me.tpUsuario.Controls.Add(Me.cbxCobranza)
        Me.tpUsuario.Controls.Add(Me.cbxPedido)
        Me.tpUsuario.Location = New System.Drawing.Point(4, 24)
        Me.tpUsuario.Name = "tpUsuario"
        Me.tpUsuario.Padding = New System.Windows.Forms.Padding(3)
        Me.tpUsuario.Size = New System.Drawing.Size(1127, 601)
        Me.tpUsuario.TabIndex = 0
        Me.tpUsuario.Text = "Usuario"
        Me.tpUsuario.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(78, 99)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(66, 23)
        Me.ComboBox1.TabIndex = 26
        '
        'lblVisista
        '
        Me.lblVisista.AutoSize = True
        Me.lblVisista.Location = New System.Drawing.Point(361, 169)
        Me.lblVisista.Name = "lblVisista"
        Me.lblVisista.Size = New System.Drawing.Size(86, 15)
        Me.lblVisista.TabIndex = 25
        Me.lblVisista.Text = "Hora de Visita:"
        '
        'DTPHORA
        '
        Me.DTPHORA.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTPHORA.Location = New System.Drawing.Point(364, 187)
        Me.DTPHORA.Name = "DTPHORA"
        Me.DTPHORA.Size = New System.Drawing.Size(106, 21)
        Me.DTPHORA.TabIndex = 24
        '
        'txtCardCode
        '
        Me.txtCardCode.Enabled = False
        Me.txtCardCode.Location = New System.Drawing.Point(8, 234)
        Me.txtCardCode.Name = "txtCardCode"
        Me.txtCardCode.Size = New System.Drawing.Size(66, 21)
        Me.txtCardCode.TabIndex = 23
        Me.txtCardCode.Visible = False
        '
        'cmbAgente_Marketing
        '
        Me.cmbAgente_Marketing.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbAgente_Marketing.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAgente_Marketing.FormattingEnabled = True
        Me.cmbAgente_Marketing.Location = New System.Drawing.Point(150, 7)
        Me.cmbAgente_Marketing.Name = "cmbAgente_Marketing"
        Me.cmbAgente_Marketing.Size = New System.Drawing.Size(320, 23)
        Me.cmbAgente_Marketing.TabIndex = 12
        '
        'dgvDetalle
        '
        Me.dgvDetalle.AllowUserToAddRows = False
        Me.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetalle.Location = New System.Drawing.Point(6, 312)
        Me.dgvDetalle.Name = "dgvDetalle"
        Me.dgvDetalle.RowHeadersWidth = 20
        Me.dgvDetalle.Size = New System.Drawing.Size(1116, 275)
        Me.dgvDetalle.TabIndex = 22
        '
        'txtUbicacion
        '
        Me.txtUbicacion.Location = New System.Drawing.Point(519, 131)
        Me.txtUbicacion.Multiline = True
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.Size = New System.Drawing.Size(388, 82)
        Me.txtUbicacion.TabIndex = 18
        '
        'txtComentario
        '
        Me.txtComentario.Location = New System.Drawing.Point(519, 24)
        Me.txtComentario.Multiline = True
        Me.txtComentario.Name = "txtComentario"
        Me.txtComentario.Size = New System.Drawing.Size(388, 76)
        Me.txtComentario.TabIndex = 17
        '
        'txtLocalidad
        '
        Me.txtLocalidad.Location = New System.Drawing.Point(150, 134)
        Me.txtLocalidad.Name = "txtLocalidad"
        Me.txtLocalidad.Size = New System.Drawing.Size(320, 21)
        Me.txtLocalidad.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(5, 285)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(150, 15)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "Detalle de registro del día."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(123, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Agente de Marketing:"
        '
        'cmbAgente_Ventas
        '
        Me.cmbAgente_Ventas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbAgente_Ventas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAgente_Ventas.FormattingEnabled = True
        Me.cmbAgente_Ventas.Location = New System.Drawing.Point(150, 36)
        Me.cmbAgente_Ventas.Name = "cmbAgente_Ventas"
        Me.cmbAgente_Ventas.Size = New System.Drawing.Size(320, 23)
        Me.cmbAgente_Ventas.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.Label9.Location = New System.Drawing.Point(214, 254)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(674, 18)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "__________________________________________________________________________"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Agente de Ventas:"
        '
        'gbxAcciones
        '
        Me.gbxAcciones.Controls.Add(Me.btnVer)
        Me.gbxAcciones.Controls.Add(Me.btnAgregar)
        Me.gbxAcciones.Location = New System.Drawing.Point(935, 9)
        Me.gbxAcciones.Name = "gbxAcciones"
        Me.gbxAcciones.Size = New System.Drawing.Size(158, 204)
        Me.gbxAcciones.TabIndex = 19
        Me.gbxAcciones.TabStop = False
        Me.gbxAcciones.Text = "Acciones"
        '
        'btnVer
        '
        Me.btnVer.Enabled = False
        Me.btnVer.Image = Global.TPDiamante.My.Resources.Resources.kate1
        Me.btnVer.Location = New System.Drawing.Point(26, 87)
        Me.btnVer.Name = "btnVer"
        Me.btnVer.Size = New System.Drawing.Size(106, 39)
        Me.btnVer.TabIndex = 1
        Me.btnVer.Text = "Ver detalle"
        Me.btnVer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnVer.UseVisualStyleBackColor = True
        Me.btnVer.Visible = False
        '
        'btnAgregar
        '
        Me.btnAgregar.Image = Global.TPDiamante.My.Resources.Resources.add
        Me.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAgregar.Location = New System.Drawing.Point(26, 30)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(106, 40)
        Me.btnAgregar.TabIndex = 0
        Me.btnAgregar.Text = "Agregar visita"
        Me.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'dtpFecha_Visita
        '
        Me.dtpFecha_Visita.Location = New System.Drawing.Point(150, 68)
        Me.dtpFecha_Visita.Name = "dtpFecha_Visita"
        Me.dtpFecha_Visita.Size = New System.Drawing.Size(320, 21)
        Me.dtpFecha_Visita.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 75)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 15)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Fecha visita:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(516, 110)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 15)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Ubicación:"
        '
        'cmbCliente
        '
        Me.cmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.Location = New System.Drawing.Point(150, 99)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(320, 23)
        Me.cmbCliente.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(516, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(170, 15)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Comentarios / Observaciones:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Cliente:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 15)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Localidad:"
        '
        'cbxOtro
        '
        Me.cbxOtro.AutoSize = True
        Me.cbxOtro.Location = New System.Drawing.Point(188, 194)
        Me.cbxOtro.Name = "cbxOtro"
        Me.cbxOtro.Size = New System.Drawing.Size(52, 19)
        Me.cbxOtro.TabIndex = 7
        Me.cbxOtro.Text = "Otro."
        Me.cbxOtro.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 169)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 15)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Motivo de visita:"
        '
        'cbxCobranza
        '
        Me.cbxCobranza.AutoSize = True
        Me.cbxCobranza.Location = New System.Drawing.Point(95, 194)
        Me.cbxCobranza.Name = "cbxCobranza"
        Me.cbxCobranza.Size = New System.Drawing.Size(82, 19)
        Me.cbxCobranza.TabIndex = 6
        Me.cbxCobranza.Text = "Cobranza."
        Me.cbxCobranza.UseVisualStyleBackColor = True
        '
        'cbxPedido
        '
        Me.cbxPedido.AutoSize = True
        Me.cbxPedido.Location = New System.Drawing.Point(16, 194)
        Me.cbxPedido.Name = "cbxPedido"
        Me.cbxPedido.Size = New System.Drawing.Size(68, 19)
        Me.cbxPedido.TabIndex = 5
        Me.cbxPedido.Text = "Pedido."
        Me.cbxPedido.UseVisualStyleBackColor = True
        '
        'tcVisitas
        '
        Me.tcVisitas.Controls.Add(Me.tpUsuario)
        Me.tcVisitas.Controls.Add(Me.tpSurpevisor)
        Me.tcVisitas.Controls.Add(Me.TbNovisitados)
        Me.tcVisitas.Location = New System.Drawing.Point(12, 12)
        Me.tcVisitas.Name = "tcVisitas"
        Me.tcVisitas.SelectedIndex = 0
        Me.tcVisitas.Size = New System.Drawing.Size(1135, 629)
        Me.tcVisitas.TabIndex = 23
        '
        'tpSurpevisor
        '
        Me.tpSurpevisor.Controls.Add(Me.Button1)
        Me.tpSurpevisor.Controls.Add(Me.BtnBuscar)
        Me.tpSurpevisor.Controls.Add(Me.DTPTermino)
        Me.tpSurpevisor.Controls.Add(Me.Label15)
        Me.tpSurpevisor.Controls.Add(Me.DgvRegistroCom)
        Me.tpSurpevisor.Controls.Add(Me.Label14)
        Me.tpSurpevisor.Controls.Add(Me.CBAgenteM)
        Me.tpSurpevisor.Controls.Add(Me.Label11)
        Me.tpSurpevisor.Controls.Add(Me.CBAgenteV)
        Me.tpSurpevisor.Controls.Add(Me.Label12)
        Me.tpSurpevisor.Controls.Add(Me.DTPInicio)
        Me.tpSurpevisor.Controls.Add(Me.Label13)
        Me.tpSurpevisor.Location = New System.Drawing.Point(4, 24)
        Me.tpSurpevisor.Name = "tpSurpevisor"
        Me.tpSurpevisor.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSurpevisor.Size = New System.Drawing.Size(1127, 601)
        Me.tpSurpevisor.TabIndex = 1
        Me.tpSurpevisor.Text = "Supervisor"
        Me.tpSurpevisor.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button1.Location = New System.Drawing.Point(509, 52)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 34)
        Me.Button1.TabIndex = 28
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BtnBuscar
        '
        Me.BtnBuscar.Location = New System.Drawing.Point(509, 20)
        Me.BtnBuscar.Name = "BtnBuscar"
        Me.BtnBuscar.Size = New System.Drawing.Size(75, 23)
        Me.BtnBuscar.TabIndex = 27
        Me.BtnBuscar.Text = "Consulta"
        Me.BtnBuscar.UseVisualStyleBackColor = True
        '
        'DTPTermino
        '
        Me.DTPTermino.Location = New System.Drawing.Point(768, 44)
        Me.DTPTermino.Name = "DTPTermino"
        Me.DTPTermino.Size = New System.Drawing.Size(320, 21)
        Me.DTPTermino.TabIndex = 26
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(621, 51)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(89, 15)
        Me.Label15.TabIndex = 25
        Me.Label15.Text = "Fecha termino:"
        '
        'DgvRegistroCom
        '
        Me.DgvRegistroCom.AllowUserToAddRows = False
        Me.DgvRegistroCom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvRegistroCom.Location = New System.Drawing.Point(3, 126)
        Me.DgvRegistroCom.Name = "DgvRegistroCom"
        Me.DgvRegistroCom.RowHeadersWidth = 20
        Me.DgvRegistroCom.Size = New System.Drawing.Size(1118, 469)
        Me.DgvRegistroCom.TabIndex = 24
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(11, 99)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(110, 15)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "Detalle de registro."
        '
        'CBAgenteM
        '
        Me.CBAgenteM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.CBAgenteM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBAgenteM.FormattingEnabled = True
        Me.CBAgenteM.Location = New System.Drawing.Point(158, 18)
        Me.CBAgenteM.Name = "CBAgenteM"
        Me.CBAgenteM.Size = New System.Drawing.Size(320, 23)
        Me.CBAgenteM.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(11, 21)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(123, 15)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "Agente de Marketing:"
        '
        'CBAgenteV
        '
        Me.CBAgenteV.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.CBAgenteV.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBAgenteV.FormattingEnabled = True
        Me.CBAgenteV.Location = New System.Drawing.Point(158, 48)
        Me.CBAgenteV.Name = "CBAgenteV"
        Me.CBAgenteV.Size = New System.Drawing.Size(320, 23)
        Me.CBAgenteV.TabIndex = 19
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(11, 52)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(105, 15)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Agente de Ventas:"
        '
        'DTPInicio
        '
        Me.DTPInicio.Location = New System.Drawing.Point(768, 17)
        Me.DTPInicio.Name = "DTPInicio"
        Me.DTPInicio.Size = New System.Drawing.Size(320, 21)
        Me.DTPInicio.TabIndex = 20
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(621, 24)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 15)
        Me.Label13.TabIndex = 17
        Me.Label13.Text = "Fecha Inicio:"
        '
        'TbNovisitados
        '
        Me.TbNovisitados.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.TbNovisitados.Controls.Add(Me.Button2)
        Me.TbNovisitados.Controls.Add(Me.Button3)
        Me.TbNovisitados.Controls.Add(Me.DateTimePicker1)
        Me.TbNovisitados.Controls.Add(Me.Label16)
        Me.TbNovisitados.Controls.Add(Me.DataGridView1)
        Me.TbNovisitados.Controls.Add(Me.Label17)
        Me.TbNovisitados.Controls.Add(Me.CBagenteMARNO)
        Me.TbNovisitados.Controls.Add(Me.Label18)
        Me.TbNovisitados.Controls.Add(Me.CBAgenteVeNO)
        Me.TbNovisitados.Controls.Add(Me.Label19)
        Me.TbNovisitados.Controls.Add(Me.DateTimePicker2)
        Me.TbNovisitados.Controls.Add(Me.Label20)
        Me.TbNovisitados.Location = New System.Drawing.Point(4, 24)
        Me.TbNovisitados.Name = "TbNovisitados"
        Me.TbNovisitados.Size = New System.Drawing.Size(1127, 601)
        Me.TbNovisitados.TabIndex = 2
        Me.TbNovisitados.Text = "No-Visitados"
        Me.TbNovisitados.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button2.Location = New System.Drawing.Point(510, 46)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 34)
        Me.Button2.TabIndex = 40
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(510, 14)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 39
        Me.Button3.Text = "Consulta"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(769, 38)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(320, 21)
        Me.DateTimePicker1.TabIndex = 38
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(622, 45)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(89, 15)
        Me.Label16.TabIndex = 37
        Me.Label16.Text = "Fecha termino:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(4, 120)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 20
        Me.DataGridView1.Size = New System.Drawing.Size(1118, 469)
        Me.DataGridView1.TabIndex = 36
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(12, 93)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(110, 15)
        Me.Label17.TabIndex = 35
        Me.Label17.Text = "Detalle de registro."
        '
        'CBagenteMARNO
        '
        Me.CBagenteMARNO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.CBagenteMARNO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBagenteMARNO.FormattingEnabled = True
        Me.CBagenteMARNO.Location = New System.Drawing.Point(159, 13)
        Me.CBagenteMARNO.Name = "CBagenteMARNO"
        Me.CBagenteMARNO.Size = New System.Drawing.Size(320, 23)
        Me.CBagenteMARNO.TabIndex = 32
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(12, 15)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(123, 15)
        Me.Label18.TabIndex = 29
        Me.Label18.Text = "Agente de Marketing:"
        '
        'CBAgenteVeNO
        '
        Me.CBAgenteVeNO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.CBAgenteVeNO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBAgenteVeNO.FormattingEnabled = True
        Me.CBAgenteVeNO.Location = New System.Drawing.Point(159, 42)
        Me.CBAgenteVeNO.Name = "CBAgenteVeNO"
        Me.CBAgenteVeNO.Size = New System.Drawing.Size(320, 23)
        Me.CBAgenteVeNO.TabIndex = 33
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(12, 46)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(105, 15)
        Me.Label19.TabIndex = 30
        Me.Label19.Text = "Agente de Ventas:"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(769, 11)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(320, 21)
        Me.DateTimePicker2.TabIndex = 34
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(622, 18)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(76, 15)
        Me.Label20.TabIndex = 31
        Me.Label20.Text = "Fecha Inicio:"
        '
        'frmVisita_Agente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1150, 653)
        Me.Controls.Add(Me.tcVisitas)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmVisita_Agente"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Registros de visitas"
        Me.tpUsuario.ResumeLayout(False)
        Me.tpUsuario.PerformLayout()
        CType(Me.dgvDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbxAcciones.ResumeLayout(False)
        Me.tcVisitas.ResumeLayout(False)
        Me.tpSurpevisor.ResumeLayout(False)
        Me.tpSurpevisor.PerformLayout()
        CType(Me.DgvRegistroCom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TbNovisitados.ResumeLayout(False)
        Me.TbNovisitados.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tpUsuario As TabPage
    Friend WithEvents dgvDetalle As DataGridView
    Friend WithEvents txtUbicacion As TextBox
    Friend WithEvents txtComentario As TextBox
    Friend WithEvents txtLocalidad As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbAgente_Ventas As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents gbxAcciones As GroupBox
    Friend WithEvents btnVer As Button
    Friend WithEvents btnAgregar As Button
    Friend WithEvents dtpFecha_Visita As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents cmbCliente As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents cbxOtro As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cbxCobranza As CheckBox
    Friend WithEvents cbxPedido As CheckBox
    Friend WithEvents tcVisitas As TabControl
    Friend WithEvents tpSurpevisor As TabPage
    Friend WithEvents cmbAgente_Marketing As ComboBox
    Friend WithEvents txtCardCode As TextBox
    Friend WithEvents DgvRegistroCom As DataGridView
    Friend WithEvents Label14 As Label
    Friend WithEvents CBAgenteM As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents CBAgenteV As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents DTPInicio As DateTimePicker
    Friend WithEvents Label13 As Label
    Friend WithEvents DTPTermino As DateTimePicker
    Friend WithEvents Label15 As Label
    Friend WithEvents BtnBuscar As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents lblVisista As Label
    Friend WithEvents DTPHORA As DateTimePicker
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents TbNovisitados As TabPage
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label16 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label17 As Label
    Friend WithEvents CBagenteMARNO As ComboBox
    Friend WithEvents Label18 As Label
    Friend WithEvents CBAgenteVeNO As ComboBox
    Friend WithEvents Label19 As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label20 As Label
End Class
