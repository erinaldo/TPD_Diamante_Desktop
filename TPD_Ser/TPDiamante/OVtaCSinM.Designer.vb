<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OVtaCSinM
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OVtaCSinM))
        Me.DGVEncOrdVta = New System.Windows.Forms.DataGridView()
        Me.DGVDetOrdVta = New System.Windows.Forms.DataGridView()
        Me.BtnGuardar = New System.Windows.Forms.Button()
        Me.LblVerSol = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnVerSol = New System.Windows.Forms.Button()
        Me.BtnActualizar = New System.Windows.Forms.Button()
        Me.LblActualizar = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CmbArticulo = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DGVAux = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.panelBotones = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnEnviarOC = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnGuardarEnviar = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ExportVend = New System.Windows.Forms.Button()
        Me.BtnRecibos = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.Process1 = New System.Diagnostics.Process()
        Me.Process2 = New System.Diagnostics.Process()
        Me.Process3 = New System.Diagnostics.Process()
        Me.Process4 = New System.Diagnostics.Process()
        Me.Process5 = New System.Diagnostics.Process()
        Me.Process6 = New System.Diagnostics.Process()
        Me.Process7 = New System.Diagnostics.Process()
        Me.Process8 = New System.Diagnostics.Process()
        Me.Process9 = New System.Diagnostics.Process()
        Me.Process10 = New System.Diagnostics.Process()
        Me.Process11 = New System.Diagnostics.Process()
        Me.Process12 = New System.Diagnostics.Process()
        Me.Process13 = New System.Diagnostics.Process()
        Me.Process14 = New System.Diagnostics.Process()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        CType(Me.DGVEncOrdVta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVDetOrdVta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVAux, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.panelBotones.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVEncOrdVta
        '
        Me.DGVEncOrdVta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVEncOrdVta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVEncOrdVta.Location = New System.Drawing.Point(3, 16)
        Me.DGVEncOrdVta.Name = "DGVEncOrdVta"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVEncOrdVta.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVEncOrdVta.Size = New System.Drawing.Size(1669, 343)
        Me.DGVEncOrdVta.TabIndex = 68
        '
        'DGVDetOrdVta
        '
        Me.DGVDetOrdVta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVDetOrdVta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVDetOrdVta.Location = New System.Drawing.Point(3, 16)
        Me.DGVDetOrdVta.Name = "DGVDetOrdVta"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVDetOrdVta.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVDetOrdVta.Size = New System.Drawing.Size(1669, 344)
        Me.DGVDetOrdVta.TabIndex = 69
        '
        'BtnGuardar
        '
        Me.BtnGuardar.BackColor = System.Drawing.Color.LightBlue
        Me.BtnGuardar.Enabled = False
        Me.BtnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGuardar.Location = New System.Drawing.Point(1605, 6)
        Me.BtnGuardar.Name = "BtnGuardar"
        Me.BtnGuardar.Size = New System.Drawing.Size(64, 47)
        Me.BtnGuardar.TabIndex = 73
        Me.BtnGuardar.Text = "Crear y Enviar Solicitud"
        Me.BtnGuardar.UseVisualStyleBackColor = False
        Me.BtnGuardar.Visible = False
        '
        'LblVerSol
        '
        Me.LblVerSol.AutoSize = True
        Me.LblVerSol.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVerSol.Location = New System.Drawing.Point(1371, 8)
        Me.LblVerSol.Name = "LblVerSol"
        Me.LblVerSol.Size = New System.Drawing.Size(61, 13)
        Me.LblVerSol.TabIndex = 72
        Me.LblVerSol.Text = "Visualizar"
        Me.LblVerSol.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(27, 536)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(278, 13)
        Me.Label1.TabIndex = 74
        Me.Label1.Text = "Ordenes de Venta Creadas Con Falta de Material"
        '
        'BtnVerSol
        '
        Me.BtnVerSol.Enabled = False
        Me.BtnVerSol.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnVerSol.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnVerSol.Image = Global.TPDiamante.My.Resources.Resources.printer
        Me.BtnVerSol.Location = New System.Drawing.Point(1556, 8)
        Me.BtnVerSol.Name = "BtnVerSol"
        Me.BtnVerSol.Size = New System.Drawing.Size(43, 39)
        Me.BtnVerSol.TabIndex = 71
        Me.BtnVerSol.Text = "            "
        Me.BtnVerSol.UseVisualStyleBackColor = True
        Me.BtnVerSol.Visible = False
        '
        'BtnActualizar
        '
        Me.BtnActualizar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnActualizar.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnActualizar.Image = Global.TPDiamante.My.Resources.Resources.Recharger
        Me.BtnActualizar.Location = New System.Drawing.Point(710, 8)
        Me.BtnActualizar.Name = "BtnActualizar"
        Me.BtnActualizar.Size = New System.Drawing.Size(43, 39)
        Me.BtnActualizar.TabIndex = 65
        Me.BtnActualizar.UseVisualStyleBackColor = True
        '
        'LblActualizar
        '
        Me.LblActualizar.AutoSize = True
        Me.LblActualizar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblActualizar.Location = New System.Drawing.Point(638, 31)
        Me.LblActualizar.Name = "LblActualizar"
        Me.LblActualizar.Size = New System.Drawing.Size(64, 13)
        Me.LblActualizar.TabIndex = 75
        Me.LblActualizar.Text = "Actualizar"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(27, 254)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(177, 13)
        Me.Label3.TabIndex = 76
        Me.Label3.Text = "Articulos sin cumplir demanda"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(293, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 17)
        Me.Label2.TabIndex = 80
        Me.Label2.Text = "Fecha Término:"
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaTer.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtpFechaTer.Location = New System.Drawing.Point(406, 18)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(181, 23)
        Me.DtpFechaTer.TabIndex = 78
        Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 17)
        Me.Label5.TabIndex = 79
        Me.Label5.Text = "Fecha Inicio:"
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.CustomFormat = "dd/MM/aaaa HH:mm:ss"
        Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtpFechaIni.Location = New System.Drawing.Point(93, 19)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(182, 23)
        Me.DtpFechaIni.TabIndex = 77
        Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(638, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 81
        Me.Label4.Text = "Buscar /"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.TPDiamante.My.Resources.Resources.Plus__6_
        Me.Button1.Location = New System.Drawing.Point(1507, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(43, 39)
        Me.Button1.TabIndex = 82
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'CmbArticulo
        '
        Me.CmbArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbArticulo.FormattingEnabled = True
        Me.CmbArticulo.Location = New System.Drawing.Point(1438, 9)
        Me.CmbArticulo.Name = "CmbArticulo"
        Me.CmbArticulo.Size = New System.Drawing.Size(63, 21)
        Me.CmbArticulo.TabIndex = 83
        Me.CmbArticulo.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1371, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 16)
        Me.Label6.TabIndex = 84
        Me.Label6.Text = "Artículo"
        Me.Label6.Visible = False
        '
        'DGVAux
        '
        Me.DGVAux.AllowUserToAddRows = False
        Me.DGVAux.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVAux.Location = New System.Drawing.Point(1438, 34)
        Me.DGVAux.Name = "DGVAux"
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVAux.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGVAux.Size = New System.Drawing.Size(63, 16)
        Me.DGVAux.TabIndex = 86
        Me.DGVAux.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.panelBotones)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.BtnActualizar)
        Me.Panel1.Controls.Add(Me.BtnVerSol)
        Me.Panel1.Controls.Add(Me.DGVAux)
        Me.Panel1.Controls.Add(Me.LblVerSol)
        Me.Panel1.Controls.Add(Me.BtnGuardar)
        Me.Panel1.Controls.Add(Me.CmbArticulo)
        Me.Panel1.Controls.Add(Me.LblActualizar)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.DtpFechaIni)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.DtpFechaTer)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1681, 62)
        Me.Panel1.TabIndex = 89
        '
        'panelBotones
        '
        Me.panelBotones.Controls.Add(Me.Label13)
        Me.panelBotones.Controls.Add(Me.btnEnviarOC)
        Me.panelBotones.Controls.Add(Me.Label12)
        Me.panelBotones.Controls.Add(Me.Button3)
        Me.panelBotones.Controls.Add(Me.Label11)
        Me.panelBotones.Controls.Add(Me.Label9)
        Me.panelBotones.Controls.Add(Me.Button2)
        Me.panelBotones.Controls.Add(Me.Label8)
        Me.panelBotones.Controls.Add(Me.btnGuardarEnviar)
        Me.panelBotones.Controls.Add(Me.Label7)
        Me.panelBotones.Controls.Add(Me.ExportVend)
        Me.panelBotones.Controls.Add(Me.BtnRecibos)
        Me.panelBotones.Location = New System.Drawing.Point(759, 6)
        Me.panelBotones.Name = "panelBotones"
        Me.panelBotones.Size = New System.Drawing.Size(473, 59)
        Me.panelBotones.TabIndex = 96
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(410, 37)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(60, 13)
        Me.Label13.TabIndex = 107
        Me.Label13.Text = "Enviar OC"
        '
        'btnEnviarOC
        '
        Me.btnEnviarOC.BackgroundImage = CType(resources.GetObject("btnEnviarOC.BackgroundImage"), System.Drawing.Image)
        Me.btnEnviarOC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnviarOC.Location = New System.Drawing.Point(422, 4)
        Me.btnEnviarOC.Name = "btnEnviarOC"
        Me.btnEnviarOC.Size = New System.Drawing.Size(39, 33)
        Me.btnEnviarOC.TabIndex = 106
        Me.btnEnviarOC.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(329, 32)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 26)
        Me.Label12.TabIndex = 105
        Me.Label12.Text = "Importar / confirmar"
        '
        'Button3
        '
        Me.Button3.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button3.Location = New System.Drawing.Point(337, 1)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(39, 33)
        Me.Button3.TabIndex = 104
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(35, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(41, 13)
        Me.Label11.TabIndex = 103
        Me.Label11.Text = "Todos"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(268, 34)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 13)
        Me.Label9.TabIndex = 102
        Me.Label9.Text = "Enviar"
        '
        'Button2
        '
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.Location = New System.Drawing.Point(270, 1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(39, 33)
        Me.Button2.TabIndex = 101
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(197, 34)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 13)
        Me.Label8.TabIndex = 100
        Me.Label8.Text = "Guardar "
        '
        'btnGuardarEnviar
        '
        Me.btnGuardarEnviar.BackColor = System.Drawing.Color.White
        Me.btnGuardarEnviar.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
        Me.btnGuardarEnviar.Location = New System.Drawing.Point(200, 1)
        Me.btnGuardarEnviar.Name = "btnGuardarEnviar"
        Me.btnGuardarEnviar.Size = New System.Drawing.Size(39, 33)
        Me.btnGuardarEnviar.TabIndex = 99
        Me.btnGuardarEnviar.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(99, 34)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 13)
        Me.Label7.TabIndex = 98
        Me.Label7.Text = "Por Vendedor"
        '
        'ExportVend
        '
        Me.ExportVend.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.ExportVend.Location = New System.Drawing.Point(121, 1)
        Me.ExportVend.Name = "ExportVend"
        Me.ExportVend.Size = New System.Drawing.Size(39, 33)
        Me.ExportVend.TabIndex = 97
        Me.ExportVend.UseVisualStyleBackColor = True
        '
        'BtnRecibos
        '
        Me.BtnRecibos.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnRecibos.Location = New System.Drawing.Point(35, 1)
        Me.BtnRecibos.Name = "BtnRecibos"
        Me.BtnRecibos.Size = New System.Drawing.Size(39, 33)
        Me.BtnRecibos.TabIndex = 96
        Me.BtnRecibos.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 62)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1681, 737)
        Me.TableLayoutPanel1.TabIndex = 90
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DGVEncOrdVta)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1675, 362)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Articulos sin cumplir demanda"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DGVDetOrdVta)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 371)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1675, 363)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Ordenes de Venta Creadas Con Falta de Material"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Process1
        '
        Me.Process1.StartInfo.Domain = ""
        Me.Process1.StartInfo.LoadUserProfile = False
        Me.Process1.StartInfo.Password = Nothing
        Me.Process1.StartInfo.StandardErrorEncoding = Nothing
        Me.Process1.StartInfo.StandardOutputEncoding = Nothing
        Me.Process1.StartInfo.UserName = ""
        Me.Process1.SynchronizingObject = Me
        '
        'Process2
        '
        Me.Process2.StartInfo.Domain = ""
        Me.Process2.StartInfo.LoadUserProfile = False
        Me.Process2.StartInfo.Password = Nothing
        Me.Process2.StartInfo.StandardErrorEncoding = Nothing
        Me.Process2.StartInfo.StandardOutputEncoding = Nothing
        Me.Process2.StartInfo.UserName = ""
        Me.Process2.SynchronizingObject = Me
        '
        'Process3
        '
        Me.Process3.StartInfo.Domain = ""
        Me.Process3.StartInfo.LoadUserProfile = False
        Me.Process3.StartInfo.Password = Nothing
        Me.Process3.StartInfo.StandardErrorEncoding = Nothing
        Me.Process3.StartInfo.StandardOutputEncoding = Nothing
        Me.Process3.StartInfo.UserName = ""
        Me.Process3.SynchronizingObject = Me
        '
        'Process4
        '
        Me.Process4.StartInfo.Domain = ""
        Me.Process4.StartInfo.LoadUserProfile = False
        Me.Process4.StartInfo.Password = Nothing
        Me.Process4.StartInfo.StandardErrorEncoding = Nothing
        Me.Process4.StartInfo.StandardOutputEncoding = Nothing
        Me.Process4.StartInfo.UserName = ""
        Me.Process4.SynchronizingObject = Me
        '
        'Process5
        '
        Me.Process5.StartInfo.Domain = ""
        Me.Process5.StartInfo.LoadUserProfile = False
        Me.Process5.StartInfo.Password = Nothing
        Me.Process5.StartInfo.StandardErrorEncoding = Nothing
        Me.Process5.StartInfo.StandardOutputEncoding = Nothing
        Me.Process5.StartInfo.UserName = ""
        Me.Process5.SynchronizingObject = Me
        '
        'Process6
        '
        Me.Process6.StartInfo.Domain = ""
        Me.Process6.StartInfo.LoadUserProfile = False
        Me.Process6.StartInfo.Password = Nothing
        Me.Process6.StartInfo.StandardErrorEncoding = Nothing
        Me.Process6.StartInfo.StandardOutputEncoding = Nothing
        Me.Process6.StartInfo.UserName = ""
        Me.Process6.SynchronizingObject = Me
        '
        'Process7
        '
        Me.Process7.StartInfo.Domain = ""
        Me.Process7.StartInfo.LoadUserProfile = False
        Me.Process7.StartInfo.Password = Nothing
        Me.Process7.StartInfo.StandardErrorEncoding = Nothing
        Me.Process7.StartInfo.StandardOutputEncoding = Nothing
        Me.Process7.StartInfo.UserName = ""
        Me.Process7.SynchronizingObject = Me
        '
        'Process8
        '
        Me.Process8.StartInfo.Domain = ""
        Me.Process8.StartInfo.LoadUserProfile = False
        Me.Process8.StartInfo.Password = Nothing
        Me.Process8.StartInfo.StandardErrorEncoding = Nothing
        Me.Process8.StartInfo.StandardOutputEncoding = Nothing
        Me.Process8.StartInfo.UserName = ""
        Me.Process8.SynchronizingObject = Me
        '
        'Process9
        '
        Me.Process9.StartInfo.Domain = ""
        Me.Process9.StartInfo.LoadUserProfile = False
        Me.Process9.StartInfo.Password = Nothing
        Me.Process9.StartInfo.StandardErrorEncoding = Nothing
        Me.Process9.StartInfo.StandardOutputEncoding = Nothing
        Me.Process9.StartInfo.UserName = ""
        Me.Process9.SynchronizingObject = Me
        '
        'Process10
        '
        Me.Process10.StartInfo.Domain = ""
        Me.Process10.StartInfo.LoadUserProfile = False
        Me.Process10.StartInfo.Password = Nothing
        Me.Process10.StartInfo.StandardErrorEncoding = Nothing
        Me.Process10.StartInfo.StandardOutputEncoding = Nothing
        Me.Process10.StartInfo.UserName = ""
        Me.Process10.SynchronizingObject = Me
        '
        'Process11
        '
        Me.Process11.StartInfo.Domain = ""
        Me.Process11.StartInfo.LoadUserProfile = False
        Me.Process11.StartInfo.Password = Nothing
        Me.Process11.StartInfo.StandardErrorEncoding = Nothing
        Me.Process11.StartInfo.StandardOutputEncoding = Nothing
        Me.Process11.StartInfo.UserName = ""
        Me.Process11.SynchronizingObject = Me
        '
        'Process12
        '
        Me.Process12.StartInfo.Domain = ""
        Me.Process12.StartInfo.LoadUserProfile = False
        Me.Process12.StartInfo.Password = Nothing
        Me.Process12.StartInfo.StandardErrorEncoding = Nothing
        Me.Process12.StartInfo.StandardOutputEncoding = Nothing
        Me.Process12.StartInfo.UserName = ""
        Me.Process12.SynchronizingObject = Me
        '
        'Process13
        '
        Me.Process13.StartInfo.Domain = ""
        Me.Process13.StartInfo.LoadUserProfile = False
        Me.Process13.StartInfo.Password = Nothing
        Me.Process13.StartInfo.StandardErrorEncoding = Nothing
        Me.Process13.StartInfo.StandardOutputEncoding = Nothing
        Me.Process13.StartInfo.UserName = ""
        Me.Process13.SynchronizingObject = Me
        '
        'Process14
        '
        Me.Process14.StartInfo.Domain = ""
        Me.Process14.StartInfo.LoadUserProfile = False
        Me.Process14.StartInfo.Password = Nothing
        Me.Process14.StartInfo.StandardErrorEncoding = Nothing
        Me.Process14.StartInfo.StandardOutputEncoding = Nothing
        Me.Process14.StartInfo.UserName = ""
        Me.Process14.SynchronizingObject = Me
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'OVtaCSinM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1681, 799)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Name = "OVtaCSinM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Solicitud de Artículos"
        CType(Me.DGVEncOrdVta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVDetOrdVta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVAux, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.panelBotones.ResumeLayout(False)
        Me.panelBotones.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGVEncOrdVta As System.Windows.Forms.DataGridView
    Friend WithEvents BtnActualizar As System.Windows.Forms.Button
    Friend WithEvents DGVDetOrdVta As System.Windows.Forms.DataGridView
    Friend WithEvents BtnGuardar As System.Windows.Forms.Button
    Friend WithEvents LblVerSol As System.Windows.Forms.Label
    Friend WithEvents BtnVerSol As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LblActualizar As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CmbArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DGVAux As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label10 As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents panelBotones As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents btnGuardarEnviar As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents ExportVend As Button
    Friend WithEvents BtnRecibos As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents btnEnviarOC As Button
    Friend WithEvents Process1 As Process
    Friend WithEvents Process2 As Process
    Friend WithEvents Process3 As Process
    Friend WithEvents Process4 As Process
    Friend WithEvents Process5 As Process
    Friend WithEvents Process6 As Process
    Friend WithEvents Process7 As Process
    Friend WithEvents Process8 As Process
    Friend WithEvents Process9 As Process
    Friend WithEvents Process10 As Process
    Friend WithEvents Process11 As Process
    Friend WithEvents Process12 As Process
    Friend WithEvents Process13 As Process
    Friend WithEvents Process14 As Process
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
End Class
