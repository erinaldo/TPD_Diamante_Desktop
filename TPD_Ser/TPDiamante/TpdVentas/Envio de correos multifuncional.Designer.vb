<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Envio_de_correos_multifuncional
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
        Me.cmbPara = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rbCliente = New System.Windows.Forms.RadioButton()
        Me.rbExterno = New System.Windows.Forms.RadioButton()
        Me.txtCC = New System.Windows.Forms.TextBox()
        Me.txtCorreo = New System.Windows.Forms.TextBox()
        Me.lblEnviar = New System.Windows.Forms.Label()
        Me.txtPara = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.txtasunto = New System.Windows.Forms.TextBox()
        Me.txtRFC = New System.Windows.Forms.TextBox()
        Me.cmbCodigo = New System.Windows.Forms.ComboBox()
        Me.cmbCorreos = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtFirma = New System.Windows.Forms.RichTextBox()
        Me.txtAgradecimientos = New System.Windows.Forms.RichTextBox()
        Me.rctxtCuerpo = New System.Windows.Forms.RichTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cmbRFC = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbPara
        '
        Me.cmbPara.FormattingEnabled = True
        Me.cmbPara.Location = New System.Drawing.Point(109, 98)
        Me.cmbPara.Name = "cmbPara"
        Me.cmbPara.Size = New System.Drawing.Size(221, 21)
        Me.cmbPara.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 264)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "CC:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "PARA:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 302)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "ASUNTO:"
        '
        'rbCliente
        '
        Me.rbCliente.AutoSize = True
        Me.rbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCliente.Location = New System.Drawing.Point(11, 43)
        Me.rbCliente.Name = "rbCliente"
        Me.rbCliente.Size = New System.Drawing.Size(77, 17)
        Me.rbCliente.TabIndex = 6
        Me.rbCliente.TabStop = True
        Me.rbCliente.Text = "CLIENTE"
        Me.rbCliente.UseVisualStyleBackColor = True
        '
        'rbExterno
        '
        Me.rbExterno.AutoSize = True
        Me.rbExterno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbExterno.Location = New System.Drawing.Point(109, 43)
        Me.rbExterno.Name = "rbExterno"
        Me.rbExterno.Size = New System.Drawing.Size(84, 17)
        Me.rbExterno.TabIndex = 7
        Me.rbExterno.TabStop = True
        Me.rbExterno.Text = "EXTERNO"
        Me.rbExterno.UseVisualStyleBackColor = True
        '
        'txtCC
        '
        Me.txtCC.Location = New System.Drawing.Point(109, 261)
        Me.txtCC.Name = "txtCC"
        Me.txtCC.Size = New System.Drawing.Size(221, 20)
        Me.txtCC.TabIndex = 9
        Me.txtCC.Text = "programador.ti@tractopartesdiamante.com.mx"
        '
        'txtCorreo
        '
        Me.txtCorreo.Location = New System.Drawing.Point(109, 344)
        Me.txtCorreo.Name = "txtCorreo"
        Me.txtCorreo.Size = New System.Drawing.Size(221, 20)
        Me.txtCorreo.TabIndex = 10
        '
        'lblEnviar
        '
        Me.lblEnviar.AutoSize = True
        Me.lblEnviar.BackColor = System.Drawing.Color.White
        Me.lblEnviar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnviar.Location = New System.Drawing.Point(4, 347)
        Me.lblEnviar.Name = "lblEnviar"
        Me.lblEnviar.Size = New System.Drawing.Size(41, 13)
        Me.lblEnviar.TabIndex = 11
        Me.lblEnviar.Text = "E-mail"
        Me.lblEnviar.Visible = False
        '
        'txtPara
        '
        Me.txtPara.Location = New System.Drawing.Point(109, 132)
        Me.txtPara.Multiline = True
        Me.txtPara.Name = "txtPara"
        Me.txtPara.Size = New System.Drawing.Size(221, 101)
        Me.txtPara.TabIndex = 8
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtCodigo)
        Me.Panel1.Controls.Add(Me.txtasunto)
        Me.Panel1.Controls.Add(Me.txtRFC)
        Me.Panel1.Controls.Add(Me.txtPara)
        Me.Panel1.Controls.Add(Me.rbExterno)
        Me.Panel1.Controls.Add(Me.rbCliente)
        Me.Panel1.Controls.Add(Me.cmbPara)
        Me.Panel1.Controls.Add(Me.lblEnviar)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtCorreo)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtCC)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(362, 638)
        Me.Panel1.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 411)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "RFC"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 380)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Código Cliente"
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(109, 377)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(221, 20)
        Me.txtCodigo.TabIndex = 17
        '
        'txtasunto
        '
        Me.txtasunto.Location = New System.Drawing.Point(109, 299)
        Me.txtasunto.Name = "txtasunto"
        Me.txtasunto.Size = New System.Drawing.Size(221, 20)
        Me.txtasunto.TabIndex = 16
        Me.txtasunto.Text = "Conoce los beneficios por ser un cliente leal de Tracto Partes Diamante de Puebla" &
    ""
        '
        'txtRFC
        '
        Me.txtRFC.Location = New System.Drawing.Point(109, 408)
        Me.txtRFC.Name = "txtRFC"
        Me.txtRFC.Size = New System.Drawing.Size(221, 20)
        Me.txtRFC.TabIndex = 14
        '
        'cmbCodigo
        '
        Me.cmbCodigo.FormattingEnabled = True
        Me.cmbCodigo.Location = New System.Drawing.Point(301, 465)
        Me.cmbCodigo.Name = "cmbCodigo"
        Me.cmbCodigo.Size = New System.Drawing.Size(102, 21)
        Me.cmbCodigo.TabIndex = 15
        '
        'cmbCorreos
        '
        Me.cmbCorreos.FormattingEnabled = True
        Me.cmbCorreos.Location = New System.Drawing.Point(183, 465)
        Me.cmbCorreos.Name = "cmbCorreos"
        Me.cmbCorreos.Size = New System.Drawing.Size(102, 21)
        Me.cmbCorreos.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.txtFirma)
        Me.Panel2.Controls.Add(Me.txtAgradecimientos)
        Me.Panel2.Controls.Add(Me.rctxtCuerpo)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Controls.Add(Me.cmbCodigo)
        Me.Panel2.Controls.Add(Me.cmbCorreos)
        Me.Panel2.Controls.Add(Me.cmbRFC)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(362, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(611, 638)
        Me.Panel2.TabIndex = 14
        '
        'txtFirma
        '
        Me.txtFirma.Enabled = False
        Me.txtFirma.Location = New System.Drawing.Point(23, 525)
        Me.txtFirma.Name = "txtFirma"
        Me.txtFirma.Size = New System.Drawing.Size(569, 60)
        Me.txtFirma.TabIndex = 27
        Me.txtFirma.Text = ""
        '
        'txtAgradecimientos
        '
        Me.txtAgradecimientos.Enabled = False
        Me.txtAgradecimientos.Location = New System.Drawing.Point(23, 433)
        Me.txtAgradecimientos.Name = "txtAgradecimientos"
        Me.txtAgradecimientos.Size = New System.Drawing.Size(576, 53)
        Me.txtAgradecimientos.TabIndex = 26
        Me.txtAgradecimientos.Text = ""
        '
        'rctxtCuerpo
        '
        Me.rctxtCuerpo.Enabled = False
        Me.rctxtCuerpo.Location = New System.Drawing.Point(21, 132)
        Me.rctxtCuerpo.Name = "rctxtCuerpo"
        Me.rctxtCuerpo.Size = New System.Drawing.Size(573, 243)
        Me.rctxtCuerpo.TabIndex = 25
        Me.rctxtCuerpo.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(22, 499)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "FIRMA:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(22, 395)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(130, 13)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "AGRADECIMIENTOS:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(22, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "CUERPO MENSAJE:"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(468, 599)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(124, 32)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "ENVIAR"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 50)
        Me.PictureBox1.TabIndex = 28
        Me.PictureBox1.TabStop = False
        '
        'cmbRFC
        '
        Me.cmbRFC.FormattingEnabled = True
        Me.cmbRFC.Location = New System.Drawing.Point(50, 465)
        Me.cmbRFC.Name = "cmbRFC"
        Me.cmbRFC.Size = New System.Drawing.Size(102, 21)
        Me.cmbRFC.TabIndex = 24
        '
        'Envio_de_correos_multifuncional
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(973, 638)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Envio_de_correos_multifuncional"
        Me.Text = "Envio_de_correos_multifuncional"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmbPara As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents rbCliente As RadioButton
    Friend WithEvents rbExterno As RadioButton
    Friend WithEvents txtCC As TextBox
    Friend WithEvents txtCorreo As TextBox
    Friend WithEvents lblEnviar As Label
    Friend WithEvents txtPara As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button1 As Button
    Friend WithEvents cmbCorreos As ComboBox
    Friend WithEvents txtRFC As TextBox
    Friend WithEvents cmbCodigo As ComboBox
    Friend WithEvents txtasunto As TextBox
    Friend WithEvents txtCodigo As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents cmbRFC As ComboBox
    Friend WithEvents rctxtCuerpo As RichTextBox
    Public WithEvents txtFirma As RichTextBox
    Friend WithEvents txtAgradecimientos As RichTextBox
End Class
