Imports System.Data.SqlClient
Public Class FrmDiferenciaPrecioComentarioDireccion

    Public conexion3 As New SqlConnection(conexion_universal.CadenaSQL)
    Dim switch As Integer = 0

    Private Sub FrmDiferenciaPrecioComentarioDireccion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TBFactura.Text = DPFactura
        DTPFecFact.Value = DPFecFact
        TBArticulo.Text = DPArticulo
        TBDescripcion.Text = DPDescripcion
        TBLinea.Text = DPLinea
        TBProveedor.Text = DPProveedor
        TxtComentario.Text = DPComentariosDir
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            conexion3.Open()
            Dim cmd4 As SqlCommand = Nothing
            cmd4 = New SqlCommand("DifPreComentariosDir2", conexion3)
            cmd4.CommandType = CommandType.StoredProcedure

            cmd4.Parameters.Add("@Factura", SqlDbType.Int).Value = TBFactura.Text
            cmd4.Parameters.Add("@Fecha", SqlDbType.Date).Value = DTPFecFact.Value
            cmd4.Parameters.Add("@Articulo", SqlDbType.VarChar).Value = TBArticulo.Text
            cmd4.Parameters.Add("@Comentario", SqlDbType.VarChar).Value = TxtComentario.Text

            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da2 As New SqlDataAdapter
            da2.SelectCommand = cmd4
            da2.SelectCommand.Connection = conexion3

            ''--------------------------------------------
            MessageBox.Show("Registros guardados correctamente", "Diferencias de Precios", MessageBoxButtons.OK,
            MessageBoxIcon.Information)

            Switch = 1

            'DisenoGrid()
        Catch ex As Exception
            MsgBox("Error al guardar: " & ex.Message)
            Return
        Finally
            If conexion3 IsNot Nothing AndAlso conexion3.State <> ConnectionState.Closed Then
                conexion3.Close()
            End If
        End Try
    End Sub

    Private Sub FrmDiferenciaPrecioComentarioDireccion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub FrmDiferenciaPrecioComentarioDireccion_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If switch = 1 Then
            DiferenciaPrecioCompras.DgDifCompras(20, DPPosRen).Value = TxtComentario.Text
        End If
    End Sub
End Class