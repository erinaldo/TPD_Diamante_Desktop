Imports System.Data.SqlClient
Imports System.Net.Mail
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Public Class frmcancelacion_nc

  'VARIABLE GLOBAL DEL FORMULRIO
  Dim envio_adj_ok As Boolean = False
  Dim actualiza_adj_ok As Boolean = False
  Dim NC As Integer 'ALMACENA EL NUMERO DE NC GENERADO
  Dim DocEntry_NC As String 'ALMACENA EL DOCENTRY DE LA NC
  Dim DocEntry_Fat As String 'ALMACENA EL DOCENTRY DE LA FACTURA
  Dim status_fac_env As String 'ALMACENA EN QUE ESTATUS SE QUEDA LA FACTURA
  Dim FacturaNombre As String  'ALMACENA LA FACTURA Y RUTA PARA LA EXPORTACION DE LA MISMA
  Dim DocDate As DateTime

  Private Sub frmcancelacion_nc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
      .Columns(0).Width = 170
      .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns(0).ReadOnly = False
      'FACTURA
      .Columns(1).Width = 80
      .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      '.Columns(1).DefaultCellStyle.Format = "$ ###,###,###.00"
      .Columns(1).ReadOnly = False
      'FECHA FACTURA
      .Columns(2).Width = 70
      .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(2).ReadOnly = False
      'FECHA SOLICITUD
      .Columns(3).Width = 95
      .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(3).ReadOnly = False
      'MOTIVO
      .Columns(4).Width = 150
      .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns(4).ReadOnly = False
      'COMENTARIOS
      .Columns(5).Width = 180
      .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns(5).ReadOnly = False
      'REQUIERE FACTURA
      .Columns(6).Width = 90
      .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(6).ReadOnly = False
      'REFACTURA (SUSTITUYE)
      .Columns(7).Width = 70
      .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(7).ReadOnly = False
      'ALMACEN
      .Columns(8).Width = 80
      .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(8).ReadOnly = False
      'NOTA DE CREDITO
      .Columns(9).Width = 80
      .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(9).ReadOnly = False
      'FECHA DE NOTA DE CREDITO
      .Columns(10).Width = 80
      .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(10).ReadOnly = False
      'ESTATUS
      .Columns(11).Width = 180
      .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns(11).ReadOnly = False
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
      SQLautoriza &= "FROM Documents_Cancel WHERE status = 'EN PROCESO DE CANCELACION' "
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
            Return
          End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
        End If
      End While
      conexion_universal.cerrar_conectar()
    Catch ex As Exception
      MsgBox("Error de consulta o conexión TPD en llenado de GRID: " & ex.Message, MsgBoxStyle.Critical)
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
    cmbstatus.SelectedIndex = -1
    'INICIALIZA LA NC EN NADA
    NC = 0
    DocEntry_Fat = ""
    DocEntry_NC = ""

    '-----
  End Sub

  Private Sub dgvautoriza_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvautoriza.CellContentClick
    'PROPIEDADES RTEQUERIDAS PARA ESTA FUNCION
    'MULTISELECT = TRUE
    'SELECTMODE = FullRowSelect
    txtfactura.Text = dgvautoriza.Item("Factura", dgvautoriza.CurrentRow.Index).Value.ToString
    'MANDA A LLAMAR AL METODO LIMPIAR
    limpiar()
    'MUESTRA LOS DATOS CON BASE A LA FACTURA SELECIONADA
    btnmostrar.PerformClick()
  End Sub
  Private Sub dgvautoriza_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvautoriza.CellMouseClick
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
          factura_ok = True
          Exit For
        End If
      Next
    Next 'FIN RECORRE TODO EL DATA GRID VIEW
  End Sub

  Private Sub brnguardar_Click(sender As Object, e As EventArgs) Handles brnguardar.Click
    Dim confirma As Integer     'VARIABLE PARA VALIDAR LA CONFIRMACIÓN

    'VALIDA QUE POR LO MENOOS UNO DE LOS CHECK ESTE ACTIVO
    If txtfactura.Text = "" Or txtfactura.Text = " " Then
      MsgBox("Favor de seleccionar una factura para su busqueda.", MsgBoxStyle.Exclamation, "Alerta de dato")
      cmbstatus.Focus()
      Return
    End If
    'VALIDA QUE POR LO MENOOS UNO DE LOS CHECK ESTE ACTIVO
    If cmbstatus.SelectedIndex = -1 Then
      MsgBox("Favor de colocar el Estatus valido.", MsgBoxStyle.Exclamation, "Alerta de dato")
      cmbstatus.Focus()
      Return
    End If

    '-----

    'VALIDA QUE ACCION REALIZARA PARA MOSTRAR EL MENSAJE QUE REALMENTE SE REQUIERE
    If cmbstatus.SelectedIndex <> -1 Then
      'PREGUNTA PRIMERO AL USUARIO SI REALMENTE DESEA AUTORIZAR O NO LA FACTURA
      'POSTERIOR A ELLO SE REALIZ LA ACCION
      confirma = MessageBox.Show("Se realizará la acción [ " + cmbstatus.Text + " ] en la factura." + " Realmente desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
      If confirma = 6 Then
        'MANDA A LLAMAR EL METODO DE ACTUALIZAR LA FACTURA
        actualiza_factura()

        '----- VALIDA QUE MENSAJE ENVIAR
        If envio_adj_ok = True And actualiza_adj_ok = True Then
          MsgBox("Se Cancelo la factura [ " + txtfactura.Text.ToString + " ].", MsgBoxStyle.Information, "Datos actualizados y Enviados")
          envio_adj_ok = False
          actualiza_adj_ok = False
          status_fac_env = ""
          'INICIALIZA LA NC EN NADA
          NC = 0
          DocEntry_NC = ""
          'MANDA A LLAMAR A LOS METODOS DE LLENADO DE GRID Y LIMPIADO DE DATOS
          limpiar()
          'llena_grid_autoriza()
          txtfactura.Text = ""
        ElseIf envio_adj_ok = False And actualiza_adj_ok = True Then
          MsgBox("Se Cancelo la factura [ " + txtfactura.Text.ToString + " ]. Envio de Cancelación fallido. Favor de notificar por otro medio la Cancelación.", MsgBoxStyle.Information, "Datos actualizados y Envio Incorrecto")
          envio_adj_ok = False
          actualiza_adj_ok = False
          status_fac_env = ""
          'INICIALIZA LA NC EN NADA
          NC = 0
          DocEntry_NC = ""
          'MANDA A LLAMAR A LOS METODOS DE LLENADO DE GRID Y LIMPIADO DE DATOS
          limpiar()
          'llena_grid_autoriza()
          txtfactura.Text = ""
        Else
          MsgBox("Ninguna accion se realizo, favor de contactar a Sistemas.", MsgBoxStyle.Critical, "Acciones fallidas")
          envio_adj_ok = False
          actualiza_adj_ok = False
          status_fac_env = ""
          'INICIALIZA LA NC EN NADA
          NC = 0
          DocEntry_NC = ""
        End If

        '-----

        'MANDA A LLENAR EL GRID CON LA ACTUALIZACIONES REALIZADAS
        llena_grid_autoriza()
      Else
        Return 'SI SE SELECCIONA QUE NO, NO HARA NADA
      End If
    End If
  End Sub

  Sub actualiza_factura()
    Dim SQLUpdate, SQLUpdateNC As String     'VARIBALE CADENA QUE ALMACENA LA CONSULTA
    Dim SQLEstatus As String    'VARIABLE CADENA PARA LA CONSULTA DE OBTENCION DEL ESTATUS
    Dim SQLNota As String    'VARIABLE CADENA PARA LA CONSULTA DE OBTENCION DEL ESTATUS
    Dim estado As String = ""        'VARIABLE QUE ALMACENARÁ EL ESTADO EN EL CUAL VAYA ESTAR EL PROCESO
    Try 'CAPTURA EL ERROR QUE SE OBTENGA
      'VALIDA QUE ACTUALIZACIÓN SE REALIZARÁ, SI ES NO PROCEDE O EN PROCESO DE CANCELACIÓN
      If cmbstatus.SelectedIndex <> -1 Then

        '-----
        Try 'INICIO TRY VALIDACIONES
          Try 'INICIO TRY CONEXION SAP
            'ABRE LA CONEXION AL TPD
            conexion_universal.conectar_sap()
            'CONSULTA PARA SABER SI YA TIENE CREADA LA NOTA DE CREDITO
            SQLNota = "SELECT T0.DocEntry as IdentificadorFact, T0.DocNum as Factura, T0.DocDate as FechaFactura, ISNULL(T2.DocNum, 0) as NC, T2.DocEntry, ISNULL(CONVERT(varchar(35), FORMAT(T2.DocDate, 'yyyy-MM-dd'), 126),'') as FechaNC "
            SQLNota &= "FROM OINV T0 INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry "
            SQLNota &= "left join ORIN T2 ON T1.TrgetEntry = T2.DocEntry "
            SQLNota &= "WHERE T0.DocNum = " + txtfactura.Text + " "
            'ALMACENA LA CONSULTA EN UN COMMAND PARA PODER HACER LA EJECUCION DE LA MISMA
            conexion_universal.slq_s = New SqlCommand(SQLNota, conexion_universal.conexion_uni_sap)
            'EJECUTA LA CONSULTA 
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'EXECUTA EL READER
            conexion_universal.rd_s.Read()
            'VALIDA QUE SI TENGA UNA NOTA DE CREDITO LA FACTURA
            If conexion_universal.rd_s.Item("NC") <> 0 Then
              '-----

              'ALMACENA LA NOTA DE CREDITO QUE SE OBTIENE
              NC = conexion_universal.rd_s.Item("NC")
              DocEntry_NC = conexion_universal.rd_s.Item("DocEntry")

              '-----
              Try  'INICIO TRY TPD
                'CONECTA A LA BD DE TPD
                conexion_universal.conectar()
                'CONSULTA PARA ACTUALIZAR LA NC DE LA FACTURA CORRESPONDIENTE
                SQLUpdateNC = "UPDATE Documents_Cancel SET doc_num_nc = '" + conexion_universal.rd_s.Item("NC").ToString + "', "
                SQLUpdateNC &= "doc_date_nc = '" + conexion_universal.rd_s.Item("FechaNC") + "' "
                SQLUpdateNC &= "WHERE doc_num = '" + txtfactura.Text + "' "
                'ALAMCENA EN UN COMMAND LA CONSULTA
                conexion_universal.sql_up = New SqlCommand(SQLUpdateNC, conexion_universal.conexion_uni)
                'EJECUTA LA CONSULTA
                conexion_universal.rd_up = conexion_universal.sql_up.ExecuteScalar
                'CIERRA LA CONEXION
                conexion_universal.cerrar_conectar()
              Catch ex As Exception
                MsgBox("Error en consulta o conexión al Actualizar la NC: " + ex.ToString, MsgBoxStyle.Critical, "Error de BD TPD")
                actualiza_adj_ok = False 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA
                conexion_universal.cerrar_conectar()
                Return
              End Try  'FIN TRY TPD

              '-----
            Else
              '-----
              MsgBox("No es posible Actualizar Estatus de factura, debido a que no se ha realizado la Cancelación [NOTA DE CREDITO] correspondiente.", MsgBoxStyle.Exclamation, "Alerta de Cancelación")
              actualiza_adj_ok = False 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA
              'CIERRA EL READER
              conexion_universal.rd_s.Close()
              'CIERRO LA CONEXION DEL SAP POR CAUSA DE ERROR
              conexion_universal.cerrar_conectar_sap()
              Return
            End If 'FIN VALIDA SI EXISTE UNA NC
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            'CIERRO LA CONEXION DEL SAP SI SE ACTUALIZA LA FACTURA EN SU CAMPO NC
            conexion_universal.cerrar_conectar_sap()
            'SELECT T0.DocEntry as IfentificadorFact, T0.DocNum as Factura, T0.DocDate as FechaFactura, ISNULL(T2.DocNum, '') as NC, ISNULL(CONVERT(varchar(35), FORMAT(T2.DocDate, 'yyyy-MM-dd'), 126),'') as FechaNC
            'FROM OINV T0 INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry
            'left join ORIN T2 ON T1.TrgetEntry = T2.DocEntry
            'WHERE T0.DocNum = 1025956
          Catch ex As Exception
            MsgBox("Error en consulta o conexión al Obtener la NC: " + ex.ToString, MsgBoxStyle.Critical, "Error de BD SAP")
            actualiza_adj_ok = False 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar_sap()
            Return
          End Try 'FIN TRY DE CONEXION SAP

          '-----

          'ABRE LA CONEXION AL TPD
          conexion_universal.conectar()
          'OBTENER EL SI LA CANCELACION VA REQUERIR REFACTURACION PARA SABER QUE ESTATUS PONER
          'CONSULTA SQL
          SQLEstatus = "SELECT  * FROM Documents_Cancel WHERE doc_num = '" + txtfactura.Text.ToString + "' and status = 'EN PROCESO DE CANCELACION' AND refactura = 'SI' "
          'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
          conexion_universal.slq_s = New SqlCommand(SQLEstatus, conexion_universal.conexion_uni)
          'EJECUTA LA CONSULTA
          conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
          'RECORRE LA CONSULTA
          If conexion_universal.rd_s.Read Then
            estado = "PENDIENTE REFACTURACION"  'SI SE CUMPLE ESTO EL ESTADO QUEDARA A ESPERA DEL NUMERO DE RE-FACTURA
          Else
            estado = "FINALIZADO"                   'EN CASO CONTRARIO SE FINALIZARÁ EL PROCESO
          End If
          'ALAMACENA EN UNA VARIABLE GLOBAL DEL FORMULARIO EL ESTADO DE LA FACTURA
          status_fac_env = estado
          conexion_universal.cerrar_conectar()
        Catch ex As Exception
          MsgBox("Error de consulta o conexión, Favor de intentar más tarde.", MsgBoxStyle.Critical, "Conexión fallida")
          actualiza_adj_ok = True 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA
          conexion_universal.cerrar_conectar()
          Return
        End Try 'FIN TRY VALIDACIONES

        '------

        'ABRE LA CONEXION A TPD
        conexion_universal.conectar()
        'VALIDA QUE ESTADO PONER EN LA ACTUALIZACIÓN
        If estado = "FINALIZADO" Then
          'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
          SQLUpdate = "UPDATE Documents_Cancel SET status = '" + estado + "', doc_date_hour_nc = getdate() "
          SQLUpdate &= "where doc_num = '" + txtfactura.Text.ToString + "' and status = 'EN PROCESO DE CANCELACION'"
        Else
          SQLUpdate = "UPDATE Documents_Cancel SET status = '" + estado + "', doc_date_hour_nc = getdate() "
          SQLUpdate &= "where doc_num = '" + txtfactura.Text.ToString + "' and status = 'EN PROCESO DE CANCELACION'"
        End If
        'CONSULTA DE LA ACTUALIZACION
        conexion_universal.slq_s = New SqlCommand(SQLUpdate, conexion_universal.conexion_uni)
        'EJECUTA LA CONSULTA
        conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
        conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION ABIERTA
        actualiza_adj_ok = True 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA

        '-----
        'MANDA A LLAMAR EL METODO DE ENVIO DE CORREO FASE 2
        EnviaProcesoCancelacion()

      End If
    Catch ex As Exception
      actualiza_adj_ok = False 'INICIALIZA EN FALSO LA VARIABLE DE QUE NO SE ACTUALIZO LA FACTURA
      MsgBox("Error en Actualizar la factura: " + ex.ToString, MsgBoxStyle.Critical, "Error en BD TPD ")
      conexion_universal.cerrar_conectar()
      Return
    End Try
  End Sub

  Sub EnviaProcesoCancelacion()
    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
    '//VARIABLE PARA LA EL CORREO
    Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
    Dim emisor As String = "" 'ALMACENA EL NOMBRE DE QUIEN ENVIA
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

    'DECLARACION DE VARIABLE DE REPORTE Y INSTANCIA DEL MISMO
    Dim DocFacturas As ReportDocument
    DocFacturas = New ReportDocument()
    Dim DocKey = String.Empty
    Dim _rutaPDF As String '// ALMACENA LA RUTA DEL PDF
    Dim fecha11082018 As DateTime = Convert.ToDateTime("2018-08-11").Date '//VARIABLES PARA VALIDAR QUE FORMATO CREAR
    Dim fecha01082018 As DateTime = Convert.ToDateTime("2018-08-01").Date
    Dim fechainvoice As DateTime = Convert.ToDateTime("2019-02-11").Date
    Dim fechaMigracionAgo2020 As DateTime = Convert.ToDateTime("2020-08-23").Date

    '= Convert.ToDateTime("2018-10-16").Date '//VARIABLES PARA VALIDAR QUE FORMATO CREAR

    '-----

    'OBTIENE LOS DATOS DE QUIEN ENVIA EL CORREO
    Try 'CAPTURA EL ERROR
      '****************************************OTRA OPCION SI SE QUIERE QUE MANDE EL CORREO DE CANCELACIONES
      ''CONECTA A LA BASE DE DATOS DEL TPD
      'conexion_universal.conectar()
      'conexion_universal.slq_s = New SqlCommand("SELECT responsable, correo, pass FROM Documents_correos WHERE id_correo = 6 AND fase_envio = 3 AND fase_nombre = 'D'", conexion_universal.conexion_uni)
      ''EJECUTA LA CONSULTA
      'conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
      ''RECORRE LA CONSULTA
      'If conexion_universal.rd_s.Read Then
      '    de = conexion_universal.rd_s.Item("correo")
      '    pass = conexion_universal.rd_s.Item("pass")
      '    cc = conexion_universal.rd_s.Item("correo") '+ ";"
      '    emisor = conexion_universal.rd_s.Item("responsable")
      'End If
      ''ALMACENA BANDERA PARA EL ENVIO SI TIENE EMISOR
      'envio_ok = True
      ''CIERRA LA CONEXION
      'conexion_universal.cerrar_conectar()
      '****************************************OTRA OPCION SI SE QUIERE QUE MANDE EL CORREO DE CANCELACIONES
      '****************************************OTRA OPCION SI NO SE QUIERE QUE MANDE EL CORREO PERSONAL QUE MANDE EL DE CANCELACIONES
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
      'CIERRA LA CONEXION
      conexion_universal.cerrar_conectar()
      '****************************************OTRA OPCION
    Catch ex As Exception
      MsgBox("Error de consulta o conexión TPD para Obtencion de Usuario y correo: " & ex.Message, MsgBoxStyle.Critical, "Error de BD TPD")
      conexion_universal.cerrar_conectar()
      envio_adj_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
      Return
    End Try 'FIN CAPTURA EL ERROR

    '------

    'OBTIENE EL DESTINATARIO DE QUIEN SOLICITO PARA EL ENVIO DE NOTIFICACION DE CANCELACION
    Try 'CAPTURA EL ERROR
      'CONECTA A LA BASE DE DATOS DEL TPD
      conexion_universal.conectar()
      conexion_universal.slq_s = New SqlCommand("SELECT T0.user1, T1.CorreoE, T1.Pswmail " +
                            "FROM Documents_Cancel T0 inner join Usuarios T1 ON T0.user1 = T1.Id_Usuario " +
                            "WHERE doc_num = " + txtfactura.Text.ToString() + " ", conexion_universal.conexion_uni)
      'EJECUTA LA CONSULTA
      conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
      'RECORRE LA CONSULTA
      If conexion_universal.rd_s.Read Then
        para = conexion_universal.rd_s.Item("CorreoE")
      End If
      'ALMACENA BANDERA PARA EL ENVIO SI TIENE EMISOR
      envio_ok = True
      'CIERRA LA CONEXION
      conexion_universal.cerrar_conectar()
      'AGREGA EL CORREO DE QUIEN SOLICITO POR PRIMERA VEZ LA CANCELACION PARA QUE SE LE ENVIE LA CONFIRMACIÓN DE LA CANCELACION
      Msg.To.Add(para)
    Catch ex As Exception
      MsgBox("Error de consulta o conexión TPD para Obtencion de Usuario solicitante: " & ex.Message, MsgBoxStyle.Critical, "Error de BD TPD")
      conexion_universal.cerrar_conectar()
      envio_adj_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
      Return
    End Try 'FIN CAPTURA EL ERROR

    '------

    Try 'CAPTURA ERROR DE ENVIO DE CORREO
      'VALIDA SI SE PUEDE HACER EL ALMACEN DE ENVIOS
      If envio_ok = True Then
        'ADJUNTA LOS CORREOS DE ENVIO VALIDADOS QUE TENGAN ENVIO
        If de <> "" Then
          'OTRA OPCION UNIDA A LA ANTERIOR
          'Msg.From = New System.Net.Mail.MailAddress(de, txtsolicita.Text, System.Text.Encoding.UTF8) 'DE QUIEN SE ENVIA
          Msg.From = New System.Net.Mail.MailAddress(de, emisor, System.Text.Encoding.UTF8) 'DE QUIEN SE ENVIA
          envio_de_ok = True
        Else
          envio_de_ok = False
          envio_adj_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
          MsgBox("El correo de aviso de cancelación no se envío, debido a no tener Emisor. Favor de avisar la cancelación de manera Telefónica.", MsgBoxStyle.Exclamation, "Alerta de Emisor")
          Return
        End If
      End If

      'VALIDA QUE EFECTIVAMENTE SE HAYA COLOCADO UN ESTATUS
      If cmbstatus.SelectedIndex <> -1 And envio_de_ok = True Then
        'OBTIENE LOS CORREO DE COPIA PARA ENVIO
        Try 'CAPTURA EL ERROR
          'CONECTA A LA BASE DE DATOS DEL TPD
          conexion_universal.conectar()
                    'CONSULTA DE OBTENCIÓN DE LOS MOTIVOS
                    conexion_universal.slq_s = New SqlCommand("SELECT fase_nombre, correo  FROM documents_correos  LEFT JOIN Usuarios ON Usuarios.Almacen = Documents_correos.WHSCODE2  where fase_envio = 3 AND Id_Usuario ='" + UsrTPM + "'", conexion_universal.conexion_uni)
                    'EJECUTA LA CONSULTA
                    conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()

          'ESTO SE TENDRA QUE ELIMINAR AL IMPLEMENTAR
          'para = "asistemas@tractopartesdiamante.com.mx"

          'RECORRE LA CONSULTA
          While conexion_universal.rd_s.Read

            'VALIDA QUE ACCION MANDAR
            If conexion_universal.rd_s.Item("fase_nombre") = "P" Then
              'ESTA LINEA TENDRA QUE IR AL IMPLEMENTAR Y ELIMINAR LA DE ABAJO
              Msg.To.Add(conexion_universal.rd_s.Item("correo"))
              'Msg.To.Add(para)
            ElseIf conexion_universal.rd_s.Item("fase_nombre") = "C" Then
              Msg.CC.Add(conexion_universal.rd_s.Item("correo"))
            ElseIf conexion_universal.rd_s.Item("fase_nombre") = "CO" Then
              Msg.Bcc.Add(conexion_universal.rd_s.Item("correo"))
            End If
          End While
          conexion_universal.rd_s.Close()
          'CIERRA LA CONEXION
          conexion_universal.cerrar_conectar()
        Catch ex As Exception
          MsgBox("Error de consulta o conexión TPD para Obtencion de los correos: " & ex.Message, MsgBoxStyle.Critical, "Error de BD")
          conexion_universal.cerrar_conectar()
          envio_adj_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
          Return
        End Try 'FIN CAPTURA EL ERROR

        '------

        'OBTIENE EL DOCENTRY DE LA FACTURA PARA PODER USARLA EN LA CREACION DEL PDF
        Try 'CAPTURA EL ERROR
          'CONECTA A LA BASE DE DATOS DEL SAP
          conexion_universal.conectar_sap()
          conexion_universal.sql_tem = New SqlCommand("SELECT DocEntry, DocNum " +
                            "FROM OINV WHERE DocNum = " + txtfactura.Text.ToString() + " ", conexion_universal.conexion_uni_sap)
          'EJECUTA LA CONSULTA
          conexion_universal.rd_tem = conexion_universal.sql_tem.ExecuteReader()
          'RECORRE LA CONSULTA
          If conexion_universal.rd_tem.Read Then
            DocEntry_Fat = conexion_universal.rd_tem.Item("DocEntry")
          End If
          'ALMACENA BANDERA PARA EL ENVIO SI TIENE EMISOR
          envio_ok = True
          'CIERRA LA CONEXION
          conexion_universal.cerrar_conectar_sap()
        Catch ex As Exception
          MsgBox("Posiblemente no se ejecute la creación del PDF: " & ex.Message, MsgBoxStyle.Critical, "Error de BD SAP")
          conexion_universal.cerrar_conectar_sap()
        End Try 'FIN CAPTURA EL ERROR

                '-----

                'ADJUNTA LA NC Y LA CANCELACIÓN

                'MANDA A LLAMAR AL METODO DE OBTENER    ----------CREO QUE NO SE OCUPA

                'ALAMCENA LA FECHA Y LA CONVIERTE EN VALIDA PARA COMPRAR
                DocDate = Convert.ToDateTime(txtfecha_factura.Text).Date
        '//VALIDA LA FECHA PARA SABER QUE FORMATO EJECUTAR

        If (DocDate <= fecha11082018) Then
                    DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\NC 3_3 19ABR2018.rpt") ' //RUTA DEL ARCHIVO .rpt
                ElseIf (DocDate > fecha11082018 And DocDate < fechainvoice) Then
                    DocFacturas.Load("\\" & conexion_universal.RutaReportes & "b1_shr\TPD\NC 3.3_93_05.rpt") ' //RUTA DEL ARCHIVO .rpt
                ElseIf (DocDate > fechainvoice And DocDate < fechaMigracionAgo2020) Then  'Formato pre migracion AGO-2020
                    DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\NC 3.3_93_06_AddOn_DLL.rpt") ' //RUTA DEL ARCHIVO .rpt
                Else
                    DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\NC 3.3-93_6AddOn9NF2020.rpt") ' //RUTA DEL ARCHIVO .rpt
                End If

        'If (DocDate <= fecha11082018) Then
        '  DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\NC 3_3 19ABR2018.rpt") ' //RUTA DEL ARCHIVO .rpt
        'Else
        '  DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\NC 3.3_93_05.rpt") ' //ruta del archivo .rpt
        'End If
        '//PARAMETROS DE CONEXION PARA EL RPT
        Dim tInfo As TableLogOnInfo = New TableLogOnInfo()
        Dim ConnectionInfo As ConnectionInfo = tInfo.ConnectionInfo

        ConnectionInfo.Password = conexion_universal.cPassword
        ConnectionInfo.UserID = conexion_universal.cUserID
        ConnectionInfo.ServerName = conexion_universal.cServerName ' // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS
        ConnectionInfo.DatabaseName = conexion_universal.cDatabaseNameSAP

        '//PASA EL PARAMETRO AL ARCHIVO RPT (DocEntry)
        DocFacturas.SetParameterValue("DocKey@", DocEntry_NC)

        'ESTABLE LA CONEXION CON EL REPORTE
        SetTableLocation(DocFacturas, ConnectionInfo)

        'ALMACENA RUTA Y NOMBRE DEL ARCHIVO EN ESTE CASO EL DOCNUM DE LA NC
        _rutaPDF = NC.ToString + ".pdf"
        '//GENERA PDF EN CARPETA TEMPORAL
        Try
          '//ALMACENA EL PDF EN LA RUTA TEMPORAL
          DocFacturas.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, _rutaPDF)
          'createPDF_OK = True
        Catch ex As Exception
          MessageBox.Show("No se pudo crear el archivo PDF de la NC: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        '//ADJUNTAR LOS ARCHIVOS PDF'S Y XML'S
        Try
          Dim ArchiveRutaPDF As Attachment = New System.Net.Mail.Attachment(_rutaPDF)
          Msg.Attachments.Add(ArchiveRutaPDF)
        Catch ex As Exception
          MessageBox.Show("Error al adjuntar el archivo PDF de la NC: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        '//CIERRA EL DOCUEMTNO DE RPT
        DocFacturas.Close()
        'INICIALIZA EN NADA LA RUTA
        _rutaPDF = ""

        'ADJUNTA Y CREA EL PDF DE FACTURA

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

        '//PASA EL PARAMETRO AL ARCHIVO RPT (DocEntry)
        DocFacturas.SetParameterValue("DocKey@", DocEntry_Fat)

        'ESTABLE LA CONEXION CON EL REPORTE
        SetTableLocation(DocFacturas, ConnectionInfo)

        'ALMACENA RUTA Y NOMBRE DEL ARCHIVO EN ESTE CASO EL DOCNUM DE LA FACTURA
        _rutaPDF = txtfactura.Text.ToString() + ".pdf"
        '//GENERA PDF EN CARPETA TEMPORAL
        Try
          '//ALMACENA EL PDF EN LA RUTA TEMPORAL
          DocFacturas.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, _rutaPDF)
          'createPDF_OK = True
        Catch ex As Exception
          MessageBox.Show("No se pudo crear el archivo PDF de la Factura: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        '//ADJUNTAR LOS ARCHIVOS PDF'S Y XML'S
        Try
          Dim ArchiveRutaPDF As Attachment = New System.Net.Mail.Attachment(_rutaPDF)
          Msg.Attachments.Add(ArchiveRutaPDF)
        Catch ex As Exception
          MessageBox.Show("Error al adjuntar el archivo PDF de la Factura: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        '//CIERRA EL DOCUEMTNO DE RPT
        DocFacturas.Close()



        '-----

        'COLOCA EL ASUNTO DEL CORREO
        Titulo = "Cancelación de Factura [ " + txtfactura.Text + " ]"

        'ADJUNTA EL TITULO AL CORREO
        Msg.Subject = Titulo

        'ENCRIPTA EL ASUNTO DEL MENSAJE
        Msg.SubjectEncoding = System.Text.Encoding.UTF8

        'COLOCA EL ENCABEZADO Y EL CUERPO DEL MENSAJE A MOSTRAR EN EL CORREO
        Dim vMensaje As String = "<html><head></head><body><h2>Cancelación Factura: " & txtfactura.Text.ToString() & "</h2>"
        vMensaje &= "<br><h3>Nota de Credito: " & NC & "</h3>"
        vMensaje &= "<p>Solicito: " & txtsolicita.Text.ToString()
        vMensaje &= "<br>Requiere Refactura: " & txtrefactura.Text.ToString()
        vMensaje &= "<br>Motivo: " & txtmotivo.Text.ToString()
        vMensaje &= "<br>Comentario: " & txtcomentario.Text.ToString()
        vMensaje &= "<br>Estatus: " & status_fac_env
        'vMensaje &= "<br>Observaciones: " & txtobservaciones.Text.ToString() & "</p><br>"
        vMensaje &= "<p>Estimado usuario:</p><p>Se describe Factura y NC correspondiente a la cancelación solicitada, cualquier duda o aclaración favor de indicarme.</p><p>Saludos cordiales!!!</p>"
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
        envio_adj_ok = True

        '-----
      End If

    Catch exc As Exception
      'INICIALIZA LA VARIABLE EN FALSO SI EL ENVIO NO SE COMPLETO
      envio_adj_ok = False
      'MENSAJE DE ERROR DE ENVIO DE CORREO
      MessageBox.Show("No fue posible enviar email de la confirmación de Cancelación," & Chr(13) & "Favor de avisar la Cancelación de manera Telefonica..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !",
            MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return
    Finally
      System.Windows.Forms.Cursor.Current = Cursors.Default
    End Try
  End Sub


  Sub PruebaProcesoCancelacion(xFactura As String)
    Cursor.Current = Cursors.WaitCursor
    Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
    '//VARIABLE PARA LA EL CORREO
    Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
    Dim emisor As String = "" 'ALMACENA EL NOMBRE DE QUIEN ENVIA
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

    'DECLARACION DE VARIABLE DE REPORTE Y INSTANCIA DEL MISMO
    Dim DocFacturas As ReportDocument
    DocFacturas = New ReportDocument()
    Dim DocKey = String.Empty
    Dim _rutaPDF As String '// ALMACENA LA RUTA DEL PDF
    Dim fecha11082018 As DateTime = Convert.ToDateTime("2018-08-11").Date '//VARIABLES PARA VALIDAR QUE FORMATO CREAR
    Dim fecha01082018 As DateTime = Convert.ToDateTime("2018-08-01").Date
    Dim fechainvoice As DateTime = Convert.ToDateTime("2019-02-11").Date
    Dim fechaMigracionAgo2020 As DateTime = Convert.ToDateTime("2020-08-23").Date

    '= Convert.ToDateTime("2018-10-16").Date '//VARIABLES PARA VALIDAR QUE FORMATO CREAR

    '-----

    ''OBTIENE LOS DATOS DE QUIEN ENVIA EL CORREO
    'Try 'CAPTURA EL ERROR
    '  '****************************************OTRA OPCION SI SE QUIERE QUE MANDE EL CORREO DE CANCELACIONES
    '  ''CONECTA A LA BASE DE DATOS DEL TPD
    '  'conexion_universal.conectar()
    '  'conexion_universal.slq_s = New SqlCommand("SELECT responsable, correo, pass FROM Documents_correos WHERE id_correo = 6 AND fase_envio = 3 AND fase_nombre = 'D'", conexion_universal.conexion_uni)
    '  ''EJECUTA LA CONSULTA
    '  'conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
    '  ''RECORRE LA CONSULTA
    '  'If conexion_universal.rd_s.Read Then
    '  '    de = conexion_universal.rd_s.Item("correo")
    '  '    pass = conexion_universal.rd_s.Item("pass")
    '  '    cc = conexion_universal.rd_s.Item("correo") '+ ";"
    '  '    emisor = conexion_universal.rd_s.Item("responsable")
    '  'End If
    '  ''ALMACENA BANDERA PARA EL ENVIO SI TIENE EMISOR
    '  'envio_ok = True
    '  ''CIERRA LA CONEXION
    '  'conexion_universal.cerrar_conectar()
    '  '****************************************OTRA OPCION SI SE QUIERE QUE MANDE EL CORREO DE CANCELACIONES
    '  '****************************************OTRA OPCION SI NO SE QUIERE QUE MANDE EL CORREO PERSONAL QUE MANDE EL DE CANCELACIONES
    '  conexion_universal.conectar()
    '  conexion_universal.slq_s = New SqlCommand("SELECT Id_Usuario, CorreoE, Pswmail, Nombre FROM Usuarios where Id_Usuario = '" + UsrTPM + "'", conexion_universal.conexion_uni)
    '  'EJECUTA LA CONSULTA
    '  conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
    '  'RECORRE LA CONSULTA
    '  If conexion_universal.rd_s.Read Then
    '    de = conexion_universal.rd_s.Item("CorreoE")
    '    pass = conexion_universal.rd_s.Item("Pswmail")
    '    cc = conexion_universal.rd_s.Item("CorreoE") + ";"
    '    emisor = conexion_universal.rd_s.Item("Nombre")
    '  End If
    '  'ALMACENA BANDERA PARA EL ENVIO SI TIENE EMISOR
    '  envio_ok = True
    '  'CIERRA LA CONEXION
    '  conexion_universal.cerrar_conectar()
    '  '****************************************OTRA OPCION
    'Catch ex As Exception
    '  MsgBox("Error de consulta o conexión TPD para Obtencion de Usuario y correo: " & ex.Message, MsgBoxStyle.Critical, "Error de BD TPD")
    '  conexion_universal.cerrar_conectar()
    '  envio_adj_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
    '  Return
    'End Try 'FIN CAPTURA EL ERROR

    ''------

    ''OBTIENE EL DESTINATARIO DE QUIEN SOLICITO PARA EL ENVIO DE NOTIFICACION DE CANCELACION
    'Try 'CAPTURA EL ERROR
    '  'CONECTA A LA BASE DE DATOS DEL TPD
    '  conexion_universal.conectar()
    '  conexion_universal.slq_s = New SqlCommand("SELECT T0.user1, T1.CorreoE, T1.Pswmail " +
    '                        "FROM Documents_Cancel T0 inner join Usuarios T1 ON T0.user1 = T1.Id_Usuario " +
    '                        "WHERE doc_num = " + txtfactura.Text.ToString() + " ", conexion_universal.conexion_uni)
    '  'EJECUTA LA CONSULTA
    '  conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
    '  'RECORRE LA CONSULTA

    '  If conexion_universal.rd_s.Read Then
    '    para = conexion_universal.rd_s.Item("CorreoE")
    '  End If
    '  'ALMACENA BANDERA PARA EL ENVIO SI TIENE EMISOR
    '  envio_ok = True
    '  'CIERRA LA CONEXION
    '  conexion_universal.cerrar_conectar()
    '  'AGREGA EL CORREO DE QUIEN SOLICITO POR PRIMERA VEZ LA CANCELACION PARA QUE SE LE ENVIE LA CONFIRMACIÓN DE LA CANCELACION
    '  Msg.To.Add(para)
    'Catch ex As Exception
    '  MsgBox("Error de consulta o conexión TPD para Obtencion de Usuario solicitante: " & ex.Message, MsgBoxStyle.Critical, "Error de BD TPD")
    '  conexion_universal.cerrar_conectar()
    '  envio_adj_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
    '  Return
    'End Try 'FIN CAPTURA EL ERROR

    ''------

    'Try 'CAPTURA ERROR DE ENVIO DE CORREO
    '  'VALIDA SI SE PUEDE HACER EL ALMACEN DE ENVIOS
    '  If envio_ok = True Then
    '    'ADJUNTA LOS CORREOS DE ENVIO VALIDADOS QUE TENGAN ENVIO
    '    If de <> "" Then
    '      'OTRA OPCION UNIDA A LA ANTERIOR
    '      'Msg.From = New System.Net.Mail.MailAddress(de, txtsolicita.Text, System.Text.Encoding.UTF8) 'DE QUIEN SE ENVIA
    '      Msg.From = New System.Net.Mail.MailAddress(de, emisor, System.Text.Encoding.UTF8) 'DE QUIEN SE ENVIA
    '      envio_de_ok = True
    '    Else
    '      envio_de_ok = False
    '      envio_adj_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
    '      MsgBox("El correo de aviso de cancelación no se envío, debido a no tener Emisor. Favor de avisar la cancelación de manera Telefónica.", MsgBoxStyle.Exclamation, "Alerta de Emisor")
    '      Return
    '    End If
    '  End If

    '  'VALIDA QUE EFECTIVAMENTE SE HAYA COLOCADO UN ESTATUS
    '  If cmbstatus.SelectedIndex <> -1 And envio_de_ok = True Then
    '    'OBTIENE LOS CORREO DE COPIA PARA ENVIO
    '    Try 'CAPTURA EL ERROR
    '      'CONECTA A LA BASE DE DATOS DEL TPD
    '      conexion_universal.conectar()
    '      'CONSULTA DE OBTENCIÓN DE LOS MOTIVOS
    '      conexion_universal.slq_s = New SqlCommand("SELECT fase_nombre, correo FROM documents_correos where fase_envio = 3", conexion_universal.conexion_uni)
    '      'EJECUTA LA CONSULTA
    '      conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()

    '      'ESTO SE TENDRA QUE ELIMINAR AL IMPLEMENTAR
    '      'para = "asistemas@tractopartesdiamante.com.mx"

    '      'RECORRE LA CONSULTA
    '      While conexion_universal.rd_s.Read

    '        'VALIDA QUE ACCION MANDAR
    '        If conexion_universal.rd_s.Item("fase_nombre") = "P" Then
    '          'ESTA LINEA TENDRA QUE IR AL IMPLEMENTAR Y ELIMINAR LA DE ABAJO
    '          Msg.To.Add(conexion_universal.rd_s.Item("correo"))
    '          'Msg.To.Add(para)
    '        ElseIf conexion_universal.rd_s.Item("fase_nombre") = "C" Then
    '          Msg.CC.Add(conexion_universal.rd_s.Item("correo"))
    '        ElseIf conexion_universal.rd_s.Item("fase_nombre") = "CO" Then
    '          Msg.Bcc.Add(conexion_universal.rd_s.Item("correo"))
    '        End If
    '      End While
    '      conexion_universal.rd_s.Close()
    '      'CIERRA LA CONEXION
    '      conexion_universal.cerrar_conectar()
    '    Catch ex As Exception
    '      MsgBox("Error de consulta o conexión TPD para Obtencion de los correos: " & ex.Message, MsgBoxStyle.Critical, "Error de BD")
    '      conexion_universal.cerrar_conectar()
    '      envio_adj_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
    '      Return
    '    End Try 'FIN CAPTURA EL ERROR

    '    '------

    'OBTIENE EL DOCENTRY DE LA FACTURA PARA PODER USARLA EN LA CREACION DEL PDF
    Try 'CAPTURA EL ERROR
      'CONECTA A LA BASE DE DATOS DEL SAP
      conexion_universal.conectar_sap()
      conexion_universal.sql_tem = New SqlCommand("SELECT DocEntry, DocNum " +
                            "FROM OINV WHERE DocNum = " & xFactura & " ", conexion_universal.conexion_uni_sap)
      'EJECUTA LA CONSULTA
      conexion_universal.rd_tem = conexion_universal.sql_tem.ExecuteReader()
      'RECORRE LA CONSULTA
      If conexion_universal.rd_tem.Read Then
        DocEntry_Fat = conexion_universal.rd_tem.Item("DocEntry")
      End If
      'ALMACENA BANDERA PARA EL ENVIO SI TIENE EMISOR
      envio_ok = True
      'CIERRA LA CONEXION
      conexion_universal.cerrar_conectar_sap()
    Catch ex As Exception
      MsgBox("Posiblemente no se ejecute la creación del PDF: " & ex.Message, MsgBoxStyle.Critical, "Error de BD SAP")
      conexion_universal.cerrar_conectar_sap()
    End Try 'FIN CAPTURA EL ERROR

    '-----

    'ADJUNTA LA FACTURA Y LA CANCELACIÓN

    'MANDA A LLAMAR AL METODO DE OBTENER    ----------CREO QUE NO SE OCUPA

    'ALAMCENA LA FECHA Y LA CONVIERTE EN VALIDA PARA COMPRAR
    DocDate = Convert.ToDateTime(Now()).Date
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
    '  DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\NC 3_3 19ABR2018.rpt") ' //RUTA DEL ARCHIVO .rpt
    'Else
    '  DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\NC 3.3_93_05.rpt") ' //ruta del archivo .rpt
    'End If
    '//PARAMETROS DE CONEXION PARA EL RPT
    Dim tInfo As TableLogOnInfo = New TableLogOnInfo()
    Dim ConnectionInfo As ConnectionInfo = tInfo.ConnectionInfo

    ConnectionInfo.Password = conexion_universal.cPassword
    ConnectionInfo.UserID = conexion_universal.cUserID
    ConnectionInfo.ServerName = conexion_universal.cServerName ' // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS
    ConnectionInfo.DatabaseName = conexion_universal.cDatabaseNameSAP

    '//PASA EL PARAMETRO AL ARCHIVO RPT (DocEntry)
    DocFacturas.SetParameterValue("DocKey@", "00000")

    'ESTABLE LA CONEXION CON EL REPORTE
    SetTableLocation(DocFacturas, ConnectionInfo)


    '************************************************************************************
    'ALMACENA RUTA Y NOMBRE DEL ARCHIVO EN ESTE CASO EL DOCNUM DE LA NC
    '************************************************************************************
    _rutaPDF = "\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\DoctosPruebaCancelacion\NotaCreditoPrueba.pdf"
    '//GENERA PDF EN CARPETA TEMPORAL
    Try
      '//ALMACENA EL PDF EN LA RUTA TEMPORAL
      DocFacturas.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, _rutaPDF)
      'createPDF_OK = True
    Catch ex As Exception
      MessageBox.Show("No se pudo crear el archivo PDF de la NC: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
    '//ADJUNTAR LOS ARCHIVOS PDF'S Y XML'S
    Try
      Dim ArchiveRutaPDF As Attachment = New System.Net.Mail.Attachment(_rutaPDF)
      Msg.Attachments.Add(ArchiveRutaPDF)
    Catch ex As Exception
      MessageBox.Show("Error al adjuntar el archivo PDF de la NC: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
    '//CIERRA EL DOCUEMTNO DE RPT
    DocFacturas.Close()
    'INICIALIZA EN NADA LA RUTA
    _rutaPDF = ""

    'ADJUNTA Y CREA EL PDF DE FACTURA





    '****************************************************************************************************************************************
    '****************************************************************************************************************************************
    '**  FACTURA ****************************************************************************************************************************
    '****************************************************************************************************************************************
    '****************************************************************************************************************************************

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

    '//PASA EL PARAMETRO AL ARCHIVO RPT (DocEntry)
    DocFacturas.SetParameterValue("DocKey@", DocEntry_Fat)

    'ESTABLE LA CONEXION CON EL REPORTE
    SetTableLocation(DocFacturas, ConnectionInfo)

    'ALMACENA RUTA Y NOMBRE DEL ARCHIVO EN ESTE CASO EL DOCNUM DE LA FACTURA
    _rutaPDF = "\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\DoctosPruebaCancelacion\" + xFactura + ".pdf"
    '//GENERA PDF EN CARPETA TEMPORAL
    Try
      '//ALMACENA EL PDF EN LA RUTA TEMPORAL
      DocFacturas.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, _rutaPDF)
      'createPDF_OK = True
    Catch ex As Exception
      MessageBox.Show("No se pudo crear el archivo PDF de la Factura: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
    '//CIERRA EL DOCUEMTNO DE RPT
    DocFacturas.Close()



    '-----

    ''COLOCA EL ASUNTO DEL CORREO
    'Titulo = "Cancelación de Factura [ " + txtfactura.Text + " ]"

    ''ADJUNTA EL TITULO AL CORREO
    'Msg.Subject = Titulo

    ''ENCRIPTA EL ASUNTO DEL MENSAJE
    'Msg.SubjectEncoding = System.Text.Encoding.UTF8

    ''COLOCA EL ENCABEZADO Y EL CUERPO DEL MENSAJE A MOSTRAR EN EL CORREO
    'Dim vMensaje As String = "<html><head></head><body><h2>Cancelación Factura: " & txtfactura.Text.ToString() & "</h2>"
    'vMensaje &= "<br><h3>Nota de Credito: " & NC & "</h3>"
    'vMensaje &= "<p>Solicito: " & txtsolicita.Text.ToString()
    'vMensaje &= "<br>Requiere Refactura: " & txtrefactura.Text.ToString()
    'vMensaje &= "<br>Motivo: " & txtmotivo.Text.ToString()
    'vMensaje &= "<br>Comentario: " & txtcomentario.Text.ToString()
    'vMensaje &= "<br>Estatus: " & status_fac_env
    ''vMensaje &= "<br>Observaciones: " & txtobservaciones.Text.ToString() & "</p><br>"
    'vMensaje &= "<p>Estimado usuario:</p><p>Se describe Factura y NC correspondiente a la cancelación solicitada, cualquier duda o aclaración favor de indicarme.</p><p>Saludos cordiales!!!</p>"
    'vMensaje &= "</body></html>"

    ''ADJUNTA EL MENSAJE AL CUERPO DEL CORREO
    'Msg.Body = vMensaje
    'Msg.IsBodyHtml = True 'EL CUERPO DEL MENSAJE NO ES HTML


    ''------

    'SMTP.UseDefaultCredentials = False ' SI REQUIERE CREDENCIALES POR DEFECTO
    'SMTP.Credentials = New System.Net.NetworkCredential(de, pass) ' 'CREDENCIALES PARA PODER MANDAR EL CORREO
    'SMTP.Port = 2525 ' EL PUERTO QUE UTILIZA PARA PODER MANDAR EL ENVIO DEL CORREO
    'SMTP.Host = "mail.tractopartesdiamante.com.mx" ' SERVIDOR DEL ENVIO DE MENSAJES
    'SMTP.EnableSsl = False ' ESTO ES PARA QUE VAYA ATRAVEEZ DE SSL(USO DEL SERTIFICADO DIGITAL) POR SI USAMOS GMAIL POR EJEMPLO
    'SMTP.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network 'ENVIAMOS ATRAVEZ DE LA RED
    ''mail.fng-puebla.com.mx 192.168.1.7
    'SMTP.Send(Msg)
    ''INICIALIZA LA VARIABLE EN VERDADERO SI SE ENVIO CORRECTAMENTE EL CORREO
    'envio_adj_ok = True

    '-----

    System.Windows.Forms.Cursor.Current = Cursors.Default
  End Sub


  'METODO PARA LA TABLA DE INFORMACIÓN DEL CRYSTAL REPORTS

  Sub SetTableLocation(report As ReportDocument, connectionInfo As ConnectionInfo)
    For Each table As Table In report.Database.Tables
      Dim tableLogOnInfo As TableLogOnInfo = table.LogOnInfo
      tableLogOnInfo.ConnectionInfo = connectionInfo
      table.ApplyLogOnInfo(tableLogOnInfo)
    Next

  End Sub
End Class