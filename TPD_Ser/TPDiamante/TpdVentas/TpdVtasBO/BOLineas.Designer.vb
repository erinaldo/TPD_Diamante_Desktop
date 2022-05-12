<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BOLineas
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pEncabezado = New System.Windows.Forms.Panel()
        Me.ckConsolidado = New System.Windows.Forms.CheckBox()
        Me.eFin = New System.Windows.Forms.Label()
        Me.eIni = New System.Windows.Forms.Label()
        Me.lExDetalle = New System.Windows.Forms.Label()
        Me.lExAgente = New System.Windows.Forms.Label()
        Me.bExLinea = New System.Windows.Forms.Button()
        Me.cmbAlmacen = New System.Windows.Forms.ComboBox()
        Me.lAlm = New System.Windows.Forms.Label()
        Me.CmbCliente = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.bExDetalle = New System.Windows.Forms.Button()
        Me.bConsultar = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.DgRptBoLineas = New System.Windows.Forms.DataGridView()
        Me.DgRptBoLineasDet = New System.Windows.Forms.DataGridView()
        Me.lDgGridLinea = New System.Windows.Forms.Label()
        Me.lDetBO = New System.Windows.Forms.Label()
        Me.pEncabezado.SuspendLayout()
        CType(Me.DgRptBoLineas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgRptBoLineasDet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pEncabezado
        '
        Me.pEncabezado.Controls.Add(Me.ckConsolidado)
        Me.pEncabezado.Controls.Add(Me.eFin)
        Me.pEncabezado.Controls.Add(Me.eIni)
        Me.pEncabezado.Controls.Add(Me.lExDetalle)
        Me.pEncabezado.Controls.Add(Me.lExAgente)
        Me.pEncabezado.Controls.Add(Me.bExLinea)
        Me.pEncabezado.Controls.Add(Me.cmbAlmacen)
        Me.pEncabezado.Controls.Add(Me.lAlm)
        Me.pEncabezado.Controls.Add(Me.CmbCliente)
        Me.pEncabezado.Controls.Add(Me.Label2)
        Me.pEncabezado.Controls.Add(Me.CmbAgteVta)
        Me.pEncabezado.Controls.Add(Me.Label1)
        Me.pEncabezado.Controls.Add(Me.bExDetalle)
        Me.pEncabezado.Controls.Add(Me.bConsultar)
        Me.pEncabezado.Controls.Add(Me.Label3)
        Me.pEncabezado.Controls.Add(Me.DtpFechaTer)
        Me.pEncabezado.Controls.Add(Me.Label5)
        Me.pEncabezado.Controls.Add(Me.DtpFechaIni)
        Me.pEncabezado.Location = New System.Drawing.Point(9, 3)
        Me.pEncabezado.Name = "pEncabezado"
        Me.pEncabezado.Size = New System.Drawing.Size(1270, 81)
        Me.pEncabezado.TabIndex = 125
        '
        'ckConsolidado
        '
        Me.ckConsolidado.AutoSize = True
        Me.ckConsolidado.Enabled = False
        Me.ckConsolidado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckConsolidado.Location = New System.Drawing.Point(538, 61)
        Me.ckConsolidado.Name = "ckConsolidado"
        Me.ckConsolidado.Size = New System.Drawing.Size(85, 19)
        Me.ckConsolidado.TabIndex = 128
        Me.ckConsolidado.Text = "Consolidar"
        Me.ckConsolidado.UseVisualStyleBackColor = True
        '
        'eFin
        '
        Me.eFin.AutoSize = True
        Me.eFin.ForeColor = System.Drawing.Color.Red
        Me.eFin.Location = New System.Drawing.Point(838, 10)
        Me.eFin.Name = "eFin"
        Me.eFin.Size = New System.Drawing.Size(11, 13)
        Me.eFin.TabIndex = 127
        Me.eFin.Text = "*"
        Me.eFin.Visible = False
        '
        'eIni
        '
        Me.eIni.AutoSize = True
        Me.eIni.ForeColor = System.Drawing.Color.Red
        Me.eIni.Location = New System.Drawing.Point(396, 9)
        Me.eIni.Name = "eIni"
        Me.eIni.Size = New System.Drawing.Size(11, 13)
        Me.eIni.TabIndex = 126
        Me.eIni.Text = "*"
        Me.eIni.Visible = False
        '
        'lExDetalle
        '
        Me.lExDetalle.AutoSize = True
        Me.lExDetalle.Location = New System.Drawing.Point(1221, 31)
        Me.lExDetalle.Name = "lExDetalle"
        Me.lExDetalle.Size = New System.Drawing.Size(40, 13)
        Me.lExDetalle.TabIndex = 125
        Me.lExDetalle.Text = "Detalle"
        '
        'lExAgente
        '
        Me.lExAgente.AutoSize = True
        Me.lExAgente.Location = New System.Drawing.Point(1142, 31)
        Me.lExAgente.Name = "lExAgente"
        Me.lExAgente.Size = New System.Drawing.Size(33, 13)
        Me.lExAgente.TabIndex = 125
        Me.lExAgente.Text = "Linea"
        '
        'bExLinea
        '
        Me.bExLinea.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.bExLinea.Location = New System.Drawing.Point(1102, 20)
        Me.bExLinea.Name = "bExLinea"
        Me.bExLinea.Size = New System.Drawing.Size(34, 34)
        Me.bExLinea.TabIndex = 124
        Me.bExLinea.UseVisualStyleBackColor = True
        '
        'cmbAlmacen
        '
        Me.cmbAlmacen.FormattingEnabled = True
        Me.cmbAlmacen.Location = New System.Drawing.Point(944, 9)
        Me.cmbAlmacen.Name = "cmbAlmacen"
        Me.cmbAlmacen.Size = New System.Drawing.Size(116, 21)
        Me.cmbAlmacen.TabIndex = 123
        '
        'lAlm
        '
        Me.lAlm.AutoSize = True
        Me.lAlm.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lAlm.Location = New System.Drawing.Point(874, 10)
        Me.lAlm.Name = "lAlm"
        Me.lAlm.Size = New System.Drawing.Size(62, 17)
        Me.lAlm.TabIndex = 13
        Me.lAlm.Text = "Almacen"
        '
        'CmbCliente
        '
        Me.CmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbCliente.FormattingEnabled = True
        Me.CmbCliente.Location = New System.Drawing.Point(538, 36)
        Me.CmbCliente.Name = "CmbCliente"
        Me.CmbCliente.Size = New System.Drawing.Size(405, 21)
        Me.CmbCliente.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(480, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 17)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Cliente"
        '
        'CmbAgteVta
        '
        Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbAgteVta.FormattingEnabled = True
        Me.CmbAgteVta.Location = New System.Drawing.Point(137, 36)
        Me.CmbAgteVta.Name = "CmbAgteVta"
        Me.CmbAgteVta.Size = New System.Drawing.Size(260, 21)
        Me.CmbAgteVta.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 17)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Agente de ventas"
        '
        'bExDetalle
        '
        Me.bExDetalle.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.bExDetalle.Location = New System.Drawing.Point(1181, 20)
        Me.bExDetalle.Name = "bExDetalle"
        Me.bExDetalle.Size = New System.Drawing.Size(34, 34)
        Me.bExDetalle.TabIndex = 14
        Me.bExDetalle.UseVisualStyleBackColor = True
        '
        'bConsultar
        '
        Me.bConsultar.BackColor = System.Drawing.Color.AliceBlue
        Me.bConsultar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bConsultar.ForeColor = System.Drawing.Color.MediumBlue
        Me.bConsultar.Location = New System.Drawing.Point(963, 36)
        Me.bConsultar.Name = "bConsultar"
        Me.bConsultar.Size = New System.Drawing.Size(97, 29)
        Me.bConsultar.TabIndex = 6
        Me.bConsultar.Text = "Consultar"
        Me.bConsultar.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(436, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 17)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Fecha Término"
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaTer.Location = New System.Drawing.Point(538, 5)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(300, 23)
        Me.DtpFechaTer.TabIndex = 1
        Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(47, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 17)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Fecha Inicio"
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaIni.Location = New System.Drawing.Point(137, 6)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(260, 23)
        Me.DtpFechaIni.TabIndex = 0
        Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'DgRptBoLineas
        '
        Me.DgRptBoLineas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DgRptBoLineas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgRptBoLineas.Location = New System.Drawing.Point(9, 94)
        Me.DgRptBoLineas.Name = "DgRptBoLineas"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgRptBoLineas.RowHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgRptBoLineas.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DgRptBoLineas.Size = New System.Drawing.Size(360, 518)
        Me.DgRptBoLineas.TabIndex = 126
        '
        'DgRptBoLineasDet
        '
        Me.DgRptBoLineasDet.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgRptBoLineasDet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgRptBoLineasDet.Location = New System.Drawing.Point(375, 94)
        Me.DgRptBoLineasDet.Name = "DgRptBoLineasDet"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgRptBoLineasDet.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgRptBoLineasDet.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DgRptBoLineasDet.Size = New System.Drawing.Size(893, 518)
        Me.DgRptBoLineasDet.TabIndex = 127
        '
        'lDgGridLinea
        '
        Me.lDgGridLinea.AutoSize = True
        Me.lDgGridLinea.BackColor = System.Drawing.Color.White
        Me.lDgGridLinea.Location = New System.Drawing.Point(9, 81)
        Me.lDgGridLinea.Name = "lDgGridLinea"
        Me.lDgGridLinea.Size = New System.Drawing.Size(108, 13)
        Me.lDgGridLinea.TabIndex = 128
        Me.lDgGridLinea.Text = "Back Order por Linea"
        '
        'lDetBO
        '
        Me.lDetBO.AutoSize = True
        Me.lDetBO.BackColor = System.Drawing.Color.White
        Me.lDetBO.Location = New System.Drawing.Point(375, 81)
        Me.lDetBO.Name = "lDetBO"
        Me.lDetBO.Size = New System.Drawing.Size(97, 13)
        Me.lDetBO.TabIndex = 129
        Me.lDetBO.Text = "Detalle Back Order"
        '
        'BOLineas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1280, 615)
        Me.Controls.Add(Me.lDetBO)
        Me.Controls.Add(Me.lDgGridLinea)
        Me.Controls.Add(Me.DgRptBoLineasDet)
        Me.Controls.Add(Me.DgRptBoLineas)
        Me.Controls.Add(Me.pEncabezado)
        Me.Name = "BOLineas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BOLineas"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pEncabezado.ResumeLayout(False)
        Me.pEncabezado.PerformLayout()
        CType(Me.DgRptBoLineas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgRptBoLineasDet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pEncabezado As System.Windows.Forms.Panel
    Friend WithEvents cmbAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents lAlm As System.Windows.Forms.Label
    Friend WithEvents CmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents bExDetalle As System.Windows.Forms.Button
    Friend WithEvents bConsultar As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents bExLinea As System.Windows.Forms.Button
    Friend WithEvents DgRptBoLineas As System.Windows.Forms.DataGridView
    Friend WithEvents DgRptBoLineasDet As System.Windows.Forms.DataGridView
    Friend WithEvents lExDetalle As System.Windows.Forms.Label
    Friend WithEvents lExAgente As System.Windows.Forms.Label
    Friend WithEvents eFin As System.Windows.Forms.Label
    Friend WithEvents eIni As System.Windows.Forms.Label
    Friend WithEvents lDgGridLinea As System.Windows.Forms.Label
    Friend WithEvents lDetBO As System.Windows.Forms.Label
    Friend WithEvents ckConsolidado As System.Windows.Forms.CheckBox
End Class
