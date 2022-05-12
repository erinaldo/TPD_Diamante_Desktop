Imports System.Data.SqlClient

Public Class BackOrderVtas

  Dim DvArticulo As New DataView
  Dim DvClte As New DataView
  Dim DvAgte As New DataView
  Dim DvBO As New DataView

  Dim DVdgagte As New DataView
  Dim DVdgclte As New DataView
  Dim DVdglin As New DataView
  Dim DVdgart As New DataView

  Private Sub BackOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.Text = "Back O - Ventas -- " & Me.Name.ToString & ".vb"
        'If VEsAgente = 1 Then
        '    Me.WindowState = FormWindowState.Normal
        '    Me.GrdConProd.Width = 1047
        '    Me.GrdConProd.Height = 512
        '    Me.Size = New System.Drawing.Size(1065, 551)
        'End If

        If UsrTPM = "RROBLES" Or UsrTPM = "VVERGARA" _
        Or UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" _
        Or UsrTPM = "VENTAS5" Or UsrTPM = "ASTRIDY" Or UsrTPM = "CSANTOS" _
        Or UsrTPM = "VENTAS8" Then
            Me.cmbAlmacen.Enabled = False

        ElseIf UsrTPM = "ACASTRO" Or UsrTPM = "JSANCHEZ" Or UsrTPM = "RMERCADO" Or UsrTPM = "AVERACRUZ" Or UsrTPM = "ABAJIO" Or UsrTPM = "AMERIDA" _
        Or UsrTPM = "VENTAS4" Or UsrTPM = "RJIMENEZ" Or UsrTPM = "ATUXTLA" Or UsrTPM = "VENTAS5" Or UsrTPM = "VENTAS9" Or UsrTPM = "LCEBALLOS" Then

            Me.CmbAgteVta.SelectedValue = vCodAgte
            Me.CmbAgteVta.Enabled = False
            Me.cmbAlmacen.Enabled = False
            BuscaClientes()
            Me.CmbCliente.Focus()
        End If


















        Dim FchInicio As DateTime
    FchInicio = DateAdd(DateInterval.Month, -1, Date.Now)
    Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
    Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")


    Dim ConsutaLista As String


    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

      mllenaComboAlmacen(SqlConnection)

      ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
      Dim daGArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

      Dim DSetTablas As New DataSet
      daGArticulo.Fill(DSetTablas, "GArticulos")

      Dim fila As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      fila = DSetTablas.Tables("GArticulos").NewRow

      'Agregamos los valores a los campos de la tabla
      fila("ItmsGrpNam") = "TODOS"
      fila("ItmsGrpCod") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("GArticulos").Rows.Add(fila)

      Me.CmbGrupoArticulo.DataSource = DSetTablas.Tables("GArticulos")
      Me.CmbGrupoArticulo.DisplayMember = "ItmsGrpNam"
      Me.CmbGrupoArticulo.ValueMember = "ItmsGrpCod"
      Me.CmbGrupoArticulo.SelectedValue = 999


            '''''*******************************
            ConsutaLista = "SELECT T0.slpcode,T0.slpname,  " +
                        "CASE WHEN T1.GroupCode IS NULL THEN 104 ELSE T1.GroupCode END " +
                        "AS 'GroupCode' FROM OSLP T0 " +
            "LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode " +
            "WHERE (T1.CbrGralAdicional = 'N' OR T0.SlpCode = -1)  and (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC') ORDER BY slpname"


            Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


      daAgte.Fill(DSetTablas, "Agentes")

      Dim filaAgte As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      filaAgte = DSetTablas.Tables("Agentes").NewRow

      'Agregamos los valores a los campos de la tabla
      filaAgte("slpname") = "TODOS"
      filaAgte("slpcode") = 999
      filaAgte("GroupCode") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

      DvAgte.Table = DSetTablas.Tables("Agentes")

      Me.CmbAgteVta.DataSource = DSetTablas.Tables("Agentes")
      Me.CmbAgteVta.DisplayMember = "slpname"
      Me.CmbAgteVta.ValueMember = "slpcode"
      Me.CmbAgteVta.SelectedValue = 999


            If UsrTPM = "ACASTRO" Or UsrTPM = "JSANCHEZ" Or UsrTPM = "RMERCADO" Or UsrTPM = "ABAJIO" Or UsrTPM = "AVERACRUZ" _
            Or UsrTPM = "VENTAS4" Or UsrTPM = "RJIMENEZ" Or UsrTPM = "ATUXTLA" Or UsrTPM = "VENTAS5" Or UsrTPM = "VENTAS9" Or UsrTPM = "AMERIDA" Or UsrTPM = "LCEBALLOS" Or UsrTPM = "ATABASCO" Then
                'MsgBox(UsrTPM)
                'MsgBox(vCodAgte)
                Me.CmbAgteVta.SelectedValue = vCodAgte
                Me.CmbAgteVta.Enabled = False
                Me.cmbAlmacen.Enabled = False
                BuscaClientes()
                Me.CmbCliente.Focus()

            Else
                Me.CmbAgteVta.SelectedValue = 999
            End If

            ''''---------------------------------



            If UsrTPM = "MANAGER" Then
                ConsutaLista = "SELECT CardCode,CardName, SlpCode, GroupCode FROM OCRD WHERE CardType = 'C' ORDER BY CardName "
            Else
                ConsutaLista = "SELECT CardCode,CardName, SlpCode, GroupCode FROM OCRD WHERE CardType = 'C' AND SlpCode = '" & vCodAgte.ToString & "' ORDER BY CardName "
            End If

            Dim daClientes As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

      daClientes.Fill(DSetTablas, "Clientes")

      Dim filaClientes As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      filaClientes = DSetTablas.Tables("Clientes").NewRow

      'Agregamos los valores a los campos de la tabla
      filaClientes("CardName") = "TODOS"
      filaClientes("CardCode") = "TODOS"
      filaClientes("slpcode") = 999
      filaClientes("groupcode") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Clientes").Rows.Add(filaClientes)

      DvClte.Table = DSetTablas.Tables("Clientes")

      Me.CmbCliente.DataSource = DvClte
      Me.CmbCliente.DisplayMember = "CardName"
      Me.CmbCliente.ValueMember = "CardCode"
      Me.CmbCliente.SelectedValue = "TODOS"


      '-----------------------------------------------------
      ConsutaLista = "SELECT ItemCode,ItemName,ItmsGrpCod FROM OITM ORDER BY ItemCode"
      Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


      daArticulo.Fill(DSetTablas, "Articulos")

      Dim filaArticulo As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      filaArticulo = DSetTablas.Tables("Articulos").NewRow

      'Agregamos los valores a los campos de la tabla
      filaArticulo("ItemName") = "TODOS"
      filaArticulo("ItemCode") = "TODOS"
      filaArticulo("ItmsGrpCod") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Articulos").Rows.Add(filaArticulo)

      DvArticulo.Table = DSetTablas.Tables("Articulos")

      Me.CmbArticulo.DataSource = DvArticulo
      Me.CmbArticulo.DisplayMember = "ItemCode"
      Me.CmbArticulo.ValueMember = "ItemCode"
      Me.CmbArticulo.SelectedValue = "TODOS"



    End Using

    'If VEsAgente = 1 Then
    '    Me.CmbAgteVta.SelectedValue = vCodAgte
    '    Me.CmbAgteVta.Enabled = False
    '    BuscaClientes()
    '    Me.CmbCliente.Focus()
    'End If

    ' RbtnPue.Checked = True
  End Sub

  Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    DgVtaAgte.Columns.Clear()
    DgClientes.Columns.Clear()
    DgLineas.Columns.Clear()
    DgArticulos.Columns.Clear()


    If Me.DtpFechaIni.Value > Me.DtpFechaTer.Value Then
      MessageBox.Show("La fecha de inicio no pede ser mayor a la de termino", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      DtpFechaIni.Focus()
      Return
    End If


    If IsNothing(CmbAgteVta.SelectedValue) Then
      MessageBox.Show("Seleccione un agente de ventas",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbAgteVta.Focus()
      Return
    End If

    If IsNothing(CmbCliente.SelectedValue) Then
      MessageBox.Show("Seleccione un cliente",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbCliente.Focus()
      Return
    End If

    If IsNothing(CmbGrupoArticulo.SelectedValue) Then
      MessageBox.Show("Seleccione una línea",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbGrupoArticulo.Focus()
      Return
    End If

    If IsNothing(CmbArticulo.SelectedValue) Then
      MessageBox.Show("Seleccione un articulo",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbArticulo.Focus()
      Return
    End If

    Ejecutar_Consulta()
  End Sub

  'Private Sub BtnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExcel.Click
  '    Dim oExcel As Object
  '    Dim oBook As Object
  '    Dim oSheet As Object

  '    'Abrimos un nuevo libro
  '    oExcel = CreateObject("Excel.Application")
  '    oBook = oExcel.workbooks.add
  '    oSheet = oBook.worksheets(1)


  '    'COMBINAMOS CELDAS
  '    oSheet.Range("A1:E1").Merge(True)
  '    oSheet.Range("A2:E2").Merge(True)
  '    oSheet.Range("A3:E3").Merge(True)
  '    oSheet.Range("A4:E4").Merge(True)
  '    oSheet.Range("A5:E5").Merge(True)
  '    oSheet.Range("A6:E6").Merge(True)


  '    'DAR COLOR DE FONDO A CELDAS
  '    oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("A3:C3").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("A4:C4").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("A5:C5").INTERIOR.COLORINDEX = 15


  '    oSheet.Range("A7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("B7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("C7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("D7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("E7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("F7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("G7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("H7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("I7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("J7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("K7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("L7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("M7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("N7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("O7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("P7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("Q7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("R7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("S7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("T7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("U7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("V7").INTERIOR.COLORINDEX = 15
  '    oSheet.Range("W7").INTERIOR.COLORINDEX = 15


  '    'Declaramos el nombre de las columnas
  '    oSheet.range("A7").value = "Almacen"
  '    oSheet.range("B7").value = "Clave"
  '    oSheet.range("C7").value = "Nombre Cliente"
  '    oSheet.range("D7").value = "Agente"
  '    oSheet.range("E7").value = "Ord.Vta."
  '    oSheet.range("F7").value = "Fecha"
  '    oSheet.range("G7").value = "Articulo"
  '    oSheet.range("H7").value = "Categoría"
  '    oSheet.range("I7").value = "Descripción"
  '    oSheet.range("J7").value = "Linea"
  '    oSheet.range("K7").value = "Pedido Cliente"
  '    oSheet.range("L7").value = "Facturado"
  '    oSheet.range("M7").value = "Back Order"

  '    If cmbAlmacen.SelectedValue = "100" Or cmbAlmacen.Text = "TODOS" Then
  '        oSheet.range("N7").value = "Stock Puebla"
  '        oSheet.range("O7").value = "Stock Mérida"
  '        oSheet.range("P7").value = "Stock Tuxtla"

  '    ElseIf cmbAlmacen.SelectedValue = "102" Then
  '        oSheet.range("N7").value = "Stock Mérida"
  '        oSheet.range("O7").value = "Stock Puebla"
  '        oSheet.range("P7").value = "Stock Tuxtla"

  '    ElseIf cmbAlmacen.SelectedValue = "103" Then
  '        oSheet.range("N7").value = "Stock Tuxtla"
  '        oSheet.range("O7").value = "Stock Puebla"
  '        oSheet.range("P7").value = "Stock Mérida"
  '    End If




  '    oSheet.range("Q7").value = "$ Precio"
  '    oSheet.range("R7").value = "Lista"
  '    oSheet.range("S7").value = "$ Total BO"
  '    oSheet.range("T7").value = "Cantidad Sol. Prov."
  '    oSheet.range("U7").value = "Fecha Entrega Prov. Aprox."
  '    oSheet.range("V7").value = "Ord. de Compra"


  '    'DISEÑO DE EXCEL

  '    'para poner la primera fila de los titulos en negrita
  '    oSheet.range("A7:V7").font.bold = True


  '    oExcel.worksheets("Hoja1").Columns("A").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("A").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("B").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("B").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("C").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("C").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("D").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("D").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("E").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("E").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("F").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("F").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("G").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("G").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("H").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("H").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("I").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("I").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("J").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("J").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("K").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("K").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("L").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("L").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("M").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("M").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("N").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("N").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("O").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("O").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("P").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("P").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("Q").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("Q").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("R").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("R").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("S").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("S").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("T").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("T").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("U").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("U").Font.Size = 8
  '    oExcel.worksheets("Hoja1").Columns("V").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
  '    oExcel.worksheets("Hoja1").Columns("V").Font.Size = 8


  '    'Cambia el alto de celda 
  '    oSheet.range("A:V").RowHeight = 13

  '    'oSheet.range("A:V").HorizontalAlignment = xlCenter

  '    'TAMAÑO DE COLUMNAS
  '    oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 10
  '    oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 12
  '    oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 18
  '    oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 16
  '    oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 7
  '    oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 10
  '    oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 13
  '    oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 5
  '    oExcel.worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 18
  '    oExcel.worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 12
  '    oExcel.worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 6
  '    oExcel.worksheets("Hoja1").Columns("L").EntireColumn.ColumnWidth = 6
  '    oExcel.worksheets("Hoja1").Columns("M").EntireColumn.ColumnWidth = 6
  '    oExcel.worksheets("Hoja1").Columns("N").EntireColumn.ColumnWidth = 6
  '    oExcel.worksheets("Hoja1").Columns("O").EntireColumn.ColumnWidth = 6
  '    oExcel.worksheets("Hoja1").Columns("P").EntireColumn.ColumnWidth = 6
  '    oExcel.worksheets("Hoja1").Columns("Q").EntireColumn.ColumnWidth = 10
  '    oExcel.worksheets("Hoja1").Columns("R").EntireColumn.ColumnWidth = 5
  '    oExcel.worksheets("Hoja1").Columns("S").EntireColumn.ColumnWidth = 12
  '    oExcel.worksheets("Hoja1").Columns("T").EntireColumn.ColumnWidth = 7
  '    oExcel.worksheets("Hoja1").Columns("U").EntireColumn.ColumnWidth = 10
  '    oExcel.worksheets("Hoja1").Columns("V").EntireColumn.ColumnWidth = 7


  '    Dim fila_dt As Integer = 0
  '    Dim fila_dt_excel As Integer = 0
  '    Dim tanto_porcentaje As String = ""
  '    Dim marikona As Integer = 0

  '    Dim total_reg As Integer = 0

  '    total_reg = Me.GrdConProd.RowCount
  '    For fila_dt = 0 To total_reg - 1

  '        'para leer una celda en concreto
  '        'el numero es la columna
  '        Dim cel1 As String = Me.GrdConProd.Item(0, fila_dt).Value
  '        Dim cel2 As String = Me.GrdConProd.Item(1, fila_dt).Value
  '        Dim cel3 As String = IIf(IsDBNull(Me.GrdConProd.Item(2, fila_dt).Value), 0, Me.GrdConProd.Item(2, fila_dt).Value)
  '        Dim cel4 As String = IIf(IsDBNull(Me.GrdConProd.Item(3, fila_dt).Value), 0, Me.GrdConProd.Item(3, fila_dt).Value)
  '        Dim cel5 As String = IIf(IsDBNull(Me.GrdConProd.Item(4, fila_dt).Value), 0, Me.GrdConProd.Item(4, fila_dt).Value)
  '        Dim cel6 As String = IIf(IsDBNull(Me.GrdConProd.Item(5, fila_dt).Value), 0, Me.GrdConProd.Item(5, fila_dt).Value)
  '        Dim cel7 As String = IIf(IsDBNull(Me.GrdConProd.Item(6, fila_dt).Value), 0, Me.GrdConProd.Item(6, fila_dt).Value)
  '        Dim cel8 As String = IIf(IsDBNull(Me.GrdConProd.Item(7, fila_dt).Value), 0, Me.GrdConProd.Item(7, fila_dt).Value)

  '        Dim cel9 As String = IIf(IsDBNull(Me.GrdConProd.Item(8, fila_dt).Value), 0, Me.GrdConProd.Item(8, fila_dt).Value)
  '        Dim cel10 As String = IIf(IsDBNull(Me.GrdConProd.Item(9, fila_dt).Value), 0, Me.GrdConProd.Item(9, fila_dt).Value)

  '        Dim cel11 As String = IIf(IsDBNull(Me.GrdConProd.Item(10, fila_dt).Value), 0, Me.GrdConProd.Item(10, fila_dt).Value)
  '        Dim cel12 As String = IIf(IsDBNull(Me.GrdConProd.Item(11, fila_dt).Value), 0, Me.GrdConProd.Item(11, fila_dt).Value)
  '        Dim cel13 As String = IIf(IsDBNull(Me.GrdConProd.Item(12, fila_dt).Value), 0, Me.GrdConProd.Item(12, fila_dt).Value)
  '        Dim cel14 As String = IIf(IsDBNull(Me.GrdConProd.Item(13, fila_dt).Value), 0, Me.GrdConProd.Item(13, fila_dt).Value)
  '        Dim cel15 As String = IIf(IsDBNull(Me.GrdConProd.Item(14, fila_dt).Value), 0, Me.GrdConProd.Item(14, fila_dt).Value)
  '        Dim cel16 As String = IIf(IsDBNull(Me.GrdConProd.Item(15, fila_dt).Value), 0, Me.GrdConProd.Item(15, fila_dt).Value)
  '        Dim cel17 As String = IIf(IsDBNull(Me.GrdConProd.Item(16, fila_dt).Value), 0, Me.GrdConProd.Item(16, fila_dt).Value)
  '        Dim cel18 As String = IIf(IsDBNull(Me.GrdConProd.Item(17, fila_dt).Value), 0, Me.GrdConProd.Item(17, fila_dt).Value)
  '        Dim cel19 As String = IIf(IsDBNull(Me.GrdConProd.Item(18, fila_dt).Value), 0, Me.GrdConProd.Item(18, fila_dt).Value)
  '        Dim cel20 As String = IIf(IsDBNull(Me.GrdConProd.Item(19, fila_dt).Value), 0, Me.GrdConProd.Item(19, fila_dt).Value)
  '        Dim cel21 As String = IIf(IsDBNull(Me.GrdConProd.Item(20, fila_dt).Value), 0, Me.GrdConProd.Item(20, fila_dt).Value)
  '        Dim cel22 As String = IIf(IsDBNull(Me.GrdConProd.Item(21, fila_dt).Value), 0, Me.GrdConProd.Item(21, fila_dt).Value)

  '        fila_dt_excel = fila_dt + 8 'Renglón en donde se empieza a registrar el reporte

  '        'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
  '        oSheet.range("A" & fila_dt_excel).value = cel1
  '        oSheet.range("B" & fila_dt_excel).value = cel2
  '        oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
  '        oSheet.range("D" & fila_dt_excel).value = cel4
  '        oSheet.range("E" & fila_dt_excel).value = cel5
  '        oSheet.range("F" & fila_dt_excel).value = cel6
  '        oSheet.range("G" & fila_dt_excel).value = cel7
  '        oSheet.range("H" & fila_dt_excel).value = cel8


  '        oExcel.Worksheets("Hoja1").Columns("Q").NumberFormat = "$ ###,###,###.##"
  '        oExcel.Worksheets("Hoja1").Columns("S").NumberFormat = "$ ###,###,###.##"


  '        oSheet.range("I" & fila_dt_excel).value = cel9
  '        oSheet.range("J" & fila_dt_excel).value = cel10
  '        oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel11, 0)
  '        oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel12, 0)
  '        oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel13, 0)
  '        oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel14, 0)
  '        oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel15, 0)
  '        oSheet.range("P" & fila_dt_excel).value = FormatNumber(cel16, 0)
  '        oSheet.range("Q" & fila_dt_excel).value = FormatNumber(cel17, 2)
  '        oSheet.range("R" & fila_dt_excel).value = cel18
  '        oSheet.range("S" & fila_dt_excel).value = FormatNumber(cel19, 2)
  '        oSheet.range("T" & fila_dt_excel).value = cel20
  '        oSheet.range("U" & fila_dt_excel).value = cel21
  '        oSheet.range("V" & fila_dt_excel).value = cel22

  '    Next

  '    'ENCABEZADOS
  '    oSheet.range("A" & fila_dt_excel + 3).value = "Totales"
  '    oSheet.range("B" & fila_dt_excel + 3).value = "Importe ($)"
  '    oSheet.range("C" & fila_dt_excel + 3).value = "  (%)  "

  '    'COLOREAMOS LAS CELDAS
  '    oSheet.Range("A" & fila_dt_excel + 3).INTERIOR.COLORINDEX = 15
  '    oSheet.Range("B" & fila_dt_excel + 3).INTERIOR.COLORINDEX = 15
  '    oSheet.Range("C" & fila_dt_excel + 3).INTERIOR.COLORINDEX = 15

  '    'DAMOS FORMATO A CELDAS DE $ Y %
  '    oSheet.Range("B" & fila_dt_excel + 4).NumberFormat = "$ ###,###,###.##"
  '    oSheet.Range("B" & fila_dt_excel + 5).NumberFormat = "$ ###,###,###.##"
  '    oSheet.Range("B" & fila_dt_excel + 6).NumberFormat = "$ ###,###,###.##"


  '    oSheet.Range("C" & fila_dt_excel + 4).NumberFormat = "###.#0 %"
  '    oSheet.Range("C" & fila_dt_excel + 5).NumberFormat = "###.#0 %"
  '    oSheet.Range("C" & fila_dt_excel + 6).NumberFormat = "###.#0 %"

  '    'DAMOS VALOR A LAS CELDAS

  '    '******** Renglon 1
  '    oSheet.range("A" & fila_dt_excel + 4).value = DGBO.Item(0, 0).Value
  '    oSheet.range("B" & fila_dt_excel + 4).value = DGBO.Item(1, 0).Value

  '    Dim aux As Decimal

  '    If DGBO.Item(2, 0).Value Is DBNull.Value Then
  '        aux = 0
  '    Else
  '        aux = DGBO.Item(2, 0).Value / 100
  '    End If

  '    oSheet.range("C" & fila_dt_excel + 4).value = aux


  '    'Fin Renglon 1

  '    '******** Renglon 2
  '    oSheet.range("A" & fila_dt_excel + 5).value = DGBO.Item(0, 1).Value
  '    oSheet.range("B" & fila_dt_excel + 5).value = DGBO.Item(1, 1).Value

  '    If DGBO.Item(2, 1).Value >= 95 Then
  '        oSheet.Range("A" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 43
  '        oSheet.Range("B" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 43
  '        oSheet.Range("C" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 43
  '    ElseIf DGBO.Item(2, 1).Value >= 90 And aux < 95 Then
  '        oSheet.Range("A" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 6
  '        oSheet.Range("B" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 6
  '        oSheet.Range("C" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 6
  '    ElseIf DGBO.Item(2, 1).Value >= 80 And aux < 90 Then
  '        oSheet.Range("A" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 45
  '        oSheet.Range("B" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 45
  '        oSheet.Range("C" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 45
  '    ElseIf DGBO.Item(2, 1).Value > 0 And aux < 80 Then
  '        oSheet.Range("A" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 3
  '        oSheet.Range("B" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 3
  '        oSheet.Range("C" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 3
  '    End If

  '    Dim aux2 As Decimal

  '    If DGBO.Item(2, 1).Value Is DBNull.Value Then
  '        aux2 = 0
  '    Else
  '        aux2 = DGBO.Item(2, 1).Value / 100
  '    End If

  '    oSheet.range("C" & fila_dt_excel + 5).value = aux2

  '    'FinRenglon 2

  '    '******** Renglon 3
  '    oSheet.range("A" & fila_dt_excel + 6).value = DGBO.Item(0, 2).Value
  '    oSheet.range("B" & fila_dt_excel + 6).value = DGBO.Item(1, 2).Value

  '    Dim aux3 As Decimal

  '    If DGBO.Item(2, 2).Value Is DBNull.Value Then
  '        aux3 = 100
  '    Else
  '        aux3 = DGBO.Item(2, 2).Value / 100
  '    End If

  '    oSheet.range("C" & fila_dt_excel + 6).value = aux3
  '    'Fin Renglon 3


  '    Dim NRow As Integer = GrdConProd.RowCount - 1
  '    For i As Integer = 8 To NRow + 8
  '        oExcel.Worksheets("Hoja1").Cells.Item(i, 13).INTERIOR.COLORINDEX = 6     '6 YELLOW
  '    Next

  '    'Formato de texto para la primera columna CLAVE ART
  '    oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "@"

  '    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  '    'oSheet.columns("A:O").entirecolumn.autofit()
  '    oSheet.range("A1").value = "Reporte de Back Orders con periodo del - " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
  '    oSheet.range("A2").value = "Agente de Ventas - " + CmbAgteVta.Text
  '    oSheet.range("A3").value = "Cliente - " + CmbCliente.Text
  '    oSheet.range("A4").value = "Línea - " + CmbGrupoArticulo.Text
  '    oSheet.range("A5").value = "Artículo - " + CmbArticulo.Text

  '    oExcel.visible = True
  '    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  '    GC.Collect()
  '    oSheet = Nothing
  '    oBook = Nothing
  '    oExcel = Nothing

  'End Sub

  'Private Sub GrdConProd_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles GrdConProd.RowPrePaint


  '    'If GrdConProd.Rows(e.RowIndex).Cells("Ord_Vta").Value = 0 Then
  '    '    GrdConProd.Rows(e.RowIndex).Cells("OrdCompra").Style.ForeColor = Color.White ' 
  '    '    GrdConProd.Rows(e.RowIndex).Cells("Fecha").Style.ForeColor = Color.Transparent
  '    '    GrdConProd.Rows(e.RowIndex).Cells("FchEntrega").Style.ForeColor = Color.White '
  '    '    GrdConProd.Rows(e.RowIndex).Cells("Ord_Vta").Style.ForeColor = Color.White '
  '    'End If

  '    GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Style.BackColor = Color.Black
  '    GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Style.ForeColor = Color.White

  'End Sub

  Private Sub CmbGrupoArticulo_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbGrupoArticulo.SelectionChangeCommitted
    BuscaArticulos()
  End Sub

  Private Sub CmbGrupoArticulo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CmbGrupoArticulo.Validating
    BuscaArticulos()
  End Sub

  Private Sub CmbGrupoArticulo_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbGrupoArticulo.KeyUp
    BuscaArticulos()
  End Sub

  Private Sub CmbAgteVta_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbAgteVta.SelectionChangeCommitted
    BuscaClientes()
  End Sub

  Private Sub CmbAgteVta_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbAgteVta.KeyUp
    BuscaClientes()
  End Sub

  Private Sub CmbAgteVta_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CmbAgteVta.Validating
    BuscaClientes()
  End Sub

  'Combo cliente
  'Private Sub CmbCliente_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCliente.SelectedIndexChanged
  '    mDatosCliente(CmbCliente.SelectedValue.ToString)
  'End Sub

  ' ''' <summary>
  ' ''' Descripcion:Consulta el agente que corresponde al cliente solicitado
  ' ''' Fecha:09/05/2015
  ' ''' </summary>
  ' ''' <remarks></remarks>
  'Private Sub mDatosCliente(ByVal sCliente As String)
  '    Try
  '        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

  '            Dim da As New SqlDataAdapter(" Select SlpCode " +
  '            " from ocrd " +
  '            " where CardCode= '" + sCliente + "'", SqlConnection)

  '            Dim ds As New DataSet
  '            da.Fill(ds)

  '            CmbAgteVta.SelectedValue = DirectCast(ds.Tables(0).Rows(0).Item(0), Integer)

  '            cmbCliente.SelectedValue = sCliente

  '        End Using

  '    Catch ex As Exception

  '    End Try
  'End Sub

  ''' <summary>
  ''' Descripcion: Llena el combo de Almacen
  ''' Fecha:05/05/2015
  ''' </summary>
  ''' <param name="conexion"></param>
  ''' <remarks></remarks>
  Private Sub mllenaComboAlmacen(ByVal conexion As SqlConnection)
    Try
      Dim da As New SqlDataAdapter("SELECT GroupCode , GroupName " +
                                          "FROM OCRG with (nolock) " +
                                          "WHERE GroupType = 'C' ORDER BY GroupName ", conexion)

      Dim ds As New DataSet
      da.Fill(ds)
      ds.Tables(0).Rows.Add(0, "TODOS")
      Me.cmbAlmacen.DataSource = ds.Tables(0)
      Me.cmbAlmacen.DisplayMember = "GroupName"
      Me.cmbAlmacen.ValueMember = "GroupCode"

      If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Or UsrTPM = "DDORANTES" Then

        Me.cmbAlmacen.SelectedValue = 0

      Else

        If AlmTPM = "01" Then
          Me.cmbAlmacen.SelectedValue = "100"
        ElseIf AlmTPM = "03" Then
          Me.cmbAlmacen.SelectedValue = "102"
        ElseIf AlmTPM = "07" Then
          Me.cmbAlmacen.SelectedValue = "103"
        End If


      End If


    Catch ex As Exception

    End Try

  End Sub

  Sub BuscaArticulos()
    Try

      CmbArticulo.SelectedValue = "TODOS"

      If CmbGrupoArticulo.SelectedValue Is Nothing Or CmbGrupoArticulo.SelectedValue = 999 Then
        CmbArticulo.SelectedValue = "TODOS"
        DvArticulo.RowFilter = String.Empty

      Else
        DvArticulo.RowFilter = "ItmsGrpCod = " & Trim(Me.CmbGrupoArticulo.SelectedValue.ToString) & " OR ItmsGrpCod = 999"
      End If

    Catch ex As Exception

    End Try
  End Sub

  Sub BuscaClientes()

    If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then

      If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
        DvClte.RowFilter = String.Empty
        CmbCliente.SelectedValue = "TODOS"
      Else
        DvClte.RowFilter = "SlpCode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString) & " OR SlpCode = 999"
        CmbCliente.SelectedValue = "TODOS"
      End If

    Else
      If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
        DvClte.RowFilter = "GroupCode = " & Trim(Me.cmbAlmacen.SelectedValue.ToString) & " OR groupcode = 999"
        CmbCliente.SelectedValue = "TODOS"
      Else
        DvClte.RowFilter = "SlpCode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString) & " OR SlpCode = 999"
        CmbCliente.SelectedValue = "TODOS"
      End If
    End If


  End Sub

  Private Sub Ejecutar_Consulta()

    'GrdConProd.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES
    Try

      Dim Consulta As String = ""
      Dim strcadena As String = ""
      Dim CTabla As String = ""
      Dim DTMObra As New DataTable
      Dim DTProb As New DataTable
      Dim vAlm As Integer = 0

      'TxtAlmacen.Text = cmbAlmacen.Text
      'TxtAlmacen.Visible = True


      Consulta &= " SELECT T1.BaseEntry,MIN(T1.ActDelDate) AS FchFactBO " &
                        " INTO #TOrdVtaFact " &
                        " FROM RDR1 T0 " &
                        " INNER JOIN INV1 T1 ON T0.DocEntry = T1.BaseEntry " &
                        "and T0.LineStatus = 'O'" &
                        " group by T1.BaseEntry  "

      'MODIFICADO POR IVAN GONZALEZ
      'SE ANEXARON LAS ORDENES DE ENTREGA SIN AFECTAR A LAS DEMAS SUCURSALES
      Consulta &= " insert into #TOrdVtaFact " &
                        " SELECT T1.BaseEntry,MIN(T1.ActDelDate) AS FchFactBO " &
                        " FROM RDR1 T0 INNER JOIN DLN1 T1 ON T0.DocEntry = T1.BaseEntry and T0.LineStatus = 'O' " &
                        " group by T1.BaseEntry "

      '--CONSULTA DE ORDENES DE COMPRA DE PROVEEDOR
      '--PARTICIONA LOS REGISTRO A ENUMERAR POR ITEMCODE Y LOS ORDENA POR T0.DocDueDate
      '--ENUMERA CON UNO EL PEDIDO MAS RECIENTE DE CADA ARTICULO
      Consulta &= " SELECT  ROW_NUMBER() OVER(PARTITION BY T1.ItemCode ORDER BY T0.DocDueDate ASC) AS Enumera,T0.DocEntry,T0.DOCNUM,T1.ItemCode,T1.WhsCode,T1.Quantity,T0.DocDueDate " +
                                " INTO #TArtSol " +
                                " FROM OPOR T0  " +
                                " INNER JOIN POR1 T1 ON T0.DocEntry = T1.DocEntry " +
                                " WHERE LineStatus = 'O' "

      '--CONSULTA LOS BACK ORDERS
      Consulta &= " SELECT T1.WhsCode Almacen, T0.CardCode AS Id_Clte,T10.CardName AS Nombre,T5.SlpCode AS slpcode,T5.SlpName AS Agente,t0.DocEntry AS Ord_Vta,MAX(T6.FchFactBO) AS FechaBO,T1.ItemCode AS Articulo, " +
                                "      T1.Dscription AS Descripción,T4.ItmsGrpNam as Linea,T2.OnHand AS StockPue,T1.Quantity AS Solicitado,T1.Quantity - T1.OpenQty AS Facturado, " +
                                "      T1.OpenQty AS BackOrder,T8.OnHand AS StockMer,T9.OnHand AS StockTux,T1.Price AS Precio,T1.U_BXP_ListaP AS Lista_Precio,T1.OpenQty * T1.Price AS TotalBO, " +
                                "      T7.Quantity AS SolProveed,T7.DocDueDate AS FchEntrega,T7.DOCNUM AS OrdCompra " +
                                " INTO #TBackOrder " +
                                " FROM ORDR T0 " +
                                "      INNER JOIN #TOrdVtaFact T6 ON T0.DocEntry = T6.BaseEntry AND " +
                                " T6.FchFactBO between '" & DtpFechaIni.Value.ToString("yyyy-MM-dd") & "' AND '" & DtpFechaTer.Value.ToString("yyyy-MM-dd") & "'" +
                                "      INNER JOIN RDR1 T1 ON T0.DocEntry = T1.DocEntry  AND T0.PaidToDate > 0 " +
                                "      LEFT JOIN OITW T2 ON T1.ItemCode = T2.ItemCode AND T2.WhsCode = 01 " +
                                "      LEFT JOIN OITW T8 ON T1.ItemCode = T8.ItemCode AND T8.WhsCode = 03 " +
                                "      LEFT JOIN OITW T9 ON T1.ItemCode = T9.ItemCode AND T9.WhsCode = 07 " +
                                "      inner join oitm t3 on t1.itemcode=t3.itemcode and T3.ItmsGrpCod<>150 " +
                                "      LEFT JOIN OITB T4 ON T3.ItmsGrpCod = T4.ItmsGrpCod " +
                                "      LEFT JOIN #TArtSol T7 ON T7.ItemCode = T1.ItemCode AND T7.Enumera = 1 AND T1.WhsCode = T7.WhsCode " +
                                "      LEFT JOIN OSLP T5 ON T0.SlpCode = T5.SlpCode " +
                                "      LEFT JOIN OCRD T10 ON T0.CardCode=T10.CardCode " +
                                " WHERE T1.LineStatus = 'O' "
      If CmbAgteVta.SelectedValue <> 999 Then
        Consulta &= " AND T0.SlpCode = @IdAgente"
      End If

      If CmbCliente.SelectedValue <> "TODOS" Then
        Consulta &= " AND T0.CardCode = @IdCliente"
      End If

      If CmbGrupoArticulo.SelectedValue <> 999 Then
        Consulta &= " AND T3.ItmsGrpCod = @GrupoArt"
      End If

      If CmbArticulo.SelectedValue <> "TODOS" Then
        Consulta &= " AND T1.ItemCode = @IdArticulo"
      End If

      If (cmbAlmacen.SelectedValue <> 0) Then

        If cmbAlmacen.SelectedValue = 100 Then
          Consulta &= " AND T1.WhsCode = '01' "

        ElseIf cmbAlmacen.SelectedValue = 102 Then
          Consulta &= " AND T1.WhsCode = '03' "

        ElseIf cmbAlmacen.SelectedValue = 103 Then
          Consulta &= " AND T1.WhsCode = '07' "

        End If

      ElseIf cmbAlmacen.SelectedValue = 0 Then
        Consulta &= "  AND T1.WhsCode in (01,03,07) "
      End If

      'Group By del query anterior
      Consulta &= " GROUP BY T1.WhsCode, T0.CardCode, T10.CardName, T5.SlpCode, T5.SlpName, t0.DocEntry, " +
                " T1.ItemCode, T1.Dscription, T4.ItmsGrpNam, T2.OnHand, T1.Quantity, T1.Quantity - T1.OpenQty, " +
                " T1.OpenQty,T8.OnHand ,T9.OnHand ,T1.Price ,T1.U_BXP_ListaP ,T1.OpenQty * T1.Price , T7.Quantity ,T7.DocDueDate ,T7.DOCNUM  "

      Consulta &= "DECLARE @FINAL TABLE(WhsName varchar(50),Id_Clte varchar(20),Nombre varchar(150),slpcode INT,Agente varchar(150),Ord_Vta int,FechaBO date,Articulo varchar(100), "
      Consulta &= "Descripción varchar(200), Linea varchar (150), Solicitado int,Facturado int,BackOrder int,StockPue int,StockMer int,StockTux int, "
      Consulta &= "Precio decimal(15,4),Lista_Precio int,TotalBO decimal(15,2), SolProveed varchar(20),FchEntrega date,OrdCompra int "
      Consulta &= " )"

      '****************************************

      'CONSULTA FINAL

      Consulta &= "INSERT INTO @FINAL(WhsName,Id_Clte,Nombre,slpcode,Agente,Ord_Vta,FechaBO,Articulo,Descripción,Linea, "
      Consulta &= "Solicitado,Facturado,BackOrder,StockPue,StockMer,StockTux,Precio,Lista_Precio,TotalBO,SolProveed,FchEntrega,OrdCompra)"
      Consulta &= "SELECT alm.WhsName,Id_Clte,Nombre,slpcode,Agente,Ord_Vta,FechaBO,Articulo,Descripción,Linea,"
      Consulta &= " Solicitado,Facturado,BackOrder,StockPue,StockMer,StockTux,Precio,Lista_Precio,TotalBO,SolProveed,FchEntrega,OrdCompra "
      Consulta &= " FROM #TBackOrder TBO  "
      Consulta &= "        inner join owhs alm on TBO.Almacen=alm.WhsCode"
      Consulta &= " UNION ALL "
      Consulta &= " SELECT '" + cmbAlmacen.Text + "'  AS Almacen,"
      Consulta &= " CAST('' AS NVARCHAR(15))AS Id_Clte,CAST('$$ Total Back Order' AS NVARCHAR(100)) AS Nombre,CAST('9999' AS NVARCHAR(3)) AS slpcode,CAST('' AS NVARCHAR(155)) AS Agente,CAST('0' AS INT) AS Ord_Vta,"
      Consulta &= " CAST('12/12/9999' AS DATE) AS Fecha,CAST('' AS NVARCHAR(20)) AS Articulo,"
      Consulta &= " CAST('$$ Total Back Order' AS NVARCHAR(100)) AS Descripción,CAST('' AS NVARCHAR(20)) as Linea,sum(CAST(Solicitado AS DECIMAL(19,6))) AS Solicitado,"
      Consulta &= " sum(CAST(Facturado AS DECIMAL(19,6)))AS Facturado,sum(CAST(BackOrder AS DECIMAL(19,6))) AS BackOrder, "
      Consulta &= " CAST(0 AS DECIMAL(19,6)) AS StockPue, CAST(0 AS DECIMAL(19,6)) AS StockMer, CAST(0 AS DECIMAL(19,6)) AS StockTux, CAST(0 AS DECIMAL(19,6)) AS Precio,"
      Consulta &= " CAST('' AS NVARCHAR(MAX)) AS Lista_Precio, "
      Consulta &= " SUM(TotalBO) AS TotalBO,sum(CAST(SolProveed AS DECIMAL(19))) AS SolProveed,CAST('' AS DATETIME) AS FchEntrega,CAST('0' AS INT) AS OrdCompra"
      Consulta &= " FROM #TBackOrder "

      'If cmbAlmacen.SelectedValue = "100" Or cmbAlmacen.Text = "TODOS" Then
      '    Consulta &= "SELECT T0.WhsName, T0.Id_Clte, T0.Nombre, T0.Agente, T0.Ord_Vta, T0.FechaBO, T0.Articulo, T1.cat, T0.Descripción,T0.Linea,  "
      '    Consulta &= "T0.Solicitado, T0.Facturado, T0.BackOrder,T0.StockPue, T0.StockMer, T0.StockTux,T0.Precio,T0.Lista_Precio, T0.TotalBO, T0.SolProveed, T0.FchEntrega, T0.OrdCompra "
      '    Consulta &= "FROM @FINAL T0 LEFT JOIN TPM.DBO.CATEGORIAS T1 ON T0.ARTICULO COLLATE Modern_Spanish_CI_AI = T1.ITEMCODE ORDER BY T0.FechaBO "

      'ElseIf cmbAlmacen.SelectedValue = "102" Then

      '    Consulta &= "SELECT T0.WhsName, T0.Id_Clte, T0.Nombre, T0.Agente, T0.Ord_Vta, T0.FechaBO, T0.Articulo, T1.cat, T0.Descripción,T0.Linea,  "
      '    Consulta &= "T0.Solicitado, T0.Facturado, T0.BackOrder,T0.StockMer,T0.StockPue,T0.StockTux,T0.Precio,T0.Lista_Precio, T0.TotalBO, T0.SolProveed, T0.FchEntrega, T0.OrdCompra "
      '    Consulta &= "FROM @FINAL T0 LEFT JOIN TPM.DBO.CATEGORIAS T1 ON T0.ARTICULO COLLATE Modern_Spanish_CI_AI = T1.ITEMCODE ORDER BY T0.FechaBO "

      'ElseIf cmbAlmacen.SelectedValue = "103" Then

      '    Consulta &= "SELECT T0.WhsName, T0.Id_Clte, T0.Nombre, T0.Agente, T0.Ord_Vta, T0.FechaBO, T0.Articulo, T1.cat, T0.Descripción,T0.Linea,  "
      '    Consulta &= "T0.Solicitado, T0.Facturado, T0.BackOrder,T0.StockTux,T0.StockPue,T0.StockMer,T0.Precio,T0.Lista_Precio, T0.TotalBO, T0.SolProveed, T0.FchEntrega, T0.OrdCompra "
      '    Consulta &= "FROM @FINAL T0 LEFT JOIN TPM.DBO.CATEGORIAS T1 ON T0.ARTICULO COLLATE Modern_Spanish_CI_AI = T1.ITEMCODE ORDER BY T0.FechaBO "

      'End If

      '******     FACTURAS
      Consulta &= "SELECT t6.DocNum, T6.DocDate, T6.CardCode,T7.CardName, T6.SlpCode, T1.SlpName, "
      Consulta &= "CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal ELSE T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100 END AS Importe, "
      Consulta &= "CASE WHEN T0.ItemCode IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T0.ItemCode END AS 'ItemCode', "
      Consulta &= "CASE WHEN T2.ItemName IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItemName END AS 'ItemName', "
      Consulta &= "T3.ItmsGrpCod, "
      Consulta &= "CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', "
      Consulta &= "T4.GroupCode, T5.GroupName "
      Consulta &= "INTO #VtasNetas "
      Consulta &= "FROM [SBO_TPD].[dbo].[OINV] T6	"
      Consulta &= "INNER JOIN [SBO_TPD].[dbo].[INV1] T0 ON T0.DocEntry = t6.DocEntry "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
      Consulta &= "LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode	"
      Consulta &= "LEFT JOIN [SBO_TPD].[dbo].[OCRD] T7 ON T6.Cardcode = T7.CardCode "
      Consulta &= "WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
      Consulta &= "AND T6.DocType <> 'S' and t2.ItmsGrpCod <> 200 "
      Consulta &= "and T4.CbrGralAdicional = 'N' "

      '********************************************
      If CmbAgteVta.SelectedValue <> 999 Then
        Consulta &= " AND T6.SlpCode = @IdAgente "
      End If

      If CmbCliente.SelectedValue <> "TODOS" Then
        Consulta &= " AND T6.CardCode = @IdCliente "
      End If

      If CmbGrupoArticulo.SelectedValue <> 999 Then
        Consulta &= " AND T3.ItmsGrpCod = @GrupoArt "
      End If

      If CmbArticulo.SelectedValue <> "TODOS" Then
        Consulta &= " AND T0.ItemCode = @IdArticulo "
      End If

      If cmbAlmacen.SelectedValue = "100" Then
        'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 100 ) "
        Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '01' union all select -1 ) "
      ElseIf cmbAlmacen.SelectedValue = "102" Then
        'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 102 ) "
        Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '03' ) "
      ElseIf cmbAlmacen.SelectedValue = "103" Then
        'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 103 ) "
        Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '07' ) "
      End If
      '**********************************************

      Consulta &= "UNION ALL "

      '*******    NOTAS DE CREDITO
      Consulta &= "SELECT t6.DocNum, T6.DocDate,T6.CardCode,T7.CardName,T6.SlpCode, T1.SlpName, "
      Consulta &= "CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal * -1 ELSE (T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100)*-1 END AS Importe, "
      Consulta &= "T0.ItemCode, T2.ItemName, T3.ItmsGrpCod, CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', "
      Consulta &= "T4.GroupCode, T5.GroupName "
      Consulta &= "FROM [SBO_TPD].[dbo].[ORIN] T6	"
      Consulta &= "INNER JOIN [SBO_TPD].[dbo].[RIN1] T0 ON T0.DocEntry = t6.DocEntry "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
      Consulta &= "left join [SBO_TPD].[dbo].ECM2 t8 on t0.DocEntry = t8.SrcObjAbs and t8.SrcObjType = 14 "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
      Consulta &= "LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
      Consulta &= "LEFT JOIN [SBO_TPD].[dbo].[OCRD] T7 ON T6.Cardcode = T7.CardCode "
      Consulta &= "WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
      Consulta &= "and T6.DocType <> 'S' "
      Consulta &= "and T2.ItmsGrpCod <> 200 "
      Consulta &= "and T8.ReportID is not null "
      Consulta &= "and T4.CbrGralAdicional = 'N' "
      '********************************************
      If CmbAgteVta.SelectedValue <> 999 Then
        Consulta &= " AND T6.SlpCode = @IdAgente "
      End If

      If CmbCliente.SelectedValue <> "TODOS" Then
        Consulta &= " AND T6.CardCode = @IdCliente "
      End If

      If CmbGrupoArticulo.SelectedValue <> 999 Then
        Consulta &= " AND T3.ItmsGrpCod = @GrupoArt "
      End If

      If CmbArticulo.SelectedValue <> "TODOS" Then
        Consulta &= " AND T0.ItemCode = @IdArticulo "
      End If

      If cmbAlmacen.SelectedValue = "100" Then
        Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '01' union all select -1 ) "
      ElseIf cmbAlmacen.SelectedValue = "102" Then
        Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '03' ) "
      ElseIf cmbAlmacen.SelectedValue = "103" Then
        Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '07' ) "
      End If
      '**********************************************FIN DEVOLUCIONES
      Consulta &= "UNION ALL "
      '*******    CANCELACIONES
      Consulta &= "SELECT t6.DocNum, T6.DocDate,T6.CardCode,T7.CardName,T6.SlpCode, T1.SlpName, "
      Consulta &= "CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal * -1 ELSE (T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100)*-1 END AS Importe, "
      Consulta &= "T0.ItemCode, T2.ItemName, T3.ItmsGrpCod, CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', "
      Consulta &= "T4.GroupCode, T5.GroupName "
      Consulta &= "FROM [SBO_TPD].[dbo].[ORIN] T6	"
      Consulta &= "INNER JOIN [SBO_TPD].[dbo].[RIN1] T0 ON T0.DocEntry = t6.DocEntry "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
      Consulta &= "left join [SBO_TPD].[dbo].ECM2 t8 on t0.DocEntry = t8.SrcObjAbs and t8.SrcObjType = 14 "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
      Consulta &= "LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
      Consulta &= "LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
      Consulta &= "LEFT JOIN [SBO_TPD].[dbo].[OCRD] T7 ON T6.Cardcode = T7.CardCode "
      Consulta &= "WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
      Consulta &= "and T6.DocType <> 'S' "
      Consulta &= "and T2.ItmsGrpCod <> 200 "
      Consulta &= "and T8.ReportID is null "
      Consulta &= "and T4.CbrGralAdicional = 'N' "

      '********************************************
      If CmbAgteVta.SelectedValue <> 999 Then
        Consulta &= " AND T6.SlpCode = @IdAgente "
      End If

      If CmbCliente.SelectedValue <> "TODOS" Then
        Consulta &= " AND T6.CardCode = @IdCliente "
      End If

      If CmbGrupoArticulo.SelectedValue <> 999 Then
        Consulta &= " AND T3.ItmsGrpCod = @GrupoArt "
      End If

      If CmbArticulo.SelectedValue <> "TODOS" Then
        Consulta &= " AND T0.ItemCode = @IdArticulo "
      End If

      If cmbAlmacen.SelectedValue = "100" Then
        'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 100 ) "
        Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '01' union all select -1 ) "
      ElseIf cmbAlmacen.SelectedValue = "102" Then
        'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 102 ) "
        Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '03' ) "
      ElseIf cmbAlmacen.SelectedValue = "103" Then
        'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 103 )  "
        Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '07' ) "
      End If
      '********************************************** FIN CANCELACIONES


      Consulta &= "DECLARE @TotNtas DECIMAL(20,2) = (SELECT CASE WHEN SUM(Importe) IS NULL THEN 0 ELSE SUM(Importe) END AS Importe FROM #VtasNetas) "

      Consulta &= "DECLARE @TotBO DECIMAL(20,2) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END AS TotalBO FROM #TBackOrder) "

      Consulta &= "DECLARE @NetBO DECIMAL(20,2) = (SELECT @TotNtas + @TotBO ) "


      Consulta &= "CREATE TABLE #DetBackOrder( "
      Consulta &= "detalle varchar(50), "
      Consulta &= "monto decimal(20,2), "
      Consulta &= "porc decimal(20,2)) "

      Consulta &= "INSERT INTO #DetBackOrder(detalle,monto,PORC) "
      Consulta &= "SELECT 'Total Back Order', CASE WHEN @TotBO IS NULL THEN 0 ELSE @TotBO END, case when @TotBO = 0 then 0 else @TotBO/@NetBO*100 end AS PORC "
      Consulta &= "UNION ALL "
      Consulta &= "SELECT 'Total Ventas', CASE WHEN @TotNtas IS NULL THEN 0 ELSE @TotNtas END, case when @TotNtas = 0 then 0 else @TotNtas/@NetBO*100 end AS PORC "
      Consulta &= "UNION ALL "
      Consulta &= "SELECT '**MONTO TOTAL**', @TotBO + @TotNtas, 100 "


      ''*****CODIDO AGREGADO
      '******************************************
      '******************************************
      '******************************************

      '--Se obtiene BO's Totales por agente
      Consulta &= "SELECT slpcode as 'slpcode',Agente as 'slpname',CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END AS 'TotalBO' "
      Consulta &= "INTO #BOAgente "
      Consulta &= "FROM @FINAL WHERE slpcode <> 999 "
      Consulta &= "GROUP BY slpcode,Agente "

      '--Se obtiene ventas totales por agente
      Consulta &= "SELECT SlpCode as 'slpcode',SlpName as 'slpname',CASE WHEN SUM(IMPORTE) IS NULL THEN 0 ELSE SUM(IMPORTE) END AS 'Importe' "
      Consulta &= "INTO #VtasAgente "
      Consulta &= "FROM #VtasNetas "
      Consulta &= "GROUP BY SlpCode,SlpName "

      '--TABLA TEMP PARA GUARDAR AGENTES CON BO Y VTAS NETAS
      Consulta &= "CREATE TABLE #AGTE "
      Consulta &= "( slpcode int, "
      Consulta &= "slpname varchar(50)) "

      Consulta &= "insert into #AGTE(slpcode,slpname) "
      Consulta &= "SELECT DISTINCT(SLPCODE),Agente FROM @FINAL where slpcode <> 999 "

      Consulta &= "insert into #AGTE(slpcode,slpname) "
      Consulta &= "SELECT DISTINCT(SLPCODE),SLPNAME FROM #VtasNetas WHERE slpcode NOT IN (SELECT slpcode FROM #AGTE) "


      Consulta &= "CREATE TABLE #BoVtasNAgte( "
      Consulta &= "slpcode INT, "
      Consulta &= "slpname VARCHAR(120), "
      Consulta &= "vtasn DECIMAL (20,2), "
      Consulta &= "bo DECIMAL (20,2), "
      Consulta &= "total DECIMAL(20,2)) "


      Consulta &= "INSERT INTO #BoVtasNAgte(slpcode,slpname,vtasn,bo,total) "
      Consulta &= "SELECT T0.slpcode,T0.slpname,CASE WHEN T1.Importe IS NULL THEN 0 ELSE T1.Importe END AS 'Importe', "
      Consulta &= "CASE WHEN T2.TotalBO IS NULL THEN 0 ELSE T2.TotalBO END AS 'TotalBO', "
      Consulta &= "CASE WHEN T1.Importe IS NULL AND T2.TotalBO IS NULL THEN 0 "
      Consulta &= "WHEN T1.Importe IS NULL AND T2.TotalBO IS NOT NULL THEN T2.TotalBO "
      Consulta &= "WHEN T2.TotalBO IS NULL AND T1.Importe IS NOT NULL THEN T1.Importe "
      Consulta &= "ELSE T1.Importe + T2.TotalBO END AS 'Total' "
      Consulta &= "FROM #AGTE T0 "
      Consulta &= "LEFT JOIN #VtasAgente T1 ON T0.slpcode = T1.SlpCode "
      Consulta &= "LEFT JOIN #BOAgente T2 ON T0.slpcode = T2.slpcode "

      '-- *************** CONSULTA AGENTES
      Consulta &= "CREATE TABLE #Agentes( "
      Consulta &= "slpcode INT, "
      Consulta &= "slpname VARCHAR (120), "
      Consulta &= "vtasn DECIMAL(20,2), "
      Consulta &= "bo DECIMAL(20,2), "
      Consulta &= "total DECIMAL(20,2), "
      Consulta &= "VtaPerdida DECIMAL(20,2), "
      Consulta &= "VtaLograda DECIMAL(20,2), "
      Consulta &= "orden INT) "

      Consulta &= "INSERT INTO #Agentes(slpcode,slpname,vtasn,bo,total,VtaPerdida,VtaLograda,orden) "
      Consulta &= "SELECT slpcode,slpname,vtasn,bo,total, "
      Consulta &= "bo/total*100 AS '% Vtaperdida', "
      Consulta &= "vtasn/total*100 AS '% Vtalograda', "
      Consulta &= "0 "
      Consulta &= "FROM #BoVtasNAgte "

      If CmbAgteVta.SelectedValue = 999 Then
        Consulta &= "INSERT INTO #Agentes(slpcode,slpname,vtasn,bo,total,VtaPerdida,VtaLograda,orden) "
        Consulta &= "SELECT 0,'MONTOS TOTALES',SUM(vtasn),SUM(bo),SUM(total), "
        Consulta &= "SUM(bo)/sum(total)*100 AS '% Vta perdida', "
        Consulta &= "SUM(vtasn)/sum(total)*100 AS '% Vta lograda', "
        Consulta &= "1 "
        Consulta &= "FROM #BoVtasNAgte "
      End If


      Consulta &= "SELECT slpcode AS 'slpcode',slpname AS 'Slpname',vtasn AS 'vtasn',bo AS 'bo',total AS 'total',"
      Consulta &= "VtaPerdida/100 as 'vtaperdida',VtaLograda/100 as 'vtalograda',orden as 'orden' "
      Consulta &= "INTO #AgentesFinal FROM #Agentes  "

      Consulta &= "SELECT * FROM #AgentesFinal ORDER BY orden,vtalograda "


      '-- *****FIN CONSULTA AGENTES


      'Consulta &= "--SELECT * FROM #Clte "
      'Consulta &= "--ORDER BY cardcode "
      'Consulta &= "--OBTENEMOS BACKORDERS Y VENTAS TOTALES POR CLIENTE "

      Consulta &= "SELECT Id_Clte,Nombre,CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END AS 'TotalBO',Agente "
      Consulta &= "INTO #BOCliente "
      Consulta &= "FROM @FINAL WHERE slpcode <> 999 "
      Consulta &= "GROUP BY Id_Clte,Nombre,Agente "

      '--Se obtiene ventas totales por cliente
      Consulta &= "SELECT CardCode,CardName,CASE WHEN SUM(IMPORTE) IS NULL THEN 0 ELSE SUM(IMPORTE) END AS 'Importe',SlpName "
      Consulta &= "INTO #VtasCliente "
      Consulta &= "FROM #VtasNetas "
      Consulta &= "GROUP BY CardCode,cardname,SlpName "

      Consulta &= "CREATE TABLE #Clte( "
      Consulta &= "cardcode VARCHAR(20), "
      Consulta &= "cardname VARCHAR(200), "
      Consulta &= "Slpname VARCHAR(200)) "

      Consulta &= "INSERT INTO #Clte(cardcode,cardname,Slpname) "
      Consulta &= "SELECT DISTINCT(Id_Clte),Nombre,Agente "
      Consulta &= "FROM #TBackOrder WHERE slpcode <> 999 "
      'Consulta &= "--GROUP BY Id_Clte,Nombre,Agente "

      Consulta &= "INSERT INTO #Clte(cardcode,cardname,Slpname) "
      Consulta &= "SELECT distinct(CardCode),CardName,SlpName "
      Consulta &= "FROM #VtasCliente "
      'Consulta &= "WHERE CardCode COLLATE SQL_Latin1_General_CP850_CI_AS NOT IN (SELECT CardCode FROM #Clte) "

      Consulta &= "SELECT cardcode, cardname, Slpname "
      Consulta &= "INTO #Clte2 "
      Consulta &= "FROM #Clte "
      Consulta &= "GROUP BY cardcode, cardname, Slpname "


      Consulta &= "CREATE TABLE #BoVtasNClte( "
      Consulta &= "cardcode VARCHAR (15), "
      Consulta &= "cardname VARCHAR (120), "
      Consulta &= "vtasn DECIMAL (20,2), "
      Consulta &= "bo DECIMAL (20,2), "
      Consulta &= "total DECIMAL(20,2), "
      Consulta &= "slpname VARCHAR(160)) "

      Consulta &= "INSERT INTO #BoVtasNClte(cardcode,cardname,vtasn,bo,total,slpname) "
      Consulta &= "SELECT T0.cardcode as 'cardcode',T0.cardname,CASE WHEN T1.Importe IS NULL THEN 0 ELSE T1.Importe END AS 'Importe', "
      Consulta &= "CASE WHEN T2.TotalBO IS NULL THEN 0 ELSE T2.TotalBO END AS 'TotalBO', "
      Consulta &= "CASE WHEN T1.Importe IS NULL AND T2.TotalBO IS NULL THEN 0 "
      Consulta &= "WHEN T1.Importe IS NULL AND T2.TotalBO IS NOT NULL THEN T2.TotalBO "
      Consulta &= "WHEN T2.TotalBO IS NULL AND T1.Importe IS NOT NULL THEN T1.Importe "
      Consulta &= "ELSE T1.Importe + T2.TotalBO END AS 'Total',t0.Slpname "
      Consulta &= "FROM #Clte2 T0 "
      Consulta &= "LEFT JOIN #VtasCliente T1 ON T0.cardcode COLLATE SQL_Latin1_General_CP1_CI_AS = T1.CardCode "
      Consulta &= "AND T0.Slpname COLLATE SQL_Latin1_General_CP1_CI_AS = T1.SlpName "
      Consulta &= "LEFT JOIN #BOCliente T2 ON T0.cardcode COLLATE SQL_Latin1_General_CP1_CI_AS = T2.Id_Clte "
      Consulta &= "AND T0.Slpname COLLATE SQL_Latin1_General_CP1_CI_AS = T2.Agente "

      'Consulta &= "--SELECT * FROM #BoVtasNClte ORDER BY slpname,cardcode "

      '-- *************** CONSULTA CLIENTES
      Consulta &= "CREATE TABLE #CLIENTES( "
      Consulta &= "cardcode VARCHAR(15), "
      Consulta &= "cardname VARCHAR(150), "
      Consulta &= "vtasn DECIMAL(20,2), "
      Consulta &= "bo DECIMAL(20,2), "
      Consulta &= "total DECIMAL(20,2), "
      Consulta &= "PorVtaPerdida DECIMAL(20,2), "
      Consulta &= "PorVtaLograda DECIMAL(20,2), "
      Consulta &= "slpname VARCHAR(100), "
      Consulta &= "orden INT) "

      Consulta &= "INSERT INTO #CLIENTES(cardcode,cardname,vtasn,bo,total,PorVtaPerdida,PorVtaLograda,slpname,orden) "
      Consulta &= "SELECT cardcode,cardname,vtasn,bo,total, "
      Consulta &= "case when bo = 0 then 0 else bo/total*100 end AS 'PorVtaPerdida', "
      Consulta &= "CASE WHEN vtasn = 0 THEN 0 ELSE vtasn/total*100 END AS 'PorVtaLograda', "
      Consulta &= "slpname, 0 "
      Consulta &= "FROM #BoVtasNClte "
      Consulta &= "ORDER BY slpname,vtasn "

      Consulta &= " INSERT INTO #CLIENTES(cardcode,cardname,vtasn,bo,total,PorVtaPerdida,PorVtaLograda,slpname,orden)"
      Consulta &= " SELECT 'TOTAL','MONTOS TOTALES',SUM(vtasn),SUM(bo),SUM(total), "
      Consulta &= " CASE WHEN SUM(bo)=0 OR SUM(total)=0 THEN 0 ELSE SUM(bo)/SUM(total)*100 END 'PorVtaPerdida',"
      Consulta &= " CASE WHEN SUM(vtasn)=0 OR SUM(total)=0 THEN 0 ELSE SUM(vtasn)/SUM(total)*100 END 'PorVtaLograda',slpname, 1 "
      Consulta &= " FROM #BoVtasNClte "
      Consulta &= " GROUP BY slpname "
      Consulta &= " ORDER BY slpname "

      Consulta &= "INSERT INTO #CLIENTES(cardcode,cardname,vtasn,bo,total,PorVtaPerdida,PorVtaLograda,slpname,orden) "
      Consulta &= "SELECT 999,'MONTOS TOTALES',SUM(vtasn),SUM(bo),SUM(total), "
      Consulta &= "SUM(bo)/SUM(total)*100,SUM(vtasn)/SUM(total)*100,'', 2 "
      Consulta &= "FROM #BoVtasNClte "

      Consulta &= "SELECT cardcode as 'cardcode',cardname as 'cardname', vtasn as 'vtasn',bo as 'bo',total,PorVtaPerdida/100 'porvtaperd',PorVtaLograda/100 'vtalograda',slpname as 'slpname',orden "
      Consulta &= "INTO #CLientesFinal FROM #CLIENTES  "

      Consulta &= "SELECT * FROM #CLientesFinal where vtalograda <> 0 ORDER BY Orden,vtalograda  "
      'ORDER BY orden, vtasn DESC,slpname
      '-- *****FIN CONSULTA CLIENTES


      Consulta &= "CREATE TABLE #Linea( "
      Consulta &= "ItmsGrpNam VARCHAR(200), "
      Consulta &= "Slpname VARCHAR(200), "
      Consulta &= "cardcode VARCHAR(10)) "

      Consulta &= "INSERT INTO #Linea(ItmsGrpNam,Slpname,cardcode) "
      Consulta &= "SELECT Linea,Agente,Id_Clte "
      Consulta &= "FROM #TBackOrder WHERE slpcode <> 999 "
      'Consulta &= "--GROUP BY Linea,Agente,Id_Clte "

      Consulta &= "INSERT INTO #Linea(ItmsGrpNam,Slpname,cardcode) "
      Consulta &= "SELECT ItmsGrpNam,SlpName,CardCode "
      Consulta &= "FROM #VtasNetas "
      'Consulta &= "--WHERE ItmsGrpNam COLLATE SQL_Latin1_General_CP850_CI_AS NOT IN (SELECT ItmsGrpNam FROM #Linea) "
      'Consulta &= "--AND SlpName COLLATE SQL_Latin1_General_CP850_CI_AS NOT IN (SELECT SlpName FROM #Linea) "
      'Consulta &= "--AND CardCode COLLATE SQL_Latin1_General_CP850_CI_AS NOT IN (SELECT CardCode FROM #Linea) "

      'Consulta &= "--SELECT * FROM #Linea ORDER BY Slpname,ItmsGrpNam "

      'Consulta &= "--SELECT * FROM #VTA "
      'Consulta &= "--ORDER BY cardcode "
      'Consulta &= "--OBTENEMOS BACKORDERS Y VENTAS TOTALES POR CLIENTE "

      Consulta &= "SELECT Linea,CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END AS 'TotalBO',Agente,Id_Clte "
      Consulta &= "INTO #BOLineas "
      Consulta &= "FROM @FINAL WHERE slpcode <> 999 "
      Consulta &= "GROUP BY Linea,Agente,Id_Clte "

      'Consulta &= "--SELECT * FROM #BOLineas "

      '--Se obtiene ventas totales por cliente
      Consulta &= "SELECT CASE WHEN ItmsGrpNam IS NULL THEN 'OTROS' ELSE ItmsGrpNam END AS 'ItmsGrpNam', "
      Consulta &= "CASE WHEN SUM(IMPORTE) IS NULL THEN 0 ELSE SUM(IMPORTE) END AS 'Importe',SlpName,CardCode "
      Consulta &= "INTO #VtasLineas "
      Consulta &= "FROM #VtasNetas "
      Consulta &= "GROUP BY ItmsGrpNam,CardCode,SlpName "

      'Consulta &= "--SELECT * FROM #VtasLineas ORDER BY SlpName,ItmsGrpNam "


      Consulta &= "CREATE TABLE #BoVtasNLineas( "
      Consulta &= "Linea VARCHAR (150), "
      Consulta &= "vtasn DECIMAL (20,2), "
      Consulta &= "bo DECIMAL (20,2), "
      Consulta &= "total DECIMAL(20,2), "
      Consulta &= "slpname VARCHAR(160), "
      Consulta &= "cardcode VARCHAR(15) "
      Consulta &= ") "

      Consulta &= "INSERT INTO #BoVtasNLineas(Linea,vtasn,bo,total,slpname,cardcode) "
      Consulta &= "SELECT T0.ItmsGrpNam,CASE WHEN T1.Importe IS NULL THEN 0 ELSE T1.Importe END AS 'Importe', "
      Consulta &= "CASE WHEN T2.TotalBO IS NULL THEN 0 ELSE T2.TotalBO END AS 'TotalBO', "
      Consulta &= "CASE WHEN T1.Importe IS NULL AND T2.TotalBO IS NULL THEN 0 "
      Consulta &= "WHEN T1.Importe IS NULL AND T2.TotalBO IS NOT NULL THEN T2.TotalBO "
      Consulta &= "WHEN T2.TotalBO IS NULL AND T1.Importe IS NOT NULL THEN T1.Importe "
      Consulta &= "ELSE T1.Importe + T2.TotalBO END AS 'Total',t0.Slpname,T0.cardcode "
      Consulta &= "FROM #Linea T0 "
      Consulta &= "LEFT JOIN #VtasLineas T1 ON T0.ItmsGrpNam COLLATE SQL_Latin1_General_CP1_CI_AS = T1.ItmsGrpNam "
      Consulta &= "AND T0.Slpname COLLATE SQL_Latin1_General_CP1_CI_AS = T1.SlpName "
      Consulta &= "AND T0.cardcode COLLATE SQL_Latin1_General_CP1_CI_AS = T1.CardCode "
      Consulta &= "LEFT JOIN #BOLineas T2 ON T0.ItmsGrpNam COLLATE SQL_Latin1_General_CP1_CI_AS = T2.Linea "
      Consulta &= "AND T0.Slpname COLLATE SQL_Latin1_General_CP1_CI_AS = T2.Agente "
      Consulta &= "AND T0.cardcode COLLATE SQL_Latin1_General_CP1_CI_AS = T2.Id_Clte "


      'Consulta &= "--SELECT * FROM #BoVtasNLineas ORDER BY Linea,slpname "

      '-- *************** CONSULTA LINEAS
      Consulta &= "CREATE TABLE #Lineas( "
      Consulta &= "Linea VARCHAR(100), "
      Consulta &= "vtasn DECIMAL(20,2), "
      Consulta &= "bo DECIMAL(20,2), "
      Consulta &= "total DECIMAL(20,2), "
      Consulta &= "VtaPerdida DECIMAL(20,2), "
      Consulta &= "Vtalograda DECIMAL(20,2), "
      Consulta &= "slpname VARCHAR(100), "
      Consulta &= "cardcode VARCHAR(20), "
      Consulta &= "Orden INT) "

      Consulta &= "INSERT INTO #Lineas(Linea,vtasn,bo,total,VtaPerdida,Vtalograda,slpname,cardcode,Orden) "
      Consulta &= "SELECT Linea,vtasn,bo,total, "
      Consulta &= "case when bo = 0 OR total = 0 then 0 else bo/total*100 end AS 'VtaPerdida', "
      Consulta &= "CASE WHEN vtasn = 0 OR total = 0 THEN 0 ELSE vtasn/total*100 END AS 'VtaLograda',slpname,cardcode,0 "
      Consulta &= "FROM #BoVtasNLineas "
      Consulta &= "GROUP BY Linea,vtasn,BO,total,slpname,cardcode "
      Consulta &= "ORDER BY cardcode,Linea "

      Consulta &= "INSERT INTO #Lineas(Linea,vtasn,bo,total,VtaPerdida,Vtalograda,slpname,cardcode,Orden) "
      Consulta &= "SELECT 'MONTOS TOTALES' ,SUM(vtasn),SUM(bo),SUM(total), "
      Consulta &= "case when SUM(bo) = 0 then 0 else SUM(bo)/SUM(total)*100 end AS 'VtaPerdida', "
      Consulta &= "CASE WHEN SUM(vtasn) = 0 THEN 0 ELSE SUM(vtasn)/SUM(total)*100 END AS 'VtaLograda', "
      Consulta &= "slpname, cardcode, 1 "
      Consulta &= "FROM #Lineas "
      Consulta &= "GROUP BY cardcode,slpname "
      Consulta &= "ORDER BY cardcode "


      Consulta &= "SELECT T0.Linea as 'linea',T0.vtasn,T0.bo,T0.total,T0.VtaPerdida/100 'vtaperdida',T0.Vtalograda/100 'vtalograda', "
      Consulta &= "T0.cardcode as 'cardcode',T1.cardname,T0.slpname as 'slpname',T0.Orden INTO #LineasFInal FROM #Lineas T0 "
      Consulta &= "LEFT JOIN SBO_TPD.DBO.OCRD T1 ON t0.cardcode COLLATE SQL_Latin1_General_CP1_CI_AS= t1.cardcode "



      Consulta &= "SELECT * FROM #LineasFinal  "

      Consulta &= "UNION ALL "
      Consulta &= "SELECT '**TOTALES**' AS 'Linea',SUM(vtasn),SUM(bo),SUM(total), "
      Consulta &= "CASE WHEN SUM(bo)=0 OR SUM(total)=0 THEN 0 ELSE SUM(bo)/SUM(total)END AS 'vtaperdida', "
      Consulta &= "CASE WHEN SUM(vtasn)=0 OR SUM(total)=0 THEN 0 ELSE SUM(vtasn)/SUM(total) END AS 'vtalograda', "
      Consulta &= "'' as 'cardcode','Montos Totales' AS 'Nombre','' as 'slpname',100 FROM #LineasFinal where linea <> 'MONTOS TOTALES' "

      Consulta &= " UNION ALL"
      Consulta &= " SELECT linea AS 'Linea',SUM(vtasn),SUM(bo),SUM(total), "
      Consulta &= " CASE WHEN SUM(bo)=0 OR SUM(total)=0 THEN 0 ELSE SUM(bo)/SUM(total)END AS 'vtaperdida', "
      Consulta &= " CASE WHEN SUM(vtasn)=0 OR SUM(total)=0 THEN 0 ELSE SUM(vtasn)/SUM(total) END AS 'vtalograda', "
      Consulta &= " '' as 'cardcode','Montos Totales Lineas' AS 'Nombre','' as 'slpname',101 "
      Consulta &= " FROM #LineasFinal where linea <> 'MONTOS TOTALES' "
      Consulta &= " GROUP BY linea"
      Consulta &= " UNION ALL"
      Consulta &= " SELECT '**TOTALES**' AS 'Linea',SUM(vtasn),SUM(bo),SUM(total), "
      Consulta &= " CASE WHEN SUM(bo)=0 OR SUM(total)=0 THEN 0 ELSE SUM(bo)/SUM(total)END AS 'vtaperdida', "
      Consulta &= " CASE WHEN SUM(vtasn)=0 OR SUM(total)=0 THEN 0 ELSE SUM(vtasn)/SUM(total) END AS 'vtalograda', "
      Consulta &= " '' as 'cardcode','Montos Totales Lineas' AS 'Nombre','' as 'slpname',102"
      Consulta &= " FROM #LineasFinal where linea <> 'MONTOS TOTALES'"

      Consulta &= " UNION ALL"
      Consulta &= " SELECT linea AS 'Linea',SUM(vtasn),SUM(bo),SUM(total), "
      Consulta &= " CASE WHEN SUM(bo)=0 OR SUM(total)=0 THEN 0 ELSE SUM(bo)/SUM(total)END AS 'vtaperdida', "
      Consulta &= " CASE WHEN SUM(vtasn)=0 OR SUM(total)=0 THEN 0 ELSE SUM(vtasn)/SUM(total) END AS 'vtalograda', "
      Consulta &= " '' as 'cardcode','Montos Totales Agente' AS 'Nombre',slpname as 'slpname',104"
      Consulta &= " FROM #LineasFinal where linea <> 'MONTOS TOTALES'"
      Consulta &= " group by linea,slpname "

      Consulta &= " UNION ALL"
      Consulta &= " SELECT '**TOTALES**' AS 'Linea',SUM(vtasn),SUM(bo),SUM(total), "
      Consulta &= " CASE WHEN SUM(bo)=0 OR SUM(total)=0 THEN 0 ELSE SUM(bo)/SUM(total)END AS 'vtaperdida', "
      Consulta &= " CASE WHEN SUM(vtasn)=0 OR SUM(total)=0 THEN 0 ELSE SUM(vtasn)/SUM(total) END AS 'vtalograda', "
      Consulta &= " '' as 'cardcode','Montos Totales Agente' AS 'Nombre',slpname as 'slpname',105"
      Consulta &= " FROM #LineasFinal where linea <> 'MONTOS TOTALES' "
      Consulta &= " group by slpname "
      'Consulta &= " ORDER BY Orden, vtalograda "

      Consulta &= "ORDER BY Orden, vtalograda "


      'ORDER BY Orden,vtasn DESC
      '-- *****FIN CONSULTA LINEAS


      Consulta &= "CREATE TABLE #Articulos( "
      Consulta &= "ItemCode VARCHAR(150), "
      Consulta &= "Descripcion VARCHAR(200), "
      Consulta &= "Slpname VARCHAR(200), "
      Consulta &= "cardcode VARCHAR(10), "
      Consulta &= "linea VARCHAR(200)) "

      Consulta &= "INSERT INTO #Articulos(ItemCode,Descripcion,Slpname,cardcode,linea) "
      Consulta &= "SELECT Articulo,Descripción,Agente,Id_Clte,Linea "
      Consulta &= "FROM #TBackOrder WHERE slpcode <> 999 "
      Consulta &= "GROUP BY Articulo,Descripción,Agente,Id_Clte,Linea "

      Consulta &= "INSERT INTO #Articulos(ItemCode,Descripcion,Slpname,cardcode,linea) "
      Consulta &= "SELECT ItemCode,ItemName,SlpName,CardCode,ItmsGrpNam "
      Consulta &= "FROM #VtasNetas "
      Consulta &= "GROUP BY ItemCode,ItemName,SlpName,CardCode,ItmsGrpNam "
      'Consulta &= "--WHERE ItmsGrpNam COLLATE SQL_Latin1_General_CP850_CI_AS NOT IN (SELECT ItmsGrpNam FROM #Linea)"

      'Consulta &= "--SELECT * FROM #Articulos "
      'Consulta &= "--ORDER BY slpname,linea "

      '--OBTENEMOS BACKORDERS Y VENTAS TOTALES POR CLIENTE

      Consulta &= "SELECT articulo,Descripción, CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END AS 'TotalBO',Agente,Id_Clte,Linea "
      Consulta &= "INTO #BOArticulos "
      Consulta &= "FROM @FINAL WHERE slpcode <> 999 "
      Consulta &= "GROUP BY Articulo,Descripción,Agente,Id_Clte,Linea "

      'Consulta &= "--SELECT * FROM #BOArticulos ORDER BY Agente,Linea "

      '--Se obtiene ventas totales por cliente
      Consulta &= "SELECT ItemCode, ItemName,CASE WHEN SUM(IMPORTE) IS NULL THEN 0 ELSE SUM(IMPORTE) END AS 'Importe',SlpName,CardCode,ItmsGrpNam "
      Consulta &= "INTO #VtasArticulos "
      Consulta &= "FROM #VtasNetas "
      Consulta &= "GROUP BY ItemCode,ItemName,CardCode,SlpName,ItmsGrpNam "


      Consulta &= "CREATE TABLE #BoVtasNArticulos( "
      Consulta &= "ItemCode VARCHAR (150), "
      Consulta &= "ItemName varchar (150), "
      Consulta &= "vtasn DECIMAL (20,2), "
      Consulta &= "bo DECIMAL (20,2), "
      Consulta &= "total DECIMAL(20,2), "
      Consulta &= "slpname VARCHAR(160), "
      Consulta &= "cardcode VARCHAR (15), "
      Consulta &= "Linea VARCHAR(120)) "


      Consulta &= "INSERT INTO #BoVtasNArticulos(ItemCode,ItemName,vtasn,bo,total,slpname,cardcode,Linea) "
      Consulta &= "SELECT T0.ItemCode,t0.Descripcion,CASE WHEN T1.Importe IS NULL THEN 0 ELSE T1.Importe END AS 'Importe', "
      Consulta &= "CASE WHEN T2.TotalBO IS NULL THEN 0 ELSE T2.TotalBO END AS 'TotalBO', "
      Consulta &= "CASE WHEN T1.Importe IS NULL AND T2.TotalBO IS NULL THEN 0 "
      Consulta &= "WHEN T1.Importe IS NULL AND T2.TotalBO IS NOT NULL THEN T2.TotalBO "
      Consulta &= "WHEN T2.TotalBO IS NULL AND T1.Importe IS NOT NULL THEN T1.Importe "
      Consulta &= "ELSE T1.Importe + T2.TotalBO END AS 'Total', "
      Consulta &= "t0.Slpname, T0.cardcode, t0.linea "
      Consulta &= "FROM #Articulos T0 "
      Consulta &= "LEFT JOIN #VtasArticulos T1 ON T0.ItemCode COLLATE SQL_Latin1_General_CP1_CI_AS = T1.ItemCode "
      Consulta &= "AND T0.Slpname COLLATE SQL_Latin1_General_CP1_CI_AS = T1.SlpName "
      Consulta &= "AND T0.cardcode COLLATE SQL_Latin1_General_CP1_CI_AS = T1.CardCode "
      Consulta &= "AND T0.linea COLLATE SQL_Latin1_General_CP1_CI_AS = T1.ItmsGrpNam "
      Consulta &= "LEFT JOIN #BOArticulos T2 ON T0.ItemCode COLLATE SQL_Latin1_General_CP1_CI_AS = T2.Articulo "
      Consulta &= "AND T0.Slpname COLLATE SQL_Latin1_General_CP1_CI_AS = T2.Agente "
      Consulta &= "AND T0.cardcode COLLATE SQL_Latin1_General_CP1_CI_AS = T2.Id_Clte "
      Consulta &= "AND T0.linea COLLATE SQL_Latin1_General_CP1_CI_AS = T2.Linea "
      Consulta &= "GROUP BY T0.ItemCode,T0.Descripcion,T0.Slpname,T0.cardcode,T0.linea, "
      Consulta &= "Importe, TotalBO "


      'Consulta &= "--SELECT * FROM #BoVtasNClte ORDER BY cardname "

      '------ *************** CONSULTA ARTICULOS
      Consulta &= "CREATE TABLE #Arts( "
      Consulta &= "ItemName VARCHAR(200), "
      Consulta &= "ItemCode VARCHAR(100), "
      Consulta &= "vtasn DECIMAL(20,2), "
      Consulta &= "bo DECIMAL(20,2), "
      Consulta &= "total DECIMAL(20,2), "
      Consulta &= "VtaPerdida DECIMAL(20,2), "
      Consulta &= "VtaLograda DECIMAL(20,2), "
      Consulta &= "slpname VARCHAR(100), "
      Consulta &= "cardcode VARCHAR(100), "
      Consulta &= "Linea VARCHAR(120), "
      Consulta &= "Orden INT)  "

      Consulta &= "INSERT INTO #Arts(ItemCode,ItemName,vtasn,bo,total,VtaPerdida,VtaLograda,slpname,cardcode,Linea,Orden) "
      Consulta &= "SELECT ItemCode,ItemName,vtasn,bo,total,  "
      Consulta &= "case when bo = 0 OR total = 0 then 0 else bo/total*100 end AS 'VtaPerdida', "
      Consulta &= "CASE WHEN vtasn = 0 OR total = 0  THEN 0 ELSE vtasn/total*100 END AS 'VtaLograda', "
      Consulta &= "slpname, cardcode, Linea, 0 "
      Consulta &= "FROM #BoVtasNArticulos "
      'Consulta &= "ORDER BY slpname,Linea "
      Consulta &= "UNION ALL "
      'Consulta &= "INSERT INTO #Arts(ItemCode,ItemName,vtasn,bo,total,VtaPerdida,VtaLograda,slpname,cardcode,Linea,Orden) "
      Consulta &= "SELECT '','MONTOS TOTALES',SUM(vtasn),SUM(bo),SUM(total),  "
      Consulta &= "case when SUM(bo) = 0 then 0 else SUM(bo)/SUM(total)*100 end AS 'VtaPerdida', "
      Consulta &= "CASE WHEN SUM(vtasn) = 0 THEN 0 ELSE SUM(vtasn)/SUM(total)*100 END AS 'VtaLograda',"
      Consulta &= "slpname, cardcode, Linea, 1 "
      Consulta &= "FROM #BoVtasNArticulos "
      Consulta &= "GROUP BY slpname,cardcode,Linea "

      Consulta &= " UNION ALL"
      Consulta &= " SELECT ItemCode,ItemName,SUM(vtasn),SUM(bo),SUM(total),  "
      Consulta &= " case when SUM(bo) = 0 then 0 else SUM(bo)/SUM(total)*100 end AS 'VtaPerdida', "
      Consulta &= " CASE WHEN SUM(vtasn) = 0 THEN 0 ELSE SUM(vtasn)/SUM(total)*100 END AS 'VtaLograda',"
      Consulta &= " '' AS slpname,'' AS cardcode, Linea, 2 "
      Consulta &= " FROM #BoVtasNArticulos "
      Consulta &= " GROUP BY ItemCode,ItemName,Linea "

      Consulta &= " UNION ALL"
      Consulta &= " SELECT '**TOTALES**','MONTOS TOTALES ART.',SUM(vtasn),SUM(bo),SUM(total),  "
      Consulta &= " case when SUM(bo) = 0 then 0 else SUM(bo)/SUM(total)*100 end AS 'VtaPerdida', "
      Consulta &= " CASE WHEN SUM(vtasn) = 0 THEN 0 ELSE SUM(vtasn)/SUM(total)*100 END AS 'VtaLograda',"
      Consulta &= " 'TOTALES ARTICULO' AS slpname,'' AS cardcode,'LINEA', 3 "
      Consulta &= " FROM #BoVtasNArticulos "

      Consulta &= " UNION ALL"
      Consulta &= " SELECT ItemCode,ItemName,SUM(vtasn),SUM(bo),SUM(total),  "
      Consulta &= " case when SUM(bo) = 0 then 0 else SUM(bo)/SUM(total)*100 end AS 'VtaPerdida', "
      Consulta &= " CASE WHEN SUM(vtasn) = 0 THEN 0 ELSE SUM(vtasn)/SUM(total)*100 END AS 'VtaLograda',"
      Consulta &= " 'TOTALES LINEA' AS slpname,'' AS cardcode,Linea, 4 "
      Consulta &= " FROM #BoVtasNArticulos "
      Consulta &= " GROUP BY ItemCode,ItemName,Linea"

      Consulta &= " UNION ALL"
      Consulta &= " SELECT 'TOTAL','**MONTOS TOTALES**',SUM(vtasn),SUM(bo),SUM(total),  "
      Consulta &= " case when SUM(bo) = 0 then 0 else SUM(bo)/SUM(total)*100 end AS 'VtaPerdida', "
      Consulta &= " CASE WHEN SUM(vtasn) = 0 THEN 0 ELSE SUM(vtasn)/SUM(total)*100 END AS 'VtaLograda',"
      Consulta &= " 'TOTALES LINEA' AS slpname,'' AS cardcode,Linea, 5 "
      Consulta &= " FROM #BoVtasNArticulos "
      Consulta &= " GROUP BY Linea "

      Consulta &= " UNION ALL"
      Consulta &= " SELECT ItemCode,ItemName,SUM(vtasn),SUM(bo),SUM(total),  "
      Consulta &= " case when SUM(bo) = 0 then 0 else SUM(bo)/SUM(total)*100 end AS 'VtaPerdida', "
      Consulta &= " CASE WHEN SUM(vtasn) = 0 THEN 0 ELSE SUM(vtasn)/SUM(total)*100 END AS 'VtaLograda',"
      Consulta &= " slpname AS slpname,'' AS cardcode,Linea, 6"
      Consulta &= " FROM #BoVtasNArticulos "
      Consulta &= " GROUP BY ITEMCODE,ITEMNAME,slpname,Linea"

      Consulta &= " UNION ALL"
      Consulta &= " SELECT 'TOTALES','MONTOS TOTALES',SUM(vtasn),SUM(bo),SUM(total),  "
      Consulta &= " case when SUM(bo) = 0 then 0 else SUM(bo)/SUM(total)*100 end AS 'VtaPerdida', "
      Consulta &= " CASE WHEN SUM(vtasn) = 0 THEN 0 ELSE SUM(vtasn)/SUM(total)*100 END AS 'VtaLograda',"
      Consulta &= " slpname AS slpname,'' AS cardcode,'TOTAL LINEAS', 7"
      Consulta &= " FROM #BoVtasNArticulos "
      Consulta &= " GROUP BY slpname "

      '--PARA OBTENER TOTALES POR CLIENTE
      Consulta &= " UNION ALL"
      Consulta &= " SELECT '','MONTOS TOTALES CLIENTE',SUM(vtasn),SUM(bo),SUM(total),  "
      Consulta &= " case when SUM(bo) = 0 then 0 else SUM(bo)/SUM(total)*100 end AS 'VtaPerdida', "
      Consulta &= " CASE WHEN SUM(vtasn) = 0 THEN 0 ELSE SUM(vtasn)/SUM(total)*100 END AS 'VtaLograda',"
      Consulta &= " '' AS slpname,cardcode AS cardcode,'TOTAL CLIENTE', 8"
      Consulta &= " FROM #BoVtasNArticulos "
      Consulta &= " GROUP BY cardcode "

      '--PARA OBTENER TOTALES POR AGENTE-LINEA
      Consulta &= " UNION ALL"
      Consulta &= " SELECT '','MONTOS TOTALES AGTE-LINEA',SUM(vtasn),SUM(bo),SUM(total),  "
      Consulta &= " case when SUM(bo) = 0 then 0 else SUM(bo)/SUM(total)*100 end AS 'VtaPerdida', "
      Consulta &= " CASE WHEN SUM(vtasn) = 0 THEN 0 ELSE SUM(vtasn)/SUM(total)*100 END AS 'VtaLograda',"
      Consulta &= " slpname AS slpname,'' AS cardcode,Linea, 9"
      Consulta &= " FROM #BoVtasNArticulos "
      Consulta &= " GROUP BY slpname,Linea "

      Consulta &= "SELECT ItemCode as 'ItemCode',ItemName as 'ItemName',vtasn,bo,total,VtaPerdida/100 as 'vtaperdida',VtaLograda/100 as 'vtalograda', "
      Consulta &= "slpname as 'slpname',cardcode as 'cardcode',Linea as 'linea',Orden INTO #ArticulosFinal FROM #Arts  "

      Consulta &= "SELECT * FROM #ArticulosFinal ORDER BY ORDEN, Vtalograda,LINEA,ITEMCODE "
      'Consulta &= ""
      'ORDER BY linea, vtasn DESC

      '------ *****FIN CONSULTA ARTICULO


      '******************************************
      '**********FIN CODIGO AGREGADO*************
      '******************************************


      '--SE ELIMINAN LAS TABLAS TEMPORALES
      Consulta &= "DROP TABLE #TArtSol  "
      Consulta &= "DROP TABLE #TBackOrder "
      Consulta &= "DROP TABLE #TOrdVtaFact "
      Consulta &= "DROP TABLE #VtasNetas "
      Consulta &= "DROP TABLE #DetBackOrder "
      Consulta &= "DROP TABLE #AGTE "
      Consulta &= "DROP TABLE #BOAgente "
      Consulta &= "DROP TABLE #VtasAgente "
      Consulta &= "DROP TABLE #BoVtasNAgte "
      Consulta &= "DROP TABLE #Clte "
      Consulta &= "DROP TABLE #Clte2 "
      Consulta &= "DROP TABLE #BOCliente "
      Consulta &= "DROP TABLE #VtasCliente "
      Consulta &= "DROP TABLE #BoVtasNClte "
      Consulta &= "DROP TABLE #Linea "
      Consulta &= "DROP TABLE #BOLineas "
      Consulta &= "DROP TABLE #VtasLineas "
      Consulta &= "DROP TABLE #BoVtasNLineas "
      Consulta &= "DROP TABLE #Articulos "
      Consulta &= "DROP TABLE #BOArticulos "
      Consulta &= "DROP TABLE #VtasArticulos "
      Consulta &= "DROP TABLE #BoVtasNArticulos "

      Consulta &= "DROP TABLE #Agentes "
      Consulta &= "DROP TABLE #CLIENTES "
      Consulta &= "DROP TABLE #Lineas "
      Consulta &= "DROP TABLE #Arts "

      Consulta &= "DROP TABLE #AgentesFinal "
      Consulta &= "DROP TABLE #CLIENTESFinal "
      Consulta &= "DROP TABLE #LineasFinal "
      Consulta &= "DROP TABLE #ArticulosFinal "


      Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

      'CmdMObra.Parameters.Add("@FechaIni", SqlDbType.Date)
      'CmdMObra.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
      'CmdMObra.Parameters.Add("@FechaTer", SqlDbType.Date)
      'CmdMObra.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value
      CmdMObra.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime).Value = Me.DtpFechaIni.Value
      CmdMObra.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime).Value = Me.DtpFechaTer.Value

      If CmbAgteVta.SelectedValue <> 999 Then
        CmdMObra.Parameters.Add("@IdAgente", SqlDbType.Int)
        CmdMObra.Parameters("@IdAgente").Value = CmbAgteVta.SelectedValue
      End If

      If CmbCliente.SelectedValue <> "TODOS" Then
        CmdMObra.Parameters.Add("@IdCliente", SqlDbType.Char)
        CmdMObra.Parameters("@IdCliente").Value = CmbCliente.SelectedValue
      End If

      If CmbGrupoArticulo.SelectedValue <> 999 Then
        CmdMObra.Parameters.Add("@GrupoArt", SqlDbType.Int)
        CmdMObra.Parameters("@GrupoArt").Value = CmbGrupoArticulo.SelectedValue
      End If


      If CmbArticulo.SelectedValue <> "TODOS" Then
        CmdMObra.Parameters.Add("@IdArticulo", SqlDbType.Char)
        CmdMObra.Parameters("@IdArticulo").Value = CmbArticulo.SelectedValue
      End If

      Dim DsVtasDet As New DataSet

      'DTMObra.TableName = "DetBO"

      DsVtasDet.Tables.Add(DTMObra)

      CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
      CmdMObra.Connection.Open()

      Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)

      AdapMObra.SelectCommand = CmdMObra
      AdapMObra.Fill(DsVtasDet, "BOVtas")

      DsVtasDet.Tables(1).TableName = "AgentesBO"
      DsVtasDet.Tables(2).TableName = "ClientesBO"
      DsVtasDet.Tables(3).TableName = "LineasBO"
      DsVtasDet.Tables(4).TableName = "ArticulosBO"

      Dim DtAgentes As New DataTable
      DtAgentes = DsVtasDet.Tables("AgentesBO")

      Dim DtClientes As New DataTable
      DtClientes = DsVtasDet.Tables("ClientesBO")

      Dim DtLineas As New DataTable
      DtLineas = DsVtasDet.Tables("LineasBO")

      Dim DtArticulos As New DataTable
      DtArticulos = DsVtasDet.Tables("ArticulosBO")


      'Me.DgVtaAgte.DataSource = DtAgentes

      'Me.DgClientes.DataSource = DtClientes

      'Me.DgLineas.DataSource = DtLineas

      'Me.DgArticulos.DataSource = DtArticulos

      DVdgagte.Table = DtAgentes
      DgVtaAgte.DataSource = DVdgagte

      DVdgclte.Table = DtClientes
      DgClientes.DataSource = DVdgclte

      DVdglin.Table = DtLineas
      DgLineas.DataSource = DVdglin

      DVdgart.Table = DtArticulos
      DgArticulos.DataSource = DVdgart

      'With Me.DGBO
      '    .DataSource = DvBO
      'End With

      DisenoGridAgente()
      DisenoGridCliente()
      DisenoGridLinea()
      DisenoGridArticulo()


      'filtra los clientes por agente seleccionado
      FiltraClientesAgte()

    Catch ex As Exception

    End Try
  End Sub

  Private Sub DisenoGridAgente()

    Try

      With Me.DgVtaAgte
        '.DataSource = DTMObra
        .ReadOnly = True
        'Color de Renglones en Grid
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .RowHeadersWidth = 20
        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        '.RowHeadersVisible = False
        '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        .MultiSelect = True
        .AllowUserToAddRows = False

        .Columns(0).HeaderText = "Código"
        .Columns(0).Width = 50
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(1).HeaderText = "Agente"
        .Columns(1).Width = 150
        '.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(2).HeaderText = "Ventas Totales"
        .Columns(2).Width = 90
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(3).HeaderText = "Back Order"
        .Columns(3).Width = 90
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(4).HeaderText = "Total ($)"
        .Columns(4).Width = 100
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(5).HeaderText = "Venta Perdida"
        .Columns(5).Width = 70
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(5).DefaultCellStyle.Format = "##0.#0 %"

        .Columns(6).HeaderText = "Venta Lograda"
        .Columns(6).Width = 70
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(6).DefaultCellStyle.Format = "##0.#0 %"

        .Columns(7).Visible = False

      End With

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Private Sub DisenoGridCliente()

    Try

      With Me.DgClientes
        '.DataSource = DTMObra
        .ReadOnly = True
        'Color de Renglones en Grid
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .RowHeadersWidth = 20
        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        '.RowHeadersVisible = False
        '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        .MultiSelect = True
        .AllowUserToAddRows = False

        .Columns(0).HeaderText = "Código"
        .Columns(0).Width = 50
        .Columns(0).Frozen = True
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(1).HeaderText = "Cliente"
        .Columns(1).Width = 150
        .Columns(1).Frozen = True
        '.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(2).HeaderText = "Ventas Totales"
        .Columns(2).Width = 90
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(3).HeaderText = "Back Order"
        .Columns(3).Width = 90
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(4).HeaderText = "Total ($)"
        .Columns(4).Width = 100
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(5).HeaderText = "Venta Perdida"
        .Columns(5).Width = 70
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(5).DefaultCellStyle.Format = "##0.#0 %"

        .Columns(6).HeaderText = "Venta Lograda"
        .Columns(6).Width = 70
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(6).DefaultCellStyle.Format = "##0.#0 %"

        .Columns(7).HeaderText = "Agente"
        .Columns(7).Width = 120

        .Columns(8).Visible = False

      End With

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub


  Private Sub DisenoGridLinea()

    Try

      With Me.DgLineas
        '.DataSource = DTMObra
        .ReadOnly = True
        'Color de Renglones en Grid
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .RowHeadersWidth = 35
        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        '.RowHeadersVisible = False
        '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        .MultiSelect = True
        .AllowUserToAddRows = False

        .Columns(0).HeaderText = "Línea"
        .Columns(0).Width = 90
        .Columns(0).Frozen = True
        '.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(1).HeaderText = "Ventas Totales"
        .Columns(1).Width = 90
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(1).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(2).HeaderText = "Back Order"
        .Columns(2).Width = 90
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(3).HeaderText = "Total ($)"
        .Columns(3).Width = 100
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(4).HeaderText = "Venta Perdida"
        .Columns(4).Width = 70
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(4).DefaultCellStyle.Format = "##0.#0 %"

        .Columns(5).HeaderText = "Venta Lograda"
        .Columns(5).Width = 70
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(5).DefaultCellStyle.Format = "##0.#0 %"

        .Columns(6).HeaderText = "Cliente"
        .Columns(6).Width = 70

        .Columns(7).HeaderText = "Nombre"
        .Columns(7).Width = 120

        .Columns(8).HeaderText = "Agente"
        .Columns(8).Width = 120

        .Columns(9).HeaderText = "Orden"
        .Columns(9).Visible = False

      End With

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub


  Private Sub DisenoGridArticulo()

    Try

      With Me.DgArticulos
        '.DataSource = DTMObra
        .ReadOnly = True
        'Color de Renglones en Grid
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .RowHeadersWidth = 35
        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        '.RowHeadersVisible = False
        '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        .MultiSelect = True
        .AllowUserToAddRows = False

        .Columns(0).HeaderText = "Código"
        .Columns(0).Width = 80
        .Columns(0).Frozen = True
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(1).HeaderText = "Artículo"
        .Columns(1).Width = 170
        .Columns(1).Frozen = True
        '.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(2).HeaderText = "Ventas Totales"
        .Columns(2).Width = 80
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(3).HeaderText = "Back Order"
        .Columns(3).Width = 80
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(4).HeaderText = "Total ($)"
        .Columns(4).Width = 80
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(5).HeaderText = "Venta Perdida"
        .Columns(5).Width = 65
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(5).DefaultCellStyle.Format = "##0.#0 %"

        .Columns(6).HeaderText = "Venta Lograda"
        .Columns(6).Width = 65
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(6).DefaultCellStyle.Format = "##0.#0 %"

        .Columns(7).HeaderText = "Agente"
        .Columns(7).Width = 120

        .Columns(8).HeaderText = "Cliente"
        .Columns(8).Width = 50

        .Columns(9).HeaderText = "Línea"
        .Columns(9).Width = 50

        .Columns(10).HeaderText = "Orden"
        .Columns(10).Visible = False

      End With

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Private Sub BuscaAgte()
    If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then
      DvAgte.RowFilter = String.Empty
      Me.CmbAgteVta.SelectedValue = 999
      Me.CmbAgteVta.DataSource = DvAgte

    Else
      DvAgte.RowFilter = String.Empty
      DvAgte.RowFilter = "GroupCode = " & Trim(Me.cmbAlmacen.SelectedValue.ToString) & " OR GroupCode = 999"
      Me.CmbAgteVta.SelectedValue = 999
    End If
  End Sub
  Private Sub cmbAlmacen_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbAlmacen.SelectionChangeCommitted
    BuscaAgte()
    BuscaClientes()
  End Sub

  Private Sub cmbAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlmacen.SelectedIndexChanged
    BuscaAgte()
    BuscaClientes()
  End Sub


  Private Sub FiltraClientesAgte()
    Try

      If DgVtaAgte.Item(0, DgVtaAgte.CurrentCell.RowIndex).Value = 0 Then
        DVdgclte.RowFilter = "CardCode <>'TOTAL' "
      Else
        DVdgclte.RowFilter = "slpname ='" & DgVtaAgte.Item(1, DgVtaAgte.CurrentRow.Index).Value.ToString & "'"      'OR slpname = '' "
      End If

    Catch ex As Exception
      'MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub FiltraLineasCli()
    Try

      If DgVtaAgte.Item(0, DgVtaAgte.CurrentCell.RowIndex).Value.ToString = 0 Then 'And 
        If DgClientes.Item(0, DgClientes.CurrentCell.RowIndex).Value.ToString = 999 Then
          DVdglin.RowFilter = "CardName = 'Montos Totales Lineas'"

        ElseIf DgClientes.Item(0, DgClientes.CurrentCell.RowIndex).Value.ToString = "TOTAL" Then
          DVdglin.RowFilter = "slpname ='" & DgClientes.Item(7, DgClientes.CurrentRow.Index).Value.ToString & "'"
        Else
          DVdglin.RowFilter = "slpname ='" & DgClientes.Item(7, DgClientes.CurrentRow.Index).Value.ToString & "' and " &
                "CardCode ='" & DgClientes.Item(0, DgClientes.CurrentRow.Index).Value.ToString & "'"
        End If

      Else
        If DgClientes.Item(0, DgClientes.CurrentCell.RowIndex).Value.ToString = "TOTAL" Then
          DVdglin.RowFilter = "cardname ='Montos Totales Agente' and slpname='" & DgClientes.Item(7, DgClientes.CurrentRow.Index).Value.ToString & "'"
        Else
          DVdglin.RowFilter = "CardCode ='" & DgClientes.Item(0, DgClientes.CurrentRow.Index).Value.ToString & "' and " &
                    "slpname ='" & DgClientes.Item(7, DgClientes.CurrentRow.Index).Value.ToString & "'"
        End If
        'ElseIf DgClientes.Item(0, DgClientes.CurrentCell.RowIndex).Value.ToString = 999 Then
        '    DVdglin.RowFilter = "slpname ='" & DgClientes.Item(7, DgClientes.CurrentRow.Index).Value.ToString & "'"

        'ElseIf DgClientes.Item(0, DgClientes.CurrentCell.RowIndex).Value.ToString = "TOTAL" Then
        '    DVdglin.RowFilter = "slpname ='" & DgClientes.Item(7, DgClientes.CurrentRow.Index).Value.ToString & "'"

        'Else

        '    DVdglin.RowFilter = "CardCode ='" & DgClientes.Item(0, DgClientes.CurrentRow.Index).Value.ToString & "' and " & _
        '    "slpname ='" & DgClientes.Item(7, DgClientes.CurrentRow.Index).Value.ToString & "'"

      End If

    Catch ex As Exception
      'MsgBox(ex.Message)
      Try

        DVdglin.RowFilter = "CardCode ='" & DgClientes.Item(0, DgClientes.CurrentRow.Index).Value.ToString & "' and " &
                "slpname ='" & DgClientes.Item(7, DgClientes.CurrentRow.Index).Value.ToString & "'"

      Catch ex2 As Exception

      End Try

    End Try
  End Sub

  Private Sub FiltraArticulosLinea()
    Try

      If DgLineas.Item(9, DgLineas.CurrentCell.RowIndex).Value.ToString = "1" Then
        DVdgart.RowFilter = "cardcode='" & DgLineas.Item(6, DgLineas.CurrentRow.Index).Value.ToString & "' and slpname='" & DgLineas.Item(8, DgLineas.CurrentCell.RowIndex).Value.ToString & "' "

      ElseIf DgLineas.Item(9, DgLineas.CurrentCell.RowIndex).Value.ToString = "0" Then
        DVdgart.RowFilter = "cardcode='" & DgLineas.Item(6, DgLineas.CurrentRow.Index).Value.ToString & "' and slpname='" & DgLineas.Item(8, DgLineas.CurrentCell.RowIndex).Value & "'  and linea='" & DgLineas.Item(0, DgLineas.CurrentCell.RowIndex).Value.ToString & "' "
      End If

      If DgVtaAgte.Item(0, DgVtaAgte.CurrentCell.RowIndex).Value.ToString = 0 Then
        If DgClientes.Item(0, DgClientes.CurrentCell.RowIndex).Value.ToString = "999" Then
          If DgLineas.Item(0, DgLineas.CurrentCell.RowIndex).Value.ToString = "**TOTALES**" Then
            DVdgart.RowFilter = "orden = '2' or orden='3'"

          ElseIf DgLineas.Item(0, DgLineas.CurrentCell.RowIndex).Value.ToString <> "**TOTALES**" And DgLineas.Item(7, DgLineas.CurrentCell.RowIndex).Value.ToString = "Montos Totales Lineas" Then
            DVdgart.RowFilter = "SlpName='TOTALES LINEA' and linea='" & DgLineas.Item(0, DgLineas.CurrentCell.RowIndex).Value & "'"

          End If

        Else
          If DgLineas.Item(9, DgLineas.CurrentCell.RowIndex).Value.ToString = "1" Then
            DVdgart.RowFilter = "cardcode='" & DgLineas.Item(6, DgLineas.CurrentRow.Index).Value.ToString & "' and slpname='" & DgLineas.Item(8, DgLineas.CurrentCell.RowIndex).Value.ToString & "' and " &
                        "itemname<>'MONTOS TOTALES' or (orden=8 AND cardcode='" & DgLineas.Item(6, DgLineas.CurrentRow.Index).Value.ToString & "') "

          ElseIf DgLineas.Item(9, DgLineas.CurrentCell.RowIndex).Value.ToString = "0" Then
            DVdgart.RowFilter = "cardcode='" & DgLineas.Item(6, DgLineas.CurrentRow.Index).Value.ToString & "' and slpname='" & DgLineas.Item(8, DgLineas.CurrentCell.RowIndex).Value & "'  and linea='" & DgLineas.Item(0, DgLineas.CurrentCell.RowIndex).Value.ToString & "' "
          End If

        End If

      Else
        If DgClientes.Item(0, DgClientes.CurrentCell.RowIndex).Value.ToString = "TOTAL" Then
          If DgLineas.Item(0, DgLineas.CurrentCell.RowIndex).Value.ToString = "**TOTALES**" Then
            DVdgart.RowFilter = "slpname = '" & DgLineas.Item(8, DgLineas.CurrentCell.RowIndex).Value & "' AND ORDEN =6 OR (" &
                            "slpname = '" & DgLineas.Item(8, DgLineas.CurrentCell.RowIndex).Value & "' AND ORDEN =7) "

          Else
            DVdgart.RowFilter = "SlpName='" & DgLineas.Item(8, DgLineas.CurrentCell.RowIndex).Value & "' and cardcode='" & DgLineas.Item(6, DgLineas.CurrentRow.Index).Value.ToString & "' and " &
                            "linea='" & DgLineas.Item(0, DgLineas.CurrentRow.Index).Value.ToString & "' or (SlpName='" & DgLineas.Item(8, DgLineas.CurrentCell.RowIndex).Value & "' and " &
                            "linea='" & DgLineas.Item(0, DgLineas.CurrentRow.Index).Value.ToString & "' and orden=9) "

          End If

        Else
          If DgLineas.Item(9, DgLineas.CurrentCell.RowIndex).Value.ToString = "1" Then
            DVdgart.RowFilter = "cardcode='" & DgLineas.Item(6, DgLineas.CurrentRow.Index).Value.ToString & "' and slpname='" & DgLineas.Item(8, DgLineas.CurrentCell.RowIndex).Value.ToString & "' and " &
                        "itemname<>'MONTOS TOTALES' or (orden=8 AND cardcode='" & DgLineas.Item(6, DgLineas.CurrentRow.Index).Value.ToString & "') "
          End If

        End If

      End If

    Catch ex As Exception
      'MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub BtnAgentes_Click(sender As Object, e As EventArgs) Handles BtnAgentes.Click
    Dim oExcel As Object
    Dim oBook As Object
    Dim oSheet As Object

    'Abrimos un nuevo libro
    oExcel = CreateObject("Excel.Application")
    oBook = oExcel.workbooks.add
    oSheet = oBook.worksheets(1)


    'COMBINAMOS CELDAS
    oSheet.Range("A1:E1").Merge(True)
    oSheet.Range("A2:E2").Merge(True)
    oSheet.Range("A3:E3").Merge(True)
    oSheet.Range("A4:E4").Merge(True)
    oSheet.Range("A5:E5").Merge(True)
    oSheet.Range("A6:E6").Merge(True)


    'DAR COLOR DE FONDO A CELDAS
    oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
    oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15
    oSheet.Range("A3:C3").INTERIOR.COLORINDEX = 15
    oSheet.Range("A4:C4").INTERIOR.COLORINDEX = 15
    oSheet.Range("A5:C5").INTERIOR.COLORINDEX = 15
    oSheet.Range("A6:C6").INTERIOR.COLORINDEX = 15

    oSheet.Range("A8").INTERIOR.COLORINDEX = 15
    oSheet.Range("B8").INTERIOR.COLORINDEX = 15
    oSheet.Range("C8").INTERIOR.COLORINDEX = 15
    oSheet.Range("D8").INTERIOR.COLORINDEX = 15
    oSheet.Range("E8").INTERIOR.COLORINDEX = 15
    oSheet.Range("F8").INTERIOR.COLORINDEX = 15
    oSheet.Range("G8").INTERIOR.COLORINDEX = 15


    'Declaramos el nombre de las columnas
    oSheet.range("A8").value = "Código"
    oSheet.range("B8").value = "Agente"
    oSheet.range("C8").value = "Ventas totales"
    oSheet.range("D8").value = "Back Order"
    oSheet.range("E8").value = "Total ($)"
    oSheet.range("F8").value = "Venta perdida"
    oSheet.range("G8").value = "Venta lograda"


    'DISEÑO DE EXCEL

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A8:I8").font.bold = True


    oExcel.worksheets("Hoja1").Columns("A").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("A").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("B").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("B").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("C").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("C").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("D").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("D").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("E").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("E").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("F").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("F").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("G").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("G").Font.Size = 8

    'Cambia el alto de celda 
    oSheet.range("A:G").RowHeight = 13

    'oSheet.range("A:V").HorizontalAlignment = xlCenter

    'TAMAÑO DE COLUMNAS
    oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 8
    oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 22
    oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 15
    oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 15
    oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 15
    oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 10

    oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "$ ###,###,###.##"
    oExcel.Worksheets("Hoja1").Columns("D").NumberFormat = "$ ###,###,###.##"
    oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "$ ###,###,###.##"

    oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "##.## %"
    oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "##.## %"



    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = Me.DgVtaAgte.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.DgVtaAgte.Item(0, fila_dt).Value
      Dim cel2 As String = Me.DgVtaAgte.Item(1, fila_dt).Value
      Dim cel3 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(2, fila_dt).Value), 0, Me.DgVtaAgte.Item(2, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(3, fila_dt).Value), 0, Me.DgVtaAgte.Item(3, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(4, fila_dt).Value), 0, Me.DgVtaAgte.Item(4, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(5, fila_dt).Value), 0, Me.DgVtaAgte.Item(5, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(6, fila_dt).Value), 0, Me.DgVtaAgte.Item(6, fila_dt).Value)


      fila_dt_excel = fila_dt + 9 'Renglón en donde se empieza a registrar el reporte

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel1
      oSheet.range("B" & fila_dt_excel).value = cel2
      oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
      oSheet.range("D" & fila_dt_excel).value = cel4
      oSheet.range("E" & fila_dt_excel).value = cel5
      oSheet.range("F" & fila_dt_excel).value = cel6
      oSheet.range("G" & fila_dt_excel).value = cel7

      If cel7 > 0.95 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 43
      ElseIf cel7 >= 0.9 And cel7 < 0.95 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 6
      ElseIf cel7 >= 0.8 And cel7 < 0.9 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 45
      ElseIf cel7 < 0.8 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 3
      End If

    Next


    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    'oSheet.columns("A:O").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Back order - Ventas (Agentes)"
    oSheet.range("A2").value = "Periodo del - " + DtpFechaIni.Text + " al " + DtpFechaTer.Text
    oSheet.range("A3").value = "Agente de Ventas - " + CmbAgteVta.Text
    oSheet.range("A4").value = "Cliente - " + CmbCliente.Text
    oSheet.range("A5").value = "Línea - " + CmbGrupoArticulo.Text
    oSheet.range("A6").value = "Artículo - " + CmbArticulo.Text

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub

  Private Sub BtnClientes_Click(sender As Object, e As EventArgs) Handles BtnClientes.Click
    Dim oExcel As Object
    Dim oBook As Object
    Dim oSheet As Object

    'Abrimos un nuevo libro
    oExcel = CreateObject("Excel.Application")
    oBook = oExcel.workbooks.add
    oSheet = oBook.worksheets(1)


    'COMBINAMOS CELDAS
    oSheet.Range("A1:C1").Merge(True)
    oSheet.Range("A2:C2").Merge(True)
    oSheet.Range("A3:C3").Merge(True)
    oSheet.Range("A4:C4").Merge(True)
    oSheet.Range("A5:C5").Merge(True)
    oSheet.Range("A6:C6").Merge(True)


    'DAR COLOR DE FONDO A CELDAS
    oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
    oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15
    oSheet.Range("A3:C3").INTERIOR.COLORINDEX = 15
    oSheet.Range("A4:C4").INTERIOR.COLORINDEX = 15
    oSheet.Range("A5:C5").INTERIOR.COLORINDEX = 15
    oSheet.Range("A6:C6").INTERIOR.COLORINDEX = 15

    oSheet.Range("A8").INTERIOR.COLORINDEX = 15
    oSheet.Range("B8").INTERIOR.COLORINDEX = 15
    oSheet.Range("C8").INTERIOR.COLORINDEX = 15
    oSheet.Range("D8").INTERIOR.COLORINDEX = 15
    oSheet.Range("E8").INTERIOR.COLORINDEX = 15
    oSheet.Range("F8").INTERIOR.COLORINDEX = 15
    oSheet.Range("G8").INTERIOR.COLORINDEX = 15
    oSheet.Range("H8").INTERIOR.COLORINDEX = 15

    'Declaramos el nombre de las columnas
    oSheet.range("A8").value = "Código"
    oSheet.range("B8").value = "Cliente"
    oSheet.range("C8").value = "Ventas totales"
    oSheet.range("D8").value = "Back Order"
    oSheet.range("E8").value = "Total ($)"
    oSheet.range("F8").value = "Venta perdida"
    oSheet.range("G8").value = "Venta lograda"
    oSheet.range("H8").value = "Agente"

    'DISEÑO DE EXCEL

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A8:H8").font.bold = True


    oExcel.worksheets("Hoja1").Columns("A").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("A").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("B").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("B").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("C").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("C").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("D").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("D").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("E").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("E").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("F").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("F").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("G").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("G").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("H").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("H").Font.Size = 8

    'Cambia el alto de celda 
    oSheet.range("A:G").RowHeight = 13

    'oSheet.range("A:V").HorizontalAlignment = xlCenter

    'TAMAÑO DE COLUMNAS
    oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 8
    oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 18
    oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 15

    oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "$ ###,###,###.##"
    oExcel.Worksheets("Hoja1").Columns("D").NumberFormat = "$ ###,###,###.##"
    oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "$ ###,###,###.##"

    oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "##.## %"
    oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "##.## %"



    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = Me.DgClientes.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.DgClientes.Item(0, fila_dt).Value
      Dim cel2 As String = Me.DgClientes.Item(1, fila_dt).Value
      Dim cel3 As String = IIf(IsDBNull(Me.DgClientes.Item(2, fila_dt).Value), 0, Me.DgClientes.Item(2, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgClientes.Item(3, fila_dt).Value), 0, Me.DgClientes.Item(3, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgClientes.Item(4, fila_dt).Value), 0, Me.DgClientes.Item(4, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgClientes.Item(5, fila_dt).Value), 0, Me.DgClientes.Item(5, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgClientes.Item(6, fila_dt).Value), 0, Me.DgClientes.Item(6, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgClientes.Item(7, fila_dt).Value), 0, Me.DgClientes.Item(7, fila_dt).Value)

      fila_dt_excel = fila_dt + 9 'Renglón en donde se empieza a registrar el reporte

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel1
      oSheet.range("B" & fila_dt_excel).value = cel2
      oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
      oSheet.range("D" & fila_dt_excel).value = cel4
      oSheet.range("E" & fila_dt_excel).value = cel5
      oSheet.range("F" & fila_dt_excel).value = cel6
      oSheet.range("G" & fila_dt_excel).value = cel7
      oSheet.range("H" & fila_dt_excel).value = cel8

      If cel7 > 0.95 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 43
      ElseIf cel7 >= 0.9 And cel7 < 0.95 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 6
      ElseIf cel7 >= 0.8 And cel7 < 0.9 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 45
      ElseIf cel7 < 0.8 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 3
      End If

    Next


    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    'oSheet.columns("A:O").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Back order - Ventas (Clientes)"
    oSheet.range("A2").value = "Periodo del - " + DtpFechaIni.Text + " al " + DtpFechaTer.Text
    oSheet.range("A3").value = "Agente de Ventas - " + CmbAgteVta.Text
    oSheet.range("A4").value = "Cliente - " + CmbCliente.Text
    oSheet.range("A5").value = "Línea - " + CmbGrupoArticulo.Text
    oSheet.range("A6").value = "Artículo - " + CmbArticulo.Text

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub

  Private Sub BtnLinea_Click(sender As Object, e As EventArgs) Handles BtnLinea.Click
    Dim oExcel As Object
    Dim oBook As Object
    Dim oSheet As Object

    'Abrimos un nuevo libro
    oExcel = CreateObject("Excel.Application")
    oBook = oExcel.workbooks.add
    oSheet = oBook.worksheets(1)


    'COMBINAMOS CELDAS
    oSheet.Range("A1:C1").Merge(True)
    oSheet.Range("A2:C2").Merge(True)
    oSheet.Range("A3:C3").Merge(True)
    oSheet.Range("A4:C4").Merge(True)
    oSheet.Range("A5:C5").Merge(True)
    oSheet.Range("A6:C6").Merge(True)


    'DAR COLOR DE FONDO A CELDAS
    oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
    oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15
    oSheet.Range("A3:C3").INTERIOR.COLORINDEX = 15
    oSheet.Range("A4:C4").INTERIOR.COLORINDEX = 15
    oSheet.Range("A5:C5").INTERIOR.COLORINDEX = 15
    oSheet.Range("A6:C6").INTERIOR.COLORINDEX = 15

    oSheet.Range("A8").INTERIOR.COLORINDEX = 15
    oSheet.Range("B8").INTERIOR.COLORINDEX = 15
    oSheet.Range("C8").INTERIOR.COLORINDEX = 15
    oSheet.Range("D8").INTERIOR.COLORINDEX = 15
    oSheet.Range("E8").INTERIOR.COLORINDEX = 15
    oSheet.Range("F8").INTERIOR.COLORINDEX = 15
    oSheet.Range("G8").INTERIOR.COLORINDEX = 15
    oSheet.Range("H8").INTERIOR.COLORINDEX = 15

    'Declaramos el nombre de las columnas
    oSheet.range("A8").value = "Línea"
    oSheet.range("B8").value = "Ventas totales"
    oSheet.range("C8").value = "Back Order"
    oSheet.range("D8").value = "Total ($)"
    oSheet.range("E8").value = "Venta perdida"
    oSheet.range("F8").value = "Venta lograda"
    oSheet.range("G8").value = "Cliente"
    oSheet.range("H8").value = "Agente"

    'DISEÑO DE EXCEL

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A8:H8").font.bold = True


    oExcel.worksheets("Hoja1").Columns("A").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("A").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("B").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("B").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("C").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("C").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("D").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("D").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("E").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("E").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("F").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("F").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("G").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("G").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("H").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("H").Font.Size = 8

    'Cambia el alto de celda 
    oSheet.range("A:H").RowHeight = 13

    'oSheet.range("A:V").HorizontalAlignment = xlCenter

    'TAMAÑO DE COLUMNAS
    oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 12
    oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 15
    oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 15
    oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 15

    oExcel.Worksheets("Hoja1").Columns("B").NumberFormat = "$ ###,###,###.##"
    oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "$ ###,###,###.##"
    oExcel.Worksheets("Hoja1").Columns("D").NumberFormat = "$ ###,###,###.##"

    oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "##.## %"
    oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "##.## %"



    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = Me.DgLineas.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.DgLineas.Item(0, fila_dt).Value
      Dim cel2 As String = Me.DgLineas.Item(1, fila_dt).Value
      Dim cel3 As String = IIf(IsDBNull(Me.DgLineas.Item(2, fila_dt).Value), 0, Me.DgLineas.Item(2, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgLineas.Item(3, fila_dt).Value), 0, Me.DgLineas.Item(3, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgLineas.Item(4, fila_dt).Value), 0, Me.DgLineas.Item(4, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgLineas.Item(5, fila_dt).Value), 0, Me.DgLineas.Item(5, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgLineas.Item(6, fila_dt).Value), 0, Me.DgLineas.Item(6, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgLineas.Item(7, fila_dt).Value), 0, Me.DgLineas.Item(7, fila_dt).Value)

      fila_dt_excel = fila_dt + 9 'Renglón en donde se empieza a registrar el reporte

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel1
      oSheet.range("B" & fila_dt_excel).value = cel2
      oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
      oSheet.range("D" & fila_dt_excel).value = cel4
      oSheet.range("E" & fila_dt_excel).value = cel5
      oSheet.range("F" & fila_dt_excel).value = cel6
      oSheet.range("G" & fila_dt_excel).value = cel7
      oSheet.range("H" & fila_dt_excel).value = cel8

      If cel6 > 0.95 Then
        oSheet.Range("F" & fila_dt_excel).INTERIOR.COLORINDEX = 43
      ElseIf cel6 >= 0.9 And cel5 < 0.95 Then
        oSheet.Range("F" & fila_dt_excel).INTERIOR.COLORINDEX = 6
      ElseIf cel6 >= 0.8 And cel5 < 0.9 Then
        oSheet.Range("F" & fila_dt_excel).INTERIOR.COLORINDEX = 45
      ElseIf cel6 < 0.8 Then
        oSheet.Range("F" & fila_dt_excel).INTERIOR.COLORINDEX = 3
      End If

    Next


    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    'oSheet.columns("A:O").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Back order - Ventas (Lineas)"
    oSheet.range("A2").value = "Periodo del - " + DtpFechaIni.Text + " al " + DtpFechaTer.Text
    oSheet.range("A3").value = "Agente de Ventas - " + CmbAgteVta.Text
    oSheet.range("A4").value = "Cliente - " + CmbCliente.Text
    oSheet.range("A5").value = "Línea - " + CmbGrupoArticulo.Text
    oSheet.range("A6").value = "Artículo - " + CmbArticulo.Text

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub

  Private Sub BtnArticulo_Click(sender As Object, e As EventArgs) Handles BtnArticulo.Click
    Dim oExcel As Object
    Dim oBook As Object
    Dim oSheet As Object

    'Abrimos un nuevo libro
    oExcel = CreateObject("Excel.Application")
    oBook = oExcel.workbooks.add
    oSheet = oBook.worksheets(1)


    'COMBINAMOS CELDAS
    oSheet.Range("A1:C1").Merge(True)
    oSheet.Range("A2:C2").Merge(True)
    oSheet.Range("A3:C3").Merge(True)
    oSheet.Range("A4:C4").Merge(True)
    oSheet.Range("A5:C5").Merge(True)
    oSheet.Range("A6:C6").Merge(True)


    'DAR COLOR DE FONDO A CELDAS
    oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
    oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15
    oSheet.Range("A3:C3").INTERIOR.COLORINDEX = 15
    oSheet.Range("A4:C4").INTERIOR.COLORINDEX = 15
    oSheet.Range("A5:C5").INTERIOR.COLORINDEX = 15
    oSheet.Range("A6:C6").INTERIOR.COLORINDEX = 15

    oSheet.Range("A8").INTERIOR.COLORINDEX = 15
    oSheet.Range("B8").INTERIOR.COLORINDEX = 15
    oSheet.Range("C8").INTERIOR.COLORINDEX = 15
    oSheet.Range("D8").INTERIOR.COLORINDEX = 15
    oSheet.Range("E8").INTERIOR.COLORINDEX = 15
    oSheet.Range("F8").INTERIOR.COLORINDEX = 15
    oSheet.Range("G8").INTERIOR.COLORINDEX = 15
    oSheet.Range("H8").INTERIOR.COLORINDEX = 15
    oSheet.Range("I8").INTERIOR.COLORINDEX = 15
    oSheet.Range("J8").INTERIOR.COLORINDEX = 15

    'Declaramos el nombre de las columnas
    oSheet.range("A8").value = "Artículo"
    oSheet.range("B8").value = "Descripción"
    oSheet.range("C8").value = "Ventas totales"
    oSheet.range("D8").value = "Back Order"
    oSheet.range("E8").value = "Total ($)"
    oSheet.range("F8").value = "Venta perdida"
    oSheet.range("G8").value = "Venta lograda"
    oSheet.range("H8").value = "Agente"
    oSheet.range("I8").value = "Cliente"
    oSheet.range("J8").value = "Línea"


    'DISEÑO DE EXCEL

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A8:J8").font.bold = True


    oExcel.worksheets("Hoja1").Columns("A").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("A").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("B").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("B").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("C").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("C").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("D").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("D").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("E").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("E").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("F").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("F").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("G").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("G").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("H").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("H").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("I").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("I").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("J").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("J").Font.Size = 8

    'Cambia el alto de celda 
    oSheet.range("A:J").RowHeight = 13

    'oSheet.range("A:V").HorizontalAlignment = xlCenter

    'TAMAÑO DE COLUMNAS
    oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 12
    oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 15
    oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 15
    oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 15
    oExcel.worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 15

    oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "$ ###,###,###.##"
    oExcel.Worksheets("Hoja1").Columns("D").NumberFormat = "$ ###,###,###.##"
    oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "$ ###,###,###.##"

    oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "##.## %"
    oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "##.## %"

    oExcel.worksheets("Hoja1").Columns("A").NumberFormat = "@"

    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = Me.DgArticulos.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.DgArticulos.Item(0, fila_dt).Value
      Dim cel2 As String = Me.DgArticulos.Item(1, fila_dt).Value
      Dim cel3 As String = IIf(IsDBNull(Me.DgArticulos.Item(2, fila_dt).Value), 0, Me.DgArticulos.Item(2, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgArticulos.Item(3, fila_dt).Value), 0, Me.DgArticulos.Item(3, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgArticulos.Item(4, fila_dt).Value), 0, Me.DgArticulos.Item(4, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgArticulos.Item(5, fila_dt).Value), 0, Me.DgArticulos.Item(5, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgArticulos.Item(6, fila_dt).Value), 0, Me.DgArticulos.Item(6, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgArticulos.Item(7, fila_dt).Value), 0, Me.DgArticulos.Item(7, fila_dt).Value)
      Dim cel9 As String = IIf(IsDBNull(Me.DgArticulos.Item(8, fila_dt).Value), 0, Me.DgArticulos.Item(8, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.DgArticulos.Item(9, fila_dt).Value), 0, Me.DgArticulos.Item(9, fila_dt).Value)

      fila_dt_excel = fila_dt + 9 'Renglón en donde se empieza a registrar el reporte

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel1
      oSheet.range("B" & fila_dt_excel).value = cel2
      oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
      oSheet.range("D" & fila_dt_excel).value = cel4
      oSheet.range("E" & fila_dt_excel).value = cel5
      oSheet.range("F" & fila_dt_excel).value = cel6
      oSheet.range("G" & fila_dt_excel).value = cel7
      oSheet.range("H" & fila_dt_excel).value = cel8
      oSheet.range("I" & fila_dt_excel).value = cel9
      oSheet.range("J" & fila_dt_excel).value = cel10

      If cel7 > 0.95 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 43
      ElseIf cel7 >= 0.9 And cel7 < 0.95 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 6
      ElseIf cel7 >= 0.8 And cel7 < 0.9 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 45
      ElseIf cel7 < 0.8 Then
        oSheet.Range("G" & fila_dt_excel).INTERIOR.COLORINDEX = 3
      End If

    Next



    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    'oSheet.columns("A:O").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Back order - Ventas (Artículos)"
    oSheet.range("A2").value = "Periodo del - " + DtpFechaIni.Text + " al " + DtpFechaTer.Text
    oSheet.range("A3").value = "Agente de Ventas - " + CmbAgteVta.Text
    oSheet.range("A4").value = "Cliente - " + CmbCliente.Text
    oSheet.range("A5").value = "Línea - " + CmbGrupoArticulo.Text
    oSheet.range("A6").value = "Artículo - " + CmbArticulo.Text

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub

  Private Sub DgVtaAgte_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DgVtaAgte.RowPrePaint
    Try

      For i = 0 To DgVtaAgte.RowCount - 1

        If DgVtaAgte.Item(6, i).Value >= 0.95 Then
          DgVtaAgte.Item(6, i).Style.BackColor = Color.YellowGreen

        ElseIf DgVtaAgte.Item(6, i).Value >= 0.9 And DgVtaAgte.Item(6, i).Value < 0.95 Then
          DgVtaAgte.Item(6, i).Style.BackColor = Color.Yellow

        ElseIf DgVtaAgte.Item(6, i).Value >= 0.8 And DgVtaAgte.Item(6, i).Value < 0.9 Then
          DgVtaAgte.Item(6, i).Style.BackColor = Color.Orange

        ElseIf DgVtaAgte.Item(6, i).Value < 0.8 Then
          DgVtaAgte.Item(6, i).Style.BackColor = Color.Red

        End If

      Next

    Catch ex As Exception

    End Try
  End Sub

  Private Sub CmbGrupoArticulo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbGrupoArticulo.SelectedIndexChanged
    BuscaArticulos()
  End Sub

  Private Sub DgLineas_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DgLineas.RowPrePaint
    Try

      For i = 0 To DgLineas.RowCount - 1

        If DgLineas.Item(5, i).Value >= 0.95 Then
          DgLineas.Item(5, i).Style.BackColor = Color.YellowGreen

        ElseIf DgLineas.Item(5, i).Value >= 0.9 And DgLineas.Item(5, i).Value < 0.95 Then
          DgLineas.Item(5, i).Style.BackColor = Color.Yellow

        ElseIf DgLineas.Item(5, i).Value >= 0.8 And DgLineas.Item(5, i).Value < 0.9 Then
          DgLineas.Item(5, i).Style.BackColor = Color.Orange

        ElseIf DgLineas.Item(5, i).Value < 0.8 Then
          DgLineas.Item(5, i).Style.BackColor = Color.Red

        End If

      Next

    Catch ex As Exception

    End Try
  End Sub

  Private Sub DgVtaAgte_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgVtaAgte.CurrentCellChanged
    FiltraClientesAgte()
  End Sub

  Private Sub DgClientes_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DgClientes.RowPrePaint
    Try

      For i = 0 To DgClientes.RowCount - 1

        If DgClientes.Item(6, i).Value >= 0.95 Then
          DgClientes.Item(6, i).Style.BackColor = Color.YellowGreen

        ElseIf DgClientes.Item(6, i).Value >= 0.9 And DgClientes.Item(6, i).Value < 0.95 Then
          DgClientes.Item(6, i).Style.BackColor = Color.Yellow

        ElseIf DgClientes.Item(6, i).Value >= 0.8 And DgClientes.Item(6, i).Value < 0.9 Then
          DgClientes.Item(6, i).Style.BackColor = Color.Orange

        ElseIf DgClientes.Item(6, i).Value < 0.8 Then
          DgClientes.Item(6, i).Style.BackColor = Color.Red

        End If

      Next


    Catch ex As Exception

    End Try
  End Sub

  Private Sub DgArticulos_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DgArticulos.RowPrePaint
    Try

      For i = 0 To DgArticulos.RowCount - 1

        If DgArticulos.Item(6, i).Value >= 0.95 Then
          DgArticulos.Item(6, i).Style.BackColor = Color.YellowGreen

        ElseIf DgArticulos.Item(6, i).Value >= 0.9 And DgArticulos.Item(6, i).Value < 0.95 Then
          DgArticulos.Item(6, i).Style.BackColor = Color.Yellow

        ElseIf DgArticulos.Item(6, i).Value >= 0.8 And DgArticulos.Item(6, i).Value < 0.9 Then
          DgArticulos.Item(6, i).Style.BackColor = Color.Orange

        ElseIf DgArticulos.Item(6, i).Value < 0.8 Then
          DgArticulos.Item(6, i).Style.BackColor = Color.Red

        End If

      Next

    Catch ex As Exception

    End Try
  End Sub

  Private Sub DgClientes_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgClientes.CurrentCellChanged
    FiltraLineasCli()
  End Sub

  Private Sub DgLineas_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgLineas.CurrentCellChanged
    FiltraArticulosLinea()
  End Sub
End Class

