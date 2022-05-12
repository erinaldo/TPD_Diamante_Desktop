
Option Explicit On
'Option Strict On
Imports System.Data.SqlClient

Public Class LHIngObj

  ' Espacios de nombres  
  ' ''''''''''''''''''''''''''''''''''''''''' 

  Public conexion As New SqlConnection(conexion_universal.CadenaSQL)

  Public command2 As New SqlCommand("LHUpdate", conexion)
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

      Me.CmbAgteVta.DataSource = Dvagentes
      Me.CmbAgteVta.DisplayMember = "slpname"
      Me.CmbAgteVta.ValueMember = "slpcode"
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

    'TBCARGO.Text = Format(0, "#,##0.00")
    'TBCARGO.TextAlign = HorizontalAlignment.Right

    'TBDP.Text = Format(0, "#,##0.00")
    'TBDP.TextAlign = HorizontalAlignment.Right

    'TBKING.Text = Format(0, "#,##0.00")
    'TBKING.TextAlign = HorizontalAlignment.Right

    'TBPOWERPRO.Text = Format(0, "#,##0.00")
    'TBPOWERPRO.TextAlign = HorizontalAlignment.Right

    'TBRODWELL.Text = Format(0, "#,##0.00")
    'TBRODWELL.TextAlign = HorizontalAlignment.Right

    'TBRODWELLC.Text = Format(0, "#,##0.00")
    'TBRODWELLC.TextAlign = HorizontalAlignment.Right

    'TBUJOINTK.Text = Format(0, "#,##0.00")
    'TBUJOINTK.TextAlign = HorizontalAlignment.Right

    'TBRODWELLB.Text = Format(0, "#,##0.00")
    'TBRODWELLB.TextAlign = HorizontalAlignment.Right

    'TBROCKWELLB.Text = Format(0, "#,##0.00")
    'TBROCKWELLB.TextAlign = HorizontalAlignment.Right

    'TBRODWELLB.Text = Format(0, "#,##0.00")
    'TBRODWELLB.TextAlign = HorizontalAlignment.Right

    txtObjetivo.Text = Format(0, "#,##0.00")
    txtObjetivo.TextAlign = HorizontalAlignment.Right

    CBAño.Text = Year(Now)

  End Sub


  Private Sub Form1_Load(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles MyBase.Load

    Actualizar()

    LlenaCmbAgte()

    LLenaMes()

    Buscar_NotasC()


    ' propiedades del datagrid  
    ' '''''''''''''''''''''''''''''''''''''  
    With DGObjetivos
      ' alternar color de filas  
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .DefaultCellStyle.BackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      '.RowsDefaultCellStyle = 
      ' Establecer el origen de datos para el DataGridview  
      'DGObjetivos.DataSource = bs
      .ReadOnly = True
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      .RowHeadersVisible = True
      .RowHeadersWidth = 15
      '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
      'Color de linea del grid

      'centrar encabezados del datagrid 
      DGObjetivos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try

        'Catch ex As Exception

        'End Try


        .Columns(0).HeaderText = "Vendedor"
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

        .Columns(4).HeaderText = "Objetivo"
        .Columns(4).Width = 100
        .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


      Catch ex As Exception

      End Try

    End With



    With DGObjGen
      ' alternar color de filas  
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .DefaultCellStyle.BackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      '.RowsDefaultCellStyle = 
      ' Establecer el origen de datos para el DataGridview  
      'DGObjetivos.DataSource = bs
      .ReadOnly = True
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      .RowHeadersVisible = True
      .RowHeadersWidth = 15
      '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
      'Color de linea del grid

      'centrar encabezados del datagrid 
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try

        'Catch ex As Exception

        'End Try


        .Columns(0).HeaderText = "Mes"
        .Columns(0).Width = 40
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        .Columns(1).HeaderText = "Año"
        .Columns(1).Width = 80
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(2).HeaderText = "Objetivo"
        .Columns(2).Width = 90
        .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


      Catch ex As Exception

      End Try

    End With



    ''btn_first.Text = "<<"
    ''btn_Previous.Text = "<"
    ''btn_next.Text = ">"
    ''btn_last.Text = ">>"



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


  Sub Buscar_NotasC()

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


      cmd4 = New SqlCommand("LHMuestraObjetivos", cnn)
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
      DsVtas.Tables(1).TableName = "Totales"
      'DsVtas.Tables(2).TableName = "VtaCltes"

      DvagentesObj.Table = DsVtas.Tables("Agentes")
      Dvtotales.Table = DsVtas.Tables("Totales")
      ''DvClientes.Table = DsVtas.Tables("VtaCltes")


      DGObjetivos.DataSource = DvagentesObj

      DGObjGen.DataSource = Dvtotales

    Catch ex As Exception
      MsgBox(ex.Message)
      'MsgBox("No existen ventas de este día")
    Finally
      If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
        cnn.Close()
      End If
    End Try

    'txtDiasRestantes.Text = Convert.ToString(vDiasMes - vDiasTrans)
    'txtAvanceOptimo.Text = Format(Convert.ToString((vDiasTrans / vDiasMes) * 100), "000.00")

    'txtAvanceOptimo.Text = (vDiasTrans / vDiasMes).ToString("P1")

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
      'Dim command As New SqlCommand("SPLHObjInd", conexion)
      Dim command As New SqlCommand("SPLineasHalcon_IngresarObjetivos", conexion)
      Try
        command.CommandType = CommandType.StoredProcedure
        command.Parameters.Add("@slpcode", SqlDbType.Int).Value = Me.CmbAgteVta.SelectedValue
        command.Parameters.Add("@slpname", SqlDbType.NVarChar, 100).Value = Me.CmbAgteVta.Text
        command.Parameters.AddWithValue("@mes", Mes)
        command.Parameters.AddWithValue("@anio", CBAño.Text)
        command.Parameters.Add("@objetivo", SqlDbType.Decimal).Value = Convert.ToDecimal(txtObjetivo.Text)
        conexion.Open()
        command.ExecuteNonQuery()
        cargar_registros("Select SLPNAME AS Vendedor, MES AS Mes, anio AS Año,objetivo as Objetivo " +
                          "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO,Objetivo order by anio DESC, mes", DGObjetivos)
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

          txtObjetivo.Text = CStr(row("OBJETIVO"))
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


          txtObjetivo.Text = CStr(row("OBJETIVO"))


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




  'BOTON CREAR REGISTRO 
  Private Sub Button5_Click(sender As Object, e As EventArgs) Handles BAgregar.Click

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
        Buscar_NotasC()
      End If
    End If
  End Sub


  'BOTON ELIMINAR

  Sub EliminarRegistroAgente()
    Dim i As Integer
    Dim agente As String
    Dim anio As String
    Dim mes As Integer

    Dim objetivo As Decimal
    Try
      i = DGObjetivos.CurrentRow.Index
      agente = DGObjetivos.Item(0, i).Value.ToString
      anio = DGObjetivos.Item(3, i).Value.ToString
      mes = DGObjetivos.Item(2, i).Value.ToString
      objetivo = DGObjetivos.Item(4, i).Value.ToString

      If (MessageBox.Show("¿Confirma que desea eliminar este registro?" + Chr(13) + Chr(13) & agente & "   " & mes &
                                "   " & anio & "   $" & objetivo,
                                "Eliminar",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

        Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
        'Dim command As New SqlCommand("SPLHObjInd", conexion)
        Dim command As New SqlCommand("LHDelete", conexion)

        Try
          'command = New SqlCommand("LHDelete", conexion)
          command.CommandType = CommandType.StoredProcedure
          'command.CommandType = CommandType.StoredProcedure
          'command.Parameters.Add("@slpcode", SqlDbType.Int).Value = Me.CmbAgteVta.SelectedValue
          command.Parameters.Add("@slpname", SqlDbType.NVarChar, 100).Value = agente
          command.Parameters.AddWithValue("@mes", mes)
          command.Parameters.AddWithValue("@anio", anio)

          conexion.Open()
          command.ExecuteScalar()
          cargar_registros("SELECT slpname AS 'Vendedor', mes AS 'Mes', anio AS 'Año', SUM(Objetivo) AS 'Objetivo'  " +
                              "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO ", DGObjetivos)

          Buscar_NotasC()
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
        'Dim command As New SqlCommand("SPLHObjInd", conexion)
        Dim command As New SqlCommand("LHDeleteGen", conexion)

        Try
          'command = New SqlCommand("LHDelete", conexion)
          command.CommandType = CommandType.StoredProcedure
          'command.CommandType = CommandType.StoredProcedure
          'command.Parameters.Add("@slpcode", SqlDbType.Int).Value = Me.CmbAgteVta.SelectedValue
          'command.Parameters.Add("@slpname", SqlDbType.NVarChar, 100).Value = agente
          command.Parameters.AddWithValue("@mes", mes)
          command.Parameters.AddWithValue("@anio", anio)

          conexion.Open()
          command.ExecuteScalar()
          'cargar_registros("SELECT slpname AS 'Vendedor', mes AS 'Mes', anio AS 'Año', SUM(Objetivo) AS 'Objetivo'  " +
          '         "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO ", DGObjetivos)

          Buscar_NotasC()
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


  Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
    EliminarRegistroAgente()
  End Sub

  Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click


    If (MessageBox.Show("¿Confirma que desea eliminar TODO?", "Eliminar", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

      Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)

      Dim command As New SqlCommand("LHTruncate", conexion)

      Try
        'command = New SqlCommand("LHDelete", conexion)
        command.CommandType = CommandType.StoredProcedure

        conexion.Open()
        command.ExecuteScalar()
        cargar_registros("SELECT slpname AS 'Vendedor', mes AS 'Mes', anio AS 'Año', SUM(Objetivo) AS 'Objetivo'  " +
                              "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO ", DGObjetivos)

        Buscar_NotasC()
      Catch ex As Exception
        MessageBox.Show(ex.Message)

      Finally
        conexion.Dispose()
        command.Dispose()
      End Try
    End If


  End Sub



  Private Sub CmbAgteVta_SelectedIndexChanged(sender As Object, e As EventArgs)


    'TBCARGO.Text = "0.00"
    'TBDP.Text = "0.00"
    'TBKING.Text = "0.00"
    'TBPOWERPRO.Text = "0.00"
    'TBRODWELL.Text = "0.00"
    'TBRODWELLC.Text = "0.00"
    'TBUJOINTK.Text = "0.00"
    'TBRODWELLB.Text = "0.00"
    'TBROCKWELLB.Text = "0.00"
    txtObjetivo.Text = "0.00"

    If CmbAgteVta.Text = "GENERAL" Then
      'Label2.Visible = True
      Label3.Visible = True
      'BSave.Visible = True
      TbObjGen.Visible = True

      Button6.Visible = True
      'End If

    ElseIf CmbAgteVta.Text <> "Seleccione Agente" Then
      Button6.Visible = True


      'Label2.Visible = False
      Label3.Visible = True
      'BSave.Visible = True
      TbObjGen.Visible = True


    Else
      ' MetodoConsulta()

      Button6.Visible = False
      'Label2.Visible = False
      Label3.Visible = False
      'BSave.Visible = False
      TbObjGen.Visible = True

    End If

    'Try

    '    Dim Mes As Integer
    '    If CBMES.SelectedItem = "Enero" Then
    '        Mes = 1
    '    ElseIf CBMES.SelectedItem = "Febrero" Then
    '        Mes = 2
    '    ElseIf CBMES.SelectedItem = "Marzo" Then
    '        Mes = 3
    '    ElseIf CBMES.SelectedItem = "Abril" Then
    '        Mes = 4
    '    ElseIf CBMES.SelectedItem = "Mayo" Then
    '        Mes = 5
    '    ElseIf CBMES.SelectedItem = "Junio" Then
    '        Mes = 6
    '    ElseIf CBMES.SelectedItem = "Julio" Then
    '        Mes = 7
    '    ElseIf CBMES.SelectedItem = "Agosto" Then
    '        Mes = 8
    '    ElseIf CBMES.SelectedItem = "Septiembre" Then
    '        Mes = 9
    '    ElseIf CBMES.SelectedItem = "Octubre" Then
    '        Mes = 10
    '    ElseIf CBMES.SelectedItem = "Noviembre" Then
    '        Mes = 11
    '    ElseIf CBMES.SelectedItem = "Diciembre" Then
    '        Mes = 12
    '    ElseIf CBMES.SelectedItem = "TODOS" Then
    '        Mes = 99
    '    End If

    '    Dim cmd As SqlCommand = New SqlCommand("SELECT objetivo FROM LHObjInd where slpcode= '" & CmbAgteVta.SelectedValue & "' and mes ='" & Mes & "' and anio='" & CBAño.Text & "'", conexion)

    '    cmd.Parameters.AddWithValue("@desc", CStr(cbEquipo.SelectedValue))

    '    Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
    '    Dim dt As New DataTable
    '    da.Fill(dt)

    '    If dt.Rows.Count > 0 Then

    '        BAct.Visible = False
    '        BAgregar.Visible = False

    '    Else
    '        MsgBox("No hay registros para este agente")
    '        TBCARGO.Text = "0.00"
    '        TBDP.Text = "0.00"
    '        TBKING.Text = "0.00"
    '        TBPOWERPRO.Text = "0.00"
    '        TBRODWELL.Text = "0.00"
    '        TBRODWELLC.Text = "0.00"
    '        TBUJOINTK.Text = "0.00"
    '        TBRODWELLB.Text = "0.00"
    '        TBROCKWELLB.Text = "0.00"

    '    End If
    'Catch ex As Exception
    '    MsgBox(ex.Message)
    'End Try

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
    'Dim command As New SqlCommand("SPLHObjInd", conexion)
    command2 = New SqlCommand("LHUpdate", conexion)
    Try

      command2.CommandType = CommandType.StoredProcedure
      'command.CommandType = CommandType.StoredProcedure

      'command2.Parameters.Add("@slpname", SqlDbType.VarChar, 150).Value = CmbAgteVta.SelectedValue


      command2.Parameters.AddWithValue("@slpname", CmbAgteVta.SelectedValue)
      command2.Parameters.AddWithValue("@mes", Mes)
      command2.Parameters.AddWithValue("@anio", CBAño.Text)
      'command2.Parameters.AddWithValue("@obj0", TBCARGO.Text)
      'command2.Parameters.AddWithValue("@obj1", TBDP.Text)
      'command2.Parameters.AddWithValue("@obj2", TBKING.Text)
      'command2.Parameters.AddWithValue("@obj3", TBPOWERPRO.Text)
      'command2.Parameters.AddWithValue("@obj4", TBRODWELL.Text)
      'command2.Parameters.AddWithValue("@obj5", TBRODWELLC.Text)
      'command2.Parameters.AddWithValue("@obj6", TBUJOINTK.Text)
      'command2.Parameters.AddWithValue("@obj7", TBRODWELLB.Text)
      'command2.Parameters.AddWithValue("@obj8", TBROCKWELLB.Text)

      'If TbObjGen.Text <> "" Then
      command2.Parameters.AddWithValue("@objgen", txtObjetivo.Text)
      'End If
      conexion.Open()
      command2.ExecuteNonQuery()
      cargar_registros("SELECT slpname AS 'Vendedor', mes AS 'Mes', anio AS 'Año', SUM(Objetivo) AS 'Objetivo'  " +
                      "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO ", DGObjetivos)

      Buscar_NotasC()

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
        Buscar_NotasC()
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
        command.Parameters.AddWithValue("@Objetivo", txtObjetivo.Text)


        conexion.Open()
        command.ExecuteNonQuery()
        'cargar_registros("SELECT anio, mes, objetivo FROM LHObjGen ORDER BY anio DESC,mes ASC ", DGObjGen)
        Buscar_NotasC()

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
    'Buscar_NotasC()
  End Sub

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    EliminarRegistroGeneral()
  End Sub
End Class
