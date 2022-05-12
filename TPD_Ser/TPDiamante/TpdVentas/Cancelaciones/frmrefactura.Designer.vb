<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmrefactura
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmrefactura))
        Me.panel_autoriza = New System.Windows.Forms.Panel()
        Me.btnrefrescar = New System.Windows.Forms.Button()
        Me.gbdatos_factura = New System.Windows.Forms.GroupBox()
        Me.txtnc = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtref = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.brnguardar = New System.Windows.Forms.Button()
        Me.txtstatus = New System.Windows.Forms.TextBox()
        Me.txtalmacen = New System.Windows.Forms.TextBox()
        Me.txtrefactura = New System.Windows.Forms.TextBox()
        Me.txtcomentario = New System.Windows.Forms.TextBox()
        Me.txtmotivo = New System.Windows.Forms.TextBox()
        Me.txtfecha_solicitud = New System.Windows.Forms.TextBox()
        Me.txtfecha_factura = New System.Windows.Forms.TextBox()
        Me.txtsolicita = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnmostrar = New System.Windows.Forms.Button()
        Me.txtfactura = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgvrefactura = New System.Windows.Forms.DataGridView()
        Me.Usuario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Factura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaFactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaCancela = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Motivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Comentarios = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Refactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sustituye = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Almacen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NotaCredito = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaNC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnbuscar = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtcom_refactura = New System.Windows.Forms.TextBox()
        Me.panel_autoriza.SuspendLayout()
        Me.gbdatos_factura.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvrefactura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panel_autoriza
        '
        Me.panel_autoriza.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panel_autoriza.Controls.Add(Me.btnrefrescar)
        Me.panel_autoriza.Controls.Add(Me.gbdatos_factura)
        Me.panel_autoriza.Controls.Add(Me.dgvrefactura)
        Me.panel_autoriza.Controls.Add(Me.Label1)
        Me.panel_autoriza.Location = New System.Drawing.Point(12, 12)
        Me.panel_autoriza.Name = "panel_autoriza"
        Me.panel_autoriza.Size = New System.Drawing.Size(903, 596)
        Me.panel_autoriza.TabIndex = 0
        '
        'btnrefrescar
        '
        Me.btnrefrescar.Image = Global.TPDiamante.My.Resources.Resources.Refresh_B
        Me.btnrefrescar.Location = New System.Drawing.Point(841, 4)
        Me.btnrefrescar.Name = "btnrefrescar"
        Me.btnrefrescar.Size = New System.Drawing.Size(44, 43)
        Me.btnrefrescar.TabIndex = 5
        Me.btnrefrescar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnrefrescar, "Actualizar la vista de Facturas pendientes.")
        Me.btnrefrescar.UseVisualStyleBackColor = True
        '
        'gbdatos_factura
        '
        Me.gbdatos_factura.Controls.Add(Me.txtnc)
        Me.gbdatos_factura.Controls.Add(Me.Label13)
        Me.gbdatos_factura.Controls.Add(Me.GroupBox1)
        Me.gbdatos_factura.Controls.Add(Me.txtstatus)
        Me.gbdatos_factura.Controls.Add(Me.txtalmacen)
        Me.gbdatos_factura.Controls.Add(Me.txtrefactura)
        Me.gbdatos_factura.Controls.Add(Me.txtcomentario)
        Me.gbdatos_factura.Controls.Add(Me.txtmotivo)
        Me.gbdatos_factura.Controls.Add(Me.txtfecha_solicitud)
        Me.gbdatos_factura.Controls.Add(Me.txtfecha_factura)
        Me.gbdatos_factura.Controls.Add(Me.txtsolicita)
        Me.gbdatos_factura.Controls.Add(Me.Label11)
        Me.gbdatos_factura.Controls.Add(Me.Label10)
        Me.gbdatos_factura.Controls.Add(Me.Label9)
        Me.gbdatos_factura.Controls.Add(Me.Label8)
        Me.gbdatos_factura.Controls.Add(Me.Label7)
        Me.gbdatos_factura.Controls.Add(Me.Label6)
        Me.gbdatos_factura.Controls.Add(Me.Label5)
        Me.gbdatos_factura.Controls.Add(Me.Label4)
        Me.gbdatos_factura.Controls.Add(Me.Label3)
        Me.gbdatos_factura.Controls.Add(Me.btnmostrar)
        Me.gbdatos_factura.Controls.Add(Me.txtfactura)
        Me.gbdatos_factura.Controls.Add(Me.Label2)
        Me.gbdatos_factura.Location = New System.Drawing.Point(16, 319)
        Me.gbdatos_factura.Name = "gbdatos_factura"
        Me.gbdatos_factura.Size = New System.Drawing.Size(869, 262)
        Me.gbdatos_factura.TabIndex = 4
        Me.gbdatos_factura.TabStop = False
        Me.gbdatos_factura.Text = "Datos de factura  de asignación de Refactura"
        '
        'txtnc
        '
        Me.txtnc.Enabled = False
        Me.txtnc.Location = New System.Drawing.Point(424, 82)
        Me.txtnc.Name = "txtnc"
        Me.txtnc.Size = New System.Drawing.Size(132, 20)
        Me.txtnc.TabIndex = 26
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(370, 85)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(25, 13)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "NC:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtcom_refactura)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.btnbuscar)
        Me.GroupBox1.Controls.Add(Me.txtref)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.brnguardar)
        Me.GroupBox1.Location = New System.Drawing.Point(593, 19)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(270, 226)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sustituye"
        '
        'txtref
        '
        Me.txtref.Location = New System.Drawing.Point(87, 27)
        Me.txtref.Name = "txtref"
        Me.txtref.Size = New System.Drawing.Size(126, 20)
        Me.txtref.TabIndex = 23
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 30)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 13)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "Refactura:"
        '
        'brnguardar
        '
        Me.brnguardar.Enabled = False
        Me.brnguardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.brnguardar.Image = Global.TPDiamante.My.Resources.Resources.kate
        Me.brnguardar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.brnguardar.Location = New System.Drawing.Point(98, 175)
        Me.brnguardar.Name = "brnguardar"
        Me.brnguardar.Size = New System.Drawing.Size(90, 38)
        Me.brnguardar.TabIndex = 24
        Me.brnguardar.Text = "Guardar"
        Me.brnguardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.brnguardar, "Actualiza la factura en sus diferentes estatus.")
        Me.brnguardar.UseVisualStyleBackColor = True
        '
        'txtstatus
        '
        Me.txtstatus.Enabled = False
        Me.txtstatus.Location = New System.Drawing.Point(424, 165)
        Me.txtstatus.Multiline = True
        Me.txtstatus.Name = "txtstatus"
        Me.txtstatus.Size = New System.Drawing.Size(132, 43)
        Me.txtstatus.TabIndex = 21
        '
        'txtalmacen
        '
        Me.txtalmacen.Enabled = False
        Me.txtalmacen.Location = New System.Drawing.Point(424, 137)
        Me.txtalmacen.Name = "txtalmacen"
        Me.txtalmacen.Size = New System.Drawing.Size(132, 20)
        Me.txtalmacen.TabIndex = 20
        '
        'txtrefactura
        '
        Me.txtrefactura.Enabled = False
        Me.txtrefactura.Location = New System.Drawing.Point(424, 108)
        Me.txtrefactura.Name = "txtrefactura"
        Me.txtrefactura.Size = New System.Drawing.Size(132, 20)
        Me.txtrefactura.TabIndex = 19
        '
        'txtcomentario
        '
        Me.txtcomentario.Enabled = False
        Me.txtcomentario.Location = New System.Drawing.Point(118, 194)
        Me.txtcomentario.Multiline = True
        Me.txtcomentario.Name = "txtcomentario"
        Me.txtcomentario.Size = New System.Drawing.Size(240, 51)
        Me.txtcomentario.TabIndex = 18
        '
        'txtmotivo
        '
        Me.txtmotivo.Enabled = False
        Me.txtmotivo.Location = New System.Drawing.Point(118, 166)
        Me.txtmotivo.Name = "txtmotivo"
        Me.txtmotivo.Size = New System.Drawing.Size(240, 20)
        Me.txtmotivo.TabIndex = 17
        '
        'txtfecha_solicitud
        '
        Me.txtfecha_solicitud.Enabled = False
        Me.txtfecha_solicitud.Location = New System.Drawing.Point(118, 138)
        Me.txtfecha_solicitud.Name = "txtfecha_solicitud"
        Me.txtfecha_solicitud.Size = New System.Drawing.Size(240, 20)
        Me.txtfecha_solicitud.TabIndex = 16
        '
        'txtfecha_factura
        '
        Me.txtfecha_factura.Enabled = False
        Me.txtfecha_factura.Location = New System.Drawing.Point(118, 108)
        Me.txtfecha_factura.Name = "txtfecha_factura"
        Me.txtfecha_factura.Size = New System.Drawing.Size(240, 20)
        Me.txtfecha_factura.TabIndex = 15
        '
        'txtsolicita
        '
        Me.txtsolicita.Enabled = False
        Me.txtsolicita.Location = New System.Drawing.Point(118, 82)
        Me.txtsolicita.Name = "txtsolicita"
        Me.txtsolicita.Size = New System.Drawing.Size(240, 20)
        Me.txtsolicita.TabIndex = 14
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(370, 168)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "Estatus:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(370, 140)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(51, 13)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "Almacén:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(370, 115)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Refactura:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 195)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Comentario:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 169)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Motivo:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 141)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Fecha de Solicitud:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Fecha factura:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Solicita:"
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label3.Location = New System.Drawing.Point(12, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(249, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "________________________________________"
        '
        'btnmostrar
        '
        Me.btnmostrar.BackgroundImage = Global.TPDiamante.My.Resources.Resources.file_find
        Me.btnmostrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnmostrar.Location = New System.Drawing.Point(189, 35)
        Me.btnmostrar.Name = "btnmostrar"
        Me.btnmostrar.Size = New System.Drawing.Size(29, 27)
        Me.btnmostrar.TabIndex = 4
        Me.btnmostrar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.ToolTip1.SetToolTip(Me.btnmostrar, "Visualizar datos de la factura.")
        Me.btnmostrar.UseVisualStyleBackColor = True
        '
        'txtfactura
        '
        Me.txtfactura.Enabled = False
        Me.txtfactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfactura.Location = New System.Drawing.Point(64, 38)
        Me.txtfactura.Name = "txtfactura"
        Me.txtfactura.Size = New System.Drawing.Size(119, 20)
        Me.txtfactura.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Factura:"
        '
        'dgvrefactura
        '
        Me.dgvrefactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvrefactura.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Usuario, Me.Factura, Me.FechaFactura, Me.FechaCancela, Me.Motivo, Me.Comentarios, Me.Refactura, Me.Sustituye, Me.Almacen, Me.NotaCredito, Me.FechaNC, Me.Status})
        Me.dgvrefactura.Location = New System.Drawing.Point(16, 52)
        Me.dgvrefactura.Name = "dgvrefactura"
        Me.dgvrefactura.ReadOnly = True
        Me.dgvrefactura.RowHeadersWidth = 20
        Me.dgvrefactura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvrefactura.Size = New System.Drawing.Size(869, 244)
        Me.dgvrefactura.TabIndex = 1
        Me.dgvrefactura.TabStop = False
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
        'FechaCancela
        '
        Me.FechaCancela.HeaderText = "Fecha de Solicitud"
        Me.FechaCancela.Name = "FechaCancela"
        Me.FechaCancela.ReadOnly = True
        '
        'Motivo
        '
        Me.Motivo.HeaderText = "Motivo"
        Me.Motivo.Name = "Motivo"
        Me.Motivo.ReadOnly = True
        '
        'Comentarios
        '
        Me.Comentarios.HeaderText = "Comentario"
        Me.Comentarios.Name = "Comentarios"
        Me.Comentarios.ReadOnly = True
        '
        'Refactura
        '
        Me.Refactura.HeaderText = "Requiere refacturación"
        Me.Refactura.Name = "Refactura"
        Me.Refactura.ReadOnly = True
        '
        'Sustituye
        '
        Me.Sustituye.HeaderText = "Sustituye"
        Me.Sustituye.Name = "Sustituye"
        Me.Sustituye.ReadOnly = True
        '
        'Almacen
        '
        Me.Almacen.HeaderText = "Almacén"
        Me.Almacen.Name = "Almacen"
        Me.Almacen.ReadOnly = True
        '
        'NotaCredito
        '
        Me.NotaCredito.HeaderText = "Nota de Credito"
        Me.NotaCredito.Name = "NotaCredito"
        Me.NotaCredito.ReadOnly = True
        '
        'FechaNC
        '
        Me.FechaNC.HeaderText = "Fecha de NC"
        Me.FechaNC.Name = "FechaNC"
        Me.FechaNC.ReadOnly = True
        '
        'Status
        '
        Me.Status.HeaderText = "Estatus"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(326, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Favor de seleccionar la factura requerida para colocar la Refactura."
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Usuario"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Factura"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Fecha de factura"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Fecha de Solicitud"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Motivo"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Comentario"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Almacén"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Nota de Credito"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Fecha de NC"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Estatus"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Refactura"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipTitle = "Visualizar datos"
        '
        'btnbuscar
        '
        Me.btnbuscar.BackgroundImage = Global.TPDiamante.My.Resources.Resources.file_find
        Me.btnbuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnbuscar.Location = New System.Drawing.Point(225, 22)
        Me.btnbuscar.Name = "btnbuscar"
        Me.btnbuscar.Size = New System.Drawing.Size(35, 29)
        Me.btnbuscar.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.btnbuscar, "Buscar refactura.")
        Me.btnbuscar.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 61)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(123, 13)
        Me.Label14.TabIndex = 26
        Me.Label14.Text = "Comentario de refactura:"
        '
        'txtcom_refactura
        '
        Me.txtcom_refactura.Location = New System.Drawing.Point(9, 87)
        Me.txtcom_refactura.Multiline = True
        Me.txtcom_refactura.Name = "txtcom_refactura"
        Me.txtcom_refactura.ReadOnly = True
        Me.txtcom_refactura.Size = New System.Drawing.Size(251, 74)
        Me.txtcom_refactura.TabIndex = 27
        '
        'frmrefactura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(927, 620)
        Me.Controls.Add(Me.panel_autoriza)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmrefactura"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Refacturación"
        Me.panel_autoriza.ResumeLayout(False)
        Me.panel_autoriza.PerformLayout()
        Me.gbdatos_factura.ResumeLayout(False)
        Me.gbdatos_factura.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvrefactura, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panel_autoriza As System.Windows.Forms.Panel
    Friend WithEvents dgvrefactura As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
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
    Friend WithEvents txtfactura As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents gbdatos_factura As System.Windows.Forms.GroupBox
    Friend WithEvents btnmostrar As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtcomentario As System.Windows.Forms.TextBox
    Friend WithEvents txtmotivo As System.Windows.Forms.TextBox
    Friend WithEvents txtfecha_solicitud As System.Windows.Forms.TextBox
    Friend WithEvents txtfecha_factura As System.Windows.Forms.TextBox
    Friend WithEvents txtsolicita As System.Windows.Forms.TextBox
    Friend WithEvents txtalmacen As System.Windows.Forms.TextBox
    Friend WithEvents txtrefactura As System.Windows.Forms.TextBox
    Friend WithEvents brnguardar As System.Windows.Forms.Button
    Friend WithEvents Usuario As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Factura As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaFactura As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaCancela As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Motivo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Comentarios As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Refactura As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sustituye As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Almacen As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NotaCredito As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaNC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnrefrescar As System.Windows.Forms.Button
    Friend WithEvents txtstatus As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtref As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtnc As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnbuscar As Button
    Friend WithEvents txtcom_refactura As TextBox
    Friend WithEvents Label14 As Label
End Class
