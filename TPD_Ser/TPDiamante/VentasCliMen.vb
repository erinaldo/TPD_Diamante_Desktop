Imports System.Data.SqlClient

Public Class VentasCliMen

    Dim DvArticulo As New DataView
    Dim DvClte As New DataView
    Dim DvAgte As New DataView
    Dim DvVentasCli As New DataView

    Public MesesCol As Integer
    Public MesesProm As Decimal

    Public valAgente As Integer

    Private Sub VentasCliMen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Ventas - Cliente Mensual -- " & Me.Name.ToString & ".vb"
        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaFin.Value = Format(Date.Now, "dd/MM/yyyy")

        Dim ConsutaLista As String


        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

            mllenaComboAlmacen(SqlConnection)

            Dim DSetTablas As New DataSet

            '''''*******************************
            ConsutaLista = "SELECT T0.slpcode,T0.slpname,  " +
                        "CASE WHEN T1.GroupCode IS NULL THEN 104 ELSE T1.GroupCode END " +
                        "AS 'GroupCode' FROM OSLP T0 " +
            "LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode " +
            "WHERE (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC') AND T1.CbrGralAdicional = 'N' OR T0.SlpCode = -1 ORDER BY slpname"


            Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


            daAgte.Fill(DSetTablas, "Agentes")

            Dim filaAgte As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaAgte = DSetTablas.Tables("Agentes").NewRow

            'Agregamos los valores a los campos de la tabla
            filaAgte("slpname") = "TODOS"
            filaAgte("slpcode") = 999
            filaAgte("GroupCode") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

            DvAgte.Table = DSetTablas.Tables("Agentes")

            Me.CmbAgteVta.DataSource = DSetTablas.Tables("Agentes")
            Me.CmbAgteVta.DisplayMember = "slpname"
            Me.CmbAgteVta.ValueMember = "slpcode"
            Me.CmbAgteVta.SelectedValue = 999

            ''''---------------------------------


            ConsutaLista = "SELECT CardCode,CardName, SlpCode, GroupCode FROM OCRD WHERE CardType = 'C' AND frozenFor = 'N' ORDER BY CardName "


            Dim daClientes As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            daClientes.Fill(DSetTablas, "Clientes")

            Dim filaClientes As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaClientes = DSetTablas.Tables("Clientes").NewRow

            'Agregamos los valores a los campos de la tabla
            filaClientes("CardName") = "TODOS"
            filaClientes("CardCode") = "TODOS"
            filaClientes("slpcode") = 999
            filaClientes("groupcode") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Clientes").Rows.Add(filaClientes)

            DvClte.Table = DSetTablas.Tables("Clientes")

            Me.CmbCliente.DataSource = DvClte
            Me.CmbCliente.DisplayMember = "CardName"
            Me.CmbCliente.ValueMember = "CardCode"
            Me.CmbCliente.SelectedValue = "TODOS"

            '-----------------------------------------------------
        End Using

    End Sub

    Private Sub mllenaComboAlmacen(ByVal conexion As SqlConnection)
        Try
            Dim da As New SqlDataAdapter("SELECT GroupCode , GroupName " +
                                         "FROM OCRG with (nolock) " +
                                         "WHERE GroupType = 'C' ORDER BY GroupName ", conexion)

            Dim ds As New DataSet
            da.Fill(ds)
            ds.Tables(0).Rows.Add(0, "TODOS")
            Me.cmbAlmacen.DataSource = ds.Tables(0)
            Me.cmbAlmacen.DisplayMember = "GroupName"
            Me.cmbAlmacen.ValueMember = "GroupCode"

            If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Or UsrTPM = "DDORANTES" Then

                Me.cmbAlmacen.SelectedValue = 0

            Else

                If AlmTPM = "01" Then
                    Me.cmbAlmacen.SelectedValue = "100"
                ElseIf AlmTPM = "03" Then
                    Me.cmbAlmacen.SelectedValue = "102"
                ElseIf AlmTPM = "07" Then
                    Me.cmbAlmacen.SelectedValue = "103"
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlmacen.SelectedIndexChanged
        Try
            BuscaAgte()
            BuscaClientes()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmbAlmacen_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbAlmacen.SelectionChangeCommitted
        Try
            BuscaAgte()
            BuscaClientes()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CmbAgteVta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAgteVta.SelectedIndexChanged
        Try
            BuscaClientes()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BuscaAgte()
        If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then
            DvAgte.RowFilter = String.Empty
            Me.CmbAgteVta.SelectedValue = 999
            Me.CmbAgteVta.DataSource = DvAgte

        Else
            DvAgte.RowFilter = String.Empty
            DvAgte.RowFilter = "GroupCode = " & Trim(Me.cmbAlmacen.SelectedValue.ToString) & " OR GroupCode = 999"
            Me.CmbAgteVta.SelectedValue = 999
        End If
    End Sub

    Sub BuscaClientes()
        CmbCliente.SelectedValue = "TODOS"
        If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then

            If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
                DvClte.RowFilter = String.Empty
                CmbCliente.SelectedValue = "TODOS"
            Else
                DvClte.RowFilter = "SlpCode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString) & " OR SlpCode = 999"
                CmbCliente.SelectedValue = "TODOS"
            End If

        Else
            If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
                DvClte.RowFilter = "GroupCode = " & Trim(Me.cmbAlmacen.SelectedValue.ToString) & " OR groupcode = 999"
                CmbCliente.SelectedValue = "TODOS"
            Else
                DvClte.RowFilter = "SlpCode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString) & " OR SlpCode = 999"
                CmbCliente.SelectedValue = "TODOS"
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DGVentasCli.Columns.Clear()
        CargarRegistros()

        If CmbAgteVta.SelectedValue = 999 Then
            valAgente = 1
        Else
            valAgente = 0
        End If

        DisenoGrid()

        'MesesCol = DateDiff("m", DtpFechaIni.Value, DtpFechaFin.Value) + 1

        'Dim aux As Integer = 4 + MesesCol + 1

        'MsgBox(MesesCol)
        'MsgBox(aux)
    End Sub

    Private Sub CargarRegistros()
        Dim cnn As SqlConnection = Nothing

        Dim cmd4 As SqlCommand = Nothing

        Try
            cnn = New SqlConnection(StrTpm)

            cmd4 = New SqlCommand("SPVentasCliMen33", cnn)
            'cmd4 = New SqlCommand("SPVentasCliMen34", cnn) 'DE PRUEBA

            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@FechaIni", DtpFechaIni.Value)
            cmd4.Parameters.AddWithValue("@FechaFin", DtpFechaFin.Value)
            cmd4.Parameters.AddWithValue("@Almacen", cmbAlmacen.SelectedValue)
            cmd4.Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
            cmd4.Parameters.AddWithValue("@Cliente", CmbCliente.SelectedValue)

            cnn.Open()

            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 2000

            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()

            ''--------------------------------------------
            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "VentasCli"

            DvVentasCli.Table = DsVtas.Tables("VentasCli")

            DGVentasCli.DataSource = DvVentasCli

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

    End Sub

    Private Sub DisenoGrid()
        'obtener numero de meses entre fecha inicial y final, (columnas con las que se trabajara)
        MesesCol = DateDiff("m", DtpFechaIni.Value, DtpFechaFin.Value)

        'MsgBox(MesesCol)

    'dias obtenidos entre fecha inicial y final, (se obtiene el promedio de meses) 
        MesesProm = DateDiff("d", DtpFechaIni.Value, DtpFechaFin.Value) / 30

        With Me.DGVentasCli
            '.DataSource = DtAgte
            .ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .AllowUserToAddRows = False
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'Color de linea del grid

            DGVentasCli.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try
                .Columns(0).Name = "Cliente"
                .Columns(0).HeaderText = "Cliente"
                .Columns(0).Width = 70
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(0).Frozen = True

                .Columns(1).Name = "Nombre"
                .Columns(1).HeaderText = "Nombre"
                .Columns(1).Width = 220
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(1).Frozen = True

                .Columns(2).HeaderText = "Cod. Agente"
                .Columns(2).Width = 60
                .Columns(2).Visible = False
                .Columns(2).Frozen = True

                .Columns(3).Name = "Agente"
                .Columns(3).HeaderText = "Agente"
                .Columns(3).Width = 120


                If valAgente = 1 Then
                    .Columns(3).Visible = True
                Else
                    .Columns(3).Visible = False
                End If

                .Columns(3).Frozen = True

                .Columns(4).Name = "Ventas Totales"
                .Columns(4).HeaderText = "Ventas Totales"
                .Columns(4).Width = 105
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).DefaultCellStyle.Format = " $ ###,###,###.#0"
                .Columns(4).Frozen = True

                .Columns(5).Name = "Promedio Mensual"
                .Columns(5).HeaderText = "Promedio Mensual"
                .Columns(5).Width = 105
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(5).DefaultCellStyle.Format = "$ ###,###,###.#0"
                .Columns(5).Frozen = True

                Dim fecha As String = DtpFechaIni.Value
                Dim mes As String
                mes = fecha.Substring(3, 2)

                Dim anio As String
                anio = fecha.Substring(6, 4)

                Dim NumMes As Integer

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

                .Columns(6).Name = mes & " " & anio
                .Columns(6).HeaderText = mes & " " & anio
                .Columns(6).Width = 90
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Format = "$ ###,###,###.#0"

                For i As Integer = 1 To MesesCol
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

                        .Columns(6 + i).Name = aux2 & " " & anio + 1
                        .Columns(6 + i).HeaderText = aux2 & " " & anio + 1
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
                        .Columns(6 + i).Name = auxm & " " & anio
                        .Columns(6 + i).HeaderText = auxm & " " & anio
                    End If


                    .Columns(6 + i).Width = 90
                    '.Columns(6 + i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(6 + i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(6 + i).DefaultCellStyle.Format = "$ ###,###,###.#0"
                Next

                .Columns(6 + MesesCol + 1).Name = "ORDEN"
                .Columns(6 + MesesCol + 1).HeaderText = "ORDEN"
                .Columns(6 + MesesCol + 1).Width = 60
                '.Columns(6 + i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6 + MesesCol + 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6 + MesesCol + 1).Visible = False

                .Columns(6 + MesesCol + 2).Name = "PrecioLista"
                .Columns(6 + MesesCol + 2).HeaderText = "Precio de Lista"
                .Columns(6 + MesesCol + 2).Width = 60
                .Columns(6 + MesesCol + 2).Visible = False
                '.Columns(6 + i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6 + MesesCol + 2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Catch ex As Exception

            End Try

        End With
    End Sub


    Private Sub DGVentasCli_RowPrePaint_1(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGVentasCli.RowPrePaint
        DGVentasCli.Rows(e.RowIndex).Cells("Promedio Mensual").Style.BackColor = Color.Yellow

        Try
            MesesCol = DateDiff("m", DtpFechaIni.Value, DtpFechaFin.Value) + 1

            For i As Integer = 6 To MesesCol + 5
                For j As Integer = 0 To DGVentasCli.RowCount - 1
                    If IIf(DGVentasCli.Item(i, j).Value Is DBNull.Value, 0, DGVentasCli.Item(i, j).Value) < IIf(DGVentasCli.Item(5, j).Value Is DBNull.Value, 0, DGVentasCli.Item(5, j).Value) Then
                        DGVentasCli.Rows(j).Cells(i).Style.BackColor = Color.Gold
                    End If
                Next
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

            'Añadimos el Libro al programa
            exLibro = exApp.Workbooks.Add

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DGVentasCli.ColumnCount
            Dim NRow As Integer = DGVentasCli.RowCount

            fFormatoExcel(exLibro, NRow, NCol)

            'Llenado de encabezados
            For i As Integer = 1 To NCol
                exLibro.Worksheets("Hoja1").Cells.Item(7, i) = DGVentasCli.Columns(i - 1).Name.ToString
            Next

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 8, Col + 1) = IIf(DGVentasCli.Rows(Fila).Cells(Col).Value Is DBNull.Value, 0, DGVentasCli.Rows(Fila).Cells(Col).Value.ToString)
                Next
                Estatus.Visible = True
                ProgressBar1.Value = (Fila * 100) / NRow
            Next
            Estatus.Visible = False

            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exLibro.Worksheets("Hoja1").Rows.Item(7).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Rows.Item(7).HorizontalAlignment = 3
            'exLibro.Worksheets("Hoja1").Cells.Range("F").HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Rows.Item(5).WrapText = True
            'exLibro.Worksheets("Hoja1").Columns.AutoFit()
            exLibro.Worksheets("Hoja1").name = "Reporte Ventas-Cliente"


            'Aplicación visible
            exLibro.Worksheets.Application.Visible = True

            exLibro = Nothing
            exApp = Nothing

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fFormatoExcel(exLibro As Microsoft.Office.Interop.Excel.Workbook, NRow As Integer, NCol As Integer)

        Try
            ''Combinamos celdas 
            exLibro.Worksheets("Hoja1").Cells.Range("A1:C1").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A2:C2").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A3:C3").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A4:C4").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A5:C5").Merge(True)
            'exLibro.Worksheets("Hoja1").Cells.Range("A6:C6").Merge(True)


            ''aplicamos un color de fondo ala celda o rango de celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A4").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A5").Interior.ColorIndex = 15
            'exLibro.Worksheets("Hoja1").Cells.Range("A6").Interior.ColorIndex = 15


            ''Cambiamos orientacion ala hoja
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Reporte de Ventas-Cliente Mensual"
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Fecha del: " + DtpFechaIni.Value + "  Al  " + DtpFechaFin.Value
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Almacén: " + cmbAlmacen.Text
            exLibro.Worksheets("Hoja1").Cells.Item(4, 1) = "Agente: " + CmbAgteVta.Text
            exLibro.Worksheets("Hoja1").Cells.Item(5, 1) = "Cliente: " + CmbCliente.Text
            'exLibro.Worksheets("Hoja1").Cells.Item(6, 1) = "Línea: " + CmbLinIni.Text

            'Encabezados en NEGRITA
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(4, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(5, 1).Font.Bold = 1
            'exLibro.Worksheets("Hoja1").Cells.Item(6, 1).Font.Bold = 1

            'Formato de texto para la primera columna CLAVE ART
            'exLibro.Worksheets("Hoja1").Columns("A").NumberFormat = "@"

            'Dim Number As Integer
            Dim NumCol As Integer = DGVentasCli.RowCount - 1


            'TAMAÑO DE COLUMNAS
            exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 9
            exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 25
            exLibro.Worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 2
            exLibro.Worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 15
            exLibro.Worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 15
            exLibro.Worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 15

            exLibro.Worksheets("Hoja1").Columns(3).HIDDEN = True

            If valAgente = 1 Then
                exLibro.Worksheets("Hoja1").Columns(4).HIDDEN = False
            Else
                exLibro.Worksheets("Hoja1").Columns(4).HIDDEN = True
            End If

            exLibro.Worksheets("Hoja1").Columns(5).NumberFormat = "$ ###,###,###.#0"
            exLibro.Worksheets("Hoja1").Columns(6).NumberFormat = "$ ###,###,###.#0"

            MesesCol = DateDiff("m", DtpFechaIni.Value, DtpFechaFin.Value) + 1

            Dim aux As Integer = 6 + MesesCol + 1

            exLibro.Worksheets("Hoja1").Columns(aux).HIDDEN = True

            'Diseño de columnas de los meses
            For i As Integer = 1 To MesesCol
                exLibro.Worksheets("Hoja1").Columns(6 + i).EntireColumn.ColumnWidth = 15
                'exLibro.Worksheets("Hoja1").Columns(6 + i).numberformat = "$ ###,###,###.#0"
            Next

            'COLOR COLUMNA PROMVENTAS
            For i As Integer = 8 To NRow + 7
                exLibro.Worksheets("Hoja1").Cells.Item(i, 6).INTERIOR.COLORINDEX = 6
            Next

            'End If

            'COLOR DE ENCABEZADO
            For i As Integer = 1 To NCol
                exLibro.Worksheets("Hoja1").Cells.Item(7, i).INTERIOR.COLORINDEX = 15
            Next

            'Formato numero
            For i As Integer = 8 To NRow
                For j As Integer = 7 To MesesCol
                   
                Next
            Next


            'exLibro.Worksheets("Hoja1").Cells.Item(5, 1) = "Cliente: " + CmbCliente.Text
            'RELLENAR MENORES A PROMEDIO
            'For i As Integer = 8 To NRow
            '    For j As Integer = 7 To MesesCol
            '        If exLibro.Worksheets("Hoja1").Cells.Item(j, i) < exLibro.Worksheets("Hoja1").Cells.Item(6, i) Then
            '            exLibro.Worksheets("Hoja1").Cells.Item(j, i).INTERIOR.COLORINDEX = 44
            '        End If
            '        'exLibro.Worksheets("Hoja1").Cells.Item(7, i).INTERIOR.COLORINDEX = 15

            '    Next
            'Next

            '*COLORES
            '44 GOLD
            '6 YELLOW   
            '20 BLUE
            '15 GRAY

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try


    End Sub
End Class