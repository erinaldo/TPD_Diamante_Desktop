Public Class Cobranza
 Dim DvLP As New DataView
 Dim objDataSet As New DataTable
 Dim Rangos As String = ""
 Dim Rangos2 As String = ""
 'Dim band1 As Integer = 0


 Private Sub ConsultaProd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  Me.DtpPVtaIni.Value = Format(Date.Now, "dd/MM/yyyy")
  Me.DtpPVtaTer.Value = Format(Date.Now, "dd/MM/yyyy")

  Me.DtpPPagoIni.Value = Format(Date.Now, "dd/MM/yyyy")
  Me.DtpPPagoTer.Value = Format(Date.Now, "dd/MM/yyyy")

  Dim ConsutaLista As String

  'UsrTPM = "COBRANZ1" Or
  'MODIFICADO POR IVAN GONZALEZ SE QUITO COBRANZA2
  If UsrTPM = "COBRANZ4" Or UsrTPM = "COBRANZ5" Then

   ConsutaLista = "SELECT IdCobranza,Nombre FROM DEPCOB WHERE IdCobranza ='" + UsrTPM + "' " + "ORDER BY Nombre"
   CmbACob.Enabled = False

  ElseIf UsrTPM = "NGOMEZ" Then

   ConsutaLista = "SELECT IdCobranza,Nombre FROM DEPCOB WHERE IdCobranza ='cobranz6' ORDER BY Nombre"
   CmbACob.Enabled = False
  Else
   ConsutaLista = "SELECT IdCobranza,Nombre FROM DEPCOB ORDER BY Nombre"
  End If


  ''ConsutaLista = "SELECT IdCobranza,Nombre FROM DEPCOB ORDER BY Nombre"
  Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, StrTpm)

  Dim dsArt As New DataSet
  daArticulo.Fill(dsArt)

  Dim fila As Data.DataRow

  If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Or UsrTPM = "COBRANZ3" Or UsrTPM = "COBRANZ2" Then
   'Asignamos a fila la nueva Row(Fila)del Dataset
   fila = dsArt.Tables(0).NewRow

   'Agregamos los valores a los campos de la tabla
   fila("IdCobranza") = "TODOS"
   fila("Nombre") = "TODOS"

   'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
   dsArt.Tables(0).Rows.Add(fila)
  End If


  Me.CmbACob.DataSource = dsArt.Tables(0)
  Me.CmbACob.DisplayMember = "Nombre"
  Me.CmbACob.ValueMember = "IdCobranza"

  'MODIFICO IVAN GONZALEZ QUITO COBRANZ2
  If UsrTPM = "COBRANZ4" Or UsrTPM = "COBRANZ5" Then
   Me.CmbACob.SelectedValue = UsrTPM
  ElseIf UsrTPM = "NGOMEZ" Then
   Me.CmbACob.SelectedValue = "cobranz6"
  ElseIf UsrTPM = "COBRANZ1" Then
   Me.CmbACob.SelectedValue = 0
  Else
   Me.CmbACob.SelectedValue = "TODOS"
  End If

 End Sub

 Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

  'If UsrTPM <> "MANAGER" Then
  '    If DtpPVtaTer.Value.Month >= FchServer.Month And DtpPVtaTer.Value.Year >= FchServer.Year Then
  '        MessageBox.Show("No es posible consultar las ventas de un mes mayor o igual al actual", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
  '        Return
  '    End If
  'End If

  If Me.DtpPVtaIni.Value > Me.DtpPVtaTer.Value Then
   MessageBox.Show("El periodo de venta de inicio no pede ser mayor al de termino", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
   DtpPVtaIni.Focus()
   Return
  End If

  'If Me.DtpDesIni.Value > Me.DtpDesTer.Value Then
  '  MessageBox.Show("El periodo de descuento de inicio no pede ser mayor al de termino", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
  '  DtpDesIni.Focus()
  '  Return
  'End If

  If Me.DtpPPagoIni.Value > Me.DtpPPagoTer.Value Then
   MessageBox.Show("El periodo de pago de inicio no pede ser mayor al de termino", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
   DtpPPagoIni.Focus()
   Return
  End If

  If Me.CmbACob.SelectedValue = "" Or IsDBNull(Me.CmbACob.SelectedValue) Then
   MessageBox.Show("Sleccione un agente de cobranza", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
   Me.CmbACob.Focus()
   Return
  End If

  cargar_registros()
 End Sub

 Sub cargar_registros()

  Dim Consulta As String = " "
  Dim strcadena As String = ""
  Dim CTabla As String = ""
  Dim DTMObra As New DataTable
  Dim DTProb As New DataTable
  'OBTIENE LAS NC DE LAS FECHAS QUE SE HAYA INDICADO
  'TMP1
  If (DtpPVtaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
   If (DtpPVtaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
    'todo normal'
    'band = 0'
    Consulta &= "select t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
    Consulta &= "into #tmp1 "
    Consulta &= "from ORIN t5 where t5.DocDate>= @PVtasIni AND T5.DocDate <= @PVtasTer  "
    Consulta &= "AND (T5.EDocPrefix = 'NC' or T5.EDocPrefix is null or T5.EDocPrefix = 'F') AND T5.DocType  = 'I'  " + vbLf
   ElseIf (DtpPVtaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    'band = 1'
    Consulta &= "select t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
    Consulta &= "into #tmp1 "
    'Consulta &= "from ORIN t5 where t5.DocDate>= @PVtasIni AND T5.DocDate <= '2017-12-31'  "
    Consulta &= "from ORIN t5 where t5.DocDate>= @PVtasIni AND T5.DocDate <= @PVtasTer  "
    Consulta &= "AND (T5.EDocPrefix = 'NC' or T5.EDocPrefix is null or T5.EDocPrefix = 'F') AND T5.DocType  = 'I'  "
    Consulta &= "union all "
    Consulta &= "select distinct t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
    Consulta &= "from ORIN t5 inner join RIN1 t6 on t5.DocEntry = t6.DocEntry "
    'Consulta &= "where t5.DocDate>= '2018-01-01' AND T5.DocDate <= @PVtasTer "
    Consulta &= "where t5.DocDate>= @PVtasIni AND T5.DocDate <= @PVtasTer "
    Consulta &= "AND (T5.EDocPrefix = 'NC' or T5.EDocPrefix is null or T5.EDocPrefix = 'F')  "
    Consulta &= "and t6.ItemCode <> 'DESCUENTO P.P'  "
    ''Consulta &= "drop table "
   End If
  ElseIf (DtpPVtaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
   If (DtpPVtaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then
    'band = 2'
   ElseIf (DtpPVtaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
    Consulta &= "select distinct t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
    Consulta &= "into #tmp1 "
    Consulta &= "from ORIN t5 inner join RIN1 t6 on t5.DocEntry = t6.DocEntry "
    Consulta &= "where t5.DocDate>= @PVtasIni AND T5.DocDate <= @PVtasTer "
    Consulta &= "AND (T5.EDocPrefix = 'NC' or T5.EDocPrefix is null or T5.EDocPrefix = 'F')  "
    Consulta &= "and t6.ItemCode <> 'DESCUENTO P.P'  " + vbLf
   End If
  End If 'FIN OBTIENE LAS NC DE LAS FECHAS QUE SE HAYA INDICADO

  ''TMP2
  'If (DtpPDevIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
  '  If (DtpPDevTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
  '    Consulta &= "select distinct t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
  '    Consulta &= "into #tmp2 "
  '    Consulta &= "from ORIN t5  "
  '    Consulta &= "WHERE T5.DocDate >= @DevIni AND T5.DocDate <= @DevTer AND DocType  = 'I' AND   "
  '    Consulta &= "CASE when t5.DocDate <= '2017-11-19' then EDocNum else "
  '    Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t5.DocEntry and t1.SrcObjType = 14) end "
  '    Consulta &= "IS NOT NULL " + vbLf
  '  ElseIf (DtpPDevTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
  '    Consulta &= "select distinct t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
  '    Consulta &= "into #tmp2 "
  '    Consulta &= "from ORIN t5  "
  '    'Consulta &= "WHERE T5.DocDate >= @DevIni AND T5.DocDate <= '2017-12-31' AND DocType  = 'I' AND   "
  '    Consulta &= "WHERE T5.DocDate >= @DevIni AND T5.DocDate <= @DevTer AND DocType  = 'I' AND   "
  '    Consulta &= "CASE when t5.DocDate <= '2017-11-19' then EDocNum else "
  '    Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t5.DocEntry and t1.SrcObjType = 14) end "
  '    Consulta &= "IS NOT NULL "
  '    Consulta &= "union all "
  '    Consulta &= "select distinct t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
  '    Consulta &= "from ORIN t5 inner join RIN1 t6 on t5.DocEntry = t6.DocEntry "
  '    'Consulta &= "WHERE T5.DocDate >= '2018-01-01' AND T5.DocDate <= @DevTer  "
  '    Consulta &= "WHERE T5.DocDate >= @DevIni AND T5.DocDate <= @DevTer  "
  '    Consulta &= "and t6.ItemCode <> 'DESCUENTO P.P' and "
  '    Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t5.DocEntry and t1.SrcObjType = 14) IS NOT NULL " + vbLf

  '  End If
  'ElseIf (DtpPDevIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
  '  If (DtpPDevTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then
  '  ElseIf (DtpPDevTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
  '    Consulta &= "select distinct t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
  '    Consulta &= "into #tmp2 "
  '    Consulta &= "from ORIN t5 inner join RIN1 t6 on t5.DocEntry = t6.DocEntry "
  '    Consulta &= "WHERE T5.DocDate >= @DevIni AND T5.DocDate <= @DevTer  "
  '    Consulta &= "and t6.ItemCode <> 'DESCUENTO P.P' and "
  '    Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t5.DocEntry and t1.SrcObjType = 14) IS NOT NULL " + vbLf
  '  End If
  'End If

  'TPM3
  'If (DtpDesIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
  '  If (DtpDesTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
  '    Consulta &= "select distinct t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
  '    Consulta &= "into #tmp3 "
  '    Consulta &= "from ORIN t5  "
  '    Consulta &= "where t5.DocDate >= @DesIni AND t5.DocDate <= @DesTer "
  '    Consulta &= "and (T5.EDocPrefix = 'NC' or T5.EDocPrefix is null or T5.EDocPrefix = 'F') AND DocType  = 'S' " + vbLf

  '  ElseIf (DtpDesTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
  '    Consulta &= "select distinct t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
  '    Consulta &= "into #tmp3 "
  '    Consulta &= "from ORIN t5  "
  '    'Consulta &= "where t5.DocDate >= @DesIni AND t5.DocDate <= '2017-12-31' "
  '    Consulta &= "where t5.DocDate >= @DesIni AND t5.DocDate <= @DesTer "
  '    Consulta &= "and (T5.EDocPrefix = 'NC' or T5.EDocPrefix is null or T5.EDocPrefix = 'F') AND DocType  = 'S' "
  '    Consulta &= "union all "
  '    Consulta &= "select distinct t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
  '    Consulta &= "from ORIN t5 inner join RIN1 t6 on t5.DocEntry = t6.DocEntry "
  '    'Consulta &= "where t5.DocDate >= '2018-01-01' AND t5.DocDate <= @DesTer "
  '    Consulta &= "where t5.DocDate >= @DesIni AND t5.DocDate <= @DesTer "
  '    Consulta &= "and (T5.EDocPrefix = 'NC' or T5.EDocPrefix is null or T5.EDocPrefix = 'F')   "
  '    Consulta &= "and t6.ItemCode = 'DESCUENTO P.P'  " + vbLf

  '  End If
  'ElseIf (DtpDesIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
  '  If (DtpDesTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then
  '  ElseIf (DtpDesTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
  '    Consulta &= "select distinct t5.DocNum, t5.SlpCode, t5.DocTotal, t5.VatSum, t5.TotalExpns "
  '    Consulta &= "into #tmp3 "
  '    Consulta &= "from ORIN t5 inner join RIN1 t6 on t5.DocEntry = t6.DocEntry "
  '    Consulta &= "where t5.DocDate >= @DesIni AND t5.DocDate <= @DesTer "
  '    Consulta &= "and (T5.EDocPrefix = 'NC' or T5.EDocPrefix is null or T5.EDocPrefix = 'F')   "
  '    Consulta &= "and t6.ItemCode = 'DESCUENTO P.P'  " + vbLf
  '  End If
  'End If

  'Se insertan las siguientes 2 lineas
  Consulta &= "If OBJECT_ID(N'tempdb.dbo.#Res', N'U') is not null "
  Consulta &= "drop table #Res "
  '

  Consulta &= "SELECT T0.SlpCode,T0.SlpName, "
  Consulta &= "SUM((T1.DocTotal - T1.VatSum) - T1.TotalExpns) -  "
  Consulta &= "CASE WHEN  "
  Consulta &= "(SELECT SUM((T5.DocTotal - T5.VatSum) - T5.TotalExpns)  "
  Consulta &= "FROM #tmp1 T5 WHERE T5.SlpCode = T0.SlpCode  "
  Consulta &= "GROUP BY T5.SlpCode) IS NULL THEN 0 ELSE  "
  Consulta &= "(SELECT SUM((T5.DocTotal - T5.VatSum) - T5.TotalExpns)  "
  Consulta &= "FROM #tmp1 T5 WHERE T5.SlpCode = T0.SlpCode  "
  Consulta &= "GROUP BY T5.SlpCode) END AS VtasTotales,CAST(0 AS decimal(5,2)) AS PorVtas, " + vbLf



  'Consulta &= "(SELECT SUM((DocTotal - VatSum) - TotalExpns) FROM #tmp2 T5 "
  'Consulta &= "WHERE T0.SlpCode = T5.SlpCode "   'ORIG: 
  'Consulta &= "GROUP BY T5.SlpCode) AS Devoluciones,CAST(0 AS decimal(5,2)) AS PorDev," + vbLf
  Consulta &= "0 Devoluciones,0 AS PorDev," + vbLf


  Consulta &= "0 AS Descuentos,CAST(0 AS decimal(5,2)) AS PorDesc,"
  '==========================================================================================================MODIFICACION PAGOS 14/09/2018

  Consulta &= "((SELECT SUM(T7.SumApplied) "
  'Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "--------------------
  Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocEntry = T7.DocNum "
  Consulta &= "INNER JOIN OINV T8 ON T8.DocEntry = T7.DocEntry AND T7.InvType = 13 "
  Consulta &= "WHERE T6.Canceled = 'N' AND T6.DocDate >=@PagoIni AND T6.DocDate <=@PagoTer AND T8.SlpCode = T0.SlpCode AND T6.DocTotal <> .01 "
  Consulta &= "AND T8.Series <> 88 AND T6.DocNum NOT IN (SELECT Pago FROM TPM.dbo.[00ExcepPagos]) "
  Consulta &= "GROUP BY T8.SlpCode) - "
  Consulta &= "CASE WHEN (SELECT  SUM(T7.SumApplied) "
  'Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "-----
  Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocEntry = T7.DocNum "
  Consulta &= "INNER JOIN ORIN T8 ON  T8.DocNum = T7.DocEntry  "
  Consulta &= "WHERE T6.Canceled = 'N' AND T6.DocDate >=@PagoIni AND T6.DocDate <=@PagoTer AND T7.InvType = 13 AND T6.DocTotal <> .01 AND T8.SlpCode = T0.SlpCode "
  Consulta &= "AND T6.DocNum NOT IN (SELECT Pago FROM TPM.dbo.[00ExcepPagos]) GROUP BY T8.SlpCode) IS NULL THEN 0 ELSE "
  Consulta &= "(SELECT  SUM(T7.SumApplied) "
  'Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "------
  Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocEntry = T7.DocNum "
  Consulta &= "INNER JOIN ORIN T8 ON  T8.DocNum = T7.DocEntry "
  Consulta &= "WHERE T6.Canceled = 'N' AND T6.DocDate >=@PagoIni AND T6.DocDate <=@PagoTer AND T7.InvType = 13 AND T6.DocTotal <> .01 AND T8.SlpCode = T0.SlpCode "
  Consulta &= "AND T6.DocNum NOT IN (SELECT Pago FROM TPM.dbo.[00ExcepPagos]) GROUP BY T8.SlpCode) "
  Consulta &= "END) / 1.16  AS Pago,CAST(0 AS decimal(5,2)) AS PorPago, "
  '==========================================================================================================MODIFICACION PAGOS 14/09/2018



  Consulta &= "((SELECT  SUM(CASE WHEN MONTH(T8.DocDate) = MONTH(@PVtasIni) THEN T7.SumApplied ELSE 0 END) "
  'Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "-------
  Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocEntry = T7.DocNum "
  Consulta &= "INNER JOIN OINV T8 ON T8.DocEntry = T7.DocEntry AND T7.InvType = 13 "
  Consulta &= "WHERE T6.Canceled = 'N' AND T6.DocDate >=@PagoIni AND T6.DocDate <=@PagoTer AND T8.SlpCode = T0.SlpCode AND T6.DocTotal <> .01 "
  Consulta &= "AND T8.Series <> 88 AND T6.DocNum NOT IN (SELECT Pago FROM TPM.dbo.[00ExcepPagos])"
  Consulta &= "GROUP BY T8.SlpCode) - "
  Consulta &= "CASE WHEN (SELECT  SUM(CASE WHEN MONTH(T8.DocDate) = MONTH(@PVtasIni) THEN T7.SumApplied ELSE 0 END) "
  'Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "-------
  Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocEntry = T7.DocNum "
  Consulta &= "INNER JOIN ORIN T8 ON T8.DocNum = T7.DocEntry "
  Consulta &= "WHERE T6.Canceled = 'N' AND T6.DocDate >=@PagoIni AND T6.DocDate <=@PagoTer AND T7.InvType = 14 AND T6.DocTotal <> .01 AND T8.SlpCode = T0.SlpCode "
  Consulta &= "AND T6.DocNum NOT IN (SELECT Pago FROM TPM.dbo.[00ExcepPagos]) GROUP BY T8.SlpCode) IS NULL THEN 0 ELSE "
  Consulta &= "(SELECT  SUM(CASE WHEN MONTH(T8.DocDate) = MONTH(@PVtasIni) THEN T7.SumApplied ELSE 0 END) "
  'Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocNum = T7.DocNum " ---
  Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocEntry = T7.DocNum "
  Consulta &= "INNER JOIN ORIN T8 ON T8.DocNum = T7.DocEntry "
  Consulta &= "WHERE T6.Canceled = 'N' AND T6.DocDate >=@PagoIni AND T6.DocDate <=@PagoTer AND T7.InvType = 14 AND T6.DocTotal <> .01 AND T8.SlpCode = T0.SlpCode "
  Consulta &= "AND T6.DocNum NOT IN (SELECT Pago FROM TPM.dbo.[00ExcepPagos]) GROUP BY T8.SlpCode) END) / 1.16 AS PagoMesFac,"

  Consulta &= "((SELECT  SUM(CASE WHEN MONTH(T8.DocDate) <> MONTH(@PVtasIni) THEN T7.SumApplied ELSE 0 END) "
  'Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "-------
  Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocEntry = T7.DocNum "
  Consulta &= "INNER JOIN OINV T8 ON T8.DocEntry = T7.DocEntry AND T7.InvType = 13 "
  Consulta &= "WHERE T6.Canceled = 'N' AND T6.DocDate >=@PagoIni AND T6.DocDate <=@PagoTer AND T8.SlpCode = T0.SlpCode AND T6.DocTotal <> .01 "
  Consulta &= "AND T8.Series <> 88 AND T6.DocNum NOT IN (SELECT Pago FROM TPM.dbo.[00ExcepPagos]) "
  Consulta &= "GROUP BY T8.SlpCode) - "
  Consulta &= "CASE WHEN (SELECT  SUM(CASE WHEN MONTH(T8.DocDate) <> MONTH(@PVtasIni) THEN T7.SumApplied ELSE 0 END) "
  'Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "
  Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocEntry = T7.DocNum "
  Consulta &= "INNER JOIN ORIN T8 ON  T8.DocNum = T7.DocEntry "
  Consulta &= "WHERE T6.Canceled = 'N' AND T6.DocDate >=@PagoIni AND T6.DocDate <=@PagoTer AND T7.InvType = 14 AND T6.DocTotal <> .01 AND T8.SlpCode = T0.SlpCode "
  Consulta &= "AND T6.DocNum NOT IN (SELECT Pago FROM TPM.dbo.[00ExcepPagos]) GROUP BY T8.SlpCode) IS NULL THEN 0 ELSE "
  Consulta &= "(SELECT  SUM(CASE WHEN MONTH(T8.DocDate) <> MONTH(@PVtasIni) THEN T7.SumApplied ELSE 0 END) "
  'Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocNum = T7.DocNum "-----
  Consulta &= "FROM ORCT T6 LEFT JOIN RCT2 T7 ON T6.DocEntry = T7.DocNum "
  Consulta &= "INNER JOIN ORIN T8 ON  T8.DocNum = T7.DocEntry "
  Consulta &= "WHERE T6.Canceled = 'N' AND T6.DocDate >=@PagoIni AND T6.DocDate <=@PagoTer AND T7.InvType = 14 AND T6.DocTotal <> .01 AND T8.SlpCode = T0.SlpCode "
  Consulta &= "AND T6.DocNum NOT IN (SELECT Pago FROM TPM.dbo.[00ExcepPagos]) GROUP BY T8.SlpCode) END) / 1.16 AS PagosOtroMes "

  'Se inserta la linea de abajo
  Consulta &= "INTO #Res "
  Consulta &= "FROM OSLP T0 LEFT JOIN OINV T1 ON T0.SlpCode = T1.SlpCode "
  Consulta &= "AND T1.Series <> 88 "

  Consulta &= "WHERE T1.DocDate >= @PVtasIni AND T1.DocDate <= @PVtasTer "
  Consulta &= " AND T1.DocNum NOT IN (3031209, 2001567) " ' Esto es una  excepcion que se tuvo que ingresar por un mal movimiento
  Consulta &= " GROUP BY T0.SlpCode,T0.SlpName ORDER BY VtasTotales DESC "


  'El siguiente codigo se usara para casos especiales donde se requiere colocar una linea especial para que le cuadre el reporte a cobranza
  Consulta &= "IF (SELECT COUNT(*) FROM TPM.dbo.CobranzaRecuperadaEspecial) > 0 "
  Consulta &= "INSERT INTO #Res "
  Consulta &= "Select t0.* From TPM.dbo.CobranzaRecuperadaEspecial t0 "
  Consulta &= "Left Join #Res t1 ON t0.SlpCode = t1.SlpCode "
  Consulta &= " And t0.SlpName COLLATE Modern_Spanish_CI_AS = t1.SlpName "
  Consulta &= "WHERE t1.SlpCode Is NULL "


  Consulta &= "drop table #tmp1 "

  Consulta &= "SELECT * FROM #Res "
  'Consulta &= "drop table #tmp2 "

  'PVtas
  ''** VENTAS TOTALES POR VENDEDOR
  ''** LO FACTURADO

  Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

  CmdMObra.Parameters.Add("@PVtasIni", SqlDbType.SmallDateTime)
  CmdMObra.Parameters("@PVtasIni").Value = Me.DtpPVtaIni.Value
  CmdMObra.Parameters.Add("@PVtasTer", SqlDbType.SmallDateTime)
  CmdMObra.Parameters("@PVtasTer").Value = Me.DtpPVtaTer.Value

  'CmdMObra.Parameters.Add("@DesIni", SqlDbType.SmallDateTime)
  'CmdMObra.Parameters("@DesIni").Value = Me.DtpDesIni.Value
  'CmdMObra.Parameters.Add("@DesTer", SqlDbType.SmallDateTime)
  'CmdMObra.Parameters("@DesTer").Value = Me.DtpDesTer.Value

  'CmdMObra.Parameters.Add("@DevIni", SqlDbType.SmallDateTime)
  'CmdMObra.Parameters("@DevIni").Value = Me.DtpPDevIni.Value
  'CmdMObra.Parameters.Add("@DevTer", SqlDbType.SmallDateTime)
  'CmdMObra.Parameters("@DevTer").Value = Me.DtpPDevTer.Values

  CmdMObra.Parameters.Add("@PagoIni", SqlDbType.SmallDateTime)
  CmdMObra.Parameters("@PagoIni").Value = Me.DtpPPagoIni.Value
  CmdMObra.Parameters.Add("@PagoTer", SqlDbType.SmallDateTime)
  CmdMObra.Parameters("@PagoTer").Value = Me.DtpPPagoTer.Value


  CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
  CmdMObra.Connection.Open()

  Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
  AdapMObra.Fill(DTMObra)
  CmdMObra.Connection.Close()

  '******************************************************************************
  Dim vtotales As Decimal = 0
  Dim Dev As Decimal = 0
  Dim Descuentos As Decimal = 0
  Dim Pagos As Decimal = 0

  For Each fila As DataRow In DTMObra.Rows
   If Not IsDBNull(fila("VtasTotales")) Then
    vtotales = vtotales + fila("VtasTotales")
   End If

   If Not IsDBNull(fila("Devoluciones")) Then
    Dev = Dev + fila("Devoluciones")
   End If

   If Not IsDBNull(fila("Descuentos")) Then
    Descuentos = Descuentos + fila("Descuentos")
   End If

   If Not IsDBNull(fila("Pago")) Then
    Pagos = Pagos + fila("Pago")
   End If

  Next

  '******************************************************************************

  ''Se crea cursor para reporte
  'CTabla = "CREATE TABLE #REPCOB (IdVend INT,Nombre Varchar(100),VtaTotal Numeric(10,2),PorVtas Numeric(8,2),"
  'CTabla &= "Dev Numeric(10,2),PorDev Numeric(8,2),Des Numeric(10,2),PorDes Numeric(8,2),"
  'CTabla &= "Pagos Numeric(10,2),PorPago Numeric(8,2),PagoMesFac Numeric(10,2),PorMesFac Numeric(8,2),"
  'CTabla &= "PagosOtroMes Numeric(10,2),PorOtroMes Numeric(8,2),ClaveCob Varchar(10),NomCob Varchar(40),orden Numeric(1,0))"

  CTabla = "CREATE TABLE #REPCOB (IdVend INT,Nombre Varchar(100),VtaTotal Numeric(10,2),"
  CTabla &= "Dev Numeric(10,2),Des Numeric(10,2),"
  CTabla &= "Pagos Numeric(10,2),PorPago Numeric(8,2),PagoMesFac Numeric(10,2),PorMesFac Numeric(8,2),"
  CTabla &= "PagosOtroMes Numeric(10,2),PorOtroMes Numeric(8,2),ClaveCob Varchar(10),NomCob Varchar(40),orden Numeric(1,0),Com Numeric(19,2),PorCom Numeric(10,2))"



  'PagoMesFac()PagosOtroMes
  Dim cmdcob As Data.SqlClient.SqlCommand
  cmdcob = New Data.SqlClient.SqlCommand()

  With cmdcob
   .Connection = New Data.SqlClient.SqlConnection(StrTpm)
   .Connection.Open()
   .CommandText = CTabla
   .ExecuteNonQuery()
  End With


  Dim vdev As Decimal
  Dim vdesc As Decimal
  Dim vpago As Decimal
  Dim vpagom As Decimal
  Dim votrom As Decimal

  For Each fila As DataRow In DTMObra.Rows

   strcadena = "INSERT INTO #REPCOB (IdVend, Nombre,VtaTotal,Dev,Des,Pagos,PorPago,"
   strcadena &= "PagoMesFac,PorMesFac,PagosOtroMes,PorOtroMes,ClaveCob,orden,NomCob,Com,PorCom) VALUES ("
   ''IdVend
   strcadena &= fila("SlpCode")
   strcadena &= ",'"
   ''Nombre
   strcadena &= fila("SlpName")
   strcadena &= "',"
   ''VtaTotal
   strcadena &= IIf(IsDBNull(fila("VtasTotales")), 0, fila("VtasTotales"))
   strcadena &= ","
   ''Dev
   vdev = IIf(IsDBNull(fila("Devoluciones")), 0, fila("Devoluciones"))
   strcadena &= vdev
   strcadena &= ","
   ''Des
   vdesc = IIf(IsDBNull(fila("Descuentos")), 0, fila("Descuentos"))
   strcadena &= vdesc
   strcadena &= ","
   ''Pagos
   vpago = If(IsDBNull(fila("Pago")), 0, fila("Pago"))
   strcadena &= vpago
   strcadena &= ","
   ''% Pago
   If vpago = 0 Or fila("VtasTotales") = 0 Then
    vpago = 0
   Else
    vpago = fila("Pago") * 100 / IIf(IsDBNull(fila("VtasTotales")), 0, fila("VtasTotales"))
   End If
   strcadena &= vpago
   strcadena &= ","
   ''PagoMesFac
   vpagom = IIf(IsDBNull(fila("PagoMesFac")), 0, fila("PagoMesFac"))
   strcadena &= vpagom
   strcadena &= ","

   ''PorMesFac
   If vpagom = 0 Or fila("VtasTotales") = 0 Then
    vpagom = 0
   Else
    vpagom = fila("PagoMesFac") * 100 / IIf(IsDBNull(fila("Pago")), 0, fila("Pago"))
   End If
   strcadena &= vpagom
   strcadena &= ","

   ''PagosOtroMes
   votrom = IIf(IsDBNull(fila("PagosOtroMes")), 0, fila("PagosOtroMes"))
   strcadena &= Math.Round(votrom, 2)
   strcadena &= ","
   ''PorOtroMes
   If votrom = 0 Or fila("VtasTotales") = 0 Then
    votrom = 0
   Else
    votrom = fila("PagosOtroMes") * 100 / IIf(IsDBNull(fila("Pago")), 0, fila("Pago"))
   End If
   strcadena &= votrom
   strcadena &= ",'"
   ''ClaveCob
   strcadena &= "SinId"
   strcadena &= "',"
   ''orden
   strcadena &= "0"
   strcadena &= ",'"
   ''NomCob
   strcadena &= "SinNombre"
   strcadena &= "'"
   strcadena &= ","
   'Com,PorCom
   strcadena &= "NULL,NULL"
   strcadena &= ")"

   With cmdcob

    .CommandText = strcadena
    .ExecuteNonQuery()

   End With
  Next

  '/**************************************************************************************************************

  strcadena = "SELECT SUM(#REPCOB.VtaTotal) AS VtaTotal,SUM(#REPCOB.Dev) AS Dev,"
  strcadena &= "SUM(#REPCOB.Des) AS Des,SUM(#REPCOB.Pagos) AS Pagos, SUM(#REPCOB.PorPago) AS PorPago,"

  strcadena &= "SUM(#REPCOB.PagoMesFac) AS PagoMesFac,SUM(#REPCOB.PorMesFac) AS PorMesFac,"
  strcadena &= "SUM(#REPCOB.PagosOtroMes) AS PagosOtroMes,SUM(#REPCOB.PorOtroMes) AS PorOtroMes "
  strcadena &= "FROM #REPCOB "

  With cmdcob
   .CommandText = strcadena
  End With

  Dim DAdapTot As New SqlClient.SqlDataAdapter(cmdcob)

  Dim DtUsrTot As New DataTable
  DAdapTot.Fill(DtUsrTot)

  For Each fila As DataRow In DtUsrTot.Rows

   'REVISAR TODO ESTE CODIGOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO VALORES NULL

   strcadena = "INSERT INTO #REPCOB (IdVend, Nombre,VtaTotal,Dev,"
   strcadena &= "Des,Pagos,PorPago,PagoMesFac,PorMesFac,PagosOtroMes,PorOtroMes,ClaveCob,NomCob,orden,Com,PorCom) VALUES ("

   strcadena &= "0"
   strcadena &= ",'"
   strcadena &= "--- Totales del Periodo ---"
   strcadena &= "',"
   strcadena &= If(IsDBNull(fila("VtaTotal")), 0, fila("VtaTotal"))
   strcadena &= ","
   strcadena &= If(IsDBNull(fila("Dev")), 0, fila("Dev"))
   strcadena &= ","

   strcadena &= If(IsDBNull(fila("Des")), 0, fila("Des"))
   strcadena &= ","

   strcadena &= If(IsDBNull(fila("Pagos")), 0, fila("Pagos"))
   strcadena &= ","
   strcadena &= If(IsDBNull(fila("VtaTotal")), 0, fila("Pagos") * 100 / fila("VtaTotal"))
   strcadena &= ","
   strcadena &= If(IsDBNull(fila("PagoMesFac")), 0, fila("PagoMesFac"))
   strcadena &= ","
   strcadena &= If(IsDBNull(fila("VtaTotal")), 0, fila("PagoMesFac") * 100 / fila("VtaTotal"))
   strcadena &= ","
   strcadena &= If(IsDBNull(fila("PagosOtroMes")), 0, fila("PagosOtroMes"))
   strcadena &= ","
   strcadena &= If(IsDBNull(fila("VtaTotal")), 0, fila("PagosOtroMes") * 100 / fila("VtaTotal"))
   strcadena &= ",'"
   strcadena &= "zTotal"
   strcadena &= "','"
   strcadena &= ""
   strcadena &= "',"
   strcadena &= "2"
   strcadena &= ","
   strcadena &= "NULL,NULL"
   strcadena &= ")"


   With cmdcob

    .CommandText = strcadena
    .ExecuteNonQuery()

   End With
  Next


  '/ SIN CLIENTES PROPIOS *******************************************************************************
  strcadena = "SELECT SUM(#REPCOB.VtaTotal) AS VtaTotal,SUM(#REPCOB.Dev) AS Dev,"
  strcadena &= "SUM(#REPCOB.Des) AS Des,SUM(#REPCOB.Pagos) AS Pagos,"
  strcadena &= "SUM(#REPCOB.PagoMesFac) AS PagoMesFac,SUM(#REPCOB.PagosOtroMes) AS PagosOtroMes"
  strcadena &= ",DEPCOB.IdCobranza,DEPCOB.Nombre AS NombreCob "
  strcadena &= "FROM #REPCOB LEFT JOIN DEPCOBR ON #REPCOB.IdVend = DEPCOBR.SlpCode and DEPCOBR.CbrGralAdicional='N' "
  strcadena &= "LEFT JOIN DEPCOB ON DEPCOBR.IdCobranza = DEPCOB.IdCobranza "
  strcadena &= "WHERE orden <> 2 AND #REPCOB.IdVend <> 1 GROUP BY DEPCOB.IdCobranza,DEPCOB.Nombre"


  With cmdcob
   .CommandText = strcadena
  End With

  Dim DAdapCob As New SqlClient.SqlDataAdapter(cmdcob)

  Dim DtUsrCob As New DataTable
  DAdapCob.Fill(DtUsrCob)
  Dim valdev As Decimal = 0
  Dim valDes As Decimal = 0
  Dim valPagos As Decimal = 0
  Dim valPagospor As Decimal = 0
  Dim valPagoMesFac As Decimal = 0
  Dim valPagosOtroMes As Decimal = 0
  Dim PComision As Decimal = 0
  Dim VComision As Decimal = 0


  For Each fila As DataRow In DtUsrCob.Rows

   strcadena = "INSERT INTO #REPCOB (IdVend, Nombre,VtaTotal,Dev,"
   strcadena &= "Des,Pagos,PorPago,PagoMesFac,PorMesFac,PagosOtroMes,PorOtroMes,ClaveCob,NomCob,orden,Com,PorCom) VALUES ("
   ''idvend
   strcadena &= 0
   strcadena &= ",'"
   ''nombre
   strcadena &= "*** Monto Total " + fila("NombreCob") + " ***"
   strcadena &= "',"
   ''vtatotal
   strcadena &= IIf(IsDBNull(fila("VtaTotal")), 0, fila("VtaTotal"))
   strcadena &= ","
   ''dev
   strcadena &= IIf(IsDBNull(fila("Dev")), 0, fila("Dev"))
   strcadena &= ","
   ''des
   strcadena &= IIf(IsDBNull(fila("Des")), 0, fila("Des"))
   strcadena &= ","
   ''Pagos
   valPagos = IIf(IsDBNull(fila("Pagos")), 0, fila("Pagos"))
   strcadena &= valPagos
   strcadena &= ","
   ''porpago
   valPagospor = 0
   If IsDBNull(fila("Pagos")) Or IsDBNull(fila("VtaTotal")) Then
    valPagospor = 0
   Else
    If fila("Pagos") <= 0 Or fila("VtaTotal") <= 0 Then
     valPagospor = 0
    Else
     valPagospor = fila("Pagos") * 100 / fila("VtaTotal")
    End If
   End If
   strcadena &= valPagospor.ToString
   strcadena &= ","
   ''PagoMesFac
   strcadena &= IIf(IsDBNull(fila("PagoMesFac")), 0, fila("PagoMesFac"))
   strcadena &= ","
   ''PorMesFac
   valPagoMesFac = 0
   If IsDBNull(fila("PagoMesFac")) Or valPagos = 0 Then
    valPagoMesFac = 0
   Else
    If fila("PagoMesFac") <= 0 Or valPagos <= 0 Then
     valPagoMesFac = 0
    Else
     valPagoMesFac = fila("PagoMesFac") * 100 / valPagos
    End If
   End If
   'strcadena &= IIf(IsDBNull(fila("PagoMesFac")), 0, fila("PagoMesFac")) * 100 / IIf(IsDBNull(fila("VtaTotal")), 0, fila("VtaTotal"))
   strcadena &= valPagoMesFac.ToString
   strcadena &= ","
   ''PagosOtroMes
   strcadena &= IIf(IsDBNull(fila("PagosOtroMes")), 0, fila("PagosOtroMes"))
   strcadena &= ","
   ''PorOtroMes
   valPagosOtroMes = 0
   If IsDBNull(fila("PagosOtroMes")) Or valPagos = 0 Then
    valPagosOtroMes = 0
   Else
    If fila("PagosOtroMes") <= 0 Or valPagos <= 0 Then
     valPagosOtroMes = 0
    Else
     valPagosOtroMes = fila("PagosOtroMes") * 100 / valPagos
    End If
   End If
   strcadena &= valPagosOtroMes.ToString
   'strcadena &= IIf(IsDBNull(fila("PagosOtroMes")), 0, fila("PagosOtroMes")) * 100 / IIf(IsDBNull(fila("VtaTotal")), 0, fila("VtaTotal"))
   strcadena &= ",'"
   ''ClaveCob
   strcadena &= fila("IdCobranza")
   strcadena &= "','"
   ''NombreCob
   strcadena &= fila("NombreCob")
   strcadena &= "',"

   ''------------------------

   strcadena &= "1"
   strcadena &= ","

   ''---------------------
   PComision = 0
   If IsDBNull(fila("Pagos")) Or IsDBNull(fila("VtaTotal")) Then
    PComision = 0
   Else

    If fila("Pagos") <= 0 Or fila("VtaTotal") <= 0 Then
     PComision = 0
    Else
     PComision = fila("Pagos") * 100 / fila("VtaTotal")
    End If
   End If

   Dim aux As Decimal

   If PComision > 80 And PComision <= 89.99 Then
    aux = 5
    strcadena &= aux

   ElseIf PComision >= 90 Then
    aux = 7
    strcadena &= 7
   Else
    aux = 0
    strcadena &= 0
   End If
   ''------------------------
   strcadena &= ","

   VComision = 0

   If IsDBNull(fila("Pagos")) Or IsDBNull(fila("VtaTotal")) Then
    VComision = 0
   Else

    If fila("Pagos") <= 0 Or fila("VtaTotal") <= 0 Then
     VComision = 0
    Else
     VComision = fila("Pagos") * 100 / fila("VtaTotal")
    End If
   End If

   If VComision > 80 And VComision <= 89.99 Then
    strcadena &= 0.05 * fila("Pagos")
   ElseIf VComision >= 90 Then
    strcadena &= 0.07 * fila("Pagos")
   Else
    aux = 0
    strcadena &= aux.ToString
   End If


   strcadena &= ")"

   With cmdcob

    .CommandText = strcadena
    .ExecuteNonQuery()

   End With
  Next
  'FIN SIN CLIENTES PROPIOS  *************************************************



  '/ CON CLIENTES PROPIOS ****************************************************

  strcadena = "SELECT SUM(#REPCOB.VtaTotal) AS VtaTotal,SUM(#REPCOB.Dev) AS Dev,"
  strcadena &= "SUM(#REPCOB.Des) AS Des,SUM(#REPCOB.Pagos) AS Pagos,"
  strcadena &= "SUM(#REPCOB.PagoMesFac) AS PagoMesFac,SUM(#REPCOB.PagosOtroMes) AS PagosOtroMes"
  strcadena &= ",DEPCOB.IdCobranza,DEPCOB.Nombre AS NombreCob "
  strcadena &= "FROM #REPCOB LEFT JOIN DEPCOBR ON #REPCOB.IdVend = DEPCOBR.SlpCode and DEPCOBR.CbrGralAdicional='N' "
  strcadena &= "LEFT JOIN DEPCOB ON DEPCOBR.IdCobranza = DEPCOB.IdCobranza "
  strcadena &= "WHERE orden <> 2 AND #REPCOB.IdVend = 1 GROUP BY DEPCOB.IdCobranza,DEPCOB.Nombre"


  With cmdcob
   .CommandText = strcadena
  End With

  Dim DAdapCob2 As New SqlClient.SqlDataAdapter(cmdcob)

  Dim DtUsrCob2 As New DataTable
  DAdapCob2.Fill(DtUsrCob2)
  Dim valdev2 As Decimal = 0
  Dim valDes2 As Decimal = 0
  Dim valPagos2 As Decimal = 0
  Dim valPagoMesFac2 As Decimal = 0
  Dim valPagosOtroMes2 As Decimal = 0
  Dim PComision2 As Decimal = 0
  Dim VComision2 As Decimal = 0


  For Each fila As DataRow In DtUsrCob2.Rows

   strcadena = "INSERT INTO #REPCOB (IdVend, Nombre,VtaTotal,Dev,"
   strcadena &= "Des,Pagos,PorPago,PagoMesFac,PorMesFac,PagosOtroMes,PorOtroMes,ClaveCob,NomCob,orden,Com,PorCom) VALUES ("
   strcadena &= 1
   strcadena &= ",'"
   strcadena &= "*** Monto Total CLIENTES PROPIOS ***"
   strcadena &= "',"
   strcadena &= IIf(IsDBNull(fila("VtaTotal")), 0, fila("VtaTotal"))
   strcadena &= ","

   strcadena &= IIf(IsDBNull(fila("Dev")), 0, fila("Dev"))
   strcadena &= ","

   strcadena &= IIf(IsDBNull(fila("Des")), 0, fila("Des"))
   strcadena &= ","

   strcadena &= IIf(IsDBNull(fila("Pagos")), 0, fila("Pagos"))
   strcadena &= ","


   valPagos2 = 0

   If IsDBNull(fila("Pagos")) Or IsDBNull(fila("VtaTotal")) Then
    valPagos2 = 0
   Else

    If fila("Pagos") <= 0 Or fila("VtaTotal") <= 0 Then
     valPagos2 = 0
    Else
     valPagos2 = fila("Pagos") * 100 / fila("VtaTotal")
    End If
   End If

   strcadena &= valPagos2.ToString

   strcadena &= ","
   strcadena &= IIf(IsDBNull(fila("PagoMesFac")), 0, fila("PagoMesFac"))
   strcadena &= ","
   valPagoMesFac = 0

   If IsDBNull(fila("PagoMesFac")) Or IsDBNull(fila("VtaTotal")) Then
    valPagoMesFac2 = 0
   Else

    If fila("PagoMesFac") <= 0 Or fila("VtaTotal") <= 0 Then
     valPagoMesFac2 = 0
    Else
     valPagoMesFac = fila("PagoMesFac") * 100 / fila("VtaTotal")
    End If
   End If

   'strcadena &= IIf(IsDBNull(fila("PagoMesFac")), 0, fila("PagoMesFac")) * 100 / IIf(IsDBNull(fila("VtaTotal")), 0, fila("VtaTotal"))
   strcadena &= valPagoMesFac2.ToString
   strcadena &= ","
   strcadena &= IIf(IsDBNull(fila("PagosOtroMes")), 0, fila("PagosOtroMes"))
   strcadena &= ","
   valPagosOtroMes = 0

   If IsDBNull(fila("PagosOtroMes")) Or IsDBNull(fila("VtaTotal")) Then
    valPagosOtroMes2 = 0
   Else

    If fila("PagosOtroMes") <= 0 Or fila("VtaTotal") <= 0 Then
     valPagosOtroMes2 = 0
    Else
     valPagosOtroMes2 = fila("PagosOtroMes") * 100 / fila("VtaTotal")
    End If
   End If

   strcadena &= valPagosOtroMes2.ToString

   'strcadena &= IIf(IsDBNull(fila("PagosOtroMes")), 0, fila("PagosOtroMes")) * 100 / IIf(IsDBNull(fila("VtaTotal")), 0, fila("VtaTotal"))
   strcadena &= ",'"
   strcadena &= fila("IdCobranza")
   strcadena &= "','"
   strcadena &= fila("NombreCob")
   strcadena &= "',"

   ''------------------------

   strcadena &= "1"
   strcadena &= ","

   ''---------------------
   PComision2 = 0
   If IsDBNull(fila("Pagos")) Or IsDBNull(fila("VtaTotal")) Then
    PComision2 = 0
   Else

    If fila("Pagos") <= 0 Or fila("VtaTotal") <= 0 Then
     PComision2 = 0
    Else
     PComision2 = fila("Pagos") * 100 / fila("VtaTotal")
    End If
   End If

   Dim aux As Decimal

   If PComision2 > 80 And PComision <= 89.99 Then
    aux = 5
    strcadena &= aux

   ElseIf PComision2 >= 90 Then
    aux = 7
    strcadena &= 7
   Else
    aux = 0
    strcadena &= 0
   End If
   ''------------------------
   strcadena &= ","

   VComision = 0

   If IsDBNull(fila("Pagos")) Or IsDBNull(fila("VtaTotal")) Then
    VComision = 0
   Else

    If fila("Pagos") <= 0 Or fila("VtaTotal") <= 0 Then
     VComision = 0
    Else
     VComision = fila("Pagos") * 100 / fila("VtaTotal")
    End If
   End If

   If VComision2 > 80 And VComision <= 89.99 Then
    strcadena &= 0.05 * fila("Pagos")
   ElseIf VComision2 >= 90 Then
    strcadena &= 0.07 * fila("Pagos")
   Else
    aux = 0
    strcadena &= aux.ToString
   End If


   strcadena &= ")"

   With cmdcob

    .CommandText = strcadena
    .ExecuteNonQuery()

   End With
  Next


  '/ FIN CON CLIENTES PROPIOS ****************************************************



  '*********************************************************************************************************
  '*SE AGREGA EL NOMBRE DEL AGENTE A LA CONSULTA QUE CONTIENE LO FACTURADO Y AGENTES QUE NO VENDIERON

  strcadena = "SELECT #REPCOB.*,DEPCOB.IdCobranza,DEPCOB.Nombre as NombreCob "
  strcadena &= "FROM #REPCOB LEFT JOIN DEPCOBR ON #REPCOB.IdVend = DEPCOBR.SlpCode and DEPCOBR.CbrGralAdicional='N'"
  strcadena &= "LEFT JOIN DEPCOB ON DEPCOBR.IdCobranza = DEPCOB.IdCobranza WHERE #REPCOB.Nombre <> 'CLIENTES PROPIOS (1)' "

  With cmdcob
   .CommandText = strcadena
  End With

  Dim DAdapter As New SqlClient.SqlDataAdapter(cmdcob)

  Dim DtPagoCob As New DataTable
  DAdapter.Fill(DtPagoCob)

  cmdcob.Connection.Close()

  CTabla = "CREATE TABLE #ORDCOB (IdVend Varchar(10),Nombre Varchar(100),VtaTotal Numeric(10,2),"
  CTabla &= "Dev Numeric(10,2),Des Numeric(10,2),"
  CTabla &= "Pagos Numeric(10,2),PorPago Numeric(8,2),PagoMesFac Numeric(10,2),PorMesFac Numeric(8,2),"
  CTabla &= "PagosOtroMes Numeric(10,2),PorOtroMes Numeric(8,2),"
  CTabla &= "ClaveCob Varchar(10),NomCob Varchar(40),orden Numeric(1,0),PorCom Numeric(10,2),Com Numeric(10,2) )"     'PorCom Numeric(10,2),Com Numeric(10,2),


  Dim cmdordena As Data.SqlClient.SqlCommand
  cmdordena = New Data.SqlClient.SqlCommand()

  With cmdordena
   .Connection = New Data.SqlClient.SqlConnection(StrTpm)
   .Connection.Open()
   .CommandText = CTabla
   .ExecuteNonQuery()
  End With

  For Each fila As DataRow In DtPagoCob.Rows

   strcadena = "INSERT INTO #ORDCOB (IdVend, Nombre,VtaTotal,Dev,Des,Pagos,PorPago,"
   strcadena &= "PagoMesFac,PorMesFac,PagosOtroMes,PorOtroMes,ClaveCob,NomCob,orden,PorCom,Com) VALUES ('"    ',PorCom,Com,
   strcadena &= IIf(fila("orden") = 0, fila("IdVend"), "")
   strcadena &= "','"
   strcadena &= fila("Nombre")
   strcadena &= "',"
   strcadena &= fila("VtaTotal")
   strcadena &= ","

   strcadena &= fila("Dev")
   strcadena &= ","

   strcadena &= fila("Des")
   strcadena &= ","

   strcadena &= fila("Pagos")
   strcadena &= ","
   strcadena &= fila("PorPago")
   strcadena &= ","
   strcadena &= fila("PagoMesFac")
   strcadena &= ","
   strcadena &= fila("PorMesFac")
   strcadena &= ","
   strcadena &= fila("PagosOtroMes")
   strcadena &= ","
   strcadena &= fila("PorOtroMes")
   strcadena &= ",'"
   strcadena &= IIf(fila("orden") = 0, fila("IdCobranza"), fila("ClaveCob"))
   strcadena &= "','"
   strcadena &= IIf(fila("orden") = 0, fila("NombreCob"), fila("NomCob"))
   strcadena &= "',"
   strcadena &= fila("orden")
   strcadena &= ","
   '----------------------------------------
   If fila("Nombre") = "*** Monto Total Jacqueline García ***" Or fila("Nombre") = "*** Monto Total Araceli Trujillo ***" Then
    If fila("PorPago") < 80 Then
     strcadena &= 0

    ElseIf fila("PorPago") < 89.99 Then
     strcadena &= 5 / 100

    ElseIf fila("PorPago") >= 90 Then
     strcadena &= 7 / 100

    End If
   ElseIf fila("Nombre") = "*** Monto Total Reyna Uc ***" Or fila("Nombre") = "*** Monto Total Nelly Gomez ***" Then
    If fila("PorPago") < 80 Then
     strcadena &= 0

    ElseIf fila("PorPago") < 89.99 Then
     strcadena &= 4 / 100

    ElseIf fila("PorPago") >= 90 Then
     strcadena &= 6 / 100

    End If
   Else
    strcadena &= 0
   End If
   '----------------------------------------
   strcadena &= ","

   If fila("Nombre") = "*** Monto Total Jacqueline García ***" Or fila("Nombre") = "*** Monto Total Araceli Trujillo ***" Then
    If fila("PorPago") < 80 Then
     strcadena &= 0

    ElseIf fila("PorPago") < 89.99 Then
     strcadena &= (5 / 10000) * fila("Pagos")

    ElseIf fila("PorPago") >= 90 Then
     strcadena &= (7 / 10000) * fila("Pagos")

    End If

   ElseIf fila("Nombre") = "*** Monto Total Reyna Uc ***" Or fila("Nombre") = "*** Monto Total Nelly Gomez ***" Then
    If fila("PorPago") < 80 Then
     strcadena &= 0

    ElseIf fila("PorPago") < 89.99 Then
     strcadena &= (4 / 10000) * fila("Pagos")

    ElseIf fila("PorPago") >= 90 Then
     strcadena &= (6 / 10000) * fila("Pagos")

    End If

   Else
    strcadena &= 0
   End If

   strcadena &= ")"

   With cmdordena
    .CommandText = strcadena
    .ExecuteNonQuery()
   End With
  Next

  If Me.CmbACob.SelectedValue = "TODOS" Then
   strcadena = "SELECT IdVend, Nombre, VtaTotal, Dev, Des, Pagos, "
   strcadena &= "PorPago,PagoMesFac, PorMesFac,PagosOtroMes,PorOtroMes,NomCob,PorCom,Com " ',PorCom,Com
   strcadena &= "FROM #ORDCOB "
   strcadena &= "GROUP BY ClaveCob, orden, IdVend, Nombre, VtaTotal, Dev, Des, Pagos, PorPago, PagoMesFac, PorMesFac, PagosOtroMes, PorOtroMes, NomCob, PorCom, Com "
   strcadena &= "ORDER BY ClaveCob,orden ASC,VtaTotal DESC "
  Else
   strcadena = "SELECT IdVend, Nombre, VtaTotal, Dev, Des, Pagos, PorPago,PagoMesFac, PorMesFac,PagosOtroMes,PorOtroMes,NomCob,PorCom,Com "    ',PorCom,Com
   strcadena &= "FROM #ORDCOB "
   strcadena &= "WHERE ClaveCob ="
   strcadena &= "'"
   strcadena &= Me.CmbACob.SelectedValue
   strcadena &= "'"
   strcadena &= " GROUP BY ClaveCob,orden, IdVend, Nombre, VtaTotal, Dev, Des, Pagos, PorPago, PagoMesFac, PorMesFac, PagosOtroMes, PorOtroMes, NomCob, PorCom, Com"
   strcadena &= " ORDER BY ClaveCob,orden ASC,VtaTotal DESC"
  End If

  With cmdordena
   .CommandText = strcadena
  End With

  Dim DaOrdena As New SqlClient.SqlDataAdapter(cmdordena)

  Dim DtOrdenaCob As New DataTable
  DaOrdena.Fill(DtOrdenaCob)

  cmdordena.Connection.Close()

  With Me.GrdConProd
   .DataSource = DtOrdenaCob
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
   .Columns(0).HeaderText = "Clave"
   .Columns(0).Width = 40
   .Columns(1).HeaderText = "Agente"
   .Columns(1).Width = 220

   .Columns(2).HeaderText = "Ventas Totales"
   .Columns(2).Width = 100
   .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   '.Columns(3).HeaderText = "% Vtas. Tot."
   '.Columns(3).Width = 50
   '.Columns(3).DefaultCellStyle.Format = "###.00"
   '.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns(3).HeaderText = "Monto Devuelto"
   .Columns(3).Width = 100
   .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(3).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(3).Visible = False

   '.Columns(5).HeaderText = "% Dvol. Sobre Venta"
   '.Columns(5).Width = 50
   '.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   '.Columns(5).DefaultCellStyle.Format = "###.00"


   .Columns(4).HeaderText = "Descuentos Pronto Pago"
   .Columns(4).Width = 100
   .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(4).DefaultCellStyle.Format = "###,###,###.00"
   .Columns(4).Visible = False

   '.Columns(7).HeaderText = "% PP Sobre Venta"
   '.Columns(7).Width = 50
   '.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   '.Columns(7).DefaultCellStyle.Format = "###.00"

   .Columns(5).HeaderText = "Pagos Totales"
   .Columns(5).Width = 100
   .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(5).DefaultCellStyle.Format = "###,###,###.00"

   .Columns(6).HeaderText = "% Pagos Totales"
   .Columns(6).Width = 50
   .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(6).DefaultCellStyle.Format = "###.00"

   .Columns(7).HeaderText = " Pago Venta Actual"
   .Columns(7).Width = 95
   .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(7).DefaultCellStyle.Format = "###,###,###.00"

   .Columns(8).HeaderText = "% Pago Venta Actual"
   .Columns(8).Width = 60
   .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(8).DefaultCellStyle.Format = "###.00"

   .Columns(9).HeaderText = "Pago Venta Anterior"
   .Columns(9).Width = 95
   .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(9).DefaultCellStyle.Format = "###,###,###.00"

   .Columns(10).HeaderText = "% Pago Venta Anterior"
   .Columns(10).Width = 50
   .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns(10).DefaultCellStyle.Format = "###.00"

   .Columns(11).HeaderText = "Cobranza"
   .Columns(11).Width = 120

   .Columns(12).HeaderText = "% Com"
   .Columns(12).Width = 40
   .Columns(12).DefaultCellStyle.Format = "## %"
   .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Then
    .Columns(12).Visible = True
   Else
    .Columns(12).Visible = False
   End If

   .Columns(13).HeaderText = "Comision"
   .Columns(13).Width = 75
   .Columns(13).DefaultCellStyle.Format = "$ ###,###.##"
   .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Then
    .Columns(13).Visible = True
   Else
    .Columns(13).Visible = False
   End If

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
  Dim Titulo As String

  Titulo = "Cobranza recuperada"

  Dim Columnas As String() = {"Clave",
                                "Agente",
                                "Ventas Totales",
                                "Devoluciones", 'Oculto
                                "Descuentos", 'Oculto
                                "Pagos Totales",
                                "% Pagos Totales",
                                "Pago Vta. Actual",
                                "% Pago Vta. Actual",
                                "Pago Vta. Anterior",
                                "% Pago Vta. Anterior",
                                "Cobranza",
                                "% Comisión",
                                "$ Comisión"}

  Dim TipoColumna As TipoDeDato() = {TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.ForzarPorcentaje,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.ForzarPorcentaje,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.ForzarPorcentaje,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Porecentaje,
                                       TipoDeDato.Pesos}
  Dim Visible As Boolean()
  If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Then
   Visible = {True,
                True,
                True,
                False,
                False,
                True,
                True,
                True,
                True,
                True,
                True,
                True,
                True,
                True}
  Else
   Visible = {True,
                True,
                True,
                False,
                False,
                True,
                True,
                True,
                True,
                True,
                True,
                True,
                False,
                False}
  End If

  funciones.exporta2Excel(Titulo, Columnas, TipoColumna, Visible, GrdConProd)


  'Dim oExcel As Object
  'Dim oBook As Object
  'Dim oSheet As Object

  ''Abrimos un nuevo libro
  'oExcel = CreateObject("Excel.Application")
  'oBook = oExcel.workbooks.add
  'oSheet = oBook.worksheets(1)

  ''Declaramos el nombre de las columnas
  'oSheet.range("A7").value = "Clave"      '1
  'oSheet.range("B7").value = "Agente"     '2
  'oSheet.range("C7").value = "Ventas Totales"     '3
  ''oSheet.range("D6").value = "% Vtas. Tot."
  ''oSheet.range("D7").value = "Monto Devuelto"     '4
  ''oSheet.range("F6").value = "% Dvol. Sobre Venta"
  ''oSheet.range("D7").value = "Descuentos Pronto Pago"     '5

  ''oSheet.range("H6").value = "% PP Sobre Venta"
  'oSheet.range("D7").value = "Pagos Totales"
  'oSheet.range("E7").value = "% Pagos Totales"
  'oSheet.range("F7").value = "Pago Vta. Actual"
  'oSheet.range("G7").value = "% Pago Vta. Actual"
  'oSheet.range("H7").value = "Pago Vta. Anterior"
  'oSheet.range("I7").value = "% Pago Vta. Anterior"
  'oSheet.range("J7").value = "Cobranza"

  'If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Then
  '  oSheet.range("K7").value = "% Comisión"
  '  oSheet.range("L7").value = "$ Comisión"
  'End If

  ''para poner la primera fila de los titulos en negrita
  'oSheet.range("A7:L7").font.bold = True
  'Dim fila_dt As Integer = 0
  'Dim fila_dt_excel As Integer = 0
  'Dim tanto_porcentaje As String = ""
  'Dim marikona As Integer = 0

  'Dim total_reg As Integer = 0

  'total_reg = Me.GrdConProd.RowCount
  'For fila_dt = 0 To total_reg - 1

  '  'para leer una celda en concreto
  '  'el numero es la columna
  '  Dim cel1 As String = Me.GrdConProd.Item(0, fila_dt).Value
  '  Dim cel2 As String = Me.GrdConProd.Item(1, fila_dt).Value
  '  Dim cel3 As String = IIf(IsDBNull(Me.GrdConProd.Item(2, fila_dt).Value), 0, Me.GrdConProd.Item(2, fila_dt).Value)
  '  'im cel4 As String = IIf(IsDBNull(Me.GrdConProd.Item(3, fila_dt).Value), 0, Me.GrdConProd.Item(3, fila_dt).Value)
  '  'Dim cel4 As String = IIf(IsDBNull(Me.GrdConProd.Item(4, fila_dt).Value), 0, Me.GrdConProd.Item(4, fila_dt).Value)
  '  Dim cel4 As String = IIf(IsDBNull(Me.GrdConProd.Item(5, fila_dt).Value), 0, Me.GrdConProd.Item(5, fila_dt).Value)
  '  Dim cel5 As String = IIf(IsDBNull(Me.GrdConProd.Item(6, fila_dt).Value), 0, Me.GrdConProd.Item(6, fila_dt).Value)
  '  Dim cel6 As String = IIf(IsDBNull(Me.GrdConProd.Item(7, fila_dt).Value), 0, Me.GrdConProd.Item(7, fila_dt).Value)
  '  Dim cel7 As String = IIf(IsDBNull(Me.GrdConProd.Item(8, fila_dt).Value), 0, Me.GrdConProd.Item(8, fila_dt).Value)
  '  Dim cel8 As String = IIf(IsDBNull(Me.GrdConProd.Item(9, fila_dt).Value), 0, Me.GrdConProd.Item(9, fila_dt).Value)
  '  Dim cel9 As String = IIf(IsDBNull(Me.GrdConProd.Item(10, fila_dt).Value), 0, Me.GrdConProd.Item(10, fila_dt).Value)
  '  Dim cel10 As String = IIf(IsDBNull(Me.GrdConProd.Item(11, fila_dt).Value), 0, Me.GrdConProd.Item(11, fila_dt).Value)

  '  'If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Then
  '  Dim cel11 As String = IIf(IsDBNull(Me.GrdConProd.Item(12, fila_dt).Value), 0, Me.GrdConProd.Item(12, fila_dt).Value)
  '  Dim cel12 As String = IIf(IsDBNull(Me.GrdConProd.Item(13, fila_dt).Value), 0, Me.GrdConProd.Item(13, fila_dt).Value)
  '  'End If


  '  fila_dt_excel = fila_dt + 8 'Renglón en donde se empieza a registrar el reporte

  '  'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
  '  oSheet.range("A" & fila_dt_excel).value = cel1
  '  oSheet.range("B" & fila_dt_excel).value = cel2
  '  oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel3, 2) 'Da formato número con dos decimales a la columna C
  '  'oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
  '  'oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
  '  oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
  '  oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
  '  oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)

  '  oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
  '  oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)
  '  oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
  '  oSheet.range("J" & fila_dt_excel).value = cel10

  '  If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Then
  '    oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel11, 2)
  '    oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel12, 2)
  '  End If

  '  'oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel14, 2)
  '  'oSheet.range("O" & fila_dt_excel).value = cel15

  '  If Me.GrdConProd.Item(1, fila_dt).Value = "*** Monto Total Jacqueline García ***" Or Me.GrdConProd.Item(1, fila_dt).Value = "*** Monto Total Araceli Trujillo ***" _
  '            Or Me.GrdConProd.Item(1, fila_dt).Value = "*** Monto Total Reyna Uc ***" Or Me.GrdConProd.Item(1, fila_dt).Value = "*** Monto Total Nelly Gomez ***" _
  '            Or Me.GrdConProd.Item(1, fila_dt).Value = "*** Monto Total CLIENTES PROPIOS ***" Then
  '    oSheet.Range("A" & fila_dt_excel).Interior.ColorIndex = 15
  '    oSheet.Range("B" & fila_dt_excel).Interior.ColorIndex = 15
  '    oSheet.Range("C" & fila_dt_excel).Interior.ColorIndex = 15
  '    'oSheet.Range("D" & fila_dt_excel).Interior.ColorIndex = 15
  '    'oSheet.Range("D" & fila_dt_excel).Interior.ColorIndex = 15
  '    oSheet.Range("D" & fila_dt_excel).Interior.ColorIndex = 15
  '    oSheet.Range("F" & fila_dt_excel).Interior.ColorIndex = 15
  '    oSheet.Range("F" & fila_dt_excel).Interior.ColorIndex = 15
  '    oSheet.Range("G" & fila_dt_excel).Interior.ColorIndex = 15
  '    oSheet.Range("H" & fila_dt_excel).Interior.ColorIndex = 15
  '    oSheet.Range("I" & fila_dt_excel).Interior.ColorIndex = 15
  '    oSheet.Range("J" & fila_dt_excel).Interior.ColorIndex = 15
  '    If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Then
  '      oSheet.Range("K" & fila_dt_excel).Interior.ColorIndex = 15
  '      oSheet.Range("L" & fila_dt_excel).Interior.ColorIndex = 15
  '    End If

  '  End If

  'Next

  '' para que el tamano de la columna tenga como minimo el maximo de sus textos
  'oSheet.columns("A:O").entirecolumn.autofit()

  'oSheet.range("A1").value = "Reporte de Cobranza"
  'oSheet.range("A2").value = "Periodo de Ventas De" + Format(Me.DtpPVtaIni.Value, " dd/MM/yyyy") + " A " + Format(Me.DtpPVtaTer.Value, " dd/MM/yyyy")
  'oSheet.range("A3").value = "Periodo de Ventas De" + Format(Me.DtpDesIni.Value, " dd/MM/yyyy") + " A " + Format(Me.DtpDesTer.Value, " dd/MM/yyyy")

  ''oSheet.Range("A1").Interior.ColorIndex = 15
  ''oSheet.Range("A2").Interior.ColorIndex = 15
  ''oSheet.Range("A3").Interior.ColorIndex = 15
  ''oSheet.Range("A4").Interior.ColorIndex = 15
  ''oSheet.Range("A5").Interior.ColorIndex = 15
  '''oSheet.Range("A6").Interior.ColorIndex = 15

  'oSheet.Columns("A").EntireColumn.ColumnWidth = 6
  'oSheet.Columns("B").EntireColumn.ColumnWidth = 30
  'oSheet.Columns("C").EntireColumn.ColumnWidth = 13
  ''oSheet.Columns("D").EntireColumn.ColumnWidth = 13
  'oSheet.Columns("D").EntireColumn.ColumnWidth = 13
  'oSheet.Columns("E").EntireColumn.ColumnWidth = 13
  'oSheet.Columns("F").EntireColumn.ColumnWidth = 7
  'oSheet.Columns("G").EntireColumn.ColumnWidth = 13
  'oSheet.Columns("H").EntireColumn.ColumnWidth = 7
  'oSheet.Columns("I").EntireColumn.ColumnWidth = 13
  'oSheet.Columns("J").EntireColumn.ColumnWidth = 7
  'oSheet.Columns("K").EntireColumn.ColumnWidth = 13

  'If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Then
  '  oSheet.Columns("L").EntireColumn.ColumnWidth = 7
  '  oSheet.Columns("M").EntireColumn.ColumnWidth = 13
  'End If

  'oSheet.range("C1").value = Rangos
  'oSheet.range("C2").value = Rangos2

  'oExcel.visible = True
  'System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  'GC.Collect()
  'oSheet = Nothing
  'oBook = Nothing
  'oExcel = Nothing

 End Sub

 Sub ValidaNave()
  If Me.CmbACob.SelectedValue <> "TODOS" Then
   DvLP.RowFilter = "Id_Area = " & "'" & Me.CmbACob.SelectedValue & "'"
  Else
   DvLP.RowFilter = String.Empty
  End If
 End Sub

 Private Sub GrdConProd_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles GrdConProd.RowPrePaint
  If GrdConProd.Rows(e.RowIndex).Cells("Nombre").Value = "*** Monto Total Jacqueline García ***" _
          Or GrdConProd.Rows(e.RowIndex).Cells("Nombre").Value = "*** Monto Total Araceli Trujillo ***" _
          Or GrdConProd.Rows(e.RowIndex).Cells("Nombre").Value = "*** Monto Total Reyna Uc ***" _
          Or GrdConProd.Rows(e.RowIndex).Cells("Nombre").Value = "*** Monto Total Nelly Gomez ***" _
          Or GrdConProd.Rows(e.RowIndex).Cells("Nombre").Value = "*** Monto Total CLIENTES PROPIOS ***" _
        Then

   GrdConProd.Rows(e.RowIndex).Cells("IdVend").Style.BackColor = Color.LightGray
   GrdConProd.Rows(e.RowIndex).Cells("Nombre").Style.BackColor = Color.LightGray
   GrdConProd.Rows(e.RowIndex).Cells("VtaTotal").Style.BackColor = Color.LightGray
   'GrdConProd.Rows(e.RowIndex).Cells("PorVtas").Style.BackColor = Color.LightGray
   'GrdConProd.Rows(e.RowIndex).Cells("Dev").Style.BackColor = Color.LightGray
   'GrdConProd.Rows(e.RowIndex).Cells("PorDev").Style.BackColor = Color.LightGray
   'GrdConProd.Rows(e.RowIndex).Cells("Des").Style.BackColor = Color.LightGray
   'GrdConProd.Rows(e.RowIndex).Cells("PorDes").Style.BackColor = Color.LightGray
   GrdConProd.Rows(e.RowIndex).Cells("Pagos").Style.BackColor = Color.LightGray
   GrdConProd.Rows(e.RowIndex).Cells("PorPago").Style.BackColor = Color.LightGray
   GrdConProd.Rows(e.RowIndex).Cells("PagoMesFac").Style.BackColor = Color.LightGray
   GrdConProd.Rows(e.RowIndex).Cells("PorMesFac").Style.BackColor = Color.LightGray
   GrdConProd.Rows(e.RowIndex).Cells("PagosOtroMes").Style.BackColor = Color.LightGray
   GrdConProd.Rows(e.RowIndex).Cells("PorOtroMes").Style.BackColor = Color.LightGray
   GrdConProd.Rows(e.RowIndex).Cells("NomCob").Style.BackColor = Color.LightGray
   GrdConProd.Rows(e.RowIndex).Cells("PorCom").Style.BackColor = Color.LightGray
   GrdConProd.Rows(e.RowIndex).Cells("Com").Style.BackColor = Color.LightGray
  End If


  'GrdConProd.Rows(e.RowIndex).Cells("VtaTotal").Style.BackColor = Color.LightGray

  'GrdConProd.Rows(e.RowIndex).Cells("PagoMesFac").Style.BackColor = Color.LightGray

  'GrdConProd.Rows(e.RowIndex).Cells("PorMesFac").Style.BackColor = Color.Yellow


  If GrdConProd.Rows(e.RowIndex).Cells("Nombre").Value = "--- Totales del Periodo ---" Then

   GrdConProd.Rows(e.RowIndex).Cells("IdVend").Style.BackColor = Color.Black
   GrdConProd.Rows(e.RowIndex).Cells("Nombre").Style.BackColor = Color.Black
   GrdConProd.Rows(e.RowIndex).Cells("VtaTotal").Style.BackColor = Color.Black
   'GrdConProd.Rows(e.RowIndex).Cells("PorVtas").Style.BackColor = Color.Black
   'GrdConProd.Rows(e.RowIndex).Cells("Dev").Style.BackColor = Color.Black
   'GrdConProd.Rows(e.RowIndex).Cells("PorDev").Style.BackColor = Color.Black
   'GrdConProd.Rows(e.RowIndex).Cells("Des").Style.BackColor = Color.Black
   'GrdConProd.Rows(e.RowIndex).Cells("PorDes").Style.BackColor = Color.Black
   GrdConProd.Rows(e.RowIndex).Cells("Pagos").Style.BackColor = Color.Black
   GrdConProd.Rows(e.RowIndex).Cells("PorPago").Style.BackColor = Color.Black
   GrdConProd.Rows(e.RowIndex).Cells("PagoMesFac").Style.BackColor = Color.Black
   GrdConProd.Rows(e.RowIndex).Cells("PorMesFac").Style.BackColor = Color.Black
   GrdConProd.Rows(e.RowIndex).Cells("PagosOtroMes").Style.BackColor = Color.Black
   GrdConProd.Rows(e.RowIndex).Cells("PorOtroMes").Style.BackColor = Color.Black
   GrdConProd.Rows(e.RowIndex).Cells("NomCob").Style.BackColor = Color.Black
   GrdConProd.Rows(e.RowIndex).Cells("PorCom").Style.BackColor = Color.Black
   GrdConProd.Rows(e.RowIndex).Cells("Com").Style.BackColor = Color.Black

   GrdConProd.Rows(e.RowIndex).Cells("IdVend").Style.ForeColor = Color.White
   GrdConProd.Rows(e.RowIndex).Cells("Nombre").Style.ForeColor = Color.White
   GrdConProd.Rows(e.RowIndex).Cells("VtaTotal").Style.ForeColor = Color.White
   'GrdConProd.Rows(e.RowIndex).Cells("PorVtas").Style.ForeColor = Color.White
   'GrdConProd.Rows(e.RowIndex).Cells("Dev").Style.ForeColor = Color.White
   'GrdConProd.Rows(e.RowIndex).Cells("PorDev").Style.ForeColor = Color.White
   'GrdConProd.Rows(e.RowIndex).Cells("Des").Style.ForeColor = Color.White
   'GrdConProd.Rows(e.RowIndex).Cells("PorDes").Style.ForeColor = Color.White
   GrdConProd.Rows(e.RowIndex).Cells("Pagos").Style.ForeColor = Color.White
   GrdConProd.Rows(e.RowIndex).Cells("PorPago").Style.ForeColor = Color.White
   GrdConProd.Rows(e.RowIndex).Cells("PagoMesFac").Style.ForeColor = Color.White
   GrdConProd.Rows(e.RowIndex).Cells("PorMesFac").Style.ForeColor = Color.White
   GrdConProd.Rows(e.RowIndex).Cells("PagosOtroMes").Style.ForeColor = Color.White
   GrdConProd.Rows(e.RowIndex).Cells("PorOtroMes").Style.ForeColor = Color.White
   GrdConProd.Rows(e.RowIndex).Cells("NomCob").Style.ForeColor = Color.White
   GrdConProd.Rows(e.RowIndex).Cells("PorCom").Style.ForeColor = Color.White
   GrdConProd.Rows(e.RowIndex).Cells("Com").Style.ForeColor = Color.White

  End If


 End Sub

 Private Sub DtpPPagoIni_ValueChanged(sender As Object, e As EventArgs) Handles DtpPPagoIni.ValueChanged

 End Sub
End Class