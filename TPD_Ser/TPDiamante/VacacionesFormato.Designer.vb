<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormatoVacaciones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormatoVacaciones))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TBFolio = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DTPFecSol = New System.Windows.Forms.DateTimePicker()
        Me.TBNumEmp = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CBNomEmp = New System.Windows.Forms.ComboBox()
        Me.DTPFecIng = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TBDiasVac = New System.Windows.Forms.TextBox()
        Me.TBFecIniVac = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TBFecCadVac = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TBDiasRest = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CBDiasSol = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TBDiasAut = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PanelFechas = New System.Windows.Forms.TableLayoutPanel()
        Me.DTPFec5 = New System.Windows.Forms.DateTimePicker()
        Me.DTPFec4 = New System.Windows.Forms.DateTimePicker()
        Me.DTPFec3 = New System.Windows.Forms.DateTimePicker()
        Me.CKAut5 = New System.Windows.Forms.CheckBox()
        Me.CKAut4 = New System.Windows.Forms.CheckBox()
        Me.CKAut3 = New System.Windows.Forms.CheckBox()
        Me.CKAut2 = New System.Windows.Forms.CheckBox()
        Me.DTPFec2 = New System.Windows.Forms.DateTimePicker()
        Me.DTPFec1 = New System.Windows.Forms.DateTimePicker()
        Me.CKAut1 = New System.Windows.Forms.CheckBox()
        Me.BSave = New System.Windows.Forms.Button()
        Me.BtnImprimir = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TBAntiguedad = New System.Windows.Forms.TextBox()
        Me.DGVCap = New System.Windows.Forms.DataGridView()
        Me.Folio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumEmp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Periodo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiaSol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Borrar = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.BtnNvo = New System.Windows.Forms.Button()
        Me.TBPeriodoCom = New System.Windows.Forms.ComboBox()
        Me.BtnEmpleados = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.lbltipo = New System.Windows.Forms.Label()
        Me.txttipo = New System.Windows.Forms.TextBox()
        Me.PanelFechas.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVCap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(173, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(322, 25)
        Me.Label1.TabIndex = 306
        Me.Label1.Text = "SOLICITUD DE VACACIONES"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(510, 49)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(38, 17)
        Me.Label13.TabIndex = 335
        Me.Label13.Text = "Folio"
        '
        'TBFolio
        '
        Me.TBFolio.Enabled = False
        Me.TBFolio.Location = New System.Drawing.Point(567, 48)
        Me.TBFolio.Name = "TBFolio"
        Me.TBFolio.Size = New System.Drawing.Size(115, 20)
        Me.TBFolio.TabIndex = 336
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(377, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 17)
        Me.Label6.TabIndex = 365
        Me.Label6.Text = "Fecha solicitud"
        '
        'DTPFecSol
        '
        Me.DTPFecSol.Enabled = False
        Me.DTPFecSol.Location = New System.Drawing.Point(483, 72)
        Me.DTPFecSol.Name = "DTPFecSol"
        Me.DTPFecSol.Size = New System.Drawing.Size(200, 20)
        Me.DTPFecSol.TabIndex = 364
        '
        'TBNumEmp
        '
        Me.TBNumEmp.Enabled = False
        Me.TBNumEmp.Location = New System.Drawing.Point(149, 100)
        Me.TBNumEmp.Name = "TBNumEmp"
        Me.TBNumEmp.Size = New System.Drawing.Size(115, 20)
        Me.TBNumEmp.TabIndex = 389
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "No. Empleado"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Nombre y Apellidos"
        '
        'CBNomEmp
        '
        Me.CBNomEmp.FormattingEnabled = True
        Me.CBNomEmp.Location = New System.Drawing.Point(149, 123)
        Me.CBNomEmp.Name = "CBNomEmp"
        Me.CBNomEmp.Size = New System.Drawing.Size(193, 21)
        Me.CBNomEmp.TabIndex = 1
        '
        'DTPFecIng
        '
        Me.DTPFecIng.Enabled = False
        Me.DTPFecIng.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFecIng.Location = New System.Drawing.Point(354, 154)
        Me.DTPFecIng.Name = "DTPFecIng"
        Me.DTPFecIng.Size = New System.Drawing.Size(115, 20)
        Me.DTPFecIng.TabIndex = 432
        Me.DTPFecIng.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 163)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Fecha de ingreso"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 204)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 13)
        Me.Label5.TabIndex = 451
        Me.Label5.Text = "Días de Vacaciones"
        '
        'TBDiasVac
        '
        Me.TBDiasVac.Enabled = False
        Me.TBDiasVac.Location = New System.Drawing.Point(149, 201)
        Me.TBDiasVac.Name = "TBDiasVac"
        Me.TBDiasVac.Size = New System.Drawing.Size(103, 20)
        Me.TBDiasVac.TabIndex = 450
        '
        'TBFecIniVac
        '
        Me.TBFecIniVac.Enabled = False
        Me.TBFecIniVac.Location = New System.Drawing.Point(149, 223)
        Me.TBFecIniVac.Name = "TBFecIniVac"
        Me.TBFecIniVac.Size = New System.Drawing.Size(103, 20)
        Me.TBFecIniVac.TabIndex = 468
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 226)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(136, 13)
        Me.Label7.TabIndex = 467
        Me.Label7.Text = "Periodo de vacaciones del "
        '
        'TBFecCadVac
        '
        Me.TBFecCadVac.Enabled = False
        Me.TBFecCadVac.Location = New System.Drawing.Point(268, 223)
        Me.TBFecCadVac.Name = "TBFecCadVac"
        Me.TBFecCadVac.Size = New System.Drawing.Size(103, 20)
        Me.TBFecCadVac.TabIndex = 483
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(253, 226)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(15, 13)
        Me.Label8.TabIndex = 482
        Me.Label8.Text = "al"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 248)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(125, 13)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Período que comprende:"
        '
        'TBDiasRest
        '
        Me.TBDiasRest.Enabled = False
        Me.TBDiasRest.Location = New System.Drawing.Point(149, 267)
        Me.TBDiasRest.Name = "TBDiasRest"
        Me.TBDiasRest.Size = New System.Drawing.Size(103, 20)
        Me.TBDiasRest.TabIndex = 507
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(17, 270)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(76, 13)
        Me.Label10.TabIndex = 506
        Me.Label10.Text = "Días restantes"
        '
        'CBDiasSol
        '
        Me.CBDiasSol.FormattingEnabled = True
        Me.CBDiasSol.Items.AddRange(New Object() {"", "1", "2", "3", "4", "5"})
        Me.CBDiasSol.Location = New System.Drawing.Point(115, 407)
        Me.CBDiasSol.Name = "CBDiasSol"
        Me.CBDiasSol.Size = New System.Drawing.Size(61, 21)
        Me.CBDiasSol.TabIndex = 519
        Me.CBDiasSol.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(17, 410)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 13)
        Me.Label11.TabIndex = 515
        Me.Label11.Text = "Días solicitados"
        Me.Label11.Visible = False
        '
        'TBDiasAut
        '
        Me.TBDiasAut.Enabled = False
        Me.TBDiasAut.Location = New System.Drawing.Point(115, 437)
        Me.TBDiasAut.Name = "TBDiasAut"
        Me.TBDiasAut.Size = New System.Drawing.Size(61, 20)
        Me.TBDiasAut.TabIndex = 528
        Me.TBDiasAut.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(17, 440)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(87, 13)
        Me.Label12.TabIndex = 527
        Me.Label12.Text = "Días autorizados"
        Me.Label12.Visible = False
        '
        'PanelFechas
        '
        Me.PanelFechas.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.PanelFechas.ColumnCount = 5
        Me.PanelFechas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93.0!))
        Me.PanelFechas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107.0!))
        Me.PanelFechas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.PanelFechas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.PanelFechas.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169.0!))
        Me.PanelFechas.Controls.Add(Me.DTPFec5, 4, 0)
        Me.PanelFechas.Controls.Add(Me.DTPFec4, 3, 0)
        Me.PanelFechas.Controls.Add(Me.DTPFec3, 2, 0)
        Me.PanelFechas.Controls.Add(Me.CKAut5, 4, 1)
        Me.PanelFechas.Controls.Add(Me.CKAut4, 3, 1)
        Me.PanelFechas.Controls.Add(Me.CKAut3, 2, 1)
        Me.PanelFechas.Controls.Add(Me.CKAut2, 1, 1)
        Me.PanelFechas.Controls.Add(Me.DTPFec2, 1, 0)
        Me.PanelFechas.Location = New System.Drawing.Point(495, 204)
        Me.PanelFechas.Name = "PanelFechas"
        Me.PanelFechas.RowCount = 2
        Me.PanelFechas.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.PanelFechas.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.PanelFechas.Size = New System.Drawing.Size(210, 54)
        Me.PanelFechas.TabIndex = 531
        Me.PanelFechas.Visible = False
        '
        'DTPFec5
        '
        Me.DTPFec5.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFec5.Location = New System.Drawing.Point(408, 4)
        Me.DTPFec5.Name = "DTPFec5"
        Me.DTPFec5.Size = New System.Drawing.Size(84, 20)
        Me.DTPFec5.TabIndex = 340
        Me.DTPFec5.Value = New Date(2015, 1, 1, 0, 0, 0, 0)
        Me.DTPFec5.Visible = False
        '
        'DTPFec4
        '
        Me.DTPFec4.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFec4.Location = New System.Drawing.Point(307, 4)
        Me.DTPFec4.Name = "DTPFec4"
        Me.DTPFec4.Size = New System.Drawing.Size(84, 20)
        Me.DTPFec4.TabIndex = 339
        Me.DTPFec4.Value = New Date(2015, 1, 1, 0, 0, 0, 0)
        Me.DTPFec4.Visible = False
        '
        'DTPFec3
        '
        Me.DTPFec3.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFec3.Location = New System.Drawing.Point(206, 4)
        Me.DTPFec3.Name = "DTPFec3"
        Me.DTPFec3.Size = New System.Drawing.Size(84, 20)
        Me.DTPFec3.TabIndex = 338
        Me.DTPFec3.Value = New Date(2015, 1, 1, 0, 0, 0, 0)
        Me.DTPFec3.Visible = False
        '
        'CKAut5
        '
        Me.CKAut5.AutoSize = True
        Me.CKAut5.Location = New System.Drawing.Point(408, 38)
        Me.CKAut5.Name = "CKAut5"
        Me.CKAut5.Size = New System.Drawing.Size(70, 12)
        Me.CKAut5.TabIndex = 337
        Me.CKAut5.Text = "Confirmar"
        Me.CKAut5.UseVisualStyleBackColor = True
        Me.CKAut5.Visible = False
        '
        'CKAut4
        '
        Me.CKAut4.AutoSize = True
        Me.CKAut4.Location = New System.Drawing.Point(307, 38)
        Me.CKAut4.Name = "CKAut4"
        Me.CKAut4.Size = New System.Drawing.Size(70, 12)
        Me.CKAut4.TabIndex = 336
        Me.CKAut4.Text = "Confirmar"
        Me.CKAut4.UseVisualStyleBackColor = True
        Me.CKAut4.Visible = False
        '
        'CKAut3
        '
        Me.CKAut3.AutoSize = True
        Me.CKAut3.Location = New System.Drawing.Point(206, 38)
        Me.CKAut3.Name = "CKAut3"
        Me.CKAut3.Size = New System.Drawing.Size(70, 12)
        Me.CKAut3.TabIndex = 335
        Me.CKAut3.Text = "Confirmar"
        Me.CKAut3.UseVisualStyleBackColor = True
        Me.CKAut3.Visible = False
        '
        'CKAut2
        '
        Me.CKAut2.AutoSize = True
        Me.CKAut2.Location = New System.Drawing.Point(98, 38)
        Me.CKAut2.Name = "CKAut2"
        Me.CKAut2.Size = New System.Drawing.Size(70, 12)
        Me.CKAut2.TabIndex = 334
        Me.CKAut2.Text = "Confirmar"
        Me.CKAut2.UseVisualStyleBackColor = True
        Me.CKAut2.Visible = False
        '
        'DTPFec2
        '
        Me.DTPFec2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFec2.Location = New System.Drawing.Point(98, 4)
        Me.DTPFec2.Name = "DTPFec2"
        Me.DTPFec2.Size = New System.Drawing.Size(84, 20)
        Me.DTPFec2.TabIndex = 329
        Me.DTPFec2.Value = New Date(2015, 1, 1, 0, 0, 0, 0)
        Me.DTPFec2.Visible = False
        '
        'DTPFec1
        '
        Me.DTPFec1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFec1.Location = New System.Drawing.Point(324, 327)
        Me.DTPFec1.Name = "DTPFec1"
        Me.DTPFec1.Size = New System.Drawing.Size(87, 20)
        Me.DTPFec1.TabIndex = 5
        Me.DTPFec1.Value = New Date(2015, 1, 1, 0, 0, 0, 0)
        '
        'CKAut1
        '
        Me.CKAut1.AutoSize = True
        Me.CKAut1.Location = New System.Drawing.Point(124, 379)
        Me.CKAut1.Name = "CKAut1"
        Me.CKAut1.Size = New System.Drawing.Size(70, 17)
        Me.CKAut1.TabIndex = 333
        Me.CKAut1.Text = "Confirmar"
        Me.CKAut1.UseVisualStyleBackColor = True
        Me.CKAut1.Visible = False
        '
        'BSave
        '
        Me.BSave.Image = CType(resources.GetObject("BSave.Image"), System.Drawing.Image)
        Me.BSave.Location = New System.Drawing.Point(645, 622)
        Me.BSave.Name = "BSave"
        Me.BSave.Size = New System.Drawing.Size(43, 35)
        Me.BSave.TabIndex = 534
        Me.BSave.UseVisualStyleBackColor = True
        '
        'BtnImprimir
        '
        Me.BtnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprimir.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnImprimir.Image = Global.TPDiamante.My.Resources.Resources.printer
        Me.BtnImprimir.Location = New System.Drawing.Point(594, 622)
        Me.BtnImprimir.Name = "BtnImprimir"
        Me.BtnImprimir.Size = New System.Drawing.Size(38, 35)
        Me.BtnImprimir.TabIndex = 535
        Me.BtnImprimir.Text = "            "
        Me.BtnImprimir.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(640, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(18, 18)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 538
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(662, 12)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(18, 18)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 539
        Me.PictureBox2.TabStop = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(17, 182)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(61, 13)
        Me.Label14.TabIndex = 540
        Me.Label14.Text = "Antiguedad"
        '
        'TBAntiguedad
        '
        Me.TBAntiguedad.Enabled = False
        Me.TBAntiguedad.Location = New System.Drawing.Point(149, 179)
        Me.TBAntiguedad.Name = "TBAntiguedad"
        Me.TBAntiguedad.Size = New System.Drawing.Size(103, 20)
        Me.TBAntiguedad.TabIndex = 541
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
        Me.DGVCap.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Folio, Me.NumEmp, Me.Periodo, Me.DiaSol, Me.Borrar})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVCap.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVCap.Location = New System.Drawing.Point(198, 379)
        Me.DGVCap.Name = "DGVCap"
        Me.DGVCap.RowHeadersVisible = False
        Me.DGVCap.RowTemplate.Height = 21
        Me.DGVCap.Size = New System.Drawing.Size(330, 208)
        Me.DGVCap.TabIndex = 7
        '
        'Folio
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.Folio.DefaultCellStyle = DataGridViewCellStyle2
        Me.Folio.HeaderText = "Folio"
        Me.Folio.Name = "Folio"
        Me.Folio.Width = 40
        '
        'NumEmp
        '
        Me.NumEmp.HeaderText = "NumEmp"
        Me.NumEmp.Name = "NumEmp"
        Me.NumEmp.ReadOnly = True
        Me.NumEmp.Width = 55
        '
        'Periodo
        '
        Me.Periodo.HeaderText = "Periodo"
        Me.Periodo.Name = "Periodo"
        Me.Periodo.ReadOnly = True
        Me.Periodo.Width = 80
        '
        'DiaSol
        '
        Me.DiaSol.DataPropertyName = "DiaSol"
        Me.DiaSol.HeaderText = "DiaSol"
        Me.DiaSol.Name = "DiaSol"
        Me.DiaSol.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DiaSol.Width = 70
        '
        'Borrar
        '
        Me.Borrar.HeaderText = "Borrar"
        Me.Borrar.Name = "Borrar"
        Me.Borrar.Width = 80
        '
        'BtnNvo
        '
        Me.BtnNvo.BackColor = System.Drawing.SystemColors.Control
        Me.BtnNvo.Enabled = False
        Me.BtnNvo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNvo.Location = New System.Drawing.Point(544, 623)
        Me.BtnNvo.Name = "BtnNvo"
        Me.BtnNvo.Size = New System.Drawing.Size(38, 35)
        Me.BtnNvo.TabIndex = 543
        Me.BtnNvo.Text = "&Nvo"
        Me.BtnNvo.UseVisualStyleBackColor = False
        '
        'TBPeriodoCom
        '
        Me.TBPeriodoCom.FormattingEnabled = True
        Me.TBPeriodoCom.Location = New System.Drawing.Point(268, 248)
        Me.TBPeriodoCom.Name = "TBPeriodoCom"
        Me.TBPeriodoCom.Size = New System.Drawing.Size(115, 21)
        Me.TBPeriodoCom.TabIndex = 3
        Me.TBPeriodoCom.Visible = False
        '
        'BtnEmpleados
        '
        Me.BtnEmpleados.BackColor = System.Drawing.Color.AliceBlue
        Me.BtnEmpleados.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEmpleados.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnEmpleados.Location = New System.Drawing.Point(483, 110)
        Me.BtnEmpleados.Name = "BtnEmpleados"
        Me.BtnEmpleados.Size = New System.Drawing.Size(135, 43)
        Me.BtnEmpleados.TabIndex = 545
        Me.BtnEmpleados.Text = "Asigne empleados."
        Me.BtnEmpleados.UseVisualStyleBackColor = False
        Me.BtnEmpleados.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(226, 341)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(80, 13)
        Me.Label15.TabIndex = 4
        Me.Label15.Text = "Días a solicitar:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(324, 352)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(87, 21)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Agregar Dia"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn1.HeaderText = "Folio"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 40
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "NumEmp"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Periodo"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 55
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "DiaSol"
        Me.DataGridViewTextBoxColumn4.HeaderText = "DiaSol"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn4.Width = 60
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "DiasRest"
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn5.HeaderText = "DiasRest"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 55
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(149, 157)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(103, 20)
        Me.TextBox1.TabIndex = 546
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = False
        Me.TextBox2.Location = New System.Drawing.Point(149, 245)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(103, 20)
        Me.TextBox2.TabIndex = 547
        '
        'lbltipo
        '
        Me.lbltipo.AutoSize = True
        Me.lbltipo.Location = New System.Drawing.Point(17, 300)
        Me.lbltipo.Name = "lbltipo"
        Me.lbltipo.Size = New System.Drawing.Size(31, 13)
        Me.lbltipo.TabIndex = 548
        Me.lbltipo.Text = "Tipo:"
        '
        'txttipo
        '
        Me.txttipo.Enabled = False
        Me.txttipo.Location = New System.Drawing.Point(149, 294)
        Me.txttipo.Name = "txttipo"
        Me.txttipo.Size = New System.Drawing.Size(100, 20)
        Me.txttipo.TabIndex = 549
        '
        'FormatoVacaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(717, 666)
        Me.Controls.Add(Me.txttipo)
        Me.Controls.Add(Me.lbltipo)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.BtnEmpleados)
        Me.Controls.Add(Me.TBPeriodoCom)
        Me.Controls.Add(Me.BtnNvo)
        Me.Controls.Add(Me.DGVCap)
        Me.Controls.Add(Me.TBAntiguedad)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.CKAut1)
        Me.Controls.Add(Me.DTPFec1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.BtnImprimir)
        Me.Controls.Add(Me.BSave)
        Me.Controls.Add(Me.PanelFechas)
        Me.Controls.Add(Me.TBDiasAut)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.CBDiasSol)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TBDiasRest)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TBFecCadVac)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TBFecIniVac)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TBDiasVac)
        Me.Controls.Add(Me.DTPFecIng)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CBNomEmp)
        Me.Controls.Add(Me.TBNumEmp)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DTPFecSol)
        Me.Controls.Add(Me.TBFolio)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormatoVacaciones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Solicitud Vacaciones"
        Me.PanelFechas.ResumeLayout(False)
        Me.PanelFechas.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVCap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TBFolio As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DTPFecSol As System.Windows.Forms.DateTimePicker
    Friend WithEvents TBNumEmp As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CBNomEmp As System.Windows.Forms.ComboBox
    Friend WithEvents DTPFecIng As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TBDiasVac As System.Windows.Forms.TextBox
    Friend WithEvents TBFecIniVac As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TBFecCadVac As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TBDiasRest As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CBDiasSol As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TBDiasAut As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents PanelFechas As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CKAut5 As System.Windows.Forms.CheckBox
    Friend WithEvents CKAut4 As System.Windows.Forms.CheckBox
    Friend WithEvents CKAut3 As System.Windows.Forms.CheckBox
    Friend WithEvents CKAut2 As System.Windows.Forms.CheckBox
    Friend WithEvents DTPFec2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPFec1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents CKAut1 As System.Windows.Forms.CheckBox
    Friend WithEvents BSave As System.Windows.Forms.Button
    Friend WithEvents DTPFec5 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPFec4 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPFec3 As System.Windows.Forms.DateTimePicker
    Friend WithEvents BtnImprimir As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TBAntiguedad As System.Windows.Forms.TextBox
    Friend WithEvents DGVCap As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnNvo As System.Windows.Forms.Button
    Friend WithEvents TBPeriodoCom As System.Windows.Forms.ComboBox
    Friend WithEvents BtnEmpleados As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Folio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NumEmp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Periodo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DiaSol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Borrar As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents lbltipo As System.Windows.Forms.Label
    Friend WithEvents txttipo As System.Windows.Forms.TextBox
End Class
