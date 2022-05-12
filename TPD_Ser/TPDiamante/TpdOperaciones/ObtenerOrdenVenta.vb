Imports System.Data.SqlClient

Public Class ObtenerOrdenVenta

 Dim ResultadoOrden As DataView
 Dim Renglonactual As Integer

 Private Sub btnCancelarOV_Click(sender As Object, e As EventArgs) Handles btnCancelarOV.Click
  Timer1.Enabled = False
  Me.Close()
 End Sub

 Private Sub ObtenerOrdenVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
  BuscarNuevasOV()
  Timer1.Enabled = True
 End Sub

 Sub BuscarNuevasOV()
  'Este metodo llena el gridOperacionVta  con un procedimiento almacenado
  Try
   'VARIABLES DE CONEXION DE LLENADO
   Dim cmd As SqlCommand
   Dim cnn As SqlConnection = Nothing
   Dim daOV As SqlDataAdapter
   Dim DsOrdenesVenta = New DataSet
   cnn = New SqlConnection(StrTpm)
   'ALMACENA LA CONSULTA EN UN COMMAND
   cmd = New SqlCommand("TPD_GETOrdenesVenta", cnn)
   'Pasa los parametros del procedimiento almacenado 
   cmd.CommandType = CommandType.StoredProcedure
   'Id del telemarketing actual
   cmd.Parameters.AddWithValue("@AgenteTelemarketing", UsrTPM)
   cnn.Open()
   'INSTANCIA UN ADAPTER
   daOV = New SqlDataAdapter
   'ALMACENA EL COMMAND DE SQL EN EL ADAPTER
   daOV.SelectCommand = cmd
   'LO EJECUTA CON LA CONEXION
   daOV.SelectCommand.Connection = cnn
   'TIEMPO DE ESPERA DE LA CONEXION
   daOV.SelectCommand.CommandTimeout = 10000
   'EJECUTA LA CONSULTA
   cmd.ExecuteNonQuery()
   'CIERRA EL COMMAND DE SQL
   cmd.Connection.Close()
   'CIERRA LA CONEXION
   cnn.Close()
   'LLENA EL ADAPTER A UN DATA SET
   daOV.Fill(DsOrdenesVenta, "OVs")

   'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
   ResultadoOrden = New DataView
   'ALMACENA EN DATA SET DE MODO TABLA
   ResultadoOrden.Table = DsOrdenesVenta.Tables(0)
   'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
   dgvNuevaOV.DataSource = ResultadoOrden
   'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES DE VENTA
   EstiloDgvOrdenesVenta()

   txtSerie.Text = ""
   txtFolio.Text = ""

  Catch ex As Exception
   MsgBox("Error: " + ex.ToString)
  End Try
 End Sub

 Sub EstiloDgvOrdenesVenta()
  'cambia el estilo de las columnas del gridview detalle operacion  
  'Cambiar el estido del campo Orden de Venta a Negritas
  Dim style As New DataGridViewCellStyle
  'style.Font = New Font(dgvOperacionDetalle.Font, FontStyle.Bold)
  With Me.dgvNuevaOV
   'COLOCA PROPIEDADES DE COLOR ALTERNADOS
   .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionForeColor = Color.White
   .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
   .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   'Serie
   .Columns("Serie").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("Serie").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
   .Columns("Serie").DefaultCellStyle = style
   .Columns("Serie").HeaderText = "Serie"
   .Columns("Serie").Width = 30
   .Columns("Serie").ReadOnly = True
   'IdOrdenVenta
   .Columns("IdOrdVta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("IdOrdVta").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
   .Columns("IdOrdVta").DefaultCellStyle = style
   .Columns("IdOrdVta").HeaderText = "Id Orden Venta"
   .Columns("IdOrdVta").Width = 50
   .Columns("IdOrdVta").ReadOnly = True
   'Id Cliente
   .Columns("IdCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
   .Columns("IdCliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("IdCliente").HeaderText = "Id Cliente"
   .Columns("IdCliente").Width = 50
   .Columns("IdCliente").ReadOnly = True
   'Nombre Cliente
   .Columns("DesClte").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
   .Columns("DesClte").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("DesClte").HeaderText = "Cliente"
   .Columns("DesClte").Width = 250
   .Columns("DesClte").ReadOnly = True
   'Importe total
   .Columns("DocTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
   .Columns("DocTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
   .Columns("DocTotal").HeaderText = "Total documento"
   .Columns("DocTotal").DefaultCellStyle.Format = "$ ###,###,###.00"
   .Columns("DocTotal").Width = 80
   .Columns("DocTotal").ReadOnly = True
   .Columns("DocTotal").Frozen = True
  End With
 End Sub

 Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
  BuscarNuevasOV()
  Try
   dgvNuevaOV.Rows(Renglonactual).Selected = True
  Catch ex As Exception

  End Try
 End Sub

 Private Sub ObtenerOrdenVenta_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
  Dim dr As DialogResult = MessageBox.Show("¿Realmente desea salir de las ordenes de venta?", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
  If (dr = DialogResult.No) Then
   ' Cancelamos el cierre del formulario.
   e.Cancel = True
  Else
   Timer1.Enabled = False
   Me.Dispose()
  End If
 End Sub

 Private Sub dgvNuevaOV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvNuevaOV.CellContentClick

 End Sub

 Private Sub dgvNuevaOV_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvNuevaOV.RowEnter
  If e.RowIndex > 0 Then
   Renglonactual = e.RowIndex
  End If
 End Sub

 Private Sub btnPrintOV_Click(sender As Object, e As EventArgs) Handles btnPrintOV.Click
  Dim Serie As String
  Dim Folio As Int16

  If txtSerie.Text.Trim() <> "" And txtFolio.Text.Trim() <> "" Then
   ImprimirOV(txtSerie.Text.Trim(), txtFolio.Text.Trim())
  Else
   If (dgvNuevaOV.SelectedRows.Count > 0) Then
    For Each row As DataGridViewRow In dgvNuevaOV.Rows
     If row.Selected Then
      If IsNothing(row.Cells(0).Value) = False Then
       Serie = row.Cells(0).Value.ToString
       Folio = Integer.Parse(row.Cells(1).Value.ToString)

       If Trim(Serie) <> "" And Folio > 0 Then
        ImprimirOV(Serie, Folio)
       End If
      End If
     End If
    Next
   End If
  End If
 End Sub

 Private Sub ImprimirOV(Serie As String, Folio As Integer)
  'CAMPOS DEL DATASET 
  Dim ErrOV As Integer = 0

  Try
   Dim DtOVta As New DataTable("OrdVenta")
   Dim dTable As New DataTable
   Dim detTable As New DataTable
   Dim dgvOVC As New DataGridView
   Dim dtvOvC As New DataView
   Dim dsOVC As New DataSet
   Dim dsOVD As New DataSet

   DtOVta.Columns.Add("IdOrdVta", Type.GetType("System.Int32"))
   DtOVta.Columns.Add("Serie", Type.GetType("System.String"))
   DtOVta.Columns.Add("FchOVta", Type.GetType("System.DateTime"))
   DtOVta.Columns.Add("UsrOVta", Type.GetType("System.String"))
   DtOVta.Columns.Add("Agente", Type.GetType("System.String"))
   DtOVta.Columns.Add("NomAgente", Type.GetType("System.String"))
   DtOVta.Columns.Add("IdCliente", Type.GetType("System.String"))
   DtOVta.Columns.Add("DesClte", Type.GetType("System.String"))
   DtOVta.Columns.Add("IdTrnsp", Type.GetType("System.Int32"))
   DtOVta.Columns.Add("DesTrnsp", Type.GetType("System.String"))
   DtOVta.Columns.Add("PerCon", Type.GetType("System.String"))
   DtOVta.Columns.Add("Comen", Type.GetType("System.String"))
   DtOVta.Columns.Add("Direccion", Type.GetType("System.String"))
   DtOVta.Columns.Add("Colonia", Type.GetType("System.String"))
   DtOVta.Columns.Add("CP", Type.GetType("System.String"))
   DtOVta.Columns.Add("Ciudad", Type.GetType("System.String"))
   DtOVta.Columns.Add("Estado", Type.GetType("System.String"))
   DtOVta.Columns.Add("Pais", Type.GetType("System.String"))
   DtOVta.Columns.Add("Rfc", Type.GetType("System.String"))
   DtOVta.Columns.Add("NumLinea", Type.GetType("System.Int32"))
   DtOVta.Columns.Add("Articulo", Type.GetType("System.String"))
   DtOVta.Columns.Add("Linea", Type.GetType("System.String"))
   DtOVta.Columns.Add("DesArt", Type.GetType("System.String"))
   DtOVta.Columns.Add("ListaP", Type.GetType("System.Int32"))
   DtOVta.Columns.Add("Precio", Type.GetType("System.Decimal"))
   DtOVta.Columns.Add("Cantidad", Type.GetType("System.Int32"))
   DtOVta.Columns.Add("DescLin", Type.GetType("System.Decimal"))
   DtOVta.Columns.Add("Totlinea", Type.GetType("System.Decimal"))
   DtOVta.Columns.Add("DocAntIva", Type.GetType("System.Decimal"))
   DtOVta.Columns.Add("DocIva", Type.GetType("System.Decimal"))
   DtOVta.Columns.Add("DocTotal", Type.GetType("System.Decimal"))

   'SE AGREGAN ESTOS DOS CAMBIOS PARA MONEDA Y TIPO DE DATO: URIEL TORALVA 19/05/2018
   DtOVta.Columns.Add("TipoCambio", Type.GetType("System.String"))
   DtOVta.Columns.Add("Moneda", Type.GetType("System.String"))
   DtOVta.Columns.Add("NumLetra", Type.GetType("System.String")) 'COLOCA EL NUMERO CON LETRA

   Dim columnas As DataColumnCollection = DtOVta.Columns

   Dim series As String = ""
   Dim _filaTemp As DataRow
   Dim numOV As Integer
   Dim fechaOV As String
   Dim UsuarioOV As String
   Dim vTotIva As Decimal = 0
   Dim vTotSIva As Decimal = 0
   Dim vTotDoc As Decimal = 0
   Dim vSinValor As Integer = 0
   Dim Fila As Integer = 0

   Dim idAgte, Agente As String
   Dim idCte, Cliente As String
   Dim idTransporte, formaEnvio As String
   Dim Contacto, Comentario As String
   Dim Direccion, Colonia, CP, Ciudad, Estado, Pais, Rfc As String

   Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
    'Consulta para traer la OV
    Dim ConsutaLista As String
    ConsutaLista = "SELECT *"
    ConsutaLista &= " FROM OrdVta"
    ConsutaLista &= " WHERE Serie = '" & Serie & "' AND IdOrdVta = " & Folio

    Dim daOVC As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)
    daOVC.Fill(dsOVC, "OCabecera")
    daOVC.Fill(dTable)

    For Each row As DataRow In dTable.Rows
     vTotIva = row("DocIva")
     vTotSIva = row("SubTot")
     vTotDoc = row("DocTotal")
     numOV = row("IdOrdVta")
     vSerie = row("Serie")
     UsuarioOV = row("UsrOVta")
     fechaOV = row("FchOVta")
     idAgte = row("Agente")
     Agente = row("NomAgente")
     idCte = row("IdCliente")
     Cliente = row("DesClte")
     idTransporte = row("IdTrnsp")
     formaEnvio = row("DesTrnsp")
     Contacto = row("PerCon")
     Comentario = row("Coment")
     Direccion = row("Direccion")
     Colonia = row("Colonia")
     CP = row("CP")
     Ciudad = row("Ciudad")
     Estado = row("Estado")
     Pais = row("Pais")
     Rfc = row("Rfc")
    Next
   End Using
   'CONVIERTE EL IMPORTE DE NUMERO EN LETRA
   Dim NumLetra As String = Numalet.ToCardinal(vTotDoc.ToString)

   Using SqlConnectionDet As New Data.SqlClient.SqlConnection(StrTpm)
    'Consulta para traer la OV
    Dim ConsutaListaDet As String
    ConsutaListaDet = "SELECT *"
    ConsutaListaDet &= " FROM RdVta1"
    ConsutaListaDet &= " WHERE Serie = '" & Serie & "' AND IdOrdVta = " & Folio

    Dim daOVD As New SqlClient.SqlDataAdapter(ConsutaListaDet, SqlConnectionDet)
    daOVD.Fill(dsOVD, "ODetalle")
    daOVD.Fill(detTable)

    Dim contador As Integer = 0

    For Each row As DataRow In detTable.Rows
     contador += 1

     _filaTemp = DtOVta.NewRow()
     _filaTemp(columnas(0)) = numOV.ToString
     _filaTemp(columnas(1)) = "-  " & vSerie & numOV.ToString
     _filaTemp(columnas(2)) = fechaOV
     _filaTemp(columnas(3)) = UsrTPM
     _filaTemp(columnas(4)) = idAgte
     _filaTemp(columnas(5)) = Agente
     _filaTemp(columnas(6)) = idCte
     _filaTemp(columnas(7)) = Cliente
     _filaTemp(columnas(8)) = idTransporte
     _filaTemp(columnas(9)) = formaEnvio
     _filaTemp(columnas(10)) = Contacto
     _filaTemp(columnas(11)) = Comentario
     _filaTemp(columnas(12)) = Direccion
     _filaTemp(columnas(13)) = Colonia
     _filaTemp(columnas(14)) = CP
     _filaTemp(columnas(15)) = Ciudad & ", " & Estado & ", " & Pais & ", " & CP
     _filaTemp(columnas(16)) = Estado
     _filaTemp(columnas(17)) = Pais
     _filaTemp(columnas(18)) = Rfc
     _filaTemp(columnas(19)) = contador
     _filaTemp(columnas(20)) = row("Articulo")    'ARTICULO
     _filaTemp(columnas(21)) = row("Linea")    'LINEA
     _filaTemp(columnas(22)) = row("DesArt")    'DESCRPCION ARTICULO
     _filaTemp(columnas(23)) = row("ListaP")   'Lista de precios capturada por el agente
     _filaTemp(columnas(24)) = row("Precio")    'PRECIO
     _filaTemp(columnas(25)) = row("Cantidad")    'CANTIDAD DE PIEZAS SOLICITADAS
     _filaTemp(columnas(26)) = row("DescLin")    'DESCUENTO
     _filaTemp(columnas(27)) = row("Totlinea")    'IMPORTE
     _filaTemp(columnas(28)) = vTotSIva.ToString
     _filaTemp(columnas(29)) = vTotIva.ToString
     _filaTemp(columnas(30)) = vTotDoc.ToString

     'SE AGREGAN CAMPOS AL REPORTE CON EL DATASET: URIEL TORALVA 19/05/2018
     '_filaTemp(columnas(31)) = txttipo_cambio.Text.ToString     'TIPO DEL CAMBIO AL DIA
     _filaTemp(columnas(32)) = "MXP"               'MONEDA USD O MXP
     _filaTemp(columnas(33)) = NumLetra               'COLOCA EL IMPORTE EN LETRA

     DtOVta.Rows.Add(_filaTemp)

    Next
   End Using


   Dim informe As New rpteOV

   'RepComsultaP.MdiParent = frmOrdenesVta
   informe.SetDataSource(DtOVta)
   RepComsultaP.CrVConsulta.ReportSource = informe

   RepComsultaP.Show()

   'una vez impresa grabo la fecha y hora de su revision y cierro panel de orden de venta
   If (ActualizaInf(Serie, Folio)) Then
    dgvNuevaOV.DataSource = Nothing
    BuscarNuevasOV()
    Renglonactual = 0
   End If

  Catch
   ErrOV = 1
   MessageBox.Show("No fue posible mostrar la orden de venta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
  End Try
 End Sub

 Private Function ActualizaInf(Serie As String, Folio As Integer) As Boolean
  Try
   Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
   SqlConnection.Open()
   Dim command As New Data.SqlClient.SqlCommand
   command.Connection = SqlConnection

   Dim strcadena As String = "UPDATE OrdVta SET FechaVistaTelemarketing = GETDATE() WHERE Serie = '" & Serie & "' AND IdOrdVta = " & Folio & " AND FechaVistaTelemarketing IS NULL"
   command.CommandText = strcadena
   command.ExecuteNonQuery()

   Return True
   'MessageBox.Show("Datos Guardados Correctamente",
   '                     "Aviso.", MessageBoxButtons.OK,
   '                     MessageBoxIcon.Information)
  Catch ex As Exception
   Return False
   'MessageBox.Show("Ocurrio un Error: " & ex.Message,
   '                     "ERROR.", MessageBoxButtons.OK,
   '                     MessageBoxIcon.Error)
  End Try
 End Function

 Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

 End Sub

 Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

 End Sub

 Private Sub txtSerie_TextChanged(sender As Object, e As EventArgs) Handles txtSerie.TextChanged

 End Sub

 Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

 End Sub

 Private Sub txtFolio_TextChanged(sender As Object, e As EventArgs) Handles txtFolio.TextChanged

 End Sub

 Private Sub panelOV_Paint(sender As Object, e As PaintEventArgs) Handles panelOV.Paint

 End Sub
End Class