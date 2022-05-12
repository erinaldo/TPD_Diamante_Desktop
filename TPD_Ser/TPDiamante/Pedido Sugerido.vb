Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Text
Imports System.Net
Imports System.Data.SqlClient


Public Class Pedido_Sugerido

    Public conexion As New SqlConnection(conexion_universal.CadenaSQLSAP)
    Public conexion2 As New SqlConnection(conexion_universal.CadenaSQL)

    Public StrProd As String = conexion_universal.CadenaSBO_Diamante
    Public StrTpm As String = conexion_universal.CadenaSQL
    Public StrCon As String = conexion_universal.CadenaSQLSAP

    Dim DvAgentes As New DataView
    Dim DvClientes As New DataView
    Dim DvDetalle As New DataView
    Dim DvPedido As New DataView
    Dim DvListaArt As New DataView
    Dim oStrem As New System.IO.MemoryStream
    Dim VErrCAd As Integer = 0

    Dim NumOVta As Long

    Dim VErrOv As Integer = 0
    Dim VErrClte As Integer = 0
    Dim cryRpt As New ReportDocument


    Private Sub Pedido_Sugerido_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LLenaGrids()

        DiseñoAgentes()
        DiseñoClientes()
        DiseñoDetalle()
        DiseñoEstimado()
        DiseñoListaArt()
        'TxtCorreoC.Text = "asistemas@tractopartesdiamante.com.mx"

    End Sub

    Private Sub LLenaGrids()

        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Dim cmd2 As SqlCommand = Nothing
        'Dim vDiasTrans As Integer

        'Dim cmd3 As SqlCommand = Nothing
        Dim cmd4 As SqlCommand = Nothing

        Try
            cnn = New SqlConnection(StrTpm)
            cnn.Open()

            Dim FechaActual As Date = Date.Now
            'MsgBox(Today)


            cmd4 = New SqlCommand("PedidoSugerido", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.Add("@FechaActual", SqlDbType.Date).Value = FechaActual


            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn


            ''--------------------------------------------
            Dim DsPedido As New DataSet
            da.Fill(DsPedido, "DsPedido")

            DsPedido.Tables(0).TableName = "Agentes"
            DsPedido.Tables(1).TableName = "Clientes"
            DsPedido.Tables(2).TableName = "Detalle"
            DsPedido.Tables(3).TableName = "Pedido"
            DsPedido.Tables(4).TableName = "ListaArt"

            DvAgentes.Table = DsPedido.Tables("Agentes")
            DvClientes.Table = DsPedido.Tables("Clientes")
            DvDetalle.Table = DsPedido.Tables("Detalle")
            DvPedido.Table = DsPedido.Tables("Pedido")
            DvListaArt.Table = DsPedido.Tables("ListaArt")

            DGAgentes.DataSource = DvAgentes

            DGClientes.DataSource = DvClientes

            DGDetalle.DataSource = DvDetalle

            DGPedido.DataSource = DvPedido

            DGListaArt.DataSource = DvListaArt

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try


    End Sub


    Private Sub DiseñoAgentes()
        Try
            With DGAgentes
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DGAgentes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25


                'Articulo	
                .Columns(0).HeaderText = "Agente"
                .Columns(0).Width = 50
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Descripcion	
                .Columns(1).HeaderText = "Nombre"
                .Columns(1).Width = 190

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DiseñoClientes()
        Try
            With DGClientes
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DGClientes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25


                'Articulo	
                .Columns(0).HeaderText = "Cliente"
                .Columns(0).Width = 60
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Descripcion	
                .Columns(1).HeaderText = "Nombre"
                .Columns(1).Width = 235

                'Descripcion	
                .Columns(2).HeaderText = "Agente"
                .Columns(2).Width = 50
                .Columns(2).Visible = False

                'Descripcion	
                .Columns(2).HeaderText = "Correo E."
                .Columns(2).Width = 120

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DiseñoDetalle()
        Try
            With DGDetalle
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DGDetalle.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25

                .Columns(0).HeaderText = "Cliente"
                .Columns(0).Width = 60
                .Columns(0).Visible = False

                .Columns(1).HeaderText = "Artículo"
                .Columns(1).Width = 110

                .Columns(2).HeaderText = "Descripción"
                .Columns(2).Width = 170

                .Columns(3).HeaderText = "Línea"
                .Columns(3).Width = 110

                .Columns(4).HeaderText = "Mes Venta"
                .Columns(4).Width = 60
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Cantidad"
                .Columns(5).Width = 60
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(6).HeaderText = "NumCliente"
                .Columns(6).Width = 50
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).Visible = False

                .Columns(7).HeaderText = "NumMes"
                .Columns(7).Width = 50
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).Visible = False

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DiseñoEstimado()
        Try
            With DGPedido
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DGPedido.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25

                .Columns(0).HeaderText = "#"
                .Columns(0).Width = 30
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(0).DefaultCellStyle.Format = "#,###,###"

                .Columns(1).HeaderText = "Cliente"
                .Columns(1).Width = 60
                .Columns(1).Visible = False

                .Columns(2).HeaderText = "Artículo"
                .Columns(2).Width = 110

                .Columns(3).HeaderText = "Descripción"
                .Columns(3).Width = 215

                .Columns(4).HeaderText = "Línea"
                .Columns(4).Width = 120

                .Columns(5).HeaderText = "Sugerido"
                .Columns(5).Width = 60
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Format = "#,###,###"

                .Columns(6).HeaderText = "NumCliente"
                .Columns(6).Width = 50
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).Visible = False

                .Columns(7).HeaderText = "Mes"
                .Columns(7).Width = 50
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).Visible = False

                .Columns(8).HeaderText = "Nombre Cliente"
                .Columns(8).Width = 120
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).Visible = False

                .Columns(9).HeaderText = "Email"
                .Columns(9).Width = 120
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).Visible = False

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DiseñoListaArt()
        Try
            With DGListaArt
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DGListaArt.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25

                .Columns(0).HeaderText = "Artículo"
                .Columns(0).Width = 110

                .Columns(1).HeaderText = "Descripción"
                .Columns(1).Width = 215

                .Columns(2).HeaderText = "Línea"
                .Columns(2).Width = 120

            End With
        Catch ex As Exception

        End Try
    End Sub



    Private Sub DGAgentes_CurrentCellChanged(sender As Object, e As EventArgs) Handles DGAgentes.CurrentCellChanged
        Try
            DvClientes.RowFilter = "SlpCode = " & DGAgentes.Item(0, DGAgentes.CurrentRow.Index).Value.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGClientes_CurrentCellChanged(sender As Object, e As EventArgs) Handles DGClientes.CurrentCellChanged
        Try
            DvDetalle.RowFilter = "CardCode ='" & DGClientes.Item(0, DGClientes.CurrentRow.Index).Value.ToString & "'"
            DvPedido.RowFilter = "CardCode ='" & DGClientes.Item(0, DGClientes.CurrentRow.Index).Value.ToString & "'"

            Dim correo As String

            correo = IIf(DGClientes.Item(3, DGClientes.CurrentRow.Index).Value Is DBNull.Value, "", DGClientes.Item(3, DGClientes.CurrentRow.Index).Value)
            'IIf((drlinea.Item("Serie") Is DBNull.Value), "", Convert.ToString(drlinea.Item("Serie")))
            TxtCorreoC.Text = correo

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnVerSol_Click(sender As Object, e As EventArgs) Handles BtnVerSol.Click

        Dim ErrOV As Integer = 0

        Try
            Dim DtOVta As New DataTable("OrdVenta")

            DtOVta.Columns.Add("idPed", Type.GetType("System.String"))
            DtOVta.Columns.Add("articulo", Type.GetType("System.String"))
            DtOVta.Columns.Add("DesArt", Type.GetType("System.String"))
            DtOVta.Columns.Add("Linea", Type.GetType("System.String"))
            DtOVta.Columns.Add("Cantidad", Type.GetType("System.Int32"))

            Dim columnas As DataColumnCollection = DtOVta.Columns

            'Dim series As String = ""

            Dim _filaTemp As DataRow



            Dim vSinValor As Integer = 0


            For Each row As DataGridViewRow In Me.DGPedido.Rows

                If row.Cells(5).Value > 0 And row.Cells(2).Value <> "TOTAL" Then
                    vSinValor = 1
                    Exit For
                End If

            Next


            If vSinValor = 0 Then
                MessageBox.Show("Articulos sin cantidad de piezas a solicitar", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                BtnVerSol.Enabled = True
                Return
            End If


            Dim contador As Integer = 0

            For Each row As DataGridViewRow In Me.DGPedido.Rows
                If Not IsDBNull(row.Cells(4).Value) Then


                    If row.Cells(5).Value <> 0 And row.Cells(2).Value <> "TOTAL" Then
                        contador += 1

                        _filaTemp = DtOVta.NewRow()

                        _filaTemp(columnas(0)) = contador.ToString
                        _filaTemp(columnas(1)) = row.Cells(2).Value 'Articulo
                        _filaTemp(columnas(2)) = row.Cells(3).Value 'Descripción
                        _filaTemp(columnas(3)) = row.Cells(4).Value 'Línea
                        _filaTemp(columnas(4)) = row.Cells(5).Value 'Cantidad de piezas solicitadas


                        DtOVta.Rows.Add(_filaTemp)
                    End If
                End If

            Next

            Dim informe As New CRPedido

            RepComsultaP.MdiParent = Inicio
            informe.SetDataSource(DtOVta)


            'CODIGO PARA INGRESAR NOMBRE DE USUARIO

            conexion2.Open()
            Dim cmd As SqlCommand = New SqlCommand("SELECT * FROM USUARIOS WHERE ID_USUARIO = @ID_USUARIO", conexion2)
            cmd.Parameters.AddWithValue("@ID_USUARIO", Module1.UsrTPM)

            Dim VAR1 As String = ""

            Try

                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                Dim row As DataRow = dt.Rows(0)

                If dt.Rows.Count > 0 Then

                    VAR1 = CStr(row("Nombre"))
                Else
                    VAR1 = "Usuario"

                End If


            Catch ex As Exception

            End Try

            conexion2.Close()

            Dim RpDatos As New CrystalDecisions.Shared.ParameterValues()

            Dim DsNombre As New CrystalDecisions.Shared.ParameterDiscreteValue()

            Dim user As String

            If UsrTPM = "MANAGER" Then
                user = "Salvador Díaz"
            ElseIf UsrTPM = "COMPRAS1" Then
                user = "Manuel Niño de Rivera"
            ElseIf UsrTPM = "ACOMPRAS" Then
                user = "Gabriela Osorio"
            ElseIf UsrTPM = "MMAZZOCO" Then
                user = "Marco Mazzoco"
            Else
                user = UsrTPM
            End If


            DsNombre.Value = VAR1
            RpDatos.Add(DsNombre)
            informe.DataDefinition.ParameterFields("ParametroNombre").ApplyCurrentValues(RpDatos)
            RpDatos.Clear()

            '****************************************

            RepComsultaP.CrVConsulta.ReportSource = informe
            RepComsultaP.CrVConsulta.ShowExportButton = True

            RepComsultaP.Show()

        Catch EX As Exception
            ErrOV = 1

            MsgBox(EX.Message)

        End Try

        If ErrOV = 1 Then
            'BtnVerSol.Enabled = True

            MessageBox.Show("No fue posible mostrar la orden de venta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Return
        End If
    End Sub

    
    Sub ParMail()
        Dim CCO() As String = {vCorreoVta & "," & vCCorreo}
        Dim CC() As String = {""}

        Dim PARA() As String = {vCorreo}
        EnviarMail(vCorreo, PARA, "xxx", "xxxx", CC, CCO)
    End Sub

    Public Sub EnviarMail(ByVal De As String, ByVal Para As String(), ByVal Asunto As String, ByVal Cuerpo As String, Optional ByVal CC As String() = Nothing, Optional ByVal CCO As String() = Nothing)

        Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
        Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        'Try

        Msg.From = New System.Net.Mail.MailAddress(De, "", System.Text.Encoding.UTF8) ' De quien se envia el Correo

        For Each From As String In Para
            If From <> "" Then Msg.To.Add(From) ' Para quien se Envia
        Next


        If CC IsNot Nothing Then
            For Each C As String In CC
                If C <> "" Then Msg.CC.Add(C)
            Next
        End If

        If CCO IsNot Nothing Then
            For Each C As String In CCO
                If C <> "" Then Msg.Bcc.Add(C)
            Next
        End If

        Dim Titulo As String
        Titulo = "Pedido Sugerido - " & NumOVta.ToString


        Msg.Subject = Titulo

        Dim vMensaje As String = "Estimado Cliente:" + vbCrLf + vbCrLf + "Adjunto a este mensaje encontrará el Pedido Sugerido del mes." + vbCrLf + vbCrLf + "Saludos cordiales."

        Msg.SubjectEncoding = System.Text.Encoding.UTF8 ' Encriptando el Asunto del Mensaje
        Msg.Body = vMensaje ' Cuerpo del Mensaje 
        Msg.BodyEncoding = System.Text.Encoding.UTF8 ' Codificando el Cuerpo del Mensaje
        Msg.IsBodyHtml = False ' El Cuerpo del Mensaje no es HTML


        Dim vNomArch As String

        vNomArch = NumOVta.ToString & "_PedidoSug.pdf"

        Dim thisAttachment As Attachment = New Attachment(oStrem, vNomArch) ' “image/jpeg”)

        Msg.Attachments.Add(thisAttachment) 'SE ADJUNTA ARCHIVO PDF


        SMTP.UseDefaultCredentials = False ' Si requiere Credenciales por Defecto
        SMTP.Credentials = New System.Net.NetworkCredential(vCorreo, vPswmail) ' las Credenciales para poder enviar el Mensaje
        SMTP.Port = 2525 ' El puerto que utiliza para el envio de Mensajes
        SMTP.Host = "mail.tractopartesdiamante.com.mx" ' el Servidor para el envio de Mensajes
        SMTP.EnableSsl = False ' Esto es para que vaya a través de SSL(Uso de Certificado Digital) por si usamos GMail por ejm.
        SMTP.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network ' Enviando Atravez de la red
        'mail.fng-puebla.com.mx 192.168.1.7
        SMTP.Send(Msg)
        'Catch exc As Exception

        '    MessageBox.Show(" 1 NO FUE POSIBLE ENVIAR EMAIL DE LA ORDEN DE VENTA," & Chr(13) & "INTENTE ENVIAR EMAIL NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
        '    MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    VErrOv = 1
        'Finally
        System.Windows.Forms.Cursor.Current = Cursors.Default
        'End Try
    End Sub


    Sub ParMail2()
        Dim CCO() As String = {""}
        Dim CC() As String = {""}

        Dim PARA() As String = {Trim(TxtCorreoC.Text)}
        EnviarMail2(vCorreo, PARA, "xxx", "xxxx", CC, CCO)
    End Sub


    Public Sub EnviarMail2(ByVal De As String, ByVal Para As String(), ByVal Asunto As String, ByVal Cuerpo As String, Optional ByVal CC As String() = Nothing, Optional ByVal CCO As String() = Nothing)

        Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
        Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Try

            Msg.From = New System.Net.Mail.MailAddress(De, "", System.Text.Encoding.UTF8) ' De quien se envia el Correo

            For Each From As String In Para
                If From <> "" Then Msg.To.Add(From) ' Para quien se Envia
            Next


            If CC IsNot Nothing Then
                For Each C As String In CC
                    If C <> "" Then Msg.CC.Add(C)
                Next
            End If

            If CCO IsNot Nothing Then
                For Each C As String In CCO
                    If C <> "" Then Msg.Bcc.Add(C)
                Next
            End If

            Dim Titulo As String
            Titulo = "PedidoSug - " & NumOVta.ToString


            Msg.Subject = Titulo

            Dim vMensaje As String = "Estimado Cliente:" + vbCrLf + vbCrLf + "Adjunto a este mensaje encontrará el Pedido Sugerido del mes." + vbCrLf + vbCrLf + "Saludos cordiales."

            Msg.SubjectEncoding = System.Text.Encoding.UTF8 ' Encriptando el Asunto del Mensaje
            Msg.Body = vMensaje ' Cuerpo del Mensaje 
            Msg.BodyEncoding = System.Text.Encoding.UTF8 ' Codificando el Cuerpo del Mensaje
            Msg.IsBodyHtml = False ' El Cuerpo del Mensaje no es HTML


            Dim vNomArch As String

            vNomArch = NumOVta.ToString & "_PedidoSug.pdf"

            Dim thisAttachment As Attachment = New Attachment(oStrem, vNomArch) ' “image/jpeg”)

            Msg.Attachments.Add(thisAttachment) 'SE ADJUNTA ARCHIVO PDF


            SMTP.UseDefaultCredentials = False ' Si requiere Credenciales por Defecto
            SMTP.Credentials = New System.Net.NetworkCredential(vCorreo, vPswmail) ' las Credenciales para poder enviar el Mensaje
            SMTP.Port = 2525 ' El puerto que utiliza para el envio de Mensajes
            SMTP.Host = "mail.tractopartesdiamante.com.mx" ' el Servidor para el envio de Mensajes
            SMTP.EnableSsl = False ' Esto es para que vaya a través de SSL(Uso de Certificado Digital) por si usamos GMail por ejm.
            SMTP.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network ' Enviando Atravez de la red
            'mail.fng-puebla.com.mx 192.168.1.7
            SMTP.Send(Msg)
        Catch exc As Exception
            VErrClte = 1
        Finally
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub


    Sub ParMail3()
        Dim CCO() As String = {Trim(TxtCorreoAd.Text)}
        Dim CC() As String = {Trim(TxtCorreoAd.Text)}

        Dim PARA() As String = {Trim(TxtCorreoAd.Text)}

        EnviarMail3(vCorreo, PARA, "xxx", "xxxx", CC, CCO)
    End Sub
    Public Sub EnviarMail3(ByVal De As String, ByVal Para As String(), ByVal Asunto As String, ByVal Cuerpo As String, Optional ByVal CC As String() = Nothing, Optional ByVal CCO As String() = Nothing)

        Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
        Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Try

            Msg.From = New System.Net.Mail.MailAddress(De, "", System.Text.Encoding.UTF8) ' De quien se envia el Correo

            For Each From As String In Para
                If From <> "" Then Msg.To.Add(From) ' Para quien se Envia
            Next


            If CC IsNot Nothing Then
                For Each C As String In CC
                    If C <> "" Then Msg.CC.Add(C)
                Next
            End If

            If CCO IsNot Nothing Then
                For Each C As String In CCO
                    If C <> "" Then Msg.Bcc.Add(C)
                Next
            End If

            Dim Titulo As String
            Titulo = "PedidoSug - " & NumOVta.ToString


            Msg.Subject = Titulo

            Dim vMensaje As String = "Estimado Cliente:" + vbCrLf + vbCrLf + "Adjunto a este mensaje encontrará el Pedido Sugerido del mes." + vbCrLf + vbCrLf + "Saludos cordiales."

            Msg.SubjectEncoding = System.Text.Encoding.UTF8 ' Encriptando el Asunto del Mensaje
            Msg.Body = vMensaje ' Cuerpo del Mensaje 
            Msg.BodyEncoding = System.Text.Encoding.UTF8 ' Codificando el Cuerpo del Mensaje
            Msg.IsBodyHtml = False ' El Cuerpo del Mensaje no es HTML


            Dim vNomArch As String

            vNomArch = NumOVta.ToString & "_PedidoSug.pdf"

            Dim thisAttachment As Attachment = New Attachment(oStrem, vNomArch) ' “image/jpeg”)

            Msg.Attachments.Add(thisAttachment) 'SE ADJUNTA ARCHIVO PDF


            SMTP.UseDefaultCredentials = False ' Si requiere Credenciales por Defecto
            SMTP.Credentials = New System.Net.NetworkCredential(vCorreo, vPswmail) ' las Credenciales para poder enviar el Mensaje
            SMTP.Port = 2525 ' El puerto que utiliza para el envio de Mensajes
            SMTP.Host = "mail.tractopartesdiamante.com.mx" ' el Servidor para el envio de Mensajes
            SMTP.EnableSsl = False ' Esto es para que vaya a través de SSL(Uso de Certificado Digital) por si usamos GMail por ejm.
            SMTP.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network ' Enviando Atravez de la red
            'mail.fng-puebla.com.mx 192.168.1.7
            SMTP.Send(Msg)
        Catch exc As Exception
            VErrCAd = 1
        Finally
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub


    Private Sub BtnEmail_Click(sender As Object, e As EventArgs) Handles BtnEmail.Click


        '***************************************************************************
        '***************************************************************************
        Dim vSinValor As Integer = 0
        Dim Fila As Integer = 0

        If MessageBox.Show("¿Confirma que desea guardar y enviar el Pedido Sugerido?", "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then

            LblMensaje.Visible = True

            'vTotIva = vTotSIva * 0.16

            'vTotDoc = vTotSIva + vTotIva

            'Procedimiento para obtener el número de transacción más actual
            Dim cmdCuenta As New Data.SqlClient.SqlCommand
            Dim FormatWO As String = ""
            cmdCuenta.CommandText = "SELECT MAX(IdPed) FROM PEDIDOS "
            cmdCuenta.CommandType = CommandType.Text
            cmdCuenta.Connection = New Data.SqlClient.SqlConnection(StrTpm)
            cmdCuenta.Connection.Open()
            'NumOVta = IIf(IsDBNull(cmdCuenta.ExecuteScalar()), 0, Val(cmdCuenta.ExecuteScalar()))

            'With cmdCuenta
            '    NumOVta = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
            '    .Connection.Close()
            'End With

            '*********************************************************************************************************************

            NumOVta = IIf(IsDBNull(Val(cmdCuenta.ExecuteScalar())), 0, Val(cmdCuenta.ExecuteScalar()))
            cmdCuenta.Connection.Close()
            NumOVta += 1

            '******************
            Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
            SqlConnection.Open()
            Dim command As New Data.SqlClient.SqlCommand
            Dim transactions As Data.SqlClient.SqlTransaction
            transactions = SqlConnection.BeginTransaction(IsolationLevel.ReadCommitted, "TransProduccion")
            command.Connection = SqlConnection
            command.Transaction = transactions
            Dim contador As Integer = 0
            'Dim cmdMovInv As Data.SqlClient.SqlCommand
            Dim strcadena As String = ""

            Try
                '******************
                strcadena = "INSERT INTO PEDIDOS (IdPed,FecPed,UserTPM,CardCode,CardName,email,slpcode,slpname,totart) VALUES ('"
                strcadena &= NumOVta.ToString
                strcadena &= "',"
                strcadena &= "@Fecha"
                strcadena &= ",'"
                strcadena &= UsrTPM.ToString
                strcadena &= "','"
                strcadena &= DGClientes.Item(0, DGClientes.CurrentRow.Index).Value.ToString
                strcadena &= "','"
                strcadena &= DGClientes.Item(1, DGClientes.CurrentRow.Index).Value.ToString
                strcadena &= "','"
                strcadena &= DGClientes.Item(3, DGClientes.CurrentRow.Index).Value.ToString
                strcadena &= "','"
                strcadena &= DGClientes.Item(2, DGClientes.CurrentRow.Index).Value.ToString
                strcadena &= "','"
                strcadena &= DGAgentes.Item(1, DGAgentes.CurrentRow.Index).Value.ToString
                strcadena &= "','"
                strcadena &= DGPedido.Item(5, DGPedido.RowCount - 1).Value.ToString
                strcadena &= "')"

                'MsgBox(NumOVta)
                'MsgBox(DateTime.Now)
                'MsgBox(UsrTPM)
                'MsgBox(DGClientes.Item(0, DGClientes.CurrentRow.Index).Value.ToString)
                'MsgBox(DGClientes.Item(1, DGClientes.CurrentRow.Index).Value.ToString)
                'MsgBox(DGClientes.Item(3, DGClientes.CurrentRow.Index).Value.ToString)
                'MsgBox(DGClientes.Item(2, DGClientes.CurrentRow.Index).Value.ToString)

                'MsgBox(DGAgentes.Item(1, DGAgentes.CurrentRow.Index).Value.ToString)
                'MsgBox(DGPedido.Item(5, DGPedido.RowCount - 1).Value.ToString)

                command.Parameters.AddWithValue("@Fecha", DateTime.Now)
                command.CommandText = strcadena
                command.ExecuteNonQuery()


                'Dim Valcadena As String
                For contador = 0 To DGPedido.RowCount - 2
                    'Each row As DataGridViewRow In Me.DGPedido.Rows

                    'Dim i As Integer = 0

                    'i += 1
                    'Valcadena = ""
                    'char(39), es apostrofe (')
                    'char(34), es comillas dobles (")

                    'Valcadena = row.Cells(2).Value.Replace(Chr(39), " ")
                    'Valcadena = Valcadena.Replace(Chr(34), " ")

                    strcadena = "INSERT INTO DETPEDIDOS (IdPed,ItemCode,Itemname,ItmsGrpNam,cantidad) VALUES ("
                    strcadena &= NumOVta.ToString
                    strcadena &= ",'"
                    strcadena &= DGPedido.Item(2, contador).Value.ToString
                    strcadena &= "','"
                    strcadena &= DGPedido.Item(3, contador).Value.ToString
                    strcadena &= "','"
                    strcadena &= DGPedido.Item(4, contador).Value.ToString
                    strcadena &= "','"
                    strcadena &= DGPedido.Item(5, contador).Value.ToString
                    strcadena &= "')"

                    command.CommandText = strcadena
                    command.ExecuteNonQuery()

                Next
                transactions.Commit()

            Catch exc As Exception
                Try
                    transactions.Rollback("TransProduccion")
                Catch exSql As SqlClient.SqlException
                    If Not transactions.Connection Is Nothing Then
                        MessageBox.Show("AL REALIZAR ROLL BACK," + exSql.Message.ToString, "SQL ERROR!", _
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Try

                MessageBox.Show("NO FUE POSIBLE CREAR Pedido," & Chr(13) & "POR FAVOR INTENTELO DE NUEVO..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
                VErrOv = 1

            Finally
                SqlConnection.Close()

            End Try
            SqlConnection.Close()

            If VErrOv = 1 Then
                'BtnGuardar.Enabled = True
                LblError.Visible = True
                LblMensaje.Visible = False
                'TxtArticulo.Focus()
                Return
            Else
                LblMensaje.Visible = False
                MessageBox.Show("Pedido Sugerido " & NumOVta & " se creo exitosamente. ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If



            '*********************************************************************************************************************



            Try
                Dim DtOVta As New DataTable("OrdVenta")

                DtOVta.Columns.Add("idPed", Type.GetType("System.String"))
                DtOVta.Columns.Add("articulo", Type.GetType("System.String"))
                DtOVta.Columns.Add("DesArt", Type.GetType("System.String"))
                DtOVta.Columns.Add("Linea", Type.GetType("System.String"))
                DtOVta.Columns.Add("Cantidad", Type.GetType("System.Int32"))

                Dim columnas As DataColumnCollection = DtOVta.Columns

                'Dim series As String = ""
                Dim _filaTemp As DataRow

                'Dim contador As Integer = 0


                For Each row As DataGridViewRow In Me.DGPedido.Rows
                    If Not IsDBNull(row.Cells(4).Value) Then


                        If row.Cells(5).Value <> 0 And row.Cells(2).Value <> "TOTAL" Then
                            contador += 1

                            _filaTemp = DtOVta.NewRow()

                            _filaTemp(columnas(0)) = contador.ToString
                            _filaTemp(columnas(1)) = row.Cells(2).Value 'Articulo
                            _filaTemp(columnas(2)) = row.Cells(3).Value 'Descripción
                            _filaTemp(columnas(3)) = row.Cells(4).Value 'Línea
                            _filaTemp(columnas(4)) = row.Cells(5).Value 'Cantidad de piezas solicitadas


                            DtOVta.Rows.Add(_filaTemp)
                        End If
                    End If

                Next


                'CODIGO PARA INGRESAR NOMBRE DE USUARIO
                conexion2.Open()
                Dim cmd As SqlCommand = New SqlCommand("SELECT * FROM USUARIOS WHERE ID_USUARIO = @ID_USUARIO", conexion2)
                cmd.Parameters.AddWithValue("@ID_USUARIO", Module1.UsrTPM)

                Dim VAR1 As String

                'Try

                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count > 0 Then

                    Dim row As DataRow = dt.Rows(0)

                    VAR1 = CStr(row("Nombre"))

                Else
                    VAR1 = "Usuario"

                End If

                Dim RpDatos As New CrystalDecisions.Shared.ParameterValues()

                Dim DsNombre As New CrystalDecisions.Shared.ParameterDiscreteValue()


                'MsgBox(VErrOv)
                'MsgBox(VErrCAd)
                'MsgBox(Module1.vCorreo)

                'If VErrOv = 0 Then

                'VErrOv = 0

                Dim informe As New CRPedido

                RepComsultaP.MdiParent = Inicio
                informe.SetDataSource(DtOVta)

                RepComsultaP.CrVConsulta.ReportSource = informe

                DsNombre.Value = VAR1
                RpDatos.Add(DsNombre)
                informe.DataDefinition.ParameterFields("ParametroNombre").ApplyCurrentValues(RpDatos)
                RpDatos.Clear()

                'RepComsultaP.Show()

                '****************************************
                oStrem = CType(informe.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), System.IO.MemoryStream)

                ParMail()

                'Dim informe2 As New CRPedido

                'RepComsultaP.MdiParent = Inicio
                'informe2.SetDataSource(DtOVta)

                'RepComsultaP.CrVConsulta.ReportSource = informe2


                'oStrem = CType(informe2.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), System.IO.MemoryStream)

                If TxtCorreoC.Text <> "" And VErrOv = 0 Then
                    ParMail2()
                End If

                If TxtCorreoAd.Text <> "" And VErrOv = 0 Then
                    ParMail3()
                End If

                conexion2.Close()
            Catch exc As Exception
                VErrOv = 1
                MessageBox.Show("NO FUE POSIBLE ENVIAR EMAIL DEL PEDIDO," & Chr(13) & "INTENTE ENVIAR EMAIL NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try



            MessageBox.Show("La orden de venta se creo exitosamente! y se envio por correo electrónico!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LblExito.Text = NumOVta.ToString & " -- " & "Pedido Sugerido y Enviado Exitosamente !"

            LblExito.Visible = True


            If VErrCAd = 1 Then
                MessageBox.Show("No fue posible enviar el Pedido al EMail capturado," & Chr(13) & "Verifique el correo electrónico e intente nuevamente..." & Chr(13), "Advertencia", _
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                BtnEmail.Enabled = True
            Else
                BtnEmail.Enabled = False
                'MsgBox("LA ORDEN SE HA ENVIADO")
            End If


        End If
        '*********************************************************************************************************************
    End Sub


    Private Sub BGuardar_Click(sender As Object, e As EventArgs)
        'BtnGuardar.Enabled = False

        LblMensaje.Visible = True

        'vTotIva = vTotSIva * 0.16

        'vTotDoc = vTotSIva + vTotIva

        'Procedimiento para obtener el número de transacción más actual
        Dim cmdCuenta As New Data.SqlClient.SqlCommand
        Dim FormatWO As String = ""
        cmdCuenta.CommandText = "SELECT MAX(IdPed) FROM PEDIDOS "
        cmdCuenta.CommandType = CommandType.Text
        cmdCuenta.Connection = New Data.SqlClient.SqlConnection(StrTpm)
        cmdCuenta.Connection.Open()
        'NumOVta = IIf(IsDBNull(cmdCuenta.ExecuteScalar()), 0, Val(cmdCuenta.ExecuteScalar()))

        'With cmdCuenta
        '    NumOVta = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
        '    .Connection.Close()
        'End With

        '*********************************************************************************************************************

        NumOVta = IIf(IsDBNull(Val(cmdCuenta.ExecuteScalar())), 0, Val(cmdCuenta.ExecuteScalar()))
        cmdCuenta.Connection.Close()
        NumOVta += 1

        '******************
        Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
        SqlConnection.Open()
        Dim command As New Data.SqlClient.SqlCommand
        Dim transactions As Data.SqlClient.SqlTransaction
        transactions = SqlConnection.BeginTransaction(IsolationLevel.ReadCommitted, "TransProduccion")
        command.Connection = SqlConnection
        command.Transaction = transactions
        Dim contador As Integer = 0
        'Dim cmdMovInv As Data.SqlClient.SqlCommand
        Dim strcadena As String = ""

        Try
            '******************
            strcadena = "INSERT INTO PEDIDOS (IdPed,FecPed,UserTPM,CardCode,CardName,email,slpcode,slpname,totart) VALUES ('"
            strcadena &= NumOVta.ToString
            strcadena &= "',"
            strcadena &= "@Fecha"
            strcadena &= ",'"
            strcadena &= UsrTPM.ToString
            strcadena &= "','"
            strcadena &= DGClientes.Item(0, DGClientes.CurrentRow.Index).Value.ToString
            strcadena &= "','"
            strcadena &= DGClientes.Item(1, DGClientes.CurrentRow.Index).Value.ToString
            strcadena &= "','"
            strcadena &= DGClientes.Item(3, DGClientes.CurrentRow.Index).Value.ToString
            strcadena &= "','"
            strcadena &= DGClientes.Item(2, DGClientes.CurrentRow.Index).Value.ToString
            strcadena &= "','"
            strcadena &= DGAgentes.Item(1, DGAgentes.CurrentRow.Index).Value.ToString
            strcadena &= "','"
            strcadena &= DGPedido.Item(5, DGPedido.RowCount - 1).Value.ToString
            strcadena &= "')"

            'MsgBox(NumOVta)
            'MsgBox(DateTime.Now)
            'MsgBox(UsrTPM)
            'MsgBox(DGClientes.Item(0, DGClientes.CurrentRow.Index).Value.ToString)
            'MsgBox(DGClientes.Item(1, DGClientes.CurrentRow.Index).Value.ToString)
            'MsgBox(DGClientes.Item(3, DGClientes.CurrentRow.Index).Value.ToString)
            'MsgBox(DGClientes.Item(2, DGClientes.CurrentRow.Index).Value.ToString)

            'MsgBox(DGAgentes.Item(1, DGAgentes.CurrentRow.Index).Value.ToString)
            'MsgBox(DGPedido.Item(5, DGPedido.RowCount - 1).Value.ToString)

            command.Parameters.AddWithValue("@Fecha", DateTime.Now)
            command.CommandText = strcadena
            command.ExecuteNonQuery()


            'Dim Valcadena As String
            For contador = 0 To DGPedido.RowCount - 2
                'Each row As DataGridViewRow In Me.DGPedido.Rows

                'Dim i As Integer = 0

                'i += 1
                'Valcadena = ""
                'char(39), es apostrofe (')
                'char(34), es comillas dobles (")

                'Valcadena = row.Cells(2).Value.Replace(Chr(39), " ")
                'Valcadena = Valcadena.Replace(Chr(34), " ")

                strcadena = "INSERT INTO DETPEDIDOS (IdPed,ItemCode,Itemname,ItmsGrpNam,cantidad) VALUES ("
                strcadena &= NumOVta.ToString
                strcadena &= ",'"
                strcadena &= DGPedido.Item(2, contador).Value.ToString
                strcadena &= "','"
                strcadena &= DGPedido.Item(3, contador).Value.ToString
                strcadena &= "','"
                strcadena &= DGPedido.Item(4, contador).Value.ToString
                strcadena &= "','"
                strcadena &= DGPedido.Item(5, contador).Value.ToString
                strcadena &= "')"

                command.CommandText = strcadena
                command.ExecuteNonQuery()

            Next
            transactions.Commit()

        Catch exc As Exception
            Try
                transactions.Rollback("TransProduccion")
            Catch exSql As SqlClient.SqlException
                If Not transactions.Connection Is Nothing Then
                    MessageBox.Show("AL REALIZAR ROLL BACK," + exSql.Message.ToString, "SQL ERROR!", _
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Try

            MessageBox.Show("NO FUE POSIBLE CREAR Pedido," & Chr(13) & "POR FAVOR INTENTELO DE NUEVO..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
            MessageBoxButtons.OK, MessageBoxIcon.Error)
            VErrOv = 1

        Finally
            SqlConnection.Close()

        End Try
        SqlConnection.Close()

        If VErrOv = 1 Then
            'BtnGuardar.Enabled = True
            LblError.Visible = True
            LblMensaje.Visible = False
            'TxtArticulo.Focus()
            Return
        Else
            LblMensaje.Visible = False
            MessageBox.Show("Pedido Sugerido " & NumOVta & " se creo exitosamente. ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If



        '*********************************************************************************************************************
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

        Dim cmdCuenta As New Data.SqlClient.SqlCommand
        Dim FormatWO As String = ""
        cmdCuenta.CommandText = "SELECT MAX(IdPed) FROM PEDIDOS "
        cmdCuenta.CommandType = CommandType.Text
        cmdCuenta.Connection = New Data.SqlClient.SqlConnection(StrTpm)
        cmdCuenta.Connection.Open()
        'NumOVta = IIf(IsDBNull(cmdCuenta.ExecuteScalar()), 0, Val(cmdCuenta.ExecuteScalar()))

        'With cmdCuenta
        'NumOVta = IIf(IsDBNull(cmdCuenta.ExecuteScalar()), 0, cmdCuenta.ExecuteScalar())

        'End With

        '*********************************************************************************************************************

        'CODIGO PARA INGRESAR NOMBRE DE USUARIO
        conexion2.Open()
        Dim cmd As SqlCommand = New SqlCommand("SELECT * FROM USUARIOS WHERE ID_USUARIO = @ID_USUARIO", conexion2)
        cmd.Parameters.AddWithValue("@ID_USUARIO", Module1.UsrTPM)

        Dim VAR1 As String

        'Try

        Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        If dt.Rows.Count > 0 Then

            Dim row As DataRow = dt.Rows(0)

            VAR1 = CStr(row("Nombre"))

        Else
            VAR1 = "Usuario"

        End If

        NumOVta = IIf(IsDBNull(Val(cmdCuenta.ExecuteScalar())), 0, Val(cmdCuenta.ExecuteScalar()))
        cmdCuenta.Connection.Close()
        NumOVta += 1

        MsgBox(NumOVta)
        MsgBox(DateTime.Now)
        MsgBox(VAR1)
        MsgBox(DGClientes.Item(0, DGClientes.CurrentRow.Index).Value.ToString)
        MsgBox(DGClientes.Item(1, DGClientes.CurrentRow.Index).Value.ToString)
        MsgBox(DGClientes.Item(3, DGClientes.CurrentRow.Index).Value.ToString)
        MsgBox(DGClientes.Item(2, DGClientes.CurrentRow.Index).Value.ToString)

        MsgBox(DGAgentes.Item(1, DGAgentes.CurrentRow.Index).Value.ToString)
        MsgBox(DGPedido.Item(5, DGPedido.RowCount - 1).Value.ToString)

        cmdCuenta.Connection.Close()

        conexion2.Close()
    End Sub

    Private Sub BtnMas_Click(sender As Object, e As EventArgs) Handles BtnMas.Click
        'Dim dt As DataTable = DGPedido

        '' Añadimos una nueva fila al objeto DataTable
        ''
        'Dim row As DataRow = dt.NewRow

        'With row
        '    .Item("IdCliente") = 43
        '    .Item("Nombre") = "Pedro"
        '    .Item("Domicilio") = "Calle cualquiera"
        'End With

        '' Posición actual del control BindingSource
        ''
        'Dim pos As Int32 = _bs.Position

        '' Insertamos el nuevo registro una posición más abajo
        '' que la que actualmente tiene el objeto BindingSource.
        ''
        'dt.Rows.InsertAt(row, pos + 1)

    End Sub
End Class