<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Boletin
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
        Me.DGBoletin = New System.Windows.Forms.DataGridView()
        Me.dtFin = New System.Windows.Forms.DateTimePicker()
        Me.dtIni = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TBDia = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CBAlmacen = New System.Windows.Forms.ComboBox()
        Me.BExcel = New System.Windows.Forms.Button()
        CType(Me.DGBoletin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGBoletin
        '
        Me.DGBoletin.AllowUserToAddRows = False
        Me.DGBoletin.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGBoletin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGBoletin.Location = New System.Drawing.Point(31, 134)
        Me.DGBoletin.Name = "DGBoletin"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGBoletin.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGBoletin.Size = New System.Drawing.Size(857, 377)
        Me.DGBoletin.TabIndex = 0
        '
        'dtFin
        '
        Me.dtFin.Location = New System.Drawing.Point(134, 78)
        Me.dtFin.Name = "dtFin"
        Me.dtFin.Size = New System.Drawing.Size(200, 20)
        Me.dtFin.TabIndex = 14
        Me.dtFin.Value = New Date(2016, 4, 25, 16, 34, 23, 0)
        '
        'dtIni
        '
        Me.dtIni.Location = New System.Drawing.Point(134, 46)
        Me.dtIni.Name = "dtIni"
        Me.dtIni.Size = New System.Drawing.Size(200, 20)
        Me.dtIni.TabIndex = 13
        Me.dtIni.Value = New Date(2015, 1, 1, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(40, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Fecha Inicio"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(43, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Fecha Final"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(522, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Día"
        '
        'TBDia
        '
        Me.TBDia.Location = New System.Drawing.Point(525, 70)
        Me.TBDia.Name = "TBDia"
        Me.TBDia.Size = New System.Drawing.Size(100, 20)
        Me.TBDia.TabIndex = 18
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button1.Location = New System.Drawing.Point(685, 55)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 31)
        Me.Button1.TabIndex = 19
        Me.Button1.Text = "Consultar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(363, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Almacén"
        '
        'CBAlmacen
        '
        Me.CBAlmacen.FormattingEnabled = True
        Me.CBAlmacen.Location = New System.Drawing.Point(366, 70)
        Me.CBAlmacen.Name = "CBAlmacen"
        Me.CBAlmacen.Size = New System.Drawing.Size(121, 21)
        Me.CBAlmacen.TabIndex = 21
        '
        'BExcel
        '
        Me.BExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BExcel.Location = New System.Drawing.Point(852, 57)
        Me.BExcel.Name = "BExcel"
        Me.BExcel.Size = New System.Drawing.Size(36, 34)
        Me.BExcel.TabIndex = 181
        Me.BExcel.UseVisualStyleBackColor = True
        '
        'Boletin
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(918, 526)
        Me.Controls.Add(Me.BExcel)
        Me.Controls.Add(Me.CBAlmacen)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TBDia)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtFin)
        Me.Controls.Add(Me.dtIni)
        Me.Controls.Add(Me.DGBoletin)
        Me.Name = "Boletin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Boletin"
        CType(Me.DGBoletin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGBoletin As System.Windows.Forms.DataGridView
    Friend WithEvents dtFin As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TBDia As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CBAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents BExcel As System.Windows.Forms.Button
End Class
