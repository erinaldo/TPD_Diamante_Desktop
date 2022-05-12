<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AgenteListaPrecio
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
    Me.components = New System.ComponentModel.Container()
    Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.panelEspere = New System.Windows.Forms.Panel()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.DtpFechaInicial = New System.Windows.Forms.DateTimePicker()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.DtpFechaFinal = New System.Windows.Forms.DateTimePicker()
    Me.btnExportaAgentes = New System.Windows.Forms.Button()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
    Me.BtnDetalles = New System.Windows.Forms.Button()
    Me.CmbSucursal = New System.Windows.Forms.ComboBox()
    Me.Button2 = New System.Windows.Forms.Button()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.DgDetalles = New System.Windows.Forms.DataGridView()
    Me.GroupBox5 = New System.Windows.Forms.GroupBox()
    Me.DgAgentes = New System.Windows.Forms.DataGridView()
    Me.GroupBox4 = New System.Windows.Forms.GroupBox()
    Me.DgResumen = New System.Windows.Forms.DataGridView()
    Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
    Me.Panel1.SuspendLayout()
    Me.panelEspere.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    CType(Me.DgDetalles, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox5.SuspendLayout()
    CType(Me.DgAgentes, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox4.SuspendLayout()
    CType(Me.DgResumen, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Panel1
    '
    Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel1.Controls.Add(Me.panelEspere)
    Me.Panel1.Controls.Add(Me.DtpFechaInicial)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.DtpFechaFinal)
    Me.Panel1.Controls.Add(Me.btnExportaAgentes)
    Me.Panel1.Controls.Add(Me.Label5)
    Me.Panel1.Controls.Add(Me.CmbAgteVta)
    Me.Panel1.Controls.Add(Me.BtnDetalles)
    Me.Panel1.Controls.Add(Me.CmbSucursal)
    Me.Panel1.Controls.Add(Me.Button2)
    Me.Panel1.Controls.Add(Me.Label10)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.Label3)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(208, 746)
    Me.Panel1.TabIndex = 182
    '
    'panelEspere
    '
    Me.panelEspere.BackColor = System.Drawing.Color.Maroon
    Me.panelEspere.Controls.Add(Me.Label4)
    Me.panelEspere.Location = New System.Drawing.Point(3, 450)
    Me.panelEspere.Name = "panelEspere"
    Me.panelEspere.Size = New System.Drawing.Size(209, 96)
    Me.panelEspere.TabIndex = 182
    Me.panelEspere.Visible = False
    '
    'Label4
    '
    Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label4.ForeColor = System.Drawing.Color.White
    Me.Label4.Location = New System.Drawing.Point(11, 16)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(187, 62)
    Me.Label4.TabIndex = 0
    Me.Label4.Text = "Procesando información por favor espere..."
    '
    'DtpFechaInicial
    '
    Me.DtpFechaInicial.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.DtpFechaInicial.Location = New System.Drawing.Point(16, 29)
    Me.DtpFechaInicial.Name = "DtpFechaInicial"
    Me.DtpFechaInicial.Size = New System.Drawing.Size(185, 23)
    Me.DtpFechaInicial.TabIndex = 180
    Me.DtpFechaInicial.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(14, 13)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(80, 13)
    Me.Label1.TabIndex = 181
    Me.Label1.Text = "Fecha Inicial"
    '
    'DtpFechaFinal
    '
    Me.DtpFechaFinal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.DtpFechaFinal.Location = New System.Drawing.Point(16, 76)
    Me.DtpFechaFinal.Name = "DtpFechaFinal"
    Me.DtpFechaFinal.Size = New System.Drawing.Size(185, 23)
    Me.DtpFechaFinal.TabIndex = 119
    Me.DtpFechaFinal.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
    '
    'btnExportaAgentes
    '
    Me.btnExportaAgentes.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
    Me.btnExportaAgentes.Location = New System.Drawing.Point(87, 311)
    Me.btnExportaAgentes.Name = "btnExportaAgentes"
    Me.btnExportaAgentes.Size = New System.Drawing.Size(36, 34)
    Me.btnExportaAgentes.TabIndex = 179
    Me.btnExportaAgentes.UseVisualStyleBackColor = True
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.Location = New System.Drawing.Point(14, 60)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(73, 13)
    Me.Label5.TabIndex = 122
    Me.Label5.Text = "Fecha Final"
    '
    'CmbAgteVta
    '
    Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbAgteVta.FormattingEnabled = True
    Me.CmbAgteVta.Location = New System.Drawing.Point(16, 170)
    Me.CmbAgteVta.Name = "CmbAgteVta"
    Me.CmbAgteVta.Size = New System.Drawing.Size(185, 21)
    Me.CmbAgteVta.TabIndex = 121
    '
    'BtnDetalles
    '
    Me.BtnDetalles.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
    Me.BtnDetalles.Location = New System.Drawing.Point(87, 383)
    Me.BtnDetalles.Name = "BtnDetalles"
    Me.BtnDetalles.Size = New System.Drawing.Size(36, 34)
    Me.BtnDetalles.TabIndex = 177
    Me.BtnDetalles.UseVisualStyleBackColor = True
    '
    'CmbSucursal
    '
    Me.CmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbSucursal.FormattingEnabled = True
    Me.CmbSucursal.Location = New System.Drawing.Point(16, 124)
    Me.CmbSucursal.Name = "CmbSucursal"
    Me.CmbSucursal.Size = New System.Drawing.Size(185, 21)
    Me.CmbSucursal.TabIndex = 120
    '
    'Button2
    '
    Me.Button2.BackColor = System.Drawing.Color.AliceBlue
    Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
    Me.Button2.Location = New System.Drawing.Point(63, 211)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(86, 36)
    Me.Button2.TabIndex = 123
    Me.Button2.Text = "Consultar"
    Me.Button2.UseVisualStyleBackColor = False
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label10.Location = New System.Drawing.Point(79, 367)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(57, 13)
    Me.Label10.TabIndex = 175
    Me.Label10.Text = "Detalles:"
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label8.Location = New System.Drawing.Point(78, 295)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(57, 13)
    Me.Label8.TabIndex = 174
    Me.Label8.Text = "Agentes:"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label3.Location = New System.Drawing.Point(14, 108)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(56, 13)
    Me.Label3.TabIndex = 132
    Me.Label3.Text = "Sucursal"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(14, 154)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(97, 13)
    Me.Label2.TabIndex = 133
    Me.Label2.Text = "Agente de vtas."
    '
    'Panel2
    '
    Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel2.Controls.Add(Me.GroupBox1)
    Me.Panel2.Controls.Add(Me.GroupBox5)
    Me.Panel2.Controls.Add(Me.GroupBox4)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel2.Location = New System.Drawing.Point(208, 0)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(1190, 746)
    Me.Panel2.TabIndex = 183
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.DgDetalles)
    Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox1.Location = New System.Drawing.Point(0, 345)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(1186, 391)
    Me.GroupBox1.TabIndex = 139
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Detalles"
    '
    'DgDetalles
    '
    Me.DgDetalles.AllowUserToAddRows = False
    Me.DgDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgDetalles.Dock = System.Windows.Forms.DockStyle.Top
    Me.DgDetalles.Location = New System.Drawing.Point(3, 16)
    Me.DgDetalles.Name = "DgDetalles"
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgDetalles.RowsDefaultCellStyle = DataGridViewCellStyle1
    Me.DgDetalles.Size = New System.Drawing.Size(1180, 369)
    Me.DgDetalles.TabIndex = 0
    '
    'GroupBox5
    '
    Me.GroupBox5.Controls.Add(Me.DgAgentes)
    Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox5.Location = New System.Drawing.Point(0, 96)
    Me.GroupBox5.Name = "GroupBox5"
    Me.GroupBox5.Size = New System.Drawing.Size(1186, 249)
    Me.GroupBox5.TabIndex = 138
    Me.GroupBox5.TabStop = False
    Me.GroupBox5.Text = "Agentes"
    '
    'DgAgentes
    '
    Me.DgAgentes.AllowUserToAddRows = False
    Me.DgAgentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgAgentes.Dock = System.Windows.Forms.DockStyle.Top
    Me.DgAgentes.Location = New System.Drawing.Point(3, 16)
    Me.DgAgentes.Name = "DgAgentes"
    DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgAgentes.RowsDefaultCellStyle = DataGridViewCellStyle2
    Me.DgAgentes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DgAgentes.Size = New System.Drawing.Size(1180, 238)
    Me.DgAgentes.TabIndex = 0
    '
    'GroupBox4
    '
    Me.GroupBox4.Controls.Add(Me.DgResumen)
    Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
    Me.GroupBox4.Name = "GroupBox4"
    Me.GroupBox4.Size = New System.Drawing.Size(1186, 96)
    Me.GroupBox4.TabIndex = 137
    Me.GroupBox4.TabStop = False
    Me.GroupBox4.Text = "Resumen"
    '
    'DgResumen
    '
    Me.DgResumen.AllowUserToAddRows = False
    Me.DgResumen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgResumen.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DgResumen.Location = New System.Drawing.Point(3, 16)
    Me.DgResumen.Name = "DgResumen"
    DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgResumen.RowsDefaultCellStyle = DataGridViewCellStyle3
    Me.DgResumen.Size = New System.Drawing.Size(1180, 77)
    Me.DgResumen.TabIndex = 5
    '
    'AgenteListaPrecio
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1398, 746)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Name = "AgenteListaPrecio"
    Me.Text = "Reporte de ventas por agente y listas de precio"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.panelEspere.ResumeLayout(False)
    Me.Panel2.ResumeLayout(False)
    Me.GroupBox1.ResumeLayout(False)
    CType(Me.DgDetalles, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox5.ResumeLayout(False)
    CType(Me.DgAgentes, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox4.ResumeLayout(False)
    CType(Me.DgResumen, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents Panel1 As Panel
  Friend WithEvents DtpFechaFinal As DateTimePicker
  Friend WithEvents btnExportaAgentes As Button
  Friend WithEvents Label5 As Label
  Friend WithEvents CmbAgteVta As ComboBox
  Friend WithEvents BtnDetalles As Button
  Friend WithEvents CmbSucursal As ComboBox
  Friend WithEvents Button2 As Button
  Friend WithEvents Label10 As Label
  Friend WithEvents Label8 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents Panel2 As Panel
  Friend WithEvents GroupBox5 As GroupBox
  Friend WithEvents DgAgentes As DataGridView
  Friend WithEvents GroupBox4 As GroupBox
  Friend WithEvents DgResumen As DataGridView
  Friend WithEvents DtpFechaInicial As DateTimePicker
  Friend WithEvents Label1 As Label
  Friend WithEvents panelEspere As Panel
  Friend WithEvents Label4 As Label
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents DgDetalles As DataGridView
  Friend WithEvents Timer1 As Timer
End Class
