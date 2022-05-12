<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LineasHalcon
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
  Me.Label1 = New System.Windows.Forms.Label()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.Button4 = New System.Windows.Forms.Button()
  Me.Button1 = New System.Windows.Forms.Button()
  Me.Button3 = New System.Windows.Forms.Button()
  Me.DgVtaAgte = New System.Windows.Forms.DataGridView()
  Me.DgTotales = New System.Windows.Forms.DataGridView()
  Me.Button2 = New System.Windows.Forms.Button()
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
  Me.CmbSucursal = New System.Windows.Forms.ComboBox()
  Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.Label10 = New System.Windows.Forms.Label()
  Me.Button5 = New System.Windows.Forms.Button()
  Me.Panel2 = New System.Windows.Forms.Panel()
  Me.GroupBox5 = New System.Windows.Forms.GroupBox()
  Me.dgvVtaLinea = New System.Windows.Forms.DataGridView()
  Me.GroupBox4 = New System.Windows.Forms.GroupBox()
  Me.GroupBox3 = New System.Windows.Forms.GroupBox()
  CType(Me.DgVtaAgte, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.DgTotales, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.Panel1.SuspendLayout()
  Me.Panel2.SuspendLayout()
  Me.GroupBox5.SuspendLayout()
  CType(Me.dgvVtaLinea, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.GroupBox4.SuspendLayout()
  Me.GroupBox3.SuspendLayout()
  Me.SuspendLayout()
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Location = New System.Drawing.Point(56, 387)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(64, 13)
  Me.Label1.TabIndex = 210
  Me.Label1.Text = "Vendedores"
  '
  'Label8
  '
  Me.Label8.AutoSize = True
  Me.Label8.Location = New System.Drawing.Point(66, 360)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(42, 13)
  Me.Label8.TabIndex = 209
  Me.Label8.Text = "Totales"
  Me.Label8.Visible = False
  '
  'Button4
  '
  Me.Button4.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
  Me.Button4.Location = New System.Drawing.Point(69, 403)
  Me.Button4.Name = "Button4"
  Me.Button4.Size = New System.Drawing.Size(36, 34)
  Me.Button4.TabIndex = 207
  Me.Button4.UseVisualStyleBackColor = True
  '
  'Button1
  '
  Me.Button1.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
  Me.Button1.Location = New System.Drawing.Point(69, 376)
  Me.Button1.Name = "Button1"
  Me.Button1.Size = New System.Drawing.Size(36, 34)
  Me.Button1.TabIndex = 206
  Me.Button1.UseVisualStyleBackColor = True
  Me.Button1.Visible = False
  '
  'Button3
  '
  Me.Button3.BackColor = System.Drawing.Color.AliceBlue
  Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button3.ForeColor = System.Drawing.Color.MediumBlue
  Me.Button3.Location = New System.Drawing.Point(631, 39)
  Me.Button3.Name = "Button3"
  Me.Button3.Size = New System.Drawing.Size(139, 56)
  Me.Button3.TabIndex = 205
  Me.Button3.Text = "Ingresar Objetivos"
  Me.Button3.UseVisualStyleBackColor = False
  '
  'DgVtaAgte
  '
  Me.DgVtaAgte.AllowUserToAddRows = False
  Me.DgVtaAgte.AllowUserToDeleteRows = False
  Me.DgVtaAgte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.DgVtaAgte.Dock = System.Windows.Forms.DockStyle.Fill
  Me.DgVtaAgte.Location = New System.Drawing.Point(3, 16)
  Me.DgVtaAgte.Name = "DgVtaAgte"
  Me.DgVtaAgte.ReadOnly = True
  DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DgVtaAgte.RowsDefaultCellStyle = DataGridViewCellStyle1
  Me.DgVtaAgte.Size = New System.Drawing.Size(1328, 239)
  Me.DgVtaAgte.TabIndex = 0
  '
  'DgTotales
  '
  Me.DgTotales.AllowUserToAddRows = False
  Me.DgTotales.AllowUserToDeleteRows = False
  Me.DgTotales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.DgTotales.Dock = System.Windows.Forms.DockStyle.Fill
  Me.DgTotales.Location = New System.Drawing.Point(3, 16)
  Me.DgTotales.Name = "DgTotales"
  Me.DgTotales.ReadOnly = True
  DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DgTotales.RowsDefaultCellStyle = DataGridViewCellStyle2
  Me.DgTotales.Size = New System.Drawing.Size(1328, 152)
  Me.DgTotales.TabIndex = 5
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
  'Panel1
  '
  Me.Panel1.Controls.Add(Me.Label10)
  Me.Panel1.Controls.Add(Me.Button5)
  Me.Panel1.Controls.Add(Me.Label7)
  Me.Panel1.Controls.Add(Me.Label1)
  Me.Panel1.Controls.Add(Me.txtDiasMes)
  Me.Panel1.Controls.Add(Me.Label8)
  Me.Panel1.Controls.Add(Me.txtDiasTranscurridos)
  Me.Panel1.Controls.Add(Me.Button4)
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
  Me.Panel1.Size = New System.Drawing.Size(194, 716)
  Me.Panel1.TabIndex = 211
  '
  'Label10
  '
  Me.Label10.AutoSize = True
  Me.Label10.Location = New System.Drawing.Point(66, 452)
  Me.Label10.Name = "Label10"
  Me.Label10.Size = New System.Drawing.Size(35, 13)
  Me.Label10.TabIndex = 212
  Me.Label10.Text = "Línea"
  '
  'Button5
  '
  Me.Button5.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
  Me.Button5.Location = New System.Drawing.Point(69, 468)
  Me.Button5.Name = "Button5"
  Me.Button5.Size = New System.Drawing.Size(36, 34)
  Me.Button5.TabIndex = 211
  Me.Button5.UseVisualStyleBackColor = True
  '
  'Panel2
  '
  Me.Panel2.Controls.Add(Me.GroupBox5)
  Me.Panel2.Controls.Add(Me.GroupBox4)
  Me.Panel2.Controls.Add(Me.GroupBox3)
  Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Panel2.Location = New System.Drawing.Point(194, 0)
  Me.Panel2.Name = "Panel2"
  Me.Panel2.Size = New System.Drawing.Size(1334, 716)
  Me.Panel2.TabIndex = 212
  '
  'GroupBox5
  '
  Me.GroupBox5.Controls.Add(Me.dgvVtaLinea)
  Me.GroupBox5.Controls.Add(Me.Button3)
  Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
  Me.GroupBox5.Location = New System.Drawing.Point(0, 429)
  Me.GroupBox5.Name = "GroupBox5"
  Me.GroupBox5.Size = New System.Drawing.Size(1334, 287)
  Me.GroupBox5.TabIndex = 207
  Me.GroupBox5.TabStop = False
  Me.GroupBox5.Text = "Lineas Halcon"
  '
  'dgvVtaLinea
  '
  Me.dgvVtaLinea.AllowUserToAddRows = False
  Me.dgvVtaLinea.AllowUserToDeleteRows = False
  Me.dgvVtaLinea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgvVtaLinea.Dock = System.Windows.Forms.DockStyle.Fill
  Me.dgvVtaLinea.Location = New System.Drawing.Point(3, 16)
  Me.dgvVtaLinea.Name = "dgvVtaLinea"
  Me.dgvVtaLinea.ReadOnly = True
  Me.dgvVtaLinea.Size = New System.Drawing.Size(1328, 268)
  Me.dgvVtaLinea.TabIndex = 0
  '
  'GroupBox4
  '
  Me.GroupBox4.Controls.Add(Me.DgVtaAgte)
  Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
  Me.GroupBox4.Location = New System.Drawing.Point(0, 171)
  Me.GroupBox4.Name = "GroupBox4"
  Me.GroupBox4.Size = New System.Drawing.Size(1334, 258)
  Me.GroupBox4.TabIndex = 206
  Me.GroupBox4.TabStop = False
  Me.GroupBox4.Text = "Vendedores"
  '
  'GroupBox3
  '
  Me.GroupBox3.Controls.Add(Me.DgTotales)
  Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
  Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
  Me.GroupBox3.Name = "GroupBox3"
  Me.GroupBox3.Size = New System.Drawing.Size(1334, 171)
  Me.GroupBox3.TabIndex = 205
  Me.GroupBox3.TabStop = False
  Me.GroupBox3.Text = "Totales"
  '
  'LineasHalcon
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1528, 716)
  Me.Controls.Add(Me.Panel2)
  Me.Controls.Add(Me.Panel1)
  Me.Name = "LineasHalcon"
  Me.ShowIcon = False
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "SC Lineas Halcon"
  CType(Me.DgVtaAgte, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.DgTotales, System.ComponentModel.ISupportInitialize).EndInit()
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  Me.Panel2.ResumeLayout(False)
  Me.GroupBox5.ResumeLayout(False)
  CType(Me.dgvVtaLinea, System.ComponentModel.ISupportInitialize).EndInit()
  Me.GroupBox4.ResumeLayout(False)
  Me.GroupBox3.ResumeLayout(False)
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents DgVtaAgte As System.Windows.Forms.DataGridView
    Friend WithEvents DgTotales As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
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
    Friend WithEvents CmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents dgvVtaLinea As DataGridView
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Button5 As Button
End Class
