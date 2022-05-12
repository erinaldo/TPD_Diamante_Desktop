Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
'LIBRERIA REQUERIDA PARA CARGAR EL CRYSTAL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmEstatusEmpaque
#Region "Declaracion de Variables"

 Dim ResultadoEmpaque As DataView
 Dim ResultadoDetalleEmpaque As DataView
 Dim DvDetalle_Estatus As DataView
 Dim Resultadopaking As DataView
 Dim DsOrdenes = New DataSet
 Dim Dia As String
 Dim LinkAction As DataGridViewLinkColumn = New DataGridViewLinkColumn()

 'Colores creados para uso en el grid DGVoperacionVta
 Dim rojo As Color
 Dim amarillo As Color
 Dim verde As Color
 Dim Anaranjado As Color
 'VARIABLES PARA BASE DE DATOS
 Dim BaseDatosTPD As String = "TPM"
 Dim BaseDatosSAP As String = "SBO_TPD"
 Dim BaseDatosTPDPru As String = "TPM"
 Dim BaseDatosSAPPru As String = "ZPRUEBAS25JUN19"

 'DECLARACION DE VARIABLE DE REPORTE Y INSTANCIA DEL MISMO
 Dim DocOrdenes As ReportDocument = New ReportDocument()
 'VARIBALE PARA EL PASO DE PARAMETROS DEL CRYSTAL
 Dim DocKey = String.Empty
 Dim ObjectType = String.Empty
 Dim _rutaPDF As String '// ALMACENA LA RUTA DEL PDF

    Dim DocEntry As Integer
    Dim DocNumOV As Integer


    Dim DocEntry_P As Integer
 Dim bandera As Boolean = True
 Dim SQL As New Comandos_SQL()
 Dim thread As New Thread(AddressOf MyBackgroundThread)

 Dim CurrentRow As Integer
 Dim CurrentCol As Integer
#End Region
#Region "Metodos"
 Sub MyBackgroundThread()
  Try
   If dgvEmpaque.Rows.Count > 0 Then
    CurrentRow = dgvEmpaque.CurrentCell.RowIndex
    CurrentCol = dgvEmpaque.CurrentCell.ColumnIndex
   Else
    CurrentRow = 0
    CurrentCol = 0
   End If
   MEjecuta_Full_Empaque()
   LlenarEmpaque()
   If CurrentRow > dgvEmpaque.Rows.Count Then
    CurrentRow = dgvEmpaque.Rows.Count
   ElseIf (dgvEmpaque.Rows.Count > 0) Then
    dgvEmpaque.CurrentCell = dgvEmpaque(CurrentCol, CurrentRow)
   End If
   dgvEmpaqueDetalle.DataSource = Nothing
   DocEntry_P = 0
   Console.WriteLine("Se actualizo el grid: " + DateTime.Now)
  Catch ex As Exception

  End Try
 End Sub

 'NUEVO METODO SE OPTIMIZO EL TIEMPO DE RESPUESTA
 'MODIFICADO POR IVAN GONZALEZ
 Dim contadorEmpaque As Integer = 1

 Sub LlenarEmpaque()
  Console.WriteLine("Se actualizo la tabla de Empaque: " + contadorEmpaque.ToString())
  'ELIMINAR LA COLUMNA IMPRIMIR Y LIMPIAR EL GRID
  If dgvEmpaque.RowCount <> 0 Then
   dgvEmpaque.Columns().Remove("Imprimir")
   dgvEmpaque.DataSource = Nothing
  End If
  'ASIGNAR EL RESULTADO DEL PROCEDIMIENTO AL DATASOURCE
  Dim tbBandera As New DataTable
  tbBandera = SQL.EjecutarProcedimiento("TPD_Estatus_Empaque", "", 0, "")
  If tbBandera.Rows.Count <> 0 Then
   dgvEmpaque.DataSource = SQL.EjecutarProcedimiento("TPD_Estatus_Empaque", "", 0, "")
   If dgvEmpaque.Rows.Count <> 0 Then
    dgvEmpaque.Columns().Remove("Accion")
    Dim col As New DataGridViewLinkColumn
    col.DataPropertyName = "Accion"
    col.HeaderText = "Acción"
    col.Name = "Accion"
    col.SortMode = DataGridViewColumnSortMode.Automatic
    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    col.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
    col.Width = 75
    col.ReadOnly = True
    dgvEmpaque.Columns.Insert(11, col)

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
    dgvEmpaque.Columns.Insert(12, col1)

    EstiloEmpaque()

   Else
    dgvEmpaque.DataSource = Nothing
   End If
  End If

 End Sub

 'ESTE METODO YA NO SE OCUPARA
 'SE BORRARA CUANDO SE TERMINEN DE HACER PRUEBAS
 Sub LlenarEmpaqueOriginal()
  'llena a travez de una consulta de sql el gridview detalle
  Try
   'VARIABLES DE CONEXION DE LLENADO
   Dim cmd As SqlCommand
   Dim cnn As SqlConnection = Nothing
   Dim da As SqlDataAdapter
   Dim DsOrdenes = New DataSet
   If dgvEmpaque.RowCount > 0 Then
    'dgvOrdenes.Rows.Clear()
    dgvEmpaque.Columns().Remove("Imprimir")
    dgvEmpaque.DataSource = Nothing
   End If
   cnn = New SqlConnection(StrTpm)
   'ALMACENA LA CONSULTA EN UN COMMAND SQL
   'cmd = New SqlCommand("EstatusEmpaque", cnn)
   cmd = New SqlCommand("TPD_Estatus_Empaque", cnn)
   'cmd = New SqlCommand("EstatusEmpaque", cnn)
   'Pasa los parametros del procedimiento almacenado 
   cmd.CommandType = CommandType.StoredProcedure
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
   ResultadoEmpaque = New DataView
   'ALMACENA EN DATA SET DE MODO TABLA
   ResultadoEmpaque.Table = DsOrdenes.Tables(0)
   'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
   dgvEmpaque.DataSource = Nothing
   'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
   dgvEmpaque.DataSource = ResultadoEmpaque
   'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE DETALLE ORDENES
   ''EstiloDetalleOperacionVta()
   'VALIDA SI EL DATATABLE TRAE DATOS
   If (ResultadoEmpaque.Count > 0) Then
    'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
    dgvEmpaque.DataSource = Nothing
    'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
    dgvEmpaque.DataSource = ResultadoEmpaque
    'CREA INSTANCIA DE COLUMNA CON LINK Y DA FORMATO EN ACCION
    dgvEmpaque.Columns().Remove("Accion")
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
    dgvEmpaque.Columns.Insert(11, col)
    'ELIMINA LA COLUMNA IMPRIMIR DEL DATA GRID TRAIDA DEL DATATABLE
    'dgvOrdenes.Columns().Remove("Printed")
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
    dgvEmpaque.Columns.Insert(12, col1)
    'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES
    EstiloEmpaque()
    '----
    'ELIMINA LA COLUMNA ACTION DEL DATA GRID TRAIDA DEL DATATABLE
    'dgvEmpaque.Columns().Remove("Accion")
   End If
  Catch ex As Exception
   MsgBox("Error: " + ex.ToString)
  End Try
  'Dim Date_grid As Date
  'RECORRE EL GRID DE ORDENES PARA REEMPLAZAR LOS HORARIOS DE PAQUETERIA
  'For Each row As DataGridViewRow In dgvEmpaque.Rows
  '    'CONVIERTE EL DATO DE FECHA DEL GRID A UNA FECHA REAL
  '    'Date_grid = Convert.ToDateTime(row.Cells("DocDate").Value)
  '    'OBTIENE SOLO EL DIA DE LA FECHA
  '    Dia = Date_grid.ToString("dddd")
  '    'VALIDA SI EL DIA ES SABADO
  '    If Dia = "sábado" Then
  '        'CAMBIA EL NOMBRE DE LA PAQUETERIA CORRESPONDIENTE A LOS VALORES ESTABLECIDOS
  '        If row.Cells("TrnspCode").Value.ToString = "43" Or row.Cells("TrnspCode").Value.ToString = "44" Then 'PAQUETERIA LOGEX'
  '            row.Cells("TrnspCode").Value = "13:00"
  '        ElseIf row.Cells("TrnspCode").Value.ToString = "9" Or row.Cells("TrnspCode").Value.ToString = "10" Then 'PAQUETERIA ESTAFETA'
  '            row.Cells("TrnspCode").Value = "13:20 - 14:00"
  '        ElseIf row.Cells("TrnspCode").Value.ToString = "20" Or row.Cells("TrnspCode").Value.ToString = "21" Then 'PAQUETERIA PAQUETE EXPRESS'
  '            row.Cells("TrnspCode").Value = "12:30 - 13:30"
  '        ElseIf row.Cells("TrnspCode").Value.ToString = "28" Or row.Cells("TrnspCode").Value.ToString = "29" Then 'PAQUETERIA TRES GUERRAS
  '            row.Cells("TrnspCode").Value = "11:15 - 11:30"
  '        Else
  '            If row.Cells("TrnspCode").Value.ToString <> "" Then
  '                row.Cells("TrnspCode").Value = "11:15 - 11:30"
  '            End If
  '        End If
  '    Else
  '        'DE NO SER SBADO ENTRA PARA HORARIOS DE ENTRE SEMANA
  '        If row.Cells("TrnspCode").Value.ToString = "43" Or row.Cells("TrnspCode").Value.ToString = "44" Then 'PAQUETERIA LOGEX'
  '            row.Cells("TrnspCode").Value = "18:00"
  '        ElseIf row.Cells("TrnspCode").Value.ToString = "9" Or row.Cells("TrnspCode").Value.ToString = "10" Then 'PAQUETERIA ESTAFETA'
  '            row.Cells("TrnspCode").Value = "18:20 - 19:00"
  '        ElseIf row.Cells("TrnspCode").Value.ToString = "20" Or row.Cells("TrnspCode").Value.ToString = "21" Then 'PAQUETERIA PAQUETE EXPRESS'
  '            row.Cells("TrnspCode").Value = "17:30 - 18:30"
  '        ElseIf row.Cells("TrnspCode").Value.ToString = "28" Or row.Cells("TrnspCode").Value.ToString = "29" Then 'PAQUETERIA TRES GUERRAS'
  '            row.Cells("TrnspCode").Value = "15:00"
  '        Else
  '            If row.Cells("TrnspCode").Value.ToString <> "" Then
  '                row.Cells("TrnspCode").Value = "16:15 - 16:30"
  '            End If
  '        End If
  '    End If 'FIN VALIDA SI EL DIA ES SABADO
  'Next
  ''FIN RECORRE EL GRID DE ORDENES PARA REEMPLAZAR LOS HORARIOS DE PAQUETERIA
 End Sub

 Sub LlenarDetalleEmpaque(ORDEN As String, entrega As String)
  'llena a travez de una consulta de sql el gridview detalle
  Try
   'MODIFICO IVAN GONZALEZ
   'SE OPTIMIZO LA CONSULTA CON PROCEDIMIENTO ALMACENADO
   Dim SQL As New Comandos_SQL()
   If DocEntry_P <> entrega Then
    dgvEmpaqueDetalle.DataSource = SQL.EjecutarProcedimiento("TPD_Detalle_Empaque", "@DocEntry", 1, entrega)
    'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE DETALLE ORDENES
    EstilDetalleEmpaque()
    DocEntry_P = DocEntry
   Else
    'Dim row As DataGridViewRow = dgvEmpaque.CurrentRow()
    'dgvEmpaqueDetalle.DataSource = SQL.EjecutarProcedimiento("TPD_Detalle_Empaque", "@DocEntry", 1, row.Cells("OrdenEmpaque").Value)
    ''MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE DETALLE ORDENES
    'EstilDetalleEmpaque()
    'DocEntry_P = row.Cells("OrdenEmpaque").Value
   End If

  Catch ex As Exception
   MsgBox("Error: " + ex.ToString)
  End Try
 End Sub


 Sub MImprimeFormatoPAKING(paking As String)
  Try
   'VARIABLE DE CADENA DE SQL
   Dim SQLOrdenes As String
   'VARIABLES DE CONEXION DE LLENADO
   Dim cmd As SqlCommand
   Dim cnn As SqlConnection = Nothing
   Dim DsOrdenes = New DataSet
   Dim da As SqlDataAdapter
   'ALAMACENA LA CONSULTA
   SQLOrdenes = "Select Distinct ItmsGrpNam ,T5.DocNum,t5.CardCode,T5.CardName,T5.DocDate,t5.LicTradNum , t5.Address, SlpName, T1.BaseRef, "
   SQLOrdenes &= "T1.ItemCode AS Codigo,Dscription AS Descripcion,(Select Surtido From  TPM.dbo.Operacion_Detalle_Entrega where T1.VisOrder=LineNum and DocEntry=t2.DocEntry )AS Cantidad, "
   SQLOrdenes &= "Round(t3.SWeight1,2,0) as PesoxUni ,ROUND(((Select Surtido From  TPM.dbo.Operacion_Detalle_Entrega where T1.VisOrder=LineNum and DocEntry=t2.DocEntry )*t3.SWeight1),2,1)as PesoxArt,t7.TrnspName  "
   SQLOrdenes &= " ,((Select Sum(Surtido) From  TPM.dbo.Operacion_Detalle_Entrega where DocEntry=t2.DocEntry)*(Select Sum(TT2.SWeight1) from DLN1 TT1 INNER JOIN OITM TT2 ON TT1.ItemCode=TT2.ItemCode WHERE TT1.DocEntry=T1.DocEntry)) AS PESO "
   SQLOrdenes &= ",(Select Name From TPM.dbo.Operacion_Empleado TT1 INNER JOIN TPM.DBO.Operacion_Entrega TT2 on TT2.UserId_Surtido =TT1.KeyCode where tt2.DocEntry=" + paking + ") AS Surtidor "
   SQLOrdenes &= ",(Select Name From TPM.dbo.Operacion_Empleado TT1 INNER JOIN TPM.DBO.Operacion_Entrega TT2 on TT2.UserId_Empacado =TT1.KeyCode where tt2.DocEntry=" + paking + ") AS Empacador "
   SQLOrdenes &= "  ,T9.SurtidoBox as Surtido_Caja,BoxNum as Caja "
   SQLOrdenes &= " From DLN1 T1 LEFT JOIN TPM.dbo.Operacion_Detalle_Entrega T2 ON T1.DocEntry=T2.DocEntry and t1.baseRef =T2.DocNum "
   SQLOrdenes &= "LEFT JOIN OITM T3 on T1.ItemCode=T3.ItemCode   LEFT JOIN OITB T4 on T3.ItmsGrpCod=T4.ItmsGrpCod  LEFT JOIN ODLN t5 ON T1.DocEntry=T5.DocEntry  "
   SQLOrdenes &= " LEFT JOIN OSLP T6 ON T1.SlpCode=T6.SlpCode LEFT JOIN OSHP T7 ON T7.TrnspCode=t5.TrnspCode "
   SQLOrdenes &= "  LEFT Join TPM.dbo.Operacion_Entrega  T8 ON T8.DocEntry=T2.DocEntry AND T8.DocNum=T2.DocNum "
   SQLOrdenes &= "INNER JOIN TPM.DBO.Operacion_Paking_List T9 ON T1.DocEntry=T9.DocEntry AND T1.LineNum=T9.LineNum "
   SQLOrdenes &= " WHERE  T2.Docentry=" + paking + " AND T4.ItmsGrpCod<>150 Order By ItmsGrpNam ,T1.ItemCode"
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
   Resultadopaking = New DataView
   'ALMACENA EN DATA SET DE MODO TABLA
   Resultadopaking.Table = DsOrdenes.Tables(0)
   'Crea un nuevo DataView         
   DvDetalle_Estatus = New DataView
   'ALMACENA EN DATA SET DE MODO TABLA
   DvDetalle_Estatus.Table = DsOrdenes.Tables(0)
   'Se crea un informe de cristal Reports
   Dim informe As New CR_Paking_List
   RepComsultaP.MdiParent = Inicio
   informe.SetDataSource(DvDetalle_Estatus)
   RepComsultaP.CrVConsulta.ReportSource = informe
   informe.PrintToPrinter(1, False, 0, 0)
  Catch ex As Exception
   MsgBox("Error: " + ex.ToString)
  End Try
 End Sub



 Sub MImprimeFormato(entrega As String)
  Try

   ''VARIABLE DE CADENA DE SQL
   'Dim SQLOrdenes As String
   ''VARIABLES DE CONEXION DE LLENADO
   'Dim cmd As SqlCommand
   'Dim cnn As SqlConnection = Nothing
   'Dim DsOrdenes = New DataSet
   'Dim da As SqlDataAdapter
   ''ALAMACENA LA CONSULTA
   'SQLOrdenes = "Select ItmsGrpNam ,T5.DocNum,t5.CardCode,T5.CardName,T5.DocDate,t5.LicTradNum , t5.Address, SlpName, T1.BaseRef, "
   'SQLOrdenes &= "T1.ItemCode AS Codigo,Dscription AS Descripcion,(Select distinct Surtido From  TPM.dbo.Operacion_Detalle_Entrega where T1.VisOrder=LineNum and DocEntry=t2.DocEntry )AS Cantidad, "
   'SQLOrdenes &= "Round(t3.SWeight1,2,0) as PesoxUni ,ROUND(((Select distinct Surtido From  TPM.dbo.Operacion_Detalle_Entrega where T1.VisOrder=LineNum and DocEntry=t2.DocEntry )*t3.SWeight1),2,1)as PesoxArt,t7.TrnspName  "
   'SQLOrdenes &= "  ,T10.Weight as Peso,(Select Name From TPM.dbo.Operacion_Empleado TT1 INNER JOIN TPM.DBO.Operacion_Entrega TT2 on TT2.UserId_Surtido =TT1.KeyCode where tt2.DocEntry=2 ) AS Surtidor "
   'SQLOrdenes &= ",(Select Name From TPM.dbo.Operacion_Empleado TT1 INNER JOIN TPM.DBO.Operacion_Entrega TT2 on TT2.UserId_Empacado =TT1.KeyCode where tt2.DocEntry=2) AS Empacador,T11.BoxTotal,ROW_NUMBER() OVER(ORDER BY t5.CardCode ASC) AS RowNumber "
   'SQLOrdenes &= "From DLN1 T1 LEFT JOIN TPM.dbo.Operacion_Detalle_Entrega T2 ON T1.DocEntry=T2.DocEntry and t1.baseRef =T2.DocNum  "
   'SQLOrdenes &= "LEFT JOIN OITM T3 on T1.ItemCode=T3.ItemCode   LEFT JOIN OITB T4 on T3.ItmsGrpCod=T4.ItmsGrpCod  LEFT JOIN ODLN t5 ON T1.DocEntry=T5.DocEntry "
   'SQLOrdenes &= "LEFT JOIN OSLP T6 ON T1.SlpCode=T6.SlpCode LEFT JOIN OSHP T7 ON T7.TrnspCode=t5.TrnspCode "
   'SQLOrdenes &= "LEFT Join TPM.dbo.Operacion_Entrega  T8 ON T8.DocEntry=T2.DocEntry AND T8.DocNum=T2.DocNum "
   'SQLOrdenes &= " LEFT JOIN ODLN T10 ON T1.DocEntry=T10.DocEntry  left join TPM.dbo.Operacion_Orden t11 on t11.DocNum = t8.DocNum "
   'SQLOrdenes &= " WHERE  T2.Docentry=" + entrega + " AND T4.ItmsGrpCod<>150 group by ItmsGrpNam ,	T5.DocNum,t5.CardCode,T5.CardName,T5.DocDate,t5.LicTradNum,t5.Address,SlpName,T1.BaseRef,T1.ItemCode,Dscription,t2.DocEntry,T1.VisOrder,t3.SWeight1,t7.TrnspName,T10.Weight,T11.BoxTotal Order By ItmsGrpNam "
   'cnn = New SqlConnection(StrCon)
   ''ALMACENA LA CONSULTA EN UN COMMAND SQL
   'cmd = New SqlCommand(SQLOrdenes, cnn)
   ''CONVIERTE EL TEXTO EN CONSULTA
   'cmd.CommandType = CommandType.Text
   ''APERTURA LA CONEXION
   'cnn.Open()
   ''INSTANCIA UN ADAPTER
   'da = New SqlDataAdapter
   ''ALMACENA EL COMMAND DE SQL EN EL ADAPTER
   'da.SelectCommand = cmd
   ''LO EJECUTA CON LA CONEXION
   'da.SelectCommand.Connection = cnn
   ''TIEMPO DE ESPERA DE LA CONEXION
   'da.SelectCommand.CommandTimeout = 10000
   ''EJECUTA LA CONSULTA
   'cmd.ExecuteNonQuery()
   ''CIERRA EL COMMAND DE SQL
   'cmd.Connection.Close()
   ''CIERRA LA CONEXION
   'cnn.Close()
   ''LLENA EL ADAPTER A UN DATA SET
   'da.Fill(DsOrdenes)
   ''INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
   'ResultadoDetalleEmpaque = New DataView
   ''ALMACENA EN DATA SET DE MODO TABLA
   'ResultadoDetalleEmpaque.Table = DsOrdenes.Tables(0)
   ''Crea un nuevo DataView         

   DvDetalle_Estatus = New DataView
            'ALMACENA EN DATA SET DE MODO TABLA

            Dim DocEntry2 As String = SQL.CampoEspecifico("select DocEntry from SBO_TPD.DBO.ODLN WHERE DocNum =" + DocEntry.ToString, "DocEntry")



            DvDetalle_Estatus.Table = SQL.EjecutarProcedimiento("TPD_OrdenEmpaque", "@DocEntry", 1, DocEntry2.ToString)

            'DvDetalle_Estatus.Table = SQL.EjecutarProcedimiento("TPD_Imprimir_Empaque", "@DocEntry", 1, entrega)
            'Se crea un informe de cristal Reports
            Dim informe As New ReporteEmpaque

   Dim Surtidor As String = IIf(IsDBNull(DvDetalle_Estatus.Item(0).Item("Surtidor")), "", DvDetalle_Estatus.Item(0).Item("Surtidor"))
   Dim Empacador As String = IIf(IsDBNull(DvDetalle_Estatus.Item(0).Item("Empacador")), "", DvDetalle_Estatus.Item(0).Item("Empacador"))
            Dim Comentario As String = IIf(IsDBNull(DvDetalle_Estatus.Item(0).Item("Comentario")), "", DvDetalle_Estatus.Item(0).Item("Comentario"))

            RepComsultaP.MdiParent = Inicio
            informe.SetDataSource(DvDetalle_Estatus)

   informe.SetParameterValue("surtio", Surtidor)
            informe.SetParameterValue("empaco", Empacador)
            informe.SetParameterValue("Comentario", Comentario)

            RepComsultaP.CrVConsulta.ReportSource = informe
   informe.PrintToPrinter(1, False, 0, 0)

  Catch ex As Exception
   MsgBox("Error: " + ex.ToString)
  End Try
 End Sub

 Sub SetTableLocation(report As ReportDocument, connectionInfo As ConnectionInfo)
  For Each table As Table In report.Database.Tables
   Dim tableLogOnInfo As TableLogOnInfo = table.LogOnInfo
   tableLogOnInfo.ConnectionInfo = connectionInfo
   table.ApplyLogOnInfo(tableLogOnInfo)
  Next
 End Sub

#End Region
#Region "Eventos"
    Private Sub frmEstatusEmpaque_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Llenar Empaque'

        ReimpresionPL.Visible = False
        If UsrTPM = "MANAGER" Or UsrTPM = "OPALMACEN" Then
            ReimpresionPL.Visible = True
        End If

        MEjecuta_Full_Empaque()
        LlenarEmpaque() '30 ms
        dgvEmpaqueDetalle.DataSource = Nothing
        DocEntry_P = 0
        Console.WriteLine("Se actualizo el grid: " + DateTime.Now)

        Timer1.Enabled = True
        CheckForIllegalCrossThreadCalls = False
        NuevaOrden()
    End Sub

    'Sub MEjecuta_Entrega()
    '  'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
    '  Dim cmd4 As SqlCommand
    '  Dim cnn As SqlConnection = Nothing
    '  Dim da As SqlDataAdapter
    '  DsOrdenes = New DataSet
    '  'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
    '  Try
    '    cnn = New SqlConnection(StrTpm) 'ORIGINAL
    '    'cnn = New SqlConnection(StrTpm) 'PRUEBAS

    '    cmd4 = New SqlCommand("SP_Operacion_Aux_Entrega", cnn)
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

    'Sub MEjecuta_Entrega_Ped()
    '  'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
    '  Dim cmd As SqlCommand
    '  Dim cnn As SqlConnection = Nothing
    '  'Dim da As SqlDataAdapter
    '  DsOrdenes = New DataSet
    '  'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
    '  Try
    '    cnn = New SqlConnection(StrTpm) 'ORIGINAL
    '    cmd = New SqlCommand("SP_Operacion_Entrega", cnn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = String.Format("{0:yyyy-MM-dd}", dtpFecha.Value)
    '    'cmd.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value))
    '    'MsgBox(DTPFecha.Value.ToString("dddd"))
    '    cnn.Open()
    '    'da = New SqlDataAdapter
    '    'da.SelectCommand = cmd
    '    'da.SelectCommand.Connection = cnn
    '    'da.SelectCommand.CommandTimeout = 10000
    '    cmd.ExecuteNonQuery()
    '    cmd.Connection.Close()
    '    cnn.Close()
    '  Catch ex As Exception
    '    MessageBox.Show("Error al Mostrar los datos de las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Return
    '  End Try
    'End Sub

    'Sub MEjecuta_Entrega_Ped_Det()
    '  'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
    '  Dim cmd As SqlCommand
    '  Dim cnn As SqlConnection = Nothing
    '  'Dim da As SqlDataAdapter
    '  DsOrdenes = New DataSet
    '  'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
    '  Try
    '    cnn = New SqlConnection(StrTpm) 'ORIGINAL
    '    'cnn = New SqlConnection(StrTpm) 'PRUEBAS
    '    cmd = New SqlCommand("SP_Operacion_Entrega_Det", cnn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = String.Format("{0:yyyy-MM-dd}", dtpFecha.Value)
    '    'cmd.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value))
    '    'MsgBox(DTPFecha.Value.ToString("dddd"))
    '    cnn.Open()
    '    'da = New SqlDataAdapter
    '    'da.SelectCommand = cmd
    '    'da.SelectCommand.Connection = cnn
    '    'da.SelectCommand.CommandTimeout = 10000
    '    cmd.ExecuteNonQuery()
    '    cmd.Connection.Close()
    '    cnn.Close()
    '  Catch ex As Exception
    '    MessageBox.Show("Error al Insertar el detalle de las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Return
    '  End Try
    'End Sub

    Sub MEjecuta_Full_Empaque()
  'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
  Dim cmd4 As SqlCommand
  Dim cnn As SqlConnection = Nothing
  Dim da As SqlDataAdapter
  DsOrdenes = New DataSet
  'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
  Try
   BtnActualizarEmp.Visible = False
   lblActualizando.Visible = True
   Me.Refresh()
   cnn = New SqlConnection(StrTpm) 'ORIGINAL

   cmd4 = New SqlCommand("SP_Full_Empaque", cnn)
   cmd4.CommandType = CommandType.StoredProcedure
   cmd4.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value))
   cnn.Open()
   da = New SqlDataAdapter
   da.SelectCommand = cmd4
   da.SelectCommand.Connection = cnn
   da.SelectCommand.CommandTimeout = 10000
   cmd4.ExecuteNonQuery()
   cmd4.Connection.Close()
   cnn.Close()


   'Esta es solo para una prueba de devoluciones
   'cmd4 = New SqlCommand("ANDROID_InsertaDevolucion", cnn)
   'cmd4.CommandType = CommandType.StoredProcedure
   'cnn.Open()
   'da = New SqlDataAdapter
   'da.SelectCommand = cmd4
   'da.SelectCommand.Connection = cnn
   'da.SelectCommand.CommandTimeout = 10000
   'cmd4.ExecuteNonQuery()
   'cmd4.Connection.Close()
   'cnn.Close()


   BtnActualizarEmp.Visible = True
   lblActualizando.Visible = False
   Me.Refresh()
  Catch ex As Exception
   MessageBox.Show("Error al insertar la información del Día. " + ex.ToString, "Error de Conexión o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
   Return
  End Try
  'FIN OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
 End Sub

 Private Sub dgvEmpaque_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

 End Sub
 'Notificación de nueva orden de Entrega
 Private Sub NuevaOrden()

  If dgvEmpaque.Rows.Count > 0 Then

   'If (dgvOrdenes.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Surtir") Then

   For Each Fila As DataGridViewRow In dgvEmpaque.Rows

    '//Puedes hacer una validación con el nombre de la columna
    If Fila.Cells("Accion").Value.ToString = "Por Empacar" Then
     My.Computer.Audio.Play(My.Resources.SD_ALERT_26, AudioPlayMode.Background)
    End If


   Next
   'End If
  End If
 End Sub
 Private Sub txtBuscarOrdenes_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarOrdenes.TextChanged
  'TEXTBOX AUTORIZACIONES
  'Filtra el dataview de una consulta ya hecha para filtrar campos ya existentes en un datagridview
  ResultadoEmpaque.RowFilter = "NombreCliente like '%" & CStr(txtBuscarOrdenes.Text) & "%' OR  OrdenDeVenta like '%" & CStr(txtBuscarOrdenes.Text) & "%'"

 End Sub
 Private Sub dgvEmpaque_SelectionChanged(sender As Object, e As EventArgs)


 End Sub
 Private Sub lblBuscarOrdenes_Click(sender As Object, e As EventArgs) Handles lblBuscarOrdenes.Click

 End Sub
 Private Sub BtnActualizarEmp_Click(sender As Object, e As EventArgs) Handles BtnActualizarEmp.Click
  Try
   If dgvEmpaque.Rows.Count > 0 Then
    CurrentRow = dgvEmpaque.CurrentCell.RowIndex
    CurrentCol = dgvEmpaque.CurrentCell.ColumnIndex
   Else
    CurrentRow = 0
    CurrentCol = 0
   End If
   MEjecuta_Full_Empaque()
   LlenarEmpaque() '5 ms
   If CurrentRow > dgvEmpaque.Rows.Count Then
    CurrentRow = dgvEmpaque.Rows.Count
   ElseIf (dgvEmpaque.Rows.Count > 0) Then
    dgvEmpaque.CurrentCell = dgvEmpaque(CurrentCol, CurrentRow)
   End If
  Catch ex As Exception

  End Try
  DocEntry_P = 0
  dgvEmpaqueDetalle.DataSource = Nothing
  NuevaOrden()
 End Sub
 Private Sub dgvEmpaque_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)

 End Sub
 Private Sub dgvEmpaque_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvEmpaque.KeyDown
  'VALIDA QUE LA TECLA PRESIONADA SEA ENTER
  If (e.KeyCode = Keys.Enter) Then
   'Se detecto que justo cuando se va a realizar una operacion el grid se actualiza y el currentrow tambien se actualiza a la posicion 0,0, esto  ya se corrigio pero se deja como opcion la linea de abajo para deshabilitar y rehabilitar el timer
   'Timer1.Enabled = False

   ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
   e.SuppressKeyPress = True
   'VALIDA QUE SEA LA ACCION DE SURTIENDO PARA MOSTRAR LA PANTALLA
   If (dgvEmpaque.CurrentRow.Cells("Accion").Value.ToString = "En Revision") Then
    If MessageBox.Show("¿Deseas terminar la revision de la orden no. [" & dgvEmpaque.CurrentRow.Cells("OrdenEmpaque").Value.ToString() & "]?", "Pregunta...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
     SQL.conectarTPM()
     'ACTUALIZAR ESTATUS
     SQL.EjecutarComando("UPDATE Operacion_Entrega SET Action = 'Revisado' WHERE DocEntry = " + dgvEmpaque.CurrentRow.Cells("OrdenEmpaque").Value.ToString())
     'ACTUALIZAR HORA
     SQL.EjecutarComando("UPDATE Operacion_Analisis SET TimeRevisado = GETDATE() WHERE DocEntry = " + dgvEmpaque.CurrentRow.Cells("OrdenDeVenta").Value.ToString())
     LlenarEmpaque()
    End If

   ElseIf (dgvEmpaque.CurrentRow.Cells("Accion").Value.ToString = "Empacando") Then
    'ALMACENA EL STATUS
    StatusSurtido = "Surtiendo"
    'ALMACENA EL DOCUMENTO DE LA ORDEN PARA PASARLO AL NUEVO FORMULARIO
    DocNumSurtido = dgvEmpaque.CurrentRow.Cells("OrdenDeVenta").Value.ToString()

    DocNumEmpacar = dgvEmpaque.CurrentRow.Cells("OrdenEmpaque").Value.ToString()

    ClienteNombre = dgvEmpaque.CurrentRow.Cells("NombreCliente").Value.ToString()

    Paqueteria_Nombre = dgvEmpaque.CurrentRow.Cells("Paqueteria").Value.ToString()

    Cajas = dgvEmpaque.CurrentRow.Cells("Cajas").Value.ToString()


    'ALMACENA EL TITULO DEL FORMULARIO DE DETALLE DE SURTIDO
    'Por Continuar 
    TituloSurtido = StatusSurtido + " Orden de Empaque | " + DocNumSurtido
    If Paqueteria_Nombre = "Entrega Personal" Then
     'ACTUALIZA EL ESTATUS A EMPACADO
     Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      SQLOrdenes = " Update TPM.dbo.Operacion_Entrega SET Status='EP', Action='Empacado' WHERE DocEntry=" + DocNumEmpacar + " "
      cnn = New SqlConnection(StrCon)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      cmd = New SqlCommand(SQLOrdenes, cnn)
      'CONVIERTE EL TEXTO EN CONSULTA
      cmd.CommandType = CommandType.Text
      'APERTURA LA CONEXION
      cnn.Open()
      cmd.ExecuteNonQuery()
      'CIERRA EL COMMAND DE SQL
      cmd.Connection.Close()
      'CIERRA LA CONEXION
      cnn.Close()
     Catch ex As Exception
      MsgBox("Error: Ocurrio un Error al Actualizar el estatus a Empacado " + ex.ToString)
     End Try
     'Actualiza operacion Orden 
     Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      SQLOrdenes = "Update TPM.dbo.Operacion_Orden  SET Status='EP', Action='Empacado' WHERE DocNum=" + DocNumSurtido + ""
      cnn = New SqlConnection(StrCon)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      cmd = New SqlCommand(SQLOrdenes, cnn)
      'CONVIERTE EL TEXTO EN CONSULTA
      cmd.CommandType = CommandType.Text
      'APERTURA LA CONEXION
      cnn.Open()
      cmd.ExecuteNonQuery()
      'CIERRA EL COMMAND DE SQL
      cmd.Connection.Close()
      'CIERRA LA CONEXION
      cnn.Close()
     Catch ex As Exception
      MsgBox("Error: Ocurrio un Error al Actualizar el estatus a Empacado " + ex.ToString)
     End Try
     'Actualiza operacion analicis
     Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      SQLOrdenes = "Update TPM.dbo.Operacion_Analisis set TimeEmpaque=GETDATE(), TimeEmpacado=GETDATE()  where DocEntry='" + dgvEmpaque.CurrentRow.Cells("OrdenDeVenta").Value.ToString() + "'"
      cnn = New SqlConnection(StrCon)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      cmd = New SqlCommand(SQLOrdenes, cnn)
      'CONVIERTE EL TEXTO EN CONSULTA
      cmd.CommandType = CommandType.Text
      'APERTURA LA CONEXION
      cnn.Open()
      cmd.ExecuteNonQuery()
      'CIERRA EL COMMAND DE SQL
      cmd.Connection.Close()
      'CIERRA LA CONEXION
      cnn.Close()
     Catch ex As Exception
      MsgBox("Error: Ocurrio un Error al Actualizar el estatus a Empacando " + ex.ToString)
     End Try
     MsgBox("El empaque se termino con Exito.", MsgBoxStyle.Information, "Termino Empaque")
     LlenarEmpaque()
    Else
     'PAUSAR EL TIMER PARA QUE NO SE TRABE CUANDO CAPTUREN LOS PESOS
     Timer1.Enabled = False

     DetalleEmpaquePeso.MdiParent = Inicio
     DetalleEmpaquePeso.Show()

     'llenar empaque cuando terminen de capturar
     LlenarEmpaque()

    End If
    'LlenarEmpaque()
    'MImprimeFormatoSINPK(DocNumEmpacar)
    'MsgBox("El empaque se termino con Exito.", MsgBoxStyle.Information, "Termino Empaque")
   ElseIf (dgvEmpaque.CurrentRow.Cells("Accion").Value.ToString = "Revisado") Then 'VALIDA QUE SEA LA ACCION DE SURTIR PARA MOSTRAR ACCESO
    Dim confirma As Integer
    'MANDA LA CONFIRMACIÓN SI REALMENTE REQUIEREN SURTIR LA ORDEN
    confirma = MessageBox.Show("Realmente desea comenzar Empaque de la orden [ " + dgvEmpaque.CurrentRow.Cells("OrdenDeVenta").Value.ToString() + " ], Desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    'SI SE ELIJE QUE SI, ENTRA EN LA PRIMERA OPCIÓN.
    If confirma = 6 Then
     'CONCATENA EL TITULO DEL FORMULARIO DE LA PANTALLA DE ACCESO
     TituloAcceso = "Acceso a" + " " + "Empacar" + " " + "orden" + " [" + dgvEmpaque.CurrentRow.Cells("OrdenDeVenta").Value.ToString() + " ]"
     'ALMACENA LA ORDEN DE VENTA
     DocNumAcceso = dgvEmpaque.CurrentRow.Cells("OrdenDeVenta").Value.ToString
     'ALMACENA LA ORDEN DE ENTREGA
     DocNumEmpacar = dgvEmpaque.CurrentRow.Cells("OrdenEmpaque").Value.ToString
     'ALMACENA EL STATUS DE LA ORDEN
     StatusAcceso = dgvEmpaque.CurrentRow.Cells("Accion").Value.ToString
     'CARGA FORMULARIO
     Using frmA As New frmAccesoEmpaque
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
        DocNumEmpacar = ""
        Return
       Else 'SI EL CIERRE ES VERDADERO SE MANDA A LLAMAR LA EJECUCION DE EL REFRES
        'MODIFICO IVAN GONZALEZ FALTABA ACTUALIZAR HORA DE EMPAQUE
        'Actualiza operacion analisis
        Try
         'VARIABLE DE CADENA DE SQL
         Dim SQLOrdenes As String
         'VARIABLES DE CONEXION DE LLENADO
         Dim cmd As SqlCommand
         Dim cnn As SqlConnection = Nothing
         SQLOrdenes = "Update TPM.dbo.Operacion_Analisis set TimeEmpaque=GETDATE()  where DocEntry='" + dgvEmpaque.CurrentRow.Cells("OrdenDeVenta").Value.ToString() + "'"
         cnn = New SqlConnection(StrCon)
         'ALMACENA LA CONSULTA EN UN COMMAND SQL
         cmd = New SqlCommand(SQLOrdenes, cnn)
         'CONVIERTE EL TEXTO EN CONSULTA
         cmd.CommandType = CommandType.Text
         'APERTURA LA CONEXION
         cnn.Open()
         cmd.ExecuteNonQuery()
         'CIERRA EL COMMAND DE SQL
         cmd.Connection.Close()
         'CIERRA LA CONEXION
         cnn.Close()
        Catch ex As Exception
         MsgBox("Error: Ocurrio un Error al Actualizar el estatus a Empaque " + ex.ToString)
        End Try
        LlenarEmpaque()
       End If
      End If
     End Using 'FIN CARGA FORMULARIO
    End If 'FIN VALIDA SI PRESIONA SI O NO
   ElseIf (dgvEmpaque.CurrentRow.Cells("Accion").Value.ToString = "Empacado") Then
   End If 'FIN VALIDA ACCIONES
  Else
   'SI ES OTRA TECLA QUE NO SEA EL ENTER PERMITE EL SALTO DE LA LINEA
   e.SuppressKeyPress = False
  End If
  'Timer1.Enabled = True
 End Sub
 Sub MImprimeFormatoSINPK(paking As String)

  'VARIABLE DE CADENA DE SQL
  Dim SQLOrdenes As String
  'VARIABLES DE CONEXION DE LLENADO
  Dim cmd As SqlCommand
  Dim cnn As SqlConnection = Nothing
  Dim DsOrdenes = New DataSet
  Dim da As SqlDataAdapter
  'ALAMACENA LA CONSULTA
  SQLOrdenes = "Select Distinct ItmsGrpNam ,T5.DocNum,t5.CardCode,T5.CardName,T5.DocDate,t5.LicTradNum , "
  SQLOrdenes &= "t5.Address, SlpName, T1.BaseRef, T1.ItemCode AS Codigo,Dscription AS Descripcion,(Select Surtido   "
  SQLOrdenes &= "From  TPM.dbo.Operacion_Detalle_Entrega where T1.VisOrder=LineNum and DocEntry=t2.DocEntry )AS Cantidad, "
  SQLOrdenes &= "Round(t3.SWeight1,2,0) as PesoxUni ,ROUND(((Select Surtido From  TPM.dbo.Operacion_Detalle_Entrega "
  SQLOrdenes &= "where T1.VisOrder=LineNum and DocEntry=t2.DocEntry )*t3.SWeight1),2,1)as PesoxArt,t7.TrnspName  "
  SQLOrdenes &= ",(Select Name From TPM.dbo.Operacion_Empleado TT1 INNER JOIN TPM.DBO.Operacion_Entrega TT2 on TT2.UserId_Surtido =TT1.KeyCode where tt2.DocEntry=DocEntry) AS Surtidor  "
  SQLOrdenes &= ",t1.LineNum From DLN1 T1 LEFT JOIN TPM.dbo.Operacion_Detalle_Entrega T2 ON T1.DocEntry=T2.DocEntry "
  SQLOrdenes &= "  LEFT JOIN OITM T3 on T1.ItemCode=T3.ItemCode   LEFT JOIN OITB T4 on T3.ItmsGrpCod=T4.ItmsGrpCod  "
  SQLOrdenes &= " LEFT JOIN ODLN t5 ON T1.DocEntry=T5.DocEntry   LEFT JOIN OSLP T6 ON T1.SlpCode=T6.SlpCode  "
  SQLOrdenes &= "LEFT JOIN OSHP T7 ON T7.TrnspCode=t5.TrnspCode   LEFT Join TPM.dbo.Operacion_Entrega  T8 ON T8.DocEntry=T2.DocEntry AND T8.DocNum=T2.DocNum   "
  SQLOrdenes &= " WHERE  T2.Docentry=" + paking + " AND T4.ItmsGrpCod<>150  Order By  ItmsGrpNam ,T1.ItemCode"
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
  Resultadopaking = New DataView
  'ALMACENA EN DATA SET DE MODO TABLA
  Resultadopaking.Table = DsOrdenes.Tables(0)
  'Crea un nuevo DataView         
  DvDetalle_Estatus = New DataView
  'ALMACENA EN DATA SET DE MODO TABLA
  DvDetalle_Estatus.Table = DsOrdenes.Tables(0)
  'Se crea un informe de cristal Reports
  Dim informe As New CR_Paking_List
  RepComsultaP.MdiParent = Inicio
  informe.SetDataSource(DvDetalle_Estatus)
  RepComsultaP.CrVConsulta.ReportSource = informe
  informe.PrintToPrinter(1, False, 0, 0)

 End Sub
#End Region
#Region "Estilo"
 Sub EstiloEmpaque()
  'Este metodo cambia el estilo del gridview detalle 
  With Me.dgvEmpaque
   'COLOCA PROPIEDADES DE COLOR ALTERNADOS
   .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionForeColor = Color.White
   .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
   .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   'Partidas
   .Columns("OrdenEmpaque").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("OrdenEmpaque").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8.5, FontStyle.Bold)
   .Columns("OrdenEmpaque").HeaderText = "Orden Entrega"
   .Columns("OrdenEmpaque").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("OrdenEmpaque").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("OrdenEmpaque").Width = 50
   .Columns("OrdenEmpaque").ReadOnly = True
   'Partidas
   .Columns("OrdenDeVenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("OrdenDeVenta").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8.5, FontStyle.Bold)
   .Columns("OrdenDeVenta").HeaderText = "Orden De Venta"
   .Columns("OrdenDeVenta").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("OrdenDeVenta").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("OrdenDeVenta").Width = 50
   .Columns("OrdenDeVenta").ReadOnly = True
   'Articulo
   .Columns("FechaDeCreacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("FechaDeCreacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("FechaDeCreacion").HeaderText = "Fecha De Creacion"
   .Columns("FechaDeCreacion").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("FechaDeCreacion").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("FechaDeCreacion").Width = 65
   .Columns("FechaDeCreacion").ReadOnly = True
   'Descripcion
   .Columns("horaCreacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("horaCreacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("horaCreacion").HeaderText = "Hora Creacion"
   .Columns("horaCreacion").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("horaCreacion").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("horaCreacion").Width = 50
   .Columns("horaCreacion").ReadOnly = True
   'Cantidad
   .Columns("Partidas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("Partidas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Partidas").HeaderText = "Partidas"
   .Columns("Partidas").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("Partidas").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("Partidas").DefaultCellStyle.Format = "N0"
   .Columns("Partidas").Width = 35
   .Columns("Partidas").ReadOnly = True
   'Surtido
   .Columns("CodigoCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("CodigoCliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
   .Columns("CodigoCliente").HeaderText = "Codigo Cliente"
   .Columns("CodigoCliente").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("CodigoCliente").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("CodigoCliente").Width = 50
   .Columns("CodigoCliente").ReadOnly = True
   'Surtido
   .Columns("NombreCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("NombreCliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
   .Columns("NombreCliente").HeaderText = "Nombre Cliente "
   .Columns("NombreCliente").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("NombreCliente").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("NombreCliente").Width = 150
   .Columns("NombreCliente").ReadOnly = True
   'Surtido
   .Columns("Paqueteria").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("Paqueteria").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
   .Columns("Paqueteria").HeaderText = "Paqueteria"
   .Columns("Paqueteria").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("Paqueteria").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("Paqueteria").Width = 110
   .Columns("Paqueteria").ReadOnly = True
   'Comment
   .Columns("Comment").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("Comment").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
   .Columns("Comment").HeaderText = "Comentario"
   .Columns("Comment").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("Comment").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("Comment").Width = 150
   .Columns("Comment").ReadOnly = True
   'Surtido
   .Columns("Cajas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("Cajas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Cajas").HeaderText = "Cajas"
   .Columns("Cajas").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("Cajas").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("Cajas").Width = 35
   .Columns("Cajas").ReadOnly = True
   'Peso 
   .Columns("Peso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("Peso").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Peso").HeaderText = "Peso"
   .Columns("Peso").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("Peso").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("Peso").DefaultCellStyle.Format = "N2"
   .Columns("Peso").Width = 60
   .Columns("Peso").ReadOnly = True
   ''Surtidor 
   '.Columns("Surtidor").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   '.Columns("Surtidor").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   '.Columns("Surtidor").HeaderText = "Surtidor"
   '.Columns("Surtidor").DefaultCellStyle.Format = "N2"
   '.Columns("Surtidor").Width = 60
   '.Columns("Surtidor").ReadOnly = True
   'Surtido
   .Columns("Accion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("Accion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Accion").HeaderText = "Accion"
   .Columns("Accion").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("Accion").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("Accion").Width = 100
   .Columns("Accion").ReadOnly = True
   'Peso 
   .Columns("Imprimir").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("Imprimir").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Imprimir").HeaderText = "Imprimir"
   .Columns("Imprimir").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("Imprimir").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("Imprimir").Width = 70
   .Columns("Imprimir").ReadOnly = True
   'Peso 
   .Columns("TrnspCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("TrnspCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("TrnspCode").HeaderText = "Horario Paq."
   .Columns("TrnspCode").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("TrnspCode").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("TrnspCode").Width = 90
            .Columns("TrnspCode").ReadOnly = True

            .Columns("Empacador").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Empacador").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Empacador").HeaderText = "Empacador"
            .Columns("Empacador").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Empacador").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns("Empacador").Width = 115
            .Columns("Empacador").ReadOnly = True

            .Columns("Agente de Ventas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Agente de Ventas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Agente de Ventas").HeaderText = "Agente de Ventas"
            .Columns("Agente de Ventas").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Agente de Ventas").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns("Agente de Ventas").Width = 115
            .Columns("Agente de Ventas").ReadOnly = True


        End With
 End Sub
 Sub EstilDetalleEmpaque()
  'Este metodo cambia el estilo del gridview detalle 
  With Me.dgvEmpaqueDetalle
   'COLOCA PROPIEDADES DE COLOR ALTERNADOS
   .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionForeColor = Color.White
   .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
   .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   'Articulo
   .Columns("Codigo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
   .Columns("Codigo").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Codigo").HeaderText = "Codigo"
   .Columns("Codigo").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("Codigo").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("Codigo").Width = 100
   .Columns("Codigo").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("Codigo").SortMode = DataGridViewColumnSortMode.NotSortable
   'Descripcion
   .Columns("Descripcion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
   .Columns("Descripcion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Descripcion").HeaderText = "Descripción"
   .Columns("Descripcion").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("Descripcion").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("Descripcion").Width = 150
   .Columns("Descripcion").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("Descripcion").SortMode = DataGridViewColumnSortMode.NotSortable
   'Cantidad
   .Columns("Cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("Cantidad").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Cantidad").HeaderText = "Cantidad"
   .Columns("Cantidad").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("Cantidad").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("Cantidad").DefaultCellStyle.Format = "N0"
   .Columns("Cantidad").Width = 80
   .Columns("Cantidad").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("ItmsGrpNam").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("ItmsGrpNam").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("ItmsGrpNam").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("ItmsGrpNam").HeaderText = "ItmsGrpNam"
   .Columns("ItmsGrpNam").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("ItmsGrpNam").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("ItmsGrpNam").DefaultCellStyle.Format = "N0"
   .Columns("ItmsGrpNam").Width = 50
   .Columns("ItmsGrpNam").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("ItmsGrpNam").Visible = False
   'Bloquea el ordenamiento del gridview
   .Columns("DocNum").Visible = False
   .Columns("DocNum").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("DocNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("DocNum").HeaderText = "DocNum"
   .Columns("DocNum").HeaderCell.Style.WrapMode = DataGridViewTriState.True
   .Columns("DocNum").DefaultCellStyle.WrapMode = DataGridViewTriState.True
   .Columns("DocNum").DefaultCellStyle.Format = "N0"
   .Columns("DocNum").Width = 50
   .Columns("DocNum").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("DocNum").Visible = False
   'Bloquea el ordenamiento del gridview
   .Columns("CardCode").Visible = False
   .Columns("CardCode").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("CardCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("CardCode").HeaderText = "DocNum"
   .Columns("CardCode").DefaultCellStyle.Format = "N0"
   .Columns("CardCode").Width = 50
   .Columns("CardCode").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("CardCode").Visible = False
   'Bloquea el ordenamiento del gridview
   .Columns("CardName").Visible = False
   .Columns("CardName").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("CardName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("CardName").HeaderText = "CardName"
   .Columns("CardName").DefaultCellStyle.Format = "N0"
   .Columns("CardName").Width = 50
   .Columns("CardName").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("CardName").Visible = False
   'Bloquea el ordenamiento del gridview
   .Columns("DocDate").Visible = False
   .Columns("DocDate").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("DocDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("DocDate").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("DocDate").HeaderText = "DocDate"
   .Columns("DocDate").DefaultCellStyle.Format = "N0"
   .Columns("DocDate").Width = 50
   .Columns("DocDate").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("DocDate").Visible = False
   'Bloquea el ordenamiento del gridview
   .Columns("LicTradNum").Visible = False
   .Columns("LicTradNum").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("LicTradNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("LicTradNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("LicTradNum").HeaderText = "LicTradNum"
   .Columns("LicTradNum").DefaultCellStyle.Format = "N0"
   .Columns("LicTradNum").Width = 50
   .Columns("LicTradNum").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("LicTradNum").Visible = False
   'Bloquea el ordenamiento del gridview
   .Columns("Address").Visible = False
   .Columns("Address").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("Address").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("Address").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("Address").HeaderText = "LicTradNum"
   .Columns("Address").DefaultCellStyle.Format = "N0"
   .Columns("Address").Width = 50
   .Columns("Address").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("Address").Visible = False
   'Bloquea el ordenamiento del gridview
   .Columns("SlpName").Visible = False
   .Columns("SlpName").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("SlpName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("SlpName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("SlpName").HeaderText = "SlpName"
   .Columns("SlpName").DefaultCellStyle.Format = "N0"
   .Columns("SlpName").Width = 50
   .Columns("SlpName").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("SlpName").Visible = False
   'Bloquea el ordenamiento del gridview
   .Columns("PesoxUni").Visible = False
   .Columns("PesoxUni").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("PesoxUni").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("PesoxUni").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("PesoxUni").HeaderText = "PesoxUni"
   .Columns("PesoxUni").DefaultCellStyle.Format = "N0"
   .Columns("PesoxUni").Width = 50
   .Columns("PesoxUni").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("PesoxUni").Visible = False
   'Bloquea el ordenamiento del gridview
   .Columns("PesoxArt").Visible = False
   .Columns("PesoxArt").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("PesoxArt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("PesoxArt").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("PesoxArt").HeaderText = "PesoxArt"
   .Columns("PesoxArt").DefaultCellStyle.Format = "N0"
   .Columns("PesoxArt").Width = 50
   .Columns("PesoxArt").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("PesoxArt").Visible = False
   'Bloquea el ordenamiento del gridview
   .Columns("TrnspName").Visible = False
   .Columns("TrnspName").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("TrnspName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("TrnspName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("TrnspName").HeaderText = "PesoxArt"
   .Columns("TrnspName").DefaultCellStyle.Format = "N0"
   .Columns("TrnspName").Width = 50
   .Columns("TrnspName").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("TrnspName").Visible = False
   'Bloquea el ordenamiento del gridview
   .Columns("BaseRef").Visible = False
   .Columns("BaseRef").SortMode = DataGridViewColumnSortMode.NotSortable
   .Columns("BaseRef").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("BaseRef").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("BaseRef").HeaderText = "BaseRef"
   .Columns("BaseRef").DefaultCellStyle.Format = "N0"
   .Columns("BaseRef").Width = 50
   .Columns("BaseRef").ReadOnly = True
   'Bloquea el ordenamiento del gridview
   .Columns("BaseRef").Visible = False
  End With
 End Sub


 Private Sub dgvEmpaqueDetalle_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvEmpaqueDetalle.RowPostPaint
  Try
   Dim NumeroFila As String = (e.RowIndex + 1).ToString 'Obtiene el número de filas
   While NumeroFila.Length < dgvEmpaqueDetalle.RowCount.ToString.Length
    NumeroFila = "0" & NumeroFila 'Agrega un cero a los que tienen un dígito menos
   End While
   Dim size As SizeF = e.Graphics.MeasureString(NumeroFila, Me.Font)
   If dgvEmpaqueDetalle.RowHeadersWidth < CInt(size.Width + 20) Then
    dgvEmpaqueDetalle.RowHeadersWidth = CInt(size.Width + 20)
   End If
   Dim Obj As Brush = SystemBrushes.ControlText
   'Dibuja el número dentro del controltext
   e.Graphics.DrawString(NumeroFila, Me.Font, Obj, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))
  Catch ex As Exception
   MessageBox.Show(ex.Message, "Error")
  End Try
 End Sub

 Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
  Try
   If dgvEmpaque.Rows.Count > 0 Then
    CurrentRow = dgvEmpaque.CurrentCell.RowIndex
    CurrentCol = dgvEmpaque.CurrentCell.ColumnIndex
   Else
    CurrentRow = 0
    CurrentCol = 0
   End If
   MEjecuta_Full_Empaque()
   LlenarEmpaque()
   If CurrentRow > dgvEmpaque.Rows.Count Then
    CurrentRow = dgvEmpaque.Rows.Count
   ElseIf (dgvEmpaque.Rows.Count > 0) Then
    dgvEmpaque.CurrentCell = dgvEmpaque(CurrentCol, CurrentRow)
   End If

   dgvEmpaqueDetalle.DataSource = Nothing
   DocEntry_P = 0
   Console.WriteLine("Se actualizo el grid: " + DateTime.Now)
  Catch ex As Exception

  End Try
 End Sub

 Private Sub dgvEmpaque_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEmpaque.CellClick
  'Al dar click en alguna columna del GridView Operacion Vta Se actualizara el Detalle en otro grid
  Try
   Dim row As DataGridViewRow = dgvEmpaque.CurrentRow()
            'Llena el datagrid detalle 
            DocEntry = row.Cells("OrdenEmpaque").Value
            'DocNumAccesoRev = CStr(row.Cells("OrdenEmpaque").Value).ToString
            DocNumOV = row.Cells("OrdenDeVenta").Value

            Dim DocEntry2 As String = SQL.CampoEspecifico("select DocEntry from SBO_TPD.DBO.ODLN WHERE DocNum =" + DocEntry.ToString, "DocEntry")




            LlenarDetalleEmpaque(CStr(row.Cells("OrdenDeVenta").Value).ToString, DocEntry2)

            If (Me.dgvEmpaque.Columns(e.ColumnIndex).Name = "Imprimir" And row.Cells("Accion").Value = "Empacando") Then
    ''MANDA A LLAMAR EL METODO DE IMPRIMIR
    MImprimeFormato(CStr(row.Cells("OrdenEmpaque").Value).ToString)
   ElseIf (Me.dgvEmpaque.Columns(e.ColumnIndex).Name = "Imprimir" And row.Cells("Accion").Value = "Empacado") Then
    MImprimeFormato(row.Cells("OrdenEmpaque").Value)
   ElseIf Me.dgvEmpaque.Columns(e.ColumnIndex).Name = "Imprimir" And row.Cells("Accion").Value = "Por Empacar" Then




                'SE AGREGA EL ESTATUS TEMPORALMENTE PARA REVISAR LO QUE SE VA A EMPACAR
                If MessageBox.Show("¿Realmente deseas imprimir la orden de entrega [" & CStr(row.Cells("OrdenEmpaque").Value).ToString & "] y empezar la revisión?", "Pregunta...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    'MODIFICAR PARA QUE SE HAGA EL UPDATE CON EL NUEVO ESTATUS
                    'INGRESAR LA CLAVE DE RUBEN

                    'frmAccesoRevision.Show()

                    DocNumAccesoRev = row.Cells("OrdenEmpaque").Value.ToString()


                    Using frmA As New frmAccesoRevision()
                        'MANDA MOSTRAR EL FORMULARIO DE ACCESO
                        Dim dr As DialogResult = frmA.ShowDialog(Me)


                        If Module1.Bandera = True Then

                            SQL.conectarTPM()


                            MImprimeFormato(CStr(row.Cells("OrdenEmpaque").Value).ToString)
                            'ACTUALIZAR ESTATUS
                            'SQL.EjecutarComando("UPDATE Operacion_Entrega SET Action = 'En Revision' WHERE DocEntry = " + DocNumAccesoRev)
                            'ACTUALIZAR TIEMPO
                            SQL.EjecutarComando("UPDATE Operacion_Analisis SET TimeEnRevision = GETDATE() WHERE DocEntry = " + dgvEmpaque.CurrentRow.Cells("OrdenDeVenta").Value.ToString())

                            'SQL.EjecutarComando("UPDATE Operacion_Entrega SET UserId_Revisado ='" + Usuario + "' WHERE DocNum = " + DocEntry)


                            'row.Cells("Accion").Value = "En revisión"
                            LlenarEmpaque()
                            SQL.Cerrar()
                        Else

                        End If
                    End Using


                Else
                    MsgBox("Dijo que NO")
                    'Si se selecciono que no se desea entonces no se manda la impresion
                    'MImprimeFormato(CStr(row.Cells("OrdenEmpaque").Value).ToString)
                End If
    'MImprimeFormato(CStr(row.Cells("OrdenEmpaque").Value).ToString)
   ElseIf (Me.dgvEmpaque.Columns(e.ColumnIndex).Name = "Imprimir" And row.Cells("Accion").Value = "En Revision") Then
    'Graba el tiempo usado para revision
    'ACTUALIZAR ESTATUS
    SQL.EjecutarComando("UPDATE Operacion_Entrega SET Action = 'Revisado' WHERE DocEntry = " + dgvEmpaque.CurrentRow.Cells("OrdenEmpaque").Value.ToString())


    MImprimeFormato(row.Cells("OrdenEmpaque").Value)
   ElseIf (Me.dgvEmpaque.Columns(e.ColumnIndex).Name = "Imprimir" And row.Cells("Accion").Value = "Revisado") Then
    MImprimeFormato(row.Cells("OrdenEmpaque").Value)

   End If
  Catch ex As Exception
   MsgBox(ex.ToString)
  End Try
 End Sub
 ' Dim contadorPostPaint As Integer = 1
 Private Sub dgvEmpaque_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvEmpaque.RowPostPaint
  Try
   rojo = ColorTranslator.FromHtml("#FFC6C6")
   amarillo = ColorTranslator.FromHtml("#FDFF6C")
   verde = ColorTranslator.FromHtml("#A6FFA0")
   Anaranjado = ColorTranslator.FromHtml("#FFCC80")
   If dgvEmpaque.Rows.Count <> 0 Then
    'COLOCA COLORES A LAS FILAS PARA DIFERENCIAR EL SURTIDO, SURTIENDO Y PENDIENTE EN DADO CASO
    For i As Integer = 0 To dgvEmpaque.Rows.Count - 1
     'COMPARA EL ESTATUS
     If dgvEmpaque.Rows(i).Cells("Accion").Value.ToString = "Empacar" Then 'SE DEJA POR SI SE OCUPA                    
     ElseIf dgvEmpaque.Rows(i).Cells("Accion").Value.ToString = "Empacando" Then
      'COLOCA COLOR A LA CELDA U LA LETRA
      dgvEmpaque.Rows(i).DefaultCellStyle.BackColor = amarillo
     ElseIf dgvEmpaque.Rows(i).Cells("Accion").Value.ToString = "Empacado" Then
      'COLOCA COLOR A LA CELDA U LA LETRA
      dgvEmpaque.Rows(i).DefaultCellStyle.BackColor = verde
     ElseIf dgvEmpaque.Rows(i).Cells("Accion").Value.ToString = "Cancelado" Then
      dgvEmpaque.Rows(i).DefaultCellStyle.BackColor = rojo

      'NUEVOS ESTATUS
     ElseIf dgvEmpaque.Rows(i).Cells("Accion").Value.ToString = "En Revision" Then
      dgvEmpaque.Rows(i).DefaultCellStyle.BackColor = Color.SkyBlue
     ElseIf dgvEmpaque.Rows(i).Cells("Accion").Value.ToString = "Revisado" Then
      dgvEmpaque.Rows(i).DefaultCellStyle.BackColor = Color.LightPink


     End If
     'Console.WriteLine("Evento RowPostPaint se repite dentro del FOR: " + contadorPostPaint = contadorPostPaint + 1)
    Next
    'contadorPostPaint = 1
   End If
  Catch ex As Exception
   'MessageBox.Show("Error en el evento RowPostPaint: " + ex.ToString(), "¡Error TPD!", MessageBoxButtons.OK, MessageBoxIcon.Error)
  End Try
 End Sub

 Private Sub dgvEmpaque_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEmpaque.CellEnter
  Dim row As DataGridViewRow = dgvEmpaque.CurrentRow()
  DocEntry = row.Cells("OrdenEmpaque").Value
  LlenarDetalleEmpaque(CStr(row.Cells("OrdenDeVenta").Value).ToString, CStr(row.Cells("OrdenEmpaque").Value).ToString)
 End Sub

 Private Sub frmEstatusEmpaque_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
  LlenarEmpaque()
  dgvEmpaqueDetalle.DataSource = Nothing
  DocEntry_P = 0
  Console.WriteLine("Se actualizo el grid: " + DateTime.Now)

  Timer1.Enabled = True
 End Sub

 Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ReimpresionPL.Click

        Dim NumEntrega As String = InputBox("Indique el numero de entrega", "Numero de entrega")
            If (NumEntrega.Trim = "") Then Exit Sub

            If IsNumeric(NumEntrega) Then
                MImprimeFormato(NumEntrega)
            Else
                MsgBox("[" & NumEntrega & "] no es un número de entrega válido")
            End If


    End Sub

#End Region
End Class