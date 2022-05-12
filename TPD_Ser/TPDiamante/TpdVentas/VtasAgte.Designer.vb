<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reporte_de_Ventas_Detalle
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Reporte_de_Ventas_Detalle))
        Me.GrdConProd = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.GrdConLinea = New System.Windows.Forms.DataGridView()
        Me.CachedCrListaPrecio31 = New TPDiamante.CachedCrListaPrecio3()
        Me.CachedCrListaPrecio32 = New TPDiamante.CachedCrListaPrecio3()
        Me.GrdDetArt = New System.Windows.Forms.DataGridView()
        Me.CachedCrListaPrecio33 = New TPDiamante.CachedCrListaPrecio3()
        Me.CachedCrListaPrecio34 = New TPDiamante.CachedCrListaPrecio3()
        Me.CachedCrListaPrecio35 = New TPDiamante.CachedCrListaPrecio3()
        Me.CachedCrListaPrecio36 = New TPDiamante.CachedCrListaPrecio3()
        Me.CachedCrListaPrecio37 = New TPDiamante.CachedCrListaPrecio3()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CachedCrListaPrecio38 = New TPDiamante.CachedCrListaPrecio3()
        Me.CachedCrListaPrecio39 = New TPDiamante.CachedCrListaPrecio3()
        Me.CachedCrListaPrecio310 = New TPDiamante.CachedCrListaPrecio3()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CachedCrListaPrecio311 = New TPDiamante.CachedCrListaPrecio3()
        Me.GrdTodosArt = New System.Windows.Forms.DataGridView()
        Me.CachedCrListaPrecio312 = New TPDiamante.CachedCrListaPrecio3()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.BtnImprimir = New System.Windows.Forms.Button()
        Me.RBTot = New System.Windows.Forms.RadioButton()
        Me.RBDet = New System.Windows.Forms.RadioButton()
        Me.CkClientes = New System.Windows.Forms.CheckBox()
        Me.pEncabezado = New System.Windows.Forms.Panel()
        CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdConLinea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdDetArt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdTodosArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pEncabezado.SuspendLayout()
        Me.SuspendLayout()
        '
        'GrdConProd
        '
        Me.GrdConProd.AllowUserToAddRows = False
        Me.GrdConProd.AllowUserToDeleteRows = False
        Me.GrdConProd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrdConProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdConProd.Location = New System.Drawing.Point(6, 63)
        Me.GrdConProd.Name = "GrdConProd"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdConProd.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GrdConProd.Size = New System.Drawing.Size(662, 592)
        Me.GrdConProd.TabIndex = 84
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button1.Location = New System.Drawing.Point(733, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 31)
        Me.Button1.TabIndex = 73
        Me.Button1.Text = "Consultar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(360, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 17)
        Me.Label3.TabIndex = 80
        Me.Label3.Text = "Fecha Término:"
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaTer.Location = New System.Drawing.Point(470, 9)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(257, 23)
        Me.DtpFechaTer.TabIndex = 66
        Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 17)
        Me.Label5.TabIndex = 75
        Me.Label5.Text = "Fecha Inicio:"
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaIni.Location = New System.Drawing.Point(95, 9)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(259, 23)
        Me.DtpFechaIni.TabIndex = 65
        Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'GrdConLinea
        '
        Me.GrdConLinea.AllowUserToAddRows = False
        Me.GrdConLinea.AllowUserToDeleteRows = False
        Me.GrdConLinea.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrdConLinea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdConLinea.Location = New System.Drawing.Point(672, 63)
        Me.GrdConLinea.Name = "GrdConLinea"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdConLinea.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.GrdConLinea.Size = New System.Drawing.Size(684, 330)
        Me.GrdConLinea.TabIndex = 87
        '
        'GrdDetArt
        '
        Me.GrdDetArt.AllowUserToAddRows = False
        Me.GrdDetArt.AllowUserToDeleteRows = False
        Me.GrdDetArt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrdDetArt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdDetArt.Location = New System.Drawing.Point(672, 413)
        Me.GrdDetArt.Name = "GrdDetArt"
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdDetArt.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.GrdDetArt.Size = New System.Drawing.Size(684, 242)
        Me.GrdDetArt.TabIndex = 88
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Linen
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(246, 17)
        Me.Label1.TabIndex = 90
        Me.Label1.Text = "Ventas Totales Por Agente de Ventas"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(672, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(168, 17)
        Me.Label6.TabIndex = 93
        Me.Label6.Text = "Ventas Totales Por Linea"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(672, 396)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(180, 17)
        Me.Label2.TabIndex = 94
        Me.Label2.Text = "Ventas Totales Por Artículo"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(883, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 17)
        Me.Label4.TabIndex = 95
        Me.Label4.Text = "Agentes"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(982, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 17)
        Me.Label7.TabIndex = 97
        Me.Label7.Text = "Linea"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(1068, 13)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 17)
        Me.Label8.TabIndex = 99
        Me.Label8.Text = "Articulo"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1171, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 17)
        Me.Label9.TabIndex = 101
        Me.Label9.Text = "Reporte"
        '
        'GrdTodosArt
        '
        Me.GrdTodosArt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdTodosArt.Location = New System.Drawing.Point(266, 434)
        Me.GrdTodosArt.Name = "GrdTodosArt"
        Me.GrdTodosArt.Size = New System.Drawing.Size(790, 272)
        Me.GrdTodosArt.TabIndex = 102
        Me.GrdTodosArt.Visible = False
        '
        'Button4
        '
        Me.Button4.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button4.Location = New System.Drawing.Point(1129, 5)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(36, 34)
        Me.Button4.TabIndex = 100
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button3.Location = New System.Drawing.Point(1025, 5)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(36, 34)
        Me.Button3.TabIndex = 98
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button2.Location = New System.Drawing.Point(944, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(36, 34)
        Me.Button2.TabIndex = 96
        Me.Button2.UseVisualStyleBackColor = True
        '
        'BtnExcel
        '
        Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnExcel.Location = New System.Drawing.Point(845, 5)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(36, 34)
        Me.BtnExcel.TabIndex = 86
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'BtnImprimir
        '
        Me.BtnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprimir.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnImprimir.Image = Global.TPDiamante.My.Resources.Resources.printer
        Me.BtnImprimir.Location = New System.Drawing.Point(1519, 10)
        Me.BtnImprimir.Name = "BtnImprimir"
        Me.BtnImprimir.Size = New System.Drawing.Size(43, 39)
        Me.BtnImprimir.TabIndex = 85
        Me.BtnImprimir.UseVisualStyleBackColor = True
        Me.BtnImprimir.Visible = False
        '
        'RBTot
        '
        Me.RBTot.AutoSize = True
        Me.RBTot.Location = New System.Drawing.Point(924, 394)
        Me.RBTot.Name = "RBTot"
        Me.RBTot.Size = New System.Drawing.Size(79, 17)
        Me.RBTot.TabIndex = 103
        Me.RBTot.TabStop = True
        Me.RBTot.Text = "Ver Totales"
        Me.RBTot.UseVisualStyleBackColor = True
        '
        'RBDet
        '
        Me.RBDet.AutoSize = True
        Me.RBDet.Location = New System.Drawing.Point(1021, 394)
        Me.RBDet.Name = "RBDet"
        Me.RBDet.Size = New System.Drawing.Size(77, 17)
        Me.RBDet.TabIndex = 104
        Me.RBDet.TabStop = True
        Me.RBDet.Text = "Ver Detalle"
        Me.RBDet.UseVisualStyleBackColor = True
        '
        'CkClientes
        '
        Me.CkClientes.AutoSize = True
        Me.CkClientes.Checked = True
        Me.CkClientes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CkClientes.Location = New System.Drawing.Point(470, 38)
        Me.CkClientes.Name = "CkClientes"
        Me.CkClientes.Size = New System.Drawing.Size(100, 17)
        Me.CkClientes.TabIndex = 105
        Me.CkClientes.Text = "Clientes propios"
        Me.CkClientes.UseVisualStyleBackColor = True
        '
        'pEncabezado
        '
        Me.pEncabezado.Controls.Add(Me.Label5)
        Me.pEncabezado.Controls.Add(Me.CkClientes)
        Me.pEncabezado.Controls.Add(Me.DtpFechaIni)
        Me.pEncabezado.Controls.Add(Me.Label3)
        Me.pEncabezado.Controls.Add(Me.DtpFechaTer)
        Me.pEncabezado.Controls.Add(Me.Button1)
        Me.pEncabezado.Controls.Add(Me.Label9)
        Me.pEncabezado.Controls.Add(Me.BtnExcel)
        Me.pEncabezado.Controls.Add(Me.Button4)
        Me.pEncabezado.Controls.Add(Me.Label4)
        Me.pEncabezado.Controls.Add(Me.Label8)
        Me.pEncabezado.Controls.Add(Me.Button2)
        Me.pEncabezado.Controls.Add(Me.Button3)
        Me.pEncabezado.Controls.Add(Me.Label7)
        Me.pEncabezado.Location = New System.Drawing.Point(6, 3)
        Me.pEncabezado.Name = "pEncabezado"
        Me.pEncabezado.Size = New System.Drawing.Size(1234, 59)
        Me.pEncabezado.TabIndex = 106
        '
        'Reporte_de_Ventas_Detalle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1370, 664)
        Me.Controls.Add(Me.RBDet)
        Me.Controls.Add(Me.RBTot)
        Me.Controls.Add(Me.GrdTodosArt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GrdDetArt)
        Me.Controls.Add(Me.GrdConLinea)
        Me.Controls.Add(Me.GrdConProd)
        Me.Controls.Add(Me.BtnImprimir)
        Me.Controls.Add(Me.pEncabezado)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Reporte_de_Ventas_Detalle"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ventas Agente - Linea"
        CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdConLinea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdDetArt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdTodosArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pEncabezado.ResumeLayout(False)
        Me.pEncabezado.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents GrdConProd As System.Windows.Forms.DataGridView
    Friend WithEvents BtnImprimir As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents GrdConLinea As System.Windows.Forms.DataGridView
    Friend WithEvents CachedCrListaPrecio31 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CachedCrListaPrecio32 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents GrdDetArt As System.Windows.Forms.DataGridView
    Friend WithEvents CachedCrListaPrecio33 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CachedCrListaPrecio34 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CachedCrListaPrecio35 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CachedCrListaPrecio36 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CachedCrListaPrecio37 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents CachedCrListaPrecio38 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CachedCrListaPrecio39 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CachedCrListaPrecio310 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CachedCrListaPrecio311 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents GrdTodosArt As System.Windows.Forms.DataGridView
    Friend WithEvents CachedCrListaPrecio312 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents RBTot As System.Windows.Forms.RadioButton
    Friend WithEvents RBDet As System.Windows.Forms.RadioButton
    Friend WithEvents CkClientes As System.Windows.Forms.CheckBox
    Friend WithEvents pEncabezado As System.Windows.Forms.Panel
End Class
