<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalidaMaterial
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
        Me.DGVOrdenesLib = New System.Windows.Forms.DataGridView()
        Me.DGVDetalleLib = New System.Windows.Forms.DataGridView()
        Me.BtnActualizar = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.DGVOrdenesLib, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVDetalleLib, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVOrdenesLib
        '
        Me.DGVOrdenesLib.AllowUserToAddRows = False
        Me.DGVOrdenesLib.AllowUserToDeleteRows = False
        Me.DGVOrdenesLib.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVOrdenesLib.Location = New System.Drawing.Point(23, 64)
        Me.DGVOrdenesLib.Name = "DGVOrdenesLib"
        Me.DGVOrdenesLib.RowHeadersWidth = 20
        Me.DGVOrdenesLib.Size = New System.Drawing.Size(878, 399)
        Me.DGVOrdenesLib.TabIndex = 0
        '
        'DGVDetalleLib
        '
        Me.DGVDetalleLib.AllowUserToAddRows = False
        Me.DGVDetalleLib.AllowUserToDeleteRows = False
        Me.DGVDetalleLib.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVDetalleLib.Location = New System.Drawing.Point(908, 64)
        Me.DGVDetalleLib.Name = "DGVDetalleLib"
        Me.DGVDetalleLib.RowHeadersWidth = 20
        Me.DGVDetalleLib.Size = New System.Drawing.Size(380, 399)
        Me.DGVDetalleLib.TabIndex = 1
        '
        'BtnActualizar
        '
        Me.BtnActualizar.Location = New System.Drawing.Point(23, 35)
        Me.BtnActualizar.Name = "BtnActualizar"
        Me.BtnActualizar.Size = New System.Drawing.Size(75, 23)
        Me.BtnActualizar.TabIndex = 2
        Me.BtnActualizar.Text = "Actualizar"
        Me.BtnActualizar.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'frmSalidaMaterial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1300, 470)
        Me.Controls.Add(Me.BtnActualizar)
        Me.Controls.Add(Me.DGVDetalleLib)
        Me.Controls.Add(Me.DGVOrdenesLib)
        Me.Name = "frmSalidaMaterial"
        Me.Text = "Salida De Material"
        CType(Me.DGVOrdenesLib, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVDetalleLib, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGVOrdenesLib As DataGridView
    Friend WithEvents DGVDetalleLib As DataGridView
    Friend WithEvents BtnActualizar As Button
    Friend WithEvents Timer1 As Timer
End Class
