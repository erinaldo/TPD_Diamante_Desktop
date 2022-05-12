<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Inventario
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
  Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Inventario))
  Me.DGInventario = New System.Windows.Forms.DataGridView()
  Me.CBAlmacen = New System.Windows.Forms.ComboBox()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
  Me.Estatus = New System.Windows.Forms.Panel()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
  Me.BSave = New System.Windows.Forms.Button()
  Me.Button3 = New System.Windows.Forms.Button()
  Me.ButtonCARGAR = New System.Windows.Forms.Button()
  Me.BExcel = New System.Windows.Forms.Button()
  Me.Button2 = New System.Windows.Forms.Button()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.CBLinea = New System.Windows.Forms.ComboBox()
  Me.CKGuardar = New System.Windows.Forms.CheckBox()
  Me.CKRecuperar = New System.Windows.Forms.CheckBox()
  Me.BtnRecuperar = New System.Windows.Forms.Button()
  Me.LabelFecMod = New System.Windows.Forms.Label()
  Me.CBFecMod = New System.Windows.Forms.ComboBox()
  Me.CmbArticulo = New System.Windows.Forms.ComboBox()
  Me.Label3 = New System.Windows.Forms.Label()
  CType(Me.DGInventario, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.Estatus.SuspendLayout()
  Me.SuspendLayout()
  '
  'DGInventario
  '
  Me.DGInventario.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
  Me.DGInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.DGInventario.Location = New System.Drawing.Point(59, 95)
  Me.DGInventario.Name = "DGInventario"
  DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DGInventario.RowsDefaultCellStyle = DataGridViewCellStyle2
  Me.DGInventario.Size = New System.Drawing.Size(1334, 530)
  Me.DGInventario.TabIndex = 0
  '
  'CBAlmacen
  '
  Me.CBAlmacen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CBAlmacen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CBAlmacen.FormattingEnabled = True
  Me.CBAlmacen.Location = New System.Drawing.Point(123, 10)
  Me.CBAlmacen.Name = "CBAlmacen"
  Me.CBAlmacen.Size = New System.Drawing.Size(140, 21)
  Me.CBAlmacen.TabIndex = 2
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(56, 11)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(61, 16)
  Me.Label2.TabIndex = 4
  Me.Label2.Text = "Almacén"
  '
  'OpenFileDialog1
  '
  Me.OpenFileDialog1.FileName = "OpenFileDialog1"
  '
  'Estatus
  '
  Me.Estatus.Controls.Add(Me.Label6)
  Me.Estatus.Controls.Add(Me.ProgressBar1)
  Me.Estatus.Location = New System.Drawing.Point(604, 274)
  Me.Estatus.Name = "Estatus"
  Me.Estatus.Size = New System.Drawing.Size(167, 54)
  Me.Estatus.TabIndex = 216
  Me.Estatus.Visible = False
  '
  'Label6
  '
  Me.Label6.AutoSize = True
  Me.Label6.Location = New System.Drawing.Point(39, 14)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(91, 13)
  Me.Label6.TabIndex = 113
  Me.Label6.Text = "Cargando archivo"
  '
  'ProgressBar1
  '
  Me.ProgressBar1.Location = New System.Drawing.Point(12, 29)
  Me.ProgressBar1.Name = "ProgressBar1"
  Me.ProgressBar1.Size = New System.Drawing.Size(140, 17)
  Me.ProgressBar1.TabIndex = 112
  '
  'BSave
  '
  Me.BSave.Image = CType(resources.GetObject("BSave.Image"), System.Drawing.Image)
  Me.BSave.Location = New System.Drawing.Point(1105, 40)
  Me.BSave.Name = "BSave"
  Me.BSave.Size = New System.Drawing.Size(43, 35)
  Me.BSave.TabIndex = 223
  Me.BSave.UseVisualStyleBackColor = True
  Me.BSave.Visible = False
  '
  'Button3
  '
  Me.Button3.BackColor = System.Drawing.SystemColors.Control
  Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button3.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
  Me.Button3.Location = New System.Drawing.Point(1286, 38)
  Me.Button3.Name = "Button3"
  Me.Button3.Size = New System.Drawing.Size(101, 40)
  Me.Button3.TabIndex = 222
  Me.Button3.Text = "CARGAR"
  Me.Button3.UseVisualStyleBackColor = False
  '
  'ButtonCARGAR
  '
  Me.ButtonCARGAR.BackColor = System.Drawing.SystemColors.Control
  Me.ButtonCARGAR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.ButtonCARGAR.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
  Me.ButtonCARGAR.Location = New System.Drawing.Point(1168, 38)
  Me.ButtonCARGAR.Name = "ButtonCARGAR"
  Me.ButtonCARGAR.Size = New System.Drawing.Size(101, 40)
  Me.ButtonCARGAR.TabIndex = 221
  Me.ButtonCARGAR.Text = "LIMPIAR"
  Me.ButtonCARGAR.UseVisualStyleBackColor = False
  '
  'BExcel
  '
  Me.BExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
  Me.BExcel.Location = New System.Drawing.Point(635, 42)
  Me.BExcel.Name = "BExcel"
  Me.BExcel.Size = New System.Drawing.Size(36, 34)
  Me.BExcel.TabIndex = 220
  Me.BExcel.UseVisualStyleBackColor = True
  '
  'Button2
  '
  Me.Button2.BackColor = System.Drawing.Color.AliceBlue
  Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
  Me.Button2.Location = New System.Drawing.Point(522, 40)
  Me.Button2.Name = "Button2"
  Me.Button2.Size = New System.Drawing.Size(86, 36)
  Me.Button2.TabIndex = 219
  Me.Button2.Text = "Consultar"
  Me.Button2.UseVisualStyleBackColor = False
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(76, 38)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(41, 16)
  Me.Label1.TabIndex = 218
  Me.Label1.Text = "Línea"
  '
  'CBLinea
  '
  Me.CBLinea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CBLinea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CBLinea.FormattingEnabled = True
  Me.CBLinea.Location = New System.Drawing.Point(123, 37)
  Me.CBLinea.Name = "CBLinea"
  Me.CBLinea.Size = New System.Drawing.Size(140, 21)
  Me.CBLinea.TabIndex = 217
  '
  'CKGuardar
  '
  Me.CKGuardar.AutoSize = True
  Me.CKGuardar.Location = New System.Drawing.Point(702, 35)
  Me.CKGuardar.Name = "CKGuardar"
  Me.CKGuardar.Size = New System.Drawing.Size(93, 17)
  Me.CKGuardar.TabIndex = 224
  Me.CKGuardar.Text = "Guardar datos"
  Me.CKGuardar.UseVisualStyleBackColor = True
  '
  'CKRecuperar
  '
  Me.CKRecuperar.AutoSize = True
  Me.CKRecuperar.Location = New System.Drawing.Point(702, 58)
  Me.CKRecuperar.Name = "CKRecuperar"
  Me.CKRecuperar.Size = New System.Drawing.Size(105, 17)
  Me.CKRecuperar.TabIndex = 225
  Me.CKRecuperar.Text = "Recuperar datos"
  Me.CKRecuperar.UseVisualStyleBackColor = True
  '
  'BtnRecuperar
  '
  Me.BtnRecuperar.BackColor = System.Drawing.Color.AliceBlue
  Me.BtnRecuperar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.BtnRecuperar.ForeColor = System.Drawing.Color.MediumBlue
  Me.BtnRecuperar.Location = New System.Drawing.Point(1000, 35)
  Me.BtnRecuperar.Name = "BtnRecuperar"
  Me.BtnRecuperar.Size = New System.Drawing.Size(86, 36)
  Me.BtnRecuperar.TabIndex = 227
  Me.BtnRecuperar.Text = "Recuperar"
  Me.BtnRecuperar.UseVisualStyleBackColor = False
  Me.BtnRecuperar.Visible = False
  '
  'LabelFecMod
  '
  Me.LabelFecMod.AutoSize = True
  Me.LabelFecMod.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.LabelFecMod.Location = New System.Drawing.Point(830, 35)
  Me.LabelFecMod.Name = "LabelFecMod"
  Me.LabelFecMod.Size = New System.Drawing.Size(131, 15)
  Me.LabelFecMod.TabIndex = 229
  Me.LabelFecMod.Text = "Fecha de Modificación"
  Me.LabelFecMod.Visible = False
  '
  'CBFecMod
  '
  Me.CBFecMod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CBFecMod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CBFecMod.FormattingEnabled = True
  Me.CBFecMod.Location = New System.Drawing.Point(833, 53)
  Me.CBFecMod.Name = "CBFecMod"
  Me.CBFecMod.Size = New System.Drawing.Size(128, 21)
  Me.CBFecMod.TabIndex = 230
  Me.CBFecMod.Visible = False
  '
  'CmbArticulo
  '
  Me.CmbArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.CmbArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.CmbArticulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.CmbArticulo.FormattingEnabled = True
  Me.CmbArticulo.Location = New System.Drawing.Point(123, 64)
  Me.CmbArticulo.Name = "CmbArticulo"
  Me.CmbArticulo.Size = New System.Drawing.Size(140, 21)
  Me.CmbArticulo.TabIndex = 231
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(65, 65)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(55, 17)
  Me.Label3.TabIndex = 232
  Me.Label3.Text = "Artículo"
  '
  'Inventario
  '
  Me.AcceptButton = Me.Button2
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1413, 637)
  Me.Controls.Add(Me.CmbArticulo)
  Me.Controls.Add(Me.Label3)
  Me.Controls.Add(Me.CBFecMod)
  Me.Controls.Add(Me.LabelFecMod)
  Me.Controls.Add(Me.BtnRecuperar)
  Me.Controls.Add(Me.CKRecuperar)
  Me.Controls.Add(Me.CKGuardar)
  Me.Controls.Add(Me.BSave)
  Me.Controls.Add(Me.Button3)
  Me.Controls.Add(Me.ButtonCARGAR)
  Me.Controls.Add(Me.BExcel)
  Me.Controls.Add(Me.Button2)
  Me.Controls.Add(Me.Label1)
  Me.Controls.Add(Me.CBLinea)
  Me.Controls.Add(Me.Estatus)
  Me.Controls.Add(Me.Label2)
  Me.Controls.Add(Me.CBAlmacen)
  Me.Controls.Add(Me.DGInventario)
  Me.Name = "Inventario"
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Inventario"
  CType(Me.DGInventario, System.ComponentModel.ISupportInitialize).EndInit()
  Me.Estatus.ResumeLayout(False)
  Me.Estatus.PerformLayout()
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub
 Friend WithEvents DGInventario As System.Windows.Forms.DataGridView
    Friend WithEvents CBAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Estatus As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents BSave As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents ButtonCARGAR As System.Windows.Forms.Button
    Friend WithEvents BExcel As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CBLinea As System.Windows.Forms.ComboBox
    Friend WithEvents CKGuardar As System.Windows.Forms.CheckBox
    Friend WithEvents CKRecuperar As System.Windows.Forms.CheckBox
    Friend WithEvents BtnRecuperar As System.Windows.Forms.Button
    Friend WithEvents LabelFecMod As System.Windows.Forms.Label
    Friend WithEvents CBFecMod As System.Windows.Forms.ComboBox
 Friend WithEvents CmbArticulo As ComboBox
 Friend WithEvents Label3 As Label
End Class
