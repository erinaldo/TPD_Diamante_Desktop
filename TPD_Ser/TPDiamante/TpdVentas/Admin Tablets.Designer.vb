<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Admin_Tablets
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
  Me.components = New System.ComponentModel.Container()
  Me.PanelControles = New System.Windows.Forms.Panel()
  Me.cmbAgente = New System.Windows.Forms.ComboBox()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.panel_maps = New System.Windows.Forms.Panel()
  Me.lblzoom = New System.Windows.Forms.Label()
  Me.trackZoom = New System.Windows.Forms.TrackBar()
  Me.rdbtnSatelite = New System.Windows.Forms.RadioButton()
  Me.rdbtnNormal = New System.Windows.Forms.RadioButton()
  Me.chkBox_Mostrar_coordenadas = New System.Windows.Forms.CheckBox()
  Me.PanelInformacion = New System.Windows.Forms.Panel()
  Me.Panel6 = New System.Windows.Forms.Panel()
  Me.lblLng = New System.Windows.Forms.Label()
  Me.lblLat = New System.Windows.Forms.Label()
  Me.Label10 = New System.Windows.Forms.Label()
  Me.Label9 = New System.Windows.Forms.Label()
  Me.gMapa = New GMap.NET.WindowsForms.GMapControl()
  Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
  Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.lblMAC = New System.Windows.Forms.Label()
  Me.lblUA = New System.Windows.Forms.Label()
  Me.lblVer = New System.Windows.Forms.Label()
  Me.timer = New System.Windows.Forms.Timer(Me.components)
  Me.PanelControles.SuspendLayout()
  Me.panel_maps.SuspendLayout()
  CType(Me.trackZoom, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.PanelInformacion.SuspendLayout()
  Me.Panel6.SuspendLayout()
  Me.MenuStrip1.SuspendLayout()
  Me.SuspendLayout()
  '
  'PanelControles
  '
  Me.PanelControles.Controls.Add(Me.lblVer)
  Me.PanelControles.Controls.Add(Me.lblUA)
  Me.PanelControles.Controls.Add(Me.lblMAC)
  Me.PanelControles.Controls.Add(Me.Label3)
  Me.PanelControles.Controls.Add(Me.Label2)
  Me.PanelControles.Controls.Add(Me.Label1)
  Me.PanelControles.Controls.Add(Me.cmbAgente)
  Me.PanelControles.Controls.Add(Me.Label4)
  Me.PanelControles.Controls.Add(Me.panel_maps)
  Me.PanelControles.Dock = System.Windows.Forms.DockStyle.Left
  Me.PanelControles.Location = New System.Drawing.Point(0, 24)
  Me.PanelControles.Name = "PanelControles"
  Me.PanelControles.Size = New System.Drawing.Size(239, 740)
  Me.PanelControles.TabIndex = 0
  '
  'cmbAgente
  '
  Me.cmbAgente.DisplayMember = "MAURICIO CHABLÉ"
  Me.cmbAgente.FormattingEnabled = True
  Me.cmbAgente.Location = New System.Drawing.Point(15, 72)
  Me.cmbAgente.Name = "cmbAgente"
  Me.cmbAgente.Size = New System.Drawing.Size(197, 21)
  Me.cmbAgente.TabIndex = 199
  '
  'Label4
  '
  Me.Label4.AutoSize = True
  Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label4.Location = New System.Drawing.Point(12, 45)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(95, 13)
  Me.Label4.TabIndex = 198
  Me.Label4.Text = "Tableta usuario"
  '
  'panel_maps
  '
  Me.panel_maps.Controls.Add(Me.lblzoom)
  Me.panel_maps.Controls.Add(Me.trackZoom)
  Me.panel_maps.Controls.Add(Me.rdbtnSatelite)
  Me.panel_maps.Controls.Add(Me.rdbtnNormal)
  Me.panel_maps.Controls.Add(Me.chkBox_Mostrar_coordenadas)
  Me.panel_maps.Dock = System.Windows.Forms.DockStyle.Bottom
  Me.panel_maps.Location = New System.Drawing.Point(0, 560)
  Me.panel_maps.Name = "panel_maps"
  Me.panel_maps.Size = New System.Drawing.Size(239, 180)
  Me.panel_maps.TabIndex = 197
  '
  'lblzoom
  '
  Me.lblzoom.Location = New System.Drawing.Point(8, 72)
  Me.lblzoom.Name = "lblzoom"
  Me.lblzoom.Size = New System.Drawing.Size(20, 23)
  Me.lblzoom.TabIndex = 202
  Me.lblzoom.Text = "00"
  '
  'trackZoom
  '
  Me.trackZoom.Location = New System.Drawing.Point(27, 72)
  Me.trackZoom.Maximum = 20
  Me.trackZoom.Name = "trackZoom"
  Me.trackZoom.Size = New System.Drawing.Size(204, 45)
  Me.trackZoom.TabIndex = 201
  Me.trackZoom.Value = 10
  '
  'rdbtnSatelite
  '
  Me.rdbtnSatelite.AutoSize = True
  Me.rdbtnSatelite.Location = New System.Drawing.Point(121, 39)
  Me.rdbtnSatelite.Name = "rdbtnSatelite"
  Me.rdbtnSatelite.Size = New System.Drawing.Size(60, 17)
  Me.rdbtnSatelite.TabIndex = 200
  Me.rdbtnSatelite.TabStop = True
  Me.rdbtnSatelite.Text = "Satélite"
  Me.rdbtnSatelite.UseVisualStyleBackColor = True
  '
  'rdbtnNormal
  '
  Me.rdbtnNormal.AutoSize = True
  Me.rdbtnNormal.Location = New System.Drawing.Point(32, 39)
  Me.rdbtnNormal.Name = "rdbtnNormal"
  Me.rdbtnNormal.Size = New System.Drawing.Size(58, 17)
  Me.rdbtnNormal.TabIndex = 199
  Me.rdbtnNormal.TabStop = True
  Me.rdbtnNormal.Text = "Normal"
  Me.rdbtnNormal.UseVisualStyleBackColor = True
  '
  'chkBox_Mostrar_coordenadas
  '
  Me.chkBox_Mostrar_coordenadas.AutoSize = True
  Me.chkBox_Mostrar_coordenadas.Location = New System.Drawing.Point(32, 3)
  Me.chkBox_Mostrar_coordenadas.Name = "chkBox_Mostrar_coordenadas"
  Me.chkBox_Mostrar_coordenadas.Size = New System.Drawing.Size(126, 17)
  Me.chkBox_Mostrar_coordenadas.TabIndex = 198
  Me.chkBox_Mostrar_coordenadas.Text = "Mostrar coordenadas"
  Me.chkBox_Mostrar_coordenadas.UseVisualStyleBackColor = True
  '
  'PanelInformacion
  '
  Me.PanelInformacion.Controls.Add(Me.Panel6)
  Me.PanelInformacion.Controls.Add(Me.gMapa)
  Me.PanelInformacion.Dock = System.Windows.Forms.DockStyle.Fill
  Me.PanelInformacion.Location = New System.Drawing.Point(239, 24)
  Me.PanelInformacion.Name = "PanelInformacion"
  Me.PanelInformacion.Size = New System.Drawing.Size(1034, 740)
  Me.PanelInformacion.TabIndex = 1
  '
  'Panel6
  '
  Me.Panel6.Controls.Add(Me.lblLng)
  Me.Panel6.Controls.Add(Me.lblLat)
  Me.Panel6.Controls.Add(Me.Label10)
  Me.Panel6.Controls.Add(Me.Label9)
  Me.Panel6.Location = New System.Drawing.Point(2, 1)
  Me.Panel6.Name = "Panel6"
  Me.Panel6.Size = New System.Drawing.Size(167, 53)
  Me.Panel6.TabIndex = 197
  '
  'lblLng
  '
  Me.lblLng.BackColor = System.Drawing.Color.DimGray
  Me.lblLng.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblLng.ForeColor = System.Drawing.Color.White
  Me.lblLng.Location = New System.Drawing.Point(55, 29)
  Me.lblLng.Name = "lblLng"
  Me.lblLng.Size = New System.Drawing.Size(100, 16)
  Me.lblLng.TabIndex = 5
  '
  'lblLat
  '
  Me.lblLat.BackColor = System.Drawing.Color.DimGray
  Me.lblLat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblLat.ForeColor = System.Drawing.Color.White
  Me.lblLat.Location = New System.Drawing.Point(55, 6)
  Me.lblLat.Name = "lblLat"
  Me.lblLat.Size = New System.Drawing.Size(100, 16)
  Me.lblLat.TabIndex = 4
  '
  'Label10
  '
  Me.Label10.Location = New System.Drawing.Point(3, 26)
  Me.Label10.Name = "Label10"
  Me.Label10.Size = New System.Drawing.Size(55, 23)
  Me.Label10.TabIndex = 2
  Me.Label10.Text = "Longitud: "
  Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label9
  '
  Me.Label9.Location = New System.Drawing.Point(3, 3)
  Me.Label9.Name = "Label9"
  Me.Label9.Size = New System.Drawing.Size(55, 23)
  Me.Label9.TabIndex = 1
  Me.Label9.Text = "Latitud: "
  Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'gMapa
  '
  Me.gMapa.Bearing = 0!
  Me.gMapa.CanDragMap = True
  Me.gMapa.Dock = System.Windows.Forms.DockStyle.Fill
  Me.gMapa.EmptyTileColor = System.Drawing.Color.Navy
  Me.gMapa.GrayScaleMode = False
  Me.gMapa.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow
  Me.gMapa.LevelsKeepInMemmory = 5
  Me.gMapa.Location = New System.Drawing.Point(0, 0)
  Me.gMapa.MarkersEnabled = True
  Me.gMapa.MaxZoom = 2
  Me.gMapa.MinZoom = 2
  Me.gMapa.MouseWheelZoomEnabled = True
  Me.gMapa.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter
  Me.gMapa.Name = "gMapa"
  Me.gMapa.NegativeMode = False
  Me.gMapa.PolygonsEnabled = True
  Me.gMapa.RetryLoadTile = 0
  Me.gMapa.RoutesEnabled = True
  Me.gMapa.ScaleMode = GMap.NET.WindowsForms.ScaleModes.[Integer]
  Me.gMapa.SelectedAreaFillColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(225, Byte), Integer))
  Me.gMapa.ShowTileGridLines = False
  Me.gMapa.Size = New System.Drawing.Size(1034, 740)
  Me.gMapa.TabIndex = 195
  Me.gMapa.Zoom = 0R
  '
  'MenuStrip1
  '
  Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalirToolStripMenuItem})
  Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
  Me.MenuStrip1.Name = "MenuStrip1"
  Me.MenuStrip1.Size = New System.Drawing.Size(1273, 24)
  Me.MenuStrip1.TabIndex = 2
  Me.MenuStrip1.Text = "MenuStrip1"
  '
  'SalirToolStripMenuItem
  '
  Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
  Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
  Me.SalirToolStripMenuItem.Text = "&Salir"
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(12, 124)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(76, 13)
  Me.Label1.TabIndex = 200
  Me.Label1.Text = "Codigo MAC"
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(12, 184)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(87, 13)
  Me.Label2.TabIndex = 201
  Me.Label2.Text = "Ultimo acceso"
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(12, 249)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(138, 13)
  Me.Label3.TabIndex = 202
  Me.Label3.Text = "Versión actual el tablet"
  '
  'lblMAC
  '
  Me.lblMAC.AutoEllipsis = True
  Me.lblMAC.BackColor = System.Drawing.Color.White
  Me.lblMAC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
  Me.lblMAC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblMAC.Location = New System.Drawing.Point(15, 141)
  Me.lblMAC.Name = "lblMAC"
  Me.lblMAC.Size = New System.Drawing.Size(195, 18)
  Me.lblMAC.TabIndex = 203
  '
  'lblUA
  '
  Me.lblUA.AutoEllipsis = True
  Me.lblUA.BackColor = System.Drawing.Color.White
  Me.lblUA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
  Me.lblUA.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblUA.Location = New System.Drawing.Point(18, 201)
  Me.lblUA.Name = "lblUA"
  Me.lblUA.Size = New System.Drawing.Size(195, 18)
  Me.lblUA.TabIndex = 204
  '
  'lblVer
  '
  Me.lblVer.AutoEllipsis = True
  Me.lblVer.BackColor = System.Drawing.Color.White
  Me.lblVer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
  Me.lblVer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblVer.Location = New System.Drawing.Point(18, 266)
  Me.lblVer.Name = "lblVer"
  Me.lblVer.Size = New System.Drawing.Size(45, 18)
  Me.lblVer.TabIndex = 205
  '
  'timer
  '
  Me.timer.Enabled = True
  Me.timer.Interval = 5000
  '
  'Admin_Tablets
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1273, 764)
  Me.Controls.Add(Me.PanelInformacion)
  Me.Controls.Add(Me.PanelControles)
  Me.Controls.Add(Me.MenuStrip1)
  Me.MainMenuStrip = Me.MenuStrip1
  Me.Name = "Admin_Tablets"
  Me.Text = "Administración de tabletas"
  Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
  Me.PanelControles.ResumeLayout(False)
  Me.PanelControles.PerformLayout()
  Me.panel_maps.ResumeLayout(False)
  Me.panel_maps.PerformLayout()
  CType(Me.trackZoom, System.ComponentModel.ISupportInitialize).EndInit()
  Me.PanelInformacion.ResumeLayout(False)
  Me.Panel6.ResumeLayout(False)
  Me.MenuStrip1.ResumeLayout(False)
  Me.MenuStrip1.PerformLayout()
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub

 Friend WithEvents PanelControles As Panel
 Friend WithEvents PanelInformacion As Panel
 Friend WithEvents MenuStrip1 As MenuStrip
 Friend WithEvents SalirToolStripMenuItem As ToolStripMenuItem
 Friend WithEvents panel_maps As Panel
 Friend WithEvents lblzoom As Label
 Friend WithEvents trackZoom As TrackBar
 Friend WithEvents rdbtnSatelite As RadioButton
 Friend WithEvents rdbtnNormal As RadioButton
 Friend WithEvents chkBox_Mostrar_coordenadas As CheckBox
 Friend WithEvents gMapa As GMap.NET.WindowsForms.GMapControl
 Friend WithEvents Panel6 As Panel
 Friend WithEvents lblLng As Label
 Friend WithEvents lblLat As Label
 Friend WithEvents Label10 As Label
 Friend WithEvents Label9 As Label
 Friend WithEvents cmbAgente As ComboBox
 Friend WithEvents Label4 As Label
 Friend WithEvents Label3 As Label
 Friend WithEvents Label2 As Label
 Friend WithEvents Label1 As Label
 Friend WithEvents lblVer As Label
 Friend WithEvents lblUA As Label
 Friend WithEvents lblMAC As Label
 Friend WithEvents timer As Timer
End Class
