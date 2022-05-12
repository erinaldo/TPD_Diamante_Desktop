<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BORec
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.CmbArticulo = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CmbGrupoArticulo = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CmbCliente = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DtpAnioTer = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpAnioIni = New System.Windows.Forms.DateTimePicker()
        Me.GrdBoRec = New System.Windows.Forms.DataGridView()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DtpMesTer = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DtpMesIni = New System.Windows.Forms.DateTimePicker()
        Me.lAlm = New System.Windows.Forms.Label()
        Me.cmbAlmacen = New System.Windows.Forms.ComboBox()
        Me.lBo = New System.Windows.Forms.Label()
        Me.cmbAgteBo = New System.Windows.Forms.ComboBox()
        CType(Me.GrdBoRec, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmbArticulo
        '
        Me.CmbArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbArticulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbArticulo.FormattingEnabled = True
        Me.CmbArticulo.Location = New System.Drawing.Point(516, 89)
        Me.CmbArticulo.Name = "CmbArticulo"
        Me.CmbArticulo.Size = New System.Drawing.Size(310, 21)
        Me.CmbArticulo.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(455, 90)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 17)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Artículo"
        '
        'CmbGrupoArticulo
        '
        Me.CmbGrupoArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbGrupoArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbGrupoArticulo.FormattingEnabled = True
        Me.CmbGrupoArticulo.Location = New System.Drawing.Point(127, 89)
        Me.CmbGrupoArticulo.Name = "CmbGrupoArticulo"
        Me.CmbGrupoArticulo.Size = New System.Drawing.Size(264, 21)
        Me.CmbGrupoArticulo.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(77, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 17)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Línea"
        '
        'CmbCliente
        '
        Me.CmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbCliente.FormattingEnabled = True
        Me.CmbCliente.Location = New System.Drawing.Point(516, 64)
        Me.CmbCliente.Name = "CmbCliente"
        Me.CmbCliente.Size = New System.Drawing.Size(310, 21)
        Me.CmbCliente.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(459, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 17)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Cliente"
        '
        'CmbAgteVta
        '
        Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbAgteVta.FormattingEnabled = True
        Me.CmbAgteVta.Location = New System.Drawing.Point(127, 64)
        Me.CmbAgteVta.Name = "CmbAgteVta"
        Me.CmbAgteVta.Size = New System.Drawing.Size(265, 21)
        Me.CmbAgteVta.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 17)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Agente de ventas"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.AliceBlue
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button1.Location = New System.Drawing.Point(905, 9)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 44)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "Consultar"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(229, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 17)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Año Término"
        '
        'DtpAnioTer
        '
        Me.DtpAnioTer.CustomFormat = "yyyy"
        Me.DtpAnioTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpAnioTer.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpAnioTer.Location = New System.Drawing.Point(325, 11)
        Me.DtpAnioTer.Name = "DtpAnioTer"
        Me.DtpAnioTer.ShowUpDown = True
        Me.DtpAnioTer.Size = New System.Drawing.Size(66, 23)
        Me.DtpAnioTer.TabIndex = 16
        Me.DtpAnioTer.Value = New Date(2012, 11, 15, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(51, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 17)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Año Inicio"
        '
        'DtpAnioIni
        '
        Me.DtpAnioIni.CustomFormat = "yyyy"
        Me.DtpAnioIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpAnioIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpAnioIni.Location = New System.Drawing.Point(127, 11)
        Me.DtpAnioIni.MinDate = New Date(2012, 1, 1, 0, 0, 0, 0)
        Me.DtpAnioIni.Name = "DtpAnioIni"
        Me.DtpAnioIni.ShowUpDown = True
        Me.DtpAnioIni.Size = New System.Drawing.Size(69, 23)
        Me.DtpAnioIni.TabIndex = 15
        Me.DtpAnioIni.Value = New Date(2012, 1, 1, 0, 0, 0, 0)
        '
        'GrdBoRec
        '
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdBoRec.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GrdBoRec.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrdBoRec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdBoRec.Location = New System.Drawing.Point(2, 129)
        Me.GrdBoRec.Name = "GrdBoRec"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdBoRec.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.GrdBoRec.Size = New System.Drawing.Size(1244, 534)
        Me.GrdBoRec.TabIndex = 22
        '
        'BtnExcel
        '
        Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnExcel.Location = New System.Drawing.Point(921, 77)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(43, 39)
        Me.BtnExcel.TabIndex = 29
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(230, 38)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 17)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Mes Término"
        '
        'DtpMesTer
        '
        Me.DtpMesTer.CustomFormat = "MM"
        Me.DtpMesTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpMesTer.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpMesTer.Location = New System.Drawing.Point(326, 38)
        Me.DtpMesTer.Name = "DtpMesTer"
        Me.DtpMesTer.ShowUpDown = True
        Me.DtpMesTer.Size = New System.Drawing.Size(66, 23)
        Me.DtpMesTer.TabIndex = 31
        Me.DtpMesTer.Value = New Date(2012, 11, 15, 0, 0, 0, 0)
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(52, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 17)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "Mes Inicio"
        '
        'DtpMesIni
        '
        Me.DtpMesIni.CustomFormat = "MM"
        Me.DtpMesIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpMesIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpMesIni.Location = New System.Drawing.Point(128, 38)
        Me.DtpMesIni.MinDate = New Date(2012, 1, 1, 0, 0, 0, 0)
        Me.DtpMesIni.Name = "DtpMesIni"
        Me.DtpMesIni.ShowUpDown = True
        Me.DtpMesIni.Size = New System.Drawing.Size(69, 23)
        Me.DtpMesIni.TabIndex = 30
        Me.DtpMesIni.Value = New Date(2012, 1, 1, 0, 0, 0, 0)
        '
        'lAlm
        '
        Me.lAlm.AutoSize = True
        Me.lAlm.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lAlm.Location = New System.Drawing.Point(448, 13)
        Me.lAlm.Name = "lAlm"
        Me.lAlm.Size = New System.Drawing.Size(62, 17)
        Me.lAlm.TabIndex = 27
        Me.lAlm.Text = "Almacen"
        '
        'cmbAlmacen
        '
        Me.cmbAlmacen.FormattingEnabled = True
        Me.cmbAlmacen.Location = New System.Drawing.Point(516, 11)
        Me.cmbAlmacen.Name = "cmbAlmacen"
        Me.cmbAlmacen.Size = New System.Drawing.Size(310, 21)
        Me.cmbAlmacen.TabIndex = 34
        '
        'lBo
        '
        Me.lBo.AutoSize = True
        Me.lBo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lBo.Location = New System.Drawing.Point(433, 41)
        Me.lBo.Name = "lBo"
        Me.lBo.Size = New System.Drawing.Size(77, 17)
        Me.lBo.TabIndex = 27
        Me.lBo.Text = "Agente BO"
        '
        'cmbAgteBo
        '
        Me.cmbAgteBo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAgteBo.FormattingEnabled = True
        Me.cmbAgteBo.Location = New System.Drawing.Point(516, 38)
        Me.cmbAgteBo.Name = "cmbAgteBo"
        Me.cmbAgteBo.Size = New System.Drawing.Size(310, 21)
        Me.cmbAgteBo.TabIndex = 35
        '
        'BORec
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1246, 675)
        Me.Controls.Add(Me.cmbAgteBo)
        Me.Controls.Add(Me.cmbAlmacen)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.DtpMesTer)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.DtpMesIni)
        Me.Controls.Add(Me.CmbArticulo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CmbGrupoArticulo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CmbCliente)
        Me.Controls.Add(Me.lBo)
        Me.Controls.Add(Me.lAlm)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CmbAgteVta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DtpAnioTer)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DtpAnioIni)
        Me.Controls.Add(Me.GrdBoRec)
        Me.Name = "BORec"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reporte de Back Order Recuperado"
        CType(Me.GrdBoRec, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CmbArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CmbGrupoArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DtpAnioTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpAnioIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents GrdBoRec As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DtpMesTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DtpMesIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents lAlm As System.Windows.Forms.Label
    Friend WithEvents cmbAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents lBo As System.Windows.Forms.Label
    Friend WithEvents cmbAgteBo As System.Windows.Forms.ComboBox
End Class
