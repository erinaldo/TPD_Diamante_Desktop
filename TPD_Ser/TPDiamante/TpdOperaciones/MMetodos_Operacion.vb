'LIBRERIAS USADAS EN EL MODULO
Imports System.Data.SqlClient
Imports System.Threading


'MODULO QUE TENDRA TODOS LOS METODOS QUE SE REQUIERAN PARA EL PROCESO DE APLICACIÓN DE OPERACIONES, ASÍ COMO VARIABLES GLOBALES
Module MMetodos_Operacion

  'INICIO VARIBALES GLOBALES POR MODULOS ==================
  '----- FORMULARIO frmAcceso
  Public TituloAcceso As String = "" 'ALAMCENA EL TITULO DEL FORMULARIO DE ACCEOS, EL ESTATUS Y QUE ORDEN ES
  Public DocNumAcceso As String = "" 'ALMACENA LA ORDEN QUE SE VA SURTIR
  Public StatusAcceso As String = "" 'ALMACENA EL STATUS QUE SE ENVIA
  Public Modificar As String = ""
  Public CierraDialogAcceso As Boolean = False 'ALMACENA EL VALOR DE FALSO O VERDADERO PARA CERRAR EL DIALOG
 Public DocNumAccesoRev As String = ""

 '----- FORMULARIO frmDetalleSurtir
 Public TituloSurtido As String = "" 'ALAMCENA EL TITULO DEL FORMULARIO DE DETALLE DE SURTIDO, EL ESTATUS Y QUE ORDEN ES
  Public DocNumSurtido As String = "" 'ALMACENA LA ORDEN QUE SE VA SURTIR
  Public StatusSurtido As String = "" 'ALMACENA EL STATUS QUE SE ENVIA
  Public ClaveSurtido As String = "" 'ALMACENA LA CLAVE DE QUIEN ESTA SURTIENDO EL PEDIDO
  Public CierraDialogSurtir As Boolean = False 'ALMACENA EL VALOR DE FALSO O VERDADERO PARA CERRAR EL DIALOG+

  '----- FORMULARIO frmDetalleEmpacar
  Public TituloEmpacar As String = "" 'ALAMCENA EL TITULO DEL FORMULARIO DE DETALLE DE SURTIDO, EL ESTATUS Y QUE ORDEN ES
  Public DocNumEmpacar As String = "" 'ALMACENA LA ORDEN QUE SE VA SURTIR
  Public StatusEmpacar As String = "" 'ALMACENA EL STATUS QUE SE ENVIA
  Public ClaveEmpacar As String = "" 'ALMACENA LA CLAVE DE QUIEN ESTA SURTIENDO EL PEDIDO
  Public CierraDialogEmpacarr As Boolean = False 'ALMACENA EL VALOR DE FALSO O VERDADERO PARA CERRAR EL DIALOG+

  Public Structure tUsuario
    Public Id_Empleado As Integer
    Public Name As String
    Public KeyCode As String
    Public Frozen As String
  End Structure

  Public ValidaUsuario As tUsuario

  'FIN VARIBALES GLOBALES POR MODULOS ==================

  '-----

  'INICIO METODOS GLOBALES POR MODULOS ==================







  'FIN METODOS GLOBALES POR MODULOS ==================

End Module
