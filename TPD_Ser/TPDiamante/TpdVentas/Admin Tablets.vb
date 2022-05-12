Imports System.Data.SqlClient
Imports GMap.NET
Imports GMap.NET.MapProviders
Imports GMap.NET.WindowsForms
Imports GMap.NET.WindowsForms.Markers

Public Class Admin_Tablets

 'Creo inf para Google markers
 Private marker As GMarkerGoogle
 Private markerOverlay As GMapOverlay
 Private Latitud, Longitud As Double
 Private Anterior As String
 Private PosActual As Integer = 0

 Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
  Me.Close()
 End Sub

 Private Sub Admin_Tablets_Load(sender As Object, e As EventArgs) Handles MyBase.Load
  gMapa.Visible = False

  'Inicio Mapa
  gMapa.MapProvider = GMapProviders.GoogleMap
  rdbtnNormal.Checked = True

  gMapa.MinZoom = 0
  gMapa.MaxZoom = 20
  gMapa.Zoom = 10
  gMapa.AutoScroll = True

  markerOverlay = New GMapOverlay("Unico")

  Using SqlConnection As New SqlConnection(StrTpm)
   Dim DSetTablas As New DataSet

   Dim ConsutaLista As String = "SELECT * FROM inf_tabletas_agentes ORDER BY asesor ASC"
   Dim daAgentes As New SqlDataAdapter(ConsutaLista, SqlConnection)

   daAgentes.Fill(DSetTablas, "usuarios")

   Me.cmbAgente.DataSource = DSetTablas.Tables("usuarios")
   Me.cmbAgente.DisplayMember = "asesor"
   Me.cmbAgente.ValueMember = "mac"
   Me.cmbAgente.SelectedIndex = 0
  End Using

  LimpiarInf()
  CargaInf(cmbAgente.SelectedValue.ToString)
 End Sub

 Private Sub InicioMapa(Latitud As Double, Longitud As Double)
  gMapa.Position = New PointLatLng(Latitud, Longitud)
 End Sub

 Private Sub AgregarMarcador(NombreMark As String, ToolTipMark As String, Latitud As Double, Longitud As Double, tipoColor As Int16)
  'Marcadores
  Dim Color As GMarkerGoogleType
  Select Case tipoColor
   Case 1 : Color = GMarkerGoogleType.blue
   Case 2 : Color = GMarkerGoogleType.green
   Case 3 : Color = GMarkerGoogleType.white_small
   Case 4 : Color = GMarkerGoogleType.red
   Case 5 : Color = GMarkerGoogleType.gray_small
   Case 6 : Color = GMarkerGoogleType.lightblue_dot
   Case 7 : Color = GMarkerGoogleType.orange
   Case 8 : Color = GMarkerGoogleType.pink
   Case 9 : Color = GMarkerGoogleType.purple
   Case 10 : Color = GMarkerGoogleType.yellow
  End Select

  marker = New GMarkerGoogle(New PointLatLng(Latitud, Longitud), Color)
  markerOverlay.Markers.Add(marker)
  marker.ToolTipText = ToolTipMark
  marker.ToolTipMode = MarkerTooltipMode.Always
 End Sub

 Private Sub LimpiarInf()
  gMapa.Visible = False
  chkBox_Mostrar_coordenadas.Checked = False
  Panel6.Visible = False
  markerOverlay.Clear()
  gMapa.Overlays.Clear()
 End Sub

 Private Sub CargaInf(findMAC As String)
  Dim command As SqlCommand
  Dim adapter As SqlDataAdapter
  Dim dtTable As DataTable

  'Abro la conexión
  Using SqlConnection As New SqlConnection(StrTpm)
   Dim DSetTablas As New DataSet

   Dim ConsutaLista As String = "SELECT * FROM inf_tabletas_agentes WHERE mac = '" & findMAC & "'"
   Dim daInfTablet As New SqlDataAdapter(ConsutaLista, SqlConnection)

   daInfTablet.Fill(DSetTablas, "inf")
   dtTable = DSetTablas.Tables("inf")

   Try
    If dtTable.Rows.Count > 0 Then
     'hago el llenado del mapa
     Dim inicio As Boolean = True
     Dim toolT As String = ""
     Dim Posicion As Int16 = 0
     For Each row As DataRow In dtTable.Rows
      Posicion = Posicion + 1
      If IsDBNull(row.Item("ultimaPos_latitud")) = False Then
       If inicio Then
        InicioMapa(row.Item("ultimaPos_latitud"), row.Item("ultimaPos_longitud"))
        inicio = False
       End If

       AgregarMarcador(row.Item("mac"), row.Item("asesor"), row.Item("ultimaPos_latitud"), row.Item("ultimaPos_longitud"), Posicion)
      End If

      lblMAC.Text = row.Item("mac")
      lblUA.Text = row.Item("ultimo_acceso")
      lblVer.Text = "Ver." & row.Item("version_tablet")
      lblLat.Text = row.Item("ultimaPos_latitud")
      lblLng.Text = row.Item("ultimaPos_longitud")
     Next
     'Agregar el Overlay en el mapa
     gMapa.Overlays.Add(markerOverlay)
     gMapa.Visible = True
    End If
   Catch expSQL As SqlException
    MsgBox(expSQL.ToString, MsgBoxStyle.OkOnly, "SQL Exception")
    LimpiarInf()
    Exit Sub
   End Try
  End Using
 End Sub

 Private Sub cmbAgente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAgente.SelectedIndexChanged
  LimpiarInf()
  CargaInf(cmbAgente.SelectedValue.ToString)
 End Sub


 Private Sub chkBox_Mostrar_coordenadas_CheckedChanged_1(sender As Object, e As EventArgs) Handles chkBox_Mostrar_coordenadas.CheckedChanged
  If chkBox_Mostrar_coordenadas.Checked = True Then
   Panel6.Visible = True
  Else
   Panel6.Visible = False
  End If
 End Sub

 Private Sub rdbtnNormal_CheckedChanged_1(sender As Object, e As EventArgs) Handles rdbtnNormal.CheckedChanged
  If rdbtnNormal.Checked = True Then
   gMapa.MapProvider = GMapProviders.GoogleMap
  Else
   gMapa.MapProvider = GMapProviders.GoogleSatelliteMap
  End If
  gMapa.Refresh()
 End Sub

 Private Sub rdbtnSatelite_CheckedChanged_1(sender As Object, e As EventArgs) Handles rdbtnSatelite.CheckedChanged
  If rdbtnSatelite.Checked = True Then
   gMapa.MapProvider = GMapProviders.GoogleSatelliteMap
  Else
   gMapa.MapProvider = GMapProviders.GoogleMap
  End If
  gMapa.Refresh()
 End Sub

 Private Sub gMapa_OnMarkerClick(item As GMapMarker, e As MouseEventArgs) Handles gMapa.OnMarkerClick
  Dim Url As String
  Url = "https://www.google.com/maps/search/?api=1&query=" & item.Position.Lat & "," & item.Position.Lng
  Process.Start(Url)
 End Sub

 Private Sub gMapa_MouseWheel(sender As Object, e As MouseEventArgs) Handles gMapa.MouseWheel
  trackZoom.Value = Convert.ToInt32(gMapa.Zoom)
  lblzoom.Text = trackZoom.Value.ToString
 End Sub

 Private Sub PanelControles_Paint(sender As Object, e As PaintEventArgs) Handles PanelControles.Paint

 End Sub

 Private Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
  markerOverlay.Clear()
  gMapa.Overlays.Clear()
  CargaInf(cmbAgente.SelectedValue.ToString)
 End Sub

 Private Sub trackZoom_ValueChanged(sender As Object, e As EventArgs) Handles trackZoom.ValueChanged
  gMapa.Zoom = trackZoom.Value
  lblzoom.Text = trackZoom.Value.ToString
 End Sub

End Class