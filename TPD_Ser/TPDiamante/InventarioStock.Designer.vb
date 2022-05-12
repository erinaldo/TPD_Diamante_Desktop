<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InventarioStock
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CBArticulo = New System.Windows.Forms.ComboBox()
        Me.BExcel = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CBAlmacen = New System.Windows.Forms.ComboBox()
        Me.CBLinea = New System.Windows.Forms.ComboBox()
        Me.DGInventario = New System.Windows.Forms.DataGridView()
        CType(Me.DGInventario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(62, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 16)
        Me.Label3.TabIndex = 233
        Me.Label3.Text = "Artículo"
        '
        'CBArticulo
        '
        Me.CBArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBArticulo.FormattingEnabled = True
        Me.CBArticulo.Location = New System.Drawing.Point(120, 83)
        Me.CBArticulo.Name = "CBArticulo"
        Me.CBArticulo.Size = New System.Drawing.Size(261, 21)
        Me.CBArticulo.TabIndex = 232
        '
        'BExcel
        '
        Me.BExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BExcel.Location = New System.Drawing.Point(536, 42)
        Me.BExcel.Name = "BExcel"
        Me.BExcel.Size = New System.Drawing.Size(36, 34)
        Me.BExcel.TabIndex = 231
        Me.BExcel.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.AliceBlue
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button2.Location = New System.Drawing.Point(411, 40)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 36)
        Me.Button2.TabIndex = 230
        Me.Button2.Text = "Consultar"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(53, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 16)
        Me.Label2.TabIndex = 229
        Me.Label2.Text = "Almacén"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(73, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 16)
        Me.Label1.TabIndex = 228
        Me.Label1.Text = "Línea"
        '
        'CBAlmacen
        '
        Me.CBAlmacen.FormattingEnabled = True
        Me.CBAlmacen.Location = New System.Drawing.Point(120, 18)
        Me.CBAlmacen.Name = "CBAlmacen"
        Me.CBAlmacen.Size = New System.Drawing.Size(169, 21)
        Me.CBAlmacen.TabIndex = 227
        '
        'CBLinea
        '
        Me.CBLinea.FormattingEnabled = True
        Me.CBLinea.Location = New System.Drawing.Point(120, 50)
        Me.CBLinea.Name = "CBLinea"
        Me.CBLinea.Size = New System.Drawing.Size(169, 21)
        Me.CBLinea.TabIndex = 226
        '
        'DGInventario
        '
        Me.DGInventario.AllowUserToAddRows = False
        Me.DGInventario.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGInventario.Location = New System.Drawing.Point(29, 123)
        Me.DGInventario.Name = "DGInventario"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGInventario.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGInventario.Size = New System.Drawing.Size(899, 559)
        Me.DGInventario.TabIndex = 225
        '
        'InventarioStock
        '
        Me.AcceptButton = Me.Button2
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(968, 704)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CBArticulo)
        Me.Controls.Add(Me.BExcel)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CBAlmacen)
        Me.Controls.Add(Me.CBLinea)
        Me.Controls.Add(Me.DGInventario)
        Me.Name = "InventarioStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Informe de Auditoría de Stock"
        CType(Me.DGInventario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CBArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents BExcel As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CBAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents CBLinea As System.Windows.Forms.ComboBox
    Friend WithEvents DGInventario As System.Windows.Forms.DataGridView
End Class
