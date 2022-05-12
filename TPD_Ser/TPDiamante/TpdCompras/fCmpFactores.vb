Public Class fCmpFactores

    Dim DvProveedoresT As New DataView
    Dim DvLineasT As New DataView
    Dim DvLineas As New DataView

    Private Sub bConsultar_Click(sender As Object, e As EventArgs) Handles bConsultar.Click
        Try
            If rbProveedor.Checked = True Then
                mConsultaProveedor()
            ElseIf rbProveedor.Checked = False Then

                mConsultaLineas()
            End If

            bExcel.Visible = True
        Catch ex As Exception

        End Try
    End Sub


    'Consulta para filtrar por Línea
    Private Sub mConsultaLineas()
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
                'CAST(CASE WHEN [11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [11]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS [Factor L09],[1] AS [Precio L01], " 
                Dim sCadena As String
                sCadena = "SELECT Articulo,Descripcion,Linea,[Stock Pue],[Stock Mer],[Stock Tux],[Stock Total],[L08 Compra],Moneda,TC, " +
                "[Factor11] AS [Factor L09],[1] AS [Precio L01], " +
                "CAST(CASE WHEN [1] <= 0 OR [Factor11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [1]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS [Factor L01], " +
                "CAST(CASE WHEN [2] <= 0 OR [Factor11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [2]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS L02, " +
                "CAST(CASE WHEN [3] <= 0 OR [Factor11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [3]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS L03, " +
                "CAST(CASE WHEN [4] <= 0 OR [Factor11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [4]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS L04, " +
                "CAST(CASE WHEN [5] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [5]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L05, " +
                "CAST(CASE WHEN [6] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [6]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L06,  " +
                "CAST(CASE WHEN [7] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [7]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L07, " +
                "CAST(CASE WHEN [12] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [12]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS L10, " +
                "CAST(CASE WHEN [13] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [13]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS L11, [Proveedor], [Nombre] " +
                "from " +
                "( select T0.ItemCode as Articulo,T1.ItemName as Descripcion,T2.[ItmsGrpNam] AS Linea,  T3.OnHand AS [Stock Pue],T4.OnHand AS [Stock Mer],T7.OnHand AS [Stock Tux], " +
                " CASE WHEN T3.OnHand IS NULL THEN 0 ELSE T3.OnHand END + CASE WHEN T4.OnHand IS NULL THEN 0 ELSE T4.OnHand END + CASE WHEN T7.OnHand IS NULL THEN 0 ELSE T7.OnHand END AS [Stock Total], " +
                "T5.Price AS [L08 Compra],T5.Currency AS Moneda,  T6.Factor AS TC,  T0.PriceList, T0.Price, T8.Factor AS [Factor11], T9.CardCode AS [Proveedor], T9.CardName AS [Nombre] " +
                "from ITM1 T0 INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode " +
                "LEFT JOIN OCRD T9 ON T9.CardCode = T1.CardCode " +
                "INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod " +
                "LEFT JOIN OITW T3 ON T0.ItemCode = T3.ItemCode AND T3.WhsCode = 01 " +
                "LEFT JOIN OITW T4 ON T0.ItemCode = T4.ItemCode AND T4.WhsCode = 03 " +
                "LEFT JOIN OITW T7 ON T0.ItemCode = T7.ItemCode AND T7.WhsCode = 07 " +
                "LEFT JOIN ITM1 T5 ON T0.ItemCode = T5.ItemCode AND T5.PriceList = 8 " +
                "LEFT JOIN ITM1 T8 ON T0.ItemCode = T8.ItemCode AND T8.PriceList = 11 " +
                "LEFT JOIN ITM1 T6 ON T0.ItemCode = T6.ItemCode AND T6.PriceList = 10 "
                If cmbLineas2.SelectedValue <> "0" And cmbProveedor2.SelectedValue <> "0" Then

                    sCadena &= " where T2.ItmsGrpCod = " + cmbLineas2.SelectedValue.ToString + " and T9.CardCode = '" + cmbProveedor2.SelectedValue.ToString + "'"
                ElseIf cmbLineas2.SelectedValue <> "0" And cmbProveedor2.SelectedValue = "0" Then
                    sCadena &= " where T2.ItmsGrpCod = " + cmbLineas2.SelectedValue.ToString
                End If

                sCadena &= " ) as source  " +
                          " pivot ( max(Price) for PriceList in ([1],[2],[3],[4],[5],[6],[7],[11],[12],[13])) as pvt " +
                          " ORDER BY Articulo"
                'ORIGINAL, MODIFICADO POR CAMBIOS DE CALCULO DE FACTORES 26/04/2018, SOLICITADO POR EL LIC. SALVADOR
                'sCadena = " SELECT Articulo,Descripcion,Linea,[Stock Pue],[Stock Mer],[Stock Tux],[Stock Total],[L08 Compra],Moneda,TC, " +
                '          " CAST(CASE WHEN [11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [11]/([L08 Compra] * TC) END AS Decimal(11,5)) AS [Factor L09],[1] AS [Precio L01], " +
                '          " CAST(CASE WHEN [1] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [1]/([L08 Compra] * TC) END AS Decimal(11,5)) AS [Factor L01], " +
                '          " CAST(CASE WHEN [2] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [2]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L02, " +
                '          " CAST(CASE WHEN [3] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [3]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L03, " +
                '          " CAST(CASE WHEN [4] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [4]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L04, " +
                '          " CAST(CASE WHEN [5] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [5]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L05, " +
                '          " CAST(CASE WHEN [6] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [6]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L06, " +
                '          " CAST(CASE WHEN [7] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [7]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L07, " +
                '          " CAST(CASE WHEN [12] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [12]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L10, " +
                '          " CAST(CASE WHEN [13] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [13]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L11 " +
                '          " from ( select T0.ItemCode as Articulo,T1.ItemName as Descripcion,T2.[ItmsGrpNam] AS Linea, " +
                '          " T3.OnHand AS [Stock Pue],T4.OnHand AS [Stock Mer],T7.OnHand AS [Stock Tux], " +
                '          " CASE WHEN T3.OnHand IS NULL THEN 0 ELSE T3.OnHand END +" +
                '          " CASE WHEN T4.OnHand IS NULL THEN 0 ELSE T4.OnHand END + CASE WHEN T7.OnHand IS NULL THEN 0 ELSE T7.OnHand END AS [Stock Total], " +
                '          " T5.Price AS [L08 Compra],T5.Currency AS Moneda, " +
                '          " T6.Factor AS TC, " +
                '          " T0.PriceList,T0.Price " +
                '          " from ITM1  T0 " +
                '          "         INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode " +
                '          "         INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod " +
                '          "         LEFT JOIN OITW T3 ON T0.ItemCode = T3.ItemCode AND T3.WhsCode = 01 " +
                '          "         LEFT JOIN OITW T4 ON T0.ItemCode = T4.ItemCode AND T4.WhsCode = 03 " +
                '          "         LEFT JOIN OITW T7 ON T0.ItemCode = T7.ItemCode AND T7.WhsCode = 07 " +
                '          "         LEFT JOIN ITM1 T5 ON T0.ItemCode = T5.ItemCode AND T5.PriceList = 8 " +
                '          "         LEFT JOIN ITM1 T6 ON T0.ItemCode = T6.ItemCode AND T6.PriceList = 10 "
                'If cmbLinea.SelectedValue <> "0" Then
                '    sCadena &= " where T2.ItmsGrpCod = " + cmbLinea.SelectedValue.ToString
                'End If

                'sCadena &= " ) as source  " +
                '          " pivot ( max(Price) for PriceList in ([1],[2],[3],[4],[5],[6],[7],[11],[12],[13])) as pvt " +
                '          " ORDER BY Articulo"


                'MsgBox(sCadena)
                Dim da As New SqlClient.SqlDataAdapter(sCadena, SqlConnection)

                Dim ds As New DataSet
                da.Fill(ds)
                EstiloGrid(DgRptMargen, ds)
                fformatogrit()
            End Using

        Catch ex As Exception

        End Try
    End Sub



    Private Sub fCmpMargenUtilidad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            rbProveedor.Checked = True

            'BloquearCb()


            'CBProvTODOS()
            'mCargaLinea()

        Catch ex As Exception

        End Try
    End Sub


    Private Sub BloquearCb()
        If rbProveedor.Checked = True Then
            cmbLineas2.Enabled = False
            cmbProveedor2.Enabled = False


        ElseIf rbProveedor.Checked = False Then
            cmbProveedor.Enabled = False
            cmbLinea.Enabled = False
        End If

    End Sub
    Private Sub mCargaLinea()
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                Dim da As New SqlClient.SqlDataAdapter("SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ", SqlConnection)

                Dim ds As New DataSet
                da.Fill(ds)
                ds.Tables(0).Rows.Add(0, "TODOS")
                Me.cmbLinea.DataSource = ds.Tables(0)
                Me.cmbLinea.DisplayMember = "ItmsGrpNam"
                Me.cmbLinea.ValueMember = "ItmsGrpCod"
                Me.cmbLinea.SelectedValue = 0

            End Using

        Catch ex As Exception
            MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub


    Private Sub mCargaLinea2()
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                Dim da As New SqlClient.SqlDataAdapter("SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ", SqlConnection)

                Dim ds As New DataSet
                da.Fill(ds)
                ds.Tables(0).Rows.Add(0, "TODOS")
                Me.cmbLineas2.DataSource = ds.Tables(0)
                Me.cmbLineas2.DisplayMember = "ItmsGrpNam"
                Me.cmbLineas2.ValueMember = "ItmsGrpCod"
                Me.cmbLineas2.SelectedValue = 0

            End Using

        Catch ex As Exception
            MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub

    'Llenar proveedores
    Private Sub CBProvTODOS()
        Try
            Dim ConsutaLista As String

            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                Dim DSetTablas As New DataSet

                ConsutaLista = "SELECT T0.cardcode, T1.cardname FROM SBO_TPD.DBO.OITM T0 "
                ConsutaLista &= "inner JOIN SBO_TPD.DBO.OCRD T1 ON T0.CARDCODE = T1.CardCode  "
                ConsutaLista &= "GROUP BY T0.CardCode, T1.CardName "


                Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


                daAgte.Fill(DSetTablas, "Proveedores")

                Dim filaAgte As Data.DataRow

                'Asignamos a fila la nueva Row(Fila)del Dataset
                filaAgte = DSetTablas.Tables("Proveedores").NewRow

                'Agregamos los valores a los campos de la tabla
                filaAgte("cardname") = "TODOS"
                filaAgte("cardcode") = "999"
                'filaAgte("itmsgrpcod") = 999

                'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                DSetTablas.Tables("Proveedores").Rows.Add(filaAgte)

                DvProveedoresT.Table = DSetTablas.Tables("Proveedores")

                Me.cmbProveedor.DataSource = DvProveedoresT
                Me.cmbProveedor.DisplayMember = "cardname"
                Me.cmbProveedor.ValueMember = "cardcode"
                Me.cmbProveedor.SelectedValue = "999"

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
            'MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub

    Private Sub mCargaLineaProveddor()
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                Dim da As New SqlClient.SqlDataAdapter("select distinct T1.ItmsGrpCod,T2.ItmsGrpNam from OITM T1  left join OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod where CardCode = '" & Trim(Me.cmbProveedor.SelectedValue.ToString) & "'", SqlConnection)

                Dim ds As New DataSet
                da.Fill(ds)
                ds.Tables(0).Rows.Add(0, "TODOS")
                Me.cmbLinea.DataSource = ds.Tables(0)
                Me.cmbLinea.DisplayMember = "ItmsGrpNam"
                Me.cmbLinea.ValueMember = "ItmsGrpCod"
                Me.cmbLinea.SelectedValue = 0

            End Using

        Catch ex As Exception
            MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub


    Private Sub lineaProveedor()
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                Dim da As New SqlClient.SqlDataAdapter("select  distinct T1.CardCode,t3.cardname from OITM T1  left join OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod left join OCRD T3 ON T3.Cardcode = t1.cardcode where t2.ItmsGrpNam = '" & Trim(Me.cmbLineas2.Text.ToString) & "'", SqlConnection)

                Dim ds As New DataSet
                da.Fill(ds)
                ds.Tables(0).Rows.Add(0, "TODOS")
                Me.cmbProveedor2.DataSource = ds.Tables(0)
                Me.cmbProveedor2.DisplayMember = "cardname"
                Me.cmbProveedor2.ValueMember = "CardCode"
                Me.cmbProveedor2.SelectedValue = 0

            End Using

        Catch ex As Exception
            MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub



    Private Sub fformatogrit()
        Try
            With DgRptMargen

                ' Articulo
                '.Columns(0).HeaderText = "Articulo"
                .Columns(0).Width = 125
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                ' Descripcion
                ' .Columns(1).HeaderText = "Descripcion"
                .Columns(1).Width = 200
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                ' Linea
                '.Columns(2).HeaderText = "Linea"
                .Columns(2).Width = 100
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Stock Pue
                '.Columns(3).HeaderText = "Stock Pue"
                .Columns(3).Width = 40
                .Columns(3).DefaultCellStyle.Format = "###,###,###"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Stock Mer
                '.Columns(4).HeaderText = "Stock Mer"
                .Columns(4).Width = 40
                .Columns(4).DefaultCellStyle.Format = "###,###,###"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Stock Tux
                '.Columns(4).HeaderText = "Stock Mer"
                .Columns(5).Width = 40
                .Columns(5).DefaultCellStyle.Format = "###,###,###"
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Stock Total
                '.Columns(5).HeaderText = "Stock Total"
                .Columns(6).Width = 40
                .Columns(6).DefaultCellStyle.Format = "###,###,###"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'L08 Compra
                '.Columns(6).HeaderText = "L08 Compra"
                .Columns(7).Width = 80
                .Columns(7).DefaultCellStyle.Format = "$ ###,###,###.0000"
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Moneda
                '.Columns(7).HeaderText = "Moneda"
                .Columns(8).Width = 50
                .Columns(8).DefaultCellStyle.Format = "$ ###,###,###.00"
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'TC
                '.Columns(8).HeaderText = "TC"
                .Columns(9).Width = 55
                .Columns(9).DefaultCellStyle.Format = "$ ###,###,###.00"
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Factor L09 
                '.Columns(9).HeaderText = "Factor L09"
                .Columns(10).Width = 50
                .Columns(10).DefaultCellStyle.Format = "###,###,###.0000"
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Dias de pago 
                '.Columns(10).HeaderText = "PrecioL01"
                .Columns(11).Width = 77
                .Columns(11).DefaultCellStyle.Format = "$ ###,###,###.00"
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Dias de pago 
                '.Columns(11).HeaderText = "Factor L01"
                .Columns(12).Width = 49
                .Columns(12).DefaultCellStyle.Format = "###,###,###.0000"
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Dias de pago 
                '.Columns(12).HeaderText = "L02"
                .Columns(13).Width = 49
                .Columns(13).DefaultCellStyle.Format = "###,###,###.0000"
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Dias de pago 
                '.Columns(13).HeaderText = "L03"
                .Columns(14).Width = 49
                .Columns(14).DefaultCellStyle.Format = "###,###,###.0000"
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Dias de pago 
                '.Columns(14).HeaderText = "L04"
                .Columns(15).Width = 49
                .Columns(15).DefaultCellStyle.Format = "###,###,###.0000"
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Dias de pago 
                '.Columns(15).HeaderText = "L05"
                .Columns(16).Width = 49
                .Columns(16).DefaultCellStyle.Format = "###,###,###.0000"
                .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Dias de pago 
                '.Columns(16).HeaderText = "L06"
                .Columns(17).Width = 49
                .Columns(17).DefaultCellStyle.Format = "###,###,###.0000"
                .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Dias de pago 
                '.Columns(17).HeaderText = "L07"
                .Columns(18).Width = 49
                .Columns(18).DefaultCellStyle.Format = "###,###,###.0000"
                .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Dias de pago 
                ' .Columns(18).HeaderText = "L10"
                .Columns(19).Width = 49
                .Columns(19).DefaultCellStyle.Format = "###,###,###.0000"
                .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'L11 
                '.Columns(19).HeaderText = "L11"
                .Columns(20).Width = 49
                .Columns(20).DefaultCellStyle.Format = "###,###,###.0000"
                .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub bExcel_Click(sender As Object, e As EventArgs) Handles bExcel.Click
        Try

            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

            'Añadimos el Libro al programa
            exLibro = exApp.Workbooks.Add

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DgRptMargen.ColumnCount
            Dim NRow As Integer = DgRptMargen.RowCount

            fFormatoExcel(exLibro, NRow)

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                exLibro.Worksheets("Hoja1").Cells.Item(5, i) = DgRptMargen.Columns(i - 1).Name.ToString
            Next

            For Fila As Integer = 0 To NRow - 1

                For Col As Integer = 0 To NCol - 1
                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 6, Col + 1) = DgRptMargen.Rows(Fila).Cells(Col).Value

                Next
                Estatus.Visible = True
                ProgressBar1.Value = (Fila * 100) / NRow
            Next
            Estatus.Visible = False

            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exLibro.Worksheets("Hoja1").Rows.Item(5).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Rows.Item(5).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Cells.Range("A5:W5").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Rows.Item(5).WrapText = True
            'exLibro.Worksheets("Hoja1").Columns.AutoFit()
            exLibro.Worksheets("Hoja1").name = "Reporte de margen de utilidad "


            'Aplicación visible
            exLibro.Worksheets.Application.Visible = True

            exLibro = Nothing
            exApp = Nothing

        Catch ex As Exception

        End Try
    End Sub


    Private Sub fFormatoExcel(exLibro As Microsoft.Office.Interop.Excel.Workbook, NRow As Integer)
        Try
            ''Combinamos celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1:W1").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A2:W2").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A3:W3").Merge(True)

            ''aplicamos un color de fondo ala celda o rango de celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15

            ''Cambiamos orientacion ala hola
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Reporte de margen de utilidad"
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Linea: " + cmbLinea.Text
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Fecha: " + Date.Now.ToShortDateString


            exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(5, 1).Font.Bold = 1

            exLibro.Worksheets("Hoja1").Columns(11).NumberFormat = "###.0000"
            exLibro.Worksheets("Hoja1").Columns("L:U").NumberFormat = "###.0000"
            exLibro.Worksheets("Hoja1").Columns("D:G").NumberFormat = "###,###,###"
            exLibro.Worksheets("Hoja1").Columns("H").NumberFormat = "$ ###,###.0000"
            exLibro.Worksheets("Hoja1").Columns("J").NumberFormat = "$ ###,###.00"
            exLibro.Worksheets("Hoja1").Columns("L").NumberFormat = "$ ###,###.00"
            exLibro.Worksheets("Hoja1").Columns("A").NumberFormat = "@"

            exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 15
            exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 45
            exLibro.Worksheets("Hoja1").Columns("D:G").EntireColumn.ColumnWidth = 5
            exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 9.5
            exLibro.Worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 7.86
            exLibro.Worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 6
            exLibro.Worksheets("Hoja1").Columns("L").EntireColumn.ColumnWidth = 8.5



            exLibro.Worksheets("Hoja1").Cells.Range("K5:K" + (NRow + 5).ToString).Interior.ColorIndex = 44
            exLibro.Worksheets("Hoja1").Cells.Range("L5:L" + (NRow + 5).ToString).Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("M5:M" + (NRow + 5).ToString).Interior.ColorIndex = 6

            exLibro.Worksheets("Hoja1").Columns("M:U").EntireColumn.ColumnWidth = 6
            exLibro.Worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 6

            exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 8
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DgRptMargen_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DgRptMargen.RowPostPaint

        DgRptMargen.Rows(e.RowIndex).Cells("Factor L09").Style.BackColor = Color.Gold
        DgRptMargen.Rows(e.RowIndex).Cells("Precio L01").Style.BackColor = Color.DarkGray
        DgRptMargen.Rows(e.RowIndex).Cells("Factor L01").Style.BackColor = Color.Yellow

    End Sub

    Private Sub lGrpArticulos_Click(sender As Object, e As EventArgs) Handles lGrpArticulos.Click

    End Sub



    Private Sub rbProveedor_CheckedChanged(sender As Object, e As EventArgs) Handles rbProveedor.CheckedChanged

        cmbLineas2.Enabled = False
        cmbProveedor2.Enabled = False
        cmbProveedor.Enabled = True
        cmbLinea.Enabled = True
        CBProvTODOS()





    End Sub

    Private Sub cmbProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProveedor.SelectedIndexChanged

        mCargaLineaProveddor()

    End Sub

    Private Sub rbLinea_CheckedChanged(sender As Object, e As EventArgs) Handles rbLinea.CheckedChanged
        cmbLineas2.Enabled = True
        cmbProveedor2.Enabled = True
        cmbProveedor.Enabled = False
        cmbLinea.Enabled = False

        'cmbProveedor.DataSource = Nothing
        mCargaLinea2()


    End Sub


    Private Sub mCargaProveedorLinea()
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                Dim da As New SqlClient.SqlDataAdapter("select  distinct t3.cardname,T1.CardCode from OITM T1  left join OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod left join OCRD T3 ON T3.Cardcode = t1.cardcode where t2.ItmsGrpCod =" & Trim(Me.cmbLineas2.SelectedValue) & "", SqlConnection)

                Dim ds As New DataSet
                da.Fill(ds)
                ds.Tables(0).Rows.Add(0, "TODOS")
                Me.cmbLinea.DataSource = ds.Tables(0)
                Me.cmbProveedor2.DisplayMember = "cardname"
                Me.cmbProveedor2.ValueMember = "CardCode"
                Me.cmbProveedor2.SelectedValue = 0

            End Using

        Catch ex As Exception
            MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub

    'Private Sub cmbLinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLinea.SelectedIndexChanged
    '    If rbProveedor.Checked = True Then

    '    ElseIf rbLinea.Checked = True Then
    '        mCargaProveedorLinea()
    '    End If

    'End Sub
    Private Sub CBProvTODOSLinea()
        Try
            Dim ConsutaLista As String

            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                Dim DSetTablas As New DataSet

                ConsutaLista = "select  distinct T1.CardCode,t3.cardname from SBO_TPD.DBO.OITM T1  left join SBO_TPD.DBO.OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod left join SBO_TPD.DBO.OCRD T3 ON T3.Cardcode = t1.cardcode "
                ConsutaLista &= "where t2.ItmsGrpCod ='" + Me.cmbLinea.SelectedValue.ToString + "'"
                '+ cmbLinea.SelectedValue + ""
                'ConsutaLista &= "GROUP BY T0.CardCode, T1.CardName "


                Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


                daAgte.Fill(DSetTablas, "Proveedores")

                Dim filaAgte As Data.DataRow

                'Asignamos a fila la nueva Row(Fila)del Dataset
                filaAgte = DSetTablas.Tables("Proveedores").NewRow

                'Agregamos los valores a los campos de la tabla
                filaAgte("cardname") = "TODOS"
                filaAgte("cardcode") = "999"
                'filaAgte("itmsgrpcod") = 999

                'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                DSetTablas.Tables("Proveedores").Rows.Add(filaAgte)

                DvProveedoresT.Table = DSetTablas.Tables("Proveedores")

                Me.cmbProveedor2.DataSource = DvProveedoresT
                Me.cmbProveedor2.DisplayMember = "cardname"
                Me.cmbProveedor2.ValueMember = "cardcode"
                Me.cmbProveedor2.SelectedValue = "999"

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
            'MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub

    Private Sub cmbLineas2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLineas2.SelectedIndexChanged
        ' mCargaProveedorLinea()
        lineaProveedor()
    End Sub

    Private Sub cmbLinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLinea.SelectedIndexChanged

    End Sub

    'Consulta para filtrar por Proveedor
    Private Sub mConsultaProveedor()
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
                'CAST(CASE WHEN [11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [11]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS [Factor L09],[1] AS [Precio L01], " 
                Dim sCadena As String
                sCadena = "SELECT Articulo,Descripcion,Linea,[Stock Pue],[Stock Mer],[Stock Tux],[Stock Total],[L08 Compra],Moneda,TC, " +
                "[Factor11] AS [Factor L09],[1] AS [Precio L01], " +
                "CAST(CASE WHEN [1] <= 0 OR [Factor11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [1]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS [Factor L01], " +
                "CAST(CASE WHEN [2] <= 0 OR [Factor11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [2]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS L02, " +
                "CAST(CASE WHEN [3] <= 0 OR [Factor11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [3]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS L03, " +
                "CAST(CASE WHEN [4] <= 0 OR [Factor11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [4]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS L04, " +
                "CAST(CASE WHEN [5] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [5]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L05, " +
                "CAST(CASE WHEN [6] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [6]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L06,  " +
                "CAST(CASE WHEN [7] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [7]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L07, " +
                "CAST(CASE WHEN [12] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [12]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS L10, " +
                "CAST(CASE WHEN [13] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [13]/([L08 Compra] * TC * [Factor11]) END AS Decimal(11,5)) AS L11, [Proveedor], [Nombre] " +
                "from " +
                "( select T0.ItemCode as Articulo,T1.ItemName as Descripcion,T2.[ItmsGrpNam] AS Linea,  T3.OnHand AS [Stock Pue],T4.OnHand AS [Stock Mer],T7.OnHand AS [Stock Tux], " +
                " CASE WHEN T3.OnHand IS NULL THEN 0 ELSE T3.OnHand END + CASE WHEN T4.OnHand IS NULL THEN 0 ELSE T4.OnHand END + CASE WHEN T7.OnHand IS NULL THEN 0 ELSE T7.OnHand END AS [Stock Total], " +
                "T5.Price AS [L08 Compra],T5.Currency AS Moneda,  T6.Factor AS TC,  T0.PriceList, T0.Price, T8.Factor AS [Factor11], T9.CardCode AS [Proveedor], T9.CardName AS [Nombre] " +
                "from ITM1 T0 INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode " +
                "LEFT JOIN OCRD T9 ON T9.CardCode = T1.CardCode " +
                "INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod " +
                "LEFT JOIN OITW T3 ON T0.ItemCode = T3.ItemCode AND T3.WhsCode = 01 " +
                "LEFT JOIN OITW T4 ON T0.ItemCode = T4.ItemCode AND T4.WhsCode = 03 " +
                "LEFT JOIN OITW T7 ON T0.ItemCode = T7.ItemCode AND T7.WhsCode = 07 " +
                "LEFT JOIN ITM1 T5 ON T0.ItemCode = T5.ItemCode AND T5.PriceList = 8 " +
                "LEFT JOIN ITM1 T8 ON T0.ItemCode = T8.ItemCode AND T8.PriceList = 11 " +
                "LEFT JOIN ITM1 T6 ON T0.ItemCode = T6.ItemCode AND T6.PriceList = 10 "
                If cmbProveedor.SelectedValue <> "0" And cmbLinea.SelectedValue <> "0" Then

                    sCadena &= " where T9.CardCode = '" + cmbProveedor.SelectedValue.ToString + "'" + " and T2.ItmsGrpCod = " + cmbLinea.SelectedValue.ToString

                    'sCadena &= " where T2.ItmsGrpCod = " + cmbLineas2.SelectedValue.ToString + " and T9.CardCode = '" + cmbProveedor2.SelectedValue.ToString + "'"
                ElseIf cmbProveedor.SelectedValue <> "0" And cmbLinea.SelectedValue = "0" Then
                    sCadena &= " where T9.CardCode = '" + cmbProveedor.SelectedValue.ToString + "'"
                End If

                sCadena &= " ) as source  " +
                          " pivot ( max(Price) for PriceList in ([1],[2],[3],[4],[5],[6],[7],[11],[12],[13])) as pvt " +
                          " ORDER BY Articulo"
                'ORIGINAL, MODIFICADO POR CAMBIOS DE CALCULO DE FACTORES 26/04/2018, SOLICITADO POR EL LIC. SALVADOR
                'sCadena = " SELECT Articulo,Descripcion,Linea,[Stock Pue],[Stock Mer],[Stock Tux],[Stock Total],[L08 Compra],Moneda,TC, " +
                '          " CAST(CASE WHEN [11] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [11]/([L08 Compra] * TC) END AS Decimal(11,5)) AS [Factor L09],[1] AS [Precio L01], " +
                '          " CAST(CASE WHEN [1] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [1]/([L08 Compra] * TC) END AS Decimal(11,5)) AS [Factor L01], " +
                '          " CAST(CASE WHEN [2] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [2]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L02, " +
                '          " CAST(CASE WHEN [3] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [3]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L03, " +
                '          " CAST(CASE WHEN [4] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [4]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L04, " +
                '          " CAST(CASE WHEN [5] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [5]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L05, " +
                '          " CAST(CASE WHEN [6] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [6]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L06, " +
                '          " CAST(CASE WHEN [7] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [7]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L07, " +
                '          " CAST(CASE WHEN [12] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [12]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L10, " +
                '          " CAST(CASE WHEN [13] <= 0 OR [L08 Compra] <= 0 OR TC <= 0 THEN 0  ELSE [13]/([L08 Compra] * TC) END AS Decimal(11,5)) AS L11 " +
                '          " from ( select T0.ItemCode as Articulo,T1.ItemName as Descripcion,T2.[ItmsGrpNam] AS Linea, " +
                '          " T3.OnHand AS [Stock Pue],T4.OnHand AS [Stock Mer],T7.OnHand AS [Stock Tux], " +
                '          " CASE WHEN T3.OnHand IS NULL THEN 0 ELSE T3.OnHand END +" +
                '          " CASE WHEN T4.OnHand IS NULL THEN 0 ELSE T4.OnHand END + CASE WHEN T7.OnHand IS NULL THEN 0 ELSE T7.OnHand END AS [Stock Total], " +
                '          " T5.Price AS [L08 Compra],T5.Currency AS Moneda, " +
                '          " T6.Factor AS TC, " +
                '          " T0.PriceList,T0.Price " +
                '          " from ITM1  T0 " +
                '          "         INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode " +
                '          "         INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod " +
                '          "         LEFT JOIN OITW T3 ON T0.ItemCode = T3.ItemCode AND T3.WhsCode = 01 " +
                '          "         LEFT JOIN OITW T4 ON T0.ItemCode = T4.ItemCode AND T4.WhsCode = 03 " +
                '          "         LEFT JOIN OITW T7 ON T0.ItemCode = T7.ItemCode AND T7.WhsCode = 07 " +
                '          "         LEFT JOIN ITM1 T5 ON T0.ItemCode = T5.ItemCode AND T5.PriceList = 8 " +
                '          "         LEFT JOIN ITM1 T6 ON T0.ItemCode = T6.ItemCode AND T6.PriceList = 10 "
                'If cmbLinea.SelectedValue <> "0" Then
                '    sCadena &= " where T2.ItmsGrpCod = " + cmbLinea.SelectedValue.ToString
                'End If

                'sCadena &= " ) as source  " +
                '          " pivot ( max(Price) for PriceList in ([1],[2],[3],[4],[5],[6],[7],[11],[12],[13])) as pvt " +
                '          " ORDER BY Articulo"


                'MsgBox(sCadena)
                Dim da As New SqlClient.SqlDataAdapter(sCadena, SqlConnection)

                Dim ds As New DataSet
                da.Fill(ds)
                EstiloGrid(DgRptMargen, ds)
                fformatogrit()
            End Using

        Catch ex As Exception

        End Try
    End Sub










End Class