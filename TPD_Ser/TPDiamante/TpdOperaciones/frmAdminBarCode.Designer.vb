<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAdminBarCode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdminBarCode))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.TodosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NuevoCódigoDeBarrasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModificarCódigoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GrabarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AgregarCódigoAlternoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PanelDatos = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtCantTR = New System.Windows.Forms.TextBox()
        Me.txtCantCaja = New System.Windows.Forms.TextBox()
        Me.txtCantBolsa = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtCBBolsa = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCBProveedor = New System.Windows.Forms.TextBox()
        Me.txtCBTarima = New System.Windows.Forms.TextBox()
        Me.txtCBCaja = New System.Windows.Forms.TextBox()
        Me.txtCBPieza = New System.Windows.Forms.TextBox()
        Me.txtCBInterno = New System.Windows.Forms.TextBox()
        Me.txtProveedor = New System.Windows.Forms.TextBox()
        Me.txtLinea = New System.Windows.Forms.TextBox()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtCategoria = New System.Windows.Forms.TextBox()
        Me.txtArticulo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblCveProveedor = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelSeleccion = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dgSeleccion = New System.Windows.Forms.DataGridView()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DtGInfCB = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.txtNombreArticulo = New System.Windows.Forms.TextBox()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.cmbCodigoAlterno = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtDesc_CodAlterno = New System.Windows.Forms.TextBox()
        Me.txtArt_CodAlterno = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.PanelDatos.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.PanelSeleccion.SuspendLayout()
        CType(Me.dgSeleccion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtGInfCB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.White
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TodosToolStripMenuItem, Me.NuevoCódigoDeBarrasToolStripMenuItem, Me.ModificarCódigoToolStripMenuItem, Me.GrabarToolStripMenuItem, Me.AgregarCódigoAlternoToolStripMenuItem, Me.SalirToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1609, 24)
        Me.MenuStrip1.TabIndex = 21
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'TodosToolStripMenuItem
        '
        Me.TodosToolStripMenuItem.Image = Global.TPDiamante.My.Resources.Resources.search16_h
        Me.TodosToolStripMenuItem.Name = "TodosToolStripMenuItem"
        Me.TodosToolStripMenuItem.Size = New System.Drawing.Size(66, 20)
        Me.TodosToolStripMenuItem.Text = "Todos"
        '
        'NuevoCódigoDeBarrasToolStripMenuItem
        '
        Me.NuevoCódigoDeBarrasToolStripMenuItem.Image = Global.TPDiamante.My.Resources.Resources.add
        Me.NuevoCódigoDeBarrasToolStripMenuItem.Name = "NuevoCódigoDeBarrasToolStripMenuItem"
        Me.NuevoCódigoDeBarrasToolStripMenuItem.Size = New System.Drawing.Size(110, 20)
        Me.NuevoCódigoDeBarrasToolStripMenuItem.Text = "&Nuevo código"
        '
        'ModificarCódigoToolStripMenuItem
        '
        Me.ModificarCódigoToolStripMenuItem.Image = Global.TPDiamante.My.Resources.Resources.kate2
        Me.ModificarCódigoToolStripMenuItem.Name = "ModificarCódigoToolStripMenuItem"
        Me.ModificarCódigoToolStripMenuItem.Size = New System.Drawing.Size(126, 20)
        Me.ModificarCódigoToolStripMenuItem.Text = "&Modificar código"
        '
        'GrabarToolStripMenuItem
        '
        Me.GrabarToolStripMenuItem.Image = CType(resources.GetObject("GrabarToolStripMenuItem.Image"), System.Drawing.Image)
        Me.GrabarToolStripMenuItem.Name = "GrabarToolStripMenuItem"
        Me.GrabarToolStripMenuItem.Size = New System.Drawing.Size(70, 20)
        Me.GrabarToolStripMenuItem.Text = "Grabar"
        '
        'AgregarCódigoAlternoToolStripMenuItem
        '
        Me.AgregarCódigoAlternoToolStripMenuItem.BackgroundImage = CType(resources.GetObject("AgregarCódigoAlternoToolStripMenuItem.BackgroundImage"), System.Drawing.Image)
        Me.AgregarCódigoAlternoToolStripMenuItem.Image = CType(resources.GetObject("AgregarCódigoAlternoToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AgregarCódigoAlternoToolStripMenuItem.Name = "AgregarCódigoAlternoToolStripMenuItem"
        Me.AgregarCódigoAlternoToolStripMenuItem.Size = New System.Drawing.Size(161, 20)
        Me.AgregarCódigoAlternoToolStripMenuItem.Text = "Agregar Código Alterno"
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Image = CType(resources.GetObject("SalirToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SalirToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.SalirToolStripMenuItem.Text = "&Salir"
        '
        'PanelDatos
        '
        Me.PanelDatos.BackColor = System.Drawing.Color.White
        Me.PanelDatos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelDatos.Controls.Add(Me.Label20)
        Me.PanelDatos.Controls.Add(Me.txtCantTR)
        Me.PanelDatos.Controls.Add(Me.txtCantCaja)
        Me.PanelDatos.Controls.Add(Me.txtCantBolsa)
        Me.PanelDatos.Controls.Add(Me.Label15)
        Me.PanelDatos.Controls.Add(Me.Label14)
        Me.PanelDatos.Controls.Add(Me.Label13)
        Me.PanelDatos.Controls.Add(Me.txtCBBolsa)
        Me.PanelDatos.Controls.Add(Me.Label12)
        Me.PanelDatos.Controls.Add(Me.txtCBProveedor)
        Me.PanelDatos.Controls.Add(Me.txtCBTarima)
        Me.PanelDatos.Controls.Add(Me.txtCBCaja)
        Me.PanelDatos.Controls.Add(Me.txtCBPieza)
        Me.PanelDatos.Controls.Add(Me.txtCBInterno)
        Me.PanelDatos.Controls.Add(Me.txtProveedor)
        Me.PanelDatos.Controls.Add(Me.txtLinea)
        Me.PanelDatos.Controls.Add(Me.txtDescripcion)
        Me.PanelDatos.Controls.Add(Me.txtCategoria)
        Me.PanelDatos.Controls.Add(Me.txtArticulo)
        Me.PanelDatos.Controls.Add(Me.Label10)
        Me.PanelDatos.Controls.Add(Me.Label9)
        Me.PanelDatos.Controls.Add(Me.Label8)
        Me.PanelDatos.Controls.Add(Me.Label7)
        Me.PanelDatos.Controls.Add(Me.Label6)
        Me.PanelDatos.Controls.Add(Me.Label5)
        Me.PanelDatos.Controls.Add(Me.Label4)
        Me.PanelDatos.Controls.Add(Me.Label3)
        Me.PanelDatos.Controls.Add(Me.Label2)
        Me.PanelDatos.Controls.Add(Me.Label1)
        Me.PanelDatos.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelDatos.Location = New System.Drawing.Point(0, 24)
        Me.PanelDatos.Name = "PanelDatos"
        Me.PanelDatos.Size = New System.Drawing.Size(1022, 787)
        Me.PanelDatos.TabIndex = 22
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(27, 14)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(93, 13)
        Me.Label20.TabIndex = 54
        Me.Label20.Text = "INFORMACIÓN"
        '
        'txtCantTR
        '
        Me.txtCantTR.Location = New System.Drawing.Point(937, 192)
        Me.txtCantTR.Name = "txtCantTR"
        Me.txtCantTR.Size = New System.Drawing.Size(64, 20)
        Me.txtCantTR.TabIndex = 48
        '
        'txtCantCaja
        '
        Me.txtCantCaja.Location = New System.Drawing.Point(937, 165)
        Me.txtCantCaja.Name = "txtCantCaja"
        Me.txtCantCaja.Size = New System.Drawing.Size(64, 20)
        Me.txtCantCaja.TabIndex = 47
        '
        'txtCantBolsa
        '
        Me.txtCantBolsa.Location = New System.Drawing.Point(937, 136)
        Me.txtCantBolsa.Name = "txtCantBolsa"
        Me.txtCantBolsa.Size = New System.Drawing.Size(64, 20)
        Me.txtCantBolsa.TabIndex = 46
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(870, 195)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(49, 13)
        Me.Label15.TabIndex = 45
        Me.Label15.Text = "Cantidad"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(870, 167)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(49, 13)
        Me.Label14.TabIndex = 44
        Me.Label14.Text = "Cantidad"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(870, 139)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(49, 13)
        Me.Label13.TabIndex = 43
        Me.Label13.Text = "Cantidad"
        '
        'txtCBBolsa
        '
        Me.txtCBBolsa.Location = New System.Drawing.Point(716, 136)
        Me.txtCBBolsa.Name = "txtCBBolsa"
        Me.txtCBBolsa.ReadOnly = True
        Me.txtCBBolsa.Size = New System.Drawing.Size(135, 20)
        Me.txtCBBolsa.TabIndex = 42
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(614, 139)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 13)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "BarCode bolsa:"
        '
        'txtCBProveedor
        '
        Me.txtCBProveedor.Location = New System.Drawing.Point(717, 54)
        Me.txtCBProveedor.Name = "txtCBProveedor"
        Me.txtCBProveedor.Size = New System.Drawing.Size(135, 20)
        Me.txtCBProveedor.TabIndex = 39
        '
        'txtCBTarima
        '
        Me.txtCBTarima.Location = New System.Drawing.Point(716, 192)
        Me.txtCBTarima.Name = "txtCBTarima"
        Me.txtCBTarima.ReadOnly = True
        Me.txtCBTarima.Size = New System.Drawing.Size(135, 20)
        Me.txtCBTarima.TabIndex = 38
        '
        'txtCBCaja
        '
        Me.txtCBCaja.Location = New System.Drawing.Point(716, 164)
        Me.txtCBCaja.Name = "txtCBCaja"
        Me.txtCBCaja.ReadOnly = True
        Me.txtCBCaja.Size = New System.Drawing.Size(135, 20)
        Me.txtCBCaja.TabIndex = 37
        '
        'txtCBPieza
        '
        Me.txtCBPieza.Location = New System.Drawing.Point(716, 109)
        Me.txtCBPieza.Name = "txtCBPieza"
        Me.txtCBPieza.ReadOnly = True
        Me.txtCBPieza.Size = New System.Drawing.Size(135, 20)
        Me.txtCBPieza.TabIndex = 36
        '
        'txtCBInterno
        '
        Me.txtCBInterno.Location = New System.Drawing.Point(716, 80)
        Me.txtCBInterno.Name = "txtCBInterno"
        Me.txtCBInterno.Size = New System.Drawing.Size(135, 20)
        Me.txtCBInterno.TabIndex = 35
        '
        'txtProveedor
        '
        Me.txtProveedor.Location = New System.Drawing.Point(93, 137)
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.ReadOnly = True
        Me.txtProveedor.Size = New System.Drawing.Size(431, 20)
        Me.txtProveedor.TabIndex = 34
        '
        'txtLinea
        '
        Me.txtLinea.Location = New System.Drawing.Point(93, 112)
        Me.txtLinea.Name = "txtLinea"
        Me.txtLinea.ReadOnly = True
        Me.txtLinea.Size = New System.Drawing.Size(431, 20)
        Me.txtLinea.TabIndex = 33
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Location = New System.Drawing.Point(93, 88)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ReadOnly = True
        Me.txtDescripcion.Size = New System.Drawing.Size(431, 20)
        Me.txtDescripcion.TabIndex = 32
        '
        'txtCategoria
        '
        Me.txtCategoria.Location = New System.Drawing.Point(93, 163)
        Me.txtCategoria.Name = "txtCategoria"
        Me.txtCategoria.Size = New System.Drawing.Size(51, 20)
        Me.txtCategoria.TabIndex = 31
        '
        'txtArticulo
        '
        Me.txtArticulo.Location = New System.Drawing.Point(93, 60)
        Me.txtArticulo.Name = "txtArticulo"
        Me.txtArticulo.ReadOnly = True
        Me.txtArticulo.Size = New System.Drawing.Size(431, 20)
        Me.txtArticulo.TabIndex = 30
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(615, 57)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(102, 13)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "BarCode proveedor:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(615, 195)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 13)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "BarCode tarima:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(614, 168)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 13)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "BarCode caja:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(615, 113)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "BarCode pieza:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(614, 84)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "BarCode interno:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 140)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Proveedor:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Línea:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Descripción:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 166)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Categoría:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Artículo:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(1111, 16)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(151, 20)
        Me.TextBox1.TabIndex = 50
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(1015, 16)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(90, 13)
        Me.Label18.TabIndex = 49
        Me.Label18.Text = "BarCode  Alterno:"
        '
        'lblCveProveedor
        '
        Me.lblCveProveedor.AutoSize = True
        Me.lblCveProveedor.Location = New System.Drawing.Point(713, 8)
        Me.lblCveProveedor.Name = "lblCveProveedor"
        Me.lblCveProveedor.Size = New System.Drawing.Size(85, 13)
        Me.lblCveProveedor.TabIndex = 40
        Me.lblCveProveedor.Text = "lblCveProveedor"
        Me.lblCveProveedor.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.PanelSeleccion)
        Me.Panel1.Controls.Add(Me.DtGInfCB)
        Me.Panel1.Location = New System.Drawing.Point(0, 252)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1609, 608)
        Me.Panel1.TabIndex = 41
        '
        'PanelSeleccion
        '
        Me.PanelSeleccion.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelSeleccion.BackColor = System.Drawing.Color.Gray
        Me.PanelSeleccion.Controls.Add(Me.Button2)
        Me.PanelSeleccion.Controls.Add(Me.Button1)
        Me.PanelSeleccion.Controls.Add(Me.dgSeleccion)
        Me.PanelSeleccion.Controls.Add(Me.Label11)
        Me.PanelSeleccion.Location = New System.Drawing.Point(3, 3)
        Me.PanelSeleccion.Name = "PanelSeleccion"
        Me.PanelSeleccion.Size = New System.Drawing.Size(1603, 565)
        Me.PanelSeleccion.TabIndex = 41
        Me.PanelSeleccion.Visible = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1445, 12)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Cancelar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Location = New System.Drawing.Point(1364, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Aceptar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'dgSeleccion
        '
        Me.dgSeleccion.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgSeleccion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgSeleccion.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dgSeleccion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSeleccion.Location = New System.Drawing.Point(3, 43)
        Me.dgSeleccion.Name = "dgSeleccion"
        Me.dgSeleccion.Size = New System.Drawing.Size(1591, 519)
        Me.dgSeleccion.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(9, 7)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1490, 28)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Seleccione el artículo al cúal desea colocarle código de barra"
        '
        'DtGInfCB
        '
        Me.DtGInfCB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DtGInfCB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtGInfCB.Location = New System.Drawing.Point(3, 3)
        Me.DtGInfCB.Name = "DtGInfCB"
        Me.DtGInfCB.Size = New System.Drawing.Size(1557, 565)
        Me.DtGInfCB.TabIndex = 21
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Button3)
        Me.Panel2.Controls.Add(Me.txtNombreArticulo)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Controls.Add(Me.txtCodigo)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(1022, 24)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(587, 85)
        Me.Panel2.TabIndex = 53
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(17, 60)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(66, 13)
        Me.Label19.TabIndex = 53
        Me.Label19.Text = "Descripción:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(17, 36)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(83, 13)
        Me.Label17.TabIndex = 52
        Me.Label17.Text = "Código Artículo:"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(1295, 14)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(60, 23)
        Me.Button3.TabIndex = 51
        Me.Button3.Text = "Guardar"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'txtNombreArticulo
        '
        Me.txtNombreArticulo.Location = New System.Drawing.Point(141, 57)
        Me.txtNombreArticulo.Name = "txtNombreArticulo"
        Me.txtNombreArticulo.Size = New System.Drawing.Size(390, 20)
        Me.txtNombreArticulo.TabIndex = 49
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(141, 33)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(390, 20)
        Me.txtCodigo.TabIndex = 49
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(17, 14)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(57, 13)
        Me.Label16.TabIndex = 49
        Me.Label16.Text = "BUSCAR"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.cmbCodigoAlterno)
        Me.Panel3.Controls.Add(Me.Label25)
        Me.Panel3.Controls.Add(Me.Button4)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Controls.Add(Me.TextBox4)
        Me.Panel3.Controls.Add(Me.Label23)
        Me.Panel3.Controls.Add(Me.Label22)
        Me.Panel3.Controls.Add(Me.txtDesc_CodAlterno)
        Me.Panel3.Controls.Add(Me.txtArt_CodAlterno)
        Me.Panel3.Controls.Add(Me.Label21)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(1022, 109)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(587, 140)
        Me.Panel3.TabIndex = 54
        '
        'cmbCodigoAlterno
        '
        Me.cmbCodigoAlterno.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbCodigoAlterno.FormattingEnabled = True
        Me.cmbCodigoAlterno.Location = New System.Drawing.Point(141, 81)
        Me.cmbCodigoAlterno.Name = "cmbCodigoAlterno"
        Me.cmbCodigoAlterno.Size = New System.Drawing.Size(390, 21)
        Me.cmbCodigoAlterno.TabIndex = 61
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(17, 79)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(89, 13)
        Me.Label25.TabIndex = 60
        Me.Label25.Text = "Códigos Alternos:"
        '
        'Button4
        '
        Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), System.Drawing.Image)
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button4.Location = New System.Drawing.Point(546, 28)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(37, 33)
        Me.Button4.TabIndex = 59
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(17, 108)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(111, 13)
        Me.Label24.TabIndex = 58
        Me.Label24.Text = "Nuevo Código Alterno"
        '
        'TextBox4
        '
        Me.TextBox4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox4.Enabled = False
        Me.TextBox4.Location = New System.Drawing.Point(141, 108)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(390, 20)
        Me.TextBox4.TabIndex = 57
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(17, 53)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(66, 13)
        Me.Label23.TabIndex = 54
        Me.Label23.Text = "Descripción:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(17, 28)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(83, 13)
        Me.Label22.TabIndex = 54
        Me.Label22.Text = "Código Artículo:"
        '
        'txtDesc_CodAlterno
        '
        Me.txtDesc_CodAlterno.Enabled = False
        Me.txtDesc_CodAlterno.Location = New System.Drawing.Point(141, 54)
        Me.txtDesc_CodAlterno.Name = "txtDesc_CodAlterno"
        Me.txtDesc_CodAlterno.Size = New System.Drawing.Size(390, 20)
        Me.txtDesc_CodAlterno.TabIndex = 56
        '
        'txtArt_CodAlterno
        '
        Me.txtArt_CodAlterno.Enabled = False
        Me.txtArt_CodAlterno.Location = New System.Drawing.Point(141, 28)
        Me.txtArt_CodAlterno.Name = "txtArt_CodAlterno"
        Me.txtArt_CodAlterno.Size = New System.Drawing.Size(390, 20)
        Me.txtArt_CodAlterno.TabIndex = 55
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(17, 8)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(181, 13)
        Me.Label21.TabIndex = 54
        Me.Label21.Text = "AGREGAR CÓDIGO ALTERNO"
        '
        'frmAdminBarCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1609, 811)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PanelDatos)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.lblCveProveedor)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmAdminBarCode"
        Me.Text = "Administración de códigos de barra"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.PanelDatos.ResumeLayout(False)
        Me.PanelDatos.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.PanelSeleccion.ResumeLayout(False)
        CType(Me.dgSeleccion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtGInfCB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents NuevoCódigoDeBarrasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ModificarCódigoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GrabarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PanelDatos As Panel
    Friend WithEvents txtCBProveedor As TextBox
    Friend WithEvents txtCBTarima As TextBox
    Friend WithEvents txtCBCaja As TextBox
    Friend WithEvents txtCBPieza As TextBox
    Friend WithEvents txtCBInterno As TextBox
    Friend WithEvents txtProveedor As TextBox
    Friend WithEvents txtLinea As TextBox
    Friend WithEvents txtDescripcion As TextBox
    Friend WithEvents txtCategoria As TextBox
    Friend WithEvents txtArticulo As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblCveProveedor As Label
    Friend WithEvents TodosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DtGInfCB As DataGridView
    Friend WithEvents PanelSeleccion As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents dgSeleccion As DataGridView
    Friend WithEvents Label11 As Label
    Friend WithEvents txtCBBolsa As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtCantCaja As TextBox
    Friend WithEvents txtCantBolsa As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtCodigo As TextBox
    Friend WithEvents txtCantTR As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Label20 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents txtNombreArticulo As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label24 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents txtDesc_CodAlterno As TextBox
    Friend WithEvents txtArt_CodAlterno As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents cmbCodigoAlterno As ComboBox
    Friend WithEvents Label25 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents AgregarCódigoAlternoToolStripMenuItem As ToolStripMenuItem
End Class
