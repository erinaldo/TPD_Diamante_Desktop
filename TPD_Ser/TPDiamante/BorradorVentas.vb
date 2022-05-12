Imports System.Data.SqlClient

Public Class BorradorVentas
  Dim DvCabecera As New DataView
  Dim DvAgentes As New DataView
  Dim DvSolicito As New DataView
  Dim DvCategoria As New DataView
  Dim DvDetalles As New DataView

  ''VARIABLE PARA LA CLASE COMANDOS SQL  
  Dim SQL As New Comandos_SQL()

  Private Sub VtasScoreCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

    Me.DtpFechaInicial.Value = Format(Date.Now, "dd/MM/yyyy")
    Me.DtpFechaFinal.Value = Format(Date.Now, "dd/MM/yyyy")
    '*********************************************************************************************************************************************
    'Codigo para obtener informacion de los agentes que cada usuario debe ver
    '*********************************************************************************************************************************************
    SQL.conectarTPM()

    '*********************************************************************************************************************************************
    'Variable para guardar la consulta de AGENTES y SUCURSALES en los combobox
    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
      Dim DSetTablas As New DataSet

      Dim daLineas As New SqlClient.SqlDataAdapter("SELECT DISTINCT T3.ItmsGrpCod, T3.ItmsGrpNam
                                                    FROM SBO_TPD.dbo.ODRF T0
                                                    INNER JOIN SBO_TPD.dbo.DRF1 T1 ON T0.DocEntry = T1.DocEntry
                                                    INNER JOIN SBO_TPD.dbo.OITM T2 ON T1.ItemCode = T2.ItemCode
                                                    INNER JOIN SBO_TPD.dbo.OITB T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod
                                                    ORDER BY T3.ItmsGrpNam ASC", SqlConnection)

      daLineas.Fill(DSetTablas, "Lineas")

      If DSetTablas.Tables("Lineas").Rows.Count > 1 Then
        Dim fila As DataRow
        'Asignamos a fila la nueva Row(Fila)del Dataset
        fila = DSetTablas.Tables("Lineas").NewRow
        'Agregamos los valores a los campos de la tabla
        fila("ItmsGrpNam") = "TODAS"
        fila("ItmsGrpCod") = 999

        'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
        DSetTablas.Tables("Lineas").Rows.Add(fila)
      End If

      Me.CmbLineas.DataSource = DSetTablas.Tables("Lineas")
      Me.CmbLineas.DisplayMember = "ItmsGrpNam"
      Me.CmbLineas.ValueMember = "ItmsGrpCod"
      If DSetTablas.Tables("Lineas").Rows.Count > 1 Then
        Me.CmbLineas.SelectedValue = 999
      Else
        Me.CmbLineas.SelectedIndex = 0
      End If

      '------------------------------------------------------------------------------------------------------------------
      '------------------------------------------------------------------------------------------------------------------
      Dim daSolicitante As New SqlClient.SqlDataAdapter("SELECT DISTINCT T1.OwnerID IdSolicitante, T2.U_NAME Solicitante
                                                         FROM SBO_TPD.dbo.ODRF T0
                                                         INNER JOIN SBO_TPD.dbo.OWDD T1 ON T0.DocEntry = T1.DraftEntry 
                                                         INNER JOIN SBO_TPD.dbo.OUSR T2 ON T1.OwnerID = T2.USERID
                                                         ORDER BY T2.U_NAME ASC", SqlConnection)

      daSolicitante.Fill(DSetTablas, "Solicito")

      Dim filaAgte As DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      filaAgte = DSetTablas.Tables("Solicito").NewRow

      'Agregamos los valores a los campos de la tabla
      filaAgte("Solicitante") = "TODOS"
      filaAgte("IdSolicitante") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Solicito").Rows.Add(filaAgte)

      DvSolicito.Table = DSetTablas.Tables("Solicito")

      Me.CmbSolicitante.DataSource = DvSolicito
      Me.CmbSolicitante.DisplayMember = "Solicitante"
      Me.CmbSolicitante.ValueMember = "IdSolicitante"
      Me.CmbSolicitante.SelectedValue = 999

      '------------------------------------------------------------------------------------------------------------------
      '------------------------------------------------------------------------------------------------------------------
      Dim daCategoria As New SqlClient.SqlDataAdapter("SELECT DISTINCT U_CLASE IdCategoria, U_CLASE Categoria FROM SBO_TPD.dbo.OITW T4 WHERE U_CLASE IS NOT NULL ORDER BY U_CLASE ASC", SqlConnection)

      daCategoria.Fill(DSetTablas, "Categoria")

      Dim filCategoria As DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      filCategoria = DSetTablas.Tables("Categoria").NewRow

      'Agregamos los valores a los campos de la tabla
      filCategoria("Categoria") = "TODAS"
      filCategoria("IdCategoria") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Categoria").Rows.Add(filCategoria)

      DvCategoria.Table = DSetTablas.Tables("Categoria")

      Me.cmbCategoria.DataSource = DvCategoria
      Me.cmbCategoria.DisplayMember = "Categoria"
      Me.cmbCategoria.ValueMember = "IdCategoria"
      Me.cmbCategoria.SelectedValue = 999
    End Using

    SQL.Cerrar()
  End Sub

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    'Valido fechas
    If CDate(DtpFechaFinal.Value) < CDate(DtpFechaInicial.Value) Then
      MsgBox("Por favor verifique que las fechas sean correctas", MsgBoxStyle.Exclamation, "Se presentó un error en las fechas")
      Exit Sub
    End If

    Espere(True)
    DgCabecera.DataSource = Nothing
    GetInf()
    Espere(False)
  End Sub

  Sub GetInf()
    Dim cnn As SqlConnection = Nothing
    Dim cmd As SqlCommand = Nothing

    Dim cmd2 As SqlCommand = Nothing
    Dim cmd3 As SqlCommand = Nothing
    Dim cmd4 As SqlCommand = Nothing

    Try
      cnn = New SqlConnection(StrTpm)
      cnn.Open()
      cmd4 = New SqlCommand("SP_Borradores", cnn)
      cmd4.CommandType = CommandType.StoredProcedure
      cmd4.Parameters.Add("@FechaIni", SqlDbType.Date).Value = Me.DtpFechaInicial.Value
      cmd4.Parameters.Add("@FechaFin", SqlDbType.Date).Value = Me.DtpFechaFinal.Value
      If CmbSolicitante.SelectedValue <> 999 Then
        cmd4.Parameters.Add("@Solicitante", SqlDbType.Int).Value = Me.CmbSolicitante.SelectedValue
      End If
      If CmbLineas.SelectedValue <> 999 Then
        cmd4.Parameters.Add("@Linea", SqlDbType.Int).Value = Me.CmbLineas.SelectedValue
      End If
      If cmbCategoria.SelectedValue <> "999" Then
        cmd4.Parameters.Add("@Categoria", SqlDbType.VarChar).Value = Me.cmbCategoria.SelectedValue
      End If

      cmd4.ExecuteNonQuery()
      cmd4.Connection.Close()
      Dim da As New SqlDataAdapter
      da.SelectCommand = cmd4
      da.SelectCommand.Connection = cnn

      ''--------------------------------------------
      Dim DsInf As New DataSet
      da.Fill(DsInf, "Informacion")

      DsInf.Tables(0).TableName = "Cabecera"
      DsInf.Tables(1).TableName = "Detalles"

      DvCabecera.Table = DsInf.Tables("Cabecera")
      DvDetalles.Table = DsInf.Tables("Detalles")

      DgCabecera.DataSource = DvCabecera
      DgDetalles.DataSource = DvDetalles

    Catch ex As Exception
      'MsgBox(ex.Message)
      Exit Sub
    Finally
      If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
        cnn.Close()
      End If
    End Try

    '-------Diseño de DATAGRID Cabecera
    With Me.DgCabecera
      .ReadOnly = True
      'Color de Renglones en Grid
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      .RowHeadersVisible = True
      .RowHeadersWidth = 25
      DgCabecera.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try
        .Columns(0).HeaderText = "DocEntry"
        .Columns(0).Width = 200
        .Columns(0).Visible = False

        .Columns(1).HeaderText = "Fecha"
        .Columns(1).Width = 80
        .Columns(1).DefaultCellStyle.Format = "dd/MM/yyyy"
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(2).HeaderText = "Folio"
        .Columns(2).Width = 50
        .Columns(2).DefaultCellStyle.Format = "0"
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(3).HeaderText = "Id. Solicitante"
        .Columns(3).Width = 80
        .Columns(3).Visible = False

        .Columns(4).HeaderText = "Solicitante"
        .Columns(4).Width = 100
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        .Columns(5).HeaderText = "Id. Cliente"
        .Columns(5).Width = 85
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(5).Visible = False

        .Columns(6).HeaderText = "Nombre Cliente"
        .Columns(6).Width = 350
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        .Columns(7).HeaderText = "Cod. Almacen"
        .Columns(7).Width = 85
        .Columns(7).Visible = False

        .Columns(8).HeaderText = "Almacen"
        .Columns(8).Width = 85
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        .Columns(9).HeaderText = "Status"
        .Columns(9).Width = 85
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        .Columns(10).HeaderText = "Orden de venta"
        .Columns(10).Width = 85
        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(11).HeaderText = "Fecha O.V."
        .Columns(11).Width = 85
        .Columns(11).DefaultCellStyle.Format = "dd/MM/yyyy"
        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Catch ex As Exception
      End Try
    End With

    DiseñoDetalles()

    If DgCabecera.Rows.Count > 0 Then
      DgCabecera.Rows(0).Selected = True
    End If
  End Sub

  Private Sub DiseñoDetalles()
    With Me.DgDetalles
      Try
        .ReadOnly = True
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

        DgDetalles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        .RowHeadersVisible = True
        .RowHeadersWidth = 25
        .Columns(0).HeaderText = "Folio"
        .Columns(0).Width = 50
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(1).HeaderText = "DocEntry"
        .Columns(1).Visible = False

        .Columns(2).HeaderText = "Orden de Venta"
        .Columns(2).Visible = False

        .Columns(3).HeaderText = "Cod. Almacen"
        .Columns(3).Visible = False

        .Columns(4).HeaderText = "Id. Linea"
        .Columns(4).Visible = False

        .Columns(5).HeaderText = "Línea"
        .Columns(5).Width = 150
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        .Columns(6).HeaderText = "Id. Artículo"
        .Columns(6).Width = 150
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        .Columns(7).HeaderText = "Des. Artículo"
        .Columns(7).Width = 350
        .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        .Columns(8).HeaderText = "Cantidad solicitada"
        .Columns(8).Width = 85
        .Columns(8).DefaultCellStyle.Format = "###,###,##0.##"
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(9).HeaderText = "Existencia actual"
        .Columns(9).Width = 85
        .Columns(9).DefaultCellStyle.Format = "###,###,##0.##"
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(10).HeaderText = "Existencia original"
        .Columns(10).Width = 85
        .Columns(10).DefaultCellStyle.Format = "###,###,##0"
        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(11).HeaderText = "Categoría"
        .Columns(11).Width = 85
        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      Catch ex As Exception
      End Try
    End With
  End Sub

 '---Generar reporte en EXCEL de TOTALES
 'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnExportaAgentes.Click
 '  ExportarCabecera()
 'End Sub

 'Sub ExportarCabecera()
 '  Dim oExcel As Object
 '  Dim oBook As Object
 '  Dim oSheet As Object

 '  Dim Rangos As String = ""
 '  Dim Rangos2 As String = ""

 '  'MsgBox("El reporte se creara a continuación")

 '  'Abrimos un nuevo libro
 '  oExcel = CreateObject("Excel.Application")
 '  oBook = oExcel.workbooks.add
 '  oSheet = oBook.worksheets(1)

 '  'Declaramos el nombre de las columnas
 '  oSheet.range("A3").value = "Agente"
 '  oSheet.range("B3").value = "Importe linea 4"
 '  oSheet.range("C3").value = "% linea 4"
 '  oSheet.range("D3").value = "Importe linea 3"
 '  oSheet.range("E3").value = "% linea 3"
 '  oSheet.range("F3").value = "Importe linea 2"
 '  oSheet.range("G3").value = "% linea 2"
 '  oSheet.range("H3").value = "Importe linea 1"
 '  oSheet.range("I3").value = "% linea 1"
 '  oSheet.range("J3").value = "Importe linea 10"
 '  oSheet.range("K3").value = "% linea 10"
 '  oSheet.range("L3").value = "Ventas totales"

 '  'para poner la primera fila de los titulos en negrita
 '  oSheet.range("A3:L3").font.bold = True
 '  oSheet.Range("A3:L3").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGray)
 '  Dim fila_dt As Integer = 0
 '  Dim fila_dt_excel As Integer = 0
 '  Dim tanto_porcentaje As String = ""
 '  Dim marikona As Integer = 0

 '  Dim total_reg As Integer = 0

 '  total_reg = DgCabecera.RowCount
 '  For fila_dt = 0 To total_reg - 1
 '    'para leer una celda en concreto
 '    'el numero es la columna
 '    Dim cel0 As String = IIf(IsDBNull(Me.DgCabecera.Item(1, fila_dt).Value), 1, Me.DgCabecera.Item(1, fila_dt).Value)
 '    Dim cel1 As String = IIf(IsDBNull(Me.DgCabecera.Item(2, fila_dt).Value), 0, Me.DgCabecera.Item(2, fila_dt).Value)
 '    Dim cel2 As String = IIf(IsDBNull(Me.DgCabecera.Item(3, fila_dt).Value), 0, Me.DgCabecera.Item(3, fila_dt).Value)
 '    Dim cel3 As String = IIf(IsDBNull(Me.DgCabecera.Item(4, fila_dt).Value), 0, Me.DgCabecera.Item(4, fila_dt).Value)
 '    Dim cel4 As String = IIf(IsDBNull(Me.DgCabecera.Item(5, fila_dt).Value), 0, Me.DgCabecera.Item(5, fila_dt).Value)
 '    Dim cel5 As String = IIf(IsDBNull(Me.DgCabecera.Item(6, fila_dt).Value), 0, Me.DgCabecera.Item(6, fila_dt).Value)
 '    Dim cel6 As String = IIf(IsDBNull(Me.DgCabecera.Item(7, fila_dt).Value), 0, Me.DgCabecera.Item(7, fila_dt).Value)
 '    Dim cel7 As String = IIf(IsDBNull(Me.DgCabecera.Item(8, fila_dt).Value), 0, Me.DgCabecera.Item(8, fila_dt).Value)
 '    Dim cel8 As String = IIf(IsDBNull(Me.DgCabecera.Item(9, fila_dt).Value), 0, Me.DgCabecera.Item(9, fila_dt).Value)
 '    Dim cel9 As String = IIf(IsDBNull(Me.DgCabecera.Item(10, fila_dt).Value), 0, Me.DgCabecera.Item(10, fila_dt).Value)
 '    Dim cel10 As String = IIf(IsDBNull(Me.DgCabecera.Item(11, fila_dt).Value), 0, Me.DgCabecera.Item(11, fila_dt).Value)
 '    Dim cel11 As String = IIf(IsDBNull(Me.DgCabecera.Item(12, fila_dt).Value), 0, Me.DgCabecera.Item(12, fila_dt).Value)

 '    fila_dt_excel = fila_dt + 4

 '    'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
 '    oSheet.range("A" & fila_dt_excel).value = cel0
 '    oSheet.range("B" & fila_dt_excel).value = FormatNumber(cel1, 2)
 '    oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 4)
 '    oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
 '    oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 4)
 '    oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
 '    oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 4)
 '    oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
 '    oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 4)
 '    oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
 '    oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 4)
 '    oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 2)
 '  Next

 '  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
 '  oSheet.columns("A:l").entirecolumn.autofit()

 '  'Coloco autofiltro
 '  Dim objRangoFiltro As Microsoft.Office.Interop.Excel.Range = oSheet.Range("A3:l3")
 '  objRangoFiltro.AutoFilter(1)

 '  'Coloco colores en columnas igual que en la pantalla
 '  oSheet.Range("B4:B" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

 '  oSheet.Range("D4:D" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
 '  oSheet.Range("F4:F" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
 '  oSheet.Range("H4:H" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
 '  oSheet.Range("J4:J" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

 '  'Formato numerico
 '  oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "0.00 %"
 '  oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "0.00 %"
 '  oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "0.00 %"
 '  oExcel.Worksheets("Hoja1").Columns("I").NumberFormat = "0.00 %"
 '  oExcel.Worksheets("Hoja1").Columns("K").NumberFormat = "0.00 %"

 '  'ENCABEZADO DEL REPORTE GENERADO
 '  Dim sqlConnection1 As New SqlConnection(conexion_universal.CadenaSQLSAP)
 '  Dim cmd As New SqlCommand
 '  Dim returnValue As Object

 '  cmd.CommandText = "SELECT slpname from oslp where slpcode = " & Trim(Me.CmbSolicitante.SelectedValue.ToString)
 '  cmd.CommandType = CommandType.Text
 '  cmd.Connection = sqlConnection1

 '  sqlConnection1.Open()
 '  returnValue = cmd.ExecuteScalar()
 '  sqlConnection1.Close()
 '  Dim cnn As SqlConnection = Nothing

 '  If CmbSolicitante.SelectedValue = 999 Then
 '    oSheet.range("A1").value = "Reporte de Ventas por listas de precio de todos los AGENTES del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
 '  Else
 '    oSheet.range("A1").value = "Reporte de Ventas por lineas y listas de precio del AGENTE " + returnValue + " del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
 '  End If

 '  oSheet.range("C1").value = Rangos
 '  oSheet.range("C2").value = Rangos2

 '  oExcel.visible = True
 '  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
 '  GC.Collect()
 '  oSheet = Nothing
 '  oBook = Nothing
 '  oExcel = Nothing
 'End Sub


 ''--------Generar Excel Agentes
 'Private Sub BtnAgentes_Click(sender As Object, e As EventArgs) Handles BtnDetalles.Click
 '  'ExportarNuevoVendedores()
 '  exportaDetalles()
 'End Sub

 'Sub exportaDetalles()
 '  Dim oExcel As Object
 '  Dim oBook As Object
 '  Dim oSheet As Object

 '  Dim Rangos As String = ""
 '  Dim Rangos2 As String = ""

 '  'MsgBox("El reporte se creara a continuación")

 '  'Abrimos un nuevo libro
 '  oExcel = CreateObject("Excel.Application")
 '  oBook = oExcel.workbooks.add
 '  oSheet = oBook.worksheets(1)

 '  'Declaramos el nombre de las columnas
 '  oSheet.range("A3").value = "Línea"
 '  oSheet.range("B3").value = "Importe linea 4"
 '  oSheet.range("C3").value = "% linea 4"
 '  oSheet.range("D3").value = "Importe linea 3"
 '  oSheet.range("E3").value = "% linea 3"
 '  oSheet.range("F3").value = "Importe linea 2"
 '  oSheet.range("G3").value = "% linea 2"
 '  oSheet.range("H3").value = "Importe linea 1"
 '  oSheet.range("I3").value = "% linea 1"
 '  oSheet.range("J3").value = "Importe linea 10"
 '  oSheet.range("K3").value = "% linea 10"
 '  oSheet.range("L3").value = "Total por Línea"

 '  'para poner la primera fila de los titulos en negrita
 '  oSheet.range("A3:L3").font.bold = True
 '  oSheet.Range("A3:L3").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGray)
 '  Dim fila_dt As Integer = 0
 '  Dim fila_dt_excel As Integer = 0
 '  Dim tanto_porcentaje As String = ""
 '  Dim marikona As Integer = 0

 '  Dim total_reg As Integer = 0

 '  total_reg = DgDetalles.RowCount
 '  For fila_dt = 0 To total_reg - 1
 '    'para leer una celda en concreto
 '    'el numero es la columna
 '    Dim cel0 As String = IIf(IsDBNull(Me.DgDetalles.Item(1, fila_dt).Value), 1, Me.DgDetalles.Item(1, fila_dt).Value)
 '    Dim cel1 As String = IIf(IsDBNull(Me.DgDetalles.Item(2, fila_dt).Value), 0, Me.DgDetalles.Item(2, fila_dt).Value)
 '    Dim cel2 As String = IIf(IsDBNull(Me.DgDetalles.Item(3, fila_dt).Value), 0, Me.DgDetalles.Item(3, fila_dt).Value)
 '    Dim cel3 As String = IIf(IsDBNull(Me.DgDetalles.Item(4, fila_dt).Value), 0, Me.DgDetalles.Item(4, fila_dt).Value)
 '    Dim cel4 As String = IIf(IsDBNull(Me.DgDetalles.Item(5, fila_dt).Value), 0, Me.DgDetalles.Item(5, fila_dt).Value)
 '    Dim cel5 As String = IIf(IsDBNull(Me.DgDetalles.Item(6, fila_dt).Value), 0, Me.DgDetalles.Item(6, fila_dt).Value)
 '    Dim cel6 As String = IIf(IsDBNull(Me.DgDetalles.Item(7, fila_dt).Value), 0, Me.DgDetalles.Item(7, fila_dt).Value)
 '    Dim cel7 As String = IIf(IsDBNull(Me.DgDetalles.Item(8, fila_dt).Value), 0, Me.DgDetalles.Item(8, fila_dt).Value)
 '    Dim cel8 As String = IIf(IsDBNull(Me.DgDetalles.Item(9, fila_dt).Value), 0, Me.DgDetalles.Item(9, fila_dt).Value)
 '    Dim cel9 As String = IIf(IsDBNull(Me.DgDetalles.Item(10, fila_dt).Value), 0, Me.DgDetalles.Item(10, fila_dt).Value)
 '    Dim cel10 As String = IIf(IsDBNull(Me.DgDetalles.Item(11, fila_dt).Value), 0, Me.DgDetalles.Item(11, fila_dt).Value)
 '    Dim cel11 As String = IIf(IsDBNull(Me.DgDetalles.Item(12, fila_dt).Value), 0, Me.DgDetalles.Item(12, fila_dt).Value)

 '    fila_dt_excel = fila_dt + 4

 '    'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
 '    oSheet.range("A" & fila_dt_excel).value = cel0
 '    oSheet.range("B" & fila_dt_excel).value = FormatNumber(cel1, 2)
 '    oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 4)
 '    oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
 '    oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 4)
 '    oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
 '    oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 4)
 '    oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
 '    oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 4)
 '    oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
 '    oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 4)
 '    oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 2)
 '  Next

 '  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
 '  oSheet.columns("A:l").entirecolumn.autofit()

 '  'Coloco colores en columnas igual que en la pantalla
 '  oSheet.Range("B4:B" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
 '  oSheet.Range("D4:D" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
 '  oSheet.Range("F4:F" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
 '  oSheet.Range("H4:H" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
 '  oSheet.Range("J4:J" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

 '  'Formato numerico
 '  oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "0.00 %"
 '  oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "0.00 %"
 '  oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "0.00 %"
 '  oExcel.Worksheets("Hoja1").Columns("I").NumberFormat = "0.00 %"
 '  oExcel.Worksheets("Hoja1").Columns("K").NumberFormat = "0.00 %"

 '  'Coloco autofiltro
 '  Dim objRangoFiltro As Microsoft.Office.Interop.Excel.Range = oSheet.Range("A3:l3")
 '  objRangoFiltro.AutoFilter(1)

 '  Try
 '    If DgCabecera.Item(0, DgCabecera.CurrentRow.Index).Value.ToString = 999 Then
 '      oSheet.range("A1").value = "Reporte de Ventas por listas de precio de todos los AGENTES del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
 '    ElseIf DgCabecera.Item(0, DgCabecera.CurrentRow.Index).Value.ToString = 100 Then
 '      oSheet.range("A1").value = "Reporte de Ventas por listas de precio de la sucursal PUEBLA del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
 '    ElseIf DgCabecera.Item(0, DgCabecera.CurrentRow.Index).Value.ToString = 102 Then
 '      oSheet.range("A1").value = "Reporte de Ventas por listas de precio de la sucursal MERIDA del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
 '    ElseIf DgCabecera.Item(0, DgCabecera.CurrentRow.Index).Value.ToString = 103 Then
 '      oSheet.range("A1").value = "Reporte de Ventas por listas de precio de la sucursal TUXTLA GUTIERREZ del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
 '    Else
 '      oSheet.range("A1").value = "Reporte de Ventas por lineas y listas de precio del AGENTE " + DgCabecera.Item(1, DgCabecera.CurrentRow.Index).Value.ToString + " del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
 '    End If
 '  Catch
 '    oSheet.range("A1").value = "Reporte de Ventas por listas de precio de todos los AGENTES del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
 '  End Try

 '  oSheet.range("C1").value = Rangos
 '  oSheet.range("C2").value = Rangos2

 '  oExcel.visible = True
 '  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
 '  GC.Collect()
 '  oSheet = Nothing
 '  oBook = Nothing
 '  oExcel = Nothing
 'End Sub

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

 Private Sub DgCabecera_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgCabecera.CellEnter
    Try
      DvDetalles.RowFilter = "Folio =" & DgCabecera.Item(2, DgCabecera.CurrentRow.Index).Value.ToString & " AND DocEntry =" & DgCabecera.Item(0, DgCabecera.CurrentRow.Index).Value.ToString
      DiseñoDetalles()
    Catch ex As Exception

    End Try
  End Sub
End Class