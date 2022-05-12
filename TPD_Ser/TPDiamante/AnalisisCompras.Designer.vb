<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AnalisisCompras
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
    Me.Label5 = New System.Windows.Forms.Label()
    Me.pEncabezado = New System.Windows.Forms.Panel()
    Me.chkAlternos = New System.Windows.Forms.CheckBox()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.cmbproveedor = New System.Windows.Forms.ComboBox()
    Me.cmblinea = New System.Windows.Forms.ComboBox()
    Me.lblp = New System.Windows.Forms.Label()
    Me.lbllinea = New System.Windows.Forms.Label()
    Me.lblapuntador2 = New System.Windows.Forms.Label()
    Me.lblapuntador = New System.Windows.Forms.Label()
    Me.cbl = New System.Windows.Forms.CheckBox()
    Me.cbp = New System.Windows.Forms.CheckBox()
    Me.CmbLinIni = New System.Windows.Forms.ComboBox()
    Me.lbllinea1 = New System.Windows.Forms.Label()
    Me.CBProveedor = New System.Windows.Forms.ComboBox()
    Me.lblp1 = New System.Windows.Forms.Label()
    Me.eFin = New System.Windows.Forms.Label()
    Me.BtnExcel = New System.Windows.Forms.Button()
    Me.BtnConsultar = New System.Windows.Forms.Button()
    Me.ckConsolidado = New System.Windows.Forms.CheckBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.eIni = New System.Windows.Forms.Label()
    Me.eLin = New System.Windows.Forms.Label()
    Me.eInv = New System.Windows.Forms.Label()
    Me.eAlm = New System.Windows.Forms.Label()
    Me.LblFechaIni = New System.Windows.Forms.Label()
    Me.LblAlmacen = New System.Windows.Forms.Label()
    Me.TBDiasInv = New System.Windows.Forms.TextBox()
    Me.DtpFechaFin = New System.Windows.Forms.DateTimePicker()
    Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
    Me.CBAlmacen = New System.Windows.Forms.ComboBox()
    Me.DGAnalisisCompras = New System.Windows.Forms.DataGridView()
    Me.Estatus = New System.Windows.Forms.Panel()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.CKVtasMes = New System.Windows.Forms.CheckBox()
    Me.PBExportacion = New System.Windows.Forms.ProgressBar()
    Me.pEncabezado.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    CType(Me.DGAnalisisCompras, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Estatus.SuspendLayout()
    Me.SuspendLayout()
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.Location = New System.Drawing.Point(23, 21)
    Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(136, 15)
    Me.Label5.TabIndex = 117
    Me.Label5.Text = "Análisis de compras"
    '
    'pEncabezado
    '
    Me.pEncabezado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.pEncabezado.Controls.Add(Me.chkAlternos)
    Me.pEncabezado.Controls.Add(Me.GroupBox1)
    Me.pEncabezado.Controls.Add(Me.BtnExcel)
    Me.pEncabezado.Controls.Add(Me.BtnConsultar)
    Me.pEncabezado.Controls.Add(Me.ckConsolidado)
    Me.pEncabezado.Controls.Add(Me.Label2)
    Me.pEncabezado.Controls.Add(Me.Label1)
    Me.pEncabezado.Controls.Add(Me.eIni)
    Me.pEncabezado.Controls.Add(Me.eLin)
    Me.pEncabezado.Controls.Add(Me.eInv)
    Me.pEncabezado.Controls.Add(Me.eAlm)
    Me.pEncabezado.Controls.Add(Me.LblFechaIni)
    Me.pEncabezado.Controls.Add(Me.LblAlmacen)
    Me.pEncabezado.Controls.Add(Me.TBDiasInv)
    Me.pEncabezado.Controls.Add(Me.DtpFechaFin)
    Me.pEncabezado.Controls.Add(Me.DtpFechaIni)
    Me.pEncabezado.Controls.Add(Me.CBAlmacen)
    Me.pEncabezado.Location = New System.Drawing.Point(87, 31)
    Me.pEncabezado.Name = "pEncabezado"
    Me.pEncabezado.Size = New System.Drawing.Size(892, 171)
    Me.pEncabezado.TabIndex = 118
    '
    'chkAlternos
    '
    Me.chkAlternos.AutoSize = True
    Me.chkAlternos.Location = New System.Drawing.Point(397, 147)
    Me.chkAlternos.Name = "chkAlternos"
    Me.chkAlternos.Size = New System.Drawing.Size(214, 17)
    Me.chkAlternos.TabIndex = 127
    Me.chkAlternos.Text = "Incluir Artículos como proveedor alterno"
    Me.chkAlternos.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.cmbproveedor)
    Me.GroupBox1.Controls.Add(Me.cmblinea)
    Me.GroupBox1.Controls.Add(Me.lblp)
    Me.GroupBox1.Controls.Add(Me.lbllinea)
    Me.GroupBox1.Controls.Add(Me.lblapuntador2)
    Me.GroupBox1.Controls.Add(Me.lblapuntador)
    Me.GroupBox1.Controls.Add(Me.cbl)
    Me.GroupBox1.Controls.Add(Me.cbp)
    Me.GroupBox1.Controls.Add(Me.CmbLinIni)
    Me.GroupBox1.Controls.Add(Me.lbllinea1)
    Me.GroupBox1.Controls.Add(Me.CBProveedor)
    Me.GroupBox1.Controls.Add(Me.lblp1)
    Me.GroupBox1.Controls.Add(Me.eFin)
    Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.GroupBox1.Location = New System.Drawing.Point(450, 13)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(421, 112)
    Me.GroupBox1.TabIndex = 123
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Filtrar por:"
    '
    'cmbproveedor
    '
    Me.cmbproveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.cmbproveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.cmbproveedor.FormattingEnabled = True
    Me.cmbproveedor.Location = New System.Drawing.Point(122, 78)
    Me.cmbproveedor.Name = "cmbproveedor"
    Me.cmbproveedor.Size = New System.Drawing.Size(290, 24)
    Me.cmbproveedor.TabIndex = 132
    Me.cmbproveedor.Visible = False
    '
    'cmblinea
    '
    Me.cmblinea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.cmblinea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.cmblinea.FormattingEnabled = True
    Me.cmblinea.Location = New System.Drawing.Point(122, 50)
    Me.cmblinea.Name = "cmblinea"
    Me.cmblinea.Size = New System.Drawing.Size(290, 24)
    Me.cmblinea.TabIndex = 131
    Me.cmblinea.Visible = False
    '
    'lblp
    '
    Me.lblp.AutoSize = True
    Me.lblp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblp.Location = New System.Drawing.Point(37, 80)
    Me.lblp.Name = "lblp"
    Me.lblp.Size = New System.Drawing.Size(75, 16)
    Me.lblp.TabIndex = 131
    Me.lblp.Text = "Proveedor:"
    Me.lblp.Visible = False
    '
    'lbllinea
    '
    Me.lbllinea.AutoSize = True
    Me.lbllinea.Location = New System.Drawing.Point(68, 49)
    Me.lbllinea.Name = "lbllinea"
    Me.lbllinea.Size = New System.Drawing.Size(44, 16)
    Me.lbllinea.TabIndex = 132
    Me.lbllinea.Text = "Linea:"
    Me.lbllinea.Visible = False
    '
    'lblapuntador2
    '
    Me.lblapuntador2.AutoSize = True
    Me.lblapuntador2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblapuntador2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
    Me.lblapuntador2.Location = New System.Drawing.Point(6, 77)
    Me.lblapuntador2.Name = "lblapuntador2"
    Me.lblapuntador2.Size = New System.Drawing.Size(29, 20)
    Me.lblapuntador2.TabIndex = 131
    Me.lblapuntador2.Text = "=>"
    Me.lblapuntador2.Visible = False
    '
    'lblapuntador
    '
    Me.lblapuntador.AutoSize = True
    Me.lblapuntador.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblapuntador.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
    Me.lblapuntador.Location = New System.Drawing.Point(5, 48)
    Me.lblapuntador.Name = "lblapuntador"
    Me.lblapuntador.Size = New System.Drawing.Size(29, 20)
    Me.lblapuntador.TabIndex = 130
    Me.lblapuntador.Text = "=>"
    Me.lblapuntador.Visible = False
    '
    'cbl
    '
    Me.cbl.AutoSize = True
    Me.cbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cbl.Location = New System.Drawing.Point(240, 20)
    Me.cbl.Name = "cbl"
    Me.cbl.Size = New System.Drawing.Size(63, 20)
    Me.cbl.TabIndex = 123
    Me.cbl.Text = "Linea."
    Me.cbl.UseVisualStyleBackColor = True
    '
    'cbp
    '
    Me.cbp.AutoSize = True
    Me.cbp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cbp.Location = New System.Drawing.Point(118, 19)
    Me.cbp.Name = "cbp"
    Me.cbp.Size = New System.Drawing.Size(94, 20)
    Me.cbp.TabIndex = 122
    Me.cbp.Text = "Proveedor."
    Me.cbp.UseVisualStyleBackColor = True
    '
    'CmbLinIni
    '
    Me.CmbLinIni.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbLinIni.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbLinIni.FormattingEnabled = True
    Me.CmbLinIni.Location = New System.Drawing.Point(122, 77)
    Me.CmbLinIni.Margin = New System.Windows.Forms.Padding(4)
    Me.CmbLinIni.Name = "CmbLinIni"
    Me.CmbLinIni.Size = New System.Drawing.Size(290, 24)
    Me.CmbLinIni.TabIndex = 126
    '
    'lbllinea1
    '
    Me.lbllinea1.AutoSize = True
    Me.lbllinea1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbllinea1.Location = New System.Drawing.Point(68, 80)
    Me.lbllinea1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.lbllinea1.Name = "lbllinea1"
    Me.lbllinea1.Size = New System.Drawing.Size(47, 17)
    Me.lbllinea1.TabIndex = 121
    Me.lbllinea1.Text = "Línea:"
    '
    'CBProveedor
    '
    Me.CBProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CBProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CBProveedor.FormattingEnabled = True
    Me.CBProveedor.Location = New System.Drawing.Point(122, 49)
    Me.CBProveedor.Margin = New System.Windows.Forms.Padding(4)
    Me.CBProveedor.Name = "CBProveedor"
    Me.CBProveedor.Size = New System.Drawing.Size(290, 24)
    Me.CBProveedor.TabIndex = 124
    '
    'lblp1
    '
    Me.lblp1.AutoSize = True
    Me.lblp1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblp1.Location = New System.Drawing.Point(39, 48)
    Me.lblp1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.lblp1.Name = "lblp1"
    Me.lblp1.Size = New System.Drawing.Size(78, 17)
    Me.lblp1.TabIndex = 118
    Me.lblp1.Text = "Proveedor:"
    '
    'eFin
    '
    Me.eFin.AutoSize = True
    Me.eFin.ForeColor = System.Drawing.Color.Red
    Me.eFin.Location = New System.Drawing.Point(94, 21)
    Me.eFin.Name = "eFin"
    Me.eFin.Size = New System.Drawing.Size(13, 16)
    Me.eFin.TabIndex = 115
    Me.eFin.Text = "*"
    '
    'BtnExcel
    '
    Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
    Me.BtnExcel.Location = New System.Drawing.Point(830, 130)
    Me.BtnExcel.Name = "BtnExcel"
    Me.BtnExcel.Size = New System.Drawing.Size(36, 34)
    Me.BtnExcel.TabIndex = 126
    Me.BtnExcel.UseVisualStyleBackColor = True
    '
    'BtnConsultar
    '
    Me.BtnConsultar.Location = New System.Drawing.Point(689, 134)
    Me.BtnConsultar.Name = "BtnConsultar"
    Me.BtnConsultar.Size = New System.Drawing.Size(115, 27)
    Me.BtnConsultar.TabIndex = 125
    Me.BtnConsultar.Text = "Consultar"
    Me.BtnConsultar.UseVisualStyleBackColor = True
    '
    'ckConsolidado
    '
    Me.ckConsolidado.AutoSize = True
    Me.ckConsolidado.Location = New System.Drawing.Point(275, 147)
    Me.ckConsolidado.Name = "ckConsolidado"
    Me.ckConsolidado.Size = New System.Drawing.Size(104, 17)
    Me.ckConsolidado.TabIndex = 124
    Me.ckConsolidado.Text = "Consolidar datos"
    Me.ckConsolidado.UseVisualStyleBackColor = True
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(48, 74)
    Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(81, 17)
    Me.Label2.TabIndex = 120
    Me.Label2.Text = "Fecha final:"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(5, 105)
    Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(126, 17)
    Me.Label1.TabIndex = 119
    Me.Label1.Text = "Días de Inventario:"
    '
    'eIni
    '
    Me.eIni.AutoSize = True
    Me.eIni.ForeColor = System.Drawing.Color.Red
    Me.eIni.Location = New System.Drawing.Point(368, 44)
    Me.eIni.Name = "eIni"
    Me.eIni.Size = New System.Drawing.Size(11, 13)
    Me.eIni.TabIndex = 114
    Me.eIni.Text = "*"
    '
    'eLin
    '
    Me.eLin.AutoSize = True
    Me.eLin.ForeColor = System.Drawing.Color.Red
    Me.eLin.Location = New System.Drawing.Point(369, 77)
    Me.eLin.Name = "eLin"
    Me.eLin.Size = New System.Drawing.Size(11, 13)
    Me.eLin.TabIndex = 113
    Me.eLin.Text = "*"
    Me.eLin.Visible = False
    '
    'eInv
    '
    Me.eInv.AutoSize = True
    Me.eInv.ForeColor = System.Drawing.Color.Red
    Me.eInv.Location = New System.Drawing.Point(368, 105)
    Me.eInv.Name = "eInv"
    Me.eInv.Size = New System.Drawing.Size(11, 13)
    Me.eInv.TabIndex = 112
    Me.eInv.Text = "*"
    '
    'eAlm
    '
    Me.eAlm.AutoSize = True
    Me.eAlm.ForeColor = System.Drawing.Color.Red
    Me.eAlm.Location = New System.Drawing.Point(368, 13)
    Me.eAlm.Name = "eAlm"
    Me.eAlm.Size = New System.Drawing.Size(11, 13)
    Me.eAlm.TabIndex = 111
    Me.eAlm.Text = "*"
    '
    'LblFechaIni
    '
    Me.LblFechaIni.AutoSize = True
    Me.LblFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LblFechaIni.Location = New System.Drawing.Point(39, 42)
    Me.LblFechaIni.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.LblFechaIni.Name = "LblFechaIni"
    Me.LblFechaIni.Size = New System.Drawing.Size(90, 17)
    Me.LblFechaIni.TabIndex = 0
    Me.LblFechaIni.Text = "Fecha inicial:"
    '
    'LblAlmacen
    '
    Me.LblAlmacen.AutoSize = True
    Me.LblAlmacen.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LblAlmacen.Location = New System.Drawing.Point(65, 13)
    Me.LblAlmacen.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
    Me.LblAlmacen.Name = "LblAlmacen"
    Me.LblAlmacen.Size = New System.Drawing.Size(66, 17)
    Me.LblAlmacen.TabIndex = 0
    Me.LblAlmacen.Text = "Almacén:"
    '
    'TBDiasInv
    '
    Me.TBDiasInv.Location = New System.Drawing.Point(134, 105)
    Me.TBDiasInv.Margin = New System.Windows.Forms.Padding(4)
    Me.TBDiasInv.MaxLength = 3
    Me.TBDiasInv.Name = "TBDiasInv"
    Me.TBDiasInv.Size = New System.Drawing.Size(232, 20)
    Me.TBDiasInv.TabIndex = 4
    '
    'DtpFechaFin
    '
    Me.DtpFechaFin.Location = New System.Drawing.Point(134, 74)
    Me.DtpFechaFin.Margin = New System.Windows.Forms.Padding(4)
    Me.DtpFechaFin.Name = "DtpFechaFin"
    Me.DtpFechaFin.Size = New System.Drawing.Size(232, 20)
    Me.DtpFechaFin.TabIndex = 3
    '
    'DtpFechaIni
    '
    Me.DtpFechaIni.Location = New System.Drawing.Point(134, 44)
    Me.DtpFechaIni.Margin = New System.Windows.Forms.Padding(4)
    Me.DtpFechaIni.Name = "DtpFechaIni"
    Me.DtpFechaIni.Size = New System.Drawing.Size(232, 20)
    Me.DtpFechaIni.TabIndex = 2
    '
    'CBAlmacen
    '
    Me.CBAlmacen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
    Me.CBAlmacen.FormattingEnabled = True
    Me.CBAlmacen.Items.AddRange(New Object() {"01", "03", "TODOS"})
    Me.CBAlmacen.Location = New System.Drawing.Point(134, 12)
    Me.CBAlmacen.Margin = New System.Windows.Forms.Padding(4)
    Me.CBAlmacen.Name = "CBAlmacen"
    Me.CBAlmacen.Size = New System.Drawing.Size(232, 21)
    Me.CBAlmacen.TabIndex = 1
    '
    'DGAnalisisCompras
    '
    Me.DGAnalisisCompras.AllowUserToAddRows = False
    Me.DGAnalisisCompras.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DGAnalisisCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DGAnalisisCompras.Location = New System.Drawing.Point(18, 230)
    Me.DGAnalisisCompras.Name = "DGAnalisisCompras"
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DGAnalisisCompras.RowsDefaultCellStyle = DataGridViewCellStyle1
    Me.DGAnalisisCompras.Size = New System.Drawing.Size(1240, 404)
    Me.DGAnalisisCompras.TabIndex = 188
    '
    'Estatus
    '
    Me.Estatus.Controls.Add(Me.Label6)
    Me.Estatus.Controls.Add(Me.ProgressBar1)
    Me.Estatus.Location = New System.Drawing.Point(524, 427)
    Me.Estatus.Name = "Estatus"
    Me.Estatus.Size = New System.Drawing.Size(167, 54)
    Me.Estatus.TabIndex = 189
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
    'CKVtasMes
    '
    Me.CKVtasMes.AutoSize = True
    Me.CKVtasMes.Location = New System.Drawing.Point(524, 207)
    Me.CKVtasMes.Name = "CKVtasMes"
    Me.CKVtasMes.Size = New System.Drawing.Size(99, 17)
    Me.CKVtasMes.TabIndex = 126
    Me.CKVtasMes.Text = "Ventas por mes"
    Me.CKVtasMes.UseVisualStyleBackColor = True
    '
    'PBExportacion
    '
    Me.PBExportacion.Location = New System.Drawing.Point(985, 176)
    Me.PBExportacion.Name = "PBExportacion"
    Me.PBExportacion.Size = New System.Drawing.Size(271, 24)
    Me.PBExportacion.Style = System.Windows.Forms.ProgressBarStyle.Continuous
    Me.PBExportacion.TabIndex = 190
    '
    'AnalisisCompras
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1276, 646)
    Me.Controls.Add(Me.PBExportacion)
    Me.Controls.Add(Me.CKVtasMes)
    Me.Controls.Add(Me.Estatus)
    Me.Controls.Add(Me.DGAnalisisCompras)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.pEncabezado)
    Me.Name = "AnalisisCompras"
    Me.ShowIcon = False
    Me.Text = "Analisis de Compras"
    Me.pEncabezado.ResumeLayout(False)
    Me.pEncabezado.PerformLayout()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.DGAnalisisCompras, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Estatus.ResumeLayout(False)
    Me.Estatus.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pEncabezado As System.Windows.Forms.Panel
    Friend WithEvents CBProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents eFin As System.Windows.Forms.Label
    Friend WithEvents eIni As System.Windows.Forms.Label
    Friend WithEvents eLin As System.Windows.Forms.Label
    Friend WithEvents eInv As System.Windows.Forms.Label
    Friend WithEvents eAlm As System.Windows.Forms.Label
    Friend WithEvents LblFechaIni As System.Windows.Forms.Label
    Friend WithEvents LblAlmacen As System.Windows.Forms.Label
    Friend WithEvents TBDiasInv As System.Windows.Forms.TextBox
    Friend WithEvents DtpFechaFin As System.Windows.Forms.DateTimePicker
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents CBAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents lbllinea1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblp1 As System.Windows.Forms.Label
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents BtnConsultar As System.Windows.Forms.Button
    Friend WithEvents ckConsolidado As System.Windows.Forms.CheckBox
    Friend WithEvents CmbLinIni As System.Windows.Forms.ComboBox
    Friend WithEvents DGAnalisisCompras As System.Windows.Forms.DataGridView
    Friend WithEvents Estatus As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents CKVtasMes As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbl As System.Windows.Forms.CheckBox
    Friend WithEvents cbp As System.Windows.Forms.CheckBox
    Friend WithEvents lblapuntador2 As Label
    Friend WithEvents lblapuntador As Label
    Friend WithEvents lblp As Label
    Friend WithEvents lbllinea As Label
    Friend WithEvents cmbproveedor As ComboBox
    Friend WithEvents cmblinea As ComboBox
  Friend WithEvents PBExportacion As ProgressBar
  Friend WithEvents chkAlternos As CheckBox
End Class
