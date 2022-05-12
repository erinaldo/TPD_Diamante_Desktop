<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reporte_Ventas_Lineas
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
  Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Me.Label9 = New System.Windows.Forms.Label()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.Label7 = New System.Windows.Forms.Label()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.GrdDetArt = New System.Windows.Forms.DataGridView()
  Me.GrdConLinea = New System.Windows.Forms.DataGridView()
  Me.GrdConProd = New System.Windows.Forms.DataGridView()
  Me.Button1 = New System.Windows.Forms.Button()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.DtpFechaTer = New System.Windows.Forms.DateTimePicker()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
  Me.Button4 = New System.Windows.Forms.Button()
  Me.Button3 = New System.Windows.Forms.Button()
  Me.Button2 = New System.Windows.Forms.Button()
  Me.BtnExcel = New System.Windows.Forms.Button()
  Me.GrdTodosArt = New System.Windows.Forms.DataGridView()
  Me.ckCteProp = New System.Windows.Forms.CheckBox()
  Me.Panel1 = New System.Windows.Forms.Panel()
  CType(Me.GrdDetArt, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.GrdConLinea, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.GrdTodosArt, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.Panel1.SuspendLayout()
  Me.SuspendLayout()
  '
  'Label9
  '
  Me.Label9.AutoSize = True
  Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label9.Location = New System.Drawing.Point(1182, 15)
  Me.Label9.Name = "Label9"
  Me.Label9.Size = New System.Drawing.Size(59, 17)
  Me.Label9.TabIndex = 122
  Me.Label9.Text = "Reporte"
  '
  'Label8
  '
  Me.Label8.AutoSize = True
  Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label8.Location = New System.Drawing.Point(1084, 16)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(55, 17)
  Me.Label8.TabIndex = 120
  Me.Label8.Text = "Articulo"
  '
  'Label7
  '
  Me.Label7.AutoSize = True
  Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label7.Location = New System.Drawing.Point(902, 15)
  Me.Label7.Name = "Label7"
  Me.Label7.Size = New System.Drawing.Size(43, 17)
  Me.Label7.TabIndex = 118
  Me.Label7.Text = "Linea"
  '
  'Label4
  '
  Me.Label4.AutoSize = True
  Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label4.Location = New System.Drawing.Point(988, 15)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(60, 17)
  Me.Label4.TabIndex = 116
  Me.Label4.Text = "Agentes"
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.BackColor = System.Drawing.Color.Gainsboro
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.ForeColor = System.Drawing.Color.Black
  Me.Label2.Location = New System.Drawing.Point(780, 398)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(180, 17)
  Me.Label2.TabIndex = 115
  Me.Label2.Text = "Ventas Totales Por Artículo"
  '
  'Label6
  '
  Me.Label6.AutoSize = True
  Me.Label6.BackColor = System.Drawing.Color.White
  Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label6.ForeColor = System.Drawing.Color.Black
  Me.Label6.Location = New System.Drawing.Point(4, 48)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(168, 17)
  Me.Label6.TabIndex = 114
  Me.Label6.Text = "Ventas Totales Por Linea"
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.BackColor = System.Drawing.Color.Gainsboro
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.ForeColor = System.Drawing.Color.Black
  Me.Label1.Location = New System.Drawing.Point(780, 47)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(246, 17)
  Me.Label1.TabIndex = 113
  Me.Label1.Text = "Ventas Totales Por Agente de Ventas"
  '
  'GrdDetArt
  '
  Me.GrdDetArt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.GrdDetArt.Location = New System.Drawing.Point(780, 415)
  Me.GrdDetArt.Name = "GrdDetArt"
  DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.GrdDetArt.RowsDefaultCellStyle = DataGridViewCellStyle1
  Me.GrdDetArt.Size = New System.Drawing.Size(790, 332)
  Me.GrdDetArt.TabIndex = 112
  '
  'GrdConLinea
  '
  Me.GrdConLinea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.GrdConLinea.Location = New System.Drawing.Point(781, 65)
  Me.GrdConLinea.Name = "GrdConLinea"
  DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.GrdConLinea.RowsDefaultCellStyle = DataGridViewCellStyle2
  Me.GrdConLinea.Size = New System.Drawing.Size(789, 330)
  Me.GrdConLinea.TabIndex = 111
  '
  'GrdConProd
  '
  Me.GrdConProd.AllowUserToAddRows = False
  Me.GrdConProd.AllowUserToDeleteRows = False
  Me.GrdConProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.GrdConProd.Location = New System.Drawing.Point(4, 65)
  Me.GrdConProd.Name = "GrdConProd"
  DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.GrdConProd.RowsDefaultCellStyle = DataGridViewCellStyle3
  Me.GrdConProd.Size = New System.Drawing.Size(769, 683)
  Me.GrdConProd.TabIndex = 108
  '
  'Button1
  '
  Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
  Me.Button1.Location = New System.Drawing.Point(749, 10)
  Me.Button1.Name = "Button1"
  Me.Button1.Size = New System.Drawing.Size(75, 31)
  Me.Button1.TabIndex = 105
  Me.Button1.Text = "Consultar"
  Me.Button1.UseVisualStyleBackColor = True
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(363, 14)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(107, 17)
  Me.Label3.TabIndex = 107
  Me.Label3.Text = "Fecha Término:"
  '
  'DtpFechaTer
  '
  Me.DtpFechaTer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DtpFechaTer.Location = New System.Drawing.Point(471, 13)
  Me.DtpFechaTer.Name = "DtpFechaTer"
  Me.DtpFechaTer.Size = New System.Drawing.Size(239, 23)
  Me.DtpFechaTer.TabIndex = 104
  Me.DtpFechaTer.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
  '
  'Label5
  '
  Me.Label5.AutoSize = True
  Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label5.Location = New System.Drawing.Point(8, 14)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(87, 17)
  Me.Label5.TabIndex = 106
  Me.Label5.Text = "Fecha Inicio:"
  '
  'DtpFechaIni
  '
  Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DtpFechaIni.Location = New System.Drawing.Point(98, 13)
  Me.DtpFechaIni.Name = "DtpFechaIni"
  Me.DtpFechaIni.Size = New System.Drawing.Size(259, 23)
  Me.DtpFechaIni.TabIndex = 103
  Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
  '
  'Button4
  '
  Me.Button4.Image = Global.TPDiamante.My.Resources.Resources.Excel_2007
  Me.Button4.Location = New System.Drawing.Point(1145, 7)
  Me.Button4.Name = "Button4"
  Me.Button4.Size = New System.Drawing.Size(36, 34)
  Me.Button4.TabIndex = 121
  Me.Button4.UseVisualStyleBackColor = True
  '
  'Button3
  '
  Me.Button3.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
  Me.Button3.Location = New System.Drawing.Point(1047, 7)
  Me.Button3.Name = "Button3"
  Me.Button3.Size = New System.Drawing.Size(36, 34)
  Me.Button3.TabIndex = 119
  Me.Button3.UseVisualStyleBackColor = True
  '
  'Button2
  '
  Me.Button2.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
  Me.Button2.Location = New System.Drawing.Point(865, 6)
  Me.Button2.Name = "Button2"
  Me.Button2.Size = New System.Drawing.Size(36, 34)
  Me.Button2.TabIndex = 117
  Me.Button2.UseVisualStyleBackColor = True
  '
  'BtnExcel
  '
  Me.BtnExcel.Image = Global.TPDiamante.My.Resources.Resources.Excel2016
  Me.BtnExcel.Location = New System.Drawing.Point(951, 7)
  Me.BtnExcel.Name = "BtnExcel"
  Me.BtnExcel.Size = New System.Drawing.Size(36, 34)
  Me.BtnExcel.TabIndex = 110
  Me.BtnExcel.UseVisualStyleBackColor = True
  '
  'GrdTodosArt
  '
  Me.GrdTodosArt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.GrdTodosArt.Location = New System.Drawing.Point(229, 485)
  Me.GrdTodosArt.Name = "GrdTodosArt"
  Me.GrdTodosArt.Size = New System.Drawing.Size(790, 272)
  Me.GrdTodosArt.TabIndex = 123
  Me.GrdTodosArt.Visible = False
  '
  'ckCteProp
  '
  Me.ckCteProp.AutoSize = True
  Me.ckCteProp.Checked = True
  Me.ckCteProp.CheckState = System.Windows.Forms.CheckState.Checked
  Me.ckCteProp.Location = New System.Drawing.Point(471, 42)
  Me.ckCteProp.Name = "ckCteProp"
  Me.ckCteProp.Size = New System.Drawing.Size(100, 17)
  Me.ckCteProp.TabIndex = 124
  Me.ckCteProp.Text = "Clientes propios"
  Me.ckCteProp.UseVisualStyleBackColor = True
  '
  'Panel1
  '
  Me.Panel1.Controls.Add(Me.Label5)
  Me.Panel1.Controls.Add(Me.ckCteProp)
  Me.Panel1.Controls.Add(Me.DtpFechaIni)
  Me.Panel1.Controls.Add(Me.Label3)
  Me.Panel1.Controls.Add(Me.Label9)
  Me.Panel1.Controls.Add(Me.DtpFechaTer)
  Me.Panel1.Controls.Add(Me.Button4)
  Me.Panel1.Controls.Add(Me.Button1)
  Me.Panel1.Controls.Add(Me.Label8)
  Me.Panel1.Controls.Add(Me.Button2)
  Me.Panel1.Controls.Add(Me.Button3)
  Me.Panel1.Controls.Add(Me.Label7)
  Me.Panel1.Controls.Add(Me.Label4)
  Me.Panel1.Controls.Add(Me.BtnExcel)
  Me.Panel1.Location = New System.Drawing.Point(4, 0)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(1247, 65)
  Me.Panel1.TabIndex = 125
  '
  'Reporte_Ventas_Lineas
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.AutoSize = True
  Me.BackColor = System.Drawing.Color.LightSlateGray
  Me.ClientSize = New System.Drawing.Size(1251, 742)
  Me.Controls.Add(Me.GrdTodosArt)
  Me.Controls.Add(Me.Label2)
  Me.Controls.Add(Me.Label6)
  Me.Controls.Add(Me.Label1)
  Me.Controls.Add(Me.GrdDetArt)
  Me.Controls.Add(Me.GrdConLinea)
  Me.Controls.Add(Me.GrdConProd)
  Me.Controls.Add(Me.Panel1)
  Me.Name = "Reporte_Ventas_Lineas"
  Me.Text = "Ventas Linea"
  CType(Me.GrdDetArt, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.GrdConLinea, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.GrdTodosArt, System.ComponentModel.ISupportInitialize).EndInit()
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub
 Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GrdDetArt As System.Windows.Forms.DataGridView
    Friend WithEvents GrdConLinea As System.Windows.Forms.DataGridView
    Friend WithEvents BtnExcel As System.Windows.Forms.Button
    Friend WithEvents GrdConProd As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaTer As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DtpFechaIni As System.Windows.Forms.DateTimePicker
    Friend WithEvents GrdTodosArt As System.Windows.Forms.DataGridView
    Friend WithEvents ckCteProp As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
