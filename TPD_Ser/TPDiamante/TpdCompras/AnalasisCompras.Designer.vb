<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AnalasisCompras
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
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CmbAlmacen = New System.Windows.Forms.ComboBox()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.DtpFechaFin = New System.Windows.Forms.DateTimePicker()
        Me.TxtDiasInv = New System.Windows.Forms.TextBox()
        Me.LblAlmacen = New System.Windows.Forms.Label()
        Me.LblFechaIni = New System.Windows.Forms.Label()
        Me.LblFechaFin = New System.Windows.Forms.Label()
        Me.LblLinIni = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnConsultar = New System.Windows.Forms.Button()
        Me.CmbLinIni = New System.Windows.Forms.ComboBox()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.DgRptCompras = New System.Windows.Forms.DataGridView()
        Me.ckConsolidado = New System.Windows.Forms.CheckBox()
        Me.eAlm = New System.Windows.Forms.Label()
        Me.eInv = New System.Windows.Forms.Label()
        Me.eLin = New System.Windows.Forms.Label()
        Me.eIni = New System.Windows.Forms.Label()
        Me.eFin = New System.Windows.Forms.Label()
        Me.pEncabezado = New System.Windows.Forms.Panel()
        Me.CBProveedor = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgRptCompras, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pEncabezado.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 11)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 15)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Analisis de compras"
        '
        'CmbAlmacen
        '
        Me.CmbAlmacen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.CmbAlmacen.FormattingEnabled = True
        Me.CmbAlmacen.Items.AddRange(New Object() {"01", "03", "TODOS"})
        Me.CmbAlmacen.Location = New System.Drawing.Point(101, 12)
        Me.CmbAlmacen.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbAlmacen.Name = "CmbAlmacen"
        Me.CmbAlmacen.Size = New System.Drawing.Size(265, 24)
        Me.CmbAlmacen.TabIndex = 1
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Location = New System.Drawing.Point(101, 44)
        Me.DtpFechaIni.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(265, 23)
        Me.DtpFechaIni.TabIndex = 3
        '
        'DtpFechaFin
        '
        Me.DtpFechaFin.Location = New System.Drawing.Point(534, 44)
        Me.DtpFechaFin.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpFechaFin.Name = "DtpFechaFin"
        Me.DtpFechaFin.Size = New System.Drawing.Size(265, 23)
        Me.DtpFechaFin.TabIndex = 4
        '
        'TxtDiasInv
        '
        Me.TxtDiasInv.Location = New System.Drawing.Point(534, 16)
        Me.TxtDiasInv.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtDiasInv.MaxLength = 3
        Me.TxtDiasInv.Name = "TxtDiasInv"
        Me.TxtDiasInv.Size = New System.Drawing.Size(265, 23)
        Me.TxtDiasInv.TabIndex = 2
        '
        'LblAlmacen
        '
        Me.LblAlmacen.AutoSize = True
        Me.LblAlmacen.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAlmacen.Location = New System.Drawing.Point(31, 16)
        Me.LblAlmacen.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblAlmacen.Name = "LblAlmacen"
        Me.LblAlmacen.Size = New System.Drawing.Size(66, 17)
        Me.LblAlmacen.TabIndex = 0
        Me.LblAlmacen.Text = "Almacen:"
        '
        'LblFechaIni
        '
        Me.LblFechaIni.AutoSize = True
        Me.LblFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFechaIni.Location = New System.Drawing.Point(7, 50)
        Me.LblFechaIni.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblFechaIni.Name = "LblFechaIni"
        Me.LblFechaIni.Size = New System.Drawing.Size(90, 17)
        Me.LblFechaIni.TabIndex = 0
        Me.LblFechaIni.Text = "Fecha inicial:"
        '
        'LblFechaFin
        '
        Me.LblFechaFin.AutoSize = True
        Me.LblFechaFin.Location = New System.Drawing.Point(449, 49)
        Me.LblFechaFin.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblFechaFin.Name = "LblFechaFin"
        Me.LblFechaFin.Size = New System.Drawing.Size(81, 17)
        Me.LblFechaFin.TabIndex = 0
        Me.LblFechaFin.Text = "Fecha final:"
        '
        'LblLinIni
        '
        Me.LblLinIni.AutoSize = True
        Me.LblLinIni.Location = New System.Drawing.Point(479, 87)
        Me.LblLinIni.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblLinIni.Name = "LblLinIni"
        Me.LblLinIni.Size = New System.Drawing.Size(51, 17)
        Me.LblLinIni.TabIndex = 0
        Me.LblLinIni.Text = "Linea :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(411, 19)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Dia de inventario:"
        '
        'BtnConsultar
        '
        Me.BtnConsultar.Location = New System.Drawing.Point(551, 115)
        Me.BtnConsultar.Name = "BtnConsultar"
        Me.BtnConsultar.Size = New System.Drawing.Size(115, 27)
        Me.BtnConsultar.TabIndex = 7
        Me.BtnConsultar.Text = "Consultar"
        Me.BtnConsultar.UseVisualStyleBackColor = True
        '
        'CmbLinIni
        '
        Me.CmbLinIni.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbLinIni.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbLinIni.FormattingEnabled = True
        Me.CmbLinIni.Location = New System.Drawing.Point(534, 80)
        Me.CmbLinIni.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbLinIni.Name = "CmbLinIni"
        Me.CmbLinIni.Size = New System.Drawing.Size(265, 24)
        Me.CmbLinIni.TabIndex = 5
        '
        'BtnExcel
        '
        Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnExcel.Location = New System.Drawing.Point(692, 111)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(36, 34)
        Me.BtnExcel.TabIndex = 8
        Me.BtnExcel.UseVisualStyleBackColor = True
        Me.BtnExcel.Visible = False
        '
        'DgRptCompras
        '
        Me.DgRptCompras.AllowUserToAddRows = False
        Me.DgRptCompras.AllowUserToDeleteRows = False
        Me.DgRptCompras.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgRptCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgRptCompras.Location = New System.Drawing.Point(5, 172)
        Me.DgRptCompras.Name = "DgRptCompras"
        Me.DgRptCompras.ReadOnly = True
        Me.DgRptCompras.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgRptCompras.Size = New System.Drawing.Size(1274, 304)
        Me.DgRptCompras.TabIndex = 109
        '
        'ckConsolidado
        '
        Me.ckConsolidado.AutoSize = True
        Me.ckConsolidado.Enabled = False
        Me.ckConsolidado.Location = New System.Drawing.Point(381, 119)
        Me.ckConsolidado.Name = "ckConsolidado"
        Me.ckConsolidado.Size = New System.Drawing.Size(133, 21)
        Me.ckConsolidado.TabIndex = 6
        Me.ckConsolidado.Text = "Consolidar datos"
        Me.ckConsolidado.UseVisualStyleBackColor = True
        '
        'eAlm
        '
        Me.eAlm.AutoSize = True
        Me.eAlm.ForeColor = System.Drawing.Color.Red
        Me.eAlm.Location = New System.Drawing.Point(366, 12)
        Me.eAlm.Name = "eAlm"
        Me.eAlm.Size = New System.Drawing.Size(13, 17)
        Me.eAlm.TabIndex = 111
        Me.eAlm.Text = "*"
        Me.eAlm.Visible = False
        '
        'eInv
        '
        Me.eInv.AutoSize = True
        Me.eInv.ForeColor = System.Drawing.Color.Red
        Me.eInv.Location = New System.Drawing.Point(798, 14)
        Me.eInv.Name = "eInv"
        Me.eInv.Size = New System.Drawing.Size(13, 17)
        Me.eInv.TabIndex = 112
        Me.eInv.Text = "*"
        Me.eInv.Visible = False
        '
        'eLin
        '
        Me.eLin.AutoSize = True
        Me.eLin.ForeColor = System.Drawing.Color.Red
        Me.eLin.Location = New System.Drawing.Point(366, 78)
        Me.eLin.Name = "eLin"
        Me.eLin.Size = New System.Drawing.Size(13, 17)
        Me.eLin.TabIndex = 113
        Me.eLin.Text = "*"
        Me.eLin.Visible = False
        '
        'eIni
        '
        Me.eIni.AutoSize = True
        Me.eIni.ForeColor = System.Drawing.Color.Red
        Me.eIni.Location = New System.Drawing.Point(366, 44)
        Me.eIni.Name = "eIni"
        Me.eIni.Size = New System.Drawing.Size(13, 17)
        Me.eIni.TabIndex = 114
        Me.eIni.Text = "*"
        Me.eIni.Visible = False
        '
        'eFin
        '
        Me.eFin.AutoSize = True
        Me.eFin.ForeColor = System.Drawing.Color.Red
        Me.eFin.Location = New System.Drawing.Point(798, 44)
        Me.eFin.Name = "eFin"
        Me.eFin.Size = New System.Drawing.Size(13, 17)
        Me.eFin.TabIndex = 115
        Me.eFin.Text = "*"
        Me.eFin.Visible = False
        '
        'pEncabezado
        '
        Me.pEncabezado.Controls.Add(Me.CBProveedor)
        Me.pEncabezado.Controls.Add(Me.Label2)
        Me.pEncabezado.Controls.Add(Me.eFin)
        Me.pEncabezado.Controls.Add(Me.eIni)
        Me.pEncabezado.Controls.Add(Me.eLin)
        Me.pEncabezado.Controls.Add(Me.BtnExcel)
        Me.pEncabezado.Controls.Add(Me.BtnConsultar)
        Me.pEncabezado.Controls.Add(Me.eInv)
        Me.pEncabezado.Controls.Add(Me.ckConsolidado)
        Me.pEncabezado.Controls.Add(Me.eAlm)
        Me.pEncabezado.Controls.Add(Me.CmbLinIni)
        Me.pEncabezado.Controls.Add(Me.Label1)
        Me.pEncabezado.Controls.Add(Me.LblLinIni)
        Me.pEncabezado.Controls.Add(Me.LblFechaFin)
        Me.pEncabezado.Controls.Add(Me.LblFechaIni)
        Me.pEncabezado.Controls.Add(Me.LblAlmacen)
        Me.pEncabezado.Controls.Add(Me.TxtDiasInv)
        Me.pEncabezado.Controls.Add(Me.DtpFechaFin)
        Me.pEncabezado.Controls.Add(Me.DtpFechaIni)
        Me.pEncabezado.Controls.Add(Me.CmbAlmacen)
        Me.pEncabezado.Location = New System.Drawing.Point(76, 16)
        Me.pEncabezado.Name = "pEncabezado"
        Me.pEncabezado.Size = New System.Drawing.Size(978, 150)
        Me.pEncabezado.TabIndex = 116
        '
        'CBProveedor
        '
        Me.CBProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBProveedor.FormattingEnabled = True
        Me.CBProveedor.Location = New System.Drawing.Point(101, 80)
        Me.CBProveedor.Margin = New System.Windows.Forms.Padding(4)
        Me.CBProveedor.Name = "CBProveedor"
        Me.CBProveedor.Size = New System.Drawing.Size(290, 24)
        Me.CBProveedor.TabIndex = 117
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 87)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 17)
        Me.Label2.TabIndex = 116
        Me.Label2.Text = "Proveedor"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "FchDoc"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Fecha del Documento"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 70
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "DiasCred"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Dias de Credito"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Factura"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Doc. Sap"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 40
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "FactProv"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Factura"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 75
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "IdProved"
        Me.DataGridViewTextBoxColumn5.HeaderText = "IdProv"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 50
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Proveedor"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Proveedor"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 236
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "TotFactura"
        Me.DataGridViewTextBoxColumn7.HeaderText = "$ Total Factura"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 80
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Moneda"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Mnd"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 35
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Pagado"
        Me.DataGridViewTextBoxColumn9.HeaderText = "$ Importe Aplicado"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 80
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "SaldoPendiente"
        Me.DataGridViewTextBoxColumn10.HeaderText = "$ Saldo Pendiente"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 80
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Moneda2"
        Me.DataGridViewTextBoxColumn11.HeaderText = "Mnd"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 35
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "DiasAtraso"
        Me.DataGridViewTextBoxColumn12.HeaderText = "Dias Atraso"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 40
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "FchVen"
        Me.DataGridViewTextBoxColumn13.HeaderText = "Fch. de Vencimiento"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Width = 70
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "SaldoPesos"
        Me.DataGridViewTextBoxColumn14.HeaderText = "$ Saldo MXP"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Width = 101
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Obrserv"
        Me.DataGridViewTextBoxColumn15.HeaderText = "Observaciones"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.Width = 184
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "Coment"
        Me.DataGridViewTextBoxColumn16.HeaderText = "Comentarios"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.Width = 265
        '
        'AnalasisCompras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1281, 480)
        Me.Controls.Add(Me.DgRptCompras)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.pEncabezado)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "AnalasisCompras"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "AnalasisCompras"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DgRptCompras, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pEncabezado.ResumeLayout(False)
        Me.pEncabezado.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CmbAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents DtpFechaFin As System.Windows.Forms.DateTimePicker
    Friend WithEvents TxtDiasInv As System.Windows.Forms.TextBox
    Friend WithEvents LblAlmacen As System.Windows.Forms.Label
    Friend WithEvents LblFechaIni As System.Windows.Forms.Label
    Friend WithEvents LblFechaFin As System.Windows.Forms.Label
    Friend WithEvents LblLinIni As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnConsultar As System.Windows.Forms.Button
    Friend WithEvents CmbLinIni As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents DgRptCompras As System.Windows.Forms.DataGridView
    Friend WithEvents ckConsolidado As System.Windows.Forms.CheckBox
    Friend WithEvents eAlm As System.Windows.Forms.Label
    Friend WithEvents eInv As System.Windows.Forms.Label
    Friend WithEvents eLin As System.Windows.Forms.Label
    Friend WithEvents eIni As System.Windows.Forms.Label
    Friend WithEvents eFin As System.Windows.Forms.Label
    Friend WithEvents pEncabezado As System.Windows.Forms.Panel
    Friend WithEvents CBProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
