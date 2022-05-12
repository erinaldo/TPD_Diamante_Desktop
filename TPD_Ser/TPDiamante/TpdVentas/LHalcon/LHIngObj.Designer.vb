<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LHIngObj
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LHIngObj))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DGObjGen = New System.Windows.Forms.DataGridView()
        Me.DGObjetivos = New System.Windows.Forms.DataGridView()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TbObjGen = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
        Me.CBAño = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.CBMES = New System.Windows.Forms.ComboBox()
        Me.txtObjetivo = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.BAct = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.BAgregar = New System.Windows.Forms.Button()
        CType(Me.DGObjGen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGObjetivos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(580, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(105, 13)
        Me.Label5.TabIndex = 513
        Me.Label5.Text = "Objetivos Mensuales"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(342, 221)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 13)
        Me.Label4.TabIndex = 512
        Me.Label4.Text = "Objetivos por agente"
        '
        'DGObjGen
        '
        Me.DGObjGen.AllowUserToAddRows = False
        Me.DGObjGen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGObjGen.Location = New System.Drawing.Point(580, 78)
        Me.DGObjGen.Name = "DGObjGen"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGObjGen.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGObjGen.Size = New System.Drawing.Size(256, 87)
        Me.DGObjGen.TabIndex = 507
        '
        'DGObjetivos
        '
        Me.DGObjetivos.AllowUserToAddRows = False
        Me.DGObjetivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGObjetivos.Location = New System.Drawing.Point(345, 241)
        Me.DGObjetivos.Name = "DGObjetivos"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGObjetivos.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DGObjetivos.Size = New System.Drawing.Size(491, 158)
        Me.DGObjetivos.TabIndex = 506
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(784, 62)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 515
        Me.Label6.Text = "Borrar Línea"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(770, 224)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(66, 13)
        Me.Label18.TabIndex = 516
        Me.Label18.Text = "Borrar Línea"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(714, 444)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(63, 13)
        Me.Label26.TabIndex = 517
        Me.Label26.Text = "Borrar Todo"
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.AliceBlue
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button6.Location = New System.Drawing.Point(401, 96)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(86, 36)
        Me.Button6.TabIndex = 533
        Me.Button6.Text = "Consultar"
        Me.Button6.UseVisualStyleBackColor = False
        Me.Button6.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(177, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(13, 13)
        Me.Label3.TabIndex = 532
        Me.Label3.Text = "$"
        Me.Label3.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(193, 81)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 13)
        Me.Label8.TabIndex = 531
        Me.Label8.Text = "Objetivo General"
        Me.Label8.Visible = False
        '
        'TbObjGen
        '
        Me.TbObjGen.Location = New System.Drawing.Point(196, 123)
        Me.TbObjGen.Name = "TbObjGen"
        Me.TbObjGen.Size = New System.Drawing.Size(83, 20)
        Me.TbObjGen.TabIndex = 530
        Me.TbObjGen.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(23, 76)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 529
        Me.Label9.Text = "Agente"
        '
        'CmbAgteVta
        '
        Me.CmbAgteVta.FormattingEnabled = True
        Me.CmbAgteVta.Location = New System.Drawing.Point(26, 96)
        Me.CmbAgteVta.Name = "CmbAgteVta"
        Me.CmbAgteVta.Size = New System.Drawing.Size(144, 21)
        Me.CmbAgteVta.TabIndex = 528
        '
        'CBAño
        '
        Me.CBAño.FormattingEnabled = True
        Me.CBAño.Items.AddRange(New Object() {"2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022"})
        Me.CBAño.Location = New System.Drawing.Point(196, 42)
        Me.CBAño.Name = "CBAño"
        Me.CBAño.Size = New System.Drawing.Size(100, 21)
        Me.CBAño.TabIndex = 527
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(193, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(26, 13)
        Me.Label10.TabIndex = 526
        Me.Label10.Text = "Año"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(23, 22)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(27, 13)
        Me.Label29.TabIndex = 525
        Me.Label29.Text = "Mes"
        '
        'CBMES
        '
        Me.CBMES.FormattingEnabled = True
        Me.CBMES.Location = New System.Drawing.Point(26, 42)
        Me.CBMES.Name = "CBMES"
        Me.CBMES.Size = New System.Drawing.Size(109, 21)
        Me.CBMES.TabIndex = 524
        '
        'txtObjetivo
        '
        Me.txtObjetivo.Location = New System.Drawing.Point(196, 97)
        Me.txtObjetivo.Name = "txtObjetivo"
        Me.txtObjetivo.Size = New System.Drawing.Size(100, 20)
        Me.txtObjetivo.TabIndex = 534
        '
        'Button2
        '
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(796, 22)
        Me.Button2.Name = "Button2"
        Me.Button2.Padding = New System.Windows.Forms.Padding(1)
        Me.Button2.Size = New System.Drawing.Size(40, 40)
        Me.Button2.TabIndex = 514
        Me.Button2.UseVisualStyleBackColor = True
        '
        'BAct
        '
        Me.BAct.Image = Global.TPDiamante.My.Resources.Resources.Refresh_B
        Me.BAct.Location = New System.Drawing.Point(210, 404)
        Me.BAct.Name = "BAct"
        Me.BAct.Size = New System.Drawing.Size(40, 36)
        Me.BAct.TabIndex = 511
        Me.BAct.UseVisualStyleBackColor = True
        Me.BAct.Visible = False
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(720, 404)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(47, 41)
        Me.Button1.TabIndex = 509
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(796, 181)
        Me.Button3.Name = "Button3"
        Me.Button3.Padding = New System.Windows.Forms.Padding(1)
        Me.Button3.Size = New System.Drawing.Size(40, 40)
        Me.Button3.TabIndex = 505
        Me.Button3.UseVisualStyleBackColor = True
        '
        'BAgregar
        '
        Me.BAgregar.BackColor = System.Drawing.SystemColors.Control
        Me.BAgregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BAgregar.ForeColor = System.Drawing.Color.MediumBlue
        Me.BAgregar.Image = CType(resources.GetObject("BAgregar.Image"), System.Drawing.Image)
        Me.BAgregar.Location = New System.Drawing.Point(315, 88)
        Me.BAgregar.Name = "BAgregar"
        Me.BAgregar.Size = New System.Drawing.Size(42, 34)
        Me.BAgregar.TabIndex = 504
        Me.BAgregar.UseVisualStyleBackColor = False
        '
        'LHIngObj
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(863, 461)
        Me.Controls.Add(Me.txtObjetivo)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TbObjGen)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.CmbAgteVta)
        Me.Controls.Add(Me.CBAño)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.CBMES)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.BAct)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DGObjGen)
        Me.Controls.Add(Me.DGObjetivos)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.BAgregar)
        Me.Name = "LHIngObj"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ingresar objetivos LH"
        CType(Me.DGObjGen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGObjetivos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents BAct As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DGObjGen As System.Windows.Forms.DataGridView
    Friend WithEvents DGObjetivos As System.Windows.Forms.DataGridView
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents BAgregar As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Button6 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TbObjGen As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents CmbAgteVta As ComboBox
    Friend WithEvents CBAño As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents CBMES As ComboBox
    Friend WithEvents txtObjetivo As TextBox
End Class
