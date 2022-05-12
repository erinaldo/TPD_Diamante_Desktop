<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LOIngObj
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LOIngObj))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CBMes = New System.Windows.Forms.ComboBox()
        Me.CBAño = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CBAgente = New System.Windows.Forms.ComboBox()
        Me.TBobjetivo = New System.Windows.Forms.TextBox()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.DGObjAge = New System.Windows.Forms.DataGridView()
        Me.DGLOIngGen = New System.Windows.Forms.DataGridView()
        Me.BSave = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.BVerObj = New System.Windows.Forms.Button()
        Me.BIngObj = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGObjAge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGLOIngGen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(-1, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(109, 10)
        Me.DataGridView1.TabIndex = 0
        Me.DataGridView1.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Seleccione Líneas"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(183, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Mes"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(363, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Año"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(363, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Objetivo"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(183, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 13)
        Me.Label8.TabIndex = 465
        Me.Label8.Text = "Agente"
        '
        'CBMes
        '
        Me.CBMes.FormattingEnabled = True
        Me.CBMes.Location = New System.Drawing.Point(186, 27)
        Me.CBMes.Name = "CBMes"
        Me.CBMes.Size = New System.Drawing.Size(115, 21)
        Me.CBMes.TabIndex = 470
        '
        'CBAño
        '
        Me.CBAño.FormattingEnabled = True
        Me.CBAño.Items.AddRange(New Object() {"2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022"})
        Me.CBAño.Location = New System.Drawing.Point(366, 27)
        Me.CBAño.Name = "CBAño"
        Me.CBAño.Size = New System.Drawing.Size(85, 21)
        Me.CBAño.TabIndex = 471
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(348, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 13)
        Me.Label5.TabIndex = 472
        Me.Label5.Text = "$"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(384, 168)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 13)
        Me.Label6.TabIndex = 474
        Me.Label6.Text = "Objetivos Totales"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(235, 313)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 13)
        Me.Label7.TabIndex = 475
        Me.Label7.Text = "Objetivos por Agente"
        '
        'CBAgente
        '
        Me.CBAgente.FormattingEnabled = True
        Me.CBAgente.Location = New System.Drawing.Point(186, 79)
        Me.CBAgente.Name = "CBAgente"
        Me.CBAgente.Size = New System.Drawing.Size(144, 21)
        Me.CBAgente.TabIndex = 480
        '
        'TBobjetivo
        '
        Me.TBobjetivo.Location = New System.Drawing.Point(366, 79)
        Me.TBobjetivo.Name = "TBobjetivo"
        Me.TBobjetivo.Size = New System.Drawing.Size(85, 20)
        Me.TBobjetivo.TabIndex = 484
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.CheckOnClick = True
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"ABRAZADERAS", "ACCUPART", "ACURRIDE", "AMB", "AMERICAN", "ATSA", "BEL-AR", "BENDIX", "BIRLOS IMPORTADOS", "BLUE AIR CONTROL", "BUJES", "BULL", "CARDAN", "CLEVITE", "COMESA", "CONTINENTAL", "CONTITECH", "CPV", "DAYTON", "DISA", "DONAL", "ENPA", "EXCEL", "FAG", "FIRESTONE", "FLEETGUARD", "FRENOSA", "FULLER", "GOODYEAR", "GRANT", "GRC", "GROTE", "HALDEX", "HOLLAND", "HORTON", "INACTIVOS", "JOS", "JUNTAS", "KINEDYNE", "KYSOR", "LLANTAS", "MALDONADO", "MANGUERAS", "MAZA", "MERIT", "MINCER", "MISCELANEOS", "MONROE", "MTY", "NATIONAL", "OTROS SERVICIOS", "PARBO", "PHILLIPS", "PHM", "PISTONES", "PJ", "PREMIER", "PTPH", "RAYBESTOS", "REMACHES", "REPISA", "RESORTES", "REYMAK", "ROAND", "RODAN", "RODILLOS", "SAP", "SEALCO", "SILVERLINE", "SKF", "SPICER", "STATSA", "STEMCO", "SUSPENSION", "SYDA", "T&J", "TEPEYAC", "TIMKEN", "TRAKTOLAMP", "TRICO", "TROQ", "TUBO FLEXIBLE", "TUERCAS", "USK", "VARIOS", "WORLD AMERICAN", "ZAPATAS"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(21, 60)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(131, 334)
        Me.CheckedListBox1.TabIndex = 487
        '
        'DGObjAge
        '
        Me.DGObjAge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGObjAge.Location = New System.Drawing.Point(238, 329)
        Me.DGObjAge.Name = "DGObjAge"
        Me.DGObjAge.Size = New System.Drawing.Size(354, 116)
        Me.DGObjAge.TabIndex = 488
        '
        'DGLOIngGen
        '
        Me.DGLOIngGen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGLOIngGen.Location = New System.Drawing.Point(387, 184)
        Me.DGLOIngGen.Name = "DGLOIngGen"
        Me.DGLOIngGen.Size = New System.Drawing.Size(205, 87)
        Me.DGLOIngGen.TabIndex = 489
        '
        'BSave
        '
        Me.BSave.Image = CType(resources.GetObject("BSave.Image"), System.Drawing.Image)
        Me.BSave.Location = New System.Drawing.Point(467, 72)
        Me.BSave.Name = "BSave"
        Me.BSave.Size = New System.Drawing.Size(38, 33)
        Me.BSave.TabIndex = 485
        Me.BSave.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(552, 138)
        Me.Button2.Name = "Button2"
        Me.Button2.Padding = New System.Windows.Forms.Padding(1)
        Me.Button2.Size = New System.Drawing.Size(40, 40)
        Me.Button2.TabIndex = 490
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(552, 283)
        Me.Button1.Name = "Button1"
        Me.Button1.Padding = New System.Windows.Forms.Padding(1)
        Me.Button1.Size = New System.Drawing.Size(40, 40)
        Me.Button1.TabIndex = 491
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(186, 168)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(100, 82)
        Me.ListBox1.TabIndex = 493
        Me.ListBox1.Visible = False
        '
        'BVerObj
        '
        Me.BVerObj.BackColor = System.Drawing.Color.AliceBlue
        Me.BVerObj.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BVerObj.ForeColor = System.Drawing.Color.MediumBlue
        Me.BVerObj.Location = New System.Drawing.Point(186, 120)
        Me.BVerObj.Name = "BVerObj"
        Me.BVerObj.Size = New System.Drawing.Size(100, 33)
        Me.BVerObj.TabIndex = 486
        Me.BVerObj.Text = "Ver Objetivos"
        Me.BVerObj.UseVisualStyleBackColor = False
        Me.BVerObj.Visible = False
        '
        'BIngObj
        '
        Me.BIngObj.BackColor = System.Drawing.Color.AliceBlue
        Me.BIngObj.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BIngObj.ForeColor = System.Drawing.Color.MediumBlue
        Me.BIngObj.Location = New System.Drawing.Point(186, 120)
        Me.BIngObj.Name = "BIngObj"
        Me.BIngObj.Size = New System.Drawing.Size(100, 42)
        Me.BIngObj.TabIndex = 494
        Me.BIngObj.Text = "Ingresar Objetivos"
        Me.BIngObj.UseVisualStyleBackColor = False
        '
        'LOIngObj
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 457)
        Me.Controls.Add(Me.BIngObj)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.DGLOIngGen)
        Me.Controls.Add(Me.DGObjAge)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.BVerObj)
        Me.Controls.Add(Me.BSave)
        Me.Controls.Add(Me.TBobjetivo)
        Me.Controls.Add(Me.CBAgente)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CBAño)
        Me.Controls.Add(Me.CBMes)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "LOIngObj"
        Me.Text = "LOIngObj"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGObjAge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGLOIngGen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CBMes As System.Windows.Forms.ComboBox
    Friend WithEvents CBAño As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CBAgente As System.Windows.Forms.ComboBox
    Friend WithEvents TBobjetivo As System.Windows.Forms.TextBox
    Friend WithEvents BSave As System.Windows.Forms.Button
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents DGObjAge As System.Windows.Forms.DataGridView
    Friend WithEvents DGLOIngGen As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents BVerObj As System.Windows.Forms.Button
    Friend WithEvents BIngObj As System.Windows.Forms.Button
End Class
