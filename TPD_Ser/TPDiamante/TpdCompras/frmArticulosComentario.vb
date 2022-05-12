Imports System.Data.SqlClient

Public Class frmArticulosComentario
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim vComent As String
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim CadenaSQL As String = ""
        vComent = QuitarCaracteres(txtComentarios.Text)

        Try
            'ALAMACENA LA CONSULTA
            CadenaSQL = "SELECT * FROM ComprasArticulosE WHERE OrdenVta = " + txtOrdenVta.Text + " AND Articulo = '" + txtArticulo.Text + "' "
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar()
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(CadenaSQL, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If conexion_universal.rd_s.Read Then
                CadenaSQL = ""
                'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
                CadenaSQL = "UPDATE ComprasArticulosE SET Comentario = '" + vComent + "'  "
                CadenaSQL &= "WHERE OrdenVta = " + txtOrdenVta.Text + " AND Articulo = '" + txtArticulo.Text + "' "
                Try
                    con.ConnectionString = StrTpm
                    con.Open()
                    cmd.Connection = con
                    cmd.CommandText = CadenaSQL
                    cmd.ExecuteNonQuery()

                Catch ex As Exception
                    MessageBox.Show("Error al actualizar el Registro" & ex.Message, "Alerta SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    con.Close()
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show("Error al actualizar el Registro" & ex.Message, "Alerta SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'CIERRA LAS CONEXIONES DE USO
            con.Close()
            conexion_universal.cerrar_conectar()
        End Try
        'COLOCA EL COMENTARIO EN LA FILA INDICADA
        'frmArticulosEspeciales.dgvContenido.Rows(CInt(lblFila.Text)).Cells(13).Value = vComent
        Me.Close()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        'CIERRA EL DIALOG
        Me.Close()
    End Sub
End Class