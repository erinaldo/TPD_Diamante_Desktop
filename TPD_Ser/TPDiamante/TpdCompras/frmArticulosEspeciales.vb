Imports System.Data.SqlClient
Imports ClosedXML

Public Class frmArticulosEspeciales
 'Fecha inicial obtenida del formulario
 Dim fi As String
 'Fecha final obtenida del formulario
 Dim ff As String
 'Fecha Final pero obtenida en tipo de dato DATE para su comparacion
 Dim final As Date
 'Fecha Inicial pero obtenida en tipo de dato DATE para su comparacion
 Dim Inicio As Date
 'Dataview contenedor de datos del metodo LlenarDetalle
 Dim ResultadoDetalle As DataView
 'Dataview contenedor de datos del metodo LlenarOrdenes
 Dim ResultadoOrden As DataView
 Dim Date_grid As Date
 'Colores creados para uso en el grid DGVoperacionVta
 Dim blanco As Color = Color.White 'CUANDO SE AGREGUE el renglon (no se tiene modificacion alguna)
 Dim azul As Color                 'Cuando se agrega la fecha de entrega
 Dim verde As Color                'Confirmado y SOlicitado
 Dim amarillo As Color             'Articulo recibido
 Dim rojo As Color                 'En caso de existir alguna devolucion (validar con columna)
 Dim naranja As Color              'PEDIDOS PREVIAMENTE SOLICITADOS, QUE YA FUERON RECIBIDOS Y NO HAN SIDO FACTURADOS.

 Dim DsOrdenes As DataSet 'DECLARA DATASET PARA PROCEDIMIENTO DE INSERTAR ORDENES
 'VALIDA CUANDO YA ESTE CREADA LA COLUMNA QUITAR
 Dim Quitar_Ok As Boolean = False
 Dim esAgente As Boolean = False

 'Manejador de DataPicker
 'Dim DTP1 As New DateTimePicker

#Region "METODOS"

 'METODO QUE EJECUTA LA CONSULTA QUE MUESTRA LAS ORDENES DE VENTA EN PEDIDO PARA ALMACEN
 Sub MEjecuta_PedidosEspeciales()
  'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
  Dim cmd As SqlCommand
  Dim cnn As SqlConnection = Nothing
  Dim da As SqlDataAdapter
  DsOrdenes = New DataSet

  'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
  Try
   cnn = New SqlConnection(StrTpm) 'ORIGINAL
   'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBA
   cmd = New SqlCommand("SP_Comp_Art_E", cnn)
   cmd.CommandType = CommandType.StoredProcedure
   cmd.Parameters.AddWithValue("@fechaIni", String.Format("{0:yyyy-MM-dd}", dtpFechaIni.Value))
   cmd.Parameters.AddWithValue("@fechaFin", String.Format("{0:yyyy-MM-dd}", dtpFechaFin.Value))
   'MsgBox(DTPFecha.Value.ToString("dddd"))
   cnn.Open()
   da = New SqlDataAdapter
   da.SelectCommand = cmd
   da.SelectCommand.Connection = cnn
   da.SelectCommand.CommandTimeout = 10000
   cmd.ExecuteNonQuery()
   cmd.Connection.Close()
   cnn.Close()
  Catch ex As Exception
   MessageBox.Show("Error al Mostrar los datos de las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
   Return
  End Try
 End Sub

 Dim SQL As New Comandos_SQL()

 Sub MLlenaOrdenes()
  If dgvContenido.RowCount <> 0 Then
   dgvContenido.Columns().Remove("Quitar")
   dgvContenido.DataSource = Nothing
  End If

  Dim cbquitado As Integer = 0
  'VALIDA QUE SE MUESTREN LOS QUITDOS
  If cbQuitados.Checked = False Then
   'SQLOrdenes &= "AND T0.Quitar <> 1 "
   cbquitado = 0
  Else
   cbquitado = 1
  End If

  Dim cbcancelado As Integer = 0
  'VALIDA QUE SE MUESTREN LOS CANCELADOS
  If cbCancelados.Checked = False Then
   'SQLOrdenes &= "AND T0.Quitar <> 1 "
   cbcancelado = 0
  Else
   cbcancelado = 1
  End If

  'VALIDA QUE SE MUESTREN TODAS LAS FACTURAS FINALIZADAS
  Dim cbfinalizada As Integer = 0
  If cbFinalizadas.Checked = True Then
   cbfinalizada = 1
   'SQLOrdenes &= "AND PendienteFac = 0 "
  Else
   cbfinalizada = 0
  End If
  'VALIDA QUE SOLO MUESTRE LOS DEVUELTOS
  Dim cbdevueltos As Integer = 0
  If cbDevuelto.Checked = True Then
   cbdevueltos = 1
   'SQLOrdenes &= "AND Devuelto > 0 "
  Else
   cbdevueltos = 0
  End If
  'SQLOrdenes &= "ORDER BY FechaCreacion asc "

  dgvContenido.DataSource = SQL.EjecutarProcedimiento("TPD_Consulta_ArtCe_Ver2", "@fechaInicio,@fechaFin,@cbQuitados,@cbCancelados2,@cbFinalizadas,@cbDevuelto", 6, dtpFechaIni.Value.ToString("yyyy-MM-dd") + "," + dtpFechaFin.Value.ToString("yyyy-MM-dd") + "," + cbquitado.ToString() + "," + cbcancelado.ToString() + "," + cbfinalizada.ToString() + "," + cbdevueltos.ToString())

  If (dgvContenido.Rows.Count <> 0) Then
   dgvContenido.Columns().Remove("Quitar")
   dgvContenido.Columns().Remove("ConfirmarSolicitud")
   dgvContenido.Columns().Remove("CancelarSolicitud")
   dgvContenido.Columns().Remove("SolicitudConfirmada")

   Dim colcs As New DataGridViewCheckBoxColumn
   colcs.DataPropertyName = "ConfirmarSolicitud"
   colcs.HeaderText = "Confirmar solicitud"
   colcs.Name = "ConfirmarSolicitud"
   colcs.SortMode = DataGridViewColumnSortMode.Automatic
   colcs.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   colcs.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
   colcs.Width = 75
   colcs.ReadOnly = True
            dgvContenido.Columns.Insert(18, colcs)

            Dim colcas As New DataGridViewCheckBoxColumn
   colcas.DataPropertyName = "CancelarSolicitud"
   colcas.HeaderText = "Cancelar solicitud"
   colcas.Name = "CancelarSolicitud"
   colcas.SortMode = DataGridViewColumnSortMode.Automatic
   colcas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   colcas.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
   colcas.Width = 75
   colcas.ReadOnly = True
   dgvContenido.Columns.Insert(19, colcas)

   Dim colsoc As New DataGridViewCheckBoxColumn
   colsoc.DataPropertyName = "SolicitudConfirmada"
   colsoc.HeaderText = "Solicitud confirmada"
   colsoc.Name = "SolicitudConfirmada"
   colsoc.SortMode = DataGridViewColumnSortMode.Automatic
   colsoc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   colsoc.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
   colsoc.Width = 75
   colsoc.ReadOnly = True
   dgvContenido.Columns.Insert(20, colsoc)

   Dim col As New DataGridViewCheckBoxColumn
   col.DataPropertyName = "Quitar"
   col.HeaderText = "Quitar"
   col.Name = "Quitar"
   col.SortMode = DataGridViewColumnSortMode.Automatic
   col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   col.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
   col.Width = 75
   col.ReadOnly = True
   dgvContenido.Columns.Insert(22, col)
   Quitar_Ok = True

  End If
  MEstiloGridOrdenes()
 End Sub

 Sub MLlenaOrdenesO()
  'VARIABLE DE CADENA DE SQL
  Dim SQLOrdenes As String
  'VARIABLES DE CONEXION DE LLENADO
  Dim cmd As SqlCommand
  Dim cnn As SqlConnection = Nothing
  Dim da As SqlDataAdapter
  Dim DsOrdenes = New DataSet
  'OBTIENE LA FECHA INICIAL Y LA FINAL
  fi = dtpFechaIni.Value.ToString("yyyy-MM-dd")
  ff = dtpFechaFin.Value.ToString("yyyy-MM-dd")
  'REFRESCA EL DATA GRID VIEW DE RESULTADO Y EL DETALLE
  If dgvContenido.RowCount > 0 Then
   'dgvOrdenes.Rows.Clear()
   dgvContenido.Columns().Remove("Quitar")
   dgvContenido.DataSource = Nothing
  End If

  'ALAMACENA LA CONSULTA
  'OBTIENE LOS DATOS DE TODAS LAS ORDENES DE COMPRA ESPECIALES
  SQLOrdenes = "SELECT T0.Articulo, T0.Descripcion, T0.CodigoLinea, T0.Linea, T0.Usuario, T0.OrdenVta, "
  SQLOrdenes &= "FORMAT(T0.FechaCreacion, 'yyyy-MM-dd') AS FechaCreacion, "
  SQLOrdenes &= "T0.Cliente, T0.Nombre, "
  SQLOrdenes &= "T1.OnHand AS StockTotal, T0.SolicitaCte, T0.Facturado, T0.PendienteFac, T0.Devuelto, T0.Comentario, 0 AS Quitar "
  SQLOrdenes &= "FROM TPM.dbo.ComprasArticulosE T0 LEFT JOIN SBO_TPD.dbo.OITW T1 ON T0.Articulo = T1.ItemCode COLLATE Modern_Spanish_CI_AS AND T1.WhsCode = '01' "
  SQLOrdenes &= "WHERE FechaCreacion BETWEEN '" + fi + "' AND '" + ff + "' "
  'VALIDA QUE SE MUESTREN LOS QUITDOS
  If cbQuitados.Checked = False Then
   SQLOrdenes &= "AND T0.Quitar <> 1 "
  End If
  'VALIDA QUE SE MUESTREN LOS CANCELADOS
  If cbCancelados.Checked = False Then
   SQLOrdenes &= "AND T0.CancelarSolicitud <> 1 "
  End If
  'VALIDA QUE SE MUESTREN TODAS LAS FACTURAS FINALIZADAS
  If cbFinalizadas.Checked = True Then
   SQLOrdenes &= "AND PendienteFac = 0 "
  End If

  'VALIDA QUE SOLO MUESTRE LOS DEVUELTOS
  If cbDevuelto.Checked = True Then
   SQLOrdenes &= "AND Devuelto > 0 "
  End If
  SQLOrdenes &= "ORDER BY FechaCreacion asc "

  Try
   'RELAIZA LA CONEXION
   'cnn = New SqlConnection(StrPruebas)
   cnn = New SqlConnection(StrCon)
   'ALMACENA LA CONSULTA EN UN COMMAND SQL
   cmd = New SqlCommand(SQLOrdenes, cnn)
   'CONVIERTE EL TEXTO EN CONSULTA
   cmd.CommandType = CommandType.Text
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
   ResultadoOrden = New DataView
   'ALMACENA EN DATA SET DE MODO TABLA
   ResultadoOrden.Table = DsOrdenes.Tables(0)
   'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
   dgvContenido.DataSource = Nothing
   'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
   dgvContenido.DataSource = ResultadoOrden
   'VALIDA SI EL RESULTADO ES MAYOR PARA AGREGAR LA COLUMNA
   If (ResultadoOrden.Count > 0) Then
    'ELIMINA LA COLUMNA Quitar DEL DATA GRID TRAIA DEL DATATABLE
    dgvContenido.Columns().Remove("Quitar")
    'CREA INSTANCIA DE COLUMNA CON CHECKBOX Y DA FORMATO EN QUITAR
    Dim col As New DataGridViewCheckBoxColumn
    col.DataPropertyName = "Quitar" 'COLOCA LA PROPIEDAD
    col.HeaderText = "Quitar" 'COLOCA EL ENCABEZADO
    col.Name = "Quitar" 'COLOCA EL NOMBRE DE LA COLUMNA
    col.SortMode = DataGridViewColumnSortMode.Automatic 'DEFINE EL ORDENADO DE LA COLUMNA
    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 'CENTRA EL CONTENIDO
    col.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular) 'FORMATO DE LETRA
    col.Width = 75 'ANCHO DE LA CELDA
    col.ReadOnly = True 'QUE SOLO SEA DE LECTURA
    'COLOCA LA COLUMNA DE DE Quitar DE MANERA CHECKBOX, EL 15 ES LA POSICIÓN EN DONDE APARECE EN EL DATA GRID, DE 
    'RECORRERSE O AGREGAR UN CAMPO MAS, ESTE NUMERO DEBERA INCREMETARSE O DISMINUIR SEGUN EL CASO.
    dgvContenido.Columns.Insert(15, col)
    'ALMACENA VERDADERO EN VARIABLE BOOLEANA DE CREACION DE QUITAR
    Quitar_Ok = True
   End If

   'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES
   MEstiloGridOrdenes()
  Catch ex As Exception
   MsgBox("Error al obtener los datos: " + ex.ToString)
  End Try
 End Sub

 Sub MEstiloGridOrdenes()
  'ESTILOS POR COLUMNAp@ssw0rd
  Try
   With Me.dgvContenido

    'COLOCA PROPIEDADES DE COLOR ALTERNADOS
    .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
    .AlternatingRowsDefaultCellStyle.BackColor = Color.White
    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
    .DefaultCellStyle.BackColor = Color.White
    .DefaultCellStyle.SelectionForeColor = Color.White
    .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
    .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    'Articulo
    .Columns("Articulo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
    .Columns("Articulo").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Articulo").HeaderText = "Articulo."
    .Columns("Articulo").Width = 120
    .Columns("Articulo").ReadOnly = True
    '.Columns("Articulo").SortMode = DataGridViewColumnSortMode.NotSortable
    'Descripcion
    .Columns("Descripcion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
    .Columns("Descripcion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Descripcion").HeaderText = "Descripcion."
    .Columns("Descripcion").Width = 200
    .Columns("Descripcion").ReadOnly = True

    'Codigo Linea
    .Columns("CodigoLinea").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("CodigoLinea").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("CodigoLinea").HeaderText = "Codigo linea"
    .Columns("CodigoLinea").Width = 50
    .Columns("CodigoLinea").ReadOnly = True
    .Columns("CodigoLinea").Visible = False

    'Linea
    .Columns("Linea").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
    .Columns("Linea").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Linea").HeaderText = "Grupo de Linea"
    .Columns("Linea").Width = 100
    .Columns("Linea").ReadOnly = True
    '.Columns("Linea").SortMode = DataGridViewColumnSortMode.NotSortable
    'Usuario
    .Columns("Usuario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    .Columns("Usuario").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Usuario").HeaderText = "Usuario"
    .Columns("Usuario").Width = 150
    .Columns("Usuario").ReadOnly = True
    'Orden Venta
    .Columns("OrdenVta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("OrdenVta").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("OrdenVta").HeaderText = "Orden Vta."
    .Columns("OrdenVta").Width = 70
    .Columns("OrdenVta").ReadOnly = True
    .Columns("OrdenVta").Frozen = True
    '.Columns("OrdenVenta").SortMode = DataGridViewColumnSortMode.NotSortable
    'Fecha creacion
    .Columns("FechaCreacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("FechaCreacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("FechaCreacion").HeaderText = "Fecha de Creación"
    .Columns("FechaCreacion").Width = 65
    .Columns("FechaCreacion").ReadOnly = True
    '.Columns("FechaCreacion").SortMode = DataGridViewColumnSortMode.NotSortable
    'Card Code
    .Columns("Cliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    .Columns("Cliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Cliente").HeaderText = "Cliente"
    .Columns("Cliente").Width = 70
    .Columns("Cliente").ReadOnly = True
    '.Columns("CardCode").SortMode = DataGridViewColumnSortMode.NotSortable
    'Card name
    .Columns("Nombre").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    .Columns("Nombre").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Nombre").HeaderText = "Nombre"
    .Columns("Nombre").Width = 200
    .Columns("Nombre").ReadOnly = True
    '.Columns("CardName").SortMode = DataGridViewColumnSortMode.NotSortable
    'STOCKtotal
    .Columns("Stock Puebla").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("Stock Puebla").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Stock Puebla").HeaderText = "Stock Puebla"
    .Columns("Stock Puebla").DefaultCellStyle.Format = "N0"
    .Columns("Stock Puebla").Width = 50
    .Columns("Stock Puebla").ReadOnly = True
    '.Columns("StockTotal").SortMode = DataGridViewColumnSortMode.NotSortable
    'STOCKtotal

    '.Columns("StockTotal").SortMode = DataGridViewColumnSortMode.NotSortable
    'STOCKtotal
    .Columns("Stock Merida").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("Stock Merida").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Stock Merida").HeaderText = "Stock Merida"
    .Columns("Stock Merida").DefaultCellStyle.Format = "N0"
    .Columns("Stock Merida").Width = 50
    .Columns("Stock Merida").ReadOnly = True


    .Columns("Stock Tuxtla").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("Stock Tuxtla").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Stock Tuxtla").HeaderText = "Stock Tuxtla"
    .Columns("Stock Tuxtla").DefaultCellStyle.Format = "N0"
    .Columns("Stock Tuxtla").Width = 50
    .Columns("Stock Tuxtla").ReadOnly = True
    '.Columns("StockTotal").SortMode = DataGridViewColumnSortMode.NotSortable
    'SOLICITA CLIENTE
    .Columns("SolicitaCte").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("SolicitaCte").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("SolicitaCte").HeaderText = "Solicita Cliente"
    .Columns("SolicitaCte").DefaultCellStyle.Format = "N0"
    .Columns("SolicitaCte").Width = 50
    .Columns("SolicitaCte").ReadOnly = True
    '.Columns("SolicitaCliente").SortMode = DataGridViewColumnSortMode.NotSortable
    'Facturado
    .Columns("Facturado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("Facturado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Facturado").HeaderText = "Facturado"
    .Columns("Facturado").DefaultCellStyle.Format = "N0"
    .Columns("Facturado").Width = 55
    .Columns("Facturado").ReadOnly = True
    '.Columns("Facturado").SortMode = DataGridViewColumnSortMode.NotSortable
    'PendienteFacturar
    .Columns("PendienteFac").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("PendienteFac").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("PendienteFac").HeaderText = "Pendiente por Facturar"
    .Columns("PendienteFac").DefaultCellStyle.Format = "N0"
    .Columns("PendienteFac").Width = 70
    .Columns("PendienteFac").ReadOnly = True
    '.Columns("PendienteFacturar").SortMode = DataGridViewColumnSortMode.NotSortable
    'Devolucion
    .Columns("Devuelto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("Devuelto").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Devuelto").HeaderText = "Devuelto"
    .Columns("Devuelto").DefaultCellStyle.Format = "N0"
    .Columns("Devuelto").Width = 50
    .Columns("Devuelto").ReadOnly = True
    '.Columns("Devolucion").SortMode = DataGridViewColumnSortMode.NotSortable

    'CantMinimaCompra
    .Columns("CantMinimaCompra").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("CantMinimaCompra").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("CantMinimaCompra").HeaderText = "Cant. mínima de compra"
    .Columns("CantMinimaCompra").DefaultCellStyle.Format = "N0"
    .Columns("CantMinimaCompra").Width = 60
    .Columns("CantMinimaCompra").ReadOnly = True
    'FechaEntrega
    .Columns("FechaEntrega").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("FechaEntrega").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("FechaEntrega").HeaderText = "Fecha de entrega"
    .Columns("FechaEntrega").DefaultCellStyle.Format = "N0"
    .Columns("FechaEntrega").Width = 80
    .Columns("FechaEntrega").ReadOnly = True
    'ConfirmarSolicitud
    .Columns("ConfirmarSolicitud").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("ConfirmarSolicitud").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("ConfirmarSolicitud").HeaderText = "Confirmar solicitud"
    .Columns("ConfirmarSolicitud").DefaultCellStyle.Format = "N0"
    .Columns("ConfirmarSolicitud").Width = 60
    If esAgente = False Then
     .Columns("ConfirmarSolicitud").ReadOnly = True
    End If
    'CancelarSolicitud
    .Columns("CancelarSolicitud").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("CancelarSolicitud").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("CancelarSolicitud").HeaderText = "Cancelar solicitud"
    .Columns("CancelarSolicitud").DefaultCellStyle.Format = "N0"
    .Columns("CancelarSolicitud").Width = 60
    .Columns("CancelarSolicitud").ReadOnly = True
    'SolicitudConfirmada
    .Columns("SolicitudConfirmada").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("SolicitudConfirmada").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("SolicitudConfirmada").HeaderText = "Solicitud confirmada"
    .Columns("SolicitudConfirmada").DefaultCellStyle.Format = "N0"
    .Columns("SolicitudConfirmada").Width = 60
    .Columns("SolicitudConfirmada").ReadOnly = True
    'FechaRecepcion
    .Columns("FechaRecepcion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("FechaRecepcion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("FechaRecepcion").HeaderText = "Fecha de Recepción"
    .Columns("FechaRecepcion").DefaultCellStyle.Format = "N0"
    .Columns("FechaRecepcion").Width = 80
    .Columns("FechaRecepcion").ReadOnly = True

    'COMENTARIO
    .Columns("Comentario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    .Columns("Comentario").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
    .Columns("Comentario").HeaderText = "Comentarios"
    .Columns("Comentario").Width = 200
    .Columns("Comentario").ReadOnly = False
    'QUITAR
    .Columns("Quitar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns("Quitar").HeaderText = "Quitar"
    .Columns("Quitar").Width = 50
    .Columns("Quitar").ReadOnly = True
    .Columns("Quitar").Visible = False
    If (IdUsuario = "MANAGER" Or IdUsuario = "COMPRAS1" Or IdUsuario = "CINTER") Then
     .Columns("Quitar").Visible = True
    End If

   End With
  Catch ex As Exception
   MsgBox("Error al dar formato en grid: " + ex.ToString)
  End Try
 End Sub

 Sub MEstiloGridDetalle()
  With Me.dgvDetalleCompra
   'COLOCA PROPIEDADES DE COLOR ALTERNADOS
   .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionForeColor = Color.White
   .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
   .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   'Articulo
   .Columns("ItemCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
   .Columns("ItemCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("ItemCode").HeaderText = "Articulo."
   .Columns("ItemCode").Width = 180
   .Columns("ItemCode").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   '.Columns("Articulo").SortMode = DataGridViewColumnSortMode.NotSortable
   'DocEntry de Orden compra
   .Columns("DocEntry").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("DocEntry").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("DocEntry").HeaderText = "DocEntry"
   .Columns("DocEntry").Width = 80
   .Columns("DocEntry").ReadOnly = True
   .Columns("DocEntry").Visible = False
   'Orden compra
   .Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("DocNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("DocNum").HeaderText = "Orden de Compra"
   .Columns("DocNum").Width = 80
   .Columns("DocNum").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   '.Columns("OrdenCompra").SortMode = DataGridViewColumnSortMode.NotSortable
   'Fecha de Creacion
   .Columns("DocDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("DocDate").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("DocDate").HeaderText = "Fecha de Creacion"
   .Columns("DocDate").Width = 85
   .Columns("DocDate").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   '.Columns("DocDate").SortMode = DataGridViewColumnSortMode.NotSortable
   'CantSolicitada
   .Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("Quantity").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Quantity").HeaderText = "Cantidad Solicitada"
   .Columns("Quantity").DefaultCellStyle.Format = "N0"
   .Columns("Quantity").Width = 80
   .Columns("Quantity").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   '.Columns("CantSolicitada").SortMode = DataGridViewColumnSortMode.NotSortable
   'Fecha Tentativa de Entrega
   .Columns("DocDueDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("DocDueDate").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("DocDueDate").HeaderText = "Fecha Tentativa Entrega"
   .Columns("DocDueDate").Width = 85
   .Columns("DocDueDate").ReadOnly = True
   'NumEntrada
   .Columns("Entrada").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
   .Columns("Entrada").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Entrada").HeaderText = "Numero de Entrada"
   .Columns("Entrada").Width = 80
   .Columns("Entrada").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   '.Columns("NumEntrada").SortMode = DataGridViewColumnSortMode.NotSortable
   'FechaEntrada
   .Columns("FechaEntrada").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("FechaEntrada").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("FechaEntrada").HeaderText = "Fecha de entrada"
   .Columns("FechaEntrada").Width = 80
   .Columns("FechaEntrada").ReadOnly = True
   .Columns("FechaEntrada").Visible = True
   'Bloquea el ordenamiento del gridview
   '.Columns("FechaEntrada").SortMode = DataGridViewColumnSortMode.NotSortable
   'CantRecibida
   .Columns("Recibido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
   .Columns("Recibido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Recibido").HeaderText = "Cantidad Recibida"
   .Columns("Recibido").DefaultCellStyle.Format = "N0"
   .Columns("Recibido").Width = 70
   .Columns("Recibido").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   '.Columns("CantRecibida").SortMode = DataGridViewColumnSortMode.NotSortable
   'Faltante
   .Columns("Faltante").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
   .Columns("Faltante").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Faltante").HeaderText = "Faltante"
   .Columns("Faltante").DefaultCellStyle.Format = "N0"
   .Columns("Faltante").Width = 73
   .Columns("Faltante").ReadOnly = True
   'BASE ENTRY
   .Columns("BaseEntry").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
   .Columns("BaseEntry").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("BaseEntry").HeaderText = "BaseEntry"
   .Columns("BaseEntry").DefaultCellStyle.Format = "N0"
   .Columns("BaseEntry").Width = 73
   .Columns("BaseEntry").Visible = False


   'Bloquea el ordenamiento del gridview
   '.Columns("Faltante").SortMode = DataGridViewColumnSortMode.NotSortable
  End With
 End Sub

 Sub LlenardgvDetalle(Articulo As String, FechaCreacion As String)
  Try
   'VARIABLE DE CADENA DE SQL
   Dim SQLOrdenes As String
   'VARIABLE QUE ALMACENA LA FECHA DEL GRID
   'VARIABLES DE CONEXION DE LLENADO
   Dim cmd As SqlCommand
   Dim cnn As SqlConnection = Nothing
   Dim da As SqlDataAdapter
   Dim DsOrdenes = New DataSet



   'ALAMACENA LA CONSULTA
   SQLOrdenes = "SELECT T1.ItemCode, T0.DocEntry, T0.DocNum, FORMAT(T0.DocDate, 'yyyy-MM-dd') AS DocDate, T1.Quantity, FORMAT(T0.DocDueDate, 'yyyy-MM-dd') AS DocDueDate "
   SQLOrdenes &= "INTO #PEDIDO "
   SQLOrdenes &= "FROM OPOR T0 INNER JOIN POR1 T1 ON T0.DocEntry = T1.DocEntry "
   'SQLOrdenes &= "WHERE T0.DocDate BETWEEN '" + FechaCreacion + "' AND '" + ff + "' AND T1.ItemCode = '" + Articulo + "' "
   SQLOrdenes &= "WHERE T1.ItemCode = '" + Articulo + "' "
   SQLOrdenes &= "AND T0.DocStatus <> 'C' "

   SQLOrdenes &= "SELECT IIF(T3.DocNum IS NULL, 0, T3.DocNum) AS Entrada, "
   SQLOrdenes &= "IIF(T3.DocDate IS NULL, '-',  "
   SQLOrdenes &= "FORMAT(T3.DocDate, 'yyyy-MM-dd')) AS FechaEntrada, IIF(T2.Quantity IS NULL, 0, T2.Quantity) AS Recibido,  "
   'SQLOrdenes &= "IIF(T2.OpenQty IS NULL, 0, T2.Quantity - T2.OpenCreQty) AS Faltante, T2.BaseEntry  "
   'SQLOrdenes &= "IIF(T2.OpenQty IS NULL, 0, T2.Quantity - T2.OpenQty) AS Faltante, T2.BaseEntry  "
   SQLOrdenes &= "IIF(T2.OpenCreQty IS NULL, 0, OpenCreQty) AS Faltante, T2.BaseEntry  "
   SQLOrdenes &= "INTO #ENTRADA "
   SQLOrdenes &= "FROM PDN1 T2 INNER JOIN OPDN T3 ON T3.DocEntry = T2.DocEntry  "
   SQLOrdenes &= "WHERE T2.DocDate BETWEEN '" + FechaCreacion + "' AND '" + ff + "' "
   SQLOrdenes &= "AND T2.BaseEntry IN(SELECT DocEntry FROM #PEDIDO WHERE ItemCode = T2.ItemCode) "

   SQLOrdenes &= "SELECT ItemCode, DocEntry, DocNum, DocDate, Quantity, DocDueDate, Entrada, FechaEntrada, Recibido, Faltante, BaseEntry "
   SQLOrdenes &= "FROM #PEDIDO T0 LEFT JOIN #ENTRADA T1 ON T0.DocEntry = T1.BaseEntry "
   SQLOrdenes &= "WHERE T0.ItemCode = '" + Articulo + "' "

   SQLOrdenes &= "DROP TABLE #PEDIDO "
   SQLOrdenes &= "DROP TABLE #ENTRADA "

   'cnn = New SqlConnection(StrPruebas)
   cnn = New SqlConnection(StrCon)
   'ALMACENA LA CONSULTA EN UN COMMAND SQL
   cmd = New SqlCommand(SQLOrdenes, cnn)
   'CONVIERTE EL TEXTO EN CONSULTA
   cmd.CommandType = CommandType.Text
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
   ResultadoDetalle = New DataView
   'ALMACENA EN DATA SET DE MODO TABLA
   ResultadoDetalle.Table = DsOrdenes.Tables(0)
   'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
   dgvDetalleCompra.DataSource = Nothing
   'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
   dgvDetalleCompra.DataSource = ResultadoDetalle
   'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES
   MEstiloGridDetalle()

  Catch ex As Exception
   MsgBox("Error: " + ex.ToString)
  End Try
 End Sub

 'METODO DE BLOQUEAR FACTURAS
 Sub MQuitados()
  'RECORRE EL GRID Y COMPARA CON LA BASE DE DATOS SI LA FACTURA ESTA BLOQUEADA
  For i As Integer = 0 To dgvContenido.Rows.Count - 1
   Try
    'APERTURA DE CONEXION
    SQL.conectarTPM()
    'cnn = New SqlConnection(StrCon)
    Dim dato As String
    'VARIABLE DE CONSULTA
    Dim SQLBuscar As String = ""
    'ALMACENA LA CONSULTA
    SQLBuscar = "SELECT Quitar "
    SQLBuscar &= "FROM TPM.dbo.ComprasArticulosE "
    SQLBuscar &= "WHERE Articulo = '" + dgvContenido.Rows(i).Cells("Articulo").Value.ToString + "' "
    SQLBuscar &= "AND OrdenVta = " + dgvContenido.Rows(i).Cells("OrdenVta").Value.ToString + " "
    'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
    dato = SQL.CampoEspecifico(SQLBuscar, "Quitar")

    'VALIDA SI LA FACTURA ESTA BLOQUEADO
    If dato = "1" Then
     dgvContenido.Rows(i).Cells("Quitar").Value = 1
    Else
     dgvContenido.Rows(i).Cells("Quitar").Value = 0
    End If
    SQL.Cerrar()

   Catch ex As Exception
    MsgBox("Error al buscar el articulo quitado: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de Busqueda")
    'CIERRA LA CONEXION
    SQL.Cerrar()
    Return
   Finally
    'CIERRA LAS CONEXIONES DE USO
    SQL.Cerrar()
   End Try
  Next
 End Sub

#End Region

 Private Sub frmArticulosEspeciales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
  'Reviso si es tipo Agente o no
  SQL.conectarTPM()
  esAgente = SQL.CampoEspecifico("SELECT CASE WHEN idRol = 5 THEN 1 ELSE 0 END AS EsAgenteVentas FROM Usuarios where Id_Usuario = '" + UsrTPM + "'", "EsAgenteVentas")

  'Obtiene las fechas de inicio y la fecha final del formulario
  Inicio = dtpFechaIni.Value
  final = dtpFechaFin.Value
  'Compara y valida quue las fechas esten en orden correcto
  If (DateTime.Compare(final.Date, Inicio.Date) > 0) Then
   'MANDA A LLAMAR EL METODO QUE INSERTA LOS ARTICULOS CON ORDENES DE VENTA QUE SON DE COMPRA ESPECIAL
   MEjecuta_PedidosEspeciales()
   ''MAND A LLAMAR EL METODO DE LLENADO DEL CONTENIDO
   MLlenaOrdenes()
   ''MANDA A LLAMAR EL METODO DE ESTILO DEL GRID
   MEstiloGridOrdenes()
   ''Obtiene el ARticulo seleccionado
   Dim row As DataGridViewRow = dgvContenido.CurrentRow()
   ''Llena el datagrid con el detalle 
   If dgvContenido.RowCount > 0 Then
    LlenardgvDetalle(CStr(row.Cells("Articulo").Value).ToString, CStr(row.Cells("FechaCreacion").Value).ToString)
    dgvContenido.ScrollBars = ScrollBars.Both
   End If

  End If
 End Sub

 Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
  Inicio = dtpFechaIni.Value
  final = dtpFechaFin.Value
  If (DateTime.Compare(final.Date, Inicio.Date) > 0) Then
   'MAND A LLAMAR EL METODO DE LLENADO DEL CONTENIDO
   MLlenaOrdenes()
   'Obtiene el ARticulo seleccionado
   Dim row As DataGridViewRow = dgvContenido.CurrentRow()
   'Llena el datagrid con el detalle 
   If dgvContenido.RowCount > 0 Then
    LlenardgvDetalle(CStr(row.Cells("Articulo").Value).ToString, CStr(row.Cells("FechaCreacion").Value).ToString)
   End If
  Else
   MsgBox("La fecha de inicio no puede ser menor a la fecha final.", MsgBoxStyle.Exclamation, "Alerta de Captura")
  End If
 End Sub

 Private Sub dgvContenido_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvContenido.CellContentClick
  Dim strValue As String
  'VARIABLES DE CONEXION A LA BASE DE DATOS
  Dim con As New SqlConnection
  Dim cmd As New SqlCommand
  Dim CadenaSQL As String = ""
  'VALIDA SI HAY REGISTROS EN EL GRID
  If e.RowIndex >= 0 Then
   'VALIDA SI FUE PRESIONADA LA CELDA DE QUITAR
   If Me.dgvContenido.Columns(e.ColumnIndex).Name = "Quitar" And (UsrTPM = "COMPRAS1" Or UsrTPM = "CINTER" Or UsrTPM = "MANAGER") Then
    'OBTIENE SI EL USUARIO DIO CLICK EN LA COLUMNA
    strValue = Me.dgvContenido.Item(e.ColumnIndex, e.RowIndex).Value
    If (strValue = "1") Then
     MsgBox("No es posible restaurar el articulo, favor de notificar al area de Sistemas.")
    Else
     If MessageBox.Show("Realmente desea quitar el articulo de la lista ? ", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
      'ASIGNA EL VALOR DE UNO SI FUE CLICK EN SI
      dgvContenido.Rows(e.RowIndex).Cells("Quitar").Value = 1
      'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
      CadenaSQL = "UPDATE TPM.dbo.ComprasArticulosE SET Quitar = " + dgvContenido.Rows(e.RowIndex).Cells("Quitar").Value.ToString + " "
      CadenaSQL &= "WHERE Articulo = '" + dgvContenido.Rows(e.RowIndex).Cells("Articulo").Value.ToString + "' "
      CadenaSQL &= "AND OrdenVta = " + dgvContenido.Rows(e.RowIndex).Cells("OrdenVta").Value.ToString + " "
      'Quito el renglon del grid
      dgvContenido.Rows.Remove(dgvContenido.Rows(e.RowIndex))
      'REFRESCA EL GRID DE CONTENIDO
      Me.dgvContenido.RefreshEdit()
     Else
      dgvContenido.Rows(e.RowIndex).Cells("Quitar").Value = 0
      Me.dgvContenido.RefreshEdit()
     End If
    End If
   Else
    dgvContenido.Rows(e.RowIndex).Cells("Quitar").Value = 0
    Me.dgvContenido.RefreshEdit()
   End If

   '******************************
   'CONFIRMAR SOLICITUD
   '******************************
   If esAgente = True Then
    If Me.dgvContenido.Columns(e.ColumnIndex).Name = "ConfirmarSolicitud" Then
     'Solamente se podra modificar la confirmacion de solicitud si esta tiene fecha de entrega
     If dgvContenido.Rows(e.RowIndex).Cells("FechaEntrega").Value = "" Then
      Exit Sub
     End If

     'OBTIENE SI EL USUARIO DIO CLICK EN LA COLUMNA
     strValue = Me.dgvContenido.Item(e.ColumnIndex, e.RowIndex).Value
      If strValue = "0" Then
       'ASIGNA EL VALOR DE UNO SI FUE CLICK EN SI
       strValue = "1"
       dgvContenido.Rows(e.RowIndex).Cells("ConfirmarSolicitud").Value = 1
      Else
       strValue = "0"
       dgvContenido.Rows(e.RowIndex).Cells("ConfirmarSolicitud").Value = 0
      End If
      'REFRESCA EL GRID DE CONTENIDO
      Me.dgvContenido.RefreshEdit()
      'VARIABLES DE CONEXION A LA BASE DE DATOS

      'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
      CadenaSQL = "UPDATE TPM.dbo.ComprasArticulosE SET ConfirmarSolicitud = " + strValue + " "
      CadenaSQL &= "WHERE Articulo = '" + dgvContenido.Rows(e.RowIndex).Cells("Articulo").Value.ToString + "' "
      CadenaSQL &= "AND OrdenVta = " + dgvContenido.Rows(e.RowIndex).Cells("OrdenVta").Value.ToString + " "
     End If

     '******************************
     'CANCELAR SOLICITUD
     '******************************
     'ElseIf Me.dgvContenido.Columns(e.ColumnIndex).Name = "CancelarSolicitud" And (UsrTPM = "COMPRAS1" Or UsrTPM = "ACOMPRAS") Then
     If Me.dgvContenido.Columns(e.ColumnIndex).Name = "CancelarSolicitud" Then
     'OBTIENE SI EL USUARIO DIO CLICK EN LA COLUMNA
     strValue = Me.dgvContenido.Item(e.ColumnIndex, e.RowIndex).Value

     If (strValue = "1") Then
      MsgBox("No es posible restaurar el articulo, favor de notificar al area de Sistemas.")
     Else
      If MessageBox.Show("Realmente desea cancelar esta solicitud de la lista? ", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
       'ASIGNA EL VALOR DE UNO SI FUE CLICK EN SI
       strValue = "1"
       dgvContenido.Rows(e.RowIndex).Cells("CancelarSolicitud").Value = 1

       'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
       CadenaSQL = "UPDATE TPM.dbo.ComprasArticulosE SET CancelarSolicitud = " + strValue + " "
       CadenaSQL &= "WHERE Articulo = '" + dgvContenido.Rows(e.RowIndex).Cells("Articulo").Value.ToString + "' "
       CadenaSQL &= "AND OrdenVta = " + dgvContenido.Rows(e.RowIndex).Cells("OrdenVta").Value.ToString + " "

       'Elimino el renglon del dataview
       dgvContenido.Rows.Remove(dgvContenido.Rows(e.RowIndex))
       'REFRESCA EL GRID DE CONTENIDO
       Me.dgvContenido.RefreshEdit()
      Else
       dgvContenido.Rows(e.RowIndex).Cells("CancelarSolicitud").Value = 0
       Me.dgvContenido.RefreshEdit()
      End If
     End If
    End If
    '******************************
    'SOLICITUD CONFIRMADA
    '******************************
   ElseIf Me.dgvContenido.Columns(e.ColumnIndex).Name = "SolicitudConfirmada" And (UsrTPM = "COMPRAS1" Or UsrTPM = "CINTER") _
               And dgvContenido.Rows(e.RowIndex).Cells("ConfirmarSolicitud").Value.ToString = "1" Then
    'OBTIENE SI EL USUARIO DIO CLICK EN LA COLUMNA
    strValue = Me.dgvContenido.Item(e.ColumnIndex, e.RowIndex).Value
     If strValue = "0" Then
      'ASIGNA EL VALOR DE UNO SI FUE CLICK EN SI
      strValue = "1"
      dgvContenido.Rows(e.RowIndex).Cells("SolicitudConfirmada").Value = 1
     Else
      strValue = "0"
      dgvContenido.Rows(e.RowIndex).Cells("SolicitudConfirmada").Value = 0
     End If
     'REFRESCA EL GRID DE CONTENIDO
     Me.dgvContenido.RefreshEdit()
     'VARIABLES DE CONEXION A LA BASE DE DATOS

     'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
     CadenaSQL = "UPDATE TPM.dbo.ComprasArticulosE SET SolicitudConfirmada = " + strValue + " "
     CadenaSQL &= "WHERE Articulo = '" + dgvContenido.Rows(e.RowIndex).Cells("Articulo").Value.ToString + "' "
     CadenaSQL &= "AND OrdenVta = " + dgvContenido.Rows(e.RowIndex).Cells("OrdenVta").Value.ToString + " "
    End If

    If CadenaSQL <> "" Then
     'EJECUTA LA CONSULTA
     Try
      con.ConnectionString = StrTpm
      con.Open()
      cmd.Connection = con
      cmd.CommandText = CadenaSQL
      cmd.ExecuteNonQuery()
      Me.dgvContenido.RefreshEdit()
      'MAND A LLAMAR EL METODO DE LLENADO DEL CONTENIDO
      'MLlenaOrdenes()
     Catch ex As Exception
      MessageBox.Show("Error al actualizar el Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Return
     Finally
      con.Close()
     End Try
    End If

    Try
     dgvDetalleCompra.Columns.Clear()
     Dim row As DataGridViewRow = dgvContenido.CurrentRow()
     'Llena el datagrid detalle 
     LlenardgvDetalle(CStr(row.Cells("Articulo").Value).ToString, CStr(row.Cells("FechaCreacion").Value).ToString)
    Catch ex As Exception
     MsgBox(ex.ToString)
    End Try
   End If
 End Sub

 Private Sub dgvContenido_SelectionChanged(sender As Object, e As EventArgs) Handles dgvContenido.SelectionChanged
  'Valida el ordenamiento del grid para el cambio de posicion 
  If dgvContenido.CurrentRow Is Nothing Then
  Else
   Try
    Dim row As DataGridViewRow = dgvContenido.CurrentRow()
    'Llena el datagrid detalle 
    LlenardgvDetalle(CStr(row.Cells("Articulo").Value).ToString, CStr(row.Cells("FechaCreacion").Value).ToString)
   Catch ex As Exception
    MsgBox(ex.ToString)
   End Try
  End If
 End Sub

 Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
  'Creamos las variables
  Dim exApp As New Microsoft.Office.Interop.Excel.Application
  Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
  Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
  Try
   'Añadimos el Libro al programa, y la hoja al libro
   exLibro = exApp.Workbooks.Add
   exHoja = exLibro.Worksheets.Add()
   ' ¿Cuantas columnas y cuantas filas?
   Dim NCol As Integer = dgvContenido.ColumnCount()
   Dim NRow As Integer = dgvContenido.RowCount
   'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
   Dim Indice As Integer = 1
   For i As Integer = 1 To NCol
    If dgvContenido.Columns(i - 1).Visible = True Then
     exHoja.Cells.Item(1, Indice) = dgvContenido.Columns(i - 1).Name.ToString
     Indice = Indice + 1
    End If
    'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
   Next

   'Colores
   Dim style_LightBlue As Microsoft.Office.Interop.Excel.Style = exHoja.Application.ActiveWorkbook.Styles.Add("LightBlue")
   Dim style_GreenYellow As Microsoft.Office.Interop.Excel.Style = exHoja.Application.ActiveWorkbook.Styles.Add("GreenYellow")
   Dim style_Orange As Microsoft.Office.Interop.Excel.Style = exHoja.Application.ActiveWorkbook.Styles.Add("Orange")
   Dim style_Yellow As Microsoft.Office.Interop.Excel.Style = exHoja.Application.ActiveWorkbook.Styles.Add("Yellow")
   Dim style_Red As Microsoft.Office.Interop.Excel.Style = exHoja.Application.ActiveWorkbook.Styles.Add("Red")
   Dim style_White As Microsoft.Office.Interop.Excel.Style = exHoja.Application.ActiveWorkbook.Styles.Add("White")

   style_LightBlue.Interior.Color = ColorTranslator.ToOle(Color.LightBlue)
   style_GreenYellow.Interior.Color = ColorTranslator.ToOle(Color.GreenYellow)
   style_Orange.Interior.Color = ColorTranslator.ToOle(Color.Orange)
   style_Yellow.Interior.Color = ColorTranslator.ToOle(Color.Yellow)
   style_Red.Interior.Color = ColorTranslator.ToOle(Color.Red)
   style_White.Interior.Color = ColorTranslator.ToOle(Color.White)

   Dim Estilo As String

   For Fila As Integer = 0 To NRow - 1
    Indice = 1
    Select Case Me.dgvContenido.Rows(Fila).DefaultCellStyle.BackColor
     Case Color.LightBlue
      Estilo = "LightBlue"
     Case Color.GreenYellow
      Estilo = "GreenYellow"
     Case Color.Orange
      Estilo = "Orange"
     Case Color.Yellow
      Estilo = "Yellow"
     Case Color.Red
      Estilo = "Red"
     Case Color.White
      Estilo = "White"
    End Select

    For Col As Integer = 0 To NCol - 1
     If dgvContenido.Columns(Col).Visible = True Then
      Select Case dgvContenido.Columns(Col).Name.ToString
       Case "ConfirmarSolicitud", "CancelarSolicitud", "SolicitudConfirmada", "Quitar"
        If dgvContenido.Rows(Fila).Cells(Col).Value = 0 Then
         exHoja.Cells.Item(Fila + 2, Indice) = "NO"
        Else
         exHoja.Cells.Item(Fila + 2, Indice) = "SI"
        End If
       Case Else
        exHoja.Cells.Item(Fila + 2, Indice) = dgvContenido.Rows(Fila).Cells(Col).Value
      End Select

      'Formato de fechas
      If (dgvContenido.Columns(Col).Name.ToString = "FechaEntrega" Or dgvContenido.Columns(Col).Name.ToString = "FechaRecepcion") And dgvContenido.Rows(Fila).Cells(Col).Value.ToString <> "" Then
       exHoja.Cells(Fila + 2, Indice) = Format(CDate(dgvContenido.Rows(Fila).Cells(Col).Value), "dd/MM/yyyy").ToString
      End If

      Indice = Indice + 1
      End If
    Next
    'Poner color a la celda
    exHoja.Cells.Range("A" & (Fila + 2).ToString & ":W" & (Fila + 2).ToString).Style = Estilo
   Next
   'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
   exHoja.Rows.Item(1).Font.Bold = 1
   exHoja.Rows.Item(1).HorizontalAlignment = 3
   exHoja.Columns.AutoFit()
   'Aplicación visible
   exApp.Application.Visible = True
   exHoja = Nothing
   exLibro = Nothing
   exApp = Nothing
  Catch ex As Exception
   MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")
  End Try
 End Sub

 Private Sub dgvContenido_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvContenido.CellDoubleClick
  'VALIDA SI EXISTE UN REGISTRO EN EL GRID
  If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
   If Me.dgvContenido.Columns(e.ColumnIndex).Name = "Comentario" And (UsrTPM = "COMPRAS1" Or UsrTPM = "CINTER" Or UsrTPM = "MANAGER") Then
    Dim FormPag As New frmArticulosComentario()
    FormPag.txtUsuario.Text = dgvContenido.Rows(e.RowIndex).Cells("Usuario").Value
    FormPag.txtCliente.Text = dgvContenido.Rows(e.RowIndex).Cells("Cliente").Value
    FormPag.txtNombre.Text = dgvContenido.Rows(e.RowIndex).Cells("Nombre").Value
    FormPag.txtOrdenVta.Text = dgvContenido.Rows(e.RowIndex).Cells("OrdenVta").Value
    FormPag.txtArticulo.Text = dgvContenido.Rows(e.RowIndex).Cells("Articulo").Value
    FormPag.txtDescripcion.Text = dgvContenido.Rows(e.RowIndex).Cells("Descripcion").Value
    FormPag.txtCantidad.Text = dgvContenido.Rows(e.RowIndex).Cells("SolicitaCte").Value
    FormPag.txtComentarios.Text = IIf(dgvContenido.Rows(e.RowIndex).Cells("Comentario").Value.ToString = "", "", dgvContenido.Rows(e.RowIndex).Cells("Comentario").Value)
    FormPag.lblFechaCreacion.Text = dgvContenido.Rows(e.RowIndex).Cells("FechaCreacion").Value
    'FormPag.TxtSaldo.Text = Format(dgvContenido.Rows(e.RowIndex).Cells("SaldoPesos").Value, "$ ##,###,###,###.00")
    FormPag.lblFila.Text = e.RowIndex.ToString
    FormPag.ShowDialog()
    'MANDA A LLAMAR EL METODO DE LLENAR ORDENES
    MLlenaOrdenes()
   End If
  End If
 End Sub

 Private Sub cbQuitados_CheckedChanged(sender As Object, e As EventArgs) Handles cbQuitados.CheckedChanged
  'MANDA A LLAMAR EL METODO DE LLENADO DE ORDENES
  MLlenaOrdenes()
  'MANDA A LLAMAR EL METODO QUAITADO
  MQuitados()
  'If cbQuitados.Checked = True Then
  ' 'MANDA A LLAMAR EL METODO DE LLENADO DE ORDENES
  ' MLlenaOrdenes()
  ' 'MANDA A LLAMAR EL METODO QUAITADO
  ' MQuitados()
  'Else
  ' 'MANDA A LLAMAR EL METODO DE LLENADO DE ORDENES
  ' MLlenaOrdenes()
  ' 'MANDA A LLAMAR EL METODO QUAITADO
  ' MQuitados()
  'End If
 End Sub

 Private Sub cbFinalizadas_CheckedChanged(sender As Object, e As EventArgs) Handles cbFinalizadas.CheckedChanged
  'VALIDA SI EL CHECK DE QUITADOS ESTA EN VERDADERO
  If cbFinalizadas.Checked = True Then
   'MANDA A LLAMAR EL METODO DE LLENADO DE ORDENES
   MLlenaOrdenes()
   'MANDA A LLAMAR EL METODO QUAITADO
   MQuitados()
  Else
   'MANDA A LLAMAR EL METODO DE LLENADO DE ORDENES
   MLlenaOrdenes()
   'MANDA A LLAMAR EL METODO QUAITADO
   MQuitados()
  End If
 End Sub

 Private Sub cbDevuelto_CheckedChanged(sender As Object, e As EventArgs) Handles cbDevuelto.CheckedChanged
  'VALIDA SI EL CHECK DE QUITADOS ESTA EN VERDADERO
  If cbDevuelto.Checked = True Then
   'MANDA A LLAMAR EL METODO DE LLENADO DE ORDENES
   MLlenaOrdenes()
   'MANDA A LLAMAR EL METODO QUAITADO
   MQuitados()
  Else
   'MANDA A LLAMAR EL METODO DE LLENADO DE ORDENES
   MLlenaOrdenes()
   'MANDA A LLAMAR EL METODO QUAITADO
   MQuitados()
  End If
 End Sub

 Private Sub dgvContenido_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvContenido.RowPostPaint
  Try
   'SE DEFINEN LOS COLORES CON PALETA DE COLORES EN RGB
   azul = ColorTranslator.FromHtml("#85C1E9")
   verde = ColorTranslator.FromHtml("#2ECC71")
   rojo = ColorTranslator.FromHtml("#FFC6C6")
   amarillo = ColorTranslator.FromHtml("#F4D03F")
   naranja = ColorTranslator.FromHtml("#DC7633")
   PintarRenglon(e.RowIndex)
   'If Not IsDBNull(dgvContenido.Rows(e.RowIndex).Cells("Articulo").Value) Then
   ' If Me.dgvContenido.Rows(e.RowIndex).Cells("Facturado").Value > 0 And Me.dgvContenido.Rows(e.RowIndex).Cells("PendienteFac").Value > 0 Then
   '  Me.dgvContenido.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LemonChiffon
   ' ElseIf Me.dgvContenido.Rows(e.RowIndex).Cells("Devuelto").Value > 0 Then
   '  Me.dgvContenido.Rows(e.RowIndex).DefaultCellStyle.BackColor = amarillo
   ' ElseIf Me.dgvContenido.Rows(e.RowIndex).Cells("Facturado").Value > 0 And Me.dgvContenido.Rows(e.RowIndex).Cells("PendienteFac").Value = 0 Then
   '  Me.dgvContenido.Rows(e.RowIndex).DefaultCellStyle.BackColor = verde
   ' ElseIf Me.dgvContenido.Rows(e.RowIndex).Cells("Stock Puebla").Value > 0 And Me.dgvContenido.Rows(e.RowIndex).Cells("Facturado").Value = 0 Then
   '  Me.dgvContenido.Rows(e.RowIndex).DefaultCellStyle.BackColor = rojo
   ' ElseIf Me.dgvContenido.Rows(e.RowIndex).Cells("Stock Tuxtla").Value > 0 And Me.dgvContenido.Rows(e.RowIndex).Cells("Facturado").Value = 0 Then
   '  Me.dgvContenido.Rows(e.RowIndex).DefaultCellStyle.BackColor = rojo
   ' ElseIf Me.dgvContenido.Rows(e.RowIndex).Cells("Stock Merida").Value > 0 And Me.dgvContenido.Rows(e.RowIndex).Cells("Facturado").Value = 0 Then
   '  Me.dgvContenido.Rows(e.RowIndex).DefaultCellStyle.BackColor = rojo
   ' ElseIf Me.dgvContenido.Rows(e.RowIndex).Cells("Quitar").Value > 0 Then
   '  Me.dgvContenido.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightSkyBlue
   ' Else
   '  Me.dgvContenido.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Empty
   ' End If
   'End If
  Catch ex As Exception

  End Try

 End Sub

 Private Sub PintarRenglon(IntRenglon As Integer)
  If Not IsDBNull(dgvContenido.Rows(IntRenglon).Cells("Articulo").Value) Then
   'If Me.dgvContenido.Rows(IntRenglon).Cells("Facturado").Value > 0 And Me.dgvContenido.Rows(IntRenglon).Cells("PendienteFac").Value > 0 Then
   If Me.dgvContenido.Rows(IntRenglon).Cells("FechaCreacion").Value <= "2021-09-24" Then 'Fecha en la que quedamos validariamos
    Me.dgvContenido.Rows(IntRenglon).DefaultCellStyle.BackColor = Color.Orange
   ElseIf Me.dgvContenido.Rows(IntRenglon).Cells("Devuelto").Value > 0 Then
    Me.dgvContenido.Rows(IntRenglon).DefaultCellStyle.BackColor = Color.Red
   ElseIf Me.dgvContenido.Rows(IntRenglon).Cells("FechaRecepcion").Value <> "" Then
    Me.dgvContenido.Rows(IntRenglon).DefaultCellStyle.BackColor = Color.Yellow
    'ElseIf Me.dgvContenido.Rows(IntRenglon).Cells("ConfirmarSolicitud").Value > 0 And Me.dgvContenido.Rows(IntRenglon).Cells("SolicitudConfirmada").Value > 0 Then
   ElseIf Me.dgvContenido.Rows(IntRenglon).Cells("ConfirmarSolicitud").Value > 0 Then
    Me.dgvContenido.Rows(IntRenglon).DefaultCellStyle.BackColor = Color.GreenYellow
   ElseIf Me.dgvContenido.Rows(IntRenglon).Cells("FechaEntrega").Value <> "" Then
    Me.dgvContenido.Rows(IntRenglon).DefaultCellStyle.BackColor = Color.LightBlue
   Else
    Me.dgvContenido.Rows(IntRenglon).DefaultCellStyle.BackColor = Color.White
    End If
   End If
 End Sub

 Private Sub dgvContenido_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvContenido.CellClick
  'VALIDA SI HAY REGISTROS EN EL GRID

  Try
   If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
    If (Me.dgvContenido.Columns(e.ColumnIndex).Name = "FechaEntrega" Or Me.dgvContenido.Columns(e.ColumnIndex).Name = "FechaRecepcion") And (UsrTPM = "COMPRAS1" Or UsrTPM = "CINTER") Then
     dgvContenido.Controls.Add(DTP1)
     DTP1.Format = DateTimePickerFormat.Custom
     DTP1.CustomFormat = "dd/MM/yyyy"

     If dgvContenido.CurrentCell.Value <> "" Then
      Try
       DTP1.Value = dgvContenido.CurrentCell.Value
      Catch ex As Exception
       DTP1.Value = DateTime.Now.ToString("dd/MM/yyyy")
      End Try
     Else
      DTP1.Value = DateTime.Now.ToString("dd/MM/yyyy")
      ActualizoFecha()
     End If

     DTP1.Visible = True
     Dim displayCalendar As Rectangle = dgvContenido.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
     DTP1.Size = New Size(displayCalendar.Width, displayCalendar.Height)
     DTP1.Location = New Point(displayCalendar.X, displayCalendar.Y)
    End If
   End If
  Catch ex As Exception

  End Try
 End Sub

 Private Sub DTP1_ValueChanged(sender As Object, e As EventArgs) Handles DTP1.ValueChanged
  ActualizoFecha()
 End Sub

 Private Sub ActualizoFecha()
  'VARIABLES DE CONEXION A LA BASE DE DATOS
  Dim con As New SqlConnection
  Dim cmd As New SqlCommand
  Dim CadenaSQL As String = ""
  Dim Entra As Boolean = False
  dgvContenido.CurrentCell.Value = DTP1.Value.ToString().Substring(0, 10)

  'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
  If (Me.dgvContenido.Columns(dgvContenido.CurrentCell.ColumnIndex).Name = "FechaEntrega") And (UsrTPM = "COMPRAS1" Or UsrTPM = "CINTER") Then
   CadenaSQL = "UPDATE TPM.dbo.ComprasArticulosE SET FechaEntrega = '" + funciones.ConvertFechaNormalToSQL(dgvContenido.Rows(dgvContenido.CurrentCell.RowIndex).Cells("FechaEntrega").Value.ToString) + "' "
   Entra = True
  ElseIf (Me.dgvContenido.Columns(dgvContenido.CurrentCell.ColumnIndex).Name = "FechaRecepcion") And (UsrTPM = "COMPRAS1" Or UsrTPM = "CINTER") Then
   CadenaSQL = "UPDATE TPM.dbo.ComprasArticulosE SET FechaRecepcion = '" + funciones.ConvertFechaNormalToSQL(dgvContenido.Rows(dgvContenido.CurrentCell.RowIndex).Cells("FechaRecepcion").Value.ToString) + "' "
   Entra = True
  End If

  If Entra = True Then
   CadenaSQL &= "WHERE Articulo = '" + dgvContenido.Rows(dgvContenido.CurrentCell.RowIndex).Cells("Articulo").Value.ToString + "' "
   CadenaSQL &= "AND OrdenVta = " + dgvContenido.Rows(dgvContenido.CurrentCell.RowIndex).Cells("OrdenVta").Value.ToString + " "

   Try
    con.ConnectionString = StrTpm
    con.Open()
    cmd.Connection = con
    cmd.CommandText = CadenaSQL
    cmd.ExecuteNonQuery()
    Me.dgvContenido.RefreshEdit()
    'MAND A LLAMAR EL METODO DE LLENADO DEL CONTENIDO
   Catch ex As Exception
    MessageBox.Show("Error al actualizar el Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Return
   Finally
    con.Close()
   End Try
  End If

  DTP1.Visible = False
  PintarRenglon(dgvContenido.CurrentCell.RowIndex)
 End Sub

 Private Sub DTP1_MouseDown(sender As Object, e As MouseEventArgs) Handles DTP1.MouseDown
  If dgvContenido.CurrentRow.Index >= 0 Then
   'VARIABLES DE CONEXION A LA BASE DE DATOS
   Dim con As New SqlConnection
   Dim cmd As New SqlCommand
   Dim CadenaSQL As String = ""

   If e.Button = MouseButtons.Right Then
    dgvContenido.CurrentCell.Value = ""

    If (Me.dgvContenido.Columns(dgvContenido.CurrentCell.ColumnIndex).Name = "FechaEntrega") And (UsrTPM = "COMPRAS1" Or UsrTPM = "CINTER") Then
     'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
     CadenaSQL = "UPDATE TPM.dbo.ComprasArticulosE SET FechaEntrega = NULL"
    ElseIf (Me.dgvContenido.Columns(dgvContenido.CurrentCell.ColumnIndex).Name = "FechaRecepcion") And (UsrTPM = "COMPRAS1" Or UsrTPM = "CINTER") Then
     CadenaSQL = "UPDATE TPM.dbo.ComprasArticulosE SET FechaRecepcion = NULL"
    End If

    CadenaSQL &= " WHERE Articulo = '" + dgvContenido.Rows(dgvContenido.CurrentCell.RowIndex).Cells("Articulo").Value.ToString + "' "
    CadenaSQL &= " AND OrdenVta = " + dgvContenido.Rows(dgvContenido.CurrentCell.RowIndex).Cells("OrdenVta").Value.ToString + " "

    Try
     con.ConnectionString = StrTpm
     con.Open()
     cmd.Connection = con
     cmd.CommandText = CadenaSQL
     cmd.ExecuteNonQuery()
     Me.dgvContenido.RefreshEdit()
     'MAND A LLAMAR EL METODO DE LLENADO DEL CONTENIDO
    Catch ex As Exception
     MessageBox.Show("Error al actualizar el Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
     Return
    Finally
     con.Close()
    End Try

    DTP1.Visible = False
   End If
  End If
 End Sub

 Private Sub cbCancelados_CheckedChanged(sender As Object, e As EventArgs) Handles cbCancelados.CheckedChanged
  'MANDA A LLAMAR EL METODO DE LLENADO DE ORDENES
  MLlenaOrdenes()
  'MANDA A LLAMAR EL METODO QUAITADO
  MQuitados()
 End Sub
End Class