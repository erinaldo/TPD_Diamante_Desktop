<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBarCode
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
  Me.Panel1 = New System.Windows.Forms.Panel()
  Me.txtBCPieza = New System.Windows.Forms.TextBox()
  Me.Label9 = New System.Windows.Forms.Label()
  Me.txtBCTarima = New System.Windows.Forms.TextBox()
  Me.Label8 = New System.Windows.Forms.Label()
  Me.txtBCCaja = New System.Windows.Forms.TextBox()
  Me.Label7 = New System.Windows.Forms.Label()
  Me.txtBCBolsa = New System.Windows.Forms.TextBox()
  Me.Label6 = New System.Windows.Forms.Label()
  Me.txtBarCode = New System.Windows.Forms.TextBox()
  Me.txtDescripcion = New System.Windows.Forms.TextBox()
  Me.txtCopias = New System.Windows.Forms.TextBox()
  Me.Label5 = New System.Windows.Forms.Label()
  Me.txtClasificacion = New System.Windows.Forms.TextBox()
  Me.Label4 = New System.Windows.Forms.Label()
  Me.Label3 = New System.Windows.Forms.Label()
  Me.Label2 = New System.Windows.Forms.Label()
  Me.Button1 = New System.Windows.Forms.Button()
  Me.btnGenerar = New System.Windows.Forms.Button()
  Me.cmbArticulos = New System.Windows.Forms.ComboBox()
  Me.Label1 = New System.Windows.Forms.Label()
  Me.rbtnBolsa = New System.Windows.Forms.RadioButton()
  Me.rbtnPieza = New System.Windows.Forms.RadioButton()
  Me.rbtnTarima = New System.Windows.Forms.RadioButton()
  Me.rbtnCaja = New System.Windows.Forms.RadioButton()
  Me.pbBarCode = New System.Windows.Forms.PictureBox()
  Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
  Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
  Me.rbtnInterno = New System.Windows.Forms.RadioButton()
  Me.Panel1.SuspendLayout()
  CType(Me.pbBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'Panel1
  '
  Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
  Me.Panel1.Controls.Add(Me.rbtnInterno)
  Me.Panel1.Controls.Add(Me.rbtnBolsa)
  Me.Panel1.Controls.Add(Me.rbtnTarima)
  Me.Panel1.Controls.Add(Me.txtBCPieza)
  Me.Panel1.Controls.Add(Me.rbtnPieza)
  Me.Panel1.Controls.Add(Me.rbtnCaja)
  Me.Panel1.Controls.Add(Me.Label9)
  Me.Panel1.Controls.Add(Me.txtBCTarima)
  Me.Panel1.Controls.Add(Me.Label8)
  Me.Panel1.Controls.Add(Me.txtBCCaja)
  Me.Panel1.Controls.Add(Me.Label7)
  Me.Panel1.Controls.Add(Me.txtBCBolsa)
  Me.Panel1.Controls.Add(Me.Label6)
  Me.Panel1.Controls.Add(Me.txtBarCode)
  Me.Panel1.Controls.Add(Me.txtDescripcion)
  Me.Panel1.Controls.Add(Me.txtCopias)
  Me.Panel1.Controls.Add(Me.Label5)
  Me.Panel1.Controls.Add(Me.txtClasificacion)
  Me.Panel1.Controls.Add(Me.Label4)
  Me.Panel1.Controls.Add(Me.Label3)
  Me.Panel1.Controls.Add(Me.Label2)
  Me.Panel1.Controls.Add(Me.Button1)
  Me.Panel1.Controls.Add(Me.btnGenerar)
  Me.Panel1.Controls.Add(Me.cmbArticulos)
  Me.Panel1.Controls.Add(Me.Label1)
  Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
  Me.Panel1.Location = New System.Drawing.Point(0, 0)
  Me.Panel1.Name = "Panel1"
  Me.Panel1.Size = New System.Drawing.Size(467, 262)
  Me.Panel1.TabIndex = 0
  '
  'txtBCPieza
  '
  Me.txtBCPieza.Location = New System.Drawing.Point(107, 118)
  Me.txtBCPieza.Name = "txtBCPieza"
  Me.txtBCPieza.Size = New System.Drawing.Size(262, 20)
  Me.txtBCPieza.TabIndex = 17
  '
  'Label9
  '
  Me.Label9.Location = New System.Drawing.Point(10, 121)
  Me.Label9.Name = "Label9"
  Me.Label9.Size = New System.Drawing.Size(91, 13)
  Me.Label9.TabIndex = 18
  Me.Label9.Text = "CB Pieza:"
  Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'txtBCTarima
  '
  Me.txtBCTarima.Location = New System.Drawing.Point(107, 202)
  Me.txtBCTarima.Name = "txtBCTarima"
  Me.txtBCTarima.Size = New System.Drawing.Size(262, 20)
  Me.txtBCTarima.TabIndex = 15
  '
  'Label8
  '
  Me.Label8.Location = New System.Drawing.Point(10, 205)
  Me.Label8.Name = "Label8"
  Me.Label8.Size = New System.Drawing.Size(91, 13)
  Me.Label8.TabIndex = 16
  Me.Label8.Text = "CB Tarima:"
  Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'txtBCCaja
  '
  Me.txtBCCaja.Location = New System.Drawing.Point(107, 174)
  Me.txtBCCaja.Name = "txtBCCaja"
  Me.txtBCCaja.Size = New System.Drawing.Size(262, 20)
  Me.txtBCCaja.TabIndex = 13
  '
  'Label7
  '
  Me.Label7.Location = New System.Drawing.Point(10, 177)
  Me.Label7.Name = "Label7"
  Me.Label7.Size = New System.Drawing.Size(91, 13)
  Me.Label7.TabIndex = 14
  Me.Label7.Text = "CB Caja:"
  Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'txtBCBolsa
  '
  Me.txtBCBolsa.Location = New System.Drawing.Point(107, 148)
  Me.txtBCBolsa.Name = "txtBCBolsa"
  Me.txtBCBolsa.Size = New System.Drawing.Size(262, 20)
  Me.txtBCBolsa.TabIndex = 11
  '
  'Label6
  '
  Me.Label6.Location = New System.Drawing.Point(10, 151)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(91, 13)
  Me.Label6.TabIndex = 12
  Me.Label6.Text = "CB Bolsa:"
  Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'txtBarCode
  '
  Me.txtBarCode.Location = New System.Drawing.Point(107, 89)
  Me.txtBarCode.Name = "txtBarCode"
  Me.txtBarCode.Size = New System.Drawing.Size(262, 20)
  Me.txtBarCode.TabIndex = 3
  '
  'txtDescripcion
  '
  Me.txtDescripcion.Location = New System.Drawing.Point(107, 41)
  Me.txtDescripcion.Multiline = True
  Me.txtDescripcion.Name = "txtDescripcion"
  Me.txtDescripcion.ReadOnly = True
  Me.txtDescripcion.Size = New System.Drawing.Size(343, 42)
  Me.txtDescripcion.TabIndex = 2
  '
  'txtCopias
  '
  Me.txtCopias.Location = New System.Drawing.Point(269, 228)
  Me.txtCopias.Name = "txtCopias"
  Me.txtCopias.Size = New System.Drawing.Size(100, 20)
  Me.txtCopias.TabIndex = 5
  '
  'Label5
  '
  Me.Label5.AutoSize = True
  Me.Label5.Location = New System.Drawing.Point(221, 231)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(42, 13)
  Me.Label5.TabIndex = 10
  Me.Label5.Text = "Copias:"
  '
  'txtClasificacion
  '
  Me.txtClasificacion.Location = New System.Drawing.Point(107, 228)
  Me.txtClasificacion.Name = "txtClasificacion"
  Me.txtClasificacion.Size = New System.Drawing.Size(100, 20)
  Me.txtClasificacion.TabIndex = 4
  '
  'Label4
  '
  Me.Label4.AutoSize = True
  Me.Label4.Location = New System.Drawing.Point(32, 231)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(69, 13)
  Me.Label4.TabIndex = 8
  Me.Label4.Text = "Clasificación:"
  '
  'Label3
  '
  Me.Label3.Location = New System.Drawing.Point(10, 92)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(91, 13)
  Me.Label3.TabIndex = 6
  Me.Label3.Text = "CB interno:"
  Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
  '
  'Label2
  '
  Me.Label2.AutoSize = True
  Me.Label2.Location = New System.Drawing.Point(35, 52)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(66, 13)
  Me.Label2.TabIndex = 4
  Me.Label2.Text = "Descripción:"
  '
  'Button1
  '
  Me.Button1.Location = New System.Drawing.Point(378, 228)
  Me.Button1.Name = "Button1"
  Me.Button1.Size = New System.Drawing.Size(75, 23)
  Me.Button1.TabIndex = 6
  Me.Button1.Text = "Imprimir"
  Me.Button1.UseVisualStyleBackColor = True
  '
  'btnGenerar
  '
  Me.btnGenerar.Location = New System.Drawing.Point(375, 11)
  Me.btnGenerar.Name = "btnGenerar"
  Me.btnGenerar.Size = New System.Drawing.Size(75, 23)
  Me.btnGenerar.TabIndex = 2
  Me.btnGenerar.Text = "&Generar"
  Me.btnGenerar.UseVisualStyleBackColor = True
  Me.btnGenerar.Visible = False
  '
  'cmbArticulos
  '
  Me.cmbArticulos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
  Me.cmbArticulos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
  Me.cmbArticulos.FormattingEnabled = True
  Me.cmbArticulos.Location = New System.Drawing.Point(107, 13)
  Me.cmbArticulos.Name = "cmbArticulos"
  Me.cmbArticulos.Size = New System.Drawing.Size(262, 21)
  Me.cmbArticulos.TabIndex = 1
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Location = New System.Drawing.Point(56, 16)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(45, 13)
  Me.Label1.TabIndex = 0
  Me.Label1.Text = "Articulo:"
  '
  'rbtnBolsa
  '
  Me.rbtnBolsa.AutoSize = True
  Me.rbtnBolsa.Location = New System.Drawing.Point(376, 150)
  Me.rbtnBolsa.Name = "rbtnBolsa"
  Me.rbtnBolsa.Size = New System.Drawing.Size(51, 17)
  Me.rbtnBolsa.TabIndex = 17
  Me.rbtnBolsa.TabStop = True
  Me.rbtnBolsa.Text = "Bolsa"
  Me.rbtnBolsa.UseVisualStyleBackColor = True
  '
  'rbtnPieza
  '
  Me.rbtnPieza.AutoSize = True
  Me.rbtnPieza.Location = New System.Drawing.Point(376, 119)
  Me.rbtnPieza.Name = "rbtnPieza"
  Me.rbtnPieza.Size = New System.Drawing.Size(51, 17)
  Me.rbtnPieza.TabIndex = 16
  Me.rbtnPieza.TabStop = True
  Me.rbtnPieza.Text = "Pieza"
  Me.rbtnPieza.UseVisualStyleBackColor = True
  '
  'rbtnTarima
  '
  Me.rbtnTarima.AutoSize = True
  Me.rbtnTarima.Location = New System.Drawing.Point(376, 204)
  Me.rbtnTarima.Name = "rbtnTarima"
  Me.rbtnTarima.Size = New System.Drawing.Size(57, 17)
  Me.rbtnTarima.TabIndex = 15
  Me.rbtnTarima.TabStop = True
  Me.rbtnTarima.Text = "Tarima"
  Me.rbtnTarima.UseVisualStyleBackColor = True
  '
  'rbtnCaja
  '
  Me.rbtnCaja.AutoSize = True
  Me.rbtnCaja.Location = New System.Drawing.Point(376, 176)
  Me.rbtnCaja.Name = "rbtnCaja"
  Me.rbtnCaja.Size = New System.Drawing.Size(46, 17)
  Me.rbtnCaja.TabIndex = 14
  Me.rbtnCaja.TabStop = True
  Me.rbtnCaja.Text = "Caja"
  Me.rbtnCaja.UseVisualStyleBackColor = True
  '
  'pbBarCode
  '
  Me.pbBarCode.Location = New System.Drawing.Point(32, 268)
  Me.pbBarCode.Name = "pbBarCode"
  Me.pbBarCode.Size = New System.Drawing.Size(403, 97)
  Me.pbBarCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
  Me.pbBarCode.TabIndex = 1
  Me.pbBarCode.TabStop = False
  Me.pbBarCode.Tag = ""
  '
  'PrintDialog1
  '
  Me.PrintDialog1.UseEXDialog = True
  '
  'PrintDocument1
  '
  '
  'rbtnInterno
  '
  Me.rbtnInterno.AutoSize = True
  Me.rbtnInterno.Location = New System.Drawing.Point(376, 91)
  Me.rbtnInterno.Name = "rbtnInterno"
  Me.rbtnInterno.Size = New System.Drawing.Size(58, 17)
  Me.rbtnInterno.TabIndex = 19
  Me.rbtnInterno.TabStop = True
  Me.rbtnInterno.Text = "Interno"
  Me.rbtnInterno.UseVisualStyleBackColor = True
  '
  'frmBarCode
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(467, 377)
  Me.Controls.Add(Me.pbBarCode)
  Me.Controls.Add(Me.Panel1)
  Me.Name = "frmBarCode"
  Me.ShowIcon = False
  Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
  Me.Text = "Generar BarCode"
  Me.Panel1.ResumeLayout(False)
  Me.Panel1.PerformLayout()
  CType(Me.pbBarCode, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)

 End Sub

 Friend WithEvents Panel1 As Panel
 Friend WithEvents Label1 As Label
 Friend WithEvents cmbArticulos As ComboBox
 Friend WithEvents pbBarCode As PictureBox
 Friend WithEvents btnGenerar As Button
 Friend WithEvents Button1 As Button
 Friend WithEvents PrintDialog1 As PrintDialog
 Friend WithEvents PrintDocument1 As Printing.PrintDocument
 Friend WithEvents Label2 As Label
 Friend WithEvents Label3 As Label
 Friend WithEvents txtClasificacion As TextBox
 Friend WithEvents Label4 As Label
 Friend WithEvents txtCopias As TextBox
 Friend WithEvents Label5 As Label
 Friend WithEvents txtBarCode As TextBox
 Friend WithEvents txtDescripcion As TextBox
 Friend WithEvents rbtnBolsa As RadioButton
 Friend WithEvents rbtnPieza As RadioButton
 Friend WithEvents rbtnTarima As RadioButton
 Friend WithEvents rbtnCaja As RadioButton
 Friend WithEvents txtBCTarima As TextBox
 Friend WithEvents Label8 As Label
 Friend WithEvents txtBCCaja As TextBox
 Friend WithEvents Label7 As Label
 Friend WithEvents txtBCBolsa As TextBox
 Friend WithEvents Label6 As Label
 Friend WithEvents txtBCPieza As TextBox
 Friend WithEvents Label9 As Label
 Friend WithEvents rbtnInterno As RadioButton
End Class
