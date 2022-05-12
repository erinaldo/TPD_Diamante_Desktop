Imports System.Data.SqlClient
Imports System.Data


Public Class Comisiones


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

        '--traco
       
        SQLTPD = "SELECT T2.SlpCode AS Agente,T4.SlpName AS NombreAgente,T2.DocNum AS DoctoSap,T2.DocDate AS FchFactura,CAST('' as Varchar(155)) AS FactSaldo,"
        SQLTPD &= "T0.DocNum AS IdPago,T0.DocDate AS FchPago,T2.CardCode AS CodClte,T2.CardName AS NombreClte,"
        SQLTPD &= "T2.DocTotal AS ImpFactConIva,T0.DocTotal AS BancoConIva,(T2.DocTotal - T2.VatSum) - T2.TotalExpns AS ImpFactSinIva,"
        SQLTPD &= "T1.SumApplied AS PagoConIva,"
        SQLTPD &= "T1.SumApplied / 1.16 AS PagoSinIva,"
        SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod = 150 THEN  T3.LineTotal ELSE 0 END) AS ImporteFlete,"
        SQLTPD &= "CASE WHEN T2.DocTotal <= 0 or SUM(CASE WHEN T5.ItmsGrpCod = 150 THEN  T3.LineTotal ELSE 0 END) <= 0 THEN 1 ELSE "
        SQLTPD &= "(((SUM(CASE WHEN T5.ItmsGrpCod = 150 THEN  T3.LineTotal ELSE 0 END) / (T2.DocTotal - T2.VatSum) - T2.TotalExpns)) - 1) * -1 "
        SQLTPD &= "END AS Factor,"
        SQLTPD &= "CASE WHEN T2.DocTotal <= 0 or SUM(CASE WHEN T5.ItmsGrpCod = 150 THEN  T3.LineTotal ELSE 0 END) <= 0 THEN T1.SumApplied / 1.16 ELSE "
        SQLTPD &= "((((SUM(CASE WHEN T5.ItmsGrpCod = 150 THEN  T3.LineTotal ELSE 0 END) / (T2.DocTotal - T2.VatSum) - T2.TotalExpns)) - 1) * -1) * "
        SQLTPD &= "(T1.SumApplied / 1.16) "
        SQLTPD &= "END AS BaseComision,"

        'CALCULO DE COMISION PASADO
        'SQLTPD &= "(CASE WHEN SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END) <= 0 OR "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  1 ELSE 0 END) <= 0 THEN 0 ELSE "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END) / "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  1 ELSE 0 END) END) * .01 "
        'SQLTPD &= "AS '%Comision',"


        'Consulta pasada Antes del 11 de Junio 2021
        'SQLTPD &= "SUM((CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END * .01) * Case When T5.ItmsGrpCod <> 150 Then  T3.LineTotal Else 0 End) / "
        'SQLTPD &= "  ((T2.DocTotal - T2.VatSum) - T2.TotalExpns) AS '%Comision',"
        SQLTPD &= "CASE WHEN (  (T1.SumApplied / 1.16) * (SUM((CASE WHEN T5.ItmsGrpCod <> 150 "
        SQLTPD &= " THEN  T3.U_BXP_Comision ELSE 0 END * .01) * CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.LineTotal ELSE 0 END) /    ((T2.DocTotal - T2.VatSum) - T2.TotalExpns)) ) <= 0 THEN 0 ELSE "


        SQLTPD &= " ROUND( ((T1.SumApplied / 1.16) * (SUM((CASE WHEN T5.ItmsGrpCod <> 150 "
        SQLTPD &= "THEN  T3.U_BXP_Comision ELSE 0 END * .01) * CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.LineTotal ELSE 0 END) /    ((T2.DocTotal - T2.VatSum) - T2.TotalExpns)))  / "

        SQLTPD &= " (CASE WHEN T2.DocTotal <= 0 or SUM(CASE WHEN T5.ItmsGrpCod = 150 THEN  T3.LineTotal ELSE 0 END)"
        SQLTPD &= "<= 0 THEN T1.SumApplied / 1.16 ELSE ((((SUM(CASE WHEN T5.ItmsGrpCod = 150 THEN  T3.LineTotal ELSE 0 END)"
        SQLTPD &= "/ (T2.DocTotal - T2.VatSum) - T2.TotalExpns)) - 1) * -1) * (T1.SumApplied / 1.16) END), 5) END AS '%Comision',"

        'CALCULO DE COMISION PASADO
        'SQLTPD &= "CASE WHEN (SUM(CASE WHEN T5.ItmsGrpCod = 150 THEN  T3.LineTotal ELSE 0 END)) > 0 "
        'SQLTPD &= "THEN "
        'SQLTPD &= "CASE WHEN T2.DocTotal <= 0 or SUM(CASE WHEN T5.ItmsGrpCod = 150 THEN  T3.LineTotal ELSE 0 END) <= 0 THEN 0 ELSE "
        'SQLTPD &= "((((SUM(CASE WHEN T5.ItmsGrpCod = 150 THEN  T3.LineTotal ELSE 0 END) / (T2.DocTotal - T2.VatSum) - T2.TotalExpns)) - 1) * -1) * "
        'SQLTPD &= "(T1.SumApplied / 1.16) "
        'SQLTPD &= "END * "
        'SQLTPD &= "(CASE WHEN SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END) <= 0 OR "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  1 ELSE 0 END) <= 0 THEN 0 ELSE "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END) / "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  1 ELSE 0 END) END * .01) "
        'SQLTPD &= "ELSE "
        'SQLTPD &= "(T1.SumApplied / 1.16)  * "
        'SQLTPD &= "(CASE WHEN SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END) <= 0 OR "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  1 ELSE 0 END) <= 0 THEN 0 ELSE "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END) / "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 150 THEN  1 ELSE 0 END) END * .01) END AS TotComision,"


        SQLTPD &= "	   (T1.SumApplied / 1.16) * (SUM((CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END * .01) * CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.LineTotal ELSE 0 END) / "
        SQLTPD &= "   ((T2.DocTotal - T2.VatSum) - T2.TotalExpns)) AS TotComision,"


        SQLTPD &= "ROW_NUMBER() OVER(PARTITION BY T0.DocNum ORDER BY T0.DocNum) as OrdPago "
        SQLTPD &= "INTO #TmPagC_Sap1 "
        'MODIFICACION LINEA POR CAMBIO DE PAGOS: URIEL 07/092018 =============================================INICIO
        'SQLTPD &= "FROM ORCT T0 INNER JOIN RCT2 T1 ON T0.DocNum = T1.DocNum "
        SQLTPD &= "FROM ORCT T0 INNER JOIN RCT2 T1 ON T0.DocEntry = T1.DocNum "
        'MODIFICACION LINEA POR CAMBIO DE PAGOS: URIEL 07/092018 =============================================FIN



        'SE AGREGA UNA EXCEPCION DE AANTICIPO RECIBIDO: URIEL 18/08/2018 =================================================INICIO
        SQLTPD &= "INNER JOIN OINV T2 ON T2.DocEntry = T1.DocEntry AND T1.InvType = 13 AND T2.Series <> 88 "
        'SQLTPD &= "INNER JOIN OINV T2 ON T2.DocEntry = T1.DocEntry AND T1.InvType = 13 "
        SQLTPD &= "INNER JOIN INV1 T3 ON T3.DocEntry = T2.DocEntry AND T3.ItemCode <> 'ANTICIPOREC' "
        'SQLTPD &= "INNER JOIN INV1 T3 ON T3.DocEntry = T2.DocEntry "
        'SE AGREGA UNA EXCEPCION DE AANTICIPO RECIBIDO: URIEL 18/08/2018 =================================================FIN




        SQLTPD &= "INNER JOIN OSLP T4 ON T4.SlpCode = T2.SlpCode "
        SQLTPD &= "INNER JOIN OITM T5 ON T5.ItemCode = T3.ItemCode "
        SQLTPD &= "WHERE T0.Canceled = 'N' AND T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND T0.DocTotal <> .01 AND T0.DocNum NOT IN (SELECT Pago FROM TPM.dbo.[00ExcepPagos]) "

        SQLTPD &= "GROUP BY T2.SlpCode,T4.SlpName,T0.DocNum,T0.DocDate,T2.CardCode,T2.CardName,T2.DocDate,T1.InvType, "
        SQLTPD &= "T1.SumApplied,T2.DocTotal,T2.VatSum,T2.TotalExpns,T2.DocNum,T2.EDocNum,T1.DocEntry,T0.DocTotal  ORDER BY T2.SlpCode,T0.DocNum "




        SQLTPD &= "SELECT T7.SlpCode AS Agente,T4.SlpName AS NombreAgente,T7.DocNum AS DoctoSap,T7.DocDate AS FchFactura,CAST('' as Varchar(155)) AS FactSaldo,"
        SQLTPD &= "T0.DocNum AS IdPago,T0.DocDate AS FchPago,T7.CardCode AS CodClte,T7.CardName AS NombreClte,"
        SQLTPD &= "T7.DocTotal AS ImpFactConIva,T0.DocTotal AS BancoConIva,(T7.DocTotal - T7.VatSum) - T7.TotalExpns AS ImpFactSinIva,"
        SQLTPD &= "T1.SumApplied AS PagoConIva,"
        SQLTPD &= "T1.SumApplied / 1.16 AS PagoSinIva,"
        SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod = 187 THEN  T3.LineTotal ELSE 0 END) AS ImporteFlete,"
        SQLTPD &= "CASE WHEN T7.DocTotal <= 0 or SUM(CASE WHEN T5.ItmsGrpCod = 187 THEN  T3.LineTotal ELSE 0 END) <= 0 THEN 1 ELSE "
        SQLTPD &= "(((SUM(CASE WHEN T5.ItmsGrpCod = 187 THEN  T3.LineTotal ELSE 0 END) / (T7.DocTotal - T7.VatSum) - T7.TotalExpns)) - 1) * -1 "
        SQLTPD &= "END AS Factor,"
        SQLTPD &= "CASE WHEN T7.DocTotal <= 0 or SUM(CASE WHEN T5.ItmsGrpCod = 187 THEN  T3.LineTotal ELSE 0 END) <= 0 THEN T1.SumApplied / 1.16 ELSE "
        SQLTPD &= "((((SUM(CASE WHEN T5.ItmsGrpCod = 187 THEN  T3.LineTotal ELSE 0 END) / (T7.DocTotal - T7.VatSum) - T7.TotalExpns)) - 1) * -1) * "
        SQLTPD &= "(T1.SumApplied / 1.16) "
        SQLTPD &= "END AS BaseComision,"

        'CALCULO DE COMISION PASADO
        'SQLTPD &= "(CASE WHEN SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  T3.U_BXP_Comision ELSE 0 END) <= 0 OR "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  1 ELSE 0 END) <= 0 THEN 0 ELSE "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  T3.U_BXP_Comision ELSE 0 END) / "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  1 ELSE 0 END) END) * .01 "
        'SQLTPD &= "AS '%Comision', "

        'Consulta pasada Antes del 11 de Junio 2021
        'SQLTPD &= "SUM((CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END * .01) * Case When T5.ItmsGrpCod <> 150 Then  T3.LineTotal Else 0 End) / "
        'SQLTPD &= "  ((T7.DocTotal - T7.VatSum) - T7.TotalExpns) AS '%Comision',"


        SQLTPD &= "CASE WHEN ((T1.SumApplied / 1.16) * (SUM((CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END * .01) "
        SQLTPD &= " * CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.LineTotal ELSE 0 END) /    ((T7.DocTotal - T7.VatSum) - T7.TotalExpns)) ) <= 0 THEN 0 ELSE "

        SQLTPD &= "    ROUND(  ((T1.SumApplied / 1.16) * (SUM((CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END * .01) "
        SQLTPD &= "  * CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.LineTotal ELSE 0 END) /    ((T7.DocTotal - T7.VatSum) - T7.TotalExpns)) ) / "

        SQLTPD &= " (CASE WHEN T7.DocTotal <= 0 or SUM(CASE WHEN T5.ItmsGrpCod = 187 THEN  T3.LineTotal ELSE 0 END) <= 0 THEN T1.SumApplied / 1.16 "
        SQLTPD &= "ELSE ((((SUM(CASE WHEN T5.ItmsGrpCod = 187 THEN  T3.LineTotal ELSE 0 END) / (T7.DocTotal - T7.VatSum) - T7.TotalExpns)) - 1) * -1) "
        SQLTPD &= "* (T1.SumApplied / 1.16) END), 5) "
        SQLTPD &= "END AS '%Comision', "



        'CALCULO DE COMISION PASADO
        'SQLTPD &= "CASE WHEN (SUM(CASE WHEN T5.ItmsGrpCod = 187 THEN  T3.LineTotal ELSE 0 END)) > 0 "
        'SQLTPD &= "THEN "
        'SQLTPD &= "CASE WHEN T7.DocTotal <= 0 or SUM(CASE WHEN T5.ItmsGrpCod = 187 THEN  T3.LineTotal ELSE 0 END) <= 0 THEN 0 ELSE "
        'SQLTPD &= "((((SUM(CASE WHEN T5.ItmsGrpCod = 187 THEN  T3.LineTotal ELSE 0 END) / (T7.DocTotal - T7.VatSum) - T7.TotalExpns)) - 1) * -1) * "
        'SQLTPD &= "(T1.SumApplied / 1.16) "
        'SQLTPD &= "END * "
        'SQLTPD &= "(CASE WHEN SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  T3.U_BXP_Comision ELSE 0 END) <= 0 OR "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  1 ELSE 0 END) <= 0 THEN 0 ELSE "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  T3.U_BXP_Comision ELSE 0 END) / "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  1 ELSE 0 END) END * .01) "
        'SQLTPD &= "ELSE "
        'SQLTPD &= "(T1.SumApplied / 1.16)  * "
        'SQLTPD &= "(CASE WHEN SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  T3.U_BXP_Comision ELSE 0 END) <= 0 OR "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  1 ELSE 0 END) <= 0 THEN 0 ELSE "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  T3.U_BXP_Comision ELSE 0 END) / "
        'SQLTPD &= "SUM(CASE WHEN T5.ItmsGrpCod <> 187 THEN  1 ELSE 0 END) END * .01) END AS TotComision, "

        SQLTPD &= "	   (T1.SumApplied / 1.16) * (SUM((CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.U_BXP_Comision ELSE 0 END * .01) * CASE WHEN T5.ItmsGrpCod <> 150 THEN  T3.LineTotal ELSE 0 END) / "
        SQLTPD &= "   ((T7.DocTotal - T7.VatSum) - T7.TotalExpns)) AS TotComision,"


        SQLTPD &= "ROW_NUMBER() OVER(PARTITION BY T0.DocNum ORDER BY T0.DocNum) as OrdPago "
        SQLTPD &= "INTO #TmPagC_Sap2 "

        'MODIFICACION LINEA POR CAMBIO DE PAGOS: URIEL 07/092018 =============================================INICIO
        'SQLTPD &= "FROM ORCT T0 LEFT JOIN RCT2 T1 ON T0.DocNum = T1.DocNum "
        SQLTPD &= "FROM ORCT T0 LEFT JOIN RCT2 T1 ON T0.DocEntry = T1.DocNum "
        'MODIFICACION LINEA POR CAMBIO DE PAGOS: URIEL 07/092018 =============================================FIN


        'SE AGREGA UNA EXCEPCION DE AANTICIPO RECIBIDO: URIEL 18/08/2018 =================================================INICIO
        SQLTPD &= "INNER JOIN OINV T2 ON T2.DocEntry = T1.DocEntry AND T1.InvType = 13 AND T2.Series <> 88 "
        'SQLTPD &= "INNER JOIN OINV T2 ON T2.DocEntry = T1.DocEntry AND T1.InvType = 13 "
        'SE AGREGA UNA EXCEPCION DE AANTICIPO RECIBIDO: URIEL 18/08/2018 =================================================FIN

        SQLTPD &= "INNER JOIN TPM.dbo.TemFac2014 T6 ON T2.DocNum  = T6.nvafac "
        SQLTPD &= "INNER JOIN [SBO-Diamante-productiva].dbo.OINV T7 ON T7.DocNum = T6.antfac "
        SQLTPD &= "LEFT JOIN [SBO-Diamante-productiva].dbo.INV1 T3 ON T3.DocEntry = T7.DocEntry "
        SQLTPD &= "LEFT JOIN [SBO-Diamante-productiva].dbo.OSLP T4 ON T4.SlpCode = T7.SlpCode "
        SQLTPD &= "LEFT JOIN [SBO-Diamante-productiva].dbo.OITM T5 ON T5.ItemCode = T3.ItemCode "


        SQLTPD &= "WHERE T0.Canceled = 'N' AND T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND T0.DocTotal <> .01 AND T0.DocNum NOT IN (SELECT Pago FROM TPM.dbo.[00ExcepPagos]) "


        SQLTPD &= "GROUP BY T7.SlpCode,T4.SlpName,T0.DocNum,T0.DocDate,T7.CardCode,T7.CardName,T7.DocDate,T1.InvType, "
        SQLTPD &= "T1.SumApplied,T7.DocTotal,T7.VatSum,T7.TotalExpns,T7.DocNum,T7.EDocNum,T1.DocEntry,T0.DocTotal  ORDER BY T7.SlpCode,T0.DocNum "



        SQLTPD &= "SELECT * INTO #TmPagC_Sap FROM #TmPagC_Sap1 UNION ALL SELECT * FROM #TmPagC_Sap2 "


        '---------------------------------------------------------
        SQLTPD &= "declare @cod as int "
        SQLTPD &= "declare @codagte as int "
        SQLTPD &= "declare @nomagte as nvarchar(310) "

        SQLTPD &= "declare CURSORITO cursor for "
        SQLTPD &= "select slpcode,slpname from oslp "

        SQLTPD &= "open CURSORITO "

        SQLTPD &= "fetch next from CURSORITO "
        SQLTPD &= "into @codagte, @nomagte "
        SQLTPD &= "while @@fetch_status = 0 "
        SQLTPD &= "begin "
        SQLTPD &= "update #TmPagC_Sap set Agente = @codagte where NombreAgente = @nomagte "

        SQLTPD &= "fetch next from CURSORITO "
        SQLTPD &= "into @codagte, @nomagte "
        SQLTPD &= "End "
        SQLTPD &= "Close CURSORITO "
        SQLTPD &= "deallocate CURSORITO "
        '---------------------------------------------------------

        SQLTPD &= "SELECT CAST('' AS int) AS Agente,CAST('Monto Total: ' as Varchar(155)) AS NombreAgente,"
        SQLTPD &= "CAST('' AS int) AS DoctoSap,CAST('' AS Datetime) AS FchFactura,"
        SQLTPD &= "CAST('Monto Total: ' as Varchar(155)) AS FactSaldo,CAST('' AS int) AS IdPago,CAST('' AS Datetime) AS FchPago,"
        SQLTPD &= "CAST('' as Varchar(15))AS CodClte,CAST('Monto Total: ' as Varchar(100)) AS NombreClte,"
        SQLTPD &= "CAST('0' AS decimal) AS ImpFactConIva,CAST('0' AS decimal) AS BancoConIva,"
        SQLTPD &= "CAST('0' AS decimal) AS ImpFactSinIva,SUM(PagoConIva) AS PagoConIva,SUM(PagoSinIva) AS PagoSinIva,"
        SQLTPD &= "SUM(ImporteFlete) AS ImporteFlete,"
        SQLTPD &= "CAST('0' AS decimal) AS Factor,CAST('0' AS decimal) AS BaseComision,CAST('0' AS decimal) AS '%Comision',"
        SQLTPD &= "SUM(TotComision) AS TotComision,CAST('' AS int) AS OrdPago "
        SQLTPD &= "INTO #Tot_Comision "
        SQLTPD &= "FROM #TmPagC_Sap "

        SQLTPD &= "DECLARE @TotComisionSinOficina DECIMAL(19,4) = ((SELECT SUM(Totcomision) FROM #Tot_Comision) - "
        SQLTPD &= "(SELECT SUM(TotComision) FROM #TmPagC_Sap WHERE Agente=2)) "
        SQLTPD &= "DECLARE @PagoSinIva DECIMAL(19,4) = (SELECT SUM(PagoSinIva) FROM #Tot_Comision) "


        SQLTPD &= "SELECT Agente,NombreAgente,SUM(PagoSinIva) AS PagoSinIva, "
        SQLTPD &= "SUM(PagoSinIva)/@PagoSinIva *100 AS '%Pago', "
        SQLTPD &= "CASE WHEN AGENTE = 2 THEN 0 ELSE SUM(TotComision) END AS TotComision, "
        SQLTPD &= "CASE WHEN AGENTE = 2 THEN 0 ELSE SUM(TotComision)/SUM(PagoSinIva) * 100 END AS '%Comision',"
        SQLTPD &= "CASE WHEN AGENTE = 2 THEN 0 ELSE SUM(TotComision) END AS TComision2 "
        SQLTPD &= "FROM #TmPagC_Sap GROUP BY Agente,NombreAgente "
        SQLTPD &= "UNION ALL "
        SQLTPD &= "SELECT Agente,NombreAgente,PagoSinIva,100,@TotComisionSinOficina,@TotComisionSinOficina/PagoSinIva*100,"
        SQLTPD &= "CAST('-1' AS decimal) AS TComision2 "
        SQLTPD &= "FROM #Tot_Comision ORDER BY TComision2 DESC; "

        'SQLTPD &= "SELECT * FROM #TmPagC_Sap UNION ALL "
        'SQLTPD &= "SELECT * FROM #Tot_Comision "

        SQLTPD &= "SELECT * FROM #TmPagC_Sap ORDER BY NombreAgente ASC,FchPago ASC,IdPago ASC,OrdPago ASC "

        'SQLTPD &= "SELECT Agente,NombreAgente,DoctoSap,FchFactura,CAST('' as Varchar(155)) AS FactSaldo,IdPago,FchPago,CodClte,NombreClte,ImpFactConIva,"
        'SQLTPD &= "BancoConIva,ImpFactSinIva,PagoConIva,PagoSinIva,ImporteFlete,Factor,BaseComision,[%Comision],TotComision,OrdPago "
        'SQLTPD &= "FROM #TmPagC_Sap ORDER BY Agente,NombreAgente ASC,FchPago ASC,IdPago ASC,OrdPago ASC  "

        SQLTPD &= "DROP TABLE #TmPagC_Sap1 "
        SQLTPD &= "DROP TABLE #TmPagC_Sap2 "
        SQLTPD &= "DROP TABLE #TmPagC_Sap "
        SQLTPD &= "DROP TABLE #Tot_Comision "

        ' Nuevo objeto Dataset   
        Dim DsVtasDet As New DataSet

        DsVtasDet.Tables.Add(DTRefacciones)


        With comando
            If Me.CmbAgteVta.SelectedValue <> 999 Then
                .Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
            End If
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
            .Fill(DsVtasDet, "PagoComision")
        End With


        DsVtasDet.Tables(1).TableName = "TotalAgtes"
        DsVtasDet.Tables(2).TableName = "DetalleAgtes"



        '**********************************************************************************************************************************

        Dim DtAgte As New DataTable
        DtAgte = DsVtasDet.Tables("TotalAgtes")

        'DataGridView1.DataSource = DtAgte


        With Me.DgTotalAgte
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
            .MultiSelect = False
            .AllowUserToAddRows = False

            .Columns(0).HeaderText = "# ID"
            .Columns(0).Width = 45
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(1).HeaderText = "Agente"
            .Columns(1).Width = 170

            .Columns(2).HeaderText = "$ Pago"
            .Columns(2).Width = 90
            .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(3).HeaderText = "% Pago"
            .Columns(3).Width = 50
            .Columns(3).DefaultCellStyle.Format = "###.00"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            .Columns(4).HeaderText = "$ Comisión"
            .Columns(4).Width = 90
            .Columns(4).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(5).HeaderText = "% Com"
            .Columns(5).Width = 50
            .Columns(5).DefaultCellStyle.Format = "###.00"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            .Columns(6).Visible = False

        End With


        Dim DtClientes As New DataTable
        DtClientes = DsVtasDet.Tables("DetalleAgtes")

        For Each Fila As DataRow In DtClientes.Rows
            If Fila("OrdPago") <> 1 Then
                Fila("BancoConIva") = 0
            End If
        Next


        With Me.DgDetAgte
            .DataSource = DtClientes
            .ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .AllowUserToAddRows = False

        End With


        With conexion2
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If IsNothing(CmbAgteVta.SelectedValue) Then
            MessageBox.Show("Seleccione un agente de ventas", _
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbAgteVta.Focus()
            Return
        End If

        cargar_registros()
    End Sub

    Private Sub Comisiones_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

        Dim ConsutaLista As String
        Dim DSetTablas As New DataSet

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
            ConsutaLista = "SELECT OSLP.slpcode,OSLP.slpname FROM OSLP ORDER BY slpname"
            Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


            daAgte.Fill(DSetTablas, "Agentes")

            Dim filaAgte As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaAgte = DSetTablas.Tables("Agentes").NewRow

            'Agregamos los valores a los campos de la tabla
            filaAgte("slpname") = "TODOS"
            filaAgte("slpcode") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

            Me.CmbAgteVta.DataSource = DSetTablas.Tables("Agentes")
            Me.CmbAgteVta.DisplayMember = "slpname"
            Me.CmbAgteVta.ValueMember = "slpcode"
            Me.CmbAgteVta.SelectedValue = 999

        End Using
    End Sub

    Private Sub BtnTotal_Click(sender As System.Object, e As System.EventArgs) Handles BtnTotal.Click

        If DgTotalAgte.RowCount <= 1 Then
            MessageBox.Show("No existen registros a exportar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        BtnTotal.Enabled = False
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)

        'Declaramos el nombre de las columnas
        oSheet.range("A5").value = "ID"
        oSheet.range("B5").value = "Agente"
        oSheet.range("C5").value = "$ Pago"
        oSheet.range("D5").value = "% Pago"
        oSheet.range("E5").value = "$ Comisión"
        oSheet.range("F5").value = "% Comisión"
       
        'para poner la primera fila de los titulos en negrita
        oSheet.range("A5:F5").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = Me.DgTotalAgte.RowCount
        For fila_dt = 0 To total_reg - 1


            'para leer una celda en concreto
            'el numero es la columna
            'Dim cel1 As String = Me.DgTotalAgte.Item(0, fila_dt).Value
            Dim cel1 As String = IIf(Me.DgTotalAgte.Item(0, fila_dt).Value = 0, "", Me.DgTotalAgte.Item(0, fila_dt).Value)

            Dim cel2 As String = Me.DgTotalAgte.Item(1, fila_dt).Value
            Dim cel3 As String = IIf(IsDBNull(Me.DgTotalAgte.Item(2, fila_dt).Value), 0, Me.DgTotalAgte.Item(2, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DgTotalAgte.Item(3, fila_dt).Value), 0, Me.DgTotalAgte.Item(3, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DgTotalAgte.Item(4, fila_dt).Value), 0, Me.DgTotalAgte.Item(4, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DgTotalAgte.Item(5, fila_dt).Value), 0, Me.DgTotalAgte.Item(5, fila_dt).Value)

            fila_dt_excel = fila_dt + 6

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel3, 2)
            oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
            oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
            oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
        Next

        Dim vTot As String

        vTot = "A" + (total_reg + 5).ToString
        vTot &= ":F" + (total_reg + 5).ToString
        oSheet.range(vTot).font.bold = True

        'oSheet.range("A5:D5").font.bold = True

        oSheet.Columns("A:A").NumberFormat = "@"

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        oSheet.columns("A:N").entirecolumn.autofit()
        oSheet.range("A1").value = "Comisiones Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
        oSheet.range("A3").value = "Agente de Ventas - " + CmbAgteVta.Text

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing
        BtnTotal.Enabled = True
    End Sub

    Private Sub BtnDetalle_Click(sender As System.Object, e As System.EventArgs) Handles BtnDetalle.Click

        If DgTotalAgte.RowCount <= 1 Then
            MessageBox.Show("No existen registros a exportar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        BtnDetalle.Enabled = False
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object
        Dim Nombre As String = ""
        Dim vBancoCIva As Decimal
        Dim vPagoCIva As Decimal
        Dim vPagoSinIva As Decimal
        Dim vImpComision As Decimal
        Dim vTotComision As Decimal

        Dim vAgente As Integer

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)

        'Declaramos el nombre de las columnas
        oSheet.range("A5").value = "Id"
        oSheet.range("B5").value = "NombreAgente"
        oSheet.range("C5").value = "DoctoSap"
        oSheet.range("D5").value = "FchFactura"
        oSheet.range("E5").value = "FactSaldo"
        oSheet.range("F5").value = "IdPago"
        oSheet.range("G5").value = "FchPago"
        oSheet.range("H5").value = "CodClte"
        oSheet.range("I5").value = "NombreClte"

        oSheet.range("J5").value = "ImpFactConIva"
        oSheet.range("K5").value = "BancoConIva"
        oSheet.range("L5").value = "ImpFactSinIva"
        oSheet.range("M5").value = "PagoConIva"
        oSheet.range("N5").value = "PagoSinIva"
        oSheet.range("O5").value = "ImporteFlete"
        oSheet.range("P5").value = "Factor"
        oSheet.range("Q5").value = "ImporteComision"
        oSheet.range("R5").value = "%Comision"
        oSheet.range("S5").value = "TotComision"


        'para poner la primera fila de los titulos en negrita
        oSheet.range("A5:S5").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 6
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0
        Dim NumHoja As Integer = 1
        Dim total_reg As Integer = 0

        total_reg = DgDetAgte.RowCount

        vAgente = Me.DgDetAgte.Item(0, fila_dt).Value


        Dim cadena As String = Me.DgDetAgte.Item(1, fila_dt).Value
        Dim numeros() As String = cadena.Split(" ")

        oSheet.Name = numeros(0) + "_" + Me.DgDetAgte.Item(0, fila_dt).Value.ToString

        'If numeros(0) = "OFICINA" Then
        '    oSheet.Name = numeros(0)
        'Else
        '    Nombre = numeros(0) + "_" + numeros(1).Substring(0, 1)
        '    oSheet.Name = Nombre
        'End If


        Const xlEdgeLeft = 7
        Const xlEdgeRight = 10
        Const xlEdgeTop = 8
        Const xlEdgeBottom = 9
        Const xlInsideHorizontal = 12
        Const xlInsideVertical = 11

        Const xlContinuous = 1
        Const xlThin = 2
        Const xlAutomatic = -4105

        Dim vTot As String = ""

        For fila_dt = 0 To total_reg - 1

            If vAgente <> Me.DgDetAgte.Item(0, fila_dt).Value Then
                oExcel.Activewindow.Zoom = 85
                oSheet.range("A3").value = "Agente de Ventas - " + DgDetAgte.Item(1, fila_dt - 1).Value
                oSheet.range("A3").font.bold = True

                oSheet.range("K" & fila_dt_excel).value = vBancoCIva

                oSheet.range("M" & fila_dt_excel).value = vPagoCIva
                oSheet.range("N" & fila_dt_excel).value = vPagoSinIva
                oSheet.range("Q" & fila_dt_excel).value = vImpComision
                oSheet.range("S" & fila_dt_excel).value = vTotComision

                oSheet.range("I" & fila_dt_excel).value = "Monto Total"

                oSheet.PageSetup.LeftMargin = 0.0
                oSheet.PageSetup.RightMargin = 0.0
                oSheet.PageSetup.TopMargin = 0.0
                oSheet.PageSetup.BottomMargin = 0.0
                oSheet.PageSetup.HeaderMargin = 0.0
                oSheet.PageSetup.FooterMargin = 0.0
                oSheet.PageSetup.Zoom = 54


                oSheet.PageSetup.Orientation = 2

                '1 Vertical
                '2 Horizontal

                vTot = "A" + (fila_dt_excel).ToString
                vTot &= ":S" + (fila_dt_excel).ToString

                oSheet.range(vTot).font.bold = True
                oSheet.range(vTot).NumberFormat = "$ ###,###,###,###.00"


                oSheet.Columns("R:R").NumberFormat = "0.000"
                oSheet.Columns("P:P").NumberFormat = "0.000"

                oSheet.Columns("A:A").NumberFormat = "@"
                oSheet.Columns("E:E").NumberFormat = "@"


                vTot = "A5"
                vTot &= ":S" + (fila_dt_excel - 1).ToString

                'Seleccionamos la hoja
                oBook.worksheets(NumHoja).Select()
                'Seleccionamos elrango de celdas al cual le vamos a poner bordes
                oBook.worksheets(NumHoja).Range(vTot).Select()


                'Aqui mostramos los bordes Izquierdos
                With oExcel.Selection.Borders(xlEdgeLeft)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                'Aqui mostramos los bordes de Arriba
                With oExcel.Selection.Borders(xlEdgeTop)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                'Aqui mostramos los bordes de abajo
                With oExcel.Selection.Borders(xlEdgeBottom)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                'Aqui mostramos los bordes derechos
                With oExcel.Selection.Borders(xlEdgeRight)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                'Aqui mostramos los bordes interiores verticales
                With oExcel.Selection.Borders(xlInsideVertical)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                'Aqui mostramos los bordes interiores horizontales
                With oExcel.Selection.Borders(xlInsideHorizontal)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With

                oSheet.Range("A2").select()
                oSheet.Range("A5:S5").Cells.Interior.Color = RGB(141, 180, 226)

                vTot = "A" + (total_reg + 5).ToString
                vTot &= ":S" + (total_reg + 4).ToString

                oSheet.range(vTot).font.bold = True

                ' para que el tamano de la columna tenga como minimo el maximo de sus textos
                oSheet.columns("A:S").entirecolumn.autofit()

                oSheet.range("A1").value = "Comisiones Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
                oSheet.range("A1").font.size = 11.5
                oSheet.Columns("I:I").Columnwidth = 33
                oSheet.Columns("E:E").Columnwidth = 14
                oSheet.Columns("B:B").Columnwidth = 23
                oSheet.Columns("A:A").Columnwidth = 2.5

                '************************************************************************************************

                'CambioHoja = 1
                NumHoja += 1
                fila_dt_excel = 6
                cadena = Me.DgDetAgte.Item(1, fila_dt).Value

                numeros = cadena.Split(" ")

                If NumHoja >= 1 Then
                    oSheet = oBook.Worksheets.Add(After:=oBook.Worksheets(oBook.Worksheets.Count))
                End If

                oSheet = oBook.worksheets(NumHoja)

                'Select Case Me.DgDetAgte.Item(0, fila_dt).Value
                '    Case 18
                '        oSheet.Name = "J._CEBALLOS"
                '    Case 30
                '        oSheet.Name = "JORGE_M."
                '    Case 11
                '        oSheet.Name = "JORGE_M.1"
                '    Case 22
                '        oSheet.Name = "MARIO_R"
                '    Case 25
                '        oSheet.Name = "MARIO_L"
                '    Case Else
                '        oSheet.Name = numeros(0)
                'End Select

                'oSheet.Name = numeros(0)
                'oSheet.Name = numeros(0) + "_" + IIf(Len(numeros(1)) > 0, numeros(1).Substring(0, 1), "")

                'If numeros(0) = "OFICINA" Then
                '    oSheet.Name = numeros(0)
                'Else
                '    Nombre = numeros(0) + "_" + numeros(1).Substring(0, 1)
                '    oSheet.Name = Nombre
                'End If

                oSheet.Name = numeros(0) + "_" + Me.DgDetAgte.Item(0, fila_dt).Value.ToString

                oSheet.range("A5").value = "Id"
                oSheet.range("B5").value = "NombreAgente"
                oSheet.range("C5").value = "DoctoSap"
                oSheet.range("D5").value = "FchFactura"
                oSheet.range("E5").value = "FactSaldo"
                oSheet.range("F5").value = "IdPago"
                oSheet.range("G5").value = "FchPago"
                oSheet.range("H5").value = "CodClte"
                oSheet.range("I5").value = "NombreClte"

                oSheet.range("J5").value = "ImpFactConIva"
                oSheet.range("K5").value = "BancoConIva"
                oSheet.range("L5").value = "ImpFactSinIva"
                oSheet.range("M5").value = "PagoConIva"
                oSheet.range("N5").value = "PagoSinIva"
                oSheet.range("O5").value = "ImporteFlete"
                oSheet.range("P5").value = "Factor"
                oSheet.range("Q5").value = "Importe Comision"
                oSheet.range("R5").value = "%Comision"
                oSheet.range("S5").value = "TotComision"

                oSheet.range("A5:S5").font.bold = True

                vBancoCIva = 0
                vPagoCIva = 0
                vPagoSinIva = 0
                vImpComision = 0
                vTotComision = 0

            End If

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel1 As String = IIf(Me.DgDetAgte.Item(0, fila_dt).Value = 0, "", Me.DgDetAgte.Item(0, fila_dt).Value)
            Dim cel2 As String = Me.DgDetAgte.Item(1, fila_dt).Value
            Dim cel3 As String = IIf(IsDBNull(Me.DgDetAgte.Item(2, fila_dt).Value), 0, Me.DgDetAgte.Item(2, fila_dt).Value)
            Dim cel4 As Date = IIf(IsDBNull(Me.DgDetAgte.Item(3, fila_dt).Value), "12/12/1999", Me.DgDetAgte.Item(3, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DgDetAgte.Item(4, fila_dt).Value), 0, Me.DgDetAgte.Item(4, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DgDetAgte.Item(5, fila_dt).Value), 0, Me.DgDetAgte.Item(5, fila_dt).Value)
            Dim cel7 As Date = IIf(IsDBNull(Me.DgDetAgte.Item(6, fila_dt).Value), "12/12/1999", Me.DgDetAgte.Item(6, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DgDetAgte.Item(7, fila_dt).Value), 0, Me.DgDetAgte.Item(7, fila_dt).Value)

            Dim cel9 As String = IIf(IsDBNull(Me.DgDetAgte.Item(8, fila_dt).Value), 0, Me.DgDetAgte.Item(8, fila_dt).Value)
            Dim cel10 As String = IIf(IsDBNull(Me.DgDetAgte.Item(9, fila_dt).Value), 0, Me.DgDetAgte.Item(9, fila_dt).Value)
            Dim cel11 As String = IIf(IsDBNull(Me.DgDetAgte.Item(10, fila_dt).Value), 0, Me.DgDetAgte.Item(10, fila_dt).Value)
            Dim cel12 As String = IIf(IsDBNull(Me.DgDetAgte.Item(11, fila_dt).Value), 0, Me.DgDetAgte.Item(11, fila_dt).Value)
            Dim cel13 As String = IIf(IsDBNull(Me.DgDetAgte.Item(12, fila_dt).Value), 0, Me.DgDetAgte.Item(12, fila_dt).Value)
            Dim cel14 As String = IIf(IsDBNull(Me.DgDetAgte.Item(13, fila_dt).Value), 0, Me.DgDetAgte.Item(13, fila_dt).Value)
            Dim cel15 As String = IIf(IsDBNull(Me.DgDetAgte.Item(14, fila_dt).Value), 0, Me.DgDetAgte.Item(14, fila_dt).Value)
            Dim cel16 As String = IIf(IsDBNull(Me.DgDetAgte.Item(15, fila_dt).Value), 0, Me.DgDetAgte.Item(15, fila_dt).Value)
            Dim cel17 As String = IIf(IsDBNull(Me.DgDetAgte.Item(16, fila_dt).Value), 0, Me.DgDetAgte.Item(16, fila_dt).Value)
            Dim cel18 As String = IIf(IsDBNull(Me.DgDetAgte.Item(17, fila_dt).Value), 0, Me.DgDetAgte.Item(17, fila_dt).Value)
            Dim cel19 As String = IIf(IsDBNull(Me.DgDetAgte.Item(18, fila_dt).Value), 0, Me.DgDetAgte.Item(18, fila_dt).Value)


            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = cel3
            oSheet.range("D" & fila_dt_excel).value = cel4
            oSheet.range("E" & fila_dt_excel).value = cel5
            oSheet.range("F" & fila_dt_excel).value = cel6
            oSheet.range("G" & fila_dt_excel).value = cel7 'FormatNumber(cel7, 2)
            oSheet.range("H" & fila_dt_excel).value = cel8

            oSheet.range("I" & fila_dt_excel).value = cel9
            oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel10, 2)
            oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel11, 2)

            oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel12, 2)
            oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel13, 2)
            oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel14, 2)
            oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel15, 2)
            oSheet.range("P" & fila_dt_excel).value = cel16
            oSheet.range("Q" & fila_dt_excel).value = FormatNumber(cel17, 2)
            oSheet.range("R" & fila_dt_excel).value = cel18
            oSheet.range("S" & fila_dt_excel).value = FormatNumber(cel19, 2)

            vAgente = Me.DgDetAgte.Item(0, fila_dt).Value

            fila_dt_excel += 1
            vBancoCIva += IIf(IsDBNull(Me.DgDetAgte.Item(10, fila_dt).Value), 0, Me.DgDetAgte.Item(10, fila_dt).Value)

            vPagoCIva += IIf(IsDBNull(Me.DgDetAgte.Item(12, fila_dt).Value), 0, Me.DgDetAgte.Item(12, fila_dt).Value)
            vPagoSinIva += IIf(IsDBNull(Me.DgDetAgte.Item(13, fila_dt).Value), 0, Me.DgDetAgte.Item(13, fila_dt).Value)
            vImpComision += IIf(IsDBNull(Me.DgDetAgte.Item(16, fila_dt).Value), 0, Me.DgDetAgte.Item(16, fila_dt).Value)
            vTotComision += IIf(IsDBNull(Me.DgDetAgte.Item(18, fila_dt).Value), 0, Me.DgDetAgte.Item(18, fila_dt).Value)

            If fila_dt = total_reg - 1 Then

                oSheet.range("A3").value = "Agente de Ventas - " + DgDetAgte.Item(1, fila_dt).Value
            End If

        Next

        oSheet.Columns("R:R").NumberFormat = "0.000"
        oSheet.Columns("P:P").NumberFormat = "0.000"

        oSheet.Columns("A:A").NumberFormat = "@"
        oSheet.Columns("E:E").NumberFormat = "@"


        vTot = "A5"
        vTot &= ":S" + (fila_dt_excel - 1).ToString

        'Seleccionamos la hoja
        oBook.worksheets(NumHoja).Select()
        'Seleccionamos las celdas
        oBook.worksheets(NumHoja).Range(vTot).Select()

        'Aqui mostramos los bordes Izquierdos
        With oExcel.Selection.Borders(xlEdgeLeft)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
        'Aqui mostramos los bordes de Arriba
        With oExcel.Selection.Borders(xlEdgeTop)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
        'Aqui mostramos los bordes de abajo
        With oExcel.Selection.Borders(xlEdgeBottom)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
        'Aqui mostramos los bordes derechos
        With oExcel.Selection.Borders(xlEdgeRight)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
        'Aqui mostramos los bordes interiores verticales
        With oExcel.Selection.Borders(xlInsideVertical)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
        'Aqui mostramos los bordes interiores horizontales
        With oExcel.Selection.Borders(xlInsideHorizontal)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With

        oSheet.Range("A2").select()
        oSheet.Range("A5:S5").Cells.Interior.Color = RGB(141, 180, 226)

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        oSheet.columns("A:S").entirecolumn.autofit()

        oSheet.range("A1").value = "Comisiones Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
        oSheet.range("A1").font.size = 11.5
        oSheet.Columns("I:I").Columnwidth = 33
        oSheet.Columns("E:E").Columnwidth = 14
        oSheet.Columns("B:B").Columnwidth = 23
        oSheet.Columns("A:A").Columnwidth = 2.5

        oSheet.range("K" & fila_dt_excel).value = vBancoCIva

        oSheet.range("M" & fila_dt_excel).value = vPagoCIva
        oSheet.range("N" & fila_dt_excel).value = vPagoSinIva
        oSheet.range("Q" & fila_dt_excel).value = vImpComision
        oSheet.range("S" & fila_dt_excel).value = vTotComision

        oSheet.range("I" & fila_dt_excel).value = "Monto Total"
        oSheet.PageSetup.LeftMargin = 0.0
        oSheet.PageSetup.RightMargin = 0.0
        oSheet.PageSetup.TopMargin = 0.0
        oSheet.PageSetup.BottomMargin = 0.0
        oSheet.PageSetup.HeaderMargin = 0.0
        oSheet.PageSetup.FooterMargin = 0.0
        oSheet.PageSetup.Zoom = 54
        oExcel.Activewindow.Zoom = 85

        oSheet.PageSetup.Orientation = 2.3
        vTot = "A" + (fila_dt_excel).ToString
        vTot &= ":S" + (fila_dt_excel).ToString
        oSheet.range(vTot).font.bold = True
        oSheet.range(vTot).NumberFormat = "$ ###,###,###,###.00"
        oSheet.range("A3").font.bold = True

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing

        BtnDetalle.Enabled = True
    End Sub
End Class