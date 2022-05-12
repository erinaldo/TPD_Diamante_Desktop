Imports System.Data.SqlClient

Public Class NC_Facts
  Private dvDevs As New DataView
  Private dvPagos As New DataView
    Private dvNCFacts As New DataView
    Dim DtNCFacts As New DataTable

  Sub Buscar_NotasC()
    ' crear nueva conexión    
    Dim conexion As New SqlConnection(StrCon)
    ' abrir la conexión con la base de datos   
    conexion.Open()
    Dim comando As New SqlCommand

    Dim SQLTPD As String
        'ANTES 6 DE MAYO
        'SQLTPD = "SELECT DISTINCT T0.EDocPrefix AS Tipo,T0.DocNum AS NCDocSap,T0.DocDate AS FchNC,T0.DocTotal AS NCTotal,T0.Comments AS NCComent,"
        'SQLTPD &= "T0.CardCode AS IdClte,T2.CardName AS Nombre,CAST(T0.U_Factura AS NVARCHAR(MAX)) AS Facturas,T1.BaseDocNum,T1.BaseType "
        'SQLTPD &= "FROM ORIN T0 INNER JOIN RIN1 T1 ON T0.DocEntry = T1.DocEntry "
        'SQLTPD &= "LEFT JOIN OCRD T2 ON T0.CardCode=T2.CardCode "
        'SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "


        SQLTPD = "SELECT DISTINCT CASE "
        SQLTPD &= "WHEN T1.ItemCode = 'DESCUENTO P.P' THEN 'DSC' "
        SQLTPD &= "WHEN T1.ItemCode = 'AP_ANTICIPO' THEN 'ANT' "
        SQLTPD &= "WHEN T1.ItemCode <> 'DESCUENTO P.P' AND T3.ReportID IS NULL THEN 'CAN' "
        SQLTPD &= "WHEN T1.ItemCode <> 'DESCUENTO P.P' AND T3.ReportID IS NOT NULL THEN 'DEV' "
        SQLTPD &= "ELSE 'REV' END  AS Tipo,"
        SQLTPD &= "T0.DocNum AS NCDocSap,T0.DocDate AS FchNC,T0.DocTotal AS NCTotal,T0.Comments AS NCComent,"
        SQLTPD &= "T0.CardCode AS IdClte,T2.CardName AS Nombre,CAST(T0.U_Factura AS NVARCHAR(MAX)) AS Facturas,T1.BaseDocNum,T1.BaseType "
        SQLTPD &= "FROM ORIN T0 INNER JOIN RIN1 T1 ON T0.DocEntry = T1.DocEntry "
        SQLTPD &= "LEFT JOIN OCRD T2 ON T0.CardCode=T2.CardCode "
        SQLTPD &= "LEFT JOIN ECM2 T3 ON T0.DocEntry = T3.SrcObjAbs AND T3.SrcObjType = '14' "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer "

        '    Select Case CmbSerie.SelectedValue

        '  Case 42
        '    SQLTPD &= " AND T0.DocType  = 'S'  "

        '  Case 43
        '    SQLTPD &= " AND T0.DocType  = 'I' AND T0.EDocPrefix <> 'ND' "

        '  Case 49
        '    SQLTPD &= " AND T0.Series = " + CmbSerie.SelectedValue.ToString

        'End Select

        Select Case CmbSerie.SelectedValue

            Case 53
                SQLTPD &= " AND T0.Series = " + CmbSerie.SelectedValue.ToString

            Case 52
                SQLTPD &= " AND T0.Series = " + CmbSerie.SelectedValue.ToString

            Case 61
                SQLTPD &= " AND T0.Series = " + CmbSerie.SelectedValue.ToString

        End Select


        'If CmbSerie.SelectedValue <> 999 Then
        '    SQLTPD &= " AND T0.Series = " + CmbSerie.SelectedValue.ToString
        'End If

        Dim DrNCFacts As SqlDataReader
    With comando
      .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
      .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
      .CommandText = SQLTPD
      .Connection = conexion
      DrNCFacts = .ExecuteReader()
    End With

    ProcesaNCR(DrNCFacts)

        ''--------------------------------------------------------------------------------------------
        ''Nueva conexion para crear nueva table y realizar consultas
        '' crear nueva conexión    
        'Dim ConexNCFac As New SqlConnection(StrCon)
        'ConexNCFac.Open()
        'Dim Adaptador As New SqlDataAdapter()
        'Dim ComanNCFac As New SqlCommand

        'SQLTPD = "CREATE TABLE #NC_FAC_R (Tipo nvarchar(10), NCDocSap int, FchNC datetime, NCTotal Numeric(19,6),"
        'SQLTPD &= "NCComent nvarchar(254), IdClte nvarchar(15), Nombre nvarchar(100), Factura int)"

        ''With ComanNCFac
        ''  .Connection = ConexNCFac
        ''  .CommandText = SQLTPD
        ''  .ExecuteNonQuery()
        ''End With

        ''--------------------------------------------------------------------------------------------

        'Dim CadenaFact As String = ""
        'Dim UltDato As String = ""
        'Dim PosComa As Integer = 0
        'Dim NumFact As Integer = 0

        'Do While DrNCFacts.Read()
        '  If IsDBNull(DrNCFacts.Item("BaseDocNum")) Then

        '    CadenaFact = IIf(IsDBNull(DrNCFacts.Item("Facturas")), "", DrNCFacts.Item("Facturas"))

        '    While CadenaFact.Length > 0
        '      PosComa = InStr(CadenaFact, ",")

        '      If PosComa = 0 Then
        '        UltDato = CadenaFact
        '        CadenaFact = ""
        '      Else
        '        UltDato = CadenaFact.Substring(0, PosComa - 1)
        '        CadenaFact = CadenaFact.Substring(PosComa, CadenaFact.Length - PosComa)

        '      End If

        '      NumFact = Val(UltDato)

        '      If NumFact > 0 Then

        '        SQLTPD = "INSERT INTO #NC_FAC_R (Tipo, NCDocSap, FchNC, NCTotal, NCComent, IdClte, Nombre, Factura) VALUES ("
        '        SQLTPD &= "'"
        '        SQLTPD &= DrNCFacts.Item("Tipo")
        '        SQLTPD &= "', "
        '        SQLTPD &= DrNCFacts.Item("NCDocSap").ToString
        '        SQLTPD &= ", "
        '        SQLTPD &= "@FchNC "
        '        SQLTPD &= ", "
        '        SQLTPD &= DrNCFacts.Item("NCTotal").ToString
        '        SQLTPD &= ", '"
        '        SQLTPD &= DrNCFacts.Item("NCComent")
        '        SQLTPD &= "', '"
        '        SQLTPD &= DrNCFacts.Item("IdClte")
        '        SQLTPD &= "', '"
        '        SQLTPD &= DrNCFacts.Item("Nombre")
        '        SQLTPD &= "', "
        '        SQLTPD &= NumFact.ToString
        '        SQLTPD &= " )"

        '      End If

        '      'Parameters.Add("@Fecha", SqlDbType.SmallDateTime)
        '      'CmdMObra.Parameters("@Fecha").Value = vYear

        '      With ComanNCFac
        '        '.Parameters.Add("@FchNC", SqlDbType.SmallDateTime)
        '        '.Parameters("@FchNC").Value = Date.Now.Date
        '        .Parameters.AddWithValue("@FchNC", DrNCFacts.Item("FchNC"))
        '        .CommandText = SQLTPD
        '        .ExecuteNonQuery()
        '        .Parameters.Clear()
        '      End With

        '      'PosComa = substrin(',',@VCadenaFact)

        '    End While

        '  Else

        '    NumFact = DrNCFacts.Item("BaseDocNum")

        '    SQLTPD = "INSERT INTO #NC_FAC_R (Tipo, NCDocSap, FchNC, NCTotal, NCComent, IdClte, Nombre, Factura) VALUES ("
        '    SQLTPD &= "'"
        '    SQLTPD &= DrNCFacts.Item("Tipo")
        '    SQLTPD &= "', "
        '    SQLTPD &= DrNCFacts.Item("NCDocSap").ToString
        '    SQLTPD &= ", "
        '    SQLTPD &= "@FchNC "
        '    SQLTPD &= ", "
        '    SQLTPD &= DrNCFacts.Item("NCTotal").ToString
        '    SQLTPD &= ", '"
        '    SQLTPD &= DrNCFacts.Item("NCComent")
        '    SQLTPD &= "', '"
        '    SQLTPD &= DrNCFacts.Item("IdClte")
        '    SQLTPD &= "', '"
        '    SQLTPD &= DrNCFacts.Item("Nombre")
        '    SQLTPD &= "', "
        '    SQLTPD &= NumFact.ToString
        '    SQLTPD &= " )"

        '    With ComanNCFac
        '      .Parameters.AddWithValue("@FchNC", DrNCFacts.Item("FchNC"))
        '      .CommandText = SQLTPD
        '      .ExecuteNonQuery()
        '      .Parameters.Clear()
        '    End With


        '  End If
        '  'Se asignan el total de tickets que tiene el recurso

        '  'DrNCFacts.Item("Id_Recurso") Then
        '  ' DrNCFacts.Item("NumTicket")

        'Loop


        'With conexion
        '  If .State = ConnectionState.Open Then
        '    .Close()
        '  End If
        '  .Dispose()
        'End With

        ''NOTAS DE CREDITO CON INFORMACÓN DE MONTO DE FACTURAS,FECHA FACTURA
        ''TABLA TEMPORAL  #TOT_NCFAC
        'SQLTPD = "SELECT T0.Tipo, T0.NCDocSap, T0.FchNC, T0.NCComent, T0.Factura, T0.IdClte, T0.Nombre,"
        'SQLTPD &= "T1.DocDate AS FchFact, T1.DocTotal AS ImpFact, T0.NCTotal,T1.DocEntry "
        'SQLTPD &= "INTO #TOT_NCFAC "
        'SQLTPD &= "FROM #NC_FAC_R T0 LEFT JOIN OINV T1 ON T0.Factura = T1.DocNum  "


        ''SE BUSCAN LAS DEVOLUCIONES DE CADA FACTURA QUE TENGAN UNA FECHA ANTERIOR A LA NOTA DE CREDITO APLICADA
        ''TABLA TEMPORAL #FAC_DEVS
        'SQLTPD &= "SELECT DISTINCT T0.Tipo, T0.NCDocSap, T0.FchNC, T0.NCComent, T0.Factura, T0.IdClte, T0.Nombre,"
        'SQLTPD &= "T0.FchFact, T0.ImpFact, T0.NCTotal, T2.DocTotal, T2.DocNum, T2.DocDate "
        'SQLTPD &= "INTO #FAC_DEVS "
        'SQLTPD &= "FROM #TOT_NCFAC T0 "
        'SQLTPD &= "INNER JOIN RIN1 T1 ON T1.BaseDocNum = T0.Factura "
        'SQLTPD &= "INNER JOIN  ORIN T2 ON T2.DocEntry = T1.DocEntry "
        'SQLTPD &= "WHERE T2.DocType  = 'I' AND T2.DocDate <= T0.FchNC AND T0.NCDocSap <> T2.DocNum  "

        '''--T2.DocDate <= '20131007' AND  

        ''TOTAL DE DEVOLUCIONES POR FACTURA
        ''TABLA TEMPORAL #TOTDEV
        'SQLTPD &= "SELECT T0.NCDocSap, T0.Factura, SUM(T0.DocTotal) AS Devolucion "
        'SQLTPD &= "INTO #TOTDEV FROM #FAC_DEVS T0 GROUP BY T0.NCDocSap, T0.Factura  "


        ''PAGOS FACTURA
        ''TABLA TEMPORAL #PAGOS_FAC
        'SQLTPD &= "SELECT T0.Tipo,	T0.NCDocSap, T0.FchNC, T0.NCComent, T0.Factura, T0.IdClte, T0.Nombre,"
        'SQLTPD &= "T0.FchFact, T0.ImpFact, T0.NCTotal, T2.DocNum AS NumPago,T2.DocDate AS FchPago, T1.SumApplied AS PagoCIvaAp,"
        'SQLTPD &= "ROW_NUMBER() OVER(PARTITION BY T0.NCDocSap,T0.Factura ORDER BY T0.NCDocSap,T0.Factura,T2.DocDate DESC) AS OrdPago "
        'SQLTPD &= "INTO #PAGOS_FAC FROM #TOT_NCFAC T0 "
        'SQLTPD &= "INNER JOIN RCT2 T1 ON T1.DocEntry = T0.DocEntry AND T1.InvType = 13 "
        ''MODIFICACION: URIEL 2019-01-16 DEBIDO A CAMBIO DE LA BASE DE DATOS============================= INICIO
        ''ANTERIOR PARA FECHAS DE AGOSTO 2018 HACIA ATRAS
        ''SQLTPD &= "INNER JOIN ORCT T2 ON T2.DocNum = T1.DocNum AND T2.Canceled = 'N' "
        ''NUEVA 2019
        'SQLTPD &= "INNER JOIN ORCT T2 ON T2.DocEntry = T1.DocNum AND T2.Canceled = 'N' "
        ''MODIFICACION: URIEL 2019-01-16 DEBIDO A CAMBIO DE LA BASE DE DATOS============================= FIN
        'SQLTPD &= "ORDER BY T0.NCDocSap, T0.Factura, T2.DocDate DESC "


        ''SE OBTIENEN TOTAL FACTURADO Y DEVUELTO POR NOTA DE CREDITO
        ''TABLA TEMPORAL #T_FACDEV
        'SQLTPD &= "SELECT T0.NCDocSap, SUM(ImpFact) AS TotFacNC,1 AS FACT INTO #T_FACDEV FROM #TOT_NCFAC T0 GROUP BY T0.NCDocSap "
        'SQLTPD &= "UNION ALL "
        'SQLTPD &= "SELECT T1.NCDocSap,SUM(T1.DocTotal) AS TotFacNC,0 AS FACT  FROM #FAC_DEVS T1 GROUP BY T1.NCDocSap  "


        'SQLTPD &= "SELECT T0.Tipo,	T0.NCDocSap, T0.FchNC, T0.NCComent, T0.Factura, T0.IdClte, T0.Nombre,"
        'SQLTPD &= "T0.FchFact,T0.ImpFact,T1.Devolucion,"
        'SQLTPD &= "CASE WHEN T1.Devolucion IS NULL THEN T0.ImpFact ELSE T0.ImpFact - T1.Devolucion END AS ImpFNeto,"
        'SQLTPD &= "T2.TotFacNC AS TotFact,T3.TotFacNC AS TotDev,"
        'SQLTPD &= "CASE WHEN T3.TotFacNC IS NULL THEN T2.TotFacNC ELSE T2.TotFacNC - T3.TotFacNC END AS TotFNeto,"
        'SQLTPD &= "T4.FchPago,T4.PagoCIvaAp,DATEDIFF(DAY,T0.FchFact,T4.FchPago) AS DiasTrans,"
        'SQLTPD &= "T6.PymntGroup AS Cond_Pago,DATEDIFF(DAY,T0.FchFact,T4.FchPago) - T6.ExtraDays AS DiasAtraso,"
        'SQLTPD &= "T0.NCTotal,T0.NCTotal * 100 / "
        'SQLTPD &= "(CASE WHEN T3.TotFacNC IS NULL THEN T2.TotFacNC ELSE T2.TotFacNC - T3.TotFacNC END) AS PorNC,"
        'SQLTPD &= "t4.NumPago "
        'SQLTPD &= "FROM #TOT_NCFAC T0 "
        'SQLTPD &= "LEFT JOIN #TOTDEV T1 ON T0.NCDocSap = T0.NCDocSap AND T0.Factura = T1.Factura "
        'SQLTPD &= "LEFT JOIN #T_FACDEV T2 ON T2.NCDocSap = T0.NCDocSap AND T2.FACT = 1 "
        'SQLTPD &= "LEFT JOIN #T_FACDEV T3 ON T3.NCDocSap = T0.NCDocSap AND T3.FACT = 0 "
        'SQLTPD &= "LEFT JOIN #PAGOS_FAC T4 ON T4.NCDocSap = T0.NCDocSap AND T4.Factura = T0.Factura AND OrdPago = 1"
        'SQLTPD &= "INNER JOIN OCRD T5 ON T5.CardCode COLLATE Modern_Spanish_CI_AI = T0.IdClte "
        'SQLTPD &= "INNER JOIN OCTG T6 ON T6.GroupNum = T5.GroupNum  ORDER BY T0.NCDocSap,T0.Factura; "


        ''T0.NumPago,T0.FchPago, T0.PagoCIvaAp,T0.NCDocSap,T0.Factura
        ''DETALLE PAGOS
        'SQLTPD &= "SELECT T0.NumPago,T0.FchPago, T0.PagoCIvaAp,T0.NCDocSap,T0.Factura "
        'SQLTPD &= "FROM #PAGOS_FAC T0 WHERE OrdPago <> 1 ORDER BY T0.NCDocSap,T0.Factura,T0.FchPago DESC; "


        ''DETALLE DEVOLUCIONES
        'SQLTPD &= "SELECT T0.DocNum AS NCDev,T0.DocDate AS Fecha,T0.DocTotal AS Total,T0.NCDocSap,T0.Factura  "
        'SQLTPD &= "FROM #FAC_DEVS T0 ORDER BY T0.NCDocSap,T0.Factura,T0.DocDate DESC; "


        ''SQLTPD &= "DROP TABLE #NC_FAC_R  "
        ''SQLTPD &= "DROP TABLE #TOT_NCFAC"

        'SQLTPD &= "DROP TABLE #NC_FAC_R  "
        'SQLTPD &= "DROP TABLE #TOT_NCFAC  "
        'SQLTPD &= "DROP TABLE #FAC_DEVS  "
        'SQLTPD &= "DROP TABLE #TOTDEV  "
        'SQLTPD &= "DROP TABLE #PAGOS_FAC  "
        'SQLTPD &= "DROP TABLE #T_FACDEV  "


        'Dim DsDetNCS As New DataSet

        ''SQLTPD = "SELECT * FROM #NC_FAC_R  "
        ''SQLTPD &= "DROP TABLE #NC_FAC_R  "


        'With ComanNCFac
        '  '.Parameters.Clear()
        '  '.Parameters.AddWithValue("@FchNC", Me.DtpFechaTer.Value)
        '  .CommandText = SQLTPD
        '  .ExecuteNonQuery()
        'End With


        'With Adaptador
        '  .SelectCommand = ComanNCFac
        '  ' llenar el dataset  
        '  .Fill(DsDetNCS, "DsDetNCS")
        '  '.Fill(DtNCFacts)
        'End With

        'DsDetNCS.Tables(0).TableName = "NCSFacts"
        'DsDetNCS.Tables(1).TableName = "Pagos"
        'DsDetNCS.Tables(2).TableName = "Devs"



        'DtNCFacts = DsDetNCS.Tables("NCSFacts")


        'dvPagos.Table = DsDetNCS.Tables("Pagos")
        'dvDevs.Table = DsDetNCS.Tables("Devs")


        'ComanNCFac.Connection.Close()




        With Me.DgvNotasC
            '.DataSource = DtNCFacts
            .DataSource = dvNCFacts

            .ReadOnly = True
      'Color de Renglones en Grid
      .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
      .ColumnHeadersHeight = 50
            '39
            '.ScrollBars = ScrollBars.Vertical
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      '.RowHeadersVisible = False
      .RowHeadersWidth = 30
      '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
      '.MultiSelect = False
      .AllowUserToAddRows = False
      'Color de linea del grid
      .Columns(0).HeaderText = "Tipo Doc"
      .Columns(0).Width = 30
      .Columns(1).HeaderText = "Doc SAP"
            .Columns(1).Width = 60 '65

            .Columns(2).HeaderText = "Fecha"
            .Columns(2).Width = 63 '70

            .Columns(3).HeaderText = "Comentario"
            .Columns(3).Width = 149 '185 '234

            .Columns(4).HeaderText = "Factura SAP"
            .Columns(4).Width = 60 '65

            .Columns(5).HeaderText = "Cliente"
      .Columns(5).Width = 45

            .Columns(6).HeaderText = "Nombre"
            .Columns(6).Width = 141
            '.Columns(6).Width = 161

            .Columns(7).HeaderText = "Fecha Factura"
            .Columns(7).Width = 63

            .Columns(8).HeaderText = "Fecha Ven- cimiento"
            .Columns(8).Width = 63 '85

            .Columns(9).HeaderText = "$ Importe   Factura"
            .Columns(9).Width = 60
            .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(10).HeaderText = "$ Importe Devuelto"
            .Columns(10).Width = 60
            .Columns(10).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(11).HeaderText = "$ Importe Neto"
            .Columns(11).Width = 60
            .Columns(11).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(12).HeaderText = "$ Total Facturado"
            .Columns(12).Width = 72
            .Columns(12).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(13).HeaderText = "$ Total Devuelto"
            .Columns(13).Width = 65
            .Columns(13).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(14).HeaderText = "$ Total Neto"
            .Columns(14).Width = 72
            .Columns(14).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns(15).HeaderText = "Fecha Pago"
            .Columns(15).Width = 63 '70

            .Columns(16).HeaderText = "$ Pago Aplicado"
            .Columns(16).Width = 65
            .Columns(16).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(17).HeaderText = "Dias Trans"
            .Columns(17).Width = 40

            .Columns(18).HeaderText = "Credito"
            .Columns(18).Width = 47

            .Columns(19).HeaderText = "Dias Atraso"
            .Columns(19).Width = 40

            .Columns(20).HeaderText = "$ Valor NC"
            .Columns(20).Width = 65
            .Columns(20).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(21).HeaderText = "% NC"
            .Columns(21).Width = 45
            .Columns(21).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(22).HeaderText = "# Pago"
            .Columns(22).Width = 39

        End With


    'DgvPagos
    With Me.DgvPagos
            '.DataSource = dvPagos.Table
            .DataSource = dvPagos
            .ReadOnly = True
            'Color de Renglones en Grid
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
      .ColumnHeadersHeight = 50
      '39
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      '.RowHeadersVisible = False
      .RowHeadersWidth = 30
      '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
      '.MultiSelect = False


      .AllowUserToAddRows = False
      .Columns(0).HeaderText = "# Pago"
      .Columns(0).Width = 55

      .Columns(1).HeaderText = "Fecha Pago"
      .Columns(1).Width = 70

      .Columns(2).HeaderText = "$ Pago Aplicado"
      .Columns(2).Width = 80
      .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(3).Visible = False

      .Columns(4).Visible = False

    End With

    With Me.DgvDev
            '.DataSource = dvDevs.Table
            .DataSource = dvDevs
            '.DataSource = dvPagos
            .ReadOnly = True
      'Color de Renglones en Grid
      .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
      .ColumnHeadersHeight = 50
      '39
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      '.RowHeadersVisible = False
      .RowHeadersWidth = 30
      '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
      '.MultiSelect = False
      .AllowUserToAddRows = False

      .Columns(0).HeaderText = "# DocSap"
      .Columns(0).Width = 65

      .Columns(1).HeaderText = "Fecha Devolucion"
      .Columns(1).Width = 70

      .Columns(2).HeaderText = "$ Importe"
      .Columns(2).Width = 80
      .Columns(2).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(3).Visible = False

      .Columns(4).Visible = False
    End With

  End Sub

  Private Sub ProcesaNCR(DrNCFacts As SqlDataReader)
    Dim SQLTPD As String

    'Nueva conexion para crear nueva table y realizar consultas
    ' crear nueva conexión    
    Dim ConexNCFac As New SqlConnection(StrCon)
    ConexNCFac.Open()
    Dim Adaptador As New SqlDataAdapter()
    Dim ComanNCFac As New SqlCommand

    SQLTPD = "CREATE TABLE #NC_FAC_R (Tipo nvarchar(10), NCDocSap int, FchNC datetime, NCTotal Numeric(19,6),"
    SQLTPD &= "NCComent nvarchar(254), IdClte nvarchar(15), Nombre nvarchar(100), Factura int)"

    Dim CadenaFact As String = ""
    Dim UltDato As String = ""
    Dim PosComa As Integer = 0
    Dim NumFact As Integer = 0

    Do While DrNCFacts.Read()
      If IsDBNull(DrNCFacts.Item("BaseDocNum")) Then

        CadenaFact = IIf(IsDBNull(DrNCFacts.Item("Facturas")), "", DrNCFacts.Item("Facturas"))

        While CadenaFact.Length > 0
          PosComa = InStr(CadenaFact, ",")

          If PosComa = 0 Then
            UltDato = CadenaFact
            CadenaFact = ""
          Else
            UltDato = CadenaFact.Substring(0, PosComa - 1)
            CadenaFact = CadenaFact.Substring(PosComa, CadenaFact.Length - PosComa)

          End If

          NumFact = Val(UltDato)
          Dim FechaDate As Date = Convert.ToDateTime(DrNCFacts.Item("FchNC"))
          Dim Mes, dia As String
          If FechaDate.Month < 10 Then
            Mes = "0" & FechaDate.Month
          Else
            Mes = FechaDate.Month
          End If
          If FechaDate.Day < 10 Then
            dia = "0" & FechaDate.Day
          Else
            dia = FechaDate.Day
          End If

          If NumFact > 0 Then

            SQLTPD &= " INSERT INTO #NC_FAC_R (Tipo, NCDocSap, FchNC, NCTotal, NCComent, IdClte, Nombre, Factura) VALUES ("
            SQLTPD &= "'"
            SQLTPD &= DrNCFacts.Item("Tipo")
            SQLTPD &= "', "
            SQLTPD &= DrNCFacts.Item("NCDocSap").ToString
            SQLTPD &= ", "
            SQLTPD &= "'" & FechaDate.Year & "-" & Mes & "-" & dia & "'"
            SQLTPD &= ", "
            SQLTPD &= DrNCFacts.Item("NCTotal").ToString
            SQLTPD &= ", '"
            SQLTPD &= DrNCFacts.Item("NCComent")
            SQLTPD &= "', '"
            SQLTPD &= DrNCFacts.Item("IdClte")
            SQLTPD &= "', '"
            SQLTPD &= DrNCFacts.Item("Nombre")
            SQLTPD &= "', "
            SQLTPD &= NumFact.ToString
            SQLTPD &= " )"

          End If

          'Parameters.Add("@Fecha", SqlDbType.SmallDateTime)
          'CmdMObra.Parameters("@Fecha").Value = vYear

          'With ComanNCFac
          '  '.Parameters.Add("@FchNC", SqlDbType.SmallDateTime)
          '  '.Parameters("@FchNC").Value = Date.Now.Date
          '  .Parameters.AddWithValue("@FchNC", DrNCFacts.Item("FchNC"))
          '  .CommandText = SQLTPD
          '  .ExecuteNonQuery()
          '  .Parameters.Clear()
          'End With

          'PosComa = substrin(',',@VCadenaFact)

        End While

      Else

        NumFact = DrNCFacts.Item("BaseDocNum")
        Dim FechaDate As Date = Convert.ToDateTime(DrNCFacts.Item("FchNC"))
        Dim Mes, dia As String
        If FechaDate.Month < 10 Then
          Mes = "0" & FechaDate.Month
        Else
          Mes = FechaDate.Month
        End If
        If FechaDate.Day < 10 Then
          dia = "0" & FechaDate.Day
        Else
          dia = FechaDate.Day
        End If

        SQLTPD &= " INSERT INTO #NC_FAC_R (Tipo, NCDocSap, FchNC, NCTotal, NCComent, IdClte, Nombre, Factura) VALUES ("
        SQLTPD &= "'"
        SQLTPD &= DrNCFacts.Item("Tipo")
        SQLTPD &= "', "
        SQLTPD &= DrNCFacts.Item("NCDocSap").ToString
        SQLTPD &= ", "
        SQLTPD &= "'" & FechaDate.Year & "-" & Mes & "-" & dia & "'"
        SQLTPD &= ", "
        SQLTPD &= DrNCFacts.Item("NCTotal").ToString
        SQLTPD &= ", '"
        SQLTPD &= DrNCFacts.Item("NCComent")
        SQLTPD &= "', '"
        SQLTPD &= DrNCFacts.Item("IdClte")
        SQLTPD &= "', '"
        SQLTPD &= DrNCFacts.Item("Nombre")
        SQLTPD &= "', "
        SQLTPD &= NumFact.ToString
        SQLTPD &= " )"

        'With ComanNCFac
        '  .Parameters.AddWithValue("@FchNC", DrNCFacts.Item("FchNC"))
        '  .CommandText = SQLTPD
        '  .ExecuteNonQuery()
        '  .Parameters.Clear()
        'End With


      End If
      'Se asignan el total de tickets que tiene el recurso

      'DrNCFacts.Item("Id_Recurso") Then
      ' DrNCFacts.Item("NumTicket")

    Loop


    'With conexion
    '  If .State = ConnectionState.Open Then
    '    .Close()
    '  End If
    '  .Dispose()
    'End With

    'NOTAS DE CREDITO CON INFORMACÓN DE MONTO DE FACTURAS,FECHA FACTURA
    'TABLA TEMPORAL  #TOT_NCFAC
    SQLTPD &= " SELECT T0.Tipo, T0.NCDocSap, T0.FchNC, T0.NCComent, T0.Factura, T0.IdClte, T0.Nombre,"
        SQLTPD &= "T1.DocDate AS FchFact,T1.DocDueDate AS FchVenc, T1.DocTotal AS ImpFact, T0.NCTotal,T1.DocEntry "
        SQLTPD &= "INTO #TOT_NCFAC "
    SQLTPD &= "FROM #NC_FAC_R T0 LEFT JOIN OINV T1 ON T0.Factura = T1.DocNum  "


    'SE BUSCAN LAS DEVOLUCIONES DE CADA FACTURA QUE TENGAN UNA FECHA ANTERIOR A LA NOTA DE CREDITO APLICADA
    'TABLA TEMPORAL #FAC_DEVS
    SQLTPD &= "SELECT DISTINCT T0.Tipo, T0.NCDocSap, T0.FchNC, T0.NCComent, T0.Factura, T0.IdClte, T0.Nombre,"
        SQLTPD &= "T0.FchFact, T0.FchVenc,T0.ImpFact, T0.NCTotal, T2.DocTotal, T2.DocNum, T2.DocDate "
        SQLTPD &= "INTO #FAC_DEVS "
    SQLTPD &= "FROM #TOT_NCFAC T0 "
    SQLTPD &= "INNER JOIN RIN1 T1 ON T1.BaseDocNum = T0.Factura "
    SQLTPD &= "INNER JOIN  ORIN T2 ON T2.DocEntry = T1.DocEntry "
    SQLTPD &= "WHERE T2.DocType  = 'I' AND T2.DocDate <= T0.FchNC AND T0.NCDocSap <> T2.DocNum  "

    ''--T2.DocDate <= '20131007' AND  

    'TOTAL DE DEVOLUCIONES POR FACTURA
    'TABLA TEMPORAL #TOTDEV
    SQLTPD &= "SELECT T0.NCDocSap, T0.Factura, SUM(T0.DocTotal) AS Devolucion "
    SQLTPD &= "INTO #TOTDEV FROM #FAC_DEVS T0 GROUP BY T0.NCDocSap, T0.Factura  "


    'PAGOS FACTURA
    'TABLA TEMPORAL #PAGOS_FAC
    SQLTPD &= "SELECT T0.Tipo,	T0.NCDocSap, T0.FchNC, T0.NCComent, T0.Factura, T0.IdClte, T0.Nombre,"
        SQLTPD &= "T0.FchFact, T0.FchVenc,T0.ImpFact, T0.NCTotal, T2.DocNum AS NumPago,T2.DocDate AS FchPago, T1.SumApplied AS PagoCIvaAp,"
        SQLTPD &= "ROW_NUMBER() OVER(PARTITION BY T0.NCDocSap,T0.Factura ORDER BY T0.NCDocSap,T0.Factura,T2.DocDate DESC) AS OrdPago "
    SQLTPD &= "INTO #PAGOS_FAC FROM #TOT_NCFAC T0 "
    SQLTPD &= "INNER JOIN RCT2 T1 ON T1.DocEntry = T0.DocEntry AND T1.InvType = 13 "
    'MODIFICACION: URIEL 2019-01-16 DEBIDO A CAMBIO DE LA BASE DE DATOS============================= INICIO
    'ANTERIOR PARA FECHAS DE AGOSTO 2018 HACIA ATRAS
    'SQLTPD &= "INNER JOIN ORCT T2 ON T2.DocNum = T1.DocNum AND T2.Canceled = 'N' "
    'NUEVA 2019
    SQLTPD &= "INNER JOIN ORCT T2 ON T2.DocEntry = T1.DocNum AND T2.Canceled = 'N' "
    'MODIFICACION: URIEL 2019-01-16 DEBIDO A CAMBIO DE LA BASE DE DATOS============================= FIN
    SQLTPD &= "ORDER BY T0.NCDocSap, T0.Factura, T2.DocDate DESC "


    'SE OBTIENEN TOTAL FACTURADO Y DEVUELTO POR NOTA DE CREDITO
    'TABLA TEMPORAL #T_FACDEV
    SQLTPD &= "SELECT T0.NCDocSap, SUM(ImpFact) AS TotFacNC,1 AS FACT INTO #T_FACDEV FROM #TOT_NCFAC T0 GROUP BY T0.NCDocSap "
    SQLTPD &= "UNION ALL "
    SQLTPD &= "SELECT T1.NCDocSap,SUM(T1.DocTotal) AS TotFacNC,0 AS FACT  FROM #FAC_DEVS T1 GROUP BY T1.NCDocSap  "


    SQLTPD &= "SELECT T0.Tipo,	T0.NCDocSap, T0.FchNC, T0.NCComent, T0.Factura, T0.IdClte, T0.Nombre,"
        SQLTPD &= "T0.FchFact,T0.FchVenc,T0.ImpFact,T1.Devolucion,"
        SQLTPD &= "CASE WHEN T1.Devolucion IS NULL THEN T0.ImpFact ELSE T0.ImpFact - T1.Devolucion END AS ImpFNeto,"
    SQLTPD &= "T2.TotFacNC AS TotFact,T3.TotFacNC AS TotDev,"
    SQLTPD &= "CASE WHEN T3.TotFacNC IS NULL THEN T2.TotFacNC ELSE T2.TotFacNC - T3.TotFacNC END AS TotFNeto,"
    SQLTPD &= "T4.FchPago,T4.PagoCIvaAp,DATEDIFF(DAY,T0.FchFact,T4.FchPago) AS DiasTrans,"
        SQLTPD &= "T6.PymntGroup AS Cond_Pago,DATEDIFF(DAY,T0.FchVenc,T4.FchPago)  AS DiasAtraso,"
        'SQLTPD &= "T6.PymntGroup AS Cond_Pago,DATEDIFF(DAY,T0.FchVenc,T4.FchPago) - T6.ExtraDays AS DiasAtraso,"


        SQLTPD &= "T0.NCTotal,CASE WHEN  T0.Tipo = 'ANT' THEN 0 ELSE  T0.NCTotal * 100 / "
        SQLTPD &= "(CASE WHEN T3.TotFacNC IS NULL THEN T2.TotFacNC ELSE T2.TotFacNC - T3.TotFacNC END) END AS PorNC,"


        '    SQLTPD &= "T0.NCTotal,T0.NCTotal * 100 / "
        'SQLTPD &= "(CASE WHEN T3.TotFacNC IS NULL THEN T2.TotFacNC ELSE T2.TotFacNC - T3.TotFacNC END) AS PorNC,"

        SQLTPD &= "t4.NumPago "
    SQLTPD &= "FROM #TOT_NCFAC T0 "
    SQLTPD &= "LEFT JOIN #TOTDEV T1 ON T0.NCDocSap = T0.NCDocSap AND T0.Factura = T1.Factura "
    SQLTPD &= "LEFT JOIN #T_FACDEV T2 ON T2.NCDocSap = T0.NCDocSap AND T2.FACT = 1 "
    SQLTPD &= "LEFT JOIN #T_FACDEV T3 ON T3.NCDocSap = T0.NCDocSap AND T3.FACT = 0 "
    SQLTPD &= "LEFT JOIN #PAGOS_FAC T4 ON T4.NCDocSap = T0.NCDocSap AND T4.Factura = T0.Factura AND OrdPago = 1"
    SQLTPD &= "INNER JOIN OCRD T5 ON T5.CardCode COLLATE Modern_Spanish_CI_AI = T0.IdClte "
    SQLTPD &= "INNER JOIN OCTG T6 ON T6.GroupNum = T5.GroupNum  ORDER BY T0.NCDocSap,T0.Factura; "


    'T0.NumPago,T0.FchPago, T0.PagoCIvaAp,T0.NCDocSap,T0.Factura
    'DETALLE PAGOS
    SQLTPD &= "SELECT T0.NumPago,T0.FchPago, T0.PagoCIvaAp,T0.NCDocSap,T0.Factura "
    SQLTPD &= "FROM #PAGOS_FAC T0 WHERE OrdPago <> 1 ORDER BY T0.NCDocSap,T0.Factura,T0.FchPago DESC; "


    'DETALLE DEVOLUCIONES
    SQLTPD &= "SELECT T0.DocNum AS NCDev,T0.DocDate AS Fecha,T0.DocTotal AS Total,T0.NCDocSap,T0.Factura  "
    SQLTPD &= "FROM #FAC_DEVS T0 ORDER BY T0.NCDocSap,T0.Factura,T0.DocDate DESC; "


    'SQLTPD &= "DROP TABLE #NC_FAC_R  "
    'SQLTPD &= "DROP TABLE #TOT_NCFAC"

    SQLTPD &= "DROP TABLE #NC_FAC_R  "
    SQLTPD &= "DROP TABLE #TOT_NCFAC  "
    SQLTPD &= "DROP TABLE #FAC_DEVS  "
    SQLTPD &= "DROP TABLE #TOTDEV  "
    SQLTPD &= "DROP TABLE #PAGOS_FAC  "
    SQLTPD &= "DROP TABLE #T_FACDEV  "


    Dim DsDetNCS As New DataSet

    'SQLTPD = "SELECT * FROM #NC_FAC_R  "
    'SQLTPD &= "DROP TABLE #NC_FAC_R  "


    With ComanNCFac
      '.Parameters.Clear()
      '.Parameters.AddWithValue("@FchNC", Me.DtpFechaTer.Value)
      .CommandText = SQLTPD
      .CommandType = CommandType.Text
      .Connection = ConexNCFac
      .ExecuteNonQuery()
    End With


    With Adaptador
      .SelectCommand = ComanNCFac
      ' llenar el dataset  
      DsDetNCS.Clear()
      .Fill(DsDetNCS, "DsDetNCS")
      '.Fill(DtNCFacts)
    End With

    DsDetNCS.Tables(0).TableName = "NCSFacts"
    DsDetNCS.Tables(1).TableName = "Pagos"
    DsDetNCS.Tables(2).TableName = "Devs"

        'DtNCFacts = DsDetNCS.Tables("NCSFacts")

        dvNCFacts.Table = DsDetNCS.Tables("NCSFacts")

        dvPagos.Table = DsDetNCS.Tables("Pagos")
    dvDevs.Table = DsDetNCS.Tables("Devs")

    ComanNCFac.Connection.Close()
  End Sub


  Private Sub BtnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConsultar.Click
        If DtpFechaIni.Value.Date < "2013/10/02" Then
            MessageBox.Show("La fecha de inicio no puede ser menor al 2 de Octubre de 2013", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DtpFechaIni.Focus()
            Return
        End If


        Buscar_NotasC()
    filtrar_Pagos()
    filtrar_devs()
        Total_FactsNC()
        cmbFiltraDoc.Text = "Todos"
    End Sub

  Private Sub NC_Facts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        cmbFiltraDoc.Items.Add("Devoluciones")
        cmbFiltraDoc.Items.Add("Descuentos")
        cmbFiltraDoc.Items.Add("Cancelaciones")
        cmbFiltraDoc.Items.Add("Anticipos")
        cmbFiltraDoc.Items.Add("Todos")
        cmbFiltraDoc.Text = "Todos"

        'If UsrTPM = "MANAGER" Then
        '    DgvNotasC.Width = 1347
        '    DgvNotasC.Height = 430

        '    'Pagos
        '    Label2.Location = New Point(284, 494)
        '    DgvPagos.Location = New Point(285, 512)

        '    'Devoluciones
        '    Label11.Location = New Point(7, 494)
        '    DgvDev.Location = New Point(7, 512)

        '    'Totales
        '    Label4.Location = New Point(940, 496)
        '    TxtTotFact.Location = New Point(957, 494)
        '    TxtTotDev.Location = New Point(1030, 494)
        '    TxtTotNeto.Location = New Point(1095, 494)

        '    'SubTotales
        '    Label7.Location = New Point(891, 525)
        '    TxtSTotFact.Location = New Point(957, 523)
        '    TxtSTotDev.Location = New Point(1030, 523)
        '    TxtSTotNeto.Location = New Point(1095, 523)

        '    Me.WindowState = FormWindowState.Maximized
        'End If

        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
    Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")
    Dim ConsutaLista As String
    Dim DSetTablas As New DataSet

    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
            ConsutaLista = "select T0.Series,T0.SeriesName from NNM1 T0 WHERE T0.ObjectCode = 14 AND T0.Series <> 6"
            Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


      daAgte.Fill(DSetTablas, "Series")

      Dim filaAgte As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      filaAgte = DSetTablas.Tables("Series").NewRow

      'Agregamos los valores a los campos de la tabla
      filaAgte("SeriesName") = "TODOS"
      filaAgte("Series") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Series").Rows.Add(filaAgte)

      Me.CmbSerie.DataSource = DSetTablas.Tables("Series")
      Me.CmbSerie.DisplayMember = "SeriesName"
      Me.CmbSerie.ValueMember = "Series"
            Me.CmbSerie.SelectedValue = 999

        End Using
  End Sub

    'Private Sub BtnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    GridAExcel(DgvNotasC)
    'End Sub

    Sub filtrar_Pagos()
        Try
            'If DgvNotasC.Item(22, DgvNotasC.CurrentRow.Index).Value = 0 Then
            If IsDBNull(DgvNotasC.Item(22, DgvNotasC.CurrentRow.Index).Value) Then
                'dvPagos.RowFilter = String.Empty
                dvPagos.RowFilter = "NCDocSap = 0"
            Else
                'dvPagos.RowFilter = "NCDocSap = 2039811"

                dvPagos.RowFilter = "NCDocSap =" & DgvNotasC.Item(1, DgvNotasC.CurrentRow.Index).Value.ToString &
                        " AND Factura = " & DgvNotasC.Item(4, DgvNotasC.CurrentRow.Index).Value.ToString

                'dvPagos.RowFilter = "NCDocSap =" & DgvNotasC.Item(1, DgvNotasC.CurrentRow.Index).Value.ToString &
                '        " AND Factura = " & DgvNotasC.Item(4, DgvNotasC.CurrentRow.Index).Value.ToString
            End If

        Catch ex As Exception
        End Try
        'DgvPagos.Refresh()
        'dvPagos.RowFilter = "NCDocSap = 2039811"
    End Sub

    Private Sub DgvNotasC_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DgvNotasC.SelectionChanged
    filtrar_Pagos()
    filtrar_devs()
  End Sub
  Sub filtrar_devs()
        Try
            If IsDBNull(DgvNotasC.Item(9, DgvNotasC.CurrentRow.Index).Value) Then
                dvDevs.RowFilter = "NCDocSap = 0"

            Else

                dvDevs.RowFilter = "NCDocSap =" & DgvNotasC.Item(1, DgvNotasC.CurrentRow.Index).Value.ToString &
                        " AND Factura = " & DgvNotasC.Item(4, DgvNotasC.CurrentRow.Index).Value.ToString

            End If

        Catch ex As Exception
        End Try



        'DgvDev.Refresh()
        'IsDBNull(DgvNotasC.Item(9, DgvNotasC.CurrentRow.Index).Value)
    End Sub
  Sub Total_FactsNC()
    'Dim VTotFProv As Decimal = 0

    'For Each row As DataGridViewRow In Me.DgFactProv.Rows
    '    If row.Cells("RegSel").Value = False Then
    '        VTotFProv += row.Cells("SaldoPesos").Value
    '    End If

    'Next

    'TxtTotEnPesos.Text = Format(VTotFProv, "$ ##,###,###,###.00")


    Dim vTotFact As Decimal = 0
    Dim vTotDev As Decimal = 0
    Dim vTotNeto As Decimal = 0
    Dim vTotNC As Decimal = 0

    Dim vNumNc As Integer = 0

    For Each row As DataGridViewRow In DgvNotasC.Rows

      '    If row.Cells("RegSel").Value = False Then
      '        VTotFProv += row.Cells("SaldoPesos").Value
      '    End If

      'SQLTPD &= "T2.TotFacNC AS TotFact,T3.TotFacNC AS TotDev,"
      'SQLTPD &= "CASE WHEN T3.TotFacNC IS NULL THEN T2.TotFacNC ELSE T2.TotFacNC - T3.TotFacNC END AS TotFNeto,"
      'SQLTPD &= "T4.FchPago,T4.PagoCIvaAp,DATEDIFF(DAY,T0.FchFact,T4.FchPago) AS DiasTrans,"
      'SQLTPD &= "T6.PymntGroup AS Cond_Pago,DATEDIFF(DAY,T0.FchFact,T4.FchPago) - T6.ExtraDays AS DiasAtraso,"
      'SQLTPD &= "T0.NCTotal,T0.NCTotal * 100 / "
      'SQLTPD &= "(CASE WHEN T3.TotFacNC IS NULL THEN T2.TotFacNC ELSE T2.TotFacNC - T3.TotFacNC END) AS PorNC,"
      'SQLTPD &= "t4.NumPago "



      If row.Cells("NCDocSap").Value <> vNumNc Then

        vTotFact += row.Cells("TotFact").Value
        vTotDev += IIf(IsDBNull(row.Cells("TotDev").Value), 0, row.Cells("TotDev").Value)
        vTotNeto += row.Cells("TotFNeto").Value

        vTotNC += row.Cells("NCTotal").Value

      End If

      vNumNc = row.Cells("NCDocSap").Value

    Next


    TxtTotFact.Text = Format(vTotFact, "##,###,###,###.0")
    TxtTotDev.Text = Format(vTotDev, "##,###,###,###.0")
    TxtTotNeto.Text = Format(vTotNeto, "##,###,###,###.0")
    TxtTotNC.Text = Format(vTotNC, "##,###,###,###.0")


    If vTotNeto <> 0 Then

      TxtPor.Text = Format(vTotNC * 100 / vTotNeto, "###.#")
    Else
      TxtPor.Text = "0"
    End If


    TxtSTotFact.Text = Format(vTotFact / 1.16, "##,###,###,###.0")
    TxtSTotDev.Text = Format(vTotDev / 1.16, "##,###,###,###.0")
    TxtSTotNeto.Text = Format(vTotNeto / 1.16, "##,###,###,###.0")
    TxtSTotNC.Text = Format(vTotNC / 1.16, "##,###,###,###.0")

    'TxtTotEnPesos.Text = Format(VTotFProv, "$ ##,###,###,###.00")


  End Sub

    Private Sub BtnExcel_Click_1(sender As Object, e As EventArgs) Handles BtnExcel.Click
        GridAExcel(DgvNotasC)
    End Sub

    Private Sub cmbFiltraDoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFiltraDoc.SelectedIndexChanged

        'Cancelaciones
        Select Case cmbFiltraDoc.Text
            Case "Devoluciones"
                dvNCFacts.RowFilter = "Tipo = 'DEV'"
        ' The following is the only Case clause that evaluates to True.
            Case "Descuentos"
                dvNCFacts.RowFilter = "Tipo = 'DSC'"
            Case "Anticipos"
                dvNCFacts.RowFilter = "Tipo = 'ANT'"
            Case "Cancelaciones"
                dvNCFacts.RowFilter = "Tipo = 'CAN'"
            Case "Todos"
                dvNCFacts.RowFilter = String.Empty
            Case Else
                dvNCFacts.RowFilter = String.Empty
        End Select


        'SQLTPD = "SELECT DISTINCT CASE "
        'SQLTPD &= "WHEN T1.ItemCode = 'DESCUENTO P.P' THEN 'DSC' "
        'SQLTPD &= "WHEN T1.ItemCode = 'AP_ANTICIPO' THEN 'ANT' "
        'SQLTPD &= "WHEN T1.ItemCode <> 'DESCUENTO P.P' AND T3.ReportID IS NULL THEN 'CAN' "
        'SQLTPD &= "WHEN T1.ItemCode <> 'DESCUENTO P.P' AND T3.ReportID IS NOT NULL THEN 'DEV' "
        'SQLTPD &= "ELSE 'REV' END  AS Tipo,"
        'SQLTPD &= "T0.DocNum AS NCDocSap,T0.DocDate AS FchNC,T0.DocTotal AS NCTotal,T0.Comments AS NCComent,"


    End Sub
End Class