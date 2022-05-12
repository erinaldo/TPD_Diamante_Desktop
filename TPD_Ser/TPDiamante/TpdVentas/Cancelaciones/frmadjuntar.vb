Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Text
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.Json
Imports MySql.Data.MySqlClient.Memcached

Public Class frmadjuntar
  Private Sub btnadjuntar_Click(sender As Object, e As EventArgs) Handles btnadjuntar.Click
    'DECLARACION DE VARIABLE DE REPORTE Y INSTANCIA DEL MISMO
    Dim DocFacturas As ReportDocument
    DocFacturas = New ReportDocument()
    Dim DocNum As String '//VARIABLES PARA OBTENER LOS NUMEROS DE DOCUMENTOS
    Dim DocKey = String.Empty
    Dim EDocNum As String = "" '//ALAMACENA LA CADENA ELECTRONICA DEL DATA GRID
    Dim E_Mail As String = ""
    Dim CardName As String = ""
    Dim _rutaPDF As String '// ALMACENA LA RUTA DEL PDF
    Dim _rutaXML As String '//ALAMACENA LA RUTA DEL XML
    Dim correo1 As String = "" '//VARIABLE PARA ALMACENAR LOS  CORREOS
    Dim fecha11082018 As DateTime = Convert.ToDateTime("2018-08-11").Date '//VARIABLES PARA VALIDAR QUE FORMATO CREAR
    Dim fecha01082018 As DateTime = Convert.ToDateTime("2018-08-01").Date
    Dim fechainvoice As DateTime = Convert.ToDateTime("2019-02-11").Date
    Dim fechaMigracionAgo2020 As DateTime = Convert.ToDateTime("2020-08-23").Date

    Dim DocDate As DateTime = Convert.ToDateTime("2018-10-16").Date '//VARIABLES PARA VALIDAR QUE FORMATO CREAR

    '//VARIABLE PARA LA EL CORREO
    Dim msg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()

    DocNum = "1027989"
    DocKey = "96912"

    E_Mail = "asistemas@tractopartesdiamante.com.mx"

    '//VALIDA LA FECHA PARA SABER QUE FORMATO EJECUTAR

    If (DocDate <= fecha11082018) Then
      DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\Factura 3_3 19ABR2018.rpt") ' //RUTA DEL ARCHIVO .rpt
    ElseIf (DocDate > fecha11082018 And DocDate < fechainvoice) Then
      DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\Factura 3.3-93_5.rpt") ' //RUTA DEL ARCHIVO .rpt
    ElseIf (DocDate > fechainvoice And DocDate < fechaMigracionAgo2020) Then  'Formato pre migracion AGO-2020
      DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\Factura 3.3-93_6_AddOn_DLL.rpt") ' //RUTA DEL ARCHIVO .rpt
    Else
      DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\Factura 3.3-93_6AddOn9NF2020.rpt") ' //RUTA DEL ARCHIVO .rpt
    End If

    'If (DocDate <= fecha11082018) Then
    '  DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\Factura 3_3 19ABR2018.rpt") ' //RUTA DEL ARCHIVO .rpt
    'Else
    '  DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\Factura 3.3-93_5.rpt") ' //ruta del archivo .rpt
    'End If

    '//PARAMETROS DE CONEXION PARA EL RPT
    Dim tInfo As TableLogOnInfo = New TableLogOnInfo()
    Dim ConnectionInfo As ConnectionInfo = tInfo.ConnectionInfo

    ConnectionInfo.Password = conexion_universal.cPassword
    ConnectionInfo.UserID = conexion_universal.cUserID
    ConnectionInfo.ServerName = conexion_universal.cServerName ' // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS
    ConnectionInfo.DatabaseName = conexion_universal.cDatabaseNameSAP

    '//PASA EL PARAMETRO AL ARCHIVO RPT (DocEntry)
    DocFacturas.SetParameterValue("DocKey@", DocKey)

    'ESTABLE LA CONEXION CON EL REPORTE
    SetTableLocation(DocFacturas, ConnectionInfo)

    'ALMACENA RUTA Y NOMBRE DEL ARCHIVO
    _rutaPDF = DocNum + ".pdf"

    '//ALMACENA RUTA DE XML
    _rutaXML = "\\" & conexion_universal.RutaReportes & "\b1_shr\xml\TPD051215UZ1\" + "2018-10\C-1824\IN\09EBCFF3-4473-4BE0-9C79-5AF7934840C4.xml"

    '//GENERA PDF EN CARPETA TEMPORAL
    Try
      '//ALMACENA EL PDF EN LA RUTA TEMPORAL
      DocFacturas.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, _rutaPDF)
      'createPDF_OK = True
    Catch ex As Exception
      MessageBox.Show("No se pudo crear el archivo PDF: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try

    EDocNum = "\\" & conexion_universal.RutaReportes & "\b1_shr\xml\TPD051215UZ1\2018-10\C-1824\IN\09EBCFF3-4473-4BE0-9C79-5AF7934840C4.xml"
    msg.To.Add(E_Mail)
    '//ADJUNTAR LOS ARCHIVOS PDF'S Y XML'S
    Try
      Dim ArchiveRutaPDF As Attachment = New System.Net.Mail.Attachment(_rutaPDF)
      msg.Attachments.Add(ArchiveRutaPDF)
      If EDocNum <> "" Then
        Dim ArchiveRutaXML As Attachment = New System.Net.Mail.Attachment(_rutaXML)
        msg.Attachments.Add(ArchiveRutaXML)
      End If '// VALIDA Then SI EL XML NO ES NULO

    Catch ex As Exception
      MessageBox.Show("Error al adjuntar el archivo PDF: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try

    '//COLOCA LA PRIORIDAD DEL CORREO
    msg.Priority = System.Net.Mail.MailPriority.High '//Prioridad
    '//COLOCA EL ASUNTO DEL CORREO
    msg.Subject = "Facturación Electrónica"
    '//VISTA DE TODO EL CONTENIDO DEL CORREO EN HTML
    Dim htmlview As AlternateView = AlternateView.CreateAlternateViewFromString("<img src= cid:companylogo width='250' height='40'><p>Atencíón:</p><p>" + "Uriel" + "</p><p>Estimado Cliente:</p><p>Por medio de la presente le informamos que  TRACTO PARTES DIAMANTE DE PUEBLA SA DE CV, le ha enviado un nuevo Comprobante Fiscal Digital</p><p>Este mensaje es un envío automático, Favor de No Responder</p><p>Si tiene alguna duda, le agradecemos contactarnos a la siguiente direccion de correo: info@tractopartesdiamante.com.mx</p><p>Saludos Cordiales.</p>", Nothing, "text/html")
    '//OBTIENE EL LOGO DE LA EMPRESA
    Dim logo As LinkedResource = New LinkedResource("\\\\" & conexion_universal.RutaReportes & "\\b1_shr\\TPD\\Facturas\\Facturas\\Facturas\\tpd.png")
    logo.ContentId = "companylogo"
    '//LO AGREGA AL CUERPO DEL CORREO
    htmlview.LinkedResources.Add(logo)
    '//msg.AlternateViews.Add(planview)
    msg.AlternateViews.Add(htmlview)

    '//DIRECCION DE EMISOR DEL CORREO
    msg.From = New System.Net.Mail.MailAddress("facturacion@tractopartesdiamante.com.mx", "Tracto Partes Diamante de Puebla") '//Remitente

    '//SE CREA EL CLIENTE DE SMTP DEL CORREO
    Dim Client As SmtpClient = New System.Net.Mail.SmtpClient()
    '//ESPECIFICA EL SERVIDOR DEL HOST ENVIANTE
    Client.Host = "mail.tractopartesdiamante.com.mx"
    '//ASIGNA AL CLIENTE  EL PUERTO 26 DE USO
    Client.Port = 26
    '//client.EnableSsl = true; -- no tenemos SSL
    '//QUE EL EMISOR SIEMPRE SOLICITE LA CONTRASEÑA
    Client.UseDefaultCredentials = True
  '//client.Credentials = New NetworkCredential("CorreoRemitente", "Contraseña")
  Client.Credentials = New NetworkCredential("facturacion@tractopartesdiamante.com.mx", "FaCt017nKcrWfo%") ' //EMISOR Y CONTRASEÑA DEL CORREO
  '//CIERRA EL DOCUEMTNO DE RPT
  DocFacturas.Close()

    Try
      '//REALIZA EL ENVIO DEL CORREO
      Client.Send(msg)
      '//CIERRA LA APERTURA DEL CLIENTE DEL ENVIO DE CORREO
      Client.Dispose()
      '//CIERRA EL CUERPO HTML DEL CORREO
      msg.Dispose()
      MessageBox.Show("Factura enviada correctamente.", "Error de envio", MessageBoxButtons.OK, MessageBoxIcon.Information)
    Catch ex As System.Net.Mail.SmtpException
      MessageBox.Show("Error al enviar el correo, : " + ex.Message, "Error de envio", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub

  Sub SetTableLocation(report As ReportDocument, connectionInfo As ConnectionInfo)
    'Throw New NotImplementedException()
    For Each table As Table In report.Database.Tables
      Dim tableLogOnInfo As TableLogOnInfo = table.LogOnInfo
      tableLogOnInfo.ConnectionInfo = connectionInfo
      table.ApplyLogOnInfo(tableLogOnInfo)
    Next
  End Sub
End Class