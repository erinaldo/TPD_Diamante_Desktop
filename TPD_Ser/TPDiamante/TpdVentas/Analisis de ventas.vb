Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Analisis_de_ventas

 Dim DvArticulo As New DataView
 Dim DvCabecera As New DataView
 Dim DvPorProducto As New DataView
 Dim DvDetalle As New DataView
 Dim DvPorLinea As New DataView
 Dim DvMeses As New DataView

 Private Enum AxisX
  TODOS
  Puebla
  Merida
  Tuxtla
 End Enum

 Private Enum AxisY
  Piezas
  Ventas
  Promedio
 End Enum

 Dim valX As AxisX
 Dim valY As AxisY

 Private Sub Analisis_de_ventas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
  Dim FchInicio As DateTime
  FchInicio = DateAdd(DateInterval.Month, -1, Date.Now)
  Me.DtpFechaIni.Value = Format(FchInicio, "dd/MM/yyyy")
  Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

  Dim ConsutaLista As String

  Using SqlConnection As New SqlConnection(StrCon)
   ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
   Dim daGArticulo As New SqlDataAdapter(ConsutaLista, SqlConnection)

   Dim DSetTablas As New DataSet
   daGArticulo.Fill(DSetTablas, "GArticulos")

   Dim fila As DataRow
   'Asignamos a fila la nueva Row(Fila)del Dataset
   fila = DSetTablas.Tables("GArticulos").NewRow
   'Agregamos los valores a los campos de la tabla
   fila("ItmsGrpNam") = "TODOS"
   fila("ItmsGrpCod") = 999
   'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
   DSetTablas.Tables("GArticulos").Rows.Add(fila)

   Me.CmbGrupoArticulo.DataSource = DSetTablas.Tables("GArticulos")
   Me.CmbGrupoArticulo.DisplayMember = "ItmsGrpNam"
   Me.CmbGrupoArticulo.ValueMember = "ItmsGrpCod"
   Me.CmbGrupoArticulo.SelectedValue = 999

   '-----------------------------------------------------
   ConsutaLista = "SELECT ItemCode,ItemName,ItmsGrpCod FROM OITM ORDER BY ItemCode"
   Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

   daArticulo.Fill(DSetTablas, "Articulos")

   'Dim filaArticulo As DataRow
   ''Asignamos a fila la nueva Row(Fila)del Dataset
   'filaArticulo = DSetTablas.Tables("Articulos").NewRow
   ''Agregamos los valores a los campos de la tabla
   'filaArticulo("ItemName") = "TODOS"
   'filaArticulo("ItemCode") = "TODOS"
   'filaArticulo("ItmsGrpCod") = 999
   ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
   'DSetTablas.Tables("Articulos").Rows.Add(filaArticulo)

   DvArticulo.Table = DSetTablas.Tables("Articulos")

   Me.CmbArticulo.DataSource = DvArticulo
   Me.CmbArticulo.DisplayMember = "ItemCode"
   Me.CmbArticulo.ValueMember = "ItemCode"
   Me.CmbArticulo.SelectedIndex = 0
  End Using

  'Leno el tipo de grafica
  Dim dt As DataTable
  dt = New DataTable("TiposG")

  dt.Columns.Add("Codigo")
  dt.Columns.Add("Descripcion")

  Dim dr As DataRow

  dr = dt.NewRow()
  dr("Codigo") = SeriesChartType.Line.ToString
  dr("Descripcion") = "Linea"
  dt.Rows.Add(dr)

  dr = dt.NewRow()
  dr("Codigo") = SeriesChartType.Bar.ToString
  dr("Descripcion") = "Barra horizontales"
  dt.Rows.Add(dr)

  dr = dt.NewRow()
  dr("Codigo") = SeriesChartType.StackedBar.ToString
  dr("Descripcion") = "Barras horizontales apiladas"
  dt.Rows.Add(dr)

  dr = dt.NewRow()
  dr("Codigo") = SeriesChartType.Column.ToString
  dr("Descripcion") = "Barras verticales"
  dt.Rows.Add(dr)

  dr = dt.NewRow()
  dr("Codigo") = SeriesChartType.Pie.ToString
  dr("Descripcion") = "Pie"
  dt.Rows.Add(dr)

  cbo_TipoGrafica.DataSource = dt
  cbo_TipoGrafica.ValueMember = "Codigo"
  cbo_TipoGrafica.DisplayMember = "Descripcion"

  cbo_TipoGrafica.SelectedIndex = 0
 End Sub

 Private Sub CmbGrupoArticulo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbGrupoArticulo.SelectedIndexChanged
  Try
   If CmbGrupoArticulo.SelectedValue Is Nothing Or CmbGrupoArticulo.SelectedValue = 999 Then
    DvArticulo.RowFilter = String.Empty
    CmbArticulo.SelectedIndex = 0
   Else
    DvArticulo.RowFilter = "ItmsGrpCod = " & Trim(Me.CmbGrupoArticulo.SelectedValue.ToString) & " OR ItmsGrpCod = 999"
    CmbArticulo.SelectedIndex = 0
   End If
  Catch ex As Exception
  End Try
 End Sub

 Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
  Espere(True)
  MostrarGrafica(False)
  If rdb_PorProducto.Checked = True Then
   If CmbArticulo.Text = "TODOS" Then
    Espere(False)
    dgv_Cabecera.DataSource = Nothing
    dgv_Producto.DataSource = Nothing
    Exit Sub
   End If
   GetResPorProducto(DtpFechaIni.Value, DtpFechaTer.Value, "", CmbArticulo.SelectedValue)
  Else
   If CmbGrupoArticulo.Text = "TODOS" Then
    Espere(False)
    dgv_Cabecera.DataSource = Nothing
    dgv_Producto.DataSource = Nothing
    Exit Sub
   End If
   GetResPorProducto(DtpFechaIni.Value, DtpFechaTer.Value, CmbGrupoArticulo.Text, "")
  End If
  Espere(False)
 End Sub

 Private Sub GetResPorProducto(FechaInicial As Date, FechaFinal As Date, Linea As String, Articulo As String)
  Dim cnn As SqlConnection = Nothing
  Dim cmd As SqlCommand = Nothing
  Dim cmd4 As SqlCommand = Nothing
  Dim DsInf As New DataSet
  Try
   cnn = New SqlConnection(StrTpm)
   cnn.Open()
   cmd4 = New SqlCommand("SP_AnalisisVentas2", cnn)
   cmd4.CommandType = CommandType.StoredProcedure

   'Siempre traera toda la informacion
   cmd4.Parameters.Add("@fechaInicio", SqlDbType.Date).Value = FechaInicial.Date
   cmd4.Parameters.Add("@fechaFin", SqlDbType.Date).Value = FechaFinal.Date
   cmd4.Parameters.Add("@linea", SqlDbType.VarChar).Value = Linea
   cmd4.Parameters.Add("@articulo", SqlDbType.VarChar).Value = Articulo
   If ckCteProp.Checked Then
    cmd4.Parameters.Add("@ClientesPropios", SqlDbType.Bit).Value = 1
   Else
    cmd4.Parameters.Add("@ClientesPropios", SqlDbType.Bit).Value = 0
   End If

   cmd4.CommandTimeout = 600

   cmd4.ExecuteNonQuery()
   cmd4.Connection.Close()
   Dim da As New SqlDataAdapter
   da.SelectCommand = cmd4
   da.SelectCommand.Connection = cnn

   ''--------------------------------------------
   da.Fill(DsInf, "Informacion")

   DsInf.Tables(0).TableName = "Detalles"
   DsInf.Tables(1).TableName = "Meses"

   DvPorProducto.Table = DsInf.Tables("Detalles")
   DvDetalle = New DataView
   DvMeses.Table = DsInf.Tables("Meses")

   dgv_Cabecera.DataSource = Nothing
   dgv_Producto.DataSource = Nothing

   dgv_Cabecera.DataSource = DvMeses
   dgv_Producto.DataSource = DvPorProducto

   MostrarGrafica(True)

   DiseñoCabecera()
   DiseñoPorProducto()

  Catch ex As Exception
   'MsgBox(ex.Message)
   Exit Sub
  Finally
   If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
    cnn.Close()
   End If
  End Try
 End Sub

 Private Sub DiseñoCabecera()
  '-------Diseño de DATAGRID Agentes
  With Me.dgv_Cabecera
   Try
    .ReadOnly = True
    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    .DefaultCellStyle.BackColor = Color.AliceBlue
    .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

    dgv_Cabecera.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    'Propiedad para no mostrar el cuadro que se encuentra en la parte
    'Superior Izquierda del gridview
    'Superior Izquierda del gridview
    .RowHeadersVisible = False
    .ColumnHeadersVisible = False
    .RowHeadersWidth = 20
    .ColumnHeadersHeight = 30

    .Columns(0).HeaderText = CmbArticulo.SelectedValue
    .Columns(0).Width = 104
    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    Dim iCol As Int16
    For iCol = 1 To DvMeses.Table.Columns.Count - 2 Step 2
     .Columns(iCol).HeaderText = DvMeses.Table.Rows(0).ItemArray(iCol).ToString()
     .Columns(iCol).Width = 220
     .Columns(iCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
     .Columns(iCol).DefaultCellStyle.SelectionBackColor = Color.Gray

     .Columns(iCol + 1).Width = 10
     .Columns(iCol + 1).DefaultCellStyle.BackColor = Color.Black
    Next

    .Columns(iCol).HeaderText = DvMeses.Table.Rows(0).ItemArray(iCol).ToString()
    .Columns(iCol).Width = 140
    .Columns(iCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(iCol).DefaultCellStyle.SelectionBackColor = Color.Gray

    'Pinto el renglo
    Dim numfilas As Integer
    numfilas = dgv_Cabecera.RowCount 'cuenta las filas del DataGrid

    '.AutoResizeColumns()
   Catch ex As Exception
   End Try
  End With
 End Sub

 Private Sub DiseñoPorProducto()
  '-------Diseño de DATAGRID Agentes
  With Me.dgv_Producto
   Try
    .ReadOnly = True
    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
    '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    .DefaultCellStyle.BackColor = Color.AliceBlue
    '.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

    dgv_Producto.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    'Propiedad para no mostrar el cuadro que se encuentra en la parte
    'Superior Izquierda del gridview
    .RowHeadersVisible = True
    .ColumnHeadersVisible = True
    .RowHeadersWidth = 4
    .ColumnHeadersHeight = 23
    .CellBorderStyle = DataGridViewCellBorderStyle.Raised
    '.BorderStyle = BorderStyle.None


    Dim iCol As Int16
    .Columns(0).HeaderText = "SUCURSAL"
    .Columns(0).Width = 100
    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(0).DefaultCellStyle.SelectionBackColor = Color.Gray

    For iCol = 1 To DvPorProducto.Table.Columns.Count - 3 Step 4
     .Columns(iCol).HeaderText = "Piezas"
     .Columns(iCol).Width = 50
     .Columns(iCol).DefaultCellStyle.Format = "###,###,##0"
     .Columns(iCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
     '.Columns(iCol).DefaultCellStyle.SelectionBackColor = Color.Gray

     .Columns(iCol + 1).HeaderText = "Importe ventas"
     .Columns(iCol + 1).Width = 100
     .Columns(iCol + 1).DefaultCellStyle.Format = "$ ###,###,##0.#0"
     .Columns(iCol + 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

     .Columns(iCol + 2).HeaderText = "Precio promedio"
     .Columns(iCol + 2).Width = 70
     .Columns(iCol + 2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
     .Columns(iCol + 2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
     .BorderStyle = BorderStyle.Fixed3D

     .Columns(iCol + 3).Width = 10
     .Columns(iCol + 3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    Next

    .Columns(iCol).HeaderText = "Tot. Pzas"
    .Columns(iCol).Width = 60
    .Columns(iCol).DefaultCellStyle.Format = "###,###,##0"
    .Columns(iCol).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(iCol + 1).HeaderText = "Promedio Pzas."
    .Columns(iCol + 1).Width = 80
    .Columns(iCol + 1).DefaultCellStyle.Format = "###,###,##0.#0"
    .Columns(iCol + 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    'Pinto el renglon de separacion
    Dim numfilas As Integer
    numfilas = dgv_Producto.RowCount 'cuenta las filas del DataGrid

    'recorre las filas del DataGrid
    For i = 0 To (numfilas - 1)
     For y = 4 To DvPorProducto.Table.Columns.Count - 1 Step 4
      .Rows(i).Cells(y).Style.BackColor = Color.Black
     Next
    Next

   Catch ex As Exception
   End Try
  End With
 End Sub

 Private Sub Espere(Esperando As Boolean)
  If Esperando Then
   Cursor = System.Windows.Forms.Cursors.WaitCursor
   panelEspere.Visible = True
   panelEspere.Refresh()
  Else
   Cursor = System.Windows.Forms.Cursors.Default
   panelEspere.Visible = False
  End If
 End Sub

 Private Sub MostrarGrafica(Status As Boolean)
  chart1.Series.Clear()
  chart1.ResetAutoValues()
  If Status = True Then
   rdb_todos.Checked = True
   rdb_Piezas.Checked = True
   valX = AxisX.TODOS
   valY = AxisY.Piezas
   Panel4.Visible = True
   chart1.Visible = True
  Else
   Panel4.Visible = False
   chart1.Visible = False
  End If
 End Sub

 Private Sub btnGrafica_Click(sender As Object, e As EventArgs) Handles btnGrafica.Click

  If DvMeses.Table Is Nothing Then
   Exit Sub
  End If

  chart1.Series.Clear()
  chart1.ResetAutoValues()

  With chart1.ChartAreas(0)
   .AxisX.Title = "MESES"
   .AxisX.MajorGrid.LineColor = Color.LightBlue
   .AxisX.Minimum = 0
   .AxisX.Interval = 1
   .AxisY.MajorGrid.LineColor = Color.LightGray
   .AxisY.Minimum = 0
   .BackColor = Color.FloralWhite
   .BackSecondaryColor = Color.White
   .BackGradientStyle = GradientStyle.HorizontalCenter
   .BorderColor = Color.Blue
   .BorderDashStyle = ChartDashStyle.Solid
   .BorderWidth = 2
   .ShadowOffset = 10
  End With

  'Dibujando el Chart
  Dim PosSerie As Int16 = 0
  Dim ValoresYP() As Double
  Dim ValoresYM() As Double
  Dim ValoresYT() As Double
  Dim PosY As Int16
  If valY = AxisY.Piezas Then
   PosY = 1
   chart1.ChartAreas(0).AxisY.Title = "Numero de piezas"
  ElseIf valY = AxisY.Ventas Then
   PosY = 2
   chart1.ChartAreas(0).AxisY.Title = "Importe de ventas ($)"
  ElseIf valY = AxisY.Promedio Then
   PosY = 3
   chart1.ChartAreas(0).AxisY.Title = "Precio promedio ($)"
  End If
  ReDim ValoresYP(DvMeses.Table.Columns.Count - 1)
  ReDim ValoresYM(DvMeses.Table.Columns.Count - 1)
  ReDim ValoresYT(DvMeses.Table.Columns.Count - 1)
  Dim Indice As Byte = 0
  For x As Integer = PosY To DvPorProducto.Table.Columns.Count - 3 Step 4
   Indice = Indice + 1
   If IsDBNull(DvPorProducto.Item(0).Row(x)) Then
    ValoresYP(Indice) = 0
   Else
    ValoresYP(Indice) = DvPorProducto.Item(0).Row(x)
   End If

   If IsDBNull(DvPorProducto.Item(1).Row(x)) Then
    ValoresYM(Indice) = 0
   Else
    ValoresYM(Indice) = DvPorProducto.Item(1).Row(x)
   End If

   If IsDBNull(DvPorProducto.Item(2).Row(x)) Then
    ValoresYT(Indice) = 0
   Else
    ValoresYT(Indice) = DvPorProducto.Item(2).Row(x)
   End If
  Next

  'chart1.Series(0).ChartType = SeriesChartType.Column
  If valX = AxisX.Puebla Or valX = AxisX.TODOS Then
   chart1.Series.Add("P = PUEBLA")
   With chart1.Series(PosSerie)
    seleccionaGrafico(PosSerie)
    .BorderWidth = 1
    .Color = Color.Red
    .BorderDashStyle = ChartDashStyle.Dash
    .MarkerStyle = MarkerStyle.Square
    .MarkerSize = 5

    Dim y As Single = 0
    For x As Integer = 1 To DvMeses.Table.Columns.Count - 3 Step 2
     y = y + 1
     .Points.AddXY(DvMeses.Item(0).Row(x), ValoresYP(y))
    Next
   End With
  End If

  If valX = AxisX.TODOS Then
   PosSerie = PosSerie + 1
  End If

  If valX = AxisX.Merida Or valX = AxisX.TODOS Then
   chart1.Series.Add("M = MERIDA")
   With chart1.Series(PosSerie)
    seleccionaGrafico(PosSerie)
    .BorderWidth = 1
    .Color = Color.Black
    .BorderDashStyle = ChartDashStyle.Dash
    .MarkerStyle = MarkerStyle.Star5
    .MarkerSize = 5

    Dim y As Single = 0
    For x As Integer = 1 To DvMeses.Table.Columns.Count - 3 Step 2
     y = y + 1
     .Points.AddXY(DvMeses.Item(0).Row(x), ValoresYM(y))
    Next
   End With
  End If

  If valX = AxisX.TODOS Then
   PosSerie = PosSerie + 1
  End If
  If valX = AxisX.Tuxtla Or valX = AxisX.TODOS Then
   chart1.Series.Add("T = TUXTLA")
   With chart1.Series(PosSerie)
    seleccionaGrafico(PosSerie)
    .BorderWidth = 1
    .Color = Color.DarkOrange
    .BorderDashStyle = ChartDashStyle.Dash
    .MarkerStyle = MarkerStyle.Diamond
    .MarkerSize = 5

    Dim y As Single
    For x As Integer = 1 To DvMeses.Table.Columns.Count - 3 Step 2
     y = y + 1
     .Points.AddXY(DvMeses.Item(0).Row(x), ValoresYT(y))
    Next
   End With
  End If
 End Sub

 Private Sub seleccionaGrafico(idGrafico As Int16)
  Select Case cbo_TipoGrafica.SelectedValue
   Case SeriesChartType.Line.ToString
    chart1.Series(idGrafico).ChartType = SeriesChartType.Line
   Case SeriesChartType.Bar.ToString
    chart1.Series(idGrafico).ChartType = SeriesChartType.Bar
   Case SeriesChartType.StackedBar.ToString
    chart1.Series(idGrafico).ChartType = SeriesChartType.StackedBar
   Case SeriesChartType.Column.ToString
    chart1.Series(idGrafico).ChartType = SeriesChartType.Column
   Case SeriesChartType.Pie.ToString
    chart1.Series(idGrafico).ChartType = SeriesChartType.Pie
  End Select
 End Sub

 Private Sub rdb_puebla_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_puebla.CheckedChanged
  If rdb_puebla.Checked = True Then
   valX = AxisX.Puebla
  End If
 End Sub

 Private Sub rdb_merida_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_merida.CheckedChanged
  If rdb_merida.Checked = True Then
   valX = AxisX.Merida
  End If
 End Sub

 Private Sub rdb_uxtla_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_tuxtla.CheckedChanged
  If rdb_tuxtla.Checked = True Then
   valX = AxisX.Tuxtla
  End If
 End Sub

 Private Sub rdb_Piezas_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Piezas.CheckedChanged
  If rdb_Piezas.Checked = True Then
   valY = AxisY.Piezas
  End If
 End Sub

 Private Sub rdb_ImporteVentas_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_ImporteVentas.CheckedChanged
  If rdb_ImporteVentas.Checked = True Then
   valY = AxisY.Ventas
  End If
 End Sub

 Private Sub rdb_PrecioPromedio_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_PrecioPromedio.CheckedChanged
  If rdb_PrecioPromedio.Checked = True Then
   valY = AxisY.Promedio
  End If
 End Sub

 Private Sub rdb_todos_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_todos.CheckedChanged
  If rdb_todos.Checked = True Then
   valX = AxisX.TODOS
  End If
 End Sub

 Private Sub dgv_Producto_Scroll(sender As Object, e As ScrollEventArgs) Handles dgv_Producto.Scroll
  dgv_Cabecera.HorizontalScrollingOffset = dgv_Producto.HorizontalScrollingOffset
 End Sub

 Private Sub dgv_Cabecera_Scroll(sender As Object, e As ScrollEventArgs) Handles dgv_Cabecera.Scroll
  dgv_Producto.HorizontalScrollingOffset = dgv_Cabecera.HorizontalScrollingOffset
 End Sub

 Private Sub rdb_PorProducto_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_PorProducto.CheckedChanged
  Label6.Visible = True
  CmbArticulo.Visible = True
  CmbArticulo.SelectedIndex = 0
 End Sub

 Private Sub rdb_PorLinea_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_PorLinea.CheckedChanged
  Label6.Visible = False
  CmbArticulo.Visible = False
 End Sub

 Private Sub btn_Exportar_Click(sender As Object, e As EventArgs) Handles btn_Exportar.Click
  Dim myStream As Stream
  Dim saveFileDialog1 As New SaveFileDialog()

  saveFileDialog1.Filter = "GIF files (*.gif)|*.GIF"
  saveFileDialog1.FilterIndex = 2
  saveFileDialog1.RestoreDirectory = True

  If saveFileDialog1.ShowDialog() = DialogResult.OK Then
   myStream = saveFileDialog1.OpenFile()
   If (myStream IsNot Nothing) Then
    ' Code to write the stream goes here.
    myStream.Close()
    Try
     chart1.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Gif)
    Catch ex As Exception
     MsgBox(ex.Message, MsgBoxStyle.Critical, "Error!")
    End Try
   End If
  End If
 End Sub
End Class