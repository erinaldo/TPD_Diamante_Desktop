Imports System.Data.SqlClient
Imports System.Data
Public Class VtasClientesPA

    Private dvLineas As New DataView
    Private dvVtaMens As New DataView
    'Private dvAgentes As New DataView
    Private dvClientes As New DataView
    Private dvArticulos As New DataView


    Dim Rangos As String = ""
    Dim Rangos2 As String = ""

    Private Sub ConsultaProd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")
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

        '----'--CANCELACIONES,Articulo-Agte,Clte
        SQLTPD = "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte,"
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,"
        SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,"
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte,"
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
        SQLTPD &= "INTO #Tem_Canc "
        SQLTPD &= "FROM ORIN T0 "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "

        If VEsAgente = 1 Then

            SQLTPD &= "AND T0.Slpcode = " & vCodAgte.ToString & " "

        End If

        SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F') AND T0.DocType  = 'I' AND Series <> 49 "
        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "


        '----'--DESCUENTOS PP,Articulo-Agte,Clte
        SQLTPD &= "SELECT 'DESCUENTOS PP' AS ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte,"
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,"
        SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,"
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) +  'DESCUENTOS PP' + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte,"
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
        SQLTPD &= "INTO #Tem_DesPP "
        SQLTPD &= "FROM ORIN T0 "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "

        If VEsAgente = 1 Then

            SQLTPD &= "AND T0.Slpcode = " & vCodAgte.ToString & " "

        End If


        SQLTPD &= "AND (T0.EDocPrefix = 'NC' or T0.EDocPrefix is null or T0.EDocPrefix = 'F') AND T0.DocType  = 'S' AND Series <> 49 "
        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "


        '--'--DEVOLUCIONES,Articulo-Agte,Clte
        SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte,"
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,"
        SQLTPD &= "(CAST('DEVOLUCION' AS CHAR(20))) AS TipoMov,"
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE + RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte,"
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
        SQLTPD &= "INTO #Tem_Dev "
        SQLTPD &= "FROM ORIN T0 "
        SQLTPD &= "INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND DocType  = 'I' AND  EDocNum IS NOT NULL "

        If VEsAgente = 1 Then

            SQLTPD &= "AND T0.Slpcode = " & vCodAgte.ToString & " "

        End If

        SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode,T0.CardCode "


        '--'--FACTURADO,Articulo-Agte,Clte
        SQLTPD &= "SELECT CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS ItemCode,"
        SQLTPD &= "SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,T0.CardCode AS IdClte,"
        SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null  THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,"
        SQLTPD &= "CAST(CASE WHEN T1.ItemCode IS NULL THEN 'NCARGO' ELSE 'FACTURACION' END AS CHAR(20))AS TipoMov,"
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END + "
        SQLTPD &= "RTRIM(LTRIM(T0.CardCode)) AS VARCHAR(22)) AS ArtAgte,"
        SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
        SQLTPD &= "INTO #Tem_Fact "
        SQLTPD &= "FROM OINV T0 "
        SQLTPD &= "INNER JOIN INV1 T1 ON T1.DocEntry = T0.DocEntry "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "

        If VEsAgente = 1 Then

            SQLTPD &= "AND T0.Slpcode = " & vCodAgte.ToString & " "

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
        SQLTPD &= "SELECT T0.ItemCode AS Articulo,"
        SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 'NOTAS DE CARGO' ELSE T3.ItemName END AS ItemName,"
        SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 998 ELSE T3.ItmsGrpCod END AS ItmsGrpCod,"
        SQLTPD &= "CASE WHEN T3.ItmsGrpCod IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T4.ItmsGrpNam END AS ItmsGrpNam,"
        SQLTPD &= "T0.TipoMov,T0.IdAgente,T6.SlpName  AS NomAgte,T0.IdClte,T7.CardName AS NomClte,T0.ArtAgte,"
        SQLTPD &= "CASE WHEN T1.CANTIDAD IS NULL THEN T0.CANTIDAD ELSE T0.CANTIDAD - T1.CANTIDAD END CANT_TOT,"
        SQLTPD &= "CASE WHEN T1.TotalSinIva IS NULL THEN T0.TotalSinIva ELSE T0.TotalSinIva - T1.TotalSinIva END MONT_TOT,"
        SQLTPD &= "CASE WHEN T2.CANTIDAD IS NULL THEN 0 ELSE T2.CANTIDAD END AS CantDev,"
        SQLTPD &= "CASE WHEN T1.CANTIDAD IS NULL THEN T0.CANTIDAD ELSE T0.CANTIDAD - T1.CANTIDAD END  - "
        SQLTPD &= "CASE WHEN T2.CANTIDAD IS NULL THEN 0 ELSE T2.CANTIDAD END AS CANT_NETA,"
        SQLTPD &= "CASE WHEN T2.TotalSinIva IS NULL THEN 0 ELSE T2.TotalSinIva END AS MontoDev,"
        SQLTPD &= "T0.TotalSinIva - "
        SQLTPD &= "(CASE WHEN T1.TotalSinIva IS NULL THEN 0 ELSE T1.TotalSinIva END + "
        SQLTPD &= "CASE WHEN T2.TotalSinIva IS NULL THEN 0 ELSE T2.TotalSinIva END) "
        SQLTPD &= "AS  MONTO_NETA,"

        SQLTPD &= "CASE WHEN T5.CANTIDAD IS NULL THEN 0 ELSE T5.CANTIDAD END AS CantPP,"
        SQLTPD &= "CASE WHEN T5.TotalSinIva IS NULL THEN 0 ELSE T5.TotalSinIva END AS MontoPP,"

        SQLTPD &= "CASE WHEN T2.TotalSinIva IS NULL THEN 0 ELSE  T2.TotalSinIva END + "
        SQLTPD &= "CASE WHEN T5.TotalSinIva IS NULL THEN 0 ELSE  T5.TotalSinIva END "
        SQLTPD &= "AS MontoNC "

        SQLTPD &= "INTO #T_DET_VTA "
        SQLTPD &= "FROM #Tem_Fact T0 "
        SQLTPD &= "LEFT JOIN #Tem_Canc T1 ON T0.ArtAgte = T1.ArtAgte "
        SQLTPD &= "LEFT JOIN #Tem_Dev T2 ON T0.ArtAgte = T2.ArtAgte "
        SQLTPD &= "LEFT JOIN #Tem_DesPP T5 ON T0.ArtAgte = T5.ArtAgte "
        SQLTPD &= "LEFT JOIN OITM T3 ON T0.ItemCode = T3.ItemCode "
        SQLTPD &= "LEFT JOIN OITB T4 ON T3.ItmsGrpCod = T4.ItmsGrpCod "
        SQLTPD &= "LEFT JOIN OSLP T6 ON T6.SlpCode = T0.IdAgente  "
        SQLTPD &= "LEFT JOIN OCRD T7 ON T7.CardCode = T0.IdClte "

        SQLTPD &= "DROP TABLE #Tem_Canc "
        SQLTPD &= "DROP TABLE #Tem_DesPP "
        SQLTPD &= "DROP TABLE #Tem_Dev "
        SQLTPD &= "DROP TABLE #Tem_Fact "


        '--'--VENTAS POR AGENTE
        SQLTPD &= "SELECT T0.IdAgente,NomAgte,"
        SQLTPD &= "SUM(T0.CANT_TOT) AS PzsVta,SUM(T0.MONT_TOT) AS MontoVta,"
        SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev,"
        SQLTPD &= "SUM(T0.CANT_NETA) AS PzsNeta,SUM(T0.MONTO_NETA) AS MontoNeto,"
        SQLTPD &= "SUM(T0.CantPP) AS CantPP,SUM(T0.MontoPP) AS MontoPP,"
        SQLTPD &= "SUM(T0.MontoNC) AS MontoNC,"
        SQLTPD &= "1 AS Llave "
        SQLTPD &= "INTO #T_VTA_AGTES "
        SQLTPD &= "FROM #T_DET_VTA T0 GROUP BY IdAgente,NomAgte ORDER BY  MontoNeto DESC "


        '--'--MONTO TOTAL DE VENTAS 
        SQLTPD &= "SELECT "
        SQLTPD &= "SUM(T0.PzsVta) AS PzsVta,SUM(T0.MontoVta) AS MontoVta,"
        SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev,"
        SQLTPD &= "SUM(T0.PzsNeta) AS PzsNeta,SUM(T0.MontoNeto) AS MontoNeto,"
        SQLTPD &= "SUM(T0.CantPP) AS CantPP,SUM(T0.MontoPP) AS MontoPP,"
        SQLTPD &= "SUM(T0.MontoNC) AS MontoNC,"
        SQLTPD &= "1 AS Llave "
        SQLTPD &= "INTO #T_VTA_TOTAL "
        SQLTPD &= "FROM #T_VTA_AGTES T0 "


        '--VENTAS TOTALES AGENTES,CLIENTES 
        SQLTPD &= "SELECT T0.IdAgente,T0.IdClte,"
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.IdAgente AS VARCHAR(2)))) + RTRIM(LTRIM(T0.IdClte)) AS VARCHAR(17)) AS ArtClte,"
        SQLTPD &= "SUM(T0.CANT_TOT) AS PzsVta,SUM(T0.MONT_TOT) AS MontoVta,"
        SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev,"
        SQLTPD &= "SUM(T0.CANT_NETA) AS PzsNeta,SUM(T0.MONTO_NETA) AS MontoNeto,"
        SQLTPD &= "SUM(T0.CantPP) AS CantPP,SUM(T0.MontoPP) AS MontoPP,"
        SQLTPD &= "SUM(T0.MontoNC) AS MontoNC "
        SQLTPD &= "INTO #T_AGTE_CLTE "
        SQLTPD &= "FROM #T_DET_VTA T0 GROUP BY T0.IdAgente,T0.IdClte ORDER BY MontoNeto DESC "


        '--VENTAS TOTALES AGENTES,CLIENTES,LINEAS
        SQLTPD &= "SELECT T0.IdAgente,T0.NomAgte,T0.IdClte,T0.NomClte,T0.ItmsGrpCod,T0.ItmsGrpNam,"
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.IdAgente AS VARCHAR(2)))) + RTRIM(LTRIM(T0.IdClte)) AS VARCHAR(17)) AS ArtClte,"
        SQLTPD &= "SUM(T0.CANT_TOT) AS PzsVta,SUM(T0.MONT_TOT) AS MontoVta,"
        SQLTPD &= "SUM(T0.CantDev) AS CantDev,SUM(T0.MontoDev) AS MontoDev,"
        SQLTPD &= "SUM(T0.CANT_NETA) AS PzsNeta,SUM(T0.MONTO_NETA) AS MontoNeto,"
        SQLTPD &= "SUM(T0.CantPP) AS CantPP,SUM(T0.MontoPP) AS MontoPP,"
        SQLTPD &= "SUM(T0.MontoNC) AS MontoNC "
        SQLTPD &= "INTO #T_A_C_LINEA "
        SQLTPD &= "FROM #T_DET_VTA T0 GROUP BY IdAgente,T0.NomAgte,IdClte,T0.NomClte,ItmsGrpCod,ItmsGrpNam ORDER BY MontoNeto DESC "


        '--VENTAS TOTALES DE CLIENTES CON DETALLE DE DEVOLUCIONES,DESCUENTOS PP ETC./Comprobado
        SQLTPD &= "SELECT  T0.IdAgente,T0.IdClte AS Cliente,"
        SQLTPD &= "T2.CardName AS Nombre,"
        SQLTPD &= "T2.City AS Ciudad,T2.State1 AS Estado,"
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
        SQLTPD &= "INTO #T_DET_CLTES "
        SQLTPD &= "FROM #T_AGTE_CLTE T0 "
        SQLTPD &= "LEFT JOIN #T_VTA_AGTES T1 ON T0.IdAgente = T1.IdAgente "
        SQLTPD &= "LEFT JOIN OCRD T2 ON T2.CardCode = T0.IdClte "
        SQLTPD &= "UNION ALL "
        SQLTPD &= "SELECT T3.SlpCode AS IdAgente,T3.CardCode AS Cliente,"
        SQLTPD &= "T3.CardName AS Nombre,"
        SQLTPD &= "T3.City AS Ciudad,T3.State1 AS Estado,"
        SQLTPD &= "0.0 AS MontoVta,0.0 AS PzsVta,0.0 AS PorVta,"
        SQLTPD &= "0.0 AS MontoDev,0.0 AS CantDev,0.0 AS PorDEV,"
        SQLTPD &= "0.0 AS MontoNeto,0.0 AS PzsNeta,0.0 AS MontoPP,"
        SQLTPD &= "0.0 AS PorPP,0.0 AS MontoNC,CAST(RTRIM(LTRIM(CAST(T3.SlpCode AS VARCHAR(2)))) + RTRIM(LTRIM(T3.CardCode)) AS VARCHAR(17)) AS ArtClte "
        SQLTPD &= "FROM OCRD T3 WHERE T3.CardType = 'C'  AND T3.frozenFor <> 'Y' AND "
        If VEsAgente = 1 Then

            SQLTPD &= "T3.Slpcode = " & vCodAgte.ToString & " AND "

        End If
        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T3.SlpCode AS VARCHAR(2)))) + RTRIM(LTRIM(T3.CardCode)) AS VARCHAR(17)) NOT IN "
        SQLTPD &= "(SELECT T4.ArtClte FROM #T_AGTE_CLTE T4) order by IdAgente "


        '--SE CONSULTA LA VENTA DE TODOS LOS MESES DE TODOS LOS CLIENTES
        SQLTPD &= "SELECT T0.Cliente,SUM((T1.DocTotal - T1.VatSum) - T1.TotalExpns) AS Total,MONTH(T1.DocDate) AS Mes,YEAR(T1.DocDate) AS Anio "
        SQLTPD &= "INTO #T_VTA_TOD_MES FROM #T_DET_CLTES T0 INNER JOIN OINV T1 ON "
        SQLTPD &= "T0.Cliente = T1.CardCode And T1.DocDate <= GETDATE() "
        SQLTPD &= "GROUP BY Cliente,MONTH(T1.DocDate),YEAR(T1.DocDate) "

        SQLTPD &= "UNION ALL "

        SQLTPD &= "SELECT T0.Cliente,SUM((T1.DocTotal - T1.VatSum) - T1.TotalExpns) * -1 AS Total,MONTH(T1.DocDate) AS Mes,YEAR(T1.DocDate) AS Anio "
        SQLTPD &= "FROM #T_DET_CLTES T0 "
        SQLTPD &= "INNER JOIN ORIN T1 ON "
        SQLTPD &= "T0.Cliente = T1.CardCode And T1.DocDate <= GETDATE() "
        SQLTPD &= "AND (T1.EDocPrefix = 'NC' or T1.EDocPrefix is null or T1.EDocPrefix = 'F') AND T1.DocType  = 'I' AND Series <> 49 "
        SQLTPD &= "GROUP BY Cliente,MONTH(T1.DocDate),YEAR(T1.DocDate) "


        '--VENTA MAS ALTA POR CLIENTE
        SQLTPD &= "SELECT Cliente,SUM(Total) AS VtaMasAlta,Mes,Anio,ROW_NUMBER() OVER (PARTITION BY Cliente ORDER BY Cliente,SUM(Total) DESC) as OrdenVta,"
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
        SQLTPD &= "END "
        SQLTPD &= "AS MesLetra "
        SQLTPD &= "INTO #VTA_MAYOR_C "
        SQLTPD &= "FROM #T_VTA_TOD_MES GROUP BY Cliente,Mes,Anio ORDER BY Cliente,SUM(Total) DESC "


        SQLTPD &= "DROP TABLE #T_VTA_TOD_MES "


        '--ÚLTIMA VENTA, FACTURA MÁS ALTA
        SQLTPD &= "SELECT T3.CardCode AS Cliente, MAX(T4.DocDate) AS Ult_Vta,MAX(t4.DocTotal) AS Fac_MasAlta, "
        SQLTPD &= "DATEDIFF(DAY, MAX(T4.DocDate), GETDATE()) AS DiasTrans "
        SQLTPD &= "INTO #ULT_VTA_CLTE "
        SQLTPD &= "FROM OCRD T3 LEFT JOIN OINV T4 ON T4.CardCode = T3.CardCode AND T4.DocDate <= GETDATE() WHERE T3.CardType = 'C'  AND T3.frozenFor <> 'Y' AND "

        'If VEsAgente = 1 Then

        '    SQLTPD &= "T3.Slpcode = " & vCodAgte.ToString & " AND "

        'End If

        SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T3.SlpCode AS VARCHAR(2)))) + RTRIM(LTRIM(T3.CardCode)) AS VARCHAR(17)) IN "
        SQLTPD &= "(SELECT T5.ArtClte FROM #T_DET_CLTES T5) GROUP BY T3.CardCode "


        '--VENTAS TOTALES POR AGENTES CON DETALLE DE DEVOLUCIONES,DESCUENTOS PP ETC.
        '--**COMPROBADO, PERFECTO**
        SQLTPD &= "SELECT T0.IdAgente,T0.NomAgte,"
        SQLTPD &= "T0.MontoVta,"
        SQLTPD &= "CASE WHEN T0.MontoVta = 0 OR  T1.MontoVta = 0 THEN 0 ELSE "
        SQLTPD &= "T0.MontoVta * 100 / T1.MontoVta END AS PorVta,T0.PzsVta,"
        SQLTPD &= "T0.MontoDev,"
        SQLTPD &= "CASE WHEN T0.MontoDev = 0 OR  T0.MontoVta = 0 THEN 0 ELSE "
        SQLTPD &= "T0.MontoDev * 100 / T0.MontoVta END AS PorDEV,T0.CantDev,"
        SQLTPD &= "T0.MontoNeto,T0.PzsNeta,T0.MontoPP,"
        SQLTPD &= "CASE WHEN T0.MontoPP = 0 OR  T0.MontoNeto = 0 THEN 0 ELSE "
        SQLTPD &= "T0.MontoPP * 100 / T0.MontoNeto END AS PorPP,"
        SQLTPD &= "T0.MontoNC "
        SQLTPD &= "FROM #T_VTA_AGTES T0 LEFT JOIN #T_VTA_TOTAL T1 ON T0.Llave = T1.Llave"

        If VEsAgente = 1 Then
            SQLTPD &= " WHERE T0.IdAgente = " & vCodAgte
        End If

        If VEsAgente <> 1 Then
            SQLTPD &= " UNION ALL "
            SQLTPD &= "SELECT 0,'MONTOS TOTALES' AS NomAgte,MontoVta,100.00 AS PorVta,PzsVta,"
            SQLTPD &= "MontoDev,0.00 AS PorDEV,CantDev,"
            SQLTPD &= "MontoNeto,PzsNeta,MontoPP,0 AS PorPP,MontoNC "
            SQLTPD &= "FROM #T_VTA_TOTAL T3; "
        Else
            SQLTPD &= "; "
        End If

       


        '--REPORTE DE VENTAS POR CLIENTES - AGENTE CON ULTIMA VENTA Y VENTA MAS ALTA A LA FECHA
        '--**COMPROBADO**
        SQLTPD &= "SELECT T0.IdAgente, T0.Cliente, T0. Nombre , T0. Ciudad , T0.Estado, T0.MontoVta,  T0.PorVta,T0.PzsVta,T1.VtaMasAlta,"
        SQLTPD &= "T1.MesLetra, T1.Anio, T2.Ult_Vta, T2.DiasTrans, T2.Fac_MasAlta, T0.MontoDev, T0.PorDEV, T0.CantDev,"
        SQLTPD &= "T0.MontoNeto, T0.PzsNeta, T0.MontoPP, T0.PorPP, T0.MontoNC,T3.SlpName  AS NomAgte "
        SQLTPD &= "FROM #T_DET_CLTES T0 "
        SQLTPD &= "LEFT JOIN #VTA_MAYOR_C T1 ON T0.Cliente = T1.Cliente AND T1.OrdenVta = 1 "
        SQLTPD &= "LEFT JOIN #ULT_VTA_CLTE T2 ON T0.Cliente = T2.Cliente "
        SQLTPD &= "LEFT JOIN OSLP T3 ON T3.SlpCode = T0.IdAgente  ORDER BY T0.IdAgente,T0.MontoNeto DESC; "


        '--VENTAS TOTALES POR AGENTES CLIENTES LINEAS.
        '--*COMPROBADO**
        SQLTPD &= "SELECT T0.IdAgente,T0.NomAgte,T0.IdClte,T0.NomClte,T0.ItmsGrpCod AS Linea,T0.ItmsGrpNam,"
        SQLTPD &= "T0.MontoVta,"
        SQLTPD &= "CASE WHEN T0.MontoVta = 0 OR  T1.MontoVta = 0 THEN 0 ELSE "
        SQLTPD &= "T0.MontoVta * 100 / T1.MontoVta END AS PorVta,T0.PzsVta,"
        SQLTPD &= "T0.MontoDev,"
        SQLTPD &= "CASE WHEN T0.MontoDev = 0 OR  T0.MontoVta = 0 THEN 0 ELSE "
        SQLTPD &= "T0.MontoDev * 100 / T0.MontoVta END AS PorDEV,T0.CantDev,"
        SQLTPD &= "T0.MontoNeto,T0.PzsNeta,T0.MontoPP,"
        SQLTPD &= "CASE WHEN T0.MontoPP = 0 OR  T0.MontoNeto = 0 THEN 0 ELSE "
        SQLTPD &= "T0.MontoPP * 100 / T0.MontoNeto END AS PorPP,"
        SQLTPD &= "T0.MontoNC "
        SQLTPD &= "FROM #T_A_C_LINEA T0 "
        SQLTPD &= "LEFT JOIN #T_AGTE_CLTE T1 ON T0.IdAgente = T1.IdAgente AND T0.IdClte = T1.IdClte "
        SQLTPD &= "ORDER BY T0.IdAgente,MontoNeto DESC; "


        '--DETALLE VENTAS ARTICULOS
        '--*COMPROBADO***
        SQLTPD &= "SELECT Articulo, ItemName,  ItmsGrpNam, IdAgente, NomAgte, IdClte, NomClte, MONT_TOT, CANT_TOT,"
        SQLTPD &= "MontoDev, CantDev, MONTO_NETA, CANT_NETA, MontoPP,  MontoNC, ItmsGrpCod, TipoMov, ArtAgte, CantPP "
        SQLTPD &= "FROM #T_DET_VTA ORDER BY IdAgente,ItmsGrpNam,IdClte,Monto_Neta DESC; "


        '--*VENTAS DE TODOS LOS MESES DE TODOS LOS CLIENTES**
        SQLTPD &= "SELECT T0.Cliente,T0.MesLetra,T0.Anio,T0.VtaMasAlta,T0.Mes,T1.CardCode AS IdClte,T1.CardName AS NomClte,"
        SQLTPD &= "T2.SlpName AS NomAgte FROM #VTA_MAYOR_C T0 "
        SQLTPD &= "LEFT JOIN OCRD T1 ON T1.CardCode = T0.Cliente "
        SQLTPD &= "LEFT JOIN OSLP T2 ON T1.SlpCode = T2.SlpCode ORDER BY T0.Cliente,T0.Anio DESC,T0.Mes DESC; "


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


        'DsVtasDet.Tables(1).TableName = "DetalleLinea"
        'DsVtasDet.Tables(2).TableName = "DetalleArticulo"
        'DsVtasDet.Tables(3).TableName = "Articulos"


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
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 35
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = False
            If VEsAgente <> 1 Then
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End If

            .MultiSelect = False
            .AllowUserToAddRows = False
            'Color de linea del grid
            .Columns(0).Visible = False
            .Columns(1).HeaderText = "Vendedor"
            .Columns(1).Width = 150

            .Columns(2).HeaderText = "$ Ventas Totales"
            .Columns(2).Width = 100
            .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(3).HeaderText = "% Vtas."
            .Columns(3).Width = 35
            .Columns(3).DefaultCellStyle.Format = "###.0"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(4).HeaderText = "Piezas"
            .Columns(4).Width = 65
            .Columns(4).DefaultCellStyle.Format = "###,###,###"
            .Columns(4).Visible = False

            .Columns(5).HeaderText = "$ Monto Devuelto"
            .Columns(5).Width = 90
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(6).HeaderText = "% Dvol. S/Vta"
            .Columns(6).Width = 50
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Format = "###.0"

            .Columns(7).HeaderText = "Piezas Devueltas"
            .Columns(7).Width = 60
            .Columns(7).DefaultCellStyle.Format = "###,###,###"
            .Columns(7).Visible = False

            .Columns(8).HeaderText = "Ventas Netas"
            .Columns(8).Width = 100
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(9).HeaderText = "Piezas Netas"
            .Columns(9).Width = 65
            .Columns(9).DefaultCellStyle.Format = "###,###,###"
            .Columns(9).Visible = False


            .Columns(10).HeaderText = "Descuentos Pronto Pago"
            .Columns(10).Width = 90
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(10).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(11).HeaderText = "% PP S/Vta"
            .Columns(11).Width = 50
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(11).DefaultCellStyle.Format = "###.0"
            '  .Columns(7).Visible = False

            .Columns(12).HeaderText = "Notas de Credito"
            .Columns(12).Width = 90
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(12).DefaultCellStyle.Format = "###,###,###.00"

        End With


        dvClientes.Table = DsVtasDet.Tables("VtaCltes")

        With Me.DgClientes
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 35
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

            .Columns(5).HeaderText = "$ Ventas Totales"
            .Columns(5).Width = 80
            .Columns(5).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(6).HeaderText = "% Vtas."
            .Columns(6).Width = 35
            .Columns(6).DefaultCellStyle.Format = "###.0"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(7).HeaderText = "Piezas"
            .Columns(7).Width = 65
            .Columns(7).DefaultCellStyle.Format = "###,###,###"
            .Columns(7).Visible = False

            .Columns(8).HeaderText = "$ Mayor Vta Mensual"
            .Columns(8).Width = 75
            .Columns(8).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(9).HeaderText = "Mes Vta"
            .Columns(9).Width = 63

            .Columns(10).HeaderText = "Año Vta"
            .Columns(10).Width = 33

            .Columns(11).HeaderText = "Fecha Última Vta"
            .Columns(11).Width = 63

            .Columns(12).HeaderText = "Días Trans."
            .Columns(12).Width = 40

            .Columns(13).HeaderText = "Factura Más Alta"
            .Columns(13).Width = 65
            .Columns(13).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(14).HeaderText = "$ Monto Devuelto"
            .Columns(14).Width = 71
            .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(14).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(15).HeaderText = "% Dvol. S/Vta"
            .Columns(15).Width = 50
            .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(15).DefaultCellStyle.Format = "###.0"

            .Columns(16).HeaderText = "Piezas Devueltas"
            .Columns(16).Width = 60
            .Columns(16).DefaultCellStyle.Format = "###,###,###"
            .Columns(16).Visible = False

            .Columns(17).HeaderText = "Ventas Netas"
            .Columns(17).Width = 90
            .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(17).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(18).HeaderText = "Piezas Netas"
            .Columns(18).Width = 65
            .Columns(18).DefaultCellStyle.Format = "###,###,###"
            .Columns(18).Visible = False

            .Columns(19).HeaderText = "Desc. PP"
            .Columns(19).Width = 70
            .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(19).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(20).HeaderText = "% PP S/Vta"
            .Columns(20).Width = 40
            .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(20).DefaultCellStyle.Format = "###.0"
            '  .Columns(7).Visible = False

            .Columns(21).HeaderText = "Notas de Credito"
            .Columns(21).Width = 70
            .Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(21).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(22).HeaderText = "Vendedor"
            .Columns(22).Width = 150


        End With

        dvLineas.Table = DsVtasDet.Tables("VtaLineas")

        With Me.DgLineas
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 35
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
            .Columns(5).Width = 120

            .Columns(6).HeaderText = "$ Ventas Totales"
            .Columns(6).Width = 80
            .Columns(6).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(7).HeaderText = "% Vtas."
            .Columns(7).Width = 35
            .Columns(7).DefaultCellStyle.Format = "###.0"
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(8).HeaderText = "Pzas"
            .Columns(8).Width = 35
            .Columns(8).DefaultCellStyle.Format = "###,###,###"
            .Columns(8).Visible = True

            .Columns(9).HeaderText = "$ Monto Devuelto"
            .Columns(9).Width = 75
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(10).HeaderText = "% Dvol. S/Vta"
            .Columns(10).Width = 50
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(10).DefaultCellStyle.Format = "###.0"

            .Columns(11).HeaderText = "Pzas Dev."
            .Columns(11).Width = 35
            .Columns(11).DefaultCellStyle.Format = "###,###,###"
            .Columns(11).Visible = True

            .Columns(12).HeaderText = "Ventas Netas"
            .Columns(12).Width = 85
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(12).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(13).HeaderText = "Pzas Net"
            .Columns(13).Width = 35
            .Columns(13).DefaultCellStyle.Format = "###,###,###"
            .Columns(13).Visible = True

            .Columns(14).HeaderText = "Desc. PP"
            .Columns(14).Width = 71
            .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(14).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(15).HeaderText = "% PP S/Vta"
            .Columns(15).Width = 40
            .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(15).DefaultCellStyle.Format = "###.0"
            '  .Columns(7).Visible = False

            .Columns(16).HeaderText = "Notas de Credito"
            .Columns(16).Width = 80
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
            .Columns(1).Width = 62

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
            .Columns(1).Width = 205

            .Columns(2).Visible = False
            .Columns(3).Visible = False
            .Columns(4).Visible = False
            .Columns(5).Visible = False
            .Columns(6).Visible = False

            .Columns(7).HeaderText = "$ Ventas Totales"
            .Columns(7).Width = 75
            .Columns(7).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(8).HeaderText = "Pzas"
            .Columns(8).Width = 30
            .Columns(8).DefaultCellStyle.Format = "###,###,###"
            .Columns(8).Visible = True

            .Columns(9).HeaderText = "$ Monto Devuelto"
            .Columns(9).Width = 65
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(10).HeaderText = "Pzas Dev."
            .Columns(10).Width = 35
            .Columns(10).DefaultCellStyle.Format = "###,###,###"
            .Columns(10).Visible = True

            .Columns(11).HeaderText = "Ventas Netas"
            .Columns(11).Width = 75
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(11).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(12).HeaderText = "Pzas Net"
            .Columns(12).Width = 35
            .Columns(12).DefaultCellStyle.Format = "###,###,###"
            .Columns(12).Visible = True


            .Columns(13).HeaderText = "Desc. PP"
            .Columns(13).Width = 71
            .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(13).DefaultCellStyle.Format = "###,###,###.00"


            .Columns(14).HeaderText = "Notas de Credito"
            .Columns(14).Width = 80
            .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(14).DefaultCellStyle.Format = "###,###,###.00"


            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).Visible = False
            .Columns(18).Visible = False

        End With



        'dvLineas

        ''**********************************************************************************************************************************


        'dvLineas.Table = DsVtasDet.Tables("DetalleLinea")

        ''GrdTodosArt.DataSource = dvTodosArt

        ''Private dvLineas As New DataView
        ''Private dvAgentes As New DataView
        ''Private dvArticulos As New DataView


        'With GrdConProd
        '    ' .DataMember = "EncVtasTot"
        '    .DataSource = dvLineas
        '    'Color de Renglones en Grid
        '    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        '    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        '    .DefaultCellStyle.BackColor = Color.AliceBlue
        '    .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        '    .RowHeadersVisible = False
        '    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        '    .MultiSelect = False
        '    .AllowUserToAddRows = False
        '    .ReadOnly = True

        '    .Columns(0).HeaderText = "Linea"
        '    .Columns(0).Width = 150

        '    .Columns(1).HeaderText = "Cantidad Piezas"
        '    .Columns(1).Width = 65
        '    .Columns(1).DefaultCellStyle.Format = "###,###,###"

        '    .Columns(2).HeaderText = "$ Venta Total"
        '    .Columns(2).Width = 100
        '    .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
        '    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '    .Columns(3).HeaderText = "% Sobre Vta."
        '    .Columns(3).Width = 60
        '    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '    .Columns(3).DefaultCellStyle.Format = "###.00"

        '    .Columns(4).HeaderText = "Cant. Piezas Devueltas"
        '    .Columns(4).Width = 60
        '    .Columns(4).DefaultCellStyle.Format = "###,###,###"

        '    .Columns(5).HeaderText = "$ Monto Devuelto"
        '    .Columns(5).Width = 94
        '    .Columns(5).DefaultCellStyle.Format = "###,###,###.00"
        '    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '    .Columns(6).HeaderText = "% Dev. Sobre Vta."
        '    .Columns(6).Width = 60
        '    .Columns(6).DefaultCellStyle.Format = "###.00"
        '    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '    .Columns(7).HeaderText = "Cant. Neta"
        '    .Columns(7).Width = 65
        '    .Columns(7).DefaultCellStyle.Format = "###,###,###"

        '    .Columns(8).HeaderText = "$ Monto Neto"
        '    .Columns(8).Width = 95
        '    .Columns(8).DefaultCellStyle.Format = "###,###,###.00"
        '    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '    .Columns(9).Visible = False

        '    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        '    .DefaultCellStyle.BackColor = Color.AliceBlue
        'End With


        'dvAgentes.Table = DsVtasDet.Tables("DetalleArticulo")

        'dvAgentes.Sort = "MontoNeto DESC"


        '' Establecer el DataSource y el DataMember para el DataGridview Detalle   
        'With GrdConLinea
        '    .DataSource = dvAgentes
        '    '.DataMember = "EncVtasTot.Relacion1"
        '    'Color de Renglones en Grid
        '    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        '    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        '    .DefaultCellStyle.BackColor = Color.AliceBlue
        '    .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        '    .RowHeadersVisible = False
        '    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        '    .MultiSelect = False
        '    .AllowUserToAddRows = False
        '    .ReadOnly = True
        '    .Columns(0).HeaderText = "Agente"
        '    .Columns(0).Width = 170

        '    .Columns(1).HeaderText = "Cantidad Piezas"
        '    .Columns(1).Width = 65
        '    .Columns(1).DefaultCellStyle.Format = "###,###,###"

        '    .Columns(2).HeaderText = "$ Venta Total"
        '    .Columns(2).Width = 100
        '    .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
        '    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '    .Columns(3).HeaderText = "% Sobre Vta."
        '    .Columns(3).Width = 60
        '    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '    .Columns(3).DefaultCellStyle.Format = "###.00"

        '    .Columns(4).HeaderText = "Cant. Piezas Devueltas"
        '    .Columns(4).Width = 60
        '    .Columns(4).DefaultCellStyle.Format = "###,###,###"

        '    .Columns(5).HeaderText = "$ Monto Devuelto"
        '    .Columns(5).Width = 94
        '    .Columns(5).DefaultCellStyle.Format = "###,###,###.00"
        '    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '    .Columns(6).HeaderText = "% Dev. Sobre Vta."
        '    .Columns(6).Width = 60
        '    .Columns(6).DefaultCellStyle.Format = "###.00"
        '    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '    .Columns(7).HeaderText = "Cant. Neta"
        '    .Columns(7).Width = 65
        '    .Columns(7).DefaultCellStyle.Format = "###,###,###"

        '    .Columns(8).HeaderText = "$ Monto Neto"
        '    .Columns(8).Width = 95
        '    .Columns(8).DefaultCellStyle.Format = "###,###,###.00"
        '    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '    .Columns(9).Visible = False
        '    .Columns(10).Visible = False
        '    .Columns(11).Visible = False

        '    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        '    .DefaultCellStyle.BackColor = Color.AliceBlue


        'End With

        'dvTodosArt.Table = DsVtasDet.Tables("Articulos")
        'dvTodosArt.Sort = "Agente ASC,DesLinea ASC,MontoNeto DESC"
        'GrdTodosArt.DataSource = dvTodosArt


        'dvArticulos.Table = DsVtasDet.Tables("Articulos")
        'dvArticulos.Sort = "MontoNeto DESC"



        'With GrdDetArt
        '    .DataSource = dvArticulos
        '    'Color de Renglones en Grid
        '    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        '    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        '    .DefaultCellStyle.BackColor = Color.AliceBlue
        '    .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        '    .RowHeadersVisible = False
        '    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        '    .MultiSelect = False
        '    .AllowUserToAddRows = False
        '    .ReadOnly = True

        '    .Columns(0).HeaderText = "Articulo"
        '    .Columns(0).Width = 100

        '    .Columns(1).HeaderText = "Descripción"
        '    .Columns(1).Width = 240

        '    .Columns(2).HeaderText = "Cantidad Piezas"
        '    .Columns(2).Width = 50
        '    .Columns(2).DefaultCellStyle.Format = "###,###,###"

        '    .Columns(3).HeaderText = "$ Venta Total"
        '    .Columns(3).Width = 75
        '    .Columns(3).DefaultCellStyle.Format = "###,###,###.00"
        '    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '    .Columns(4).HeaderText = "% Sobre Vta."
        '    .Columns(4).Width = 40
        '    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '    .Columns(4).DefaultCellStyle.Format = "###.00"

        '    .Columns(5).HeaderText = "Cant. Piezas Devueltas"
        '    .Columns(5).Width = 40
        '    .Columns(5).DefaultCellStyle.Format = "###,###,###"

        '    .Columns(6).HeaderText = "$ Monto Devuelto"
        '    .Columns(6).Width = 60
        '    .Columns(6).DefaultCellStyle.Format = "###,###,###.00"
        '    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '    .Columns(7).HeaderText = "% Dev. Sobre Vta."
        '    .Columns(7).Width = 40
        '    .Columns(7).DefaultCellStyle.Format = "###.00"
        '    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '    .Columns(8).HeaderText = "Cant. Neta"
        '    .Columns(8).Width = 50
        '    .Columns(8).DefaultCellStyle.Format = "###,###,###"

        '    .Columns(9).HeaderText = "$ Monto Neto"
        '    .Columns(9).Width = 75
        '    .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
        '    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        '    .Columns(10).Visible = False
        '    .Columns(11).Visible = False
        '    .Columns(12).Visible = False
        '    .Columns(13).Visible = False
        '    .Columns(14).Visible = False
        '    .Columns(15).Visible = False





        'End With

        ' cerrar la conexíón   
        With conexion2
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With

    End Sub

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

    Private Sub DgVtaAgte_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DgVtaAgte.SelectionChanged
        filtrar_Clientes()
    End Sub

    Private Sub DgClientes_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DgClientes.SelectionChanged
        filtrar_Lineas()
        filtrar_VtaMensual()
    End Sub

    Private Sub VtasClientesAgte_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-MX")
        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        'MsgBox(vCodAgte)

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
    Sub filtrar_Clientes()
        Try
            If DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value = 0 Then
                dvClientes.RowFilter = String.Empty
            Else
                dvClientes.RowFilter = "IdAgente =" & DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value.ToString

            End If

        Catch ex As Exception
        End Try

    End Sub

    Sub filtrar_Lineas()
        Try

            dvLineas.RowFilter = "IdAgente =" & DgClientes.Item(0, DgClientes.CurrentRow.Index).Value.ToString & " AND " & _
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
            dvArticulos.RowFilter = "ItmsGrpCod =" & DgLineas.Item(4, DgLineas.CurrentRow.Index).Value.ToString & " AND " & _
            "IdClte ='" & DgLineas.Item(2, DgLineas.CurrentRow.Index).Value.ToString & "' AND " & _
            "IdAgente =" & DgLineas.Item(0, DgLineas.CurrentRow.Index).Value.ToString

        Catch ex As Exception
            dvArticulos.RowFilter = "ItmsGrpCod =0"
        End Try
    End Sub

    Private Sub DgLineas_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles DgLineas.SelectionChanged
        filtrar_Articulos()
    End Sub

    Private Sub BtnAgentes_Click(sender As System.Object, e As System.EventArgs) Handles BtnAgentes.Click


        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)


        'Declaramos el nombre de las columnas
        oSheet.range("A3").value = "Vendedor"
        oSheet.range("B3").value = "Ventas Totales"
        oSheet.range("C3").value = "% Vtas. Tot."
        oSheet.range("D3").value = "Monto Devuelto"
        oSheet.range("E3").value = "% Dvol. Sobre Venta"
        oSheet.range("F3").value = " Ventas Netas"
        oSheet.range("G3").value = "Descuentos Pronto Pago"
        oSheet.range("H3").value = " % PP Sobre Venta"
        oSheet.range("I3").value = "Total Notas de Credito"


        'para poner la primera fila de los titulos en negrita
        oSheet.range("A3:I3").font.bold = True
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
            Dim cel3 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(3, fila_dt).Value), 0, Me.DgVtaAgte.Item(3, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(5, fila_dt).Value), 0, Me.DgVtaAgte.Item(5, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(6, fila_dt).Value), 0, Me.DgVtaAgte.Item(6, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(8, fila_dt).Value), 0, Me.DgVtaAgte.Item(8, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(10, fila_dt).Value), 0, Me.DgVtaAgte.Item(10, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(11, fila_dt).Value), 0, Me.DgVtaAgte.Item(11, fila_dt).Value)

            Dim cel9 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(12, fila_dt).Value), 0, Me.DgVtaAgte.Item(12, fila_dt).Value)

            fila_dt_excel = fila_dt + 4

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = FormatNumber(cel2, 2)
            oSheet.range("C" & fila_dt_excel).value = cel3
            oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
            oSheet.range("E" & fila_dt_excel).value = cel5
            oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
            oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
            oSheet.range("H" & fila_dt_excel).value = cel8

            oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)

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

    Private Sub BtnClientes_Click(sender As System.Object, e As System.EventArgs) Handles BtnClientes.Click
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
        oSheet.range("E3").value = "$ Ventas Totales"
        oSheet.range("F3").value = "% Vtas."
        oSheet.range("G3").value = "$ Venta Más ALta"
        oSheet.range("H3").value = "Mes Vta"
        oSheet.range("I3").value = "Año Vta"

        oSheet.range("J3").value = "Fecha Última Vta"
        oSheet.range("K3").value = "Días Trans."
        oSheet.range("L3").value = "Factura Mas ALta"
        oSheet.range("M3").value = "Monto Devuelto"
        oSheet.range("N3").value = "% Dvol. Sobre Venta"
        oSheet.range("O3").value = " Ventas Netas"
        oSheet.range("P3").value = "Descuentos Pronto Pago"
        oSheet.range("Q3").value = " % PP Sobre Venta"
        oSheet.range("R3").value = "Total Notas de Credito"
        oSheet.range("S3").value = "Vendedor"



        'para poner la primera fila de los titulos en negrita
        oSheet.range("A3:S3").font.bold = True
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
            Dim cel7 As String = IIf(IsDBNull(Me.DgClientes.Item(8, fila_dt).Value), 0, Me.DgClientes.Item(8, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DgClientes.Item(9, fila_dt).Value), 0, Me.DgClientes.Item(9, fila_dt).Value)

            Dim cel9 As String = IIf(IsDBNull(Me.DgClientes.Item(10, fila_dt).Value), 0, Me.DgClientes.Item(10, fila_dt).Value)
            Dim cel10 As String = IIf(IsDBNull(Me.DgClientes.Item(11, fila_dt).Value), 0, Me.DgClientes.Item(11, fila_dt).Value)
            Dim cel11 As String = IIf(IsDBNull(Me.DgClientes.Item(12, fila_dt).Value), 0, Me.DgClientes.Item(12, fila_dt).Value)
            Dim cel12 As String = IIf(IsDBNull(Me.DgClientes.Item(13, fila_dt).Value), 0, Me.DgClientes.Item(13, fila_dt).Value)
            Dim cel13 As String = IIf(IsDBNull(Me.DgClientes.Item(14, fila_dt).Value), 0, Me.DgClientes.Item(14, fila_dt).Value)
            Dim cel14 As String = IIf(IsDBNull(Me.DgClientes.Item(15, fila_dt).Value), 0, Me.DgClientes.Item(15, fila_dt).Value)
            Dim cel15 As String = IIf(IsDBNull(Me.DgClientes.Item(17, fila_dt).Value), 0, Me.DgClientes.Item(17, fila_dt).Value)
            Dim cel16 As String = IIf(IsDBNull(Me.DgClientes.Item(19, fila_dt).Value), 0, Me.DgClientes.Item(19, fila_dt).Value)
            Dim cel17 As String = IIf(IsDBNull(Me.DgClientes.Item(20, fila_dt).Value), 0, Me.DgClientes.Item(20, fila_dt).Value)
            Dim cel18 As String = IIf(IsDBNull(Me.DgClientes.Item(21, fila_dt).Value), 0, Me.DgClientes.Item(21, fila_dt).Value)
            Dim cel19 As String = IIf(IsDBNull(Me.DgClientes.Item(22, fila_dt).Value), 0, Me.DgClientes.Item(22, fila_dt).Value)

            fila_dt_excel = fila_dt + 4

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = cel3
            oSheet.range("D" & fila_dt_excel).value = cel4
            oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
            oSheet.range("F" & fila_dt_excel).value = cel6
            oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
            oSheet.range("H" & fila_dt_excel).value = cel8

            oSheet.range("I" & fila_dt_excel).value = cel9
            oSheet.range("J" & fila_dt_excel).value = cel10
            oSheet.range("K" & fila_dt_excel).value = cel11

            oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel12, 2)
            oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel13, 2)
            oSheet.range("N" & fila_dt_excel).value = cel14
            oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel15, 2)
            oSheet.range("P" & fila_dt_excel).value = FormatNumber(cel16, 2)
            oSheet.range("Q" & fila_dt_excel).value = cel17
            oSheet.range("R" & fila_dt_excel).value = FormatNumber(cel18, 2)
            oSheet.range("S" & fila_dt_excel).value = cel19

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

    Private Sub BtnLinea_Click(sender As System.Object, e As System.EventArgs) Handles BtnLinea.Click
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

    Private Sub BtnHistVtas_Click(sender As System.Object, e As System.EventArgs) Handles BtnHistVtas.Click


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

    Private Sub BtnArticulo_Click(sender As System.Object, e As System.EventArgs) Handles BtnArticulo.Click
        'oSheet.range("B3").value = "Ventas Totales"
        'oSheet.range("C3").value = "% Vtas. Tot."
        'oSheet.range("D3").value = "Monto Devuelto"
        'oSheet.range("E3").value = "% Dvol. Sobre Venta"
        'oSheet.range("F3").value = " Ventas Netas"
        'oSheet.range("G3").value = "Descuentos Pronto Pago"
        'oSheet.range("H3").value = " % PP Sobre Venta"
        'oSheet.range("I3").value = "Total Notas de Credito"

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


            'SQLTPD &= "SELECT Articulo, ItemName,  ItmsGrpNam, IdAgente, NomAgte, IdClte, NomClte, MONT_TOT, CANT_TOT,"
            'SQLTPD &= "MontoDev, CantDev, MONTO_NETA, CANT_NETA, MontoPP,  MontoNC, ItmsGrpCod, TipoMov, ArtAgte, CantPP "
            'SQLTPD &= "FROM #T_DET_VTA ORDER BY IdAgente,ItmsGrpNam,IdClte,Monto_Neta DESC; "


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
            'Dim cel15 As String = IIf(IsDBNull(Me.DgArticulos.Item(15, fila_dt).Value), 0, Me.DgArticulos.Item(15, fila_dt).Value)
            'Dim cel16 As String = IIf(IsDBNull(Me.DgArticulos.Item(16, fila_dt).Value), 0, Me.DgArticulos.Item(16, fila_dt).Value)
            'Dim cel17 As String = IIf(IsDBNull(Me.DgArticulos.Item(17, fila_dt).Value), 0, Me.DgArticulos.Item(17, fila_dt).Value)


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
            'oSheet.range("O" & fila_dt_excel).value = cel15
            'oSheet.range("P" & fila_dt_excel).value = cel15
            'oSheet.range("Q" & fila_dt_excel).value = cel15

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
End Class