<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDetalleEmpaque
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
        Me.dgvDetalleEmpaque = New System.Windows.Forms.DataGridView()
        Me.lblOrden_Entrega = New System.Windows.Forms.Label()
        Me.lblOrden_VTA = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnTerminoEmpaque = New System.Windows.Forms.Button()
        Me.txtNumCajas = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblproducto = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtcajas = New System.Windows.Forms.TextBox()
        Me.lblcantidad = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtcantidad = New System.Windows.Forms.TextBox()
        Me.btnActualizar = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblnumcajas = New System.Windows.Forms.Label()
        Me.lblContenedor = New System.Windows.Forms.Label()
        Me.dgv_Paking_Detalle = New System.Windows.Forms.DataGridView()
        Me.txtcontenedor = New System.Windows.Forms.TextBox()
        Me.btnEmergencia = New System.Windows.Forms.Button()
        CType(Me.dgvDetalleEmpaque, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv_Paking_Detalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDetalleEmpaque
        '
        Me.dgvDetalleEmpaque.AllowUserToAddRows = False
        Me.dgvDetalleEmpaque.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetalleEmpaque.Location = New System.Drawing.Point(12, 67)
        Me.dgvDetalleEmpaque.Name = "dgvDetalleEmpaque"
        Me.dgvDetalleEmpaque.RowHeadersWidth = 20
        Me.dgvDetalleEmpaque.Size = New System.Drawing.Size(714, 435)
        Me.dgvDetalleEmpaque.TabIndex = 0
        '
        'lblOrden_Entrega
        '
        Me.lblOrden_Entrega.AutoSize = True
        Me.lblOrden_Entrega.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrden_Entrega.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblOrden_Entrega.Location = New System.Drawing.Point(125, 33)
        Me.lblOrden_Entrega.Name = "lblOrden_Entrega"
        Me.lblOrden_Entrega.Size = New System.Drawing.Size(57, 17)
        Me.lblOrden_Entrega.TabIndex = 2
        Me.lblOrden_Entrega.Text = "Label2"
        '
        'lblOrden_VTA
        '
        Me.lblOrden_VTA.AutoSize = True
        Me.lblOrden_VTA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrden_VTA.Location = New System.Drawing.Point(22, 33)
        Me.lblOrden_VTA.Name = "lblOrden_VTA"
        Me.lblOrden_VTA.Size = New System.Drawing.Size(109, 15)
        Me.lblOrden_VTA.TabIndex = 1
        Me.lblOrden_VTA.Text = "Orden De Entrega:"
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(753, 350)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 23)
        Me.btnGuardar.TabIndex = 5
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnTerminoEmpaque
        '
        Me.btnTerminoEmpaque.Location = New System.Drawing.Point(917, 350)
        Me.btnTerminoEmpaque.Name = "btnTerminoEmpaque"
        Me.btnTerminoEmpaque.Size = New System.Drawing.Size(114, 23)
        Me.btnTerminoEmpaque.TabIndex = 6
        Me.btnTerminoEmpaque.Text = "Cancelar"
        Me.btnTerminoEmpaque.UseVisualStyleBackColor = True
        '
        'txtNumCajas
        '
        Me.txtNumCajas.Location = New System.Drawing.Point(337, 33)
        Me.txtNumCajas.Margin = New System.Windows.Forms.Padding(2)
        Me.txtNumCajas.MaxLength = 2
        Me.txtNumCajas.Name = "txtNumCajas"
        Me.txtNumCajas.Size = New System.Drawing.Size(65, 20)
        Me.txtNumCajas.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(238, 33)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Numero de Cajas"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "#Partidas"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Codigo"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Descripción"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Surtido"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Caja"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'lblproducto
        '
        Me.lblproducto.AutoSize = True
        Me.lblproducto.Location = New System.Drawing.Point(8, 15)
        Me.lblproducto.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblproducto.Name = "lblproducto"
        Me.lblproducto.Size = New System.Drawing.Size(39, 13)
        Me.lblproducto.TabIndex = 9
        Me.lblproducto.Text = "Label2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 98)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Caja"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 132)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Cantidad"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtcajas)
        Me.GroupBox1.Controls.Add(Me.lblcantidad)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblproducto)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtcantidad)
        Me.GroupBox1.Location = New System.Drawing.Point(731, 86)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(307, 210)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        '
        'txtcajas
        '
        Me.txtcajas.Location = New System.Drawing.Point(82, 98)
        Me.txtcajas.Margin = New System.Windows.Forms.Padding(2)
        Me.txtcajas.Name = "txtcajas"
        Me.txtcajas.Size = New System.Drawing.Size(76, 20)
        Me.txtcajas.TabIndex = 16
        '
        'lblcantidad
        '
        Me.lblcantidad.AutoSize = True
        Me.lblcantidad.Location = New System.Drawing.Point(105, 48)
        Me.lblcantidad.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblcantidad.Name = "lblcantidad"
        Me.lblcantidad.Size = New System.Drawing.Size(0, 13)
        Me.lblcantidad.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 48)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Cantidad Faltante"
        '
        'txtcantidad
        '
        Me.txtcantidad.Location = New System.Drawing.Point(82, 132)
        Me.txtcantidad.Margin = New System.Windows.Forms.Padding(2)
        Me.txtcantidad.MaxLength = 4
        Me.txtcantidad.Name = "txtcantidad"
        Me.txtcantidad.Size = New System.Drawing.Size(76, 20)
        Me.txtcantidad.TabIndex = 11
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(838, 350)
        Me.btnActualizar.Margin = New System.Windows.Forms.Padding(2)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(66, 23)
        Me.btnActualizar.TabIndex = 15
        Me.btnActualizar.Text = "Actualizar"
        Me.btnActualizar.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(729, 65)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(157, 15)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Numero de cajas Sugerido:"
        '
        'lblnumcajas
        '
        Me.lblnumcajas.AutoSize = True
        Me.lblnumcajas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnumcajas.Location = New System.Drawing.Point(874, 65)
        Me.lblnumcajas.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblnumcajas.Name = "lblnumcajas"
        Me.lblnumcajas.Size = New System.Drawing.Size(51, 15)
        Me.lblnumcajas.TabIndex = 17
        Me.lblnumcajas.Text = "Label6"
        '
        'lblContenedor
        '
        Me.lblContenedor.AutoSize = True
        Me.lblContenedor.Enabled = False
        Me.lblContenedor.Location = New System.Drawing.Point(422, 33)
        Me.lblContenedor.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblContenedor.Name = "lblContenedor"
        Me.lblContenedor.Size = New System.Drawing.Size(62, 13)
        Me.lblContenedor.TabIndex = 18
        Me.lblContenedor.Text = "Contenedor"
        Me.lblContenedor.Visible = False
        '
        'dgv_Paking_Detalle
        '
        Me.dgv_Paking_Detalle.AllowUserToAddRows = False
        Me.dgv_Paking_Detalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Paking_Detalle.Location = New System.Drawing.Point(12, 507)
        Me.dgv_Paking_Detalle.Margin = New System.Windows.Forms.Padding(2)
        Me.dgv_Paking_Detalle.Name = "dgv_Paking_Detalle"
        Me.dgv_Paking_Detalle.RowHeadersWidth = 20
        Me.dgv_Paking_Detalle.RowTemplate.Height = 24
        Me.dgv_Paking_Detalle.Size = New System.Drawing.Size(714, 158)
        Me.dgv_Paking_Detalle.TabIndex = 19
        '
        'txtcontenedor
        '
        Me.txtcontenedor.Enabled = False
        Me.txtcontenedor.Location = New System.Drawing.Point(488, 33)
        Me.txtcontenedor.Margin = New System.Windows.Forms.Padding(2)
        Me.txtcontenedor.Name = "txtcontenedor"
        Me.txtcontenedor.Size = New System.Drawing.Size(81, 20)
        Me.txtcontenedor.TabIndex = 20
        Me.txtcontenedor.Visible = False
        '
        'btnEmergencia
        '
        Me.btnEmergencia.Enabled = False
        Me.btnEmergencia.Location = New System.Drawing.Point(839, 424)
        Me.btnEmergencia.Name = "btnEmergencia"
        Me.btnEmergencia.Size = New System.Drawing.Size(86, 23)
        Me.btnEmergencia.TabIndex = 21
        Me.btnEmergencia.Text = "Imprimir"
        Me.btnEmergencia.UseVisualStyleBackColor = True
        Me.btnEmergencia.Visible = False
        '
        'frmDetalleEmpaque
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1047, 674)
        Me.Controls.Add(Me.btnEmergencia)
        Me.Controls.Add(Me.txtcontenedor)
        Me.Controls.Add(Me.dgv_Paking_Detalle)
        Me.Controls.Add(Me.lblContenedor)
        Me.Controls.Add(Me.lblnumcajas)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnActualizar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtNumCajas)
        Me.Controls.Add(Me.btnTerminoEmpaque)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.lblOrden_Entrega)
        Me.Controls.Add(Me.lblOrden_VTA)
        Me.Controls.Add(Me.dgvDetalleEmpaque)
        Me.Name = "frmDetalleEmpaque"
        Me.Text = "Creacion Paking List"
        CType(Me.dgvDetalleEmpaque, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv_Paking_Detalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvDetalleEmpaque As System.Windows.Forms.DataGridView
    Friend WithEvents lblOrden_Entrega As System.Windows.Forms.Label
    Friend WithEvents lblOrden_VTA As System.Windows.Forms.Label
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents btnTerminoEmpaque As System.Windows.Forms.Button
    Friend WithEvents txtNumCajas As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents lblproducto As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtcantidad As TextBox
    Friend WithEvents lblcantidad As Label
    Friend WithEvents btnActualizar As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents lblnumcajas As Label
    Friend WithEvents lblContenedor As Label
    Friend WithEvents dgv_Paking_Detalle As DataGridView
    Friend WithEvents txtcajas As TextBox
    Friend WithEvents txtcontenedor As TextBox
    Friend WithEvents btnEmergencia As Button
End Class
