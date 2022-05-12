Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel

Public Class DiferenciaPrecioCompras

  Dim sError As String
  Dim Resultado As New DataView
#Region "Eventos"

  Private Sub DiferenciaPrecioCompras_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    'Dim FchInicio As DateTime
    'FchInicio = DateAdd(DateInterval.Month, -1, Date.Now)
    'Me.DtpFechaIni.Value = Format(FchInicio, "dd/MM/yyyy")
    CheckRevisados.Checked = False
    Me.CBFecConta.Checked = True

    Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")

    Me.DtpFechaFin.Value = Format(Date.Now, "dd/MM/yyyy")

    sError = "Verifique los siguientes campos: "
    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

      mCargaLineaIni()

      mCargaArticulo(SqlConnection, False)

      mCargaPoveedor(String.Empty)

    End Using

  End Sub

  Private Sub CmbLin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbLin.SelectedIndexChanged
    If CmbLin.SelectedValue.ToString <> "0" Then
      Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
        mCargaArticulo(SqlConnection, True)
        mCargaPoveedor("Linea")
      End Using
    End If
  End Sub

  Private Sub DtpFechaFin_ValueChanged(sender As Object, e As EventArgs) Handles DtpFechaFin.ValueChanged
    fValidaFechas()
  End Sub

  Private Sub DtpFechaIni_ValueChanged(sender As Object, e As EventArgs) Handles DtpFechaIni.ValueChanged
    fValidaFechas()
  End Sub


  Private Sub bConsultar_Click(sender As Object, e As EventArgs) Handles bConsultar.Click

    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
      Dim sCadena As String

      '----CODIGO PARA OBTENER FACTURAS CANCELADAS

      sCadena = " DECLARE @FACT AS TABLE(DocNum  INT, NumAtCard VARCHAR(100), DocDate DATE, BaseEntry INT, BaseType INT, Comments VARCHAR(MAX), DocTotal numeric(19, 6)) "

      sCadena &= " INSERT INTO @FACT "

      sCadena &= " SELECT DISTINCT T1.DocNum, T1.NumAtCard, T1.DocDate, T0.BaseEntry, T0.BaseType,T1.Comments, T1.DocTotal "
      sCadena &= " FROM SBO_TPD.dbo.RPC1 T0 "
      sCadena &= " INNER JOIN SBO_TPD.dbo.ORPC T1 ON T0.DocEntry = T1.DocEntry"
      'sCadena = " --WHERE T0.BaseType = 20 --AND EDocNum IS NULL"

      sCadena &= " DECLARE @FactCancel AS TABLE("
      sCadena &= " FACTURA INT,"
      sCadena &= " REFERENCIAFACT VARCHAR(120),"
      sCadena &= " FECHAFACT DATE,"
      sCadena &= " NCSAP INT, "
      sCadena &= " COMMENTS VARCHAR(250), "
      sCadena &= " REFERENCIANC VARCHAR(150), "
      sCadena &= " FECHANC DATE, "
      sCadena &= " TOTALFACT NUMERIC(20, 6), "
      sCadena &= " TOTALNC NUMERIC(20, 6), "
      sCadena &= " DOCTYPE VARCHAR(10) "
      sCadena &= " ) "

      sCadena &= " INSERT INTO @FactCancel(FACTURA,REFERENCIAFACT,FECHAFACT,NCSAP,COMMENTS,REFERENCIANC,FECHANC,TOTALFACT,TOTALNC,DOCTYPE) "
      sCadena &= " Select DISTINCT "
      sCadena &= " T0.DocNum [FACTURA] ,T0.NumAtCard [REFERENCIAFACT] ,T0.DocDate [FECHAFACT],"
      sCadena &= " T1.DocNum [NCSAP], T1.COMMENTS,T1.NumAtCard [REFERENCIANC], T1.DocDate [FECHANC], T0.DocTotal [TOTALFACT], T1.DocTotal [TOTALNC], T0.DocType [DOCTYPE] "
      sCadena &= " FROM SBO_TPD.dbo.OPCH T0 "
      sCadena &= " LEFT JOIN @FACT T1 ON T0.DocEntry = T1.BaseEntry AND T0.ObjType = T1.BaseType "
      sCadena &= " WHERE T0.DocDate >= '20150101' "
      sCadena &= " AND T1.DocNum IS NOT NULL "
      sCadena &= " and T0.DocTotal = T1.DocTotal "
      sCadena &= " ORDER BY 1 "

      '--SELECT * FROM @FactCancel

      ''----FIN CODIGO PARA OBTENER FACTURAS CANCELADAS

      sCadena &= "Declare @Diferencias table(Revisar int, Factura int,Fecha datetime, [Fecha Contabilizacion] datetime, Articulo varchar(50),Descripcion varchar(200),Linea varchar(50),Cantidad int, " +
                        "[Precio L8] decimal(14,4),Moneda char(3), [Precio compra] decimal(11,2), DescuentoArt decimal(8,2),  " +
                        "DescuentoGral decimal(8,2), Preciotot decimal(11,4), Diferencia decimal(11,4),[Diferencia en porcentaje] decimal(11,4), " +
                        "Comentarios varchar(300), Comentarios_Direccion varchar(300) , Proveedor varchar(200),[Codigo proveedor] varchar(200), " +
                        "ProPredeterminado varchar(200), [CodPredeterminado] varchar(200) ) "
      '"ProAlterno varchar(200), [CodAlterno] varchar(200) ) " 'AQUI SE AGREGA LINEA DE PROVEEDOR ALTERNO: MODIFICACION URIEL 19/09/2018, DESCOMENTAR SI SE REQUIERE


      sCadena &= " Insert into @Diferencias(Revisar,Factura,Fecha,[Fecha Contabilizacion],Articulo,Descripcion,Linea,Cantidad,[Precio L8],Moneda,[Precio compra],  DescuentoArt,  DescuentoGral, Preciotot, " +
                        "Diferencia,[Diferencia en porcentaje],Comentarios,Comentarios_Direccion,Proveedor,[Codigo proveedor], " +
                        "ProPredeterminado, [CodPredeterminado] ) "
      '"ProAlterno, [CodAlterno] ) " 'AQUI SE AGREGA LINEA DE PROVEEDOR ALTERNO: MODIFICACION URIEL 19/09/2018 DESCOMENTAR SI SE REQUIERE

      If CBFecConta.Checked Then

        sCadena &= "select Distinct ISNULL(COM.Revisar,0) Revisar, fac.DocNum Factura, fac.TaxDate Fecha, fac.DocDate [Fecha Contabilizacion], det.ItemCode Articulo, art.ItemName Descripcion, lin.ItmsGrpNam Linea,  "
        sCadena &= "det.Quantity Cantidad, "
        sCadena &= "CASE WHEN T0.Precio8 IS NULL THEN detart.Price ELSE "
        sCadena &= "CASE WHEN T0.Precio8 = 0 THEN detart.Price ELSE  "
        sCadena &= "T0.Precio8  "
        sCadena &= "END "
        sCadena &= "END 'Precio L8', "

        sCadena &= " detart.Currency Moneda, "

        sCadena &= "case when det.Currency=detart.Currency then "
        sCadena &= "det.PriceBefDi "
        sCadena &= "else "
        sCadena &= "case when det.Currency='MXP' then  "
        sCadena &= "det.PriceBefDi /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))  "
        sCadena &= "else  "
        sCadena &= "det.PriceBefDi *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))  "
        sCadena &= "END   "
        sCadena &= "End 'Precio compra', "



        sCadena &= " det.discprcnt/100 as 'Descuento ', "
        sCadena &= " isnull(fac.discprcnt,0)/100 as 'Descuento General', "

        sCadena &= "case when det.Currency=detart.Currency then  "
        sCadena &= "det.Price -(det.price * (isnull(fac.discprcnt, 0) / 100)) "
        sCadena &= "when det.Currency='MXP' then  "
        sCadena &= "(det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) -   "
        sCadena &= "((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) "
        sCadena &= "when det.Currency='USD' then  "
        sCadena &= "(det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) -   "
        sCadena &= "((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) "
        sCadena &= "End 'Precio Total',  "

        sCadena &= "case when det.Currency=detart.Currency then  "
        sCadena &= "det.Price -(det.price * (isnull(fac.discprcnt, 0) / 100)) -   "
        sCadena &= "CASE WHEN T0.Precio8 IS NULL THEN detart.Price ELSE  "
        sCadena &= "CASE WHEN T0.Precio8 = 0 THEN detart.Price ELSE  "
        sCadena &= "T0.Precio8  "
        sCadena &= "END  "
        sCadena &= "END  "
        sCadena &= "when det.Currency='MXP' then  "
        sCadena &= "(det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) -   "
        sCadena &= "((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) -   "
        sCadena &= "CASE WHEN T0.Precio8 IS NULL THEN detart.Price ELSE  "
        sCadena &= "CASE WHEN T0.Precio8 = 0 THEN detart.Price ELSE  "
        sCadena &= "T0.Precio8  "
        sCadena &= "END  "
        sCadena &= "END  "
        sCadena &= "when det.Currency='USD' then  "
        sCadena &= "(det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) -   "
        sCadena &= "((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) -   "
        sCadena &= "CASE WHEN T0.Precio8 IS NULL THEN detart.Price ELSE  "
        sCadena &= "CASE WHEN T0.Precio8 = 0 THEN detart.Price ELSE  "
        sCadena &= "T0.Precio8  "
        sCadena &= "END  "
        sCadena &= "END  "
        sCadena &= "End 'Diferencia',  "

        sCadena &= "CASE WHEN T0.Precio8 IS NULL THEN  "
        sCadena &= "CASE WHEN detart.Price = 0 THEN  "
        sCadena &= "0   "
        sCadena &= "ELSE  "
        sCadena &= "case when det.Currency=detart.Currency then  	 "
        sCadena &= "((det.Price -(det.price * (isnull(fac.discprcnt,0) / 100))- detart.Price)/detart.Price)  "
        sCadena &= "when det.Currency='MXP' then  "
        sCadena &= "(((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) - ((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) - detart.Price )/detart.Price )  "
        sCadena &= "when det.Currency='USD' then  "
        sCadena &= "(((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) - ((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) - detart.Price )/detart.Price )  "
        sCadena &= "END  "
        sCadena &= "END  "
        sCadena &= "ELSE  "
        sCadena &= "CASE WHEN  T0.Precio8  = 0 THEN  "
        sCadena &= "0   "
        sCadena &= "ELSE  "
        sCadena &= "case when det.Currency=detart.Currency then   "
        sCadena &= "((det.Price -(det.price * (isnull(fac.discprcnt,0) / 100))- T0.Precio8)/T0.Precio8)  "
        sCadena &= "when det.Currency='MXP' then   "
        sCadena &= "(((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) - ((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) - T0.Precio8 )/T0.Precio8 )  "
        sCadena &= "when det.Currency='USD' then   "
        sCadena &= "(((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) - ((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) - T0.Precio8 )/T0.Precio8 ) "
        sCadena &= "END  "
        sCadena &= "END	  "
        sCadena &= "END 'Diferencia en porcentaje',    "


        sCadena &= " CASE WHEN COM.Comentario IS NULL THEN '' ElSE COM.Comentario END AS 'Comentarios', CASE WHEN COM.Comentario_Direccion IS NULL THEN '' ElSE COM.Comentario_Direccion END AS 'Comentarios Direccion',   "
        sCadena &= " prov.CardName Proveedor,  fac.CardCode [Codigo proveedor], "
        'AQUI SE AGREGA LINEA DE PROVEEDOR ALTERNO: MODIFICACION URIEL 19/09/2018, DESCOMENTAR SI SE REQUIERE
        'sCadena &= " Case when(select CardName2 from TPM.dbo.Proveedores where art.ItemCode = ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS) = '-' THEN '-' "
        'sCadena &= " Else (select CardName2 from TPM.dbo.Proveedores where art.ItemCode = ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS) "
        'sCadena &= " End As ProAlterno, "
        'sCadena &= " Case when(select CardCode2 from TPM.dbo.Proveedores where art.ItemCode = ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS) = '-' THEN '-' "
        'sCadena &= " Else (select CardCode2 from TPM.dbo.Proveedores where art.ItemCode = ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS) "
        'sCadena &= " End As CodAlterno "
        sCadena &= "CASE WHEN (SELECT CardName FROM OCRD WHERE CardCode = art.CardCode) IS NULL THEN '-' ELSE (SELECT CardName FROM OCRD WHERE CardCode = art.CardCode) END AS ProPredeterminado, "
        sCadena &= "CASE WHEN (SELECT CardCode FROM OCRD WHERE CardCode = art.CardCode) IS NULL THEN '-' ELSE (SELECT CardCode FROM OCRD WHERE CardCode = art.CardCode) END AS CodPredeterminado "


        sCadena &= " from OPCH fac "
        sCadena &= " inner join pch1 det on Fac.DocEntry=det.DocEntry  	 "
        sCadena &= " inner join OITM art on det.ItemCode= art.ItemCode 	   "
        sCadena &= " inner join OITB lin on art.ItmsGrpCod=lin.ItmsGrpCod 	  "
        sCadena &= " inner join itm1 detart on detart.itemcode = art.itemCode and PriceList=8   "
        sCadena &= " inner join itm1 detart1 on detart1.itemcode = art.itemCode and detart1.PriceList=10  "
        sCadena &= " inner join OCRD prov on fac.CardCode=prov.CardCode "
        sCadena &= " LEFT JOIN TPM.dbo.DifPreComen COM ON fac.DocNum=com.Factura "  '--AND fac.TaxDate=COM.FecFact
        sCadena &= " AND det.ItemCode COLLATE MODERN_SPANISH_CI_AS =COM.Articulo "
        sCadena &= " LEFT JOIN TPM.dbo.FactProvPrecioL8 T0 ON fac.DocNum=T0.Factura "
        sCadena &= " AND fac.TaxDate=t0.FechaDoc AND det.ItemCode COLLATE Modern_Spanish_CI_AS=T0.Articulo "
        sCadena &= " where fac.DocDate between '" + DtpFechaIni.Value.ToString("yyyy-MM-dd") + "' and '" + DtpFechaFin.Value.ToString("yyyy-MM-dd") + "' "


      ElseIf CBFecDoc.Checked Then
        sCadena &= " select ISNULL(COM.Revisar,0) Revisar, fac.DocNum Factura, fac.TaxDate Fecha, fac.DocDate [Fecha Contabilizacion], det.ItemCode Articulo, art.ItemName Descripcion, lin.ItmsGrpNam Linea,  "
        sCadena &= " det.Quantity Cantidad,  "
        sCadena &= "CASE WHEN T0.Precio8 IS NULL THEN detart.Price ELSE "
        sCadena &= "CASE WHEN T0.Precio8 = 0 THEN detart.Price ELSE  "
        sCadena &= "T0.Precio8  "
        sCadena &= "END "
        sCadena &= "END 'Precio L8', "

        sCadena &= " detart.Currency Moneda,"

        sCadena &= "case when det.Currency=detart.Currency then  "
        sCadena &= "det.PriceBefDi "
        sCadena &= "else "
        sCadena &= "case when det.Currency='MXP' then  "
        sCadena &= "det.PriceBefDi /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))  "
        sCadena &= "else  "
        sCadena &= "det.PriceBefDi *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))  "
        sCadena &= "END   "
        sCadena &= "End 'Precio compra', "

        sCadena &= " det.discprcnt/100 as 'Descuento ', "
        sCadena &= " isnull(fac.discprcnt,0)/100 as 'Descuento General', "

        sCadena &= "case when det.Currency=detart.Currency then  "
        sCadena &= "det.Price -(det.price * (isnull(fac.discprcnt, 0) / 100)) "
        sCadena &= "when det.Currency='MXP' then  "
        sCadena &= "(det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) -   "
        sCadena &= "((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) "
        sCadena &= "when det.Currency='USD' then  "
        sCadena &= "(det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) -   "
        sCadena &= "((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) "
        sCadena &= "End 'Precio Total',  "

        sCadena &= "case when det.Currency=detart.Currency then  "
        sCadena &= "det.Price -(det.price * (isnull(fac.discprcnt, 0) / 100)) -   "
        sCadena &= "CASE WHEN T0.Precio8 IS NULL THEN detart.Price ELSE  "
        sCadena &= "CASE WHEN T0.Precio8 = 0 THEN detart.Price ELSE  "
        sCadena &= "T0.Precio8  "
        sCadena &= "END  "
        sCadena &= "END  "
        sCadena &= "when det.Currency='MXP' then  "
        sCadena &= "(det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) -   "
        sCadena &= "((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) -   "
        sCadena &= "CASE WHEN T0.Precio8 IS NULL THEN detart.Price ELSE  "
        sCadena &= "CASE WHEN T0.Precio8 = 0 THEN detart.Price ELSE  "
        sCadena &= "T0.Precio8  "
        sCadena &= "END  "
        sCadena &= "END  "
        sCadena &= "when det.Currency='USD' then  "
        sCadena &= "(det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) -   "
        sCadena &= "((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) -   "
        sCadena &= "CASE WHEN T0.Precio8 IS NULL THEN detart.Price ELSE  "
        sCadena &= "CASE WHEN T0.Precio8 = 0 THEN detart.Price ELSE  "
        sCadena &= "T0.Precio8  "
        sCadena &= "END  "
        sCadena &= "END  "
        sCadena &= "End 'Diferencia',  "

        sCadena &= "CASE WHEN T0.Precio8 IS NULL THEN  "
        sCadena &= "CASE WHEN detart.Price = 0 THEN  "
        sCadena &= "0   "
        sCadena &= "ELSE  "
        sCadena &= "case when det.Currency=detart.Currency then  	 "
        sCadena &= "((det.Price -(det.price * (isnull(fac.discprcnt,0) / 100))- detart.Price)/detart.Price)  "
        sCadena &= "when det.Currency='MXP' then  "
        sCadena &= "(((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) - ((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) - detart.Price )/detart.Price )  "
        sCadena &= "when det.Currency='USD' then  "
        sCadena &= "(((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) - ((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) - detart.Price )/detart.Price )  "
        sCadena &= "END  "
        sCadena &= "END  "
        sCadena &= "ELSE  "
        sCadena &= "CASE WHEN  T0.Precio8  = 0 THEN  "
        sCadena &= "0   "
        sCadena &= "ELSE  "
        sCadena &= "case when det.Currency=detart.Currency then   "
        sCadena &= "((det.Price -(det.price * (isnull(fac.discprcnt,0) / 100))- T0.Precio8)/T0.Precio8)  "
        sCadena &= "when det.Currency='MXP' then   "
        sCadena &= "(((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) - ((det.Price /  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) - T0.Precio8 )/T0.Precio8 )  "
        sCadena &= "when det.Currency='USD' then   "
        sCadena &= "(((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) - ((det.Price *  "
        sCadena &= "(select tt1.rate from ORTT tt1 where tt1.RateDate = DATEADD(day, -1, fac.DocDate))) * isnull(fac.DiscPrcnt,0) / 100) - T0.Precio8 )/T0.Precio8 ) "
        sCadena &= "END  "
        sCadena &= "END	  "
        sCadena &= "END 'Diferencia en porcentaje',    "

        sCadena &= " CASE WHEN COM.Comentario IS NULL THEN '' ElSE COM.Comentario END AS 'Comentarios',  CASE WHEN COM.Comentario_Direccion IS NULL THEN '' ElSE COM.Comentario_Direccion END AS 'Comentarios Direccion',  "
        sCadena &= " prov.CardName Proveedor,  fac.CardCode [Codigo proveedor], "
        'AQUI SE AGREGA LINEA DE PROVEEDOR ALTERNO: MODIFICACION URIEL 19/09/2018, DESCOMENTAR SI SE REQUIERE
        'sCadena &= " Case when(select CardName2 from TPM.dbo.Proveedores where art.ItemCode = ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS) Is NULL THEN '-' "
        'sCadena &= " Else (select CardName2 from TPM.dbo.Proveedores where art.ItemCode = ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS) "
        'sCadena &= " End As ProAlterno, "
        'sCadena &= " Case when(select CardCode2 from TPM.dbo.Proveedores where art.ItemCode = ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS) Is NULL THEN '-' "
        'sCadena &= " Else (select CardCode2 from TPM.dbo.Proveedores where art.ItemCode = ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS) "
        'sCadena &= " End As CodAlterno "
        sCadena &= "CASE WHEN (SELECT CardName FROM OCRD WHERE CardCode = art.CardCode) IS NULL THEN '-' ELSE (SELECT CardName FROM OCRD WHERE CardCode = art.CardCode) END AS ProPredeterminado, "
        sCadena &= "CASE WHEN (SELECT CardCode FROM OCRD WHERE CardCode = art.CardCode) IS NULL THEN '-' ELSE (SELECT CardCode FROM OCRD WHERE CardCode = art.CardCode) END AS CodPredeterminado "

        sCadena &= " from OPCH fac "
        sCadena &= " inner join pch1 det on Fac.DocEntry=det.DocEntry  	 "
        sCadena &= " inner join OITM art on det.ItemCode= art.ItemCode 	   "
        sCadena &= " inner join OITB lin on art.ItmsGrpCod=lin.ItmsGrpCod 	  "
        sCadena &= " inner join itm1 detart on detart.itemcode = art.itemCode and PriceList=8   "
        sCadena &= " inner join itm1 detart1 on detart1.itemcode = art.itemCode and detart1.PriceList=10  "
        sCadena &= " inner join OCRD prov on fac.CardCode=prov.CardCode"
        sCadena &= " LEFT JOIN TPM.dbo.DifPreComen COM ON fac.DocNum=com.Factura "  '--AND fac.TaxDate=COM.FecFact
        sCadena &= " AND det.ItemCode COLLATE MODERN_SPANISH_CI_AS =COM.Articulo "
        sCadena &= " LEFT JOIN TPM.dbo.FactProvPrecioL8 T0 ON fac.DocNum=T0.Factura "
        sCadena &= " AND fac.TaxDate=t0.FechaDoc AND det.ItemCode COLLATE Modern_Spanish_CI_AS=T0.Articulo "
        sCadena &= " where fac.TaxDate between '" + DtpFechaIni.Value.ToString("yyyy-MM-dd") + "' and '" + DtpFechaFin.Value.ToString("yyyy-MM-dd") + "' "
      End If

      If CmbArticulo.Text <> "TODOS" Then
        sCadena &= " and art.ItemCode='" + CmbArticulo.Text + "' "
      End If
      If CmbLin.SelectedValue <> 0 Then
        sCadena &= " and lin.ItmsGrpCod=" + CmbLin.SelectedValue.ToString + " "
        'MsgBox(CmbLin.SelectedValue.ToString)
      End If
      If cmbProveedor.SelectedValue <> "0" Then
        sCadena &= " and det.BaseCard='" + cmbProveedor.SelectedValue.ToString + "'"
      End If

      'VALIDA QUE PUEDAN VER SOLO POR REVISAR CUANDO ESTE SELECCIONADO UNA BUSQUEDA
      If CBFecConta.Checked = True Or CBFecDoc.Checked = True Then
        sCadena &= " Select DISTINCT * into #tmp from @Diferencias " +
                        " where ([Diferencia en porcentaje] > .0099 or [Diferencia en porcentaje] < -.0099) AND Factura NOT IN (SELECT Factura FROM @FactCancel)  " 'ORDER BY [Diferencia en porcentaje]"

        sCadena &= "SELECT Articulo,Descripcion,Linea,[Stock Pue],[Stock Mer],[Stock Tux],[Stock Total],[L08 Compra],Moneda,TC,   "
        sCadena &= "CAST(CASE WHEN [11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [11]/([L08 Compra] * TC) END AS Decimal(11,5)) AS [Factor L09],[1] AS [Precio L01],   "
        sCadena &= "CAST(CASE WHEN [1] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [1]/([L08 Compra] * TC) END AS Decimal(11,5)) AS [Factor L01],   "
        sCadena &= "CAST(CASE WHEN [2] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [2]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L02,   "
        sCadena &= "CAST(CASE WHEN [3] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [3]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L03,   "
        sCadena &= "CAST(CASE WHEN [4] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [4]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L04,   "
        sCadena &= "CAST(CASE WHEN [5] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [5]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L05,   "
        sCadena &= "CAST(CASE WHEN [6] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [6]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L06,   "
        sCadena &= "CAST(CASE WHEN [7] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [7]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L07,   "
        sCadena &= "CAST(CASE WHEN [12] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [12]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L10,   "
        sCadena &= "CAST(CASE WHEN [13] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [13]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L11   "
        sCadena &= "into #tmp2 "
        sCadena &= "from ( select T0.ItemCode as Articulo,T1.ItemName as Descripcion,T2.[ItmsGrpNam] AS Linea,   "
        sCadena &= "T3.OnHand AS [Stock Pue],T4.OnHand AS [Stock Mer],T7.OnHand AS [Stock Tux],   "
        sCadena &= "CASE WHEN T3.OnHand IS NULL THEN 0 ELSE T3.OnHand END + "
        sCadena &= "CASE WHEN T4.OnHand IS NULL THEN 0 ELSE T4.OnHand END + CASE WHEN T7.OnHand IS NULL THEN 0 ELSE T7.OnHand END AS [Stock Total],   "
        sCadena &= "T5.Price AS [L08 Compra],T5.Currency AS Moneda,   "
        sCadena &= "T6.Factor AS TC,   "
        sCadena &= "T0.PriceList,T0.Price   "
        sCadena &= "from ITM1  T0   "
        sCadena &= "INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode   "
        sCadena &= "INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod   "
        sCadena &= "LEFT JOIN OITW T3 ON T0.ItemCode = T3.ItemCode AND T3.WhsCode = 01   "
        sCadena &= "LEFT JOIN OITW T4 ON T0.ItemCode = T4.ItemCode AND T4.WhsCode = 03   "
        sCadena &= "LEFT JOIN OITW T7 ON T0.ItemCode = T7.ItemCode AND T7.WhsCode = 07   "
        sCadena &= "LEFT JOIN ITM1 T5 ON T0.ItemCode = T5.ItemCode AND T5.PriceList = 8   "
        sCadena &= "LEFT JOIN ITM1 T6 ON T0.ItemCode = T6.ItemCode AND T6.PriceList = 10  "
        sCadena &= ") as source    "
        sCadena &= "pivot ( max(Price) for PriceList in ([1],[2],[3],[4],[5],[6],[7],[11],[12],[13])) as pvt   "
        sCadena &= "ORDER BY Articulo "

        sCadena &= "select t0.Revisar, t0.Factura, t0.Fecha, t0.[Fecha Contabilizacion], t0.Articulo, t0.Descripcion, t0.Linea, t0.Cantidad, "
        sCadena &= "t0.[Precio L8], t0.Moneda, t0.[Precio compra], t0.DescuentoArt, t0.DescuentoGral, t0.Preciotot, t0.Diferencia, t0.[Diferencia en porcentaje], "
        sCadena &= "t1.[Factor L09], t1.[Precio L01], t1.[Factor L01], t0.Comentarios,t0.Comentarios_Direccion, t0.Proveedor AS 'Prov. Compra', t0.[Codigo proveedor] AS 'Cod. Proveedor', "
        sCadena &= "t0.ProPredeterminado as 'Pro. Predet.', t0.[CodPredeterminado] AS 'Cod. Pro. Pred.' "
                sCadena &= "from #tmp t0 left join #tmp2 t1 on t0.Articulo = t1.Articulo where (t0.Factura <>24960 and t0.Factura <>25154 and t0.Factura <>25155) "
                sCadena &= "order by t0.[Diferencia en porcentaje] "

        sCadena &= "drop table #tmp "
        sCadena &= "drop table #tmp2 "
      Else
        MsgBox("Favor de seleccionar una busqueda por Fecha de Contabilización o de Documento", MsgBoxStyle.Exclamation, "Alerta de Selección")
        Return
      End If

      'MsgBox(sCadena)

      Dim da As New SqlClient.SqlDataAdapter(sCadena, SqlConnection)

      Dim ds As New DataSet
      da.Fill(ds)


      fFormatoGrid(ds)
    End Using

    BtnExcel.Visible = True

  End Sub

  Private Sub BtnExcel_Click(sender As Object, e As EventArgs) Handles BtnExcel.Click
    mGeneraExcel()
    'Export()
  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    sError = "Verifique los siguientes campos: "
    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

      mCargaLineaIni()

      mCargaArticulo(SqlConnection, False)

      mCargaPoveedor(String.Empty)

    End Using
    DgDifCompras.DataSource = String.Empty
    BtnExcel.Visible = False
  End Sub

#End Region

#Region "Metodos"

  Private Sub mCargaLineaIni()
    Try
      Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

        Dim da As New SqlClient.SqlDataAdapter("SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ", SqlConnection)

        Dim ds As New DataSet
        da.Fill(ds)
        ds.Tables(0).Rows.Add(0, "TODOS")
        Me.CmbLin.DataSource = ds.Tables(0)
        Me.CmbLin.DisplayMember = "ItmsGrpNam"
        Me.CmbLin.ValueMember = "ItmsGrpCod"
        Me.CmbLin.SelectedValue = 0

      End Using

    Catch ex As Exception
      MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
    End Try
  End Sub

  Private Sub mCargaArticulo(SqlConnection As Data.SqlClient.SqlConnection, bBuscar As Boolean)
    Try
      Dim sCadena As String
      sCadena = " Select det.ItemCode codigo, art.ItmsGrpCod linea from PCH1 det " +
                      "	inner join OITM art on det.ItemCode=art.ItemCode "
      If bBuscar Then
        sCadena &= " where art.ItmsGrpCod=" + CmbLin.SelectedValue.ToString
      End If

      sCadena &= " group by  det.ItemCode, art.ItmsGrpCod "

      Dim da As New SqlClient.SqlDataAdapter(sCadena, SqlConnection)

      Dim ds As New DataSet

      da.Fill(ds)
      ds.Tables(0).Rows.Add("TODOS", 0)
      Me.CmbArticulo.DataSource = ds.Tables(0)
      Me.CmbArticulo.DisplayMember = "Linea "
      Me.CmbArticulo.ValueMember = "codigo"
      Me.CmbArticulo.SelectedValue = "TODOS"

    Catch ex As Exception

    End Try

  End Sub

  Private Sub mCargaPoveedor(ByVal cadena As String)
    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
      Try
        Dim sCadena As String
        sCadena = " Select Prov.Cardcode Codigo,prov.CardName +' ('+prov.CardCode+')' Nombre  " +
                                                        " from OPCH fac" +
                                                        "	inner join pch1 det on Fac.DocEntry=det.DocEntry " +
                                                        "	inner join OCRD Prov on fac.CardCode= prov.CardCode " +
                                                        "   inner join OITM art on det.ItemCode= art.ItemCode "
        Select Case cadena
          Case "Articulo"
            sCadena &= " where det.ItemCode= '" + CmbArticulo.Text + "' "
          Case "Linea"
            sCadena &= "  where art.ItmsGrpCod= " + CmbLin.SelectedValue.ToString
          Case String.Empty
            sCadena &= " "
        End Select

        sCadena &= " group by  prov.CardName, Prov.Cardcode  "
        sCadena &= " order by prov.CardName +'    ('+prov.CardCode+')'  "

        Dim da As New SqlClient.SqlDataAdapter(sCadena, SqlConnection)

        Dim ds As New DataSet
        da.Fill(ds)
        ds.Tables(0).Rows.Add(0, "TODOS")
        Me.cmbProveedor.DataSource = ds.Tables(0)
        Me.cmbProveedor.DisplayMember = "Nombre"
        Me.cmbProveedor.ValueMember = "Codigo"
        cmbProveedor.SelectedValue = 0

      Catch ex As Exception

      End Try

    End Using

  End Sub

  Private Sub mGeneraExcel()
    Try
      Dim exApp As New Microsoft.Office.Interop.Excel.Application
      Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

      'Añadimos el Libro al programa
      exLibro = exApp.Workbooks.Add

      ' ¿Cuantas columnas y cuantas filas?
      Dim NCol As Integer = DgDifCompras.ColumnCount
      Dim NRow As Integer = DgDifCompras.RowCount

      ''Combinamos celdas
      exLibro.Worksheets("Hoja1").Cells.Range("A1:g1").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("A2:g2").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("A3:g3").Merge(True)
      exLibro.Worksheets("Hoja1").Cells.Range("A4:g4").Merge(True)

      ''aplicamos un color de fondo ala celda o rango de celdas
      exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15
      exLibro.Worksheets("Hoja1").Cells.Range("A4").Interior.ColorIndex = 15

      '************
      exLibro.Worksheets("Hoja1").Columns("E").NumberFormat = "@" 'Articulo'
      exLibro.Worksheets("Hoja1").Columns("P").NumberFormat = "##0.#0 %" 'Articulo'
      exLibro.Worksheets("Hoja1").Columns("I").NumberFormat = "$ ###,###,###.0000" 'Articulo'
      exLibro.Worksheets("Hoja1").Columns("K").NumberFormat = "$ ###,##0.00" 'Articulo'
      exLibro.Worksheets("Hoja1").Columns("N").NumberFormat = "$ ###,##0.00" 'Articulo'
      exLibro.Worksheets("Hoja1").Columns("O").NumberFormat = "$ ###,##0.00" 'Articulo'

      exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 4 'Factura'
      exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 5 'Factura'
      exLibro.Worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 9 'Fecha'
      exLibro.Worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 9 'Fecha cont'
      exLibro.Worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 10 'Articulo'
      exLibro.Worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 12 'Descripcion'
      exLibro.Worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 8 'Linea'
      exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 4 'Cantidad'
      'exLibro.Worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 10
      'exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 7
      exLibro.Worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 9
      exLibro.Worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 4
      exLibro.Worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 9
      exLibro.Worksheets("Hoja1").Columns("L").EntireColumn.ColumnWidth = 6
      exLibro.Worksheets("Hoja1").Columns("M").EntireColumn.ColumnWidth = 6
      exLibro.Worksheets("Hoja1").Columns("N").EntireColumn.ColumnWidth = 9
      exLibro.Worksheets("Hoja1").Columns("O").EntireColumn.ColumnWidth = 9 'Diferencia'
      exLibro.Worksheets("Hoja1").Columns("P").EntireColumn.ColumnWidth = 9 'Diferencia en porcentajes'
      exLibro.Worksheets("Hoja1").Columns("Q").EntireColumn.ColumnWidth = 15 'Factor L9'
      exLibro.Worksheets("Hoja1").Columns("R").EntireColumn.ColumnWidth = 10 'Comentarios'
      exLibro.Worksheets("Hoja1").Columns("S").EntireColumn.ColumnWidth = 8 'Prov. Compras'
      exLibro.Worksheets("Hoja1").Columns("T").EntireColumn.ColumnWidth = 8 'Cod. Proveedor'
      exLibro.Worksheets("Hoja1").Columns("U").EntireColumn.ColumnWidth = 8 'Prov. Predetermiando'
      exLibro.Worksheets("Hoja1").Columns("V").EntireColumn.ColumnWidth = 8 'Cod. Prov. Predeterminado

      '************

      ''Cambiamos orientacion ala hola
      exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Reporte diferencia de precios"
      exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Articulo: " + CmbArticulo.Text + "  Linea: " + CmbLin.Text
      exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Del: " + DtpFechaIni.Value
      exLibro.Worksheets("Hoja1").Cells.Item(4, 1) = "Al: " + DtpFechaFin.Value

      exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Cells.Item(4, 1).Font.Bold = 1

      'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
      Dim i_aux As Integer = 1
      For i As Integer = 1 To NCol
        If DgDifCompras.Columns(i - 1).Visible = True Then
          exLibro.Worksheets("Hoja1").Cells.Item(6, i_aux) = DgDifCompras.Columns(i - 1).HeaderText.ToString
          i_aux = i_aux + 1
        End If

      Next

      For Fila As Integer = 0 To NRow - 1
        i_aux = 2
        If DgDifCompras.Rows(Fila).Cells(0).Value = 0 Then
          exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, 1) = "NO"

        Else
          exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, 1) = "SI"
          exLibro.Worksheets("Hoja1").Cells.Range("A" & (Fila + 7).ToString & ":V" & (Fila + 7).ToString).interior.color = Color.LightPink
        End If

        For Col As Integer = 1 To NCol - 1
          If DgDifCompras.Columns(Col).Visible = True Then

            exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, i_aux) = DgDifCompras.Rows(Fila).Cells(Col).Value
            i_aux = i_aux + 1
          End If
        Next
        Estatus.Visible = True
        ProgressBar1.Value = (Fila * 100) / NRow

      Next
      Estatus.Visible = False
      exLibro.Worksheets("Hoja1").Cells.Range("A6:V6").WrapText = True
      exLibro.Worksheets("Hoja1").Rows.Item(6).VerticalAlignment = 2
      exLibro.Worksheets("Hoja1").Cells.Range("A6:V" & (NRow + 7).ToString).Font.Size = 9

      'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
      exLibro.Worksheets("Hoja1").Rows.Item(6).Font.Bold = 1
      exLibro.Worksheets("Hoja1").Rows.Item(6).HorizontalAlignment = 3
      exLibro.Worksheets("Hoja1").Rows.Item(6).Interior.ColorIndex = 15
      'exLibro.Worksheets("Hoja1").Columns.AutoFit()
      exLibro.Worksheets("Hoja1").name = "Reporte Dif de Precios"

      'Aplicación visible
      exLibro.Worksheets.Application.Visible = True

      exLibro = Nothing
      exApp = Nothing

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Sub Export()
    Dim wb As New XLWorkbook()
    Dim ws = wb.Worksheets.Add("Hoja 1")
    Dim index As Integer = 7
    Dim chk As String
    'DETALLE DEL REPORTE
    ws.Cell("A1").Value = "Reporte diferencia de precios"
    ws.Range("A1:G1").Merge()
    ws.Cell("A1").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("A2").Value = "Articulo: TODOS  Linea: TODOS"
    ws.Range("A2:G2").Merge()
    ws.Cell("A2").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("A3").Value = "Del: 01/10/2019"
    ws.Range("A3:G3").Merge()
    ws.Cell("A3").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("A4").Value = "Al: 25/10/2019"
    ws.Range("A4:G4").Merge()
    ws.Cell("A4").Style.Fill.BackgroundColor = XLColor.Gray

    'ENCABEZADO DEL REPORTE
    ws.Cell("A6").Value = "Revisar"
    ws.Cell("A6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("B6").Value = "Factura"
    ws.Cell("B6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("C6").Value = "Fecha"
    ws.Cell("C6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("D6").Value = "Fch. Cont."
    ws.Cell("D6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("E6").Value = "Articulo"
    ws.Cell("E6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("F6").Value = "Descripcion"
    ws.Cell("F6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("G6").Value = "Linea"
    ws.Cell("G6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("H6").Value = "Cantidad"
    ws.Cell("H6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("I6").Value = "Precio L8"
    ws.Cell("I6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("J6").Value = "Moneda"
    ws.Cell("J6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("K6").Value = "Precio compra"
    ws.Cell("K6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("L6").Value = "Desc. por articulo"
    ws.Cell("L6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("M6").Value = "Desc. general"
    ws.Cell("M6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("N6").Value = "Precio total"
    ws.Cell("N6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("O6").Value = "Diferencia"
    ws.Cell("O6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("P6").Value = "Dif. en %"
    ws.Cell("P6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("Q6").Value = "Factor L09"
    ws.Cell("Q6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("R6").Value = "Comentarios Compras"
    ws.Cell("R6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("S6").Value = "Comentarios_Direccion"
    ws.Cell("S6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("T6").Value = "Prov. Compra"
    ws.Cell("T6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("U6").Value = "Cod. Proveedor"
    ws.Cell("U6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("V6").Value = "Pro. Predet."
    ws.Cell("V6").Style.Fill.BackgroundColor = XLColor.Gray
    ws.Cell("W6").Value = "Cod. Pro. Pred."
    ws.Cell("W6").Style.Fill.BackgroundColor = XLColor.Gray

    For Each fila As DataGridViewRow In DgDifCompras.Rows
      If fila.Cells(0).Value.ToString() = "1" Then
        chk = "SI"
      Else
        chk = "NO"
      End If
      ws.Cell("A" + index.ToString).Value = chk
      ws.Cell("B" + index.ToString).Value = fila.Cells(1).Value.ToString()
      ws.Cell("C" + index.ToString).Value = fila.Cells(2).Value.ToString()
      ws.Cell("D" + index.ToString).Value = fila.Cells(3).Value.ToString()
      ws.Cell("E" + index.ToString).Value = fila.Cells(4).Value.ToString()
      ws.Cell("F" + index.ToString).Value = fila.Cells(5).Value.ToString()
      ws.Cell("G" + index.ToString).Value = fila.Cells(6).Value.ToString()
      ws.Cell("H" + index.ToString).Value = fila.Cells(7).Value.ToString()
      ws.Cell("I" + index.ToString).Value = fila.Cells(8).Value.ToString()
      ws.Cell("J" + index.ToString).Value = fila.Cells(9).Value.ToString()
      ws.Cell("K" + index.ToString).Value = fila.Cells(10).Value.ToString()
      ws.Cell("L" + index.ToString).Value = fila.Cells(11).Value.ToString()
      ws.Cell("M" + index.ToString).Value = fila.Cells(12).Value.ToString()
      ws.Cell("N" + index.ToString).Value = fila.Cells(13).Value.ToString()
      ws.Cell("O" + index.ToString).Value = fila.Cells(14).Value.ToString()
      ws.Cell("P" + index.ToString).Value = fila.Cells(15).Value.ToString()
      ws.Cell("Q" + index.ToString).Value = fila.Cells(16).Value.ToString()
      ws.Cell("R" + index.ToString).Value = fila.Cells(17).Value.ToString()
      ws.Cell("S" + index.ToString).Value = fila.Cells(18).Value.ToString()
      ws.Cell("T" + index.ToString).Value = fila.Cells(19).Value.ToString()
      ws.Cell("U" + index.ToString).Value = fila.Cells(20).Value.ToString()
      ws.Cell("V" + index.ToString).Value = fila.Cells(21).Value.ToString()
      ws.Cell("W" + index.ToString).Value = fila.Cells(22).Value.ToString()
      index = index + 1
    Next

    Dim saveFileDialog1 As New SaveFileDialog()
    saveFileDialog1.Filter = "Excel|*.xlsx"
    saveFileDialog1.Title = "Save Excel File"
    saveFileDialog1.FileName = "Export_" & DgDifCompras.Name.ToString() & ".xlsx"
    saveFileDialog1.ShowDialog()
    saveFileDialog1.InitialDirectory = "C:/"

    If saveFileDialog1.FileName <> "" Then
      Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
      fs.Close()
    End If

    Dim strFileName As String = saveFileDialog1.FileName
    wb.SaveAs(strFileName)

  End Sub

#End Region

#Region "Funciones"

  Private Function fValidaFechas() As Boolean

    If (DtpFechaIni.Value > DtpFechaFin.Value) Then
      MsgBox("La fecha final no puede ser mayor ala fecha inicial.", MsgBoxStyle.Exclamation, "Tracto Partes Diamante")
      eIni.Visible = True
      eFin.Visible = True
    Else
      eIni.Visible = False
      eFin.Visible = False
    End If

  End Function

  Private Sub fFormatoGrid(ByRef ds As DataSet)
    Try
      With DgDifCompras
        Resultado.Table = ds.Tables(0)
        If CheckRevisados.Checked = False Then
          Resultado.RowFilter = String.Empty
        Else
          Resultado.RowFilter = "Revisar = 1"
        End If

        '.DataSource = ds.Tables(0)
        .DataSource = Resultado

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        .Columns().Remove("Revisar")
        Dim col As New DataGridViewCheckBoxColumn
        col.DataPropertyName = "Revisar"
        col.HeaderText = "Revisar"
        col.Name = "Revisar"
        col.SortMode = DataGridViewColumnSortMode.Automatic
        '.Columns.Add(col)
        .Columns.Insert(0, col)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        .ReadOnly = True

        'Color de Renglones en Grid
        .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        .ColumnHeadersHeight = 39
        '39
        .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        '.RowHeadersVisible = False
        .RowHeadersWidth = 20


        ''Revisar
        .Columns(0).Width = 40
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '.Columns(0).ReadOnly = True

        ''Factura
        .Columns(1).Width = 40
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(1).ReadOnly = True

        ' fecha
        .Columns(2).Width = 63
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(2).ReadOnly = True

        ' fecha contabilizacion
        .Columns(3).HeaderText = "Fch. Cont."
        .Columns(3).Width = 63
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(3).ReadOnly = True

        'Articulo	
        .Columns(4).Width = 68
        .Columns(4).ReadOnly = True

        ''Descripcion
        .Columns(5).Width = 125
        .Columns(5).ReadOnly = True

        '' Linea
        .Columns(6).Width = 70
        '.Columns(6).DefaultCellStyle.Format = "###,###"
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        .Columns(6).ReadOnly = True

        ' Cantidad
        .Columns(7).Width = 35
        .Columns(7).DefaultCellStyle.Format = "###,###"
        .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(7).ReadOnly = True

        ' Precio L8
        .Columns(8).Width = 65
        .Columns(8).DefaultCellStyle.Format = "$ ###,###,###.0000"
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(8).ReadOnly = True

        ' Moneda
        .Columns(9).Width = 40
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(9).ReadOnly = True

        ' Precio compra 
        .Columns(10).Width = 65
        .Columns(10).DefaultCellStyle.Format = "$ ###,###.#0"
        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(10).ReadOnly = True

        ' Descuento por articulo
        .Columns(11).HeaderText = "Desc. por articulo"
        .Columns(11).Width = 50
        .Columns(11).DefaultCellStyle.Format = "##0.#0 %"
        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(11).ReadOnly = True


        ' descuento general
        .Columns(12).HeaderText = "Desc. general"
        .Columns(12).Width = 50
        .Columns(12).DefaultCellStyle.Format = "##0.#0 %"
        .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(12).ReadOnly = True

        ' precio total 
        .Columns(13).HeaderText = "Precio total"
        .Columns(13).Width = 65
        .Columns(13).DefaultCellStyle.Format = "$ ###,###.#0"
        .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(13).ReadOnly = True


        ' Diferencia 
        .Columns(14).Width = 65
        .Columns(14).DefaultCellStyle.Format = "$ ###,###.#0"
        .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(14).ReadOnly = True

        ' Diferencia porcentaje
        .Columns(15).HeaderText = "Dif. en %"
        .Columns(15).Width = 65
        .Columns(15).DefaultCellStyle.Format = "##0.#0 %"
        .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(15).ReadOnly = True

        .Columns(16).Width = 42
        .Columns(16).DefaultCellStyle.Format = "###,###,###.0000"
        .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(17).Width = 77
        .Columns(17).DefaultCellStyle.Format = "$ ###,###,###.00"
        .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(17).Visible = False

        .Columns(18).Width = 49
        .Columns(18).DefaultCellStyle.Format = "###,###,###.0000"
        .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(18).Visible = False

        'Comentarios
        .Columns(19).HeaderText = "Comentarios Compras"
        .Columns(19).Width = 135
        .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        .Columns(19).ReadOnly = True

        ' Proveedor Compras 135
        .Columns(20).Width = 135
        .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        .Columns(20).ReadOnly = True

        'Code Proveedor Compra 125
        .Columns(21).Width = 125
        .Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(21).ReadOnly = True

        ' Proveedor Predeterminado 50
        .Columns(22).Width = 50
        .Columns(22).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        .Columns(22).ReadOnly = True

        'Code Proveedor Predeterminado 125
        .Columns(23).Width = 125
        .Columns(23).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(23).ReadOnly = True

        '
        .Columns(24).Width = 50
        .Columns(24).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(24).ReadOnly = True



        Dim numfilas As Integer

        numfilas = DgDifCompras.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)

                    If DgDifCompras.Item(14, i).Value < 0 Then
                        DgDifCompras.Rows(i).Cells(14).Style.ForeColor = Color.Red
                    ElseIf DgDifCompras.Item(14, i).Value > 0 Then
                        DgDifCompras.Rows(i).Cells(14).Style.ForeColor = Color.Black
          End If

                    If DgDifCompras.Item(15, i).Value < 0 Then
                        DgDifCompras.Rows(i).Cells(15).Style.ForeColor = Color.Red
                    ElseIf DgDifCompras.Item(15, i).Value > 0 Then
                        DgDifCompras.Rows(i).Cells(15).Style.ForeColor = Color.Black

          End If

        Next






      End With

    Catch ex As Exception
      MsgBox("Error al aplicar algun estilo al grid " & ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
    End Try
  End Sub





#End Region

    Private Sub CBFecConta_CheckStateChanged(sender As Object, e As EventArgs) Handles CBFecConta.CheckStateChanged
    If CBFecConta.Checked Then
      CBFecDoc.Checked = False
    End If
  End Sub

  Private Sub CBFecDoc_CheckStateChanged(sender As Object, e As EventArgs) Handles CBFecDoc.CheckStateChanged
    If CBFecDoc.Checked Then
      CBFecConta.Checked = False
    End If
  End Sub

  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

  End Sub

  Private Sub DgDifCompras_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgDifCompras.ColumnHeaderMouseClick
    Dim numfilas As Integer

    numfilas = DgDifCompras.RowCount 'cuenta las filas del DataGrid

    'recorre las filas del DataGrid
    For i = 0 To (numfilas - 1)

            If DgDifCompras.Item(14, i).Value < 0 Then
                DgDifCompras.Rows(i).Cells(14).Style.ForeColor = Color.Red
            ElseIf DgDifCompras.Item(14, i).Value > 0 Then
                DgDifCompras.Rows(i).Cells(14).Style.ForeColor = Color.Black
      End If

            If DgDifCompras.Item(15, i).Value < 0 Then
                DgDifCompras.Rows(i).Cells(15).Style.ForeColor = Color.Red
            ElseIf DgDifCompras.Item(15, i).Value > 0 Then
                DgDifCompras.Rows(i).Cells(15).Style.ForeColor = Color.Black

      End If

    Next
  End Sub

  'MODIFICADO POR IVAN GONZALEZ
  Public TipoCom As String
  Private Sub DgDifCompras_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgDifCompras.CellDoubleClick, DgDifCompras.CellContentDoubleClick

    If Me.DgDifCompras.Columns(e.ColumnIndex).Name = "Comentarios" Then
      TipoCom = "C"
      DPFactura = DgDifCompras.Item(1, DgDifCompras.CurrentRow.Index).Value
      DPFecFact = DgDifCompras.Item(2, DgDifCompras.CurrentRow.Index).Value
      DPArticulo = DgDifCompras.Item(4, DgDifCompras.CurrentRow.Index).Value
      DPDescripcion = DgDifCompras.Item(5, DgDifCompras.CurrentRow.Index).Value
      DPLinea = DgDifCompras.Item(6, DgDifCompras.CurrentRow.Index).Value
      DPProveedor = DgDifCompras.Item(20, DgDifCompras.CurrentRow.Index).Value
      DPComentarios = DgDifCompras.Item(19, DgDifCompras.CurrentRow.Index).Value

      DPPosRen = DgDifCompras.CurrentRow.Index

      Dim frm As New DifPreciosComentarios()
      '    'Mostramos el formulario Form2.
      frm.ShowDialog()
    ElseIf Me.DgDifCompras.Columns(e.ColumnIndex).Name = "Comentarios_Direccion" And UsrTPM = "MANAGER" Then
      TipoCom = "D"
      DPFactura = DgDifCompras.Item(1, DgDifCompras.CurrentRow.Index).Value
      DPFecFact = DgDifCompras.Item(2, DgDifCompras.CurrentRow.Index).Value
      DPArticulo = DgDifCompras.Item(4, DgDifCompras.CurrentRow.Index).Value
      DPDescripcion = DgDifCompras.Item(5, DgDifCompras.CurrentRow.Index).Value
      DPLinea = DgDifCompras.Item(6, DgDifCompras.CurrentRow.Index).Value
      DPProveedor = DgDifCompras.Item(20, DgDifCompras.CurrentRow.Index).Value
      DPComentariosDir = DgDifCompras.Item(20, DgDifCompras.CurrentRow.Index).Value

      DPPosRen = DgDifCompras.CurrentRow.Index

      Dim frm As New FrmDiferenciaPrecioComentarioDireccion()
      '    'Mostramos el formulario Form2.
      frm.ShowDialog()
    End If

  End Sub


  Private Sub DgDifCompras_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

  End Sub

  Private Sub CheckRevisados_CheckedChanged(sender As Object, e As EventArgs) Handles CheckRevisados.CheckedChanged
    If CheckRevisados.Checked = False Then
      Resultado.RowFilter = String.Empty
    Else
      Resultado.RowFilter = "Revisar = 1 "
    End If

    Dim numfilas As Integer

    numfilas = DgDifCompras.RowCount 'cuenta las filas del DataGrid

    For i = 0 To (numfilas - 1)

            If DgDifCompras.Item(14, i).Value < 0 Then
                DgDifCompras.Rows(i).Cells(14).Style.ForeColor = Color.Red
            ElseIf DgDifCompras.Item(14, i).Value > 0 Then
                DgDifCompras.Rows(i).Cells(14).Style.ForeColor = Color.Black
      End If

            If DgDifCompras.Item(15, i).Value < 0 Then
                DgDifCompras.Rows(i).Cells(15).Style.ForeColor = Color.Red
            Else IF DgDifCompras.Item(15, i).Value > 0 Then
                DgDifCompras.Rows(i).Cells(15).Style.ForeColor = Color.Black

            End If

    Next

  End Sub

  Private Sub DgDifCompras_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DgDifCompras.RowPrePaint
    Try
      If DgDifCompras.Rows(e.RowIndex).Cells("Revisar").Value = 1 Then
        'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
        DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightPink
        'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
      Else
        DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
        'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
      End If
      'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightPink
      'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightPink
      'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightPink

      DgDifCompras.Rows(e.RowIndex).Cells("Factor L09").Style.BackColor = Color.Gold
      'DgDifCompras.Rows(e.RowIndex).Cells("Precio L01").Style.BackColor = Color.DarkGray
      'DgDifCompras.Rows(e.RowIndex).Cells("Factor L01").Style.BackColor = Color.Yellow
      DgDifCompras.Rows(e.RowIndex).Cells(14).Style.BackColor = Color.LightGray
    Catch ex As Exception
      MessageBox.Show("¡Error al cambiar de color en la tabla! " + ex.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try



  End Sub
  Dim SQL As New Comandos_SQL()
  Private Sub DgDifCompras_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DgDifCompras.CellContentClick
    'Creado por Ivan Gonzalez
    SQL.conectarTPM()
    If Me.DgDifCompras.Columns(e.ColumnIndex).Name = "Revisar" Then
      If DgDifCompras.Rows(e.RowIndex).Cells("Revisar").Value = 0 Then
        DgDifCompras.Rows(e.RowIndex).Cells("Revisar").Value = 1

        If SQL.SiExiste("SELECT * FROM DifPreComen where Factura = " + DgDifCompras.Rows(e.RowIndex).Cells("Factura").Value.ToString() + " AND FecFact = '" + Convert.ToDateTime(DgDifCompras.Rows(e.RowIndex).Cells("Fecha").Value).ToString("yyyy-MM-dd") +
                                "' AND Articulo = '" + DgDifCompras.Rows(e.RowIndex).Cells("Articulo").Value.ToString() + "'") Then

          If SQL.EjecutarComando("UPDATE DifPreComen SET Revisar = 1 WHERE Factura = " + DgDifCompras.Rows(e.RowIndex).Cells("Factura").Value.ToString() + " AND FecFact = '" + Convert.ToDateTime(DgDifCompras.Rows(e.RowIndex).Cells("Fecha").Value).ToString("yyyy-MM-dd") +
                                            "' AND Articulo = '" + DgDifCompras.Rows(e.RowIndex).Cells("Articulo").Value.ToString() + "'") Then

          Else
            MessageBox.Show("¡Error en EjecutaComando!", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
          End If
        Else
          If SQL.EjecutarComando("INSERT INTO DifPreComen (Factura,FecFact,Articulo,Revisar) VALUES(" + DgDifCompras.Rows(e.RowIndex).Cells("Factura").Value.ToString() + ",'" +
                                            Convert.ToDateTime(DgDifCompras.Rows(e.RowIndex).Cells("Fecha").Value).ToString("yyyy-MM-dd") + "','" + DgDifCompras.Rows(e.RowIndex).Cells("Articulo").Value.ToString() + "',1)") Then

          Else
            MessageBox.Show("¡Error en EjecutaComando!", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
          End If
        End If

      Else
        DgDifCompras.Rows(e.RowIndex).Cells("Revisar").Value = 0

        If SQL.SiExiste("SELECT * FROM DifPreComen where Factura = " + DgDifCompras.Rows(e.RowIndex).Cells("Factura").Value.ToString() + " AND FecFact = '" + Convert.ToDateTime(DgDifCompras.Rows(e.RowIndex).Cells("Fecha").Value).ToString("yyyy-MM-dd") +
                                "' AND Articulo = '" + DgDifCompras.Rows(e.RowIndex).Cells("Articulo").Value.ToString() + "'") Then

          If SQL.EjecutarComando("UPDATE DifPreComen SET Revisar = 0 WHERE Factura = " + DgDifCompras.Rows(e.RowIndex).Cells("Factura").Value.ToString() + " AND FecFact = '" + Convert.ToDateTime(DgDifCompras.Rows(e.RowIndex).Cells("Fecha").Value).ToString("yyyy-MM-dd") +
                                            "' AND Articulo = '" + DgDifCompras.Rows(e.RowIndex).Cells("Articulo").Value.ToString() + "'") Then

          Else
            MessageBox.Show("¡Error en EjecutaComando!", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
          End If
        Else
          If SQL.EjecutarComando("INSERT INTO DifPreComen (Factura,FecFact,Articulo,Revisar) VALUES(" + DgDifCompras.Rows(e.RowIndex).Cells("Factura").Value.ToString() + ",'" +
                                            Convert.ToDateTime(DgDifCompras.Rows(e.RowIndex).Cells("Fecha").Value).ToString("yyyy-MM-dd") + "','" + DgDifCompras.Rows(e.RowIndex).Cells("Articulo").Value.ToString() + "',0)") Then

          Else
            MessageBox.Show("¡Error en EjecutaComando!", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
          End If
        End If

      End If
    End If
  End Sub
End Class