'VARIABLE PARA LAS CONEXIONES
Imports System.Data.SqlClient
'LIBRERIA REQUERIDA PARA CARGAR EL CRYSTAL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmAcceso
 'VARIABLE DE STATUS GLOIBAL A NIVEL FORMULARIO
 Dim Status As String = ""
 'DECLARACION DE VARIABLE DE REPORTE Y INSTANCIA DEL MISMO
 Dim DocOrdenes As ReportDocument = New ReportDocument()
 'VARIBALE PARA EL PASO DE PARAMETROS DEL CRYSTAL
 Dim DocKey = String.Empty
 Dim ObjectType = String.Empty
 Dim _rutaPDF As String '// ALMACENA LA RUTA DEL PDF
 'VARIABLE VALIDA SI SE IMPRIMIO EL DOCUMENTO
 Dim ImpresoOK As Boolean = False

 Dim BaseDatosTPD As String = "TPM"
 Dim BaseDatosSAP As String = "SBO_TPD"

 Private Sub frmAcceso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
  'VARIABLE QUE ALMACENA CONSULTA DE STATUS
  Dim SQLStatus As String = ""

  'COLOCA EL NOMBRE DEL FORMULARIO SEGUN LO QUE TRAIGA EN EL DE SURTIR ORDENES
  Me.Text = TituloAcceso

  '-----
  'ALAMACENA LA CONSULTA DE SOLO OBTENER EL STATUS QUE CORRESPONDA
  SQLStatus = "SELECT Status, StatusName FROM Operacion_Status WHERE StatusName = '" + StatusAcceso + "' AND Activo = 'Y' "

  Try
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(SQLStatus, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
   'RECORRE LA CONSULTA
   If (conexion_universal.rd_s.Read) Then
    Status = rd_s.Item("Status") 'ALMACENA EL ID DEL STATUS QUE TRAE
   End If
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION

  Catch ex As Exception
   MsgBox("Error en busqueda del Status: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexioón")
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   Return
  End Try

  '-----

 End Sub

 'METODO QUE IMPRIME EL FORMATO DE LA ORDEN DE VENTA QUE SE EMPEZARÁ A SURTIR. Método anterior
 Sub MImprimeFormato()
  Try
   'RUTA DONDE SE GUARDA EL ARCHIVO PDF DE LA ORDEN
   _rutaPDF = "\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\OrdenPDF\" + DocNumAcceso.ToString() + ".pdf"
   'CREA EL PDF DE LA REFACTURA FACTURA

   '//PARAMETROS DE CONEXION PARA EL RPT
   Dim tInfo As TableLogOnInfo = New TableLogOnInfo()
   Dim ConnectionInfo As ConnectionInfo = tInfo.ConnectionInfo

   ConnectionInfo.Password = conexion_universal.cPassword
   ConnectionInfo.UserID = conexion_universal.cUserID
   ConnectionInfo.ServerName = conexion_universal.cServerName ' // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS
   ConnectionInfo.DatabaseName = conexion_universal.cDatabaseNameSAP
   'OBTIENE LA RUTA DEL ARCHIVO DE CRYSTAL PARA CARGARLO (PEDIDO ALMACEN)

   Dim informe As New PedidoArtAlmacen
   RepComsultaP.MdiParent = Inicio

   'CONEXION A LA BASE DE DATOS
   informe.SetParameterValue("DocKey@", DocNumAcceso)
   informe.SetParameterValue("ObjectId@", 17)
   informe.SetParameterValue("surtio", ValidaUsuario.Name)

   RepComsultaP.CrVConsulta.ReportSource = informe

   SetTableLocation(informe, ConnectionInfo)
   informe.PrintOptions.PaperOrientation = PaperOrientation.Landscape
   informe.PrintOptions.PaperSize = PaperSize.PaperA4

   informe.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, _rutaPDF)
   'oStrem = CType(informe.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), System.IO.MemoryStream)

   'INDICA LA ORIENTACIÓN DE LA HOJA IMPRESA
   informe.PrintOptions.PaperOrientation = PaperOrientation.Landscape
   'INDICA EL TAMAÑO DE LA HOJA
   informe.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
   'INDICA LA PREFERENCIA DE LA IMPRESORA
   informe.PrintToPrinter(1, False, 0, 0)
   'COLOCA EN VERDADERO SI LA IMPRESIÓN FUE CORRECTA
   ImpresoOK = True

   '****************************************************************
   ' Esta parte es el antiguo codigo, estaba marcando error al tratar de obtener la ruta del reporte
   '****************************************************************
   ''ESTABLECE LA CONEXION CON EL REPORTE
   'SetTableLocation(DocOrdenes, ConnectionInfo)
  Catch ex As Exception
   If (UsrTPM = "MANAGER") Then
    MsgBox("Error para manager: " & ex.Message)
   End If
  End Try
 End Sub

 'METODO PARA LA TABLA DE INFORMACIÓN DEL CRYSTAL REPORTS
 Sub SetTableLocation(report As ReportDocument, connectionInfo As ConnectionInfo)
  For Each table As Table In report.Database.Tables
   Dim tableLogOnInfo As TableLogOnInfo = table.LogOnInfo
   tableLogOnInfo.ConnectionInfo = connectionInfo
   table.ApplyLogOnInfo(tableLogOnInfo)
  Next
 End Sub

 Private Sub txtClave_KeyPress(sender As Object, e As KeyPressEventArgs)
  'DETECTA EL ENTER Y REALIZA UNA OPCIÓN
  If Asc(e.KeyChar) = Keys.Enter Then
   'SE ACTIVA EL EVENTO CLIC DEL BOTON ACEPTAR
   btnAceptar.PerformClick()
   frmMostrarOrdenes.Timer1.Enabled = True
  End If
 End Sub

 Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
  'DESHABILITA LOS ELEMENTOS
  txtClave.Enabled = True
  btnAceptar.Enabled = True
  btnCancelar.Enabled = True

  'BUSCA EL ID O CLAVE EXISTE EN LA BASE DE DATOS DEL TPD
  Dim SQLEmpleado As String
  'VARIABLE DE ALMACENAMIENTO DE EMPLEADO
  Dim UserIdSurtidor As String
  Dim NameSurtidor As String

  '-----

  'ALAMACENA LA CONSULTA
  SQLEmpleado = "SELECT * FROM Operacion_Empleado WHERE KeyCode = '" + txtClave.Text + "' AND Frozen = 'Y' AND preCode = '" + txtPreClave.Text + "'"

  Try
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(SQLEmpleado, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
   'RECORRE LA CONSULTA
   If (conexion_universal.rd_s.Read) Then
    UserIdSurtidor = rd_s.Item("KeyCode")
    NameSurtidor = rd_s.Item("Name")
    ValidaUsuario.Frozen = rd_s.Item("Frozen")
    ValidaUsuario.Id_Empleado = rd_s.Item("Id_Empleado")
    ValidaUsuario.KeyCode = rd_s.Item("KeyCode")
    ValidaUsuario.Name = rd_s.Item("Name")
   Else
    'MsgBox("Por que da el error")
    MsgBox("Clave de acceso o la preclave es incorrecta, favor de intentar nuevamente.", MsgBoxStyle.Exclamation, "Alerta de Acceso")
    conexion_universal.rd_s.Close() 'CIERRA EL READE
    conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
    txtPreClave.Focus()
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


  VerificarOrdenLibre(UserIdSurtidor)

  '-----
  'ACTUALIZA LA ORDEN DE VENTA QUE SE VA A SURTIR, EN AL CUAL AGREGA EL STATUS Y QUE EMPLEADO LO SURTE
  SQLEmpleado = "UPDATE Operacion_Orden SET UserId_Surtido = '" + UserIdSurtidor + "', Status = '" + Status + "',PrintTime= GETDATE(), DateUpdate = GETDATE(), UserId_Update = '" + UserIdSurtidor + "' "
  SQLEmpleado &= "WHERE DocNum = '" + DocNumAcceso + "' AND UserId_Surtido IS NULL"

  Try
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(SQLEmpleado, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   'ALMACENA EN LA VARIABLE GLOBAL QUIEN VA A SURTIR LA ORDEN
   ClaveSurtido = UserIdSurtidor

   '-----

  Catch ex As Exception
   MsgBox("Error al Actualizar el Status en la Orden, probablemente ya este en estatus de Surtido: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexioón")
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   CierraDialogAcceso = False
   Me.Close()
   Return
  End Try

  '-----

  If (VerificarOrdenLibre(UserIdSurtidor) = True) Then
   'MANDA A LLAMAR EL METODO DE IMPRESIÓN DEL PEDIDO
   ImprimeNuevoFormato()
  Else
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   CierraDialogAcceso = False
   Me.Close()
   Return
  End If

  '-----

  '-----
  'INSERTA LA ORDEN DE VENTA EN Operacion_Analisis DONDE VAN LOS TIEMPOS DE SURTIDO, EMPACADO Y FINALIZADO
  Dim SDocEntry As String = ""
  Dim SDocNum As String = ""
  Dim SPrintTime As String = ""
  Dim SDocDate As String = ""
  Dim SDocDateO As String = ""
  Dim SStatus As String = ""
  Dim SUserId_Surtido As String = ""
  Dim SCardCode As String = ""
  Dim SCardName As String = ""
  Dim SQLAnalisis As String = ""
  SQLAnalisis = "SELECT T0.DocEntry, T0.DocNum, FORMAT(T0.DocDate, 'yyyy-MM-dd') AS DocDate, FORMAT(T0.PrintTime, 'yyyy-MM-dd hh\:mm') AS PrintTime, FORMAT(GETDATE(), 'yyyy-MM-dd hh\:mm') AS DocDateO, T0.Status, T0.UserId_Surtido, "
  SQLAnalisis &= "T0.CardCode, T0.CardName "
  SQLAnalisis &= "FROM Operacion_Orden T0 "
  SQLAnalisis &= "WHERE DocNum = " + DocNumAcceso + " AND T0.DocNum NOT IN (SELECT DocNum FROM Operacion_Analisis) "

  Try
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(SQLAnalisis, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader
   'VALIDA QUE TRAIGA DATOS
   If (conexion_universal.rd_s.Read) Then
    SDocEntry = conexion_universal.rd_s.Item("DocEntry")
    SDocNum = conexion_universal.rd_s.Item("DocNum")
    SDocDate = conexion_universal.rd_s.Item("DocDate")
    SPrintTime = conexion_universal.rd_s.Item("PrintTime")
    SDocDateO = conexion_universal.rd_s.Item("DocDateO")
    SStatus = conexion_universal.rd_s.Item("Status")
    SUserId_Surtido = conexion_universal.rd_s.Item("UserId_Surtido")
    SCardCode = conexion_universal.rd_s.Item("CardCode")
    SCardName = conexion_universal.rd_s.Item("CardName")
   Else
    MsgBox("La orden ya se encuentra en Estatus [ Surtiendo ], favor de seleccionar una orden Nueva. ", MsgBoxStyle.Exclamation, "Alerta de Dato")
    conexion_universal.rd_s.Close() 'CIERRA EL READE
    conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
    'ALMACENA VERDADERO PARA CERRAR EL DIALOG SIN MENSAJE
    CierraDialogAcceso = False
    Return
   End If
   'CIERRA EL READER
   conexion_universal.rd_s.Close()
   'CIERRA LA CONEXION
   conexion_universal.conexion_uni.Close()
   'APERTURA LA CONEXION
   conexion_universal.conexion_uni.Open()
   SQLAnalisis = ""
   'INSERTA EL REGISTRO EL REGISTRO
   SQLAnalisis = "INSERT INTO Operacion_Analisis (DocEntry, DocNum, DocDate, PrintTime, GetTime, Status, Pump, CardCode, CardName) VALUES ( "
   SQLAnalisis &= "'" + SDocEntry + "', '" + SDocNum + "',  '" + SDocDate + "', '" + SPrintTime + "', '" + SDocDateO + "', '" + SStatus + "', '" + SUserId_Surtido + "', '" + SCardCode + "', '" + SCardName + "' )"
   'ALMACENA LA CONSULTA
   conexion_universal.slq_s = New SqlCommand(SQLAnalisis, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   'ALMACENA EN LA VARIABLE GLOBAL QUIEN VA A SURTIR LA ORDEN

   '-----

  Catch ex As Exception
   MsgBox("Error al Insertar analisis de surtido: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexioón")
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   'ALMACENA VERDADERO PARA CERRAR EL DIALOG SIN MENSAJE
   CierraDialogAcceso = False
   Return
  End Try

  '------

  'VALIDA SI EL DOCUMENTO SE IMPRIMIO CORRECTAMENTE
  If ImpresoOK = True Then
   MsgBox("Documento Impreso y listo para Surtirse.", MsgBoxStyle.Information, "Operación procesada")
  Else
   MsgBox("Error al imprimir la Orden, intente imprimir manualmente el documento. Esto no impide iniciar surtimiento de la Orden.", MsgBoxStyle.Exclamation, "Impresión incorrecta")
   ImpresoOK = False
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
  Status = ""

 End Sub

 Private Function VerificarOrdenLibre(Surtidor As String)
  'Verifico si no han tomado esta orden desde la HH
  Dim PreSQLAnalisis As String = ""
  Dim UserId_Surtido As String = ""
  PreSQLAnalisis = "SELECT CASE WHEN UserId_Surtido IS NULL THEN '' ELSE UserId_Surtido END UserId_Surtido "
  PreSQLAnalisis &= "FROM Operacion_Orden "
  PreSQLAnalisis &= "WHERE DocNum = " + DocNumAcceso

  Try
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()
   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(PreSQLAnalisis, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader
   'VALIDA QUE TRAIGA DATOS
   If (conexion_universal.rd_s.Read) Then
    UserId_Surtido = conexion_universal.rd_s.Item("UserId_Surtido")
    If (UserId_Surtido <> "" And UserId_Surtido <> Surtidor) Then
     MsgBox("La orden ya se encuentra en Estatus [ Surtiendo ], favor de seleccionar una orden Nueva. ", MsgBoxStyle.Exclamation, "Alerta de Dato")
     conexion_universal.rd_s.Close() 'CIERRA EL READE
     conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
     'ALMACENA VERDADERO PARA CERRAR EL DIALOG SIN MENSAJE
     CierraDialogAcceso = False
     Me.Close()
     Return False
    End If
   End If
  Catch ex As Exception
   MsgBox("Error al Insertar analisis de surtido: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   'ALMACENA VERDADERO PARA CERRAR EL DIALOG SIN MENSAJE
   CierraDialogAcceso = False
   Return False
  End Try
  Return True
 End Function


 Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
  'INICIALIZA EN NADA LA VARIABLE DE TITULO DE ACCESO
  TituloAcceso = ""
  StatusAcceso = ""
  Status = ""
  'CIERRA EL FORMULARIO
  Me.Close()
 End Sub



 Sub ImprimeNuevoFormato()
  Dim Pedido As String = DocNumAcceso
  Dim Surtidor As String = ValidaUsuario.Name
  Try

   Dim informe As New PedidoArtAlmacen
   RepComsultaP.MdiParent = Inicio

   'CONEXION A LA BASE DE DATOS
   Dim tInfo As TableLogOnInfo = New TableLogOnInfo()
   Dim ConnectionInfo As ConnectionInfo = tInfo.ConnectionInfo

   ConnectionInfo.Password = cPassword
   ConnectionInfo.UserID = conexion_universal.cUserID
   ConnectionInfo.ServerName = conexion_universal.cServerName ' // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS
   ConnectionInfo.DatabaseName = conexion_universal.cDatabaseNameSAP

   informe.SetParameterValue("DocKey@", Pedido)
   informe.SetParameterValue("ObjectId@", 17)
   informe.SetParameterValue("surtio", Surtidor)

   RepComsultaP.CrVConsulta.ReportSource = informe

   SetTableLocation(informe, ConnectionInfo)
   informe.PrintOptions.PaperOrientation = PaperOrientation.Landscape
   informe.PrintOptions.PaperSize = PaperSize.PaperA4


   'Return

   informe.PrintToPrinter(1, False, 0, 0)
   ImpresoOK = True

  Catch ex As Exception
   MessageBox.Show(ex.ToString, "Error al imprimir PDF surtir")
  End Try

  'ACTUALIZA TOTAL DE IMPRESIONES
  'VARIABLE ALMACENA TOTAL DE IMPRESIONES
  Dim TotalPrint As Integer
  'VARIABLE DE CONSULTA
  Dim SQLUpdateImp
  'CAPTURA EL ERROR DE CONSULTA O CONEXION
  Try
   'CONECTA A LA BASE DE DATOS
   conexion_universal.conectar()

   'ALMACENA LA CONSULTA DE BUSQUEDA
   SQLUpdateImp = "SELECT IIF(Printed IS NULL, 0, Printed) AS Printed FROM " + BaseDatosTPD + ".dbo.Operacion_Orden WHERE DocNum = '" + Pedido + "' "

   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(SQLUpdateImp, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
   'RECORRE LA CONSULTA
   If (conexion_universal.rd_s.Read) Then
    TotalPrint = CInt(rd_s.Item("Printed").ToString) + 1
   End If
   conexion_universal.rd_s.Close() 'CIERRA EL READE
   conexion_universal.conexion_uni.Close() 'CIERRA LA CONEXION

   'INICIALIZA LA VARIABLE EN NADA
   SQLUpdateImp = ""

   'APERTURA DE LA CONEXION
   conexion_universal.conexion_uni.Open()

   'ALMACENA LA CONSULTA PARA LA ACTUALIZACIÓN
   SQLUpdateImp = "UPDATE " + BaseDatosTPD + ".dbo.Operacion_Orden SET Printed = '" + TotalPrint.ToString + "' "
   SQLUpdateImp &= "WHERE DocNum = '" + Pedido + "' "

   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
   conexion_universal.slq_s = New SqlCommand(SQLUpdateImp, conexion_universal.conexion_uni)
   'EJECUTA LA CONSULTA
   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar

   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION

   '-----
  Catch ex As Exception
   MsgBox("Error de Consulta o Conexion al Actualziar las Impresiones: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
   conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
   Return
  End Try
 End Sub


End Class