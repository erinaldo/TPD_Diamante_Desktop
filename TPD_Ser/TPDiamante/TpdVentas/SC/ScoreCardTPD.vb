Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel

Public Class ScoreCardTPD
 'Conexiones a la Base de datos
 'Public StrProd As String = conexion_universal.CadenaSBO_Diamante
 Public StrTpm As String = conexion_universal.CadenaSQL
 Public StrCon As String = conexion_universal.CadenaSQLSAP

 Dim DvTotales As New DataView
 Dim DvAgentes As New DataView
 Dim DvAgentes2 As New DataView
 Dim DvClientes As New DataView

 ''VARIABLE PARA LA CLASE COMANDOS SQL
 ''MODIFICADO POR IVAN GONZALEZ
 Dim SQL As New Comandos_SQL()

 Private Sub VtasScoreCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

  'Modificacion Julian Gasca '
  '--------------------------------------------------------------------------------------------------------------------------------------'
  'IMPORTANTE ESTE REPORTE SE DEBE ACTUALIZAR CADA AÑO DEPENDIENDO LOS DIAS FERIADOS PARA SU CORRECTO FUNCIONAMIENTO 
  'La tabla de sql donde se deben agregar los dias festivos es la siguiente : [dbo].[tbl_Festivos]
  'Donde se tendran que agregar la fecha del dia festivo en caso de ser el dia completo se pondra  0.00 en caso de quue
  'Solo sea medio dia debe colocarse 0.60


  Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

  Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")

  Dim ConsutaListaS As String
  Dim ConsutaListaA As String

  '*********************************************************************************************************************************************
  'Codigo para obtener informacion de los agentes que cada usuario debe ver
  '*********************************************************************************************************************************************
  SQL.conectarTPM()

  Dim GroupCode As String
  Dim slpcode As String = SQL.CampoEspecifico("SELECT AgteVentas FROM Usuarios where Id_Usuario = '" & UsrTPM & "'", "AgteVentas")
  Dim CodAgte As String = SQL.CampoEspecifico("SELECT CodAgte FROM Usuarios where Id_Usuario = '" & UsrTPM & "'", "CodAgte")

  If slpcode = "" And UsrTPM <> "MANAGER" And UsrTPM <> "COMERCIAL" Then
   CerrarSCClientes = True
   MsgBox("Este usuario no tienen definido el valor de Agte Ventas en su registro", MsgBoxStyle.Exclamation, "Falta configuración de agente ventas")
   Exit Sub
  End If

  If slpcode <> "" Then
   GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " & slpcode, "GroupCode")
   If GroupCode = False Then
    MsgBox("Verifique que este usuario este registrado en la tabla DEPCOBR de TPM", MsgBoxStyle.Exclamation, "Falta configuración en tabla")
    Exit Sub
   End If
  Else
   GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " & CodAgte, "GroupCode")
  End If

  Dim Almacen As String = SQL.CampoEspecifico("select Almacen from Usuarios where Id_Usuario = '" & UsrTPM & "'", "Almacen")

  Button3.Visible = False
  'COMBO DE SUCURSALES
  If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Then
   Button3.Visible = True
   ConsutaListaS = "SELECT GroupCode,GroupName FROM SBO_TPD.dbo.OCRG with (nolock) WHERE GroupType = 'C' ORDER BY GroupName"
  Else
   ConsutaListaS = "SELECT GroupCode,GroupName FROM SBO_TPD.dbo.OCRG with (nolock) WHERE GroupType = 'C' AND GroupCode = '" & GroupCode & "' ORDER BY GroupName"
    End If

    'COMBO DE AGENTES
    If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Then
      'SQL.LlenarComboBox1("SELECT T0.slpcode,T0.slpname FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 ORDER BY slpname", "slpcode,slpname", cmbAgteVta)
      ConsutaListaA = "SELECT DISTINCT T0.SlpCode,SlpName,T1.GroupCode FROM SBO_TPD.dbo.OSLP t0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') ORDER BY SlpName"

      'If UsrTPM = "RROBLES" Or UsrTPM = "VENTAS5" Then 'GERENTE DE MERIDA
      '  ConsutaListaA &= " AND T1.GroupCode = 102"
      'End If

    ElseIf UsrTPM = "RROBLES" Or UsrTPM = "VVERGARA" Or UsrTPM = "VENTAS5" Then
      ConsutaListaA = "SELECT DISTINCT T0.SlpCode,SlpName,T1.GroupCode FROM SBO_TPD.dbo.OSLP t0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') AND Memo = '" & Almacen & "' AND T1.GroupCode = 102 ORDER BY slpname"
    Else
      'SI ES AGENETE DE MARKETING/VENTAS
      ConsutaListaA = "SELECT DISTINCT T0.SlpCode,SlpName,T1.GroupCode FROM SBO_TPD.dbo.OSLP t0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE U_VENTAS = '" & UsrTPM & "' AND (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') ORDER BY slpname"

      'If CmbAgteVta.Items.Count = 0 Then
      '  If slpcode <> "" Then
      '    ConsutaListaA = "SELECT SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE SlpCode = " & slpcode & " ORDER BY slpname"
      '  Else
      '    ConsutaListaA = "select SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE SlpCode = " & CodAgte & " ORDER BY slpname"
      '  End If
      'End If
    End If

    '*********************************************************************************************************************************************

    'Variable para guardar la consulta de AGENTES y SUCURSALES en los combobox

    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
      Dim DSetTablas As New DataSet

      'ConsutaLista = "SELECT GroupCode , GroupName FROM OCRG with (nolock) WHERE GroupType = 'C' "

      'If UsrTPM = "RROBLES" Or UsrTPM = "VENTAS5" Then 'GERENTE DE MERIDA
      '  ConsutaListaS &= " AND GroupCode = 102"
      'End If

      Dim daGSucural As New SqlClient.SqlDataAdapter(ConsutaListaS, SqlConnection)

      'Dim DSetTablas As New DataSet
      daGSucural.Fill(DSetTablas, "Sucursales")

      If DSetTablas.Tables("Sucursales").Rows.Count > 1 Then
        Dim fila As Data.DataRow
        'Asignamos a fila la nueva Row(Fila)del Dataset
        fila = DSetTablas.Tables("Sucursales").NewRow
        'Agregamos los valores a los campos de la tabla
        fila("GroupName") = "TODAS"
        fila("GroupCode") = 99

        'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
        DSetTablas.Tables("Sucursales").Rows.Add(fila)
      End If

      Me.CmbSucursal.DataSource = DSetTablas.Tables("Sucursales")
      Me.CmbSucursal.DisplayMember = "GroupName"
      Me.CmbSucursal.ValueMember = "GroupCode"
      If DSetTablas.Tables("Sucursales").Rows.Count > 1 Then
        Me.CmbSucursal.SelectedValue = 99
      Else
        Me.CmbSucursal.SelectedIndex = 0
      End If

      '---------------------------------------------------------
      'ConsutaLista = "SELECT T0.slpcode,T0.slpname,T1.GroupCode FROM OSLP T0 "
      'ConsutaLista &= "INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
      'ConsutaLista &= "WHERE T1.CbrGralAdicional = 'N' AND T0.SLPCODE <> 1 AND (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC')"

      Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaListaA, SqlConnection)

      daAgte.Fill(DSetTablas, "Agentes")

      Dim filaAgte As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      filaAgte = DSetTablas.Tables("Agentes").NewRow

      'Agregamos los valores a los campos de la tabla
      filaAgte("slpname") = "TODOS"
      filaAgte("slpcode") = 999
      filaAgte("GroupCode") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

      DvAgentes2.Table = DSetTablas.Tables("Agentes")

      Me.CmbAgteVta.DataSource = DvAgentes2
      Me.CmbAgteVta.DisplayMember = "slpname"
      Me.CmbAgteVta.ValueMember = "slpcode"
      Me.CmbAgteVta.SelectedValue = 999

    End Using

    SQL.Cerrar()
  End Sub

  Sub BuscaAgentes()

    If CmbSucursal.SelectedValue Is Nothing Or CmbSucursal.SelectedValue = 99 Then
      DvAgentes2.RowFilter = String.Empty
      Me.CmbAgteVta.SelectedValue = 999
      Me.CmbAgteVta.DataSource = DvAgentes2

    Else
      DvAgentes2.RowFilter = String.Empty
      Me.CmbAgteVta.SelectedValue = 999
      DvAgentes2.RowFilter = "GroupCode = " & Trim(Me.CmbSucursal.SelectedValue.ToString) & " OR GroupCode = 999"
    End If
  End Sub

  Private Sub DgVtaAgte_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgVtaAgte.CurrentCellChanged
    Try
      If DgVtaAgte.Item(0, DgVtaAgte.CurrentCell.RowIndex).Value.ToString = 999 Then
        DvClientes.RowFilter = ""
      ElseIf DgVtaAgte.Item(0, DgVtaAgte.CurrentCell.RowIndex).Value.ToString = 100 Then
        DvClientes.RowFilter = "groupcode=100 "
      ElseIf DgVtaAgte.Item(0, DgVtaAgte.CurrentCell.RowIndex).Value.ToString = 102 Then
        DvClientes.RowFilter = "groupcode=102 "
      ElseIf DgVtaAgte.Item(0, DgVtaAgte.CurrentCell.RowIndex).Value.ToString = 103 Then
        DvClientes.RowFilter = "groupcode=103 "
      Else
        DvClientes.RowFilter = "slpcode =" & DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value.ToString '& " OR GroupCode = 999"
      End If


      'DvClientes.RowFilter = "slpcode =" & DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value.ToString '& " OR GroupCode = 999"
    Catch ex As Exception
    End Try

  End Sub


  Private Sub CmbSucursal_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbSucursal.SelectionChangeCommitted
    BuscaAgentes()
  End Sub

  Private Sub CmbSucursal_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbSucursal.Validating
    BuscaAgentes()
  End Sub

  Private Sub CmbSucursal_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbSucursal.KeyUp
    BuscaAgentes()
  End Sub

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    DgVtaAgte.DataSource = Nothing
    Buscar_NotasC()
  End Sub

  Sub Buscar_NotasC()
    Dim vDiasMes As Integer
    Dim cnn As SqlConnection = Nothing
    Dim cmd As SqlCommand = Nothing

    Dim cmd2 As SqlCommand = Nothing
    Dim vDiasTrans As Integer

    Dim cmd3 As SqlCommand = Nothing
    Dim cmd4 As SqlCommand = Nothing


    Try
      cnn = New SqlConnection(StrTpm)
      cmd = New SqlCommand("Indicadores", cnn)
      cmd.CommandType = CommandType.StoredProcedure
      cmd.Parameters.Add("@FechaFinal", SqlDbType.SmallDateTime).Value = Me.DtpFechaIni.Value
      cmd.Parameters.Add("@TipoConsulta", SqlDbType.Int).Value = 1

      cnn.Open()

      'MODIFICADO POR IVAN GONZALEZ
      SQL.conectarTPM()
      Dim mont As String = Month(DtpFechaIni.Value.ToString("dd-MM-yyyy"))
      Dim yea As String = Year(DtpFechaIni.Value.ToString("dd-MM-yyyy"))
      vDiasMes = SQL.CampoEspecifico("EXEC TPD_DiasHabiles " & mont & "," & yea, "Dias")
      SQL.Cerrar()

      'vDiasMes = CInt(cmd.ExecuteScalar())
      txtDiasMes.Text = vDiasMes.ToString

      cmd2 = New SqlCommand("Indicadores", cnn)
      cmd2.CommandType = CommandType.StoredProcedure
      cmd2.Parameters.Add("@FechaFinal", SqlDbType.SmallDateTime).Value = Me.DtpFechaIni.Value
      cmd2.Parameters.Add("@TipoConsulta", SqlDbType.Int).Value = 2

      vDiasTrans = CInt(cmd2.ExecuteScalar())
      txtDiasTranscurridos.Text = vDiasTrans.ToString

      cmd4 = New SqlCommand("TPD_ScoreCard_General", cnn)
      cmd4.CommandType = CommandType.StoredProcedure
      cmd4.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = Me.DtpFechaIni.Value
      cmd4.Parameters.Add("@DiasMes", SqlDbType.Int).Value = vDiasMes
      cmd4.Parameters.Add("@DiasTrans", SqlDbType.Int).Value = vDiasTrans

      Dim DiasRestantes As Integer
      DiasRestantes = vDiasMes - vDiasTrans
      If DiasRestantes = 0 Then
        DiasRestantes = 1
      End If

      'cmd4.Parameters.Add("@DiasRest", SqlDbType.Int).Value = vDiasMes - vDiasTrans
      cmd4.Parameters.Add("@DiasRest", SqlDbType.Int).Value = DiasRestantes
      cmd4.Parameters.Add("@PorAvanOptimo", SqlDbType.Decimal).Value = vDiasTrans / vDiasMes
      cmd4.Parameters.Add("@Sucursal", SqlDbType.Int).Value = Me.CmbSucursal.SelectedValue
      cmd4.Parameters.Add("@Agente", SqlDbType.VarChar, 30).Value = Me.CmbAgteVta.SelectedValue
      cmd4.Parameters.Add("@AgenteVentas", SqlDbType.VarChar, 30).Value = UsrTPM

      'Dim mes As Int16
      'mes = DtpFechaIni.Text.Substring(3, 2)


      Dim mes As Integer
      mes = CInt(DtpFechaIni.Value.Month)

      'Dim anio As Int16
      'anio = DtpFechaIni.Text.Substring(6, 4)

      Dim anio As Integer
      anio = CInt(DtpFechaIni.Value.Year)

      'cmd4.Parameters.Add("@MesActual", SqlDbType.Int).Value = mes
      'cmd4.Parameters.Add("@AñoActual", SqlDbType.Int).Value = anio


      cmd4.ExecuteNonQuery()
      cmd4.Connection.Close()
      Dim da As New SqlDataAdapter
      da.SelectCommand = cmd4
      da.SelectCommand.Connection = cnn


      ''--------------------------------------------
      Dim DsVtas As New DataSet
      da.Fill(DsVtas, "DsVtas")

      DsVtas.Tables(0).TableName = "Totales"
      DsVtas.Tables(1).TableName = "VtaAgtes"
      DsVtas.Tables(2).TableName = "VtaCltes"

      DvTotales.Table = DsVtas.Tables("Totales")
      DvAgentes.Table = DsVtas.Tables("VtaAgtes")
      DvClientes.Table = DsVtas.Tables("VtaCltes")


      DgTotales.DataSource = DvTotales

      DgVtaAgte.DataSource = DvAgentes

      DgClientes.DataSource = DvClientes

    Catch ex As Exception
      'MsgBox(ex.Message)
    Finally
      If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
        cnn.Close()
      End If
    End Try

    txtDiasRestantes.Text = Convert.ToString(vDiasMes - vDiasTrans)
    txtAvanceOptimo.Text = Format(Convert.ToString((vDiasTrans / vDiasMes) * 100), "000.00")

    txtAvanceOptimo.Text = (vDiasTrans / vDiasMes).ToString("P1")


    '-------Diseño de DATAGRID Totales
    With Me.DgTotales
      '.DataSource = DtAgte
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
      '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
      'Color de linea del grid

      DgTotales.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try
        'Catch ex As Exception
        'End Try

        .Columns(0).HeaderText = "Clave"
        .Columns(0).Width = 50
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(1).HeaderText = "Descripción"
        .Columns(1).Width = 90

        .Columns(2).HeaderText = "Venta Dia"
        .Columns(2).Width = 80
        .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(3).HeaderText = "Acumulado"
        .Columns(3).Width = 85
        .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(4).HeaderText = "Objetivo"
        .Columns(4).Width = 85
        .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0"
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(5).HeaderText = "Acum vs Obj ($)"
        .Columns(5).Width = 105
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(5).DefaultCellStyle.Format = " $ ###,###,##0.#0"

        .Columns(6).HeaderText = "Acum Vs Obj (%)"
        .Columns(6).Width = 90
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(6).DefaultCellStyle.Format = "##0.#0 %"

        Dim numfilas As Integer
        numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 2)

          If DgTotales.Item(6, i).Value Is DBNull.Value Then
            DgTotales.Item(6, i).Value = 0
          End If


          If DgTotales.Item(6, i).Value < 0.85 Then
            DgTotales.Rows(i).Cells(6).Style.BackColor = Color.Red

          ElseIf DgTotales.Item(6, i).Value >= 0.85 And DgVtaAgte.Item(6, i).Value < 1 Then
            DgTotales.Rows(i).Cells(6).Style.BackColor = Color.Yellow

          ElseIf DgTotales.Item(6, i).Value >= 1 Then
            DgTotales.Rows(i).Cells(6).Style.BackColor = Color.LimeGreen

          End If

        Next

        .Columns(7).HeaderText = "Vta mínima requerida a la fecha."
        .Columns(7).Width = 100
        .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(7).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(8).HeaderText = "Pronostico Fin Mes ($)"
        .Columns(8).Width = 90
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(8).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(9).HeaderText = "Pronostico Fin Mes (%)"
        .Columns(9).Width = 100
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(9).DefaultCellStyle.Format = "#,##0.#0 %"

        'numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)


          If DgTotales.Item(9, i).Value Is DBNull.Value Then
            DgTotales.Item(9, i).Value = 0
          End If


          If DgTotales.Item(9, i).Value < 0.85 Then
            DgTotales.Rows(i).Cells(9).Style.BackColor = Color.Red

          ElseIf DgTotales.Item(9, i).Value >= 0.85 And DgVtaAgte.Item(9, i).Value < 1 Then
            DgTotales.Rows(i).Cells(9).Style.BackColor = Color.Yellow

          ElseIf DgTotales.Item(9, i).Value >= 1 Then
            DgTotales.Rows(i).Cells(9).Style.BackColor = Color.LimeGreen

          End If

        Next

        .Columns(10).HeaderText = "Requerido Dia Para llegar a Obj."
        .Columns(10).Width = 100
        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(10).DefaultCellStyle.Format = "$ ###,###,##0.#0"


        .Columns(11).Visible = False
        .Columns(11).HeaderText = "GroupCode"
        .Columns(11).Width = 75
        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(12).Visible = False
        .Columns(12).HeaderText = "GroupName"
        .Columns(12).Width = 75
        .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(13).Visible = False
        .Columns(13).HeaderText = "Ord Total"
        .Columns(13).Width = 80
        .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(13).DefaultCellStyle.Format = "###,###,##0.#0"

      Catch ex As Exception

      End Try


    End With


    '-------Diseño de DATAGRID Agentes
    With Me.DgVtaAgte
      Try
        '.DataSource = DtAgte
        .ReadOnly = True
        'Color de Renglones en Grid
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

        DgVtaAgte.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        .RowHeadersVisible = True
        .RowHeadersWidth = 25
        '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'Color de linea del grid

        .Columns(0).HeaderText = "Clave"
        .Columns(0).Width = 50
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(1).HeaderText = "Agente"
        .Columns(1).Width = 150

        .Columns(2).HeaderText = "Venta Día"
        .Columns(2).Width = 95
        .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(3).HeaderText = "Acumulado"
        .Columns(3).Width = 95
        .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(4).HeaderText = "$ Objetivo"
        .Columns(4).Width = 95
        .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0"
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(5).HeaderText = "Acum vs Obj ($)"
        .Columns(5).Width = 105
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(5).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(6).HeaderText = "Acum vs Obj (%)"
        .Columns(6).Width = 100
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(6).DefaultCellStyle.Format = "##0.#0 %"

        Dim numfilas As Integer

        numfilas = DgVtaAgte.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)

          If DgVtaAgte.Item(6, i).Value < 0.85 Then
            DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.Red

          ElseIf DgVtaAgte.Item(6, i).Value >= 0.85 And DgVtaAgte.Item(6, i).Value < 1 Then
            DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.Yellow

          ElseIf DgVtaAgte.Item(6, i).Value >= 1 Then
            DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.LimeGreen

          End If

        Next


        .Columns(7).HeaderText = "Vta Requerida al dia para cumplir Obj."
        .Columns(7).Width = 100
        .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(7).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(8).HeaderText = "Pron. Fin Mes ($)"
        .Columns(8).Width = 100
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(8).DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns(9).HeaderText = "Pron. Fin Mes (%)"
        .Columns(9).Width = 100
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(9).DefaultCellStyle.Format = "#,##0.#0 %"

        'Dim numfilas As Integer

        numfilas = DgVtaAgte.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)

          If DgVtaAgte.Item(9, i).Value < 0.85 Then
            DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.Red

          ElseIf DgVtaAgte.Item(9, i).Value >= 0.85 And DgVtaAgte.Item(9, i).Value < 1 Then
            DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.Yellow

          ElseIf DgVtaAgte.Item(9, i).Value >= 1 Then
            DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.LimeGreen

          End If

        Next


        .Columns(10).HeaderText = "Req. Dia Para Cumplir Obj."
        .Columns(10).Width = 105
        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(10).DefaultCellStyle.Format = " $ ###,###,##0.#0"


        .Columns(11).HeaderText = "Clave Suc."
        .Columns(11).Width = 80
        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        .Columns(12).HeaderText = "Sucursal"
        .Columns(12).Width = 105
        .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(13).HeaderText = "Estatus"
        .Columns(13).Width = 105
        .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(14).Visible = False
        .Columns(14).HeaderText = "Ord Total"
        .Columns(14).Width = 80
        .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(14).DefaultCellStyle.Format = "###,###,##0.#0"

      Catch ex As Exception

      End Try

    End With


    '-------Diseño de DATAGRID Clientes
    With Me.DgClientes
      Try
        '.DataSource = DtAgte
        .ReadOnly = True
        'Color de Renglones en Grid
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        .RowHeadersVisible = True
        .RowHeadersWidth = 25
        '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        'Color de linea del grid

        .Columns(0).HeaderText = "Clave"
        .Columns(0).Width = 70
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns(1).HeaderText = "Cliente"
        .Columns(1).Width = 193

        .Columns(2).HeaderText = "Venta Dia"
        .Columns(2).Width = 100
        .Columns(2).DefaultCellStyle.Format = "$ ###,###,###.00"
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(3).HeaderText = "Acumulado"
        .Columns(3).Width = 110
        .Columns(3).DefaultCellStyle.Format = "$ ###,###,###.00"
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '----COLUMNA 8
        .Columns(4).HeaderText = "Pron. Fin Mes ($)"
        .Columns(4).Width = 120
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(4).DefaultCellStyle.Format = "$ ###,###,###.00"


        '----COLUMNA 11
        .Columns(5).HeaderText = "Clave Agte."
        .Columns(5).Width = 70
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '----COLUMNA 12
        .Columns(6).HeaderText = "Agente"
        .Columns(6).Width = 170
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '----COLUMNA 13
        .Columns(7).HeaderText = "Clave Suc."
        .Columns(7).Width = 70
        .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '----COLUMNA 14
        .Columns(8).HeaderText = "Sucursal"
        .Columns(8).Width = 140
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'COLUMNA 15
        .Columns(9).HeaderText = "Ruta"
        .Columns(9).Width = 140
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Catch ex As Exception

      End Try
    End With
  End Sub


  '---Generar reporte en EXCEL de TOTALES
  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    ExportarNuevoTotales()
  End Sub

  Sub ExportarNuevoTotales()
    Dim dv As DataView = DirectCast(DgTotales.DataSource, DataView)
    'Dim ds As DataSet = DgVtaAgte.DataSource
    Dim dt As DataTable = dv.Table

    Dim wb = New XLWorkbook()
    Dim ws = wb.Worksheets.Add("ScoreCard (Totales)")

    Dim range = ws.Range(3, 1, dt.Rows.Count + 3, dt.Columns.Count).Merge().AddToNamed("Totales")
    Dim rangeWithData = ws.Cell(4, 1).InsertData(dt.AsEnumerable)

    Dim tab = range.CreateTable()
    tab.Theme = XLTableTheme.TableStyleLight8

    'DAR FOMATO A LAS CELDAS
    Dim index As Integer = 3

    For i As Integer = 0 To dt.Rows.Count

      Try
        'Encabezados dependiendo
        If index = 3 Then
          Dim cellA3 As String = String.Format("A{0}", 1)
          wb.Worksheet(1).Cells(cellA3).Value = "Reporte de Ventas Totales Del Periodo " & Format(Me.DtpFechaIni.Value)
          wb.Worksheet(1).Cells(cellA3).Style.Font.Bold = True

          Dim cellA0 As String = String.Format("A{0}", index)
          wb.Worksheet(1).Cells(cellA0).Value = "Clave"

          Dim cellB0 As String = String.Format("B{0}", index)
          wb.Worksheet(1).Cells(cellB0).Value = "Descripción"

          Dim cellC0 As String = String.Format("C{0}", index)
          wb.Worksheet(1).Cells(cellC0).Value = "Venta Día"

          Dim cellD0 As String = String.Format("D{0}", index)
          wb.Worksheet(1).Cells(cellD0).Value = "Acumulado"

          Dim cellE0 As String = String.Format("E{0}", index)
          wb.Worksheet(1).Cells(cellE0).Value = "$ Objetivo"

          Dim cellF0 As String = String.Format("F{0}", index)
          wb.Worksheet(1).Cells(cellF0).Value = "Acum vs Obj ($)"

          Dim cellG0 As String = String.Format("G{0}", index)
          wb.Worksheet(1).Cells(cellG0).Value = "Acum vs Obj (%)"

          Dim cellH0 As String = String.Format("H{0}", index)
          wb.Worksheet(1).Cells(cellH0).Value = "Vta mínima Requerida a la fecha."

          Dim cellI0 As String = String.Format("I{0}", index)
          wb.Worksheet(1).Cells(cellI0).Value = "Pronóstico Fin Mes ($)"

          Dim cellJ0 As String = String.Format("J{0}", index)
          wb.Worksheet(1).Cells(cellJ0).Value = "Pronóstico Fin Mes (%)"

          Dim cellK0 As String = String.Format("K{0}", index)
          wb.Worksheet(1).Cells(cellK0).Value = "Requerido Día Para Llegar a Obj."

          index = index + 1
        End If

        'Formato de cada una de las celdas
        Dim cellA As String = String.Format("A{0}", index)
        'wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "#,##0"

        Dim cellB As String = String.Format("B{0}", index)
        'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellC As String = String.Format("C{0}", index)
        wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellD As String = String.Format("D{0}", index)
        wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellE As String = String.Format("E{0}", index)
        wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellF As String = String.Format("F{0}", index)
        wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellG As String = String.Format("G{0}", index)
        wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "0.00 %"

        Dim cellH As String = String.Format("H{0}", index)
        wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellI As String = String.Format("I{0}", index)
        wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellJ As String = String.Format("J{0}", index)
        wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "0.00 %"

        Dim cellK As String = String.Format("K{0}", index)
        wb.Worksheet(1).Cells(cellK).Style.NumberFormat.Format = "$ #,##0.00"

      Catch ex As Exception
        MessageBox.Show(ex.ToString(), "Error al dar formato a celdas (Totales): ")
      End Try

      index = index + 1
    Next

    ws.Columns("N").Delete()
    ws.Columns("M").Delete()
    ws.Columns("L").Delete()
    ws.Columns().Width = 15
    ws.Rows(3).Style.Alignment.WrapText = False

    Try
      Dim saveFileDialog1 As New SaveFileDialog()
      saveFileDialog1.Filter = "Excel|*.xlsx"
      saveFileDialog1.Title = "Save Excel File"
      saveFileDialog1.FileName = "ScoreCard (Totales) al " & DtpFechaIni.Value.ToString("dd-MM-yyyy") & ".xlsx"
      saveFileDialog1.ShowDialog()
      saveFileDialog1.InitialDirectory = "C:/"

      If saveFileDialog1.FileName <> "" Then
        Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
        fs.Close()
      End If

      Dim strFileName As String = saveFileDialog1.FileName
      wb.SaveAs(strFileName)
      Process.Start(saveFileDialog1.FileName)
    Catch ex As Exception
      MessageBox.Show(ex.ToString(), "Error al guardar el archivo (Totales): ")
    End Try
  End Sub

  Sub ExportarViejoTotales()
    Dim oExcel As Object
    Dim oBook As Object
    Dim oSheet As Object

    Dim Rangos As String = ""
    Dim Rangos2 As String = ""

    'MsgBox("El reporte se creara a continuación")

    'Abrimos un nuevo libro
    oExcel = CreateObject("Excel.Application")
    oBook = oExcel.workbooks.add
    oSheet = oBook.worksheets(1)


    'Declaramos el nombre de las columnas
    oSheet.range("A3").value = "SlpCode"
    oSheet.range("B3").value = "SlpName"
    oSheet.range("C3").value = "VtaDia"
    oSheet.range("D3").value = "Acumulado"
    oSheet.range("E3").value = "Objetivo"
    oSheet.range("F3").value = "Acum vs Obj"
    oSheet.range("G3").value = "PorAcum vs Obj"
    oSheet.range("H3").value = "Vta Requerida"
    oSheet.range("I3").value = "Pron Fin Mes"
    oSheet.range("J3").value = "Por Pron Fin Mes"
    oSheet.range("K3").value = "Req Dia P Obj"
    oSheet.range("L3").value = "GroupCode"
    oSheet.range("M3").value = "GroupName"
    oSheet.range("N3").value = "Ord Total"

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A3:N3").font.bold = True
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgTotales.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel0 As String = Me.DgTotales.Item(0, fila_dt).Value
      Dim cel1 As String = Me.DgTotales.Item(1, fila_dt).Value
      Dim cel2 As String = IIf(IsDBNull(Me.DgTotales.Item(2, fila_dt).Value), 0, Me.DgTotales.Item(2, fila_dt).Value)
      Dim cel3 As String = IIf(IsDBNull(Me.DgTotales.Item(3, fila_dt).Value), 0, Me.DgTotales.Item(3, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgTotales.Item(4, fila_dt).Value), 0, Me.DgTotales.Item(4, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgTotales.Item(5, fila_dt).Value), 0, Me.DgTotales.Item(5, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgTotales.Item(6, fila_dt).Value), 0, Me.DgTotales.Item(6, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgTotales.Item(7, fila_dt).Value), 0, Me.DgTotales.Item(7, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgTotales.Item(8, fila_dt).Value), 0, Me.DgTotales.Item(8, fila_dt).Value)
      Dim cel9 As String = IIf(IsDBNull(Me.DgTotales.Item(9, fila_dt).Value), 0, Me.DgTotales.Item(9, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.DgTotales.Item(10, fila_dt).Value), 0, Me.DgTotales.Item(10, fila_dt).Value)
      Dim cel11 As String = IIf(IsDBNull(Me.DgTotales.Item(11, fila_dt).Value), 0, Me.DgTotales.Item(11, fila_dt).Value)
      Dim cel12 As String = Me.DgTotales.Item(12, fila_dt).Value
      Dim cel13 As String = IIf(IsDBNull(Me.DgTotales.Item(13, fila_dt).Value), 0, Me.DgTotales.Item(13, fila_dt).Value)


      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel0
      oSheet.range("B" & fila_dt_excel).value = cel1
      oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
      oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
      oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
      oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
      oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 2)
      oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
      oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 2)
      oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
      oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 2)
      oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 2)
      oSheet.range("M" & fila_dt_excel).value = cel12
      oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel13, 2)
    Next

    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:N").entirecolumn.autofit()

    'ENCABEZADO DEL REPORTE GENERADO

    Dim sqlConnection1 As New SqlConnection(conexion_universal.CadenaSQLSAP)
    Dim cmd As New SqlCommand
    Dim returnValue As Object

    cmd.CommandText = "SELECT slpname from oslp where slpcode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString)
    cmd.CommandType = CommandType.Text
    cmd.Connection = sqlConnection1

    sqlConnection1.Open()

    returnValue = cmd.ExecuteScalar()

    sqlConnection1.Close()

    Dim cnn As SqlConnection = Nothing


    If CmbAgteVta.SelectedValue = 999 Then
      oSheet.range("A1").value = "Reporte de Ventas TOTALES de todos los AGENTES con FECHA " & Format(Me.DtpFechaIni.Value)
    Else
      oSheet.range("A1").value = "Reporte de Ventas del AGENTE " & returnValue & " con FECHA " & Format(Me.DtpFechaIni.Value)
    End If


    oSheet.range("C1").value = Rangos
    oSheet.range("C2").value = Rangos2
    'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") & " Al " & Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub

  '--------Generar Excel Agentes
  Private Sub BtnAgentes_Click(sender As Object, e As EventArgs) Handles BtnAgentes.Click
    'export()

    Dim Columnas As String() = {"Clave", "Agente", "Venta del día", "Acumulado", "$ Objetivo",
                                "Acum vs Obj ($)", "Acum vs Obj (%)", "Vta Requerida al dia para cumplir Obj.", "Pron. Fin Mes ($)", "Pron. Fin Mes (%)",
                                "Req. Día Para Cumplir Obj.", "Clave Suc.", "Sucursal", "Estatus"}
    Dim TipoColumna As TipoDeDato() = {TipoDeDato.Cadena, TipoDeDato.Cadena, TipoDeDato.Pesos, TipoDeDato.Pesos, TipoDeDato.Pesos,
                                       TipoDeDato.Cadena, TipoDeDato.Porecentaje, TipoDeDato.Pesos, TipoDeDato.Pesos, TipoDeDato.Porecentaje,
                                       TipoDeDato.Pesos, TipoDeDato.Cadena, TipoDeDato.Cadena, TipoDeDato.Cadena}
    Dim Visible As Boolean() = {True, True, True, True, True, True, True, True, True, True, True, True, True, True, True}

    funciones.exporta2Excel("Reporte de Ventas Por Agente Del Periodo " & Format(Me.DtpFechaIni.Value), Columnas, TipoColumna, Visible, DgVtaAgte)
  End Sub

  Sub export()
    Dim dv As DataView = DirectCast(DgVtaAgte.DataSource, DataView)
    'Dim ds As DataSet = DgVtaAgte.DataSource
    Dim dt As DataTable = dv.Table

    Dim wb = New XLWorkbook()
    Dim ws = wb.Worksheets.Add("ScoreCard (Vendedores)")

    Dim range = ws.Range(3, 1, dt.Rows.Count + 3, dt.Columns.Count).Merge().AddToNamed("Vendedores")
    Dim rangeWithData = ws.Cell(4, 1).InsertData(dt.AsEnumerable)

    Dim tab = range.CreateTable()
    ws.Cell(6, 1).AsRange().AddToNamed("Vendedores")
    tab.Theme = XLTableTheme.TableStyleLight8

    'DAR FOMATO A LAS CELDAS
    Dim index As Integer = 3

    For i As Integer = 0 To dt.Rows.Count

      Try
        'Encabezados dependiendo
        If index = 3 Then
          Dim cellA3 As String = String.Format("A{0}", 1)
          wb.Worksheet(1).Cells(cellA3).Value = "Reporte de Ventas Por Agente Del Periodo " & Format(Me.DtpFechaIni.Value)
          wb.Worksheet(1).Cells(cellA3).Style.Font.Bold = True

          Dim cellA0 As String = String.Format("A{0}", index)
          wb.Worksheet(1).Cells(cellA0).Value = "Clave"

          Dim cellB0 As String = String.Format("B{0}", index)
          wb.Worksheet(1).Cells(cellB0).Value = "Agente"

          Dim cellC0 As String = String.Format("C{0}", index)
          wb.Worksheet(1).Cells(cellC0).Value = "Venta Día"

          Dim cellD0 As String = String.Format("D{0}", index)
          wb.Worksheet(1).Cells(cellD0).Value = "Acumulado"

          Dim cellE0 As String = String.Format("E{0}", index)
          wb.Worksheet(1).Cells(cellE0).Value = "$ Objetivo"

          Dim cellF0 As String = String.Format("F{0}", index)
          wb.Worksheet(1).Cells(cellF0).Value = "Acum vs Obj ($)"

          Dim cellG0 As String = String.Format("G{0}", index)
          wb.Worksheet(1).Cells(cellG0).Value = "Acum vs Obj (%)"

          Dim cellH0 As String = String.Format("H{0}", index)
          wb.Worksheet(1).Cells(cellH0).Value = "Vta Requerida al dia para cumplir Obj."

          Dim cellI0 As String = String.Format("I{0}", index)
          wb.Worksheet(1).Cells(cellI0).Value = "Pron. Fin Mes ($)"

          Dim cellJ0 As String = String.Format("J{0}", index)
          wb.Worksheet(1).Cells(cellJ0).Value = "Pron. Fin Mes (%)"

          Dim cellK0 As String = String.Format("K{0}", index)
          wb.Worksheet(1).Cells(cellK0).Value = "Req. Día Para Cumplir Obj."

          Dim cellL0 As String = String.Format("L{0}", index)
          wb.Worksheet(1).Cells(cellL0).Value = "Clave Suc."

          Dim cellM0 As String = String.Format("M{0}", index)
          wb.Worksheet(1).Cells(cellM0).Value = "Sucursal"

          Dim cellN0 As String = String.Format("N{0}", index)
          wb.Worksheet(1).Cells(cellN0).Value = "Estatus"

          index = index + 1
        End If

        'Formato de cada una de las celdas
        Dim cellA As String = String.Format("A{0}", index)
        wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "#,##0"

        Dim cellB As String = String.Format("B{0}", index)
        'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellC As String = String.Format("C{0}", index)
        wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellD As String = String.Format("D{0}", index)
        wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellE As String = String.Format("E{0}", index)
        wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellF As String = String.Format("F{0}", index)
        wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellG As String = String.Format("G{0}", index)
        wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "0.00 %"

        Dim cellH As String = String.Format("H{0}", index)
        wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellI As String = String.Format("I{0}", index)
        wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellJ As String = String.Format("J{0}", index)
        wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "0.00 %"

        Dim cellK As String = String.Format("K{0}", index)
        wb.Worksheet(1).Cells(cellK).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellL As String = String.Format("L{0}", index)
        wb.Worksheet(1).Cells(cellL).Style.NumberFormat.Format = "#,##0"

        Dim cellM As String = String.Format("M{0}", index)

        Dim cellN As String = String.Format("N{0}", index)

      Catch ex As Exception
        MessageBox.Show(ex.ToString(), "Error al dar formato a celdas (Vendedores): ")
      End Try

      index = index + 1
    Next

    ws.Columns("O").Delete()
    ws.Columns().Width = 15
    ws.Rows(6).Style.Alignment.WrapText = False

    Try
      Dim saveFileDialog1 As New SaveFileDialog()
      saveFileDialog1.Filter = "Excel|*.xlsx"
      saveFileDialog1.Title = "Save Excel File"
      saveFileDialog1.FileName = "ScoreCard (Vendedores) al " & DtpFechaIni.Value.ToString("dd-MM-yyyy") & ".xlsx"
      saveFileDialog1.ShowDialog()
      saveFileDialog1.InitialDirectory = "C:/"

      If saveFileDialog1.FileName <> "" Then
        Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
        fs.Close()
      End If

      Dim strFileName As String = saveFileDialog1.FileName
      wb.SaveAs(strFileName)
      Process.Start(saveFileDialog1.FileName)
    Catch ex As Exception
      MessageBox.Show(ex.ToString(), "Error al guardar el archivo (Vendedores): ")
    End Try

  End Sub

  Sub ExportarNuevoVendedores()
    Dim dv As DataView = DirectCast(DgVtaAgte.DataSource, DataView)
    'Dim ds As DataSet = DgVtaAgte.DataSource
    Dim dt As DataTable = dv.Table

    Using wb As New XLWorkbook()

      wb.Worksheets.Add(dt, "ScoreCard (Vendedores)")
      wb.Worksheets(1).Table(1).Theme = XLTableTheme.TableStyleDark1

      Dim index As Integer = 2

      For i As Integer = 0 To dt.Rows.Count

        Try
          'Encabezados dependiendo
          If i = 1 Then
            Dim cellA0 As String = String.Format("A{0}", i)
            wb.Worksheet(1).Cells(cellA0).Value = "Clave"

            Dim cellB0 As String = String.Format("B{0}", i)
            wb.Worksheet(1).Cells(cellB0).Value = "Agente"

            Dim cellC0 As String = String.Format("C{0}", i)
            wb.Worksheet(1).Cells(cellC0).Value = "Venta Día"

            Dim cellD0 As String = String.Format("D{0}", i)
            wb.Worksheet(1).Cells(cellD0).Value = "Acumulado"

            Dim cellE0 As String = String.Format("E{0}", i)
            wb.Worksheet(1).Cells(cellE0).Value = "$ Objetivo"

            Dim cellF0 As String = String.Format("F{0}", i)
            wb.Worksheet(1).Cells(cellF0).Value = "Acum vs Obj ($)"

            Dim cellG0 As String = String.Format("G{0}", i)
            wb.Worksheet(1).Cells(cellG0).Value = "Acum vs Obj (%)"

            Dim cellH0 As String = String.Format("H{0}", i)
            wb.Worksheet(1).Cells(cellH0).Value = "Vta Requerida al dia para cumplir Obj."

            Dim cellI0 As String = String.Format("I{0}", i)
            wb.Worksheet(1).Cells(cellI0).Value = "Pron. Fin Mes ($)"

            Dim cellJ0 As String = String.Format("J{0}", i)
            wb.Worksheet(1).Cells(cellJ0).Value = "Pron. Fin Mes (%)"

            Dim cellK0 As String = String.Format("K{0}", i)
            wb.Worksheet(1).Cells(cellK0).Value = "Req. Día Para Cumplir Obj."

            Dim cellL0 As String = String.Format("L{0}", i)
            wb.Worksheet(1).Cells(cellL0).Value = "Clave Suc."

            Dim cellM0 As String = String.Format("M{0}", i)
            wb.Worksheet(1).Cells(cellM0).Value = "Sucursal"

          End If

          'Formato de cada una de las celdas
          Dim cellA As String = String.Format("A{0}", index)
          wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "#,##0"

          Dim cellB As String = String.Format("B{0}", index)
          'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

          Dim cellC As String = String.Format("C{0}", index)
          wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

          Dim cellD As String = String.Format("D{0}", index)
          wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "$ #,##0.00"

          Dim cellE As String = String.Format("E{0}", index)
          wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "$ #,##0.00"

          Dim cellF As String = String.Format("F{0}", index)
          wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "$ #,##0.00"

          Dim cellG As String = String.Format("G{0}", index)
          wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "0.00 %"

          Dim cellH As String = String.Format("H{0}", index)
          wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "$ #,##0.00"

          Dim cellI As String = String.Format("I{0}", index)
          wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "$ #,##0.00"

          Dim cellJ As String = String.Format("J{0}", index)
          wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "0.00 %"

          Dim cellK As String = String.Format("K{0}", index)
          wb.Worksheet(1).Cells(cellK).Style.NumberFormat.Format = "$ #,##0.00"

          Dim cellL As String = String.Format("L{0}", index)
          wb.Worksheet(1).Cells(cellL).Style.NumberFormat.Format = "#,##0"

          Dim cellM As String = String.Format("M{0}", index)
          'wb.Worksheet(1).Cells(cellM).Style.NumberFormat.Format = "$ #,##0.00"

        Catch ex As Exception

        End Try

        index = index + 1
      Next

      'wb.Worksheet(1).Columns("K").Hide()
      'wb.Worksheet(1).Columns("L").Hide()
      'wb.Worksheet(1).Columns("M").Hide()
      'wb.Worksheet(1).Columns("N").Hide()
      'wb.Worksheet(1).Columns().AdjustToContents()
      wb.Worksheet(1).Tables(1).Theme = XLTableTheme.TableStyleDark1

      Try
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Excel|*.xlsx"
        saveFileDialog1.Title = "Save Excel File"
        saveFileDialog1.FileName = "ScoreCard (Vendedores) al " & DtpFechaIni.Value.ToString("dd-MM-yyyy") & ".xlsx"
        saveFileDialog1.ShowDialog()
        saveFileDialog1.InitialDirectory = "C:/"

        If saveFileDialog1.FileName <> "" Then
          Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
          fs.Close()
        End If

        Dim strFileName As String = saveFileDialog1.FileName
        wb.SaveAs(strFileName)
        Process.Start(saveFileDialog1.FileName)
      Catch ex As Exception
        MessageBox.Show("Error al guardar el archivo: " & ex.ToString)
      End Try
    End Using
  End Sub

  Sub ExportarViejoVendedores()
    Dim oExcel As Object
    Dim oBook As Object
    Dim oSheet As Object

    Dim Rangos As String = ""
    Dim Rangos2 As String = ""

    'Abrimos un nuevo libro
    oExcel = CreateObject("Excel.Application")
    oBook = oExcel.workbooks.add
    oSheet = oBook.worksheets(1)


    'Declaramos el nombre de las columnas
    oSheet.range("A3").value = "SlpCode"
    oSheet.range("B3").value = "SlpName"
    oSheet.range("C3").value = "VtaDia"
    oSheet.range("D3").value = "Acumulado"
    oSheet.range("E3").value = "Objetivo"
    oSheet.range("F3").value = "Acum vs Obj"
    oSheet.range("G3").value = "PorAcum vs Obj"
    oSheet.range("H3").value = "Vta Requerida"
    oSheet.range("I3").value = "Pron Fin Mes"

    oSheet.range("J3").value = "Por Pron Fin Mes"
    oSheet.range("K3").value = "Req Dia P Obj"
    oSheet.range("L3").value = "GroupCode"
    oSheet.range("M3").value = "GroupName"
    oSheet.range("N3").value = "Ord Total"

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A3:N3").font.bold = True
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgVtaAgte.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel0 As String = Me.DgVtaAgte.Item(0, fila_dt).Value
      Dim cel1 As String = Me.DgVtaAgte.Item(1, fila_dt).Value
      Dim cel2 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(2, fila_dt).Value), 0, Me.DgVtaAgte.Item(2, fila_dt).Value)
      Dim cel3 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(3, fila_dt).Value), 0, Me.DgVtaAgte.Item(3, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(4, fila_dt).Value), 0, Me.DgVtaAgte.Item(4, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(5, fila_dt).Value), 0, Me.DgVtaAgte.Item(5, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(6, fila_dt).Value), 0, Me.DgVtaAgte.Item(6, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(7, fila_dt).Value), 0, Me.DgVtaAgte.Item(7, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(8, fila_dt).Value), 0, Me.DgVtaAgte.Item(8, fila_dt).Value)
      Dim cel9 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(9, fila_dt).Value), 0, Me.DgVtaAgte.Item(9, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(10, fila_dt).Value), 0, Me.DgVtaAgte.Item(10, fila_dt).Value)
      Dim cel11 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(11, fila_dt).Value), 0, Me.DgVtaAgte.Item(11, fila_dt).Value)
      Dim cel12 As String = If(IsDBNull(Me.DgVtaAgte.Item(12, fila_dt).Value), "", Me.DgVtaAgte.Item(12, fila_dt).Value)
      Dim cel13 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(13, fila_dt).Value), 0, Me.DgVtaAgte.Item(13, fila_dt).Value)


      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel0
      oSheet.range("B" & fila_dt_excel).value = cel1
      oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
      oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
      oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
      oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
      oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 2)
      oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
      oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 2)
      oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
      oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 2)
      oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 2)
      oSheet.range("M" & fila_dt_excel).value = cel12
      oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel13, 2)
    Next


    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:N").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Ventas Por Agente Del Periodo " & Format(Me.DtpFechaIni.Value)
    oSheet.range("C1").value = Rangos
    oSheet.range("C2").value = Rangos2
    'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") & " Al " & Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub

  Private Sub BtnClientes_Click(sender As Object, e As EventArgs) Handles BtnClientes.Click
    'ExportarNuevoClientes()
    Dim Columnas As String() = {"Clave", "Cliente", "Venta del día", "Acumulado", "Pron. Fin Mes ($)", "ClaveAgte.", "Agente", "Clave Suc.", "Sucursal", "Ruta"}
    Dim TipoColumna As TipoDeDato() = {TipoDeDato.Cadena, TipoDeDato.Cadena, TipoDeDato.Pesos, TipoDeDato.Pesos, TipoDeDato.Pesos, TipoDeDato.Cadena, TipoDeDato.Cadena, TipoDeDato.Cadena, TipoDeDato.Cadena, TipoDeDato.Cadena}
    Dim Visible As Boolean() = {True, True, True, True, True, True, True, True, True, True}

    funciones.exporta2Excel("Reporte de Ventas Por Agente Del Periodo " & Format(Me.DtpFechaIni.Value), Columnas, TipoColumna, Visible, DgClientes)
  End Sub

  Sub ExportarNuevoClientes()
    Dim dv As DataView = DirectCast(DgClientes.DataSource, DataView)
    'Dim ds As DataSet = DgVtaAgte.DataSource
    Dim dt As DataTable = dv.Table

    Dim wb = New XLWorkbook()
    Dim ws = wb.Worksheets.Add("ScoreCard (Clientes)")

    Dim range = ws.Range(3, 1, dt.Rows.Count + 3, dt.Columns.Count).Merge().AddToNamed("Clientes")
    Dim rangeWithData = ws.Cell(4, 1).InsertData(dt.AsEnumerable)

    Dim tab = range.CreateTable()
    tab.Theme = XLTableTheme.TableStyleLight8

    'DAR FOMATO A LAS CELDAS
    Dim index As Integer = 3

    For i As Integer = 0 To dt.Rows.Count

      Try
        'Encabezados dependiendo
        If index = 3 Then
          Dim cellA3 As String = String.Format("A{0}", 1)
          wb.Worksheet(1).Cells(cellA3).Value = "Reporte de Clientes Del Periodo " & Format(Me.DtpFechaIni.Value)
          wb.Worksheet(1).Cells(cellA3).Style.Font.Bold = True

          Dim cellA0 As String = String.Format("A{0}", index)
          wb.Worksheet(1).Cells(cellA0).Value = "Clave"

          Dim cellB0 As String = String.Format("B{0}", index)
          wb.Worksheet(1).Cells(cellB0).Value = "Cliente"

          Dim cellC0 As String = String.Format("C{0}", index)
          wb.Worksheet(1).Cells(cellC0).Value = "Venta Día"

          Dim cellD0 As String = String.Format("D{0}", index)
          wb.Worksheet(1).Cells(cellD0).Value = "Acumulado"

          Dim cellE0 As String = String.Format("E{0}", index)
          wb.Worksheet(1).Cells(cellE0).Value = "Pron. Fin Mes ($)"

          Dim cellF0 As String = String.Format("F{0}", index)
          wb.Worksheet(1).Cells(cellF0).Value = "ClaveAgte."

          Dim cellG0 As String = String.Format("G{0}", index)
          wb.Worksheet(1).Cells(cellG0).Value = "Agente"

          Dim cellH0 As String = String.Format("H{0}", index)
          wb.Worksheet(1).Cells(cellH0).Value = "Clave Suc."

          Dim cellI0 As String = String.Format("I{0}", index)
          wb.Worksheet(1).Cells(cellI0).Value = "Sucursal"

          Dim cellJ0 As String = String.Format("J{0}", index)
          wb.Worksheet(1).Cells(cellJ0).Value = "Ruta"

          index = index + 1
        End If

        'Formato de cada una de las celdas
        Dim cellA As String = String.Format("A{0}", index)
        'wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "#,##0"

        Dim cellB As String = String.Format("B{0}", index)
        'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellC As String = String.Format("C{0}", index)
        wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellD As String = String.Format("D{0}", index)
        wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellE As String = String.Format("E{0}", index)
        wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellF As String = String.Format("F{0}", index)
        wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "#,##0"

        Dim cellG As String = String.Format("G{0}", index)
        'wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "0.00 %"

        Dim cellH As String = String.Format("H{0}", index)
        wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "#,##0"

        Dim cellI As String = String.Format("I{0}", index)
        'wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "$ #,##0.00"

        Dim cellJ As String = String.Format("J{0}", index)

      Catch ex As Exception
        MessageBox.Show(ex.ToString(), "Error al dar formato a celdas (Clientes): ")
      End Try

      index = index + 1
    Next

    ws.Columns("N").Delete()
    ws.Columns().Width = 15
    ws.Rows(6).Style.Alignment.WrapText = False

    Try
      Dim saveFileDialog1 As New SaveFileDialog()
      saveFileDialog1.Filter = "Excel|*.xlsx"
      saveFileDialog1.Title = "Save Excel File"
      saveFileDialog1.FileName = "ScoreCard (Clientes) al " & DtpFechaIni.Value.ToString("dd-MM-yyyy") & ".xlsx"
      saveFileDialog1.ShowDialog()
      saveFileDialog1.InitialDirectory = "C:/"

      If saveFileDialog1.FileName <> "" Then
        Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
        fs.Close()
      End If

      Dim strFileName As String = saveFileDialog1.FileName
      wb.SaveAs(strFileName)
      Process.Start(saveFileDialog1.FileName)
    Catch ex As Exception
      MessageBox.Show(ex.ToString(), "Error al guardar el archivo (Clientes): ")
    End Try
  End Sub

  Sub ExportarViejoClientes()
    '--------Generar Excel Clientes

    Dim oExcel As Object
    Dim oBook As Object
    Dim oSheet As Object

    Dim Rangos As String = ""
    Dim Rangos2 As String = ""

    'Abrimos un nuevo libro
    oExcel = CreateObject("Excel.Application")
    oBook = oExcel.workbooks.add
    oSheet = oBook.worksheets(1)


    'Declaramos el nombre de las columnas
    oSheet.range("A3").value = "CardCode"
    oSheet.range("B3").value = "CardName"
    oSheet.range("C3").value = "VtaDia"
    oSheet.range("D3").value = "Acumulado"
    oSheet.range("E3").value = "Pron Fin Mes"   'celda I3
    oSheet.range("F3").value = "SlpCode"    'celda L3
    oSheet.range("G3").value = "SlpName"    'celda M3
    oSheet.range("H3").value = "GroupCode"  'celda N3
    oSheet.range("I3").value = "GroupName"  'celda O3

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A3:I3").font.bold = True
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgClientes.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel0 As String = Me.DgClientes.Item(0, fila_dt).Value
      Dim cel1 As String = Me.DgClientes.Item(1, fila_dt).Value
      Dim cel2 As String = IIf(IsDBNull(Me.DgClientes.Item(2, fila_dt).Value), 0, Me.DgClientes.Item(2, fila_dt).Value)
      Dim cel3 As String = IIf(IsDBNull(Me.DgClientes.Item(3, fila_dt).Value), 0, Me.DgClientes.Item(3, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgClientes.Item(4, fila_dt).Value), 0, Me.DgClientes.Item(4, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgClientes.Item(5, fila_dt).Value), 0, Me.DgClientes.Item(5, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgClientes.Item(6, fila_dt).Value), 0, Me.DgClientes.Item(6, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgClientes.Item(7, fila_dt).Value), 0, Me.DgClientes.Item(7, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgClientes.Item(8, fila_dt).Value), 0, Me.DgClientes.Item(8, fila_dt).Value)

      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel0
      oSheet.range("B" & fila_dt_excel).value = cel1
      oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
      oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
      oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
      oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
      oSheet.range("G" & fila_dt_excel).value = cel6
      oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
      oSheet.range("I" & fila_dt_excel).value = cel8

    Next


    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:N").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Clientes del Agente  " & Format(Me.DtpFechaIni.Value)
    oSheet.range("C1").value = Rangos
    oSheet.range("C2").value = Rangos2
    'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") & " Al " & Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub


  'Ingresar objetivos
  Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
    Dim frm As New ScoreCardIngresarObjetivos()
    frm.Show()
  End Sub


  'Colores para el DataGrid

  Private Sub DgVtaAgte_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgVtaAgte.ColumnHeaderMouseClick
    Dim numfilas As Integer

    numfilas = DgVtaAgte.RowCount 'cuenta las filas del DataGrid

    'recorre las filas del DataGrid
    For i = 0 To (numfilas - 1)



      If DgVtaAgte.Item(6, i).Value < 0.85 Then
        DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.Red

      ElseIf DgVtaAgte.Item(6, i).Value >= 0.85 And DgVtaAgte.Item(6, i).Value < 1 Then
        DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.Yellow

      ElseIf DgVtaAgte.Item(6, i).Value >= 1 Then
        DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.LimeGreen

      End If

    Next

    For i = 0 To (numfilas - 1)



      If DgVtaAgte.Item(9, i).Value < 0.85 Then
        DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.Red

      ElseIf DgVtaAgte.Item(9, i).Value >= 0.85 And DgVtaAgte.Item(9, i).Value < 1 Then
        DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.Yellow

      ElseIf DgVtaAgte.Item(9, i).Value >= 1 Then
        DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.LimeGreen

      End If

    Next
  End Sub

  Private Sub DgVtaAgte_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgVtaAgte.CellEnter

  End Sub
End Class


'Para establecer mediante programación la celda actual
'Me.dataGridView1.CurrentCell = Me.dataGridView1(1, 0)