<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FacturasArtCli
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
        Me.CmbArticulo = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CmbGrupoArticulo = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbAlmacen = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblFechaIni = New System.Windows.Forms.Label()
        Me.DtpFechaFin = New System.Windows.Forms.DateTimePicker()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.DGFacturas = New System.Windows.Forms.DataGridView()
        Me.pEncabezado.SuspendLayout()
        CType(Me.DGFacturas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(27, 17)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 15)
        Me.Label5.TabIndex = 141
        Me.Label5.Text = "Detalle Facturas"
        '
        'pEncabezado
        '
        Me.pEncabezado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pEncabezado.Controls.Add(Me.CmbArticulo)
        Me.pEncabezado.Controls.Add(Me.Label6)
        Me.pEncabezado.Controls.Add(Me.CmbGrupoArticulo)
        Me.pEncabezado.Controls.Add(Me.Label4)
        Me.pEncabezado.Controls.Add(Me.BtnExcel)
        Me.pEncabezado.Controls.Add(Me.Button2)
        Me.pEncabezado.Controls.Add(Me.Label3)
        Me.pEncabezado.Controls.Add(Me.cmbAlmacen)
        Me.pEncabezado.Controls.Add(Me.Label1)
        Me.pEncabezado.Controls.Add(Me.LblFechaIni)
        Me.pEncabezado.Controls.Add(Me.DtpFechaFin)
        Me.pEncabezado.Controls.Add(Me.DtpFechaIni)
        Me.pEncabezado.Location = New System.Drawing.Point(30, 26)
        Me.pEncabezado.Name = "pEncabezado"
        Me.pEncabezado.Size = New System.Drawing.Size(943, 158)
        Me.pEncabezado.TabIndex = 140
        '
        'CmbArticulo
        '
        Me.CmbArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbArticulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbArticulo.FormattingEnabled = True
        Me.CmbArticulo.Location = New System.Drawing.Point(355, 33)
        Me.CmbArticulo.Name = "CmbArticulo"
        Me.CmbArticulo.Size = New System.Drawing.Size(183, 21)
        Me.CmbArticulo.TabIndex = 182
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(352, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 17)
        Me.Label6.TabIndex = 184
        Me.Label6.Text = "Artículo"
        '
        'CmbGrupoArticulo
        '
        Me.CmbGrupoArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbGrupoArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbGrupoArticulo.FormattingEnabled = True
        Me.CmbGrupoArticulo.Location = New System.Drawing.Point(180, 33)
        Me.CmbGrupoArticulo.Name = "CmbGrupoArticulo"
        Me.CmbGrupoArticulo.Size = New System.Drawing.Size(157, 21)
        Me.CmbGrupoArticulo.TabIndex = 181
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(177, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 17)
        Me.Label4.TabIndex = 183
        Me.Label4.Text = "Línea"
        '
        'BtnExcel
        '
        Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnExcel.Location = New System.Drawing.Point(543, 81)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(36, 34)
        Me.BtnExcel.TabIndex = 180
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.AliceBlue
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button2.Location = New System.Drawing.Point(406, 79)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 36)
        Me.Button2.TabIndex = 150
        Me.Button2.Text = "Consultar"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
        Me.Label3.Location = New System.Drawing.Point(12, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 15)
        Me.Label3.TabIndex = 148
        Me.Label3.Text = "Almacen"
        '
        'cmbAlmacen
        '
        Me.cmbAlmacen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbAlmacen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAlmacen.FormattingEnabled = True
        Me.cmbAlmacen.Location = New System.Drawing.Point(12, 33)
        Me.cmbAlmacen.Name = "cmbAlmacen"
        Me.cmbAlmacen.Size = New System.Drawing.Size(151, 21)
        Me.cmbAlmacen.TabIndex = 146
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(42, 107)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 17)
        Me.Label1.TabIndex = 145
        Me.Label1.Text = "Fecha final:"
        '
        'LblFechaIni
        '
        Me.LblFechaIni.AutoSize = True
        Me.LblFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFechaIni.Location = New System.Drawing.Point(33, 79)
        Me.LblFechaIni.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblFechaIni.Name = "LblFechaIni"
        Me.LblFechaIni.Size = New System.Drawing.Size(90, 17)
        Me.LblFechaIni.TabIndex = 142
        Me.LblFechaIni.Text = "Fecha inicial:"
        '
        'DtpFechaFin
        '
        Me.DtpFechaFin.Location = New System.Drawing.Point(124, 107)
        Me.DtpFechaFin.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpFechaFin.Name = "DtpFechaFin"
        Me.DtpFechaFin.Size = New System.Drawing.Size(213, 20)
        Me.DtpFechaFin.TabIndex = 144
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Location = New System.Drawing.Point(124, 79)
        Me.DtpFechaIni.Margin = New System.Windows.Forms.Padding(4)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(213, 20)
        Me.DtpFechaIni.TabIndex = 143
        '
        'DGFacturas
        '
        Me.DGFacturas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGFacturas.Location = New System.Drawing.Point(30, 192)
        Me.DGFacturas.Name = "DGFacturas"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGFacturas.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGFacturas.Size = New System.Drawing.Size(1151, 326)
        Me.DGFacturas.TabIndex = 142
        '
        'FacturasArtCli
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1216, 528)
        Me.Controls.Add(Me.DGFacturas)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.pEncabezado)
        Me.Name = "FacturasArtCli"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FacturasArtCli"
        Me.pEncabezado.ResumeLayout(False)
        Me.pEncabezado.PerformLayout()
        CType(Me.DGFacturas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pEncabezado As System.Windows.Forms.Panel
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LblFechaIni As System.Windows.Forms.Label
    Friend WithEvents DtpFechaFin As System.Windows.Forms.DateTimePicker
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents CmbArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CmbGrupoArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DGFacturas As System.Windows.Forms.DataGridView
End Class
