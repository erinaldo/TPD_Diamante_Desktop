Imports System.Data.SqlClient

Public Module conexion_universal
  'Ruta para los reportes
  Public RutaReportes As String

  'VARIABLES DE CONEXION
  Public cPassword As String
  Public cUserID As String
  Public cServerName As String
  Public cDatabaseNameSAP As String
  Public cDatabaseNameTMP As String
  Public cDatabaseNameZPRUE2018 As String
  Public cDatabaseNameSBO_Diamante As String
  Public cDatabaseNameZPRUE2019 As String
  Public cDatabaseNameTPM20FEB19 As String

  Public cConstanteTPM
  'Public dt As DataTable
  'PARA SAP
  Public CadenaSQLSAP As String
  'PARA TPD
  Public CadenaSQL As String
  'PARA SAP DE PRUEBAS
  Public CadenaSQLPRU As String
  'PARA SBO-Diamante-productiva
  Public CadenaSBO_Diamante As String
  'PARA Pruebas ZPRUEBAS16ABR2019
  Public StrConPru As String
  'PARA pruebas TPM09FEB2019
  Public StrTpmPrueba As String
  Public CadenaSQLTPMPru As String

  Public conexion_uni As SqlConnection
  Public conexion_uni_sap As SqlConnection
  Public conexion_uni_pru As SqlConnection
  Public slq_s As SqlCommand
  Public slq_con As SqlCommand
  Public sql_tem As SqlCommand
  Public sql_up As SqlCommand
  Public rd_s As SqlDataReader
  Public rd_tem As SqlDataReader
  Public rd_up As SqlDataReader
  Public rd_con As SqlDataReader
  'Public Lista1 As New ArrayList

  Sub New()
    cUserID = My.Settings.gUserID
    cDatabaseNameZPRUE2018 = My.Settings.gDatabaseNameZPRUE2018
    cDatabaseNameSBO_Diamante = My.Settings.gDatabaseNameSBO_Diamante
    cDatabaseNameZPRUE2019 = My.Settings.gDatabaseNameZPRUE2019
    cDatabaseNameTPM20FEB19 = My.Settings.gDatabaseNameTPM20FEB19
    If My.Settings.AMBIENTE_PRODUCCION Then
      cPassword = My.Settings.gPasswordProduccion
      cServerName = My.Settings.gServerNameProduccion
      cDatabaseNameSAP = My.Settings.gDatabaseNameSAPProduccion
      cDatabaseNameTMP = My.Settings.gDatabaseNameTMPProduccion
      RutaReportes = My.Settings.gRutaReportesProduccion
    Else
      cPassword = My.Settings.gPasswordPruebas
      cServerName = My.Settings.gServerNamePruebas
      cDatabaseNameSAP = My.Settings.gDatabaseNameSAPPruebas
      cDatabaseNameTMP = My.Settings.gDatabaseNameTMPPruebas
      RutaReportes = My.Settings.gRutaReportesPruebas
    End If

    cConstanteTPM = "Server=" & cServerName & "; Database=" & cDatabaseNameTMP & "; User id=" & cUserID & "; Password=" & cPassword

    'Public dt As DataTable
    'PARA SAP
    CadenaSQLSAP = "Data Source=" & cServerName & "; Initial Catalog=" & cDatabaseNameSAP & "; User Id=" & cUserID & "; Password=" & cPassword & ";"
    'PARA TPD
    CadenaSQL = "Data Source=" & cServerName & "; Initial Catalog=" & cDatabaseNameTMP & "; User Id=" & cUserID & "; Password=" & cPassword & ";"
    'PARA SAP DE PRUEBAS
    CadenaSQLPRU = "Data Source=" & cServerName & "; Initial Catalog=" & cDatabaseNameZPRUE2018 & "; User Id=" & cUserID & "; Password=" & cPassword & ";"
    'PARA SBO-Diamante-productiva
    CadenaSBO_Diamante = "Data Source=" & cServerName & "; Initial Catalog=" & cDatabaseNameSBO_Diamante & "; User Id=" & cUserID & "; Password=" & cPassword & ";"
    'PARA Pruebas ZPRUEBAS16ABR2019
    StrConPru = "Data Source=" & cServerName & "; Initial Catalog=" & cDatabaseNameZPRUE2019 & "; User Id=" & cUserID & "; Password=" & cPassword & ";"
    'PARA pruebas TPM09FEB2019
    StrTpmPrueba = "Data Source=" & cServerName & "; Initial Catalog=" & cDatabaseNameTPM20FEB19 & "; User Id=" & cUserID & "; Password=" & cPassword & ";"
    CadenaSQLTPMPru = "Data Source=" & cServerName & "; Initial Catalog=" & cDatabaseNameTPM20FEB19 & "; User Id=" & cUserID & "; Password=" & cPassword & ";"
  End Sub

  'REALIZA LA CONEXIÓN A LA BASE DE DATOS TPD
  Sub conectar()
    Try
      conexion_uni = New SqlConnection(CadenaSQL)
      conexion_uni.Open()
    Catch ex As Exception
      MsgBox("Error al realizar la conexion SAP: " & ex.ToString, MsgBoxStyle.Critical, "Error de conexión")
      conexion_uni.Close()
    End Try
  End Sub
  'CIERRA LA CONEXION 
  Sub cerrar_conectar()
    Try
      'CIERRA LA CONEXION ABIERTA
      conexion_uni.Close()
    Catch ex As Exception
      MsgBox("Error al cerrar la conexion de TPD: " & ex.ToString, MsgBoxStyle.Critical, "Error de conexión")
      Return
    End Try
  End Sub

  '---------

  'REALIZA LA CONEXIÓN A LA BASE DE DATOS SAP
  Sub conectar_sap()
    Try
      conexion_uni_sap = New SqlConnection(CadenaSQLSAP)
      conexion_uni_sap.Open()
    Catch ex As Exception
      MsgBox("Error al realizar la conexion a SAP: " & ex.ToString, MsgBoxStyle.Critical, "Error de conexión")
      conexion_uni_sap.Close()
    End Try
  End Sub
  'CIERRA LA CONEXION 
  Sub cerrar_conectar_sap()
    Try
      'CIERRA LA CONEXION ABIERTA
      conexion_uni_sap.Close()
    Catch ex As Exception
      MsgBox("Error al cerrar la conexion de SAP: " & ex.ToString, MsgBoxStyle.Critical, "Error de conexión")
      Return
    End Try
  End Sub

  '----- 

  'ABRIR CONEXION Y CERRAR LA DE PRUEBAS

  'REALIZA LA CONEXIÓN A LA BASE DE DATOS SAP
  Sub conectar_pru()
    Try
      conexion_uni_pru = New SqlConnection(CadenaSQLTPMPru)
      conexion_uni_pru.Open()
    Catch ex As Exception
      MsgBox("Error al realizar la conexion a SAP Prueba: " & ex.ToString, MsgBoxStyle.Critical, "Error de conexión")
      conexion_uni_sap.Close()
    End Try
  End Sub
  'CIERRA LA CONEXION 
  Sub cerrar_conectar_pru()
    Try
      'CIERRA LA CONEXION ABIERTA
      conexion_uni_pru.Close()
    Catch ex As Exception
      MsgBox("Error al cerrar la conexion de SAP Prueba: " & ex.ToString, MsgBoxStyle.Critical, "Error de conexión")
      Return
    End Try
  End Sub
End Module
