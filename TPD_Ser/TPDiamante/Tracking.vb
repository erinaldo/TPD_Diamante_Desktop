Imports System.Data.SqlClient
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Tools
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Text
Imports System.Net
Imports System.IO

Public Class Tracking
    Dim row_aux As DataRowView
    Dim conexion As SqlConnection
    Dim DSAll As New DataSet
    Dim DsVtas As DataSet
    Dim DvLP As New DataView
    Dim DvAux As New DataView
    Dim Resultado As New DataView
    Dim oStrem As System.IO.MemoryStream
    Dim Consulta As String = ""
    Dim meta_mensual As Integer = Nothing
    Dim meta_mensual_d As Decimal = Nothing
    Dim strTemp As String = ""

    Dim exApp As Application
    Dim exLibro As Workbook

    Dim SQL As New Comandos_SQL()

    Private Sub Tracking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MsgBox(Year(Date.Now))
        'MsgBox(Date.Now)
        Try
            Dim tb As New System.Data.DataTable 'DataTable
            tb.Columns.Add("Text", GetType(String))
            tb.Columns.Add("Value", GetType(Integer))
            tb.Rows.Add("Enero", 1)
            tb.Rows.Add("Febrero", 2)
            tb.Rows.Add("Marzo", 3)
            tb.Rows.Add("Abril", 4)
            tb.Rows.Add("Mayo", 5)
            tb.Rows.Add("Junio", 6)
            tb.Rows.Add("Julio", 7)
            tb.Rows.Add("Agosto", 8)
            tb.Rows.Add("Septiembre", 9)
            tb.Rows.Add("Octubre", 10)
            tb.Rows.Add("Noviembre", 11)
            tb.Rows.Add("Diciembre", 12)

            CmbMes.DataSource = tb
            CmbMes.DisplayMember = "Text"
            CmbMes.ValueMember = "Value"
            CmbMes.SelectedValue = Month(Date.Now).ToString


            conexion = New SqlConnection(StrTpm)
            'Consulta = "select t0.SlpCode, CASE when CHARINDEX('(', t0.SlpName) = 0 then t0.SlpName "
            'Consulta &= "else Substring(t0.SlpName,0, CHARINDEX('(', t0.SlpName)) end as 'SlpName', t1.Correo  "
            'Consulta &= "from OSLP t0 left join TPM.dbo.Agentes t1 on t0.SlpCode = t1.SlpCode "
            'Consulta &= "where t0.SlpCode not in  (-1, 1, 2) order by t0.SlpName "

            'Modificado por Ivan Gonzalez
            Consulta = " select distinct " +
                        "    t0.slpcode," +
                        "    CASE when CHARINDEX('(', t0.SlpName) = 0 then t0.SlpName else Substring(t0.SlpName,0, CHARINDEX('(', t0.SlpName)) end as 'SlpName'," +
                        "    t2.Correo" +
                        " from" +
                        "    SC_Objetivos t0 " +
                        "    left join SBO_TPD.dbo.OSLP t1 on t1.SlpCode = t0.slpcode" +
                        "    left join Agentes t2 on t2.SlpCode = t0.slpcode" +
                        " where" +
                        "    t0.SlpCode not in (-1, 1, 2) and (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') "

            'MODIFICADO POR IVAN GONZALEZ
            'SE MODIFICO PARA QUE PUEDAN ENTRAR LOS AGENTES DE VENTAS Y DE MARKETING
            If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Then
                '----------------
                Consulta += " order by" +
                        "    t0.slpcode "

                Dim adapter As New SqlDataAdapter(Consulta, conexion)
                adapter.Fill(DSAll)

                Dim filaClte As Data.DataRow

                filaClte = DSAll.Tables(0).NewRow
                filaClte("SlpName") = "Ventas Tuxtla"
                filaClte("Correo") = "victorvergara@tractopartesdiamante.com.mx"
                filaClte("SlpCode") = "83"
                DSAll.Tables(0).Rows.Add(filaClte)

                filaClte = DSAll.Tables(0).NewRow
                filaClte("SlpName") = "Ventas Merida"
                filaClte("Correo") = "ricardorobles@tractopartesdiamante.com.mx"
                filaClte("SlpCode") = "84"
                DSAll.Tables(0).Rows.Add(filaClte)


                filaClte = DSAll.Tables(0).NewRow
                filaClte("SlpName") = "--Ningun Resultado--"
                filaClte("Correo") = ""
                filaClte("SlpCode") = "1010"
                DSAll.Tables(0).Rows.Add(filaClte)


                DvLP.Table = DSAll.Tables(0)
                DvAux.Table = DSAll.Tables(0)
                DvLP.RowFilter = "SlpCode <> 1010"
                CmbAgte.DataSource = DvLP
                CmbAgte.DisplayMember = "SlpName"
                CmbAgte.ValueMember = "SlpCode"
                CmbAgte.SelectedIndex = -1
                '----------------
            Else
                '---------------------
                SQL.conectarTPM()
                Dim CodAgente As String = SQL.CampoEspecifico("select CodAgte from Usuarios where Id_Usuario = '" + UsrTPM + "'", "CodAgte")
                Consulta += " and t0.slpcode = " + CodAgente + " order by" +
                        "    t0.slpcode "
                Dim adapter As New SqlDataAdapter(Consulta, conexion)

                adapter.Fill(DSAll)

                'Dim tbdatos As DataTable

                'tbdatos = SQL.ConsultarTabla(Consulta)

                'If tbdatos.Then Then

                'End If
                DvLP.Table = DSAll.Tables(0)
                DvAux.Table = DSAll.Tables(0)
                DvLP.RowFilter = "SlpCode <> 1010"
                CmbAgte.DataSource = DvLP
                CmbAgte.DisplayMember = "SlpName"
                CmbAgte.ValueMember = "SlpCode"

                CmbAgte.SelectedValue = CodAgente
                'CmbAgte.Enabled = False
                TxBxMeta.Enabled = False
                SQL.Cerrar()
                '---------------------

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'MsgBox(Month(Date.Now).ToString)
    End Sub


    Private Sub TxBxMeta_TextChanged(sender As Object, e As EventArgs) Handles TxBxMeta.TextChanged
        'Dim strcadena As String = ""
        'Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
        'SqlConnection.Open()
        'Dim command As New Data.SqlClient.SqlCommand
        'command.Connection = SqlConnection
        Try
            If TxBxMeta.Text <> "" Then
                meta_mensual = CInt(TxBxMeta.Text)
                TxBxMeta.Text = Format(meta_mensual, "###,###,###,###,##0")
                TxBxMeta.SelectionStart = TxBxMeta.TextLength
                'strcadena = "insert into HistoricoTracking (SlpCode, Mes, Anio, Meta) values (" & CmbAgte.SelectedValue & ", " & CmbMes.SelectedValue & ", " & Year(Date.Now) & ", " & meta_mensual & ")"
                'command.CommandText = strcadena
                'command.ExecuteNonQuery()
            Else
                meta_mensual = Nothing
            End If
           
        Catch ex As Exception
            meta_mensual = Nothing
            TxBxMeta.SelectAll()
            MsgBox("Ingresa una cantidad valida")
        End Try
    End Sub

    Private Sub Consultar_Click(sender As Object, e As EventArgs) Handles Consultar.Click
        If (CmbAgte.SelectedIndex = -1) Then
            'MsgBox("Selecciona un agente")
            MessageBox.Show("Selecciona un agente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbAgte.Focus()
            Return
        End If

        If (CmbMes.SelectedIndex = -1) Then
            MessageBox.Show("Selecciona un mes", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'MsgBox("Selecciona un mes")
            CmbMes.Focus()
            Return

        End If

        If (meta_mensual = Nothing) Then
            'MsgBox("Ingresa una meta mensual")
            MessageBox.Show("Ingresa una meta mensual", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxBxMeta.Focus()
            Return
        End If
        
        meta_mensual_d = CDec(meta_mensual)
        Ejecutar_Consulta()
        'Modifico Ivan Gonzalez
        'SE MODIFICO PARA QUE SE SUMEN LAS VENTAS SI SON NEGATIVAS AL SIGUIENTE DIA EN CASO CONTARIO SE QUEDA CON LA VENTA DIARIA
        If DGVResultado.Rows.Count <> 0 Then

            Dim MovimientosDiarios As Decimal = 0
            Dim Bandera As Boolean = False
            Dim DiaAnterior As Decimal = 0
            Dim DiferenciaAnterior As Decimal = 0

            For Each row As DataGridViewRow In DGVResultado.Rows
                Dim MetaDiaria As String = CStr(row.Cells("Meta Diaria").Value.ToString())
                Dim Realizado As String = CStr(row.Cells("Realizado").Value.ToString())
                If Convert.ToDateTime(row.Cells("Fecha").Value).ToString("dd") > DateTime.Now.Day Then
                    row.Cells("Diferencia Diaria").Value = Realizado - MetaDiaria
                Else
                    If Bandera Then
                        If DiferenciaAnterior < 0 Then
                            DiaAnterior += Math.Abs(DiferenciaAnterior)
                            row.Cells("Meta Diaria").Value = DiaAnterior
                            DiferenciaAnterior = Realizado - DiaAnterior
                            row.Cells("Diferencia Diaria").Value = DiferenciaAnterior
                            DiaAnterior = MetaDiaria
                        Else
                            DiferenciaAnterior = Realizado - MetaDiaria
                            DiaAnterior = MetaDiaria
                        End If
                    Else
                        DiferenciaAnterior = Realizado - MetaDiaria
                        DiaAnterior = MetaDiaria
                        Bandera = True
                    End If
                End If
            Next

            For i = 0 To (DGVResultado.Rows.Count - 1)
                If DGVResultado.Item(6, i).Value < 0 Then
                    DGVResultado.Rows(i).Cells(6).Style.ForeColor = Color.Red
                Else
                    DGVResultado.Rows(i).Cells(6).Style.ForeColor = Color.DarkGreen
                End If
            Next

        End If

    End Sub

    Private Sub CmbAgte_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbAgte.KeyUp
        Try
            If (e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z) Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
                strTemp = CmbAgte.Text
                If strTemp.Trim.CompareTo(String.Empty) = 0 Then
                    DvLP.RowFilter = String.Empty
                    DvLP.RowFilter = "SlpCode <> 1010"
                Else
                    Dim strRowFilter As String = String.Concat("SlpName LIKE '%", CmbAgte.Text, "%' and SlpCode <> 1010 ")
                    DvLP.RowFilter = strRowFilter
                    'MsgBox(DvLP.Count)
                    If DvLP.Count = 0 Then
                        DvLP.RowFilter = "SlpCode = 1010"
                    End If

                End If


                CmbAgte.Text = ""
                CmbAgte.Text = strTemp
                CmbAgte.SelectionStart = strTemp.Length
                CmbAgte.SelectedIndex = -1
                CmbAgte.DroppedDown = True
                CmbAgte.SelectedIndex = -1
                CmbAgte.Text = ""
                CmbAgte.Text = strTemp
                CmbAgte.SelectionStart = strTemp.Length

            End If



            'DvClte.RowFilter = "Nombre2 like '%" & CmbCliente.Text & "%'"
            'CmbCliente.DroppedDown = True
        Catch ex As Exception
            'MsgBox("errror en filtro nuevo " & ex.Message)
        End Try
    End Sub

    Private Sub CmbAgte_DropDown(sender As Object, e As EventArgs) Handles CmbAgte.DropDown
        Me.Cursor = Cursors.Arrow

        If strTemp <> "" Then
            CmbAgte.Text = strTemp
            CmbAgte.SelectionStart = strTemp.Length
        End If
        'CBNomEmp.SelectionStart = strTemp.Length
    End Sub

    Private Sub CmbAgte_Leave(sender As Object, e As EventArgs) Handles CmbAgte.Leave
        'MsgBox("entre")
        TxBxMeta.Text = ""
        meta_mensual = Nothing
        meta_mensual_d = Nothing
        Try
            If CmbAgte.SelectedIndex.ToString = "-1" Then
                If strTemp <> "" Then
                    CmbAgte.Text = strTemp
                    CmbAgte.SelectionStart = strTemp.Length
                End If
                CmbAgte.SelectedIndex = -1

                Return
            End If

            If CmbAgte.SelectedValue = 1010 Then
                CmbAgte.SelectedIndex = -1

                CmbAgte.Text = strTemp
                CmbAgte.SelectionStart = strTemp.Length
                Return
            End If
            'filtraClientes()
            DvAux.RowFilter = String.Empty
            DvAux.RowFilter = "SlpCode = " & CmbAgte.SelectedValue
            'MsgBox(DvAux.Item(0).Item(2).ToString)
            txtCorreo.Text = DvAux.Item(0).Item(2).ToString
            'MsgBox(DvAux.Table.Rows(0)(2).ToString)
            BuscaMeta()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Ejecutar_Consulta()
        Dim cmd4 As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        DsVtas = New DataSet
        Try
           
            cnn = New SqlConnection(StrTpm)
            If CmbAgte.SelectedValue.ToString = "83" Then
                cmd4 = New SqlCommand("SPTrackingDiarioTuxtla", cnn)
            ElseIf CmbAgte.SelectedValue.ToString = "84" Then
                cmd4 = New SqlCommand("SPTrackingDiarioMerida", cnn)
            Else
                cmd4 = New SqlCommand("SPTrackingDiario2", cnn)
            End If

            cmd4.CommandType = CommandType.StoredProcedure
            'cmd4.Parameters.AddWithValue("@fecha_inicio", DtpFechaIni.Value)
            'cmd4.Parameters.AddWithValue("@fecha_fin", DateTimePicker1.Value)
            cmd4.Parameters.AddWithValue("@mes", CmbMes.SelectedValue.ToString)
            cmd4.Parameters.AddWithValue("@meta", meta_mensual_d)
            cmd4.Parameters.AddWithValue("@cod_agte", CmbAgte.SelectedValue.ToString)
            cnn.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 2000
            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            cnn.Close()

            da.Fill(DsVtas)
            Resultado.Table = DsVtas.Tables(0)
            DGVResultado.DataSource = Resultado
            'MsgBox(DsVtas.Tables(0).Rows(0)(1).ToString)
            'MsgBox(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString)
            'MsgBox(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(6).ToString)
            If DsVtas.Tables(0).Rows.Count <> 0 Then


                'MsgBox(CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4)).ToString("###,###,###.#0"))
                Label8.Text = DsVtas.Tables(0).Rows(0)(1).ToString
                Label9.Text = CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4)).ToString("$ ###,###,###.#0")
                TextBox1.Text = CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4)).ToString("$ ###,###,###.#0")
                Label10.Text = CDec((CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString) - meta_mensual_d)).ToString("$ ###,###,###.#0")

                If CDec((CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString) - meta_mensual_d)) < 0 Then
                    Label10.ForeColor = Color.DarkRed
                Else
                    Label10.ForeColor = Color.DarkGreen
                End If

                Label4.Visible = True
                Label5.Visible = True
                Label7.Visible = True
                Label8.Visible = True
                Label9.Visible = True
                Label10.Visible = True
            End If
            diseno_grid()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

    End Sub

    Public Sub diseno_grid()
        With DGVResultado
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try
                .Columns(0).HeaderText = "Fecha"
                .Columns(0).Width = 70
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(0).DefaultCellStyle.Format = String.Format("dd-MMM", Globalization.CultureInfo.CreateSpecificCulture("en-ES"))
                '.Columns(0).DefaultCellStyle.
                .Columns(0).ReadOnly = True

                .Columns(1).HeaderText = "Días hábiles restantes"
                .Columns(1).Width = 50
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).DefaultCellStyle.Format = "##"
                .Columns(1).ReadOnly = True

                '.Columns(2).HeaderText = "Meta Diaria"
                .Columns(2).Width = 100
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).DefaultCellStyle.Format = "$ ###,###,###,###,###.#0"
                .Columns(2).ReadOnly = True

                '.Columns(3).HeaderText = "Realizado"
                .Columns(3).Width = 100
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Format = "$ ###,###,###,###,###.#0"
                .Columns(3).ReadOnly = True

                '.Columns(4).HeaderText = "Acumulado"
                .Columns(4).Width = 100
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Format = "$ ###,###,###,###,###.#0"
                .Columns(4).ReadOnly = True

                .Columns(5).HeaderText = "Acumulado Óptimo"
                .Columns(5).Width = 100
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Format = "$ ###,###,###,###,###.#0"
                .Columns(5).ReadOnly = True

                '.Columns(6).HeaderText = "Diferencia Diaria"
                .Columns(6).Width = 100
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Format = "$ ###,###,###,###,###.#0"
                .Columns(6).ReadOnly = True

                '.Columns(7).HeaderText = "Diferencia Final"
                .Columns(7).Width = 100
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Format = "$ ###,###,###,###,###.#0"
                .Columns(7).ReadOnly = True

                For i = 0 To (DGVResultado.Rows.Count - 1)

                    If DGVResultado.Item(6, i).Value < 0 Then
                        DGVResultado.Rows(i).Cells(6).Style.ForeColor = Color.Red
                    Else
                        DGVResultado.Rows(i).Cells(6).Style.ForeColor = Color.DarkGreen
                    End If


                Next




            Catch ex As Exception

            End Try

        End With
    End Sub

    Private Sub BtnRecibos_Click(sender As Object, e As EventArgs) Handles BtnRecibos.Click
        mGeneraExcel()
    End Sub

    Private Sub mGeneraExcel()
        Try
            exApp = New Application
            'Dim exLibro As Workbook

            'Añadimos el Libro al programa
            exLibro = exApp.Workbooks.Add

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DGVResultado.ColumnCount
            Dim NRow As Integer = DGVResultado.RowCount

            ''Combinamos celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1:H1").Merge(True)

            exLibro.Worksheets("Hoja1").Cells.Range("A2:C2").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("G2:H2").Merge(True)

            exLibro.Worksheets("Hoja1").Cells.Range("A3:C3").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("E3:H4").MergeCells = True


            exLibro.Worksheets("Hoja1").Cells.Range("A4:C4").Merge(True)

            ''aplicamos un color de fondo ala celda o rango de celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.Color = RGB(0, 112, 192)
            exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.Color = RGB(0, 112, 192)
            exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.Color = RGB(0, 112, 192)
            exLibro.Worksheets("Hoja1").Cells.Range("A4").Interior.Color = RGB(0, 112, 192)

            exLibro.Worksheets("Hoja1").Cells.Range("E2:F2").Interior.Color = RGB(0, 112, 192)

            '************
            ''exLibro.Worksheets("Hoja1").Columns("E").NumberFormat = "@" 'Articulo'
            'exLibro.Worksheets("Hoja1").Columns("J").NumberFormat = "$ ###,##0.00" 'Articulo'

            exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 11.71 'Factura'
            exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 11.71 'Factura'
            exLibro.Worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 13.86 'Fecha'
            exLibro.Worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 16.71 'Fecha cont'
            exLibro.Worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 13.86 'Articulo'
            exLibro.Worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 13.86 'Descripcion'
            exLibro.Worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 21.14 'Linea'
            exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 18.14 'Cantidad'
            exLibro.Worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 1.57 'Comentarios'

            exLibro.Worksheets("Hoja1").Rows(1).RowHeight = 19.5
            exLibro.Worksheets("Hoja1").Rows(2).RowHeight = 20.25
            exLibro.Worksheets("Hoja1").Rows(3).RowHeight = 18.75
            exLibro.Worksheets("Hoja1").Rows(4).RowHeight = 19.5
            exLibro.Worksheets("Hoja1").Rows(5).RowHeight = 3.75
            exLibro.Worksheets("Hoja1").Rows(6).RowHeight = 56.25

            ''************

            'Cambiamos orientacion ala hola
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = CmbAgte.Text.ToString
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Meta Mensual"
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Item(2, 4) = TxBxMeta.Text
            exLibro.Worksheets("Hoja1").Cells.Item(2, 4).NumberFormat = "$#,##0.00;-$#,##0.00" 'Articulo'

            exLibro.Worksheets("Hoja1").Cells.Item(2, 5) = "Días Hábiles"
            exLibro.Worksheets("Hoja1").Cells.Item(2, 5).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Cells.Item(2, 5).Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Cells.Item(2, 7) = DsVtas.Tables(0).Rows(0)(1).ToString
            exLibro.Worksheets("Hoja1").Cells.Item(2, 7).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Realizado"
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Cells.Item(3, 4) = DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString
            exLibro.Worksheets("Hoja1").Cells.Item(3, 4).NumberFormat = "_-$* #,##0.00_-;-$* #,##0.00_-;_-$* ""-""??_-;_-@_-"

            exLibro.Worksheets("Hoja1").Cells.Item(3, 5) = CmbMes.Text.ToString
            exLibro.Worksheets("Hoja1").Cells.Item(3, 5).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Cells.Item(3, 5).VerticalAlignment = 2
            'exLibro.Worksheets("Hoja1").Cells.Range("E3").value = CmbMes.Text.ToString


            exLibro.Worksheets("Hoja1").Cells.Item(4, 1) = "Diferencia"
            exLibro.Worksheets("Hoja1").Cells.Item(4, 1).Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Cells.Item(4, 1).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Item(4, 4) = CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString) - meta_mensual_d

            If (CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString) - meta_mensual_d) < 0 Then
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Interior.Color = RGB(255, 199, 206)
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Font.Color = RGB(156, 0, 6)
            ElseIf (CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString) - meta_mensual_d) = 0 Then
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Interior.Color = RGB(255, 235, 156)
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Font.Color = RGB(156, 101, 0)
            ElseIf (CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString) - meta_mensual_d) > 0 Then
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Interior.Color = RGB(198, 239, 206)
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Font.Color = RGB(0, 97, 0)

            End If


            exLibro.Worksheets("Hoja1").Cells.Item(4, 4).NumberFormat = "_-$* #,##0.00_-;-$* #,##0.00_-;_-$* ""-""??_-;_-@_-"

            exLibro.Worksheets("Hoja1").Cells.Range("A1:H1").Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble
            exLibro.Worksheets("Hoja1").Cells.Range("E3:H4").BorderAround(1, 3)

            Dim i_aux As Integer = 1

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                If DGVResultado.Columns(i - 1).Visible = True Then
                    exLibro.Worksheets("Hoja1").Cells.Item(6, i_aux) = DGVResultado.Columns(i - 1).HeaderText.ToString
                    exLibro.Worksheets("Hoja1").Cells.Item(6, i_aux).Font.Size = 9
                    'exLibro.Worksheets("Hoja1").Cells.Item(6, i_aux).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
                    i_aux = i_aux + 1
                End If
            Next

            exLibro.Worksheets("Hoja1").Cells.Range("A6:H6").Interior.Color = RGB(0, 112, 192)


            For Fila As Integer = 0 To NRow - 1
                i_aux = 1
                For Col As Integer = 0 To NCol - 1
                    If DGVResultado.Columns(Col).Visible = True Then
                        exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, i_aux) = DGVResultado.Rows(Fila).Cells(Col).Value
                        'exLibro.Worksheets("Hoja1").Cells.Item(Fila + 8, i_aux).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
                        'exLibro.Worksheets("Hoja1").Cells.Item(Fila + 8, i_aux).Font.Size = 9
                        i_aux = i_aux + 1
                    End If
                Next
                If DGVResultado.Rows(Fila).Cells(6).Value < 0 Then
                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, 7).Interior.Color = RGB(255, 199, 206)
                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, 7).Font.Color = RGB(156, 0, 6)
                ElseIf DGVResultado.Rows(Fila).Cells(6).Value > 0 Then
                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, 7).Interior.Color = RGB(198, 239, 206)
                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, 7).Font.Color = RGB(0, 97, 0)

                End If

                Estatus.Visible = True
                ProgressBar1.Value = (Fila * 100) / NRow
            Next
            Estatus.Visible = False
            'MsgBox(NRow)
            exLibro.Worksheets("Hoja1").Cells.Range("B6:B6").WrapText = True
            exLibro.Worksheets("Hoja1").Cells.Range("F6:F6").WrapText = True

            exLibro.Worksheets("Hoja1").Cells.Range("A6:H6").Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Rows.Item(6).VerticalAlignment = 2
            exLibro.Worksheets("Hoja1").Rows.Item(6).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Range("A7:A" & (NRow + 7).ToString).NumberFormat = "dd-MMM"
            exLibro.Worksheets("Hoja1").Cells.Range("A7:H" & (NRow + 7).ToString).Font.Size = 12
            exLibro.Worksheets("Hoja1").Cells.Range("A7:B" & (NRow + 7).ToString).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Range("C7:H" & (NRow + 7).ToString).NumberFormat = "_-$* #,##0.00_-;-$* #,##0.00_-;_-$* ""-""??_-;_-@_-" 'Articulo'

            exLibro.Worksheets("Hoja1").Cells.Range("A1:H6").Font.Size = 14
            exLibro.Worksheets("Hoja1").Cells.Range("A1:H6").Font.Bold = 1


            Dim grafico As ChartObject
            Dim wks As Worksheet
            wks = exLibro.Worksheets("Hoja1")
            grafico = wks.ChartObjects.Add(Left:=677, Width:=577, Top:=83, Height:=310)
            grafico.Name = "Grafico_1"
            grafico.Chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine
            grafico.Chart.SetSourceData(Source:=exLibro.Worksheets.Application.Union(exLibro.Worksheets("Hoja1").Cells.Range("A6:a" & (NRow + 6).ToString), exLibro.Worksheets("Hoja1").Cells.Range("C6:C" & (NRow + 6).ToString), exLibro.Worksheets("Hoja1").Cells.Range("D6:D" & (NRow + 6).ToString)))
            grafico.Chart.SeriesCollection(2).Format.Line.ForeColor.RGB = RGB(192, 0, 0)
            grafico.Chart.SeriesCollection(2).Format.Line.Weight = 2.25 'Line.Weigth works ever
            grafico.Chart.SeriesCollection(1).Format.Line.Weight = 2.25 'Line.Weigth works ever

            exLibro.Worksheets("Hoja1").Cells.Range("A5").select()
            exLibro.Windows.Item(1).FreezePanes = True
            exLibro.Worksheets("Hoja1").Cells.Range("D3").select()
            'Dim temp_name As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString + ".xlsx")
            'exLibro.SaveAs(temp_name)
            'exLibro.Close()
            'exApp.Quit()
            'oStrem = New System.IO.MemoryStream(File.ReadAllBytes(temp_name))

            'MsgBox(temp_name)
            'ParMail()


            'Aplicación visible
            exLibro.Worksheets.Application.Visible = True


            exLibro = Nothing
            exApp = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub ParMail()
        'Dim CCO() As String = {vCorreoVta & "," & vCCorreo}
        Dim CCO() As String = {"dinoradorantes@tractopartesdiamante.com.mx"}
        Dim CC() As String = {""}

        Dim PARA() As String = {txtCorreo.Text}
        EnviarMail("dinoradorantes@tractopartesdiamante.com.mx", PARA, "xxx", "xxxx", CC, CCO)
    End Sub

    Public Sub EnviarMail(ByVal De As String, ByVal Para As String(), ByVal Asunto As String, ByVal Cuerpo As String, Optional ByVal CC As String() = Nothing, Optional ByVal CCO As String() = Nothing)

        Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
        Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Try

            Msg.From = New System.Net.Mail.MailAddress(De, "", System.Text.Encoding.UTF8) ' De quien se envia el Correo

            For Each From As String In Para
                If From <> "" Then Msg.To.Add(From) ' Para quien se Envia
            Next


            If CC IsNot Nothing Then
                For Each C As String In CC
                    If C <> "" Then Msg.CC.Add(C)
                Next
            End If

            If CCO IsNot Nothing Then
                For Each C As String In CCO
                    If C <> "" Then Msg.Bcc.Add(C)
                Next
            End If

            Dim Titulo As String
            Titulo = "Tracking diario " & CmbAgte.Text & " " & MonthName(CmbMes.SelectedValue, True) & " " & Year(Date.Now).ToString.Substring(2)


            Msg.Subject = Titulo

            'Dim vMensaje As String = "Ajunto Tracking diario al dia " & Day(Date.Now.AddDays(-1)) & "-" & MonthName(Date.Now)

            Dim vMensaje As String = ""
            If (CmbMes.SelectedValue < Month(Date.Now)) Then
                vMensaje = "Adjunto Tracking diario al dia " & DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(0)
            Else
                'vMensaje = "Adjunto Tracking diario al dia " & Microsoft.VisualBasic.DateAndTime.Day(Date.Now.AddDays(-1)).ToString & "/" & MonthName(Month(Date.Now)). & "/" & Year(Date.Now).ToString
                vMensaje = "Adjunto Tracking diario al dia " & Date.Now.AddDays(-1)
            End If
            'Dim aux_int As Integer = Microsoft.VisualBasic.DateAndTime.Day(Date.Now.AddDays(-1)).ToString

            'MsgBox("Adjunto Tracking diario al dia " & DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(0))
            Msg.SubjectEncoding = System.Text.Encoding.UTF8 ' Encriptando el Asunto del Mensaje
            Msg.Body = vMensaje ' Cuerpo del Mensaje 
            Msg.BodyEncoding = System.Text.Encoding.UTF8 ' Codificando el Cuerpo del Mensaje
            Msg.IsBodyHtml = False ' El Cuerpo del Mensaje no es HTML


            Dim vNomArch As String

            vNomArch = Titulo & ".xlsx"

            Dim thisAttachment As Attachment = New Attachment(oStrem, vNomArch) ' “image/jpeg”)

            Msg.Attachments.Add(thisAttachment) 'SE ADJUNTA ARCHIVO excel


            SMTP.UseDefaultCredentials = False ' Si requiere Credenciales por Defecto
            SMTP.Credentials = New System.Net.NetworkCredential("dinoradorantes@tractopartesdiamante.com.mx", "ddTr@cto2012") ' las Credenciales para poder enviar el Mensaje
            SMTP.Port = 2525 ' El puerto que utiliza para el envio de Mensajes
            SMTP.Host = "mail.tractopartesdiamante.com.mx" ' el Servidor para el envio de Mensajes
            SMTP.EnableSsl = False ' Esto es para que vaya a través de SSL(Uso de Certificado Digital) por si usamos GMail por ejm.
            SMTP.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network ' Enviando Atravez de la red
            'mail.fng-puebla.com.mx 192.168.1.7
            SMTP.Send(Msg)
            'MsgBox("Correo enviado correctamente")
            MessageBox.Show("Correo enviado correctamente", "Accion Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch exc As Exception

            MessageBox.Show("NO FUE POSIBLE ENVIAR EMAIL DE LA ORDEN DE VENTA," & Chr(13) & "INTENTE ENVIAR EMAIL NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
            MessageBoxButtons.OK, MessageBoxIcon.Error)
            'VErrOv = 1
        Finally
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            exApp = New Application
            'Dim exLibro As Workbook

            'Añadimos el Libro al programa
            exLibro = exApp.Workbooks.Add

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DGVResultado.ColumnCount
            Dim NRow As Integer = DGVResultado.RowCount

            ''Combinamos celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1:H1").Merge(True)

            exLibro.Worksheets("Hoja1").Cells.Range("A2:C2").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("G2:H2").Merge(True)

            exLibro.Worksheets("Hoja1").Cells.Range("A3:C3").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("E3:H4").MergeCells = True


            exLibro.Worksheets("Hoja1").Cells.Range("A4:C4").Merge(True)

            ''aplicamos un color de fondo ala celda o rango de celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.Color = RGB(0, 112, 192)
            exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.Color = RGB(0, 112, 192)
            exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.Color = RGB(0, 112, 192)
            exLibro.Worksheets("Hoja1").Cells.Range("A4").Interior.Color = RGB(0, 112, 192)

            exLibro.Worksheets("Hoja1").Cells.Range("E2:F2").Interior.Color = RGB(0, 112, 192)

            '************
            ''exLibro.Worksheets("Hoja1").Columns("E").NumberFormat = "@" 'Articulo'
            'exLibro.Worksheets("Hoja1").Columns("J").NumberFormat = "$ ###,##0.00" 'Articulo'

            exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 11.71 'Factura'
            exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 11.71 'Factura'
            exLibro.Worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 13.86 'Fecha'
            exLibro.Worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 16.71 'Fecha cont'
            exLibro.Worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 13.86 'Articulo'
            exLibro.Worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 13.86 'Descripcion'
            exLibro.Worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 21.14 'Linea'
            exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 18.14 'Cantidad'
            exLibro.Worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 1.57 'Comentarios'

            exLibro.Worksheets("Hoja1").Rows(1).RowHeight = 19.5
            exLibro.Worksheets("Hoja1").Rows(2).RowHeight = 20.25
            exLibro.Worksheets("Hoja1").Rows(3).RowHeight = 18.75
            exLibro.Worksheets("Hoja1").Rows(4).RowHeight = 19.5
            exLibro.Worksheets("Hoja1").Rows(5).RowHeight = 3.75
            exLibro.Worksheets("Hoja1").Rows(6).RowHeight = 56.25

            ''************

            'Cambiamos orientacion ala hola
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = CmbAgte.Text.ToString
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Meta Mensual"
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Item(2, 4) = TxBxMeta.Text
            exLibro.Worksheets("Hoja1").Cells.Item(2, 4).NumberFormat = "$#,##0.00;-$#,##0.00" 'Articulo'

            exLibro.Worksheets("Hoja1").Cells.Item(2, 5) = "Días Hábiles"
            exLibro.Worksheets("Hoja1").Cells.Item(2, 5).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Cells.Item(2, 5).Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Cells.Item(2, 7) = DsVtas.Tables(0).Rows(0)(1).ToString
            exLibro.Worksheets("Hoja1").Cells.Item(2, 7).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Realizado"
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Cells.Item(3, 4) = DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString
            exLibro.Worksheets("Hoja1").Cells.Item(3, 4).NumberFormat = "_-$* #,##0.00_-;-$* #,##0.00_-;_-$* ""-""??_-;_-@_-"

            exLibro.Worksheets("Hoja1").Cells.Item(3, 5) = CmbMes.Text.ToString
            exLibro.Worksheets("Hoja1").Cells.Item(3, 5).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Cells.Item(3, 5).VerticalAlignment = 2
            'exLibro.Worksheets("Hoja1").Cells.Range("E3").value = CmbMes.Text.ToString


            exLibro.Worksheets("Hoja1").Cells.Item(4, 1) = "Diferencia"
            exLibro.Worksheets("Hoja1").Cells.Item(4, 1).Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Cells.Item(4, 1).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Item(4, 4) = CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString) - meta_mensual_d

            If (CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString) - meta_mensual_d) < 0 Then
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Interior.Color = RGB(255, 199, 206)
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Font.Color = RGB(156, 0, 6)
            ElseIf (CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString) - meta_mensual_d) = 0 Then
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Interior.Color = RGB(255, 235, 156)
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Font.Color = RGB(156, 101, 0)
            ElseIf (CDec(DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString) - meta_mensual_d) > 0 Then
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Interior.Color = RGB(198, 239, 206)
                exLibro.Worksheets("Hoja1").Cells.Item(4, 4).Font.Color = RGB(0, 97, 0)

            End If


            exLibro.Worksheets("Hoja1").Cells.Item(4, 4).NumberFormat = "_-$* #,##0.00_-;-$* #,##0.00_-;_-$* ""-""??_-;_-@_-"

            exLibro.Worksheets("Hoja1").Cells.Range("A1:H1").Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble
            exLibro.Worksheets("Hoja1").Cells.Range("E3:H4").BorderAround(1, 3)

            Dim i_aux As Integer = 1

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                If DGVResultado.Columns(i - 1).Visible = True Then
                    exLibro.Worksheets("Hoja1").Cells.Item(6, i_aux) = DGVResultado.Columns(i - 1).HeaderText.ToString
                    exLibro.Worksheets("Hoja1").Cells.Item(6, i_aux).Font.Size = 9
                    'exLibro.Worksheets("Hoja1").Cells.Item(6, i_aux).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
                    i_aux = i_aux + 1
                End If
            Next

            exLibro.Worksheets("Hoja1").Cells.Range("A6:H6").Interior.Color = RGB(0, 112, 192)


            For Fila As Integer = 0 To NRow - 1
                i_aux = 1
                For Col As Integer = 0 To NCol - 1
                    If DGVResultado.Columns(Col).Visible = True Then
                        exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, i_aux) = DGVResultado.Rows(Fila).Cells(Col).Value
                        'exLibro.Worksheets("Hoja1").Cells.Item(Fila + 8, i_aux).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
                        'exLibro.Worksheets("Hoja1").Cells.Item(Fila + 8, i_aux).Font.Size = 9
                        i_aux = i_aux + 1
                    End If
                Next
                If DGVResultado.Rows(Fila).Cells(6).Value < 0 Then
                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, 7).Interior.Color = RGB(255, 199, 206)
                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, 7).Font.Color = RGB(156, 0, 6)
                ElseIf DGVResultado.Rows(Fila).Cells(6).Value > 0 Then
                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, 7).Interior.Color = RGB(198, 239, 206)
                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, 7).Font.Color = RGB(0, 97, 0)

                End If

                'Estatus.Visible = True
                'ProgressBar1.Value = (Fila * 100) / NRow
            Next
            'Estatus.Visible = False
            'MsgBox(NRow)
            exLibro.Worksheets("Hoja1").Cells.Range("B6:B6").WrapText = True
            exLibro.Worksheets("Hoja1").Cells.Range("F6:F6").WrapText = True

            exLibro.Worksheets("Hoja1").Cells.Range("A6:H6").Font.Color = RGB(242, 242, 242)
            exLibro.Worksheets("Hoja1").Rows.Item(6).VerticalAlignment = 2
            exLibro.Worksheets("Hoja1").Rows.Item(6).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Range("A7:A" & (NRow + 7).ToString).NumberFormat = "dd-MMM"
            exLibro.Worksheets("Hoja1").Cells.Range("A7:H" & (NRow + 7).ToString).Font.Size = 12
            exLibro.Worksheets("Hoja1").Cells.Range("A7:B" & (NRow + 7).ToString).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Range("C7:H" & (NRow + 7).ToString).NumberFormat = "_-$* #,##0.00_-;-$* #,##0.00_-;_-$* ""-""??_-;_-@_-" 'Articulo'

            exLibro.Worksheets("Hoja1").Cells.Range("A1:H6").Font.Size = 14
            exLibro.Worksheets("Hoja1").Cells.Range("A1:H6").Font.Bold = 1


            Dim grafico As ChartObject
            Dim wks As Worksheet
            wks = exLibro.Worksheets("Hoja1")
            grafico = wks.ChartObjects.Add(Left:=677, Width:=577, Top:=83, Height:=310)
            grafico.Name = "Grafico_1"
            grafico.Chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine
            grafico.Chart.SetSourceData(Source:=exLibro.Worksheets.Application.Union(exLibro.Worksheets("Hoja1").Cells.Range("A6:a" & (NRow + 6).ToString), exLibro.Worksheets("Hoja1").Cells.Range("C6:C" & (NRow + 6).ToString), exLibro.Worksheets("Hoja1").Cells.Range("D6:D" & (NRow + 6).ToString)))
            grafico.Chart.SeriesCollection(2).Format.Line.ForeColor.RGB = RGB(192, 0, 0)
            grafico.Chart.SeriesCollection(2).Format.Line.Weight = 2.25 'Line.Weigth works ever
            grafico.Chart.SeriesCollection(1).Format.Line.Weight = 2.25 'Line.Weigth works ever

            exLibro.Worksheets("Hoja1").Cells.Range("A5").select()
            exLibro.Windows.Item(1).FreezePanes = True
            exLibro.Worksheets("Hoja1").Cells.Range("D3").select()
            Dim temp_name As String = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString + ".xlsx")
            exLibro.SaveAs(temp_name)
            exLibro.Close()
            exApp.Quit()
            oStrem = New System.IO.MemoryStream(File.ReadAllBytes(temp_name))
            File.Delete(temp_name)

            ParMail()
            System.Windows.Forms.Cursor.Current = Cursors.Default

            'Aplicación visible
            'exLibro.Worksheets.Application.Visible = True


        Catch ex As Exception
            MsgBox(ex.Message)
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub CmbMes_Leave(sender As Object, e As EventArgs) Handles CmbMes.Leave
        TxBxMeta.Text = ""
        meta_mensual = Nothing
        meta_mensual_d = Nothing
        BuscaMeta()
    End Sub

    Public Sub BuscaMeta()
        '
        Dim DSetAux As New DataSet
        Dim conexion_aux As SqlConnection
        If (CmbAgte.SelectedIndex <> -1) Then
            If (CmbMes.SelectedIndex <> -1) Then
                ''MsgBox("entre")
                'conexion_aux = New SqlConnection(StrTpm)
                'Dim consult As String = "select Meta from HistoricoTracking where SlpCode = " & CmbAgte.SelectedValue & " and Mes = " & CmbMes.SelectedValue & " and Anio =  " & Year(Date.Now)
                'Dim adapter As New SqlDataAdapter(consult, conexion_aux)
                'adapter.Fill(DSetAux)
                ''MsgBox("hola " & DSetAux.Tables(0).Rows.Count)
                'If DSetAux.Tables(0).Rows.Count <> 0 Then

                '    meta_mensual = CInt(DSetAux.Tables(0).Rows(0)(0).ToString())
                '    meta_mensual_d = CDec(meta_mensual)
                '    TxBxMeta.Text = Format(meta_mensual, "###,###,###,###,##0")
                '    TxBxMeta.SelectionStart = TxBxMeta.TextLength

                '    'TxBxMeta.Text = DSetAux.Tables(0).Rows.Count
                'Else
                '    meta_mensual = Nothing
                'End If
                ''DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString()

                'Modificado por Ivan Gonzalez
                Dim consult As String = "select objetivo from SC_Objetivos where slpcode = " + CmbAgte.SelectedValue.ToString() + " and mes = " + CmbMes.SelectedValue.ToString() + " and anio = " + Year(Date.Now).ToString()
                Dim SQL As New Comandos_SQL()
                SQL.conectarTPM()
                meta_mensual = SQL.CampoEspecifico(consult, "objetivo")
                If meta_mensual = 0 Then
                    meta_mensual = "0"
                End If
                TxBxMeta.Text = Format(Decimal.Parse(meta_mensual), "###,###,###,###,##0")
                lbMetaMensual.Text = "$" + Format(Decimal.Parse(meta_mensual), "###,###,###,###,##0")
            End If
        End If


    End Sub

    Private Sub CmbMes_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbMes.SelectionChangeCommitted
        TxBxMeta.Text = ""
        meta_mensual = Nothing
        meta_mensual_d = Nothing
        BuscaMeta()
    End Sub

    Private Sub CmbAgte_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbAgte.SelectionChangeCommitted
        'MsgBox("entr2")
        TxBxMeta.Text = ""
        meta_mensual = Nothing
        meta_mensual_d = Nothing

        DvAux.RowFilter = String.Empty
        DvAux.RowFilter = "SlpCode = " & CmbAgte.SelectedValue
        'MsgBox(DvAux.Item(0).Item(2).ToString)
        txtCorreo.Text = DvAux.Item(0).Item(2).ToString

        BuscaMeta()
    End Sub

    Private Sub TxBxMeta_Leave(sender As Object, e As EventArgs) Handles TxBxMeta.Leave
        Try
            Dim DSetAux As New DataSet
            Dim conexion_aux As SqlConnection
            If (CmbAgte.SelectedIndex <> -1) Then
                If (CmbMes.SelectedIndex <> -1) Then
                    conexion_aux = New SqlConnection(StrTpm)
                    Dim consult As String = "select Meta from HistoricoTracking where SlpCode = " & CmbAgte.SelectedValue & " and Mes = " & CmbMes.SelectedValue & " and Anio =  " & Year(Date.Now)
                    Dim adapter As New SqlDataAdapter(consult, conexion_aux)
                    adapter.Fill(DSetAux)
                    'MsgBox(DSetAux.Tables(0).Rows.Count)
                    If DSetAux.Tables(0).Rows.Count = 0 Then
                        Dim strcadena As String = ""
                        Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
                        SqlConnection.Open()
                        Dim command As New Data.SqlClient.SqlCommand
                        command.Connection = SqlConnection

                        If meta_mensual <> Nothing Then
                            'meta_mensual = CInt(TxBxMeta.Text)
                            'TxBxMeta.Text = Format(meta_mensual, "###,###,###,###,##0")
                            'TxBxMeta.SelectionStart = TxBxMeta.TextLength
                            strcadena = "insert into HistoricoTracking (SlpCode, Mes, Anio, Meta) values (" & CmbAgte.SelectedValue & ", " & CmbMes.SelectedValue & ", " & Year(Date.Now) & ", " & meta_mensual & ")"
                            command.CommandText = strcadena
                            command.ExecuteNonQuery()

                        End If
                    Else
                        Dim strcadena As String = ""
                        Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
                        SqlConnection.Open()
                        Dim command As New Data.SqlClient.SqlCommand
                        command.Connection = SqlConnection

                        If meta_mensual <> Nothing Then
                            'meta_mensual = CInt(TxBxMeta.Text)
                            'TxBxMeta.Text = Format(meta_mensual, "###,###,###,###,##0")
                            'TxBxMeta.SelectionStart = TxBxMeta.TextLength
                            strcadena = "update HistoricoTracking set Meta = " & meta_mensual & " where SlpCode = " & CmbAgte.SelectedValue & " and Mes = " & CmbMes.SelectedValue & " and Anio = " & Year(Date.Now)
                            command.CommandText = strcadena
                            command.ExecuteNonQuery()

                        End If
                    End If
                    'DsVtas.Tables(0).Rows(DsVtas.Tables(0).Rows.Count - 1)(4).ToString()
                End If
            End If
        Catch ex As Exception

        End Try
        


       
    End Sub
End Class