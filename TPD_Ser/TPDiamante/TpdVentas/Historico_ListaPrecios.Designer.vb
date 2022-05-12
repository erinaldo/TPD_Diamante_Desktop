<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Historico_ListaPrecios
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
  Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.Button1 = New System.Windows.Forms.Button()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.panelEspere = New System.Windows.Forms.Panel()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.btnExportaAgentes = New System.Windows.Forms.Button()
  Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
  Me.BtnDetalles = New System.Windows.Forms.Button()
  Me.CmbSucursal = New System.Windows.Forms.ComboBox()
  Me.Button2 = New System.Windows.Forms.Button()
  Me.Label10 = New System.Windows.Forms.Label()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.GroupBox4 = New System.Windows.Forms.GroupBox()
  Me.DgTotales = New System.Windows.Forms.DataGridView()
  Me.GroupBox5 = New System.Windows.Forms.GroupBox()
  Me.DgPorMes = New System.Windows.Forms.DataGridView()
  Me.GroupBox1 = New System.Windows.Forms.GroupBox()
  Me.DgPorAgente = New System.Windows.Forms.DataGridView()
  Me.CopiaDgAgente = New System.Windows.Forms.DataGridView()
  Me.Panel1.SuspendLayout()
  Me.panelEspere.SuspendLayout()
  Me.GroupBox4.SuspendLayout()
  CType(Me.DgTotales, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.GroupBox5.SuspendLayout()
  CType(Me.DgPorMes, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.GroupBox1.SuspendLayout()
  CType(Me.DgPorAgente, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.CopiaDgAgente, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'Panel1
  '
  Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel1.Controls.Add(Me.Button1)
  Me.Panel1.Controls.Add(Me.Label1)
  Me.Panel1.Controls.Add(Me.panelEspere)
  Me.Panel1.Controls.Add(Me.btnExportaAgentes)
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
  Me.Panel1.Size = New System.Drawing.Size(208, 879)
  Me.Panel1.TabIndex = 183
  '
  'Button1
  '
  Me.Button1.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
  Me.Button1.Location = New System.Drawing.Point(87, 399)
  Me.Button1.Name = "Button1"
  Me.Button1.Size = New System.Drawing.Size(36, 34)
  Me.Button1.TabIndex = 184
  Me.Button1.UseVisualStyleBackColor = True
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(79, 383)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(57, 13)
  Me.Label1.TabIndex = 183
  Me.Label1.Text = "Agentes:"
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
  'btnExportaAgentes
  '
  Me.btnExportaAgentes.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
  Me.btnExportaAgentes.Location = New System.Drawing.Point(87, 246)
  Me.btnExportaAgentes.Name = "btnExportaAgentes"
  Me.btnExportaAgentes.Size = New System.Drawing.Size(36, 34)
  Me.btnExportaAgentes.TabIndex = 179
  Me.btnExportaAgentes.UseVisualStyleBackColor = True
  '
  'CmbAgteVta
  '
  Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbAgteVta.FormattingEnabled = True
  Me.CmbAgteVta.Location = New System.Drawing.Point(16, 76)
  Me.CmbAgteVta.Name = "CmbAgteVta"
  Me.CmbAgteVta.Size = New System.Drawing.Size(185, 21)
  Me.CmbAgteVta.TabIndex = 121
  '
  'BtnDetalles
  '
  Me.BtnDetalles.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
  Me.BtnDetalles.Location = New System.Drawing.Point(87, 321)
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
  Me.CmbSucursal.Location = New System.Drawing.Point(16, 30)
  Me.CmbSucursal.Name = "CmbSucursal"
  Me.CmbSucursal.Size = New System.Drawing.Size(185, 21)
  Me.CmbSucursal.TabIndex = 120
  '
  'Button2
  '
  Me.Button2.BackColor = System.Drawing.Color.AliceBlue
  Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
  Me.Button2.Location = New System.Drawing.Point(63, 148)
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
  Me.Label10.Location = New System.Drawing.Point(79, 305)
  Me.Label10.Name = "Label10"
  Me.Label10.Size = New System.Drawing.Size(73, 13)
  Me.Label10.TabIndex = 175
  Me.Label10.Text = "Sucursales:"
  '
  'Label8
  '
  Me.Label8.AutoSize = True
  Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label8.Location = New System.Drawing.Point(79, 230)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(53, 13)
  Me.Label8.TabIndex = 174
  Me.Label8.Text = "Totales:"
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(14, 14)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(56, 13)
  Me.Label3.TabIndex = 132
  Me.Label3.Text = "Sucursal"
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(14, 60)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(97, 13)
  Me.Label2.TabIndex = 133
  Me.Label2.Text = "Agente de vtas."
  '
  'GroupBox4
  '
  Me.GroupBox4.Controls.Add(Me.DgTotales)
  Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
  Me.GroupBox4.Location = New System.Drawing.Point(208, 0)
  Me.GroupBox4.Name = "GroupBox4"
  Me.GroupBox4.Size = New System.Drawing.Size(1246, 245)
  Me.GroupBox4.TabIndex = 184
  Me.GroupBox4.TabStop = False
  Me.GroupBox4.Text = "Totales"
  '
  'DgTotales
  '
  Me.DgTotales.AllowUserToAddRows = False
  Me.DgTotales.ColumnHeadersHeight = 10
  Me.DgTotales.Dock = System.Windows.Forms.DockStyle.Fill
  Me.DgTotales.Location = New System.Drawing.Point(3, 16)
  Me.DgTotales.Name = "DgTotales"
  DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DgTotales.RowsDefaultCellStyle = DataGridViewCellStyle1
  Me.DgTotales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
  Me.DgTotales.Size = New System.Drawing.Size(1240, 226)
  Me.DgTotales.TabIndex = 5
  '
  'GroupBox5
  '
  Me.GroupBox5.Controls.Add(Me.DgPorMes)
  Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
  Me.GroupBox5.Location = New System.Drawing.Point(208, 245)
  Me.GroupBox5.Name = "GroupBox5"
  Me.GroupBox5.Size = New System.Drawing.Size(1246, 240)
  Me.GroupBox5.TabIndex = 185
  Me.GroupBox5.TabStop = False
  Me.GroupBox5.Text = "Sucursales"
  '
  'DgPorMes
  '
  Me.DgPorMes.AllowUserToAddRows = False
  Me.DgPorMes.ColumnHeadersHeight = 10
  Me.DgPorMes.Dock = System.Windows.Forms.DockStyle.Fill
  Me.DgPorMes.Location = New System.Drawing.Point(3, 16)
  Me.DgPorMes.Name = "DgPorMes"
  DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DgPorMes.RowsDefaultCellStyle = DataGridViewCellStyle2
  Me.DgPorMes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
  Me.DgPorMes.Size = New System.Drawing.Size(1240, 221)
  Me.DgPorMes.TabIndex = 1
  '
  'GroupBox1
  '
  Me.GroupBox1.Controls.Add(Me.DgPorAgente)
  Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
  Me.GroupBox1.Location = New System.Drawing.Point(208, 485)
  Me.GroupBox1.Name = "GroupBox1"
  Me.GroupBox1.Size = New System.Drawing.Size(1246, 240)
  Me.GroupBox1.TabIndex = 186
  Me.GroupBox1.TabStop = False
  Me.GroupBox1.Text = "Agente"
  '
  'DgPorAgente
  '
  Me.DgPorAgente.AllowUserToAddRows = False
  Me.DgPorAgente.ColumnHeadersHeight = 10
  Me.DgPorAgente.Dock = System.Windows.Forms.DockStyle.Fill
  Me.DgPorAgente.Location = New System.Drawing.Point(3, 16)
  Me.DgPorAgente.Name = "DgPorAgente"
  DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DgPorAgente.RowsDefaultCellStyle = DataGridViewCellStyle3
  Me.DgPorAgente.Size = New System.Drawing.Size(1240, 221)
  Me.DgPorAgente.TabIndex = 0
  '
  'CopiaDgAgente
  '
  Me.CopiaDgAgente.AllowUserToAddRows = False
  Me.CopiaDgAgente.ColumnHeadersHeight = 10
  Me.CopiaDgAgente.Dock = System.Windows.Forms.DockStyle.Fill
  Me.CopiaDgAgente.Location = New System.Drawing.Point(208, 725)
  Me.CopiaDgAgente.Name = "CopiaDgAgente"
  DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.CopiaDgAgente.RowsDefaultCellStyle = DataGridViewCellStyle4
  Me.CopiaDgAgente.Size = New System.Drawing.Size(1246, 154)
  Me.CopiaDgAgente.TabIndex = 187
  Me.CopiaDgAgente.Visible = False
  '
  'Historico_ListaPrecios
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1454, 879)
  Me.Controls.Add(Me.CopiaDgAgente)
  Me.Controls.Add(Me.GroupBox1)
  Me.Controls.Add(Me.GroupBox5)
  Me.Controls.Add(Me.GroupBox4)
  Me.Controls.Add(Me.Panel1)
  Me.Name = "Historico_ListaPrecios"
  Me.Text = "Histórico de Lista de Precios"
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  Me.panelEspere.ResumeLayout(False)
  Me.GroupBox4.ResumeLayout(False)
  CType(Me.DgTotales, System.ComponentModel.ISupportInitialize).EndInit()
  Me.GroupBox5.ResumeLayout(False)
  CType(Me.DgPorMes, System.ComponentModel.ISupportInitialize).EndInit()
  Me.GroupBox1.ResumeLayout(False)
  CType(Me.DgPorAgente, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.CopiaDgAgente, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)

 End Sub

 Friend WithEvents Panel1 As Panel
 Friend WithEvents panelEspere As Panel
 Friend WithEvents Label4 As Label
 Friend WithEvents btnExportaAgentes As Button
 Friend WithEvents CmbAgteVta As ComboBox
 Friend WithEvents BtnDetalles As Button
 Friend WithEvents CmbSucursal As ComboBox
 Friend WithEvents Button2 As Button
 Friend WithEvents Label10 As Label
 Friend WithEvents Label8 As Label
 Friend WithEvents Label3 As Label
 Friend WithEvents Label2 As Label
 Friend WithEvents GroupBox4 As GroupBox
 Friend WithEvents DgTotales As DataGridView
 Friend WithEvents GroupBox5 As GroupBox
 Friend WithEvents GroupBox1 As GroupBox
 Friend WithEvents DgPorAgente As DataGridView
 Friend WithEvents DgPorMes As DataGridView
 Friend WithEvents CopiaDgAgente As DataGridView
 Friend WithEvents Button1 As Button
 Friend WithEvents Label1 As Label
End Class
