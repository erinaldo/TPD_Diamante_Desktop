Imports System.IO
Imports ClosedXML.Excel

Public Class ConsultaProd
 Dim DivTer As New DataView
 Dim DivIni As New DataView
 Dim objDataSet As New DataTable
 Dim Rangos As String = ""
 Dim Rangos2 As String = ""


 Private Sub ConsultaProd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
  Me.Text = "Agentes -- " & Me.Name.ToString & ".vb"
  If UsrTPM = "MANAGER" Then
   GrdConProd.Height = 550
   Me.Height = 700
  End If

  If VEsAgente = 1 Then
   CkClientes.Checked = False
   CkClientes.Enabled = False
  End If


  Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
  Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")
  'Dim toolTip1 As New ToolTip()
  'toolTip1.SetToolTip(Me.BtnExcel, "Exportar consulta de producción a Excel")
  'Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

  '    Dim da As New SqlClient.SqlDataAdapter("SELECT GETDATE() AS FechaServer FROM Turnos Where Id_Turno = '1RO'", SqlConnection)

  '    Dim ds As New DataSet
  '    da.Fill(ds)


  '    End Using
 End Sub

 Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConsultar.Click
  BtnConsultar.Enabled = False
  cargar_registros()
  BtnConsultar.Enabled = True
 End Sub

 Sub cargar_registros()

  Dim Consulta As String = ""
  Dim strcadena As String = ""
  Dim CTabla As String = ""
  Dim DTMObra As New DataTable
  Dim DTProb As New DataTable

  '** VENTAS TOTALES POR VENDEDOR
  '** LO FACTURADO
  'Serie 59 son las facturas que cancelan NC'

  'ANTES del 16 de Enero de 2018 era asi:------------------------------------------------------------------------------------------
  'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as VtaAntesIva "
  'Consulta &= "FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DOCNUM <> 29870 AND DocType <> 'S' AND SERIES<>59 "
  'FIN DE ANTES DEL 16 DE ENERO--------------------------------------------------------------------------------------------------------------------------------

  'DESPUES DEL 16 de Enero de 2018' *----------------------------------------------------------------------------------------------------------
  'se quita la validacion de serie <> 59 para contabilizar la notas de cargo (facturas de ARTICULO para cancelar notas de credito mal aplicadas)'
  'ya que estas se usan para saldar la NC de articulo mal aplicada'
  'por ejemplo, la factura 2000993 con doctype 'I' y series 59 es una factura de articulo que se uso para cancelar una nota de credito mal aplicada'
  'dicha factura se debe de contabilizar, ya que a pesar de ser serie 59, es una factura de ARTICULO y se debe contabilizar '
  'este cambio se planteo con el Lic Salvador y él autorizo que se quedara asi'

  'ESTE REPORTE NO VA A CUADRAR AL 100% CON EL REPORTE DE VENTAS DE SAP, YA QUE EN LA SIG CONSULTA, SE ESTAN EXCLUYENDO LAS FACTURAS DE SERVICIO'
  'ESTAS FACTURAS DE SERVICIO SE REALIZAN PARA AJUSTAR SALDOS, POR LO QUE NO SE CONSIDERA VENTA DE LOS AGENTES Y POR ENDE, NO SE TOMAN EN CUENTA'
  'SI EN ALGUN MOMENTO, SE QUISIERA CUADRAR CONTRA SAP, UNICAMENTE DE LA SIG CONSULTA, HABRIA QUE QUITAR LA CONDICION DE " DocType <> 'S' "'
  'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as VtaAntesIva "
  'Consulta &= "FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType <> 'S'  "


  'OBTIENE TODAS LAS FACTURAS DE ARTICULOS
  Consulta = "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
  Consulta &= "into #TodasFacturasArticulos "
  Consulta &= "from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry  "
  Consulta &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
  Consulta &= "where t0.DocDate between @FechaIni and @FechaTer  "
  Consulta &= "and t0.DocType <> 'S' "
  Consulta &= "and t2.ItmsGrpCod <> 200 "
  If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
   Consulta &= "and t0.slpCode = " & vCodAgte

  ElseIf UsrTPM = "VVERGARA" Then
   'Consulta &= "and Slpcode in (select slpcode from TPM.dbo.DEPCOBR where groupcode = 103 ) "
   'Consulta &= "and (slpCode = 8 OR slpCode = 12 OR slpCode = 26 OR slpCode = 30) "
   Consulta &= "and t0.Slpcode in (select SlpCode from OSLP where Memo = '07' ) "


  ElseIf UsrTPM = "RROBLES" Then
   'Consulta &= "and (select slpcode from TPM.dbo.DEPCOBR where groupcode = 102) "
   Consulta &= "and t0.Slpcode in (select SlpCode from OSLP where Memo = '03' ) "

  Else
   If (CkClientes.Checked = False) Then
    Consulta &= " and t0.slpCode <> 1 "
   End If

  End If

  'OBTIENE TODAS LAS NC NO TIMBRADAS DE ARTICULOS
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
  Consulta &= "AND T3.ReportID IS NULL "

  If VEsAgente = 1 And UsrTPM <> "VVERGARA" And UsrTPM <> "RROBLES" Then
   Consulta &= "and t0.slpCode = " & vCodAgte

  ElseIf UsrTPM = "VVERGARA" Then
   Consulta &= "and t0.Slpcode in (select SlpCode from OSLP where Memo = '07' ) "
  ElseIf UsrTPM = "RROBLES" Then
   Consulta &= "and t0.Slpcode in (select SlpCode from OSLP where Memo = '03' ) "
  Else
   If (CkClientes.Checked = False) Then
    Consulta &= " and t0.slpCode <> 1 "
   End If
  End If

  'INSERTA EN #TEMP1 LS VENTAS SIN IVA DE LAS FACTURAS
  Consulta &= "SELECT t0.Slpcode as IdVend,SUM((t0.DocTotal - t0.VatSum) - t0.TotalExpns) as VtaAntesIva  "
  Consulta &= "into #tmp1 "
  Consulta &= "FROM #TodasFacturasArticulos t0 "
  Consulta &= "GROUP BY t0.SlpCode ORDER BY VtaAntesIva DESC "

  'INSERTA EN #TEMP2 LAS VENTAS SIN IVA DE LAS NC
  Consulta &= "SELECT t0.Slpcode as IdVend,SUM((t0.DocTotal - t0.VatSum) - t0.TotalExpns) as VtaAntesIva  "
  Consulta &= "into #tmp2 "
  Consulta &= "FROM #NC_Art_NoTimb t0 "
  Consulta &= "GROUP BY t0.SlpCode ORDER BY VtaAntesIva DESC "

  'Inserto los agentes que no hayan tenido ventas pero si NCR, basicamente los que esten en la #temp2 pero no en la #temp1 con importe 0
  Consulta &= "INSERT INTO #tmp1 "
  Consulta &= "SELECT t0.IdVend, 0 FROM #tmp2 t0 "
  Consulta &= "LEFT OUTER JOIN #tmp1 t1 ON t0.IdVend = t1.IdVend "
  Consulta &= "WHERE t1.IdVend Is NULL "

  'OBTIENE LOS RESULTADOS DE LAS OPERACIONES DE RESTAR EL TOTAL DE NC EN LAS FACTURAS TOTALS
  Consulta &= "select t0.IdVend, t0.VtaAntesIva - ISNULL(t1.VtaAntesIva,0) as 'VtaAntesIva' "
  Consulta &= "from #tmp1 t0 left join #tmp2 t1 on t0.IdVend = t1.IdVend ORDER BY VtaAntesIva DESC "
  'ELIMINAMOS LAS TABLAS TEMPORALES
  Consulta &= "drop table #TodasFacturasArticulos "
  Consulta &= "drop table #NC_Art_NoTimb "
  Consulta &= "drop table #tmp1 "
  Consulta &= "drop table #tmp2 "

  'FIN DEL DESPUES DEL 16 DE ENERO------------------------------------------------------------------------------------------------------------



  'Consulta &= "SELECT t0.Slpcode as IdVend,SUM((t0.DocTotal - t0.VatSum) - t0.TotalExpns) as VtaAntesIva  "
  'Consulta &= "FROM #TodasFacturasArticulos t0 "
  'Consulta &= "GROUP BY t0.SlpCode ORDER BY VtaAntesIva DESC "
  'Consulta &= "drop table #TodasFacturasArticulos "

  'Consulta &= "GROUP BY OINV.SlpCode ORDER BY VtaAntesIva DESC"

  Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

  CmdMObra.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime)
  CmdMObra.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
  CmdMObra.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime)
  CmdMObra.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value
  CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
  CmdMObra.CommandTimeout = 200
  CmdMObra.Connection.Open()

  Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
  AdapMObra.Fill(DTMObra)
  CmdMObra.Connection.Close()


  '** Se crea cursor para reporte
  ' CTabla = "CREATE TABLE #REPVTAS (IdVend INT,VtaAntesIva Numeric(20,2),PorVtas Numeric(20,2),"
  CTabla = "CREATE TABLE #REPVTAS (IdVend INT,VtaAntesIva Numeric(20,2),"
  CTabla &= "Dev Numeric(20,2),PorDev Numeric(20,2),Des Numeric(20,2),PorDes Numeric(20,2),"
  'CTabla &= "Cancels Numeric(20,2),PorCan Numeric(20,2),VtaCDes Numeric(20,2),PorCDes Numeric(20,2),"
  CTabla &= "VtaCDes Numeric(20,2),PorCDes Numeric(20,2),"
  CTabla &= "VtasNt Numeric(20,2),TotNC Numeric(20,2))"

  Dim cmdcost As Data.SqlClient.SqlCommand
  cmdcost = New Data.SqlClient.SqlCommand()

  With cmdcost
   .Connection = New Data.SqlClient.SqlConnection(StrCon)
   .Connection.Open()
   .CommandText = CTabla
   .CommandTimeout = 200
   .ExecuteNonQuery()
  End With


  '** SE OBTIENE EL MONTO TOTAL DE LAS VENTAS Y SE AGREGA AL CURSOR-----------------------------------------------------------



  Dim TotalVtas As Double
  Dim cmd As New Data.SqlClient.SqlCommand
  With cmd
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   '.CommandText = "SELECT SUM((DocTotal - VatSum) - TotalExpns) as VtaAntesIva FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType <> 'S' "  ''AND DOCNUM <> 2000458 AND DOCNUM <> 2000459

   '.CommandText = "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
   '.CommandText &= "into #TodasFacturasArticulos2 "
   '.CommandText &= "from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry  "
   '.CommandText &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
   '.CommandText &= "where t0.DocDate between @FechaIni and @FechaTer  "
   '.CommandText &= "and t0.DocType <> 'S' "
   '.CommandText &= "and t2.ItmsGrpCod <> 200 "
   'If (CkClientes.Checked = False) Then
   '    .CommandText = .CommandText + " and t0.slpCode <> 1 "
   'End If

   '.CommandText &= "SELECT SUM((DocTotal - VatSum) - TotalExpns) as VtaAntesIva  "
   '.CommandText &= "FROM #TodasFacturasArticulos2  "
   '.CommandText &= "drop table #TodasFacturasArticulos2 "


   .CommandText = "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
   .CommandText &= "into #TodasFacturasArticulos2 "
   .CommandText &= "from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry  "
   .CommandText &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
   .CommandText &= "where t0.DocDate between @FechaIni and @FechaTer  "
   .CommandText &= "and t0.DocType <> 'S' "
   .CommandText &= "and t2.ItmsGrpCod <> 200 "
   If (CkClientes.Checked = False) Then
    .CommandText = .CommandText + " and t0.slpCode <> 1 "
   End If

   .CommandText &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
   .CommandText &= "into #NC_Art_NoTimb2 "
   .CommandText &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
   .CommandText &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
   .CommandText &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
   .CommandText &= "where t0.DocDate between @FechaIni and @FechaTer "
   .CommandText &= "and t0.DocType <> 'S' "
   .CommandText &= "and t2.ItmsGrpCod <> 200 "
   .CommandText &= "and (t3.ReportID is null AND t0.U_BXP_UUID IS NULL) "
   If (CkClientes.Checked = False) Then
    .CommandText = .CommandText + " and t0.slpCode <> 1 "
   End If

   .CommandText &= "SELECT t0.Slpcode as IdVend,SUM((t0.DocTotal - t0.VatSum) - t0.TotalExpns) as VtaAntesIva  "
   .CommandText &= "into #tmp1_2 "
   .CommandText &= "FROM #TodasFacturasArticulos2 t0 "
   .CommandText &= "GROUP BY t0.SlpCode ORDER BY VtaAntesIva DESC "

   .CommandText &= "SELECT t0.Slpcode as IdVend,SUM((t0.DocTotal - t0.VatSum) - t0.TotalExpns) as VtaAntesIva  "
   .CommandText &= "into #tmp2_2 "
   .CommandText &= "FROM #NC_Art_NoTimb2 t0 "
   .CommandText &= "GROUP BY t0.SlpCode ORDER BY VtaAntesIva DESC "

   .CommandText &= "select SUM(t0.VtaAntesIva - ISNULL(t1.VtaAntesIva,0)) 'VtaAntesIva' "
   .CommandText &= "from #tmp1_2 t0 left join #tmp2_2 t1 on t0.IdVend = t1.IdVend "

   .CommandText &= "drop table #TodasFacturasArticulos2 "
   .CommandText &= "drop table #NC_Art_NoTimb2 "
   .CommandText &= "drop table #tmp1_2 "
   .CommandText &= "drop table #tmp2_2 "

   .CommandTimeout = 200
   .CommandType = CommandType.Text
   .Connection = New Data.SqlClient.SqlConnection(StrCon)
   .Connection.Open()
   TotalVtas = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
   .Connection.Close()

  End With
  'MsgBox("Total: " & TotalVtas.ToString)


  For Each fila As DataRow In DTMObra.Rows
   strcadena = "INSERT INTO #REPVTAS (IdVend, VtaAntesIva) VALUES ("
   strcadena &= fila("IdVend")
   strcadena &= ","
   strcadena &= fila("VtaAntesIva")

   strcadena &= ")"

   With cmdcost
    '.Parameters.AddWithValue("@FechaCosto", VMObFechaP2)
    .CommandText = strcadena
    .ExecuteNonQuery()
    ' .Parameters.Clear()
   End With
  Next


  '******Consulta para contemplar los agentes que no tienen ventas**********************************
  Dim DTSinVta As New DataTable

  strcadena = "SELECT ORIN.SlpCode as IdVend FROM ORIN WHERE DocDate >= @FechaIni AND "
  strcadena &= "DocDate <= @FechaTer AND ORIN.SlpCode not in "
  strcadena &= "(select distinct t0.SlpCode from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry  "
  strcadena &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
  strcadena &= "where t0.DocDate between @FechaIni and @FechaTer  "
  strcadena &= "and t0.DocType <> 'S' and t2.ItmsGrpCod <> 200) "

  'strcadena &= "(SELECT Slpcode FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType <> 'S' "  ''AND DOCNUM <> 2000458 AND DOCNUM <> 2000459
  'strcadena &= " GROUP BY OINV.SlpCode) "
  If (CkClientes.Checked = False) Then
   strcadena &= " and slpCode <> 1 "
  End If
  strcadena &= "GROUP BY ORIN.SlpCode "

  Dim CmdSinVta As New SqlClient.SqlCommand(strcadena)
  CmdSinVta.CommandTimeout = 200
  CmdSinVta.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime)
  CmdSinVta.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
  CmdSinVta.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime)
  CmdSinVta.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value
  CmdSinVta.Connection = New SqlClient.SqlConnection(StrCon)
  CmdSinVta.Connection.Open()

  Dim AdapSinVta As New SqlClient.SqlDataAdapter(CmdSinVta)
  AdapSinVta.Fill(DTSinVta)
  CmdSinVta.Connection.Close()


  For Each fila As DataRow In DTSinVta.Rows
   strcadena = "INSERT INTO #REPVTAS (IdVend, VtaAntesIva) VALUES ("
   strcadena &= fila("IdVend")
   strcadena &= ","
   strcadena &= "0"

   strcadena &= ")"

   With cmdcost
    '.Parameters.AddWithValue("@FechaCosto", VMObFechaP2)
    .CommandText = strcadena
    .ExecuteNonQuery()
    ' .Parameters.Clear()
   End With
  Next



  '************************************************************************************

  If VEsAgente <> 1 Or UsrTPM = "VVERGARA" Or UsrTPM = "RROBLES" Then

   strcadena = "INSERT INTO #REPVTAS (IdVend, VtaAntesIva) VALUES ("
   strcadena &= 0
   strcadena &= ","
   strcadena &= TotalVtas

   strcadena &= ")"

   With cmdcost
    '.Parameters.AddWithValue("@FechaCosto", VMObFechaP2)
    .CommandText = strcadena
    .ExecuteNonQuery()
    ' .Parameters.Clear()
   End With

  End If
  '****************************************************************************************************************************************************************************************



  Dim DTRefacciones As New DataTable

  'strcadena = "SELECT #REPVTAS.IdVend,OSLP.SlpName,"
  'strcadena &= "CAST(#REPVTAS.VtaCDes AS dec(10,2)) as VtaCDes, "
  'strcadena &= "CAST(#REPVTAS.PorCDes AS dec(5,2)) as PorCDes, "
  'strcadena &= "Dev,PorDev,Des,PorDes,Cancels,PorCan, "
  'strcadena &= "CAST(#REPVTAS.VtaAntesIva AS dec(10,2)) as VtaAntesIva, "
  'strcadena &= "CAST(#REPVTAS.PorVtas AS dec(5,2)) as PorVtas "
  'strcadena &= "FROM #REPVTAS LEFT JOIN OSLP ON #REPVTAS.IdVend = OSLP.SlpCode"

  '*SE AGREGA EL NOMBRE DEL AGENTE A LA CONSULTA QUE CONTIENE LO FACTURADO Y AGENTES QUE NO VENDIERON
  strcadena = "SELECT #REPVTAS.IdVend,OSLP.SlpName,"
  strcadena &= "CAST(#REPVTAS.VtaCDes AS dec(20,2)) as VtaCDes, "
  strcadena &= "CAST(#REPVTAS.PorCDes AS dec(5,2)) as PorCDes, "
  'strcadena &= "Dev,PorDev,VtasNt,Des,PorDes,TotNC,Cancels,PorCan, "
  strcadena &= "Dev,PorDev,VtasNt,Des,PorDes,TotNC, "
  strcadena &= "CAST(#REPVTAS.VtaAntesIva AS dec(20,2)) as VtaAntesIva "
  'strcadena &= "CAST(#REPVTAS.PorVtas AS dec(5,2)) as PorVtas "
  strcadena &= "FROM #REPVTAS LEFT JOIN OSLP ON #REPVTAS.IdVend = OSLP.SlpCode"
  If (CkClientes.Checked = False) Then
   strcadena &= " and OSLP.slpCode <> 1"
  End If



  'VtasNt Numeric(10,2),TotNC

  'vYTermino
  With cmdcost
   .CommandText = strcadena
  End With

  Dim DAdapter As New SqlClient.SqlDataAdapter(cmdcost)

  DAdapter.Fill(DTRefacciones)

  'cmdcost.Connection.Close()
  'DEVOLUCIONES----------------------------------------------------------------------------------------------------------------------
  Dim DataCRec As Data.SqlClient.SqlDataReader

  Dim fi As String
  Dim ft As String

  'ALMACENAN LA FECHA OBTENIDA EN EL FORMULARIO
  fi = DtpFechaIni.Value.ToString("yyyy-MM-dd")
  ft = DtpFechaTer.Value.ToString("yyyy-MM-dd")

  'VALIDAR FECHAS PARA PODER SABER QUE DATOS OBTENER
  If (fi > ft) Then 'VALIDA QUE LA FECHA DE TERMINO NO SEA MAYOR QUE LA INCIAL
   MsgBox("La fecha de termino no puede ser mayor a la de Inicio")
  ElseIf (fi <= "2017-12-31" And ft <= "2017-12-31") Then 'VALIDA QUE LA FECHA INICIAL Y LA DE TERMINO SEA MENOR AL 01-01-2018
   Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DevAntesIva "
   Consulta &= "FROM ORIN t0 WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType  = 'I' AND   "
   Consulta &= "(CASE when DocDate <= '2017-11-19' then "
   Consulta &= "EDocNum else "
   Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end IS NOT NULL OR t0.U_BXP_UUID IS NOT NULL)"
   'VALIDA SI ESTA ACTIVADA LA CASILLA DE CLIENTES PROPIOS
   If (CkClientes.Checked = False) Then
    Consulta &= " and slpCode <> 1 "
   End If
   Consulta &= "GROUP BY t0.SlpCode ORDER BY DevAntesIva DESC "
  ElseIf (fi >= "2018-01-01" And ft >= "2018-01-01") Then 'VALIDA QUE LA FECHA DE INICIO Y DE TERMINO SEAN MAYOR A 01-01-2018
   Consulta = "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
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
   'VALIDA SI ESTA CON CLIENTES PROPIOS
   If (CkClientes.Checked = False) Then
    Consulta &= " and t0.slpCode <> 1 "
   End If
   Consulta &= "select SlpCode as IdVend, SUM((DocTotal - VatSum) - TotalExpns) as DevAntesIva  "
   Consulta &= "from #NC_Art_Timb "
   Consulta &= "group by SlpCode "
   Consulta &= "order by DevAntesIva DESC "

   Consulta &= "drop table #NC_Art_Timb "
  End If
  'FIN VALIDAR FECHAS PARA PODER SABER QUE DATOS OBTENER



  'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DevAntesIva "
  'Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType  = 'I' AND  EDocNum IS NOT NULL "
  'If (CkClientes.Checked = False) Then
  '    Consulta &= " and slpCode <> 1"
  'End If
  'Consulta &= "GROUP BY ORIN.SlpCode ORDER BY DevAntesIva DESC"


  'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DevAntesIva "
  'Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND Eseries = 6"
  'Consulta &= "GROUP BY ORIN.SlpCode ORDER BY DevAntesIva DESC"


  With cmdcost
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   .CommandText = Consulta
   cmdcost.CommandTimeout = 1200
   DataCRec = .ExecuteReader()
  End With

  Dim TotDev As Double
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


  ''DESCUENTOS PRONTO PAGO----------------------------------------------------------------------------------------------------------------

  cmdcost.Connection.Close()
  Dim DtVtaDes As Data.SqlClient.SqlDataReader
  'BANDERA PARA VALIDAR DATOS DE DESCUENTOS DE PAGO
  Dim datos_ok As Boolean
  datos_ok = False

  'VALIDA LAS FECHAS PARA OBTENCION DE DATOS
  If (fi > ft) Then 'VALIDA QUE LA FECHA DE TERMINO NO SEA MAYOR QUE LA INCIAL
   MsgBox("La fecha de termino no puede ser mayor a la de Inicio")
  ElseIf (fi <= "2017-12-31" And ft >= "2018-01-01") Then
   MsgBox("Favor de seleccionar otro periodo, debido a Cambios de Sistema SAP, Los datos de Descuentos P.P no seran los correctos")
   'INICIALIZA EN FALSE LA BANDERA DE DATOS DEBIDO A QUE NO PUEDE HABER DATOS DE DPP EN ESTE PERIODO
   datos_ok = False
  ElseIf (fi <= "2017-12-31" And ft <= "2017-12-31") Then 'VALIDA QUE LA FECHA INICIAL Y LA DE TERMINO SEA MENOR AL 01-01-2018
   Consulta = "SELECT Slpcode as IdVend, SUM((DocTotal - VatSum) - TotalExpns) as DesAntesIva "
   Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer "
   Consulta &= "AND (Eseries != 6 or Eseries is null) AND DocType  = 'S' AND Series <> 49 "
   'Consulta &= "CASE when DocDate <= '2017-11-19' then "
   'Consulta &= "EDocNum else (select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end IS NOT NULL  "
   If (CkClientes.Checked = False) Then
    Consulta &= " and SlpCode <> 1 "
   End If
   Consulta &= "GROUP BY ORIN.SlpCode ORDER BY DesAntesIva DESC "
   datos_ok = True
   'URIEL: NUEVA 2018
   'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DesAntesIva "
   'Consulta &= "FROM ORIN t0 WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType  = 'S' "
   'Consulta &= " GROUP BY T0.SlpCode ORDER BY DesAntesIva DESC "
  ElseIf (fi >= "2018-01-01" And ft >= "2018-01-01") Then 'VALIDA QUE LA FECHA DE INICIO Y DE TERMINO SEAN MAYOR A 01-01-2018
   'OBTIENE TODAS LAS FACTURAS CON DESCUENTOS P.P
   Consulta = "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
   Consulta &= "into #FacturasPP "
   Consulta &= "from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry "
   Consulta &= "where  "
   Consulta &= "t0.DocDate between @FechaIni and @FechaTer and  "
   Consulta &= "t0.DocType = 'I' "
   Consulta &= " AND (t1.ItemCode = 'DESCUENTO P.P' OR t1.ItemCode = 'AP_ANTICIPO') "
   'VALIDA CASILLA DE CLIENTES PROPIOS
   If (CkClientes.Checked = False) Then
    Consulta &= " and t0.slpCode <> 1"
   End If
   'OBTIENE LAS NC CON DESCUENTOS P.P.
   Consulta &= "select distinct t0.DocEntry, t0.DocNum, t0.SlpCode, t0.DocTotal, t0.VatSum, t0.TotalExpns "
   Consulta &= "into #NCPP "
   Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
   Consulta &= "where  "
   Consulta &= "t0.DocDate between @FechaIni and @FechaTer and  "
   Consulta &= "t0.DocType = 'I' and "
   Consulta &= "(T1.ItemCode = 'DESCUENTO P.P' OR T1.ItemCode = 'AP_ANTICIPO') "
   If (CkClientes.Checked = False) Then
    Consulta &= " and t0.slpCode <> 1"
   End If
   'LISTA DATOS DE LOS ANGENTES CON BASE A LAS FACTURAS DE DESCUENTOS P.P
   Consulta &= "select Slpcode as IdVend,(SUM((DocTotal - VatSum) - TotalExpns))* -1 as DesAntesIva "
   Consulta &= "into #tmp13 "
   Consulta &= "from #FacturasPP "
   Consulta &= "group by SlpCode "
   'LISTA DATOS DE LOS ANGENTES CON BASE A LAS NC DE DESCUENTOS P.P
   Consulta &= "select Slpcode as IdVend,(SUM((DocTotal - VatSum) - TotalExpns)) as DesAntesIva "
   Consulta &= "into #tmp14 "
   Consulta &= "from #NCPP "
   Consulta &= "group by SlpCode "
   'LISTA LOS DATOS DE LOS AGENTES CON EL TOTAL DE DESCUENTOS ENTRE LAS FACTURAS Y NC
   Consulta &= "select t0.IdVend, t0.DesAntesIva + isnull(t1.DesAntesIva,0) as DesAntesIva "
   Consulta &= "from #tmp14 t0 left join #tmp13 t1 on t0.IdVend = t1.IdVend ORDER BY DesAntesIva DESC "
   'ELIMINA LAS TABLAS TEMPORALES
   Consulta &= "drop table #FacturasPP "
   Consulta &= "drop table #NCPP "
   Consulta &= "drop table #tmp13 "
   Consulta &= "drop table #tmp14 "
   datos_ok = True
  End If
  'FIN VALIDA LAS FECHAS PARA OBTENCION DE DATOS

  'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DesAntesIva "
  'Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer "
  'Consulta &= "AND DocType  = 'S' "
  'If (CkClientes.Checked = False) Then
  '    Consulta &= " and slpCode <> 1"
  'End If
  'Consulta &= "GROUP BY ORIN.SlpCode ORDER BY DesAntesIva DESC"
  'VALIDA QUE LOS DATOS DE DESCUENTOS PP SEANLOS CORRECTOS
  Dim TotDes As Double
  If (datos_ok = True) Then
   With cmdcost
    .Connection = New Data.SqlClient.SqlConnection(StrCon)
    cmdcost.Connection.Open()
    .Parameters.Clear()
    .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
    .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
    .CommandText = Consulta
    DtVtaDes = .ExecuteReader()
   End With

   '            Dim TotDes As Double
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
  End If
  'FIN VALIDA DATOS DE DESCUENTOS PP

  cmdcost.Connection.Close()

  'CANCELACIONES-----------------------------------------------------------------------------------------------------------------
  cmdcost.Connection.Close()
  Dim DRCanc As Data.SqlClient.SqlDataReader

  'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels "
  'Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer "
  'Consulta &= "AND (Eseries != 6 or Eseries is null) AND DocType  = 'I' AND Series <> 49 "
  'Consulta &= "GROUP BY ORIN.SlpCode ORDER BY TotCancels DESC"

  If (DtpFechaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then 'si la fecha de inicio es antes del 2 de enero de 2018'
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then '... y si la fecha de fin es antes del 2 de enero de 2018'

    'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels "
    'Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer "
    'Consulta &= "AND DocType  = 'I' AND  EDocNum IS NULL "
    'If (CkClientes.Checked = False) Then
    '    Consulta &= " and slpCode <> 1"
    'End If
    'Consulta &= "GROUP BY ORIN.SlpCode ORDER BY TotCancels DESC"

    ' consulta de inicio a fin
    Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels "
    Consulta &= "FROM ORIN t0 WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DocType  = 'I' AND   "
    Consulta &= "(CASE when DocDate <= '2017-11-19' then "
    Consulta &= "EDocNum else (select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end IS NULL AND t0.U_BXP_UUID IS NULL) "
    If (CkClientes.Checked = False) Then
     Consulta &= " and slpCode <> 1 "
    End If
    Consulta &= "GROUP BY t0.SlpCode ORDER BY TotCancels DESC "
   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then '... y la fecha de fin es despues del 2 de enero'
    ' consulta de la fecha de inicio hacia el 31 de diciembre de 2017
    ' union
    ' consulta del 1 de enero hacia la fecha de fin
    Consulta = "select distinct SlpCode, DocTotal, VatSum, TotalExpns, t0.docnum, "
    Consulta &= "CASE  when DocDate <= '2017-11-19' then "
    Consulta &= "EDocNum else t1.ReportID end as 'EDocNum' "
    Consulta &= "into #tmp "
    Consulta &= "from ORIN t0 left join ECM2 t1 on t0.DocEntry = t1.SrcObjAbs and t1.SrcObjType = 14 "
    Consulta &= "WHERE DocDate >=  @FechaIni AND DocDate <= '2017-12-31' AND DocType  = 'I'  "
    If (CkClientes.Checked = False) Then
     Consulta &= " and t0.slpCode <> 1 "
    End If

    Consulta &= "select distinct t0.SlpCode, t0.DocTotal, t0.VatSum, TotalExpns, t0.docnum,  "
    Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.docEntry and t1.SrcObjType = 14) as 'EDocNum' "
    Consulta &= "into #tmp2 "
    Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry  "
    Consulta &= "INNER JOIN OITM T2 on T1.ItemCode = T2.ItemCode "
    Consulta &= "LEFT JOIN ECM2 T3 on T0.DocEntry = T3.SrcObjAbs AND T3.SrcObjType = 14 "
    Consulta &= "where T0.DocDate between  @FechaIni AND @FechaTer AND T0.DocType <> 'S' "
    Consulta &= "AND (T2.ItmsGrpCod <> 200 OR T0.Series = 59 OR T0.Series = 88) "
    'Consulta &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    Consulta &= "AND ((CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate >= '2020-08-24' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    Consulta &= "AND T0.U_BXP_TIMBRADO <> 'T') "
    Consulta &= "AND T3.ReportID IS NULL "

    If (CkClientes.Checked = False) Then
     Consulta &= " and t0.slpCode <> 1 "
    End If

    Consulta &= "select * into #tmp3  "
    Consulta &= "from #tmp union all select * from #tmp2 "
    Consulta &= "select  Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels  "
    Consulta &= "from #tmp3 where EDocNum is null "
    Consulta &= "GROUP BY SlpCode ORDER BY TotCancels DESC "
    Consulta &= "drop table #tmp "
    Consulta &= "drop table #tmp2 "
    Consulta &= "drop table #tmp3 "

   End If

  ElseIf (DtpFechaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
   If (DtpFechaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then
    ' no hace nada porque la fecha de inicio no debe ser mayor a la fecha de termino
   ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then '
    ' consulta de inicio a fin
    Consulta = "select distinct t0.SlpCode, t0.DocTotal, t0.VatSum, TotalExpns, t0.docnum,  "
    Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.docEntry and t1.SrcObjType = 14) as 'EDocNum' "
    Consulta &= "into #tmp2 "
    Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry  "
    Consulta &= "INNER JOIN OITM T2 on T1.ItemCode = T2.ItemCode "
    Consulta &= "LEFT JOIN ECM2 T3 on T0.DocEntry = T3.SrcObjAbs AND T3.SrcObjType = 14 "
    Consulta &= "where T0.DocDate between @FechaIni AND @FechaTer AND T0.DocType <> 'S' "
    Consulta &= "AND (T2.ItmsGrpCod <> 200 OR T0.Series = 59 OR T0.Series = 88) "
    'Consulta &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    Consulta &= "AND ((CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate >= '2020-08-24' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
    Consulta &= "AND T0.U_BXP_TIMBRADO <> 'T') "
    Consulta &= "AND T3.ReportID IS NULL "

    If (CkClientes.Checked = False) Then
     Consulta &= " and t0.slpCode <> 1 "
    End If
    Consulta &= "select  Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels  "
    Consulta &= "from #tmp2 where EDocNum is null "
    Consulta &= "GROUP BY SlpCode ORDER BY TotCancels DESC "
    Consulta &= "drop table #tmp2 "
   End If
  End If

  With cmdcost
   .Connection = New Data.SqlClient.SqlConnection(StrCon)
   cmdcost.Connection.Open()
   .Parameters.Clear()
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   .CommandText = Consulta
   cmdcost.CommandTimeout = 1200
   DRCanc = .ExecuteReader()
  End With

  Dim vnetas As Double
  For Each fila As DataRow In DTRefacciones.Rows

   If fila("IdVend") = 0 Then
    fila("SlpName") = "MONTO TOTAL"
    'fila("Cancels") = TotCanc
   End If

   fila("VtaCDes") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) '- IIf(IsDBNull(fila("Cancels")), 0, fila("Cancels"))
   'fila("VtasNt") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) - (IIf(IsDBNull(fila("Cancels")), 0, fila("Cancels")) + IIf(IsDBNull(fila("Dev")), 0, fila("Dev")))
   fila("VtasNt") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) - (IIf(IsDBNull(fila("Dev")), 0, fila("Dev")))
   fila("TotNC") = IIf(IsDBNull(fila("Des")), 0, fila("Des")) + IIf(IsDBNull(fila("Dev")), 0, fila("Dev"))

   If fila("IdVend") <> 0 Then
    vnetas = vnetas + fila("VtaCDes")
   End If


  Next


  Dim TotVnetas As Double
  Dim TotNCred As Double

  For Each fila As DataRow In DTRefacciones.Rows
   If fila("IdVend") = 0 Then
    fila("Dev") = TotDev
    fila("Des") = TotDes
    'fila("Cancels") = TotCanc
    fila("VtaCDes") = vnetas
    fila("VtasNt") = TotVnetas
    fila("TotNC") = TotNCred
   End If

   If fila("IdVend") <> 0 Then
    If Not IsDBNull(fila("Dev")) And fila("VtaCDes") <> 0 Then
     If fila("Dev") <> 0 Then
      fila("PorDev") = fila("Dev") / fila("VtaCDes") 'ORIGINAL: fila("PorDev") = fila("Dev") * 100 / fila("VtaCDes")
     End If
    End If


    If Not IsDBNull(fila("Des")) And fila("VtaCDes") <> 0 Then
     If fila("Des") <> 0 Then
      fila("PorDes") = fila("Des") / fila("VtaCDes")    'ORIGINAL: fila("PorDes") = fila("Des") * 100 / fila("VtaCDes")
     End If

    End If


    TotVnetas = TotVnetas + fila("VtasNt")
    TotNCred = TotNCred + fila("TotNC")

   End If


   If Not IsDBNull(fila("VtaCDes")) Then
    If fila("VtaCDes") <> 0 Then
     fila("PorCDes") = fila("VtaCDes") / vnetas    'ORIGINAL:  fila("PorCDes") = fila("VtaCDes") * 100 / vnetas
    End If
   End If

  Next




  cmdcost.Connection.Close()

  With Me.GrdConProd
   .DataSource = DTRefacciones
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
   .MultiSelect = True
   .AllowUserToAddRows = False
   'Color de linea del grid
   .Columns(0).HeaderText = "Clave"
   .Columns(0).Width = 40
   .Columns(1).HeaderText = "Vendedor"
   .Columns(1).Width = 180
   ' .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns(2).HeaderText = "Ventas Totales"
   .Columns(2).Width = 120
   .Columns(2).DefaultCellStyle.Format = "$ ###,###,###.00"
   .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   '.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns(3).HeaderText = "% Vtas. Tot."
   .Columns(3).Width = 60
   .Columns(3).DefaultCellStyle.Format = "##0.00 %"
   .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(4).HeaderText = "Monto Devuelto"
   .Columns(4).Width = 105
   .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(4).DefaultCellStyle.Format = "$ ###,###,###.00"

   .Columns(5).HeaderText = "% Dvol. Sobre Venta"
   .Columns(5).Width = 60
   .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(5).DefaultCellStyle.Format = "##.00 %"


   .Columns(6).HeaderText = "Ventas Netas"
   .Columns(6).Width = 110
   .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(6).DefaultCellStyle.Format = "$ ###,###,###.00"
   '.Columns(6).DefaultCellStyle.BackColor = Color.Gray


   .Columns(7).HeaderText = "Descuentos Pronto Pago"
   .Columns(7).Width = 105
   .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(7).DefaultCellStyle.Format = "$ ###,###,###.00"

   .Columns(8).HeaderText = "% PP Sobre Venta"
   .Columns(8).Width = 60
   .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(8).DefaultCellStyle.Format = "##.00 %"
   '  .Columns(7).Visible = False

   .Columns(9).HeaderText = "Total Notas de Credito"
   .Columns(9).Width = 105
   .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(9).DefaultCellStyle.Format = "$ ###,###,###.00"




   .Columns(10).HeaderText = "Facturación"
   .Columns(10).Width = 120
   .Columns(10).DefaultCellStyle.Format = "$ ###,###,###.00"
   .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(10).Visible = True

   Dim numfilas As Integer

   numfilas = GrdConProd.RowCount 'cuenta las filas del DataGrid

   'recorre las filas del DataGrid
   For i = 0 To (numfilas - 1)
    GrdConProd.Rows(i).Cells(6).Style.BackColor = Color.LightGray
    'GrdConProd.Rows(i).Cells(6).Style.ForeColor = Color.White
   Next
  End With



 End Sub

 Private Sub CmbLineaIni_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
  e.KeyChar = Char.ToUpper(e.KeyChar)
 End Sub

 Private Sub CmbLineaTer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
  e.KeyChar = Char.ToUpper(e.KeyChar)
 End Sub

 Private Sub CmbNParteIni_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
  e.KeyChar = Char.ToUpper(e.KeyChar)
 End Sub

 Private Sub CmbNParteTer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
  e.KeyChar = Char.ToUpper(e.KeyChar)
 End Sub

 Private Sub CmbTurnoIni_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
  e.KeyChar = Char.ToUpper(e.KeyChar)
 End Sub

 Private Sub CmbTurnoTer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
  e.KeyChar = Char.ToUpper(e.KeyChar)
 End Sub

 Private Sub BtnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExcel.Click
  ExportarNuevoAgentes()
 End Sub

 Sub ExportarNuevoAgentes()
  'Dim dv As DataView = DirectCast(GrdConProd.DataSource, DataView)
  'Dim ds As DataSet = GrdConProd.DataSource
  Dim dt As DataTable = GrdConProd.DataSource


  Dim wb = New XLWorkbook()
  Dim ws = wb.Worksheets.Add("Ventas Totales por Agente")

  Dim range = ws.Range(3, 1, dt.Rows.Count + 3, dt.Columns.Count).Merge().AddToNamed("Totales")
  Dim rangeWithData = ws.Cell(4, 1).InsertData(dt.AsEnumerable)

  Dim tab = range.CreateTable()
  tab.Theme = XLTableTheme.TableStyleLight8

  'DAR FOMATO A LAS CELDAS
  Dim index As Integer = 3

  For i As Integer = 0 To dt.Rows.Count

   Try
    'Encabezados dependiendo
    If index = 3 Then
     Dim cellA3 As String = String.Format("A{0}", 1)
     wb.Worksheet(1).Cells(cellA3).Value = "Reporte de Ventas Totales por Agente  Del Periodo " & Format(Me.DtpFechaIni.Value)
     wb.Worksheet(1).Cells(cellA3).Style.Font.Bold = True

     Dim cellA0 As String = String.Format("A{0}", index)
     wb.Worksheet(1).Cells(cellA0).Value = "Clave"

     Dim cellB0 As String = String.Format("B{0}", index)
     wb.Worksheet(1).Cells(cellB0).Value = "Vendedor"

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
     wb.Worksheet(1).Cells(cellI0).Value = "% PP Sobre Vent"

     Dim cellJ0 As String = String.Format("J{0}", index)
     wb.Worksheet(1).Cells(cellJ0).Value = "Total Notas de Credito"




     Dim cellK0 As String = String.Format("K{0}", index)
     wb.Worksheet(1).Cells(cellK0).Value = "Facturación"

     index = index + 1
    End If

    'Formato de cada una de las celdas
    Dim cellA As String = String.Format("A{0}", index)
    'wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "#,##0"

    Dim cellB As String = String.Format("B{0}", index)
    'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellC As String = String.Format("C{0}", index)
    wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellD As String = String.Format("D{0}", index)
    wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "0.00 %"

    Dim cellE As String = String.Format("E{0}", index)
    wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellF As String = String.Format("F{0}", index)
    wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "0.00 %"

    Dim cellG As String = String.Format("G{0}", index)
    wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellH As String = String.Format("H{0}", index)
    wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "$ #,##0.00"

    Dim cellI As String = String.Format("I{0}", index)
    wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "0.00 %"

    Dim cellJ As String = String.Format("J{0}", index)
    wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "$ #,##0.00"


    Dim cellK As String = String.Format("K{0}", index)
    wb.Worksheet(1).Cells(cellK).Style.NumberFormat.Format = "$ #,##0.00"


   Catch ex As Exception
    MessageBox.Show(ex.ToString(), "Error al dar formato a celdas (Totales): ")
   End Try

   index = index + 1
  Next
  ws.Columns().Width = 15
  ws.Rows(3).Style.Alignment.WrapText = False

  Try
   Dim saveFileDialog1 As New SaveFileDialog()
   saveFileDialog1.Filter = "Excel|*.xlsx"
   saveFileDialog1.Title = "Save Excel File"
   saveFileDialog1.FileName = "Ventas Totales por asesor al " & DtpFechaIni.Value.ToString("dd-MM-yyyy") & ".xlsx"
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
   MessageBox.Show(ex.ToString(), "Error al guardar el archivo (Totales): ")
  End Try
 End Sub



 Private Sub GrdConProd_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GrdConProd.ColumnHeaderMouseClick
  Dim numfilas As Integer

  numfilas = GrdConProd.RowCount 'cuenta las filas del DataGrid

  'recorre las filas del DataGrid
  For i = 0 To (numfilas - 1)
   GrdConProd.Rows(i).Cells(6).Style.BackColor = Color.LightGray
   'GrdConProd.Rows(i).Cells(6).Style.ForeColor = Color.White
  Next
 End Sub
End Class