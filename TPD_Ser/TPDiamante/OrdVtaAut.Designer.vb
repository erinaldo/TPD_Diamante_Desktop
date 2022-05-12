<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrdVtaAut
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
    Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Me.DgvEncOVtas = New System.Windows.Forms.DataGridView()
    Me.DgvDetOVtas = New System.Windows.Forms.DataGridView()
    Me.TxtTotOVta = New System.Windows.Forms.TextBox()
    Me.BtnActualizar = New System.Windows.Forms.Button()
    Me.LblActualizar = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.TxtFalt = New System.Windows.Forms.TextBox()
    Me.TxtPor = New System.Windows.Forms.TextBox()
    Me.TxtExist = New System.Windows.Forms.TextBox()
    Me.BtnExcel = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
    Me.btnCOnsultar = New System.Windows.Forms.Button()
    CType(Me.DgvEncOVtas, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.DgvDetOVtas, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'DgvEncOVtas
    '
    Me.DgvEncOVtas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgvEncOVtas.Location = New System.Drawing.Point(9, 38)
    Me.DgvEncOVtas.Name = "DgvEncOVtas"
    DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgvEncOVtas.RowsDefaultCellStyle = DataGridViewCellStyle3
    Me.DgvEncOVtas.Size = New System.Drawing.Size(983, 346)
    Me.DgvEncOVtas.TabIndex = 0
    '
    'DgvDetOVtas
    '
    Me.DgvDetOVtas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.DgvDetOVtas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgvDetOVtas.Location = New System.Drawing.Point(9, 405)
    Me.DgvDetOVtas.Name = "DgvDetOVtas"
    DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DgvDetOVtas.RowsDefaultCellStyle = DataGridViewCellStyle4
    Me.DgvDetOVtas.Size = New System.Drawing.Size(983, 280)
    Me.DgvDetOVtas.TabIndex = 2
    '
    'TxtTotOVta
    '
    Me.TxtTotOVta.BackColor = System.Drawing.Color.White
    Me.TxtTotOVta.Location = New System.Drawing.Point(559, 384)
    Me.TxtTotOVta.Name = "TxtTotOVta"
    Me.TxtTotOVta.ReadOnly = True
    Me.TxtTotOVta.Size = New System.Drawing.Size(66, 20)
    Me.TxtTotOVta.TabIndex = 6
    Me.TxtTotOVta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'BtnActualizar
    '
    Me.BtnActualizar.Image = Global.TPDiamante.My.Resources.Resources.Refresh_B
    Me.BtnActualizar.Location = New System.Drawing.Point(682, 1)
    Me.BtnActualizar.Name = "BtnActualizar"
    Me.BtnActualizar.Size = New System.Drawing.Size(38, 35)
    Me.BtnActualizar.TabIndex = 3
    Me.BtnActualizar.UseVisualStyleBackColor = True
    '
    'LblActualizar
    '
    Me.LblActualizar.AutoSize = True
    Me.LblActualizar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LblActualizar.Location = New System.Drawing.Point(503, 387)
    Me.LblActualizar.Name = "LblActualizar"
    Me.LblActualizar.Size = New System.Drawing.Size(59, 13)
    Me.LblActualizar.TabIndex = 5
    Me.LblActualizar.Text = "Totales $"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.BackColor = System.Drawing.Color.Transparent
    Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(9, 391)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(134, 13)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "Detalle de Orden de Venta"
    '
    'TxtFalt
    '
    Me.TxtFalt.BackColor = System.Drawing.Color.White
    Me.TxtFalt.ForeColor = System.Drawing.Color.DarkRed
    Me.TxtFalt.Location = New System.Drawing.Point(625, 384)
    Me.TxtFalt.Name = "TxtFalt"
    Me.TxtFalt.ReadOnly = True
    Me.TxtFalt.Size = New System.Drawing.Size(60, 20)
    Me.TxtFalt.TabIndex = 7
    Me.TxtFalt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'TxtPor
    '
    Me.TxtPor.BackColor = System.Drawing.Color.White
    Me.TxtPor.Location = New System.Drawing.Point(685, 384)
    Me.TxtPor.Name = "TxtPor"
    Me.TxtPor.ReadOnly = True
    Me.TxtPor.Size = New System.Drawing.Size(35, 20)
    Me.TxtPor.TabIndex = 8
    Me.TxtPor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'TxtExist
    '
    Me.TxtExist.BackColor = System.Drawing.Color.White
    Me.TxtExist.ForeColor = System.Drawing.Color.Navy
    Me.TxtExist.Location = New System.Drawing.Point(720, 384)
    Me.TxtExist.Name = "TxtExist"
    Me.TxtExist.ReadOnly = True
    Me.TxtExist.Size = New System.Drawing.Size(66, 20)
    Me.TxtExist.TabIndex = 9
    Me.TxtExist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'BtnExcel
    '
    Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
    Me.BtnExcel.Location = New System.Drawing.Point(735, 1)
    Me.BtnExcel.Name = "BtnExcel"
    Me.BtnExcel.Size = New System.Drawing.Size(38, 35)
    Me.BtnExcel.TabIndex = 4
    Me.BtnExcel.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
    Me.Label1.Location = New System.Drawing.Point(12, 12)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(89, 15)
    Me.Label1.TabIndex = 203
    Me.Label1.Text = "Agente de vtas."
    '
    'CmbAgteVta
    '
    Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbAgteVta.FormattingEnabled = True
    Me.CmbAgteVta.Location = New System.Drawing.Point(105, 9)
    Me.CmbAgteVta.Name = "CmbAgteVta"
    Me.CmbAgteVta.Size = New System.Drawing.Size(182, 21)
    Me.CmbAgteVta.TabIndex = 202
    '
    'btnCOnsultar
    '
    Me.btnCOnsultar.Location = New System.Drawing.Point(324, 9)
    Me.btnCOnsultar.Name = "btnCOnsultar"
    Me.btnCOnsultar.Size = New System.Drawing.Size(75, 23)
    Me.btnCOnsultar.TabIndex = 204
    Me.btnCOnsultar.Text = "Consultar"
    Me.btnCOnsultar.UseVisualStyleBackColor = True
    '
    'OrdVtaAut
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.LightSteelBlue
    Me.ClientSize = New System.Drawing.Size(995, 693)
    Me.Controls.Add(Me.btnCOnsultar)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.CmbAgteVta)
    Me.Controls.Add(Me.BtnExcel)
    Me.Controls.Add(Me.TxtExist)
    Me.Controls.Add(Me.TxtPor)
    Me.Controls.Add(Me.TxtFalt)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.BtnActualizar)
    Me.Controls.Add(Me.TxtTotOVta)
    Me.Controls.Add(Me.DgvDetOVtas)
    Me.Controls.Add(Me.DgvEncOVtas)
    Me.Controls.Add(Me.LblActualizar)
    Me.Name = "OrdVtaAut"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Ordenes de Venta Creadas Por Facturar"
    CType(Me.DgvEncOVtas, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.DgvDetOVtas, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents DgvEncOVtas As System.Windows.Forms.DataGridView
    Friend WithEvents DgvDetOVtas As System.Windows.Forms.DataGridView
    Friend WithEvents TxtTotOVta As System.Windows.Forms.TextBox
    Friend WithEvents BtnActualizar As System.Windows.Forms.Button
    Friend WithEvents LblActualizar As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtFalt As System.Windows.Forms.TextBox
    Friend WithEvents TxtPor As System.Windows.Forms.TextBox
    Friend WithEvents TxtExist As System.Windows.Forms.TextBox
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
  Friend WithEvents Label1 As Label
  Friend WithEvents CmbAgteVta As ComboBox
  Friend WithEvents btnCOnsultar As Button
End Class
