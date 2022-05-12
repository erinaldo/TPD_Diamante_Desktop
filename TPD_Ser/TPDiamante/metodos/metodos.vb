Imports System.Data.SqlClient

Module metodos
  'VARIABLES DE CONEXION
  Public dt As DataTable
  Public conexion As New SqlConnection(conexion_universal.CadenaSQLSAP)
  Public adaptador As SqlDataAdapter
  Public adaptador_s As SqlDataAdapter
  Public consulta As SqlCommand
  Public consulta_s As SqlCommand
  Public respuesta As SqlDataReader
  Public respuesta_s As SqlDataReader
  Public linea_OK As Boolean
  Public Lista1 As New ArrayList

  'REALIZA LA CONEXIÓN A LA BASE DE DATOS
  Sub conectar()
    Try
      conexion = New SqlConnection(conexion_universal.CadenaSQLSAP)
      conexion.Open()
    Catch ex As Exception
      MsgBox("Error al realizar la conexion: " & ex.ToString, MsgBoxStyle.Critical, "Error de conexión")
      conexion.Close()
    End Try
  End Sub

  'LLENA UN COMBO BOX CON OPCIONES DE AUTOCOMPLETAR
  Sub autoCompletarTextbox(ByVal campoTexto As ComboBox)
    'Sub autoCompletarTextbox()
    'CAPTURA EL ERROR
    'Try
    '    'MANDA A LLAMAR EL METODO DE CONECTAR A LA BASE DE DATOS
    '    conectar()

    '    'CONSULTA PARA AUTOLLENADO DE LAS LINEAS
    '    consulta = New SqlCommand("select ItmsGrpNam from OITB where ItmsGrpCod <> 200 and ItmsGrpCod <> 150 order by ItmsGrpNam", conexion)
    '    'EJECUTA LA CONSULTA
    '    respuesta = consulta.ExecuteReader()
    '    'RECORRE LA CONSULTA
    '    While respuesta.Read
    '        'AUTOCOMPLETA LA RESPUESTA
    '        campoTexto.AutoCompleteCustomSource.Add(respuesta.Item("ItmsGrpNam"))
    '    End While
    '    'CIERRA LA RESPUESTA DEL SQLDATAREADER
    '    respuesta.Close()
    'Catch ex As Exception
    '    'MANDA ERROR A PANTALLA
    '    MsgBox("Error en la conexión o consulta: " & ex.ToString, MsgBoxStyle.Critical)
    'End Try
  End Sub

  'VALIDA SI EXISTE LA LINEA PARA PODER AGREGARLA EN EL FORMULARIO DE VALOR_INVENTARIO
  Sub valida_linea(ByVal line As String)
    'INICIALIZA LA BARIABLE EN FALSE
    linea_OK = False
    'CAPTURA EL ERROR
    Try
      'CONSULTA PARA AUTOLLENADO DE LAS LINEAS
      consulta = New SqlCommand("select ItmsGrpNam from OITB where ItmsGrpNam = '" & line & "' and (ItmsGrpCod <> 200 and ItmsGrpCod <> 150) order by ItmsGrpNam", conexion)
      'EJECUTA LA CONSULTA
      respuesta = consulta.ExecuteReader()
      'VALIDA SI EL DATO ESTA CORRECTO O NO
      If respuesta.Read Then
        linea_OK = True
      Else
        linea_OK = False
      End If
      'CIERRA LA RESPUESTA DEL SQLDATAREADER
      respuesta.Close()
    Catch ex As Exception
      'MANDA ERROR A PANTALLA
      MsgBox("Error en la conexión o consulta: " & ex.ToString, MsgBoxStyle.Critical)
    End Try
  End Sub
End Module
