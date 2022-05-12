<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmvalor_inventario
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmvalor_inventario))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpfecha = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnlimpiar = New System.Windows.Forms.Button()
        Me.chbtodas_lineas = New System.Windows.Forms.CheckBox()
        Me.dgvres = New System.Windows.Forms.DataGridView()
        Me.Lineas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Valor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvlineas = New System.Windows.Forms.DataGridView()
        Me.line = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnexportar = New System.Windows.Forms.Button()
        Me.btnconsultar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnquitar_linea = New System.Windows.Forms.Button()
        Me.btnagregar_linea = New System.Windows.Forms.Button()
        Me.cmblineas = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvres, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvlineas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(243, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(212, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Valor del Inventario por Linea"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Fecha de validación:"
        '
        'dtpfecha
        '
        Me.dtpfecha.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpfecha.Location = New System.Drawing.Point(138, 80)
        Me.dtpfecha.Name = "dtpfecha"
        Me.dtpfecha.Size = New System.Drawing.Size(200, 20)
        Me.dtpfecha.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.btnlimpiar)
        Me.Panel1.Controls.Add(Me.chbtodas_lineas)
        Me.Panel1.Controls.Add(Me.dgvres)
        Me.Panel1.Controls.Add(Me.dgvlineas)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.btnexportar)
        Me.Panel1.Controls.Add(Me.btnconsultar)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.btnquitar_linea)
        Me.Panel1.Controls.Add(Me.btnagregar_linea)
        Me.Panel1.Controls.Add(Me.cmblineas)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(15, 123)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(681, 492)
        Me.Panel1.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(349, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(317, 32)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "* Esta consulta incluye todos los almacenes a excepción del almacen 02 de Garantí" & _
    "as."
        '
        'btnlimpiar
        '
        Me.btnlimpiar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnlimpiar.Image = Global.TPDiamante.My.Resources.Resources.kate2
        Me.btnlimpiar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnlimpiar.Location = New System.Drawing.Point(271, 274)
        Me.btnlimpiar.Name = "btnlimpiar"
        Me.btnlimpiar.Size = New System.Drawing.Size(75, 56)
        Me.btnlimpiar.TabIndex = 10
        Me.btnlimpiar.Text = "Limpiar"
        Me.btnlimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnlimpiar.UseVisualStyleBackColor = True
        '
        'chbtodas_lineas
        '
        Me.chbtodas_lineas.AutoSize = True
        Me.chbtodas_lineas.Location = New System.Drawing.Point(62, 24)
        Me.chbtodas_lineas.Name = "chbtodas_lineas"
        Me.chbtodas_lineas.Size = New System.Drawing.Size(102, 17)
        Me.chbtodas_lineas.TabIndex = 1
        Me.chbtodas_lineas.Text = "Todas las lineas"
        Me.chbtodas_lineas.UseVisualStyleBackColor = True
        Me.chbtodas_lineas.Visible = False
        '
        'dgvres
        '
        Me.dgvres.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvres.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Lineas, Me.Valor})
        Me.dgvres.Location = New System.Drawing.Point(352, 127)
        Me.dgvres.Name = "dgvres"
        Me.dgvres.RowHeadersWidth = 20
        Me.dgvres.Size = New System.Drawing.Size(314, 353)
        Me.dgvres.TabIndex = 9
        '
        'Lineas
        '
        Me.Lineas.HeaderText = "Lineas"
        Me.Lineas.Name = "Lineas"
        '
        'Valor
        '
        DataGridViewCellStyle1.Format = "C2"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.Valor.DefaultCellStyle = DataGridViewCellStyle1
        Me.Valor.HeaderText = "$ Valor"
        Me.Valor.Name = "Valor"
        '
        'dgvlineas
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvlineas.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvlineas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvlineas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.line})
        Me.dgvlineas.Location = New System.Drawing.Point(55, 127)
        Me.dgvlineas.Name = "dgvlineas"
        Me.dgvlineas.RowHeadersWidth = 20
        Me.dgvlineas.Size = New System.Drawing.Size(210, 353)
        Me.dgvlineas.TabIndex = 8
        '
        'line
        '
        Me.line.HeaderText = "Lineas"
        Me.line.Name = "line"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(377, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(145, 15)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Resultado de la consulta."
        '
        'btnexportar
        '
        Me.btnexportar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexportar.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.btnexportar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnexportar.Location = New System.Drawing.Point(271, 199)
        Me.btnexportar.Name = "btnexportar"
        Me.btnexportar.Size = New System.Drawing.Size(75, 56)
        Me.btnexportar.TabIndex = 6
        Me.btnexportar.Text = "Exportar"
        Me.btnexportar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnexportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnexportar, "Exportar resultados a Excel")
        Me.btnexportar.UseVisualStyleBackColor = True
        '
        'btnconsultar
        '
        Me.btnconsultar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnconsultar.Image = Global.TPDiamante.My.Resources.Resources.file_find
        Me.btnconsultar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnconsultar.Location = New System.Drawing.Point(271, 127)
        Me.btnconsultar.Name = "btnconsultar"
        Me.btnconsultar.Size = New System.Drawing.Size(75, 56)
        Me.btnconsultar.TabIndex = 5
        Me.btnconsultar.Text = "Consultar"
        Me.btnconsultar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnconsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnconsultar, "Consultar filtros de lineas")
        Me.btnconsultar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(17, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(252, 15)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Lineas para filtro de validación del Inventario."
        '
        'btnquitar_linea
        '
        Me.btnquitar_linea.BackgroundImage = Global.TPDiamante.My.Resources.Resources.Minus__6_
        Me.btnquitar_linea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnquitar_linea.Location = New System.Drawing.Point(18, 127)
        Me.btnquitar_linea.Name = "btnquitar_linea"
        Me.btnquitar_linea.Size = New System.Drawing.Size(31, 29)
        Me.btnquitar_linea.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnquitar_linea, "Quitar línea del filtro")
        Me.btnquitar_linea.UseVisualStyleBackColor = True
        '
        'btnagregar_linea
        '
        Me.btnagregar_linea.BackgroundImage = Global.TPDiamante.My.Resources.Resources.Plus__6_
        Me.btnagregar_linea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnagregar_linea.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnagregar_linea.Location = New System.Drawing.Point(251, 44)
        Me.btnagregar_linea.Name = "btnagregar_linea"
        Me.btnagregar_linea.Size = New System.Drawing.Size(31, 29)
        Me.btnagregar_linea.TabIndex = 3
        Me.btnagregar_linea.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnagregar_linea, "Agregar linea para filtro.")
        Me.btnagregar_linea.UseVisualStyleBackColor = True
        '
        'cmblineas
        '
        Me.cmblineas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmblineas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cmblineas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmblineas.FormattingEnabled = True
        Me.cmblineas.Location = New System.Drawing.Point(62, 47)
        Me.cmblineas.Name = "cmblineas"
        Me.cmblineas.Size = New System.Drawing.Size(183, 23)
        Me.cmblineas.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 15)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Linea:"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Lineas"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "$Valor"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Lineas"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'frmvalor_inventario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(708, 627)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.dtpfecha)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmvalor_inventario"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Valor de Inventario por Linea"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvres, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvlineas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpfecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnconsultar As System.Windows.Forms.Button
    Friend WithEvents btnexportar As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmblineas As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnquitar_linea As System.Windows.Forms.Button
    Friend WithEvents btnagregar_linea As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvlineas As System.Windows.Forms.DataGridView
    Friend WithEvents dgvres As System.Windows.Forms.DataGridView
    Friend WithEvents line As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Lineas As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Valor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chbtodas_lineas As System.Windows.Forms.CheckBox
    Friend WithEvents btnlimpiar As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
