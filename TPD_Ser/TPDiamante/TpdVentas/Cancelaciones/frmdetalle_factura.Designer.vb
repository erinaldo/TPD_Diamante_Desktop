<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDetalleOrden
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.txtcontacto = New System.Windows.Forms.TextBox()
        Me.txtnombre = New System.Windows.Forms.TextBox()
        Me.txtcliente = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtfecha = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtfactura_cancelar = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtfecha_ven = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtfecha_doc = New System.Windows.Forms.TextBox()
        Me.dgvdetalle_f = New System.Windows.Forms.DataGridView()
        Me.ItemCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dscription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ListaP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LineTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txttotal = New System.Windows.Forms.TextBox()
        Me.txtimpuesto = New System.Windows.Forms.TextBox()
        Me.txtsubtotal = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtcom_factura = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtalmacen = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtagente = New System.Windows.Forms.TextBox()
        CType(Me.dgvdetalle_f, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtcontacto
        '
        Me.txtcontacto.Location = New System.Drawing.Point(121, 61)
        Me.txtcontacto.Name = "txtcontacto"
        Me.txtcontacto.ReadOnly = True
        Me.txtcontacto.Size = New System.Drawing.Size(178, 20)
        Me.txtcontacto.TabIndex = 32
        '
        'txtnombre
        '
        Me.txtnombre.Location = New System.Drawing.Point(121, 35)
        Me.txtnombre.Name = "txtnombre"
        Me.txtnombre.ReadOnly = True
        Me.txtnombre.Size = New System.Drawing.Size(178, 20)
        Me.txtnombre.TabIndex = 31
        '
        'txtcliente
        '
        Me.txtcliente.Location = New System.Drawing.Point(121, 9)
        Me.txtcliente.Name = "txtcliente"
        Me.txtcliente.ReadOnly = True
        Me.txtcliente.Size = New System.Drawing.Size(178, 20)
        Me.txtcliente.TabIndex = 30
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 64)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(109, 13)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "Persona de contacto:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(10, 38)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(47, 13)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "Nombre:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(10, 13)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 13)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Cliente:"
        '
        'txtfecha
        '
        Me.txtfecha.Location = New System.Drawing.Point(569, 31)
        Me.txtfecha.Name = "txtfecha"
        Me.txtfecha.ReadOnly = True
        Me.txtfecha.Size = New System.Drawing.Size(131, 20)
        Me.txtfecha.TabIndex = 35
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(452, 38)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 13)
        Me.Label12.TabIndex = 34
        Me.Label12.Text = "Fecha de factura:"
        '
        'txtfactura_cancelar
        '
        Me.txtfactura_cancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfactura_cancelar.Location = New System.Drawing.Point(569, 8)
        Me.txtfactura_cancelar.Name = "txtfactura_cancelar"
        Me.txtfactura_cancelar.ReadOnly = True
        Me.txtfactura_cancelar.Size = New System.Drawing.Size(131, 22)
        Me.txtfactura_cancelar.TabIndex = 33
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(452, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Factura:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(452, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Fecha de vencimiento:"
        '
        'txtfecha_ven
        '
        Me.txtfecha_ven.Location = New System.Drawing.Point(569, 57)
        Me.txtfecha_ven.Name = "txtfecha_ven"
        Me.txtfecha_ven.ReadOnly = True
        Me.txtfecha_ven.Size = New System.Drawing.Size(131, 20)
        Me.txtfecha_ven.TabIndex = 38
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(452, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 13)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Fecha de documento:"
        '
        'txtfecha_doc
        '
        Me.txtfecha_doc.Location = New System.Drawing.Point(569, 84)
        Me.txtfecha_doc.Name = "txtfecha_doc"
        Me.txtfecha_doc.ReadOnly = True
        Me.txtfecha_doc.Size = New System.Drawing.Size(131, 20)
        Me.txtfecha_doc.TabIndex = 40
        '
        'dgvdetalle_f
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvdetalle_f.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvdetalle_f.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvdetalle_f.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ItemCode, Me.Dscription, Me.Quantity, Me.ListaP, Me.Price, Me.LineTotal})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvdetalle_f.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvdetalle_f.Location = New System.Drawing.Point(13, 150)
        Me.dgvdetalle_f.Name = "dgvdetalle_f"
        Me.dgvdetalle_f.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvdetalle_f.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvdetalle_f.RowHeadersWidth = 20
        Me.dgvdetalle_f.Size = New System.Drawing.Size(687, 150)
        Me.dgvdetalle_f.TabIndex = 41
        '
        'ItemCode
        '
        Me.ItemCode.HeaderText = "Articulo"
        Me.ItemCode.Name = "ItemCode"
        Me.ItemCode.ReadOnly = True
        '
        'Dscription
        '
        Me.Dscription.HeaderText = "Descripción"
        Me.Dscription.Name = "Dscription"
        Me.Dscription.ReadOnly = True
        '
        'Quantity
        '
        Me.Quantity.HeaderText = "Cantidad"
        Me.Quantity.Name = "Quantity"
        Me.Quantity.ReadOnly = True
        '
        'ListaP
        '
        Me.ListaP.HeaderText = "Lista P."
        Me.ListaP.Name = "ListaP"
        Me.ListaP.ReadOnly = True
        '
        'Price
        '
        Me.Price.HeaderText = "Precio por Unidad"
        Me.Price.Name = "Price"
        Me.Price.ReadOnly = True
        '
        'LineTotal
        '
        Me.LineTotal.HeaderText = "Importe"
        Me.LineTotal.Name = "LineTotal"
        Me.LineTotal.ReadOnly = True
        '
        'txttotal
        '
        Me.txttotal.Location = New System.Drawing.Point(574, 354)
        Me.txttotal.Name = "txttotal"
        Me.txttotal.ReadOnly = True
        Me.txttotal.Size = New System.Drawing.Size(126, 20)
        Me.txttotal.TabIndex = 47
        Me.txttotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtimpuesto
        '
        Me.txtimpuesto.Location = New System.Drawing.Point(574, 332)
        Me.txtimpuesto.Name = "txtimpuesto"
        Me.txtimpuesto.ReadOnly = True
        Me.txtimpuesto.Size = New System.Drawing.Size(126, 20)
        Me.txtimpuesto.TabIndex = 46
        Me.txtimpuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtsubtotal
        '
        Me.txtsubtotal.Location = New System.Drawing.Point(574, 308)
        Me.txtsubtotal.Name = "txtsubtotal"
        Me.txtsubtotal.ReadOnly = True
        Me.txtsubtotal.Size = New System.Drawing.Size(126, 20)
        Me.txtsubtotal.TabIndex = 45
        Me.txtsubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(517, 358)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(34, 13)
        Me.Label18.TabIndex = 44
        Me.Label18.Text = "Total:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(517, 335)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(53, 13)
        Me.Label17.TabIndex = 43
        Me.Label17.Text = "Impuesto:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(517, 311)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(49, 13)
        Me.Label16.TabIndex = 42
        Me.Label16.Text = "Subtotal:"
        '
        'txtcom_factura
        '
        Me.txtcom_factura.Location = New System.Drawing.Point(121, 311)
        Me.txtcom_factura.Multiline = True
        Me.txtcom_factura.Name = "txtcom_factura"
        Me.txtcom_factura.ReadOnly = True
        Me.txtcom_factura.Size = New System.Drawing.Size(178, 63)
        Me.txtcom_factura.TabIndex = 49
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(10, 320)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 13)
        Me.Label14.TabIndex = 48
        Me.Label14.Text = "Comentarios:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "Almacén:"
        '
        'txtalmacen
        '
        Me.txtalmacen.Location = New System.Drawing.Point(121, 87)
        Me.txtalmacen.Name = "txtalmacen"
        Me.txtalmacen.ReadOnly = True
        Me.txtalmacen.Size = New System.Drawing.Size(178, 20)
        Me.txtalmacen.TabIndex = 51
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 118)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Agente:"
        '
        'txtagente
        '
        Me.txtagente.Location = New System.Drawing.Point(121, 115)
        Me.txtagente.Name = "txtagente"
        Me.txtagente.ReadOnly = True
        Me.txtagente.Size = New System.Drawing.Size(178, 20)
        Me.txtagente.TabIndex = 53
        '
        'frmdetalle_factura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(712, 391)
        Me.Controls.Add(Me.txtagente)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtalmacen)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtcom_factura)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txttotal)
        Me.Controls.Add(Me.txtimpuesto)
        Me.Controls.Add(Me.txtsubtotal)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.dgvdetalle_f)
        Me.Controls.Add(Me.txtfecha_doc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtfecha_ven)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtfecha)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtfactura_cancelar)
        Me.Controls.Add(Me.txtcontacto)
        Me.Controls.Add(Me.txtnombre)
        Me.Controls.Add(Me.txtcliente)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.MaximizeBox = False
        Me.Name = "frmdetalle_factura"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Detalle de Factura"
        CType(Me.dgvdetalle_f, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtcontacto As System.Windows.Forms.TextBox
    Friend WithEvents txtnombre As System.Windows.Forms.TextBox
    Friend WithEvents txtcliente As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtfecha As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtfactura_cancelar As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtfecha_ven As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtfecha_doc As System.Windows.Forms.TextBox
    Friend WithEvents dgvdetalle_f As System.Windows.Forms.DataGridView
    Friend WithEvents ItemCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dscription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ListaP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LineTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txttotal As System.Windows.Forms.TextBox
    Friend WithEvents txtimpuesto As System.Windows.Forms.TextBox
    Friend WithEvents txtsubtotal As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtcom_factura As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtalmacen As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtagente As System.Windows.Forms.TextBox
End Class
