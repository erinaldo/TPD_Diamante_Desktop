'VARIABLE PARA LAS CONEXIONES
Imports System.Data.SqlClient
'LIBRERIA REQUERIDA PARA CARGAR EL CRYSTAL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmAccesoEmpaque
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
  Dim ResultadoDetalleEmpaque As DataView
  Dim DvDetalle_Estatus As DataView

  Private Sub frmAccesoEmpaque_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    'VARIABLE QUE ALMACENA CONSULTA DE STATUS
    Dim SQLStatus As String = ""

    'COLOCA EL NOMBRE DEL FORMULARIO SEGUN LO QUE TRAIGA EN EL DE SURTIR ORDENES
    Me.Text = TituloAcceso

    '-----
    'ALAMACENA LA CONSULTA DE SOLO OBTENER EL STATUS QUE CORRESPONDA
    SQLStatus = "SELECT Status, StatusName2 FROM Operacion_Status WHERE StatusName = '" + StatusAcceso + "' AND Activo = 'Y' "

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

  Private Sub txtClave_TextChanged(sender As Object, e As EventArgs) Handles txtClave.TextChanged

  End Sub

  Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
    'INICIALIZA EN NADA LA VARIABLE DE TITULO DE ACCESO
    TituloAcceso = ""
    StatusAcceso = ""
    Status = ""
    'CIERRA EL FORMULARIO
    Me.Close()
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
    Dim idUsuario As Integer
    Dim NameSurtidor As String
  '-----
  'ALAMACENA LA CONSULTA
  'SQLEmpleado = "SELECT  * FROM Operacion_Empleado WHERE KeyCode = '" + txtClave.Text + "' AND Frozen = 'Y' AND preCode = '" + txtPreClave.Text + "'"
  SQLEmpleado = "SELECT  * FROM Operacion_Empleado WHERE KeyCode = '" + txtClave.Text + "' AND Frozen = 'Y'"

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
        idUsuario = rd_s.Item("Id_Empleado")
        NameSurtidor = rd_s.Item("Name")
      Else
    'MsgBox("Por que da el error")
    MsgBox("Clave de acceso es incorrecta, favor de intentar nuevamente.", MsgBoxStyle.Exclamation, "Alerta de Acceso")
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
    '-----  

    'ACTUALIZA LA ORDEN DE VENTA QUE SE VA A SURTIR, EN AL CUAL AGREGA EL STATUS Y QUE EMPLEADO LO SURTE
    If Actualizar_Operacion_Entrega("UserId_Empacado", idUsuario, "EP", "Empacando", DocNumEmpacar, "UserId_Update", idUsuario) = False Then
      CierraDialogAcceso = False
      Return
    End If

    'SQLEmpleado = "UPDATE Operacion_Entrega  SET UserId_Empacado = '" + UserIdSurtidor + "', Status = 'EP', DateUpdate = GETDATE(), UserId_Update = '" + UserIdSurtidor + "', Action='Empacando' "
    '    SQLEmpleado &= "WHERE DocEntry = '" + DocNumEmpacar + "' "
    '    Try
    '        'CONECTA A LA BASE DE DATOS
    '        conexion_universal.conectar()
    '        'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
    '        conexion_universal.slq_s = New SqlCommand(SQLEmpleado, conexion_universal.conexion_uni)
    '        'EJECUTA LA CONSULTA
    '        conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
    '        conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
    '        'ALMACENA EN LA VARIABLE GLOBAL QUIEN VA A SURTIR LA ORDEN
    '        ClaveSurtido = UserIdSurtidor
    '        '-----        
    '    Catch ex As Exception
    '        MsgBox("Error al Actualizar el Status en la Orden: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexioón")
    '        conexion_universal.rd_s.Close() 'CIERRA EL READE
    '        conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
    '        CierraDialogAcceso = False
    '        Return
    '    End Try


    ''ACTUALIZA EL ESTATUS A EMPACANDO DESCOMENTAR CUANDO SE PASE A PRODUCTIVO 
    'Try
    '    'VARIABLE DE CADENA DE SQL
    '    Dim SQLOrdenes As String
    '    'VARIABLES DE CONEXION DE LLENADO
    '    Dim cmd As SqlCommand
    '    Dim cnn As SqlConnection = Nothing
    '    SQLOrdenes = "Update Operacion_Orden set Action='Empacando', Status'E' where DocEntry='" + DocNumAcceso + "'"
    '    cnn = New SqlConnection(StrConPru)
    '    'ALMACENA LA CONSULTA EN UN COMMAND SQL
    '    cmd = New SqlCommand(SQLOrdenes, cnn)
    '    'CONVIERTE EL TEXTO EN CONSULTA
    '    cmd.CommandType = CommandType.Text
    '    'APERTURA LA CONEXION
    '    cnn.Open()
    '    cmd.ExecuteNonQuery()
    '    'CIERRA EL COMMAND DE SQL
    '    cmd.Connection.Close()
    '    'CIERRA LA CONEXION
    '    cnn.Close()
    'Catch ex As Exception
    '    MsgBox("Error: Ocurrio un Error al Actualizar el estatus a Empacando " + ex.ToString)
    'End Try


    'Modificado por Ivan Gonzalez se omitio la impresion de documento
    '-----
    'MANDA A LLAMAR EL METODO DE IMPRESIÓN DEL PEDIDO
    'MImprimeFormato(DocNumEmpacar)

    ''VALIDA SI EL DOCUMENTO SE IMPRIMIO CORRECTAMENTE
    'If ImpresoOK = True Then
    '    MsgBox("Documento Impreso y listo para comenzar el empaque.", MsgBoxStyle.Information, "Operación procesada")
    'Else
    '    MsgBox("Error al imprimir la Orden de empaque, intente imprimir manualmente el documento. Esto no impide iniciar el empacado.", MsgBoxStyle.Exclamation, "Impresión incorrecta")
    '    ImpresoOK = False
    'End If

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
  Private Sub txtClave_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtClave.KeyPress
    'DETECTA EL ENTER Y REALIZA UNA OPCIÓN
    If Asc(e.KeyChar) = Keys.Enter Then
      'SE ACTIVA EL EVENTO CLIC DEL BOTON ACEPTAR
      btnAceptar.PerformClick()
    End If
  End Sub
  'METODO PARA LA TABLA DE INFORMACIÓN DEL CRYSTAL REPORTS
  Sub SetTableLocation(report As ReportDocument, connectionInfo As ConnectionInfo)
    For Each table As Table In report.Database.Tables
      Dim tableLogOnInfo As TableLogOnInfo = table.LogOnInfo
      tableLogOnInfo.ConnectionInfo = connectionInfo
      table.ApplyLogOnInfo(tableLogOnInfo)
    Next
  End Sub
  Sub MImprimeFormato(entrega As String)

    Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      Dim DsOrdenes = New DataSet
      Dim da As SqlDataAdapter
      SQLOrdenes = "Select Distinct ItmsGrpNam ,T5.DocNum,t5.CardCode,T5.CardName,T5.DocDate,t5.LicTradNum , t5.Address, SlpName, T1.BaseRef, "
      SQLOrdenes &= "T1.ItemCode AS Codigo,Dscription AS Descripcion,(Select Surtido From  TPM.dbo.Operacion_Detalle_Entrega where T1.VisOrder=LineNum and DocEntry=t2.DocEntry )AS Cantidad, "
      SQLOrdenes &= "Round(t3.SWeight1,2,0) as PesoxUni ,ROUND(((Select Surtido From  TPM.dbo.Operacion_Detalle_Entrega where T1.VisOrder=LineNum and DocEntry=t2.DocEntry )*t3.SWeight1),2,1)as PesoxArt,t7.TrnspName  "
      SQLOrdenes &= "  ,T10.Weight as Peso,(Select Name From TPM.dbo.Operacion_Empleado TT1 INNER JOIN TPM.DBO.Operacion_Entrega TT2 on TT2.UserId_Surtido =TT1.KeyCode where tt2.DocEntry=" + entrega + " ) AS Surtidor "
      SQLOrdenes &= ",(Select Name From TPM.dbo.Operacion_Empleado TT1 INNER JOIN TPM.DBO.Operacion_Entrega TT2 on TT2.UserId_Empacado =TT1.KeyCode where tt2.DocEntry=" + entrega + ") AS Empacador "
      SQLOrdenes &= "From DLN1 T1 LEFT JOIN TPM.dbo.Operacion_Detalle_Entrega T2 ON T1.DocEntry=T2.DocEntry and t1.baseRef =T2.DocNum  "
      SQLOrdenes &= "LEFT JOIN OITM T3 on T1.ItemCode=T3.ItemCode   LEFT JOIN OITB T4 on T3.ItmsGrpCod=T4.ItmsGrpCod  LEFT JOIN ODLN t5 ON T1.DocEntry=T5.DocEntry "
      SQLOrdenes &= "LEFT JOIN OSLP T6 ON T1.SlpCode=T6.SlpCode LEFT JOIN OSHP T7 ON T7.TrnspCode=t5.TrnspCode "
      SQLOrdenes &= "LEFT Join TPM.dbo.Operacion_Entrega  T8 ON T8.DocEntry=T2.DocEntry AND T8.DocNum=T2.DocNum "
      SQLOrdenes &= " LEFT JOIN ODLN T10 ON T1.DocEntry=T10.DocEntry"
      SQLOrdenes &= " WHERE  T5.DocNum=" + entrega + " AND T4.ItmsGrpCod<>150 Order By ItmsGrpNam "
      ''ALAMACENA LA CONSULTA
      'SQLOrdenes = "Select Distinct ItmsGrpNam ,T5.DocNum,t5.CardCode,T5.CardName,T5.DocDate,t5.LicTradNum , t5.Address, SlpName, T1.BaseRef, "
      'SQLOrdenes &= "T1.ItemCode AS Codigo,Dscription AS Descripcion,(Select Surtido From  TPM09FEB2019.dbo.Operacion_Detalle_Entrega where T1.LineNum=LineNum and DocEntry=t2.DocEntry )AS Cantidad, "
      'SQLOrdenes &= "Round(t3.SWeight1,2,0) as PesoxUni ,ROUND(((Select Surtido From  TPM09FEB2019.dbo.Operacion_Detalle_Entrega where T1.LineNum=LineNum and DocEntry=t2.DocEntry )*t3.SWeight1),2,1)as PesoxArt,t7.TrnspName  "
      'SQLOrdenes &= "From DLN1 T1 LEFT JOIN TPM09FEB2019.dbo.Operacion_Detalle_Entrega T2 ON T1.DocEntry=T2.DocEntry and t1.baseRef =T2.DocNum  "
      'SQLOrdenes &= "LEFT JOIN OITM T3 on T1.ItemCode=T3.ItemCode   LEFT JOIN OITB T4 on T3.ItmsGrpCod=T4.ItmsGrpCod  LEFT JOIN ODLN t5 ON T1.DocEntry=T5.DocEntry "
      'SQLOrdenes &= "LEFT JOIN OSLP T6 ON T1.SlpCode=T6.SlpCode LEFT JOIN OSHP T7 ON T7.TrnspCode=t5.TrnspCode"
      'SQLOrdenes &= " WHERE  T2.Docentry=" + entrega + " AND T4.ItmsGrpCod<>150"
      cnn = New SqlConnection(StrCon)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      cmd = New SqlCommand(SQLOrdenes, cnn)
      'CONVIERTE EL TEXTO EN CONSULTA
      cmd.CommandType = CommandType.Text
      'APERTURA LA CONEXION
      cnn.Open()
      'INSTANCIA UN ADAPTER
      da = New SqlDataAdapter
      'ALMACENA EL COMMAND DE SQL EN EL ADAPTER
      da.SelectCommand = cmd
      'LO EJECUTA CON LA CONEXION
      da.SelectCommand.Connection = cnn
      'TIEMPO DE ESPERA DE LA CONEXION
      da.SelectCommand.CommandTimeout = 10000
      'EJECUTA LA CONSULTA
      cmd.ExecuteNonQuery()
      'CIERRA EL COMMAND DE SQL
      cmd.Connection.Close()
      'CIERRA LA CONEXION
      cnn.Close()
      'LLENA EL ADAPTER A UN DATA SET
      da.Fill(DsOrdenes)
      'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
      ResultadoDetalleEmpaque = New DataView
      'ALMACENA EN DATA SET DE MODO TABLA
      ResultadoDetalleEmpaque.Table = DsOrdenes.Tables(0)
      'Crea un nuevo DataView         
      DvDetalle_Estatus = New DataView
      'ALMACENA EN DATA SET DE MODO TABLA
      DvDetalle_Estatus.Table = DsOrdenes.Tables(0)
      'Se crea un informe de cristal Reports
      Dim informe As New ReporteEmpaque
      RepComsultaP.MdiParent = Inicio
      informe.SetDataSource(DvDetalle_Estatus)
      RepComsultaP.CrVConsulta.ReportSource = informe
      informe.PrintToPrinter(1, False, 0, 0)
    Catch ex As Exception

      MsgBox("Error: " + ex.ToString)
    End Try
    ImpresoOK = True
  End Sub

End Class