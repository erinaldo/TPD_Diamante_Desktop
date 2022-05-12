Imports System.Data
Imports System.Data.SqlClient

Public Class LineasHalcon

  'Conexiones a la Base de datos
  Public StrProd As String = conexion_universal.CadenaSBO_Diamante
  Public StrTpm As String = conexion_universal.CadenaSQL
  Public StrCon As String = conexion_universal.CadenaSQLSAP

  Dim DvTotales As New DataView
  Dim DvAgentes As New DataView
  Dim DvAgentes2 As New DataView
  Dim DvLineas As New DataView

  Dim SQL As New Comandos_SQL()


  Private Sub LineasHalcon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")


    Dim ConsutaListaS As String
    Dim ConsutaListaA As String

    '*********************************************************************************************************************************************
    'Codigo para obtener informacion de los agentes que cada usuario debe ver
    '*********************************************************************************************************************************************
    SQL.conectarTPM()

    Dim GroupCode As String
    Dim slpcode As String = SQL.CampoEspecifico("SELECT AgteVentas FROM Usuarios where Id_Usuario = '" + UsrTPM + "'", "AgteVentas")
    Dim CodAgte As String = SQL.CampoEspecifico("SELECT CodAgte FROM Usuarios where Id_Usuario = '" + UsrTPM + "'", "CodAgte")

    If slpcode = "" And UsrTPM <> "MANAGER" And UsrTPM <> "COMERCIAL" Then
      CerrarSCClientes = True
      MsgBox("Este usuario no tienen definido el valor de Agte Ventas en su registro", MsgBoxStyle.Exclamation, "Falta configuración de agente ventas")
      Exit Sub
    End If

  'If slpcode <> "" Then
  '  GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " + slpcode, "GroupCode")
  'Else
  '  GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " + CodAgte, "GroupCode")
  'End If

  If slpcode <> "" Then
   GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " & slpcode, "GroupCode")
   If GroupCode = False Then
    MsgBox("Verifique que este usuario este registrado en la tabla DEPCOBR de TPM", MsgBoxStyle.Exclamation, "Falta configuración en tabla")
    Exit Sub
   End If
  Else
   GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " & CodAgte, "GroupCode")
  End If

  Dim Almacen As String = SQL.CampoEspecifico("select Almacen from Usuarios where Id_Usuario = '" + UsrTPM + "'", "Almacen")

  Button3.Visible = False
  'COMBO DE SUCURSALES
  If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Then
   Button3.Visible = True
   ConsutaListaS = "SELECT GroupCode,GroupName FROM SBO_TPD.dbo.OCRG with (nolock) WHERE GroupType = 'C' ORDER BY GroupName"
  Else
   ConsutaListaS = "SELECT GroupCode,GroupName FROM SBO_TPD.dbo.OCRG with (nolock) WHERE GroupType = 'C' AND GroupCode = '" + GroupCode + "' ORDER BY GroupName"
    End If

    'COMBO DE AGENTES
    If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Then
      'SQL.LlenarComboBox1("SELECT T0.slpcode,T0.slpname FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 ORDER BY slpname", "slpcode,slpname", cmbAgteVta)
      ConsutaListaA = "SELECT DISTINCT T0.SlpCode,SlpName,T1.GroupCode FROM SBO_TPD.dbo.OSLP t0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') ORDER BY SlpName"

    ElseIf UsrTPM = "RROBLES" Or UsrTPM = "VVERGARA" Or UsrTPM = "VENTAS5" Then
      ConsutaListaA = "SELECT DISTINCT T0.SlpCode,SlpName,T1.GroupCode FROM SBO_TPD.dbo.OSLP t0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') AND Memo = '" + Almacen + "' AND T1.GroupCode = 102 ORDER BY slpname"
    Else
      'SI ES AGENETE DE MARKETING/VENTAS
      ConsutaListaA = "SELECT DISTINCT T0.SlpCode,SlpName,T1.GroupCode FROM SBO_TPD.dbo.OSLP t0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE U_VENTAS = '" + UsrTPM + "' AND (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') ORDER BY slpname"
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
    'DgVtaAgte.Rows.Clear()
    DgVtaAgte.DataSource = Nothing
    Buscar_NotasC()
  End Sub

  Sub Buscar_NotasC()
    Dim vDiasMes As Integer
    Dim vDiasTrans As Integer
    Dim fecha As String = Me.DtpFechaIni.Value.ToString("yyyy-MM-dd")
    Try
      SQL.conectarTPM()
      Dim mont As String = Month(DtpFechaIni.Value.ToString("dd-MM-yyyy"))
      Dim yea As String = Year(DtpFechaIni.Value.ToString("dd-MM-yyyy"))
      vDiasMes = SQL.CampoEspecifico("EXEC TPD_DiasHabiles " + mont + "," + yea, "Dias")
      txtDiasMes.Text = vDiasMes.ToString

      vDiasTrans = SQL.CampoEspecifico("EXEC Indicadores '" + DtpFechaIni.Value.ToString("yyyy-MM-dd") + "'," + "2", "diasTrans")
      txtDiasTranscurridos.Text = vDiasTrans.ToString

      txtDiasRestantes.Text = Convert.ToString(vDiasMes - vDiasTrans)
      txtAvanceOptimo.Text = Format(Convert.ToString((vDiasTrans / vDiasMes) * 100), "000.00")

      txtAvanceOptimo.Text = (vDiasTrans / vDiasMes).ToString("P1")

      Dim DsVtas As New DataSet
      'Consultas para llenado de grid's, consultas y comparaciones respecto al objetivo
      Dim conec = New SqlConnection(StrTpm)
      Dim cmd = New SqlCommand("TPD_ScoreCard_LH2", conec)
      'Dim cmd = New SqlCommand("TPD_ScoreCard_NuevasLineasHalcon", conec)
      cmd.CommandType = CommandType.StoredProcedure
      cmd.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = Me.DtpFechaIni.Value
      cmd.Parameters.Add("@DiasMes", SqlDbType.Int).Value = vDiasMes
      cmd.Parameters.Add("@DiasTrans", SqlDbType.Int).Value = vDiasTrans

      'Dim DiasRestantes As Int32
      'DiasRestantes = vDiasMes - vDiasTrans
      'If DiasRestantes = 0 Then
      '    DiasRestantes = 1
      'End If
      'cmd.Parameters.Add("@DiasRest", SqlDbType.Int).Value = DiasRestantes
      'cmd.Parameters.Add("@PorAvanOptimo", SqlDbType.Int).Value = 70.8 'txtAvanceOptimo.Text

      cmd.Parameters.Add("@agente", SqlDbType.Int).Value = CmbAgteVta.SelectedValue
      cmd.Parameters.Add("@Sucursal", SqlDbType.Int).Value = CmbSucursal.SelectedValue
      cmd.Parameters.Add("@AgenteVentas", SqlDbType.VarChar).Value = UsrTPM
      conec.Open()
      Dim adaptador = New SqlDataAdapter()
      adaptador.SelectCommand = cmd
      adaptador.SelectCommand.Connection = conec
      adaptador.SelectCommand.CommandTimeout = 10000
      cmd.ExecuteNonQuery()
      cmd.Connection.Close()
      conec.Close()
      adaptador.Fill(DsVtas)

      DsVtas.Tables(0).TableName = "Totales"
      DsVtas.Tables(1).TableName = "VtaAgtes"
      DsVtas.Tables(2).TableName = "VtaLinHal"

      DvTotales.Table = DsVtas.Tables("Totales")
      DvAgentes.Table = DsVtas.Tables("VtaAgtes")
      DvLineas.Table = DsVtas.Tables("VtaLinHal")

      DgTotales.DataSource = DvTotales
      EstiloDgTotales()
      DgVtaAgte.DataSource = DvAgentes
      EstiloDgVtaAgte()
      dgvVtaLinea.DataSource = DvLineas
      EstilodgvVtaLinea()

      SQL.Cerrar()
    Catch ex As Exception
      MessageBox.Show(ex.ToString, "Error al ejecutar los procedimientos almacenados")
    End Try


  End Sub

  'Estilos de los grid's total
  Sub EstiloDgTotales()
    With Me.DgTotales
      .ReadOnly = True
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .RowHeadersVisible = True
      .RowHeadersWidth = 25
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try

        .Columns("SlpCode").HeaderText = "Clave"
        .Columns("SlpCode").Width = 50
        .Columns("SlpCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("slpname").HeaderText = "Descripción"
        .Columns("slpname").Width = 90

        .Columns("groupcode").HeaderText = "Codigo Sucursal"
        .Columns("groupcode").Width = 80
        .Columns("groupcode").DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns("groupcode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("groupcode").Visible = False

        .Columns("groupname").HeaderText = "Nombre Sucursal"
        .Columns("groupname").Width = 85
        .Columns("groupname").DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns("groupname").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("groupname").Visible = False

        .Columns("Objetivo").HeaderText = "Objetivo"
        .Columns("Objetivo").Width = 85
        .Columns("Objetivo").DefaultCellStyle.Format = "$ ###,###,##0"
        .Columns("Objetivo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("venta").HeaderText = "Acumulado"
        .Columns("venta").Width = 105
        .Columns("venta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("venta").DefaultCellStyle.Format = " $ ###,###,##0.#0"



        .Columns("PorcAcumVsObj").HeaderText = "Acum Vs Obj (%)"
        .Columns("PorcAcumVsObj").Width = 90
        .Columns("PorcAcumVsObj").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("PorcAcumVsObj").DefaultCellStyle.Format = "##0.#0 %"



        Dim numfilas As Integer
        Dim Porcent As Decimal
        numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)

          If DgTotales.Item(6, i).Value Is DBNull.Value Then
            DgTotales.Item(6, i).Value = 0
            Porcent = 0
          Else
            Porcent = DgTotales.Item(6, i).Value
          End If


          If Porcent < 0.85 Then
            DgTotales.Rows(i).Cells(6).Style.BackColor = Color.Red

          ElseIf Porcent >= 0.85 And Porcent < 1 Then
            DgTotales.Rows(i).Cells(6).Style.BackColor = Color.Yellow

          ElseIf Porcent >= 1 Then
            DgTotales.Rows(i).Cells(6).Style.BackColor = Color.LimeGreen

          End If

        Next

        .Columns("porvender").HeaderText = "Vta mínima requerida a la fecha."
        .Columns("porvender").Width = 100
        .Columns("porvender").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("porvender").DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns("PronFinMes").HeaderText = "Pronostico Fin Mes ($)"
        .Columns("PronFinMes").Width = 90
        .Columns("PronFinMes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("PronFinMes").DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns("PorPronFinMes").HeaderText = "Pronostico Fin Mes (%)"
        .Columns("PorPronFinMes").Width = 100
        .Columns("PorPronFinMes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("PorPronFinMes").DefaultCellStyle.Format = "#,##0.#0 %"

        'numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)

          If DgTotales.Item(9, i).Value Is DBNull.Value Then
            DgTotales.Item(9, i).Value = 0
            Porcent = 0
          Else
            Porcent = DgTotales.Item(9, i).Value
          End If

          If Porcent < 0.85 Then
            DgTotales.Rows(i).Cells(9).Style.BackColor = Color.Red
          ElseIf Porcent >= 0.85 And Porcent < 1 Then
            DgTotales.Rows(i).Cells(9).Style.BackColor = Color.Yellow

          ElseIf Porcent >= 1 Then
            DgTotales.Rows(i).Cells(9).Style.BackColor = Color.LimeGreen
          End If

        Next

      Catch ex As Exception
        'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView")
      End Try
    End With
  End Sub
  'Estilos de los grid's agentes
  Sub EstiloDgVtaAgte()
    With Me.DgVtaAgte
      .ReadOnly = True
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .RowHeadersVisible = True
      .RowHeadersWidth = 25
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try

        .Columns("SlpCode").HeaderText = "Clave"
        .Columns("SlpCode").Width = 50
        .Columns("SlpCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("slpname").HeaderText = "Agente"
        .Columns("slpname").Width = 180

        .Columns("groupcode").HeaderText = "Codigo Sucursal"
        .Columns("groupcode").Width = 80
        .Columns("groupcode").DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns("groupcode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("groupcode").Visible = False

        .Columns("groupname").HeaderText = "Nombre Sucursal"
        .Columns("groupname").Width = 85
        .Columns("groupname").DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns("groupname").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("groupname").Visible = False

        .Columns("Objetivo").HeaderText = "Objetivo"
        .Columns("Objetivo").Width = 85
        .Columns("Objetivo").DefaultCellStyle.Format = "$ ###,###,##0"
        .Columns("Objetivo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("venta").HeaderText = "Acumulado"
        .Columns("venta").Width = 105
        .Columns("venta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("venta").DefaultCellStyle.Format = " $ ###,###,##0.#0"

        .Columns("PorcAcumVsObj").HeaderText = "Acum Vs Obj (%)"
        .Columns("PorcAcumVsObj").Width = 90
        .Columns("PorcAcumVsObj").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("PorcAcumVsObj").DefaultCellStyle.Format = "##0.#0 %"

        Dim numfilas As Integer
        Dim Porcent As Decimal
        numfilas = DgVtaAgte.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)

          If DgVtaAgte.Item(6, i).Value Is DBNull.Value Then
            DgVtaAgte.Item(6, i).Value = 0
            Porcent = 0
          Else
            Porcent = DgVtaAgte.Item(6, i).Value
          End If


          If Porcent < 0.85 Then
            DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.Red

          ElseIf Porcent >= 0.85 And Porcent < 1 Then
            DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.Yellow

          ElseIf Porcent >= 1 Then
            DgVtaAgte.Rows(i).Cells(6).Style.BackColor = Color.LimeGreen

          End If

        Next

        .Columns("porvender").HeaderText = "Vta mínima requerida a la fecha."
        .Columns("porvender").Width = 100
        .Columns("porvender").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("porvender").DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns("PronFinMes").HeaderText = "Pronostico Fin Mes ($)"
        .Columns("PronFinMes").Width = 90
        .Columns("PronFinMes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("PronFinMes").DefaultCellStyle.Format = "$ ###,###,##0.#0"

        .Columns("PorPronFinMes").HeaderText = "Pronostico Fin Mes (%)"
        .Columns("PorPronFinMes").Width = 100
        .Columns("PorPronFinMes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("PorPronFinMes").DefaultCellStyle.Format = "#,##0.#0 %"

        'numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)

          If DgVtaAgte.Item(9, i).Value Is DBNull.Value Then
            DgVtaAgte.Item(9, i).Value = 0
            Porcent = 0
          Else
            Porcent = DgVtaAgte.Item(9, i).Value
          End If

          If Porcent < 0.85 Then
            DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.Red
          ElseIf Porcent >= 0.85 And Porcent < 1 Then
            DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.Yellow

          ElseIf Porcent >= 1 Then
            DgVtaAgte.Rows(i).Cells(9).Style.BackColor = Color.LimeGreen
          End If

        Next

      Catch ex As Exception
        'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView")
      End Try
    End With
  End Sub
  'Venta por línea
  Sub EstilodgvVtaLinea()
    With Me.dgvVtaLinea
      .ReadOnly = True
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .RowHeadersVisible = True
      .RowHeadersWidth = 25
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try

        .Columns("SlpCode").HeaderText = "Clave"
        .Columns("SlpCode").Width = 100
        .Columns("SlpCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("SlpCode").Visible = True

        .Columns("slpname").HeaderText = "Agente"
        .Columns("slpname").Width = 180

        .Columns("groupcode").HeaderText = "Codigo Sucursal"
        .Columns("groupcode").Width = 140
        .Columns("groupcode").Visible = False

        .Columns("groupname").HeaderText = "Nombre Sucursal"
        .Columns("groupname").Width = 140
        .Columns("groupname").Visible = False

        .Columns("Importe").HeaderText = "Acumulado"
        .Columns("Importe").Width = 180
        .Columns("Importe").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("Importe").DefaultCellStyle.Format = " $ ###,###,##0.#0"

        .Columns("ItmsGrpCod").HeaderText = "Código Linea Halcon"
        .Columns("ItmsGrpCod").Width = 180

        .Columns("ItmsGrpCod").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("ItmsGrpNam").HeaderText = "Linea Halcon"
        .Columns("ItmsGrpNam").Width = 180
        .Columns("ItmsGrpNam").DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns("ItmsGrpNam").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("tipo").HeaderText = "Tipo DISA/DIAMANTE"
        .Columns("tipo").Width = 180
        .Columns("tipo").Visible = False

      Catch ex As Exception
        'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView")
      End Try
    End With
  End Sub

  Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
    Dim frm As New LHIngObj()
    frm.Show()
  End Sub


  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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
    oSheet.range("C3").value = "Objetivo"
    oSheet.range("D3").value = "Acumulado"
    oSheet.range("E3").value = "Acum Vs Obj (%)"
    oSheet.range("F3").value = "Vta mínima requerida a la fecha"
    oSheet.range("G3").value = "Pronóstico fin mes ($)"
    oSheet.range("H3").value = "Pronóstico fin mes (%)"

    'para poner la primera fila de los titulos en negrita
    oSheet.range("C3:H3").font.bold = True
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgTotales.RowCount
    For fila_dt = 0 To total_reg - 1
      'para leer una celda en concreto
      'el numero es la columna
      'Dim cel0 As String = Me.DgTotales.Item(0, fila_dt).Value
      'Dim cel1 As String = Me.DgTotales.Item(1, fila_dt).Value
      Dim cel0 As String = IIf(IsDBNull(Me.DgTotales.Item(4, fila_dt).Value), 0, Me.DgTotales.Item(4, fila_dt).Value)
      Dim cel1 As String = IIf(IsDBNull(Me.DgTotales.Item(5, fila_dt).Value), 0, Me.DgTotales.Item(5, fila_dt).Value)
      Dim cel2 As String = IIf(IsDBNull(Me.DgTotales.Item(6, fila_dt).Value), 0, Me.DgTotales.Item(6, fila_dt).Value * 100)
      Dim cel3 As String = IIf(IsDBNull(Me.DgTotales.Item(7, fila_dt).Value), 0, Me.DgTotales.Item(7, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgTotales.Item(8, fila_dt).Value), 0, Me.DgTotales.Item(8, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgTotales.Item(9, fila_dt).Value), 0, Me.DgTotales.Item(9, fila_dt).Value * 100)

      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel0, 0)
      oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel1, 1)
      oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel2, 2)
      oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel3, 2)
      oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel4, 2)
      oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel5, 2)
    Next

    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:F").entirecolumn.autofit()

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
      oSheet.range("A1").value = "Reporte de Ventas TOTALES de todos los AGENTES con FECHA " + Format(Me.DtpFechaIni.Value)
    Else
      oSheet.range("A1").value = "Reporte de Ventas del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
    End If


    oSheet.range("C1").value = Rangos
    oSheet.range("C2").value = Rangos2
    'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing

    'Dim oExcel As Object
    'Dim oBook As Object
    'Dim oSheet As Object

    'Dim Rangos As String = ""
    'Dim Rangos2 As String = ""

    ''MsgBox("El reporte se creara a continuación")

    ''Abrimos un nuevo libro
    'oExcel = CreateObject("Excel.Application")
    'oBook = oExcel.workbooks.add
    'oSheet = oBook.worksheets(1)


    ''Declaramos el nombre de las columnas
    'oSheet.range("A3").value = "Objetivo"
    'oSheet.range("B3").value = "Venta"
    'oSheet.range("C3").value = "VentaVsCuota"
    'oSheet.range("D3").value = "PorVender"
    'oSheet.range("E3").value = "Tendencia fin mes ($)"
    'oSheet.range("F3").value = "Tendencia fin mes (%)"


    ''para poner la primera fila de los titulos en negrita
    'oSheet.range("A3:F3").font.bold = True
    'Dim fila_dt As Integer = 0
    'Dim fila_dt_excel As Integer = 0
    'Dim tanto_porcentaje As String = ""
    'Dim marikona As Integer = 0

    'Dim total_reg As Integer = 0

    'total_reg = DgTotales.RowCount
    'For fila_dt = 0 To total_reg - 1

    '    'para leer una celda en concreto
    '    'el numero es la columna
    '    'Dim cel0 As String = Me.DgTotales.Item(0, fila_dt).Value
    '    'Dim cel1 As String = Me.DgTotales.Item(1, fila_dt).Value
    '    Dim cel0 As String = IIf(IsDBNull(Me.DgTotales.Item(0, fila_dt).Value), 0, Me.DgTotales.Item(0, fila_dt).Value)
    '    Dim cel1 As String = IIf(IsDBNull(Me.DgTotales.Item(1, fila_dt).Value), 0, Me.DgTotales.Item(1, fila_dt).Value)
    '    Dim cel2 As String = IIf(IsDBNull(Me.DgTotales.Item(2, fila_dt).Value), 0, Me.DgTotales.Item(2, fila_dt).Value)
    '    Dim cel3 As String = IIf(IsDBNull(Me.DgTotales.Item(3, fila_dt).Value), 0, Me.DgTotales.Item(3, fila_dt).Value)
    '    Dim cel4 As String = IIf(IsDBNull(Me.DgTotales.Item(4, fila_dt).Value), 0, Me.DgTotales.Item(4, fila_dt).Value)
    '    Dim cel5 As String = IIf(IsDBNull(Me.DgTotales.Item(5, fila_dt).Value), 0, Me.DgTotales.Item(5, fila_dt).Value)

    '    fila_dt_excel = fila_dt + 4

    '    'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
    '    oSheet.range("A" & fila_dt_excel).value = FormatNumber(cel0, 0)
    '    oSheet.range("B" & fila_dt_excel).value = Format(cel1, 1)
    '    oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
    '    oSheet.range("D" & fila_dt_excel).value = Format(cel3, 2)
    '    oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
    '    oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
    'Next

    '' para que el tamano de la columna tenga como minimo el maximo de sus textos
    'oSheet.columns("A:F").entirecolumn.autofit()

    ''ENCABEZADO DEL REPORTE GENERADO

    'Dim sqlConnection1 As New SqlConnection(conexion_universal.CadenaSQLSAP)
    'Dim cmd As New SqlCommand
    'Dim returnValue As Object

    'cmd.CommandText = "SELECT slpname from oslp where slpcode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString)
    'cmd.CommandType = CommandType.Text
    'cmd.Connection = sqlConnection1

    'sqlConnection1.Open()

    'returnValue = cmd.ExecuteScalar()

    'sqlConnection1.Close()

    'Dim cnn As SqlConnection = Nothing


    'If CmbAgteVta.SelectedValue = 999 Then
    '    oSheet.range("A1").value = "Reporte de Ventas TOTALES de todos los AGENTES con FECHA " + Format(Me.DtpFechaIni.Value)
    'Else
    '    oSheet.range("A1").value = "Reporte de Ventas del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
    'End If


    'oSheet.range("C1").value = Rangos
    'oSheet.range("C2").value = Rangos2
    ''Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

    'oExcel.visible = True
    'System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    'GC.Collect()
    'oSheet = Nothing
    'oBook = Nothing
    'oExcel = Nothing
  End Sub

  'Private Sub DgVtaAgte_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgVtaAgte.CellClick
  '    Dim frm As New LineasHalconDetalle()
  '    frm.Show()
  'End Sub

  Private Sub DgVtaAgte_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgVtaAgte.CellDoubleClick
    Dim frm As New LineasHalconDetalle()
    frm.Show()
  End Sub

  Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
    Dim Titulo As String

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
      Titulo = "Reporte de Ventas TOTALES de todos los AGENTES con FECHA " + Format(Me.DtpFechaIni.Value)
    Else
      Titulo = "Reporte de Ventas del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
    End If

    Dim Columnas As String() = {"Clave",
                                "Agente",
                                "Codigo Sucursal",
                                "Sucursal",
                                "Objetivo",
                                "Acumulado",
                                "Acum Vs Obj (%)",
                                "Vta mínima requerida a la fecha",
                                "Pronóstico fin mes ($)",
                                "Pronóstico fin mes (%)"}
    Dim TipoColumna As TipoDeDato() = {TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Porecentaje,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Porecentaje}
    Dim Visible As Boolean() = {True,
                                True,
                                False,
                                False,
                                True,
                                True,
                                True,
                                True,
                                True,
                                True}

    funciones.exporta2Excel(Titulo, Columnas, TipoColumna, Visible, DgVtaAgte)


    'Dim oExcel As Object
    'Dim oBook As Object
    'Dim oSheet As Object

    'Dim Rangos As String = ""
    'Dim Rangos2 As String = ""

    ''MsgBox("El reporte se creara a continuación")

    ''Abrimos un nuevo libro
    'oExcel = CreateObject("Excel.Application")
    'oBook = oExcel.workbooks.add
    'oSheet = oBook.worksheets(1)


    ''Declaramos el nombre de las columna
    'oSheet.range("A3").value = "Clave"
    'oSheet.range("B3").value = "Agente"
    'oSheet.range("C3").value = "Objetivo"
    'oSheet.range("D3").value = "Acumulado"
    'oSheet.range("E3").value = "Acum Vs Obj (%)"
    'oSheet.range("F3").value = "Vta mínima requerida a la fecha"
    'oSheet.range("G3").value = "Pronóstico fin mes ($)"
    'oSheet.range("H3").value = "Pronóstico fin mes (%)"


    ''para poner la primera fila de los titulos en negrita
    'oSheet.range("A3:H3").font.bold = True
    'Dim fila_dt As Integer = 0
    'Dim fila_dt_excel As Integer = 0
    'Dim tanto_porcentaje As String = ""
    'Dim marikona As Integer = 0

    'Dim total_reg As Integer = 0

    'total_reg = DgVtaAgte.RowCount
    'For fila_dt = 0 To total_reg - 1

    '  'para leer una celda en concreto
    '  'el numero es la columna
    '  'Dim cel0 As String = Me.DgVtaAgte.Item(0, fila_dt).Value
    '  'Dim cel1 As String = Me.DgVtaAgte.Item(1, fila_dt).Value
    '  Dim cel0 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(0, fila_dt).Value), 0, Me.DgVtaAgte.Item(0, fila_dt).Value) ' Clave
    '  Dim cel1 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(1, fila_dt).Value), 0, Me.DgVtaAgte.Item(1, fila_dt).Value) ' Agente
    '  Dim cel2 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(4, fila_dt).Value), 0, Me.DgVtaAgte.Item(4, fila_dt).Value) ' Objetivo
    '  Dim cel3 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(5, fila_dt).Value), 0, Me.DgVtaAgte.Item(5, fila_dt).Value) ' Acumulado
    '  Dim cel4 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(6, fila_dt).Value), 0, Me.DgVtaAgte.Item(6, fila_dt).Value * 100) ' Acum Vs Obj (%)
    '  Dim cel5 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(7, fila_dt).Value), 0, Me.DgVtaAgte.Item(7, fila_dt).Value) ' Vta mínima requerida a la fecha
    '  Dim cel6 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(8, fila_dt).Value), 0, Me.DgVtaAgte.Item(8, fila_dt).Value) ' Pronóstico fin mes ($)
    '  Dim cel7 As String = IIf(IsDBNull(Me.DgVtaAgte.Item(9, fila_dt).Value), 0, Me.DgVtaAgte.Item(9, fila_dt).Value * 100) ' Pronóstico fin mes (%)

    '  fila_dt_excel = fila_dt + 4

    '  'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
    '  oSheet.range("A" & fila_dt_excel).value = cel0
    '  oSheet.range("B" & fila_dt_excel).value = cel1
    '  oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
    '  oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
    '  oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
    '  oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
    '  oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 2)
    '  oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
    'Next

    '' para que el tamano de la columna tenga como minimo el maximo de sus textos
    'oSheet.columns("A:H").entirecolumn.autofit()

    ''ENCABEZADO DEL REPORTE GENERADO

    'Dim sqlConnection1 As New SqlConnection(conexion_universal.CadenaSQLSAP)
    'Dim cmd As New SqlCommand
    'Dim returnValue As Object

    'cmd.CommandText = "SELECT slpname from oslp where slpcode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString)
    'cmd.CommandType = CommandType.Text
    'cmd.Connection = sqlConnection1

    'sqlConnection1.Open()

    'returnValue = cmd.ExecuteScalar()

    'sqlConnection1.Close()

    'Dim cnn As SqlConnection = Nothing


    'If CmbAgteVta.SelectedValue = 999 Then
    '  oSheet.range("A1").value = "Reporte de Ventas TOTALES de todos los AGENTES con FECHA " + Format(Me.DtpFechaIni.Value)
    'Else
    '  oSheet.range("A1").value = "Reporte de Ventas del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
    'End If


    'oSheet.range("C1").value = Rangos
    'oSheet.range("C2").value = Rangos2
    ''Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

    'oExcel.visible = True
    'System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    'GC.Collect()
    'oSheet = Nothing
    'oBook = Nothing
    'oExcel = Nothing
  End Sub

  Private Sub Button5_Click(sender As Object, e As EventArgs)

  End Sub

  'Private Sub DgVtaAgte_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgVtaAgte.CurrentCellChanged
  '    Try
  '        If DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value.ToString = "999" Then
  '            DvLineas.RowFilter = "slpcodeT =" & DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value.ToString
  '            EstilodgvVtaLinea()
  '        Else
  '            DvLineas.RowFilter = "slpcode =" & DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value.ToString
  '            EstilodgvVtaLinea()
  '        End If
  '    Catch ex As Exception

  '    End Try


  'End Sub

  Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
    Dim Titulo As String

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
      Titulo = "Reporte de Ventas TOTALES de todos los AGENTES con FECHA " + Format(Me.DtpFechaIni.Value)
    Else
      Titulo = "Reporte de Ventas del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
    End If

    Dim Columnas As String() = {"Clave",
                                "Agente",
                                "Codigo Sucursal",
                                "Sucursal",
                                "Acumulado",
                                "Código Linea Halcon",
                                "Linea Halcon",
                                "Tipo DISA/DIAMANTE"}
    Dim TipoColumna As TipoDeDato() = {TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena}
    Dim Visible As Boolean() = {True,
                                True,
                                False,
                                False,
                                True,
                                True,
                                True,
                                False}

    funciones.exporta2Excel(Titulo, Columnas, TipoColumna, Visible, dgvVtaLinea)



    'Dim oExcel As Object
    'Dim oBook As Object
    'Dim oSheet As Object

    'Dim Rangos As String = ""
    'Dim Rangos2 As String = ""

    ''MsgBox("El reporte se creara a continuación")

    ''Abrimos un nuevo libro
    'oExcel = CreateObject("Excel.Application")
    'oBook = oExcel.workbooks.add
    'oSheet = oBook.worksheets(1)


    ''Declaramos el nombre de las columnas

    'oSheet.range("C3").value = "Clave"
    'oSheet.range("D3").value = "Agente"
    'oSheet.range("E3").value = "Acumulado"
    'oSheet.range("F3").value = "Código línea"
    'oSheet.range("G3").value = "Línea"



    ''para poner la primera fila de los titulos en negrita
    'oSheet.range("A3:H3").font.bold = True
    'Dim fila_dt As Integer = 0
    'Dim fila_dt_excel As Integer = 0
    'Dim tanto_porcentaje As String = ""
    'Dim marikona As Integer = 0

    'Dim total_reg As Integer = 0

    'total_reg = dgvVtaLinea.RowCount
    'For fila_dt = 0 To total_reg - 1

    '  'para leer una celda en concreto
    '  'el numero es la columna
    '  'Dim cel0 As String = Me.DgVtaAgte.Item(0, fila_dt).Value
    '  'Dim cel1 As String = Me.DgVtaAgte.Item(1, fila_dt).Value
    '  Dim cel0 As String = IIf(IsDBNull(Me.dgvVtaLinea.Item(0, fila_dt).Value), 0, Me.dgvVtaLinea.Item(0, fila_dt).Value)
    '  Dim cel1 As String = IIf(IsDBNull(Me.dgvVtaLinea.Item(1, fila_dt).Value), 0, Me.dgvVtaLinea.Item(1, fila_dt).Value)
    '  Dim cel2 As String = IIf(IsDBNull(Me.dgvVtaLinea.Item(4, fila_dt).Value), 0, Me.dgvVtaLinea.Item(4, fila_dt).Value)
    '  Dim cel3 As String = IIf(IsDBNull(Me.dgvVtaLinea.Item(5, fila_dt).Value), 0, Me.dgvVtaLinea.Item(5, fila_dt).Value)
    '  Dim cel4 As String = IIf(IsDBNull(Me.dgvVtaLinea.Item(6, fila_dt).Value), 0, Me.dgvVtaLinea.Item(6, fila_dt).Value)


    '  fila_dt_excel = fila_dt + 4

    '  'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente

    '  oSheet.range("C" & fila_dt_excel).value = cel0
    '  oSheet.range("D" & fila_dt_excel).value = cel1
    '  oSheet.range("E" & fila_dt_excel).value = cel2
    '  oSheet.range("F" & fila_dt_excel).value = cel3
    '  oSheet.range("G" & fila_dt_excel).value = cel4

    'Next

    '' para que el tamano de la columna tenga como minimo el maximo de sus textos
    'oSheet.columns("A:H").entirecolumn.autofit()

    ''ENCABEZADO DEL REPORTE GENERADO

    'Dim sqlConnection1 As New SqlConnection(conexion_universal.CadenaSQLSAP)
    'Dim cmd As New SqlCommand
    'Dim returnValue As Object

    'cmd.CommandText = "SELECT slpname from oslp where slpcode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString)
    'cmd.CommandType = CommandType.Text
    'cmd.Connection = sqlConnection1

    'sqlConnection1.Open()

    'returnValue = cmd.ExecuteScalar()

    'sqlConnection1.Close()

    'Dim cnn As SqlConnection = Nothing


    'If CmbAgteVta.SelectedValue = 999 Then
    '  oSheet.range("A1").value = "Reporte de Ventas TOTALES de todos los AGENTES con FECHA " + Format(Me.DtpFechaIni.Value)
    'Else
    '  oSheet.range("A1").value = "Reporte de Ventas del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
    'End If


    'oSheet.range("C1").value = Rangos
    'oSheet.range("C2").value = Rangos2
    ''Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

    'oExcel.visible = True
    'System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    'GC.Collect()
    'oSheet = Nothing
    'oBook = Nothing
    'oExcel = Nothing
  End Sub

  Private Sub DgVtaAgte_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgVtaAgte.CellEnter
    Try
      If DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value.ToString = "999" Then
        If DgVtaAgte.Item(1, DgVtaAgte.CurrentRow.Index).Value.ToString = "TODOS" Then
          DvLineas.RowFilter = "tipo <> ''"
        ElseIf DgVtaAgte.Item(1, DgVtaAgte.CurrentRow.Index).Value.ToString = "DISA" Then
          DvLineas.RowFilter = "tipo = 'DISA'"
        ElseIf DgVtaAgte.Item(1, DgVtaAgte.CurrentRow.Index).Value.ToString = "DIAMANTE" Then
          DvLineas.RowFilter = "tipo = 'DIAMANTE'"
        End If
      Else
        DvLineas.RowFilter = "slpcode =" & DgVtaAgte.Item(0, DgVtaAgte.CurrentRow.Index).Value.ToString
      End If

      EstilodgvVtaLinea()
    Catch ex As Exception

    End Try
  End Sub
End Class