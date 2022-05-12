'LIBRERIA REQUERIDA PARA CARGAR EL CRYSTAL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
'LIBRERIA DEL SQLSERVER
Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading

Public Class frmMostrarOrdenes
    Dim DsOrdenes As DataSet 'DECLARA DATASET PARA PROCEDIMIENTO DE INSERTAR ORDENES
    Dim ResOrdPed As DataView = New DataView 'DECLARA DATAVIEW PARA MOSTRAR EL RESULTADO EN LOS GRID
    Dim DetailOrdPed As DataView 'DECLARA DATAVIEW PARA MOSTRAR EL RESULTADO EN LOS GRID
    Dim fi As String 'VARIABLE PARA OBTENER LA FECHA DEL DATAPICKER
    'VARIABLE PARA CREAR LA COLUMNA DE UNA ACTION EN EL GRID Y PODER HACER LA ACCION DE SURTIR O DESCARTAR
    Dim LinkAction As DataGridViewLinkColumn = New DataGridViewLinkColumn()
    Dim Resultado As DataView 'VARIABLE DE DATAVIEW PARA LLENAR EL GRID DE ORDENES
    Dim Dia As String 'VARIABLE QUE ALMACENA EL DIA DEL DATAPICKER PARA SABER HORARIO DE PAQUETERIA

    'DECLARACION DE VARIABLE DE REPORTE Y INSTANCIA DEL MISMO
    Dim DocOrdenes As ReportDocument = New ReportDocument()
    'VARIBALE PARA EL PASO DE PARAMETROS DEL CRYSTAL
    Dim DocKey = String.Empty
    Dim ObjectType = String.Empty
    Dim _rutaPDF As String '// ALMACENA LA RUTA DEL PDF

    'VARIABLES PARA BASE DE DATOS
    Dim BaseDatosTPD As String = "TPM"
    Dim BaseDatosSAP As String = "SBO_TPD"

    'Colores creados para uso en el grid DGVoperacionVta
    Dim rojo As Color
    Dim amarillo As Color
    Dim verde As Color

    Dim DocNum As Integer
    Dim DocNum_P As Integer
    Dim SQL As New Comandos_SQL()

    'MODIFICADO POR IVAN GONZALEZ PARA QUE SE ACTUALICE EN SEGUNDO PLANO(BACKGROUND)
    'VARIABLES DE THREADS(HILOS)
    Dim thread As New Thread(AddressOf MyBackgroundThread)

    Sub MyBackgroundThread()
        'MEjecuta_Orden()
        'MEjecuta_Orden_Ped()
        'MEjecuta_Orden_Ped_Det()
        MLlenaOrdenes()

        DocNum_P = 0
        dgvDetalle.DataSource = Nothing
        Console.WriteLine("Se actualizaron los Grid: " + DateTime.Now)
    End Sub

    '---- GENERAL
#Region "General"
    Private Sub frmMostrarOrdenes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MEjecuta_Orden()
        'MEjecuta_Orden_Ped()
        'MEjecuta_Orden_Ped_Det()
        MLlenaOrdenes()

        DocNum_P = 0
        dgvDetalle.DataSource = Nothing

        Timer1.Enabled = True
        CheckForIllegalCrossThreadCalls = False
        NuevaOrden()
    End Sub

    Private Sub NuevaOrden()

        If dgvOrdenes.Rows.Count > 0 Then

            'If (dgvOrdenes.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Surtir") Then

            For Each Fila As DataGridViewRow In dgvOrdenes.Rows

                '//Puedes hacer una validación con el nombre de la columna
                If Fila.Cells("Accion").Value.ToString = "Surtir" Then
                    My.Computer.Audio.Play(My.Resources.alarma_humo, AudioPlayMode.Background)
                End If


            Next
            'End If
        End If


        'Dim filas As Integer

        'filas = dgvOrdenes.Rows.Count

        'MessageBox.Show(filas)

        'If filas = filas Then

        'Else
        '    My.Computer.Audio.Play(My.Resources.alarma_humo, AudioPlayMode.Background)
        'End If

    End Sub
#End Region
    '---- FIN GENERAL

    '----- METODOS
#Region "Metodos"
    'METODO DE ESTILO PARA EL GRID ORDENES
    Sub MEstiloGridOrdenes()
        Try
            If dgvOrdenes.Rows.Count <> 0 Then
                'ESTILOS POR COLUMNA
                With Me.dgvOrdenes
                    'COLOCA PROPIEDADES DE COLOR ALTERNADOS
                    Dim clr1 As Color
                    clr1 = ColorTranslator.FromHtml("#deeaf6")
                    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .AlternatingRowsDefaultCellStyle.BackColor = Color.White
                    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
                    .DefaultCellStyle.BackColor = clr1
                    .DefaultCellStyle.SelectionForeColor = Color.White
                    .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
                    .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    'ORDEN DE VENTA
                    .Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("DocNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns("DocNum").DefaultCellStyle.ForeColor = Color.Red
                    .Columns("DocNum").HeaderText = "Orden Vta."
                    .Columns("DocNum").HeaderCell.Style.WrapMode = DataGridViewTriState.True
                    .Columns("DocNum").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("DocNum").Width = 70
                    .Columns("DocNum").ReadOnly = True
                    'FECHA DE ORDEN
                    .Columns("DocDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("DocDate").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                    .Columns("DocDate").HeaderText = "Fecha Doc."
                    .Columns("DocDate").HeaderCell.Style.WrapMode = DataGridViewTriState.True
                    .Columns("DocDate").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("DocDate").Width = 85
                    .Columns("DocDate").ReadOnly = True
                    'HORA DE IMPRESION
                    .Columns("PrintTime").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("PrintTime").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                    .Columns("PrintTime").HeaderText = "Hora Creación"
                    .Columns("PrintTime").HeaderCell.Style.WrapMode = DataGridViewTriState.True
                    .Columns("PrintTime").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("PrintTime").Width = 65
                    .Columns("PrintTime").ReadOnly = True
                    .Columns("PrintTime").Frozen = True
                    'PARTIDAS POR ORDEN
                    .Columns("LineNumTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("LineNumTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns("LineNumTotal").HeaderText = "Partidas"
                    .Columns("LineNumTotal").HeaderCell.Style.WrapMode = DataGridViewTriState.True
                    .Columns("LineNumTotal").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("LineNumTotal").Width = 50
                    .Columns("LineNumTotal").ReadOnly = True
                    'PAQUETERIA
                    .Columns("TrnspName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns("TrnspName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                    .Columns("TrnspName").HeaderText = "Paquereria"
                    .Columns("TrnspName").HeaderCell.Style.WrapMode = DataGridViewTriState.True
                    .Columns("TrnspName").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("TrnspName").Width = 130
                    .Columns("TrnspName").ReadOnly = True
                    'HORARIO DE PAQUEREIAS
                    .Columns("TrnspCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("TrnspCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                    .Columns("TrnspCode").HeaderText = "Horario paq."
                    .Columns("TrnspCode").HeaderCell.Style.WrapMode = DataGridViewTriState.True
                    .Columns("TrnspCode").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("TrnspCode").Width = 90
                    .Columns("TrnspCode").ReadOnly = True
                    'CODIGO CLIENTE
                    .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns("CardCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                    .Columns("CardCode").HeaderText = "Cliente"
                    .Columns("CardCode").HeaderCell.Style.WrapMode = DataGridViewTriState.True
                    .Columns("CardCode").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("CardCode").Width = 70
                    .Columns("CardCode").ReadOnly = True
                    'NOMBRE CLIENTE
                    .Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns("CardName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                    .Columns("CardName").HeaderText = "Nombre"
                    .Columns("CardName").HeaderCell.Style.WrapMode = DataGridViewTriState.True
                    .Columns("CardName").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("CardName").Width = 150
                    .Columns("CardName").ReadOnly = True
                    'COMENTARIOS
                    .Columns("Comment").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns("Comment").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                    .Columns("Comment").HeaderText = "Comentario SAP"
                    .Columns("Comment").HeaderCell.Style.WrapMode = DataGridViewTriState.True
                    .Columns("Comment").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("Comment").Width = 150
                    .Columns("Comment").ReadOnly = True
                    'PERSONAL SURTIDOR Y ALMACENISTA
                    .Columns("Surtidor").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Surtidor").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                    .Columns("Surtidor").HeaderText = "Surtidor"
                    .Columns("Surtidor").HeaderCell.Style.WrapMode = DataGridViewTriState.True
                    .Columns("Surtidor").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("Surtidor").Width = 100
                    .Columns("Surtidor").ReadOnly = True
                    'IMPRIMIR PACKLIST
                    .Columns("Printed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Printed").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                    .Columns("Printed").HeaderText = "Impresión pedido"
                    .Columns("Printed").HeaderCell.Style.WrapMode = DataGridViewTriState.True
                    .Columns("Printed").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("Printed").Width = 50
                    .Columns("Printed").ReadOnly = True
                    .Columns("Printed").Visible = False
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub
    'METODO DE ESTILO PARA EL GRID DETALLE
    'Grid de la derecha
    Sub MEstiloGridDetalle()
        'ESTILOS POR COLUMNA
        With Me.dgvDetalle
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            Dim clr1 As Color
            clr1 = ColorTranslator.FromHtml("#deeaf6")
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.White
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = clr1
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'PARTIDAS
            .Columns("# Partida").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("# Partida").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            .Columns("# Partida").Width = 30
            .Columns("# Partida").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("# Partida").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns("# Partida").DefaultCellStyle.ForeColor = Color.Red
            'ARTICULO
            .Columns("Cod. Artículo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Cod. Artículo").Width = 80
            .Columns("Cod. Artículo").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Cod. Artículo").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns("Cod. Artículo").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            'DESCRIPTION
            .Columns("Descripción").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Descripción").Width = 200
            .Columns("Descripción").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Descripción").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns("Descripción").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            'CANTIDAD
            .Columns("Cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Cantidad").Width = 30
            .Columns("Cantidad").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Cantidad").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns("Cantidad").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            'SURTIDO (REAL)
            .Columns("Surtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Surtido").Width = 30
            .Columns("Surtido").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Surtido").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns("Surtido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
        End With
    End Sub

    'Sub MEjecuta_Orden()
    '  Try
    '    SQL.EjecutarProcedimientoIUD("SP_Operacion_Ord", "@fecha", 1, dtpFecha.Value.ToString("yyyy-MM-dd"))
    '  Catch ex As Exception
    '    MessageBox.Show("Error al insertar las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Return
    '  End Try
    'End Sub

    'METODO QUE EJECUTA LA CONSULTA QUE OBTIENE TODAS LAS ORDENES DE VENTA DEL DIA Y 7 DIAS ATRAS
    'Sub MEjecuta_OrdenViejo()
    '  'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
    '  Dim cmd4 As SqlCommand
    '  Dim cnn As SqlConnection = Nothing
    '  Dim da As SqlDataAdapter
    '  DsOrdenes = New DataSet

    '  'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
    '  Try
    '    cnn = New SqlConnection(StrTpm) 'ORIGINAL
    '    'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBAS
    '    cmd4 = New SqlCommand("SP_Operacion_Ord", cnn)
    '    cmd4.CommandType = CommandType.StoredProcedure
    '    cmd4.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value))
    '    'MsgBox(DTPFecha.Value.ToString("dddd"))
    '    cnn.Open()
    '    da = New SqlDataAdapter
    '    da.SelectCommand = cmd4
    '    da.SelectCommand.Connection = cnn
    '    da.SelectCommand.CommandTimeout = 10000
    '    cmd4.ExecuteNonQuery()
    '    cmd4.Connection.Close()
    '    cnn.Close()
    '  Catch ex As Exception
    '    MessageBox.Show("Error al insertar las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Return
    '  End Try
    '  'FIN OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
    'End Sub

    'Sub MEjecuta_Orden_Ped()
    '  Try
    '    SQL.EjecutarProcedimientoIUD("SP_Operacion_Ord_Ped", "@fecha", 1, dtpFecha.Value.ToString("yyyy-MM-dd"))
    '  Catch ex As Exception
    '    MessageBox.Show("Error al Mostrar los datos de las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Return
    '  End Try
    'End Sub

    ''METODO QUE EJECUTA LA CONSULTA QUE MUESTRA LAS ORDENES DE VENTA EN PEDIDO PARA ALMACEN
    'Sub MEjecuta_Orden_PedViejo()
    '  'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
    '  Dim cmd As SqlCommand
    '  Dim cnn As SqlConnection = Nothing
    '  Dim da As SqlDataAdapter
    '  DsOrdenes = New DataSet

    '  'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
    '  Try
    '    cnn = New SqlConnection(StrTpm) 'ORIGINAL
    '    'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBA
    '    cmd = New SqlCommand("SP_Operacion_Ord_Ped", cnn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value))
    '    'MsgBox(DTPFecha.Value.ToString("dddd"))
    '    cnn.Open()
    '    da = New SqlDataAdapter
    '    da.SelectCommand = cmd
    '    da.SelectCommand.Connection = cnn
    '    da.SelectCommand.CommandTimeout = 10000
    '    cmd.ExecuteNonQuery()
    '    cmd.Connection.Close()
    '    cnn.Close()
    '  Catch ex As Exception
    '    MessageBox.Show("Error al Mostrar los datos de las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Return
    '  End Try
    'End Sub

    'Sub MEjecuta_Orden_Ped_Det()
    '  Try
    '    SQL.EjecutarProcedimientoIUD("SP_Operacion_Ord_Ped_Det", "@fecha", 1, dtpFecha.Value.ToString("yyyy-MM-dd"))
    '  Catch ex As Exception
    '    MessageBox.Show("Error al Insertar el detalle de las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Return
    '  End Try
    'End Sub

    ''METODO QUE INSERTA EN TABLA DETALLE LAS PARTIDAS DE CADA ORDEN
    'Sub MEjecuta_Orden_Ped_DetViejo()
    '  'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
    '  Dim cmd As SqlCommand
    '  Dim cnn As SqlConnection = Nothing
    '  Dim da As SqlDataAdapter
    '  DsOrdenes = New DataSet

    '  'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
    '  Try
    '    cnn = New SqlConnection(StrTpm) 'ORIGINAL
    '    'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBAS
    '    cmd = New SqlCommand("SP_Operacion_Ord_Ped_Det", cnn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value))
    '    'MsgBox(DTPFecha.Value.ToString("dddd"))
    '    cnn.Open()
    '    da = New SqlDataAdapter
    '    da.SelectCommand = cmd
    '    da.SelectCommand.Connection = cnn
    '    da.SelectCommand.CommandTimeout = 10000
    '    cmd.ExecuteNonQuery()
    '    cmd.Connection.Close()
    '    cnn.Close()
    '  Catch ex As Exception
    '    MessageBox.Show("Error al Insertar el detalle de las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Return
    '  End Try
    'End Sub

    'MODIFICADO POR IVAN GONZALEZ
    'NUEVO METODO PARA OPTIMIZAR
    Sub MLlenaOrdenes()
        'SE ELIMINA LA COLUMNA CREADA Y LIMPIA EL DATAGRIDVIEW

        btnUpdate.Visible = False
        lblActualizando.Visible = True
        Me.Refresh()
        NuevaOrden()

        If dgvOrdenes.RowCount <> 0 Then
            dgvOrdenes.Columns().Remove("Imprimir")
            dgvOrdenes.DataSource = Nothing
        End If

        'SE ASIGNA EL RESULTADO DEL PROCEDIMIENTO ALMACENADO AL DATAGRIDVIEW
        Dim tbBandera As New DataTable
        'tbBandera = SQL.EjecutarProcedimiento("TPD_Surtir_Almacen", "@Fecha", 1, dtpFecha.Value.ToString("yyyy-MM-dd"))
        tbBandera = SQL.EjecutarProcedimiento("SP_Full_Surtido", "@fecha", 1, dtpFecha.Value.ToString("yyyy-MM-dd"))
        If tbBandera.Rows.Count <> 0 Then
            dgvOrdenes.DataSource = tbBandera 'SQL.EjecutarProcedimiento("TPD_Surtir_Almacen", "@Fecha", 1, dtpFecha.Value.ToString("yyyy-MM-dd"))
            'VALIDAMOS QUE TENGA DATOS EL DATAGRIDVIEW PARA PODER INSERTAR LAS NUEVAS COLUMNAS
            If dgvOrdenes.Rows.Count <> 0 Then
                dgvOrdenes.Columns().Remove("Accion")
                Dim col As New DataGridViewLinkColumn
                col.DataPropertyName = "Accion"
                col.HeaderText = "Acción"
                col.Name = "Accion"
                col.SortMode = DataGridViewColumnSortMode.Automatic
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                col.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                col.Width = 75
                col.ReadOnly = True
                dgvOrdenes.Columns.Insert(10, col)

                'Hacer la modificación en esta parte para poder imprimir

                'If col.Text = "Surtir" Then

                Dim col1 As New DataGridViewButtonColumn
                col1.DataPropertyName = "Imprimir"
                col1.HeaderText = "Imprimir"
                col1.Name = "Imprimir"
                col1.Text = "Imprimir"
                col1.UseColumnTextForButtonValue = True
                col1.SortMode = DataGridViewColumnSortMode.Automatic
                col1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                col1.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
                col1.Width = 55
                col1.ReadOnly = True
                dgvOrdenes.Columns.Insert(11, col1)


                MEstiloGridOrdenes()

            Else
                dgvOrdenes.DataSource = Nothing
            End If
        End If

        btnUpdate.Visible = True
        lblActualizando.Visible = False
        Me.Refresh()
        NuevaOrden()


    End Sub

    'METODO QUE LLENA EL DATAGRID DE ORDENES DE VENTAS
    'YA NO SE UTILIZARA BORRAR DESPUES
    Sub MLlenaOrdenesOriginal()
        'VARIABLE DE CADENA DE SQL
        Dim SQLOrdenes As String
        'VARIABLE QUE ALMACENA LA FECHA DEL GRID
        Dim Date_grid As Date
        'VARIABLES DE CONEXION DE LLENADO
        Dim cmd As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        Dim DsOrdenes = New DataSet
        'CREA INSTANCIA DE COLUMNA CON IMAGEN Y DA FORMATO
        Dim col1 As New DataGridViewButtonColumn

        'OBTIENE LA FECHA INICIAL Y LA FINAL
        fi = dtpFecha.Value.ToString("yyyy-MM-dd")
        ''REFRESCA EL DATA GRID VIEW DE RESULTADO Y EL DETALLE
        'If dgvDetalle.RowCount > 0 Then
        '    dgvDetalle.Rows.Clear()
        'End If
        If dgvOrdenes.RowCount > 0 Then
            'dgvOrdenes.Rows.Clear()
            dgvOrdenes.Columns().Remove("Imprimir")
            dgvOrdenes.DataSource = Nothing
        End If
        ''ALAMACENA LA CONSULTA
        'SQLOrdenes = "Select DISTINCT(T0.DocNum) As DocNum, FORMAT(T0.DocDate, 'yyyy-MM-dd') AS DocDate, CONVERT(char(8), T0.PrintTime, 108) AS PrintTime, "
        'SQLOrdenes &= "T0.LineNumTotal AS LineNumTotal, T0.TrnspName, T0.TrnspCode AS TrnspCode, T0.CardCode, T0.CardName, T0.Comment,  IIF(t4.Name Is null,'-',t4.Name) as Surtidor,"
        'SQLOrdenes &= "CASE WHEN T0.Status = 'S' AND T0.LastStatus = 'Y' AND T2.Canceled <> 'Y' THEN T1.StatusName2 "
        'SQLOrdenes &= " WHEN T0.Status = 'S' AND T0.LastStatus = 'E' AND T2.Canceled <> 'Y' THEN 'En Espera' "
        'SQLOrdenes &= " WHEN T0.Status = 'P' AND T0.LastStatus = 'E' AND T2.CANCELED <> 'Y' THEN T1.StatusName2  "
        'SQLOrdenes &= " WHEN T0.Status = 'A' AND T0.LastStatus = 'Y' AND T2.CANCELED <> 'Y' THEN 'Surtir' "
        'SQLOrdenes &= "WHEN T0.Status = 'A' AND T0.LastStatus = 'E' AND T2.CANCELED <> 'Y' THEN 'En Espera' "
        'SQLOrdenes &= " WHEN T2.CANCELED = 'Y' THEN 'Cancelado' "
        'SQLOrdenes &= "ELSE 'No establecido' END AS Accion, "
        'SQLOrdenes &= "'' AS Printed "
        'SQLOrdenes &= "FROM " + BaseDatosTPD + ".dbo.Operacion_Orden T0 INNER JOIN " + BaseDatosTPD + ".dbo.Operacion_Status T1 ON T0.Status = T1.Status "
        'SQLOrdenes &= "INNER JOIN " + BaseDatosSAP + ".dbo.ORDR T2 ON T2.DocEntry = T0.DocEntry "
        'SQLOrdenes &= "LEFT JOIN  SBO_TPD.dbo.RDR1 T3 ON T2.DocEntry=T3.DocEntry "
        'SQLOrdenes &= "  LEFT JOIN TPM.dbo.Operacion_Empleado	T4 ON T0.UserId_Surtido = T4.KeyCode "
        'SQLOrdenes &= "WHERE T0.DocDate BETWEEN DATEADD(MONTH, -2, '" + fi + "') AND '" + fi + "' AND (T0.Status <> 'ST' AND T0.Status <> 'N') "
        'SQLOrdenes &= " AND (select  Count(TrgetEntry) From SBO_TPD.dbo.RDR1 where DocEntry=T2.DocEntry) = 0    "
        'SQLOrdenes &= "AND T2.DocStatus = 'O'  "
        ''MOSTRAR CANCELADOS SOLO POR 2 DIAS 
        'SQLOrdenes &= "AND (T2.CANCELED <> 'Y' OR T2.CANCELED = 'Y' AND T0.DocDate>DATEADD(DAY, -2, '" + fi + "' )) "
        'SQLOrdenes &= "ORDER BY T0.DocNum DESC "

        'Modificado por Ivan Gonzalez se cambio a un procedimiento almacenado "TPD_Surtir_Almacen" por si surgen errores en la consulta
        Try
            'RELAIZA LA CONEXION
            cnn = New SqlConnection(StrTpm) 'ORIGINAL
            'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBAS
            'ALMACENA LA CONSULTA EN UN COMMAND SQL
            cmd = New SqlCommand("TPD_Surtir_Almacen", cnn)
            'CONVIERTE EL TEXTO EN CONSULTA
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Fecha", dtpFecha.Value.ToString("yyyy-MM-dd"))
            'APERTURA LA CONEXION
            cnn.Open()
            'INSTANCIA UN ADAPTER
            da = New SqlDataAdapter
            'ALMACENA EL COMMAND DE SQL EN EL ADAPTER
            da.SelectCommand = cmd
            'LO EJECUTA CON LA CONEXION
            da.SelectCommand.Connection = cnn
            'TIEMPO DE ESPERA DE LA CONEXION
            da.SelectCommand.CommandTimeout = 10000
            'EJECUTA LA CONSULTA
            cmd.ExecuteNonQuery()
            'CIERRA EL COMMAND DE SQL
            cmd.Connection.Close()
            'CIERRA LA CONEXION
            cnn.Close()
            'LLENA EL ADAPTER A UN DATA SET
            da.Fill(DsOrdenes)
            'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
            Resultado = New DataView
            'ALMACENA EN DATA SET DE MODO TABLA
            Resultado.Table = DsOrdenes.Tables(0)

            'VALIDA SI EL DATATABLE TRAE DATOS
            If (Resultado.Count > 0) Then

                'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
                dgvOrdenes.DataSource = Nothing
                'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
                dgvOrdenes.DataSource = Resultado

                'ELIMINA LA COLUMNA ACTION DEL DATA GRID TRAIDA DEL DATATABLE
                dgvOrdenes.Columns().Remove("Accion")
                'CREA INSTANCIA DE COLUMNA CON LINK Y DA FORMATO EN ACCION
                Dim col As New DataGridViewLinkColumn
                col.DataPropertyName = "Accion" 'COLOCA LA PROPIEDAD
                col.HeaderText = "Acción" 'COLOCA EL ENCABEZADO
                col.Name = "Accion" 'COLOCA EL NOMBRE DE LA COLUMNA
                col.SortMode = DataGridViewColumnSortMode.Automatic 'DEFINE EL ORDENADO DE LA COLUMNA
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 'CENTRA EL CONTENIDO
                col.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular) 'FORMATO DE LETRA
                col.Width = 75 'ANCHO DE LA CELDA
                col.ReadOnly = True 'QUE SOLO SEA DE LECTURA
                'COLOCA LA COLUMNA DE DE ACCION DE MANERA LINK, EL 7 ES LA POSICIÓN EN DONDE APARECE EN EL DATA GRID, DE 
                'RECORRERSE O AGREGAR UN CAMPO MAS, ESTE NUMERO DEBERA INCREMETARSE O DISMINUIR SEGUN EL CASO.
                dgvOrdenes.Columns.Insert(10, col)

                '----
                'ELIMINA LA COLUMNA IMPRIMIR DEL DATA GRID TRAIDA DEL DATATABLE
                'dgvOrdenes.Columns().Remove("Printed")

                col1.DataPropertyName = "Imprimir" 'COLOCA LA PROPIEDAD
                col1.HeaderText = "Imprimir" 'COLOCA EL ENCABEZADO
                col1.Name = "Imprimir" 'COLOCA EL NOMBRE DE LA COLUMNA
                col1.Text = "Imprimir" 'COLOCA EL TEXTO EN EL BOTON
                col1.UseColumnTextForButtonValue = True 'PERMITE QUE SE VISUALICE EL TEXTO EN EL BOTON
                col1.SortMode = DataGridViewColumnSortMode.Automatic
                col1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 'CENTRA EL CONTENIDO
                col1.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular) 'FORMATO DE LETRA
                col1.Width = 55 'ANCHO DE LA CELDA
                col1.ReadOnly = True 'QUE SOLO SEA DE LECTURA
                'COLOCA LA COLUMNA DE IMPRESION DE MANERA BOTON, EL 9 ES LA POSICIÓN EN DONDE APARECE EN EL DATA GRID, DE 
                'RECORRERSE O AGREGAR UN CAMPO MAS, ESTE NUMERO DEBERA INCREMETARSE O DISMINUIR SEGUN EL CASO.
                dgvOrdenes.Columns.Insert(11, col1)

                MEstiloGridOrdenes()
            Else
                'Limpiar el detalle de la orden
                If dgvOrdenes.Rows.Count = 0 Then
                    dgvDetalle.Rows.Clear()
                End If
            End If
            'FIN VALIDA SI EL DATATABLE TRAE DATOS

            '-----

        Catch ex As Exception
            MsgBox("Error en el llenado de las Ordenes: " + ex.ToString, MsgBoxStyle.Exclamation, "Error en Conexión o llenado del Grid")
            Return
        End Try

        '-----

        'RECORRE EL GRID DE ORDENES PARA REEMPLAZAR LOS HORARIOS DE PAQUETERIA
        For Each row As DataGridViewRow In dgvOrdenes.Rows
            'CONVIERTE EL DATO DE FECHA DEL GRID A UNA FECHA REAL
            Date_grid = Convert.ToDateTime(row.Cells("DocDate").Value)
            'OBTIENE SOLO EL DIA DE LA FECHA
            Dia = Date_grid.ToString("dddd")
            'VALIDA SI EL DIA ES SABADO
            If Dia = "sábado" Then
                'CAMBIA EL NOMBRE DE LA PAQUETERIA CORRESPONDIENTE A LOS VALORES ESTABLECIDOS
                If row.Cells("TrnspCode").Value.ToString = "43" Or row.Cells("TrnspCode").Value.ToString = "44" Then 'PAQUETERIA LOGEX'
                    row.Cells("TrnspCode").Value = "13:00"
                ElseIf row.Cells("TrnspCode").Value.ToString = "9" Or row.Cells("TrnspCode").Value.ToString = "10" Then 'PAQUETERIA ESTAFETA'
                    row.Cells("TrnspCode").Value = "13:20 - 14:00"
                ElseIf row.Cells("TrnspCode").Value.ToString = "20" Or row.Cells("TrnspCode").Value.ToString = "21" Then 'PAQUETERIA PAQUETE EXPRESS'
                    row.Cells("TrnspCode").Value = "12:30 - 13:30"
                ElseIf row.Cells("TrnspCode").Value.ToString = "28" Or row.Cells("TrnspCode").Value.ToString = "29" Then 'PAQUETERIA TRES GUERRAS
                    row.Cells("TrnspCode").Value = "11:15 - 11:30"
                Else
                    If row.Cells("TrnspCode").Value.ToString <> "" Then
                        row.Cells("TrnspCode").Value = "11:15 - 11:30"
                    End If
                End If
            Else
                'DE NO SER SBADO ENTRA PARA HORARIOS DE ENTRE SEMANA
                If row.Cells("TrnspCode").Value.ToString = "43" Or row.Cells("TrnspCode").Value.ToString = "44" Then 'PAQUETERIA LOGEX'
                    row.Cells("TrnspCode").Value = "18:00"
                ElseIf row.Cells("TrnspCode").Value.ToString = "9" Or row.Cells("TrnspCode").Value.ToString = "10" Then 'PAQUETERIA ESTAFETA'
                    row.Cells("TrnspCode").Value = "18:20 - 19:00"
                ElseIf row.Cells("TrnspCode").Value.ToString = "20" Or row.Cells("TrnspCode").Value.ToString = "21" Then 'PAQUETERIA PAQUETE EXPRESS'
                    row.Cells("TrnspCode").Value = "17:30 - 18:30"
                ElseIf row.Cells("TrnspCode").Value.ToString = "28" Or row.Cells("TrnspCode").Value.ToString = "29" Then 'PAQUETERIA TRES GUERRAS'
                    row.Cells("TrnspCode").Value = "15:00"
                Else
                    If row.Cells("TrnspCode").Value.ToString <> "" Then
                        row.Cells("TrnspCode").Value = "16:15 - 16:30"
                    End If
                End If
            End If 'FIN VALIDA SI EL DIA ES SABADO
        Next
        'FIN RECORRE EL GRID DE ORDENES PARA REEMPLAZAR LOS HORARIOS DE PAQUETERIA

    End Sub



    'MÉTODO QUE LLENA EL DATAGRID DE DETALLE DE ORDENES DE VENTAS
    Sub MLlenaOrdenesDetalle(ByVal DocNum As Integer)
        Try 'CAPTURA EL ERROR DE CONSULTA O CONEXION DE LA BASE DE DATOS DEL TPD
            ''CONECTA A LA BASE DE DATOS
            'conexion_universal.conectar()
            ''ALAMACENA LA CONSULTA
            'SQLDetalle = "SELECT Distinct T0.LineNum, T0.ItemCode, T0.Description, T0.Quantity, T0.Surtido "
            'SQLDetalle &= "FROM " + BaseDatosTPD + ".dbo.Operacion_Detalle T0 LEFT JOIN " + BaseDatosTPD + ".dbo.Operacion_Orden T1 ON T0.DocNum = T1.DocNum "
            'SQLDetalle &= "WHERE T1.DocNum = " + DocNum.ToString + " "
            ''CAMBIAR LINEA AQUI
            'SQLDetalle &= "AND T0.ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS NOT IN(select ItemCode from " + BaseDatosSAP + ".dbo.OITM WHERE ItmsGrpCod = 150) "
            ''SQLDetalle &= "AND T0.ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS NOT IN(select ItemCode from ZPRUEBAS31OCT18.dbo.OITM WHERE ItmsGrpCod = 150) "
            ''SQLDetalle &= "ORDER BY T0.DocNum DESC "

            ''CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            'conexion_universal.slq_s = New SqlCommand(SQLDetalle, conexion_universal.conexion_uni)
            ''EJECUTA LA CONSULTA
            'conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            ''RECORRE LA CONSULTA
            'While conexion_universal.rd_s.Read
            '    Line = Line + 1
            '    If dgvDetalle.RowCount > 0 Then
            '        'MANDA LOS RESULTADOS
            '        Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            '            'rd_s.Item("LineNum")
            '            Me.dgvDetalle.Rows.Add(Line, rd_s.Item("ItemCode").ToString, rd_s.Item("Description"), CInt(rd_s.Item("Quantity")).ToString,
            '            CInt(rd_s.Item("Surtido")).ToString)
            '            'RECORRE EL DATA GRID VIEW
            '            With dgvDetalle
            '                'ESTABLECE LA CELDA ACTUAL
            '                .CurrentCell = .Rows(Me.dgvDetalle.Rows.Count - 1).Cells(0)
            '            End With
            '        Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            '            'MANDA EL MENSAJE DE ERROR
            '            MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
            '            Return
            '        End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            '    Else
            '        'MANDA LOS RESULTADOS
            '        Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            '            Me.dgvDetalle.Rows.Add(Line, rd_s.Item("ItemCode").ToString, rd_s.Item("Description"), CInt(rd_s.Item("Quantity")).ToString,
            '            CInt(rd_s.Item("Surtido")).ToString)
            '            'RECORRE EL DATA GRID VIEW
            '            With dgvDetalle
            '                'ESTABLECE LA CELDA ACTUAL
            '                .CurrentCell = .Rows(Me.dgvDetalle.Rows.Count - 1).Cells(0)
            '            End With
            '        Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            '            'MANDA EL MENSAJE DE ERROR
            '            MsgBox("Error al agregar el resultado en Ordenes : " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
            '            Return
            '        End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            '    End If
            'End While
            'conexion_universal.cerrar_conectar()

            ''MODIFICADO POR IVAN GONZALEZ
            'SE OPTIMOZO LA CONSULTA CON UN PROCEDIMIENTO ALMACENADO
            Dim SQL As New Comandos_SQL()
            If DocNum_P <> DocNum Then
                dgvDetalle.DataSource = SQL.EjecutarProcedimiento("TPD_Detalle_Surtido", "@DocNum", 1, DocNum)
                DocNum_P = DocNum
                'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES
                MEstiloGridDetalle()
            Else

            End If

        Catch ex As Exception
            MsgBox("Error de consulta o conexión TPD en llenado de GRID de Ordenes: " & ex.Message, MsgBoxStyle.Critical)
            conexion_universal.cerrar_conectar()
            Return
        End Try 'FIN CAPTURA EL ERROR
    End Sub



    'METODO BUSCAR ORDENES DE VENTA
    Sub MBuscarOrdenes()
        Try
            'FILTRA EL RESUTADO DEL DATA VIEW
            Resultado.RowFilter = "DocNum like '%" & txtBuscarOrden.Text & "%'"
        Catch exc As Exception
            MsgBox("Error en busqueda de la orden: " + exc.ToString)
            Return
        End Try
    End Sub

    'METODO IMPRIMIR NUEVO FORMATO
    Sub ImprimeNuevoFormato()
        Dim Pedido As String = dgvOrdenes.CurrentRow.Cells("DocNum").Value
        Dim Surtidor As String = dgvOrdenes.CurrentRow.Cells("Surtidor").Value
        Try

            Dim informe As New PedidoArtAlmacen
            RepComsultaP.MdiParent = Inicio

            'CONEXION A LA BASE DE DATOS
            Dim tInfo As TableLogOnInfo = New TableLogOnInfo()
            Dim ConnectionInfo As ConnectionInfo = tInfo.ConnectionInfo

            ConnectionInfo.Password = cPassword
            ConnectionInfo.UserID = conexion_universal.cUserID
            ConnectionInfo.ServerName = conexion_universal.cServerName ' // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS
            ConnectionInfo.DatabaseName = conexion_universal.cDatabaseNameSAP

            informe.SetParameterValue("DocKey@", Pedido)
            informe.SetParameterValue("ObjectId@", 17)
            informe.SetParameterValue("surtio", Surtidor)

            RepComsultaP.CrVConsulta.ReportSource = informe

            SetTableLocation(informe, ConnectionInfo)
            informe.PrintOptions.PaperOrientation = PaperOrientation.Landscape
            informe.PrintOptions.PaperSize = PaperSize.PaperA4


            'Return

            informe.PrintToPrinter(1, False, 0, 0)


        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error al imprimir PDF surtir")
        End Try

        'ACTUALIZA TOTAL DE IMPRESIONES
        'VARIABLE ALMACENA TOTAL DE IMPRESIONES
        Dim TotalPrint As Integer
        'VARIABLE DE CONSULTA
        Dim SQLUpdateImp
        'CAPTURA EL ERROR DE CONSULTA O CONEXION
        Try
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar()

            'ALMACENA LA CONSULTA DE BUSQUEDA
            SQLUpdateImp = "SELECT IIF(Printed IS NULL, 0, Printed) AS Printed FROM " + BaseDatosTPD + ".dbo.Operacion_Orden WHERE DocNum = '" + Pedido + "' "

            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLUpdateImp, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If (conexion_universal.rd_s.Read) Then
                TotalPrint = CInt(rd_s.Item("Printed").ToString) + 1
            End If
            conexion_universal.rd_s.Close() 'CIERRA EL READE
            conexion_universal.conexion_uni.Close() 'CIERRA LA CONEXION

            'INICIALIZA LA VARIABLE EN NADA
            SQLUpdateImp = ""

            'APERTURA DE LA CONEXION
            conexion_universal.conexion_uni.Open()

            'ALMACENA LA CONSULTA PARA LA ACTUALIZACIÓN
            SQLUpdateImp = "UPDATE " + BaseDatosTPD + ".dbo.Operacion_Orden SET Printed = '" + TotalPrint.ToString + "' "
            SQLUpdateImp &= "WHERE DocNum = '" + Pedido + "' "

            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLUpdateImp, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar

            conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION

            '-----
        Catch ex As Exception
            MsgBox("Error de Consulta o Conexion al Actualziar las Impresiones: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
            conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
            Return
        End Try
    End Sub


    'METODO IMPRIMIR ORDEN
    Sub MImprimeFormato()
        'VARIABLE QUE ALMACENA EL DOCUMENTO PARA OBTENER EL NUMERO DE PEDIDO
        Dim Pedido As String = dgvOrdenes.CurrentRow.Cells("DocNum").Value
        Dim Surtidor As String = dgvOrdenes.CurrentRow.Cells("Surtidor").Value
        'RUTA DONDE SE GUARDA EL ARCHIVO PDF DE LA ORDEN
        _rutaPDF = "\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\OrdenPDF\" + Pedido + ".pdf"

        'CREA EL PDF DE LA REFACTURA FACTURA

        '//PARAMETROS DE CONEXION PARA EL RPT
        Dim tInfo As TableLogOnInfo = New TableLogOnInfo()
        Dim ConnectionInfo As ConnectionInfo = tInfo.ConnectionInfo

        ConnectionInfo.Password = conexion_universal.cPassword
        ConnectionInfo.UserID = conexion_universal.cUserID
        ConnectionInfo.ServerName = conexion_universal.cServerName ' // SERVERSQLINSTANCE -> 10.0.0.1SQLEXPRESS
        ConnectionInfo.DatabaseName = conexion_universal.cDatabaseNameSAP
        'OBTIENE LA RUTA DEL ARCHIVO DE CRYSTAL PARA CARGARLO (PEDIDO ALMACEN)
        DocOrdenes.Load("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\PedidoArtAlmacen.rpt") ' //RUTA DEL ARCHIVO .rpt


        '//PASA EL PARAMETRO AL ARCHIVO RPT (DocEntry)(ObjectType)
        DocOrdenes.SetParameterValue("DocKey@", Pedido)
        DocOrdenes.SetParameterValue("ObjectId@", 17)
        DocOrdenes.SetParameterValue("surtio", Surtidor)


        'ESTABLECE LA CONEXION CON EL REPORTE
        SetTableLocation(DocOrdenes, ConnectionInfo)

        '//GENERA PDF EN CARPETA TEMPORAL
        Try
            '//ALMACENA EL PDF EN LA RUTA TEMPORAL
            DocOrdenes.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, _rutaPDF)
            'INDICA LA ORIENTACIÓN DE LA HOJA IMPRESA
            DocOrdenes.PrintOptions.PaperOrientation = PaperOrientation.Landscape
            'INDICA EL TAMAÑO DE LA HOJA
            DocOrdenes.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
            'INDICA LA PREFERENCIA DE LA IMPRESORA
            DocOrdenes.PrintToPrinter(1, False, 0, 0)
        Catch ex As Exception
            'VALIDA SI EL USUARIO ES MANAGER QUE PERMITA VER EL ERROR QUE PRESENTA
            If (UsrTPM = "MANAGER") Then
                MessageBox.Show("No se pudo crear el archivo PDF de la Orden de Venta: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            Else
                MessageBox.Show("Documento sin imprimir. Error: " + ex.ToString(), "Alerta de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
        End Try

        '//CIERRA EL DOCUEMTNO DE RPT
        DocOrdenes.Close()

        '------

        'ACTUALIZA TOTAL DE IMPRESIONES
        'VARIABLE ALMACENA TOTAL DE IMPRESIONES
        Dim TotalPrint As Integer
        'VARIABLE DE CONSULTA
        Dim SQLUpdateImp
        'CAPTURA EL ERROR DE CONSULTA O CONEXION
        Try
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar()

            'ALMACENA LA CONSULTA DE BUSQUEDA
            SQLUpdateImp = "SELECT IIF(Printed IS NULL, 0, Printed) AS Printed FROM " + BaseDatosTPD + ".dbo.Operacion_Orden WHERE DocNum = '" + Pedido + "' "

            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLUpdateImp, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If (conexion_universal.rd_s.Read) Then
                TotalPrint = CInt(rd_s.Item("Printed").ToString) + 1
            End If
            conexion_universal.rd_s.Close() 'CIERRA EL READE
            conexion_universal.conexion_uni.Close() 'CIERRA LA CONEXION

            'INICIALIZA LA VARIABLE EN NADA
            SQLUpdateImp = ""

            'APERTURA DE LA CONEXION
            conexion_universal.conexion_uni.Open()

            'ALMACENA LA CONSULTA PARA LA ACTUALIZACIÓN
            SQLUpdateImp = "UPDATE " + BaseDatosTPD + ".dbo.Operacion_Orden SET Printed = '" + TotalPrint.ToString + "' "
            SQLUpdateImp &= "WHERE DocNum = '" + Pedido + "' "

            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLUpdateImp, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar

            conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION

            '-----

        Catch ex As Exception
            MsgBox("Error de Consulta o Conexion al Actualziar las Impresiones: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
            conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
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

#End Region
    '----- FIN METODOS

    '----- BOTONES
#Region "Botones"

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'MEjecuta_Orden()
        'MEjecuta_Orden_Ped()
        'MEjecuta_Orden_Ped_Det()
        MLlenaOrdenes()

        DocNum_P = 0
        dgvDetalle.DataSource = Nothing
        NuevaOrden()

    End Sub

#End Region
    '----- FIN BOTONES

    '----- EVENTOS
#Region "Eventos"
    'OBTIENE EL VALOR DE UNA CELADA AL DARLE CLICK EN EL DATA GRID
    Private Sub dgvOrdenes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrdenes.CellClick
        'OBTIENE EL VALOR DE LA CELDA DE DOCNUM QUE ES LA ORDEN DE PEDIDO
        DocNum = dgvOrdenes.CurrentRow.Cells("DocNum").Value
        Dim Num As Integer = CInt(Convert.ToString(dgvOrdenes.CurrentRow.Cells("DocNum").Value))
        'MANDA A LLAMAR EL METODO DE LLENADO DEL DATA GRID DETALLE PARA MOSTRAR LAS PARTIDAS DE LA ORDEN SELECCIONADA.
        MLlenaOrdenesDetalle(Num)
        'VALIDA SI POR LO MENOS SE ACTIVA UNA CELDA AL DAR CLICK, DE ESTAR VACIO EL DATA GRID NO HACENADA
        If e.RowIndex >= 0 Then
            '-----
            'VALIDA QUE LA CELDA QUE SE ACTIVA DEBE DE SER LA DE ACCIÓN
            If Me.dgvOrdenes.Columns(e.ColumnIndex).Name = "Accion" Then
                '-----
                'VALIDA SI FUE UN CLICK EN CUALQUIER CELDA DE ACCION, Y VALIDA QUE ESTATUS SE TOMARIA
                If (dgvOrdenes.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Surtir") Then

                    Dim confirma As Integer
                    'MANDA LA CONFIRMACIÓN SI REALMENTE REQUIEREN SURTIR LA ORDEN
                    confirma = MessageBox.Show("Realmente desea comenzar surtido de la orden [ " + dgvOrdenes.CurrentRow.Cells("DocNum").Value + " ], Desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    'SI SE ELIJE QUE SI, ENTRA EN LA PRIMERA OPCIÓN.
                    If confirma = 6 Then
                        'CONCATENA EL TITULO DEL FORMULARIO DE LA PANTALLA DE ACCESO
                        TituloAcceso = "Acceso a" + " " + "Surtir" + " " + "orden" + " [" + dgvOrdenes.CurrentRow.Cells("DocNum").Value + " ]"
                        'ALMACENA LA ORDEN DE VENTA
                        DocNumAcceso = dgvOrdenes.CurrentRow.Cells("DocNum").Value.ToString
                        'ALMACENA EL STATUS DE LA ORDEN
                        StatusAcceso = dgvOrdenes.CurrentRow.Cells("Accion").Value.ToString

                        Using frmA As New frmAcceso()
                            'MANDA MOSTRAR EL FORMULARIO DE ACCESO
                            Dim dr As DialogResult = frmA.ShowDialog(Me)
                            'SI SE PRESIONA EL BOTON CERRAR DEL FORMULARIO DE ACCESO, NO SE HACE NINGUNA FUNCION
                            If (dr = Windows.Forms.DialogResult.Cancel) Then
                                'VALIDA SI SE CERRO POR CANCELAR O POR CAMBIO DE FORMULARIO DE SURTIDO
                                If CierraDialogAcceso = False Then
                                    'MANDA MENSAJE DE QUE NO SE VA SURTIR LA ORDEN DE VENTA.
                                    'MsgBox("Acceso cancelado, la Orden de Venta no comenzará a surtirse.", MsgBoxStyle.Exclamation, "Acceso cancelado")
                                    'COLOCA EN NADA LA VARIABLE GLOBAL DE ACCESO A SURTIR
                                    TituloAcceso = ""
                                    DocNumAcceso = ""
                                    StatusAcceso = ""
                                    Return
                                Else 'SI EL CIERRE ES VERDADERO SE MANDA A LLAMAR LA EJECUCION DE EL REFRES
                                    'MANDA A LLAMAR EL MÉTODO DE INSERTAR LAS ORDENES DE VENTA DEL DÍA
                                    'MEjecuta_Orden()
                                    'MANDA A LLAMAR EL MÉTODO DE MOSTRAR LAS ORDENES DE VENTA DEL DIA
                                    'MEjecuta_Orden_Ped()
                                    'MANADA A LLAMAR EL MÉTODO DE INSERTAR EL DETALLE DE LAS ORDENE DEL DÍA
                                    'MEjecuta_Orden_Ped_Det()
                                    'MANDA A LLAMAR EL MÉTODO DE MOSTRAR EN EL GRID LAS ORDENES DE VENTA DEL DIA
                                    MLlenaOrdenes()
                                End If
                            End If
                        End Using
                    Else 'EN CASO DE QUE DIGA QUE NO
                        'MsgBox("Dijo que NO")
                        Return
                    End If
                ElseIf (dgvOrdenes.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Surtiendo") Then
                    'ALMACENA EL STATUS
                    StatusSurtido = "Surtiendo"
                    'ALMACENA EL DOCUMENTO DE LA ORDEN PARA PASARLO AL NUEVO FORMULARIO
                    DocNumSurtido = dgvOrdenes.CurrentRow.Cells("DocNum").Value.ToString
                    'ALMACENA EL TITULO DEL FORMULARIO DE DETALLE DE SURTIDO
                    TituloSurtido = StatusSurtido + " Orden de Venta | " + DocNumSurtido
                    'MANDA A LLAMAR EL FORMULARIO DE SURTIDO
                    frmDetalleSurtir.MdiParent = Inicio
                    frmDetalleSurtir.Show()

                    'INICIALIZA LAS VARIABLE DE TITULO DE DETALLA SURTIDO
                    TituloAcceso = ""
                    StatusAcceso = ""
                    DocNumAcceso = ""

                End If 'FIN VALIDA QUE LA CELDA QUE SE ACTIVA DEBE DE SER LA DE ACCIÓN
                '----
            ElseIf (Me.dgvOrdenes.Columns(e.ColumnIndex).Name = "Imprimir") Then
                'MANDA A LLAMAR EL METODO DE IMPRIMIR
                'MImprimeFormato()
                If (dgvOrdenes.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Surtir") Then
                    MessageBox.Show("Inicie el surtido para poder imprimir la Orden de Venta")
                ElseIf (dgvOrdenes.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Surtiendo") Then

                    ImprimeNuevoFormato()
                End If

            End If
            '-----

        End If
    End Sub

    'INICIO EVENTO DE TIMER
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'MEjecuta_Orden()
        'SQL.Cerrar()
        'MEjecuta_Orden_Ped()
        'SQL.Cerrar()
        'MEjecuta_Orden_Ped_Det()
        'SQL.Cerrar()
        MLlenaOrdenes()
        SQL.Cerrar()

        DocNum_P = 0
        dgvDetalle.DataSource = Nothing


        'thread = New Thread(AddressOf MyBackgroundThread)
        'thread.Start()
    End Sub


    'BUSCA LA ORDEN QUE SE REQUIERA
    Private Sub txtBuscarOrden_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarOrden.TextChanged
        'MANDA A LLLAMAR EL METODO DE BUSQUEDA DE ORDEN
        MBuscarOrdenes()
    End Sub


    'DETECTA QUE TECLA SE PRESIONA DENTRO DEL GRID PARA PODER HACER UNA ACCIÓN QUITANDO EL SALTO DE LINEA SI SE PRECIONO ENTER
    Private Sub dgvOrdenes_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvOrdenes.KeyDown
        'VALIDA QUE LA TECLA PRESIONADA SEA ENTER
        If (e.KeyCode = Keys.Enter) Then
            ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
            e.SuppressKeyPress = True
            'VALIDA QUE SEA LA ACCION DE SURTIENDO PARA MOSTRAR LA PANTALLA
            If (dgvOrdenes.CurrentRow.Cells("Accion").Value.ToString = "Surtiendo") Then
                'ALMACENA EL STATUS
                StatusSurtido = "Surtiendo"
                'ALMACENA EL DOCUMENTO DE LA ORDEN PARA PASARLO AL NUEVO FORMULARIO
                DocNumSurtido = dgvOrdenes.CurrentRow.Cells("DocNum").Value.ToString
                'ALMACENA EL TITULO DEL FORMULARIO DE DETALLE DE SURTIDO
                TituloSurtido = StatusSurtido + " Orden de Venta | " + DocNumSurtido
                'MANDA A LLAMAR EL FORMULARIO DE SURTIDO

                'PARAR EL TIMER PARA QUE NO SE TRABE EL SISTEMA
                Timer1.Enabled = False


                frmDetalleSurtir.MdiParent = Inicio
                frmDetalleSurtir.Show()

                'INICIALIZA LAS VARIABLE DE TITULO DE DETALLA SURTIDO
                TituloAcceso = ""
                StatusAcceso = ""
                DocNumAcceso = ""



            ElseIf (dgvOrdenes.CurrentRow.Cells("Accion").Value.ToString = "Surtir") Then 'VALIDA QUE SEA LA ACCION DE SURTIR PARA MOSTRAR ACCESO
                Dim confirma As Integer
                'MANDA LA CONFIRMACIÓN SI REALMENTE REQUIEREN SURTIR LA ORDEN
                confirma = MessageBox.Show("Realmente desea comenzar surtido de la orden [ " + dgvOrdenes.CurrentRow.Cells("DocNum").Value + " ], Desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                'SI SE ELIJE QUE SI, ENTRA EN LA PRIMERA OPCIÓN.
                If confirma = 6 Then
                    'CONCATENA EL TITULO DEL FORMULARIO DE LA PANTALLA DE ACCESO
                    TituloAcceso = "Acceso a" + " " + "Surtir" + " " + "orden" + " [" + dgvOrdenes.CurrentRow.Cells("DocNum").Value + " ]"
                    'ALMACENA LA ORDEN DE VENTA
                    DocNumAcceso = dgvOrdenes.CurrentRow.Cells("DocNum").Value.ToString
                    'ALMACENA EL STATUS DE LA ORDEN
                    StatusAcceso = dgvOrdenes.CurrentRow.Cells("Accion").Value.ToString
                    'CARGA FORMULARIO

                    'SE PARA EL TIMER PARRA QUE NO SE TRABE AL INGRESAR LAS CLAVES DE LOS EMPACADORES
                    Timer1.Enabled = False


                    Using frmA As New frmAcceso()
                        'MANDA MOSTRAR EL FORMULARIO DE ACCESO
                        Dim dr As DialogResult = frmA.ShowDialog(Me)

                        'SI SE PRESIONA EL BOTON CERRAR DEL FORMULARIO DE ACCESO, NO SE HACE NINGUNA FUNCION
                        If (dr = Windows.Forms.DialogResult.Cancel) Then
                            'VALIDA SI SE CERRO POR CANCELAR O POR CAMBIO DE FORMULARIO DE SURTIDO
                            If CierraDialogAcceso = False Then
                                'MANDA MENSAJE DE QUE NO SE VA SURTIR LA ORDEN DE VENTA.
                                'MsgBox("Acceso cancelado, la Orden de Venta no comenzará a surtirse.", MsgBoxStyle.Exclamation, "Acceso cancelado")
                                'COLOCA EN NADA LA VARIABLE GLOBAL DE ACCESO A SURTIR
                                TituloAcceso = ""
                                DocNumAcceso = ""
                                StatusAcceso = ""
                                'Timer1.Enabled = True
                                Return
                            Else 'SI EL CIERRE ES VERDADERO SE MANDA A LLAMAR LA EJECUCION DE EL REFRES

                                'MODIFICAR SI SE TRABA
                                'MEjecuta_Orden()
                                'MEjecuta_Orden_Ped()
                                'MEjecuta_Orden_Ped_Det()
                                'Timer1.Enabled = True

                                MLlenaOrdenes()
                                DocNum_P = 0
                                dgvDetalle.DataSource = Nothing

                                'DocNum_P = 0
                                'dgvDetalle.DataSource = Nothing
                            End If
                        End If
                    End Using 'FIN CARGA FORMULARIO
                End If 'FIN VALIDA SI PRESIONA SI O NO
            End If 'FIN VALIDA ACCIONES
        Else
            'SI ES OTRA TECLA QUE NO SEA EL ENTER PERMITE EL SALTO DE LA LINEA
            e.SuppressKeyPress = False
        End If
    End Sub


    Private Sub dgvDetalle_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub dgvOrdenes_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvOrdenes.RowPostPaint
        Try
            If dgvOrdenes.Rows.Count <> 0 Then
                amarillo = ColorTranslator.FromHtml("#FDFF6C")
                For i As Integer = 0 To dgvOrdenes.Rows.Count - 1
                    If dgvOrdenes.Rows(i).Cells("Accion").Value.ToString = "Surtir" Then 'SE DEJA POR SI SE OCUPA

                    ElseIf dgvOrdenes.Rows(i).Cells("Accion").Value.ToString = "Surtiendo" Then
                        dgvOrdenes.Rows(i).DefaultCellStyle.BackColor = Color.Khaki
                        dgvOrdenes.Rows(i).DefaultCellStyle.ForeColor = Color.Black

                    ElseIf dgvOrdenes.Rows(i).Cells("Accion").Value.ToString = "En Espera" Or dgvOrdenes.Rows(i).Cells("Accion").Value.ToString = "Esperando articulo" Then
                        dgvOrdenes.Rows(i).DefaultCellStyle.BackColor = amarillo
                        dgvOrdenes.Rows(i).DefaultCellStyle.ForeColor = Color.Black
                    ElseIf dgvOrdenes.Rows(i).Cells("Accion").Value.ToString = "Cancelado" Then
                        dgvOrdenes.Rows(i).DefaultCellStyle.BackColor = Color.LightCoral
                        dgvOrdenes.Rows(i).DefaultCellStyle.ForeColor = Color.Black
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvOrdenes_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrdenes.CellEnter
        DocNum = dgvOrdenes.CurrentRow.Cells("DocNum").Value
        Dim Num As Integer = CInt(Convert.ToString(dgvOrdenes.CurrentRow.Cells("DocNum").Value))
        MLlenaOrdenesDetalle(Num)
    End Sub

    Private Sub frmMostrarOrdenes_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        MLlenaOrdenes()
        Timer1.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        My.Computer.Audio.Play(My.Resources.alarma_humo, AudioPlayMode.Background)
    End Sub

    Private Sub btn_PreClaves_Click(sender As Object, e As EventArgs) Handles btn_PreClaves.Click
        Try
            Dim SQLUpdatepreClave As String = ""
            Dim Cve As String = InputBox("Ingresa la Clave", "Administrador de preclaves", "")
            Dim Autorizado As Boolean = False

            'Verifico que este autorizado
            Try
                SQL.conectarTPM()

                Dim con As New SqlConnection(StrTpm)
                Dim cmd As New SqlCommand()
                Dim strSelect As String = ""
                Dim dReader As SqlDataReader

                strSelect = "SELECT Count(*) as Correcto FROM Operacion_Empleado WHERE KeyCode = '" & Cve & "' AND Permiso_para_cambiarPreclaves = 1"
                cmd.Connection = con
                cmd.CommandText = strSelect
                con.Open()
                cmd.CommandType = CommandType.Text
                dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                While dReader.Read()
                    If CInt(dReader("Correcto").ToString()) > 0 Then
                        Autorizado = True
                    Else
                        Autorizado = False
                        MsgBox("Error de clave o usuaro no tiene permisos", MsgBoxStyle.Exclamation, "No se pudo acceder al cambio de preclaves")
                        Exit Sub
                    End If
                End While
                dReader.Close()
                con.Close()
            Catch ex As Exception
            End Try

            'Recorro a los usuarios para grabar su clave 
            Try
                SQL.conectarTPM()

                Dim con As New SqlConnection(StrTpm)
                Dim cmd As New SqlCommand()
                Dim strSelect As String = ""
                Dim dReader As SqlDataReader

                strSelect = "SELECT * FROM Operacion_Empleado WHERE Activo = 'Y' AND Id_Empleado <> 999"
                cmd.Connection = con
                cmd.CommandText = strSelect
                con.Open()
                cmd.CommandType = CommandType.Text
                dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                While dReader.Read()
Regresa:
                    Dim NvaPreClave As String
                    NvaPreClave = InputBox("Ingresa la preclave para " & dReader("Name"), "Administrador de preclaves", dReader("preCode"))
                    If String.IsNullOrEmpty(NvaPreClave.Trim) Then
                        MsgBox("Deberá registrarse la preclave!!", MsgBoxStyle.Exclamation, "Información obligatoria")
                        GoTo Regresa
                    End If

                    ''Realizo el UPDATE
                    'INICIALIZA LA VARIABLE EN NADA
                    Dim conexionUpdate As New SqlConnection(StrTpm)
                    SQLUpdatepreClave = "UPDATE " + BaseDatosTPD + ".dbo.Operacion_Empleado SET preCode = '" + NvaPreClave.ToString() + "' "
                    SQLUpdatepreClave &= "WHERE Id_Empleado = " + dReader("Id_Empleado").ToString()
                    Dim commandUpdate As SqlCommand = New SqlCommand(SQLUpdatepreClave, conexionUpdate)
                    commandUpdate.Connection = conexionUpdate
                    conexionUpdate.Open()
                    commandUpdate.ExecuteNonQuery()
                    conexionUpdate.Dispose()
                    commandUpdate.Dispose()

                End While
                dReader.Close()
                con.Close()
            Catch ex As Exception
                MsgBox("Error de Consulta o Conexion al leer usuario: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
            End Try






            '   'CONECTA A LA BASE DE DATOS
            '   conexion_universal.conectar()
            '   Dim Qry As String = "SELECT Count(*) as Correcto FROM Operacion_Empleado WHERE KeyCode = '" & Cve & "' AND Permiso_para_cambiarPreclaves = 1"

            '   conexion_universal.slq_s = New SqlCommand(Qry, conexion_universal.conexion_uni)
            '   'EJECUTA LA CONSULTA
            '   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            '   'RECORRE LA CONSULTA
            '   If (conexion_universal.rd_s.Read) Then
            '    If CInt(rd_s.Item("Correcto").ToString()) > 0 Then
            '     conexion_universal.rd_s.Close() 'CIERRA EL READE

            '     'consulta DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            '     conexion_universal.slq_s = New SqlCommand("SELECT * FROM Operacion_Empleado WHERE Activo = 'Y' AND Id_Empleado <> 999", conexion_universal.conexion_uni)
            '     'EJECUTA LA CONSULTA
            '     conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            '     'RECORRE LA CONSULTA
            '     While conexion_universal.rd_s.Read
            'Regresa:
            '      Dim NvaPreClave As String
            '      NvaPreClave = InputBox("Ingresa la preclave para " & rd_s.Item("Name"), "Administrador de preclaves", rd_s.Item("preCode"))
            '      If String.IsNullOrEmpty(NvaPreClave.Trim) Then
            '       MsgBox("Deberá registrarse la preclave!!", MsgBoxStyle.Exclamation, "Información obligatoria")
            '       GoTo Regresa
            '      End If

            '      ''Realizo el UPDATE
            '      'INICIALIZA LA VARIABLE EN NADA
            '      Dim conexionUpdate As New SqlConnection(StrTpm)
            '      SQLUpdatepreClave = "UPDATE " + BaseDatosTPD + ".dbo.Operacion_Empleado SET preCode = '" + NvaPreClave.ToString() + "' "
            '      SQLUpdatepreClave &= "WHERE Id_Empleado = " + rd_s.Item("Id_Empleado").ToString()
            '      Dim commandUpdate As SqlCommand = New SqlCommand(SQLUpdatepreClave, conexionUpdate)
            '      commandUpdate.Connection = conexionUpdate
            '      conexionUpdate.Open()
            '      commandUpdate.ExecuteNonQuery()
            '      conexionUpdate.Dispose()
            '      commandUpdate.Dispose()
            '      'conexion2.Close()

            '     End While
            '     conexion_universal.cerrar_conectar()
            '    Else
            '     MsgBox("Error de clave o usuaro no tiene permisos", MsgBoxStyle.Exclamation, "No se pudo acceder al cambio de preclaves")
            '    End If
            '   End If
        Catch ex As Exception
            MsgBox("Error de Consulta o Conexion al leer usuario: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
            conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
            Return
        End Try
    End Sub

    Private Sub dgvOrdenes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrdenes.CellContentClick

    End Sub
#End Region
    '----- FIN EVENTOS



End Class