
Imports System.Data
Imports System.Data.SqlClient


Public Class ScoreCardTPD


    'Conexiones a la Base de datos
    Public StrProd As String = "Data Source=SERVIDORSAP;Initial Catalog=SBO-Diamante-productiva;User Id=SA;Password=SD1amany3S;"
    Public StrTpm As String = "Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;"
    Public StrCon As String = "Data Source=SERVIDORSAP;Initial Catalog=SBO_TPD;User Id=SA;Password=SD1amany3S;"

    Dim DvTotales As New DataView
    Dim DvAgentes As New DataView
    Dim DvAgentes2 As New DataView
    Dim DvClientes As New DataView


    Private Sub VtasScoreCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")

        'Variable para guardar la consulta de AGENTES y SUCURSALES en los combobox
        Dim ConsutaLista As String


        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)


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

            Me.CmbSucursal.DataSource = DSetTablas.Tables("Sucursales")
            Me.CmbSucursal.DisplayMember = "GroupName"
            Me.CmbSucursal.ValueMember = "GroupCode"
            Me.CmbSucursal.SelectedValue = 99


            '---------------------------------------------------------

            ConsutaLista = "SELECT T0.slpcode,T0.slpname,T1.GroupCode FROM OSLP T0 "
            ConsutaLista &= "INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
            ConsutaLista &= "WHERE T1.CbrGralAdicional = 'N'  ORDER BY slpcode, slpname "


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

            Me.CmbAgteVta.DataSource = DvAgentes2
            Me.CmbAgteVta.DisplayMember = "slpname"
            Me.CmbAgteVta.ValueMember = "slpcode"
            Me.CmbAgteVta.SelectedValue = 999

            ' -----------------------------------------------------

        End Using

    End Sub

    Sub BuscaAgentes()

        If CmbSucursal.SelectedValue Is Nothing Or CmbSucursal.SelectedValue = 99 Then
            DvAgentes2.RowFilter = String.Empty
            Me.CmbAgteVta.SelectedValue = 999
            Me.CmbAgteVta.DataSource = DvAgentes2

        Else
            DvAgentes2.RowFilter = String.Empty
            Me.CmbAgteVta.SelectedValue = 999
            DvAgentes2.RowFilter = "GroupCode = " & Trim(Me.CmbSucursal.SelectedValue.ToString) & " OR GroupCode = 999"
        End If
    End Sub

    Private Sub DgVtaAgte_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgVtaAgte.CurrentCellChanged
   
        Try
            DvClientes.RowFilter = "slpcode =" & DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value.ToString
        Catch ex As Exception
        End Try

    End Sub


    Private Sub CmbSucursal_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbSucursal.SelectionChangeCommitted
        BuscaAgentes()
    End Sub

    Private Sub CmbSucursal_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbSucursal.Validating
        BuscaAgentes()
    End Sub

    Private Sub CmbSucursal_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbSucursal.KeyUp
        BuscaAgentes()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DgVtaAgte.DataSource = Nothing
        Buscar_NotasC()
    End Sub

    Sub Buscar_NotasC()
        Dim vDiasMes As Integer
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Dim cmd2 As SqlCommand = Nothing
        Dim vDiasTrans As Integer

        Dim cmd3 As SqlCommand = Nothing
        Dim cmd4 As SqlCommand = Nothing

        Try
            cnn = New SqlConnection(StrTpm)
            cmd = New SqlCommand("Indicadores", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@FechaFinal", SqlDbType.SmallDateTime).Value = Me.DtpFechaIni.Value
            cmd.Parameters.Add("@TipoConsulta", SqlDbType.Int).Value = 1

            cnn.Open()

            vDiasMes = CInt(cmd.ExecuteScalar())
            txtDiasMes.Text = vDiasMes.ToString

            cmd2 = New SqlCommand("Indicadores", cnn)
            cmd2.CommandType = CommandType.StoredProcedure
            cmd2.Parameters.Add("@FechaFinal", SqlDbType.SmallDateTime).Value = Me.DtpFechaIni.Value
            cmd2.Parameters.Add("@TipoConsulta", SqlDbType.Int).Value = 2

            vDiasTrans = CInt(cmd2.ExecuteScalar())
            txtDiasTranscurridos.Text = vDiasTrans.ToString


            cmd4 = New SqlCommand("SPsc2", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = Me.DtpFechaIni.Value
            cmd4.Parameters.Add("@DiasMes", SqlDbType.Int).Value = vDiasMes
            cmd4.Parameters.Add("@DiasTrans", SqlDbType.Int).Value = vDiasTrans

            Dim DiasRestantes As Integer
            DiasRestantes = vDiasMes - vDiasTrans
            If DiasRestantes = 0 Then
                DiasRestantes = 1
            End If

            'cmd4.Parameters.Add("@DiasRest", SqlDbType.Int).Value = vDiasMes - vDiasTrans
            cmd4.Parameters.Add("@DiasRest", SqlDbType.Int).Value = DiasRestantes
            cmd4.Parameters.Add("@PorAvanOptimo", SqlDbType.Decimal).Value = vDiasTrans / vDiasMes
            cmd4.Parameters.Add("@Sucursal", SqlDbType.Int).Value = Me.CmbSucursal.SelectedValue
            cmd4.Parameters.Add("@Agente", SqlDbType.VarChar, 30).Value = Me.CmbAgteVta.SelectedValue

            Dim mes As Int16
            mes = DtpFechaIni.Text.Substring(3, 2)
            'MsgBox(mes)
            Dim anio As Int16
            anio = DtpFechaIni.Text.Substring(6, 4)
            'MsgBox(anio)

            cmd4.Parameters.Add("@MesActual", SqlDbType.Int).Value = mes
            cmd4.Parameters.Add("@AñoActual", SqlDbType.Int).Value = anio


            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn


            ''--------------------------------------------
            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "Totales"
            DsVtas.Tables(1).TableName = "VtaAgtes"
            DsVtas.Tables(2).TableName = "VtaCltes"

            DvTotales.Table = DsVtas.Tables("Totales")
            DvAgentes.Table = DsVtas.Tables("VtaAgtes")
            DvClientes.Table = DsVtas.Tables("VtaCltes")


            DgTotales.DataSource = DvTotales

            DgVtaAgte.DataSource = DvAgentes

            DgClientes.DataSource = DvClientes

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

        txtDiasRestantes.Text = Convert.ToString(vDiasMes - vDiasTrans)
        txtAvanceOptimo.Text = Format(Convert.ToString((vDiasTrans / vDiasMes) * 100), "000.00")

        txtAvanceOptimo.Text = (vDiasTrans / vDiasMes).ToString("P1")


        '-------Diseño de DATAGRID Totales
        With Me.DgTotales
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

            DgTotales.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Dim numfilas As Integer
            Try

                'Catch ex As Exception

                'End Try


                .Columns(0).HeaderText = "SlpCode"
                .Columns(0).Width = 50
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "SlpName"
                .Columns(1).Width = 90

                .Columns(2).HeaderText = "VtaDia"
                .Columns(2).Width = 80
                .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Acumulado"
                .Columns(3).Width = 85
                .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).HeaderText = "Objetivo"
                .Columns(4).Width = 85
                .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Acum vs Obj"
                .Columns(5).Width = 105
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Format = " $ ###,###,##0.#0"

                .Columns(6).HeaderText = "PorAcumVsObj"
                .Columns(6).Width = 90
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Format = "% ##0.#0"
            
            numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid

            'recorre las filas del DataGrid
                For i = 0 To (numfilas - 2)

                    If DgTotales.Item(6, i).Value Is DBNull.Value Then
                        DgTotales.Item(6, i).Value = 0
                    End If


                    If DgTotales.Item(6, i).Value < 0.85 Then
                        DgTotales.Rows(i).Cells(6).Style.BackColor = Color.Red

                    ElseIf DgTotales.Item(6, i).Value >= 0.85 And DgVtaAgte.Item(6, i).Value < 1 Then
                        DgTotales.Rows(i).Cells(6).Style.BackColor = Color.Yellow

                    ElseIf DgTotales.Item(6, i).Value >= 1 Then
                        DgTotales.Rows(i).Cells(6).Style.BackColor = Color.LimeGreen

                    End If

                Next

            .Columns(7).HeaderText = "Vta Requerida"
            .Columns(7).Width = 100
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(8).HeaderText = "PronFinMes"
                .Columns(8).Width = 90
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(9).HeaderText = "PorPronFinMes"
                .Columns(9).Width = 100
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).DefaultCellStyle.Format = "% #,##0.#0"

                numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid

                'recorre las filas del DataGrid
                For i = 0 To (numfilas - 2)


                    If DgTotales.Item(9, i).Value Is DBNull.Value Then
                        DgTotales.Item(9, i).Value = 0
                    End If


                    If DgTotales.Item(9, i).Value < 0.85 Then
                        DgTotales.Rows(i).Cells(9).Style.BackColor = Color.Red

                    ElseIf DgTotales.Item(9, i).Value >= 0.85 And DgVtaAgte.Item(9, i).Value < 1 Then
                        DgTotales.Rows(i).Cells(9).Style.BackColor = Color.Yellow

                    ElseIf DgTotales.Item(9, i).Value >= 1 Then
                        DgTotales.Rows(i).Cells(9).Style.BackColor = Color.LimeGreen

                    End If

                Next

                .Columns(10).HeaderText = "ReqDiaPObjetivo"
                .Columns(10).Width = 100
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(10).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(11).HeaderText = "GroupCode"
                .Columns(11).Width = 75
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(12).HeaderText = "GroupName"
                .Columns(12).Width = 75
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(13).HeaderText = "Ord Total"
                .Columns(13).Width = 80
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(13).DefaultCellStyle.Format = "###,###,##0.#0"

            Catch ex As Exception

            End Try


        End With

        
        '-------Diseño de DATAGRID Agentes
        With Me.DgVtaAgte
            Try

           
            '.DataSource = DtAgte
            .ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

            DgVtaAgte.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'Color de linea del grid

            .Columns(0).HeaderText = "SlpCode"
            .Columns(0).Width = 50
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(1).HeaderText = "SlpName"
            .Columns(1).Width = 150

            .Columns(2).HeaderText = "VtaDia"
            .Columns(2).Width = 95
                .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(3).HeaderText = "Acumulado"
            .Columns(3).Width = 95
                .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(4).HeaderText = "$ Objetivo"
            .Columns(4).Width = 95
                .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Acum vs Obj ($)"
            .Columns(5).Width = 105
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(6).HeaderText = "PorAcumVsObj (%)"
            .Columns(6).Width = 100
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Format = "% ##0.#0"

            Dim numfilas As Integer

            numfilas = DgVtaAgte.RowCount 'cuenta las filas del DataGrid

            'recorre las filas del DataGrid
            For i = 0 To (numfilas - 1)

                

                    If DgVtaAgte.Item(6, i).Value < 0.85 Then
                        DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.Red

                    ElseIf DgVtaAgte.Item(6, i).Value >= 0.85 And DgVtaAgte.Item(6, i).Value < 1 Then
                        DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.Yellow

                    ElseIf DgVtaAgte.Item(6, i).Value >= 1 Then
                        DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.LimeGreen

                    End If

            Next


            .Columns(7).HeaderText = "Vta Requerida"
            .Columns(7).Width = 100
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(8).HeaderText = "PronFinMes ($)"
            .Columns(8).Width = 100
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(9).HeaderText = "PorPronFinMes (%)"
            .Columns(9).Width = 100
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).DefaultCellStyle.Format = "% #,###.#0.#0"

            'Dim numfilas As Integer

            numfilas = DgVtaAgte.RowCount 'cuenta las filas del DataGrid

            'recorre las filas del DataGrid
            For i = 0 To (numfilas - 1)

                    If DgVtaAgte.Item(9, i).Value < 0.85 Then
                        DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.Red

                    ElseIf DgVtaAgte.Item(9, i).Value >= 0.85 And DgVtaAgte.Item(9, i).Value < 1 Then
                        DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.Yellow

                    ElseIf DgVtaAgte.Item(9, i).Value >= 1 Then
                        DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.LimeGreen

                    End If

            Next


            .Columns(10).HeaderText = "ReqDiaPObjetivo"
            .Columns(10).Width = 105
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(10).DefaultCellStyle.Format = " $ ###,###,##0.#0"

            .Columns(11).HeaderText = "GroupCode"
            .Columns(11).Width = 80
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(12).HeaderText = "GroupName"
            .Columns(12).Width = 105
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(13).HeaderText = "Ord Total"
            .Columns(13).Width = 80
            .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(13).DefaultCellStyle.Format = "###,###,##0.#0"

            Catch ex As Exception

            End Try

        End With


        '-------Diseño de DATAGRID Clientes
        With Me.DgClientes

            Try

                '.DataSource = DtAgte
                .ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                'Color de linea del grid

                .Columns(0).HeaderText = "CarCode"
                .Columns(0).Width = 70
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "CardName"
                .Columns(1).Width = 193

                .Columns(2).HeaderText = "VtaDia"
                .Columns(2).Width = 100
                .Columns(2).DefaultCellStyle.Format = "$ ###,###,###.00"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(3).HeaderText = "Acumulado"
                .Columns(3).Width = 110
                .Columns(3).DefaultCellStyle.Format = "$ ###,###,###.00"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                '----COLUMNA 8
                .Columns(4).HeaderText = "PronFinMes"
                .Columns(4).Width = 120
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).DefaultCellStyle.Format = "$ ###,###,###.00"


                '----COLUMNA 11
                .Columns(5).HeaderText = "SlpCode"
                .Columns(5).Width = 70
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '----COLUMNA 12
                .Columns(6).HeaderText = "SlpName"
                .Columns(6).Width = 170
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '----COLUMNA 13
                .Columns(7).HeaderText = "GroupCode"
                .Columns(7).Width = 70
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '----COLUMNA 14
                .Columns(8).HeaderText = "GroupName"
                .Columns(8).Width = 140
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Catch ex As Exception

            End Try
    End With
    End Sub


    '---Generar reporte en EXCEL de TOTALES
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
        oSheet.range("A3").value = "SlpCode"
        oSheet.range("B3").value = "SlpName"
        oSheet.range("C3").value = "VtaDia"
        oSheet.range("D3").value = "Acumulado"
        oSheet.range("E3").value = "Objetivo"
        oSheet.range("F3").value = "Acum vs Obj"
        oSheet.range("G3").value = "PorAcum vs Obj"
        oSheet.range("H3").value = "Vta Requerida"
        oSheet.range("I3").value = "Pron Fin Mes"
        oSheet.range("J3").value = "Por Pron Fin Mes"
        oSheet.range("K3").value = "Req Dia P Obj"
        oSheet.range("L3").value = "GroupCode"
        oSheet.range("M3").value = "GroupName"
        oSheet.range("N3").value = "Ord Total"

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A3:N3").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = DgTotales.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel0 As String = Me.DgTotales.Item(0, fila_dt).Value
            Dim cel1 As String = Me.DgTotales.Item(1, fila_dt).Value
            Dim cel2 As String = IIf(IsDBNull(Me.DgTotales.Item(2, fila_dt).Value), 0, Me.DgTotales.Item(2, fila_dt).Value)
            Dim cel3 As String = IIf(IsDBNull(Me.DgTotales.Item(3, fila_dt).Value), 0, Me.DgTotales.Item(3, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DgTotales.Item(4, fila_dt).Value), 0, Me.DgTotales.Item(4, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DgTotales.Item(5, fila_dt).Value), 0, Me.DgTotales.Item(5, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DgTotales.Item(6, fila_dt).Value), 0, Me.DgTotales.Item(6, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DgTotales.Item(7, fila_dt).Value), 0, Me.DgTotales.Item(7, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DgTotales.Item(8, fila_dt).Value), 0, Me.DgTotales.Item(8, fila_dt).Value)
            Dim cel9 As String = IIf(IsDBNull(Me.DgTotales.Item(9, fila_dt).Value), 0, Me.DgTotales.Item(9, fila_dt).Value)
            Dim cel10 As String = IIf(IsDBNull(Me.DgTotales.Item(10, fila_dt).Value), 0, Me.DgTotales.Item(10, fila_dt).Value)
            Dim cel11 As String = IIf(IsDBNull(Me.DgTotales.Item(11, fila_dt).Value), 0, Me.DgTotales.Item(11, fila_dt).Value)
            Dim cel12 As String = Me.DgTotales.Item(12, fila_dt).Value
            Dim cel13 As String = IIf(IsDBNull(Me.DgTotales.Item(13, fila_dt).Value), 0, Me.DgTotales.Item(13, fila_dt).Value)


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
            oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 2)
            oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
            oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 2)
            oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 2)
            oSheet.range("M" & fila_dt_excel).value = cel12
            oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel13, 2)
        Next

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        oSheet.columns("A:N").entirecolumn.autofit()

        'ENCABEZADO DEL REPORTE GENERADO

        Dim sqlConnection1 As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=SBO_TPD;User Id=SA;Password=SD1amany3S;")
        Dim cmd As New SqlCommand
        Dim returnValue As Object

        cmd.CommandText = "SELECT slpname from oslp where slpcode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString)
        cmd.CommandType = CommandType.Text
        cmd.Connection = sqlConnection1

        sqlConnection1.Open()

        returnValue = cmd.ExecuteScalar()

        sqlConnection1.Close()

        Dim cnn As SqlConnection = Nothing


        If CmbAgteVta.SelectedValue = 999 Then
            oSheet.range("A1").value = "Reporte de Ventas TOTALES de todos los AGENTES con FECHA " + Format(Me.DtpFechaIni.Value)
        Else
            oSheet.range("A1").value = "Reporte de Ventas del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
        End If


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


    '--------Generar Excel Agentes
    Private Sub BtnAgentes_Click(sender As Object, e As EventArgs) Handles BtnAgentes.Click
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        Dim Rangos As String = ""
        Dim Rangos2 As String = ""

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)


        'Declaramos el nombre de las columnas
        oSheet.range("A3").value = "SlpCode"
        oSheet.range("B3").value = "SlpName"
        oSheet.range("C3").value = "VtaDia"
        oSheet.range("D3").value = "Acumulado"
        oSheet.range("E3").value = "Objetivo"
        oSheet.range("F3").value = "Acum vs Obj"
        oSheet.range("G3").value = "PorAcum vs Obj"
        oSheet.range("H3").value = "Vta Requerida"
        oSheet.range("I3").value = "Pron Fin Mes"

        oSheet.range("J3").value = "Por Pron Fin Mes"
        oSheet.range("K3").value = "Req Dia P Obj"
        oSheet.range("L3").value = "GroupCode"
        oSheet.range("M3").value = "GroupName"
        oSheet.range("N3").value = "Ord Total"

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A3:N3").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = DgVtaAgte.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel0 As String = Me.DgVtaAgte.Item(0, fila_dt).Value
            Dim cel1 As String = Me.DgVtaAgte.Item(1, fila_dt).Value
            Dim cel2 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(2, fila_dt).Value), 0, Me.DgVtaAgte.Item(2, fila_dt).Value)
            Dim cel3 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(3, fila_dt).Value), 0, Me.DgVtaAgte.Item(3, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(4, fila_dt).Value), 0, Me.DgVtaAgte.Item(4, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(5, fila_dt).Value), 0, Me.DgVtaAgte.Item(5, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(6, fila_dt).Value), 0, Me.DgVtaAgte.Item(6, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(7, fila_dt).Value), 0, Me.DgVtaAgte.Item(7, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(8, fila_dt).Value), 0, Me.DgVtaAgte.Item(8, fila_dt).Value)
            Dim cel9 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(9, fila_dt).Value), 0, Me.DgVtaAgte.Item(9, fila_dt).Value)
            Dim cel10 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(10, fila_dt).Value), 0, Me.DgVtaAgte.Item(10, fila_dt).Value)
            Dim cel11 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(11, fila_dt).Value), 0, Me.DgVtaAgte.Item(11, fila_dt).Value)
            Dim cel12 As String = Me.DgVtaAgte.Item(12, fila_dt).Value
            Dim cel13 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(13, fila_dt).Value), 0, Me.DgVtaAgte.Item(13, fila_dt).Value)


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
            oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 2)
            oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
            oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 2)
            oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 2)
            oSheet.range("M" & fila_dt_excel).value = cel12
            oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel13, 2)
        Next


        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        oSheet.columns("A:N").entirecolumn.autofit()
        oSheet.range("A1").value = "Reporte de Ventas Por Agente Del Periodo " + Format(Me.DtpFechaIni.Value)
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

    Private Sub BtnClientes_Click(sender As Object, e As EventArgs) Handles BtnClientes.Click
        '--------Generar Excel Clientes

        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        Dim Rangos As String = ""
        Dim Rangos2 As String = ""

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)


        'Declaramos el nombre de las columnas
        oSheet.range("A3").value = "CardCode"
        oSheet.range("B3").value = "CardName"
        oSheet.range("C3").value = "VtaDia"
        oSheet.range("D3").value = "Acumulado"
        oSheet.range("E3").value = "Pron Fin Mes"   'celda I3
        oSheet.range("F3").value = "SlpCode"    'celda L3
        oSheet.range("G3").value = "SlpName"    'celda M3
        oSheet.range("H3").value = "GroupCode"  'celda N3
        oSheet.range("I3").value = "GroupName"  'celda O3

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A3:I3").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = DgClientes.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel0 As String = Me.DgClientes.Item(0, fila_dt).Value
            Dim cel1 As String = Me.DgClientes.Item(1, fila_dt).Value
            Dim cel2 As String = IIf(IsDBNull(Me.DgClientes.Item(2, fila_dt).Value), 0, Me.DgClientes.Item(2, fila_dt).Value)
            Dim cel3 As String = IIf(IsDBNull(Me.DgClientes.Item(3, fila_dt).Value), 0, Me.DgClientes.Item(3, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DgClientes.Item(4, fila_dt).Value), 0, Me.DgClientes.Item(4, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DgClientes.Item(5, fila_dt).Value), 0, Me.DgClientes.Item(5, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DgClientes.Item(6, fila_dt).Value), 0, Me.DgClientes.Item(6, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DgClientes.Item(7, fila_dt).Value), 0, Me.DgClientes.Item(7, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DgClientes.Item(8, fila_dt).Value), 0, Me.DgClientes.Item(8, fila_dt).Value)

            fila_dt_excel = fila_dt + 4

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel0
            oSheet.range("B" & fila_dt_excel).value = cel1
            oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
            oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
            oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
            oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
            oSheet.range("G" & fila_dt_excel).value = cel6
            oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
            oSheet.range("I" & fila_dt_excel).value = cel8

        Next


        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        oSheet.columns("A:N").entirecolumn.autofit()
        oSheet.range("A1").value = "Reporte de Clientes del Agente  " + Format(Me.DtpFechaIni.Value)
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

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New ScoreCardIngresarObjetivos()
        frm.Show()
    End Sub

End Class


'Para establecer mediante programación la celda actual
'Me.dataGridView1.CurrentCell = Me.dataGridView1(1, 0)