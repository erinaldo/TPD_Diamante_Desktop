Imports System.Net
Imports System.Net.Mail
Imports System.Text

Public Class frmEnviarCorreo

  Public rutaArchivo As String

  Private Sub frmEnviarCorreo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    txtsunto.Text = "Pedido Diario " + DateTime.Now.ToString("dd-MM-yyyy")
    If lblOrigen.Text = "OC" Then
      txtsunto.Text = "Ordenes de compra del " & DateTime.Now.ToString("dd-MM-yyyy")
    Else
      txtsunto.Text = "Pedido Diario " + DateTime.Now.ToString("dd-MM-yyyy")
    End If
  End Sub

    Public Sub EnvioMail(RutaArchivo As String, Too As String, CC As String, Subject As String, body As String, Optional CopiaOculta As String = "")
        Try
            Dim correo As New MailMessage
            Dim smtp As New SmtpClient()

            correo.From = New MailAddress("compras@tractopartesdiamante.com.mx", "Compras", Encoding.UTF8)
            correo.To.Add(Too)
            If CC <> "" Then
                'Dim CCopia As String[]
                correo.CC.Add(CC)
            End If

            If CopiaOculta <> "" Then
                correo.Bcc.Add(CopiaOculta)
            End If

            correo.SubjectEncoding = Encoding.UTF8
            correo.Subject = Subject

            Dim archivoAdjunto As New Attachment(RutaArchivo)
            correo.Attachments.Add(archivoAdjunto)

            correo.Body = body
            correo.BodyEncoding = Encoding.UTF8
            correo.IsBodyHtml = False
            correo.Priority = MailPriority.High



            smtp.UseDefaultCredentials = True
            smtp.Credentials = New NetworkCredential("compras@tractopartesdiamante.com.mx", "coTr@cto2012")
            smtp.Port = 587
            smtp.Host = "servidor3315.tl.controladordns.com"
            smtp.EnableSsl = True

            smtp.Send(correo)
            MessageBox.Show("Se envio correctamente el correo", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error al enviar el correo")
        End Try
    End Sub

    Private Sub btnEnviarCorreo_Click(sender As Object, e As EventArgs) Handles btnEnviarCorreo.Click

        Dim correo As String = "compras@tractopartesdiamante.com.mx"
        EnvioMail(lblRuta.Text, txtPara.Text, txtCC.Text, txtsunto.Text, txtCuerpo.Text, correo)
    End Sub

    Private Sub txtCuerpo_TextChanged(sender As Object, e As EventArgs) Handles txtCuerpo.TextChanged

    End Sub
End Class