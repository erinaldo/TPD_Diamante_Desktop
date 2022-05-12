<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NC_Facts
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DgvNotasC = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DgvDev = New System.Windows.Forms.DataGridView()
        Me.TxtTotFact = New System.Windows.Forms.TextBox()
        Me.TxtTotDev = New System.Windows.Forms.TextBox()
        Me.TxtTotNeto = New System.Windows.Forms.TextBox()
        Me.TxtTotNC = New System.Windows.Forms.TextBox()
        Me.TxtPor = New System.Windows.Forms.TextBox()
        Me.TxtSTotNC = New System.Windows.Forms.TextBox()
        Me.TxtSTotNeto = New System.Windows.Forms.TextBox()
        Me.TxtSTotDev = New System.Windows.Forms.TextBox()
        Me.TxtSTotFact = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DgvPagos = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbSerie = New System.Windows.Forms.ComboBox()
        Me.BtnConsultar = New System.Windows.Forms.Button()
        Me.BtnExcel = New System.Windows.Forms.Button()
        Me.BtnImprimir = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbFiltraDoc = New System.Windows.Forms.ComboBox()
        CType(Me.DgvNotasC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvDev, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvPagos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DgvNotasC
        '
        Me.DgvNotasC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvNotasC.Location = New System.Drawing.Point(7, 63)
        Me.DgvNotasC.Name = "DgvNotasC"
        Me.DgvNotasC.RowHeadersWidth = 51
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvNotasC.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DgvNotasC.Size = New System.Drawing.Size(1517, 464)
        Me.DgvNotasC.TabIndex = 68
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Gainsboro
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(284, 529)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 17)
        Me.Label2.TabIndex = 153
        Me.Label2.Text = "Pagos"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Gainsboro
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(7, 529)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(93, 17)
        Me.Label11.TabIndex = 151
        Me.Label11.Text = "Devoluciones"
        '
        'DgvDev
        '
        Me.DgvDev.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvDev.Location = New System.Drawing.Point(7, 547)
        Me.DgvDev.Name = "DgvDev"
        Me.DgvDev.RowHeadersWidth = 51
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvDev.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DgvDev.Size = New System.Drawing.Size(264, 124)
        Me.DgvDev.TabIndex = 150
        '
        'TxtTotFact
        '
        Me.TxtTotFact.Location = New System.Drawing.Point(890, 528)
        Me.TxtTotFact.Name = "TxtTotFact"
        Me.TxtTotFact.ReadOnly = True
        Me.TxtTotFact.Size = New System.Drawing.Size(73, 20)
        Me.TxtTotFact.TabIndex = 154
        Me.TxtTotFact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtTotDev
        '
        Me.TxtTotDev.Location = New System.Drawing.Point(963, 528)
        Me.TxtTotDev.Name = "TxtTotDev"
        Me.TxtTotDev.ReadOnly = True
        Me.TxtTotDev.Size = New System.Drawing.Size(65, 20)
        Me.TxtTotDev.TabIndex = 155
        Me.TxtTotDev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtTotNeto
        '
        Me.TxtTotNeto.Location = New System.Drawing.Point(1028, 528)
        Me.TxtTotNeto.Name = "TxtTotNeto"
        Me.TxtTotNeto.ReadOnly = True
        Me.TxtTotNeto.Size = New System.Drawing.Size(73, 20)
        Me.TxtTotNeto.TabIndex = 156
        Me.TxtTotNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtTotNC
        '
        Me.TxtTotNC.Location = New System.Drawing.Point(1354, 528)
        Me.TxtTotNC.Name = "TxtTotNC"
        Me.TxtTotNC.ReadOnly = True
        Me.TxtTotNC.Size = New System.Drawing.Size(67, 20)
        Me.TxtTotNC.TabIndex = 157
        Me.TxtTotNC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtPor
        '
        Me.TxtPor.Location = New System.Drawing.Point(1421, 528)
        Me.TxtPor.Name = "TxtPor"
        Me.TxtPor.ReadOnly = True
        Me.TxtPor.Size = New System.Drawing.Size(47, 20)
        Me.TxtPor.TabIndex = 158
        '
        'TxtSTotNC
        '
        Me.TxtSTotNC.Location = New System.Drawing.Point(1354, 577)
        Me.TxtSTotNC.Name = "TxtSTotNC"
        Me.TxtSTotNC.ReadOnly = True
        Me.TxtSTotNC.Size = New System.Drawing.Size(67, 20)
        Me.TxtSTotNC.TabIndex = 162
        Me.TxtSTotNC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtSTotNeto
        '
        Me.TxtSTotNeto.Location = New System.Drawing.Point(1028, 577)
        Me.TxtSTotNeto.Name = "TxtSTotNeto"
        Me.TxtSTotNeto.ReadOnly = True
        Me.TxtSTotNeto.Size = New System.Drawing.Size(73, 20)
        Me.TxtSTotNeto.TabIndex = 161
        Me.TxtSTotNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtSTotDev
        '
        Me.TxtSTotDev.Location = New System.Drawing.Point(963, 577)
        Me.TxtSTotDev.Name = "TxtSTotDev"
        Me.TxtSTotDev.ReadOnly = True
        Me.TxtSTotDev.Size = New System.Drawing.Size(65, 20)
        Me.TxtSTotDev.TabIndex = 160
        Me.TxtSTotDev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtSTotFact
        '
        Me.TxtSTotFact.Location = New System.Drawing.Point(890, 577)
        Me.TxtSTotFact.Name = "TxtSTotFact"
        Me.TxtSTotFact.ReadOnly = True
        Me.TxtSTotFact.Size = New System.Drawing.Size(73, 20)
        Me.TxtSTotFact.TabIndex = 159
        Me.TxtSTotFact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Gainsboro
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(873, 529)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(16, 17)
        Me.Label4.TabIndex = 164
        Me.Label4.Text = "$"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Gainsboro
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(1337, 529)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(16, 17)
        Me.Label6.TabIndex = 165
        Me.Label6.Text = "$"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Gainsboro
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(824, 578)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 17)
        Me.Label7.TabIndex = 166
        Me.Label7.Text = "$ Sin IVA"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Gainsboro
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(1288, 578)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 17)
        Me.Label8.TabIndex = 167
        Me.Label8.Text = "$ Sin IVA"
        '
        'DgvPagos
        '
        Me.DgvPagos.AllowUserToAddRows = False
        Me.DgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvPagos.Location = New System.Drawing.Point(284, 547)
        Me.DgvPagos.Name = "DgvPagos"
        Me.DgvPagos.RowHeadersWidth = 51
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvPagos.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.DgvPagos.Size = New System.Drawing.Size(254, 123)
        Me.DgvPagos.TabIndex = 169
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DtpFechaIni)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.DtpFechaTer)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.CmbSerie)
        Me.Panel1.Controls.Add(Me.BtnConsultar)
        Me.Panel1.Location = New System.Drawing.Point(7, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(816, 53)
        Me.Panel1.TabIndex = 168
        '
        'DtpFechaIni
        '
        Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaIni.Location = New System.Drawing.Point(11, 21)
        Me.DtpFechaIni.Name = "DtpFechaIni"
        Me.DtpFechaIni.Size = New System.Drawing.Size(265, 23)
        Me.DtpFechaIni.TabIndex = 63
        Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 17)
        Me.Label5.TabIndex = 66
        Me.Label5.Text = "Fecha Inicio:"
        '
        'DtpFechaTer
        '
        Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFechaTer.Location = New System.Drawing.Point(296, 21)
        Me.DtpFechaTer.Name = "DtpFechaTer"
        Me.DtpFechaTer.Size = New System.Drawing.Size(265, 23)
        Me.DtpFechaTer.TabIndex = 64
        Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(294, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 17)
        Me.Label3.TabIndex = 67
        Me.Label3.Text = "Fecha Término:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(593, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 17)
        Me.Label1.TabIndex = 72
        Me.Label1.Text = "Serie"
        '
        'CmbSerie
        '
        Me.CmbSerie.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbSerie.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbSerie.FormattingEnabled = True
        Me.CmbSerie.ItemHeight = 13
        Me.CmbSerie.Location = New System.Drawing.Point(593, 22)
        Me.CmbSerie.Name = "CmbSerie"
        Me.CmbSerie.Size = New System.Drawing.Size(80, 21)
        Me.CmbSerie.TabIndex = 71
        '
        'BtnConsultar
        '
        Me.BtnConsultar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnConsultar.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnConsultar.Location = New System.Drawing.Point(706, 16)
        Me.BtnConsultar.Name = "BtnConsultar"
        Me.BtnConsultar.Size = New System.Drawing.Size(75, 31)
        Me.BtnConsultar.TabIndex = 65
        Me.BtnConsultar.Text = "Consultar"
        Me.BtnConsultar.UseVisualStyleBackColor = True
        '
        'BtnExcel
        '
        Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
        Me.BtnExcel.Location = New System.Drawing.Point(1481, 20)
        Me.BtnExcel.Name = "BtnExcel"
        Me.BtnExcel.Size = New System.Drawing.Size(43, 39)
        Me.BtnExcel.TabIndex = 171
        Me.BtnExcel.UseVisualStyleBackColor = True
        '
        'BtnImprimir
        '
        Me.BtnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprimir.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnImprimir.Image = Global.TPDiamante.My.Resources.Resources.printer
        Me.BtnImprimir.Location = New System.Drawing.Point(1354, 18)
        Me.BtnImprimir.Name = "BtnImprimir"
        Me.BtnImprimir.Size = New System.Drawing.Size(43, 39)
        Me.BtnImprimir.TabIndex = 170
        Me.BtnImprimir.UseVisualStyleBackColor = True
        Me.BtnImprimir.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1002, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(152, 17)
        Me.Label9.TabIndex = 173
        Me.Label9.Text = "Filtrar Tipo Documento"
        '
        'cmbFiltraDoc
        '
        Me.cmbFiltraDoc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbFiltraDoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFiltraDoc.FormattingEnabled = True
        Me.cmbFiltraDoc.ItemHeight = 13
        Me.cmbFiltraDoc.Location = New System.Drawing.Point(1003, 24)
        Me.cmbFiltraDoc.Name = "cmbFiltraDoc"
        Me.cmbFiltraDoc.Size = New System.Drawing.Size(152, 21)
        Me.cmbFiltraDoc.TabIndex = 172
        '
        'NC_Facts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1524, 696)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cmbFiltraDoc)
        Me.Controls.Add(Me.BtnExcel)
        Me.Controls.Add(Me.BtnImprimir)
        Me.Controls.Add(Me.DgvPagos)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtSTotNC)
        Me.Controls.Add(Me.TxtSTotNeto)
        Me.Controls.Add(Me.TxtSTotDev)
        Me.Controls.Add(Me.TxtSTotFact)
        Me.Controls.Add(Me.TxtPor)
        Me.Controls.Add(Me.TxtTotNC)
        Me.Controls.Add(Me.TxtTotNeto)
        Me.Controls.Add(Me.TxtTotDev)
        Me.Controls.Add(Me.TxtTotFact)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.DgvDev)
        Me.Controls.Add(Me.DgvNotasC)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "NC_Facts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Aplicación de Notas de Credito"
        CType(Me.DgvNotasC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvDev, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvPagos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DgvNotasC As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents DgvDev As System.Windows.Forms.DataGridView
    Friend WithEvents TxtTotFact As System.Windows.Forms.TextBox
    Friend WithEvents TxtTotDev As System.Windows.Forms.TextBox
    Friend WithEvents TxtTotNeto As System.Windows.Forms.TextBox
    Friend WithEvents TxtTotNC As System.Windows.Forms.TextBox
    Friend WithEvents TxtPor As System.Windows.Forms.TextBox
    Friend WithEvents TxtSTotNC As System.Windows.Forms.TextBox
    Friend WithEvents TxtSTotNeto As System.Windows.Forms.TextBox
    Friend WithEvents TxtSTotDev As System.Windows.Forms.TextBox
    Friend WithEvents TxtSTotFact As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DgvPagos As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DtpFechaIni As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents DtpFechaTer As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents CmbSerie As ComboBox
    Friend WithEvents BtnConsultar As Button
    Friend WithEvents BtnExcel As Button
    Friend WithEvents BtnImprimir As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents cmbFiltraDoc As ComboBox
End Class
