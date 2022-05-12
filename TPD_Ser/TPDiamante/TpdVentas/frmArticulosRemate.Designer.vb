<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmArticulosRemate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmArticulosRemate))
        Me.gbremate = New System.Windows.Forms.GroupBox()
        Me.dgvarticulos = New System.Windows.Forms.DataGridView()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnexportar = New System.Windows.Forms.Button()
        Me.btnconsultar = New System.Windows.Forms.Button()
        Me.cbventa = New System.Windows.Forms.CheckBox()
        Me.cmblinea = New System.Windows.Forms.ComboBox()
        Me.cmbalmacen = New System.Windows.Forms.ComboBox()
        Me.dtpff = New System.Windows.Forms.DateTimePicker()
        Me.dtpfi = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Articulo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Linea = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.st = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.stt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.L01 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TL01 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PzaVtaPue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpVtaPue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PzaDevPue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpDevPue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PzaVtaMer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpVtaMer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PzaDevMer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpDevMer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PzaVtaTux = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpVtaTux = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PzaDevTux = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpDevTux = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PzasTot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbremate.SuspendLayout()
        CType(Me.dgvarticulos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbremate
        '
        Me.gbremate.Controls.Add(Me.dgvarticulos)
        Me.gbremate.Controls.Add(Me.Label6)
        Me.gbremate.Controls.Add(Me.btnexportar)
        Me.gbremate.Controls.Add(Me.btnconsultar)
        Me.gbremate.Controls.Add(Me.cbventa)
        Me.gbremate.Controls.Add(Me.cmblinea)
        Me.gbremate.Controls.Add(Me.cmbalmacen)
        Me.gbremate.Controls.Add(Me.dtpff)
        Me.gbremate.Controls.Add(Me.dtpfi)
        Me.gbremate.Controls.Add(Me.Label5)
        Me.gbremate.Controls.Add(Me.Label4)
        Me.gbremate.Controls.Add(Me.Label3)
        Me.gbremate.Controls.Add(Me.Label2)
        Me.gbremate.Controls.Add(Me.Label1)
        Me.gbremate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbremate.Location = New System.Drawing.Point(12, 4)
        Me.gbremate.Name = "gbremate"
        Me.gbremate.Size = New System.Drawing.Size(1260, 625)
        Me.gbremate.TabIndex = 0
        Me.gbremate.TabStop = False
        '
        'dgvarticulos
        '
        Me.dgvarticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvarticulos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Articulo, Me.Descripcion, Me.Linea, Me.sp, Me.sm, Me.st, Me.stt, Me.L01, Me.TL01, Me.PzaVtaPue, Me.ImpVtaPue, Me.PzaDevPue, Me.ImpDevPue, Me.PzaVtaMer, Me.ImpVtaMer, Me.PzaDevMer, Me.ImpDevMer, Me.PzaVtaTux, Me.ImpVtaTux, Me.PzaDevTux, Me.ImpDevTux, Me.PzasTot, Me.Total})
        Me.dgvarticulos.Location = New System.Drawing.Point(9, 147)
        Me.dgvarticulos.Name = "dgvarticulos"
        Me.dgvarticulos.RowHeadersWidth = 20
        Me.dgvarticulos.Size = New System.Drawing.Size(1245, 472)
        Me.dgvarticulos.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label6.Location = New System.Drawing.Point(260, 110)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(728, 18)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "_________________________________________________________________________________" & _
    "_________"
        '
        'btnexportar
        '
        Me.btnexportar.BackgroundImage = Global.TPDiamante.My.Resources.Resources.Excel2016
        Me.btnexportar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnexportar.Location = New System.Drawing.Point(1151, 51)
        Me.btnexportar.Name = "btnexportar"
        Me.btnexportar.Size = New System.Drawing.Size(70, 39)
        Me.btnexportar.TabIndex = 11
        Me.btnexportar.UseVisualStyleBackColor = True
        '
        'btnconsultar
        '
        Me.btnconsultar.Location = New System.Drawing.Point(1065, 51)
        Me.btnconsultar.Name = "btnconsultar"
        Me.btnconsultar.Size = New System.Drawing.Size(70, 39)
        Me.btnconsultar.TabIndex = 10
        Me.btnconsultar.Text = "Consultar"
        Me.btnconsultar.UseVisualStyleBackColor = True
        '
        'cbventa
        '
        Me.cbventa.AutoSize = True
        Me.cbventa.Location = New System.Drawing.Point(721, 56)
        Me.cbventa.Name = "cbventa"
        Me.cbventa.Size = New System.Drawing.Size(136, 19)
        Me.cbventa.TabIndex = 9
        Me.cbventa.Text = "Articulos con ventas."
        Me.cbventa.UseVisualStyleBackColor = True
        '
        'cmblinea
        '
        Me.cmblinea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmblinea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cmblinea.FormattingEnabled = True
        Me.cmblinea.Location = New System.Drawing.Point(485, 83)
        Me.cmblinea.Name = "cmblinea"
        Me.cmblinea.Size = New System.Drawing.Size(203, 23)
        Me.cmblinea.TabIndex = 8
        '
        'cmbalmacen
        '
        Me.cmbalmacen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbalmacen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cmbalmacen.FormattingEnabled = True
        Me.cmbalmacen.Location = New System.Drawing.Point(485, 56)
        Me.cmbalmacen.Name = "cmbalmacen"
        Me.cmbalmacen.Size = New System.Drawing.Size(203, 23)
        Me.cmbalmacen.TabIndex = 7
        '
        'dtpff
        '
        Me.dtpff.Location = New System.Drawing.Point(105, 81)
        Me.dtpff.Name = "dtpff"
        Me.dtpff.Size = New System.Drawing.Size(260, 21)
        Me.dtpff.TabIndex = 6
        '
        'dtpfi
        '
        Me.dtpfi.Location = New System.Drawing.Point(105, 54)
        Me.dtpfi.Name = "dtpfi"
        Me.dtpfi.Size = New System.Drawing.Size(260, 21)
        Me.dtpfi.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(414, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 15)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Linea:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(414, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 15)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Almacén:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Fecha final:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Fecha de inicio:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(482, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(256, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Reporte de ventas Articulos de Remate"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Articulo"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Descripción"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Linea"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Stock Puebla"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Stock Merida"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Stock Tuxtla"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Stock Total"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Lista 01"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Total L01"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Pza. Vta. Puebla"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Imp. Vta. Puebla"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "Pza. Dev. Puebla"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "Imp. Dev. Puebla"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.HeaderText = "Pza. Vta. Mérida"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.HeaderText = "Imp. Vta. Mérida"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.HeaderText = "Pza. Dev. Mérida"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.HeaderText = "Imp. Dev. Mérida"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.HeaderText = "Pza. Vta. Tuxtla"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.HeaderText = "Imp. Vta. Tuxtla"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.HeaderText = "Pza. Dev. Tuxtla"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.ReadOnly = True
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.HeaderText = "Imp. Dev. Tuxtla"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.ReadOnly = True
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.HeaderText = "Total Pzas. Vtas."
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.ReadOnly = True
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.HeaderText = "Imp. Total Vtas."
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.ReadOnly = True
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.HeaderText = "Total Pzas. Devs."
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.ReadOnly = True
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.HeaderText = "Imp. Total Devs."
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.ReadOnly = True
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.HeaderText = "Pzas. Totales"
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.ReadOnly = True
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.HeaderText = "Imp. Total"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.ReadOnly = True
        '
        'Articulo
        '
        Me.Articulo.Frozen = True
        Me.Articulo.HeaderText = "Articulo"
        Me.Articulo.Name = "Articulo"
        Me.Articulo.ReadOnly = True
        '
        'Descripcion
        '
        Me.Descripcion.Frozen = True
        Me.Descripcion.HeaderText = "Descripción"
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.ReadOnly = True
        '
        'Linea
        '
        Me.Linea.Frozen = True
        Me.Linea.HeaderText = "Linea"
        Me.Linea.Name = "Linea"
        Me.Linea.ReadOnly = True
        '
        'sp
        '
        Me.sp.Frozen = True
        Me.sp.HeaderText = "Stock Puebla"
        Me.sp.Name = "sp"
        Me.sp.ReadOnly = True
        '
        'sm
        '
        Me.sm.Frozen = True
        Me.sm.HeaderText = "Stock Merida"
        Me.sm.Name = "sm"
        Me.sm.ReadOnly = True
        '
        'st
        '
        Me.st.Frozen = True
        Me.st.HeaderText = "Stock Tuxtla"
        Me.st.Name = "st"
        Me.st.ReadOnly = True
        '
        'stt
        '
        Me.stt.Frozen = True
        Me.stt.HeaderText = "Stock Total"
        Me.stt.Name = "stt"
        Me.stt.ReadOnly = True
        '
        'L01
        '
        Me.L01.HeaderText = "Lista 01"
        Me.L01.Name = "L01"
        Me.L01.ReadOnly = True
        '
        'TL01
        '
        Me.TL01.HeaderText = "Total L01"
        Me.TL01.Name = "TL01"
        Me.TL01.ReadOnly = True
        '
        'PzaVtaPue
        '
        Me.PzaVtaPue.HeaderText = "Pza. Vta. Puebla"
        Me.PzaVtaPue.Name = "PzaVtaPue"
        Me.PzaVtaPue.ReadOnly = True
        '
        'ImpVtaPue
        '
        Me.ImpVtaPue.HeaderText = "Imp. Vta. Puebla"
        Me.ImpVtaPue.Name = "ImpVtaPue"
        Me.ImpVtaPue.ReadOnly = True
        '
        'PzaDevPue
        '
        Me.PzaDevPue.HeaderText = "Pza. Dev. Puebla"
        Me.PzaDevPue.Name = "PzaDevPue"
        Me.PzaDevPue.ReadOnly = True
        '
        'ImpDevPue
        '
        Me.ImpDevPue.HeaderText = "Imp. Dev. Puebla"
        Me.ImpDevPue.Name = "ImpDevPue"
        Me.ImpDevPue.ReadOnly = True
        '
        'PzaVtaMer
        '
        Me.PzaVtaMer.HeaderText = "Pza. Vta. Mérida"
        Me.PzaVtaMer.Name = "PzaVtaMer"
        Me.PzaVtaMer.ReadOnly = True
        '
        'ImpVtaMer
        '
        Me.ImpVtaMer.HeaderText = "Imp. Vta. Mérida"
        Me.ImpVtaMer.Name = "ImpVtaMer"
        Me.ImpVtaMer.ReadOnly = True
        '
        'PzaDevMer
        '
        Me.PzaDevMer.HeaderText = "Pza. Dev. Mérida"
        Me.PzaDevMer.Name = "PzaDevMer"
        Me.PzaDevMer.ReadOnly = True
        '
        'ImpDevMer
        '
        Me.ImpDevMer.HeaderText = "Imp. Dev. Mérida"
        Me.ImpDevMer.Name = "ImpDevMer"
        Me.ImpDevMer.ReadOnly = True
        '
        'PzaVtaTux
        '
        Me.PzaVtaTux.HeaderText = "Pza. Vta. Tuxtla"
        Me.PzaVtaTux.Name = "PzaVtaTux"
        Me.PzaVtaTux.ReadOnly = True
        '
        'ImpVtaTux
        '
        Me.ImpVtaTux.HeaderText = "Imp. Vta. Tuxtla"
        Me.ImpVtaTux.Name = "ImpVtaTux"
        Me.ImpVtaTux.ReadOnly = True
        '
        'PzaDevTux
        '
        Me.PzaDevTux.HeaderText = "Pza. Dev. Tuxtla"
        Me.PzaDevTux.Name = "PzaDevTux"
        Me.PzaDevTux.ReadOnly = True
        '
        'ImpDevTux
        '
        Me.ImpDevTux.HeaderText = "Imp. Dev. Tuxtla"
        Me.ImpDevTux.Name = "ImpDevTux"
        Me.ImpDevTux.ReadOnly = True
        '
        'PzasTot
        '
        Me.PzasTot.HeaderText = "Pzas. Totales"
        Me.PzasTot.Name = "PzasTot"
        Me.PzasTot.ReadOnly = True
        '
        'Total
        '
        Me.Total.HeaderText = "Imp. Total"
        Me.Total.Name = "Total"
        Me.Total.ReadOnly = True
        '
        'frmArticulosRemate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1284, 641)
        Me.Controls.Add(Me.gbremate)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmArticulosRemate"
        Me.Text = "Articulos de  Remate"
        Me.gbremate.ResumeLayout(False)
        Me.gbremate.PerformLayout()
        CType(Me.dgvarticulos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbremate As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpff As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpfi As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmblinea As System.Windows.Forms.ComboBox
    Friend WithEvents cmbalmacen As System.Windows.Forms.ComboBox
    Friend WithEvents cbventa As System.Windows.Forms.CheckBox
    Friend WithEvents btnexportar As System.Windows.Forms.Button
    Friend WithEvents btnconsultar As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dgvarticulos As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Articulo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Linea As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sm As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents st As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents stt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents L01 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TL01 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PzaVtaPue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ImpVtaPue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PzaDevPue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ImpDevPue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PzaVtaMer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ImpVtaMer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PzaDevMer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ImpDevMer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PzaVtaTux As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ImpVtaTux As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PzaDevTux As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ImpDevTux As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PzasTot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
