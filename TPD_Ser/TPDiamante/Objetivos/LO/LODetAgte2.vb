
Imports System.Data.SqlClient

Public Class LODetAgte2
    Dim cnn As SqlConnection = Nothing
    Dim cmd As SqlCommand = Nothing
    Private Sub LineasObjetivoDetalle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim vDiasMes As Integer



        Try

            cnn = New SqlConnection(StrTpm)

            cnn.Open()

            cmd = New SqlCommand("SPLODetalle", cnn)
            cmd.CommandType = CommandType.StoredProcedure

            Dim mes As New Integer
            mes = LineasObjetivo.DtpFechaIni.Text.Substring(3, 2)
            'MsgBox(mes)
            Dim anio As New Integer
            anio = LineasObjetivo.DtpFechaIni.Text.Substring(6, 4)

            cmd.Parameters.Add("@Agente", SqlDbType.NVarChar, 100).Value = LineasObjetivo.DgVtaAgte.Item(0, LineasObjetivo.DgVtaAgte.CurrentCell.RowIndex).Value
            cmd.Parameters.AddWithValue("@mes", mes)
            cmd.Parameters.AddWithValue("@anio", anio)


            Label1.Text = "Detalle del Agente: " & LineasObjetivo.DgVtaAgte.Item(0, LineasObjetivo.DgVtaAgte.CurrentCell.RowIndex).Value

            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd
            da.SelectCommand.Connection = cnn


            ''--------------------------------------------
            Dim DvAgentes As New DataView


            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "VtaAgtes"

            DvAgentes.Table = DsVtas.Tables("VtaAgtes")

            DataGridView1.DataSource = DvAgentes


        Catch ex As Exception
            MsgBox(ex.Message)
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
                .Columns(0).Width = 30
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Linea"
                .Columns(1).Width = 90
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(2).HeaderText = "Objetivo"
                .Columns(2).Width = 60
                .Columns(2).DefaultCellStyle.Format = "$ #,##0"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            Catch ex As Exception

            End Try

        End With

    End Sub

End Class