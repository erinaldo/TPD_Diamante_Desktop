Imports System.Data.SqlClient

Public Class frmVentasNetasTotales
    'VARIABLE PARA FECHA INICIAL OBTENIDA
    Dim fi As String
    'VARIABLE PARA FECHA FINAL OBTENIDA
    Dim ff As String
    'FECHA FINAL PERO OBTENIDA EN TIPO DE DATO DATE PARA SU COMPROBACION
    Dim Inicio As Date
    'FECHA FINAL PERO OBTENIDA EN TIPO DE DATO DATE PARA SU COMPROBACION
    Dim Final As Date
    'DECLARA DATASET PARA PROCEDIMIENTO DE OBTENCION DE VENTAS
    Dim DsVentas As DataSet
    'Dataview contenedor de datos del metodo LlenarOrdenes
    Dim ResultadoOrden As DataView
    'VARIABLE PARA LAS COLUMNAS DINAMICAS DE MESES
    Public MesesColumnas As Integer


    Private Sub frmVentasNetasTotales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MANDA A LLMAR EL METODO DE LLENADO DE AGENTES
        MLlenaAgenteVta()
        'MANDA A LLAMAR AL METODO EJECUTA METODOS QUE PERMITE LA LLAMADA DE TODOS LOS METODOS
        MEjecutaMetodos()


    End Sub
#Region "Metodos"
    'METODO DE LLENADO DE COMBOBOX DE AGENTE
    Sub MLlenaAgenteVta()

        'VARIABLE QUE ALMACENA LA CONSULTA
        Dim SQLConsulta As String = ""
        'VARIABLE DE TIPO DATAVIEW
        Dim DVAgente As New DataView
        'VARIABLE DE TIPO COMMAND
        Dim cmdAgente As Data.SqlClient.SqlCommand
        'INSTANCIA DE VARIABLE DE TIPO COMMAND
        cmdAgente = New Data.SqlClient.SqlCommand()
        'VARIABLE PARA CREAR UNA FILA MAS DE TODOS
        Dim FilaAgente As Data.DataRow
        'ALMACENA LA CONSULTA
        SQLConsulta = "SELECT SlpCode, SlpName FROM OSLP where (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') "
        'REALIZA LA CONEXION POR MEDIO DEL COMMAND
        With cmdAgente
            .Connection = New Data.SqlClient.SqlConnection(StrCon)
            .Connection.Open()
            .CommandText = SQLConsulta
            .ExecuteNonQuery()
        End With
        'ALMANCENA EL COMMAND DE LA CONSULTA EN UN DATA ADAPTER
        Dim DAAgente As New SqlClient.SqlDataAdapter(cmdAgente)
        'INSTANCIA DE UN DATASET
        Dim DSAgente As New DataSet
        'LLENA EL DATA SET
        DAAgente.Fill(DSAgente)
        'OBTIENE LA PRIMER TABLA DEL DATA SET
        FilaAgente = DSAgente.Tables(0).NewRow
        'SE AGREGA UN VALOR
        FilaAgente("SlpCode") = "999"
        'SE AGREGA UN NOMBRE PARA EL SLPNAME
        FilaAgente("SlpName") = "TODOS"
        'SE LLENA NUEVAMENTE EL DATA SET
        DSAgente.Tables(0).Rows.Add(FilaAgente)
        'SE LLENA EL DATA VIEW
        DVAgente.Table = DSAgente.Tables(0)

        Me.cbxAgentes.DataSource = DVAgente
        'QUE DATO MOSTRAR
        Me.cbxAgentes.DisplayMember = "SlpName"
        'QUE DATO LLEVARA COMO VALOR
        Me.cbxAgentes.ValueMember = "SlpCode"
        'SELECCIONA EL VALOR A MOSTRAR DE FORMA PRIMERA
        Me.cbxAgentes.SelectedValue = "999"

        'CIERRA LA CONEXION
        cmdAgente.Connection.Close()

    End Sub

    'METODO QUE MADNA A LLAMAR A TODOS LOS METODOS CON BASE A LA VALIDACION DE LA FECHA
    Sub MEjecutaMetodos()
        'OBTIENE LAS FECHAS PARA VALIDAR SI ES MAYOR UNA QUE LA OTRA
        Inicio = dtpFechaIni.Value
        Final = dtpFechaFin.Value
        'VALIDA QUE LA FECHA INICIAL NO SEA MAYOR QUE LA FINAL
        If (Inicio.Date <= Final.Date) Then
            'MANDA A LLAMAR EL METODO DE OBTENCION DE VENTAS
            MObtieneVentas()
            DisenoGrid()


        Else
            MsgBox("La fecha inicial no puede ser mayor que la final.", MsgBoxStyle.Exclamation, "Alerta de captura de Fecha")
        End If
    End Sub

    'METODO QUE EJECUTA EL PRTOCEDIMIENTE QUE SE NECESITA Y MANDA LOS PARAMETROS CORRESPONDIENTES
    Sub MObtieneVentas()
        'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
        Dim cmd As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        DsVentas = New DataSet

        'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
        Try
            cnn = New SqlConnection(StrTpm) 'ORIGINAL
            'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBA
            cmd = New SqlCommand("SP_Reporte_Vta_Mes", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@fechaIni", String.Format("{0:yyyy-MM-dd}", dtpFechaIni.Value))
            cmd.Parameters.AddWithValue("@fechaFin", String.Format("{0:yyyy-MM-dd}", dtpFechaFin.Value))
            cmd.Parameters.AddWithValue("@Agente", cbxAgentes.SelectedValue)
            cnn.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 10000
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
            cnn.Close()

            'LLENA EL ADAPTER A UN DATA SET
            da.Fill(DsVentas)
            'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
            ResultadoOrden = New DataView
            'ALMACENA EN DATA SET DE MODO TABLA
            ResultadoOrden.Table = DsVentas.Tables(0)
            'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
            dgvDatosVentas.DataSource = Nothing
            'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
            dgvDatosVentas.DataSource = ResultadoOrden
        Catch ex As Exception
            MessageBox.Show("Error al Mostrar las ventas. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End Try

    End Sub

    'METODO QUE EJECUTA EL DISEÑO DEL GRID PARA VISUALIZAR LOS DATOS
    Private Sub DisenoGrid()
        'OBTENER NUMERO DE MESES ENTRE FECHA INICIAL Y FINAL, COLUMNAS EN LAS QUE SE TRABAJARA
        MesesColumnas = DateDiff("m", dtpFechaIni.Value, dtpFechaFin.Value)

        With Me.dgvDatosVentas
            .ReadOnly = True
            'Color de Renglones en Grid
            Dim clr1 As Color
            clr1 = ColorTranslator.FromHtml("#deeaf6")
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.White
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = clr1
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try
                .Columns("SLPCODE").Name = "SLPCODE"
                .Columns("SLPCODE").HeaderText = "Cod. Agente"
                .Columns("SLPCODE").Width = 50
                .Columns("SLPCODE").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("SLPCODE").Frozen = True

                .Columns("SLPNAME").Name = "SLPNAME"
                .Columns("SLPNAME").HeaderText = "Nombre"
                .Columns("SLPNAME").Width = 220
                .Columns("SLPNAME").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns("SLPNAME").Frozen = True

                .Columns("VENTASTOTALES").Name = "VENTASTOTALES"
                .Columns("VENTASTOTALES").HeaderText = "Ventas Totales"
                .Columns("VENTASTOTALES").Width = 120
                .Columns("VENTASTOTALES").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("VENTASTOTALES").DefaultCellStyle.Format = " $ ###,###,###.#0"
                .Columns("VENTASTOTALES").Frozen = True

                .Columns("MONTODEVUELTO").Name = "MONTODEVUELTO"
                .Columns("MONTODEVUELTO").HeaderText = "Monto devuelto"
                .Columns("MONTODEVUELTO").Width = 120
                .Columns("MONTODEVUELTO").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("MONTODEVUELTO").DefaultCellStyle.Format = " $ ###,###,###.#0"
                .Columns("MONTODEVUELTO").Frozen = True

                .Columns("VENTASNETAS").Name = "VENTASNETAS"
                .Columns("VENTASNETAS").HeaderText = "Ventas netas"
                .Columns("VENTASNETAS").Width = 120
                .Columns("VENTASNETAS").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("VENTASNETAS").DefaultCellStyle.Format = " $ ###,###,###.#0"
                .Columns("VENTASNETAS").Frozen = True
                'ALMACENA LA FECHA INICAL
                Dim fecha As String = dtpFechaIni.Value
                'VARIABLE PARA ALMACENAR EL MES DE LA FECHA
                Dim mes As String
                'OBTIENE EL MES PARTICIONANDO LA FECHA EN POSICIONES
                mes = fecha.Substring(3, 2)
                'VARIABLE PARA ALMACENAR EL AÑO DE LA FECHA
                Dim anio As String
                'OBTIENE EL AÑO PARTICIONANDO LA FECHA EN POSICIONES
                anio = fecha.Substring(6, 4)
                'VARIABLE PARA AUMENTAR EL NUMERO DE MESES
                Dim NumMes As Integer
                'VALIDA QUE MES TOMAR PRIMERO
                If mes = 1 Or mes = "01" Then
                    mes = "Enero"
                    NumMes = 1
                ElseIf mes = 2 Or mes = "02" Then
                    mes = "Febrero"
                    NumMes = 2
                ElseIf mes = 3 Or mes = "03" Then
                    mes = "Marzo"
                    NumMes = 3
                ElseIf mes = 4 Or mes = "04" Then
                    mes = "Abril"
                    NumMes = 4
                ElseIf mes = 5 Or mes = "05" Then
                    mes = "Mayo"
                    NumMes = 5
                ElseIf mes = 6 Or mes = "06" Then
                    mes = "Junio"
                    NumMes = 6
                ElseIf mes = 7 Or mes = "07" Then
                    mes = "Julio"
                    NumMes = 7
                ElseIf mes = 8 Or mes = "08" Then
                    mes = "Agosto"
                    NumMes = 8
                ElseIf mes = 9 Or mes = "09" Then
                    mes = "Septiembre"
                    NumMes = 9
                ElseIf mes = 10 Then
                    mes = "Octubre"
                    NumMes = 10
                ElseIf mes = 11 Then
                    mes = "Noviembre"
                    NumMes = 11
                ElseIf mes = 12 Then
                    mes = "Diciembre"
                    NumMes = 12

                End If

                .Columns(5).Name = mes & " " & anio
                .Columns(5).HeaderText = mes & " " & anio
                .Columns(5).Width = 120
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(5).DefaultCellStyle.Format = "$ ###,###,###.#0"

                'RECORRE TODOS LOS MESES DE COLUMNAS CAPTURADO AL INICIO
                For i As Integer = 1 To MesesColumnas
                    Dim aux As Integer
                    aux = NumMes + i

                    If aux > 12 Then
                        Dim aux2 As String
                        aux2 = aux - 12

                        Select Case aux2
                            Case 1
                                aux2 = "Enero"
                            Case 2
                                aux2 = "Febrero"
                            Case 3
                                aux2 = "Marzo"
                            Case 4
                                aux2 = "Abril"
                            Case 5
                                aux2 = "Mayo"
                            Case 6
                                aux2 = "Junio"
                            Case 7
                                aux2 = "Julio"
                            Case 8
                                aux2 = "Agosto"
                            Case 9
                                aux2 = "Septiembre"
                            Case 10
                                aux2 = "Octubre"
                            Case 11
                                aux2 = "Noviembre"
                            Case 12
                                aux2 = "Diciembre"
                        End Select

                        .Columns(5 + i).Name = aux2 & " " & anio + 1
                        .Columns(5 + i).HeaderText = aux2 & " " & anio + 1
                    Else

                        Dim auxm As String
                        auxm = NumMes + i

                        Select Case auxm
                            Case 1
                                auxm = "Enero"
                            Case 2
                                auxm = "Febrero"
                            Case 3
                                auxm = "Marzo"
                            Case 4
                                auxm = "Abril"
                            Case 5
                                auxm = "Mayo"
                            Case 6
                                auxm = "Junio"
                            Case 7
                                auxm = "Julio"
                            Case 8
                                auxm = "Agosto"
                            Case 9
                                auxm = "Septiembre"
                            Case 10
                                auxm = "Octubre"
                            Case 11
                                auxm = "Noviembre"
                            Case 12
                                auxm = "Diciembre"
                        End Select
                        .Columns(5 + i).Name = auxm & " " & anio
                        .Columns(5 + i).HeaderText = auxm & " " & anio
                    End If

                    .Columns(5 + i).Width = 120
                    .Columns(5 + i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(5 + i).DefaultCellStyle.Format = "$ ###,###,###.#0"
                Next

            Catch ex As Exception

            End Try

        End With
    End Sub

    'METODO DE FORMATO DE EXCEL
    Private Sub fFormatoExcel(exLibro As Microsoft.Office.Interop.Excel.Workbook, NRow As Integer, NCol As Integer)
        Try
            ''Combinamos celdas 
            exLibro.Worksheets("Hoja1").Cells.Range("A1:C1").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A2:C2").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A3:C3").Merge(True)

            ''aplicamos un color de fondo ala celda o rango de celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15

            ''Cambiamos orientacion ala hoja
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Reporte de Ventas-Agentes por Mes"
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Fecha del: " + dtpFechaIni.Value + "  Al  " + dtpFechaFin.Value
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Agente: " + cbxAgentes.Text

            'Encabezados en NEGRITA
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1

            'Dim Number As Integer
            Dim NumCol As Integer = dgvDatosVentas.RowCount - 1

            'TAMAÑO DE COLUMNAS
            exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 9
            exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 25
            exLibro.Worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 15
            exLibro.Worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 15
            exLibro.Worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 15
            exLibro.Worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 15

            'COLOCA FORMATO A LAS COLUMNAS  VENTAS TOTALES, MONTO DEVUELTO, VENTAS NETAS
            exLibro.Worksheets("Hoja1").Columns(3).NumberFormat = "$ ###,###,###.#0"
            exLibro.Worksheets("Hoja1").Columns(4).NumberFormat = "$ ###,###,###.#0"
            exLibro.Worksheets("Hoja1").Columns(5).NumberFormat = "$ ###,###,###.#0"

            MesesColumnas = DateDiff("m", dtpFechaIni.Value, dtpFechaFin.Value) + 1

            Dim aux As Integer = 5 + MesesColumnas + 1

            exLibro.Worksheets("Hoja1").Columns(aux).HIDDEN = True

            'Diseño de columnas de los meses
            For i As Integer = 1 To MesesColumnas
                exLibro.Worksheets("Hoja1").Columns(5 + i).EntireColumn.ColumnWidth = 15
                exLibro.Worksheets("Hoja1").Columns(5 + i).numberformat = "$ ###,###,###.#0"
            Next

            'COLOR COLUMNA VENTAS NETAS EN AMARILLO
            For i As Integer = 6 To NRow + 6
                exLibro.Worksheets("Hoja1").Cells.Item(i, 5).INTERIOR.COLORINDEX = 6
            Next

            'COLOR DE ENCABEZADO GRIS
            For i As Integer = 1 To NCol
                exLibro.Worksheets("Hoja1").Cells.Item(5, i).INTERIOR.COLORINDEX = 15
            Next

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

#End Region


#Region "Botones"

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        'MANDA A LLAMAR EL METODO QUE EJECUTA TODOS LOS METODOS DE CALCULOS
        MEjecutaMetodos()

    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Try

            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

            'Añadimos el Libro al programa
            exLibro = exApp.Workbooks.Add

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = dgvDatosVentas.ColumnCount
            Dim NRow As Integer = dgvDatosVentas.RowCount

            fFormatoExcel(exLibro, NRow, NCol)

            'Llenado de encabezados
            For i As Integer = 1 To NCol
                exLibro.Worksheets("Hoja1").Cells.Item(5, i) = dgvDatosVentas.Columns(i - 1).HeaderText.ToString
            Next

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 6, Col + 1) = IIf(dgvDatosVentas.Rows(Fila).Cells(Col).Value Is DBNull.Value, 0, dgvDatosVentas.Rows(Fila).Cells(Col).Value.ToString)
                Next
                Estatus.Visible = True
                ProgressBar1.Value = (Fila * 100) / NRow
            Next
            Estatus.Visible = False

            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exLibro.Worksheets("Hoja1").Rows.Item(5).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Rows.Item(5).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Rows.Item(5).WrapText = True
            'exLibro.Worksheets("Hoja1").Columns.AutoFit()
            exLibro.Worksheets("Hoja1").name = "Reporte Ventas-Agente Mes"


            'Aplicación visible
            exLibro.Worksheets.Application.Visible = True

            exLibro = Nothing
            exApp = Nothing

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "EVENTOS"

    Private Sub dgvDatosVentas_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvDatosVentas.RowPostPaint
        'DECLARA VARIABLE DE TIPO COLOR
        Dim amarillo As Color
        'ASIGNA EN HEXADECIMAL EL COLOR AMARILLO
        amarillo = ColorTranslator.FromHtml("#FDFF6C")
        Try
            'COLOCA COLOR A LA CELDA DE VENTAS NETAS
            dgvDatosVentas.Rows(e.RowIndex).Cells("VENTASNETAS").Style.BackColor = amarillo
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

#End Region


End Class