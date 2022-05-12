Imports System.Data.SqlClient

Public Class ValeSalidaConsulta

    Public conexion2 As New SqlConnection(conexion_universal.CadenaSQL)
    Public DvDetalle As New DataView

    Dim DvArticulo As New DataView
    Dim DvClte As New DataView
    Dim DvAgte As New DataView
    Dim DvBO As New DataView

    Private Sub ValeSalidaConsulta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ''--------------------

            Dim ConsutaLista As String


            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                'mllenaComboAlmacen(SqlConnection)

                'LLENA COMBO LINEAS

                ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
                Dim daGArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

                Dim DSetTablas As New DataSet
                daGArticulo.Fill(DSetTablas, "GArticulos")

                Dim fila As Data.DataRow

                'Asignamos a fila la nueva Row(Fila)del Dataset
                fila = DSetTablas.Tables("GArticulos").NewRow

                'Agregamos los valores a los campos de la tabla
                fila("ItmsGrpNam") = "TODAS"
                fila("ItmsGrpCod") = 999

                'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                DSetTablas.Tables("GArticulos").Rows.Add(fila)

                Me.CmbGrupoArticulo.DataSource = DSetTablas.Tables("GArticulos")
                Me.CmbGrupoArticulo.DisplayMember = "ItmsGrpNam"
                Me.CmbGrupoArticulo.ValueMember = "ItmsGrpCod"
                Me.CmbGrupoArticulo.SelectedValue = 999


                '''''*******************************
                ''''---------------------------------

                'LLENA COMBO ARTICULOS

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

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try

            conexion2.Open()
            Dim cmd4 As SqlCommand = Nothing
            cmd4 = New SqlCommand("SPValeSalidaCon", conexion2)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.Add("@Folio", SqlDbType.VarChar, 30).Value = TBFolio.Text
            cmd4.Parameters.Add("@Articulo", SqlDbType.VarChar, 150).Value = CmbArticulo.Text
            cmd4.Parameters.Add("@Linea", SqlDbType.VarChar, 150).Value = CmbGrupoArticulo.Text
            cmd4.Parameters.Add("@FechaIni", SqlDbType.Date).Value = DtpFechaIni.Value
            cmd4.Parameters.Add("@FechaFin", SqlDbType.Date).Value = DtpFechaTer.Value


            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da2 As New SqlDataAdapter
            da2.SelectCommand = cmd4
            da2.SelectCommand.Connection = conexion2


            ''--------------------------------------------
            Dim DsVtas As New DataSet
            da2.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "Detalle"


            DvDetalle.Table = DsVtas.Tables("Detalle")

            DGVArt.DataSource = DvDetalle


            'DisenoGrid()

            '        Else
            'MsgBox("No hay factura con este folio")
            '        End If

            DisenoGrid()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
                conexion2.Close()
            End If
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim frm As New ValeSalida
        'frm.ShowDialog()
        Me.Close()
    End Sub

    Private Sub DisenoGrid()
        With Me.DGVArt

            '.DataSource = DtAgte
            .ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue


            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'Color de linea del grid

            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try

                .Columns(0).HeaderText = "Folio"
                .Columns(0).Width = 50
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Artículo"
                .Columns(1).Width = 130

                '.Columns(2).HeaderText = "Descripción"
                '.Columns(2).Width = 160
                '.Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                '.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(2).HeaderText = "Cantidad"
                .Columns(2).Width = 60
                .Columns(2).DefaultCellStyle.Format = "###,##0"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Línea"
                .Columns(3).Width = 120
                '.Columns(4).DefaultCellStyle.Format = "$ ###,###,##0"
                '.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).HeaderText = "Descripción"
                .Columns(4).Width = 180
                '.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Format = " $ ###,###,##0.#0"

                .Columns(5).HeaderText = "Fecha"
                .Columns(5).Width = 80
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(5).DefaultCellStyle.Format = "##0.#0 %"

                .Columns(6).HeaderText = "Comentarios"
                .Columns(6).Width = 220
                '.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(7).HeaderText = "Motivo"
                .Columns(7).Width = 100
                '.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(8).HeaderText = "Se Entrega a"
                .Columns(8).Width = 110
                '.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Catch ex As Exception

            End Try

        End With
    End Sub

    Private Sub CmbGrupoArticulo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbGrupoArticulo.SelectedIndexChanged
        BuscaArticulos()
    End Sub

    Private Sub BuscaArticulos()
        Try
            If CmbGrupoArticulo.SelectedValue Is Nothing Or CmbGrupoArticulo.SelectedValue = 999 Then
                DvArticulo.RowFilter = String.Empty
                CmbArticulo.SelectedValue = "TODOS"

            Else
                DvArticulo.RowFilter = "ItmsGrpCod = " & Trim(Me.CmbGrupoArticulo.SelectedValue.ToString) & " OR ItmsGrpCod = 999"

                CmbArticulo.SelectedValue = "TODOS"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles BtnImprimir.Click
        Dim NumSalida As Long
        NumSalida = 0
        'VALIDA QUE TENGA ARTICULOS EL VALE DE SALIDA
        If DGVArt.RowCount = 0 Then
            MessageBox.Show("No se encontraron Articulos", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        'FIN VALIDA QUE TENGA ARTICULOS EL VALE DE SALIDA
        Try

            conexion2.Open()
            Dim cmd4 As SqlCommand = Nothing
            cmd4 = New SqlCommand("SPValeSalidaCon", conexion2)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.Add("@Folio", SqlDbType.VarChar, 30).Value = TBFolio.Text
            cmd4.Parameters.Add("@Articulo", SqlDbType.VarChar, 150).Value = CmbArticulo.Text
            cmd4.Parameters.Add("@Linea", SqlDbType.VarChar, 150).Value = CmbGrupoArticulo.Text
            cmd4.Parameters.Add("@FechaIni", SqlDbType.Date).Value = DtpFechaIni.Value
            cmd4.Parameters.Add("@FechaFin", SqlDbType.Date).Value = DtpFechaTer.Value

            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da2 As New SqlDataAdapter
            da2.SelectCommand = cmd4
            da2.SelectCommand.Connection = conexion2
            ''--------------------------------------------
            Dim DsVtas As New DataSet
            da2.Fill(DsVtas, "DsVtas")
            DsVtas.Tables(0).TableName = "Detalle"
            DvDetalle.Table = DsVtas.Tables("Detalle")
            DGVArt.DataSource = DvDetalle
            'DisenoGrid()




            Try
                Dim DtOVta As New DataTable("DvDetalle")



                DtOVta.Columns.Add("IdOrdVta", Type.GetType("System.Int32"))
                DtOVta.Columns.Add("FchOVta", Type.GetType("System.DateTime"))
                DtOVta.Columns.Add("UsrOVta", Type.GetType("System.String"))
                DtOVta.Columns.Add("NumLinea", Type.GetType("System.Int32"))
                DtOVta.Columns.Add("Articulo", Type.GetType("System.String"))
                DtOVta.Columns.Add("DesArt", Type.GetType("System.String"))
                DtOVta.Columns.Add("Linea", Type.GetType("System.String"))
                DtOVta.Columns.Add("Cantidad", Type.GetType("System.Int32"))
                DtOVta.Columns.Add("Comen", Type.GetType("System.String"))
                DtOVta.Columns.Add("DocTotal", Type.GetType("System.Int32"))
                DtOVta.Columns.Add("Serie", Type.GetType("System.String"))


                Dim columnas As DataColumnCollection = DtOVta.Columns


                Dim series As String = ""
                Dim _filaTemp As DataRow


                Dim contador As Integer = 0
                Dim piezas As Integer = 0

                For Each row As DataGridViewRow In Me.DGVArt.Rows
                    'AUMENTA LAS PARTIDAS DE CADA VALE
                    contador = contador + 1
                    _filaTemp = DtOVta.NewRow()
                    'ID DE SALIDA
                    _filaTemp(columnas(0)) = row.Cells(0).Value
                    'FECHA DE SALIDA ACTUAL
                    '_filaTemp(columnas(1)) = Date.Now.ToString
                    'FECHA DE SALIDA REAL
                    _filaTemp(columnas(1)) = row.Cells(5).Value.ToString
                    'USUARIO
                    _filaTemp(columnas(2)) = UsrTPM
                    'NUMERO DE FILAS
                    _filaTemp(columnas(3)) = contador
                    'ARTICULO O ITEMCODE
                    _filaTemp(columnas(4)) = row.Cells(1).Value
                    'DESCRIPCION
                    _filaTemp(columnas(5)) = row.Cells(4).Value
                    'LINEA
                    _filaTemp(columnas(6)) = row.Cells(3).Value
                    'CANTIDAD
                    _filaTemp(columnas(7)) = row.Cells(2).Value
                    'COMENTARIO
                    _filaTemp(columnas(8)) = row.Cells(6).Value
                    'SUMATORIA DE EL TOTAL DE PIEZAS
                    piezas = piezas + row.Cells(2).Value
                    'TOTAL DE PARTIDAS
                    _filaTemp(columnas(9)) = piezas
                    'COLOCA EL FOLIO DE LA SALIDA EN EL ENCAMBEZA
                    _filaTemp(columnas(10)) = " - " & TBFolio.Text

                    DtOVta.Rows.Add(_filaTemp)
                Next

                Dim informe As New CrValeSalida
                RepComsultaP.MdiParent = Inicio
                informe.SetDataSource(DtOVta)
                RepComsultaP.CrVConsulta.ReportSource = informe
                RepComsultaP.Show()

            Catch ex As Exception


                'MsgBox(ex.Message)

                MessageBox.Show("No fue posible mostrar la orden de venta. " & ex.Message, "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try






        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
                conexion2.Close()
            End If
        End Try

    End Sub
End Class