Imports System.Data.SqlClient

Public Class frmStatusVentas
    'VARIABLES GLOBAL DEL FORMULARIO
    Dim fi As String
    Dim ff As String
    'VARIABLES PARA OBTENER LOS DIAS TRANSCURRIDOS
    Dim Fc_conv As Date
    Dim Fa_conv As Date
    Dim dias_trans As Integer
    Dim ts As TimeSpan

    Private Sub frmStatusVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'CARGA LA FECHA DEL DATA TIME PICKER FINAL EN UNA SEMANA ANTES DE LA FECHA ACTUAL
        dtpfecha_ini.Value = DateTime.Now.AddDays(-5)

        'MANDA A LLAMAR AL METODO DE ESTILO DE GRID
        MEstilo_grid_auto()

        'MANADA A LLAMAR EL METODO DE LLENADO DE GRID
        MllenaDetallePedido()

    End Sub

    ' SECCIÓN DE METODOS REALIZADOS ================================================================================================================== INICIO

    '----- INICIO ESTILO DE GRID
    Sub MEstilo_grid_auto() 'ESTILO DEL GRID DE LINEAS
        With Me.dgvDetallePed
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            '.RowHeadersVisible = False

            .AllowUserToAddRows = False
            'NOMBRE DEL USUARIO
            .Columns("User").Width = 100
            .Columns("User").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("User").ReadOnly = True
            'ESTADO DE LA AUTORIZACION
            .Columns("Status").Width = 80
            .Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Status").ReadOnly = True
            'ORDEN DE VENTA
            .Columns("DocEntry").Width = 60
            .Columns("DocEntry").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("DocEntry").DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("DocEntry").ReadOnly = True
            'DOCUMENTO PRELIMINAR
            .Columns("DraftEntry").Width = 60
            .Columns("DraftEntry").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DraftEntry").ReadOnly = True
            'FECHA DE CREACION
            .Columns("TaxDate").Width = 80
            .Columns("TaxDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("TaxDate").ReadOnly = True
            'HORA DE CREACION
            .Columns("DateHour").Width = 80
            .Columns("DateHour").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DateHour").ReadOnly = True
            'CLIENTE
            .Columns("CardCode").Width = 50
            .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("CardCode").ReadOnly = True
            'NOMBRE DEL CLIENTE
            .Columns("CardName").Width = 150
            .Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("CardName").ReadOnly = True
            'CODIGO DE ALMACEN
            .Columns("WhsCode").Width = 50
            .Columns("WhsCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("WhsCode").ReadOnly = True
            'NOMBRE DE ALMACEN
            .Columns("WhsName").Width = 80
            .Columns("WhsName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("WhsName").ReadOnly = True
            'MONTO TOTAL DEL BORRADOR
            .Columns("DocTotal").Width = 80
            .Columns("DocTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DocTotal").DefaultCellStyle.Format = "$ ###,###,###.00"
            .Columns("DocTotal").ReadOnly = True
            'DIAS TRASNCURRIDOS
            .Columns("Days").Width = 50
            .Columns("Days").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Days").ReadOnly = True
            'USUARIO QUE AUTORIZA
            .Columns("Autoriza").Width = 150
            .Columns("Autoriza").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Autoriza").ReadOnly = True
            'FECHA DE AUTORIZACIÓN
            .Columns("FechaAutoriza").Width = 80
            .Columns("FechaAutoriza").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaAutoriza").ReadOnly = True
            'HORA DE AUTORIZACION
            .Columns("HoraAutoriza").Width = 80
            .Columns("HoraAutoriza").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("HoraAutoriza").ReadOnly = True
            'COMENTARIOS COBRANZA
            .Columns("Comment").Width = 150
            .Columns("Comment").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Comment").ReadOnly = True
        End With
    End Sub
    '----- FIN ESTILO DE GRID


    '----- INICIO DE METODO DE LLENADO DEL GRID
    Sub MllenaDetallePedido()
        'VARIABLES DE ALMACENAMIENTO DE DATOS
        Dim status As String

        'VARIABLE DE CADENA PARA LA CONSULTA
        Dim SQLConsulta As String

        'OBTIENE LA FECHA INICIAL Y LA FINAL
        fi = dtpfecha_ini.Value.ToString("yyyy-MM-dd")
        ff = dtpfecha_fin.Value.ToString("yyyy-MM-dd")

        'ALMACENA LOS DATOS SELECCIONADOS
        status = cmbEstado.Text.ToString()

        '-----

        'REFRESCA EL DATA GRID VIEW DE RESULTADO
        If dgvDetallePed.RowCount > 0 Then
            dgvDetallePed.Rows.Clear()
        End If

        '-----

        Try
            'CONECTA A LA BASE DE DATOS
            'conexion_universal.conectar()
            conexion_universal.conectar_sap()
            'ALAMACENA LA CONSULTA
            ''''''SQLConsulta = "select *, FORMAT(doc_date, 'yyyy-MM-dd') as date_conver, CONVERT(varchar(50), CONVERT(MONEY, Total), 1) as Total_C from Documents_Cancel where format(cancel_date_hour, 'yyyy-MM-dd') between '" + fi + "' and '" + ff + "' "
            ''''''If status <> "TODOS" Then 'VALIDA SI SE REQUIERE TODOS LOS ESTATUS O UNO EN ESPECIFICO
            ''''''    SQLConsulta &= "and status = '" + status + "' "
            ''''''End If
            ''''''If usuario <> "TODOS" Then 'VALIDA SI SE REQUIERE TODOS LOS USUARIOS O UNO EN ESPECIFICO
            ''''''    SQLConsulta &= "and name_user = '" + usuario + "' "
            ''''''End If
            ''''''If Almacen <> "TODOS" Then 'VALIDA SI SE REQUIERE TODOS LOS ALAMACENES O UNO EN ESPECIFICO
            ''''''    SQLConsulta &= "and warehouse = '" + Almacen + "' "
            ''''''End If
            ''''''SQLConsulta &= " order by cancel_date_hour desc "


            SQLConsulta = "SELECT DISTINCT (T0.DraftEntry) AS ClavePreliminar, T0.OwnerID AS CodigoUsuario, T1.U_NAME AS NombreUsuario, T1.USER_CODE AS Usuario, "
            SQLConsulta &= "Case WHEN T0.Status = 'Y' THEN 'Autorizado' "
            SQLConsulta &= "WHEN T0.Status = 'W' THEN 'Pendiente' "
            SQLConsulta &= "WHEN T0.Status = 'N' THEN 'Rechazado' "
            SQLConsulta &= "WHEN T0.Status = 'C' THEN 'Cancelado' "
            SQLConsulta &= "WHEN T0.Status = 'P' THEN 'Creado' "
            SQLConsulta &= "WHEN T0.Status = 'A' THEN 'Autorizando' "
            SQLConsulta &= "End As EstadoNombre, IIf(T0.DocEntry Is NULL, 0, T0.DocEntry) AS OrdenPed, "
            SQLConsulta &= "IIf(T2.DocNum Is NULL, 0, T2.DocNum) As DocPreliminar, "
            SQLConsulta &= "Format(T2.DocDate, 'yyyy-MM-dd') AS FechaBorrador, "
            SQLConsulta &= "T0.CreateTime As HoraBorrador, T2.CardCode As CodCliente, T2.CardName As NombreCliente, "
            SQLConsulta &= "T3.WhsCode AS CodAlmacen, T4.WhsName As NombreAlmacen, CONVERT(varchar(50), CONVERT(MONEY, T2.DocTotal), 1) As TotalBorrador, 0 As DiasTrans, "
            SQLConsulta &= "(SELECT IIf(UserID Is NULL, '-', UserID) As UserID FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')) AS CodAutoriza, "
            SQLConsulta &= "(SELECT V.U_NAME FROM WDD1 T INNER JOIN OUSR V ON T.UserID = V.USERID WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')) AS NombreAutoriza, "
            SQLConsulta &= "(SELECT FORMAT(UpdateDate, 'yyyy-MM-dd') FROM WDD1 WHERE WddCode = T0.WddCode AND (Status = 'Y' OR Status = 'N')) AS FechaAutorizacion,  "
            SQLConsulta &= "(SELECT UpdateTime FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')) AS HoraAutorizacion, "
            SQLConsulta &= "(SELECT Remarks FROM WDD1 WHERE WddCode = T0.WddCode And (Status = 'Y' OR Status = 'N')) AS ComentariosCobranza "
            SQLConsulta &= "From OWDD T0 INNER Join OUSR T1 ON T0.OwnerID = T1.USERID "
            SQLConsulta &= "INNER Join ODRF T2 ON T2.DocEntry = T0.DraftEntry "
            SQLConsulta &= "INNER Join DRF1 T3 ON T2.DocEntry = T3.DocEntry "
            SQLConsulta &= "INNER Join OWHS T4 ON T3.WhsCode = T4.WhsCode "
            SQLConsulta &= "WHERE T2.DocDate BETWEEN '" + fi + "' AND '" + ff + "' And T0.ObjType = 17 "
            'SQLConsulta &= "WHERE T0.DraftEntry = 116310 And T0.ObjType = 17 And T4.WhsCode = '03' "

            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLConsulta, conexion_universal.conexion_uni_sap)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            While (conexion_universal.rd_s.Read())
                'CONVIERTE LAS FECHAS DE SQL A LAS FECHAS DEL SISTEMA
                Fc_conv = Convert.ToDateTime(rd_s.Item("FechaBorrador")) 'CONVIERTE STRING A DATA
                Fa_conv = Convert.ToDateTime(rd_s.Item("FechaAutorizacion"))

                'OBTIENE LOS DIAS TRANSCURRIDOS
                ts = (Fa_conv - Fc_conv)
                dias_trans = ts.Days 'LOS ALMACENA EN UNA VARIABLE

                If dgvDetallePed.RowCount > 0 Then
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvDetallePed.Rows.Add(rd_s.Item("NombreUsuario").ToString, rd_s.Item("EstadoNombre"), rd_s.Item("OrdenPed"), rd_s.Item("DocPreliminar").ToString, rd_s.Item("FechaBorrador").ToString, rd_s.Item("HoraBorrador").ToString,
                        rd_s.Item("CodCliente").ToString, rd_s.Item("NombreCliente").ToString, rd_s.Item("CodAlmacen").ToString, rd_s.Item("NombreAlmacen").ToString, rd_s.Item("TotalBorrador").ToString, dias_trans.ToString,
                        rd_s.Item("NombreAutoriza").ToString, rd_s.Item("FechaAutorizacion").ToString, rd_s.Item("HoraAutorizacion").ToString, rd_s.Item("ComentariosCobranza").ToString)
                        'RECORRE EL DATA GRID VIEW
                        With dgvDetallePed
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvDetallePed.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                Else
                    'MANDA LOS RESULTADOS
                    Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        Me.dgvDetallePed.Rows.Add(rd_s.Item("NombreUsuario").ToString, rd_s.Item("EstadoNombre"), rd_s.Item("OrdenPed"), rd_s.Item("DocPreliminar").ToString, rd_s.Item("FechaBorrador").ToString, rd_s.Item("HoraBorrador").ToString,
                        rd_s.Item("CodCliente").ToString, rd_s.Item("NombreCliente").ToString, rd_s.Item("CodAlmacen").ToString, rd_s.Item("NombreAlmacen").ToString, rd_s.Item("TotalBorrador").ToString, dias_trans.ToString,
                        rd_s.Item("NombreAutoriza").ToString, rd_s.Item("FechaAutorizacion").ToString, rd_s.Item("HoraAutorizacion").ToString, rd_s.Item("ComentariosCobranza").ToString)

                        'RECORRE EL DATA GRID VIEW
                        With dgvDetallePed
                            'ESTABLECE LA CELDA ACTUAL
                            .CurrentCell = .Rows(Me.dgvDetallePed.Rows.Count - 1).Cells(0)
                        End With
                    Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                        'MANDA EL MENSAJE DE ERROR
                        MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
                        Return
                    End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
                End If
            End While
            'RECORRE EL DATAGRIDVIEW PARA COMPARAR EL STATUS Y PODER COLOCAR EL COLOR QUE SE REQUIERA.
            For i As Integer = 0 To dgvDetallePed.Rows.Count - 1
                'COMPARA EL ESTATUS
                If dgvDetallePed.Rows(i).Cells("Status").Value.ToString = "Autorizado" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    'dgvDetallePed.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                    dgvDetallePed.Rows(i).DefaultCellStyle.ForeColor = Color.Green

                ElseIf dgvDetallePed.Rows(i).Cells("Status").Value.ToString = "Pendiente" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    'dgvDetallePed.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
                    dgvDetallePed.Rows(i).DefaultCellStyle.ForeColor = Color.Black

                ElseIf dgvDetallePed.Rows(i).Cells("Status").Value.ToString = "Rechazado" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    'dgvDetallePed.Rows(i).DefaultCellStyle.BackColor = Color.Khaki
                    dgvDetallePed.Rows(i).DefaultCellStyle.ForeColor = Color.DarkOrange

                ElseIf dgvDetallePed.Rows(i).Cells("Status").Value.ToString = "Cancelado" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    'dgvDetallePed.Rows(i).DefaultCellStyle.BackColor = Color.DarkOrange
                    dgvDetallePed.Rows(i).DefaultCellStyle.ForeColor = Color.DarkRed

                ElseIf dgvDetallePed.Rows(i).Cells("Status").Value.ToString = "Creado" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    'dgvDetallePed.Rows(i).DefaultCellStyle.BackColor = Color.OliveDrab
                    dgvDetallePed.Rows(i).DefaultCellStyle.ForeColor = Color.Black

                ElseIf dgvDetallePed.Rows(i).Cells("Status").Value.ToString = "Autorizando" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    'dgvDetallePed.Rows(i).DefaultCellStyle.BackColor = Color.OliveDrab
                    dgvDetallePed.Rows(i).DefaultCellStyle.ForeColor = Color.Green
                End If
            Next
            conexion_universal.cerrar_conectar_sap()
        Catch ex As Exception
            'MsgBox("Error de consulta o conexión TPD en llenado de GRID: " & ex.Message, MsgBoxStyle.Critical)
            conexion_universal.cerrar_conectar_sap()
            Return
        End Try 'FIN CAPTURA EL ERROR
    End Sub
    '----- FIN DE METODO DE LLENADO DEL GRID

    ' SECCIÓN DE METODOS REALIZADOS ================================================================================================================== FIN

    '-----
    'NOTÓN DE CONSULTAR ATUTORIZACIONES
    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click

    End Sub



End Class