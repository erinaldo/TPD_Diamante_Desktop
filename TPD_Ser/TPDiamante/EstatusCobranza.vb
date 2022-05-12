Public Class EstatusCobranza
    Dim DvLP As New DataView
    Dim DvClte As New DataView


    Private Sub cobranzagral_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ConsutaLista As String


        Dim ctabla As String

        'Se crea cursor para reporte
        ctabla = "SELECT * FROM DEPCOBR_TMP where CbrGralAdicional='N'"

        'PagoMesFac()PagosOtroMes
        Dim cmdcob As Data.SqlClient.SqlCommand
        cmdcob = New Data.SqlClient.SqlCommand()

        With cmdcob
            .Connection = New Data.SqlClient.SqlConnection(StrTpm)
            .Connection.Open()
            .CommandText = ctabla
        End With


        Dim DAdapter As New SqlClient.SqlDataAdapter(cmdcob)

        Dim DtPagoCob As New DataTable
        DAdapter.Fill(DtPagoCob)

        cmdcob.Connection.Close()


        '/*******************************************************


        ctabla = "CREATE TABLE #AGTECOB (SlpCode Int,IdCobranza Varchar(10))"

        Dim cmdordena As Data.SqlClient.SqlCommand
        cmdordena = New Data.SqlClient.SqlCommand()
        With cmdordena
            .Connection = New Data.SqlClient.SqlConnection(StrCon)
            .Connection.Open()
            .CommandText = ctabla
            .ExecuteNonQuery()
        End With

        Dim strcadena As String

        For Each fila As DataRow In DtPagoCob.Rows

            strcadena = "INSERT INTO #AGTECOB (SlpCode, IdCobranza) VALUES ("
            strcadena &= fila("SlpCode")
            strcadena &= ",'"
            strcadena &= fila("IdCobranza")
            strcadena &= "')"

            With cmdordena
                .CommandText = strcadena
                .ExecuteNonQuery()
            End With
        Next

        With cmdordena
            .CommandText = "SELECT OSLP.slpcode,OSLP.slpname,#AGTECOB.IdCobranza FROM OSLP,#AGTECOB WHERE OSLP.slpcode = #AGTECOB.slpcode ORDER BY slpname"
            .ExecuteNonQuery()
        End With


        Dim DaAgteCob As New SqlClient.SqlDataAdapter(cmdordena)

        Dim DsAgtesCob As New DataSet
        DaAgteCob.Fill(DsAgtesCob)

        DvLP.Table = DsAgtesCob.Tables(0)
        Me.CmbAgteVta.DataSource = DvLP
        Me.CmbAgteVta.DisplayMember = "slpname"
        Me.CmbAgteVta.ValueMember = "slpcode"
        Me.CmbAgteVta.SelectedValue = "0"

        With cmdordena
            .CommandText = "SELECT CardCode,CardName, SlpCode FROM OCRD WHERE CardType = 'C' ORDER BY SlpCode,CardName"
            '.ExecuteNonQuery()
        End With

        Dim DaClte As New SqlClient.SqlDataAdapter(cmdordena)

        Dim DsClte As New DataSet
        DaClte.Fill(DsClte)

        DvClte.Table = DsClte.Tables(0)
        Me.CmbCliente.DataSource = DvClte
        Me.CmbCliente.DisplayMember = "CardName"
        Me.CmbCliente.ValueMember = "CardCode"
        Me.CmbCliente.SelectedValue = ""

        cmdordena.Connection.Close()

        '**********************************************************************************

        If UsrTPM = "COBRANZ1" Or UsrTPM = "COBRANZ2" Or UsrTPM = "COBRANZ3" Or UsrTPM = "COBRANZ4" Or UsrTPM = "COBRANZ5" Then

            ConsutaLista = "SELECT IdCobranza,Nombre FROM DEPCOB WHERE IdCobranza ='" + UsrTPM + "' " + "ORDER BY Nombre"

        Else

            ConsutaLista = "SELECT IdCobranza,Nombre FROM DEPCOB ORDER BY Nombre"
        End If


        ''ConsutaLista = "SELECT IdCobranza,Nombre FROM DEPCOB ORDER BY Nombre"
        Dim daACob As New SqlClient.SqlDataAdapter(ConsutaLista, StrTpm)

        Dim dsACob As New DataSet
        daACob.Fill(dsACob)

        Dim fila2 As Data.DataRow

        If UsrTPM = "MANAGER" Then
            'Asignamos a fila la nueva Row(Fila)del Dataset
            fila2 = dsACob.Tables(0).NewRow

            'Agregamos los valores a los campos de la tabla
            fila2("IdCobranza") = "TODOS"
            fila2("Nombre") = "TODOS"

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            dsACob.Tables(0).Rows.Add(fila2)

        End If

        Me.CmbAgteCob.DataSource = dsACob.Tables(0)
        Me.CmbAgteCob.DisplayMember = "Nombre"
        Me.CmbAgteCob.ValueMember = "IdCobranza"
        Me.CmbAgteCob.SelectedValue = ""
        Me.CmbAgteCob.SelectedValue = "TODOS"

    End Sub
    Sub ValidaNave()
        If Me.CmbAgteCob.SelectedValue = "TODOS" Then
            DvLP.RowFilter = String.Empty
        Else
            DvLP.RowFilter = "IdCobranza = " & "'" & Me.CmbAgteCob.SelectedValue & "'"
        End If

        CmbAgteVta.SelectedValue = "0"
        'If Me.CmbAgteCob.SelectedValue <> "" Then
        '    DvLP.RowFilter = "IdCobranza = " & "'" & Me.CmbAgteCob.SelectedValue & "'"

        'Else
        '    DvLP.RowFilter = String.Empty

        'End If

    End Sub

    Sub BuscaCltes()
        'Dim v As String
        'v = Me.CmbAgteVta.SelectedValue.ToString

        'v = "slpcode = " & "'" & Trim(Me.CmbAgteVta.SelectedValue.ToString) & "'"
        If CmbAgteVta.SelectedValue Is Nothing Then
            DvClte.RowFilter = "slpcode = 0"
        Else
            DvClte.RowFilter = "slpcode = " & "'" & Trim(Me.CmbAgteVta.SelectedValue.ToString) & "'"
        End If

        CmbCliente.SelectedValue = 0

    End Sub


    Sub cargar_registros2()

        Dim DTMObra As New DataTable
        Dim informe As New CrCGral2

        Dim Consulta As String = ""
        Dim strcadena As String = ""



        Consulta = "SELECT T9.DocNum AS DocSAP,T9.EDocNum AS FactFiscal,T9.CardCode,t9.CardName,T9.DocDate as FchDoc,T9.DocDueDate as FchVenci,"
        Consulta &= "CAST(0 AS numeric(19,6)) as DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,CAST(0 AS numeric(19,6)) AS SaldoPendiene,T9.NumAtCard AS FactSaldo,"
        Consulta &= "t9.Comments,CAST(0 AS int) as IdPago,T9.EDocPrefix as TipoMov,"
        Consulta &= "CASE "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'I' THEN 'CANCELACION' "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'S'  THEN 'DESCUENTO' "
        Consulta &= "WHEN T9.EDocPrefix = 'ND' THEN 'DEVOLUCION' "
        Consulta &= "END AS Movimiento,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS Cargo,CASE WHEN T9.DocTotal - T9.PaidToDate > 0 THEN T9.DocTotal - T9.PaidToDate ELSE T9.DocTotal END AS Abono,CAST(3 AS int) as Orden,"
        Consulta &= "T9.DocNum as NumNota,T10.DocTotal - T10.PaidToDate as MPendiene,T10.DocDate as FchFFact "
        Consulta &= "into #TmpNotaCSAP "
        Consulta &= "FROM ORIN T9, OINV T10 WHERE T10.SlpCode = @RAgente AND T10.CardCode = T9.CardCode AND "
        Consulta &= "RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.DocNum AS nvarchar(100)))) "
        Consulta &= "AND T10.DocTotal - T10.PaidToDate > 0 AND T10.Series <> 48"

        'T10.Series <> 48 se usa para eliminar las notas de credito canceladas con nota de cargo
        'por ejemplo nota de cargo 1000000053 y nota de credito 2154

        Consulta &= "SELECT T8.DocNum AS DocSAP,T8.EDocNum AS FactFiscal,T8.CardCode,T8.CardName,T8.DocDate as FchDoc,T8.DocDueDate as FchVenci,"
        Consulta &= "T8.DocTotal,T8.PaidToDate AS Aplicado,T8.DocTotal - T8.PaidToDate as SaldoPendiene,T8.NumAtCard AS FactSaldo,CAST('' AS nvarchar(254)) AS Comments,"
        Consulta &= "CAST(0 AS int) as IdPago,CAST('' AS nvarchar(10)) AS TipoMov, 'FACTURA' AS Movimiento,"
        Consulta &= "T8.DocTotal AS Cargo,CAST(0 AS numeric(19,6)) AS Abono,CAST(1 AS int) as Orden,CAST(0 AS int) as NumNota,T8.DocTotal - T8.PaidToDate as MPendiene,T8.DocDate as FchFFact "
        Consulta &= "into #TmpFacPag "
        Consulta &= "FROM OINV T8 WHERE T8.SlpCode = @RAgente AND T8.DocTotal - T8.PaidToDate > 0 "

        Consulta &= " UNION ALL "

        Consulta &= "SELECT T8.DocNum AS DocSAP,T8.EDocNum AS FactFiscal,T6.CardCode,T6.CardName,T6.DocDate as FchDoc,T6.DocDueDate as FchVenci,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,CAST(0 AS numeric(19,6)) as SaldoPendiene,CAST('' AS nvarchar(100)) AS FactSaldo,T6.Comments,"
        Consulta &= "T7.DocNum as IdPago,CAST('' AS nvarchar(10)) AS TipoMov,  'PAGO' AS Movimiento,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS Cargo,T7.SumApplied  AS Abono,CAST(2 AS int) as Orden,CAST(0 AS int) as NumNota,"
        Consulta &= "T8.DocTotal - T8.PaidToDate as MPendiene,T8.DocDate as FchFFact "
        Consulta &= "FROM ORCT T6 INNER JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "
        Consulta &= "INNER JOIN OINV T8 ON T8.DocEntry = T7.DocEntry AND T7.InvType = 13 "
        Consulta &= "WHERE T8.SlpCode = @RAgente AND T6.Canceled = 'N' AND T6.DocTotal <> .01 AND T8.DocTotal - T8.PaidToDate > 0 "

        Consulta &= " UNION ALL "

        Consulta &= "SELECT CAST(FactSaldo AS int) AS DocSAP,FactFiscal,CardCode,CardName,FchDoc,FchVenci,"
        Consulta &= "DocTotal,Aplicado,SaldoPendiene,CAST(DocSAP AS nvarchar(100))AS FactSaldo,"
        Consulta &= "Comments,IdPago,TipoMov,"
        Consulta &= "Movimiento, Cargo, Abono, Orden, NumNota, MPendiene,FchFFact "
        Consulta &= "FROM #TmpNotaCSAP "

        Consulta &= " UNION ALL "

        Consulta &= "SELECT t10.docnum AS DocNum,T9.EDocNum AS FactFiscal,T9.CardCode,t9.CardName,T9.DocDate as FchDoc,T9.DocDueDate as FchVenci,"
        Consulta &= "CAST(0 AS numeric(19,6)) as DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,CAST(0 AS numeric(19,6)) AS SaldoPendiene,CAST(T9.DocNum  AS nvarchar(100))AS FactSaldo,"
        Consulta &= "t9.Comments,CAST(0 AS int) as IdPago,T9.EDocPrefix as TipoMov,"
        Consulta &= "CASE "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'I' THEN 'CANCELACION' "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'S'  THEN 'DESCUENTO' "
        Consulta &= "WHEN T9.EDocPrefix = 'ND' THEN 'DEVOLUCION' "
        Consulta &= "END AS Movimiento,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS Cargo,CASE WHEN T9.DocTotal - T9.PaidToDate > 0 THEN T9.DocTotal - T9.PaidToDate ELSE T9.DocTotal END AS Abono,CAST(3 AS int) as Orden,"
        Consulta &= "T9.DocNum as NumNota,T10.DocTotal - T10.PaidToDate as MPendiene,T10.DocDate as FchFFact "
        Consulta &= "FROM ORIN T9, OINV T10 WHERE T10.SlpCode = @RAgente AND T10.CardCode = T9.CardCode AND "
        Consulta &= "RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.EDocNum AS nvarchar(100)))) "
        Consulta &= "AND T10.DocTotal - T10.PaidToDate > 0  AND T10.Series <> 48 AND T9.DocNum NOT IN (SELECT NumNota FROM #TmpNotaCSAP)"

        Consulta &= " UNION ALL "

        Consulta &= "SELECT t10.docnum AS DocNum,T9.EDocNum AS FactFiscal,T9.CardCode,t9.CardName,T9.DocDate as FchDoc,T9.DocDueDate as FchVenci,"
        Consulta &= "CAST(0 AS numeric(19,6)) as DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,CAST(0 AS numeric(19,6)) AS SaldoPendiene,CAST(T9.DocNum  AS nvarchar(100))AS FactSaldo,"
        Consulta &= "t9.Comments,CAST(0 AS int) as IdPago,T9.EDocPrefix as TipoMov,"
        Consulta &= "CASE "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'I' THEN 'CANCELACION' "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'S'  THEN 'DESCUENTO' "
        Consulta &= "WHEN T9.EDocPrefix = 'ND' THEN 'DEVOLUCION' "
        Consulta &= "END AS Movimiento,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS Cargo,CASE WHEN T9.DocTotal - T9.PaidToDate > 0 THEN T9.DocTotal - T9.PaidToDate ELSE T9.DocTotal END AS Abono,CAST(3 AS int) as Orden,"
        Consulta &= "T9.DocNum as NumNota,T10.DocTotal - T10.PaidToDate as MPendiene,T10.DocDate as FchFFact "
        Consulta &= "FROM ORIN T9, OINV T10 WHERE T10.SlpCode = @RAgente AND T10.CardCode = T9.CardCode AND "
        Consulta &= "RTrim(LTrim(T9.NumAtCard)) = RTrim(LTrim(T10.NumAtCard)) "
        Consulta &= "AND T10.DocTotal - T10.PaidToDate > 0 AND T10.Series <> 48 AND T9.DocNum NOT IN (SELECT NumNota FROM #TmpNotaCSAP UNION ALL "
        Consulta &= "Select T9.DocNum "
        Consulta &= "FROM ORIN T9, OINV T10 WHERE T10.SlpCode = 17 AND T10.CardCode = T9.CardCode AND "
        Consulta &= "RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.EDocNum AS nvarchar(100)))) "
        Consulta &= "AND T10.DocTotal - T10.PaidToDate > 0  AND T10.Series <> 48 AND T9.DocNum NOT IN (SELECT NumNota FROM #TmpNotaCSAP)) "

        Consulta &= " UNION ALL "

        Consulta &= "SELECT T8.DocNum AS DocSAP,T8.EDocNum AS FactFiscal,T8.CardCode,T8.CardName,T8.DocDate as FchDoc,T8.DocDueDate as FchVenci,"
        Consulta &= "T8.DocTotal,T8.PaidToDate AS Aplicado,T8.DocTotal - T8.PaidToDate as SaldoPendiene,T8.NumAtCard AS FactSaldo,t8.Comments,"
        Consulta &= "CAST(0 AS int) as IdPago,CAST('' AS nvarchar(10)) AS TipoMov,CASE "
        Consulta &= "WHEN (T8.EDocPrefix = 'NC' or T8.EDocPrefix is null or T8.EDocPrefix = 'F') AND T8.DocType  = 'I' THEN 'CANCELACION' "
        Consulta &= "WHEN (T8.EDocPrefix = 'NC' or T8.EDocPrefix is null or T8.EDocPrefix = 'F') AND T8.DocType  = 'S'  THEN 'DESCUENTO' "
        Consulta &= "WHEN T8.EDocPrefix = 'ND' THEN 'DEVOLUCION' "
        Consulta &= "END AS Movimiento,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS Cargo,CASE WHEN T8.DocTotal - T8.PaidToDate > 0 THEN T8.DocTotal - T8.PaidToDate ELSE T8.DocTotal END AS Abono,CAST(0 AS int) as Orden,T8.DocNum as NumNota,T8.DocTotal - T8.PaidToDate as MPendiene,CAST('20100101' AS datetime) as FchFFact  "
        Consulta &= "FROM ORIN T8 WHERE T8.SlpCode = 17 AND T8.DocStatus = 'O' AND T8.DocNum  NOT IN (SELECT NumNota FROM #TmpNotaCSAP UNION ALL "
        'Amtes t8.doctotal as abono

        Consulta &= "Select T9.DocNum "
        Consulta &= "FROM ORIN T9, OINV T10 WHERE T10.SlpCode = 17 AND T10.CardCode = T9.CardCode AND "
        Consulta &= "RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.EDocNum AS nvarchar(100)))) "
        Consulta &= "AND T10.DocTotal - T10.PaidToDate > 0  AND T10.Series <> 48 AND T9.DocNum NOT IN (SELECT NumNota FROM #TmpNotaCSAP) "

        Consulta &= " UNION ALL "

        Consulta &= "Select T9.DocNum "
        Consulta &= "FROM ORIN T9, OINV T10 WHERE T10.SlpCode = 17 AND T10.CardCode = T9.CardCode AND "
        Consulta &= "RTrim(LTrim(T9.NumAtCard)) = RTrim(LTrim(T10.NumAtCard)) "
        Consulta &= "AND T10.DocTotal - T10.PaidToDate > 0 AND T10.Series <> 48 AND  T9.DocNum NOT IN (SELECT NumNota FROM #TmpNotaCSAP UNION ALL "
        Consulta &= "Select T9.DocNum "
        Consulta &= "FROM ORIN T9, OINV T10 WHERE T10.SlpCode = 17 AND T10.CardCode = T9.CardCode AND "
        Consulta &= "RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.EDocNum AS nvarchar(100)))) "
        Consulta &= "AND T10.DocTotal - T10.PaidToDate > 0  AND T10.Series <> 48 AND T9.DocNum NOT IN (SELECT NumNota FROM #TmpNotaCSAP))"
        Consulta &= ") "


        Consulta &= " SELECT T8.CardCode,T8.DocTotal as TotFact,T8.PaidToDate AS TotAplicado,T8.DocTotal - T8.PaidToDate as TotSaldoP,T8.SlpCode "
        Consulta &= "INTO #SaldoCte_SAP "
        Consulta &= "FROM OINV T8 WHERE T8.SlpCode = @RAgente AND T8.DocTotal - T8.PaidToDate > 0 "
        Consulta &= "UNION ALL "
        Consulta &= "SELECT T9.CardCode,CAST(0 AS numeric(19,6)) AS DocTotal,(DocTotal - PaidToDate) AS TotAplicado,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS TotSaldoP,T9.SlpCode FROM ORIN T9 WHERE T9.SlpCode = @RAgente AND DocStatus = 'O' "


        Consulta &= "SELECT CardCode,sum(TotFact) as TotFact,Sum(TotAplicado) AS TotAplicado,Sum(TotFact - TotAplicado) as TotSaldoP,SlpCode "
        Consulta &= "INTO #SaldoR_SAP FROM #SaldoCte_SAP GROUP BY CardCode,SlpCode "


        Consulta &= "SELECT T1.DocSAP,T1.FactFiscal,T1.FchDoc,T1.FchVenci,"
        Consulta &= "CASE WHEN Movimiento = 'FACTURA' THEN T1.DocTotal ELSE NULL END AS DocTotal,"
        Consulta &= "CASE WHEN Movimiento = 'FACTURA' THEN T1.Aplicado ELSE NULL END AS Aplicado,"
        Consulta &= "CASE WHEN Movimiento = 'FACTURA' THEN T1.SaldoPendiene ELSE NULL END AS SaldoPendiene,"
        Consulta &= "T1.Movimiento,T1.Comments,T1.Cargo,T1.Abono,T1.IdPago,T1.NumNota,T1.MPendiene,T1.CardCode,"
        Consulta &= "T6.CardName,T2.Phone1,T2.ListNum AS ListaP,T3.PymntGroup,TotFact,TotAplicado,TotSaldoP,"
        Consulta &= "CASE WHEN Movimiento = 'FACTURA' THEN DocSAP ELSE NULL END AS FDocSap,"
        Consulta &= "CASE WHEN Movimiento = 'FACTURA' THEN FchDoc ELSE NULL END AS FFchDoc,"
        Consulta &= "CASE WHEN Movimiento = 'FACTURA' THEN FchVenci ELSE NULL END AS FFchVen,"
        Consulta &= "CASE WHEN Movimiento = 'FACTURA' THEN FactFiscal ELSE NULL END AS FFiscal,"
        Consulta &= "T4.SlpCode,T5.SlpName,"
        Consulta &= "CASE WHEN Movimiento <> 'FACTURA' THEN FchDoc ELSE NULL END AS FchMov,CAST(REPLACE(T1.CardCode, 'C-', '') AS int) as NumClte,T1.FchFFact "
        Consulta &= "FROM #TmpFacPag T1,OCRD T2,OCTG T3,#SaldoR_SAP T4,OSLP T5, OCRD T6  "
        Consulta &= "WHERE T1.CardCode = T2.CardCode AND T2.GroupNum = T3.GroupNum AND T1.CardCode = T4.CardCode AND "
        Consulta &= "T5.SlpCode = T4.slpcode AND T1.Movimiento <> 'CANCELACION' AND "
        Consulta &= "T4.TotSaldoP <> 0 AND T1.CardCode = T6.CardCode "
        Consulta &= "ORDER BY NumClte,FchFFact,DocSAP,Orden ASC "
        'AND T4.TotSaldoP <> 0 
        'T4.TotSaldoP <> 0 Se agrega a la consulta para descartar a los clientes que tienen saldo pendiente 0 
        'ya que en la consulta anterior "#SaldoR_SAP" se agregan las notas de credito
        ' y estas pueden ayudar a dejar el saldo del cliente en 0

        Consulta &= " DROP TABLE #TmpFacPag"
        Consulta &= " DROP TABLE #TmpNotaCSAP"
        Consulta &= " DROP TABLE #SaldoR_SAP"
        Consulta &= " DROP TABLE #SaldoCte_SAP"


        'Else

        '    Consulta = "SELECT T0.[ItemCode] AS Codigo,T1.ItemName AS Descripcion,T0.[Price] AS Precio ,"
        '    Consulta &= " T2.[ItmsGrpNam] AS Grupo_Articulo,T0.[PriceList] AS Lista,CAST(0 AS int) as Cod  "
        '    Consulta &= "FROM ITM1 T0 INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode AND T1.frozenFor <> 'Y' INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod "
        '    Consulta &= "WHERE T0.[PriceList] =@lista AND T0.Price > 0 ORDER BY T2.[ItmsGrpNam],T0.[ItemCode]"
        'End If

        Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

        CmdMObra.Parameters.Add("@RAgente", SqlDbType.SmallInt)
        CmdMObra.Parameters("@RAgente").Value = CmbAgteVta.SelectedValue
        'If Me.CmbGrupoArticulo.SelectedValue <> 999 Then
        '    CmdMObra.Parameters.Add("@Codigo", SqlDbType.SmallInt)
        '    CmdMObra.Parameters("@Codigo").Value = CmbGrupoArticulo.SelectedValue
        'End If


        CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
        CmdMObra.Connection.Open()

        Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
        'AdapMObra.Fill(DTMObra)

        'GrdConProd.DataSource = DTMObra

        '/***********************************************/
        Dim DsCob As New DataSet
        AdapMObra.Fill(DsCob)


        Dim DvConsulta As New DataView

        DvConsulta.Table = DsCob.Tables(0)


        If Me.CmbCliente.SelectedValue <> " " And Me.CmbCliente.SelectedValue Is Nothing = False Then
            DvConsulta.RowFilter = "CardCode = " & "'" & Me.CmbCliente.SelectedValue & "'"

        Else
            DvConsulta.RowFilter = String.Empty
        End If


        DTMObra = DvConsulta.ToTable

        ' GrdConProd.DataSource = DTMObra

        '/***********************************************/

        RepComsultaP.MdiParent = Inicio
        informe.SetDataSource(DTMObra)

        RepComsultaP.CrVConsulta.ReportSource = informe
        RepComsultaP.Show()

    End Sub


    Private Sub BtnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprimir.Click
        If IsNothing(CmbAgteCob.SelectedValue) Then
            MessageBox.Show("Seleccione un agente de cobranza", _
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbAgteCob.Focus()
            Return
        End If

        'If IsNothing(CmbAgteVta.SelectedValue) And IsNothing(CmbCliente.SelectedValue) Then
        '    MessageBox.Show("Seleccione un agente de ventas o un Cliente", _
        '    "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    CmbAgteVta.Focus()
        '    Return
        'End If

        If IsNothing(CmbAgteVta.SelectedValue) Then
            MessageBox.Show("Seleccione un agente de ventas", _
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbAgteVta.Focus()
            Return
        End If
        cargar_registros2()

    End Sub

    Private Sub CmbGrupoArticulo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbAgteVta.KeyPress
        e.KeyChar = Char.ToUpper(e.KeyChar)
    End Sub

    Private Sub CmbAgteCob_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CmbAgteCob.Validating
        ValidaNave()
    End Sub

    Private Sub CmbAgteCob_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbAgteCob.SelectionChangeCommitted
        ValidaNave()
    End Sub

    Private Sub CmbAgteCob_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbAgteCob.KeyUp
        ValidaNave()
    End Sub

    Private Sub CmbAgteVta_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CmbAgteVta.Validating
        BuscaCltes()
    End Sub

    Private Sub CmbAgteVta_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbAgteVta.SelectionChangeCommitted
        BuscaCltes()
    End Sub

    Private Sub CmbAgteVta_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbAgteVta.KeyUp
        BuscaCltes()
    End Sub
End Class