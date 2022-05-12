<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEstatusEmpaque
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
  Me.dgvEmpaque = New System.Windows.Forms.DataGridView()
  Me.dgvEmpaqueDetalle = New System.Windows.Forms.DataGridView()
  Me.BtnActualizarEmp = New System.Windows.Forms.Button()
  Me.txtBuscarOrdenes = New System.Windows.Forms.TextBox()
  Me.lblBuscarOrdenes = New System.Windows.Forms.Label()
  Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
  Me.lblFecha = New System.Windows.Forms.Label()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.lblActualizando = New System.Windows.Forms.Label()
  Me.ReimpresionPL = New System.Windows.Forms.Button()
  Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
  Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
  CType(Me.dgvEmpaque, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.dgvEmpaqueDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.Panel1.SuspendLayout()
  CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SplitContainer1.Panel1.SuspendLayout()
  Me.SplitContainer1.Panel2.SuspendLayout()
  Me.SplitContainer1.SuspendLayout()
  Me.SuspendLayout()
  '
  'dgvEmpaque
  '
  Me.dgvEmpaque.AllowUserToAddRows = False
  Me.dgvEmpaque.AllowUserToDeleteRows = False
  Me.dgvEmpaque.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgvEmpaque.Dock = System.Windows.Forms.DockStyle.Fill
  Me.dgvEmpaque.Location = New System.Drawing.Point(0, 0)
  Me.dgvEmpaque.Name = "dgvEmpaque"
  Me.dgvEmpaque.RowHeadersWidth = 20
  Me.dgvEmpaque.Size = New System.Drawing.Size(1063, 520)
  Me.dgvEmpaque.TabIndex = 0
  '
  'dgvEmpaqueDetalle
  '
  Me.dgvEmpaqueDetalle.AllowUserToAddRows = False
  Me.dgvEmpaqueDetalle.AllowUserToDeleteRows = False
  Me.dgvEmpaqueDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgvEmpaqueDetalle.Dock = System.Windows.Forms.DockStyle.Fill
  Me.dgvEmpaqueDetalle.Location = New System.Drawing.Point(0, 0)
  Me.dgvEmpaqueDetalle.Name = "dgvEmpaqueDetalle"
  Me.dgvEmpaqueDetalle.RowHeadersWidth = 20
  Me.dgvEmpaqueDetalle.Size = New System.Drawing.Size(366, 520)
  Me.dgvEmpaqueDetalle.TabIndex = 1
  '
  'BtnActualizarEmp
  '
  Me.BtnActualizarEmp.Dock = System.Windows.Forms.DockStyle.Right
  Me.BtnActualizarEmp.Location = New System.Drawing.Point(1354, 0)
  Me.BtnActualizarEmp.Name = "BtnActualizarEmp"
  Me.BtnActualizarEmp.Size = New System.Drawing.Size(75, 36)
  Me.BtnActualizarEmp.TabIndex = 2
  Me.BtnActualizarEmp.Text = "Actualizar"
  Me.BtnActualizarEmp.UseVisualStyleBackColor = True
  '
  'txtBuscarOrdenes
  '
  Me.txtBuscarOrdenes.Location = New System.Drawing.Point(400, 9)
  Me.txtBuscarOrdenes.Name = "txtBuscarOrdenes"
  Me.txtBuscarOrdenes.Size = New System.Drawing.Size(161, 20)
  Me.txtBuscarOrdenes.TabIndex = 3
  '
  'lblBuscarOrdenes
  '
  Me.lblBuscarOrdenes.AutoSize = True
  Me.lblBuscarOrdenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblBuscarOrdenes.Location = New System.Drawing.Point(285, 10)
  Me.lblBuscarOrdenes.Name = "lblBuscarOrdenes"
  Me.lblBuscarOrdenes.Size = New System.Drawing.Size(109, 15)
  Me.lblBuscarOrdenes.TabIndex = 4
  Me.lblBuscarOrdenes.Text = "Buscar Ordenes"
  '
  'dtpFecha
  '
  Me.dtpFecha.Location = New System.Drawing.Point(62, 6)
  Me.dtpFecha.Name = "dtpFecha"
  Me.dtpFecha.Size = New System.Drawing.Size(217, 20)
  Me.dtpFecha.TabIndex = 5
  '
  'lblFecha
  '
  Me.lblFecha.AutoSize = True
  Me.lblFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblFecha.Location = New System.Drawing.Point(10, 10)
  Me.lblFecha.Name = "lblFecha"
  Me.lblFecha.Size = New System.Drawing.Size(46, 15)
  Me.lblFecha.TabIndex = 6
  Me.lblFecha.Text = "Fecha"
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(567, 10)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(130, 16)
  Me.Label1.TabIndex = 7
  Me.Label1.Text = "Orden de Entrega"
  '
  'Panel1
  '
  Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel1.Controls.Add(Me.lblActualizando)
  Me.Panel1.Controls.Add(Me.ReimpresionPL)
  Me.Panel1.Controls.Add(Me.lblFecha)
  Me.Panel1.Controls.Add(Me.Label1)
  Me.Panel1.Controls.Add(Me.BtnActualizarEmp)
  Me.Panel1.Controls.Add(Me.dtpFecha)
  Me.Panel1.Controls.Add(Me.lblBuscarOrdenes)
  Me.Panel1.Controls.Add(Me.txtBuscarOrdenes)
  Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
  Me.Panel1.Location = New System.Drawing.Point(0, 0)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(1433, 40)
  Me.Panel1.TabIndex = 8
  '
  'lblActualizando
  '
  Me.lblActualizando.AutoSize = True
  Me.lblActualizando.BackColor = System.Drawing.Color.Yellow
  Me.lblActualizando.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblActualizando.Location = New System.Drawing.Point(769, 13)
  Me.lblActualizando.Name = "lblActualizando"
  Me.lblActualizando.Size = New System.Drawing.Size(243, 16)
  Me.lblActualizando.TabIndex = 9
  Me.lblActualizando.Text = "ACTUALIZANDO INFORMACION..."
  '
  'ReimpresionPL
  '
  Me.ReimpresionPL.Location = New System.Drawing.Point(1176, 0)
  Me.ReimpresionPL.Name = "ReimpresionPL"
  Me.ReimpresionPL.Size = New System.Drawing.Size(172, 37)
  Me.ReimpresionPL.TabIndex = 8
  Me.ReimpresionPL.Text = "Reimpresión Orden de Entrega"
  Me.ReimpresionPL.UseVisualStyleBackColor = True
  '
  'SplitContainer1
  '
  Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
  Me.SplitContainer1.Location = New System.Drawing.Point(0, 40)
  Me.SplitContainer1.Name = "SplitContainer1"
  '
  'SplitContainer1.Panel1
  '
  Me.SplitContainer1.Panel1.Controls.Add(Me.dgvEmpaque)
  '
  'SplitContainer1.Panel2
  '
  Me.SplitContainer1.Panel2.Controls.Add(Me.dgvEmpaqueDetalle)
  Me.SplitContainer1.Size = New System.Drawing.Size(1433, 520)
  Me.SplitContainer1.SplitterDistance = 1063
  Me.SplitContainer1.TabIndex = 9
  '
  'Timer1
  '
  Me.Timer1.Interval = 90000
  '
  'frmEstatusEmpaque
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1433, 560)
  Me.Controls.Add(Me.SplitContainer1)
  Me.Controls.Add(Me.Panel1)
  Me.Name = "frmEstatusEmpaque"
  Me.Text = "Empaque"
  Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
  CType(Me.dgvEmpaque, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.dgvEmpaqueDetalle, System.ComponentModel.ISupportInitialize).EndInit()
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  Me.SplitContainer1.Panel1.ResumeLayout(False)
  Me.SplitContainer1.Panel2.ResumeLayout(False)
  CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
  Me.SplitContainer1.ResumeLayout(False)
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents dgvEmpaque As System.Windows.Forms.DataGridView
    Friend WithEvents dgvEmpaqueDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents BtnActualizarEmp As System.Windows.Forms.Button
    Friend WithEvents txtBuscarOrdenes As System.Windows.Forms.TextBox
    Friend WithEvents lblBuscarOrdenes As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ReimpresionPL As Button
  Friend WithEvents lblActualizando As Label
End Class
