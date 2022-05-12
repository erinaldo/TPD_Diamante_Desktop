Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel

Public Class ScoreCardCliente
  'Conexiones a la Base de datos
  Public StrProd As String = conexion_universal.CadenaSBO_Diamante
  Public StrTpm As String = conexion_universal.CadenaSQL
  Public StrCon As String = conexion_universal.CadenaSQLSAP

  Dim DvGTotal As New DataView
  Dim DvTotales As New DataView
  Dim DvAgentes As New DataView
  Dim DvAgentes2 As New DataView
  Dim DvLineas As New DataView

  Dim SQL As New Comandos_SQL()

  Private Sub SCClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    'iniciar
  End Sub

  Private Sub iniciar()
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
  ' GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " + slpcode, "GroupCode")
  'Else
  ' GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " + CodAgte, "GroupCode")
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

      'If CmbAgteVta.Items.Count = 0 Then
      '  If slpcode <> "" Then
      '    ConsutaListaA = "SELECT SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE SlpCode = " + slpcode + " ORDER BY slpname"
      '  Else
      '    ConsutaListaA = "select SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE SlpCode = " + CodAgte + " ORDER BY slpname"
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

  'Sub LlenarCombos()
  '  'LLENA COMBOBOX cmbSucursal
  '  SQL.conectarTPM()
  '  Dim GroupCode As String
  '  Dim slpcode As String = SQL.CampoEspecifico("SELECT AgteVentas FROM Usuarios where Id_Usuario = '" + UsrTPM + "'", "AgteVentas")
  '  Dim CodAgte As String = SQL.CampoEspecifico("SELECT CodAgte FROM Usuarios where Id_Usuario = '" + UsrTPM + "'", "CodAgte")

  '  If slpcode <> "" Then
  '    GroupCode = SQL.CampoEspecifico("SELECT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " + slpcode, "GroupCode")
  '  Else
  '    GroupCode = SQL.CampoEspecifico("SELECT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " + CodAgte, "GroupCode")
  '  End If

  '  Dim Almacen As String = SQL.CampoEspecifico("select Almacen from Usuarios where Id_Usuario = '" + UsrTPM + "'", "Almacen")

  '  'COMBO DE SUCURSALES
  '  If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Then
  '    SQL.LlenarComboBox1("SELECT GroupCode,GroupName FROM SBO_TPD.dbo.OCRG with (nolock) WHERE GroupType = 'C' ORDER BY GroupName", "GroupCode,GroupName", CmbSucursal)
  '  Else
  '    SQL.LlenarComboBox1("SELECT GroupCode,GroupName FROM SBO_TPD.dbo.OCRG with (nolock) WHERE GroupType = 'C' AND GroupCode = '" + GroupCode + "' ORDER BY GroupName", "GroupCode,GroupName", CmbSucursal)
  '  End If

  '  'COMBO DE AGENTES
  '  If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Then
  '    'SQL.LlenarComboBox1("SELECT T0.slpcode,T0.slpname FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 ORDER BY slpname", "slpcode,slpname", cmbAgteVta)
  '    SQL.LlenarComboBox1("select SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') ORDER BY SlpName", "slpcode,slpname", CmbAgteVta)

  '  ElseIf UsrTPM = "RROBLES" Then

  '    If slpcode <> "" Then
  '      'SQL.LlenarComboBox1("SELECT T0.slpcode,T0.slpname FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 AND T0.SlpCode = " + slpcode + " ORDER BY slpname", "slpcode,slpname", cmbAgteVta)
  '      SQL.LlenarComboBox1("SELECT SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') AND Memo = '" + Almacen + "' ORDER BY slpname", "slpcode,slpname", CmbAgteVta)
  '    Else
  '      SQL.LlenarComboBox1("select SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') AND Memo = '" + Almacen + "' ORDER BY slpname", "slpcode,slpname", CmbAgteVta)
  '    End If

  '  ElseIf UsrTPM = "VVERGARA" Then

  '    If slpcode <> "" Then
  '      'SQL.LlenarComboBox1("SELECT T0.slpcode,T0.slpname FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 AND T0.SlpCode = " + slpcode + " ORDER BY slpname", "slpcode,slpname", cmbAgteVta)
  '      SQL.LlenarComboBox1("SELECT SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') AND Memo = '" + Almacen + "' ORDER BY slpname", "slpcode,slpname", CmbAgteVta)
  '    Else
  '      SQL.LlenarComboBox1("select SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') AND Memo = '" + Almacen + "' ORDER BY slpname", "slpcode,slpname", CmbAgteVta)
  '    End If

  '  Else
  '    'SI ES AGENETE DE MARKETING/VENTAS
  '    SQL.LlenarComboBox1("select SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE U_VENTAS = '" + UsrTPM + "' AND (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') ORDER BY slpname", "slpcode,slpname", CmbAgteVta)
  '    If CmbAgteVta.Items.Count = 0 Then
  '      If slpcode <> "" Then
  '        'SQL.LlenarComboBox1("SELECT T0.slpcode,T0.slpname FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 AND T0.SlpCode = " + slpcode + " ORDER BY slpname", "slpcode,slpname", cmbAgteVta)
  '        SQL.LlenarComboBox1("SELECT SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE SlpCode = " + slpcode + " ORDER BY slpname", "slpcode,slpname", CmbAgteVta)
  '      Else
  '        SQL.LlenarComboBox1("select SlpCode,SlpName from SBO_TPD.dbo.OSLP WHERE SlpCode = " + CodAgte + " ORDER BY slpname", "slpcode,slpname", CmbAgteVta)
  '      End If
  '    End If


  '  End If

  '  SQL.Cerrar()
  'End Sub


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
    DgPorCliente.DataSource = Nothing
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
      Dim DsGTotal As New DataSet

      Dim conec = New SqlConnection(StrTpm)
      Dim cmd = New SqlCommand("[TPD_ScoreCard_Clientes]", conec)
      cmd.CommandType = CommandType.StoredProcedure
      cmd.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = Me.DtpFechaIni.Value
      cmd.Parameters.Add("@DiasMes", SqlDbType.Int).Value = vDiasMes
      cmd.Parameters.Add("@DiasTrans", SqlDbType.Int).Value = vDiasTrans
      cmd.Parameters.Add("@agente", SqlDbType.Int).Value = CmbAgteVta.SelectedValue
      cmd.Parameters.Add("@Sucursal", SqlDbType.Int).Value = CmbSucursal.SelectedValue
      cmd.Parameters.Add("@DiasRest", SqlDbType.Int).Value = vDiasMes - vDiasTrans
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

      DsVtas.Tables(0).TableName = "GranTotal"
      DsVtas.Tables(1).TableName = "Totales"
      DsVtas.Tables(2).TableName = "VtaAgtes"

      DvGTotal.Table = DsVtas.Tables("GranTotal")
      DvTotales.Table = DsVtas.Tables("Totales")
      DvAgentes.Table = DsVtas.Tables("VtaAgtes")


      DgGTotal.DataSource = DvGTotal
      EstiloDgGTotal()

      DgTotales.DataSource = DvTotales
      EstiloDgTotales()

      DgPorCliente.DataSource = DvAgentes
      EstiloDgPorCte()

      SQL.Cerrar()
    Catch ex As Exception
      MessageBox.Show(ex.ToString, "Error al ejecutar los procedimientos almacenados")
    End Try
  End Sub

  '************************************************************************
  'INICIAN ESTILOS
  '************************************************************************
  Sub EstiloDgGTotal()
    With Me.DgGTotal
      .ReadOnly = True
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .RowHeadersVisible = True
            .RowHeadersWidth = 25
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try

        .Columns("CveAgt").HeaderText = "Clave"
        .Columns("CveAgt").Width = 50
        .Columns("CveAgt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("Agente").HeaderText = "Agente"
                .Columns("Agente").Width = 250

                .Columns("Acumulado").HeaderText = "Acumulado"
        .Columns("Acumulado").Width = 105
        .Columns("Acumulado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("Acumulado").DefaultCellStyle.Format = " ###,##0"

        .Columns("objetivo").HeaderText = "Objetivo"
        .Columns("objetivo").Width = 105
        .Columns("objetivo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("objetivo").DefaultCellStyle.Format = " ###,##0"

                .Columns("Vs").HeaderText = "Acum Vs Obj (%)"
                .Columns("Vs").Width = 90
                .Columns("Vs").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns("Vs").DefaultCellStyle.Format = "#0.#0 %"

                Dim numfilas As Integer
                Dim Porcent As Decimal
                numfilas = DgGTotal.RowCount 'cuenta las filas del DataGrid

                'recorre las filas del DataGrid
                For i = 0 To (numfilas - 1)
                    If DgGTotal.Item(4, i).Value Is DBNull.Value Then
                        DgGTotal.Item(4, i).Value = 0
                        Porcent = 0
                    Else
                        Porcent = DgGTotal.Item(4, i).Value
                    End If


                    If Porcent < 0.85 Then
                        DgGTotal.Rows(i).Cells(4).Style.BackColor = Color.Orange

                    ElseIf Porcent >= 0.85 And Porcent < 1 Then
                        DgGTotal.Rows(i).Cells(4).Style.BackColor = Color.Yellow

                    ElseIf Porcent >= 1 Then
                        DgGTotal.Rows(i).Cells(4).Style.BackColor = Color.LimeGreen

                    End If

                Next

        .Columns("CtesMinimosaLaFecha").HeaderText = "Ctes. mínimos requeridos a la fecha"
        .Columns("CtesMinimosaLaFecha").Width = 90
        .Columns("CtesMinimosaLaFecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("CtesMinimosaLaFecha").DefaultCellStyle.Format = "###,##0"
      Catch ex As Exception
        'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView")
      End Try
    End With
  End Sub

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

        .Columns("CveAgt").HeaderText = "Clave"
        .Columns("CveAgt").Width = 50
        .Columns("CveAgt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("Agente").HeaderText = "Agente"
                .Columns("Agente").Width = 250

                .Columns("Acumulado").HeaderText = "Acumulado"
        .Columns("Acumulado").Width = 105
        .Columns("Acumulado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("Acumulado").DefaultCellStyle.Format = " ###,##0"

        .Columns("objetivo").HeaderText = "Objetivo"
        .Columns("objetivo").Width = 105
        .Columns("objetivo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("objetivo").DefaultCellStyle.Format = " ###,##0"

        .Columns("Vs").HeaderText = "Acum Vs Obj (%)"
        .Columns("Vs").Width = 90
        .Columns("Vs").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("Vs").DefaultCellStyle.Format = "#0.#0 %"

        Dim numfilas As Integer
        Dim Porcent As Decimal
        numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)
          If DgTotales.Item(4, i).Value Is DBNull.Value Then
            DgTotales.Item(4, i).Value = 0
            Porcent = 0
          Else
            Porcent = DgTotales.Item(4, i).Value
          End If


          If Porcent < 0.85 Then
                        DgTotales.Rows(i).Cells(4).Style.BackColor = Color.Orange

                    ElseIf Porcent >= 0.85 And Porcent < 1 Then
            DgTotales.Rows(i).Cells(4).Style.BackColor = Color.Yellow

          ElseIf Porcent >= 1 Then
            DgTotales.Rows(i).Cells(4).Style.BackColor = Color.LimeGreen

          End If

        Next

        .Columns("CtesMinimosaLaFecha").HeaderText = "Ctes. mínimos requeridos a la fecha"
        .Columns("CtesMinimosaLaFecha").Width = 90
        .Columns("CtesMinimosaLaFecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("CtesMinimosaLaFecha").DefaultCellStyle.Format = "###,##0"
      Catch ex As Exception
        'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView")
      End Try
    End With
  End Sub

  Sub EstiloDgPorCte()
    With Me.DgPorCliente
      .ReadOnly = True
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .RowHeadersVisible = True
      .RowHeadersWidth = 25
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try
        .Columns("CveCte").HeaderText = "Cve Cte."
        .Columns("CveCte").Width = 50
        .Columns("CveCte").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("CveCte").Visible = True

        .Columns("Cliente").HeaderText = "Cliente"
        .Columns("Cliente").Width = 250

        .Columns("Objetivo").HeaderText = "Objetivo"
        .Columns("Objetivo").Width = 85
        .Columns("Objetivo").DefaultCellStyle.Format = "$ ###,###,##0..#0"
        .Columns("Objetivo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("Acumulado").HeaderText = "Acumulado"
        .Columns("Acumulado").Width = 105
        .Columns("Acumulado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("Acumulado").DefaultCellStyle.Format = " $ ###,###,##0.#0"

        .Columns("Avance").HeaderText = "Acum Vs Obj (%)"
        .Columns("Avance").Width = 90
        .Columns("Avance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("Avance").DefaultCellStyle.Format = "##0.#0 %"


                Dim numfilas As Integer

                numfilas = DgPorCliente.RowCount 'cuenta las filas del DataGrid

                'recorre las filas del DataGrid
                For i = 0 To (numfilas - 1)

                    If DgPorCliente.Item(4, i).Value < 0.85 Then
                        DgPorCliente.Rows(i).Cells(4).Style.BackColor = Color.Orange

                    ElseIf DgPorCliente.Item(4, i).Value >= 0.85 And DgPorCliente.Item(4, i).Value < 1 Then
                        DgPorCliente.Rows(i).Cells(4).Style.BackColor = Color.Yellow

                    ElseIf DgPorCliente.Item(4, i).Value >= 1 Then
                        DgPorCliente.Rows(i).Cells(4).Style.BackColor = Color.LimeGreen

                    End If

                Next


                .Columns("Faltante").HeaderText = "Faltante para objetivo"
                .Columns("Faltante").Width = 105


                Dim Faltante As Decimal
        numfilas = DgPorCliente.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)
          If DgPorCliente.Item(5, i).Value Is DBNull.Value Then
            DgPorCliente.Item(5, i).Value = 0
            Faltante = 0
          Else
            If IsNumeric(DgPorCliente.Item(5, i).Value) = True Then
              Faltante = DgPorCliente.Item(5, i).Value

              DgPorCliente.Rows(i).Cells(5).Value = Faltante.ToString(" $ ###,###,##0.#0")

              If Faltante > 0 Then
                DgPorCliente.Rows(i).Cells(5).Style.BackColor = Color.Yellow
              End If
            Else
              If (DgPorCliente.Item(5, i).Value = "CUBIERTO") Then
                DgPorCliente.Rows(i).Cells(5).Style.BackColor = Color.LimeGreen
              End If
            End If
          End If
        Next

        .Columns("Faltante").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("CveAgt").HeaderText = "CveAgt"
        .Columns("CveAgt").Width = 180
        .Columns("CveAgt").Visible = False

        .Columns("Agente").HeaderText = "Agente"
                .Columns("Agente").Width = 200

                .Columns("CveSuc").HeaderText = "CveSuc"
        .Columns("CveSuc").Width = 180
        .Columns("CveSuc").Visible = False

        .Columns("Sucursal").HeaderText = "Sucursal"
        .Columns("Sucursal").Width = 80

        .Columns("Ruta").HeaderText = "Ruta"
        .Columns("Ruta").Width = 80

        .Columns("Estatus").HeaderText = "Status"
        .Columns("Estatus").Width = 80

      Catch ex As Exception
        'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView")
      End Try
    End With
  End Sub

  '************************************************************************
  'TERMINAN ESTILOS
  '************************************************************************

  Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
    Dim form As Form = ScoreCardCteObj
    form.Show()
  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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
      Titulo = "Reporte de Score Card Clientes TOTALES de todos los AGENTES con objetivos con FECHA " + Format(Me.DtpFechaIni.Value)
    Else
      Titulo = "Reporte de Score Card Clientes TOTALES del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
    End If

    Dim Columnas As String() = {"Cve. Agte.",
                                "Agente",
                                "Acumulado",
                                "Objetivo",
                                "Acumulado Vs Objetivo (%)",
                                "Ctes. min. requeridos a la fecha"}
    Dim TipoColumna As TipoDeDato() = {TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Entero,
                                       TipoDeDato.Entero,
                                       TipoDeDato.Porecentaje,
                                       TipoDeDato.Entero}
    Dim Visible As Boolean() = {True,
                                True,
                                True,
                                True,
                                True,
                                True}

    funciones.exporta2Excel(Titulo, Columnas, TipoColumna, Visible, DgTotales)


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
    'oSheet.range("A3").value = "Clave Agte."
    'oSheet.range("B3").value = "Agente"
    'oSheet.range("C3").value = "Acumulado"
    'oSheet.range("D3").value = "Objetivo"
    'oSheet.range("E3").value = "Acumulado Vs Objetivo (%)"
    'oSheet.range("F3").value = "Ctes. min. requeridos a la fecha"


    ''para poner la primera fila de los titulos en negrita
    'oSheet.range("A3:F3").font.bold = True
    'Dim fila_dt As Integer = 0
    'Dim fila_dt_excel As Integer = 0
    'Dim tanto_porcentaje As String = ""
    'Dim marikona As Integer = 0

    'Dim total_reg As Integer = 0

    'total_reg = DgTotales.RowCount
    'For fila_dt = 0 To total_reg - 1

    '  'para leer una celda en concreto
    '  'el numero es la columna
    '  Dim cel0 As String = IIf(IsDBNull(Me.DgTotales.Item(0, fila_dt).Value), 0, Me.DgTotales.Item(0, fila_dt).Value)
    '  Dim cel1 As String = IIf(IsDBNull(Me.DgTotales.Item(1, fila_dt).Value), 0, Me.DgTotales.Item(1, fila_dt).Value)
    '  Dim cel2 As String = IIf(IsDBNull(Me.DgTotales.Item(2, fila_dt).Value), 0, Me.DgTotales.Item(2, fila_dt).Value)
    '  Dim cel3 As String = IIf(IsDBNull(Me.DgTotales.Item(3, fila_dt).Value), 0, Me.DgTotales.Item(3, fila_dt).Value)
    '  Dim cel4 As String = IIf(IsDBNull(Me.DgTotales.Item(4, fila_dt).Value), 0, Me.DgTotales.Item(4, fila_dt).Value * 100)
    '  Dim cel5 As String = IIf(IsDBNull(Me.DgTotales.Item(5, fila_dt).Value), 0, Me.DgTotales.Item(5, fila_dt).Value)

    '  fila_dt_excel = fila_dt + 4

    '  'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
    '  oSheet.range("A" & fila_dt_excel).value = FormatNumber(cel0, 0)
    '  oSheet.range("B" & fila_dt_excel).value = cel1
    '  oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 0)
    '  oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 0)
    '  oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
    '  oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 0)
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
    '  oSheet.range("A1").value = "Reporte de Score Card Clientes TOTALES de todos los AGENTES con objetivos con FECHA " + Format(Me.DtpFechaIni.Value)
    'Else
    '  oSheet.range("A1").value = "Reporte de Score Card Clientes TOTALES del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
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

  Private Sub DgVtaAgte_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
    'Dim frm As New LineasHalconDetalle()
    'frm.Show()
  End Sub

  Private Sub ImpExcelPorCte_Click(sender As Object, e As EventArgs) Handles ImpExcelPorCte.Click
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
      Titulo = "Reporte de Score Card Clientes por cliente de todos los AGENTES con FECHA " + Format(Me.DtpFechaIni.Value)
    Else
      Titulo = "Reporte de Score Card Clientes por cliente del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
    End If

    Dim Columnas As String() = {"Cve. Cte.",
                                "Cliente",
                                "Objetivo",
                                "Acumulado",
                                "Acumulado Vs Objetivo (%)",
                                "Faltante para objetivo",
                                "Cve. Agente",
                                "Agente",
                                "Cve. Sucursal",
                                "Sucursal",
                                "Ruta",
                                "Estatus"}

    Dim TipoColumna As TipoDeDato() = {TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Porecentaje,
                                       TipoDeDato.Pesos,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena,
                                       TipoDeDato.Cadena}
    Dim Visible As Boolean() = {True,
                                True,
                                True,
                                True,
                                True,
                                True,
                                False,
                                True,
                                false,
                                True,
                                True,
                                True}

    funciones.exporta2Excel(Titulo, Columnas, TipoColumna, Visible, DgPorCliente)

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
    'oSheet.range("A3").value = "Cve. Cte."
    'oSheet.range("B3").value = "Cliente"
    'oSheet.range("C3").value = "Objetivo"
    'oSheet.range("D3").value = "Acumulado"
    'oSheet.range("E3").value = "Acumulado Vs Objetivo (%)"
    'oSheet.range("F3").value = "Faltante para objetivo"
    'oSheet.range("G3").value = "Agente"
    'oSheet.range("H3").value = "Sucursal"
    'oSheet.range("I3").value = "Ruta"
    'oSheet.range("J3").value = "Estatus"

    ''para poner la primera fila de los titulos en negrita
    'oSheet.range("A3:J3").font.bold = True
    'Dim fila_dt As Integer = 0
    'Dim fila_dt_excel As Integer = 0
    'Dim tanto_porcentaje As String = ""
    'Dim marikona As Integer = 0

    'Dim total_reg As Integer = 0

    'total_reg = DgPorCliente.RowCount
    'For fila_dt = 0 To total_reg - 1

    '  'para leer una celda en concreto
    '  'el numero es la columna
    '  Dim cel0 As String = IIf(IsDBNull(Me.DgPorCliente.Item(0, fila_dt).Value), 0, Me.DgPorCliente.Item(0, fila_dt).Value)
    '  Dim cel1 As String = IIf(IsDBNull(Me.DgPorCliente.Item(1, fila_dt).Value), 0, Me.DgPorCliente.Item(1, fila_dt).Value)
    '  Dim cel2 As String = IIf(IsDBNull(Me.DgPorCliente.Item(2, fila_dt).Value), 0, Me.DgPorCliente.Item(2, fila_dt).Value)
    '  Dim cel3 As String = IIf(IsDBNull(Me.DgPorCliente.Item(3, fila_dt).Value), 0, Me.DgPorCliente.Item(3, fila_dt).Value)
    '  Dim cel4 As String = IIf(IsDBNull(Me.DgPorCliente.Item(4, fila_dt).Value), 0, Me.DgPorCliente.Item(4, fila_dt).Value * 100)
    '  Dim cel5 As String = IIf(IsDBNull(Me.DgPorCliente.Item(5, fila_dt).Value), 0, Me.DgPorCliente.Item(5, fila_dt).Value)
    '  Dim cel6 As String = IIf(IsDBNull(Me.DgPorCliente.Item(7, fila_dt).Value), 0, Me.DgPorCliente.Item(7, fila_dt).Value)
    '  Dim cel7 As String = IIf(IsDBNull(Me.DgPorCliente.Item(9, fila_dt).Value), 0, Me.DgPorCliente.Item(9, fila_dt).Value)
    '  Dim cel8 As String = IIf(IsDBNull(Me.DgPorCliente.Item(10, fila_dt).Value), 0, Me.DgPorCliente.Item(10, fila_dt).Value)
    '  Dim cel9 As String = IIf(IsDBNull(Me.DgPorCliente.Item(11, fila_dt).Value), 0, Me.DgPorCliente.Item(11, fila_dt).Value)

    '  fila_dt_excel = fila_dt + 4

    '  'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
    '  oSheet.range("A" & fila_dt_excel).value = cel0
    '  oSheet.range("B" & fila_dt_excel).value = cel1
    '  oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
    '  oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
    '  oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
    '  oSheet.range("F" & fila_dt_excel).value = cel5
    '  oSheet.range("G" & fila_dt_excel).value = cel6
    '  oSheet.range("H" & fila_dt_excel).value = cel7
    '  oSheet.range("I" & fila_dt_excel).value = cel8
    '  oSheet.range("J" & fila_dt_excel).value = cel9
    'Next

    '' para que el tamano de la columna tenga como minimo el maximo de sus textos
    'oSheet.columns("A:J").entirecolumn.autofit()

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
    '  oSheet.range("A1").value = "Reporte de Score Card Clientes por cliente de todos los AGENTES con FECHA " + Format(Me.DtpFechaIni.Value)
    'Else
    '  oSheet.range("A1").value = "Reporte de Score Card Clientes por cliente del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
    'End If

    'oSheet.range("C1").value = Rangos
    'oSheet.range("C2").value = Rangos2

    'oExcel.visible = True
    'System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    'GC.Collect()
    'oSheet = Nothing
    'oBook = Nothing
    'oExcel = Nothing
  End Sub

  Private Sub ScoreCardCliente_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    iniciar()
  End Sub

  Private Sub DgTotales_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgTotales.CurrentCellChanged

  End Sub

  Private Sub DgTotales_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgTotales.CellEnter
    Try
      If (IsNothing(DvAgentes) = False) Then
        If DgTotales.Item(0, DgTotales.CurrentRow.Index).Value.ToString = "999" Then
          DvAgentes.RowFilter = ""
          'DvAgentes.RowFilter = "CveAgt =" & DgTotales.Item(0, DgTotales.CurrentRow.Index).Value.ToString
        ElseIf instr(DgTotales.Item(0, DgTotales.CurrentRow.Index).Value.ToString, "999-") > 0 Then
          'Obtengo el numero del agente
          Dim AgteNum As Integer = CInt(Mid(DgTotales.Item(0, DgTotales.CurrentRow.Index).Value.ToString, 4))
          DvAgentes.RowFilter = "CveAgt =" & AgteNum.ToString
        Else
          DvAgentes.RowFilter = "CveAgt =" & DgTotales.Item(0, DgTotales.CurrentRow.Index).Value.ToString
        End If
        EstiloDgPorCte()
      End If
    Catch ex As Exception

    End Try
  End Sub

    Private Sub DgGTotal_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgGTotal.CellContentClick

    End Sub

    Private Sub DgTotales_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgTotales.ColumnHeaderMouseClick

        Dim numfilas As Integer
        Dim Porcent As Decimal
        numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)
            If DgTotales.Item(4, i).Value Is DBNull.Value Then
                DgTotales.Item(4, i).Value = 0
                Porcent = 0
            Else
                Porcent = DgTotales.Item(4, i).Value
            End If


            If Porcent < 0.85 Then
                DgTotales.Rows(i).Cells(4).Style.BackColor = Color.Orange

            ElseIf Porcent >= 0.85 And Porcent < 1 Then
                DgTotales.Rows(i).Cells(4).Style.BackColor = Color.Yellow

            ElseIf Porcent >= 1 Then
                DgTotales.Rows(i).Cells(4).Style.BackColor = Color.LimeGreen

            End If
        Next
    End Sub

    Private Sub DgGTotal_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgGTotal.ColumnHeaderMouseClick
        Dim numfilas As Integer
        Dim Porcent As Decimal
        numfilas = DgGTotal.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)
            If DgGTotal.Item(4, i).Value Is DBNull.Value Then
                DgGTotal.Item(4, i).Value = 0
                Porcent = 0
            Else
                Porcent = DgGTotal.Item(4, i).Value
            End If


            If Porcent < 0.85 Then
                DgGTotal.Rows(i).Cells(4).Style.BackColor = Color.Orange

            ElseIf Porcent >= 0.85 And Porcent < 1 Then
                DgGTotal.Rows(i).Cells(4).Style.BackColor = Color.Yellow

            ElseIf Porcent >= 1 Then
                DgGTotal.Rows(i).Cells(4).Style.BackColor = Color.LimeGreen

            End If

        Next
    End Sub

    Private Sub DgPorCliente_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgPorCliente.ColumnHeaderMouseClick
        Dim numfilas As Integer
        Dim Faltante As Decimal
        numfilas = DgPorCliente.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)
            If DgPorCliente.Item(5, i).Value Is DBNull.Value Then
                DgPorCliente.Item(5, i).Value = 0
                Faltante = 0
            Else
                If IsNumeric(DgPorCliente.Item(5, i).Value) = True Then
                    Faltante = DgPorCliente.Item(5, i).Value

                    DgPorCliente.Rows(i).Cells(5).Value = Faltante.ToString(" $ ###,###,##0.#0")

                    If Faltante > 0 Then
                        DgPorCliente.Rows(i).Cells(5).Style.BackColor = Color.Yellow
                    End If
                Else
                    If (DgPorCliente.Item(5, i).Value = "CUBIERTO") Then
                        DgPorCliente.Rows(i).Cells(5).Style.BackColor = Color.LimeGreen
                    End If
                End If
            End If
        Next
    End Sub
End Class

