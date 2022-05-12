<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Packin
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
  Me.components = New System.ComponentModel.Container()
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Packin))
  Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
  Me.PictureBox1 = New System.Windows.Forms.PictureBox()
  Me.Button2 = New System.Windows.Forms.Button()
  Me.Label9 = New System.Windows.Forms.Label()
  Me.Label10 = New System.Windows.Forms.Label()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.TBPerCon = New System.Windows.Forms.TextBox()
  Me.TBNomCli = New System.Windows.Forms.TextBox()
  Me.TBCliente = New System.Windows.Forms.TextBox()
  Me.TBDocDate = New System.Windows.Forms.TextBox()
  Me.TBDocNum = New System.Windows.Forms.TextBox()
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.Label24 = New System.Windows.Forms.Label()
  Me.Label23 = New System.Windows.Forms.Label()
  Me.DGDetalle = New System.Windows.Forms.DataGridView()
  Me.TextBox1 = New System.Windows.Forms.TextBox()
  Me.ComboBox1 = New System.Windows.Forms.ComboBox()
  Me.Label26 = New System.Windows.Forms.Label()
  Me.Label25 = New System.Windows.Forms.Label()
  Me.TBComentarios = New System.Windows.Forms.TextBox()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.DataGridView1 = New System.Windows.Forms.DataGridView()
  Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
  Me.BSave = New System.Windows.Forms.Button()
  Me.TBDocEntry = New System.Windows.Forms.TextBox()
  CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.Panel1.SuspendLayout()
  CType(Me.DGDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'PictureBox1
  '
  Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
  Me.PictureBox1.Location = New System.Drawing.Point(1148, 16)
  Me.PictureBox1.Name = "PictureBox1"
  Me.PictureBox1.Size = New System.Drawing.Size(30, 27)
  Me.PictureBox1.TabIndex = 150
  Me.PictureBox1.TabStop = False
  Me.PictureBox1.Visible = False
  '
  'Button2
  '
  Me.Button2.BackColor = System.Drawing.Color.AliceBlue
  Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
  Me.Button2.Location = New System.Drawing.Point(1008, 6)
  Me.Button2.Name = "Button2"
  Me.Button2.Size = New System.Drawing.Size(86, 46)
  Me.Button2.TabIndex = 1
  Me.Button2.Text = "Consultar"
  Me.Button2.UseVisualStyleBackColor = False
  '
  'Label9
  '
  Me.Label9.AutoSize = True
  Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label9.Location = New System.Drawing.Point(721, 60)
  Me.Label9.Name = "Label9"
  Me.Label9.Size = New System.Drawing.Size(49, 16)
  Me.Label9.TabIndex = 8
  Me.Label9.Text = "Fecha:"
  '
  'Label10
  '
  Me.Label10.AutoSize = True
  Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label10.Location = New System.Drawing.Point(720, 23)
  Me.Label10.Name = "Label10"
  Me.Label10.Size = New System.Drawing.Size(76, 20)
  Me.Label10.TabIndex = 6
  Me.Label10.Text = "Factura:"
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label3.Location = New System.Drawing.Point(50, 92)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(98, 16)
  Me.Label3.TabIndex = 4
  Me.Label3.Text = "Telemarketing:"
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label2.Location = New System.Drawing.Point(50, 60)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(60, 16)
  Me.Label2.TabIndex = 2
  Me.Label2.Text = "Nombre:"
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label1.Location = New System.Drawing.Point(50, 28)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(52, 16)
  Me.Label1.TabIndex = 0
  Me.Label1.Text = "Cliente:"
  '
  'TBPerCon
  '
  Me.TBPerCon.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.TBPerCon.Location = New System.Drawing.Point(154, 86)
  Me.TBPerCon.Name = "TBPerCon"
  Me.TBPerCon.ReadOnly = True
  Me.TBPerCon.Size = New System.Drawing.Size(474, 26)
  Me.TBPerCon.TabIndex = 5
  '
  'TBNomCli
  '
  Me.TBNomCli.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.TBNomCli.Location = New System.Drawing.Point(154, 54)
  Me.TBNomCli.Name = "TBNomCli"
  Me.TBNomCli.ReadOnly = True
  Me.TBNomCli.Size = New System.Drawing.Size(474, 26)
  Me.TBNomCli.TabIndex = 3
  '
  'TBCliente
  '
  Me.TBCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.TBCliente.Location = New System.Drawing.Point(154, 22)
  Me.TBCliente.Name = "TBCliente"
  Me.TBCliente.ReadOnly = True
  Me.TBCliente.Size = New System.Drawing.Size(112, 26)
  Me.TBCliente.TabIndex = 1
  '
  'TBDocDate
  '
  Me.TBDocDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.TBDocDate.Location = New System.Drawing.Point(802, 55)
  Me.TBDocDate.Name = "TBDocDate"
  Me.TBDocDate.ReadOnly = True
  Me.TBDocDate.Size = New System.Drawing.Size(200, 24)
  Me.TBDocDate.TabIndex = 9
  '
  'TBDocNum
  '
  Me.TBDocNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.TBDocNum.ForeColor = System.Drawing.Color.DarkRed
  Me.TBDocNum.Location = New System.Drawing.Point(802, 7)
  Me.TBDocNum.Name = "TBDocNum"
  Me.TBDocNum.Size = New System.Drawing.Size(200, 44)
  Me.TBDocNum.TabIndex = 0
  '
  'Panel1
  '
  Me.Panel1.Controls.Add(Me.Label24)
  Me.Panel1.Controls.Add(Me.Label23)
  Me.Panel1.Controls.Add(Me.DGDetalle)
  Me.Panel1.Location = New System.Drawing.Point(53, 127)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(1175, 432)
  Me.Panel1.TabIndex = 3
  '
  'Label24
  '
  Me.Label24.AutoSize = True
  Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label24.Location = New System.Drawing.Point(1008, 410)
  Me.Label24.Name = "Label24"
  Me.Label24.Size = New System.Drawing.Size(20, 15)
  Me.Label24.TabIndex = 0
  Me.Label24.Text = "kg"
  '
  'Label23
  '
  Me.Label23.AutoSize = True
  Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label23.Location = New System.Drawing.Point(922, 410)
  Me.Label23.Name = "Label23"
  Me.Label23.Size = New System.Drawing.Size(64, 15)
  Me.Label23.TabIndex = 1
  Me.Label23.Text = "Peso total:"
  '
  'DGDetalle
  '
  Me.DGDetalle.AllowUserToAddRows = False
  Me.DGDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.DGDetalle.Location = New System.Drawing.Point(3, 3)
  Me.DGDetalle.Name = "DGDetalle"
  DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DGDetalle.RowsDefaultCellStyle = DataGridViewCellStyle1
  Me.DGDetalle.Size = New System.Drawing.Size(1165, 404)
  Me.DGDetalle.TabIndex = 0
  '
  'TextBox1
  '
  Me.TextBox1.Location = New System.Drawing.Point(1087, 94)
  Me.TextBox1.Name = "TextBox1"
  Me.TextBox1.Size = New System.Drawing.Size(79, 20)
  Me.TextBox1.TabIndex = 14
  '
  'ComboBox1
  '
  Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.ComboBox1.FormattingEnabled = True
  Me.ComboBox1.Items.AddRange(New Object() {"Cesar Cruz", "Omar Abed", "Juan Pablo", "Santiago", "Cristian", "Luis Angel", "Marco Mazzoco"})
  Me.ComboBox1.Location = New System.Drawing.Point(802, 86)
  Me.ComboBox1.Name = "ComboBox1"
  Me.ComboBox1.Size = New System.Drawing.Size(200, 24)
  Me.ComboBox1.TabIndex = 2
  Me.ComboBox1.Visible = False
  '
  'Label26
  '
  Me.Label26.AutoSize = True
  Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label26.Location = New System.Drawing.Point(721, 92)
  Me.Label26.Name = "Label26"
  Me.Label26.Size = New System.Drawing.Size(82, 16)
  Me.Label26.TabIndex = 10
  Me.Label26.Text = "Empacador:"
  Me.Label26.Visible = False
  '
  'Label25
  '
  Me.Label25.AutoSize = True
  Me.Label25.Location = New System.Drawing.Point(1033, 94)
  Me.Label25.Name = "Label25"
  Me.Label25.Size = New System.Drawing.Size(48, 26)
  Me.Label25.TabIndex = 13
  Me.Label25.Text = "Hora de " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Inicio"
  '
  'TBComentarios
  '
  Me.TBComentarios.Location = New System.Drawing.Point(143, 566)
  Me.TBComentarios.MaxLength = 275
  Me.TBComentarios.Multiline = True
  Me.TBComentarios.Name = "TBComentarios"
  Me.TBComentarios.Size = New System.Drawing.Size(295, 84)
  Me.TBComentarios.TabIndex = 5
  '
  'Label5
  '
  Me.Label5.AutoSize = True
  Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.Label5.Location = New System.Drawing.Point(50, 569)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(87, 16)
  Me.Label5.TabIndex = 7
  Me.Label5.Text = "Comentarios:"
  '
  'DataGridView1
  '
  Me.DataGridView1.AllowUserToAddRows = False
  Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
  Me.DataGridView1.Location = New System.Drawing.Point(898, 633)
  Me.DataGridView1.Name = "DataGridView1"
  DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
  Me.DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle2
  Me.DataGridView1.Size = New System.Drawing.Size(119, 10)
  Me.DataGridView1.TabIndex = 9
  Me.DataGridView1.Visible = False
  '
  'Timer1
  '
  Me.Timer1.Enabled = True
  Me.Timer1.Interval = 50
  '
  'BSave
  '
  Me.BSave.Image = CType(resources.GetObject("BSave.Image"), System.Drawing.Image)
  Me.BSave.Location = New System.Drawing.Point(1175, 594)
  Me.BSave.Name = "BSave"
  Me.BSave.Size = New System.Drawing.Size(53, 49)
  Me.BSave.TabIndex = 4
  Me.BSave.UseVisualStyleBackColor = True
  '
  'TBDocEntry
  '
  Me.TBDocEntry.Location = New System.Drawing.Point(1036, 66)
  Me.TBDocEntry.Name = "TBDocEntry"
  Me.TBDocEntry.Size = New System.Drawing.Size(93, 20)
  Me.TBDocEntry.TabIndex = 12
  Me.TBDocEntry.Visible = False
  '
  'Packin
  '
  Me.AcceptButton = Me.Button2
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(1280, 672)
  Me.Controls.Add(Me.TextBox1)
  Me.Controls.Add(Me.TBDocEntry)
  Me.Controls.Add(Me.Label25)
  Me.Controls.Add(Me.Label26)
  Me.Controls.Add(Me.ComboBox1)
  Me.Controls.Add(Me.BSave)
  Me.Controls.Add(Me.DataGridView1)
  Me.Controls.Add(Me.TBComentarios)
  Me.Controls.Add(Me.Label5)
  Me.Controls.Add(Me.Panel1)
  Me.Controls.Add(Me.PictureBox1)
  Me.Controls.Add(Me.Button2)
  Me.Controls.Add(Me.Label9)
  Me.Controls.Add(Me.Label10)
  Me.Controls.Add(Me.Label3)
  Me.Controls.Add(Me.Label2)
  Me.Controls.Add(Me.Label1)
  Me.Controls.Add(Me.TBPerCon)
  Me.Controls.Add(Me.TBNomCli)
  Me.Controls.Add(Me.TBCliente)
  Me.Controls.Add(Me.TBDocDate)
  Me.Controls.Add(Me.TBDocNum)
  Me.Name = "Packin"
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Packing"
  CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  CType(Me.DGDetalle, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub
 Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TBPerCon As System.Windows.Forms.TextBox
    Friend WithEvents TBNomCli As System.Windows.Forms.TextBox
    Friend WithEvents TBCliente As System.Windows.Forms.TextBox
    Friend WithEvents TBDocDate As System.Windows.Forms.TextBox
    Friend WithEvents TBDocNum As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DGDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents TBComentarios As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents BSave As System.Windows.Forms.Button
    Friend WithEvents TBDocEntry As System.Windows.Forms.TextBox
End Class
