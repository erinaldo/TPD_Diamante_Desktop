Imports System.Data.SqlClient

Public Class BackOrder
  Dim DvArticulo As New DataView
  Dim DvClte As New DataView
  Dim DvAgte As New DataView
  Dim DvBO As New DataView

  Public ColorRen As Integer
  Public Almacen As Integer

  Private Sub BackOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.Text = "Por Recuperar -- " & Me.Name.ToString & ".vb"
        'path_form.Text = Me.Name.ToString & ".vb"
        'If VEsAgente = 1 Then
        '    Me.WindowState = FormWindowState.Normal
        '    Me.GrdConProd.Width = 1047
        '    Me.GrdConProd.Height = 512
        '    Me.Size = New System.Drawing.Size(1065, 551)
        'End If

        If UsrTPM = "RROBLES" Or UsrTPM = "VVERGARA" _
        Or UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" _
        Or UsrTPM = "VENTAS5" Or UsrTPM = "ASTRIDY" Or UsrTPM = "VENTAS14" Or UsrTPM = "CSANTOS" _
        Or UsrTPM = "VENTAS8" Then
            Me.cmbAlmacen.Enabled = False

        ElseIf UsrTPM = "ACASTRO" Or UsrTPM = "JSANCHEZ" Or UsrTPM = "VENTAS5" Or UsrTPM = "RMERCADO" _
            Or UsrTPM = "ABAJIO" Or UsrTPM = "AVERACRUZ" Or UsrTPM = "AMERIDA" Or UsrTPM = "ATUXTLA" _
        Or UsrTPM = "VENTAS4" Or UsrTPM = "RJIMENEZ" Or UsrTPM = "LCEBALLOS" Or UsrTPM = "VENTAS9" Or UsrTPM = "ATABASCO" Then
            'MsgBox(UsrTPM)
            'MsgBox(vCodAgte)
            Me.CmbAgteVta.SelectedValue = vCodAgte
      Me.CmbAgteVta.Enabled = False
      Me.cmbAlmacen.Enabled = False
      BuscaClientes()
      Me.CmbCliente.Focus()
    End If


    Dim FchInicio As DateTime
    FchInicio = DateAdd(DateInterval.Month, -1, Date.Now)
    Me.DtpFechaIni.Value = Format(FchInicio, "dd/MM/yyyy")
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
            ' If UsrTPM = "MANAGER" Then
            '     ConsutaLista = "SELECT T0.slpcode,T0.slpname,  " +
            '             "CASE WHEN T1.GroupCode IS NULL THEN 104 ELSE T1.GroupCode END " +
            '             "AS 'GroupCode' FROM OSLP T0 " +
            ' "LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode " +
            ' "WHERE (T1.CbrGralAdicional = 'N' OR T0.SlpCode = -1)  and (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC') " +
            ' "ORDER BY slpname"
            ' ElseIf UsrTPM = "VENTAS8" Then
            '     ConsutaLista = "SELECT T0.slpcode,T0.slpname,  " +
            '            "CASE WHEN T1.GroupCode IS NULL THEN 104 ELSE T1.GroupCode END " +
            '            "AS 'GroupCode' FROM OSLP T0 " +
            '"LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode " +
            '"WHERE (T1.CbrGralAdicional = 'N' OR T0.SlpCode = -1)  and (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC') and " +
            ' "T0.SlpCode in (52,57,8)" +
            '"ORDER BY slpname"
            ' End If

            ConsutaLista = "SELECT T0.slpcode,T0.slpname,  " +
                    "CASE WHEN T1.GroupCode IS NULL THEN 104 ELSE T1.GroupCode END " +
                    "AS 'GroupCode' FROM OSLP T0 " +
        "LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode " +
        "WHERE (T1.CbrGralAdicional = 'N' OR T0.SlpCode = -1)  and (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC') " +
        "ORDER BY slpname"


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

            If UsrTPM = "ACASTRO" Or UsrTPM = "JSANCHEZ" Or UsrTPM = "RMERCADO" Or UsrTPM = "ABAJIO" Or UsrTPM = "ATUXTLA" _
                Or UsrTPM = "VENTAS9" Or UsrTPM = "VENTAS5" Or UsrTPM = "AVERACRUZ" _
            Or UsrTPM = "VENTAS4" Or UsrTPM = "RJIMENEZ" Or UsrTPM = "AMERIDA" Or UsrTPM = "LCEBALLOS" Or UsrTPM = "ATABASCO" Then
                'MsgBox(UsrTPM)
                'MsgBox(vCodAgte)
                Me.CmbAgteVta.SelectedValue = vCodAgte
                Me.CmbAgteVta.Enabled = False
                Me.cmbAlmacen.Enabled = False
                BuscaClientes()
                Me.CmbCliente.Focus()
                'Aqui se hizo el cambio
            Else
                Me.CmbAgteVta.SelectedValue = 999

            End If



            ''''---------------------------------

            'If UsrTPM = "MANAGER" Or UsrTPM = "VENTAS8" Then
            If UsrTPM = "MANAGER" Then
                ConsutaLista = "SELECT CardCode,CardName, SlpCode, GroupCode FROM OCRD WHERE CardType = 'C' ORDER BY CardName "
            ElseIf UsrTPM = "VENTAS8" Then
                ConsutaLista = "SELECT CardCode,CardName, SlpCode, GroupCode FROM OCRD WHERE CardType = 'C' AND SlpCode IN (52,57,8) ORDER BY CardName "

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

    Almacen = cmbAlmacen.SelectedValue

    Ejecutar_Consulta()

    DisenoGrid()

    DisenoGridDet()

    DisenoRecuperable()

  End Sub

  Private Sub BtnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExcel.Click
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


    oSheet.Range("A7").INTERIOR.COLORINDEX = 15
    oSheet.Range("B7").INTERIOR.COLORINDEX = 15
    oSheet.Range("C7").INTERIOR.COLORINDEX = 15
    oSheet.Range("D7").INTERIOR.COLORINDEX = 15
    oSheet.Range("E7").INTERIOR.COLORINDEX = 15
    oSheet.Range("F7").INTERIOR.COLORINDEX = 15
    oSheet.Range("G7").INTERIOR.COLORINDEX = 15
    oSheet.Range("H7").INTERIOR.COLORINDEX = 15
    oSheet.Range("I7").INTERIOR.COLORINDEX = 15
    oSheet.Range("J7").INTERIOR.COLORINDEX = 15
    oSheet.Range("K7").INTERIOR.COLORINDEX = 15
    oSheet.Range("L7").INTERIOR.COLORINDEX = 15
    oSheet.Range("M7").INTERIOR.COLORINDEX = 15
    oSheet.Range("N7").INTERIOR.COLORINDEX = 15
    oSheet.Range("O7").INTERIOR.COLORINDEX = 15
    oSheet.Range("P7").INTERIOR.COLORINDEX = 15
    oSheet.Range("Q7").INTERIOR.COLORINDEX = 15
    oSheet.Range("R7").INTERIOR.COLORINDEX = 15
    oSheet.Range("S7").INTERIOR.COLORINDEX = 15
    oSheet.Range("T7").INTERIOR.COLORINDEX = 15
    oSheet.Range("U7").INTERIOR.COLORINDEX = 15
    oSheet.Range("V7").INTERIOR.COLORINDEX = 15
    oSheet.Range("W7").INTERIOR.COLORINDEX = 15


    'Declaramos el nombre de las columnas
    oSheet.range("A7").value = "Almacen"
    oSheet.range("B7").value = "Clave"
    oSheet.range("C7").value = "Nombre Cliente"
    oSheet.range("D7").value = "Agente"
    oSheet.range("E7").value = "Ord.Vta."
    oSheet.range("F7").value = "Fecha"
    oSheet.range("G7").value = "Articulo"
    oSheet.range("H7").value = "Categoría"
    oSheet.range("I7").value = "Descripción"
    oSheet.range("J7").value = "Linea"
    oSheet.range("K7").value = "Pedido Cliente"
    oSheet.range("L7").value = "Facturado"
    oSheet.range("M7").value = "Back Order"

    If Almacen = "100" Or Almacen = 0 Then
      oSheet.range("N7").value = "Stock Puebla"
      oSheet.range("O7").value = "Stock Mérida"
      oSheet.range("P7").value = "Stock Tuxtla"

    ElseIf Almacen = "102" Then
      oSheet.range("N7").value = "Stock Mérida"
      oSheet.range("O7").value = "Stock Puebla"
      oSheet.range("P7").value = "Stock Tuxtla"

    ElseIf Almacen = "103" Then
      oSheet.range("N7").value = "Stock Tuxtla"
      oSheet.range("O7").value = "Stock Puebla"
      oSheet.range("P7").value = "Stock Mérida"
    End If




    oSheet.range("Q7").value = "$ Precio"
    oSheet.range("R7").value = "Lista"
    oSheet.range("S7").value = "$ Total BO"
    oSheet.range("T7").value = "Cantidad Sol. Prov."
    oSheet.range("U7").value = "Fecha Entrega Prov. Aprox."
    oSheet.range("V7").value = "Ord. de Compra"


    'DISEÑO DE EXCEL

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A7:V7").font.bold = True


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
    oExcel.worksheets("Hoja1").Columns("K").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("K").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("L").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("L").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("M").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("M").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("N").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("N").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("O").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("O").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("P").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("P").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("Q").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("Q").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("R").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("R").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("S").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("S").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("T").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("T").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("U").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("U").Font.Size = 8
    oExcel.worksheets("Hoja1").Columns("V").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
    oExcel.worksheets("Hoja1").Columns("V").Font.Size = 8


    'Cambia el alto de celda 
    oSheet.range("A:V").RowHeight = 13

    'oSheet.range("A:V").HorizontalAlignment = xlCenter

    'TAMAÑO DE COLUMNAS
    oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 12
    oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 18
    oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 16
    oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 12
    oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 13
    oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 5
    oExcel.worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 18
    oExcel.worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 12
    oExcel.worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 6
    oExcel.worksheets("Hoja1").Columns("L").EntireColumn.ColumnWidth = 6
    oExcel.worksheets("Hoja1").Columns("M").EntireColumn.ColumnWidth = 6
    oExcel.worksheets("Hoja1").Columns("N").EntireColumn.ColumnWidth = 6
    oExcel.worksheets("Hoja1").Columns("O").EntireColumn.ColumnWidth = 6
    oExcel.worksheets("Hoja1").Columns("P").EntireColumn.ColumnWidth = 6
    oExcel.worksheets("Hoja1").Columns("Q").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("R").EntireColumn.ColumnWidth = 5
    oExcel.worksheets("Hoja1").Columns("S").EntireColumn.ColumnWidth = 12
    oExcel.worksheets("Hoja1").Columns("T").EntireColumn.ColumnWidth = 7
    oExcel.worksheets("Hoja1").Columns("U").EntireColumn.ColumnWidth = 10
    oExcel.worksheets("Hoja1").Columns("V").EntireColumn.ColumnWidth = 7


    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = Me.GrdConProd.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.GrdConProd.Item(0, fila_dt).Value
      Dim cel2 As String = Me.GrdConProd.Item(1, fila_dt).Value
      Dim cel3 As String = IIf(IsDBNull(Me.GrdConProd.Item(2, fila_dt).Value), 0, Me.GrdConProd.Item(2, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.GrdConProd.Item(3, fila_dt).Value), 0, Me.GrdConProd.Item(3, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.GrdConProd.Item(4, fila_dt).Value), 0, Me.GrdConProd.Item(4, fila_dt).Value)
      Dim cel6 As Date = IIf(IsDBNull(Me.GrdConProd.Item(5, fila_dt).Value), "12/12/1999", Me.GrdConProd.Item(5, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.GrdConProd.Item(6, fila_dt).Value), 0, Me.GrdConProd.Item(6, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.GrdConProd.Item(7, fila_dt).Value), 0, Me.GrdConProd.Item(7, fila_dt).Value)

      Dim cel9 As String = IIf(IsDBNull(Me.GrdConProd.Item(8, fila_dt).Value), 0, Me.GrdConProd.Item(8, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.GrdConProd.Item(9, fila_dt).Value), 0, Me.GrdConProd.Item(9, fila_dt).Value)

      Dim cel11 As String = IIf(IsDBNull(Me.GrdConProd.Item(10, fila_dt).Value), 0, Me.GrdConProd.Item(10, fila_dt).Value)
      Dim cel12 As String = IIf(IsDBNull(Me.GrdConProd.Item(11, fila_dt).Value), 0, Me.GrdConProd.Item(11, fila_dt).Value)
      Dim cel13 As String = IIf(IsDBNull(Me.GrdConProd.Item(12, fila_dt).Value), 0, Me.GrdConProd.Item(12, fila_dt).Value)
      Dim cel14 As String = IIf(IsDBNull(Me.GrdConProd.Item(13, fila_dt).Value), 0, Me.GrdConProd.Item(13, fila_dt).Value)
      Dim cel15 As String = IIf(IsDBNull(Me.GrdConProd.Item(14, fila_dt).Value), 0, Me.GrdConProd.Item(14, fila_dt).Value)
      Dim cel16 As String = IIf(IsDBNull(Me.GrdConProd.Item(15, fila_dt).Value), 0, Me.GrdConProd.Item(15, fila_dt).Value)
      Dim cel17 As String = IIf(IsDBNull(Me.GrdConProd.Item(16, fila_dt).Value), 0, Me.GrdConProd.Item(16, fila_dt).Value)
      Dim cel18 As String = IIf(IsDBNull(Me.GrdConProd.Item(17, fila_dt).Value), 0, Me.GrdConProd.Item(17, fila_dt).Value)
      Dim cel19 As String = IIf(IsDBNull(Me.GrdConProd.Item(18, fila_dt).Value), 0, Me.GrdConProd.Item(18, fila_dt).Value)
      Dim cel20 As String = IIf(IsDBNull(Me.GrdConProd.Item(19, fila_dt).Value), 0, Me.GrdConProd.Item(19, fila_dt).Value)
      Dim cel21 As String = IIf(IsDBNull(Me.GrdConProd.Item(20, fila_dt).Value), "12/12/1999", Me.GrdConProd.Item(20, fila_dt).Value)
      'Dim cel21 As Date = IIf(IsDBNull(Me.GrdConProd.Item(20, fila_dt).Value), "12/12/1999", Date.Parse(Me.GrdConProd.Item(20, fila_dt).Value))
      Dim cel22 As String = IIf(IsDBNull(Me.GrdConProd.Item(21, fila_dt).Value), 0, Me.GrdConProd.Item(21, fila_dt).Value)

      fila_dt_excel = fila_dt + 8 'Renglón en donde se empieza a registrar el reporte

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel1
      oSheet.range("B" & fila_dt_excel).value = cel2
      oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
      oSheet.range("D" & fila_dt_excel).value = cel4
      oSheet.range("E" & fila_dt_excel).value = cel5
      oSheet.range("F" & fila_dt_excel).value = cel6
      oSheet.range("G" & fila_dt_excel).value = cel7
      oSheet.range("H" & fila_dt_excel).value = cel8


      oExcel.Worksheets("Hoja1").Columns("Q").NumberFormat = "$ ###,###,###.##"
      oExcel.Worksheets("Hoja1").Columns("S").NumberFormat = "$ ###,###,###.##"


      oSheet.range("I" & fila_dt_excel).value = cel9
      oSheet.range("J" & fila_dt_excel).value = cel10
      oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel11, 0)
      oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel12, 0)
      oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel13, 0)
      oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel14, 0)
      oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel15, 0)
      oSheet.range("P" & fila_dt_excel).value = FormatNumber(cel16, 0)
      oSheet.range("Q" & fila_dt_excel).value = FormatNumber(cel17, 2)
      oSheet.range("R" & fila_dt_excel).value = cel18
      oSheet.range("S" & fila_dt_excel).value = FormatNumber(cel19, 2)
      oSheet.range("T" & fila_dt_excel).value = cel20
      oSheet.range("U" & fila_dt_excel).value = cel21
      oSheet.range("V" & fila_dt_excel).value = cel22

    Next

    ''*******************************
    ''*******************************
    ''*******************************
    If Almacen = "100" Or Almacen = "0" Then

      For Con = 0 To GrdConProd.RowCount - 1
        If GrdConProd.Item(0, Con).Value = "PUEBLA" Then
          If GrdConProd.Item(13, Con).Value >= GrdConProd.Item(12, Con).Value Then
            oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.color = RGB(169, 208, 142)
          ElseIf GrdConProd.Item(13, Con).Value > 0 And GrdConProd.Item(13, Con).Value < GrdConProd.Item(12, 0).Value Then
            oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.COLORINDEX = 44
          End If
        ElseIf GrdConProd.Item(0, Con).Value = "MÉRIDA" Then
          If GrdConProd.Item(14, Con).Value >= GrdConProd.Item(12, Con).Value Then
            oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.color = RGB(169, 208, 142)
          ElseIf GrdConProd.Item(14, Con).Value > 0 And GrdConProd.Item(13, Con).Value < GrdConProd.Item(12, Con).Value Then
            oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.COLORINDEX = 44
          End If
        ElseIf GrdConProd.Item(0, Con).Value = "TUXTLA GTZ" Then
          If GrdConProd.Item(15, Con).Value >= GrdConProd.Item(12, Con).Value Then
            oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.color = RGB(169, 208, 142)
          ElseIf GrdConProd.Item(15, Con).Value > 0 And GrdConProd.Item(13, Con).Value < GrdConProd.Item(12, Con).Value Then
            oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.COLORINDEX = 44
          End If
        End If
      Next

    ElseIf Almacen = "102" Then

      For Con = 0 To GrdConProd.RowCount - 1
        'If GrdConProd.Item(0, Con).Value = "PUEBLA" Then
        If GrdConProd.Item(13, Con).Value >= GrdConProd.Item(12, Con).Value Then
          oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.color = RGB(169, 208, 142)
        ElseIf GrdConProd.Item(13, Con).Value > 0 And GrdConProd.Item(13, Con).Value < GrdConProd.Item(12, 0).Value Then
          oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.COLORINDEX = 44
        End If
        'ElseIf GrdConProd.Item(0, Con).Value = "MÉRIDA" Then
        'If GrdConProd.Item(14, Con).Value >= GrdConProd.Item(12, Con).Value Then
        '    oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.color = RGB(169, 208, 142)
        'ElseIf GrdConProd.Item(14, Con).Value > 0 And GrdConProd.Item(13, Con).Value < GrdConProd.Item(12, Con).Value Then
        '    oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.COLORINDEX = 44
        'End If
        'ElseIf GrdConProd.Item(0, Con).Value = "TUXTLA GTZ" Then
        'If GrdConProd.Item(15, Con).Value >= GrdConProd.Item(12, Con).Value Then
        '    oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.color = RGB(169, 208, 142)
        'ElseIf GrdConProd.Item(15, Con).Value > 0 And GrdConProd.Item(13, Con).Value < GrdConProd.Item(12, Con).Value Then
        '    oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.COLORINDEX = 44
        'End If
        'End If
      Next

    ElseIf Almacen = "103" Then
      For Con = 0 To GrdConProd.RowCount - 1
        'If GrdConProd.Item(0, Con).Value = "PUEBLA" Then
        If GrdConProd.Item(13, Con).Value >= GrdConProd.Item(12, Con).Value Then
          oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.color = RGB(169, 208, 142)
        ElseIf GrdConProd.Item(13, Con).Value > 0 And GrdConProd.Item(13, Con).Value < GrdConProd.Item(12, 0).Value Then
          oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.COLORINDEX = 44
        End If
      Next
    End If

    ''*******************************


    ''INICIO DATAGRID2
    'ENCABEZADOS DG2
    oSheet.range("A" & fila_dt_excel + 3).value = "Totales"
    oSheet.range("B" & fila_dt_excel + 3).value = "Importe ($)"
    oSheet.range("C" & fila_dt_excel + 3).value = "  (%)  "

    'COLOREAMOS LAS CELDAS
    oSheet.Range("A" & fila_dt_excel + 3).INTERIOR.COLORINDEX = 15
    oSheet.Range("B" & fila_dt_excel + 3).INTERIOR.COLORINDEX = 15
    oSheet.Range("C" & fila_dt_excel + 3).INTERIOR.COLORINDEX = 15

    'DAMOS FORMATO A CELDAS DE $ Y %
    oSheet.Range("B" & fila_dt_excel + 4).NumberFormat = "$ ###,###,###.##"
    oSheet.Range("B" & fila_dt_excel + 5).NumberFormat = "$ ###,###,###.##"
    oSheet.Range("B" & fila_dt_excel + 6).NumberFormat = "$ ###,###,###.##"


    oSheet.Range("C" & fila_dt_excel + 4).NumberFormat = "###.#0 %"
    oSheet.Range("C" & fila_dt_excel + 5).NumberFormat = "###.#0 %"
    oSheet.Range("C" & fila_dt_excel + 6).NumberFormat = "###.#0 %"

    'DAMOS VALOR A LAS CELDAS

    '******** Renglon 1
    oSheet.range("A" & fila_dt_excel + 4).value = DGBO.Item(0, 0).Value
    oSheet.range("B" & fila_dt_excel + 4).value = DGBO.Item(1, 0).Value

    Dim aux As Decimal

    If DGBO.Item(2, 0).Value Is DBNull.Value Then
      aux = 0
    Else
      aux = DGBO.Item(2, 0).Value / 100
    End If

    oSheet.range("C" & fila_dt_excel + 4).value = aux


    'Fin Renglon 1

    '******** Renglon 2
    oSheet.range("A" & fila_dt_excel + 5).value = DGBO.Item(0, 1).Value
    oSheet.range("B" & fila_dt_excel + 5).value = DGBO.Item(1, 1).Value

    If DGBO.Item(2, 1).Value >= 95 Then
      oSheet.Range("A" & fila_dt_excel + 5).INTERIOR.color = RGB(169, 208, 142)
      oSheet.Range("B" & fila_dt_excel + 5).INTERIOR.color = RGB(169, 208, 142)
      oSheet.Range("C" & fila_dt_excel + 5).INTERIOR.color = RGB(169, 208, 142)
    ElseIf DGBO.Item(2, 1).Value >= 90 And aux < 95 Then
      oSheet.Range("A" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 6
      oSheet.Range("B" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 6
      oSheet.Range("C" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 6
    ElseIf DGBO.Item(2, 1).Value >= 80 And aux < 90 Then
      oSheet.Range("A" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 44
      oSheet.Range("B" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 44
      oSheet.Range("C" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 44
    ElseIf DGBO.Item(2, 1).Value > 0 And aux < 80 Then
      oSheet.Range("A" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 3
      oSheet.Range("B" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 3
      oSheet.Range("C" & fila_dt_excel + 5).INTERIOR.COLORINDEX = 3
    End If

    Dim aux2 As Decimal

    If DGBO.Item(2, 1).Value Is DBNull.Value Then
      aux2 = 0
    Else
      aux2 = DGBO.Item(2, 1).Value / 100
    End If

    oSheet.range("C" & fila_dt_excel + 5).value = aux2

    'FinRenglon 2

    '******** Renglon 3
    oSheet.range("A" & fila_dt_excel + 6).value = DGBO.Item(0, 2).Value
    oSheet.range("B" & fila_dt_excel + 6).value = DGBO.Item(1, 2).Value

    Dim aux3 As Decimal

    If DGBO.Item(2, 2).Value Is DBNull.Value Then
      aux3 = 100
    Else
      aux3 = DGBO.Item(2, 2).Value * 100
    End If

    oSheet.range("C" & fila_dt_excel + 6).value = aux3
    'Fin Renglon 3


    Dim NRow As Integer = GrdConProd.RowCount - 1
    For i As Integer = 8 To NRow + 8
      oExcel.Worksheets("Hoja1").Cells.Item(i, 13).INTERIOR.COLORINDEX = 6     '6 YELLOW
    Next


    '''''''''''''''''''''''
    '**********************

    'COLOREAMOS LAS CELDAS
    oSheet.Range("A" & fila_dt_excel + 8).INTERIOR.COLORINDEX = 15
    oSheet.Range("B" & fila_dt_excel + 8).INTERIOR.COLORINDEX = 15
    oSheet.Range("C" & fila_dt_excel + 8).INTERIOR.COLORINDEX = 15
    oSheet.Range("D" & fila_dt_excel + 8).INTERIOR.COLORINDEX = 15
    oSheet.Range("E" & fila_dt_excel + 8).INTERIOR.COLORINDEX = 15
    oSheet.Range("F" & fila_dt_excel + 8).INTERIOR.COLORINDEX = 15

    ''LLENADO DATAGRID 3
    oSheet.Range("B" & fila_dt_excel + 9).INTERIOR.color = RGB(169, 208, 142)
    oSheet.Range("C" & fila_dt_excel + 9).INTERIOR.COLORINDEX = 44
    oSheet.Range("B" & fila_dt_excel + 10).INTERIOR.color = RGB(169, 208, 142)
    oSheet.Range("C" & fila_dt_excel + 10).INTERIOR.COLORINDEX = 44

    If Almacen = 0 Then
      oSheet.Range("B" & fila_dt_excel + 11).INTERIOR.color = RGB(169, 208, 142)
      oSheet.Range("C" & fila_dt_excel + 11).INTERIOR.COLORINDEX = 44
      oSheet.Range("B" & fila_dt_excel + 12).INTERIOR.color = RGB(169, 208, 142)
      oSheet.Range("C" & fila_dt_excel + 12).INTERIOR.COLORINDEX = 44
      oSheet.Range("B" & fila_dt_excel + 13).INTERIOR.color = RGB(169, 208, 142)
      oSheet.Range("C" & fila_dt_excel + 13).INTERIOR.COLORINDEX = 44
    End If

    oSheet.range("A" & fila_dt_excel + 8).value = "Almacen"
    oSheet.range("B" & fila_dt_excel + 8).value = "B.O. Recuperable"
    oSheet.range("C" & fila_dt_excel + 8).value = "B.O. Parc. Recup."
    oSheet.range("D" & fila_dt_excel + 8).value = "B.O. sin posib. de Recup."
    oSheet.range("E" & fila_dt_excel + 8).value = "Totales"
    oSheet.range("F" & fila_dt_excel + 8).value = "%"


    'Dim fila_dt As Integer = 0
    'Dim fila_dt_excel As Integer = 0
    'Dim tanto_porcentaje As String = ""
    'Dim marikona As Integer = 0

    'Dim total_reg As Integer = 0

    total_reg = Me.DGRecuperable.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.DGRecuperable.Item(0, fila_dt).Value
      Dim cel2 As String = Me.DGRecuperable.Item(1, fila_dt).Value
      Dim cel3 As String = IIf(IsDBNull(Me.DGRecuperable.Item(2, fila_dt).Value), 0, Me.DGRecuperable.Item(2, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DGRecuperable.Item(3, fila_dt).Value), 0, Me.DGRecuperable.Item(3, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DGRecuperable.Item(4, fila_dt).Value), 0, Me.DGRecuperable.Item(4, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DGRecuperable.Item(5, fila_dt).Value), 0, Me.DGRecuperable.Item(5, fila_dt).Value)
      'Dim cel7 As String = IIf(IsDBNull(Me.DGRecuperable.Item(6, fila_dt).Value), 0, Me.DGRecuperable.Item(6, fila_dt).Value)
      'Dim cel8 As String = IIf(IsDBNull(Me.DGRecuperable.Item(7, fila_dt).Value), 0, Me.DGRecuperable.Item(7, fila_dt).Value)



      'fila_dt_excel = fila_dt + 8 'Renglón en donde se empieza a registrar el reporte

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt + fila_dt_excel + 9).value = cel1
      oSheet.range("B" & fila_dt + fila_dt_excel + 9).value = FormatNumber(cel2, 2)
      oSheet.range("C" & fila_dt + fila_dt_excel + 9).value = FormatNumber(cel3, 2)
      oSheet.range("D" & fila_dt + fila_dt_excel + 9).value = FormatNumber(cel4, 2)
      oSheet.range("E" & fila_dt + fila_dt_excel + 9).value = FormatNumber(cel5, 2)
      oSheet.range("F" & fila_dt + fila_dt_excel + 9).value = FormatNumber(cel6, 2)
    Next
    '' ''' ''''''''''


    'Formato de texto para la primera columna CLAVE ART
    oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "@"

    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    'oSheet.columns("A:O").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Back Orders con periodo del - " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
    oSheet.range("A2").value = "Agente de Ventas - " + CmbAgteVta.Text
    oSheet.range("A3").value = "Cliente - " + CmbCliente.Text
    oSheet.range("A4").value = "Línea - " + CmbGrupoArticulo.Text
    oSheet.range("A5").value = "Artículo - " + CmbArticulo.Text

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing

    ''------------------

  End Sub

  Private Sub GrdConProd_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles GrdConProd.RowPrePaint

    GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Style.BackColor = Color.Black
    GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Style.ForeColor = Color.White

    'If cmbAlmacen.SelectedValue = 0 Then

    If GrdConProd.Rows(e.RowIndex).Cells("WhsName").Value = "PUEBLA" Then

      If GrdConProd.Rows(e.RowIndex).Cells("StockPue").Value >= GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Value Then

        ColorRen = 35   'lightgreen

        GrdConProd.Rows(e.RowIndex).Cells("WhsName").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Id_Clte").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Nombre").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Agente").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Ord_Vta").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("FechaBO").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Articulo").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("cat").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Descripción").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Linea").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Solicitado").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Facturado").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("StockPue").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("StockMer").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("StockTux").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Precio").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Lista_Precio").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("TotalBO").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("SolProveed").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("FchEntrega").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.LightGreen


      ElseIf GrdConProd.Rows(e.RowIndex).Cells("StockPue").Value > 0 And GrdConProd.Rows(e.RowIndex).Cells("StockPue").Value < GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Value Then

        ColorRen = 44   'gold

        GrdConProd.Rows(e.RowIndex).Cells("WhsName").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Id_Clte").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Nombre").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Agente").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Ord_Vta").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("FechaBO").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Articulo").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("cat").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Descripción").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Linea").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Solicitado").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Facturado").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("StockPue").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("StockMer").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("StockTux").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Precio").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Lista_Precio").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("TotalBO").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("SolProveed").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("FchEntrega").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.Gold
      End If

    ElseIf GrdConProd.Rows(e.RowIndex).Cells("WhsName").Value = "MÉRIDA" Then

      If GrdConProd.Rows(e.RowIndex).Cells("StockMer").Value >= GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Value Then

        ColorRen = 35   'lightgreen

        GrdConProd.Rows(e.RowIndex).Cells("WhsName").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Id_Clte").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Nombre").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Agente").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Ord_Vta").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("FechaBO").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Articulo").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("cat").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Descripción").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Linea").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Solicitado").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Facturado").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("StockPue").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("StockMer").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("StockTux").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Precio").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Lista_Precio").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("TotalBO").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("SolProveed").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("FchEntrega").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.LightGreen


      ElseIf GrdConProd.Rows(e.RowIndex).Cells("StockMer").Value > 0 And GrdConProd.Rows(e.RowIndex).Cells("StockMer").Value < GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Value Then

        ColorRen = 44   'gold

        GrdConProd.Rows(e.RowIndex).Cells("WhsName").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Id_Clte").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Nombre").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Agente").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Ord_Vta").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("FechaBO").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Articulo").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("cat").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Descripción").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Linea").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Solicitado").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Facturado").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("StockPue").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("StockMer").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("StockTux").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Precio").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Lista_Precio").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("TotalBO").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("SolProveed").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("FchEntrega").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.Gold
      End If


    ElseIf GrdConProd.Rows(e.RowIndex).Cells("WhsName").Value = "TUXTLA GTZ" Then

      If GrdConProd.Rows(e.RowIndex).Cells("StockTux").Value >= GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Value Then

        ColorRen = 35   'lightgreen

        GrdConProd.Rows(e.RowIndex).Cells("WhsName").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Id_Clte").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Nombre").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Agente").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Ord_Vta").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("FechaBO").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Articulo").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("cat").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Descripción").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Linea").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Solicitado").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Facturado").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("StockPue").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("StockMer").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("StockTux").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Precio").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("Lista_Precio").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("TotalBO").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("SolProveed").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("FchEntrega").Style.BackColor = Color.LightGreen
        GrdConProd.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.LightGreen


      ElseIf GrdConProd.Rows(e.RowIndex).Cells("StockTux").Value > 0 And GrdConProd.Rows(e.RowIndex).Cells("StockTux").Value < GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Value Then

        ColorRen = 44   'gold

        GrdConProd.Rows(e.RowIndex).Cells("WhsName").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Id_Clte").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Nombre").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Agente").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Ord_Vta").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("FechaBO").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Articulo").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("cat").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Descripción").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Linea").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Solicitado").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Facturado").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("StockPue").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("StockMer").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("StockTux").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Precio").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("Lista_Precio").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("TotalBO").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("SolProveed").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("FchEntrega").Style.BackColor = Color.Gold
        GrdConProd.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.Gold
      End If

    End If     ''''CIERRA IF DE ALMACEN (PUEBLA, MER, TUXT)

    'Else


    'End If      '''''CIERRA IF ALMACEN <> TODOS

  End Sub

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
  Private Sub CmbCliente_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCliente.SelectedIndexChanged
    'mDatosCliente(CmbCliente.SelectedValue.ToString)
  End Sub

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
    If CmbGrupoArticulo.SelectedValue Is Nothing Or CmbGrupoArticulo.SelectedValue = 999 Then
      DvArticulo.RowFilter = String.Empty
      CmbArticulo.SelectedValue = "TODOS"

    Else
      DvArticulo.RowFilter = "ItmsGrpCod = " & Trim(Me.CmbGrupoArticulo.SelectedValue.ToString) & " OR ItmsGrpCod = 999"
      CmbArticulo.SelectedValue = "TODOS"
    End If
  End Sub

  Sub BuscaClientes()
    CmbCliente.SelectedValue = "TODOS"
    If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then
      'CkClientes.Enabled = True
      If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
        'CkClientes.Enabled = True
        DvClte.RowFilter = String.Empty
        'CmbCliente.SelectedValue = "TODOS"
      Else
        'CkClientes.Enabled = False
        DvClte.RowFilter = "SlpCode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString) & " OR SlpCode = 999"
        'CmbCliente.SelectedValue = "TODOS"
      End If

    Else
      'CkClientes.Enabled = False
      If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
        DvClte.RowFilter = "GroupCode = " & Trim(Me.cmbAlmacen.SelectedValue.ToString) & " OR groupcode = 999"
        CmbCliente.SelectedValue = "TODOS"
        If cmbAlmacen.SelectedValue.ToString = "100" Then
          'MsgBox("Escogiste puebla")
          'CkClientes.Enabled = True
        Else
          'CkClientes.Enabled = False
        End If
      Else
        'CkClientes.Enabled = False
        DvClte.RowFilter = "SlpCode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString) & " OR SlpCode = 999"
        'CkClientes.Enabled = False
        CmbCliente.SelectedValue = "TODOS"
      End If
    End If

  End Sub

  Private Sub Ejecutar_Consulta()

    GrdConProd.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES

    Dim Consulta As String = ""
    Dim strcadena As String = ""
    Dim CTabla As String = ""
    Dim DTMObra As New DataTable
    Dim DTProb As New DataTable
    Dim vAlm As Integer = 0

    TxtAlmacen.Text = cmbAlmacen.Text
    TxtAlmacen.Visible = True


    Consulta &= " SELECT T1.BaseEntry,MIN(T1.ActDelDate) AS FchFactBO " &
                    " INTO #TOrdVtaFact " &
                    " FROM RDR1 T0 " &
                    " INNER JOIN INV1 T1 ON T0.DocEntry = T1.BaseEntry " &
                    " and T0.LineStatus = 'O' " &
                    " group by T1.BaseEntry "

    'MODIFICO IVAN GONZALEZ PARA EL NUEVO PROCEDIMIENTO SIN AFECTAR EL OTRO
    Consulta &= " insert into #TOrdVtaFact " &
                    " SELECT T1.BaseEntry,MIN(T1.ActDelDate) AS FchFactBO " &
                    " FROM RDR1 T0 INNER JOIN DLN1 T1 ON T0.DocEntry = T1.BaseEntry and T0.LineStatus = 'O' " &
                    " group by T1.BaseEntry "

    '--CONSULTA DE ORDENES DE COMPRA DE PROVEEDOR
    '--PARTICIONA LOS REGISTRO A ENUMERAR POR ITEMCODE Y LOS ORDENA POR T0.DocDueDate
    '--ENUMERA CON UNO EL PEDIDO MAS RECIENTE DE CADA ARTICULO
    Consulta &= " SELECT  ROW_NUMBER() OVER(PARTITION BY T1.ItemCode,T1.WHSCODE ORDER BY T0.DocDueDate ASC) AS Enumera,T0.DocEntry,T0.DOCNUM,T1.ItemCode,T1.WhsCode,T1.OpenQty,T0.DocDueDate " +
                            " INTO #TArtSol " +
                            " FROM OPOR T0  " +
                            " INNER JOIN POR1 T1 ON T0.DocEntry = T1.DocEntry " +
                            " WHERE LineStatus = 'O' "

    Consulta &= "select distinct ItemCode, WhsCode "
    Consulta &= "into #todos_articulos  "
    Consulta &= "from #TArtSol "


    Consulta &= "CREATE TABLE #RF (ItemCode varchar(MAX), CadenaFechas varchar(MAX), CadenasDocNum varchar(MAX), Almacen varchar(10)) "
    Consulta &= "Declare @id int, "
    Consulta &= "@count int, "
    Consulta &= "@itemcode as varchar(MAX), "
    Consulta &= "@almacen as varchar(10), "
    Consulta &= "@id2 int, "
    Consulta &= "@count2 int, "
    Consulta &= "@cadena as varchar(MAX) = '', "
    Consulta &= "@cadena_docnum as varchar(MAX) = '' "
    Consulta &= "Set @id=1 "
    Consulta &= "select @count=count(*)from #todos_articulos  "
    Consulta &= "while @id<=@count "
    Consulta &= "begin "
    Consulta &= "set @itemcode = (select ItemCode from (select  *,RANK()OVER (ORDER BY itemcode, WhsCode ASC)AS RANK from #todos_articulos) as ji "
    Consulta &= "where rank=@id)  "
    Consulta &= "set @almacen = (select WhsCode from (select  *,RANK()OVER (ORDER BY itemcode, WhsCode ASC)AS RANK from #todos_articulos) as ji "
    Consulta &= "where rank=@id) "
    Consulta &= "select *  "
    Consulta &= "into #t_itemcode "
    Consulta &= "from #TArtSol where ItemCode = @itemcode and WhsCode = @almacen "
    Consulta &= "Set @id2=1 "
    Consulta &= "select @count2=count(*)from #t_itemcode  "
    Consulta &= "set @cadena = '' "
    Consulta &= "set @cadena_docnum = '' "
    Consulta &= "while @id2<=@count2 "
    Consulta &= "begin "
    Consulta &= "set @cadena = @cadena + ', ' + (select convert(varchar(16), DocDueDate, 105) from (select  *,RANK()OVER (ORDER BY Enumera ASC)AS RANK from #t_itemcode) as ji "
    Consulta &= "where rank=@id2) "
    Consulta &= "set @cadena_docnum = @cadena_docnum + ', ' + (select convert(varchar(16), DOCNUM) from (select  *,RANK()OVER (ORDER BY Enumera ASC)AS RANK from #t_itemcode) as ji "
    Consulta &= "where rank=@id2) "
    Consulta &= "set @id2=@id2+1 "
    Consulta &= "end "
    Consulta &= "insert into #RF "
    Consulta &= "select @itemcode, @cadena, @cadena_docnum, @almacen "
    Consulta &= "drop table #t_itemcode "
    Consulta &= "select @id=@id+1 "
    Consulta &= "end "




    '--CONSULTA LOS BACK ORDERS
    Consulta &= " SELECT T1.WhsCode Almacen, T0.CardCode AS Id_Clte,T0.CardName AS Nombre,T5.SlpName AS Agente,t0.DocEntry AS Ord_Vta,MAX(T6.FchFactBO) AS FechaBO,T1.ItemCode AS Articulo, " +
                            "      T1.Dscription AS Descripción,T4.ItmsGrpNam as Linea,T2.OnHand AS StockPue,T1.Quantity AS Solicitado,T1.Quantity - T1.OpenQty AS Facturado, " +
                            "      T1.OpenQty AS BackOrder,T8.OnHand AS StockMer,T9.OnHand AS StockTux,T1.Price AS Precio,T1.U_BXP_ListaP AS Lista_Precio,T1.OpenQty * T1.Price AS TotalBO, " +
                            "      (select SUM(OpenQty) from #TArtSol tt1 where tt1.ItemCode = T1.ItemCode) as SolProveed,   " +
                            "      SUBSTRING(T7.CadenaFechas,3, LEN(t7.CadenaFechas)) as FchEntrega, SUBSTRING(T7.CadenasDocNum,3,LEN(T7.CadenasDocNum)) as 'OrdCompra' " +
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
                            "      LEFT JOIN #RF T7 ON T7.ItemCode  COLLATE Modern_Spanish_CI_AI = T1.ItemCode AND T1.WhsCode  COLLATE Modern_Spanish_CI_AI = T7.Almacen  " +
                            "      LEFT JOIN OSLP T5 ON T0.SlpCode = T5.SlpCode " +
                            " WHERE T1.LineStatus = 'O'  "


    'AND T1.WhsCode = T7.WhsCode

    If CmbAgteVta.SelectedValue <> 999 Then
      Consulta &= " AND T0.SlpCode = @IdAgente"
      'MsgBox("no elegi todos")
    Else
      If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then
        If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
          'CkClientes.Enabled = True
          If CkClientes.Checked Then
            'MsgBox("Debo incluir ctes propios")
          Else
            'MsgBox("No debo incluir ctes propios")
            Consulta &= " and T0.SlpCode <> 1 "
          End If
        Else
          'CkClientes.Enabled = False
        End If

      Else
        If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
          If cmbAlmacen.SelectedValue.ToString = "100" Then
            'CkClientes.Enabled = True
            If CkClientes.Checked Then
              'MsgBox("Debo incluir ctes propios")
            Else
              'MsgBox("No debo incluir ctes propios")
              Consulta &= " and T0.SlpCode <> 1 "
            End If
          Else
            'CkClientes.Enabled = False
          End If
        Else
          'CkClientes.Enabled = False
        End If
      End If
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
    Consulta &= " GROUP BY T1.WhsCode, T0.CardCode,T0.CardName,T5.SlpName,t0.DocEntry, " +
                " T1.ItemCode, T1.Dscription ,T4.ItmsGrpNam ,T2.OnHand ,T1.Quantity ,T1.Quantity - T1.OpenQty, " +
                " T1.OpenQty ,T8.OnHand ,T9.OnHand ,T1.Price ,T1.U_BXP_ListaP ,T1.OpenQty * T1.Price, T7.CadenaFechas, T7.CadenasDocNum "


    Consulta &= "DECLARE @FINAL TABLE(WhsName varchar(50),Id_Clte varchar(20),Nombre varchar(150),Agente varchar(150),Ord_Vta int,FechaBO date,Articulo varchar(100), "
    Consulta &= "Descripción varchar(200), Linea varchar (150), Solicitado int,Facturado int,BackOrder int,StockPue int,StockMer int,StockTux int, "
    Consulta &= "Precio decimal(19,6),Lista_Precio int,TotalBO decimal(19,6), SolProveed varchar(20),FchEntrega varchar(300),OrdCompra varchar(300) "
    Consulta &= " )"

    '****************************************

    'CONSULTA FINAL
    Consulta &= "INSERT INTO @FINAL(WhsName,Id_Clte,Nombre,Agente,Ord_Vta,FechaBO,Articulo,Descripción,Linea, "
    Consulta &= "Solicitado,Facturado,BackOrder,StockPue,StockMer,StockTux,Precio,Lista_Precio,TotalBO,SolProveed,FchEntrega,OrdCompra)"
    Consulta &= "SELECT alm.WhsName,Id_Clte,Nombre,Agente,Ord_Vta,FechaBO,Articulo,Descripción,Linea,"
    Consulta &= " Solicitado,Facturado,BackOrder,StockPue,StockMer,StockTux,Precio,Lista_Precio,TotalBO,SolProveed,FchEntrega,OrdCompra "
    Consulta &= " FROM #TBackOrder TBO  "
    Consulta &= " inner join owhs alm on TBO.Almacen=alm.WhsCode"

    Consulta &= " UNION ALL "
    Consulta &= " SELECT '" + cmbAlmacen.Text + "'  AS Almacen,"
    Consulta &= " CAST('' AS NVARCHAR(15))AS Id_Clte,CAST('$$ Total Back Order' AS NVARCHAR(100)) AS Nombre,CAST('' AS NVARCHAR(155))AS Agente,CAST('0' AS INT) AS Ord_Vta,"
    Consulta &= " CAST('12/12/9999' AS DATE) AS Fecha,CAST('' AS NVARCHAR(20)) AS Articulo,"
    Consulta &= " CAST('$$ Total Back Order' AS NVARCHAR(100)) AS Descripción,CAST('' AS NVARCHAR(20)) as Linea,sum(CAST(Solicitado AS DECIMAL(19,6))) AS Solicitado,"
    Consulta &= " sum(CAST(Facturado AS DECIMAL(19,6)))AS Facturado,sum(CAST(BackOrder AS DECIMAL(19,6))) AS BackOrder, "
    Consulta &= " CAST(0 AS DECIMAL(19,6)) AS StockPue, CAST(0 AS DECIMAL(19,6)) AS StockMer, CAST(0 AS DECIMAL(19,6)) AS StockTux, CAST(0 AS DECIMAL(19,6)) AS Precio,"
    Consulta &= " CAST('' AS NVARCHAR(MAX)) AS Lista_Precio, "
    Consulta &= " SUM(TotalBO) AS TotalBO,sum(CAST(SolProveed AS DECIMAL(19,6))) AS SolProveed,CAST('' AS varchar) AS FchEntrega,CAST('0' AS varchar) AS OrdCompra"
    Consulta &= " FROM #TBackOrder "

    If cmbAlmacen.SelectedValue = "100" Or cmbAlmacen.Text = "TODOS" Then
      Consulta &= "SELECT  T0.WhsName, T0.Id_Clte, T0.Nombre, T0.Agente, T0.Ord_Vta, T0.FechaBO, T0.Articulo, T1.cat, T0.Descripción,T0.Linea,  "
      Consulta &= "T0.Solicitado, T0.Facturado, T0.BackOrder,T0.StockPue, T0.StockMer, T0.StockTux,T0.Precio,T0.Lista_Precio, T0.TotalBO, ROUND(T0.SolProveed,0) SolProveed, T0.FchEntrega, T0.OrdCompra "
      Consulta &= "FROM @FINAL T0 LEFT JOIN TPM.DBO.CATEGORIAS T1 ON T0.ARTICULO COLLATE Modern_Spanish_CI_AI = T1.ITEMCODE ORDER BY T0.FechaBO,T0.WhsName,Agente "

    ElseIf cmbAlmacen.SelectedValue = "102" Then

      Consulta &= "SELECT T0.WhsName, T0.Id_Clte, T0.Nombre, T0.Agente, T0.Ord_Vta, T0.FechaBO, T0.Articulo, T1.cat, T0.Descripción,T0.Linea,  "
      Consulta &= "T0.Solicitado, T0.Facturado, T0.BackOrder,T0.StockMer,T0.StockPue,T0.StockTux,T0.Precio,T0.Lista_Precio, T0.TotalBO, ROUND(T0.SolProveed,0) SolProveed, T0.FchEntrega, T0.OrdCompra "
      Consulta &= "FROM @FINAL T0 LEFT JOIN TPM.DBO.CATEGORIAS T1 ON T0.ARTICULO COLLATE Modern_Spanish_CI_AI = T1.ITEMCODE ORDER BY T0.FechaBO "

    ElseIf cmbAlmacen.SelectedValue = "103" Then

      Consulta &= "SELECT T0.WhsName, T0.Id_Clte, T0.Nombre, T0.Agente, T0.Ord_Vta, T0.FechaBO, T0.Articulo, T1.cat, T0.Descripción,T0.Linea,  "
      Consulta &= "T0.Solicitado, T0.Facturado, T0.BackOrder,T0.StockTux,T0.StockPue,T0.StockMer,T0.Precio,T0.Lista_Precio, T0.TotalBO, ROUND(T0.SolProveed,0) SolProveed, T0.FchEntrega, T0.OrdCompra "
      Consulta &= "FROM @FINAL T0 LEFT JOIN TPM.DBO.CATEGORIAS T1 ON T0.ARTICULO COLLATE Modern_Spanish_CI_AI = T1.ITEMCODE ORDER BY T0.FechaBO "

    End If


    '*******    NOTAS DE CREDITO
    Consulta &= "/*NOTAS DE CREDITO*/"
    Consulta &= "SELECT t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, "
    Consulta &= "CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal * -1 ELSE (T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100)*-1 END AS Importe, "
    Consulta &= "T0.ItemCode, T2.ItemName, T3.ItmsGrpCod, T3.ItmsGrpNam, T4.GroupCode, T5.GroupName, "
    Consulta &= "CASE when T6.DocDate <= '2017-12-31' then "
    Consulta &= "CASE when T6.DocType = 'I' then 1 else 0 end "
    Consulta &= "else "
    Consulta &= "CASE when T0.ItemCode <> 'DESCUENTO P.P' then 1 else 0 end "
    Consulta &= "end as 'bande' "
    Consulta &= "into #tmp_NC1 "
    Consulta &= "FROM [SBO_TPD].[dbo].[ORIN] T6	"
    Consulta &= "INNER JOIN [SBO_TPD].[dbo].[RIN1] T0 ON T0.DocEntry = t6.DocEntry "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
    Consulta &= "left join [SBO_TPD].[dbo].ECM2 t7 on t0.DocEntry = t7.SrcObjAbs and t7.SrcObjType = 14 "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
    Consulta &= "LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
    Consulta &= "WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
    Consulta &= "and T6.DocType <> 'S' "
    Consulta &= "and T2.ItmsGrpCod <> 200 "
    Consulta &= "and T7.ReportID is not null "
    Consulta &= "and T4.CbrGralAdicional = 'N' " 'AND T6.DocType = 'I' "

    '********************************************
    If CmbAgteVta.SelectedValue <> 999 Then
      Consulta &= " AND T6.SlpCode = @IdAgente "
    Else
      If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then
        If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
          'CkClientes.Enabled = True
          If CkClientes.Checked Then
            'MsgBox("Debo incluir ctes propios")
          Else
            'MsgBox("No debo incluir ctes propios")
            Consulta &= " and T6.SlpCode <> 1 "
          End If
        Else
          'CkClientes.Enabled = False
        End If

      Else
        If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
          If cmbAlmacen.SelectedValue.ToString = "100" Then
            'CkClientes.Enabled = True
            If CkClientes.Checked Then
              'MsgBox("Debo incluir ctes propios")
            Else
              'MsgBox("No debo incluir ctes propios")
              Consulta &= " and T6.SlpCode <> 1 "
            End If
          Else
            'CkClientes.Enabled = False
          End If
        Else
          'CkClientes.Enabled = False
        End If
      End If
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
      'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 100 )  "
      Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '01' union all select -1 ) "
    ElseIf cmbAlmacen.SelectedValue = "102" Then
      'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 102 )"
      Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '03' ) "
    ElseIf cmbAlmacen.SelectedValue = "103" Then
      'Consulta &= "AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 103 ) "
      Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '07' ) "
    End If
    '**********************************************

    '*******    CANCELADAS
    Consulta &= "SELECT t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, "
    Consulta &= "CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal * -1 ELSE (T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100)*-1 END AS Importe, "
    Consulta &= "T0.ItemCode, T2.ItemName, T3.ItmsGrpCod, T3.ItmsGrpNam, T4.GroupCode, T5.GroupName, "
    Consulta &= "CASE when T6.DocDate <= '2017-12-31' then "
    Consulta &= "CASE when T6.DocType = 'I' then 1 else 0 end "
    Consulta &= "else "
    Consulta &= "CASE when T0.ItemCode <> 'DESCUENTO P.P' then 1 else 0 end "
    Consulta &= "end as 'bande' "
    Consulta &= "into #tmp_Canc "
    Consulta &= "FROM [SBO_TPD].[dbo].[ORIN] T6	"
    Consulta &= "INNER JOIN [SBO_TPD].[dbo].[RIN1] T0 ON T0.DocEntry = t6.DocEntry "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
    Consulta &= "left join [SBO_TPD].[dbo].ECM2 t7 on t0.DocEntry = t7.SrcObjAbs and t7.SrcObjType = 14 "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
    Consulta &= "LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
    Consulta &= "WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
    Consulta &= "and T6.DocType <> 'S' "
    Consulta &= "and T2.ItmsGrpCod <> 200 "
    Consulta &= "and T7.ReportID is null "
    Consulta &= "and T4.CbrGralAdicional = 'N' " 'AND T6.DocType = 'I' "

    '********************************************
    If CmbAgteVta.SelectedValue <> 999 Then
      Consulta &= " AND T6.SlpCode = @IdAgente "
    Else
      If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then
        If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
          'CkClientes.Enabled = True
          If CkClientes.Checked Then
            'MsgBox("Debo incluir ctes propios")
          Else
            'MsgBox("No debo incluir ctes propios")
            Consulta &= " and T6.SlpCode <> 1 "
          End If
        Else
          'CkClientes.Enabled = False
        End If

      Else
        If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
          If cmbAlmacen.SelectedValue.ToString = "100" Then
            'CkClientes.Enabled = True
            If CkClientes.Checked Then
              'MsgBox("Debo incluir ctes propios")
            Else
              'MsgBox("No debo incluir ctes propios")
              Consulta &= " and T6.SlpCode <> 1 "
            End If
          Else
            'CkClientes.Enabled = False
          End If
        Else
          'CkClientes.Enabled = False
        End If
      End If
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
      'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 100 )  "
      Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '01' union all select -1 ) "
    ElseIf cmbAlmacen.SelectedValue = "102" Then
      'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 102 )"
      Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '03' ) "
    ElseIf cmbAlmacen.SelectedValue = "103" Then
      'Consulta &= "AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 103 ) "
      Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '07' ) "
    End If
    '********************************************** FIN CANCELADAS

    '******     FACTURAS
    Consulta &= "SELECT t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, "
    Consulta &= "CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal ELSE T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100 END AS Importe, "
    Consulta &= "T0.ItemCode, T2.ItemName, T3.ItmsGrpCod, T3.ItmsGrpNam, T4.GroupCode, T5.GroupName "
    Consulta &= "INTO #VtasNetas "
    Consulta &= "FROM [SBO_TPD].[dbo].[OINV] T6	"
    Consulta &= "INNER JOIN [SBO_TPD].[dbo].[INV1] T0 ON T0.DocEntry = t6.DocEntry "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
    Consulta &= "LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
    Consulta &= "LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode	"
    'Consulta &= "WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer AND T6.DocType <> 'S' AND T6.SERIES<>59  "
    Consulta &= "WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
    Consulta &= "AND T6.DocType <> 'S' and t2.ItmsGrpCod <> 200 "
    Consulta &= "and T4.CbrGralAdicional = 'N' "

    '********************************************
    If CmbAgteVta.SelectedValue <> 999 Then
      Consulta &= " AND T6.SlpCode = @IdAgente "
    Else
      If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then
        If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
          'CkClientes.Enabled = True
          If CkClientes.Checked Then
            'MsgBox("Debo incluir ctes propios")
          Else
            'MsgBox("No debo incluir ctes propios")
            Consulta &= " and T6.SlpCode <> 1 "
          End If
        Else
          'CkClientes.Enabled = False
        End If

      Else
        If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
          If cmbAlmacen.SelectedValue.ToString = "100" Then
            'CkClientes.Enabled = True
            If CkClientes.Checked Then
              'MsgBox("Debo incluir ctes propios")
            Else
              'MsgBox("No debo incluir ctes propios")
              Consulta &= " and T6.SlpCode <> 1 "
            End If
          Else
            'CkClientes.Enabled = False
          End If
        Else
          'CkClientes.Enabled = False
        End If
      End If
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
    Consulta &= "select DocNum, DocDate, SlpCode, SlpName, Importe, ItemCode, ItemName, ItmsGrpCod, ItmsGrpNam, GroupCode, GroupName from #tmp_NC1 where bande = 1 "
    Consulta &= "UNION ALL "
    Consulta &= "select DocNum, DocDate, SlpCode, SlpName, Importe, ItemCode, ItemName, ItmsGrpCod, ItmsGrpNam, GroupCode, GroupName from #tmp_Canc where bande = 1 "

    ''*******    NOTAS DE CREDITO
    'Consulta &= "SELECT t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, "
    'Consulta &= "CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal * -1 ELSE (T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100)*-1 END AS Importe, "
    'Consulta &= "T0.ItemCode, T2.ItemName, T3.ItmsGrpCod, T3.ItmsGrpNam, T4.GroupCode, T5.GroupName "
    'Consulta &= "FROM [SBO_TPD].[dbo].[ORIN] T6	"
    'Consulta &= "INNER JOIN [SBO_TPD].[dbo].[RIN1] T0 ON T0.DocEntry = t6.DocEntry "
    'Consulta &= "LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
    'Consulta &= "LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
    'Consulta &= "LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
    'Consulta &= "LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
    'Consulta &= "LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
    'Consulta &= "WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
    'Consulta &= "and T4.CbrGralAdicional = 'N' AND T6.DocType = 'I' "

    ''********************************************
    'If CmbAgteVta.SelectedValue <> 999 Then
    '    Consulta &= " AND T6.SlpCode = @IdAgente "
    'Else
    '    If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then
    '        If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
    '            'CkClientes.Enabled = True
    '            If CkClientes.Checked Then
    '                'MsgBox("Debo incluir ctes propios")
    '            Else
    '                'MsgBox("No debo incluir ctes propios")
    '                Consulta &= " and T6.SlpCode <> 1 "
    '            End If
    '        Else
    '            'CkClientes.Enabled = False
    '        End If

    '    Else
    '        If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
    '            If cmbAlmacen.SelectedValue.ToString = "100" Then
    '                'CkClientes.Enabled = True
    '                If CkClientes.Checked Then
    '                    'MsgBox("Debo incluir ctes propios")
    '                Else
    '                    'MsgBox("No debo incluir ctes propios")
    '                    Consulta &= " and T6.SlpCode <> 1 "
    '                End If
    '            Else
    '                'CkClientes.Enabled = False
    '            End If
    '        Else
    '            'CkClientes.Enabled = False
    '        End If
    '    End If
    'End If

    'If CmbCliente.SelectedValue <> "TODOS" Then
    '    Consulta &= " AND T6.CardCode = @IdCliente "
    'End If

    'If CmbGrupoArticulo.SelectedValue <> 999 Then
    '    Consulta &= " AND T3.ItmsGrpCod = @GrupoArt "
    'End If

    'If CmbArticulo.SelectedValue <> "TODOS" Then
    '    Consulta &= " AND T0.ItemCode = @IdArticulo "
    'End If

    'If cmbAlmacen.SelectedValue = "100" Then
    '    'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 100 )  "
    '    Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '01' union all select -1 ) "
    'ElseIf cmbAlmacen.SelectedValue = "102" Then
    '    'Consulta &= " AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 102 )"
    '    Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '03' ) "
    'ElseIf cmbAlmacen.SelectedValue = "103" Then
    '    'Consulta &= "AND T6.slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 103 ) "
    '    Consulta &= " AND T6.slpcode in (SELECT SlpCode FROM SBO_TPD.dbo.OSLP WHERE Memo = '07' ) "
    'End If
    ''**********************************************



    'Consulta &= ") tmp "
    'Consulta &= "GROUP BY CardCode, slpcode, DocDate "
    'Consulta &= "ORDER BY SlpCode,CardCode "

    Consulta &= "DECLARE @TotNtas DECIMAL(19,6) = (SELECT CASE WHEN SUM(Importe) IS NULL THEN 0 ELSE SUM(Importe) END AS Importe FROM #VtasNetas) "

    Consulta &= "DECLARE @TotBO DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END AS TotalBO FROM #TBackOrder) "

    Consulta &= "DECLARE @NetBO DECIMAL(19,6) = (SELECT @TotNtas + @TotBO ) "


    Consulta &= "CREATE TABLE #DetBackOrder( "
    Consulta &= "detalle varchar(50), "
    Consulta &= "monto decimal(19,6), "
    Consulta &= "porc decimal(19,6)) "

    Consulta &= "INSERT INTO #DetBackOrder(detalle,monto,PORC) "
    Consulta &= "SELECT 'Total Back Order', CASE WHEN @TotBO IS NULL THEN 0 ELSE @TotBO END, case when @TotBO = 0 OR @NetBO=0 then 0 else @TotBO/@NetBO*100 end AS PORC "
    Consulta &= "UNION ALL "
    Consulta &= "SELECT 'Total Ventas', CASE WHEN @TotNtas IS NULL THEN 0 ELSE @TotNtas END, case when @TotNtas = 0 OR @NetBO=0 then 0 else @TotNtas/@NetBO*100 end AS PORC "
    Consulta &= "UNION ALL "
    Consulta &= "SELECT '**MONTO TOTAL**', @TotBO + @TotNtas, 1 "

    Consulta &= "SELECT * FROM #DetBackOrder "



    'B.O. RECUPERABLE

    If cmbAlmacen.SelectedValue = 0 Then

      ' variables para obtener DETALLE DE BACK ORDERF

      'PUEBLA

      Consulta &= " DECLARE @BORecuP DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END "
      Consulta &= " FROM @FINAL WHERE StockPue >= BackOrder AND WHSNAME ='PUEBLA') "

      Consulta &= " DECLARE @BOParcRecuP DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END "
      Consulta &= " FROM @FINAL WHERE StockPue < BackOrder AND StockPue > 0 AND StockPue IS NOT NULL AND WHSNAME ='PUEBLA') "

      Consulta &= " DECLARE @BOSinRecupP DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END "
      Consulta &= " FROM @FINAL WHERE (StockPue = 0 OR StockPue IS NULL ) AND WHSNAME ='PUEBLA') "

      Consulta &= " DECLARE @TotAlmaP DECIMAL(19,6) = (SELECT @BORecuP + @BOParcRecuP + @BOSinRecupP) "

      'FIN PUEBLA

      'MERIDA

      Consulta &= " DECLARE @BORecuM DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END "
      Consulta &= " FROM @FINAL WHERE StockMer >= BackOrder AND WHSNAME ='MÉRIDA') "

      Consulta &= " DECLARE @BOParcRecuM DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END "
      Consulta &= " FROM @FINAL WHERE StockMer < BackOrder AND StockMer > 0 AND StockMer IS NOT NULL AND WHSNAME ='MÉRIDA') "

      Consulta &= " DECLARE @BOSinRecupM DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END "
      Consulta &= " FROM @FINAL WHERE (StockMer = 0 OR StockMer IS NULL ) AND WHSNAME ='MÉRIDA') "

      Consulta &= " DECLARE @TotAlmaM DECIMAL(19,6) = (SELECT @BORecuM + @BOParcRecuM + @BOSinRecupM) "

      'FIN MERIDA

      'TUXTLA

      Consulta &= " DECLARE @BORecuT DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END "
      Consulta &= " FROM @FINAL WHERE StockTux >= BackOrder AND WHSNAME ='TUXTLA GTZ') "

      Consulta &= " DECLARE @BOParcRecuT DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END "
      Consulta &= " FROM @FINAL WHERE StockTux < BackOrder AND StockTux > 0 AND StockTux IS NOT NULL AND WHSNAME ='TUXTLA GTZ') "

      Consulta &= " DECLARE @BOSinRecupT DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END"
      Consulta &= " FROM @FINAL WHERE (StockTux = 0 OR StockTux IS NULL ) AND WHSNAME ='TUXTLA GTZ') "

      Consulta &= " DECLARE @TotAlmaT DECIMAL(19,6) = (SELECT @BORecuT + @BOParcRecuT + @BOSinRecupT) "

      'FIN TUXTLA



      ''''VARIABLES DE TOTALES
      Consulta &= " DECLARE @BORecuTOTAL DECIMAL(19,6) =  (@BORecuP + @BORecuM + @BORecuT) "

      Consulta &= " DECLARE @BOParcRecuTOTAL DECIMAL(19,6) = (@BOParcRecuP + @BOParcRecuM + @BOParcRecuT) "

      Consulta &= " DECLARE @BOSinRecupTOTAL DECIMAL(19,6) =  (@BOSinRecupP + @BOSinRecupM + @BOSinRecupT) "

      Consulta &= " DECLARE @MontosTotales DECIMAL(19,6) = (@BORecuTOTAL + @BOParcRecuTOTAL + @BOSinRecupTOTAL) "

      ''''FIN VARIABLES DE TOTALES



      Consulta &= " SELECT 'PUEBLA',@BORecuP AS 'Recuperable', @BOParcRecuP AS 'Parcial', @BOSinRecupP,@TotAlmaP, "
      Consulta &= " CASE WHEN @TotAlmaP=0 OR @MontosTotales=0 THEN 0 ELSE @TotAlmaP/@MontosTotales*100 END "

      Consulta &= " UNION ALL "

      Consulta &= " SELECT 'MÉRIDA',@BORecuM AS 'Recuperable', @BOParcRecuM AS 'Parcial', @BOSinRecupM,@TotAlmaM, "
      Consulta &= " CASE WHEN @TotAlmaM=0 OR @MontosTotales=0 THEN 0 ELSE @TotAlmaM/@MontosTotales*100 END "

      Consulta &= " UNION ALL "

      Consulta &= " SELECT 'TUXTLA GTZ',@BORecuT AS 'Recuperable', @BOParcRecuT AS 'Parcial', @BOSinRecupT,@TotAlmaT, "
      Consulta &= " CASE WHEN @TotAlmaT=0 OR @MontosTotales=0 THEN 0 ELSE @TotAlmaT/@MontosTotales*100 END "


      Consulta &= " UNION ALL "

      Consulta &= " SELECT 'MONTOS TOTALES',@BORecuTOTAL AS 'Recuperable', @BOParcRecuTOTAL AS 'Parcial', @BOSinRecupTOTAL,@MontosTotales,100 "

      Consulta &= " UNION ALL "

      Consulta &= " SELECT '%',CASE WHEN @BORecuTOTAL=0 OR @MontosTotales=0 THEN 0 ELSE @BORecuTOTAL/@MontosTotales*100 END AS 'Recuperable', "
      Consulta &= " CASE WHEN @BOParcRecuTOTAL=0 OR @MontosTotales=0 THEN 0 ELSE @BOParcRecuTOTAL/@MontosTotales*100 END AS 'Parcial', "
      Consulta &= " CASE WHEN @BOSinRecupTOTAL=0 OR @MontosTotales=0 THEN 0 ELSE @BOSinRecupTOTAL/@MontosTotales*100 END,100,0"

    Else

      Consulta &= " DECLARE @BORecu DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END "

      Consulta &= " FROM @FINAL WHERE "

      If cmbAlmacen.Text = "Puebla" Then
        Consulta &= " StockPue >= BackOrder AND WHSNAME ='PUEBLA') "

      ElseIf cmbAlmacen.Text = "Mérida" Then
        Consulta &= " StockMer >= BackOrder AND WHSNAME ='MÉRIDA') "

      ElseIf cmbAlmacen.Text = "Tuxtla Gutierrez" Then
        Consulta &= " StockTux >= BackOrder AND WHSNAME ='TUXTLA GTZ') "

      End If

      'Consulta &= " AND WHSNAME ='" & cmbAlmacen.Text & "') "

      Consulta &= " DECLARE @BOParcRecu DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END "
      Consulta &= " FROM @FINAL WHERE "

      If cmbAlmacen.Text = "Puebla" Then
        Consulta &= " StockPue < BackOrder AND StockPue > 0 AND StockPue IS NOT NULL AND WHSNAME ='PUEBLA') "

      ElseIf cmbAlmacen.Text = "Mérida" Then
        Consulta &= " StockMer < BackOrder AND StockMer > 0 AND StockMer IS NOT NULL AND WHSNAME ='MÉRIDA') "

      ElseIf cmbAlmacen.Text = "Tuxtla Gutierrez" Then
        Consulta &= " StockTux < BackOrder AND StockTux > 0 AND StockTux IS NOT NULL AND WHSNAME ='TUXTLA GTZ') "

      End If

      ' '''''''''''''''

      Consulta &= " DECLARE @BOSinRecu DECIMAL(19,6) = (SELECT CASE WHEN SUM(TotalBO) IS NULL THEN 0 ELSE SUM(TotalBO) END "
      Consulta &= " FROM @FINAL WHERE "

      If cmbAlmacen.Text = "Puebla" Then
        Consulta &= " (StockPue = 0 OR StockPue IS NULL ) AND WHSNAME ='PUEBLA' AND FechaBO <> '99991212' ) "

      ElseIf cmbAlmacen.Text = "Mérida" Then
        Consulta &= " (StockMer = 0 OR StockMer IS NULL ) AND WHSNAME ='MÉRIDA' AND FechaBO <> '99991212' ) "     'AND FechaBO <> 'Mérida'

      ElseIf cmbAlmacen.Text = "Tuxtla Gutierrez" Then
        Consulta &= " (StockTux = 0 OR StockTux IS NULL ) AND WHSNAME ='TUXTLA GTZ' AND FechaBO <> '99991212') "

      End If

      ''(StockPue = 0 OR StockPue IS NULL ) AND WHSNAME ='PUEBLA') "

      Consulta &= " DECLARE @TotAlma DECIMAL(19,6) =  (@BORecu + @BOParcRecu + @BOSinRecu) "


      Consulta &= " SELECT '" & cmbAlmacen.Text & "' AS 'Almacen',@BORecu AS 'Recuperable', @BOParcRecu AS 'Parcial', @BOSinRecu AS 'SinRecu',"
      Consulta &= " @TotAlma as 'totalm',0 as 'porc' INTO #Recuperable "

      Consulta &= " UNION ALL "

      Consulta &= " SELECT '%' AS 'Almacen', CASE WHEN @BORecu=0 OR @TotAlma=0 THEN 0 ELSE @BORecu/@TotAlma*100 END AS 'Recuperable',  "
      Consulta &= " CASE WHEN @BOParcRecu=0 OR @TotAlma=0 THEN 0 ELSE @BOParcRecu/@TotAlma*100 END AS 'Parcial', "
      Consulta &= " CASE WHEN @BOSinRecu=0 OR @TotAlma=0 THEN 0 ELSE @BOSinRecu/@TotAlma*100 END AS 'SinRecu',100 as 'totalm',0 as 'porc'"



      Consulta &= " SELECT * FROM #Recuperable "

    End If

    '"SELECT T0.WhsName, T0.Id_Clte, T0.Nombre, T0.Agente, T0.Ord_Vta, T0.FechaBO, T0.Articulo, T1.cat, T0.Descripción,T0.Linea,  "
    'Consulta &= "T0.Solicitado, T0.Facturado, T0.BackOrder,T0.StockPue, T0.StockMer, T0.StockTux,T0.Precio,T0.Lista_Precio, T0.TotalBO, T0.SolProveed, T0.FchEntrega, T0.OrdCompra "
    'Consulta &= "FROM @FINAL T0 LEFT JOIN TPM.DBO.CATEGORIAS T1 ON T0.ARTICULO COLLATE Modern_Spanish_CI_AI = T1.ITEMCODE ORDER BY T0.FechaBO "

    '--SE ELIMINAN LAS TABLAS TEMPORALES
    Consulta &= " DROP TABLE #TArtSol  "
    Consulta &= " drop table #RF "
    Consulta &= " drop table #todos_articulos "

    Consulta &= " DROP TABLE #TBackOrder "
    Consulta &= " DROP TABLE #TOrdVtaFact "
    Consulta &= " DROP TABLE #VtasNetas "
    Consulta &= " DROP TABLE #DetBackOrder "
    Consulta &= " drop table #tmp_NC1 "


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
    AdapMObra.Fill(DsVtasDet, "BO")

    DsVtasDet.Tables(1).TableName = "BackOrder"
    DsVtasDet.Tables(2).TableName = "DetBO"
    DsVtasDet.Tables(3).TableName = "Recuperable"

    'datagridview 1
    'Se crea datatable y se asigna al datagridview
    Dim DtBackOrder As New DataTable
    DtBackOrder = DsVtasDet.Tables("BackOrder")
    Me.GrdConProd.DataSource = DtBackOrder

    'datagridview2
    'aqui se asigna al dataview el datatable
    DvBO.Table = DsVtasDet.Tables("DetBO")

    'despues se asigna el dataview al datatable
    With Me.DGBO
      .DataSource = DvBO
    End With

    'datagridview 3

    'Se cre datatable y se asigna al datagridview
    Dim DtRecuperable As New DataTable
    DtBackOrder = DsVtasDet.Tables("Recuperable")
    Me.DGRecuperable.DataSource = DtBackOrder

  End Sub

  Private Sub DisenoGrid()

    Try

      With Me.GrdConProd
        '.DataSource = DTMObra
        .ReadOnly = True
        'Color de Renglones en Grid
        '.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        '.DefaultCellStyle.BackColor = Color.AliceBlue
        '.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .RowHeadersWidth = 35
        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        '.RowHeadersVisible = False
        '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        .MultiSelect = True
        .AllowUserToAddRows = False
        If UsrTPM <> "MANAGER" Then
          'Color de linea del grid
          .Columns(0).HeaderText = "Almacen"
          .Columns(0).Width = 60

          .Columns(1).HeaderText = "Clave Cliente"
          .Columns(1).Width = 45

          .Columns(2).HeaderText = "Nombre Cliente"
          .Columns(2).Width = 127

          'If VEsAgente = 1 Then
          '.Columns(3).Visible = False
          'Else
          .Columns(3).HeaderText = "Agente"
          .Columns(3).Width = 70
          'End If

          .Columns(4).HeaderText = "Ord. Vta"
          .Columns(4).Width = 40

          .Columns(5).HeaderText = "Fecha"
          .Columns(5).Width = 70

          .Columns(6).HeaderText = "Articulo"
          .Columns(6).Width = 80
          .Columns(6).DefaultCellStyle.Format = "###.00"

          .Columns(7).HeaderText = "Categoría"
          .Columns(7).Width = 50
          .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

          .Columns(8).HeaderText = "Descripción"
          .Columns(8).Width = 140

          .Columns(9).HeaderText = "Línea"
          .Columns(9).Width = 80

          ''''''''''''''''''***********************
          '****************************************

          .Columns(10).HeaderText = "Pedido Cliente"
          .Columns(10).Width = 45
          .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(10).DefaultCellStyle.Format = "###,###,###"

          .Columns(11).HeaderText = "Factu- rado"
          .Columns(11).Width = 45
          .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(11).DefaultCellStyle.Format = "###,###,###"

          .Columns(12).HeaderText = "Back Order"
          .Columns(12).Width = 45
          .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(12).DefaultCellStyle.Format = "###,###,###"


          If cmbAlmacen.SelectedValue = "100" Or cmbAlmacen.Text = "TODOS" Then
            .Columns(13).HeaderText = "Stock Puebla"

            .Columns(14).HeaderText = "Stock Mérida"

            .Columns(15).HeaderText = "Stock Tuxtla"


          ElseIf cmbAlmacen.SelectedValue = "102" Then

            .Columns(13).HeaderText = "Stock Mérida"

            .Columns(14).HeaderText = "Stock Puebla"

            .Columns(15).HeaderText = "Stock Tuxtla"

          ElseIf cmbAlmacen.SelectedValue = "103" Then

            .Columns(13).HeaderText = "Stock Tuxtla"

            .Columns(14).HeaderText = "Stock Puebla"

            .Columns(15).HeaderText = "Stock Mérida"

          End If

          .Columns(13).Width = 45
          .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(13).DefaultCellStyle.Format = "###,###,###"

          .Columns(14).Width = 45
          .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(14).DefaultCellStyle.Format = "###,###,###"

          .Columns(15).Width = 45
          .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(15).DefaultCellStyle.Format = "###,###,###"

          .Columns(16).HeaderText = "$ Precio"
          .Columns(16).Width = 50
          .Columns(16).DefaultCellStyle.Format = "###,###,###.##"
          .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

          .Columns(17).HeaderText = "Lista Precio"
          .Columns(17).Width = 40
          .Columns(17).DefaultCellStyle.Format = "###"
          .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

          .Columns(18).HeaderText = "$ Total BO"
          .Columns(18).Width = 70
          .Columns(18).DefaultCellStyle.Format = "###,###,###.##"
          .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

          .Columns(19).HeaderText = "Pzas. Sol."
          .Columns(19).Width = 60
          .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(19).DefaultCellStyle.Format = "###,##0"

          .Columns(20).HeaderText = "Fecha Entrega"
          .Columns(20).Width = 150

          .Columns(21).HeaderText = "Ord. Compra"
          .Columns(21).Width = 90
        Else
          .Columns(0).HeaderText = "Almacen"
          .Columns(0).Width = 50

          .Columns(1).HeaderText = "Clave Cliente"
          .Columns(1).Width = 45

          .Columns(2).HeaderText = "Nombre Cliente"
          .Columns(2).Width = 253

          .Columns(3).HeaderText = "Agente"
          .Columns(3).Width = 140

          .Columns(4).HeaderText = "Ord. Vta"
          .Columns(4).Width = 40

          .Columns(5).HeaderText = "Fecha"
          .Columns(5).Width = 70

          .Columns(6).HeaderText = "Articulo"
          .Columns(6).Width = 110
          .Columns(6).DefaultCellStyle.Format = "###.00"

          .Columns(7).HeaderText = "Categoría"
          .Columns(7).Width = 45
          .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

          .Columns(8).HeaderText = "Descripción"
          .Columns(8).Width = 240

          .Columns(9).HeaderText = "Línea"
          .Columns(9).Width = 105

          .Columns(10).HeaderText = "Pedido Cliente"
          .Columns(10).Width = 45
          .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(10).DefaultCellStyle.Format = "###,###,###"

          .Columns(11).HeaderText = "Factu- rado"
          .Columns(11).Width = 45
          .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(11).DefaultCellStyle.Format = "###,###,###"

          .Columns(12).HeaderText = "Back Order"
          .Columns(12).Width = 45
          .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(12).DefaultCellStyle.Format = "###,###,###"

          If cmbAlmacen.SelectedValue = "100" Or cmbAlmacen.Text = "TODOS" Then
            .Columns(13).HeaderText = "Stock Puebla"

            .Columns(14).HeaderText = "Stock Mérida"

            .Columns(15).HeaderText = "Stock Tuxtla"

          ElseIf cmbAlmacen.SelectedValue = "102" Then

            .Columns(13).HeaderText = "Stock Mérida"

            .Columns(14).HeaderText = "Stock Puebla"

            .Columns(15).HeaderText = "Stock Tuxtla"

          ElseIf cmbAlmacen.SelectedValue = "103" Then

            .Columns(13).HeaderText = "Stock Tuxtla"

            .Columns(14).HeaderText = "Stock Puebla"

            .Columns(15).HeaderText = "Stock Mérida"

          End If


          .Columns(13).Width = 45
          .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(13).DefaultCellStyle.Format = "###,###,###"

          .Columns(14).Width = 45
          .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(14).DefaultCellStyle.Format = "###,###,###"

          .Columns(15).Width = 45
          .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(15).DefaultCellStyle.Format = "###,###,###"

          .Columns(16).HeaderText = "$ Precio"
          .Columns(16).Width = 50
          .Columns(16).DefaultCellStyle.Format = "###,###,###.##"
          .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

          .Columns(17).HeaderText = "Lista Precio"
          .Columns(17).Width = 40
          .Columns(17).DefaultCellStyle.Format = "###"
          .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

          .Columns(18).HeaderText = "$ Total BO"
          .Columns(18).Width = 80
          .Columns(18).DefaultCellStyle.Format = "###,###,###.##"
          .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

          .Columns(19).HeaderText = "Pzas Sol."
          .Columns(19).Width = 60
          .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
          .Columns(19).DefaultCellStyle.Format = "###,##0"

          .Columns(20).HeaderText = "Fecha Entrega"
          .Columns(20).Width = 150

          .Columns(21).HeaderText = "Ord. Compra"
          .Columns(21).Width = 90
        End If

      End With

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Private Sub DisenoGridDet()

    Try

      With Me.DGBO
        .ReadOnly = True
        'Color de Renglones en Grid
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .RowHeadersWidth = 35
        .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        '.RowHeadersVisible = False
        '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        .MultiSelect = True
        .AllowUserToAddRows = False

        .Columns(0).HeaderText = "Totales"
        .Columns(0).Width = 150

        .Columns(1).HeaderText = "Importe ($)"
        .Columns(1).Width = 120
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(1).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(2).HeaderText = "  (%)  "
        .Columns(2).Width = 100
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(2).DefaultCellStyle.Format = "##0.#0"


      End With

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub


  Private Sub DisenoRecuperable()
    Try

      With Me.DGRecuperable
        .ReadOnly = True
        ''Color de Renglones en Grid
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .RowHeadersWidth = 20
        .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        '.RowHeadersVisible = False
        '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        '.MultiSelect = True
        .AllowUserToAddRows = False

        .Columns(0).HeaderText = "Almacén"
        .Columns(0).Width = 125
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(1).HeaderText = "B.O. Recuperable"
        .Columns(1).Width = 110
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(1).DefaultCellStyle.Format = "###,###,##0.#0"

        .Columns(2).HeaderText = "B.O. Parcialmente Recuperable"
        .Columns(2).Width = 110
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(2).DefaultCellStyle.Format = "###,###,##0.#0"

        .Columns(3).HeaderText = "B.O. sin Posibilidad de Recuperar"
        .Columns(3).Width = 110
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(3).DefaultCellStyle.Format = "###,###,##0.#0"
        '.Columns(3).DefaultCellStyle.Format = "##0.#0 %"

        .Columns(4).HeaderText = "Totales"
        .Columns(4).Width = 110
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(4).DefaultCellStyle.Format = "###,###,##0.#0"

        .Columns(5).HeaderText = "%"
        .Columns(5).Width = 70
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(5).DefaultCellStyle.Format = "###.##"

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

  'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
  '    MsgBox(Me.DtpFechaIni.Value)
  '    MsgBox(Me.DtpFechaTer.Value)
  'End Sub

  Private Sub DGBO_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGBO.RowPrePaint
    If DGBO.Item(2, 1).Value >= 95 Then
      DGBO.Item(0, 1).Style.BackColor = Color.LimeGreen
      DGBO.Item(1, 1).Style.BackColor = Color.LimeGreen
      DGBO.Item(2, 1).Style.BackColor = Color.LimeGreen
    ElseIf DGBO.Item(2, 1).Value > 90 And DGBO.Item(2, 1).Value < 95 Then
      DGBO.Item(0, 1).Style.BackColor = Color.Yellow
      DGBO.Item(1, 1).Style.BackColor = Color.Yellow
      DGBO.Item(2, 1).Style.BackColor = Color.Yellow
    ElseIf DGBO.Item(2, 1).Value >= 80 And DGBO.Item(2, 1).Value < 90 Then
      DGBO.Item(0, 1).Style.BackColor = Color.Orange
      DGBO.Item(1, 1).Style.BackColor = Color.Orange
      DGBO.Item(2, 1).Style.BackColor = Color.Orange
    ElseIf DGBO.Item(2, 1).Value > 0 And DGBO.Item(2, 1).Value < 80 Then
      DGBO.Item(0, 1).Style.BackColor = Color.Red
      DGBO.Item(1, 1).Style.BackColor = Color.Red
      DGBO.Item(2, 1).Style.BackColor = Color.Red
    Else
      DGBO.Item(0, 1).Style.BackColor = Color.FloralWhite
      DGBO.Item(1, 1).Style.BackColor = Color.FloralWhite
      DGBO.Item(2, 1).Style.BackColor = Color.FloralWhite
    End If

  End Sub


  'Private Sub GrdConProd_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GrdConProd.RowPostPaint
  '    If cmbAlmacen.SelectedValue = 0 Then

  '    End If
  'End Sub

  Private Sub DGRecuperable_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGRecuperable.RowPrePaint

    DGRecuperable.Rows(e.RowIndex).Cells("Recuperable").Style.BackColor = Color.LightGreen
    DGRecuperable.Rows(e.RowIndex).Cells("Parcial").Style.BackColor = Color.Gold

  End Sub

  Private Sub CmbGrupoArticulo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbGrupoArticulo.SelectedIndexChanged

    Try

      If CmbGrupoArticulo.SelectedValue Is Nothing Or CmbGrupoArticulo.SelectedValue = 999 Then
        DvArticulo.RowFilter = String.Empty
        CmbArticulo.SelectedValue = "TODOS"

      Else
        DvArticulo.RowFilter = "ItmsGrpCod = " & Trim(Me.CmbGrupoArticulo.SelectedValue.ToString) & " OR ItmsGrpCod = 999"
        CmbArticulo.SelectedValue = "TODOS"
      End If

    Catch ex As Exception

    End Try

  End Sub

    Private Sub CmbAgteVta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAgteVta.SelectedIndexChanged
        'BuscaClientes()
    End Sub
End Class

