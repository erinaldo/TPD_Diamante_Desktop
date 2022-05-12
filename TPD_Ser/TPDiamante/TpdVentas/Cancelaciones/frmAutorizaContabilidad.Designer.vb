<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutorizaContabilidad
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAutorizaContabilidad))
        Me.panel_autoriza = New System.Windows.Forms.Panel()
        Me.btnrefrescar = New System.Windows.Forms.Button()
        Me.gbdatos_factura = New System.Windows.Forms.GroupBox()
        Me.gbautorizacion = New System.Windows.Forms.GroupBox()
        Me.brnguardar = New System.Windows.Forms.Button()
        Me.cbno_procede = New System.Windows.Forms.CheckBox()
        Me.cbautorizado = New System.Windows.Forms.CheckBox()
        Me.txtobservaciones = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtstatus = New System.Windows.Forms.TextBox()
        Me.txtalmacen = New System.Windows.Forms.TextBox()
        Me.txtrefactura = New System.Windows.Forms.TextBox()
        Me.txtcomentario = New System.Windows.Forms.TextBox()
        Me.txtmotivo = New System.Windows.Forms.TextBox()
        Me.txtfecha_solicitud = New System.Windows.Forms.TextBox()
        Me.txtfecha_factura = New System.Windows.Forms.TextBox()
        Me.txtsolicita = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnmostrar = New System.Windows.Forms.Button()
        Me.txtfactura = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgvautoriza = New System.Windows.Forms.DataGridView()
        Me.Usuario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Factura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaFactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaCancela = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Motivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Comentarios = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Refactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sustituye = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Almacen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NotaCredito = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaNC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.panel_autoriza.SuspendLayout()
        Me.gbdatos_factura.SuspendLayout()
        Me.gbautorizacion.SuspendLayout()
        CType(Me.dgvautoriza, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panel_autoriza
        '
        Me.panel_autoriza.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panel_autoriza.Controls.Add(Me.btnrefrescar)
        Me.panel_autoriza.Controls.Add(Me.gbdatos_factura)
        Me.panel_autoriza.Controls.Add(Me.dgvautoriza)
        Me.panel_autoriza.Controls.Add(Me.Label1)
        Me.panel_autoriza.Location = New System.Drawing.Point(12, 12)
        Me.panel_autoriza.Name = "panel_autoriza"
        Me.panel_autoriza.Size = New System.Drawing.Size(903, 596)
        Me.panel_autoriza.TabIndex = 1
        '
        'btnrefrescar
        '
        Me.btnrefrescar.Image = Global.TPDiamante.My.Resources.Resources.Refresh_B
        Me.btnrefrescar.Location = New System.Drawing.Point(841, 4)
        Me.btnrefrescar.Name = "btnrefrescar"
        Me.btnrefrescar.Size = New System.Drawing.Size(44, 43)
        Me.btnrefrescar.TabIndex = 5
        Me.btnrefrescar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrefrescar.UseVisualStyleBackColor = True
        '
        'gbdatos_factura
        '
        Me.gbdatos_factura.Controls.Add(Me.gbautorizacion)
        Me.gbdatos_factura.Controls.Add(Me.txtstatus)
        Me.gbdatos_factura.Controls.Add(Me.txtalmacen)
        Me.gbdatos_factura.Controls.Add(Me.txtrefactura)
        Me.gbdatos_factura.Controls.Add(Me.txtcomentario)
        Me.gbdatos_factura.Controls.Add(Me.txtmotivo)
        Me.gbdatos_factura.Controls.Add(Me.txtfecha_solicitud)
        Me.gbdatos_factura.Controls.Add(Me.txtfecha_factura)
        Me.gbdatos_factura.Controls.Add(Me.txtsolicita)
        Me.gbdatos_factura.Controls.Add(Me.Label11)
        Me.gbdatos_factura.Controls.Add(Me.Label10)
        Me.gbdatos_factura.Controls.Add(Me.Label9)
        Me.gbdatos_factura.Controls.Add(Me.Label8)
        Me.gbdatos_factura.Controls.Add(Me.Label7)
        Me.gbdatos_factura.Controls.Add(Me.Label6)
        Me.gbdatos_factura.Controls.Add(Me.Label5)
        Me.gbdatos_factura.Controls.Add(Me.Label4)
        Me.gbdatos_factura.Controls.Add(Me.Label3)
        Me.gbdatos_factura.Controls.Add(Me.btnmostrar)
        Me.gbdatos_factura.Controls.Add(Me.txtfactura)
        Me.gbdatos_factura.Controls.Add(Me.Label2)
        Me.gbdatos_factura.Location = New System.Drawing.Point(16, 319)
        Me.gbdatos_factura.Name = "gbdatos_factura"
        Me.gbdatos_factura.Size = New System.Drawing.Size(869, 262)
        Me.gbdatos_factura.TabIndex = 4
        Me.gbdatos_factura.TabStop = False
        Me.gbdatos_factura.Text = "Datos de facturas para Autorización"
        '
        'gbautorizacion
        '
        Me.gbautorizacion.Controls.Add(Me.brnguardar)
        Me.gbautorizacion.Controls.Add(Me.cbno_procede)
        Me.gbautorizacion.Controls.Add(Me.cbautorizado)
        Me.gbautorizacion.Controls.Add(Me.txtobservaciones)
        Me.gbautorizacion.Controls.Add(Me.Label12)
        Me.gbautorizacion.Enabled = False
        Me.gbautorizacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbautorizacion.Location = New System.Drawing.Point(595, 29)
        Me.gbautorizacion.Name = "gbautorizacion"
        Me.gbautorizacion.Size = New System.Drawing.Size(256, 216)
        Me.gbautorizacion.TabIndex = 22
        Me.gbautorizacion.TabStop = False
        Me.gbautorizacion.Text = "Autorización"
        '
        'brnguardar
        '
        Me.brnguardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.brnguardar.Image = Global.TPDiamante.My.Resources.Resources.kate
        Me.brnguardar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.brnguardar.Location = New System.Drawing.Point(81, 171)
        Me.brnguardar.Name = "brnguardar"
        Me.brnguardar.Size = New System.Drawing.Size(90, 38)
        Me.brnguardar.TabIndex = 4
        Me.brnguardar.Text = "Guardar"
        Me.brnguardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.brnguardar.UseVisualStyleBackColor = True
        '
        'cbno_procede
        '
        Me.cbno_procede.AutoSize = True
        Me.cbno_procede.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbno_procede.Location = New System.Drawing.Point(131, 32)
        Me.cbno_procede.Name = "cbno_procede"
        Me.cbno_procede.Size = New System.Drawing.Size(93, 17)
        Me.cbno_procede.TabIndex = 2
        Me.cbno_procede.Text = "No Autorizado"
        Me.cbno_procede.UseVisualStyleBackColor = True
        '
        'cbautorizado
        '
        Me.cbautorizado.AutoSize = True
        Me.cbautorizado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbautorizado.Location = New System.Drawing.Point(38, 32)
        Me.cbautorizado.Name = "cbautorizado"
        Me.cbautorizado.Size = New System.Drawing.Size(76, 17)
        Me.cbautorizado.TabIndex = 1
        Me.cbautorizado.Text = "Autorizado"
        Me.cbautorizado.UseVisualStyleBackColor = True
        '
        'txtobservaciones
        '
        Me.txtobservaciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtobservaciones.Location = New System.Drawing.Point(9, 83)
        Me.txtobservaciones.MaxLength = 100
        Me.txtobservaciones.Multiline = True
        Me.txtobservaciones.Name = "txtobservaciones"
        Me.txtobservaciones.Size = New System.Drawing.Size(241, 73)
        Me.txtobservaciones.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(6, 67)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(78, 13)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Observaciones"
        '
        'txtstatus
        '
        Me.txtstatus.Enabled = False
        Me.txtstatus.Location = New System.Drawing.Point(424, 138)
        Me.txtstatus.Multiline = True
        Me.txtstatus.Name = "txtstatus"
        Me.txtstatus.Size = New System.Drawing.Size(157, 44)
        Me.txtstatus.TabIndex = 21
        '
        'txtalmacen
        '
        Me.txtalmacen.Enabled = False
        Me.txtalmacen.Location = New System.Drawing.Point(424, 111)
        Me.txtalmacen.Name = "txtalmacen"
        Me.txtalmacen.Size = New System.Drawing.Size(157, 20)
        Me.txtalmacen.TabIndex = 20
        '
        'txtrefactura
        '
        Me.txtrefactura.Enabled = False
        Me.txtrefactura.Location = New System.Drawing.Point(424, 82)
        Me.txtrefactura.Name = "txtrefactura"
        Me.txtrefactura.Size = New System.Drawing.Size(157, 20)
        Me.txtrefactura.TabIndex = 19
        '
        'txtcomentario
        '
        Me.txtcomentario.Enabled = False
        Me.txtcomentario.Location = New System.Drawing.Point(118, 194)
        Me.txtcomentario.Multiline = True
        Me.txtcomentario.Name = "txtcomentario"
        Me.txtcomentario.Size = New System.Drawing.Size(240, 51)
        Me.txtcomentario.TabIndex = 18
        '
        'txtmotivo
        '
        Me.txtmotivo.Enabled = False
        Me.txtmotivo.Location = New System.Drawing.Point(118, 166)
        Me.txtmotivo.Name = "txtmotivo"
        Me.txtmotivo.Size = New System.Drawing.Size(240, 20)
        Me.txtmotivo.TabIndex = 17
        '
        'txtfecha_solicitud
        '
        Me.txtfecha_solicitud.Enabled = False
        Me.txtfecha_solicitud.Location = New System.Drawing.Point(118, 138)
        Me.txtfecha_solicitud.Name = "txtfecha_solicitud"
        Me.txtfecha_solicitud.Size = New System.Drawing.Size(240, 20)
        Me.txtfecha_solicitud.TabIndex = 16
        '
        'txtfecha_factura
        '
        Me.txtfecha_factura.Enabled = False
        Me.txtfecha_factura.Location = New System.Drawing.Point(118, 108)
        Me.txtfecha_factura.Name = "txtfecha_factura"
        Me.txtfecha_factura.Size = New System.Drawing.Size(240, 20)
        Me.txtfecha_factura.TabIndex = 15
        '
        'txtsolicita
        '
        Me.txtsolicita.Enabled = False
        Me.txtsolicita.Location = New System.Drawing.Point(118, 82)
        Me.txtsolicita.Name = "txtsolicita"
        Me.txtsolicita.Size = New System.Drawing.Size(240, 20)
        Me.txtsolicita.TabIndex = 14
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(370, 141)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "Estatus:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(370, 114)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(51, 13)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "Almacén:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(370, 89)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Refactura:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 195)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Comentario:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 169)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Motivo:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 141)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Fecha de Solicitud:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Fecha factura:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Solicita:"
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label3.Location = New System.Drawing.Point(12, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(249, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "________________________________________"
        '
        'btnmostrar
        '
        Me.btnmostrar.BackgroundImage = Global.TPDiamante.My.Resources.Resources.file_find
        Me.btnmostrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnmostrar.Location = New System.Drawing.Point(189, 35)
        Me.btnmostrar.Name = "btnmostrar"
        Me.btnmostrar.Size = New System.Drawing.Size(29, 27)
        Me.btnmostrar.TabIndex = 4
        Me.btnmostrar.UseVisualStyleBackColor = True
        '
        'txtfactura
        '
        Me.txtfactura.Enabled = False
        Me.txtfactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfactura.Location = New System.Drawing.Point(64, 38)
        Me.txtfactura.Name = "txtfactura"
        Me.txtfactura.Size = New System.Drawing.Size(119, 20)
        Me.txtfactura.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Factura:"
        '
        'dgvautoriza
        '
        Me.dgvautoriza.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvautoriza.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Usuario, Me.Factura, Me.FechaFactura, Me.FechaCancela, Me.Motivo, Me.Comentarios, Me.Refactura, Me.Sustituye, Me.Almacen, Me.NotaCredito, Me.FechaNC, Me.Status})
        Me.dgvautoriza.Location = New System.Drawing.Point(16, 52)
        Me.dgvautoriza.Name = "dgvautoriza"
        Me.dgvautoriza.ReadOnly = True
        Me.dgvautoriza.RowHeadersWidth = 20
        Me.dgvautoriza.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvautoriza.Size = New System.Drawing.Size(869, 244)
        Me.dgvautoriza.TabIndex = 1
        '
        'Usuario
        '
        Me.Usuario.Frozen = True
        Me.Usuario.HeaderText = "Usuario"
        Me.Usuario.Name = "Usuario"
        Me.Usuario.ReadOnly = True
        '
        'Factura
        '
        Me.Factura.Frozen = True
        Me.Factura.HeaderText = "Factura"
        Me.Factura.Name = "Factura"
        Me.Factura.ReadOnly = True
        '
        'FechaFactura
        '
        Me.FechaFactura.Frozen = True
        Me.FechaFactura.HeaderText = "Fecha de factura"
        Me.FechaFactura.Name = "FechaFactura"
        Me.FechaFactura.ReadOnly = True
        '
        'FechaCancela
        '
        Me.FechaCancela.HeaderText = "Fecha de Solicitud"
        Me.FechaCancela.Name = "FechaCancela"
        Me.FechaCancela.ReadOnly = True
        '
        'Motivo
        '
        Me.Motivo.HeaderText = "Motivo"
        Me.Motivo.Name = "Motivo"
        Me.Motivo.ReadOnly = True
        '
        'Comentarios
        '
        Me.Comentarios.HeaderText = "Comentario"
        Me.Comentarios.Name = "Comentarios"
        Me.Comentarios.ReadOnly = True
        '
        'Refactura
        '
        Me.Refactura.HeaderText = "Requiere refacturación"
        Me.Refactura.Name = "Refactura"
        Me.Refactura.ReadOnly = True
        '
        'Sustituye
        '
        Me.Sustituye.HeaderText = "Sustituye"
        Me.Sustituye.Name = "Sustituye"
        Me.Sustituye.ReadOnly = True
        '
        'Almacen
        '
        Me.Almacen.HeaderText = "Almacén"
        Me.Almacen.Name = "Almacen"
        Me.Almacen.ReadOnly = True
        '
        'NotaCredito
        '
        Me.NotaCredito.HeaderText = "Nota de Credito"
        Me.NotaCredito.Name = "NotaCredito"
        Me.NotaCredito.ReadOnly = True
        '
        'FechaNC
        '
        Me.FechaNC.HeaderText = "Fecha de NC"
        Me.FechaNC.Name = "FechaNC"
        Me.FechaNC.ReadOnly = True
        '
        'Status
        '
        Me.Status.HeaderText = "Estatus"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(287, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Favor de seleccionar la factura requerida para autorización."
        '
        'frmAutorizaContabilidad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(927, 620)
        Me.Controls.Add(Me.panel_autoriza)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAutorizaContabilidad"
        Me.Text = "Autorización de cancelación contable"
        Me.panel_autoriza.ResumeLayout(False)
        Me.panel_autoriza.PerformLayout()
        Me.gbdatos_factura.ResumeLayout(False)
        Me.gbdatos_factura.PerformLayout()
        Me.gbautorizacion.ResumeLayout(False)
        Me.gbautorizacion.PerformLayout()
        CType(Me.dgvautoriza, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panel_autoriza As Panel
    Friend WithEvents btnrefrescar As Button
    Friend WithEvents gbdatos_factura As GroupBox
    Friend WithEvents gbautorizacion As GroupBox
    Friend WithEvents brnguardar As Button
    Friend WithEvents cbno_procede As CheckBox
    Friend WithEvents cbautorizado As CheckBox
    Friend WithEvents txtobservaciones As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtstatus As TextBox
    Friend WithEvents txtalmacen As TextBox
    Friend WithEvents txtrefactura As TextBox
    Friend WithEvents txtcomentario As TextBox
    Friend WithEvents txtmotivo As TextBox
    Friend WithEvents txtfecha_solicitud As TextBox
    Friend WithEvents txtfecha_factura As TextBox
    Friend WithEvents txtsolicita As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnmostrar As Button
    Friend WithEvents txtfactura As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents dgvautoriza As DataGridView
    Friend WithEvents Usuario As DataGridViewTextBoxColumn
    Friend WithEvents Factura As DataGridViewTextBoxColumn
    Friend WithEvents FechaFactura As DataGridViewTextBoxColumn
    Friend WithEvents FechaCancela As DataGridViewTextBoxColumn
    Friend WithEvents Motivo As DataGridViewTextBoxColumn
    Friend WithEvents Comentarios As DataGridViewTextBoxColumn
    Friend WithEvents Refactura As DataGridViewTextBoxColumn
    Friend WithEvents Sustituye As DataGridViewTextBoxColumn
    Friend WithEvents Almacen As DataGridViewTextBoxColumn
    Friend WithEvents NotaCredito As DataGridViewTextBoxColumn
    Friend WithEvents FechaNC As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
End Class
