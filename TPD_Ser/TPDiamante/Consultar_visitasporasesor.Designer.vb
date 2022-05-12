<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Consultar_visitasporasesor
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Consultar_visitasporasesor))
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.GroupBox1 = New System.Windows.Forms.GroupBox()
  Me.panel_controles = New System.Windows.Forms.Panel()
  Me.EnviarMail = New System.Windows.Forms.Button()
  Me.Button2 = New System.Windows.Forms.Button()
  Me.Button1 = New System.Windows.Forms.Button()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.picCaptura = New System.Windows.Forms.PictureBox()
  Me.panel_maps = New System.Windows.Forms.Panel()
  Me.tb_infvisitas = New System.Windows.Forms.TrackBar()
  Me.lblzoom = New System.Windows.Forms.Label()
  Me.trackZoom = New System.Windows.Forms.TrackBar()
  Me.rdbtnSatelite = New System.Windows.Forms.RadioButton()
  Me.rdbtnNormal = New System.Windows.Forms.RadioButton()
  Me.chkBox_Mostrar_coordenadas = New System.Windows.Forms.CheckBox()
  Me.dtgv_detalles = New System.Windows.Forms.DataGridView()
  Me.chkbox_MostrarSoloSeleccioado = New System.Windows.Forms.CheckBox()
  Me.PBStatus = New System.Windows.Forms.ProgressBar()
  Me.PBExportacion = New System.Windows.Forms.ProgressBar()
  Me.Label7 = New System.Windows.Forms.Label()
  Me.Panel5 = New System.Windows.Forms.Panel()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.Panel3 = New System.Windows.Forms.Panel()
  Me.cmbAgente = New System.Windows.Forms.ComboBox()
  Me.lblfin = New System.Windows.Forms.Label()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.lblnv = New System.Windows.Forms.Label()
  Me.Label21 = New System.Windows.Forms.Label()
  Me.lblPormedioVisita = New System.Windows.Forms.Label()
  Me.Label19 = New System.Windows.Forms.Label()
  Me.lblInicio = New System.Windows.Forms.Label()
  Me.Label17 = New System.Windows.Forms.Label()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.dtpFin = New System.Windows.Forms.DateTimePicker()
  Me.dtpInicio = New System.Windows.Forms.DateTimePicker()
  Me.Panel2 = New System.Windows.Forms.Panel()
  Me.Panel4 = New System.Windows.Forms.Panel()
  Me.Panel6 = New System.Windows.Forms.Panel()
  Me.lblLng = New System.Windows.Forms.Label()
  Me.lblLat = New System.Windows.Forms.Label()
  Me.lblRow = New System.Windows.Forms.Label()
  Me.Label10 = New System.Windows.Forms.Label()
  Me.Label9 = New System.Windows.Forms.Label()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.gMapVisitas = New GMap.NET.WindowsForms.GMapControl()
  Me.dgvVisitas = New System.Windows.Forms.DataGridView()
  Me.Panel1.SuspendLayout()
  Me.GroupBox1.SuspendLayout()
  Me.panel_controles.SuspendLayout()
  CType(Me.picCaptura, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.panel_maps.SuspendLayout()
  CType(Me.tb_infvisitas, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.trackZoom, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.dtgv_detalles, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.Panel2.SuspendLayout()
  Me.Panel4.SuspendLayout()
  Me.Panel6.SuspendLayout()
  CType(Me.dgvVisitas, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'Panel1
  '
  Me.Panel1.Controls.Add(Me.GroupBox1)
  Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
  Me.Panel1.Location = New System.Drawing.Point(0, 0)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(257, 903)
  Me.Panel1.TabIndex = 7
  '
  'GroupBox1
  '
  Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
  Me.GroupBox1.Controls.Add(Me.panel_controles)
  Me.GroupBox1.Controls.Add(Me.picCaptura)
  Me.GroupBox1.Controls.Add(Me.panel_maps)
  Me.GroupBox1.Controls.Add(Me.PBStatus)
  Me.GroupBox1.Controls.Add(Me.PBExportacion)
  Me.GroupBox1.Controls.Add(Me.Label7)
  Me.GroupBox1.Controls.Add(Me.Panel5)
  Me.GroupBox1.Controls.Add(Me.Label1)
  Me.GroupBox1.Controls.Add(Me.Panel3)
  Me.GroupBox1.Controls.Add(Me.cmbAgente)
  Me.GroupBox1.Controls.Add(Me.lblfin)
  Me.GroupBox1.Controls.Add(Me.Label5)
  Me.GroupBox1.Controls.Add(Me.lblnv)
  Me.GroupBox1.Controls.Add(Me.Label21)
  Me.GroupBox1.Controls.Add(Me.lblPormedioVisita)
  Me.GroupBox1.Controls.Add(Me.Label19)
  Me.GroupBox1.Controls.Add(Me.lblInicio)
  Me.GroupBox1.Controls.Add(Me.Label17)
  Me.GroupBox1.Controls.Add(Me.Label4)
  Me.GroupBox1.Controls.Add(Me.Label3)
  Me.GroupBox1.Controls.Add(Me.Label2)
  Me.GroupBox1.Controls.Add(Me.dtpFin)
  Me.GroupBox1.Controls.Add(Me.dtpInicio)
  Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
  Me.GroupBox1.Name = "GroupBox1"
  Me.GroupBox1.Size = New System.Drawing.Size(254, 868)
  Me.GroupBox1.TabIndex = 2
  Me.GroupBox1.TabStop = False
  '
  'panel_controles
  '
  Me.panel_controles.Controls.Add(Me.EnviarMail)
  Me.panel_controles.Controls.Add(Me.Button2)
  Me.panel_controles.Controls.Add(Me.Button1)
  Me.panel_controles.Controls.Add(Me.Label8)
  Me.panel_controles.Location = New System.Drawing.Point(12, 397)
  Me.panel_controles.Name = "panel_controles"
  Me.panel_controles.Size = New System.Drawing.Size(239, 44)
  Me.panel_controles.TabIndex = 199
  '
  'EnviarMail
  '
  Me.EnviarMail.BackgroundImage = CType(resources.GetObject("EnviarMail.BackgroundImage"), System.Drawing.Image)
  Me.EnviarMail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
  Me.EnviarMail.Location = New System.Drawing.Point(167, 4)
  Me.EnviarMail.Name = "EnviarMail"
  Me.EnviarMail.Size = New System.Drawing.Size(39, 33)
  Me.EnviarMail.TabIndex = 202
  Me.EnviarMail.UseVisualStyleBackColor = True
  '
  'Button2
  '
  Me.Button2.BackgroundImage = Global.TPDiamante.My.Resources.Resources.Excel
  Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
  Me.Button2.Location = New System.Drawing.Point(122, 5)
  Me.Button2.Name = "Button2"
  Me.Button2.Size = New System.Drawing.Size(34, 34)
  Me.Button2.TabIndex = 201
  Me.Button2.UseVisualStyleBackColor = True
  '
  'Button1
  '
  Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button1.Location = New System.Drawing.Point(32, 6)
  Me.Button1.Name = "Button1"
  Me.Button1.Size = New System.Drawing.Size(73, 34)
  Me.Button1.TabIndex = 200
  Me.Button1.Text = "Consultar"
  Me.Button1.UseVisualStyleBackColor = True
  '
  'Label8
  '
  Me.Label8.AutoSize = True
  Me.Label8.Location = New System.Drawing.Point(26, 1)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(0, 13)
  Me.Label8.TabIndex = 29
  '
  'picCaptura
  '
  Me.picCaptura.Location = New System.Drawing.Point(159, 306)
  Me.picCaptura.Name = "picCaptura"
  Me.picCaptura.Size = New System.Drawing.Size(78, 38)
  Me.picCaptura.TabIndex = 198
  Me.picCaptura.TabStop = False
  Me.picCaptura.Visible = False
  '
  'panel_maps
  '
  Me.panel_maps.Controls.Add(Me.tb_infvisitas)
  Me.panel_maps.Controls.Add(Me.lblzoom)
  Me.panel_maps.Controls.Add(Me.trackZoom)
  Me.panel_maps.Controls.Add(Me.rdbtnSatelite)
  Me.panel_maps.Controls.Add(Me.rdbtnNormal)
  Me.panel_maps.Controls.Add(Me.chkBox_Mostrar_coordenadas)
  Me.panel_maps.Controls.Add(Me.dtgv_detalles)
  Me.panel_maps.Controls.Add(Me.chkbox_MostrarSoloSeleccioado)
  Me.panel_maps.Location = New System.Drawing.Point(10, 487)
  Me.panel_maps.Name = "panel_maps"
  Me.panel_maps.Size = New System.Drawing.Size(238, 377)
  Me.panel_maps.TabIndex = 196
  '
  'tb_infvisitas
  '
  Me.tb_infvisitas.LargeChange = 1
  Me.tb_infvisitas.Location = New System.Drawing.Point(1, 330)
  Me.tb_infvisitas.Name = "tb_infvisitas"
  Me.tb_infvisitas.Size = New System.Drawing.Size(234, 45)
  Me.tb_infvisitas.TabIndex = 203
  '
  'lblzoom
  '
  Me.lblzoom.Location = New System.Drawing.Point(8, 295)
  Me.lblzoom.Name = "lblzoom"
  Me.lblzoom.Size = New System.Drawing.Size(20, 23)
  Me.lblzoom.TabIndex = 202
  Me.lblzoom.Text = "00"
  '
  'trackZoom
  '
  Me.trackZoom.Location = New System.Drawing.Point(27, 295)
  Me.trackZoom.Maximum = 20
  Me.trackZoom.Name = "trackZoom"
  Me.trackZoom.Size = New System.Drawing.Size(204, 45)
  Me.trackZoom.TabIndex = 201
  Me.trackZoom.Value = 10
  '
  'rdbtnSatelite
  '
  Me.rdbtnSatelite.AutoSize = True
  Me.rdbtnSatelite.Location = New System.Drawing.Point(121, 267)
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
  Me.rdbtnNormal.Location = New System.Drawing.Point(32, 267)
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
  'dtgv_detalles
  '
  Me.dtgv_detalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dtgv_detalles.Location = New System.Drawing.Point(8, 49)
  Me.dtgv_detalles.Name = "dtgv_detalles"
  Me.dtgv_detalles.Size = New System.Drawing.Size(223, 211)
  Me.dtgv_detalles.TabIndex = 197
  '
  'chkbox_MostrarSoloSeleccioado
  '
  Me.chkbox_MostrarSoloSeleccioado.AutoSize = True
  Me.chkbox_MostrarSoloSeleccioado.Location = New System.Drawing.Point(32, 26)
  Me.chkbox_MostrarSoloSeleccioado.Name = "chkbox_MostrarSoloSeleccioado"
  Me.chkbox_MostrarSoloSeleccioado.Size = New System.Drawing.Size(149, 17)
  Me.chkbox_MostrarSoloSeleccioado.TabIndex = 196
  Me.chkbox_MostrarSoloSeleccioado.Text = "Mostrar solo seleccionado"
  Me.chkbox_MostrarSoloSeleccioado.UseVisualStyleBackColor = True
  '
  'PBStatus
  '
  Me.PBStatus.Location = New System.Drawing.Point(25, 439)
  Me.PBStatus.Name = "PBStatus"
  Me.PBStatus.Size = New System.Drawing.Size(190, 21)
  Me.PBStatus.TabIndex = 192
  '
  'PBExportacion
  '
  Me.PBExportacion.Location = New System.Drawing.Point(25, 458)
  Me.PBExportacion.Name = "PBExportacion"
  Me.PBExportacion.Size = New System.Drawing.Size(190, 23)
  Me.PBExportacion.Style = System.Windows.Forms.ProgressBarStyle.Continuous
  Me.PBExportacion.TabIndex = 191
  '
  'Label7
  '
  Me.Label7.AutoSize = True
  Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label7.Location = New System.Drawing.Point(41, 381)
  Me.Label7.Name = "Label7"
  Me.Label7.Size = New System.Drawing.Size(115, 13)
  Me.Label7.TabIndex = 28
  Me.Label7.Text = "Lugar desconocido"
  Me.Label7.Visible = False
  '
  'Panel5
  '
  Me.Panel5.BackColor = System.Drawing.Color.Yellow
  Me.Panel5.Location = New System.Drawing.Point(18, 381)
  Me.Panel5.Name = "Panel5"
  Me.Panel5.Size = New System.Drawing.Size(17, 13)
  Me.Panel5.TabIndex = 27
  Me.Panel5.Visible = False
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(41, 358)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(95, 13)
  Me.Label1.TabIndex = 26
  Me.Label1.Text = "Lugar conocido"
  Me.Label1.Visible = False
  '
  'Panel3
  '
  Me.Panel3.BackColor = System.Drawing.Color.Lime
  Me.Panel3.Location = New System.Drawing.Point(18, 358)
  Me.Panel3.Name = "Panel3"
  Me.Panel3.Size = New System.Drawing.Size(17, 13)
  Me.Panel3.TabIndex = 25
  Me.Panel3.Visible = False
  '
  'cmbAgente
  '
  Me.cmbAgente.DisplayMember = "MAURICIO CHABLÉ"
  Me.cmbAgente.FormattingEnabled = True
  Me.cmbAgente.Location = New System.Drawing.Point(18, 105)
  Me.cmbAgente.Name = "cmbAgente"
  Me.cmbAgente.Size = New System.Drawing.Size(197, 21)
  Me.cmbAgente.TabIndex = 24
  '
  'lblfin
  '
  Me.lblfin.AutoSize = True
  Me.lblfin.Location = New System.Drawing.Point(15, 229)
  Me.lblfin.Name = "lblfin"
  Me.lblfin.Size = New System.Drawing.Size(49, 13)
  Me.lblfin.TabIndex = 23
  Me.lblfin.Text = "00:00:00"
  '
  'Label5
  '
  Me.Label5.AutoSize = True
  Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label5.Location = New System.Drawing.Point(12, 205)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(131, 15)
  Me.Label5.TabIndex = 22
  Me.Label5.Text = "Hora de fin de ruta:"
  '
  'lblnv
  '
  Me.lblnv.AutoSize = True
  Me.lblnv.Location = New System.Drawing.Point(12, 331)
  Me.lblnv.Name = "lblnv"
  Me.lblnv.Size = New System.Drawing.Size(13, 13)
  Me.lblnv.TabIndex = 21
  Me.lblnv.Text = "0"
  '
  'Label21
  '
  Me.Label21.AutoSize = True
  Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label21.Location = New System.Drawing.Point(12, 309)
  Me.Label21.Name = "Label21"
  Me.Label21.Size = New System.Drawing.Size(112, 13)
  Me.Label21.TabIndex = 20
  Me.Label21.Text = "Número de visitas:"
  '
  'lblPormedioVisita
  '
  Me.lblPormedioVisita.AutoSize = True
  Me.lblPormedioVisita.Location = New System.Drawing.Point(12, 283)
  Me.lblPormedioVisita.Name = "lblPormedioVisita"
  Me.lblPormedioVisita.Size = New System.Drawing.Size(49, 13)
  Me.lblPormedioVisita.TabIndex = 19
  Me.lblPormedioVisita.Text = "00:00:00"
  '
  'Label19
  '
  Me.Label19.AutoSize = True
  Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label19.Location = New System.Drawing.Point(12, 256)
  Me.Label19.Name = "Label19"
  Me.Label19.Size = New System.Drawing.Size(181, 15)
  Me.Label19.TabIndex = 18
  Me.Label19.Text = "Tiempo promedio de visita:"
  '
  'lblInicio
  '
  Me.lblInicio.AutoSize = True
  Me.lblInicio.Location = New System.Drawing.Point(15, 174)
  Me.lblInicio.Name = "lblInicio"
  Me.lblInicio.Size = New System.Drawing.Size(49, 13)
  Me.lblInicio.TabIndex = 17
  Me.lblInicio.Text = "00:00:00"
  '
  'Label17
  '
  Me.Label17.AutoSize = True
  Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label17.Location = New System.Drawing.Point(12, 150)
  Me.Label17.Name = "Label17"
  Me.Label17.Size = New System.Drawing.Size(150, 15)
  Me.Label17.TabIndex = 16
  Me.Label17.Text = "Hora de inicio de ruta:"
  '
  'Label4
  '
  Me.Label4.AutoSize = True
  Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label4.Location = New System.Drawing.Point(15, 78)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(105, 13)
  Me.Label4.TabIndex = 12
  Me.Label4.Text = "Asesor de ventas"
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(15, 3)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(44, 13)
  Me.Label3.TabIndex = 5
  Me.Label3.Text = "Hasta:"
  Me.Label3.Visible = False
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(15, 26)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(46, 13)
  Me.Label2.TabIndex = 4
  Me.Label2.Text = "Fecha:"
  '
  'dtpFin
  '
  Me.dtpFin.Location = New System.Drawing.Point(15, 19)
  Me.dtpFin.Name = "dtpFin"
  Me.dtpFin.Size = New System.Drawing.Size(200, 20)
  Me.dtpFin.TabIndex = 3
  Me.dtpFin.Visible = False
  '
  'dtpInicio
  '
  Me.dtpInicio.Location = New System.Drawing.Point(18, 42)
  Me.dtpInicio.Name = "dtpInicio"
  Me.dtpInicio.Size = New System.Drawing.Size(200, 20)
  Me.dtpInicio.TabIndex = 2
  '
  'Panel2
  '
  Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel2.Controls.Add(Me.dgvVisitas)
  Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
  Me.Panel2.Location = New System.Drawing.Point(257, 0)
  Me.Panel2.Name = "Panel2"
  Me.Panel2.Size = New System.Drawing.Size(1107, 400)
  Me.Panel2.TabIndex = 8
  '
  'Panel4
  '
  Me.Panel4.Controls.Add(Me.Panel6)
  Me.Panel4.Controls.Add(Me.gMapVisitas)
  Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Panel4.Location = New System.Drawing.Point(257, 400)
  Me.Panel4.Name = "Panel4"
  Me.Panel4.Size = New System.Drawing.Size(1107, 503)
  Me.Panel4.TabIndex = 9
  '
  'Panel6
  '
  Me.Panel6.Controls.Add(Me.lblLng)
  Me.Panel6.Controls.Add(Me.lblLat)
  Me.Panel6.Controls.Add(Me.lblRow)
  Me.Panel6.Controls.Add(Me.Label10)
  Me.Panel6.Controls.Add(Me.Label9)
  Me.Panel6.Controls.Add(Me.Label6)
  Me.Panel6.Location = New System.Drawing.Point(2, 1)
  Me.Panel6.Name = "Panel6"
  Me.Panel6.Size = New System.Drawing.Size(167, 90)
  Me.Panel6.TabIndex = 195
  '
  'lblLng
  '
  Me.lblLng.BackColor = System.Drawing.Color.DimGray
  Me.lblLng.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblLng.ForeColor = System.Drawing.Color.White
  Me.lblLng.Location = New System.Drawing.Point(55, 60)
  Me.lblLng.Name = "lblLng"
  Me.lblLng.Size = New System.Drawing.Size(100, 16)
  Me.lblLng.TabIndex = 5
  '
  'lblLat
  '
  Me.lblLat.BackColor = System.Drawing.Color.DimGray
  Me.lblLat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblLat.ForeColor = System.Drawing.Color.White
  Me.lblLat.Location = New System.Drawing.Point(55, 37)
  Me.lblLat.Name = "lblLat"
  Me.lblLat.Size = New System.Drawing.Size(100, 16)
  Me.lblLat.TabIndex = 4
  '
  'lblRow
  '
  Me.lblRow.BackColor = System.Drawing.Color.DimGray
  Me.lblRow.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblRow.ForeColor = System.Drawing.Color.White
  Me.lblRow.Location = New System.Drawing.Point(55, 9)
  Me.lblRow.Name = "lblRow"
  Me.lblRow.Size = New System.Drawing.Size(100, 16)
  Me.lblRow.TabIndex = 3
  '
  'Label10
  '
  Me.Label10.Location = New System.Drawing.Point(3, 57)
  Me.Label10.Name = "Label10"
  Me.Label10.Size = New System.Drawing.Size(55, 23)
  Me.Label10.TabIndex = 2
  Me.Label10.Text = "Longitud: "
  Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label9
  '
  Me.Label9.Location = New System.Drawing.Point(3, 34)
  Me.Label9.Name = "Label9"
  Me.Label9.Size = New System.Drawing.Size(55, 23)
  Me.Label9.TabIndex = 1
  Me.Label9.Text = "Latitud: "
  Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label6
  '
  Me.Label6.Location = New System.Drawing.Point(3, 5)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(55, 23)
  Me.Label6.TabIndex = 0
  Me.Label6.Text = "Renglón:"
  Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'gMapVisitas
  '
  Me.gMapVisitas.Bearing = 0!
  Me.gMapVisitas.CanDragMap = True
  Me.gMapVisitas.Dock = System.Windows.Forms.DockStyle.Fill
  Me.gMapVisitas.EmptyTileColor = System.Drawing.Color.Navy
  Me.gMapVisitas.GrayScaleMode = False
  Me.gMapVisitas.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow
  Me.gMapVisitas.LevelsKeepInMemmory = 5
  Me.gMapVisitas.Location = New System.Drawing.Point(0, 0)
  Me.gMapVisitas.MarkersEnabled = True
  Me.gMapVisitas.MaxZoom = 2
  Me.gMapVisitas.MinZoom = 2
  Me.gMapVisitas.MouseWheelZoomEnabled = True
  Me.gMapVisitas.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter
  Me.gMapVisitas.Name = "gMapVisitas"
  Me.gMapVisitas.NegativeMode = False
  Me.gMapVisitas.PolygonsEnabled = True
  Me.gMapVisitas.RetryLoadTile = 0
  Me.gMapVisitas.RoutesEnabled = True
  Me.gMapVisitas.ScaleMode = GMap.NET.WindowsForms.ScaleModes.[Integer]
  Me.gMapVisitas.SelectedAreaFillColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(225, Byte), Integer))
  Me.gMapVisitas.ShowTileGridLines = False
  Me.gMapVisitas.Size = New System.Drawing.Size(1107, 503)
  Me.gMapVisitas.TabIndex = 194
  Me.gMapVisitas.Zoom = 0R
  '
  'dgvVisitas
  '
  Me.dgvVisitas.AllowDrop = True
  Me.dgvVisitas.AllowUserToAddRows = False
  Me.dgvVisitas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
  Me.dgvVisitas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders
  Me.dgvVisitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgvVisitas.Dock = System.Windows.Forms.DockStyle.Fill
  Me.dgvVisitas.Location = New System.Drawing.Point(0, 0)
  Me.dgvVisitas.Name = "dgvVisitas"
  Me.dgvVisitas.Size = New System.Drawing.Size(1103, 396)
  Me.dgvVisitas.TabIndex = 6
  '
  'Consultar_visitasporasesor
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.BackColor = System.Drawing.Color.White
  Me.ClientSize = New System.Drawing.Size(1364, 903)
  Me.Controls.Add(Me.Panel4)
  Me.Controls.Add(Me.Panel2)
  Me.Controls.Add(Me.Panel1)
  Me.Name = "Consultar_visitasporasesor"
  Me.Text = "Consultar_visitasporasesor"
  Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
  Me.Panel1.ResumeLayout(False)
  Me.GroupBox1.ResumeLayout(False)
  Me.GroupBox1.PerformLayout()
  Me.panel_controles.ResumeLayout(False)
  Me.panel_controles.PerformLayout()
  CType(Me.picCaptura, System.ComponentModel.ISupportInitialize).EndInit()
  Me.panel_maps.ResumeLayout(False)
  Me.panel_maps.PerformLayout()
  CType(Me.tb_infvisitas, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.trackZoom, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.dtgv_detalles, System.ComponentModel.ISupportInitialize).EndInit()
  Me.Panel2.ResumeLayout(False)
  Me.Panel4.ResumeLayout(False)
  Me.Panel6.ResumeLayout(False)
  CType(Me.dgvVisitas, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents Panel1 As Panel
 Friend WithEvents Panel2 As Panel
 Friend WithEvents Panel4 As Panel
 Friend WithEvents gMapVisitas As GMap.NET.WindowsForms.GMapControl
 Friend WithEvents GroupBox1 As GroupBox
 Friend WithEvents PBStatus As ProgressBar
 Friend WithEvents PBExportacion As ProgressBar
 Friend WithEvents Label8 As Label
 Friend WithEvents Label7 As Label
 Friend WithEvents Panel5 As Panel
 Friend WithEvents Label1 As Label
 Friend WithEvents Panel3 As Panel
 Friend WithEvents cmbAgente As ComboBox
 Private WithEvents lblfin As Label
 Private WithEvents Label5 As Label
 Private WithEvents lblnv As Label
 Private WithEvents Label21 As Label
 Private WithEvents lblPormedioVisita As Label
 Private WithEvents Label19 As Label
 Private WithEvents lblInicio As Label
 Private WithEvents Label17 As Label
 Friend WithEvents Label4 As Label
 Friend WithEvents Label3 As Label
 Friend WithEvents Label2 As Label
 Friend WithEvents dtpFin As DateTimePicker
 Friend WithEvents dtpInicio As DateTimePicker
 Friend WithEvents Panel6 As Panel
 Friend WithEvents lblLng As Label
 Friend WithEvents lblLat As Label
 Friend WithEvents lblRow As Label
 Friend WithEvents Label10 As Label
 Friend WithEvents Label9 As Label
 Friend WithEvents Label6 As Label
 Friend WithEvents panel_maps As Panel
 Friend WithEvents chkBox_Mostrar_coordenadas As CheckBox
 Friend WithEvents dtgv_detalles As DataGridView
 Friend WithEvents chkbox_MostrarSoloSeleccioado As CheckBox
 Friend WithEvents rdbtnSatelite As RadioButton
 Friend WithEvents rdbtnNormal As RadioButton
 Friend WithEvents lblzoom As Label
 Friend WithEvents trackZoom As TrackBar
 Friend WithEvents picCaptura As PictureBox
 Friend WithEvents panel_controles As Panel
 Friend WithEvents EnviarMail As Button
 Friend WithEvents Button2 As Button
 Friend WithEvents Button1 As Button
 Friend WithEvents tb_infvisitas As TrackBar
 Friend WithEvents dgvVisitas As DataGridView
End Class
