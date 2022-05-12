<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tracking
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbMes = New System.Windows.Forms.ComboBox()
        Me.CmbAgte = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxBxMeta = New System.Windows.Forms.TextBox()
        Me.Consultar = New System.Windows.Forms.Button()
        Me.DGVResultado = New System.Windows.Forms.DataGridView()
        Me.BtnRecibos = New System.Windows.Forms.Button()
        Me.Estatus = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCorreo = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.lbMetaMensual = New System.Windows.Forms.Label()
        CType(Me.DGVResultado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Estatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(31, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Mes:"
        '
        'CmbMes
        '
        Me.CmbMes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbMes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbMes.FormattingEnabled = True
        Me.CmbMes.Location = New System.Drawing.Point(162, 18)
        Me.CmbMes.Name = "CmbMes"
        Me.CmbMes.Size = New System.Drawing.Size(185, 21)
        Me.CmbMes.TabIndex = 1
        '
        'CmbAgte
        '
        Me.CmbAgte.FormattingEnabled = True
        Me.CmbAgte.Location = New System.Drawing.Point(162, 45)
        Me.CmbAgte.Name = "CmbAgte"
        Me.CmbAgte.Size = New System.Drawing.Size(185, 21)
        Me.CmbAgte.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(31, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Meta Mensual:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(31, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Agente de Ventas:"
        '
        'TxBxMeta
        '
        Me.TxBxMeta.Location = New System.Drawing.Point(353, 18)
        Me.TxBxMeta.Name = "TxBxMeta"
        Me.TxBxMeta.Size = New System.Drawing.Size(185, 20)
        Me.TxBxMeta.TabIndex = 5
        Me.TxBxMeta.Visible = False
        '
        'Consultar
        '
        Me.Consultar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Consultar.ForeColor = System.Drawing.Color.MediumBlue
        Me.Consultar.Location = New System.Drawing.Point(353, 73)
        Me.Consultar.Name = "Consultar"
        Me.Consultar.Size = New System.Drawing.Size(75, 37)
        Me.Consultar.TabIndex = 6
        Me.Consultar.Text = "Consultar"
        Me.Consultar.UseVisualStyleBackColor = True
        '
        'DGVResultado
        '
        Me.DGVResultado.AllowUserToAddRows = False
        Me.DGVResultado.AllowUserToDeleteRows = False
        Me.DGVResultado.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGVResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVResultado.Location = New System.Drawing.Point(12, 116)
        Me.DGVResultado.Name = "DGVResultado"
        Me.DGVResultado.ReadOnly = True
        Me.DGVResultado.Size = New System.Drawing.Size(1284, 500)
        Me.DGVResultado.TabIndex = 9
        '
        'BtnRecibos
        '
        Me.BtnRecibos.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnRecibos.Location = New System.Drawing.Point(434, 73)
        Me.BtnRecibos.Name = "BtnRecibos"
        Me.BtnRecibos.Size = New System.Drawing.Size(36, 37)
        Me.BtnRecibos.TabIndex = 7
        Me.BtnRecibos.UseVisualStyleBackColor = True
        '
        'Estatus
        '
        Me.Estatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Estatus.Controls.Add(Me.ProgressBar1)
        Me.Estatus.Controls.Add(Me.Label6)
        Me.Estatus.Location = New System.Drawing.Point(622, 335)
        Me.Estatus.Name = "Estatus"
        Me.Estatus.Size = New System.Drawing.Size(150, 32)
        Me.Estatus.TabIndex = 10
        Me.Estatus.Visible = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(98, 8)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(43, 17)
        Me.ProgressBar1.TabIndex = 112
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 13)
        Me.Label6.TabIndex = 192
        Me.Label6.Text = "Cargando archivo:"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button1.Location = New System.Drawing.Point(476, 73)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 37)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Enviar Por Correo"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(581, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Días Hábiles:"
        Me.Label4.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(581, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 16)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Realizado:"
        Me.Label5.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(581, 73)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 16)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Diferencia:"
        Me.Label7.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(686, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 16)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Días Hábiles"
        Me.Label8.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.DarkRed
        Me.Label9.Location = New System.Drawing.Point(686, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(98, 16)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Días Hábiles"
        Me.Label9.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.DarkRed
        Me.Label10.Location = New System.Drawing.Point(686, 73)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(98, 16)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Días Hábiles"
        Me.Label10.Visible = False
        '
        'txtCorreo
        '
        Me.txtCorreo.Location = New System.Drawing.Point(353, 46)
        Me.txtCorreo.Name = "txtCorreo"
        Me.txtCorreo.ReadOnly = True
        Me.txtCorreo.Size = New System.Drawing.Size(198, 20)
        Me.txtCorreo.TabIndex = 17
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(790, 42)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(198, 20)
        Me.TextBox1.TabIndex = 9
        Me.TextBox1.Visible = False
        '
        'lbMetaMensual
        '
        Me.lbMetaMensual.AutoSize = True
        Me.lbMetaMensual.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMetaMensual.Location = New System.Drawing.Point(158, 73)
        Me.lbMetaMensual.Name = "lbMetaMensual"
        Me.lbMetaMensual.Size = New System.Drawing.Size(29, 20)
        Me.lbMetaMensual.TabIndex = 18
        Me.lbMetaMensual.Text = "$0"
        '
        'Tracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1308, 628)
        Me.Controls.Add(Me.lbMetaMensual)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.txtCorreo)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Estatus)
        Me.Controls.Add(Me.BtnRecibos)
        Me.Controls.Add(Me.DGVResultado)
        Me.Controls.Add(Me.Consultar)
        Me.Controls.Add(Me.TxBxMeta)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CmbAgte)
        Me.Controls.Add(Me.CmbMes)
        Me.Controls.Add(Me.Label3)
        Me.Name = "Tracking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reporte Tracking Diario"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DGVResultado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Estatus.ResumeLayout(False)
        Me.Estatus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CmbMes As System.Windows.Forms.ComboBox
    Friend WithEvents CmbAgte As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxBxMeta As System.Windows.Forms.TextBox
    Friend WithEvents Consultar As System.Windows.Forms.Button
    Friend WithEvents DGVResultado As System.Windows.Forms.DataGridView
    Friend WithEvents BtnRecibos As System.Windows.Forms.Button
    Friend WithEvents Estatus As System.Windows.Forms.Panel
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCorreo As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents lbMetaMensual As Label
End Class
