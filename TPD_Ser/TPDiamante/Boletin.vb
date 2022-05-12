

Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class Boletin

  Public StrProd As String = conexion_universal.CadenaSBO_Diamante
  Public StrTpm As String = conexion_universal.CadenaSQL
    Public StrCon As String = conexion_universal.CadenaSQLSAP

    Dim DvArticulos As New DataView
    Dim DvAlmacen As New DataView

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TBDia.Text = "" Or TBDia.Text = "0" Then
            MsgBox("Ingrese numero de días")

        Else

            Dim cnn As SqlConnection = Nothing

            Dim cmd4 As SqlCommand = Nothing

            Dim dia As Integer

            dia = TBDia.Text

            Try
                cnn = New SqlConnection(StrTpm)
                cnn.Open()

                Dim DiasTrans As Decimal

                DiasTrans = DateDiff("D", dtIni.Value, dtFin.Value)

                'MsgBox(DiasTrans)

                If CBAlmacen.SelectedValue = "01" Then
                    cmd4 = New SqlCommand("SPBoletin2", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@Tipo", 1)
                    cmd4.Parameters.AddWithValue("@FechaIni", dtIni.Value)
                    cmd4.Parameters.AddWithValue("@FechaFin", dtFin.Value)
                    cmd4.Parameters.AddWithValue("@DiasTrans", DiasTrans)
                    cmd4.Parameters.AddWithValue("@Dia", dia)

                ElseIf CBAlmacen.SelectedValue = "03" Then
                    cmd4 = New SqlCommand("SPBoletin2", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@Tipo", 2)
                    cmd4.Parameters.AddWithValue("@FechaIni", dtIni.Value)
                    cmd4.Parameters.AddWithValue("@FechaFin", dtFin.Value)
                    cmd4.Parameters.AddWithValue("@DiasTrans", DiasTrans)
                    cmd4.Parameters.AddWithValue("@Dia", dia)

                ElseIf CBAlmacen.SelectedValue = "07" Then
                    cmd4 = New SqlCommand("SPBoletin2", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@Tipo", 3)
                    cmd4.Parameters.AddWithValue("@FechaIni", dtIni.Value)
                    cmd4.Parameters.AddWithValue("@FechaFin", dtFin.Value)
                    cmd4.Parameters.AddWithValue("@DiasTrans", DiasTrans)
                    cmd4.Parameters.AddWithValue("@Dia", dia)

                ElseIf CBAlmacen.SelectedValue = "999" Then
                    cmd4 = New SqlCommand("SPBoletin2", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@Tipo", 9)
                    cmd4.Parameters.AddWithValue("@FechaIni", dtIni.Value)
                    cmd4.Parameters.AddWithValue("@FechaFin", dtFin.Value)
                    cmd4.Parameters.AddWithValue("@Dia", dia)
                    cmd4.Parameters.AddWithValue("@DiasTrans", DiasTrans)
                End If

                cmd4.ExecuteNonQuery()
                cmd4.Connection.Close()
                Dim da As New SqlDataAdapter
                da.SelectCommand = cmd4
                da.SelectCommand.Connection = cnn

                ''--------------------------------------------
                Dim DsUpdate As New DataSet
                da.Fill(DsUpdate, "DsUpdate")

                DsUpdate.Tables(0).TableName = "Articulos"

                DvArticulos.Table = DsUpdate.Tables("Articulos")

                DGBoletin.DataSource = DvArticulos

            Catch ex As Exception
                MsgBox(ex.Message)
                'MsgBox("No existen ventas de este día")
            Finally
                If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                    cnn.Close()
                End If
            End Try

            DisenoGrid()
        End If
    End Sub

    Private Sub Boletin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.WindowState = FormWindowState.Maximized

        dtFin.Value = Today()

        Dim ConsutaLista As String

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

            Dim DSetTablas As New DataSet

            ' -----------------------------------------------------
            Try

                Dim DSetTablas2 As New DataSet
                ConsutaLista = "SELECT whscode,whsname FROM owhs WHERE whscode in ('01','03','07')"

                Dim daarticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


                daarticulo.Fill(DSetTablas2, "Almacenes")

                Dim filaArticulo As Data.DataRow

                'Asignamos a fila la nueva Row(Fila)del Dataset
                filaArticulo = DSetTablas2.Tables("Almacenes").NewRow

                'Agregamos los valores a los campos de la tabla
                filaArticulo("whsname") = "TODOS"
                filaArticulo("whscode") = 999

                'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                DSetTablas2.Tables("Almacenes").Rows.Add(filaArticulo)

                DvAlmacen.Table = DSetTablas2.Tables("Almacenes")

                Me.CBAlmacen.DataSource = DvAlmacen
                Me.CBAlmacen.DisplayMember = "whsname"
                Me.CBAlmacen.ValueMember = "whscode"
                Me.CBAlmacen.SelectedValue = "999"

                ' -----------------------------------------------------

                'DisenoGrid()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Using

    End Sub

    Private Sub DisenoGrid()
        Try
            With DGBoletin
                '.DataSource = DtAgte
                '.ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DGBoletin.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25


                'Articulo	
                .Columns(0).HeaderText = "Código"
                .Columns(0).Width = 120
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Descripcion	
                .Columns(1).HeaderText = "Descripción"
                .Columns(1).Width = 225
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Linea	
                .Columns(2).HeaderText = "Línea"
                .Columns(2).Width = 170
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Existencia
                .Columns(3).HeaderText = "Existencia"
                .Columns(3).Width = 70
                .Columns(3).DefaultCellStyle.Format = "###,##0"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Vta Neta
                .Columns(4).HeaderText = "Venta al día"
                .Columns(4).Width = 70
                .Columns(4).DefaultCellStyle.Format = "###,##0"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Proyectado
                .Columns(5).HeaderText = "Proyectado"
                .Columns(5).Width = 70
                .Columns(5).DefaultCellStyle.Format = "###,##0"
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Compra
                .Columns(6).HeaderText = "Compra"
                .Columns(6).Width = 70
                .Columns(6).DefaultCellStyle.Format = "###,##0"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BExcel_Click(sender As Object, e As EventArgs) Handles BExcel.Click
        Try
            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
            Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

            'Añadimos el Libro al programa, y la hoja al libro
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets.Add()

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DGBoletin.ColumnCount
            Dim NRow As Integer = DGBoletin.RowCount

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                exHoja.Cells.Item(5, i) = DGBoletin.Columns(i - 1).Name.ToString
                'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
            Next

            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    'exHoja.Cells.Item(Fila + 6, Col + 1).NumberFormat = "@"
                    exHoja.Cells.Item(Fila + 6, Col + 1) = DGBoletin.Rows(Fila).Cells(Col).Value
                Next
            Next

            exLibro.Worksheets("Hoja2").Columns("A").NumberFormat = "@"

            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exHoja.Rows.Item(5).Font.Bold = 1
            exHoja.Rows.Item(5).HorizontalAlignment = 3
            exHoja.Columns.AutoFit()
            'Aplicación visible
            exApp.Application.Visible = True


            ''Cambiamos orientacion ala hola
            exHoja.Cells.Item(1, 1) = "Reporte de Artículos en Boletín"
            exHoja.Cells.Item(2, 1) = "Periodo de Ventas del: " + dtIni.Value + " al " + dtFin.Value
            exHoja.Cells.Item(3, 1) = "Día: " + TBDia.Text
            exHoja.Cells.Item(3, 2) = "Almacen: " + CBAlmacen.Text

            exHoja = Nothing
            exLibro = Nothing
            exApp = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")

        End Try
    End Sub
End Class