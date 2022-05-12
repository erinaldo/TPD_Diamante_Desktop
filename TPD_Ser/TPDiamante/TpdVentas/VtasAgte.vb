Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ClosedXML.Excel

Public Class Reporte_de_Ventas_Detalle
 Private dv As New DataView
 Private dvTodosArt As New DataView
 Dim DivTer As New DataView
 Dim DivIni As New DataView
 Dim DvDlinea As New DataView
 Dim objDataSet As New DataTable
 Dim Rangos As String = ""
 Dim Rangos2 As String = ""

 Private Sub ConsultaProd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  Me.Text = "Agente - Linea -- " & "VtasAgte.vb"
  If UsrTPM = "MANAGER" Then
   pEncabezado.Location = New Point(56, 3)
   Me.WindowState = FormWindowState.Maximized
  End If
  Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
  Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")
  RBTot.Checked = True

  If VEsAgente = 1 Then
   CkClientes.Checked = False
   CkClientes.Enabled = False
  End If

 End Sub

 Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
  cargar_registros()
  If GrdConProd.RowCount > 0 Then
   'Posicionar cursor en datagridview
   GrdConProd.CurrentCell = GrdConProd.Rows(0).Cells(1)
  End If
  Try
   dv.RowFilter = "LineaAgte ='" + GrdConLinea.Item(12, GrdConLinea.CurrentRow.Index).Value.ToString + "'"
   dvTodosArt.RowFilter = "IdAgente =" & GrdConProd.Item(0, GrdConProd.CurrentRow.Index).Value.ToString
  Catch ex As Exception

  End Try
 End Sub

 Sub cargar_registros()


  Dim Consulta As String = ""
  Dim strcadena As String = ""
  Dim CTabla As String = ""
  Dim DTMObra As New DataTable
  Dim DTProb As New DataTable



  '** VENTAS TOTALES POR VENDEDOR
  '** LO FACTURADO
  '****************************************************************ANTERIOR
  'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as VtaAntesIva "
  ''Consulta &= "FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType <> 'S' AND SERIES <> 59 "
  'Consulta &= "FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType <> 'S' "

  'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
  '    Consulta &= "AND SlpCode = '" & vCodAgte & "' "
  'ElseIf UsrTPM = "VVERGARA" Then
  '    Consulta &= "AND SlpCode in (select SlpCode from OSLP where Memo = '07' ) "
  'ElseIf UsrTPM = "RROBLES" Then
  '    Consulta &= "AND SlpCode in (select SlpCode from OSLP where Memo = '03' ) "
  'Else
  '    If (CkClientes.Checked = False) Then
  '        Consulta &= " and slpCode <> 1 "
  '    End If
  'End If
  'Consulta &= "GROUP BY OINV.SlpCode ORDER BY VtaAntesIva DESC "
  '****************************************************************ANTERIOR
  'MODIFICADO POR URIEL
  'SE OBTIENEN LAS VENTAS TOTALES
  Consulta = " select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
  Consulta &= "into #TodasFacturasArticulos "
  Consulta &= "from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry "
  Consulta &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
  Consulta &= "where t0.DocDate between @FechaIni and @FechaTer "
  Consulta &= "and t0.DocType <> 'S' "
  Consulta &= "and t2.ItmsGrpCod <> 200 "
  If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
   Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
  ElseIf UsrTPM = "VVERGARA" Then
   Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' ) "
  ElseIf UsrTPM = "RROBLES" Then
   Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' ) "
  Else
   If (CkClientes.Checked = False) Then
    Consulta &= " and t0.slpCode <> 1 "
   End If
  End If
  'SE OBTIENEN LAS CANCELACIONES TOTALES
  Consulta &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
  Consulta &= "into #NC_Art_NoTimb "
  Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
  Consulta &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
  Consulta &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
  Consulta &= "where t0.DocDate between @FechaIni and @FechaTer "
  Consulta &= "and t0.DocType <> 'S' "
  Consulta &= "and t2.ItmsGrpCod <> 200 "
  'Consulta &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
  Consulta &= "AND ((CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate >= '2020-08-24' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
  Consulta &= "AND T0.U_BXP_TIMBRADO <> 'T') "
  Consulta &= " AND T3.ReportID IS NULL "

  If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
   Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
  ElseIf UsrTPM = "VVERGARA" Then
   Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' ) "
  ElseIf UsrTPM = "RROBLES" Then
   Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' ) "
  Else
   If (CkClientes.Checked = False) Then
    Consulta &= " and t0.slpCode <> 1 "
   End If
  End If

  Consulta &= "SELECT t0.Slpcode as IdVend,SUM((t0.DocTotal - t0.VatSum) - t0.TotalExpns) as VtaAntesIva "
  Consulta &= "into #tmp1 "
  Consulta &= "FROM #TodasFacturasArticulos t0 "
  Consulta &= "GROUP BY t0.SlpCode ORDER BY VtaAntesIva DESC "

  Consulta &= "SELECT t0.Slpcode as IdVend,SUM((t0.DocTotal - t0.VatSum) - t0.TotalExpns) as VtaAntesIva "
  Consulta &= "into #tmp2 "
  Consulta &= "FROM #NC_Art_NoTimb t0 "
  Consulta &= "GROUP BY t0.SlpCode ORDER BY VtaAntesIva DESC "

  'Inserto los agentes que no hayan tenido ventas pero si NCR, basicamente los que esten en la #temp2 pero no en la #temp1 con importe 0
  Consulta &= "INSERT INTO #tmp1 "
  Consulta &= "SELECT t0.IdVend, 0 FROM #tmp2 t0 "
  Consulta &= "LEFT OUTER JOIN #tmp1 t1 ON t0.IdVend = t1.IdVend "
  Consulta &= "WHERE t1.IdVend Is NULL "

  Consulta &= "select t0.IdVend, t0.VtaAntesIva - ISNULL(t1.VtaAntesIva,0) as 'VtaAntesIva' "
  Consulta &= "from #tmp1 t0 left join #tmp2 t1 on t0.IdVend = t1.IdVend ORDER BY VtaAntesIva DESC "

  Consulta &= "drop table #TodasFacturasArticulos "
  Consulta &= "drop table #NC_Art_NoTimb "
  Consulta &= "drop table #tmp1 "
  Consulta &= "drop table #tmp2 "


  Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

  CmdMObra.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime)
  CmdMObra.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
  CmdMObra.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime)
  CmdMObra.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value
  CmdMObra.CommandTimeout = 360
  CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
  CmdMObra.Connection.Open()

  Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
  AdapMObra.Fill(DTMObra)
  CmdMObra.Connection.Close()


  '** Se crea tabla temporal
  CTabla = "CREATE TABLE #REPVTAS (IdVend INT,VtaAntesIva Numeric(19,2),PorVtas Numeric(5,2),"
  CTabla &= "Dev Numeric(19,2),PorDev Numeric(5,2),Des Numeric(19,2),PorDes Numeric(5,2),"
  CTabla &= "Cancels Numeric(19,2),PorCan Numeric(5,2),VtaCDes Numeric(19,2),PorCDes Numeric(5,2),"
  CTabla &= "VtasNt Numeric(19,2),TotNC Numeric(19,2))"

  Dim cmdcost As Data.SqlClient.SqlCommand
  cmdcost = New Data.SqlClient.SqlCommand()

  With cmdcost
   .Connection = New Data.SqlClient.SqlConnection(StrCon)
   .Connection.Open()
   .CommandText = CTabla
   .ExecuteNonQuery()
  End With
  '** SE OBTIENE EL MONTO TOTAL DE LAS VENTAS Y SE AGREGA A LA TABLA TEMPORAL
  Dim TotalVtas As Decimal
  Dim cmd As New Data.SqlClient.SqlCommand
  With cmd
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   '.CommandText = "SELECT SUM((DocTotal - VatSum) - TotalExpns) as VtaAntesIva FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType <> 'S' AND SERIES <> 59 "
   '*******************************************ANTERIOR
   '.CommandText = "SELECT SUM((DocTotal - VatSum) - TotalExpns) as VtaAntesIva FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType <> 'S' "

   'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
   '    .CommandText = .CommandText + " AND SlpCode = '" & vCodAgte & "' "
   'ElseIf UsrTPM = "VVERGARA" Then
   '    .CommandText = .CommandText + " AND SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
   'ElseIf UsrTPM = "RROBLES" Then
   '    .CommandText = .CommandText + " AND SlpCode in (select SlpCode from OSLP where Memo = '03' ) "
   'Else
   '    If (CkClientes.Checked = False) Then
   '        .CommandText = .CommandText + " and slpCode <> 1 "
   '    End If

   'End If
   '**************************************************ANTERIOR
   'MODIFICADO POR URIEL
   .CommandText = "SELECT SUM((t0.DocTotal - t0.VatSum) - t0.TotalExpns) as VtaAntesIva FROM OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry inner join OITM t2 on t1.ItemCode = t2.ItemCode WHERE t0.DocDate >= @FechaIni AND t0.DocDate <= @FechaTer AND DocType <> 'S' and t2.ItmsGrpCod <> 200 "

   If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
    .CommandText = .CommandText + " AND t0.SlpCode = '" & vCodAgte & "' "
   ElseIf UsrTPM = "VVERGARA" Then
    .CommandText = .CommandText + " AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
   ElseIf UsrTPM = "RROBLES" Then
    .CommandText = .CommandText + " AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' ) "
   Else
    If (CkClientes.Checked = False) Then
     .CommandText = .CommandText + " and t0.slpCode <> 1 "
    End If

   End If
   .CommandTimeout = 360
   .CommandType = CommandType.Text
   .Connection = New Data.SqlClient.SqlConnection(StrCon)
   .Connection.Open()
   TotalVtas = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
   .Connection.Close()
  End With

  For Each fila As DataRow In DTMObra.Rows
   strcadena = "INSERT INTO #REPVTAS (IdVend, VtaAntesIva,PorVtas) VALUES ("
   strcadena &= fila("IdVend")
   strcadena &= ","
   strcadena &= fila("VtaAntesIva")
   strcadena &= ","
   strcadena &= fila("VtaAntesIva") * 100 / TotalVtas
   strcadena &= ")"

   With cmdcost
    .CommandText = strcadena
    .ExecuteNonQuery()
   End With
  Next


  '******Consulta para contemplar los agentes que no tienen ventas**********************************
  Dim DTSinVta As New DataTable

  strcadena = " SELECT Slpcode INTO #TEMP_OINV FROM OINV WHERE DocDate BETWEEN @FechaIni AND @FechaTer AND DocType <> 'S' GROUP BY OINV.SlpCode "
  strcadena &= " SELECT ORIN.SlpCode as IdVend FROM ORIN WHERE DocDate >= @FechaIni AND "
  strcadena &= "DocDate <= @FechaTer AND ORIN.SlpCode not in (SELECT Slpcode FROM #TEMP_OINV) "
  'strcadena &= " /*(SELECT Slpcode FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType <> 'S' GROUP BY OINV.SlpCode)*/ "
  If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
   strcadena &= "AND SlpCode = '" & vCodAgte & "' "
  ElseIf UsrTPM = "VVERGARA" Then
   strcadena &= "AND SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
  ElseIf UsrTPM = "RROBLES" Then
   strcadena &= "AND SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
  Else
   If (CkClientes.Checked = False) Then
    strcadena &= " and slpCode <> 1 "
   End If

  End If
  strcadena &= "GROUP BY ORIN.SlpCode "

  Dim CmdSinVta As New SqlClient.SqlCommand(strcadena)

  CmdSinVta.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime)
  CmdSinVta.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
  CmdSinVta.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime)
  CmdSinVta.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value
  CmdSinVta.CommandTimeout = 360
  CmdSinVta.Connection = New SqlClient.SqlConnection(StrCon)
  CmdSinVta.Connection.Open()

  Dim AdapSinVta As New SqlClient.SqlDataAdapter(CmdSinVta)
  AdapSinVta.Fill(DTSinVta)
  CmdSinVta.Connection.Close()


  For Each fila As DataRow In DTSinVta.Rows
   strcadena = "INSERT INTO #REPVTAS (IdVend, VtaAntesIva,PorVtas) VALUES ("
   strcadena &= fila("IdVend")
   strcadena &= ","
   strcadena &= "0"
   strcadena &= ","
   strcadena &= "0"
   strcadena &= ")"

   With cmdcost
    .CommandText = strcadena
    .ExecuteNonQuery()
   End With
  Next
  '************************************************************************************

  strcadena = "INSERT INTO #REPVTAS (IdVend, VtaAntesIva,PorVtas) VALUES ("
  strcadena &= 0
  strcadena &= ","
  strcadena &= TotalVtas
  strcadena &= ","
  strcadena &= 100
  strcadena &= ")"

  With cmdcost
   .CommandText = strcadena
   .ExecuteNonQuery()
  End With


  '****************************************************************************************************************************************************************************************
  Dim DTRefacciones As New DataTable

  '*SE AGREGA EL NOMBRE DEL AGENTE A LA CONSULTA QUE CONTIENE LO FACTURADO Y AGENTES QUE NO VENDIERON
  strcadena = "SELECT CAST(#REPVTAS.IdVend AS SMALLINT) AS IdVend,OSLP.SlpName,"
  strcadena &= "CAST(#REPVTAS.VtaCDes AS dec(19,2)) as VtaCDes, "
  strcadena &= "CAST(#REPVTAS.PorCDes AS dec(5,2)) as PorCDes, "
  strcadena &= "Dev,PorDev,VtasNt,Des,PorDes,TotNC,Cancels,PorCan, "
  strcadena &= "CAST(#REPVTAS.VtaAntesIva AS dec(19,2)) as VtaAntesIva, "
  strcadena &= "CAST(#REPVTAS.PorVtas AS dec(5,2)) as PorVtas "
  strcadena &= "FROM #REPVTAS LEFT JOIN OSLP ON #REPVTAS.IdVend = OSLP.SlpCode"

  With cmdcost
   .CommandText = strcadena
  End With

  Dim DAdapter As New SqlClient.SqlDataAdapter(cmdcost)

  DAdapter.Fill(DTRefacciones)
  Consulta = ""
  'DEVOLUCIONES-----------------------------------------------------------------------------------------------------------------------------
  Dim DataCRec As Data.SqlClient.SqlDataReader
  If (DtpFechaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
    Consulta &= "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DevAntesIva  "
    Consulta &= "FROM ORIN t0 WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType  = 'I' AND   "
    Consulta &= "CASE when t0.docdate <= '2017-11-19' then EDocNum  "
    Consulta &= "else (select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end IS NOT NULL  "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     Consulta &= "AND SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     Consulta &= "AND SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     Consulta &= "AND SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      Consulta &= " and slpCode <> 1 "
     End If
    End If

    Consulta &= "GROUP BY t0.SlpCode ORDER BY DevAntesIva DESC "
   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    Consulta &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    Consulta &= "into #tmp "
    Consulta &= "from ORIN t0 where t0.DocDate >= @FechaIni and t0.DocDate <= @FechaTer and t0.DocType = 'I' and "
    Consulta &= "CASE when t0.DocDate <= '2017-11-19' then EDocNum "
    Consulta &= "else (select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end IS NOT NULL  "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      Consulta &= " and t0.slpCode <> 1 "
     End If
    End If
    '******************************************ANTERIOR
    'Consulta &= "union all "
    'Consulta &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    'Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry  "
    'Consulta &= "where t0.DocDate >= '2018-01-01' and t0.DocDate <= @FechaTer and "
    'Consulta &= "t1.ItemCode <> 'DESCUENTO P.P' and "
    'Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14)  IS NOT NULL  "
    'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    'ElseIf UsrTPM = "VVERGARA" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    'ElseIf UsrTPM = "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    'Else
    '    If (CkClientes.Checked = False) Then
    '        Consulta &= " and t0.slpCode <> 1 "
    '    End If
    'End If

    'Consulta &= "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DevAntesIva  "
    'Consulta &= "FROM #tmp t0  "
    'Consulta &= "GROUP BY t0.SlpCode ORDER BY DevAntesIva DESC "
    'Consulta &= "drop table #tmp "
    '********************************************ANTERIOR
    'MODIFICADO POR URIEL
    'OBTIENE TODAS LAS DEVOLUCIONES
    Consulta &= "union all "
    Consulta &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    Consulta &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
    Consulta &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
    Consulta &= "where t0.DocDate between @FechaIni and @FechaTer "
    Consulta &= "and t0.DocType <> 'S' "
    Consulta &= "and t2.ItmsGrpCod <> 200 "
    Consulta &= "AND (T3.ReportID IS NOT NULL OR T0.U_BXP_UUID IS NOT NULL) "
    Consulta &= "AND ((T0.U_BXP_TIMBRADO = 'T' OR T0.U_BXP_TIMBRADO = 'P') OR T0.EDocGenTyp = 'G') "
    Consulta &= "AND t0.DocNum NOT IN (SELECT DocNum FROM TPM.dbo.[00ExcepDevolucion]) "

    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      Consulta &= " and t0.slpCode <> 1 "
     End If
    End If
    Consulta &= "select SlpCode as IdVend, SUM((DocTotal - VatSum) - TotalExpns) as DevAntesIva "
    Consulta &= "from #tmp "
    Consulta &= "group by SlpCode "
    Consulta &= "order by DevAntesIva DESC "
    Consulta &= "drop table #tmp "

   End If
  ElseIf (DtpFechaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then
   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    '********************************************ANTERIOR
    'Consulta &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    'Consulta &= "into #tmp "
    'Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry  "
    'Consulta &= "where t0.DocDate >= @FechaIni and t0.DocDate <= @FechaTer and "
    'Consulta &= "t1.ItemCode <> 'DESCUENTO P.P' and "
    'Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14)  IS NOT NULL  "
    'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    'ElseIf UsrTPM = "VVERGARA" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    'ElseIf UsrTPM = "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    'Else
    '    If (CkClientes.Checked = False) Then
    '        Consulta &= " and t0.slpCode <> 1 "
    '    End If
    'End If

    'Consulta &= "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DevAntesIva  "
    'Consulta &= "FROM #tmp t0  "
    'Consulta &= "GROUP BY t0.SlpCode ORDER BY DevAntesIva DESC "
    'Consulta &= "drop table #tmp "
    '******************************************ANTERIOR
    'MODIFICADO POR URIEL
    'OBTIENE TODAS LAS DEVOLUCIONES
    Consulta &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    Consulta &= "into #NC_Art_Timb "
    Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    Consulta &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
    Consulta &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
    Consulta &= "where t0.DocDate between @FechaIni and @FechaTer "
    Consulta &= "and t0.DocType <> 'S' "
    Consulta &= "and t2.ItmsGrpCod <> 200 "
    Consulta &= "AND (T3.ReportID IS NOT NULL OR T0.U_BXP_UUID IS NOT NULL) "
    Consulta &= "AND ((T0.U_BXP_TIMBRADO = 'T' OR T0.U_BXP_TIMBRADO = 'P') OR T0.EDocGenTyp = 'G') "
    Consulta &= "AND t0.DocNum NOT IN (SELECT DocNum FROM TPM.dbo.[00ExcepDevolucion]) "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      Consulta &= " and t0.slpCode <> 1 "
     End If
    End If
    Consulta &= "select SlpCode as IdVend, SUM((DocTotal - VatSum) - TotalExpns) as DevAntesIva "
    Consulta &= "from #NC_Art_Timb "
    Consulta &= "group by SlpCode "
    Consulta &= "order by DevAntesIva DESC "
    Consulta &= "drop table #NC_Art_Timb "
   End If
  End If
  With cmdcost
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   .CommandText = Consulta
   .CommandTimeout = 360
   DataCRec = .ExecuteReader()
  End With

  Dim TotDev As Decimal
  Do While DataCRec.Read()
   For Each fila As DataRow In DTRefacciones.Rows
    'Se asignan el total de tickets que tiene el recurso
    If fila("IdVend") = DataCRec.Item("IdVend") Then
     If IsDBNull(DataCRec.Item("DevAntesIva")) Then
      fila("Dev") = 0
     Else
      fila("Dev") = DataCRec.Item("DevAntesIva")
     End If
     TotDev = TotDev + DataCRec.Item("DevAntesIva")
    End If

   Next
  Loop


  '****************************************************
  'DESCUENTOS PRONTO PAGO--------------------------------------------------------------------------------------------------
  Consulta = ""
  cmdcost.Connection.Close()
  Dim DtVtaDes As Data.SqlClient.SqlDataReader
  If (DtpFechaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
    Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DesAntesIva "
    Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer "
    Consulta &= "AND DocType  = 'S'  "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     Consulta &= "AND SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     Consulta &= "AND SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     Consulta &= "AND SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      Consulta &= " and slpCode <> 1 "
     End If
    End If
    Consulta &= "GROUP BY ORIN.SlpCode ORDER BY DesAntesIva DESC "

   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    'MODIFICADO POR URIEL
    'OBTIENE LOS DESCUENTOS DE PRONTO PAGO
    Consulta &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    Consulta &= "into #FacturasPP "
    Consulta &= "from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry "
    Consulta &= "where "
    Consulta &= "t0.DocDate between @FechaIni and @FechaTer and "
    Consulta &= "t0.DocType = 'I' and "
    Consulta &= "t1.ItemCode = 'DESCUENTO P.P' "
    If (CkClientes.Checked = False) Then
     Consulta &= " and t0.slpCode <> 1 "
    End If

    Consulta &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    Consulta &= "into #NCPP "
    Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    Consulta &= "where "
    Consulta &= "t0.DocDate between @FechaIni and @FechaTer and "
    Consulta &= "t0.DocType = 'I' "
    Consulta &= " AND (t1.ItemCode = 'DESCUENTO P.P' OR t1.ItemCode = 'AP_ANTICIPO') "
    If (CkClientes.Checked = False) Then
     Consulta &= " and t0.slpCode <> 1 "
    End If

    Consulta &= "select Slpcode as IdVend,(SUM((DocTotal - VatSum) - TotalExpns))* -1 as DesAntesIva "
    Consulta &= "into #tmp13 "
    Consulta &= "from #FacturasPP "
    Consulta &= "group by SlpCode "

    Consulta &= "select Slpcode as IdVend,(SUM((DocTotal - VatSum) - TotalExpns)) as DesAntesIva "
    Consulta &= "into #tmp14 "
    Consulta &= "from #NCPP "
    Consulta &= "group by SlpCode "

    Consulta &= "select t0.IdVend, t0.DesAntesIva + isnull(t1.DesAntesIva,0) as DesAntesIva "
    Consulta &= "into #tmp22 "
    Consulta &= "from #tmp14 t0 left join #tmp13 t1 on t0.IdVend = t1.IdVend "

    Consulta &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    Consulta &= "into #tmp23 "
    Consulta &= "from ORIN t0 where t0.DocDate >= @FechaIni AND t0.DocDate <= @FechaTer  "
    Consulta &= "and t0.DocType = 'S' "
    If (CkClientes.Checked = False) Then
     Consulta &= " and t0.slpCode <> 1 "
    End If

    Consulta &= "select IdVend, DesAntesIva "
    Consulta &= "into #tmp2 "
    Consulta &= "from #tmp22 "
    Consulta &= "union all "
    Consulta &= "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DesAntesIva  "
    Consulta &= "FROM #tmp23 t0 "
    Consulta &= "GROUP BY t0.SlpCode ORDER BY DesAntesIva DESC "

    Consulta &= "select IdVend, SUM(DesAntesIva) as DesAntesIva  "
    Consulta &= "from #tmp2 "
    Consulta &= "group by IdVend "

    Consulta &= "drop table #FacturasPP "
    Consulta &= "drop table #NCPP "
    Consulta &= "drop table #tmp13 "
    Consulta &= "drop table #tmp14 "
    Consulta &= "drop table #tmp22 "
    Consulta &= "drop table #tmp23 "
    Consulta &= "drop table #tmp2 "

    '*****************************************ANTERIOR
    'Consulta &= "union all "
    'Consulta &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    'Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry  "
    'Consulta &= "where t0.DocDate >= '2018-01-01' AND t0.DocDate <= @FechaTer and "
    'Consulta &= "t1.ItemCode = 'DESCUENTO P.P' "
    'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    'ElseIf UsrTPM = "VVERGARA" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    'ElseIf UsrTPM = "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    'Else
    '    If (CkClientes.Checked = False) Then
    '        Consulta &= " and t0.slpCode <> 1 "
    '    End If
    'End If

    'Consulta &= "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DesAntesIva  "
    'Consulta &= "FROM #tmp2 t0 "
    'Consulta &= "GROUP BY t0.SlpCode ORDER BY DesAntesIva DESC "
    'Consulta &= "drop table #tmp2 "
    '******************************************ANTERIOR
   End If
  ElseIf (DtpFechaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then
   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    '************************************************ANTERIOR
    'Consulta &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    'Consulta &= "into #tmp2 "
    'Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry  "
    'Consulta &= "where t0.DocDate >= @FechaIni AND t0.DocDate <= @FechaTer and "
    'Consulta &= "t1.ItemCode = 'DESCUENTO P.P' "
    'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    'ElseIf UsrTPM = "VVERGARA" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    'ElseIf UsrTPM = "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    'Else
    '    If (CkClientes.Checked = False) Then
    '        Consulta &= " and t0.slpCode <> 1 "
    '    End If
    'End If

    'Consulta &= "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DesAntesIva  "
    'Consulta &= "FROM #tmp2 t0 "
    'Consulta &= "GROUP BY t0.SlpCode ORDER BY DesAntesIva DESC "
    'Consulta &= "drop table #tmp2 "
    '************************************************ANTERIOR
    'MODIFICADO POR URIEL
    'OBTIENE LOS DESCUENTOS DE PRONTO PAGO
    Consulta &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    Consulta &= "into #FacturasPP "
    Consulta &= "from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry "
    Consulta &= "where t0.DocDate between @FechaIni and @FechaTer "
    Consulta &= "AND t0.DocType = 'I' "
    Consulta &= "AND (t1.ItemCode = 'DESCUENTO P.P' OR t1.ItemCode = 'AP_ANTICIPO') "
    If (CkClientes.Checked = False) Then
     Consulta &= " and t0.slpCode <> 1 "
    End If

    Consulta &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    Consulta &= "into #NCPP "
    Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    Consulta &= "where t0.DocDate between @FechaIni and @FechaTer "
    Consulta &= " and t0.DocType = 'I' "
    Consulta &= " AND (t1.ItemCode = 'DESCUENTO P.P' OR t1.ItemCode = 'AP_ANTICIPO') "
    If (CkClientes.Checked = False) Then
     Consulta &= " and t0.slpCode <> 1 "
    End If

    Consulta &= "select Slpcode as IdVend,(SUM((DocTotal - VatSum) - TotalExpns))* -1 as DesAntesIva "
    Consulta &= "into #tmp13 "
    Consulta &= "from #FacturasPP "
    Consulta &= "group by SlpCode "

    Consulta &= "select Slpcode as IdVend,(SUM((DocTotal - VatSum) - TotalExpns)) as DesAntesIva "
    Consulta &= "into #tmp14 "
    Consulta &= "from #NCPP "
    Consulta &= "group by SlpCode "

    Consulta &= "select t0.IdVend, t0.DesAntesIva + isnull(t1.DesAntesIva,0) as DesAntesIva "
    Consulta &= "from #tmp14 t0 left join #tmp13 t1 on t0.IdVend = t1.IdVend "

    Consulta &= "drop table #FacturasPP "
    Consulta &= "drop table #NCPP "
    Consulta &= "drop table #tmp13 "
    Consulta &= "drop table #tmp14 "
   End If
  End If
  With cmdcost
   .Connection = New Data.SqlClient.SqlConnection(StrCon)
   cmdcost.Connection.Open()
   .Parameters.Clear()
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   .CommandText = Consulta
   DtVtaDes = .ExecuteReader()
  End With

  Dim TotDes As Decimal
  Do While DtVtaDes.Read()
   For Each fila As DataRow In DTRefacciones.Rows
    'Se asignan el total de tickets que tiene el recurso
    If fila("IdVend") = DtVtaDes.Item("IdVend") Then
     If IsDBNull(DtVtaDes.Item("DesAntesIva")) Then
      fila("Des") = 0
     Else
      fila("Des") = DtVtaDes.Item("DesAntesIva")
     End If
     TotDes = TotDes + DtVtaDes.Item("DesAntesIva")
    End If

   Next
  Loop


  cmdcost.Connection.Close()

  '****************************************************
  'CANCELACIONES------------------------------------------------------------------------------------------------------------------------------------
  cmdcost.Connection.Close()
  Dim DRCanc As Data.SqlClient.SqlDataReader
  Consulta = ""
  If (DtpFechaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
    Consulta &= "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels  "
    Consulta &= "FROM ORIN t0 WHERE DocDate >= @FechaIni AND DocDate <=  @FechaTer "
    Consulta &= "AND DocType  = 'I' AND  CASE when t0.DocDate <= '2017-11-19' then EDocNum  "
    Consulta &= "else (select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end IS NULL   "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      Consulta &= " and t0.slpCode <> 1 "
     End If
    End If
    Consulta &= "GROUP BY t0.SlpCode ORDER BY TotCancels DESC "

   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    Consulta &= "SELECT distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    Consulta &= "INTO #TMP3 "
    Consulta &= "FROM ORIN t0 WHERE t0.DocDate >= @FechaIni AND t0.DocDate <= @FechaTer "
    Consulta &= "AND t0.DocType  = 'I' AND  CASE when t0.DocDate <= '2017-11-19' then EDocNum "
    Consulta &= "else (select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end IS NULL   "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      Consulta &= " and t0.slpCode <> 1 "
     End If
    End If
    '****************************************************ANTERIOR
    'Consulta &= "UNION ALL "
    'Consulta &= "SELECT distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    'Consulta &= "FROM ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry  "
    'Consulta &= "WHERE t0.DocDate >= @FechaIni AND t0.DocDate <= @FechaTer "
    'Consulta &= "and t1.ItemCode <> 'DESCUENTO P.P' and  "
    'Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) IS NULL   "
    'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    'ElseIf UsrTPM = "VVERGARA" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    'ElseIf UsrTPM = "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    'Else
    '    If (CkClientes.Checked = False) Then
    '        Consulta &= " and t0.slpCode <> 1 "
    '    End If
    'End If
    'Consulta &= "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels  "
    'Consulta &= "FROM #TMP3 T0 "
    'Consulta &= "GROUP BY T0.SlpCode ORDER BY TotCancels DESC "
    'Consulta &= "DROP TABLE #TMP3 "
    '*******************************************************ANTERIOR
    'MODIFICADO POR URIEL
    'OBTIENE LAS CANCELACIONES TOTALES
    Consulta &= "UNION ALL "
    Consulta &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    'Consulta &= "INTO #TMP3 "
    Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    Consulta &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
    Consulta &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
    Consulta &= "where t0.DocDate between @FechaIni and @FechaTer "
    Consulta &= "and t0.DocType <> 'S' "
    Consulta &= "and t2.ItmsGrpCod <> 200 "
    'Consulta &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    Consulta &= "AND ((CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate >= '2020-08-24' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    Consulta &= "AND T0.U_BXP_TIMBRADO <> 'T') "
    Consulta &= " AND T3.ReportID IS NULL "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      Consulta &= " and t0.slpCode <> 1 "
     End If
    End If
    Consulta &= "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels  "
    Consulta &= "FROM #TMP3 T0 "
    Consulta &= "GROUP BY T0.SlpCode ORDER BY TotCancels DESC "
   End If
  ElseIf (DtpFechaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then
   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    '***********************************ANTERIOR
    'Consulta &= "SELECT distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    'Consulta &= "INTO #TMP3 "
    'Consulta &= "FROM ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry  "
    'Consulta &= "WHERE t0.DocDate >= @FechaIni AND t0.DocDate <= @FechaTer "
    'Consulta &= "and t1.ItemCode <> 'DESCUENTO P.P' and  "
    'Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) IS NULL   "
    'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    'ElseIf UsrTPM = "VVERGARA" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    'ElseIf UsrTPM = "RROBLES" Then
    '    Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    'Else
    '    If (CkClientes.Checked = False) Then
    '        Consulta &= " and t0.slpCode <> 1 "
    '    End If
    'End If

    'Consulta &= "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels  "
    'Consulta &= "FROM #TMP3 T0 "
    'Consulta &= "GROUP BY T0.SlpCode ORDER BY TotCancels DESC "
    '***********************************ANTERIOR
    'MODIFICADO POR URIEL
    'OBTIENE LAS CANCELACIONES TOTALES
    Consulta &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
    Consulta &= "INTO #TMP3 "
    Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    Consulta &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
    Consulta &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
    Consulta &= "where t0.DocDate between @FechaIni and @FechaTer "
    Consulta &= "and t0.DocType <> 'S' "
    Consulta &= "and t2.ItmsGrpCod <> 200 "
    'Consulta &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    Consulta &= "AND ((CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate >= '2020-08-24' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    Consulta &= "AND T0.U_BXP_TIMBRADO <> 'T') "
    Consulta &= " AND T3.ReportID IS NULL "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     Consulta &= "AND t0.SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     Consulta &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      Consulta &= " and t0.slpCode <> 1 "
     End If
    End If
    Consulta &= "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels  "
    Consulta &= "FROM #TMP3 T0 "
    Consulta &= "GROUP BY T0.SlpCode ORDER BY TotCancels DESC "
   End If
  End If

  With cmdcost
   .Connection = New Data.SqlClient.SqlConnection(StrCon)
   cmdcost.Connection.Open()
   .Parameters.Clear()
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   .CommandText = Consulta
   DRCanc = .ExecuteReader()
  End With


  Dim TotCanc As Decimal
  Do While DRCanc.Read()
   For Each fila As DataRow In DTRefacciones.Rows
    'Se asignan el total de tickets que tiene el recurso
    If fila("IdVend") = DRCanc.Item("IdVend") Then

     If IsDBNull(DRCanc.Item("TotCancels")) Then
      fila("Cancels") = 0
     Else
      fila("Cancels") = DRCanc.Item("TotCancels")
     End If
     TotCanc = TotCanc + DRCanc.Item("TotCancels")

    End If

   Next

  Loop


  Dim vnetas As Decimal
  For Each fila As DataRow In DTRefacciones.Rows

   If fila("IdVend") = 0 Then
    fila("SlpName") = "MONTO TOTAL"
    fila("Cancels") = TotCanc
   End If

   'fila("VtaCDes") = 333
   'fila("VtasNt") = 444
   'fila("TotNC") = 555
   '**********************************************ANTERIOR
   'SE CAMBIA DEBIDO A QUE YA SE CALCULA DIRECTAMENTE LAS CANCELACIONES
   'fila("VtaCDes") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) - IIf(IsDBNull(fila("Cancels")), 0, fila("Cancels"))
   'fila("VtasNt") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) - (IIf(IsDBNull(fila("Cancels")), 0, fila("Cancels")) + IIf(IsDBNull(fila("Dev")), 0, fila("Dev")))
   '**********************************************ANTERIOR
   'MODIFICADO POR URIEL
   fila("VtaCDes") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva"))
   'fila("VtasNt") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) - IIf(IsDBNull(fila("Dev")), 0, fila("Dev"))
   '**********************************************ANTERIOR
   'fila("VtasNt") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) - (IIf(IsDBNull(fila("Cancels")), 0, fila("Cancels")) + IIf(IsDBNull(fila("Dev")), 0, fila("Dev")))
   fila("VtasNt") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva"))

   '******************************************************************************************************************************
   '** EN ALGUN MOMENTO NO CUADRARA POR QUE A VECES SI DEBE CONSIDERAR CANCELACIONES Y A VECES NO, AUN NO ENTIENDO POR QUE ******
   '******************************************************************************************************************************
   'SE CAMBIA DEBIDO A QUE NO CUADRABAN LOS REPORTES AL NO CONSIDERAR LAS CANCELACIONES (No cuadran las Ventas Netas)
   Dim Cancelaciones As Decimal = IIf(IsDBNull(fila("Cancels")), 0, fila("Cancels"))
   Dim Devoluciones As Decimal = IIf(IsDBNull(fila("Dev")), 0, fila("Dev"))
   If (fila("VtasNt") > 0) Then
    fila("VtasNt") = fila("VtasNt") - Devoluciones  'No considera las cancelaciones
   Else
    fila("VtasNt") = fila("VtasNt") - Devoluciones  'No considera las cancelaciones
    'fila("VtasNt") = fila("VtasNt") - (Cancelaciones + Devoluciones) ' Si considera las cancelaciones
   End If
   '******************************************************************************************************************************
   '******************************************************************************************************************************

   fila("TotNC") = IIf(IsDBNull(fila("Des")), 0, fila("Des")) + IIf(IsDBNull(fila("Dev")), 0, fila("Dev"))


   If fila("IdVend") <> 0 Then
    vnetas = vnetas + fila("VtaCDes")
   End If
  Next


  Dim TotVnetas As Decimal
  Dim TotNCred As Decimal

  For Each fila As DataRow In DTRefacciones.Rows
   If fila("IdVend") = 0 Then
    fila("VtaCDes") = vnetas
    fila("Dev") = TotDev
    fila("VtasNt") = TotVnetas
    fila("Des") = TotDes
    fila("TotNC") = TotNCred
    fila("Cancels") = TotCanc

    If TotDev > 0 And vnetas > 0 Then
     fila("PorDev") = fila("Dev") * 100 / vnetas
    End If


    If TotDes > 0 And vnetas > 0 Then
     fila("PorDes") = TotDes * 100 / vnetas
    End If


   End If

   If fila("IdVend") <> 0 Then
    If Not IsDBNull(fila("Dev")) And fila("VtaCDes") <> 0 Then
     If fila("Dev") <> 0 Then
      fila("PorDev") = fila("Dev") * 100 / fila("VtaCDes")
     End If
    End If


    If Not IsDBNull(fila("Des")) And fila("VtaCDes") <> 0 Then
     If fila("Des") <> 0 Then
      fila("PorDes") = fila("Des") * 100 / fila("VtaCDes")
     End If

    End If

    If Not IsDBNull(fila("Cancels")) And fila("VtaCDes") <> 0 Then
     If fila("Cancels") <> 0 Then
      fila("PorCan") = fila("Cancels") * 100 / fila("VtaCDes")
     End If

    End If

    TotVnetas = TotVnetas + fila("VtasNt")
    TotNCred = TotNCred + fila("TotNC")

   End If


   If Not IsDBNull(fila("VtaCDes")) Then
    If fila("VtaCDes") <> 0 Then
     fila("PorCDes") = fila("VtaCDes") * 100 / vnetas
    End If
   End If


  Next


  cmdcost.Connection.Close()

  '****************************************************************************************************************************

  ' crear nueva conexión    
  Dim conexion2 As New SqlConnection(StrCon)

  ' abrir la conexión con la base de datos   
  conexion2.Open()

  Dim Adaptador As New SqlDataAdapter()
  Dim comando As New SqlCommand

  Dim SQLTPD As String = ""


  '--CANCELACIONES
  If (DtpFechaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
    SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente, "
    SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
    SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE AS VARCHAR(22)) AS ArtAgte, "
    SQLTPD &= "CAST(0 AS NUMERIC(19,2)) AS CANTNETA  "
    SQLTPD &= "INTO #Tem_Canc  "
    SQLTPD &= "FROM ORIN T0 INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
    SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer  "
    SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F') AND T0.DocType  = 'I' AND   "
    SQLTPD &= "CASE when T0.DocDate <= '2017-11-19' then EDocNum  "
    SQLTPD &= "else (select ReportID from ECM2 t3 where t3.SrcObjAbs = t0.DocEntry and t3.SrcObjType = 14) end IS NULL   "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     SQLTPD &= "AND T0.SlpCode = '" & vCodAgte & "'"
    ElseIf UsrTPM = "VVERGARA" Then
     SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '03' ) "
    Else
     If (CkClientes.Checked = False) Then
      SQLTPD &= " and T0.slpCode <> 1 "
     End If
    End If
    SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode   "

   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DiscPrcnt "
    SQLTPD &= "into #tmp4 "
    SQLTPD &= "from ORIN t0  "
    SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer  "
    SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F') AND T0.DocType  = 'I' AND   "
    SQLTPD &= "CASE when T0.DocDate <= '2017-11-19' then EDocNum  "
    SQLTPD &= "else (select ReportID from ECM2 t3 where t3.SrcObjAbs = t0.DocEntry and t3.SrcObjType = 14) end IS NULL   "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     SQLTPD &= "AND T0.SlpCode = '" & vCodAgte & "'"
    ElseIf UsrTPM = "VVERGARA" Then
     SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '03' ) "
    Else
     If (CkClientes.Checked = False) Then
      SQLTPD &= " and T0.slpCode <> 1 "
     End If
    End If
    '*************************************************ANTERIOR
    'SQLTPD &= "union all "
    'SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DiscPrcnt "
    'SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer  "
    'SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F') AND  "
    'SQLTPD &= "t1.ItemCode <> 'DESCUENTO P.P' AND   "
    'SQLTPD &= "(select ReportID from ECM2 t3 where t3.SrcObjAbs = t0.DocEntry and t3.SrcObjType = 14) IS NULL   "
    'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
    '    SQLTPD &= "AND T0.SlpCode = '" & vCodAgte & "'"
    'ElseIf UsrTPM = "VVERGARA" Then
    '    SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    'ElseIf UsrTPM = "RROBLES" Then
    '    SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '03' ) "
    'Else
    '    If (CkClientes.Checked = False) Then
    '        SQLTPD &= " and T0.slpCode <> 1 "
    '    End If
    'End If
    '***************************************************ANTERIOR
    'MODIFICADO POR URIEL
    SQLTPD &= "union all "
    SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DiscPrcnt "
    SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
    SQLTPD &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
    SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer "
    SQLTPD &= "and t0.DocType <> 'S' "
    SQLTPD &= "and t2.ItmsGrpCod <> 200 "
    'SQLTPD &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    SQLTPD &= "AND ((CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate >= '2020-08-24' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    SQLTPD &= "AND T0.U_BXP_TIMBRADO <> 'T') "
    SQLTPD &= " AND T3.ReportID IS NULL "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     SQLTPD &= "AND t0.SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     SQLTPD &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     SQLTPD &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      SQLTPD &= " and t0.slpCode <> 1 "
     End If
    End If

    SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente, "
    SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
    SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE AS VARCHAR(22)) AS ArtAgte, "
    SQLTPD &= "CAST(0 AS NUMERIC(19,2)) AS CANTNETA  "
    SQLTPD &= "INTO #Tem_Canc  "
    SQLTPD &= "FROM #tmp4 T0 INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
    SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode   "
    SQLTPD &= "drop table #tmp4 "
    SQLTPD &= " "
   End If
  ElseIf (DtpFechaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then
   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    '********************************************ANTERIOR
    'SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DiscPrcnt "
    'SQLTPD &= "into #tmp4 "
    'SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer  "
    'SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F') AND  "
    'SQLTPD &= "t1.ItemCode <> 'DESCUENTO P.P' AND   "
    'SQLTPD &= "(select ReportID from ECM2 t3 where t3.SrcObjAbs = t0.DocEntry and t3.SrcObjType = 14) IS NULL   "
    'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
    '    SQLTPD &= "AND T0.SlpCode = '" & vCodAgte & "'"
    'ElseIf UsrTPM = "VVERGARA" Then
    '    SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    'ElseIf UsrTPM = "RROBLES" Then
    '    SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '03' ) "
    'Else
    '    If (CkClientes.Checked = False) Then
    '        SQLTPD &= " and T0.slpCode <> 1 "
    '    End If
    'End If
    '**********************************************ANTERIOR
    SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DiscPrcnt "
    SQLTPD &= "into #tmp4 "
    SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
    SQLTPD &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
    SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer "
    SQLTPD &= "and t0.DocType <> 'S' "
    SQLTPD &= "and t2.ItmsGrpCod <> 200 "
    'SQLTPD &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    SQLTPD &= "AND ((CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate >= '2020-08-24' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    SQLTPD &= "AND T0.U_BXP_TIMBRADO <> 'T') "
    SQLTPD &= " AND T3.ReportID IS NULL "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     SQLTPD &= "AND t0.SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     SQLTPD &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     SQLTPD &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      SQLTPD &= " and t0.slpCode <> 1 "
     End If
    End If

    SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente, "
    SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
    SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE AS VARCHAR(22)) AS ArtAgte, "
    SQLTPD &= "CAST(0 AS NUMERIC(19,2)) AS CANTNETA  "
    SQLTPD &= "INTO #Tem_Canc  "
    SQLTPD &= "FROM #tmp4 T0 INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
    SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode   "
    SQLTPD &= "drop table #tmp4 "
   End If
  End If
  '--DEVOLUCIONES
  If (DtpFechaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
    SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente, "
    SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
    SQLTPD &= "(CAST('DEVOLUCION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE AS VARCHAR(22)) AS ArtAgte, "
    SQLTPD &= "CAST(0 AS NUMERIC(19,2)) AS CANTNETA  "
    SQLTPD &= "INTO #Tem_Dev  "
    SQLTPD &= "FROM ORIN T0 INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
    SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND DocType  = 'I' AND   "
    SQLTPD &= "CASE when T0.DocDate <= '2017-11-19' then EDocNum "
    SQLTPD &= "else (select ReportID from ECM2 t3 where t3.SrcObjAbs = t0.DocEntry and t3.SrcObjType = 14) end IS NOT NULL   "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     SQLTPD &= "AND T0.SlpCode = '" & vCodAgte & "'"
    ElseIf UsrTPM = "VVERGARA" Then
     SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      SQLTPD &= " and T0.slpCode <> 1 "
     End If
    End If
    SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode  "

   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DiscPrcnt "
    SQLTPD &= "into #tmp5 "
    SQLTPD &= "from ORIN t0 "
    SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND DocType  = 'I' AND  "
    SQLTPD &= "CASE when T0.DocDate <= '2017-11-19' then EDocNum "
    SQLTPD &= "else (select ReportID from ECM2 t3 where t3.SrcObjAbs = t0.DocEntry and t3.SrcObjType = 14) end IS NOT NULL   "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     SQLTPD &= "AND T0.SlpCode = '" & vCodAgte & "'"
    ElseIf UsrTPM = "VVERGARA" Then
     SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      SQLTPD &= " and T0.slpCode <> 1 "
     End If
    End If
    '*******************************************************ANTERIOR
    'SQLTPD &= "union all "
    'SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DiscPrcnt "
    'SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer  "
    'SQLTPD &= "and t1.ItemCode <> 'DESCUENTO P.P' and  "
    'SQLTPD &= "(select ReportID from ECM2 t3 where t3.SrcObjAbs = t0.DocEntry and t3.SrcObjType = 14) IS NOT NULL  "
    'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
    '    SQLTPD &= "AND T0.SlpCode = '" & vCodAgte & "'"
    'ElseIf UsrTPM = "VVERGARA" Then
    '    SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    'ElseIf UsrTPM = "RROBLES" Then
    '    SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    'Else
    '    If (CkClientes.Checked = False) Then
    '        SQLTPD &= " and T0.slpCode <> 1 "
    '    End If
    'End If
    '*******************************************************ANTERIOR
    'MODIFICADO POR URIEL
    SQLTPD &= "union all "
    SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DiscPrcnt "
    'Consulta &= "into #NC_Art_Timb "
    SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
    SQLTPD &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
    SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer "
    SQLTPD &= "and t0.DocType <> 'S' "
    SQLTPD &= "and t2.ItmsGrpCod <> 200 "
    SQLTPD &= "AND (T3.ReportID IS NOT NULL OR T0.U_BXP_UUID IS NOT NULL) "
    SQLTPD &= "AND ((T0.U_BXP_TIMBRADO = 'T' OR T0.U_BXP_TIMBRADO = 'P') OR T0.EDocGenTyp = 'G') "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     SQLTPD &= "AND t0.SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     SQLTPD &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     SQLTPD &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      SQLTPD &= " and t0.slpCode <> 1 "
     End If
    End If
    'Consulta &= "select SlpCode as IdVend, SUM((DocTotal - VatSum) - TotalExpns) as DevAntesIva "
    'Consulta &= "from #NC_Art_Timb "
    'Consulta &= "group by SlpCode "
    'Consulta &= "order by DevAntesIva "
    'Consulta &= "drop table #NC_Art_Timb "

    SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente, "
    SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
    SQLTPD &= "(CAST('DEVOLUCION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE AS VARCHAR(22)) AS ArtAgte, "
    SQLTPD &= "CAST(0 AS NUMERIC(19,2)) AS CANTNETA  "
    SQLTPD &= "INTO #Tem_Dev  "
    SQLTPD &= "FROM #tmp5 T0 INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
    SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode   "
    SQLTPD &= "drop table #tmp5 "
   End If
  ElseIf (DtpFechaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then
   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    '************************************ANTERIOR
    'SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DiscPrcnt "
    'SQLTPD &= "into #tmp5 "
    'SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer  "
    'SQLTPD &= "and t1.ItemCode <> 'DESCUENTO P.P' and  "
    'SQLTPD &= "(select ReportID from ECM2 t3 where t3.SrcObjAbs = t0.DocEntry and t3.SrcObjType = 14) IS NOT NULL  "
    'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
    '    SQLTPD &= "AND T0.SlpCode = '" & vCodAgte & "'"
    'ElseIf UsrTPM = "VVERGARA" Then
    '    SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    'ElseIf UsrTPM = "RROBLES" Then
    '    SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    'Else
    '    If (CkClientes.Checked = False) Then
    '        SQLTPD &= " and T0.slpCode <> 1 "
    '    End If
    'End If
    '*****************************************ANTERIOR
    'MODIFICADO POR URIEL
    SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DiscPrcnt "
    SQLTPD &= "into #tmp5 "
    SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
    SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
    SQLTPD &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
    SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer "
    SQLTPD &= "and t0.DocType <> 'S' "
    SQLTPD &= "and t2.ItmsGrpCod <> 200 "
    SQLTPD &= "AND (T3.ReportID IS NOT NULL OR T0.U_BXP_UUID IS NOT NULL) "
    SQLTPD &= "AND ((T0.U_BXP_TIMBRADO = 'T' OR T0.U_BXP_TIMBRADO = 'P') OR T0.EDocGenTyp = 'G') "
    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
     SQLTPD &= "AND t0.SlpCode = '" & vCodAgte & "' "
    ElseIf UsrTPM = "VVERGARA" Then
     SQLTPD &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '07' )  "
    ElseIf UsrTPM = "RROBLES" Then
     SQLTPD &= "AND t0.SlpCode in (select SlpCode from OSLP where Memo = '03' )  "
    Else
     If (CkClientes.Checked = False) Then
      SQLTPD &= " and t0.slpCode <> 1 "
     End If
    End If

    SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente, "
    SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
    SQLTPD &= "(CAST('DEVOLUCION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE AS VARCHAR(22)) AS ArtAgte, "
    SQLTPD &= "CAST(0 AS NUMERIC(19,2)) AS CANTNETA  "
    SQLTPD &= "INTO #Tem_Dev  "
    SQLTPD &= "FROM #tmp5 T0 INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
    SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode   "
    SQLTPD &= "drop table #tmp5 "
   End If
  End If
  '--FACTURADO
  '***********************************************ANTERIOR
  'SQLTPD &= "SELECT CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS ItemCode,"
  'SQLTPD &= "SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,"
  'SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null  THEN T1.LineTotal ELSE T1.LineTotal - (T1.LineTotal * T0.DiscPrcnt / 100) END) AS TotalSinIva,"
  'SQLTPD &= "CAST(CASE WHEN T1.ItemCode IS NULL THEN 'NCARGO' ELSE 'FACTURACION' END AS CHAR(20))AS TipoMov,"
  'SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS VARCHAR(22)) AS ArtAgte,"
  'SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
  'SQLTPD &= "INTO #Tem_Fact FROM OINV T0 INNER JOIN INV1 T1 ON T1.DocEntry = T0.DocEntry "
  'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND T0.DocType <> 'S'  "
  'If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
  '    SQLTPD &= "AND T0.SlpCode = '" & vCodAgte & "'"
  'ElseIf UsrTPM = "VVERGARA" Then
  '    SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '07' ) "
  'ElseIf UsrTPM = "RROBLES" Then
  '    SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '03' ) "
  'Else
  '    If (CkClientes.Checked = False) Then
  '        SQLTPD &= " and T0.slpCode <> 1 "
  '    End If
  'End If
  'SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode  "
  '***********************************************ANTERIOR
  'MODIFICADO POR URIEL
  SQLTPD &= "SELECT CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS ItemCode,"
  SQLTPD &= "SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,"
  SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null  THEN T1.LineTotal ELSE T1.LineTotal - (T1.LineTotal * T0.DiscPrcnt / 100) END) AS TotalSinIva,"
  SQLTPD &= "CAST(CASE WHEN T1.ItemCode IS NULL THEN 'NCARGO' ELSE 'FACTURACION' END AS CHAR(20))AS TipoMov,"
  SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS VARCHAR(22)) AS ArtAgte,"
  SQLTPD &= "CAST(0 AS NUMERIC(19,2)) AS CANTNETA "
  SQLTPD &= "INTO #Tem_Fact FROM OINV T0 INNER JOIN INV1 T1 ON T1.DocEntry = T0.DocEntry "
  SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
  SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND T0.DocType <> 'S' and t2.ItmsGrpCod <> 200"
  If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
   SQLTPD &= "AND T0.SlpCode = '" & vCodAgte & "'"
  ElseIf UsrTPM = "VVERGARA" Then
   SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '07' ) "
  ElseIf UsrTPM = "RROBLES" Then
   SQLTPD &= "AND T0.SlpCode in (select SlpCode from OSLP where Memo = '03' ) "
  Else
   If (CkClientes.Checked = False) Then
    SQLTPD &= " and T0.slpCode <> 1 "
   End If
  End If
  SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode  "

  '--CONSULTA CON INSERT SE BUSCAN LOS ARTICULOS CANCELADOS QUE NO HAN SIDO FACTURADOS EN ESE DIA POR ESE AGENTE
  SQLTPD &= "INSERT #Tem_Fact (ItemCode, CANTIDAD, IdAgente,TotalSinIva,TipoMov,ArtAgte,CANTNETA) "
  SQLTPD &= "SELECT ItemCode, 0, IdAgente,0,TipoMov,ArtAgte,CANTNETA "
  SQLTPD &= "FROM #Tem_Canc AS T0 WHERE T0.ArtAgte NOT IN (SELECT T1.ArtAgte FROM #Tem_Fact T1 WHERE T1.ArtAgte IS NOT NULL)  "

  '--CONSULTA CON INSERT SE BUSCAN LOS ARTICULOS CANCELADOS QUE NO HAN SIDO FACTURADOS EN ESE DIA POR ESE AGENTE
  SQLTPD &= "INSERT #Tem_Fact (ItemCode, CANTIDAD, IdAgente,TotalSinIva,TipoMov,ArtAgte,CANTNETA) "
  SQLTPD &= "SELECT ItemCode, 0, IdAgente,0,TipoMov,ArtAgte,CANTNETA "
  SQLTPD &= "FROM #Tem_Dev AS T0 WHERE T0.ArtAgte NOT IN (SELECT T1.ArtAgte FROM #Tem_Fact T1 WHERE T1.ArtAgte IS NOT NULL)  "


  SQLTPD &= " SELECT T0.ItemCode AS Articulo,"
  SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 'NOTAS DE CARGO' ELSE T3.ItemName END AS ItemName,"
  SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 999 ELSE T3.ItmsGrpCod END AS ItmsGrpCod,"
  SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T4.ItmsGrpNam END AS ItmsGrpNam,"
  SQLTPD &= "T0.TipoMov,T0.IdAgente,T0.ArtAgte,"
  SQLTPD &= "CASE WHEN T1.CANTIDAD IS NULL THEN T0.CANTIDAD ELSE T0.CANTIDAD - T1.CANTIDAD END CANT_TOT,"
  SQLTPD &= "CASE WHEN T1.TotalSinIva IS NULL THEN T0.TotalSinIva ELSE T0.TotalSinIva - T1.TotalSinIva END MONT_TOT,"
  SQLTPD &= "CASE WHEN T2.CANTIDAD IS NULL THEN 0 ELSE T2.CANTIDAD END AS CantDev,"
  SQLTPD &= "CASE WHEN T1.CANTIDAD IS NULL THEN T0.CANTIDAD ELSE T0.CANTIDAD - T1.CANTIDAD END  - "
  SQLTPD &= "CASE WHEN T2.CANTIDAD IS NULL THEN 0 ELSE T2.CANTIDAD END AS CANT_NETA,"
  SQLTPD &= "CASE WHEN T2.TotalSinIva IS NULL THEN 0 ELSE T2.TotalSinIva END AS MontoDev,"
  SQLTPD &= "T0.TotalSinIva - "
  SQLTPD &= "(CASE WHEN T1.TotalSinIva IS NULL THEN 0 ELSE T1.TotalSinIva END + "
  SQLTPD &= "CASE WHEN T2.TotalSinIva IS NULL THEN 0 ELSE T2.TotalSinIva END) "
  SQLTPD &= "AS  MONTO_NETA "
  SQLTPD &= "INTO #T_VTA_ART FROM #Tem_Fact T0 LEFT JOIN #Tem_Canc T1 ON T0.ArtAgte = T1.ArtAgte "
  SQLTPD &= "LEFT JOIN #Tem_Dev T2 ON T0.ArtAgte = T2.ArtAgte "
  SQLTPD &= "LEFT  JOIN OITM T3 ON T0.ItemCode = T3.ItemCode "
  SQLTPD &= "LEFT JOIN OITB T4 ON T3.ItmsGrpCod = T4.ItmsGrpCod  "


  '--TOTAL DE VENTAS POR AGENTE
  SQLTPD &= "SELECT T0.IdAgente,SUM(T0.CANT_TOT) AS CANT_TOT ,SUM(T0.MONT_TOT)AS MONT_TOT,"
  SQLTPD &= "SUM(T0.CantDev) AS CantDev ,SUM(T0.MontoDev)AS MontoDev INTO #T_TOTAGTE FROM #T_VTA_ART T0 GROUP BY T0.IdAgente  "

  '--TOTAL DE VENTAS DE TODOS LOS AGENTES Y SE INSERTA EL REGISTRO EN LA TABLA TEMPORAL #T_TOTAGTE
  SQLTPD &= "INSERT INTO #T_TOTAGTE(IdAgente,CANT_TOT,MONT_TOT,CantDev,MontoDev ) "
  SQLTPD &= "SELECT 0,SUM(T0.CANT_TOT) AS CANT_TOT ,SUM(T0.MONT_TOT)AS MONT_TOT,"
  SQLTPD &= "SUM(T0.CantDev) AS CantDev ,SUM(T0.MontoDev)AS MontoDev FROM #T_TOTAGTE T0  "

  '-- TOTAL DE VENTAS POR LINEAS DE ARTICULOS DE LOS AGENTES 
  SQLTPD &= "SELECT T0.IdAgente,T0.ItmsGrpCod,SUM(T0.CANT_TOT) AS CANT_TOT ,SUM(T0.MONT_TOT)AS MONT_TOT,"
  SQLTPD &= "SUM(T0.CantDev) AS CantDev ,SUM(T0.MontoDev)AS MontoDev INTO #T_TOTGRUP FROM #T_VTA_ART T0 GROUP BY T0.IdAgente,T0.ItmsGrpCod  "


  '--CONSULTA DE LINEAS DE LOS AGENTES CON PORCENTAJE DE VENTAS AGENTE
  SQLTPD &= "SELECT T0.IdAgente,CASE WHEN T0.ItmsGrpCod IS NULL THEN 999 ELSE T0.ItmsGrpCod END AS ItmsGrpCod,CASE WHEN T2.ItmsGrpCod IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItmsGrpNam END AS ItmsGrpNam,T0.CANT_TOT ,T0.MONT_TOT,"
  SQLTPD &= "T1.MONT_TOT AS TOTAL_MONTO,"
  SQLTPD &= "CASE WHEN T0.MONT_TOT <= 0 OR  T1.MONT_TOT <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MONT_TOT * 100 / T1.MONT_TOT "
  SQLTPD &= "END AS POR_TOT,T0.CantDev ,T0.MontoDev,T1.MontoDev AS DEV_MONTO,"
  SQLTPD &= "CASE WHEN T0.MontoDev <= 0 OR  T0.MONT_TOT <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoDev * 100 / T0.MONT_TOT END AS POR_DEV,"
  SQLTPD &= "T0.CANT_TOT - T0.CantDev AS CantNeta,"
  SQLTPD &= "T0.MONT_TOT - T0.MontoDev AS MontoNeto "
  SQLTPD &= "INTO #T_GRUP_AGTE FROM #T_TOTGRUP T0 INNER JOIN #T_TOTAGTE T1 ON T0.IdAgente = T1.IdAgente "
  SQLTPD &= "LEFT JOIN OITB T2 ON T0.ItmsGrpCod = T2.ItmsGrpCod "
  SQLTPD &= "ORDER BY T0.IdAgente,ItmsGrpCod  "

  '--SE INSERTAN LAS VENTAS TOTALES POR LINEA
  SQLTPD &= "SELECT 0 AS IdAgente,T0.ItmsGrpCod,SUM(T0.CANT_TOT) AS CANT_TOT,SUM(T0.MONT_TOT) AS MONT_TOT,"
  SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev,SUM(T0.CantNeta) AS CantNeta,"
  SQLTPD &= "SUM(T0.MontoNeto) AS MontoNeto "
  SQLTPD &= "INTO #T_TOT_LINEA FROM #T_GRUP_AGTE T0 GROUP BY T0.ItmsGrpCod  "

  '--SE INSERTAN LAS VENTAS TOTALES POR LINEA CON PORCENTAJES
  SQLTPD &= "INSERT #T_GRUP_AGTE (IdAgente,ItmsGrpCod,ItmsGrpNam,CANT_TOT,MONT_TOT,TOTAL_MONTO,POR_TOT,CantDev,MontoDev,DEV_MONTO,POR_DEV,CantNeta,MontoNeto) "
  SQLTPD &= "SELECT T0.IdAgente,CASE WHEN T0.ItmsGrpCod IS NULL THEN 999 ELSE T0.ItmsGrpCod END AS ItmsGrpCod,CASE WHEN T2.ItmsGrpCod IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItmsGrpNam END AS ItmsGrpNam,T0.CANT_TOT,T0.MONT_TOT,T1.MONT_TOT AS TOTAL_MONTO,"
  SQLTPD &= "CASE WHEN T0.MONT_TOT <= 0 OR  T1.MONT_TOT <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MONT_TOT * 100 / T1.MONT_TOT "
  SQLTPD &= "END AS POR_TOT,T0.CantDev ,T0.MontoDev,T1.MontoDev AS DEV_MONTO,"
  SQLTPD &= "CASE WHEN T0.MontoDev <= 0 OR  T0.MONT_TOT <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoDev * 100 / T0.MONT_TOT END AS POR_DEV,"
  SQLTPD &= "T0.CANT_TOT - T0.CantDev AS CantNeta,"
  SQLTPD &= "T0.MONT_TOT - T0.MontoDev AS MontoNeto "
  SQLTPD &= "FROM #T_TOT_LINEA T0 "
  SQLTPD &= "INNER JOIN #T_TOTAGTE T1 ON T0.IdAgente = T1.IdAgente "
  SQLTPD &= "LEFT JOIN OITB T2 ON T0.ItmsGrpCod = T2.ItmsGrpCod  "


  ' --CONSULTA DE ARTICLOS DE LOS AGENTES CON PORCENTAJE DE VENTAS POR LINEA
  SQLTPD &= " SELECT T0.Articulo,T0.ItmsGrpNam AS Lnea,T0.CANT_TOT AS CantFact,"
  SQLTPD &= "T0.MONT_TOT AS MontFact,T1.MONT_TOT as MontoLinea,"
  SQLTPD &= "CASE WHEN T0.MONT_TOT <= 0 OR  T1.MONT_TOT <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MONT_TOT * 100 / T1.MONT_TOT "
  SQLTPD &= "END AS PorFact,"
  SQLTPD &= "T0.CantDev,T0.MontoDev,T1.MontoDev as MontLineDev,"
  SQLTPD &= "CASE WHEN T0.MontoDev <= 0 OR  T0.MONT_TOT <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoDev * 100 / T0.MONT_TOT END AS PorDev,"
  SQLTPD &= "T0.CANT_NETA AS CantNeta,T0.MONTO_NETA AS MontNeto,T0.IdAgente,T0.ItmsGrpCod "
  SQLTPD &= "INTO #T_AGTE_ART FROM #T_VTA_ART T0 "
  SQLTPD &= "INNER JOIN #T_GRUP_AGTE T1 ON T0.IdAgente = T1.IdAgente AND T0.ItmsGrpCod = T1.ItmsGrpCod "
  SQLTPD &= "ORDER BY T0.IdAgente,T0.ItmsGrpCod  "


  '--SE CONSULTA LAS VENTAS DE LOS ARTICLOS 
  SQLTPD &= "SELECT 0 AS IdAgente,T0.Articulo,SUM(T0.CANT_TOT) AS CANT_TOT,SUM(T0.MONT_TOT) AS MONT_TOT,"
  SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev "
  SQLTPD &= "INTO #T_TOTVTA_ART FROM #T_VTA_ART T0  GROUP BY T0.Articulo  "


  '--SE INSERTAN LAS VENTAS DE LOS ARTICULOS CON PORCENTAJES Y DATOS DE LINEAS
  SQLTPD &= "INSERT INTO #T_AGTE_ART (Articulo,Lnea,CantFact,MontFact,MontoLinea,PorFact,CantDev,MontoDev,MontLineDev,PorDev,CantNeta,MontNeto,IdAgente,ItmsGrpCod) "
  SQLTPD &= "SELECT CASE WHEN T0.Articulo IS NULL THEN 'NOTA-DE-CARGO' ELSE T0.Articulo END AS Articulo,CASE WHEN T2.ItmsGrpCod IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItmsGrpNam END AS Linea,T0.CANT_TOT AS CantFact,T0.MONT_TOT as MontFact,"
  SQLTPD &= "T3.MONT_TOT AS MontoLinea,"
  SQLTPD &= "CASE WHEN T0.MONT_TOT <= 0 OR  T3.MONT_TOT <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MONT_TOT * 100 / T3.MONT_TOT "
  SQLTPD &= "END AS PorFact,"
  SQLTPD &= "T0.CantDev AS CantDev,T0.MontoDev,"
  SQLTPD &= "T3.MontoDev,"
  SQLTPD &= "CASE WHEN T0.MontoDev <= 0 OR  T0.MONT_TOT <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoDev * 100 / T0.MONT_TOT END AS POR_DEV,"
  SQLTPD &= "T0.CANT_TOT - T0.CantDev AS CantNeta,"
  SQLTPD &= "T0.MONT_TOT - T0.MontoDev AS MontNeto,"
  SQLTPD &= "t0.IdAgente, CASE WHEN T1.ItmsGrpCod IS NULL THEN 999 ELSE T1.ItmsGrpCod END AS ItmsGrpCod "
  SQLTPD &= "FROM #T_TOTVTA_ART T0 LEFT JOIN OITM T1 ON T0.Articulo = T1.ItemCode "
  SQLTPD &= "LEFT JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod "
  SQLTPD &= "LEFT JOIN #T_GRUP_AGTE T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod AND T3.IdAgente = 0 ORDER BY T2.ItmsGrpCod  "


  SQLTPD &= "SELECT 'MONTO TOTAL' AS Linea, SUM(T0.CANT_TOT) AS Cantidad, SUM(T0.MONT_TOT) AS Monto, 100 AS PorVta, SUM(T0.CantDev) AS CantDev, "
  SQLTPD &= "SUM(T0.MontoDev) AS MontoDev,"
  SQLTPD &= "CASE WHEN SUM(T0.MontoDev) <= 0 OR SUM(T0.MONT_TOT) <= 0 THEN 0 ELSE "
  SQLTPD &= "SUM(T0.MontoDev) * 100 / SUM(T0.MONT_TOT) END AS POR_DEV,"
  SQLTPD &= "SUM(T0.CantNeta) AS CantNeta, SUM(T0.MontoNeto) AS MontoNeto, 'TODOS' AS Agente, "
  SQLTPD &= "999 as ItmsGrpCod, CAST(T0.IdAgente AS SMALLINT) AS IdVend,'TODOS' as LineaAgte, T0.IdAgente, 1 as Ordena "
  SQLTPD &= "FROM #T_GRUP_AGTE T0 GROUP BY T0.IdAgente "
  SQLTPD &= "UNION ALL "

  '----------------------------------------------------------------
  'Se consultan todos los montos totales por linea y tambien por linea agente
  SQLTPD &= "SELECT T0.ItmsGrpNam AS Linea,T0.CANT_TOT AS Cantidad,T0.MONT_TOT AS Monto,T0.POR_TOT AS PorVta,T0.CantDev,"
  SQLTPD &= "T0.MontoDev,T0.POR_DEV,T0.CantNeta,T0.MontoNeto,CASE WHEN T1.SlpName IS NULL THEN 'TODOS' ELSE T1.SlpName END AS Agente,"
  SQLTPD &= "T0.ItmsGrpCod,CAST(T0.IdAgente AS SMALLINT) AS IdVend,CAST(T0.ItmsGrpCod AS VARCHAR(6)) + CAST(T0.IdAgente AS VARCHAR(4)) as LineaAgte, T0.IdAgente, 0 as Ordena "
  SQLTPD &= "FROM #T_GRUP_AGTE T0 LEFT JOIN OSLP T1 ON T0.IdAgente = T1.SlpCode ORDER BY T0.IdAgente; "


  'Se consultan todos los montos totales por articulo y tambien por articulo,linea,agente
  'SQLTPD &= "SELECT CASE WHEN T0.Articulo IS NULL THEN 'NOTA-DE-CARGO' ELSE T0.Articulo END AS Articulo,CASE WHEN T1.ItemCode IS NULL THEN 'NOTAS DE CARGO' ELSE T1.ItemName END AS Descripcion,T0.CantFact,T0.MontFact,T0.PorFact,T0.CantDev,"
  SQLTPD &= "SELECT CASE WHEN T0.Articulo IS NULL THEN 'NOTA-DE-CARGO' ELSE T0.Articulo END AS Articulo, "
  SQLTPD &= "CASE WHEN T1.ItemCode IS NULL THEN 'NOTAS DE CARGO' ELSE T1.ItemName END AS Descripcion,T0.Lnea,T0.CantFact, T1.OnHand-T3.OnHand,T0.MontFact,T0.PorFact,T0.CantDev, "
  SQLTPD &= "T0.MontoDev,T0.PorDev,T0.CantNeta,T0.MontNeto,CASE WHEN T2.SlpName IS NULL THEN 'TODOS' ELSE T2.SlpName END AS Agente, "
  SQLTPD &= "T0.IdAgente,T0.ItmsGrpCod, "
  SQLTPD &= "CAST(T0.ItmsGrpCod AS VARCHAR(6)) + CAST(T0.IdAgente AS VARCHAR(4)) as LineaAgte,T0.MontLineDev,T0.MontoLinea " 'T0.MontoLinea  MontNeto
  SQLTPD &= "FROM #T_AGTE_ART T0 LEFT JOIN OITM T1 ON T0.Articulo = T1.ItemCode "
  SQLTPD &= "LEFT JOIN OSLP T2 ON T0.IdAgente = T2.SlpCode "
  SQLTPD &= "INNER JOIN OITW T3 ON T0.Articulo = T3.ItemCode AND T3.WhsCode = 02 ORDER BY IdAgente,T0.MontoLinea - T0.MontLineDev  DESC, T0.MontNeto DESC;  "
  'PorFact


  '*******************************

  'SQLTPD &= "SELECT 'MONTO TOTAL' AS Linea,SUM(T0.CANT_TOT) AS Cantidad,SUM(T0.MONT_TOT) AS Monto,100 AS PorVta,SUM(T0.CantDev) AS CantDev,"
  'SQLTPD &= "SUM(T0.MontoDev) AS MontoDev,"
  'SQLTPD &= "0 AS POR_DEV,"
  'SQLTPD &= "SUM(T0.CantNeta) AS CantNeta,SUM(T0.MontoNeto) AS  MontoNeto,'TODOS' AS Agente,"
  'SQLTPD &= "'MONTO TOTAL' AS ItmsGrpCod,0 AS IdVend,'TODOS' as LineaAgte "
  'SQLTPD &= "FROM #T_GRUP_AGTE T0 ORDER BY T0.IdAgente,T0.MontoNeto DESC; "

  '*******************************
  'SQLTPD &= "T0.POR_DEV,"

  SQLTPD &= "DROP TABLE #Tem_Canc  "
  SQLTPD &= "DROP TABLE #Tem_Dev  "
  SQLTPD &= "DROP TABLE #Tem_Fact  "
  SQLTPD &= "DROP TABLE #T_VTA_ART  "
  SQLTPD &= "DROP TABLE #T_TOTAGTE  "
  SQLTPD &= "DROP TABLE #T_TOTGRUP  "
  SQLTPD &= "DROP TABLE #T_GRUP_AGTE  "
  SQLTPD &= "DROP TABLE #T_TOT_LINEA  "
  SQLTPD &= "DROP TABLE #T_AGTE_ART  "
  SQLTPD &= "DROP TABLE #T_TOTVTA_ART  "


  ' Nuevo objeto Dataset   
  Dim DsVtasDet As New DataSet

  DTRefacciones.TableName = "EncVtasTot"

  DsVtasDet.Tables.Add(DTRefacciones)


  With comando
   .CommandTimeout = 360
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   ' Asignar el sql para seleccionar los datos de la tabla Maestro   
   .CommandText = SQLTPD
   .Connection = conexion2
  End With


  With Adaptador
   .SelectCommand = comando
   ' llenar el dataset   
   .TableMappings.Add("DetLinea", "DetArticulo")
   .Fill(DsVtasDet, "DetLineas")
  End With


  DsVtasDet.Tables(1).TableName = "DetalleLinea"
  DsVtasDet.Tables(2).TableName = "DetalleArticulo"


  DvDlinea.Table = DsVtasDet.Tables("DetalleLinea")

  '**********************************************************************************************************************************

  With GrdConProd
   .DataMember = "EncVtasTot"
   .DataSource = DsVtasDet
   'Color de Renglones en Grid
   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .RowHeadersVisible = False
   .SelectionMode = DataGridViewSelectionMode.FullRowSelect
   .MultiSelect = False

   .AllowUserToAddRows = False
   .ReadOnly = True
   .Columns(0).HeaderText = "Id"
   .Columns(0).Width = 19
   '20'15'5,5,5,5
   .Columns(1).HeaderText = "Vendedor"
   .Columns(1).Width = 150

   .Columns(2).HeaderText = "Venta Total"
   .Columns(2).Width = 77
   .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(3).HeaderText = "% Vtas. Tot."
   .Columns(3).Width = 41
   .Columns(3).DefaultCellStyle.Format = "###.00"
   .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(4).HeaderText = "Monto Devuelto"
   .Columns(4).Width = 67
   .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(4).DefaultCellStyle.Format = "###,###,###.00"

   .Columns(5).HeaderText = "% Dvol. Sobre Venta"
   .Columns(5).Width = 37
   .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(5).DefaultCellStyle.Format = "###.00"

   .Columns(6).HeaderText = "Ventas Netas"
   .Columns(6).Width = 80
   .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(6).DefaultCellStyle.Format = "###,###,###.00"

   .Columns(7).HeaderText = "Descuentos Pronto Pago"
   .Columns(7).Width = 72
   .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(7).DefaultCellStyle.Format = "###,###,###.00"

   .Columns(8).HeaderText = "% PP Sobre Venta"
   .Columns(8).Width = 40
   .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(8).DefaultCellStyle.Format = "###.00"

   .Columns(9).HeaderText = "Total Notas de Credito"
   .Columns(9).Width = 75
   .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(9).DefaultCellStyle.Format = "###,###,###.00"

   .Columns(10).HeaderText = "Cancelaciones Antes IVA"
   .Columns(10).Width = 90
   .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(10).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(10).Visible = False

   .Columns(11).HeaderText = "% Canc. Desc."
   .Columns(11).Width = 45
   .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(11).DefaultCellStyle.Format = "###.00"
   .Columns(11).Visible = False

   .Columns(12).HeaderText = "Facturación"
   .Columns(12).Width = 120
   .Columns(12).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(12).Visible = False

   .Columns(13).HeaderText = "% Facturación"
   .Columns(13).Width = 100
   .Columns(13).DefaultCellStyle.Format = "###.00"
   .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(13).Visible = False

   .SelectionMode = DataGridViewSelectionMode.FullRowSelect
   .DefaultCellStyle.BackColor = Color.AliceBlue
  End With




  DvDlinea.Sort = "Ordena, MontoNeto DESC"
  With GrdConLinea
   .DataSource = DvDlinea

   'Color de Renglones en Grid
   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .RowHeadersVisible = False
   .SelectionMode = DataGridViewSelectionMode.FullRowSelect
   .MultiSelect = False
   .AllowUserToAddRows = False
   .ReadOnly = True
   .Columns(0).HeaderText = "Linea"
   .Columns(0).Width = 150

   .Columns(1).HeaderText = "Cantidad Piezas"
   .Columns(1).Width = 66
   .Columns(1).DefaultCellStyle.Format = "###,###,###"
   .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(2).HeaderText = "$ Venta Total"
   .Columns(2).Width = 78
   .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(3).HeaderText = "% Sobre Vta."
   .Columns(3).Width = 50
   .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(3).DefaultCellStyle.Format = "###.00"

   .Columns(4).HeaderText = "Cant. Piezas Devueltas"
   .Columns(4).Width = 60
   .Columns(4).DefaultCellStyle.Format = "###,###,###"
   .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(5).HeaderText = "$ Monto Devuelto"
   .Columns(5).Width = 69
   .Columns(5).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(6).HeaderText = "% Dev. Sobre Vta."
   .Columns(6).Width = 40
   .Columns(6).DefaultCellStyle.Format = "###.00"
   .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(7).HeaderText = "Cant. Neta"
   .Columns(7).Width = 70
   .Columns(7).DefaultCellStyle.Format = "###,###,###"
   .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(8).HeaderText = "$ Monto Neto"
   .Columns(8).Width = 81
   .Columns(8).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(9).Visible = False
   .Columns(10).Visible = False
   .Columns(11).Visible = False
   .Columns(12).Visible = False
   .Columns(13).Visible = False
   .Columns(14).Visible = False
  End With

  'DsVtasDet.Tables(2)
  dv.Table = DsVtasDet.Tables("DetalleArticulo")
  dvTodosArt.Table = DsVtasDet.Tables("DetalleArticulo")

  GrdTodosArt.DataSource = dvTodosArt

  dv.Sort = "MontNeto DESC"
  With GrdDetArt
   .DataSource = dv
   'Color de Renglones en Grid
   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

   .RowHeadersWidth = 15
   'Propiedad para no mostrar el cuadro que se encuentra en la parte
   'Superior Izquierda del gridview
   '.RowHeadersVisible = False
   '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
   .MultiSelect = True
   .AllowUserToAddRows = False
   .AllowDrop = False
   .AllowUserToDeleteRows = False


   .Columns(0).HeaderText = "Articulo"
   .Columns(0).Width = 100
   .Columns(0).Frozen = True


   .Columns(1).HeaderText = "Descripción"
   .Columns(1).Width = 159
   .Columns(1).Frozen = True

   .Columns(2).HeaderText = "Linea"
   .Columns(2).Width = 70

   .Columns(3).HeaderText = "Cant. Piezas"
   .Columns(3).Width = 50
   .Columns(3).DefaultCellStyle.Format = "###,###,###"
   .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(4).HeaderText = "Stock"
   .Columns(4).Width = 35
   .Columns(4).DefaultCellStyle.Format = "###,###,###"
   .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(5).HeaderText = "$ Venta Total"
   .Columns(5).Width = 75
   .Columns(5).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(6).HeaderText = "% Sobre Vta."
   .Columns(6).Width = 40
   .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(6).DefaultCellStyle.Format = "###.00"

   .Columns(7).HeaderText = "Cant. Piezas Dev."
   .Columns(7).Width = 40
   .Columns(7).DefaultCellStyle.Format = "###,###,###"
   .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(8).HeaderText = "$ Monto Devuelto"
   .Columns(8).Width = 55
   .Columns(8).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(9).HeaderText = "% Dev. Sobre Vta."
   .Columns(9).Width = 40
   .Columns(9).DefaultCellStyle.Format = "###.00"
   .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(10).HeaderText = "Cant. Neta"
   .Columns(10).Width = 50
   .Columns(10).DefaultCellStyle.Format = "###,###,###"
   .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(11).HeaderText = "$ Monto Neto"
   .Columns(11).Width = 65
   .Columns(11).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   '.Columns(12).Visible = False


   .Columns(12).HeaderText = "Vendedor"
   .Columns(12).Width = 65

   .Columns(13).Visible = False
   .Columns(14).Visible = False
   .Columns(15).Visible = False
   .Columns(16).Visible = False
   .Columns(17).Visible = False

  End With

  ' cerrar la conexíón   
  With conexion2
   If .State = ConnectionState.Open Then
    .Close()
   End If
   .Dispose()
  End With

 End Sub

 Private Sub BtnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExcel.Click
  If MessageBox.Show("¿Desea exportar con nuevo estilo?", "Pregunta...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
   ExportNuevo()
  Else
   ExportarViejo()
  End If

 End Sub

 Sub ExportarViejo()
  Dim oExcel As Object
  Dim oBook As Object
  Dim oSheet As Object

  'Abrimos un nuevo libro
  oExcel = CreateObject("Excel.Application")
  oBook = oExcel.workbooks.add
  oSheet = oBook.worksheets(1)

  'Declaramos el nombre de las columnas
  oSheet.range("A3").value = "Clave"
  oSheet.range("B3").value = "Vendedor"
  oSheet.range("C3").value = "Ventas Totales"
  oSheet.range("D3").value = "% Vtas. Tot."
  oSheet.range("E3").value = "Monto Devuelto"
  oSheet.range("F3").value = "% Dvol. Sobre Venta"
  oSheet.range("G3").value = "Ventas Netas"
  oSheet.range("H3").value = "Descuentos Pronto Pago"
  oSheet.range("I3").value = "% PP Sobre Venta"
  oSheet.range("J3").value = "Total Notas de Credito"


  'para poner la primera fila de los titulos en negrita
  oSheet.range("A3:L3").font.bold = True
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
   Dim cel6 As String = IIf(IsDBNull(Me.GrdConProd.Item(5, fila_dt).Value), 0, Me.GrdConProd.Item(5, fila_dt).Value)
   Dim cel7 As String = IIf(IsDBNull(Me.GrdConProd.Item(6, fila_dt).Value), 0, Me.GrdConProd.Item(6, fila_dt).Value)
   Dim cel8 As String = IIf(IsDBNull(Me.GrdConProd.Item(7, fila_dt).Value), 0, Me.GrdConProd.Item(7, fila_dt).Value)

   Dim cel9 As String = IIf(IsDBNull(Me.GrdConProd.Item(8, fila_dt).Value), 0, Me.GrdConProd.Item(8, fila_dt).Value)
   Dim cel10 As String = IIf(IsDBNull(Me.GrdConProd.Item(9, fila_dt).Value), 0, Me.GrdConProd.Item(9, fila_dt).Value)

   fila_dt_excel = fila_dt + 4

   'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
   oSheet.range("A" & fila_dt_excel).value = cel1
   oSheet.range("B" & fila_dt_excel).value = cel2
   oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel3, 2)
   oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
   oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
   oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
   oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
   oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)

   oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
   oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel10, 2)

  Next

  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  oSheet.columns("A:N").entirecolumn.autofit()
  oSheet.range("A1").value = "Reporte de Ventas Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
  oSheet.range("C1").value = Rangos
  oSheet.range("C2").value = Rangos2

  oExcel.visible = True
  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  GC.Collect()
  oSheet = Nothing
  oBook = Nothing
  oExcel = Nothing
 End Sub

 Sub ExportNuevo()
  Dim ds As DataSet = GrdConProd.DataSource
  Dim dt As DataTable = ds.Tables(0)

  Using wb As New XLWorkbook()
   wb.Worksheets.Add(dt, "Ventas Agente - Linea (Agentes)")

   Dim index As Integer = 2

   For i As Integer = 0 To dt.Rows.Count

    Try
     Dim row As DataRow = dt.Rows(i)

     'Encabezados dependiendo sucursal
     If i = 1 Then
      Dim cellA0 As String = String.Format("A{0}", i)
      wb.Worksheet(1).Cells(cellA0).Value = "Clave"

      Dim cellB0 As String = String.Format("B{0}", i)
      wb.Worksheet(1).Cells(cellB0).Value = "Vendedor"

      Dim cellC0 As String = String.Format("C{0}", i)
      wb.Worksheet(1).Cells(cellC0).Value = "Ventas Totales"

      Dim cellD0 As String = String.Format("D{0}", i)
      wb.Worksheet(1).Cells(cellD0).Value = "% Vtas. Tot."

      Dim cellE0 As String = String.Format("E{0}", i)
      wb.Worksheet(1).Cells(cellE0).Value = "Monto Devuelto"

      Dim cellF0 As String = String.Format("F{0}", i)
      wb.Worksheet(1).Cells(cellF0).Value = "% Dvol. Sobre Venta"

      Dim cellG0 As String = String.Format("G{0}", i)
      wb.Worksheet(1).Cells(cellG0).Value = "Ventas Netas"

      Dim cellH0 As String = String.Format("H{0}", i)
      wb.Worksheet(1).Cells(cellH0).Value = "Descuentos Pronto Pago"

      Dim cellI0 As String = String.Format("I{0}", i)
      wb.Worksheet(1).Cells(cellI0).Value = "% PP Sobre Venta"

      Dim cellJ0 As String = String.Format("J{0}", i)
      wb.Worksheet(1).Cells(cellJ0).Value = "Total Notas de Credito"

     End If

     'Formato de cada una de las celdas
     Dim cellA As String = String.Format("A{0}", index)
     'wb.Worksheet(1).Cells(cellA).Value = CStr(row(0))

     Dim cellB As String = String.Format("B{0}", index)
     wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellC As String = String.Format("C{0}", index)
     wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellD As String = String.Format("D{0}", index)
     wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "0.0 %"

     Dim cellE As String = String.Format("E{0}", index)
     wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellF As String = String.Format("F{0}", index)
     wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "0.0 %"

     Dim cellG As String = String.Format("G{0}", index)
     wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellH As String = String.Format("H{0}", index)
     wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellI As String = String.Format("I{0}", index)
     wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "0.0 %"

     Dim cellJ As String = String.Format("J{0}", index)
     wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "$ #,##0.00"

    Catch ex As Exception

    End Try

    index = index + 1
   Next

   wb.Worksheet(1).Columns("K").Hide()
   wb.Worksheet(1).Columns("L").Hide()
   wb.Worksheet(1).Columns("M").Hide()
   wb.Worksheet(1).Columns("N").Hide()
   wb.Worksheet(1).Columns().AdjustToContents()

   Try
    Dim saveFileDialog1 As New SaveFileDialog()
    saveFileDialog1.Filter = "Excel|*.xlsx"
    saveFileDialog1.Title = "Save Excel File"
    saveFileDialog1.FileName = "Ventas Agente - Linea (Agentes) de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"
    saveFileDialog1.ShowDialog()
    saveFileDialog1.InitialDirectory = "C:/"

    If saveFileDialog1.FileName <> "" Then
     Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
     fs.Close()
    End If

    Dim strFileName As String = saveFileDialog1.FileName
    wb.SaveAs(strFileName)
    Process.Start(saveFileDialog1.FileName)
   Catch ex As Exception
    MessageBox.Show("Error al guardar el archivo: " + ex.ToString)
   End Try
  End Using
 End Sub

 Private Sub GrdConLinea_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdConLinea.SelectionChanged

  FiltraDetArticulos()

 End Sub

 Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
  If MessageBox.Show("¿Deseas exportar con el nuevo formato?", "Pregunta...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
   ExportNuevoLinea()
  Else
   ExportarViejoLinea()
  End If
 End Sub

 Sub ExportarViejoLinea()
  Dim oExcel As Object
  Dim oBook As Object
  Dim oSheet As Object

  'Abrimos un nuevo libro
  oExcel = CreateObject("Excel.Application")
  oBook = oExcel.workbooks.add
  oSheet = oBook.worksheets(1)

  'Declaramos el nombre de las columnas
  oSheet.range("A3").value = "Linea"
  oSheet.range("B3").value = "Cantidad Piezas"
  oSheet.range("C3").value = "$ Venta Total"
  oSheet.range("D3").value = "% Sobre Vta."
  oSheet.range("E3").value = "Piezas Devueltas"
  oSheet.range("F3").value = "$ Monto Devuelto"
  oSheet.range("G3").value = "% Dev.Sobre Vta."
  oSheet.range("H3").value = "Cant. Neta"
  oSheet.range("I3").value = "$ Monto Neto"
  oSheet.range("J3").value = "Agente"

  'para poner la primera fila de los titulos en negrita
  oSheet.range("A3:L3").font.bold = True
  Dim fila_dt As Integer = 0
  Dim fila_dt_excel As Integer = 0
  Dim tanto_porcentaje As String = ""
  Dim marikona As Integer = 0

  Dim total_reg As Integer = 0

  total_reg = Me.GrdConLinea.RowCount
  For fila_dt = 0 To total_reg - 1


   'para leer una celda en concreto
   'el numero es la columna
   Dim cel1 As String = Me.GrdConLinea.Item(0, fila_dt).Value
   Dim cel2 As String = Me.GrdConLinea.Item(1, fila_dt).Value
   Dim cel3 As String = IIf(IsDBNull(Me.GrdConLinea.Item(2, fila_dt).Value), 0, Me.GrdConLinea.Item(2, fila_dt).Value)
   Dim cel4 As String = IIf(IsDBNull(Me.GrdConLinea.Item(3, fila_dt).Value), 0, Me.GrdConLinea.Item(3, fila_dt).Value)
   Dim cel5 As String = IIf(IsDBNull(Me.GrdConLinea.Item(4, fila_dt).Value), 0, Me.GrdConLinea.Item(4, fila_dt).Value)
   Dim cel6 As String = IIf(IsDBNull(Me.GrdConLinea.Item(5, fila_dt).Value), 0, Me.GrdConLinea.Item(5, fila_dt).Value)
   Dim cel7 As String = IIf(IsDBNull(Me.GrdConLinea.Item(6, fila_dt).Value), 0, Me.GrdConLinea.Item(6, fila_dt).Value)
   Dim cel8 As String = IIf(IsDBNull(Me.GrdConLinea.Item(7, fila_dt).Value), 0, Me.GrdConLinea.Item(7, fila_dt).Value)

   Dim cel9 As String = IIf(IsDBNull(Me.GrdConLinea.Item(8, fila_dt).Value), 0, Me.GrdConLinea.Item(8, fila_dt).Value)
   Dim cel10 As String = IIf(IsDBNull(Me.GrdConLinea.Item(9, fila_dt).Value), 0, Me.GrdConLinea.Item(9, fila_dt).Value)

   fila_dt_excel = fila_dt + 4

   'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
   oSheet.range("A" & fila_dt_excel).value = cel1
   oSheet.range("B" & fila_dt_excel).value = FormatNumber(cel2, 2)
   oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel3, 2)
   oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
   oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
   oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
   oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
   oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)
   oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
   oSheet.range("J" & fila_dt_excel).value = cel10
  Next

  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  oSheet.columns("A:N").entirecolumn.autofit()
  oSheet.range("A1").value = "Reporte de Ventas Con Detalle de Lineas Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
  oSheet.range("C1").value = Rangos
  oSheet.range("C2").value = Rangos2

  oExcel.visible = True
  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  GC.Collect()
  oSheet = Nothing
  oBook = Nothing
  oExcel = Nothing
 End Sub

 Sub ExportNuevoLinea()
  Dim dv As DataView = DirectCast(GrdConLinea.DataSource, DataView)
  Dim dt As DataTable = dv.Table

  Using wb As New XLWorkbook()
   wb.Worksheets.Add(dt, "Ventas Agente - Linea (Linea)")
   Dim index As Integer = 2

   For i As Integer = 0 To dt.Rows.Count

    Try
     Dim row As DataRow = dt.Rows(i)

     'Encabezados dependiendo sucursal
     If i = 1 Then
      Dim cellA0 As String = String.Format("A{0}", i)
      wb.Worksheet(1).Cells(cellA0).Value = "Linea"

      Dim cellB0 As String = String.Format("B{0}", i)
      wb.Worksheet(1).Cells(cellB0).Value = "Cantidad Piezas"

      Dim cellC0 As String = String.Format("C{0}", i)
      wb.Worksheet(1).Cells(cellC0).Value = "$ Venta Total"

      Dim cellD0 As String = String.Format("D{0}", i)
      wb.Worksheet(1).Cells(cellD0).Value = "% Sobre Vta."

      Dim cellE0 As String = String.Format("E{0}", i)
      wb.Worksheet(1).Cells(cellE0).Value = "Piezas Devueltas"

      Dim cellF0 As String = String.Format("F{0}", i)
      wb.Worksheet(1).Cells(cellF0).Value = "$ Monto Devuelto"

      Dim cellG0 As String = String.Format("G{0}", i)
      wb.Worksheet(1).Cells(cellG0).Value = "% Dev.Sobre Vta."

      Dim cellH0 As String = String.Format("H{0}", i)
      wb.Worksheet(1).Cells(cellH0).Value = "Cant. Neta"

      Dim cellI0 As String = String.Format("I{0}", i)
      wb.Worksheet(1).Cells(cellI0).Value = "$ Monto Neto"

      Dim cellJ0 As String = String.Format("J{0}", i)
      wb.Worksheet(1).Cells(cellJ0).Value = "Agente"

     End If

     'Formato de cada una de las celdas
     Dim cellA As String = String.Format("A{0}", index)
     'wb.Worksheet(1).Cells(cellA).Value = CStr(row(0))

     Dim cellB As String = String.Format("B{0}", index)
     wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "#,##0.00"

     Dim cellC As String = String.Format("C{0}", index)
     wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellD As String = String.Format("D{0}", index)
     wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "% 0.0"

     Dim cellE As String = String.Format("E{0}", index)
     wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "#,##0"

     Dim cellF As String = String.Format("F{0}", index)
     wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellG As String = String.Format("G{0}", index)
     wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "% 0.0"

     Dim cellH As String = String.Format("H{0}", index)
     wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellI As String = String.Format("I{0}", index)
     wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellJ As String = String.Format("J{0}", index)
     'wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "$ #,##0.00"


    Catch ex As Exception

    End Try

    index = index + 1
   Next

   wb.Worksheet(1).Columns(11).Hide()
   wb.Worksheet(1).Columns(12).Hide()
   wb.Worksheet(1).Columns(13).Hide()
   wb.Worksheet(1).Columns(14).Hide()
   wb.Worksheet(1).Columns(15).Hide()
   wb.Worksheet(1).Columns().AdjustToContents()

   Try
    Dim saveFileDialog1 As New SaveFileDialog()
    saveFileDialog1.Filter = "Excel|*.xlsx"
    saveFileDialog1.Title = "Save Excel File"
    saveFileDialog1.FileName = "Ventas Agente - Linea (Linea) de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"
    saveFileDialog1.ShowDialog()
    saveFileDialog1.InitialDirectory = "C:/"

    If saveFileDialog1.FileName <> "" Then
     Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
     fs.Close()
    End If

    Dim strFileName As String = saveFileDialog1.FileName
    wb.SaveAs(strFileName)
    Process.Start(saveFileDialog1.FileName)
   Catch ex As Exception
    MessageBox.Show("Error al guardar el archivo: " + ex.ToString)
   End Try

  End Using
 End Sub

 Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
  If MessageBox.Show("¿Deseas emportar con nuevo estilo?", "Pregunta...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
   ExportNuevoArticulo()
  Else
   ExportViejoArticulo()
  End If
 End Sub

 Sub ExportViejoArticulo()
  Dim oExcel As Object
  Dim oBook As Object
  Dim oSheet As Object


  'Abrimos un nuevo libro
  oExcel = CreateObject("Excel.Application")
  oBook = oExcel.workbooks.add
  oSheet = oBook.worksheets(1)

  'Declaramos el nombre de las columnas
  oSheet.range("A3").value = "Articulo"
  oSheet.range("B3").value = "Descripción"
  oSheet.range("C3").value = "Linea"
  oSheet.range("D3").value = "Cantidad Piezas"
  oSheet.range("E3").value = "Stock"
  oSheet.range("F3").value = "$ Venta Total"
  oSheet.range("G3").value = "% Sobre Vta."
  oSheet.range("H3").value = "Cant.Piezas Dev."
  oSheet.range("I3").value = "$ Monto Devuelto"
  oSheet.range("J3").value = "% Dev. Sobre Vta."
  oSheet.range("K3").value = "Cant. Neta"
  oSheet.range("L3").value = "$ Monto Neto"

  oSheet.range("M3").value = "Agente"


  'para poner la primera fila de los titulos en negrita
  oSheet.range("A3:M3").font.bold = True
  Dim fila_dt As Integer = 0
  Dim fila_dt_excel As Integer = 0
  Dim tanto_porcentaje As String = ""
  Dim marikona As Integer = 0

  Dim total_reg As Integer = 0

  total_reg = Me.GrdDetArt.RowCount
  For fila_dt = 0 To total_reg - 1


   'para leer una celda en concreto
   'el numero es la columna
   Dim cel1 As String = Me.GrdDetArt.Item(0, fila_dt).Value
   Dim cel2 As String = Me.GrdDetArt.Item(1, fila_dt).Value
   Dim cel3 As String = IIf(IsDBNull(Me.GrdDetArt.Item(2, fila_dt).Value), 0, Me.GrdDetArt.Item(2, fila_dt).Value)
   Dim cel4 As String = IIf(IsDBNull(Me.GrdDetArt.Item(3, fila_dt).Value), 0, Me.GrdDetArt.Item(3, fila_dt).Value)
   Dim cel5 As String = IIf(IsDBNull(Me.GrdDetArt.Item(4, fila_dt).Value), 0, Me.GrdDetArt.Item(4, fila_dt).Value)
   Dim cel6 As String = IIf(IsDBNull(Me.GrdDetArt.Item(5, fila_dt).Value), 0, Me.GrdDetArt.Item(5, fila_dt).Value)
   Dim cel7 As String = IIf(IsDBNull(Me.GrdDetArt.Item(6, fila_dt).Value), 0, Me.GrdDetArt.Item(6, fila_dt).Value)
   Dim cel8 As String = IIf(IsDBNull(Me.GrdDetArt.Item(7, fila_dt).Value), 0, Me.GrdDetArt.Item(7, fila_dt).Value)

   Dim cel9 As String = IIf(IsDBNull(Me.GrdDetArt.Item(8, fila_dt).Value), 0, Me.GrdDetArt.Item(8, fila_dt).Value)
   Dim cel10 As String = IIf(IsDBNull(Me.GrdDetArt.Item(9, fila_dt).Value), 0, Me.GrdDetArt.Item(9, fila_dt).Value)
   Dim cel11 As String = IIf(IsDBNull(Me.GrdDetArt.Item(10, fila_dt).Value), 0, Me.GrdDetArt.Item(10, fila_dt).Value)
   Dim cel12 As String = IIf(IsDBNull(Me.GrdDetArt.Item(11, fila_dt).Value), 0, Me.GrdDetArt.Item(11, fila_dt).Value)
   Dim cel13 As String = IIf(IsDBNull(Me.GrdDetArt.Item(12, fila_dt).Value), 0, Me.GrdDetArt.Item(12, fila_dt).Value)

   fila_dt_excel = fila_dt + 4

   'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
   oSheet.range("A" & fila_dt_excel).value = cel1
   oSheet.range("B" & fila_dt_excel).value = cel2
   oSheet.range("C" & fila_dt_excel).value = cel3
   oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
   oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
   oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
   oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
   oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)

   oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
   oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel10, 2)
   oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel11, 2)
   oSheet.range("L" & fila_dt_excel).value = cel12
   oSheet.range("M" & fila_dt_excel).value = cel13

  Next


  oSheet.Columns("A:A").NumberFormat = "@"
  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  oSheet.columns("A:N").entirecolumn.autofit()
  oSheet.range("A1").value = "Reporte de Ventas Con Detalle de Articulos Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
  oSheet.range("C1").value = Rangos
  oSheet.range("C2").value = Rangos2

  oExcel.visible = True
  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  GC.Collect()
  oSheet = Nothing
  oBook = Nothing
  oExcel = Nothing
 End Sub

 Sub ExportNuevoArticulo()
  Dim dv As DataView = DirectCast(GrdDetArt.DataSource, DataView)
  Dim dt As DataTable = dv.Table

  Using wb As New XLWorkbook()
   wb.Worksheets.Add(dt, "Ventas Agente-Linea (Articulo)")
   Dim index As Integer = 2

   For i As Integer = 0 To dt.Rows.Count

    Try
     Dim row As DataRow = dt.Rows(i)

     'Encabezados dependiendo sucursal
     If i = 1 Then
      Dim cellA0 As String = String.Format("A{0}", i)
      wb.Worksheet(1).Cells(cellA0).Value = "Articulo"

      Dim cellB0 As String = String.Format("B{0}", i)
      wb.Worksheet(1).Cells(cellB0).Value = "Descripción"

      Dim cellC0 As String = String.Format("C{0}", i)
      wb.Worksheet(1).Cells(cellC0).Value = "Linea"

      Dim cellD0 As String = String.Format("D{0}", i)
      wb.Worksheet(1).Cells(cellD0).Value = "Cantidad Piezas"

      Dim cellE0 As String = String.Format("E{0}", i)
      wb.Worksheet(1).Cells(cellE0).Value = "Stock"

      Dim cellF0 As String = String.Format("F{0}", i)
      wb.Worksheet(1).Cells(cellF0).Value = "$ Venta Total"

      Dim cellG0 As String = String.Format("G{0}", i)
      wb.Worksheet(1).Cells(cellG0).Value = "% Sobre Vta."

      Dim cellH0 As String = String.Format("H{0}", i)
      wb.Worksheet(1).Cells(cellH0).Value = "Cant.Piezas Dev."

      Dim cellI0 As String = String.Format("I{0}", i)
      wb.Worksheet(1).Cells(cellI0).Value = "$ Monto Devuelto"

      Dim cellJ0 As String = String.Format("J{0}", i)
      wb.Worksheet(1).Cells(cellJ0).Value = "% Dev. Sobre Vta."

      Dim cellK0 As String = String.Format("K{0}", i)
      wb.Worksheet(1).Cells(cellK0).Value = "Cant. Neta"

      Dim cellL0 As String = String.Format("L{0}", i)
      wb.Worksheet(1).Cells(cellL0).Value = "$ Monto Neto"

      Dim cellM0 As String = String.Format("M{0}", i)
      wb.Worksheet(1).Cells(cellM0).Value = "Agente"

     End If

     'Formato de cada una de las celdas
     Dim cellA As String = String.Format("A{0}", index)
     'wb.Worksheet(1).Cells(cellA).Value = CStr(row(0))

     Dim cellB As String = String.Format("B{0}", index)
     'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "#,##0.00"

     Dim cellC As String = String.Format("C{0}", index)
     'wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellD As String = String.Format("D{0}", index)
     wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "#,##0"

     Dim cellE As String = String.Format("E{0}", index)
     wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "#,##0"

     Dim cellF As String = String.Format("F{0}", index)
     wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellG As String = String.Format("G{0}", index)
     wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "% 0.0"

     Dim cellH As String = String.Format("H{0}", index)
     wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "#,##0"

     Dim cellI As String = String.Format("I{0}", index)
     wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellJ As String = String.Format("J{0}", index)
     wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "% 0.0"

     Dim cellK As String = String.Format("K{0}", index)
     wb.Worksheet(1).Cells(cellK).Style.NumberFormat.Format = "#,##0"

     Dim cellL As String = String.Format("L{0}", index)
     wb.Worksheet(1).Cells(cellL).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellM As String = String.Format("M{0}", index)
     'wb.Worksheet(1).Cells(cellM).Style.NumberFormat.Format = "% 0.0"


    Catch ex As Exception

    End Try

    index = index + 1
   Next

   wb.Worksheet(1).Columns("N").Hide()
   wb.Worksheet(1).Columns("O").Hide()
   wb.Worksheet(1).Columns("P").Hide()
   wb.Worksheet(1).Columns("Q").Hide()
   wb.Worksheet(1).Columns("R").Hide()
   wb.Worksheet(1).Columns().AdjustToContents()

   Try
    Dim saveFileDialog1 As New SaveFileDialog()
    saveFileDialog1.Filter = "Excel|*.xlsx"
    saveFileDialog1.Title = "Save Excel File"
    saveFileDialog1.FileName = "Ventas Agente - Linea (Articulo) de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"
    saveFileDialog1.ShowDialog()
    saveFileDialog1.InitialDirectory = "C:/"

    If saveFileDialog1.FileName <> "" Then
     Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
     fs.Close()
    End If

    Dim strFileName As String = saveFileDialog1.FileName
    wb.SaveAs(strFileName)
    Process.Start(saveFileDialog1.FileName)
   Catch ex As Exception
    MessageBox.Show("Error al guardar el archivo: " + ex.ToString)
   End Try

  End Using
 End Sub

 Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
  If MessageBox.Show("¿Desea exportar con nuevo estilo?", "Pregunta...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
   ExportNuevoReporte()
  Else
   ExportViejoReporte()
  End If
 End Sub

 Sub ExportViejoReporte()
  Dim oExcel As Object
  Dim oBook As Object
  Dim oSheet As Object

  'Abrimos un nuevo libro
  oExcel = CreateObject("Excel.Application")
  oBook = oExcel.workbooks.add
  oSheet = oBook.worksheets(1)

  'Declaramos el nombre de las columnas
  oSheet.range("A3").value = "Nombre Agente"
  oSheet.range("B3").value = "Ventas Totales Del Agente"
  oSheet.range("C3").value = "% Vtas. Totales"
  oSheet.range("D3").value = "Monto Devuelto"
  oSheet.range("E3").value = "% Dvol. Sobre Venta"
  oSheet.range("F3").value = "$ Total Ventas Netas"
  oSheet.range("G3").value = "Desc. Pronto Pago"
  oSheet.range("H3").value = " % PP Sobre Venta"
  oSheet.range("I3").value = "$ Total Notas de Credito"


  'para poner la primera fila de los titulos en negrita
  oSheet.range("A3:I3").font.bold = True
  Dim fila_dt As Integer = 0
  Dim fila_dt_excel As Integer = 0
  Dim tanto_porcentaje As String = ""
  Dim marikona As Integer = 0

  Dim total_reg As Integer = 0

  total_reg = Me.GrdConProd.RowCount

  'para leer una celda en concreto
  'el numero es la columna
  Dim cel1 As String = Me.GrdConProd.Item(1, GrdConProd.CurrentCell.RowIndex).Value
  Dim cel2 As String = Me.GrdConProd.Item(2, GrdConProd.CurrentCell.RowIndex).Value
  Dim cel3 As String = IIf(IsDBNull(Me.GrdConProd.Item(3, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(3, GrdConProd.CurrentCell.RowIndex).Value)
  Dim cel4 As String = IIf(IsDBNull(Me.GrdConProd.Item(4, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(4, GrdConProd.CurrentCell.RowIndex).Value)
  Dim cel5 As String = IIf(IsDBNull(Me.GrdConProd.Item(5, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(5, GrdConProd.CurrentCell.RowIndex).Value)
  Dim cel6 As String = IIf(IsDBNull(Me.GrdConProd.Item(6, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(6, GrdConProd.CurrentCell.RowIndex).Value)
  Dim cel7 As String = IIf(IsDBNull(Me.GrdConProd.Item(7, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(7, GrdConProd.CurrentCell.RowIndex).Value)
  Dim cel8 As String = IIf(IsDBNull(Me.GrdConProd.Item(8, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(8, GrdConProd.CurrentCell.RowIndex).Value)

  Dim cel9 As String = IIf(IsDBNull(Me.GrdConProd.Item(9, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(9, GrdConProd.CurrentCell.RowIndex).Value)
  'Dim cel10 As String = Me.GrdConProd.Item(4, GrdConProd.CurrentCell.RowIndex).Value

  fila_dt_excel = fila_dt + 4

  'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
  oSheet.range("A" & fila_dt_excel).value = cel1
  oSheet.range("B" & fila_dt_excel).value = FormatNumber(cel2, 2)
  oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel3, 2)
  oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
  oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
  oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
  oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
  oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)

  oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
  'oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel10, 2)

  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  oSheet.columns("A:N").entirecolumn.autofit()
  oSheet.range("A1").value = "Reporte de Ventas Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
  oSheet.range("C1").value = Rangos
  oSheet.range("C2").value = Rangos2

  '***************************************************Codigo de reporte de lineas
  'Declaramos el nombre de las columnas
  oSheet.range("A7").value = "Linea"
  oSheet.range("B7").value = "Cantidad Piezas"
  oSheet.range("C7").value = "$ Venta Total"
  oSheet.range("D7").value = "% Sobre Vta."
  oSheet.range("E7").value = "Piezas Devueltas"
  oSheet.range("F7").value = "$ Monto Devuelto"
  oSheet.range("G7").value = "% Dev.Sobre Vta."
  oSheet.range("H7").value = "Cant. Piezas Netas"
  oSheet.range("I7").value = "$ Total Ventas Netas"


  'para poner la primera fila de los titulos en negrita
  oSheet.range("A7:I7").font.bold = True
  fila_dt = 0
  fila_dt_excel = 7
  tanto_porcentaje = ""
  marikona = 0

  total_reg = 0

  total_reg = Me.GrdConLinea.RowCount
  For fila_dt = 0 To total_reg - 1

   'para leer una celda en concreto
   'el numero es la columna
   cel1 = Me.GrdConLinea.Item(0, fila_dt).Value
   cel2 = IIf(IsDBNull(Me.GrdConLinea.Item(1, fila_dt).Value), 0, Me.GrdConLinea.Item(1, fila_dt).Value)
   cel3 = IIf(IsDBNull(Me.GrdConLinea.Item(2, fila_dt).Value), 0, Me.GrdConLinea.Item(2, fila_dt).Value)
   cel4 = IIf(IsDBNull(Me.GrdConLinea.Item(3, fila_dt).Value), 0, Me.GrdConLinea.Item(3, fila_dt).Value)
   cel5 = IIf(IsDBNull(Me.GrdConLinea.Item(4, fila_dt).Value), 0, Me.GrdConLinea.Item(4, fila_dt).Value)
   cel6 = IIf(IsDBNull(Me.GrdConLinea.Item(5, fila_dt).Value), 0, Me.GrdConLinea.Item(5, fila_dt).Value)
   cel7 = IIf(IsDBNull(Me.GrdConLinea.Item(6, fila_dt).Value), 0, Me.GrdConLinea.Item(6, fila_dt).Value)
   cel8 = IIf(IsDBNull(Me.GrdConLinea.Item(7, fila_dt).Value), 0, Me.GrdConLinea.Item(7, fila_dt).Value)

   cel9 = IIf(IsDBNull(Me.GrdConLinea.Item(8, fila_dt).Value), 0, Me.GrdConLinea.Item(8, fila_dt).Value)


   fila_dt_excel = fila_dt + 8

   'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
   oSheet.range("A" & fila_dt_excel).value = cel1
   oSheet.range("B" & fila_dt_excel).value = FormatNumber(cel2, 2)
   oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel3, 2)
   oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
   oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
   oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
   oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
   oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)

   oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
  Next

  '***************************************************************************Detalle de artículos****************************
  Dim valreg As Integer = total_reg + 9


  'Declaramos el nombre de las columnas
  oSheet.range("A" & valreg.ToString).value = "Articulo"
  oSheet.range("B" & valreg.ToString).value = "Descripción"
  oSheet.range("C" & valreg.ToString).value = "Linea"
  oSheet.range("D" & valreg.ToString).value = "Cantidad Piezas"
  oSheet.range("E" & valreg.ToString).value = "Stock"
  oSheet.range("F" & valreg.ToString).value = "$ Venta Total"
  oSheet.range("G" & valreg.ToString).value = "% Sobre Vta."
  oSheet.range("H" & valreg.ToString).value = "Cant.Piezas Dev."
  oSheet.range("I" & valreg.ToString).value = "$ Monto Devuelto"
  oSheet.range("J" & valreg.ToString).value = "% Dev. Sobre Vta."
  oSheet.range("K" & valreg.ToString).value = "Cant. Piezas Netas"
  oSheet.range("L" & valreg.ToString).value = "$ Total Ventas Netas"

  'para poner la primera fila de los titulos en negrita
  oSheet.range("A" & valreg.ToString & ":" & "L" & valreg.ToString).font.bold = True
  fila_dt = 0
  fila_dt_excel = 0
  tanto_porcentaje = ""
  marikona = 0

  total_reg = 0

  total_reg = Me.GrdTodosArt.RowCount
  For fila_dt = 0 To total_reg - 1

   'para leer una celda en concreto
   'el numero es la columna
   cel1 = Me.GrdTodosArt.Item(0, fila_dt).Value
   cel2 = IIf(IsDBNull(Me.GrdTodosArt.Item(1, fila_dt).Value), 0, Me.GrdTodosArt.Item(1, fila_dt).Value)
   cel3 = IIf(IsDBNull(Me.GrdTodosArt.Item(2, fila_dt).Value), 0, Me.GrdTodosArt.Item(2, fila_dt).Value)
   cel4 = IIf(IsDBNull(Me.GrdTodosArt.Item(3, fila_dt).Value), 0, Me.GrdTodosArt.Item(3, fila_dt).Value)
   cel5 = IIf(IsDBNull(Me.GrdTodosArt.Item(4, fila_dt).Value), 0, Me.GrdTodosArt.Item(4, fila_dt).Value)
   cel6 = IIf(IsDBNull(Me.GrdTodosArt.Item(5, fila_dt).Value), 0, Me.GrdTodosArt.Item(5, fila_dt).Value)
   cel7 = IIf(IsDBNull(Me.GrdTodosArt.Item(6, fila_dt).Value), 0, Me.GrdTodosArt.Item(6, fila_dt).Value)
   cel8 = IIf(IsDBNull(Me.GrdTodosArt.Item(7, fila_dt).Value), 0, Me.GrdTodosArt.Item(7, fila_dt).Value)

   cel9 = IIf(IsDBNull(Me.GrdTodosArt.Item(8, fila_dt).Value), 0, Me.GrdTodosArt.Item(8, fila_dt).Value)
   Dim cel10 As String = IIf(IsDBNull(Me.GrdTodosArt.Item(9, fila_dt).Value), 0, Me.GrdTodosArt.Item(9, fila_dt).Value)
   Dim cel11 As String = IIf(IsDBNull(Me.GrdTodosArt.Item(10, fila_dt).Value), 0, Me.GrdTodosArt.Item(10, fila_dt).Value)
   Dim cel12 As String = IIf(IsDBNull(Me.GrdTodosArt.Item(11, fila_dt).Value), 0, Me.GrdTodosArt.Item(11, fila_dt).Value)

   fila_dt_excel = fila_dt + valreg + 1

   'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
   oSheet.range("A" & fila_dt_excel).value = cel1
   oSheet.range("B" & fila_dt_excel).value = cel2
   oSheet.range("C" & fila_dt_excel).value = cel3
   oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
   oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
   oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
   oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
   oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)

   oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
   oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel10, 2)
   oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel11, 2)
   oSheet.range("L" & fila_dt_excel).value = cel12
  Next

  oSheet.Columns("A:A").NumberFormat = "@"
  oSheet.Columns("I:I").ColumnWidth = 18

  oExcel.visible = True
  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  GC.Collect()
  oSheet = Nothing
  oBook = Nothing
  oExcel = Nothing
 End Sub

 Sub ExportNuevoReporte()
  Dim ds As DataSet = GrdConProd.DataSource
  Dim dt As DataTable = ds.Tables(0)

  Using wb As New XLWorkbook()
   wb.Worksheets.Add(dt, "Ventas Agente - Linea (Agentes)")
   Dim index As Integer = 2

   For i As Integer = 0 To dt.Rows.Count

    Try
     'Encabezados dependiendo sucursal
     If i = 1 Then
      Dim cellA0 As String = String.Format("A{0}", i)
      wb.Worksheet(1).Cells(cellA0).Value = "Clave"

      Dim cellB0 As String = String.Format("B{0}", i)
      wb.Worksheet(1).Cells(cellB0).Value = "Vendedor"

      Dim cellC0 As String = String.Format("C{0}", i)
      wb.Worksheet(1).Cells(cellC0).Value = "Ventas Totales"

      Dim cellD0 As String = String.Format("D{0}", i)
      wb.Worksheet(1).Cells(cellD0).Value = "% Vtas. Tot."

      Dim cellE0 As String = String.Format("E{0}", i)
      wb.Worksheet(1).Cells(cellE0).Value = "Monto Devuelto"

      Dim cellF0 As String = String.Format("F{0}", i)
      wb.Worksheet(1).Cells(cellF0).Value = "% Dvol. Sobre Venta"

      Dim cellG0 As String = String.Format("G{0}", i)
      wb.Worksheet(1).Cells(cellG0).Value = "Ventas Netas"

      Dim cellH0 As String = String.Format("H{0}", i)
      wb.Worksheet(1).Cells(cellH0).Value = "Descuentos Pronto Pago"

      Dim cellI0 As String = String.Format("I{0}", i)
      wb.Worksheet(1).Cells(cellI0).Value = "% PP Sobre Venta"

      Dim cellJ0 As String = String.Format("J{0}", i)
      wb.Worksheet(1).Cells(cellJ0).Value = "Total Notas de Credito"

     End If

     'Formato de cada una de las celdas
     Dim cellA As String = String.Format("A{0}", index)
     'wb.Worksheet(1).Cells(cellA).Value = CStr(row(0))

     Dim cellB As String = String.Format("B{0}", index)
     wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellC As String = String.Format("C{0}", index)
     wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellD As String = String.Format("D{0}", index)
     wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "% 0.0"

     Dim cellE As String = String.Format("E{0}", index)
     wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellF As String = String.Format("F{0}", index)
     wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "% 0.0"

     Dim cellG As String = String.Format("G{0}", index)
     wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellH As String = String.Format("H{0}", index)
     wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellI As String = String.Format("I{0}", index)
     wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "% 0.0"

     Dim cellJ As String = String.Format("J{0}", index)
     wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "$ #,##0.00"

    Catch ex As Exception

    End Try

    index = index + 1
   Next

   wb.Worksheet(1).Columns("K").Hide()
   wb.Worksheet(1).Columns("L").Hide()
   wb.Worksheet(1).Columns("M").Hide()
   wb.Worksheet(1).Columns("N").Hide()
   wb.Worksheet(1).Columns().AdjustToContents()
   '-------------------------------------------------------------TERMINA PRIMERA HOJA DE EXCEL AGENTES  


   Dim dv As DataView = DirectCast(GrdConLinea.DataSource, DataView)
   Dim dt2 As DataTable = dv.Table

   wb.Worksheets.Add(dt2, "Ventas Agente - Linea (Linea)")
   index = 2

   For i As Integer = 0 To dt2.Rows.Count

    Try
     'Encabezados dependiendo sucursal
     If i = 1 Then
      Dim cellA0 As String = String.Format("A{0}", i)
      wb.Worksheet(2).Cells(cellA0).Value = "Linea"

      Dim cellB0 As String = String.Format("B{0}", i)
      wb.Worksheet(2).Cells(cellB0).Value = "Cantidad Piezas"

      Dim cellC0 As String = String.Format("C{0}", i)
      wb.Worksheet(2).Cells(cellC0).Value = "$ Venta Total"

      Dim cellD0 As String = String.Format("D{0}", i)
      wb.Worksheet(2).Cells(cellD0).Value = "% Sobre Vta."

      Dim cellE0 As String = String.Format("E{0}", i)
      wb.Worksheet(2).Cells(cellE0).Value = "Piezas Devueltas"

      Dim cellF0 As String = String.Format("F{0}", i)
      wb.Worksheet(2).Cells(cellF0).Value = "$ Monto Devuelto"

      Dim cellG0 As String = String.Format("G{0}", i)
      wb.Worksheet(2).Cells(cellG0).Value = "% Dev.Sobre Vta."

      Dim cellH0 As String = String.Format("H{0}", i)
      wb.Worksheet(2).Cells(cellH0).Value = "Cant. Neta"

      Dim cellI0 As String = String.Format("I{0}", i)
      wb.Worksheet(2).Cells(cellI0).Value = "$ Monto Neto"

      Dim cellJ0 As String = String.Format("J{0}", i)
      wb.Worksheet(2).Cells(cellJ0).Value = "Agente"

     End If

     'Formato de cada una de las celdas
     Dim cellA As String = String.Format("A{0}", index)
     'wb.Worksheet(1).Cells(cellA).Value = CStr(row(0))

     Dim cellB As String = String.Format("B{0}", index)
     wb.Worksheet(2).Cells(cellB).Style.NumberFormat.Format = "#,##0.00"

     Dim cellC As String = String.Format("C{0}", index)
     wb.Worksheet(2).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellD As String = String.Format("D{0}", index)
     wb.Worksheet(2).Cells(cellD).Style.NumberFormat.Format = "% 0.0"

     Dim cellE As String = String.Format("E{0}", index)
     wb.Worksheet(2).Cells(cellE).Style.NumberFormat.Format = "#,##0"

     Dim cellF As String = String.Format("F{0}", index)
     wb.Worksheet(2).Cells(cellF).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellG As String = String.Format("G{0}", index)
     wb.Worksheet(2).Cells(cellG).Style.NumberFormat.Format = "% 0.0"

     Dim cellH As String = String.Format("H{0}", index)
     wb.Worksheet(2).Cells(cellH).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellI As String = String.Format("I{0}", index)
     wb.Worksheet(2).Cells(cellI).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellJ As String = String.Format("J{0}", index)
     'wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "$ #,##0.00"


    Catch ex As Exception

    End Try

    index = index + 1
   Next

   wb.Worksheet(2).Columns(11).Hide()
   wb.Worksheet(2).Columns(12).Hide()
   wb.Worksheet(2).Columns(13).Hide()
   wb.Worksheet(2).Columns(14).Hide()
   wb.Worksheet(2).Columns(15).Hide()
   wb.Worksheet(2).Columns().AdjustToContents()
   '--------------------------------------------------------FIN DE LA HOJA DOS LINEA

   Dim dv3 As DataView = DirectCast(GrdDetArt.DataSource, DataView)
   Dim dt3 As DataTable = dv3.Table

   wb.Worksheets.Add(dt3, "Ventas Agente-Linea (Articulo)")
   index = 2

   For i As Integer = 0 To dt3.Rows.Count

    Try
     'Encabezados dependiendo sucursal
     If i = 1 Then
      Dim cellA0 As String = String.Format("A{0}", i)
      wb.Worksheet(3).Cells(cellA0).Value = "Articulo"

      Dim cellB0 As String = String.Format("B{0}", i)
      wb.Worksheet(3).Cells(cellB0).Value = "Descripción"

      Dim cellC0 As String = String.Format("C{0}", i)
      wb.Worksheet(3).Cells(cellC0).Value = "Linea"

      Dim cellD0 As String = String.Format("D{0}", i)
      wb.Worksheet(3).Cells(cellD0).Value = "Cantidad Piezas"

      Dim cellE0 As String = String.Format("E{0}", i)
      wb.Worksheet(3).Cells(cellE0).Value = "Stock"

      Dim cellF0 As String = String.Format("F{0}", i)
      wb.Worksheet(3).Cells(cellF0).Value = "$ Venta Total"

      Dim cellG0 As String = String.Format("G{0}", i)
      wb.Worksheet(3).Cells(cellG0).Value = "% Sobre Vta."

      Dim cellH0 As String = String.Format("H{0}", i)
      wb.Worksheet(3).Cells(cellH0).Value = "Cant.Piezas Dev."

      Dim cellI0 As String = String.Format("I{0}", i)
      wb.Worksheet(3).Cells(cellI0).Value = "$ Monto Devuelto"

      Dim cellJ0 As String = String.Format("J{0}", i)
      wb.Worksheet(3).Cells(cellJ0).Value = "% Dev. Sobre Vta."

      Dim cellK0 As String = String.Format("K{0}", i)
      wb.Worksheet(3).Cells(cellK0).Value = "Cant. Neta"

      Dim cellL0 As String = String.Format("L{0}", i)
      wb.Worksheet(3).Cells(cellL0).Value = "$ Monto Neto"

      Dim cellM0 As String = String.Format("M{0}", i)
      wb.Worksheet(3).Cells(cellM0).Value = "Agente"

     End If

     'Formato de cada una de las celdas
     Dim cellA As String = String.Format("A{0}", index)
     'wb.Worksheet(1).Cells(cellA).Value = CStr(row(0))

     Dim cellB As String = String.Format("B{0}", index)
     'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "#,##0.00"

     Dim cellC As String = String.Format("C{0}", index)
     'wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellD As String = String.Format("D{0}", index)
     wb.Worksheet(3).Cells(cellD).Style.NumberFormat.Format = "#,##0"

     Dim cellE As String = String.Format("E{0}", index)
     wb.Worksheet(3).Cells(cellE).Style.NumberFormat.Format = "#,##0"

     Dim cellF As String = String.Format("F{0}", index)
     wb.Worksheet(3).Cells(cellF).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellG As String = String.Format("G{0}", index)
     wb.Worksheet(3).Cells(cellG).Style.NumberFormat.Format = "% 0.0"

     Dim cellH As String = String.Format("H{0}", index)
     wb.Worksheet(3).Cells(cellH).Style.NumberFormat.Format = "#,##0"

     Dim cellI As String = String.Format("I{0}", index)
     wb.Worksheet(3).Cells(cellI).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellJ As String = String.Format("J{0}", index)
     wb.Worksheet(3).Cells(cellJ).Style.NumberFormat.Format = "% 0.0"

     Dim cellK As String = String.Format("K{0}", index)
     wb.Worksheet(3).Cells(cellK).Style.NumberFormat.Format = "#,##0"

     Dim cellL As String = String.Format("L{0}", index)
     wb.Worksheet(3).Cells(cellL).Style.NumberFormat.Format = "$ #,##0.00"

     Dim cellM As String = String.Format("M{0}", index)
     'wb.Worksheet(1).Cells(cellM).Style.NumberFormat.Format = "% 0.0"


    Catch ex As Exception

    End Try

    index = index + 1
   Next

   wb.Worksheet(3).Columns("N").Hide()
   wb.Worksheet(3).Columns("O").Hide()
   wb.Worksheet(3).Columns("P").Hide()
   wb.Worksheet(3).Columns("Q").Hide()
   wb.Worksheet(3).Columns("R").Hide()
   wb.Worksheet(3).Columns().AdjustToContents()

   Try
    Dim saveFileDialog1 As New SaveFileDialog()
    saveFileDialog1.Filter = "Excel|*.xlsx"
    saveFileDialog1.Title = "Save Excel File"
    saveFileDialog1.FileName = "Ventas Agente - Linea (Reporte) de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"
    saveFileDialog1.ShowDialog()
    saveFileDialog1.InitialDirectory = "C:/"

    If saveFileDialog1.FileName <> "" Then
     Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
     fs.Close()
    End If

    Dim strFileName As String = saveFileDialog1.FileName
    wb.SaveAs(strFileName)
    Process.Start(saveFileDialog1.FileName)
   Catch ex As Exception
    MessageBox.Show("Error al guardar el archivo: " + ex.ToString)
   End Try
  End Using
 End Sub


 Private Sub GrdConProd_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdConProd.SelectionChanged

  Try
   DvDlinea.RowFilter = "IdAgente =" & GrdConProd.Item(0, GrdConProd.CurrentRow.Index).Value.ToString
  Catch ex As Exception
  End Try

  Try
   dvTodosArt.RowFilter = "IdAgente =" & GrdConProd.Item(0, GrdConProd.CurrentRow.Index).Value.ToString
  Catch ex As Exception
  End Try
 End Sub

 Sub FiltraDetArticulos()
  Try

   If GrdConLinea.Item(14, GrdConLinea.CurrentRow.Index).Value = 0 Then


    If GrdConProd.Item(0, GrdConProd.CurrentRow.Index).Value = 0 Then

     If RBTot.Checked = True Then
      dv.RowFilter = "IdAgente = 0 AND ItmsGrpCod = '" + GrdConLinea.Item(10, GrdConLinea.CurrentRow.Index).Value.ToString + "'"
     Else
      dv.RowFilter = "IdAgente <> 0 AND ItmsGrpCod = '" + GrdConLinea.Item(10, GrdConLinea.CurrentRow.Index).Value.ToString + "'"
     End If

    Else
     dv.RowFilter = "LineaAgte ='" + GrdConLinea.Item(12, GrdConLinea.CurrentRow.Index).Value.ToString + "'"
    End If



   Else
    If GrdConLinea.Item(14, GrdConLinea.CurrentRow.Index).Value = 1 And
                    GrdConProd.Item(0, GrdConProd.CurrentRow.Index).Value <> 0 Then

     dv.RowFilter = "IdAgente =" & GrdConProd.Item(0, GrdConProd.CurrentRow.Index).Value.ToString
    Else
     If RBTot.Checked = True Then
      dv.RowFilter = "IdAgente = 0"
     Else
      dv.RowFilter = "IdAgente <> 0"
     End If


    End If
   End If

  Catch ex As Exception

  End Try
 End Sub

 Private Sub RBTot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTot.CheckedChanged
  FiltraDetArticulos()
 End Sub

 Private Sub RBDet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBDet.CheckedChanged
  FiltraDetArticulos()
 End Sub
End Class