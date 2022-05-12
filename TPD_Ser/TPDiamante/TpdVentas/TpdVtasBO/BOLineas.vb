Public Class BOLineas
  Private dvArticulos As New DataView
  Private dvLineasCon As New DataView
  Private dvLineas As New DataView

  Public almacen As String

#Region "Eventos"
  Private Sub BOLineas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    'MsgBox(AlmTPM)

    Try
      DtpFechaIni.Value = Date.Now()
      DtpFechaTer.Value = Date.Now()


      Dim FchInicio As DateTime
      FchInicio = DateAdd(DateInterval.Month, -1, Date.Now)
      Me.DtpFechaIni.Value = Format(FchInicio, "dd/MM/yyyy")
      Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

      Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

        mCargaAlmacen(SqlConnection)
        mCargaAgente(SqlConnection)
        mCargaCliente(SqlConnection)
      End Using

    Catch ex As Exception

    End Try
  End Sub

  Private Sub bConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConsultar.Click
    Try

      DgRptBoLineasDet.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES

      Dim sError As String
      sError = String.Empty

      If fValidaDatos(sError) Then

        mConsultabBO()
      Else
        MsgBox("Verifique los siguientes campos: " + sError, MsgBoxStyle.Exclamation, "Tracto Partes Diamante")
      End If

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub DgRptBoLineas_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DgRptBoLineas.SelectionChanged
    Try
      If DgRptBoLineas.Item(1, DgRptBoLineas.CurrentRow.Index).Value.ToString <> " " And DgRptBoLineas.Item(1, DgRptBoLineas.CurrentRow.Index).Value.ToString <> "" Then
        dvArticulos.RowFilter = "Linea ='" & DgRptBoLineas.Item(1, DgRptBoLineas.CurrentRow.Index).Value.ToString & "' "
        If ckConsolidado.Checked = False Then
          dvArticulos.RowFilter &= " AND (Ciudad ='" & DgRptBoLineas.Item(0, DgRptBoLineas.CurrentRow.Index).Value.ToString & "' OR Ciudad = 'TODOSXLINE')"
        End If
      Else
        dvArticulos.RowFilter = "Linea like '%' AND Ciudad <> 'TODOSXLINE'"
        'dvArticulos.RowFilter = "Linea <> '' AND Descripcion = ''"
      End If

    Catch ex As Exception
    End Try

  End Sub

  Private Sub bExLinea_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bExLinea.Click
    Try
      Dim exApp As New Microsoft.Office.Interop.Excel.Application
      Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

      'Añadimos el Libro al programa
      exLibro = exApp.Workbooks.Add

      ' ¿Cuantas columnas y cuantas filas?
      Dim NCol As Integer = DgRptBoLineas.ColumnCount - 1
      Dim NRow As Integer = DgRptBoLineas.RowCount

      ''Combinamos celdas
      exLibro.Worksheets("Hoja1").Cells.Range("A1:h1").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("A2:c2").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("A3:c3").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("d2:h2").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("d3:h3").Merge(True)

      ''aplicamos un color de fondo ala celda o rango de celdas
      exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("d2").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("d3").Interior.ColorIndex = 15

      ''Cambiamos orientacion ala hola
      exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Reporte de Back Order Lineas Detalle"
      exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Del: " + DtpFechaIni.Value
      exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Al: " + DtpFechaTer.Value
      exLibro.Worksheets("Hoja1").Cells.Item(2, 4) = "Agente: " + CmbAgteVta.Text
      exLibro.Worksheets("Hoja1").Cells.Item(3, 4) = "Cliente: " + CmbCliente.Text


      exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(5, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(2, 4).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(3, 4).Font.Bold = 1



      'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
      For i As Integer = 1 To NCol
        exLibro.Worksheets("Hoja1").Cells.Item(5, i) = DgRptBoLineas.Columns(i - 1).Name.ToString
      Next

      For Fila As Integer = 0 To NRow - 1
        For Col As Integer = 0 To NCol - 1
          exLibro.Worksheets("Hoja1").Cells.Item(Fila + 6, Col + 1) = DgRptBoLineas.Rows(Fila).Cells(Col).Value
        Next
      Next

      exLibro.Worksheets("Hoja1").Columns(4).NumberFormat = "$ ###,###.00"
      exLibro.Worksheets("Hoja1").Columns(3).NumberFormat = "###,###"
      'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
      exLibro.Worksheets("Hoja1").Rows.Item(5).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Rows.Item(5).HorizontalAlignment = 3
      exLibro.Worksheets("Hoja1").Cells.Range("A5:D5").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Columns.AutoFit()
      exLibro.Worksheets("Hoja1").name = "Reporte de BO Lineas"

      'Aplicación visible
      exLibro.Worksheets.Application.Visible = True

      exLibro = Nothing
      exApp = Nothing

    Catch ex As Exception

    End Try
  End Sub

  Private Sub bExDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bExDetalle.Click
    Try
      Dim exApp As New Microsoft.Office.Interop.Excel.Application
      Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

      'Añadimos el Libro al programa
      exLibro = exApp.Workbooks.Add

      ' ¿Cuantas columnas y cuantas filas?
      Dim NCol As Integer = DgRptBoLineasDet.ColumnCount
      Dim NRow As Integer = DgRptBoLineasDet.RowCount

      ''Combinamos celdas
      exLibro.Worksheets("Hoja1").Cells.Range("A1:N1").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("A2:G2").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("A3:G3").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("h2:n2").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("h3:n3").Merge(True)

      ''aplicamos un color de fondo ala celda o rango de celdas
      exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("h2").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("h3").Interior.ColorIndex = 15

      ''Cambiamos orientacion ala hola
      exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Reporte de Back Order Lineas Detalle"
      exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Del: " + DtpFechaIni.Value
      exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Al: " + DtpFechaTer.Value
      exLibro.Worksheets("Hoja1").Cells.Item(2, 8) = "Agente: " + CmbAgteVta.Text
      exLibro.Worksheets("Hoja1").Cells.Item(3, 8) = "Cliente: " + CmbCliente.Text

      exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(5, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(2, 8).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(3, 8).Font.Bold = 1



      'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
      For i As Integer = 1 To NCol
        exLibro.Worksheets("Hoja1").Cells.Item(5, i) = DgRptBoLineasDet.Columns(i - 1).Name.ToString
      Next

      For Fila As Integer = 0 To NRow - 1
        For Col As Integer = 0 To NCol - 1
          exLibro.Worksheets("Hoja1").Cells.Item(Fila + 6, Col + 1) = DgRptBoLineasDet.Rows(Fila).Cells(Col).Value
        Next
      Next

      For i As Integer = 5 To NRow + 5
        exLibro.Worksheets("Hoja1").Cells.Item(i, 5).INTERIOR.COLORINDEX = 15     '6 GRIS CLARO
        exLibro.Worksheets("Hoja1").Cells.Item(i, 6).INTERIOR.COLORINDEX = 15     '6 GRIS CLARO
        exLibro.Worksheets("Hoja1").Cells.Item(i, 7).INTERIOR.COLORINDEX = 15     '6 GRIS CLARO
        exLibro.Worksheets("Hoja1").Cells.Item(i, 8).INTERIOR.COLORINDEX = 19     '6 YELLOW

        If almacen = "01" Then
          exLibro.Worksheets("Hoja1").Cells.Item(i, 9).INTERIOR.COLORINDEX = 37     'Azul
          exLibro.Worksheets("Hoja1").Cells.Item(i, 13).INTERIOR.COLORINDEX = 22    'Rojo
          exLibro.Worksheets("Hoja1").Cells.Item(i, 17).INTERIOR.COLORINDEX = 35    'Verde
        ElseIf almacen = "03" Then
          exLibro.Worksheets("Hoja1").Cells.Item(i, 9).INTERIOR.COLORINDEX = 22     'Rojo
          exLibro.Worksheets("Hoja1").Cells.Item(i, 13).INTERIOR.COLORINDEX = 37     'Azul
          exLibro.Worksheets("Hoja1").Cells.Item(i, 17).INTERIOR.COLORINDEX = 35     'Verde
        ElseIf almacen = "07" Then
          exLibro.Worksheets("Hoja1").Cells.Item(i, 9).INTERIOR.COLORINDEX = 35     'Verde
          exLibro.Worksheets("Hoja1").Cells.Item(i, 13).INTERIOR.COLORINDEX = 37     'Azul 
          exLibro.Worksheets("Hoja1").Cells.Item(i, 17).INTERIOR.COLORINDEX = 22     'Rojo
        End If

      Next



      ''*************
      'sCadena &= " SELECT alm.WhsName Ciudad, "
      'sCadena &= " Articulo, Descripción,Linea, sum(BackOrder)[Pzas BO], "
      'sCadena &= " [Stock Total], [Sol. Total], "
      'sCadena &= " sum([BO. puebla])+SUM([BO. merida])+SUM([BO. tuxtla]) [BO. total], "
      'sCadena &= " sum([BO.pueblaQ]) [BO.pueblaQ],sum([BO. puebla]) [BO. puebla], [Stock Puebla],[Sol. Puebla], "
      'sCadena &= " SUM([BO.meridaQ]) [BO.meridaQ],SUM([BO. merida]) [BO. merida], [Stock Merida],[Sol. Merida], "
      'sCadena &= " SUM([BO.tuxtlaQ]) [BO.tuxtlaQ],SUM([BO. tuxtla]) [BO. tuxtla], [Stock Tuxtla],[Sol. Tuxtla] "

      'sCadena &= " FROM #TBackOrder TBO "
      'sCadena &= " inner join owhs alm on TBO.Ciudad=alm.WhsCode "
      'sCadena &= " group by "
      ''*************

      exLibro.Worksheets("Hoja1").Columns(5).NumberFormat = "###,###"         'Pzas BO
      exLibro.Worksheets("Hoja1").Columns(6).NumberFormat = "###,###"         'StockTotal
      exLibro.Worksheets("Hoja1").Columns(7).NumberFormat = "###,###"         'Sol.Total
      exLibro.Worksheets("Hoja1").Columns(8).NumberFormat = "$ ###,###.#0"    'BO.Total

      exLibro.Worksheets("Hoja1").Columns(9).NumberFormat = "###,###"         'BO.PueblaCantidad
      exLibro.Worksheets("Hoja1").Columns(10).NumberFormat = "$ ###,###.#0"     'BO.Puebla$
      exLibro.Worksheets("Hoja1").Columns(11).NumberFormat = "###,###"         'StockPuebla
      exLibro.Worksheets("Hoja1").Columns(12).NumberFormat = "###,###"         'SolPuebla

      exLibro.Worksheets("Hoja1").Columns(13).NumberFormat = "###,###"        'BO.MeridaCantidad
      exLibro.Worksheets("Hoja1").Columns(14).NumberFormat = "$ ###,###.#0"   'BO.Merida$
      exLibro.Worksheets("Hoja1").Columns(15).NumberFormat = "###,###"        'StockMerida
      exLibro.Worksheets("Hoja1").Columns(16).NumberFormat = "###,###"        'SolMerida

      exLibro.Worksheets("Hoja1").Columns(17).NumberFormat = "###,###"        'BO.TuxtlaCantidad
      exLibro.Worksheets("Hoja1").Columns(18).NumberFormat = "$ ###,###.#0"   'BO.Tuxtla$
      exLibro.Worksheets("Hoja1").Columns(19).NumberFormat = "###,###"        'StockTuxtla
      exLibro.Worksheets("Hoja1").Columns(20).NumberFormat = "###,###"        'SolTuxtla

      'exLibro.Worksheets("Hoja1").Columns(21).NumberFormat = "###,###"
      'exLibro.Worksheets("Hoja1").Columns(22).NumberFormat = "###,###"
      'exLibro.Worksheets("Hoja1").Columns(23).NumberFormat = "$ ###,###.00"
      'exLibro.Worksheets("Hoja1").Columns(24).NumberFormat = "$ ###,###.00"
      'exLibro.Worksheets("Hoja1").Columns(25).NumberFormat = "$ ###,###.00"

      'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
      exLibro.Worksheets("Hoja1").Rows.Item(5).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Rows.Item(5).HorizontalAlignment = 3
      exLibro.Worksheets("Hoja1").Cells.Range("A5:T5").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Columns.AutoFit()
      exLibro.Worksheets("Hoja1").name = "Reporte de BO Lineas"

      'Aplicación visible
      exLibro.Worksheets.Application.Visible = True

      exLibro = Nothing
      exApp = Nothing

    Catch ex As Exception

    End Try
  End Sub

  Private Sub cmbAlmacen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAlmacen.SelectedIndexChanged
    If (cmbAlmacen.Text = "TODOS") Then
      ckConsolidado.Enabled = True
      ckConsolidado.Checked = True
    Else
      ckConsolidado.Enabled = False
      ckConsolidado.Checked = False
    End If
  End Sub

#End Region

#Region "Metodos"

  Private Sub mConsultabBO()
    Try


      Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
        Dim sCadena As String

        '************ INTO #TOrdVtaFact
        sCadena = " SELECT T1.BaseEntry,MIN(T1.ActDelDate) AS FchFactBO " &
                          " INTO #TOrdVtaFact " &
                          " FROM RDR1 T0 " &
                          " INNER JOIN INV1 T1 ON T0.DocEntry = T1.BaseEntry " &
                          " and T0.LineStatus = 'O'" &
                          " group by T1.BaseEntry  "

        'MODIFICADO POR IVAN GONZALEZ
        'SE ANEXARON LAS ORDENES DE ENTREGA SIN AFECTAR A LAS DEMAS SUCURSALES
        sCadena &= " insert into #TOrdVtaFact " &
                            " SELECT T1.BaseEntry,MIN(T1.ActDelDate) AS FchFactBO " &
                            " FROM RDR1 T0 INNER JOIN DLN1 T1 ON T0.DocEntry = T1.BaseEntry and T0.LineStatus = 'O' " &
                            " group by T1.BaseEntry "


        '************ INTO #TArtSol
        sCadena &= " SELECT  ROW_NUMBER() OVER(PARTITION BY T1.ItemCode ORDER BY T0.DocDueDate ASC) AS Enumera,T0.DocEntry,T1.ItemCode ,T1.Quantity,T0.DocDueDate " +
                          " INTO #TArtSol " +
                          " FROM OPOR T0 " +
                          "	    INNER JOIN POR1 T1 ON T0.DocEntry = T1.DocEntry " +
                          " WHERE LineStatus = 'O' "

        '************ INTO #TBackOrder
        '" T3.OnHand-T10.OnHand-T11.OnHand  [Stock Total],  " + --Se reemplazo esta linea
        sCadena &= " SELECT DISTINCT T1.WhsCode Ciudad,T1.ItemCode AS Articulo,T1.Dscription AS Descripción,T4.ItmsGrpNam as Linea,T1.OpenQty AS BackOrder, " +
                            " T2.OnHand AS [Stock Puebla] ,T8.OnHand AS [Stock Merida],T9.OnHand AS [Stock Tuxtla], " +
                            " T2.OnHand + T8.OnHand + T9.OnHand AS [Stock Total], " +
                            " T2.OnOrder AS [Sol. Puebla],T8.OnOrder AS [Sol. Merida],T9.OnOrder AS [Sol. Tuxtla],t3.OnOrder [Sol. Total], " +
                            "case when t1.WhsCode=01 then  T1.OpenQty else 0 end [BO.pueblaPzs],  " +
                            "case when t1.WhsCode=03 then  T1.OpenQty else 0 end [BO.meridaPzs],  " +
                            "case when t1.WhsCode=07 then  T1.OpenQty else 0 end [BO.tuxtlaPzs],  " +
                            " case when t1.WhsCode=01 then  T1.OpenQty * T1.Price else 0 end [BO. puebla], " +
                            " case when t1.WhsCode=03 then  T1.OpenQty * T1.Price else 0 end [BO. merida], " +
                            " case when t1.WhsCode=07 then  T1.OpenQty * T1.Price else 0 end [BO. tuxtla] " +
                            " INTO #TBackOrder" +
                            " FROM ORDR T0 " +
                            "        INNER JOIN #TOrdVtaFact T6 ON T0.DocEntry = T6.BaseEntry " +
                            "                            and T6.FchFactBO between '" & DtpFechaIni.Value.ToString("yyyy-MM-dd") & "' AND '" & DtpFechaTer.Value.ToString("yyyy-MM-dd") & "'" +
                            "        INNER JOIN RDR1 T1 ON T0.DocEntry = T1.DocEntry  AND T0.PaidToDate > 0 " +
                            "        inner join oitm t3 on t1.itemcode=t3.itemcode and T3.ItmsGrpCod <> 150	 " +
                            "        LEFT JOIN OITB T4 ON T3.ItmsGrpCod = T4.ItmsGrpCod 					 " +
                            "        LEFT JOIN #TArtSol T7 ON T7.ItemCode = T1.ItemCode AND T7.Enumera = 1" +
                            "        LEFT JOIN OITW T2 ON T1.ItemCode = T2.ItemCode AND T2.WhsCode = 01" +
                            "        LEFT JOIN OITW T8 ON T1.ItemCode = T8.ItemCode AND T8.WhsCode = 03  " +
                            "        LEFT JOIN OITW T9 ON T1.ItemCode = T9.ItemCode AND T9.WhsCode = 07  " +
                            "        LEFT JOIN OITW T10 ON T1.ItemCode = T10.ItemCode AND T10.WhsCode = 02  " +
                            "        LEFT JOIN OITW T11 ON T1.ItemCode = T11.ItemCode AND T11.WhsCode = 06  " +
                            " WHERE T1.LineStatus = 'O'  "
        If CmbAgteVta.SelectedValue <> 0 Then
          sCadena &= "  AND T0.SlpCode = " + CmbAgteVta.SelectedValue.ToString + "     "
        End If

        If CmbCliente.SelectedValue.ToString <> "0" Then
          sCadena &= "  AND T0.CardCode = '" + CmbCliente.SelectedValue.ToString + "'     "
        End If

        If (cmbAlmacen.SelectedValue <> 0) Then
          sCadena &= "  AND T1.WhsCode = '" + cmbAlmacen.SelectedValue.ToString + "'     "
        ElseIf (cmbAlmacen.SelectedValue = 0) Then
          sCadena &= "  AND T1.WhsCode in (01,03,07) "
        End If

        '********** PRIMER SELECT CONSULTA DE LADO IZQUIERDO
        sCadena &= " SELECT alm.WhsName Ciudad ,Linea,sum(BackOrder)[Pzas BO],sum([BO. puebla])+SUM([BO. merida])+SUM([BO. tuxtla]) [BO. total] ,1 orden " +
                          " FROM #TBackOrder TBO  " +
                          "        inner join owhs alm on TBO.Ciudad=alm.WhsCode " +
                          " group by alm.WhsName ,Linea " +
                          " UNION ALL" +
                          " SELECT '" + cmbAlmacen.Text + "', " +
                          " CAST('' AS NVARCHAR(20)) as Linea," +
                          " sum(CAST(BackOrder AS DECIMAL(19,6))) AS [Pzas BO]," +
                          " sum([BO. puebla])+SUM([BO. merida])+SUM([BO. tuxtla]) [BO. total],0 orden " +
                          " FROM #TBackOrder  " +
                          " order by 5 desc,4 desc  "


        '***********
        sCadena &= " SELECT "

        If ckConsolidado.Checked = False Then
          sCadena &= "alm.WhsName Ciudad, "
        Else
          sCadena &= "'TODOS     ' Ciudad, "
        End If

        If cmbAlmacen.SelectedValue = "01" Or cmbAlmacen.Text = "TODOS" Then

          almacen = "01"

          sCadena &= " Articulo, Descripción,Linea, sum(BackOrder)[Pzas BO], "
          sCadena &= " [Stock Total], [Sol. Total], "
          sCadena &= " sum([BO. Puebla])+SUM([BO. Merida])+SUM([BO. Tuxtla]) [BO. Total], "
          sCadena &= " sum([BO.pueblaPzs]) [BO.PueblaPzs],sum([BO. puebla]) [BO. Puebla], [Stock Puebla],[Sol. Puebla], "
          sCadena &= " SUM([BO.meridaPzs]) [BO.MeridaPzs],SUM([BO. merida]) [BO. Merida], [Stock Merida],[Sol. Merida], "
          sCadena &= " SUM([BO.tuxtlaPzs]) [BO.TuxtlaPzs],SUM([BO. tuxtla]) [BO. Tuxtla], [Stock Tuxtla],[Sol. Tuxtla] "

          sCadena &= " INTO #TmpDetallesPorLinea "
          sCadena &= " FROM #TBackOrder TBO "
          sCadena &= " inner join owhs alm on TBO.Ciudad=alm.WhsCode "
          sCadena &= " group by "

        ElseIf cmbAlmacen.SelectedValue = "03" Then

          almacen = "03"

          sCadena &= " Articulo, Descripción,Linea, sum(BackOrder)[Pzas BO], "
          sCadena &= " [Stock Total], [Sol. Total], "
          sCadena &= " sum([BO. puebla])+SUM([BO. merida])+SUM([BO. tuxtla]) [BO. Total], "
          sCadena &= " SUM([BO.meridaPzs]) [BO.MeridaPzs],SUM([BO. merida]) [BO. Merida], [Stock Merida],[Sol. Merida], "
          sCadena &= " sum([BO.pueblaPzs]) [BO.PueblaPzs],sum([BO. puebla]) [BO. Puebla], [Stock Puebla],[Sol. Puebla], "
                    'Hice un cambio
                    sCadena &= " SUM([BO.tuxtlaPzs]) [BO.TuxtlaPzs],SUM([BO. tuxtla]) [BO. Tuxtla], [Stock Tuxtla],[Sol. Tuxtla] "

                    sCadena &= " INTO #TmpDetallesPorLinea "
          sCadena &= " FROM #TBackOrder TBO "
          sCadena &= " inner join owhs alm on TBO.Ciudad=alm.WhsCode "
          sCadena &= " group by "

        ElseIf cmbAlmacen.SelectedValue = "07" Then

          almacen = "07"

          sCadena &= " Articulo, Descripción,Linea, sum(BackOrder)[Pzas BO], "
          sCadena &= " [Stock Total], [Sol. Total], "
          sCadena &= " sum([BO. puebla])+SUM([BO. merida])+SUM([BO. tuxtla]) [BO. Total], "
          sCadena &= " SUM([BO.TuxtlaPzs]) [BO.TuxtlaPzs],SUM([BO. tuxtla]) [BO. Tuxtla], [Stock Tuxtla],[Sol. Tuxtla], "
          sCadena &= " sum([BO.PueblaPzs]) [BO.PueblaPzs],sum([BO. puebla]) [BO. Puebla], [Stock Puebla],[Sol. Puebla], "
          sCadena &= " SUM([BO.MeridaPzs]) [BO.MeridaPzs],SUM([BO. merida]) [BO. Merida], [Stock Merida],[Sol. Merida] "

          sCadena &= " INTO #TmpDetallesPorLinea "
          sCadena &= " FROM #TBackOrder TBO "
          sCadena &= " inner join owhs alm on TBO.Ciudad=alm.WhsCode "
          sCadena &= " group by "
        End If

        If ckConsolidado.Checked = False Then
          sCadena &= "alm.WhsName, "
        End If

        sCadena &= " Articulo, Descripción,Linea,[Stock Puebla],[Stock Merida],[Stock Tuxtla], [Stock Total], [Sol. Puebla],[Sol. Merida],[Sol. Tuxtla],[Sol. Total] " +
                            " ORDER BY sum([BO. Puebla])+SUM([BO. Merida])+SUM([BO. Tuxtla]) DESC, 14 desc, linea desc"


        'Agrego totales en Detalles
        sCadena &= " /*TODOS*/
                    INSERT INTO #TmpDetallesPorLinea
                    SELECT 'TODAS' Ciudad,  'TODOS' Articulo, '' Descripción, '' Linea, 
                    sum([Pzas BO]),  SUM([Stock Total]), SUM([Sol. Total]),  
                    sum([BO. Total]), sum([BO.PueblaPzs]), sum([BO. Puebla]), 
                    SUM([Stock Puebla]),SUM([Sol. Puebla]), SUM([BO.MeridaPzs]),SUM([BO. Merida]), SUM([Stock Merida]),SUM([Sol. Merida]),
                    SUM([BO.TuxtlaPzs]),SUM([BO. Tuxtla]), SUM([Stock Tuxtla]),SUM([Sol. Tuxtla])
                    FROM #TmpDetallesPorLinea TBO

                    /*TOTALES PUEBLA*/
                    INSERT INTO #TmpDetallesPorLinea
                    SELECT 'PUEBLA' Ciudad,
                    'TODOS' Articulo,
                    '' Descripción,
                    '' Linea,
                    SUM([Pzas BO]), SUM([Stock Total]), SUM([Sol. Total]), SUM([BO. Total]), SUM([BO.PueblaPzs]), SUM([BO. Puebla]), SUM([Stock Puebla]), SUM([Sol. Puebla]), 
                    0,0,0,0, 
                    0,0,0,0
                    FROM #TmpDetallesPorLinea TBO
                    WHERE Ciudad NOT IN ('TODAS')

                    /*TOTALES PUEBLA*/
                    INSERT INTO #TmpDetallesPorLinea
                    SELECT 'MERIDA' Ciudad,  
                    'TODOS' Articulo, 
                    '' Descripción, 
                    '' Linea, 
                    SUM([Pzas BO]), SUM([Stock Total]), SUM([Sol. Total]), SUM([BO. Total]), 
                    0,0,0,0,
                    SUM([BO.MeridaPzs]), SUM([BO. Merida]), SUM([Stock Merida]), SUM([Sol. Merida]), 
                    0,0,0,0
                    FROM #TmpDetallesPorLinea TBO
                    WHERE Ciudad NOT IN ('TODAS','PUEBLA')

                    /*TOTALES PUEBLA*/
                    INSERT INTO #TmpDetallesPorLinea
                    SELECT 'TUXTLA' Ciudad,  
                    'TODOS' Articulo, 
                    '' Descripción, 
                    '' Linea, 
                    SUM([Pzas BO]), SUM([Stock Total]), SUM([Sol. Total]), SUM([BO. Total]), 
                    0,0,0,0, 
                    0,0,0,0,
                    SUM([BO.TuxtlaPzs]), SUM([BO. Tuxtla]), SUM([Stock Tuxtla]), SUM([Sol. Tuxtla])
                    FROM #TmpDetallesPorLinea TBO
                    WHERE Ciudad NOT IN ('TODAS','PUEBLA','MERIDA') 

                    /*TOTAL POR LINEA*/
                    INSERT INTO #TmpDetallesPorLinea
                    SELECT 'TODOSXLINE' Ciudad,  'TODOS' Articulo, '' Descripción, Linea, 
                    sum([Pzas BO]),  SUM([Stock Total]), SUM([Sol. Total]),  
                    sum([BO. Total]), sum([BO.PueblaPzs]), sum([BO. Puebla]), 
                    SUM([Stock Puebla]),SUM([Sol. Puebla]), SUM([BO.MeridaPzs]),SUM([BO. Merida]), SUM([Stock Merida]),SUM([Sol. Merida]),
                    SUM([BO.TuxtlaPzs]),SUM([BO. Tuxtla]), SUM([Stock Tuxtla]),SUM([Sol. Tuxtla])
                    FROM #TmpDetallesPorLinea TBO
                    WHERE Linea <> ''
                    GROUP BY Linea" + vbLf

        'Query para desplegar los Detalles
        sCadena &= " SELECT * FROM  #TmpDetallesPorLinea ORDER BY [BO. Total] DESC " + vbLf

        'CONSULTA DE LADO IZQUIERDO, MUESTRA TOTALES POR LINEA
        sCadena &= " SELECT 'TODOS' Ciudad , Linea, sum(BackOrder) [Pzas BO.], sum([BO. puebla])+SUM([BO. merida])+SUM([BO. tuxtla]) [BO. total],1 orden   " +
                            " FROM #TBackOrder TBO " +
                            "          inner join owhs alm on TBO.Ciudad=alm.WhsCode   " +
                            " group by Linea  " +
                            " UNION ALL " +
                            " SELECT 'TODOS',  CAST(' ' AS NVARCHAR(20)) as Linea, sum(CAST(BackOrder AS DECIMAL(19,6))) AS BackOrder,sum([BO. puebla]) + SUM([BO. merida]) + SUM([BO. tuxtla]) " +
                            "[BO. total],0 orden " +
                            " FROM #TBackOrder  " +
                            " ORDER BY 5 desc,4 desc  " +
                            " DROP TABLE #TArtSol  " +
                            " DROP TABLE #TBackOrder " +
                            " DROP TABLE #TOrdVtaFact " +
                            " DROP TABLE #TmpDetallesPorLinea "

        Dim da As New SqlClient.SqlDataAdapter(sCadena, SqlConnection)

        Dim ds As New DataSet
        da.Fill(ds)

        dvLineas.Table = ds.Tables(0)
        dvArticulos.Table = ds.Tables(1)
        dvLineasCon.Table = ds.Tables(2)

        EstiloGrid(DgRptBoLineasDet, dvArticulos)
        fFormatoGridDet()

        If ckConsolidado.Checked = True Then
          EstiloGrid(DgRptBoLineas, dvLineasCon)
          fFormtoGrid(DgRptBoLineas)
        Else
          EstiloGrid(DgRptBoLineas, dvLineas)
          fFormtoGrid(DgRptBoLineas)
        End If

        DgRptBoLineas.CurrentCell = DgRptBoLineas.Item(0,0)

        If DgRptBoLineas.Item(1, DgRptBoLineas.CurrentRow.Index).Value.ToString <> " " And DgRptBoLineas.Item(1, DgRptBoLineas.CurrentRow.Index).Value.ToString <> "" Then
          dvArticulos.RowFilter = "Linea ='" & DgRptBoLineas.Item(1, DgRptBoLineas.CurrentRow.Index).Value.ToString & "' "
          If ckConsolidado.Checked = False Then
            dvArticulos.RowFilter &= " AND (Ciudad ='" & DgRptBoLineas.Item(0, DgRptBoLineas.CurrentRow.Index).Value.ToString & "' OR Ciudad = 'TODOS')"
          End If
        End If

      End Using
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Private Sub mCargaAlmacen(ByVal conexion As SqlClient.SqlConnection)
    Try
      Dim da As New SqlClient.SqlDataAdapter("Select WhsCode,WhsName " +
                                          " from owhs " +
                                          " where WhsCode in (01,03,07) ", conexion)

      Dim ds As New DataSet
      da.Fill(ds)
      ds.Tables(0).Rows.Add(0, "TODOS")
      Me.cmbAlmacen.DataSource = ds.Tables(0)
      Me.cmbAlmacen.DisplayMember = "WhsName"
      Me.cmbAlmacen.ValueMember = "WhsCode"


            If UsrTPM = "RROBLES" Or UsrTPM = "VVERGARA" _
            Or UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" _
            Or UsrTPM = "VENTAS5" Or UsrTPM = "ASTRIDY" Or UsrTPM = "VENTAS14" Or UsrTPM = "CSANTOS" _
            Or UsrTPM = "VENTAS8" Then

                Me.cmbAlmacen.SelectedValue = AlmTPM.ToString()
                Me.cmbAlmacen.Enabled = False

            ElseIf UsrTPM = "ACASTRO" Or UsrTPM = "JSANCHEZ" Or UsrTPM = "RMERCADO" _
            Or UsrTPM = "VENTAS4" Or UsrTPM = "RJIMENEZ" Or UsrTPM = "ATABASCO" Then

                Me.cmbAlmacen.SelectedValue = AlmTPM.ToString()
                Me.cmbAlmacen.Enabled = False

            Else
                Me.cmbAlmacen.SelectedValue = 0

      End If

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Private Sub mCargaAgente(ByVal conexion As SqlClient.SqlConnection)
    Try
            Dim da As New SqlClient.SqlDataAdapter("SELECT OSLP.slpcode,OSLP.slpname " +
                                                    "FROM OSLP WHERE  (OSLP.U_ESTATUS = 'ACTIVO' OR OSLP.U_ESTATUS = 'INACTIVOCC') ORDER BY slpname ", conexion)

            Dim ds As New DataSet
      da.Fill(ds)
      ds.Tables(0).Rows.Add(0, "TODOS")
      Me.CmbAgteVta.DataSource = ds.Tables(0)
      Me.CmbAgteVta.DisplayMember = "slpname"
      Me.CmbAgteVta.ValueMember = "slpcode"

      If UsrTPM = "ACASTRO" Or UsrTPM = "JSANCHEZ" Or UsrTPM = "RMERCADO" _
            Or UsrTPM = "VENTAS4" Or UsrTPM = "RJIMENEZ" Or UsrTPM = "ATABASCO" Then

        Me.CmbAgteVta.SelectedValue = vCodAgte
        Me.CmbAgteVta.Enabled = False
      Else
        Me.CmbAgteVta.SelectedValue = 0
      End If




    Catch ex As Exception

    End Try
  End Sub

  Private Sub mCargaCliente(ByVal conexion As SqlClient.SqlConnection)
    Try
      Dim da As New SqlClient.SqlDataAdapter(" SELECT CardCode Clave,CardName+' ('+CardCode+')' Nombre  " +
                                                    " FROM OCRD WHERE CardType = 'C' ORDER BY SlpCode,CardName", conexion)

      Dim ds As New DataSet
      da.Fill(ds)
      ds.Tables(0).Rows.Add(0, "TODOS")
      Me.CmbCliente.DataSource = ds.Tables(0)
      Me.CmbCliente.DisplayMember = "Nombre"
      Me.CmbCliente.ValueMember = "Clave"
      Me.CmbCliente.SelectedValue = 0
    Catch ex As Exception

    End Try
  End Sub


#End Region

#Region "Funciones"

  Private Function fValidaDatos(ByRef sError) As Boolean
    Try
      Dim bValido As Boolean
      bValido = True
      If (DtpFechaIni.Value > DtpFechaTer.Value) Then
        sError = sError + Environment.NewLine + "-La fecha término debe ser mayor ala fecha inicial."
        bValido = False
        eFin.Visible = True
        eIni.Visible = True
      Else
        eIni.Visible = False
        eFin.Visible = False
      End If

      Return bValido
    Catch ex As Exception

    End Try
  End Function

  Private Sub fFormtoGrid(ByVal Grid As DataGridView)
    Try

      With Grid
        .Columns(0).HeaderText = "Almacén"
        .Columns(0).Width = 50

        .Columns(1).HeaderText = "Linea"
        .Columns(1).Width = 90

        .Columns(2).HeaderText = "Pzas BO."
        .Columns(2).Width = 70
        .Columns(2).DefaultCellStyle.Format = "###,###,###"
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(3).HeaderText = "$ Total BO"
        .Columns(3).Width = 100
        .Columns(3).DefaultCellStyle.Format = " $ ###,###,###.00"
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(4).HeaderText = ""
        .Columns(4).Width = 0
        .Columns(4).Visible = False
      End With

    Catch ex As Exception

    End Try

  End Sub

  Private Sub fFormatoGridDet()
    Try
      With DgRptBoLineasDet

        .Columns(0).HeaderText = "Almacén"
        .Columns(0).Width = 50

        .Columns(1).HeaderText = "Cod. Articulo"
        .Columns(1).Width = 90

        .Columns(2).HeaderText = "Articulo"
        .Columns(2).Width = 250

        .Columns(3).HeaderText = "Linea"
        .Columns(3).Width = 70
        .Columns(3).Visible = False

        .Columns(4).HeaderText = "Pzas BO."
        .Columns(4).Width = 40
        .Columns(4).DefaultCellStyle.Format = "###,###,###"
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(5).HeaderText = "Stock Total"
        .Columns(5).Width = 45
        .Columns(5).DefaultCellStyle.Format = "###,###,###"
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(6).HeaderText = "Sol. Total"
        .Columns(6).Width = 45
        .Columns(6).DefaultCellStyle.Format = "###,###,###"
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(7).HeaderText = "$ Total BO"
        .Columns(7).Width = 90
        .Columns(7).DefaultCellStyle.Format = "$ ###,###,###.#0"
        .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(7).Frozen = True

        If almacen = "01" Then
          .Columns(8).HeaderText = "BO. Puebla Piezas"
          .Columns(9).HeaderText = "BO. Puebla ($)"
          .Columns(10).HeaderText = "Stock Puebla"
          .Columns(11).HeaderText = "Sol. Puebla"
          .Columns(12).HeaderText = "BO. Merida Piezas"
          .Columns(13).HeaderText = "BO. Merida ($)"
          .Columns(14).HeaderText = "Stock Merida"
          .Columns(15).HeaderText = "Sol. Merida"
          .Columns(16).HeaderText = "BO. Tuxtla Piezas"
          .Columns(17).HeaderText = "BO. Tuxtla ($)"
          .Columns(18).HeaderText = "Stock Tuxtla"
          .Columns(19).HeaderText = "Sol. Tuxtla"

        ElseIf cmbAlmacen.SelectedValue = "03" Then
          .Columns(8).HeaderText = "BO. Merida Piezas"
          .Columns(9).HeaderText = "BO. Merida ($)"
          .Columns(10).HeaderText = "Stock Merida"
          .Columns(11).HeaderText = "Sol. Merida"
          .Columns(12).HeaderText = "BO. Puebla Piezas"
          .Columns(13).HeaderText = "BO. Puebla ($)"
          .Columns(14).HeaderText = "Stock Puebla"
          .Columns(15).HeaderText = "Sol. Puebla"
          .Columns(16).HeaderText = "BO. Tuxtla Piezas"
          .Columns(17).HeaderText = "BO. Tuxtla ($)"
          .Columns(18).HeaderText = "Stock Tuxtla"
          .Columns(19).HeaderText = "Sol. Tuxtla"

        ElseIf cmbAlmacen.SelectedValue = "07" Then
          .Columns(8).HeaderText = "BO. Tuxtla Piezas"
          .Columns(9).HeaderText = "BO. Tuxtla ($)"
          .Columns(10).HeaderText = "Stock Tuxtla"
          .Columns(11).HeaderText = "Sol. Tuxtla"
          .Columns(12).HeaderText = "BO. Puebla Piezas"
          .Columns(13).HeaderText = "BO. Puebla ($)"
          .Columns(14).HeaderText = "Stock Puebla"
          .Columns(15).HeaderText = "Sol. Puebla"
          .Columns(16).HeaderText = "BO. Merida Piezas"
          .Columns(17).HeaderText = "BO. Merida ($)"
          .Columns(18).HeaderText = "Stock Merida"
          .Columns(19).HeaderText = "Sol. Merida"
        End If

        'BO. Piezas
        .Columns(8).Width = 50
        .Columns(8).DefaultCellStyle.Format = "###,###,###"
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'BO. ($)
        .Columns(9).Width = 60
        .Columns(9).DefaultCellStyle.Format = "$ ###,###,###.#0"
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'Stock
        .Columns(10).Width = 50
        .Columns(10).DefaultCellStyle.Format = "###,###,###"
        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'Sol.
        .Columns(11).Width = 50
        .Columns(11).DefaultCellStyle.Format = "###,###,###"
        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'BO. Piezas
        .Columns(12).Width = 50
        .Columns(12).DefaultCellStyle.Format = "###,###,###"
        .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'BO. ($)
        .Columns(13).Width = 60
        .Columns(13).DefaultCellStyle.Format = "$ ###,###,###.#0"
        .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'Stock
        .Columns(14).Width = 50
        .Columns(14).DefaultCellStyle.Format = "###,###,###"
        .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'Sol.
        .Columns(15).Width = 50
        .Columns(15).DefaultCellStyle.Format = "###,###,###"
        .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'BO. Piezas
        .Columns(16).Width = 50
        .Columns(16).DefaultCellStyle.Format = "###,###,###"
        .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'BO. ($)
        .Columns(17).Width = 60
        .Columns(17).DefaultCellStyle.Format = "$ ###,###,###.#0"
        .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'Stock
        .Columns(18).Width = 50
        .Columns(18).DefaultCellStyle.Format = "###,###,###"
        .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'Sol.
        .Columns(19).Width = 50
        .Columns(19).DefaultCellStyle.Format = "###,###,###"
        .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


      End With

    Catch ex As Exception

    End Try
  End Sub

  Public Function EstiloGrid(ByVal Grid As DataGridView, ByRef ds As DataView) As Boolean
    Try
      With Grid
        .DataSource = ds
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .RowHeadersVisible = True
        .SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
        '.MultiSelect = False
        .AllowUserToAddRows = False
        .ReadOnly = True
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .RowHeadersWidth = 25
      End With

    Catch ex As Exception

    End Try

  End Function

#End Region
  Private Sub DgRptBoLineasDet_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DgRptBoLineasDet.RowPrePaint
    Try
      DgRptBoLineasDet.Rows(e.RowIndex).Cells("Pzas BO").Style.BackColor = Color.DarkGray
      DgRptBoLineasDet.Rows(e.RowIndex).Cells("Stock Total").Style.BackColor = Color.DarkGray
      DgRptBoLineasDet.Rows(e.RowIndex).Cells("Sol. Total").Style.BackColor = Color.DarkGray
      DgRptBoLineasDet.Rows(e.RowIndex).Cells("BO. total").Style.BackColor = Color.LightYellow
      DgRptBoLineasDet.Rows(e.RowIndex).Cells("Pzas BO").Style.ForeColor = Color.White
      DgRptBoLineasDet.Rows(e.RowIndex).Cells("Stock Total").Style.ForeColor = Color.White
      DgRptBoLineasDet.Rows(e.RowIndex).Cells("Sol. Total").Style.ForeColor = Color.White

      DgRptBoLineasDet.Rows(e.RowIndex).Cells("BO.pueblaPzs").Style.BackColor = Color.LightBlue
      DgRptBoLineasDet.Rows(e.RowIndex).Cells("BO.meridaPzs").Style.BackColor = Color.LightCoral
      DgRptBoLineasDet.Rows(e.RowIndex).Cells("BO.tuxtlaPzs").Style.BackColor = Color.LightGreen

    Catch ex As Exception

    End Try


  End Sub

  Private Sub CmbAgteVta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAgteVta.SelectedIndexChanged
    Try
      Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
        If CmbAgteVta.SelectedValue.ToString = "0" Then
          mCargaCliente(SqlConnection)
        Else
          Dim da As New SqlClient.SqlDataAdapter(" SELECT CardCode Clave,CardName+' ('+CardCode+')' Nombre  " +
                                                            " FROM OCRD " +
                                                            " WHERE CardType = 'C'" +
                                                            " and SlpCode= " + CmbAgteVta.SelectedValue.ToString +
                                                            " ORDER BY SlpCode,CardName", SqlConnection)

          Dim ds As New DataSet
          da.Fill(ds)
          ds.Tables(0).Rows.Add(0, "TODOS")
          Me.CmbCliente.DataSource = ds.Tables(0)
          Me.CmbCliente.DisplayMember = "Nombre"
          Me.CmbCliente.ValueMember = "Clave"
          Me.CmbCliente.SelectedValue = 0
        End If

      End Using
    Catch ex As Exception

    End Try
  End Sub


End Class