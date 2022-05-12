Imports System.Data.SqlClient

Public Class AgenteListaPrecio
  Dim DvResumen As New DataView
  Dim DvAgentes As New DataView
  Dim DvAgentes2 As New DataView
  Dim DvDetalles As New DataView
  Dim DvDetalles2 As New DataView

  ''VARIABLE PARA LA CLASE COMANDOS SQL  
  Dim SQL As New Comandos_SQL()

  Private Sub VtasScoreCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

    Me.DtpFechaInicial.Value = Format(Date.Now, "dd/MM/yyyy")
    Me.DtpFechaFinal.Value = Format(Date.Now, "dd/MM/yyyy")

    Dim ConsutaListaS As String
    Dim ConsutaListaA As String

    '*********************************************************************************************************************************************
    'Codigo para obtener informacion de los agentes que cada usuario debe ver
    '*********************************************************************************************************************************************
    SQL.conectarTPM()

    Dim GroupCode As String
    Dim slpcode As String = SQL.CampoEspecifico("SELECT AgteVentas FROM Usuarios where Id_Usuario = '" & UsrTPM & "'", "AgteVentas")
    Dim CodAgte As String = SQL.CampoEspecifico("SELECT CodAgte FROM Usuarios where Id_Usuario = '" & UsrTPM & "'", "CodAgte")

        If slpcode = "" And UsrTPM <> "MANAGER" And UsrTPM <> "TESORERIA" And UsrTPM <> "COMERCIAL" Then
            CerrarSCClientes = True
            MsgBox("Este usuario no tienen definido el valor de Agte Ventas en su registro", MsgBoxStyle.Exclamation, "Falta configuración de agente ventas")
            Exit Sub
        End If

  '    If slpcode <> "" Then
  '  GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " & slpcode, "GroupCode")
  'Else
  '  GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " & CodAgte, "GroupCode")
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

  Dim Almacen As String = SQL.CampoEspecifico("select Almacen from Usuarios where Id_Usuario = '" & UsrTPM & "'", "Almacen")

        'COMBO DE SUCURSALES
        If UsrTPM = "MANAGER" Or UsrTPM = "TESORERIA" Or UsrTPM = "COMERCIAL" Then
            ConsutaListaS = "SELECT GroupCode,GroupName FROM SBO_TPD.dbo.OCRG with (nolock) WHERE GroupType = 'C' ORDER BY GroupName"
        Else
            ConsutaListaS = "SELECT GroupCode,GroupName FROM SBO_TPD.dbo.OCRG with (nolock) WHERE GroupType = 'C' AND GroupCode = '" & GroupCode & "' ORDER BY GroupName"
    End If

        'COMBO DE AGENTES
        If UsrTPM = "MANAGER" Or UsrTPM = "TESORERIA" Or UsrTPM = "COMERCIAL" Then
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
    'Valido fechas
    If CDate(DtpFechaFinal.Value) < CDate(DtpFechaInicial.Value) Then
      MsgBox("Por favor verifique que las fechas sean correctas", MsgBoxStyle.Exclamation, "Se presentó un error en las fechas")
      Exit Sub
    End If

    Espere(True)
    DgAgentes.DataSource = Nothing
    GetInf()
    Espere(False)
  End Sub

  Sub GetInf()
    Dim vDiasMes As Integer
    Dim cnn As SqlConnection = Nothing
    Dim cmd As SqlCommand = Nothing

    Dim cmd2 As SqlCommand = Nothing
    Dim vDiasTrans As Integer

    Dim cmd3 As SqlCommand = Nothing
    Dim cmd4 As SqlCommand = Nothing

    Try
      cnn = New SqlConnection(StrTpm)
      cnn.Open()
      cmd4 = New SqlCommand("TPD_AgenteListaPrecio", cnn)
      cmd4.CommandType = CommandType.StoredProcedure
      cmd4.Parameters.Add("@FechaInicial", SqlDbType.Date).Value = Me.DtpFechaInicial.Value
      cmd4.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = Me.DtpFechaFinal.Value
      cmd4.Parameters.Add("@Sucursal", SqlDbType.Int).Value = Me.CmbSucursal.SelectedValue
      cmd4.Parameters.Add("@Agente", SqlDbType.VarChar, 30).Value = Me.CmbAgteVta.SelectedValue
      cmd4.Parameters.Add("@AgenteVentas", SqlDbType.VarChar, 30).Value = UsrTPM

      cmd4.CommandTimeout = 600
      cmd4.ExecuteNonQuery()
      cmd4.Connection.Close()
      Dim da As New SqlDataAdapter
      da.SelectCommand = cmd4
      da.SelectCommand.Connection = cnn

      ''--------------------------------------------
      Dim DsInf As New DataSet
      da.Fill(DsInf, "Informacion")

      DsInf.Tables(0).TableName = "Resumen"
      DsInf.Tables(1).TableName = "Agentes"
      DsInf.Tables(2).TableName = "Detalles"

      DvResumen.Table = DsInf.Tables("Resumen")
      DvAgentes.Table = DsInf.Tables("Agentes")
      DvDetalles.Table = DsInf.Tables("Detalles")

      DgResumen.DataSource = DvResumen
      DgAgentes.DataSource = DvAgentes
      DgDetalles.DataSource = DvDetalles

    Catch ex As Exception
      'MsgBox(ex.Message)
      Exit Sub
    Finally
      If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
        cnn.Close()
      End If
    End Try

    '-------Diseño de DATAGRID Resumen
    With Me.DgResumen
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
      DgResumen.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try
        .Columns(0).HeaderText = "Resumen de información"
        .Columns(0).Width = 200

        .Columns(1).HeaderText = "Importe Lista 4"
        .Columns(1).Width = 85
        .Columns(1).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(1).DefaultCellStyle.SelectionBackColor = Color.Gray

        .Columns(2).HeaderText = "% Lista 4"
        .Columns(2).Width = 85
        .Columns(2).DefaultCellStyle.Format = "0.00 %"
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(3).HeaderText = "Importe Lista 3"
        .Columns(3).Width = 85
        .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(3).DefaultCellStyle.SelectionBackColor = Color.Gray

        .Columns(4).HeaderText = "% Lista 3"
        .Columns(4).Width = 85
        .Columns(4).DefaultCellStyle.Format = "0.00 %"
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(5).HeaderText = "Importe Lista 2"
        .Columns(5).Width = 85
        .Columns(5).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(5).DefaultCellStyle.SelectionBackColor = Color.Gray

        .Columns(6).HeaderText = "% Lista 2"
        .Columns(6).Width = 85
        .Columns(6).DefaultCellStyle.Format = "0.00 %"
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(7).HeaderText = "Importe Lista 1"
        .Columns(7).Width = 85
        .Columns(7).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(7).DefaultCellStyle.SelectionBackColor = Color.Gray

        .Columns(8).HeaderText = "% Lista 1"
        .Columns(8).Width = 85
        .Columns(8).DefaultCellStyle.Format = "0.00 %"
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(9).HeaderText = "Importe Lista 10"
        .Columns(9).Width = 85
        .Columns(9).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns(9).DefaultCellStyle.SelectionBackColor = Color.Gray

        .Columns(10).HeaderText = "% Lista 10"
        .Columns(10).Width = 85
        .Columns(10).DefaultCellStyle.Format = "0.00 %"
        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(11).HeaderText = "Total otros servicios"
        .Columns(11).Width = 85
        .Columns(11).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(12).HeaderText = "Importe Total"
        .Columns(12).Width = 85
        .Columns(12).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        For i = 0 To DgResumen.RowCount - 1
          DgResumen.Item(1, i).Style.BackColor = Color.LightGray
          DgResumen.Item(3, i).Style.BackColor = Color.LightGray
          DgResumen.Item(5, i).Style.BackColor = Color.LightGray
          DgResumen.Item(7, i).Style.BackColor = Color.LightGray
          DgResumen.Item(9, i).Style.BackColor = Color.LightGray
        Next
      Catch ex As Exception
      End Try
    End With

    DiseñoAgentes()
    DiseñoDetalles()

    If DgAgentes.Rows.Count > 0 Then
      DgAgentes.Rows(0).Selected = True
    End If
  End Sub

  Private Sub DiseñoAgentes()
    '-------Diseño de DATAGRID Agentes
    With Me.DgAgentes
      Try
        '.DataSource = DtAgte
        .ReadOnly = True
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

        DgAgentes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        .RowHeadersVisible = True
        .RowHeadersWidth = 25
        .Columns(0).HeaderText = "Clave Agente"
        .Columns(0).Width = 50
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(0).Visible = False

        .Columns(1).HeaderText = "Agente"
        .Columns(1).Width = 200

        .Columns(2).HeaderText = "Importe Lista 4"
        .Columns(2).Width = 85
        .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(3).HeaderText = "% Lista 4"
        .Columns(3).Width = 85
        .Columns(3).DefaultCellStyle.Format = "0.00 %"
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(4).HeaderText = "Importe Lista 3"
        .Columns(4).Width = 85
        .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(5).HeaderText = "% Lista 3"
        .Columns(5).Width = 85
        .Columns(5).DefaultCellStyle.Format = "0.00 %"
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(6).HeaderText = "Importe Lista 2"
        .Columns(6).Width = 85
        .Columns(6).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(7).HeaderText = "% Lista 2"
        .Columns(7).Width = 85
        .Columns(7).DefaultCellStyle.Format = "0.00 %"
        .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(8).HeaderText = "Importe Lista 1"
        .Columns(8).Width = 85
        .Columns(8).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(9).HeaderText = "% Lista 1"
        .Columns(9).Width = 85
        .Columns(9).DefaultCellStyle.Format = "0.00 %"
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(10).HeaderText = "Importe Lista 10"
        .Columns(10).Width = 85
        .Columns(10).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(11).HeaderText = "% Lista 10"
        .Columns(11).Width = 85
        .Columns(11).DefaultCellStyle.Format = "0.00 %"
        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(12).HeaderText = "Ventas Totales"
        .Columns(12).Width = 85
        .Columns(12).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(13).HeaderText = "CveSuc"
        .Columns(13).Width = 250
        .Columns(13).Visible = False

        .Columns(14).HeaderText = "Sucursal"
        .Columns(14).Width = 150
        .Columns(14).Visible = False

        For i = 0 To DgAgentes.RowCount - 1
          DgAgentes.Item(2, i).Style.BackColor = Color.LightGray
          DgAgentes.Item(4, i).Style.BackColor = Color.LightGray
          DgAgentes.Item(6, i).Style.BackColor = Color.LightGray
          DgAgentes.Item(8, i).Style.BackColor = Color.LightGray
          DgAgentes.Item(10, i).Style.BackColor = Color.LightGray
        Next

        '.AutoResizeColumns()
      Catch ex As Exception
      End Try
    End With
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
        .Columns(0).HeaderText = "CveAgente"
        .Columns(0).Width = 50
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(0).Visible = False

        .Columns(1).HeaderText = "Línea"
        .Columns(1).Width = 200

        .Columns(2).HeaderText = "Importe Lista 4"
        .Columns(2).Width = 85
        .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(3).HeaderText = "% Lista 4"
        .Columns(3).Width = 85
        .Columns(3).DefaultCellStyle.Format = "0.00 %"
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(4).HeaderText = "Importe Lista 3"
        .Columns(4).Width = 85
        .Columns(4).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(5).HeaderText = "% Lista 3"
        .Columns(5).Width = 85
        .Columns(5).DefaultCellStyle.Format = "0.00 %"
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(6).HeaderText = "Importe Lista 2"
        .Columns(6).Width = 85
        .Columns(6).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(7).HeaderText = "% Lista 2"
        .Columns(7).Width = 85
        .Columns(7).DefaultCellStyle.Format = "0.00 %"
        .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(8).HeaderText = "Importe Lista 1"
        .Columns(8).Width = 85
        .Columns(8).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(9).HeaderText = "% Lista 1"
        .Columns(9).Width = 85
        .Columns(9).DefaultCellStyle.Format = "0.00 %"
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(10).HeaderText = "Importe Lista 10"
        .Columns(10).Width = 85
        .Columns(10).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(11).HeaderText = "% Lista 10"
        .Columns(11).Width = 85
        .Columns(11).DefaultCellStyle.Format = "0.00 %"
        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(12).HeaderText = "Total por línea"
        .Columns(12).Width = 85
        .Columns(12).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(13).HeaderText = "CveSuc"
        .Columns(13).Width = 250
        .Columns(13).Visible = False

        .Columns(14).HeaderText = "Sucursal"
        .Columns(14).Width = 150
        .Columns(14).Visible = False

        For i = 0 To DgDetalles.RowCount - 1
          DgDetalles.Item(2, i).Style.BackColor = Color.LightGray
          DgDetalles.Item(4, i).Style.BackColor = Color.LightGray
          DgDetalles.Item(6, i).Style.BackColor = Color.LightGray
          DgDetalles.Item(8, i).Style.BackColor = Color.LightGray
          DgDetalles.Item(10, i).Style.BackColor = Color.LightGray
        Next

        '.AutoResizeColumns()
      Catch ex As Exception
      End Try
    End With
  End Sub

  '---Generar reporte en EXCEL de TOTALES
  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnExportaAgentes.Click
    ExportarAgentes()
  End Sub

  Sub ExportarAgentes()
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
    oSheet.range("A3").value = "Agente"
    oSheet.range("B3").value = "Importe linea 4"
    oSheet.range("C3").value = "% linea 4"
    oSheet.range("D3").value = "Importe linea 3"
    oSheet.range("E3").value = "% linea 3"
    oSheet.range("F3").value = "Importe linea 2"
    oSheet.range("G3").value = "% linea 2"
    oSheet.range("H3").value = "Importe linea 1"
    oSheet.range("I3").value = "% linea 1"
    oSheet.range("J3").value = "Importe linea 10"
    oSheet.range("K3").value = "% linea 10"
    oSheet.range("L3").value = "Ventas totales"

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A3:L3").font.bold = True
    oSheet.Range("A3:L3").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGray)
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgAgentes.RowCount
    For fila_dt = 0 To total_reg - 1
      'para leer una celda en concreto
      'el numero es la columna
      Dim cel0 As String = IIf(IsDBNull(Me.DgAgentes.Item(1, fila_dt).Value), 1, Me.DgAgentes.Item(1, fila_dt).Value)
      Dim cel1 As String = IIf(IsDBNull(Me.DgAgentes.Item(2, fila_dt).Value), 0, Me.DgAgentes.Item(2, fila_dt).Value)
      Dim cel2 As String = IIf(IsDBNull(Me.DgAgentes.Item(3, fila_dt).Value), 0, Me.DgAgentes.Item(3, fila_dt).Value)
      Dim cel3 As String = IIf(IsDBNull(Me.DgAgentes.Item(4, fila_dt).Value), 0, Me.DgAgentes.Item(4, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgAgentes.Item(5, fila_dt).Value), 0, Me.DgAgentes.Item(5, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgAgentes.Item(6, fila_dt).Value), 0, Me.DgAgentes.Item(6, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgAgentes.Item(7, fila_dt).Value), 0, Me.DgAgentes.Item(7, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgAgentes.Item(8, fila_dt).Value), 0, Me.DgAgentes.Item(8, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgAgentes.Item(9, fila_dt).Value), 0, Me.DgAgentes.Item(9, fila_dt).Value)
      Dim cel9 As String = IIf(IsDBNull(Me.DgAgentes.Item(10, fila_dt).Value), 0, Me.DgAgentes.Item(10, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.DgAgentes.Item(11, fila_dt).Value), 0, Me.DgAgentes.Item(11, fila_dt).Value)
      Dim cel11 As String = IIf(IsDBNull(Me.DgAgentes.Item(12, fila_dt).Value), 0, Me.DgAgentes.Item(12, fila_dt).Value)

      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel0
      oSheet.range("B" & fila_dt_excel).value = FormatNumber(cel1, 2)
      oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 4)
      oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
      oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 4)
      oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
      oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 4)
      oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
      oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 4)
      oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
      oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 4)
      oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 2)
    Next

    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:l").entirecolumn.autofit()

    'Coloco autofiltro
    Dim objRangoFiltro As Microsoft.Office.Interop.Excel.Range = oSheet.Range("A3:l3")
    objRangoFiltro.AutoFilter(1)

    'Coloco colores en columnas igual que en la pantalla
    oSheet.Range("B4:B" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

    oSheet.Range("D4:D" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
    oSheet.Range("F4:F" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
    oSheet.Range("H4:H" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
    oSheet.Range("J4:J" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

    'Formato numerico
    oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "0.00 %"
    oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "0.00 %"
    oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "0.00 %"
    oExcel.Worksheets("Hoja1").Columns("I").NumberFormat = "0.00 %"
    oExcel.Worksheets("Hoja1").Columns("K").NumberFormat = "0.00 %"

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
      oSheet.range("A1").value = "Reporte de Ventas por listas de precio de todos los AGENTES del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
    Else
      oSheet.range("A1").value = "Reporte de Ventas por lineas y listas de precio del AGENTE " + returnValue + " del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
    End If

    oSheet.range("C1").value = Rangos
    oSheet.range("C2").value = Rangos2

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
  End Sub


  '--------Generar Excel Agentes
  Private Sub BtnAgentes_Click(sender As Object, e As EventArgs) Handles BtnDetalles.Click
    'ExportarNuevoVendedores()
    exportaDetalles()
  End Sub

  Sub exportaDetalles()
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
    oSheet.range("A3").value = "Línea"
    oSheet.range("B3").value = "Importe linea 4"
    oSheet.range("C3").value = "% linea 4"
    oSheet.range("D3").value = "Importe linea 3"
    oSheet.range("E3").value = "% linea 3"
    oSheet.range("F3").value = "Importe linea 2"
    oSheet.range("G3").value = "% linea 2"
    oSheet.range("H3").value = "Importe linea 1"
    oSheet.range("I3").value = "% linea 1"
    oSheet.range("J3").value = "Importe linea 10"
    oSheet.range("K3").value = "% linea 10"
    oSheet.range("L3").value = "Total por Línea"

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A3:L3").font.bold = True
    oSheet.Range("A3:L3").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGray)
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgDetalles.RowCount
    For fila_dt = 0 To total_reg - 1
      'para leer una celda en concreto
      'el numero es la columna
      Dim cel0 As String = IIf(IsDBNull(Me.DgDetalles.Item(1, fila_dt).Value), 1, Me.DgDetalles.Item(1, fila_dt).Value)
      Dim cel1 As String = IIf(IsDBNull(Me.DgDetalles.Item(2, fila_dt).Value), 0, Me.DgDetalles.Item(2, fila_dt).Value)
      Dim cel2 As String = IIf(IsDBNull(Me.DgDetalles.Item(3, fila_dt).Value), 0, Me.DgDetalles.Item(3, fila_dt).Value)
      Dim cel3 As String = IIf(IsDBNull(Me.DgDetalles.Item(4, fila_dt).Value), 0, Me.DgDetalles.Item(4, fila_dt).Value)
      Dim cel4 As String = IIf(IsDBNull(Me.DgDetalles.Item(5, fila_dt).Value), 0, Me.DgDetalles.Item(5, fila_dt).Value)
      Dim cel5 As String = IIf(IsDBNull(Me.DgDetalles.Item(6, fila_dt).Value), 0, Me.DgDetalles.Item(6, fila_dt).Value)
      Dim cel6 As String = IIf(IsDBNull(Me.DgDetalles.Item(7, fila_dt).Value), 0, Me.DgDetalles.Item(7, fila_dt).Value)
      Dim cel7 As String = IIf(IsDBNull(Me.DgDetalles.Item(8, fila_dt).Value), 0, Me.DgDetalles.Item(8, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.DgDetalles.Item(9, fila_dt).Value), 0, Me.DgDetalles.Item(9, fila_dt).Value)
      Dim cel9 As String = IIf(IsDBNull(Me.DgDetalles.Item(10, fila_dt).Value), 0, Me.DgDetalles.Item(10, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.DgDetalles.Item(11, fila_dt).Value), 0, Me.DgDetalles.Item(11, fila_dt).Value)
      Dim cel11 As String = IIf(IsDBNull(Me.DgDetalles.Item(12, fila_dt).Value), 0, Me.DgDetalles.Item(12, fila_dt).Value)

      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel0
      oSheet.range("B" & fila_dt_excel).value = FormatNumber(cel1, 2)
      oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 4)
      oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
      oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 4)
      oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
      oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 4)
      oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
      oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 4)
      oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
      oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 4)
      oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 2)
    Next

    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:l").entirecolumn.autofit()

    'Coloco colores en columnas igual que en la pantalla
    oSheet.Range("B4:B" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
    oSheet.Range("D4:D" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
    oSheet.Range("F4:F" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
    oSheet.Range("H4:H" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
    oSheet.Range("J4:J" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

    'Formato numerico
    oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "0.00 %"
    oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "0.00 %"
    oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "0.00 %"
    oExcel.Worksheets("Hoja1").Columns("I").NumberFormat = "0.00 %"
    oExcel.Worksheets("Hoja1").Columns("K").NumberFormat = "0.00 %"

    'Coloco autofiltro
    Dim objRangoFiltro As Microsoft.Office.Interop.Excel.Range = oSheet.Range("A3:l3")
    objRangoFiltro.AutoFilter(1)

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

    Try
      If DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString = 999 Then
        oSheet.range("A1").value = "Reporte de Ventas por listas de precio de todos los AGENTES del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
      ElseIf DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString = 100 Then
        oSheet.range("A1").value = "Reporte de Ventas por listas de precio de la sucursal PUEBLA del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
      ElseIf DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString = 102 Then
        oSheet.range("A1").value = "Reporte de Ventas por listas de precio de la sucursal MERIDA del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
      ElseIf DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString = 103 Then
        oSheet.range("A1").value = "Reporte de Ventas por listas de precio de la sucursal TUXTLA GUTIERREZ del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
      Else
        oSheet.range("A1").value = "Reporte de Ventas por lineas y listas de precio del AGENTE " + DgAgentes.Item(1, DgAgentes.CurrentRow.Index).Value.ToString + " del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
      End If
    Catch
      oSheet.range("A1").value = "Reporte de Ventas por listas de precio de todos los AGENTES del " + Format(Me.DtpFechaInicial.Value, "dd/MM/yyyy") & " al " & Format(Me.DtpFechaFinal.Value, "dd/MM/yyyy")
    End Try

    oSheet.range("C1").value = Rangos
    oSheet.range("C2").value = Rangos2

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing
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

  Private Sub DgAgentes_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgAgentes.CellEnter
    Try
      DvDetalles.RowFilter = "SlpCode =" & DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString
      DiseñoDetalles()

      'If DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString = "999" Then
      '  DvDetalles.RowFilter = "SlpCode > 0 "
      '  DiseñoDetalles()
      'Else
      '  'En el caso de que busquen por sucursal
      '  If DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString = "100" Or
      '    DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString = "102" Or
      '    DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString = "103" Then
      '    DvDetalles.RowFilter = "CveSuc =" & DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString
      '  Else
      '    DvDetalles.RowFilter = "SlpCode =" & DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString
      '  End If
      '  DiseñoDetalles()
      'End If
    Catch ex As Exception

    End Try
  End Sub
End Class