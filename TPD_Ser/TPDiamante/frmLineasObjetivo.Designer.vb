<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLineasObjetivo
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.dgvTotales = New System.Windows.Forms.DataGridView()
        Me.dgvVendedores = New System.Windows.Forms.DataGridView()
        Me.dgvLineasObj = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbSucursal = New System.Windows.Forms.ComboBox()
        Me.cmbAgteVta = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAvanceOptimo = New System.Windows.Forms.TextBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.txtDiasRestantes = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDiasTranscurridos = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDiasMes = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgvTotales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvVendedores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvLineasObj, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmbSucursal)
        Me.Panel1.Controls.Add(Me.cmbAgteVta)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.dtpFechaIni)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtAvanceOptimo)
        Me.Panel1.Controls.Add(Me.label9)
        Me.Panel1.Controls.Add(Me.txtDiasRestantes)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtDiasTranscurridos)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtDiasMes)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(214, 557)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(214, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1137, 557)
        Me.Panel2.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvTotales)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1133, 110)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Totales"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvVendedores)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 110)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1133, 147)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Vendedores"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.dgvLineasObj)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 257)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1133, 296)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Lineas Objetivo"
        '
        'dgvTotales
        '
        Me.dgvTotales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTotales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTotales.Location = New System.Drawing.Point(3, 16)
        Me.dgvTotales.Name = "dgvTotales"
        Me.dgvTotales.Size = New System.Drawing.Size(1127, 91)
        Me.dgvTotales.TabIndex = 0
        '
        'dgvVendedores
        '
        Me.dgvVendedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvVendedores.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvVendedores.Location = New System.Drawing.Point(3, 16)
        Me.dgvVendedores.Name = "dgvVendedores"
        Me.dgvVendedores.Size = New System.Drawing.Size(1127, 128)
        Me.dgvVendedores.TabIndex = 1
        '
        'dgvLineasObj
        '
        Me.dgvLineasObj.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLineasObj.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLineasObj.Location = New System.Drawing.Point(3, 16)
        Me.dgvLineasObj.Name = "dgvLineasObj"
        Me.dgvLineasObj.Size = New System.Drawing.Size(1127, 277)
        Me.dgvLineasObj.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 170)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 159
        Me.Label3.Text = "Sucursal"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 221)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 160
        Me.Label2.Text = "Agente de vtas."
        '
        'cmbSucursal
        '
        Me.cmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSucursal.FormattingEnabled = True
        Me.cmbSucursal.Location = New System.Drawing.Point(14, 186)
        Me.cmbSucursal.Name = "cmbSucursal"
        Me.cmbSucursal.Size = New System.Drawing.Size(186, 21)
        Me.cmbSucursal.TabIndex = 156
        '
        'cmbAgteVta
        '
        Me.cmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAgteVta.FormattingEnabled = True
        Me.cmbAgteVta.Items.AddRange(New Object() {"Todos"})
        Me.cmbAgteVta.Location = New System.Drawing.Point(14, 237)
        Me.cmbAgteVta.Name = "cmbAgteVta"
        Me.cmbAgteVta.Size = New System.Drawing.Size(186, 21)
        Me.cmbAgteVta.TabIndex = 157
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(11, 118)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 13)
        Me.Label5.TabIndex = 158
        Me.Label5.Text = "Fecha Final"
        '
        'dtpFechaIni
        '
        Me.dtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaIni.Location = New System.Drawing.Point(14, 136)
        Me.dtpFechaIni.Name = "dtpFechaIni"
        Me.dtpFechaIni.Size = New System.Drawing.Size(185, 23)
        Me.dtpFechaIni.TabIndex = 155
        Me.dtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 79)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 154
        Me.Label4.Text = "Avance Optimo"
        '
        'txtAvanceOptimo
        '
        Me.txtAvanceOptimo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAvanceOptimo.Location = New System.Drawing.Point(134, 76)
        Me.txtAvanceOptimo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAvanceOptimo.Name = "txtAvanceOptimo"
        Me.txtAvanceOptimo.ReadOnly = True
        Me.txtAvanceOptimo.Size = New System.Drawing.Size(65, 20)
        Me.txtAvanceOptimo.TabIndex = 153
        Me.txtAvanceOptimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label9.Location = New System.Drawing.Point(11, 58)
        Me.label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(95, 13)
        Me.label9.TabIndex = 152
        Me.label9.Text = "Días Restantes"
        '
        'txtDiasRestantes
        '
        Me.txtDiasRestantes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiasRestantes.Location = New System.Drawing.Point(134, 55)
        Me.txtDiasRestantes.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDiasRestantes.Name = "txtDiasRestantes"
        Me.txtDiasRestantes.ReadOnly = True
        Me.txtDiasRestantes.Size = New System.Drawing.Size(65, 20)
        Me.txtDiasRestantes.TabIndex = 151
        Me.txtDiasRestantes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 37)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(115, 13)
        Me.Label6.TabIndex = 150
        Me.Label6.Text = "Días Transcurridos"
        '
        'txtDiasTranscurridos
        '
        Me.txtDiasTranscurridos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiasTranscurridos.Location = New System.Drawing.Point(134, 34)
        Me.txtDiasTranscurridos.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDiasTranscurridos.Name = "txtDiasTranscurridos"
        Me.txtDiasTranscurridos.ReadOnly = True
        Me.txtDiasTranscurridos.Size = New System.Drawing.Size(65, 20)
        Me.txtDiasTranscurridos.TabIndex = 148
        Me.txtDiasTranscurridos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(11, 16)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.TabIndex = 149
        Me.Label7.Text = "Días Mes"
        '
        'txtDiasMes
        '
        Me.txtDiasMes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiasMes.Location = New System.Drawing.Point(134, 13)
        Me.txtDiasMes.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDiasMes.Name = "txtDiasMes"
        Me.txtDiasMes.ReadOnly = True
        Me.txtDiasMes.Size = New System.Drawing.Size(65, 20)
        Me.txtDiasMes.TabIndex = 147
        Me.txtDiasMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmLineasObjetivo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1351, 557)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmLineasObjetivo"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScoreCard Lineas Objetivo"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.dgvTotales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvVendedores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvLineasObj, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents dgvLineasObj As DataGridView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents dgvVendedores As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dgvTotales As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbSucursal As ComboBox
    Friend WithEvents cmbAgteVta As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpFechaIni As DateTimePicker
    Private WithEvents Label4 As Label
    Private WithEvents txtAvanceOptimo As TextBox
    Private WithEvents label9 As Label
    Private WithEvents txtDiasRestantes As TextBox
    Private WithEvents Label6 As Label
    Private WithEvents txtDiasTranscurridos As TextBox
    Private WithEvents Label7 As Label
    Private WithEvents txtDiasMes As TextBox
End Class
