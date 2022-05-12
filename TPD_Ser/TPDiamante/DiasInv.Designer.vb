<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiasInv
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DiasInv))
        Me.CBLineas = New System.Windows.Forms.ComboBox()
        Me.CBAlmacenes = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.bExcel = New System.Windows.Forms.Button()
        Me.BGuardar = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DGDiasInv = New System.Windows.Forms.DataGridView()
        Me.Mostrar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ItmsGrpCod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItmsGrpNam = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WhsCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WhsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiasInven = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.DGDiasInv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CBLineas
        '
        Me.CBLineas.FormattingEnabled = True
        Me.CBLineas.Location = New System.Drawing.Point(26, 51)
        Me.CBLineas.Name = "CBLineas"
        Me.CBLineas.Size = New System.Drawing.Size(159, 21)
        Me.CBLineas.TabIndex = 2
        '
        'CBAlmacenes
        '
        Me.CBAlmacenes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBAlmacenes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBAlmacenes.FormattingEnabled = True
        Me.CBAlmacenes.Location = New System.Drawing.Point(223, 53)
        Me.CBAlmacenes.Name = "CBAlmacenes"
        Me.CBAlmacenes.Size = New System.Drawing.Size(155, 21)
        Me.CBAlmacenes.TabIndex = 3
        Me.CBAlmacenes.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Línea"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(220, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Almacén"
        Me.Label2.Visible = False
        '
        'bExcel
        '
        Me.bExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.bExcel.Location = New System.Drawing.Point(544, 45)
        Me.bExcel.Name = "bExcel"
        Me.bExcel.Size = New System.Drawing.Size(32, 27)
        Me.bExcel.TabIndex = 125
        Me.bExcel.UseVisualStyleBackColor = True
        '
        'BGuardar
        '
        Me.BGuardar.Image = CType(resources.GetObject("BGuardar.Image"), System.Drawing.Image)
        Me.BGuardar.Location = New System.Drawing.Point(534, 380)
        Me.BGuardar.Name = "BGuardar"
        Me.BGuardar.Size = New System.Drawing.Size(42, 38)
        Me.BGuardar.TabIndex = 215
        Me.BGuardar.Tag = "Actualizar"
        Me.BGuardar.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Código Línea"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 60
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Línea"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Código Almacén"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 60
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Almacén"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 120
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Días de Inventario"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'DGDiasInv
        '
        Me.DGDiasInv.AllowUserToAddRows = False
        Me.DGDiasInv.AllowUserToDeleteRows = False
        Me.DGDiasInv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGDiasInv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGDiasInv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Mostrar, Me.ItmsGrpCod, Me.ItmsGrpNam, Me.WhsCode, Me.WhsName, Me.DiasInven})
        Me.DGDiasInv.Location = New System.Drawing.Point(26, 82)
        Me.DGDiasInv.Name = "DGDiasInv"
        Me.DGDiasInv.RowHeadersWidth = 25
        Me.DGDiasInv.Size = New System.Drawing.Size(548, 292)
        Me.DGDiasInv.TabIndex = 216
        '
        'Mostrar
        '
        Me.Mostrar.DataPropertyName = "Mostrar"
        Me.Mostrar.HeaderText = "Mostrar"
        Me.Mostrar.Name = "Mostrar"
        Me.Mostrar.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Mostrar.Width = 50
        '
        'ItmsGrpCod
        '
        Me.ItmsGrpCod.DataPropertyName = "ItmsGrpCod"
        Me.ItmsGrpCod.HeaderText = "Clave Línea"
        Me.ItmsGrpCod.Name = "ItmsGrpCod"
        Me.ItmsGrpCod.ReadOnly = True
        Me.ItmsGrpCod.Width = 60
        '
        'ItmsGrpNam
        '
        Me.ItmsGrpNam.DataPropertyName = "ItmsGrpNam"
        Me.ItmsGrpNam.HeaderText = "Línea"
        Me.ItmsGrpNam.Name = "ItmsGrpNam"
        Me.ItmsGrpNam.ReadOnly = True
        Me.ItmsGrpNam.Width = 150
        '
        'WhsCode
        '
        Me.WhsCode.DataPropertyName = "WhsCode"
        Me.WhsCode.HeaderText = "Clave Almacén"
        Me.WhsCode.Name = "WhsCode"
        Me.WhsCode.ReadOnly = True
        Me.WhsCode.Width = 50
        '
        'WhsName
        '
        Me.WhsName.DataPropertyName = "WhsName"
        Me.WhsName.HeaderText = "Almacén"
        Me.WhsName.Name = "WhsName"
        Me.WhsName.ReadOnly = True
        Me.WhsName.Width = 120
        '
        'DiasInven
        '
        Me.DiasInven.DataPropertyName = "DiasInv"
        Me.DiasInven.HeaderText = "Días de Inventario"
        Me.DiasInven.Name = "DiasInven"
        Me.DiasInven.Width = 60
        '
        'Button1
        '
        Me.Button1.Image = Global.TPDiamante.My.Resources.Resources.ballwhite
        Me.Button1.Location = New System.Drawing.Point(470, 380)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(40, 38)
        Me.Button1.TabIndex = 217
        Me.Button1.Tag = "Actualizar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(467, 421)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 218
        Me.Label3.Text = "Reiniciar"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(534, 421)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 219
        Me.Label4.Text = "Guardar"
        '
        'DiasInv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(588, 436)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DGDiasInv)
        Me.Controls.Add(Me.BGuardar)
        Me.Controls.Add(Me.bExcel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CBAlmacenes)
        Me.Controls.Add(Me.CBLineas)
        Me.Name = "DiasInv"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dias de Inventario"
        CType(Me.DGDiasInv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CBLineas As System.Windows.Forms.ComboBox
    Friend WithEvents CBAlmacenes As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents bExcel As System.Windows.Forms.Button
    Friend WithEvents BGuardar As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DGDiasInv As System.Windows.Forms.DataGridView
    Friend WithEvents Mostrar As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ItmsGrpCod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItmsGrpNam As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WhsCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WhsName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DiasInven As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
