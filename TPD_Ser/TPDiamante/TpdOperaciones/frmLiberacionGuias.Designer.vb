<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLiberacionGuias
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
        Me.DGVLiberacionGuias = New System.Windows.Forms.DataGridView()
        Me.btnActualiza = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.DgvGuiasDetalle = New System.Windows.Forms.DataGridView()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.dgvLiberadas = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.bUpdateLiberadas = New System.Windows.Forms.Button()
        Me.dgvFetesFacturados = New System.Windows.Forms.DataGridView()
        CType(Me.DGVLiberacionGuias, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvGuiasDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgvLiberadas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvFetesFacturados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVLiberacionGuias
        '
        Me.DGVLiberacionGuias.AllowUserToAddRows = False
        Me.DGVLiberacionGuias.AllowUserToDeleteRows = False
        Me.DGVLiberacionGuias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVLiberacionGuias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVLiberacionGuias.Location = New System.Drawing.Point(0, 0)
        Me.DGVLiberacionGuias.Name = "DGVLiberacionGuias"
        Me.DGVLiberacionGuias.RowHeadersWidth = 20
        Me.DGVLiberacionGuias.Size = New System.Drawing.Size(1132, 389)
        Me.DGVLiberacionGuias.TabIndex = 0
        '
        'btnActualiza
        '
        Me.btnActualiza.Location = New System.Drawing.Point(225, 1)
        Me.btnActualiza.Name = "btnActualiza"
        Me.btnActualiza.Size = New System.Drawing.Size(75, 29)
        Me.btnActualiza.TabIndex = 1
        Me.btnActualiza.Text = "Actualizar"
        Me.btnActualiza.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'DgvGuiasDetalle
        '
        Me.DgvGuiasDetalle.AllowUserToAddRows = False
        Me.DgvGuiasDetalle.AllowUserToDeleteRows = False
        Me.DgvGuiasDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvGuiasDetalle.Dock = System.Windows.Forms.DockStyle.Left
        Me.DgvGuiasDetalle.Location = New System.Drawing.Point(0, 0)
        Me.DgvGuiasDetalle.Name = "DgvGuiasDetalle"
        Me.DgvGuiasDetalle.RowHeadersWidth = 20
        Me.DgvGuiasDetalle.Size = New System.Drawing.Size(169, 389)
        Me.DgvGuiasDetalle.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1403, 450)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1395, 424)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Por liberar"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 32)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DGVLiberacionGuias)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvFetesFacturados)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DgvGuiasDetalle)
        Me.SplitContainer1.Size = New System.Drawing.Size(1389, 389)
        Me.SplitContainer1.SplitterDistance = 1132
        Me.SplitContainer1.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dtpFecha)
        Me.Panel1.Controls.Add(Me.btnActualiza)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1389, 29)
        Me.Panel1.TabIndex = 3
        '
        'dtpFecha
        '
        Me.dtpFecha.Location = New System.Drawing.Point(5, 3)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(214, 20)
        Me.dtpFecha.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel3)
        Me.TabPage2.Controls.Add(Me.Panel2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1395, 424)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Liberadas"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.dgvLiberadas)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 34)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1389, 387)
        Me.Panel3.TabIndex = 1
        '
        'dgvLiberadas
        '
        Me.dgvLiberadas.AllowUserToAddRows = False
        Me.dgvLiberadas.AllowUserToDeleteRows = False
        Me.dgvLiberadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLiberadas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLiberadas.Location = New System.Drawing.Point(0, 0)
        Me.dgvLiberadas.Name = "dgvLiberadas"
        Me.dgvLiberadas.ReadOnly = True
        Me.dgvLiberadas.Size = New System.Drawing.Size(1389, 387)
        Me.dgvLiberadas.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.bUpdateLiberadas)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1389, 31)
        Me.Panel2.TabIndex = 0
        '
        'bUpdateLiberadas
        '
        Me.bUpdateLiberadas.Dock = System.Windows.Forms.DockStyle.Left
        Me.bUpdateLiberadas.Location = New System.Drawing.Point(0, 0)
        Me.bUpdateLiberadas.Name = "bUpdateLiberadas"
        Me.bUpdateLiberadas.Size = New System.Drawing.Size(75, 31)
        Me.bUpdateLiberadas.TabIndex = 0
        Me.bUpdateLiberadas.Text = "&Actualizar"
        Me.bUpdateLiberadas.UseVisualStyleBackColor = True
        '
        'dgvFetesFacturados
        '
        Me.dgvFetesFacturados.AllowUserToAddRows = False
        Me.dgvFetesFacturados.AllowUserToDeleteRows = False
        Me.dgvFetesFacturados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFetesFacturados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFetesFacturados.Location = New System.Drawing.Point(169, 0)
        Me.dgvFetesFacturados.Name = "dgvFetesFacturados"
        Me.dgvFetesFacturados.Size = New System.Drawing.Size(84, 389)
        Me.dgvFetesFacturados.TabIndex = 3
        '
        'frmLiberacionGuias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1403, 450)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmLiberacionGuias"
        Me.ShowIcon = False
        Me.Text = "Liberación de Guias"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DGVLiberacionGuias, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvGuiasDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.dgvLiberadas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.dgvFetesFacturados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGVLiberacionGuias As DataGridView
    Friend WithEvents btnActualiza As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents DgvGuiasDetalle As DataGridView
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Panel3 As Panel
    Friend WithEvents dgvLiberadas As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents bUpdateLiberadas As Button
    Friend WithEvents dtpFecha As DateTimePicker
    Friend WithEvents dgvFetesFacturados As DataGridView
End Class
