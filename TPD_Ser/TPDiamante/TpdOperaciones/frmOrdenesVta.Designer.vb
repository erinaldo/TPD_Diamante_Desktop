<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrdenesVta
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrdenesVta))
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.lblperiodo = New System.Windows.Forms.Label()
        Me.lblfecha = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtbuscar = New System.Windows.Forms.TextBox()
        Me.dgvOperacionOvTA = New System.Windows.Forms.DataGridView()
        Me.dgvOperacionDetalle = New System.Windows.Forms.DataGridView()
        Me.btnActualizarFecha = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TcOrdenVta = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.panelOV = New System.Windows.Forms.Panel()
        Me.txtFolio = New System.Windows.Forms.TextBox()
        Me.txtSerie = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnCancelarOV = New System.Windows.Forms.Button()
        Me.btnPrintOV = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dgvNuevaOV = New System.Windows.Forms.DataGridView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkActualizar = New System.Windows.Forms.CheckBox()
        Me.btnNuevaOrdenVenta = New System.Windows.Forms.Button()
        Me.CBMosctrarCan = New System.Windows.Forms.CheckBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.DgvAutorizaciones = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpHoraF = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpHoraI = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CBAutorizaciones = New System.Windows.Forms.ComboBox()
        Me.CBBuscarUsuario = New System.Windows.Forms.ComboBox()
        Me.LBLbuscar_usuario = New System.Windows.Forms.Label()
        Me.lblBuscar = New System.Windows.Forms.Label()
        Me.txtbuscarautorizaciones = New System.Windows.Forms.TextBox()
        Me.dtpAutorizacion = New System.Windows.Forms.DateTimePicker()
        Me.LblFechaAct = New System.Windows.Forms.Label()
        Me.BtnActualizar_autorizacion = New System.Windows.Forms.Button()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.dgvOperacionOvTA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOperacionDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TcOrdenVta.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.panelOV.SuspendLayout()
        CType(Me.dgvNuevaOV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DgvAutorizaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtpFecha
        '
        Me.dtpFecha.Location = New System.Drawing.Point(598, 8)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(200, 20)
        Me.dtpFecha.TabIndex = 0
        Me.dtpFecha.Visible = False
        '
        'lblperiodo
        '
        Me.lblperiodo.AutoSize = True
        Me.lblperiodo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblperiodo.Location = New System.Drawing.Point(472, 7)
        Me.lblperiodo.Name = "lblperiodo"
        Me.lblperiodo.Size = New System.Drawing.Size(120, 15)
        Me.lblperiodo.TabIndex = 1
        Me.lblperiodo.Text = "Periodo de Fecha"
        Me.lblperiodo.Visible = False
        '
        'lblfecha
        '
        Me.lblfecha.AutoSize = True
        Me.lblfecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfecha.Location = New System.Drawing.Point(804, 11)
        Me.lblfecha.Name = "lblfecha"
        Me.lblfecha.Size = New System.Drawing.Size(42, 13)
        Me.lblfecha.TabIndex = 2
        Me.lblfecha.Text = "Fecha"
        Me.lblfecha.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Buscar Ordenes"
        '
        'txtbuscar
        '
        Me.txtbuscar.Location = New System.Drawing.Point(106, 6)
        Me.txtbuscar.Name = "txtbuscar"
        Me.txtbuscar.Size = New System.Drawing.Size(200, 20)
        Me.txtbuscar.TabIndex = 4
        '
        'dgvOperacionOvTA
        '
        Me.dgvOperacionOvTA.AllowUserToAddRows = False
        Me.dgvOperacionOvTA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOperacionOvTA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvOperacionOvTA.Location = New System.Drawing.Point(0, 0)
        Me.dgvOperacionOvTA.Name = "dgvOperacionOvTA"
        Me.dgvOperacionOvTA.RowHeadersWidth = 20
        Me.dgvOperacionOvTA.Size = New System.Drawing.Size(1120, 568)
        Me.dgvOperacionOvTA.TabIndex = 5
        '
        'dgvOperacionDetalle
        '
        Me.dgvOperacionDetalle.AllowUserToAddRows = False
        Me.dgvOperacionDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOperacionDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvOperacionDetalle.Location = New System.Drawing.Point(0, 0)
        Me.dgvOperacionDetalle.Name = "dgvOperacionDetalle"
        Me.dgvOperacionDetalle.RowHeadersWidth = 20
        Me.dgvOperacionDetalle.Size = New System.Drawing.Size(439, 568)
        Me.dgvOperacionDetalle.TabIndex = 6
        '
        'btnActualizarFecha
        '
        Me.btnActualizarFecha.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnActualizarFecha.Location = New System.Drawing.Point(1484, 0)
        Me.btnActualizarFecha.Name = "btnActualizarFecha"
        Me.btnActualizarFecha.Size = New System.Drawing.Size(75, 35)
        Me.btnActualizarFecha.TabIndex = 7
        Me.btnActualizarFecha.Text = "Actualizar"
        Me.btnActualizarFecha.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'TcOrdenVta
        '
        Me.TcOrdenVta.Controls.Add(Me.TabPage1)
        Me.TcOrdenVta.Controls.Add(Me.TabPage2)
        Me.TcOrdenVta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TcOrdenVta.Location = New System.Drawing.Point(0, 0)
        Me.TcOrdenVta.Name = "TcOrdenVta"
        Me.TcOrdenVta.SelectedIndex = 0
        Me.TcOrdenVta.Size = New System.Drawing.Size(1577, 639)
        Me.TcOrdenVta.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1569, 613)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Operación Venta"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 42)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.panelOV)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvOperacionOvTA)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvOperacionDetalle)
        Me.SplitContainer1.Size = New System.Drawing.Size(1563, 568)
        Me.SplitContainer1.SplitterDistance = 1120
        Me.SplitContainer1.TabIndex = 10
        '
        'panelOV
        '
        Me.panelOV.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelOV.Controls.Add(Me.txtFolio)
        Me.panelOV.Controls.Add(Me.txtSerie)
        Me.panelOV.Controls.Add(Me.Label9)
        Me.panelOV.Controls.Add(Me.Label8)
        Me.panelOV.Controls.Add(Me.btnCancelarOV)
        Me.panelOV.Controls.Add(Me.btnPrintOV)
        Me.panelOV.Controls.Add(Me.Label7)
        Me.panelOV.Controls.Add(Me.Label6)
        Me.panelOV.Controls.Add(Me.dgvNuevaOV)
        Me.panelOV.Controls.Add(Me.Label5)
        Me.panelOV.Location = New System.Drawing.Point(542, 15)
        Me.panelOV.Name = "panelOV"
        Me.panelOV.Size = New System.Drawing.Size(575, 439)
        Me.panelOV.TabIndex = 9
        '
        'txtFolio
        '
        Me.txtFolio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolio.Location = New System.Drawing.Point(156, 397)
        Me.txtFolio.Name = "txtFolio"
        Me.txtFolio.Size = New System.Drawing.Size(45, 20)
        Me.txtFolio.TabIndex = 10
        Me.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSerie
        '
        Me.txtSerie.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerie.Location = New System.Drawing.Point(67, 397)
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Size = New System.Drawing.Size(30, 20)
        Me.txtSerie.TabIndex = 9
        Me.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(127, 400)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Folio"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(33, 400)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Serie"
        '
        'btnCancelarOV
        '
        Me.btnCancelarOV.Location = New System.Drawing.Point(396, 391)
        Me.btnCancelarOV.Name = "btnCancelarOV"
        Me.btnCancelarOV.Size = New System.Drawing.Size(151, 31)
        Me.btnCancelarOV.TabIndex = 6
        Me.btnCancelarOV.Text = "Salir de Ordenes de Venta"
        Me.btnCancelarOV.UseVisualStyleBackColor = True
        '
        'btnPrintOV
        '
        Me.btnPrintOV.Location = New System.Drawing.Point(220, 391)
        Me.btnPrintOV.Name = "btnPrintOV"
        Me.btnPrintOV.Size = New System.Drawing.Size(151, 31)
        Me.btnPrintOV.TabIndex = 5
        Me.btnPrintOV.Text = "Imprimir Orden de venta"
        Me.btnPrintOV.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(30, 370)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 20)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "PASADA"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(30, 84)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 20)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "NUEVAS"
        '
        'dgvNuevaOV
        '
        Me.dgvNuevaOV.AllowUserToAddRows = False
        Me.dgvNuevaOV.AllowUserToResizeColumns = False
        Me.dgvNuevaOV.AllowUserToResizeRows = False
        Me.dgvNuevaOV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNuevaOV.Location = New System.Drawing.Point(33, 112)
        Me.dgvNuevaOV.Name = "dgvNuevaOV"
        Me.dgvNuevaOV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvNuevaOV.Size = New System.Drawing.Size(512, 231)
        Me.dgvNuevaOV.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(542, 32)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "ORDENES DE VENTA"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.chkActualizar)
        Me.Panel1.Controls.Add(Me.btnNuevaOrdenVenta)
        Me.Panel1.Controls.Add(Me.lblperiodo)
        Me.Panel1.Controls.Add(Me.CBMosctrarCan)
        Me.Panel1.Controls.Add(Me.btnActualizarFecha)
        Me.Panel1.Controls.Add(Me.dtpFecha)
        Me.Panel1.Controls.Add(Me.lblfecha)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtbuscar)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1563, 39)
        Me.Panel1.TabIndex = 9
        '
        'chkActualizar
        '
        Me.chkActualizar.AutoSize = True
        Me.chkActualizar.Checked = True
        Me.chkActualizar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActualizar.Dock = System.Windows.Forms.DockStyle.Right
        Me.chkActualizar.Location = New System.Drawing.Point(1387, 0)
        Me.chkActualizar.Name = "chkActualizar"
        Me.chkActualizar.Size = New System.Drawing.Size(97, 35)
        Me.chkActualizar.TabIndex = 10
        Me.chkActualizar.Text = "Auto Actualizar"
        Me.chkActualizar.UseVisualStyleBackColor = True
        '
        'btnNuevaOrdenVenta
        '
        Me.btnNuevaOrdenVenta.BackColor = System.Drawing.Color.Red
        Me.btnNuevaOrdenVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevaOrdenVenta.ForeColor = System.Drawing.Color.Black
        Me.btnNuevaOrdenVenta.Location = New System.Drawing.Point(936, 3)
        Me.btnNuevaOrdenVenta.Name = "btnNuevaOrdenVenta"
        Me.btnNuevaOrdenVenta.Size = New System.Drawing.Size(199, 29)
        Me.btnNuevaOrdenVenta.TabIndex = 9
        Me.btnNuevaOrdenVenta.Text = "Nueva orden de venta"
        Me.btnNuevaOrdenVenta.UseVisualStyleBackColor = False
        '
        'CBMosctrarCan
        '
        Me.CBMosctrarCan.AutoSize = True
        Me.CBMosctrarCan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBMosctrarCan.Location = New System.Drawing.Point(312, 6)
        Me.CBMosctrarCan.Name = "CBMosctrarCan"
        Me.CBMosctrarCan.Size = New System.Drawing.Size(154, 19)
        Me.CBMosctrarCan.TabIndex = 8
        Me.CBMosctrarCan.Text = "Mostrar Cancelados"
        Me.CBMosctrarCan.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel3)
        Me.TabPage2.Controls.Add(Me.Panel2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1569, 613)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Autorizaciónes"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.DgvAutorizaciones)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 50)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1563, 560)
        Me.Panel3.TabIndex = 11
        '
        'DgvAutorizaciones
        '
        Me.DgvAutorizaciones.AllowUserToAddRows = False
        Me.DgvAutorizaciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvAutorizaciones.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DgvAutorizaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvAutorizaciones.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvAutorizaciones.Location = New System.Drawing.Point(0, 0)
        Me.DgvAutorizaciones.Name = "DgvAutorizaciones"
        Me.DgvAutorizaciones.RowHeadersWidth = 20
        Me.DgvAutorizaciones.Size = New System.Drawing.Size(1559, 556)
        Me.DgvAutorizaciones.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.dtpHoraF)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.dtpHoraI)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.CBAutorizaciones)
        Me.Panel2.Controls.Add(Me.CBBuscarUsuario)
        Me.Panel2.Controls.Add(Me.LBLbuscar_usuario)
        Me.Panel2.Controls.Add(Me.lblBuscar)
        Me.Panel2.Controls.Add(Me.txtbuscarautorizaciones)
        Me.Panel2.Controls.Add(Me.dtpAutorizacion)
        Me.Panel2.Controls.Add(Me.LblFechaAct)
        Me.Panel2.Controls.Add(Me.BtnActualizar_autorizacion)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1563, 47)
        Me.Panel2.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1328, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Hora final"
        '
        'dtpHoraF
        '
        Me.dtpHoraF.CustomFormat = "HH:mm:ss"
        Me.dtpHoraF.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHoraF.Location = New System.Drawing.Point(1380, 22)
        Me.dtpHoraF.Name = "dtpHoraF"
        Me.dtpHoraF.ShowUpDown = True
        Me.dtpHoraF.Size = New System.Drawing.Size(66, 20)
        Me.dtpHoraF.TabIndex = 15
        Me.dtpHoraF.Value = New Date(2020, 3, 4, 9, 47, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1308, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Hora de inicio"
        '
        'dtpHoraI
        '
        Me.dtpHoraI.CustomFormat = "HH:mm:ss"
        Me.dtpHoraI.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHoraI.Location = New System.Drawing.Point(1380, 1)
        Me.dtpHoraI.Name = "dtpHoraI"
        Me.dtpHoraI.ShowUpDown = True
        Me.dtpHoraI.Size = New System.Drawing.Size(66, 20)
        Me.dtpHoraI.TabIndex = 13
        Me.dtpHoraI.Value = New Date(2020, 3, 4, 9, 47, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 16)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Estatus"
        '
        'CBAutorizaciones
        '
        Me.CBAutorizaciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBAutorizaciones.FormattingEnabled = True
        Me.CBAutorizaciones.Location = New System.Drawing.Point(66, 10)
        Me.CBAutorizaciones.Name = "CBAutorizaciones"
        Me.CBAutorizaciones.Size = New System.Drawing.Size(275, 23)
        Me.CBAutorizaciones.TabIndex = 4
        Me.CBAutorizaciones.Text = "Default ( Pendiente, Autorizado, Rechazado )"
        '
        'CBBuscarUsuario
        '
        Me.CBBuscarUsuario.Enabled = False
        Me.CBBuscarUsuario.FormattingEnabled = True
        Me.CBBuscarUsuario.Location = New System.Drawing.Point(458, 12)
        Me.CBBuscarUsuario.Name = "CBBuscarUsuario"
        Me.CBBuscarUsuario.Size = New System.Drawing.Size(231, 21)
        Me.CBBuscarUsuario.TabIndex = 8
        Me.CBBuscarUsuario.Visible = False
        '
        'LBLbuscar_usuario
        '
        Me.LBLbuscar_usuario.AutoSize = True
        Me.LBLbuscar_usuario.Enabled = False
        Me.LBLbuscar_usuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLbuscar_usuario.Location = New System.Drawing.Point(347, 13)
        Me.LBLbuscar_usuario.Name = "LBLbuscar_usuario"
        Me.LBLbuscar_usuario.Size = New System.Drawing.Size(114, 16)
        Me.LBLbuscar_usuario.TabIndex = 9
        Me.LBLbuscar_usuario.Text = "Buscar Usuario"
        Me.LBLbuscar_usuario.Visible = False
        '
        'lblBuscar
        '
        Me.lblBuscar.AutoSize = True
        Me.lblBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBuscar.Location = New System.Drawing.Point(695, 13)
        Me.lblBuscar.Name = "lblBuscar"
        Me.lblBuscar.Size = New System.Drawing.Size(56, 16)
        Me.lblBuscar.TabIndex = 7
        Me.lblBuscar.Text = "Buscar"
        '
        'txtbuscarautorizaciones
        '
        Me.txtbuscarautorizaciones.Location = New System.Drawing.Point(753, 12)
        Me.txtbuscarautorizaciones.Name = "txtbuscarautorizaciones"
        Me.txtbuscarautorizaciones.Size = New System.Drawing.Size(231, 20)
        Me.txtbuscarautorizaciones.TabIndex = 6
        '
        'dtpAutorizacion
        '
        Me.dtpAutorizacion.Location = New System.Drawing.Point(1047, 12)
        Me.dtpAutorizacion.Name = "dtpAutorizacion"
        Me.dtpAutorizacion.Size = New System.Drawing.Size(200, 20)
        Me.dtpAutorizacion.TabIndex = 0
        Me.dtpAutorizacion.Visible = False
        '
        'LblFechaAct
        '
        Me.LblFechaAct.AutoSize = True
        Me.LblFechaAct.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFechaAct.Location = New System.Drawing.Point(990, 13)
        Me.LblFechaAct.Name = "LblFechaAct"
        Me.LblFechaAct.Size = New System.Drawing.Size(51, 16)
        Me.LblFechaAct.TabIndex = 3
        Me.LblFechaAct.Text = "Fecha"
        Me.LblFechaAct.Visible = False
        '
        'BtnActualizar_autorizacion
        '
        Me.BtnActualizar_autorizacion.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnActualizar_autorizacion.Location = New System.Drawing.Point(1484, 0)
        Me.BtnActualizar_autorizacion.Name = "BtnActualizar_autorizacion"
        Me.BtnActualizar_autorizacion.Size = New System.Drawing.Size(75, 43)
        Me.BtnActualizar_autorizacion.TabIndex = 1
        Me.BtnActualizar_autorizacion.Text = "Actualizar"
        Me.BtnActualizar_autorizacion.UseVisualStyleBackColor = True
        '
        'Timer2
        '
        '
        'Timer3
        '
        Me.Timer3.Interval = 200
        '
        'frmOrdenesVta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(1577, 639)
        Me.Controls.Add(Me.TcOrdenVta)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmOrdenesVta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Proceso de Orden de Venta."
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvOperacionOvTA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOperacionDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TcOrdenVta.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.panelOV.ResumeLayout(False)
        Me.panelOV.PerformLayout()
        CType(Me.dgvNuevaOV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.DgvAutorizaciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblperiodo As System.Windows.Forms.Label
    Friend WithEvents lblfecha As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtbuscar As System.Windows.Forms.TextBox
    Friend WithEvents dgvOperacionOvTA As System.Windows.Forms.DataGridView
    Friend WithEvents dgvOperacionDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents btnActualizarFecha As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents TcOrdenVta As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents CBMosctrarCan As System.Windows.Forms.CheckBox
    Friend WithEvents DgvAutorizaciones As System.Windows.Forms.DataGridView
    Friend WithEvents BtnActualizar_autorizacion As System.Windows.Forms.Button
    Friend WithEvents dtpAutorizacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents LblFechaAct As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents CBAutorizaciones As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblBuscar As System.Windows.Forms.Label
    Friend WithEvents txtbuscarautorizaciones As System.Windows.Forms.TextBox
    Friend WithEvents LBLbuscar_usuario As Label
    Friend WithEvents CBBuscarUsuario As ComboBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
  Friend WithEvents Label4 As Label
  Friend WithEvents dtpHoraF As DateTimePicker
  Friend WithEvents Label2 As Label
  Friend WithEvents dtpHoraI As DateTimePicker
 Friend WithEvents btnNuevaOrdenVenta As Button
 Friend WithEvents panelOV As Panel
 Friend WithEvents Label7 As Label
 Friend WithEvents Label6 As Label
 Friend WithEvents dgvNuevaOV As DataGridView
 Friend WithEvents Label5 As Label
 Friend WithEvents btnCancelarOV As Button
 Friend WithEvents btnPrintOV As Button
 Friend WithEvents txtFolio As TextBox
 Friend WithEvents txtSerie As TextBox
 Friend WithEvents Label9 As Label
 Friend WithEvents Label8 As Label
 Friend WithEvents Timer3 As Timer
    Friend WithEvents chkActualizar As CheckBox
End Class
