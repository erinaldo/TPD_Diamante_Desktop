'LIBRERIAS REQUERIDAS
Imports System.Data.SqlClient
Public Class frmArticulosRemate
    'VARIABLE PARA RELLENO DEL COMBO DE USUARIOS Y ALMACENES
    Dim Lista_linea As New ArrayList
    Dim Lista_alm As New ArrayList
    'VARIABLES GLOBAL DEL FORMULARIO
    Dim fi As String
    Dim ff As String
    Private Sub frmArticulosRemate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MANDA A LLAMAR EL METODO DE LLENADO DEL COMBO BOX DE ALMACEN
        llenarComboAlmacen()
        'MANDA A LLAMAR AL METODO DE LLENADO DEL COMBO BOX DE ALMACEN
        llenarCombolineas()
        'MANDA A LLAMAR AL METODO DE ESTILO DEL DATAGRIDVIEW ARTICULOS
        estilo_grid_articulos()
    End Sub

    Sub llenarComboAlmacen()
        'OBTIENE TODOS LOS USUARIOS
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS DEL SAP
            conexion_universal.conectar_sap()
            'CONSULTA DE OBTENCIÓN DE LOS ALMACENES
            conexion_universal.slq_s = New SqlCommand("SELECT WhsCode, WhsName FROM OWHS WHERE (WhsCode = 01 or WhsCode = '03' or WhsCode = '07')", conexion_universal.conexion_uni_sap)
            'COLOCA EL DATO DE TODOS AL INICIO DE LOS ALMACENES
            Lista_alm.Add("TODOS")
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                Lista_alm.Add(conexion_universal.rd_s.Item("WhsName"))
            End While
            conexion_universal.rd_s.Close()
            With cmbalmacen
                .DropDownStyle = ComboBoxStyle.DropDown
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DataSource = Lista_alm
                .DisplayMember = "WhsName"
                '.ValueMember = "ItmsGrpNam"
            End With
            'COLOCA EN LA POSICION CERO EL COMBO PARA MOSTRAR VALOR POR DEFECTO
            'cmbalmacen.SelectedIndex = -1
            cmbalmacen.SelectedIndex = 0

            'CIERRA LA CONEXION
            conexion_universal.cerrar_conectar_sap()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión SAP: " & ex.Message, MsgBoxStyle.Critical)
            conexion_universal.cerrar_conectar_sap()
            Return
        End Try 'FIN CAPTURA EL ERROR
    End Sub

    Sub llenarCombolineas()
        'OBTIENE TODOS LAS LINEAS
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS DEL SAP
            conexion_universal.conectar_sap()
            'CONSULTA DE OBTENCIÓN DE LAS LINEAS
            conexion_universal.slq_s = New SqlCommand("select ItmsGrpCod as Linea, ItmsGrpNam as Nombre from OITB", conexion_universal.conexion_uni_sap)
            'COLOCA EL DATO DE TODOS AL INICIO DE LOS ALMACENES
            Lista_linea.Add("TODOS")
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                Lista_linea.Add(conexion_universal.rd_s.Item("Nombre"))
            End While
            conexion_universal.rd_s.Close()
            With cmblinea
                .DropDownStyle = ComboBoxStyle.DropDown
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DataSource = Lista_linea
                .DisplayMember = "Nombre"
                '.ValueMember = "ItmsGrpNam"
            End With
            'COLOCA EN LA POSICION CERO EL COMBO PARA MOSTRAR VALOR POR DEFECTO
            'cmbalmacen.SelectedIndex = -1
            cmblinea.SelectedIndex = 0

            'CIERRA LA CONEXION
            conexion_universal.cerrar_conectar_sap()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión SAP: " & ex.Message, MsgBoxStyle.Critical)
            conexion_universal.cerrar_conectar_sap()
            Return
        End Try 'FIN CAPTURA EL ERROR
    End Sub

    Sub estilo_grid_articulos()
        With Me.dgvarticulos
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            '.RowHeadersVisible = False
            'ARTICULO
            .AllowUserToAddRows = False
            .Columns("Articulo").Width = 100
            .Columns("Articulo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Articulo").ReadOnly = False
            'DESCRIPCION
            .Columns("Descripcion").Width = 150
            .Columns("Descripcion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            '.Columns("Factura").DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("Descripcion").ReadOnly = False
            'LINEA
            .Columns("Linea").Width = 100
            .Columns("Linea").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Linea").ReadOnly = False
            'STOCK PUEBLA
            .Columns("sp").Width = 50
            .Columns("sp").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("sp").ReadOnly = False
            'STOCK MERIDA
            .Columns("sm").Width = 50
            .Columns("sm").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("sm").ReadOnly = False
            'STOCK TUXTLA
            .Columns("st").Width = 50
            .Columns("st").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("st").ReadOnly = False
            'STOCK TOTAL
            .Columns("stt").Width = 50
            .Columns("stt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("stt").ReadOnly = False
            'LISTA 01
            .Columns("L01").Width = 80
            .Columns("L01").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("L01").ReadOnly = False
            'TOTAL L01
            .Columns("TL01").Width = 80
            .Columns("TL01").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TL01").ReadOnly = False
            'PZA. VENDIDA PUEBLA
            .Columns("PzaVtaPue").Width = 50
            .Columns("PzaVtaPue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PzaVtaPue").ReadOnly = False
            'IMP. VENTA PUEBLA
            .Columns("ImpVtaPue").Width = 80
            .Columns("ImpVtaPue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("ImpVtaPue").ReadOnly = False
            'PZA. DEVUELTA PUEBLA
            .Columns("PzaDevPue").Width = 50
            .Columns("PzaDevPue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PzaDevPue").ReadOnly = False
            'IMP. DEVUELTA PUEBLA
            .Columns("ImpDevPue").Width = 80
            .Columns("ImpDevPue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("ImpDevPue").ReadOnly = False
            'PZA. VENDIDA MERIDA
            .Columns("PzaVtaMer").Width = 50
            .Columns("PzaVtaMer").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PzaVtaMer").ReadOnly = False
            'IMP. VENTAS MERIDA
            .Columns("ImpVtaMer").Width = 80
            .Columns("ImpVtaMer").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("ImpVtaMer").ReadOnly = False
            'PZA. DEVUELTAS MERIDA
            .Columns("PzaDevMer").Width = 50
            .Columns("PzaDevMer").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PzaDevMer").ReadOnly = False
            'IMP. DEVUELTAS MERIDA
            .Columns("ImpDevMer").Width = 80
            .Columns("ImpDevMer").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("ImpDevMer").ReadOnly = False
            'PZAS. VENDIDAS TUXTLA
            .Columns("PzaVtaTux").Width = 50
            .Columns("PzaVtaTux").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PzaVtaTux").ReadOnly = False
            'IMP. VENTAS TUXTLA
            .Columns("ImpVtaTux").Width = 80
            .Columns("ImpVtaTux").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("ImpVtaTux").ReadOnly = False
            'PZAS. DEVUELTAS TUXTLA
            .Columns("PzaDevTux").Width = 50
            .Columns("PzaDevTux").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PzaDevTux").ReadOnly = False
            'IMP. DEVUELTAS TUXTLA
            .Columns("ImpDevTux").Width = 80
            .Columns("ImpDevTux").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("ImpDevTux").ReadOnly = False
            ''TOTAL DE PIEZAS VENDIDAS
            '.Columns("TotPzaVta").Width = 50
            '.Columns("TotPzaVta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("TotPzaVta").ReadOnly = False
            ''IMP. TOTAL DE VENTAS
            '.Columns("ImpTotVtas").Width = 80
            '.Columns("ImpTotVtas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns("ImpTotVtas").ReadOnly = False
            ''TOTAL DE PIEZAS DEVUELTAS
            '.Columns("TotPzasDev").Width = 50
            '.Columns("TotPzasDev").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("TotPzasDev").ReadOnly = False
            ''IMP. TOTAL DEVUELTO
            '.Columns("ImpTotDev").Width = 80
            '.Columns("ImpTotDev").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns("ImpTotDev").ReadOnly = False
            'PIEZAS TOTALES LAS VENDIDAS - DEVUELTAS
            .Columns("PzasTot").Width = 50
            .Columns("PzasTot").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("PzasTot").DefaultCellStyle.BackColor = Color.Coral
            .Columns("PzasTot").ReadOnly = False
            'TOTAL DE VENTAS - DEVUELTAS
            .Columns("Total").Width = 80
            .Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Total").DefaultCellStyle.BackColor = Color.Orange
            .Columns("Total").ReadOnly = False
        End With
    End Sub

    Private Sub btnconsultar_Click(sender As Object, e As EventArgs) Handles btnconsultar.Click
        'ALMACENAN LAS FECHAS CON EL FORMATO QUE SE REQUIERE
        fi = dtpfi.Value.ToString("yyyy-MM-dd")
        ff = dtpff.Value.ToString("yyyy-MM-dd")
        'ALMACENA EL ESTADO DEL CHECK DE SOLO VENTAS
        Dim estado As String = cbventa.Checked.ToString

        'VARIABLES DE TIPO FECHA
        Dim fecha_ini As Date
        Dim fecha_fin As Date

        'CONVIERTE EL DATA TIME PICKER EN FECHA
        fecha_ini = Convert.ToDateTime(fi).Date
        fecha_fin = Convert.ToDateTime(ff).Date
        'BORRA LOS DATOS DEL GRID
        If dgvarticulos.RowCount > 0 Then
            dgvarticulos.Rows.Clear()
        End If
        'VALIDA QUE LAS FECHAS SEAN CORRECTAS
        If (fecha_ini <= fecha_fin) Then
            Try
                'ABRE LA CONEXION A SAP
                conexion_universal.conectar()
                'MANDA A LLAMAR EL PROCEDIMIENTO ALMACENADO
                conexion_universal.slq_s = New SqlCommand("ArticulosRemate", conexion_uni)
                'COLOCA LOS PARAMETROS DE ENTRADA
                conexion_universal.slq_s.CommandType = CommandType.StoredProcedure
                conexion_universal.slq_s.Parameters.AddWithValue("@Fi", fi)
                conexion_universal.slq_s.Parameters.AddWithValue("@Ff", ff)
                conexion_universal.slq_s.Parameters.AddWithValue("@Almacen", cmbalmacen.SelectedValue.ToString())
                conexion_universal.slq_s.Parameters.AddWithValue("@Linea", cmblinea.SelectedValue.ToString())
                conexion_universal.slq_s.Parameters.AddWithValue("@Ventas", estado)
                'ALMACENA EL PROCEDIMIENTO
                conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader
                'RECORRE EL PROCEDIMIENTO
                While conexion_universal.rd_s.Read
                    'VALIDA SI EL DATA GRID TIENE FILAS O NO
                    If dgvarticulos.RowCount > 0 Then
                        Try
                            Me.dgvarticulos.Rows.Add(conexion_universal.rd_s.Item("Articulo").ToString, conexion_universal.rd_s.Item("Descripcion").ToString, conexion_universal.rd_s.Item("Linea").ToString,
                                                     CInt(conexion_universal.rd_s.Item("StockPuebla")), CInt(conexion_universal.rd_s.Item("StockMerida")), CInt(conexion_universal.rd_s.Item("StockTuxtla")),
                                                     CInt(conexion_universal.rd_s.Item("StockTotal")), conexion_universal.rd_s.Item("Lista01").ToString, conexion_universal.rd_s.Item("TotalL01").ToString, CInt(conexion_universal.rd_s.Item("PzaVendidasPuebla")),
                                                     conexion_universal.rd_s.Item("ImpVtaPuebla").ToString, CInt(conexion_universal.rd_s.Item("PzaDevPuebla")), conexion_universal.rd_s.Item("ImpDevPuebla").ToString,
                                                     CInt(conexion_universal.rd_s.Item("PzaVendidasMerida")), conexion_universal.rd_s.Item("ImpVtaMerida").ToString, CInt(conexion_universal.rd_s.Item("PzaDevMerida")), conexion_universal.rd_s.Item("ImpDevMerida").ToString,
                                                     CInt(conexion_universal.rd_s.Item("PzaVendidasTuxtla")), conexion_universal.rd_s.Item("ImpVtaTuxtla").ToString, CInt(conexion_universal.rd_s.Item("PzaDevTuxtla")), conexion_universal.rd_s.Item("ImpDevTuxtla").ToString,
                                                     CInt(conexion_universal.rd_s.Item("TotalPzasVdas")), conexion_universal.rd_s.Item("ImporteVtas"))
                            'RECORRE EL DATA GRID VIEW
                            With dgvarticulos
                                'ESTABLECE LA CELDA ACTUAL
                                .CurrentCell = .Rows(Me.dgvarticulos.Rows.Count - 1).Cells(0)
                            End With
                        Catch ex As Exception
                            'MANDA EL MENSAJE DE ERROR
                            MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Exclamation)
                            Return
                        End Try
                    Else
                        Try
                            Me.dgvarticulos.Rows.Add(conexion_universal.rd_s.Item("Articulo").ToString, conexion_universal.rd_s.Item("Descripcion").ToString, conexion_universal.rd_s.Item("Linea").ToString,
                                                     CInt(conexion_universal.rd_s.Item("StockPuebla")), CInt(conexion_universal.rd_s.Item("StockMerida")), CInt(conexion_universal.rd_s.Item("StockTuxtla")),
                                                     CInt(conexion_universal.rd_s.Item("StockTotal")), CDbl(conexion_universal.rd_s.Item("Lista01")), CDbl(conexion_universal.rd_s.Item("TotalL01")), CInt(conexion_universal.rd_s.Item("PzaVendidasPuebla")),
                                                     CDbl(conexion_universal.rd_s.Item("ImpVtaPuebla")), CInt(conexion_universal.rd_s.Item("PzaDevPuebla")), CDbl(conexion_universal.rd_s.Item("ImpDevPuebla")),
                                                     CInt(conexion_universal.rd_s.Item("PzaVendidasMerida")), CDbl(conexion_universal.rd_s.Item("ImpVtaMerida")), CInt(conexion_universal.rd_s.Item("PzaDevMerida")), CDbl(conexion_universal.rd_s.Item("ImpDevMerida")),
                                                     CInt(conexion_universal.rd_s.Item("PzaVendidasTuxtla")), CDbl(conexion_universal.rd_s.Item("ImpVtaTuxtla")), CInt(conexion_universal.rd_s.Item("PzaDevTuxtla")), CDbl(conexion_universal.rd_s.Item("ImpDevTuxtla")),
                                                     CInt(conexion_universal.rd_s.Item("TotalPzasVdas")), CDbl(conexion_universal.rd_s.Item("ImporteVtas")))
                            'RECORRE EL DATA GRID VIEW
                            With dgvarticulos
                                'ESTABLECE LA CELDA ACTUAL
                                .CurrentCell = .Rows(Me.dgvarticulos.Rows.Count - 1).Cells(0)
                            End With
                        Catch ex As Exception
                            'MANDA EL MENSAJE DE ERROR
                            MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Exclamation)
                            Return
                        End Try
                    End If
                End While
                'RECORRE EL DATAGRIDVIEW PARA COMPARAR FILAS O CELDAS Y COLOCAR FORMATO.
                For i As Integer = 0 To dgvarticulos.Rows.Count - 1
                    'COMPARA EL ESTATUS
                    If dgvarticulos.Rows(i).Cells("Articulo").Value.ToString = "MONTOS TOTALES" Then
                        'COLOCA COLOR A LA CELDA U LA LETRA
                        dgvarticulos.Rows(i).DefaultCellStyle.BackColor = Color.White
                        dgvarticulos.Rows(i).DefaultCellStyle.ForeColor = Color.Black
                        dgvarticulos.Rows(i).DefaultCellStyle.Font = New Font(dgvarticulos.DefaultCellStyle.Font, FontStyle.Bold)
                    End If
                Next
                conexion_universal.rd_s.Close()
            Catch ex As Exception
                MsgBox("Error de conexión o consulta: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de BD")
                conexion_universal.rd_s.Close()
                conexion_universal.cerrar_conectar()
                Return
            End Try
        Else
            'MANADA MENSAJE EN PANTALLA DE ERROR, Y NO EJECUTA NINGUN CODIGO
            MessageBox.Show("La fecha de Inicio NO puede ser mayor a la Final.", "Error en Perido de Fechas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'MANDA EL FOCUS AL ELEMENTO QUE SE REQUIERA
            dtpfi.Focus()
        End If
    End Sub

    Private Sub btnexportar_Click(sender As Object, e As EventArgs) Handles btnexportar.Click
        'VALIDA SI HAY DATOS QUE EXPAORTAR
        If dgvarticulos.RowCount > 0 Then
            'CONTADOR DE LAS COLUMNAS DONDE TENDRA QUE EMPEZAR EL EMPATE CON EL EXCEL
            Dim con, confila As Integer
            con = 4
            confila = 0
            'DECLARACIÓN DE VARIABLES PARA LIBRO DE EXCEL CREANDO UN OBJETO DE EXCEL
            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
            Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
            Try
                'AÑADIMOS EL LIBRO AL PROGRAMA , Y LA HOJA AL LIBRO
                exLibro = exApp.Workbooks.Add
                exHoja = exLibro.Worksheets.Add()
                ' ¿CUANTAS COLUMNAS Y CUANTAS FILAS?
                Dim NCol As Integer = dgvarticulos.ColumnCount
                Dim NRow As Integer = dgvarticulos.RowCount
                'AQUI Aqui recorremos todas las filas, y por cada fila todas las columnas
                'y vamos escribiendo.
                exHoja.Cells.Item(1, 1) = "Ventas Articulos de remate " + fi + " al " + ff
                For i As Integer = 1 To NCol
                    'COLOCA EL NOMBRE DE LAS COLUMNAS DEL DATAGRID
                    'exHoja.Cells.Item(1, i) = dgvres.Columns(i - 1).Name.ToString
                    exHoja.Cells.Item(3, 1) = "Articulo"
                    exHoja.Cells.Item(3, 2) = "Descripción"
                    exHoja.Cells.Item(3, 3) = "Linea"
                    exHoja.Cells.Item(3, 4) = "Stock Puebla"
                    exHoja.Cells.Item(3, 5) = "Stock Mérida"
                    exHoja.Cells.Item(3, 6) = "Stock Tuxtla"
                    exHoja.Cells.Item(3, 7) = "Stock Total"
                    exHoja.Cells.Item(3, 8) = "Lista 01"
                    exHoja.Cells.Item(3, 9) = "Total L01"
                    exHoja.Cells.Item(3, 10) = "Pza. Vta. Puebla"
                    exHoja.Cells.Item(3, 11) = "Imp. Vta. Puebla"
                    exHoja.Cells.Item(3, 12) = "Pza. Dev. Puebla"
                    exHoja.Cells.Item(3, 13) = "Imp. Dev. Puebla"
                    exHoja.Cells.Item(3, 14) = "Pza. Vta. Mérida"
                    exHoja.Cells.Item(3, 15) = "Imp. Vta. Mérida"
                    exHoja.Cells.Item(3, 16) = "Pza. Dev. Mérida"
                    exHoja.Cells.Item(3, 17) = "Imp. Dev. Mérida"
                    exHoja.Cells.Item(3, 18) = "Pza. Vta. Tuxtla"
                    exHoja.Cells.Item(3, 19) = "Imp. Vta. Tuxtla"
                    exHoja.Cells.Item(3, 20) = "Pza. Dev. Tuxtla"
                    exHoja.Cells.Item(3, 21) = "Imp. Dev. Tuxtla"
                    exHoja.Cells.Item(3, 22) = "Total Pzas. Vendidas"
                    exHoja.Cells.Item(3, 23) = "Total Imp. Ventas"
                Next
                For Fila As Integer = 0 To NRow - 1
                    For Col As Integer = 0 To NCol - 1
                        exHoja.Cells.Item(Fila + 4, Col + 1) = _
                        dgvarticulos.Rows(Fila).Cells(Col).Value
                    Next
                Next
                'TITULO EN NEGRITA, ALINEADO AL CENTRO Y QUE EL TAMAÑO DE LA COLUMNA
                'COLOCA NEGRITAS LA FILA 1
                exHoja.Rows.Item(1).Font.Bold = 1
                'COLOCA NEGRITAS LA FILA 3
                exHoja.Rows.Item(3).Font.Bold = 1
                'ALINEACION DE LA FILA 3
                exHoja.Rows.Item(3).HorizontalAlignment = 3
                'SE AJUSTE AL TEXTO
                exHoja.Columns.AutoFit()
                'COLOCA EL NOMBRE DE LA HOJA
                exHoja.Name = "Articulos Remate".ToString
                'MUESTRA EN PANTALLA EL EXCEL
                exApp.Application.Visible = True
                exHoja = Nothing
                exLibro = Nothing
                exApp = Nothing
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")
            End Try
        Else
            MsgBox("No hay datos que exportar.", MsgBoxStyle.Exclamation, "Datos no encontrados")
            dtpfi.Focus()
            Return
        End If
    End Sub
End Class