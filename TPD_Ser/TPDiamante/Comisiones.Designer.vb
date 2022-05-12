<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Comisiones
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
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DgTotalAgte = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.BtnDetalle = New System.Windows.Forms.Button()
        Me.DgDetAgte = New System.Windows.Forms.DataGridView()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.BtnTotal = New System.Windows.Forms.Button()
        Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DgTotalAgte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgDetAgte, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(667, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Detalle"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(125, 76)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 17)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Comisiones"
        '
        'DgTotalAgte
        '
        Me.DgTotalAgte.AllowUserToAddRows = False
        Me.DgTotalAgte.AllowUserToDeleteRows = False
        Me.DgTotalAgte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgTotalAgte.Location = New System.Drawing.Point(125, 94)
        Me.DgTotalAgte.Name = "DgTotalAgte"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgTotalAgte.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgTotalAgte.Size = New System.Drawing.Size(528, 534)
        Me.DgTotalAgte.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button1.Location = New System.Drawing.Point(125, 39)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 31)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Consultar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(386, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 17)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Fecha Término:"
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaTer.Location = New System.Drawing.Point(497, 6)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(240, 23)
        Me.DtpFechaTer.TabIndex = 1
        Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(36, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 17)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Fecha Inicio:"
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaIni.Location = New System.Drawing.Point(126, 7)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(240, 23)
        Me.DtpFechaIni.TabIndex = 0
        Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'BtnDetalle
        '
        Me.BtnDetalle.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnDetalle.Location = New System.Drawing.Point(628, 42)
        Me.BtnDetalle.Name = "BtnDetalle"
        Me.BtnDetalle.Size = New System.Drawing.Size(36, 34)
        Me.BtnDetalle.TabIndex = 6
        Me.BtnDetalle.UseVisualStyleBackColor = True
        '
        'DgDetAgte
        '
        Me.DgDetAgte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgDetAgte.Location = New System.Drawing.Point(39, 193)
        Me.DgDetAgte.Name = "DgDetAgte"
        Me.DgDetAgte.Size = New System.Drawing.Size(699, 387)
        Me.DgDetAgte.TabIndex = 147
        Me.DgDetAgte.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(536, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(40, 17)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "Total"
        '
        'BtnTotal
        '
        Me.BtnTotal.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnTotal.Location = New System.Drawing.Point(497, 41)
        Me.BtnTotal.Name = "BtnTotal"
        Me.BtnTotal.Size = New System.Drawing.Size(36, 34)
        Me.BtnTotal.TabIndex = 5
        Me.BtnTotal.UseVisualStyleBackColor = True
        '
        'CmbAgteVta
        '
        Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbAgteVta.FormattingEnabled = True
        Me.CmbAgteVta.Location = New System.Drawing.Point(125, 61)
        Me.CmbAgteVta.Name = "CmbAgteVta"
        Me.CmbAgteVta.Size = New System.Drawing.Size(240, 21)
        Me.CmbAgteVta.TabIndex = 2
        Me.CmbAgteVta.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 17)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Agente de ventas"
        Me.Label1.Visible = False
        '
        'Comisiones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(788, 630)
        Me.Controls.Add(Me.CmbAgteVta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.BtnTotal)
        Me.Controls.Add(Me.DgDetAgte)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.BtnDetalle)
        Me.Controls.Add(Me.DgTotalAgte)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DtpFechaTer)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DtpFechaIni)
        Me.Name = "Comisiones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comisiones"
        CType(Me.DgTotalAgte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgDetAgte, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BtnDetalle As System.Windows.Forms.Button
    Friend WithEvents DgTotalAgte As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents DgDetAgte As System.Windows.Forms.DataGridView
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents BtnTotal As System.Windows.Forms.Button
    Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
