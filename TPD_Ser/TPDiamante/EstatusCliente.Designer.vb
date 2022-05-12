<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EstatusCliente
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
        Me.lPeriodo = New System.Windows.Forms.Label()
        Me.cmbAgente = New System.Windows.Forms.ComboBox()
        Me.dgRptEstatusClt = New System.Windows.Forms.DataGridView()
        Me.dtDel = New System.Windows.Forms.DateTimePicker()
        Me.dtAl = New System.Windows.Forms.DateTimePicker()
        Me.lAl = New System.Windows.Forms.Label()
        Me.lAgente = New System.Windows.Forms.Label()
        Me.lCliente = New System.Windows.Forms.Label()
        Me.cmbCliente = New System.Windows.Forms.ComboBox()
        Me.lDiasCred = New System.Windows.Forms.Label()
        Me.lLimCred = New System.Windows.Forms.Label()
        Me.bConsultar = New System.Windows.Forms.Button()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lPorPer4 = New System.Windows.Forms.Label()
        Me.lPorPer3 = New System.Windows.Forms.Label()
        Me.lPorPer2 = New System.Windows.Forms.Label()
        Me.lPorPer1 = New System.Windows.Forms.Label()
        Me.tPeriodo4 = New System.Windows.Forms.TextBox()
        Me.tPeriodo3 = New System.Windows.Forms.TextBox()
        Me.tPeriodo2 = New System.Windows.Forms.TextBox()
        Me.tPeriodo1 = New System.Windows.Forms.TextBox()
        Me.tPagado = New System.Windows.Forms.TextBox()
        Me.tCredito = New System.Windows.Forms.TextBox()
        Me.tSaldo = New System.Windows.Forms.TextBox()
        Me.tLimCred = New System.Windows.Forms.TextBox()
        Me.lTotalFac = New System.Windows.Forms.Label()
        Me.tDiasCred = New System.Windows.Forms.TextBox()
        Me.tFacturado = New System.Windows.Forms.TextBox()
        Me.lPeriodo4 = New System.Windows.Forms.Label()
        Me.lCredito = New System.Windows.Forms.Label()
        Me.lPeriodo3 = New System.Windows.Forms.Label()
        Me.lPeriodo2 = New System.Windows.Forms.Label()
        Me.lSaldo = New System.Windows.Forms.Label()
        Me.lPeriodo1 = New System.Windows.Forms.Label()
        Me.lInfClinte = New System.Windows.Forms.Label()
        Me.lTotalPag = New System.Windows.Forms.Label()
        Me.ckPenPago = New System.Windows.Forms.CheckBox()
        Me.bLimpiar = New System.Windows.Forms.Button()
        Me.eFin = New System.Windows.Forms.Label()
        Me.eIni = New System.Windows.Forms.Label()
        Me.eAgen = New System.Windows.Forms.Label()
        CType(Me.dgRptEstatusClt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lPeriodo
        '
        Me.lPeriodo.AutoSize = True
        Me.lPeriodo.Location = New System.Drawing.Point(-3, 3)
        Me.lPeriodo.Name = "lPeriodo"
        Me.lPeriodo.Size = New System.Drawing.Size(63, 13)
        Me.lPeriodo.TabIndex = 1
        Me.lPeriodo.Text = "Periodo del:"
        '
        'cmbAgente
        '
        Me.cmbAgente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbAgente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAgente.FormattingEnabled = True
        Me.cmbAgente.Location = New System.Drawing.Point(61, 27)
        Me.cmbAgente.Name = "cmbAgente"
        Me.cmbAgente.Size = New System.Drawing.Size(301, 21)
        Me.cmbAgente.TabIndex = 3
        '
        'dgRptEstatusClt
        '
        Me.dgRptEstatusClt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgRptEstatusClt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgRptEstatusClt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgRptEstatusClt.Location = New System.Drawing.Point(5, 136)
        Me.dgRptEstatusClt.Name = "dgRptEstatusClt"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgRptEstatusClt.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgRptEstatusClt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgRptEstatusClt.Size = New System.Drawing.Size(1040, 444)
        Me.dgRptEstatusClt.TabIndex = 3
        '
        'dtDel
        '
        Me.dtDel.Location = New System.Drawing.Point(62, 0)
        Me.dtDel.Name = "dtDel"
        Me.dtDel.Size = New System.Drawing.Size(200, 20)
        Me.dtDel.TabIndex = 1
        '
        'dtAl
        '
        Me.dtAl.Location = New System.Drawing.Point(335, 0)
        Me.dtAl.Name = "dtAl"
        Me.dtAl.Size = New System.Drawing.Size(210, 20)
        Me.dtAl.TabIndex = 2
        '
        'lAl
        '
        Me.lAl.AutoSize = True
        Me.lAl.Location = New System.Drawing.Point(316, 3)
        Me.lAl.Name = "lAl"
        Me.lAl.Size = New System.Drawing.Size(19, 13)
        Me.lAl.TabIndex = 0
        Me.lAl.Text = "Al:"
        '
        'lAgente
        '
        Me.lAgente.AutoSize = True
        Me.lAgente.Location = New System.Drawing.Point(15, 31)
        Me.lAgente.Name = "lAgente"
        Me.lAgente.Size = New System.Drawing.Size(44, 13)
        Me.lAgente.TabIndex = 8
        Me.lAgente.Text = "Agente:"
        '
        'lCliente
        '
        Me.lCliente.AutoSize = True
        Me.lCliente.Location = New System.Drawing.Point(18, 60)
        Me.lCliente.Name = "lCliente"
        Me.lCliente.Size = New System.Drawing.Size(42, 13)
        Me.lCliente.TabIndex = 9
        Me.lCliente.Text = "Cliente:"
        '
        'cmbCliente
        '
        Me.cmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.Location = New System.Drawing.Point(62, 55)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(483, 21)
        Me.cmbCliente.TabIndex = 7
        '
        'lDiasCred
        '
        Me.lDiasCred.AutoSize = True
        Me.lDiasCred.Location = New System.Drawing.Point(569, 52)
        Me.lDiasCred.Name = "lDiasCred"
        Me.lDiasCred.Size = New System.Drawing.Size(82, 13)
        Me.lDiasCred.TabIndex = 0
        Me.lDiasCred.Text = "Dias de Credito:"
        '
        'lLimCred
        '
        Me.lLimCred.AutoSize = True
        Me.lLimCred.Location = New System.Drawing.Point(562, 74)
        Me.lLimCred.Name = "lLimCred"
        Me.lLimCred.Size = New System.Drawing.Size(88, 13)
        Me.lLimCred.TabIndex = 12
        Me.lLimCred.Text = "Limite de Credito:"
        '
        'bConsultar
        '
        Me.bConsultar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.bConsultar.Location = New System.Drawing.Point(62, 86)
        Me.bConsultar.Name = "bConsultar"
        Me.bConsultar.Size = New System.Drawing.Size(71, 23)
        Me.bConsultar.TabIndex = 13
        Me.bConsultar.Text = "Consultar"
        Me.bConsultar.UseVisualStyleBackColor = True
        '
        'BtnExcel
        '
        Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnExcel.Location = New System.Drawing.Point(331, 82)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(32, 27)
        Me.BtnExcel.TabIndex = 14
        Me.BtnExcel.UseVisualStyleBackColor = True
        Me.BtnExcel.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lPorPer4)
        Me.Panel1.Controls.Add(Me.lPorPer3)
        Me.Panel1.Controls.Add(Me.lPorPer2)
        Me.Panel1.Controls.Add(Me.lPorPer1)
        Me.Panel1.Controls.Add(Me.tPeriodo4)
        Me.Panel1.Controls.Add(Me.tPeriodo3)
        Me.Panel1.Controls.Add(Me.tPeriodo2)
        Me.Panel1.Controls.Add(Me.tPeriodo1)
        Me.Panel1.Controls.Add(Me.tPagado)
        Me.Panel1.Controls.Add(Me.tCredito)
        Me.Panel1.Controls.Add(Me.tSaldo)
        Me.Panel1.Controls.Add(Me.tLimCred)
        Me.Panel1.Controls.Add(Me.lTotalFac)
        Me.Panel1.Controls.Add(Me.tDiasCred)
        Me.Panel1.Controls.Add(Me.tFacturado)
        Me.Panel1.Controls.Add(Me.lPeriodo4)
        Me.Panel1.Controls.Add(Me.lCredito)
        Me.Panel1.Controls.Add(Me.lPeriodo3)
        Me.Panel1.Controls.Add(Me.lPeriodo2)
        Me.Panel1.Controls.Add(Me.lSaldo)
        Me.Panel1.Controls.Add(Me.lPeriodo1)
        Me.Panel1.Controls.Add(Me.lInfClinte)
        Me.Panel1.Controls.Add(Me.lTotalPag)
        Me.Panel1.Controls.Add(Me.ckPenPago)
        Me.Panel1.Controls.Add(Me.bLimpiar)
        Me.Panel1.Controls.Add(Me.BtnExcel)
        Me.Panel1.Controls.Add(Me.bConsultar)
        Me.Panel1.Controls.Add(Me.eFin)
        Me.Panel1.Controls.Add(Me.eIni)
        Me.Panel1.Controls.Add(Me.lLimCred)
        Me.Panel1.Controls.Add(Me.lDiasCred)
        Me.Panel1.Controls.Add(Me.cmbCliente)
        Me.Panel1.Controls.Add(Me.lCliente)
        Me.Panel1.Controls.Add(Me.lAgente)
        Me.Panel1.Controls.Add(Me.lAl)
        Me.Panel1.Controls.Add(Me.dtAl)
        Me.Panel1.Controls.Add(Me.dtDel)
        Me.Panel1.Controls.Add(Me.cmbAgente)
        Me.Panel1.Controls.Add(Me.lPeriodo)
        Me.Panel1.Controls.Add(Me.eAgen)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Panel1.Size = New System.Drawing.Size(1041, 134)
        Me.Panel1.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(789, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 13)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "Dias posteriores a vencimiento"
        '
        'lPorPer4
        '
        Me.lPorPer4.Location = New System.Drawing.Point(980, 113)
        Me.lPorPer4.Name = "lPorPer4"
        Me.lPorPer4.Size = New System.Drawing.Size(55, 16)
        Me.lPorPer4.TabIndex = 47
        Me.lPorPer4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lPorPer3
        '
        Me.lPorPer3.Location = New System.Drawing.Point(980, 91)
        Me.lPorPer3.Name = "lPorPer3"
        Me.lPorPer3.Size = New System.Drawing.Size(55, 16)
        Me.lPorPer3.TabIndex = 47
        Me.lPorPer3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lPorPer2
        '
        Me.lPorPer2.Location = New System.Drawing.Point(980, 68)
        Me.lPorPer2.Name = "lPorPer2"
        Me.lPorPer2.Size = New System.Drawing.Size(55, 16)
        Me.lPorPer2.TabIndex = 47
        Me.lPorPer2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lPorPer1
        '
        Me.lPorPer1.Location = New System.Drawing.Point(980, 44)
        Me.lPorPer1.Name = "lPorPer1"
        Me.lPorPer1.Size = New System.Drawing.Size(55, 16)
        Me.lPorPer1.TabIndex = 46
        Me.lPorPer1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tPeriodo4
        '
        Me.tPeriodo4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tPeriodo4.Location = New System.Drawing.Point(864, 110)
        Me.tPeriodo4.Name = "tPeriodo4"
        Me.tPeriodo4.ReadOnly = True
        Me.tPeriodo4.Size = New System.Drawing.Size(110, 19)
        Me.tPeriodo4.TabIndex = 45
        Me.tPeriodo4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tPeriodo3
        '
        Me.tPeriodo3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tPeriodo3.Location = New System.Drawing.Point(864, 87)
        Me.tPeriodo3.Name = "tPeriodo3"
        Me.tPeriodo3.ReadOnly = True
        Me.tPeriodo3.Size = New System.Drawing.Size(110, 19)
        Me.tPeriodo3.TabIndex = 44
        Me.tPeriodo3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tPeriodo2
        '
        Me.tPeriodo2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tPeriodo2.Location = New System.Drawing.Point(864, 64)
        Me.tPeriodo2.Name = "tPeriodo2"
        Me.tPeriodo2.ReadOnly = True
        Me.tPeriodo2.Size = New System.Drawing.Size(110, 19)
        Me.tPeriodo2.TabIndex = 43
        Me.tPeriodo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tPeriodo1
        '
        Me.tPeriodo1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tPeriodo1.Location = New System.Drawing.Point(864, 41)
        Me.tPeriodo1.Name = "tPeriodo1"
        Me.tPeriodo1.ReadOnly = True
        Me.tPeriodo1.Size = New System.Drawing.Size(110, 19)
        Me.tPeriodo1.TabIndex = 42
        Me.tPeriodo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tPagado
        '
        Me.tPagado.AllowDrop = True
        Me.tPagado.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tPagado.Location = New System.Drawing.Point(864, 1)
        Me.tPagado.Name = "tPagado"
        Me.tPagado.ReadOnly = True
        Me.tPagado.Size = New System.Drawing.Size(110, 19)
        Me.tPagado.TabIndex = 41
        Me.tPagado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tCredito
        '
        Me.tCredito.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tCredito.Location = New System.Drawing.Point(653, 93)
        Me.tCredito.Name = "tCredito"
        Me.tCredito.ReadOnly = True
        Me.tCredito.Size = New System.Drawing.Size(110, 19)
        Me.tCredito.TabIndex = 40
        Me.tCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tSaldo
        '
        Me.tSaldo.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tSaldo.Location = New System.Drawing.Point(653, 4)
        Me.tSaldo.Name = "tSaldo"
        Me.tSaldo.ReadOnly = True
        Me.tSaldo.Size = New System.Drawing.Size(110, 19)
        Me.tSaldo.TabIndex = 39
        Me.tSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tLimCred
        '
        Me.tLimCred.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tLimCred.Location = New System.Drawing.Point(653, 70)
        Me.tLimCred.Name = "tLimCred"
        Me.tLimCred.ReadOnly = True
        Me.tLimCred.Size = New System.Drawing.Size(110, 19)
        Me.tLimCred.TabIndex = 38
        Me.tLimCred.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lTotalFac
        '
        Me.lTotalFac.AutoSize = True
        Me.lTotalFac.Location = New System.Drawing.Point(568, 29)
        Me.lTotalFac.Name = "lTotalFac"
        Me.lTotalFac.Size = New System.Drawing.Size(82, 13)
        Me.lTotalFac.TabIndex = 25
        Me.lTotalFac.Text = "Total facturado:"
        '
        'tDiasCred
        '
        Me.tDiasCred.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tDiasCred.Location = New System.Drawing.Point(653, 48)
        Me.tDiasCred.Name = "tDiasCred"
        Me.tDiasCred.ReadOnly = True
        Me.tDiasCred.Size = New System.Drawing.Size(110, 19)
        Me.tDiasCred.TabIndex = 37
        '
        'tFacturado
        '
        Me.tFacturado.AllowDrop = True
        Me.tFacturado.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tFacturado.Location = New System.Drawing.Point(653, 26)
        Me.tFacturado.Name = "tFacturado"
        Me.tFacturado.ReadOnly = True
        Me.tFacturado.Size = New System.Drawing.Size(110, 19)
        Me.tFacturado.TabIndex = 36
        Me.tFacturado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lPeriodo4
        '
        Me.lPeriodo4.AutoSize = True
        Me.lPeriodo4.Location = New System.Drawing.Point(776, 114)
        Me.lPeriodo4.Name = "lPeriodo4"
        Me.lPeriodo4.Size = New System.Drawing.Size(82, 13)
        Me.lPeriodo4.TabIndex = 35
        Me.lPeriodo4.Text = "Mas de 30 dias:"
        '
        'lCredito
        '
        Me.lCredito.AutoSize = True
        Me.lCredito.Location = New System.Drawing.Point(555, 96)
        Me.lCredito.Name = "lCredito"
        Me.lCredito.Size = New System.Drawing.Size(96, 13)
        Me.lCredito.TabIndex = 32
        Me.lCredito.Text = "Credito disponible :"
        '
        'lPeriodo3
        '
        Me.lPeriodo3.AutoSize = True
        Me.lPeriodo3.Location = New System.Drawing.Point(784, 91)
        Me.lPeriodo3.Name = "lPeriodo3"
        Me.lPeriodo3.Size = New System.Drawing.Size(78, 13)
        Me.lPeriodo3.TabIndex = 34
        Me.lPeriodo3.Text = "Hasta 30 dias: "
        '
        'lPeriodo2
        '
        Me.lPeriodo2.AutoSize = True
        Me.lPeriodo2.Location = New System.Drawing.Point(784, 68)
        Me.lPeriodo2.Name = "lPeriodo2"
        Me.lPeriodo2.Size = New System.Drawing.Size(78, 13)
        Me.lPeriodo2.TabIndex = 31
        Me.lPeriodo2.Text = "Hasta 15 dias: "
        '
        'lSaldo
        '
        Me.lSaldo.AutoSize = True
        Me.lSaldo.Location = New System.Drawing.Point(561, 9)
        Me.lSaldo.Name = "lSaldo"
        Me.lSaldo.Size = New System.Drawing.Size(89, 13)
        Me.lSaldo.TabIndex = 23
        Me.lSaldo.Text = "Saldo del Cliente:"
        '
        'lPeriodo1
        '
        Me.lPeriodo1.AutoSize = True
        Me.lPeriodo1.Location = New System.Drawing.Point(790, 45)
        Me.lPeriodo1.Name = "lPeriodo1"
        Me.lPeriodo1.Size = New System.Drawing.Size(72, 13)
        Me.lPeriodo1.TabIndex = 30
        Me.lPeriodo1.Text = "Hasta 5 dias: "
        '
        'lInfClinte
        '
        Me.lInfClinte.AutoSize = True
        Me.lInfClinte.Location = New System.Drawing.Point(2, 118)
        Me.lInfClinte.Name = "lInfClinte"
        Me.lInfClinte.Size = New System.Drawing.Size(10, 13)
        Me.lInfClinte.TabIndex = 18
        Me.lInfClinte.Text = " "
        '
        'lTotalPag
        '
        Me.lTotalPag.AutoSize = True
        Me.lTotalPag.Location = New System.Drawing.Point(786, 5)
        Me.lTotalPag.Name = "lTotalPag"
        Me.lTotalPag.Size = New System.Drawing.Size(74, 13)
        Me.lTotalPag.TabIndex = 29
        Me.lTotalPag.Text = "Total Pagado:"
        '
        'ckPenPago
        '
        Me.ckPenPago.AutoSize = True
        Me.ckPenPago.Checked = True
        Me.ckPenPago.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckPenPago.Location = New System.Drawing.Point(389, 31)
        Me.ckPenPago.Name = "ckPenPago"
        Me.ckPenPago.Size = New System.Drawing.Size(101, 17)
        Me.ckPenPago.TabIndex = 22
        Me.ckPenPago.Text = "Pendiente pago"
        Me.ckPenPago.UseVisualStyleBackColor = True
        '
        'bLimpiar
        '
        Me.bLimpiar.Location = New System.Drawing.Point(148, 85)
        Me.bLimpiar.Name = "bLimpiar"
        Me.bLimpiar.Size = New System.Drawing.Size(77, 24)
        Me.bLimpiar.TabIndex = 18
        Me.bLimpiar.Text = "Clave Cliente"
        Me.bLimpiar.UseVisualStyleBackColor = True
        '
        'eFin
        '
        Me.eFin.AutoSize = True
        Me.eFin.BackColor = System.Drawing.Color.Transparent
        Me.eFin.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eFin.ForeColor = System.Drawing.Color.Red
        Me.eFin.Location = New System.Drawing.Point(551, -1)
        Me.eFin.Name = "eFin"
        Me.eFin.Size = New System.Drawing.Size(13, 17)
        Me.eFin.TabIndex = 18
        Me.eFin.Text = "*"
        Me.eFin.Visible = False
        '
        'eIni
        '
        Me.eIni.AutoSize = True
        Me.eIni.BackColor = System.Drawing.Color.Transparent
        Me.eIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eIni.ForeColor = System.Drawing.Color.Red
        Me.eIni.Location = New System.Drawing.Point(261, -2)
        Me.eIni.Name = "eIni"
        Me.eIni.Size = New System.Drawing.Size(13, 17)
        Me.eIni.TabIndex = 17
        Me.eIni.Text = "*"
        Me.eIni.Visible = False
        '
        'eAgen
        '
        Me.eAgen.AutoSize = True
        Me.eAgen.BackColor = System.Drawing.Color.Transparent
        Me.eAgen.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eAgen.ForeColor = System.Drawing.Color.Red
        Me.eAgen.Location = New System.Drawing.Point(362, 53)
        Me.eAgen.Name = "eAgen"
        Me.eAgen.Size = New System.Drawing.Size(13, 17)
        Me.eAgen.TabIndex = 19
        Me.eAgen.Text = "*"
        Me.eAgen.Visible = False
        '
        'EstatusCliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1057, 582)
        Me.Controls.Add(Me.dgRptEstatusClt)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "EstatusCliente"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EstatusCliente"
        CType(Me.dgRptEstatusClt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lPeriodo As System.Windows.Forms.Label
    Friend WithEvents cmbAgente As System.Windows.Forms.ComboBox
    Friend WithEvents dgRptEstatusClt As System.Windows.Forms.DataGridView
    Friend WithEvents dtDel As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtAl As System.Windows.Forms.DateTimePicker
    Friend WithEvents lAl As System.Windows.Forms.Label
    Friend WithEvents lAgente As System.Windows.Forms.Label
    Friend WithEvents lCliente As System.Windows.Forms.Label
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents lDiasCred As System.Windows.Forms.Label
    Friend WithEvents lLimCred As System.Windows.Forms.Label
    Friend WithEvents bConsultar As System.Windows.Forms.Button
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents eIni As System.Windows.Forms.Label
    Friend WithEvents eFin As System.Windows.Forms.Label
    Friend WithEvents eAgen As System.Windows.Forms.Label
    Friend WithEvents bLimpiar As System.Windows.Forms.Button
    Friend WithEvents lInfClinte As System.Windows.Forms.Label
    Friend WithEvents lSaldo As System.Windows.Forms.Label
    Friend WithEvents ckPenPago As System.Windows.Forms.CheckBox
    Friend WithEvents lTotalFac As System.Windows.Forms.Label
    Friend WithEvents lTotalPag As System.Windows.Forms.Label
    Friend WithEvents lPeriodo1 As System.Windows.Forms.Label
    Friend WithEvents lPeriodo2 As System.Windows.Forms.Label
    Friend WithEvents lCredito As System.Windows.Forms.Label
    Friend WithEvents lPeriodo4 As System.Windows.Forms.Label
    Friend WithEvents lPeriodo3 As System.Windows.Forms.Label
    Friend WithEvents tSaldo As System.Windows.Forms.TextBox
    Friend WithEvents tLimCred As System.Windows.Forms.TextBox
    Friend WithEvents tDiasCred As System.Windows.Forms.TextBox
    Private WithEvents tFacturado As System.Windows.Forms.TextBox
    Friend WithEvents tPeriodo4 As System.Windows.Forms.TextBox
    Friend WithEvents tPeriodo3 As System.Windows.Forms.TextBox
    Friend WithEvents tPeriodo2 As System.Windows.Forms.TextBox
    Friend WithEvents tPeriodo1 As System.Windows.Forms.TextBox
    Private WithEvents tPagado As System.Windows.Forms.TextBox
    Friend WithEvents tCredito As System.Windows.Forms.TextBox
    Friend WithEvents lPorPer4 As System.Windows.Forms.Label
    Friend WithEvents lPorPer3 As System.Windows.Forms.Label
    Friend WithEvents lPorPer2 As System.Windows.Forms.Label
    Friend WithEvents lPorPer1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
