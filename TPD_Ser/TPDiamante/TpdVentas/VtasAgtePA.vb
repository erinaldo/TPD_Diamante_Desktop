Imports System.Data.SqlClient
Imports System.Data

Public Class Reporte_de_Ventas_DetallePA
 Private dv As New DataView
 Private dvTodosArt As New DataView
 Dim DivTer As New DataView
 Dim DivIni As New DataView
 Dim objDataSet As New DataTable
 Dim Rangos As String = ""
 Dim Rangos2 As String = ""

 Private Sub ConsultaProd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
  Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
  Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")
 End Sub

 Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
  cargar_registros()
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
  Consulta = "SELECT t0.Slpcode as IdVend,SUM((t0.DocTotal - t0.VatSum) - t0.TotalExpns) as VtaAntesIva "
  Consulta &= "FROM OINV t0 INNER JOIN INV1 t1 on t0.DocEntry = t1.DocEntry "
  Consulta &= "INNER JOIN OITM t2 on t1.ItemCode = t2.ItemCode "
  Consulta &= "WHERE t0.DocDate >= @FechaIni AND t0.DocDate <= @FechaTer AND t0.DOCNUM <> 29870 AND t0.DOCNUM <> 29821 "
  Consulta &= "and t0.DocType <> 'S' "
  Consulta &= "and t2.ItmsGrpCod <> 200 "
  If VEsAgente = 1 Then
   Consulta &= "AND t0.Slpcode = " & vCodAgte.ToString & " "
  End If
  Consulta &= "GROUP BY t0.SlpCode ORDER BY VtaAntesIva DESC"

  'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as VtaAntesIva "
  '  Consulta &= "from OINV t0 inner join INV1 t1 on t0.DocEntry = t1.DocEntry "
  '  Consulta &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
  '  Consulta &= "WHERE t0.DocDate >= @FechaIni AND t0.DocDate <= @FechaTer AND t0.DOCNUM <> 29870 AND t0.DOCNUM <> 29821 "
  '  Consulta &= "and t0.DocType <> 'S' "
  '  Consulta &= "and t2.ItmsGrpCod <> 200 "
  '  If VEsAgente = 1 Then
  '    Consulta &= "AND OINV.Slpcode = " & vCodAgte.ToString & " "
  '  End If
  '  Consulta &= "GROUP BY OINV.SlpCode ORDER BY VtaAntesIva DESC"

  Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

  CmdMObra.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime)
  CmdMObra.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
  CmdMObra.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime)
  CmdMObra.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value
  CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
  CmdMObra.Connection.Open()

  Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
  AdapMObra.Fill(DTMObra)
  CmdMObra.Connection.Close()


  '** Se crea tabla temporal
  CTabla = "CREATE TABLE #REPVTAS (IdVend INT,VtaAntesIva Numeric(12,2),PorVtas Numeric(10,2),"
  CTabla &= "Dev Numeric(10,2),PorDev Numeric(5,2),Des Numeric(10,2),PorDes Numeric(5,2),"
  CTabla &= "Cancels Numeric(10,2),PorCan Numeric(5,2),VtaCDes Numeric(10,2),PorCDes Numeric(5,2),"
  CTabla &= "VtasNt Numeric(10,2),TotNC Numeric(10,2))"

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
   If VEsAgente = 1 Then
    CTabla = "SELECT SUM((DocTotal - VatSum) - TotalExpns) as VtaAntesIva FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DOCNUM <> 29870 AND DOCNUM <> 29821 AND SERIES <> 59 "
    CTabla &= "AND OINV.Slpcode = " & vCodAgte.ToString
   Else
    CTabla = "SELECT SUM((DocTotal - VatSum) - TotalExpns) as VtaAntesIva FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND DOCNUM <> 29870 AND DOCNUM <> 29821 AND SERIES <> 59 "
   End If

   .CommandText = CTabla
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
   strcadena &= Format(fila("VtaAntesIva"), "###.00")
   strcadena &= ","
   strcadena &= Format(Val(fila("VtaAntesIva") * 100 / TotalVtas), "###.00")
   strcadena &= ")"

   With cmdcost
    .CommandText = strcadena
    .ExecuteNonQuery()
   End With
  Next


  '******Consulta para contemplar los agentes que no tienen ventas**********************************
  Dim DTSinVta As New DataTable

  strcadena = "SELECT ORIN.SlpCode as IdVend FROM ORIN WHERE DocDate >= @FechaIni AND "
  If VEsAgente = 1 Then
   strcadena &= "ORIN.Slpcode = " & vCodAgte.ToString & " AND "
  End If
  strcadena &= "DocDate <= @FechaTer AND ORIN.SlpCode not in "
  strcadena &= "(SELECT Slpcode FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer"
  strcadena &= " GROUP BY OINV.SlpCode) GROUP BY ORIN.SlpCode"

  Dim CmdSinVta As New SqlClient.SqlCommand(strcadena)


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
  strcadena &= "CAST(#REPVTAS.VtaCDes AS dec(12,2)) as VtaCDes, "
  strcadena &= "CAST(#REPVTAS.PorCDes AS dec(10,2)) as PorCDes, "
  strcadena &= "Dev,PorDev,VtasNt,Des,PorDes,TotNC,Cancels,PorCan, "
  strcadena &= "CAST(#REPVTAS.VtaAntesIva AS dec(12,2)) as VtaAntesIva, "
  strcadena &= "CAST(#REPVTAS.PorVtas AS dec(10,2)) as PorVtas "
  strcadena &= "FROM #REPVTAS LEFT JOIN OSLP ON #REPVTAS.IdVend = OSLP.SlpCode"

  With cmdcost
   .CommandText = strcadena
  End With

  Dim DAdapter As New SqlClient.SqlDataAdapter(cmdcost)

  DAdapter.Fill(DTRefacciones)

  'DEVOLUCIONES
  Dim DataCRec As Data.SqlClient.SqlDataReader

  Consulta = "SELECT t0.Slpcode as IdVend,SUM((t0.DocTotal - t0.VatSum) - t0.TotalExpns) as DevAntesIva "
  Consulta &= "from ORIN t0 inner join RIN1 t1 on t0.DocEntry = t1.DocEntry "
  Consulta &= "inner join OITM t2 on t1.ItemCode = t2.ItemCode "
  Consulta &= "left join ECM2 t3 on t0.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 "
  Consulta &= "where t0.DocDate between @FechaIni and @FechaTer "
  Consulta &= "and t0.DocType <> 'S' "
  Consulta &= "and t2.ItmsGrpCod <> 200 "
  Consulta &= "AND (T3.ReportID IS NOT NULL OR T0.U_BXP_UUID IS NOT NULL) "
  Consulta &= "AND ((T0.U_BXP_TIMBRADO = 'T' OR T0.U_BXP_TIMBRADO = 'P') OR T0.EDocGenTyp = 'G') "
  If VEsAgente = 1 Then
   Consulta &= "AND t0.Slpcode = " & vCodAgte.ToString & " "
  End If
  Consulta &= "GROUP BY t0.SlpCode ORDER BY DevAntesIva DESC"


  With cmdcost
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   .CommandText = Consulta
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
  'DESCUENTOS PRONTO PAGO
  cmdcost.Connection.Close()
  Dim DtVtaDes As Data.SqlClient.SqlDataReader


  Consulta = "SELECT T0.Slpcode as IdVend,SUM((T0.DocTotal - T0.VatSum) - T0.TotalExpns) as DesAntesIva "
  Consulta &= "From ORIN T0 INNER Join RIN1 T1 on T0.DocEntry = T1.DocEntry "
  Consulta &= "Where T0.DocDate between @FechaIni AND @FechaTer "
  Consulta &= "And  T0.DocType = 'I' AND (T1.ItemCode = 'DESCUENTO P.P' OR T1.ItemCode = 'AP_ANTICIPO') "
  If VEsAgente = 1 Then
   Consulta &= "AND T0.Slpcode = " & vCodAgte.ToString & " "
  End If
  Consulta &= "GROUP BY T0.SlpCode ORDER BY DesAntesIva DESC "

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
  'CANCELACIONES
  cmdcost.Connection.Close()
  Dim DRCanc As Data.SqlClient.SqlDataReader


  Consulta = "SELECT T0.Slpcode as IdVend,SUM((T0.DocTotal - T0.VatSum) - T0.TotalExpns) as TotCancels "
  'Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer "
  Consulta &= "from ORIN T0 INNER JOIN RIN1 T1 on T0.DocEntry = T1.DocEntry "
  Consulta &= "INNER JOIN OITM T2 on T1.ItemCode = T2.ItemCode "
  Consulta &= "LEFT JOIN ECM2 T3 on T0.DocEntry = T3.SrcObjAbs AND T3.SrcObjType = 14 "
  Consulta &= "where T0.DocDate between @FechaIni AND @FechaIni AND T0.DocType <> 'S'  "
  Consulta &= "AND (T2.ItmsGrpCod <> 200 OR T0.Series = 59 OR T0.Series = 88) "
  'Consulta &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
  Consulta &= "AND ((CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate >= '2020-08-24' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
  Consulta &= "AND T0.U_BXP_TIMBRADO <> 'T') "
  Consulta &= "AND T3.ReportID IS NULL "

  If VEsAgente = 1 Then
   Consulta &= "AND T0.Slpcode = " & vCodAgte.ToString & " "
  End If
  Consulta &= "GROUP BY T0.SlpCode ORDER BY TotCancels DESC"


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


   fila("VtaCDes") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) - IIf(IsDBNull(fila("Cancels")), 0, fila("Cancels"))
   fila("VtasNt") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) - (IIf(IsDBNull(fila("Cancels")), 0, fila("Cancels")) + IIf(IsDBNull(fila("Dev")), 0, fila("Dev")))
   fila("TotNC") = IIf(IsDBNull(fila("Des")), 0, fila("Des")) + IIf(IsDBNull(fila("Dev")), 0, fila("Dev"))


   If fila("IdVend") <> 0 Then
    vnetas = vnetas + fila("VtaCDes")
   End If


  Next


  Dim TotVnetas As Decimal
  Dim TotNCred As Decimal

  For Each fila As DataRow In DTRefacciones.Rows
   If fila("IdVend") = 0 Then
    fila("Dev") = TotDev
    fila("Des") = TotDes
    fila("Cancels") = TotCanc
    fila("VtaCDes") = vnetas
    fila("VtasNt") = TotVnetas
    fila("TotNC") = TotNCred
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

  Dim SQLTPD As String


  '--CANCELACIONES
  SQLTPD = "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,"
  SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,"
  SQLTPD &= "(CAST('CANCELACION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE AS VARCHAR(22)) AS ArtAgte,"
  SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
  SQLTPD &= "INTO #Tem_Canc "
  SQLTPD &= "FROM ORIN T0 INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry "
  SQLTPD &= "INNER JOIN OITM T2 on T1.ItemCode = T2.ItemCode "
  SQLTPD &= "LEFT JOIN ECM2 T3 on T0.DocEntry = T3.SrcObjAbs AND T3.SrcObjType = 14 "
  SQLTPD &= "where T0.DocDate between @FechaIni AND @FechaTer AND T0.DocType <> 'S' "
  SQLTPD &= "AND (T2.ItmsGrpCod <> 200 OR T0.Series = 59 OR T0.Series = 88) "
  'SQLTPD &= "AND (CASE WHEN T0.DocDate <= '2019-02-10' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
  SQLTPD &= "AND ((CASE WHEN T0.DocDate <= '2019-02-10' OR T0.DocDate >= '2020-08-24' THEN T0.EDocGenTyp ELSE T0.U_BXP_TIMBRADO END) = 'N' "
  SQLTPD &= "AND T0.U_BXP_TIMBRADO <> 'T') "
  Consulta &= "AND T3.ReportID IS NULL) "
  If VEsAgente = 1 Then
   SQLTPD &= "AND T0.Slpcode = " & vCodAgte.ToString & " "
  End If
  SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode  "


  '--DEVOLUCIONES
  SQLTPD &= "SELECT T1.ItemCode,SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,"
  SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,"
  SQLTPD &= "(CAST('DEVOLUCION' AS CHAR(20))) AS TipoMov,CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + T1.ITEMCODE AS VARCHAR(22)) AS ArtAgte,"
  SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
  SQLTPD &= "INTO #Tem_Dev "
  SQLTPD &= "FROM ORIN T0 INNER JOIN RIN1 T1 ON T1.DocEntry = T0.DocEntry "
  SQLTPD &= "INNER JOIN OITM T2 on T1.ItemCode = T2.ItemCode "
  SQLTPD &= "LEFT JOIN ECM2 T3 on T0.DocEntry = T3.SrcObjAbs AND T3.SrcObjType = 14 "
  SQLTPD &= "where T0.DocDate between @FechaIni AND @FechaTer AND T0.DocType <> 'S' "
  SQLTPD &= "AND (T2.ItmsGrpCod <> 200 OR T0.Series = 59) "
  SQLTPD &= "AND (T3.ReportID IS NOT NULL OR T0.U_BXP_UUID IS NOT NULL) "
  SQLTPD &= "AND ((T0.U_BXP_TIMBRADO = 'T' OR T0.U_BXP_TIMBRADO = 'P') OR T0.EDocGenTyp = 'G') "
  If VEsAgente = 1 Then
   SQLTPD &= "AND T0.Slpcode = " & vCodAgte.ToString & " "

  End If
  SQLTPD &= "GROUP BY T1.ItemCode,T0.SlpCode  "


  '--FACTURADO
  SQLTPD &= "SELECT CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS ItemCode,"
  SQLTPD &= "SUM(T1.Quantity) AS CANTIDAD,T0.SlpCode AS IdAgente,"
  SQLTPD &= "SUM(CASE WHEN t0.DiscPrcnt is null  THEN T1.LineTotal ELSE T1.LineTotal - T1.LineTotal * T0.DiscPrcnt / 100 END) AS TotalSinIva,"
  SQLTPD &= "CAST(CASE WHEN T1.ItemCode IS NULL THEN 'NCARGO' ELSE 'FACTURACION' END AS CHAR(20))AS TipoMov,"
  SQLTPD &= "CAST(RTRIM(LTRIM(CAST(T0.SlpCode AS VARCHAR(2)))) + CASE WHEN T1.ItemCode IS NULL THEN 'NOTA-DE-CARGO' ELSE T1.ItemCode END AS VARCHAR(22)) AS ArtAgte,"
  SQLTPD &= "CAST(0 AS NUMERIC(19,6)) AS CANTNETA "
  SQLTPD &= "INTO #Tem_Fact FROM OINV T0 INNER JOIN INV1 T1 ON T1.DocEntry = T0.DocEntry "
  SQLTPD &= "INNER JOIN OITM T2 on T1.ItemCode = T2.ItemCode "
  SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "
  SQLTPD &= "AND T0.DocType <> 'S' AND (T2.ItmsGrpCod <> 200 OR T0.Series = 59 OR T0.Series = 88) "
  If VEsAgente = 1 Then
   SQLTPD &= "AND T0.Slpcode = " & vCodAgte.ToString & " "
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

  'Se consultan todos los montos totales por linea y tambien por linea agente
  SQLTPD &= "SELECT T0.ItmsGrpNam AS Linea,T0.CANT_TOT AS Cantidad,T0.MONT_TOT AS Monto,T0.POR_TOT AS PorVta,T0.CantDev,"
  SQLTPD &= "T0.MontoDev,T0.POR_DEV,T0.CantNeta,T0.MontoNeto,CASE WHEN T1.SlpName IS NULL THEN 'TODOS' ELSE T1.SlpName END AS Agente,"
  SQLTPD &= "T0.ItmsGrpCod,CAST(T0.IdAgente AS SMALLINT) AS IdVend,CAST(T0.ItmsGrpCod AS VARCHAR(6)) + CAST(T0.IdAgente AS VARCHAR(4)) as LineaAgte "
  SQLTPD &= "FROM #T_GRUP_AGTE T0 LEFT JOIN OSLP T1 ON T0.IdAgente = T1.SlpCode ORDER BY T0.IdAgente,T0.MontoNeto DESC; "

  'If VEsAgente <> 1 Then
  '    SQLTPD &= "FROM #T_GRUP_AGTE T0 LEFT JOIN OSLP T1 ON T0.IdAgente = T1.SlpCode ORDER BY T0.IdAgente,T0.MontoNeto DESC; "
  'Else
  '    SQLTPD &= "FROM #T_GRUP_AGTE T0 LEFT JOIN OSLP T1 ON T0.IdAgente = T1.SlpCode WHERE T0.IdAgente <> 0 ORDER BY T0.IdAgente,T0.MontoNeto DESC; "


  'End If

  ''T0.POR_TOT DESC


  'Se consultan todos los montos totales por articulo y tambien por articulo,linea,agente
  SQLTPD &= "SELECT CASE WHEN T0.Articulo IS NULL THEN 'NOTA-DE-CARGO' ELSE T0.Articulo END AS Articulo,CASE WHEN T1.ItemCode IS NULL THEN 'NOTAS DE CARGO' ELSE T1.ItemName END AS Descripcion,T0.CantFact,T0.MontFact,T0.PorFact,T0.CantDev,"
  SQLTPD &= "T0.MontoDev,T0.PorDev,T0.CantNeta,T0.MontNeto,T0.Lnea,CASE WHEN T2.SlpName IS NULL THEN 'TODOS' ELSE T2.SlpName END AS Agente,"
  SQLTPD &= "T0.IdAgente,T0.ItmsGrpCod,"
  SQLTPD &= "CAST(T0.ItmsGrpCod AS VARCHAR(6)) + CAST(T0.IdAgente AS VARCHAR(4)) as LineaAgte,T0.MontLineDev,T0.MontoLinea " 'T0.MontoLinea  MontNeto
  SQLTPD &= "FROM #T_AGTE_ART T0 LEFT JOIN OITM T1 ON T0.Articulo = T1.ItemCode "
  SQLTPD &= "LEFT JOIN OSLP T2 ON T0.IdAgente = T2.SlpCode ORDER BY IdAgente,T0.MontoLinea - T0.MontLineDev  DESC, T0.MontNeto DESC;  "
  'PorFact

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




  With GrdConProd
   .DataMember = "EncVtasTot"
   .DataSource = DsVtasDet
   'Color de Renglones en Grid
   .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
   .ColumnHeadersHeight = 35
   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .RowHeadersVisible = False
   .SelectionMode = DataGridViewSelectionMode.FullRowSelect
   .MultiSelect = False
   .AllowUserToAddRows = False
   .ReadOnly = True
   .Columns(0).HeaderText = "Clave"
   .Columns(0).Width = 40

   .Columns(1).HeaderText = "Vendedor"
   .Columns(1).Width = 170

   .Columns(2).HeaderText = "$ Ventas Totales"
   .Columns(2).Width = 90
   .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(3).HeaderText = "% Vtas."
   .Columns(3).Width = 44
   .Columns(3).DefaultCellStyle.Format = "###.00"
   .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(4).HeaderText = "$ Monto Devuelto"
   .Columns(4).Width = 80
   .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(4).DefaultCellStyle.Format = "###,###,###.00"

   .Columns(5).HeaderText = "% Dvol. S/Vta"
   .Columns(5).Width = 50
   .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(5).DefaultCellStyle.Format = "###.00"

   .Columns(6).HeaderText = "$ Ventas Netas"
   .Columns(6).Width = 90
   .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(6).DefaultCellStyle.Format = "###,###,###.00"

   .Columns(7).HeaderText = "Descuento Pronto Pago"
   .Columns(7).Width = 80
   .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(7).DefaultCellStyle.Format = "###,###,###.00"

   .Columns(8).HeaderText = "% PP S/Vta"
   .Columns(8).Width = 45
   .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(8).DefaultCellStyle.Format = "###.00"

   .Columns(9).HeaderText = "$ Notas de Credito"
   .Columns(9).Width = 80
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

  ' Agregar la relación ( campo en común : campo_Relacionado = idCliente )   
  ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''   

  With DsVtasDet
   .Relations.Add("Relacion1",
                          .Tables("EncVtasTot").Columns("IdVend"),
                          .Tables("DetalleLinea").Columns("IdVend"))
  End With


  ' Establecer el DataSource y el DataMember para el DataGridview Detalle   
  With GrdConLinea
   .DataSource = DsVtasDet
   .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
   .ColumnHeadersHeight = 35
   .DataMember = "EncVtasTot.Relacion1"
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
   .Columns(0).Width = 140

   .Columns(1).HeaderText = "Piezas"
   .Columns(1).Width = 80
   .Columns(1).DefaultCellStyle.Format = "###,###,###"
   .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(2).HeaderText = "$ Venta Total"
   .Columns(2).Width = 90
   .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(3).HeaderText = "% Vtas."
   .Columns(3).Width = 60
   .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(3).DefaultCellStyle.Format = "###.00"

   .Columns(4).HeaderText = "Piezas Devueltas"
   .Columns(4).Width = 80
   .Columns(4).DefaultCellStyle.Format = "###,###,###"
   .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(5).HeaderText = "$ Monto Devuelto"
   .Columns(5).Width = 89
   .Columns(5).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(6).HeaderText = "% Dvol. S/Vta"
   .Columns(6).Width = 60
   .Columns(6).DefaultCellStyle.Format = "###.00"
   .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(7).HeaderText = "Cant. Neta"
   .Columns(7).Width = 80
   .Columns(7).DefaultCellStyle.Format = "###,###,###"
   .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(8).HeaderText = "$ Monto Neto"
   .Columns(8).Width = 90
   .Columns(8).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(9).Visible = False
   .Columns(10).Visible = False
   .Columns(11).Visible = False
   .Columns(12).Visible = False

  End With

  'DsVtasDet.Tables(2)
  dv.Table = DsVtasDet.Tables("DetalleArticulo")
  dvTodosArt.Table = DsVtasDet.Tables("DetalleArticulo")

  GrdTodosArt.DataSource = dvTodosArt


  With GrdDetArt
   .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
   .ColumnHeadersHeight = 35
   .DataSource = dv
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

   .Columns(0).HeaderText = "Articulo"
   .Columns(0).Width = 100

   .Columns(1).HeaderText = "Descripción"
   .Columns(1).Width = 230

   .Columns(2).HeaderText = "Piezas"
   .Columns(2).Width = 50
   .Columns(2).DefaultCellStyle.Format = "###,###,###"
   .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(3).HeaderText = "$ Venta Total"
   .Columns(3).Width = 75
   .Columns(3).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(4).HeaderText = "% Vtas"
   .Columns(4).Width = 40
   .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(4).DefaultCellStyle.Format = "###.00"

   .Columns(5).HeaderText = "Piezas Dev."
   .Columns(5).Width = 40
   .Columns(5).DefaultCellStyle.Format = "###,###,###"
   .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(6).HeaderText = "$ Monto Devuelto"
   .Columns(6).Width = 60
   .Columns(6).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(7).HeaderText = "% Dvol. S/Vta"
   .Columns(7).Width = 50
   .Columns(7).DefaultCellStyle.Format = "###.00"
   .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(8).HeaderText = "Cant. Neta"
   .Columns(8).Width = 50
   .Columns(8).DefaultCellStyle.Format = "###,###,###"
   .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(9).HeaderText = "$ Monto Neto"
   .Columns(9).Width = 75
   .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(10).Visible = False
   .Columns(11).Visible = False
   .Columns(12).Visible = False
   .Columns(13).Visible = False
   .Columns(14).Visible = False
   .Columns(15).Visible = False
   .Columns(16).Visible = False

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
  oSheet.range("G3").value = " Ventas Netas"
  oSheet.range("H3").value = "Descuentos Pronto Pago"
  oSheet.range("I3").value = " % PP Sobre Venta"
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

 Private Sub GrdConLinea_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdConLinea.SelectionChanged
  Try
   dv.RowFilter = "LineaAgte ='" + GrdConLinea.Item(12, GrdConLinea.CurrentRow.Index).Value.ToString + "'"
  Catch ex As Exception
   ' MessageBox.Show("AL BUSCAR DATOS" + Convert.ToString(ex), " E R R O R ! ! !", MessageBoxButtons.OK, MessageBoxIcon.Error)

  End Try
 End Sub

 Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

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

 Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
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
  Dim oExcel As Object
  Dim oBook As Object
  Dim oSheet As Object

  'Abrimos un nuevo libro
  oExcel = CreateObject("Excel.Application")
  oBook = oExcel.workbooks.add
  oSheet = oBook.worksheets(1)

  'Declaramos el nombre de las columnas
  oSheet.range("A3").value = "}Nombre Agente"
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
  oSheet.range("C" & valreg.ToString).value = "Cantidad Piezas"
  oSheet.range("D" & valreg.ToString).value = "$ Venta Total"
  oSheet.range("E" & valreg.ToString).value = "% Sobre Vta."
  oSheet.range("F" & valreg.ToString).value = "Cant.Piezas Dev."
  oSheet.range("G" & valreg.ToString).value = "$ Monto Devuelto"
  oSheet.range("H" & valreg.ToString).value = "% Dev. Sobre Vta."
  oSheet.range("I" & valreg.ToString).value = "Cant. Piezas Netas"
  oSheet.range("J" & valreg.ToString).value = "$ Total Ventas Netas"
  oSheet.range("K" & valreg.ToString).value = "Linea"


  'para poner la primera fila de los titulos en negrita
  oSheet.range("A" & valreg.ToString & ":" & "K" & valreg.ToString).font.bold = True
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
  Next


  oSheet.Columns("A:A").NumberFormat = "@"
  oSheet.Columns("J:J").ColumnWidth = 18


  oExcel.visible = True
  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  GC.Collect()
  oSheet = Nothing
  oBook = Nothing
  oExcel = Nothing
 End Sub
 Private Sub GrdConProd_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdConProd.SelectionChanged
  Try
   dvTodosArt.RowFilter = "IdAgente =" & GrdConProd.Item(0, GrdConProd.CurrentRow.Index).Value.ToString
  Catch ex As Exception
  End Try
 End Sub

End Class