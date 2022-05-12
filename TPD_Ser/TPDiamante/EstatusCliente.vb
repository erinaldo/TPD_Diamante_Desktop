Imports System.Data.SqlClient

Public Class EstatusCliente
 Private dvCliente, dvPagos, dvSaldo As New DataView
#Region "Eventos"

 ' Load 
 Private Sub EstatusCliente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
  Try
   'Dim sCliente As String
   'sCliente = String.Empty

   'sCliente = InputBox("Conoces el numero de cliente" + Environment.NewLine + Environment.NewLine +
   '                    Environment.NewLine +
   '                    Environment.NewLine + "-Digita el numero del cliente", "Tracto partes diamante")


   CodClienteEstatus.Close()

   Me.dtDel.Value = "01/01/2015"
   Me.dtAl.Value = Format(Date.Now, "dd/MM/yyyy")

   ' ' ' Metodo para cargar a los agentes 
   mCargaAgente()
   mCargaClientes()
   mbuscaCliente(Module1.sCliente)


  Catch ex As Exception
   MsgBox("Error al cargar el formulario:" + Environment.NewLine + "-Estatus del cliente ", MsgBoxStyle.Critical, "Tracto Partes Diamante")
  End Try
 End Sub

 ''' <summary>
 ''' 
 ''' Descripcion: Carga los agentes al combo de agentes
 ''' Fecha:07/05/2015
 ''' </summary>
 ''' <remarks></remarks>
 Private Sub mCargaAgente()
  Try
   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

    Dim da As New SqlDataAdapter("SELECT SlpCode,SlpName FROM oslp order by SlpName", SqlConnection)

    Dim ds As New DataSet
    da.Fill(ds)
    Me.cmbAgente.DataSource = ds.Tables(0)
    Me.cmbAgente.DisplayMember = "SlpName"
    Me.cmbAgente.ValueMember = "SlpCode"
    Me.cmbAgente.SelectedValue = -1
   End Using
  Catch ex As Exception
   MsgBox("Error al cargar en el metodo:" + Environment.NewLine + "-mCargaAgente() " +
                   Environment.NewLine + ex.ToString(), MsgBoxStyle.Critical, "Tracto Partes Diamante")
  End Try
 End Sub

 ' Combo de agente
 Private Sub cmbAgente_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAgente.SelectedIndexChanged
  Try
   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
    Dim cadena As String
    cadena = " SELECT CardCode Clave, CardName + ' ('+ CardCode+ ')' Nombre from OCRD clt " &
                                             " WHERE clt.CardType = 'C' "       'and clt.frozenfor='N'
    If cmbAgente.SelectedValue <> 0 Then
     cadena &= " and slpCode= '" & cmbAgente.SelectedValue & "'"
    End If
    cadena &= " ORDER BY clt.CardName "

    Dim da As New SqlDataAdapter(cadena, SqlConnection)
    Dim ds As New DataSet
    da.Fill(ds)
    Me.cmbCliente.DataSource = ds.Tables(0)
    Me.cmbCliente.DisplayMember = "Nombre"
    Me.cmbCliente.ValueMember = "Clave"
    ''Me.cmbCliente.SelectedValue = -1
   End Using
  Catch ex As Exception

  End Try

 End Sub

 ' Boton de generacion de Excel 
 Private Sub BtnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExcel.Click
  Try
   Dim exApp As New Microsoft.Office.Interop.Excel.Application
   Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

   'Añadimos el Libro al programa
   exLibro = exApp.Workbooks.Add

   ' ¿Cuantas columnas y cuantas filas?
   Dim NCol As Integer = dgRptEstatusClt.ColumnCount
   Dim NRow As Integer = dgRptEstatusClt.RowCount

   ''Combinamos celdas
   exLibro.Worksheets("Hoja1").Cells.Range("A1:h1").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("A2:h2").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("A3:h3").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("A4:h4").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("A5:h5").Merge(True)

   exLibro.Worksheets("Hoja1").Cells.Range("A7:h7").Merge(False)
   exLibro.Worksheets("Hoja1").Cells.Range("A8:d8").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("A9:d9").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("A10:d10").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("A11:d11").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("A12:d12").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("e8:h8").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("e9:h9").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("e10:g10").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("e11:g11").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("e12:g12").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("a13:d13").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("e13:g13").Merge(True)


   ''aplicamos un color de fondo ala celda o rango de celdas
   exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("A4").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("A5").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("A7").Interior.ColorIndex = 6
   exLibro.Worksheets("Hoja1").Cells.Range("A8").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("A9").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("A10").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("A11").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("A12").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("A13").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("e8").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("e9").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("e10").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("e11").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("e12").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("e13").Interior.ColorIndex = 15

   exLibro.Worksheets("Hoja1").Cells.Range("h10").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("h11").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("h12").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("h13").Interior.ColorIndex = 15

   ''Cambiamos orientacion a la hoja
   exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Reporte de estatus cliente"
   exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Clave del cliente: " + cmbCliente.SelectedValue
   exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Cliente: " + cmbCliente.Text
   exLibro.Worksheets("Hoja1").Cells.Item(4, 1) = "Del: " + dtDel.Value
   exLibro.Worksheets("Hoja1").Cells.Item(5, 1) = "Al: " + dtAl.Value

   exLibro.Worksheets("Hoja1").Cells.Item(7, 1) = " Datos generales del estatus del cliente"
   exLibro.Worksheets("Hoja1").Cells.Item(8, 1) = " Saldo del cliente: " + tSaldo.Text
   exLibro.Worksheets("Hoja1").Cells.Item(9, 1) = " Total facturado: " + tFacturado.Text
   exLibro.Worksheets("Hoja1").Cells.Item(10, 1) = " Dias de credito: " + tDiasCred.Text
   exLibro.Worksheets("Hoja1").Cells.Item(11, 1) = " Limite de credito: " + tLimCred.Text
   exLibro.Worksheets("Hoja1").Cells.Item(12, 1) = " Credito disponible: " + tCredito.Text

   exLibro.Worksheets("Hoja1").Cells.Item(8, 5) = " Pagos totales: " + tPagado.Text
   exLibro.Worksheets("Hoja1").Cells.Item(9, 5) = " Dias posteriores a vencimiento %"
   exLibro.Worksheets("Hoja1").Cells.Item(10, 5) = " Hasta 5 dias: " + tPeriodo1.Text
   exLibro.Worksheets("Hoja1").Cells.Item(11, 5) = " Hasta 15 dias " + tPeriodo2.Text
   exLibro.Worksheets("Hoja1").Cells.Item(12, 5) = " Hasta 30 dias: " + tPeriodo3.Text
   exLibro.Worksheets("Hoja1").Cells.Item(13, 5) = " Mas de 30 dias: " + tPeriodo4.Text


   exLibro.Worksheets("Hoja1").Cells.Item(10, 8) = lPorPer1.Text
   exLibro.Worksheets("Hoja1").Cells.Item(11, 8) = lPorPer2.Text
   exLibro.Worksheets("Hoja1").Cells.Item(12, 8) = lPorPer3.Text
   exLibro.Worksheets("Hoja1").Cells.Item(13, 8) = lPorPer4.Text

   exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(4, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(5, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(7, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(8, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(9, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(10, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(11, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(12, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(8, 5).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(9, 5).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(10, 5).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(11, 5).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(12, 5).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Range("h9").Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Range("h10").Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Range("h11").Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Range("h12").Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Range("h13").Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Range("e13").Font.Bold = 1

   'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
   For i As Integer = 1 To NCol
    exLibro.Worksheets("Hoja1").Cells.Item(15, i) = dgRptEstatusClt.Columns(i - 1).Name.ToString
   Next

   For Fila As Integer = 0 To NRow - 1
    For Col As Integer = 0 To NCol - 1
     exLibro.Worksheets("Hoja1").Cells.Item(Fila + 16, Col + 1) = dgRptEstatusClt.Rows(Fila).Cells(Col).Value
    Next
   Next

   'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
   exLibro.Worksheets("Hoja1").Rows.Item(15).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Rows.Item(15).HorizontalAlignment = 3
   exLibro.Worksheets("Hoja1").Rows.Item(15).Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Columns.AutoFit()
   exLibro.Worksheets("Hoja1").name = "Reporte de estatus cliente"

   'Aplicación visible
   exLibro.Worksheets.Application.Visible = True

   exLibro = Nothing
   exApp = Nothing

  Catch ex As Exception

  End Try
 End Sub

 ' Boton consultar
 Private Sub bConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConsultar.Click
  Try

   Dim sError As String

   sError = String.Empty
   If (fValidaDatos(sError)) Then
    mConsultaCliente(String.Empty, dtDel.Value, dtAl.Value)
    BtnExcel.Visible = True
   Else
    MsgBox("Verifique los siguientes campos: " + sError, MsgBoxStyle.Exclamation, "Tracto Partes Diamante")
   End If


  Catch ex As Exception
   MsgBox("Error al cargar en el metodo:" + Environment.NewLine + "-bConsultar_Click() " +
                 Environment.NewLine + ex.ToString(), MsgBoxStyle.Critical, "Tracto Partes Diamante")
  End Try
 End Sub


 ' boton limpiar
 Private Sub bLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bLimpiar.Click
  Dim sCliente As String
  sCliente = String.Empty

  sCliente = InputBox("Conoces el numero de cliente" + Environment.NewLine + Environment.NewLine +
                            Environment.NewLine +
                            Environment.NewLine + "-Digita el numero del cliente", "Tracto partes diamante")
  mbuscaCliente(sCliente)

 End Sub


 ' Pinta Grid 
 Private Sub dgRptEstatusClt_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgRptEstatusClt.RowPostPaint
  Try
   If IsDBNull(dgRptEstatusClt.Rows(e.RowIndex).Cells("Dias atraso").Value) Then
    dgRptEstatusClt.Rows(e.RowIndex).Cells("Dias atraso").Style.BackColor = Color.White
   Else
    Select Case dgRptEstatusClt.Rows(e.RowIndex).Cells("Dias atraso").Value
     Case -50000 To 0
                        'dgRptEstatusClt.Rows(e.RowIndex).Cells("Dias atraso").Style.BackColor = Color.White
     Case 1 To 5
      dgRptEstatusClt.Rows(e.RowIndex).Cells("Dias atraso").Style.BackColor = Color.GreenYellow
     Case 6 To 15
      dgRptEstatusClt.Rows(e.RowIndex).Cells("Dias atraso").Style.BackColor = Color.Yellow
     Case 16 To 30
      dgRptEstatusClt.Rows(e.RowIndex).Cells("Dias atraso").Style.BackColor = Color.Orange
     Case 31 To 50000
      dgRptEstatusClt.Rows(e.RowIndex).Cells("Dias atraso").Style.BackColor = Color.Red
    End Select
   End If
  Catch ex As Exception

  End Try


 End Sub

 Private Sub ckPenPago_CheckStateChanged(sender As Object, e As EventArgs) Handles ckPenPago.CheckStateChanged
  If ckPenPago.Checked Then
   dvCliente.RowFilter = " DocStatus ='O' "
  Else
   dvCliente.RowFilter = " DocStatus LIKE '%' and Factura<>'Totales' "
  End If

 End Sub

#End Region

#Region "Metodos"

 ''' <summary>
 ''' Descripcion: Metodo que consulta los pagos del cliente 
 '''              asi como la fecha y los dias de atraso que presento 
 ''' Fecha:07/05/2015
 ''' </summary>
 ''' <remarks></remarks>
 Private Sub mConsultaCliente(ByRef sCliente As String, ByVal ini As DateTime, ByVal fin As DateTime)
  'DECLARO VARIABLES LOCALES PARA ALMACENAR LA FECHA
  Dim fi As String
  Dim ff As String
  'ALMACENA LA FECHA A TRAER
  fi = dtDel.Value.ToString("yyyy-MM-dd")
  ff = dtAl.Value.ToString("yyyy-MM-dd")
  'INICIA EXCEPCION
  Try
   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
    'CREA LA CONEXIÓN
    mConsultaDatosCtl(SqlConnection)
    'VARIABLE QUE ALMACENA LAS CONSULTAS
    Dim sCadena As String
    'CREA TABLA TEMPORAL PARA ALMACENAR LAS FACTURAS
    sCadena = "DECLARE @FACT AS TABLE(DocNum  INT, NumAtCard VARCHAR(350), DocDate DATE, BaseEntry INT, BaseType INT, Comments VARCHAR(MAX),U_Factura VARCHAR(250)) "
    sCadena &= "INSERT INTO @FACT "
    'OBTINE LOS DATOS DE LAS FACTURAS 
    sCadena &= "SELECT DISTINCT T1.DocNum, T1.NumAtCard, T1.DocDate, T0.BaseEntry, T0.BaseType,T1.Comments,CONVERT(VARCHAR(250),U_FACTURA) "
    sCadena &= "FROM SBO_TPD.dbo.RIN1 T0 "
    sCadena &= "INNER JOIN SBO_TPD.dbo.ORIN T1 ON T0.DocEntry = T1.DocEntry "
    sCadena &= "WHERE T0.BaseType = 13 "
    'CREA TABLA PARA INSERCION DE DATOS
    sCadena &= "DECLARE @FactCancel AS TABLE( "
    sCadena &= "FACTURA INT, "
    sCadena &= "REFERENCIAFACT VARCHAR(350), "
    sCadena &= "FECHAFACT DATE, "
    sCadena &= "NCSAP INT, "
    sCadena &= "COMMENTS VARCHAR(350), "
    sCadena &= "REFERENCIANC VARCHAR(350), "
    sCadena &= "FECHANC DATE, "
    sCadena &= "U_FACTURA VARCHAR(250)) "
    'URIEL: VALIDA LAS FECHAS PARA OBTENCION DE DATOS
    If fi > ff Then 'VALIDA QUE LA FECHA INICAL NO PUEDE SER MAYOR A LA FINAL
     MsgBox("La fecha final no puede ser mayor a la Inicial")
    ElseIf (fi <= "2017-12-31" Or ff <= "2017-12-31") Then 'VALIDA QUE AMBAS FECHAS SEAN MENOR AL 1 DE ENERO DEL 2018
     sCadena &= "INSERT INTO @FactCancel(FACTURA,REFERENCIAFACT,FECHAFACT,NCSAP,COMMENTS,REFERENCIANC,FECHANC,U_FACTURA) "
     sCadena &= "Select DISTINCT "
     sCadena &= "T0.DocNum [Factura] ,T0.NumAtCard [Referencia] ,T0.DocDate [Fecha], "
     sCadena &= "T1.DocNum [NCSAP], T1.Comments,T1.NumAtCard [Referencia2], T1.DocDate [Fecha],T1.U_Factura "
     sCadena &= "FROM SBO_TPD.dbo.OINV T0 "
     sCadena &= "LEFT JOIN @FACT T1 ON T0.DocEntry = T1.BaseEntry AND T0.ObjType = T1.BaseType "
     'sCadena &= "WHERE T0.DocDate >= '20150101'  "
     sCadena &= "WHERE T0.DocDate >= '" + fi + "'  "
     sCadena &= "AND T1.DocNum IS NOT NULL AND T0.InvntSttus = 'C' "
     sCadena &= "ORDER BY 1 "
    ElseIf (fi >= "2018-01-01" Or ff >= "2018-01-01") Then 'VALDIA QUE AMBAS FECHAS SEAN MAYOR AL 1 DE ENERO DEL 2018
     sCadena &= "INSERT INTO @FactCancel(FACTURA,REFERENCIAFACT,FECHAFACT,NCSAP,COMMENTS,REFERENCIANC,FECHANC,U_FACTURA) "
     sCadena &= "Select DISTINCT "
     sCadena &= "T0.DocNum [Factura] ,T0.NumAtCard [Referencia] ,T0.DocDate [Fecha], "
     sCadena &= "T1.DocNum [NCSAP], T1.Comments,T1.NumAtCard [Referencia2], T1.DocDate [Fecha],T1.U_Factura "
     sCadena &= "FROM SBO_TPD.dbo.OINV T0 "
     sCadena &= "LEFT JOIN @FACT T1 ON T0.DocEntry = T1.BaseEntry AND T0.ObjType = T1.BaseType "
     sCadena &= "INNER JOIN SBO_TPD.dbo.ECM2 T2 ON T1.BaseEntry = T2.SrcObjAbs "
     'sCadena &= "WHERE T0.DocDate >= '20150101' "
     sCadena &= "WHERE T0.DocDate >= '" + ff + "' "
     'URIEL: MODIFICO ESTA LINEA SUSTITUYENDO T1.DocNum x T1.SrcObjAbs Y TAMBIEN SE AGREGO AL FINALIZAR LA LINEA *AND T2.ReportID IS NOT NULL
     sCadena &= "AND T2.SrcObjAbs IS NOT NULL AND T2.ReportID IS NOT NULL AND T0.U_BXP_UUID IS NOT NULL AND T0.InvntSttus = 'C'"
     sCadena &= "ORDER BY 1 "
    End If
    'URIEL: FIN VALIDA LAS FECHAS PARA OBTENCION DE DATOS

    'URIEL: REALIZO MODIFICACIONES A CONSULTA EN 2 LINEAS PARA OBTENER LOS SALDOS PENDIENTES
    sCadena &= " declare @tEtusCobranza table (Factura varchar(100), [Fecha fact] datetime,[Importe fact] decimal(13,2),[Saldo pendiente] decimal(13,2), " +
                          " Vencimiento datetime, [Pagos realizados] decimal(13,2),[Fecha pago] datetime,  [Dias atraso] int , DocStatus char(1), " +
                          " SlpCode varchar(100), CardCode varchar(50),TipoPago varchar(150),TipoDoc char(1), Timbre char(1), Series INT )  " +
                          " Insert @tEtusCobranza (Factura, [Fecha fact] , [Importe fact],[Saldo pendiente],  Vencimiento, [Pagos realizados], [Fecha pago], [Dias atraso], DocStatus,TipoPago,TipoDoc,Timbre,Series ) " +
                          " SELECT fact.DocNum Factura, fact.DocDate Fechafact, fact.DocTotal Importe,(fact.DocTotal- isnull(fact.PaidToDate,0.00)) [Saldo pendiente], " +
                          "	   fact.DocDueDate Vencimiento,  isnull(P.SumApplied,0.00) Pago,isnull(dtpag.DocDate,null) FechaDep," +
                          "	   DATEDIFF(day,fact.DocDueDate,isnull(dtpag.DocDate,CONVERT(date, GETDATE()))) Atraso, DocStatus  ,  " +
                          "	   CASE when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)<=5 then 'Verde'	 " +
                          "			when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)>=6 and isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)<=15 then 'Amarillo'	" +
                          "			when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)>=16 and isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)<=30 then 'Naranja' " +
                          "			when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)>=30 then 'Rojo' End ,'F', " +
                          "     CASE WHEN E1.ReportID IS NULL AND fact.U_BXP_UUID IS NULL THEN 'N' " +
                          "ELSE 'Y' END AS 'Timbre', fact.Series  " +
                          "FROM OINV fact " +
                          " LEFT JOIN ECM2 E1 ON fact.DocEntry = E1.SrcObjAbs " +
                          "     LEFT JOIN RCT2 P ON fact.DocEntry = P.DocEntry    " +
                          "     LEFT JOIN ORCT dtpag ON P.DocNum = dtpag.DocEntry  " + 'SE CAMBIA ESTA LINEA POR CORRECCION DE UNION DE TABLAS "     LEFT JOIN ORCT dtpag ON p.DocNum = dtpag.DocNum  " +
                          " where fact.DocDate between '" + ini.ToString("yyyy-MM-dd") + "' and '" + fin.ToString("yyyy-MM-dd") + "' and (E1.ReportID is not null OR fact.U_BXP_UUID IS NOT NULL)  "   '+SE MODIFICO ESTA LINEA PARA OBTENER TIMBRE
    '+--------------------------------------------------------CONSULTA ANTERIOR QUE YA NO FUNCIONABA------------------------------------------------------+
    'sCadena &= " declare @tEtusCobranza table (Factura varchar(100), [Fecha fact] datetime,[Importe fact] decimal(13,2),[Saldo pendiente] decimal(13,2), " +
    '          " Vencimiento datetime, [Pagos realizados] decimal(13,2),[Fecha pago] datetime,  [Dias atraso] int , DocStatus char(1), " +
    '          " SlpCode varchar(100), CardCode varchar(50),TipoPago varchar(150),TipoDoc char(1), Timbre char(1), Series INT )  " +
    '          " Insert @tEtusCobranza (Factura, [Fecha fact] , [Importe fact],[Saldo pendiente],  Vencimiento, [Pagos realizados], [Fecha pago], [Dias atraso], DocStatus,TipoPago,TipoDoc,Timbre,Series ) " +
    '          " SELECT fact.DocNum Factura, fact.DocDate Fechafact, fact.DocTotal Importe,(fact.DocTotal- isnull(fact.PaidToDate,0.00)) [Saldo pendiente], " +
    '          "	   fact.DocDueDate Vencimiento,  isnull(P.SumApplied,0.00) Pago,isnull(dtpag.DocDate,null) FechaDep," +
    '          "	   DATEDIFF(day,fact.DocDueDate,isnull(dtpag.DocDate,CONVERT(date, GETDATE()))) Atraso, DocStatus  ,  " +
    '          "	   CASE when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)<=5 then 'Verde'	 " +
    '          "			when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)>=6 and isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)<=15 then 'Amarillo'	" +
    '          "			when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)>=16 and isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)<=30 then 'Naranja' " +
    '          "			when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)>=30 then 'Rojo' End ,'F', " +
    '          "CASE WHEN fact.EDocNum IS NULL THEN 'N' " +
    '          "ELSE 'Y' END AS 'Timbre', fact.Series  " +
    '          "FROM OINV fact " +
    '          " " +
    '          "     LEFT JOIN RCT2 P ON fact.DocEntry = P.DocEntry    " +
    '          "     LEFT JOIN ORCT dtpag ON p.DocNum = dtpag.DocNum  " +
    '          " where fact.DocDate between '" + ini.ToString("yyyy-MM-dd") + "' and '" + fin.ToString("yyyy-MM-dd") + "' AND fact.EDOCNUM IS NOT NULL "   '+
    '+--------------------------------------------------------FIN CONSULTA ANTERIOR QUE YA NO FUNCIONABA------------------------------------------------------+

    '"AND FACT.Series <> 59 "  '+
    '"AND CONVERT(VARCHAR(50),fact.DocNum) NOT IN " +
    '"(SELECT U_Factura FROM @FactCancel) "

    If sCliente = String.Empty Then
     sCadena &= " and SlpCode='" + cmbAgente.SelectedValue.ToString() + "'" +
                               " and fact.CardCode='" + cmbCliente.SelectedValue.ToString() + "'"
    Else
     sCadena &= " and fact.CardCode='" + sCliente.ToString() + "'"
     sCliente = String.Empty
    End If

    sCadena &= " union  " +
                           "SELECT fact.DocNum Factura, fact.DocDate Fechafact, fact.DocTotal Importe,(fact.DocTotal- isnull(fact.PaidToDate,0.00)) [Saldo pendiente], " +
                           "	   fact.DocDueDate Vencimiento,  isnull(P.SumApplied,0.00) Pago,isnull(dtpag.DocDate,null) FechaDep,  " +
                           "	   DATEDIFF(day,fact.DocDueDate,isnull(dtpag.DocDate,CONVERT(date, GETDATE()))) Atraso, DocStatus  ,  " +
                           "	   CASE when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)<=5 then 'Verde' " +
                           "			when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)>=6 and isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)<=15 then 'Amarillo' " +
                           "			when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)>=16 and isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)<=30 then 'Naranja' " +
                           "			when isnull(DATEDIFF(day,fact.DocDueDate,dtpag.DocDate),0)>=30 then 'Rojo' End ,'F', " +
                           "CASE WHEN fact.EDocNum IS NULL THEN 'N' " +
                           "ELSE 'Y' END AS 'Timbre',fact.Series    " +
                           " FROM [SBO-Diamante-productiva].dbo.OINV fact " +
                           "   LEFT JOIN [SBO-Diamante-productiva].dbo.RCT2 P ON fact.DocEntry = P.DocEntry" +
                           "   LEFT JOIN [SBO-Diamante-productiva].dbo.ORCT dtpag ON p.DocNum = dtpag.DocEntry" +   'SE CAMBIA ESTA LINEA PARA OBTENER LA FECHA DEL PAGO "   LEFT JOIN [SBO-Diamante-productiva].dbo.ORCT dtpag ON p.DocNum = dtpag.DocNum" +
                           "   inner join [SBO-Diamante-productiva].dbo.OSLP agent on fact.SlpCode= agent.SlpCode" +
                           " where fact.DocDate between '" + ini.ToString("yyyy-MM-dd") + "' and '" + fin.ToString("yyyy-MM-dd") + "'"
    If sCliente = String.Empty Then
     sCadena &= " and agent.Memo='" + cmbAgente.SelectedValue.ToString() + "'" +
                               " and fact.CardCode='" + cmbCliente.SelectedValue.ToString() + "'"
    Else
     sCadena &= " and fact.CardCode='" + sCliente.ToString() + "'"
     sCliente = String.Empty
    End If

    sCadena &= " union  " +
                            " select DocNum Factura,DocDate fechafact,DocTotal*-1 importe,PaidToDate-DocTotal,DocDueDate Vencimiento,0 Pago,null FechaDep,null Atraso," +
                            " DocStatus ,'Verde','N', " +
                            "CASE WHEN EDocNum IS NULL THEN 'N' " +
                            "ELSE 'Y' END AS 'Timbre',Series " +
                            " from orin " +
                            " where DocDueDate between '" + ini.ToString("yyyy-MM-dd") + "' and '" + fin.ToString("yyyy-MM-dd") + "' " + ""
    If sCliente = String.Empty Then
     sCadena &= " and SlpCode='" + cmbAgente.SelectedValue.ToString() + "'" +
                               " and CardCode='" + cmbCliente.SelectedValue.ToString() + "'"
    Else
     sCadena &= " and CardCode='" + sCliente.ToString() + "'"
     sCliente = String.Empty
    End If

    sCadena &= " union  " +
                           " select DocNum Factura,DocDate fechafact,DocTotal*-1 importe,PaidToDate-DocTotal,DocDueDate Vencimiento,0 Pago,null FechaDep,null Atraso, " +
                           " DocStatus ,'Verde','N', " +
                           "CASE WHEN nc.EDocNum IS NULL THEN 'N' " +
                           "ELSE 'Y' END AS 'Timbre',nc.Series " +
                           " from [SBO-Diamante-productiva].dbo.orin nc " +
                           "    inner join [SBO-Diamante-productiva].dbo.OSLP agent on nc.SlpCode= agent.SlpCode " +
                           " where DocDueDate between '" + ini.ToString("yyyy-MM-dd") + "' and '" + fin.ToString("yyyy-MM-dd") + "' " + ""
    If sCliente = String.Empty Then
     sCadena &= " and agent.Memo='" + cmbAgente.SelectedValue.ToString() + "'" +
                               " and nc.CardCode='" + cmbCliente.SelectedValue.ToString() + "'"
    Else
     sCadena &= " and nc.CardCode='" + sCliente.ToString() + "'"
     sCliente = String.Empty
    End If

    sCadena &= " AND DocStatus='O'" +
                           " update @tEtusCobranza" +
                           " 	set SlpCode= act.SlpCode " +
                           " from @tEtusCobranza fac " +
                           "left join oinv act on  fac.CardCode=act.CardCode "

    '**CONSULTA 1
    sCadena &= " select Factura,[Fecha fact],[Importe fact],Vencimiento,[Pagos realizados],[Saldo pendiente], [Fecha pago], [Dias atraso],DocStatus  " +
                           " INTO #EstatusClientes from @tEtusCobranza " +
                           " where TipoDoc='F' AND Factura NOT IN (SELECT factura FROM @FactCancel) AND Series<>59 " +
                           " union all " +
                           " select Factura, [Fecha fact], [Importe fact],Vencimiento,[Pagos realizados],[Saldo pendiente], [Fecha pago], [Dias atraso],DocStatus " +
                           " from @tEtusCobranza " +
                           " where TipoDoc='N' and [Saldo pendiente]<0  "
    '" union all " +
    '" select 'Total facturado',null,SUM([Importe fact]),'20991230',SUM([Pagos realizados]),null,null, null, 'C'   " +
    '" from @tEtusCobranza  " +
    '" where DocStatus in ('C','O') " +
    sCadena &= "ORDER BY Vencimiento, [Importe fact] DESC "

    sCadena &= " select Factura,[Importe fact],SUM([Pagos realizados]) Pagos,TipoPago "
    sCadena &= " into #pagos "
    sCadena &= " from @tEtusCobranza  "
    sCadena &= " group by Factura,[Importe fact],TipoPago    "

    sCadena &= " select Factura,[Importe fact]  "
    sCadena &= " into #Factura "
    sCadena &= " from #EstatusClientes where [Pagos realizados] <> 0.00 or [Saldo pendiente] <> 0.00 "
    sCadena &= " GROUP BY Factura,[Importe fact]  "

    sCadena &= "DECLARE @TotImporteFact DECIMAL(19,2)=(SELECT SUM([Importe fact]) FROM #Factura) "


    sCadena &= " SELECT * FROM #EstatusClientes where [Pagos realizados] <> 0.00 or [Saldo pendiente] <> 0.00 "
    sCadena &= " union all "
    sCadena &= " select 'Total facturado',null,@TotImporteFact,'20991230',SUM([Pagos realizados]),SUM([Saldo pendiente]),null, null, 'C' "
    sCadena &= " from #EstatusClientes where [Pagos realizados] <> 0.00 or [Saldo pendiente] <> 0.00 "

    'CONSULTA 2
    sCadena &= " select SUM(Pagos) PagoT,TipoPago  from #pagos "
    sCadena &= " group by TipoPago "
    sCadena &= " union all "
    sCadena &= " select SUM(Pagos),'Total' from #pagos "



    'CONSULTA 3
    sCadena &= " select SUM([Importe fact]) importe "
    sCadena &= " from #Factura "

    sCadena &= " drop table #pagos "
    sCadena &= " drop table #Factura "
    sCadena &= "DROP TABLE #EstatusClientes "

    Dim da As New SqlDataAdapter(sCadena, SqlConnection)

    Dim ds As New DataSet
    'URIEL: AQUI TUVE QUE LIMPIAR LAS TABLAS DEBIDO QUE AL HACER OTRA CONSULTA MANDABA ERROR = "INCORRECT SYNTAX NEAR 'NULLORDER'"
    ds.Tables.Clear()
    da.SelectCommand.CommandTimeout = 300000
    da.Fill(ds)
    dvCliente.Table = ds.Tables(0)
    dvPagos.Table = ds.Tables(1)
    dvSaldo.Table = ds.Tables(2)

    GridEC(dgRptEstatusClt, dvCliente)
    fFormatoGrid()

    If ckPenPago.Checked Then
     dvCliente.RowFilter = "DocStatus ='O'"
    Else
     dvCliente.RowFilter = " DocStatus LIKE '%' and Factura<>'Totales' "
    End If

    lInfClinte.Text = cmbCliente.Text.ToString

    lInfClinte.BackColor = Color.White

    fLimpiatxt()


    For index = 0 To (ds.Tables(1).Rows.Count - 1)
     If ds.Tables(1).Rows(index).Item(1).ToString = "Verde" Then
      tPeriodo1.Text = " " + DirectCast(ds.Tables(1).Rows(index).Item(0), Decimal).ToString("$ ###,###,####.00")
      tPeriodo1.BackColor = Color.GreenYellow
     End If
     If ds.Tables(1).Rows(index).Item(1).ToString = "Amarillo" Then
      tPeriodo2.Text = " " + DirectCast(ds.Tables(1).Rows(index).Item(0), Decimal).ToString("$ ###,###,####.00")
      tPeriodo2.BackColor = Color.Yellow
     End If
     If ds.Tables(1).Rows(index).Item(1).ToString = "Naranja" Then
      tPeriodo3.Text = " " + DirectCast(ds.Tables(1).Rows(index).Item(0), Decimal).ToString("$ ###,###,####.00")
      tPeriodo3.BackColor = Color.Orange
     End If
     If ds.Tables(1).Rows(index).Item(1).ToString = "Rojo" Then
      tPeriodo4.Text = " " + DirectCast(ds.Tables(1).Rows(index).Item(0), Decimal).ToString("$ ###,###,####.00")
      tPeriodo4.BackColor = Color.Red
     End If
     If ds.Tables(1).Rows(index).Item(1).ToString = "Total" Then
      tPagado.Text = " " + DirectCast(ds.Tables(1).Rows(index).Item(0), Decimal).ToString("$ ###,###,####.00")
     End If
    Next


    tFacturado.Text = " " + DirectCast(ds.Tables(2).Rows(0).Item(0), Decimal).ToString("$ ###,###,####.00")
    mCalculaPorcentajes()
   End Using
  Catch ex As Exception
   MsgBox(ex.Message)
  End Try
 End Sub

 ''' <summary>
 ''' Descripcion: Consulta la linea de credito y lo dias que tiene el cliente para pagar 
 ''' Fecha:07/05/2015
 ''' </summary>
 ''' <remarks></remarks>
 Private Sub mConsultaDatosCtl(ByVal conexion As Data.SqlClient.SqlConnection)

  Dim da As New SqlDataAdapter(" select CreditLine,PymntGroup, detCtl.Balance,CreditLine- Balance CredVig  from ocrd detCtl " +
                                     " inner join octg d on detCtl.GroupNum= d.GroupNum " +
                                     " where CardCode='" + cmbCliente.SelectedValue + "'", conexion)

  Dim ds As New DataSet
  da.Fill(ds)

  tLimCred.Text = " " + DirectCast(ds.Tables(0).Rows(0).Item(0), Decimal).ToString("$ ###,###,####.00")
  tDiasCred.Text = " " + DirectCast(ds.Tables(0).Rows(0).Item(1), String)
  tSaldo.Text = " " + DirectCast(ds.Tables(0).Rows(0).Item(2), Decimal).ToString("$ ###,###,####.00")
  tCredito.Text = " " + DirectCast(ds.Tables(0).Rows(0).Item(3), Decimal).ToString("$ ###,###,####.00")

 End Sub

 ''' <summary>
 ''' Descripcion:Consulta el agente que corresponde al cliente solicitado
 ''' Fecha:07/05/2015
 ''' </summary>
 ''' <remarks></remarks>
 Private Sub mDatosCliente(ByVal sCliente As String)
  Try
   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

    Dim da As New SqlDataAdapter(" Select SlpCode " +
                " from ocrd " +
                " where CardCode= '" + sCliente + "'", SqlConnection)

    Dim ds As New DataSet
    da.Fill(ds)

    cmbAgente.SelectedValue = DirectCast(ds.Tables(0).Rows(0).Item(0), Integer)

    cmbCliente.SelectedValue = sCliente

   End Using

  Catch ex As Exception

  End Try
 End Sub

#End Region

#Region "Funciones"

 ''' <summary>
 ''' Autor: Ivan Cordero Ramos
 ''' Descripcion: calida las fechas y que ayan seleccionado un agente
 ''' Fecha:07/05/2015
 ''' </summary>
 ''' <remarks></remarks>
 Private Function fValidaDatos(ByRef sError) As Boolean
  Try
   Dim bValido As Boolean
   bValido = True
   If (DateDiff("m", dtDel.Value, dtAl.Value) = -1 Or (dtDel.Value > dtAl.Value)) Then
    sError = sError + Environment.NewLine + "-La fecha final no puede ser mayor ala fecha inicial."
    bValido = False
    eFin.Visible = True
    eIni.Visible = True
   Else
    eIni.Visible = False
    eFin.Visible = False
   End If

   If cmbAgente.SelectedValue = -1 Then
    sError = sError + Environment.NewLine + "-Debe seleccionar el agente"
    bValido = False
    eAgen.Visible = True
   End If

   Return bValido
  Catch ex As Exception
   MsgBox("Error al cargar en la funcion :" + Environment.NewLine + "-fValidaFecha() " +
                Environment.NewLine + ex.ToString(), MsgBoxStyle.Critical, "Tracto Partes Diamante")
  End Try
 End Function

 ''' <summary>
 ''' Autopr: Ivan Cordero Ramos 
 ''' Descripcion: Se da formato al grid del estatus del cliente 
 ''' Fecha: 11/05/2015
 ''' </summary>
 ''' <remarks></remarks>
 Private Sub fFormatoGrid()
  Try
   With dgRptEstatusClt

    'Factura
    .Columns(0).HeaderText = "Factura"
    .Columns(0).Width = 75
    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

    ' Fecha de facturacion	
    .Columns(1).HeaderText = "Fecha facturada"
    .Columns(1).Width = 75
    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    'Importe
    .Columns(2).HeaderText = "Importe facturado"
    .Columns(2).Width = 100
    .Columns(2).DefaultCellStyle.Format = "$ ###,###,###.00"
    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    'Fecha de vencimiento 
    .Columns(3).HeaderText = "Fecha vencimiento"
    .Columns(3).Width = 75
    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    'Pago
    .Columns(4).HeaderText = "Pagos realizados"
    .Columns(4).Width = 100
    .Columns(4).DefaultCellStyle.Format = "$ ###,###,###.00"
    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    'Adeudo
    .Columns(5).HeaderText = "Saldo pendiente"
    .Columns(5).Width = 100
    .Columns(5).DefaultCellStyle.Format = "$ ###,###,###.00"
    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    'Fecha pago
    .Columns(6).HeaderText = "Fecha pago"
    .Columns(6).Width = 75
    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    'Dias de pago 
    .Columns(7).HeaderText = "Dias atraso"
    .Columns(7).Width = 75
    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    'Estado de la Factura
    .Columns(8).Width = 75
    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(8).Visible = False


   End With

  Catch ex As Exception
   MsgBox("Error al aplicar algun estilo al grid", MsgBoxStyle.Critical, "Tracto Partes Diamante")
  End Try
 End Sub
 Public Function GridEC(ByVal Grid As DataGridView, ByRef ds As DataView) As Boolean

  With Grid
   .DataSource = ds
   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .RowHeadersVisible = True
   .SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
   .MultiSelect = True
   .AllowUserToAddRows = False
   .ReadOnly = True
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
   .ColumnHeadersHeight = 39
   'Superior Izquierda del gridview
   '.RowHeadersVisible = False
   .RowHeadersWidth = 25
  End With
 End Function

#End Region

 Private Sub mCalculaPorcentajes()
  Dim iPeriodo, iTotal As Decimal
  iTotal = tPagado.Text

  If tPeriodo1.Text <> String.Empty Then
   iPeriodo = tPeriodo1.Text
   lPorPer1.Text = "% " + Format(iPeriodo * 100 / iTotal, "##0.00")
   lPorPer1.BackColor = Color.White
  Else
   lPorPer1.BackColor = Color.LightSteelBlue
   lPorPer1.Text = String.Empty
  End If

  If tPeriodo2.Text <> String.Empty Then
   iPeriodo = tPeriodo2.Text
   lPorPer2.Text = "% " + Format(iPeriodo * 100 / iTotal, "##0.00")
   lPorPer2.BackColor = Color.White
  Else
   lPorPer2.BackColor = Color.LightSteelBlue
   lPorPer2.Text = String.Empty
  End If

  If tPeriodo3.Text <> String.Empty Then
   iPeriodo = tPeriodo3.Text
   lPorPer3.Text = "% " + Format(iPeriodo * 100 / iTotal, "##0.00")
   lPorPer3.BackColor = Color.White
  Else
   lPorPer3.BackColor = Color.LightSteelBlue
   lPorPer3.Text = String.Empty
  End If

  If tPeriodo4.Text <> String.Empty Then
   iPeriodo = tPeriodo4.Text
   lPorPer4.Text = "% " + Format(iPeriodo * 100 / iTotal, "##0.00")
   lPorPer4.BackColor = Color.White
  Else
   lPorPer4.BackColor = Color.LightSteelBlue
   lPorPer4.Text = String.Empty
  End If

 End Sub

 Public Function fLimpiatxt() As Boolean
  tPeriodo1.Text = String.Empty
  tPeriodo2.Text = String.Empty
  tPeriodo3.Text = String.Empty
  tPeriodo4.Text = String.Empty
  tPeriodo1.BackColor = Color.White
  tPeriodo2.BackColor = Color.White
  tPeriodo3.BackColor = Color.White
  tPeriodo4.BackColor = Color.White

  lPorPer1.BackColor = Color.LightSteelBlue
  lPorPer1.Text = String.Empty
  lPorPer2.BackColor = Color.LightSteelBlue
  lPorPer2.Text = String.Empty
  lPorPer3.BackColor = Color.LightSteelBlue
  lPorPer3.Text = String.Empty
  lPorPer4.BackColor = Color.LightSteelBlue
  lPorPer4.Text = String.Empty

 End Function

 Private Sub mbuscaCliente(sCliente As String)
  If sCliente <> String.Empty Then
   mDatosCliente(sCliente)
   mConsultaCliente(sCliente, dtDel.Value, dtAl.Value)
   tCredito.Text = Format(tLimCred.Text - tSaldo.Text, "$ ###,###,####.00")
   BtnExcel.Visible = True
   cmbCliente.Visible = True
  End If
 End Sub

 Private Sub mCargaClientes()
  Try
   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
    Dim cadena As String
    cadena = " SELECT CardName + ' ('+ CardCode+ ')' Nombre,CardCode Clave from OCRD clt " &
                                             " WHERE clt.CardType = 'C'  " +
                         " ORDER BY clt.CardName "
    'and clt.frozenfor='N'
    Dim da As New SqlDataAdapter(cadena, SqlConnection)
    Dim ds As New DataSet
    da.Fill(ds)
    ''ds.Tables(0).Rows.Add("TODOS", "0")
    Me.cmbCliente.DataSource = ds.Tables(0)
    Me.cmbCliente.DisplayMember = "Nombre"
    Me.cmbCliente.ValueMember = "Clave"
    Me.cmbCliente.SelectedValue = "P-014"
   End Using
  Catch ex As Exception

  End Try
 End Sub

 Private Sub cmbCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCliente.SelectedIndexChanged
  Try
   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

    Dim da As New SqlDataAdapter(" Select SlpCode " +
                " from ocrd " +
                " where CardCode= '" + cmbCliente.SelectedValue + "'", SqlConnection)

    Dim ds As New DataSet
    da.Fill(ds)

    cmbAgente.SelectedValue = DirectCast(ds.Tables(0).Rows(0).Item(0), Integer)



   End Using

  Catch ex As Exception

  End Try
 End Sub

End Class