Imports System.Data.SqlClient
Public Class PagoCapCom

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        Dim vComent As String
        'Dim con As New SqlConnection
        'Dim cmd As New SqlCommand
        Dim CadenaSQL As String = ""
        vComent = QuitarCaracteres(TxtComentario.Text)
        Dim colEdit As String = ""

        Dim sql As New Comandos_SQL

        CadenaSQL = "select * from COMP1 where FactCompras = " + TxtFactura1.Text

    'VALIDAR PERFIL PARA EDITAR COLUMNA EN BASE DE DATOS
    'If UsrTPM = "MANAGER" Then
    '    colEdit = "Coment2"
    'Else
    '    colEdit = "Coment"
    'End If

    'Ambos usaran ahora este campo solamente
    colEdit = "Coment"

    sql.conectarTPM()
        'If TxtComentario.Text <> "" Then
        'CREADO POR IVAN GONZALEZ
        Try
                'VALIDAR SI EXISTE REGISTRO EN COMP1
                If sql.SiExiste(CadenaSQL) Then
                    '
                    CadenaSQL = "UPDATE COMP1 SET FactCompras = " + TxtFactura1.Text +
                                               " , fecha = " + DateTime.Now.ToString("yyyy-MM-dd") + " , Id_Usuario = '" + UsrTPM + "' , " + colEdit + " = '" + vComent +
                                               "' WHERE FactCompras = " + TxtFactura1.Text
                    Try
                        'ACTUALIZAR EL REGISTRO DEPENDIENDO FACTCOMPRAS
                        If sql.EjecutarComando(CadenaSQL) Then
                            MessageBox.Show("El comentario se modifico correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error al ejecutar el comando " & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        sql.Cerrar()
                    End Try
                Else
                    '
                    CadenaSQL = "INSERT INTO COMP1 (FactCompras,Fecha,Id_Usuario," + colEdit + ") " +
                                           "VALUES (" + TxtFactura1.Text + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + UsrTPM + "','" +
                                                        vComent + "')"
                    Try
                        'INSERTAR REGISTRO EN LA TABLA COMP1 EN DADO CASO DE QUE NO EXISTA
                        If sql.EjecutarComando(CadenaSQL) Then
                            MessageBox.Show("El comentario se guardo correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error al ejecutar el comando " & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        sql.Cerrar()
                    End Try
                End If
                sql.Cerrar()
            Catch ex As Exception
                MessageBox.Show("Error al verificar si existe el registro " & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                sql.Cerrar()
            End Try
            'Else
            '    Try
            '        CadenaSQL = "DELETE FROM COM1 WHERE FactCompras = " + TxtFactura1.Text
            '        sql.EjecutarComando(CadenaSQL)
            '    Catch ex As Exception
            '        MessageBox.Show("Error al eliminar registro " & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Finally
            '        sql.Cerrar()
            '    End Try
            'End If




            'vComent = vComent.Replace(Chr(10), " ")

            'OMITIR PARA QUE NO SE QUITE LO BLOQUEADO
            'CadenaSQL = "DELETE FROM COMP1 WHERE FactCompras = " + TxtFactura1.Text + "  "

            'SOLO ACTUALIZAR EL COMENTARIO DEPENDIENDO FACTCOMPRAS PARA NO PERDER EL BLOQUEO
            'If TxtComentario.Text <> "" Then
            '    'CadenaSQL &= "INSERT INTO COMP1 (FactCompras,Fecha,Id_Usuario,Coment) VALUES ("
            '    'CadenaSQL &= TxtFactura1.Text
            '    'CadenaSQL &= ","
            '    'CadenaSQL &= "@fecha"
            '    'CadenaSQL &= ",'"
            '    'CadenaSQL &= UsrTPM
            '    'CadenaSQL &= "','"
            '    'CadenaSQL &= vComent
            '    'CadenaSQL &= "')"
            'End If

            'Try
            '    con.ConnectionString = StrTpm
            '    con.Open()
            '    cmd.Connection = con
            '    cmd.CommandText = CadenaSQL
            '    cmd.Parameters.Add(New SqlParameter("@fecha", DateTime.Now))
            '    cmd.ExecuteNonQuery()

            'Catch ex As Exception
            '    MessageBox.Show("Error Eliminando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'Finally

            '    con.Close()
            'End Try
            If UsrTPM = "MANAGER" Then
      'PagoProveedores.DgFactProv.Rows(LblRow.Text).Cells("Comentarios Direccion").Value = vComent
      PagoProveedores.DgFactProv.Rows(LblRow.Text).Cells("Comentarios").Value = vComent
      Me.Close()
        Else
            PagoProveedores.DgFactProv.Rows(LblRow.Text).Cells("Comentarios").Value = vComent
            Me.Close()
        End If

    End Sub
End Class