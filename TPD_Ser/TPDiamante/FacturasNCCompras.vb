Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class FacturasNCCompras
    Public StrTpm As String = conexion_universal.CadenaSQL

    Dim DvConta As New DataView

    Private Sub FacturasXmlConta_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Devuelve el primer dia del mes de la fecha actual
        Dim FechaIni As New Date
        FechaIni = Today
        FechaIni = FechaIni.AddDays(-FechaIni.Day + 1)

        'Devuelve el ultimo dia del mes de la fecha actual
        Dim FechaFin As New Date
        FechaFin = Today
        FechaFin = FechaFin.AddDays(-FechaFin.Day + 1).AddMonths(1).AddDays(-1)

        DTIni.Value = FechaIni
        DTFin.Value = FechaFin

        CBDocumento.SelectedItem = "Facturas"

        'Me.DTIni.Value = Format("dd/MM/yyyy")
        'Me.DTFin.Value = Format("dd/MM/yyyy")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            'MsgBox(DTIni.Value)
            'MsgBox(DTFin.Value)

            Dim cnn As SqlConnection = Nothing

            Dim cmd4 As SqlCommand = Nothing

            If CBDocumento.Text = "Facturas" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("FNCCompras", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@Tipodoc", 1)
                    cmd4.Parameters.AddWithValue("@FechaInicial", DTIni.Value)
                    cmd4.Parameters.AddWithValue("@FechaFinal", DTFin.Value)

                    cnn.Open()

                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()

                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmd4
                    da.SelectCommand.Connection = cnn


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Inventario"

                    DvConta.Table = DsVtas.Tables("Inventario")

                    DataGridView1.DataSource = DvConta


                    DisenoGrid()

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            ElseIf CBDocumento.Text = "Notas de Crédito" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("FNCCompras", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@Tipodoc", 2)
                    cmd4.Parameters.AddWithValue("@FechaInicial", DTIni.Value)
                    cmd4.Parameters.AddWithValue("@FechaFinal", DTFin.Value)

                    cnn.Open()

                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()

                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmd4
                    da.SelectCommand.Connection = cnn


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Inventario"

                    DvConta.Table = DsVtas.Tables("Inventario")

                    DataGridView1.DataSource = DvConta

                    DisenoGrid()

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub bExcel_Click(sender As Object, e As EventArgs) Handles bExcel.Click
        Try
            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
            Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

            'Añadimos el Libro al programa, y la hoja al libro
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets.Add()

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DataGridView1.ColumnCount
            Dim NRow As Integer = DataGridView1.RowCount


            fFormatoExcel(exLibro, NRow)


            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                exHoja.Cells.Item(5, i) = DataGridView1.Columns(i - 1).Name.ToString
                'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
            Next

            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    'exHoja.Cells.Item(Fila + 6, Col + 1).NumberFormat = "@"
                    exHoja.Cells.Item(Fila + 6, Col + 1) = DataGridView1.Rows(Fila).Cells(Col).Value
                Next

                Estatus.Visible = True
                ProgressBar1.Value = (Fila * 100) / NRow
            Next

            Estatus.Visible = False

            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exHoja.Rows.Item(4).Font.Bold = 1
            exHoja.Rows.Item(4).HorizontalAlignment = 3
            exHoja.Columns.AutoFit()
            'Aplicación visible
            exApp.Application.Visible = True


            ''Cambiamos orientacion ala hola
            exHoja.Cells.Item(1, 1) = "Reporte de " & CBDocumento.Text
            exHoja.Cells.Item(2, 1) = "Fecha del: " + DTIni.Value + "  Al  " + DTFin.Value

            exHoja = Nothing
            exLibro = Nothing
            exApp = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")

        End Try
    End Sub

    Private Sub fFormatoExcel(exLibro As Microsoft.Office.Interop.Excel.Workbook, NRow As Integer)
        Try
            ' ''Combinamos celdas
            'exLibro.Worksheets("Hoja1").Cells.Range("A1:T1").Merge(True)
            'exLibro.Worksheets("Hoja1").Cells.Range("A2:T2").Merge(True)
            'exLibro.Worksheets("Hoja1").Cells.Range("A3:T3").Merge(True)

            ' ''aplicamos un color de fondo ala celda o rango de celdas
            'exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
            'exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
            'exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15


            'exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
            'exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
            'exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1
            'exLibro.Worksheets("Hoja1").Cells.Item(5, 1).Font.Bold = 1

            'exLibro.Worksheets("Hoja1").Columns(10).NumberFormat = "###.0000"
            'exLibro.Worksheets("Hoja1").Columns("L:T").NumberFormat = "###.0000"
            'exLibro.Worksheets("Hoja1").Columns("D:f").NumberFormat = "###,###,###"
            'exLibro.Worksheets("Hoja1").Columns("G").NumberFormat = "$ ###,###.00"
            'exLibro.Worksheets("Hoja1").Columns("I").NumberFormat = "$ ###,###.00"
            'exLibro.Worksheets("Hoja1").Columns("K").NumberFormat = "$ ###,###.00"
            'exLibro.Worksheets("Hoja1").Columns("A").NumberFormat = "@"

            'exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 15
            'exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 45
            'exLibro.Worksheets("Hoja1").Columns("D:f").EntireColumn.ColumnWidth = 5
            'exLibro.Worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 9.5
            'exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 7.86
            'exLibro.Worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 6
            'exLibro.Worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 8.5



            exLibro.Worksheets("Hoja2").Cells.Range("A5:M" + 5.ToString).Interior.ColorIndex = 20

        Catch ex As Exception

        End Try


    End Sub


    Private Sub DisenoGrid()
        Try
            With DataGridView1
                '.DataSource = DtAgte
                '.ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25

                .Columns(0).HeaderText = "Serie"
                .Columns(0).Width = 50
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .Columns(1).HeaderText = "Número de Documento"
                .Columns(1).Width = 70
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .Columns(2).HeaderText = "Fecha de Contabilización"
                .Columns(2).Width = 90
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Descripcion	
                .Columns(3).HeaderText = "Tipo de Documento"
                .Columns(3).Width = 70
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Linea	
                .Columns(4).HeaderText = "Clave Proveedor"
                .Columns(4).Width = 80
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Precio L9
                .Columns(5).HeaderText = "Nombre"
                .Columns(5).Width = 180
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                '.Columns(6).HeaderText = "Norma de reparto"
                '.Columns(6).Width = 75
                '.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                'Vta Neta
                .Columns(6).HeaderText = "Total Documento"
                .Columns(6).Width = 80
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Format = "$ ###,###,##0.##"

                .Columns(7).HeaderText = "Folio Pref"
                .Columns(7).Width = 60
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(8).HeaderText = "Folio Num"
                .Columns(8).Width = 70
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(9).HeaderText = "Ruta"
                .Columns(9).Width = 140
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(10).HeaderText = "Nombre de archivo"
                .Columns(10).Width = 140
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(11).HeaderText = "Extensión"
                .Columns(11).Width = 60
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(12).HeaderText = "Factura"
                .Columns(12).Width = 100
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            End With
        Catch ex As Exception

        End Try
    End Sub

End Class