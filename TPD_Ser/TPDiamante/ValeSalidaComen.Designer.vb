<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ValeSalidaComen
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TBArticulo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TBLinea = New System.Windows.Forms.TextBox()
        Me.TBDescripcion = New System.Windows.Forms.TextBox()
        Me.TBMotivo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TBEntrega = New System.Windows.Forms.TextBox()
        Me.TBComen = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(25, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Descripción"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(209, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Linea"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(26, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Artículo"
        '
        'TBArticulo
        '
        Me.TBArticulo.Enabled = False
        Me.TBArticulo.Location = New System.Drawing.Point(29, 30)
        Me.TBArticulo.Name = "TBArticulo"
        Me.TBArticulo.Size = New System.Drawing.Size(174, 20)
        Me.TBArticulo.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(209, 154)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 15)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Comentarios"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 15)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Motivo"
        '
        'TBLinea
        '
        Me.TBLinea.Enabled = False
        Me.TBLinea.Location = New System.Drawing.Point(212, 30)
        Me.TBLinea.Name = "TBLinea"
        Me.TBLinea.Size = New System.Drawing.Size(151, 20)
        Me.TBLinea.TabIndex = 25
        '
        'TBDescripcion
        '
        Me.TBDescripcion.Enabled = False
        Me.TBDescripcion.Location = New System.Drawing.Point(29, 75)
        Me.TBDescripcion.Name = "TBDescripcion"
        Me.TBDescripcion.Size = New System.Drawing.Size(209, 20)
        Me.TBDescripcion.TabIndex = 26
        '
        'TBMotivo
        '
        Me.TBMotivo.Location = New System.Drawing.Point(29, 122)
        Me.TBMotivo.Name = "TBMotivo"
        Me.TBMotivo.Size = New System.Drawing.Size(209, 20)
        Me.TBMotivo.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(25, 154)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 15)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Se entrega a:"
        '
        'TBEntrega
        '
        Me.TBEntrega.Location = New System.Drawing.Point(29, 172)
        Me.TBEntrega.Name = "TBEntrega"
        Me.TBEntrega.Size = New System.Drawing.Size(151, 20)
        Me.TBEntrega.TabIndex = 35
        '
        'TBComen
        '
        Me.TBComen.Location = New System.Drawing.Point(212, 172)
        Me.TBComen.MaxLength = 275
        Me.TBComen.Multiline = True
        Me.TBComen.Name = "TBComen"
        Me.TBComen.Size = New System.Drawing.Size(174, 78)
        Me.TBComen.TabIndex = 36
        '
        'ValeSalidaComen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 262)
        Me.Controls.Add(Me.TBComen)
        Me.Controls.Add(Me.TBEntrega)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TBMotivo)
        Me.Controls.Add(Me.TBDescripcion)
        Me.Controls.Add(Me.TBLinea)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TBArticulo)
        Me.Name = "ValeSalidaComen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ValeSalidaComen"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TBArticulo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TBLinea As System.Windows.Forms.TextBox
    Friend WithEvents TBDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents TBMotivo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TBEntrega As System.Windows.Forms.TextBox
    Friend WithEvents TBComen As System.Windows.Forms.TextBox
End Class
