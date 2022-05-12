<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DetDevolucionSeg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DetDevolucionSeg))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CBDictamen = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.DTPFecDic = New System.Windows.Forms.DateTimePicker()
        Me.BSave = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.TBNumGuia = New System.Windows.Forms.TextBox()
        Me.DTPEnvCli = New System.Windows.Forms.DateTimePicker()
        Me.TBDiasTransGuia = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.TBDiasTransTot = New System.Windows.Forms.TextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.dtpFechaEntregaCobranza = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TBDiasTransAlm2 = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.TBFecTrasp = New System.Windows.Forms.TextBox()
        Me.TBNumTrasp = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.TBFecRespCli = New System.Windows.Forms.DateTimePicker()
        Me.TBNumDoc = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rbDetalle = New System.Windows.Forms.RadioButton()
        Me.rbOptimas = New System.Windows.Forms.RadioButton()
        Me.cbCondiciones = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DTPFecIniRev = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DTPRecComp = New System.Windows.Forms.DateTimePicker()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.cbCausa = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TBCantidad = New System.Windows.Forms.TextBox()
        Me.TBItemName = New System.Windows.Forms.TextBox()
        Me.TBItemCode = New System.Windows.Forms.TextBox()
        Me.TBCardName = New System.Windows.Forms.TextBox()
        Me.TBCardCode = New System.Windows.Forms.TextBox()
        Me.TBFolio = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cbx4 = New System.Windows.Forms.CheckBox()
        Me.cbx3 = New System.Windows.Forms.CheckBox()
        Me.cbx2 = New System.Windows.Forms.CheckBox()
        Me.cbx1 = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CBDictamen)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label31)
        Me.GroupBox1.Controls.Add(Me.DTPFecDic)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 443)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(687, 46)
        Me.GroupBox1.TabIndex = 346
        Me.GroupBox1.TabStop = False
        '
        'CBDictamen
        '
        Me.CBDictamen.FormattingEnabled = True
        Me.CBDictamen.Items.AddRange(New Object() {"Pendiente autorización", "Sí procede", "No procede"})
        Me.CBDictamen.Location = New System.Drawing.Point(125, 19)
        Me.CBDictamen.Name = "CBDictamen"
        Me.CBDictamen.Size = New System.Drawing.Size(86, 21)
        Me.CBDictamen.TabIndex = 53
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(13, 19)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 13)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "Dictamen" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(241, 16)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(52, 26)
        Me.Label31.TabIndex = 50
        Me.Label31.Text = "Fecha" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Dictamen"
        '
        'DTPFecDic
        '
        Me.DTPFecDic.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFecDic.Location = New System.Drawing.Point(320, 20)
        Me.DTPFecDic.Name = "DTPFecDic"
        Me.DTPFecDic.Size = New System.Drawing.Size(93, 20)
        Me.DTPFecDic.TabIndex = 52
        '
        'BSave
        '
        Me.BSave.Image = CType(resources.GetObject("BSave.Image"), System.Drawing.Image)
        Me.BSave.Location = New System.Drawing.Point(622, 34)
        Me.BSave.Name = "BSave"
        Me.BSave.Size = New System.Drawing.Size(43, 35)
        Me.BSave.TabIndex = 345
        Me.BSave.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.TBNumGuia)
        Me.GroupBox7.Controls.Add(Me.DTPEnvCli)
        Me.GroupBox7.Controls.Add(Me.TBDiasTransGuia)
        Me.GroupBox7.Controls.Add(Me.Label44)
        Me.GroupBox7.Controls.Add(Me.Label45)
        Me.GroupBox7.Controls.Add(Me.Label47)
        Me.GroupBox7.Location = New System.Drawing.Point(22, 641)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(432, 74)
        Me.GroupBox7.TabIndex = 342
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "No procede"
        '
        'TBNumGuia
        '
        Me.TBNumGuia.Location = New System.Drawing.Point(125, 48)
        Me.TBNumGuia.Name = "TBNumGuia"
        Me.TBNumGuia.Size = New System.Drawing.Size(92, 20)
        Me.TBNumGuia.TabIndex = 324
        '
        'DTPEnvCli
        '
        Me.DTPEnvCli.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPEnvCli.Location = New System.Drawing.Point(10, 48)
        Me.DTPEnvCli.Name = "DTPEnvCli"
        Me.DTPEnvCli.Size = New System.Drawing.Size(93, 20)
        Me.DTPEnvCli.TabIndex = 64
        '
        'TBDiasTransGuia
        '
        Me.TBDiasTransGuia.Enabled = False
        Me.TBDiasTransGuia.Location = New System.Drawing.Point(244, 48)
        Me.TBDiasTransGuia.Name = "TBDiasTransGuia"
        Me.TBDiasTransGuia.Size = New System.Drawing.Size(46, 20)
        Me.TBDiasTransGuia.TabIndex = 326
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(6, 16)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(78, 13)
        Me.Label44.TabIndex = 7
        Me.Label44.Text = "Envio a Cliente"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(122, 16)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(41, 13)
        Me.Label45.TabIndex = 6
        Me.Label45.Text = "# Guía"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(241, 16)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(71, 26)
        Me.Label47.TabIndex = 4
        Me.Label47.Text = "Días " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Transcurridos" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'TBDiasTransTot
        '
        Me.TBDiasTransTot.Enabled = False
        Me.TBDiasTransTot.Location = New System.Drawing.Point(456, 686)
        Me.TBDiasTransTot.Name = "TBDiasTransTot"
        Me.TBDiasTransTot.Size = New System.Drawing.Size(58, 20)
        Me.TBDiasTransTot.TabIndex = 343
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.dtpFechaEntregaCobranza)
        Me.GroupBox5.Controls.Add(Me.Label2)
        Me.GroupBox5.Controls.Add(Me.TBDiasTransAlm2)
        Me.GroupBox5.Controls.Add(Me.Label29)
        Me.GroupBox5.Controls.Add(Me.TBFecTrasp)
        Me.GroupBox5.Controls.Add(Me.TBNumTrasp)
        Me.GroupBox5.Controls.Add(Me.Label30)
        Me.GroupBox5.Controls.Add(Me.TBFecRespCli)
        Me.GroupBox5.Controls.Add(Me.TBNumDoc)
        Me.GroupBox5.Controls.Add(Me.Label36)
        Me.GroupBox5.Controls.Add(Me.Label38)
        Me.GroupBox5.Controls.Add(Me.Label39)
        Me.GroupBox5.Location = New System.Drawing.Point(22, 495)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(687, 137)
        Me.GroupBox5.TabIndex = 340
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Sí procede"
        '
        'dtpFechaEntregaCobranza
        '
        Me.dtpFechaEntregaCobranza.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaEntregaCobranza.Location = New System.Drawing.Point(125, 36)
        Me.dtpFechaEntregaCobranza.Name = "dtpFechaEntregaCobranza"
        Me.dtpFechaEntregaCobranza.Size = New System.Drawing.Size(88, 20)
        Me.dtpFechaEntregaCobranza.TabIndex = 454
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 26)
        Me.Label2.TabIndex = 453
        Me.Label2.Text = "Fecha Entrega" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "a Cobranza"
        '
        'TBDiasTransAlm2
        '
        Me.TBDiasTransAlm2.Enabled = False
        Me.TBDiasTransAlm2.Location = New System.Drawing.Point(534, 36)
        Me.TBDiasTransAlm2.Name = "TBDiasTransAlm2"
        Me.TBDiasTransAlm2.Size = New System.Drawing.Size(46, 20)
        Me.TBDiasTransAlm2.TabIndex = 452
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(431, 36)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(97, 26)
        Me.Label29.TabIndex = 423
        Me.Label29.Text = "Días Transcurridos" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Resp. Cliente" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'TBFecTrasp
        '
        Me.TBFecTrasp.Enabled = False
        Me.TBFecTrasp.Location = New System.Drawing.Point(320, 78)
        Me.TBFecTrasp.Name = "TBFecTrasp"
        Me.TBFecTrasp.Size = New System.Drawing.Size(95, 20)
        Me.TBFecTrasp.TabIndex = 450
        '
        'TBNumTrasp
        '
        Me.TBNumTrasp.Enabled = False
        Me.TBNumTrasp.Location = New System.Drawing.Point(320, 39)
        Me.TBNumTrasp.Name = "TBNumTrasp"
        Me.TBNumTrasp.Size = New System.Drawing.Size(93, 20)
        Me.TBNumTrasp.TabIndex = 449
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(14, 111)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(37, 13)
        Me.Label30.TabIndex = 429
        Me.Label30.Text = "Fecha"
        '
        'TBFecRespCli
        '
        Me.TBFecRespCli.Enabled = False
        Me.TBFecRespCli.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.TBFecRespCli.Location = New System.Drawing.Point(125, 105)
        Me.TBFecRespCli.Name = "TBFecRespCli"
        Me.TBFecRespCli.Size = New System.Drawing.Size(88, 20)
        Me.TBFecRespCli.TabIndex = 448
        Me.TBFecRespCli.Value = New Date(2015, 1, 1, 0, 0, 0, 0)
        '
        'TBNumDoc
        '
        Me.TBNumDoc.Enabled = False
        Me.TBNumDoc.Location = New System.Drawing.Point(125, 74)
        Me.TBNumDoc.Name = "TBNumDoc"
        Me.TBNumDoc.Size = New System.Drawing.Size(88, 20)
        Me.TBNumDoc.TabIndex = 447
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(14, 68)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(62, 26)
        Me.Label36.TabIndex = 424
        Me.Label36.Text = "Número de " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Documento"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(241, 36)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(62, 26)
        Me.Label38.TabIndex = 421
        Me.Label38.Text = "Número de " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Traspaso"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(241, 81)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(37, 13)
        Me.Label39.TabIndex = 420
        Me.Label39.Text = "Fecha"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(453, 657)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(97, 26)
        Me.Label24.TabIndex = 341
        Me.Label24.Text = "Días Transcurridos" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Totales"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rbDetalle)
        Me.GroupBox4.Controls.Add(Me.rbOptimas)
        Me.GroupBox4.Controls.Add(Me.cbCondiciones)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.DTPFecIniRev)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.DTPRecComp)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Location = New System.Drawing.Point(22, 240)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(684, 73)
        Me.GroupBox4.TabIndex = 339
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Almacén"
        '
        'rbDetalle
        '
        Me.rbDetalle.AutoSize = True
        Me.rbDetalle.Location = New System.Drawing.Point(354, 45)
        Me.rbDetalle.Name = "rbDetalle"
        Me.rbDetalle.Size = New System.Drawing.Size(78, 17)
        Me.rbDetalle.TabIndex = 348
        Me.rbDetalle.TabStop = True
        Me.rbDetalle.Text = "Con detalle"
        Me.rbDetalle.UseVisualStyleBackColor = True
        '
        'rbOptimas
        '
        Me.rbOptimas.AutoSize = True
        Me.rbOptimas.Location = New System.Drawing.Point(270, 45)
        Me.rbOptimas.Name = "rbOptimas"
        Me.rbOptimas.Size = New System.Drawing.Size(63, 17)
        Me.rbOptimas.TabIndex = 6
        Me.rbOptimas.TabStop = True
        Me.rbOptimas.Text = "Óptimas"
        Me.rbOptimas.UseVisualStyleBackColor = True
        '
        'cbCondiciones
        '
        Me.cbCondiciones.FormattingEnabled = True
        Me.cbCondiciones.Items.AddRange(New Object() {"ROTA", "INCOMPLETA", "MALTRATADA", "USADA", "EMPAQUE DAÑADO/MALTRATADO", "OTRA"})
        Me.cbCondiciones.Location = New System.Drawing.Point(476, 45)
        Me.cbCondiciones.Name = "cbCondiciones"
        Me.cbCondiciones.Size = New System.Drawing.Size(147, 21)
        Me.cbCondiciones.TabIndex = 347
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(267, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(148, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Condiciones de la devolucíón"
        '
        'DTPFecIniRev
        '
        Me.DTPFecIniRev.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFecIniRev.Location = New System.Drawing.Point(119, 47)
        Me.DTPFecIniRev.Name = "DTPFecIniRev"
        Me.DTPFecIniRev.Size = New System.Drawing.Size(92, 20)
        Me.DTPFecIniRev.TabIndex = 42
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(118, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 26)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Fecha inicio de" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "revisión"
        '
        'DTPRecComp
        '
        Me.DTPRecComp.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPRecComp.Location = New System.Drawing.Point(11, 45)
        Me.DTPRecComp.Name = "DTPRecComp"
        Me.DTPRecComp.Size = New System.Drawing.Size(92, 20)
        Me.DTPRecComp.TabIndex = 40
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(8, 16)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(95, 39)
        Me.Label17.TabIndex = 39
        Me.Label17.Text = "Fecha Recepción " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Almacén" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.txtObservaciones)
        Me.GroupBox3.Controls.Add(Me.cbCausa)
        Me.GroupBox3.Controls.Add(Me.BSave)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.TBCantidad)
        Me.GroupBox3.Controls.Add(Me.TBItemName)
        Me.GroupBox3.Controls.Add(Me.TBItemCode)
        Me.GroupBox3.Controls.Add(Me.TBCardName)
        Me.GroupBox3.Controls.Add(Me.TBCardCode)
        Me.GroupBox3.Controls.Add(Me.TBFolio)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.Label22)
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Controls.Add(Me.Label20)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(693, 222)
        Me.GroupBox3.TabIndex = 338
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Almacén"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 348
        Me.Label5.Text = "Observaciones"
        '
        'txtObservaciones
        '
        Me.txtObservaciones.Location = New System.Drawing.Point(123, 126)
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(197, 68)
        Me.txtObservaciones.TabIndex = 347
        '
        'cbCausa
        '
        Me.cbCausa.FormattingEnabled = True
        Me.cbCausa.Items.AddRange(New Object() {"ERROR SURTIDO", "NO SOLICITADO (VENTAS)", "NO SOLICITADO (CLIENTE)", "SURTIDO FUERA DE TIEMPO", "ERROR DE PEDIDO (C)", "ERROR DE PEDIDO (V)", "CARACTERÍTICAS DISTINTAS  LAS REQUERIDAS (CLIENTE)"})
        Me.cbCausa.Location = New System.Drawing.Point(345, 81)
        Me.cbCausa.Name = "cbCausa"
        Me.cbCausa.Size = New System.Drawing.Size(330, 21)
        Me.cbCausa.TabIndex = 346
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(342, 65)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(107, 13)
        Me.Label18.TabIndex = 43
        Me.Label18.Text = "Causa de devolución" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'TBCantidad
        '
        Me.TBCantidad.Location = New System.Drawing.Point(276, 81)
        Me.TBCantidad.Name = "TBCantidad"
        Me.TBCantidad.ReadOnly = True
        Me.TBCantidad.Size = New System.Drawing.Size(46, 20)
        Me.TBCantidad.TabIndex = 42
        '
        'TBItemName
        '
        Me.TBItemName.Location = New System.Drawing.Point(123, 81)
        Me.TBItemName.Name = "TBItemName"
        Me.TBItemName.ReadOnly = True
        Me.TBItemName.Size = New System.Drawing.Size(150, 20)
        Me.TBItemName.TabIndex = 41
        '
        'TBItemCode
        '
        Me.TBItemCode.Location = New System.Drawing.Point(16, 81)
        Me.TBItemCode.Name = "TBItemCode"
        Me.TBItemCode.ReadOnly = True
        Me.TBItemCode.Size = New System.Drawing.Size(92, 20)
        Me.TBItemCode.TabIndex = 40
        '
        'TBCardName
        '
        Me.TBCardName.Location = New System.Drawing.Point(205, 34)
        Me.TBCardName.Name = "TBCardName"
        Me.TBCardName.ReadOnly = True
        Me.TBCardName.Size = New System.Drawing.Size(195, 20)
        Me.TBCardName.TabIndex = 39
        '
        'TBCardCode
        '
        Me.TBCardCode.Location = New System.Drawing.Point(123, 34)
        Me.TBCardCode.Name = "TBCardCode"
        Me.TBCardCode.ReadOnly = True
        Me.TBCardCode.Size = New System.Drawing.Size(60, 20)
        Me.TBCardCode.TabIndex = 38
        '
        'TBFolio
        '
        Me.TBFolio.Location = New System.Drawing.Point(16, 34)
        Me.TBFolio.Name = "TBFolio"
        Me.TBFolio.Size = New System.Drawing.Size(55, 20)
        Me.TBFolio.TabIndex = 37
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(202, 18)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(44, 13)
        Me.Label23.TabIndex = 36
        Me.Label23.Text = "Nombre"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(120, 18)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(39, 13)
        Me.Label22.TabIndex = 35
        Me.Label22.Text = "Cliente"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(13, 65)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(44, 13)
        Me.Label21.TabIndex = 34
        Me.Label21.Text = "Artículo"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(120, 65)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(63, 13)
        Me.Label20.TabIndex = 33
        Me.Label20.Text = "Descripción"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(273, 65)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(49, 13)
        Me.Label19.TabIndex = 32
        Me.Label19.Text = "Cantidad"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Folio"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cbx4)
        Me.GroupBox2.Controls.Add(Me.cbx3)
        Me.GroupBox2.Controls.Add(Me.cbx2)
        Me.GroupBox2.Controls.Add(Me.cbx1)
        Me.GroupBox2.Enabled = False
        Me.GroupBox2.Location = New System.Drawing.Point(22, 319)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(687, 118)
        Me.GroupBox2.TabIndex = 348
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Condiciones con detalle"
        '
        'cbx4
        '
        Me.cbx4.AutoSize = True
        Me.cbx4.Location = New System.Drawing.Point(15, 88)
        Me.cbx4.Name = "cbx4"
        Me.cbx4.Size = New System.Drawing.Size(196, 17)
        Me.cbx4.TabIndex = 4
        Me.cbx4.Text = "Fecha de fabricación mayor a 1 año"
        Me.cbx4.UseVisualStyleBackColor = True
        '
        'cbx3
        '
        Me.cbx3.AutoSize = True
        Me.cbx3.Location = New System.Drawing.Point(15, 65)
        Me.cbx3.Name = "cbx3"
        Me.cbx3.Size = New System.Drawing.Size(107, 17)
        Me.cbx3.TabIndex = 3
        Me.cbx3.Text = "Ha sido instalada"
        Me.cbx3.UseVisualStyleBackColor = True
        '
        'cbx2
        '
        Me.cbx2.AutoSize = True
        Me.cbx2.Location = New System.Drawing.Point(15, 42)
        Me.cbx2.Name = "cbx2"
        Me.cbx2.Size = New System.Drawing.Size(121, 17)
        Me.cbx2.TabIndex = 2
        Me.cbx2.Text = "Incompleto/Faltante"
        Me.cbx2.UseVisualStyleBackColor = True
        '
        'cbx1
        '
        Me.cbx1.AutoSize = True
        Me.cbx1.Location = New System.Drawing.Point(15, 19)
        Me.cbx1.Name = "cbx1"
        Me.cbx1.Size = New System.Drawing.Size(140, 17)
        Me.cbx1.TabIndex = 1
        Me.cbx1.Text = "Empaque en mal estado"
        Me.cbx1.UseVisualStyleBackColor = True
        '
        'DetDevolucionSeg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 727)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.TBDiasTransTot)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "DetDevolucionSeg"
        Me.Text = "Seguimiento Devolucion"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents DTPFecDic As DateTimePicker
    Friend WithEvents BSave As Button
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents TBNumGuia As TextBox
    Friend WithEvents DTPEnvCli As DateTimePicker
    Friend WithEvents TBDiasTransGuia As TextBox
    Friend WithEvents Label44 As Label
    Friend WithEvents Label45 As Label
    Friend WithEvents Label47 As Label
    Friend WithEvents TBDiasTransTot As TextBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents TBDiasTransAlm2 As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents TBFecTrasp As TextBox
    Friend WithEvents TBNumTrasp As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents TBFecRespCli As DateTimePicker
    Friend WithEvents TBNumDoc As TextBox
    Friend WithEvents Label36 As Label
    Friend WithEvents Label38 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents DTPRecComp As DateTimePicker
    Friend WithEvents Label17 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label18 As Label
    Friend WithEvents TBCantidad As TextBox
    Friend WithEvents TBItemName As TextBox
    Friend WithEvents TBItemCode As TextBox
    Friend WithEvents TBCardName As TextBox
    Friend WithEvents TBCardCode As TextBox
    Friend WithEvents TBFolio As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpFechaEntregaCobranza As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents cbCausa As ComboBox
    Friend WithEvents cbCondiciones As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents DTPFecIniRev As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cbx4 As CheckBox
    Friend WithEvents cbx3 As CheckBox
    Friend WithEvents cbx2 As CheckBox
    Friend WithEvents cbx1 As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtObservaciones As TextBox
    Friend WithEvents rbDetalle As RadioButton
    Friend WithEvents rbOptimas As RadioButton
    Friend WithEvents CBDictamen As ComboBox
End Class
