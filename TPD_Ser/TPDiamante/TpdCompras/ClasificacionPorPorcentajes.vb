Imports System.Data.SqlClient

Public Class ClasificacionPorPorcentajes
    Public StrTpm As String = conexion_universal.CadenaSQL
    Public StrCon As String = conexion_universal.CadenaSQLSAP
    Public conexion As New SqlConnection(conexion_universal.CadenaSQL)

    Dim DvTotales As New DataView
    Dim DvTotalesPuebla As New DataView
    Dim DvTotalesMerida As New DataView
    Dim DvTotalesTuxla As New DataView

    Dim DvClasificacion As New DataView


    Private Sub ClasificacionPorPorcentajes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LlenaAlmacen()
        'LlenaLinea()

    End Sub


    'Llenar combobox almacen

    'Private Sub LlenaAlmacen()

    '    Dim ConsutaLista As String

    '    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)


    '        Dim DSetTablas As New DataSet
    '        ConsutaLista = "select WhsCode, WhsName from OWHS where WhsCode='01' or WhsCode='03' or WhsCode='07'"
    '        Dim daAlmacen As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

    '        'Dim DSetTablas As New DataSet
    '        daAlmacen.Fill(DSetTablas, "Almacen")

    '        Dim fila As Data.DataRow

    '        'Asignamos a fila la nueva Row(Fila)del Dataset
    '        fila = DSetTablas.Tables("Almacen").NewRow

    '        'Agregamos los valores a los campos de la tabla
    '        fila("whsname") = "TODOS"
    '        fila("whscode") = 99

    '        'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
    '        DSetTablas.Tables("Almacen").Rows.Add(fila)

    '        Me.cbAlmacen.DataSource = DSetTablas.Tables("Almacen")
    '        Me.cbAlmacen.DisplayMember = "whsname"
    '        Me.cbAlmacen.ValueMember = "whscode"
    '        Me.cbAlmacen.SelectedValue = 99


    '    End Using
    'End Sub
    ''Llenar combobox línea

    'Sub LlenaLinea()
    '    Try
    '        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
    '            Dim ConsutaLista As String
    '            Dim ds As New DataSet
    '            ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
    '            Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

    '            Dim dsArt As New DataSet
    '            daArticulo.Fill(dsArt)

    '            Dim fila As Data.DataRow

    '            'Asignamos a fila la nueva Row(Fila)del Dataset
    '            fila = dsArt.Tables(0).NewRow

    '            'Agregamos los valores a los campos de la tabla
    '            fila("ItmsGrpNam") = "TODAS"
    '            fila("ItmsGrpCod") = 999

    '            ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
    '            dsArt.Tables(0).Rows.Add(fila)

    '            Me.cmbLinea.DataSource = dsArt.Tables(0)
    '            Me.cmbLinea.DisplayMember = "ItmsGrpNam"
    '            Me.cmbLinea.ValueMember = "ItmsGrpCod"
    '            Me.cmbLinea.SelectedValue = 999

    '        End Using
    '    Catch ex As Exception
    '        MsgBox("Error al cargar las lineas: " + ex.Message)
    '    End Try
    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Llenar_DataGridView()

        CalcularAcumulado(dgvClasificacion)
        CalcularAcumuladoPuebla(dgvPuebla)
        CalcularAcumuladoMerida(DgvMerida)
        CalcularAcumuladoTuxtla(DgvTuxtla)





    End Sub

    'Llenar los grid
    Private Sub Llenar_DataGridView()
        'Los argumentos de conexión a la base de datos 

        Dim args As String = conexion_universal.CadenaSQL

        Dim command As SqlCommand
        Dim adapter As SqlDataAdapter
        'Dim dtTable As DataTable

        Using connection As SqlConnection = New SqlConnection(args)
            command = New SqlCommand("ClasificacionPorPorcentajesPiezas", connection)
            command.CommandType = CommandType.StoredProcedure

            With command.Parameters
                'Envió los parámetros que necesito
                .Add(New SqlParameter("@FechaInicio", SqlDbType.Date)).Value = Convert.ToDateTime(dtpInicio.Value)
                .Add(New SqlParameter("@FechaFin", SqlDbType.Date)).Value = Convert.ToDateTime(dtpFinal.Value)

            End With

            Try
                adapter = New SqlDataAdapter(command)
                'adapter.SelectCommand = command
                adapter.SelectCommand.Connection = connection

                Dim DsVtas As New DataSet
                adapter.Fill(DsVtas, "DsVtas")

                DsVtas.Tables(0).TableName = "Ventas Totales"
                DsVtas.Tables(1).TableName = "Ventas Puebla"
                DsVtas.Tables(2).TableName = "Ventas Mérida"
                DsVtas.Tables(3).TableName = "Ventas Tuxtla"

                DvTotales.Table = DsVtas.Tables("Ventas Totales")
                DvTotalesPuebla.Table = DsVtas.Tables("Ventas Puebla")
                DvTotalesMerida.Table = DsVtas.Tables("Ventas Mérida")
                DvTotalesTuxla.Table = DsVtas.Tables("Ventas Tuxtla")

                dgvClasificacion.DataSource = DvTotales
                dgvPuebla.DataSource = DvTotalesPuebla
                DgvMerida.DataSource = DvTotalesMerida
                DgvTuxtla.DataSource = DvTotalesTuxla



                'dtTable = New DataTable
                'adapter.Fill(dtTable)
                'dgvClasificacion.DataSource = dtTable
                adapter.SelectCommand.CommandTimeout = 10000
            Catch expSQL As SqlException
                MsgBox(expSQL.ToString, MsgBoxStyle.OkOnly, "SQL Exception")
                Exit Sub
            End Try
        End Using
    End Sub

    'Calcular Acumulado por todos los almacenes
    Private Sub CalcularAcumulado(dgvClasificacion As Object)

        If dgvClasificacion.Rows.Count > 0 Then

            Dim Filas As Integer = dgvClasificacion.Rows.Count - 1
            Dim aux As Integer
            Dim aux2 As Integer
            Dim aux4 As Double
            Dim suma As Double

            For i As Integer = 0 To Filas

                aux = dgvClasificacion.Rows.Item(i).Cells(0).Value

                If IsDBNull(dgvClasificacion.Rows.Item(i).Cells(0).Value) Then dgvClasificacion.Rows.Item(i).Cells(0).Value = 0
                aux2 = i
                If aux2 > 0 Then
                    aux4 = dgvClasificacion.Rows.Item(i - 1).Cells(0).Value
                    If dgvClasificacion.Rows.Item(i - 1).Cells(0).Value = aux Then
                        suma = suma + dgvClasificacion.Rows.Item(i).Cells(5).Value

                        dgvClasificacion.Rows.Item(i).Cells(7).Value = Math.Round(suma, 4, MidpointRounding.ToEven)

                    Else
                        suma = 0
                        aux = aux + 1

                    End If
                Else
                    dgvClasificacion.Rows.Item(i).Cells(7).Value = 0
                End If
            Next
        End If

        actualizarBD(dgvClasificacion)
    End Sub



    Private Sub actualizarBD(dgvClasificacion As Object)

        Dim args As String = conexion_universal.CadenaSQL

        Dim query As String = "UPDATE ClasificacionPorPiezas SET AcumuladoT=@param1 WHERE ItemCode=@id"
        Dim cmd As New SqlCommand(query, conexion_uni)


        For Each row As DataGridViewRow In dgvClasificacion.Rows
            cmd.Parameters.Clear()

            cmd.Parameters.AddWithValue("@param1", Convert.ToDouble(row.Cells("AcumuladoT").Value))
            cmd.Parameters.AddWithValue("@id", Convert.ToString(row.Cells("Articulo").Value))
            conexion_uni.Open()
            cmd.ExecuteNonQuery()
            conexion_uni.Close()
        Next

    End Sub

    Private Sub actualizarBDPuebla(dgvPuebla As Object)

        Dim args As String = conexion_universal.CadenaSQL

        Dim query As String = "UPDATE ClasificacionPorPiezas SET AcumuladoPuebla=@param1 WHERE ItemCode=@id"
        Dim cmd As New SqlCommand(query, conexion_uni)


        For Each row As DataGridViewRow In dgvPuebla.Rows
            cmd.Parameters.Clear()

            cmd.Parameters.AddWithValue("@param1", Convert.ToDouble(row.Cells("Acumulado Puebla").Value))
            cmd.Parameters.AddWithValue("@id", Convert.ToString(row.Cells("Articulo").Value))
            conexion_uni.Open()
            cmd.ExecuteNonQuery()
            conexion_uni.Close()
        Next

    End Sub

    Private Sub actualizarBDMerida(dgvMerida As Object)

        Dim args As String = conexion_universal.CadenaSQL

        Dim query As String = "UPDATE ClasificacionPorPiezas SET AcumuladoMerida=@param1 WHERE ItemCode=@id"
        Dim cmd As New SqlCommand(query, conexion_uni)


        For Each row As DataGridViewRow In dgvMerida.Rows
            cmd.Parameters.Clear()

            cmd.Parameters.AddWithValue("@param1", Convert.ToDouble(row.Cells("Acumulado Merida").Value))
            cmd.Parameters.AddWithValue("@id", Convert.ToString(row.Cells("Articulo").Value))
            conexion_uni.Open()
            cmd.ExecuteNonQuery()
            conexion_uni.Close()
        Next

    End Sub

    Private Sub actualizarBDTuxtla(DgvTuxtla As Object)

        Dim args As String = conexion_universal.CadenaSQL

        Dim query As String = "UPDATE ClasificacionPorPiezas SET AcumuladoTuxtla=@param1 WHERE ItemCode=@id"
        Dim cmd As New SqlCommand(query, conexion_uni)


        For Each row As DataGridViewRow In DgvTuxtla.Rows
            cmd.Parameters.Clear()

            cmd.Parameters.AddWithValue("@param1", Convert.ToDouble(row.Cells("Acumulado Tuxtla").Value))
            cmd.Parameters.AddWithValue("@id", Convert.ToString(row.Cells("Articulo").Value))
            conexion_uni.Open()
            cmd.ExecuteNonQuery()
            conexion_uni.Close()
        Next

    End Sub

    Private Sub CalsoficacionGeneral()


        Dim Filas As Integer = dgvClasificacion.Rows.Count - 1
        For i As Integer = 0 To Filas
            If IsDBNull(dgvClasificacion.Rows.Item(i).Cells(8).Value) Then dgvClasificacion.Rows.Item(i).Cells(0).Value = 0


            If (dgvClasificacion.Rows.Item(i).Cells(5).Value) = 0 And (dgvClasificacion.Rows.Item(i).Cells(7).Value) = 1 Then

                dgvClasificacion.Rows.Item(i).Cells(8).Value = "D"
            ElseIf (dgvClasificacion.Rows.Item(i).Cells(3).Value) = " " Then
                dgvClasificacion.Rows.Item(i).Cells(8).Value = " "
            ElseIf (dgvClasificacion.Rows.Item(i).Cells(7).Value) = 0 Then
                dgvClasificacion.Rows.Item(i).Cells(8).Value = "D"
            ElseIf (dgvClasificacion.Rows.Item(i).Cells(7).Value) >= 0.95 And (dgvClasificacion.Rows.Item(i).Cells(7).Value) <= 1 Then
                dgvClasificacion.Rows.Item(i).Cells(8).Value = "C"
            ElseIf (dgvClasificacion.Rows.Item(i).Cells(7).Value) >= 0.8 And (dgvClasificacion.Rows.Item(i).Cells(7).Value) <= 0.9499999 Then
                dgvClasificacion.Rows.Item(i).Cells(8).Value = "B"
            ElseIf (dgvClasificacion.Rows.Item(i).Cells(7).Value) > 0 And (dgvClasificacion.Rows.Item(i).Cells(7).Value) <= 0.79999999 Then
                dgvClasificacion.Rows.Item(i).Cells(8).Value = "A"
            End If
        Next

    End Sub


    Private Sub CalcularAcumuladoPuebla(dgvPuebla As Object)

        If dgvPuebla.Rows.Count > 0 Then

            Dim Filas As Integer = dgvPuebla.Rows.Count - 1
            Dim aux As Integer
            Dim aux2 As Integer
            Dim aux4 As Double
            Dim suma As Double

            For i As Integer = 0 To Filas

                aux = dgvPuebla.Rows.Item(i).Cells(0).Value

                If IsDBNull(dgvPuebla.Rows.Item(i).Cells(0).Value) Then dgvPuebla.Rows.Item(i).Cells(0).Value = 0
                aux2 = i
                If aux2 > 0 Then
                    aux4 = dgvPuebla.Rows.Item(i - 1).Cells(0).Value
                    If dgvPuebla.Rows.Item(i - 1).Cells(0).Value = aux Then
                        suma = suma + dgvPuebla.Rows.Item(i).Cells(5).Value

                        dgvPuebla.Rows.Item(i).Cells(7).Value = Math.Round(suma, 4, MidpointRounding.ToEven)

                    Else
                        suma = 0
                        aux = aux + 1

                    End If
                Else
                    dgvPuebla.Rows.Item(i).Cells(7).Value = 0
                End If
            Next
        End If

        actualizarBDPuebla(dgvPuebla)

    End Sub


    Private Sub CalcularAcumuladoMerida(DgvMerida As Object)

        If DgvMerida.Rows.Count > 0 Then

            Dim Filas As Integer = DgvMerida.Rows.Count - 1
            Dim aux As Integer
            Dim aux2 As Integer
            Dim aux4 As Double
            Dim suma As Double

            For i As Integer = 0 To Filas

                aux = DgvMerida.Rows.Item(i).Cells(0).Value

                If IsDBNull(DgvMerida.Rows.Item(i).Cells(0).Value) Then DgvMerida.Rows.Item(i).Cells(0).Value = 0
                aux2 = i
                If aux2 > 0 Then
                    aux4 = DgvMerida.Rows.Item(i - 1).Cells(0).Value
                    If DgvMerida.Rows.Item(i - 1).Cells(0).Value = aux Then
                        suma = suma + DgvMerida.Rows.Item(i).Cells(5).Value

                        DgvMerida.Rows.Item(i).Cells(7).Value = Math.Round(suma, 4, MidpointRounding.ToEven)

                    Else
                        suma = 0
                        aux = aux + 1

                    End If
                Else
                    DgvMerida.Rows.Item(i).Cells(7).Value = 0
                End If
            Next
        End If

        actualizarBDMerida(DgvMerida)


    End Sub

    Private Sub CalcularAcumuladoTuxtla(DgvTuxtla As Object)

        If DgvTuxtla.Rows.Count > 0 Then

            Dim Filas As Integer = DgvTuxtla.Rows.Count - 1
            Dim aux As Integer
            Dim aux2 As Integer
            Dim aux4 As Double
            Dim suma As Double

            For i As Integer = 0 To Filas

                aux = DgvTuxtla.Rows.Item(i).Cells(0).Value

                If IsDBNull(DgvTuxtla.Rows.Item(i).Cells(0).Value) Then DgvTuxtla.Rows.Item(i).Cells(0).Value = 0
                aux2 = i
                If aux2 > 0 Then
                    aux4 = DgvTuxtla.Rows.Item(i - 1).Cells(0).Value
                    If DgvTuxtla.Rows.Item(i - 1).Cells(0).Value = aux Then
                        suma = suma + DgvTuxtla.Rows.Item(i).Cells(5).Value

                        DgvTuxtla.Rows.Item(i).Cells(7).Value = Math.Round(suma, 4, MidpointRounding.ToEven)

                    Else
                        suma = 0
                        aux = aux + 1

                    End If
                Else
                    DgvTuxtla.Rows.Item(i).Cells(7).Value = 0
                End If
            Next
        End If

        actualizarBDTuxtla(DgvTuxtla)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ClasificacionPorPiezas.Show()
    End Sub
End Class