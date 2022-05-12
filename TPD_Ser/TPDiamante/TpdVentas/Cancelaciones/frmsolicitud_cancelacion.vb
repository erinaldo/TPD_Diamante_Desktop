Imports System.Data.SqlClient
Imports System.Net.Mail

Public Class frmsolicitud_cancelacion
    'VARIABLE DE ALMACENAMIENTO DEL USUARIO
    Dim user As String
    'VARIABLE PARA RELLENO DEL COMBO DE MOTIVOS
    Dim Lista As New ArrayList
    'VARIABLE BANDERA DE ENVIO DE CORREO EXITOSO
    Dim envio_exi_ok As Boolean = False
    Private Sub frmsolicitud_cancelacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'COLOCA LA FECHA ACTUAL EN LA ETIQUETA DE FECHA
        lblfecha.Text = DateTime.Now.ToString("yyyy-MM-dd")

        '-------

        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar()
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand("SELECT Id_Usuario, Nombre FROM Usuarios WHERE Id_Usuario = '" + UsrTPM + "'", conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                'ALMACENA EL RESULTADO
                user = conexion_universal.rd_s.GetValue(0).ToString
                txtusuario.Text = conexion_universal.rd_s.GetValue(1).ToString
            End While
            conexion_universal.cerrar_conectar()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión TPD: " & ex.Message, MsgBoxStyle.Critical)
            conexion_universal.cerrar_conectar()
            Return
        End Try 'FIN CAPTURA EL ERROR

        '------

        'ESTILO DE GRID DE CANCELACIONES
        estilo_grid_can()

        '-----

        'ESTILO DE GRID DE DETALLE DE FACTURA
        estilo_grid_detalle()

        '------

        'LLENADO DE COMBO DE LOS MOTIVOS POR CANCELACIÓN
        Llena_Motivos()

        'AQUI VA LO QUE SE MUESTRA POR USUARIO

        'If UsrTPM = "MANAGER" Then

        'End If
    End Sub

    Sub Llena_Motivos()
        'OBTIENE TODOS LOS MOTIVOS DE CANCELACIONES
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS DEL TPD
            conexion_universal.conectar()
            'CONSULTA DE OBTENCIÓN DE LOS MOTIVOS
            conexion_universal.slq_s = New SqlCommand("SELECT Motivo FROM Documents_motivos", conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                Lista.Add(conexion_universal.rd_s.Item("Motivo"))
            End While
            Lista.Add("OTROS")
            conexion_universal.rd_s.Close()
            With cmbmotivos
                .DropDownStyle = ComboBoxStyle.DropDown
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DataSource = Lista
                .DisplayMember = "Motivo"
                '.ValueMember = "ItmsGrpNam"
            End With
            'COLOCA EN LA POSICION CERO EL COMBO PARA NO MOSTRAR NINGUN VALOR POR DEFECTO
            cmbmotivos.SelectedIndex = -1

            'CIERRA LA CONEXION
            conexion_universal.cerrar_conectar()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión TPD: " & ex.Message, MsgBoxStyle.Critical)
            conexion_universal.cerrar_conectar()
            Return
        End Try 'FIN CAPTURA EL ERROR
    End Sub

    'ENVIA CORREO CORRESPONDIENTE
    Sub EnviaCancelacion()
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
        Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
        Dim de As String = "" 'ALMACENA QUIEN MANDA EL CORREO
        Dim para As String = "" ' PARA QUIEN MANDA EL CORREO
        Dim cc As String = "" 'COPIA DEL CORREO
        Dim cco As String = "" 'COPIA OCULTA DEL CORREO
        Dim Titulo As String = "" 'COLOCA EL ASUNTO DEL CORREO
        Dim pass As String = "" 'CONTRASEÑA DEL CORREO QUE ENVIA
        Dim envio_ok As Boolean = False 'BANDERA PARA ENVIOS CORRECTOS
        Dim envio_de_ok As Boolean = False 'BANDERA PARA VALIDAR SI EL USUARIO SI TIENE ALGUN CORREO
        Dim refactura As String = "" 'ALMACENA SI SE REFACTURA LA FACTURA O NO

        '-----

        'ALMACENA SI LA FACTURA A CANCELAR SE TIENE QUE REFACTURAR O NO
        'ALMACENA VALOR DE REFACTURACIÓN
        If cbsi.Checked = True Then
            refactura = "SI"
        Else
            refactura = "NO"
        End If

        '-----

        'OBTIENE LOS DATOS DE QUIEN ENVIA EL CORREO
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS DEL TPD
            conexion_universal.conectar()
            'CONSULTA DE OBTENCIÓN DE LOS MOTIVOS
            conexion_universal.slq_s = New SqlCommand("SELECT Id_Usuario, CorreoE, Pswmail FROM Usuarios where Id_Usuario = '" + UsrTPM + "'", conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If conexion_universal.rd_s.Read Then
                de = conexion_universal.rd_s.Item("CorreoE")
                pass = conexion_universal.rd_s.Item("Pswmail")
                cc = conexion_universal.rd_s.Item("CorreoE") + ";"
            End If
            'ALMACENA BANDERA PARA EL ENVIO SI TIENE EMISOR
            envio_ok = True
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            'CIERRA LA CONEXION
            conexion_universal.cerrar_conectar()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión TPD para Obtencion de Usuario: " & ex.Message, MsgBoxStyle.Critical, "Error de BD")
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar()
            envio_exi_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
            Return
        End Try 'FIN CAPTURA EL ERROR

        '------

        Try 'CAPTURA ERROR DE ENVIO DE CORREO
            'VALIDA SI SE PUEDE HACER EL ALMACEN DE ENVIOS
            If envio_ok = True Then
                'ADJUNTA LOS CORREOS DE ENVIO VALIDADOS QUE TENGAN ENVIO
                If de <> "" Then
                    'AQUI SE TIENE QUE CAMBIAR EL NOMBRE CUANDO YA SE IMPLEMENTE
                    Msg.From = New System.Net.Mail.MailAddress(de, txtusuario.Text, System.Text.Encoding.UTF8) 'DE QUIEN SE ENVIA
                    'Msg.From = New System.Net.Mail.MailAddress(de, "Uriel Toralva", System.Text.Encoding.UTF8) 'DE QUIEN SE ENVIA
                    envio_de_ok = True
                Else
                    envio_de_ok = False
                    envio_exi_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
                    MsgBox("El correo de cancelación no se envío, debido a no tener Emisor. Favor de avisar la cancelación de manera Telefónica.", MsgBoxStyle.Exclamation, "Error de Emisor")
                    Return
                End If
            End If

            'OBTIENE LOS CORREO DE COPIA PARA ENVIO
            Try 'CAPTURA EL ERROR
                'CONECTA A LA BASE DE DATOS DEL TPD
                conexion_universal.conectar()
                'CONSULTA DE OBTENCIÓN DE LOS MOTIVOS

                conexion_universal.slq_s = New SqlCommand("SELECT fase_nombre, correo FROM documents_correos LEFT JOIN Usuarios ON Usuarios.Almacen = Documents_correos.WHSCODE2 where fase_envio = 1 AND Id_Usuario ='" + UsrTPM + "'", conexion_universal.conexion_uni)
                'EJECUTA LA CONSULTA
                conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()



                'para = "asistemas@tractopartesdiamante.com.mx"
                'cc = "asistemas@tractopartesdiamante.com.mx"
                'cco = "asistemas@tractopartesdiamante.com.mx"

                'RECORRE LA CONSULTA
                While conexion_universal.rd_s.Read

                    'VALIDA QUE ACCION MANDAR
                    If conexion_universal.rd_s.Item("fase_nombre") = "P" Then
                        Msg.To.Add(conexion_universal.rd_s.Item("correo"))
                        'Msg.To.Add(para)
                    ElseIf conexion_universal.rd_s.Item("fase_nombre") = "C" Then
                        Msg.CC.Add(conexion_universal.rd_s.Item("correo"))
                    ElseIf conexion_universal.rd_s.Item("fase_nombre") = "CO" Then
                        Msg.Bcc.Add(conexion_universal.rd_s.Item("correo"))
                    End If
                End While
                'CIERRA EL READER
                conexion_universal.rd_s.Close()
                'CIERRA LA CONEXION
                conexion_universal.cerrar_conectar()
            Catch ex As Exception
                MsgBox("Error de consulta o conexión TPD para Obtencion de los correos: " & ex.Message, MsgBoxStyle.Critical, "Error de BD")
                'CIERRA EL READER
                conexion_universal.rd_s.Close()
                conexion_universal.cerrar_conectar()
                envio_exi_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
                Return
            End Try 'FIN CAPTURA EL ERROR

            '-----

            'COLOCA EL ASUNTO DEL CORREO
            Titulo = "Solicitud de Cancelación Factura [ " + txtfactura_cancelar.Text + " ]"

            'ADJUNTA EL TITULO AL CORREO
            Msg.Subject = Titulo

            'ENCRIPTA EL ASUNTO DEL MENSAJE
            Msg.SubjectEncoding = System.Text.Encoding.UTF8
            'COLOCA EL ENCABEZADO Y EL CUERPO DEL MENSAJE A MOSTRAR EN EL CORREO

            Dim vMensaje As String = "<html><head></head><body><h2>Cancelación de Factura: " & txtfactura_cancelar.Text.ToString() & "</h2>"
            vMensaje &= "<p>Solicita: " & txtusuario.Text.ToString()
            vMensaje &= "<br>Cliente: " & txtcliente.Text & "  " & txtnombre.Text
            vMensaje &= "<br>Motivo: " & cmbmotivos.Text.ToString()
            vMensaje &= "<br>Comentario: " & txtcomentarios.Text.ToString()
            vMensaje &= "<br>Estatus: PENDIENTE AUTORIZACIÓN</p><br>"
            vMensaje &= "<p>Estimado usuario:</p><p>Solicito de su apoyo para la cancelación de la factura descrita, cualquier duda o aclaración favor de indicarme.</p><p>Saludos cordiales!!!</p>"
            vMensaje &= "</body></html>"

            'ADJUNTA EL MENSAJE AL CUERPO DEL CORREO
            Msg.Body = vMensaje
            Msg.IsBodyHtml = True 'EL CUERPO DEL MENSAJE NO ES HTML

            '------

            SMTP.UseDefaultCredentials = False ' SI REQUIERE CREDENCIALES POR DEFECTO
            SMTP.Credentials = New System.Net.NetworkCredential(de, pass) ' 'CREDENCIALES PARA PODER MANDAR EL CORREO
            SMTP.Port = 2525 ' EL PUERTO QUE UTILIZA PARA PODER MANDAR EL ENVIO DEL CORREO
            SMTP.Host = "mail.tractopartesdiamante.com.mx" ' SERVIDOR DEL ENVIO DE MENSAJES
            SMTP.EnableSsl = False ' ESTO ES PARA QUE VAYA ATRAVEEZ DE SSL(USO DEL SERTIFICADO DIGITAL) POR SI USAMOS GMAIL POR EJEMPLO
            SMTP.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network 'ENVIAMOS ATRAVEZ DE LA RED
            'mail.fng-puebla.com.mx 192.168.1.7
            SMTP.Send(Msg)
            'INICIALIZA LA VARIABLE EN VERDADERO SI SE ENVIO CORRECTAMENTE EL CORREO
            envio_exi_ok = True
        Catch exc As Exception
            'INICIALIZA LA VARIABLE EN FALSO SI EL ENVIO NO SE COMPLETO
            envio_exi_ok = False
            'MENSAJE DE ERROR DE ENVIO DE CORREO
            MessageBox.Show("No fue posible enviar Email de la cancelación," & Chr(13) & "Favor de avisar la Autorización de manera Telefonica..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub estilo_grid_can() 'ESTILO DEL GRID DE LINEAS
        With Me.dgvfacturas_can
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            '.RowHeadersVisible = False
            'USUARIO
            .AllowUserToAddRows = False
            .Columns("Usuario").Width = 170
            .Columns("Usuario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Usuario").ReadOnly = False
            'FACTURA
            .Columns("Factura").Width = 80
            .Columns("Factura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("Factura").DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("Factura").ReadOnly = False
            'FECHA FACTURA
            .Columns("Fecha_factura").Width = 70
            .Columns("Fecha_factura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Fecha_factura").ReadOnly = False
            'FECHA SOLICITUD
            .Columns("FechaSolicitudCan").Width = 95
            .Columns("FechaSolicitudCan").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaSolicitudCan").ReadOnly = False
            'COMENTARIOS
            .Columns("Comentarios").Width = 200
            .Columns("Comentarios").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Comentarios").ReadOnly = False
            'ALMACEN
            .Columns("Almacen").Width = 80
            .Columns("Almacen").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Almacen").ReadOnly = False
            'CODIGO DEL CLIENTE
            .Columns("CardCode").Width = 80
            .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("CardCode").ReadOnly = False
            'NOMBRE DEL CLIENTE
            .Columns("CardName").Width = 170
            .Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("CardName").ReadOnly = False
            'COMENTARIOS SAP
            .Columns("Comments_sap").Width = 200
            .Columns("Comments_sap").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Comments_sap").ReadOnly = False
            'TOTAL FACTURA
            .Columns("Total").Width = 70
            .Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Total").ReadOnly = False
            'NOTA DE CREDITO
            .Columns("NotaCredito").Width = 80
            .Columns("NotaCredito").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("NotaCredito").ReadOnly = False
            'FECHA DE NOTA DE CREDITO
            .Columns("FechaNC").Width = 80
            .Columns("FechaNC").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaNC").ReadOnly = False
            'ESTATUS
            .Columns("Estado").Width = 180
            .Columns("Estado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Estado").ReadOnly = False
        End With
    End Sub

    Sub estilo_grid_detalle() 'ESTILO DEL GRID DE LINEAS
        With Me.dgvdetalle_f
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            '.RowHeadersVisible = False
            'ARTICULO
            .AllowUserToAddRows = False
            .Columns("ItemCode").Width = 130
            .Columns("ItemCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("ItemCode").ReadOnly = False
            'DESCRIPCION
            .Columns("Dscription").Width = 250
            .Columns("Dscription").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Dscription").ReadOnly = False
            'CANTIDAD
            .Columns("Quantity").Width = 60
            .Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Quantity").ReadOnly = False
            'LISTA DE PRECIO
            .Columns("ListaP").Width = 70
            .Columns("ListaP").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns(3).DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("ListaP").ReadOnly = False
            'PRECIO
            .Columns("Price").Width = 100
            .Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Price").DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("Price").ReadOnly = False
            'IMPORTE
            .Columns("LineTotal").Width = 100
            .Columns("LineTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("LineTotal").DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("LineTotal").ReadOnly = False
        End With
    End Sub


    Sub buscarFactura()
        'VARIABLE PARA CONSULTA DE OBTENCION DE DATOS
        Dim SQLFactura As String

        '-----

        'BORRA LOS DATOS DEL GRID
        If dgvdetalle_f.RowCount > 0 Then
            dgvdetalle_f.Rows.Clear()
        End If

        '-----
        Try
            'CONECTA A LA BASE DE DATOS DEL SAP
            conexion_universal.conectar_sap()
            'ALMACENA LA CADENA DE LA CONSULTA
            SQLFactura = "select T0.DocEntry, T0.DocNum, T0.Series, T0.CardCode, T0.CardName, "
            SQLFactura &= "T0.CntctCode, ISNULL(T2.CntctPrsn, '') AS Contacto, T0.DocDate, T0.SlpCode, T1.SlpName AS Agente, "
            SQLFactura &= "T0.Comments, ISNULL(T3.WhsCode, '') AS WhsCode, T3.ItemCode, T3.Dscription, T3.Quantity, T3.U_BXP_ListaP AS LP, "
            SQLFactura &= "CONVERT(varchar(50), CONVERT(MONEY, T3.Price), 1) as Price, "
            SQLFactura &= "CONVERT(varchar(50), CONVERT(MONEY, T3.LineTotal), 1) as LineTotal, "
            SQLFactura &= "CONVERT(varchar(50), CONVERT(MONEY, (T0.DocTotalSy - T0.VatSumSy)) , 1) as SubTotal, "
            SQLFactura &= "CONVERT(varchar(50), CONVERT(MONEY, T0.VatSumSy), 1) as VatSumSy, "
            SQLFactura &= "CONVERT(varchar(50), CONVERT(MONEY, T0.DocTotalSy), 1) as DocTotalSy  "
            SQLFactura &= "from OINV T0 INNER JOIN INV1 T3 ON T0.DocEntry = T3.DocEntry "
            SQLFactura &= "INNER JOIN OSLP T1 ON T1.SlpCode = T0.SlpCode "
            SQLFactura &= "INNER JOIN OCRD T2 ON T0.CardCode = T2.CardCode "
            SQLFactura &= "WHERE T0.DOCNUM = " + txtfactura_cancelar.Text + " "

            'ALMACENA EN UN COMMAND LA CONSULTA
            conexion_universal.slq_s = New SqlCommand(SQLFactura, conexion_universal.conexion_uni_sap)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                txtalmacen.Text = conexion_universal.rd_s.Item("WhsCode")
                txtcliente.Text = conexion_universal.rd_s.Item("CardCode")
                txtnombre.Text = conexion_universal.rd_s.Item("CardName")
                txtcontacto.Text = conexion_universal.rd_s.Item("Contacto")
                txtfecha.Text = conexion_universal.rd_s.Item("DocDate")
                txtagente.Text = conexion_universal.rd_s.Item("Agente")
                txtcom_factura.Text = conexion_universal.rd_s.Item("Comments")
                txtsubtotal.Text = conexion_universal.rd_s.Item("SubTotal").ToString
                txtimpuesto.Text = conexion_universal.rd_s.Item("VatSumSy").ToString
                txttotal.Text = conexion_universal.rd_s.Item("DocTotalSy").ToString
                'txtsubtotal.Text = CDbl(conexion_universal.rd_s.Item("SubTotal")).ToString
                'txtimpuesto.Text = CDbl(conexion_universal.rd_s.Item("VatSumSy")).ToString
                'txttotal.Text = CDbl(conexion_universal.rd_s.Item("DocTotalSy")).ToString

                '-----

                'LLENA EL DATA GRID
                If dgvdetalle_f.RowCount > 0 Then
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvdetalle_f.Rows.Add(rd_s.Item("ItemCode"), rd_s.Item("Dscription").ToString, CInt(rd_s.Item("Quantity")).ToString, rd_s.Item("LP").ToString,
                        rd_s.Item("Price").ToString, rd_s.Item("LineTotal").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvdetalle_f
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvdetalle_f.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                        'CIERRA EL READER
                        conexion_universal.rd_s.Close()
                        conexion_universal.cerrar_conectar_sap()
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                Else
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvdetalle_f.Rows.Add(rd_s.Item("ItemCode"), rd_s.Item("Dscription").ToString, CInt(rd_s.Item("Quantity")).ToString, rd_s.Item("LP").ToString,
                        rd_s.Item("Price").ToString, rd_s.Item("LineTotal").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvdetalle_f
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvdetalle_f.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                        'CIERRA EL READER
                        conexion_universal.rd_s.Close()
                        conexion_universal.cerrar_conectar_sap()
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                End If

                '-----
            End While
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            'CIERRA LA CONEXION DEL SAP
            conexion_universal.cerrar_conectar_sap()
        Catch ex As Exception
            'MANDA EL MENSAJE DE ERROR
            MsgBox("Error de conexión o Conuslta en detalle de factura: " & ex.Message, MsgBoxStyle.Critical, "Error Conexion o consulta")
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar_sap()
            Return
        End Try
        
        '-----

    End Sub
    Sub limpia_detalle()
        'SOLO LIMPIA EL DETALLE DE LA FACTURA
        If dgvdetalle_f.RowCount > 0 Then
            dgvdetalle_f.Rows.Clear()
        End If
        txtalmacen.Text = ""
        txtcliente.Text = ""
        txtnombre.Text = ""
        txtcontacto.Text = ""
        txtfecha.Text = ""
        txtagente.Text = ""
        txtcom_factura.Text = ""
        txtsubtotal.Text = ""
        txtimpuesto.Text = ""
        txttotal.Text = ""

    End Sub

    Private Sub btnregistrar_Click(sender As Object, e As EventArgs) Handles btnregistrar.Click
        'VARIABLE BANDERA SI SE INSERTO CORRECTAMENTE LA FACTURA
        Dim inserta_ok As Boolean = False
        'VARIABLES DE ALMACENAMIENTO DE DATOS
        Dim id_cancel As Integer = 0
        Dim user As String = UsrTPM
        Dim name_user As String = txtusuario.Text
        Dim doc_num As String = txtfactura_cancelar.Text
        Dim doc_date As String = ""
        Dim comments As String = txtcomentarios.Text
        Dim id_warehouse As String = ""
        Dim warehouse As String = ""
        'Dim doc_num_nc As String
        'Dim doc_date_nc As String
        'Dim doc_date_hour_nc As String
        Dim status As String = "PENDIENTE AUTORIZACION"
        'VARIABLES QUE ALMACENAN EL DETALLE DE LA FACTURA
        Dim CardCode As String = txtcliente.Text
        Dim CardName As String = txtnombre.Text
        Dim SlpName As String = txtagente.Text
        Dim Comments_sap As String = txtcom_factura.Text
        Dim SubTotal As String = txtsubtotal.Text
        Dim Impuesto As String = txtimpuesto.Text
        Dim Total As String = txttotal.Text

        'ALMACENA LA CADENA QUE SE REQUIERA
        Dim SQLCadena As String = ""
        Dim SQLInsCadena As String = ""
        'OBTIENE SOLO EL MES DEL SITEMA PARA LA VALIDACIÓN DE MISMO MES
        Dim month As Integer
        month = Format(Now(), "MM")
        'VARIABLE QUE ALMACENA SI REFACTURA O NO
        Dim ref As String

        'VALIDA QUE CONTENGA UN VALOR LA FACTURA
        If txtfactura_cancelar.Text = "" Or txtfactura_cancelar.Text = " " Then
            MsgBox("Favor de colocar la factura a Cancelar.", MsgBoxStyle.Exclamation, "Alerta dato vacio")
            txtfactura_cancelar.Focus()
            inserta_ok = False 'INDICA FALSO SI NO SE INSERTARA EL DATO
            Return
        End If

        'VALIDA QUE VERIFIQUEN LA FACTURA PRIMERO
        If txtcliente.Text = "" Or txtcliente.Text = " " Then
            MsgBox("Favor de verificar primero si la factura colocada es la requerida.", MsgBoxStyle.Exclamation, "Alerta de Factura")
            btnbuscar.Focus()
            Return
        End If

        'VALIDA QUE CONTENGA UN COMENTARIO DE MOTIVO A CANCELAR
        If cmbmotivos.Text = "" Or cmbmotivos.Text = " " Or cmbmotivos.SelectedIndex = -1 Then
            MsgBox("Favor de colocar motivo valido para la cancelación.", MsgBoxStyle.Exclamation, "Alerta dato no encontrado")
            cmbmotivos.Focus()
            inserta_ok = False 'INDICA FALSO SI NO SE INSERTARA EL DATO
            Return
        End If
        'VALIDA LA PROPIEDAD OTROS MOTIVOS
        If cmbmotivos.Text = "OTROS" Then
            'MANDA ERROR SI NO SE COLOCA UN COMENTARIO
            If txtcomentarios.Text = "" Or txtcomentarios.Text = " " Then
                MsgBox("Favor de especificar el motivo de cancelación.", MsgBoxStyle.Exclamation, "Alerta dato vacio")
                txtcomentarios.Focus()
                inserta_ok = False 'INDICA FALSO SI NO SE INSERTARA EL DATO
                Return
            End If
        End If
        'VALIDA QUE SE SELECCIONES UNA OPCION DE REFACTURACIÓN
        If cbsi.Checked = False And cbno.Checked = False Then
            MsgBox("Favor de seleccionar si la factura a registrar tendrá Refacturación o no.", MsgBoxStyle.Exclamation, "Alerta de Selección")
            cbsi.Focus()
            inserta_ok = False 'INDICA FALSO SI NO SE INSERTARA EL DATO
            Return
        End If

        'ALMACENA VALOR DE REFACTURACIÓN
        If cbsi.Checked = True Then
            ref = "SI"
        Else
            ref = "NO"
        End If

        '-----

        'OBTIENE EL CONSECUTIVO DE NUMEROS DE CANCELACIONES
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar()
            'CONSULTA DE OBTENCIÓN DEL NUMERO SIGUIENTE
            conexion_universal.slq_s = New SqlCommand("SELECT max(id_cancel) + 1 FROM Documents_Cancel", conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If conexion_universal.rd_s.Read Then
                'AUMENTA EL CONSECUTIVO
                id_cancel = CInt(conexion_universal.rd_s.GetValue(0).ToString)
            End If
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión TPD: " & ex.Message, MsgBoxStyle.Critical)
            inserta_ok = False 'INDICA FALSO SI NO SE INSERTARA EL DATO
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar()
            Return
        End Try 'FIN CAPTURA EL ERROR

        '-----

        'VALIDA QUE LA FACTURA EXISTA EN EL SAP
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar_sap()
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand("SELECT DocNum, InvntSttus, ObjType, month(DocDate) AS Mes FROM OINV WHERE DocNum = " + txtfactura_cancelar.Text + " and InvntSttus = 'O' AND ObjType = 13", conexion_universal.conexion_uni_sap)
            'conexion_universal.slq_s = New SqlCommand("SELECT DocNum, InvntSttus, ObjType, month(DocDate) AS Mes FROM OINV WHERE DocNum = " + txtfactura_cancelar.Text + "   AND ObjType = 13", conexion_universal.conexion_uni_sap)

            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If conexion_universal.rd_s.Read = 0 Then
                MsgBox("La factura introducida ya se encuentra [ Cancelada / Devuelta / No existe ].", MsgBoxStyle.Exclamation, "Alerta de Factura")
                'CIERRA EL READER
                conexion_universal.rd_s.Close()
                conexion_universal.cerrar_conectar_sap()
                Return
            Else
                Dim mes As Integer = CInt(conexion_universal.rd_s.GetValue(3).ToString)
                'VALIDA SI LA FACTURA ES DEL MISMO MES
                If mes <> month Then
                    MsgBox("La factura no corresponde al mes en curso. Favor de solicitar la Devolución en el area correspondiente.", MsgBoxStyle.Exclamation, "Alerta de Solicitud")
                    'CIERRA EL READER
                    conexion_universal.rd_s.Close()
                    conexion_universal.cerrar_conectar_sap()
                    txtfactura_cancelar.Focus()
                    Return
                End If
            End If
            conexion_universal.cerrar_conectar_sap()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión SAP: " & ex.Message, MsgBoxStyle.Critical)
            inserta_ok = False 'INDICA FALSO SI NO SE INSERTARA EL DATO
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar_sap()
            Return
        End Try 'FIN CAPTURA EL ERROR

        '-----

        'VALIDA SI LA FACTURA YA ESTA EN PROCESO DE CANCELACIÓN
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar()
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand("SELECT doc_num, status FROM Documents_Cancel WHERE doc_num = " + txtfactura_cancelar.Text + "", conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                If conexion_universal.rd_s.GetValue(1).ToString = "PENDIENTE AUTORIZACION" Then
                    MsgBox("La factura introducida ya Existe, se encuentra en Estatus [PENDIENTE AUTORIZACION].", MsgBoxStyle.Exclamation, "Alerta de Factura")
                    txtfactura_cancelar.Focus()
                    'CIERRA EL READER
                    conexion_universal.rd_s.Close()
                    conexion_universal.cerrar_conectar()
                    Return

                ElseIf conexion_universal.rd_s.GetValue(1).ToString = "EN AUTORIZACION CONTABLE" Then
                    MsgBox("La factura introducida ya Existe, se encuentra en Estatus [EN AUTORIZACION CONTABLE].", MsgBoxStyle.Exclamation, "Alerta de Factura")
                    txtfactura_cancelar.Focus()
                    'CIERRA EL READER
                    conexion_universal.rd_s.Close()
                    conexion_universal.cerrar_conectar()
                    Return

                ElseIf conexion_universal.rd_s.GetValue(1).ToString = "EN PROCESO DE CANCELACION" Then
                    MsgBox("La factura introducida ya Existe, se encuentra en Estatus [EN PROCESO DE CANCELACION].", MsgBoxStyle.Exclamation, "Alerta de Factura")
                    txtfactura_cancelar.Focus()
                    'CIERRA EL READER
                    conexion_universal.rd_s.Close()
                    conexion_universal.cerrar_conectar()
                    Return

                ElseIf conexion_universal.rd_s.GetValue(1).ToString = "NO PROCEDE" Then
                    MsgBox("La factura introducida ya Existe, se encuentra en Estatus [NO PROCEDE].", MsgBoxStyle.Exclamation, "Alerta de Factura")
                    txtfactura_cancelar.Focus()
                    'CIERRA EL READER
                    conexion_universal.rd_s.Close()
                    conexion_universal.cerrar_conectar()
                    Return

                ElseIf conexion_universal.rd_s.GetValue(1).ToString = "FINALIZADO" Then
                    MsgBox("La factura introducida ya Existe, se encuentra en Estatus [FINALIZADO].", MsgBoxStyle.Exclamation, "Alerta de Factura")
                    txtfactura_cancelar.Focus()
                    'CIERRA EL READER
                    conexion_universal.rd_s.Close()
                    conexion_universal.cerrar_conectar()
                    Return

                End If
            End While
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión TPD: " & ex.Message, MsgBoxStyle.Critical)
            inserta_ok = False 'INDICA QUE NO SE INSERTARA EL DATO
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar()
            Return
        End Try 'FIN CAPTURA EL ERROR

        '-----

        'OBTIENE LOS DATOS DE FACTURAS
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar_sap()
            SQLCadena = "SELECT DISTINCT(T0.DocNum), FORMAT(T0.DocDate, 'yyyy-MM-dd') as DocDate, T1.WhsCode, T2.WhsName "
            SQLCadena &= "FROM OINV T0 INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry "
            SQLCadena &= "INNER JOIN OWHS T2 ON T2.WhsCode = T1.WhsCode "
            SQLCadena &= "WHERE T0.DocNum = " + txtfactura_cancelar.Text + " "
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLCadena, conexion_universal.conexion_uni_sap)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                doc_num = conexion_universal.rd_s.GetValue(0).ToString
                'doc_date = conexion_universal.rd_s.GetValue(1).ToString("yyyy/MM/dd")
                doc_date = conexion_universal.rd_s.Item("DocDate")
                id_warehouse = conexion_universal.rd_s.GetValue(2).ToString
                warehouse = conexion_universal.rd_s.GetValue(3).ToString
            End While
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar_sap()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión SAP: " & ex.Message, MsgBoxStyle.Critical)
            inserta_ok = False 'INDICA QUE NO SE INSERTARA EL DATO
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar_sap()
            Return
        End Try 'FIN CAPTURA EL ERROR

        '-----

        'VALIDA SI REALMENTE DESEA CONFIRMAR LA CANCELACION DE LA FACTURA EN CURSO
        Dim cc As Integer
        cc = MessageBox.Show("Favor de confirmar el envio de cancelacion de la factura [ " + txtfactura_cancelar.Text + " ]." + " Realmente desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If cc = 6 Then

            'INSERTA LA FACTURA A CANCELAR
            Try
                conexion_universal.conectar()
                SQLInsCadena = "INSERT INTO Documents_Cancel ( "
                SQLInsCadena &= "id_cancel, user1, name_user, doc_num, doc_date, cancel_date_hour, motivo, comments, id_warehouse, warehouse, doc_num_nc, doc_date_nc, doc_date_hour_nc, en_use, status, refactura, "
                SQLInsCadena &= "CardCode, CardName, SlpName, Comments_sap, SubTotal, Impuesto, Total"
                SQLInsCadena &= " ) values(" + id_cancel.ToString + ", '" + user + "', '" + name_user + "', '" + doc_num + "', '" + doc_date + "', getdate(), '" + cmbmotivos.Text + "', '" + comments + "', '" + id_warehouse + "', "
                SQLInsCadena &= "'" + warehouse + "', null, null, null, '0', '" + status + "', '" + ref + "', '" + CardCode + "', '" + CardName + "',  '" + SlpName + "', '" + Comments_sap + "', '" + SubTotal + "', '" + Impuesto + "', '" + Total + "') "
                'REALIZA LA INSERCIÓN DE LOS DATOS
                conexion_universal.slq_s = New SqlCommand(SQLInsCadena, conexion_universal.conexion_uni)
                'EJECUTA LA CONSULTA
                conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
                inserta_ok = True 'INDICA VERDADERO SI SE INSERTO EL DATO
                conexion_universal.cerrar_conectar()
            Catch ex As Exception
                MsgBox("Error al Insertar la factura en TPD: " & ex.Message, MsgBoxStyle.Critical)
                inserta_ok = False 'INDICA FALSO SI NO SE INSERTO EL DATO
                conexion_universal.cerrar_conectar()
                'Return
            End Try

            '-----

            'COLOCA EN LA ETIQUETA ENVIANDO CORREO
            lblenvio.Visible = True
            'LAPSO DE TIEMPO PARA EJECUTAR EL CODIGO DEL GRID
            System.Threading.Thread.Sleep(1500)
        Else
            inserta_ok = False
            Return 'SI SELECCIONA NO, NO HARA NADA
        End If

        '-----
        'TEMPORALLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL COMENTADO
        'MANDA A LLAMAR EL PROCEDIMIENTO DE ENVIO DE CORREO PARA LA CANCELACIÓN
        EnviaCancelacion()

        '----- VALIDA QUE MENSAJE ENVIAR

        If envio_exi_ok = True And inserta_ok = True Then
            lblenvio.Visible = False
            MsgBox("Factura insertada y enviada correctamente para su Cancelación.", MsgBoxStyle.Information, "Datos insertados y Enviados")
            envio_exi_ok = False
            inserta_ok = False
            btnlimpiar.PerformClick()
        ElseIf envio_exi_ok = False And inserta_ok = True Then
            MsgBox("Factura insertada correctamente, Envio de cancelación fallido. Favor de notificar por otro medio la Cancelación.", MsgBoxStyle.Information, "Factura correcta y Envio Incorrecto")
            lblenvio.Visible = False
            envio_exi_ok = False
            inserta_ok = False
            btnlimpiar.PerformClick()
        Else
            MsgBox("Ninguna accion se realizo, favor de contactar a Sistemas.", MsgBoxStyle.Critical, "Acciones fallidas")
            lblenvio.Visible = False
            envio_exi_ok = False
            inserta_ok = False
            btnlimpiar.PerformClick()
        End If

        '-----

        'REFRESCA EL DATA GRID VIEW DE RESULTADO
        If dgvfacturas_can.RowCount > 0 Then
            dgvfacturas_can.Rows.Clear()
        End If
        Try
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar()
            'ALAMACENA LA CONSULTA
            SQLCadena = "SELECT user1, name_user, doc_num, FORMAT(doc_date, 'yyyy-MM-dd') as doc_date , FORMAT(cancel_date_hour, 'yyyy-MM-dd hh\:mm') as cancel_date_hour, comments, id_warehouse, warehouse, ISNULL(doc_num_nc,'') AS Num_NC, ISNULL(CONVERT(varchar(35), doc_date_nc, 126),'') AS Date_NC, status, "
            SQLCadena &= "CardCode, CardName, Comments_sap, CONVERT(varchar(50), CONVERT(MONEY, Total), 1) as Total "
            SQLCadena &= "FROM Documents_Cancel "
            SQLCadena &= "WHERE cancel_date_hour >= '" + lblfecha.Text + "' "
            SQLCadena &= "AND user1 = '" + user + "' order by cancel_date_hour desc "
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLCadena, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                If dgvfacturas_can.RowCount > 0 Then
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvfacturas_can.Rows.Add(rd_s.Item("name_user"), rd_s.Item("doc_num").ToString, rd_s.Item("doc_date"), rd_s.Item("cancel_date_hour").ToString,
                        rd_s.Item("comments").ToString, rd_s.Item("warehouse").ToString, rd_s.Item("CardCode").ToString, rd_s.Item("CardName").ToString, rd_s.Item("Comments_sap").ToString, rd_s.Item("Total").ToString,
                        rd_s.Item("Num_NC").ToString, rd_s.Item("Date_NC").ToString, rd_s.Item("status").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvfacturas_can
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvfacturas_can.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                        'CIERRA EL READER
                        conexion_universal.rd_s.Close()
                        conexion_universal.cerrar_conectar()
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                Else
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvfacturas_can.Rows.Add(rd_s.Item("name_user"), rd_s.Item("doc_num").ToString, rd_s.Item("doc_date"), rd_s.Item("cancel_date_hour").ToString,
                        rd_s.Item("comments").ToString, rd_s.Item("warehouse").ToString, rd_s.Item("CardCode").ToString, rd_s.Item("CardName").ToString, rd_s.Item("Comments_sap").ToString, rd_s.Item("Total").ToString,
                        rd_s.Item("Num_NC").ToString, rd_s.Item("Date_NC").ToString, rd_s.Item("status").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvfacturas_can
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvfacturas_can.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                        'CIERRA EL READER
                        conexion_universal.rd_s.Close()
                        conexion_universal.cerrar_conectar()
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                End If
            End While
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión TPD en llenado de GRID: " & ex.Message, MsgBoxStyle.Critical)
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar()
            Return
        End Try 'FIN CAPTURA EL ERROR

    End Sub

    Private Sub txtfactura_cancelar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtfactura_cancelar.KeyPress
        'VALIDA QUE SOLO PERMITA NUMEROS Y NO LETRAS NI DIGITOS
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
            MsgBox("Solo se permiten digitos numericos", MsgBoxStyle.Exclamation, "Alerta de caracter")
            txtfactura_cancelar.Focus()
        End If
        'VALIDA QUE SI SE PRECIONE ENTER SE REALICE LA BUSQUEDA
        If Asc(e.KeyChar) = 13 Then
            'MANDA A LLAMAR EL METODO DE FACTURA
            buscarFactura()
        End If
        'BORRA LOS DATOS DEL DETALLE SI LLEGAN A BORAR EL NUMERO DE FACTURA QUE BUSCARON
        If Asc(e.KeyChar) = 8 Then
            'MANDA  LLAMAR EL METODO LIMPIA DETALLE
            limpia_detalle()
        End If
    End Sub

    Private Sub cbsi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbsi.KeyPress
        'VALIDA QUE SI ESTA EN SU ESTADO CHEKED QUITE EL ESTADO DEL OTRO CHECK
        If cbsi.Checked = True Then
            cbno.CheckState = False
        End If
    End Sub

    Private Sub cbno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbno.KeyPress
        'VALIDA QUE SI ESTA EN SU ESTADO CHEKED QUITE EL ESTADO DEL OTRO CHECK
        If cbno.Checked = True Then
            cbsi.CheckState = False
        End If
    End Sub

    Private Sub cbsi_CheckedChanged(sender As Object, e As EventArgs) Handles cbsi.CheckedChanged
        'VALIDA QUE SI ESTA EN SU ESTADO CHEKED QUITE EL ESTADO DEL OTRO CHECK
        If cbsi.Checked = True Then
            cbno.CheckState = False
        End If
    End Sub

    Private Sub cbno_CheckedChanged(sender As Object, e As EventArgs) Handles cbno.CheckedChanged
        'VALIDA QUE SI ESTA EN SU ESTADO CHEKED QUITE EL ESTADO DEL OTRO CHECK
        If cbno.Checked = True Then
            cbsi.CheckState = False
        End If
    End Sub

    Private Sub btnlimpiar_Click(sender As Object, e As EventArgs) Handles btnlimpiar.Click
        'LIMPIA TODOS LOS COMPONENTES DEL FORMULARIO

        '----
        'REALIZA NUEVAMENTE EL LLENADO DE LOS MOTIVOS
        Llena_Motivos()

        '-----

        'PRIMERO TODAS LAS VARIABLES
        envio_exi_ok = False

        'SEGUNDOS LOS COMPONENTES
        txtfactura_cancelar.Text = ""
        cmbmotivos.SelectedIndex = -1
        txtcomentarios.Text = ""
        cbsi.Checked = False
        cbno.Checked = False
        lblenvio.Visible = False

        '-----

        'TERCER LOS COMPONENTES DEL DETALL DE FACTURA

        txtalmacen.Text = ""
        txtcliente.Text = ""
        txtnombre.Text = ""
        txtcontacto.Text = ""
        txtfecha.Text = ""
        txtagente.Text = ""
        txtcom_factura.Text = ""
        txtsubtotal.Text = ""
        txtimpuesto.Text = ""
        txttotal.Text = ""

        '-----

        'BORRA LOS DATOS DE LOS GRIDS
        If dgvfacturas_can.RowCount > 0 Then
            dgvfacturas_can.Rows.Clear()
        End If
        If dgvdetalle_f.RowCount > 0 Then
            dgvdetalle_f.Rows.Clear()
        End If

        '-----

        'POSICIONA EL APUNTADOR EN EL CAMPO DE INICIO
        txtfactura_cancelar.Focus()
    End Sub

    Private Sub btnbuscar_Click(sender As Object, e As EventArgs) Handles btnbuscar.Click
        'MANDA A LLAMAR AL METODO BUSCARFACTURA
        buscarFactura()
    End Sub
End Class