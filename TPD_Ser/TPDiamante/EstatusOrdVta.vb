Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Public Class EstatusOrdVta

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
    Dim bandera_edit As Boolean = False

    Dim cant_ant As Integer

    Dim rojo As Color
    Dim amarillo As Color
    Dim verde As Color

    Dim selected_row1 As Integer = 0
    Dim selected_column1 As Integer = 0
    Dim selected_row2 As Integer = 0
    Dim selected_column2 As Integer = 0

    Dim multiList As New List(Of List(Of String))
    Dim clr1 As Color = ColorTranslator.FromHtml("#deeaf6")
    'clr1 = ColorTranslator.FromHtml("#deeaf6")

    Dim clr2 As Color = ColorTranslator.FromHtml("#44546a")
    'clr2 = ColorTranslator.FromHtml("#44546a")


    Private Sub EstatusOrdVta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rojo = ColorTranslator.FromHtml("#FFC6C6")
        amarillo = ColorTranslator.FromHtml("#FDFF6C")
        verde = ColorTranslator.FromHtml("#A6FFA0")
        Ejecutar_Consulta()
    End Sub

    Private Sub Ejecutar_Consulta()
        'MsgBox("entre a Ejecutar_Consulta")
        bandera2 = 88
        dia = DTPFecha.Value.ToString("dddd")
        Timer1.Enabled = False
        Dim cmd4 As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        DsVtas = New DataSet
        Try
            cnn = New SqlConnection(StrTpm)
            cmd4 = New SqlCommand("SPShowStatusOrd", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", DTPFecha.Value))
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
            DGVResultado.DataSource = Resultado
            DGVDetalle.DataSource = DVDetail

            For Each row As DataGridViewRow In DGVResultado.Rows
                DVDetail.RowFilter = "DocEntry = '" & row.Cells("DocNum").Value.ToString & "'"
                row.Cells("NumPiezas").Value = DVDetail.Count
            Next

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
            DVDetail.RowFilter = String.Empty
            DGVResultado.Columns().Remove("Accion")
            Dim col As New DataGridViewLinkColumn
            col.DataPropertyName = "Accion"
            col.HeaderText = "Accion"
            col.Name = "Accion"
            col.SortMode = DataGridViewColumnSortMode.Automatic
            DGVResultado.Columns.Insert(16, col)

            Try
                DGVResultado.CurrentCell = DGVResultado.Rows(selected_row1).Cells(selected_column1) 'aca entra'
                FiltraDetalle()
                DGVDetalle.CurrentCell = DGVDetalle.Rows(selected_row2).Cells(selected_column2)
                'DGVDetalle.CurrentCell = DGVDetalle.Rows(0).Cells(0)
            Catch ex As Exception

            End Try
            raux1 = selected_row1
            'MsgBox("Despues de consulta DG: " & DGVResultado.CurrentCell.RowIndex.ToString & " RAUX1: " & raux1.ToString)
            'MsgBox("selected_row1: " & selected_row1.ToString)
            bandera2 = 555
            diseno_grid()
            diseno_grid_detail()

            If DGVDetalle.RowCount <> 0 Then
                For Each l As List(Of String) In multiList

                    DGVDetalle.Rows(l.Item(0).ToString).Cells("Real").Value = CInt(l.Item(1).ToString)
                    If (DGVDetalle.Rows(l.Item(0).ToString).Cells("Real").Value.ToString <> DGVDetalle.Rows(l.Item(0).ToString).Cells("RR").Value.ToString) Then
                        'MsgBox(DGVDetalle.Rows(l.Item(0).ToString).Cells("DocEntry").Value.ToString)
                        'MsgBox(DGVDetalle.Rows(l.Item(0).ToString).Cells("Real").Value.ToString)
                        'MsgBox(DGVDetalle.Rows(l.Item(0).ToString).Cells("RR").Value.ToString)
                        'MsgBox("hola")
                        DGVDetalle.Rows(l.Item(0).ToString).DefaultCellStyle.BackColor = rojo
                    Else
                        'MsgBox(DGVDetalle.Rows(l.Item(0).ToString).Cells("DocEntry").Value.ToString & " debe volver a su color normal")
                        'MsgBox("hola2")
                        If (CInt(DGVDetalle.Rows(l.Item(0).ToString).Cells("Real").Value.ToString) <> CInt(DGVDetalle.Rows(l.Item(0).ToString).Cells("Quantity").Value.ToString)) Then
                            'MsgBox("Real:" & DGVDetalle.Rows(l.Item(0).ToString).Cells("Real").Value.ToString)
                            'MsgBox("Quantity:" & DGVDetalle.Rows(l.Item(0).ToString).Cells("Quantity").Value.ToString)
                            DGVDetalle.Rows(l.Item(0).ToString).DefaultCellStyle.BackColor = rojo
                        Else
                            If (CInt(l.Item(0).ToString) Mod 2 = 0) Then

                                DGVDetalle.Rows(l.Item(0).ToString).DefaultCellStyle.BackColor = clr1
                            Else
                                DGVDetalle.Rows(l.Item(0).ToString).DefaultCellStyle.BackColor = Color.White
                            End If
                        End If
                        'If (CInt(DGVDetalle.Rows(e.RowIndex).Cells("Real").Value.ToString()) <> CInt(DGVDetalle.Rows(e.RowIndex).Cells("Quantity").Value.ToString())) Then
                        '    DGVDetalle.Rows(e.RowIndex).DefaultCellStyle.BackColor = rojo
                        'Else
                        '    If (e.RowIndex Mod 2 = 0) Then
                        '        DGVDetalle.Rows(e.RowIndex).DefaultCellStyle.BackColor = clr1
                        '    Else
                        '        DGVDetalle.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
                        '    End If
                        'End If


                    End If

                Next
            Else
                multiList = New List(Of List(Of String))
            End If
            Try
                If bandera_edit = True Then
                    DGVResultado.BeginEdit(False)
                    bandera_edit = False
                End If
            Catch ex As Exception

            End Try

        Catch ex As Exception
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

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
                '.Columns("DocNum").DefaultCellStyle.ForeColor = Color.BlueViolet
                .Columns("DocNum").HeaderText = "Ord. Vta."
                .Columns("DocNum").Width = 61
                .Columns("DocNum").ReadOnly = True
                .Columns("DocNum").Frozen = True
                '.Columns("DocNum").Width = 100

                '.Columns("DocEntry").Visible = False

                .Columns("DocDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("DocDate").DefaultCellStyle.Format = "dd-MMM-yyyy"
                .Columns("DocDate").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("DocDate").HeaderText = "Fecha Doc."
                .Columns("DocDate").Width = 68
                .Columns("DocDate").ReadOnly = True
                .Columns("DocDate").Frozen = True
                '.Columns("DocDate").Visible = False

                .Columns("CardName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 7)
                .Columns("CardName").HeaderText = "Fecha"
                .Columns("CardName").Width = 130
                .Columns("CardName").HeaderText = "Cliente"
                '.Columns("CardName").Visible = False
                .Columns("CardName").ReadOnly = True
                .Columns("CardName").Frozen = True

                If UsrTPM = "MMAZZOCO" Then
                    .Columns("CardName").Visible = False
                Else
                    .Columns("CardName").Visible = True
                End If

                .Columns("HoraImpresion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("HoraImpresion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("HoraImpresion").DefaultCellStyle.Format = "HH:mm:ss"
                .Columns("HoraImpresion").HeaderText = "Hora Impresion"
                .Columns("HoraImpresion").Width = 50
                .Columns("HoraImpresion").ReadOnly = True
                .Columns("HoraImpresion").Frozen = True
                '.Columns("HoraImpresion").Width = 90

                .Columns("NumPiezas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("NumPiezas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .Columns("NumPiezas").DefaultCellStyle.Format = "###,###,###"
                .Columns("NumPiezas").HeaderText = "Partidas"
                .Columns("NumPiezas").Width = 45
                .Columns("NumPiezas").ReadOnly = True
                .Columns("NumPiezas").Frozen = True


                .Columns("Paqueteria").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Paqueteria").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("Paqueteria").Width = 100
                .Columns("Paqueteria").ReadOnly = True
                .Columns("Paqueteria").Frozen = True
                If UsrTPM = "MMAZZOCO" Then
                    .Columns("Paqueteria").Visible = True
                Else
                    .Columns("Paqueteria").Visible = False
                End If

                .Columns("TrnspCode").Visible = False

                .Columns("HorarioPaq").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("HorarioPaq").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("HorarioPaq").Width = 72
                .Columns("HorarioPaq").HeaderText = "Horario Paq."
                .Columns("HorarioPaq").ReadOnly = True
                .Columns("HorarioPaq").Frozen = True
                If UsrTPM = "MMAZZOCO" Then
                    .Columns("HorarioPaq").Visible = True
                Else
                    .Columns("HorarioPaq").Visible = False
                End If

                .Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Status").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 7)
                '.Columns("Status").Width = 100
                .Columns("Status").Width = 108
                .Columns("Status").ReadOnly = True
                .Columns("Status").Frozen = True

                .Columns("HoraTomado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("HoraTomado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("HoraTomado").DefaultCellStyle.Format = "HH:mm:ss"
                .Columns("HoraTomado").HeaderText = "Hora Tomada"
                .Columns("HoraTomado").Width = 50
                .Columns("HoraTomado").ReadOnly = True
                '.Columns("HoraTomado").Width = 90

                .Columns("HoraSurtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("HoraSurtido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("HoraSurtido").DefaultCellStyle.Format = "HH:mm:ss"
                .Columns("HoraSurtido").HeaderText = "Hora Fin Surtido"
                .Columns("HoraSurtido").Width = 50
                .Columns("HoraSurtido").ReadOnly = True
                '.Columns("HoraSurtido").Width = 90

                .Columns("HoraLiberacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("HoraLiberacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("HoraLiberacion").DefaultCellStyle.Format = "HH:mm:ss"
                .Columns("HoraLiberacion").HeaderText = "Hora Liberacion"
                .Columns("HoraLiberacion").Width = 55
                .Columns("HoraLiberacion").ReadOnly = True
                '.Columns("HoraLiberacion").Width = 90

                .Columns("HoraTomadoEmp").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("HoraTomadoEmp").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("HoraTomadoEmp").DefaultCellStyle.Format = "HH:mm:ss"
                .Columns("HoraTomadoEmp").HeaderText = "Hora Tomado Emp"
                .Columns("HoraTomadoEmp").Width = 50
                .Columns("HoraTomadoEmp").ReadOnly = True
                '.Columns("HoraTomadoEmp").Width = 90

                .Columns("HoraEmpaque").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("HoraEmpaque").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8)
                .Columns("HoraEmpaque").DefaultCellStyle.Format = "HH:mm:ss"
                .Columns("HoraEmpaque").HeaderText = "Hora Fin Empaque"
                .Columns("HoraEmpaque").Width = 55
                .Columns("HoraEmpaque").ReadOnly = True
                '.Columns("HoraEmpaque").Width = 90

                '.Columns("Paqueteria").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Paqueteria").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9)
                '.Columns("Paqueteria").Width = 120

                '.Columns("HorarioPaq").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("HorarioPaq").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10)
                '.Columns("HorarioPaq").Width = 80
                .Columns("Cajas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Cajas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9)
                .Columns("Cajas").DefaultCellStyle.Format = "###,###,###"
                .Columns("Cajas").Width = 37
                .Columns("Cajas").ReadOnly = True

                .Columns("Comentario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Comentario").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9)
                .Columns("Comentario").Width = 150
                If UsrTPM = "MMAZZOCO" Then
                    .Columns("Comentario").ReadOnly = True
                Else
                    .Columns("Comentario").ReadOnly = False
                End If
                '.Columns("Comentario").ReadOnly = False

                'If (DGVResultado.CurrentRow.Cells("Accion").Value.ToString = "Liberar") Then
                '    '.Columns("Caja").Visible = True
                '    .Columns("Comentario").ReadOnly = False
                'Else
                '    '.Columns("Caja").Visible = False
                '    .Columns("Comentario").ReadOnly = True
                'End If

                .Columns("Accion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Accion").DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold)
                '.Columns("Accion").Width = 65
                .Columns("Accion").Width = 95
                .Columns("Accion").ReadOnly = True
                If UsrTPM = "MMAZZOCO" Then
                    .Columns("Accion").Visible = False
                Else
                    .Columns("Accion").Visible = True
                End If
                '.Columns("Accion").Visible = False


            Catch ex As Exception
                'MsgBox("trono" & ex.Message)
            End Try
        End With
    End Sub

    Public Sub diseno_grid_detail()
        With Me.DGVDetalle
            .RowHeadersVisible = False

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
                .Columns("ItemCode").HeaderText = "Codigo "
                .Columns("ItemCode").Width = 105
                .Columns("ItemCode").ReadOnly = True

                .Columns("Dscription").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 7)
                .Columns("Dscription").HeaderText = "Descripcion"
                .Columns("Dscription").Width = 260
                .Columns("Dscription").ReadOnly = True

                .Columns("Quantity").DefaultCellStyle.Format = "###,####,##0"
                .Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Quantity").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .Columns("Quantity").HeaderText = "Num Piezas"
                .Columns("Quantity").Width = 45
                .Columns("Quantity").ReadOnly = True


                .Columns("Caja").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Caja").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .Columns("Caja").Width = 55
                .Columns("Caja").ReadOnly = True
                .Columns("Caja").Visible = False

                .Columns("Real").DefaultCellStyle.Format = "###,####,##0"
                .Columns("Real").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Real").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
                .Columns("Real").HeaderText = "Surtido Real"
                .Columns("Real").Width = 45

                .Columns("RR").Visible = False
                If (DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Empacado") Then
                    .Columns("Caja").Visible = True
                Else
                    .Columns("Caja").Visible = False
                End If


                'If (DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Surtido") Or (DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Listo Para Empacar") Then
                If (DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Surtido") Or (DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Listo Para Empacar") Or (DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Empacado") Then
                    .Columns("Real").Visible = True
                    If (DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Surtido") Then
                        .Columns("Real").ReadOnly = False
                    Else
                        .Columns("Real").ReadOnly = True
                    End If

                    '.Columns("Real").ReadOnly = False

                Else
                    .Columns("Real").Visible = False
                    .Columns("Real").ReadOnly = True
                    '.Columns("Real").ReadOnly = True

                End If


            Catch ex As Exception
                'MsgBox("trono" & ex.Message)
            End Try
        End With
    End Sub

    Private Sub BtnActualizar_Click(sender As Object, e As EventArgs) Handles BtnActualizar.Click
        'Try
        '    MsgBox(DGVResultado.CurrentRow.Cells("DocNum").Value.ToString())
        'Catch ex As Exception

        'End Try
        'If DGVResultado.CurrentRow.Cells("Comentario").IsInEditMode() Then
        '    MsgBox("se estaba editando")
        'Else
        '    MsgBox("no se estaba editando")
        'End If
        Ejecutar_Consulta()
    End Sub

    Sub FiltraDetalle()
        Try
            DVDetail.RowFilter = "DocEntry = " & DGVResultado.Item(0, DGVResultado.CurrentRow.Index).Value.ToString
            cambia_values()
            If (DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Empacado") Then
                DGVDetalle.Columns("Caja").Visible = True
            Else
                DGVDetalle.Columns("Caja").Visible = False
            End If

            If DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Surtido" Or (DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Listo Para Empacar") Or (DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Empacado") Then
                DGVDetalle.Columns("Real").Visible = True
                If DGVResultado.CurrentRow.Cells("Status").Value.ToString = "Surtido" Then
                    DGVDetalle.Columns("Real").ReadOnly = False
                Else
                    DGVDetalle.Columns("Real").ReadOnly = True
                End If

                For Each row As DataGridViewRow In DGVDetalle.Rows
                    'MsgBox("quantity: " & row.Cells("Quantity").Value.ToString)
                    'MsgBox("real: " & row.Cells("Real").Value.ToString)
                    If CInt(row.Cells("Real").Value.ToString) <> CInt(row.Cells("Quantity").Value.ToString) Then
                        'MsgBox("hola")
                        row.DefaultCellStyle.BackColor = rojo
                        'row.Cells("Quantity").Style.ForeColor = Color.BlueViolet
                        row.Cells("Real").Style.ForeColor = Color.Red
                        'row.DefaultCellStyle.ForeColor = Color.White
                    Else
                        'row.Cells("Real").Style.ForeColor = Color.Green
                    End If
                Next
            Else

                DGVDetalle.Columns("Real").Visible = False
                DGVDetalle.Columns("Real").ReadOnly = True
            End If

            'cambia_values()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGVResultado_SelectionChanged(sender As Object, e As EventArgs) Handles DGVResultado.SelectionChanged
        Try
            'MsgBox(DGVResultado.)
            'If (DGVResultado.CurrentRow.Cells("Accion").Value.ToString = "Liberar") Then
            '    '.Columns("Caja").Visible = True
            '    DGVResultado.Columns("Comentario").ReadOnly = False
            'Else
            '    '.Columns("Caja").Visible = False
            '    DGVResultado.Columns("Comentario").ReadOnly = True
            'End If

            If (bandera2 <> 88) Then
                'MsgBox("puedo borrar")
                'MsgBox("actual: " & Me.DGVResultado.CurrentCell.RowIndex.ToString & " anterior: " & raux1.ToString)
                If DGVResultado.CurrentCell.RowIndex <> raux1 Then
                    'MsgBox("puedo borrar")
                    'MsgBox("puedo borrar: DG: " & DGVResultado.CurrentCell.RowIndex.ToString & " RAUX1: " & raux1.ToString)
                    multiList = New List(Of List(Of String))
                    FiltraDetalle()
                Else
                    'MsgBox("else")
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

    Private Sub DGVResultado_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGVResultado.RowPrePaint
        For Each row As DataGridViewRow In DGVResultado.Rows
            If row.Cells("Status").Value.ToString = "Surtido" Then
                row.DefaultCellStyle.BackColor = verde
            ElseIf row.Cells("Status").Value.ToString = "Surtiendo" Then
                row.DefaultCellStyle.BackColor = amarillo
            ElseIf row.Cells("Status").Value.ToString = "En cola" Then
                row.DefaultCellStyle.BackColor = rojo
                'row.DefaultCellStyle.BackColor = Color.Red
                'row.DefaultCellStyle.ForeColor = Color.White
            ElseIf row.Cells("Status").Value.ToString = "Listo Para Empacar" Then
                row.DefaultCellStyle.BackColor = Color.LightBlue
            ElseIf row.Cells("Status").Value.ToString = "Descartada-Cancelada" Then
                row.DefaultCellStyle.BackColor = Color.LightGray
            ElseIf row.Cells("Status").Value.ToString = "Empacado" Then
                row.DefaultCellStyle.BackColor = verde
            ElseIf row.Cells("Status").Value.ToString = "Empacando" Then
                row.DefaultCellStyle.BackColor = amarillo
            End If
           
        Next
    End Sub

    Private Sub DGVResultado_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVResultado.CellContentClick
        If e.RowIndex >= 0 Then
            Try
                If Me.DGVResultado.Columns(e.ColumnIndex).Name = "Accion" Then
                    
                    If (DGVResultado.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Liberar") Then
                        'MsgBox("voy a surtir")
                        If (MessageBox.Show("¿Esta seguro que desea Liberar la orden " & DGVResultado.Rows(e.RowIndex).Cells("DocNum").Value.ToString & " para empaque?", _
                                "Advertencia", _
                                MessageBoxButtons.YesNo, _
                                MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
                            actualizar_registro(DGVResultado.Rows(e.RowIndex))
                            'insertar_registro(DGVResultado.Rows(e.RowIndex))

                            'Me.DGVResultado.Rows.Remove(DGVResultado.Rows(e.RowIndex))
                            'TBDiasRest.Text = TBDiasRest.Text + 1
                        End If
                    Else
                        ''MsgBox("voy a guardar")
                        'If (MessageBox.Show("¿Esta seguro que desea registrar termino de surtido de la orden " & DGVResultado.Rows(e.RowIndex).Cells("DocNum").Value.ToString & "?", _
                        '        "Advertencia", _
                        '        MessageBoxButtons.YesNo, _
                        '        MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
                        '    actualizar_registro(DGVResultado.Rows(e.RowIndex))

                        '    'Me.DGVResultado.Rows.Remove(DGVResultado.Rows(e.RowIndex))
                        '    'TBDiasRest.Text = TBDiasRest.Text + 1
                        'End If
                    End If

                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Public Sub actualizar_registro(ByVal fila As DataGridViewRow)

        Dim strcadena As String = ""
        Try
            Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
            SqlConnection.Open()
            Dim command As New Data.SqlClient.SqlCommand
            command.Connection = SqlConnection

            For Each row As DataGridViewRow In Me.DGVDetalle.Rows
                'MsgBox("hola")
                'MsgBox(fila.Cells("ItemCode").Value.ToString)
                'MsgBox(row.Cells(0).Value.ToString)
                If (row.Cells("Real").Value.ToString() <> row.Cells("RR").Value.ToString()) Then
                    'MsgBox("voy a modificar " & row.Cells("ItemCode").Value.ToString)
                    strcadena = "UPDATE Analisis_AlmacDetail SET Real = " & row.Cells("Real").Value.ToString & " where DocNum = '" & fila.Cells("DocNum").Value.ToString & "' "
                    strcadena &= "and LineNum = " & row.Cells("LineNum").Value.ToString & " and ItemCode = '" & row.Cells("ItemCode").Value.ToString & "'"
                    command.CommandText = strcadena
                    command.ExecuteNonQuery()
                    'MsgBox(strcadena)
                End If

                'strcadena = "INSERT INTO Analisis_AlmacDetail (DocNum, LineNum, ItemCode, Description, Cantidad, Real) VALUES ('"
                'strcadena &= fila.Cells("DocNum").Value.ToString & "', " & row.Cells("LineNum").Value.ToString
                'strcadena &= ", '" & row.Cells("ItemCode").Value.ToString & "', '" & row.Cells("Dscription").Value.ToString
                'strcadena &= "', " & row.Cells("Quantity").Value.ToString & ", " & row.Cells("Real").Value.ToString & ")"
                'command.CommandText = strcadena
                'command.ExecuteNonQuery()

            Next

            'Return
            'MsgBox("pase")

            strcadena = "update Analisis_Almac set HoraLiberacion = GETDATE(), Status = 'Listo Para Empacar' where DocNum = '" & fila.Cells("DocNum").Value.ToString & "'"
            command.CommandText = strcadena
            command.ExecuteNonQuery()
            Ejecutar_Consulta()
            'fila.Cells("Status").Value = "Listo Para Empacar"
            'fila.Cells("Accion").Value = ""
            MessageBox.Show("Datos Guardados Correctamente", _
                                 "Aviso.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Information)


            'Try
            '    Me.DGVResultado.Rows.Remove(fila)

            '    'DGVResultado.CurrentCell = DGVResultado.Rows(0).Cells(0) 'aca entra'
            '    'DGVResultado.CurrentCell = DGVResultado.Rows(selected_row1).Cells(selected_column1) 'aca entra'
            '    FiltraDetalle()

            '    If Me.DGVResultado.Rows.Count = 0 Then
            '        Me.DGVDetalle.DataSource = Nothing
            '    End If
            'Catch ex As Exception

            'End Try
        Catch ex As Exception
            MessageBox.Show("Ocurrio un Error: " & ex.Message, _
                                 "ERROR.", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Error)
        End Try
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

    Private Sub DGVDetalle_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVDetalle.CellEndEdit
        'MsgBox("termine de editar")
        'MsgBox("viejo: " & cant_ant.ToString())
        'MsgBox("nuevo: " & DGVDetalle.Rows(e.RowIndex).Cells("Real").Value.ToString())
        If (CInt(DGVDetalle.Rows(e.RowIndex).Cells("Real").Value.ToString()) > CInt(DGVDetalle.Rows(e.RowIndex).Cells("RR").Value.ToString())) Then
            MsgBox("No puedes ingresar cantidades mayores")
            DGVDetalle.Rows(e.RowIndex).Cells("Real").Value = cant_ant.ToString
        Else
            Dim list_ax As New List(Of String)
            list_ax.Add(e.RowIndex.ToString)
            list_ax.Add(DGVDetalle.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString)
            multiList.Add(list_ax)
            If (CInt(DGVDetalle.Rows(e.RowIndex).Cells("Real").Value.ToString()) <> CInt(DGVDetalle.Rows(e.RowIndex).Cells("RR").Value.ToString())) Then
                DGVDetalle.Rows(e.RowIndex).DefaultCellStyle.BackColor = rojo
            Else
                'MsgBox(e.RowIndex.ToString) clr1
                If (CInt(DGVDetalle.Rows(e.RowIndex).Cells("Real").Value.ToString()) <> CInt(DGVDetalle.Rows(e.RowIndex).Cells("Quantity").Value.ToString())) Then
                    DGVDetalle.Rows(e.RowIndex).DefaultCellStyle.BackColor = rojo
                Else
                    If (e.RowIndex Mod 2 = 0) Then
                        DGVDetalle.Rows(e.RowIndex).DefaultCellStyle.BackColor = clr1
                    Else
                        DGVDetalle.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
                    End If
                End If
                
            End If

        End If
    End Sub

    Private Sub DGVDetalle_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DGVDetalle.DataError
        MsgBox("Ingresa un valor correcto")
    End Sub

    Private Sub DGVDetalle_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DGVDetalle.CellValidating
        'MsgBox("validando")
    End Sub

    Private Sub DGVDetalle_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DGVDetalle.CellBeginEdit
        'cant_ant = DGVResultado.Rows(e.RowIndex).Cells("DocNum").Value.ToString
        cant_ant = CInt(DGVDetalle.Rows(e.RowIndex).Cells("Real").Value.ToString())
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Try
        '    MsgBox(DGVResultado.CurrentRow.Cells("DocNum").Value.ToString())
        'Catch ex As Exception

        'End Try
        If DGVResultado.RowCount <> 0 Then
            If DGVResultado.CurrentRow.Cells("Comentario").IsInEditMode() Then
                'MsgBox("se estaba editando")
                'MsgBox(DGVResultado.CurrentRow.Cells("Comentario").Value.ToString)
                DGVResultado.EndEdit()
                bandera_edit = True


            Else
                'MsgBox("no se estaba editando")
            End If
        End If
       
        Ejecutar_Consulta()
    End Sub

    Private Sub DGVDetalle_SelectionChanged(sender As Object, e As EventArgs) Handles DGVDetalle.SelectionChanged
        Try
            raux2 = DGVDetalle.CurrentCell.RowIndex
            caux2 = DGVDetalle.CurrentCell.ColumnIndex
        Catch ex As Exception

        End Try
    End Sub

    Public Sub cambia_values()
        For Each row As DataGridViewRow In DGVDetalle.Rows
            row.Cells("Real").Value = row.Cells("RR").Value
        Next
    End Sub

    Private Sub DGVResultado_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVResultado.CellEndEdit
        'MsgBox(DGVResultado.Rows(e.RowIndex).Cells("Comentario").Value.ToString)
        'Dim r As String
        'r = DGVResultado.Rows(e.RowIndex).Cells("Comentario").Value.ToString
        'MsgBox("entre a DGVResultado_CellEndEdit")
        Dim strcadena As String = ""
        Try
            Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
            SqlConnection.Open()
            Dim command As New Data.SqlClient.SqlCommand
            command.Connection = SqlConnection
            strcadena = "update Analisis_Almac set Comentario = '" & DGVResultado.Rows(e.RowIndex).Cells("Comentario").Value.ToString & "' where DocNum = '" & DGVResultado.Rows(e.RowIndex).Cells("DocNum").Value.ToString & "'"
            command.CommandText = strcadena
            command.ExecuteNonQuery()
        Catch ex As Exception

        End Try


    End Sub
End Class