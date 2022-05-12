Imports System.Data.SqlClient

Public Class frmcancelaciones
    'VARIABLE PARA RELLENO DEL COMBO DE USUARIOS Y ALMACENES
    Dim Lista_usu As New ArrayList
    Dim Lista_alm As New ArrayList
    'VARIABLES GLOBAL DEL FORMULARIO
    Dim fi As String
    Dim ff As String
    'VARIABLE PARA FILTRO DE COMBO DE USUARIOS
    Dim FiltroUser As String

    Private Sub btnsolicitud_Click(sender As Object, e As EventArgs) Handles btnsolicitud.Click
        frmsolicitud_cancelacion.MdiParent = Inicio
        frmsolicitud_cancelacion.Show()
    End Sub

    Private Sub frmcancelaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MANDA A LLAMAR EL METODO DE DERECHOS
        MPrivilegios()
        'MANDA A LLENAR EL COMBOBOX DE ALMACEN
        llena_almacen()
        'MANDA A LLENAR EL COMBOBOX DE USUARIOS
        llena_usuarios()
        'MANDA A LLAMAR EL ESTILO DEL GRID
        estilo_grid_cancelaciones()
        'COLOCA EN LA POSICION CERO EL COMBO PARA MOSTRAR VALOR POR DEFECTO
        cmbstatus.SelectedIndex = 0

    End Sub

    Sub MPrivilegios()
        'OBTIENE TODOS LOS USUARIOS CON DERECHOSW
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS DEL TPD
            conexion_universal.conectar()
            'CONSULTA DE OBTENCIÓN DE LOS MOTIVOS
            conexion_universal.slq_s = New SqlCommand("SELECT * FROM Documents_Accesos WHERE Id_Usuario = '" + UsrTPM + "'", conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If conexion_universal.rd_s.Read Then
                'CONSULTAR
                If (conexion_universal.rd_s.Item("Consultar") = "N0") Then
                    btnconsultar.Enabled = False
                End If
                'EXPORTAR
                If (conexion_universal.rd_s.Item("Exportar") = "N0") Then
                    btnexportar.Enabled = False
                End If
                'SOLICITAR
                If (conexion_universal.rd_s.Item("Solicitud") = "N0") Then
                    btnsolicitud.Enabled = False
                End If
                'AUTORIZAR
                If (conexion_universal.rd_s.Item("Autorizar") = "N0") Then
                    btnautorizaciones.Enabled = False
                End If
                'CANCELAR
                If (conexion_universal.rd_s.Item("Cancelar") = "N0") Then
                    btncancelaciones.Enabled = False
                End If
                'CANCELAR
                If (conexion_universal.rd_s.Item("Refacturar") = "N0") Then
                    btnrefacturaciones.Enabled = False
                End If
                If (conexion_universal.rd_s.Item("AutorizarContab") = "N0") Then
                    btnAutorizaContab.Enabled = False
                End If
            Else
                MsgBox("Notienes permisos para este Apartado.", "Acceso Denegado", MsgBoxStyle.Exclamation)
                Return
                Me.Close()
            End If
            conexion_universal.rd_s.Close()
            'CIERRA LA CONEXION
            conexion_universal.cerrar_conectar()
        Catch ex As Exception
            MsgBox("Error de Acceso a este apartado del TPD: " & ex.Message, MsgBoxStyle.Critical)
            conexion_universal.cerrar_conectar()
            Me.Close()
            Return
        End Try 'FIN CAPTURA EL ERROR
    End Sub

    Sub llena_usuarios()
        'OBTIENE TODOS LOS USUARIOS
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS DEL TPD
            conexion_universal.conectar()
            'CONSULTA DE OBTENCIÓN DE LOS MOTIVOS
            conexion_universal.slq_s = New SqlCommand("SELECT Id_Usuario, FiltroUser FROM Documents_Accesos WHERE Id_Usuario = '" + UsrTPM + "'", conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                'ALMACENA QUE USUARIO SE VA MOSTRAR
                FiltroUser = conexion_universal.rd_s.Item("FiltroUser")
            End While
            'CIERRA EL READER
            conexion_universal.rd_s.Close()
            'CIERRA LA CONEXION
            conexion_universal.cerrar_conectar()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión TPD al Obtener el Filtro de Usuario: " & ex.Message, MsgBoxStyle.Critical)
            conexion_universal.cerrar_conectar()
            Return
        End Try 'FIN CAPTURA EL ERROR

        'OBTIENE TODOS LOS USUARIOS
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS DEL TPD
            conexion_universal.conectar()
            'VALIDA QUE USUARIOS MOSTRAR
            If (FiltroUser = "TODOS") Then
                'CONSULTA DE OBTENCIÓN DE LOS USUARIOS
                conexion_universal.slq_s = New SqlCommand("SELECT T0.Id_Usuario, T0.Nombre " +
                "FROM Usuarios T0 INNER JOIN Documents_Accesos T1 ON T0.Id_Usuario = T1.Id_Usuario ORDER BY T0.Nombre ASC", conexion_universal.conexion_uni)
                'COLOCA TODOS EN LA CONSULTA
                Lista_usu.Add("TODOS")
            ElseIf (FiltroUser = "UNO") Then
                'CONSULTA DE OBTENCIÓN DE LOS USUARIOS
                conexion_universal.slq_s = New SqlCommand("SELECT Id_Usuario, Nombre FROM Usuarios WHERE Id_Usuario = '" + UsrTPM + "'", conexion_universal.conexion_uni)
            End If
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                Lista_usu.Add(conexion_universal.rd_s.Item("Nombre"))
            End While
            conexion_universal.rd_s.Close()
            With cmbusuarios
                .DropDownStyle = ComboBoxStyle.DropDown
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DataSource = Lista_usu
                .DisplayMember = "Id_Usuario"
                '.ValueMember = "Id_Usuario"
            End With
            'COLOCA EN LA POSICION CERO EL COMBO PARA MOSTRAR VALOR POR DEFECTO
            cmbusuarios.SelectedIndex = 0
            'CIERRA LA CONEXION
            conexion_universal.cerrar_conectar()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión TPD: " & ex.Message, MsgBoxStyle.Critical)
            conexion_universal.cerrar_conectar()
            Return
        End Try 'FIN CAPTURA EL ERROR
    End Sub

    Sub llena_almacen()
        'OBTIENE TODOS LOS USUARIOS
        Try 'CAPTURA EL ERROR
            'CONECTA A LA BASE DE DATOS DEL SAP
            conexion_universal.conectar_sap()
            'CONSULTA DE OBTENCIÓN DE LOS ALMACENES
            'Modificado por Ivan Gonzalez, se coloco en codigo duro por el momento
            Dim consulta As String
            Dim SQL As New Comandos_SQL()
            SQL.conectarTPM()
      If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Or UsrTPM = "MMAZZOCO" Then

        consulta = "SELECT WhsCode, WhsName FROM OWHS WHERE (WhsCode = 01 or WhsCode = '03' or WhsCode = '07')"
      ElseIf UsrTPM = "OPERACIOND" Then

        consulta = "SELECT WhsCode, WhsName FROM OWHS WHERE (WhsCode = 01 or WhsCode = '06')"


      Else
        Dim almacen As String = SQL.CampoEspecifico("select Almacen from Usuarios where Id_Usuario = '" + UsrTPM + "'", "Almacen")
        consulta = "SELECT WhsCode, WhsName FROM OWHS WHERE WhsCode = '" + almacen + "'"
      End If
      SQL.Cerrar()

      conexion_universal.slq_s = New SqlCommand(consulta, conexion_universal.conexion_uni_sap)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                Lista_alm.Add(conexion_universal.rd_s.Item("WhsName"))
            End While

            If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Or UsrTPM = "MMAZZOCO" Then
                Lista_alm.Add("TODOS")
            End If

            conexion_universal.rd_s.Close()
            With cmbalmacen
                .DropDownStyle = ComboBoxStyle.DropDown
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.ListItems
                .DataSource = Lista_alm
                .DisplayMember = "WhsName"
                '.ValueMember = "ItmsGrpNam"
            End With
            'COLOCA EN LA POSICION CERO EL COMBO PARA MOSTRAR VALOR POR DEFECTO
            'cmbalmacen.SelectedIndex = -1
            cmbalmacen.SelectedIndex = 0

            'CIERRA LA CONEXION
            conexion_universal.cerrar_conectar_sap()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión SAP: " & ex.Message, MsgBoxStyle.Critical)
            conexion_universal.cerrar_conectar_sap()
            Return
        End Try 'FIN CAPTURA EL ERROR
    End Sub

    Sub estilo_grid_cancelaciones() 'ESTILO DEL GRID DE LINEAS
        With Me.dgvcancelaciones
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            '.RowHeadersVisible = False

            .AllowUserToAddRows = False
            'ESTATUS
            .Columns("Estatus").Width = 180
            .Columns("Estatus").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Estatus").ReadOnly = False
            'USUARIO
            .Columns("Usuario").Width = 120
            .Columns("Usuario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Usuario").ReadOnly = False
            'FACTURA
            .Columns("Factura").Width = 60
            .Columns("Factura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("Factura").DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("Factura").ReadOnly = False
            'FECHA FACTURA
            .Columns("FechaFactura").Width = 70
            .Columns("FechaFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaFactura").ReadOnly = False
            'FECHA SOLICITUD
            .Columns("FechaSolicitud").Width = 110
            .Columns("FechaSolicitud").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaSolicitud").ReadOnly = False
            'MOTIVO
            .Columns("Motivo").Width = 180
            .Columns("Motivo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Motivo").ReadOnly = False
            'COMENTARIOS
            .Columns("Comentario").Width = 180
            .Columns("Comentario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Comentario").ReadOnly = False
            'REFACTURA
            .Columns("Refactura").Width = 60
            .Columns("Refactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Refactura").ReadOnly = False
            'ALMACEN
            .Columns("Almacen").Width = 80
            .Columns("Almacen").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Almacen").ReadOnly = False
            'NOTA DE CREDITO
            .Columns("NC").Width = 80
            .Columns("NC").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("NC").ReadOnly = False
            'FECHA DE NOTA DE CREDITO
            .Columns("FechaNC").Width = 80
            .Columns("FechaNC").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaNC").ReadOnly = False
            'CLIENTE
            .Columns("CardCode").Width = 50
            .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("CardCode").ReadOnly = False
            'NOMBRE CLIENTE
            .Columns("CardName").Width = 170
            .Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("CardName").ReadOnly = False
            'COMENTARAIOS SAP
            .Columns("Comments_sap").Width = 180
            .Columns("Comments_sap").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Comments_sap").ReadOnly = False
            'TOTAL
            .Columns("Total").Width = 80
            .Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Total").ReadOnly = False
        End With
    End Sub

    Private Sub btnconsultar_Click(sender As Object, e As EventArgs) Handles btnconsultar.Click
        'VARIABLES DE ALMACENAMIENTO DE DATOS
        Dim status As String
        Dim usuario As String
        Dim almacen As String
        'VARIABLE DE CADENA PARA LA CONSULTA
        Dim SQLConsulta As String

        'OBTIENE LA FECHA INICIAL Y LA FINAL
        fi = dtpfecha_ini.Value.ToString("yyyy-MM-dd")
        ff = dtpfecha_fin.Value.ToString("yyyy-MM-dd")

        'ALMACENA LOS DATOS SELECCIONADOS
        status = cmbstatus.Text.ToString()
        usuario = cmbusuarios.Text.ToString()
        almacen = cmbalmacen.Text.ToString()

        '-----

        'REFRESCA EL DATA GRID VIEW DE RESULTADO
        If dgvcancelaciones.RowCount > 0 Then
            dgvcancelaciones.Rows.Clear()
        End If

        '-----

        Try
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar()
            'ALAMACENA LA CONSULTA
            SQLConsulta = "select *, FORMAT(doc_date, 'yyyy-MM-dd') as date_conver, CONVERT(varchar(50), CONVERT(MONEY, Total), 1) as Total_C from Documents_Cancel where format(cancel_date_hour, 'yyyy-MM-dd') between '" + fi + "' and '" + ff + "' "
            If status <> "TODOS" Then 'VALIDA SI SE REQUIERE TODOS LOS ESTATUS O UNO EN ESPECIFICO
                SQLConsulta &= "and status = '" + status + "' "
            End If
            If usuario <> "TODOS" Then 'VALIDA SI SE REQUIERE TODOS LOS USUARIOS O UNO EN ESPECIFICO
                SQLConsulta &= "and name_user = '" + usuario + "' "
            End If
            If almacen <> "TODOS" Then 'VALIDA SI SE REQUIERE TODOS LOS ALAMACENES O UNO EN ESPECIFICO
                SQLConsulta &= "and warehouse = '" + almacen + "' "
            End If
            SQLConsulta &= " order by cancel_date_hour desc "

            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLConsulta, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While conexion_universal.rd_s.Read
                If dgvcancelaciones.RowCount > 0 Then
                        'MANDA LOS RESULTADOS
                        Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvcancelaciones.Rows.Add(rd_s.Item("status").ToString, rd_s.Item("name_user"), rd_s.Item("doc_num").ToString, rd_s.Item("date_conver").ToString, rd_s.Item("cancel_date_hour").ToString,
                        rd_s.Item("motivo").ToString, rd_s.Item("comments").ToString, rd_s.Item("sustituye").ToString, rd_s.Item("warehouse").ToString, rd_s.Item("doc_num_nc").ToString,
                        rd_s.Item("doc_date_nc").ToString, rd_s.Item("CardCode").ToString, rd_s.Item("CardName").ToString, rd_s.Item("Comments_sap").ToString,
                        rd_s.Item("Total_C").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvcancelaciones
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvcancelaciones.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                    Else
                        'MANDA LOS RESULTADOS
                        Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvcancelaciones.Rows.Add(rd_s.Item("status").ToString, rd_s.Item("name_user"), rd_s.Item("doc_num").ToString, rd_s.Item("date_conver").ToString, rd_s.Item("cancel_date_hour").ToString,
                        rd_s.Item("motivo").ToString, rd_s.Item("comments").ToString, rd_s.Item("sustituye").ToString, rd_s.Item("warehouse").ToString, rd_s.Item("doc_num_nc").ToString,
                        rd_s.Item("doc_date_nc").ToString, rd_s.Item("CardCode").ToString, rd_s.Item("CardName").ToString, rd_s.Item("Comments_sap").ToString,
                        rd_s.Item("Total_C").ToString)

                        'RECORRE EL DATA GRID VIEW
                        With dgvcancelaciones
                                'ESTABLECE LA CELDA ACTUAL
                                .CurrentCell = .Rows(Me.dgvcancelaciones.Rows.Count - 1).Cells(0)
                            End With
                        Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                            'MANDA EL MENSAJE DE ERROR
                            MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                            Return
                        End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                    End If
            End While
            'RECORRE EL DATAGRIDVIEW PARA COMPARAR EL STATUS Y PODER COLOCAR EL COLOR QUE SE REQUIERA.
            For i As Integer = 0 To dgvcancelaciones.Rows.Count - 1
                'COMPARA EL ESTATUS
                If dgvcancelaciones.Rows(i).Cells("Estatus").Value.ToString = "NO PROCEDE" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    dgvcancelaciones.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                    dgvcancelaciones.Rows(i).DefaultCellStyle.ForeColor = Color.White

                ElseIf dgvcancelaciones.Rows(i).Cells("Estatus").Value.ToString = "EN AUTORIZACION CONTABLE" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    dgvcancelaciones.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
                    dgvcancelaciones.Rows(i).DefaultCellStyle.ForeColor = Color.Black

                ElseIf dgvcancelaciones.Rows(i).Cells("Estatus").Value.ToString = "EN PROCESO DE CANCELACION" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    dgvcancelaciones.Rows(i).DefaultCellStyle.BackColor = Color.Khaki
                    dgvcancelaciones.Rows(i).DefaultCellStyle.ForeColor = Color.Black

                ElseIf dgvcancelaciones.Rows(i).Cells("Estatus").Value.ToString = "PENDIENTE REFACTURACION" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    dgvcancelaciones.Rows(i).DefaultCellStyle.BackColor = Color.DarkOrange
                    dgvcancelaciones.Rows(i).DefaultCellStyle.ForeColor = Color.Black

                ElseIf dgvcancelaciones.Rows(i).Cells("Estatus").Value.ToString = "FINALIZADO" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    dgvcancelaciones.Rows(i).DefaultCellStyle.BackColor = Color.OliveDrab
                    dgvcancelaciones.Rows(i).DefaultCellStyle.ForeColor = Color.White
                End If
            Next
            conexion_universal.cerrar_conectar()
        Catch ex As Exception
            MsgBox("Error de consulta o conexión TPD en llenado de GRID: " & ex.Message, MsgBoxStyle.Critical)
            conexion_universal.cerrar_conectar()
            Return
        End Try 'FIN CAPTURA EL ERROR
    End Sub

    Private Sub btnexportar_Click(sender As Object, e As EventArgs) Handles btnexportar.Click
        'ALMACENA EL PERIODO DE LAS FECHAS
        'Dim inicial As String = dtpfecha_ini.Value.ToString("yyyy-MM-dd")
        'Dim final As String = ndt

        '-----------------------------EXPORTAR A EXCEL LOS RESULTADOS OBTENIDOS ---------------------------------------------------
        'VALIDA QUE EL DATAGRID VIEW NO ESTE VACIO
        If dgvcancelaciones.RowCount > 0 Then
            'CONTADOR DE LAS COLUMNAS DONDE TENDRA QUE EMPEZAR EL EMPATE CON EL EXCEL
            Dim con, confila As Integer
            con = 4
            confila = 0
            'DECLARACIÓN DE VARIABLES PARA LIBRO DE EXCEL CREANDO UN OBJETO DE EXCEL
            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
            Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
            Try
                'AÑADIMOS EL LIBRO AL PROGRAMA , Y LA HOJA AL LIBRO
                exLibro = exApp.Workbooks.Add
                exHoja = exLibro.Worksheets.Add()
                ' ¿CUANTAS COLUMNAS Y CUANTAS FILAS?
                Dim NCol As Integer = dgvcancelaciones.ColumnCount
                Dim NRow As Integer = dgvcancelaciones.RowCount
                'AQUI Aqui recorremos todas las filas, y por cada fila todas las columnas
                'y vamos escribiendo.
                exHoja.Cells.Item(1, 1) = "Cancelación de Facturas del periodo " + fi + " al " + ff
                For i As Integer = 1 To NCol
                    'COLOCA EL NOMBRE DE LAS COLUMNAS DEL DATAGRID
                    'exHoja.Cells.Item(1, i) = dgvres.Columns(i - 1).Name.ToString
                    exHoja.Cells.Item(3, 1) = "Estatus"
                    exHoja.Cells.Item(3, 2) = "Usuarios"
                    exHoja.Cells.Item(3, 3) = "Factura"
                    exHoja.Cells.Item(3, 4) = "Fecha factura"
                    exHoja.Cells.Item(3, 5) = "Fecha solicitud"
                    exHoja.Cells.Item(3, 6) = "Motivo"
                    exHoja.Cells.Item(3, 7) = "Comentarios"
                    exHoja.Cells.Item(3, 8) = "Refacturacon"
                    exHoja.Cells.Item(3, 9) = "Almacen"
                    exHoja.Cells.Item(3, 10) = "Nota de Credito"
                    exHoja.Cells.Item(3, 11) = "Fecha de NC"
                    exHoja.Cells.Item(3, 12) = "Cliente"
                    exHoja.Cells.Item(3, 13) = "Nombre Cli."
                    exHoja.Cells.Item(3, 14) = "Comentario SAP"
                    exHoja.Cells.Item(3, 15) = "Total"
                Next
                For Fila As Integer = 0 To NRow - 1
                    For Col As Integer = 0 To NCol - 1
                        exHoja.Cells.Item(Fila + 4, Col + 1) = _
                        dgvcancelaciones.Rows(Fila).Cells(Col).Value
                    Next
                Next
                'TITULO EN NEGRITA, ALINEADO AL CENTRO Y QUE EL TAMAÑO DE LA COLUMNA
                'COLOCA NEGRITAS LA FILA 1
                exHoja.Rows.Item(1).Font.Bold = 1
                'COLOCA NEGRITAS LA FILA 3
                exHoja.Rows.Item(3).Font.Bold = 1
                'ALINEACION DE LA FILA 3
                exHoja.Rows.Item(3).HorizontalAlignment = 3
                'SE AJUSTE AL TEXTO
                exHoja.Columns.AutoFit()
                'COLOCA EL NOMBRE DE LA HOJA
                exHoja.Name = "Cancelaciones".ToString
                'MUESTRA EN PANTALLA EL EXCEL
                exApp.Application.Visible = True
                exHoja = Nothing
                exLibro = Nothing
                exApp = Nothing
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")
            End Try
        Else
            'MANDA MENSAJE DE ERROR
            MsgBox("No hay datos que Exportar", MsgBoxStyle.Exclamation, "Datos no Encontrados")
        End If
    End Sub

    Private Sub dtpfecha_ini_ValueChanged(sender As Object, e As EventArgs) Handles dtpfecha_ini.ValueChanged
        'AL CAMBIAR LA FECHA FINAL REFRESCA EL DATA GRID VIEW DE RESULTADO
        If dgvcancelaciones.RowCount > 0 Then
            dgvcancelaciones.Rows.Clear()
        End If
    End Sub

    Private Sub dtpfecha_fin_ValueChanged(sender As Object, e As EventArgs) Handles dtpfecha_fin.ValueChanged
        'AL CAMBIAR LA FECHA FINAL REFRESCA EL DATA GRID VIEW DE RESULTADO
        If dgvcancelaciones.RowCount > 0 Then
            dgvcancelaciones.Rows.Clear()
        End If
    End Sub

    Private Sub btnautorizaciones_Click(sender As Object, e As EventArgs) Handles btnautorizaciones.Click
        frmautorizacion_cancelacion.MdiParent = Inicio
        frmautorizacion_cancelacion.Show()
    End Sub

    Private Sub btncancelaciones_Click(sender As Object, e As EventArgs) Handles btncancelaciones.Click
        frmcancelacion_nc.MdiParent = Inicio
        frmcancelacion_nc.Show()
    End Sub

    Private Sub btnrefacturaciones_Click(sender As Object, e As EventArgs) Handles btnrefacturaciones.Click
        frmrefactura.MdiParent = Inicio
        frmrefactura.Show()
    End Sub

    Private Sub dgvcancelaciones_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgvcancelaciones.MouseDoubleClick
        'MUESTRA EL DETALLE DE FACTURA
        Try
            Dim valor As String = dgvcancelaciones.Item("Factura", dgvcancelaciones.CurrentRow.Index).Value.ToString
            frmDetalleOrden.ValorFactura(valor)
            frmDetalleOrden.ShowDialog()
        Catch ex As Exception
            MsgBox("No hay datos que mostrar.", MsgBoxStyle.Exclamation, "Datos no encontrados")
        End Try
    End Sub

    Private Sub btnAutorizaContab_Click(sender As Object, e As EventArgs) Handles btnAutorizaContab.Click
        'MUESTRA EL FORMULARIO DE AUTORIZACIÓN PARA CONTABILIDAD
        frmAutorizaContabilidad.MdiParent = Inicio
        frmAutorizaContabilidad.Show()
    End Sub

    Private Sub panel_cancelaciones_Paint(sender As Object, e As PaintEventArgs) Handles panel_cancelaciones.Paint

    End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    Dim nfac As String
    nfac = InputBox("Ingrese el numero de factura a probar", "Num Factura", "")
    frmcancelacion_nc.PruebaProcesoCancelacion(nfac)
    MsgBox("Proceso terminado abra la ruta: Z:\TPD\DoctosPruebaCancelacion")
  End Sub
End Class