Imports System.Data.SqlClient
Public Class NotasC

    Sub Buscar_NotasC()
        ' crear nueva conexión    
        Dim conexion2 As New SqlConnection(StrCon)

        ' abrir la conexión con la base de datos   
        conexion2.Open()

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand


        Dim SQLTPD As String


        SQLTPD = "SELECT T0.EDocPrefix AS Tipo,T0.TransId AS TransNC,T0.DocNum AS NCDocSap,T0.EDocNum AS NCDFiscal,T0.DocDate AS FchNC,"
        SQLTPD &= "T0.DocTotal AS NCTotal,T2.ReconSum AS NCAplicada,T0.Comments AS NCComent,T0.JrnlMemo AS NCMemo,"
        SQLTPD &= "T1.ReconNum AS ReconNC,T1.ReconSum AS TotRecFact,T2.TransId AS TransFact,T3.DocNum AS FactSAP,"
        SQLTPD &= "T3.DocDate AS FchFact,T3.DocTotal AS TotFact,T3.Comments AS ComentFact,T3.JrnlMemo AS MemoFact,"
        SQLTPD &= "T0.CardCode AS IdClte,T0.CardName AS Nombre "
        SQLTPD &= "INTO #T_NC_SER FROM ORIN T0 INNER JOIN ITR1 T1 ON T0.TransId = T1.TransId "
        SQLTPD &= "INNER JOIN OITR T4 ON T1.ReconNum = T4.ReconNum AND T4.Canceled = 'N' "
        SQLTPD &= "LEFT JOIN ITR1 T2 ON T4.ReconNum = T2.ReconNum AND T2.IsCredit <> 'C' "
        SQLTPD &= "LEFT JOIN OINV T3 ON T2.TransId = T3.TransId "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND T2.ReconSum > 1.0"
        'SQLTPD &= "WHERE T0.DocType = 'S' AND T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer ORDER BY T0.DocNum "

        If CmbSerie.SelectedValue <> 999 Then
            SQLTPD &= " AND T0.Series = " + CmbSerie.SelectedValue.ToString
        End If


        SQLTPD &= " ORDER BY T0.DocNum "

        SQLTPD &= "SELECT T0.*,T1.ReconNum AS ReconFact,T1.ReconSum AS TotReconDet,T2.TransId AS RecRelNC,T2.SrcObjTyp AS TipoDoc,"
        SQLTPD &= "T3.DocNum AS RecNotaCred,T3.DocDate AS RecNCFch,T3.EDocNum,T4.DocNum AS NumPago,T4.DocDate AS FchPago,"
        SQLTPD &= "T3.EDocPrefix, T3.DocType,T3.Comments,T3.DocTotal AS RecNcTotal,"
        SQLTPD &= "CASE WHEN T3.DocType = 'I' THEN T3.DocTotal ELSE 0 END AS Devolucion,T4.JrnlMemo,T4.DocTotal AS PagoTot,"

        SQLTPD &= "ROW_NUMBER() OVER(PARTITION BY T0.TransNC,T0.FactSAP ORDER BY T4.DocDate DESC) AS OrdPago "

        SQLTPD &= "INTO #T_RECON_FACT "
        SQLTPD &= "FROM #T_NC_SER T0 "
        SQLTPD &= "LEFT JOIN ITR1 T1 ON T0.TransFact = T1.TransId AND T1.ReconNum < T0.ReconNC "
        SQLTPD &= "INNER JOIN OITR T5 ON T5.ReconNum = T1.ReconNum AND T5.Canceled = 'N' AND T5.ReconType <> 0"
        SQLTPD &= "LEFT JOIN ITR1 T2 ON T2.ReconNum = T1.ReconNum AND T2.IsCredit = 'C' "
        SQLTPD &= "LEFT JOIN ORIN T3 ON  T3.TransId = T2.TransId AND T2.SrcObjTyp <> 24 "
        SQLTPD &= "LEFT JOIN ORCT T4 ON  T4.TransId = T2.TransId AND T2.SrcObjTyp <> 14 AND T4.Canceled = 'N' ORDER BY T0.NCDocSap  "


        SQLTPD &= "SELECT T0.TransFact, SUM(T0.Devolucion) AS TotDev INTO #NC_DEV FROM #T_RECON_FACT T0 GROUP BY TransFact "

        'SQLTPD &= "SELECT TransFact,NumPago,FchPago,TotReconDet INTO #PAG_TRANS FROM #T_RECON_FACT "
        'SQLTPD &= "WHERE NumPago is not null group by TransFact,NumPago,FchPago,TotReconDet "


        SQLTPD &= "SELECT T0.Tipo,T0.NCDocSap,T0.FchNC,T0.NCComent,T0.FactSAP,T0.IdClte AS Cliente,T0.Nombre,T0.FchFact,"
        SQLTPD &= "T0.TotFact AS ImpFact,CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END AS Devolucion,"
        SQLTPD &= "T0.TotFact - CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END AS FactMenosD,"
        SQLTPD &= "T1.FchPago,TotReconDet AS PagoA,DATEDIFF(DAY,T0.FchFact,T1.FchPago) AS DiasTrans,T4.PymntGroup AS Cond_Pago,"
        SQLTPD &= "DATEDIFF(DAY,T0.FchFact,T1.FchPago) - T4.ExtraDays AS DiasAtraso,T0.NCTotal AS ValorNC,T0.NCAplicada,"
        SQLTPD &= "T0.NCAplicada * 100 / (T0.TotFact - CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END) AS PorNC,"
        SQLTPD &= "T1.NumPago, T0.ReconNC "
        SQLTPD &= "FROM #T_NC_SER T0 "
        SQLTPD &= "LEFT JOIN #T_RECON_FACT T1 ON T0.TransNC = T1.TransNC AND T0.FactSAP = T1.FactSAP AND OrdPago = 1 "
        SQLTPD &= "LEFT JOIN #NC_DEV T2 ON T0.TransFact = T2.TransFact "
        SQLTPD &= "LEFT JOIN OCRD T3 ON T0.IdClte = T3.CardCode "
        SQLTPD &= "LEFT JOIN OCTG T4 ON T3.GroupNum = T4.GroupNum  "


        SQLTPD &= "DROP TABLE #T_NC_SER "
        SQLTPD &= "DROP TABLE #T_RECON_FACT "
        SQLTPD &= "DROP TABLE #NC_DEV "
        'SQLTPD &= "DROP TABLE #PAG_TRANS "

        'SQLTPD &= "SELECT T0.*,T1.ReconNum AS DetReconFact,T1.ReconSum AS TotReconDet,T2.TransId AS RecRelNC,T2.SrcObjTyp AS TipoDoc,"
        'SQLTPD &= "T3.DocNum AS RecNotaCred,T3.DocDate AS RecNCFch,T3.EDocNum,T4.DocNum AS NumPago,T4.DocDate AS FchPago,"
        'SQLTPD &= "T3.EDocPrefix, T3.DocType,T3.Comments,T3.DocTotal AS RecNcTotal,"
        'SQLTPD &= "CASE WHEN T3.DocType = 'I' THEN T3.DocTotal ELSE 0 END AS Devolucion,T4.JrnlMemo,T4.DocTotal AS PagoTot,"
        'SQLTPD &= "ROW_NUMBER() OVER(PARTITION BY T4.DocNum ORDER BY T4.DocDate) as Enumpago "
        'SQLTPD &= "INTO #T_RECON_FACT "
        'SQLTPD &= "FROM #T_NC_SER T0 "
        'SQLTPD &= "LEFT JOIN ITR1 T1 ON T0.TransFact = T1.TransId AND T1.ReconNum < T0.ReconFact "
        'SQLTPD &= "INNER JOIN OITR T5 ON T5.ReconNum = T1.ReconNum AND T5.Canceled = 'N' "
        'SQLTPD &= "LEFT JOIN ITR1 T2 ON T2.ReconNum = T1.ReconNum AND T2.IsCredit = 'C' "
        'SQLTPD &= "LEFT JOIN ORIN T3 ON T3.TransId = T2.TransId AND T2.SrcObjTyp <> 24 "
        'SQLTPD &= "LEFT JOIN ORCT T4 ON T4.TransId = T2.TransId AND T2.SrcObjTyp <> 14 AND T4.Canceled = 'N' ORDER BY T0.NCDocSap "


        'SQLTPD &= "SELECT T0.TransFact, SUM(T0.Devolucion) AS TotDev INTO #NC_DEV FROM #T_RECON_FACT T0 GROUP BY TransFact "


        ''SQLTPD &= "SELECT T0.NCDocSap,T0.FchNC,T0.NCComent,T0.FactSAP,T0.IdClte AS Cliente,T0.Nombre,T0.FchFact,T0.TotFact AS ImpFact,"
        ''SQLTPD &= "T2.TotDev AS Devolucion,T0.TotFact - T2.TotDev AS FactMenosD,"
        ''SQLTPD &= "T1.FchPago,DATEDIFF(DAY,T1.FchPago,T0.FchFact) AS DiasTrans,T0.NCTotal AS ValorNC,T0.NCAplicada,"
        ''SQLTPD &= "T0.NCAplicada * 100 / (T0.TotFact - T2.TotDev) AS PorNC,T1.NumPago,T0.ReconFact "
        ''SQLTPD &= "FROM #T_NC_SER T0 "
        ''SQLTPD &= "LEFT JOIN #T_RECON_FACT T1 ON T0.TransFact = T1.TransFact AND T1.NumPago IS NOT NULL "
        ''SQLTPD &= "LEFT JOIN #NC_DEV T2 ON T0.TransFact = T2.TransFact  "


        ''SQLTPD &= "SELECT T0.Tipo,T0.NCDocSap,T0.FchNC,T0.NCComent,T0.FactSAP,T0.IdClte AS Cliente,T0.Nombre,T0.FchFact,"
        ''SQLTPD &= "T0.TotFact AS ImpFact,CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END AS Devolucion,"
        ''SQLTPD &= "T0.TotFact - CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END AS FactMenosD,"
        ''SQLTPD &= "T1.FchPago,DATEDIFF(DAY,T0.FchFact,T1.FchPago) AS DiasTrans,T4.PymntGroup AS Cond_Pago,"
        ''SQLTPD &= "DATEDIFF(DAY,T0.FchFact,T1.FchPago) - T4.ExtraDays AS DiasAtraso,T0.NCTotal AS ValorNC,T0.NCAplicada,"
        ''SQLTPD &= "T0.NCAplicada * 100 / (T0.TotFact - CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END) AS PorNC,"
        ''SQLTPD &= "T1.NumPago, T0.ReconFact "
        ''SQLTPD &= "FROM #T_NC_SER T0 "
        ''SQLTPD &= "LEFT JOIN #T_RECON_FACT T1 ON T0.TransFact = T1.TransFact AND T1.NumPago IS NOT NULL "
        ''SQLTPD &= "LEFT JOIN #NC_DEV T2 ON T0.TransFact = T2.TransFact "
        ''SQLTPD &= "LEFT JOIN OCRD T3 ON T0.IdClte = T3.CardCode "
        ''SQLTPD &= "LEFT JOIN OCTG T4 ON T3.GroupNum = T4.GroupNum  "


        'SQLTPD &= "SELECT T0.Tipo,T0.NCDocSap,T0.FchNC,T0.NCComent,T0.FactSAP,T0.IdClte AS Cliente,T0.Nombre,T0.FchFact,"
        'SQLTPD &= "T0.TotFact AS ImpFact,CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END AS Devolucion,"
        'SQLTPD &= "T0.TotFact - CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END AS FactMenosD,"
        'SQLTPD &= "T1.FchPago,DATEDIFF(DAY,T0.FchFact,T1.FchPago) AS DiasTrans,T4.PymntGroup AS Cond_Pago,"
        'SQLTPD &= "DATEDIFF(DAY,T0.FchFact,T1.FchPago) - T4.ExtraDays AS DiasAtraso,T0.NCTotal AS ValorNC,T0.NCAplicada,"
        'SQLTPD &= "T0.NCAplicada * 100 / (T0.TotFact - CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END) AS PorNC,"
        'SQLTPD &= "T1.NumPago, T0.ReconFact "
        'SQLTPD &= "FROM #T_NC_SER T0 "
        'SQLTPD &= "LEFT JOIN #T_RECON_FACT T1 ON T0.TransFact = T1.TransFact AND T1.NumPago IS NOT NULL "
        ''SQLTPD &= "LEFT JOIN #T_RECON_FACT T1 ON T0.TransFact = T1.TransFact AND T1.NumPago IS NOT NULL AND T1.Enumpago = 1 "
        'SQLTPD &= "LEFT JOIN #NC_DEV T2 ON T0.TransFact = T2.TransFact "
        'SQLTPD &= "LEFT JOIN OCRD T3 ON T0.IdClte = T3.CardCode "
        'SQLTPD &= "LEFT JOIN OCTG T4 ON T3.GroupNum = T4.GroupNum  "



        'SQLTPD &= "DROP TABLE #T_NC_SER "
        'SQLTPD &= "DROP TABLE #T_RECON_FACT "
        'SQLTPD &= "DROP TABLE #NC_DEV "


        With comando
            'If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
            '    .Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
            'End If

            .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
            .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)


            ' Asignar el sql para seleccionar los datos de la tabla Maestro   
            .CommandText = SQLTPD
            .Connection = conexion2
        End With

        Dim DtNCred As New DataTable

        With Adaptador
            .SelectCommand = comando
            ' llenar el dataset   
            .Fill(DtNCred)
        End With

        With Me.DgvNotasC
            .DataSource = DtNCred
            .ReadOnly = True
            'Color de Renglones en Grid
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 39
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
            .Columns(1).Width = 65
            '' .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns(2).HeaderText = "Ventas Totales"
            '.Columns(2).Width = 120

            .Columns(2).HeaderText = "Fecha"
            .Columns(2).Width = 70

            '.Columns(2).DefaultCellStyle.Format = "###,###,###.00"
            '.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            ''.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).HeaderText = "Comentario"
            .Columns(3).Width = 264
            '.Columns(3).DefaultCellStyle.Format = "###.00"
            '.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(4).HeaderText = "Factura SAP"
            .Columns(4).Width = 65

            '.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(4).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(5).HeaderText = "Cliente"
            .Columns(5).Width = 45
            '.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(5).DefaultCellStyle.Format = "###.00"


            .Columns(6).HeaderText = "Nombre"
            .Columns(6).Width = 185
            '.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(6).DefaultCellStyle.Format = "###,###,###.00"


            .Columns(7).HeaderText = "Fecha Factura"
            .Columns(7).Width = 70
            '.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(7).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(8).HeaderText = "$ Importe   Factura"
            .Columns(8).Width = 70
            .Columns(8).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(8).DefaultCellStyle.Format = "###.00"
            ''  .Columns(7).Visible = False

            .Columns(9).HeaderText = "$Devolución"
            .Columns(9).Width = 72
            .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(9).DefaultCellStyle.Format = "###,###,###.00"


            .Columns(10).HeaderText = "$ Total"
            .Columns(10).Width = 70
            .Columns(10).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(10).DefaultCellStyle.Format = "###,###,###.00"
            '.Columns(10).Visible = False

            .Columns(11).HeaderText = "Fecha Pago"
            .Columns(11).Width = 70
            '.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(11).DefaultCellStyle.Format = "###.00"
            '.Columns(11).Visible = False


            .Columns(12).HeaderText = "$ Pago Aplicado"
            .Columns(12).Width = 70
            .Columns(12).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(12).DefaultCellStyle.Format = "###,###,###.00"
            '.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(12).Visible = False

            .Columns(13).HeaderText = "Dias Trans"
            .Columns(13).Width = 40

            .Columns(14).HeaderText = "Credito"
            .Columns(14).Width = 42

            .Columns(15).HeaderText = "Dias Atraso"
            .Columns(15).Width = 40

            .Columns(16).HeaderText = "$ Valor NC"
            .Columns(16).Width = 70
            .Columns(16).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(17).HeaderText = "$ NC Aplicada"
            .Columns(17).Width = 70
            .Columns(17).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(18).HeaderText = "% NC"
            .Columns(18).Width = 35
            .Columns(18).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(19).HeaderText = "# Pago"
            .Columns(19).Width = 40


            .Columns(20).HeaderText = "# RecNC"
            .Columns(20).Width = 47




            '.Columns(13).DefaultCellStyle.Format = "###.00"
            '.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(13).Visible = False
        End With

        With conexion2
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With

    End Sub

    Private Sub NotasC_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")
        Dim ConsutaLista As String
        Dim DSetTablas As New DataSet

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
            ConsutaLista = "select T0.Series,T0.SeriesName from NNM1 T0 WHERE T0.ObjectCode = 14"
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
            Me.CmbSerie.SelectedValue = 42

        End Using
    End Sub

    Private Sub BtnConsultar_Click(sender As System.Object, e As System.EventArgs) Handles BtnConsultar.Click
        BtnConsultar.Enabled = False
        Buscar_NotasC()
        BtnConsultar.Enabled = True
    End Sub

    Private Sub BtnExcel_Click(sender As System.Object, e As System.EventArgs) Handles BtnExcel.Click
        GridAExcel(DgvNotasC)
    End Sub

    Private Sub DgvNotasC_RowPrePaint(sender As System.Object, e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DgvNotasC.RowPrePaint
        'DgvNotasC.Rows(e.RowIndex).Cells("PorNC").Style.BackColor = Color.Red
        'DgvNotasC.Rows(e.RowIndex).Cells("PorNC").Style.ForeColor = Color.White
        'LightSteelBlue

        '.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue


        DgvNotasC.Rows(e.RowIndex).Cells("PorNC").Style.BackColor = Color.Gainsboro
        DgvNotasC.Rows(e.RowIndex).Cells("PorNC").Style.ForeColor = Color.Black

        DgvNotasC.Rows(e.RowIndex).Cells("NCAplicada").Style.BackColor = Color.Gainsboro
        DgvNotasC.Rows(e.RowIndex).Cells("NCAplicada").Style.ForeColor = Color.Black

        DgvNotasC.Rows(e.RowIndex).Cells("FactMenosD").Style.BackColor = Color.Gainsboro
        DgvNotasC.Rows(e.RowIndex).Cells("FactMenosD").Style.ForeColor = Color.Black



        'SQLTPD &= "SELECT T0.Tipo,T0.NCDocSap,T0.FchNC,T0.NCComent,T0.FactSAP,T0.IdClte AS Cliente,T0.Nombre,T0.FchFact,"
        'SQLTPD &= "T0.TotFact AS ImpFact,CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END AS Devolucion,"
        'SQLTPD &= "T0.TotFact - CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END AS FactMenosD,"
        'SQLTPD &= "T1.FchPago,TotReconDet AS PagoA,DATEDIFF(DAY,T0.FchFact,T1.FchPago) AS DiasTrans,T4.PymntGroup AS Cond_Pago,"
        'SQLTPD &= "DATEDIFF(DAY,T0.FchFact,T1.FchPago) - T4.ExtraDays AS DiasAtraso,T0.NCTotal AS ValorNC,T0.NCAplicada,"
        'SQLTPD &= "T0.NCAplicada * 100 / (T0.TotFact - CASE WHEN T2.TotDev IS NULL THEN 0 ELSE T2.TotDev END) AS PorNC,"
        'SQLTPD &= "T1.NumPago, T0.ReconNC "


    End Sub
End Class