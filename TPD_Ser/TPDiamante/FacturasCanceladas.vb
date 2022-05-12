Imports System.Data.SqlClient

Public Class Facturas_Canceladas

    Dim DvConta As New DataView

    Private Sub FacturasCanceladas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim FechaIni As New Date
        FechaIni = Today
        FechaIni = FechaIni.AddDays(-FechaIni.Day + 1)

        'Devuelve el ultimo dia del mes de la fecha actual
        Dim FechaFin As New Date
        FechaFin = Today
        FechaFin = FechaFin.AddDays(-FechaFin.Day + 1).AddMonths(1).AddDays(-1)

        DTIni.Value = FechaIni
        DTFin.Value = FechaFin
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cnn As SqlConnection = Nothing
        Dim cmd4 As SqlCommand = Nothing

        Try
            cnn = New SqlConnection(StrTpm)

            

            cmd4 = New SqlCommand("GetFacturasCanceladas", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            'cmd4.Parameters.AddWithValue("@Tipodoc", 1)
            cmd4.Parameters.AddWithValue("@fechainicio", DTIni.Value)
            cmd4.Parameters.AddWithValue("@fechafin", DTFin.Value)

            cnn.Open()

            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()

            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn


            ''--------------------------------------------
            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "Inventario"

            DvConta.Table = DsVtas.Tables("Inventario")

            DataGridView1.DataSource = DvConta


            DisenoGrid()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try
    End Sub

    Private Sub DisenoGrid()
        Try
            With DataGridView1
                '.DataSource = DtAgte
                '.ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25

                .Columns(0).Visible = False

                .Columns(1).HeaderText = "Serie"
                .Columns(1).Width = 50
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .Columns(2).HeaderText = "Factura"
                .Columns(2).Width = 70
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Codigo de Cliente"
                .Columns(3).Width = 90
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).HeaderText = "Cliente"
                .Columns(4).Width = 400
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Fecha Factura"
                .Columns(5).Width = 90
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Vta Neta
                .Columns(6).HeaderText = "Total Factura"
                .Columns(6).Width = 100
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Format = "$ ###,###,##0.##"

                .Columns(7).HeaderText = "NC"
                .Columns(7).Width = 70
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(8).HeaderText = "Fecha Factura"
                .Columns(8).Width = 90
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(9).HeaderText = "Total NC"
                .Columns(9).Width = 100
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).DefaultCellStyle.Format = "$ ###,###,##0.##"
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bExcel_Click(sender As Object, e As EventArgs) Handles bExcel.Click
        ExportarDatosExcel(DataGridView1, "Facturas Canceladas")
    End Sub
End Class