
Imports System.Data.SqlClient
Imports System.Data

Public Class ValoracionInv

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGinv.CellContentClick

    End Sub

    Private Sub BtnActualizar_Click(sender As Object, e As EventArgs) Handles BtnActualizar.Click
        Dim DTRefacciones As New DataTable

        '****************************************************************************************************************************

        ' crear nueva conexión    
        Dim conexion2 As New SqlConnection(StrCon)

        ' abrir la conexión con la base de datos   
        conexion2.Open()

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand

        Dim SQLTPD As String

        '--traco

        SQLTPD = "SELECT T0.WhsName AS 'Almacen',SUM(T1.OnHand*Price) AS 'Valor' "
        SQLTPD &= "INTO #ValInv "
        SQLTPD &= "FROM SBO_TPD.dbo.OWHS T0 "
        SQLTPD &= "LEFT JOIN SBO_TPD.dbo.OITW T1 ON T0.WhsCode = T1.WhsCode "
        SQLTPD &= "LEFT JOIN SBO_TPD.dbo.ITM1 T2 ON T1.ItemCode = T2.ItemCode AND T2.PriceList = 11 "
        SQLTPD &= "WHERE T0.WhsCode IN ('01','03','07') "
        SQLTPD &= "GROUP BY T0.WhsName,T0.WhsCode "
        SQLTPD &= "ORDER BY T0.WhsCode "


        SQLTPD &= "SELECT 'PUEBLA' AS 'Almacen',SUM(Importe) AS Importe INTO #VtasPue "
        SQLTPD &= "FROM (SELECT ((DocTotal - VatSum) - TotalExpns)  AS Importe "
        SQLTPD &= "FROM [SBO_TPD].[dbo].[OINV] "
        SQLTPD &= "WHERE DocDate >= @FechaIni AND DocDate <= @FechaFin "
        'SQLTPD &= "AND SlpCode <> 8 AND SlpCode <> 12 AND SlpCode <> 26 AND SlpCode <> 17 AND SlpCode <> 20 "
        SQLTPD &= " AND SlpCode in (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '01' OR TT1.SlpCode = -1) "

        If CkCliPro.Checked = False Then
            SQLTPD &= "AND SlpCode <> 1 "
        End If

        SQLTPD &= "UNION ALL "
        SQLTPD &= "SELECT ((DocTotal - VatSum) - TotalExpns) * -1  AS Importe "
        SQLTPD &= "FROM [SBO_TPD].[dbo].[ORIN] "
        SQLTPD &= "WHERE DocType  = 'I'  "
        SQLTPD &= "AND DocDate >= @FechaIni AND DocDate <= @FechaFin "
        'SQLTPD &= "AND SlpCode <> 8 AND SlpCode <> 12 AND SlpCode <> 26 AND SlpCode <> 17 AND SlpCode <> 20 "
        SQLTPD &= " AND SlpCode in (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '01' OR TT1.SlpCode = -1) "

        If CkCliPro.Checked = False Then
            SQLTPD &= "AND SlpCode <> 1 "
        End If

        SQLTPD &= ") tmp   "

        SQLTPD &= "SELECT 'MÉRIDA' AS 'Almacen',SUM(Importe) AS Importe INTO #VtasMer "
        SQLTPD &= "FROM (SELECT ((DocTotal - VatSum) - TotalExpns)  AS Importe "
        SQLTPD &= "FROM [SBO_TPD].[dbo].[OINV] "
        SQLTPD &= "WHERE DocDate >= @FechaIni AND DocDate <= @FechaFin "

        If CkCliPro.Checked = False Then
            SQLTPD &= "AND SlpCode <> 1 "
        End If

        'SQLTPD &= "AND (SlpCode = 17 or SlpCode = 20) "
        SQLTPD &= " AND SlpCode in (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '03' ) "
        SQLTPD &= "UNION ALL "
        SQLTPD &= "SELECT ((DocTotal - VatSum) - TotalExpns) * -1  AS Importe "
        SQLTPD &= "FROM [SBO_TPD].[dbo].[ORIN] "
        SQLTPD &= "WHERE DocType  = 'I'  "
        SQLTPD &= "AND DocDate >= @FechaIni AND DocDate <= @FechaFin "

        If CkCliPro.Checked = False Then
            SQLTPD &= "AND SlpCode <> 1 "
        End If

        'SQLTPD &= "AND (SlpCode = 17 or SlpCode = 20) "
        SQLTPD &= " AND SlpCode in (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '03' ) "
        SQLTPD &= ") tmp   "

        SQLTPD &= "SELECT 'TUXTLA GTZ' AS 'Almacen',SUM(Importe) AS Importe INTO #VtasTux "
        SQLTPD &= "FROM (SELECT ((DocTotal - VatSum) - TotalExpns)  AS Importe "
        SQLTPD &= "FROM [SBO_TPD].[dbo].[OINV] "
        SQLTPD &= "WHERE DocDate >= @FechaIni AND DocDate <= @FechaFin  "

        If CkCliPro.Checked = False Then
            SQLTPD &= "AND SlpCode <> 1 "
        End If

        'SQLTPD &= "AND (SlpCode = 8 or SlpCode = 12 or SlpCode = 26) "
        SQLTPD &= " AND SlpCode in (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '07' ) "
        SQLTPD &= "UNION ALL "
        SQLTPD &= "SELECT ((DocTotal - VatSum) - TotalExpns) * -1  AS Importe "
        SQLTPD &= "FROM [SBO_TPD].[dbo].[ORIN] "
        SQLTPD &= "WHERE DocType  = 'I' "
        SQLTPD &= "AND DocDate >= @FechaIni AND DocDate <= @FechaFin "

        If CkCliPro.Checked = False Then
            SQLTPD &= "AND SlpCode <> 1 "
        End If

        'SQLTPD &= "AND (SlpCode = 8 or SlpCode = 12 or SlpCode = 26) "
        SQLTPD &= " AND SlpCode in (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '07' ) "
        SQLTPD &= ") tmp  "


        SQLTPD &= "DECLARE @TotVal DECIMAL = (SELECT SUM(VALOR) FROM #ValInv) "
        SQLTPD &= "DECLARE @TotVen DECIMAL = ((SELECT SUM(IMPORTE) FROM #VtasPue)+(SELECT SUM(IMPORTE) FROM #VtasMer)+(SELECT SUM(IMPORTE) FROM #VtasTux))  "


        SQLTPD &= "SELECT T0.Almacen AS 'Almacen',T0.Valor AS 'Valor',T0.Valor/@TotVal AS 'PorValor', "
        SQLTPD &= "CASE WHEN T0.Almacen ='PUEBLA' THEN T1.Importe "
        SQLTPD &= "WHEN T0.Almacen ='MÉRIDA' THEN T2.Importe "
        SQLTPD &= "WHEN T0.Almacen ='TUXTLA GTZ' THEN T3.Importe "
        SQLTPD &= "END AS 'Ventas', "
        SQLTPD &= "CASE WHEN T0.Almacen ='PUEBLA' THEN T1.Importe/@TotVen "
        SQLTPD &= "WHEN T0.Almacen ='MÉRIDA' THEN T2.Importe/@TotVen "
        SQLTPD &= "WHEN T0.Almacen ='TUXTLA GTZ' THEN T3.Importe/@TotVen "
        SQLTPD &= "END AS 'PorVentas' "
        SQLTPD &= "INTO #ValInvFin "
        SQLTPD &= "FROM #ValInv T0 "
        SQLTPD &= "LEFT JOIN #VtasPue T1 ON T0.Almacen=T1.Almacen "
        SQLTPD &= "LEFT JOIN #VtasMer T2 ON T0.Almacen=T2.Almacen "
        SQLTPD &= "LEFT JOIN #VtasTux T3 ON T0.Almacen=T3.Almacen "

        SQLTPD &= "SELECT Almacen,Valor,PorValor,Ventas,PorVentas "
        SQLTPD &= "FROM #ValInvFin "
        SQLTPD &= "UNION ALL "
        SQLTPD &= "SELECT 'Totales',SUM(VALOR),SUM(PorValor),SUM(Ventas),SUM(PorVentas) "
        SQLTPD &= "FROM #ValInvFin "



        SQLTPD &= "DROP TABLE #ValInv "
        SQLTPD &= "DROP TABLE #VtasPue "
        SQLTPD &= "DROP TABLE #VtasMer "
        SQLTPD &= "DROP TABLE #VtasTux "
        SQLTPD &= "DROP TABLE #ValInvFin "

        ' Nuevo objeto Dataset   
        Dim DsVtasDet As New DataSet

        DsVtasDet.Tables.Add(DTRefacciones)


        With comando
            'If Me.CmbAgteVta.SelectedValue <> 999 Then
            '    .Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
            'End If
            .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
            .Parameters.AddWithValue("@FechaFin", Me.DtpFechaTer.Value)

            ' Asignar el sql para seleccionar los datos de la tabla Maestro   
            .CommandText = SQLTPD
            .Connection = conexion2
        End With

        '/***************************parte de codigo'
        With Adaptador
            .SelectCommand = comando
            ' llenar el dataset   
            '.TableMappings.Add("DetLinea", "DetArticulo")
            .Fill(DsVtasDet, "ValoracionInv")
        End With


        DsVtasDet.Tables(1).TableName = "ValInv"
        'DsVtasDet.Tables(2).TableName = "DetalleAgtes"



        '**********************************************************************************************************************************

        Dim DtAgte As New DataTable
        DtAgte = DsVtasDet.Tables("ValInv")

        'DataGridView1.DataSource = DtAgte


        With Me.DGinv
            .DataSource = DtAgte
            .ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = False
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = True
            .AllowUserToAddRows = False

            .Columns(0).HeaderText = "Almacén"
            .Columns(0).Width = 110
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(1).HeaderText = "Existencia ($)"
            .Columns(1).Width = 110
            .Columns(1).DefaultCellStyle.Format = "$ ###,###,###.#0"
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(2).HeaderText = "Existencia (%)"
            .Columns(2).Width = 110
            .Columns(2).DefaultCellStyle.Format = "##.#0 %"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(3).HeaderText = "Ventas ($)"
            .Columns(3).Width = 110
            .Columns(3).DefaultCellStyle.Format = "$ ###,###,###.#0"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(4).HeaderText = "Ventas (%)"
            .Columns(4).Width = 110
            .Columns(4).DefaultCellStyle.Format = "##.#0 %"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End With


        With conexion2
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With
    End Sub

    Private Sub ValoracionInv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

        CkCliPro.Checked = True

    End Sub

    Private Sub BtnAgentes_Click(sender As Object, e As EventArgs) Handles BtnAgentes.Click
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

        oSheet.range("A1").value = "Reporte de valoración de inventarios"
        oSheet.range("A2").value = "Ventas del " + DtpFechaIni.Value + "al " + DtpFechaIni.Value


        'DAR COLOR DE FONDO A CELDAS
        oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
        oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15

        oSheet.Range("A4").INTERIOR.COLORINDEX = 15
        oSheet.Range("B4").INTERIOR.COLORINDEX = 15
        oSheet.Range("C4").INTERIOR.COLORINDEX = 15
        oSheet.Range("D4").INTERIOR.COLORINDEX = 15
        oSheet.Range("E4").INTERIOR.COLORINDEX = 15
        'oSheet.Range("F6").INTERIOR.COLORINDEX = 6
        'oSheet.Range("G6").INTERIOR.COLORINDEX = 45
        'oSheet.Range("H6").INTERIOR.COLORINDEX = 46
        'oSheet.Range("I6").INTERIOR.COLORINDEX = 3


        'Declaramos el nombre de las columnas
        oSheet.range("A4").value = "Almacén"
        oSheet.range("B4").value = "Valor Inv. $"
        oSheet.range("C4").value = "Valor Inv. %"
        oSheet.range("D4").value = "Ventas $"
        oSheet.range("E4").value = "Ventas %"


        'DISEÑO DE EXCEL

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A4:E4").font.bold = True


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


        'Cambia el alto de celda 
        oSheet.range("A:E").RowHeight = 13

        'oSheet.range("A:V").HorizontalAlignment = xlCenter

        'TAMAÑO DE COLUMNAS
        oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 12
        oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 15
        oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 15
        oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 10

        oExcel.Worksheets("Hoja1").Columns("B").NumberFormat = "$ ###,###,###.##"
        oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "% ##.##"
        oExcel.Worksheets("Hoja1").Columns("D").NumberFormat = "$ ###,###,###.##"
        oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "% ##.##"




        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = Me.DGinv.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel1 As String = Me.DGinv.Item(0, fila_dt).Value
            Dim cel2 As String = IIf(IsDBNull(Me.DGinv.Item(1, fila_dt).Value), 0, Me.DGinv.Item(1, fila_dt).Value)
            Dim cel3 As String = IIf(IsDBNull(Me.DGinv.Item(2, fila_dt).Value), 0, Me.DGinv.Item(2, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DGinv.Item(3, fila_dt).Value), 0, Me.DGinv.Item(3, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DGinv.Item(4, fila_dt).Value), 0, Me.DGinv.Item(4, fila_dt).Value)
            'Dim cel6 As String = IIf(IsDBNull(Me.DGinv.Item(5, fila_dt).Value), 0, Me.DGinv.Item(5, fila_dt).Value)
            'Dim cel7 As String = IIf(IsDBNull(Me.DGinv.Item(6, fila_dt).Value), 0, Me.DGinv.Item(6, fila_dt).Value)

            fila_dt_excel = fila_dt + 5 'Renglón en donde se empieza a registrar el reporte

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
            oSheet.range("D" & fila_dt_excel).value = cel4
            oSheet.range("E" & fila_dt_excel).value = cel5


        Next


        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        'oSheet.columns("A:O").entirecolumn.autofit()


        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing
    End Sub
End Class