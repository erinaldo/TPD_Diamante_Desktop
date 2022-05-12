Imports System.Data.SqlClient
Imports System.Globalization

Public Class frmHistoricoPasadoListasPrecio

 Dim DvRes As New DataView
 Dim DvPorMes As New DataView
 Dim DvPorMes2 As New DataView
 Dim DvPorAgente As New DataView

 ''VARIABLE PARA LA CLASE COMANDOS SQL  
 Dim SQL As New Comandos_SQL()


 Private Sub frmHistoricoPasadoListasPrecio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
  Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

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

  'If slpcode <> "" Then
  ' GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " & slpcode, "GroupCode")
  'Else
  ' GroupCode = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " & CodAgte, "GroupCode")
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

   '********************************************************************
   'AGREGAMOS TODOS EN SUCURSALES
   '********************************************************************
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
   '********************************************************************

   Me.CmbSucursal.DataSource = DSetTablas.Tables("Sucursales")
   Me.CmbSucursal.DisplayMember = "GroupName"
   Me.CmbSucursal.ValueMember = "GroupCode"
   Me.CmbSucursal.SelectedIndex = 0
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

   '********************************************************************
   ' AGREGAMOS TODOS EN AGENTES ****************************************
   '********************************************************************
   'Agregamos los valores a los campos de la tabla
   filaAgte("slpname") = "TODOS"
   filaAgte("slpcode") = 999
   filaAgte("GroupCode") = 999

   'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
   DSetTablas.Tables("Agentes").Rows.Add(filaAgte)
   '********************************************************************

   DvPorMes2.Table = DSetTablas.Tables("Agentes")

   Me.CmbAgteVta.DataSource = DvPorMes2
   Me.CmbAgteVta.DisplayMember = "slpname"
   Me.CmbAgteVta.ValueMember = "slpcode"

   Me.CmbAgteVta.SelectedValue = 999
  End Using
  SQL.Cerrar()

  Dim Mes() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}

  Dim tMesI As DataTable
  tMesI = New DataTable("Meses")
  tMesI.Columns.Add("id")
  tMesI.Columns.Add("DescMes")

  Dim tMesF As DataTable
  tMesF = New DataTable("Meses")
  tMesF.Columns.Add("id")
  tMesF.Columns.Add("DescMes")

  Dim rowMes As DataRow
  For ciclo = 0 To 11
   rowMes = tMesI.NewRow
   rowMes("id") = ciclo.ToString
   rowMes("DescMes") = Mes(ciclo)
   tMesI.Rows.Add(rowMes)

   rowMes = tMesF.NewRow
   rowMes("id") = ciclo.ToString
   rowMes("DescMes") = Mes(ciclo)
   tMesF.Rows.Add(rowMes)
  Next

  cmb_MesI.DataSource = tMesI
  cmb_MesI.ValueMember = "id"
  cmb_MesI.DisplayMember = "DescMes"

  cmb_MesF.DataSource = tMesF
  cmb_MesF.ValueMember = "id"
  cmb_MesF.DisplayMember = "DescMes"
 End Sub

 Private Sub txtYearI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtYearI.KeyPress
  If Char.IsDigit(e.KeyChar) Then
   e.Handled = False
  ElseIf Char.IsControl(e.KeyChar) Then
   e.Handled = False
  Else
   e.Handled = True
  End If
 End Sub

 Private Sub txtYearF_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtYearF.KeyPress
  If Char.IsDigit(e.KeyChar) Then
   e.Handled = False
  ElseIf Char.IsControl(e.KeyChar) Then
   e.Handled = False
  Else
   e.Handled = True
  End If
 End Sub

 Private Sub CmbSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSucursal.SelectedIndexChanged
  'Filtrar Sucursal de acuerdo al combo
  Dim Cambios As Boolean = False
  Try
   If Me.CmbSucursal.SelectedValue > 0 Then
    DvPorMes.RowFilter = "CveSuc =" & Me.CmbSucursal.SelectedValue
    Cambios = True
    If Me.CmbAgteVta.SelectedValue > 0 Then
     DvPorAgente.RowFilter = "SlpCode =" & Me.CmbAgteVta.SelectedValue
     Cambios = True
    End If
   End If
  Catch ex As Exception

  End Try
 End Sub

 Sub BuscaAgentes()
  Dim AgteActual As Integer
  If CmbSucursal.SelectedValue Is Nothing Or CmbSucursal.SelectedValue = 99 Then
   DvPorMes2.RowFilter = String.Empty
   Me.CmbAgteVta.SelectedValue = 999
   Me.CmbAgteVta.DataSource = DvPorMes2
  Else
   AgteActual = Me.CmbAgteVta.SelectedValue
   DvPorMes2.RowFilter = "GroupCode = " & Trim(Me.CmbSucursal.SelectedValue.ToString) & " OR GroupCode = 999"
   Me.CmbAgteVta.SelectedValue = AgteActual
  End If
 End Sub

 Sub BuscaSucursal()
  Try
   If Not CmbAgteVta.SelectedValue Is Nothing Then
    If Not CmbAgteVta.SelectedValue.Equals(999) Then
     CmbSucursal.SelectedValue = SQL.CampoEspecifico("SELECT DISTINCT T1.GroupCode FROM SBO_TPD.dbo.OSLP t0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T0.SlpCode = " & CmbAgteVta.SelectedValue, "GroupCode")
     BuscaAgentes()
    End If
   End If
  Catch ex As Exception
  End Try
 End Sub

 Private Sub CmbSucursal_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbSucursal.SelectionChangeCommitted
  BuscaAgentes()
 End Sub

 Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
  panelEspere.Visible = True
  panelEspere.Refresh()
  GetInf()
  panelEspere.Visible = False
 End Sub

 Private Sub GetInf()
  Dim vDiasMes As Integer

  Dim FI As Date
  Dim FF As Date
  Dim FT As Date 'Fecha de termino
  Dim DsInf As New DataSet
  Dim DsTmp As New DataSet
  Dim sFI, sFF As String
  Dim Salir As Boolean = False
  Dim Break As Boolean = False

  If txtYearI.Text.Equals("") Or txtYearF.Text.Equals("") Then
   Return
  End If

  FT = Date.Parse(txtYearF.Text & "-" & (cmb_MesF.SelectedValue + 1) & "-01", CultureInfo.InvariantCulture)
  FT = DateSerial(Year(FT), Month(FT) + 1, 0)

  If FT > Now Then
   FT = Now
  End If

  FI = Date.Parse(txtYearI.Text & "-" & (cmb_MesI.SelectedValue + 1) & "-01", CultureInfo.InvariantCulture)
  FF = DateSerial(Year(FI), Month(FI) + 1, 0)

  Do While (FT.Month >= FF.Month Or FT.Year >= FF.Year) And Break = False
   DsTmp = GetResSP(FI.Date, FF.Date, Me.CmbAgteVta.SelectedValue, Me.CmbSucursal.SelectedValue)
   DsInf.Merge(DsTmp, True)

   FI = DateAdd("m", 1, FI)
   FF = DateSerial(Year(FI), Month(FI) + 1, 0)

   If Salir = True Or (FF.Month > FT.Month And FT.Year = FF.Year) Then
    Break = True
   End If

   If FF >= Now Then
    FF = Now
    Salir = True
   End If
  Loop

  DvRes.Table = DsInf.Tables("Informacion")

  If Me.CmbAgteVta.SelectedValue = 999 And Me.CmbSucursal.SelectedValue = 99 Then
   DiseñoTodos()
  ElseIf Me.CmbAgteVta.SelectedValue = 999 And Me.CmbSucursal.SelectedValue <> 99 Then
   DiseñoPorSucursal()
  ElseIf Me.CmbAgteVta.SelectedValue <> 999 And Me.CmbSucursal.SelectedValue <> 99 Then
   DiseñoPorAgente()
  End If

 End Sub

 Private Function GetResSP(FechaInicial As Date, FechaFinal As Date, Agente As Integer, Sucursal As Integer) As DataSet
  Dim cnn As SqlConnection = Nothing
  Dim cmd As SqlCommand = Nothing
  Dim cmd4 As SqlCommand = Nothing
  Dim DsInf As New DataSet
  Try
   cnn = New SqlConnection(StrTpm)
   cnn.Open()
   cmd4 = New SqlCommand("TPD_Historico_Pasado_GetInf", cnn)
   cmd4.CommandType = CommandType.StoredProcedure

   'Siempre traera toda la informacion
   cmd4.Parameters.Add("@FechaInicial", SqlDbType.Date).Value = FechaInicial.Date
   cmd4.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = FechaFinal.Date
   cmd4.Parameters.Add("@Agente", SqlDbType.Int).Value = Agente
   cmd4.Parameters.Add("@Sucursal", SqlDbType.Int).Value = Sucursal
   'cmd4.Parameters.Add("@AgenteVentas", SqlDbType.VarChar, 30).Value = UsrTPM

   cmd4.CommandTimeout = 600
   cmd4.ExecuteNonQuery()
   cmd4.Connection.Close()
   Dim da As New SqlDataAdapter
   da.SelectCommand = cmd4
   da.SelectCommand.Connection = cnn

   ''--------------------------------------------

   da.Fill(DsInf, "Informacion")

   Return DsInf
   'DsInf.Tables(0).TableName = "Totales"
   'DsInf.Tables(1).TableName = "PorMes"
   'DsInf.Tables(2).TableName = "PorAgente"

   'DvTotales.Table = DsInf.Tables("Totales")
   'DvPorMes.Table = DsInf.Tables("PorMes")
   'DvPorAgente.Table = DsInf.Tables("PorAgente")
   'CopiaDvPorAgente.Table = DsInf.Tables("PorAgente")

   'DgTotales.DataSource = DvTotales


  Catch ex As Exception
   'MsgBox(ex.Message)
   Return DsInf
   Exit Function
  Finally
   If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
    cnn.Close()
   End If
  End Try
 End Function

 Private Sub DiseñoTodos()
  '-------Diseño de DATAGRID Agentes
  With Me.dgInf
   Try
    .DataSource = DvRes
    .ReadOnly = True
    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    .DefaultCellStyle.BackColor = Color.AliceBlue
    .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

    dgInf.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    'Propiedad para no mostrar el cuadro que se encuentra en la parte
    'Superior Izquierda del gridview
    .RowHeadersVisible = True
    .ColumnHeadersVisible = True
    .RowHeadersWidth = 5
    .ColumnHeadersHeight = 23

    .Columns(0).HeaderText = "TODAS"
    .Columns(0).Width = 100
    .Columns(0).Visible = False

    .Columns(1).HeaderText = "SUCURSAL"
    .Columns(1).Width = 100
    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(1).DefaultCellStyle.SelectionBackColor = Color.Gray
    .Columns(1).Visible = False

    .Columns(2).HeaderText = "MES"
    .Columns(2).Width = 150
    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(2).DefaultCellStyle.SelectionBackColor = Color.Gray
    .Columns(2).Visible = True

    .Columns(3).HeaderText = "Importe L04t"
    .Columns(3).Width = 90
    .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(4).HeaderText = "%"
    .Columns(4).Width = 50
    .Columns(4).DefaultCellStyle.Format = "0.00 %"
    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(5).HeaderText = "Importe L03"
    .Columns(5).Width = 90
    .Columns(5).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(6).HeaderText = "%"
    .Columns(6).Width = 50
    .Columns(6).DefaultCellStyle.Format = "0.00 %"
    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(7).HeaderText = "Importe L02"
    .Columns(7).Width = 90
    .Columns(7).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(8).HeaderText = "%"
    .Columns(8).Width = 50
    .Columns(8).DefaultCellStyle.Format = "0.00 %"
    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(9).HeaderText = "Importe L01"
    .Columns(9).Width = 90
    .Columns(9).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(10).HeaderText = "%"
    .Columns(10).Width = 50
    .Columns(10).DefaultCellStyle.Format = "0.00 %"
    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(11).HeaderText = "Importe L10"
    .Columns(11).Width = 90
    .Columns(11).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(12).HeaderText = "%"
    .Columns(12).Width = 50
    .Columns(12).DefaultCellStyle.Format = "0.00 %"
    .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(13).HeaderText = "Otros Srv."
    .Columns(13).Width = 100
    .Columns(13).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(14).HeaderText = "Score Card"
    .Columns(14).Width = 110
    .Columns(14).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    For i = 0 To dgInf.RowCount - 1
     dgInf.Item(3, i).Style.BackColor = Color.LightGray
     dgInf.Item(5, i).Style.BackColor = Color.LightGray
     dgInf.Item(7, i).Style.BackColor = Color.LightGray
     dgInf.Item(9, i).Style.BackColor = Color.LightGray
     dgInf.Item(11, i).Style.BackColor = Color.LightGray
     dgInf.Item(13, i).Style.BackColor = Color.LightGray
    Next

    'Pinto el renglo
    Dim numfilas As Integer
    numfilas = dgInf.RowCount 'cuenta las filas del DataGrid

    'recorre las filas del DataGrid
    'For i = 0 To (numfilas - 1)
    ' If dgInf.Item(2, i).Value.ToString().Equals("OBJETIVO") Then
    '  For y = 3 To 14
    '   Select Case y
    '    Case 3, 5, 7, 9, 11, 13, 14
    '     .Rows(i).Cells(y).Style.BackColor = Color.Yellow
    '    Case 4, 6, 8, 10, 12
    '     .Rows(i).Cells(y).Style.BackColor = Color.Orange
    '   End Select
    '  Next
    ' End If
    'Next

    '.AutoResizeColumns()
   Catch ex As Exception
   End Try
  End With
 End Sub

 Private Sub DiseñoPorSucursal()
  With Me.dgInf
   .ReadOnly = True
   .DataSource = DvRes
   'Color de Renglones en Grid
   .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
   .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
   .DefaultCellStyle.BackColor = Color.AliceBlue
   .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

   'Propiedad para no mostrar el cuadro que se encuentra en la parte
   'Superior Izquierda del gridview
   .RowHeadersVisible = True
   .ColumnHeadersVisible = True
   .RowHeadersWidth = 5
   .ColumnHeadersHeight = 23
   dgInf.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   Try
    .Columns(0).HeaderText = "TODAS"
    .Columns(0).Width = 100
    .Columns(0).Visible = False

    .Columns(1).HeaderText = "TODOS"
    .Columns(1).Width = 85
    '.Columns(1).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(1).DefaultCellStyle.SelectionBackColor = Color.Gray
    .Columns(1).Visible = False

    .Columns(2).HeaderText = "MES"
    .Columns(2).Width = 150
    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(2).DefaultCellStyle.SelectionBackColor = Color.Gray
    .Columns(2).Visible = True

    .Columns(3).HeaderText = "Importe L04"
    .Columns(3).Width = 90
    .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(3).DefaultCellStyle.SelectionBackColor = Color.Gray

    .Columns(4).HeaderText = "%"
    .Columns(4).Width = 50
    .Columns(4).DefaultCellStyle.Format = "0.00 %"
    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(5).HeaderText = "Importe L03"
    .Columns(5).Width = 90
    .Columns(5).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(5).DefaultCellStyle.SelectionBackColor = Color.Gray

    .Columns(6).HeaderText = "%"
    .Columns(6).Width = 50
    .Columns(6).DefaultCellStyle.Format = "0.00 %"
    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(7).HeaderText = "Importe L02"
    .Columns(7).Width = 90
    .Columns(7).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(7).DefaultCellStyle.SelectionBackColor = Color.Gray

    .Columns(8).HeaderText = "%"
    .Columns(8).Width = 50
    .Columns(8).DefaultCellStyle.Format = "0.00 %"
    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(9).HeaderText = "Importe L01"
    .Columns(9).Width = 90
    .Columns(9).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(9).DefaultCellStyle.SelectionBackColor = Color.Gray

    .Columns(10).HeaderText = "%"
    .Columns(10).Width = 50
    .Columns(10).DefaultCellStyle.Format = "0.00 %"
    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(11).HeaderText = "Importe L10"
    .Columns(11).Width = 90
    .Columns(11).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(11).DefaultCellStyle.SelectionBackColor = Color.Gray

    .Columns(12).HeaderText = "%"
    .Columns(12).Width = 50
    .Columns(12).DefaultCellStyle.Format = "0.00 %"
    .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(13).HeaderText = "Otros Srv."
    .Columns(13).Width = 100
    .Columns(13).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(14).HeaderText = "Score Card"
    .Columns(14).Width = 110
    .Columns(14).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(15).HeaderText = "num Suc"
    .Columns(15).Width = 110
    .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(15).Visible = False

    For i = 0 To dgInf.RowCount - 1
     dgInf.Item(3, i).Style.BackColor = Color.LightGray
     dgInf.Item(5, i).Style.BackColor = Color.LightGray
     dgInf.Item(7, i).Style.BackColor = Color.LightGray
     dgInf.Item(9, i).Style.BackColor = Color.LightGray
     dgInf.Item(11, i).Style.BackColor = Color.LightGray
     dgInf.Item(13, i).Style.BackColor = Color.LightGray
    Next

    'Pinto el renglo
    Dim numfilas As Integer
    numfilas = dgInf.RowCount 'cuenta las filas del DataGrid

    ''recorre las filas del DataGrid
    'For i = 0 To (numfilas - 1)
    ' If dgInf.Item(2, i).Value.ToString().Equals("OBJETIVO") Then
    '  For y = 3 To 15
    '   Select Case y
    '    Case 3, 5, 7, 9, 11, 13, 14
    '     .Rows(i).Cells(y).Style.BackColor = Color.Yellow
    '    Case 4, 6, 8, 10, 12
    '     .Rows(i).Cells(y).Style.BackColor = Color.Orange
    '   End Select
    '  Next
    ' End If
    'Next

   Catch ex As Exception
   End Try
  End With
 End Sub

 Private Sub DiseñoPorAgente()
  With Me.dgInf
   Try
    .DataSource = DvRes
    .ReadOnly = True
    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    .DefaultCellStyle.BackColor = Color.AliceBlue
    .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

    .RowHeadersVisible = True
    .ColumnHeadersVisible = True
    .RowHeadersWidth = 5
    .ColumnHeadersHeight = 23

    dgInf.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    'Propiedad para no mostrar el cuadro que se encuentra en la parte
    'Superior Izquierda del gridview
    .Columns(0).HeaderText = "AGENTE"
    .Columns(0).Width = 150
    .Columns(0).ReadOnly = True
    .Columns(0).Visible = False

    .Columns(1).HeaderText = "SUCURSAL"
    .Columns(1).Width = 80

    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    .Columns(1).DefaultCellStyle.SelectionBackColor = Color.Gray
    .Columns(1).ReadOnly = True
    .Columns(1).Visible = False

    .Columns(2).HeaderText = "MES"
    .Columns(2).Width = 150
    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(2).DefaultCellStyle.SelectionBackColor = Color.Gray
    .Columns(2).ReadOnly = True

    .Columns(3).HeaderText = "Importe L04"
    .Columns(3).Width = 90
    .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(3).ReadOnly = True

    .Columns(4).HeaderText = "%"
    .Columns(4).Width = 50
    .Columns(4).DefaultCellStyle.Format = "0.00%"
    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '.Columns(4).DefaultCellStyle.Font = New Font("Footlight MT Light ", 6.75, FontStyle.Regular)

    .Columns(5).HeaderText = "Importe L03"
    .Columns(5).Width = 90
    .Columns(5).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(5).ReadOnly = True

    .Columns(6).HeaderText = "%"
    .Columns(6).Width = 50
    .Columns(6).DefaultCellStyle.Format = "0.00 %"
    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(7).HeaderText = "Importe L02"
    .Columns(7).Width = 90
    .Columns(7).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(7).ReadOnly = True

    .Columns(8).HeaderText = "%"
    .Columns(8).Width = 50
    .Columns(8).DefaultCellStyle.Format = "0.00 %"
    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(9).HeaderText = "Importe L01"
    .Columns(9).Width = 90
    .Columns(9).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(9).ReadOnly = True

    .Columns(10).HeaderText = "%"
    .Columns(10).Width = 50
    .Columns(10).DefaultCellStyle.Format = "0.00 %"
    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(11).HeaderText = "Importe L10"
    .Columns(11).Width = 90
    .Columns(11).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(11).ReadOnly = True

    .Columns(12).HeaderText = "%"
    .Columns(12).Width = 50
    .Columns(12).DefaultCellStyle.Format = "0.00 %"
    .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    .Columns(13).HeaderText = "Otros Srv."
    .Columns(13).Width = 100
    .Columns(13).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(13).ReadOnly = True

    .Columns(14).HeaderText = "Score Card"
    .Columns(14).Width = 110
    .Columns(14).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(14).ReadOnly = True

    .Columns(15).HeaderText = "SlpCode"
    .Columns(15).Visible = False

    .Columns(16).HeaderText = "mes"
    .Columns(16).Visible = False

    .Columns(17).HeaderText = "Cve Sucursal"
    .Columns(17).Visible = False

    .Columns(18).HeaderText = "num Suc"
    .Columns(18).Width = 110
    .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(18).Visible = False

    For i = 0 To dgInf.RowCount - 1
     dgInf.Item(3, i).Style.BackColor = Color.LightGray
     dgInf.Item(5, i).Style.BackColor = Color.LightGray
     dgInf.Item(7, i).Style.BackColor = Color.LightGray
     dgInf.Item(9, i).Style.BackColor = Color.LightGray
     dgInf.Item(11, i).Style.BackColor = Color.LightGray
     dgInf.Item(13, i).Style.BackColor = Color.LightGray
    Next

    ''Pinto el renglo
    'Dim numfilas As Integer
    'numfilas = dgInf.RowCount 'cuenta las filas del DataGrid
    'For i = 0 To (numfilas - 1)
    ' If dgInf.Item(2, i).Value.ToString().Equals("OBJETIVO") Then
    '  For y = 3 To 14
    '   Select Case y
    '    Case 3, 5, 7, 9, 11, 13, 14
    '     .Rows(i).Cells(y).Style.BackColor = Color.Yellow

    '    Case 4, 6, 8, 10, 12
    '     .Rows(i).Cells(y).Style.BackColor = Color.Orange
    '     .Rows(i).Cells(y).ReadOnly = False
    '   End Select
    '  Next
    ' Else
    '  For y = 3 To 13
    '   .Rows(i).Cells(y).ReadOnly = True
    '  Next
    ' End If
    'Next

    '.AutoResizeColumns()
   Catch ex As Exception
   End Try
  End With
 End Sub

 Private Sub CmbAgteVta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAgteVta.SelectedIndexChanged
  BuscaSucursal()
 End Sub

 Private Sub dgInf_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgInf.CellContentClick

 End Sub

 Private Sub panelEspere_Paint(sender As Object, e As PaintEventArgs) Handles panelEspere.Paint

 End Sub
End Class