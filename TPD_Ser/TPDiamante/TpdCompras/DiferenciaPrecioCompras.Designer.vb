<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiferenciaPrecioCompras
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CheckRevisados = New System.Windows.Forms.CheckBox()
        Me.CBFecDoc = New System.Windows.Forms.CheckBox()
        Me.CBFecConta = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.CmbArticulo = New System.Windows.Forms.ComboBox()
        Me.CmbLin = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.bConsultar = New System.Windows.Forms.Button()
        Me.lFchIni = New System.Windows.Forms.Label()
        Me.cmbProveedor = New System.Windows.Forms.ComboBox()
        Me.lCliente = New System.Windows.Forms.Label()
        Me.eFin = New System.Windows.Forms.Label()
        Me.eIni = New System.Windows.Forms.Label()
        Me.lFchFin = New System.Windows.Forms.Label()
        Me.DtpFechaFin = New System.Windows.Forms.DateTimePicker()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.DgDifCompras = New System.Windows.Forms.DataGridView()
        Me.Estatus = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Panel1.SuspendLayout()
        CType(Me.DgDifCompras, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Estatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CheckRevisados)
        Me.Panel1.Controls.Add(Me.CBFecDoc)
        Me.Panel1.Controls.Add(Me.CBFecConta)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.BtnExcel)
        Me.Panel1.Controls.Add(Me.CmbArticulo)
        Me.Panel1.Controls.Add(Me.CmbLin)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.bConsultar)
        Me.Panel1.Controls.Add(Me.lFchIni)
        Me.Panel1.Controls.Add(Me.cmbProveedor)
        Me.Panel1.Controls.Add(Me.lCliente)
        Me.Panel1.Controls.Add(Me.eFin)
        Me.Panel1.Controls.Add(Me.eIni)
        Me.Panel1.Controls.Add(Me.lFchFin)
        Me.Panel1.Controls.Add(Me.DtpFechaFin)
        Me.Panel1.Controls.Add(Me.DtpFechaIni)
        Me.Panel1.Location = New System.Drawing.Point(12, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1060, 105)
        Me.Panel1.TabIndex = 0
        '
        'CheckRevisados
        '
        Me.CheckRevisados.AutoSize = True
        Me.CheckRevisados.Location = New System.Drawing.Point(827, 70)
        Me.CheckRevisados.Name = "CheckRevisados"
        Me.CheckRevisados.Size = New System.Drawing.Size(124, 17)
        Me.CheckRevisados.TabIndex = 132
        Me.CheckRevisados.Text = "Ver Sólo Por Revisar"
        Me.CheckRevisados.UseVisualStyleBackColor = True
        '
        'CBFecDoc
        '
        Me.CBFecDoc.AutoSize = True
        Me.CBFecDoc.Location = New System.Drawing.Point(827, 40)
        Me.CBFecDoc.Name = "CBFecDoc"
        Me.CBFecDoc.Size = New System.Drawing.Size(127, 17)
        Me.CBFecDoc.TabIndex = 131
        Me.CBFecDoc.Text = "Fecha de documento"
        Me.CBFecDoc.UseVisualStyleBackColor = True
        '
        'CBFecConta
        '
        Me.CBFecConta.AutoSize = True
        Me.CBFecConta.Location = New System.Drawing.Point(827, 17)
        Me.CBFecConta.Name = "CBFecConta"
        Me.CBFecConta.Size = New System.Drawing.Size(144, 17)
        Me.CBFecConta.TabIndex = 130
        Me.CBFecConta.Text = "Fecha de contabilización"
        Me.CBFecConta.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(662, 72)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 129
        Me.Button1.Text = "Limpiar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BtnExcel
        '
        Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnExcel.Location = New System.Drawing.Point(759, 70)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(29, 24)
        Me.BtnExcel.TabIndex = 128
        Me.BtnExcel.UseVisualStyleBackColor = True
        Me.BtnExcel.Visible = False
        '
        'CmbArticulo
        '
        Me.CmbArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbArticulo.FormattingEnabled = True
        Me.CmbArticulo.Location = New System.Drawing.Point(521, 41)
        Me.CmbArticulo.Name = "CmbArticulo"
        Me.CmbArticulo.Size = New System.Drawing.Size(265, 21)
        Me.CmbArticulo.TabIndex = 126
        '
        'CmbLin
        '
        Me.CmbLin.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbLin.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbLin.FormattingEnabled = True
        Me.CmbLin.Location = New System.Drawing.Point(107, 42)
        Me.CmbLin.Name = "CmbLin"
        Me.CmbLin.Size = New System.Drawing.Size(265, 21)
        Me.CmbLin.TabIndex = 125
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 124
        Me.Label2.Text = "Grupo de articulos :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(462, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 122
        Me.Label1.Text = "Articulos :"
        '
        'bConsultar
        '
        Me.bConsultar.Location = New System.Drawing.Point(567, 72)
        Me.bConsultar.Name = "bConsultar"
        Me.bConsultar.Size = New System.Drawing.Size(75, 23)
        Me.bConsultar.TabIndex = 6
        Me.bConsultar.Text = "Consultar"
        Me.bConsultar.UseVisualStyleBackColor = True
        '
        'lFchIni
        '
        Me.lFchIni.AutoSize = True
        Me.lFchIni.Location = New System.Drawing.Point(33, 13)
        Me.lFchIni.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lFchIni.Name = "lFchIni"
        Me.lFchIni.Size = New System.Drawing.Size(71, 13)
        Me.lFchIni.TabIndex = 0
        Me.lFchIni.Text = "Fecha Inicia :"
        '
        'cmbProveedor
        '
        Me.cmbProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProveedor.FormattingEnabled = True
        Me.cmbProveedor.Location = New System.Drawing.Point(107, 73)
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Size = New System.Drawing.Size(409, 21)
        Me.cmbProveedor.TabIndex = 5
        '
        'lCliente
        '
        Me.lCliente.AutoSize = True
        Me.lCliente.Location = New System.Drawing.Point(44, 76)
        Me.lCliente.Name = "lCliente"
        Me.lCliente.Size = New System.Drawing.Size(62, 13)
        Me.lCliente.TabIndex = 0
        Me.lCliente.Text = "Proveedor :"
        '
        'eFin
        '
        Me.eFin.AutoSize = True
        Me.eFin.ForeColor = System.Drawing.Color.Red
        Me.eFin.Location = New System.Drawing.Point(783, 8)
        Me.eFin.Name = "eFin"
        Me.eFin.Size = New System.Drawing.Size(11, 13)
        Me.eFin.TabIndex = 121
        Me.eFin.Text = "*"
        Me.eFin.Visible = False
        '
        'eIni
        '
        Me.eIni.AutoSize = True
        Me.eIni.ForeColor = System.Drawing.Color.Red
        Me.eIni.Location = New System.Drawing.Point(376, 9)
        Me.eIni.Name = "eIni"
        Me.eIni.Size = New System.Drawing.Size(11, 13)
        Me.eIni.TabIndex = 120
        Me.eIni.Text = "*"
        Me.eIni.Visible = False
        '
        'lFchFin
        '
        Me.lFchFin.AutoSize = True
        Me.lFchFin.Location = New System.Drawing.Point(451, 12)
        Me.lFchFin.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lFchFin.Name = "lFchFin"
        Me.lFchFin.Size = New System.Drawing.Size(65, 13)
        Me.lFchFin.TabIndex = 0
        Me.lFchFin.Text = "Fecha final :"
        '
        'DtpFechaFin
        '
        Me.DtpFechaFin.Location = New System.Drawing.Point(519, 8)
        Me.DtpFechaFin.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpFechaFin.Name = "DtpFechaFin"
        Me.DtpFechaFin.Size = New System.Drawing.Size(265, 20)
        Me.DtpFechaFin.TabIndex = 2
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Location = New System.Drawing.Point(107, 9)
        Me.DtpFechaIni.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(265, 20)
        Me.DtpFechaIni.TabIndex = 1
        '
        'DgDifCompras
        '
        Me.DgDifCompras.AllowUserToAddRows = False
        Me.DgDifCompras.AllowUserToDeleteRows = False
        Me.DgDifCompras.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgDifCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgDifCompras.Location = New System.Drawing.Point(0, 103)
        Me.DgDifCompras.Name = "DgDifCompras"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgDifCompras.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgDifCompras.Size = New System.Drawing.Size(1278, 442)
        Me.DgDifCompras.TabIndex = 2
        '
        'Estatus
        '
        Me.Estatus.Controls.Add(Me.Label6)
        Me.Estatus.Controls.Add(Me.ProgressBar1)
        Me.Estatus.Location = New System.Drawing.Point(531, 278)
        Me.Estatus.Name = "Estatus"
        Me.Estatus.Size = New System.Drawing.Size(167, 54)
        Me.Estatus.TabIndex = 191
        Me.Estatus.Visible = False
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
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 29)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(140, 17)
        Me.ProgressBar1.TabIndex = 112
        '
        'DiferenciaPrecioCompras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1281, 557)
        Me.Controls.Add(Me.Estatus)
        Me.Controls.Add(Me.DgDifCompras)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "DiferenciaPrecioCompras"
        Me.Text = "Diferencias de precio de compras"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DgDifCompras, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Estatus.ResumeLayout(False)
        Me.Estatus.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents eFin As System.Windows.Forms.Label
    Friend WithEvents eIni As System.Windows.Forms.Label
    Friend WithEvents lFchFin As System.Windows.Forms.Label
    Friend WithEvents DtpFechaFin As System.Windows.Forms.DateTimePicker
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents bConsultar As System.Windows.Forms.Button
    Friend WithEvents lFchIni As System.Windows.Forms.Label
    Friend WithEvents cmbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents lCliente As System.Windows.Forms.Label
    Friend WithEvents CmbLin As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DgDifCompras As System.Windows.Forms.DataGridView
    Friend WithEvents CBFecDoc As System.Windows.Forms.CheckBox
    Friend WithEvents CBFecConta As System.Windows.Forms.CheckBox
    Friend WithEvents CheckRevisados As System.Windows.Forms.CheckBox
    Friend WithEvents Estatus As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
End Class
