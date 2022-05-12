<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OVtaCSol
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DGVDetOrdVta = New System.Windows.Forms.DataGridView()
        Me.DGVEncOrdVta = New System.Windows.Forms.DataGridView()
        Me.DGVFilDet = New System.Windows.Forms.DataGridView()
        CType(Me.DGVDetOrdVta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVEncOrdVta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVFilDet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(957, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(109, 27)
        Me.Button1.TabIndex = 72
        Me.Button1.Text = "Guardar Solicitud"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DGVDetOrdVta
        '
        Me.DGVDetOrdVta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVDetOrdVta.Location = New System.Drawing.Point(6, 414)
        Me.DGVDetOrdVta.Name = "DGVDetOrdVta"
        Me.DGVDetOrdVta.Size = New System.Drawing.Size(1185, 340)
        Me.DGVDetOrdVta.TabIndex = 74
        '
        'DGVEncOrdVta
        '
        Me.DGVEncOrdVta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVEncOrdVta.Location = New System.Drawing.Point(6, 35)
        Me.DGVEncOrdVta.Name = "DGVEncOrdVta"
        Me.DGVEncOrdVta.Size = New System.Drawing.Size(1185, 355)
        Me.DGVEncOrdVta.TabIndex = 73
        '
        'DGVFilDet
        '
        Me.DGVFilDet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVFilDet.Location = New System.Drawing.Point(6, 427)
        Me.DGVFilDet.Name = "DGVFilDet"
        Me.DGVFilDet.Size = New System.Drawing.Size(1185, 340)
        Me.DGVFilDet.TabIndex = 75
        Me.DGVFilDet.Visible = False
        '
        'OVtaCSol
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1197, 779)
        Me.Controls.Add(Me.DGVFilDet)
        Me.Controls.Add(Me.DGVDetOrdVta)
        Me.Controls.Add(Me.DGVEncOrdVta)
        Me.Controls.Add(Me.Button1)
        Me.Name = "OVtaCSol"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OVtaCSol"
        CType(Me.DGVDetOrdVta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVEncOrdVta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVFilDet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DGVDetOrdVta As System.Windows.Forms.DataGridView
    Friend WithEvents DGVEncOrdVta As System.Windows.Forms.DataGridView
    Friend WithEvents DGVFilDet As System.Windows.Forms.DataGridView
End Class
