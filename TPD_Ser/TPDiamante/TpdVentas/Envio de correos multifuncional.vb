Imports System.Net
Imports System.Net.Mail
Imports System.Text

Public Class Envio_de_correos_multifuncional




    Private Sub Envio_de_correos_multifuncional_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rbCliente.Checked = True
        LlenaClientes()


        'txtsunto.Text = ComboBox1.DisplayMember






    End Sub

    'Llenar combo con clientes con acceso a sitio web
    Private Sub LlenaClientes()

        Dim ConsutaLista As String
        ' Dim codigoCliente As String

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

            Dim dvClientes As New DataView
            Dim DSetTablas As New DataSet
            ConsutaLista = "select CardCode, Cardname,U_BXP_CorreoPrincipal,LictradNum from OCRD where U_BXP_AccesoWeb = 'Y' Order by Cardname"
            Dim daClientes As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            'Dim DSetTablas As New DataSet
            daClientes.Fill(DSetTablas, "Clientes")

            Dim fila As Data.DataRow
            ' Dim fila2 As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            fila = DSetTablas.Tables("Clientes").NewRow
            'fila2 = DSetTablas.Tables("Codigos").NewRow

            'Agregamos los valores a los campos de la tabla
            fila("Cardname") = "TODOS"
            fila("Cardname") = "Soporte TI"
            fila("CardCode") = 99

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Clientes").Rows.Add(fila)
            ' DSetTablas.Tables("Codigos").Rows.Add(fila)

            dvClientes = DSetTablas.Tables("Clientes").DefaultView

            Me.cmbPara.DataSource = DSetTablas.Tables("Clientes")
            Me.cmbPara.DisplayMember = "Cardname"
            'Me.cmbPara.ValueMember = "CardCode"
            'Me.cmbPara.SelectedValue = 99


            'Me.txtCuerpo.Text = dvClientes(0).ToString()
            'Me.txtCuerpo.Text = "CardCode"

            Me.cmbCorreos.DataSource = DSetTablas.Tables("Clientes")
            Me.cmbCorreos.DisplayMember = "U_BXP_CorreoPrincipal"
            'Me.cmbCorreos.DisplayMember = "TODOS"

            Me.cmbCodigo.DataSource = DSetTablas.Tables("Clientes")
            Me.cmbCodigo.DisplayMember = "CardCode"

            Me.cmbRFC.DataSource = DSetTablas.Tables("Clientes")
            Me.cmbRFC.DisplayMember = "LictradNum"


            'Me.txtEmail.Text = DSetTablas.Tables("Clientes").ToString
            'Me.txtEmail.Text = "U_BXP_CorreoPrincipal"








        End Using

    End Sub

    Public Sub comboCorreos()
        txtCorreo.Text = cmbCorreos.Text

    End Sub

    Public Sub comboCodigos()

        txtCodigo.Text = cmbCodigo.Text
    End Sub

    Public Sub comboRFC()
        txtRFC.Text = cmbRFC.Text

    End Sub


    'Enviar Mail
    Public Sub EnvioMail(Too As String, CC As String, Subject As String, body As String, RutaArchivo As String)
        Try
            Dim correo As New MailMessage
            Dim smtp As New SmtpClient()

            correo.From = New MailAddress("clienteleal@tractopartesdiamante.com.mx", "Tracto Partes Diamante de Puebla", Encoding.UTF8)

            correo.To.Add(Too)
            If CC <> "" Then
                'Dim CCopia As String[]
                correo.CC.Add(CC)
            End If
            correo.SubjectEncoding = Encoding.UTF8
            correo.Subject = Subject
            Dim archivoAdjunto As New Attachment(RutaArchivo)
            correo.Attachments.Add(archivoAdjunto)



            correo.Body = body
            correo.BodyEncoding = Encoding.UTF8
            correo.IsBodyHtml = True
            correo.Priority = MailPriority.High

            smtp.UseDefaultCredentials = True
            smtp.Credentials = New NetworkCredential("clienteleal@tractopartesdiamante.com.mx", "Cl1Leal@cto2020")
            smtp.Port = 587
            smtp.Host = "servidor3315.tl.controladordns.com"
            smtp.EnableSsl = True

            smtp.Send(correo)

            'Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString("Cuerpo del correo", Nothing, "text/html")
            ''Path de la imagen
            'Dim logo As New LinkedResource("C:\Users\Programador TI\Pictures\logo.jpg")
            'logo.ContentId = "companylogo"
            ''Adicionando logo
            'htmlView.LinkedResources.Add(logo)


            MessageBox.Show("Se envio correctamente el correo", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error al enviar el correo")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim cadena As String
        cadena = "" &
        " <!DOCTYPE html>" &
        "<html xmlns='http://www.w3.org/1999/xhtml'>" &
        "<head>" &
        " <title></title>" &
         "</head>" &
         "<body >" & " <h3>" + "Estimado(a) Cliente " + cmbPara.Text + " </h3>" &
          " <p style=text-align:justify >" & "<b>" + "Tracto Partes Diamante de Puebla, S.A. de C.V." + "</b>" + " como siempre le agradece su preferencia y confianza al considerarnos como uno de los Proveedores de su empresa y/o negocio. Es por ello que, pensando en Usted, estamos trabajando para ofrecerle una excelente atención y brindarle un mejor servicio." + "</p>" &
         " <p style=text-align:justify >" + "Por tal motivo hemos creado para su mayor comodidad una página web, en donde podrá realizar lo siguiente:" + "</p>" &
          " <p style=text-align:justify >" + "<ul>" & "<li>" + "Consultas y/o descargas de Documentos Fiscales" + "</li>" &
          "<li>" + "Consulta de Estado de Cuenta" + "</li>" &
          "<li>" + "Rastrear pedidos en línea" + "</li>" &
           "</ul>" + "</p>" &
          " <p style=text-align:justify >" + "Con ello, Usted podrá estar actualizado del estatus de su relación comercial con nosotros y contar con información en tiempo real en el momento que lo desee.
           Lo invitamos a conocer esta página en la siguiente dirección: " + "</p>" &
           "<p style=text-align:justify >" + "www.clienteleal.com " + "</p>" &
           "<p style=text-align:justify >" + "A continuación, le proporcionamos sus credenciales para que pueda iniciar sesión y acceder a su información personalizada: " + "</p>" &
           "Correo electrónico:  " + txtCorreo.Text + "<br>" + "Contraseña:   " + txtRFC.Text +
           "<p style=text-align:justify >" + "Adjunto a este correo encontrará un manual que servirá de apoyo para facilitar la navegación en el sitio." + "</p>" &
           "<p style=text-align:justify >" + "Muchas Gracias por brindarnos su confianza y lealtad.  Nosotros seguiremos innovando para garantizar su satisfacción total en nuestro servicio y calidad de nuestros productos. " + "</p>" &
           "<p style=text-align:justify >" + "Atentamente" + "</p>" &
           "<p style=text-align:justify >" + "TRACTO PARTES DIAMANTE DE PUEBLA, S.A. DE C.V." + "</p>" &
           " </body></html>" + txtAgradecimientos.Text + vbCrLf + txtFirma.Text


        'A.rtf = cadena.

        EnvioMail(txtCorreo.Text, txtCC.Text, txtasunto.Text, cadena, "C:\Users\Programador TI\Desktop\TPD_Maual_CLTPD.pdf")

        'End If

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint


        If rbCliente.Checked = True Then
            cmbPara.Enabled = True
            txtPara.Enabled = False
            lblEnviar.Visible = True
            'txtEmail.Visible = True
        ElseIf rbExterno.Checked = True Then
            cmbPara.Enabled = False
            txtPara.Enabled = True
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCorreos.SelectedIndexChanged
        comboCorreos()
    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtRFC.TextChanged

    End Sub

    Private Sub cmbRFC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRFC.SelectedIndexChanged
        comboRFC()
    End Sub

    Private Sub cmbCodigo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCodigo.SelectedIndexChanged
        comboCodigos()
    End Sub
End Class