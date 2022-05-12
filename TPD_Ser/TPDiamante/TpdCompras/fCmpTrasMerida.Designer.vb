<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fCmpTrasMerida
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnConsultar = New System.Windows.Forms.Button()
        Me.bExcel = New System.Windows.Forms.Button()
        Me.chcArticulosReq = New System.Windows.Forms.CheckBox()
        Me.CmbLin = New System.Windows.Forms.ComboBox()
        Me.txtDiasSM = New System.Windows.Forms.TextBox()
        Me.txtDiasSP = New System.Windows.Forms.TextBox()
        Me.dtFin = New System.Windows.Forms.DateTimePicker()
        Me.dtIni = New System.Windows.Forms.DateTimePicker()
        Me.lblLinea = New System.Windows.Forms.Label()
        Me.lblDiasSM = New System.Windows.Forms.Label()
        Me.lblDiasSP = New System.Windows.Forms.Label()
        Me.lblFin = New System.Windows.Forms.Label()
        Me.lblIni = New System.Windows.Forms.Label()
        Me.eInvP = New System.Windows.Forms.Label()
        Me.eInvM = New System.Windows.Forms.Label()
        Me.DgRptTraspaso = New System.Windows.Forms.DataGridView()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.DgRptTraspaso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnConsultar)
        Me.Panel1.Controls.Add(Me.bExcel)
        Me.Panel1.Controls.Add(Me.chcArticulosReq)
        Me.Panel1.Controls.Add(Me.CmbLin)
        Me.Panel1.Controls.Add(Me.txtDiasSM)
        Me.Panel1.Controls.Add(Me.txtDiasSP)
        Me.Panel1.Controls.Add(Me.dtFin)
        Me.Panel1.Controls.Add(Me.dtIni)
        Me.Panel1.Controls.Add(Me.lblLinea)
        Me.Panel1.Controls.Add(Me.lblDiasSM)
        Me.Panel1.Controls.Add(Me.lblDiasSP)
        Me.Panel1.Controls.Add(Me.lblFin)
        Me.Panel1.Controls.Add(Me.lblIni)
        Me.Panel1.Controls.Add(Me.eInvP)
        Me.Panel1.Controls.Add(Me.eInvM)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(943, 79)
        Me.Panel1.TabIndex = 11
        '
        'btnConsultar
        '
        Me.btnConsultar.Location = New System.Drawing.Point(859, 8)
        Me.btnConsultar.Name = "btnConsultar"
        Me.btnConsultar.Size = New System.Drawing.Size(75, 23)
        Me.btnConsultar.TabIndex = 115
        Me.btnConsultar.Text = "Consultar"
        Me.btnConsultar.UseVisualStyleBackColor = True
        '
        'bExcel
        '
        Me.bExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.bExcel.Location = New System.Drawing.Point(877, 42)
        Me.bExcel.Name = "bExcel"
        Me.bExcel.Size = New System.Drawing.Size(32, 27)
        Me.bExcel.TabIndex = 112
        Me.bExcel.UseVisualStyleBackColor = True
        Me.bExcel.Visible = False
        '
        'chcArticulosReq
        '
        Me.chcArticulosReq.AutoSize = True
        Me.chcArticulosReq.Location = New System.Drawing.Point(411, 47)
        Me.chcArticulosReq.Name = "chcArticulosReq"
        Me.chcArticulosReq.Size = New System.Drawing.Size(153, 17)
        Me.chcArticulosReq.TabIndex = 20
        Me.chcArticulosReq.Text = "Articulos con requerimiento"
        Me.chcArticulosReq.UseVisualStyleBackColor = True
        '
        'CmbLin
        '
        Me.CmbLin.FormattingEnabled = True
        Me.CmbLin.Location = New System.Drawing.Point(97, 45)
        Me.CmbLin.Name = "CmbLin"
        Me.CmbLin.Size = New System.Drawing.Size(281, 21)
        Me.CmbLin.TabIndex = 19
        '
        'txtDiasSM
        '
        Me.txtDiasSM.Location = New System.Drawing.Point(724, 45)
        Me.txtDiasSM.Name = "txtDiasSM"
        Me.txtDiasSM.Size = New System.Drawing.Size(100, 20)
        Me.txtDiasSM.TabIndex = 15
        '
        'txtDiasSP
        '
        Me.txtDiasSP.Location = New System.Drawing.Point(724, 9)
        Me.txtDiasSP.Name = "txtDiasSP"
        Me.txtDiasSP.Size = New System.Drawing.Size(100, 20)
        Me.txtDiasSP.TabIndex = 14
        '
        'dtFin
        '
        Me.dtFin.Location = New System.Drawing.Point(411, 9)
        Me.dtFin.Name = "dtFin"
        Me.dtFin.Size = New System.Drawing.Size(200, 20)
        Me.dtFin.TabIndex = 12
        '
        'dtIni
        '
        Me.dtIni.Location = New System.Drawing.Point(97, 9)
        Me.dtIni.Name = "dtIni"
        Me.dtIni.Size = New System.Drawing.Size(200, 20)
        Me.dtIni.TabIndex = 11
        Me.dtIni.Value = New Date(2014, 10, 1, 0, 0, 0, 0)
        '
        'lblLinea
        '
        Me.lblLinea.AutoSize = True
        Me.lblLinea.Location = New System.Drawing.Point(53, 47)
        Me.lblLinea.Name = "lblLinea"
        Me.lblLinea.Size = New System.Drawing.Size(42, 13)
        Me.lblLinea.TabIndex = 21
        Me.lblLinea.Text = "Linea : "
        '
        'lblDiasSM
        '
        Me.lblDiasSM.AutoSize = True
        Me.lblDiasSM.Location = New System.Drawing.Point(627, 47)
        Me.lblDiasSM.Name = "lblDiasSM"
        Me.lblDiasSM.Size = New System.Drawing.Size(95, 13)
        Me.lblDiasSM.TabIndex = 18
        Me.lblDiasSM.Text = "Dias stock Merida:"
        '
        'lblDiasSP
        '
        Me.lblDiasSP.AutoSize = True
        Me.lblDiasSP.Location = New System.Drawing.Point(627, 12)
        Me.lblDiasSP.Name = "lblDiasSP"
        Me.lblDiasSP.Size = New System.Drawing.Size(96, 13)
        Me.lblDiasSP.TabIndex = 17
        Me.lblDiasSP.Text = "Dias stock Puebla:"
        '
        'lblFin
        '
        Me.lblFin.AutoSize = True
        Me.lblFin.Location = New System.Drawing.Point(332, 12)
        Me.lblFin.Name = "lblFin"
        Me.lblFin.Size = New System.Drawing.Size(77, 13)
        Me.lblFin.TabIndex = 16
        Me.lblFin.Text = "Fecha termino:"
        '
        'lblIni
        '
        Me.lblIni.AutoSize = True
        Me.lblIni.Location = New System.Drawing.Point(22, 12)
        Me.lblIni.Name = "lblIni"
        Me.lblIni.Size = New System.Drawing.Size(72, 13)
        Me.lblIni.TabIndex = 13
        Me.lblIni.Text = "Fecha inicial :"
        '
        'eInvP
        '
        Me.eInvP.AutoSize = True
        Me.eInvP.ForeColor = System.Drawing.Color.Red
        Me.eInvP.Location = New System.Drawing.Point(824, 11)
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
        Me.eInvM.Location = New System.Drawing.Point(824, 46)
        Me.eInvM.Name = "eInvM"
        Me.eInvM.Size = New System.Drawing.Size(11, 13)
        Me.eInvM.TabIndex = 117
        Me.eInvM.Text = "*"
        Me.eInvM.Visible = False
        '
        'DgRptTraspaso
        '
        Me.DgRptTraspaso.AllowUserToAddRows = False
        Me.DgRptTraspaso.AllowUserToDeleteRows = False
        Me.DgRptTraspaso.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgRptTraspaso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgRptTraspaso.Location = New System.Drawing.Point(12, 87)
        Me.DgRptTraspaso.Name = "DgRptTraspaso"
        Me.DgRptTraspaso.ReadOnly = True
        Me.DgRptTraspaso.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgRptTraspaso.Size = New System.Drawing.Size(1283, 426)
        Me.DgRptTraspaso.TabIndex = 111
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.AliceBlue
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button3.Location = New System.Drawing.Point(1153, 35)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(139, 36)
        Me.Button3.TabIndex = 181
        Me.Button3.Text = "Dias de Inventario"
        Me.Button3.UseVisualStyleBackColor = False
        Me.Button3.Visible = False
        '
        'fCmpTrasMerida
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1304, 515)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.DgRptTraspaso)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "fCmpTrasMerida"
        Me.Text = "Traspaso Merida"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DgRptTraspaso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblLinea As System.Windows.Forms.Label
    Friend WithEvents chcArticulosReq As System.Windows.Forms.CheckBox
    Friend WithEvents CmbLin As System.Windows.Forms.ComboBox
    Friend WithEvents lblDiasSM As System.Windows.Forms.Label
    Friend WithEvents lblDiasSP As System.Windows.Forms.Label
    Friend WithEvents lblFin As System.Windows.Forms.Label
    Friend WithEvents txtDiasSM As System.Windows.Forms.TextBox
    Friend WithEvents txtDiasSP As System.Windows.Forms.TextBox
    Friend WithEvents lblIni As System.Windows.Forms.Label
    Friend WithEvents dtFin As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnConsultar As System.Windows.Forms.Button
    Friend WithEvents bExcel As System.Windows.Forms.Button
    Friend WithEvents DgRptTraspaso As System.Windows.Forms.DataGridView
    Friend WithEvents eInvP As System.Windows.Forms.Label
    Friend WithEvents eInvM As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
