<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmUsuarios
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUsuarios))
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.btnGrabar = New System.Windows.Forms.Button()
    Me.btLimpiar = New System.Windows.Forms.Button()
    Me.btEliminar = New System.Windows.Forms.Button()
    Me.btModificar = New System.Windows.Forms.Button()
    Me.btAgregar = New System.Windows.Forms.Button()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Panel5 = New System.Windows.Forms.Panel()
    Me.cboAgenteVentas = New System.Windows.Forms.ComboBox()
    Me.cboAlmacen = New System.Windows.Forms.ComboBox()
    Me.cboBOAutoriza = New System.Windows.Forms.ComboBox()
    Me.cboEsAgente = New System.Windows.Forms.ComboBox()
    Me.cboAgente = New System.Windows.Forms.ComboBox()
    Me.Label16 = New System.Windows.Forms.Label()
    Me.txtCVentas = New System.Windows.Forms.TextBox()
    Me.Label17 = New System.Windows.Forms.Label()
    Me.txtCCVentas = New System.Windows.Forms.TextBox()
    Me.cboRol = New System.Windows.Forms.ComboBox()
    Me.Label15 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtIDUsr = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cboEstatus = New System.Windows.Forms.ComboBox()
    Me.txtNombre = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.cboDepto = New System.Windows.Forms.ComboBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.txtClave = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.txtCorreoEmpresarial = New System.Windows.Forms.TextBox()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.txtClaveC = New System.Windows.Forms.TextBox()
    Me.txtRutaPDF = New System.Windows.Forms.TextBox()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.txtSerie = New System.Windows.Forms.TextBox()
    Me.txtCodAgente = New System.Windows.Forms.TextBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Panel4 = New System.Windows.Forms.Panel()
    Me.Panel3 = New System.Windows.Forms.Panel()
    Me.btnAccesos = New System.Windows.Forms.Button()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.dgvUsuarios = New System.Windows.Forms.DataGridView()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.Panel1.SuspendLayout()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.Panel5.SuspendLayout()
    Me.Panel3.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    CType(Me.dgvUsuarios, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(1465, 77)
    Me.Panel1.TabIndex = 0
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.AutoSize = True
    Me.TableLayoutPanel1.ColumnCount = 5
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.btnGrabar, 2, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.btLimpiar, 4, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.btEliminar, 3, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.btModificar, 1, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.btAgregar, 0, 0)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(1465, 77)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'btnGrabar
    '
    Me.btnGrabar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
    Me.btnGrabar.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btnGrabar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.btnGrabar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
    Me.btnGrabar.Image = CType(resources.GetObject("btnGrabar.Image"), System.Drawing.Image)
    Me.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.btnGrabar.Location = New System.Drawing.Point(589, 3)
    Me.btnGrabar.Name = "btnGrabar"
    Me.btnGrabar.Size = New System.Drawing.Size(287, 71)
    Me.btnGrabar.TabIndex = 4
    Me.btnGrabar.Text = "&Grabar"
    Me.btnGrabar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.btnGrabar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btnGrabar.UseVisualStyleBackColor = True
    '
    'btLimpiar
    '
    Me.btLimpiar.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.btLimpiar.Image = CType(resources.GetObject("btLimpiar.Image"), System.Drawing.Image)
    Me.btLimpiar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.btLimpiar.Location = New System.Drawing.Point(1175, 3)
    Me.btLimpiar.Name = "btLimpiar"
    Me.btLimpiar.Size = New System.Drawing.Size(287, 71)
    Me.btLimpiar.TabIndex = 3
    Me.btLimpiar.Text = "&Limpíar"
    Me.btLimpiar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.btLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btLimpiar.UseVisualStyleBackColor = True
    '
    'btEliminar
    '
    Me.btEliminar.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.btEliminar.ForeColor = System.Drawing.Color.Red
    Me.btEliminar.Image = CType(resources.GetObject("btEliminar.Image"), System.Drawing.Image)
    Me.btEliminar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.btEliminar.Location = New System.Drawing.Point(882, 3)
    Me.btEliminar.Name = "btEliminar"
    Me.btEliminar.Size = New System.Drawing.Size(287, 71)
    Me.btEliminar.TabIndex = 2
    Me.btEliminar.Text = "&Eliminar"
    Me.btEliminar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.btEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btEliminar.UseVisualStyleBackColor = True
    '
    'btModificar
    '
    Me.btModificar.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.btModificar.ForeColor = System.Drawing.Color.DeepSkyBlue
    Me.btModificar.Image = CType(resources.GetObject("btModificar.Image"), System.Drawing.Image)
    Me.btModificar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.btModificar.Location = New System.Drawing.Point(296, 3)
    Me.btModificar.Name = "btModificar"
    Me.btModificar.Size = New System.Drawing.Size(287, 71)
    Me.btModificar.TabIndex = 1
    Me.btModificar.Text = "&Modificar"
    Me.btModificar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.btModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btModificar.UseVisualStyleBackColor = True
    '
    'btAgregar
    '
    Me.btAgregar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.btAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
    Me.btAgregar.Dock = System.Windows.Forms.DockStyle.Fill
    Me.btAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.btAgregar.ForeColor = System.Drawing.Color.ForestGreen
    Me.btAgregar.Image = CType(resources.GetObject("btAgregar.Image"), System.Drawing.Image)
    Me.btAgregar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
    Me.btAgregar.Location = New System.Drawing.Point(3, 3)
    Me.btAgregar.Name = "btAgregar"
    Me.btAgregar.Size = New System.Drawing.Size(287, 71)
    Me.btAgregar.TabIndex = 0
    Me.btAgregar.Text = "&Agregar"
    Me.btAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.btAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btAgregar.UseVisualStyleBackColor = True
    '
    'Panel2
    '
    Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel2.Controls.Add(Me.Panel5)
    Me.Panel2.Controls.Add(Me.Panel4)
    Me.Panel2.Controls.Add(Me.Panel3)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
    Me.Panel2.Location = New System.Drawing.Point(0, 77)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(1465, 190)
    Me.Panel2.TabIndex = 1
    '
    'Panel5
    '
    Me.Panel5.Controls.Add(Me.Button1)
    Me.Panel5.Controls.Add(Me.cboAgenteVentas)
    Me.Panel5.Controls.Add(Me.cboAlmacen)
    Me.Panel5.Controls.Add(Me.cboBOAutoriza)
    Me.Panel5.Controls.Add(Me.cboEsAgente)
    Me.Panel5.Controls.Add(Me.cboAgente)
    Me.Panel5.Controls.Add(Me.Label16)
    Me.Panel5.Controls.Add(Me.txtCVentas)
    Me.Panel5.Controls.Add(Me.Label17)
    Me.Panel5.Controls.Add(Me.txtCCVentas)
    Me.Panel5.Controls.Add(Me.cboRol)
    Me.Panel5.Controls.Add(Me.Label15)
    Me.Panel5.Controls.Add(Me.Label1)
    Me.Panel5.Controls.Add(Me.txtIDUsr)
    Me.Panel5.Controls.Add(Me.Label2)
    Me.Panel5.Controls.Add(Me.cboEstatus)
    Me.Panel5.Controls.Add(Me.txtNombre)
    Me.Panel5.Controls.Add(Me.Label3)
    Me.Panel5.Controls.Add(Me.Label14)
    Me.Panel5.Controls.Add(Me.cboDepto)
    Me.Panel5.Controls.Add(Me.Label4)
    Me.Panel5.Controls.Add(Me.Label13)
    Me.Panel5.Controls.Add(Me.txtClave)
    Me.Panel5.Controls.Add(Me.Label5)
    Me.Panel5.Controls.Add(Me.Label12)
    Me.Panel5.Controls.Add(Me.Label6)
    Me.Panel5.Controls.Add(Me.txtCorreoEmpresarial)
    Me.Panel5.Controls.Add(Me.Label11)
    Me.Panel5.Controls.Add(Me.Label7)
    Me.Panel5.Controls.Add(Me.txtClaveC)
    Me.Panel5.Controls.Add(Me.txtRutaPDF)
    Me.Panel5.Controls.Add(Me.Label10)
    Me.Panel5.Controls.Add(Me.Label8)
    Me.Panel5.Controls.Add(Me.txtSerie)
    Me.Panel5.Controls.Add(Me.txtCodAgente)
    Me.Panel5.Controls.Add(Me.Label9)
    Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel5.Location = New System.Drawing.Point(85, 0)
    Me.Panel5.Name = "Panel5"
    Me.Panel5.Size = New System.Drawing.Size(1289, 186)
    Me.Panel5.TabIndex = 30
    '
    'cboAgenteVentas
    '
    Me.cboAgenteVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboAgenteVentas.FormattingEnabled = True
    Me.cboAgenteVentas.Location = New System.Drawing.Point(689, 156)
    Me.cboAgenteVentas.Name = "cboAgenteVentas"
    Me.cboAgenteVentas.Size = New System.Drawing.Size(168, 21)
    Me.cboAgenteVentas.TabIndex = 38
    '
    'cboAlmacen
    '
    Me.cboAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboAlmacen.FormattingEnabled = True
    Me.cboAlmacen.Location = New System.Drawing.Point(600, 157)
    Me.cboAlmacen.Name = "cboAlmacen"
    Me.cboAlmacen.Size = New System.Drawing.Size(83, 21)
    Me.cboAlmacen.TabIndex = 37
    '
    'cboBOAutoriza
    '
    Me.cboBOAutoriza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboBOAutoriza.FormattingEnabled = True
    Me.cboBOAutoriza.Location = New System.Drawing.Point(516, 157)
    Me.cboBOAutoriza.Name = "cboBOAutoriza"
    Me.cboBOAutoriza.Size = New System.Drawing.Size(78, 21)
    Me.cboBOAutoriza.TabIndex = 36
    '
    'cboEsAgente
    '
    Me.cboEsAgente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboEsAgente.FormattingEnabled = True
    Me.cboEsAgente.Location = New System.Drawing.Point(16, 157)
    Me.cboEsAgente.Name = "cboEsAgente"
    Me.cboEsAgente.Size = New System.Drawing.Size(78, 21)
    Me.cboEsAgente.TabIndex = 35
    '
    'cboAgente
    '
    Me.cboAgente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboAgente.FormattingEnabled = True
    Me.cboAgente.Location = New System.Drawing.Point(116, 157)
    Me.cboAgente.Name = "cboAgente"
    Me.cboAgente.Size = New System.Drawing.Size(173, 21)
    Me.cboAgente.TabIndex = 34
    '
    'Label16
    '
    Me.Label16.AutoSize = True
    Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label16.Location = New System.Drawing.Point(13, 97)
    Me.Label16.Name = "Label16"
    Me.Label16.Size = New System.Drawing.Size(91, 13)
    Me.Label16.TabIndex = 30
    Me.Label16.Text = "Correo Ventas:"
    '
    'txtCVentas
    '
    Me.txtCVentas.Location = New System.Drawing.Point(16, 113)
    Me.txtCVentas.MaxLength = 200
    Me.txtCVentas.Name = "txtCVentas"
    Me.txtCVentas.Size = New System.Drawing.Size(552, 20)
    Me.txtCVentas.TabIndex = 31
    '
    'Label17
    '
    Me.Label17.AutoSize = True
    Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label17.Location = New System.Drawing.Point(576, 98)
    Me.Label17.Name = "Label17"
    Me.Label17.Size = New System.Drawing.Size(150, 13)
    Me.Label17.TabIndex = 32
    Me.Label17.Text = "Copia corredo de ventas:"
    '
    'txtCCVentas
    '
    Me.txtCCVentas.Location = New System.Drawing.Point(574, 114)
    Me.txtCCVentas.MaxLength = 200
    Me.txtCCVentas.Name = "txtCCVentas"
    Me.txtCCVentas.Size = New System.Drawing.Size(709, 20)
    Me.txtCCVentas.TabIndex = 33
    '
    'cboRol
    '
    Me.cboRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboRol.FormattingEnabled = True
    Me.cboRol.Location = New System.Drawing.Point(1103, 24)
    Me.cboRol.Name = "cboRol"
    Me.cboRol.Size = New System.Drawing.Size(180, 21)
    Me.cboRol.TabIndex = 29
    '
    'Label15
    '
    Me.Label15.AutoSize = True
    Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label15.Location = New System.Drawing.Point(1100, 8)
    Me.Label15.Name = "Label15"
    Me.Label15.Size = New System.Drawing.Size(30, 13)
    Me.Label15.TabIndex = 28
    Me.Label15.Text = "Rol:"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(13, 8)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(71, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "ID Usuario:"
    '
    'txtIDUsr
    '
    Me.txtIDUsr.Location = New System.Drawing.Point(16, 24)
    Me.txtIDUsr.MaxLength = 10
    Me.txtIDUsr.Name = "txtIDUsr"
    Me.txtIDUsr.Size = New System.Drawing.Size(100, 20)
    Me.txtIDUsr.TabIndex = 1
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(119, 8)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(54, 13)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "Nombre:"
    '
    'cboEstatus
    '
    Me.cboEstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboEstatus.FormattingEnabled = True
    Me.cboEstatus.Location = New System.Drawing.Point(994, 24)
    Me.cboEstatus.Name = "cboEstatus"
    Me.cboEstatus.Size = New System.Drawing.Size(94, 21)
    Me.cboEstatus.TabIndex = 9
    '
    'txtNombre
    '
    Me.txtNombre.Location = New System.Drawing.Point(122, 24)
    Me.txtNombre.MaxLength = 35
    Me.txtNombre.Name = "txtNombre"
    Me.txtNombre.Size = New System.Drawing.Size(446, 20)
    Me.txtNombre.TabIndex = 3
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label3.Location = New System.Drawing.Point(571, 8)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(107, 13)
    Me.Label3.TabIndex = 4
    Me.Label3.Text = "ID Departamento:"
    '
    'Label14
    '
    Me.Label14.AutoSize = True
    Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label14.Location = New System.Drawing.Point(686, 140)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(94, 13)
    Me.Label14.TabIndex = 26
    Me.Label14.Text = "Agente Ventas:"
    '
    'cboDepto
    '
    Me.cboDepto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboDepto.FormattingEnabled = True
    Me.cboDepto.Location = New System.Drawing.Point(574, 24)
    Me.cboDepto.Name = "cboDepto"
    Me.cboDepto.Size = New System.Drawing.Size(247, 21)
    Me.cboDepto.TabIndex = 5
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label4.Location = New System.Drawing.Point(834, 9)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(104, 13)
    Me.Label4.TabIndex = 6
    Me.Label4.Text = "Contraseña TPD:"
    '
    'Label13
    '
    Me.Label13.AutoSize = True
    Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label13.Location = New System.Drawing.Point(599, 140)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(59, 13)
    Me.Label13.TabIndex = 24
    Me.Label13.Text = "Almacen:"
    '
    'txtClave
    '
    Me.txtClave.Location = New System.Drawing.Point(831, 24)
    Me.txtClave.MaxLength = 10
    Me.txtClave.Name = "txtClave"
    Me.txtClave.Size = New System.Drawing.Size(154, 20)
    Me.txtClave.TabIndex = 7
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.Location = New System.Drawing.Point(991, 8)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(53, 13)
    Me.Label5.TabIndex = 8
    Me.Label5.Text = "Estatus:"
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label12.Location = New System.Drawing.Point(515, 140)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(78, 13)
    Me.Label12.TabIndex = 22
    Me.Label12.Text = "BO Autoriza:"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label6.Location = New System.Drawing.Point(13, 56)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(117, 13)
    Me.Label6.TabIndex = 10
    Me.Label6.Text = "Correo Empresarial:"
    '
    'txtCorreoEmpresarial
    '
    Me.txtCorreoEmpresarial.Location = New System.Drawing.Point(16, 72)
    Me.txtCorreoEmpresarial.Name = "txtCorreoEmpresarial"
    Me.txtCorreoEmpresarial.Size = New System.Drawing.Size(552, 20)
    Me.txtCorreoEmpresarial.TabIndex = 11
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label11.Location = New System.Drawing.Point(13, 140)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(81, 13)
    Me.Label11.TabIndex = 20
    Me.Label11.Text = "Usr Agente?:"
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label7.Location = New System.Drawing.Point(576, 57)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(66, 13)
    Me.Label7.TabIndex = 12
    Me.Label7.Text = "Ruta PDF:"
    '
    'txtClaveC
    '
    Me.txtClaveC.Location = New System.Drawing.Point(409, 157)
    Me.txtClaveC.MaxLength = 20
    Me.txtClaveC.Name = "txtClaveC"
    Me.txtClaveC.Size = New System.Drawing.Size(100, 20)
    Me.txtClaveC.TabIndex = 19
    '
    'txtRutaPDF
    '
    Me.txtRutaPDF.Location = New System.Drawing.Point(574, 73)
    Me.txtRutaPDF.MaxLength = 200
    Me.txtRutaPDF.Name = "txtRutaPDF"
    Me.txtRutaPDF.Size = New System.Drawing.Size(709, 20)
    Me.txtRutaPDF.TabIndex = 13
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label10.Location = New System.Drawing.Point(406, 140)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(91, 13)
    Me.Label10.TabIndex = 18
    Me.Label10.Text = "Contraseña C.:"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label8.Location = New System.Drawing.Point(113, 140)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(51, 13)
    Me.Label8.TabIndex = 14
    Me.Label8.Text = "Agente:"
    '
    'txtSerie
    '
    Me.txtSerie.Location = New System.Drawing.Point(303, 157)
    Me.txtSerie.MaxLength = 2
    Me.txtSerie.Name = "txtSerie"
    Me.txtSerie.Size = New System.Drawing.Size(100, 20)
    Me.txtSerie.TabIndex = 17
    '
    'txtCodAgente
    '
    Me.txtCodAgente.Location = New System.Drawing.Point(863, 156)
    Me.txtCodAgente.Name = "txtCodAgente"
    Me.txtCodAgente.Size = New System.Drawing.Size(100, 20)
    Me.txtCodAgente.TabIndex = 15
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label9.Location = New System.Drawing.Point(300, 140)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(40, 13)
    Me.Label9.TabIndex = 16
    Me.Label9.Text = "Serie:"
    '
    'Panel4
    '
    Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
    Me.Panel4.Location = New System.Drawing.Point(0, 0)
    Me.Panel4.Name = "Panel4"
    Me.Panel4.Size = New System.Drawing.Size(85, 186)
    Me.Panel4.TabIndex = 29
    '
    'Panel3
    '
    Me.Panel3.Controls.Add(Me.btnAccesos)
    Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
    Me.Panel3.Location = New System.Drawing.Point(1374, 0)
    Me.Panel3.Name = "Panel3"
    Me.Panel3.Size = New System.Drawing.Size(87, 186)
    Me.Panel3.TabIndex = 28
    '
    'btnAccesos
    '
    Me.btnAccesos.Location = New System.Drawing.Point(15, 39)
    Me.btnAccesos.Name = "btnAccesos"
    Me.btnAccesos.Size = New System.Drawing.Size(58, 118)
    Me.btnAccesos.TabIndex = 0
    Me.btnAccesos.Text = "Accesos"
    Me.btnAccesos.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.dgvUsuarios)
    Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.GroupBox1.Location = New System.Drawing.Point(0, 267)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(1465, 508)
    Me.GroupBox1.TabIndex = 2
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Usuarios"
    '
    'dgvUsuarios
    '
    Me.dgvUsuarios.AllowUserToAddRows = False
    Me.dgvUsuarios.AllowUserToDeleteRows = False
    Me.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvUsuarios.Dock = System.Windows.Forms.DockStyle.Fill
    Me.dgvUsuarios.Location = New System.Drawing.Point(3, 16)
    Me.dgvUsuarios.Name = "dgvUsuarios"
    Me.dgvUsuarios.ReadOnly = True
    Me.dgvUsuarios.Size = New System.Drawing.Size(1459, 489)
    Me.dgvUsuarios.TabIndex = 0
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(1232, 156)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(36, 23)
    Me.Button1.TabIndex = 39
    Me.Button1.Text = "Button1"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'frmUsuarios
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.ClientSize = New System.Drawing.Size(1465, 775)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
    Me.Name = "frmUsuarios"
    Me.ShowIcon = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Usuarios"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.Panel2.ResumeLayout(False)
    Me.Panel5.ResumeLayout(False)
    Me.Panel5.PerformLayout()
    Me.Panel3.ResumeLayout(False)
    Me.GroupBox1.ResumeLayout(False)
    CType(Me.dgvUsuarios, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents btEliminar As Button
  Friend WithEvents btModificar As Button
  Friend WithEvents btAgregar As Button
  Friend WithEvents dgvUsuarios As DataGridView
  Friend WithEvents Label14 As Label
  Friend WithEvents Label13 As Label
  Friend WithEvents Label12 As Label
  Friend WithEvents Label11 As Label
  Friend WithEvents txtClaveC As TextBox
  Friend WithEvents Label10 As Label
  Friend WithEvents txtSerie As TextBox
  Friend WithEvents Label9 As Label
  Friend WithEvents Label8 As Label
  Friend WithEvents txtRutaPDF As TextBox
  Friend WithEvents Label7 As Label
  Friend WithEvents txtCorreoEmpresarial As TextBox
  Friend WithEvents Label6 As Label
  Friend WithEvents cboEstatus As ComboBox
  Friend WithEvents Label5 As Label
  Friend WithEvents txtClave As TextBox
  Friend WithEvents Label4 As Label
  Friend WithEvents cboDepto As ComboBox
  Friend WithEvents Label3 As Label
  Friend WithEvents txtNombre As TextBox
  Friend WithEvents Label2 As Label
  Friend WithEvents txtIDUsr As TextBox
  Friend WithEvents Label1 As Label
  Friend WithEvents Panel4 As Panel
  Friend WithEvents Panel3 As Panel
  Friend WithEvents Panel5 As Panel
  Friend WithEvents cboRol As ComboBox
  Friend WithEvents Label15 As Label
  Friend WithEvents btnGrabar As Button
  Friend WithEvents btLimpiar As Button
  Friend WithEvents Label16 As Label
  Friend WithEvents txtCVentas As TextBox
  Friend WithEvents Label17 As Label
  Friend WithEvents txtCCVentas As TextBox
  Friend WithEvents cboAgente As ComboBox
  Friend WithEvents cboEsAgente As ComboBox
  Friend WithEvents cboBOAutoriza As ComboBox
  Friend WithEvents cboAlmacen As ComboBox
  Friend WithEvents txtCodAgente As TextBox
  Friend WithEvents cboAgenteVentas As ComboBox
  Friend WithEvents btnAccesos As Button
  Friend WithEvents Button1 As Button
End Class
