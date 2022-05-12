Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel
Imports TPD_C.GPSImport
Imports GMap.NET
Imports GMap.NET.MapProviders
Imports GMap.NET.WindowsForms
Imports GMap.NET.WindowsForms.Markers
Imports System.Net.Mail
Imports System.Text
Imports System.Net
Imports System.Net.Mime

Public Class Consultar_visitasporasesor

 Private Enum TipoUbicacion
  Casa = 1
  Sucursal = 2
  Cliente = 3
  Prospecto = 4
  Desconocido = 5
 End Enum

 Private ConRegistros As Boolean

 'Creo inf para Google markers
 Private marker As GMarkerGoogle
 Private markerOverlay As GMapOverlay
 Private Latitud, Longitud As Double
 Private Anterior As String
 Private PosActual As Integer = 0

 'Informacion para envio de correo
 Private subject As String = "Visitas realizadas el día " & DateTime.Now.ToString("dd-MM-yyyy")
 Private Body As String = ""

 Private Sub Consultar_visitasporasesor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
  gMapVisitas.Visible = False

  'Inicio Mapa
  gMapVisitas.MapProvider = GMapProviders.GoogleMap
  rdbtnNormal.Checked = True

  gMapVisitas.MinZoom = 0
  gMapVisitas.MaxZoom = 20
  gMapVisitas.Zoom = 10
  gMapVisitas.AutoScroll = True

  markerOverlay = New GMapOverlay("Unico")
  'marker.ToolTipMode = MarkerTooltipMode.Always

  Using SqlConnection As New SqlConnection(StrTpm)
   Dim DSetTablas As New DataSet

   Dim ConsutaLista As String = "SELECT Agente,IdGPS FROM GPS_Agente ORDER BY Agente ASC"
   Dim daAgentes As New SqlDataAdapter(ConsutaLista, SqlConnection)

   daAgentes.Fill(DSetTablas, "Agentes")

   Me.cmbAgente.DataSource = DSetTablas.Tables("Agentes")
   Me.cmbAgente.DisplayMember = "Agente"
   Me.cmbAgente.ValueMember = "IdGPS"
   Me.cmbAgente.SelectedIndex = 0
  End Using

  panel_maps.Visible = False
  EnviarMail.Visible = False
  LimpiarInf()
 End Sub

 Private Sub InicioMapa(Latitud As Double, Longitud As Double)
  gMapVisitas.Position = New PointLatLng(Latitud, Longitud)
 End Sub

 Private Sub AgregarMarcador(NombreMark As String, ToolTipMark As String, Latitud As Double, Longitud As Double, tipoUbicacion As TipoUbicacion)
  'Marcadores
  'markerOverlay = New GMapOverlay(NombreMark)

  'If Anterior = "" Or Anterior <> ToolTipMark Then
  '  Anterior = ToolTipMark
  Dim Color As GMarkerGoogleType
  Select Case tipoUbicacion
   Case TipoUbicacion.Casa, TipoUbicacion.Sucursal : Color = GMarkerGoogleType.blue
   Case TipoUbicacion.Cliente : Color = GMarkerGoogleType.green
   Case TipoUbicacion.Prospecto : Color = GMarkerGoogleType.white_small
   Case TipoUbicacion.Desconocido : Color = GMarkerGoogleType.red
  End Select

  marker = New GMarkerGoogle(New PointLatLng(Latitud, Longitud), Color)
  markerOverlay.Markers.Add(marker)
  'ToolTip
  marker.ToolTipText = ToolTipMark
  marker.ToolTipMode = MarkerTooltipMode.Always
  ''Agregar el Overlay en el mapa
  'gMapVisitas.Overlays.Add(markerOverlay)
  'End If
 End Sub

 Private Sub LimpiarInf()
  Panel2.Height = 400
  tb_infvisitas.Maximum = 700
  tb_infvisitas.Minimum = 100
  tb_infvisitas.Value = 400

  ConRegistros = False

  lblInicio.Text = "00:00:00"
  lblfin.Text = "00:00:00"
  lblPormedioVisita.Text = "00:00:00"
  lblnv.Text = "0"
  PBExportacion.Minimum = 0
  PBExportacion.Maximum = 0
  PBExportacion.Value = 0
  dgvVisitas.DataSource = Nothing
  Button2.Enabled = False
  PBExportacion.Visible = False
  PBStatus.Visible = False
  gMapVisitas.Visible = False
  panel_maps.Visible = False
  EnviarMail.Visible = False
  chkbox_MostrarSoloSeleccioado.Checked = False
  chkBox_Mostrar_coordenadas.Checked = False
  dtgv_detalles.DataSource = Nothing
  dtgv_detalles.Rows.Clear()
  Panel6.Visible = False
  markerOverlay.Clear()
  gMapVisitas.Overlays.Clear()
 End Sub

 Private Sub Llenar_DataGridView(Optional UltimaPosicion As Boolean = False)
  'Los argumentos de conexión a la base de datos 
  Dim args As String = conexion_universal.CadenaSQL

  'Abro la conexión
  Using connection As SqlConnection = New SqlConnection(args)

   Dim cmd2 As SqlCommand = Nothing
   Dim promedio As String

   Dim cmd3 As SqlCommand = Nothing
   Dim promedio2 As String

   'Dim cmd5 As SqlCommand = Nothing
   'Dim promedio3 As String

   'Dim cmd4 As SqlCommand = Nothing
   'Dim numero As Integer

   Dim command As SqlCommand
   Dim adapter As SqlDataAdapter
   Dim dtTable As DataTable

   Dim fechaActual As Date = Date.Now

   Try

    connection.Open()

    If UltimaPosicion = False Then
     cmd3 = New SqlCommand("TPD_InicioRuta", connection)
     cmd3.CommandType = CommandType.StoredProcedure
     cmd3.Parameters.Add("@Fechainicio", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(dtpInicio.Value)
     'cmd3.Parameters.Add("@FechaFin", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(dtpFin.Value)
     cmd3.Parameters.Add("@agente", SqlDbType.Text).Value = cmbAgente.Text
     promedio2 = (cmd3.ExecuteScalar())
     lblInicio.Text = promedio2.ToString

     cmd2 = New SqlCommand("SP_Promedio_Visita", connection)
     cmd2.CommandType = CommandType.StoredProcedure
     cmd2.Parameters.Add("@Fechainicio", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(dtpInicio.Value)
     'cmd2.Parameters.Add("@FechaFin", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(dtpFin.Value)
     cmd2.Parameters.Add("@agente", SqlDbType.Text).Value = cmbAgente.Text
     promedio = (cmd2.ExecuteScalar())
     lblPormedioVisita.Text = promedio.ToString
    Else
     lblInicio.Text = "00:00:00"
     lblPormedioVisita.Text = "00:00:00"
     dtpInicio.Value = DateAdd(DateInterval.Day, -1, dtpInicio.Value)
    End If

    command = New SqlCommand("SP_ConsultaVisitasRealizadas2", connection)
    command.CommandType = CommandType.StoredProcedure
    adapter = New SqlDataAdapter(command)
    dtTable = New DataTable

    With command.Parameters
     'Envió los parámetros que necesito
     .Add(New SqlParameter("@Fechainicio", SqlDbType.Date)).Value = Convert.ToDateTime(dtpInicio.Value)
     '.Add(New SqlParameter("@FechaFin", SqlDbType.Date)).Value = Convert.ToDateTime(dtpFin.Value)
     .Add(New SqlParameter("@agente", SqlDbType.Text)).Value = cmbAgente.Text
     If UltimaPosicion = True Then
      .Add(New SqlParameter("@UltimaVisita", SqlDbType.Int)).Value = 1
     End If
    End With

    Try
     adapter.Fill(dtTable)
     dgvVisitas.DataSource = dtTable

     If dtTable.Rows.Count > 0 Then
      panel_maps.Visible = True
      EnviarMail.Visible = True
      'hago el llenado del mapa
      Dim inicio As Boolean = True
      Dim toolT As String = ""
      For Each row As DataRow In dtTable.Rows
       If IsDBNull(row.Item("latitusp")) = False Then
        If inicio Then
         InicioMapa(row.Item("latitusp"), row.Item("longitudsp"))
         inicio = False
        End If

        Dim TU As TipoUbicacion

        If IsDBNull(row.Item("Tipo Visita")) = False Then
         Select Case row.Item("Tipo Visita")
          Case 1, 2, 9 : TU = TipoUbicacion.Casa
           toolT = "Casa/Sucursal"
          Case 3, 4, 7 : TU = TipoUbicacion.Cliente
           toolT = row.Item("No. Cliente")
          Case 5, 6, 8 : TU = TipoUbicacion.Desconocido
           'toolT = "Desconocido"
           toolT = ""
          Case 10 : TU = TipoUbicacion.Sucursal
           toolT = "Sucursal"
          Case Else
           TU = TipoUbicacion.Desconocido
           toolT = ""
         End Select
        Else
         TU = TipoUbicacion.Desconocido
         toolT = ""
        End If

        AgregarMarcador(row.Item("Id Viaje"), toolT, row.Item("latitusp"), row.Item("longitudsp"), TU)
       End If
      Next
      'Agregar el Overlay en el mapa
      gMapVisitas.Overlays.Add(markerOverlay)

      gMapVisitas.Visible = True
     End If
    Catch expSQL As SqlException
     MsgBox(expSQL.ToString, MsgBoxStyle.OkOnly, "SQL Exception")
     LimpiarInf()
     Exit Sub
    End Try

   Catch ex As Exception
    MsgBox("Información dispobible a partir del día anterior o el agente no tiene visitas en el rango seleccionado", MsgBoxStyle.OkOnly)
    LimpiarInf()
   End Try
   DisenoGrid()
  End Using

 End Sub


 Private Sub DisenoGrid()
  '-------Diseño de DATAGRID Totales
  With Me.dgvVisitas

   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

   .RowHeadersVisible = True
   .RowHeadersWidth = 25


   dgvVisitas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   Try

    .Columns(0).HeaderText = "Id Viaje"
    .Columns(0).Width = 100
    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    .Columns(0).ReadOnly = True
    .Columns(0).Visible = False

    .Columns(1).HeaderText = "Fecha Salida"
    .Columns(1).Width = 40
    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    .Columns(1).ReadOnly = True

    .Columns(2).HeaderText = "No. Cliente"
    .Columns(2).Width = 120
    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    .Columns(2).ReadOnly = True

    .Columns(3).HeaderText = "Cliente"
    .Columns(3).Width = 80
    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    .Columns(3).ReadOnly = True

    .Columns(4).HeaderText = "Ciudad"
    .Columns(4).Width = 50
    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    .Columns(4).ReadOnly = True


    .Columns(5).HeaderText = "Estado"
    .Columns(5).Width = 50
    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(5).ReadOnly = True


    .Columns(6).HeaderText = "Hora inicio"
    .Columns(6).Width = 80
    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(6).ReadOnly = True

    .Columns(7).HeaderText = "Hora fin"
    .Columns(7).Width = 80
    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(7).ReadOnly = True

    .Columns(8).HeaderText = "Duración"
    .Columns(8).Width = 80
    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(8).ReadOnly = True
    .Columns(8).Visible = True

    .Columns(9).HeaderText = "Duracion3"
    .Columns(9).Visible = False

    .Columns(10).HeaderText = "Tipo Visita"

    .Columns(10).Visible = False

    .Columns(11).HeaderText = "latitusp"
    .Columns(11).Visible = False

    .Columns(12).HeaderText = "longitudsp"
    .Columns(12).Visible = False
   Catch ex As Exception
   End Try
  End With
 End Sub

 Sub Exportar()

  Dim m_Excel As New Microsoft.Office.Interop.Excel.Application
  m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlWait
  'm_Excel.Visible = True
  Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook = m_Excel.Workbooks.Add
  Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet = objLibroExcel.Worksheets(1)
  With objHojaExcel
   '.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
   'Encabezado
   'ENCABEZADO DEL REPORTE GENERADO
   .Range("A1:D1").Merge()
   .Range("A1:D1").Value = "Reporte de Visitas realizadas por asesor"
   .Range("A1:D1").Font.Bold = True

   .Range("A2:D2").Merge()
   .Range("A2:D2").Value = "Fecha del: " + dtpInicio.Value + "  Al  " + dtpFin.Value
   .Range("A2:D2").Font.Bold = True

   .Range("A2:D3").Merge()
   .Range("A3:D3").Value = "Asesor de ventas: " + cmbAgente.Text
   .Range("A3:D3").Font.Bold = True

   .Range("A4:D4").Merge()
   .Range("A4:D4").Value = "Hora promedio de inicio de ruta: " + lblInicio.Text
   .Range("A4:D4").Font.Bold = True

   .Range("A5:D5").Merge()
   .Range("A5:D5").Value = "Hora promedio de fin de ruta: " + lblfin.Text
   .Range("A5:D5").Font.Bold = True

   .Range("A6:D6").Merge()
   .Range("A6:D6").Value = "Tiempo promedio de visita: " + lblPormedioVisita.Text
   .Range("A6:D6").Font.Bold = True

   .Range("A7:D7").Merge()
   .Range("A7:D7").Value = "Número de visitas: " + lblnv.Text
   .Range("A7:D7").Font.Bold = True

  End With


  Try
   Dim dt As New DataTable()
   For Each columns As DataGridViewColumn In dgvVisitas.Columns
    dt.Columns.Add(columns.HeaderText, columns.ValueType)
   Next
   For Each row As DataGridViewRow In dgvVisitas.Rows
    dt.Rows.Add()

    For Each cell As DataGridViewCell In row.Cells
     If Not Convert.IsDBNull(cell.Value) = 0 Then
      dt.Rows(dt.Rows.Count - 1)(cell.ColumnIndex) = IsDBNull("")
     Else
      If cell.Value = Nothing Then
       dt.Rows(dt.Rows.Count - 1)(cell.ColumnIndex) = IsDBNull("")
      Else
       dt.Rows(dt.Rows.Count - 1)(cell.ColumnIndex) = cell.Value.ToString()
      End If
     End If
    Next
   Next

   Dim saveFileDialog1 As New SaveFileDialog()
   saveFileDialog1.Filter = "Excel|*.xlsx"
   saveFileDialog1.Title = "Save Excel File"
   saveFileDialog1.FileName = "Export_" & dgvVisitas.Name.ToString() & ".xlsx"
   saveFileDialog1.ShowDialog()
   saveFileDialog1.InitialDirectory = "C:/"

   If saveFileDialog1.FileName <> "" Then
    Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
    fs.Close()
   End If

   Dim strFileName As String = saveFileDialog1.FileName
   Dim blnFileOpen As Boolean = False

   Using wb As New XLWorkbook
    wb.Worksheets.Add(dt, "Hoja 1")
    wb.SaveAs(strFileName)
   End Using

   Process.Start(strFileName)
  Catch ex As Exception
   MessageBox.Show("¡Error al exportar archivo: " + Environment.NewLine + ex.ToString() + "!", "¡Error en ExportarSinEstilo!", MessageBoxButtons.OK, MessageBoxIcon.Error)
  End Try


 End Sub

 Private Sub dgvVisitas_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvVisitas.RowPrePaint
  If IsDBNull(dgvVisitas.Rows(e.RowIndex).Cells("Duracion3").Value) Then dgvVisitas.Rows(e.RowIndex).Cells("Duracion3").Value = 0
  If IsDBNull(dgvVisitas.Rows(e.RowIndex).Cells("Tipo Visita").Value) Then dgvVisitas.Rows(e.RowIndex).Cells("Tipo Visita").Value = 0


  If dgvVisitas.Rows(e.RowIndex).Cells("Duracion3").Value < 599 And (dgvVisitas.Rows(e.RowIndex).Cells("Tipo Visita").Value = 3 Or dgvVisitas.Rows(e.RowIndex).Cells("Tipo Visita").Value = 4) Then

   dgvVisitas.Rows(e.RowIndex).Cells("Fecha salida").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("No. Cliente").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Cliente").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Ciudad").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Estado").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Hora inicio").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Hora fin").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Duración").Style.BackColor = Color.Red

  ElseIf dgvVisitas.Rows(e.RowIndex).Cells("Duracion3").Value >= 600 And dgvVisitas.Rows(e.RowIndex).Cells("Duracion3").Value < 900 And (dgvVisitas.Rows(e.RowIndex).Cells("Tipo Visita").Value = 3 Or dgvVisitas.Rows(e.RowIndex).Cells("Tipo Visita").Value = 4) Then
   dgvVisitas.Rows(e.RowIndex).Cells("Fecha salida").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("No. Cliente").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Cliente").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Ciudad").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Estado").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Hora inicio").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Hora fin").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Duración").Style.BackColor = Color.Yellow

  ElseIf dgvVisitas.Rows(e.RowIndex).Cells("Duracion3").Value >= 900 And (dgvVisitas.Rows(e.RowIndex).Cells("Tipo Visita").Value = 3 Or dgvVisitas.Rows(e.RowIndex).Cells("Tipo Visita").Value = 4) Then
   dgvVisitas.Rows(e.RowIndex).Cells("Fecha salida").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("No. Cliente").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Cliente").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Ciudad").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Estado").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Hora inicio").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Hora fin").Style.BackColor = Color.LightGray
   dgvVisitas.Rows(e.RowIndex).Cells("Duración").Style.BackColor = Color.LightGreen
  End If
 End Sub

 Sub ExportarNuevo()

  Dim dt As DataTable = dgvVisitas.DataSource

  Dim wb = New XLWorkbook()
  Dim ws = wb.Worksheets.Add("Visitas realizadas")


  Dim range = ws.Range(8, 1, dt.Rows.Count + 8, dt.Columns.Count).Merge().AddToNamed("Totales")

  Dim rangeWithData = ws.Cell(9, 1).InsertData(dt.AsEnumerable)

  Dim tab = range.CreateTable()
  tab.Theme = XLTableTheme.TableStyleLight8

  'DAR FOMATO A LAS CELDAS
  Dim index As Integer = 8

  For i As Integer = 0 To dt.Rows.Count

   Try
    'Encabezados dependiendo
    If index = 8 Then
     Dim cellA3 As String = String.Format("A{0}", 1)
     wb.Worksheet(1).Cells(cellA3).Value = "Reporte de visitas realizadas el día "
     wb.Worksheet(1).Cells(cellA3).Style.Font.Bold = True
     wb.Worksheet(1).Cells(cellA3).Style.Font.FontSize = 11

     Dim cellB3 As String = String.Format("B{0}", 1)
     wb.Worksheet(1).Cells(cellB3).Value = Format(Me.dtpInicio.Value, "dd-MM-yyyy")


     Dim cellA4 As String = String.Format("A{0}", 2)
     wb.Worksheet(1).Cells(cellA4).Value = "Asesor de ventas: "
     wb.Worksheet(1).Cells(cellA4).Style.Font.Bold = True
     wb.Worksheet(1).Cells(cellA4).Style.Font.FontSize = 11

     Dim cellB4 As String = String.Format("B{0}", 2)
     wb.Worksheet(1).Cells(cellB4).Value = cmbAgente.Text.ToString()


     Dim cellA5 As String = String.Format("A{0}", 3)
     wb.Worksheet(1).Cells(cellA5).Value = "Hora promedio de inicio de ruta: "
     wb.Worksheet(1).Cells(cellA5).Style.Font.Bold = True
     wb.Worksheet(1).Cells(cellA5).Style.Font.FontSize = 11

     Dim cellB5 As String = String.Format("B{0}", 3)
     wb.Worksheet(1).Cells(cellB5).Value = lblInicio.Text.ToString()



     Dim cellA6 As String = String.Format("A{0}", 4)
     wb.Worksheet(1).Cells(cellA6).Value = "Hora promedio de fin de ruta: "
     wb.Worksheet(1).Cells(cellA6).Style.Font.Bold = True
     wb.Worksheet(1).Cells(cellA6).Style.Font.FontSize = 11

     Dim cellB6 As String = String.Format("B{0}", 4)
     wb.Worksheet(1).Cells(cellB6).Value = lblfin.Text.ToString()



     Dim cellA7 As String = String.Format("A{0}", 5)
     wb.Worksheet(1).Cells(cellA7).Value = "Tiempo promedio de visita: "
     wb.Worksheet(1).Cells(cellA7).Style.Font.Bold = True
     wb.Worksheet(1).Cells(cellA7).Style.Font.FontSize = 11

     Dim cellB7 As String = String.Format("B{0}", 5)
     wb.Worksheet(1).Cells(cellB7).Value = lblPormedioVisita.Text.ToString()

     Dim cellA8 As String = String.Format("A{0}", 6)
     wb.Worksheet(1).Cells(cellA8).Value = "Número de visitas: "
     wb.Worksheet(1).Cells(cellA8).Style.Font.Bold = True
     wb.Worksheet(1).Cells(cellA8).Style.Font.FontSize = 11

     Dim cellB8 As String = String.Format("B{0}", 6)
     wb.Worksheet(1).Cells(cellB8).Value = lblnv.Text


     Dim cellA0 As String = String.Format("A{0}", index)
     wb.Worksheet(1).Cells(cellA0).Value = "Id Viaje"

     Dim cellB0 As String = String.Format("B{0}", index)
     wb.Worksheet(1).Cells(cellB0).Value = "Fecha Salida"

     Dim cellC0 As String = String.Format("C{0}", index)
     wb.Worksheet(1).Cells(cellC0).Value = "No. Cliente"

     Dim cellD0 As String = String.Format("D{0}", index)
     wb.Worksheet(1).Cells(cellD0).Value = "Cliente"

     Dim cellE0 As String = String.Format("E{0}", index)
     wb.Worksheet(1).Cells(cellE0).Value = "Ciudad"

     Dim cellF0 As String = String.Format("F{0}", index)
     wb.Worksheet(1).Cells(cellF0).Value = "Estado"

     Dim cellG0 As String = String.Format("G{0}", index)
     wb.Worksheet(1).Cells(cellG0).Value = "Hora inicio"

     Dim cellH0 As String = String.Format("H{0}", index)
     wb.Worksheet(1).Cells(cellH0).Value = "Hora fin"

     Dim cellI0 As String = String.Format("I{0}", index)
     wb.Worksheet(1).Cells(cellI0).Value = "Duración"
    End If
   Catch ex As Exception
    MessageBox.Show(ex.ToString(), "Error al dar formato a celdas (Visitas): ")
   End Try

   index = index + 1

   ws.Columns("J").Delete()
   ws.Columns("K").Delete()
   ws.Columns("L").Delete()
   ws.Columns("M").Delete()
  Next

  ws.Columns("A").Width = 20
  ws.Columns("B").Width = 15
  ws.Columns("C").Width = 70
  ws.Columns("D").Width = 45
  ws.Columns("E").Width = 25
  ws.Columns("F").Width = 15
  ws.Columns("G").Width = 25
  ws.Columns("H").Width = 15
  ws.Columns("I").Width = 15

  ws.Rows(3).Style.Alignment.WrapText = False

  Try
   Dim saveFileDialog1 As New SaveFileDialog()
   saveFileDialog1.Filter = "Excel|*.xlsx"
   saveFileDialog1.Title = "Save Excel File"
   saveFileDialog1.FileName = "Registro de visitas.xlsx"
   saveFileDialog1.ShowDialog()
   saveFileDialog1.InitialDirectory = "C:/
 "

   If saveFileDialog1.FileName <> "" Then
    Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
    fs.Close()
   End If

   Dim strFileName As String = saveFileDialog1.FileName
   wb.SaveAs(strFileName)
   Process.Start(saveFileDialog1.FileName)
  Catch ex As Exception
   MessageBox.Show(ex.ToString(), "Error al guardar el archivo (Visitas): ")
  End Try
 End Sub

 Private Sub Agrupar(dgvVisitas As Object)

  If dgvVisitas.Rows.Count > 0 Then
   Dim Filas As Integer = dgvVisitas.Rows.Count - 1
   Dim aux As String

   For i As Integer = 0 To Filas

    aux = dgvVisitas.Rows.Item(i).Cells(2).Value
    'For j As Integer = 0 To Filas
    If IsDBNull(dgvVisitas.Rows.Item(i).Cells(9).Value) Then dgvVisitas.Rows.Item(i).Cells(9).Value = 0
    Dim aux2 As Integer

    If dgvVisitas.Rows.Item(i).Cells(2).Value = aux Then
     aux2 = dgvVisitas.Rows.Item(i).Cells(9).Value
     aux2 = aux2 + dgvVisitas.Rows.Item(i).Cells(9).Value
     aux = dgvVisitas.Rows.Count - 1
    End If

    'Next
   Next
  End If
 End Sub


 Private Sub Agrupar2(dgvVisitas As Object)

  Dim i As Integer = 1
  Dim contar As Integer = dgvVisitas.Rows.Count


  Do While (i <= dgvVisitas.Rows.Count - 1)
   Dim aux As String
   Dim aux3 As String

   Dim aux2 As Integer

   Dim horas As Integer
   Dim minutos As Integer
   Dim segundos As Integer
   Dim resultado As String
   Dim horasAux As String
   Dim minutosAux As String
   Dim segundosAux As String


   aux = Trim(dgvVisitas.Rows.Item(i - 1).Cells(2).Value)
   aux3 = Trim(dgvVisitas.Rows.Item(i).Cells(2).Value)
   If aux3 = aux Then
    If IsDBNull(dgvVisitas.Rows.Item(i - 1).Cells(9).Value) Then dgvVisitas.Rows.Item(i - 1).Cells(9).Value = 0

    aux2 = dgvVisitas.Rows.Item(i - 1).Cells(9).Value
    aux2 = aux2 + dgvVisitas.Rows.Item(i).Cells(9).Value
    dgvVisitas.Rows.Item(i).Cells(2).Value = aux


    dgvVisitas.Rows.Item(i - 1).Cells(7).Value = dgvVisitas.Rows.Item(i).Cells(7).Value

    dgvVisitas.CurrentRow.Index
    dgvVisitas.Rows.Item(i).Visible = False

    If dgvVisitas.Rows.Item(i).Visible = False Then
     contar = contar - 1
    End If

    If aux2 < 3600 Then
     horas = 0
    Else
     horas = (aux2 / 3600)
    End If


    minutos = ((aux2 - horas * 3600) / 60)
    segundos = aux2 - (horas * 3600 + minutos * 60)


    If horas < 10 Then
     horasAux = "0" + horas.ToString()
    Else
     horasAux = horas.ToString()
    End If

    If minutos < 0 Then
     minutos = minutos * -1
     If minutos < 10 Then
      minutosAux = "0" + minutos.ToString()
     Else
      minutosAux = minutos.ToString()
     End If
    Else
     If minutos < 10 Then
      minutosAux = "0" + minutos.ToString()
     Else
      minutosAux = minutos.ToString()
     End If
    End If

    If segundos < 0 Then
     segundos = segundos * -1
     If segundos < 10 Then
      segundosAux = "0" + segundos.ToString()
     Else
      segundosAux = segundos.ToString()
     End If
    Else
     If segundos < 10 Then
      segundosAux = "0" + segundos.ToString()
     Else
      segundosAux = segundos.ToString()
     End If
    End If

    resultado = horasAux + ":" + minutosAux + ":" + segundosAux



    dgvVisitas.Rows.Item(i - 1).Cells(8).Value = resultado
    dgvVisitas.Rows.Item(i - 1).Cells(9).Value = aux2
    i = i + 1

   Else
    If IsDBNull(dgvVisitas.Rows.Item(i - 1).Cells(9).Value) Then


     aux2 = dgvVisitas.Rows.Item(i).Cells(9).Value = 0
     i = i + 1

    Else
     aux2 = dgvVisitas.Rows.Item(i - 1).Cells(9).Value
     i = i + 1

    End If
   End If
  Loop

  lblnv.Text = contar
  If contar > 0 Then
   lblfin.Text = dgvVisitas.Rows.Item(i - 1).Cells(7).Value
  End If
 End Sub

 Private Sub dgvVisitas_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvVisitas.RowEnter
  Dim currentRow As DataGridViewRow = dgvVisitas.Rows(e.RowIndex)
  Dim i As Integer
  PosActual = e.RowIndex
  If gMapVisitas.Visible = True Then
   If IsDBNull(markerOverlay) = False And IsNothing(markerOverlay) = False Then
    lblRow.Text = PosActual.ToString()
    lblLat.Text = ""
    lblLng.Text = ""
    If chkbox_MostrarSoloSeleccioado.Checked = True Then
     For i = 0 To markerOverlay.Markers.Count - 1
      If i <> e.RowIndex Then
       markerOverlay.Markers(i).IsVisible = False
      Else
       markerOverlay.Markers(i).IsVisible = True
      End If
     Next
    End If
    If IsDBNull(dgvVisitas.Rows.Item(e.RowIndex).Cells(11).Value) = False Then
     lblLat.Text = dgvVisitas.Rows.Item(e.RowIndex).Cells(11).Value
     lblLng.Text = dgvVisitas.Rows.Item(e.RowIndex).Cells(12).Value
     InicioMapa(dgvVisitas.Rows.Item(e.RowIndex).Cells(11).Value, dgvVisitas.Rows.Item(e.RowIndex).Cells(12).Value)
     ObtieneDetalles(dtpInicio.Value, cmbAgente.Text, dgvVisitas.Rows.Item(e.RowIndex).Cells(2).Value, dgvVisitas.Rows.Item(e.RowIndex).Cells(11).Value.ToString().Substring(0, 5), dgvVisitas.Rows.Item(e.RowIndex).Cells(12).Value.ToString().Substring(0, 6)
                          )
    End If
   End If
  End If
  '  gMapVisitas.Refresh()
 End Sub

 Private Sub ObtieneDetalles(fechaReporte As Date, Conductor As String, Lugar As String, vLat As String, vLng As String)
  'Los argumentos de conexión a la base de datos 
  Dim args As String = conexion_universal.CadenaSQL

  'Abro la conexión
  Dim dSet As New DataSet
  Dim Adaptador = New SqlDataAdapter
  Dim dtView As New DataView

  Dim conec = New SqlConnection(StrTpm)
  Dim cmd = New SqlCommand("[SP_GPS_DetallesLugarV2]", conec)
  cmd.CommandType = CommandType.StoredProcedure
  cmd.Parameters.Add("@Conductor", SqlDbType.VarChar).Value = Conductor
  cmd.Parameters.Add("@Fecha", SqlDbType.SmallDateTime).Value = fechaReporte
  'cmd.Parameters.Add("@Lugar", SqlDbType.Text).Value = Lugar
  cmd.Parameters.Add("@Lat", SqlDbType.Text).Value = vLat
  cmd.Parameters.Add("@Lng", SqlDbType.Text).Value = vLng

  Try
   conec.Open()
   Adaptador.SelectCommand = cmd
   Adaptador.SelectCommand.Connection = conec
   Adaptador.SelectCommand.CommandTimeout = 10000
   cmd.ExecuteNonQuery()
   cmd.Connection.Close()
   conec.Close()

   Adaptador.Fill(dSet)
   dSet.Tables(0).TableName = "Res"
   dtView.Table = dSet.Tables("Res")
   dtgv_detalles.DataSource = dtView
   EstiloDetalle()
  Catch ex As Exception
   MessageBox.Show("Error")
  End Try
 End Sub

 Private Sub chkBox_Mostrar_coordenadas_CheckedChanged_1(sender As Object, e As EventArgs) Handles chkBox_Mostrar_coordenadas.CheckedChanged
  If chkBox_Mostrar_coordenadas.Checked = True Then
   Panel6.Visible = True
  Else
   Panel6.Visible = False
  End If
 End Sub

 Private Sub chkbox_MostrarSoloSeleccioado_CheckedChanged_1(sender As Object, e As EventArgs) Handles chkbox_MostrarSoloSeleccioado.CheckedChanged
  Dim i As Integer
  If chkbox_MostrarSoloSeleccioado.Checked = True Then
   For i = 0 To markerOverlay.Markers.Count - 1
    If i <> PosActual Then
     markerOverlay.Markers(i).IsVisible = False
    Else
     markerOverlay.Markers(i).IsVisible = True
    End If
   Next
  Else
   For i = 0 To markerOverlay.Markers.Count - 1
    markerOverlay.Markers(i).IsVisible = True
   Next
  End If
 End Sub

 Sub EstiloDetalle()
  With Me.dtgv_detalles
   .ReadOnly = True
   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .RowHeadersVisible = True
   .RowHeadersWidth = 25
   .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   Try

    .Columns("fechaSalida").HeaderText = "Fecha"
    .Columns("fechaSalida").Width = 75
    .Columns("fechaSalida").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns("TotalVisitas").HeaderText = "Visitas"
    .Columns("TotalVisitas").Width = 40

    '.Columns("Duracion2").HeaderText = "Tiempo en segundo"
    '.Columns("Duracion2").Visible = False

    .Columns("Duracion").HeaderText = "Duración"
    .Columns("Duracion").Width = 75
    .Columns("Duracion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    '.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
   Catch ex As Exception
    'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView") 2LV8U
   End Try
  End With
 End Sub

 Private Sub rdbtnNormal_CheckedChanged_1(sender As Object, e As EventArgs) Handles rdbtnNormal.CheckedChanged
  If rdbtnNormal.Checked = True Then
   gMapVisitas.MapProvider = GMapProviders.GoogleMap
  Else
   gMapVisitas.MapProvider = GMapProviders.GoogleSatelliteMap
  End If
  gMapVisitas.Refresh()
 End Sub

 Private Sub rdbtnSatelite_CheckedChanged_1(sender As Object, e As EventArgs) Handles rdbtnSatelite.CheckedChanged
  If rdbtnSatelite.Checked = True Then
   gMapVisitas.MapProvider = GMapProviders.GoogleSatelliteMap
  Else
   gMapVisitas.MapProvider = GMapProviders.GoogleMap
  End If
  gMapVisitas.Refresh()
 End Sub

 Private Sub gMapVisitas_OnMarkerClick(item As GMapMarker, e As MouseEventArgs) Handles gMapVisitas.OnMarkerClick
  Dim Url As String
  Url = "https://www.google.com/maps/search/?api=1&query=" & item.Position.Lat & "," & item.Position.Lng
  Process.Start(Url)
 End Sub

 Private Sub gMapVisitas_OnMarkerEnter(item As GMapMarker) Handles gMapVisitas.OnMarkerEnter
  'MsgBox("Geocerca de " & item.Position.Lat)
 End Sub

 Private Sub gMapVisitas_MouseWheel(sender As Object, e As MouseEventArgs) Handles gMapVisitas.MouseWheel
  trackZoom.Value = Convert.ToInt32(gMapVisitas.Zoom)
  lblzoom.Text = trackZoom.Value.ToString
 End Sub

 Private Function CapturarPantalla() As Boolean
  ' Capturar todo el área del formulario
  Dim gr As Graphics = Me.CreateGraphics
  If Me.WindowState = FormWindowState.Maximized Then
   ' Tamaño de lo que queremos copiar
   Dim fSize As Size = Me.Size
   ' Creamos el bitmap con el área que vamos a capturar
   ' En este caso, con el tamaño del formulario actual
   Dim bm As New Bitmap(fSize.Width - 10, fSize.Height - 30, gr)
   ' Un objeto Graphics a partir del bitmap
   Dim gr2 As Graphics = Graphics.FromImage(bm)
   ' Copiar el área de la pantalla que ocupa el formulario
   gr2.CopyFromScreen(Me.Location.X + 10, Me.Location.Y + 100, 0, 0, fSize)
   ' Asignamos la imagen al PictureBox
   Me.picCaptura.Image = bm
  Else
   ' Tamaño de lo que queremos copiar
   Dim fSize As Size = Me.Size
   ' Creamos el bitmap con el área que vamos a capturar
   ' En este caso, con el tamaño del formulario actual
   Dim bm As New Bitmap(fSize.Width - 5, fSize.Height - 40, gr)
   ' Un objeto Graphics a partir del bitmap
   Dim gr2 As Graphics = Graphics.FromImage(bm)
   ' Copiar el área de la pantalla que ocupa el formulario
   gr2.CopyFromScreen(Me.Location.X + 5, Me.Location.Y + 80, 0, 0, fSize)
   ' Asignamos la imagen al PictureBox
   Me.picCaptura.Image = bm
  End If

  Try
   picCaptura.Image.Save(Application.StartupPath & "\imgRastreoGPS.jpg")
   Return True
  Catch ex As Exception
   MessageBox.Show(ex.ToString, "Error al crear la captura de pantalla, por favor intentelo de nuevo")
   Return False
  End Try
 End Function

 Private Function GetMailAgente() As String
  If (cmbAgente.Text = "MAURO TOVAR") Then
   Return "asesorbajio@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "RICARDO ROBLES") Then
   Return "ricardorobles@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "CARLOS EBER") Then
   Return "ventastuxtla1@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "CRUZ SÁNCHEZ MÉNDEZ") Then
   Return "asesorveracruz@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "MAURICIO CHABLÉ") Then
   Return "mauriciochable@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "RAFAEL J MENDOZA") Then
   Return "asesortuxtla@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "DEIVID CHALÉ") Then
   Return "deividchale@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "JAIME SÁNCHEZ") Then
   Return "jaimesanchez@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "VICTOR VERGARA") Then
   Return "victorvergara@tractopartesdiamante.com.mx"
  End If
 End Function

 Private Function GetMailAgenteTelemarketing() As String
  If (cmbAgente.Text = "MAURO TOVAR") Then
   Return "israelsantibanez@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "RICARDO ROBLES") Then
   Return ""
  ElseIf (cmbAgente.Text = "CARLOS EBER") Then
   Return "victorvergara@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "CRUZ SÁNCHEZ MÉNDEZ") Then
   Return "ventas1@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "MAURICIO CHABLÉ") Then
   Return ""
  ElseIf (cmbAgente.Text = "RAFAEL J MENDOZA") Then
   Return "victorvergara@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "DEIVID CHALÉ") Then
   Return ""
  ElseIf (cmbAgente.Text = "JAIME SÁNCHEZ") Then
   Return "raymundolira@tractopartesdiamante.com.mx"
  ElseIf (cmbAgente.Text = "VICTOR VERGARA") Then
   Return ""
  End If
 End Function


 Private Function EnvioMail(rutaArchivo As String, Too As String, CC As String, Subject As String, body As String, Optional CopiaOculta As String = "", Optional CopiaAgenteT As String = "") As Boolean
  Try
   Dim correo As New MailMessage
   Dim smtp As New SmtpClient()

   'Too = "desarrollo.ti@tractopartesdiamante.com.mx"

   Dim Cuerpo As String = ""
   'If (ConRegistros = True) Then
   ' Cuerpo = "Estimado " & StrConv(cmbAgente.Text, VbStrConv.ProperCase) & " buen día, en la siguiente imágen encontrarás el reporte de tus visitas del día " & dtpInicio.Value.ToLongDateString
   'Else
   ' Cuerpo = "Estimado " & StrConv(cmbAgente.Text, VbStrConv.ProperCase) & " buen día, se le informa que usted no cuenta con registros de visita para el día " & dtpInicio.Value.ToLongDateString
   'End If
   Cuerpo = "Estimado " & StrConv(cmbAgente.Text, VbStrConv.ProperCase) & " buen día, en la siguiente imágen encontrarás el reporte de tus visitas del día " & dtpInicio.Value.ToLongDateString

   body = "<html><body><h2>" & Cuerpo & "</h2><br><img src=""cid:Pic1""></a></body></html>"
    Dim avHtml As AlternateView = AlternateView.CreateAlternateViewFromString(body, Nothing, MediaTypeNames.Text.Html)
    Dim pic1 As New LinkedResource(rutaArchivo)
    pic1.ContentId = "Pic1"
    avHtml.LinkedResources.Add(pic1)
    correo.AlternateViews.Add(avHtml)

    correo.From = New MailAddress("programador.ti@tractopartesdiamante.com.mx", "Sistema TPD GPS", Encoding.UTF8)
    correo.To.Add(Too)
    If CC <> "" Then
     'Dim CCopia As String[]
     correo.CC.Add(CC)
    End If
    If CopiaOculta <> "" Then
     correo.Bcc.Add(CopiaOculta)
    End If

    If CopiaAgenteT <> "" Then
     correo.Bcc.Add(CopiaAgenteT)
    End If
    correo.SubjectEncoding = Encoding.UTF8
    correo.Subject = Subject

    Dim attFile As New FileStream(rutaArchivo, FileMode.Open, FileAccess.Read)
    Dim att As New Attachment(attFile, "reporte.jpg")
    correo.Attachments.Add(att)

    correo.BodyEncoding = Encoding.UTF8
    correo.IsBodyHtml = True
    correo.Priority = MailPriority.High

    smtp.UseDefaultCredentials = True
    smtp.Credentials = New NetworkCredential("programador.ti@tractopartesdiamante.com.mx", "progTr@to2012")
    smtp.Port = 587
    smtp.Host = "servidor3315.tl.controladordns.com"
    smtp.EnableSsl = True

   smtp.Send(correo)
   MessageBox.Show("Se envió correctamente el correo", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)

   correo.AlternateViews.Dispose()
    pic1.Dispose()
    correo.Attachments.Dispose()
    attFile.Close()

    Try
     Kill(rutaArchivo)
    Catch ex As Exception
     MessageBox.Show(ex.ToString, "Error al enviar el correo")
    End Try

    Return True
    Me.Close()
        Catch ex As Exception
   MessageBox.Show(ex.ToString, "Error al enviar el correo")
   Return False
  End Try
 End Function

 Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
  Dim iGPS = New ImportGPS()
  Dim Registros As Integer

  PBStatus.Visible = True

  If cmbAgente.SelectedValue = "0" Then 'En el caso de que no exista inf en la tabla
   MsgBox("No se localizó información a mostrar", MsgBoxStyle.Information, "Sin información")
  ElseIf cmbAgente.SelectedValue = "99" Then 'En el caso de que se seleccionen TODOS
   For Ciclo = 0 To cmbAgente.Items.Count - 2
    'iGPS.ImportaInfGPS(cmbAgente.Items, cmbAgente.SelectedText, dtpInicio.Value, dtpFin.Value)
   Next
  Else 'En el caso de que se seleccione uno solo
   Registros = iGPS.ImportaInfGPS(cmbAgente.SelectedValue, cmbAgente.Text, dtpInicio.Value, dtpInicio.Value, PBStatus)
  End If

  LimpiarInf()

  If Registros > 0 Then
   ConRegistros = True
   Llenar_DataGridView()
   Button2.Enabled = True
  Else
   ConRegistros = False
   'En el caso de que no tengan movimientos solicito el licenciado que se envie el reporte vacio asi como esta, por lo que se comento lo que solicito anteriormente mas abajo
   Llenar_DataGridView()
   EnviarMail.Visible = True

   'Reviso si el dia a revisar no es superior al actual, si es asi entonces indico que no es posible
   'If dtpInicio.Value > Now Then
   ' MsgBox("No es posible obtener un reporte de una fecha superior al dia de hoy", MsgBoxStyle.Information, "Error en fecha")
   ' LimpiarInf()
   'Else
   ' 'En el caso de que no se hayan reconocido movimientos en el dia el licenciado quiere ver la ultima posicion del vehiculo, por lo tanto hacemos los siguiente
   ' 'Llenar_DataGridView(True)
   ' Llenar_DataGridView()
   ' EnviarMail.Visible = True
   ' 'MsgBox("No se han localizado movimientos recientes, sin embargo, se mostrará la última posición reconocida del vehículo", MsgBoxStyle.Information, "Última posición del vehículo")
   'End If
  End If
  PBStatus.Visible = False
  Agrupar2(dgvVisitas)
 End Sub

 Private Sub EnviarMail_Click_1(sender As Object, e As EventArgs) Handles EnviarMail.Click
  panel_controles.Visible = False
  panel_controles.Refresh()
  tb_infvisitas.Visible = False
  tb_infvisitas.Refresh()
  GroupBox1.Refresh()

  If CapturarPantalla() = False Then
   Exit Sub
  End If

  Dim Continua As Boolean = False
  Do Until Continua = True
   If Dir(Application.StartupPath & "\imgRastreoGPS.jpg", FileAttribute.Archive) <> "" Then 'Verifico que exista
    Continua = True
   End If
  Loop
  'salvadordiaz@tractopartesdiamante.com.mx
  EnvioMail(Application.StartupPath & "\imgRastreoGPS.jpg", GetMailAgente, "salvadordiaz@tractopartesdiamante.com.mx", subject, Body, "programador.ti@tractopartesdiamante.com.mx", GetMailAgenteTelemarketing)

  'Pruebas
  'EnvioMail(Application.StartupPath & "\imgRastreoGPS.jpg", "licjorgeluis@hotmail.com", "desarrollo.ti@tractopartesdiamante.com.mx", subject, Body, "", "")
  panel_controles.Visible = True
  panel_controles.Refresh()
  tb_infvisitas.Visible = True
  tb_infvisitas.Refresh()
  GroupBox1.Refresh()
  LimpiarInf()
 End Sub

 Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
  PBExportacion.Visible = True
  ExportarNuevo()
  PBExportacion.Visible = False
 End Sub

 Private Sub cmbAgente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAgente.SelectedIndexChanged
  LimpiarInf()
 End Sub

 Private Sub dtpInicio_ValueChanged(sender As Object, e As EventArgs) Handles dtpInicio.ValueChanged
  LimpiarInf()
 End Sub

 Private Sub tb_infvisitas_Scroll_1(sender As Object, e As EventArgs) Handles tb_infvisitas.Scroll
  Panel2.Height = tb_infvisitas.Value
 End Sub

 Private Sub trackZoom_ValueChanged(sender As Object, e As EventArgs) Handles trackZoom.ValueChanged
  gMapVisitas.Zoom = trackZoom.Value
  lblzoom.Text = trackZoom.Value.ToString
 End Sub
End Class
