<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScoreCardCteObj
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
    Me.CBAño = New System.Windows.Forms.ComboBox()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label29 = New System.Windows.Forms.Label()
    Me.CBMES = New System.Windows.Forms.ComboBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.DGObjGen = New System.Windows.Forms.DataGridView()
    Me.DGObjetivosDetalles = New System.Windows.Forms.DataGridView()
    Me.BAct = New System.Windows.Forms.Button()
    Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.btnImportar = New System.Windows.Forms.Button()
    Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.DGObjIndAsesor = New System.Windows.Forms.DataGridView()
    Me.Button1 = New System.Windows.Forms.Button()
    CType(Me.DGObjGen, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DGObjetivosDetalles, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DGObjIndAsesor, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'CBAño
    '
    Me.CBAño.FormattingEnabled = True
    Me.CBAño.Items.AddRange(New Object() {"2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022"})
    Me.CBAño.Location = New System.Drawing.Point(182, 28)
    Me.CBAño.Name = "CBAño"
    Me.CBAño.Size = New System.Drawing.Size(100, 21)
    Me.CBAño.TabIndex = 585
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Location = New System.Drawing.Point(179, 7)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(26, 13)
    Me.Label10.TabIndex = 584
    Me.Label10.Text = "Año"
    '
    'Label29
    '
    Me.Label29.AutoSize = True
    Me.Label29.Location = New System.Drawing.Point(9, 8)
    Me.Label29.Name = "Label29"
    Me.Label29.Size = New System.Drawing.Size(27, 13)
    Me.Label29.TabIndex = 583
    Me.Label29.Text = "Mes"
    '
    'CBMES
    '
    Me.CBMES.FormattingEnabled = True
    Me.CBMES.Location = New System.Drawing.Point(12, 28)
    Me.CBMES.Name = "CBMES"
    Me.CBMES.Size = New System.Drawing.Size(109, 21)
    Me.CBMES.TabIndex = 582
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(434, 28)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(132, 13)
    Me.Label5.TabIndex = 577
    Me.Label5.Text = "Objetivos por asesor anual"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(12, 334)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(147, 13)
    Me.Label4.TabIndex = 576
    Me.Label4.Text = "Objetivos por agente y cliente"
    '
    'DGObjGen
    '
    Me.DGObjGen.AllowUserToAddRows = False
    Me.DGObjGen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DGObjGen.Location = New System.Drawing.Point(437, 44)
    Me.DGObjGen.Name = "DGObjGen"
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DGObjGen.RowsDefaultCellStyle = DataGridViewCellStyle1
    Me.DGObjGen.Size = New System.Drawing.Size(385, 87)
    Me.DGObjGen.TabIndex = 573
    '
    'DGObjetivosDetalles
    '
    Me.DGObjetivosDetalles.AllowUserToAddRows = False
    Me.DGObjetivosDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DGObjetivosDetalles.Location = New System.Drawing.Point(12, 350)
    Me.DGObjetivosDetalles.Name = "DGObjetivosDetalles"
    DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DGObjetivosDetalles.RowsDefaultCellStyle = DataGridViewCellStyle2
    Me.DGObjetivosDetalles.Size = New System.Drawing.Size(810, 197)
    Me.DGObjetivosDetalles.TabIndex = 572
    '
    'BAct
    '
    Me.BAct.Image = Global.TPDiamante.My.Resources.Resources.Refresh_B
    Me.BAct.Location = New System.Drawing.Point(105, 269)
    Me.BAct.Name = "BAct"
    Me.BAct.Size = New System.Drawing.Size(40, 36)
    Me.BAct.TabIndex = 575
    Me.BAct.UseVisualStyleBackColor = True
    Me.BAct.Visible = False
    '
    'CmbAgteVta
    '
    Me.CmbAgteVta.FormattingEnabled = True
    Me.CmbAgteVta.Location = New System.Drawing.Point(15, 99)
    Me.CmbAgteVta.Name = "CmbAgteVta"
    Me.CmbAgteVta.Size = New System.Drawing.Size(144, 21)
    Me.CmbAgteVta.TabIndex = 590
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(12, 79)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(41, 13)
    Me.Label9.TabIndex = 589
    Me.Label9.Text = "Agente"
    '
    'btnImportar
    '
    Me.btnImportar.BackColor = System.Drawing.Color.AliceBlue
    Me.btnImportar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnImportar.ForeColor = System.Drawing.Color.MediumBlue
    Me.btnImportar.Location = New System.Drawing.Point(182, 89)
    Me.btnImportar.Name = "btnImportar"
    Me.btnImportar.Size = New System.Drawing.Size(164, 36)
    Me.btnImportar.TabIndex = 591
    Me.btnImportar.Text = "Importar información"
    Me.btnImportar.UseVisualStyleBackColor = False
    '
    'OpenFileDialog1
    '
    Me.OpenFileDialog1.FileName = "OpenFileDialog1"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(193, 157)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(200, 13)
    Me.Label1.TabIndex = 593
    Me.Label1.Text = "Cantidad de clientes objetivos por asesor"
    '
    'DGObjIndAsesor
    '
    Me.DGObjIndAsesor.AllowUserToAddRows = False
    Me.DGObjIndAsesor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DGObjIndAsesor.Location = New System.Drawing.Point(196, 173)
    Me.DGObjIndAsesor.Name = "DGObjIndAsesor"
    DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DGObjIndAsesor.RowsDefaultCellStyle = DataGridViewCellStyle3
    Me.DGObjIndAsesor.Size = New System.Drawing.Size(626, 132)
    Me.DGObjIndAsesor.TabIndex = 592
    '
    'Button1
    '
    Me.Button1.BackColor = System.Drawing.Color.AliceBlue
    Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
    Me.Button1.Location = New System.Drawing.Point(42, 205)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(117, 58)
    Me.Button1.TabIndex = 594
    Me.Button1.Text = "Grabar objetivos por asesor"
    Me.Button1.UseVisualStyleBackColor = False
    '
    'ScoreCardCteObj
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(863, 559)
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.DGObjIndAsesor)
    Me.Controls.Add(Me.btnImportar)
    Me.Controls.Add(Me.CmbAgteVta)
    Me.Controls.Add(Me.Label9)
    Me.Controls.Add(Me.CBAño)
    Me.Controls.Add(Me.Label10)
    Me.Controls.Add(Me.Label29)
    Me.Controls.Add(Me.CBMES)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.BAct)
    Me.Controls.Add(Me.DGObjGen)
    Me.Controls.Add(Me.DGObjetivosDetalles)
    Me.Name = "ScoreCardCteObj"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Objetivos de Score Card Clientes"
    CType(Me.DGObjGen, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DGObjetivosDetalles, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DGObjIndAsesor, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents CBAño As ComboBox
  Friend WithEvents Label10 As Label
  Friend WithEvents Label29 As Label
  Friend WithEvents CBMES As ComboBox
  Friend WithEvents Label5 As Label
  Friend WithEvents Label4 As Label
  Friend WithEvents BAct As Button
  Friend WithEvents DGObjGen As DataGridView
  Friend WithEvents DGObjetivosDetalles As DataGridView
  Friend WithEvents CmbAgteVta As ComboBox
  Friend WithEvents Label9 As Label
  Friend WithEvents btnImportar As Button
  Friend WithEvents OpenFileDialog1 As OpenFileDialog
  Friend WithEvents Label1 As Label
  Friend WithEvents DGObjIndAsesor As DataGridView
  Friend WithEvents Button1 As Button
End Class
