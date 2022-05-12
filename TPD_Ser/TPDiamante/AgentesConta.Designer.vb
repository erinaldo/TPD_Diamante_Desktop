<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AgentesConta
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
        Me.CkClientes = New System.Windows.Forms.CheckBox()
        Me.GrdConProd = New System.Windows.Forms.DataGridView()
        Me.BtnConsultar = New System.Windows.Forms.Button()
        Me.CmbLineaTer = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CmbLineaIni = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CmbAreaTer = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CmbAreaIni = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CmbNParteTer = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CmbNParteIni = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.CmbTurnoTer = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbTurnoIni = New System.Windows.Forms.ComboBox()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.BtnImprimir = New System.Windows.Forms.Button()
        CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CkClientes
        '
        Me.CkClientes.AutoSize = True
        Me.CkClientes.Checked = True
        Me.CkClientes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CkClientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CkClientes.Location = New System.Drawing.Point(166, 9)
        Me.CkClientes.Name = "CkClientes"
        Me.CkClientes.Size = New System.Drawing.Size(114, 19)
        Me.CkClientes.TabIndex = 87
        Me.CkClientes.Text = "Clientes propios"
        Me.CkClientes.UseVisualStyleBackColor = True
        '
        'GrdConProd
        '
        Me.GrdConProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdConProd.Location = New System.Drawing.Point(12, 104)
        Me.GrdConProd.Name = "GrdConProd"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdConProd.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GrdConProd.Size = New System.Drawing.Size(1094, 645)
        Me.GrdConProd.TabIndex = 84
        '
        'BtnConsultar
        '
        Me.BtnConsultar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnConsultar.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnConsultar.Location = New System.Drawing.Point(166, 62)
        Me.BtnConsultar.Name = "BtnConsultar"
        Me.BtnConsultar.Size = New System.Drawing.Size(75, 31)
        Me.BtnConsultar.TabIndex = 74
        Me.BtnConsultar.Text = "Consultar"
        Me.BtnConsultar.UseVisualStyleBackColor = True
        '
        'CmbLineaTer
        '
        Me.CmbLineaTer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CmbLineaTer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbLineaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbLineaTer.FormattingEnabled = True
        Me.CmbLineaTer.Location = New System.Drawing.Point(603, 37)
        Me.CmbLineaTer.Name = "CmbLineaTer"
        Me.CmbLineaTer.Size = New System.Drawing.Size(310, 24)
        Me.CmbLineaTer.TabIndex = 71
        Me.CmbLineaTer.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(493, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(107, 17)
        Me.Label9.TabIndex = 82
        Me.Label9.Text = "Línea  Término:"
        Me.Label9.Visible = False
        '
        'CmbLineaIni
        '
        Me.CmbLineaIni.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CmbLineaIni.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbLineaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbLineaIni.FormattingEnabled = True
        Me.CmbLineaIni.Location = New System.Drawing.Point(136, 40)
        Me.CmbLineaIni.Name = "CmbLineaIni"
        Me.CmbLineaIni.Size = New System.Drawing.Size(310, 24)
        Me.CmbLineaIni.TabIndex = 70
        Me.CmbLineaIni.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(53, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 17)
        Me.Label10.TabIndex = 77
        Me.Label10.Text = "Línea Inicio:"
        Me.Label10.Visible = False
        '
        'CmbAreaTer
        '
        Me.CmbAreaTer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CmbAreaTer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbAreaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbAreaTer.FormattingEnabled = True
        Me.CmbAreaTer.Location = New System.Drawing.Point(603, 33)
        Me.CmbAreaTer.Name = "CmbAreaTer"
        Me.CmbAreaTer.Size = New System.Drawing.Size(310, 24)
        Me.CmbAreaTer.TabIndex = 69
        Me.CmbAreaTer.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(502, 37)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 17)
        Me.Label8.TabIndex = 81
        Me.Label8.Text = "Área Término:"
        Me.Label8.Visible = False
        '
        'CmbAreaIni
        '
        Me.CmbAreaIni.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CmbAreaIni.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbAreaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbAreaIni.FormattingEnabled = True
        Me.CmbAreaIni.Location = New System.Drawing.Point(136, 34)
        Me.CmbAreaIni.Name = "CmbAreaIni"
        Me.CmbAreaIni.Size = New System.Drawing.Size(310, 24)
        Me.CmbAreaIni.TabIndex = 68
        Me.CmbAreaIni.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(58, 34)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 17)
        Me.Label7.TabIndex = 76
        Me.Label7.Text = "Área Inicio:"
        Me.Label7.Visible = False
        '
        'CmbNParteTer
        '
        Me.CmbNParteTer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CmbNParteTer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbNParteTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbNParteTer.FormattingEnabled = True
        Me.CmbNParteTer.Location = New System.Drawing.Point(604, 44)
        Me.CmbNParteTer.Name = "CmbNParteTer"
        Me.CmbNParteTer.Size = New System.Drawing.Size(310, 24)
        Me.CmbNParteTer.TabIndex = 73
        Me.CmbNParteTer.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(458, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(148, 17)
        Me.Label4.TabIndex = 83
        Me.Label4.Text = "No. de Parte Término:"
        Me.Label4.Visible = False
        '
        'CmbNParteIni
        '
        Me.CmbNParteIni.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CmbNParteIni.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbNParteIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbNParteIni.FormattingEnabled = True
        Me.CmbNParteIni.Location = New System.Drawing.Point(137, 46)
        Me.CmbNParteIni.Name = "CmbNParteIni"
        Me.CmbNParteIni.Size = New System.Drawing.Size(310, 24)
        Me.CmbNParteIni.TabIndex = 72
        Me.CmbNParteIni.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(19, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(128, 17)
        Me.Label6.TabIndex = 78
        Me.Label6.Text = "No. de Parte Inicio:"
        Me.Label6.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(536, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 17)
        Me.Label3.TabIndex = 80
        Me.Label3.Text = "Fecha Término:"
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaTer.Location = New System.Drawing.Point(644, 36)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(265, 23)
        Me.DtpFechaTer.TabIndex = 67
        Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(74, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 17)
        Me.Label5.TabIndex = 75
        Me.Label5.Text = "Fecha Inicio:"
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaIni.Location = New System.Drawing.Point(164, 36)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(265, 23)
        Me.DtpFechaIni.TabIndex = 66
        Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'CmbTurnoTer
        '
        Me.CmbTurnoTer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CmbTurnoTer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbTurnoTer.DropDownWidth = 61
        Me.CmbTurnoTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbTurnoTer.FormattingEnabled = True
        Me.CmbTurnoTer.Location = New System.Drawing.Point(513, 42)
        Me.CmbTurnoTer.Name = "CmbTurnoTer"
        Me.CmbTurnoTer.Size = New System.Drawing.Size(70, 24)
        Me.CmbTurnoTer.TabIndex = 65
        Me.CmbTurnoTer.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(405, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 17)
        Me.Label1.TabIndex = 79
        Me.Label1.Text = "Turno Término:"
        Me.Label1.Visible = False
        '
        'CmbTurnoIni
        '
        Me.CmbTurnoIni.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CmbTurnoIni.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbTurnoIni.DropDownWidth = 61
        Me.CmbTurnoIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbTurnoIni.FormattingEnabled = True
        Me.CmbTurnoIni.Location = New System.Drawing.Point(324, 58)
        Me.CmbTurnoIni.Name = "CmbTurnoIni"
        Me.CmbTurnoIni.Size = New System.Drawing.Size(70, 24)
        Me.CmbTurnoIni.TabIndex = 64
        Me.CmbTurnoIni.Visible = False
        '
        'BtnExcel
        '
        Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnExcel.Location = New System.Drawing.Point(868, 63)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(43, 39)
        Me.BtnExcel.TabIndex = 86
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'BtnImprimir
        '
        Me.BtnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprimir.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnImprimir.Image = Global.TPDiamante.My.Resources.Resources.printer
        Me.BtnImprimir.Location = New System.Drawing.Point(795, 64)
        Me.BtnImprimir.Name = "BtnImprimir"
        Me.BtnImprimir.Size = New System.Drawing.Size(43, 39)
        Me.BtnImprimir.TabIndex = 85
        Me.BtnImprimir.UseVisualStyleBackColor = True
        Me.BtnImprimir.Visible = False
        '
        'AgentesConta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1118, 759)
        Me.Controls.Add(Me.CkClientes)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.GrdConProd)
        Me.Controls.Add(Me.BtnImprimir)
        Me.Controls.Add(Me.BtnConsultar)
        Me.Controls.Add(Me.CmbLineaTer)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.CmbLineaIni)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.CmbAreaTer)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.CmbAreaIni)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.CmbNParteTer)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CmbNParteIni)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DtpFechaTer)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DtpFechaIni)
        Me.Controls.Add(Me.CmbTurnoTer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CmbTurnoIni)
        Me.Name = "AgentesConta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AgentesConta"
        CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CkClientes As System.Windows.Forms.CheckBox
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents GrdConProd As System.Windows.Forms.DataGridView
    Friend WithEvents BtnImprimir As System.Windows.Forms.Button
    Friend WithEvents BtnConsultar As System.Windows.Forms.Button
    Friend WithEvents CmbLineaTer As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CmbLineaIni As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CmbAreaTer As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CmbAreaIni As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CmbNParteTer As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CmbNParteIni As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents CmbTurnoTer As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbTurnoIni As System.Windows.Forms.ComboBox
End Class
