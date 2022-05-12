<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LineasHalconIngresarObjetivos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LineasHalconIngresarObjetivos))
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CBMES = New System.Windows.Forms.ComboBox()
        Me.CBAño = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TBROCKWELLB = New System.Windows.Forms.TextBox()
        Me.TBRODWELLB = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.TBPOWERPRO = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.TBKING = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.TBDP = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.TBCARGO = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
        Me.DGObjetivos = New System.Windows.Forms.DataGridView()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.TbObjGen = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TBUJOINTK = New System.Windows.Forms.TextBox()
        Me.TBRODWELLC = New System.Windows.Forms.TextBox()
        Me.TBRODWELL = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.BAct = New System.Windows.Forms.Button()
        Me.BSave = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.DGObjetivos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Algerian", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(611, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(162, 21)
        Me.Label6.TabIndex = 318
        Me.Label6.Text = "LINEAS HALCON"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(53, 32)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(27, 13)
        Me.Label10.TabIndex = 311
        Me.Label10.Text = "Mes"
        '
        'CBMES
        '
        Me.CBMES.FormattingEnabled = True
        Me.CBMES.Location = New System.Drawing.Point(56, 52)
        Me.CBMES.Name = "CBMES"
        Me.CBMES.Size = New System.Drawing.Size(109, 21)
        Me.CBMES.TabIndex = 308
        '
        'CBAño
        '
        Me.CBAño.FormattingEnabled = True
        Me.CBAño.Items.AddRange(New Object() {"2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022"})
        Me.CBAño.Location = New System.Drawing.Point(226, 52)
        Me.CBAño.Name = "CBAño"
        Me.CBAño.Size = New System.Drawing.Size(100, 21)
        Me.CBAño.TabIndex = 358
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(223, 31)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(26, 13)
        Me.Label9.TabIndex = 357
        Me.Label9.Text = "Año"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(207, 257)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(13, 13)
        Me.Label1.TabIndex = 393
        Me.Label1.Text = "$"
        '
        'TBROCKWELLB
        '
        Me.TBROCKWELLB.Location = New System.Drawing.Point(226, 254)
        Me.TBROCKWELLB.Name = "TBROCKWELLB"
        Me.TBROCKWELLB.Size = New System.Drawing.Size(100, 20)
        Me.TBROCKWELLB.TabIndex = 392
        '
        'TBRODWELLB
        '
        Me.TBRODWELLB.Location = New System.Drawing.Point(226, 297)
        Me.TBRODWELLB.Name = "TBRODWELLB"
        Me.TBRODWELLB.Size = New System.Drawing.Size(100, 20)
        Me.TBRODWELLB.TabIndex = 391
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(207, 300)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(13, 13)
        Me.Label31.TabIndex = 390
        Me.Label31.Text = "$"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(207, 343)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(13, 13)
        Me.Label28.TabIndex = 388
        Me.Label28.Text = "$"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(207, 322)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(13, 13)
        Me.Label27.TabIndex = 386
        Me.Label27.Text = "$"
        '
        'TBPOWERPRO
        '
        Me.TBPOWERPRO.Location = New System.Drawing.Point(226, 231)
        Me.TBPOWERPRO.Name = "TBPOWERPRO"
        Me.TBPOWERPRO.Size = New System.Drawing.Size(100, 20)
        Me.TBPOWERPRO.TabIndex = 383
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(207, 234)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(13, 13)
        Me.Label24.TabIndex = 382
        Me.Label24.Text = "$"
        '
        'TBKING
        '
        Me.TBKING.Location = New System.Drawing.Point(226, 209)
        Me.TBKING.Name = "TBKING"
        Me.TBKING.Size = New System.Drawing.Size(100, 20)
        Me.TBKING.TabIndex = 381
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(207, 212)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(13, 13)
        Me.Label23.TabIndex = 380
        Me.Label23.Text = "$"
        '
        'TBDP
        '
        Me.TBDP.Location = New System.Drawing.Point(226, 188)
        Me.TBDP.Name = "TBDP"
        Me.TBDP.Size = New System.Drawing.Size(100, 20)
        Me.TBDP.TabIndex = 379
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(207, 191)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(13, 13)
        Me.Label22.TabIndex = 378
        Me.Label22.Text = "$"
        '
        'TBCARGO
        '
        Me.TBCARGO.Location = New System.Drawing.Point(226, 166)
        Me.TBCARGO.Name = "TBCARGO"
        Me.TBCARGO.Size = New System.Drawing.Size(100, 20)
        Me.TBCARGO.TabIndex = 377
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(207, 169)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(13, 13)
        Me.Label21.TabIndex = 376
        Me.Label21.Text = "$"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(53, 259)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(113, 13)
        Me.Label20.TabIndex = 375
        Me.Label20.Text = "ROCKWELL BRAKES"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(53, 300)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(109, 13)
        Me.Label19.TabIndex = 374
        Me.Label19.Text = "RODWELL BOMBAS"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(54, 343)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(78, 13)
        Me.Label17.TabIndex = 373
        Me.Label17.Text = "U-JOINT KING"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(53, 234)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 13)
        Me.Label14.TabIndex = 370
        Me.Label14.Text = "POWER PRO"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(53, 212)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(33, 13)
        Me.Label13.TabIndex = 369
        Me.Label13.Text = "KING"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(53, 191)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(22, 13)
        Me.Label12.TabIndex = 368
        Me.Label12.Text = "DP"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(53, 169)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 367
        Me.Label11.Text = "CARGO"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(100, 148)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 366
        Me.Label7.Text = "LINEAS"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(53, 86)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 13)
        Me.Label8.TabIndex = 365
        Me.Label8.Text = "Agente"
        '
        'CmbAgteVta
        '
        Me.CmbAgteVta.FormattingEnabled = True
        Me.CmbAgteVta.Location = New System.Drawing.Point(56, 106)
        Me.CmbAgteVta.Name = "CmbAgteVta"
        Me.CmbAgteVta.Size = New System.Drawing.Size(144, 21)
        Me.CmbAgteVta.TabIndex = 364
        '
        'DGObjetivos
        '
        Me.DGObjetivos.AllowUserToAddRows = False
        Me.DGObjetivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGObjetivos.Location = New System.Drawing.Point(355, 259)
        Me.DGObjetivos.Name = "DGObjetivos"
        Me.DGObjetivos.Size = New System.Drawing.Size(418, 108)
        Me.DGObjetivos.TabIndex = 395
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.AliceBlue
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button6.Location = New System.Drawing.Point(355, 96)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(86, 36)
        Me.Button6.TabIndex = 396
        Me.Button6.Text = "Consultar"
        Me.Button6.UseVisualStyleBackColor = False
        Me.Button6.Visible = False
        '
        'TbObjGen
        '
        Me.TbObjGen.Location = New System.Drawing.Point(226, 107)
        Me.TbObjGen.Name = "TbObjGen"
        Me.TbObjGen.Size = New System.Drawing.Size(100, 20)
        Me.TbObjGen.TabIndex = 401
        Me.TbObjGen.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(223, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 402
        Me.Label2.Text = "Objetivo General"
        Me.Label2.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(207, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(13, 13)
        Me.Label3.TabIndex = 403
        Me.Label3.Text = "$"
        Me.Label3.Visible = False
        '
        'TBUJOINTK
        '
        Me.TBUJOINTK.Location = New System.Drawing.Point(226, 340)
        Me.TBUJOINTK.Name = "TBUJOINTK"
        Me.TBUJOINTK.Size = New System.Drawing.Size(100, 20)
        Me.TBUJOINTK.TabIndex = 409
        '
        'TBRODWELLC
        '
        Me.TBRODWELLC.Location = New System.Drawing.Point(226, 319)
        Me.TBRODWELLC.Name = "TBRODWELLC"
        Me.TBRODWELLC.Size = New System.Drawing.Size(100, 20)
        Me.TBRODWELLC.TabIndex = 408
        '
        'TBRODWELL
        '
        Me.TBRODWELL.Location = New System.Drawing.Point(226, 275)
        Me.TBRODWELL.Name = "TBRODWELL"
        Me.TBRODWELL.Size = New System.Drawing.Size(100, 20)
        Me.TBRODWELL.TabIndex = 407
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(207, 278)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(13, 13)
        Me.Label25.TabIndex = 406
        Me.Label25.Text = "$"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(54, 322)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(147, 13)
        Me.Label16.TabIndex = 405
        Me.Label16.Text = "RODWELL COMPRESORES"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(54, 278)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(61, 13)
        Me.Label15.TabIndex = 404
        Me.Label15.Text = "RODWELL"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(355, 166)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(250, 69)
        Me.DataGridView1.TabIndex = 411
        '
        'BAct
        '
        Me.BAct.Image = Global.TPDiamante.My.Resources.Resources.Refresh_B
        Me.BAct.Location = New System.Drawing.Point(286, 373)
        Me.BAct.Name = "BAct"
        Me.BAct.Size = New System.Drawing.Size(40, 36)
        Me.BAct.TabIndex = 410
        Me.BAct.UseVisualStyleBackColor = True
        '
        'BSave
        '
        Me.BSave.Image = CType(resources.GetObject("BSave.Image"), System.Drawing.Image)
        Me.BSave.Location = New System.Drawing.Point(355, 98)
        Me.BSave.Name = "BSave"
        Me.BSave.Size = New System.Drawing.Size(38, 33)
        Me.BSave.TabIndex = 400
        Me.BSave.UseVisualStyleBackColor = True
        Me.BSave.Visible = False
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(716, 373)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(57, 52)
        Me.Button1.TabIndex = 399
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(716, 83)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(57, 44)
        Me.Button3.TabIndex = 398
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.SystemColors.Control
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.Location = New System.Drawing.Point(653, 83)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(57, 44)
        Me.Button5.TabIndex = 397
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(354, 241)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 13)
        Me.Label4.TabIndex = 412
        Me.Label4.Text = "Objetivos por agente"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(354, 147)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 13)
        Me.Label5.TabIndex = 413
        Me.Label5.Text = "Objetivos Generales"
        '
        'LineasHalconIngresarObjetivos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(793, 428)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.BAct)
        Me.Controls.Add(Me.TBUJOINTK)
        Me.Controls.Add(Me.TBRODWELLC)
        Me.Controls.Add(Me.TBRODWELL)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TbObjGen)
        Me.Controls.Add(Me.BSave)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.DGObjetivos)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TBROCKWELLB)
        Me.Controls.Add(Me.TBRODWELLB)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.TBPOWERPRO)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.TBKING)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.TBDP)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.TBCARGO)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.CmbAgteVta)
        Me.Controls.Add(Me.CBAño)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.CBMES)
        Me.Name = "LineasHalconIngresarObjetivos"
        Me.Text = "Lineas Halcon Ingresar Objetivos"
        CType(Me.DGObjetivos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CBMES As System.Windows.Forms.ComboBox
    Friend WithEvents CBAño As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TBROCKWELLB As System.Windows.Forms.TextBox
    Friend WithEvents TBRODWELLB As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents TBPOWERPRO As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents TBKING As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TBDP As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TBCARGO As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
    Friend WithEvents DGObjetivos As System.Windows.Forms.DataGridView
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents BSave As System.Windows.Forms.Button
    Friend WithEvents TbObjGen As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TBUJOINTK As System.Windows.Forms.TextBox
    Friend WithEvents TBRODWELLC As System.Windows.Forms.TextBox
    Friend WithEvents TBRODWELL As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents BAct As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
