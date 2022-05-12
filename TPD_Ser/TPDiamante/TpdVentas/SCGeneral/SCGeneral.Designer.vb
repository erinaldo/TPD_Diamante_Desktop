<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SCGeneral
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
    Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Me.GroupBox3 = New System.Windows.Forms.GroupBox()
    Me.DgTotales = New System.Windows.Forms.DataGridView()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.txtDiasMes = New System.Windows.Forms.TextBox()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.txtDiasTranscurridos = New System.Windows.Forms.TextBox()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.txtDiasRestantes = New System.Windows.Forms.TextBox()
    Me.label9 = New System.Windows.Forms.Label()
    Me.txtAvanceOptimo = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Button2 = New System.Windows.Forms.Button()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
    Me.CmbSucursal = New System.Windows.Forms.ComboBox()
    Me.GroupBox3.SuspendLayout()
    CType(Me.DgTotales, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'GroupBox3
    '
    Me.GroupBox3.Controls.Add(Me.DgTotales)
    Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
    Me.GroupBox3.Location = New System.Drawing.Point(194, 0)
    Me.GroupBox3.Name = "GroupBox3"
    Me.GroupBox3.Size = New System.Drawing.Size(784, 171)
    Me.GroupBox3.TabIndex = 220
    Me.GroupBox3.TabStop = False
    Me.GroupBox3.Text = "Concentrado de Totales"
    '
    'DgTotales
    '
    Me.DgTotales.AllowUserToAddRows = False
    Me.DgTotales.AllowUserToDeleteRows = False
    Me.DgTotales.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DgTotales.Location = New System.Drawing.Point(3, 16)
    Me.DgTotales.Name = "DgTotales"
    Me.DgTotales.ReadOnly = True
    DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgTotales.RowsDefaultCellStyle = DataGridViewCellStyle2
    Me.DgTotales.Size = New System.Drawing.Size(778, 152)
    Me.DgTotales.TabIndex = 5
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.Label7)
    Me.Panel1.Controls.Add(Me.txtDiasMes)
    Me.Panel1.Controls.Add(Me.Label8)
    Me.Panel1.Controls.Add(Me.txtDiasTranscurridos)
    Me.Panel1.Controls.Add(Me.Label6)
    Me.Panel1.Controls.Add(Me.Button1)
    Me.Panel1.Controls.Add(Me.txtDiasRestantes)
    Me.Panel1.Controls.Add(Me.label9)
    Me.Panel1.Controls.Add(Me.txtAvanceOptimo)
    Me.Panel1.Controls.Add(Me.Label4)
    Me.Panel1.Controls.Add(Me.Button2)
    Me.Panel1.Controls.Add(Me.Label5)
    Me.Panel1.Controls.Add(Me.Label3)
    Me.Panel1.Controls.Add(Me.DtpFechaIni)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Controls.Add(Me.CmbAgteVta)
    Me.Panel1.Controls.Add(Me.CmbSucursal)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(194, 413)
    Me.Panel1.TabIndex = 219
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label7.Location = New System.Drawing.Point(16, 19)
    Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(53, 13)
    Me.Label7.TabIndex = 194
    Me.Label7.Text = "Días Mes"
    '
    'txtDiasMes
    '
    Me.txtDiasMes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtDiasMes.Location = New System.Drawing.Point(116, 15)
    Me.txtDiasMes.Margin = New System.Windows.Forms.Padding(4)
    Me.txtDiasMes.Name = "txtDiasMes"
    Me.txtDiasMes.ReadOnly = True
    Me.txtDiasMes.Size = New System.Drawing.Size(65, 20)
    Me.txtDiasMes.TabIndex = 192
    Me.txtDiasMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(66, 348)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(42, 13)
    Me.Label8.TabIndex = 209
    Me.Label8.Text = "Totales"
    '
    'txtDiasTranscurridos
    '
    Me.txtDiasTranscurridos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtDiasTranscurridos.Location = New System.Drawing.Point(116, 36)
    Me.txtDiasTranscurridos.Margin = New System.Windows.Forms.Padding(4)
    Me.txtDiasTranscurridos.Name = "txtDiasTranscurridos"
    Me.txtDiasTranscurridos.ReadOnly = True
    Me.txtDiasTranscurridos.Size = New System.Drawing.Size(65, 20)
    Me.txtDiasTranscurridos.TabIndex = 193
    Me.txtDiasTranscurridos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label6.Location = New System.Drawing.Point(16, 40)
    Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(97, 13)
    Me.Label6.TabIndex = 195
    Me.Label6.Text = "Días Transcurridos"
    '
    'Button1
    '
    Me.Button1.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
    Me.Button1.Location = New System.Drawing.Point(69, 364)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(36, 34)
    Me.Button1.TabIndex = 206
    Me.Button1.UseVisualStyleBackColor = True
    '
    'txtDiasRestantes
    '
    Me.txtDiasRestantes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtDiasRestantes.Location = New System.Drawing.Point(116, 57)
    Me.txtDiasRestantes.Margin = New System.Windows.Forms.Padding(4)
    Me.txtDiasRestantes.Name = "txtDiasRestantes"
    Me.txtDiasRestantes.ReadOnly = True
    Me.txtDiasRestantes.Size = New System.Drawing.Size(65, 20)
    Me.txtDiasRestantes.TabIndex = 196
    Me.txtDiasRestantes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'label9
    '
    Me.label9.AutoSize = True
    Me.label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.label9.Location = New System.Drawing.Point(16, 61)
    Me.label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.label9.Name = "label9"
    Me.label9.Size = New System.Drawing.Size(81, 13)
    Me.label9.TabIndex = 197
    Me.label9.Text = "Días Restantes"
    '
    'txtAvanceOptimo
    '
    Me.txtAvanceOptimo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtAvanceOptimo.Location = New System.Drawing.Point(116, 78)
    Me.txtAvanceOptimo.Margin = New System.Windows.Forms.Padding(4)
    Me.txtAvanceOptimo.Name = "txtAvanceOptimo"
    Me.txtAvanceOptimo.ReadOnly = True
    Me.txtAvanceOptimo.Size = New System.Drawing.Size(65, 20)
    Me.txtAvanceOptimo.TabIndex = 198
    Me.txtAvanceOptimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label4.Location = New System.Drawing.Point(16, 82)
    Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(80, 13)
    Me.Label4.TabIndex = 199
    Me.Label4.Text = "Avance Optimo"
    '
    'Button2
    '
    Me.Button2.BackColor = System.Drawing.Color.AliceBlue
    Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
    Me.Button2.Location = New System.Drawing.Point(43, 290)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(91, 38)
    Me.Button2.TabIndex = 202
    Me.Button2.Text = "Consultar"
    Me.Button2.UseVisualStyleBackColor = False
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
    Me.Label5.Location = New System.Drawing.Point(16, 114)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(71, 15)
    Me.Label5.TabIndex = 191
    Me.Label5.Text = "Fecha Final"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
    Me.Label3.Location = New System.Drawing.Point(16, 170)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(55, 15)
    Me.Label3.TabIndex = 200
    Me.Label3.Text = "Sucursal"
    '
    'DtpFechaIni
    '
    Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.DtpFechaIni.Location = New System.Drawing.Point(19, 132)
    Me.DtpFechaIni.Name = "DtpFechaIni"
    Me.DtpFechaIni.Size = New System.Drawing.Size(112, 23)
    Me.DtpFechaIni.TabIndex = 188
    Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
    Me.Label2.Location = New System.Drawing.Point(16, 221)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(89, 15)
    Me.Label2.TabIndex = 201
    Me.Label2.Text = "Agente de vtas."
    '
    'CmbAgteVta
    '
    Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbAgteVta.FormattingEnabled = True
    Me.CmbAgteVta.Location = New System.Drawing.Point(19, 239)
    Me.CmbAgteVta.Name = "CmbAgteVta"
    Me.CmbAgteVta.Size = New System.Drawing.Size(139, 21)
    Me.CmbAgteVta.TabIndex = 190
    '
    'CmbSucursal
    '
    Me.CmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbSucursal.FormattingEnabled = True
    Me.CmbSucursal.Location = New System.Drawing.Point(19, 188)
    Me.CmbSucursal.Name = "CmbSucursal"
    Me.CmbSucursal.Size = New System.Drawing.Size(139, 21)
    Me.CmbSucursal.TabIndex = 189
    '
    'SCGeneral
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoSize = True
    Me.ClientSize = New System.Drawing.Size(978, 413)
    Me.Controls.Add(Me.GroupBox3)
    Me.Controls.Add(Me.Panel1)
    Me.Name = "SCGeneral"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Score Card General"
    Me.GroupBox3.ResumeLayout(False)
    CType(Me.DgTotales, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBox3 As GroupBox
  Friend WithEvents DgTotales As DataGridView
  Friend WithEvents Panel1 As Panel
  Private WithEvents Label7 As Label
  Private WithEvents txtDiasMes As TextBox
  Friend WithEvents Label8 As Label
  Private WithEvents txtDiasTranscurridos As TextBox
  Private WithEvents Label6 As Label
  Friend WithEvents Button1 As Button
  Private WithEvents txtDiasRestantes As TextBox
  Private WithEvents label9 As Label
  Private WithEvents txtAvanceOptimo As TextBox
  Private WithEvents Label4 As Label
  Friend WithEvents Button2 As Button
  Friend WithEvents Label5 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents DtpFechaIni As DateTimePicker
  Friend WithEvents Label2 As Label
  Friend WithEvents CmbAgteVta As ComboBox
  Friend WithEvents CmbSucursal As ComboBox
End Class
