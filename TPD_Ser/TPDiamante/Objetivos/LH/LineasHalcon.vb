Imports System.Data
Imports System.Data.SqlClient

Public Class LineasHalcon

    'Conexiones a la Base de datos
    Public StrProd As String = "Data Source=SERVIDORSAP;Initial Catalog=SBO-Diamante-productiva;User Id=SA;Password=SD1amany3S;"
    Public StrTpm As String = "Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;"
    Public StrCon As String = "Data Source=SERVIDORSAP;Initial Catalog=SBO_TPD;User Id=SA;Password=SD1amany3S;"

    Dim DvTotales As New DataView
    Dim DvAgentes As New DataView
    Dim DvAgentes2 As New DataView
    Dim DvClientes As New DataView


    Private Sub LineasHalcon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            ConsutaLista &= "WHERE T1.CbrGralAdicional = 'N' AND T0.SLPCODE <> 1 ORDER BY slpcode, slpname "


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
        'DgVtaAgte.Rows.Clear()
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


            cmd4 = New SqlCommand("SPLineasHalcon", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = Me.DtpFechaIni.Value
            cmd4.Parameters.Add("@DiasMes", SqlDbType.Int).Value = vDiasMes
            cmd4.Parameters.Add("@DiasTrans", SqlDbType.Int).Value = vDiasTrans
            cmd4.Parameters.Add("@DiasRest", SqlDbType.Int).Value = vDiasMes - vDiasTrans
            cmd4.Parameters.Add("@PorAvanOptimo", SqlDbType.Decimal).Value = vDiasTrans / vDiasMes
            cmd4.Parameters.Add("@Sucursal", SqlDbType.Int).Value = Me.CmbSucursal.SelectedValue
            cmd4.Parameters.Add("@Agente", SqlDbType.VarChar, 30).Value = Me.CmbAgteVta.SelectedValue

            Dim mes As Integer
            mes = DtpFechaIni.Text.Substring(3, 2)
            'MsgBox(mes)
            Dim anio As Integer
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
            'DsVtas.Tables(2).TableName = "VtaCltes"

            DvTotales.Table = DsVtas.Tables("Totales")
            DvAgentes.Table = DsVtas.Tables("VtaAgtes")
            'DvClientes.Table = DsVtas.Tables("VtaCltes")


            DgTotales.DataSource = DvTotales

            DgVtaAgte.DataSource = DvAgentes

        Catch ex As Exception
            MsgBox(ex.Message)
            'MsgBox("No existen ventas de este día")
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



            Try

                'Catch ex As Exception

                'End Try

                .Columns(0).HeaderText = "Objetivo"
                .Columns(0).Width = 95
                .Columns(0).DefaultCellStyle.Format = "$ ###,##0,##0"
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Venta"
                .Columns(1).Width = 95
                .Columns(1).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(2).HeaderText = "Venta Vs Cuota"
                .Columns(2).Width = 85
                .Columns(2).DefaultCellStyle.Format = "% #0.#0"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Por Vender "
                .Columns(3).Width = 95
                .Columns(3).DefaultCellStyle.Format = "$ #,###,##0.#0"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).HeaderText = "Tendencia fin mes ($)"
                .Columns(4).Width = 95
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(5).HeaderText = "Tendencia fin mes (%)"
                .Columns(5).Width = 85
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Format = "% #0.#0"

                '.Columns.Add = "ColumnCheck"
                '.Columns(6).Width = 85
                '.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(6).DefaultCellStyle.Format = "% #0.#0"


                'Dim ColumnCheck = New DataGridViewCheckBoxColumn()
                ''dgvColumnCheck.Name = HeaderText(i)
                ''dgvColumnCheck.HeaderText = HeaderText(i)
                ' ''Aca se agrega la columna tipo CheckBox
                '.Columns.Add(ColumnCheck)
                '.Columns(6).HeaderText = "check"

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
                .RowHeadersWidth = 30
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                'Color de linea del grid

                .Columns(0).HeaderText = "Vendedor"
                .Columns(0).Width = 140
                '.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Sucursal"
                .Columns(1).Width = 70
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(2).HeaderText = "Objetivo"
                .Columns(2).Width = 95
                .Columns(2).DefaultCellStyle.Format = "$ ###,##0,##0"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .Columns(3).HeaderText = "Venta"
                .Columns(3).Width = 95
                .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .Columns(4).HeaderText = "Venta Vs Cuota"
                .Columns(4).Width = 95
                .Columns(4).DefaultCellStyle.Format = "% #,##0.#0"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Por Vender"
                .Columns(5).Width = 95
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(6).HeaderText = "Tendencia Fin de Mes ($)"
                .Columns(6).Width = 95
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(7).HeaderText = "Tendencia Fin de Mes (%)"
                .Columns(7).Width = 95
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Format = "% #,##0.#0"

                'Dim btn As New DataGridViewButtonColumn()

                'DgVtaAgte.Columns.Add(btn)
                'btn.HeaderText = "Detalle"
                'btn.Text = "Detalle"
                'btn.Name = "btn"
                'btn.Width = 105
                'btn.UseColumnTextForButtonValue = True


            Catch ex As Exception

            End Try

        End With

    End Sub

    'Private Sub DgVtaAgte_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgVtaAgte.CellContentClick
    '    'If e.ColumnIndex = 8 Then
    '    '    MsgBox(("Row : " + e.RowIndex.ToString & "  Col : ") + e.ColumnIndex.ToString)


    '    'LineasHalconDetalle.Location = New Point(20, 20)

    '    'End If
    'End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New LHIngObj()
        frm.Show()
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
        oSheet.range("A3").value = "Objetivo"
        oSheet.range("B3").value = "Venta"
        oSheet.range("C3").value = "VentaVsCuota"
        oSheet.range("D3").value = "PorVender"
        oSheet.range("E3").value = "Tendencia fin mes ($)"
        oSheet.range("F3").value = "Tendencia fin mes (%)"
  

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A3:F3").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = DgTotales.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            'Dim cel0 As String = Me.DgTotales.Item(0, fila_dt).Value
            'Dim cel1 As String = Me.DgTotales.Item(1, fila_dt).Value
            Dim cel0 As String = IIf(IsDBNull(Me.DgTotales.Item(0, fila_dt).Value), 0, Me.DgTotales.Item(0, fila_dt).Value)
            Dim cel1 As String = IIf(IsDBNull(Me.DgTotales.Item(1, fila_dt).Value), 0, Me.DgTotales.Item(1, fila_dt).Value)
            Dim cel2 As String = IIf(IsDBNull(Me.DgTotales.Item(2, fila_dt).Value), 0, Me.DgTotales.Item(2, fila_dt).Value)
            Dim cel3 As String = IIf(IsDBNull(Me.DgTotales.Item(3, fila_dt).Value), 0, Me.DgTotales.Item(3, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DgTotales.Item(4, fila_dt).Value), 0, Me.DgTotales.Item(4, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DgTotales.Item(5, fila_dt).Value), 0, Me.DgTotales.Item(5, fila_dt).Value)

            fila_dt_excel = fila_dt + 4

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = FormatNumber(cel0, 0)
            oSheet.range("B" & fila_dt_excel).value = FormatNumber(cel1, 1)
            oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
            oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
            oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
            oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
        Next

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        oSheet.columns("A:F").entirecolumn.autofit()

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

    Private Sub DgVtaAgte_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgVtaAgte.CellClick
        Dim frm As New LineasHalconDetalle()
        frm.Show()
    End Sub
End Class