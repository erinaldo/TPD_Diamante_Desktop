Imports System.Data.SqlClient

Public Class DifPreciosComentarios

    Public conexion2 As New SqlConnection(conexion_universal.CadenaSQL)
    Dim switch As Integer = 0

    Private Sub DifPreciosComentarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TBFactura.Text = DPFactura
        DTPFecFact.Value = DPFecFact
        TBArticulo.Text = DPArticulo
        TBDescripcion.Text = DPDescripcion
        TBLinea.Text = DPLinea
        TBProveedor.Text = DPProveedor
        TxtComentario.Text = DPComentarios

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            'MODIFICADO POR IVAN GONZALEZ
            conexion2.Open()
            Dim cmd4 As SqlCommand = Nothing
            cmd4 = New SqlCommand("SPDifPreComentarios", conexion2)
            cmd4.CommandType = CommandType.StoredProcedure

            cmd4.Parameters.Add("@Factura", SqlDbType.Int).Value = TBFactura.Text
            cmd4.Parameters.Add("@Fecha", SqlDbType.Date).Value = DTPFecFact.Value
            cmd4.Parameters.Add("@Articulo", SqlDbType.VarChar).Value = TBArticulo.Text
            cmd4.Parameters.Add("@Comentario", SqlDbType.VarChar).Value = TxtComentario.Text
            cmd4.Parameters.Add("@TipoCom", SqlDbType.VarChar).Value = DiferenciaPrecioCompras.TipoCom

            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da2 As New SqlDataAdapter
            da2.SelectCommand = cmd4
            da2.SelectCommand.Connection = conexion2

            ''--------------------------------------------
            MessageBox.Show("Registros guardados correctamente", "Diferencias de Precios", MessageBoxButtons.OK, _
            MessageBoxIcon.Information)

            switch = 1

            'DisenoGrid()
        Catch ex As Exception
            MsgBox("Error al guardar: " & ex.Message)
            Return
        Finally
            If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
                conexion2.Close()
            End If
        End Try
    End Sub

    Private Sub DifPreciosComentarios_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If switch = 1 Then
            DiferenciaPrecioCompras.DgDifCompras(19, DPPosRen).Value = TxtComentario.Text
        End If
    End Sub

    Private Sub TxtComentario_TextChanged(sender As Object, e As EventArgs) Handles TxtComentario.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class