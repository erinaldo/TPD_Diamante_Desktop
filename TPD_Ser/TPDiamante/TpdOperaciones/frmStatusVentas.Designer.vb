<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStatusVentas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStatusVentas))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvDetallePed = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpfecha_ini = New System.Windows.Forms.DateTimePicker()
        Me.dtpfecha_fin = New System.Windows.Forms.DateTimePicker()
        Me.cmbEstado = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnConsultar = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbAlmacen = New System.Windows.Forms.ComboBox()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.User = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DocEntry = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DraftEntry = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TaxDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateHour = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CardCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CardName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WhsCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WhsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DocTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Days = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Autoriza = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaAutoriza = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HoraAutoriza = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Comment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvDetallePed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 162)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(143, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Detalle de Pedido de cliente."
        '
        'dgvDetallePed
        '
        Me.dgvDetallePed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetallePed.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.User, Me.Status, Me.DocEntry, Me.DraftEntry, Me.TaxDate, Me.DateHour, Me.CardCode, Me.CardName, Me.WhsCode, Me.WhsName, Me.DocTotal, Me.Days, Me.Autoriza, Me.FechaAutoriza, Me.HoraAutoriza, Me.Comment})
        Me.dgvDetallePed.Location = New System.Drawing.Point(12, 180)
        Me.dgvDetallePed.Name = "dgvDetallePed"
        Me.dgvDetallePed.RowHeadersWidth = 20
        Me.dgvDetallePed.Size = New System.Drawing.Size(1077, 298)
        Me.dgvDetallePed.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Fecha inicio:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Fecha final:"
        '
        'dtpfecha_ini
        '
        Me.dtpfecha_ini.Location = New System.Drawing.Point(82, 24)
        Me.dtpfecha_ini.Name = "dtpfecha_ini"
        Me.dtpfecha_ini.Size = New System.Drawing.Size(217, 20)
        Me.dtpfecha_ini.TabIndex = 4
        '
        'dtpfecha_fin
        '
        Me.dtpfecha_fin.Location = New System.Drawing.Point(82, 54)
        Me.dtpfecha_fin.Name = "dtpfecha_fin"
        Me.dtpfecha_fin.Size = New System.Drawing.Size(217, 20)
        Me.dtpfecha_fin.TabIndex = 5
        '
        'cmbEstado
        '
        Me.cmbEstado.FormattingEnabled = True
        Me.cmbEstado.Location = New System.Drawing.Point(82, 85)
        Me.cmbEstado.Name = "cmbEstado"
        Me.cmbEstado.Size = New System.Drawing.Size(217, 21)
        Me.cmbEstado.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Estado:"
        '
        'btnConsultar
        '
        Me.btnConsultar.Image = Global.TPDiamante.My.Resources.Resources.file_find
        Me.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConsultar.Location = New System.Drawing.Point(349, 25)
        Me.btnConsultar.Name = "btnConsultar"
        Me.btnConsultar.Size = New System.Drawing.Size(94, 38)
        Me.btnConsultar.TabIndex = 8
        Me.btnConsultar.Text = "Consultar"
        Me.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnConsultar.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 119)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Almacén:"
        '
        'cmbAlmacen
        '
        Me.cmbAlmacen.FormattingEnabled = True
        Me.cmbAlmacen.Location = New System.Drawing.Point(82, 116)
        Me.cmbAlmacen.Name = "cmbAlmacen"
        Me.cmbAlmacen.Size = New System.Drawing.Size(217, 21)
        Me.cmbAlmacen.TabIndex = 10
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Usuario"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Estado"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Doc. Preliminar"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Fecha Borrador"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Hora Borrador"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Cliente"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Nombre"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Almacén"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Total borrador"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Dias trans."
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Autoriza"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "Fecha autorización"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "Hora de autorización"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.HeaderText = "Comentarios cobranza"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.HeaderText = "Comentarios cobranza"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.HeaderText = "Comentarios cobranza"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        '
        'User
        '
        Me.User.Frozen = True
        Me.User.HeaderText = "Usuario"
        Me.User.Name = "User"
        Me.User.ReadOnly = True
        '
        'Status
        '
        Me.Status.Frozen = True
        Me.Status.HeaderText = "Estado"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        '
        'DocEntry
        '
        Me.DocEntry.Frozen = True
        Me.DocEntry.HeaderText = "Orden de Venta"
        Me.DocEntry.Name = "DocEntry"
        Me.DocEntry.ReadOnly = True
        '
        'DraftEntry
        '
        Me.DraftEntry.Frozen = True
        Me.DraftEntry.HeaderText = "Doc. Preliminar"
        Me.DraftEntry.Name = "DraftEntry"
        Me.DraftEntry.ReadOnly = True
        '
        'TaxDate
        '
        Me.TaxDate.HeaderText = "Fecha Borrador"
        Me.TaxDate.Name = "TaxDate"
        Me.TaxDate.ReadOnly = True
        '
        'DateHour
        '
        Me.DateHour.HeaderText = "Hora Borrador"
        Me.DateHour.Name = "DateHour"
        Me.DateHour.ReadOnly = True
        '
        'CardCode
        '
        Me.CardCode.HeaderText = "Cliente"
        Me.CardCode.Name = "CardCode"
        Me.CardCode.ReadOnly = True
        '
        'CardName
        '
        Me.CardName.HeaderText = "Nombre"
        Me.CardName.Name = "CardName"
        Me.CardName.ReadOnly = True
        '
        'WhsCode
        '
        Me.WhsCode.HeaderText = "Cod. Almacen"
        Me.WhsCode.Name = "WhsCode"
        Me.WhsCode.ReadOnly = True
        Me.WhsCode.Visible = False
        '
        'WhsName
        '
        Me.WhsName.HeaderText = "Almacén"
        Me.WhsName.Name = "WhsName"
        Me.WhsName.ReadOnly = True
        '
        'DocTotal
        '
        Me.DocTotal.HeaderText = "Total borrador"
        Me.DocTotal.Name = "DocTotal"
        Me.DocTotal.ReadOnly = True
        '
        'Days
        '
        Me.Days.HeaderText = "Dias trans."
        Me.Days.Name = "Days"
        Me.Days.ReadOnly = True
        '
        'Autoriza
        '
        Me.Autoriza.HeaderText = "Autoriza"
        Me.Autoriza.Name = "Autoriza"
        Me.Autoriza.ReadOnly = True
        '
        'FechaAutoriza
        '
        Me.FechaAutoriza.HeaderText = "Fecha autorización"
        Me.FechaAutoriza.Name = "FechaAutoriza"
        Me.FechaAutoriza.ReadOnly = True
        '
        'HoraAutoriza
        '
        Me.HoraAutoriza.HeaderText = "Hora autorización"
        Me.HoraAutoriza.Name = "HoraAutoriza"
        Me.HoraAutoriza.ReadOnly = True
        '
        'Comment
        '
        Me.Comment.HeaderText = "Comentarios cobranza"
        Me.Comment.Name = "Comment"
        Me.Comment.ReadOnly = True
        '
        'frmStatusVentas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1101, 487)
        Me.Controls.Add(Me.cmbAlmacen)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnConsultar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbEstado)
        Me.Controls.Add(Me.dtpfecha_fin)
        Me.Controls.Add(Me.dtpfecha_ini)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dgvDetallePed)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmStatusVentas"
        Me.Text = "Informe de Status de Autorización"
        CType(Me.dgvDetallePed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents dgvDetallePed As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpfecha_ini As DateTimePicker
    Friend WithEvents dtpfecha_fin As DateTimePicker
    Friend WithEvents cmbEstado As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As DataGridViewTextBoxColumn
    Friend WithEvents btnConsultar As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbAlmacen As ComboBox
    Friend WithEvents DataGridViewTextBoxColumn15 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As DataGridViewTextBoxColumn
    Friend WithEvents User As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents DocEntry As DataGridViewTextBoxColumn
    Friend WithEvents DraftEntry As DataGridViewTextBoxColumn
    Friend WithEvents TaxDate As DataGridViewTextBoxColumn
    Friend WithEvents DateHour As DataGridViewTextBoxColumn
    Friend WithEvents CardCode As DataGridViewTextBoxColumn
    Friend WithEvents CardName As DataGridViewTextBoxColumn
    Friend WithEvents WhsCode As DataGridViewTextBoxColumn
    Friend WithEvents WhsName As DataGridViewTextBoxColumn
    Friend WithEvents DocTotal As DataGridViewTextBoxColumn
    Friend WithEvents Days As DataGridViewTextBoxColumn
    Friend WithEvents Autoriza As DataGridViewTextBoxColumn
    Friend WithEvents FechaAutoriza As DataGridViewTextBoxColumn
    Friend WithEvents HoraAutoriza As DataGridViewTextBoxColumn
    Friend WithEvents Comment As DataGridViewTextBoxColumn
End Class
