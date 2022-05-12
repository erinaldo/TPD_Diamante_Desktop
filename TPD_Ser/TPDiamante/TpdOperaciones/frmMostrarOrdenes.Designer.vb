<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMostrarOrdenes
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
  Me.components = New System.ComponentModel.Container()
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMostrarOrdenes))
  Me.Label1 = New System.Windows.Forms.Label()
  Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.txtBuscarOrden = New System.Windows.Forms.TextBox()
  Me.dgvOrdenes = New System.Windows.Forms.DataGridView()
  Me.dgvDetalle = New System.Windows.Forms.DataGridView()
  Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
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
  Me.GroupBox1 = New System.Windows.Forms.GroupBox()
  Me.btn_PreClaves = New System.Windows.Forms.Button()
  Me.lblActualizando = New System.Windows.Forms.Label()
  Me.btnUpdate = New System.Windows.Forms.Button()
  Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
  CType(Me.dgvOrdenes, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.dgvDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.GroupBox1.SuspendLayout()
  CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SplitContainer1.Panel1.SuspendLayout()
  Me.SplitContainer1.Panel2.SuspendLayout()
  Me.SplitContainer1.SuspendLayout()
  Me.SuspendLayout()
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(6, 20)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(55, 16)
  Me.Label1.TabIndex = 0
  Me.Label1.Text = "Fecha:"
  '
  'dtpFecha
  '
  Me.dtpFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.dtpFecha.Location = New System.Drawing.Point(67, 15)
  Me.dtpFecha.Name = "dtpFecha"
  Me.dtpFecha.Size = New System.Drawing.Size(265, 22)
  Me.dtpFecha.TabIndex = 3
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(338, 20)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(106, 16)
  Me.Label2.TabIndex = 2
  Me.Label2.Text = "Buscar Orden:"
  '
  'txtBuscarOrden
  '
  Me.txtBuscarOrden.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtBuscarOrden.Location = New System.Drawing.Point(450, 17)
  Me.txtBuscarOrden.Name = "txtBuscarOrden"
  Me.txtBuscarOrden.Size = New System.Drawing.Size(221, 22)
  Me.txtBuscarOrden.TabIndex = 2
  '
  'dgvOrdenes
  '
  Me.dgvOrdenes.AllowUserToAddRows = False
  Me.dgvOrdenes.AllowUserToDeleteRows = False
  Me.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill
  Me.dgvOrdenes.Location = New System.Drawing.Point(0, 0)
  Me.dgvOrdenes.Name = "dgvOrdenes"
  Me.dgvOrdenes.RowHeadersWidth = 20
  Me.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
  Me.dgvOrdenes.Size = New System.Drawing.Size(987, 706)
  Me.dgvOrdenes.TabIndex = 1
  '
  'dgvDetalle
  '
  Me.dgvDetalle.AllowUserToAddRows = False
  Me.dgvDetalle.AllowUserToDeleteRows = False
  Me.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill
  Me.dgvDetalle.Location = New System.Drawing.Point(0, 0)
  Me.dgvDetalle.Name = "dgvDetalle"
  Me.dgvDetalle.RowHeadersWidth = 20
  Me.dgvDetalle.Size = New System.Drawing.Size(363, 706)
  Me.dgvDetalle.TabIndex = 4
  '
  'Timer1
  '
  Me.Timer1.Interval = 60000
  '
  'DataGridViewTextBoxColumn1
  '
  Me.DataGridViewTextBoxColumn1.HeaderText = "Orden Vta."
  Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
  Me.DataGridViewTextBoxColumn1.ReadOnly = True
  Me.DataGridViewTextBoxColumn1.Width = 70
  '
  'DataGridViewTextBoxColumn2
  '
  Me.DataGridViewTextBoxColumn2.HeaderText = "Fecha Doc."
  Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
  Me.DataGridViewTextBoxColumn2.ReadOnly = True
  Me.DataGridViewTextBoxColumn2.Width = 70
  '
  'DataGridViewTextBoxColumn3
  '
  Me.DataGridViewTextBoxColumn3.HeaderText = "Hora impresión"
  Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
  Me.DataGridViewTextBoxColumn3.ReadOnly = True
  Me.DataGridViewTextBoxColumn3.Width = 70
  '
  'DataGridViewTextBoxColumn4
  '
  Me.DataGridViewTextBoxColumn4.HeaderText = "Partidas"
  Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
  Me.DataGridViewTextBoxColumn4.ReadOnly = True
  Me.DataGridViewTextBoxColumn4.Width = 50
  '
  'DataGridViewTextBoxColumn5
  '
  Me.DataGridViewTextBoxColumn5.HeaderText = "Paqueteria"
  Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
  Me.DataGridViewTextBoxColumn5.ReadOnly = True
  Me.DataGridViewTextBoxColumn5.Width = 130
  '
  'DataGridViewTextBoxColumn6
  '
  Me.DataGridViewTextBoxColumn6.HeaderText = "Horario paq."
  Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
  Me.DataGridViewTextBoxColumn6.ReadOnly = True
  Me.DataGridViewTextBoxColumn6.Width = 90
  '
  'DataGridViewTextBoxColumn7
  '
  Me.DataGridViewTextBoxColumn7.HeaderText = "Comentario SAP"
  Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
  Me.DataGridViewTextBoxColumn7.ReadOnly = True
  Me.DataGridViewTextBoxColumn7.Width = 150
  '
  'DataGridViewTextBoxColumn8
  '
  Me.DataGridViewTextBoxColumn8.HeaderText = "Acción"
  Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
  Me.DataGridViewTextBoxColumn8.ReadOnly = True
  Me.DataGridViewTextBoxColumn8.Width = 80
  '
  'DataGridViewTextBoxColumn9
  '
  Me.DataGridViewTextBoxColumn9.HeaderText = "Impresion pedido"
  Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
  Me.DataGridViewTextBoxColumn9.ReadOnly = True
  Me.DataGridViewTextBoxColumn9.Width = 80
  '
  'DataGridViewTextBoxColumn10
  '
  Me.DataGridViewTextBoxColumn10.HeaderText = "Almacenista - Empacador"
  Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
  Me.DataGridViewTextBoxColumn10.ReadOnly = True
  Me.DataGridViewTextBoxColumn10.Visible = False
  Me.DataGridViewTextBoxColumn10.Width = 200
  '
  'DataGridViewTextBoxColumn11
  '
  Me.DataGridViewTextBoxColumn11.HeaderText = "# Partida"
  Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
  Me.DataGridViewTextBoxColumn11.ReadOnly = True
  Me.DataGridViewTextBoxColumn11.Width = 50
  '
  'DataGridViewTextBoxColumn12
  '
  Me.DataGridViewTextBoxColumn12.HeaderText = "Cod. Articulo"
  Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
  Me.DataGridViewTextBoxColumn12.ReadOnly = True
  '
  'DataGridViewTextBoxColumn13
  '
  Me.DataGridViewTextBoxColumn13.HeaderText = "Cantidad"
  Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
  Me.DataGridViewTextBoxColumn13.ReadOnly = True
  Me.DataGridViewTextBoxColumn13.Width = 240
  '
  'DataGridViewTextBoxColumn14
  '
  Me.DataGridViewTextBoxColumn14.HeaderText = "Surtido"
  Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
  Me.DataGridViewTextBoxColumn14.ReadOnly = True
  Me.DataGridViewTextBoxColumn14.Visible = False
  Me.DataGridViewTextBoxColumn14.Width = 50
  '
  'DataGridViewTextBoxColumn15
  '
  Me.DataGridViewTextBoxColumn15.HeaderText = "Surtido"
  Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
  Me.DataGridViewTextBoxColumn15.Width = 50
  '
  'GroupBox1
  '
  Me.GroupBox1.Controls.Add(Me.btn_PreClaves)
  Me.GroupBox1.Controls.Add(Me.lblActualizando)
  Me.GroupBox1.Controls.Add(Me.Label1)
  Me.GroupBox1.Controls.Add(Me.btnUpdate)
  Me.GroupBox1.Controls.Add(Me.dtpFecha)
  Me.GroupBox1.Controls.Add(Me.Label2)
  Me.GroupBox1.Controls.Add(Me.txtBuscarOrden)
  Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
  Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
  Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
  Me.GroupBox1.Name = "GroupBox1"
  Me.GroupBox1.Size = New System.Drawing.Size(1354, 55)
  Me.GroupBox1.TabIndex = 6
  Me.GroupBox1.TabStop = False
  '
  'btn_PreClaves
  '
  Me.btn_PreClaves.Location = New System.Drawing.Point(691, 16)
  Me.btn_PreClaves.Name = "btn_PreClaves"
  Me.btn_PreClaves.Size = New System.Drawing.Size(71, 26)
  Me.btn_PreClaves.TabIndex = 11
  Me.btn_PreClaves.Text = "Pre Claves"
  Me.btn_PreClaves.UseVisualStyleBackColor = True
  '
  'lblActualizando
  '
  Me.lblActualizando.AutoSize = True
  Me.lblActualizando.BackColor = System.Drawing.Color.Yellow
  Me.lblActualizando.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblActualizando.Location = New System.Drawing.Point(780, 21)
  Me.lblActualizando.Name = "lblActualizando"
  Me.lblActualizando.Size = New System.Drawing.Size(243, 16)
  Me.lblActualizando.TabIndex = 10
  Me.lblActualizando.Text = "ACTUALIZANDO INFORMACION..."
  '
  'btnUpdate
  '
  Me.btnUpdate.Dock = System.Windows.Forms.DockStyle.Right
  Me.btnUpdate.Image = Global.TPDiamante.My.Resources.Resources.Refresh_B
  Me.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
  Me.btnUpdate.Location = New System.Drawing.Point(1260, 16)
  Me.btnUpdate.Name = "btnUpdate"
  Me.btnUpdate.Size = New System.Drawing.Size(91, 36)
  Me.btnUpdate.TabIndex = 5
  Me.btnUpdate.Text = "Actualizar"
  Me.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  Me.btnUpdate.UseVisualStyleBackColor = True
  '
  'SplitContainer1
  '
  Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
  Me.SplitContainer1.Location = New System.Drawing.Point(0, 55)
  Me.SplitContainer1.Name = "SplitContainer1"
  '
  'SplitContainer1.Panel1
  '
  Me.SplitContainer1.Panel1.Controls.Add(Me.dgvOrdenes)
  '
  'SplitContainer1.Panel2
  '
  Me.SplitContainer1.Panel2.Controls.Add(Me.dgvDetalle)
  Me.SplitContainer1.Size = New System.Drawing.Size(1354, 706)
  Me.SplitContainer1.SplitterDistance = 987
  Me.SplitContainer1.TabIndex = 7
  '
  'frmMostrarOrdenes
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1354, 761)
  Me.Controls.Add(Me.SplitContainer1)
  Me.Controls.Add(Me.GroupBox1)
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.MinimumSize = New System.Drawing.Size(800, 600)
  Me.Name = "frmMostrarOrdenes"
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Surtir"
  Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
  CType(Me.dgvOrdenes, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.dgvDetalle, System.ComponentModel.ISupportInitialize).EndInit()
  Me.GroupBox1.ResumeLayout(False)
  Me.GroupBox1.PerformLayout()
  Me.SplitContainer1.Panel1.ResumeLayout(False)
  Me.SplitContainer1.Panel2.ResumeLayout(False)
  CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
  Me.SplitContainer1.ResumeLayout(False)
  Me.ResumeLayout(False)

 End Sub

 Friend WithEvents Label1 As Label
 Friend WithEvents dtpFecha As DateTimePicker
 Friend WithEvents Label2 As Label
 Friend WithEvents txtBuscarOrden As TextBox
 Friend WithEvents dgvOrdenes As DataGridView
 Friend WithEvents dgvDetalle As DataGridView
 Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn12 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn13 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn14 As DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn15 As DataGridViewTextBoxColumn
 Friend WithEvents Timer1 As Timer
 Friend WithEvents btnUpdate As Button
 Friend WithEvents GroupBox1 As GroupBox
 Friend WithEvents SplitContainer1 As SplitContainer
 Friend WithEvents lblActualizando As Label
 Friend WithEvents btn_PreClaves As Button
End Class
