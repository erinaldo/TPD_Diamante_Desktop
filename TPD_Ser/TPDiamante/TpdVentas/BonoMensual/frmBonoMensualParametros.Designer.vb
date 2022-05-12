<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBonoMensualParametros
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBonoMensualParametros))
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.btnEliminarRegistro = New System.Windows.Forms.Button()
  Me.btnGrabaDetalles = New System.Windows.Forms.Button()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
  Me.Label7 = New System.Windows.Forms.Label()
  Me.txtCantidadLinea = New System.Windows.Forms.TextBox()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.Label9 = New System.Windows.Forms.Label()
  Me.DGImporteBono = New System.Windows.Forms.DataGridView()
  Me.PanelGeneral = New System.Windows.Forms.Panel()
  Me.btnGrabaGenerales = New System.Windows.Forms.Button()
  Me.txtLineasObjetivo = New System.Windows.Forms.TextBox()
  Me.txtLineasHalcon = New System.Windows.Forms.TextBox()
  Me.txtClientes = New System.Windows.Forms.TextBox()
  Me.txtScoreCard = New System.Windows.Forms.TextBox()
  Me.txtMinimoparabono = New System.Windows.Forms.TextBox()
  Me.Label11 = New System.Windows.Forms.Label()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
  Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
  Me.CBAño = New System.Windows.Forms.ComboBox()
  Me.Label10 = New System.Windows.Forms.Label()
  Me.Label29 = New System.Windows.Forms.Label()
  Me.CBMES = New System.Windows.Forms.ComboBox()
  Me.Button1 = New System.Windows.Forms.Button()
  Me.Panel1.SuspendLayout()
  CType(Me.DGImporteBono, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.PanelGeneral.SuspendLayout()
  Me.MenuStrip1.SuspendLayout()
  Me.SuspendLayout()
  '
  'Panel1
  '
  Me.Panel1.Controls.Add(Me.btnEliminarRegistro)
  Me.Panel1.Controls.Add(Me.btnGrabaDetalles)
  Me.Panel1.Controls.Add(Me.Label3)
  Me.Panel1.Controls.Add(Me.CmbAgteVta)
  Me.Panel1.Controls.Add(Me.Label7)
  Me.Panel1.Controls.Add(Me.txtCantidadLinea)
  Me.Panel1.Controls.Add(Me.Label2)
  Me.Panel1.Controls.Add(Me.Label9)
  Me.Panel1.Controls.Add(Me.DGImporteBono)
  Me.Panel1.Location = New System.Drawing.Point(12, 284)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(462, 342)
  Me.Panel1.TabIndex = 0
  '
  'btnEliminarRegistro
  '
  Me.btnEliminarRegistro.Location = New System.Drawing.Point(365, 57)
  Me.btnEliminarRegistro.Name = "btnEliminarRegistro"
  Me.btnEliminarRegistro.Size = New System.Drawing.Size(92, 47)
  Me.btnEliminarRegistro.TabIndex = 585
  Me.btnEliminarRegistro.Text = "Eliminar registro de agente"
  Me.btnEliminarRegistro.UseVisualStyleBackColor = True
  '
  'btnGrabaDetalles
  '
  Me.btnGrabaDetalles.Location = New System.Drawing.Point(365, 6)
  Me.btnGrabaDetalles.Name = "btnGrabaDetalles"
  Me.btnGrabaDetalles.Size = New System.Drawing.Size(92, 47)
  Me.btnGrabaDetalles.TabIndex = 584
  Me.btnGrabaDetalles.Text = "Grabar registro de agente"
  Me.btnGrabaDetalles.UseVisualStyleBackColor = True
  '
  'Label3
  '
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.ForeColor = System.Drawing.Color.Navy
  Me.Label3.Location = New System.Drawing.Point(3, 3)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(335, 23)
  Me.Label3.TabIndex = 583
  Me.Label3.Text = "Parametros a detalle por vendedor"
  Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'CmbAgteVta
  '
  Me.CmbAgteVta.FormattingEnabled = True
  Me.CmbAgteVta.Location = New System.Drawing.Point(8, 80)
  Me.CmbAgteVta.Name = "CmbAgteVta"
  Me.CmbAgteVta.Size = New System.Drawing.Size(192, 21)
  Me.CmbAgteVta.TabIndex = 582
  '
  'Label7
  '
  Me.Label7.AutoSize = True
  Me.Label7.Location = New System.Drawing.Point(217, 84)
  Me.Label7.Name = "Label7"
  Me.Label7.Size = New System.Drawing.Size(13, 13)
  Me.Label7.TabIndex = 581
  Me.Label7.Text = "$"
  Me.Label7.Visible = False
  '
  'txtCantidadLinea
  '
  Me.txtCantidadLinea.Location = New System.Drawing.Point(231, 81)
  Me.txtCantidadLinea.Name = "txtCantidadLinea"
  Me.txtCantidadLinea.Size = New System.Drawing.Size(76, 20)
  Me.txtCantidadLinea.TabIndex = 580
  Me.txtCantidadLinea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Location = New System.Drawing.Point(228, 63)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(70, 13)
  Me.Label2.TabIndex = 579
  Me.Label2.Text = "Importe Bono"
  '
  'Label9
  '
  Me.Label9.AutoSize = True
  Me.Label9.Location = New System.Drawing.Point(5, 60)
  Me.Label9.Name = "Label9"
  Me.Label9.Size = New System.Drawing.Size(41, 13)
  Me.Label9.TabIndex = 576
  Me.Label9.Text = "Agente"
  '
  'DGImporteBono
  '
  Me.DGImporteBono.AllowUserToAddRows = False
  Me.DGImporteBono.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.DGImporteBono.Location = New System.Drawing.Point(8, 107)
  Me.DGImporteBono.Name = "DGImporteBono"
  DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DGImporteBono.RowsDefaultCellStyle = DataGridViewCellStyle1
  Me.DGImporteBono.Size = New System.Drawing.Size(448, 232)
  Me.DGImporteBono.TabIndex = 571
  '
  'PanelGeneral
  '
  Me.PanelGeneral.Controls.Add(Me.btnGrabaGenerales)
  Me.PanelGeneral.Controls.Add(Me.txtLineasObjetivo)
  Me.PanelGeneral.Controls.Add(Me.txtLineasHalcon)
  Me.PanelGeneral.Controls.Add(Me.txtClientes)
  Me.PanelGeneral.Controls.Add(Me.txtScoreCard)
  Me.PanelGeneral.Controls.Add(Me.txtMinimoparabono)
  Me.PanelGeneral.Controls.Add(Me.Label11)
  Me.PanelGeneral.Controls.Add(Me.Label8)
  Me.PanelGeneral.Controls.Add(Me.Label6)
  Me.PanelGeneral.Controls.Add(Me.Label5)
  Me.PanelGeneral.Controls.Add(Me.Label4)
  Me.PanelGeneral.Controls.Add(Me.Label1)
  Me.PanelGeneral.Location = New System.Drawing.Point(12, 86)
  Me.PanelGeneral.Name = "PanelGeneral"
  Me.PanelGeneral.Size = New System.Drawing.Size(462, 187)
  Me.PanelGeneral.TabIndex = 1
  '
  'btnGrabaGenerales
  '
  Me.btnGrabaGenerales.BackColor = System.Drawing.SystemColors.Control
  Me.btnGrabaGenerales.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.btnGrabaGenerales.ForeColor = System.Drawing.Color.MediumBlue
  Me.btnGrabaGenerales.Image = CType(resources.GetObject("btnGrabaGenerales.Image"), System.Drawing.Image)
  Me.btnGrabaGenerales.Location = New System.Drawing.Point(363, 83)
  Me.btnGrabaGenerales.Name = "btnGrabaGenerales"
  Me.btnGrabaGenerales.Size = New System.Drawing.Size(42, 34)
  Me.btnGrabaGenerales.TabIndex = 571
  Me.btnGrabaGenerales.UseVisualStyleBackColor = False
  '
  'txtLineasObjetivo
  '
  Me.txtLineasObjetivo.Location = New System.Drawing.Point(277, 154)
  Me.txtLineasObjetivo.Name = "txtLineasObjetivo"
  Me.txtLineasObjetivo.Size = New System.Drawing.Size(32, 20)
  Me.txtLineasObjetivo.TabIndex = 10
  Me.txtLineasObjetivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtLineasHalcon
  '
  Me.txtLineasHalcon.Location = New System.Drawing.Point(277, 126)
  Me.txtLineasHalcon.Name = "txtLineasHalcon"
  Me.txtLineasHalcon.Size = New System.Drawing.Size(32, 20)
  Me.txtLineasHalcon.TabIndex = 9
  Me.txtLineasHalcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtClientes
  '
  Me.txtClientes.Location = New System.Drawing.Point(277, 97)
  Me.txtClientes.Name = "txtClientes"
  Me.txtClientes.Size = New System.Drawing.Size(32, 20)
  Me.txtClientes.TabIndex = 8
  Me.txtClientes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtScoreCard
  '
  Me.txtScoreCard.Location = New System.Drawing.Point(277, 69)
  Me.txtScoreCard.Name = "txtScoreCard"
  Me.txtScoreCard.Size = New System.Drawing.Size(32, 20)
  Me.txtScoreCard.TabIndex = 7
  Me.txtScoreCard.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtMinimoparabono
  '
  Me.txtMinimoparabono.Location = New System.Drawing.Point(277, 42)
  Me.txtMinimoparabono.Name = "txtMinimoparabono"
  Me.txtMinimoparabono.Size = New System.Drawing.Size(32, 20)
  Me.txtMinimoparabono.TabIndex = 6
  Me.txtMinimoparabono.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'Label11
  '
  Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label11.Location = New System.Drawing.Point(16, 156)
  Me.Label11.Name = "Label11"
  Me.Label11.Size = New System.Drawing.Size(311, 15)
  Me.Label11.TabIndex = 5
  Me.Label11.Text = "Porcentaje de Scar Cord Líneas Objetivo:          %"
  Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label8
  '
  Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label8.Location = New System.Drawing.Point(16, 128)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(311, 15)
  Me.Label8.TabIndex = 4
  Me.Label8.Text = "Porcentaje de Scar Cord Líneas Halcon:          %"
  Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label6
  '
  Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label6.Location = New System.Drawing.Point(16, 99)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(311, 15)
  Me.Label6.TabIndex = 3
  Me.Label6.Text = "Porcentaje de Scar Cord clientes:          %"
  Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label5
  '
  Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label5.Location = New System.Drawing.Point(16, 71)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(311, 15)
  Me.Label5.TabIndex = 2
  Me.Label5.Text = "Porcentaje de Scar Cord general:          %"
  Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label4
  '
  Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label4.Location = New System.Drawing.Point(16, 44)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(311, 15)
  Me.Label4.TabIndex = 1
  Me.Label4.Text = "Porcentaje mínimo para recibir bono mesual:          %"
  Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label1
  '
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.ForeColor = System.Drawing.Color.Navy
  Me.Label1.Location = New System.Drawing.Point(4, 3)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(352, 23)
  Me.Label1.TabIndex = 0
  Me.Label1.Text = "Parametros generales"
  Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'MenuStrip1
  '
  Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalirToolStripMenuItem})
  Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
  Me.MenuStrip1.Name = "MenuStrip1"
  Me.MenuStrip1.Size = New System.Drawing.Size(484, 24)
  Me.MenuStrip1.TabIndex = 2
  Me.MenuStrip1.Text = "MenuStrip1"
  '
  'SalirToolStripMenuItem
  '
  Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
  Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
  Me.SalirToolStripMenuItem.Text = "&Salir"
  '
  'CBAño
  '
  Me.CBAño.FormattingEnabled = True
  Me.CBAño.Items.AddRange(New Object() {"2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022"})
  Me.CBAño.Location = New System.Drawing.Point(64, 47)
  Me.CBAño.Name = "CBAño"
  Me.CBAño.Size = New System.Drawing.Size(67, 21)
  Me.CBAño.TabIndex = 579
  '
  'Label10
  '
  Me.Label10.AutoSize = True
  Me.Label10.Location = New System.Drawing.Point(32, 49)
  Me.Label10.Name = "Label10"
  Me.Label10.Size = New System.Drawing.Size(26, 13)
  Me.Label10.TabIndex = 578
  Me.Label10.Text = "Año"
  '
  'Label29
  '
  Me.Label29.AutoSize = True
  Me.Label29.Location = New System.Drawing.Point(164, 49)
  Me.Label29.Name = "Label29"
  Me.Label29.Size = New System.Drawing.Size(27, 13)
  Me.Label29.TabIndex = 577
  Me.Label29.Text = "Mes"
  '
  'CBMES
  '
  Me.CBMES.FormattingEnabled = True
  Me.CBMES.Location = New System.Drawing.Point(193, 47)
  Me.CBMES.Name = "CBMES"
  Me.CBMES.Size = New System.Drawing.Size(85, 21)
  Me.CBMES.TabIndex = 576
  '
  'Button1
  '
  Me.Button1.Location = New System.Drawing.Point(317, 36)
  Me.Button1.Name = "Button1"
  Me.Button1.Size = New System.Drawing.Size(152, 41)
  Me.Button1.TabIndex = 580
  Me.Button1.Text = "Copiar parametros del mes anterior"
  Me.Button1.UseVisualStyleBackColor = True
  '
  'frmBonoMensualParametros
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(484, 638)
  Me.ControlBox = False
  Me.Controls.Add(Me.Button1)
  Me.Controls.Add(Me.CBAño)
  Me.Controls.Add(Me.Label10)
  Me.Controls.Add(Me.Label29)
  Me.Controls.Add(Me.CBMES)
  Me.Controls.Add(Me.PanelGeneral)
  Me.Controls.Add(Me.Panel1)
  Me.Controls.Add(Me.MenuStrip1)
  Me.MainMenuStrip = Me.MenuStrip1
  Me.Name = "frmBonoMensualParametros"
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Parametros para bonos mensuales"
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  CType(Me.DGImporteBono, System.ComponentModel.ISupportInitialize).EndInit()
  Me.PanelGeneral.ResumeLayout(False)
  Me.PanelGeneral.PerformLayout()
  Me.MenuStrip1.ResumeLayout(False)
  Me.MenuStrip1.PerformLayout()
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub

 Friend WithEvents Panel1 As Panel
  Friend WithEvents CmbAgteVta As ComboBox
  Friend WithEvents Label7 As Label
  Friend WithEvents txtCantidadLinea As TextBox
  Friend WithEvents Label2 As Label
  Friend WithEvents Label9 As Label
  Friend WithEvents DGImporteBono As DataGridView
  Friend WithEvents PanelGeneral As Panel
  Friend WithEvents Label3 As Label
  Friend WithEvents txtLineasObjetivo As TextBox
  Friend WithEvents txtLineasHalcon As TextBox
  Friend WithEvents txtClientes As TextBox
  Friend WithEvents txtScoreCard As TextBox
  Friend WithEvents txtMinimoparabono As TextBox
  Friend WithEvents Label11 As Label
  Friend WithEvents Label8 As Label
  Friend WithEvents Label6 As Label
  Friend WithEvents Label5 As Label
  Friend WithEvents Label4 As Label
  Friend WithEvents Label1 As Label
  Friend WithEvents btnGrabaGenerales As Button
  Friend WithEvents MenuStrip1 As MenuStrip
  Friend WithEvents SalirToolStripMenuItem As ToolStripMenuItem
  Friend WithEvents btnEliminarRegistro As Button
  Friend WithEvents btnGrabaDetalles As Button
  Friend WithEvents CBAño As ComboBox
  Friend WithEvents Label10 As Label
  Friend WithEvents Label29 As Label
  Friend WithEvents CBMES As ComboBox
 Friend WithEvents Button1 As Button
End Class
