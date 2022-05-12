Imports System.Data.SqlClient

Public Class frmAccesoRevision
    Private Sub frmAccesoRevision_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim SQLStatus As String = ""
        conexion_universal.slq_s = New SqlCommand(SQLStatus, conexion_universal.conexion_uni)

    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        'DESHABILITA LOS ELEMENTOS
        txtClave.Enabled = True
        btnAceptar.Enabled = True
        btnCancelar.Enabled = True

        'BUSCA EL ID O CLAVE EXISTE EN LA BASE DE DATOS DEL TPD
        Dim SQLEmpleado As String
        'VARIABLE DE ALMACENAMIENTO DE EMPLEADO
        Dim UserIdRevisa As String
        Dim idUsuario As Integer
        Dim NameSurtidor As String
        '-----
        'ALAMACENA LA CONSULTA
        SQLEmpleado = "SELECT  * FROM Operacion_Empleado WHERE KeyCode = '" + txtClave.Text + "' AND Frozen = 'Y' AND Acceso_Revisar = 1 "

        Try
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar()
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLEmpleado, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If (conexion_universal.rd_s.Read) Then
                UserIdRevisa = rd_s.Item("KeyCode")
                idUsuario = rd_s.Item("Id_Empleado")
                NameSurtidor = rd_s.Item("Name")

                Module1.Bandera = True
                Module1.Usuario = UserIdRevisa




            Else
                'MsgBox("Por que da el error")
                MsgBox("Clave de acceso incorrecta o no cuentas con el acceso a revisar, favor de intentar nuevamente.", MsgBoxStyle.Exclamation, "Alerta de Acceso")
                conexion_universal.rd_s.Close() 'CIERRA EL READE
                conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
                txtClave.Focus()
                CierraDialogAcceso = True
                Bandera = False
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

        'ACTUALIZA LA ORDEN DE VENTA QUE SE VA A SURTIR, EN AL CUAL AGREGA EL STATUS Y QUE EMPLEADO LO SURTE
        If Actualizar_Operacion_Entrega_Revision("UserId_Revisado", idUsuario, "EP", "En Revision", DocNumAccesoRev, "UserId_Update", idUsuario) = False Then
            CierraDialogAcceso = False
            Return
        End If


        'HABILITA LOS ELEMENTOS
        txtClave.Enabled = False
        btnAceptar.Enabled = False
        btnCancelar.Enabled = False
        'ALMACENA VERDADERO PARA CERRAR EL DIALOG SIN MENSAJE
        CierraDialogAcceso = True
        'CIERRA EL FORMULARIO DE DIALOG
        Me.Close()
        'INICIALIZA EN NADA LA VARIABLE DE Status
        'Status = ""
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
        Module1.Bandera = False
    End Sub
End Class