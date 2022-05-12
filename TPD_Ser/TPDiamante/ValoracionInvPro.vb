
Imports System.Data.SqlClient

Public Class ValoracionInvPro

    Dim DvArticulo As New DataView
    Dim DvClte As New DataView
    Dim DvValInv As New DataView
    Dim DvBO As New DataView



    Private Sub ValoracionInvPro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim FchInicio As DateTime
        'FchInicio = DateAdd(DateInterval.Month, -1, Date.Now)
        'Me.DtpFechaIni.Value = Format(FchInicio, "dd/MM/yyyy")
        'Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")


        'If VEsAgente = 1 Then
        '    Me.WindowState = FormWindowState.Normal
        '    Me.DGLineas.Width = 1047
        '    Me.DGLineas.Height = 512
        '    Me.Size = New System.Drawing.Size(1065, 551)
        'End If

        'If UsrTPM = "RROBLES" Or UsrTPM = "VVERGARA" _
        'Or UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" _
        'Or UsrTPM = "VENTAS5" Or UsrTPM = "ASTRIDY" Or UsrTPM = "CSANTOS" Then
        '    Me.cmbAlmacen.Enabled = False

        'ElseIf UsrTPM = "ACASTRO" Or UsrTPM = "JSANCHEZ" Or UsrTPM = "RMERCADO" _
        'Or UsrTPM = "VENTAS4" Or UsrTPM = "RJIMENEZ" Or UsrTPM = "LCEBALLOS" Then

        '    Me.CmbAgteVta.SelectedValue = vCodAgte
        '    Me.CmbAgteVta.Enabled = False
        '    Me.cmbAlmacen.Enabled = False
        '    BuscaClientes()
        '    Me.CmbCliente.Focus()
        'End If

        CKBCli.Checked = True

        Dim FchInicio As DateTime
        FchInicio = DateAdd(DateInterval.Month, -1, Date.Now)
        'Me.DtpFechaIni.Value = Format(FchInicio, "dd/MM/yyyy")
        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")


        Dim ConsutaLista As String


        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

            'mllenaComboAlmacen(SqlConnection)

            ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
            Dim daGArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            Dim DSetTablas As New DataSet
            daGArticulo.Fill(DSetTablas, "GArticulos")

            Dim fila As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            fila = DSetTablas.Tables("GArticulos").NewRow

            'Agregamos los valores a los campos de la tabla
            fila("ItmsGrpNam") = "TODOS"
            fila("ItmsGrpCod") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("GArticulos").Rows.Add(fila)

            'DvArticulo = DSetTablas.Tables("GArticulos")

            Me.CmbGrupoArticulo.DataSource = DSetTablas.Tables("GArticulos")
            Me.CmbGrupoArticulo.DisplayMember = "ItmsGrpNam"
            Me.CmbGrupoArticulo.ValueMember = "ItmsGrpCod"
            Me.CmbGrupoArticulo.SelectedValue = 999


            '''''*******************************
            Try
                Dim da As New SqlDataAdapter("SELECT GroupCode , GroupName " +
                                             "FROM OCRG with (nolock) " +
                                             "WHERE GroupType = 'C' ORDER BY GroupName ", SqlConnection)

                Dim ds As New DataSet
                da.Fill(ds)
                ds.Tables(0).Rows.Add(0, "TODOS")
                Me.cmbAlmacen.DataSource = ds.Tables(0)
                Me.cmbAlmacen.DisplayMember = "GroupName"
                Me.cmbAlmacen.ValueMember = "GroupCode"

                'If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Or UsrTPM = "DDORANTES" Then

                Me.cmbAlmacen.SelectedValue = 0

                'Else

                'If AlmTPM = "01" Then
                '    Me.cmbAlmacen.SelectedValue = "100"
                'ElseIf AlmTPM = "03" Then
                '    Me.cmbAlmacen.SelectedValue = "102"
                'ElseIf AlmTPM = 100 Then
                '    Me.cmbAlmacen.SelectedValue = "103"
                'End If


                'End If


            Catch ex As Exception

            End Try


            '''''********************************
            '-----------------------------------------------------
            ConsutaLista = "SELECT ItemCode,ItemName,ItmsGrpCod FROM OITM ORDER BY ItemCode"
            Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


            daArticulo.Fill(DSetTablas, "Articulos")

            Dim filaArticulo As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaArticulo = DSetTablas.Tables("Articulos").NewRow

            'Agregamos los valores a los campos de la tabla
            filaArticulo("ItemName") = "TODOS"
            filaArticulo("ItemCode") = "TODOS"
            filaArticulo("ItmsGrpCod") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Articulos").Rows.Add(filaArticulo)

            DvArticulo.Table = DSetTablas.Tables("Articulos")

            Me.CmbArticulo.DataSource = DvArticulo
            Me.CmbArticulo.DisplayMember = "ItemCode"
            Me.CmbArticulo.ValueMember = "ItemCode"
            Me.CmbArticulo.SelectedValue = "TODOS"

        End Using
    End Sub

    Sub BuscaArticulos()
        If CmbGrupoArticulo.SelectedValue Is Nothing Or CmbGrupoArticulo.SelectedValue = 999 Then
            DvArticulo.RowFilter = String.Empty
            CmbArticulo.SelectedValue = "TODOS"

        Else
            DvArticulo.RowFilter = "ItmsGrpCod = " & Trim(Me.CmbGrupoArticulo.SelectedValue.ToString) & " OR ItmsGrpCod = 999"
        End If
    End Sub


    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Ejecutar_Consulta()

        'Try
        '    DvBO.RowFilter = "id = '" & DGLineas.Item(0, DGLineas.CurrentRow.Index).Value & "' and itmsgrpcod=" & DGLineas.Item(2, DGLineas.CurrentRow.Index).Value

        'Catch ex As Exception
        '    'MsgBox(ex.Message)
        'End Try
        'Try
        '    DGLineas.CurrentCell = DGLineas.Rows(0).Cells(2)
        'Catch ex As Exception

        'End Try
        BuscaDetalle()

    End Sub

    Private Sub Ejecutar_Consulta()

        DGLineas.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES

        Dim Consulta As String = ""
        Dim strcadena As String = ""
        Dim CTabla As String = ""
        Dim DTMObra As New DataTable
        Dim DTProb As New DataTable
        Dim vAlm As Integer = 0

        'TxtAlmacen.Text = cmbAlmacen.Text
        'TxtAlmacen.Visible = True


        Consulta &= "DECLARE @NumDias DECIMAL(15,5) = (SELECT CASE WHEN DATEDIFF(DD,@FechaIni,@FechaTer) = 0 THEN 1 ELSE DATEDIFF(DD,@FechaIni,@FechaTer) END) "

        Consulta &= " DECLARE @PromMeses DECIMAL(15,5) = @numdias/30 "

        '--SELECT @PromMeses

        '--Se obtiene valor de INVENTARIO

        Consulta &= " SELECT T0.WhsCode AS 'Id',T0.WhsName AS 'Almacen',T4.ItmsGrpCod,T4.ItmsGrpNam,T3.ItemCode,T3.ItemName, "
        Consulta &= " SUM(T1.OnHand*T2.Price) AS 'Valor' "
        Consulta &= " INTO #ValInv"
        Consulta &= " FROM SBO_TPD.dbo.OWHS T0"
        Consulta &= " LEFT JOIN SBO_TPD.dbo.OITW T1 ON T0.WhsCode = T1.WhsCode"
        Consulta &= " LEFT JOIN SBO_TPD.dbo.ITM1 T2 ON T1.ItemCode = T2.ItemCode AND T2.PriceList = 11"
        Consulta &= " LEFT JOIN SBO_TPD.dbo.OITM T3 ON T2.ItemCode = T3.ItemCode"
        Consulta &= " LEFT JOIN SBO_TPD.dbo.OITB T4 ON T3.ItmsGrpCod = T4.ItmsGrpCod"
        Consulta &= " WHERE  "

        If cmbAlmacen.SelectedValue = 100 Then
            Consulta &= " T0.WhsCode = '01' "
        ElseIf cmbAlmacen.SelectedValue = 102 Then
            Consulta &= " T0.WhsCode = '03' "
        ElseIf cmbAlmacen.SelectedValue = 103 Then
            Consulta &= " T0.WhsCode = '07' "
        Else
            Consulta &= " T0.WhsCode IN ('01','03','07') "
        End If

        If CmbGrupoArticulo.SelectedValue <> 999 Then
            Consulta &= " AND T4.ItmsGrpCod = " & CmbGrupoArticulo.SelectedValue
        End If

        If CmbArticulo.SelectedValue <> "TODOS" Then
            Consulta &= " AND T3.ItemCode = '" & CmbArticulo.SelectedValue & "'"
        End If


        '--AND T4.ItmsGrpCod=122
        '--AND T1.ItemCode = '3030-DP'
        Consulta &= " GROUP BY T0.WhsName,T0.WhsCode,T4.ItmsGrpCod,T4.ItmsGrpNam,T3.ItemCode,T3.ItemName"
        Consulta &= " ORDER BY T0.WhsCode,t4.ItmsGrpCod,ItemCode"
        '--FIN VALOR INVENTARIO

        '--SE OBTIENEN VENTAS POR ALMACEN, AGENTE, LINEA Y ARTICULO
        Consulta &= " SELECT TipoDoc,Docnum,docdate,slpcode,slpname,importe,itemcode,itemname,"
        Consulta &= " itmsgrpcod,itmsgrpnam,groupcode,groupname INTO #VtasNet FROM("
        Consulta &= " SELECT 'FACTURA' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, "
        Consulta &= " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal ELSE T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100 END AS Importe, "
        Consulta &= " CASE WHEN T0.ItemCode IS NULL THEN 'OTROS' ELSE T0.ItemCode END AS 'ItemCode', "
        Consulta &= " CASE WHEN T2.ItemName IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItemName END AS 'ItemName', "
        Consulta &= " CASE WHEN T3.ItmsGrpCod IS NULL THEN 0 ELSE T3.ItmsGrpCod END AS 'ItmsGrpCod', "
        Consulta &= " CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', "
        Consulta &= " T4.GroupCode, T5.GroupName"
        Consulta &= " FROM [SBO_TPD].[dbo].[OINV] T6	"
        Consulta &= " INNER JOIN [SBO_TPD].[dbo].[INV1] T0 ON T0.DocEntry = t6.DocEntry "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
        Consulta &= " LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode	"
        Consulta &= " WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
        Consulta &= " and T4.CbrGralAdicional = 'N' "

        If CKBCli.Checked = False Then
            Consulta &= " and T6.SlpCode <> 1 "
        End If

        If cmbAlmacen.SelectedValue <> 0 Then
            If cmbAlmacen.SelectedValue = 100 Then
                'Consulta &= " AND T6.SlpCode <> 8 AND T6.SlpCode <> 12 and T6.SlpCode <> 26 and T6.SlpCode <> 17 and T6.SlpCode <> 20 "
                Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '01') OR T6.SlpCode = -1) "
            ElseIf cmbAlmacen.SelectedValue = 102 Then
                'Consulta &= " AND (T6.SlpCode = 17 OR T6.SlpCode = 20 )"
                Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '03')) "
            ElseIf cmbAlmacen.SelectedValue = 103 Then
                'Consulta &= " AND (T6.SlpCode = 8 OR T6.SlpCode = 12 OR T6.SlpCode = 26 )"
                Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '07')) "
            End If
        End If

        If CmbGrupoArticulo.SelectedValue <> 999 Then
            Consulta &= " AND T3.ItmsGrpCod = " & CmbGrupoArticulo.SelectedValue
        End If

        If CmbArticulo.SelectedValue <> "TODOS" Then
            Consulta &= " AND T0.ItemCode ='" & CmbArticulo.SelectedValue & "'"
        End If


        Consulta &= " UNION ALL"
        Consulta &= " SELECT 'NOTA C.' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, "
        Consulta &= " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal * -1 ELSE (T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100)*-1 END AS Importe, "
        Consulta &= " CASE WHEN T0.ItemCode IS NULL THEN 'OTROS' ELSE T0.ItemCode END AS 'ItemCode', "
        Consulta &= " CASE WHEN T2.ItemName IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T2.ItemName END AS 'ItemName', "
        Consulta &= " CASE WHEN T3.ItmsGrpCod IS NULL THEN 0 ELSE T3.ItmsGrpCod END AS 'ItmsGrpCod', "
        Consulta &= " CASE WHEN T3.ItmsGrpNam IS NULL THEN 'OTROS MOVIMIENTOS' ELSE T3.ItmsGrpNam END AS 'ItmsGrpNam', "
        Consulta &= " T4.GroupCode, T5.GroupName"
        Consulta &= " FROM [SBO_TPD].[dbo].[ORIN] T6	"
        Consulta &= " INNER JOIN [SBO_TPD].[dbo].[RIN1] T0 ON T0.DocEntry = t6.DocEntry "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OSLP] T1 ON T6.SlpCode = T1.SlpCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITM] T2 ON T0.ItemCode = T2.ItemCode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OITB] T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod "
        Consulta &= " LEFT join [TPM].[dbo].[DEPCOBR] T4 ON T0.slpcode = T4.slpcode "
        Consulta &= " LEFT join [SBO_TPD].[dbo].[OCRG] T5 ON T4.GroupCode = T5.GroupCode "
        Consulta &= " WHERE T6.DocDate >= @FechaIni AND T6.DocDate <= @FechaTer "
        Consulta &= " and T4.CbrGralAdicional = 'N' AND T6.DocType = 'I'"

        If CKBCli.Checked = False Then
            Consulta &= " and T6.SlpCode <> 1 "
        End If

        If cmbAlmacen.SelectedValue <> 0 Then
            If cmbAlmacen.SelectedValue = 100 Then
                'Consulta &= " AND T6.SlpCode <> 8 AND T6.SlpCode <> 12 AND T6.SlpCode <> 26 AND T6.SlpCode <> 17 AND T6.SlpCode <> 20 "
                Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '01') OR T6.SlpCode = -1) "
            ElseIf cmbAlmacen.SelectedValue = 102 Then
                'Consulta &= " AND (T6.SlpCode = 17 OR T6.SlpCode <> 20 ) "
                Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '03')) "
            ElseIf cmbAlmacen.SelectedValue = 103 Then
                'Consulta &= " AND (T6.SlpCode = 8 OR T6.SlpCode <> 12 OR T6.SlpCode <> 26 )"
                Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '07')) "
            End If
        End If

        If CmbGrupoArticulo.SelectedValue <> 999 Then
            Consulta &= " AND T3.ItmsGrpCod = " & CmbGrupoArticulo.SelectedValue
        End If

        If CmbArticulo.SelectedValue <> "TODOS" Then
            Consulta &= " AND T0.ItemCode ='" & CmbArticulo.SelectedValue & "'"
        End If

        'If CmbGrupoArticulo.SelectedValue Then

        'End If

        Consulta &= " ) TMP"
        Consulta &= " ORDER BY SlpName"


        '--SELECT * FROM #VtasNet


        Consulta &= " SELECT *,'01' AS 'Almacen' INTO #NetPue "
        Consulta &= " FROM #VtasNet "
        'Consulta &= " WHERE slpcode <> 17 And slpcode <> 20 And slpcode <> 8 And slpcode <> 12 And slpcode <> 26"
        Consulta &= " WHERE slpcode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '01') OR slpcode = -1 "

        Consulta &= " SELECT *,'03' AS 'Almacen' INTO #NetMer"
        Consulta &= " FROM #VtasNet "
        'Consulta &= " WHERE slpcode = 17 Or slpcode = 20"
        Consulta &= " WHERE slpcode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '03') "

        Consulta &= " SELECT *,'07' AS 'Almacen' INTO #NetTux"
        Consulta &= " FROM #VtasNet "
        'Consulta &= " WHERE slpcode = 8 Or slpcode = 12 Or slpcode = 26"
        Consulta &= " WHERE slpcode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '07') "
        '--FIN VENTAS

        Consulta &= " DECLARE @TotVal DECIMAL = (SELECT SUM(VALOR) FROM #ValInv)"

        Consulta &= " DECLARE @TotVen DECIMAL = (SELECT SUM(importe) FROM #VtasNet)"

        Consulta &= " DECLARE @PromVtasMen DECIMAL = (SELECT SUM(importe)/@PromMeses FROM #VtasNet)"

        Consulta &= " SELECT T0.Id, T0.Almacen,SUM(T0.Valor) AS 'Valor',SUM(T0.Valor)/@TotVal*100 AS 'PorValor'"
        Consulta &= " INTO #ValInv2"
        Consulta &= " FROM #ValInv T0"
        Consulta &= " GROUP BY T0.id,T0.Almacen"
        Consulta &= " ORDER BY 'ID' "

        '--SELECT * FROM #ValInv2

        Consulta &= " SELECT T0.Id,T0.Almacen,T0.Valor,T0.PorValor,"
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN SUM(T1.Importe)"
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN SUM(T2.Importe)"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN SUM(T3.Importe)"
        Consulta &= " END AS 'VtasTotales',"
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN SUM(T1.Importe)/@TotVen*100"
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN SUM(T2.Importe)/@TotVen*100"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN SUM(T3.Importe)/@TotVen*100"
        Consulta &= " END AS 'PorVtasTot',"
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN SUM(T1.Importe)/@PromMeses"
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN SUM(T2.Importe)/@PromMeses"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN SUM(T3.Importe)/@PromMeses"
        Consulta &= " END AS 'PromVtasMen',"
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN (SUM(T1.Importe)/@PromMeses)/@PromVtasMen*100"
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN (SUM(T2.Importe)/@PromMeses)/@PromVtasMen*100"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN (SUM(T3.Importe)/@PromMeses)/@PromVtasMen*100"
        Consulta &= " END AS 'PorPromVtasMen'"
        Consulta &= " INTO #TotInv"
        Consulta &= " FROM #ValInv2 T0"
        Consulta &= " LEFT JOIN #NetPue T1 ON T0.Id COLLATE MODERN_SPANISH_CI_AS = T1.Almacen"
        Consulta &= " LEFT JOIN #NetMer T2 ON T0.Id COLLATE MODERN_SPANISH_CI_AS = T2.Almacen"
        Consulta &= " LEFT JOIN #NetTux T3 ON T0.Id COLLATE MODERN_SPANISH_CI_AS = T3.Almacen"
        Consulta &= " GROUP BY T0.Id,T0.Almacen,T0.Valor,T0.PorValor "


        '------	CONSULTA 1
        '--SELECT * FROM #TotInv


        Consulta &= " SELECT Id,Almacen,ItmsGrpCod,ItmsGrpNam,SUM(Valor) AS 'Valor',SUM(Valor)/@TotVal*100 AS 'PorValor'"
        Consulta &= " INTO #InvLinea"
        Consulta &= " FROM #ValInv"
        Consulta &= " GROUP BY Id,Almacen,ItmsGrpCod,ItmsGrpNam"

        '----OBTENEMOS TOTALES DE INVENTARIO POR LINEA

        Consulta &= " DECLARE @TotLinea TABLE("
        Consulta &= " CodLin INT, "
        Consulta &= " Linea VARCHAR (100),"
        Consulta &= " Total DECIMAL (19,2))"

        Consulta &= " INSERT INTO @TotLinea(CodLin,Linea,Total)"
        Consulta &= " SELECT ItmsGrpCod,ItmsGrpNam,SUM(Valor) FROM #InvLinea"
        Consulta &= " GROUP BY ItmsGrpCod,ItmsGrpNam"

        Consulta &= " DECLARE @TotVtasLinea TABLE("
        Consulta &= " CodLin INT, "
        Consulta &= " Linea VARCHAR (100),"
        Consulta &= " Total DECIMAL (19,2))"

        Consulta &= " INSERT INTO @TotVtasLinea(CodLin,Linea,Total)"
        Consulta &= " SELECT ItmsGrpCod,ItmsGrpNam,SUM(Importe) FROM #VtasNet"
        Consulta &= " GROUP BY ItmsGrpCod,ItmsGrpNam"


        Consulta &= " DECLARE @TotPromLinea TABLE("
        Consulta &= " CodLin INT, "
        Consulta &= " Linea VARCHAR (100),"
        Consulta &= " Total DECIMAL (19,2))"

        Consulta &= " INSERT INTO @TotPromLinea(CodLin,Linea,Total)"
        Consulta &= " SELECT ItmsGrpCod,ItmsGrpNam,SUM(Importe)/@PromMeses FROM #VtasNet"
        Consulta &= " GROUP BY ItmsGrpCod,ItmsGrpNam"

        Consulta &= " SELECT T0.Id,T0.Almacen,T0.ItmsGrpCod,T0.ItmsGrpNam,T0.Valor,"
        Consulta &= " CASE WHEN T0.Valor IS NULL OR T4.Total IS NULL OR T0.Valor=0 OR T4.Total=0 THEN 0 "
        Consulta &= " ELSE T0.Valor/T4.Total END AS 'PorValor',"
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN CASE WHEN SUM(T1.Importe) IS NULL THEN 0 ELSE SUM(T1.Importe) END "
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN CASE WHEN SUM(T2.Importe) IS NULL THEN 0 ELSE SUM(T2.Importe) END"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN CASE WHEN SUM(T3.Importe) IS NULL THEN 0 ELSE SUM(T3.Importe) END"
        Consulta &= " END AS 'VtasTotales',	"   '--/@TotVen*100
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN CASE WHEN SUM(T1.Importe) IS NULL or T5.Total=0 THEN 0 ELSE SUM(T1.Importe)/T5.Total END "
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN CASE WHEN SUM(T2.Importe) IS NULL OR T5.Total=0 THEN 0 ELSE SUM(T2.Importe)/T5.Total END"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN CASE WHEN SUM(T3.Importe) IS NULL OR T5.Total=0 THEN 0 ELSE SUM(T3.Importe)/T5.Total END"
        Consulta &= " END AS 'PorVtasTot',"
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN CASE WHEN SUM(T1.Importe) IS NULL THEN 0 ELSE SUM(T1.Importe)/@PromMeses END"
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN CASE WHEN SUM(T2.Importe) IS NULL THEN 0 ELSE SUM(T2.Importe)/@PromMeses END"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN CASE WHEN SUM(T3.Importe) IS NULL THEN 0 ELSE SUM(T3.Importe)/@PromMeses END"
        Consulta &= " END AS 'PromVtasMen',"
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN CASE WHEN SUM(T1.Importe) IS NULL OR T6.Total=0 THEN 0 ELSE (SUM(T1.Importe)/@PromMeses)/t6.Total END"
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN CASE WHEN SUM(T2.Importe) IS NULL OR T6.Total=0 THEN 0 ELSE (SUM(T2.Importe)/@PromMeses)/t6.Total END"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN CASE WHEN SUM(T3.Importe) IS NULL OR T6.Total=0 THEN 0 ELSE (SUM(T3.Importe)/@PromMeses)/t6.Total END"
        Consulta &= " END AS 'PorPromVtasMen', T0.ItmsGrpNam as 'LinOrd' "
        Consulta &= " INTO #TotLineas"
        Consulta &= " FROM #InvLinea T0"
        Consulta &= " LEFT JOIN #NetPue T1 ON T0.Id = T1.Almacen COLLATE SQL_Latin1_General_CP1_CI_AS AND T0.ItmsGrpCod=T1.ItmsGrpCod"
        Consulta &= " LEFT JOIN #NetMer T2 ON T0.Id = T2.Almacen COLLATE SQL_Latin1_General_CP1_CI_AS AND T0.ItmsGrpCod=T2.ItmsGrpCod"
        Consulta &= " LEFT JOIN #NetTux T3 ON T0.Id = T3.Almacen COLLATE SQL_Latin1_General_CP1_CI_AS AND T0.ItmsGrpCod=T3.ItmsGrpCod"
        Consulta &= " LEFT JOIN @TotLinea T4 ON T0.ItmsGrpCod = T4.CodLin"
        Consulta &= " LEFT JOIN @TotVtasLinea T5 ON T0.ItmsGrpCod =T5.CodLin"
        Consulta &= " left join @TotPromLinea t6 on T0.ItmsGrpCod =T6.CodLin"
        Consulta &= " GROUP BY T0.Id,T0.Almacen,T0.ItmsGrpCod,T0.ItmsGrpNam,T0.Valor,T4.Total,T5.Total,t6.Total,T0.ItmsGrpNam "

        '---- CONSULTA 2
        If cmbAlmacen.SelectedValue = 0 Then
            Consulta &= " SELECT 0 AS ID,'TODOS' AS ALMACEN,ItmsGrpCod,ItmsGrpNam,SUM(Valor) AS 'Valor',SUM(PorValor) AS 'PorValor',SUM(VtasTotales) AS 'VtasTotales',"
            Consulta &= " SUM(PorVtasTot) AS 'PorVtasTot',SUM(PromVtasMen) AS 'PromVtasMen',SUM(PorPromVtasMen) AS 'PorPromVtasMen', LinOrd"
            Consulta &= " INTO #Totales FROM #TotLineas"
            Consulta &= " GROUP BY ItmsGrpCod,ItmsGrpNam,LinOrd"
            Consulta &= " UNION ALL"
            Consulta &= " SELECT * FROM #TotLineas"

        Else
            Consulta &= " SELECT * INTO #Totales FROM #TotLineas"
        End If

        'Consulta &= " SELECT * FROM #Totales"

        Consulta &= " SELECT * from #Totales"
        Consulta &= " UNION ALL"
        Consulta &= " SELECT 999 AS ID,'**Totales**' AS ALMACEN,0 as ItmsGrpCod,'*Todas**' ItmsGrpNam,SUM(Valor) AS 'Valor',SUM(PorValor) AS 'PorValor',SUM(VtasTotales) AS 'VtasTotales',"
        Consulta &= " SUM(PorVtasTot) AS 'PorVtasTot',SUM(PromVtasMen) AS 'PromVtasMen',SUM(PorPromVtasMen) AS 'PorPromVtasMen', 'ZZZZZ'"
        Consulta &= " FROM #Totales WHERE ALMACEN = 'TODOS' "
        Consulta &= " ORDER BY LinOrd,ID"

        '----********
        '----********

        Consulta &= " SELECT Id,Almacen,ItemCode,ItemName,ItmsGrpCod,ItmsGrpNam,SUM(Valor) AS 'Valor',SUM(Valor)/@TotVal*100 AS 'PorValor'"
        Consulta &= " INTO #InvArticulos"
        Consulta &= " FROM #ValInv"
        Consulta &= " GROUP BY Id,Almacen,ItemCode,ItemName,ItmsGrpCod,ItmsGrpNam"

        Consulta &= " SELECT T0.ItemCode,T0.ItemName,T0.ItmsGrpCod,T0.ItmsGrpNam,T0.Valor,"
        Consulta &= " CASE WHEN T0.Valor = 0 OR T4.Total = 0 THEN 0 ELSE T0.Valor/T4.Total END AS 'PorValor',"
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN CASE WHEN SUM(T1.Importe) IS NULL THEN 0 ELSE SUM(T1.Importe) END "
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN CASE WHEN SUM(T2.Importe) IS NULL THEN 0 ELSE SUM(T2.Importe) END"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN CASE WHEN SUM(T3.Importe) IS NULL THEN 0 ELSE SUM(T3.Importe) END"
        Consulta &= " END AS 'VtasTotales',	"       '--/@TotVen*100
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN CASE WHEN SUM(T1.Importe) IS NULL OR T5.Total = 0 THEN 0 ELSE SUM(T1.Importe)/T5.Total END "
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN CASE WHEN SUM(T2.Importe) IS NULL  OR T5.Total = 0 THEN 0 ELSE SUM(T2.Importe)/T5.Total END"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN CASE WHEN SUM(T3.Importe) IS NULL OR T5.Total = 0 THEN 0 ELSE SUM(T3.Importe)/T5.Total END"
        Consulta &= " END AS 'PorVtasTot',"
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN CASE WHEN SUM(T1.Importe) IS NULL THEN 0 ELSE SUM(T1.Importe)/@PromMeses END"
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN CASE WHEN SUM(T2.Importe) IS NULL THEN 0 ELSE SUM(T2.Importe)/@PromMeses END"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN CASE WHEN SUM(T3.Importe) IS NULL THEN 0 ELSE SUM(T3.Importe)/@PromMeses END"
        Consulta &= " END AS 'PromVtasMen',"
        Consulta &= " CASE WHEN T0.Almacen = 'PUEBLA' THEN CASE WHEN SUM(T1.Importe) IS NULL OR T6.Total = 0 THEN 0 ELSE (SUM(T1.Importe)/@PromMeses)/T6.Total END"
        Consulta &= " WHEN T0.Almacen = 'MÉRIDA' THEN CASE WHEN SUM(T2.Importe) IS NULL OR T6.Total = 0 THEN 0 ELSE (SUM(T2.Importe)/@PromMeses)/T6.Total END"
        Consulta &= " WHEN T0.Almacen = 'TUXTLA GTZ' THEN CASE WHEN SUM(T3.Importe) IS NULL OR T6.Total = 0 THEN 0 ELSE (SUM(T3.Importe)/@PromMeses)/T6.Total END"
        Consulta &= " END AS 'PorPromVtasMen',T0.Id,T0.Almacen "
        Consulta &= " INTO #TotArticulos"
        Consulta &= " FROM #InvArticulos T0"
        Consulta &= " LEFT JOIN #NetPue T1 ON T0.Id COLLATE SQL_Latin1_General_CP1_CI_AS = T1.Almacen AND T0.ItemCode=T1.ItemCode"
        Consulta &= " LEFT JOIN #NetMer T2 ON T0.Id COLLATE SQL_Latin1_General_CP1_CI_AS = T2.Almacen AND T0.ItemCode=T2.ItemCode"
        Consulta &= " LEFT JOIN #NetTux T3 ON T0.Id COLLATE SQL_Latin1_General_CP1_CI_AS  = T3.Almacen AND T0.ItemCode=T3.ItemCode"
        Consulta &= " LEFT JOIN @TotLinea T4 ON T0.ItmsGrpCod = T4.CodLin"
        Consulta &= " LEFT JOIN @TotVtasLinea T5 ON T0.ItmsGrpCod =T5.CodLin"
        Consulta &= " left join @TotPromLinea t6 on T0.ItmsGrpCod =T6.CodLin"
        Consulta &= " GROUP BY T0.Id,T0.Almacen,T0.ItemCode,T0.ItemName,T0.ItmsGrpCod,T0.ItmsGrpNam,T0.Valor,T0.PorValor, "
        Consulta &= " T4.Total, t5.Total, t6.Total"


        '---- CONSULTA 3
        Consulta &= " SELECT * FROM #TotArticulos ORDER BY ItmsGrpNam,ID,PromVtasMen DESC,VALOR DESC"
        '--ORDER BY ITMS

        '----********
        '----********



        Consulta &= " DROP TABLE #ValInv"
        Consulta &= " DROP TABLE #VtasNet"
        Consulta &= " DROP TABLE #NetPue"
        Consulta &= " DROP TABLE #NetMer"
        Consulta &= " DROP TABLE #NetTux"
        Consulta &= " DROP TABLE #ValInv2"
        Consulta &= " DROP TABLE #TotInv"
        Consulta &= " DROP TABLE #InvLinea"
        Consulta &= " DROP TABLE #TotLineas"
        Consulta &= " DROP TABLE #InvArticulos"
        Consulta &= " DROP TABLE #TotArticulos "


        Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

        'CmdMObra.Parameters.Add("@FechaIni", SqlDbType.Date)
        'CmdMObra.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
        'CmdMObra.Parameters.Add("@FechaTer", SqlDbType.Date)
        'CmdMObra.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value
        CmdMObra.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime).Value = Me.DtpFechaIni.Value
        CmdMObra.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime).Value = Me.DtpFechaTer.Value



        'If CmbGrupoArticulo.SelectedValue <> 999 Then
        '    CmdMObra.Parameters.Add("@GrupoArt", SqlDbType.Int)
        '    CmdMObra.Parameters("@GrupoArt").Value = CmbGrupoArticulo.SelectedValue
        'End If


        'If CmbArticulo.SelectedValue <> "TODOS" Then
        '    CmdMObra.Parameters.Add("@IdArticulo", SqlDbType.Char)
        '    CmdMObra.Parameters("@IdArticulo").Value = CmbArticulo.SelectedValue
        'End If

        Dim DsVtasDet As New DataSet

        'DTMObra.TableName = "DetBO"

        DsVtasDet.Tables.Add(DTMObra)

        CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
        CmdMObra.Connection.Open()

        Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)

        AdapMObra.SelectCommand = CmdMObra
        AdapMObra.Fill(DsVtasDet, "BO")

        DsVtasDet.Tables(1).TableName = "BackOrder"
        DsVtasDet.Tables(2).TableName = "DetBO"

        'Dim DtBackOrder As New DataTable
        'DtBackOrder = DsVtasDet.Tables("BackOrder")

        'Me.DGLineas.DataSource = DtBackOrder

        DvValInv.Table = DsVtasDet.Tables("BackOrder")

        With Me.DGLineas
            .DataSource = DvValInv
        End With

        DvBO.Table = DsVtasDet.Tables("DetBO")

        With Me.DGArticulos
            .DataSource = DvBO
        End With

        DisenoGrid()

        DisenoGridDet()

    End Sub


    Private Sub DisenoGrid()

        Try

            With Me.DGLineas
                '.DataSource = DTMObra
                '.ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .RowHeadersWidth = 25
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                '.RowHeadersVisible = False
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = True
                .AllowUserToAddRows = False
                '.AllowUserToAddRows = False

                .Columns(0).HeaderText = "idAlm"
                .Columns(0).Width = 30
                .Columns(0).Visible = False

                .Columns(1).HeaderText = "Almacen"
                .Columns(1).Width = 100
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(2).HeaderText = "itmsgrpcod"
                .Columns(2).Width = 30
                .Columns(2).Visible = False

                .Columns(3).HeaderText = "Línea"
                .Columns(3).Width = 150

                .Columns(4).HeaderText = "Inventario ($)"
                .Columns(4).Width = 100
                .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(5).HeaderText = "Inventario (%)"
                .Columns(5).Width = 75
                .Columns(5).DefaultCellStyle.Format = "#0.#0 %"
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(6).HeaderText = "Ventas Netas ($)"
                .Columns(6).Width = 100
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                '.Columns(6).Visible = False

                .Columns(7).HeaderText = "Ventas Netas (%)"
                .Columns(7).Width = 75
                '.Columns(7).Visible = False
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Format = "#0.#0 %"

                .Columns(8).HeaderText = "Promedio Ventas Mensual ($)"
                .Columns(8).Width = 100
                .Columns(8).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                .Columns(9).HeaderText = "Promedio Ventas Mensual (%)"
                .Columns(9).Width = 75
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).DefaultCellStyle.Format = "#0.#0 %"

                .Columns(10).HeaderText = "Linea"
                .Columns(10).Width = 75
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(10).Visible = False

            End With

        Catch ex As Exception
            'MsgBox("DG1")
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DisenoGridDet()

        Try

            With Me.DGArticulos
                '.DataSource = DTMObra
                .ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .RowHeadersWidth = 35
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                '.RowHeadersVisible = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = True
                .AllowUserToAddRows = False
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(0).HeaderText = "Artículo"
                .Columns(0).Width = 90
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(0).Frozen = True

                .Columns(1).HeaderText = "Descripción"
                .Columns(1).Width = 180
                .Columns(1).Frozen = True

                .Columns(2).HeaderText = "CodLin"
                .Columns(2).Width = 140
                .Columns(2).Visible = False

                .Columns(3).HeaderText = "Línea"
                .Columns(3).Width = 120
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                '.Columns(5).DefaultCellStyle.Format = "##.#0 %"

                .Columns(4).HeaderText = "Valor Inventario ($)"
                .Columns(4).Width = 80
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                '.Columns(4).Visible = False

                .Columns(5).HeaderText = "Valor Inventario (%)"
                .Columns(5).Width = 60
                .Columns(5).DefaultCellStyle.Format = "#0.#0 %"
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .Columns(6).HeaderText = "Ventas Netas ($)"
                .Columns(6).Width = 80
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(7).HeaderText = "Ventas Netas (%)"
                .Columns(7).Width = 60
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Format = "#0.#0 %"

                .Columns(8).HeaderText = "Promedio Ventas Mensual ($)"
                .Columns(8).Width = 80
                .Columns(8).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(9).HeaderText = "Promedio Ventas Mensual (%)"
                .Columns(9).Width = 60
                .Columns(9).DefaultCellStyle.Format = "#0.#0 %"
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(10).HeaderText = "idAlm"
                .Columns(10).Width = 30
                .Columns(10).Visible = False

                .Columns(11).HeaderText = "Almacen"
                .Columns(11).Width = 90
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            End With

        Catch ex As Exception
            'MsgBox("DG2")
            'MsgBox(ex.Message)
        End Try

    End Sub


    'FILTROS BUSQUEDA

    'Private Sub BuscaArticulos()

    'End Sub


    Private Sub BuscaDetalle()
        Try
            If DGLineas.Item(0, DGLineas.CurrentRow.Index).Value = 0 Then
                DvBO.RowFilter = "itmsgrpcod=" & DGLineas.Item(2, DGLineas.CurrentRow.Index).Value
            Else
                DvBO.RowFilter = "almacen = '" & DGLineas.Item(1, DGLineas.CurrentRow.Index).Value.ToString & "' and itmsgrpcod=" & DGLineas.Item(2, DGLineas.CurrentRow.Index).Value
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGLineas_CurrentCellChanged(sender As Object, e As EventArgs) Handles DGLineas.CurrentCellChanged
        BuscaDetalle()
    End Sub

    Private Sub CmbGrupoArticulo_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbGrupoArticulo.SelectionChangeCommitted
        'Try

        '    If CmbGrupoArticulo.SelectedValue <> 999 Then
        '        DvArticulo.RowFilter = "itmsgrpcod = " & CmbGrupoArticulo.SelectedValue & " or ItmsGrpCod = 999 "
        '        CmbArticulo.SelectedValue = "TODOS"
        '    Else
        '        DvArticulo.RowFilter = String.Empty
        '        CmbArticulo.SelectedValue = "TODOS"
        '    End If

        'Catch ex As Exception

        'End Try

        BuscaDetalle()

    End Sub

    Private Sub DGLineas_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGLineas.RowPrePaint
        Try
            If DGLineas.Rows(e.RowIndex).Cells("almacen").Value = "TODOS" Then
                DGLineas.Rows(e.RowIndex).Cells("id").Style.BackColor = Color.LightGray
                DGLineas.Rows(e.RowIndex).Cells("almacen").Style.BackColor = Color.LightGray
                DGLineas.Rows(e.RowIndex).Cells("itmsgrpcod").Style.BackColor = Color.LightGray
                DGLineas.Rows(e.RowIndex).Cells("itmsgrpnam").Style.BackColor = Color.LightGray
                DGLineas.Rows(e.RowIndex).Cells("valor").Style.BackColor = Color.LightGray
                DGLineas.Rows(e.RowIndex).Cells("porvalor").Style.BackColor = Color.LightGray
                DGLineas.Rows(e.RowIndex).Cells("VtasTotales").Style.BackColor = Color.LightGray
                DGLineas.Rows(e.RowIndex).Cells("PorVtasTot").Style.BackColor = Color.LightGray
                DGLineas.Rows(e.RowIndex).Cells("PromVtasMen").Style.BackColor = Color.LightGray
                DGLineas.Rows(e.RowIndex).Cells("PorPromVtasMen").Style.BackColor = Color.LightGray
            End If

            'Consulta &= " SELECT 0 AS ID,'TODOS' AS ALMACEN,ItmsGrpCod,ItmsGrpNam,SUM(Valor) AS 'Valor',SUM(PorValor) AS 'PorValor',SUM(VtasTotales) AS 'VtasTotales',"
            'Consulta &= " SUM(PorVtasTot) AS 'PorVtasTot',SUM(PromVtasMen) AS 'PromVtasMen',SUM(PorPromVtasMen) AS 'PorPromVtasMen' "

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGArticulos_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGArticulos.RowPrePaint
        For i As Integer = 0 To DGArticulos.RowCount - 1
            DGArticulos.Rows(e.RowIndex).Cells("valor").Style.BackColor = Color.Yellow
            DGArticulos.Rows(e.RowIndex).Cells("VtasTotales").Style.BackColor = Color.LightGreen
            DGArticulos.Rows(e.RowIndex).Cells("PromVtasMen").Style.BackColor = Color.Gold
        Next

    End Sub

    Private Sub BtnExcel_Click(sender As Object, e As EventArgs) Handles BtnExcel.Click

        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)


        'COMBINAMOS CELDAS
        oSheet.Range("A1:E1").Merge(True)
        oSheet.Range("A2:E2").Merge(True)
        oSheet.Range("A3:E3").Merge(True)
        oSheet.Range("A4:E4").Merge(True)
        oSheet.Range("A5:E5").Merge(True)

        oSheet.range("A1:E1").font.bold = True
        oSheet.range("A2:E2").font.bold = True
        oSheet.range("A3:E3").font.bold = True
        oSheet.range("A4:E4").font.bold = True
        oSheet.range("A5:E5").font.bold = True

        'DAR COLOR DE FONDO A CELDAS
        oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
        oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15
        oSheet.Range("A3:C3").INTERIOR.COLORINDEX = 15
        oSheet.Range("A4:C4").INTERIOR.COLORINDEX = 15
        oSheet.Range("A5:C5").INTERIOR.COLORINDEX = 15


        oSheet.Range("A7").INTERIOR.COLORINDEX = 15
        oSheet.Range("B7").INTERIOR.COLORINDEX = 15
        oSheet.Range("C7").INTERIOR.COLORINDEX = 15
        oSheet.Range("D7").INTERIOR.COLORINDEX = 15
        oSheet.Range("E7").INTERIOR.COLORINDEX = 15
        oSheet.Range("F7").INTERIOR.COLORINDEX = 15
        oSheet.Range("G7").INTERIOR.COLORINDEX = 15
        oSheet.Range("H7").INTERIOR.COLORINDEX = 15
        'oSheet.Range("7").INTERIOR.COLORINDEX = 15
        'oSheet.Range("J7").INTERIOR.COLORINDEX = 15
        'oSheet.Range("K7").INTERIOR.COLORINDEX = 15



        'Declaramos el nombre de las columnas
        oSheet.range("A7").value = "Almacen"
        oSheet.range("B7").value = "Línea"
        oSheet.range("C7").value = "Inventario($)"
        oSheet.range("D7").value = "Inventario(%)"
        oSheet.range("E7").value = "Vtas Netas($)"
        oSheet.range("F7").value = "Vtas Netas(%)"
        oSheet.range("G7").value = "Prom. Vtas.Mens($)"
        oSheet.range("H7").value = "Prom. Vtas.Mens(%)"
        'oSheet.range("I6").value = "Descripción"
        'oSheet.range("J6").value = "Linea"
        'oSheet.range("K6").value = "Pedido Cliente"
        'oSheet.range("L6").value = "Facturado"
        'oSheet.range("M6").value = "Back Order"


        'DISEÑO DE EXCEL

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A7:H7").font.bold = True


        oExcel.worksheets("Hoja1").Columns("A").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("A").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("B").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("B").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("C").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("C").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("D").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("D").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("E").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("E").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("F").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("F").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("G").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("G").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("H").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("H").Font.Size = 8
        'oExcel.worksheets("Hoja1").Columns("I").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        'oExcel.worksheets("Hoja1").Columns("I").Font.Size = 8
        'oExcel.worksheets("Hoja1").Columns("J").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        'oExcel.worksheets("Hoja1").Columns("J").Font.Size = 8
        'oExcel.worksheets("Hoja1").Columns("K").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        'oExcel.worksheets("Hoja1").Columns("K").Font.Size = 8
        'oExcel.worksheets("Hoja1").Columns("L").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        'oExcel.worksheets("Hoja1").Columns("L").Font.Size = 8


        'Cambia el alto de celda 
        oSheet.range("A:H").RowHeight = 13

        'oSheet.range("A:V").HorizontalAlignment = xlCenter

        'TAMAÑO DE COLUMNAS
        oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 12
        oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 18
        oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 8
        oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 8
        oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 8
        'oExcel.worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 11
        'oExcel.worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 12
        'oExcel.worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 6
        'oExcel.worksheets("Hoja1").Columns("L").EntireColumn.ColumnWidth = 6



        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = Me.DGLineas.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel1 As String = Me.DGLineas.Item(1, fila_dt).Value
            Dim cel2 As String = Me.DGLineas.Item(3, fila_dt).Value
            Dim cel3 As String = IIf(IsDBNull(Me.DGLineas.Item(4, fila_dt).Value), 0, Me.DGLineas.Item(4, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DGLineas.Item(5, fila_dt).Value), 0, Me.DGLineas.Item(5, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DGLineas.Item(6, fila_dt).Value), 0, Me.DGLineas.Item(6, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DGLineas.Item(7, fila_dt).Value), 0, Me.DGLineas.Item(7, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DGLineas.Item(8, fila_dt).Value), 0, Me.DGLineas.Item(8, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DGLineas.Item(9, fila_dt).Value), 0, Me.DGLineas.Item(9, fila_dt).Value)

            fila_dt_excel = fila_dt + 8 'Renglón en donde se empieza a registrar el reporte

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
            oSheet.range("D" & fila_dt_excel).value = cel4
            oSheet.range("E" & fila_dt_excel).value = cel5
            oSheet.range("F" & fila_dt_excel).value = cel6
            oSheet.range("G" & fila_dt_excel).value = cel7
            oSheet.range("H" & fila_dt_excel).value = cel8


            oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "$ ###,###,###.##"
            oExcel.Worksheets("Hoja1").Columns("D").NumberFormat = "##.## %"
            oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "$ ###,###,###.##"
            oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "##.## %"
            oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "$ ###,###,###.##"
            oExcel.Worksheets("Hoja1").Columns("H").NumberFormat = "##.## %"

            Estatus.Visible = True
            ProgressBar1.Value = (fila_dt * 100) / total_reg

        Next

        Estatus.Visible = False

        'Formato de texto para la primera columna CLAVE ART
        oExcel.Worksheets("Hoja1").Columns("A").NumberFormat = "@"

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        'oSheet.columns("A:O").entirecolumn.autofit()
        oSheet.range("A1").value = "Reporte: Valorización de inventario por producto "
        oSheet.range("A2").value = "Sucursal: " + cmbAlmacen.Text
        oSheet.range("A3").value = "Línea: " + CmbGrupoArticulo.Text
        oSheet.range("A4").value = "Artículo: " + CmbArticulo.Text
        oSheet.range("A5").value = "Periodo de ventas del: " + DtpFechaIni.Value + " al " + DtpFechaTer.Value

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)


        'COMBINAMOS CELDAS
        oSheet.Range("A1:E1").Merge(True)
        oSheet.Range("A2:E2").Merge(True)
        oSheet.Range("A3:E3").Merge(True)
        oSheet.Range("A4:E4").Merge(True)
        oSheet.Range("A5:E5").Merge(True)

        oSheet.range("A1:E1").font.bold = True
        oSheet.range("A2:E2").font.bold = True
        oSheet.range("A3:E3").font.bold = True
        oSheet.range("A4:E4").font.bold = True
        oSheet.range("A5:E5").font.bold = True

        'DAR COLOR DE FONDO A CELDAS
        oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
        oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15
        oSheet.Range("A3:C3").INTERIOR.COLORINDEX = 15
        oSheet.Range("A4:C4").INTERIOR.COLORINDEX = 15
        oSheet.Range("A5:C5").INTERIOR.COLORINDEX = 15


        oSheet.Range("A7").INTERIOR.COLORINDEX = 15
        oSheet.Range("B7").INTERIOR.COLORINDEX = 15
        oSheet.Range("C7").INTERIOR.COLORINDEX = 15
        oSheet.Range("D7").INTERIOR.COLORINDEX = 15
        oSheet.Range("E7").INTERIOR.COLORINDEX = 15
        oSheet.Range("F7").INTERIOR.COLORINDEX = 15
        oSheet.Range("G7").INTERIOR.COLORINDEX = 15
        oSheet.Range("H7").INTERIOR.COLORINDEX = 15
        oSheet.Range("I7").INTERIOR.COLORINDEX = 15
        oSheet.Range("J7").INTERIOR.COLORINDEX = 15




        'Declaramos el nombre de las columnas
        oSheet.range("A7").value = "Artículo"
        oSheet.range("B7").value = "Descripción"
        oSheet.range("C7").value = "Línea"
        oSheet.range("D7").value = "Inventario($)"
        oSheet.range("E7").value = "Inventario(%)"
        oSheet.range("F7").value = "Vtas Netas($)"
        oSheet.range("G7").value = "Vtas Netas(%)"
        oSheet.range("H7").value = "Prom. Vtas.Mens($)"
        oSheet.range("I7").value = "Prom. Vtas.Mens(%)"
        oSheet.range("J7").value = "Almacen"
        'oSheet.range("L6").value = "Facturado"
        'oSheet.range("M6").value = "Back Order"


        'DISEÑO DE EXCEL

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A7:k7").font.bold = True


        oExcel.worksheets("Hoja1").Columns("A").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("A").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("B").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("B").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("C").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("C").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("D").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("D").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("E").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("E").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("F").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("F").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("G").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("G").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("H").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("H").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("I").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("I").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("J").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("J").Font.Size = 8
        'oExcel.worksheets("Hoja1").Columns("K").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        'oExcel.worksheets("Hoja1").Columns("K").Font.Size = 8



        'Cambia el alto de celda 
        oSheet.range("A:K").RowHeight = 13

        'oSheet.range("A:V").HorizontalAlignment = xlCenter

        'TAMAÑO DE COLUMNAS
        oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 14
        oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 20
        oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 18
        oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 8
        oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 8
        oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 8
        oExcel.worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 15





        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = Me.DGArticulos.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel1 As String = Me.DGArticulos.Item(0, fila_dt).Value
            Dim cel2 As String = Me.DGArticulos.Item(1, fila_dt).Value
            Dim cel3 As String = Me.DGArticulos.Item(3, fila_dt).Value

            Dim cel4 As String = IIf(IsDBNull(Me.DGArticulos.Item(4, fila_dt).Value), 0, Me.DGArticulos.Item(4, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DGArticulos.Item(5, fila_dt).Value), 0, Me.DGArticulos.Item(5, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DGArticulos.Item(6, fila_dt).Value), 0, Me.DGArticulos.Item(6, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DGArticulos.Item(7, fila_dt).Value), 0, Me.DGArticulos.Item(7, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DGArticulos.Item(8, fila_dt).Value), 0, Me.DGArticulos.Item(8, fila_dt).Value)
            Dim cel9 As String = IIf(IsDBNull(Me.DGArticulos.Item(9, fila_dt).Value), 0, Me.DGArticulos.Item(9, fila_dt).Value)
            Dim cel10 As String = Me.DGArticulos.Item(10, fila_dt).Value


            fila_dt_excel = fila_dt + 8 'Renglón en donde se empieza a registrar el reporte

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
            oSheet.range("D" & fila_dt_excel).value = cel4
            oSheet.range("E" & fila_dt_excel).value = cel5
            oSheet.range("F" & fila_dt_excel).value = cel6
            oSheet.range("G" & fila_dt_excel).value = cel7
            oSheet.range("H" & fila_dt_excel).value = cel8
            oSheet.range("I" & fila_dt_excel).value = cel9
            oSheet.range("J" & fila_dt_excel).value = cel10


            oExcel.Worksheets("Hoja1").Columns("D").NumberFormat = "$ ###,###,###.##"
            oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "##.## %"
            oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "$ ###,###,###.##"
            oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "##.## %"
            oExcel.Worksheets("Hoja1").Columns("H").NumberFormat = "$ ###,###,###.##"
            oExcel.Worksheets("Hoja1").Columns("I").NumberFormat = "##.## %"

            Panel1.Visible = True
            ProgressBar2.Value = (fila_dt * 100) / total_reg

        Next

        Panel1.Visible = False

        'Formato de texto para la primera columna CLAVE ART
        oExcel.Worksheets("Hoja1").Columns("A").NumberFormat = "@"

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        'oSheet.columns("A:O").entirecolumn.autofit()
        oSheet.range("A1").value = "Reporte: Valorización de inventario por producto - DETALLE "
        oSheet.range("A2").value = "Sucursal: " + cmbAlmacen.Text
        oSheet.range("A3").value = "Línea: " + CmbGrupoArticulo.Text
        oSheet.range("A4").value = "Artículo: " + CmbArticulo.Text
        oSheet.range("A5").value = "Periodo de ventas del: " + DtpFechaIni.Value + " al " + DtpFechaTer.Value

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing
    End Sub
End Class