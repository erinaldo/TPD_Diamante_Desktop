Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class VentaCaida



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


    Sub DiseñoDG1()
        '-------Diseño de DATAGRID LINEAS
        With Me.dataGridView1
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

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try


                .Columns(0).HeaderText = "Linea"
                .Columns(0).Width = 120
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(0).Frozen = True

                Dim mes6 As String = MonthName(DateTime.Now.AddMonths(-6).Month)
                .Columns(1).HeaderText = mes6
                .Columns(1).Width = 95
                .Columns(1).DefaultCellStyle.Format = "$ #,###,##0.##"
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                Dim mes5 As String = MonthName(DateTime.Now.AddMonths(-5).Month)
                .Columns(2).HeaderText = mes5
                .Columns(2).Width = 95
                .Columns(2).DefaultCellStyle.Format = "$ #,###,##0.##"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                Dim mes4 As String = MonthName(DateTime.Now.AddMonths(-4).Month)
                .Columns(3).HeaderText = mes4
                .Columns(3).Width = 95
                .Columns(3).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                Dim mes3 As String = MonthName(DateTime.Now.AddMonths(-3).Month)
                .Columns(4).HeaderText = mes3
                .Columns(4).Width = 95
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).DefaultCellStyle.Format = "$ #,###,##0.#0"

                Dim mes2 As String = MonthName(DateTime.Now.AddMonths(-2).Month)
                .Columns(5).HeaderText = mes2
                .Columns(5).Width = 95
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(5).DefaultCellStyle.Format = "$ #,###,##0.#0"

                Dim mes1 As String = MonthName(DateTime.Now.AddMonths(-1).Month)
                .Columns(6).HeaderText = mes1
                .Columns(6).Width = 95
                .Columns(6).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(7).HeaderText = "Promedio Mayor 3 Meses"
                .Columns(7).Width = 95
                .Columns(7).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(8).HeaderText = "Venta Actual"
                .Columns(8).Width = 95
                .Columns(8).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                Dim mesActual As String = MonthName(DateTime.Now.Month)
                .Columns(9).HeaderText = "Tendencia ($) Mes Actual " & mesActual
                .Columns(9).Width = 95
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(10).HeaderText = "Tendencia (%)"
                .Columns(10).Width = 95
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).DefaultCellStyle.Format = "#0.#0 %"

                .Columns(11).HeaderText = "Trimestre1 " & mes6 & "-" & mes4
                .Columns(11).Width = 95
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(11).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(12).HeaderText = "Trimestre2 " & mes3 & "-" & mes1
                .Columns(12).Width = 95
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(12).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(13).HeaderText = "Crecimiento %"
                .Columns(13).Width = 80
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(13).DefaultCellStyle.Format = "% #0.#0"

            Catch ex As Exception

            End Try


        End With

    End Sub

    Sub DisenoDG2()
        '-------Diseño de DATAGRID CLIENTES
        With Me.DataGridView2
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

            DataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try

                '.Sort(DataGridView2.Columns(0), ListSortDirection.Ascending)
                .Columns(0).HeaderText = "Clave"
                .Columns(0).Width = 50
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(0).Frozen = True

                .Columns(1).HeaderText = "Nombre"
                .Columns(1).Width = 180
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(1).Frozen = True

                Dim mes6 As String = MonthName(DateTime.Now.AddMonths(-6).Month)
                .Columns(2).HeaderText = mes6
                .Columns(2).Width = 95
                .Columns(2).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                Dim mes5 As String = MonthName(DateTime.Now.AddMonths(-5).Month)
                .Columns(3).HeaderText = mes5
                .Columns(3).Width = 95
                .Columns(3).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                Dim mes4 As String = MonthName(DateTime.Now.AddMonths(-4).Month)
                .Columns(4).HeaderText = mes4
                .Columns(4).Width = 95
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).DefaultCellStyle.Format = "$ #,###,##0.#0"

                Dim mes3 As String = MonthName(DateTime.Now.AddMonths(-3).Month)
                .Columns(5).HeaderText = mes3
                .Columns(5).Width = 95
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(5).DefaultCellStyle.Format = "$ #,###,##0.#0"

                Dim mes2 As String = MonthName(DateTime.Now.AddMonths(-2).Month)
                .Columns(6).HeaderText = mes2
                .Columns(6).Width = 95
                .Columns(6).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                Dim mes1 As String = MonthName(DateTime.Now.AddMonths(-1).Month)
                .Columns(7).HeaderText = mes1
                .Columns(7).Width = 95
                .Columns(7).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                '.Columns(7).HeaderCell.SortGlyphDirection = SortOrder.Descending
                '.Sort(dataGridView2.Columns(8), ListSortDirection.Ascending)
                .Columns(8).HeaderText = "Promedio Mayor 3 Meses"
                .Columns(8).Width = 95
                .Columns(8).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(9).HeaderText = "Venta Actual"
                .Columns(9).Width = 80
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).DefaultCellStyle.Format = "$ #,###,##0.#0"


                Dim mesActual As String = MonthName(DateTime.Now.Month)
                .Columns(10).HeaderText = "Tendencia ($) Mes Actual " & mesActual
                .Columns(10).Width = 95
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(11).HeaderText = "Tendencia (%) " & mesActual
                .Columns(11).Width = 80
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(11).DefaultCellStyle.Format = "#0.#0 %"

                .Columns(12).HeaderText = "Trimestre1 " & mes6 & "-" & mes4
                .Columns(12).Width = 85
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(12).DefaultCellStyle.Format = "$ #,###,##0.#0"


                .Columns(13).HeaderText = "Trimestre2 " & mes3 & "-" & mes1
                .Columns(13).Width = 85
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(13).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(14).HeaderText = "Crecimiento"
                .Columns(14).Width = 80
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(14).DefaultCellStyle.Format = "#0.#0 %"

            Catch ex As Exception

            End Try
        End With
    End Sub

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

            Me.CBSucursal.DataSource = DSetTablas.Tables("Sucursales")
            Me.CBSucursal.DisplayMember = "GroupName"
            Me.CBSucursal.ValueMember = "GroupCode"
            Me.CBSucursal.SelectedValue = 99


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

            Me.CBAgente.DataSource = DvAgentes2
            Me.CBAgente.DisplayMember = "slpname"
            Me.CBAgente.ValueMember = "slpcode"
            Me.CBAgente.SelectedValue = 999

            '***************************
            'cargar lineas**************

            ConsutaLista = "select itmsgrpcod,itmsgrpnam from SBO_TPD.dbo.oitb with (nolock) "

            Dim daLinea As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


            daLinea.Fill(DSetTablas, "Lineas")

            Dim filaLinea As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaLinea = DSetTablas.Tables("Lineas").NewRow

            'Agregamos los valores a los campos de la tabla
            filaLinea("ItmsGrpNam") = "TODAS"
            filaLinea("ItmsGrpCod") = 999
            'filaAgte("GroupCode") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Lineas").Rows.Add(filaLinea)

            DvLineas.Table = DSetTablas.Tables("Lineas")

            Me.CBLinea.DataSource = DvLineas
            Me.CBLinea.DisplayMember = "ItmsGrpNam"
            Me.CBLinea.ValueMember = "ItmsGrpCod"
            Me.CBLinea.SelectedValue = 999


        End Using

    End Sub


    '''*************************LOAD
    Private Sub VentaCaida_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        CargaCB()
        DiseñoDG1()
        DisenoDG2()

        Linea.Text = " - Linea"
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


    'BOTON CONSULTAR
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Buscar_NotasC()
        DiseñoDG1()
        DisenoDG2()

    End Sub

    Sub Buscar_NotasC()
        'Dim vDiasMes As Integer
        Dim cnn As SqlConnection = Nothing

        Dim cmd4 As SqlCommand = Nothing

        Try
            cnn = New SqlConnection(StrTpm)

            cnn.Open()

            'cmd4 = New SqlCommand("PJ_VentaCaida", cnn)
            cmd4 = New SqlCommand("TPD_VentaCaida2", cnn)

            cmd4.CommandType = CommandType.StoredProcedure


            ' MUESTRA VENTAS POR LINEA DE TODAS LAS SUCURSALES, AGENTES Y LINEAS
            If CBSucursal.Text = "TODAS" And CBAgente.Text = "TODOS" And CBLinea.Text = "TODAS" Then

                cmd4.Parameters.AddWithValue("@TipoConsulta", 1)
                cmd4.Parameters.AddWithValue("@Sucursal", 0)
                cmd4.Parameters.AddWithValue("@Vendedor", 0)
                cmd4.Parameters.AddWithValue("@Linea", 0)

                ' MUESTRA VENTAS POR LINEA DE LA SUCURSAL SELECCIONADA, TODOS LOS AGENTES Y TODAS LAS LINEAS
            ElseIf CBSucursal.Text <> "TODAS" And CBAgente.Text = "TODOS" And CBLinea.Text = "TODAS" Then
                cmd4.Parameters.AddWithValue("@TipoConsulta", 2)
                cmd4.Parameters.AddWithValue("@Sucursal", CBSucursal.SelectedValue)
                cmd4.Parameters.AddWithValue("@Vendedor", 0)
                cmd4.Parameters.AddWithValue("@Linea", 0)

                'MUESTRA VENTAS POR LINEA DEL AGENTE SELECCIONADO
            ElseIf CBAgente.Text <> "TODOS" And CBLinea.Text = "TODAS" Then
                cmd4.Parameters.AddWithValue("@TipoConsulta", 3)
                cmd4.Parameters.AddWithValue("@Sucursal", 0)
                cmd4.Parameters.AddWithValue("@Vendedor", CBAgente.SelectedValue)
                cmd4.Parameters.AddWithValue("@Linea", 0)

            End If
            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn

            ''--------------------------------------------
            Dim DsLineas As New DataSet
            da.Fill(DsLineas, "DsVtas")

            DsLineas.Tables(0).TableName = "Lineas"
            'DsVtas.Tables(1).TableName = "VtaAgtes"
            'DsVtas.Tables(2).TableName = "VtaCltes"

            DvLineas2.Table = DsLineas.Tables("Lineas")
            'DvAgentes.Table = DsVtas.Tables("VtaAgtes")
            'DvClientes.Table = DsVtas.Tables("VtaCltes")


            dataGridView1.DataSource = DvLineas2

            'dataGridView2.DataSource = DvAgentes

        Catch ex As Exception
            MsgBox(ex.Message)
            'MsgBox("No existen ventas de este día")
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

    End Sub

    Private Sub dataGridView1_CurrentCellChanged(sender As Object, e As EventArgs) Handles dataGridView1.CurrentCellChanged
        DisenoDG2()

        Dim cnn As SqlConnection = Nothing

        Dim cmd4 As SqlCommand = Nothing

        Try
            cnn = New SqlConnection(StrTpm)

            cnn.Open()

            cmd4 = New SqlCommand("PJ_VentaCaida", cnn)
            cmd4.CommandType = CommandType.StoredProcedure


            ' MUESTRA VENTAS POR LINEA DE TODAS LAS SUCURSALES, AGENTES Y LINEAS
            If CBSucursal.Text = "TODAS" And CBAgente.Text = "TODOS" And CBLinea.Text = "TODAS" Then

                cmd4.Parameters.AddWithValue("@TipoConsulta", 6)
                cmd4.Parameters.AddWithValue("@Sucursal", String.Empty)
                cmd4.Parameters.AddWithValue("@Vendedor", String.Empty)
                cmd4.Parameters.AddWithValue("@Linea", dataGridView1.Item(0, dataGridView1.CurrentCell.RowIndex).Value.ToString)

                ' MUESTRA VENTAS POR LINEA DE LA SUCURSAL SELECCIONADA, TODOS LOS AGENTES Y TODAS LAS LINEAS
            ElseIf CBSucursal.Text <> "TODAS" And CBAgente.Text = "TODOS" Then
                cmd4.Parameters.AddWithValue("@TipoConsulta", 7)
                cmd4.Parameters.AddWithValue("@Sucursal", CBSucursal.SelectedValue)
                cmd4.Parameters.AddWithValue("@Vendedor", String.Empty)
                cmd4.Parameters.AddWithValue("@Linea", dataGridView1.Item(0, dataGridView1.CurrentCell.RowIndex).Value.ToString)

                'MUESTRA VENTAS POR LINEA DEL AGENTE SELECCIONADO
            ElseIf CBAgente.Text <> "TODOS" And CBLinea.Text = "TODAS" Then
                cmd4.Parameters.AddWithValue("@TipoConsulta", 8)
                cmd4.Parameters.AddWithValue("@Sucursal", 0)
                cmd4.Parameters.AddWithValue("@Vendedor", CBAgente.SelectedValue)
                cmd4.Parameters.AddWithValue("@Linea", dataGridView1.Item(0, dataGridView1.CurrentCell.RowIndex).Value.ToString)

            End If
            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn

            ''--------------------------------------------
            Dim DsClientes As New DataSet
            da.Fill(DsClientes, "Clientes")

            DsClientes.Tables(0).TableName = "Clientes"
            'DsVtas.Tables(1).TableName = "VtaAgtes"
            'DsVtas.Tables(2).TableName = "VtaCltes"

            DvClientes.Table = DsClientes.Tables("Clientes")
            'DvAgentes.Table = DsVtas.Tables("VtaAgtes")
            'DvClientes.Table = DsVtas.Tables("VtaCltes")


            DataGridView2.DataSource = DvClientes

            'dataGridView2.DataSource = DvAgentes


        Catch ex As Exception
            'MsgBox(ex.Message)
            'MsgBox("No existen ventas de este día")
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

    End Sub

    Private Sub dataGridView2_CurrentCellChanged(sender As Object, e As EventArgs) Handles DataGridView2.CurrentCellChanged
        Try
            Label3.Text = " - " & dataGridView1.Item(0, dataGridView1.CurrentCell.RowIndex).Value.ToString & " - " & DataGridView2.Item(1, DataGridView2.CurrentCell.RowIndex).Value.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataGridView1.CellClick

        DisenoDG2()

        Dim cnn As SqlConnection = Nothing

        Dim cmd4 As SqlCommand = Nothing

        Try
            cnn = New SqlConnection(StrTpm)

            cnn.Open()

            cmd4 = New SqlCommand("PJ_VentaCaida", cnn)
            cmd4.CommandType = CommandType.StoredProcedure



            ' MUESTRA VENTAS POR LINEA DE TODAS LAS SUCURSALES, AGENTES Y LINEAS
            If CBSucursal.Text = "TODAS" And CBAgente.Text = "TODOS" And CBLinea.Text = "TODAS" Then

                cmd4.Parameters.AddWithValue("@TipoConsulta", 6)
                cmd4.Parameters.AddWithValue("@Sucursal", String.Empty)
                cmd4.Parameters.AddWithValue("@Vendedor", String.Empty)
                cmd4.Parameters.AddWithValue("@Linea", dataGridView1.Item(0, dataGridView1.CurrentCell.RowIndex).Value.ToString)

                ' MUESTRA VENTAS POR LINEA DE LA SUCURSAL SELECCIONADA, TODOS LOS AGENTES Y TODAS LAS LINEAS
            ElseIf CBSucursal.Text <> "TODAS" And CBAgente.Text = "TODOS" And CBLinea.Text = "TODAS" Then
                cmd4.Parameters.AddWithValue("@TipoConsulta", 7)
                cmd4.Parameters.AddWithValue("@Sucursal", CBSucursal.SelectedValue)
                cmd4.Parameters.AddWithValue("@Vendedor", String.Empty)
                cmd4.Parameters.AddWithValue("@Linea", dataGridView1.Item(0, dataGridView1.CurrentCell.RowIndex).Value.ToString)

                'MUESTRA VENTAS POR LINEA DEL AGENTE SELECCIONADO
            ElseIf CBAgente.Text <> "TODOS" And CBLinea.Text = "TODAS" Then
                cmd4.Parameters.AddWithValue("@TipoConsulta", 8)
                cmd4.Parameters.AddWithValue("@Sucursal", 0)
                cmd4.Parameters.AddWithValue("@Vendedor", CBAgente.SelectedValue)
                cmd4.Parameters.AddWithValue("@Linea", dataGridView1.Item(0, dataGridView1.CurrentCell.RowIndex).Value.ToString)

            End If
            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da2 As New SqlDataAdapter
            da2.SelectCommand = cmd4
            da2.SelectCommand.Connection = cnn

            ''--------------------------------------------
            Dim DsClientes As New DataSet
            da2.Fill(DsClientes, "DsClientes")

            DsClientes.Tables(0).TableName = "DsClientes"
            'DsVtas.Tables(1).TableName = "VtaAgtes"
            'DsVtas.Tables(2).TableName = "VtaCltes"

            DvClientes.Table = DsClientes.Tables("DsClientes")
            'DvAgentes.Table = DsVtas.Tables("VtaAgtes")
            'DvClientes.Table = DsVtas.Tables("VtaCltes")


            DataGridView2.DataSource = DvClientes

            'dataGridView2.DataSource = DvAgentes

            Label3.Text = " - " & dataGridView1.Item(0, dataGridView1.CurrentCell.RowIndex).Value.ToString

        Catch ex As Exception
            'MsgBox(ex.Message)
            'MsgBox("No existen ventas de este día")
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

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
        oSheet.range("A3").value = "Id Cliente"

        oSheet.range("B3").value = "Nombre"

        Dim mes6 As String = MonthName(DateTime.Now.AddMonths(-6).Month)
        oSheet.range("C3").value = mes6

        Dim mes5 As String = MonthName(DateTime.Now.AddMonths(-5).Month)
        oSheet.range("D3").value = mes5

        Dim mes4 As String = MonthName(DateTime.Now.AddMonths(-4).Month)
        oSheet.range("E3").value = mes4

        Dim mes3 As String = MonthName(DateTime.Now.AddMonths(-3).Month)
        oSheet.range("F3").value = mes3

        Dim mes2 As String = MonthName(DateTime.Now.AddMonths(-2).Month)
        oSheet.range("G3").value = mes2

        Dim mes1 As String = MonthName(DateTime.Now.AddMonths(-1).Month)
        oSheet.range("H3").value = mes1


        oSheet.range("I3").value = "Promedio mayor 3 meses"

        Dim mesActual As String = MonthName(DateTime.Now.Month)
        oSheet.range("J3").value = "Tendencia " & mesActual

        oSheet.range("K3").value = "Tendencia (%) "
        oSheet.range("L3").value = "Trimestre1 " & mes6 & "-" & mes4
        oSheet.range("M3").value = "Trimestre2 " & mes3 & "-" & mes3
        oSheet.range("N3").value = "Crecimiento % "


        'para poner la primera fila de los titulos en negrita
        oSheet.range("A3:N3").font.bold = True
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
            Dim cel10 As String = IIf(IsDBNull(Me.DataGridView2.Item(10, fila_dt).Value), 0, Me.DataGridView2.Item(10, fila_dt).Value)
            Dim cel11 As String = IIf(IsDBNull(Me.DataGridView2.Item(11, fila_dt).Value), 0, Me.DataGridView2.Item(11, fila_dt).Value)
            Dim cel12 As String = IIf(IsDBNull(Me.DataGridView2.Item(12, fila_dt).Value), 0, Me.DataGridView2.Item(12, fila_dt).Value)
            Dim cel13 As String = IIf(IsDBNull(Me.DataGridView2.Item(13, fila_dt).Value), 0, Me.DataGridView2.Item(13, fila_dt).Value)


            fila_dt_excel = fila_dt + 4

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel0
            oSheet.range("B" & fila_dt_excel).value = cel1
            oSheet.range("C" & fila_dt_excel).value = cel2
            oSheet.range("D" & fila_dt_excel).value = cel3
            oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
            oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
            oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 2)
            oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
            oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 2)
            oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
            oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 2)
            oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 2)
            oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel12, 2)
            oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel13, 2)

        Next

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        oSheet.columns("A:N").entirecolumn.autofit()

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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button1.Click
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
        oSheet.range("A3").value = "Linea"

        Dim mes6 As String = MonthName(DateTime.Now.AddMonths(-6).Month)
        oSheet.range("B3").value = mes6

        Dim mes5 As String = MonthName(DateTime.Now.AddMonths(-5).Month)
        oSheet.range("C3").value = mes5

        Dim mes4 As String = MonthName(DateTime.Now.AddMonths(-4).Month)
        oSheet.range("D3").value = mes4

        Dim mes3 As String = MonthName(DateTime.Now.AddMonths(-3).Month)
        oSheet.range("E3").value = mes3

        Dim mes2 As String = MonthName(DateTime.Now.AddMonths(-2).Month)
        oSheet.range("F3").value = mes2

        Dim mes1 As String = MonthName(DateTime.Now.AddMonths(-1).Month)
        oSheet.range("G3").value = mes1


        oSheet.range("H3").value = "Promedio mayor 3 meses"

        Dim mesActual As String = MonthName(DateTime.Now.Month)
        oSheet.range("I3").value = "Tendencia " & mesActual

        oSheet.range("J3").value = "Tendencia (%) "
        oSheet.range("K3").value = "Trimestre1 " & mes6 & "-" & mes4
        oSheet.range("L3").value = "Trimestre2 " & mes3 & "-" & mes3
        oSheet.range("M3").value = "Crecimiento % "


        'para poner la primera fila de los titulos en negrita
        oSheet.range("A3:M3").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = dataGridView1.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel0 As String = Me.dataGridView1.Item(0, fila_dt).Value
            Dim cel1 As String = Me.dataGridView1.Item(1, fila_dt).Value
            Dim cel2 As String = IIf(IsDBNull(Me.dataGridView1.Item(2, fila_dt).Value), 0, Me.dataGridView1.Item(2, fila_dt).Value)
            Dim cel3 As String = IIf(IsDBNull(Me.dataGridView1.Item(3, fila_dt).Value), 0, Me.dataGridView1.Item(3, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.dataGridView1.Item(4, fila_dt).Value), 0, Me.dataGridView1.Item(4, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.dataGridView1.Item(5, fila_dt).Value), 0, Me.dataGridView1.Item(5, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.dataGridView1.Item(6, fila_dt).Value), 0, Me.dataGridView1.Item(6, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.dataGridView1.Item(7, fila_dt).Value), 0, Me.dataGridView1.Item(7, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.dataGridView1.Item(8, fila_dt).Value), 0, Me.dataGridView1.Item(8, fila_dt).Value)
            Dim cel9 As String = IIf(IsDBNull(Me.dataGridView1.Item(9, fila_dt).Value), 0, Me.dataGridView1.Item(9, fila_dt).Value)
            Dim cel10 As String = IIf(IsDBNull(Me.dataGridView1.Item(10, fila_dt).Value), 0, Me.dataGridView1.Item(10, fila_dt).Value)
            Dim cel11 As String = IIf(IsDBNull(Me.dataGridView1.Item(11, fila_dt).Value), 0, Me.dataGridView1.Item(11, fila_dt).Value)
            Dim cel12 As String = IIf(IsDBNull(Me.dataGridView1.Item(12, fila_dt).Value), 0, Me.dataGridView1.Item(12, fila_dt).Value)


            fila_dt_excel = fila_dt + 4

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel0
            oSheet.range("B" & fila_dt_excel).value = cel1
            oSheet.range("C" & fila_dt_excel).value = cel2
            oSheet.range("D" & fila_dt_excel).value = cel3
            oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
            oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
            oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 2)
            oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
            oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 2)
            oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
            oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 2)
            oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 2)
            oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel12, 2)

        Next

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        oSheet.columns("A:M").entirecolumn.autofit()

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