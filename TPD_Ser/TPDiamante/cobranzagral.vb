Public Class cobranzagral
 Dim DvLP As New DataView
 Dim DvClte As New DataView

 Dim DvRuta As New DataView
 Dim strTemp As String

 Private Sub cobranzagral_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  Dim ConsutaLista As String

  Dim ctabla As String

  'Se crea cursor para reporte
  ctabla = "SELECT DISTINCT SlpCode,IdCobranza FROM DEPCOBR "

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

   Debug.Print(strcadena)

   With cmdordena
    .CommandText = strcadena
    .ExecuteNonQuery()
   End With
  Next

  With cmdordena
   .CommandText = "SELECT OSLP.slpcode,OSLP.slpname,#AGTECOB.IdCobranza FROM OSLP,#AGTECOB WHERE (OSLP.slpcode = #AGTECOB.slpcode) AND 
                            ((OSLP.U_ESTATUS = 'ACTIVO' OR OSLP.U_ESTATUS = 'INACTIVOCC') OR OSLP.SlpCode = 1) ORDER BY slpname"
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

  '**********************************************************************************
  '**********************************************************************************
  With cmdordena
   .CommandText = "SELECT SlpCode,U_BXP_Ruta AS Ruta FROM SBO_TPD.dbo.OCRD WHERE CardType = 'C'AND  U_BXP_Ruta IS NOT NULL " &
                 "GROUP BY SlpCode,U_BXP_Ruta ORDER BY SlpCode "
   '.ExecuteNonQuery()
  End With

  Dim DaRuta As New SqlClient.SqlDataAdapter(cmdordena)

  Dim DsRuta As New DataSet
  DaRuta.Fill(DsRuta)

  'Dim filaClte As Data.DataRow

  'filaClte = DsRuta.Tables(0).NewRow
  'filaClte("slpcode") = "9999"
  'filaClte("CardName") = "TODOS"
  'filaClte("Slpcode") = "999"

  'DsClte.Tables(0).Rows.Add(filaClte)

  DvRuta.Table = DsRuta.Tables(0)
  Me.CBRuta.DataSource = DvRuta
  Me.CBRuta.DisplayMember = "Ruta"
  Me.CBRuta.ValueMember = "SlpCode"
  Me.CBRuta.SelectedValue = "0"

  '**********************************************************************************
  '**********************************************************************************

  With cmdordena
   .CommandText = "SELECT CardCode, CardName as 'Name', CardCode + ' -> ' + CardName as 'CardName', SlpCode,U_BXP_Ruta AS Ruta, CAST(SUBSTRING(CardCode, CHARINDEX('-', CardCode) + 1, LEN(CardCode)) as VARCHAR) as 'orden' FROM OCRD WHERE CardType = 'C' AND (U_BXP_Estatus IS NULL  or U_BXP_Estatus='04' or U_BXP_Estatus='03' ) ORDER BY CardCode; --CAST(SUBSTRING(CardCode, CHARINDEX('-', CardCode) + 1, LEN(CardCode)) as integer) ASC; "

   '.ExecuteNonQuery()
  End With

  Dim DaClte As New SqlClient.SqlDataAdapter(cmdordena)

  Dim DsClte As New DataSet
  DaClte.Fill(DsClte)



  Dim filaClte As Data.DataRow

  filaClte = DsClte.Tables(0).NewRow
  filaClte("Cardcode") = "9999"
  filaClte("CardName") = "TODOS"
  filaClte("Slpcode") = "999"

  DsClte.Tables(0).Rows.Add(filaClte)

  DvClte.Table = DsClte.Tables(0)
  Me.CmbCliente.DataSource = DvClte
  Me.CmbCliente.DisplayMember = "CardName"
  Me.CmbCliente.ValueMember = "CardCode"
  Me.CmbCliente.SelectedValue = ""

  cmdordena.Connection.Close()

  '**********************************************************************************

  'UsrTPM = "COBRANZ1" Or 
  'MODIFICADO POR IVAN GONZALEZ SE QUITO COBRANZ2
  If UsrTPM = "COBRANZ4" Or UsrTPM = "COBRANZ5" Then

   ConsutaLista = "SELECT IdCobranza,Nombre FROM DEPCOB WHERE IdCobranza ='" + UsrTPM + "' " + " ORDER BY Nombre"

  ElseIf UsrTPM = "NGOMEZ" Then
   ConsutaLista = "SELECT IdCobranza,Nombre FROM DEPCOB WHERE IdCobranza ='cobranz6' ORDER BY Nombre"
  Else
   ConsutaLista = "SELECT IdCobranza,Nombre FROM DEPCOB ORDER BY Nombre"
  End If


  ''ConsutaLista = "SELECT IdCobranza,Nombre FROM DEPCOB ORDER BY Nombre"
  Dim daACob As New SqlClient.SqlDataAdapter(ConsutaLista, StrTpm)

  Dim dsACob As New DataSet
  daACob.Fill(dsACob)

  Dim fila2 As Data.DataRow

  'Or VEsAgente = 1

  If UsrTPM = "MANAGER" Or UsrTPM = "COBRANZ1" Or UsrTPM = "PRUEBAS" Or UsrTPM = "COBRANZ3" Or UsrTPM = "COBRANZ2" Or VEsAgente = 1 Then
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
  'Me.CmbAgteCob.SelectedValue = ""

  If UsrTPM = "COBRANZ2" Or UsrTPM = "COBRANZ4" Or UsrTPM = "COBRANZ5" Then

   Me.CmbAgteCob.SelectedValue = UsrTPM

  ElseIf UsrTPM = "NGOMEZ" Then
   Me.CmbAgteCob.SelectedValue = "COBRANZ6"

  Else
   Me.CmbAgteCob.SelectedValue = "TODOS"
  End If


  If VEsAgente = 1 Then
   Me.CmbAgteCob.Enabled = False
   Me.CmbAgteCob.SelectedValue = "TODOS"
   If (UsrTPM = "RROBLES") Then
    DvLP.RowFilter = "slpcode = 20 or slpcode = 17 or slpcode = 50 or slpcode = 54"
    Me.CmbAgteVta.SelectedValue = vCodAgte
   Else
    Me.CmbAgteVta.SelectedValue = vCodAgte
    Me.CmbAgteVta.Enabled = False
   End If


  End If

  BuscaRutas()
  BuscaRutasCliente()


 End Sub
 Sub ValidaNave()
  If (UsrTPM <> "RROBLES") Then
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
  Else


  End If


 End Sub

 Sub BuscaCltes()
  'Dim v As String
  'v = Me.CmbAgteVta.SelectedValue.ToString

  'v = "slpcode = " & "'" & Trim(Me.CmbAgteVta.SelectedValue.ToString) & "'"
  Try

   If CmbAgteVta.SelectedValue Is Nothing Then
    DvClte.RowFilter = "slpcode = 0"
   Else
    DvClte.RowFilter = "slpcode = " & "'" & Trim(Me.CmbAgteVta.SelectedValue.ToString) & "'"
   End If

   CmbCliente.SelectedValue = 0 '
  Catch ex As Exception

  End Try

 End Sub

 Sub BuscaRutas()

  Try

   If CmbAgteVta.SelectedValue Is Nothing Then
    DvRuta.RowFilter = String.Empty
   Else
    DvRuta.RowFilter = "slpcode = " & "'" & Trim(Me.CmbAgteVta.SelectedValue.ToString) & "'"
   End If
   CBRuta.SelectedValue = 0

  Catch ex As Exception

  End Try
 End Sub

 Sub BuscaRutasCliente()
  'MsgBox("entre")
  If (CmbAgteVta.Text = "") Then
   'MsgBox("dbo quitar filtro")
   DvClte.RowFilter = String.Empty
   Me.CmbCliente.SelectedValue = ""
   strTemp = ""
   Return

   'Me.CmbAgteVta.SelectedValue = "0"
  End If
  Try

   If CBRuta.Text Is Nothing Or CBRuta.Text = "" Then
    DvClte.RowFilter = "slpcode = " & "'" & Trim(Me.CmbAgteVta.SelectedValue.ToString) & "'"
   Else
    DvClte.RowFilter = "ruta = " & "'" & Trim(Me.CBRuta.Text) & "'"
   End If

   CmbCliente.SelectedValue = 0

  Catch ex As Exception

  End Try
 End Sub

 Sub cargar_registros2()

  Dim DTMObra As New DataTable
  Dim informe As New CGCliente
  Dim informe2 As New CGAgente

  Dim CmdMObra As SqlClient.SqlCommand
  Dim AdapMObra As SqlClient.SqlDataAdapter
  Dim DsCob As DataSet
  Dim DataSetX As DataSet
  Dim DvConsulta As DataView


  Dim Consulta As String = ""
  Dim strcadena As String = ""

  'MsgBox("1" & CmbCliente.SelectedIndex.ToString)

  If Me.CmbCliente.SelectedIndex.ToString <> "-1" Then
   'MsgBox("entre al if")
   Try
    'Consulta &= "TRUNCATE TABLE TPM.dbo.CobrGral "

    ''Se relacionan las notas de credito con las facturas por número de vendedor, número de cliente y referencia
    ''en donde tenga saldo pendiente la factura y sea una nota de credito
    ''de un agente de terminado (parametro)
    'Consulta &= " SELECT T9.DocNum AS DocSAP,T9.EDocNum AS FactFiscal,T9.CardCode,t9.CardName,T9.DocDate as FchDoc,T9.DocDueDate as FchVenci,"
    'Consulta &= " CAST(0 AS numeric(19,6)) as DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,CAST(0 AS numeric(19,6)) AS SaldoPendiene,"
    'Consulta &= " T9.NumAtCard AS FactSaldo,"
    'Consulta &= " t9.Comments,CAST(0 AS int) as IdPago,T9.EDocPrefix as TipoMov,"
    'Consulta &= " CASE "
    'Consulta &= " WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'I' THEN 'CANCELACION' "
    'Consulta &= " WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'S'  THEN 'DESCUENTO' "
    'Consulta &= " WHEN T9.EDocPrefix = 'ND' THEN 'DEVOLUCION' "
    'Consulta &= " END AS Movimiento,"
    'Consulta &= " CAST(0 AS numeric(19,6)) AS Cargo,CASE WHEN T9.DocTotal - T9.PaidToDate > 0 THEN T9.DocTotal - T9.PaidToDate ELSE T9.DocTotal END AS Abono,CAST(3 AS int) as Orden,"
    'Consulta &= " T9.DocNum as NumNota,T10.DocTotal - T10.PaidToDate as MPendiene,T10.DocDate as FchFFact "
    'Consulta &= " into #TmpNotaCSAP "
    'Consulta &= " FROM ORIN T9, OINV T10 WHERE "
    'Consulta &= " T10.CardCode = T9.CardCode AND "
    'Consulta &= " RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.DocNum AS nvarchar(100)))) "
    'Consulta &= " AND T10.DocTotal - T10.PaidToDate <> 0 AND T10.Series <> 48"

    ''T10.Series <> 48 se usa para eliminar las notas de credito canceladas con nota de cargo
    ''por ejemplo nota de cargo 1000000053 y nota de credito 2154

    ''Se buscan todas las facturas con saldo pendiente de un agente determinado (parametro)
    'Consulta &= " SELECT T8.DocNum AS DocSAP,T8.EDocNum AS FactFiscal,T8.CardCode,T8.CardName,T8.DocDate as FchDoc,T8.DocDueDate as FchVenci,"
    'Consulta &= " T8.DocTotal,T8.PaidToDate AS Aplicado,T8.DocTotal - T8.PaidToDate as SaldoPendiene,T8.NumAtCard AS FactSaldo,CAST('' AS nvarchar(254)) AS Comments,"
    'Consulta &= " CAST(0 AS int) as IdPago,CAST('' AS nvarchar(10)) AS TipoMov, 'FACTURA' AS Movimiento,"
    'Consulta &= " T8.DocTotal AS Cargo,CAST(0 AS numeric(19,6)) AS Abono,CAST(1 AS int) as Orden,CAST(0 AS int) as NumNota,T8.DocTotal - T8.PaidToDate as MPendiene,T8.DocDate as FchFFact "
    'Consulta &= " into #TmpFacPag "
    'Consulta &= " FROM OINV T8 "
    'Consulta &= " WHERE T8.DocTotal - T8.PaidToDate > 0"

    'Consulta &= " UNION ALL "

    ''Busca los pagos de las facturas que tienen saldo pendiente y que no estan canceladas de un agente determinado (parametro)
    'Consulta &= " SELECT T8.DocNum AS DocSAP,T8.EDocNum AS FactFiscal,T6.CardCode,T6.CardName,T6.DocDate as FchDoc,"
    'Consulta &= " T6.DocDueDate as FchVenci,CAST(0 AS numeric(19,6)) AS DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,"
    'Consulta &= " CAST(0 AS numeric(19,6)) as SaldoPendiene,"
    'Consulta &= " CAST('' AS nvarchar(100)) AS FactSaldo,T6.Comments,"
    'Consulta &= " T7.DocNum as IdPago,CAST('' AS nvarchar(10)) AS TipoMov,  'PAGO' AS Movimiento,"
    'Consulta &= " CAST(0 AS numeric(19,6)) AS Cargo,T7.SumApplied  AS Abono,CAST(2 AS int) as Orden,CAST(0 AS int) as NumNota,"
    'Consulta &= " T8.DocTotal - T8.PaidToDate as MPendiene,T8.DocDate as FchFFact "
    'Consulta &= " FROM ORCT T6 "
    'Consulta &= " INNER JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "
    'Consulta &= " INNER JOIN OINV T8 ON T8.DocEntry = T7.DocEntry AND T7.InvType = 13 "
    'Consulta &= " WHERE "
    'Consulta &= " T6.Canceled = 'N' "
    'Consulta &= " AND T8.DocTotal - T8.PaidToDate > 0 "

    'Consulta &= " UNION ALL "

    ''Se une con la consulta anterior de notas de credito
    'Consulta &= " SELECT CAST(FactSaldo AS int) AS DocSAP,FactFiscal,CardCode,CardName,FchDoc,FchVenci,"
    'Consulta &= " DocTotal,Aplicado,SaldoPendiene,CAST(DocSAP AS nvarchar(100))AS FactSaldo,"
    'Consulta &= " Comments, IdPago, TipoMov,"
    'Consulta &= " Movimiento, Cargo, Abono, Orden, NumNota, MPendiene, FchFFact"
    'Consulta &= " FROM #TmpNotaCSAP "

    'Consulta &= " UNION ALL "

    ''Se relacionan las notas de credito con las facturas por número de vendedor, número de cliente y referencia
    ''en donde tenga saldo pendiente la factura y sea una nota de credito
    ''de un agente de terminado (parametro)
    ''Con la diferencia a la consulta TmpNotaCSAP que se buscan las NC que si se relacionan directamente por el campo 'EDocNum'
    'Consulta &= " SELECT t10.docnum AS DocNum,T9.EDocNum AS FactFiscal,T9.CardCode,t9.CardName,T9.DocDate as FchDoc,"
    'Consulta &= " T9.DocDueDate as FchVenci,CAST(0 AS numeric(19,6)) as DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,"
    'Consulta &= " CAST(0 AS numeric(19,6)) AS SaldoPendiene,CAST(T9.DocNum  AS nvarchar(100))AS FactSaldo,"
    'Consulta &= " t9.Comments,CAST(0 AS int) as IdPago,T9.EDocPrefix as TipoMov,"
    'Consulta &= " CASE "
    'Consulta &= " WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'I' THEN 'CANCELACION' "
    'Consulta &= " WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'S'  THEN 'DESCUENTO' "
    'Consulta &= " WHEN T9.EDocPrefix = 'ND' THEN 'DEVOLUCION' "
    'Consulta &= " END AS Movimiento,"
    'Consulta &= " CAST(0 AS numeric(19,6)) AS Cargo,CASE WHEN T9.DocTotal - T9.PaidToDate > 0 THEN T9.DocTotal - T9.PaidToDate "
    'Consulta &= " ELSE T9.DocTotal  - T9.PaidToDate END AS Abono,CAST(3 AS int) as Orden,"
    'Consulta &= " T9.DocNum as NumNota,T10.DocTotal - T10.PaidToDate as MPendiene,T10.DocDate as FchFFact "
    'Consulta &= " FROM ORIN T9, OINV T10 WHERE T10.CardCode = T9.CardCode AND"
    'Consulta &= " RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.EDocNum AS nvarchar(100)))) "
    'Consulta &= " AND T10.DocTotal - T10.PaidToDate > 0  "
    'Consulta &= " AND T10.Series <> 48 "
    'Consulta &= " AND T9.DocNum NOT IN (SELECT NumNota FROM #TmpNotaCSAP)"

    'Consulta &= " UNION ALL"

    ''SE CALCULAN SALDOS A FAVOR
    'Consulta &= " SELECT '' AS DocSAP,'' AS FactFiscal,T6.CardCode,T6.CardName,T6.DocDate as FchDoc,T6.DocDueDate as FchVenci,"
    'Consulta &= " 0 AS DocTotal,T6.OpenBal AS Aplicado,T6.OpenBal as SaldoPendiene,CAST('' AS nvarchar(100)) AS FactSaldo,T6.Comments,"
    'Consulta &= " T7.DocNum as IdPago,CAST('' AS nvarchar(10)) AS TipoMov, 'PAGO' AS Movimiento,"
    'Consulta &= " CAST(0 AS numeric(19,6)) AS Cargo,T6.OpenBal  AS Abono,CAST(2 AS int) as Orden,CAST(0 AS int) as NumNota,"
    'Consulta &= " T6.OpenBal as MPendiene,'19991212' as FchFFact "
    'Consulta &= " FROM ORCT T6 "
    'Consulta &= " INNER JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "
    'Consulta &= " INNER JOIN OINV T8 ON T8.DocEntry = T7.DocEntry AND T7.InvType = 13 "
    'Consulta &= " WHERE "
    'Consulta &= " T6.Canceled = 'N' "
    'Consulta &= " AND T6.OpenBal > 0 "
    'Consulta &= " group by T6.CardCode,T6.CardName,T6.DocDate,T6.DocDueDate,T6.OpenBal,T6.Comments,"
    'Consulta &= " T7.DocNum "

    ''--SE CALCULA EL SALDO DEL CLIENTE,MONTO DE LAS FACTURAS CON SALDO PENDIENTE Y NOTAS DE CREDITO ABIERTAS DE LOS CLIENTES DEL AGENTE
    'Consulta &= " SELECT T8.CardCode,T8.DocTotal as TotFact,T8.PaidToDate AS TotAplicado ,"
    'Consulta &= " T8.DocTotal - T8.PaidToDate as TotSaldoP,T8.SlpCode "
    'Consulta &= " INTO #SaldoCte_SAP "
    'Consulta &= " FROM OINV T8 "
    'Consulta &= " where T8.DocTotal - T8.PaidToDate > 0 "
    'Consulta &= " UNION ALL "
    'Consulta &= " SELECT T9.CardCode,CAST(0 AS numeric(19,6)) AS DocTotal,(DocTotal - PaidToDate) AS TotAplicado,"
    'Consulta &= " CAST(0 AS numeric(19,6)) AS TotSaldoP,T9.SlpCode "
    'Consulta &= " FROM ORIN T9 WHERE DocStatus = 'O' "

    ''--SE SUMAN TODAS LAS FACTURAS CON NOTAS DE CREDITO POR CLIENTE,AGENTE
    'Consulta &= " SELECT CardCode,sum(TotFact) as TotFact,Sum(TotAplicado) AS TotAplicado,Sum(TotFact - TotAplicado) as TotSaldoP "
    'Consulta &= " INTO #SaldoR_SAP "
    'Consulta &= " FROM #SaldoCte_SAP "
    'Consulta &= " GROUP BY CardCode"

    ''-- SE UNEN TODAS LAS CONSULTAS DE DETALLE Y MONTOS TOTALES

    ''Consulta &= "TRUNCATE TABLE TPM.DBO.CobrGral "

    ''Consulta &= " TRUNCATE TABLE TPM.dbo.CobrGral "

    'Consulta &= " SELECT CardCode,CardName,SUM(Aplicado) AS 'Aplicado' INTO #SaldosFavor FROM("
    'Consulta &= " SELECT T6.DocNum,T6.CardCode,T6.CardName,T6.OpenBal AS Aplicado,T6.OpenBal as SaldoPendiene,"
    'Consulta &= " T6.OpenBal AS Abono,"
    'Consulta &= " T6.OpenBal as MPendiene"
    'Consulta &= " FROM ORCT T6 "
    'Consulta &= " INNER JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "
    'Consulta &= " INNER JOIN OINV T8 ON T8.DocEntry = T7.DocEntry AND T7.InvType = 13 "
    'Consulta &= " WHERE T6.Canceled = 'N' "
    'Consulta &= " AND T6.OpenBal > 0 "
    'Consulta &= " group by T6.DocNum,T6.CardCode,T6.CardName,T6.OpenBal"
    'Consulta &= " )TMP GROUP BY CardCode,CardName "


    'Consulta &= " INSERT INTO TPM.dbo.CobrGral(DocSAP,FactFiscal,FchDoc,FchVenci,DocTotal,Aplicado,SaldoPendiene,Movimiento,Comments,Cargo,"
    'Consulta &= " Abono,IdPago,NumNota,MPendiene,CardCode,CardName,Phone1,ListaP,PymntGroup,TotFact,TotAplicado,TotSaldoP,FDocSap,FFchDoc,"
    'Consulta &= " FFchVen,FFiscal,SlpCode,SlpName,FchMov,NumClte,FchFFact,COLUMNS_TotFact,COLUMN_TotAplicado,COLUMN_TotSaldoP )"

    'Consulta &= " SELECT T1.DocSAP,T1.FactFiscal,T1.FchDoc,T1.FchVenci,"
    'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.DocTotal ELSE NULL END AS DocTotal,"
    'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.Aplicado ELSE NULL END AS Aplicado,"
    'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.SaldoPendiene ELSE NULL END AS SaldoPendiene,"
    'Consulta &= " T1.Movimiento,T1.Comments,T1.Cargo,T1.Abono,T1.IdPago,T1.NumNota,T1.MPendiene,T1.CardCode,"
    'Consulta &= " T6.CardName,T2.Phone1,T2.ListNum AS ListaP,T3.PymntGroup,"
    'Consulta &= " TotFact,TotAplicado,"
    'Consulta &= " TotSaldoP,"
    'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.DocSAP ELSE NULL END AS FDocSap,"
    'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.FchDoc ELSE NULL END AS FFchDoc,"
    'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.FchVenci ELSE NULL END AS FFchVen,"
    'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.FactFiscal ELSE NULL END AS FFiscal,"
    'Consulta &= " T2.SlpCode,T5.SlpName,"
    'Consulta &= " CASE WHEN Movimiento <> 'FACTURA' THEN T1.FchDoc ELSE NULL END AS FchMov,CAST(REPLACE(T1.CardCode, 'C-', '') AS int) as NumClte,T1.FchFFact,"
    'Consulta &= " T4.TotFact,"
    'Consulta &= " CASE WHEN T7.Aplicado IS NULL THEN T4.TotAplicado ELSE T4.TotAplicado + T7.Aplicado END AS TotAplicado,"
    'Consulta &= " CASE WHEN T7.Aplicado IS NULL THEN T4.TotSaldoP ELSE T4.TotSaldoP - T7.Aplicado END AS TotSaldoP"
    'Consulta &= " FROM #TmpFacPag T1"
    'Consulta &= " INNER JOIN OCRD T2 ON T1.CardCode = T2.CardCode"
    'Consulta &= " INNER JOIN OCTG T3 ON T2.GroupNum = T3.GroupNum"
    'Consulta &= " INNER JOIN #SaldoR_SAP T4 ON T1.CardCode = T4.CardCode"
    'Consulta &= " INNER JOIN OSLP T5 ON T5.SlpCode = T2.slpcode"
    'Consulta &= " INNER JOIN OCRD T6 ON T1.CardCode = T6.CardCode"
    'Consulta &= " LEFT JOIN #SaldosFavor T7 ON T1.CardCode=T7.CardCode"
    'Consulta &= " WHERE T1.Movimiento <> 'CANCELACION' AND "
    'Consulta &= " T4.TotSaldoP <> 0  "
    'Consulta &= " ORDER BY NumClte,FchFFact,DocSAP,Orden ASC "

    'Consulta &= "SELECT * FROM TPM.dbo.CobrGral WHERE CardCode = @ParaCliente "

    ''End If
    'MsgBox("entre al if")
    Consulta &= "select t0.DocNum, t0.DocDate, t0.DocDueDate, t0.CardCode, t0.DocTotal, t0.PaidToDate, "
    Consulta &= "(t0.DocTotal - t0.PaidToDate) as 'Resta', t0.TransId "
    Consulta &= "into #FacturasPendientes "
    Consulta &= "from OINV t0 "
    Consulta &= "where t0.CardCode = @ParaCliente and t0.DocTotal - t0.PaidToDate <> 0 "

    Consulta &= "select distinct t1.DocNum as 'Factura', t1.DocDate as 'FechaFactura', t1.DocDueDate as 'FechaVenc', "
    Consulta &= "t1.DocTotal as 'ImporteFactura', t5.DocTotal as 'Abono', t5.DocNum, t5.DocDate "
    Consulta &= "into #NotasCredito_TodasFacturas "
    Consulta &= "from #FacturasPendientes t0 inner join OINV t1 on t0.DocNum = t1.DocNum "
    Consulta &= "inner join ITR1 t2 on t1.TransId = t2.TransId inner join ITR1 t3 on t2.ReconNum = t3.ReconNum "
    Consulta &= "inner join ITR1 t4 on t3.ReconNum = t4.ReconNum inner join ORIN t5 on t4.TransId = t5.TransId "
    Consulta &= "where t4.ShortName = @ParaCliente "

    'INICIA MODIFICACION
    Consulta &= " If OBJECT_ID(N'tempdb.dbo.#NotasCredito_TodasFacturas2_PRE', N'U') is not null"
    Consulta &= " DROP TABLE #NotasCredito_TodasFacturas2_PRE; "
    Consulta &= " SELECT DISTINCT t4.DocNum as 'Factura', "
    Consulta &= " t3.ReconSum as 'Abono', t0.Abono 'Abono 2', t1.DocNum, t1.DocDate, t1.DocTotal, "
    Consulta &= " IIF (t1.DocType = 'I' AND t6.AcctCode = '2180-001-000', 'A', t1.DocType) AS DocType, "
    Consulta &= " t4.DocTotal - t4.PaidToDate as 'RestanteFactura', t2.ReconNum "
    Consulta &= " INTO #NotasCredito_TodasFacturas2_PRE "
    Consulta &= " FROM #NotasCredito_TodasFacturas t0 inner join ORIN t1 on t0.DocNum = t1.DocNum "
    Consulta &= " inner join RIN1 t6 on t6.DocEntry = t1.DocEntry "
    Consulta &= " inner join ITR1 t2 on t1.TransId = t2.TransId inner join ITR1 t3 on t2.ReconNum = t3.ReconNum "
    Consulta &= " inner join OINV t4 on t3.TransId = t4.TransId inner join OITR t5 on t3.ReconNum = t5.ReconNum "
    Consulta &= " where t3.ShortName = @ParaCliente and t4.DocNum in (select Factura from #NotasCredito_TodasFacturas) "
    Consulta &= " and t5.Canceled <> 'Y' and t3.ReconSum > 1.0 "
    Consulta &= " order by t4.DocNum, t1.DocNum "

    ''ACTUALIZO ABONOS EN TABLA
    Consulta &= " UPDATE #NotasCredito_TodasFacturas2_PRE"
    Consulta &= " SET Abono = [Abono 2]"
    Consulta &= " WHERE ReconNum IN (SELECT ReconNum FROM #NotasCredito_TodasFacturas2_PRE"
    Consulta &= " GROUP BY ReconNum, Factura"
    Consulta &= " HAVING COUNT(Factura + ReconNum) > 1)"

    'RELIZO LA CREACION DE lA TABLA TEMPORAL #NotasCredito_TodasFacturas2
    Consulta &= " SELECT Factura, Abono, DocNum, DocDate, DocTotal, DocType, RestanteFactura "
    Consulta &= " INTO #NotasCredito_TodasFacturas2 "
    Consulta &= " FROM #NotasCredito_TodasFacturas2_PRE "


    'Consulta &= "select distinct t4.DocNum as 'Factura', "
    'Consulta &= "t3.ReconSum as 'Abono', t1.DocNum, t1.DocDate, t1.DocTotal, "
    'Consulta &= "IIF (t1.DocType = 'I' AND t6.AcctCode = '2180-001-000', 'A', t1.DocType) AS DocType, "
    'Consulta &= "t4.DocTotal - t4.PaidToDate as 'RestanteFactura' "
    'Consulta &= "into #NotasCredito_TodasFacturas2 "
    'Consulta &= "from #NotasCredito_TodasFacturas t0 inner join ORIN t1 on t0.DocNum = t1.DocNum "
    'Consulta &= "inner join RIN1 t6 on t6.DocEntry = t1.DocEntry "
    'Consulta &= "inner join ITR1 t2 on t1.TransId = t2.TransId inner join ITR1 t3 on t2.ReconNum = t3.ReconNum "
    'Consulta &= "inner join OINV t4 on t3.TransId = t4.TransId inner join OITR t5 on t3.ReconNum = t5.ReconNum "
    'Consulta &= "where t3.ShortName = @ParaCliente and t4.DocNum in (select Factura from #NotasCredito_TodasFacturas) "
    'Consulta &= "and t5.Canceled <> 'Y' and t3.ReconSum > 1.0 "
    'Consulta &= "order by t4.DocNum, t1.DocNum "

    Consulta &= "select t1.DocNum as 'Factura', "
    Consulta &= "t3.DocTotal as 'Abono', t3.DocNum, t3.DocDate, t2.SumApplied "
    Consulta &= "into #Pagos_TodasFactura2 "
    Consulta &= "from #FacturasPendientes t0 inner join OINV t1 on t0.DocNum = t1.DocNum "
    'Consulta &= "inner join RCT2 t2 on t1.DocEntry = t2.DocEntry inner join ORCT t3 on t2.DocNum = t3.DocNum where t3.Canceled <> 'Y' "
    Consulta &= "inner join RCT2 t2 on t1.DocEntry = t2.DocEntry inner join ORCT t3 on t2.DocNum = t3.DocEntry where t3.Canceled <> 'Y' "
    Consulta &= "AND t3.DocTotal > 1.0 "

    Consulta &= "select distinct t1.DocNum as 'Factura', t5.DocTotal as 'Abono', t5.DocNum, t5.DocDate, t3.ReconSum as 'SumApplied' "
    Consulta &= "into #Pagos_TodasFactura "
    Consulta &= "from #FacturasPendientes t0 inner join OINV t1 on t0.DocNum = t1.DocNum "
    Consulta &= "left join ITR1 t2 on t1.TransId = t2.TransId left join ITR1 t3 on t2.ReconNum = t3.ReconNum "
    Consulta &= "left join OITR t4 on t3.ReconNum = t4.ReconNum "
    Consulta &= "inner join ORCT t5 on t3.TransId = t5.TransId "
    Consulta &= "where t2.ShortName = @ParaCliente and t2.ReconSum > 1.0 and t4.Canceled <> 'Y' "
    Consulta &= "and t5.DocNum not in (select t8.DocNum from #Pagos_TodasFactura2 t8)  "
    Consulta &= "union all "
    Consulta &= "select * from #Pagos_TodasFactura2 "



    Consulta &= "select Factura, Abono, DocDate, "
    Consulta &= "CASE when DocType = 'I' then 'NC Dev' when DocType = 'A' then 'Anticipo' when DocType = 'S' then 'NC Desc' end as 'TipoAbono' "
    Consulta &= "into #Abonos "
    Consulta &= "from #NotasCredito_TodasFacturas2 "
    Consulta &= "union all "
    Consulta &= "select Factura, SumApplied as 'Abono', "
    Consulta &= "DocDate, 'Pago' as 'TipoAbono' "
    Consulta &= "from #Pagos_TodasFactura "
    Consulta &= "order by Factura "
    Consulta &= "select *, ROW_NUMBER() over (PARTITION BY Factura order by Factura) as 'NumRow' "
    Consulta &= "into #Abonos2 "
    Consulta &= "from #Abonos "
    Consulta &= "select t0.DocNum, t0.DocDate, t0.DocDueDate, t0.CardCode, t0.DocTotal, "
    Consulta &= "t0.DocTotal - t0.OpenBal as 'PaidToDate', t0.OpenBal as 'Resta', t0.TransId, 'Pago' as 'TipoAbono', "
    Consulta &= "'A' as 'Clase' "
    Consulta &= "into #AbonosPendientes "
    Consulta &= "from ORCT t0 where t0.CardCode = @ParaCliente and t0.OpenBal <> 0 "
    Consulta &= "union all "
    Consulta &= "select t0.DocNum, t0.DocDate, t0.DocDueDate, t0.CardCode, t0.DocTotal, t0.PaidToDate, "
    Consulta &= "(t0.DocTotal - t0.PaidToDate) as 'Resta', t0.TransId,"
    Consulta &= "CASE when t0.DocType = 'I' then 'NC Dev' "
    Consulta &= "when DocType = 'S' then 'NC Desc' end as 'TipoAbono', "
    Consulta &= "'A' as 'Clase' "
    Consulta &= "from ORIN t0 "
    Consulta &= "where t0.CardCode = @ParaCliente and t0.DocTotal - t0.PaidToDate <> 0 "
    Consulta &= "select t0.DocNum, 1 as Prioridad, t0.DocDate, t0.DocDueDate, t0.CardCode, t0.DocTotal, t0.PaidToDate, t0.Resta, t0.TransId,  "
    Consulta &= "t1.Factura, t1.Abono, t1.DocDate as 'FechaAbono', TipoAbono, NumRow, 'F' as 'Clase' "
    Consulta &= "into #pre_1 "
    Consulta &= "from #FacturasPendientes t0 left join #Abonos2 t1 on t0.DocNum = t1.Factura "
    Consulta &= "union all "
    Consulta &= "select DocNum, 2 as Prioridad, DocDate, DocDueDate, CardCode, DocTotal, PaidToDate, Resta, TransId, NULL as 'Factura',"
    Consulta &= "NULL as 'Abono', NULL as 'FechaAbono', TipoAbono, NULL as 'NumRow', Clase "
    Consulta &= "from #AbonosPendientes "
    Consulta &= "order by Prioridad ASC, DocDate ASC "
    Consulta &= "select "
    Consulta &= "CASE when Clase = 'F' then cast(DocNum as varchar) "
    Consulta &= "when Clase = 'A' then '-' end as 'Factura', "
    Consulta &= "CASE when NumRow = 1 then convert(varchar(15),DocDate,3) "
    Consulta &= "when NumRow <> 1 then '' "
    Consulta &= "when Clase = 'A' then '' "
    Consulta &= "when NumRow is null and Clase <> 'A' then convert(varchar(15),DocDate,3) "
    Consulta &= "end as 'Fecha', "
    Consulta &= "CASE when NumRow = 1 then convert(varchar(15),DocDueDate,3) "
    Consulta &= "when NumRow <> 1 then '' "
    Consulta &= "when Clase = 'A' then '' "
    Consulta &= "when NumRow is null and Clase <> 'A' then convert(varchar(15),DocDueDate,3) "
    Consulta &= "end as 'Vencimiento', "
    Consulta &= "CASE when NumRow = 1 then cast(DATEDIFF(day, DocDueDate,GETDATE()) as varchar) "
    Consulta &= "when NumRow <> 1 then '' "
    Consulta &= "when NumRow is null and Clase = 'F' then cast(DATEDIFF(day, DocDueDate,GETDATE()) as varchar) "
    Consulta &= "when NumRow is null and Clase = 'A' then '' end as 'DiasAtraso',"
    Consulta &= "CASE when NumRow is not null and NumRow = 1 then '$' + CONVERT(varchar(50), (convert(money,DocTotal)),1) "
    Consulta &= "when NumRow is not null and NumRow <> 1 then '' "
    Consulta &= "when NumRow is null and Clase <> 'A' then '$' + CONVERT(varchar(50), (convert(money,DocTotal)),1) "
    Consulta &= "when NumRow is null and Clase = 'A' then '' "
    Consulta &= "end as 'Importe',"
    Consulta &= "CASE when NumRow is NULL and Clase <> 'A'  then '' "
    Consulta &= "when NumRow is null and Clase = 'A' then '$' + CONVERT(varchar(50), (convert(money,Resta)),1) "
    Consulta &= "when NumRow is not null  then '$' + CONVERT(varchar(10), (convert(money,Abono)),1) end as 'Abono', "
    Consulta &= "CASE when NumRow is null and Clase <> 'A' then '' "
    Consulta &= "when NumRow is null and Clase = 'A' then convert(varchar(15),DocDate,3) "
    Consulta &= "when NumRow is not null then convert(varchar(15),FechaAbono,3) end as 'FechaAbono', "
    Consulta &= "CASE when NumRow is not null and NumRow = (select MAX(NumRow) from #Abonos2 t1 where t1.Factura = t0.Factura) "
    Consulta &= "then '$' + CONVERT(varchar(50), (convert(money,Resta)),1) "
    Consulta &= "when NumRow is not null and NumRow <> (select MAX(NumRow) from #Abonos2 t1 where t1.Factura = t0.Factura) "
    Consulta &= "then '' "
    Consulta &= "when NumRow is null and Clase <> 'A' then '$' + CONVERT(varchar(50), (convert(money,Resta)),1) "
    Consulta &= "when NumRow is null and Clase = 'A' then '$' + CONVERT(varchar(50), (convert(money,(Resta * -1))),1) "
    Consulta &= "end as 'SaldoPendiente',"
    Consulta &= "CASE when NumRow is not null then TipoAbono "
    Consulta &= "when NumRow is null and Clase <> 'A' then '' "
    Consulta &= "when NumRow is null and Clase = 'A' then TipoAbono "
    Consulta &= "end as 'TipoAbono' "
    Consulta &= "from #pre_1 t0 "
    Consulta &= "select (select ISNULL(SUM(DocTotal),0) from #FacturasPendientes) as 'Importe', "
    Consulta &= "(select ISNULL(SUM(Abono),0) from #Abonos2) + (select ISNULL(SUM(Resta),0) from #AbonosPendientes)  "
    Consulta &= "as 'Abono' "
    Consulta &= "into #tmp1 "
    Consulta &= "select '$' + convert(varchar(50), (convert(money, Importe)),1) as 'Importe', "
    Consulta &= "'$' + convert(varchar(50), (convert(money, Abono)),1) as 'Abono', "
    Consulta &= "'$' + convert(varchar(50), (convert(money,(Importe - Abono))),1) as 'Saldo' from #tmp1 "
    Consulta &= "select distinct convert(varchar(20), t0.CardCode) as 'Cliente', "
    Consulta &= "convert(varchar(50),t0.CardName) as 'Nombre', convert(varchar(30),t0.Phone1) as 'Telefono', "
    Consulta &= "convert(varchar(30),t1.PymntGroup) as 'DiasCredito',"
    Consulta &= "'$' + convert(varchar(50), (convert(money, t0.Balance)),1) as 'Saldo', "
    Consulta &= "convert(varchar(50),t2.SlpName) as 'Agente', convert(varchar(30),t0.U_BXP_Ruta) as 'Ruta',"
    Consulta &= "convert(varchar(50),t3.City) as 'Ciudad', convert(varchar(30),t3.State) as 'Estado' "
    Consulta &= "from OCRD t0 inner join OCTG t1 on t0.GroupNum = t1.GroupNum  "
    Consulta &= "inner join CRD1 t3 on t0.CardCode = t3.CardCode "
    ' EL CLIENTE NO APARECE O NO AROJA EL REPORTE SI EL ADDRESS NO TRAE FISCAL
    Consulta &= "inner join OSLP t2 on t0.SlpCode = t2.SlpCode  where t0.CardCode = @ParaCliente and t3.Address = 'FISCAL' AND (U_BXP_Estatus IS NULL  or U_BXP_Estatus='04' or U_BXP_Estatus='03' ) "
    Consulta &= "drop table #FacturasPendientes "
    Consulta &= "drop table #NotasCredito_TodasFacturas "
    'Consulta &= "drop table #NotasCredito_TodasFacturas2_PRE "
    Consulta &= "drop table #NotasCredito_TodasFacturas2 "
    Consulta &= "drop table #Pagos_TodasFactura2 "
    Consulta &= "drop table #Pagos_TodasFactura "
    Consulta &= "drop table #Abonos "
    Consulta &= "drop table #Abonos2 "
    Consulta &= "drop table #pre_1 "
    Consulta &= "drop table #AbonosPendientes "
    Consulta &= "drop table #tmp1 "

    'MsgBox(Consulta)
    CmdMObra = New SqlClient.SqlCommand(Consulta)

    'If Me.CmbAgteVta.SelectedValue.ToString <> " " And Me.CmbAgteVta.SelectedValue.ToString <> "" And Me.CmbAgteVta.SelectedValue Is Nothing = False Then
    '    CmdMObra.Parameters.Add("@RAgente", SqlDbType.SmallInt)
    '    CmdMObra.Parameters("@RAgente").Value = CmbAgteVta.SelectedValue
    'End If


    If Me.CmbCliente.SelectedValue <> " " And Me.CmbCliente.SelectedValue Is Nothing = False Then
     CmdMObra.Parameters.Add("@ParaCliente", SqlDbType.VarChar)
     CmdMObra.Parameters("@ParaCliente").Value = CmbCliente.SelectedValue
    End If

    If Me.CBRuta.Text <> " " And Me.CmbCliente.Text Is Nothing = False Then
     CmdMObra.Parameters.Add("@ParaRuta", SqlDbType.VarChar)
     CmdMObra.Parameters("@ParaRuta").Value = CBRuta.Text
    End If

    CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
    CmdMObra.Connection.Open()

    AdapMObra = New SqlClient.SqlDataAdapter(CmdMObra)
    AdapMObra.SelectCommand.CommandTimeout = 2000

    DsCob = New DataSet
    DataSetX = New DataSet

    AdapMObra.Fill(DsCob)

    DvConsulta = New DataView
    DvConsulta.Table = DsCob.Tables(0) 'origen de datos para la tabla de DvConsulta '


    DTMObra = DvConsulta.ToTable
    DTMObra.TableName = "ResumenSaldo"
    DataSetX.Tables.Add(DTMObra)

    DvConsulta = New DataView
    DTMObra = New DataTable
    DvConsulta.Table = DsCob.Tables(1)
    DTMObra = DvConsulta.ToTable
    DTMObra.TableName = "SaldoFinal"
    DataSetX.Tables.Add(DTMObra)

    DvConsulta = New DataView
    DTMObra = New DataTable
    DvConsulta.Table = DsCob.Tables(2)
    DTMObra = DvConsulta.ToTable
    DTMObra.TableName = "DatosCliente"
    DataSetX.Tables.Add(DTMObra)

    informe.SetDataSource(DataSetX)
    RepComsultaP.MdiParent = Inicio
    RepComsultaP.CrVConsulta.ReportSource = informe

   Catch ex As Exception
    MsgBox("Ocurrio un error " & ex.Message)
   End Try

  Else
   ''************************************COMENTADO

   'Consulta &= " TRUNCATE TABLE TPM.DBO.CobrGral "

   'Consulta &= " DECLARE @FACT AS TABLE(DocNum  INT, NumAtCard VARCHAR(100), DocDate DATE,"
   'Consulta &= " BaseEntry INT, BaseType INT, Comments VARCHAR(MAX),U_Factura VARCHAR(100))"

   'Consulta &= " INSERT INTO @FACT"
   'Consulta &= " SELECT DISTINCT T1.DocNum, T1.NumAtCard, T1.DocDate, T0.BaseEntry, T0.BaseType,T1.Comments,CONVERT(VARCHAR(100),U_FACTURA)"
   'Consulta &= " FROM SBO_TPD.dbo.RIN1 T0"
   'Consulta &= " INNER JOIN SBO_TPD.dbo.ORIN T1 ON T0.DocEntry = T1.DocEntry AND T1.SlpCode=@RAgente"
   'Consulta &= " WHERE DocStatus='O' "
   'Consulta &= " UNION ALL"
   'Consulta &= " SELECT DISTINCT T1.DocNum, T1.NumAtCard, T1.DocDate, T0.BaseEntry, T0.BaseType,T1.Comments,CONVERT(VARCHAR(100),t1.U_Factura)"
   'Consulta &= " FROM SBO_TPD.dbo.RIN1 T0"
   'Consulta &= " INNER JOIN SBO_TPD.dbo.ORIN T1 ON T0.DocEntry = T1.DocEntry AND T1.SlpCode=@RAgente"
   'Consulta &= " INNER JOIN SBO_TPD.dbo.OINV T2 ON T2.DocEntry = T0.BaseEntry AND T2.ObjType = T0.BaseType"
   'Consulta &= " WHERE"
   'Consulta &= " T2.DocTotal -T2.PaidToDate > 0"
   'Consulta &= " order by DocNum"


   'Consulta &= " DECLARE @FactCancel AS TABLE("
   'Consulta &= " FACTURA INT,"
   'Consulta &= " REFERENCIAFACT VARCHAR(120),"
   'Consulta &= " FECHAFACT DATE,"
   'Consulta &= " NCSAP INT,"
   'Consulta &= " COMMENTS VARCHAR(300),"
   'Consulta &= " REFERENCIANC VARCHAR(150),"
   'Consulta &= " FECHANC DATE,"
   'Consulta &= " U_FACTURA VARCHAR(100)"
   'Consulta &= " )"

   'Consulta &= " INSERT INTO @FactCancel(FACTURA,REFERENCIAFACT,FECHAFACT,NCSAP,COMMENTS,REFERENCIANC,FECHANC,U_FACTURA)"
   'Consulta &= " Select DISTINCT"
   'Consulta &= " '' [Factura] ,'' [Referencia] ,'' [Fecha],"
   'Consulta &= " T1.DocNum [NCSAP], T1.Comments,T1.NumAtCard [Referencia2], T1.DocDate [Fecha],T1.U_Factura"
   'Consulta &= " FROM @FACT T1"
   'Consulta &= " ORDER BY T1.DocNum "


   ''Se relacionan las notas de credito con las facturas por número de vendedor, número de cliente y referencia
   ''en donde tenga saldo pendiente la factura y sea una nota de credito
   ''de un agente de terminado (parametro)
   'Consulta &= " SELECT T9.DocNum AS DocSAP,T9.EDocNum AS FactFiscal,T9.CardCode,t9.CardName,T9.DocDate as FchDoc,T9.DocDueDate as FchVenci,"
   'Consulta &= " CAST(0 AS numeric(19,6)) as DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,CAST(0 AS numeric(19,6)) AS SaldoPendiene,"
   'Consulta &= " T9.NumAtCard AS FactSaldo,"
   'Consulta &= " t9.Comments,CAST(0 AS int) as IdPago,T9.EDocPrefix as TipoMov,"
   'Consulta &= " CASE "
   'Consulta &= " WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'I' THEN 'CANCELACION' "
   'Consulta &= " WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'S'  THEN 'DESCUENTO' "
   'Consulta &= " WHEN T9.EDocPrefix = 'ND' THEN 'DEVOLUCION' "
   'Consulta &= " END AS Movimiento,"
   'Consulta &= " CAST(0 AS numeric(19,6)) AS Cargo,CASE WHEN T9.DocTotal - T9.PaidToDate > 0 THEN T9.DocTotal - T9.PaidToDate ELSE T9.DocTotal END AS Abono,CAST(3 AS int) as Orden,"
   'Consulta &= " T9.DocNum as NumNota,T10.DocTotal - T10.PaidToDate as MPendiene,T10.DocDate as FchFFact,T9.SlpCode AS SlpCode"
   'Consulta &= " into #TmpNotaCSAP "
   'Consulta &= " FROM SBO_TPD.dbo.ORIN T9, "
   'Consulta &= " SBO_TPD.dbo.OINV T10 WHERE T9.SlpCode=T10.SlpCode "
   'Consulta &= " AND T9.SlpCode=@RAgente AND "
   'Consulta &= " T10.CardCode = T9.CardCode AND "
   'Consulta &= " RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.DocNum AS nvarchar(100)))) "
   'Consulta &= " AND T10.DocTotal - T10.PaidToDate <> 0 "

   ''T10.Series <> 48 se usa para eliminar las notas de credito canceladas con nota de cargo
   ''por ejemplo nota de cargo 1000000053 y nota de credito 2154

   ''Se buscan todas las facturas con saldo pendiente de un agente determinado (parametro)
   'Consulta &= " SELECT T8.DocNum AS DocSAP,T8.EDocNum AS FactFiscal,T8.CardCode,T8.CardName,T8.DocDate as FchDoc,"
   'Consulta &= " T8.DocDueDate as FchVenci,T8.DocTotal,0 AS Aplicado,"
   'Consulta &= " T8.DocTotal - T8.PaidToDate as SaldoPendiene,T8.NumAtCard AS FactSaldo,CAST('' AS nvarchar(254)) AS Comments,"
   'Consulta &= " CAST(0 AS int) as IdPago,CAST('' AS nvarchar(10)) AS TipoMov, 'FACTURA' AS Movimiento,"
   'Consulta &= " T8.DocTotal AS Cargo,0 AS Abono,"
   'Consulta &= " CAST(1 AS int) as Orden,CAST(0 AS int) as NumNota,T8.DocTotal - T8.PaidToDate as MPendiene,"
   'Consulta &= " T8.DocDate as FchFFact,T8.SlpCode"
   'Consulta &= " into #TmpFacPag "
   'Consulta &= " FROM SBO_TPD.dbo.OINV T8 WHERE T8.SlpCode = @RAgente "
   'Consulta &= " AND T8.DocTotal - T8.PaidToDate > 0 "

   'Consulta &= " UNION ALL"

   ' ''--Busca los pagos de las facturas que tienen saldo pendiente y que no estan canceladas de un agente determinado (parametro)
   'Consulta &= " SELECT T8.DocNum AS DocSAP,T8.EDocNum AS FactFiscal,T6.CardCode,T6.CardName,T6.DocDate as FchDoc,"
   'Consulta &= " T6.DocDueDate as FchVenci,CAST(0 AS numeric(19,6)) AS DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,"
   'Consulta &= " CAST(0 AS numeric(19,6)) as SaldoPendiene,"
   'Consulta &= " CAST('' AS nvarchar(100)) AS FactSaldo,T6.Comments,"
   'Consulta &= " T7.DocNum as IdPago,CAST('' AS nvarchar(10)) AS TipoMov,  'PAGO' AS Movimiento,"
   'Consulta &= " CAST(0 AS numeric(19,6)) AS Cargo,T7.SumApplied  AS Abono,CAST(2 AS int) as Orden,CAST(0 AS int) as NumNota,"
   'Consulta &= " T8.DocTotal - T8.PaidToDate as MPendiene,T8.DocDate as FchFFact,"
   'Consulta &= " T8.SlpCode"
   'Consulta &= " FROM SBO_TPD.dbo.ORCT T6 "
   'Consulta &= " INNER JOIN SBO_TPD.dbo.RCT2 T7 ON T6.DocNum = T7.DocNum "
   'Consulta &= " INNER JOIN SBO_TPD.dbo.OINV T8 ON T8.DocEntry = T7.DocEntry AND T7.InvType = 13 "
   'Consulta &= " WHERE T8.SlpCode = @RAgente AND "
   'Consulta &= " T6.Canceled = 'N' "
   'Consulta &= " AND T8.DocTotal - T8.PaidToDate > 0 "

   'Consulta &= " UNION ALL"

   ' ''--Se une con la consulta anterior de notas de credito
   'Consulta &= " SELECT CAST(FactSaldo AS int) AS DocSAP,FactFiscal,CardCode,CardName,FchDoc,FchVenci,"
   'Consulta &= " DocTotal,Aplicado,SaldoPendiene,CAST(DocSAP AS nvarchar(100))AS FactSaldo,"
   'Consulta &= " Comments, IdPago, TipoMov,"
   'Consulta &= " Movimiento, Cargo, Abono, Orden, NumNota, MPendiene, FchFFact, SlpCode"
   'Consulta &= " FROM #TmpNotaCSAP "

   'Consulta &= " UNION ALL"

   ' ''--Se relacionan las notas de credito con las facturas por número de vendedor, número de cliente y referencia
   ' ''--en donde tenga saldo pendiente la factura y sea una nota de credito
   ' ''--de un agente de determinado (parametro)
   ' ''--Con la diferencia a la consulta TmpNotaCSAP que se buscan las NC que si se relacionan directamente por el campo 'EDocNum'
   'Consulta &= " SELECT t10.docnum AS DocNum,T9.EDocNum AS FactFiscal,T9.CardCode,t9.CardName,T9.DocDate as FchDoc,"
   'Consulta &= " T9.DocDueDate as FchVenci,CAST(0 AS numeric(19,6)) as DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,"
   'Consulta &= " CAST(0 AS numeric(19,6)) AS SaldoPendiene,CAST(T9.DocNum  AS nvarchar(100))AS FactSaldo,"
   'Consulta &= " t9.Comments,CAST(0 AS int) as IdPago,T9.EDocPrefix as TipoMov,"
   'Consulta &= " CASE "
   'Consulta &= " WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'I' THEN 'CANCELACION' "
   'Consulta &= " WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'S'  THEN 'DESCUENTO' "
   'Consulta &= " WHEN T9.EDocPrefix = 'ND' THEN 'DEVOLUCION' "
   'Consulta &= " END AS Movimiento,"
   'Consulta &= " CAST(0 AS numeric(19,6)) AS Cargo,CASE WHEN T9.DocTotal - T9.PaidToDate > 0 THEN T9.DocTotal - T9.PaidToDate "
   'Consulta &= " ELSE T9.DocTotal  - T9.PaidToDate END AS Abono,CAST(3 AS int) as Orden,"
   'Consulta &= " T9.DocNum as NumNota,T10.DocTotal - T10.PaidToDate as MPendiene,T10.DocDate as FchFFact,"
   'Consulta &= " T9.SlpCode "
   'Consulta &= " FROM SBO_TPD.dbo.ORIN T9, SBO_TPD.dbo.OINV T10 "
   ' ''--WHERE T10.CardCode = T9.CardCode AND
   'Consulta &= " WHERE T9.SlpCode = @RAgente AND "
   'Consulta &= " T10.CardCode = T9.CardCode AND "
   'Consulta &= " RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.EDocNum AS nvarchar(100)))) "
   ' ''--AND T10.DocTotal - T10.PaidToDate > 0  
   'Consulta &= " AND T10.Series <> 48 "
   'Consulta &= " AND T9.DocNum NOT IN (SELECT NumNota FROM #TmpNotaCSAP)"

   'Consulta &= " UNION ALL"
   ' ''---SE CALCULAN SALDOS A FAVOR
   'Consulta &= " SELECT '' AS DocSAP,'' AS FactFiscal,T6.CardCode,T6.CardName,T6.DocDate as FchDoc,"
   'Consulta &= " T6.DocDueDate as FchVenci,0 AS DocTotal,T6.OpenBal AS Aplicado,T6.OpenBal as SaldoPendiene,"
   'Consulta &= " CAST('' AS nvarchar(100)) AS FactSaldo,T6.Comments,"
   'Consulta &= " T7.DocNum as IdPago,CAST('' AS nvarchar(10)) AS TipoMov, 'PAGO' AS Movimiento,"
   'Consulta &= " CAST(0 AS numeric(19,6)) AS Cargo,T6.OpenBal  AS Abono,CAST(2 AS int) as Orden,"
   'Consulta &= " CAST(0 AS int) as NumNota,"
   'Consulta &= " T6.OpenBal as MPendiene,'19991212' as FchFFact,T8.SlpCode "
   ' ''--INTO #SaldosFavor
   'Consulta &= " FROM SBO_TPD.dbo.ORCT T6 "
   'Consulta &= " INNER JOIN SBO_TPD.dbo.RCT2 T7 ON T6.DocNum = T7.DocNum "
   'Consulta &= " INNER JOIN SBO_TPD.dbo.OINV T8 ON T8.DocEntry = T7.DocEntry AND T7.InvType = 13 "
   'Consulta &= " WHERE T8.SlpCode = @RAgente AND "
   'Consulta &= " T6.Canceled = 'N' AND T6.DocTotal <> .01 "
   'Consulta &= " AND T6.OpenBal > 0 "
   'Consulta &= " group by T6.CardCode,T6.CardName,T6.DocDate,T6.DocDueDate,T6.OpenBal,T6.Comments,"
   'Consulta &= " T7.DocNum, T8.SlpCode"

   ''Consulta &= " UNION ALL"
   '' ''-------NOTAS DE CREDITO
   ''Consulta &= " SELECT T6.NCSAP AS DocSAP,T7.EDocNum AS 'FactFiscal',T7.CardCode,T7.CardName,T7.DocDate as FchDoc,"
   ''Consulta &= " T7.DocDueDate as FchVenci,T7.DocTotal AS DocTotal,"
   ''Consulta &= " T7.DocTotal- T7.PaidToDate AS Aplicado,"
   ''Consulta &= " T7.DocTotal - T7.PaidToDate as SaldoPendiene,"
   ''Consulta &= " CAST('' AS NVARCHAR(100)) AS FactSaldo,T6.Comments,"
   ''Consulta &= " CAST('' AS NVARCHAR(50)) as IdPago,CAST('' AS Nvarchar(50)) AS TipoMov,'NC'  AS Movimiento,"
   ''Consulta &= " CAST(0 AS numeric(19,6)) AS Cargo,"

   ''Consulta &= " t7.PaidToDate  AS Abono,"

   ''Consulta &= " CAST(3 AS int) as Orden,"
   ''Consulta &= " CAST(0 AS int) as NumNota,"
   ''Consulta &= " T7.DocTotal - T7.PaidToDate as MPendiene,'19991212' as FchFFact,T7.SlpCode "

   ''Consulta &= " FROM @FactCancel T6"
   ''Consulta &= " LEFT JOIN SBO_TPD.dbo.ORIN T7 ON T6.NCSAP=T7.DocNum"

   ''--SE CALCULA EL SALDO DEL CLIENTE,MONTO DE LAS FACTURAS CON SALDO PENDIENTE Y NOTAS DE CREDITO ABIERTAS DE LOS CLIENTES DEL AGENTE
   'Consulta &= " SELECT T8.CardCode,T8.DocTotal as TotFact,T8.PaidToDate AS TotAplicado,"
   'Consulta &= " T8.DocTotal - T8.PaidToDate as TotSaldoP,T8.SlpCode "
   'Consulta &= " INTO #SaldoCte_SAP "
   'Consulta &= " FROM SBO_TPD.dbo.OINV T8 "
   ' ''--where T8.DocTotal - T8.PaidToDate > 0 
   'Consulta &= " WHERE "
   'Consulta &= " T8.SlpCode = @RAgente AND "
   'Consulta &= " T8.DocTotal -T8.PaidToDate > 0"

   'Consulta &= " UNION ALL"

   'Consulta &= " SELECT T9.CardCode,CAST(0 AS numeric(19,6)) AS DocTotal,(DocTotal - PaidToDate) AS TotAplicado,"
   'Consulta &= " CAST(0 AS numeric(19,6)) AS TotSaldoP,T9.SlpCode "
   'Consulta &= " FROM SBO_TPD.dbo.ORIN T9 "
   'Consulta &= " WHERE T9.SlpCode = @RAgente AND "
   'Consulta &= " DocStatus = 'O' "


   ''--SE SUMAN TODAS LAS FACTURAS CON NOTAS DE CREDITO POR CLIENTE,AGENTE
   'Consulta &= " SELECT CardCode,sum(TotFact) as TotFact,Sum(TotAplicado) AS TotAplicado,"
   'Consulta &= " Sum(TotFact - TotAplicado) as TotSaldoP "
   'Consulta &= " INTO #SaldoR_SAP "
   'Consulta &= " FROM #SaldoCte_SAP "
   'Consulta &= " GROUP BY CardCode "


   ''-- SE UNEN TODAS LAS CONSULTAS DE DETALLE Y MONTOS TOTALES

   ''Consulta &= "TRUNCATE TABLE TPM.DBO.CobrGral "

   ''Consulta &= " TRUNCATE TABLE TPM.dbo.CobrGral "

   'Consulta &= " SELECT CardCode,CardName,SUM(Aplicado) AS 'Aplicado' INTO #SaldosFavor FROM("
   'Consulta &= " SELECT T6.DocNum,T6.CardCode,T6.CardName,T6.OpenBal AS Aplicado,T6.OpenBal as SaldoPendiene,"
   'Consulta &= " T6.OpenBal AS Abono,"
   'Consulta &= " T6.OpenBal as MPendiene"
   'Consulta &= " FROM SBO_TPD.dbo.ORCT T6 "
   'Consulta &= " INNER JOIN SBO_TPD.dbo.RCT2 T7 ON T6.DocNum = T7.DocNum "
   'Consulta &= " INNER JOIN SBO_TPD.dbo.OINV T8 ON T8.DocEntry = T7.DocEntry AND T7.InvType = 13 "
   'Consulta &= " WHERE T6.Canceled = 'N'"
   'Consulta &= " AND T6.OpenBal > 0 AND T8.SlpCode=@RAgente"
   'Consulta &= " group by T6.DocNum,T6.CardCode,T6.CardName,T6.OpenBal"
   'Consulta &= " )TMP GROUP BY CardCode,CardName "


   'Consulta &= " INSERT INTO TPM.dbo.CobrGral(DocSAP,FactFiscal,FchDoc,FchVenci,DocTotal,Aplicado,SaldoPendiene,Movimiento,Comments,Cargo,"
   'Consulta &= " Abono,IdPago,NumNota,MPendiene,CardCode,CardName,Phone1,ListaP,PymntGroup,TotFact,TotAplicado,TotSaldoP,FDocSap,FFchDoc,"
   'Consulta &= " FFchVen,FFiscal,SlpCode,SlpName,FchMov,NumClte,FchFFact,COLUMNS_TotFact,COLUMN_TotAplicado,COLUMN_TotSaldoP,Ruta )"

   'Consulta &= " SELECT T1.DocSAP,T1.FactFiscal,T1.FchDoc,T1.FchVenci,"
   'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.DocTotal ELSE NULL END AS DocTotal,"
   'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.Aplicado ELSE NULL END AS Aplicado,"
   'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.SaldoPendiene ELSE NULL END AS SaldoPendiene,"
   'Consulta &= " T1.Movimiento,T1.Comments,T1.Cargo,T1.Abono,T1.IdPago,T1.NumNota,T1.MPendiene,T1.CardCode,"
   'Consulta &= " T6.CardName,T2.Phone1,T2.ListNum AS ListaP,T3.PymntGroup,"
   'Consulta &= " TotFact,TotAplicado,"
   'Consulta &= " TotSaldoP,"
   'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.DocSAP ELSE NULL END AS FDocSap,"
   'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.FchDoc ELSE NULL END AS FFchDoc,"
   'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.FchVenci ELSE NULL END AS FFchVen,"
   'Consulta &= " CASE WHEN Movimiento = 'FACTURA' THEN T1.FactFiscal ELSE NULL END AS FFiscal,"
   'Consulta &= " T1.SlpCode,T5.SlpName,"
   'Consulta &= " CASE WHEN Movimiento <> 'FACTURA' THEN T1.FchDoc ELSE NULL END AS FchMov,"
   'Consulta &= " CAST(REPLACE(T1.CardCode, 'C-', '') AS int) as NumClte,T1.FchFFact,"
   'Consulta &= " T4.TotFact,"
   'Consulta &= " CASE WHEN T7.Aplicado IS NULL THEN T4.TotAplicado ELSE T4.TotAplicado + T7.Aplicado END AS TotAplicado,"
   'Consulta &= " CASE WHEN T7.Aplicado IS NULL THEN T4.TotSaldoP ELSE T4.TotSaldoP - T7.Aplicado END AS TotSaldoP,"
   'Consulta &= " t6.U_BXP_Ruta AS 'Ruta'"
   'Consulta &= " FROM #TmpFacPag T1"
   'Consulta &= " INNER JOIN SBO_TPD.dbo.OCRD T2 ON T1.CardCode = T2.CardCode"
   'Consulta &= " INNER JOIN SBO_TPD.dbo.OCTG T3 ON T2.GroupNum = T3.GroupNum"
   'Consulta &= " INNER JOIN #SaldoR_SAP T4 ON T1.CardCode = T4.CardCode"
   'Consulta &= " INNER JOIN SBO_TPD.dbo.OSLP T5 ON T5.SlpCode = T1.slpcode"
   'Consulta &= " INNER JOIN SBO_TPD.dbo.OCRD T6 ON T1.CardCode = T6.CardCode"
   'Consulta &= " LEFT JOIN #SaldosFavor T7 ON T1.CardCode=T7.CardCode"
   'Consulta &= " WHERE T1.Movimiento <> 'CANCELACION' AND "
   'Consulta &= " T4.TotSaldoP <> 0  "
   'Consulta &= " ORDER BY NumClte,FchFFact,DocSAP,Orden ASC "
   Try
    Consulta &= "select CardCode into #clienteDeAgente from OCRD where SlpCode = @RAgente "
    'If CBRuta.Text <> "" Then
    '    MsgBox("Si elegiste una ruta")
    'End If

    If CBRuta.SelectedIndex.ToString <> "-1" Then
     'MsgBox("Si elegiste una ruta")
     'MsgBox(CBRuta.Text)
     Consulta &= " and U_BXP_Ruta = @ParaRuta "
    End If
    Consulta &= "select t0.CardCode, t0.DocNum, t0.DocDate, t0.DocDueDate, t0.DocTotal, t0.PaidToDate, "
    Consulta &= "(t0.DocTotal - t0.PaidToDate) as 'Resta', t0.TransId, 'F' as 'Clase', "
    'SE REEMPLAZA POR REPLACE
    Consulta &= "REPLACE(REPLACE(t0.CardCode,'-',''),'C','') as 'orden' "
    Consulta &= "into #FacturasPendientes  "
    Consulta &= "from OINV t0 inner join #clienteDeAgente t1 on t0.CardCode = t1.CardCode "
    Consulta &= "where t0.DocTotal - t0.PaidToDate <> 0 "

    Consulta &= "select t0.CardCode, t0.DocNum, t0.DocDate, t0.DocDueDate, t0.DocTotal, t0.PaidToDate,  "
    Consulta &= "(t0.DocTotal - t0.PaidToDate) as 'Resta', t0.TransId, 'A' as 'Clase',  "
    Consulta &= "CASE "
    Consulta &= "when t0.DocType = 'I' then 'NC Dev' "
    Consulta &= "when t0.DocType = 'S' then 'NC Desc' end as 'TipoAbono', "
    'SE REEMPLAZA POR REPLACE
    Consulta &= "REPLACE(REPLACE(t0.CardCode,'-',''),'C','') as 'orden' "
    Consulta &= "into #AbonosPendientes "
    Consulta &= "from ORIN t0 inner join #clienteDeAgente t1 on t0.CardCode = t1.CardCode "
    'Consulta &= "inner join RIN1 t6 on t6.DocEntry = t0.DocEntry "
    Consulta &= "where t0.DocTotal - t0.PaidToDate <> 0 "
    Consulta &= "union all "
    Consulta &= "select t0.CardCode, t0.DocNum, t0.DocDate, t0.DocDueDate, t0.DocTotal, (t0.DocTotal - t0.OpenBal) as 'PaidToDate', "
    Consulta &= "t0.OpenBal as 'Resta', t0.TransId, 'A' as 'Clase', 'Pago' as 'TipoAbono',  "
    'SE REEMPLAZA POR REPLACE
    Consulta &= "REPLACE(REPLACE(t0.CardCode,'-',''),'C','') as 'orden' "
    Consulta &= "from ORCT t0 inner join #clienteDeAgente t1 on t0.CardCode = t1.CardCode "
    Consulta &= "where t0.OpenBal <> 0  "

    Consulta &= "select distinct t3.ShortName, t0.DocNum as 'Factura',  "
    Consulta &= "t4.DocTotal as 'Abono', t4.DocNum, t4.DocDate "
    Consulta &= "into #NotasCredito_TodasFacturas  "
    Consulta &= "from #FacturasPendientes t0 inner join ITR1 t1 on t0.TransId = t1.TransId  "
    Consulta &= "inner join ITR1 t2 on t1.ReconNum = t2.ReconNum inner join ITR1 t3 on t2.ReconNum = t3.ReconNum "
    Consulta &= "inner join ORIN t4 on t3.TransId = t4.TransId inner join #clienteDeAgente t5 on t3.ShortName = t5.CardCode "

    'INICIA MODIFICACION
    Consulta &= " If OBJECT_ID(N'tempdb.dbo.#NotasCredito_TodasFacturas2_PRE', N'U') is not null"
    Consulta &= " DROP TABLE #NotasCredito_TodasFacturas2_PRE; "
    Consulta &= " SELECT DISTINCT t4.CardCode, t4.DocNum as 'Factura',  "
    Consulta &= " t0.Abono as 'Abono 2', "
    Consulta &= " t3.ReconSum as 'Abono',"
    Consulta &= " t1.DocNum, t1.DocDate,  CASE WHEN t1.DocType = 'I' and t7.AcctCode = '2180-001-000' "
    Consulta &= " THEN 'Anticipo'"
    Consulta &= " WHEN t1.DocType = 'I' THEN 'NC Dev' WHEN t1.DocType = 'S' THEN 'NC Desc' END AS 'TipoAbono' "
    Consulta &= " ,t2.ReconNum"
    Consulta &= " INTO #NotasCredito_TodasFacturas2_PRE"
    Consulta &= " FROM #NotasCredito_TodasFacturas t0"
    Consulta &= " inner join ORIN t1 on t0.DocNum = t1.DocNum	"
    Consulta &= " inner join RIN1 t7 on t7.DocEntry = t1.DocEntry"
    Consulta &= " inner join ITR1 t2 on t1.TransId = t2.TransId"
    Consulta &= " inner join ITR1 t3 on t2.ReconNum = t3.ReconNum"
    Consulta &= " and t3.ReconSum > 1.0"
    Consulta &= " inner join OINV t4 on t3.TransId = t4.TransId"
    Consulta &= " AND t4.DocNum In (Select DISTINCT Factura FROM #NotasCredito_TodasFacturas) "
    Consulta &= " inner join OITR t5 on t3.ReconNum = t5.ReconNum "
    Consulta &= " And t5.Canceled <> 'Y'"
    Consulta &= " inner join #clienteDeAgente t6 on t3.ShortName = t6.CardCode "

    ''ACTUALIZO ABONOS EN TABLA
    Consulta &= " UPDATE #NotasCredito_TodasFacturas2_PRE "
    Consulta &= " SET Abono = [Abono 2]"
    Consulta &= " WHERE ReconNum IN (SELECT ReconNum FROM #NotasCredito_TodasFacturas2_PRE "
    Consulta &= " GROUP BY ReconNum, Factura"
    Consulta &= " HAVING COUNT(Factura + ReconNum) > 1)"

    'RELIZO LA CREACION DE lA TABLA TEMPORAL #NotasCredito_TodasFacturas2
    Consulta &= " SELECT DISTINCT CardCode, Factura, Abono, DocNum, DocDate,  TipoAbono"
    Consulta &= " INTO #NotasCredito_TodasFacturas2 "
    Consulta &= " FROM #NotasCredito_TodasFacturas2_PRE "

    'Consulta &= "Select distinct t4.CardCode, t4.DocNum As 'Factura',  "
    'Consulta &= "t3.ReconSum as 'Abono', t1.DocNum, t1.DocDate,  "
    'Consulta &= "CASE when t1.DocType = 'I' and t7.AcctCode = '2180-001-000' then 'Anticipo' "
    'Consulta &= "when t1.DocType = 'I' then 'NC Dev' "
    'Consulta &= "when t1.DocType = 'S' then 'NC Desc' end as 'TipoAbono' "
    'Consulta &= "into #NotasCredito_TodasFacturas2 "
    'Consulta &= "from #NotasCredito_TodasFacturas t0 inner join ORIN t1 on t0.DocNum = t1.DocNum "
    'Consulta &= "inner join RIN1 t7 on t7.DocEntry = t1.DocEntry "
    'Consulta &= "inner join ITR1 t2 on t1.TransId = t2.TransId inner join ITR1 t3 on t2.ReconNum = t3.ReconNum "
    'Consulta &= "inner join OINV t4 on t3.TransId = t4.TransId inner join OITR t5 on t3.ReconNum = t5.ReconNum "
    'Consulta &= "inner join #clienteDeAgente t6 on t3.ShortName = t6.CardCode "
    'Consulta &= "where t4.DocNum in (select Factura from #NotasCredito_TodasFacturas) "
    'Consulta &= "and t5.Canceled <> 'Y' and t3.ReconSum > 1.0 "

    'Consulta &= "select t1.CardCode,t1.DocNum as 'Factura', t2.SumApplied as 'Abono', t3.DocNum, t3.DocDate, "
    'Consulta &= "'Pago' as 'TipoAbono' "
    'Consulta &= "into #Pagos_TodasFactura "
    'Consulta &= "from #FacturasPendientes t0 inner join OINV t1 on t0.DocNum = t1.DocNum  "
    'Consulta &= "inner join RCT2 t2 on t1.DocEntry = t2.DocEntry inner join ORCT t3 on t2.DocNum = t3.DocNum "

    Consulta &= "select distinct t1.CardCode, t1.DocNum as 'Factura', t2.SumApplied as 'Abono', t3.DocNum, t3.DocDate, "
    Consulta &= "'Pago' as 'TipoAbono' "
    Consulta &= "into #Pagos_TodasFactura2 "
    Consulta &= "from #FacturasPendientes t0 inner join OINV t1 on t0.DocNum = t1.DocNum "
    'Consulta &= "inner join RCT2 t2 on t1.DocEntry = t2.DocEntry inner join ORCT t3 on t2.DocNum = t3.DocNum where t3.Canceled <> 'Y' "
    Consulta &= "inner join RCT2 t2 on t1.DocEntry = t2.DocEntry inner join ORCT t3 on t2.DocNum = t3.DocEntry where t3.Canceled <> 'Y' "
    Consulta &= "AND t3.DocTotal > 1.0 "

    Consulta &= "select t4.CardCode, t0.DocNum as 'Factura', t2.ReconSum as 'Abono', t4.DocNum, t4.DocDate, "
    Consulta &= "'Pago' as 'TipoAbono' "
    Consulta &= "into #Pagos_TodasFactura  "
    Consulta &= "from #FacturasPendientes t0 inner join ITR1 t1 on t0.TransId = t1.TransId "
    Consulta &= "left join ITR1 t2 on t1.ReconNum = t2.ReconNum left join OITR t3 on t2.ReconNum = t3.ReconNum "
    Consulta &= "inner join ORCT t4 on t2.TransId = t4.TransId "
    Consulta &= "where t1.ShortName = t0.CardCode and t1.ReconSum > 1.0 and t3.Canceled <> 'Y' "
    Consulta &= "and t4.DocNum not in (select t8.DocNum from #Pagos_TodasFactura2 t8) "
    Consulta &= "union all  "
    Consulta &= "select * from #Pagos_TodasFactura2 "

    'EN ESTE QUERY SE CAMBIO EL TAMAÑO DE LA CONVERSIÓN DE LOS VARCHAR A MONEDA, DEBIDO A QUE MANDABA EL ERROR 
    'There is insufficient result space to convert a money value to varchar
    Consulta &= "select *  "
    Consulta &= "into #Abonos "
    Consulta &= "from #NotasCredito_TodasFacturas2 "
    Consulta &= "union all "
    Consulta &= "select * from #Pagos_TodasFactura "
    Consulta &= "select *, ROW_NUMBER() over (PARTITION BY CardCode, Factura order by CardCode, Factura) as 'NumRow' "
    Consulta &= "into #Abonos2 from #Abonos "
    Consulta &= "select cast(t0.orden as integer) as 'Filtro', t0.CardCode, 1 as Prioridad, t0.DocNum, t0.DocDate, t0.DocDueDate, "
    Consulta &= "t0.DocTotal, t0.PaidToDate, t0.Resta, t0.TransId, t0.Clase, t0.orden, t1.Factura, t1.Abono, t1.DocDate as 'FechaAbono', TipoAbono, NumRow "
    Consulta &= "into #pre_1 "
    Consulta &= "from #FacturasPendientes t0 left join #Abonos2 t1 on t0.DocNum = t1.Factura "
    Consulta &= "union all "
    Consulta &= "select cast(orden as integer) as 'Filtro', CardCode, 2 as Prioridad, DocNum, DocDate, DocDueDate, DocTotal, PaidToDate, Resta, TransId, Clase, orden, "
    Consulta &= "NULL as 'Factura', "
    Consulta &= "NULL as 'Abono', NULL as 'FechaAbono', TipoAbono, NULL as 'NumRow' "
    Consulta &= "from #AbonosPendientes "
    Consulta &= "order by Filtro, Prioridad ASC, DocDate ASC "
    Consulta &= "select distinct convert(varchar(20), t0.CardCode) as 'Cliente',  "
    Consulta &= "convert(varchar(50),t0.CardName) as 'Nombre', convert(varchar(30),t0.Phone1) as 'Telefono',  "
    Consulta &= "convert(varchar(30),t1.PymntGroup) as 'DiasCredito', "
    Consulta &= "'$' + convert(varchar(50), (convert(money, t0.Balance)),1) as 'Saldo',  "
    Consulta &= "convert(varchar(50),t2.SlpName) as 'Agente', convert(varchar(30),t0.U_BXP_Ruta) as 'Ruta', "
    Consulta &= "convert(varchar(50),t3.City) as 'Ciudad', convert(varchar(30),t3.State) as 'Estado', "
    'SE REEMPLAZA POR REPLACE
    Consulta &= "REPLACE(REPLACE(t0.CardCode,'-',''),'C','') as 'orden' "
    Consulta &= "into #ClienteHeader "
    Consulta &= "from OCRD t0 inner join OCTG t1 on t0.GroupNum = t1.GroupNum  "
    Consulta &= "inner join CRD1 t3 on t0.CardCode = t3.CardCode "
    Consulta &= "inner join OSLP t2 on t0.SlpCode = t2.SlpCode inner join #pre_1 t4 on t0.CardCode = t4.CardCode  "
    Consulta &= "where t3.Address = 'FISCAL' and t0.Balance > 0 AND (U_BXP_Estatus IS NULL  or U_BXP_Estatus='04' or U_BXP_Estatus='03' )"
    Consulta &= "select t0.CardCode, CASE when Clase = 'F' then CAST(DocNum as varchar)  "
    Consulta &= "when Clase = 'A' then '-' end as 'Factura', "
    Consulta &= "CASE when NumRow = 1 then convert(varchar(15),DocDate,3) "
    Consulta &= "when NumRow <> 1 then '' "
    Consulta &= "when Clase = 'A' then ''  "
    Consulta &= "when NumRow is null and Clase <> 'A' then convert(varchar(15),DocDate,3) "
    Consulta &= "end as 'Fecha',  "
    Consulta &= "CASE when NumRow = 1 then convert(varchar(15),DocDueDate,3) "
    Consulta &= "when NumRow <> 1 then '' "
    Consulta &= "when Clase = 'A' then '' "
    Consulta &= "when NumRow is null and Clase <> 'A' then convert(varchar(15),DocDueDate,3) "
    Consulta &= "end as 'Vencimiento',  "
    Consulta &= "CASE when NumRow = 1 then cast(DATEDIFF(day, DocDueDate,GETDATE()) as varchar) "
    Consulta &= "when NumRow <> 1 then '' "
    Consulta &= "when NumRow is null and Clase = 'F' then cast(DATEDIFF(day, DocDueDate,GETDATE()) as varchar) "
    Consulta &= "when NumRow is null and Clase = 'A' then '' end as 'DiasAtraso', "
    Consulta &= "CASE when NumRow is not null and NumRow = 1 then '$' + CONVERT(varchar(50), (convert(money,DocTotal)),1) "
    Consulta &= "when NumRow is not null and NumRow <> 1 then '' "
    Consulta &= "when NumRow is null and Clase <> 'A' then '$' + CONVERT(varchar(50), (convert(money,DocTotal)),1) "
    Consulta &= "when NumRow is null and Clase = 'A' then '' "
    Consulta &= "end as 'Importe', "
    Consulta &= "CASE when NumRow is NULL and Clase <> 'A'  then '' "
    Consulta &= "when NumRow is null and Clase = 'A' then '$' + CONVERT(varchar(50), (convert(money,Resta)),1) "
    Consulta &= "when NumRow is not null  then '$' + CONVERT(varchar(50), (convert(money,Abono)),1) end as 'Abono', "
    Consulta &= "CASE when NumRow is null and Clase <> 'A' then '' "
    Consulta &= "when NumRow is null and Clase = 'A' then convert(varchar(15),DocDate,3) "
    Consulta &= "when NumRow is not null then convert(varchar(15),FechaAbono,3) end as 'FechaAbono', "
    Consulta &= "CASE when NumRow is not null and NumRow = (select MAX(NumRow) from #Abonos2 t1 where t1.Factura = t0.Factura) "
    Consulta &= "then '$' + CONVERT(varchar(50), (convert(money,Resta)),1)  "
    Consulta &= "when NumRow is not null and NumRow <> (select MAX(NumRow) from #Abonos2 t1 where t1.Factura = t0.Factura) "
    Consulta &= "then '' "
    Consulta &= "when NumRow is null and Clase <> 'A' then '$' + CONVERT(varchar(50), (convert(money,Resta)),1) "
    Consulta &= "when NumRow is null and Clase = 'A' then '$' + CONVERT(varchar(50), (convert(money,(Resta * -1))),1) "
    Consulta &= "end as 'SaldoPendiente', "
    Consulta &= "CASE when NumRow is not null then TipoAbono "
    Consulta &= "when NumRow is null and Clase <> 'A' then '' "
    Consulta &= "when NumRow is null and Clase = 'A' then TipoAbono "
    Consulta &= "end as 'TipoAbono' "
    Consulta &= "from #pre_1 t0 where CardCode in (select Cliente from #ClienteHeader) "
    Consulta &= "select ('C-' + CAST(orden as varchar)) as 'Cliente', Nombre, Telefono, DiasCredito, Saldo, "
    Consulta &= "Agente, Ruta, Ciudad, Estado "
    Consulta &= "from #ClienteHeader order by CAST(orden as integer) ASC "
    Consulta &= "select Distinct t0.Cliente, (select ISNULL(SUM(DocTotal),0) from #FacturasPendientes where CardCode = t0.Cliente) "
    Consulta &= "as 'Importe', (select ISNULL(SUM(Abono),0) from #Abonos2 where CardCode = t0.Cliente) +  "
    Consulta &= "(select ISNULL(SUM(Resta),0) from #AbonosPendientes where CardCode = t0.Cliente) as 'Abono', "
    'SE REEMPLAZA POR REPLACE
    Consulta &= "CAST(REPLACE(REPLACE(t0.Cliente,'-',''),'C','') as VARCHAR) as 'orden' "
    Consulta &= "into #tmp1 "
    Consulta &= "from #ClienteHeader t0 "
    Consulta &= "select 'C-' + CAST(orden as varchar ) as 'Cliente','$' + convert(varchar(50), (convert(money, Importe)),1) as 'Importe',  "
    Consulta &= "'$' + convert(varchar(50), (convert(money, Abono)),1) as 'Abono',  "
    Consulta &= "'$' + convert(varchar(50), (convert(money,(Importe - Abono))),1) as 'Saldo' "
    Consulta &= "from #tmp1 order by CAST(orden as integer) ASC "
    Consulta &= "select SlpName from OSLP where SlpCode = @RAgente "
    Consulta &= "drop table #Abonos "
    Consulta &= "drop table #Abonos2 "
    Consulta &= "drop table #AbonosPendientes "
    Consulta &= "drop table #ClienteHeader "
    Consulta &= "drop table #FacturasPendientes "
    Consulta &= "drop table #NotasCredito_TodasFacturas "
    Consulta &= "drop table #NotasCredito_TodasFacturas2 "
    Consulta &= "drop table #Pagos_TodasFactura2 "
    Consulta &= "drop table #Pagos_TodasFactura "
    Consulta &= "drop table #clienteDeAgente "
    Consulta &= "drop table #pre_1 "
    Consulta &= "drop table #tmp1 "

    CmdMObra = New SqlClient.SqlCommand(Consulta)

    If Me.CmbAgteVta.SelectedValue.ToString <> " " And Me.CmbAgteVta.SelectedValue.ToString <> "" And Me.CmbAgteVta.SelectedValue Is Nothing = False Then
     CmdMObra.Parameters.Add("@RAgente", SqlDbType.SmallInt)
     CmdMObra.Parameters("@RAgente").Value = CmbAgteVta.SelectedValue
    End If

    If Me.CmbCliente.SelectedValue <> " " And Me.CmbCliente.SelectedValue Is Nothing = False Then
     CmdMObra.Parameters.Add("@ParaCliente", SqlDbType.VarChar)
     CmdMObra.Parameters("@ParaCliente").Value = CmbCliente.SelectedValue
    End If

    If Me.CBRuta.Text <> " " And Me.CmbCliente.Text Is Nothing = False Then
     CmdMObra.Parameters.Add("@ParaRuta", SqlDbType.VarChar)
     CmdMObra.Parameters("@ParaRuta").Value = CBRuta.Text
    End If

    'MsgBox(Consulta)

    CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
    CmdMObra.Connection.Open()

    AdapMObra = New SqlClient.SqlDataAdapter(CmdMObra)
    AdapMObra.SelectCommand.CommandTimeout = 2000

    DsCob = New DataSet
    DataSetX = New DataSet

    AdapMObra.Fill(DsCob)

    DvConsulta = New DataView
    DvConsulta.Table = DsCob.Tables(0) 'origen de datos para la tabla de DvConsulta '
    DTMObra = DvConsulta.ToTable
    DTMObra.TableName = "ResumenSaldo"
    DataSetX.Tables.Add(DTMObra)

    DvConsulta = New DataView
    DTMObra = New DataTable
    DvConsulta.Table = DsCob.Tables(1)
    DTMObra = DvConsulta.ToTable
    DTMObra.TableName = "DatosCliente"
    DataSetX.Tables.Add(DTMObra)

    DvConsulta = New DataView
    DTMObra = New DataTable
    DvConsulta.Table = DsCob.Tables(2)
    DTMObra = DvConsulta.ToTable
    DTMObra.TableName = "SaldosFinales"
    DataSetX.Tables.Add(DTMObra)

    DvConsulta = New DataView
    DTMObra = New DataTable
    DvConsulta.Table = DsCob.Tables(3)
    DTMObra = DvConsulta.ToTable
    DTMObra.TableName = "DatosAgente"
    DataSetX.Tables.Add(DTMObra)

    informe2.SetDataSource(DataSetX)
    RepComsultaP.MdiParent = Inicio
    RepComsultaP.CrVConsulta.ReportSource = informe2



    'If CBRuta.Text = "" Then
    '    Consulta &= " SELECT * FROM TPM.dbo.CobrGral WHERE SLPCODE = @RAgente ORDER BY NumClte ASC "
    'Else
    '    Consulta &= " SELECT * FROM TPM.dbo.CobrGral WHERE SLPCODE = @RAgente AND Ruta = @ParaRuta ORDER BY NumClte ASC "
    'End If

   Catch ex As Exception
    MsgBox("Algo salio mal" & ex.Message)
   End Try


  End If



  'Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

  Try
            '    'MsgBox(Consulta)
            '    CmdMObra.Parameters.Add("@RAgente", SqlDbType.SmallInt)
            '    CmdMObra.Parameters("@RAgente").Value = CmbAgteVta.SelectedValue

            '    If Me.CmbCliente.SelectedValue <> " " And Me.CmbCliente.SelectedValue Is Nothing = False Then
            '        CmdMObra.Parameters.Add("@ParaCliente", SqlDbType.VarChar)
            '        CmdMObra.Parameters("@ParaCliente").Value = CmbCliente.SelectedValue
            '    End If

            '    If Me.CBRuta.Text <> " " And Me.CmbCliente.Text Is Nothing = False Then
            '        CmdMObra.Parameters.Add("@ParaRuta", SqlDbType.VarChar)
            '        CmdMObra.Parameters("@ParaRuta").Value = CBRuta.Text
            '    End If

            '    'If Me.CmbGrupoArticulo.SelectedValue <> 999 Then
            '    '    CmdMObra.Parameters.Add("@Codigo", SqlDbType.SmallInt)
            '    '    CmdMObra.Parameters("@Codigo").Value = CmbGrupoArticulo.SelectedValue
            '    'End If

            '    CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
            '    CmdMObra.Connection.Open()

            '    Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
            '    AdapMObra.SelectCommand.CommandTimeout = 2000

            '    'AdapMObra.Fill(DTMObra)

            '    'GrdConProd.DataSource = DTMObra

            '    '/***********************************************/
            '    Dim DsCob As New DataSet
            '    Dim DataSetX As New DataSet

            '    AdapMObra.Fill(DsCob)


            '    Dim DvConsulta As New DataView
            '    DvConsulta.Table = DsCob.Tables(0) 'origen de datos para la tabla de DvConsulta '

            '    'If Me.CmbCliente.SelectedValue <> " " And Me.CmbCliente.SelectedValue Is Nothing = False Then
            '    '    DvConsulta.RowFilter = "CardCode = " & "'" & Me.CmbCliente.SelectedValue & "'"

            '    'Else
            '    '    DvConsulta.RowFilter = String.Empty
            '    'End If


            '    DTMObra = DvConsulta.ToTable
            '    DTMObra.TableName = "ResumenSaldo"
            '    DataSetX.Tables.Add(DTMObra)

            '    DvConsulta = New DataView
            '    DTMObra = New DataTable
            '    DvConsulta.Table = DsCob.Tables(1)
            '    DTMObra = DvConsulta.ToTable
            '    DTMObra.TableName = "DatosCliente"
            '    DataSetX.Tables.Add(DTMObra)

            '    DvConsulta = New DataView
            '    DTMObra = New DataTable
            '    DvConsulta.Table = DsCob.Tables(2)
            '    DTMObra = DvConsulta.ToTable
            '    DTMObra.TableName = "SaldoFinal"
            '    DataSetX.Tables.Add(DTMObra)

            '    'MsgBox("numero de tablas en datasetx es " & DataSetX.Tables.Count)
            '    'MsgBox(DataSetX.Tables(0).TableName.ToString)
            '    'MsgBox(DataSetX.Tables(1).TableName.ToString)
            '    'MsgBox(DataSetX.Tables(2).TableName.ToString)
            '    'MsgBox(DataSetX.Tables(1).Rows(0)("Nombre"))
            '    'MsgBox(DataSetX.Tables(2).Rows(0)("Saldo").ToString)


            '    ' GrdConProd.DataSource = DTMObra

            '    '/***********************************************/

            'RepComsultaP.MdiParent = Inicio


            'RepComsultaP.CrVConsulta.ReportSource = informe
            If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Or UsrTPM = "NGOMEZ" Or UsrTPM = "COBRANZ5" Or UsrTPM = "VENTAS9" Then
                RepComsultaP.CrVConsulta.ShowExportButton = True
            End If
            RepComsultaP.Show()
  Catch ex As Exception
   MsgBox("hola " & ex.Message)
  End Try

 End Sub


 Private Sub BtnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprimir.Click
  'MsgBox(CmbCliente.SelectedText.ToString)
  'MsgBox(CmbCliente.Text.ToString)
  'MsgBox(CmbCliente.SelectedValue.ToString)
  'CmbCliente.

  If IsNothing(CmbAgteVta.SelectedValue) Then
   If (CmbCliente.SelectedIndex = -1) Then
    If (CmbCliente.Text.ToString = "") Then
     'MsgBox("No hya nada de nada")
     MessageBox.Show("Seleccione un agente de ventas o un cliente",
         "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     Return
    End If
    'MsgBox(CmbCliente.FindString(CmbCliente.Text.ToString).ToString)
    If (CmbCliente.FindString(CmbCliente.Text.ToString).ToString = "-1") Then
     MessageBox.Show("Seleccione un agente de ventas o un cliente",
          "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     'CmbAgteCob.Focus()
     Return
    Else
     CmbCliente.SelectedIndex = CmbCliente.FindString(CmbCliente.Text.ToString)
    End If
    'MsgBox(CmbCliente.FindString(CmbCliente.Text.ToString).ToString)
    'MsgBox(CmbCliente.Text.ToString)
   End If
   'MessageBox.Show("Seleccione un agente de ventas o un cliente", _
   '"Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
   'CmbAgteCob.Focus()
   'Return
  Else
   'MsgBox("entre al else osea que si hay un agente")
   'MsgBox("hola" & CmbCliente.Text.ToString & "mundo")
   If (CmbCliente.Text.ToString <> "") Then
    If (CmbCliente.FindString(CmbCliente.Text.ToString).ToString = "-1") Then
     MessageBox.Show("Seleccione un cliente valido",
          "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     Return
    Else
     CmbCliente.SelectedIndex = CmbCliente.FindString(CmbCliente.Text.ToString)
    End If
   End If
   'If (CmbCliente.Text.ToString <> "" And CmbCliente.FindString(CmbCliente.Text.ToString).ToString = "-1") Then
   '    MsgBox("entrte al segundo if")
   '    MessageBox.Show("Seleccione un cliente valido", _
   '    "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
   '    'CmbAgteCob.Focus()
   '    Return
   'Else
   '    MsgBox("entre al segundo else")
   '    CmbCliente.SelectedIndex = CmbCliente.FindString(CmbCliente.Text.ToString)
   'End If

  End If

  'If IsNothing(CmbAgteVta.SelectedValue) And IsNothing(CmbCliente.SelectedValue) Then
  '    MessageBox.Show("Seleccione un agente de ventas o un cliente", _
  '    "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
  '    'CmbAgteCob.Focus()
  '    Return
  'End If

  'If IsNothing(CmbAgteVta.SelectedValue) And IsNothing(CmbCliente.SelectedValue) Then
  '    MessageBox.Show("Seleccione un agente de ventas o un Cliente", _
  '    "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
  '    CmbAgteVta.Focus()
  '    Return
  'End If

  'If IsNothing(CmbAgteVta.SelectedValue) Then
  '    MessageBox.Show("Seleccione un agente de ventas", _
  '    "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
  '    CmbAgteVta.Focus()
  '    Return
  'End If
  cargar_registros2()

  'BuscaRutas()
  'BuscaRutasCliente()

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
  BuscaRutas()
  BuscaRutasCliente()
 End Sub

 Private Sub CmbAgteVta_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbAgteVta.SelectionChangeCommitted
  BuscaRutas()
  BuscaRutasCliente()
 End Sub

 Private Sub CmbAgteVta_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbAgteVta.KeyUp
  BuscaRutas()
  BuscaRutasCliente()

 End Sub

 Private Sub CBRuta_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CBRuta.Validating
  BuscaRutasCliente()
 End Sub


 Private Sub CBRuta_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBRuta.SelectionChangeCommitted
  BuscaRutasCliente()
 End Sub

 Private Sub CBRuta_KeyUp(sender As Object, e As KeyEventArgs) Handles CBRuta.KeyUp
  BuscaRutasCliente()
 End Sub

 Private Sub CmbCliente_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbCliente.KeyUp
  Try
   If e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
    strTemp = CmbCliente.Text
    If strTemp.Trim.CompareTo(String.Empty) = 0 Then
     DvClte.RowFilter = String.Empty
    Else
     Dim strRowFilter As String = String.Concat("CardName LIKE '%", CmbCliente.Text, "%'")

     If CmbAgteVta.SelectedIndex <> -1 Then
      'MsgBox("si hay agente")
      strRowFilter &= " and slpcode = " & "'" & Trim(Me.CmbAgteVta.SelectedValue.ToString) & "'"
      'DvClte.RowFilter = "slpcode = " & "'" & Trim(Me.CmbAgteVta.SelectedValue.ToString) & "'"
     End If
     If CBRuta.SelectedIndex <> -1 Then
      strRowFilter &= " and ruta = " & "'" & Trim(Me.CBRuta.Text) & "'"
      'DvClte.RowFilter = "ruta = " & "'" & Trim(Me.CBRuta.Text) & "'"
     End If
     DvClte.RowFilter = strRowFilter
    End If


    CmbCliente.Text = ""
    CmbCliente.Text = strTemp
    CmbCliente.SelectionStart = strTemp.Length
    CmbCliente.SelectedIndex = -1
    CmbCliente.DroppedDown = True
    CmbCliente.SelectedIndex = -1
    CmbCliente.Text = ""
    CmbCliente.Text = strTemp
    CmbCliente.SelectionStart = strTemp.Length

   End If



   'DvClte.RowFilter = "Nombre2 like '%" & CmbCliente.Text & "%'"
   'CmbCliente.DroppedDown = True
  Catch ex As Exception
   MsgBox("errror en filtro nuevo " & ex.Message)
  End Try
 End Sub

 Private Sub CmbCliente_DropDown(sender As Object, e As EventArgs) Handles CmbCliente.DropDown
  Me.Cursor = Cursors.Arrow
  CmbCliente.Text = strTemp
 End Sub



 Private Sub CmbCliente_Leave(sender As Object, e As EventArgs) Handles CmbCliente.Leave
  'If (CmbCliente.SelectedIndex = -1) Then
  '    MsgBox(CmbCliente.Text)
  'End If
 End Sub

 Private Sub CmbAgteVta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAgteVta.SelectedIndexChanged

 End Sub

 Private Sub CmbCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCliente.SelectedIndexChanged

 End Sub
End Class