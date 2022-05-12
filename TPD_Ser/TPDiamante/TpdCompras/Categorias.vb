
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class Categorias
    'Conexiones a la Base de datos
    Public StrProd As String = conexion_universal.CadenaSBO_Diamante
    Public StrTpm As String = conexion_universal.CadenaSQL
    Public StrCon As String = conexion_universal.CadenaSQLSAP

    Dim DvCategorias As New DataView

    Private Sub Categorias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaFin.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaIni.Value = "01-01-2015"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try

            Dim cnn As SqlConnection = Nothing
                Dim cmd4 As SqlCommand = Nothing
                cnn = New SqlConnection(StrTpm)
                cnn.Open()
                cmd4 = New SqlCommand("SPCategorias", cnn)
                cmd4.CommandType = CommandType.StoredProcedure
                cmd4.Parameters.Add("@FechaInicial", SqlDbType.Date).Value = Me.DtpFechaIni.Value
                cmd4.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = Me.DtpFechaFin.Value

                cmd4.ExecuteNonQuery()
                cmd4.Connection.Close()
                Dim da As New SqlDataAdapter
                da.SelectCommand = cmd4
                da.SelectCommand.Connection = cnn


                ''--------------------------------------------
                Dim DsVtas As New DataSet
                da.Fill(DsVtas, "DsVtas")

                DsVtas.Tables(0).TableName = "Categorias"

                DvCategorias.Table = DsVtas.Tables("Categorias")

                DGCategorias.DataSource = DvCategorias

            DisenoGrid()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Sub DisenoGrid()
        Try

            'DisenoGrid()

            With Me.DGCategorias
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

                DGCategorias.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(0).HeaderText = "Id"
                .Columns(0).Visible = False

                .Columns(1).HeaderText = "Clave Art."
                .Columns(1).Width = 120
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(2).HeaderText = "Descripción"
                .Columns(2).Width = 200
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(3).HeaderText = "Línea"
                .Columns(3).Width = 110
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(4).HeaderText = "Cantidad"
                .Columns(4).Width = 80
                .Columns(4).DefaultCellStyle.Format = " ###,###,##0"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Total sin IVA"
                .Columns(5).Width = 80
                .Columns(5).DefaultCellStyle.Format = " ###,##0.#0"
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(6).HeaderText = "Ganancia Bruta"
                .Columns(6).Width = 80
                .Columns(6).DefaultCellStyle.Format = "#,###,##0.#0"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(7).HeaderText = "Ganancia Bruta (%)"
                .Columns(7).Width = 80
                .Columns(7).DefaultCellStyle.Format = "##0.###0"
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(8).HeaderText = "Acumulado"
                .Columns(8).Width = 80
                .Columns(8).DefaultCellStyle.Format = "#,##0.###0"
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(9).HeaderText = "Categoría"
                .Columns(9).Width = 80
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim DifDias As Integer
        DifDias = DateDiff("d", "01/09/2016", Today) '"22/09/2016"
        MsgBox(DifDias)
    End Sub
End Class