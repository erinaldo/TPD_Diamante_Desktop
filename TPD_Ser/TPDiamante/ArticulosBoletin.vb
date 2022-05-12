
Imports System.Data.SqlClient

Public Class ArticulosBoletin

    Public StrTpm As String = conexion_universal.CadenaSQL
    Public StrCon As String = conexion_universal.CadenaSQLSAP
    Public conexion As New SqlConnection(conexion_universal.CadenaSQL)

    Private Sub ArticulosBoletin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LlenaAlmacen()
        LlenaListas()

        DTPFecIni.Value = DateAdd("M", -6, Today)
        DTPFecFin.Value = Today

    End Sub

    Private Sub LlenaAlmacen()
        Dim ConsutaLista As String

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)


            Dim DSetTablas As New DataSet
            ConsutaLista = "select WhsCode, WhsName from OWHS where WhsCode='01' or WhsCode='03' or WhsCode='07'"
            Dim daAlmacen As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            'Dim DSetTablas As New DataSet
            daAlmacen.Fill(DSetTablas, "Almacen")

            Dim fila As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            fila = DSetTablas.Tables("Almacen").NewRow

            'Agregamos los valores a los campos de la tabla
            fila("whsname") = "TODOS"
            fila("whscode") = 99

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Almacen").Rows.Add(fila)

            Me.CBAlmacen.DataSource = DSetTablas.Tables("Almacen")
            Me.CBAlmacen.DisplayMember = "whsname"
            Me.CBAlmacen.ValueMember = "whscode"
            Me.CBAlmacen.SelectedValue = 99

            '------------------------------------------------------
            ' -----------------------------------------------------

        End Using
    End Sub

    Private Sub LlenaListas()
        Dim ConsutaLista As String

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)


            Dim DSetTablas As New DataSet
            ConsutaLista = "SELECT * FROM LISTAPREBOL"
            Dim daAlmacen As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            'Dim DSetTablas As New DataSet
            daAlmacen.Fill(DSetTablas, "Listas")

            'Dim fila As Data.DataRow

            ''Asignamos a fila la nueva Row(Fila)del Dataset
            'fila = DSetTablas.Tables("Almacen").NewRow

            ''Agregamos los valores a los campos de la tabla
            'fila("whsname") = "TODOS"
            'fila("whscode") = 99

            ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            'DSetTablas.Tables("Almacen").Rows.Add(fila)

            Me.CBListaP.DataSource = DSetTablas.Tables("Listas")
            Me.CBListaP.DisplayMember = "nombre"
            Me.CBListaP.ValueMember = "lispre"
            'Me.CBListaP.SelectedIndex = 0

            '------------------------------------------------------
            ' -----------------------------------------------------

        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Ejecutar_Consulta()
    End Sub

    Private Sub Ejecutar_Consulta()



        Dim Consulta As String = ""
        Dim strcadena As String = ""
        Dim CTabla As String = ""
        Dim DTMObra As New DataTable
        Dim DTProb As New DataTable
        Dim vAlm As Integer = 0




       
        '****************************************

        '----DATOS BOLETIN"

        Consulta &= " SELECT ValidoDesde,ValidoHasta,Articulo,Descripcion,Linea,CostoUltCom,Lista09,PreLista,"
        Consulta &= " Factor1, Descuento, Cantidad, PreBoletin, Factor2, StockPue, StockMer, StockTux, StockTot"
        Consulta &= " INTO #Boletin FROM("
        Consulta &= " Select T0.FromDate 'ValidoDesde',T0.ToDate 'ValidoHasta',T0.ItemCode 'Articulo',"
        Consulta &= " T1.ItemName 'Descripcion',T2.ItmsGrpNam 'Linea',T1.LastPurPrc 'CostoUltCom',"
        Consulta &= " T3.Price 'Lista09', T4.Price 'PreLista',T4.Price/T3.Price 'Factor1',"
        Consulta &= " T0.Discount 'Descuento',1 'Cantidad', "
        'SE COMENTA ANTERIOR Y SE COLOCA LA NUEVA LINEA: URIEL TORALVA 24/05/2018
        Consulta &= " T4.Price - (T4.Price * (T0.Discount / 100)) AS 'PreBoletin', "
        'Consulta &= " T0.Price 'PreBoletin', "
        '----------------------------------------------------------------------------------------------------------
        Consulta &= " T0.Price/T3.Price 'Factor2', "
        Consulta &= " T5.OnHand 'StockPue',T6.OnHand 'StockMer',T7.OnHand 'StockTux',"
        Consulta &= " T1.OnHand -T8.OnHand 'StockTot'"
        Consulta &= " FROM SPP1 T0"
        Consulta &= " LEFT JOIN OITM T1 ON T0.ItemCode=T1.ItemCode"
        Consulta &= " LEFT JOIN OITB T2 ON T1.ItmsGrpCod=T2.ItmsGrpCod"
        Consulta &= " LEFT JOIN ITM1 T3 ON T0.ItemCode=T3.ItemCode AND T3.PriceList=11 "
        Consulta &= " LEFT JOIN ITM1 T4 ON T0.ItemCode=T4.ItemCode AND T4.PriceList=" & CBListaP.SelectedValue
        Consulta &= " LEFT JOIN OITW T5 ON T0.ItemCode=T5.ItemCode AND T5.WhsCode='01'"
        Consulta &= " LEFT JOIN OITW T6 ON T0.ItemCode=T6.ItemCode AND T6.WhsCode='03'"
        Consulta &= " LEFT JOIN OITW T7 ON T0.ItemCode=T7.ItemCode AND T7.WhsCode='07'"
        Consulta &= " LEFT JOIN OITW T8 ON T0.ItemCode=T8.ItemCode AND T8.WhsCode='02'"
        Consulta &= " WHERE  T0.CardCode = '*" & CBListaP.SelectedValue & "'"
        Consulta &= " UNION ALL"
        Consulta &= " Select T9.FromDate 'ValidoDesde',T9.ToDate 'ValidoHasta',T0.ItemCode 'Articulo',"
        Consulta &= " T1.ItemName 'Descripcion',T2.ItmsGrpNam 'Linea',T1.LastPurPrc 'CostoUltCom',"
        Consulta &= " T3.Price 'Lista09', T4.Price 'PreLista',T4.Price/T3.Price 'Factor1',"
        Consulta &= " T0.Discount 'Descuento',T0.Amount, "
        'SE COMENTA LINEA ANTERIOR Y SE AGREGA NUEVA LINEA : URIEL TORALVA 24/05/2018
        Consulta &= " T4.Price - (T4.Price * (T0.Discount / 100)) AS 'PreBoletin', "
        'Consulta &= " T0.Price 'PreBoletin', "
        '-----------------------------------------------------------------------------------------------------
        Consulta &= " T0.Price/T3.Price 'Factor2', "
        Consulta &= " T5.OnHand 'StockPue',T6.OnHand 'StockMer',T7.OnHand 'StockTux',"
        Consulta &= " T1.OnHand -T8.OnHand 'StockTot'"
        Consulta &= " FROM SPP2 T0"
        Consulta &= " INNER JOIN SPP1 T9 ON T0.ItemCode=T9.ItemCode AND T0.CardCode = T9.CardCode"
        Consulta &= " LEFT JOIN OITM T1 ON T0.ItemCode=T1.ItemCode"
        Consulta &= " LEFT JOIN OITB T2 ON T1.ItmsGrpCod=T2.ItmsGrpCod"
        Consulta &= " LEFT JOIN ITM1 T3 ON T0.ItemCode=T3.ItemCode AND T3.PriceList=11"
        Consulta &= " LEFT JOIN ITM1 T4 ON T0.ItemCode=T4.ItemCode AND T4.PriceList=" & CBListaP.SelectedValue
        Consulta &= " LEFT JOIN OITW T5 ON T0.ItemCode=T5.ItemCode AND T5.WhsCode='01'"
        Consulta &= " LEFT JOIN OITW T6 ON T0.ItemCode=T6.ItemCode AND T6.WhsCode='03'"
        Consulta &= " LEFT JOIN OITW T7 ON T0.ItemCode=T7.ItemCode AND T7.WhsCode='07'"
        Consulta &= " LEFT JOIN OITW T8 ON T0.ItemCode=T8.ItemCode AND T8.WhsCode='02'"
        Consulta &= " WHERE T0.CardCode = '*" & CBListaP.SelectedValue.ToString & "' ) TMP		"
        Consulta &= " GROUP BY ValidoDesde,ValidoHasta,Articulo,Descripcion,Linea,CostoUltCom,Lista09,PreLista,"
        Consulta &= " Factor1, Descuento, Cantidad, PreBoletin, Factor2, StockPue, StockMer, StockTux, StockTot"
        Consulta &= " ORDER BY Articulo"

        Dim NumDias As Decimal = DateDiff("D", DTPFecIni.Value, DTPFecFin.Value)

        'Consulta &= " DECLARE @FechaIni DATE = DATEADD(MM,-6,GETDATE())"
        Consulta &= " DECLARE @NumDias DECIMAL(19,2) = " & NumDias & ""
        Consulta &= " DECLARE @PromMeses DECIMAL(19,4) = @NumDias/30 "

        '----VENTAS
        Consulta &= " SELECT TipoDoc,Docnum,docdate,slpcode,slpname,importe,quantity,itemcode,itemname,"
        Consulta &= " itmsgrpcod,itmsgrpnam,groupcode,groupname INTO #VtasNet FROM("
        Consulta &= " SELECT 'FACTURA' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, "
        Consulta &= " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal ELSE T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100 END AS Importe, "
        Consulta &= " T0.QUANTITY,"
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
        Consulta &= " WHERE T6.DocDate >= '" & DTPFecIni.Value.ToString("yyyy-MM-dd") & "' AND T6.DocDate <= '" & DTPFecFin.Value.ToString("yyyy-MM-dd") & "' "
        Consulta &= " and T4.CbrGralAdicional = 'N' "

        If CBAlmacen.SelectedValue = "01" Then
            'Consulta &= " AND T6.SlpCode <> 8 AND T6.SlpCode <> 12 AND T6.SlpCode <> 26 AND T6.SlpCode <> 17 AND T6.SlpCode <> 20 "
            Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '01') OR T6.SlpCode = -1) "
        ElseIf CBAlmacen.SelectedValue = "03" Then
            'Consulta &= " AND (T6.SlpCode = 17 OR T6.SlpCode = 20 )"
            Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '03')) "
        ElseIf CBAlmacen.SelectedValue = "07" Then
            'Consulta &= " AND (T6.SlpCode = 8 OR T6.SlpCode = 12 OR T6.SlpCode = 26)"
            Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '07')) "
        End If


        Consulta &= " UNION ALL"
        Consulta &= " SELECT 'NOTA C.' AS 'TipoDoc',t6.DocNum, T6.DocDate, T6.SlpCode, T1.SlpName, "
        Consulta &= " CASE WHEN T6.DiscPrcnt is null  THEN T0.LineTotal * -1 ELSE (T0.LineTotal - T0.LineTotal * T6.DiscPrcnt / 100)*-1 END AS Importe,"
        Consulta &= " T0.QUANTITY *-1, "
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
        Consulta &= " WHERE T6.DocDate >= '" & DTPFecIni.Value.ToString("yyyy-MM-dd") & "' AND T6.DocDate <= '" & DTPFecFin.Value.ToString("yyyy-MM-dd") & "' "
        Consulta &= " and T4.CbrGralAdicional = 'N' AND T6.DocType = 'I'"

        If CBAlmacen.SelectedValue = "01" Then
            'Consulta &= " AND T6.SlpCode <> 8 AND T6.SlpCode <> 12 AND T6.SlpCode <> 26 AND T6.SlpCode <> 17 AND T6.SlpCode <> 20 "
            Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '01') OR T6.SlpCode = -1) "
        ElseIf CBAlmacen.SelectedValue = "03" Then
            'Consulta &= " AND (T6.SlpCode = 17 OR T6.SlpCode = 20 )"
            Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '03')) "
        ElseIf CBAlmacen.SelectedValue = "07" Then
            'Consulta &= " AND (T6.SlpCode = 8 OR T6.SlpCode = 12 OR T6.SlpCode = 26)"
            Consulta &= " AND (T6.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '07')) "
        End If

        Consulta &= " ) TMP"
        Consulta &= " ORDER BY SlpName"

        ''FIN VENTAS


        Consulta &= " SELECT ItemCode,SUM(Quantity) 'Quantity' INTO #CanTot FROM #VtasNet"
        Consulta &= " GROUP BY ItemCode"
        Consulta &= " ORDER BY ItemCode"

        Consulta &= " SELECT T0.*,T1.Quantity 'VtasT',"
        Consulta &= " CASE WHEN T1.Quantity = 0 THEN 0 ELSE T1.Quantity/@PromMeses END 'VtaMes',"
        Consulta &= " CASE WHEN T1.Quantity=0 OR T0.StockTot=0 THEN 0 ELSE "

        If CBAlmacen.SelectedValue = "01" Then
            Consulta &= " T0.StockPue/(T1.Quantity/@PromMeses) END 'MesStock'"

        ElseIf CBAlmacen.SelectedValue = "03" Then
            Consulta &= " T0.StockMer/(T1.Quantity/@PromMeses) END 'MesStock'"

        ElseIf CBAlmacen.SelectedValue = "07" Then
            Consulta &= " T0.StockTux/(T1.Quantity/@PromMeses) END 'MesStock'"
        Else
            Consulta &= " T0.StockTot/(T1.Quantity/@PromMeses) END 'MesStock'"
        End If




        Consulta &= " FROM #Boletin T0"
        Consulta &= " LEFT JOIN #CanTot T1 ON T0.Articulo=T1.ItemCode"



        Consulta &= " DROP TABLE #Boletin "
        Consulta &= " DROP TABLE #VtasNet"
        Consulta &= " DROP TABLE #CanTot"


        Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

        ''CmdMObra.Parameters.Add("@FechaIni", SqlDbType.Date)
        ''CmdMObra.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
        ''CmdMObra.Parameters.Add("@FechaTer", SqlDbType.Date)
        ''CmdMObra.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value
        'CmdMObra.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime).Value = Me.DtpFechaIni.Value
        'CmdMObra.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime).Value = Me.DtpFechaTer.Value

        'If CmbAgteVta.SelectedValue <> 999 Then
        '    CmdMObra.Parameters.Add("@IdAgente", SqlDbType.Int)
        '    CmdMObra.Parameters("@IdAgente").Value = CmbAgteVta.SelectedValue
        'End If

        'If CmbCliente.SelectedValue <> "TODOS" Then
        '    CmdMObra.Parameters.Add("@IdCliente", SqlDbType.Char)
        '    CmdMObra.Parameters("@IdCliente").Value = CmbCliente.SelectedValue
        'End If

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
        'DsVtasDet.Tables(2).TableName = "DetBO"
        'DsVtasDet.Tables(3).TableName = "Recuperable"

        'datagridview 1
        'Se crea datatable y se asigna al datagridview
        Dim DtBackOrder As New DataTable
        DtBackOrder = DsVtasDet.Tables("BackOrder")
        Me.DGBoletin.DataSource = DtBackOrder

        ''datagridview2
        ''aqui se asigna al dataview el datatable
        'DvBO.Table = DsVtasDet.Tables("DetBO")

        ''despues se asigna el dataview al datatable
        'With Me.DGBO
        '    .DataSource = DvBO
        'End With

        ''datagridview 3

        ''Se cre datatable y se asigna al datagridview
        'Dim DtRecuperable As New DataTable
        'DtBackOrder = DsVtasDet.Tables("Recuperable")
        'Me.DGRecuperable.DataSource = DtBackOrder

        DisenoGrid()

    End Sub

    Private Sub DisenoGrid()
        Try

            With Me.DGBoletin
                .ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .RowHeadersWidth = 35
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                '.RowHeadersVisible = False
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = True
                .AllowUserToAddRows = False


                .Columns(0).HeaderText = "Válido desde"
                .Columns(0).Width = 85
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(0).Frozen = True

                .Columns(1).HeaderText = "ValidoHasta"
                .Columns(1).Width = 85
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).Frozen = True

                .Columns(2).HeaderText = "Artículo"
                .Columns(2).Width = 110
                .Columns(2).Frozen = True
                '.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Descripción"
                .Columns(3).Width = 180
                '.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .Columns(4).HeaderText = "Línea"
                .Columns(4).Width = 110
                '.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Costo Últ. Compra"
                .Columns(5).Width = 85
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(5).DefaultCellStyle.Format = "$ ###,###,##0.#0"


                .Columns(6).HeaderText = "Lista 09"
                .Columns(6).Width = 75
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(7).HeaderText = "Precio de Lista 0" & CBListaP.SelectedValue.ToString
                .Columns(7).Width = 75
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(8).HeaderText = "Factor"
                .Columns(8).Width = 55
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Format = "##0.##"

                .Columns(9).HeaderText = "Descuento %"
                .Columns(9).Width = 60
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).DefaultCellStyle.Format = "##0.#0"


                .Columns(10).HeaderText = "Cantidad"
                .Columns(10).Width = 60
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(10).DefaultCellStyle.Format = "##0"


                .Columns(11).HeaderText = "Precio Boletin"
                .Columns(11).Width = 75
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(11).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(12).HeaderText = "Factor"
                .Columns(12).Width = 50
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(12).DefaultCellStyle.Format = "##0.##"

                .Columns(13).HeaderText = "StockPue"
                .Columns(13).Width = 75
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(13).DefaultCellStyle.Format = "##0"

                .Columns(14).HeaderText = "StockMer"
                .Columns(14).Width = 75
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(14).DefaultCellStyle.Format = "##0"

                .Columns(15).HeaderText = "StockTux"
                .Columns(15).Width = 75
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(15).DefaultCellStyle.Format = "##0"

                .Columns(16).HeaderText = "Stock Total"
                .Columns(16).Width = 80
                .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(16).DefaultCellStyle.Format = "##0"

                .Columns(17).HeaderText = "Vtas. Totales"
                .Columns(17).Width = 90
                .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(17).DefaultCellStyle.Format = "###,###,##0"

                .Columns(18).HeaderText = "Prom. Mensual"
                .Columns(18).Width = 80
                .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(18).DefaultCellStyle.Format = "###,##0.#0"

                .Columns(19).HeaderText = "Meses Stock"
                .Columns(19).Width = 70
                .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(19).DefaultCellStyle.Format = "###,##0.#0"
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGBoletin_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGBoletin.RowPrePaint
        'SELECT ValidoDesde,ValidoHasta,Articulo,Descripcion,Linea,CostoUltCom,Lista09,PreLista,"
        'Factor1, Descuento, Cantidad, PreBoletin, Factor2, StockPue, StockMer, StockTux, StockTot"

        'DGBoletin.Rows(e.RowIndex).Cells("CostoUltCom").Style.BackColor = Color.LightGray

        DGBoletin.Rows(e.RowIndex).Cells("PreLista").Style.BackColor = Color.LightGray

        DGBoletin.Rows(e.RowIndex).Cells("Descuento").Style.BackColor = Color.Gold

        DGBoletin.Rows(e.RowIndex).Cells("PreBoletin").Style.BackColor = Color.Yellow

        If CBAlmacen.SelectedValue = "01" Then
            DGBoletin.Rows(e.RowIndex).Cells("StockPue").Style.BackColor = Color.LightBlue
        ElseIf CBAlmacen.SelectedValue = "03" Then
            DGBoletin.Rows(e.RowIndex).Cells("StockMer").Style.BackColor = Color.LightBlue
        ElseIf CBAlmacen.SelectedValue = "07" Then
            DGBoletin.Rows(e.RowIndex).Cells("StockTux").Style.BackColor = Color.LightBlue

        Else
            DGBoletin.Rows(e.RowIndex).Cells("StockTot").Style.BackColor = Color.LightBlue
        End If

        DGBoletin.Rows(e.RowIndex).Cells("MesStock").Style.BackColor = Color.LightCoral

        DGBoletin.Rows(e.RowIndex).Cells("VtasT").Style.BackColor = Color.LightGreen

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
        oSheet.Range("A1:C1").Merge(True)
        oSheet.Range("A2:C2").Merge(True)



        'DAR COLOR DE FONDO A CELDAS
        oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
        oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15


        oSheet.Range("A4").INTERIOR.COLORINDEX = 15
        oSheet.Range("B4").INTERIOR.COLORINDEX = 15
        oSheet.Range("C4").INTERIOR.COLORINDEX = 15
        oSheet.Range("D4").INTERIOR.COLORINDEX = 15
        oSheet.Range("E4").INTERIOR.COLORINDEX = 15
        oSheet.Range("F4").INTERIOR.COLORINDEX = 15
        oSheet.Range("G4").INTERIOR.COLORINDEX = 15
        oSheet.Range("H4").INTERIOR.COLORINDEX = 15
        oSheet.Range("I4").INTERIOR.COLORINDEX = 15
        oSheet.Range("J4").INTERIOR.COLORINDEX = 15
        oSheet.Range("K4").INTERIOR.COLORINDEX = 15
        oSheet.Range("L4").INTERIOR.COLORINDEX = 15
        oSheet.Range("M4").INTERIOR.COLORINDEX = 15
        oSheet.Range("N4").INTERIOR.COLORINDEX = 15
        oSheet.Range("O4").INTERIOR.COLORINDEX = 15
        oSheet.Range("P4").INTERIOR.COLORINDEX = 15
        oSheet.Range("Q4").INTERIOR.COLORINDEX = 15
        oSheet.Range("R4").INTERIOR.COLORINDEX = 15
        oSheet.Range("S4").INTERIOR.COLORINDEX = 15
        oSheet.Range("T4").INTERIOR.COLORINDEX = 15



        'Declaramos el nombre de las columnas
        oSheet.range("A4").value = "Válido desde"
        oSheet.range("B4").value = "Válido hasta"
        oSheet.range("C4").value = "Artículo"
        oSheet.range("D4").value = "Descripción"
        oSheet.range("E4").value = "Línea."
        oSheet.range("F4").value = "Costo Últ.Compra"
        oSheet.range("G4").value = "Lista 09"
        oSheet.range("H4").value = "Precio Lista" & CBListaP.SelectedValue
        oSheet.range("I4").value = "Factor"
        oSheet.range("J4").value = "Descuento %"
        oSheet.range("K4").value = "Cantidad"
        oSheet.range("L4").value = "Precio Boletín"
        oSheet.range("M4").value = "Factor"
        oSheet.range("N4").value = "Stock Puebla"
        oSheet.range("O4").value = "Stock Mérida"
        oSheet.range("P4").value = "Stock Tuxtla"
        oSheet.range("Q4").value = "Stock Total"
        oSheet.range("R4").value = "Vtas Totales"
        oSheet.range("S4").value = "Prom. Mensual"
        oSheet.range("T4").value = "Meses Stock"




        'DISEÑO DE EXCEL

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A4:T4").font.bold = True


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
        oExcel.worksheets("Hoja1").Columns("K").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("K").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("L").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("L").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("M").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("M").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("N").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("N").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("O").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("O").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("P").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("P").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("Q").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("Q").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("R").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("R").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("S").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("S").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("T").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("T").Font.Size = 8



        'Cambia el alto de celda 
        oSheet.range("A:T").RowHeight = 13

        'oSheet.range("A:V").HorizontalAlignment = xlCenter

        'TAMAÑO DE COLUMNAS
        oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 12
        oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 16
        oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("L").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("M").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("N").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("O").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("P").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("Q").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("R").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("S").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("T").EntireColumn.ColumnWidth = 10



        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = Me.DGBoletin.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel1 As Date = IIf(IsDBNull(Me.DGBoletin.Item(0, fila_dt).Value), "12/12/1999", Me.DGBoletin.Item(0, fila_dt).Value)
            Dim cel2 As String = IIf(IsDBNull(Me.DGBoletin.Item(1, fila_dt).Value), "12/12/1999", Me.DGBoletin.Item(1, fila_dt).Value)
            Dim cel3 As String = IIf(IsDBNull(Me.DGBoletin.Item(2, fila_dt).Value), 0, Me.DGBoletin.Item(2, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DGBoletin.Item(3, fila_dt).Value), 0, Me.DGBoletin.Item(3, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DGBoletin.Item(4, fila_dt).Value), 0, Me.DGBoletin.Item(4, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DGBoletin.Item(5, fila_dt).Value), 0, Me.DGBoletin.Item(5, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DGBoletin.Item(6, fila_dt).Value), 0, Me.DGBoletin.Item(6, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DGBoletin.Item(7, fila_dt).Value), 0, Me.DGBoletin.Item(7, fila_dt).Value)

            Dim cel9 As String = IIf(IsDBNull(Me.DGBoletin.Item(8, fila_dt).Value), 0, Me.DGBoletin.Item(8, fila_dt).Value)
            Dim cel10 As String = IIf(IsDBNull(Me.DGBoletin.Item(9, fila_dt).Value), 0, Me.DGBoletin.Item(9, fila_dt).Value)

            Dim cel11 As String = IIf(IsDBNull(Me.DGBoletin.Item(10, fila_dt).Value), 0, Me.DGBoletin.Item(10, fila_dt).Value)
            Dim cel12 As String = IIf(IsDBNull(Me.DGBoletin.Item(11, fila_dt).Value), 0, Me.DGBoletin.Item(11, fila_dt).Value)
            Dim cel13 As String = IIf(IsDBNull(Me.DGBoletin.Item(12, fila_dt).Value), 0, Me.DGBoletin.Item(12, fila_dt).Value)
            Dim cel14 As String = IIf(IsDBNull(Me.DGBoletin.Item(13, fila_dt).Value), 0, Me.DGBoletin.Item(13, fila_dt).Value)
            Dim cel15 As String = IIf(IsDBNull(Me.DGBoletin.Item(14, fila_dt).Value), 0, Me.DGBoletin.Item(14, fila_dt).Value)
            Dim cel16 As String = IIf(IsDBNull(Me.DGBoletin.Item(15, fila_dt).Value), 0, Me.DGBoletin.Item(15, fila_dt).Value)
            Dim cel17 As String = IIf(IsDBNull(Me.DGBoletin.Item(16, fila_dt).Value), 0, Me.DGBoletin.Item(16, fila_dt).Value)
            Dim cel18 As String = IIf(IsDBNull(Me.DGBoletin.Item(17, fila_dt).Value), 0, Me.DGBoletin.Item(17, fila_dt).Value)
            Dim cel19 As String = IIf(IsDBNull(Me.DGBoletin.Item(18, fila_dt).Value), 0, Me.DGBoletin.Item(18, fila_dt).Value)
            Dim cel20 As String = IIf(IsDBNull(Me.DGBoletin.Item(19, fila_dt).Value), 0, Me.DGBoletin.Item(19, fila_dt).Value)


            fila_dt_excel = fila_dt + 5 'Renglón en donde se empieza a registrar el reporte

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
            oSheet.range("D" & fila_dt_excel).value = cel4
            oSheet.range("E" & fila_dt_excel).value = cel5
            oSheet.range("F" & fila_dt_excel).value = cel6
            oSheet.range("G" & fila_dt_excel).value = cel7
            oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel8, 2)
            oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
            oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel10, 2)
            oSheet.range("K" & fila_dt_excel).value = cel11
            oSheet.range("L" & fila_dt_excel).value = cel12
            oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel13, 2)
            oSheet.range("N" & fila_dt_excel).value = cel14
            oSheet.range("O" & fila_dt_excel).value = cel15
            oSheet.range("P" & fila_dt_excel).value = cel16
            oSheet.range("Q" & fila_dt_excel).value = cel17
            oSheet.range("R" & fila_dt_excel).value = cel18
            oSheet.range("S" & fila_dt_excel).value = FormatNumber(cel19, 2)
            oSheet.range("T" & fila_dt_excel).value = FormatNumber(cel20, 2)

            oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "$ ###,###,###.##"
            oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "$ ###,###,###.##"
            oExcel.Worksheets("Hoja1").Columns("L").NumberFormat = "$ ###,###,###.##"
            'oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "$ ###,###,###.##"

        Next


        oExcel.Worksheets("Hoja1").Cells.Range("H5:H" + (total_reg + 4).ToString).Interior.ColorIndex = 15
        oExcel.Worksheets("Hoja1").Cells.Range("J5:J" + (total_reg + 4).ToString).Interior.ColorIndex = 44
        oExcel.Worksheets("Hoja1").Cells.Range("L5:L" + (total_reg + 4).ToString).Interior.ColorIndex = 6
        oExcel.Worksheets("Hoja1").Cells.Range("Q5:Q" + (total_reg + 4).ToString).Interior.ColorIndex = 37
        oExcel.Worksheets("Hoja1").Cells.Range("T5:T" + (total_reg + 4).ToString).Interior.ColorIndex = 22
        oExcel.Worksheets("Hoja1").Cells.Range("R5:R" + (total_reg + 4).ToString).Interior.ColorIndex = 35
        'oExcel.Worksheets("Hoja1").Cells.Range("J5:J" + (total_reg + 4).ToString).Interior.ColorIndex = 44

        'Formato de texto para la primera columna CLAVE ART
        oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "@"

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        'oSheet.columns("A:O").entirecolumn.autofit()
        oSheet.range("A1").value = "Reporte de Artículos de Boletín"
        oSheet.range("A2").value = "Lista de precios" + CBListaP.Text


        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing

    End Sub
End Class