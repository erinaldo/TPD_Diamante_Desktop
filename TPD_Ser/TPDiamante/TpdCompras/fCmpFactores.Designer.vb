<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fCmpFactores
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
        Me.lGrpArticulos = New System.Windows.Forms.Label()
        Me.cmbLinea = New System.Windows.Forms.ComboBox()
        Me.bConsultar = New System.Windows.Forms.Button()
        Me.DgRptMargen = New System.Windows.Forms.DataGridView()
        Me.bExcel = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Estatus = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbProveedor = New System.Windows.Forms.ComboBox()
        Me.rbProveedor = New System.Windows.Forms.RadioButton()
        Me.rbLinea = New System.Windows.Forms.RadioButton()
        Me.cmbLineas2 = New System.Windows.Forms.ComboBox()
        Me.cmbProveedor2 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.DgRptMargen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Estatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'lGrpArticulos
        '
        Me.lGrpArticulos.AutoSize = True
        Me.lGrpArticulos.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lGrpArticulos.Location = New System.Drawing.Point(436, 38)
        Me.lGrpArticulos.Name = "lGrpArticulos"
        Me.lGrpArticulos.Size = New System.Drawing.Size(43, 14)
        Me.lGrpArticulos.TabIndex = 0
        Me.lGrpArticulos.Text = "Línea:"
        '
        'cmbLinea
        '
        Me.cmbLinea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbLinea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLinea.FormattingEnabled = True
        Me.cmbLinea.Location = New System.Drawing.Point(543, 36)
        Me.cmbLinea.Name = "cmbLinea"
        Me.cmbLinea.Size = New System.Drawing.Size(250, 21)
        Me.cmbLinea.TabIndex = 1
        '
        'bConsultar
        '
        Me.bConsultar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bConsultar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.bConsultar.Location = New System.Drawing.Point(848, 34)
        Me.bConsultar.Name = "bConsultar"
        Me.bConsultar.Size = New System.Drawing.Size(97, 23)
        Me.bConsultar.TabIndex = 2
        Me.bConsultar.Text = "Consultar"
        Me.bConsultar.UseVisualStyleBackColor = True
        '
        'DgRptMargen
        '
        Me.DgRptMargen.AllowUserToAddRows = False
        Me.DgRptMargen.AllowUserToDeleteRows = False
        Me.DgRptMargen.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgRptMargen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgRptMargen.Location = New System.Drawing.Point(3, 104)
        Me.DgRptMargen.Name = "DgRptMargen"
        Me.DgRptMargen.ReadOnly = True
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgRptMargen.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgRptMargen.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgRptMargen.Size = New System.Drawing.Size(1379, 537)
        Me.DgRptMargen.TabIndex = 110
        '
        'bExcel
        '
        Me.bExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.bExcel.Location = New System.Drawing.Point(848, 69)
        Me.bExcel.Name = "bExcel"
        Me.bExcel.Size = New System.Drawing.Size(38, 31)
        Me.bExcel.TabIndex = 111
        Me.bExcel.UseVisualStyleBackColor = True
        Me.bExcel.Visible = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 29)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(140, 17)
        Me.ProgressBar1.TabIndex = 112
        '
        'Estatus
        '
        Me.Estatus.Controls.Add(Me.Label1)
        Me.Estatus.Controls.Add(Me.ProgressBar1)
        Me.Estatus.Location = New System.Drawing.Point(543, 192)
        Me.Estatus.Name = "Estatus"
        Me.Estatus.Size = New System.Drawing.Size(167, 54)
        Me.Estatus.TabIndex = 113
        Me.Estatus.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(39, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 113
        Me.Label1.Text = "Cargando archivo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(39, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 14)
        Me.Label2.TabIndex = 114
        Me.Label2.Text = "Consultar por:"
        '
        'cmbProveedor
        '
        Me.cmbProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProveedor.FormattingEnabled = True
        Me.cmbProveedor.Location = New System.Drawing.Point(150, 38)
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Size = New System.Drawing.Size(250, 21)
        Me.cmbProveedor.TabIndex = 118
        '
        'rbProveedor
        '
        Me.rbProveedor.AutoSize = True
        Me.rbProveedor.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbProveedor.Location = New System.Drawing.Point(42, 39)
        Me.rbProveedor.Name = "rbProveedor"
        Me.rbProveedor.Size = New System.Drawing.Size(88, 18)
        Me.rbProveedor.TabIndex = 119
        Me.rbProveedor.TabStop = True
        Me.rbProveedor.Text = "Proveedor"
        Me.rbProveedor.UseVisualStyleBackColor = True
        '
        'rbLinea
        '
        Me.rbLinea.AutoSize = True
        Me.rbLinea.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbLinea.Location = New System.Drawing.Point(42, 75)
        Me.rbLinea.Name = "rbLinea"
        Me.rbLinea.Size = New System.Drawing.Size(57, 18)
        Me.rbLinea.TabIndex = 120
        Me.rbLinea.TabStop = True
        Me.rbLinea.Text = "Línea"
        Me.rbLinea.UseVisualStyleBackColor = True
        '
        'cmbLineas2
        '
        Me.cmbLineas2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbLineas2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLineas2.FormattingEnabled = True
        Me.cmbLineas2.Location = New System.Drawing.Point(150, 77)
        Me.cmbLineas2.Name = "cmbLineas2"
        Me.cmbLineas2.Size = New System.Drawing.Size(250, 21)
        Me.cmbLineas2.TabIndex = 121
        '
        'cmbProveedor2
        '
        Me.cmbProveedor2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbProveedor2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProveedor2.FormattingEnabled = True
        Me.cmbProveedor2.Location = New System.Drawing.Point(543, 77)
        Me.cmbProveedor2.Name = "cmbProveedor2"
        Me.cmbProveedor2.Size = New System.Drawing.Size(250, 21)
        Me.cmbProveedor2.TabIndex = 122
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(436, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 14)
        Me.Label5.TabIndex = 124
        Me.Label5.Text = "Proveedor:"
        '
        'fCmpFactores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1384, 644)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbProveedor2)
        Me.Controls.Add(Me.cmbLineas2)
        Me.Controls.Add(Me.rbLinea)
        Me.Controls.Add(Me.rbProveedor)
        Me.Controls.Add(Me.cmbProveedor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Estatus)
        Me.Controls.Add(Me.bExcel)
        Me.Controls.Add(Me.DgRptMargen)
        Me.Controls.Add(Me.bConsultar)
        Me.Controls.Add(Me.cmbLinea)
        Me.Controls.Add(Me.lGrpArticulos)
        Me.Name = "fCmpFactores"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Factores"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DgRptMargen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Estatus.ResumeLayout(False)
        Me.Estatus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lGrpArticulos As System.Windows.Forms.Label
    Friend WithEvents cmbLinea As System.Windows.Forms.ComboBox
    Friend WithEvents bConsultar As System.Windows.Forms.Button
    Friend WithEvents DgRptMargen As System.Windows.Forms.DataGridView
    Friend WithEvents bExcel As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Estatus As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbProveedor As ComboBox
    Friend WithEvents rbProveedor As RadioButton
    Friend WithEvents rbLinea As RadioButton
    Friend WithEvents cmbLineas2 As ComboBox
    Friend WithEvents cmbProveedor2 As ComboBox
    Friend WithEvents Label5 As Label
End Class
