<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBonoMensual
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
  Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.Label7 = New System.Windows.Forms.Label()
  Me.txtDiasMes = New System.Windows.Forms.TextBox()
  Me.txtDiasTranscurridos = New System.Windows.Forms.TextBox()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.txtDiasRestantes = New System.Windows.Forms.TextBox()
  Me.btnParametros = New System.Windows.Forms.Button()
  Me.label9 = New System.Windows.Forms.Label()
  Me.Button2 = New System.Windows.Forms.Button()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.DtpFechaIni = New System.Windows.Forms.DateTimePicker()
  Me.Panel2 = New System.Windows.Forms.Panel()
  Me.GroupBox5 = New System.Windows.Forms.GroupBox()
  Me.lblEspera = New System.Windows.Forms.Label()
  Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
  Me.txtBonoClientes = New System.Windows.Forms.TextBox()
  Me.txtPorLogradoClientes = New System.Windows.Forms.TextBox()
  Me.txtAcumClientes = New System.Windows.Forms.TextBox()
  Me.txtObjClientes = New System.Windows.Forms.TextBox()
  Me.txtPesosCorrClientes = New System.Windows.Forms.TextBox()
  Me.txtPorcCorrClientes = New System.Windows.Forms.TextBox()
  Me.txtBonoLineas = New System.Windows.Forms.TextBox()
  Me.txtPorLogradoLineas = New System.Windows.Forms.TextBox()
  Me.txtAcumLineas = New System.Windows.Forms.TextBox()
  Me.txtObjLineas = New System.Windows.Forms.TextBox()
  Me.txtPesosCorrLineas = New System.Windows.Forms.TextBox()
  Me.txtPorcCorrLineas = New System.Windows.Forms.TextBox()
  Me.txtBonoHalcon = New System.Windows.Forms.TextBox()
  Me.txtPorLogradoHalcon = New System.Windows.Forms.TextBox()
  Me.txtAcumHalcon = New System.Windows.Forms.TextBox()
  Me.txtObjHalcon = New System.Windows.Forms.TextBox()
  Me.txtPesosCorrHalcon = New System.Windows.Forms.TextBox()
  Me.txtPorcCorrHalcon = New System.Windows.Forms.TextBox()
  Me.txtBonoExcedente = New System.Windows.Forms.TextBox()
  Me.txtPorExcedente = New System.Windows.Forms.TextBox()
  Me.txtBonoGeneral = New System.Windows.Forms.TextBox()
  Me.txtPorLogradoGeneral = New System.Windows.Forms.TextBox()
  Me.txtAcumGeneral = New System.Windows.Forms.TextBox()
  Me.txtObjGeneral = New System.Windows.Forms.TextBox()
  Me.txtPesosCorrGeneral = New System.Windows.Forms.TextBox()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.Label11 = New System.Windows.Forms.Label()
  Me.Label12 = New System.Windows.Forms.Label()
  Me.Label14 = New System.Windows.Forms.Label()
  Me.Label13 = New System.Windows.Forms.Label()
  Me.Label16 = New System.Windows.Forms.Label()
  Me.Label15 = New System.Windows.Forms.Label()
  Me.Label17 = New System.Windows.Forms.Label()
  Me.Label18 = New System.Windows.Forms.Label()
  Me.Label20 = New System.Windows.Forms.Label()
  Me.Label19 = New System.Windows.Forms.Label()
  Me.Label22 = New System.Windows.Forms.Label()
  Me.Label21 = New System.Windows.Forms.Label()
  Me.txtBonoTotalAlcanzado = New System.Windows.Forms.TextBox()
  Me.txtPorcCorrGeneral = New System.Windows.Forms.TextBox()
  Me.GroupBox4 = New System.Windows.Forms.GroupBox()
  Me.txtPorcentajeMinimoParaBono = New System.Windows.Forms.TextBox()
  Me.Label10 = New System.Windows.Forms.Label()
  Me.txtImporteBono = New System.Windows.Forms.TextBox()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.GroupBox3 = New System.Windows.Forms.GroupBox()
  Me.DgAgentes = New System.Windows.Forms.DataGridView()
  Me.Panel1.SuspendLayout()
  Me.Panel2.SuspendLayout()
  Me.GroupBox5.SuspendLayout()
  Me.TableLayoutPanel1.SuspendLayout()
  Me.GroupBox4.SuspendLayout()
  Me.GroupBox3.SuspendLayout()
  CType(Me.DgAgentes, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'Panel1
  '
  Me.Panel1.Controls.Add(Me.Label7)
  Me.Panel1.Controls.Add(Me.txtDiasMes)
  Me.Panel1.Controls.Add(Me.txtDiasTranscurridos)
  Me.Panel1.Controls.Add(Me.Label6)
  Me.Panel1.Controls.Add(Me.txtDiasRestantes)
  Me.Panel1.Controls.Add(Me.btnParametros)
  Me.Panel1.Controls.Add(Me.label9)
  Me.Panel1.Controls.Add(Me.Button2)
  Me.Panel1.Controls.Add(Me.Label5)
  Me.Panel1.Controls.Add(Me.DtpFechaIni)
  Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
  Me.Panel1.Location = New System.Drawing.Point(0, 0)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(194, 600)
  Me.Panel1.TabIndex = 217
  '
  'Label7
  '
  Me.Label7.AutoSize = True
  Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label7.Location = New System.Drawing.Point(16, 19)
  Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
  Me.Label7.Name = "Label7"
  Me.Label7.Size = New System.Drawing.Size(53, 13)
  Me.Label7.TabIndex = 194
  Me.Label7.Text = "Días Mes"
  '
  'txtDiasMes
  '
  Me.txtDiasMes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtDiasMes.Location = New System.Drawing.Point(116, 15)
  Me.txtDiasMes.Margin = New System.Windows.Forms.Padding(4)
  Me.txtDiasMes.Name = "txtDiasMes"
  Me.txtDiasMes.ReadOnly = True
  Me.txtDiasMes.Size = New System.Drawing.Size(65, 20)
  Me.txtDiasMes.TabIndex = 192
  Me.txtDiasMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
  '
  'txtDiasTranscurridos
  '
  Me.txtDiasTranscurridos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtDiasTranscurridos.Location = New System.Drawing.Point(116, 36)
  Me.txtDiasTranscurridos.Margin = New System.Windows.Forms.Padding(4)
  Me.txtDiasTranscurridos.Name = "txtDiasTranscurridos"
  Me.txtDiasTranscurridos.ReadOnly = True
  Me.txtDiasTranscurridos.Size = New System.Drawing.Size(65, 20)
  Me.txtDiasTranscurridos.TabIndex = 193
  Me.txtDiasTranscurridos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
  '
  'Label6
  '
  Me.Label6.AutoSize = True
  Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label6.Location = New System.Drawing.Point(16, 40)
  Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(97, 13)
  Me.Label6.TabIndex = 195
  Me.Label6.Text = "Días Transcurridos"
  '
  'txtDiasRestantes
  '
  Me.txtDiasRestantes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtDiasRestantes.Location = New System.Drawing.Point(116, 57)
  Me.txtDiasRestantes.Margin = New System.Windows.Forms.Padding(4)
  Me.txtDiasRestantes.Name = "txtDiasRestantes"
  Me.txtDiasRestantes.ReadOnly = True
  Me.txtDiasRestantes.Size = New System.Drawing.Size(65, 20)
  Me.txtDiasRestantes.TabIndex = 196
  Me.txtDiasRestantes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
  '
  'btnParametros
  '
  Me.btnParametros.BackColor = System.Drawing.Color.AliceBlue
  Me.btnParametros.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.btnParametros.ForeColor = System.Drawing.Color.MediumBlue
  Me.btnParametros.Location = New System.Drawing.Point(16, 246)
  Me.btnParametros.Name = "btnParametros"
  Me.btnParametros.Size = New System.Drawing.Size(139, 56)
  Me.btnParametros.TabIndex = 205
  Me.btnParametros.Text = "Ingresar Parametros"
  Me.btnParametros.UseVisualStyleBackColor = False
  '
  'label9
  '
  Me.label9.AutoSize = True
  Me.label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.label9.Location = New System.Drawing.Point(16, 61)
  Me.label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
  Me.label9.Name = "label9"
  Me.label9.Size = New System.Drawing.Size(81, 13)
  Me.label9.TabIndex = 197
  Me.label9.Text = "Días Restantes"
  '
  'Button2
  '
  Me.Button2.BackColor = System.Drawing.Color.AliceBlue
  Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
  Me.Button2.Location = New System.Drawing.Point(40, 183)
  Me.Button2.Name = "Button2"
  Me.Button2.Size = New System.Drawing.Size(91, 38)
  Me.Button2.TabIndex = 202
  Me.Button2.Text = "Consultar"
  Me.Button2.UseVisualStyleBackColor = False
  '
  'Label5
  '
  Me.Label5.AutoSize = True
  Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
  Me.Label5.Location = New System.Drawing.Point(16, 114)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(71, 15)
  Me.Label5.TabIndex = 191
  Me.Label5.Text = "Fecha Final"
  '
  'DtpFechaIni
  '
  Me.DtpFechaIni.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
  Me.DtpFechaIni.Location = New System.Drawing.Point(19, 132)
  Me.DtpFechaIni.Name = "DtpFechaIni"
  Me.DtpFechaIni.Size = New System.Drawing.Size(112, 23)
  Me.DtpFechaIni.TabIndex = 188
  Me.DtpFechaIni.Value = New Date(2010, 5, 3, 12, 46, 0, 0)
  '
  'Panel2
  '
  Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
  Me.Panel2.Controls.Add(Me.GroupBox5)
  Me.Panel2.Controls.Add(Me.GroupBox4)
  Me.Panel2.Controls.Add(Me.GroupBox3)
  Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Panel2.Location = New System.Drawing.Point(194, 0)
  Me.Panel2.Name = "Panel2"
  Me.Panel2.Size = New System.Drawing.Size(1080, 600)
  Me.Panel2.TabIndex = 218
  '
  'GroupBox5
  '
  Me.GroupBox5.Controls.Add(Me.lblEspera)
  Me.GroupBox5.Controls.Add(Me.TableLayoutPanel1)
  Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
  Me.GroupBox5.Location = New System.Drawing.Point(0, 344)
  Me.GroupBox5.Name = "GroupBox5"
  Me.GroupBox5.Size = New System.Drawing.Size(1080, 256)
  Me.GroupBox5.TabIndex = 218
  Me.GroupBox5.TabStop = False
  Me.GroupBox5.Text = "Cálculo de Bono Mensual"
  '
  'lblEspera
  '
  Me.lblEspera.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
  Me.lblEspera.Font = New System.Drawing.Font("Microsoft Sans Serif", 39.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.lblEspera.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
  Me.lblEspera.Location = New System.Drawing.Point(23, 23)
  Me.lblEspera.Name = "lblEspera"
  Me.lblEspera.Size = New System.Drawing.Size(155, 68)
  Me.lblEspera.TabIndex = 207
  Me.lblEspera.Text = "REALIZANDO CALCULOS PARA OBTENER LA INFORMACION DEL BONO MENSUAL, POR FAVOR ESPER" &
    "E..."
  Me.lblEspera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'TableLayoutPanel1
  '
  Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Wheat
  Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
  Me.TableLayoutPanel1.ColumnCount = 7
  Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.58241!))
  Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.73627!))
  Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.73627!))
  Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.73627!))
  Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.73627!))
  Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.73627!))
  Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.73627!))
  Me.TableLayoutPanel1.Controls.Add(Me.txtBonoClientes, 6, 5)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPorLogradoClientes, 5, 5)
  Me.TableLayoutPanel1.Controls.Add(Me.txtAcumClientes, 4, 5)
  Me.TableLayoutPanel1.Controls.Add(Me.txtObjClientes, 3, 5)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPesosCorrClientes, 2, 5)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPorcCorrClientes, 1, 5)
  Me.TableLayoutPanel1.Controls.Add(Me.txtBonoLineas, 6, 4)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPorLogradoLineas, 5, 4)
  Me.TableLayoutPanel1.Controls.Add(Me.txtAcumLineas, 4, 4)
  Me.TableLayoutPanel1.Controls.Add(Me.txtObjLineas, 3, 4)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPesosCorrLineas, 2, 4)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPorcCorrLineas, 1, 4)
  Me.TableLayoutPanel1.Controls.Add(Me.txtBonoHalcon, 6, 3)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPorLogradoHalcon, 5, 3)
  Me.TableLayoutPanel1.Controls.Add(Me.txtAcumHalcon, 4, 3)
  Me.TableLayoutPanel1.Controls.Add(Me.txtObjHalcon, 3, 3)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPesosCorrHalcon, 2, 3)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPorcCorrHalcon, 1, 3)
  Me.TableLayoutPanel1.Controls.Add(Me.txtBonoExcedente, 6, 2)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPorExcedente, 5, 2)
  Me.TableLayoutPanel1.Controls.Add(Me.txtBonoGeneral, 6, 1)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPorLogradoGeneral, 5, 1)
  Me.TableLayoutPanel1.Controls.Add(Me.txtAcumGeneral, 4, 1)
  Me.TableLayoutPanel1.Controls.Add(Me.txtObjGeneral, 3, 1)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPesosCorrGeneral, 2, 1)
  Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 0)
  Me.TableLayoutPanel1.Controls.Add(Me.Label11, 1, 0)
  Me.TableLayoutPanel1.Controls.Add(Me.Label12, 2, 0)
  Me.TableLayoutPanel1.Controls.Add(Me.Label14, 4, 0)
  Me.TableLayoutPanel1.Controls.Add(Me.Label13, 3, 0)
  Me.TableLayoutPanel1.Controls.Add(Me.Label16, 5, 0)
  Me.TableLayoutPanel1.Controls.Add(Me.Label15, 6, 0)
  Me.TableLayoutPanel1.Controls.Add(Me.Label17, 0, 1)
  Me.TableLayoutPanel1.Controls.Add(Me.Label18, 0, 2)
  Me.TableLayoutPanel1.Controls.Add(Me.Label20, 0, 3)
  Me.TableLayoutPanel1.Controls.Add(Me.Label19, 0, 4)
  Me.TableLayoutPanel1.Controls.Add(Me.Label22, 0, 5)
  Me.TableLayoutPanel1.Controls.Add(Me.Label21, 5, 6)
  Me.TableLayoutPanel1.Controls.Add(Me.txtBonoTotalAlcanzado, 6, 6)
  Me.TableLayoutPanel1.Controls.Add(Me.txtPorcCorrGeneral, 1, 1)
  Me.TableLayoutPanel1.Location = New System.Drawing.Point(26, 42)
  Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
  Me.TableLayoutPanel1.RowCount = 7
  Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.62311!))
  Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.56281!))
  Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.56281!))
  Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.56281!))
  Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.56281!))
  Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.56281!))
  Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.56281!))
  Me.TableLayoutPanel1.Size = New System.Drawing.Size(1035, 207)
  Me.TableLayoutPanel1.TabIndex = 1
  '
  'txtBonoClientes
  '
  Me.txtBonoClientes.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtBonoClientes.Location = New System.Drawing.Point(891, 154)
  Me.txtBonoClientes.Name = "txtBonoClientes"
  Me.txtBonoClientes.ReadOnly = True
  Me.txtBonoClientes.Size = New System.Drawing.Size(125, 22)
  Me.txtBonoClientes.TabIndex = 43
  Me.txtBonoClientes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPorLogradoClientes
  '
  Me.txtPorLogradoClientes.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPorLogradoClientes.Location = New System.Drawing.Point(750, 154)
  Me.txtPorLogradoClientes.Name = "txtPorLogradoClientes"
  Me.txtPorLogradoClientes.ReadOnly = True
  Me.txtPorLogradoClientes.Size = New System.Drawing.Size(100, 22)
  Me.txtPorLogradoClientes.TabIndex = 42
  Me.txtPorLogradoClientes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtAcumClientes
  '
  Me.txtAcumClientes.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtAcumClientes.Location = New System.Drawing.Point(609, 154)
  Me.txtAcumClientes.Name = "txtAcumClientes"
  Me.txtAcumClientes.ReadOnly = True
  Me.txtAcumClientes.Size = New System.Drawing.Size(125, 22)
  Me.txtAcumClientes.TabIndex = 41
  Me.txtAcumClientes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtObjClientes
  '
  Me.txtObjClientes.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtObjClientes.Location = New System.Drawing.Point(468, 154)
  Me.txtObjClientes.Name = "txtObjClientes"
  Me.txtObjClientes.ReadOnly = True
  Me.txtObjClientes.Size = New System.Drawing.Size(125, 22)
  Me.txtObjClientes.TabIndex = 40
  Me.txtObjClientes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPesosCorrClientes
  '
  Me.txtPesosCorrClientes.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPesosCorrClientes.Location = New System.Drawing.Point(327, 154)
  Me.txtPesosCorrClientes.Name = "txtPesosCorrClientes"
  Me.txtPesosCorrClientes.ReadOnly = True
  Me.txtPesosCorrClientes.Size = New System.Drawing.Size(125, 22)
  Me.txtPesosCorrClientes.TabIndex = 39
  Me.txtPesosCorrClientes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPorcCorrClientes
  '
  Me.txtPorcCorrClientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPorcCorrClientes.Location = New System.Drawing.Point(186, 154)
  Me.txtPorcCorrClientes.Name = "txtPorcCorrClientes"
  Me.txtPorcCorrClientes.ReadOnly = True
  Me.txtPorcCorrClientes.Size = New System.Drawing.Size(81, 22)
  Me.txtPorcCorrClientes.TabIndex = 38
  Me.txtPorcCorrClientes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtBonoLineas
  '
  Me.txtBonoLineas.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtBonoLineas.Location = New System.Drawing.Point(891, 129)
  Me.txtBonoLineas.Name = "txtBonoLineas"
  Me.txtBonoLineas.ReadOnly = True
  Me.txtBonoLineas.Size = New System.Drawing.Size(125, 22)
  Me.txtBonoLineas.TabIndex = 37
  Me.txtBonoLineas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPorLogradoLineas
  '
  Me.txtPorLogradoLineas.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPorLogradoLineas.Location = New System.Drawing.Point(750, 129)
  Me.txtPorLogradoLineas.Name = "txtPorLogradoLineas"
  Me.txtPorLogradoLineas.ReadOnly = True
  Me.txtPorLogradoLineas.Size = New System.Drawing.Size(100, 22)
  Me.txtPorLogradoLineas.TabIndex = 36
  Me.txtPorLogradoLineas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtAcumLineas
  '
  Me.txtAcumLineas.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtAcumLineas.Location = New System.Drawing.Point(609, 129)
  Me.txtAcumLineas.Name = "txtAcumLineas"
  Me.txtAcumLineas.ReadOnly = True
  Me.txtAcumLineas.Size = New System.Drawing.Size(125, 22)
  Me.txtAcumLineas.TabIndex = 35
  Me.txtAcumLineas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtObjLineas
  '
  Me.txtObjLineas.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtObjLineas.Location = New System.Drawing.Point(468, 129)
  Me.txtObjLineas.Name = "txtObjLineas"
  Me.txtObjLineas.ReadOnly = True
  Me.txtObjLineas.Size = New System.Drawing.Size(125, 22)
  Me.txtObjLineas.TabIndex = 34
  Me.txtObjLineas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPesosCorrLineas
  '
  Me.txtPesosCorrLineas.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPesosCorrLineas.Location = New System.Drawing.Point(327, 129)
  Me.txtPesosCorrLineas.Name = "txtPesosCorrLineas"
  Me.txtPesosCorrLineas.ReadOnly = True
  Me.txtPesosCorrLineas.Size = New System.Drawing.Size(125, 22)
  Me.txtPesosCorrLineas.TabIndex = 33
  Me.txtPesosCorrLineas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPorcCorrLineas
  '
  Me.txtPorcCorrLineas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPorcCorrLineas.Location = New System.Drawing.Point(186, 129)
  Me.txtPorcCorrLineas.Name = "txtPorcCorrLineas"
  Me.txtPorcCorrLineas.ReadOnly = True
  Me.txtPorcCorrLineas.Size = New System.Drawing.Size(81, 22)
  Me.txtPorcCorrLineas.TabIndex = 32
  Me.txtPorcCorrLineas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtBonoHalcon
  '
  Me.txtBonoHalcon.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtBonoHalcon.Location = New System.Drawing.Point(891, 104)
  Me.txtBonoHalcon.Name = "txtBonoHalcon"
  Me.txtBonoHalcon.ReadOnly = True
  Me.txtBonoHalcon.Size = New System.Drawing.Size(125, 22)
  Me.txtBonoHalcon.TabIndex = 31
  Me.txtBonoHalcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPorLogradoHalcon
  '
  Me.txtPorLogradoHalcon.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPorLogradoHalcon.Location = New System.Drawing.Point(750, 104)
  Me.txtPorLogradoHalcon.Name = "txtPorLogradoHalcon"
  Me.txtPorLogradoHalcon.ReadOnly = True
  Me.txtPorLogradoHalcon.Size = New System.Drawing.Size(100, 22)
  Me.txtPorLogradoHalcon.TabIndex = 30
  Me.txtPorLogradoHalcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtAcumHalcon
  '
  Me.txtAcumHalcon.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtAcumHalcon.Location = New System.Drawing.Point(609, 104)
  Me.txtAcumHalcon.Name = "txtAcumHalcon"
  Me.txtAcumHalcon.ReadOnly = True
  Me.txtAcumHalcon.Size = New System.Drawing.Size(125, 22)
  Me.txtAcumHalcon.TabIndex = 29
  Me.txtAcumHalcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtObjHalcon
  '
  Me.txtObjHalcon.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtObjHalcon.Location = New System.Drawing.Point(468, 104)
  Me.txtObjHalcon.Name = "txtObjHalcon"
  Me.txtObjHalcon.ReadOnly = True
  Me.txtObjHalcon.Size = New System.Drawing.Size(125, 22)
  Me.txtObjHalcon.TabIndex = 28
  Me.txtObjHalcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPesosCorrHalcon
  '
  Me.txtPesosCorrHalcon.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPesosCorrHalcon.Location = New System.Drawing.Point(327, 104)
  Me.txtPesosCorrHalcon.Name = "txtPesosCorrHalcon"
  Me.txtPesosCorrHalcon.ReadOnly = True
  Me.txtPesosCorrHalcon.Size = New System.Drawing.Size(125, 22)
  Me.txtPesosCorrHalcon.TabIndex = 27
  Me.txtPesosCorrHalcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPorcCorrHalcon
  '
  Me.txtPorcCorrHalcon.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPorcCorrHalcon.Location = New System.Drawing.Point(186, 104)
  Me.txtPorcCorrHalcon.Name = "txtPorcCorrHalcon"
  Me.txtPorcCorrHalcon.ReadOnly = True
  Me.txtPorcCorrHalcon.Size = New System.Drawing.Size(81, 22)
  Me.txtPorcCorrHalcon.TabIndex = 26
  Me.txtPorcCorrHalcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtBonoExcedente
  '
  Me.txtBonoExcedente.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtBonoExcedente.Location = New System.Drawing.Point(891, 79)
  Me.txtBonoExcedente.Name = "txtBonoExcedente"
  Me.txtBonoExcedente.ReadOnly = True
  Me.txtBonoExcedente.Size = New System.Drawing.Size(125, 22)
  Me.txtBonoExcedente.TabIndex = 25
  Me.txtBonoExcedente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPorExcedente
  '
  Me.txtPorExcedente.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPorExcedente.Location = New System.Drawing.Point(750, 79)
  Me.txtPorExcedente.Name = "txtPorExcedente"
  Me.txtPorExcedente.ReadOnly = True
  Me.txtPorExcedente.Size = New System.Drawing.Size(100, 22)
  Me.txtPorExcedente.TabIndex = 24
  Me.txtPorExcedente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtBonoGeneral
  '
  Me.txtBonoGeneral.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtBonoGeneral.Location = New System.Drawing.Point(891, 54)
  Me.txtBonoGeneral.Name = "txtBonoGeneral"
  Me.txtBonoGeneral.ReadOnly = True
  Me.txtBonoGeneral.Size = New System.Drawing.Size(125, 22)
  Me.txtBonoGeneral.TabIndex = 19
  Me.txtBonoGeneral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPorLogradoGeneral
  '
  Me.txtPorLogradoGeneral.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPorLogradoGeneral.Location = New System.Drawing.Point(750, 54)
  Me.txtPorLogradoGeneral.Name = "txtPorLogradoGeneral"
  Me.txtPorLogradoGeneral.ReadOnly = True
  Me.txtPorLogradoGeneral.Size = New System.Drawing.Size(100, 22)
  Me.txtPorLogradoGeneral.TabIndex = 18
  Me.txtPorLogradoGeneral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtAcumGeneral
  '
  Me.txtAcumGeneral.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtAcumGeneral.Location = New System.Drawing.Point(609, 54)
  Me.txtAcumGeneral.Name = "txtAcumGeneral"
  Me.txtAcumGeneral.ReadOnly = True
  Me.txtAcumGeneral.Size = New System.Drawing.Size(125, 22)
  Me.txtAcumGeneral.TabIndex = 17
  Me.txtAcumGeneral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtObjGeneral
  '
  Me.txtObjGeneral.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtObjGeneral.Location = New System.Drawing.Point(468, 54)
  Me.txtObjGeneral.Name = "txtObjGeneral"
  Me.txtObjGeneral.ReadOnly = True
  Me.txtObjGeneral.Size = New System.Drawing.Size(125, 22)
  Me.txtObjGeneral.TabIndex = 16
  Me.txtObjGeneral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPesosCorrGeneral
  '
  Me.txtPesosCorrGeneral.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPesosCorrGeneral.Location = New System.Drawing.Point(327, 54)
  Me.txtPesosCorrGeneral.Name = "txtPesosCorrGeneral"
  Me.txtPesosCorrGeneral.ReadOnly = True
  Me.txtPesosCorrGeneral.Size = New System.Drawing.Size(125, 22)
  Me.txtPesosCorrGeneral.TabIndex = 15
  Me.txtPesosCorrGeneral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'Label8
  '
  Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label8.Location = New System.Drawing.Point(5, 2)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(173, 47)
  Me.Label8.TabIndex = 1
  Me.Label8.Text = "CONCEPTOS"
  Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'Label11
  '
  Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label11.Location = New System.Drawing.Point(186, 2)
  Me.Label11.Name = "Label11"
  Me.Label11.Size = New System.Drawing.Size(133, 47)
  Me.Label11.TabIndex = 2
  Me.Label11.Text = "% CORRESPONDIENTE"
  Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'Label12
  '
  Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label12.Location = New System.Drawing.Point(327, 2)
  Me.Label12.Name = "Label12"
  Me.Label12.Size = New System.Drawing.Size(133, 47)
  Me.Label12.TabIndex = 3
  Me.Label12.Text = "$ CORRESPONDIENTE"
  Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'Label14
  '
  Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label14.Location = New System.Drawing.Point(609, 2)
  Me.Label14.Name = "Label14"
  Me.Label14.Size = New System.Drawing.Size(133, 47)
  Me.Label14.TabIndex = 5
  Me.Label14.Text = "ALCANZADO A LA FECHA"
  Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'Label13
  '
  Me.Label13.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label13.Location = New System.Drawing.Point(468, 2)
  Me.Label13.Name = "Label13"
  Me.Label13.Size = New System.Drawing.Size(133, 47)
  Me.Label13.TabIndex = 4
  Me.Label13.Text = "OBJETIVO"
  Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'Label16
  '
  Me.Label16.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label16.Location = New System.Drawing.Point(750, 2)
  Me.Label16.Name = "Label16"
  Me.Label16.Size = New System.Drawing.Size(133, 47)
  Me.Label16.TabIndex = 7
  Me.Label16.Text = "% LOGRADO"
  Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'Label15
  '
  Me.Label15.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label15.Location = New System.Drawing.Point(891, 2)
  Me.Label15.Name = "Label15"
  Me.Label15.Size = New System.Drawing.Size(139, 47)
  Me.Label15.TabIndex = 6
  Me.Label15.Text = "$ BONO ALCANZADO"
  Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'Label17
  '
  Me.Label17.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label17.Location = New System.Drawing.Point(5, 51)
  Me.Label17.Name = "Label17"
  Me.Label17.Size = New System.Drawing.Size(173, 23)
  Me.Label17.TabIndex = 8
  Me.Label17.Text = "OBJETIVO VOLUMEN"
  Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label18
  '
  Me.Label18.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label18.Location = New System.Drawing.Point(5, 76)
  Me.Label18.Name = "Label18"
  Me.Label18.Size = New System.Drawing.Size(173, 23)
  Me.Label18.TabIndex = 9
  Me.Label18.Text = "EXC. CUOTA SC"
  Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label20
  '
  Me.Label20.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label20.Location = New System.Drawing.Point(5, 101)
  Me.Label20.Name = "Label20"
  Me.Label20.Size = New System.Drawing.Size(173, 23)
  Me.Label20.TabIndex = 11
  Me.Label20.Text = "LINEAS HALCON"
  Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label19
  '
  Me.Label19.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label19.Location = New System.Drawing.Point(5, 126)
  Me.Label19.Name = "Label19"
  Me.Label19.Size = New System.Drawing.Size(173, 23)
  Me.Label19.TabIndex = 10
  Me.Label19.Text = "LINEAS OBJETIVO"
  Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label22
  '
  Me.Label22.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label22.Location = New System.Drawing.Point(5, 151)
  Me.Label22.Name = "Label22"
  Me.Label22.Size = New System.Drawing.Size(173, 23)
  Me.Label22.TabIndex = 13
  Me.Label22.Text = "CTES. OBJETIVO"
  Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label21
  '
  Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label21.Location = New System.Drawing.Point(750, 176)
  Me.Label21.Name = "Label21"
  Me.Label21.Size = New System.Drawing.Size(133, 23)
  Me.Label21.TabIndex = 12
  Me.Label21.Text = "BONO ALCANZADO"
  Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'txtBonoTotalAlcanzado
  '
  Me.txtBonoTotalAlcanzado.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtBonoTotalAlcanzado.Location = New System.Drawing.Point(891, 179)
  Me.txtBonoTotalAlcanzado.Name = "txtBonoTotalAlcanzado"
  Me.txtBonoTotalAlcanzado.ReadOnly = True
  Me.txtBonoTotalAlcanzado.Size = New System.Drawing.Size(125, 22)
  Me.txtBonoTotalAlcanzado.TabIndex = 44
  Me.txtBonoTotalAlcanzado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'txtPorcCorrGeneral
  '
  Me.txtPorcCorrGeneral.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.txtPorcCorrGeneral.Location = New System.Drawing.Point(186, 54)
  Me.txtPorcCorrGeneral.Name = "txtPorcCorrGeneral"
  Me.txtPorcCorrGeneral.ReadOnly = True
  Me.txtPorcCorrGeneral.Size = New System.Drawing.Size(81, 22)
  Me.txtPorcCorrGeneral.TabIndex = 14
  Me.txtPorcCorrGeneral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'GroupBox4
  '
  Me.GroupBox4.Controls.Add(Me.txtPorcentajeMinimoParaBono)
  Me.GroupBox4.Controls.Add(Me.Label10)
  Me.GroupBox4.Controls.Add(Me.txtImporteBono)
  Me.GroupBox4.Controls.Add(Me.Label1)
  Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
  Me.GroupBox4.Location = New System.Drawing.Point(0, 284)
  Me.GroupBox4.Name = "GroupBox4"
  Me.GroupBox4.Size = New System.Drawing.Size(1080, 60)
  Me.GroupBox4.TabIndex = 217
  Me.GroupBox4.TabStop = False
  Me.GroupBox4.Text = "Información"
  '
  'txtPorcentajeMinimoParaBono
  '
  Me.txtPorcentajeMinimoParaBono.Location = New System.Drawing.Point(535, 27)
  Me.txtPorcentajeMinimoParaBono.Name = "txtPorcentajeMinimoParaBono"
  Me.txtPorcentajeMinimoParaBono.Size = New System.Drawing.Size(30, 20)
  Me.txtPorcentajeMinimoParaBono.TabIndex = 3
  Me.txtPorcentajeMinimoParaBono.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'Label10
  '
  Me.Label10.AutoSize = True
  Me.Label10.Location = New System.Drawing.Point(283, 29)
  Me.Label10.Name = "Label10"
  Me.Label10.Size = New System.Drawing.Size(246, 13)
  Me.Label10.TabIndex = 2
  Me.Label10.Text = "Porcentaje mínimo requerido para obtener el bono:"
  '
  'txtImporteBono
  '
  Me.txtImporteBono.Location = New System.Drawing.Point(118, 27)
  Me.txtImporteBono.Name = "txtImporteBono"
  Me.txtImporteBono.Size = New System.Drawing.Size(60, 20)
  Me.txtImporteBono.TabIndex = 1
  Me.txtImporteBono.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Location = New System.Drawing.Point(23, 30)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(92, 13)
  Me.Label1.TabIndex = 0
  Me.Label1.Text = "Importe del bono: "
  '
  'GroupBox3
  '
  Me.GroupBox3.Controls.Add(Me.DgAgentes)
  Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
  Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
  Me.GroupBox3.Name = "GroupBox3"
  Me.GroupBox3.Size = New System.Drawing.Size(1080, 284)
  Me.GroupBox3.TabIndex = 216
  Me.GroupBox3.TabStop = False
  Me.GroupBox3.Text = "Vendedor"
  '
  'DgAgentes
  '
  Me.DgAgentes.AllowUserToAddRows = False
  Me.DgAgentes.AllowUserToDeleteRows = False
  Me.DgAgentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.DgAgentes.Dock = System.Windows.Forms.DockStyle.Fill
  Me.DgAgentes.Location = New System.Drawing.Point(3, 16)
  Me.DgAgentes.Name = "DgAgentes"
  Me.DgAgentes.ReadOnly = True
  DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DgAgentes.RowsDefaultCellStyle = DataGridViewCellStyle1
  Me.DgAgentes.Size = New System.Drawing.Size(1074, 265)
  Me.DgAgentes.TabIndex = 5
  '
  'frmBonoMensual
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1274, 600)
  Me.Controls.Add(Me.Panel2)
  Me.Controls.Add(Me.Panel1)
  Me.Name = "frmBonoMensual"
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Bono Mensual"
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  Me.Panel2.ResumeLayout(False)
  Me.GroupBox5.ResumeLayout(False)
  Me.TableLayoutPanel1.ResumeLayout(False)
  Me.TableLayoutPanel1.PerformLayout()
  Me.GroupBox4.ResumeLayout(False)
  Me.GroupBox4.PerformLayout()
  Me.GroupBox3.ResumeLayout(False)
  CType(Me.DgAgentes, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents Panel1 As Panel
  Private WithEvents Label7 As Label
  Private WithEvents txtDiasMes As TextBox
  Private WithEvents txtDiasTranscurridos As TextBox
  Private WithEvents Label6 As Label
  Private WithEvents txtDiasRestantes As TextBox
  Friend WithEvents btnParametros As Button
  Private WithEvents label9 As Label
  Friend WithEvents Button2 As Button
  Friend WithEvents Label5 As Label
  Friend WithEvents DtpFechaIni As DateTimePicker
  Friend WithEvents Panel2 As Panel
  Friend WithEvents GroupBox5 As GroupBox
  Friend WithEvents GroupBox3 As GroupBox
  Friend WithEvents DgAgentes As DataGridView
  Friend WithEvents GroupBox4 As GroupBox
  Friend WithEvents Label1 As Label
  Friend WithEvents txtPorcentajeMinimoParaBono As TextBox
  Friend WithEvents Label10 As Label
  Friend WithEvents txtImporteBono As TextBox
  Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
  Friend WithEvents txtBonoClientes As TextBox
  Friend WithEvents txtPorLogradoClientes As TextBox
  Friend WithEvents txtAcumClientes As TextBox
  Friend WithEvents txtObjClientes As TextBox
  Friend WithEvents txtPesosCorrClientes As TextBox
  Friend WithEvents txtPorcCorrClientes As TextBox
  Friend WithEvents txtBonoLineas As TextBox
  Friend WithEvents txtPorLogradoLineas As TextBox
  Friend WithEvents txtAcumLineas As TextBox
  Friend WithEvents txtObjLineas As TextBox
  Friend WithEvents txtPesosCorrLineas As TextBox
  Friend WithEvents txtPorcCorrLineas As TextBox
  Friend WithEvents txtBonoHalcon As TextBox
  Friend WithEvents txtPorLogradoHalcon As TextBox
  Friend WithEvents txtAcumHalcon As TextBox
  Friend WithEvents txtObjHalcon As TextBox
  Friend WithEvents txtPesosCorrHalcon As TextBox
  Friend WithEvents txtPorcCorrHalcon As TextBox
  Friend WithEvents txtBonoExcedente As TextBox
  Friend WithEvents txtPorExcedente As TextBox
  Friend WithEvents txtBonoGeneral As TextBox
  Friend WithEvents txtPorLogradoGeneral As TextBox
  Friend WithEvents txtAcumGeneral As TextBox
  Friend WithEvents txtObjGeneral As TextBox
  Friend WithEvents txtPesosCorrGeneral As TextBox
  Friend WithEvents Label8 As Label
  Friend WithEvents Label11 As Label
  Friend WithEvents Label12 As Label
  Friend WithEvents Label14 As Label
  Friend WithEvents Label13 As Label
  Friend WithEvents Label16 As Label
  Friend WithEvents Label15 As Label
  Friend WithEvents Label17 As Label
  Friend WithEvents Label18 As Label
  Friend WithEvents Label20 As Label
  Friend WithEvents Label19 As Label
  Friend WithEvents Label22 As Label
  Friend WithEvents Label21 As Label
  Friend WithEvents txtBonoTotalAlcanzado As TextBox
  Friend WithEvents txtPorcCorrGeneral As TextBox
  Friend WithEvents lblEspera As Label
End Class
