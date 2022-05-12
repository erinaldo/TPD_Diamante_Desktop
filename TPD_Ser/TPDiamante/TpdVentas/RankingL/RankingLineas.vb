
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class RankingLineas


    'Conexiones a la Base de datos
    Public StrProd As String = conexion_universal.CadenaSBO_Diamante
    Public StrTpm As String = conexion_universal.CadenaSQL
    Public StrCon As String = conexion_universal.CadenaSQLSAP

    'Dim DvTotales As New DataView
    Dim DvAgentes As New DataView
    Dim DvAgentes2 As New DataView
    Dim DvLineas As New DataView
    Dim DvLineas2 As New DataView
    Dim DvClientes As New DataView

    Sub CargaCB()

        'Variable para guardar la consulta de AGENTES y SUCURSALES en los combobox
        Dim ConsutaLista As String

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

            '***********CARGAR SUCURSALES***********

            Dim DSetTablas As New DataSet
            ConsutaLista = "SELECT GroupCode , GroupName FROM OCRG with (nolock) WHERE GroupType = 'C' ORDER BY GroupName "
            Dim daGSucural As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            'Dim DSetTablas As New DataSet
            daGSucural.Fill(DSetTablas, "Sucursales")

            Dim fila As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            fila = DSetTablas.Tables("Sucursales").NewRow

            'Agregamos los valores a los campos de la tabla
            fila("GroupName") = "TODAS"
            fila("GroupCode") = 99

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Sucursales").Rows.Add(fila)

            CBSucursal.DataSource = DSetTablas.Tables("Sucursales")
            CBSucursal.DisplayMember = "GroupName"
            CBSucursal.ValueMember = "GroupCode"
            CBSucursal.SelectedValue = 99


            '---------------------------------------------------------
            '********CARGAR AGENTES***********************************

            ConsutaLista = "SELECT T0.slpcode,T0.slpname,T1.GroupCode FROM OSLP T0 "
            ConsutaLista &= "INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
            ConsutaLista &= "WHERE T1.CbrGralAdicional = 'N' AND T0.SLPCODE <> 1 and (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') ORDER BY slpname "


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

            DvAgentes2.Table = DSetTablas.Tables("Agentes")

            CBAgente.DataSource = DvAgentes2
            CBAgente.DisplayMember = "slpname"
            CBAgente.ValueMember = "slpcode"
            CBAgente.SelectedValue = 999

            '***************************

        End Using

    End Sub


    Sub DiseñoDG1()
        '-------Diseño de DATAGRID LINEAS
        With DataGridView1
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

            DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try

                .Columns(0).HeaderText = "Ranking"
                .Columns(0).Width = 50
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(0).Frozen = True   'inmovilizar columna


                .Columns(1).HeaderText = "Linea"
                .Columns(1).Width = 150
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                Dim MesIni As Int16
                MesIni = DtpFechaIni.Text.Substring(3, 2)

                Dim MesFin As Int16
                MesFin = DtpFechaFin.Text.Substring(3, 2)

                MesFin = MesFin - 1
                Dim MesAnterior As String = MonthName(MesIni)
                Dim MesSiguiente As String = MonthName(MesFin)


                Dim MesA As Int16
                MesA = DtpFechaFin.Text.Substring(3, 2)
                Dim MesActual As String = MonthName(MesA)
                'MesActual = DtpFechaFin.Text.Substring(3, 2)

                .Columns(2).HeaderText = "Totales " & MesAnterior & "-" & MesSiguiente
                .Columns(2).Width = 100
                .Columns(2).DefaultCellStyle.Format = "$ #,###,##0.##"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Promedio($) " & MesAnterior & "-" & MesSiguiente
                .Columns(3).Width = 100
                .Columns(3).DefaultCellStyle.Format = "$ #,###,##0.##0"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).HeaderText = "Promedio (%) "
                .Columns(4).Width = 100
                .Columns(4).DefaultCellStyle.Format = "#0.##0 %"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Venta Actual"
                .Columns(5).Width = 100
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(6).HeaderText = "Pronostico " & MesActual
                .Columns(6).Width = 100
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(7).HeaderText = "Pronostico (%) "
                .Columns(7).Width = 100
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Format = "##0.#0 %"

            Catch ex As Exception

            End Try


        End With

    End Sub

    Sub DisenoDG2()
        '-------Diseño de DATAGRID CLIENTES


        With DataGridView2
            '.DataSource = DtAgte
            .ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue


            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            '.RowHeadersVisible = True
            '.RowHeadersWidth = 25
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'Color de linea del grid

            DataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try
                Dim i As Integer
                For i = 0 To DataGridView2.RowCount - 2

                Next


                'If DataGridView2 IsNot Nothing Then
                '    For Each r As DataGridViewRow In DataGridView2.Rows
                '        r.HeaderCell.Value = (r.Index + 1).ToString()
                '    Next
                'End If

                .Columns(0).HeaderText = "Ranking General"
                .Columns(0).Width = 55
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(0).Frozen = True   'inmovilizar columna

                .Columns(1).HeaderText = "Clave"
                .Columns(1).Width = 50
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).Frozen = True

                .Columns(2).HeaderText = "Nombre"
                .Columns(2).Width = 170
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(2).Frozen = True

                .Columns(3).HeaderText = "Linea"
                .Columns(3).Width = 60
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).Frozen = True
                .Columns(3).Visible = False


                Dim MesIni As Int16
                MesIni = DtpFechaIni.Text.Substring(3, 2)

                Dim MesFin As Int16
                MesFin = DtpFechaFin.Text.Substring(3, 2)

                MesFin = MesFin - 1
                Dim MesAnterior As String = MonthName(MesIni)
                Dim MesSiguiente As String = MonthName(MesFin)


                Dim MesA As Int16
                MesA = DtpFechaFin.Text.Substring(3, 2)
                Dim MesActual As String = MonthName(MesA)
                'MesActual = DtpFechaFin.Text.Substring(3, 2)


                .Columns(4).HeaderText = "Totales " & MesAnterior & "-" & MesSiguiente
                .Columns(4).Width = 100
                .Columns(4).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Promedio($) " & MesAnterior & "-" & MesSiguiente
                .Columns(5).Width = 100
                .Columns(5).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .Columns(6).HeaderText = "Promedio (%)"
                .Columns(6).Width = 100
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Format = "#0.#0 %"

                .Columns(7).HeaderText = "Venta Actual"
                .Columns(7).Width = 100
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(8).HeaderText = "Pronostico($) " & MesActual
                .Columns(8).Width = 100
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(9).HeaderText = "Pronostico (%)"
                .Columns(9).Width = 100
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).DefaultCellStyle.Format = "##0.#0 %"


            Catch ex As Exception

            End Try
        End With
    End Sub


    Private Sub RankingLineas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.DtpFechaIni.Value = Format("dd/MM/yyyy")
        DtpFechaFin.Value = Format(Date.Now, "dd/MM/yyyy")

        CargaCB()
        DiseñoDG1()
        DisenoDG2()
    End Sub

    Sub BuscaAgentes()

        If CBSucursal.SelectedValue Is Nothing Or CBSucursal.SelectedValue = 99 Then
            DvAgentes2.RowFilter = String.Empty
            Me.CBAgente.SelectedValue = 999
            Me.CBAgente.DataSource = DvAgentes2

        Else
            DvAgentes2.RowFilter = String.Empty
            Me.CBAgente.SelectedValue = 999
            DvAgentes2.RowFilter = "GroupCode = " & Trim(Me.CBSucursal.SelectedValue.ToString) & " OR GroupCode = 999"
        End If
    End Sub


    Private Sub CmbSucursal_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBSucursal.SelectionChangeCommitted
        BuscaAgentes()
    End Sub

    Private Sub CmbSucursal_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CBSucursal.Validating
        BuscaAgentes()
    End Sub

    Private Sub CmbSucursal_KeyUp(sender As Object, e As KeyEventArgs) Handles CBSucursal.KeyUp
        BuscaAgentes()
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'BuscaAgentes()
        DataGridView1.DataSource = Nothing
        DataGridView2.DataSource = Nothing

        Dim NumMes As Integer

        NumMes = DateDiff("m", Me.DtpFechaIni.Value, Me.DtpFechaFin.Value)


        If CBSucursal.Text = "TODAS" And CBAgente.Text = "TODOS" Then
            Dim cnn As SqlConnection = Nothing

            Dim cmd4 As SqlCommand = Nothing

            Try

                cnn = New SqlConnection(StrTpm)

                'cnn.Open()

                'Dim mes As Int16
                'mes = DtpFechaIni.Text.Substring(3, 2)

                cmd4 = New SqlCommand("SPRankingLineas", cnn)
                cmd4.CommandType = CommandType.StoredProcedure


                cmd4.Parameters.AddWithValue("@TipoConsulta", 1)
                cmd4.Parameters.AddWithValue("@sucursal", String.Empty)
                cmd4.Parameters.AddWithValue("@agente", String.Empty)
                cmd4.Parameters.AddWithValue("@Linea", String.Empty)
                cmd4.Parameters.AddWithValue("@NumMes", NumMes)
                cmd4.Parameters.AddWithValue("@FechaInicial", DtpFechaIni.Value)
                cmd4.Parameters.AddWithValue("@FechaFinal", DtpFechaFin.Value)
                'cmd4.Parameters.AddWithValue("@MesActual", mes)
                cnn.Open()

                cmd4.ExecuteNonQuery()
                cmd4.Connection.Close()
                Dim da As New SqlDataAdapter
                da.SelectCommand = cmd4
                da.SelectCommand.Connection = cnn

                ''--------------------------------------------
                Dim DsRanking As New DataSet
                da.Fill(DsRanking, "DsRanking")

                'Dim DsClientes As New DataSet
                'da.Fill(DsClientes, "Clientes")

                DsRanking.Tables(0).TableName = "Lineas"
                DsRanking.Tables(1).TableName = "Clientes"

                ''DsVtas.Tables(2).TableName = "VtaCltes"

                DvLineas2.Table = DsRanking.Tables("Lineas")
                'DvAgentes.Table = DsVtas.Tables("VtaAgtes")
                DvClientes.Table = DsRanking.Tables("Clientes")


                DataGridView1.DataSource = DvLineas2

                DataGridView2.DataSource = DvClientes

                DiseñoDG1()
                DisenoDG2()
            Catch ex As Exception
                'DvClientes.RowFilter = "linea='" & DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value.ToString() & "'"
                MsgBox(ex.Message)
                'MsgBox("No existen ventas de este día")
            Finally
                If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                    cnn.Close()
                End If
            End Try

        ElseIf CBSucursal.Text <> "TODAS" And CBAgente.Text = "TODOS" Then
            Dim cnn As SqlConnection = Nothing

            Dim cmd4 As SqlCommand = Nothing

            Try

                cnn = New SqlConnection(StrTpm)

                'cnn.Open()

                'Dim mes As Int16
                'mes = DtpFechaIni.Text.Substring(3, 2)

                cmd4 = New SqlCommand("SPRankingLineas", cnn)
                cmd4.CommandType = CommandType.StoredProcedure


                cmd4.Parameters.AddWithValue("@TipoConsulta", 2)
                cmd4.Parameters.AddWithValue("@sucursal", CBSucursal.SelectedValue)
                cmd4.Parameters.AddWithValue("@agente", String.Empty)
                cmd4.Parameters.AddWithValue("@Linea", String.Empty)
                cmd4.Parameters.AddWithValue("@NumMes", NumMes)
                cmd4.Parameters.AddWithValue("@FechaInicial", DtpFechaIni.Value)
                cmd4.Parameters.AddWithValue("@FechaFinal", DtpFechaFin.Value)
                'cmd4.Parameters.AddWithValue("@MesActual", mes)
                cnn.Open()

                cmd4.ExecuteNonQuery()
                cmd4.Connection.Close()
                Dim da As New SqlDataAdapter
                da.SelectCommand = cmd4
                da.SelectCommand.Connection = cnn

                ''--------------------------------------------
                Dim DsRanking As New DataSet
                da.Fill(DsRanking, "DsRanking")

                'Dim DsClientes As New DataSet
                'da.Fill(DsClientes, "Clientes")

                DsRanking.Tables(0).TableName = "Lineas"
                DsRanking.Tables(1).TableName = "Clientes"

                ''DsVtas.Tables(2).TableName = "VtaCltes"

                DvLineas2.Table = DsRanking.Tables("Lineas")
                'DvAgentes.Table = DsVtas.Tables("VtaAgtes")
                DvClientes.Table = DsRanking.Tables("Clientes")


                DataGridView1.DataSource = DvLineas2

                DataGridView2.DataSource = DvClientes

                DiseñoDG1()
                DisenoDG2()
            Catch ex As Exception
                'DvClientes.RowFilter = "linea='" & DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value.ToString() & "'"
                MsgBox(ex.Message)
                'MsgBox("No existen ventas de este día")
            Finally
                If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                    cnn.Close()
                End If
            End Try

        ElseIf CBAgente.Text <> "TODOS" Then
            Dim cnn As SqlConnection = Nothing

            Dim cmd4 As SqlCommand = Nothing

            Try

                cnn = New SqlConnection(StrTpm)

                'cnn.Open()

                'Dim mes As Int16
                'mes = DtpFechaIni.Text.Substring(3, 2)

                cmd4 = New SqlCommand("SPRankingLineas", cnn)
                cmd4.CommandType = CommandType.StoredProcedure


                cmd4.Parameters.AddWithValue("@TipoConsulta", 3)
                cmd4.Parameters.AddWithValue("@sucursal", String.Empty)
                cmd4.Parameters.AddWithValue("@agente", CBAgente.SelectedValue)
                cmd4.Parameters.AddWithValue("@Linea", String.Empty)
                cmd4.Parameters.AddWithValue("@NumMes", NumMes)
                cmd4.Parameters.AddWithValue("@FechaInicial", DtpFechaIni.Value)
                cmd4.Parameters.AddWithValue("@FechaFinal", DtpFechaFin.Value)
                'cmd4.Parameters.AddWithValue("@MesActual", mes)
                cnn.Open()

                cmd4.ExecuteNonQuery()
                cmd4.Connection.Close()
                Dim da As New SqlDataAdapter
                da.SelectCommand = cmd4
                da.SelectCommand.Connection = cnn

                ''--------------------------------------------
                Dim DsRanking As New DataSet
                da.Fill(DsRanking, "DsRanking")

                'Dim DsClientes As New DataSet
                'da.Fill(DsClientes, "Clientes")

                DsRanking.Tables(0).TableName = "Lineas"
                DsRanking.Tables(1).TableName = "Clientes"

                ''DsVtas.Tables(2).TableName = "VtaCltes"

                DvLineas2.Table = DsRanking.Tables("Lineas")
                'DvAgentes.Table = DsVtas.Tables("VtaAgtes")
                DvClientes.Table = DsRanking.Tables("Clientes")


                DataGridView1.DataSource = DvLineas2

                DataGridView2.DataSource = DvClientes

                DiseñoDG1()
                DisenoDG2()
            Catch ex As Exception
                'DvClientes.RowFilter = "linea='" & DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value.ToString() & "'"
                MsgBox(ex.Message)
                'MsgBox("No existen ventas de este día")
            Finally
                If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                    cnn.Close()
                End If
            End Try

        End If



        DataGridView2.RowHeadersWidth = 55
        DataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
        If DataGridView2 IsNot Nothing Then
            For Each r As DataGridViewRow In DataGridView2.Rows
                r.HeaderCell.Value = (r.Index + 1).ToString()
            Next
        End If

    End Sub

    Private Sub dataGridView1_CurrentCellChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellChanged
        Try
            If DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value = 10000 Then
                DvClientes.RowFilter = "ranking='" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value.ToString() & "'"
            Else
                DvClientes.RowFilter = "linea='" & DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value.ToString() & "'"
            End If



            DataGridView2.RowHeadersWidth = 55
            DataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            If DataGridView2 IsNot Nothing Then
                For Each r As DataGridViewRow In DataGridView2.Rows
                    r.HeaderCell.Value = (r.Index + 1).ToString()
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView2_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.ColumnHeaderMouseClick
        Try

            'DvClientes.RowFilter = "linea='" & DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value.ToString() & "'"

            DataGridView2.RowHeadersWidth = 55
            DataGridView2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            If DataGridView2 IsNot Nothing Then
                For Each r As DataGridViewRow In DataGridView2.Rows
                    r.HeaderCell.Value = (r.Index + 1).ToString()
                Next
            End If


            If DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value = 10000 Then
                DvClientes.RowFilter = "ranking='" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value.ToString() & "'"
            Else
                DvClientes.RowFilter = "linea='" & DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value.ToString() & "'"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView2_CurrentCellChanged(sender As Object, e As EventArgs) Handles DataGridView2.CurrentCellChanged
        Try
            Label6.Text = " - " & DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex).Value & " - " & DataGridView2.Item(2, DataGridView2.CurrentCell.RowIndex).Value
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        Dim Rangos As String = ""
        Dim Rangos2 As String = ""

        'MsgBox("El reporte se creara a continuación")

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)


        'Declaramos el nombre de las columnas
        oSheet.range("A3").value = "Ranking"
        oSheet.range("B3").value = "Linea"
        Dim MesIni As Int16
        MesIni = DtpFechaIni.Text.Substring(3, 2)

        Dim MesFin As Int16
        MesFin = DtpFechaFin.Text.Substring(3, 2)

        MesFin = MesFin - 1
        Dim MesAnterior As String = MonthName(MesIni)
        Dim MesSiguiente As String = MonthName(MesFin)


        Dim MesA As Int16
        MesA = DtpFechaFin.Text.Substring(3, 2)
        Dim MesActual As String = MonthName(MesA)
        'MesActual = DtpFechaFin.Text.Substring(3, 2)

        oSheet.range("C3").value = "Totales " & MesAnterior & "-" & MesSiguiente
        oSheet.range("D3").value = "Promedio ($)" & MesAnterior & "-" & MesSiguiente
        oSheet.range("E3").value = "Promedio (%)"

        oSheet.range("F3").value = "Venta Actual"

        oSheet.range("G3").value = "Pronostico ($) " & MesActual
        oSheet.range("H3").value = "Pronostico (%)"
  

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A3:H3").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = DataGridView1.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel0 As String = Me.DataGridView1.Item(0, fila_dt).Value
            Dim cel1 As String = Me.DataGridView1.Item(1, fila_dt).Value
            Dim cel2 As String = IIf(IsDBNull(Me.DataGridView1.Item(2, fila_dt).Value), 0, Me.DataGridView1.Item(2, fila_dt).Value)
            Dim cel3 As String = IIf(IsDBNull(Me.DataGridView1.Item(3, fila_dt).Value), 0, Me.DataGridView1.Item(3, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DataGridView1.Item(4, fila_dt).Value), 0, Me.DataGridView1.Item(4, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DataGridView1.Item(5, fila_dt).Value), 0, Me.DataGridView1.Item(5, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DataGridView1.Item(6, fila_dt).Value), 0, Me.DataGridView1.Item(6, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DataGridView1.Item(7, fila_dt).Value), 0, Me.DataGridView1.Item(7, fila_dt).Value)


            fila_dt_excel = fila_dt + 4

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel0
            oSheet.range("B" & fila_dt_excel).value = cel1
            oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
            oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
            oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
            oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
            oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 2)
            oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
        Next

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        oSheet.columns("A:H").entirecolumn.autofit()

        'ENCABEZADO DEL REPORTE GENERADO

        'Dim sqlConnection1 As New SqlConnection(conexion_universal.CadenaSQLSAP)
        'Dim cmd As New SqlCommand
        'Dim returnValue As Object

        'cmd.CommandText = "SELECT slpname from oslp where slpcode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString)
        'cmd.CommandType = CommandType.Text
        'cmd.Connection = sqlConnection1

        'sqlConnection1.Open()

        'returnValue = cmd.ExecuteScalar()

        'sqlConnection1.Close()

        'Dim cnn As SqlConnection = Nothing



        oSheet.range("C1").value = Rangos
        oSheet.range("C2").value = Rangos2
        'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        Dim Rangos As String = ""
        Dim Rangos2 As String = ""

        'MsgBox("El reporte se creara a continuación")

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)


        'Declaramos el nombre de las columnas
        oSheet.range("A3").value = "Ranking"
        oSheet.range("B3").value = "ID Articulo"
        oSheet.range("C3").value = "Descripcion"
        oSheet.range("D3").value = ""

        Dim MesIni As Int16
        MesIni = DtpFechaIni.Text.Substring(3, 2)

        Dim MesFin As Int16
        MesFin = DtpFechaFin.Text.Substring(3, 2)

        MesFin = MesFin - 1
        Dim MesAnterior As String = MonthName(MesIni)
        Dim MesSiguiente As String = MonthName(MesFin)


        Dim MesA As Int16
        MesA = DtpFechaFin.Text.Substring(3, 2)
        Dim MesActual As String = MonthName(MesA)
        'MesActual = DtpFechaFin.Text.Substring(3, 2)

        oSheet.range("E3").value = "Totales " & MesAnterior & "-" & MesSiguiente
        oSheet.range("F3").value = "Promedio ($) " & MesAnterior & "-" & MesSiguiente
        oSheet.range("G3").value = "Promedio (%)"

        oSheet.range("H3").value = "Venta Actual"

        oSheet.range("I3").value = "Pronostico ($) " & MesActual
        oSheet.range("J3").value = "Pronostico (%)"


        'para poner la primera fila de los titulos en negrita
        oSheet.range("A3:J3").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = DataGridView2.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel0 As String = Me.DataGridView2.Item(0, fila_dt).Value
            Dim cel1 As String = Me.DataGridView2.Item(1, fila_dt).Value
            Dim cel2 As String = IIf(IsDBNull(Me.DataGridView2.Item(2, fila_dt).Value), 0, Me.DataGridView2.Item(2, fila_dt).Value)
            Dim cel3 As String = IIf(IsDBNull(Me.DataGridView2.Item(3, fila_dt).Value), 0, Me.DataGridView2.Item(3, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DataGridView2.Item(4, fila_dt).Value), 0, Me.DataGridView2.Item(4, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DataGridView2.Item(5, fila_dt).Value), 0, Me.DataGridView2.Item(5, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DataGridView2.Item(6, fila_dt).Value), 0, Me.DataGridView2.Item(6, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DataGridView2.Item(7, fila_dt).Value), 0, Me.DataGridView2.Item(7, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DataGridView2.Item(8, fila_dt).Value), 0, Me.DataGridView2.Item(8, fila_dt).Value)
            Dim cel9 As String = IIf(IsDBNull(Me.DataGridView2.Item(9, fila_dt).Value), 0, Me.DataGridView2.Item(9, fila_dt).Value)

            fila_dt_excel = fila_dt + 4

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel0
            oSheet.range("B" & fila_dt_excel).value = cel1
            oSheet.range("C" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = cel3
            oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
            oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
            oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 2)
            oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
            oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 2)
            oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
        Next

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        oSheet.columns("A:J").entirecolumn.autofit()

        'ENCABEZADO DEL REPORTE GENERADO

        'Dim sqlConnection1 As New SqlConnection(conexion_universal.CadenaSQLSAP)
        'Dim cmd As New SqlCommand
        'Dim returnValue As Object

        'cmd.CommandText = "SELECT slpname from oslp where slpcode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString)
        'cmd.CommandType = CommandType.Text
        'cmd.Connection = sqlConnection1

        'sqlConnection1.Open()

        'returnValue = cmd.ExecuteScalar()

        'sqlConnection1.Close()

        'Dim cnn As SqlConnection = Nothing



        oSheet.range("C1").value = Rangos
        oSheet.range("C2").value = Rangos2
        'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing
    End Sub
End Class
