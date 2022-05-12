Imports System.Data.SqlClient

Public Class frmValidaAcceso
  'VARIABLE DE STATUS GLOBAL A NIVEL FORMULARIO
  Dim Status As String = ""

  Private Sub frmAcceso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ValidaUsuario.Id_Empleado = 0
    ValidaUsuario.KeyCode = ""
    ValidaUsuario.Name = ""
    ValidaUsuario.Frozen = ""
  End Sub

  Private Sub txtClave_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtClave.KeyPress
    'DETECTA EL ENTER Y REALIZA UNA OPCIÓN
    If Asc(e.KeyChar) = Keys.Enter Then
      'SE ACTIVA EL EVENTO CLIC DEL BOTON ACEPTAR
      btnAceptar.PerformClick()
    End If
  End Sub

  Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
    'DESHABILITA LOS ELEMENTOS
    txtClave.Enabled = True
    btnAceptar.Enabled = True
    btnCancelar.Enabled = True

    'BUSCA EL ID O CLAVE EXISTE EN LA BASE DE DATOS DEL TPD
    Dim SQLEmpleado As String

    '-----
    'ALAMACENA LA CONSULTA
    SQLEmpleado = "SELECT * FROM Operacion_Empleado WHERE KeyCode = '" + txtClave.Text + "' AND Frozen = 'Y' "

    Try
      'CONECTA A LA BASE DE DATOS
      conexion_universal.conectar()
      'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
      conexion_universal.slq_s = New SqlCommand(SQLEmpleado, conexion_universal.conexion_uni)
      'EJECUTA LA CONSULTA
      conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
      'RECORRE LA CONSULTA
      If (conexion_universal.rd_s.Read) Then
        ValidaUsuario.Id_Empleado = rd_s.Item("Id_Empleado")
        ValidaUsuario.KeyCode = rd_s.Item("KeyCode")
        ValidaUsuario.Name = rd_s.Item("Name")
        ValidaUsuario.Frozen = rd_s.Item("Frozen")
      Else
        'MsgBox("Por que da el error")
        MsgBox("Clave de acceso incorrecta, favor de intentar nuevamente.", MsgBoxStyle.Exclamation, "Alerta de Acceso")
        conexion_universal.rd_s.Close() 'CIERRA EL READE
        conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
        txtClave.Focus()
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
    '-----

    'Graba la informacion a devolver
    Me.Close()
  End Sub

  Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
    'INICIALIZA EN NADA LA VARIABLE DE TITULO DE ACCESO
    TituloAcceso = ""
    StatusAcceso = ""
    Status = ""
    'CIERRA EL FORMULARIO
    Me.Close()
  End Sub
End Class