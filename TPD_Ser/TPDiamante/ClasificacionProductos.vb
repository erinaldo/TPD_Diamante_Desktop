Imports System.Data.SqlClient

Imports System.Data
Imports System.Configuration



Imports System.IO
Imports ClosedXML.Excel


Public Class ClasificacionProductosPorVentas

  Public StrTpm As String = conexion_universal.CadenaSQL
  Public StrCon As String = conexion_universal.CadenaSQLSAP
  Public conexion As New SqlConnection(conexion_universal.CadenaSQL)

  Dim DvClasificacion As New DataView

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    Llenar_DataGridView()
  End Sub

  Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

  End Sub

  'Llenar combobox almacen

  Private Sub LlenaAlmacen()

    Dim ConsutaLista As String

    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)


      Dim DSetTablas As New DataSet
      ConsutaLista = "select WhsCode, WhsName from OWHS where WhsCode='01' or WhsCode='03' or WhsCode='07'"
      Dim daAlmacen As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

      'Dim DSetTablas As New DataSet
      daAlmacen.Fill(DSetTablas, "Almacen")

      Dim fila As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      fila = DSetTablas.Tables("Almacen").NewRow

      'Agregamos los valores a los campos de la tabla
      fila("whsname") = "TODOS"
      fila("whscode") = 99

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Almacen").Rows.Add(fila)

      Me.cbAlmacen.DataSource = DSetTablas.Tables("Almacen")
      Me.cbAlmacen.DisplayMember = "whsname"
      Me.cbAlmacen.ValueMember = "whscode"
      Me.cbAlmacen.SelectedValue = 99


    End Using
  End Sub

  'Llenar combobox línea

  Sub LlenaLinea()
    Try
      Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
        Dim ConsutaLista As String
        Dim ds As New DataSet
        ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
        Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

        Dim dsArt As New DataSet
        daArticulo.Fill(dsArt)

        Dim fila As Data.DataRow

        'Asignamos a fila la nueva Row(Fila)del Dataset
        fila = dsArt.Tables(0).NewRow

        'Agregamos los valores a los campos de la tabla
        fila("ItmsGrpNam") = "TODAS"
        fila("ItmsGrpCod") = 999

        ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
        dsArt.Tables(0).Rows.Add(fila)

        Me.cmbLinea.DataSource = dsArt.Tables(0)
        Me.cmbLinea.DisplayMember = "ItmsGrpNam"
        Me.cmbLinea.ValueMember = "ItmsGrpCod"
        Me.cmbLinea.SelectedValue = 999

      End Using
    Catch ex As Exception
      MsgBox("Error al cargar las lineas: " + ex.Message)
    End Try
  End Sub

  Private Sub ClasificacionProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    chk80_20.Checked = False
    Me.dtpInicio.Value = Format(Date.Now, "yyyy-MM-dd")
    Me.dtpFinal.Value = Format(Date.Now, "yyyy-MM-dd")
    LlenaAlmacen()
    LlenaLinea()
  End Sub


  Private Sub Llenar_DataGridView()
    'Los argumentos de conexión a la base de datos 

    Dim args As String = conexion_universal.CadenaSQL

    Dim command As SqlCommand
    Dim adapter As SqlDataAdapter
    Dim dtTable As DataTable

    Using connection As SqlConnection = New SqlConnection(args)
      command = New SqlCommand("SP_ClasificacionPorductosTodosAlmacenes", connection)
      command.CommandType = CommandType.StoredProcedure
      adapter = New SqlDataAdapter(command)
      dtTable = New DataTable
      With command.Parameters
        'Envió los parámetros que necesito
        .Add(New SqlParameter("@FechaInicio", SqlDbType.Date)).Value = Convert.ToDateTime(dtpInicio.Value)
        .Add(New SqlParameter("@FechaFin", SqlDbType.Date)).Value = Convert.ToDateTime(dtpFinal.Value)

        If cbAlmacen.Text <> "TODOS" Then
          .Add(New SqlParameter("@almacen", SqlDbType.Text)).Value = cbAlmacen.SelectedValue
        End If
        If cmbLinea.Text <> "TODAS" Then
          .Add(New SqlParameter("@linea", SqlDbType.Text)).Value = cmbLinea.Text
        End If
        If chk80_20.Checked = True Then
          .Add(New SqlParameter("@Reporte80_20", SqlDbType.Int)).Value = 1
        End If
      End With

      Try
        adapter.Fill(dtTable)
        dgvClasificacion.DataSource = dtTable
        'dgvClasificacion.AutoGenerateColumns = True
      Catch expSQL As SqlException
        MsgBox(expSQL.ToString, MsgBoxStyle.OkOnly, "SQL Exception")
        Exit Sub
      End Try
    End Using

    'If cbAlmacen.Text <> "TODOS" And cmbLinea.Text <> "TODAS" Then

    '  'Abro la conexión
    '  Using connection As SqlConnection = New SqlConnection(args)

    '    command = New SqlCommand("SP_ClasificacionMerida2", connection)
    '    command.CommandType = CommandType.StoredProcedure
    '    adapter = New SqlDataAdapter(command)
    '    dtTable = New DataTable
    '    With command.Parameters
    '      'Envió los parámetros que necesito
    '      .Add(New SqlParameter("@FechaInicio", SqlDbType.Date)).Value = Convert.ToDateTime(dtpInicio.Value)
    '      .Add(New SqlParameter("@FechaFin", SqlDbType.Date)).Value = Convert.ToDateTime(dtpFinal.Value)
    '      .Add(New SqlParameter("@linea", SqlDbType.Text)).Value = cmbLinea.Text
    '      .Add(New SqlParameter("@almacen", SqlDbType.Text)).Value = cbAlmacen.SelectedValue
    '    End With

    '    Try

    '      adapter.Fill(dtTable)

    '      dgvClasificacion.DataSource = dtTable

    '      'dgvClasificacion.AutoGenerateColumns = True
    '    Catch expSQL As SqlException
    '      MsgBox(expSQL.ToString, MsgBoxStyle.OkOnly, "SQL Exception")
    '      Exit Sub
    '    End Try



    '  End Using

    'ElseIf cbAlmacen.Text = "TODOS" And cmbLinea.Text = "TODAS" Then
    '  Using connection As SqlConnection = New SqlConnection(args)

    '    command = New SqlCommand("SP_ClasificacionPorductosTodosAlmacenes", connection)
    '    command.CommandType = CommandType.StoredProcedure
    '    adapter = New SqlDataAdapter(command)
    '    dtTable = New DataTable
    '    With command.Parameters
    '      'Envió los parámetros que necesito
    '      .Add(New SqlParameter("@FechaInicio", SqlDbType.Date)).Value = Convert.ToDateTime(dtpInicio.Value)
    '      .Add(New SqlParameter("@FechaFin", SqlDbType.Date)).Value = Convert.ToDateTime(dtpFinal.Value)
    '      If chk80_20.Checked = True Then
    '        .Add(New SqlParameter("@Reporte80_20", SqlDbType.Int)).Value = 1
    '      End If
    '    End With

    '    Try

    '      adapter.Fill(dtTable)

    '      dgvClasificacion.DataSource = dtTable

    '      'dgvClasificacion.AutoGenerateColumns = True
    '    Catch expSQL As SqlException
    '      MsgBox(expSQL.ToString, MsgBoxStyle.OkOnly, "SQL Exception")
    '      Exit Sub
    '    End Try



    '  End Using
    'ElseIf cbAlmacen.Text = "TODOS" And cmbLinea.Text <> "TODAS" Then
    '  Using connection As SqlConnection = New SqlConnection(args)

    '    command = New SqlCommand("SP_ClasificacionLineas", connection)
    '    command.CommandType = CommandType.StoredProcedure
    '    adapter = New SqlDataAdapter(command)
    '    dtTable = New DataTable
    '    With command.Parameters
    '      'Envió los parámetros que necesito
    '      .Add(New SqlParameter("@FechaInicio", SqlDbType.Date)).Value = Convert.ToDateTime(dtpInicio.Value)
    '      .Add(New SqlParameter("@FechaFin", SqlDbType.Date)).Value = Convert.ToDateTime(dtpFinal.Value)
    '      .Add(New SqlParameter("@linea", SqlDbType.Text)).Value = cmbLinea.Text

    '    End With

    '    Try

    '      adapter.Fill(dtTable)

    '      dgvClasificacion.DataSource = dtTable

    '      'dgvClasificacion.AutoGenerateColumns = True
    '    Catch expSQL As SqlException
    '      MsgBox(expSQL.ToString, MsgBoxStyle.OkOnly, "SQL Exception")
    '      Exit Sub
    '    End Try



    '  End Using

    'ElseIf cbAlmacen.Text <> "TODOS" And cmbLinea.Text = "TODAS" Then
    '  Using connection As SqlConnection = New SqlConnection(args)

    '    command = New SqlCommand("SP_ClasificacionAlmacenes", connection)
    '    command.CommandType = CommandType.StoredProcedure
    '    adapter = New SqlDataAdapter(command)
    '    dtTable = New DataTable
    '    With command.Parameters
    '      'Envió los parámetros que necesito
    '      .Add(New SqlParameter("@FechaInicio", SqlDbType.Date)).Value = Convert.ToDateTime(dtpInicio.Value)
    '      .Add(New SqlParameter("@FechaFin", SqlDbType.Date)).Value = Convert.ToDateTime(dtpFinal.Value)
    '      .Add(New SqlParameter("@almacen", SqlDbType.Text)).Value = cbAlmacen.SelectedValue



    '    End With

    '    Try

    '      adapter.Fill(dtTable)

    '      dgvClasificacion.DataSource = dtTable

    '      'dgvClasificacion.AutoGenerateColumns = True
    '    Catch expSQL As SqlException
    '      MsgBox(expSQL.ToString, MsgBoxStyle.OkOnly, "SQL Exception")
    '      Exit Sub
    '    End Try



    '  End Using
    'End If

  End Sub

  Sub Exportar()
    Try
      Dim dt As New DataTable()
      For Each columns As DataGridViewColumn In dgvClasificacion.Columns
        dt.Columns.Add(columns.HeaderText, columns.ValueType)
      Next
      For Each row As DataGridViewRow In dgvClasificacion.Rows
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
      saveFileDialog1.FileName = "Export_" & dgvClasificacion.Name.ToString() & ".xlsx"
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

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    Exportar()
  End Sub
End Class