<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class cobranzagral
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
        Me.CmbAgteVta = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbListaPrecio = New System.Windows.Forms.ComboBox()
        Me.BtnImprimir = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GrdConProd = New System.Windows.Forms.DataGridView()
        Me.CachedCrListaPrecio31 = New TPDiamante.CachedCrListaPrecio3()
        Me.CachedCrListaPrecio32 = New TPDiamante.CachedCrListaPrecio3()
        Me.CmbAgteCob = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CachedCrListaPrecio33 = New TPDiamante.CachedCrListaPrecio3()
        Me.CmbCliente = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CachedCrListaPrecio34 = New TPDiamante.CachedCrListaPrecio3()
        Me.CachedCrListaPrecio35 = New TPDiamante.CachedCrListaPrecio3()
        Me.CachedCrListaPrecio36 = New TPDiamante.CachedCrListaPrecio3()
        Me.CBRuta = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmbAgteVta
        '
        Me.CmbAgteVta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbAgteVta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbAgteVta.FormattingEnabled = True
        Me.CmbAgteVta.Location = New System.Drawing.Point(178, 61)
        Me.CmbAgteVta.Name = "CmbAgteVta"
        Me.CmbAgteVta.Size = New System.Drawing.Size(364, 21)
        Me.CmbAgteVta.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Agente de ventas"
        '
        'CmbListaPrecio
        '
        Me.CmbListaPrecio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbListaPrecio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbListaPrecio.FormattingEnabled = True
        Me.CmbListaPrecio.Location = New System.Drawing.Point(431, 199)
        Me.CmbListaPrecio.Name = "CmbListaPrecio"
        Me.CmbListaPrecio.Size = New System.Drawing.Size(259, 21)
        Me.CmbListaPrecio.TabIndex = 9
        Me.CmbListaPrecio.Visible = False
        '
        'BtnImprimir
        '
        Me.BtnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprimir.ForeColor = System.Drawing.Color.MediumBlue
        Me.BtnImprimir.Image = Global.TPDiamante.My.Resources.Resources.printer
        Me.BtnImprimir.Location = New System.Drawing.Point(627, 50)
        Me.BtnImprimir.Name = "BtnImprimir"
        Me.BtnImprimir.Size = New System.Drawing.Size(43, 39)
        Me.BtnImprimir.TabIndex = 8
        Me.BtnImprimir.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(326, 200)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 17)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Lista de Precio"
        Me.Label5.Visible = False
        '
        'GrdConProd
        '
        Me.GrdConProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrdConProd.Location = New System.Drawing.Point(25, 187)
        Me.GrdConProd.Name = "GrdConProd"
        Me.GrdConProd.Size = New System.Drawing.Size(699, 218)
        Me.GrdConProd.TabIndex = 7
        Me.GrdConProd.Visible = False
        '
        'CmbAgteCob
        '
        Me.CmbAgteCob.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbAgteCob.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbAgteCob.FormattingEnabled = True
        Me.CmbAgteCob.Location = New System.Drawing.Point(178, 17)
        Me.CmbAgteCob.Name = "CmbAgteCob"
        Me.CmbAgteCob.Size = New System.Drawing.Size(364, 21)
        Me.CmbAgteCob.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(22, 21)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(138, 17)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Agente de Cobranza"
        '
        'CmbCliente
        '
        Me.CmbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbCliente.FormattingEnabled = True
        Me.CmbCliente.Location = New System.Drawing.Point(178, 142)
        Me.CmbCliente.Name = "CmbCliente"
        Me.CmbCliente.Size = New System.Drawing.Size(364, 21)
        Me.CmbCliente.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 146)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Cliente"
        '
        'CBRuta
        '
        Me.CBRuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBRuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBRuta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBRuta.FormattingEnabled = True
        Me.CBRuta.Location = New System.Drawing.Point(178, 103)
        Me.CBRuta.Name = "CBRuta"
        Me.CBRuta.Size = New System.Drawing.Size(364, 21)
        Me.CBRuta.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(22, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Ruta"
        '
        'cobranzagral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 187)
        Me.Controls.Add(Me.CBRuta)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CmbCliente)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CmbAgteCob)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.GrdConProd)
        Me.Controls.Add(Me.CmbAgteVta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CmbListaPrecio)
        Me.Controls.Add(Me.BtnImprimir)
        Me.Controls.Add(Me.Label5)
        Me.Name = "cobranzagral"
        Me.Text = "Cobranza Clientes"
        CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CmbAgteVta As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbListaPrecio As System.Windows.Forms.ComboBox
    Friend WithEvents BtnImprimir As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GrdConProd As System.Windows.Forms.DataGridView
    Friend WithEvents CachedCrListaPrecio31 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CachedCrListaPrecio32 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CmbAgteCob As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CachedCrListaPrecio33 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CachedCrListaPrecio34 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CachedCrListaPrecio35 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CachedCrListaPrecio36 As TPDiamante.CachedCrListaPrecio3
    Friend WithEvents CBRuta As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
