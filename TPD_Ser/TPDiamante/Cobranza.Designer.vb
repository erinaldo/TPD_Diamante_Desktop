<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cobranza
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Cobranza))
    Me.GrdConProd = New System.Windows.Forms.DataGridView()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.DtpPVtaTer = New System.Windows.Forms.DateTimePicker()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.DtpPVtaIni = New System.Windows.Forms.DateTimePicker()
    Me.BtnExcel = New System.Windows.Forms.Button()
    Me.BtnImprimir = New System.Windows.Forms.Button()
    Me.CmbACob = New System.Windows.Forms.ComboBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.CmbAVtas = New System.Windows.Forms.ComboBox()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.DtpPPagoTer = New System.Windows.Forms.DateTimePicker()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.DtpPPagoIni = New System.Windows.Forms.DateTimePicker()
    CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GrdConProd
    '
    Me.GrdConProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.GrdConProd.Location = New System.Drawing.Point(8, 92)
    Me.GrdConProd.Name = "GrdConProd"
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.GrdConProd.RowsDefaultCellStyle = DataGridViewCellStyle1
    Me.GrdConProd.Size = New System.Drawing.Size(1133, 521)
    Me.GrdConProd.TabIndex = 9
    '
    'Button1
    '
    Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
    Me.Button1.Location = New System.Drawing.Point(832, 5)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(92, 31)
    Me.Button1.TabIndex = 8
    Me.Button1.Text = "Consultar"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label3.Location = New System.Drawing.Point(311, 6)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(20, 17)
    Me.Label3.TabIndex = 80
    Me.Label3.Text = "a "
    '
    'DtpPVtaTer
    '
    Me.DtpPVtaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DtpPVtaTer.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.DtpPVtaTer.Location = New System.Drawing.Point(332, 3)
    Me.DtpPVtaTer.Name = "DtpPVtaTer"
    Me.DtpPVtaTer.Size = New System.Drawing.Size(94, 23)
    Me.DtpPVtaTer.TabIndex = 1
    Me.DtpPVtaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.Location = New System.Drawing.Point(12, 8)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(151, 17)
    Me.Label5.TabIndex = 75
    Me.Label5.Text = "Periodo de Ventas  De"
    '
    'DtpPVtaIni
    '
    Me.DtpPVtaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DtpPVtaIni.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.DtpPVtaIni.Location = New System.Drawing.Point(211, 3)
    Me.DtpPVtaIni.Name = "DtpPVtaIni"
    Me.DtpPVtaIni.Size = New System.Drawing.Size(94, 23)
    Me.DtpPVtaIni.TabIndex = 0
    Me.DtpPVtaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
    '
    'BtnExcel
    '
    Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
    Me.BtnExcel.Location = New System.Drawing.Point(881, 46)
    Me.BtnExcel.Name = "BtnExcel"
    Me.BtnExcel.Size = New System.Drawing.Size(43, 39)
    Me.BtnExcel.TabIndex = 10
    Me.BtnExcel.UseVisualStyleBackColor = True
    '
    'BtnImprimir
    '
    Me.BtnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.BtnImprimir.ForeColor = System.Drawing.Color.MediumBlue
    Me.BtnImprimir.Image = Global.TPDiamante.My.Resources.Resources.printer
    Me.BtnImprimir.Location = New System.Drawing.Point(832, 47)
    Me.BtnImprimir.Name = "BtnImprimir"
    Me.BtnImprimir.Size = New System.Drawing.Size(43, 39)
    Me.BtnImprimir.TabIndex = 85
    Me.BtnImprimir.UseVisualStyleBackColor = True
    Me.BtnImprimir.Visible = False
    '
    'CmbACob
    '
    Me.CmbACob.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbACob.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbACob.FormattingEnabled = True
    Me.CmbACob.Location = New System.Drawing.Point(599, 10)
    Me.CmbACob.Name = "CmbACob"
    Me.CmbACob.Size = New System.Drawing.Size(195, 21)
    Me.CmbACob.TabIndex = 99
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label9.Location = New System.Drawing.Point(457, 11)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(138, 17)
    Me.Label9.TabIndex = 100
    Me.Label9.Text = "Agente de Cobranza"
    '
    'CmbAVtas
    '
    Me.CmbAVtas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbAVtas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbAVtas.FormattingEnabled = True
    Me.CmbAVtas.Location = New System.Drawing.Point(599, 54)
    Me.CmbAVtas.Name = "CmbAVtas"
    Me.CmbAVtas.Size = New System.Drawing.Size(195, 21)
    Me.CmbAVtas.TabIndex = 101
    Me.CmbAVtas.Visible = False
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label10.Location = New System.Drawing.Point(474, 57)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(121, 17)
    Me.Label10.TabIndex = 102
    Me.Label10.Text = "Agente de Ventas"
    Me.Label10.Visible = False
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(311, 47)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(20, 17)
    Me.Label1.TabIndex = 106
    Me.Label1.Text = "a "
    '
    'DtpPPagoTer
    '
    Me.DtpPPagoTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DtpPPagoTer.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.DtpPPagoTer.Location = New System.Drawing.Point(332, 44)
    Me.DtpPPagoTer.Name = "DtpPPagoTer"
    Me.DtpPPagoTer.Size = New System.Drawing.Size(94, 23)
    Me.DtpPPagoTer.TabIndex = 104
    Me.DtpPPagoTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.Location = New System.Drawing.Point(12, 47)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(147, 17)
    Me.Label2.TabIndex = 105
    Me.Label2.Text = "Periodo de Pagos  De"
    '
    'DtpPPagoIni
    '
    Me.DtpPPagoIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.DtpPPagoIni.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.DtpPPagoIni.Location = New System.Drawing.Point(211, 44)
    Me.DtpPPagoIni.Name = "DtpPPagoIni"
    Me.DtpPPagoIni.Size = New System.Drawing.Size(94, 23)
    Me.DtpPPagoIni.TabIndex = 103
    Me.DtpPPagoIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
    '
    'Cobranza
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoSize = True
    Me.BackColor = System.Drawing.Color.LightSteelBlue
    Me.ClientSize = New System.Drawing.Size(1149, 615)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.DtpPPagoTer)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.DtpPPagoIni)
    Me.Controls.Add(Me.CmbAVtas)
    Me.Controls.Add(Me.Label10)
    Me.Controls.Add(Me.CmbACob)
    Me.Controls.Add(Me.Label9)
    Me.Controls.Add(Me.BtnExcel)
    Me.Controls.Add(Me.GrdConProd)
    Me.Controls.Add(Me.BtnImprimir)
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.DtpPVtaTer)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.DtpPVtaIni)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "Cobranza"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Reporte de cobranza"
    CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents GrdConProd As System.Windows.Forms.DataGridView
    Friend WithEvents BtnImprimir As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DtpPVtaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpPVtaIni As System.Windows.Forms.DateTimePicker
  Friend WithEvents CmbACob As System.Windows.Forms.ComboBox
  Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CmbAVtas As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents Label1 As Label
  Friend WithEvents DtpPPagoTer As DateTimePicker
  Friend WithEvents Label2 As Label
  Friend WithEvents DtpPPagoIni As DateTimePicker
End Class
