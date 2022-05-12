Imports System.Data.SqlClient
Imports ClosedXML.Excel

Public Class ScoreCardCteObj
  ' Espacios de nombres  
  ' ''''''''''''''''''''''''''''''''''''''''' 

  Public conexion As New SqlConnection(conexion_universal.CadenaSQL)

  Public command2 As New SqlCommand("LOUpdate", conexion)
  Public Rs As SqlDataReader
  Public SQL As String
  Public Com As New SqlCommand
  'Public MiConexion As New SqlConnection(conexion)
  'Dim MiConexion As New SqlConnection(conexion)
  'Dim Rs As SqlDataReader
  'Dim Com As New SqlCommand

  Dim Dvtotales As New DataView

  'data view para mostrar combobox
  Dim Dvagentes As New DataView
  Dim DvObjIndAsesor As New DataView
  'data view para mostrar datagrid
  Dim DvagentesObj As New DataView

  'BindingSource  
  Private WithEvents bs As New BindingSource

  ' Adaptador de datos sql  
  Private SqlDataAdapter As SqlDataAdapter

  ' Cadena de conexión  
  Private cs As String = conexion_universal.cConstanteTPM

  ' flag  
  Private bEdit As Boolean

  Public StrCon As String = conexion_universal.CadenaSQLSAP

  ' actualizar los cambios al salir  
  ' ''''''''''''''''''''''''''''''''''''''''  
  Private Sub ScoreCardIngresarObjetivos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    If bEdit Then
      'preguntar si se desea guardar  
      If (MessageBox.Show("¿Desea guardar cambios?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
        Actualizar(False)
      End If
    End If
  End Sub


  Sub LlenaCmbAgte()
    Dim ConsutaLista As String

    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
      Dim DSetTablas As New DataSet
      ConsutaLista = "SELECT T0.slpcode,T0.slpname,T1.GroupCode FROM OSLP T0 "
      ConsutaLista &= "INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
      ConsutaLista &= "WHERE T1.CbrGralAdicional = 'N' AND T0.SLPCODE <> 1 ORDER BY slpname "

      Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

      daAgte.Fill(DSetTablas, "Agentes")

      Dim filaAgte As Data.DataRow
      Dim filaAgte1 As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      filaAgte = DSetTablas.Tables("Agentes").NewRow
      filaAgte1 = DSetTablas.Tables("Agentes").NewRow

      'Agregamos los valores a los campos de la tabla

      filaAgte1("slpname") = "GENERAL"
      filaAgte1("slpcode") = 998
      filaAgte1("GroupCode") = 998

      filaAgte("slpname") = "Seleccione Agente"
      filaAgte("slpcode") = 999
      filaAgte("GroupCode") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet

      DSetTablas.Tables("Agentes").Rows.Add(filaAgte1)
      DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

      Dvagentes.Table = DSetTablas.Tables("Agentes")

      Me.CmbAgteVta.ValueMember = "slpcode"
      Me.CmbAgteVta.DisplayMember = "slpname"
      Me.CmbAgteVta.DataSource = Dvagentes
      Me.CmbAgteVta.SelectedValue = 999
    End Using
  End Sub

  Sub LLenaMes()
    'elementos del combobox
    CBMES.Items.Add("Enero")
    CBMES.Items.Add("Febrero")
    CBMES.Items.Add("Marzo")
    CBMES.Items.Add("Abril")
    CBMES.Items.Add("Mayo")
    CBMES.Items.Add("Junio")
    CBMES.Items.Add("Julio")
    CBMES.Items.Add("Agosto")
    CBMES.Items.Add("Septiembre")
    CBMES.Items.Add("Octubre")
    CBMES.Items.Add("Noviembre")
    CBMES.Items.Add("Diciembre")
    'ComboBoxMes.Items.Add("Seleccione mes")

    CBMES.SelectedIndex = Month(Now) - 1

    CBAño.Text = Year(Now)

  End Sub

  Private Sub Form1_Load(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles MyBase.Load

    Actualizar()
    LlenaCmbAgte()
    LLenaMes()
    Llenar_Grids()

    ' propiedades del datagrid  
    ' '''''''''''''''''''''''''''''''''''''  
    With DGObjetivosDetalles
      ' alternar color de filas  
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .DefaultCellStyle.BackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .ReadOnly = True
      .RowHeadersVisible = True
      .RowHeadersWidth = 15

      'centrar encabezados del datagrid 
      DGObjetivosDetalles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try
        .Columns(0).HeaderText = "Asesor"
        .Columns(0).Width = 150
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        .Columns(1).HeaderText = "Sucursal"
        .Columns(1).Width = 90
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        .Columns(2).HeaderText = "Mes"
        .Columns(2).Width = 40
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(3).HeaderText = "Año"
        .Columns(3).Width = 80
        '.Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(4).HeaderText = "Cliente"
        .Columns(4).Width = 300
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(5).HeaderText = "Objetivo"
        .Columns(5).Width = 100
        .Columns(5).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      Catch ex As Exception

      End Try

    End With

    With DGObjIndAsesor
      ' alternar color de filas  
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .DefaultCellStyle.BackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .ReadOnly = False
      .RowHeadersVisible = True
      .RowHeadersWidth = 15

      'centrar encabezados del datagrid 
      DGObjetivosDetalles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try
        .Columns(0).HeaderText = "Cve Asesor"
        .Columns(0).Width = 50
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        .Columns(0).ReadOnly = True

        .Columns(1).HeaderText = "Asesor"
        .Columns(1).Width = 160
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        .Columns(1).ReadOnly = True

        .Columns(2).HeaderText = "Mes"
        .Columns(2).Width = 40
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(2).ReadOnly = True

        .Columns(3).HeaderText = "Año"
        .Columns(3).Width = 80
        '.Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(3).ReadOnly = True

        .Columns(4).HeaderText = "Cant. de clientes objetivo"
        .Columns(4).Width = 100
        .Columns(4).DefaultCellStyle.Format = "###,##0"
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(4).ReadOnly = False

        .Columns(5).HeaderText = "Valor Original"
        .Columns(5).Width = 100
        .Columns(4).DefaultCellStyle.Format = "###,##0"
        .Columns(5).ReadOnly = True
        .Columns(5).Visible = False
      Catch ex As Exception

      End Try

    End With

    With DGObjGen
      ' alternar color de filas  
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .DefaultCellStyle.BackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .ReadOnly = True
      .RowHeadersVisible = True
      .RowHeadersWidth = 15

      'centrar encabezados del datagrid 
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try
        'Catch ex As Exception
        'End Try
        .Columns(0).HeaderText = "Mes"
        .Columns(0).Width = 80
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(0).ReadOnly = True

        .Columns(1).HeaderText = "Año"
        .Columns(1).Width = 80
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(1).ReadOnly = True

        .Columns(2).HeaderText = "Objetivo anual ($)"
        .Columns(2).Width = 100
        .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(2).ReadOnly = True
      Catch ex As Exception
      End Try
    End With
  End Sub

  Private Sub cargar_registros(ByVal sql As String, ByVal dv As DataGridView)

    Try
      ' Inicializar el SqlDataAdapter indicandole el comando y el connection string  
      SqlDataAdapter = New SqlDataAdapter(sql, cs)

      Dim SqlCommandBuilder As New SqlCommandBuilder(SqlDataAdapter)

      ' llenar el DataTable  
      Dim dt As New DataTable()
      SqlDataAdapter.Fill(dt)

      ' Enlazar el BindingSource con el datatable anterior  
      ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''  
      bs.DataSource = dt

      With dv
        .Refresh()
        ' coloca el registro arriba de todo  
        '.FirstDisplayedCell = Me.DGObjetivos.CurrentCell

      End With

      bEdit = False

    Catch exSql As SqlException
      MsgBox(exSql.Message.ToString)
    Catch ex As Exception
      'MsgBox(ex.Message.ToString)
      'MsgBox("Actualmente no existen registros en la base de datos")
      'MessageBox.Show("Nn hay Informacion por mostrar", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Try
  End Sub

  ' botón para guardar los cambios y llenar la grilla  
  Private Sub Button1_Click(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs)

    Actualizar()

  End Sub


  ' Eliminar el elemento actual del BindingSource y actualizar  
  Private Sub btn_delete_Click(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs)

    If Not bs.Current Is Nothing Then
      ' eliminar  
      bs.RemoveCurrent()

      'Guardar los cambios y recargar  
      Actualizar()
    Else
      MsgBox("No hay un registro actual para eliminar",
                    MsgBoxStyle.Exclamation,
                    "Eliminar")
    End If


  End Sub

  Private Sub Actualizar(Optional ByVal bCargar As Boolean = True)
    ' Actualizar y guardar cambios  

    'If Not bs.DataSource Is Nothing Then
    '    SqlDataAdapter.Update(CType(bs.DataSource, DataTable))
    '    If bCargar Then
    '        cargar_registros("Select SLPNAME AS Vendedor, MES AS Mes, anio AS Año, sum(objetivo) as Objetivo " +
    '                 "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO order by anio DESC, mes", DGObjetivos)
    '    End If
    'End If
  End Sub


  Sub Llenar_Grids()

    Dim Mes As Integer
    If CBMES.SelectedItem = "Enero" Then
      Mes = 1
    ElseIf CBMES.SelectedItem = "Febrero" Then
      Mes = 2
    ElseIf CBMES.SelectedItem = "Marzo" Then
      Mes = 3
    ElseIf CBMES.SelectedItem = "Abril" Then
      Mes = 4
    ElseIf CBMES.SelectedItem = "Mayo" Then
      Mes = 5
    ElseIf CBMES.SelectedItem = "Junio" Then
      Mes = 6
    ElseIf CBMES.SelectedItem = "Julio" Then
      Mes = 7
    ElseIf CBMES.SelectedItem = "Agosto" Then
      Mes = 8
    ElseIf CBMES.SelectedItem = "Septiembre" Then
      Mes = 9
    ElseIf CBMES.SelectedItem = "Octubre" Then
      Mes = 10
    ElseIf CBMES.SelectedItem = "Noviembre" Then
      Mes = 11
    ElseIf CBMES.SelectedItem = "Diciembre" Then
      Mes = 12
    ElseIf CBMES.SelectedItem = "TODOS" Then
      Mes = 99
    End If

    'Dim vDiasMes As Integer
    Dim cnn As SqlConnection = Nothing
    Dim cmd As SqlCommand = Nothing

    Dim cmd2 As SqlCommand = Nothing
    'Dim vDiasTrans As Integer

    Dim cmd3 As SqlCommand = Nothing
    Dim cmd4 As SqlCommand = Nothing

    Try
      cnn = New SqlConnection(StrTpm)
      cnn.Open()

      cmd4 = New SqlCommand("SC_MuestraObjetivosClientes", cnn)
      cmd4.CommandType = CommandType.StoredProcedure
      cmd4.Parameters.AddWithValue("@anio", CBAño.Text)
      cmd4.Parameters.AddWithValue("@mes", Mes)

      cmd4.ExecuteNonQuery()
      cmd4.Connection.Close()
      Dim da As New SqlDataAdapter
      da.SelectCommand = cmd4
      da.SelectCommand.Connection = cnn

      ''--------------------------------------------
      Dim DsVtas As New DataSet
      da.Fill(DsVtas, "DsVtas")

      DsVtas.Tables(0).TableName = "Agentes"
      DsVtas.Tables(1).TableName = "ObjIndividuales"
      DsVtas.Tables(2).TableName = "Totales"

      DvagentesObj.Table = DsVtas.Tables("Agentes")
      DvObjIndAsesor.Table = DsVtas.Tables("ObjIndividuales")
      Dvtotales.Table = DsVtas.Tables("Totales")

      DGObjetivosDetalles.DataSource = DvagentesObj
      DGObjIndAsesor.DataSource = DvObjIndAsesor
      DGObjGen.DataSource = Dvtotales

    Catch ex As Exception
      MsgBox(ex.Message)
      'MsgBox("No existen ventas de este día")
    Finally
      If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
        cnn.Close()
      End If
    End Try
  End Sub


  Private Sub btn_first_Click(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs)
  End Sub

  Private Sub DataGridView1_CellEndEdit(
        ByVal sender As Object,
        ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    bEdit = True
  End Sub

  Private Sub GenerarObjetivos()
    Dim Mes As Integer
    If CBMES.SelectedItem = "Enero" Then
      Mes = 1
    ElseIf CBMES.SelectedItem = "Febrero" Then
      Mes = 2
    ElseIf CBMES.SelectedItem = "Marzo" Then
      Mes = 3
    ElseIf CBMES.SelectedItem = "Abril" Then
      Mes = 4
    ElseIf CBMES.SelectedItem = "Mayo" Then
      Mes = 5
    ElseIf CBMES.SelectedItem = "Junio" Then
      Mes = 6
    ElseIf CBMES.SelectedItem = "Julio" Then
      Mes = 7
    ElseIf CBMES.SelectedItem = "Agosto" Then
      Mes = 8
    ElseIf CBMES.SelectedItem = "Septiembre" Then
      Mes = 9
    ElseIf CBMES.SelectedItem = "Octubre" Then
      Mes = 10
    ElseIf CBMES.SelectedItem = "Noviembre" Then
      Mes = 11
    ElseIf CBMES.SelectedItem = "Diciembre" Then
      Mes = 12
    ElseIf CBMES.SelectedItem = "TODOS" Then
      Mes = 99
    End If

    If Mes <> 99 And CmbAgteVta.SelectedValue <> 999 Then
      '----PROCEDIMIENTO para ingresar objetivo de manera individual
      Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)

      '---------Procedimiento para ingresar objetivo general
      Dim command As New SqlCommand("SPLineasObjetivo_IngresarObjetivos", conexion)
      Try
        command.CommandType = CommandType.StoredProcedure
        command.Parameters.Add("@slpcode", SqlDbType.Int).Value = Me.CmbAgteVta.SelectedValue
        command.Parameters.Add("@slpname", SqlDbType.NVarChar, 100).Value = Me.CmbAgteVta.GetItemText(CmbAgteVta.SelectedItem)
        command.Parameters.AddWithValue("@mes", Mes)
        command.Parameters.AddWithValue("@anio", CBAño.Text)

        conexion.Open()
        command.ExecuteNonQuery()
        cargar_registros("Select SLPNAME AS Vendedor, MES AS Mes, anio AS Año, desc_linea AS 'Línea objetivo', objetivo as Objetivo " +
                        "FROM LOObjInd ORDER BY anio DESC, mes", DGObjetivosDetalles)
      Catch ex As Exception
        'MessageBox.Show(ex.Message)
        MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

      Finally
        conexion.Dispose()
        command.Dispose()
      End Try
    End If
  End Sub


  Private Sub Button4_Click(sender As Object, e As EventArgs)
    Actualizar()
  End Sub


  Private Sub Button4_Click_1(sender As Object, e As EventArgs)
    Actualizar()
  End Sub

  'CONSULTAR
  Private Sub MetodoConsulta()
    Dim Mes As Integer
    If CBMES.SelectedItem = "Enero" Then
      Mes = 1
    ElseIf CBMES.SelectedItem = "Febrero" Then
      Mes = 2
    ElseIf CBMES.SelectedItem = "Marzo" Then
      Mes = 3
    ElseIf CBMES.SelectedItem = "Abril" Then
      Mes = 4
    ElseIf CBMES.SelectedItem = "Mayo" Then
      Mes = 5
    ElseIf CBMES.SelectedItem = "Junio" Then
      Mes = 6
    ElseIf CBMES.SelectedItem = "Julio" Then
      Mes = 7
    ElseIf CBMES.SelectedItem = "Agosto" Then
      Mes = 8
    ElseIf CBMES.SelectedItem = "Septiembre" Then
      Mes = 9
    ElseIf CBMES.SelectedItem = "Octubre" Then
      Mes = 10
    ElseIf CBMES.SelectedItem = "Noviembre" Then
      Mes = 11
    ElseIf CBMES.SelectedItem = "Diciembre" Then
      Mes = 12
    ElseIf CBMES.SelectedItem = "TODOS" Then
      Mes = 99
    End If

    Try

      If CmbAgteVta.Text = "GENERAL" Then
        Dim cmd As SqlCommand = New SqlCommand("SELECT objetivo FROM LHObjGen where mes ='" & Mes & "' and anio='" & CBAño.Text & "'", conexion)

        'cmd.Parameters.AddWithValue("@desc", CStr(cbEquipo.SelectedValue))

        Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        If dt.Rows.Count > 0 Then

          BAct.Visible = True
          'BSave.Visible = False

          Dim row As DataRow = dt.Rows(0)
        End If

        'Else
        '    MsgBox("No hay registros para este agente")
        '    TBCARGO.Text = "0.00"
        '    TBDP.Text = "0.00"
        '    TBKING.Text = "0.00"
        '    TBPOWERPRO.Text = "0.00"
        '    TBRODWELL.Text = "0.00"
        '    TBRODWELLC.Text = "0.00"
        '    TBUJOINTK.Text = "0.00"
        '    TBRODWELLB.Text = "0.00"
        '    TBROCKWELLB.Text = "0.00"

        '    BAct.Visible = False

      ElseIf CmbAgteVta.Text <> "Seleccione agente" Then

        Dim cmd2 As SqlCommand = New SqlCommand("SELECT objetivo FROM LHObjInd where slpcode= '" & CmbAgteVta.SelectedValue & "' and mes ='" & Mes & "' and anio='" & CBAño.Text & "'", conexion)

        'cmd.Parameters.AddWithValue("@desc", CStr(cbEquipo.SelectedValue))

        Dim da2 As SqlDataAdapter = New SqlDataAdapter(cmd2)
        Dim dt2 As New DataTable
        da2.Fill(dt2)

        If dt2.Rows.Count > 0 Then

          BAct.Visible = True
          'BSave.Visible = False

          Dim row As DataRow = dt2.Rows(0)
          Dim row1 As DataRow = dt2.Rows(1)
          Dim row2 As DataRow = dt2.Rows(2)
          Dim row3 As DataRow = dt2.Rows(3)
          Dim row4 As DataRow = dt2.Rows(4)
          Dim row5 As DataRow = dt2.Rows(5)
          Dim row6 As DataRow = dt2.Rows(6)
          Dim row7 As DataRow = dt2.Rows(7)
          Dim row8 As DataRow = dt2.Rows(8)
        Else
          MsgBox("No hay registros de este agente")

        End If
      Else
        MsgBox("No hay Objetivo de este periodo")
      End If



    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub


  Private Sub Button6_Click_1(sender As Object, e As EventArgs)
    MetodoConsulta()
  End Sub

  'BOTON ELIMINAR
  Sub EliminarRegistroAgente()
    Dim i As Integer
    Dim agente As String
    Dim anio As String
    Dim mes As Integer
    Dim linea As String

    Dim objetivo As Decimal
    Try
      i = DGObjetivosDetalles.CurrentRow.Index
      agente = DGObjetivosDetalles.Item(0, i).Value.ToString
      anio = DGObjetivosDetalles.Item(3, i).Value.ToString
      mes = DGObjetivosDetalles.Item(2, i).Value.ToString
      linea = DGObjetivosDetalles.Item(4, i).Value.ToString
      objetivo = DGObjetivosDetalles.Item(5, i).Value.ToString

      If (MessageBox.Show("¿Confirma que desea eliminar este registro?" + Chr(13) + Chr(13) & agente & "   " & mes &
                              "   " & anio & "   $" & objetivo,
                              "Eliminar",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

        Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
        Dim command As New SqlCommand("LODelete", conexion)

        Try
          command.CommandType = CommandType.StoredProcedure
          'command.CommandType = CommandType.StoredProcedure
          'command.Parameters.Add("@slpcode", SqlDbType.Int).Value = Me.CmbAgteVta.SelectedValue
          command.Parameters.Add("@slpname", SqlDbType.NVarChar, 100).Value = agente
          command.Parameters.AddWithValue("@mes", mes)
          command.Parameters.AddWithValue("@anio", anio)
          command.Parameters.Add("@linea", SqlDbType.NVarChar, 50).Value = linea

          conexion.Open()
          command.ExecuteScalar()
          cargar_registros("SELECT slpname AS 'Vendedor', mes AS 'Mes', anio AS 'Año', desc_linea AS 'Línea objetivo', Objetivo AS 'Objetivo'  " +
                        "FROM LOObjInd", DGObjetivosDetalles)

          Llenar_Grids()
        Catch ex As Exception
          MessageBox.Show(ex.Message)
          'MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Finally
          conexion.Dispose()
          command.Dispose()
        End Try

      End If

    Catch ex As Exception
      MsgBox("No hay un registro actual para eliminar",
                            MsgBoxStyle.Exclamation,
                            "Eliminar")
    End Try
  End Sub

  Sub EliminarRegistroGeneral()
    Dim i As Integer
    'Dim agente As String
    Dim anio As String
    Dim mes As Integer

    Dim objetivo As Decimal
    Try


      i = DGObjGen.CurrentRow.Index
      anio = DGObjGen.Item(1, i).Value.ToString
      mes = DGObjGen.Item(0, i).Value.ToString
      objetivo = DGObjGen.Item(2, i).Value.ToString

      If (MessageBox.Show("¿Confirma que desea eliminar este registro?" + Chr(13) + Chr(13) & mes &
                                "   " & anio & "   $" & objetivo,
                                "Eliminar",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

        Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
        Dim command As New SqlCommand("LODeleteGen", conexion)

        Try
          command.CommandType = CommandType.StoredProcedure
          command.Parameters.AddWithValue("@mes", mes)
          command.Parameters.AddWithValue("@anio", anio)

          conexion.Open()
          command.ExecuteScalar()

          Llenar_Grids()
        Catch ex As Exception
          MessageBox.Show(ex.Message)
          'MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Finally
          conexion.Dispose()
          command.Dispose()
        End Try

      End If

    Catch ex As Exception
      MsgBox("No hay un registro actual para eliminar",
                        MsgBoxStyle.Exclamation,
                        "Eliminar")
    End Try
  End Sub

  'ACTUALIZAR
  Private Sub BAct_Click(sender As Object, e As EventArgs) Handles BAct.Click

    'Dim anio As String
    'Dim mes As Integer

    Dim Mes As Integer
    If CBMES.SelectedItem = "Enero" Then
      Mes = 1
    ElseIf CBMES.SelectedItem = "Febrero" Then
      Mes = 2
    ElseIf CBMES.SelectedItem = "Marzo" Then
      Mes = 3
    ElseIf CBMES.SelectedItem = "Abril" Then
      Mes = 4
    ElseIf CBMES.SelectedItem = "Mayo" Then
      Mes = 5
    ElseIf CBMES.SelectedItem = "Junio" Then
      Mes = 6
    ElseIf CBMES.SelectedItem = "Julio" Then
      Mes = 7
    ElseIf CBMES.SelectedItem = "Agosto" Then
      Mes = 8
    ElseIf CBMES.SelectedItem = "Septiembre" Then
      Mes = 9
    ElseIf CBMES.SelectedItem = "Octubre" Then
      Mes = 10
    ElseIf CBMES.SelectedItem = "Noviembre" Then
      Mes = 11
    ElseIf CBMES.SelectedItem = "Diciembre" Then
      Mes = 12
    ElseIf CBMES.SelectedItem = "TODOS" Then
      Mes = 99
    End If

    Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
    command2 = New SqlCommand("LHUpdate", conexion)
    Try

      command2.CommandType = CommandType.StoredProcedure


      command2.Parameters.AddWithValue("@slpname", CmbAgteVta.SelectedValue)
      command2.Parameters.AddWithValue("@mes", Mes)
      command2.Parameters.AddWithValue("@anio", CBAño.Text)

      'If TbObjGen.Text <> "" Then
      'command2.Parameters.AddWithValue("@objgen", txtObjetivo.Text)
      'End If

      conexion.Open()
      command2.ExecuteNonQuery()
      cargar_registros("SELECT slpname AS 'Vendedor', mes AS 'Mes', anio AS 'Año', desc_linea AS 'Línea objetivo', Objetivo AS 'Objetivo'  " +
                      "FROM LOObjInd ", DGObjetivosDetalles)

      Llenar_Grids()

      'MsgBox("Se actualizo correctamente")

      MessageBox.Show("Objetivos actualizados correctamente", "Actualización exitosa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)

    Catch ex As Exception
      MessageBox.Show(ex.Message)
      'MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

    Finally
      conexion.Dispose()
      command2.Dispose()
    End Try
  End Sub

  Private Sub BSave_Click(sender As Object, e As EventArgs)
    If CmbAgteVta.SelectedValue = 999 Then

      MessageBox.Show(
          "Seleccione agente ", "Agente no válido", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation)

    Else

      If (MessageBox.Show(
              "¿Confirma que desea generar este registro?" + Chr(13) + Chr(13) & CmbAgteVta.Text & "   " & CBMES.Text &
                                "   " & CBAño.Text,
                            "Generar Registro", MessageBoxButtons.YesNo,
                          MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

        GenerarObjetivos()
        Llenar_Grids()
      End If
    End If


    If CmbAgteVta.Text = "GENERAL" Then



      Dim Mes As Integer
      If CBMES.SelectedItem = "Enero" Then
        Mes = 1
      ElseIf CBMES.SelectedItem = "Febrero" Then
        Mes = 2
      ElseIf CBMES.SelectedItem = "Marzo" Then
        Mes = 3
      ElseIf CBMES.SelectedItem = "Abril" Then
        Mes = 4
      ElseIf CBMES.SelectedItem = "Mayo" Then
        Mes = 5
      ElseIf CBMES.SelectedItem = "Junio" Then
        Mes = 6
      ElseIf CBMES.SelectedItem = "Julio" Then
        Mes = 7
      ElseIf CBMES.SelectedItem = "Agosto" Then
        Mes = 8
      ElseIf CBMES.SelectedItem = "Septiembre" Then
        Mes = 9
      ElseIf CBMES.SelectedItem = "Octubre" Then
        Mes = 10
      ElseIf CBMES.SelectedItem = "Noviembre" Then
        Mes = 11
      ElseIf CBMES.SelectedItem = "Diciembre" Then
        Mes = 12
      ElseIf CBMES.SelectedItem = "TODOS" Then
        Mes = 99
      End If

      Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
      Dim command As New SqlCommand("SPLHObjGen", conexion)
      Try

        command.CommandType = CommandType.StoredProcedure

        command.Parameters.AddWithValue("@mes", Mes)
        command.Parameters.AddWithValue("@anio", CBAño.Text)

        conexion.Open()
        command.ExecuteNonQuery()
        Llenar_Grids()

        MessageBox.Show("Objetivos guardados correctamente", "Operación exitosa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
      Catch ex As Exception
        MessageBox.Show("Ocurrio un error inesperado" & ex.Message)
        'MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

      Finally
        conexion.Dispose()
        command.Dispose()
      End Try
    End If
  End Sub

  Private Sub DGObjGen_CurrentCellChanged(sender As Object, e As EventArgs) Handles DGObjGen.CurrentCellChanged
    Try
      DvagentesObj.RowFilter = "Mes = " & DGObjGen.Item(0, DGObjGen.CurrentRow.Index).Value.ToString & " AND Año = " & DGObjGen.Item(1, DGObjGen.CurrentRow.Index).Value.ToString
    Catch ex As Exception

    End Try

    'Llenar_Grids()
  End Sub

  Sub ImportarScoreCard()
    Dim SQL As New Comandos_SQL()
    Try
      SQL.conectarTPM()
      Dim filePath As String

      If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
        filePath = OpenFileDialog1.FileName
      End If
      'If filePath <> IsDBNull("") Then
      Using workBook = New XLWorkbook(filePath)
        'Read the first Sheet from Excel file.
        Dim workSheet As IXLWorksheet = workBook.Worksheet(1)
        Dim CardCode As String = "0"
        Dim CardName As String = "0"
        Dim Objetivo As Decimal = -1
        Dim Periodo As Integer = 0
        Dim Año As Integer = 0

        'Create a new DataTable.
        'Dim dt As New DataTable()

        'Loop through the Worksheet rows.
        Dim firstRow As Boolean = True
        For Each row As IXLRow In workSheet.Rows()
          'Use the first row to add columns to DataTable.
          If firstRow Then
            For Each cell As IXLCell In row.Cells()
              'dt.Columns.Add(cell.Value.ToString())
            Next
            firstRow = False
          Else
            'Add rows to DataTable.
            'dt.Rows.Add()
            Dim i As Integer = 0
            For Each cell As IXLCell In row.Cells()
              'dt.Rows(dt.Rows.Count - 1)(i) = cell.Value.ToString()
              If CardCode = "0" Then
                CardCode = cell.Value.ToString()
              ElseIf CardName = "0" Then
                CardName = cell.Value.ToString()
              ElseIf Objetivo = -1 Then
                Objetivo = Decimal.Parse(cell.Value.ToString())
              ElseIf Periodo = 0 Then
                Periodo = Integer.Parse(cell.Value.ToString())
              Else
                Año = Integer.Parse(cell.Value.ToString())
              End If

              i += 1
            Next

            If CardCode <> "" Then
              If SQL.SiExiste("select * from ScoreCard_Clientes where CardCode = '" + CardCode + "' and CardName = '" + CardName + "' and Objetivo = " + Objetivo.ToString() + " and Periodo = " + Periodo.ToString()) Then
                'If MessageBox.Show("Ya existen registros con los mismos datos, ¿Deseas actualizar los datos?", "Pregunta...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                'End If
                MessageBox.Show("¡Ya existen registros con los mismos datos!", "No es posible agregar los datos", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
              Else
                If SQL.EjecutarComando("insert into ScoreCard_Clientes (CardCode,CardName,Objetivo,Periodo,año)values('" + CardCode + "','" + CardName + "'," + Objetivo.ToString() + "," + Periodo.ToString() + "," + Año.ToString() + ")") Then
                  CardCode = "0"
                  CardName = "0"
                  Objetivo = -1
                  Periodo = 0
                  Año = 0
                Else
                  MessageBox.Show("¡Error al agregar los objetivos de los clientes!", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
              End If

            End If

          End If

          'dgv.DataSource = dt
          'dgv.DataBindings()
        Next
      End Using
      SQL.Cerrar()
      MessageBox.Show("¡Se Guardaron/Actualizaron correctamente!", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
      'End If

    Catch ex As Exception
      MessageBox.Show("Error al cargar excel: " + Environment.NewLine + ex.ToString())
    End Try
  End Sub

  Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
    ImportarScoreCard()
  End Sub

  Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
    For Each Fila As DataGridViewRow In DGObjIndAsesor.Rows
      If Not Fila Is Nothing Then
        '//O puedes hacer una validación con el número de la columna
        If Fila.Cells(4).Value <> Fila.Cells(5).Value Then
          'Realizo la actualizacion o el insert
          Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
          Dim command As New SqlCommand("SC_UpdateObjetivoCliente", conexion)
          Try
            command.CommandType = CommandType.StoredProcedure
            command.Parameters.AddWithValue("@Asesor", Fila.Cells(0).Value)
            command.Parameters.AddWithValue("@mes", Fila.Cells(2).Value)
            command.Parameters.AddWithValue("@anio", Fila.Cells(3).Value)
            command.Parameters.AddWithValue("@objetivo", Fila.Cells(4).Value)

            If conexion.State <> ConnectionState.Open Then
              conexion.Open()
            End If
            command.ExecuteNonQuery()
          Catch ex As Exception
            MessageBox.Show("Ocurrio un error inesperado" & ex.Message)
            'MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
          Finally
            conexion.Dispose()
            command.Dispose()
          End Try
        End If
      End If
    Next
    MessageBox.Show("Los objetivos fueron actualizados")
    Llenar_Grids()
  End Sub

  Private Sub CBMES_SelectedValueChanged(sender As Object, e As EventArgs) Handles CBMES.SelectedValueChanged
    Llenar_Grids()
  End Sub
End Class