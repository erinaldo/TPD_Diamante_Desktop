<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEnviarCorreo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEnviarCorreo))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPara = New System.Windows.Forms.TextBox()
        Me.txtCC = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtsunto = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCuerpo = New System.Windows.Forms.TextBox()
        Me.btnEnviarCorreo = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.lblOrigen = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(135, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "PARA:"
        '
        'txtPara
        '
        Me.txtPara.Location = New System.Drawing.Point(185, 12)
        Me.txtPara.Multiline = True
        Me.txtPara.Name = "txtPara"
        Me.txtPara.Size = New System.Drawing.Size(308, 20)
        Me.txtPara.TabIndex = 1
        Me.txtPara.Text = "jose.torres@tractozone.com.mx"
        '
        'txtCC
        '
        Me.txtCC.Location = New System.Drawing.Point(185, 38)
        Me.txtCC.Multiline = True
        Me.txtCC.Name = "txtCC"
        Me.txtCC.Size = New System.Drawing.Size(308, 20)
        Me.txtCC.TabIndex = 3
        Me.txtCC.Text = "compras2@tractopartesdiamante.com.mx" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(152, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "CC:"
        '
        'txtsunto
        '
        Me.txtsunto.Location = New System.Drawing.Point(185, 64)
        Me.txtsunto.Multiline = True
        Me.txtsunto.Name = "txtsunto"
        Me.txtsunto.Size = New System.Drawing.Size(308, 20)
        Me.txtsunto.TabIndex = 5
        Me.txtsunto.Text = "Pedido Diario"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(117, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "ASUNTO:"
        '
        'txtCuerpo
        '
        Me.txtCuerpo.Location = New System.Drawing.Point(12, 90)
        Me.txtCuerpo.Multiline = True
        Me.txtCuerpo.Name = "txtCuerpo"
        Me.txtCuerpo.Size = New System.Drawing.Size(481, 230)
        Me.txtCuerpo.TabIndex = 7
        Me.txtCuerpo.Text = "Buenas tardes, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Adjunto encuentras el archivo en Excel con el requerimiento de n" &
    "uestro pedido diario ¿Me apoyas con la disponibilidad?" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'btnEnviarCorreo
        '
        Me.btnEnviarCorreo.BackgroundImage = CType(resources.GetObject("btnEnviarCorreo.BackgroundImage"), System.Drawing.Image)
        Me.btnEnviarCorreo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnviarCorreo.Location = New System.Drawing.Point(12, 12)
        Me.btnEnviarCorreo.Name = "btnEnviarCorreo"
        Me.btnEnviarCorreo.Size = New System.Drawing.Size(99, 72)
        Me.btnEnviarCorreo.TabIndex = 8
        Me.btnEnviarCorreo.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 321)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Adjunto:"
        '
        'lblRuta
        '
        Me.lblRuta.Location = New System.Drawing.Point(57, 322)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(436, 27)
        Me.lblRuta.TabIndex = 10
        '
        'lblOrigen
        '
        Me.lblOrigen.AutoSize = True
        Me.lblOrigen.Location = New System.Drawing.Point(12, -4)
        Me.lblOrigen.Name = "lblOrigen"
        Me.lblOrigen.Size = New System.Drawing.Size(38, 13)
        Me.lblOrigen.TabIndex = 11
        Me.lblOrigen.Text = "Origen"
        Me.lblOrigen.Visible = False
        '
        'frmEnviarCorreo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(505, 351)
        Me.Controls.Add(Me.lblOrigen)
        Me.Controls.Add(Me.lblRuta)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnEnviarCorreo)
        Me.Controls.Add(Me.txtCuerpo)
        Me.Controls.Add(Me.txtsunto)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCC)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPara)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEnviarCorreo"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Enviar correo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtPara As TextBox
    Friend WithEvents txtCC As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtsunto As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtCuerpo As TextBox
    Friend WithEvents btnEnviarCorreo As Button
  Friend WithEvents Label4 As Label
  Friend WithEvents lblRuta As Label
  Friend WithEvents lblOrigen As Label
End Class
