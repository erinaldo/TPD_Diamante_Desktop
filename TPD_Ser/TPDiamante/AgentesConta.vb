Public Class AgentesConta
        Dim DivTer As New DataView
        Dim DivIni As New DataView
        Dim objDataSet As New DataTable
        Dim Rangos As String = ""
        Dim Rangos2 As String = ""


        Private Sub ConsultaProd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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


        Consulta = " SELECT TipoDoc,Docnum,docdate,IdVend,slpname,cuenta,VtaAntesIva,itemcode,itemname, "
        Consulta &= " itmsgrpcod,itmsgrpnam,groupcode,groupname INTO #VtasNet FROM( "
        Consulta &= " SELECT 'FACTURA' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode 'IdVend', T1.SlpName, "
        Consulta &= " T0.AcctCode 'cuenta', "
        Consulta &= " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal ELSE T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100 END AS 'VtaAntesIva', "
        Consulta &= " CASE WHEN T0.ItemCode IS NULL THEN 'OTROS' ELSE T0.ItemCode END AS 'ItemCode', "
        Consulta &= " CASE WHEN T2.ItemName IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItemName END AS 'ItemName', "
        Consulta &= " CASE WHEN T3.ItmsGrpCod IS NULL THEN 0 ELSE T3.ItmsGrpCod END AS 'ItmsGrpCod', "
        Consulta &= " CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', "
        Consulta &= " T4.GroupCode, T5.GroupName "
        Consulta &= " FROM [SBO_TPD].[dbo].[OINV] T6 "
        Consulta &= " INNER JOIN [SBO_TPD].[dbo].[INV1] T0 ON T0.DocEntry = t6.DocEntry "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
        Consulta &= " LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
        Consulta &= " WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
        Consulta &= " and T4.CbrGralAdicional = 'N' "
        Consulta &= " UNION ALL"
        Consulta &= " SELECT 'NOTA C.' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, "
        Consulta &= " T0.AcctCode 'cuenta', "
        Consulta &= " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal * -1 ELSE (T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100)*-1 END AS 'Importe', "
        Consulta &= " CASE WHEN T0.ItemCode IS NULL THEN 'OTROS' ELSE T0.ItemCode END AS 'ItemCode', "
        Consulta &= " CASE WHEN T2.ItemName IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItemName END AS 'ItemName', "
        Consulta &= " CASE WHEN T3.ItmsGrpCod IS NULL THEN 0 ELSE T3.ItmsGrpCod END AS 'ItmsGrpCod', "
        Consulta &= " CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', "
        Consulta &= " T4.GroupCode, T5.GroupName "
        Consulta &= " FROM [SBO_TPD].[dbo].[ORIN] T6 "
        Consulta &= " INNER JOIN [SBO_TPD].[dbo].[RIN1] T0 ON T0.DocEntry = t6.DocEntry "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
        Consulta &= " LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
        Consulta &= " WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
        Consulta &= " and T4.CbrGralAdicional = 'N' "
        Consulta &= " ) TMP"
        Consulta &= " ORDER BY VtaAntesIva "

        '** VENTAS TOTALES POR VENDEDOR
        '** LO FACTURADO
        Consulta &= " SELECT SUM(VtaAntesIva) 'VtaAntesIva',IdVend "
        Consulta &= " FROM #VtasNet "
        Consulta &= " WHERE (cuenta='4110-001-000' "
        Consulta &= " OR cuenta='4110-002-000' OR cuenta='4110-003-000')  "
        'Consulta &= "AND T0.Series<>'59' "

        If (CkClientes.Checked = False) Then
            Consulta &= " AND IdVend <> 1"
        End If

        Consulta &= " GROUP BY IdVend ORDER BY VtaAntesIva DESC "

        Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

        'MsgBox(Me.DtpFechaIni.Value)
        'MsgBox(Me.DtpFechaTer.Value)

        CmdMObra.Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
        CmdMObra.Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)

        'CmdMObra.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime)
        '    CmdMObra.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
        'CmdMObra.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime)
        '    CmdMObra.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value
        CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
        CmdMObra.Connection.Open()

        Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
        AdapMObra.Fill(DTMObra)
        CmdMObra.Connection.Close()


        '** Se crea cursor para reporte
        CTabla = "CREATE TABLE #REPVTAS (IdVend INT,VtaAntesIva Numeric(20,2),PorVtas Numeric(20,2),"
        CTabla &= "Dev Numeric(20,2),PorDev Numeric(20,2),Des Numeric(20,2),PorDes Numeric(20,2),"
        CTabla &= "Cancels Numeric(20,2),PorCan Numeric(20,2),VtaCDes Numeric(20,2),PorCDes Numeric(20,2),"
        CTabla &= "VtasNt Numeric(20,2),TotNC Numeric(20,2))"

        Dim cmdcost As Data.SqlClient.SqlCommand
        cmdcost = New Data.SqlClient.SqlCommand()

    With cmdcost
            .Connection = New Data.SqlClient.SqlConnection(StrCon)
            .Connection.Open()
            .CommandText = CTabla
            .ExecuteNonQuery()
        End With


        'AND Doctype = 'I'
        '** SE OBTIENE EL MONTO TOTAL DE LAS VENTAS Y SE AGREGA AL CURSOR
        Dim TotalVtas As Decimal
        Dim cmd As New Data.SqlClient.SqlCommand
        With cmd
            .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
            .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)

            .CommandText = "SELECT TipoDoc,Docnum,docdate,IdVend,slpname,cuenta,VtaAntesIva,itemcode,itemname, " +
            " itmsgrpcod,itmsgrpnam,groupcode,groupname INTO #VtasNet FROM( " +
            " SELECT 'FACTURA' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode 'IdVend', T1.SlpName, " +
             " T0.AcctCode 'cuenta', " +
             " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal ELSE T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100 END AS 'VtaAntesIva', " +
             " CASE WHEN T0.ItemCode IS NULL THEN 'OTROS' ELSE T0.ItemCode END AS 'ItemCode', " +
             " CASE WHEN T2.ItemName IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItemName END AS 'ItemName', " +
             " CASE WHEN T3.ItmsGrpCod IS NULL THEN 0 ELSE T3.ItmsGrpCod END AS 'ItmsGrpCod', " +
             " CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', " +
             " T4.GroupCode, T5.GroupName " +
             " FROM [SBO_TPD].[dbo].[OINV] T6 " +
             " INNER JOIN [SBO_TPD].[dbo].[INV1] T0 ON T0.DocEntry = t6.DocEntry " +
             " LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode " +
             " LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode " +
             " LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod " +
             " LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode " +
             " LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode " +
             " WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer " +
             " and T4.CbrGralAdicional = 'N' " +
             " UNION ALL" +
             " SELECT 'NOTA C.' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, " +
             " T0.AcctCode 'cuenta', " +
             " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal * -1 ELSE (T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100)*-1 END AS 'Importe', " +
             " CASE WHEN T0.ItemCode IS NULL THEN 'OTROS' ELSE T0.ItemCode END AS 'ItemCode', " +
             " CASE WHEN T2.ItemName IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItemName END AS 'ItemName', " +
             " CASE WHEN T3.ItmsGrpCod IS NULL THEN 0 ELSE T3.ItmsGrpCod END AS 'ItmsGrpCod', " +
             " CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', " +
             " T4.GroupCode, T5.GroupName " +
             " FROM [SBO_TPD].[dbo].[ORIN] T6 " +
             " INNER JOIN [SBO_TPD].[dbo].[RIN1] T0 ON T0.DocEntry = t6.DocEntry " +
             " LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode " +
             " LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode " +
             " LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod " +
             " LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode " +
             " LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode " +
             " WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer " +
             " and T4.CbrGralAdicional = 'N' " +
             " ) TMP" +
             " ORDER BY VtaAntesIva " +
            "SELECT SUM(VtaAntesIva) 'VtaAntesIva' FROM #VtasNet  " +
            " WHERE (cuenta='4110-001-000' " +
            " OR cuenta='4110-002-000' OR cuenta='4110-003-000')  "
            If (CkClientes.Checked = False) Then
                .CommandText = .CommandText + " and IdVend <> 1 "
            End If
            .CommandTimeout = 200
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
            strcadena &= fila("VtaAntesIva")
            strcadena &= ","
            strcadena &= fila("VtaAntesIva") * 100 / TotalVtas
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
        strcadena &= "(SELECT Slpcode FROM OINV WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer "  ''AND SERIES <> 59  AND DOCNUM <> 2000458 AND DOCNUM <> 2000459
        strcadena &= " GROUP BY OINV.SlpCode) "
        If (CkClientes.Checked = False) Then
            strcadena &= " and slpCode <> 1"
        End If
        strcadena &= "GROUP BY ORIN.SlpCode"

        Dim CmdSinVta As New SqlClient.SqlCommand(strcadena)
        CmdSinVta.CommandTimeout = 200

        CmdSinVta.Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
        CmdSinVta.Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)

        'CmdSinVta.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime)
        'CmdSinVta.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
        'CmdSinVta.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime)
        'CmdSinVta.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value
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
                '.Parameters.AddWithValue("@FechaCosto", VMObFechaP2)
                .CommandText = strcadena
                .ExecuteNonQuery()
                ' .Parameters.Clear()
            End With
        Next



        '************************************************************************************

        ''If VEsAgente <> 1 Or UsrTPM = "VVERGARA" Or UsrTPM = "RROBLES" Then

        strcadena = "INSERT INTO #REPVTAS (IdVend, VtaAntesIva,PorVtas) VALUES ("
        strcadena &= 0
        strcadena &= ","
        strcadena &= TotalVtas
        strcadena &= ","
        strcadena &= 100
        strcadena &= ")"

        With cmdcost
            '.Parameters.AddWithValue("@FechaCosto", VMObFechaP2)
            .CommandText = strcadena
            .ExecuteNonQuery()
            ' .Parameters.Clear()
        End With

        ''End If
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
        strcadena &= "Dev,PorDev,VtasNt,Des,PorDes,TotNC,Cancels,PorCan, "
        strcadena &= "CAST(#REPVTAS.VtaAntesIva AS dec(20,2)) as VtaAntesIva, "
        strcadena &= "CAST(#REPVTAS.PorVtas AS dec(5,2)) as PorVtas "
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

        Consulta = " SELECT TipoDoc,Docnum,docdate,IdVend,slpname,cuenta,VtaAntesIva,itemcode,itemname, "
        Consulta &= " itmsgrpcod,itmsgrpnam,groupcode,groupname INTO #VtasNet FROM( "
        Consulta &= " SELECT 'FACTURA' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode 'IdVend', T1.SlpName, "
        Consulta &= " T0.AcctCode 'cuenta', "
        Consulta &= " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal ELSE T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100 END AS 'VtaAntesIva', "
        Consulta &= " CASE WHEN T0.ItemCode IS NULL THEN 'OTROS' ELSE T0.ItemCode END AS 'ItemCode', "
        Consulta &= " CASE WHEN T2.ItemName IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItemName END AS 'ItemName', "
        Consulta &= " CASE WHEN T3.ItmsGrpCod IS NULL THEN 0 ELSE T3.ItmsGrpCod END AS 'ItmsGrpCod', "
        Consulta &= " CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', "
        Consulta &= " T4.GroupCode, T5.GroupName "
        Consulta &= " FROM [SBO_TPD].[dbo].[OINV] T6 "
        Consulta &= " INNER JOIN [SBO_TPD].[dbo].[INV1] T0 ON T0.DocEntry = t6.DocEntry "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
        Consulta &= " LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
        Consulta &= " WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
        Consulta &= " and T4.CbrGralAdicional = 'N' "
        Consulta &= " UNION ALL"
        Consulta &= " SELECT 'NOTA C.' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, "
        Consulta &= " T0.AcctCode 'cuenta', "
        Consulta &= " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal * -1 ELSE (T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100)*-1 END AS 'Importe', "
        Consulta &= " CASE WHEN T0.ItemCode IS NULL THEN 'OTROS' ELSE T0.ItemCode END AS 'ItemCode', "
        Consulta &= " CASE WHEN T2.ItemName IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItemName END AS 'ItemName', "
        Consulta &= " CASE WHEN T3.ItmsGrpCod IS NULL THEN 0 ELSE T3.ItmsGrpCod END AS 'ItmsGrpCod', "
        Consulta &= " CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', "
        Consulta &= " T4.GroupCode, T5.GroupName "
        Consulta &= " FROM [SBO_TPD].[dbo].[ORIN] T6 "
        Consulta &= " INNER JOIN [SBO_TPD].[dbo].[RIN1] T0 ON T0.DocEntry = t6.DocEntry "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
        Consulta &= " LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
        Consulta &= " WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
        Consulta &= " and T4.CbrGralAdicional = 'N' "
        Consulta &= " ) TMP"
        Consulta &= " ORDER BY VtaAntesIva "


        'cmdcost.Connection.Close()
        'DEVOLUCIONES
        Dim DataCRec As Data.SqlClient.SqlDataReader

        Consulta &= " SELECT IdVend,SUM(VtaAntesIva)*-1 'DevAntesIva' "
        Consulta &= " FROM #VtasNet "
        Consulta &= " WHERE (cuenta='4310-001-000' "
        Consulta &= " OR cuenta='4310-002-000' OR cuenta='4310-003-000')  "
        If (CkClientes.Checked = False) Then
            Consulta &= " and slpCode <> 1"
        End If
        Consulta &= "GROUP BY IdVend ORDER BY 2 DESC"


        'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DevAntesIva "
        'Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer AND Eseries = 6"
        'Consulta &= "GROUP BY ORIN.SlpCode ORDER BY DevAntesIva DESC"


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

        'For Each fila As DataRow In DTRefacciones.Rows
        '    If fila("IdVend") = 0 Then
        '        fila("Dev") = TotDev
        '    End If
        '    If Not IsDBNull(fila("Dev")) Then
        '        fila("PorDev") = fila("Dev") * 100 / TotDev
        '    End If
        'Next


        '****************************************************
        'DESCUENTOS PRONTO PAGO
        cmdcost.Connection.Close()
        Dim DtVtaDes As Data.SqlClient.SqlDataReader

        Consulta = " SELECT TipoDoc,Docnum,docdate,IdVend,slpname,cuenta,VtaAntesIva,itemcode,itemname, "
        Consulta &= " itmsgrpcod,itmsgrpnam,groupcode,groupname INTO #VtasNet FROM( "
        Consulta &= " SELECT 'FACTURA' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode 'IdVend', T1.SlpName, "
        Consulta &= " T0.AcctCode 'cuenta', "
        Consulta &= " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal ELSE T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100 END AS 'VtaAntesIva', "
        Consulta &= " CASE WHEN T0.ItemCode IS NULL THEN 'OTROS' ELSE T0.ItemCode END AS 'ItemCode', "
        Consulta &= " CASE WHEN T2.ItemName IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItemName END AS 'ItemName', "
        Consulta &= " CASE WHEN T3.ItmsGrpCod IS NULL THEN 0 ELSE T3.ItmsGrpCod END AS 'ItmsGrpCod', "
        Consulta &= " CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', "
        Consulta &= " T4.GroupCode, T5.GroupName "
        Consulta &= " FROM [SBO_TPD].[dbo].[OINV] T6 "
        Consulta &= " INNER JOIN [SBO_TPD].[dbo].[INV1] T0 ON T0.DocEntry = t6.DocEntry "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
        Consulta &= " LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
        Consulta &= " WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
        Consulta &= " and T4.CbrGralAdicional = 'N' "
        Consulta &= " UNION ALL"
        Consulta &= " SELECT 'NOTA C.' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, "
        Consulta &= " T0.AcctCode 'cuenta', "
        Consulta &= " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal * -1 ELSE (T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100)*-1 END AS 'Importe', "
        Consulta &= " CASE WHEN T0.ItemCode IS NULL THEN 'OTROS' ELSE T0.ItemCode END AS 'ItemCode', "
        Consulta &= " CASE WHEN T2.ItemName IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItemName END AS 'ItemName', "
        Consulta &= " CASE WHEN T3.ItmsGrpCod IS NULL THEN 0 ELSE T3.ItmsGrpCod END AS 'ItmsGrpCod', "
        Consulta &= " CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', "
        Consulta &= " T4.GroupCode, T5.GroupName "
        Consulta &= " FROM [SBO_TPD].[dbo].[ORIN] T6 "
        Consulta &= " INNER JOIN [SBO_TPD].[dbo].[RIN1] T0 ON T0.DocEntry = t6.DocEntry "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
        Consulta &= " LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
        Consulta &= " WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
        Consulta &= " and T4.CbrGralAdicional = 'N' "
        Consulta &= " ) TMP"
        Consulta &= " ORDER BY VtaAntesIva "

        'DESCUENTOS PRONTO PAGO
        Consulta &= " SELECT IdVend,SUM(VtaAntesIva)*-1 'DesAntesIva' "
        Consulta &= " FROM #VtasNet "
        Consulta &= " WHERE (cuenta='4210-001-000' "
        Consulta &= " OR cuenta='4210-002-000' OR cuenta='4210-003-000')  "
        'Consulta &= "AND T0.Series<>'59' "

        'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as DesAntesIva "
        'Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer "
        'Consulta &= "AND DocType  = 'S' "
        If (CkClientes.Checked = False) Then
            Consulta &= " and IdVend <> 1 "
        End If
        Consulta &= "GROUP BY IdVend ORDER BY 2 DESC "

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

        'Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels "
        'Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer "
        'Consulta &= "AND (Eseries != 6 or Eseries is null) AND DocType  = 'I' AND Series <> 49 "
        'Consulta &= "GROUP BY ORIN.SlpCode ORDER BY TotCancels DESC"

        'If Me.DtpFechaIni.Value ="31-10-2016" OR Me.DtpFechaTer.Value =



        Consulta = "SELECT Slpcode as IdVend,SUM((DocTotal - VatSum) - TotalExpns) as TotCancels "
        Consulta &= "FROM ORIN WHERE DocDate >= @FechaIni AND DocDate <= @FechaTer "
        Consulta &= "AND DocType  = 'I' AND  EDocNum IS NULL "    ''SE AGREGO PORQUE EN OCTUBRE SE CANCELO MAL      AND  EDocNum IS NULL OR DOCNUM=2014036
        If (CkClientes.Checked = False) Then
            Consulta &= " and slpCode <> 1"
        End If
        Consulta &= "GROUP BY ORIN.SlpCode ORDER BY TotCancels DESC"



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


            fila("VtaCDes") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) ' - IIf(IsDBNull(fila("Cancels")), 0, fila("Cancels"))
            fila("VtasNt") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) - (IIf(IsDBNull(fila("Des")), 0, fila("Des")) + IIf(IsDBNull(fila("Dev")), 0, fila("Dev")))
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
                        fila("PorDev") = fila("Dev") / fila("VtaCDes") 'ORIGINAL: fila("PorDev") = fila("Dev") * 100 / fila("VtaCDes")
                    End If
                End If


                If Not IsDBNull(fila("Des")) And fila("VtaCDes") <> 0 Then
                    If fila("Des") <> 0 Then
                        fila("PorDes") = fila("Des") / fila("VtaCDes")    'ORIGINAL: fila("PorDes") = fila("Des") * 100 / fila("VtaCDes")
                    End If

                End If

                If Not IsDBNull(fila("Cancels")) And fila("VtaCDes") <> 0 Then
                    If fila("Cancels") <> 0 Then
                        fila("PorCan") = fila("Cancels") / fila("VtaCDes")    'fila("PorCan") = fila("Cancels") * 100 / fila("VtaCDes")
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

            'fila("Dev") = TotDev
            'fila("PorDev") = fila("Dev") * 100 / TotDev

            'fila("Des") = TotDes
            'fila("PorDes") = fila("Des") * 100 / TotDes

            'fila("Cancels") = TotCanc
            'fila("PorCan") = fila("Cancels") * 100 / TotCanc

            'fila("PorCDes") = fila("VtaCDes") * 100 / vnetas
            'fila("VtaCDes") = vnetas


            'If Not IsDBNull(fila("Cancels")) Then
            '    fila("PorCan") = fila("Cancels") * 100 / TotCanc
            'End If
        Next


        '**********************************


        'fila("VtaCDes") = IIf(IsDBNull(fila("VtaAntesIva")), 0, fila("VtaAntesIva")) - (IIf(IsDBNull(fila("Des")), 0, fila("Des")) + IIf(IsDBNull(fila("Cancels")), 0, fila("Cancels")) + IIf(IsDBNull(fila("Dev")), 0, fila("Dev")))
        'vnetas = vnetas + fila("VtaCDes")


        'For Each fila As DataRow In DTRefacciones.Rows
        '    If fila("IdVend") = 0 Then
        '        fila("SlpName") = "MONTO TOTAL"
        '        fila("Cancels") = TotCanc
        '    End If

        '    fila("VtaCDes") = fila("VtaAntesIva") - (fila("Des") + fila("Cancels") + fila("Dev"))

        '    If Not IsDBNull(fila("Cancels")) Then
        '        fila("PorCan") = fila("Cancels") * 100 / TotCanc
        '    End If
        'Next


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


            .Columns(10).HeaderText = "Cancelaciones Antes IVA"
            .Columns(10).Width = 120
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(10).DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns(10).Visible = False

            .Columns(11).HeaderText = "% Canc. Desc."
            .Columns(11).Width = 100
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(11).DefaultCellStyle.Format = "###.00 %"
            .Columns(11).Visible = False


            .Columns(12).HeaderText = "Facturación"
            .Columns(12).Width = 120
            .Columns(12).DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(12).Visible = True

            .Columns(13).HeaderText = "% Facturación"
            .Columns(13).Width = 100
            .Columns(13).DefaultCellStyle.Format = "###.00 %"
            .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(13).Visible = False

            Dim numfilas As Integer

            numfilas = GrdConProd.RowCount 'cuenta las filas del DataGrid

            'recorre las filas del DataGrid
            For i = 0 To (numfilas - 1)
                GrdConProd.Rows(i).Cells(6).Style.BackColor = Color.LightGray
                'GrdConProd.Rows(i).Cells(6).Style.ForeColor = Color.White
            Next
        End With



    End Sub

        Private Sub CmbLineaIni_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbLineaIni.KeyPress
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End Sub

        Private Sub CmbLineaTer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbLineaTer.KeyPress
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End Sub

        Private Sub CmbNParteIni_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbNParteIni.KeyPress
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End Sub

        Private Sub CmbNParteTer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbNParteTer.KeyPress
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End Sub

        Private Sub CmbTurnoIni_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbTurnoIni.KeyPress
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End Sub

        Private Sub CmbTurnoTer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbTurnoTer.KeyPress
            e.KeyChar = Char.ToUpper(e.KeyChar)
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


                'With Me.GrdConProd
                '    .DataSource = DTRefacciones
                '    .ReadOnly = True
                '    'Color de Renglones en Grid
                '    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                '    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                '    .DefaultCellStyle.BackColor = Color.AliceBlue
                '    .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                '    'Propiedad para no mostrar el cuadro que se encuentra en la parte
                '    'Superior Izquierda del gridview
                '    .RowHeadersVisible = False
                '    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                '    .MultiSelect = False
                '    .AllowUserToAddRows = False
                '    'Color de linea del grid
                '    .Columns(0).HeaderText = "Clave Vendedor"
                '    .Columns(0).Width = 80
                '    .Columns(1).HeaderText = "Vendedor"
                '    .Columns(1).Width = 200
                '    ' .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '    .Columns(2).HeaderText = "Ventas Totales"
                '    .Columns(2).Width = 120
                '    .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
                '    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    '.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '    .Columns(3).HeaderText = "%"
                '    .Columns(3).Width = 100
                '    .Columns(3).DefaultCellStyle.Format = "###.00"
                '    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                '    .Columns(4).HeaderText = "Monto Devuelto"
                '    .Columns(4).Width = 120
                '    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    .Columns(4).DefaultCellStyle.Format = "###,###,###.00"

                '    .Columns(5).HeaderText = "% Dvol. Sobre Venta"
                '    .Columns(5).Width = 100
                '    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    .Columns(5).DefaultCellStyle.Format = "###.00"


                '    .Columns(6).HeaderText = "Ventas Netas"
                '    .Columns(6).Width = 120
                '    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    .Columns(6).DefaultCellStyle.Format = "###,###,###.00"


                '    .Columns(7).HeaderText = "Descuentos Pronto Pago"
                '    .Columns(7).Width = 120
                '    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    .Columns(7).DefaultCellStyle.Format = "###,###,###.00"

                '    .Columns(8).HeaderText = "% PP Sobre Venta"
                '    .Columns(8).Width = 100
                '    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    .Columns(8).DefaultCellStyle.Format = "###.00"
                '    '  .Columns(7).Visible = False

                '    .Columns(9).HeaderText = "Total Notas de Credito"
                '    .Columns(9).Width = 120
                '    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    .Columns(9).DefaultCellStyle.Format = "###,###,###.00"







                '    .Columns(10).HeaderText = "Cancelaciones Antes IVA"
                '    .Columns(10).Width = 120
                '    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    .Columns(10).DefaultCellStyle.Format = "###,###,###.00"
                '    .Columns(10).Visible = False

                '    .Columns(11).HeaderText = "% Canc. Desc."
                '    .Columns(11).Width = 100
                '    .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    .Columns(11).DefaultCellStyle.Format = "###.00"
                '    .Columns(11).Visible = False


                '    .Columns(12).HeaderText = "Facturación"
                '    .Columns(12).Width = 120
                '    .Columns(12).DefaultCellStyle.Format = "###,###,###.00"
                '    .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    .Columns(12).Visible = False

                '    .Columns(13).HeaderText = "% Facturación"
                '    .Columns(13).Width = 100
                '    .Columns(13).DefaultCellStyle.Format = "###.00"
                '    .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    .Columns(13).Visible = False



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
                'Dim cel11 As String = IIf(IsDBNull(Me.GrdConProd.Item(10, fila_dt).Value), 0, Me.GrdConProd.Item(10, fila_dt).Value)
                'Dim cel12 As String = IIf(IsDBNull(Me.GrdConProd.Item(11, fila_dt).Value), 0, Me.GrdConProd.Item(11, fila_dt).Value)

                fila_dt_excel = fila_dt + 4

                'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
                oSheet.range("A" & fila_dt_excel).value = cel1
                oSheet.range("B" & fila_dt_excel).value = cel2
                oSheet.range("C" & fila_dt_excel).value = cel3
                oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4, 2)
                oSheet.range("E" & fila_dt_excel).value = cel5
                oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
                oSheet.range("G" & fila_dt_excel).value = cel7
                oSheet.range("H" & fila_dt_excel).value = cel8

                oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
                oSheet.range("J" & fila_dt_excel).value = cel10
                'oSheet.range("K" & fila_dt_excel).value = cel11
                'oSheet.range("L" & fila_dt_excel).value = cel12
                'oSheet.Range("A3:J3").Cells.Interior.ColorIndex = 19

                '35,34,19
            Next

            'oSheet.columns("D:D").NumberFormat = "0"
            'oSheet.columns("D:D").HorizontalAlignment = -4131
            'oSheet.columns("D:D").VerticalAlignment = -4107

            'oSheet.columns("H:H").NumberFormat = "0"
            'oSheet.columns("H:H").HorizontalAlignment = -4131
            'oSheet.columns("H:H").VerticalAlignment = -4107

            ' para que el tamano de la columna tenga como minimo el maximo de sus textos
            oSheet.columns("A:N").entirecolumn.autofit()
            oSheet.range("A1").value = "Reporte de Ventas Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
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