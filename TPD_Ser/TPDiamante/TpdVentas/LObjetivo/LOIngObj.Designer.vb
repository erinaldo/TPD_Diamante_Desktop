<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LOIngObj
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LOIngObj))
    Me.txtCantidadLinea = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cmbLineas = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Button6 = New System.Windows.Forms.Button()
    Me.TbObjGen = New System.Windows.Forms.TextBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.CBAño = New System.Windows.Forms.ComboBox()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label29 = New System.Windows.Forms.Label()
    Me.CBMES = New System.Windows.Forms.ComboBox()
    Me.Label26 = New System.Windows.Forms.Label()
    Me.Label18 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.DGObjGen = New System.Windows.Forms.DataGridView()
    Me.DGObjetivos = New System.Windows.Forms.DataGridView()
    Me.Button2 = New System.Windows.Forms.Button()
    Me.BAct = New System.Windows.Forms.Button()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.Button3 = New System.Windows.Forms.Button()
    Me.BAgregar = New System.Windows.Forms.Button()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
    CType(Me.DGObjGen, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DGObjetivos, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'txtCantidadLinea
    '
    Me.txtCantidadLinea.Location = New System.Drawing.Point(182, 180)
    Me.txtCantidadLinea.Name = "txtCantidadLinea"
    Me.txtCantidadLinea.Size = New System.Drawing.Size(76, 20)
    Me.txtCantidadLinea.TabIndex = 566
    Me.txtCantidadLinea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(179, 162)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(49, 13)
    Me.Label2.TabIndex = 565
    Me.Label2.Text = "Cantidad"
    '
    'cmbLineas
    '
    Me.cmbLineas.FormattingEnabled = True
    Me.cmbLineas.Location = New System.Drawing.Point(12, 180)
    Me.cmbLineas.Name = "cmbLineas"
    Me.cmbLineas.Size = New System.Drawing.Size(144, 21)
    Me.cmbLineas.TabIndex = 564
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(9, 162)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(84, 13)
    Me.Label1.TabIndex = 563
    Me.Label1.Text = "Lineas objectivo"
    '
    'Button6
    '
    Me.Button6.BackColor = System.Drawing.Color.AliceBlue
    Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button6.ForeColor = System.Drawing.Color.MediumBlue
    Me.Button6.Location = New System.Drawing.Point(387, 82)
    Me.Button6.Name = "Button6"
    Me.Button6.Size = New System.Drawing.Size(86, 36)
    Me.Button6.TabIndex = 561
    Me.Button6.Text = "Consultar"
    Me.Button6.UseVisualStyleBackColor = False
    Me.Button6.Visible = False
    '
    'TbObjGen
    '
    Me.TbObjGen.Location = New System.Drawing.Point(182, 109)
    Me.TbObjGen.Name = "TbObjGen"
    Me.TbObjGen.Size = New System.Drawing.Size(83, 20)
    Me.TbObjGen.TabIndex = 558
    Me.TbObjGen.Visible = False
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(9, 62)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(41, 13)
    Me.Label9.TabIndex = 557
    Me.Label9.Text = "Agente"
    '
    'CBAño
    '
    Me.CBAño.FormattingEnabled = True
    Me.CBAño.Items.AddRange(New Object() {"2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022"})
    Me.CBAño.Location = New System.Drawing.Point(182, 28)
    Me.CBAño.Name = "CBAño"
    Me.CBAño.Size = New System.Drawing.Size(100, 21)
    Me.CBAño.TabIndex = 555
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Location = New System.Drawing.Point(179, 7)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(26, 13)
    Me.Label10.TabIndex = 554
    Me.Label10.Text = "Año"
    '
    'Label29
    '
    Me.Label29.AutoSize = True
    Me.Label29.Location = New System.Drawing.Point(9, 8)
    Me.Label29.Name = "Label29"
    Me.Label29.Size = New System.Drawing.Size(27, 13)
    Me.Label29.TabIndex = 553
    Me.Label29.Text = "Mes"
    '
    'CBMES
    '
    Me.CBMES.FormattingEnabled = True
    Me.CBMES.Location = New System.Drawing.Point(12, 28)
    Me.CBMES.Name = "CBMES"
    Me.CBMES.Size = New System.Drawing.Size(109, 21)
    Me.CBMES.TabIndex = 552
    '
    'Label26
    '
    Me.Label26.AutoSize = True
    Me.Label26.Location = New System.Drawing.Point(700, 430)
    Me.Label26.Name = "Label26"
    Me.Label26.Size = New System.Drawing.Size(63, 13)
    Me.Label26.TabIndex = 551
    Me.Label26.Text = "Borrar Todo"
    '
    'Label18
    '
    Me.Label18.AutoSize = True
    Me.Label18.Location = New System.Drawing.Point(756, 210)
    Me.Label18.Name = "Label18"
    Me.Label18.Size = New System.Drawing.Size(66, 13)
    Me.Label18.TabIndex = 550
    Me.Label18.Text = "Borrar Línea"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(770, 48)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(66, 13)
    Me.Label6.TabIndex = 549
    Me.Label6.Text = "Borrar Línea"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(566, 48)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(105, 13)
    Me.Label5.TabIndex = 547
    Me.Label5.Text = "Objetivos Mensuales"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(328, 207)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(105, 13)
    Me.Label4.TabIndex = 546
    Me.Label4.Text = "Objetivos por agente"
    '
    'DGObjGen
    '
    Me.DGObjGen.AllowUserToAddRows = False
    Me.DGObjGen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DGObjGen.Location = New System.Drawing.Point(566, 64)
    Me.DGObjGen.Name = "DGObjGen"
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DGObjGen.RowsDefaultCellStyle = DataGridViewCellStyle1
    Me.DGObjGen.Size = New System.Drawing.Size(256, 87)
    Me.DGObjGen.TabIndex = 543
    '
    'DGObjetivos
    '
    Me.DGObjetivos.AllowUserToAddRows = False
    Me.DGObjetivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DGObjetivos.Location = New System.Drawing.Point(196, 227)
    Me.DGObjetivos.Name = "DGObjetivos"
    DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DGObjetivos.RowsDefaultCellStyle = DataGridViewCellStyle2
    Me.DGObjetivos.Size = New System.Drawing.Size(626, 158)
    Me.DGObjetivos.TabIndex = 542
    '
    'Button2
    '
    Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
    Me.Button2.Location = New System.Drawing.Point(782, 8)
    Me.Button2.Name = "Button2"
    Me.Button2.Padding = New System.Windows.Forms.Padding(1)
    Me.Button2.Size = New System.Drawing.Size(40, 40)
    Me.Button2.TabIndex = 548
    Me.Button2.UseVisualStyleBackColor = True
    '
    'BAct
    '
    Me.BAct.Image = Global.TPDiamante.My.Resources.Resources.Refresh_B
    Me.BAct.Location = New System.Drawing.Point(196, 390)
    Me.BAct.Name = "BAct"
    Me.BAct.Size = New System.Drawing.Size(40, 36)
    Me.BAct.TabIndex = 545
    Me.BAct.UseVisualStyleBackColor = True
    Me.BAct.Visible = False
    '
    'Button1
    '
    Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
    Me.Button1.Location = New System.Drawing.Point(706, 390)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(47, 41)
    Me.Button1.TabIndex = 544
    Me.Button1.UseVisualStyleBackColor = True
    '
    'Button3
    '
    Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
    Me.Button3.Location = New System.Drawing.Point(782, 167)
    Me.Button3.Name = "Button3"
    Me.Button3.Padding = New System.Windows.Forms.Padding(1)
    Me.Button3.Size = New System.Drawing.Size(40, 40)
    Me.Button3.TabIndex = 541
    Me.Button3.UseVisualStyleBackColor = True
    '
    'BAgregar
    '
    Me.BAgregar.BackColor = System.Drawing.SystemColors.Control
    Me.BAgregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.BAgregar.ForeColor = System.Drawing.Color.MediumBlue
    Me.BAgregar.Image = CType(resources.GetObject("BAgregar.Image"), System.Drawing.Image)
    Me.BAgregar.Location = New System.Drawing.Point(62, 271)
    Me.BAgregar.Name = "BAgregar"
    Me.BAgregar.Size = New System.Drawing.Size(42, 34)
    Me.BAgregar.TabIndex = 540
    Me.BAgregar.UseVisualStyleBackColor = False
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(168, 183)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(13, 13)
    Me.Label7.TabIndex = 568
    Me.Label7.Text = "$"
    Me.Label7.Visible = False
    '
    'CmbAgteVta
    '
    Me.CmbAgteVta.FormattingEnabled = True
    Me.CmbAgteVta.Location = New System.Drawing.Point(12, 82)
    Me.CmbAgteVta.Name = "CmbAgteVta"
    Me.CmbAgteVta.Size = New System.Drawing.Size(144, 21)
    Me.CmbAgteVta.TabIndex = 569
    '
    'LOIngObj
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(863, 461)
    Me.Controls.Add(Me.CmbAgteVta)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.txtCantidadLinea)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.cmbLineas)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.Button6)
    Me.Controls.Add(Me.TbObjGen)
    Me.Controls.Add(Me.Label9)
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
    Me.Name = "LOIngObj"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Ingresar objetivos de Lineas"
    CType(Me.DGObjGen, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DGObjetivos, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents txtCantidadLinea As TextBox
  Friend WithEvents Label2 As Label
  Friend WithEvents cmbLineas As ComboBox
  Friend WithEvents Label1 As Label
  Friend WithEvents Button6 As Button
  Friend WithEvents TbObjGen As TextBox
  Friend WithEvents Label9 As Label
  Friend WithEvents CBAño As ComboBox
  Friend WithEvents Label10 As Label
  Friend WithEvents Label29 As Label
  Friend WithEvents CBMES As ComboBox
  Friend WithEvents Label26 As Label
  Friend WithEvents Label18 As Label
  Friend WithEvents Label6 As Label
  Friend WithEvents Button2 As Button
  Friend WithEvents Label5 As Label
  Friend WithEvents Label4 As Label
  Friend WithEvents BAct As Button
  Friend WithEvents Button1 As Button
  Friend WithEvents DGObjGen As DataGridView
  Friend WithEvents DGObjetivos As DataGridView
  Friend WithEvents Button3 As Button
  Friend WithEvents BAgregar As Button
  Friend WithEvents Label7 As Label
  Friend WithEvents CmbAgteVta As ComboBox
End Class
