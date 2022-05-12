<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RepComsultaP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RepComsultaP))
        Me.CrVConsulta = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'CrVConsulta
        '
        Me.CrVConsulta.ActiveViewIndex = -1
        Me.CrVConsulta.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CrVConsulta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrVConsulta.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrVConsulta.Location = New System.Drawing.Point(-1, -1)
        Me.CrVConsulta.Name = "CrVConsulta"
        Me.CrVConsulta.SelectionFormula = ""
        Me.CrVConsulta.ShowExportButton = False
        Me.CrVConsulta.ShowGroupTreeButton = False
        Me.CrVConsulta.ShowParameterPanelButton = False
        Me.CrVConsulta.ShowRefreshButton = False
        Me.CrVConsulta.Size = New System.Drawing.Size(910, 834)
        Me.CrVConsulta.TabIndex = 44
        Me.CrVConsulta.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.CrVConsulta.ViewTimeSelectionFormula = ""
        '
        'RepComsultaP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(911, 832)
        Me.Controls.Add(Me.CrVConsulta)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RepComsultaP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reporte Vale"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CrVConsulta As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
