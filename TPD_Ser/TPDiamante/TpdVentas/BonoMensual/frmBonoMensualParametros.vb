Imports System.Data.SqlClient

Public Class frmBonoMensualParametros
 ' Espacios de nombres  
 ' ''''''''''''''''''''''''''''''''''''''''' 
 Dim SQL As New Comandos_SQL()

 Public conexion As New SqlConnection(conexion_universal.CadenaSQL)

 Public command2 As New SqlCommand("LOUpdate", conexion)
 Public Rs As SqlDataReader
 'Public SQL As String
 Public Com As New SqlCommand

 'Data views para grids
 Dim DvGeneral As New DataView
 Dim DvPorAgentes As New DataView

 'data view para mostrar combobox
 Dim Dvagentes As New DataView
 Dim DvLienas As New DataView

 'BindingSource  
 Private WithEvents bs As New BindingSource

 ' Adaptador de datos sql  
 Private SqlDataAdapter As SqlDataAdapter

 ' Cadena de conexión  
 Private cs As String = conexion_universal.cConstanteTPM


 ' flag  
 Private bEdit As Boolean

 'Informacion del renglon seleccionado
 Dim CveAgt As Integer
 Dim iMes As Integer
 Dim iAño As Integer

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
     ConsutaLista = "SELECT DISTINCT T0.slpcode,T0.slpname,T1.GroupCode FROM OSLP T0 "
     ConsutaLista &= "INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
     ConsutaLista &= "WHERE T1.CbrGralAdicional = 'N' AND T0.U_ESTATUS = 'ACTIVO' AND T0.SLPCODE <> 1 ORDER BY slpname "

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
   txtCantidadLinea.Text = Format(0, "#,##0.00")
   txtCantidadLinea.TextAlign = HorizontalAlignment.Right
   CBAño.Text = Year(Now)
 End Sub


 Private Sub Form1_Load(
       ByVal sender As System.Object,
       ByVal e As System.EventArgs) Handles MyBase.Load

   Actualizar()
   LlenaCmbAgte()
   LLenaMes()
   Buscar_Parametros()

   txtCantidadLinea.Text = Format(0, "#,##0.00")

   LlenaGridObjetivos()
 End Sub

 Private Sub LlenaGridObjetivos()
   ' propiedades del datagrid  
   ' '''''''''''''''''''''''''''''''''''''  
   With DGImporteBono
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
     DGImporteBono.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

     Try

       .Columns("slpcode").HeaderText = "Cve. Agente"
       .Columns("slpcode").Width = 50
   .Columns("slpcode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
   .Columns("slpcode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

   .Columns("slpname").HeaderText = "Agente"
       .Columns("slpname").Width = 120
       .Columns("slpname").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

       .Columns("mes").HeaderText = "Mes"
       .Columns("mes").Width = 40
       .Columns("mes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

       .Columns("anio").HeaderText = "Año"
       .Columns("anio").Width = 80
       .Columns("anio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

       .Columns("ImporteBonoMensual").HeaderText = "Importe Bono Mensual"
       .Columns("ImporteBonoMensual").Width = 100
       .Columns("ImporteBonoMensual").DefaultCellStyle.Format = "$ ###,###,##0.#0"
       .Columns("ImporteBonoMensual").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
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


 Sub Buscar_Parametros()

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

     cmd4 = New SqlCommand("SPBonoMensual", cnn)
     cmd4.CommandType = CommandType.StoredProcedure

     If CBAño.Text = "" Then Exit Sub

     Dim FechaActual As Date = Now()
     Dim fCha As String = "01/" & (CBMES.SelectedIndex + 1) & "/" & CBAño.Text
     FechaActual = CDate(fCha)

     cmd4.Parameters.AddWithValue("@anio", FechaActual.Year)
     cmd4.Parameters.AddWithValue("@mes", FechaActual.Month)

     cmd4.ExecuteNonQuery()
     cmd4.Connection.Close()
     Dim da As New SqlDataAdapter
     da.SelectCommand = cmd4
     da.SelectCommand.Connection = cnn

     ''--------------------------------------------
     Dim DsInfParametros As New DataSet
     da.Fill(DsInfParametros, "DsInfParametros")

     DsInfParametros.Tables(0).TableName = "InfGeneral"
     DsInfParametros.Tables(1).TableName = "InfPorAgente"

     DvGeneral.Table = DsInfParametros.Tables("InfGeneral")
     DvPorAgentes.Table = DsInfParametros.Tables("InfPorAgente")

     If DvGeneral.Count <= 0 Then
       MessageBox.Show("No existen parametros indicados para el mes y año indicados", "Sin paramentros para el mes " & CBMES.Text & " del " & CBAño.Text)
       txtMinimoparabono.Text = ""
       txtScoreCard.Text = ""
       txtClientes.Text = ""
       txtLineasHalcon.Text = ""
   txtLineasObjetivo.Text = ""
   Button1.Visible = True
   Exit Sub
  End If

  Button1.Visible =False
  'Lleno datos fe parametros generales
  For Each item As DataRowView In DvGeneral
       Dim row As DataRow = item.Row
       txtMinimoparabono.Text = Trim(item("MinimoParaBono").ToString)
       txtScoreCard.Text = Trim(item("Porc_ScoreCard").ToString)
       txtClientes.Text = Trim(item("Porc_Clientes").ToString)
       txtLineasHalcon.Text = Trim(item("Porc_Halcon").ToString)
       txtLineasObjetivo.Text = Trim(item("Porc_LineasObjetivo").ToString)
     Next

     DGImporteBono.DataSource = DvPorAgentes
     LlenaGridObjetivos()

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

 'BOTON ELIMINAR
 Sub EliminarRegistroAgente()
   Dim i As Integer
   Dim agente As String
   Dim anio As String
   Dim mes As Integer
   Dim linea As String

   Dim objetivo As Decimal
   Try
     i = DGImporteBono.CurrentRow.Index
     agente = DGImporteBono.Item(0, i).Value.ToString
     anio = DGImporteBono.Item(3, i).Value.ToString
     mes = DGImporteBono.Item(2, i).Value.ToString
     linea = DGImporteBono.Item(4, i).Value.ToString
     objetivo = DGImporteBono.Item(5, i).Value.ToString

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
                       "FROM LOObjInd", DGImporteBono)

         Buscar_Parametros()
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

 Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
   Me.Close()
 End Sub

 Private Sub btnGrabaGenerales_Click(sender As Object, e As EventArgs) Handles btnGrabaGenerales.Click
   SQL.conectarTPM()
   Try
     If Integer.Parse(txtMinimoparabono.Text) <= 0 Then
       MessageBox.Show("Falta Porcentaje mínimo para obtener bono mensual", "Por favor ingrese el número en porcentaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
       txtMinimoparabono.Select()
       Exit Sub
     End If
     If Integer.Parse(txtScoreCard.Text) <= 0 Then
       MessageBox.Show("Falta Porcentaje del Score Card", "Por favor ingrese el número en porcentaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
       txtScoreCard.Select()
       Exit Sub
     End If
     If Integer.Parse(txtClientes.Text) <= 0 Then
       MessageBox.Show("Falta Porcentaje del Score Card Clientes", "Por favor ingrese el número en porcentaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
       txtClientes.Select()
       Exit Sub
     End If
     If Integer.Parse(txtLineasHalcon.Text) <= 0 Then
       MessageBox.Show("Falta Porcentaje del Score Card Líneas Halcon", "Por favor ingrese el número en porcentaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
       txtLineasHalcon.Select()
       Exit Sub
     End If
     If Integer.Parse(txtLineasObjetivo.Text) <= 0 Then
       MessageBox.Show("Falta Porcentaje del Score Card Líneas Objetivo", "Por favor ingrese el número en porcentaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
       txtLineasObjetivo.Select()
       Exit Sub
     End If

     'Verifico suma de los porcentajes, deben dar el 100% entre las 4
     Dim Suma As Integer
     Suma = Integer.Parse(txtScoreCard.Text) + Integer.Parse(txtClientes.Text) + Integer.Parse(txtLineasHalcon.Text) + Integer.Parse(txtLineasObjetivo.Text)

     If Suma <> 100 Then
       MsgBox("La suma de los porcentajes de los ScoreCard deben sar 100%", MsgBoxStyle.Critical, "Por favor corrija las cantidades")
       Exit Sub
     End If

     'Valido que no exista ya el registro
     Dim Existe As String = SQL.CampoEspecifico("SELECT count(*) as Total FROM BonoMensual_ParametrosGenerales WHERE Año = " & CBAño.Text & " AND Mes = " & CBMES.SelectedIndex + 1, "Total")

     If Existe > 0 Then
       If MsgBox("Este Periodo ya esta registrado, desea modificarlo?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Por favor confirme") = vbNo Then
         Exit Sub
       Else
         SQL.EjecutarComando("UPDATE BonoMensual_ParametrosGenerales Set MinimoParaBono = " & Decimal.Parse(txtMinimoparabono.Text.ToString) &
                             ", Porc_ScoreCard = " & Decimal.Parse(txtScoreCard.Text.ToString) &
                             ", Porc_Clientes = " & Decimal.Parse(txtClientes.Text.ToString) &
                             ", Porc_Halcon = " & Decimal.Parse(txtLineasHalcon.Text.ToString) &
                             ", Porc_LineasObjetivo = " & Decimal.Parse(txtLineasObjetivo.Text.ToString) &
                             " WHERE Año = " & CBAño.Text & " And Mes = " & CBMES.SelectedIndex + 1)
         Buscar_Parametros()
         MessageBox.Show("La información general fue actualizada correctamente", "Actualización correcta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
       End If
     Else
       If (MessageBox.Show(
      "¿Confirma que desea generar este registro?" + Chr(13) + Chr(13) & CmbAgteVta.Text & "   " & CBMES.Text &
                      "   " & CBAño.Text,
                  "Generar Registro", MessageBoxButtons.YesNo,
                  MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

         Dim SqlSQry As String = "INSERT INTO BonoMensual_ParametrosGenerales (Año, Mes, MinimoParaBono, Porc_ScoreCard, Porc_Clientes, Porc_Halcon, Porc_LineasObjetivo)
                                  VALUES (" & CBAño.Text & "," & CBMES.SelectedIndex + 1 & "," & Decimal.Parse(txtMinimoparabono.Text.ToString) &
                                   ", " & Decimal.Parse(txtScoreCard.Text.ToString) & ", " & Decimal.Parse(txtClientes.Text.ToString) & "," &
                                   Decimal.Parse(txtLineasHalcon.Text.ToString) & "," & Decimal.Parse(txtLineasObjetivo.Text.ToString) & ")"
         SQL.EjecutarComando(SqlSQry)

         Buscar_Parametros()
       End If
     End If

     SQL.Cerrar()

   Catch ex As Exception
     SQL.Cerrar()
     MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
   End Try
 End Sub

 Private Sub btnGrabaDetalles_Click(sender As Object, e As EventArgs) Handles btnGrabaDetalles.Click
   SQL.conectarTPM()
   If CmbAgteVta.SelectedValue = 999 Then
     MessageBox.Show(
   "Seleccione agente ", "Agente no válido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
   Else
     Try
       If CDbl(txtCantidadLinea.Text) <= 0 Then
         MessageBox.Show("Falta el importe del bono mensual", "Por favor ingrese el importe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
         txtCantidadLinea.Select()
         Exit Sub
       End If

       'Valido que no exista ya el registro
       Dim Existe As String = SQL.CampoEspecifico("SELECT count(*) as Total FROM BonoMensual_ParametrosPorAgente WHERE slpcode = " & CmbAgteVta.SelectedValue & " AND anio = " & CBAño.Text & " AND mes = " & CBMES.SelectedIndex + 1, "Total")

       If Existe > 0 Then
         If MsgBox("Este Agente ya esta registrado para ese mes y fecha, desea modificarlo con este nuevo importe?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Por favor confirme") = vbNo Then
           Exit Sub
         Else
           SQL.EjecutarComando("UPDATE BonoMensual_ParametrosPorAgente SET ImporteBonoMensual = " & Decimal.Parse(txtCantidadLinea.Text.ToString) & " WHERE slpcode = " & CmbAgteVta.SelectedValue & " AND anio = " & CBAño.Text & " AND mes = " & CBMES.SelectedIndex + 1)
           Buscar_Parametros()
           MessageBox.Show("La información del agente fue actualizada correctamente", "Actualización correcta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
         End If
       Else
         Dim SqlSQry As String = "INSERT INTO BonoMensual_ParametrosPorAgente (slpcode, slpname, mes, anio, ImporteBonoMensual) 
                                    VALUES (" & CmbAgteVta.SelectedValue & ",'" & CmbAgteVta.Text.ToString & "'," & CBMES.SelectedIndex + 1 & ", " & CBAño.Text & ", " & Decimal.Parse(txtCantidadLinea.Text.ToString) & ")"
         SQL.EjecutarComando(SqlSQry)
         Buscar_Parametros()
       End If

       SQL.Cerrar()

     Catch ex As Exception
       SQL.Cerrar()
       MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
     End Try
   End If
 End Sub

 Private Sub btnEliminarRegistro_Click(sender As Object, e As EventArgs) Handles btnEliminarRegistro.Click
   Try
     SQL.conectarTPM()

     If (MessageBox.Show(
        "¿Confirma que desea eliminar este registro?",
                    "Generar Registro", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

       Dim SqlSQry As String = "DELETE FROM BonoMensual_ParametrosPorAgente WHERE slpcode = " & CveAgt & " AND mes = " & iMes & " AND anio = " & iAño
       SQL.EjecutarComando(SqlSQry)

       For Each item As DataRowView In DvPorAgentes
         Dim row As DataRow = item.Row
         If row("slpcode") = CveAgt Then
           row.Delete()
           Exit For
         End If
       Next


       Buscar_Parametros()
       MessageBox.Show("La información del agente fue eliminada correctamente", "Eliminación correcta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
     End If

     SQL.Cerrar()
   Catch ex As Exception
     SQL.Cerrar()
   End Try
 End Sub

 Private Sub DGImporteBono_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGImporteBono.CellContentClick
   selectRenglon(e.RowIndex)
 End Sub

 Private Sub selectRenglon(renglon As Integer)
   If renglon >= 0 Then
     Dim row As DataGridViewRow = DGImporteBono.Rows(renglon)
     CveAgt = row.Cells("slpcode").Value
     iMes = row.Cells("mes").Value
     iAño = row.Cells("anio").Value
   End If
 End Sub

 Private Sub DGImporteBono_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DGImporteBono.RowEnter
   selectRenglon(e.RowIndex)
 End Sub

 Private Sub CBMES_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBMES.SelectedIndexChanged
   Buscar_Parametros()
 End Sub

 Private Sub CBAño_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBAño.SelectedIndexChanged
   Buscar_Parametros()
 End Sub

Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
 Dim MesToFind As Integer = CBMES.SelectedIndex
 Dim YearToFind As Integer = CBAño.Text
 Dim MesToPut As Integer = CBMES.SelectedIndex + 1
 Dim YearToPut As Integer = CBAño.Text

  SQL.conectarTPM()

  If MesToFind = 0 Then
  MesToFind = 12
  YearToFind = YearToFind - 1
 End If

 Try
  SQL.EjecutarComando("INSERT INTO BonoMensual_ParametrosGenerales (Año, Mes, MinimoParaBono, Porc_ScoreCard, Porc_Clientes, Porc_Halcon, Porc_LineasObjetivo) SELECT " & YearToPut & "," & MesToPut & ", MinimoParaBono, Porc_ScoreCard, Porc_Clientes, Porc_Halcon, Porc_LineasObjetivo FROM BonoMensual_ParametrosGenerales WHERE mes = " & MesToFind & " AND Año = " & YearToFind)
  SQL.EjecutarComando("INSERT INTO BonoMensual_ParametrosPorAgente (slpcode, slpname, mes, anio, ImporteBonoMensual) SELECT slpcode, slpname, " & MesToPut & ", " & YearToPut & ", ImporteBonoMensual FROM BonoMensual_ParametrosPorAgente WHERE mes = " & MesToFind & " AND Anio = " & YearToFind)

 Catch ex As Exception
   MessageBox.Show("Se presentó un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
   SQL.Cerrar()
   Return
 End Try
  MessageBox.Show("La información del agente fue actualizada correctamente", "Actualización correcta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
  Buscar_Parametros()
  SQL.Cerrar()
 End Sub
End Class