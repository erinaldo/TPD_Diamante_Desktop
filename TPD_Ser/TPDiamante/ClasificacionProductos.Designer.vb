<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClasificacionProductosPorVentas
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
  Me.dtpFinal = New System.Windows.Forms.DateTimePicker()
  Me.dtpInicio = New System.Windows.Forms.DateTimePicker()
  Me.GroupBox1 = New System.Windows.Forms.GroupBox()
  Me.chk80_20 = New System.Windows.Forms.CheckBox()
  Me.Button2 = New System.Windows.Forms.Button()
  Me.Button1 = New System.Windows.Forms.Button()
  Me.dgvClasificacion = New System.Windows.Forms.DataGridView()
  Me.cbAlmacen = New System.Windows.Forms.ComboBox()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.cmbLinea = New System.Windows.Forms.ComboBox()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.GroupBox1.SuspendLayout()
  CType(Me.dgvClasificacion, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'dtpFinal
  '
  Me.dtpFinal.Location = New System.Drawing.Point(556, 49)
  Me.dtpFinal.Name = "dtpFinal"
  Me.dtpFinal.Size = New System.Drawing.Size(306, 23)
  Me.dtpFinal.TabIndex = 0
  '
  'dtpInicio
  '
  Me.dtpInicio.Location = New System.Drawing.Point(109, 48)
  Me.dtpInicio.Name = "dtpInicio"
  Me.dtpInicio.Size = New System.Drawing.Size(314, 23)
  Me.dtpInicio.TabIndex = 1
  '
  'GroupBox1
  '
  Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
  Me.GroupBox1.BackColor = System.Drawing.Color.White
  Me.GroupBox1.Controls.Add(Me.chk80_20)
  Me.GroupBox1.Controls.Add(Me.Button2)
  Me.GroupBox1.Controls.Add(Me.Button1)
  Me.GroupBox1.Controls.Add(Me.dgvClasificacion)
  Me.GroupBox1.Controls.Add(Me.cbAlmacen)
  Me.GroupBox1.Controls.Add(Me.Label4)
  Me.GroupBox1.Controls.Add(Me.Label3)
  Me.GroupBox1.Controls.Add(Me.cmbLinea)
  Me.GroupBox1.Controls.Add(Me.Label2)
  Me.GroupBox1.Controls.Add(Me.Label1)
  Me.GroupBox1.Controls.Add(Me.dtpInicio)
  Me.GroupBox1.Controls.Add(Me.dtpFinal)
  Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
  Me.GroupBox1.Location = New System.Drawing.Point(13, 13)
  Me.GroupBox1.Name = "GroupBox1"
  Me.GroupBox1.Size = New System.Drawing.Size(1210, 592)
  Me.GroupBox1.TabIndex = 2
  Me.GroupBox1.TabStop = False
  Me.GroupBox1.Text = "Clasificación"
  '
  'chk80_20
  '
  Me.chk80_20.AutoSize = True
  Me.chk80_20.Location = New System.Drawing.Point(894, 49)
  Me.chk80_20.Name = "chk80_20"
  Me.chk80_20.Size = New System.Drawing.Size(171, 21)
  Me.chk80_20.TabIndex = 11
  Me.chk80_20.Text = "Filtrar productos 80-20"
  Me.chk80_20.UseVisualStyleBackColor = True
  '
  'Button2
  '
  Me.Button2.BackgroundImage = Global.TPDiamante.My.Resources.Resources.Excel
  Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
  Me.Button2.Location = New System.Drawing.Point(1011, 75)
  Me.Button2.Name = "Button2"
  Me.Button2.Size = New System.Drawing.Size(100, 47)
  Me.Button2.TabIndex = 10
  Me.Button2.UseVisualStyleBackColor = True
  '
  'Button1
  '
  Me.Button1.Location = New System.Drawing.Point(894, 75)
  Me.Button1.Name = "Button1"
  Me.Button1.Size = New System.Drawing.Size(100, 46)
  Me.Button1.TabIndex = 9
  Me.Button1.Text = "Consultar"
  Me.Button1.UseVisualStyleBackColor = True
  '
  'dgvClasificacion
  '
  Me.dgvClasificacion.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
  Me.dgvClasificacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
  Me.dgvClasificacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgvClasificacion.Location = New System.Drawing.Point(19, 142)
  Me.dgvClasificacion.Name = "dgvClasificacion"
  Me.dgvClasificacion.Size = New System.Drawing.Size(1169, 444)
  Me.dgvClasificacion.TabIndex = 8
  '
  'cbAlmacen
  '
  Me.cbAlmacen.FormattingEnabled = True
  Me.cbAlmacen.Location = New System.Drawing.Point(556, 92)
  Me.cbAlmacen.Name = "cbAlmacen"
  Me.cbAlmacen.Size = New System.Drawing.Size(306, 24)
  Me.cbAlmacen.TabIndex = 7
  '
  'Label4
  '
  Me.Label4.AutoSize = True
  Me.Label4.Location = New System.Drawing.Point(450, 98)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(62, 17)
  Me.Label4.TabIndex = 6
  Me.Label4.Text = "Almacén"
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Location = New System.Drawing.Point(22, 101)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(43, 17)
  Me.Label3.TabIndex = 5
  Me.Label3.Text = "Línea"
  '
  'cmbLinea
  '
  Me.cmbLinea.FormattingEnabled = True
  Me.cmbLinea.Location = New System.Drawing.Point(109, 98)
  Me.cmbLinea.Name = "cmbLinea"
  Me.cmbLinea.Size = New System.Drawing.Size(314, 24)
  Me.cmbLinea.TabIndex = 4
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Location = New System.Drawing.Point(450, 49)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(49, 17)
  Me.Label2.TabIndex = 3
  Me.Label2.Text = "Hasta:"
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Location = New System.Drawing.Point(16, 48)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(53, 17)
  Me.Label1.TabIndex = 2
  Me.Label1.Text = "Desde:"
  '
  'ClasificacionProductosPorVentas
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.BackColor = System.Drawing.Color.White
  Me.ClientSize = New System.Drawing.Size(1235, 627)
  Me.Controls.Add(Me.GroupBox1)
  Me.Name = "ClasificacionProductosPorVentas"
  Me.Text = "Clasificaciòn de Productos por ventas"
  Me.GroupBox1.ResumeLayout(False)
  Me.GroupBox1.PerformLayout()
  CType(Me.dgvClasificacion, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)

 End Sub

 Friend WithEvents dtpFinal As DateTimePicker
    Friend WithEvents dtpInicio As DateTimePicker
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dgvClasificacion As DataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbLinea As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents cbAlmacen As ComboBox
  Friend WithEvents chk80_20 As CheckBox
End Class
