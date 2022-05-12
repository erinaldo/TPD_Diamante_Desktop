Imports System.Data.SqlClient
Imports System.Data
Imports System.ComponentModel
Imports ClosedXML.Excel
Imports System.IO

Public Class Reporte_Ventas_Lineas


 Private dvLineas As New DataView
 Private dvAgentes As New DataView
 Private dvArticulos As New DataView
 Private dvTodosArt As New DataView

 Dim Rangos As String = ""
 Dim Rangos2 As String = ""

 Private Sub ConsultaProd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
  If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Then
   GrdConProd.Width = 685
   GrdConProd.Height = 590

   'Ventas Totales Por Agente de Ventas
   Label1.Location = New Point(693, 47)
   GrdConLinea.Location = New Point(693, 65)
   GrdConLinea.Width = 700
   GrdConLinea.Height = 330

   'Ventas Totales Por Artículo
   Label2.Location = New Point(693, 398)
   GrdDetArt.Location = New Point(693, 415)
   GrdDetArt.Width = 700
   GrdDetArt.Height = 240

   'Button5.Location = New Point(700, 55)

   Me.WindowState = FormWindowState.Maximized
  End If

  Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
  Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")
 End Sub

 Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
  cargar_registros()

  'GrdConProd.CurrentCell = GrdConProd.Rows(0).Cells(1)

  Try
   'dvArticulos.RowFilter = "DesLinea = '" & GrdConLinea.Item(1, GrdConLinea.CurrentCell.RowIndex).Value.ToString & "'"
   dvArticulos.RowFilter = "CodLinea = 999"
  Catch ex As Exception

  End Try

 End Sub

 Sub cargar_registros()


  Dim DTRefacciones As New DataTable


  '****************************************************************************************************************************

  ' crear nueva conexión    
  Dim conexion2 As New SqlConnection(StrCon)

  ' abrir la conexión con la base de datos   
  conexion2.Open()

  Dim Adaptador As New SqlDataAdapter()
  Dim comando As New SqlCommand

  Dim SQLTPD As String


  '--CANCELACIONES
  '------------------------------ CONSULTA ANTERIOR DE CANCELACION POR ACTUALIZACION DEL SISTEMA SAP-----------------------------------------

  'SQLTPD = "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,"
  'SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,"
  'SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE AS VARCHAR(22)) AS ArtAgte,"
  'SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA INTO #Tem_Canc FROM ORIN T0 INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry "
  'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "
  'SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F')" '' AND T0.DocType  = 'I' AND Series <> 49 "
  'SQLTPD &= "and t1.ItemCode <> 'DESCUENTO P.P' "
  ''SQLTPD &= "AND DocType  = 'I' AND  EDocNum IS NULL  "

  '------------------------------NUEVA ACTUALZIACION DE CONSULTA DE CANCELACION POR ACTUALIZACION DEL SISTEMA SAP-----------------------------------------
  '------YA NO SE USA
  'SQLTPD = "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.DiscPrcnt, t2.ReportID "
  'SQLTPD &= "into #tmp_ORIN "
  'SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
  'SQLTPD &= "left join ECM2 t2 on t0.DocEntry = t2.SrcObjAbs and t2.SrcObjType = 14  "
  'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer  "
  'SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F')  "
  'SQLTPD &= "AND CASE WHEN t0.DocDate <= '2017-12-31' THEN "
  'SQLTPD &= "CASE WHEN t0.DocType = 'I' THEN  "
  'SQLTPD &= "1 ELSE 0 END "
  'SQLTPD &= "ELSE "
  'SQLTPD &= "CASE WHEN t1.ItemCode <> 'DESCUENTO P.P' THEN "
  'SQLTPD &= "1 ELSE 0 END "
  'SQLTPD &= "END = 1 "
  'SQLTPD &= "and t2.ReportID is null "

  'SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente, "
  'SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
  'SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE AS VARCHAR(22)) AS ArtAgte, "
  'SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA  "
  'SQLTPD &= "INTO #Tem_Canc  "
  'SQLTPD &= "FROM #tmp_ORIN T0 INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
  'If (ckCteProp.Checked = False) Then
  '  SQLTPD &= " and T0.slpCode <> 1"
  'End If
  'SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode  "

  '--FIN CANCELACIONES----------------------



  '--DEVOLUCIONES

  '------------------------------ CONSULTA ANTERIOR DE DEVOLUCIONES POR ACTUALIZACION DEL SISTEMA SAP-----------------------------------------

  'SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,"
  'SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,"
  'SQLTPD &= "(CAST('DEVOLUCION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE AS VARCHAR(22)) AS ArtAgte,"
  'SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA INTO #Tem_Dev FROM ORIN T0 INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry "
  'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer " 'AND T0.Series = 49 "
  ''SQLTPD &= "and t1.ItemCode <> 'DESCUENTO P.P'"
  'SQLTPD &= " AND DocType  = 'I' AND  EDocNum IS NOT NULL "
  'If (ckCteProp.Checked = False) Then
  '    SQLTPD &= " and T0.slpCode <> 1"
  'End If
  'SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode  "

  SQLTPD &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
  SQLTPD &= "into #NC_Art_Timb  "
  SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry  "
  SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode  "
  SQLTPD &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14  "
  SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer  "
  SQLTPD &= "and t0.DocType <> 'S'  "
  SQLTPD &= "and t2.ItmsGrpCod <> 200  "
  SQLTPD &= "AND (T3.ReportID IS NOT NULL OR T0.U_BXP_UUID IS NOT NULL) "
  SQLTPD &= "AND ((T0.U_BXP_TIMBRADO = 'T' OR T0.U_BXP_TIMBRADO = 'P') OR T0.EDocGenTyp = 'G') "
  SQLTPD &= "AND t0.DocNum NOT IN (SELECT DocNum FROM TPM.dbo.[00ExcepDevolucion]) "
  If (ckCteProp.Checked = False) Then
   SQLTPD &= "   and T0.slpCode <> 1"
  End If


  SQLTPD &= "SELECT t0.DocNum, t1.ItemCode, t1.Quantity, t0.SlpCode,  "
  SQLTPD &= "CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END AS TotalSinIva "
  SQLTPD &= "into #tmp1 "
  SQLTPD &= "FROM #NC_Art_Timb t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "

  SQLTPD &= "select ItemCode, SUM(Quantity) AS CANTIDAD, SlpCode AS IdAgente, SUM(TotalSinIva) AS TotalSinIva, "
  SQLTPD &= "(CAST('DEVOLUCION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(SlpCode AS VARCHAR(2)))) + ITEMCODE AS VARCHAR(22)) AS ArtAgte,  "
  SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA  "
  SQLTPD &= "INTO #Tem_Dev  "
  SQLTPD &= "from #tmp1 "
  SQLTPD &= "group by ItemCode, SlpCode "

  SQLTPD &= "drop table #NC_Art_Timb  "
  SQLTPD &= "drop table #tmp1 "

  '-- FIN DEVOLUCIONES

  '--FACTURADO
  'SQLTPD &= "SELECT CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS ItemCode,"
  'SQLTPD &= "SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,"
  'SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null  THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,"
  'SQLTPD &= "CAST(CASE WHEN T1.ItemCode IS NULL THEN 'NCARGO' ELSE 'FACTURACION' END AS CHAR(20))AS TipoMov,"
  'SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS VARCHAR(22)) AS ArtAgte,"
  'SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
  'SQLTPD &= "INTO #Tem_Fact FROM OINV T0 INNER JOIN INV1 T1 ON T1.DocEntry = T0.DocEntry "
  ''SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND T0.DOCNUM <> 29870 AND T0.DOCNUM <> 29821 AND T0.DOCNUM <>'32759' AND T0.DOCNUM <>'32764'" ' AND T0.SERIES <> 59 "
  'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND T0.DOCNUM <> 29870 AND T0.DOCNUM <> 29821 AND T0.DOCNUM <>'32759' AND T0.DOCNUM <>'32764' AND T0.DocType <> 'S' "
  'If (ckCteProp.Checked = False) Then
  '    SQLTPD &= " and T0.slpCode <> 1"
  'End If
  'SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode  "

  SQLTPD &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
  SQLTPD &= "into #TodasFacturasArticulos  "
  SQLTPD &= "from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry   "
  SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode   "
  SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer  "
  SQLTPD &= "and t0.DocType <> 'S'  "
  SQLTPD &= "and t2.ItmsGrpCod <> 200  "
  If (ckCteProp.Checked = False) Then
   SQLTPD &= " and T0.slpCode <> 1 "
  End If

  SQLTPD &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.CardCode, t0.DiscPrcnt  "
  SQLTPD &= "into #NC_Art_NoTimb  "
  SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry  "
  SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode  "
  SQLTPD &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14  "
  SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer  "
  SQLTPD &= "and t0.DocType <> 'S'  "
  SQLTPD &= "and t2.ItmsGrpCod <> 200  "
  SQLTPD &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate > '2020-08-23' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
  SQLTPD &= "AND t3.ReportID Is NULL "
  If (ckCteProp.Checked = False) Then
   SQLTPD &= " and T0.slpCode <> 1 "
  End If

  SQLTPD &= "select t0.DocNum, CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS ItemCode, t1.Quantity, t0.SlpCode,  "
  SQLTPD &= "CASE WHEN t0.DiscPrcnt is null  THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END AS TotalSinIva "
  SQLTPD &= "into #Tmp_Fact "
  SQLTPD &= "from #TodasFacturasArticulos t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry "

  SQLTPD &= "select t0.DocNum, CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS ItemCode, t1.Quantity, t0.SlpCode,  "
  SQLTPD &= "CASE WHEN t0.DiscPrcnt is null  THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END AS TotalSinIva "
  SQLTPD &= "into #Tmp_NC "
  SQLTPD &= "from #NC_Art_NoTimb t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "

  SQLTPD &= "select ItemCode, SUM(Quantity) as 'CANTIDAD', SlpCode, SUM(TotalSinIva) as 'TotalSinIva' "
  SQLTPD &= "into #F_F "
  SQLTPD &= "from #Tmp_Fact "
  SQLTPD &= "group by ItemCode, SlpCode "

  SQLTPD &= "select ItemCode, SUM(Quantity) as 'CANTIDAD', SlpCode, SUM(TotalSinIva) as 'TotalSinIva' "
  SQLTPD &= "into #F_NC "
  SQLTPD &= "from #Tmp_NC "
  SQLTPD &= "group by ItemCode, SlpCode "

  SQLTPD &= "select t0.ItemCode, t0.CANTIDAD, t0.SlpCode as 'IdAgente', t0.TotalSinIva - isnull(t1.TotalSinIva, 0) as 'TotalSinIva', "
  SQLTPD &= "CAST(CASE WHEN t0.ItemCode IS NULL THEN 'NCARGO' ELSE 'FACTURACION' END AS CHAR(20))AS TipoMov, "
  SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + CASE WHEN t0.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE t0.ItemCode END AS VARCHAR(22)) AS ArtAgte, "
  SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA  "
  SQLTPD &= "INTO #Tem_Fact  "
  SQLTPD &= "from #F_F t0 left join #F_NC t1 on t0.ItemCode = t1.ItemCode and t0.SlpCode = t1.SlpCode "
  SQLTPD &= "union all "
  SQLTPD &= "select t1.ItemCode, t1.CANTIDAD, t1.SlpCode as 'IdAgente', t1.TotalSinIva * -1 as 'TotalSinIva', "
  SQLTPD &= "CAST(CASE WHEN t1.ItemCode IS NULL THEN 'NCARGO' ELSE 'FACTURACION' END AS CHAR(20))AS TipoMov, "
  SQLTPD &= "CAST(RTRIM(LTRIM(CAST(t1.SlpCode AS VARCHAR(2)))) + CASE WHEN t1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE t1.ItemCode END AS VARCHAR(22)) AS ArtAgte, "
  SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA  "
  SQLTPD &= "from #F_F t0 right join #F_NC t1 on t0.ItemCode = t1.ItemCode and t0.SlpCode = t1.SlpCode where t0.ItemCode is null "
  SQLTPD &= "order by ItemCode "

  SQLTPD &= "drop table #F_F "
  SQLTPD &= "drop table #F_NC "
  SQLTPD &= "drop table #NC_Art_NoTimb "
  SQLTPD &= "drop table #Tmp_Fact "
  SQLTPD &= "drop table #Tmp_NC "
  SQLTPD &= "drop table #TodasFacturasArticulos "


  '--CONSULTA CON INSERT SE BUSCAN LOS ARTICULOS CANCELADOS QUE NO HAN SIDO FACTURADOS EN ESE DIA POR ESE AGENTE
  'SQLTPD &= "INSERT #Tem_Fact (ItemCode, CANTIDAD, IdAgente,TotalSinIva,TipoMov,ArtAgte,CANTNETA) "
  'SQLTPD &= "SELECT ItemCode, 0, IdAgente,0,TipoMov,ArtAgte,CANTNETA "
  'SQLTPD &= "FROM #Tem_Canc AS T0 WHERE T0.ArtAgte NOT IN (SELECT T1.ArtAgte FROM #Tem_Fact T1 WHERE T1.ArtAgte IS NOT NULL)  "

  '--CONSULTA CON INSERT SE BUSCAN LOS ARTICULOS CANCELADOS QUE NO HAN SIDO FACTURADOS EN ESE DIA POR ESE AGENTE
  SQLTPD &= "INSERT #Tem_Fact (ItemCode, CANTIDAD, IdAgente,TotalSinIva,TipoMov,ArtAgte,CANTNETA) "
  SQLTPD &= "SELECT ItemCode, 0, IdAgente,0,TipoMov,ArtAgte,CANTNETA "
  SQLTPD &= "FROM #Tem_Dev AS T0 WHERE T0.ArtAgte NOT IN (SELECT T1.ArtAgte FROM #Tem_Fact T1 WHERE T1.ArtAgte IS NOT NULL)  "

  '*****************************************************************************************************************************************

  SQLTPD &= "SELECT T0.ItemCode AS Articulo,"
  SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 'NOTAS DE CARGO' ELSE T3.ItemName END AS ItemName,"
  SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 998 ELSE T3.ItmsGrpCod END AS ItmsGrpCod,"
  SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T4.ItmsGrpNam END AS ItmsGrpNam,"
  SQLTPD &= "T0.TipoMov,T0.IdAgente,T0.ArtAgte,"
  'SQLTPD &= "CASE WHEN T1.CANTIDAD IS NULL THEN T0.CANTIDAD ELSE T0.CANTIDAD - T1.CANTIDAD END CANT_TOT,"
  SQLTPD &= "T0.CANTIDAD as CANT_TOT,"
  'SQLTPD &= "CASE WHEN T1.TotalSinIva IS NULL THEN T0.TotalSinIva ELSE T0.TotalSinIva - T1.TotalSinIva END MONT_TOT,"
  SQLTPD &= "T0.TotalSinIva as MONT_TOT,"
  SQLTPD &= "CASE WHEN T2.CANTIDAD IS NULL THEN 0 ELSE T2.CANTIDAD END AS CantDev,"
  'SQLTPD &= "CASE WHEN T1.CANTIDAD IS NULL THEN T0.CANTIDAD ELSE T0.CANTIDAD - T1.CANTIDAD END  - "
  SQLTPD &= "T0.CANTIDAD - "
  SQLTPD &= "CASE WHEN T2.CANTIDAD IS NULL THEN 0 ELSE T2.CANTIDAD END AS CANT_NETA,"
  SQLTPD &= "CASE WHEN T2.TotalSinIva IS NULL THEN 0 ELSE T2.TotalSinIva END AS MontoDev,"
  SQLTPD &= "T0.TotalSinIva - "
  'SQLTPD &= "(CASE WHEN T1.TotalSinIva IS NULL THEN 0 ELSE T1.TotalSinIva END + "
  SQLTPD &= "(0 + "
  SQLTPD &= "CASE WHEN T2.TotalSinIva IS NULL THEN 0 ELSE T2.TotalSinIva END) "
  SQLTPD &= "AS  MONTO_NETA "
  'SQLTPD &= "INTO #T_VTA_ART FROM #Tem_Fact T0 LEFT JOIN #Tem_Canc T1 ON T0.ArtAgte = T1.ArtAgte "
  SQLTPD &= "INTO #T_VTA_ART FROM #Tem_Fact T0  "
  SQLTPD &= "LEFT JOIN #Tem_Dev T2 ON T0.ArtAgte = T2.ArtAgte "
  SQLTPD &= "LEFT JOIN OITM T3 ON T0.ItemCode = T3.ItemCode "
  SQLTPD &= "LEFT JOIN OITB T4 ON T3.ItmsGrpCod = T4.ItmsGrpCod  "



  '--SE OBTIENE MONTOS POR LINEA
  SQLTPD &= "SELECT ItmsGrpCod AS CodLinea,"
  SQLTPD &= "SUM(CANT_TOT) AS PzsVta,SUM(MONT_TOT) AS MontoVta,"
  SQLTPD &= "Sum(CantDev) AS CantDev,SUM(MontoDev) AS MontoDev,"
  SQLTPD &= "SUM(CANT_NETA) AS PzsNeta,SUM(MONTO_NETA) AS MontoNeto,"
  SQLTPD &= "1 AS Llave "
  SQLTPD &= "INTO #T_DET_LINEA FROM #T_VTA_ART GROUP BY ItmsGrpCod  "


  '--MONTOS TOTALES
  SQLTPD &= "SELECT "
  SQLTPD &= "SUM(CANT_TOT) AS PzsVta,SUM(MONT_TOT) AS MontoVta,"
  SQLTPD &= "Sum(CantDev) AS CantDev,SUM(MontoDev) AS MontoDev,"
  SQLTPD &= "SUM(CANT_NETA) AS PzsNeta,SUM(MONTO_NETA) AS MontoNeto,"
  SQLTPD &= "1 AS Llave "
  SQLTPD &= "INTO #T_TOT_LINEA FROM #T_VTA_ART "


  '--********SE OBTIENEN EL MONTO TOTAL DE LAS LINEAS CON PORCENTAJES
  SQLTPD &= "SELECT T0.CodLinea,"
  SQLTPD &= "CASE WHEN T2.ItmsGrpCod IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItmsGrpNam END AS DesLinea,"
  SQLTPD &= "T0.PzsVta,T0.MontoVta,"
  SQLTPD &= "CASE WHEN T0.MontoVta <= 0 OR  T1.MontoVta <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoVta * 100 / T1.MontoVta END AS PorVta,"
  SQLTPD &= "T0.CantDev,T0.MontoDev,"
  SQLTPD &= "CASE WHEN T0.MontoDev <= 0 OR  T1.MontoVta <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoDev * 100 / T1.MontoVta END AS PorDEV,"
  SQLTPD &= "T0.PzsNeta,T0.MontoNeto,0 AS Orden "
  SQLTPD &= "INTO #T_DET_LIN_P "
  SQLTPD &= "FROM #T_DET_LINEA T0 INNER JOIN #T_TOT_LINEA T1 ON T0.Llave = T1.Llave "
  SQLTPD &= "LEFT JOIN OITB T2 ON T2.ItmsGrpCod = T0.CodLinea "
  SQLTPD &= "UNION ALL "
  SQLTPD &= "SELECT ItmsGrpCod AS CodLinea,ItmsGrpNam AS DesLinea,"
  SQLTPD &= "CAST(0 AS INTEGER) AS PzsVta,CAST(0 AS INTEGER) AS MontoVta,CAST(0 AS INTEGER) AS PorVta,"
  SQLTPD &= "CAST(0 AS INTEGER) AS CantDev,CAST(0 AS INTEGER) AS MontoDev,CAST(0 AS INTEGER) AS PorDEV,"
  SQLTPD &= "CAST(0 AS INTEGER) AS PzsNeta,CAST(0 AS INTEGER) AS MontoNeto,0 AS Orden "
  SQLTPD &= "FROM OITB T4 WHERE T4.ItmsGrpCod NOT IN (SELECT CodLinea FROM #T_DET_LINEA) "
  SQLTPD &= "UNION ALL "
  SQLTPD &= "SELECT "
  SQLTPD &= "'999' AS CodLinea,'MONTOS TOTALES' AS DesLinea,"
  SQLTPD &= "PzsVta,MontoVta,CAST(100 AS INTEGER) AS PorVta,CantDev,MontoDev, CAST(0 AS INTEGER) AS PorDEV,PzsNeta,MontoNeto,1 AS Orden FROM #T_TOT_LINEA T3  "


  '--SE OBTIENE EL MONTO DE VENTAS POR AGENTE-LINEA
  SQLTPD &= "SELECT T0.IdAgente, T0.ItmsGrpCod AS CodLinea,"
  SQLTPD &= "SUM(T0.CANT_TOT) AS PzsVta,SUM(T0.MONT_TOT) AS MontoVta,"
  SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev,"
  SQLTPD &= "SUM(T0.CANT_NETA) AS PzsNeta,SUM(T0.MONTO_NETA) AS MontoNeto "
  SQLTPD &= "INTO #T_AGTE_LIN FROM #T_VTA_ART T0 GROUP BY IdAgente,ItmsGrpCod  "


  '----*************************
  'SELECT 'The order is due on ' + CONVERT(varchar(12), DueDate, 101)
  '--TOTALES POR AGENTE
  'Select (Cast(id_ciudad as nvarchar(3)) + ' - ' + nombre_ciudad) as ciudad from ciudad 

  SQLTPD &= "SELECT T0.IdAgente, '999' AS CodLinea,'MONTOS TOTALES' AS DesLinea,"
  SQLTPD &= "SUM(T0.PzsVta) AS PzsVta,SUM(T0.MontoVta) AS MontoVta,"
  SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev,"
  SQLTPD &= "SUM(T0.PzsNeta) AS PzsNeta,SUM(T0.MontoNeto) AS MontoNeto "
  SQLTPD &= "INTO #T_TOT_AGTE "
  SQLTPD &= "FROM #T_AGTE_LIN T0 GROUP BY T0.IdAgente  "


  '----*************************


  '----*********DETALLE POR LINEA
  SQLTPD &= "SELECT DesLinea, PzsVta, MontoVta, PorVta, CantDev, MontoDev, PorDEV, PzsNeta, MontoNeto,CodLinea "
  SQLTPD &= "FROM #T_DET_LIN_P ORDER BY Orden ASC,MontoNeto DESC; "


  '--********SE OBTIENE LOS PORCENTAJES Y DESCRIPCIONES DE LA CONSULTA DE AGENTE-LINEA

  SQLTPD &= "SELECT T2.SlpName AS Agente,"
  SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS DesLinea,"
  SQLTPD &= "T0.PzsVta,T0.MontoVta,"
  SQLTPD &= "CASE WHEN T0.MontoVta <= 0 OR T1.MontoVta <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoVta * 100 / T1.MontoVta END AS PorVta,"
  SQLTPD &= "T0.CantDev, T0.MontoDev,"
  SQLTPD &= "CASE WHEN T0.MontoDev <= 0 OR T1.MontoVta <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoDev * 100 / T1.MontoVta END AS PorDEV,"
  SQLTPD &= "T0.PzsNeta, T0.MontoNeto,"
  SQLTPD &= "T0.IdAgente,T0.CodLinea "

  SQLTPD &= "FROM #T_AGTE_LIN T0 INNER JOIN #T_DET_LINEA T1 ON T0.CodLinea = T1.CodLinea "
  SQLTPD &= "INNER JOIN OSLP T2 ON T0.IdAgente = T2.SlpCode "
  SQLTPD &= "LEFT JOIN OITB T3 ON T0.CodLinea = T3.ItmsGrpCod "

  SQLTPD &= "UNION ALL "
  SQLTPD &= "SELECT T2.SlpName AS Agente, T0.DesLinea,"
  SQLTPD &= "T0.PzsVta, T0.MontoVta, "
  SQLTPD &= "CASE WHEN T0.MontoVta <= 0 OR T1.MontoVta <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoVta * 100 / T1.MontoVta END AS PorVta,"
  SQLTPD &= "T0.CantDev, T0.MontoDev,"
  SQLTPD &= "CASE WHEN T0.MontoDev <= 0 OR T1.MontoVta <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoDev * 100 / T1.MontoVta END AS PorDEV,"
  SQLTPD &= "T0.PzsNeta, T0.MontoNeto,T0.IdAgente,T0.CodLinea FROM #T_TOT_AGTE T0 "
  SQLTPD &= "LEFT JOIN #T_DET_LIN_P T1 ON T0.CodLinea = T1.CodLinea "
  SQLTPD &= "INNER JOIN OSLP T2 ON T0.IdAgente = T2.SlpCode   ORDER BY SLPNAME, MontoNeto DESC; "


  '--***SE CONSULTA EL DETALLE DE AGENTE-LINEA-ARTICULOS CON PORCENTAJES DE VENTAS
  SQLTPD &= "SELECT T0.Articulo, T0.ItemName, "
  SQLTPD &= "T0.CANT_TOT AS PzsVta, T0.MONT_TOT AS MontoVta,"
  SQLTPD &= "CASE WHEN T0.MONT_TOT <= 0 OR T1.MontoVta <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MONT_TOT * 100 / T1.MontoVta END AS PorVta,"
  SQLTPD &= "T0.CantDev,T0.MontoDev,  "
  SQLTPD &= "CASE WHEN T0.MontoDev <= 0 OR  T1.MontoVta <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoDev * 100 / T1.MontoVta END AS PorDEV,"
  SQLTPD &= "T0.CANT_NETA AS PzsNeta, T0.MONTO_NETA AS MontoNeto, "
  SQLTPD &= "T0.ItmsGrpNam AS DesLinea, T2.SlpName AS Agente,T0.ItmsGrpCod AS CodLinea, "
  SQLTPD &= "T0.TipoMov, T0.IdAgente, T0.ArtAgte "
  SQLTPD &= "FROM #T_VTA_ART T0 INNER JOIN #T_AGTE_LIN T1 ON T0.IdAgente = T1.IdAgente AND T0.ItmsGrpCod = T1.CodLinea "
  SQLTPD &= "INNER JOIN OSLP T2 ON T0.IdAgente = T2.SlpCode "

  SQLTPD &= "UNION ALL "
  SQLTPD &= "SELECT T0.Articulo, T0.ItemName, "
  SQLTPD &= "T0.CANT_TOT AS PzsVta, T0.MONT_TOT AS MontoVta, "
  SQLTPD &= "CASE WHEN T0.MONT_TOT <= 0 OR  T1.MontoVta <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MONT_TOT * 100 / T1.MontoVta END AS PorVta,"
  SQLTPD &= "T0.CantDev,T0.MontoDev,  "
  SQLTPD &= "CASE WHEN T0.MontoDev <= 0 OR T1.MontoVta <= 0 THEN 0 ELSE "
  SQLTPD &= "T0.MontoDev * 100 / T1.MontoVta END AS PorDEV,"
  SQLTPD &= "T0.CANT_NETA AS PzsNeta, T0.MONTO_NETA AS MontoNeto, "
  SQLTPD &= "T0.ItmsGrpNam AS DesLinea, T2.SlpName AS Agente,'999' AS CodLinea, "
  SQLTPD &= "T0.TipoMov, T0.IdAgente, T0.ArtAgte "
  SQLTPD &= "FROM #T_VTA_ART T0 INNER JOIN #T_TOT_AGTE T1 ON T0.IdAgente = T1.IdAgente "
  SQLTPD &= "INNER JOIN OSLP T2 ON T0.IdAgente = T2.SlpCode WHERE CodLinea <> 999 ORDER BY MONTONETO DESC; "


  'SQLTPD &= "DROP TABLE #Tem_Canc  "
  SQLTPD &= "DROP TABLE #Tem_Dev  "
  SQLTPD &= "DROP TABLE #Tem_Fact  "
  SQLTPD &= "DROP TABLE #T_VTA_ART  "
  SQLTPD &= "DROP TABLE #T_DET_LINEA  "
  SQLTPD &= "DROP TABLE #T_TOT_LINEA  "
  SQLTPD &= "DROP TABLE #T_DET_LIN_P  "
  SQLTPD &= "DROP TABLE #T_AGTE_LIN  "
  SQLTPD &= "DROP TABLE #T_TOT_AGTE  "
  'SQLTPD &= "DROP TABLE #tmp_ORIN  "
  'SQLTPD &= "DROP TABLE #tmp_ORIN_DEV "


  'MsgBox(SQLTPD)


  ' Nuevo objeto Dataset   
  Dim DsVtasDet As New DataSet

  DTRefacciones.TableName = "EncVtasTot"

  DsVtasDet.Tables.Add(DTRefacciones)


  With comando
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   ' Asignar el sql para seleccionar los datos de la tabla Maestro   
   .CommandText = SQLTPD
   .CommandTimeout = 600
   .Connection = conexion2
  End With

  '/***************************parte de codigo'
  With Adaptador
   .SelectCommand = comando
   ' llenar el dataset   
   '.TableMappings.Add("DetLinea", "DetArticulo")
   .Fill(DsVtasDet, "DetLineas")
  End With


  DsVtasDet.Tables(1).TableName = "DetalleLinea"
  DsVtasDet.Tables(2).TableName = "DetalleArticulo"
  DsVtasDet.Tables(3).TableName = "Articulos"

  '**********************************************************************************************************************************
  dvLineas.Table = DsVtasDet.Tables("DetalleLinea")

  'GrdTodosArt.DataSource = dvTodosArt


  'Private dvLineas As New DataView
  'Private dvAgentes As New DataView
  'Private dvArticulos As New DataView


  With GrdConProd
   ' .DataMember = "EncVtasTot"
   .DataSource = dvLineas
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
   .Columns(1).Width = 55
   .Columns(1).DefaultCellStyle.Format = "###,###,###"

   .Columns(2).HeaderText = "$ Venta Total"
   .Columns(2).Width = 80
   .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(3).HeaderText = "% Sobre Vta."
   .Columns(3).Width = 50
   .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns(3).DefaultCellStyle.Format = "###.00"

   .Columns(4).HeaderText = "Cant. Piezas Devueltas"
   .Columns(4).Width = 60
   .Columns(4).DefaultCellStyle.Format = "###,###,###"

   .Columns(5).HeaderText = "$ Monto Devuelto"
   .Columns(5).Width = 74
   .Columns(5).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(6).HeaderText = "% Dev. Sobre Vta."
   .Columns(6).Width = 55
   .Columns(6).DefaultCellStyle.Format = "###.00"
   .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   .Columns(7).HeaderText = "Cant. Neta"
   .Columns(7).Width = 55
   .Columns(7).DefaultCellStyle.Format = "###,###,###"

   .Columns(8).HeaderText = "$ Monto Neto"
   .Columns(8).Width = 85
   .Columns(8).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(9).Visible = False

   .SelectionMode = DataGridViewSelectionMode.FullRowSelect
   .DefaultCellStyle.BackColor = Color.AliceBlue
  End With


  dvAgentes.Table = DsVtasDet.Tables("DetalleArticulo")

  dvAgentes.Sort = "MontoNeto DESC"


  ' Establecer el DataSource y el DataMember para el DataGridview Detalle   
  With GrdConLinea
   .DataSource = dvAgentes
   '.DataMember = "EncVtasTot.Relacion1"
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

   .Sort(GrdConLinea.Columns(0), ListSortDirection.Ascending)
   .Columns(0).HeaderText = "Agente"
   .Columns(0).Width = 160
   .Columns(0).Frozen = True

   .Columns(1).HeaderText = "Linea"
   .Columns(1).Frozen = True

   .Columns(2).HeaderText = "Cantidad Piezas"
   .Columns(2).Width = 55
   .Columns(2).DefaultCellStyle.Format = "###,###,###"

   .Columns(3).HeaderText = "$ Venta Total"
   .Columns(3).Width = 60
   .Columns(3).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(4).HeaderText = "% Sobre Vta."
   .Columns(4).Width = 60
   .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns(4).DefaultCellStyle.Format = "###.00"

   .Columns(5).HeaderText = "Cant. Piezas Devueltas"
   .Columns(5).Width = 60
   .Columns(5).DefaultCellStyle.Format = "###,###,###"

   .Columns(6).HeaderText = "$ Monto Devuelto"
   .Columns(6).Width = 60
   .Columns(6).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(7).HeaderText = "% Dev. Sobre Vta."
   .Columns(7).Width = 50
   .Columns(7).DefaultCellStyle.Format = "###.00"
   .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   .Columns(8).HeaderText = "Cant. Neta"
   .Columns(8).Width = 48
   .Columns(8).DefaultCellStyle.Format = "###,###,###"

   .Columns(9).HeaderText = "$ Monto Neto"
   .Columns(9).Width = 80
   .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   '.Sort(GrdConLinea.Columns(9), ListSortDirection.Descending)

   .Columns(10).Visible = False
   .Columns(11).Visible = False

   .SelectionMode = DataGridViewSelectionMode.FullRowSelect
   .DefaultCellStyle.BackColor = Color.AliceBlue


  End With

  dvTodosArt.Table = DsVtasDet.Tables("Articulos")
  dvTodosArt.Sort = "Agente ASC,DesLinea ASC,MontoNeto DESC"
  GrdTodosArt.DataSource = dvTodosArt


  dvArticulos.Table = DsVtasDet.Tables("Articulos")
  dvArticulos.Sort = "MontoNeto DESC"



  With GrdDetArt
   .DataSource = dvArticulos
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

   '.Sort(GrdDetArt.Columns(0), ListSortDirection.Descending)
   .Columns(0).HeaderText = "Artículo"
   .Columns(0).Width = 100
   .Columns(0).Frozen = True

   .Columns(1).HeaderText = "Descripción"
   .Columns(1).Width = 240
   .Columns(1).Frozen = True

   .Columns(2).HeaderText = "Cantidad Piezas"
   .Columns(2).Width = 50
   .Columns(2).DefaultCellStyle.Format = "###,###,###"

   .Columns(3).HeaderText = "$ Venta Total"
   .Columns(3).Width = 75
   .Columns(3).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(4).HeaderText = "% Sobre Vta."
   .Columns(4).Width = 40
   .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns(4).DefaultCellStyle.Format = "###.00"

   .Columns(5).HeaderText = "Cant. Piezas Devueltas"
   .Columns(5).Width = 40
   .Columns(5).DefaultCellStyle.Format = "###,###,###"

   .Columns(6).HeaderText = "$ Monto Devuelto"
   .Columns(6).Width = 60
   .Columns(6).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(7).HeaderText = "% Dev. Sobre Vta."
   .Columns(7).Width = 40
   .Columns(7).DefaultCellStyle.Format = "###.00"
   .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   .Columns(8).HeaderText = "Cant. Neta"
   .Columns(8).Width = 50
   .Columns(8).DefaultCellStyle.Format = "###,###,###"

   .Sort(GrdDetArt.Columns(9), ListSortDirection.Ascending)
   .Columns(9).HeaderText = "$ Monto Neto"
   .Columns(9).Width = 75
   .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   '.Sort(GrdDetArt.Columns(10), ListSortDirection.Ascending)
   .Columns(10).Visible = True

   .Sort(GrdDetArt.Columns(11), ListSortDirection.Ascending)
   .Columns(11).Visible = True
   .Columns(12).Visible = False
   .Columns(13).Visible = False
   .Columns(14).Visible = False
   .Columns(15).Visible = False





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
  ExportarNuevoAgentes()
 End Sub

 Sub ExportarNuevoAgentes()
  Dim dv As DataView = DirectCast(GrdConLinea.DataSource, DataView)
  'Dim ds As DataSet = DgVtaAgte.DataSource
  Dim dt As DataTable = dv.Table

  Dim wb = New XLWorkbook()
  Dim ws = wb.Worksheets.Add("Agentes")


  Dim range = ws.Range(3, 1, dt.Rows.Count + 3, dt.Columns.Count).Merge().AddToNamed("Agentes")
  Dim rangeWithData = ws.Cell(4, 1).InsertData(dt.AsEnumerable)

  ws.Columns("L").Delete()
  ws.Columns("K").Delete()

  Dim tab = range.CreateTable()
  tab.Theme = XLTableTheme.TableStyleLight8

  'DAR FOMATO A LAS CELDAS
  Dim index As Integer = 3

  For i As Integer = 0 To dt.Rows.Count

   Try
    'Encabezados dependiendo
    If index = 3 Then
     Dim cellA3 As String = String.Format("A{0}", 1)
     wb.Worksheet(1).Cells(cellA3).Value = "Reporte de Ventas Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
     wb.Worksheet(1).Cells(cellA3).Style.Font.Bold = True

     Dim cellA0 As String = String.Format("A{0}", index)
     wb.Worksheet(1).Cells(cellA0).Value = "Agente"

     Dim cellB0 As String = String.Format("B{0}", index)
     wb.Worksheet(1).Cells(cellB0).Value = "Linea"

     Dim cellC0 As String = String.Format("C{0}", index)
     wb.Worksheet(1).Cells(cellC0).Value = "Cantidad Piezas"

     Dim cellD0 As String = String.Format("D{0}", index)
     wb.Worksheet(1).Cells(cellD0).Value = "$ Venta Total"

     Dim cellE0 As String = String.Format("E{0}", index)
     wb.Worksheet(1).Cells(cellE0).Value = "% Sobre Vta."

     Dim cellF0 As String = String.Format("F{0}", index)
     wb.Worksheet(1).Cells(cellF0).Value = "Piezas Devueltas"

     Dim cellG0 As String = String.Format("G{0}", index)
     wb.Worksheet(1).Cells(cellG0).Value = "$ Monto Devuelto"

     Dim cellH0 As String = String.Format("H{0}", index)
     wb.Worksheet(1).Cells(cellH0).Value = "% Dev.Sobre Vta."

     Dim cellI0 As String = String.Format("I{0}", index)
     wb.Worksheet(1).Cells(cellI0).Value = "Cant. Neta"

     Dim cellJ0 As String = String.Format("J{0}", index)
     wb.Worksheet(1).Cells(cellJ0).Value = "$ Monto Neto"

     'AGREGAR LAS CELDAS DEPENDIENDO EL REPORTE SEGUIR LA MISMA ESTRUCTURA

     index = index + 1
    End If

    'Formato de cada una de las celdas
    Dim cellA As String = String.Format("A{0}", index)
    'wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellB As String = String.Format("B{0}", index)
    'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellC As String = String.Format("C{0}", index)
    wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "#,##0"

    Dim cellD As String = String.Format("D{0}", index)
    wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellE As String = String.Format("E{0}", index)
    wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "0.00\%"

    Dim cellF As String = String.Format("F{0}", index)
    wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "#,##0"

    Dim cellG As String = String.Format("G{0}", index)
    wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellH As String = String.Format("H{0}", index)
    wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "0.00\%"

    Dim cellI As String = String.Format("I{0}", index)
    wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "#,##0"

    Dim cellJ As String = String.Format("J{0}", index)
    wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "$ #,##0.00"

   Catch ex As Exception
    MessageBox.Show(ex.ToString(), "Error al dar formato a celdas")
   End Try

   index = index + 1
  Next

  ws.Columns().Width = 15
  ws.Rows(6).Style.Alignment.WrapText = False

  Try
   Dim saveFileDialog1 As New SaveFileDialog()
   saveFileDialog1.Filter = "Excel|*.xlsx"
   saveFileDialog1.Title = "Save Excel File"
   saveFileDialog1.FileName = "Reporte de agentes de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"
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
   MessageBox.Show(ex.ToString(), "Error al guardar el archivo")
  End Try
 End Sub

 Sub ExportarViejoAgentes()
  Dim oExcel As Object
  Dim oBook As Object
  Dim oSheet As Object

  'Abrimos un nuevo libro
  oExcel = CreateObject("Excel.Application")
  oBook = oExcel.workbooks.add
  oSheet = oBook.worksheets(1)

  'Declaramos el nombre de las columnas
  oSheet.range("A3").value = "Agente"
  oSheet.range("B3").value = "Cantidad Piezas"
  oSheet.range("C3").value = "$ Venta Total"
  oSheet.range("D3").value = "% Sobre Vta."
  oSheet.range("E3").value = "Piezas Devueltas"
  oSheet.range("F3").value = "$ Monto Devuelto"
  oSheet.range("G3").value = "% Dev.Sobre Vta."
  oSheet.range("H3").value = "Cant. Neta"
  oSheet.range("I3").value = "$ Monto Neto"
  oSheet.range("J3").value = "Linea"


  'para poner la primera fila de los titulos en negrita
  oSheet.range("A3:J3").font.bold = True
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
   oSheet.range("B" & fila_dt_excel).value = cel2
   oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel3, 2)
   oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
   oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
   oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
   oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
   oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)

   oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
   oSheet.range("J" & fila_dt_excel).value = cel10
  Next

  '' para que el tamano de la columna tenga como minimo el maximo de sus textos
  'oSheet.columns("A:N").entirecolumn.autofit()
  'oSheet.range("A1").value = "Reporte de Ventas Con Detalle de Lineas Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
  'oSheet.range("C1").value = Rangos
  'oSheet.range("C2").value = Rangos2

  'oExcel.visible = True
  'System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  'GC.Collect()
  'oSheet = Nothing
  'oBook = Nothing
  'oExcel = Nothing

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



 Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
  ExportarNuevoLinea()
 End Sub

 Sub ExportarNuevoLinea()
  Dim dv As DataView = DirectCast(GrdConProd.DataSource, DataView)
  'Dim ds As DataSet = DgVtaAgte.DataSource
  Dim dt As DataTable = dv.Table

  Dim wb = New XLWorkbook()
  Dim ws = wb.Worksheets.Add("Líneas")


  Dim range = ws.Range(3, 1, dt.Rows.Count + 3, dt.Columns.Count).Merge().AddToNamed("Líneas")
  Dim rangeWithData = ws.Cell(4, 1).InsertData(dt.AsEnumerable)

  ws.Columns("J").Delete()

  Dim tab = range.CreateTable()
  tab.Theme = XLTableTheme.TableStyleLight8

  'DAR FOMATO A LAS CELDAS
  Dim index As Integer = 3

  For i As Integer = 0 To dt.Rows.Count

   Try
    'Encabezados dependiendo
    If index = 3 Then
     Dim cellA3 As String = String.Format("A{0}", 1)
     wb.Worksheet(1).Cells(cellA3).Value = "Reporte de Ventas Con Detalle de Lineas Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
     wb.Worksheet(1).Cells(cellA3).Style.Font.Bold = True

     Dim cellA0 As String = String.Format("A{0}", index)
     wb.Worksheet(1).Cells(cellA0).Value = "Linea"

     Dim cellB0 As String = String.Format("B{0}", index)
     wb.Worksheet(1).Cells(cellB0).Value = "Cantidad Piezas"

     Dim cellC0 As String = String.Format("C{0}", index)
     wb.Worksheet(1).Cells(cellC0).Value = "$ Venta Total"

     Dim cellD0 As String = String.Format("D{0}", index)
     wb.Worksheet(1).Cells(cellD0).Value = "% Sobre Vta."

     Dim cellE0 As String = String.Format("E{0}", index)
     wb.Worksheet(1).Cells(cellE0).Value = "Piezas Devueltas"

     Dim cellF0 As String = String.Format("F{0}", index)
     wb.Worksheet(1).Cells(cellF0).Value = "$ Monto Devuelto"

     Dim cellG0 As String = String.Format("G{0}", index)
     wb.Worksheet(1).Cells(cellG0).Value = "% Dev.Sobre Vta."

     Dim cellH0 As String = String.Format("H{0}", index)
     wb.Worksheet(1).Cells(cellH0).Value = "Cant. Neta"

     Dim cellI0 As String = String.Format("I{0}", index)
     wb.Worksheet(1).Cells(cellI0).Value = "$ Monto Neto"

     'AGREGAR LAS CELDAS DEPENDIENDO EL REPORTE SEGUIR LA MISMA ESTRUCTURA

     index = index + 1
    End If

    'Formato de cada una de las celdas
    Dim cellA As String = String.Format("A{0}", index)
    'wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellB As String = String.Format("B{0}", index)
    wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "#,##0"

    Dim cellC As String = String.Format("C{0}", index)
    wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellD As String = String.Format("D{0}", index)
    wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "0.00\%"

    Dim cellE As String = String.Format("E{0}", index)
    wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "#,##0"

    Dim cellF As String = String.Format("F{0}", index)
    wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellG As String = String.Format("G{0}", index)
    wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "0.00\%"

    Dim cellH As String = String.Format("H{0}", index)
    wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "#,##0"

    Dim cellI As String = String.Format("I{0}", index)
    wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "$ #,##0.00"

    '"0.0\%"

   Catch ex As Exception
    MessageBox.Show(ex.ToString(), "Error al dar formato a celdas")
   End Try

   index = index + 1
  Next

  ws.Columns().Width = 15
  ws.Rows(6).Style.Alignment.WrapText = False

  Try
   Dim saveFileDialog1 As New SaveFileDialog()
   saveFileDialog1.Filter = "Excel|*.xlsx"
   saveFileDialog1.Title = "Save Excel File"
   saveFileDialog1.FileName = "Reporte de linea de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"
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
   MessageBox.Show(ex.ToString(), "Error al guardar el archivo")
  End Try
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

 Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
  ExportarNuevoArticulos()
 End Sub

 Sub ExportarNuevoArticulos()
  Dim dv As DataView = DirectCast(GrdDetArt.DataSource, DataView)
  'Dim ds As DataSet = DgVtaAgte.DataSource
  Dim dt As DataTable = dv.Table

  Dim wb = New XLWorkbook()
  Dim ws = wb.Worksheets.Add("Articulos")


  Dim range = ws.Range(3, 1, dt.Rows.Count + 3, dt.Columns.Count).Merge().AddToNamed("Articulos")
  Dim rangeWithData = ws.Cell(4, 1).InsertData(dt.AsEnumerable)

  ws.Columns("P").Delete()
  ws.Columns("O").Delete()
  ws.Columns("N").Delete()
  ws.Columns("M").Delete()

  Dim tab = range.CreateTable()
  tab.Theme = XLTableTheme.TableStyleLight8

  'DAR FOMATO A LAS CELDAS
  Dim index As Integer = 3

  For i As Integer = 0 To dt.Rows.Count

   Try
    'Encabezados dependiendo
    If index = 3 Then
     Dim cellA3 As String = String.Format("A{0}", 1)
     wb.Worksheet(1).Cells(cellA3).Value = "Reporte de Ventas Con Detalle de Articulos Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
     wb.Worksheet(1).Cells(cellA3).Style.Font.Bold = True

     Dim cellA0 As String = String.Format("A{0}", index)
     wb.Worksheet(1).Cells(cellA0).Value = "Articulo"

     Dim cellB0 As String = String.Format("B{0}", index)
     wb.Worksheet(1).Cells(cellB0).Value = "Descripción"

     Dim cellC0 As String = String.Format("C{0}", index)
     wb.Worksheet(1).Cells(cellC0).Value = "Cantidad Piezas"

     Dim cellD0 As String = String.Format("D{0}", index)
     wb.Worksheet(1).Cells(cellD0).Value = "$ Venta Total"

     Dim cellE0 As String = String.Format("E{0}", index)
     wb.Worksheet(1).Cells(cellE0).Value = "% Sobre Vta."

     Dim cellF0 As String = String.Format("F{0}", index)
     wb.Worksheet(1).Cells(cellF0).Value = "Cant.Piezas Dev."

     Dim cellG0 As String = String.Format("G{0}", index)
     wb.Worksheet(1).Cells(cellG0).Value = "$ Monto Devuelto"

     Dim cellH0 As String = String.Format("H{0}", index)
     wb.Worksheet(1).Cells(cellH0).Value = "% Dev. Sobre Vta."

     Dim cellI0 As String = String.Format("I{0}", index)
     wb.Worksheet(1).Cells(cellI0).Value = "Cant. Neta"

     Dim cellJ0 As String = String.Format("J{0}", index)
     wb.Worksheet(1).Cells(cellJ0).Value = "$ Monto Neto"

     Dim cellK0 As String = String.Format("K{0}", index)
     wb.Worksheet(1).Cells(cellK0).Value = "Linea"

     Dim cellL0 As String = String.Format("L{0}", index)
     wb.Worksheet(1).Cells(cellL0).Value = "Agente"

     'AGREGAR LAS CELDAS DEPENDIENDO EL REPORTE SEGUIR LA MISMA ESTRUCTURA

     index = index + 1
    End If

    'Formato de cada una de las celdas
    Dim cellA As String = String.Format("A{0}", index)
    'wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellB As String = String.Format("B{0}", index)
    'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellC As String = String.Format("C{0}", index)
    wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "#,##0"

    Dim cellD As String = String.Format("D{0}", index)
    wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellE As String = String.Format("E{0}", index)
    wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "0.00\%"

    Dim cellF As String = String.Format("F{0}", index)
    wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "#,##0"

    Dim cellG As String = String.Format("G{0}", index)
    wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellH As String = String.Format("H{0}", index)
    wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "0.00\%"

    Dim cellI As String = String.Format("I{0}", index)
    wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "#,##0"

    Dim cellJ As String = String.Format("J{0}", index)
    wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellK As String = String.Format("K{0}", index)
    'wb.Worksheet(1).Cells(cellK).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellL As String = String.Format("L{0}", index)
    'wb.Worksheet(1).Cells(cellL).Style.NumberFormat.Format = "$ #,##0.00"

   Catch ex As Exception
    MessageBox.Show(ex.ToString(), "Error al dar formato a celdas")
   End Try

   index = index + 1
  Next

  ws.Columns().Width = 15
  ws.Rows(6).Style.Alignment.WrapText = False

  Try
   Dim saveFileDialog1 As New SaveFileDialog()
   saveFileDialog1.Filter = "Excel|*.xlsx"
   saveFileDialog1.Title = "Save Excel File"
   saveFileDialog1.FileName = "Reporte de articulos de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"
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
   MessageBox.Show(ex.ToString(), "Error al guardar el archivo")
  End Try
 End Sub

 Sub ExportarViejoArticulos()
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
  oSheet.range("C3").value = "Cantidad Piezas"
  oSheet.range("D3").value = "$ Venta Total"
  oSheet.range("E3").value = "% Sobre Vta."
  oSheet.range("F3").value = "Cant.Piezas Dev."
  oSheet.range("G3").value = "$ Monto Devuelto"
  oSheet.range("H3").value = "% Dev. Sobre Vta."
  oSheet.range("I3").value = "Cant. Neta"
  oSheet.range("J3").value = "$ Monto Neto"
  oSheet.range("K3").value = "Linea"
  oSheet.range("L3").value = "Agente"


  'para poner la primera fila de los titulos en negrita
  oSheet.range("A3:L3").font.bold = True
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
   oSheet.range("K" & fila_dt_excel).value = cel11
   oSheet.range("L" & fila_dt_excel).value = cel12
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

 Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
  'ExportarNuevoReporte()
  ExportarViejoReporte()
 End Sub

 Sub ExportarNuevoReporte()
  Dim dv As DataView = DirectCast(GrdConProd.DataSource, DataView)
  'Dim ds As DataSet = DgVtaAgte.DataSource
  Dim dt As DataTable = dv.Table

  Dim wb = New XLWorkbook()
  Dim ws = wb.Worksheets.Add("Líneas")

  Dim range = ws.Range(3, 1, dt.Rows.Count + 3, dt.Columns.Count).Merge().AddToNamed("Líneas")
  Dim rangeWithData = ws.Cell(4, 1).InsertData(dt.AsEnumerable)

  ws.Columns("J").Delete()

  Dim tab = range.CreateTable()
  tab.Theme = XLTableTheme.TableStyleLight8

  'DAR FOMATO A LAS CELDAS
  Dim index As Integer = 3

  For i As Integer = 0 To dt.Rows.Count

   Try
    'Encabezados dependiendo
    If index = 3 Then
     Dim cellA3 As String = String.Format("A{0}", 1)
     wb.Worksheet(1).Cells(cellA3).Value = "Reporte de Ventas Con Detalle de Lineas Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
     wb.Worksheet(1).Cells(cellA3).Style.Font.Bold = True

     Dim cellA0 As String = String.Format("A{0}", index)
     wb.Worksheet(1).Cells(cellA0).Value = "Linea"

     Dim cellB0 As String = String.Format("B{0}", index)
     wb.Worksheet(1).Cells(cellB0).Value = "Cantidad Piezas"

     Dim cellC0 As String = String.Format("C{0}", index)
     wb.Worksheet(1).Cells(cellC0).Value = "$ Venta Total"

     Dim cellD0 As String = String.Format("D{0}", index)
     wb.Worksheet(1).Cells(cellD0).Value = "% Sobre Vta."

     Dim cellE0 As String = String.Format("E{0}", index)
     wb.Worksheet(1).Cells(cellE0).Value = "Piezas Devueltas"

     Dim cellF0 As String = String.Format("F{0}", index)
     wb.Worksheet(1).Cells(cellF0).Value = "$ Monto Devuelto"

     Dim cellG0 As String = String.Format("G{0}", index)
     wb.Worksheet(1).Cells(cellG0).Value = "% Dev.Sobre Vta."

     Dim cellH0 As String = String.Format("H{0}", index)
     wb.Worksheet(1).Cells(cellH0).Value = "Cant. Neta"

     Dim cellI0 As String = String.Format("I{0}", index)
     wb.Worksheet(1).Cells(cellI0).Value = "$ Monto Neto"

     'AGREGAR LAS CELDAS DEPENDIENDO EL REPORTE SEGUIR LA MISMA ESTRUCTURA

     index = index + 1
    End If

    'Formato de cada una de las celdas
    Dim cellA As String = String.Format("A{0}", index)
    'wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellB As String = String.Format("B{0}", index)
    wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "#,##0"

    Dim cellC As String = String.Format("C{0}", index)
    wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellD As String = String.Format("D{0}", index)
    wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "0.00\%"

    Dim cellE As String = String.Format("E{0}", index)
    wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "#,##0"

    Dim cellF As String = String.Format("F{0}", index)
    wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellG As String = String.Format("G{0}", index)
    wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "0.00\%"

    Dim cellH As String = String.Format("H{0}", index)
    wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "#,##0"

    Dim cellI As String = String.Format("I{0}", index)
    wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "$ #,##0.00"

    '"0.0\%"

   Catch ex As Exception
    MessageBox.Show(ex.ToString(), "Error al dar formato a celdas")
   End Try

   index = index + 1
  Next

  ws.Columns().Width = 15
  ws.Rows(6).Style.Alignment.WrapText = False


  'TABLA DE AGENTES-------------------------------------------------------------------------------------
  dv = DirectCast(GrdConLinea.DataSource, DataView)
  'Dim ds As DataSet = DgVtaAgte.DataSource
  dt = dv.Table

  'Dim wb = New XLWorkbook()
  ws = wb.Worksheets.Add("Agentes")

  range = ws.Range(3, 1, dt.Rows.Count + 3, dt.Columns.Count).Merge().AddToNamed("Agentes")
  rangeWithData = ws.Cell(4, 1).InsertData(dt.AsEnumerable)

  ws.Columns("L").Delete()
  ws.Columns("K").Delete()

  tab = range.CreateTable("Agentes")
  tab.Theme = XLTableTheme.TableStyleLight8

  'DAR FOMATO A LAS CELDAS
  index = 3

  For i As Integer = 0 To dt.Rows.Count

   Try
    'Encabezados dependiendo
    If index = 3 Then
     Dim cellA3 As String = String.Format("A{0}", 1)
     wb.Worksheet(2).Cells(cellA3).Value = "Reporte de Ventas Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
     wb.Worksheet(2).Cells(cellA3).Style.Font.Bold = True

     Dim cellA0 As String = String.Format("A{0}", index)
     wb.Worksheet(2).Cells(cellA0).Value = "Agente"

     Dim cellB0 As String = String.Format("B{0}", index)
     wb.Worksheet(2).Cells(cellB0).Value = "Linea"

     Dim cellC0 As String = String.Format("C{0}", index)
     wb.Worksheet(2).Cells(cellC0).Value = "Cantidad Piezas"

     Dim cellD0 As String = String.Format("D{0}", index)
     wb.Worksheet(2).Cells(cellD0).Value = "$ Venta Total"

     Dim cellE0 As String = String.Format("E{0}", index)
     wb.Worksheet(2).Cells(cellE0).Value = "% Sobre Vta."

     Dim cellF0 As String = String.Format("F{0}", index)
     wb.Worksheet(2).Cells(cellF0).Value = "Piezas Devueltas"

     Dim cellG0 As String = String.Format("G{0}", index)
     wb.Worksheet(2).Cells(cellG0).Value = "$ Monto Devuelto"

     Dim cellH0 As String = String.Format("H{0}", index)
     wb.Worksheet(2).Cells(cellH0).Value = "% Dev.Sobre Vta."

     Dim cellI0 As String = String.Format("I{0}", index)
     wb.Worksheet(2).Cells(cellI0).Value = "Cant. Neta"

     Dim cellJ0 As String = String.Format("J{0}", index)
     wb.Worksheet(2).Cells(cellJ0).Value = "$ Monto Neto"

     'AGREGAR LAS CELDAS DEPENDIENDO EL REPORTE SEGUIR LA MISMA ESTRUCTURA

     index = index + 1
    End If

    'Formato de cada una de las celdas
    Dim cellA As String = String.Format("A{0}", index)
    'wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellB As String = String.Format("B{0}", index)
    'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellC As String = String.Format("C{0}", index)
    wb.Worksheet(2).Cells(cellC).Style.NumberFormat.Format = "#,##0"

    Dim cellD As String = String.Format("D{0}", index)
    wb.Worksheet(2).Cells(cellD).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellE As String = String.Format("E{0}", index)
    wb.Worksheet(2).Cells(cellE).Style.NumberFormat.Format = "0.00\%"

    Dim cellF As String = String.Format("F{0}", index)
    wb.Worksheet(2).Cells(cellF).Style.NumberFormat.Format = "#,##0"

    Dim cellG As String = String.Format("G{0}", index)
    wb.Worksheet(2).Cells(cellG).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellH As String = String.Format("H{0}", index)
    wb.Worksheet(2).Cells(cellH).Style.NumberFormat.Format = "0.00\%"

    Dim cellI As String = String.Format("I{0}", index)
    wb.Worksheet(2).Cells(cellI).Style.NumberFormat.Format = "#,##0"

    Dim cellJ As String = String.Format("J{0}", index)
    wb.Worksheet(2).Cells(cellJ).Style.NumberFormat.Format = "$ #,##0.00"

   Catch ex As Exception
    MessageBox.Show(ex.ToString(), "Error al dar formato a celdas")
   End Try

   index = index + 1
  Next

  ws.Columns().Width = 15
  ws.Rows(6).Style.Alignment.WrapText = False

















  Try
   Dim saveFileDialog1 As New SaveFileDialog()
   saveFileDialog1.Filter = "Excel|*.xlsx"
   saveFileDialog1.Title = "Save Excel File"
   saveFileDialog1.FileName = "Reporte de general de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"
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
   MessageBox.Show(ex.ToString(), "Error al guardar el archivo")
  End Try
 End Sub

 Sub ExportarViejoReporte()
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


  'para poner la primera fila de los titulos en negrita
  oSheet.range("A3:L3").font.bold = True
  Dim fila_dt As Integer = 0
  Dim fila_dt_excel As Integer = 0
  Dim tanto_porcentaje As String = ""
  Dim marikona As Integer = 0

  Dim total_reg As Integer = 0


  'Dim cel1 As String
  'Dim cel2 As String
  'Dim cel3 As String
  'Dim cel4 As String
  'Dim cel5 As String
  'Dim cel6 As String
  'Dim cel7 As String
  'Dim cel8 As String

  'Dim cel9 As String






  'para leer una celda en concreto
  'el numero es la columna

  Dim cel1 As String = Me.GrdConProd.Item(0, GrdConProd.CurrentCell.RowIndex).Value
  Dim cel2 As String = Me.GrdConProd.Item(1, GrdConProd.CurrentCell.RowIndex).Value
  Dim cel3 As String = IIf(IsDBNull(Me.GrdConProd.Item(2, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(2, GrdConProd.CurrentCell.RowIndex).Value)
  Dim cel4 As String = IIf(IsDBNull(Me.GrdConProd.Item(3, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(3, GrdConProd.CurrentCell.RowIndex).Value)
  Dim cel5 As String = IIf(IsDBNull(Me.GrdConProd.Item(4, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(4, GrdConProd.CurrentCell.RowIndex).Value)
  Dim cel6 As String = IIf(IsDBNull(Me.GrdConProd.Item(5, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(5, GrdConProd.CurrentCell.RowIndex).Value)
  Dim cel7 As String = IIf(IsDBNull(Me.GrdConProd.Item(6, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(6, GrdConProd.CurrentCell.RowIndex).Value)
  Dim cel8 As String = IIf(IsDBNull(Me.GrdConProd.Item(7, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(7, GrdConProd.CurrentCell.RowIndex).Value)

  Dim cel9 As String = IIf(IsDBNull(Me.GrdConProd.Item(8, GrdConProd.CurrentCell.RowIndex).Value), 0, Me.GrdConProd.Item(8, GrdConProd.CurrentCell.RowIndex).Value)




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




  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  oSheet.columns("A:N").entirecolumn.autofit()
  oSheet.range("A1").value = "Reporte de Ventas Con Detalle de Lineas Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
  oSheet.range("C1").value = Rangos
  oSheet.range("C2").value = Rangos2


  '***************************************************Codigo de reporte de lineas
  'Declaramos el nombre de las columnas
  oSheet.range("A7").value = "Agente"
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
   oSheet.range("B" & fila_dt_excel).value = cel2
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
  oSheet.range("C" & valreg.ToString).value = "Cantidad Piezas"
  oSheet.range("D" & valreg.ToString).value = "$ Venta Total"
  oSheet.range("E" & valreg.ToString).value = "% Sobre Vta."
  oSheet.range("F" & valreg.ToString).value = "Cant.Piezas Dev."
  oSheet.range("G" & valreg.ToString).value = "$ Monto Devuelto"
  oSheet.range("H" & valreg.ToString).value = "% Dev. Sobre Vta."
  oSheet.range("I" & valreg.ToString).value = "Cant. Piezas Netas"
  oSheet.range("J" & valreg.ToString).value = "$ Total Ventas Netas"
  oSheet.range("K" & valreg.ToString).value = "Linea"
  oSheet.range("L" & valreg.ToString).value = "Agente"


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
   oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel3, 2)
   oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
   oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
   oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
   oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
   oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)

   oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
   oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel10, 2)
   oSheet.range("K" & fila_dt_excel).value = cel11
   oSheet.range("L" & fila_dt_excel).value = cel12
  Next



  oSheet.Columns("J:J").ColumnWidth = 18
  oSheet.Columns("A:A").ColumnWidth = 25
  oSheet.Columns("B:B").ColumnWidth = 50
  oSheet.Columns("A:A").NumberFormat = "@"

  oExcel.visible = True
  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  GC.Collect()
  oSheet = Nothing
  oBook = Nothing
  oExcel = Nothing
 End Sub

 Private Sub GrdConProd_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdConProd.SelectionChanged
  Try
   dvAgentes.RowFilter = "CodLinea =" & GrdConProd.Item(9, GrdConProd.CurrentRow.Index).Value.ToString
  Catch ex As Exception
  End Try
 End Sub

 Private Sub GrdConLinea_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdConLinea.SelectionChanged
  Try
   'dvArticulos.RowFilter = "CodLinea =" & GrdConLinea.Item(11, GrdConLinea.CurrentRow.Index).Value.ToString & " AND " &
   '                "IdAgente =" & GrdConLinea.Item(10, GrdConLinea.CurrentRow.Index).Value.ToString

   If (GrdConLinea.Item(11, GrdConLinea.CurrentRow.Index).Value.ToString() = "999") Then
    dvArticulos.RowFilter = "IdAgente =" & GrdConLinea.Item(10, GrdConLinea.CurrentRow.Index).Value.ToString
   Else
    dvArticulos.RowFilter = "CodLinea =" & GrdConLinea.Item(11, GrdConLinea.CurrentRow.Index).Value.ToString & " AND " &
                   "IdAgente =" & GrdConLinea.Item(10, GrdConLinea.CurrentRow.Index).Value.ToString
   End If

  Catch ex As Exception
   dvArticulos.RowFilter = "CodLinea = 0"
  End Try


  'Try
  '    If GrdConProd.Item(9, GrdConProd.CurrentRow.Index).Value = 999 Then
  '        dvArticulos.RowFilter = "CodLinea = 999"
  '    Else
  '        dvArticulos.RowFilter = "CodLinea =" & GrdConLinea.Item(11, GrdConLinea.CurrentRow.Index).Value.ToString & " AND " & _
  '                "IdAgente =" & GrdConLinea.Item(10, GrdConLinea.CurrentRow.Index).Value.ToString
  '    End If

  'Catch ex As Exception
  '    dvArticulos.RowFilter = "CodLinea = 0"
  'End Try


 End Sub

 Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

 End Sub

 Private Sub Button5_Click(sender As Object, e As EventArgs)
  cargar_registros()
 End Sub

 Private Sub GrdConProd_CurrentCellChanged(sender As Object, e As EventArgs) Handles GrdConProd.CurrentCellChanged
  'GrdConProd.CurrentCell = GrdConProd.Rows(0).Cells(1)
  Try
   dvAgentes.RowFilter = "CodLinea =" & GrdConProd.Item(9, GrdConProd.CurrentRow.Index).Value.ToString
  Catch ex As Exception
  End Try
 End Sub

 Private Sub GrdConLinea_CurrentCellChanged(sender As Object, e As EventArgs) Handles GrdConLinea.CurrentCellChanged
  Try
   dvArticulos.RowFilter = "DesLinea = '" & GrdConLinea.Item(1, GrdConLinea.CurrentCell.RowIndex).Value.ToString & "'"
   'dvTodosArt.RowFilter = 
  Catch ex As Exception

  End Try
 End Sub

 Private Sub ckCteProp_CheckedChanged(sender As Object, e As EventArgs) Handles ckCteProp.CheckedChanged

 End Sub
End Class