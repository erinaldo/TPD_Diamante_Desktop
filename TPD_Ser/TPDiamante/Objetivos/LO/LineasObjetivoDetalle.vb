
Imports System.Data.SqlClient

Public Class LineasObjetivoDetalle

    Private Sub LineasObjetivoDetalle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim vDiasMes As Integer
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Dim cmd2 As SqlCommand = Nothing
        'Dim vDiasTrans As Integer

        Dim cmd3 As SqlCommand = Nothing
        Dim cmd4 As SqlCommand

        Try
            cnn = New SqlConnection(StrTpm)

            cnn.Open()

            'vDiasMes = CInt(cmd.ExecuteScalar())
            'txtDiasMes.Text = vDiasMes.ToString

            'cmd2 = New SqlCommand("Indicadores", cnn)
            'cmd2.CommandType = CommandType.StoredProcedure
            'cmd2.Parameters.Add("@FechaFinal", SqlDbType.SmallDateTime).Value = LineasHalcon.DtpFechaIni.Value
            'cmd2.Parameters.Add("@TipoConsulta", SqlDbType.Int).Value = 2

            'vDiasTrans = CInt(cmd2.ExecuteScalar())
            'txtDiasTranscurridos.Text = vDiasTrans.ToString


            cmd4 = New SqlCommand("SPLODetalle", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            'cmd4.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = LineasHalcon.DtpFechaIni.Value
            'cmd4.Parameters.Add("@DiasMes", SqlDbType.Int).Value = vDiasMes
            'cmd4.Parameters.Add("@DiasTrans", SqlDbType.Int).Value = vDiasTrans
            'cmd4.Parameters.Add("@DiasRest", SqlDbType.Int).Value = vDiasMes - vDiasTrans
            'cmd4.Parameters.Add("@PorAvanOptimo", SqlDbType.Decimal).Value = vDiasTrans / vDiasMes
            'cmd4.Parameters.Add("@Agente", SqlDbType.VarChar, 30).Value = "RICARDO ROBLES (33)"
            cmd4.Parameters.Add("@Agente", SqlDbType.NVarChar, 100).Value = LOIngObj.DGObjAge.Item(0, LOIngObj.DGObjAge.CurrentCell.RowIndex).Value
            cmd4.Parameters.AddWithValue("@mes", LOIngObj.DGObjAge.Item(1, LOIngObj.DGObjAge.CurrentCell.RowIndex).Value)
            cmd4.Parameters.AddWithValue("@anio", LOIngObj.DGObjAge.Item(2, LOIngObj.DGObjAge.CurrentCell.RowIndex).Value)


            Label1.Text = LOIngObj.DGObjAge.Item(0, LOIngObj.DGObjAge.CurrentCell.RowIndex).Value
            'Me.CmbAgteVta.SelectedValue

            'Dim mes As Int16
            'mes = DtpFechaIni.Text.Substring(3, 2)
            ''MsgBox(mes)
            'Dim anio As Int16
            'anio = DtpFechaIni.Text.Substring(6, 4)
            ''MsgBox(anio)

            'cmd4.Parameters.Add("@MesActual", SqlDbType.Int).Value = mes
            'cmd4.Parameters.Add("@AñoActual", SqlDbType.Int).Value = anio


            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn


            ''--------------------------------------------
            Dim DvAgentes As New DataView


            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "VtaAgtes"

            DvAgentes.Table = DsVtas.Tables("VtaAgtes")

            DataGridView1.DataSource = DvAgentes


        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

        'txtDiasRestantes.Text = Convert.ToString(vDiasMes - vDiasTrans)
        'txtAvanceOptimo.Text = Format(Convert.ToString((vDiasTrans / vDiasMes) * 100), "000.00")

        'txtAvanceOptimo.Text = (vDiasTrans / vDiasMes).ToString("P1")


        With Me.DataGridView1
            Try


                '.DataSource = DtAgte
                .ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = False
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                'Color de linea del grid

                .Columns(0).HeaderText = "Clave"
                .Columns(0).Width = 40
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Linea"
                .Columns(1).Width = 100
                .Columns(1).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(2).HeaderText = "Objetivo"
                .Columns(2).Width = 70
                .Columns(2).DefaultCellStyle.Format = "$ ###,##0.#0"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            Catch ex As Exception

            End Try

        End With
    End Sub
End Class