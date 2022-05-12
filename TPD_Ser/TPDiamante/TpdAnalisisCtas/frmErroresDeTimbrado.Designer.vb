<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmErroresDeTimbrado
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
        Me.dgvErroresTimbrado = New System.Windows.Forms.DataGridView()
        Me.btnActualizarERR = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.dgvErroresTimbrado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvErroresTimbrado
        '
        Me.dgvErroresTimbrado.AllowUserToAddRows = False
        Me.dgvErroresTimbrado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvErroresTimbrado.Location = New System.Drawing.Point(12, 59)
        Me.dgvErroresTimbrado.Name = "dgvErroresTimbrado"
        Me.dgvErroresTimbrado.RowHeadersWidth = 20
        Me.dgvErroresTimbrado.Size = New System.Drawing.Size(901, 505)
        Me.dgvErroresTimbrado.TabIndex = 0
        '
        'btnActualizarERR
        '
        Me.btnActualizarERR.Location = New System.Drawing.Point(12, 12)
        Me.btnActualizarERR.Name = "btnActualizarERR"
        Me.btnActualizarERR.Size = New System.Drawing.Size(75, 23)
        Me.btnActualizarERR.TabIndex = 1
        Me.btnActualizarERR.Text = "Actualizar"
        Me.btnActualizarERR.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'frmErroresDeTimbrado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(917, 568)
        Me.Controls.Add(Me.btnActualizarERR)
        Me.Controls.Add(Me.dgvErroresTimbrado)
        Me.Name = "frmErroresDeTimbrado"
        Me.ShowIcon = False
        Me.Text = "Errores De Timbrado"
        CType(Me.dgvErroresTimbrado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvErroresTimbrado As System.Windows.Forms.DataGridView
    Friend WithEvents btnActualizarERR As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
