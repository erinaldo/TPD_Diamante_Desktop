<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ScoreCardTPD
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
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.txtAvanceOptimo = New System.Windows.Forms.TextBox()
    Me.label9 = New System.Windows.Forms.Label()
    Me.txtDiasRestantes = New System.Windows.Forms.TextBox()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.txtDiasTranscurridos = New System.Windows.Forms.TextBox()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.txtDiasMes = New System.Windows.Forms.TextBox()
    Me.Button2 = New System.Windows.Forms.Button()
    Me.CmbSucursal = New System.Windows.Forms.ComboBox()
    Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
    Me.DgTotales = New System.Windows.Forms.DataGridView()
    Me.DgVtaAgte = New System.Windows.Forms.DataGridView()
    Me.DgClientes = New System.Windows.Forms.DataGridView()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Button3 = New System.Windows.Forms.Button()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.BtnClientes = New System.Windows.Forms.Button()
    Me.BtnAgentes = New System.Windows.Forms.Button()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.GroupBox6 = New System.Windows.Forms.GroupBox()
    Me.GroupBox5 = New System.Windows.Forms.GroupBox()
    Me.GroupBox4 = New System.Windows.Forms.GroupBox()
    CType(Me.DgTotales, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DgVtaAgte, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DgClientes, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.GroupBox6.SuspendLayout()
    Me.GroupBox5.SuspendLayout()
    Me.GroupBox4.SuspendLayout()
    Me.SuspendLayout()
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label3.Location = New System.Drawing.Point(14, 162)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(56, 13)
    Me.Label3.TabIndex = 132
    Me.Label3.Text = "Sucursal"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(14, 208)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(97, 13)
    Me.Label2.TabIndex = 133
    Me.Label2.Text = "Agente de vtas."
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label4.Location = New System.Drawing.Point(13, 79)
    Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(93, 13)
    Me.Label4.TabIndex = 131
    Me.Label4.Text = "Avance Optimo"
    '
    'txtAvanceOptimo
    '
    Me.txtAvanceOptimo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtAvanceOptimo.Location = New System.Drawing.Point(136, 76)
    Me.txtAvanceOptimo.Margin = New System.Windows.Forms.Padding(4)
    Me.txtAvanceOptimo.Name = "txtAvanceOptimo"
    Me.txtAvanceOptimo.ReadOnly = True
    Me.txtAvanceOptimo.Size = New System.Drawing.Size(65, 20)
    Me.txtAvanceOptimo.TabIndex = 130
    Me.txtAvanceOptimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'label9
    '
    Me.label9.AutoSize = True
    Me.label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.label9.Location = New System.Drawing.Point(14, 58)
    Me.label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.label9.Name = "label9"
    Me.label9.Size = New System.Drawing.Size(95, 13)
    Me.label9.TabIndex = 129
    Me.label9.Text = "Días Restantes"
    '
    'txtDiasRestantes
    '
    Me.txtDiasRestantes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtDiasRestantes.Location = New System.Drawing.Point(136, 55)
    Me.txtDiasRestantes.Margin = New System.Windows.Forms.Padding(4)
    Me.txtDiasRestantes.Name = "txtDiasRestantes"
    Me.txtDiasRestantes.ReadOnly = True
    Me.txtDiasRestantes.Size = New System.Drawing.Size(65, 20)
    Me.txtDiasRestantes.TabIndex = 128
    Me.txtDiasRestantes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label6.Location = New System.Drawing.Point(13, 37)
    Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(115, 13)
    Me.Label6.TabIndex = 127
    Me.Label6.Text = "Días Transcurridos"
    '
    'txtDiasTranscurridos
    '
    Me.txtDiasTranscurridos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtDiasTranscurridos.Location = New System.Drawing.Point(136, 34)
    Me.txtDiasTranscurridos.Margin = New System.Windows.Forms.Padding(4)
    Me.txtDiasTranscurridos.Name = "txtDiasTranscurridos"
    Me.txtDiasTranscurridos.ReadOnly = True
    Me.txtDiasTranscurridos.Size = New System.Drawing.Size(65, 20)
    Me.txtDiasTranscurridos.TabIndex = 125
    Me.txtDiasTranscurridos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label7.Location = New System.Drawing.Point(14, 16)
    Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(61, 13)
    Me.Label7.TabIndex = 126
    Me.Label7.Text = "Días Mes"
    '
    'txtDiasMes
    '
    Me.txtDiasMes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtDiasMes.Location = New System.Drawing.Point(136, 13)
    Me.txtDiasMes.Margin = New System.Windows.Forms.Padding(4)
    Me.txtDiasMes.Name = "txtDiasMes"
    Me.txtDiasMes.ReadOnly = True
    Me.txtDiasMes.Size = New System.Drawing.Size(65, 20)
    Me.txtDiasMes.TabIndex = 124
    Me.txtDiasMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'Button2
    '
    Me.Button2.BackColor = System.Drawing.Color.AliceBlue
    Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
    Me.Button2.Location = New System.Drawing.Point(63, 251)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(86, 36)
    Me.Button2.TabIndex = 123
    Me.Button2.Text = "Consultar"
    Me.Button2.UseVisualStyleBackColor = False
    '
    'CmbSucursal
    '
    Me.CmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbSucursal.FormattingEnabled = True
    Me.CmbSucursal.Location = New System.Drawing.Point(16, 178)
    Me.CmbSucursal.Name = "CmbSucursal"
    Me.CmbSucursal.Size = New System.Drawing.Size(185, 21)
    Me.CmbSucursal.TabIndex = 120
    '
    'CmbAgteVta
    '
    Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbAgteVta.FormattingEnabled = True
    Me.CmbAgteVta.Location = New System.Drawing.Point(16, 224)
    Me.CmbAgteVta.Name = "CmbAgteVta"
    Me.CmbAgteVta.Size = New System.Drawing.Size(185, 21)
    Me.CmbAgteVta.TabIndex = 121
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.Location = New System.Drawing.Point(14, 114)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(73, 13)
    Me.Label5.TabIndex = 122
    Me.Label5.Text = "Fecha Final"
    '
    'DtpFechaIni
    '
    Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.DtpFechaIni.Location = New System.Drawing.Point(16, 130)
    Me.DtpFechaIni.Name = "DtpFechaIni"
    Me.DtpFechaIni.Size = New System.Drawing.Size(185, 23)
    Me.DtpFechaIni.TabIndex = 119
    Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
    '
    'DgTotales
    '
    Me.DgTotales.AllowUserToAddRows = False
    Me.DgTotales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgTotales.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DgTotales.Location = New System.Drawing.Point(3, 16)
    Me.DgTotales.Name = "DgTotales"
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgTotales.RowsDefaultCellStyle = DataGridViewCellStyle1
    Me.DgTotales.Size = New System.Drawing.Size(1067, 87)
    Me.DgTotales.TabIndex = 5
    '
    'DgVtaAgte
    '
    Me.DgVtaAgte.AllowUserToAddRows = False
    Me.DgVtaAgte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgVtaAgte.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DgVtaAgte.Location = New System.Drawing.Point(3, 16)
    Me.DgVtaAgte.Name = "DgVtaAgte"
    DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgVtaAgte.RowsDefaultCellStyle = DataGridViewCellStyle2
    Me.DgVtaAgte.Size = New System.Drawing.Size(1067, 334)
    Me.DgVtaAgte.TabIndex = 0
    '
    'DgClientes
    '
    Me.DgClientes.AllowUserToAddRows = False
    Me.DgClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgClientes.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DgClientes.Location = New System.Drawing.Point(3, 16)
    Me.DgClientes.Name = "DgClientes"
    DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgClientes.RowsDefaultCellStyle = DataGridViewCellStyle3
    Me.DgClientes.Size = New System.Drawing.Size(1067, 248)
    Me.DgClientes.TabIndex = 1
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label11.Location = New System.Drawing.Point(57, 401)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(103, 13)
    Me.Label11.TabIndex = 176
    Me.Label11.Text = "Clientes (Todos):"
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label10.Location = New System.Drawing.Point(63, 348)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(90, 13)
    Me.Label10.TabIndex = 175
    Me.Label10.Text = "Vendedor (es):"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label8.Location = New System.Drawing.Point(78, 295)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(53, 13)
    Me.Label8.TabIndex = 174
    Me.Label8.Text = "Totales:"
    Me.Label8.Visible = False
    '
    'Button3
    '
    Me.Button3.BackColor = System.Drawing.Color.AliceBlue
    Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button3.ForeColor = System.Drawing.Color.MediumBlue
    Me.Button3.Location = New System.Drawing.Point(38, 457)
    Me.Button3.Name = "Button3"
    Me.Button3.Size = New System.Drawing.Size(139, 36)
    Me.Button3.TabIndex = 180
    Me.Button3.Text = "Ingresar Objetivos"
    Me.Button3.UseVisualStyleBackColor = False
    '
    'Button1
    '
    Me.Button1.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
    Me.Button1.Location = New System.Drawing.Point(87, 311)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(36, 34)
    Me.Button1.TabIndex = 179
    Me.Button1.UseVisualStyleBackColor = True
    Me.Button1.Visible = False
    '
    'BtnClientes
    '
    Me.BtnClientes.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
    Me.BtnClientes.Location = New System.Drawing.Point(87, 417)
    Me.BtnClientes.Name = "BtnClientes"
    Me.BtnClientes.Size = New System.Drawing.Size(36, 34)
    Me.BtnClientes.TabIndex = 178
    Me.BtnClientes.UseVisualStyleBackColor = True
    '
    'BtnAgentes
    '
    Me.BtnAgentes.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
    Me.BtnAgentes.Location = New System.Drawing.Point(87, 364)
    Me.BtnAgentes.Name = "BtnAgentes"
    Me.BtnAgentes.Size = New System.Drawing.Size(36, 34)
    Me.BtnAgentes.TabIndex = 177
    Me.BtnAgentes.UseVisualStyleBackColor = True
    '
    'Panel1
    '
    Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel1.Controls.Add(Me.Label7)
    Me.Panel1.Controls.Add(Me.Button3)
    Me.Panel1.Controls.Add(Me.DtpFechaIni)
    Me.Panel1.Controls.Add(Me.Button1)
    Me.Panel1.Controls.Add(Me.Label5)
    Me.Panel1.Controls.Add(Me.BtnClientes)
    Me.Panel1.Controls.Add(Me.CmbAgteVta)
    Me.Panel1.Controls.Add(Me.BtnAgentes)
    Me.Panel1.Controls.Add(Me.CmbSucursal)
    Me.Panel1.Controls.Add(Me.Label11)
    Me.Panel1.Controls.Add(Me.Button2)
    Me.Panel1.Controls.Add(Me.Label10)
    Me.Panel1.Controls.Add(Me.txtDiasMes)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.txtDiasTranscurridos)
    Me.Panel1.Controls.Add(Me.Label6)
    Me.Panel1.Controls.Add(Me.txtDiasRestantes)
    Me.Panel1.Controls.Add(Me.label9)
    Me.Panel1.Controls.Add(Me.Label3)
    Me.Panel1.Controls.Add(Me.txtAvanceOptimo)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Controls.Add(Me.Label4)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(214, 730)
    Me.Panel1.TabIndex = 181
    '
    'Panel2
    '
    Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel2.Controls.Add(Me.GroupBox6)
    Me.Panel2.Controls.Add(Me.GroupBox5)
    Me.Panel2.Controls.Add(Me.GroupBox4)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel2.Location = New System.Drawing.Point(214, 0)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(1077, 730)
    Me.Panel2.TabIndex = 182
    '
    'GroupBox6
    '
    Me.GroupBox6.Controls.Add(Me.DgClientes)
    Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.GroupBox6.Location = New System.Drawing.Point(0, 459)
    Me.GroupBox6.Name = "GroupBox6"
    Me.GroupBox6.Size = New System.Drawing.Size(1073, 267)
    Me.GroupBox6.TabIndex = 139
    Me.GroupBox6.TabStop = False
    Me.GroupBox6.Text = "Clientes"
    '
    'GroupBox5
    '
    Me.GroupBox5.Controls.Add(Me.DgVtaAgte)
    Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox5.Location = New System.Drawing.Point(0, 106)
    Me.GroupBox5.Name = "GroupBox5"
    Me.GroupBox5.Size = New System.Drawing.Size(1073, 353)
    Me.GroupBox5.TabIndex = 138
    Me.GroupBox5.TabStop = False
    Me.GroupBox5.Text = "Vendedores"
    '
    'GroupBox4
    '
    Me.GroupBox4.Controls.Add(Me.DgTotales)
    Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
    Me.GroupBox4.Name = "GroupBox4"
    Me.GroupBox4.Size = New System.Drawing.Size(1073, 106)
    Me.GroupBox4.TabIndex = 137
    Me.GroupBox4.TabStop = False
    Me.GroupBox4.Text = "Totales"
    '
    'ScoreCardTPD
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1291, 730)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Name = "ScoreCardTPD"
    Me.Text = "ScoreCardTPD"
    CType(Me.DgTotales, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DgVtaAgte, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DgClientes, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.GroupBox6.ResumeLayout(False)
    Me.GroupBox5.ResumeLayout(False)
    Me.GroupBox4.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Private WithEvents Label4 As System.Windows.Forms.Label
  Private WithEvents txtAvanceOptimo As System.Windows.Forms.TextBox
  Private WithEvents label9 As System.Windows.Forms.Label
  Private WithEvents txtDiasRestantes As System.Windows.Forms.TextBox
  Private WithEvents Label6 As System.Windows.Forms.Label
  Private WithEvents txtDiasTranscurridos As System.Windows.Forms.TextBox
  Private WithEvents Label7 As System.Windows.Forms.Label
  Private WithEvents txtDiasMes As System.Windows.Forms.TextBox
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents CmbSucursal As System.Windows.Forms.ComboBox
  Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
  Friend WithEvents DgTotales As System.Windows.Forms.DataGridView
  Friend WithEvents DgVtaAgte As System.Windows.Forms.DataGridView
  Friend WithEvents DgClientes As System.Windows.Forms.DataGridView
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents BtnClientes As System.Windows.Forms.Button
  Friend WithEvents BtnAgentes As System.Windows.Forms.Button
  Friend WithEvents Label11 As System.Windows.Forms.Label
  Friend WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents Button3 As System.Windows.Forms.Button
  Friend WithEvents Panel1 As Panel
  Friend WithEvents Panel2 As Panel
  Friend WithEvents GroupBox6 As GroupBox
  Friend WithEvents GroupBox5 As GroupBox
  Friend WithEvents GroupBox4 As GroupBox
End Class
