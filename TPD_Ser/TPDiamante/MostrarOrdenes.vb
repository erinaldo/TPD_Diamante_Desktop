Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Public Class MostrarOrdenes
    Dim conexion As SqlConnection
    Dim DsVtas As DataSet
    Dim DSAll As New DataSet
    Dim Resultado As DataView
    Dim DVDetail As DataView
    Dim ConsutaLista As String

    Dim raux1 As Integer = 0
    Dim caux1 As Integer = 0
    Dim raux2 As Integer = 0
    Dim caux2 As Integer = 0

    Dim dia As String

    Dim bandera2 As Integer = 88
    Dim cant_ant As Integer

    Dim selected_row1 As Integer = 0
    Dim selected_column1 As Integer = 0
    Dim selected_row2 As Integer = 0
    Dim selected_column2 As Integer = 0

    Dim multiList As New List(Of List(Of String))

    Public Almacenista As Integer = 9999
    Public Empacador As Integer = 9999

    Public Nombre_Almacenista As String = ""
    Public Nombre_Empacador As String = ""

    Dim rojo As Color
    Dim amarillo As Color
    Private Sub MostrarOrdenes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rojo = ColorTranslator.FromHtml("#FFC6C6")
        amarillo = ColorTranslator.FromHtml("#FDFF6C")
        Try
            conexion = New SqlConnection(StrTpm)
            ConsutaLista = "select t0.fecha, t0.id, t0.puesto, t0.empleado, t1.Nombre from HistoricoAlmacen t0 inner join Almacenistas t1 on t0.empleado = t1.id where t0.fecha = '" & String.Format("{0:yyyy-MM-dd}", DTPFecha.Value) & "' order by puesto"
            Dim daGArticulo As New SqlDataAdapter(ConsutaLista, conexion)
            daGArticulo.Fill(DSAll)
            If (DSAll.Tables(0).Rows.Count <> 2) Then
                If (MessageBox.Show("Aun no has seleccion ningun almacenista o empacador para el dia de hoy.", _
                                 "Aviso.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Warning)) = MsgBoxResult.Ok Then
                    Dim frm As New AnalisisAlmacen
                    frm.ShowDialog()
                End If
            Else
                Almacenista = CInt(DSAll.Tables(0).Rows(0)(3).ToString)
                Nombre_Almacenista = DSAll.Tables(0).Rows(0)(4).ToString
                Empacador = CInt(DSAll.Tables(0).Rows(1)(3).ToString)
                Nombre_Empacador = DSAll.Tables(0).Rows(1)(4).ToString
            End If

        Catch ex As Exception
            'MsgBox("hola:" & ex.Message)
        End Try
        If Nombre_Almacenista <> "" And Nombre_Empacador <> "" Then
            Label10.Text = Nombre_Almacenista
            Label6.Text = Nombre_Empacador
            Label2.Visible = True
            Label5.Visible = True
            Label6.Visible = True
            Label10.Visible = True
        End If
        Ejecutar_Consulta()
    End Sub

    Private Sub Ejecutar_Consulta()
        bandera2 = 88
        dia = DTPFecha.Value.ToString("dddd")
        Timer1.Enabled = False
        Dim cmd4 As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        DsVtas = New DataSet
        Try
            cnn = New SqlConnection(StrTpm)
            cmd4 = New SqlCommand("SP_InsertOrds", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", DTPFecha.Value))
            'MsgBox(DTPFecha.Value.ToString("dddd"))
            cnn.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 10000
            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            cnn.Close()

            Timer1.Enabled = True

            da.Fill(DsVtas)
            Resultado = New DataView
            DVDetail = New DataView

            Resultado.Table = DsVtas.Tables(0)
            DVDetail.Table = DsVtas.Tables(1)
            DGVResultado.DataSource = Nothing 'aca entra'
            DGVDetalle.DataSource = Nothing

            selected_row1 = raux1
            selected_column1 = caux1

            selected_row2 = raux2
            selected_column2 = caux2
            DGVResultado.DataSource = Resultado 'aca entra y pierde los datos'
            DGVDetalle.DataSource = DVDetail

            For Each row As DataGridViewRow In DGVResultado.Rows
                'MsgBox("hola")
                DVDetail.RowFilter = "DocEntry = '" & row.Cells("DocNum").Value.ToString & "'"
                row.Cells("NumPiezas").Value = DVDetail.Count
                'If row.Cells("CANCELED").Value.ToString = "Y" Then
                '    'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
                '    row.DefaultCellStyle.BackColor = rojo
                '    'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
                'End If
                'If row.Cells("Accion").Value.ToString = "Guardar" Then
                '    'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
                '    MsgBox("amarillo")
                '    row.DefaultCellStyle.BackColor = amarillo
                '    'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
                'End If
            Next
            'Return

            If dia = "sábado" Then
                For Each row As DataGridViewRow In DGVResultado.Rows
                    'MsgBox(row.Cells("TrnspCode").Value.ToString)
                    If row.Cells("TrnspCode").Value.ToString = "43" Or row.Cells("TrnspCode").Value.ToString = "44" Then 'PAQUETERIA LOGEX'
                        row.Cells("HorarioPaq").Value = "13:00"
                    ElseIf row.Cells("TrnspCode").Value.ToString = "9" Or row.Cells("TrnspCode").Value.ToString = "10" Then 'PAQUETERIA ESTAFETA'
                        row.Cells("HorarioPaq").Value = "13:20 - 13:40"
                    ElseIf row.Cells("TrnspCode").Value.ToString = "20" Or row.Cells("TrnspCode").Value.ToString = "21" Then 'PAQUETERIA PAQUETE EXPRESS'
                        row.Cells("HorarioPaq").Value = "13:00 - 13:30"
                    ElseIf row.Cells("TrnspCode").Value.ToString = "28" Or row.Cells("TrnspCode").Value.ToString = "29" Then 'PAQUETERIA PAQUETE EXPRESS'
                        row.Cells("HorarioPaq").Value = "11:15 - 11:30"
                    Else
                        If row.Cells("Paqueteria").Value.ToString <> "" Then
                            row.Cells("HorarioPaq").Value = "11:15 - 11:30"
                        End If
                    End If
                Next
            Else
                For Each row As DataGridViewRow In DGVResultado.Rows
                    'MsgBox(row.Cells("TrnspCode").Value.ToString)
                    If row.Cells("TrnspCode").Value.ToString = "43" Or row.Cells("TrnspCode").Value.ToString = "44" Then 'PAQUETERIA LOGEX'
                        row.Cells("HorarioPaq").Value = "18:00"
                    ElseIf row.Cells("TrnspCode").Value.ToString = "9" Or row.Cells("TrnspCode").Value.ToString = "10" Then 'PAQUETERIA ESTAFETA'
                        row.Cells("HorarioPaq").Value = "18:20 - 18:40"
                    ElseIf row.Cells("TrnspCode").Value.ToString = "20" Or row.Cells("TrnspCode").Value.ToString = "21" Then 'PAQUETERIA PAQUETE EXPRESS'
                        row.Cells("HorarioPaq").Value = "17:30 - 18:15"
                    ElseIf row.Cells("TrnspCode").Value.ToString = "28" Or row.Cells("TrnspCode").Value.ToString = "29" Then 'PAQUETERIA PAQUETE EXPRESS'
                        row.Cells("HorarioPaq").Value = "15:00"
                    Else
                        If row.Cells("Paqueteria").Value.ToString <> "" Then
                            row.Cells("HorarioPaq").Value = "16:15 - 16:30"
                        End If
                    End If
                Next
            End If

            cambia_values()
            DGVResultado.Columns().Remove("Accion")
            Dim col As New DataGridViewLinkColumn
            col.DataPropertyName = "Accion"
            col.HeaderText = "Accion"
            col.Name = "Accion"
            col.SortMode = DataGridViewColumnSortMode.Automatic
            DGVResultado.Columns.Insert(14, col)

            Try
                DGVResultado.CurrentCell = DGVResultado.Rows(selected_row1).Cells(selected_column1) 'aca entra'
                FiltraDetalle()
                DGVDetalle.CurrentCell = DGVDetalle.Rows(selected_row2).Cells(selected_column2)
            Catch ex As Exception

            End Try
            raux1 = selected_row1
            bandera2 = 555
            diseno_grid()
            diseno_grid_detail()

            If DGVDetalle.RowCount <> 0 Then
                For Each l As List(Of String) In multiList

                    DGVDetalle.Rows(l.Item(0).ToString).Cells("Real").Value = CInt(l.Item(1).ToString)
                    DGVDetalle.Rows(l.Item(0).ToString).DefaultCellStyle.BackColor = rojo
                Next
            Else
                multiList = New List(Of List(Of String))
            End If


        Catch ex As Exception
            'MsgBox("hola2: " & ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Ejecutar_Consulta()
    End Sub

    Public Sub diseno_grid()
        With Me.DGVResultado
            .RowHeadersVisible = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue

            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try

                .Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("DocNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
                .Columns("DocNum").DefaultCellStyle.ForeColor = Color.Red
                .Columns("DocNum").HeaderText = "Ord. Vta."
                .Columns("DocNum").Width = 61
                '.Columns("DocNum").Width = 85

                .Columns("DocEntry").Visible = False

                .Columns("DocDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("DocDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                .Columns("DocDate").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("DocDate").HeaderText = "Fecha Doc"
                .Columns("DocDate").Width = 68
                '.Columns("DocDate").Width = 120

                .Columns("FechaImpresion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("FechaImpresion").DefaultCellStyle.Format = "dd-MMM-yyyy"
                .Columns("FechaImpresion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("FechaImpresion").HeaderText = "Fecha Impresion"
                .Columns("FechaImpresion").Width = 68
                '.Columns("FechaImpresion").Width = 120
                .Columns("FechaImpresion").Visible = False

                .Columns("HoraImpresion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("HoraImpresion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("HoraImpresion").DefaultCellStyle.Format = "HH:mm:ss"
                .Columns("HoraImpresion").HeaderText = "Hora Impresion"
                .Columns("HoraImpresion").Width = 50
                '.Columns("HoraImpresion").Width = 100

                .Columns("NumPiezas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("NumPiezas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .Columns("NumPiezas").DefaultCellStyle.Format = "###,###,###"
                .Columns("NumPiezas").HeaderText = "Partidas"
                .Columns("NumPiezas").Width = 45
                '.Columns("NumPiezas").Width = 65

                .Columns("Paqueteria").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Paqueteria").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("Paqueteria").Width = 130
                '.Columns("Paqueteria").Width = 160

                .Columns("TrnspCode").Visible = False

                .Columns("HorarioPaq").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("HorarioPaq").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("HorarioPaq").HeaderText = "Horario Paq."
                .Columns("HorarioPaq").Width = 72
                '.Columns("HorarioPaq").Width = 110

                .Columns("Estado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Estado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 7)
                .Columns("Estado").Width = 80
                .Columns("Estado").Visible = False

                '.Columns("Comments").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Comments").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 7)
                .Columns("Comments").HeaderText = "Comentario SAP"
                .Columns("Comments").Width = 120

                .Columns("Accion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Accion").DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold)
                .Columns("Accion").Width = 80
                '.Columns("Accion").Width = 120

                .Columns("CANCELED").Visible = False
                .Columns("DocStatus").Visible = False
                .Columns("InvntSttus").Visible = False
                .Columns("band").Visible = False


            Catch ex As Exception
                'MsgBox("trono" & ex.Message)
            End Try
        End With
    End Sub

    Public Sub diseno_grid_detail()
        With Me.DGVDetalle
            .RowHeadersVisible = False
            Dim clr1 As Color
            clr1 = ColorTranslator.FromHtml("#deeaf6")

            Dim clr2 As Color
            clr2 = ColorTranslator.FromHtml("#44546a")


            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.White
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            '.AlternatingRowsDefaultCellStyle.ForeColor = clr2

            .DefaultCellStyle.BackColor = clr1
            '.DefaultCellStyle.ForeColor = clr2
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try
                .Columns("DocEntry").Visible = False

                .Columns("LineNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 7)
                .Columns("LineNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("LineNum").HeaderText = "#"
                .Columns("LineNum").Width = 30
                .Columns("LineNum").ReadOnly = True

                .Columns("ItemCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9)
                .Columns("ItemCode").HeaderText = "Codigo Art."
                .Columns("ItemCode").Width = 110
                .Columns("ItemCode").ReadOnly = True

                .Columns("Dscription").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 7)
                .Columns("Dscription").HeaderText = "Descripcion"
                .Columns("Dscription").Width = 310
                .Columns("Dscription").ReadOnly = True

                .Columns("Quantity").DefaultCellStyle.Format = "###,####,##0"
                .Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Quantity").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .Columns("Quantity").HeaderText = "Cantidad"
                .Columns("Quantity").Width = 70
                .Columns("Quantity").ReadOnly = True

                .Columns("Real").DefaultCellStyle.Format = "###,####,##0"
                .Columns("Real").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Real").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .Columns("Real").HeaderText = "Surtido Real"
                .Columns("Real").Width = 70

                If (DGVResultado.CurrentRow.Cells("Accion").Value.ToString = "Surtir") Or (DGVResultado.CurrentRow.Cells("Accion").Value.ToString = "Descartar") Then
                    .Columns("Real").Visible = False
                Else
                    .Columns("Real").Visible = True
                End If


            Catch ex As Exception
                'MsgBox("trono" & ex.Message)
            End Try
        End With
    End Sub

    Private Sub DGVResultado_SelectionChanged(sender As Object, e As EventArgs) Handles DGVResultado.SelectionChanged
        'MsgBox("entre a selectionchanged")

        Try
            'MsgBox(DGVResultado.)
            If (bandera2 <> 88) Then
                'MsgBox("puedo borrar")
                'MsgBox("actual: " & Me.DGVResultado.CurrentCell.RowIndex.ToString & " anterior: " & raux1.ToString)
                If DGVResultado.CurrentCell.RowIndex <> raux1 Then
                    'MsgBox("puedo borrar: DG: " & DGVResultado.CurrentCell.RowIndex.ToString & " RAUX1: " & raux1.ToString)
                    multiList = New List(Of List(Of String))
                    FiltraDetalle()
                Else
                    'MsgBox("no borrar")
                    'MsgBox("no borrar: DG: " & DGVResultado.CurrentCell.RowIndex.ToString & " RAUX1: " & raux1.ToString)
                    FiltraDetalle()
                    For Each l As List(Of String) In multiList
                        DGVDetalle.Rows(l.Item(0).ToString).Cells("Real").Value = CInt(l.Item(1).ToString)
                    Next

                End If

            Else
                'MsgBox("no se puede borrar")
            End If

            raux1 = DGVResultado.CurrentCell.RowIndex
            caux1 = DGVResultado.CurrentCell.ColumnIndex
        Catch ex As Exception
        End Try
    End Sub

    Sub FiltraDetalle()
        Try
            If DGVResultado.CurrentRow.Cells("Accion").Value.ToString = "Surtir" Or (DGVResultado.CurrentRow.Cells("Accion").Value.ToString = "Descartar") Then
                DGVDetalle.Columns("Real").Visible = False
            Else

                DGVDetalle.Columns("Real").Visible = True
            End If
            DVDetail.RowFilter = "DocEntry = " & DGVResultado.Item(1, DGVResultado.CurrentRow.Index).Value.ToString
            cambia_values()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnActualizar_Click(sender As Object, e As EventArgs) Handles BtnActualizar.Click
        Ejecutar_Consulta()
    End Sub

    Private Sub MostrarOrdenes_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If (MessageBox.Show("Si cierras la ventana, no se podra obtener la hora de impresion correcta de nuevas ordenes de venta." & vbCrLf & "¿Esta seguro que desea cerrar la ventana?", _
                            "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub TxtBxBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBxBuscar.TextChanged
        BuscaOrdenes()
    End Sub

    Sub BuscaOrdenes()
        Try

            Resultado.RowFilter = "DocNum like '%" & TxtBxBuscar.Text & "%'"

        Catch exc As Exception
            'MsgBox("Bucar ordenes fallo " & exc.Message)
        End Try

    End Sub

    Private Sub DGVResultado_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVResultado.CellContentClick
        If e.RowIndex >= 0 Then
            Try
                If Me.DGVResultado.Columns(e.ColumnIndex).Name = "Accion" Then
                    If (Almacenista = 9999 And Empacador = 9999) Then
                        If (MessageBox.Show("Aun no has seleccion ningun almacenista o empacador para el dia de hoy.", _
                                 "Aviso.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Warning)) = MsgBoxResult.Ok Then
                            'MsgBox("voy a abrir")
                            Dim frm As New AnalisisAlmacen
                            frm.ShowDialog()
                        End If
                    Else
                        If (DGVResultado.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Surtir") Then
                            'MsgBox("voy a surtir")
                            If (MessageBox.Show("¿Esta seguro que desea comenzar el surtido de la orden " & DGVResultado.Rows(e.RowIndex).Cells("DocNum").Value.ToString & "?", _
                                   "Advertencia", _
                                   MessageBoxButtons.YesNo, _
                                   MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
                                insertar_registro(DGVResultado.Rows(e.RowIndex))


                                'Me.DGVResultado.Rows.Remove(DGVResultado.Rows(e.RowIndex))
                                'TBDiasRest.Text = TBDiasRest.Text + 1
                            End If
                        ElseIf (DGVResultado.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Guardar") Then
                            'MsgBox("voy a guardar")
                            If (MessageBox.Show("¿Esta seguro que desea registrar termino de surtido de la orden " & DGVResultado.Rows(e.RowIndex).Cells("DocNum").Value.ToString & "?", _
                                   "Advertencia", _
                                   MessageBoxButtons.YesNo, _
                                   MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
                                actualizar_registro(DGVResultado.Rows(e.RowIndex))

                                'Me.DGVResultado.Rows.Remove(DGVResultado.Rows(e.RowIndex))
                                'TBDiasRest.Text = TBDiasRest.Text + 1
                            End If
                        ElseIf DGVResultado.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Descartar" Then
                            'MsgBox("descartar")
                            If (MessageBox.Show("¿Esta seguro que desea descartar la orden " & DGVResultado.Rows(e.RowIndex).Cells("DocNum").Value.ToString & "?", _
                                   "Advertencia", _
                                   MessageBoxButtons.YesNo, _
                                   MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
                                If DGVResultado.Rows(e.RowIndex).Cells("band").Value.ToString = "0" Then
                                    descartar_registro(DGVResultado.Rows(e.RowIndex))
                                Else
                                    descartar_registro2(DGVResultado.Rows(e.RowIndex))
                                End If


                                'Me.DGVResultado.Rows.Remove(DGVResultado.Rows(e.RowIndex))
                                'TBDiasRest.Text = TBDiasRest.Text + 1
                            End If
                        End If

                    End If

                    'MsgBox("voy a borrar el dia " & row.Cells("DiaSol").Value.ToString)

                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Public Sub insertar_registro(ByVal fila As DataGridViewRow)
        Dim d_aux As System.DateTime
        d_aux = fila.Cells("HoraImpresion").Value
        Dim text As String = d_aux.ToString("yyyy-MM-dd HH:mm:ss.fff")


        Dim strcadena As String = ""
        Try
            Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
            SqlConnection.Open()
            Dim command As New Data.SqlClient.SqlCommand
            command.Connection = SqlConnection
            strcadena = "insert into Analisis_Almac (Fecha, DocNum, HoraImpresion, HoraTomado, HoraSurtido, HoraLiberacion, HoraTomadoEmp, HoraEmpaque, Almacenista, Empacador, Status) "
            'strcadena &= "values ('" & String.Format("{0:yyyy-MM-dd}", DTPFecha.Value) & "', '" & fila.Cells("DocNum").Value.ToString & "', '"
            strcadena &= "values ('" & String.Format("{0:yyyy-MM-dd}", fila.Cells("DocDate").Value) & "', '" & fila.Cells("DocNum").Value.ToString & "', '"
            'strcadena &= "values ('" & fila.Cells("DocDate").Value.ToString & "', '" & fila.Cells("DocNum").Value.ToString & "', '"
            strcadena &= text & "', GETDATE(), NULL, NULL, NULL, NULL, " & Almacenista.ToString & ", " & Empacador.ToString & ", 'Surtiendo')"
            command.CommandText = strcadena
            command.ExecuteNonQuery()
            fila.Cells("Accion").Value = "Guardar"
            DGVDetalle.Columns("Real").Visible = True
            fila.DefaultCellStyle.BackColor = amarillo

            MessageBox.Show("Datos Guardados Correctamente", _
                                 "Aviso.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Ocurrio un Error: " & ex.Message, _
                                 "ERROR.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub descartar_registro(ByVal fila As DataGridViewRow)
        Dim d_aux As System.DateTime
        d_aux = fila.Cells("HoraImpresion").Value
        Dim text As String = d_aux.ToString("yyyy-MM-dd HH:mm:ss.fff")


        Dim strcadena As String = ""
        Try
            Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
            SqlConnection.Open()
            Dim command As New Data.SqlClient.SqlCommand
            command.Connection = SqlConnection
            strcadena = "insert into Analisis_Almac (Fecha, DocNum, HoraImpresion, HoraTomado, HoraSurtido, HoraLiberacion, HoraTomadoEmp, HoraEmpaque, Almacenista, Empacador, Status) "
            strcadena &= "values ('" & String.Format("{0:yyyy-MM-dd}", DTPFecha.Value) & "', '" & fila.Cells("DocNum").Value.ToString & "', '"
            strcadena &= text & "', GETDATE(), NULL, NULL, NULL, NULL, " & Almacenista.ToString & ", " & Empacador.ToString & ", 'Descartada-Cancelada')"
            command.CommandText = strcadena
            command.ExecuteNonQuery()
            'fila.Cells("Accion").Value = "Guardar"
            'DGVDetalle.Columns("Real").Visible = True

            Try
                Me.DGVResultado.Rows.Remove(fila)

                'DGVResultado.CurrentCell = DGVResultado.Rows(0).Cells(0) 'aca entra'
                'DGVResultado.CurrentCell = DGVResultado.Rows(selected_row1).Cells(selected_column1) 'aca entra'
                FiltraDetalle()

                If Me.DGVResultado.Rows.Count = 0 Then
                    Me.DGVDetalle.DataSource = Nothing
                End If
            Catch ex As Exception

            End Try

            MessageBox.Show("Orden descartada correctamente", _
                                 "Aviso.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Ocurrio un Error: " & ex.Message, _
                                 "ERROR.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub descartar_registro2(ByVal fila As DataGridViewRow)

        Dim strcadena As String = ""
        Try
            Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
            SqlConnection.Open()
            Dim command As New Data.SqlClient.SqlCommand
            command.Connection = SqlConnection

            strcadena = "update Analisis_Almac set Status = 'Descartada-Cancelada' where DocNum = '" & fila.Cells("DocNum").Value.ToString & "'"
            command.CommandText = strcadena
            command.ExecuteNonQuery()

            Try
                Me.DGVResultado.Rows.Remove(fila)

                'DGVResultado.CurrentCell = DGVResultado.Rows(0).Cells(0) 'aca entra'
                'DGVResultado.CurrentCell = DGVResultado.Rows(selected_row1).Cells(selected_column1) 'aca entra'
                FiltraDetalle()

                If Me.DGVResultado.Rows.Count = 0 Then
                    Me.DGVDetalle.DataSource = Nothing
                End If


            Catch ex As Exception

            End Try

            MessageBox.Show("Orden descartada correctamente", _
                                 "Aviso.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Ocurrio un Error: " & ex.Message, _
                                 "ERROR.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub actualizar_registro(ByVal fila As DataGridViewRow)

        Dim strcadena As String = ""
        Try
            Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
            SqlConnection.Open()
            Dim command As New Data.SqlClient.SqlCommand
            command.Connection = SqlConnection

           

            For Each row As DataGridViewRow In Me.DGVDetalle.Rows

                'MsgBox(fila.Cells("ItemCode").Value.ToString)
                'MsgBox(fila.Cells(0).Value.ToString)
                strcadena = "INSERT INTO Analisis_AlmacDetail (DocNum, LineNum, ItemCode, Description, Cantidad, Real) VALUES ('"
                strcadena &= fila.Cells("DocNum").Value.ToString & "', " & row.Cells("LineNum").Value.ToString
                strcadena &= ", '" & row.Cells("ItemCode").Value.ToString & "', '" & row.Cells("Dscription").Value.ToString
                strcadena &= "', " & row.Cells("Quantity").Value.ToString & ", " & row.Cells("Real").Value.ToString & ")"
                command.CommandText = strcadena
                command.ExecuteNonQuery()

            Next

            strcadena = "update Analisis_Almac set HoraSurtido = GETDATE(), Status = 'Surtido' where DocNum = '" & fila.Cells("DocNum").Value.ToString & "'"
            command.CommandText = strcadena
            command.ExecuteNonQuery()
            
            MessageBox.Show("Datos Guardados Correctamente", _
                                 "Aviso.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Information)


            Try
                Me.DGVResultado.Rows.Remove(fila)

                'DGVResultado.CurrentCell = DGVResultado.Rows(0).Cells(0) 'aca entra'
                'DGVResultado.CurrentCell = DGVResultado.Rows(selected_row1).Cells(selected_column1) 'aca entra'
                FiltraDetalle()

                If Me.DGVResultado.Rows.Count = 0 Then
                    Me.DGVDetalle.DataSource = Nothing
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception
            MessageBox.Show("Ocurrio un Error: " & ex.Message, _
                                 "ERROR.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub pinta_labels()
        Label10.Text = Nombre_Almacenista
        Label6.Text = Nombre_Empacador
        Label2.Visible = True
        Label5.Visible = True
        Label6.Visible = True
        Label10.Visible = True
    End Sub

    Private Sub DGVDetalle_SelectionChanged(sender As Object, e As EventArgs) Handles DGVDetalle.SelectionChanged
        Try
            raux2 = DGVDetalle.CurrentCell.RowIndex
            caux2 = DGVDetalle.CurrentCell.ColumnIndex
        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub DGVDetalle_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVDetalle.CellEndEdit
        'MsgBox(e.RowIndex.ToString & " = " & DGVDetalle.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString)
        Dim list_ax As New List(Of String)
        list_ax.Add(e.RowIndex.ToString)
        list_ax.Add(DGVDetalle.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString)
        multiList.Add(list_ax)
        If (CInt(DGVDetalle.Rows(e.RowIndex).Cells("Real").Value.ToString()) <> cant_ant) Then
            DGVDetalle.Rows(e.RowIndex).DefaultCellStyle.BackColor = rojo
        End If


        'array.Add(e.RowIndex, DGVDetalle.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString)
    End Sub


    Public Sub cambia_values()
        For Each row As DataGridViewRow In DGVDetalle.Rows
            row.Cells("Real").Value = row.Cells("Quantity").Value
        Next
    End Sub

    Private Sub DGVResultado_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGVResultado.RowPrePaint
        'If DGVResultado.Rows(e.RowIndex).Cells("CANCELED").Value.ToString = "Y" Then
        '    'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
        '    DGVResultado.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightPink
        '    'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
        'End If
        If DGVResultado.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Guardar" Then
            'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
            DGVResultado.Rows(e.RowIndex).DefaultCellStyle.BackColor = amarillo
            'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
        End If

        If DGVResultado.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Descartar" Then
            'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
            DGVResultado.Rows(e.RowIndex).DefaultCellStyle.BackColor = rojo
            'DgDifCompras.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.White
        End If

       
    End Sub

    Private Sub DGVDetalle_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DGVDetalle.DataError
        MsgBox("Ingresa un valor correcto")
    End Sub

    Private Sub DGVDetalle_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DGVDetalle.CellBeginEdit
        cant_ant = CInt(DGVDetalle.Rows(e.RowIndex).Cells("Real").Value.ToString())
    End Sub
End Class