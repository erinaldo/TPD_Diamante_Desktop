<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScoreCardIngresarObjetivos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScoreCardIngresarObjetivos))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TBObjetivo = New System.Windows.Forms.TextBox()
        Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnConsultar = New System.Windows.Forms.Button()
        Me.TextBoxAño = New System.Windows.Forms.TextBox()
        Me.ComboBoxMes = New System.Windows.Forms.ComboBox()
        Me.btn_last = New System.Windows.Forms.Button()
        Me.btn_next = New System.Windows.Forms.Button()
        Me.btn_Previous = New System.Windows.Forms.Button()
        Me.btn_first = New System.Windows.Forms.Button()
        Me.DGObjetivos = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnAgregar = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.DGObjetivos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(219, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 219
        Me.Label3.Text = "Objetivo"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(49, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 218
        Me.Label4.Text = "Agente"
        '
        'TBObjetivo
        '
        Me.TBObjetivo.Location = New System.Drawing.Point(222, 105)
        Me.TBObjetivo.Name = "TBObjetivo"
        Me.TBObjetivo.Size = New System.Drawing.Size(100, 20)
        Me.TBObjetivo.TabIndex = 217
        '
        'CmbAgteVta
        '
        Me.CmbAgteVta.FormattingEnabled = True
        Me.CmbAgteVta.Location = New System.Drawing.Point(52, 102)
        Me.CmbAgteVta.Name = "CmbAgteVta"
        Me.CmbAgteVta.Size = New System.Drawing.Size(109, 21)
        Me.CmbAgteVta.TabIndex = 216
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(219, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 212
        Me.Label2.Text = "Año"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(49, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 211
        Me.Label1.Text = "Mes"
        '
        'BtnConsultar
        '
        Me.BtnConsultar.BackColor = System.Drawing.Color.AliceBlue
        Me.BtnConsultar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnConsultar.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnConsultar.Location = New System.Drawing.Point(370, 70)
        Me.BtnConsultar.Name = "BtnConsultar"
        Me.BtnConsultar.Size = New System.Drawing.Size(86, 36)
        Me.BtnConsultar.TabIndex = 210
        Me.BtnConsultar.Text = "Consultar"
        Me.BtnConsultar.UseVisualStyleBackColor = False
        '
        'TextBoxAño
        '
        Me.TextBoxAño.Location = New System.Drawing.Point(222, 55)
        Me.TextBoxAño.Name = "TextBoxAño"
        Me.TextBoxAño.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxAño.TabIndex = 209
        '
        'ComboBoxMes
        '
        Me.ComboBoxMes.FormattingEnabled = True
        Me.ComboBoxMes.Location = New System.Drawing.Point(52, 54)
        Me.ComboBoxMes.Name = "ComboBoxMes"
        Me.ComboBoxMes.Size = New System.Drawing.Size(109, 21)
        Me.ComboBoxMes.TabIndex = 208
        '
        'btn_last
        '
        Me.btn_last.Location = New System.Drawing.Point(210, 348)
        Me.btn_last.Name = "btn_last"
        Me.btn_last.Size = New System.Drawing.Size(38, 23)
        Me.btn_last.TabIndex = 207
        Me.btn_last.Text = "Button7"
        Me.btn_last.UseVisualStyleBackColor = True
        '
        'btn_next
        '
        Me.btn_next.Location = New System.Drawing.Point(156, 348)
        Me.btn_next.Name = "btn_next"
        Me.btn_next.Size = New System.Drawing.Size(38, 23)
        Me.btn_next.TabIndex = 206
        Me.btn_next.Text = "Button6"
        Me.btn_next.UseVisualStyleBackColor = True
        '
        'btn_Previous
        '
        Me.btn_Previous.Location = New System.Drawing.Point(103, 348)
        Me.btn_Previous.Name = "btn_Previous"
        Me.btn_Previous.Size = New System.Drawing.Size(38, 23)
        Me.btn_Previous.TabIndex = 205
        Me.btn_Previous.Text = "Button5"
        Me.btn_Previous.UseVisualStyleBackColor = True
        '
        'btn_first
        '
        Me.btn_first.Location = New System.Drawing.Point(50, 348)
        Me.btn_first.Name = "btn_first"
        Me.btn_first.Size = New System.Drawing.Size(38, 23)
        Me.btn_first.TabIndex = 204
        Me.btn_first.Text = "Button4"
        Me.btn_first.UseVisualStyleBackColor = True
        '
        'DGObjetivos
        '
        Me.DGObjetivos.AllowUserToAddRows = False
        Me.DGObjetivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGObjetivos.Location = New System.Drawing.Point(46, 141)
        Me.DGObjetivos.Name = "DGObjetivos"
        Me.DGObjetivos.Size = New System.Drawing.Size(721, 190)
        Me.DGObjetivos.TabIndex = 203
        '
        'Button2
        '
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(710, 70)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(57, 44)
        Me.Button2.TabIndex = 215
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(705, 348)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(62, 44)
        Me.Button1.TabIndex = 214
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BtnAgregar
        '
        Me.BtnAgregar.BackColor = System.Drawing.SystemColors.Control
        Me.BtnAgregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAgregar.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnAgregar.Image = CType(resources.GetObject("BtnAgregar.Image"), System.Drawing.Image)
        Me.BtnAgregar.Location = New System.Drawing.Point(647, 70)
        Me.BtnAgregar.Name = "BtnAgregar"
        Me.BtnAgregar.Size = New System.Drawing.Size(57, 44)
        Me.BtnAgregar.TabIndex = 213
        Me.BtnAgregar.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Algerian", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(644, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(131, 21)
        Me.Label5.TabIndex = 220
        Me.Label5.Text = "SCORE CARD"
        '
        'ScoreCardIngresarObjetivos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(823, 451)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TBObjetivo)
        Me.Controls.Add(Me.CmbAgteVta)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BtnAgregar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnConsultar)
        Me.Controls.Add(Me.TextBoxAño)
        Me.Controls.Add(Me.ComboBoxMes)
        Me.Controls.Add(Me.btn_last)
        Me.Controls.Add(Me.btn_next)
        Me.Controls.Add(Me.btn_Previous)
        Me.Controls.Add(Me.btn_first)
        Me.Controls.Add(Me.DGObjetivos)
        Me.Name = "ScoreCardIngresarObjetivos"
        Me.Text = "Ingresar objetivos SC"
        CType(Me.DGObjetivos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TBObjetivo As System.Windows.Forms.TextBox
    Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents BtnAgregar As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnConsultar As System.Windows.Forms.Button
    Friend WithEvents TextBoxAño As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxMes As System.Windows.Forms.ComboBox
    Friend WithEvents btn_last As System.Windows.Forms.Button
    Friend WithEvents btn_next As System.Windows.Forms.Button
    Friend WithEvents btn_Previous As System.Windows.Forms.Button
    Friend WithEvents btn_first As System.Windows.Forms.Button
    Friend WithEvents DGObjetivos As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
