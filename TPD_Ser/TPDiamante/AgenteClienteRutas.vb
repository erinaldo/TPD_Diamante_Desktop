Imports System.Data.SqlClient
Imports System.Data
Imports ClosedXML.Excel
Imports System.IO

Public Class AgenteClienteRutas

  Private dvLineas As New DataView
  Private dvVtaMens As New DataView
  Private dvClientes As New DataView
  Private dvArticulos As New DataView


  Dim Rangos As String = ""
  Dim Rangos2 As String = ""

  Private Sub ConsultaProd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
    Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

    If VEsAgente = 1 Then
      CkCteProp.Checked = False
      CkCteProp.Enabled = False
    End If

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

    Dim SQLTPD As String = ""


    'CANCELACIONES -> Articulo -> Agte,Clte------------------------------------------------------------------------------------------------------

    If (DtpFechaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
      If (DtpFechaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
        SQLTPD = "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte, "
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
        SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov, "
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte, "
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
        SQLTPD &= "INTO #Tem_Canc "
        SQLTPD &= "FROM ORIN T0 "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "
        SQLTPD &= " AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F')  "
        SQLTPD &= " AND DocType  = 'I' AND case when t0.docdate <= '2017-11-19' then EDocNum  "
        SQLTPD &= "else (select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end is null "
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If
        SQLTPD &= " GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "

      ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
        SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
        SQLTPD &= "into #tmp "
        SQLTPD &= "from ORIN t0 "
        SQLTPD &= "where t0.DocDate >= @FechaIni and t0.DocDate <= @FechaTer "
        SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F')   "
        SQLTPD &= "and DocType = 'I' and case when t0.docdate <= '2017-11-19' then EDocNum   "
        SQLTPD &= "else (select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end is null "
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If
        SQLTPD &= "union all "
        SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
        SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
        SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
        SQLTPD &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
        SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer "
        SQLTPD &= "and t0.DocType <> 'S' "
        SQLTPD &= "and t2.ItmsGrpCod <> 200 "
        'SQLTPD &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
        SQLTPD &= "AND ((CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate >= '2020-08-24' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
        SQLTPD &= "AND T0.U_BXP_TIMBRADO <> 'T') "
        SQLTPD &= "AND t3.ReportID IS NULL "
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If

        SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte,  "
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,  "
        SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,  "
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte,  "
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA  "
        SQLTPD &= "INTO #Tem_Canc  "
        SQLTPD &= "FROM #tmp T0  "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "
        SQLTPD &= "drop table #tmp "

      End If
    ElseIf (DtpFechaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
      If (DtpFechaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then
        'NO HACE NADA
      ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
        'SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.CardCode, t0.DiscPrcnt  "
        'SQLTPD &= "into #tmp "
        'SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
        'SQLTPD &= "where t0.DocDate >= @FechaIni and t0.DocDate <= @FechaTer "
        'SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F')   "
        'SQLTPD &= "and t1.ItemCode <> 'DESCUENTO P.P' and (select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) is null "
        SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
        SQLTPD &= "into #tmp "
        SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
        SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
        SQLTPD &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
        SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer "
        SQLTPD &= "and t0.DocType <> 'S' "
        SQLTPD &= "and t2.ItmsGrpCod <> 200 "
        'SQLTPD &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
        SQLTPD &= "AND ((CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate >= '2020-08-24' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
        SQLTPD &= "AND T0.U_BXP_TIMBRADO <> 'T') "
        SQLTPD &= "AND T3.ReportID IS NULL "
        'VALIDA SE ESTA ACTIVA LA CASILLA DE CLIENTES PROPIOS
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If

        SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte,  "
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,  "
        SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,  "
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte,  "
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA  "
        SQLTPD &= "INTO #Tem_Canc  "
        SQLTPD &= "FROM #tmp T0  "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "
        SQLTPD &= "drop table #tmp "

      End If
    End If

    'SQLTPD = "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte, "
    'SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
    'SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov, "
    'SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte, "
    'SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
    'SQLTPD &= "INTO #Tem_Canc "
    'SQLTPD &= "FROM ORIN T0 "
    'SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry "
    'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "
    'SQLTPD &= " AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F')  "
    'SQLTPD &= " AND DocType  = 'I' AND  EDocNum IS NULL "
    ' '''' ICR_24022015 se agrego cambio para los clientes propis
    'If (CkCteProp.Checked = False) Then
    '    SQLTPD &= " and T0.slpCode <> 1 "
    'End If
    'SQLTPD &= " GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "



    'DESCUENTOS PP -> Articulo-Agte -> Clte -----------------------------------------------------------------------------------------------------------------
    If (DtpFechaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
      If (DtpFechaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
        SQLTPD &= "SELECT 'DESCUENTOS PP' AS ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte, "
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
        SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,"
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) +  'DESCUENTOS PP' + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte, "
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
        SQLTPD &= "INTO #Tem_DesPP "
        SQLTPD &= "FROM ORIN T0 "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "
        SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F') AND T0.DocType  = 'S' "
        'VALIDA QUE LA CASILLA DE CLIENTES PROPIOS ESTE ACTIVA
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If
        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "
      ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
        SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
        SQLTPD &= "into #tmp2 "
        SQLTPD &= "from ORIN t0  "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer  "
        SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F') AND T0.DocType  = 'S'  "
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If
        SQLTPD &= "union all "
        SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
        SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
        SQLTPD &= "where  "
        SQLTPD &= "t0.DocDate between @FechaIni and @FechaTer and  "
        SQLTPD &= "t0.DocType = 'I' "
        SQLTPD &= "AND (t1.ItemCode = 'DESCUENTO P.P' OR t1.ItemCode = 'AP_ANTICIPO') "
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If

        SQLTPD &= "SELECT 'DESCUENTOS PP' AS ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte,  "
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,  "
        SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov, "
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) +  'DESCUENTOS PP' + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte,  "
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA  "
        SQLTPD &= "INTO #aux  "
        SQLTPD &= "FROM #tmp2 T0  "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "

        SQLTPD &= "select ItemCode, SUM(CANTIDAD) as 'CANTIDAD', IdAgente, IdClte, SUM(TotalSinIva) as 'TotalSinIva', TipoMov, ArtAgte, CANTNETA  "
        SQLTPD &= "INTO #Tem_DesPP  "
        SQLTPD &= "from #aux  "
        SQLTPD &= "group by ItemCode, IdAgente, IdClte, TipoMov, ArtAgte, CANTNETA "
        SQLTPD &= "drop table #aux "
        SQLTPD &= "drop table #tmp2 "
      End If
    ElseIf (DtpFechaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
      If (DtpFechaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then
        'NO HACE NADA
      ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then

        SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
        SQLTPD &= "into #tmp1 "
        SQLTPD &= "FROM SBO_TPD.dbo.OINV T0 inner join SBO_TPD.dbo.INV1 T00 ON T0.DocEntry = T00.DocEntry "
        SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer "
        SQLTPD &= "and t0.DocType ='I' "
        SQLTPD &= "AND (T00.ItemCode = 'DESCUENTO P.P' OR T00.ItemCode = 'AP_ANTICIPO') "
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If

        SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
        SQLTPD &= "into #tmp2 "
        SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
        SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer "
        SQLTPD &= "and t0.DocType = 'I' "
        SQLTPD &= "AND (t1.ItemCode = 'DESCUENTO P.P' OR t1.ItemCode = 'AP_ANTICIPO') "
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If

        SQLTPD &= "SELECT 'DESCUENTOS PP' AS ItemCode, SUM(T1.Quantity) AS CANTIDAD, T0.SlpCode AS IdAgente, T0.CardCode AS IdClte, "
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) *-1 AS TotalSinIva,  "
        SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov, "
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) +  'DESCUENTOS PP' + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte,  "
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA   "
        SQLTPD &= "INTO #aux  "
        SQLTPD &= "FROM #tmp1 T0  "
        SQLTPD &= "INNER JOIN INV1 T1 ON T1.DocEntry = T0.DocEntry  "
        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "

        SQLTPD &= "union all "

        SQLTPD &= "SELECT 'DESCUENTOS PP' AS ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte, "
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,  "
        SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov, "
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) +  'DESCUENTOS PP' + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte,  "
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA   "
        SQLTPD &= "FROM #tmp2 T0  "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "

        SQLTPD &= "select ItemCode, SUM(CANTIDAD) as 'CANTIDAD', IdAgente, IdClte, SUM(TotalSinIva) as 'TotalSinIva', TipoMov, ArtAgte, CANTNETA  "
        SQLTPD &= "INTO #Tem_DesPP  "
        SQLTPD &= "from #aux  "
        SQLTPD &= "group by ItemCode, IdAgente, IdClte, TipoMov, ArtAgte, CANTNETA "

        SQLTPD &= "drop table #aux  "
        SQLTPD &= "drop table #tmp1 "
        SQLTPD &= "drop table #tmp2 "
      End If
    End If

    'SQLTPD &= "SELECT 'DESCUENTOS PP' AS ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte, "
    'SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
    'SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,"
    'SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) +  'DESCUENTOS PP' + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte, "
    'SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
    'SQLTPD &= "INTO #Tem_DesPP "
    'SQLTPD &= "FROM ORIN T0 "
    'SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry "
    'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "
    'SQLTPD &= " AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F') AND T0.DocType  = 'S' "
    ''---' ICR_24022015 se agrego cambio para los clientes propios
    'If (CkCteProp.Checked = False) Then
    '    SQLTPD &= " and T0.slpCode <> 1 "
    'End If
    'SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "


    'DEVOLUCIONES -> Articulo-Agte -> Clte---------------------------------------------------------------------------------------------------------------------
    If (DtpFechaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
      If (DtpFechaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
        SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte,  "
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,  "
        SQLTPD &= "(CAST('DEVOLUCION' AS CHAR(20))) AS TipoMov,  "
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte, "
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA  "
        SQLTPD &= "INTO #Tem_Dev "
        SQLTPD &= "FROM ORIN T0  "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer  "
        SQLTPD &= "AND DocType  = 'I' AND  CASE when T0.DocDate <= '2017-11-19' then EDocNum  "
        SQLTPD &= "else (select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end IS NOT NULL  "
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If
        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode  "
      ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
        SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
        SQLTPD &= "into #tmp3 "
        SQLTPD &= "from ORIN t0 "
        SQLTPD &= "where T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer  "
        SQLTPD &= "AND DocType  = 'I' AND  CASE when T0.DocDate <= '2017-11-19' then EDocNum  "
        SQLTPD &= "else (select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end IS NOT NULL  "
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If
        SQLTPD &= "union all "
        SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
        SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
        SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
        SQLTPD &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
        SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer "
        SQLTPD &= "and t0.DocType <> 'S' "
        SQLTPD &= "and t2.ItmsGrpCod <> 200 "
        SQLTPD &= "AND (T3.ReportID IS NOT NULL OR T0.U_BXP_UUID IS NOT NULL) "
        SQLTPD &= "AND ((T0.U_BXP_TIMBRADO = 'T' OR T0.U_BXP_TIMBRADO = 'P') OR T0.EDocGenTyp = 'G') "
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If

        SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte,  "
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,  "
        SQLTPD &= "(CAST('DEVOLUCION' AS CHAR(20))) AS TipoMov,  "
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte, "
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA  "
        SQLTPD &= "INTO #Tem_Dev  "
        SQLTPD &= "FROM #tmp3 T0  "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode  "
        SQLTPD &= "drop table #tmp3 "

      End If
    ElseIf (DtpFechaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
      If (DtpFechaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then

      ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
        SQLTPD &= "select distinct t0.DocNum, t0.DocEntry, t0.SlpCode, t0.CardCode, t0.DiscPrcnt "
        SQLTPD &= "into #tmp3 "
        SQLTPD &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
        SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
        SQLTPD &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
        SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer "
        SQLTPD &= "and t0.DocType <> 'S' "
        SQLTPD &= "and t2.ItmsGrpCod <> 200 "
        SQLTPD &= "AND (T3.ReportID IS NOT NULL OR T0.U_BXP_UUID IS NOT NULL) "
        SQLTPD &= "AND ((T0.U_BXP_TIMBRADO = 'T' OR T0.U_BXP_TIMBRADO = 'P') OR T0.EDocGenTyp = 'G') "
        If (CkCteProp.Checked = False) Then
          SQLTPD &= " and T0.slpCode <> 1 "
        End If

        SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte,  "
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,  "
        SQLTPD &= "(CAST('DEVOLUCION' AS CHAR(20))) AS TipoMov,  "
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte, "
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA  "
        SQLTPD &= "INTO #Tem_Dev  "
        SQLTPD &= "FROM #tmp3 T0  "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry  "
        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode  "
        SQLTPD &= "drop table #tmp3 "
      End If
    End If

    'FACTURADO -> Articulo-Agte -> Clte -----------------------------------------------------------------------------------------------------------------------------
    'VALIDA FECHAS PARA SABER COMO 
    SQLTPD &= "SELECT CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS ItemCode, "
    SQLTPD &= "SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte, "
    SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null  THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva, "
    SQLTPD &= "CAST(CASE WHEN T1.ItemCode IS NULL THEN 'NCARGO' ELSE 'FACTURACION' END AS CHAR(20))AS TipoMov, "
    SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END + "
    SQLTPD &= "RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte, "
    SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
    SQLTPD &= "INTO #Tem_Fact "
    SQLTPD &= "from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry  "
    SQLTPD &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
    SQLTPD &= "where t0.DocDate between @FechaIni and @FechaTer  "
    SQLTPD &= "and t0.DocType <> 'S' "
    SQLTPD &= "and t2.ItmsGrpCod <> 200 "
    '---' ICR_24022015 se agrego cambio para los clientes propios
    If (CkCteProp.Checked = False) Then
      SQLTPD &= " and T0.slpCode <> 1 "
    End If
    SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "


    '--'--CONSULTA CON INSERT SE BUSCAN LOS ARTICULOS CANCELADOS QUE NO HAN SIDO FACTURADOS EN ESE DIA POR ESE AGENTE
    SQLTPD &= "INSERT #Tem_Fact (ItemCode, CANTIDAD, IdAgente,IdClte,TotalSinIva,TipoMov,ArtAgte,CANTNETA) "
    SQLTPD &= "SELECT ItemCode, 0, IdAgente,IdClte,0,TipoMov,ArtAgte,CANTNETA "
    SQLTPD &= "FROM #Tem_Canc AS T0 WHERE T0.ArtAgte NOT IN (SELECT T1.ArtAgte FROM #Tem_Fact T1 WHERE T1.ArtAgte IS NOT NULL) "

    '--'--CONSULTA CON INSERT SE AGREGAN LAS NOTAS DE CREDITO
    SQLTPD &= "INSERT #Tem_Fact (ItemCode, CANTIDAD, IdAgente,IdClte,TotalSinIva,TipoMov,ArtAgte,CANTNETA) "
    SQLTPD &= "SELECT ItemCode, 0, IdAgente,IdClte,0,TipoMov,ArtAgte,CANTNETA "
    SQLTPD &= "FROM #Tem_DesPP AS T0 WHERE T0.ArtAgte NOT IN (SELECT T1.ArtAgte FROM #Tem_Fact T1 WHERE T1.ArtAgte IS NOT NULL) "

    '--'--CONSULTA CON INSERT SE BUSCAN LOS ARTICULOS CANCELADOS QUE NO HAN SIDO FACTURADOS EN ESE DIA POR ESE AGENTE
    SQLTPD &= "INSERT #Tem_Fact (ItemCode, CANTIDAD, IdAgente,IdClte,TotalSinIva,TipoMov,ArtAgte,CANTNETA) "
    SQLTPD &= "SELECT ItemCode, 0, IdAgente,IdClte,0,TipoMov,ArtAgte,CANTNETA "
    SQLTPD &= "FROM #Tem_Dev AS T0 WHERE T0.ArtAgte NOT IN (SELECT T1.ArtAgte FROM #Tem_Fact T1 WHERE T1.ArtAgte IS NOT NULL) "


    '--DETALLE DE VENTAS CON INFORMACIÓN DE AGENTE,CLIENTE,LINEA,ARTÍCULO
    SQLTPD &= "SELECT T0.ItemCode AS Articulo, "
    SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 'NOTAS DE CARGO' ELSE T3.ItemName END AS ItemName, "
    SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 998 ELSE T3.ItmsGrpCod END AS ItmsGrpCod, "
    SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T4.ItmsGrpNam END AS ItmsGrpNam, "
    SQLTPD &= "T0.TipoMov,T0.IdAgente,T6.SlpName  AS NomAgte,T0.IdClte,T7.CardName AS NomClte, "
    'SQLTPD &= "CASE WHEN T7.U_VLGX_PLC IS NULL THEN '' ELSE T7.U_VLGX_PLC END AS 'Ruta', "
    SQLTPD &= "CASE WHEN T7.U_BXP_Ruta IS NULL THEN '' ELSE T7.U_BXP_Ruta END AS 'Ruta', "
    SQLTPD &= "T0.ArtAgte,CASE WHEN T1.CANTIDAD IS NULL THEN T0.CANTIDAD ELSE T0.CANTIDAD - T1.CANTIDAD END CANT_TOT, "
    SQLTPD &= "CASE WHEN T1.TotalSinIva IS NULL THEN T0.TotalSinIva ELSE T0.TotalSinIva - T1.TotalSinIva END MONT_TOT, "
    SQLTPD &= "CASE WHEN T2.CANTIDAD IS NULL THEN 0 ELSE T2.CANTIDAD END AS CantDev, "
    SQLTPD &= "CASE WHEN T1.CANTIDAD IS NULL THEN T0.CANTIDAD ELSE T0.CANTIDAD - T1.CANTIDAD END  - "
    SQLTPD &= "CASE WHEN T2.CANTIDAD IS NULL THEN 0 ELSE T2.CANTIDAD END AS CANT_NETA, "
    SQLTPD &= "CASE WHEN T2.TotalSinIva IS NULL THEN 0 ELSE T2.TotalSinIva END AS MontoDev,"
    SQLTPD &= "T0.TotalSinIva - "
    SQLTPD &= "(CASE WHEN T1.TotalSinIva IS NULL THEN 0 ELSE T1.TotalSinIva END + "
    SQLTPD &= "CASE WHEN T2.TotalSinIva IS NULL THEN 0 ELSE T2.TotalSinIva END) "
    SQLTPD &= "AS  MONTO_NETA,"

    SQLTPD &= "CASE WHEN T5.CANTIDAD IS NULL THEN 0 ELSE T5.CANTIDAD END AS CantPP, "
    SQLTPD &= "CASE WHEN T5.TotalSinIva IS NULL THEN 0 ELSE T5.TotalSinIva END AS MontoPP, "

    SQLTPD &= "CASE WHEN T2.TotalSinIva IS NULL THEN 0 ELSE  T2.TotalSinIva END + "
    SQLTPD &= "CASE WHEN T5.TotalSinIva IS NULL THEN 0 ELSE  T5.TotalSinIva END "
    SQLTPD &= "AS MontoNC "

    SQLTPD &= "INTO #T_DET_VTA "
    SQLTPD &= "FROM #Tem_Fact T0 "
    SQLTPD &= "LEFT JOIN #Tem_Canc T1 ON T0.ArtAgte = T1.ArtAgte "
    SQLTPD &= "AND T0.IdAgente = T1.IdAgente  "
    SQLTPD &= "AND T0.IdClte = T1.IdClte  "
    SQLTPD &= "LEFT JOIN #Tem_Dev T2 ON T0.ArtAgte = T2.ArtAgte "
    SQLTPD &= "AND T0.IdAgente = T2.IdAgente  "
    SQLTPD &= "AND T0.IdClte = T2.IdClte  "
    SQLTPD &= "LEFT JOIN #Tem_DesPP T5 ON T0.ArtAgte = T5.ArtAgte "
    SQLTPD &= "AND T0.IdAgente = T5.IdAgente  "
    SQLTPD &= "AND T0.IdClte = T5.IdClte  "
    SQLTPD &= "LEFT JOIN OITM T3 ON T0.ItemCode = T3.ItemCode "
    SQLTPD &= "LEFT JOIN OITB T4 ON T3.ItmsGrpCod = T4.ItmsGrpCod "
    SQLTPD &= "LEFT JOIN OSLP T6 ON T6.SlpCode = T0.IdAgente  "
    SQLTPD &= "LEFT JOIN OCRD T7 ON T7.CardCode = T0.IdClte "

    SQLTPD &= "DROP TABLE #Tem_Canc "
    SQLTPD &= "DROP TABLE #Tem_DesPP "
    SQLTPD &= "DROP TABLE #Tem_Dev "
    SQLTPD &= "DROP TABLE #Tem_Fact "


    '--'--VENTAS POR AGENTE
    SQLTPD &= "SELECT IdAgente,NomAgte,PzsVta,MontoVta,CantDev,MontoDev, "
    SQLTPD &= "PzsNeta, MontoNeto, CantPP, MontoPP, MontoNC, Llave "
    SQLTPD &= "INTO #T_VTA_AGTES "
    SQLTPD &= "FROM(	"
    SQLTPD &= "SELECT T0.IdAgente,T0.NomAgte,"
    SQLTPD &= "SUM(T0.CANT_TOT) AS PzsVta,SUM(T0.MONT_TOT) AS MontoVta, "
    SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev, "
    SQLTPD &= "SUM(T0.CANT_NETA) AS PzsNeta,SUM(T0.MONTO_NETA) AS MontoNeto, "
    SQLTPD &= "SUM(T0.CantPP) AS CantPP,SUM(T0.MontoPP) AS MontoPP, "
    SQLTPD &= "SUM(T0.MontoNC) AS MontoNC, "
    SQLTPD &= "1 AS Llave "

    SQLTPD &= "FROM #T_DET_VTA T0 "
    SQLTPD &= "GROUP BY IdAgente,NomAgte "
    SQLTPD &= ")TMP "
    SQLTPD &= "ORDER BY NomAgte,MontoNeto DESC  "


    '--'--MONTO TOTAL DE VENTAS 
    SQLTPD &= "SELECT "
    SQLTPD &= "SUM(T0.PzsVta) AS PzsVta,SUM(T0.MontoVta) AS MontoVta, "
    SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev, "
    SQLTPD &= "SUM(T0.PzsNeta) AS PzsNeta,SUM(T0.MontoNeto) AS MontoNeto, "
    SQLTPD &= "SUM(T0.CantPP) AS CantPP,SUM(T0.MontoPP) AS MontoPP, "
    SQLTPD &= "SUM(T0.MontoNC) AS MontoNC, "
    SQLTPD &= "1 AS Llave "
    SQLTPD &= "INTO #T_VTA_TOTAL "
    SQLTPD &= "FROM #T_VTA_AGTES T0 "

    '*********------
    If UsrTPM = "VVERGARA" Then
      SQLTPD &= "WHERE IdAgente in (select SlpCode from OSLP where Memo = 07 ) "
    ElseIf UsrTPM = "RROBLES" Then
      SQLTPD &= "WHERE IdAgente in (select SlpCode from OSLP where Memo = 03 ) "
    End If
    '*********------

    '--VENTAS TOTALES AGENTES,CLIENTES 
    SQLTPD &= "SELECT T0.IdAgente,T0.IdClte, "
    'SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.IdAgente AS VARCHAR(2)))) + RTRIM(LTRIM(T0.IdClte)) AS VARCHAR(17)) AS ArtClte,T1.U_VLGX_PLC, "
    SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.IdAgente AS VARCHAR(2)))) + RTRIM(LTRIM(T0.IdClte)) AS VARCHAR(17)) AS ArtClte,T1.U_BXP_Ruta, "
    SQLTPD &= "SUM(T0.CANT_TOT) AS PzsVta,SUM(T0.MONT_TOT) AS MontoVta, "
    SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev, "
    SQLTPD &= "SUM(T0.CANT_NETA) AS PzsNeta,SUM(T0.MONTO_NETA) AS MontoNeto, "
    SQLTPD &= "SUM(T0.CantPP) AS CantPP,SUM(T0.MontoPP) AS MontoPP, "
    SQLTPD &= "SUM(T0.MontoNC) AS MontoNC "
    SQLTPD &= "INTO #T_AGTE_CLTE "
    SQLTPD &= "FROM #T_DET_VTA T0 "
    SQLTPD &= "LEFT JOIN SBO_TPD.dbo.OCRD T1 ON T0.IdClte = T1.CardCode "
    ' SQLTPD &= "GROUP BY T0.IdAgente,T0.IdClte,T1.U_VLGX_PLC ORDER BY MontoNeto DESC "
    SQLTPD &= "GROUP BY T0.IdAgente,T0.IdClte,T1.U_BXP_Ruta ORDER BY MontoNeto DESC "



    '--VENTAS TOTALES AGENTES,CLIENTES,LINEAS
    SQLTPD &= "SELECT T0.IdAgente,T0.NomAgte,T0.IdClte,T0.NomClte,T0.ItmsGrpCod,T0.ItmsGrpNam, "
    SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.IdAgente AS VARCHAR(2)))) + RTRIM(LTRIM(T0.IdClte)) AS VARCHAR(17)) AS ArtClte, "
    SQLTPD &= "SUM(T0.CANT_TOT) AS PzsVta,SUM(T0.MONT_TOT) AS MontoVta, "
    SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev, "
    SQLTPD &= "SUM(T0.CANT_NETA) AS PzsNeta,SUM(T0.MONTO_NETA) AS MontoNeto, "
    SQLTPD &= "SUM(T0.CantPP) AS CantPP,SUM(T0.MontoPP) AS MontoPP, "
    SQLTPD &= "SUM(T0.MontoNC) AS MontoNC "
    SQLTPD &= "INTO #T_A_C_LINEA "
    SQLTPD &= "FROM #T_DET_VTA T0 GROUP BY IdAgente,T0.NomAgte,IdClte,T0.NomClte,ItmsGrpCod,ItmsGrpNam "
    SQLTPD &= "ORDER BY MontoNeto DESC "



    '--VENTAS TOTALES DE CLIENTES CON DETALLE DE DEVOLUCIONES,DESCUENTOS PP ETC./Comprobado
    SQLTPD &= "SELECT  T0.IdAgente,T0.IdClte AS Cliente,"
    SQLTPD &= "T2.CardName AS Nombre,"
    'SQLTPD &= "T2.City AS Ciudad,T2.State1 AS Estado,T0.U_VLGX_PLC,"
    SQLTPD &= "T2.City AS Ciudad,T2.State1 AS Estado,T0.U_BXP_Ruta,"
    SQLTPD &= "T0.MontoVta,T0.PzsVta,"
    SQLTPD &= "CASE WHEN T0.MontoVta = 0 OR  T1.MontoVta = 0 THEN 0 ELSE "
    SQLTPD &= "T0.MontoVta * 100 / T1.MontoVta END AS PorVta,"
    SQLTPD &= "T0.MontoDev,T0.CantDev,"
    SQLTPD &= "CASE WHEN T0.MontoDev = 0 OR  T0.MontoVta = 0 THEN 0 ELSE "
    SQLTPD &= "T0.MontoDev * 100 / T0.MontoVta END AS PorDEV,"
    SQLTPD &= "T0.MontoNeto,T0.PzsNeta,T0.MontoPP,"
    SQLTPD &= "CASE WHEN T0.MontoPP = 0 OR  T0.MontoNeto = 0 THEN 0 ELSE "
    SQLTPD &= "T0.MontoPP * 100 / T0.MontoNeto END AS PorPP,"
    SQLTPD &= "T0.MontoNC, ArtClte "
    SQLTPD &= "INTO #T_DET_CLTES	"
    SQLTPD &= "FROM #T_AGTE_CLTE T0 "
    SQLTPD &= "LEFT JOIN #T_VTA_AGTES T1 ON T0.IdAgente = T1.IdAgente "
    SQLTPD &= "LEFT JOIN OCRD T2 ON T2.CardCode = T0.IdClte "
    SQLTPD &= "UNION ALL "
    SQLTPD &= "SELECT T3.SlpCode AS IdAgente,T3.CardCode AS Cliente, "
    SQLTPD &= "T3.CardName AS Nombre,"
    'SQLTPD &= "T3.City AS Ciudad,T3.State1 AS Estado,T3.U_VLGX_PLC,"
    SQLTPD &= "T3.City AS Ciudad,T3.State1 AS Estado,T3.U_BXP_Ruta,"
    SQLTPD &= "0.0 AS MontoVta,0.0 AS PzsVta,0.0 AS PorVta,"
    SQLTPD &= "0.0 AS MontoDev,0.0 AS CantDev,0.0 AS PorDEV,"
    SQLTPD &= "0.0 AS MontoNeto,0.0 AS PzsNeta,0.0 AS MontoPP,"
    SQLTPD &= "0.0 AS PorPP,0.0 AS MontoNC,CAST(RTRIM(LTRIM(CAST(T3.SlpCode AS VARCHAR(2)))) + RTRIM(LTRIM(T3.CardCode)) AS VARCHAR(17)) AS ArtClte "
    SQLTPD &= "FROM OCRD T3 WHERE T3.CardType = 'C' AND T3.frozenFor <> 'Y' AND "
    SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T3.SlpCode AS VARCHAR(2)))) + RTRIM(LTRIM(T3.CardCode)) AS VARCHAR(17)) NOT IN "
    SQLTPD &= "(SELECT T4.ArtClte FROM #T_AGTE_CLTE T4) "
    '---' ICR_24022015 se agrego cambio para los clientes propios
    If (CkCteProp.Checked = False) Then
      SQLTPD &= " and T3.slpCode <> 1"
    End If
    SQLTPD &= " order by T0.MontoNeto "


    If (DtpFechaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
      If (DtpFechaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
        SQLTPD &= "SELECT T0.Cliente,SUM((T1.DocTotal - T1.VatSum) - T1.TotalExpns) * -1 AS Total,MONTH(T1.DocDate) AS Mes,YEAR(T1.DocDate) AS Anio "
        SQLTPD &= "into #RR "
        SQLTPD &= "FROM #T_DET_CLTES T0 "
        SQLTPD &= "INNER JOIN ORIN T1 ON "
        SQLTPD &= "T0.Cliente = T1.CardCode And T1.DocDate <= GETDATE() "
        SQLTPD &= " AND (T1.EDocPrefix = 'NC' or T1.EDocPrefix is null or T1.EDocPrefix = 'F') AND T1.DocType  = 'I' "
        SQLTPD &= "GROUP BY Cliente,MONTH(T1.DocDate),YEAR(T1.DocDate) "
      ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
        SQLTPD &= "select distinct T1.DocNum, t1.DocDate, t1.DocEntry, t1.Doctype  "
        SQLTPD &= "into #ORIN_TPM from #T_DET_CLTES T0 "
        SQLTPD &= "INNER JOIN ORIN T1 ON "
        SQLTPD &= "T0.Cliente = T1.CardCode And T1.DocDate <= GETDATE() "
        SQLTPD &= "AND (T1.EDocPrefix = 'NC' or T1.EDocPrefix is null or T1.EDocPrefix = 'F') "

        SQLTPD &= "select distinct t0.DocNum into #th from #ORIN_TPM t0 where t0.DocDate <= '2017-12-31' and t0.Doctype = 'I' "
        SQLTPD &= "union all "
        SQLTPD &= "select distinct t0.DocNum from #ORIN_TPM t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
        SQLTPD &= "where t1.ItemCode <> 'DESCUENTO P.P' and  t0.DocDate >= '2018-01-01' "
        SQLTPD &= "select t1.* into #ORIN2 from #th t0 inner join ORIN t1 on t0.DocNum = t1.DocNum "

        SQLTPD &= "SELECT T0.Cliente,SUM((T1.DocTotal - T1.VatSum) - T1.TotalExpns) * -1 AS Total,MONTH(T1.DocDate) AS Mes,YEAR(T1.DocDate) AS Anio "
        SQLTPD &= "into #RR "
        SQLTPD &= "FROM #T_DET_CLTES T0 "
        SQLTPD &= "INNER JOIN #ORIN2 T1 ON "
        SQLTPD &= "T0.Cliente = T1.CardCode And T1.DocDate <= GETDATE() "
        SQLTPD &= " AND (T1.EDocPrefix = 'NC' or T1.EDocPrefix is null or T1.EDocPrefix = 'F')  "
        SQLTPD &= "GROUP BY Cliente,MONTH(T1.DocDate),YEAR(T1.DocDate) "

        SQLTPD &= "drop table #ORIN_TPM "
        SQLTPD &= "drop table #th "
        SQLTPD &= "drop table #ORIN2 "


      End If
    ElseIf (DtpFechaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
      If (DtpFechaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then

      ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then '
        SQLTPD &= "select distinct T1.DocNum, t1.DocDate, t1.DocEntry, t1.Doctype  "
        SQLTPD &= "into #ORIN_TPM2 from #T_DET_CLTES T0 "
        SQLTPD &= "INNER JOIN ORIN T1 ON "
        SQLTPD &= "T0.Cliente = T1.CardCode And T1.DocDate <= GETDATE() "
        SQLTPD &= "AND (T1.EDocPrefix = 'NC' or T1.EDocPrefix is null or T1.EDocPrefix = 'F') "

        SQLTPD &= "select distinct t0.DocNum into #th2 from #ORIN_TPM2 t0 where t0.DocDate <= '2017-12-31' and t0.Doctype = 'I' "
        SQLTPD &= "union all "
        SQLTPD &= "select distinct t0.DocNum from #ORIN_TPM2 t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
        SQLTPD &= "where t1.ItemCode <> 'DESCUENTO P.P' and  t0.DocDate >= '2018-01-01' "
        SQLTPD &= "select t1.* into #ORIN22 from #th2 t0 inner join ORIN t1 on t0.DocNum = t1.DocNum "

        SQLTPD &= "SELECT T0.Cliente,SUM((T1.DocTotal - T1.VatSum) - T1.TotalExpns) * -1 AS Total,MONTH(T1.DocDate) AS Mes,YEAR(T1.DocDate) AS Anio "
        SQLTPD &= "into #RR "
        SQLTPD &= "FROM #T_DET_CLTES T0 "
        SQLTPD &= "INNER JOIN #ORIN22 T1 ON "
        SQLTPD &= "T0.Cliente = T1.CardCode And T1.DocDate <= GETDATE() "
        SQLTPD &= " AND (T1.EDocPrefix = 'NC' or T1.EDocPrefix is null or T1.EDocPrefix = 'F')  "
        SQLTPD &= "GROUP BY Cliente,MONTH(T1.DocDate),YEAR(T1.DocDate) "

        SQLTPD &= "drop table #ORIN_TPM2 "
        SQLTPD &= "drop table #th2 "
        SQLTPD &= "drop table #ORIN22 "
      End If
    End If


    '--SE CONSULTA LA VENTA DE TODOS LOS MESES DE TODOS LOS CLIENTES
    SQLTPD &= "SELECT T0.Cliente,SUM((T1.DocTotal - T1.VatSum) - T1.TotalExpns) AS Total,MONTH(T1.DocDate) AS Mes,YEAR(T1.DocDate) AS Anio "
    SQLTPD &= "INTO #T_VTA_TOD_MES "
    SQLTPD &= "FROM #T_DET_CLTES T0 INNER JOIN OINV T1 ON "
    SQLTPD &= "T0.Cliente = T1.CardCode And T1.DocDate <= GETDATE() "
    SQLTPD &= "GROUP BY Cliente,MONTH(T1.DocDate),YEAR(T1.DocDate) "

    SQLTPD &= "UNION ALL "

    SQLTPD &= "select * from #RR "
    SQLTPD &= "drop table #RR "

    'SQLTPD &= "SELECT T0.Cliente,SUM((T1.DocTotal - T1.VatSum) - T1.TotalExpns) * -1 AS Total,MONTH(T1.DocDate) AS Mes,YEAR(T1.DocDate) AS Anio "
    'SQLTPD &= "FROM #T_DET_CLTES T0 "
    'SQLTPD &= "INNER JOIN ORIN T1 ON "
    'SQLTPD &= "T0.Cliente = T1.CardCode And T1.DocDate <= GETDATE() "
    'SQLTPD &= " AND (T1.EDocPrefix = 'NC' or T1.EDocPrefix is null or T1.EDocPrefix = 'F') AND T1.DocType  = 'I' "
    'SQLTPD &= "GROUP BY Cliente,MONTH(T1.DocDate),YEAR(T1.DocDate) "


    '--VENTA MAS ALTA POR CLIENTE
    SQLTPD &= "SELECT Cliente,SUM(Total) AS VtaMasAlta,Mes,Anio,ROW_NUMBER() OVER (PARTITION BY Cliente ORDER BY Cliente,SUM(Total) DESC) as OrdenVta, "
    SQLTPD &= "CASE WHEN MES = 1 THEN 'Enero' "
    SQLTPD &= "WHEN MES = 2 THEN 'Febrero' "
    SQLTPD &= "WHEN MES = 3 THEN 'Marzo' "
    SQLTPD &= "WHEN MES = 4 THEN 'Abril' "
    SQLTPD &= "WHEN MES = 5 THEN 'Mayo' "
    SQLTPD &= "WHEN MES = 6 THEN 'Junio' "
    SQLTPD &= "WHEN MES = 7 THEN 'Julio' "
    SQLTPD &= "WHEN MES = 8 THEN 'Agosto' "
    SQLTPD &= "WHEN MES = 9 THEN 'Septiembre' "
    SQLTPD &= "WHEN MES = 10 THEN 'Octubre' "
    SQLTPD &= "WHEN MES = 11 THEN 'Noviembre' "
    SQLTPD &= "WHEN MES = 12 THEN 'Diciembre' "
    SQLTPD &= "ELSE ' ' "
    SQLTPD &= "End "
    SQLTPD &= "AS MesLetra "
    SQLTPD &= "INTO #VTA_MAYOR_C "
    SQLTPD &= "FROM #T_VTA_TOD_MES GROUP BY Cliente,Mes,Anio ORDER BY Cliente,SUM(Total) DESC "

    SQLTPD &= "DROP TABLE #T_VTA_TOD_MES "

    '--ÚLTIMA VENTA, FACTURA MÁS ALTA
    SQLTPD &= "SELECT T3.CardCode AS Cliente, MAX(T4.DocDate) AS Ult_Vta,MAX(t4.DocTotal) AS Fac_MasAlta, "
    SQLTPD &= "DATEDIFF(DAY, MAX(T4.DocDate), GETDATE()) AS DiasTrans "
    SQLTPD &= "INTO #ULT_VTA_CLTE "
    SQLTPD &= "FROM OCRD T3 LEFT JOIN OINV T4 ON T4.CardCode = T3.CardCode AND T4.DocDate <= GETDATE() WHERE T3.CardType = 'C'  AND T3.frozenFor <> 'Y' AND "
    SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T3.SlpCode AS VARCHAR(2)))) + RTRIM(LTRIM(T3.CardCode)) AS VARCHAR(17)) IN "
    SQLTPD &= "(SELECT T5.ArtClte FROM #T_DET_CLTES T5) "
    '---' ICR_24022015 se agrego cambio para los clientes propios
    If (CkCteProp.Checked = False) Then
      SQLTPD &= " and T3.slpCode <>  1 "
    End If
    SQLTPD &= "GROUP BY T3.CardCode "




    SQLTPD &= "CREATE TABLE #TempAgteRuta(IdAgente INT,NomAgte VARCHAR(155),Ruta VARCHAR(50), "
    SQLTPD &= "MontoVta DECIMAL(20,4),PorVta DECIMAL (20,4),PzsVta DECIMAL, "
    SQLTPD &= "MontoDev DECIMAL(20,4),PorDEV DECIMAL (20,4),CantDev DECIMAL, "
    SQLTPD &= "MontoNeto DECIMAL(20,4),PzsNeta DECIMAL, MontoPP DECIMAL(20,4), "
    SQLTPD &= "PorPP DECIMAL(20,4),MontoNC DECIMAL(20,4),IDAgte INT,NomAgte2 VARCHAR(100),Orden INT ) "

    SQLTPD &= "CREATE TABLE #TempAgteRuta2(IdAgente INT,NomAgte VARCHAR(155),Ruta VARCHAR(50),"
    SQLTPD &= "MontoVta DECIMAL(20,4),PorVta DECIMAL (20,4),PzsVta DECIMAL, "
    SQLTPD &= "MontoDev DECIMAL(20,4),PorDEV DECIMAL (20,4),CantDev DECIMAL, "
    SQLTPD &= "MontoNeto DECIMAL(20,4),PzsNeta DECIMAL, MontoPP DECIMAL(20,4), "
    SQLTPD &= "PorPP DECIMAL(20,4),MontoNC DECIMAL(20,4),IDAgte INT,NomAgte2 VARCHAR(100),Orden INT ) "

    SQLTPD &= "DECLARE @VtaTotal DECIMAL(19,4)=(SELECT MontoVta FROM #T_VTA_TOTAL) "
    SQLTPD &= "DECLARE @DevTotal DECIMAL(19,4)=(SELECT MontoDev FROM #T_VTA_TOTAL) "

    SQLTPD &= "INSERT INTO #TempAgteRuta(IdAgente,NomAgte,Ruta,MontoVta,PorVta,PzsVta,MontoDev,PorDEV,"
    SQLTPD &= "CantDev,MontoNeto,PzsNeta,MontoPP,PorPP,MontoNC,IDAgte,NomAgte2,Orden) "
    SQLTPD &= "SELECT T0.IdAgente,T3.SlpName  AS NomAgte,"
    'SQLTPD &= "CASE WHEN T4.U_VLGX_PLC IS NULL THEN '' ELSE T4.U_VLGX_PLC END AS Ruta,"
    SQLTPD &= "CASE WHEN T4.U_BXP_Ruta IS NULL THEN '' ELSE T4.U_BXP_Ruta END AS Ruta,"
    SQLTPD &= "SUM(T0.MontoVta),"
    SQLTPD &= "CASE WHEN SUM(T0.MontoVta) = 0 OR @VtaTotal = 0 THEN 0 "
    SQLTPD &= "ELSE SUM(T0.MontoVta)/@VtaTotal*100 END AS PorVta,"
    SQLTPD &= "SUM(T0.PzsVta),"
    SQLTPD &= "SUM(T0.MontoDev),"
    SQLTPD &= "CASE WHEN SUM(T0.MontoDev)=0 OR @DevTotal=0 THEN 0 "
    SQLTPD &= "ELSE SUM(T0.MontoDev)/@DevTotal*100 END AS PorDev, "
    SQLTPD &= "SUM(T0.CantDev),"
    SQLTPD &= "SUM(T0.MontoNeto), SUM(T0.PzsNeta),SUM(T0.MontoPP), SUM(T0.PorPP), SUM(T0.MontoNC),T0.IdAgente,T3.SlpName,0 "
    SQLTPD &= "FROM #T_DET_CLTES T0 "
    SQLTPD &= "LEFT JOIN #VTA_MAYOR_C T1 ON T0.Cliente = T1.Cliente AND T1.OrdenVta = 1 "
    SQLTPD &= "LEFT JOIN #ULT_VTA_CLTE T2 ON T0.Cliente = T2.Cliente "
    SQLTPD &= "LEFT JOIN OSLP T3 ON T3.SlpCode = T0.IdAgente "
    SQLTPD &= "LEFT JOIN OCRD T4 ON T0.Cliente=T4.CardCode "
    SQLTPD &= "WHERE T0.IdAgente NOT IN (SELECT SlpCode FROM TPM.DBO.AgteRuta) " 'CR'
    'SQLTPD &= "GROUP BY T0.IdAgente,T3.SlpName,T4.U_VLGX_PLC "
    SQLTPD &= "GROUP BY T0.IdAgente,T3.SlpName,T4.U_BXP_Ruta "


    SQLTPD &= "INSERT INTO #TempAgteRuta(IdAgente,NomAgte,Ruta,MontoVta,PorVta,PzsVta,MontoDev,PorDEV, "
    SQLTPD &= "CantDev,MontoNeto,PzsNeta,MontoPP,PorPP,MontoNC,IDAgte,NomAgte2,Orden) "
    SQLTPD &= "SELECT 0,'MONTOS TOTALES' AS NomAgte,'',MontoVta,"
    SQLTPD &= "100.00 AS PorVta,PzsVta,"
    SQLTPD &= "MontoDev,0.00 AS PorDEV,CantDev,"
    SQLTPD &= "MontoNeto,PzsNeta,MontoPP,0 AS PorPP,MontoNC,99,'zTOTAL',99 "
    SQLTPD &= "FROM #T_VTA_TOTAL "

    SQLTPD &= "INSERT INTO #TempAgteRuta2(IdAgente,NomAgte,Ruta,MontoVta,PorVta,PzsVta,MontoDev,PorDEV, "
    SQLTPD &= "CantDev,MontoNeto,PzsNeta,MontoPP,PorPP,MontoNC,IDAgte,NomAgte2,Orden) "
    SQLTPD &= "SELECT T0.IdAgente,T3.SlpName  AS NomAgte, "
    'SQLTPD &= "CASE WHEN T4.U_VLGX_PLC IS NULL THEN '' ELSE T4.U_VLGX_PLC END AS Ruta, "
    SQLTPD &= "CASE WHEN T4.U_BXP_Ruta IS NULL THEN '' ELSE T4.U_BXP_Ruta END AS Ruta, "
    SQLTPD &= "SUM(T0.MontoVta),SUM(T0.PorVta),SUM(T0.PzsVta),SUM(T0.MontoDev),SUM(T0.PorDEV), SUM(T0.CantDev), "
    SQLTPD &= "SUM(T0.MontoNeto), SUM(T0.PzsNeta),SUM(T0.MontoPP), SUM(T0.PorPP), SUM(T0.MontoNC),T0.IdAgente,T3.SlpName,0 "
    SQLTPD &= "FROM #T_DET_CLTES T0 "
    SQLTPD &= "LEFT JOIN #VTA_MAYOR_C T1 ON T0.Cliente = T1.Cliente AND T1.OrdenVta = 1 "
    SQLTPD &= "LEFT JOIN #ULT_VTA_CLTE T2 ON T0.Cliente = T2.Cliente "
    SQLTPD &= "LEFT JOIN OSLP T3 ON T3.SlpCode = T0.IdAgente "
    SQLTPD &= "LEFT JOIN OCRD T4 ON T0.Cliente=T4.CardCode "
    SQLTPD &= "WHERE T0.IdAgente NOT IN (SELECT IDAgte FROM #TempAgteRuta) "
    'SQLTPD &= "GROUP BY T0.IdAgente,T3.SlpName,T4.U_VLGX_PLC "
    SQLTPD &= "GROUP BY T0.IdAgente,T3.SlpName,T4.U_BXP_Ruta "


    SQLTPD &= "CREATE TABLE #TempAgteRutaTot(IdAgente INT,NomAgte VARCHAR(155),Ruta VARCHAR(50), "
    SQLTPD &= "MontoVta DECIMAL(20,4),PorVta DECIMAL (20,4),PzsVta DECIMAL, "
    SQLTPD &= "MontoDev DECIMAL(20,4),PorDEV DECIMAL (20,4),CantDev DECIMAL,"
    SQLTPD &= "MontoNeto DECIMAL(20,4),PzsNeta DECIMAL, MontoPP DECIMAL(20,4),"
    SQLTPD &= "PorPP DECIMAL(20,4),MontoNC DECIMAL(20,4),IDAgte INT,NomAgte2 VARCHAR(100),Orden INT ) "


    SQLTPD &= "INSERT INTO #TempAgteRutaTot(IdAgente,NomAgte,Ruta,MontoVta,PorVta,PzsVta,MontoDev,PorDEV,"
    SQLTPD &= "CantDev,MontoNeto,PzsNeta,MontoPP,PorPP,MontoNC,IDAgte,NomAgte2,Orden) "
    SQLTPD &= "SELECT IdAgente,'***Totales ' + T0.NomAgte '***','', "
    SQLTPD &= "SUM(T0.MontoVta),"
    SQLTPD &= "CASE WHEN SUM(T0.MontoVta) = 0 OR @VtaTotal = 0 THEN 0 "
    SQLTPD &= "ELSE SUM(T0.MontoVta)/@VtaTotal*100 END AS PorVta,"
    SQLTPD &= "sum(T0.PzsVta),"
    SQLTPD &= "sum(T0.MontoDev),"
    SQLTPD &= "CASE WHEN SUM(T0.MontoDev) = 0  OR SUM(T0.MontoVta) = 0 THEN 0 "
    SQLTPD &= "ELSE SUM(T0.MontoDev)/SUM(T0.MontoVta)*100 END AS PorDev,"
    SQLTPD &= "SUM(T0.CantDev),"
    SQLTPD &= "SUM(T0.MontoNeto),SUM(T0.PzsNeta),SUM(T0.MontoPP),"
    SQLTPD &= "CASE WHEN SUM(T0.MontoPP)=0 OR SUM(T0.MontoVta)=0 THEN 0 "
    SQLTPD &= "ELSE SUM(T0.MontoPP)/SUM(T0.MontoVta)*100 END AS PorPP,"
    SQLTPD &= "SUM(T0.MontoNC),t0.IdAgente,t0.NomAgte,2 AS 'Orden' "
    SQLTPD &= "FROM #TempAgteRuta2 T0 "
    SQLTPD &= "GROUP BY T0.IdAgente,t0.NomAgte "
    SQLTPD &= "ORDER BY t0.NomAgte "

    SQLTPD &= "SELECT * FROM ( "

    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
      SQLTPD &= "SELECT * FROM #TempAgteRuta WHERE IdAgente = " & vCodAgte

    ElseIf UsrTPM = "VVERGARA" Then
      SQLTPD &= "SELECT * FROM #TempAgteRuta WHERE IdAgente in (select SlpCode from OSLP where Memo = '07' ) OR IdAgente = 0 "

    ElseIf UsrTPM = "RROBLES" Then
      SQLTPD &= "SELECT * FROM #TempAgteRuta WHERE IdAgente in (select SlpCode from OSLP where Memo = '03' ) OR IdAgente = 0 "

    Else
      SQLTPD &= "SELECT * FROM #TempAgteRuta "

    End If

    SQLTPD &= "UNION ALL "

    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
      SQLTPD &= "SELECT * FROM #TempAgteRutaTot WHERE IdAgente = " & vCodAgte

    ElseIf UsrTPM = "VVERGARA" Then
      SQLTPD &= "SELECT * FROM #TempAgteRutaTot WHERE IdAgente in (select SlpCode from OSLP where Memo = '07') "

    ElseIf UsrTPM = "RROBLES" Then
      SQLTPD &= "SELECT * FROM #TempAgteRutaTot WHERE IdAgente in (select SlpCode from OSLP where Memo = '03' ) "
    Else
      SQLTPD &= "SELECT * FROM #TempAgteRutaTot "
    End If

    SQLTPD &= " UNION ALL "

    If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
      SQLTPD &= "SELECT * FROM #TempAgteRuta2 WHERE IdAgente = " & vCodAgte

    ElseIf UsrTPM = "VVERGARA" Then
      SQLTPD &= "SELECT * FROM #TempAgteRuta2 WHERE IdAgente in (select SlpCode from OSLP where Memo = '07') "

    ElseIf UsrTPM = "RROBLES" Then
      SQLTPD &= "SELECT * FROM #TempAgteRuta2 WHERE IdAgente in (select SlpCode from OSLP where Memo = '03') "
    Else
      SQLTPD &= "SELECT * FROM #TempAgteRuta2 "
    End If

    SQLTPD &= ") TMP ORDER BY NOMAGTE2,Orden "

    'SQLTPD &= "SELECT * FROM ( "
    'SQLTPD &= "SELECT * FROM #TempAgteRuta "
    'SQLTPD &= "UNION ALL "
    'SQLTPD &= "SELECT * FROM #TempAgteRutaTot "
    'SQLTPD &= "UNION ALL "
    'SQLTPD &= "SELECT * FROM #TempAgteRuta2 "
    'SQLTPD &= ") TMP ORDER BY NOMAGTE2,Orden "

    '--'*****  2
    '--REPORTE DE VENTAS POR CLIENTES - AGENTE CON ULTIMA VENTA Y VENTA MAS ALTA A LA FECHA
    '--**COMPROBADO**
    SQLTPD &= "SELECT T0.IdAgente, T0.Cliente, T0. Nombre , T0. Ciudad , T0.Estado, "
    'SQLTPD &= "CASE WHEN T4.U_VLGX_PLC IS NULL THEN '' ELSE T4.U_VLGX_PLC END AS Ruta, "
    SQLTPD &= "CASE WHEN T4.U_BXP_Ruta IS NULL THEN '' ELSE T4.U_BXP_Ruta END AS Ruta, "
    SQLTPD &= "T0.MontoVta,  T0.PorVta,T0.PzsVta,T1.VtaMasAlta, "
    SQLTPD &= "T1.MesLetra, T1.Anio, T2.Ult_Vta, T2.DiasTrans, T2.Fac_MasAlta, T0.MontoDev, T0.PorDEV, T0.CantDev, "
    SQLTPD &= "T0.MontoNeto, T0.PzsNeta, T0.MontoPP, T0.PorPP, T0.MontoNC,T3.SlpName  AS NomAgte "
    SQLTPD &= "FROM #T_DET_CLTES T0 "
    SQLTPD &= "LEFT JOIN #VTA_MAYOR_C T1 ON T0.Cliente = T1.Cliente AND T1.OrdenVta = 1 "
    SQLTPD &= "LEFT JOIN #ULT_VTA_CLTE T2 ON T0.Cliente = T2.Cliente "
    SQLTPD &= "LEFT JOIN OSLP T3 ON T3.SlpCode = T0.IdAgente "
    SQLTPD &= "LEFT JOIN OCRD T4 ON T0.Cliente=T4.CardCode WHERE T0.IdAgente<>1000 AND T0.Cliente<>'C-1532' "
    If (CkCteProp.Checked = False) Then
      SQLTPD &= "AND T0.IdAgente <> 1 "
    End If

    SQLTPD &= " ORDER BY T0.IdAgente,T0.MontoNeto DESC "

    '****   4
    '--VENTAS TOTALES POR AGENTES CLIENTES LINEAS.
    '--*COMPROBADO**
    SQLTPD &= "SELECT T0.IdAgente,T0.NomAgte,T0.IdClte,T0.NomClte,T0.ItmsGrpCod AS Linea,T0.ItmsGrpNam, "
    SQLTPD &= "T0.MontoVta, "
    SQLTPD &= "CASE WHEN T0.MontoVta = 0 OR  T1.MontoVta = 0 THEN 0 ELSE "
    SQLTPD &= "T0.MontoVta * 100 / T1.MontoVta END AS PorVta,T0.PzsVta, "
    SQLTPD &= "T0.MontoDev, "
    SQLTPD &= "CASE WHEN T0.MontoDev = 0 OR  T0.MontoVta = 0 THEN 0 ELSE "
    SQLTPD &= "T0.MontoDev * 100 / T0.MontoVta END AS PorDEV,T0.CantDev, "
    SQLTPD &= "T0.MontoNeto,T0.PzsNeta,T0.MontoPP, "
    SQLTPD &= "CASE WHEN T0.MontoPP = 0 OR  T0.MontoNeto = 0 THEN 0 ELSE "
    SQLTPD &= "T0.MontoPP * 100 / T0.MontoNeto END AS PorPP, "
    SQLTPD &= "T0.MontoNC "
    SQLTPD &= "FROM #T_A_C_LINEA T0 "
    SQLTPD &= "LEFT JOIN #T_AGTE_CLTE T1 ON T0.IdAgente = T1.IdAgente AND T0.IdClte = T1.IdClte "
    If (CkCteProp.Checked = False) Then
      SQLTPD &= " where T0.IdAgente <> 1 "
    End If
    SQLTPD &= " ORDER BY T0.IdAgente,MontoNeto DESC; "

    '****  3
    '--DETALLE VENTAS ARTICULOS
    '--*COMPROBADO***
    SQLTPD &= "SELECT Articulo, ItemName,  ItmsGrpNam, IdAgente, NomAgte, IdClte, NomClte, MONT_TOT, CANT_TOT,"
    SQLTPD &= "MontoDev, CantDev, MONTO_NETA, CANT_NETA, MontoPP,  MontoNC, ItmsGrpCod, TipoMov, ArtAgte, CantPP "
    SQLTPD &= "FROM #T_DET_VTA "
    If (CkCteProp.Checked = False) Then
      SQLTPD &= "where IdAgente <> 1 "
    End If
    SQLTPD &= "ORDER BY IdAgente,ItmsGrpNam,IdClte,Monto_Neta DESC "

    '****   5
    '--*VENTAS DE TODOS LOS MESES DE TODOS LOS CLIENTES**
    SQLTPD &= "SELECT T0.Cliente,T0.MesLetra,T0.Anio,T0.VtaMasAlta,T0.Mes,T1.CardCode AS IdClte,T1.CardName AS NomClte, "
    SQLTPD &= "T2.SlpName AS NomAgte FROM #VTA_MAYOR_C T0 "
    SQLTPD &= "LEFT JOIN OCRD T1 ON T1.CardCode = T0.Cliente "
    SQLTPD &= "LEFT JOIN OSLP T2 ON T1.SlpCode = T2.SlpCode "
    If (CkCteProp.Checked = False) Then
      SQLTPD &= " where T2.SlpCode<>1 "
    End If
    SQLTPD &= " ORDER BY T0.Cliente,T0.Anio DESC,T0.Mes DESC "


    SQLTPD &= "DROP TABLE #T_VTA_TOTAL "
    SQLTPD &= "DROP TABLE #T_DET_VTA "
    SQLTPD &= "DROP TABLE #T_VTA_AGTES "
    SQLTPD &= "DROP TABLE #T_AGTE_CLTE "
    SQLTPD &= "DROP TABLE #T_A_C_LINEA "
    SQLTPD &= "DROP TABLE #T_DET_CLTES "
    SQLTPD &= "DROP TABLE #VTA_MAYOR_C "
    SQLTPD &= "DROP TABLE #ULT_VTA_CLTE "

    ' Nuevo objeto Dataset   
    Dim DsVtasDet As New DataSet

    DTRefacciones.TableName = "EncVtasTot"

    DsVtasDet.Tables.Add(DTRefacciones)


    With comando
      .CommandTimeout = 200
      .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
      .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
      ' Asignar el sql para seleccionar los datos de la tabla Maestro   
      .CommandText = SQLTPD
      .Connection = conexion2
    End With

    '/***************************parte de codigo'
    With Adaptador
      .SelectCommand = comando
      ' llenar el dataset   
      '.TableMappings.Add("DetLinea", "DetArticulo")
      .Fill(DsVtasDet, "Ventas")
    End With

    DsVtasDet.Tables(1).TableName = "VtaAgtes"
    DsVtasDet.Tables(2).TableName = "VtaCltes"
    DsVtasDet.Tables(3).TableName = "VtaLineas"
    DsVtasDet.Tables(4).TableName = "VtaArticulo"
    DsVtasDet.Tables(5).TableName = "VtaMesClte"



    '**********************************************************************************************************************************

    Dim DtAgte As New DataTable
    DtAgte = DsVtasDet.Tables("VtaAgtes")

    'DataGridView1.DataSource = DtAgte


    With Me.DgVtaAgte
      .DataSource = DtAgte
      .ReadOnly = True
      'Color de Renglones en Grid
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      .RowHeadersVisible = False
      .SelectionMode = DataGridViewSelectionMode.FullRowSelect
      '.MultiSelect = False
      .AllowUserToAddRows = False
      'Color de linea del grid

      .Columns(0).Visible = False

      .Columns(1).HeaderText = "Vendedor"
      .Columns(1).Width = 150

      '.Columns(2).HeaderText = "Cod Ruta"
      '.Columns(2).Width = 20
      '.Columns(2).Visible = False

      .Columns(2).HeaderText = "Ruta"
      .Columns(2).Width = 80
      .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      .Columns(3).HeaderText = "$ Ventas Totales"
      .Columns(3).Width = 90
      .Columns(3).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(4).HeaderText = "% Vtas."
      .Columns(4).Width = 35
      .Columns(4).DefaultCellStyle.Format = "###.0"
      .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(5).HeaderText = "Piezas"
      .Columns(5).Width = 65
      .Columns(5).DefaultCellStyle.Format = "###,###,###"
      .Columns(5).Visible = False

      .Columns(6).HeaderText = "$ Monto Devuelto"
      .Columns(6).Width = 80
      .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(6).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(7).HeaderText = "% Dvol. Sobre Vta"
      .Columns(7).Width = 37
      .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(7).DefaultCellStyle.Format = "###.0"

      .Columns(8).HeaderText = "Piezas Devueltas"
      .Columns(8).Width = 60
      .Columns(8).DefaultCellStyle.Format = "###,###,###"
      .Columns(8).Visible = False

      .Columns(9).HeaderText = "Ventas Netas"
      .Columns(9).Width = 90
      .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(9).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(10).HeaderText = "Piezas Netas"
      .Columns(10).Width = 65
      .Columns(10).DefaultCellStyle.Format = "###,###,###"
      .Columns(10).Visible = False


      .Columns(11).HeaderText = "Descuentos Pronto Pago"
      .Columns(11).Width = 80
      .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(11).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(12).HeaderText = "% PP Sobre Vta"
      .Columns(12).Width = 39
      .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(12).DefaultCellStyle.Format = "###.0"
      '  .Columns(7).Visible = False

      .Columns(13).HeaderText = "Notas de Credito"
      .Columns(13).Width = 80
      .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(13).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(14).Visible = False
      .Columns(15).Visible = False
      .Columns(16).Visible = False

    End With


    dvClientes.Table = DsVtasDet.Tables("VtaCltes")

    With Me.DgClientes
      .DataSource = dvClientes
      .ReadOnly = True
      'Color de Renglones en Grid
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      .RowHeadersVisible = False
      .SelectionMode = DataGridViewSelectionMode.FullRowSelect
      .MultiSelect = False
      .AllowUserToAddRows = False
      'Color de linea del grid

      .Columns(0).Visible = False
      .Columns(1).HeaderText = "Id Cliente"
      .Columns(1).Width = 45

      .Columns(2).HeaderText = "Nombre"
      .Columns(2).Width = 145

      .Columns(3).HeaderText = "Ciudad"
      .Columns(3).Width = 80

      .Columns(4).HeaderText = "Edo."
      .Columns(4).Width = 34

      .Columns(5).HeaderText = "Ruta"
      .Columns(5).Width = 80
      .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      .Columns(6).HeaderText = "$ Ventas Totales"
      .Columns(6).Width = 80
      .Columns(6).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(7).HeaderText = "% Vtas."
      .Columns(7).Width = 35
      .Columns(7).DefaultCellStyle.Format = "###.0"
      .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(8).HeaderText = "Piezas"
      .Columns(8).Width = 65
      .Columns(8).DefaultCellStyle.Format = "###,###,###"
      .Columns(8).Visible = False

      .Columns(9).HeaderText = "$ Venta Más ALta"
      .Columns(9).Width = 75
      .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(10).HeaderText = "Mes Vta"
      .Columns(10).Width = 65

      .Columns(11).HeaderText = "Año Vta"
      .Columns(11).Width = 35

      .Columns(12).HeaderText = "Fecha Última Vta"
      .Columns(12).Width = 70

      .Columns(13).HeaderText = "Días Trans."
      .Columns(13).Width = 40

      .Columns(14).HeaderText = "Factura Más ALta"
      .Columns(14).Width = 65
      .Columns(14).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(15).HeaderText = "$ Monto Devuelto"
      .Columns(15).Width = 60
      .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(15).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(16).HeaderText = "% Dvol. Sobre Vta"
      .Columns(16).Width = 37
      .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(16).DefaultCellStyle.Format = "###.0"

      .Columns(17).HeaderText = "Piezas Devueltas"
      .Columns(17).Width = 60
      .Columns(17).DefaultCellStyle.Format = "###,###,###"
      .Columns(17).Visible = False

      .Columns(18).HeaderText = "Ventas Netas"
      .Columns(18).Width = 80
      .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(18).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(19).HeaderText = "Piezas Netas"
      .Columns(19).Width = 65
      .Columns(19).DefaultCellStyle.Format = "###,###,###"
      .Columns(19).Visible = False

      .Columns(20).HeaderText = "Descuentos Pronto Pago"
      .Columns(20).Width = 70
      .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(20).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(21).HeaderText = "% PP Sobre Vta"
      .Columns(21).Width = 39
      .Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(21).DefaultCellStyle.Format = "###.0"
      '  .Columns(7).Visible = False

      .Columns(22).HeaderText = "Notas de Credito"
      .Columns(22).Width = 70
      .Columns(22).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(22).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(23).HeaderText = "Vendedor"
      .Columns(23).Width = 150


    End With

    dvLineas.Table = DsVtasDet.Tables("VtaLineas")

    With Me.DgLineas
      .DataSource = dvLineas
      .ReadOnly = True
      'Color de Renglones en Grid
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      .RowHeadersVisible = False
      .SelectionMode = DataGridViewSelectionMode.FullRowSelect
      .MultiSelect = False
      .AllowUserToAddRows = False
      'Color de linea del grid

      .Columns(0).Visible = False
      .Columns(1).Visible = False
      .Columns(2).Visible = False
      .Columns(3).Visible = False
      .Columns(4).Visible = False

      .Columns(5).HeaderText = "Linea"
      .Columns(5).Width = 105

      .Columns(6).HeaderText = "$ Ventas Totales"
      .Columns(6).Width = 70
      .Columns(6).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(7).HeaderText = "% Vtas."
      .Columns(7).Width = 35
      .Columns(7).DefaultCellStyle.Format = "###.0"
      .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(8).HeaderText = "Pza"
      .Columns(8).Width = 40
      .Columns(8).DefaultCellStyle.Format = "###,###,###"
      .Columns(8).Visible = True

      .Columns(9).HeaderText = "$ Monto Devuelto"
      .Columns(9).Width = 60
      .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(9).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(10).HeaderText = "% Dvol. Sobre Vta"
      .Columns(10).Width = 37
      .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(10).DefaultCellStyle.Format = "###.0"

      .Columns(11).HeaderText = "Pza Dev."
      .Columns(11).Width = 35
      .Columns(11).DefaultCellStyle.Format = "###,###,###"
      .Columns(11).Visible = True

      .Columns(12).HeaderText = "Ventas Netas"
      .Columns(12).Width = 65
      .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(12).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(13).HeaderText = "Pza Net"
      .Columns(13).Width = 35
      .Columns(13).DefaultCellStyle.Format = "###,###,###"
      .Columns(13).Visible = True


      .Columns(14).HeaderText = "Descuentos Pronto Pago"
      .Columns(14).Width = 70
      .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(14).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(15).HeaderText = "% PP Sob Vta"
      .Columns(15).Width = 30
      .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(15).DefaultCellStyle.Format = "###.0"
      '  .Columns(7).Visible = False

      .Columns(16).HeaderText = "Notas de Cred."
      .Columns(16).Width = 60
      .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(16).DefaultCellStyle.Format = "###,###,###.00"

    End With

    dvVtaMens.Table = DsVtasDet.Tables("VtaMesClte")

    With Me.DgVtaMens
      .DataSource = dvVtaMens
      .ReadOnly = True
      'Color de Renglones en Grid
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      .RowHeadersVisible = False
      .SelectionMode = DataGridViewSelectionMode.FullRowSelect
      .MultiSelect = False
      .AllowUserToAddRows = False
      'Color de linea del grid

      .Columns(0).Visible = False

      .Columns(1).HeaderText = "Mes Vta"
      .Columns(1).Width = 65

      .Columns(2).HeaderText = "Año Vta"
      .Columns(2).Width = 35

      .Columns(3).HeaderText = "$ Venta "
      .Columns(3).Width = 75
      .Columns(3).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(4).Visible = False
      .Columns(5).Visible = False
      .Columns(6).Visible = False
      .Columns(7).Visible = False

    End With

    dvArticulos.Table = DsVtasDet.Tables("VtaArticulo")

    With Me.DgArticulos
      .DataSource = dvArticulos
      .ReadOnly = True
      'Color de Renglones en Grid
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      .RowHeadersVisible = False
      .SelectionMode = DataGridViewSelectionMode.FullRowSelect
      .MultiSelect = False
      .AllowUserToAddRows = False
      'Color de linea del grid


      .Columns(0).HeaderText = "Articulo"
      .Columns(0).Width = 100

      .Columns(1).HeaderText = "Descripción"
      .Columns(1).Width = 210


      .Columns(2).Visible = False
      .Columns(3).Visible = False
      .Columns(4).Visible = False
      .Columns(5).Visible = False
      .Columns(6).Visible = False


      .Columns(7).HeaderText = "$ Ventas Totales"
      .Columns(7).Width = 65
      .Columns(7).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(8).HeaderText = "Pza"
      .Columns(8).Width = 30
      .Columns(8).DefaultCellStyle.Format = "###,###,###"
      .Columns(8).Visible = True

      .Columns(9).HeaderText = "$ Monto Devuelto"
      .Columns(9).Width = 55
      .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(9).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(10).HeaderText = "Pza Dev."
      .Columns(10).Width = 35
      .Columns(10).DefaultCellStyle.Format = "###,###,###"
      .Columns(10).Visible = True

      .Columns(11).HeaderText = "Ventas Netas"
      .Columns(11).Width = 65
      .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(11).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(12).HeaderText = "Pza Net"
      .Columns(12).Width = 35
      .Columns(12).DefaultCellStyle.Format = "###,###,###"
      .Columns(12).Visible = True


      .Columns(13).HeaderText = "Descuentos Pronto Pago"
      .Columns(13).Width = 60
      .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(13).DefaultCellStyle.Format = "###,###,###.00"


      .Columns(14).HeaderText = "Notas de Cred."
      .Columns(14).Width = 60
      .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(14).DefaultCellStyle.Format = "###,###,###.00"


      .Columns(15).Visible = False
      .Columns(16).Visible = False
      .Columns(17).Visible = False
      .Columns(18).Visible = False

    End With


    ' cerrar la conexíón   
    With conexion2
      If .State = ConnectionState.Open Then
        .Close()
      End If
      .Dispose()
    End With

  End Sub


  '''''''MODIFICACION 1
  Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

      Dim cel9 As String = IIf(IsDBNull(Me.DgLineas.Item(8, fila_dt).Value), 0, Me.DgLineas.Item(8, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.DgLineas.Item(9, fila_dt).Value), 0, Me.DgLineas.Item(9, fila_dt).Value)
      Dim cel11 As String = IIf(IsDBNull(Me.DgLineas.Item(10, fila_dt).Value), 0, Me.DgLineas.Item(10, fila_dt).Value)
      Dim cel12 As String = IIf(IsDBNull(Me.DgLineas.Item(11, fila_dt).Value), 0, Me.DgLineas.Item(11, fila_dt).Value)

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

  'Private Sub DgVtaAgte_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '    filtrar_Clientes()
  'End Sub

  'Private Sub DgClientes_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '    filtrar_Lineas()
  '    filtrar_VtaMensual()
  'End Sub

  Private Sub VtasClientesAgte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.Text = "Agente - Cliente (Rutas) -- " & Me.Name.ToString & ".vb"
    If UsrTPM = "MANAGER" Then

      pEncabezado.Location = New Point(100, 0)

      DgVtaAgte.Width = 703
      DgVtaAgte.Height = 445
      DgClientes.Location = New Point(718, 44)
      DgClientes.Width = 645
      DgClientes.Height = 445
      Label11.Location = New Point(3, 492)
      DgArticulos.Width = 720
      DgArticulos.Location = New Point(3, 510)
      Label2.Location = New Point(729, 492)
      DgLineas.Location = New Point(729, 510)
      DgLineas.Width = 433
      Label10.Location = New Point(1167, 492)
      DgVtaMens.Location = New Point(1167, 510)

      Me.WindowState = FormWindowState.Maximized

    End If

    Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
    Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

    If VEsAgente = 1 Then
      CkCteProp.Checked = False
      CkCteProp.Enabled = False
    End If

  End Sub

  'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

  'End Sub
  Sub filtrar_Clientes()
    Try
      If DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value = 0 Then
        dvClientes.RowFilter = String.Empty
      ElseIf DgVtaAgte.Item(16, DgVtaAgte.CurrentRow.Index).Value = 2 Then
        dvClientes.RowFilter = "IdAgente =" & DgVtaAgte.Item(14, DgVtaAgte.CurrentRow.Index).Value.ToString
      Else
        dvClientes.RowFilter = "IdAgente =" & DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value.ToString & " AND RUTA ='" & DgVtaAgte.Item(2, DgVtaAgte.CurrentRow.Index).Value.ToString & "'"
      End If

    Catch ex As Exception
      'MsgBox(ex.Message)
    End Try

  End Sub

  Sub filtrar_Lineas()
    Try

      dvLineas.RowFilter = "IdAgente =" & DgClientes.Item(0, DgClientes.CurrentRow.Index).Value.ToString & " AND " &
            "IdClte ='" & DgClientes.Item(1, DgClientes.CurrentRow.Index).Value.ToString & "'"

    Catch ex As Exception
    End Try

  End Sub

  Sub filtrar_VtaMensual()
    Try
      dvVtaMens.RowFilter = "Cliente ='" & DgClientes.Item(1, DgClientes.CurrentRow.Index).Value.ToString & "'"

    Catch ex As Exception
    End Try

  End Sub

  Sub filtrar_Articulos()
    Try
      dvArticulos.RowFilter = "ItmsGrpCod =" & DgLineas.Item(4, DgLineas.CurrentRow.Index).Value.ToString & " AND " &
            "IdClte ='" & DgLineas.Item(2, DgLineas.CurrentRow.Index).Value.ToString & "' AND " &
            "IdAgente =" & DgLineas.Item(0, DgLineas.CurrentRow.Index).Value.ToString

    Catch ex As Exception
      dvArticulos.RowFilter = "ItmsGrpCod = 0"
    End Try
  End Sub


  Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
    cargar_registros()
    Try
      DgVtaAgte.CurrentCell = DgVtaAgte.Rows(0).Cells(1)
    Catch
    End Try
    filtrar_Clientes()
    filtrar_Lineas()
    filtrar_VtaMensual()
    filtrar_Articulos()
  End Sub

  Private Sub DgVtaAgte_SelectionChanged_1(sender As Object, e As EventArgs) Handles DgVtaAgte.SelectionChanged
    filtrar_Clientes()
  End Sub

  Private Sub DgClientes_SelectionChanged_1(sender As Object, e As EventArgs) Handles DgClientes.SelectionChanged
    filtrar_Lineas()
    filtrar_VtaMensual()
  End Sub

  Private Sub DgLineas_SelectionChanged_1(sender As Object, e As EventArgs) Handles DgLineas.SelectionChanged
    filtrar_Articulos()
  End Sub

  Private Sub BtnAgentes_Click_1(sender As Object, e As EventArgs) Handles BtnAgentes.Click
    'ExportarNuevoAgentes()
    ExportarViejoAgentes()
  End Sub

  Sub ExportarNuevoAgentes()
    'Dim dv As DataView = DirectCast(DgVtaAgte.DataSource, DataView)
    'Dim ds As DataSet = DgVtaAgte.DataSource
    Dim dt As DataTable = DgVtaAgte.DataSource

    Dim wb = New XLWorkbook()
    Dim ws = wb.Worksheets.Add("Agente-Cliente(Rutas-Agente)")

    Dim range = ws.Range(3, 1, dt.Rows.Count + 3, dt.Columns.Count).Merge().AddToNamed("Rutas")
    Dim rangeWithData = ws.Cell(4, 1).InsertData(dt.AsEnumerable)

    'ws.Columns("N").Delete()

    Dim tab = range.CreateTable()
    tab.Theme = XLTableTheme.TableStyleLight8

    'DAR FOMATO A LAS CELDAS
    Dim index As Integer = 3

    For i As Integer = 0 To dt.Rows.Count

      Try
        'Encabezados dependiendo
        If index = 3 Then
          Dim cellA3 As String = String.Format("A{0}", 1)
          wb.Worksheet(1).Cells(cellA3).Value = "Reporte de Ventas Por Agente Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
          wb.Worksheet(1).Cells(cellA3).Style.Font.Bold = True

          Dim cellA0 As String = String.Format("A{0}", index)
          wb.Worksheet(1).Cells(cellA0).Value = "Vendedor"

          Dim cellB0 As String = String.Format("B{0}", index)
          wb.Worksheet(1).Cells(cellB0).Value = "Ruta"

          Dim cellC0 As String = String.Format("C{0}", index)
          wb.Worksheet(1).Cells(cellC0).Value = "Ventas Totales"

          Dim cellD0 As String = String.Format("D{0}", index)
          wb.Worksheet(1).Cells(cellD0).Value = "% Vtas. Tot."

          Dim cellE0 As String = String.Format("E{0}", index)
          wb.Worksheet(1).Cells(cellE0).Value = "Monto Devuelto"

          Dim cellF0 As String = String.Format("F{0}", index)
          wb.Worksheet(1).Cells(cellF0).Value = "% Dvol. Sobre Venta"

          Dim cellG0 As String = String.Format("G{0}", index)
          wb.Worksheet(1).Cells(cellG0).Value = "Ventas Netas"

          Dim cellH0 As String = String.Format("H{0}", index)
          wb.Worksheet(1).Cells(cellH0).Value = "Descuentos Pronto Pago"

          Dim cellI0 As String = String.Format("I{0}", index)
          wb.Worksheet(1).Cells(cellI0).Value = "% PP Sobre Venta"

          Dim cellJ0 As String = String.Format("J{0}", index)
          wb.Worksheet(1).Cells(cellJ0).Value = "Total Notas de Credito"

          'AGREGAR LAS CELDAS DEPENDIENDO EL REPORTE SEGUIR LA MISMA ESTRUCTURA

          index = index + 1
        End If

        'Formato de cada una de las celdas
        Dim cellA As String = String.Format("A{0}", index)
        'wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellB As String = String.Format("B{0}", index)
        'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellC As String = String.Format("C{0}", index)
        wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellD As String = String.Format("D{0}", index)
        wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "0.00\%"

        Dim cellE As String = String.Format("E{0}", index)
        wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellF As String = String.Format("F{0}", index)
        wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "0.00\%"

        Dim cellG As String = String.Format("G{0}", index)
        wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellH As String = String.Format("H{0}", index)
        wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellI As String = String.Format("I{0}", index)
        wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "0.00\%"

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
      saveFileDialog1.FileName = "Agente-Cliente(Rutas-Agente) de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"
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
    oSheet.range("A3").value = "Vendedor"
    oSheet.range("B3").value = "Ruta"
    oSheet.range("C3").value = "Ventas Totales"
    oSheet.range("D3").value = "% Vtas. Tot."
    oSheet.range("E3").value = "Monto Devuelto"
    oSheet.range("F3").value = "% Dvol. Sobre Venta"
    oSheet.range("G3").value = " Ventas Netas"
    oSheet.range("H3").value = "Descuentos Pronto Pago"
    oSheet.range("I3").value = " % PP Sobre Venta"
    oSheet.range("J3").value = "Total Notas de Credito"

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A3:J3").font.bold = True
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgVtaAgte.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.DgVtaAgte.Item(1, fila_dt).Value
      Dim cel2 As String = Me.DgVtaAgte.Item(2, fila_dt).Value
      Dim cel3 As String = Me.DgVtaAgte.Item(3, fila_dt).Value
      Dim cel4 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(4, fila_dt).Value), 0, Me.DgVtaAgte.Item(4, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(6, fila_dt).Value), 0, Me.DgVtaAgte.Item(6, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(7, fila_dt).Value), 0, Me.DgVtaAgte.Item(7, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(9, fila_dt).Value), 0, Me.DgVtaAgte.Item(9, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(11, fila_dt).Value), 0, Me.DgVtaAgte.Item(11, fila_dt).Value)
      Dim cel9 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(12, fila_dt).Value), 0, Me.DgVtaAgte.Item(12, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(13, fila_dt).Value), 0, Me.DgVtaAgte.Item(13, fila_dt).Value)

      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel1
      oSheet.range("B" & fila_dt_excel).value = cel2
      oSheet.range("C" & fila_dt_excel).value = cel3
      oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
      oSheet.range("E" & fila_dt_excel).value = cel5
      oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
      oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
      oSheet.range("H" & fila_dt_excel).value = cel8

      oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
      oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel10, 2)

    Next


    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:J").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Ventas Por Agente Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
    oSheet.range("C1").value = Rangos
    oSheet.range("C2").value = Rangos2
    'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

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


    'Declaramos el nombre de las columnas
    oSheet.range("A3").value = "Id Cliente"
    oSheet.range("B3").value = "Nombre"
    oSheet.range("C3").value = "Ciudad"
    oSheet.range("D3").value = "Edo."
    oSheet.range("E3").value = "Ruta"
    oSheet.range("F3").value = "$ Ventas Totales"
    oSheet.range("G3").value = "% Vtas."
    oSheet.range("H3").value = "$ Venta Más ALta"
    oSheet.range("I3").value = "Mes Vta" '10'
    oSheet.range("J3").value = "Año Vta" '11'

    oSheet.range("K3").value = "Fecha Última Vta" '12'
    oSheet.range("L3").value = "Días Trans." '13'
    oSheet.range("M3").value = "Factura Mas ALta" '14'
    oSheet.range("N3").value = "Monto Devuelto" '15'
    oSheet.range("O3").value = "% Dvol. Sobre Venta" '17'
    oSheet.range("P3").value = " Ventas Netas" '19'
    oSheet.range("Q3").value = "Descuentos Pronto Pago" '20'
    oSheet.range("R3").value = " % PP Sobre Venta" '21'
    oSheet.range("S3").value = "Total Notas de Credito" '22'
    oSheet.range("T3").value = "Vendedor" '23'



    'para poner la primera fila de los titulos en negrita
    oSheet.range("A3:T3").font.bold = True
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgClientes.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.DgClientes.Item(1, fila_dt).Value
      Dim cel2 As String = Me.DgClientes.Item(2, fila_dt).Value
      Dim cel3 As String = IIf(IsDBNull(Me.DgClientes.Item(3, fila_dt).Value), 0, Me.DgClientes.Item(3, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgClientes.Item(4, fila_dt).Value), 0, Me.DgClientes.Item(4, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgClientes.Item(5, fila_dt).Value), 0, Me.DgClientes.Item(5, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgClientes.Item(6, fila_dt).Value), 0, Me.DgClientes.Item(6, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgClientes.Item(7, fila_dt).Value), 0, Me.DgClientes.Item(7, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgClientes.Item(9, fila_dt).Value), 0, Me.DgClientes.Item(9, fila_dt).Value)
      Dim cel9 As String = IIf(IsDBNull(Me.DgClientes.Item(10, fila_dt).Value), 0, Me.DgClientes.Item(10, fila_dt).Value)

      'MsgBox("Celda 11: " & Me.DgClientes.Item(11, fila_dt).Value.ToString & " CELDA 10: " & Me.DgClientes.Item(10, fila_dt).Value.ToString)
      'Dim cel10 As Date = IIf(IsDBNull(Me.DgClientes.Item(11, fila_dt).Value), "12/12/1999", Me.DgClientes.Item(11, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.DgClientes.Item(11, fila_dt).Value), 0, Me.DgClientes.Item(11, fila_dt).Value)
      'Dim cel11 As String = IIf(IsDBNull(Me.DgClientes.Item(12, fila_dt).Value), 0, Me.DgClientes.Item(12, fila_dt).Value)
      Dim cel11 As Date = IIf(IsDBNull(Me.DgClientes.Item(12, fila_dt).Value), "12/12/1999", Me.DgClientes.Item(12, fila_dt).Value)
      Dim cel12 As String = IIf(IsDBNull(Me.DgClientes.Item(13, fila_dt).Value), 0, Me.DgClientes.Item(13, fila_dt).Value)
      Dim cel13 As String = IIf(IsDBNull(Me.DgClientes.Item(14, fila_dt).Value), 0, Me.DgClientes.Item(14, fila_dt).Value)
      Dim cel14 As String = IIf(IsDBNull(Me.DgClientes.Item(15, fila_dt).Value), 0, Me.DgClientes.Item(15, fila_dt).Value)
      Dim cel15 As String = IIf(IsDBNull(Me.DgClientes.Item(17, fila_dt).Value), 0, Me.DgClientes.Item(17, fila_dt).Value)
      Dim cel16 As String = IIf(IsDBNull(Me.DgClientes.Item(18, fila_dt).Value), 0, Me.DgClientes.Item(18, fila_dt).Value)
      Dim cel17 As String = IIf(IsDBNull(Me.DgClientes.Item(20, fila_dt).Value), 0, Me.DgClientes.Item(20, fila_dt).Value)
      Dim cel18 As String = IIf(IsDBNull(Me.DgClientes.Item(21, fila_dt).Value), 0, Me.DgClientes.Item(21, fila_dt).Value)
      Dim cel19 As String = IIf(IsDBNull(Me.DgClientes.Item(22, fila_dt).Value), 0, Me.DgClientes.Item(22, fila_dt).Value)
      Dim cel20 As String = IIf(IsDBNull(Me.DgClientes.Item(23, fila_dt).Value), 0, Me.DgClientes.Item(23, fila_dt).Value)

      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel1
      oSheet.range("B" & fila_dt_excel).value = cel2
      oSheet.range("C" & fila_dt_excel).value = cel3
      oSheet.range("D" & fila_dt_excel).value = cel4
      oSheet.range("E" & fila_dt_excel).value = cel5
      oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
      oSheet.range("G" & fila_dt_excel).value = cel7
      oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)
      oSheet.range("I" & fila_dt_excel).value = cel9

      oSheet.range("J" & fila_dt_excel).value = cel10
      oSheet.range("K" & fila_dt_excel).value = cel11
      oSheet.range("L" & fila_dt_excel).value = cel12

      oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel13, 2)
      oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel14, 2)
      oSheet.range("O" & fila_dt_excel).value = cel15
      oSheet.range("P" & fila_dt_excel).value = FormatNumber(cel16, 2)
      oSheet.range("Q" & fila_dt_excel).value = FormatNumber(cel17, 2)
      oSheet.range("R" & fila_dt_excel).value = cel18
      oSheet.range("S" & fila_dt_excel).value = FormatNumber(cel19, 2)
      oSheet.range("T" & fila_dt_excel).value = cel20

    Next


    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:T").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Ventas Por Cliente Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
    oSheet.range("C1").value = Rangos
    oSheet.range("C2").value = Rangos2
    'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub

  Private Sub BtnLinea_Click_1(sender As Object, e As EventArgs) Handles BtnLinea.Click
    Dim oExcel As Object
    Dim oBook As Object
    Dim oSheet As Object

    'Abrimos un nuevo libro
    oExcel = CreateObject("Excel.Application")
    oBook = oExcel.workbooks.add
    oSheet = oBook.worksheets(1)
    'ahregar idagente nombre, cliente nomcliente

    'Declaramos el nombre de las columnas

    oSheet.range("A3").value = "Vendedor"
    oSheet.range("B3").value = "IdCliente"
    oSheet.range("C3").value = "Nombre"
    oSheet.range("D3").value = "Linea"
    oSheet.range("E3").value = "$ Ventas Totales"
    oSheet.range("F3").value = "% Vtas."
    oSheet.range("G3").value = "Pzas"
    oSheet.range("H3").value = "$ Monto Devuelto"
    oSheet.range("I3").value = "% Dvol. Sobre Vta"

    oSheet.range("J3").value = "Pzas Dev."
    oSheet.range("K3").value = "Ventas Netas"
    oSheet.range("L3").value = "Pza Net"
    oSheet.range("M3").value = "Desc. Pronto Pago"
    oSheet.range("N3").value = "% PP Sob Vta"
    oSheet.range("O3").value = "Notas de Cred."


    'para poner la primera fila de los titulos en negrita
    oSheet.range("A3:S3").font.bold = True
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgLineas.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.DgLineas.Item(1, fila_dt).Value
      Dim cel2 As String = Me.DgLineas.Item(2, fila_dt).Value
      Dim cel3 As String = IIf(IsDBNull(Me.DgLineas.Item(3, fila_dt).Value), 0, Me.DgLineas.Item(3, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgLineas.Item(5, fila_dt).Value), 0, Me.DgLineas.Item(5, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgLineas.Item(6, fila_dt).Value), 0, Me.DgLineas.Item(6, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgLineas.Item(7, fila_dt).Value), 0, Me.DgLineas.Item(7, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgLineas.Item(8, fila_dt).Value), 0, Me.DgLineas.Item(8, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgLineas.Item(9, fila_dt).Value), 0, Me.DgLineas.Item(9, fila_dt).Value)

      Dim cel9 As String = IIf(IsDBNull(Me.DgLineas.Item(10, fila_dt).Value), 0, Me.DgLineas.Item(10, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.DgLineas.Item(11, fila_dt).Value), 0, Me.DgLineas.Item(11, fila_dt).Value)
      Dim cel11 As String = IIf(IsDBNull(Me.DgLineas.Item(12, fila_dt).Value), 0, Me.DgLineas.Item(12, fila_dt).Value)
      Dim cel12 As String = IIf(IsDBNull(Me.DgLineas.Item(13, fila_dt).Value), 0, Me.DgLineas.Item(13, fila_dt).Value)
      Dim cel13 As String = IIf(IsDBNull(Me.DgLineas.Item(14, fila_dt).Value), 0, Me.DgLineas.Item(14, fila_dt).Value)
      Dim cel14 As String = IIf(IsDBNull(Me.DgLineas.Item(15, fila_dt).Value), 0, Me.DgLineas.Item(15, fila_dt).Value)
      Dim cel15 As String = IIf(IsDBNull(Me.DgLineas.Item(16, fila_dt).Value), 0, Me.DgLineas.Item(16, fila_dt).Value)


      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel1
      oSheet.range("B" & fila_dt_excel).value = cel2
      oSheet.range("C" & fila_dt_excel).value = cel3
      oSheet.range("D" & fila_dt_excel).value = cel4
      oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
      oSheet.range("F" & fila_dt_excel).value = cel6
      oSheet.range("G" & fila_dt_excel).value = cel7
      oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)

      oSheet.range("I" & fila_dt_excel).value = cel9
      oSheet.range("J" & fila_dt_excel).value = cel10
      oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel11, 2)

      oSheet.range("L" & fila_dt_excel).value = cel12
      oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel13, 2)
      oSheet.range("N" & fila_dt_excel).value = cel14
      oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel15, 2)

    Next


    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:T").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Ventas Por Linea Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
    oSheet.range("C1").value = Rangos
    oSheet.range("C2").value = Rangos2
    'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub

  Private Sub BtnHistVtas_Click_1(sender As Object, e As EventArgs) Handles BtnHistVtas.Click
    Dim oExcel As Object
    Dim oBook As Object
    Dim oSheet As Object

    'Abrimos un nuevo libro
    oExcel = CreateObject("Excel.Application")
    oBook = oExcel.workbooks.add
    oSheet = oBook.worksheets(1)


    'Declaramos el nombre de las columnas
    oSheet.range("A3").value = "Mes Vta"
    oSheet.range("B3").value = "Año Vta"
    oSheet.range("C3").value = "$ Venta "
    oSheet.range("D3").value = "Id Cliente"
    oSheet.range("E3").value = "Nombre"
    oSheet.range("F3").value = "Vendedor"


    'para poner la primera fila de los titulos en negrita
    oSheet.range("A3:I3").font.bold = True
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgVtaMens.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.DgVtaMens.Item(1, fila_dt).Value
      Dim cel2 As String = Me.DgVtaMens.Item(2, fila_dt).Value
      Dim cel3 As String = IIf(IsDBNull(Me.DgVtaMens.Item(3, fila_dt).Value), 0, Me.DgVtaMens.Item(3, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgVtaMens.Item(5, fila_dt).Value), 0, Me.DgVtaMens.Item(5, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgVtaMens.Item(6, fila_dt).Value), 0, Me.DgVtaMens.Item(6, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgVtaMens.Item(7, fila_dt).Value), 0, Me.DgVtaMens.Item(7, fila_dt).Value)


      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel1
      oSheet.range("B" & fila_dt_excel).value = cel2
      oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel3, 2)
      oSheet.range("D" & fila_dt_excel).value = cel4
      oSheet.range("E" & fila_dt_excel).value = cel5
      oSheet.range("F" & fila_dt_excel).value = cel6

    Next


    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:I").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Ventas Por Agente Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
    oSheet.range("C1").value = Rangos
    oSheet.range("C2").value = Rangos2
    'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub

  Private Sub BtnArticulo_Click_1(sender As Object, e As EventArgs) Handles BtnArticulo.Click
    Dim oExcel As Object
    Dim oBook As Object
    Dim oSheet As Object

    'Abrimos un nuevo libro
    oExcel = CreateObject("Excel.Application")
    oBook = oExcel.workbooks.add
    oSheet = oBook.worksheets(1)
    'ahregar idagente nombre, cliente nomcliente

    'Declaramos el nombre de las columnas

    oSheet.range("A3").value = "Artículo"
    oSheet.range("B3").value = "Descripción"
    oSheet.range("C3").value = "Linea"
    oSheet.range("D3").value = "Vendedor"
    oSheet.range("E3").value = "IdClte"
    oSheet.range("F3").value = "Cliente"
    oSheet.range("G3").value = "$ Ventas Totales"
    oSheet.range("H3").value = "Pzas"
    oSheet.range("I3").value = "$ Monto Devuelto"

    oSheet.range("J3").value = "Pzas Dev."
    oSheet.range("K3").value = "Ventas Netas"
    oSheet.range("L3").value = "Pza Net"
    oSheet.range("M3").value = "Desc. Pronto Pago"
    oSheet.range("N3").value = "Notas de Cred."



    'para poner la primera fila de los titulos en negrita
    oSheet.range("A3:N3").font.bold = True
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgArticulos.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.DgArticulos.Item(0, fila_dt).Value
      Dim cel2 As String = Me.DgArticulos.Item(1, fila_dt).Value
      Dim cel3 As String = IIf(IsDBNull(Me.DgArticulos.Item(2, fila_dt).Value), 0, Me.DgArticulos.Item(2, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgArticulos.Item(4, fila_dt).Value), 0, Me.DgArticulos.Item(4, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgArticulos.Item(5, fila_dt).Value), 0, Me.DgArticulos.Item(5, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgArticulos.Item(6, fila_dt).Value), 0, Me.DgArticulos.Item(6, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgArticulos.Item(7, fila_dt).Value), 0, Me.DgArticulos.Item(7, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgArticulos.Item(8, fila_dt).Value), 0, Me.DgArticulos.Item(8, fila_dt).Value)

      Dim cel9 As String = IIf(IsDBNull(Me.DgArticulos.Item(9, fila_dt).Value), 0, Me.DgArticulos.Item(9, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.DgArticulos.Item(10, fila_dt).Value), 0, Me.DgArticulos.Item(10, fila_dt).Value)
      Dim cel11 As String = IIf(IsDBNull(Me.DgArticulos.Item(11, fila_dt).Value), 0, Me.DgArticulos.Item(11, fila_dt).Value)
      Dim cel12 As String = IIf(IsDBNull(Me.DgArticulos.Item(12, fila_dt).Value), 0, Me.DgArticulos.Item(12, fila_dt).Value)
      Dim cel13 As String = IIf(IsDBNull(Me.DgArticulos.Item(13, fila_dt).Value), 0, Me.DgArticulos.Item(13, fila_dt).Value)
      Dim cel14 As String = IIf(IsDBNull(Me.DgArticulos.Item(14, fila_dt).Value), 0, Me.DgArticulos.Item(14, fila_dt).Value)

      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel1
      oSheet.range("B" & fila_dt_excel).value = cel2
      oSheet.range("C" & fila_dt_excel).value = cel3
      oSheet.range("D" & fila_dt_excel).value = cel4
      oSheet.range("E" & fila_dt_excel).value = cel5
      oSheet.range("F" & fila_dt_excel).value = cel6
      oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
      oSheet.range("H" & fila_dt_excel).value = cel8

      'FormatNumber(cel5, 2)

      oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
      oSheet.range("J" & fila_dt_excel).value = cel10
      oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel11, 2)

      oSheet.range("L" & fila_dt_excel).value = cel12
      oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel13, 2)
      oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel14, 2)

    Next


    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:T").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Ventas Por Linea Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
    oSheet.range("C1").value = Rangos
    oSheet.range("C2").value = Rangos2
    'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub

  Private Sub pEncabezado_Paint(sender As Object, e As PaintEventArgs) Handles pEncabezado.Paint

  End Sub
End Class


