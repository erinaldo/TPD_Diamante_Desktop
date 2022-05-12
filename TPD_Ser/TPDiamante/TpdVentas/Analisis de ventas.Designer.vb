<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Analisis_de_ventas
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
  Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
  Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
  Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.ckCteProp = New System.Windows.Forms.CheckBox()
  Me.panelEspere = New System.Windows.Forms.Panel()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.Button2 = New System.Windows.Forms.Button()
  Me.CmbArticulo = New System.Windows.Forms.ComboBox()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.CmbGrupoArticulo = New System.Windows.Forms.ComboBox()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
  Me.rdb_PorLinea = New System.Windows.Forms.RadioButton()
  Me.rdb_PorProducto = New System.Windows.Forms.RadioButton()
  Me.Panel2 = New System.Windows.Forms.Panel()
  Me.dgv_Cabecera = New System.Windows.Forms.DataGridView()
  Me.Panel3 = New System.Windows.Forms.Panel()
  Me.dgv_Producto = New System.Windows.Forms.DataGridView()
  Me.Panel4 = New System.Windows.Forms.Panel()
  Me.btn_Exportar = New System.Windows.Forms.Button()
  Me.cbo_TipoGrafica = New System.Windows.Forms.ComboBox()
  Me.btnGrafica = New System.Windows.Forms.Button()
  Me.Panel7 = New System.Windows.Forms.Panel()
  Me.rdb_PrecioPromedio = New System.Windows.Forms.RadioButton()
  Me.rdb_ImporteVentas = New System.Windows.Forms.RadioButton()
  Me.rdb_Piezas = New System.Windows.Forms.RadioButton()
  Me.Panel6 = New System.Windows.Forms.Panel()
  Me.rdb_tuxtla = New System.Windows.Forms.RadioButton()
  Me.rdb_merida = New System.Windows.Forms.RadioButton()
  Me.rdb_puebla = New System.Windows.Forms.RadioButton()
  Me.rdb_todos = New System.Windows.Forms.RadioButton()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.Panel5 = New System.Windows.Forms.Panel()
  Me.chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
  Me.Panel1.SuspendLayout()
  Me.panelEspere.SuspendLayout()
  Me.Panel2.SuspendLayout()
  CType(Me.dgv_Cabecera, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.Panel3.SuspendLayout()
  CType(Me.dgv_Producto, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.Panel4.SuspendLayout()
  Me.Panel7.SuspendLayout()
  Me.Panel6.SuspendLayout()
  Me.Panel5.SuspendLayout()
  CType(Me.chart1, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'Panel1
  '
  Me.Panel1.Controls.Add(Me.ckCteProp)
  Me.Panel1.Controls.Add(Me.panelEspere)
  Me.Panel1.Controls.Add(Me.Button2)
  Me.Panel1.Controls.Add(Me.CmbArticulo)
  Me.Panel1.Controls.Add(Me.Label6)
  Me.Panel1.Controls.Add(Me.CmbGrupoArticulo)
  Me.Panel1.Controls.Add(Me.Label4)
  Me.Panel1.Controls.Add(Me.Label3)
  Me.Panel1.Controls.Add(Me.DtpFechaTer)
  Me.Panel1.Controls.Add(Me.Label5)
  Me.Panel1.Controls.Add(Me.DtpFechaIni)
  Me.Panel1.Controls.Add(Me.rdb_PorLinea)
  Me.Panel1.Controls.Add(Me.rdb_PorProducto)
  Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
  Me.Panel1.Location = New System.Drawing.Point(0, 0)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(1155, 130)
  Me.Panel1.TabIndex = 0
  '
  'ckCteProp
  '
  Me.ckCteProp.AutoSize = True
  Me.ckCteProp.Checked = True
  Me.ckCteProp.CheckState = System.Windows.Forms.CheckState.Checked
  Me.ckCteProp.Location = New System.Drawing.Point(309, 13)
  Me.ckCteProp.Name = "ckCteProp"
  Me.ckCteProp.Size = New System.Drawing.Size(130, 17)
  Me.ckCteProp.TabIndex = 185
  Me.ckCteProp.Text = "Incluir clientes propios"
  Me.ckCteProp.UseVisualStyleBackColor = True
  '
  'panelEspere
  '
  Me.panelEspere.BackColor = System.Drawing.Color.Maroon
  Me.panelEspere.Controls.Add(Me.Label1)
  Me.panelEspere.Location = New System.Drawing.Point(921, 18)
  Me.panelEspere.Name = "panelEspere"
  Me.panelEspere.Size = New System.Drawing.Size(209, 96)
  Me.panelEspere.TabIndex = 184
  Me.panelEspere.Visible = False
  '
  'Label1
  '
  Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.ForeColor = System.Drawing.Color.White
  Me.Label1.Location = New System.Drawing.Point(11, 16)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(187, 62)
  Me.Label1.TabIndex = 0
  Me.Label1.Text = "Procesando información por favor espere..."
  '
  'Button2
  '
  Me.Button2.BackColor = System.Drawing.Color.AliceBlue
  Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
  Me.Button2.Location = New System.Drawing.Point(800, 54)
  Me.Button2.Name = "Button2"
  Me.Button2.Size = New System.Drawing.Size(86, 36)
  Me.Button2.TabIndex = 183
  Me.Button2.Text = "Consultar"
  Me.Button2.UseVisualStyleBackColor = False
  '
  'CmbArticulo
  '
  Me.CmbArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbArticulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.CmbArticulo.FormattingEnabled = True
  Me.CmbArticulo.Location = New System.Drawing.Point(503, 88)
  Me.CmbArticulo.Name = "CmbArticulo"
  Me.CmbArticulo.Size = New System.Drawing.Size(269, 21)
  Me.CmbArticulo.TabIndex = 17
  '
  'Label6
  '
  Me.Label6.AutoSize = True
  Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label6.Location = New System.Drawing.Point(445, 89)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(55, 17)
  Me.Label6.TabIndex = 21
  Me.Label6.Text = "Artículo"
  '
  'CmbGrupoArticulo
  '
  Me.CmbGrupoArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbGrupoArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbGrupoArticulo.FormattingEnabled = True
  Me.CmbGrupoArticulo.Location = New System.Drawing.Point(114, 87)
  Me.CmbGrupoArticulo.Name = "CmbGrupoArticulo"
  Me.CmbGrupoArticulo.Size = New System.Drawing.Size(265, 21)
  Me.CmbGrupoArticulo.TabIndex = 16
  '
  'Label4
  '
  Me.Label4.AutoSize = True
  Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label4.Location = New System.Drawing.Point(64, 88)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(43, 17)
  Me.Label4.TabIndex = 19
  Me.Label4.Text = "Línea"
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(395, 44)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(103, 17)
  Me.Label3.TabIndex = 20
  Me.Label3.Text = "Fecha Término"
  '
  'DtpFechaTer
  '
  Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DtpFechaTer.Location = New System.Drawing.Point(503, 39)
  Me.DtpFechaTer.Name = "DtpFechaTer"
  Me.DtpFechaTer.Size = New System.Drawing.Size(270, 23)
  Me.DtpFechaTer.TabIndex = 15
  Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
  '
  'Label5
  '
  Me.Label5.AutoSize = True
  Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label5.Location = New System.Drawing.Point(24, 43)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(83, 17)
  Me.Label5.TabIndex = 18
  Me.Label5.Text = "Fecha Inicio"
  '
  'DtpFechaIni
  '
  Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DtpFechaIni.Location = New System.Drawing.Point(114, 39)
  Me.DtpFechaIni.Name = "DtpFechaIni"
  Me.DtpFechaIni.Size = New System.Drawing.Size(270, 23)
  Me.DtpFechaIni.TabIndex = 14
  Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
  '
  'rdb_PorLinea
  '
  Me.rdb_PorLinea.AutoSize = True
  Me.rdb_PorLinea.Location = New System.Drawing.Point(180, 13)
  Me.rdb_PorLinea.Name = "rdb_PorLinea"
  Me.rdb_PorLinea.Size = New System.Drawing.Size(70, 17)
  Me.rdb_PorLinea.TabIndex = 1
  Me.rdb_PorLinea.TabStop = True
  Me.rdb_PorLinea.Text = "Por Linea"
  Me.rdb_PorLinea.UseVisualStyleBackColor = True
  '
  'rdb_PorProducto
  '
  Me.rdb_PorProducto.AutoSize = True
  Me.rdb_PorProducto.Location = New System.Drawing.Point(49, 13)
  Me.rdb_PorProducto.Name = "rdb_PorProducto"
  Me.rdb_PorProducto.Size = New System.Drawing.Size(86, 17)
  Me.rdb_PorProducto.TabIndex = 0
  Me.rdb_PorProducto.TabStop = True
  Me.rdb_PorProducto.Text = "Por producto"
  Me.rdb_PorProducto.UseVisualStyleBackColor = True
  '
  'Panel2
  '
  Me.Panel2.Controls.Add(Me.dgv_Cabecera)
  Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
  Me.Panel2.Location = New System.Drawing.Point(0, 130)
  Me.Panel2.Name = "Panel2"
  Me.Panel2.Size = New System.Drawing.Size(1155, 24)
  Me.Panel2.TabIndex = 3
  '
  'dgv_Cabecera
  '
  Me.dgv_Cabecera.AllowUserToAddRows = False
  Me.dgv_Cabecera.AllowUserToDeleteRows = False
  Me.dgv_Cabecera.AllowUserToResizeColumns = False
  Me.dgv_Cabecera.AllowUserToResizeRows = False
  Me.dgv_Cabecera.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgv_Cabecera.Dock = System.Windows.Forms.DockStyle.Top
  Me.dgv_Cabecera.Location = New System.Drawing.Point(0, 0)
  Me.dgv_Cabecera.Name = "dgv_Cabecera"
  Me.dgv_Cabecera.ScrollBars = System.Windows.Forms.ScrollBars.None
  Me.dgv_Cabecera.Size = New System.Drawing.Size(1155, 23)
  Me.dgv_Cabecera.TabIndex = 2
  '
  'Panel3
  '
  Me.Panel3.Controls.Add(Me.dgv_Producto)
  Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
  Me.Panel3.Location = New System.Drawing.Point(0, 154)
  Me.Panel3.Name = "Panel3"
  Me.Panel3.Size = New System.Drawing.Size(1155, 142)
  Me.Panel3.TabIndex = 4
  '
  'dgv_Producto
  '
  Me.dgv_Producto.AllowUserToAddRows = False
  Me.dgv_Producto.AllowUserToDeleteRows = False
  Me.dgv_Producto.AllowUserToResizeColumns = False
  Me.dgv_Producto.AllowUserToResizeRows = False
  Me.dgv_Producto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgv_Producto.Dock = System.Windows.Forms.DockStyle.Fill
  Me.dgv_Producto.EnableHeadersVisualStyles = False
  Me.dgv_Producto.Location = New System.Drawing.Point(0, 0)
  Me.dgv_Producto.Name = "dgv_Producto"
  Me.dgv_Producto.Size = New System.Drawing.Size(1155, 142)
  Me.dgv_Producto.TabIndex = 3
  '
  'Panel4
  '
  Me.Panel4.Controls.Add(Me.btn_Exportar)
  Me.Panel4.Controls.Add(Me.cbo_TipoGrafica)
  Me.Panel4.Controls.Add(Me.btnGrafica)
  Me.Panel4.Controls.Add(Me.Panel7)
  Me.Panel4.Controls.Add(Me.Panel6)
  Me.Panel4.Controls.Add(Me.Label2)
  Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
  Me.Panel4.Location = New System.Drawing.Point(0, 296)
  Me.Panel4.Name = "Panel4"
  Me.Panel4.Size = New System.Drawing.Size(1155, 83)
  Me.Panel4.TabIndex = 5
  '
  'btn_Exportar
  '
  Me.btn_Exportar.BackColor = System.Drawing.Color.AliceBlue
  Me.btn_Exportar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.btn_Exportar.ForeColor = System.Drawing.Color.MediumBlue
  Me.btn_Exportar.Location = New System.Drawing.Point(881, 15)
  Me.btn_Exportar.Name = "btn_Exportar"
  Me.btn_Exportar.Size = New System.Drawing.Size(86, 52)
  Me.btn_Exportar.TabIndex = 186
  Me.btn_Exportar.Text = "Exportar gráfica"
  Me.btn_Exportar.UseVisualStyleBackColor = False
  '
  'cbo_TipoGrafica
  '
  Me.cbo_TipoGrafica.FormattingEnabled = True
  Me.cbo_TipoGrafica.Location = New System.Drawing.Point(625, 34)
  Me.cbo_TipoGrafica.Name = "cbo_TipoGrafica"
  Me.cbo_TipoGrafica.Size = New System.Drawing.Size(248, 21)
  Me.cbo_TipoGrafica.TabIndex = 185
  '
  'btnGrafica
  '
  Me.btnGrafica.BackColor = System.Drawing.Color.AliceBlue
  Me.btnGrafica.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.btnGrafica.ForeColor = System.Drawing.Color.MediumBlue
  Me.btnGrafica.Location = New System.Drawing.Point(533, 16)
  Me.btnGrafica.Name = "btnGrafica"
  Me.btnGrafica.Size = New System.Drawing.Size(86, 52)
  Me.btnGrafica.TabIndex = 184
  Me.btnGrafica.Text = "Generar gráfica"
  Me.btnGrafica.UseVisualStyleBackColor = False
  '
  'Panel7
  '
  Me.Panel7.Controls.Add(Me.rdb_PrecioPromedio)
  Me.Panel7.Controls.Add(Me.rdb_ImporteVentas)
  Me.Panel7.Controls.Add(Me.rdb_Piezas)
  Me.Panel7.Location = New System.Drawing.Point(93, 46)
  Me.Panel7.Name = "Panel7"
  Me.Panel7.Size = New System.Drawing.Size(434, 28)
  Me.Panel7.TabIndex = 9
  '
  'rdb_PrecioPromedio
  '
  Me.rdb_PrecioPromedio.AutoSize = True
  Me.rdb_PrecioPromedio.Location = New System.Drawing.Point(325, 7)
  Me.rdb_PrecioPromedio.Name = "rdb_PrecioPromedio"
  Me.rdb_PrecioPromedio.Size = New System.Drawing.Size(101, 17)
  Me.rdb_PrecioPromedio.TabIndex = 10
  Me.rdb_PrecioPromedio.TabStop = True
  Me.rdb_PrecioPromedio.Text = "Precio promedio"
  Me.rdb_PrecioPromedio.UseVisualStyleBackColor = True
  '
  'rdb_ImporteVentas
  '
  Me.rdb_ImporteVentas.AutoSize = True
  Me.rdb_ImporteVentas.Location = New System.Drawing.Point(201, 5)
  Me.rdb_ImporteVentas.Name = "rdb_ImporteVentas"
  Me.rdb_ImporteVentas.Size = New System.Drawing.Size(96, 17)
  Me.rdb_ImporteVentas.TabIndex = 9
  Me.rdb_ImporteVentas.TabStop = True
  Me.rdb_ImporteVentas.Text = "Importe Ventas"
  Me.rdb_ImporteVentas.UseVisualStyleBackColor = True
  '
  'rdb_Piezas
  '
  Me.rdb_Piezas.AutoSize = True
  Me.rdb_Piezas.Location = New System.Drawing.Point(111, 7)
  Me.rdb_Piezas.Name = "rdb_Piezas"
  Me.rdb_Piezas.Size = New System.Drawing.Size(56, 17)
  Me.rdb_Piezas.TabIndex = 8
  Me.rdb_Piezas.TabStop = True
  Me.rdb_Piezas.Text = "Piezas"
  Me.rdb_Piezas.UseVisualStyleBackColor = True
  '
  'Panel6
  '
  Me.Panel6.Controls.Add(Me.rdb_tuxtla)
  Me.Panel6.Controls.Add(Me.rdb_merida)
  Me.Panel6.Controls.Add(Me.rdb_puebla)
  Me.Panel6.Controls.Add(Me.rdb_todos)
  Me.Panel6.Location = New System.Drawing.Point(93, 8)
  Me.Panel6.Name = "Panel6"
  Me.Panel6.Size = New System.Drawing.Size(435, 31)
  Me.Panel6.TabIndex = 8
  '
  'rdb_tuxtla
  '
  Me.rdb_tuxtla.AutoSize = True
  Me.rdb_tuxtla.Location = New System.Drawing.Point(325, 7)
  Me.rdb_tuxtla.Name = "rdb_tuxtla"
  Me.rdb_tuxtla.Size = New System.Drawing.Size(54, 17)
  Me.rdb_tuxtla.TabIndex = 8
  Me.rdb_tuxtla.TabStop = True
  Me.rdb_tuxtla.Text = "Tuxtla"
  Me.rdb_tuxtla.UseVisualStyleBackColor = True
  '
  'rdb_merida
  '
  Me.rdb_merida.AutoSize = True
  Me.rdb_merida.Location = New System.Drawing.Point(201, 7)
  Me.rdb_merida.Name = "rdb_merida"
  Me.rdb_merida.Size = New System.Drawing.Size(57, 17)
  Me.rdb_merida.TabIndex = 7
  Me.rdb_merida.TabStop = True
  Me.rdb_merida.Text = "Mérida"
  Me.rdb_merida.UseVisualStyleBackColor = True
  '
  'rdb_puebla
  '
  Me.rdb_puebla.AutoSize = True
  Me.rdb_puebla.Location = New System.Drawing.Point(111, 7)
  Me.rdb_puebla.Name = "rdb_puebla"
  Me.rdb_puebla.Size = New System.Drawing.Size(58, 17)
  Me.rdb_puebla.TabIndex = 6
  Me.rdb_puebla.TabStop = True
  Me.rdb_puebla.Text = "Puebla"
  Me.rdb_puebla.UseVisualStyleBackColor = True
  '
  'rdb_todos
  '
  Me.rdb_todos.AutoSize = True
  Me.rdb_todos.Location = New System.Drawing.Point(24, 7)
  Me.rdb_todos.Name = "rdb_todos"
  Me.rdb_todos.Size = New System.Drawing.Size(55, 17)
  Me.rdb_todos.TabIndex = 5
  Me.rdb_todos.TabStop = True
  Me.rdb_todos.Text = "Todos"
  Me.rdb_todos.UseVisualStyleBackColor = True
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(13, 7)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(74, 16)
  Me.Label2.TabIndex = 0
  Me.Label2.Text = "GRAFICO"
  '
  'Panel5
  '
  Me.Panel5.Controls.Add(Me.chart1)
  Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Panel5.Location = New System.Drawing.Point(0, 379)
  Me.Panel5.Name = "Panel5"
  Me.Panel5.Size = New System.Drawing.Size(1155, 507)
  Me.Panel5.TabIndex = 6
  '
  'chart1
  '
  ChartArea1.Name = "ChartArea1"
  Me.chart1.ChartAreas.Add(ChartArea1)
  Me.chart1.Dock = System.Windows.Forms.DockStyle.Fill
  Legend1.Name = "Legend1"
  Me.chart1.Legends.Add(Legend1)
  Me.chart1.Location = New System.Drawing.Point(0, 0)
  Me.chart1.Name = "chart1"
  Series1.ChartArea = "ChartArea1"
  Series1.Legend = "Legend1"
  Series1.Name = "Series1"
  Me.chart1.Series.Add(Series1)
  Me.chart1.Size = New System.Drawing.Size(1155, 507)
  Me.chart1.TabIndex = 0
  Me.chart1.Text = "Chart1"
  '
  'Analisis_de_ventas
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1155, 886)
  Me.Controls.Add(Me.Panel5)
  Me.Controls.Add(Me.Panel4)
  Me.Controls.Add(Me.Panel3)
  Me.Controls.Add(Me.Panel2)
  Me.Controls.Add(Me.Panel1)
  Me.Name = "Analisis_de_ventas"
  Me.Text = "Analisis de ventas"
  Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  Me.panelEspere.ResumeLayout(False)
  Me.Panel2.ResumeLayout(False)
  CType(Me.dgv_Cabecera, System.ComponentModel.ISupportInitialize).EndInit()
  Me.Panel3.ResumeLayout(False)
  CType(Me.dgv_Producto, System.ComponentModel.ISupportInitialize).EndInit()
  Me.Panel4.ResumeLayout(False)
  Me.Panel4.PerformLayout()
  Me.Panel7.ResumeLayout(False)
  Me.Panel7.PerformLayout()
  Me.Panel6.ResumeLayout(False)
  Me.Panel6.PerformLayout()
  Me.Panel5.ResumeLayout(False)
  CType(Me.chart1, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)

 End Sub

 Friend WithEvents Panel1 As Panel
 Friend WithEvents rdb_PorLinea As RadioButton
 Friend WithEvents rdb_PorProducto As RadioButton
 Friend WithEvents CmbArticulo As ComboBox
 Friend WithEvents Label6 As Label
 Friend WithEvents CmbGrupoArticulo As ComboBox
 Friend WithEvents Label4 As Label
 Friend WithEvents Label3 As Label
 Friend WithEvents DtpFechaTer As DateTimePicker
 Friend WithEvents Label5 As Label
 Friend WithEvents DtpFechaIni As DateTimePicker
 Friend WithEvents panelEspere As Panel
 Friend WithEvents Label1 As Label
 Friend WithEvents Button2 As Button
 Friend WithEvents Panel2 As Panel
 Friend WithEvents dgv_Cabecera As DataGridView
 Friend WithEvents Panel3 As Panel
 Friend WithEvents dgv_Producto As DataGridView
 Friend WithEvents Panel4 As Panel
 Friend WithEvents Panel5 As Panel
 Friend WithEvents Label2 As Label
 Friend WithEvents Panel7 As Panel
 Friend WithEvents rdb_PrecioPromedio As RadioButton
 Friend WithEvents rdb_ImporteVentas As RadioButton
 Friend WithEvents rdb_Piezas As RadioButton
 Friend WithEvents Panel6 As Panel
 Friend WithEvents rdb_tuxtla As RadioButton
 Friend WithEvents rdb_merida As RadioButton
 Friend WithEvents rdb_puebla As RadioButton
 Friend WithEvents rdb_todos As RadioButton
 Friend WithEvents chart1 As DataVisualization.Charting.Chart
 Friend WithEvents btnGrafica As Button
 Friend WithEvents cbo_TipoGrafica As ComboBox
 Friend WithEvents btn_Exportar As Button
 Friend WithEvents ckCteProp As CheckBox
End Class
