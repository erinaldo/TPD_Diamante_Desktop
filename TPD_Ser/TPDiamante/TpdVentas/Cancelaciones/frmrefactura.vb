Imports System.Data.SqlClient
Imports System.Net.Mail
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Public Class frmrefactura

  'VARIABLE GLOBAL DEL FORMULRIO
  Dim envio_ref_ok As Boolean = False
  Dim actualiza_ref_ok As Boolean = False
  Dim ref As Integer 'ALMACENA EL NUMERO DE NC GENERADO
  Dim status_ref_env As String 'ALMACENA EN QUE ESTATUS SE QUEDA LA FACTURA
  Dim FacturaNombre As String  'ALMACENA LA FACTURA Y RUTA PARA LA EXPORTACION DE LA MISMA
  Dim DocEntry_Fat As String 'ALMACENA EL DOCENTRY DE LA REFACTURA



  Dim DocDate As DateTime
  'DECLARACION DE VARIABLE DE REPORTE Y INSTANCIA DEL MISMO
  Dim DocFacturas As ReportDocument = New ReportDocument()
  'DocFacturas = New ReportDocument()
  Dim DocKey = String.Empty
  Dim _rutaPDF As String '// ALMACENA LA RUTA DEL PDF
  Dim fecha11082018 As DateTime = Convert.ToDateTime("2018-08-11").Date '//VARIABLES PARA VALIDAR QUE FORMATO CREAR

  Private Sub frmrefactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    'MANDA A LLAMAR AL DISEÑO DEL GRID
    estilo_grid_can()
    'MANDA A LLAMAR AL RELLENO DEL GRID CON LAS FACTURAS PENDIENTES POR AUTORIZAR
    llena_grid_autoriza()
  End Sub
  Sub estilo_grid_can() 'ESTILO DEL GRID
    With Me.dgvrefactura
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
    If dgvrefactura.RowCount > 0 Then
      dgvrefactura.Rows.Clear()
    End If
    Try
      'CONECTA A LA BASE DE DATOS
      conexion_universal.conectar()
      'VALIDA SI EL USUARIO ES NORMAL O EL MANAGER
      If UsrTPM = "MANAGER" Then
        'ALAMACENA LA CONSULTA
        SQLautoriza = "SELECT user1, name_user, doc_num, FORMAT(doc_date, 'yyyy-MM-dd') as doc_date , FORMAT(cancel_date_hour, 'yyyy-MM-dd hh\:mm') as cancel_date_hour, motivo, comments, "
        SQLautoriza &= "refactura, sustituye, id_warehouse, warehouse, ISNULL(doc_num_nc,'') AS Num_NC, ISNULL(CONVERT(varchar(35), doc_date_nc, 126),'') AS Date_NC, status "
        SQLautoriza &= "FROM Documents_Cancel WHERE status = 'PENDIENTE REFACTURACION' "
        SQLautoriza &= "order by cancel_date_hour ASC"
      Else
        'ALAMACENA LA CONSULTA
        SQLautoriza = "SELECT user1, name_user, doc_num, FORMAT(doc_date, 'yyyy-MM-dd') as doc_date , FORMAT(cancel_date_hour, 'yyyy-MM-dd hh\:mm') as cancel_date_hour, motivo, comments, "
        SQLautoriza &= "refactura, sustituye, id_warehouse, warehouse, ISNULL(doc_num_nc,'') AS Num_NC, ISNULL(CONVERT(varchar(35), doc_date_nc, 126),'') AS Date_NC, status "
        SQLautoriza &= "FROM Documents_Cancel WHERE status = 'PENDIENTE REFACTURACION' AND user1 = '" + UsrTPM + "' "
        SQLautoriza &= "order by cancel_date_hour ASC"
      End If
      'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
      conexion_universal.slq_s = New SqlCommand(SQLautoriza, conexion_universal.conexion_uni)
      'EJECUTA LA CONSULTA
      conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
      'RECORRE LA CONSULTA
      While conexion_universal.rd_s.Read
        If dgvrefactura.RowCount > 0 Then
          'MANDA LOS RESULTADOS
          Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            Me.dgvrefactura.Rows.Add(rd_s.Item("name_user"), rd_s.Item("doc_num").ToString, rd_s.Item("doc_date"), rd_s.Item("cancel_date_hour").ToString,
                        rd_s.Item("motivo").ToString, rd_s.Item("comments").ToString, rd_s.Item("refactura").ToString, rd_s.Item("sustituye").ToString,
                        rd_s.Item("warehouse").ToString, rd_s.Item("Num_NC").ToString, rd_s.Item("Date_NC").ToString, rd_s.Item("status").ToString)
            'RECORRE EL DATA GRID VIEW
            With dgvrefactura
              'ESTABLECE LA CELDA ACTUAL
              .CurrentCell = .Rows(Me.dgvrefactura.Rows.Count - 1).Cells(0)
            End With
          Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            'MANDA EL MENSAJE DE ERROR
            MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
            Return
          End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
        Else
          'MANDA LOS RESULTADOS
          Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            Me.dgvrefactura.Rows.Add(rd_s.Item("name_user"), rd_s.Item("doc_num").ToString, rd_s.Item("doc_date"), rd_s.Item("cancel_date_hour").ToString,
                        rd_s.Item("motivo").ToString, rd_s.Item("comments").ToString, rd_s.Item("refactura").ToString, rd_s.Item("sustituye").ToString,
                        rd_s.Item("warehouse").ToString, rd_s.Item("Num_NC").ToString, rd_s.Item("Date_NC").ToString, rd_s.Item("status").ToString)
            'RECORRE EL DATA GRID VIEW
            With dgvrefactura
              'ESTABLECE LA CELDA ACTUAL
              .CurrentCell = .Rows(Me.dgvrefactura.Rows.Count - 1).Cells(0)
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
    txtstatus.Text = ""
    txtref.Text = ""
    txtnc.Text = ""
    txtcom_refactura.Text = ""
    brnguardar.Enabled = False
    'INICIALIZA LA NC EN NADA
    ref = 0
    '-----
  End Sub

  Private Sub dgvautoriza_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvrefactura.CellContentClick
    'MANDA A LLAMAR AL METODO LIMPIAR
    limpiar()
    'PROPIEDADES RTEQUERIDAS PARA ESTA FUNCION
    'MULTISELECT = TRUE
    'SELECTMODE = FullRowSelect
    txtfactura.Text = dgvrefactura.Item("Factura", dgvrefactura.CurrentRow.Index).Value.ToString
    txtsolicita.Text = dgvrefactura.Item("Usuario", dgvrefactura.CurrentRow.Index).Value.ToString
    txtfecha_factura.Text = dgvrefactura.Item("FechaFactura", dgvrefactura.CurrentRow.Index).Value.ToString
    txtfecha_solicitud.Text = dgvrefactura.Item("FechaCancela", dgvrefactura.CurrentRow.Index).Value.ToString
    txtmotivo.Text = dgvrefactura.Item("Motivo", dgvrefactura.CurrentRow.Index).Value.ToString
    txtcomentario.Text = dgvrefactura.Item("Comentarios", dgvrefactura.CurrentRow.Index).Value.ToString
    txtnc.Text = dgvrefactura.Item("NotaCredito", dgvrefactura.CurrentRow.Index).Value.ToString
    txtrefactura.Text = dgvrefactura.Item("Refactura", dgvrefactura.CurrentRow.Index).Value.ToString
    txtalmacen.Text = dgvrefactura.Item("Almacen", dgvrefactura.CurrentRow.Index).Value.ToString
    txtstatus.Text = dgvrefactura.Item("Status", dgvrefactura.CurrentRow.Index).Value.ToString
  End Sub
  Private Sub dgvautoriza_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvrefactura.CellMouseClick
    'MANDA A LLAMAR AL METODO LIMPIAR
    limpiar()
    txtfactura.Text = dgvrefactura.Item("Factura", dgvrefactura.CurrentRow.Index).Value.ToString
    txtsolicita.Text = dgvrefactura.Item("Usuario", dgvrefactura.CurrentRow.Index).Value.ToString
    txtfecha_factura.Text = dgvrefactura.Item("FechaFactura", dgvrefactura.CurrentRow.Index).Value.ToString
    txtfecha_solicitud.Text = dgvrefactura.Item("FechaCancela", dgvrefactura.CurrentRow.Index).Value.ToString
    txtmotivo.Text = dgvrefactura.Item("Motivo", dgvrefactura.CurrentRow.Index).Value.ToString
    txtcomentario.Text = dgvrefactura.Item("Comentarios", dgvrefactura.CurrentRow.Index).Value.ToString
    txtnc.Text = dgvrefactura.Item("NotaCredito", dgvrefactura.CurrentRow.Index).Value.ToString
    txtrefactura.Text = dgvrefactura.Item("Refactura", dgvrefactura.CurrentRow.Index).Value.ToString
    txtalmacen.Text = dgvrefactura.Item("Almacen", dgvrefactura.CurrentRow.Index).Value.ToString
    txtstatus.Text = dgvrefactura.Item("Status", dgvrefactura.CurrentRow.Index).Value.ToString
  End Sub
  Private Sub dgvautoriza_KeyUp(sender As Object, e As KeyEventArgs) Handles dgvrefactura.KeyUp
    'MANDA A LLAMAR AL METODO LIMPIAR
    limpiar()
    'MUESTRA EL RESULTADO EN EL TEXTBOX
    txtfactura.Text = dgvrefactura.Item("Factura", dgvrefactura.CurrentRow.Index).Value.ToString
    txtsolicita.Text = dgvrefactura.Item("Usuario", dgvrefactura.CurrentRow.Index).Value.ToString
    txtfecha_factura.Text = dgvrefactura.Item("FechaFactura", dgvrefactura.CurrentRow.Index).Value.ToString
    txtfecha_solicitud.Text = dgvrefactura.Item("FechaCancela", dgvrefactura.CurrentRow.Index).Value.ToString
    txtmotivo.Text = dgvrefactura.Item("Motivo", dgvrefactura.CurrentRow.Index).Value.ToString
    txtcomentario.Text = dgvrefactura.Item("Comentarios", dgvrefactura.CurrentRow.Index).Value.ToString
    txtnc.Text = dgvrefactura.Item("NotaCredito", dgvrefactura.CurrentRow.Index).Value.ToString
    txtrefactura.Text = dgvrefactura.Item("Refactura", dgvrefactura.CurrentRow.Index).Value.ToString
    txtalmacen.Text = dgvrefactura.Item("Almacen", dgvrefactura.CurrentRow.Index).Value.ToString
    txtstatus.Text = dgvrefactura.Item("Status", dgvrefactura.CurrentRow.Index).Value.ToString
  End Sub

  Private Sub txtref_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtref.KeyPress
    'VALIDA QUE SOLO PERMITA NUMEROS Y NO LETRAS NI DIGITOS
    If Char.IsDigit(e.KeyChar) Then
      e.Handled = False
    ElseIf Char.IsControl(e.KeyChar) Then
      e.Handled = False
    Else
      e.Handled = True
      MsgBox("Solo se permiten digitos numericos", MsgBoxStyle.Exclamation, "Alerta de caracter")
      txtref.Focus()
    End If
    If Asc(e.KeyChar) = 8 Then
      txtcom_refactura.Text = ""
      brnguardar.Enabled = False
    End If
  End Sub

  Private Sub btnrefrescar_Click(sender As Object, e As EventArgs) Handles btnrefrescar.Click
    'BORRA LOS DATOS DEL GRID
    If dgvrefactura.RowCount > 0 Then
      dgvrefactura.Rows.Clear()
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
    For i As Integer = 0 To dgvrefactura.RowCount - 1
      'VALIDA SI ROMPE EL CICLO O NO
      If factura_ok = True Then
        Exit For 'ROMPE EL CICLO FOR
      End If
      For j As Integer = 0 To dgvrefactura.ColumnCount - 1
        If dgvrefactura.Item(j, i).Value.ToString = txtfactura.Text Then
          'MUESTRA LOS DATOS SI LA FACTURA EXISTE EN EL GRID
          txtsolicita.Text = dgvrefactura.Item("Usuario", i).Value.ToString
          txtfecha_factura.Text = dgvrefactura.Item("FechaFactura", i).Value.ToString
          txtfecha_solicitud.Text = dgvrefactura.Item("FechaCancela", i).Value.ToString
          txtmotivo.Text = dgvrefactura.Item("Motivo", i).Value.ToString
          txtcomentario.Text = dgvrefactura.Item("Comentarios", i).Value.ToString
          txtnc.Text = dgvrefactura.Item("NotaCredito", i).Value.ToString
          txtrefactura.Text = dgvrefactura.Item("Refactura", i).Value.ToString
          txtalmacen.Text = dgvrefactura.Item("Almacen", i).Value.ToString
          txtstatus.Text = dgvrefactura.Item("Status", i).Value.ToString
          factura_ok = True
          Exit For
        End If
      Next
    Next 'FIN RECORRE TODO EL DATA GRID VIEW
  End Sub

  Private Sub brnguardar_Click(sender As Object, e As EventArgs) Handles brnguardar.Click
    Dim confirma As Integer     'VARIABLE PARA VALIDAR LA CONFIRMACIÓN

    'VALIDA QUE NO VAYA VACIO EL CAMPO DE LA REFACTURA Y DE LA FACTURA
    If txtref.Text = "" Or txtref.Text = " " Or txtfactura.Text = "" Then
      MsgBox("Favor de colocar la refactura Valida.", MsgBoxStyle.Exclamation, "Alerta de dato")
      txtref.Focus()
      Return
    End If

    '-----

    'VALIDA QUE ACCION REALIZARA PARA MOSTRAR EL MENSAJE QUE REALMENTE SE REQUIERE
    If txtref.Text <> "" And txtref.Text <> " " And txtfactura.Text <> "" Then
      'PREGUNTA PRIMERO AL USUARIO SI REALMENTE DESEA AUTORIZAR O NO LA FACTURA
      'POSTERIOR A ELLO SE REALIZ LA ACCION
      confirma = MessageBox.Show("Se sustituirá la factura [ " + txtfactura.Text + " ] por la Refactura [ " + txtref.Text + " ] Realmente desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
      If confirma = 6 Then
        'MANDA A LLAMAR EL METODO DE ACTUALIZAR LA FACTURA
        actualiza_factura()

        '----- VALIDA QUE MENSAJE ENVIAR
        If envio_ref_ok = True And actualiza_ref_ok = True Then
          MsgBox("Se sustituyo la factura [ " + txtfactura.Text.ToString + " ] correctamente.", MsgBoxStyle.Information, "Datos actualizados y Enviados")
          envio_ref_ok = False
          actualiza_ref_ok = False
          status_ref_env = ""
          'INICIALIZA LA NC EN NADA
          ref = 0
          'MANDA A LLAMAR A LOS METODOS DE LLENADO DE GRID Y LIMPIADO DE DATOS
          limpiar()
          'llena_grid_autoriza()
          txtfactura.Text = ""
        ElseIf envio_ref_ok = False And actualiza_ref_ok = True Then
          MsgBox("Se sustituyo la factura [ " + txtfactura.Text.ToString + " ]. Envio de Sustitución fallido. Favor de notificar por otro medio la Sustitución.", MsgBoxStyle.Information, "Datos actualizados y Envio Incorrecto")
          envio_ref_ok = False
          actualiza_ref_ok = False
          status_ref_env = ""
          'INICIALIZA LA NC EN NADA
          ref = 0
          'MANDA A LLAMAR A LOS METODOS DE LLENADO DE GRID Y LIMPIADO DE DATOS
          limpiar()
          'llena_grid_autoriza()
          txtfactura.Text = ""
        Else
          MsgBox("Ninguna accion se realizo, favor de contactar a Sistemas.", MsgBoxStyle.Critical, "Acciones fallidas")
          envio_ref_ok = False
          actualiza_ref_ok = False
          status_ref_env = ""
          'INICIALIZA LA NC EN NADA
          ref = 0
        End If

        '-----

        'MANDA A LLENAR EL GRID CON LA ACTUALIZACIONES REALIZADAS
        llena_grid_autoriza()
      Else
        Return 'SI SE SELECCIONA QUE NO, NO HARA NADA
      End If
    Else
      MsgBox("Datos de busqueda no encontrados, favor de validar los datos introducidos.", MsgBoxStyle.Exclamation, "Alerta de datos")
      Return
    End If
  End Sub
  Sub actualiza_factura()
    Dim SQLUpdateRef As String    'VARIBALE CADENA QUE ALMACENA LA CONSULTA
    Dim SQLEstatus As String = ""  'VARIABLE CADENA PARA LA CONSULTA DE OBTENCION DEL ESTATUS
    Dim SQLNota As String = ""   'VARIABLE CADENA PARA LA CONSULTA DE OBTENCION DEL ESTATUS
    Dim SQLRef As String = ""    'VARIABLE CADENA PARA LA CONSULTA DE OBTENCION DE LA REFACTURA
    Dim SQLValidaRef As String = ""  'VARIABLE CADENA PARA LA CONSULTA DE VALIDAR MONTO Y ARTICULOS DE LA FACTURA CONTRA LA REFACTURA
    Dim estado As String = ""        'VARIABLE QUE ALMACENARÁ EL ESTADO EN EL CUAL VAYA ESTAR EL PROCESO
    Dim fecha_val As String = ""
    Try 'CAPTURA EL ERROR QUE SE OBTENGA
      'VALIDA QUE ACTUALIZACIÓN SE REALIZARÁ, SI ES NO PROCEDE O EN PROCESO DE CANCELACIÓN
      If txtref.Text <> "" And txtref.Text <> " " Then
        '-----
        Try 'INICIO TRY VALIDACIONES
          Try 'INICIO TRY CONEXION SAP
            'ABRE LA CONEXION AL TPD
            conexion_universal.conectar_sap()
            'ALMACENA LA FACTURA CANCELADA PARA SU VALIDACION
            'CONSULTA PARA SABER SI YA TIENE CREADA LA NOTA DE CREDITO
            SQLNota = "SELECT T0.DocEntry as IdentificadorFact, T0.DocNum as Factura, FORMAT(T0.DocDate, 'yyyy-MM-dd') as FechaFactura, ISNULL(T2.DocNum, 0) as NC, ISNULL(CONVERT(varchar(35), FORMAT(T2.DocDate, 'yyyy-MM-dd'), 126),'') as FechaNC "
            SQLNota &= "FROM OINV T0 INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry "
            SQLNota &= "left join ORIN T2 ON T1.TrgetEntry = T2.DocEntry "
            SQLNota &= "WHERE T0.DocNum = '" + txtfactura.Text + "' "
            'ALMACENA LA CONSULTA EN UN COMMAND PARA PODER HACER LA EJECUCION DE LA MISMA
            conexion_universal.slq_s = New SqlCommand(SQLNota, conexion_universal.conexion_uni_sap)
            'EJECUTA LA CONSULTA 
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'EXECUTA EL READER
            conexion_universal.rd_s.Read()
            'VALIDA QUE SI TENGA UNA NOTA DE CREDITO LA FACTURA
            If conexion_universal.rd_s.Item("NC") <> 0 Then
              '-----
              'ALMACENA LA FECHA DE LA FACTURA
              fecha_val = conexion_universal.rd_s.Item("FechaFactura")
              '-----
              Try
                'ABRE LA CONEXION AL TPD
                conexion_universal.conectar_sap()
                'CONSULTA PARA VALIDAR QUE LA REFACTURA SEA VALIDA
                'FORMAT(T0.DocDate, 'yyyy-MM-dd')
                SQLRef = "SELECT T0.DocEntry AS DocEntry, T0.DocNum AS Refactura, FORMAT(T0.DocDate, 'yyyy-MM-dd') AS FechaRefactura "
                SQLRef &= "FROM OINV T0 "
                SQLRef &= "WHERE T0.DocNum =  '" + txtref.Text + "' and T0.DocDate >= '" + fecha_val + "' "
                SQLRef &= "and T0.DocNum <> '" + txtfactura.Text + "' "
                'ALMACENA LA CONSULTA EN UN COMANDO PARA PODER HACER LA EJECUCION
                conexion_universal.slq_con = New SqlCommand(SQLRef, conexion_universal.conexion_uni_sap)
                'EJECUTA LA CONSULTA
                conexion_universal.rd_con = conexion_universal.slq_con.ExecuteReader()
                'VALIDA QUE EXISTA LA FACTURA DE REFACTURACION
                If conexion_universal.rd_con.Read() Then

                  '-----

                  Try  'INICIO TRY TPD
                    'CONECTA A LA BD DE TPD
                    conexion_universal.conectar()
                    'CONSULTA PARA ACTUALIZAR LA REFACTURA DE LA FACTURA CORRESPONDIENTE
                    SQLUpdateRef = "UPDATE Documents_Cancel SET sustituye = '" + conexion_universal.rd_con.Item("Refactura").ToString + "', "
                    SQLUpdateRef &= "refactura_date = '" + conexion_universal.rd_con.Item("FechaRefactura") + "', status = 'FINALIZADO' "
                    SQLUpdateRef &= "WHERE doc_num = '" + txtfactura.Text + "' "
                    'ALMACENA EN UN COMMAND LA CONSULTA
                    conexion_universal.sql_up = New SqlCommand(SQLUpdateRef, conexion_universal.conexion_uni)
                    'EJECUTA LA CONSULTA
                    conexion_universal.rd_up = conexion_universal.sql_up.ExecuteScalar
                    '------
                    actualiza_ref_ok = True 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA
                    'ALMACENA LOS VALORES DE LA REFACTURA Y DEL ESTATUS DEL PROCESO
                    ref = txtref.Text
                    status_ref_env = "FINALIZADO"
                    'CIERRA LA CONEXION
                    conexion_universal.cerrar_conectar()
                  Catch ex As Exception
                    MsgBox("Error en consulta o conexión al Actualizar la Refactura: " + ex.ToString, MsgBoxStyle.Critical, "Error de BD TPD")
                    actualiza_ref_ok = False 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA
                    'CIERRA EL READER
                    conexion_universal.rd_con.Close()
                    conexion_universal.cerrar_conectar()
                    Return
                  End Try  'FIN TRY TPD
                  'CIERRA EL READER
                  conexion_universal.rd_con.Close()

                  '-----

                Else
                  MsgBox("No es posible registrar la Refactura, debido a los posibles motivos siguientes: " & Chr(13) & Chr(13) &
                                          " * No existe la refactura en la BD. " & Chr(13) &
                                          " * No corresponde al periodo o fecha de factura. " & Chr(13) &
                                          " * La refactura es el mismo número de factura. ", MsgBoxStyle.Exclamation, "Alerta de Refactura")
                  actualiza_ref_ok = False 'INICIALIZA EN FALSO LA ACTUALIZACION DE LA FACTURA
                  conexion_universal.rd_con.Close()
                  conexion_universal.cerrar_conectar_sap()
                  Return
                End If

                '-----

              Catch ex As Exception
                MsgBox("Error en consulta o conexión al Buscar la Refactura: " + ex.ToString, MsgBoxStyle.Critical, "Error de BD SAP")
                actualiza_ref_ok = False 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA
                conexion_universal.rd_con.Close()
                conexion_universal.cerrar_conectar_sap()
                Return
              End Try

              '-----
            Else
              '-----
              MsgBox("No es posible agregar la Refactura, debido a que no se ha realizado la Cancelación [NOTA DE CREDITO] correspondiente.", MsgBoxStyle.Exclamation, "Alerta de Cancelación")
              actualiza_ref_ok = False 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA
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
            actualiza_ref_ok = False 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            conexion_universal.cerrar_conectar_sap()
            Return
          End Try 'FIN TRY DE CONEXION SAP

          '-----

        Catch ex As Exception
          MsgBox("Error de consulta o conexión, Favor de intentar más tarde.", MsgBoxStyle.Critical, "Conexión fallida")
          actualiza_ref_ok = True 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA
          conexion_universal.cerrar_conectar()
          Return
        End Try 'FIN TRY VALIDACIONES

        '-----

        'MANDA A LLAMAR EL METODO DE ENVIO DE CORREO FASE 2
        EnviaProcesoCancelacion()

      End If
    Catch ex As Exception
      actualiza_ref_ok = False 'INICIALIZA EN FALSO LA VARIABLE DE QUE NO SE ACTUALIZO LA FACTURA
      MsgBox("Error en Actualizar la factura: " + ex.ToString, MsgBoxStyle.Critical, "Error en BD TPD ")
      conexion_universal.cerrar_conectar()
      Return
    End Try
  End Sub

  Sub EnviaProcesoCancelacion()
    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
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
    Dim refactura As String = txtref.Text 'ALMACENA SI SE REFACTURA LA FACTURA O NO
    'VARIABLE DE CADENA PARA CONSULTA
    'Dim SQLUsuario As String

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
        cc = conexion_universal.rd_s.Item("CorreoE") '+ ";"
        emisor = conexion_universal.rd_s.Item("Nombre")
      End If
      'ALMACENA BANDERA PARA EL ENVIO SI TIENE EMISOR
      envio_ok = True
      'CIERRA LA CONEXION
      conexion_universal.cerrar_conectar()
    Catch ex As Exception
      MsgBox("Error de consulta o conexión TPD para Obtencion de Usuario y correo: " & ex.Message, MsgBoxStyle.Critical, "Error de BD TPD")
      conexion_universal.cerrar_conectar()
      envio_ref_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
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
          envio_ref_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
          MsgBox("El correo de aviso de Refacturación no se envío, debido a no tener Emisor. Favor de avisar la Refacturación de manera Telefónica.", MsgBoxStyle.Exclamation, "Alerta de Emisor")
          Return
        End If
      End If

      '------

      'VALIDA QUE EFECTIVAMENTE SE HAYA COLOCADO UN ESTATUS
      If txtref.Text <> "" And txtref.Text <> " " And envio_de_ok = True Then
        'OBTIENE LOS CORREO DE COPIA PARA ENVIO
        Try 'CAPTURA EL ERROR
          'CONECTA A LA BASE DE DATOS DEL TPD
          conexion_universal.conectar()
                    'CONSULTA DE OBTENCIÓN DE LOS MOTIVOS
                    conexion_universal.slq_s = New SqlCommand("SELECT fase_nombre, correo FROM documents_correos LEFT JOIN Usuarios ON Usuarios.Almacen = Documents_correos.WHSCODE2  where fase_envio = 5 AND Id_Usuario ='" + UsrTPM + "'", conexion_universal.conexion_uni)
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
          envio_ref_ok = False 'COLOCA FALSO SI NO SE VA ENVIAR EL CORREO
          Return
        End Try 'FIN CAPTURA EL ERROR

        '-----

        'OBTIENE EL DOCENTRY DE LA FACTURA PARA PODER USARLA EN LA CREACION DEL PDF
        Try 'CAPTURA EL ERROR
          'CONECTA A LA BASE DE DATOS DEL SAP
          conexion_universal.conectar_sap()
          conexion_universal.sql_tem = New SqlCommand("SELECT DocEntry, DocNum, DocDate " +
                            "FROM OINV WHERE DocNum = " + txtref.Text.ToString() + " ", conexion_universal.conexion_uni_sap)
          'EJECUTA LA CONSULTA
          conexion_universal.rd_tem = conexion_universal.sql_tem.ExecuteReader()
          'RECORRE LA CONSULTA
          If conexion_universal.rd_tem.Read Then
            DocEntry_Fat = conexion_universal.rd_tem.Item("DocEntry")
            DocDate = Convert.ToDateTime(conexion_universal.rd_tem.Item("DocDate").ToString).Date
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

        'ADJUNTA Y CREA EL PDF DE LA REFACTURA FACTURA

        '//PARAMETROS DE CONEXION PARA EL RPT
        Dim tInfo As TableLogOnInfo = New TableLogOnInfo()
        Dim ConnectionInfo As ConnectionInfo = tInfo.ConnectionInfo
        ConnectionInfo.Password = conexion_universal.cPassword
        ConnectionInfo.UserID = conexion_universal.cUserID
        ConnectionInfo.ServerName = conexion_universal.cServerName ' // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS
        ConnectionInfo.DatabaseName = conexion_universal.cDatabaseNameSAP

        '//VALIDA LA FECHA PARA SABER QUE FORMATO EJECUTAR
        If (DocDate <= fecha11082018) Then
          DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\Factura 3_3 19ABR2018.rpt") ' //RUTA DEL ARCHIVO .rpt
        Else
          DocFacturas.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\Factura 3.3-93_5.rpt") ' //ruta del archivo .rpt
        End If

        '//PASA EL PARAMETRO AL ARCHIVO RPT (DocEntry)
        DocFacturas.SetParameterValue("DocKey@", DocEntry_Fat)

        'ESTABLECE LA CONEXION CON EL REPORTE
        SetTableLocation(DocFacturas, ConnectionInfo)

        'ALMACENA RUTA Y NOMBRE DEL ARCHIVO EN ESTE CASO EL DOCNUM DE LA FACTURA
        _rutaPDF = txtref.Text.ToString() + ".pdf"
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
        Dim vMensaje As String = "<html><head></head><body><h2>Factura Cancelada: " & txtfactura.Text.ToString() & "</h2>"
        'vMensaje &= "<br><h3> Refactura: " & txtref.Text.ToString() & "</h3>"
        vMensaje &= "<br><h3> Refactura: " & refactura & "</h3>"
        vMensaje &= "<p>Solicito: " & txtsolicita.Text.ToString()
        vMensaje &= "<br>Requiere Refactura: " & txtrefactura.Text.ToString()
        vMensaje &= "<br>Motivo: " & txtmotivo.Text.ToString()
        vMensaje &= "<br>Comentario: " & txtcomentario.Text.ToString()
        vMensaje &= "<br>Estatus: " & status_ref_env
        'vMensaje &= "<br>Observaciones: " & txtobservaciones.Text.ToString() & "</p><br>"
        vMensaje &= "<p>Estimado usuario:</p><p>Se agrego la Refactura correspondiente a la cancelación solicitada, cualquier duda o aclaración favor de indicarme.</p><p>Saludos cordiales!!!</p>"
        vMensaje &= "</body></html>"

        'ADJUNTA EL MENSAJE AL CUERPO DEL CORREO
        Msg.Body = vMensaje
        Msg.IsBodyHtml = True 'EL CUERPO DEL MENSAJE NO ES HTML

        '------

        'Dim thisAttachment As Attachment = New Attachment(FacturaNombre) ' “image/jpeg”)
        'Msg.Attachments.Add(thisAttachment) 'SE ADJUNTA ARCHIVO PDF

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
        envio_ref_ok = True

        '-----
      End If

    Catch exc As Exception
      'INICIALIZA LA VARIABLE EN FALSO SI EL ENVIO NO SE COMPLETO
      envio_ref_ok = False
      'MENSAJE DE ERROR DE ENVIO DE CORREO
      MessageBox.Show("No fue posible enviar email de la confirmación de Refactura," & Chr(13) & "Favor de avisar la Refactura de manera Telefonica..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !",
            MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return
    Finally
      System.Windows.Forms.Cursor.Current = Cursors.Default
    End Try
  End Sub

  Private Sub dgvrefactura_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgvrefactura.MouseDoubleClick
    Try
      Dim valor As String = dgvrefactura.Item("Factura", dgvrefactura.CurrentRow.Index).Value.ToString
      frmDetalleOrden.ValorFactura(valor)
      frmDetalleOrden.ShowDialog()
    Catch ex As Exception
      MsgBox("No hay datos que mostrar.", MsgBoxStyle.Exclamation, "Datos no encontrados")
    End Try
  End Sub

  Private Sub btnbuscar_Click(sender As Object, e As EventArgs) Handles btnbuscar.Click
    'VARIABLES DE CONSILTAS
    Dim SQLBuscar As String

    'VALIDA QUE POR LO MENOOS UNO DE LOS CHECK ESTE ACTIVO
    If txtref.Text = "" Or txtref.Text = " " Or txtfactura.Text = "" Then
      MsgBox("Favor de colocar la refactura Valida.", MsgBoxStyle.Exclamation, "Alerta de dato")
      txtref.Focus()
      Return
    End If

    '--------

    'SE BUSCA REFACTURA PARA OBTENER EL COMENTARIO Y HABILITA EL BOTON DE GUARDAR.
    Try
      'ABRE LA CONEXION AL TPD
      conexion_universal.conectar_sap()
      'CONSULTA PARA VALIDAR QUE LA REFACTURA SEA VALIDA
      'FORMAT(T0.DocDate, 'yyyy-MM-dd')
      SQLBuscar = "SELECT T0.DocEntry AS DocEntry, T0.DocNum AS Refactura, FORMAT(T0.DocDate, 'yyyy-MM-dd') AS FechaRefactura, T0.Comments "
      SQLBuscar &= "FROM OINV T0 "
      SQLBuscar &= "WHERE T0.DocNum =  '" + txtref.Text + "' "
      'ALMACENA LA CONSULTA EN UN COMANDO PARA PODER HACER LA EJECUCION
      conexion_universal.slq_con = New SqlCommand(SQLBuscar, conexion_universal.conexion_uni_sap)
      'EJECUTA LA CONSULTA
      conexion_universal.rd_con = conexion_universal.slq_con.ExecuteReader()
      'VALIDA QUE EXISTA LA FACTURA DE REFACTURACION
      If conexion_universal.rd_con.Read() Then

        '-----
        txtcom_refactura.Text = conexion_universal.rd_con.Item("Comments")
        brnguardar.Enabled = True
        'CIERRA EL READER
        conexion_universal.rd_con.Close()
        '-----

      Else
        MsgBox("No es posible visualizar el comentario, la factura no Existe. ", MsgBoxStyle.Exclamation, "Alerta de Refactura")
        actualiza_ref_ok = False 'INICIALIZA EN FALSO LA ACTUALIZACION DE LA FACTURA
        conexion_universal.rd_con.Close()
        conexion_universal.cerrar_conectar_sap()
        brnguardar.Enabled = False
        Return
      End If

      '-----

    Catch ex As Exception
      MsgBox("Error en consulta o conexión al Buscar la Refactura: " + ex.ToString, MsgBoxStyle.Critical, "Error de BD SAP")
      actualiza_ref_ok = False 'INICIALIZA EN VERDADERO LA ACTUALIZACION DE LA FACTURA
      conexion_universal.rd_con.Close()
      conexion_universal.cerrar_conectar_sap()
      brnguardar.Enabled = False
      Return
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

End Class