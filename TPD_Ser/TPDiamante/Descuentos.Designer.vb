<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Descuentos
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
        Me.Serie = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmbSucursal = New System.Windows.Forms.ComboBox()
        Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.al = New System.Windows.Forms.Label()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DGFacturas = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.DGDetalle = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Estatus = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DGFacturas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DGDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Estatus.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Serie
        '
        Me.Serie.AutoSize = True
        Me.Serie.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
        Me.Serie.Location = New System.Drawing.Point(9, 67)
        Me.Serie.Name = "Serie"
        Me.Serie.Size = New System.Drawing.Size(36, 15)
        Me.Serie.TabIndex = 136
        Me.Serie.Text = "Serie"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
        Me.Label2.Location = New System.Drawing.Point(204, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 15)
        Me.Label2.TabIndex = 137
        Me.Label2.Text = "Agente de vtas."
        '
        'CmbSucursal
        '
        Me.CmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbSucursal.FormattingEnabled = True
        Me.CmbSucursal.Location = New System.Drawing.Point(12, 85)
        Me.CmbSucursal.Name = "CmbSucursal"
        Me.CmbSucursal.Size = New System.Drawing.Size(151, 21)
        Me.CmbSucursal.TabIndex = 134
        '
        'CmbAgteVta
        '
        Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbAgteVta.FormattingEnabled = True
        Me.CmbAgteVta.Location = New System.Drawing.Point(204, 85)
        Me.CmbAgteVta.Name = "CmbAgteVta"
        Me.CmbAgteVta.Size = New System.Drawing.Size(151, 21)
        Me.CmbAgteVta.TabIndex = 135
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(146, 17)
        Me.Label5.TabIndex = 140
        Me.Label5.Text = "Periodo de ventas del"
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaIni.Location = New System.Drawing.Point(15, 32)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(259, 23)
        Me.DtpFechaIni.TabIndex = 138
        Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'al
        '
        Me.al.AutoSize = True
        Me.al.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.al.Location = New System.Drawing.Point(301, 12)
        Me.al.Name = "al"
        Me.al.Size = New System.Drawing.Size(19, 17)
        Me.al.TabIndex = 141
        Me.al.Text = "al"
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaTer.Location = New System.Drawing.Point(295, 32)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(257, 23)
        Me.DtpFechaTer.TabIndex = 139
        Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Estatus)
        Me.GroupBox1.Controls.Add(Me.DGFacturas)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 119)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(924, 228)
        Me.GroupBox1.TabIndex = 142
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Facturas"
        '
        'DGFacturas
        '
        Me.DGFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGFacturas.Location = New System.Drawing.Point(6, 17)
        Me.DGFacturas.Name = "DGFacturas"
        Me.DGFacturas.Size = New System.Drawing.Size(912, 205)
        Me.DGFacturas.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.DGDetalle)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 364)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(924, 228)
        Me.GroupBox2.TabIndex = 143
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detalle"
        '
        'DGDetalle
        '
        Me.DGDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGDetalle.Location = New System.Drawing.Point(6, 16)
        Me.DGDetalle.Name = "DGDetalle"
        Me.DGDetalle.Size = New System.Drawing.Size(912, 205)
        Me.DGDetalle.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.AliceBlue
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button2.Location = New System.Drawing.Point(424, 75)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 36)
        Me.Button2.TabIndex = 144
        Me.Button2.Text = "Consultar"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'BtnExcel
        '
        Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnExcel.Location = New System.Drawing.Point(791, 75)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(43, 43)
        Me.BtnExcel.TabIndex = 151
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button1.Location = New System.Drawing.Point(854, 75)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(43, 43)
        Me.Button1.TabIndex = 152
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(788, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 153
        Me.Label1.Text = "Facturas"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(851, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 154
        Me.Label3.Text = "Detalle"
        '
        'Estatus
        '
        Me.Estatus.Controls.Add(Me.Label4)
        Me.Estatus.Controls.Add(Me.ProgressBar1)
        Me.Estatus.Location = New System.Drawing.Point(373, 98)
        Me.Estatus.Name = "Estatus"
        Me.Estatus.Size = New System.Drawing.Size(167, 54)
        Me.Estatus.TabIndex = 188
        Me.Estatus.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(39, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 13)
        Me.Label4.TabIndex = 113
        Me.Label4.Text = "Cargando archivo"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 29)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(140, 17)
        Me.ProgressBar1.TabIndex = 112
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.ProgressBar2)
        Me.Panel1.Location = New System.Drawing.Point(373, 82)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(167, 54)
        Me.Panel1.TabIndex = 189
        Me.Panel1.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(39, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(91, 13)
        Me.Label6.TabIndex = 113
        Me.Label6.Text = "Cargando archivo"
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Location = New System.Drawing.Point(12, 29)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(140, 17)
        Me.ProgressBar2.TabIndex = 112
        '
        'Descuentos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(948, 604)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DtpFechaIni)
        Me.Controls.Add(Me.al)
        Me.Controls.Add(Me.DtpFechaTer)
        Me.Controls.Add(Me.Serie)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CmbSucursal)
        Me.Controls.Add(Me.CmbAgteVta)
        Me.Name = "Descuentos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Descuentos"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DGFacturas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DGDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Estatus.ResumeLayout(False)
        Me.Estatus.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Serie As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents al As System.Windows.Forms.Label
    Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents DGFacturas As System.Windows.Forms.DataGridView
    Friend WithEvents DGDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Estatus As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar2 As System.Windows.Forms.ProgressBar
End Class
