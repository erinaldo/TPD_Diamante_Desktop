<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TraspasoAlm
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cbxAlmacenOri = New System.Windows.Forms.ComboBox()
        Me.CBAlmacenDestino = New System.Windows.Forms.ComboBox()
        Me.CBIndividual = New System.Windows.Forms.CheckBox()
        Me.CheckReq = New System.Windows.Forms.CheckBox()
        Me.CmbLin = New System.Windows.Forms.ComboBox()
        Me.CBGeneral = New System.Windows.Forms.CheckBox()
        Me.TBDiasInvDestino = New System.Windows.Forms.TextBox()
        Me.TBDiasInvP = New System.Windows.Forms.TextBox()
        Me.dtFin = New System.Windows.Forms.DateTimePicker()
        Me.dtIni = New System.Windows.Forms.DateTimePicker()
        Me.lblLinea = New System.Windows.Forms.Label()
        Me.lblDiasSM = New System.Windows.Forms.Label()
        Me.lblDiasSP = New System.Windows.Forms.Label()
        Me.lblFin = New System.Windows.Forms.Label()
        Me.lblIni = New System.Windows.Forms.Label()
        Me.eInvP = New System.Windows.Forms.Label()
        Me.eInvM = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnConsultar = New System.Windows.Forms.Button()
        Me.bExcel = New System.Windows.Forms.Button()
        Me.DGTraspaso = New System.Windows.Forms.DataGridView()
        Me.Estatus = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.DGTraspaso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Estatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.AliceBlue
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button3.Location = New System.Drawing.Point(1112, 53)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(139, 36)
        Me.Button3.TabIndex = 184
        Me.Button3.Text = "Dias de Inventario"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cbxAlmacenOri)
        Me.Panel1.Controls.Add(Me.CBAlmacenDestino)
        Me.Panel1.Controls.Add(Me.CBIndividual)
        Me.Panel1.Controls.Add(Me.CheckReq)
        Me.Panel1.Controls.Add(Me.CmbLin)
        Me.Panel1.Controls.Add(Me.CBGeneral)
        Me.Panel1.Controls.Add(Me.TBDiasInvDestino)
        Me.Panel1.Controls.Add(Me.TBDiasInvP)
        Me.Panel1.Controls.Add(Me.dtFin)
        Me.Panel1.Controls.Add(Me.dtIni)
        Me.Panel1.Controls.Add(Me.lblLinea)
        Me.Panel1.Controls.Add(Me.lblDiasSM)
        Me.Panel1.Controls.Add(Me.lblDiasSP)
        Me.Panel1.Controls.Add(Me.lblFin)
        Me.Panel1.Controls.Add(Me.lblIni)
        Me.Panel1.Controls.Add(Me.eInvP)
        Me.Panel1.Controls.Add(Me.eInvM)
        Me.Panel1.Location = New System.Drawing.Point(11, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(961, 82)
        Me.Panel1.TabIndex = 182
        '
        'cbxAlmacenOri
        '
        Me.cbxAlmacenOri.FormattingEnabled = True
        Me.cbxAlmacenOri.Location = New System.Drawing.Point(659, 9)
        Me.cbxAlmacenOri.Name = "cbxAlmacenOri"
        Me.cbxAlmacenOri.Size = New System.Drawing.Size(84, 21)
        Me.cbxAlmacenOri.TabIndex = 186
        '
        'CBAlmacenDestino
        '
        Me.CBAlmacenDestino.FormattingEnabled = True
        Me.CBAlmacenDestino.Location = New System.Drawing.Point(660, 44)
        Me.CBAlmacenDestino.Name = "CBAlmacenDestino"
        Me.CBAlmacenDestino.Size = New System.Drawing.Size(83, 21)
        Me.CBAlmacenDestino.TabIndex = 119
        '
        'CBIndividual
        '
        Me.CBIndividual.AutoSize = True
        Me.CBIndividual.Location = New System.Drawing.Point(829, 39)
        Me.CBIndividual.Name = "CBIndividual"
        Me.CBIndividual.Size = New System.Drawing.Size(112, 17)
        Me.CBIndividual.TabIndex = 185
        Me.CBIndividual.Text = "Reporte Individual"
        Me.CBIndividual.UseVisualStyleBackColor = True
        '
        'CheckReq
        '
        Me.CheckReq.AutoSize = True
        Me.CheckReq.Location = New System.Drawing.Point(359, 46)
        Me.CheckReq.Name = "CheckReq"
        Me.CheckReq.Size = New System.Drawing.Size(178, 17)
        Me.CheckReq.TabIndex = 20
        Me.CheckReq.Text = "Solo artículos con requerimiento"
        Me.CheckReq.UseVisualStyleBackColor = True
        '
        'CmbLin
        '
        Me.CmbLin.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbLin.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbLin.FormattingEnabled = True
        Me.CmbLin.Location = New System.Drawing.Point(59, 45)
        Me.CmbLin.Name = "CmbLin"
        Me.CmbLin.Size = New System.Drawing.Size(281, 21)
        Me.CmbLin.TabIndex = 19
        '
        'CBGeneral
        '
        Me.CBGeneral.AutoSize = True
        Me.CBGeneral.Location = New System.Drawing.Point(829, 16)
        Me.CBGeneral.Name = "CBGeneral"
        Me.CBGeneral.Size = New System.Drawing.Size(104, 17)
        Me.CBGeneral.TabIndex = 120
        Me.CBGeneral.Text = "Reporte General"
        Me.CBGeneral.UseVisualStyleBackColor = True
        '
        'TBDiasInvDestino
        '
        Me.TBDiasInvDestino.Location = New System.Drawing.Point(749, 45)
        Me.TBDiasInvDestino.Name = "TBDiasInvDestino"
        Me.TBDiasInvDestino.Size = New System.Drawing.Size(60, 20)
        Me.TBDiasInvDestino.TabIndex = 15
        '
        'TBDiasInvP
        '
        Me.TBDiasInvP.Location = New System.Drawing.Point(749, 9)
        Me.TBDiasInvP.Name = "TBDiasInvP"
        Me.TBDiasInvP.Size = New System.Drawing.Size(60, 20)
        Me.TBDiasInvP.TabIndex = 14
        '
        'dtFin
        '
        Me.dtFin.Location = New System.Drawing.Point(378, 9)
        Me.dtFin.Name = "dtFin"
        Me.dtFin.Size = New System.Drawing.Size(200, 20)
        Me.dtFin.TabIndex = 12
        '
        'dtIni
        '
        Me.dtIni.Location = New System.Drawing.Point(88, 9)
        Me.dtIni.Name = "dtIni"
        Me.dtIni.Size = New System.Drawing.Size(200, 20)
        Me.dtIni.TabIndex = 11
        Me.dtIni.Value = New Date(2015, 1, 1, 0, 0, 0, 0)
        '
        'lblLinea
        '
        Me.lblLinea.AutoSize = True
        Me.lblLinea.Location = New System.Drawing.Point(15, 47)
        Me.lblLinea.Name = "lblLinea"
        Me.lblLinea.Size = New System.Drawing.Size(42, 13)
        Me.lblLinea.TabIndex = 21
        Me.lblLinea.Text = "Linea : "
        '
        'lblDiasSM
        '
        Me.lblDiasSM.AutoSize = True
        Me.lblDiasSM.Location = New System.Drawing.Point(596, 46)
        Me.lblDiasSM.Name = "lblDiasSM"
        Me.lblDiasSM.Size = New System.Drawing.Size(60, 13)
        Me.lblDiasSM.TabIndex = 18
        Me.lblDiasSM.Text = "Dias stock "
        '
        'lblDiasSP
        '
        Me.lblDiasSP.AutoSize = True
        Me.lblDiasSP.Location = New System.Drawing.Point(596, 12)
        Me.lblDiasSP.Name = "lblDiasSP"
        Me.lblDiasSP.Size = New System.Drawing.Size(57, 13)
        Me.lblDiasSP.TabIndex = 17
        Me.lblDiasSP.Text = "Dias stock"
        '
        'lblFin
        '
        Me.lblFin.AutoSize = True
        Me.lblFin.Location = New System.Drawing.Point(299, 12)
        Me.lblFin.Name = "lblFin"
        Me.lblFin.Size = New System.Drawing.Size(77, 13)
        Me.lblFin.TabIndex = 16
        Me.lblFin.Text = "Fecha termino:"
        '
        'lblIni
        '
        Me.lblIni.AutoSize = True
        Me.lblIni.Location = New System.Drawing.Point(13, 12)
        Me.lblIni.Name = "lblIni"
        Me.lblIni.Size = New System.Drawing.Size(72, 13)
        Me.lblIni.TabIndex = 13
        Me.lblIni.Text = "Fecha inicial :"
        '
        'eInvP
        '
        Me.eInvP.AutoSize = True
        Me.eInvP.ForeColor = System.Drawing.Color.Red
        Me.eInvP.Location = New System.Drawing.Point(808, 11)
        Me.eInvP.Name = "eInvP"
        Me.eInvP.Size = New System.Drawing.Size(11, 13)
        Me.eInvP.TabIndex = 116
        Me.eInvP.Text = "*"
        Me.eInvP.Visible = False
        '
        'eInvM
        '
        Me.eInvM.AutoSize = True
        Me.eInvM.ForeColor = System.Drawing.Color.Red
        Me.eInvM.Location = New System.Drawing.Point(808, 46)
        Me.eInvM.Name = "eInvM"
        Me.eInvM.Size = New System.Drawing.Size(11, 13)
        Me.eInvM.TabIndex = 117
        Me.eInvM.Text = "*"
        Me.eInvM.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(581, 82)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(83, 20)
        Me.TextBox1.TabIndex = 118
        Me.TextBox1.Text = "PUEBLA"
        Me.TextBox1.Visible = False
        '
        'btnConsultar
        '
        Me.btnConsultar.Location = New System.Drawing.Point(984, 12)
        Me.btnConsultar.Name = "btnConsultar"
        Me.btnConsultar.Size = New System.Drawing.Size(104, 28)
        Me.btnConsultar.TabIndex = 115
        Me.btnConsultar.Text = "Consultar"
        Me.btnConsultar.UseVisualStyleBackColor = True
        '
        'bExcel
        '
        Me.bExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.bExcel.Location = New System.Drawing.Point(1023, 52)
        Me.bExcel.Name = "bExcel"
        Me.bExcel.Size = New System.Drawing.Size(32, 27)
        Me.bExcel.TabIndex = 112
        Me.bExcel.UseVisualStyleBackColor = True
        '
        'DGTraspaso
        '
        Me.DGTraspaso.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGTraspaso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGTraspaso.Location = New System.Drawing.Point(11, 108)
        Me.DGTraspaso.Name = "DGTraspaso"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGTraspaso.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DGTraspaso.Size = New System.Drawing.Size(1262, 517)
        Me.DGTraspaso.TabIndex = 187
        '
        'Estatus
        '
        Me.Estatus.Controls.Add(Me.Label1)
        Me.Estatus.Controls.Add(Me.ProgressBar1)
        Me.Estatus.Location = New System.Drawing.Point(610, 284)
        Me.Estatus.Name = "Estatus"
        Me.Estatus.Size = New System.Drawing.Size(167, 54)
        Me.Estatus.TabIndex = 188
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
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 29)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(140, 17)
        Me.ProgressBar1.TabIndex = 112
        '
        'btnExport
        '
        Me.btnExport.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
        Me.btnExport.Location = New System.Drawing.Point(1215, 14)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(36, 33)
        Me.btnExport.TabIndex = 189
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'TraspasoAlm
        '
        Me.AcceptButton = Me.btnConsultar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1285, 637)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.Estatus)
        Me.Controls.Add(Me.DGTraspaso)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.bExcel)
        Me.Controls.Add(Me.btnConsultar)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "TraspasoAlm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Traspaso entre Almacenes"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DGTraspaso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Estatus.ResumeLayout(False)
        Me.Estatus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnConsultar As System.Windows.Forms.Button
    Friend WithEvents bExcel As System.Windows.Forms.Button
    Friend WithEvents CheckReq As System.Windows.Forms.CheckBox
    Friend WithEvents CmbLin As System.Windows.Forms.ComboBox
    Friend WithEvents TBDiasInvDestino As System.Windows.Forms.TextBox
    Friend WithEvents TBDiasInvP As System.Windows.Forms.TextBox
    Friend WithEvents dtFin As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblLinea As System.Windows.Forms.Label
    Friend WithEvents lblDiasSM As System.Windows.Forms.Label
    Friend WithEvents lblDiasSP As System.Windows.Forms.Label
    Friend WithEvents lblFin As System.Windows.Forms.Label
    Friend WithEvents lblIni As System.Windows.Forms.Label
    Friend WithEvents eInvP As System.Windows.Forms.Label
    Friend WithEvents eInvM As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents CBAlmacenDestino As System.Windows.Forms.ComboBox
    Friend WithEvents CBGeneral As System.Windows.Forms.CheckBox
    Friend WithEvents CBIndividual As System.Windows.Forms.CheckBox
    Friend WithEvents DGTraspaso As System.Windows.Forms.DataGridView
    Friend WithEvents Estatus As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents cbxAlmacenOri As ComboBox
    Friend WithEvents btnExport As Button
End Class
