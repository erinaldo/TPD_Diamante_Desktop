<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reporte_de_Ventas_DetallePA
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Reporte_de_Ventas_DetallePA))
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
        CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdConLinea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdDetArt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdTodosArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GrdConProd
        '
        Me.GrdConProd.AllowUserToAddRows = False
        Me.GrdConProd.AllowUserToDeleteRows = False
        Me.GrdConProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdConProd.Location = New System.Drawing.Point(9, 34)
        Me.GrdConProd.Name = "GrdConProd"
        Me.GrdConProd.Size = New System.Drawing.Size(789, 81)
        Me.GrdConProd.TabIndex = 84
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button1.Location = New System.Drawing.Point(723, 1)
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
        Me.Label3.Location = New System.Drawing.Point(341, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 17)
        Me.Label3.TabIndex = 80
        Me.Label3.Text = "Fecha Término:"
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaTer.Location = New System.Drawing.Point(451, 4)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(240, 23)
        Me.DtpFechaTer.TabIndex = 66
        Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 17)
        Me.Label5.TabIndex = 75
        Me.Label5.Text = "Fecha Inicio:"
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaIni.Location = New System.Drawing.Point(96, 4)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(240, 23)
        Me.DtpFechaIni.TabIndex = 65
        Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'GrdConLinea
        '
        Me.GrdConLinea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdConLinea.Location = New System.Drawing.Point(9, 134)
        Me.GrdConLinea.Name = "GrdConLinea"
        Me.GrdConLinea.Size = New System.Drawing.Size(789, 188)
        Me.GrdConLinea.TabIndex = 87
        '
        'GrdDetArt
        '
        Me.GrdDetArt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrdDetArt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdDetArt.Location = New System.Drawing.Point(6, 342)
        Me.GrdDetArt.Name = "GrdDetArt"
        Me.GrdDetArt.Size = New System.Drawing.Size(789, 167)
        Me.GrdDetArt.TabIndex = 88
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(9, 117)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(168, 17)
        Me.Label6.TabIndex = 93
        Me.Label6.Text = "Ventas Totales Por Linea"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(6, 324)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(180, 17)
        Me.Label2.TabIndex = 94
        Me.Label2.Text = "Ventas Totales Por Artículo"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(840, 138)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 17)
        Me.Label4.TabIndex = 95
        Me.Label4.Text = "Agentes"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(841, 187)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 17)
        Me.Label7.TabIndex = 97
        Me.Label7.Text = "Linea"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(842, 238)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 17)
        Me.Label8.TabIndex = 99
        Me.Label8.Text = "Articulo"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(846, 289)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 17)
        Me.Label9.TabIndex = 101
        Me.Label9.Text = "Reporte"
        '
        'GrdTodosArt
        '
        Me.GrdTodosArt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdTodosArt.Location = New System.Drawing.Point(231, 489)
        Me.GrdTodosArt.Name = "GrdTodosArt"
        Me.GrdTodosArt.Size = New System.Drawing.Size(790, 272)
        Me.GrdTodosArt.TabIndex = 102
        Me.GrdTodosArt.Visible = False
        '
        'Button4
        '
        Me.Button4.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button4.Location = New System.Drawing.Point(804, 282)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(36, 34)
        Me.Button4.TabIndex = 100
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button3.Location = New System.Drawing.Point(804, 231)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(36, 34)
        Me.Button3.TabIndex = 98
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button2.Location = New System.Drawing.Point(804, 181)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(36, 34)
        Me.Button2.TabIndex = 96
        Me.Button2.UseVisualStyleBackColor = True
        '
        'BtnExcel
        '
        Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnExcel.Location = New System.Drawing.Point(804, 132)
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
        Me.BtnImprimir.Location = New System.Drawing.Point(1092, 50)
        Me.BtnImprimir.Name = "BtnImprimir"
        Me.BtnImprimir.Size = New System.Drawing.Size(43, 39)
        Me.BtnImprimir.TabIndex = 85
        Me.BtnImprimir.UseVisualStyleBackColor = True
        Me.BtnImprimir.Visible = False
        '
        'Reporte_de_Ventas_DetallePA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(905, 513)
        Me.Controls.Add(Me.GrdTodosArt)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GrdDetArt)
        Me.Controls.Add(Me.GrdConLinea)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.GrdConProd)
        Me.Controls.Add(Me.BtnImprimir)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DtpFechaTer)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DtpFechaIni)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Reporte_de_Ventas_DetallePA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ventas Agente - Linea"
        CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdConLinea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdDetArt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdTodosArt, System.ComponentModel.ISupportInitialize).EndInit()
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
End Class
