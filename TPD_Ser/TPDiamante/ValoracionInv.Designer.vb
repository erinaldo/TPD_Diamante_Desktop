<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ValoracionInv
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
        Me.DGinv = New System.Windows.Forms.DataGridView()
        Me.LblActualizar = New System.Windows.Forms.Label()
        Me.BtnActualizar = New System.Windows.Forms.Button()
        Me.BtnAgentes = New System.Windows.Forms.Button()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CkCliPro = New System.Windows.Forms.CheckBox()
        CType(Me.DGinv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGinv
        '
        Me.DGinv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGinv.Location = New System.Drawing.Point(24, 89)
        Me.DGinv.Name = "DGinv"
        Me.DGinv.Size = New System.Drawing.Size(551, 169)
        Me.DGinv.TabIndex = 0
        '
        'LblActualizar
        '
        Me.LblActualizar.AutoSize = True
        Me.LblActualizar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblActualizar.Location = New System.Drawing.Point(462, 63)
        Me.LblActualizar.Name = "LblActualizar"
        Me.LblActualizar.Size = New System.Drawing.Size(64, 13)
        Me.LblActualizar.TabIndex = 77
        Me.LblActualizar.Text = "Actualizar"
        '
        'BtnActualizar
        '
        Me.BtnActualizar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnActualizar.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnActualizar.Image = Global.TPDiamante.My.Resources.Resources.Recharger
        Me.BtnActualizar.Location = New System.Drawing.Point(474, 21)
        Me.BtnActualizar.Name = "BtnActualizar"
        Me.BtnActualizar.Size = New System.Drawing.Size(43, 39)
        Me.BtnActualizar.TabIndex = 76
        Me.BtnActualizar.UseVisualStyleBackColor = True
        '
        'BtnAgentes
        '
        Me.BtnAgentes.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnAgentes.Location = New System.Drawing.Point(539, 24)
        Me.BtnAgentes.Name = "BtnAgentes"
        Me.BtnAgentes.Size = New System.Drawing.Size(36, 34)
        Me.BtnAgentes.TabIndex = 161
        Me.BtnAgentes.UseVisualStyleBackColor = True
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Location = New System.Drawing.Point(24, 40)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(194, 20)
        Me.DtpFechaIni.TabIndex = 162
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Location = New System.Drawing.Point(238, 40)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(194, 20)
        Me.DtpFechaTer.TabIndex = 163
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 164
        Me.Label1.Text = "Periodo de ventas del "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(235, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 13)
        Me.Label2.TabIndex = 165
        Me.Label2.Text = "al"
        '
        'CkCliPro
        '
        Me.CkCliPro.AutoSize = True
        Me.CkCliPro.Location = New System.Drawing.Point(27, 66)
        Me.CkCliPro.Name = "CkCliPro"
        Me.CkCliPro.Size = New System.Drawing.Size(100, 17)
        Me.CkCliPro.TabIndex = 166
        Me.CkCliPro.Text = "Clientes propios"
        Me.CkCliPro.UseVisualStyleBackColor = True
        '
        'ValoracionInv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 270)
        Me.Controls.Add(Me.CkCliPro)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DtpFechaTer)
        Me.Controls.Add(Me.DtpFechaIni)
        Me.Controls.Add(Me.BtnAgentes)
        Me.Controls.Add(Me.LblActualizar)
        Me.Controls.Add(Me.BtnActualizar)
        Me.Controls.Add(Me.DGinv)
        Me.Name = "ValoracionInv"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Valoración de Inventarios por Almacén"
        CType(Me.DGinv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGinv As System.Windows.Forms.DataGridView
    Friend WithEvents LblActualizar As System.Windows.Forms.Label
    Friend WithEvents BtnActualizar As System.Windows.Forms.Button
    Friend WithEvents BtnAgentes As System.Windows.Forms.Button
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CkCliPro As System.Windows.Forms.CheckBox
End Class
