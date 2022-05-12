
Imports System.Data.SqlClient
Public Class LODetalleGen2

    Private Sub LODetalleGen2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim vDiasMes As Integer
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing


        Try

            cnn = New SqlConnection(StrTpm)

            cnn.Open()

            cmd = New SqlCommand("SPLODetalleTotales", cnn)
            cmd.CommandType = CommandType.StoredProcedure

            Dim mes As New Integer
            mes = LineasObjetivo.DtpFechaIni.Text.Substring(3, 2)
            'MsgBox(mes)
            Dim anio As New Integer
            anio = LineasObjetivo.DtpFechaIni.Text.Substring(6, 4)

            'cmd.Parameters.Add("@Agente", SqlDbType.NVarChar, 100).Value = LOIngObj.DataGridView3.Item(0, LOIngObj.DataGridView3.CurrentCell.RowIndex).Value
            cmd.Parameters.AddWithValue("@mes", mes)
            cmd.Parameters.AddWithValue("@anio", anio)


            Label1.Text = "Detalle de lineas del Mes: " & mes & " Año: " & anio

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
                .Columns(0).Width = 50
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Linea"
                .Columns(1).Width = 150
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            Catch ex As Exception

            End Try

        End With


        Try


            Dim mes As New Integer
            mes = LineasObjetivo.DtpFechaIni.Text.Substring(3, 2)
            'MsgBox(mes)
            Dim anio As New Integer
            anio = LineasObjetivo.DtpFechaIni.Text.Substring(6, 4)

            Dim cmd2 As SqlCommand = New SqlCommand("SELECT objetivo FROM LOObjGen where mes ='" & mes & "' and anio='" & anio & "'", cnn)

            'cmd.Parameters.AddWithValue("@desc", CStr(cbEquipo.SelectedValue))

            Dim da2 As SqlDataAdapter = New SqlDataAdapter(cmd2)
            Dim dt As New DataTable
            da2.Fill(dt)

            If dt.Rows.Count > 0 Then

                Dim row As DataRow = dt.Rows(0)

                TBObjetivo.TextAlign = HorizontalAlignment.Right

                TBObjetivo.Text = CStr(row("objetivo"))
                TBObjetivo.Text = Format(Integer.Parse(TBObjetivo.Text), "###,###,###")
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class
