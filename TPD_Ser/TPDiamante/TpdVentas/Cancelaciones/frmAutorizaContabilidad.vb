Imports System.Data.SqlClient
Imports System.Net.Mail

Public Class frmAutorizaContabilidad

    'VARIABLE GLOBAL DEL FORMULRIO
    Dim envio_cor_ok As Boolean = False
    Dim actualiza_ok As Boolean = False


    Private Sub frmAutorizaContabilidad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MANDA A LLAMAR AL DISEÑO DEL GRID
        estilo_grid_can()
        'MANDA A LLAMAR AL RELLENO DEL GRID CON LAS FACTURAS PENDIENTES POR AUTORIZAR
        llena_grid_autoriza()
    End Sub

    Sub estilo_grid_can() 'ESTILO DEL GRID
        With Me.dgvautoriza
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
            '.Columns(1).DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("Factura").ReadOnly = False
            'FECHA FACTURA
            .Columns("FechaFactura").Width = 70
            .Columns("FechaFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaFactura").ReadOnly = False
            'FECHA SOLICITUD
            .Columns("FechaCancela").Width = 95
            .Columns("FechaCancela").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaCancela").ReadOnly = False
            'MOTIVO
            .Columns("Motivo").Width = 150
            .Columns("Motivo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Motivo").ReadOnly = False
            'COMENTARIOS
            .Columns("Comentarios").Width = 180
            .Columns("Comentarios").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Comentarios").ReadOnly = False
            'REQUIERE FACTURA
            .Columns("Refactura").Width = 90
            .Columns("Refactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Refactura").ReadOnly = False
            'REFACTURA (SUSTITUYE)
            .Columns("Sustituye").Width = 70
            .Columns("Sustituye").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Sustituye").ReadOnly = False
            'ALMACEN
            .Columns("Almacen").Width = 80
            .Columns("Almacen").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Almacen").ReadOnly = False
            'NOTA DE CREDITO
            .Columns("NotaCredito").Width = 80
            .Columns("NotaCredito").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("NotaCredito").ReadOnly = False
            'FECHA DE NOTA DE CREDITO
            .Columns("FechaNC").Width = 80
            .Columns("FechaNC").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaNC").ReadOnly = False
            'ESTATUS
            .Columns("Status").Width = 180
            .Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Status").ReadOnly = False
        End With
    End Sub
    Sub llena_grid_autoriza()
        'VARIABLE DE CADENA DE SQL
        Dim SQLautoriza As String
        ''REFRESCA EL DATA GRID VIEW DE RESULTADO
        If dgvautoriza.RowCount > 0 Then
            dgvautoriza.Rows.Clear()
        End If
        Try
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar()
            'ALAMACENA LA CONSULTA
            SQLautoriza = "SELECT user1, name_user, doc_num, FORMAT(doc_date, 'yyyy-MM-dd') as doc_date , FORMAT(cancel_date_hour, 'yyyy-MM-dd hh\:mm') as cancel_date_hour, motivo, comments, "
            SQLautoriza &= "refactura, sustituye, id_warehouse, warehouse, ISNULL(doc_num_nc,'') AS Num_NC, ISNULL(CONVERT(varchar(35), doc_date_nc, 126),'') AS Date_NC, status "
            SQLautoriza &= "FROM Documents_Cancel WHERE status = 'EN AUTORIZACION CONTABLE' "
            SQLautoriza &= "order by cancel_date_hour ASC"
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLautoriza, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                If dgvautoriza.RowCount > 0 Then
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvautoriza.Rows.Add(rd_s.Item("name_user"), rd_s.Item("doc_num").ToString, rd_s.Item("doc_date"), rd_s.Item("cancel_date_hour").ToString,
                        rd_s.Item("motivo").ToString, rd_s.Item("comments").ToString, rd_s.Item("refactura").ToString, rd_s.Item("sustituye").ToString,
                        rd_s.Item("warehouse").ToString, rd_s.Item("Num_NC").ToString, rd_s.Item("Date_NC").ToString, rd_s.Item("status").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvautoriza
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvautoriza.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                        'CIERRA EL READER
                        rd_s.Close()
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                Else
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvautoriza.Rows.Add(rd_s.Item("name_user"), rd_s.Item("doc_num").ToString, rd_s.Item("doc_date"), rd_s.Item("cancel_date_hour").ToString,
                        rd_s.Item("motivo").ToString, rd_s.Item("comments").ToString, rd_s.Item("refactura").ToString, rd_s.Item("sustituye").ToString,
                        rd_s.Item("warehouse").ToString, rd_s.Item("Num_NC").ToString, rd_s.Item("Date_NC").ToString, rd_s.Item("status").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvautoriza
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvautoriza.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                        'CIERRA EL READER
                        rd_s.Close()
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                End If
            End While
            'CIERRA EL READER
            rd_s.Close()
            conexion_universal.cerrar_conectar()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión TPD en llenado de GRID: " & ex.Message, MsgBoxStyle.Critical)
            'CIERRA EL READER
            rd_s.Close()
            conexion_universal.cerrar_conectar()
            Return
        End Try 'FIN CAPTURA EL ERROR
    End Sub

    Sub limpiar()
        'LIMPIA TODOS LOS COMPONENTES Y LO DEJA TAL CUAL EMPIEZA EL FORM
        txtsolicita.Text = ""
        txtfecha_factura.Text = ""
        txtfecha_solicitud.Text = ""
        txtmotivo.Text = ""
        txtcomentario.Text = ""
        txtrefactura.Text = ""
        txtalmacen.Text = ""
        txtstatus.Text = ""

        '-----

        txtobservaciones.Text = ""
        cbautorizado.Checked = False
        cbno_procede.Checked = False
        gbautorizacion.Enabled = False
    End Sub

    Private Sub dgvautoriza_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvautoriza.CellContentClick
        txtfactura.Text = dgvautoriza.Item("Factura", dgvautoriza.CurrentRow.Index).Value.ToString
        'MANDA A LLAMAR AL METODO LIMPIAR
        limpiar()
        'MUESTRA LOS DATOS CON BASE A LA FACTURA SELECIONADA
        btnmostrar.PerformClick()
    End Sub
    Private Sub dgvautoriza_KeyUp(sender As Object, e As KeyEventArgs) Handles dgvautoriza.KeyUp
        'MUESTRA EL RESULTADO EN EL TEXTBOX
        txtfactura.Text = dgvautoriza.Item("Factura", dgvautoriza.CurrentRow.Index).Value.ToString
        'MANDA A LLAMAR AL METODO LIMPIAR
        limpiar()
        'MUESTRA LOS DATOS CON BASE A LA FACTURA SELECIONADA
        btnmostrar.PerformClick()
    End Sub

    Private Sub btnrefrescar_Click(sender As Object, e As EventArgs) Handles btnrefrescar.Click
        'BORRA LOS DATOS DEL GRID
        If dgvautoriza.RowCount > 0 Then
            dgvautoriza.Rows.Clear()
        End If
        'MANDA A LLAMAR AL METODO LIMPIAR
        limpiar()
        'MANDA A LLAMAR AL RELLENO DEL GRID CON LAS FACTURAS PENDIENTES POR AUTORIZAR
        llena_grid_autoriza()
    End Sub
    Private Sub btnmostrar_Click(sender As Object, e As EventArgs) Handles btnmostrar.Click
        'VARIABLE BANDERA PARA VALIDAR QUE EXISTA LA FACTURA
        Dim factura_ok As Boolean = False
        'VALIDA QUE LE TXT DE FACTURA NO ESTE VACIO
        If txtfactura.Text = "" Or txtfactura.Text = " " Then
            MsgBox("Favor de seleccionar una factura valida.", MsgBoxStyle.Exclamation, "Alerta de selección")
            Return
        End If
        'RECORRE TODO EL DATA GRID VIEW
        For i As Integer = 0 To dgvautoriza.RowCount - 1
            'VALIDA SI ROMPE EL CICLO O NO
            If factura_ok = True Then
                Exit For 'ROMPE EL CICLO FOR
            End If
            For j As Integer = 0 To dgvautoriza.ColumnCount - 1
                If dgvautoriza.Item(j, i).Value.ToString = txtfactura.Text Then
                    'MUESTRA LOS DATOS SI LA FACTURA EXISTE EN EL GRID
                    txtsolicita.Text = dgvautoriza.Item("Usuario", i).Value.ToString
                    txtfecha_factura.Text = dgvautoriza.Item("FechaFactura", i).Value.ToString
                    txtfecha_solicitud.Text = dgvautoriza.Item("FechaCancela", i).Value.ToString
                    txtmotivo.Text = dgvautoriza.Item("Motivo", i).Value.ToString
                    txtcomentario.Text = dgvautoriza.Item("Comentarios", i).Value.ToString
                    txtrefactura.Text = dgvautoriza.Item("Refactura", i).Value.ToString
                    txtalmacen.Text = dgvautoriza.Item("Almacen", i).Value.ToString
                    txtstatus.Text = dgvautoriza.Item("Status", i).Value.ToString
                    factura_ok = True
                    Exit For
                End If
            Next
        Next 'FIN RECORRE TODO EL DATA GRID VIEW
        'ACTIVA EL GRUPO BOX DE AUTORIZACION
        gbautorizacion.Enabled = True
        cbautorizado.Focus()
    End Sub

    Private Sub brnguardar_Click(sender As Object, e As EventArgs) Handles brnguardar.Click
        'VARIABLE PARA VALIDAR LA CONFIRMACIÓN
        Dim confirma As Integer
        'VALIDA QUE POR LO MENOOS UNO DE LOS CHECK ESTE ACTIVO
        If cbautorizado.Checked = False And cbno_procede.Checked = False Then
            MsgBox("Favor de marcar la casilla correspondiente a lo que se requiere.", MsgBoxStyle.Exclamation, "Alerta de dato")
            cbautorizado.Focus()
            Return
        End If

        'VALIDA QUE ACCION REALIZARA PARA MOSTRAR EL MENSAJE QUE REALMENTE SE REQUIERE
        If cbautorizado.Checked = True Then
            'PREGUNTA PRIMERO AL USUARIO SI REALMENTE DESEA AUTORIZAR O NO LA FACTURA
            'POSTERIOR A ELLO SE REALIZ LA ACCION
            confirma = MessageBox.Show("Se realizará la acción [ " + cbautorizado.Text + " ] en la factura." + " Realmente desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirma = 6 Then
                'MANDA A LLAMAR EL METODO DE ACTUALIZAR LA FACTURA
                actualiza_factura()

                '----- VALIDA QUE MENSAJE ENVIAR
                If envio_cor_ok = True And actualiza_ok = True Then
                    MsgBox("Se Autorizo la factura [ " + txtfactura.Text.ToString + " ] para su cancelación.", MsgBoxStyle.Information, "Datos actualizados y Enviados")
                    envio_cor_ok = False
                    actualiza_ok = False
                    'MANDA A LLAMAR A LOS METODOS DE LLENADO DE GRID Y LIMPIADO DE DATOS
                    limpiar()
                    'llena_grid_autoriza()
                    txtfactura.Text = ""
                ElseIf envio_cor_ok = False And actualiza_ok = True Then
                    MsgBox("Se Autorizo la factura [ " + txtfactura.Text.ToString + " ] para su cancelación. Envio de Autorización fallido. Favor de notificar por otro medio la Autorización.", MsgBoxStyle.Information, "Datos actualizados y Envio Incorrecto")
                    envio_cor_ok = False
                    actualiza_ok = False
                    'MANDA A LLAMAR A LOS METODOS DE LLENADO DE GRID Y LIMPIADO DE DATOS
                    limpiar()
                    txtfactura.Text = ""
                Else
                    MsgBox("Ninguna accion se realizo, favor de contactar a Sistemas.", MsgBoxStyle.Critical, "Acciones fallidas")
                    envio_cor_ok = False
                    actualiza_ok = False
                    'MANDA A LLAMAR A LOS METODOS DE LLENADO DE GRID Y LIMPIADO DE DATOS
                    'limpiar()
                    'txtfactura.Text = ""
                End If

                '-----

                'MANDA A LLENAR EL GRID CON LA ACTUALIZACIONES REALIZADAS
                llena_grid_autoriza()
            Else
                Return 'SI SE SELECCIONA QUE NO, NO HARA NADA
            End If
        ElseIf cbno_procede.Checked = True Then
            'PREGUNTA PRIMERO AL USUARIO SI REALMENTE DESEA NO AUTORIZAR LA FACTURA
            'POSTERIOR A ELLO SE REALIZA LA ACCION
            confirma = MessageBox.Show("Se realizará la acción [ " + cbno_procede.Text + " ] en la factura." + " Realmente desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirma = 6 Then
                'MANDA A LLAMAR EL METODO DE ACTUALIZAR LA FACTURA
                'actualiza_factura()

                '----- VALIDA QUE MENSAJE ENVIAR
                If envio_cor_ok = True And actualiza_ok = True Then
                    MsgBox("No Autorizada la factura [ " + txtfactura.Text.ToString + " ] para su cancelación.", MsgBoxStyle.Information, "Datos actualizados y Enviados")
                    envio_cor_ok = False
                    actualiza_ok = False
                    'MANDA A LLAMAR A LOS METODOS DE LLENADO DE GRID Y LIMPIADO DE DATOS
                    limpiar()
                    'llena_grid_autoriza()
                    txtfactura.Text = ""
                ElseIf envio_cor_ok = False And actualiza_ok = True Then
                    MsgBox("No Autorizada la factura [ " + txtfactura.Text.ToString + " ] para su cancelación. Envio de Estatus fallido. Favor de notificar por otro medio el Estatus.", MsgBoxStyle.Information, "Datos actualizados y Envio Incorrecto")
                    envio_cor_ok = False
                    actualiza_ok = False
                    'MANDA A LLAMAR A LOS METODOS DE LLENADO DE GRID Y LIMPIADO DE DATOS
                    limpiar()
                    'llena_grid_autoriza()
                    txtfactura.Text = ""
                Else
                    MsgBox("Ninguna accion se realizo, favor de contactar a Sistemas.", MsgBoxStyle.Critical, "Acciones fallidas")
                    envio_cor_ok = False
                    actualiza_ok = False
                End If

                '-----

                'MANDA A LLENAR EL GRID CON LA ACTUALIZACIONES REALIZADAS
                llena_grid_autoriza()
            Else
                Return 'SI SE SELECCIONA QUE NO, NO HARA NADA
            End If
        End If
    End Sub

    'METODO PARA ACTUALIZACIÓN DE ESTATUS DE FACTURA =================================================================================================INICIO
    Sub actualiza_factura()
        'VARIBALE CADENA QUE ALMACENA LA CONSULTA
        Dim SQLCadena As String = ""
        Dim SQLUpdate As String = ""

        'VARIABLES PARA ALMACENAMIENTO DE DATOS DE FACTURAS DEL SAP
        Dim DocNum As String = ""
        Dim DocDate As String = ""
        Dim IdWarehouse As String = ""
        Dim NameWarehouse As String = ""
        Dim CardCode As String = ""
        Dim Mes As String = ""
        Dim DocTotal As Double = 0.0
        Dim Status As String = ""

        '-----

        Try 'CAPTURA EL ERROR QUE SE OBTENGA
            'VALIDA QUE ACTUALIZACIÓN SE REALIZARÁ, SI ES NO PROCEDE O EN PROCESO DE CANCELACIÓN
            If cbautorizado.Checked = True Then

                'OBTIENE LOS DATOS DE FACTURAS
                Try 'CAPTURA EL ERROR
                    'CONECTA A LA BASE DE DATOS
                    conexion_universal.conectar_sap()
                    SQLCadena = "SELECT DISTINCT(T0.DocNum), FORMAT(T0.DocDate, 'yyyy-MM-dd') as DocDate, T1.WhsCode, T2.WhsName, "
                    SQLCadena &= "T0.CardCode, MONTH(T0.Docdate) AS Mes, T0.DocTotal "
                    SQLCadena &= "FROM OINV T0 INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry "
                    SQLCadena &= "INNER JOIN OWHS T2 ON T2.WhsCode = T1.WhsCode "
                    SQLCadena &= "WHERE T0.DocNum = " + txtfactura.Text + " "
                    'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
                    conexion_universal.slq_s = New SqlCommand(SQLCadena, conexion_universal.conexion_uni_sap)
                    'EJECUTA LA CONSULTA
                    conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
                    'RECORRE LA CONSULTA
                    While conexion_universal.rd_s.Read
                        'DocNum = conexion_universal.rd_s.GetValue(0).ToString
                        DocNum = conexion_universal.rd_s.Item("DocNum")
                        DocDate = conexion_universal.rd_s.Item("DocDate")
                        'IdWarehouse = conexion_universal.rd_s.GetValue(2).ToString
                        IdWarehouse = conexion_universal.rd_s.Item("WhsCode")
                        'NameWarehouse = conexion_universal.rd_s.GetValue(3).ToString
                        NameWarehouse = conexion_universal.rd_s.Item("WhsName")
                        'CardCode = conexion_universal.rd_s.GetValue(4).ToString
                        CardCode = conexion_universal.rd_s.Item("CardCode")
                        'Mes = conexion_universal.rd_s.GetValue(5).ToString
                        Mes = conexion_universal.rd_s.Item("Mes")
                        'DocTotal = conexion_universal.rd_s.GetDouble(6)
                        DocTotal = CDbl(conexion_universal.rd_s.Item("DocTotal"))
                    End While
                    'CIERRA EL READER
                    conexion_universal.rd_s.Close()
                    conexion_universal.cerrar_conectar_sap()
                Catch ex As Exception
                    MsgBox("Error de consulta o conexión SAP al Obtener datos de Validación: " & ex.Message, MsgBoxStyle.Critical)
                    actualiza_ok = False 'INDICA QUE NO SE ACTUALICE EL DATO
                    'CIERRA EL READER
                    conexion_universal.rd_s.Close()
                    conexion_universal.cerrar_conectar_sap()
                    Return
                End Try 'FIN CAPTURA EL ERROR

                'ALMACENA EL NUEVO STATUS DEL PROCESO DE CANCELACIÓN
                Status = "EN PROCESO DE CANCELACION"

                'CONECTA A LA BASE DE DATOS
                conexion_universal.conectar()
                'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
                SQLUpdate = "UPDATE Documents_Cancel SET status = '" + Status + "', auto_cont = 'SI', com_cont = '" + txtobservaciones.Text.ToString + "' "
                SQLUpdate &= "where doc_num = '" + txtfactura.Text.ToString + "' and status = 'EN AUTORIZACION CONTABLE'"
                'CONSULTA DE LA ACTUALIZACION
                conexion_universal.slq_s = New SqlCommand(SQLUpdate, conexion_universal.conexion_uni)
                'EJECUTA LA CONSULTA
                conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
                conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION ABIERTA
                actualiza_ok = True 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA

                '-----
                'MANDA A LLAMAR EL METODO DE ENVIO DE CORREO FASE 2 PARA ALMACÉN
                EnviaProcesoCancelacion()
            ElseIf cbno_procede.Checked = True Then
                'CONECTA A LA BASE DE DATOS
                conexion_universal.conectar()
                'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
                SQLUpdate = "UPDATE Documents_Cancel SET status = 'NO PROCEDE', autorizada = 'NO', comments_auto = '" + txtobservaciones.Text.ToString + "' "
                SQLUpdate &= "where doc_num = '" + txtfactura.Text.ToString + "' and status = 'PENDIENTE AUTORIZACION'"
                'CONSULTA DE LA ACTUALIZACION
                conexion_universal.slq_s = New SqlCommand(SQLUpdate, conexion_universal.conexion_uni)
                'EJECUTA LA CONSULTA
                conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
                conexion_universal.cerrar_conectar()
                actualiza_ok = True 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA

                '-----

                'MANDA A LLAMAR EL METODO DE ENVIO DE CORREO FASE 4
                EnviaProcesoCancelacion()

            End If
        Catch ex As Exception
            actualiza_ok = False 'INICIALIZA EN FALSO LA VARIABLE DE QUE NO SE ACTUALIZO LA FACTURA
            MsgBox("Error en Actualizar la factura: " + ex.ToString, MsgBoxStyle.Critical, "Error en BD TPD ")
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar()
            Return
        End Try
    End Sub
    'METODO PARA ACTUALIZACIÓN DE ESTATUS DE FACTURA =================================================================================================FIN

    'METODO PARA ENVIO DE CANCELACIÓN DE FACTURA =====================================================================================================INICIO
    Sub EnviaProcesoCancelacion()
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
        Dim emisor As String = "" 'ALMACENA EL NOMBRE DE QUIEN ENVIA
        'VARIABLE DE CADENA PARA CONSULTA
        Dim SQLUsuario As String

        '-----

        'OBTIENE LOS DATOS DE QUIEN ENVIA EL CORREO
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS DEL TPD
            conexion_universal.conectar()
            conexion_universal.slq_s = New SqlCommand("SELECT Id_Usuario, CorreoE, Pswmail, Nombre FROM Usuarios where Id_Usuario = '" + UsrTPM + "'", conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If conexion_universal.rd_s.Read Then
                de = conexion_universal.rd_s.Item("CorreoE")
                pass = conexion_universal.rd_s.Item("Pswmail")
                cc = conexion_universal.rd_s.Item("CorreoE") + ";"
                emisor = conexion_universal.rd_s.Item("Nombre")
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
            envio_cor_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
            Return
        End Try 'FIN CAPTURA EL ERROR

        '------

        Try 'CAPTURA ERROR DE ENVIO DE CORREO
            'VALIDA SI SE PUEDE HACER EL ALMACEN DE ENVIOS
            If envio_ok = True Then
                'ADJUNTA LOS CORREOS DE ENVIO VALIDADOS QUE TENGAN ENVIO
                If de <> "" Then
                    'AQUI SE CAMBIA LINEA CUANDO SE IMPLEMENTE ======================REVISAR 
                    Msg.From = New System.Net.Mail.MailAddress(de, emisor, System.Text.Encoding.UTF8) 'DE QUIEN SE ENVIA
                    'Msg.From = New System.Net.Mail.MailAddress(de, "Ana Espejel", System.Text.Encoding.UTF8) 'DE QUIEN SE ENVIA
                    'Msg.From = New System.Net.Mail.MailAddress(de, "Uriel Toralva", System.Text.Encoding.UTF8) 'DE QUIEN SE ENVIA
                    envio_de_ok = True
                Else
                    envio_de_ok = False
                    envio_cor_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
                    MsgBox("El correo de Autorización de cancelación no se envío, debido a no tener Emisor. Favor de avisar la autorización de manera Telefónica.", MsgBoxStyle.Exclamation, "Alerta de Emisor")
                    Return
                End If
            End If

            '------

            'VALIDA SI EL ENVIO SE HARA POR AUTORIZADO O NO PROCEDE (FASE 2 O FASE 4)
            If cbautorizado.Checked = True Then
                'OBTIENE LOS CORREO DE COPIA PARA ENVIO
                Try 'CAPTURA EL ERROR
                    'CONECTA A LA BASE DE DATOS DEL TPD
                    conexion_universal.conectar()
                    'CONSULTA DE OBTENCIÓN DE FASE DE ENVIO
                    'conexion_universal.slq_s = New SqlCommand("SELECT fase_nombre, correo FROM documents_correos where fase_envio = 2", conexion_universal.conexion_uni)

                    conexion_universal.slq_s = New SqlCommand("SELECT fase_nombre, correo FROM documents_correos LEFT JOIN Documents_Cancel ON Documents_Cancel.id_warehouse = Documents_correos.WHSCODE2  where fase_envio = 2 AND doc_num=" + txtfactura.Text + "", conexion_universal.conexion_uni)

                    'EJECUTA LA CONSULTA
                    conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()



                    'para = "asistemas@tractopartesdiamante.com.mx"



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
                    envio_cor_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
                    Return
                End Try 'FIN CAPTURA EL ERROR

                '-----

                'COLOCA EL ASUNTO DEL CORREO SI ES PARA ALMACEN O CONTABILIDAD
                Titulo = "Solicitud de Cancelación Factura [ " + txtfactura.Text + " ]"

                'ADJUNTA EL TITULO AL CORREO
                Msg.Subject = Titulo

                'ENCRIPTA EL ASUNTO DEL MENSAJE
                Msg.SubjectEncoding = System.Text.Encoding.UTF8

                Dim vMensaje As String = ""
                'SE COLOCA EL ENCABEZADO DEL CORREO CORRESPONDIENTE PARA VER SI ES PARA ALMACÉN O CONTABILIDAD
                'COLOCA EL ENCABEZADO Y EL CUERPO DEL MENSAJE A MOSTRAR EN EL CORREO
                vMensaje = "<html><head></head><body><h2>Cancelación Factura: " & txtfactura.Text.ToString() & "</h2>"
                vMensaje &= "<p>Solicita: " & txtsolicita.Text.ToString()
                vMensaje &= "<br>Requiere Refactura: " & txtrefactura.Text.ToString()
                vMensaje &= "<br>Motivo: " & txtmotivo.Text.ToString()
                vMensaje &= "<br>Comentario: " & txtcomentario.Text.ToString()
                vMensaje &= "<br>Estatus: EN PROCESO DE CANCELACION"
                vMensaje &= "<br>Observaciones: " & txtobservaciones.Text.ToString() & "</p><br>"
                vMensaje &= "<p>Estimado usuario:</p><p>Solicito de su apoyo para la creación de la NC de la factura descrita, cualquier duda o aclaración favor de indicarme.</p><p>Saludos cordiales!!!</p>"
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
                envio_cor_ok = True

                '-----

            ElseIf cbno_procede.Checked = True Then

                '-----

                'OBTIENE LOS CORREO DE COPIA PARA ENVIO Y PARA QUIEN SERIA
                Try 'CAPTURA EL ERROR
                    'CONECTA A LA BASE DE DATOS DEL TPD
                    conexion_universal.conectar()
                    'CONSULTA CON BASE A LA FACTURA QUE NO PROCEDE PARA QUIEN SE LE ENVIARIA, CON BASE AL USUARIO
                    SQLUsuario = "select distinct(T0.user1), T0.name_user, T1.CorreoE "
                    SQLUsuario &= "from Documents_Cancel T0 INNER JOIN Usuarios T1 ON T0.user1 = T1.Id_Usuario "
                    SQLUsuario &= "where T1.Nombre = '" + txtsolicita.Text + "' "
                    conexion_universal.slq_s = New SqlCommand(SQLUsuario, conexion_universal.conexion_uni)
                    'EJECUTA LA CONSULTA
                    conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
                    If conexion_universal.rd_s.Read Then
                        para = conexion_universal.rd_s.Item("CorreoE")
                        'CIERRA EL READER
                        conexion_universal.rd_s.Close()
                    Else
                        envio_cor_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
                        MsgBox("No se cuenta con correo Electronico del usuario quien Solicita la cancelación.", MsgBoxStyle.Exclamation, "Alerta de Correo")
                        'CIERRA EL READER
                        conexion_universal.rd_s.Close()
                        'CIERRA LA CONEXION
                        conexion_universal.cerrar_conectar()
                        Return
                    End If

                    '-----

                    'COLOCA EL PRINCIPAL DESTINATARIO OBTENIDO EN LA CONSULTA ANTERIOR
                    Msg.To.Add(para)

                    '-----

                    'CONSULTA DE OBTENCIÓN DE LOS MOTIVOS
                    'conexion_universal.slq_s = New SqlCommand("SELECT fase_nombre, correo FROM documents_correos where fase_envio = 4", conexion_universal.conexion_uni)
                    conexion_universal.slq_s = New SqlCommand("SELECT fase_nombre, correo FROM documents_correos LEFT JOIN Documents_Cancel ON Documents_Cancel.id_warehouse = Documents_correos.WHSCODE2  where fase_envio = 4 AND doc_num=" + txtfactura.Text + "", conexion_universal.conexion_uni)

                    'EJECUTA LA CONSULTA
                    conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()

                    'RECORRE LA CONSULTA
                    While conexion_universal.rd_s.Read
                        'VALIDA QUE ACCION MANDAR
                        If conexion_universal.rd_s.Item("fase_nombre") = "P" Then
                            Msg.To.Add(conexion_universal.rd_s.Item("correo"))
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
                    envio_cor_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
                    Return
                End Try 'FIN CAPTURA EL ERROR

                '-----

                'COLOCA EL ASUNTO DEL CORREO
                Titulo = "Solicitud de Cancelación Factura [ " + txtfactura.Text + " ]"

                'ADJUNTA EL TITULO AL CORREO
                Msg.Subject = Titulo

                'ENCRIPTA EL ASUNTO DEL MENSAJE
                Msg.SubjectEncoding = System.Text.Encoding.UTF8

                'COLOCA EL ENCABEZADO Y EL CUERPO DEL MENSAJE A MOSTRAR EN EL CORREO
                Dim vMensaje As String = "<html><head></head><body><h2>Cancelación Factura: " & txtfactura.Text.ToString() & "</h2>"
                vMensaje &= "<p>Solicita: " & txtsolicita.Text.ToString()
                vMensaje &= "<br>Requiere Refactura: " & txtrefactura.Text.ToString()
                vMensaje &= "<br>Motivo: " & txtmotivo.Text.ToString()
                vMensaje &= "<br>Comentario: " & txtcomentario.Text.ToString()
                'vMensaje &= "<br>Estatus: " & txtstatus.Text.ToString() & "</p><br>"
                vMensaje &= "<br>Estatus: NO PROCEDE"
                vMensaje &= "<br>Observaciones: " & txtobservaciones.Text.ToString() & "</p><br>"
                vMensaje &= "<p>Estimado usuario:</p><p>Le informamos que la solicitud de cancelación de la factura requerida descrita lineas arriba, No Procede, los motivos están descritos en las observaciones de este mismo correo, "
                vMensaje &= "cualquier duda o aclaración favor de indicarme.</p><p>Saludos cordiales!!!</p>"
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
                envio_cor_ok = True

                '-----
            End If

        Catch exc As Exception
            'INICIALIZA LA VARIABLE EN FALSO SI EL ENVIO NO SE COMPLETO
            envio_cor_ok = False
            'MENSAJE DE ERROR DE ENVIO DE CORREO
            MessageBox.Show("No fue posible enviar email de la Autorización de Cancelación," & Chr(13) & "Favor de avisar la Autorización de manera Telefonica..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !",
            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub
    'METODO DE ENVIO DE CANCELACION DE FACTURA ==================================================================================================================FIN




End Class