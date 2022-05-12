<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pedido_Sugerido
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pedido_Sugerido))
        Me.DGAgentes = New System.Windows.Forms.DataGridView()
        Me.DGClientes = New System.Windows.Forms.DataGridView()
        Me.DGDetalle = New System.Windows.Forms.DataGridView()
        Me.DGPedido = New System.Windows.Forms.DataGridView()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LblVerSol = New System.Windows.Forms.Label()
        Me.BtnVerSol = New System.Windows.Forms.Button()
        Me.BtnEmail = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LblExito = New System.Windows.Forms.Label()
        Me.LblError = New System.Windows.Forms.Label()
        Me.LblMensaje = New System.Windows.Forms.Label()
        Me.TxtCorreoC = New System.Windows.Forms.TextBox()
        Me.TxtCorreoAd = New System.Windows.Forms.TextBox()
        Me.BtnMenos = New System.Windows.Forms.Button()
        Me.BtnMas = New System.Windows.Forms.Button()
        Me.DGListaArt = New System.Windows.Forms.DataGridView()
        CType(Me.DGAgentes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGListaArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGAgentes
        '
        Me.DGAgentes.AllowUserToAddRows = False
        Me.DGAgentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGAgentes.Location = New System.Drawing.Point(23, 43)
        Me.DGAgentes.Name = "DGAgentes"
        Me.DGAgentes.Size = New System.Drawing.Size(288, 301)
        Me.DGAgentes.TabIndex = 0
        '
        'DGClientes
        '
        Me.DGClientes.AllowUserToAddRows = False
        Me.DGClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGClientes.Location = New System.Drawing.Point(325, 43)
        Me.DGClientes.Name = "DGClientes"
        Me.DGClientes.Size = New System.Drawing.Size(346, 301)
        Me.DGClientes.TabIndex = 1
        '
        'DGDetalle
        '
        Me.DGDetalle.AllowUserToAddRows = False
        Me.DGDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGDetalle.Location = New System.Drawing.Point(685, 43)
        Me.DGDetalle.Name = "DGDetalle"
        Me.DGDetalle.Size = New System.Drawing.Size(558, 301)
        Me.DGDetalle.TabIndex = 2
        '
        'DGPedido
        '
        Me.DGPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGPedido.Location = New System.Drawing.Point(23, 381)
        Me.DGPedido.Name = "DGPedido"
        Me.DGPedido.Size = New System.Drawing.Size(583, 271)
        Me.DGPedido.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Gainsboro
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(20, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 17)
        Me.Label11.TabIndex = 175
        Me.Label11.Text = "Agentes"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Gainsboro
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(322, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 17)
        Me.Label1.TabIndex = 176
        Me.Label1.Text = "Clientes"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Gainsboro
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(682, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 17)
        Me.Label2.TabIndex = 177
        Me.Label2.Text = "Detalle de Ventas"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Gainsboro
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(20, 361)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(113, 17)
        Me.Label3.TabIndex = 178
        Me.Label3.Text = "Pedido Sugerido"
        '
        'LblVerSol
        '
        Me.LblVerSol.AutoSize = True
        Me.LblVerSol.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVerSol.Location = New System.Drawing.Point(615, 422)
        Me.LblVerSol.Name = "LblVerSol"
        Me.LblVerSol.Size = New System.Drawing.Size(61, 13)
        Me.LblVerSol.TabIndex = 180
        Me.LblVerSol.Text = "Visualizar"
        '
        'BtnVerSol
        '
        Me.BtnVerSol.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnVerSol.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnVerSol.Image = Global.TPDiamante.My.Resources.Resources.printer
        Me.BtnVerSol.Location = New System.Drawing.Point(624, 381)
        Me.BtnVerSol.Name = "BtnVerSol"
        Me.BtnVerSol.Size = New System.Drawing.Size(43, 39)
        Me.BtnVerSol.TabIndex = 179
        Me.BtnVerSol.Text = "            "
        Me.BtnVerSol.UseVisualStyleBackColor = True
        '
        'BtnEmail
        '
        Me.BtnEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEmail.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnEmail.Image = CType(resources.GetObject("BtnEmail.Image"), System.Drawing.Image)
        Me.BtnEmail.Location = New System.Drawing.Point(624, 463)
        Me.BtnEmail.Name = "BtnEmail"
        Me.BtnEmail.Size = New System.Drawing.Size(43, 30)
        Me.BtnEmail.TabIndex = 181
        Me.BtnEmail.Text = "            "
        Me.BtnEmail.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(625, 494)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 13)
        Me.Label4.TabIndex = 182
        Me.Label4.Text = "Enviar"
        '
        'LblExito
        '
        Me.LblExito.AutoSize = True
        Me.LblExito.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblExito.ForeColor = System.Drawing.Color.Green
        Me.LblExito.Location = New System.Drawing.Point(21, 704)
        Me.LblExito.Name = "LblExito"
        Me.LblExito.Size = New System.Drawing.Size(260, 15)
        Me.LblExito.TabIndex = 183
        Me.LblExito.Text = "Orden Creada y Enviada Exitosamente !"
        Me.LblExito.Visible = False
        '
        'LblError
        '
        Me.LblError.AutoSize = True
        Me.LblError.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblError.ForeColor = System.Drawing.Color.Red
        Me.LblError.Location = New System.Drawing.Point(20, 719)
        Me.LblError.Name = "LblError"
        Me.LblError.Size = New System.Drawing.Size(337, 15)
        Me.LblError.TabIndex = 184
        Me.LblError.Text = "No fue posible enviar EMails. Intentelo nuevamente"
        Me.LblError.Visible = False
        '
        'LblMensaje
        '
        Me.LblMensaje.AutoSize = True
        Me.LblMensaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMensaje.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.LblMensaje.Location = New System.Drawing.Point(20, 719)
        Me.LblMensaje.Name = "LblMensaje"
        Me.LblMensaje.Size = New System.Drawing.Size(412, 15)
        Me.LblMensaje.TabIndex = 185
        Me.LblMensaje.Text = "Creando Orden de Venta y Enviando Correo Electronico. . . . . . ."
        Me.LblMensaje.Visible = False
        '
        'TxtCorreoC
        '
        Me.TxtCorreoC.Location = New System.Drawing.Point(23, 659)
        Me.TxtCorreoC.Name = "TxtCorreoC"
        Me.TxtCorreoC.Size = New System.Drawing.Size(275, 20)
        Me.TxtCorreoC.TabIndex = 190
        '
        'TxtCorreoAd
        '
        Me.TxtCorreoAd.Location = New System.Drawing.Point(23, 685)
        Me.TxtCorreoAd.Name = "TxtCorreoAd"
        Me.TxtCorreoAd.Size = New System.Drawing.Size(275, 20)
        Me.TxtCorreoAd.TabIndex = 189
        '
        'BtnMenos
        '
        Me.BtnMenos.BackColor = System.Drawing.SystemColors.Control
        Me.BtnMenos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMenos.Image = Global.TPDiamante.My.Resources.Resources.Minus__6_
        Me.BtnMenos.Location = New System.Drawing.Point(629, 596)
        Me.BtnMenos.Name = "BtnMenos"
        Me.BtnMenos.Size = New System.Drawing.Size(38, 43)
        Me.BtnMenos.TabIndex = 192
        Me.BtnMenos.UseVisualStyleBackColor = False
        Me.BtnMenos.Visible = False
        '
        'BtnMas
        '
        Me.BtnMas.BackColor = System.Drawing.SystemColors.Control
        Me.BtnMas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMas.Image = Global.TPDiamante.My.Resources.Resources.Plus__6_
        Me.BtnMas.Location = New System.Drawing.Point(628, 537)
        Me.BtnMas.Name = "BtnMas"
        Me.BtnMas.Size = New System.Drawing.Size(38, 43)
        Me.BtnMas.TabIndex = 191
        Me.BtnMas.UseVisualStyleBackColor = False
        Me.BtnMas.Visible = False
        '
        'DGListaArt
        '
        Me.DGListaArt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGListaArt.Location = New System.Drawing.Point(685, 381)
        Me.DGListaArt.Name = "DGListaArt"
        Me.DGListaArt.Size = New System.Drawing.Size(558, 271)
        Me.DGListaArt.TabIndex = 193
        Me.DGListaArt.Visible = False
        '
        'Pedido_Sugerido
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1255, 739)
        Me.Controls.Add(Me.DGListaArt)
        Me.Controls.Add(Me.BtnMenos)
        Me.Controls.Add(Me.BtnMas)
        Me.Controls.Add(Me.TxtCorreoC)
        Me.Controls.Add(Me.TxtCorreoAd)
        Me.Controls.Add(Me.LblExito)
        Me.Controls.Add(Me.LblError)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.BtnEmail)
        Me.Controls.Add(Me.LblVerSol)
        Me.Controls.Add(Me.BtnVerSol)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.DGPedido)
        Me.Controls.Add(Me.DGDetalle)
        Me.Controls.Add(Me.DGClientes)
        Me.Controls.Add(Me.DGAgentes)
        Me.Controls.Add(Me.LblMensaje)
        Me.Name = "Pedido_Sugerido"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pedido Sugerido"
        CType(Me.DGAgentes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGClientes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGPedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGListaArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGAgentes As System.Windows.Forms.DataGridView
    Friend WithEvents DGClientes As System.Windows.Forms.DataGridView
    Friend WithEvents DGDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents DGPedido As System.Windows.Forms.DataGridView
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LblVerSol As System.Windows.Forms.Label
    Friend WithEvents BtnVerSol As System.Windows.Forms.Button
    Friend WithEvents BtnEmail As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LblExito As System.Windows.Forms.Label
    Friend WithEvents LblError As System.Windows.Forms.Label
    Friend WithEvents LblMensaje As System.Windows.Forms.Label
    Friend WithEvents TxtCorreoC As System.Windows.Forms.TextBox
    Friend WithEvents TxtCorreoAd As System.Windows.Forms.TextBox
    Friend WithEvents BtnMenos As System.Windows.Forms.Button
    Friend WithEvents BtnMas As System.Windows.Forms.Button
    Friend WithEvents DGListaArt As System.Windows.Forms.DataGridView
End Class
