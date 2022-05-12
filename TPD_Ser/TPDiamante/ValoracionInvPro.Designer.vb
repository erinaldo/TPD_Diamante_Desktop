<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ValoracionInvPro
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Estatus = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.DGLineas = New System.Windows.Forms.DataGridView()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Almacen = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbAlmacen = New System.Windows.Forms.ComboBox()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CmbArticulo = New System.Windows.Forms.ComboBox()
        Me.CmbGrupoArticulo = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.DGArticulos = New System.Windows.Forms.DataGridView()
        Me.CKBCli = New System.Windows.Forms.CheckBox()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.Estatus.SuspendLayout()
        CType(Me.DGLineas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DGArticulos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Estatus)
        Me.GroupBox1.Controls.Add(Me.DGLineas)
        Me.GroupBox1.Location = New System.Drawing.Point(37, 100)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(998, 252)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Líneas"
        '
        'Estatus
        '
        Me.Estatus.Controls.Add(Me.Label3)
        Me.Estatus.Controls.Add(Me.ProgressBar1)
        Me.Estatus.Location = New System.Drawing.Point(368, 98)
        Me.Estatus.Name = "Estatus"
        Me.Estatus.Size = New System.Drawing.Size(167, 54)
        Me.Estatus.TabIndex = 189
        Me.Estatus.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(39, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 113
        Me.Label3.Text = "Cargando archivo"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 29)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(140, 17)
        Me.ProgressBar1.TabIndex = 112
        '
        'DGLineas
        '
        Me.DGLineas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGLineas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGLineas.Location = New System.Drawing.Point(6, 13)
        Me.DGLineas.Name = "DGLineas"
        Me.DGLineas.ReadOnly = True
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGLineas.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGLineas.Size = New System.Drawing.Size(986, 233)
        Me.DGLineas.TabIndex = 188
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(6, 13)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(839, 233)
        Me.DataGridView2.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DataGridView2)
        Me.GroupBox2.Location = New System.Drawing.Point(37, 358)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(851, 252)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Artículos"
        '
        'Almacen
        '
        Me.Almacen.AutoSize = True
        Me.Almacen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
        Me.Almacen.Location = New System.Drawing.Point(42, 14)
        Me.Almacen.Name = "Almacen"
        Me.Almacen.Size = New System.Drawing.Size(55, 15)
        Me.Almacen.TabIndex = 136
        Me.Almacen.Text = "Almacen"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
        Me.Label4.Location = New System.Drawing.Point(189, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 15)
        Me.Label4.TabIndex = 138
        Me.Label4.Text = "Línea"
        '
        'cmbAlmacen
        '
        Me.cmbAlmacen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbAlmacen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAlmacen.FormattingEnabled = True
        Me.cmbAlmacen.Location = New System.Drawing.Point(45, 32)
        Me.cmbAlmacen.Name = "cmbAlmacen"
        Me.cmbAlmacen.Size = New System.Drawing.Size(128, 21)
        Me.cmbAlmacen.TabIndex = 135
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Location = New System.Drawing.Point(45, 74)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(218, 20)
        Me.DtpFechaIni.TabIndex = 140
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
        Me.Label1.Location = New System.Drawing.Point(42, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 15)
        Me.Label1.TabIndex = 141
        Me.Label1.Text = "Periodo de ventas del"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
        Me.Label2.Location = New System.Drawing.Point(341, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 15)
        Me.Label2.TabIndex = 143
        Me.Label2.Text = "al"
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Location = New System.Drawing.Point(344, 74)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(218, 20)
        Me.DtpFechaTer.TabIndex = 142
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(341, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 17)
        Me.Label6.TabIndex = 145
        Me.Label6.Text = "Artículo"
        '
        'CmbArticulo
        '
        Me.CmbArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbArticulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbArticulo.FormattingEnabled = True
        Me.CmbArticulo.Location = New System.Drawing.Point(344, 32)
        Me.CmbArticulo.Name = "CmbArticulo"
        Me.CmbArticulo.Size = New System.Drawing.Size(128, 21)
        Me.CmbArticulo.TabIndex = 147
        '
        'CmbGrupoArticulo
        '
        Me.CmbGrupoArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbGrupoArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbGrupoArticulo.FormattingEnabled = True
        Me.CmbGrupoArticulo.Location = New System.Drawing.Point(192, 32)
        Me.CmbGrupoArticulo.Name = "CmbGrupoArticulo"
        Me.CmbGrupoArticulo.Size = New System.Drawing.Size(128, 21)
        Me.CmbGrupoArticulo.TabIndex = 146
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.AliceBlue
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button2.Location = New System.Drawing.Point(701, 32)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 36)
        Me.Button2.TabIndex = 148
        Me.Button2.Text = "Consultar"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.Panel1)
        Me.GroupBox4.Controls.Add(Me.DGArticulos)
        Me.GroupBox4.Location = New System.Drawing.Point(37, 358)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1062, 252)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Artículos"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.ProgressBar2)
        Me.Panel1.Location = New System.Drawing.Point(461, 91)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(167, 54)
        Me.Panel1.TabIndex = 189
        Me.Panel1.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(39, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 13)
        Me.Label5.TabIndex = 113
        Me.Label5.Text = "Cargando archivo"
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Location = New System.Drawing.Point(12, 29)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(140, 17)
        Me.ProgressBar2.TabIndex = 112
        '
        'DGArticulos
        '
        Me.DGArticulos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGArticulos.Location = New System.Drawing.Point(6, 13)
        Me.DGArticulos.Name = "DGArticulos"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGArticulos.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DGArticulos.Size = New System.Drawing.Size(1050, 233)
        Me.DGArticulos.TabIndex = 189
        '
        'CKBCli
        '
        Me.CKBCli.AutoSize = True
        Me.CKBCli.Location = New System.Drawing.Point(609, 77)
        Me.CKBCli.Name = "CKBCli"
        Me.CKBCli.Size = New System.Drawing.Size(100, 17)
        Me.CKBCli.TabIndex = 149
        Me.CKBCli.Text = "Clientes propios"
        Me.CKBCli.UseVisualStyleBackColor = True
        '
        'BtnExcel
        '
        Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnExcel.Location = New System.Drawing.Point(839, 56)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(43, 43)
        Me.BtnExcel.TabIndex = 150
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.Button1.Location = New System.Drawing.Point(1050, 309)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(43, 43)
        Me.Button1.TabIndex = 151
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ValoracionInvPro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1111, 622)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.CKBCli)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.CmbArticulo)
        Me.Controls.Add(Me.CmbGrupoArticulo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DtpFechaTer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DtpFechaIni)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Almacen)
        Me.Controls.Add(Me.cmbAlmacen)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ValoracionInvPro"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Valoracion de inventarios por producto"
        Me.GroupBox1.ResumeLayout(False)
        Me.Estatus.ResumeLayout(False)
        Me.Estatus.PerformLayout()
        CType(Me.DGLineas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DGArticulos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Almacen As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CmbArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents CmbGrupoArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents CKBCli As System.Windows.Forms.CheckBox
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DGLineas As System.Windows.Forms.DataGridView
    Friend WithEvents DGArticulos As System.Windows.Forms.DataGridView
    Friend WithEvents Estatus As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar2 As System.Windows.Forms.ProgressBar
End Class
