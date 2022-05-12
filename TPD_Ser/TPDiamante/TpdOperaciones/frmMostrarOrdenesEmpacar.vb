Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmMostrarOrdenesEmpacar
  Dim DsOrdenes As DataSet 'DECLARA DATASET PARA PROCEDIMIENTO DE INSERTAR ORDENES
  Dim ResOrdPed As DataView = New DataView 'DECLARA DATAVIEW PARA MOSTRAR EL RESULTADO EN LOS GRID
  Dim DetailOrdPed As DataView 'DECLARA DATAVIEW PARA MOSTRAR EL RESULTADO EN LOS GRID
  Dim fi As String 'VARIABLE PARA OBTENER LA FECHA DEL DATAPICKER
  Dim Resultado As DataView 'VARIABLE DE DATAVIEW PARA LLENAR EL GRID DE ORDENES
  Dim Dia As String 'VARIABLE QUE ALMACENA EL DIA DEL DATAPICKER PARA SABER HORARIO DE PAQUETERIA

  'DECLARACION DE VARIABLE DE REPORTE Y INSTANCIA DEL MISMO
  Dim DocOrdenes As ReportDocument = New ReportDocument()
  'VARIBALE PARA EL PASO DE PARAMETROS DEL CRYSTAL
  Dim DocKey = String.Empty
  Dim ObjectType = String.Empty
  Dim _rutaPDF As String '// ALMACENA LA RUTA DEL PDF

  Private Sub frmMostrarOrdenesEmpacar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    'MANDA LLAMAR EL MÉTODO DE ESTILO DE GRID DEL DETALLE DE LAS ORDENES
    MestiloGridDetalle()
    'MANADA LLAMAR EL MÉTODO DE LLENADO DE LAS ORDENES
    MLlenaOrdenes()


  End Sub

  'METODOS
#Region "Metodos"
  'METODO DE ESTILO DEL GRID DETALLE
  Sub MestiloGridDetalle()
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
      .Columns("Partida").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Partida").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
      .Columns("Partida").DefaultCellStyle.ForeColor = Color.Red
      'ARTICULO
      .Columns("ItemCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns("ItemCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      'DESCRIPTION
      .Columns("Description").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns("Description").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      'CANTIDAD
      .Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Quantity").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
      'SURTIDO (REAL)
      .Columns("Surtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Surtido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
      .Columns("Surtido").DefaultCellStyle.ForeColor = Color.Blue
    End With
  End Sub

  'METODO DE ESTILO DEL GRID ORDENES
  Sub MEstiloGridOrdenes()

    'ESTILOS POR COLUMNA
    With Me.dgvOrdenes
      'COLOCA PROPIEDADES DE COLOR ALTERNADOS
      .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionForeColor = Color.White
      .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      'ORDEN DE VENTA
      .Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("DocNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
      .Columns("DocNum").DefaultCellStyle.ForeColor = Color.Red
      .Columns("DocNum").HeaderText = "Orden Vta."
      .Columns("DocNum").Width = 70
      .Columns("DocNum").ReadOnly = True
      'FECHA DE ORDEN
      .Columns("DocDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("DocDate").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("DocDate").HeaderText = "Fecha Doc."
      .Columns("DocDate").Width = 85
      .Columns("DocDate").ReadOnly = True
      'HORA DE IMPRESION
      .Columns("PrintTime").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("PrintTime").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("PrintTime").HeaderText = "Hora impresión"
      .Columns("PrintTime").Width = 65
      .Columns("PrintTime").ReadOnly = True
      'PARTIDAS POR ORDEN
      .Columns("LineNumTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("LineNumTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
      .Columns("LineNumTotal").HeaderText = "Partidas"
      .Columns("LineNumTotal").Width = 50
      .Columns("LineNumTotal").ReadOnly = True
      'PAQUETERIA
      .Columns("TrnspName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns("TrnspName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("TrnspName").HeaderText = "Paquereria"
      .Columns("TrnspName").Width = 130
      .Columns("TrnspName").ReadOnly = True
      'HORARIO DE PAQUEREIAS
      .Columns("TrnspCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("TrnspCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("TrnspCode").HeaderText = "Horario paq."
      .Columns("TrnspCode").Width = 90
      .Columns("TrnspCode").ReadOnly = True
      'CODIGO CLIENTE
      .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns("CardCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("CardCode").HeaderText = "Cliente"
      .Columns("CardCode").Width = 70
      .Columns("CardCode").ReadOnly = True
      'NOMBRE CLIENTE
      .Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns("CardName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("CardName").HeaderText = "Nombre"
      .Columns("CardName").Width = 150
      .Columns("CardName").ReadOnly = True
      'COMENTARIOS
      .Columns("Comment").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns("Comment").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Comment").HeaderText = "Comentario SAP"
      .Columns("Comment").Width = 150
      .Columns("Comment").ReadOnly = True
      'IMPRIMIR PACKLIST
      .Columns("Printed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Printed").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Printed").HeaderText = "Impresión pedido"
      .Columns("Printed").Width = 50
      .Columns("Printed").ReadOnly = True
      .Columns("Printed").Visible = False
      'PERSONAL SURTIDOR Y ALMACENISTA
      .Columns("Pump").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Pump").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Pump").HeaderText = "Surtidor"
      .Columns("Pump").Width = 50
      .Columns("Pump").ReadOnly = True
      .Columns("Pump").Visible = False
    End With
  End Sub

  'METODO DE LLENADO DE ORDENES
  Sub MLlenaOrdenes()
    'VARIABLE DE CADENA DE SQL
    Dim SQLOrdenes As String
    'VARIABLE QUE ALMACENA LA FECHA DEL GRID
    Dim Date_grid As Date
    'VARIABLES DE CONEXION DE LLENADO
    Dim cmd As SqlCommand
    Dim cnn As SqlConnection = Nothing
    Dim da As SqlDataAdapter
    Dim DsOrdenes = New DataSet

    'OBTIENE LA FECHA INICIAL Y LA FINAL
    fi = dtpFecha.Value.ToString("yyyy-MM-dd")
    'REFRESCA EL DATA GRID VIEW DE RESULTADO Y EL DETALLE
    If dgvDetalle.RowCount > 0 Then
      dgvDetalle.Rows.Clear()
    End If
    If dgvOrdenes.RowCount > 0 Then
      'dgvOrdenes.Rows.Clear()
      dgvOrdenes.Columns().Remove("Imprimir")
      dgvOrdenes.DataSource = Nothing
    End If

    'ALAMACENA LA CONSULTA
    SQLOrdenes = "SELECT DISTINCT(T0.DocNum) AS DocNum, FORMAT(T0.DocDate, 'yyyy-MM-dd') AS DocDate, CONVERT(char(8), T0.PrintTime, 108) AS PrintTime, "
    SQLOrdenes &= "T0.LineNumTotal AS LineNumTotal, T0.TrnspName, T0.TrnspCode AS TrnspCode, T0.CardCode, T0.CardName, "
    SQLOrdenes &= "T0.Comment, CASE WHEN T0.Status = 'E' THEN T1.StatusName2 ELSE 'Empacar' END AS Accion, T0.Pump, '' AS Printed "
    SQLOrdenes &= "FROM Operacion_Orden T0 INNER JOIN Operacion_Status T1 ON T0.Status = T1.Status "
    SQLOrdenes &= "WHERE T0.DocDate BETWEEN DATEADD(DAY, -20, '" + fi + "') AND '" + fi + "' AND (T0.Status = 'ST' OR T0.Status = 'C') "
    SQLOrdenes &= "ORDER BY T0.DocNum DESC "

    Try
      'RELAIZA LA CONEXION
      cnn = New SqlConnection(StrTpm)
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
      Resultado = New DataView
      'ALMACENA EN DATA SET DE MODO TABLA
      Resultado.Table = DsOrdenes.Tables(0)
      'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
      dgvOrdenes.DataSource = Nothing
      'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
      dgvOrdenes.DataSource = Resultado
      'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES
      MEstiloGridOrdenes()

      '----

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
      dgvOrdenes.Columns.Insert(9, col)

      '----
      'ELIMINA LA COLUMNA IMPRIMIR DEL DATA GRID TRAIDA DEL DATATABLE
      'dgvOrdenes.Columns().Remove("Printed")
      'CREA INSTANCIA DE COLUMNA CON IMAGEN Y DA FORMATO
      Dim col1 As New DataGridViewButtonColumn
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
    'VARIABLE DE CADENA DE SQL
    Dim SQLDetalle As String
    'ALMACENA CONTADOR DE LINEAS
    Dim Line As Integer = 0
    'REFRESCA EL DATA GRID VIEW DE RESULTADO
    If dgvDetalle.RowCount > 0 Then
      dgvDetalle.Rows.Clear()
    End If
    Try 'CAPTURA EL ERROR DE CONSULTA O CONEXION DE LA BASE DE DATOS DEL TPD
      'CONECTA A LA BASE DE DATOS
      conexion_universal.conectar()

      'ALAMACENA LA CONSULTA
      SQLDetalle = "SELECT T0.LineNum, T0.ItemCode, T0.Description, T0.Quantity, T0.Surtido "
      SQLDetalle &= "FROM Operacion_Detalle T0 INNER JOIN Operacion_Orden T1 ON T0.DocNum = T1.DocNum "
      SQLDetalle &= "WHERE T1.DocNum = " + DocNum.ToString + " "
      SQLDetalle &= "AND T0.ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS NOT IN(select ItemCode from SBO_TPD.dbo.OITM WHERE ItmsGrpCod = 150) "
      SQLDetalle &= "ORDER BY T0.DocNum DESC "

      'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
      conexion_universal.slq_s = New SqlCommand(SQLDetalle, conexion_universal.conexion_uni)
      'EJECUTA LA CONSULTA
      conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
      'RECORRE LA CONSULTA
      While conexion_universal.rd_s.Read
        Line = Line + 1
        If dgvDetalle.RowCount > 0 Then
          'MANDA LOS RESULTADOS
          Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            'rd_s.Item("LineNum")
            Me.dgvDetalle.Rows.Add(Line, rd_s.Item("ItemCode").ToString, rd_s.Item("Description"), CInt(rd_s.Item("Quantity")).ToString,
                        CInt(rd_s.Item("Surtido")).ToString)
            'RECORRE EL DATA GRID VIEW
            With dgvDetalle
              'ESTABLECE LA CELDA ACTUAL
              .CurrentCell = .Rows(Me.dgvDetalle.Rows.Count - 1).Cells(0)
            End With
          Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            'MANDA EL MENSAJE DE ERROR
            MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
            Return
          End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
        Else
          'MANDA LOS RESULTADOS
          Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            Me.dgvDetalle.Rows.Add(Line, rd_s.Item("ItemCode").ToString, rd_s.Item("Description"), CInt(rd_s.Item("Quantity")).ToString,
                        CInt(rd_s.Item("Surtido")).ToString)
            'RECORRE EL DATA GRID VIEW
            With dgvDetalle
              'ESTABLECE LA CELDA ACTUAL
              .CurrentCell = .Rows(Me.dgvDetalle.Rows.Count - 1).Cells(0)
            End With
          Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            'MANDA EL MENSAJE DE ERROR
            MsgBox("Error al agregar el resultado en Ordenes : " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
            Return
          End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
        End If
      End While
      conexion_universal.cerrar_conectar()
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
      DocOrdenes.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
      'INDICA LA PREFERENCIA DE LA IMPRESORA
      DocOrdenes.PrintToPrinter(1, False, 0, 0)
    Catch ex As Exception
      'VALIDA SI EL USUARIO ES MANAGER QUE PERMITA VER EL ERROR QUE PRESENTA
      If (UsrTPM = "MANAGER") Then
        MessageBox.Show("No se pudo crear el archivo PDF de la Orden de Venta: " + ex.ToString(), "Error en PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Return
      Else
        MessageBox.Show("Documento sin imprimir.", "Alerta de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
      SQLUpdateImp = "SELECT IIF(Printed IS NULL, 0, Printed) AS Printed FROM Operacion_Orden WHERE DocNum = '" + Pedido + "' "

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
      SQLUpdateImp = "UPDATE Operacion_Orden SET Printed = '" + TotalPrint.ToString + "' "
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

  'EVENTOS
#Region "Eventos"

  'OBTIENE EL VALOR DE UNA CELADA AL DARLE CLICK EN EL DATA GRID
  Private Sub dgvOrdenes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrdenes.CellClick
    'OBTIENE EL VALOR DE LA CELDA DE DOCNUM QUE ES LA ORDEN DE PEDIDO
    Dim Num As Integer = CInt(Convert.ToString(dgvOrdenes.CurrentRow.Cells("DocNum").Value))
    'MANDA A LLAMAR EL METODO DE LLENADO DEL DATA GRID DETALLE PARA MOSTRAR LAS PARTIDAS DE LA ORDEN SELECCIONADA.
    MLlenaOrdenesDetalle(Num)
  End Sub

  'BUSCA LA ORDEN QUE SE REQUIERA
  Private Sub txtBuscarOrden_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarOrden.TextChanged
    'MANDA A LLLAMAR EL METODO DE BUSQUEDA DE ORDEN
    MBuscarOrdenes()
  End Sub

  'AL SELECCIONAR UNA FILA SE EJECUTA PARA MOSTRAR EL DETALLE
  Private Sub dgvOrdenes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvOrdenes.SelectionChanged
    'VALIDA SI EL ORDENAMIENTO VIENE EN 0
    If (dgvOrdenes.CurrentRow Is Nothing) Then
      'NO HACE NADA
    Else
      'OBTIENE EL VALOR DE LA CELDA DE DOCNUM QUE ES LA ORDEN DE PEDIDO
      Dim Num As Integer = CInt(Convert.ToString(dgvOrdenes.CurrentRow.Cells("DocNum").Value))
      'MANDA A EJECUTARSE EL METODO DE MOSTRAR DETALLE
      MLlenaOrdenesDetalle(Num)
    End If
  End Sub

  'ESTE ENVENTO SE ACTIVA AL HACER CLICK EN CUALQUIER CELDA
  Private Sub dgvOrdenes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOrdenes.CellContentClick
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
                  MsgBox("Acceso cancelado, la Orden de Venta no comenzará a surtirse.", MsgBoxStyle.Exclamation, "Acceso cancelado")
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
        MImprimeFormato()
      End If

      '-----

    End If
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
          Using frmA As New frmAcceso()
            'MANDA MOSTRAR EL FORMULARIO DE ACCESO
            Dim dr As DialogResult = frmA.ShowDialog(Me)
            'SI SE PRESIONA EL BOTON CERRAR DEL FORMULARIO DE ACCESO, NO SE HACE NINGUNA FUNCION
            If (dr = Windows.Forms.DialogResult.Cancel) Then
              'VALIDA SI SE CERRO POR CANCELAR O POR CAMBIO DE FORMULARIO DE SURTIDO
              If CierraDialogAcceso = False Then
                'MANDA MENSAJE DE QUE NO SE VA SURTIR LA ORDEN DE VENTA.
                MsgBox("Acceso cancelado, la Orden de Venta no comenzará a surtirse.", MsgBoxStyle.Exclamation, "Acceso cancelado")
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
                'MANDA A LLAMAR EL MÉTODO DE MOSTRAR LAS ORDENES DE VENTA DEL DIA
                'MEjecuta_Orden_Ped_Det()
                'MANDA A LLAMAR EL MÉTODO DE MOSTRAR EN EL GRID LAS ORDENES DE VENTA DEL DIA
                MLlenaOrdenes()
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

  'DA FORMATO DE COLOR ANTES DE HACER EL ESTILO AL GRID DE ORDENES
  Private Sub dgvOrdenes_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvOrdenes.RowPrePaint
    Try
      'COLOCA COLORES A LAS FILAS PARA DIFERENCIAR EL SURTIDO, SURTIENDO Y PENDIENTE EN DADO CASO
      For i As Integer = 0 To dgvOrdenes.Rows.Count - 1
        'COMPARA EL ESTATUS
        If dgvOrdenes.Rows(i).Cells("Accion").Value.ToString = "Surtir" Then 'SE DEJA POR SI SE OCUPA
          'COLOCA COLOR A LA CELDA U LA LETRA
          'dgvOrdenes.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
          'dgvOrdenes.Rows(i).DefaultCellStyle.ForeColor = Color.White
          'dgvOrdenes.Rows(i).DefaultCellStyle.BackColor = Color.Cornsilk
        ElseIf dgvOrdenes.Rows(i).Cells("Accion").Value.ToString = "Surtiendo" Then
          'COLOCA COLOR A LA CELDA U LA LETRA
          dgvOrdenes.Rows(i).DefaultCellStyle.BackColor = Color.Khaki
          dgvOrdenes.Rows(i).DefaultCellStyle.ForeColor = Color.Black

        ElseIf dgvOrdenes.Rows(i).Cells("Accion").Value.ToString = "En Espera" Then
          'COLOCA COLOR A LA CELDA U LA LETRA
          dgvOrdenes.Rows(i).DefaultCellStyle.BackColor = Color.Yellow
          dgvOrdenes.Rows(i).DefaultCellStyle.ForeColor = Color.Black
        End If
      Next
    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try
  End Sub

  'INICIO EVENTO DE TIMER
  Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    'MANDA A LLAMAR EL MÉTODO DE INSERTAR LAS ORDENES DE VENTA DEL DÍA
    'MEjecuta_Orden()
    'MANDA A LLAMAR EL MÉTODO DE MOSTRAR LAS ORDENES DE VENTA DEL DIA
    'MEjecuta_Orden_Ped()
    'MANDA A LLAMAR EL MÉTODO DE MOSTRAR EN EL GRID LAS ORDENES DE VENTA DEL DIA
    MLlenaOrdenes()
  End Sub


#End Region

  'BOTONES
#Region "Botones"

#End Region



End Class