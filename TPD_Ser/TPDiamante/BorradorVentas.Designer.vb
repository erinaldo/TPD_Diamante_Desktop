<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BorradorVentas
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
    Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.DgDetalles = New System.Windows.Forms.DataGridView()
    Me.GroupBox4 = New System.Windows.Forms.GroupBox()
    Me.DgCabecera = New System.Windows.Forms.DataGridView()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Button2 = New System.Windows.Forms.Button()
    Me.CmbLineas = New System.Windows.Forms.ComboBox()
    Me.CmbSolicitante = New System.Windows.Forms.ComboBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.DtpFechaFinal = New System.Windows.Forms.DateTimePicker()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.DtpFechaInicial = New System.Windows.Forms.DateTimePicker()
    Me.panelEspere = New System.Windows.Forms.Panel()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.cmbCategoria = New System.Windows.Forms.ComboBox()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.GroupBox1.SuspendLayout()
    CType(Me.DgDetalles, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox4.SuspendLayout()
    CType(Me.DgCabecera, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.panelEspere.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.DgDetalles)
    Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox1.Location = New System.Drawing.Point(208, 367)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(1114, 375)
    Me.GroupBox1.TabIndex = 186
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
    DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgDetalles.RowsDefaultCellStyle = DataGridViewCellStyle3
    Me.DgDetalles.Size = New System.Drawing.Size(1108, 369)
    Me.DgDetalles.TabIndex = 0
    '
    'GroupBox4
    '
    Me.GroupBox4.Controls.Add(Me.DgCabecera)
    Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox4.Location = New System.Drawing.Point(208, 0)
    Me.GroupBox4.Name = "GroupBox4"
    Me.GroupBox4.Size = New System.Drawing.Size(1114, 367)
    Me.GroupBox4.TabIndex = 184
    Me.GroupBox4.TabStop = False
    Me.GroupBox4.Text = "Cabecera"
    '
    'DgCabecera
    '
    Me.DgCabecera.AllowUserToAddRows = False
    Me.DgCabecera.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgCabecera.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DgCabecera.Location = New System.Drawing.Point(3, 16)
    Me.DgCabecera.Name = "DgCabecera"
    DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgCabecera.RowsDefaultCellStyle = DataGridViewCellStyle4
    Me.DgCabecera.Size = New System.Drawing.Size(1108, 348)
    Me.DgCabecera.TabIndex = 5
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(14, 109)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(87, 13)
    Me.Label2.TabIndex = 133
    Me.Label2.Text = "Quien capturó"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label3.Location = New System.Drawing.Point(14, 161)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(44, 13)
    Me.Label3.TabIndex = 132
    Me.Label3.Text = "Lineas"
    '
    'Button2
    '
    Me.Button2.BackColor = System.Drawing.Color.AliceBlue
    Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
    Me.Button2.Location = New System.Drawing.Point(51, 290)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(86, 36)
    Me.Button2.TabIndex = 123
    Me.Button2.Text = "Consultar"
    Me.Button2.UseVisualStyleBackColor = False
    '
    'CmbLineas
    '
    Me.CmbLineas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbLineas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbLineas.FormattingEnabled = True
    Me.CmbLineas.Location = New System.Drawing.Point(16, 177)
    Me.CmbLineas.Name = "CmbLineas"
    Me.CmbLineas.Size = New System.Drawing.Size(185, 21)
    Me.CmbLineas.TabIndex = 120
    '
    'CmbSolicitante
    '
    Me.CmbSolicitante.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbSolicitante.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbSolicitante.FormattingEnabled = True
    Me.CmbSolicitante.Location = New System.Drawing.Point(16, 125)
    Me.CmbSolicitante.Name = "CmbSolicitante"
    Me.CmbSolicitante.Size = New System.Drawing.Size(185, 21)
    Me.CmbSolicitante.TabIndex = 121
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
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label6.Location = New System.Drawing.Point(15, 212)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(63, 13)
    Me.Label6.TabIndex = 184
    Me.Label6.Text = "Categoría"
    '
    'cmbCategoria
    '
    Me.cmbCategoria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.cmbCategoria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.cmbCategoria.FormattingEnabled = True
    Me.cmbCategoria.Location = New System.Drawing.Point(17, 228)
    Me.cmbCategoria.Name = "cmbCategoria"
    Me.cmbCategoria.Size = New System.Drawing.Size(185, 21)
    Me.cmbCategoria.TabIndex = 183
    '
    'Panel1
    '
    Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel1.Controls.Add(Me.cmbCategoria)
    Me.Panel1.Controls.Add(Me.Label6)
    Me.Panel1.Controls.Add(Me.panelEspere)
    Me.Panel1.Controls.Add(Me.DtpFechaInicial)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.DtpFechaFinal)
    Me.Panel1.Controls.Add(Me.Label5)
    Me.Panel1.Controls.Add(Me.CmbSolicitante)
    Me.Panel1.Controls.Add(Me.CmbLineas)
    Me.Panel1.Controls.Add(Me.Button2)
    Me.Panel1.Controls.Add(Me.Label3)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(208, 749)
    Me.Panel1.TabIndex = 183
    '
    'BorradorVentas
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1322, 749)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.GroupBox4)
    Me.Controls.Add(Me.Panel1)
    Me.Name = "BorradorVentas"
    Me.Text = "Borrador Ventas"
    Me.GroupBox1.ResumeLayout(False)
    CType(Me.DgDetalles, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox4.ResumeLayout(False)
    CType(Me.DgCabecera, System.ComponentModel.ISupportInitialize).EndInit()
    Me.panelEspere.ResumeLayout(False)
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents DgDetalles As DataGridView
  Friend WithEvents GroupBox4 As GroupBox
  Friend WithEvents DgCabecera As DataGridView
  Friend WithEvents Label2 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents Button2 As Button
  Friend WithEvents CmbLineas As ComboBox
  Friend WithEvents CmbSolicitante As ComboBox
  Friend WithEvents Label5 As Label
  Friend WithEvents DtpFechaFinal As DateTimePicker
  Friend WithEvents Label1 As Label
  Friend WithEvents DtpFechaInicial As DateTimePicker
  Friend WithEvents panelEspere As Panel
  Friend WithEvents Label4 As Label
  Friend WithEvents Label6 As Label
  Friend WithEvents cmbCategoria As ComboBox
  Friend WithEvents Panel1 As Panel
End Class
