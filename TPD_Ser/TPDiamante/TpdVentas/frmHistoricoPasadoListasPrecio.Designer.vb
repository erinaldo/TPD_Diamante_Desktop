<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHistoricoPasadoListasPrecio
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
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.txtYearI = New System.Windows.Forms.TextBox()
  Me.txtYearF = New System.Windows.Forms.TextBox()
  Me.cmb_MesI = New System.Windows.Forms.ComboBox()
  Me.cmb_MesF = New System.Windows.Forms.ComboBox()
  Me.Button1 = New System.Windows.Forms.Button()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.panelEspere = New System.Windows.Forms.Panel()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
  Me.CmbSucursal = New System.Windows.Forms.ComboBox()
  Me.Button2 = New System.Windows.Forms.Button()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.PanelInf = New System.Windows.Forms.Panel()
  Me.dgInf = New System.Windows.Forms.DataGridView()
  Me.Panel1.SuspendLayout()
  Me.panelEspere.SuspendLayout()
  Me.PanelInf.SuspendLayout()
  CType(Me.dgInf, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'Panel1
  '
  Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel1.Controls.Add(Me.Label6)
  Me.Panel1.Controls.Add(Me.Label5)
  Me.Panel1.Controls.Add(Me.txtYearI)
  Me.Panel1.Controls.Add(Me.txtYearF)
  Me.Panel1.Controls.Add(Me.cmb_MesI)
  Me.Panel1.Controls.Add(Me.cmb_MesF)
  Me.Panel1.Controls.Add(Me.Button1)
  Me.Panel1.Controls.Add(Me.Label1)
  Me.Panel1.Controls.Add(Me.panelEspere)
  Me.Panel1.Controls.Add(Me.CmbAgteVta)
  Me.Panel1.Controls.Add(Me.CmbSucursal)
  Me.Panel1.Controls.Add(Me.Button2)
  Me.Panel1.Controls.Add(Me.Label3)
  Me.Panel1.Controls.Add(Me.Label2)
  Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
  Me.Panel1.Location = New System.Drawing.Point(0, 0)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(208, 829)
  Me.Panel1.TabIndex = 184
  '
  'Label6
  '
  Me.Label6.AutoSize = True
  Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label6.Location = New System.Drawing.Point(17, 71)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(97, 13)
  Me.Label6.TabIndex = 190
  Me.Label6.Text = "Mes y año  final"
  '
  'Label5
  '
  Me.Label5.AutoSize = True
  Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label5.Location = New System.Drawing.Point(17, 8)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(106, 13)
  Me.Label5.TabIndex = 189
  Me.Label5.Text = "Mes y año  inicial"
  '
  'txtYearI
  '
  Me.txtYearI.Location = New System.Drawing.Point(155, 25)
  Me.txtYearI.Name = "txtYearI"
  Me.txtYearI.Size = New System.Drawing.Size(44, 20)
  Me.txtYearI.TabIndex = 188
  Me.txtYearI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
  '
  'txtYearF
  '
  Me.txtYearF.Location = New System.Drawing.Point(155, 90)
  Me.txtYearF.Name = "txtYearF"
  Me.txtYearF.Size = New System.Drawing.Size(44, 20)
  Me.txtYearF.TabIndex = 187
  Me.txtYearF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
  '
  'cmb_MesI
  '
  Me.cmb_MesI.FormattingEnabled = True
  Me.cmb_MesI.Location = New System.Drawing.Point(13, 24)
  Me.cmb_MesI.Name = "cmb_MesI"
  Me.cmb_MesI.Size = New System.Drawing.Size(136, 21)
  Me.cmb_MesI.TabIndex = 186
  '
  'cmb_MesF
  '
  Me.cmb_MesF.FormattingEnabled = True
  Me.cmb_MesF.Location = New System.Drawing.Point(13, 89)
  Me.cmb_MesF.Name = "cmb_MesF"
  Me.cmb_MesF.Size = New System.Drawing.Size(137, 21)
  Me.cmb_MesF.TabIndex = 185
  '
  'Button1
  '
  Me.Button1.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
  Me.Button1.Location = New System.Drawing.Point(87, 399)
  Me.Button1.Name = "Button1"
  Me.Button1.Size = New System.Drawing.Size(36, 34)
  Me.Button1.TabIndex = 184
  Me.Button1.UseVisualStyleBackColor = True
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(79, 383)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(57, 13)
  Me.Label1.TabIndex = 183
  Me.Label1.Text = "Agentes:"
  '
  'panelEspere
  '
  Me.panelEspere.BackColor = System.Drawing.Color.Maroon
  Me.panelEspere.Controls.Add(Me.Label4)
  Me.panelEspere.Location = New System.Drawing.Point(3, 450)
  Me.panelEspere.Name = "panelEspere"
  Me.panelEspere.Size = New System.Drawing.Size(209, 96)
  Me.panelEspere.TabIndex = 182
  Me.panelEspere.Visible = False
  '
  'Label4
  '
  Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label4.ForeColor = System.Drawing.Color.White
  Me.Label4.Location = New System.Drawing.Point(11, 16)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(187, 62)
  Me.Label4.TabIndex = 0
  Me.Label4.Text = "Procesando información por favor espere..."
  '
  'CmbAgteVta
  '
  Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbAgteVta.FormattingEnabled = True
  Me.CmbAgteVta.Location = New System.Drawing.Point(16, 194)
  Me.CmbAgteVta.Name = "CmbAgteVta"
  Me.CmbAgteVta.Size = New System.Drawing.Size(185, 21)
  Me.CmbAgteVta.TabIndex = 121
  '
  'CmbSucursal
  '
  Me.CmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbSucursal.FormattingEnabled = True
  Me.CmbSucursal.Location = New System.Drawing.Point(16, 148)
  Me.CmbSucursal.Name = "CmbSucursal"
  Me.CmbSucursal.Size = New System.Drawing.Size(185, 21)
  Me.CmbSucursal.TabIndex = 120
  '
  'Button2
  '
  Me.Button2.BackColor = System.Drawing.Color.AliceBlue
  Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
  Me.Button2.Location = New System.Drawing.Point(63, 344)
  Me.Button2.Name = "Button2"
  Me.Button2.Size = New System.Drawing.Size(86, 36)
  Me.Button2.TabIndex = 123
  Me.Button2.Text = "Consultar"
  Me.Button2.UseVisualStyleBackColor = False
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(14, 132)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(56, 13)
  Me.Label3.TabIndex = 132
  Me.Label3.Text = "Sucursal"
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(14, 178)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(97, 13)
  Me.Label2.TabIndex = 133
  Me.Label2.Text = "Agente de vtas."
  '
  'PanelInf
  '
  Me.PanelInf.Controls.Add(Me.dgInf)
  Me.PanelInf.Dock = System.Windows.Forms.DockStyle.Fill
  Me.PanelInf.Location = New System.Drawing.Point(208, 0)
  Me.PanelInf.Name = "PanelInf"
  Me.PanelInf.Size = New System.Drawing.Size(1237, 829)
  Me.PanelInf.TabIndex = 185
  '
  'dgInf
  '
  Me.dgInf.AllowUserToAddRows = False
  Me.dgInf.AllowUserToDeleteRows = False
  Me.dgInf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.dgInf.Dock = System.Windows.Forms.DockStyle.Fill
  Me.dgInf.Location = New System.Drawing.Point(0, 0)
  Me.dgInf.Name = "dgInf"
  Me.dgInf.Size = New System.Drawing.Size(1237, 829)
  Me.dgInf.TabIndex = 7
  '
  'frmHistoricoPasadoListasPrecio
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1445, 829)
  Me.Controls.Add(Me.PanelInf)
  Me.Controls.Add(Me.Panel1)
  Me.Name = "frmHistoricoPasadoListasPrecio"
  Me.Text = "Consulta de Histórico de listas de precio"
  Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  Me.panelEspere.ResumeLayout(False)
  Me.PanelInf.ResumeLayout(False)
  CType(Me.dgInf, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)

 End Sub

 Friend WithEvents Panel1 As Panel
 Friend WithEvents Button1 As Button
 Friend WithEvents Label1 As Label
 Friend WithEvents panelEspere As Panel
 Friend WithEvents Label4 As Label
 Friend WithEvents CmbAgteVta As ComboBox
 Friend WithEvents CmbSucursal As ComboBox
 Friend WithEvents Button2 As Button
 Friend WithEvents Label3 As Label
 Friend WithEvents Label2 As Label
 Friend WithEvents Label6 As Label
 Friend WithEvents Label5 As Label
 Friend WithEvents txtYearI As TextBox
 Friend WithEvents txtYearF As TextBox
 Friend WithEvents cmb_MesI As ComboBox
 Friend WithEvents cmb_MesF As ComboBox
 Friend WithEvents PanelInf As Panel
 Friend WithEvents dgInf As DataGridView
End Class
