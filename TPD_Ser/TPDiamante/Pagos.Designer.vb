<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pagos
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pagos))
    Me.GrdConProd = New System.Windows.Forms.DataGridView()
    Me.BtnImprimir = New System.Windows.Forms.Button()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.CmbListaPrecio = New System.Windows.Forms.ComboBox()
    Me.CmbGrupoArticulo = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lblListaEspecifica = New System.Windows.Forms.Label()
    CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GrdConProd
    '
    Me.GrdConProd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.GrdConProd.Location = New System.Drawing.Point(12, 104)
    Me.GrdConProd.Name = "GrdConProd"
    Me.GrdConProd.Size = New System.Drawing.Size(948, 583)
    Me.GrdConProd.TabIndex = 68
    Me.GrdConProd.Visible = False
    '
    'BtnImprimir
    '
    Me.BtnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.BtnImprimir.ForeColor = System.Drawing.Color.MediumBlue
    Me.BtnImprimir.Image = Global.TPDiamante.My.Resources.Resources.printer
    Me.BtnImprimir.Location = New System.Drawing.Point(389, 25)
    Me.BtnImprimir.Name = "BtnImprimir"
    Me.BtnImprimir.Size = New System.Drawing.Size(43, 39)
    Me.BtnImprimir.TabIndex = 2
    Me.BtnImprimir.UseVisualStyleBackColor = True
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label5.Location = New System.Drawing.Point(49, 15)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(102, 17)
    Me.Label5.TabIndex = 66
    Me.Label5.Text = "Lista de Precio"
    '
    'CmbListaPrecio
    '
    Me.CmbListaPrecio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbListaPrecio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbListaPrecio.FormattingEnabled = True
    Me.CmbListaPrecio.Location = New System.Drawing.Point(154, 14)
    Me.CmbListaPrecio.Name = "CmbListaPrecio"
    Me.CmbListaPrecio.Size = New System.Drawing.Size(195, 21)
    Me.CmbListaPrecio.TabIndex = 0
    '
    'CmbGrupoArticulo
    '
    Me.CmbGrupoArticulo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
    Me.CmbGrupoArticulo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
    Me.CmbGrupoArticulo.FormattingEnabled = True
    Me.CmbGrupoArticulo.Location = New System.Drawing.Point(154, 55)
    Me.CmbGrupoArticulo.Name = "CmbGrupoArticulo"
    Me.CmbGrupoArticulo.Size = New System.Drawing.Size(195, 21)
    Me.CmbGrupoArticulo.TabIndex = 1
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(49, 56)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(99, 17)
    Me.Label1.TabIndex = 72
    Me.Label1.Text = "Grupo Artículo"
    '
    'lblListaEspecifica
    '
    Me.lblListaEspecifica.AutoSize = True
    Me.lblListaEspecifica.Location = New System.Drawing.Point(304, 85)
    Me.lblListaEspecifica.Name = "lblListaEspecifica"
    Me.lblListaEspecifica.Size = New System.Drawing.Size(81, 13)
    Me.lblListaEspecifica.TabIndex = 73
    Me.lblListaEspecifica.Text = "Lista Especifica"
    Me.lblListaEspecifica.Visible = False
    '
    'Pagos
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(505, 170)
    Me.Controls.Add(Me.lblListaEspecifica)
    Me.Controls.Add(Me.CmbGrupoArticulo)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.CmbListaPrecio)
    Me.Controls.Add(Me.GrdConProd)
    Me.Controls.Add(Me.BtnImprimir)
    Me.Controls.Add(Me.Label5)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "Pagos"
    Me.Text = "Lista de Precios"
    CType(Me.GrdConProd, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents GrdConProd As System.Windows.Forms.DataGridView
    Friend WithEvents BtnImprimir As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CmbListaPrecio As System.Windows.Forms.ComboBox
    Friend WithEvents CmbGrupoArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents lblListaEspecifica As Label
End Class
