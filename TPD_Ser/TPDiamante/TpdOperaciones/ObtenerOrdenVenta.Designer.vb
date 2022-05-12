<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ObtenerOrdenVenta
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
  Me.panelOV = New System.Windows.Forms.Panel()
  Me.txtFolio = New System.Windows.Forms.TextBox()
  Me.txtSerie = New System.Windows.Forms.TextBox()
  Me.Label9 = New System.Windows.Forms.Label()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.btnCancelarOV = New System.Windows.Forms.Button()
  Me.btnPrintOV = New System.Windows.Forms.Button()
  Me.Label7 = New System.Windows.Forms.Label()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.dgvNuevaOV = New System.Windows.Forms.DataGridView()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
  Me.panelOV.SuspendLayout()
  CType(Me.dgvNuevaOV, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'panelOV
  '
  Me.panelOV.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
  Me.panelOV.Controls.Add(Me.txtFolio)
  Me.panelOV.Controls.Add(Me.txtSerie)
  Me.panelOV.Controls.Add(Me.Label9)
  Me.panelOV.Controls.Add(Me.Label8)
  Me.panelOV.Controls.Add(Me.btnCancelarOV)
  Me.panelOV.Controls.Add(Me.btnPrintOV)
  Me.panelOV.Controls.Add(Me.Label7)
  Me.panelOV.Controls.Add(Me.Label6)
  Me.panelOV.Controls.Add(Me.dgvNuevaOV)
  Me.panelOV.Controls.Add(Me.Label5)
  Me.panelOV.Location = New System.Drawing.Point(0, 1)
  Me.panelOV.Name = "panelOV"
  Me.panelOV.Size = New System.Drawing.Size(575, 442)
  Me.panelOV.TabIndex = 10
  '
  'txtFolio
  '
  Me.txtFolio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtFolio.Location = New System.Drawing.Point(156, 397)
  Me.txtFolio.Name = "txtFolio"
  Me.txtFolio.Size = New System.Drawing.Size(45, 20)
  Me.txtFolio.TabIndex = 10
  Me.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
  '
  'txtSerie
  '
  Me.txtSerie.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtSerie.Location = New System.Drawing.Point(67, 397)
  Me.txtSerie.Name = "txtSerie"
  Me.txtSerie.Size = New System.Drawing.Size(30, 20)
  Me.txtSerie.TabIndex = 9
  Me.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
  '
  'Label9
  '
  Me.Label9.AutoSize = True
  Me.Label9.Location = New System.Drawing.Point(127, 400)
  Me.Label9.Name = "Label9"
  Me.Label9.Size = New System.Drawing.Size(29, 13)
  Me.Label9.TabIndex = 8
  Me.Label9.Text = "Folio"
  '
  'Label8
  '
  Me.Label8.AutoSize = True
  Me.Label8.Location = New System.Drawing.Point(33, 400)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(31, 13)
  Me.Label8.TabIndex = 7
  Me.Label8.Text = "Serie"
  '
  'btnCancelarOV
  '
  Me.btnCancelarOV.Location = New System.Drawing.Point(396, 391)
  Me.btnCancelarOV.Name = "btnCancelarOV"
  Me.btnCancelarOV.Size = New System.Drawing.Size(151, 31)
  Me.btnCancelarOV.TabIndex = 6
  Me.btnCancelarOV.Text = "Salir de Ordenes de Venta"
  Me.btnCancelarOV.UseVisualStyleBackColor = True
  '
  'btnPrintOV
  '
  Me.btnPrintOV.Location = New System.Drawing.Point(220, 391)
  Me.btnPrintOV.Name = "btnPrintOV"
  Me.btnPrintOV.Size = New System.Drawing.Size(151, 31)
  Me.btnPrintOV.TabIndex = 5
  Me.btnPrintOV.Text = "Imprimir Orden de venta"
  Me.btnPrintOV.UseVisualStyleBackColor = True
  '
  'Label7
  '
  Me.Label7.AutoSize = True
  Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label7.Location = New System.Drawing.Point(30, 370)
  Me.Label7.Name = "Label7"
  Me.Label7.Size = New System.Drawing.Size(81, 20)
  Me.Label7.TabIndex = 3
  Me.Label7.Text = "PASADA"
  '
  'Label6
  '
  Me.Label6.AutoSize = True
  Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label6.Location = New System.Drawing.Point(30, 84)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(82, 20)
  Me.Label6.TabIndex = 2
  Me.Label6.Text = "NUEVAS"
  '
  'dgvNuevaOV
  '
  Me.dgvNuevaOV.AllowUserToAddRows = False
  Me.dgvNuevaOV.AllowUserToResizeColumns = False
  Me.dgvNuevaOV.AllowUserToResizeRows = False
  Me.dgvNuevaOV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgvNuevaOV.Location = New System.Drawing.Point(33, 112)
  Me.dgvNuevaOV.Name = "dgvNuevaOV"
  Me.dgvNuevaOV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
  Me.dgvNuevaOV.Size = New System.Drawing.Size(512, 231)
  Me.dgvNuevaOV.TabIndex = 1
  '
  'Label5
  '
  Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label5.Location = New System.Drawing.Point(3, 22)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(542, 32)
  Me.Label5.TabIndex = 0
  Me.Label5.Text = "ORDENES DE VENTA"
  Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'Timer1
  '
  Me.Timer1.Enabled = True
  Me.Timer1.Interval = 10000
  '
  'ObtenerOrdenVenta
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
  Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
  Me.ClientSize = New System.Drawing.Size(577, 445)
  Me.ControlBox = False
  Me.Controls.Add(Me.panelOV)
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.Name = "ObtenerOrdenVenta"
  Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Nuevas ordenes de venta"
  Me.panelOV.ResumeLayout(False)
  Me.panelOV.PerformLayout()
  CType(Me.dgvNuevaOV, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)

 End Sub

 Friend WithEvents panelOV As Panel
 Friend WithEvents txtFolio As TextBox
 Friend WithEvents txtSerie As TextBox
 Friend WithEvents Label9 As Label
 Friend WithEvents Label8 As Label
 Friend WithEvents btnCancelarOV As Button
 Friend WithEvents btnPrintOV As Button
 Friend WithEvents Label7 As Label
 Friend WithEvents Label6 As Label
 Friend WithEvents dgvNuevaOV As DataGridView
 Friend WithEvents Label5 As Label
 Friend WithEvents Timer1 As Timer
End Class
