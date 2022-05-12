<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmpleadoAlta
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EmpleadoAlta))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TBNumEmp = New System.Windows.Forms.TextBox()
        Me.TBApePat = New System.Windows.Forms.TextBox()
        Me.TBApeMat = New System.Windows.Forms.TextBox()
        Me.DTPFecIng = New System.Windows.Forms.DateTimePicker()
        Me.TBNomEmp = New System.Windows.Forms.TextBox()
        Me.DTPFecIMSS = New System.Windows.Forms.DateTimePicker()
        Me.CBSucursal = New System.Windows.Forms.ComboBox()
        Me.BSave = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DGVacaciones = New System.Windows.Forms.DataGridView()
        CType(Me.DGVacaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 66)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Apellido" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Paterno"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(201, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 26)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Apellido" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Materno"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(385, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Nombre(s)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(201, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 26)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Fecha " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "alta IMSS"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 26)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Fecha de" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ingreso"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 158)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Ubicación"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 26)
        Me.Label7.TabIndex = 536
        Me.Label7.Text = "No. de " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Empleado"
        '
        'TBNumEmp
        '
        Me.TBNumEmp.Location = New System.Drawing.Point(75, 20)
        Me.TBNumEmp.Name = "TBNumEmp"
        Me.TBNumEmp.Size = New System.Drawing.Size(100, 20)
        Me.TBNumEmp.TabIndex = 537
        '
        'TBApePat
        '
        Me.TBApePat.Location = New System.Drawing.Point(75, 63)
        Me.TBApePat.Name = "TBApePat"
        Me.TBApePat.Size = New System.Drawing.Size(100, 20)
        Me.TBApePat.TabIndex = 538
        '
        'TBApeMat
        '
        Me.TBApeMat.Location = New System.Drawing.Point(260, 63)
        Me.TBApeMat.Name = "TBApeMat"
        Me.TBApeMat.Size = New System.Drawing.Size(100, 20)
        Me.TBApeMat.TabIndex = 545
        '
        'DTPFecIng
        '
        Me.DTPFecIng.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFecIng.Location = New System.Drawing.Point(75, 115)
        Me.DTPFecIng.Name = "DTPFecIng"
        Me.DTPFecIng.Size = New System.Drawing.Size(100, 20)
        Me.DTPFecIng.TabIndex = 552
        '
        'TBNomEmp
        '
        Me.TBNomEmp.Location = New System.Drawing.Point(456, 60)
        Me.TBNomEmp.Name = "TBNomEmp"
        Me.TBNomEmp.Size = New System.Drawing.Size(159, 20)
        Me.TBNomEmp.TabIndex = 551
        '
        'DTPFecIMSS
        '
        Me.DTPFecIMSS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFecIMSS.Location = New System.Drawing.Point(260, 115)
        Me.DTPFecIMSS.Name = "DTPFecIMSS"
        Me.DTPFecIMSS.Size = New System.Drawing.Size(100, 20)
        Me.DTPFecIMSS.TabIndex = 556
        '
        'CBSucursal
        '
        Me.CBSucursal.FormattingEnabled = True
        Me.CBSucursal.Location = New System.Drawing.Point(75, 155)
        Me.CBSucursal.Name = "CBSucursal"
        Me.CBSucursal.Size = New System.Drawing.Size(121, 21)
        Me.CBSucursal.TabIndex = 559
        '
        'BSave
        '
        Me.BSave.Image = CType(resources.GetObject("BSave.Image"), System.Drawing.Image)
        Me.BSave.Location = New System.Drawing.Point(317, 158)
        Me.BSave.Name = "BSave"
        Me.BSave.Size = New System.Drawing.Size(43, 35)
        Me.BSave.TabIndex = 561
        Me.BSave.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.AliceBlue
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button2.Location = New System.Drawing.Point(189, 10)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(66, 36)
        Me.Button2.TabIndex = 562
        Me.Button2.Text = "Buscar"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'DGVacaciones
        '
        Me.DGVacaciones.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGVacaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVacaciones.Location = New System.Drawing.Point(26, 211)
        Me.DGVacaciones.Name = "DGVacaciones"
        Me.DGVacaciones.ReadOnly = True
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVacaciones.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVacaciones.Size = New System.Drawing.Size(589, 294)
        Me.DGVacaciones.TabIndex = 563
        '
        'EmpleadoAlta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 530)
        Me.Controls.Add(Me.DGVacaciones)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.BSave)
        Me.Controls.Add(Me.CBSucursal)
        Me.Controls.Add(Me.DTPFecIMSS)
        Me.Controls.Add(Me.DTPFecIng)
        Me.Controls.Add(Me.TBNomEmp)
        Me.Controls.Add(Me.TBApeMat)
        Me.Controls.Add(Me.TBApePat)
        Me.Controls.Add(Me.TBNumEmp)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "EmpleadoAlta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Alta de Empleados"
        CType(Me.DGVacaciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TBNumEmp As System.Windows.Forms.TextBox
    Friend WithEvents TBApePat As System.Windows.Forms.TextBox
    Friend WithEvents TBApeMat As System.Windows.Forms.TextBox
    Friend WithEvents DTPFecIng As System.Windows.Forms.DateTimePicker
    Friend WithEvents TBNomEmp As System.Windows.Forms.TextBox
    Friend WithEvents DTPFecIMSS As System.Windows.Forms.DateTimePicker
    Friend WithEvents CBSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents BSave As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents DGVacaciones As System.Windows.Forms.DataGridView
End Class
