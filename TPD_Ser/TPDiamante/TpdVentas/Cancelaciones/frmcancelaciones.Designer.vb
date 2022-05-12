<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmcancelaciones
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmcancelaciones))
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
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.dtpfecha_ini = New System.Windows.Forms.DateTimePicker()
    Me.dtpfecha_fin = New System.Windows.Forms.DateTimePicker()
    Me.cmbstatus = New System.Windows.Forms.ComboBox()
    Me.cmbusuarios = New System.Windows.Forms.ComboBox()
    Me.btnconsultar = New System.Windows.Forms.Button()
    Me.btnexportar = New System.Windows.Forms.Button()
    Me.gbacciones = New System.Windows.Forms.GroupBox()
    Me.btnAutorizaContab = New System.Windows.Forms.Button()
    Me.btnrefacturaciones = New System.Windows.Forms.Button()
    Me.btncancelaciones = New System.Windows.Forms.Button()
    Me.btnautorizaciones = New System.Windows.Forms.Button()
    Me.btnsolicitud = New System.Windows.Forms.Button()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.cmbalmacen = New System.Windows.Forms.ComboBox()
    Me.dgvcancelaciones = New System.Windows.Forms.DataGridView()
    Me.Estatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Usuario = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Factura = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.FechaFactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.FechaSolicitud = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Motivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Comentario = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Refactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Almacen = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.NC = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.FechaNC = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.CardCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.CardName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Comments_sap = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.panel_cancelaciones = New System.Windows.Forms.Panel()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label18 = New System.Windows.Forms.Label()
    Me.Label17 = New System.Windows.Forms.Label()
    Me.Label16 = New System.Windows.Forms.Label()
    Me.Label15 = New System.Windows.Forms.Label()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.gbacciones.SuspendLayout()
    CType(Me.dgvcancelaciones, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.panel_cancelaciones.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'DataGridViewTextBoxColumn1
    '
    Me.DataGridViewTextBoxColumn1.Frozen = True
    Me.DataGridViewTextBoxColumn1.HeaderText = "Usuario"
    Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
    Me.DataGridViewTextBoxColumn1.ReadOnly = True
    '
    'DataGridViewTextBoxColumn2
    '
    Me.DataGridViewTextBoxColumn2.Frozen = True
    Me.DataGridViewTextBoxColumn2.HeaderText = "Factura"
    Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
    Me.DataGridViewTextBoxColumn2.ReadOnly = True
    '
    'DataGridViewTextBoxColumn3
    '
    Me.DataGridViewTextBoxColumn3.Frozen = True
    Me.DataGridViewTextBoxColumn3.HeaderText = "Fecha de factura"
    Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
    Me.DataGridViewTextBoxColumn3.ReadOnly = True
    '
    'DataGridViewTextBoxColumn4
    '
    Me.DataGridViewTextBoxColumn4.HeaderText = "Fecha de solicitud"
    Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
    Me.DataGridViewTextBoxColumn4.ReadOnly = True
    '
    'DataGridViewTextBoxColumn5
    '
    Me.DataGridViewTextBoxColumn5.HeaderText = "Motivo"
    Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
    Me.DataGridViewTextBoxColumn5.ReadOnly = True
    '
    'DataGridViewTextBoxColumn6
    '
    Me.DataGridViewTextBoxColumn6.HeaderText = "Comentario"
    Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
    Me.DataGridViewTextBoxColumn6.ReadOnly = True
    '
    'DataGridViewTextBoxColumn7
    '
    Me.DataGridViewTextBoxColumn7.HeaderText = "Re-factura"
    Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
    Me.DataGridViewTextBoxColumn7.ReadOnly = True
    '
    'DataGridViewTextBoxColumn8
    '
    Me.DataGridViewTextBoxColumn8.HeaderText = "Almacen"
    Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
    Me.DataGridViewTextBoxColumn8.ReadOnly = True
    '
    'DataGridViewTextBoxColumn9
    '
    Me.DataGridViewTextBoxColumn9.HeaderText = "Nota de Credito"
    Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
    Me.DataGridViewTextBoxColumn9.ReadOnly = True
    '
    'DataGridViewTextBoxColumn10
    '
    Me.DataGridViewTextBoxColumn10.HeaderText = "Fecha NC"
    Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
    Me.DataGridViewTextBoxColumn10.ReadOnly = True
    '
    'DataGridViewTextBoxColumn11
    '
    Me.DataGridViewTextBoxColumn11.HeaderText = "Estatus"
    Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
    Me.DataGridViewTextBoxColumn11.ReadOnly = True
    '
    'DataGridViewTextBoxColumn12
    '
    Me.DataGridViewTextBoxColumn12.HeaderText = "Cliente"
    Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
    Me.DataGridViewTextBoxColumn12.ReadOnly = True
    '
    'DataGridViewTextBoxColumn13
    '
    Me.DataGridViewTextBoxColumn13.HeaderText = "Nombre Cli."
    Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
    Me.DataGridViewTextBoxColumn13.ReadOnly = True
    '
    'DataGridViewTextBoxColumn14
    '
    Me.DataGridViewTextBoxColumn14.HeaderText = "Comentario SAP"
    Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
    Me.DataGridViewTextBoxColumn14.ReadOnly = True
    '
    'DataGridViewTextBoxColumn15
    '
    Me.DataGridViewTextBoxColumn15.HeaderText = "Total"
    Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
    Me.DataGridViewTextBoxColumn15.ReadOnly = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(10, 9)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(303, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Introducir los datos requeridos en el formulario para la consulta."
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(10, 46)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(69, 13)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "Fecha inicial:"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(10, 73)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(62, 13)
    Me.Label3.TabIndex = 2
    Me.Label3.Text = "Fecha final:"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(10, 103)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(76, 13)
    Me.Label4.TabIndex = 3
    Me.Label4.Text = "Status factura:"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(10, 133)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(46, 13)
    Me.Label5.TabIndex = 4
    Me.Label5.Text = "Usuario:"
    '
    'dtpfecha_ini
    '
    Me.dtpfecha_ini.Location = New System.Drawing.Point(117, 40)
    Me.dtpfecha_ini.Name = "dtpfecha_ini"
    Me.dtpfecha_ini.Size = New System.Drawing.Size(262, 20)
    Me.dtpfecha_ini.TabIndex = 5
    '
    'dtpfecha_fin
    '
    Me.dtpfecha_fin.Location = New System.Drawing.Point(117, 67)
    Me.dtpfecha_fin.Name = "dtpfecha_fin"
    Me.dtpfecha_fin.Size = New System.Drawing.Size(262, 20)
    Me.dtpfecha_fin.TabIndex = 6
    '
    'cmbstatus
    '
    Me.cmbstatus.AutoCompleteCustomSource.AddRange(New String() {"TODOS", "PENDIENTE AUTORIZACION", "PROCESO DE CANCELACION", "NO PROCEDE", "FINALIZADO"})
    Me.cmbstatus.FormattingEnabled = True
    Me.cmbstatus.Items.AddRange(New Object() {"TODOS", "PENDIENTE AUTORIZACION", "EN PROCESO DE CANCELACION", "PENDIENTE REFACTURACION", "NO PROCEDE", "FINALIZADO"})
    Me.cmbstatus.Location = New System.Drawing.Point(117, 100)
    Me.cmbstatus.Name = "cmbstatus"
    Me.cmbstatus.Size = New System.Drawing.Size(262, 21)
    Me.cmbstatus.TabIndex = 7
    '
    'cmbusuarios
    '
    Me.cmbusuarios.FormattingEnabled = True
    Me.cmbusuarios.Location = New System.Drawing.Point(117, 130)
    Me.cmbusuarios.Name = "cmbusuarios"
    Me.cmbusuarios.Size = New System.Drawing.Size(262, 21)
    Me.cmbusuarios.TabIndex = 8
    '
    'btnconsultar
    '
    Me.btnconsultar.Image = Global.TPDiamante.My.Resources.Resources.file_find
    Me.btnconsultar.Location = New System.Drawing.Point(465, 90)
    Me.btnconsultar.Name = "btnconsultar"
    Me.btnconsultar.Size = New System.Drawing.Size(75, 59)
    Me.btnconsultar.TabIndex = 9
    Me.btnconsultar.Text = "Consultar"
    Me.btnconsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btnconsultar.UseVisualStyleBackColor = True
    '
    'btnexportar
    '
    Me.btnexportar.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
    Me.btnexportar.Location = New System.Drawing.Point(554, 90)
    Me.btnexportar.Name = "btnexportar"
    Me.btnexportar.Size = New System.Drawing.Size(75, 59)
    Me.btnexportar.TabIndex = 10
    Me.btnexportar.Text = "Exportar"
    Me.btnexportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btnexportar.UseVisualStyleBackColor = True
    '
    'gbacciones
    '
    Me.gbacciones.Controls.Add(Me.Button1)
    Me.gbacciones.Controls.Add(Me.btnAutorizaContab)
    Me.gbacciones.Controls.Add(Me.btnrefacturaciones)
    Me.gbacciones.Controls.Add(Me.btncancelaciones)
    Me.gbacciones.Controls.Add(Me.btnautorizaciones)
    Me.gbacciones.Controls.Add(Me.btnsolicitud)
    Me.gbacciones.Dock = System.Windows.Forms.DockStyle.Right
    Me.gbacciones.Location = New System.Drawing.Point(1004, 0)
    Me.gbacciones.Name = "gbacciones"
    Me.gbacciones.Size = New System.Drawing.Size(153, 262)
    Me.gbacciones.TabIndex = 11
    Me.gbacciones.TabStop = False
    Me.gbacciones.Text = "Acciones"
    '
    'btnAutorizaContab
    '
    Me.btnAutorizaContab.Location = New System.Drawing.Point(27, 78)
    Me.btnAutorizaContab.Name = "btnAutorizaContab"
    Me.btnAutorizaContab.Size = New System.Drawing.Size(98, 34)
    Me.btnAutorizaContab.TabIndex = 4
    Me.btnAutorizaContab.Text = "Autorización Contabilidad"
    Me.btnAutorizaContab.UseVisualStyleBackColor = True
    '
    'btnrefacturaciones
    '
    Me.btnrefacturaciones.Location = New System.Drawing.Point(27, 207)
    Me.btnrefacturaciones.Name = "btnrefacturaciones"
    Me.btnrefacturaciones.Size = New System.Drawing.Size(98, 35)
    Me.btnrefacturaciones.TabIndex = 3
    Me.btnrefacturaciones.Text = "Refacturaciones"
    Me.btnrefacturaciones.UseVisualStyleBackColor = True
    '
    'btncancelaciones
    '
    Me.btncancelaciones.Location = New System.Drawing.Point(27, 164)
    Me.btncancelaciones.Name = "btncancelaciones"
    Me.btncancelaciones.Size = New System.Drawing.Size(98, 35)
    Me.btncancelaciones.TabIndex = 2
    Me.btncancelaciones.Text = "Cancelaciones"
    Me.btncancelaciones.UseVisualStyleBackColor = True
    '
    'btnautorizaciones
    '
    Me.btnautorizaciones.Location = New System.Drawing.Point(27, 121)
    Me.btnautorizaciones.Name = "btnautorizaciones"
    Me.btnautorizaciones.Size = New System.Drawing.Size(98, 35)
    Me.btnautorizaciones.TabIndex = 1
    Me.btnautorizaciones.Text = "Autorización de Solicitud"
    Me.btnautorizaciones.UseVisualStyleBackColor = True
    '
    'btnsolicitud
    '
    Me.btnsolicitud.Location = New System.Drawing.Point(27, 35)
    Me.btnsolicitud.Name = "btnsolicitud"
    Me.btnsolicitud.Size = New System.Drawing.Size(98, 35)
    Me.btnsolicitud.TabIndex = 0
    Me.btnsolicitud.Text = "Solicitud de Cancelación"
    Me.btnsolicitud.UseVisualStyleBackColor = True
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(395, 46)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(51, 13)
    Me.Label6.TabIndex = 12
    Me.Label6.Text = "Almacén:"
    '
    'cmbalmacen
    '
    Me.cmbalmacen.FormattingEnabled = True
    Me.cmbalmacen.Location = New System.Drawing.Point(465, 43)
    Me.cmbalmacen.Name = "cmbalmacen"
    Me.cmbalmacen.Size = New System.Drawing.Size(164, 21)
    Me.cmbalmacen.TabIndex = 11
    '
    'dgvcancelaciones
    '
    Me.dgvcancelaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvcancelaciones.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Estatus, Me.Usuario, Me.Factura, Me.FechaFactura, Me.FechaSolicitud, Me.Motivo, Me.Comentario, Me.Refactura, Me.Almacen, Me.NC, Me.FechaNC, Me.CardCode, Me.CardName, Me.Comments_sap, Me.Total})
    Me.dgvcancelaciones.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvcancelaciones.Location = New System.Drawing.Point(0, 0)
    Me.dgvcancelaciones.Name = "dgvcancelaciones"
    Me.dgvcancelaciones.ReadOnly = True
    Me.dgvcancelaciones.RowHeadersWidth = 20
    Me.dgvcancelaciones.Size = New System.Drawing.Size(1153, 350)
    Me.dgvcancelaciones.TabIndex = 13
    '
    'Estatus
    '
    Me.Estatus.Frozen = True
    Me.Estatus.HeaderText = "Estatus"
    Me.Estatus.Name = "Estatus"
    Me.Estatus.ReadOnly = True
    '
    'Usuario
    '
    Me.Usuario.Frozen = True
    Me.Usuario.HeaderText = "Usuario"
    Me.Usuario.Name = "Usuario"
    Me.Usuario.ReadOnly = True
    '
    'Factura
    '
    Me.Factura.Frozen = True
    Me.Factura.HeaderText = "Factura"
    Me.Factura.Name = "Factura"
    Me.Factura.ReadOnly = True
    '
    'FechaFactura
    '
    Me.FechaFactura.Frozen = True
    Me.FechaFactura.HeaderText = "Fecha de factura"
    Me.FechaFactura.Name = "FechaFactura"
    Me.FechaFactura.ReadOnly = True
    '
    'FechaSolicitud
    '
    Me.FechaSolicitud.HeaderText = "Fecha de solicitud"
    Me.FechaSolicitud.Name = "FechaSolicitud"
    Me.FechaSolicitud.ReadOnly = True
    '
    'Motivo
    '
    Me.Motivo.HeaderText = "Motivo"
    Me.Motivo.Name = "Motivo"
    Me.Motivo.ReadOnly = True
    '
    'Comentario
    '
    Me.Comentario.HeaderText = "Comentario"
    Me.Comentario.Name = "Comentario"
    Me.Comentario.ReadOnly = True
    '
    'Refactura
    '
    Me.Refactura.HeaderText = "Re-factura"
    Me.Refactura.Name = "Refactura"
    Me.Refactura.ReadOnly = True
    '
    'Almacen
    '
    Me.Almacen.HeaderText = "Almacen"
    Me.Almacen.Name = "Almacen"
    Me.Almacen.ReadOnly = True
    '
    'NC
    '
    Me.NC.HeaderText = "Nota de Credito"
    Me.NC.Name = "NC"
    Me.NC.ReadOnly = True
    '
    'FechaNC
    '
    Me.FechaNC.HeaderText = "Fecha NC"
    Me.FechaNC.Name = "FechaNC"
    Me.FechaNC.ReadOnly = True
    '
    'CardCode
    '
    Me.CardCode.HeaderText = "Cliente"
    Me.CardCode.Name = "CardCode"
    Me.CardCode.ReadOnly = True
    '
    'CardName
    '
    Me.CardName.HeaderText = "Nombre Cli."
    Me.CardName.Name = "CardName"
    Me.CardName.ReadOnly = True
    '
    'Comments_sap
    '
    Me.Comments_sap.HeaderText = "Comentario SAP"
    Me.Comments_sap.Name = "Comments_sap"
    Me.Comments_sap.ReadOnly = True
    '
    'Total
    '
    Me.Total.HeaderText = "Total"
    Me.Total.Name = "Total"
    Me.Total.ReadOnly = True
    '
    'panel_cancelaciones
    '
    Me.panel_cancelaciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.panel_cancelaciones.Controls.Add(Me.Panel2)
    Me.panel_cancelaciones.Controls.Add(Me.Panel1)
    Me.panel_cancelaciones.Dock = System.Windows.Forms.DockStyle.Fill
    Me.panel_cancelaciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.panel_cancelaciones.Location = New System.Drawing.Point(0, 0)
    Me.panel_cancelaciones.Name = "panel_cancelaciones"
    Me.panel_cancelaciones.Size = New System.Drawing.Size(1161, 620)
    Me.panel_cancelaciones.TabIndex = 0
    '
    'Panel2
    '
    Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel2.Controls.Add(Me.dgvcancelaciones)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel2.Location = New System.Drawing.Point(0, 262)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(1157, 354)
    Me.Panel2.TabIndex = 27
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.Label18)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Controls.Add(Me.Label17)
    Me.Panel1.Controls.Add(Me.Label3)
    Me.Panel1.Controls.Add(Me.Label16)
    Me.Panel1.Controls.Add(Me.Label4)
    Me.Panel1.Controls.Add(Me.Label15)
    Me.Panel1.Controls.Add(Me.Label5)
    Me.Panel1.Controls.Add(Me.Label14)
    Me.Panel1.Controls.Add(Me.dtpfecha_ini)
    Me.Panel1.Controls.Add(Me.Label13)
    Me.Panel1.Controls.Add(Me.dtpfecha_fin)
    Me.Panel1.Controls.Add(Me.Label12)
    Me.Panel1.Controls.Add(Me.cmbstatus)
    Me.Panel1.Controls.Add(Me.Label11)
    Me.Panel1.Controls.Add(Me.cmbusuarios)
    Me.Panel1.Controls.Add(Me.Label10)
    Me.Panel1.Controls.Add(Me.btnconsultar)
    Me.Panel1.Controls.Add(Me.Label9)
    Me.Panel1.Controls.Add(Me.btnexportar)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.gbacciones)
    Me.Panel1.Controls.Add(Me.Label7)
    Me.Panel1.Controls.Add(Me.Label6)
    Me.Panel1.Controls.Add(Me.cmbalmacen)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(1157, 262)
    Me.Panel1.TabIndex = 26
    '
    'Label18
    '
    Me.Label18.BackColor = System.Drawing.Color.LightSteelBlue
    Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Label18.Location = New System.Drawing.Point(459, 172)
    Me.Label18.Name = "Label18"
    Me.Label18.Size = New System.Drawing.Size(113, 13)
    Me.Label18.TabIndex = 25
    Me.Label18.Visible = False
    '
    'Label17
    '
    Me.Label17.AutoSize = True
    Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label17.Location = New System.Drawing.Point(308, 173)
    Me.Label17.Name = "Label17"
    Me.Label17.Size = New System.Drawing.Size(145, 12)
    Me.Label17.TabIndex = 24
    Me.Label17.Text = "EN AUTORIZACION CONTABLE"
    Me.Label17.Visible = False
    '
    'Label16
    '
    Me.Label16.AutoSize = True
    Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label16.Location = New System.Drawing.Point(11, 235)
    Me.Label16.Name = "Label16"
    Me.Label16.Size = New System.Drawing.Size(67, 12)
    Me.Label16.TabIndex = 23
    Me.Label16.Text = "NO PROCEDE"
    Me.Label16.Visible = False
    '
    'Label15
    '
    Me.Label15.AutoSize = True
    Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label15.Location = New System.Drawing.Point(11, 219)
    Me.Label15.Name = "Label15"
    Me.Label15.Size = New System.Drawing.Size(63, 12)
    Me.Label15.TabIndex = 22
    Me.Label15.Text = "FINALIZADO"
    Me.Label15.Visible = False
    '
    'Label14
    '
    Me.Label14.BackColor = System.Drawing.Color.Tomato
    Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Label14.Location = New System.Drawing.Point(171, 237)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(113, 13)
    Me.Label14.TabIndex = 21
    Me.Label14.Visible = False
    '
    'Label13
    '
    Me.Label13.BackColor = System.Drawing.Color.OliveDrab
    Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Label13.Location = New System.Drawing.Point(171, 221)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(113, 13)
    Me.Label13.TabIndex = 20
    Me.Label13.Visible = False
    '
    'Label12
    '
    Me.Label12.BackColor = System.Drawing.Color.Orange
    Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Label12.Location = New System.Drawing.Point(171, 204)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(113, 13)
    Me.Label12.TabIndex = 19
    Me.Label12.Visible = False
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label11.Location = New System.Drawing.Point(10, 205)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(143, 12)
    Me.Label11.TabIndex = 18
    Me.Label11.Text = "PENDIENTE REFACTURACIÓN"
    Me.Label11.Visible = False
    '
    'Label10
    '
    Me.Label10.BackColor = System.Drawing.Color.Khaki
    Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Label10.Location = New System.Drawing.Point(171, 188)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(113, 13)
    Me.Label10.TabIndex = 17
    Me.Label10.Visible = False
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label9.Location = New System.Drawing.Point(10, 190)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(153, 12)
    Me.Label9.TabIndex = 16
    Me.Label9.Text = "EN PROCESO DE CANCELACIÓN"
    Me.Label9.Visible = False
    '
    'Label8
    '
    Me.Label8.BackColor = System.Drawing.Color.White
    Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Label8.Location = New System.Drawing.Point(171, 172)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(113, 13)
    Me.Label8.TabIndex = 15
    Me.Label8.Visible = False
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label7.Location = New System.Drawing.Point(10, 174)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(133, 12)
    Me.Label7.TabIndex = 14
    Me.Label7.Text = "PENDIENTE AUTORIZACIÓN"
    Me.Label7.Visible = False
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(144, 16)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(8, 9)
    Me.Button1.TabIndex = 5
    Me.Button1.Text = "Button1"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'frmcancelaciones
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1161, 620)
    Me.Controls.Add(Me.panel_cancelaciones)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmcancelaciones"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Cancelaciones"
    Me.gbacciones.ResumeLayout(False)
    CType(Me.dgvcancelaciones, System.ComponentModel.ISupportInitialize).EndInit()
    Me.panel_cancelaciones.ResumeLayout(False)
    Me.Panel2.ResumeLayout(False)
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
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
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpfecha_ini As DateTimePicker
    Friend WithEvents dtpfecha_fin As DateTimePicker
    Friend WithEvents cmbstatus As ComboBox
    Friend WithEvents cmbusuarios As ComboBox
    Friend WithEvents btnconsultar As Button
    Friend WithEvents btnexportar As Button
    Friend WithEvents gbacciones As GroupBox
    Friend WithEvents btnrefacturaciones As Button
    Friend WithEvents btncancelaciones As Button
    Friend WithEvents btnautorizaciones As Button
    Friend WithEvents btnsolicitud As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbalmacen As ComboBox
    Friend WithEvents dgvcancelaciones As DataGridView
    Friend WithEvents panel_cancelaciones As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Estatus As DataGridViewTextBoxColumn
    Friend WithEvents Usuario As DataGridViewTextBoxColumn
    Friend WithEvents Factura As DataGridViewTextBoxColumn
    Friend WithEvents FechaFactura As DataGridViewTextBoxColumn
    Friend WithEvents FechaSolicitud As DataGridViewTextBoxColumn
    Friend WithEvents Motivo As DataGridViewTextBoxColumn
    Friend WithEvents Comentario As DataGridViewTextBoxColumn
    Friend WithEvents Refactura As DataGridViewTextBoxColumn
    Friend WithEvents Almacen As DataGridViewTextBoxColumn
    Friend WithEvents NC As DataGridViewTextBoxColumn
    Friend WithEvents FechaNC As DataGridViewTextBoxColumn
    Friend WithEvents CardCode As DataGridViewTextBoxColumn
    Friend WithEvents CardName As DataGridViewTextBoxColumn
    Friend WithEvents Comments_sap As DataGridViewTextBoxColumn
    Friend WithEvents Total As DataGridViewTextBoxColumn
    Friend WithEvents btnAutorizaContab As Button
    Friend WithEvents Label18 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
  Friend WithEvents Button1 As Button
End Class
