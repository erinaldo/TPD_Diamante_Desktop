<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmArticulosEspeciales
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmArticulosEspeciales))
  Me.Label1 = New System.Windows.Forms.Label()
  Me.dtpFechaIni = New System.Windows.Forms.DateTimePicker()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.dtpFechaFin = New System.Windows.Forms.DateTimePicker()
  Me.gbAcciones = New System.Windows.Forms.GroupBox()
  Me.DTP1 = New System.Windows.Forms.DateTimePicker()
  Me.cbFinalizadas = New System.Windows.Forms.CheckBox()
  Me.cbDevuelto = New System.Windows.Forms.CheckBox()
  Me.cbQuitados = New System.Windows.Forms.CheckBox()
  Me.btnExportar = New System.Windows.Forms.Button()
  Me.btnBuscar = New System.Windows.Forms.Button()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
  Me.dgvDetalleCompra = New System.Windows.Forms.DataGridView()
  Me.dgvContenido = New System.Windows.Forms.DataGridView()
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.GroupBox1 = New System.Windows.Forms.GroupBox()
  Me.GroupBox2 = New System.Windows.Forms.GroupBox()
  Me.cbCancelados = New System.Windows.Forms.CheckBox()
  Me.gbAcciones.SuspendLayout()
  CType(Me.dgvDetalleCompra, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.dgvContenido, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.Panel1.SuspendLayout()
  Me.GroupBox1.SuspendLayout()
  Me.GroupBox2.SuspendLayout()
  Me.SuspendLayout()
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(10, 42)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(81, 13)
  Me.Label1.TabIndex = 0
  Me.Label1.Text = "Fecha Inicio:"
  '
  'dtpFechaIni
  '
  Me.dtpFechaIni.Location = New System.Drawing.Point(97, 36)
  Me.dtpFechaIni.Name = "dtpFechaIni"
  Me.dtpFechaIni.Size = New System.Drawing.Size(222, 20)
  Me.dtpFechaIni.TabIndex = 1
  Me.dtpFechaIni.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(10, 69)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(77, 13)
  Me.Label2.TabIndex = 2
  Me.Label2.Text = "Fecha Final:"
  '
  'dtpFechaFin
  '
  Me.dtpFechaFin.Location = New System.Drawing.Point(97, 63)
  Me.dtpFechaFin.Name = "dtpFechaFin"
  Me.dtpFechaFin.Size = New System.Drawing.Size(222, 20)
  Me.dtpFechaFin.TabIndex = 3
  '
  'gbAcciones
  '
  Me.gbAcciones.Controls.Add(Me.cbCancelados)
  Me.gbAcciones.Controls.Add(Me.DTP1)
  Me.gbAcciones.Controls.Add(Me.cbFinalizadas)
  Me.gbAcciones.Controls.Add(Me.cbDevuelto)
  Me.gbAcciones.Controls.Add(Me.cbQuitados)
  Me.gbAcciones.Controls.Add(Me.btnExportar)
  Me.gbAcciones.Controls.Add(Me.btnBuscar)
  Me.gbAcciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.gbAcciones.Location = New System.Drawing.Point(492, 10)
  Me.gbAcciones.Name = "gbAcciones"
  Me.gbAcciones.Size = New System.Drawing.Size(778, 81)
  Me.gbAcciones.TabIndex = 4
  Me.gbAcciones.TabStop = False
  Me.gbAcciones.Text = "Acciones"
  '
  'DTP1
  '
  Me.DTP1.Location = New System.Drawing.Point(695, 11)
  Me.DTP1.Name = "DTP1"
  Me.DTP1.Size = New System.Drawing.Size(77, 20)
  Me.DTP1.TabIndex = 12
  Me.DTP1.Visible = False
  '
  'cbFinalizadas
  '
  Me.cbFinalizadas.AutoSize = True
  Me.cbFinalizadas.Location = New System.Drawing.Point(485, 26)
  Me.cbFinalizadas.Name = "cbFinalizadas"
  Me.cbFinalizadas.Size = New System.Drawing.Size(113, 17)
  Me.cbFinalizadas.TabIndex = 11
  Me.cbFinalizadas.Text = "Ver finalizadas."
  Me.cbFinalizadas.UseVisualStyleBackColor = True
  '
  'cbDevuelto
  '
  Me.cbDevuelto.AutoSize = True
  Me.cbDevuelto.Location = New System.Drawing.Point(243, 50)
  Me.cbDevuelto.Name = "cbDevuelto"
  Me.cbDevuelto.Size = New System.Drawing.Size(205, 17)
  Me.cbDevuelto.TabIndex = 9
  Me.cbDevuelto.Text = "Ver articulos con devoluciones."
  Me.cbDevuelto.UseVisualStyleBackColor = True
  '
  'cbQuitados
  '
  Me.cbQuitados.AutoSize = True
  Me.cbQuitados.Location = New System.Drawing.Point(243, 26)
  Me.cbQuitados.Name = "cbQuitados"
  Me.cbQuitados.Size = New System.Drawing.Size(153, 17)
  Me.cbQuitados.TabIndex = 10
  Me.cbQuitados.Text = "Ver articulos quitados."
  Me.cbQuitados.UseVisualStyleBackColor = True
  '
  'btnExportar
  '
  Me.btnExportar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.btnExportar.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
  Me.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
  Me.btnExportar.Location = New System.Drawing.Point(116, 26)
  Me.btnExportar.Name = "btnExportar"
  Me.btnExportar.Size = New System.Drawing.Size(91, 41)
  Me.btnExportar.TabIndex = 1
  Me.btnExportar.Text = "Exportar"
  Me.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  Me.btnExportar.UseVisualStyleBackColor = True
  '
  'btnBuscar
  '
  Me.btnBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.btnBuscar.Image = Global.TPDiamante.My.Resources.Resources.Refresh_B1
  Me.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
  Me.btnBuscar.Location = New System.Drawing.Point(10, 26)
  Me.btnBuscar.Name = "btnBuscar"
  Me.btnBuscar.Size = New System.Drawing.Size(100, 41)
  Me.btnBuscar.TabIndex = 0
  Me.btnBuscar.Text = "Actualizar"
  Me.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  Me.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
  Me.btnBuscar.UseVisualStyleBackColor = True
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(10, 7)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(104, 13)
  Me.Label3.TabIndex = 5
  Me.Label3.Text = "Periodo de fecha"
  '
  'DataGridViewTextBoxColumn1
  '
  Me.DataGridViewTextBoxColumn1.HeaderText = "Articulo"
  Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
  Me.DataGridViewTextBoxColumn1.ReadOnly = True
  '
  'DataGridViewTextBoxColumn2
  '
  Me.DataGridViewTextBoxColumn2.HeaderText = "Descripción"
  Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
  Me.DataGridViewTextBoxColumn2.ReadOnly = True
  '
  'DataGridViewTextBoxColumn3
  '
  Me.DataGridViewTextBoxColumn3.HeaderText = "Codigo Linea"
  Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
  Me.DataGridViewTextBoxColumn3.ReadOnly = True
  '
  'DataGridViewTextBoxColumn4
  '
  Me.DataGridViewTextBoxColumn4.HeaderText = "Linea"
  Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
  Me.DataGridViewTextBoxColumn4.ReadOnly = True
  '
  'DataGridViewTextBoxColumn5
  '
  Me.DataGridViewTextBoxColumn5.HeaderText = "Usuario Vta."
  Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
  Me.DataGridViewTextBoxColumn5.ReadOnly = True
  '
  'DataGridViewTextBoxColumn6
  '
  Me.DataGridViewTextBoxColumn6.HeaderText = "Orden Vta."
  Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
  Me.DataGridViewTextBoxColumn6.ReadOnly = True
  '
  'DataGridViewTextBoxColumn7
  '
  Me.DataGridViewTextBoxColumn7.HeaderText = "Fecha Creación"
  Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
  Me.DataGridViewTextBoxColumn7.ReadOnly = True
  '
  'DataGridViewTextBoxColumn8
  '
  Me.DataGridViewTextBoxColumn8.HeaderText = "Pedido del Cli."
  Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
  Me.DataGridViewTextBoxColumn8.ReadOnly = True
  '
  'DataGridViewTextBoxColumn9
  '
  Me.DataGridViewTextBoxColumn9.HeaderText = "Cliente"
  Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
  Me.DataGridViewTextBoxColumn9.ReadOnly = True
  '
  'DataGridViewTextBoxColumn10
  '
  Me.DataGridViewTextBoxColumn10.HeaderText = "Nombre"
  Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
  Me.DataGridViewTextBoxColumn10.ReadOnly = True
  '
  'DataGridViewTextBoxColumn11
  '
  Me.DataGridViewTextBoxColumn11.HeaderText = "Orden de Compra"
  Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
  Me.DataGridViewTextBoxColumn11.ReadOnly = True
  '
  'DataGridViewTextBoxColumn12
  '
  Me.DataGridViewTextBoxColumn12.HeaderText = "Solicitado compras"
  Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
  Me.DataGridViewTextBoxColumn12.ReadOnly = True
  '
  'DataGridViewTextBoxColumn13
  '
  Me.DataGridViewTextBoxColumn13.HeaderText = "# Entrada"
  Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
  Me.DataGridViewTextBoxColumn13.ReadOnly = True
  '
  'DataGridViewTextBoxColumn14
  '
  Me.DataGridViewTextBoxColumn14.HeaderText = "Cantidad recibida"
  Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
  Me.DataGridViewTextBoxColumn14.ReadOnly = True
  '
  'DataGridViewTextBoxColumn15
  '
  Me.DataGridViewTextBoxColumn15.HeaderText = "Cant. facturada"
  Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
  Me.DataGridViewTextBoxColumn15.ReadOnly = True
  '
  'DataGridViewTextBoxColumn16
  '
  Me.DataGridViewTextBoxColumn16.HeaderText = "Faltante x Facturar"
  Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
  Me.DataGridViewTextBoxColumn16.ReadOnly = True
  '
  'DataGridViewTextBoxColumn17
  '
  Me.DataGridViewTextBoxColumn17.HeaderText = "Facturación"
  Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
  Me.DataGridViewTextBoxColumn17.ReadOnly = True
  '
  'DataGridViewTextBoxColumn18
  '
  Me.DataGridViewTextBoxColumn18.HeaderText = "Devolución"
  Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
  Me.DataGridViewTextBoxColumn18.ReadOnly = True
  '
  'dgvDetalleCompra
  '
  Me.dgvDetalleCompra.AllowUserToAddRows = False
  Me.dgvDetalleCompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgvDetalleCompra.Dock = System.Windows.Forms.DockStyle.Fill
  Me.dgvDetalleCompra.Location = New System.Drawing.Point(3, 16)
  Me.dgvDetalleCompra.Name = "dgvDetalleCompra"
  Me.dgvDetalleCompra.RowHeadersWidth = 20
  Me.dgvDetalleCompra.Size = New System.Drawing.Size(1278, 65)
  Me.dgvDetalleCompra.TabIndex = 7
  '
  'dgvContenido
  '
  Me.dgvContenido.AllowUserToAddRows = False
  Me.dgvContenido.AllowUserToDeleteRows = False
  Me.dgvContenido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgvContenido.Dock = System.Windows.Forms.DockStyle.Fill
  Me.dgvContenido.Location = New System.Drawing.Point(3, 16)
  Me.dgvContenido.Name = "dgvContenido"
  Me.dgvContenido.RowHeadersWidth = 20
  Me.dgvContenido.Size = New System.Drawing.Size(1278, 428)
  Me.dgvContenido.TabIndex = 8
  '
  'Panel1
  '
  Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel1.Controls.Add(Me.Label3)
  Me.Panel1.Controls.Add(Me.Label1)
  Me.Panel1.Controls.Add(Me.dtpFechaIni)
  Me.Panel1.Controls.Add(Me.gbAcciones)
  Me.Panel1.Controls.Add(Me.Label2)
  Me.Panel1.Controls.Add(Me.dtpFechaFin)
  Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
  Me.Panel1.Location = New System.Drawing.Point(0, 0)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(1284, 99)
  Me.Panel1.TabIndex = 9
  '
  'GroupBox1
  '
  Me.GroupBox1.Controls.Add(Me.dgvContenido)
  Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
  Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
  Me.GroupBox1.Location = New System.Drawing.Point(0, 99)
  Me.GroupBox1.Name = "GroupBox1"
  Me.GroupBox1.Size = New System.Drawing.Size(1284, 447)
  Me.GroupBox1.TabIndex = 10
  Me.GroupBox1.TabStop = False
  '
  'GroupBox2
  '
  Me.GroupBox2.Controls.Add(Me.dgvDetalleCompra)
  Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
  Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
  Me.GroupBox2.Location = New System.Drawing.Point(0, 546)
  Me.GroupBox2.Name = "GroupBox2"
  Me.GroupBox2.Size = New System.Drawing.Size(1284, 84)
  Me.GroupBox2.TabIndex = 11
  Me.GroupBox2.TabStop = False
  '
  'cbCancelados
  '
  Me.cbCancelados.AutoSize = True
  Me.cbCancelados.Location = New System.Drawing.Point(485, 50)
  Me.cbCancelados.Name = "cbCancelados"
  Me.cbCancelados.Size = New System.Drawing.Size(178, 17)
  Me.cbCancelados.TabIndex = 13
  Me.cbCancelados.Text = "Ver solicitudes canceladas"
  Me.cbCancelados.UseVisualStyleBackColor = True
  '
  'frmArticulosEspeciales
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1284, 630)
  Me.Controls.Add(Me.GroupBox2)
  Me.Controls.Add(Me.GroupBox1)
  Me.Controls.Add(Me.Panel1)
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Name = "frmArticulosEspeciales"
  Me.Text = "Solicitudes de Articulos Especiales"
  Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
  Me.gbAcciones.ResumeLayout(False)
  Me.gbAcciones.PerformLayout()
  CType(Me.dgvDetalleCompra, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.dgvContenido, System.ComponentModel.ISupportInitialize).EndInit()
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  Me.GroupBox1.ResumeLayout(False)
  Me.GroupBox2.ResumeLayout(False)
  Me.ResumeLayout(False)

 End Sub

 Friend WithEvents Label1 As Label
 Friend WithEvents dtpFechaIni As DateTimePicker
 Friend WithEvents Label2 As Label
 Friend WithEvents dtpFechaFin As DateTimePicker
 Friend WithEvents gbAcciones As GroupBox
 Friend WithEvents btnExportar As Button
 Friend WithEvents btnBuscar As Button
 Friend WithEvents Label3 As Label
 Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents dgvDetalleCompra As System.Windows.Forms.DataGridView
 Friend WithEvents dgvContenido As System.Windows.Forms.DataGridView
 Friend WithEvents cbDevuelto As CheckBox
 Friend WithEvents cbQuitados As CheckBox
 Friend WithEvents cbFinalizadas As CheckBox
 Friend WithEvents Panel1 As Panel
 Friend WithEvents GroupBox1 As GroupBox
 Friend WithEvents GroupBox2 As GroupBox
 Friend WithEvents DTP1 As DateTimePicker
 Friend WithEvents cbCancelados As CheckBox
End Class
