Imports System.Data.SqlClient

Public Class frmSolicitaFirma
 Public StatusFirmaAcceso As Boolean = False

 Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
  Dim SQLEmpleado As String
  'ALAMACENA LA CONSULTA
  SQLEmpleado = "SELECT * FROM Operacion_Empleado WHERE KeyCode = '" + txtClave.Text + "' AND Frozen = 'Y'"

  Try
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(SQLEmpleado, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
   'RECORRE LA CONSULTA
   If (conexion_universal.rd_s.Read) Then
    If (rd_s.Item("Permiso_para_cambiarPreclaves") = 1) Then
     StatusFirmaAcceso = True
     Me.Close()
    Else
     MsgBox("El usuario no tiene privilegios", MsgBoxStyle.Exclamation, "Usuario sin privilegios")
    End If
   Else
    MsgBox("Clave de usuario incorrecta, favor de intentar nuevamente.", MsgBoxStyle.Exclamation, "Error de credenciales")
    conexion_universal.rd_s.Close() 'CIERRA EL READE
    conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
    CierraDialogAcceso = False
    Return
   End If
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   '-----

  Catch ex As Exception
   MsgBox("Error en busqueda del Empleado: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexioón")
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   CierraDialogAcceso = False
   Return
  End Try
 End Sub

 Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
  'INICIALIZA EN NADA LA VARIABLE DE TITULO DE ACCESO
  lblmsgfirma.Text = ""
  StatusFirmaAcceso = False
  'CIERRA EL FORMULARIO
  Me.Close()
 End Sub
End Class