<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReporteAlberto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReporteAlberto))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.bExportarExcel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtfechafin = New System.Windows.Forms.DateTimePicker()
        Me.bGenerar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtfechainicio = New System.Windows.Forms.DateTimePicker()
        Me.dgvDatos = New System.Windows.Forms.DataGridView()
        Me.lbUltimaCons = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lbUltimaCons)
        Me.Panel1.Controls.Add(Me.bExportarExcel)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.dtfechafin)
        Me.Panel1.Controls.Add(Me.bGenerar)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.dtfechainicio)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1188, 42)
        Me.Panel1.TabIndex = 0
        '
        'bExportarExcel
        '
        Me.bExportarExcel.BackgroundImage = CType(resources.GetObject("bExportarExcel.BackgroundImage"), System.Drawing.Image)
        Me.bExportarExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.bExportarExcel.Location = New System.Drawing.Point(363, 2)
        Me.bExportarExcel.Name = "bExportarExcel"
        Me.bExportarExcel.Size = New System.Drawing.Size(49, 34)
        Me.bExportarExcel.TabIndex = 5
        Me.bExportarExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.bExportarExcel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Fecha :"
        '
        'dtfechafin
        '
        Me.dtfechafin.Enabled = False
        Me.dtfechafin.Location = New System.Drawing.Point(66, 10)
        Me.dtfechafin.Name = "dtfechafin"
        Me.dtfechafin.Size = New System.Drawing.Size(210, 20)
        Me.dtfechafin.TabIndex = 3
        '
        'bGenerar
        '
        Me.bGenerar.Location = New System.Drawing.Point(282, 7)
        Me.bGenerar.Name = "bGenerar"
        Me.bGenerar.Size = New System.Drawing.Size(75, 23)
        Me.bGenerar.TabIndex = 2
        Me.bGenerar.Text = "&Consultar"
        Me.bGenerar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(429, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Fecha Inicio:"
        Me.Label1.Visible = False
        '
        'dtfechainicio
        '
        Me.dtfechainicio.Location = New System.Drawing.Point(516, 10)
        Me.dtfechainicio.Name = "dtfechainicio"
        Me.dtfechainicio.Size = New System.Drawing.Size(210, 20)
        Me.dtfechainicio.TabIndex = 0
        Me.dtfechainicio.Visible = False
        '
        'dgvDatos
        '
        Me.dgvDatos.AllowUserToAddRows = False
        Me.dgvDatos.AllowUserToDeleteRows = False
        Me.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDatos.Location = New System.Drawing.Point(0, 42)
        Me.dgvDatos.Name = "dgvDatos"
        Me.dgvDatos.ReadOnly = True
        Me.dgvDatos.Size = New System.Drawing.Size(1188, 448)
        Me.dgvDatos.TabIndex = 1
        '
        'lbUltimaCons
        '
        Me.lbUltimaCons.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbUltimaCons.AutoSize = True
        Me.lbUltimaCons.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbUltimaCons.ForeColor = System.Drawing.Color.Red
        Me.lbUltimaCons.Location = New System.Drawing.Point(1135, 13)
        Me.lbUltimaCons.Name = "lbUltimaCons"
        Me.lbUltimaCons.Size = New System.Drawing.Size(46, 16)
        Me.lbUltimaCons.TabIndex = 6
        Me.lbUltimaCons.Text = "Label3"
        '
        'frmReporteAlberto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1188, 490)
        Me.Controls.Add(Me.dgvDatos)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmReporteAlberto"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cred. Disp. por cliente al dia"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents dgvDatos As DataGridView
    Friend WithEvents bGenerar As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents dtfechainicio As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents dtfechafin As DateTimePicker
    Friend WithEvents bExportarExcel As Button
    Friend WithEvents lbUltimaCons As Label
End Class
