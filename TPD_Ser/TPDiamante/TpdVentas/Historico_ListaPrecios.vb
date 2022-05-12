Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class Historico_ListaPrecios
 Dim DvTotales As New DataView
 Dim DvPorMes As New DataView
 Dim DvPorMes2 As New DataView
 Dim DvPorAgente As New DataView
 Dim CopiaDvPorAgente As New DataView
 Dim DvPorAgente2 As New DataView

 ''VARIABLE PARA LA CLASE COMANDOS SQL  
 Dim SQL As New Comandos_SQL()

 Private Sub VtasScoreCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
   'YA NO SE PODRAN ELEGIR A TODAS LAS SUCURSALES
   '********************************************************************
   'If DSetTablas.Tables("Sucursales").Rows.Count > 1 Then
   ' Dim fila As Data.DataRow
   ' 'Asignamos a fila la nueva Row(Fila)del Dataset
   ' fila = DSetTablas.Tables("Sucursales").NewRow
   ' 'Agregamos los valores a los campos de la tabla
   ' fila("GroupName") = "TODAS"
   ' fila("GroupCode") = 99

   ' 'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
   ' DSetTablas.Tables("Sucursales").Rows.Add(fila)
   'End If
   '********************************************************************
   'YA NO SE PODRAN ELEGIR A TODAS LAS SUCURSALES
   '********************************************************************

   Me.CmbSucursal.DataSource = DSetTablas.Tables("Sucursales")
   Me.CmbSucursal.DisplayMember = "GroupName"
   Me.CmbSucursal.ValueMember = "GroupCode"
   Me.CmbSucursal.SelectedIndex = 0
   'If DSetTablas.Tables("Sucursales").Rows.Count > 1 Then
   ' Me.CmbSucursal.SelectedValue = 99
   'Else
   ' Me.CmbSucursal.SelectedIndex = 0
   'End If

   '---------------------------------------------------------
   Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaListaA, SqlConnection)

   daAgte.Fill(DSetTablas, "Agentes")

   Dim filaAgte As Data.DataRow

   'Asignamos a fila la nueva Row(Fila)del Dataset
   filaAgte = DSetTablas.Tables("Agentes").NewRow

   '********************************************************************
   'YA NO SE PODRA ELEGIR ATODOS
   '********************************************************************
   ''Agregamos los valores a los campos de la tabla
   'filaAgte("slpname") = "TODOS"
   'filaAgte("slpcode") = 999
   'filaAgte("GroupCode") = 999

   ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
   'DSetTablas.Tables("Agentes").Rows.Add(filaAgte)
   '********************************************************************

   DvPorMes2.Table = DSetTablas.Tables("Agentes")

   Me.CmbAgteVta.DataSource = DvPorMes2
   Me.CmbAgteVta.DisplayMember = "slpname"
   Me.CmbAgteVta.ValueMember = "slpcode"

   BuscaAgentes()

   'Me.CmbAgteVta.SelectedValue = 999
  End Using

  SQL.Cerrar()

  Consulta()
 End Sub

 Sub BuscaAgentes()
  DvPorMes2.RowFilter = String.Empty
  Me.CmbAgteVta.SelectedValue = 0
  DvPorMes2.RowFilter = "GroupCode = " & Trim(Me.CmbSucursal.SelectedValue.ToString) & " OR GroupCode = 999"
 End Sub

 Private Sub CmbSucursal_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbSucursal.SelectionChangeCommitted
  BuscaAgentes()
 End Sub

 Private Sub CmbSucursal_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbSucursal.Validating
  'BuscaAgentes()
 End Sub

 Private Sub CmbSucursal_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbSucursal.KeyUp
  'BuscaAgentes()
 End Sub


 Private Sub Consulta()
  Espere(True)
  DgPorMes.DataSource = Nothing
  GetInf()
  Espere(False)
 End Sub

 Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
  Espere(True)
  DgPorMes.DataSource = Nothing
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
   cmd4 = New SqlCommand("TPD_Historico_AgenteListaPrecio2", cnn)
   cmd4.CommandType = CommandType.StoredProcedure

   'Siempre traera toda la informacion
   cmd4.Parameters.Add("@Sucursal", SqlDbType.Int).Value = 99
   cmd4.Parameters.Add("@Agente", SqlDbType.VarChar, 30).Value = 999
   cmd4.Parameters.Add("@AgenteVentas", SqlDbType.VarChar, 30).Value = UsrTPM

   'cmd4.Parameters.Add("@Sucursal", SqlDbType.Int).Value = Me.CmbSucursal.SelectedValue
   'cmd4.Parameters.Add("@Agente", SqlDbType.VarChar, 30).Value = Me.CmbAgteVta.SelectedValue
   'cmd4.Parameters.Add("@AgenteVentas", SqlDbType.VarChar, 30).Value = UsrTPM

   cmd4.CommandTimeout = 600
   cmd4.ExecuteNonQuery()
   cmd4.Connection.Close()
   Dim da As New SqlDataAdapter
   da.SelectCommand = cmd4
   da.SelectCommand.Connection = cnn

   ''--------------------------------------------
   Dim DsInf As New DataSet
   da.Fill(DsInf, "Informacion")

   DsInf.Tables(0).TableName = "Totales"
   DsInf.Tables(1).TableName = "PorMes"
   DsInf.Tables(2).TableName = "PorAgente"

   DvTotales.Table = DsInf.Tables("Totales")
   DvPorMes.Table = DsInf.Tables("PorMes")
   DvPorAgente.Table = DsInf.Tables("PorAgente")
   CopiaDvPorAgente.Table = DsInf.Tables("PorAgente")

   DgTotales.DataSource = DvTotales
   DgPorMes.DataSource = DvPorMes
   DgPorAgente.DataSource = DvPorAgente '

   CopiaDgAgente.DataSource = CopiaDvPorAgente

  Catch ex As Exception
   'MsgBox(ex.Message)
   Exit Sub
  Finally
   If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
    cnn.Close()
   End If
  End Try

  '-------Diseño de DATAGRID Resumen
  With Me.DgTotales
   .ReadOnly = True
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
   DgTotales.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

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
    .Columns(2).Width = 250
    '.Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(2).DefaultCellStyle.SelectionBackColor = Color.Gray

    .Columns(3).HeaderText = "Importe L04"
    .Columns(3).Width = 90
    .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(3).DefaultCellStyle.SelectionBackColor = Color.Gray

    .Columns(4).HeaderText = "%"
    .Columns(4).Width = 50
    .Columns(4).DefaultCellStyle.Format = "0.00 %"
    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .DefaultCellStyle.Font = New Font(.DefaultFont, 7)

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

    .Columns(15).HeaderText = "Comp. %"
    .Columns(15).Width = 60
    .Columns(15).DefaultCellStyle.Format = "0.00 %"
    .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(15).DefaultCellStyle.Font = New Font(.DefaultFont, FontStyle.Bold = True)

    .Columns(16).HeaderText = "Comp. $"
    .Columns(16).Width = 110
    .Columns(16).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(16).DefaultCellStyle.Font = New Font(.DefaultFont, FontStyle.Bold = True)

    For i = 0 To DgTotales.RowCount - 1
     DgTotales.Item(3, i).Style.BackColor = Color.LightGray
     DgTotales.Item(5, i).Style.BackColor = Color.LightGray
     DgTotales.Item(7, i).Style.BackColor = Color.LightGray
     DgTotales.Item(9, i).Style.BackColor = Color.LightGray
     DgTotales.Item(11, i).Style.BackColor = Color.LightGray
     DgTotales.Item(13, i).Style.BackColor = Color.LightGray
    Next

    'Pinto el renglo
    Dim numfilas As Integer
    numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid

    'recorre las filas del DataGrid

    For i = 0 To (numfilas - 1)
     If DgTotales.Item(2, i).Value.ToString().Equals("OBJETIVO") Then
      For y = 3 To 15
       Select Case y
        Case 3, 5, 7, 9, 11, 13, 14
         .Rows(i).Cells(y).Style.BackColor = Color.Yellow
        Case 4, 6, 8, 10, 12
         .Rows(i).Cells(y).Style.BackColor = Color.Orange
       End Select
      Next
     End If
    Next

   Catch ex As Exception
   End Try
  End With

  DiseñoPorMes()
  DiseñoPorAgente()

  'Filtrar Sucursal de acuerdo al combo
  DvPorMes.RowFilter = "CveSuc =" & Me.CmbSucursal.SelectedValue

  'Filtrar Agente de acuerdo al combo
  DvPorAgente.RowFilter = "SlpCode =" & Me.CmbAgteVta.SelectedValue

  If DgPorMes.Rows.Count > 0 Then
   DgPorMes.Rows(0).Selected = True
  End If

  RealizaCalculos()
 End Sub

 Private Sub DiseñoPorMes()
  '-------Diseño de DATAGRID Agentes
  With Me.DgPorMes
   Try
    '.DataSource = DtAgte
    .ReadOnly = True
    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    .DefaultCellStyle.BackColor = Color.AliceBlue
    .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

    DgPorMes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
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
    '.Columns(1).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(1).DefaultCellStyle.SelectionBackColor = Color.Gray

    .Columns(2).HeaderText = "MES"
    .Columns(2).Width = 150
    '.Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(2).DefaultCellStyle.SelectionBackColor = Color.Gray

    .Columns(3).HeaderText = "Importe L04"
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

    .Columns(15).HeaderText = "Comp. %"
    .Columns(15).Width = 60
    .Columns(15).DefaultCellStyle.Format = "0.00 %"
    .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(15).DefaultCellStyle.Font = New Font(.DefaultFont, FontStyle.Bold)

    .Columns(16).HeaderText = "Comp. $"
    .Columns(16).Width = 110
    .Columns(16).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(16).DefaultCellStyle.Font = New Font(.DefaultFont, FontStyle.Bold)

    .Columns(17).HeaderText = "Cve Sucursal"
    .Columns(17).Visible = False

    For i = 0 To DgPorMes.RowCount - 1
     DgPorMes.Item(3, i).Style.BackColor = Color.LightGray
     DgPorMes.Item(5, i).Style.BackColor = Color.LightGray
     DgPorMes.Item(7, i).Style.BackColor = Color.LightGray
     DgPorMes.Item(9, i).Style.BackColor = Color.LightGray
     DgPorMes.Item(11, i).Style.BackColor = Color.LightGray
     DgPorMes.Item(13, i).Style.BackColor = Color.LightGray
    Next

    'Pinto el renglo
    Dim numfilas As Integer
    numfilas = DgPorMes.RowCount 'cuenta las filas del DataGrid

    'recorre las filas del DataGrid

    For i = 0 To (numfilas - 1)
     If DgPorMes.Item(2, i).Value.ToString().Equals("OBJETIVO") Then
      For y = 3 To 14
       Select Case y
        Case 3, 5, 7, 9, 11, 13, 14
         .Rows(i).Cells(y).Style.BackColor = Color.Yellow
        Case 4, 6, 8, 10, 12
         .Rows(i).Cells(y).Style.BackColor = Color.Orange
       End Select
      Next
     End If
    Next

    '.AutoResizeColumns()
   Catch ex As Exception
   End Try
  End With
 End Sub

 Private Sub DiseñoPorAgente()
  With Me.DgPorAgente
   Try
    .ReadOnly = False
    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    .DefaultCellStyle.BackColor = Color.AliceBlue
    .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

    .RowHeadersVisible = True
    .ColumnHeadersVisible = True
    .RowHeadersWidth = 5
    .ColumnHeadersHeight = 23

    DgPorAgente.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    'Propiedad para no mostrar el cuadro que se encuentra en la parte
    'Superior Izquierda del gridview
    .Columns(0).HeaderText = "AGENTE"
    .Columns(0).Width = 150
    .Columns(0).ReadOnly = True

    .Columns(1).HeaderText = "SUCURSAL"
    .Columns(1).Width = 80
    '.Columns(1).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    .Columns(1).DefaultCellStyle.SelectionBackColor = Color.Gray
    .Columns(1).ReadOnly = True
    .Columns(1).Visible = False

    .Columns(2).HeaderText = "MES"
    .Columns(2).Width = 100
    '.Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
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

    .Columns(15).HeaderText = "Comp. %"
    .Columns(15).Width = 60
    .Columns(15).DefaultCellStyle.Format = "0.00 %"
    .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(15).ReadOnly = True
    .Columns(15).DefaultCellStyle.Font = New Font(.DefaultFont, FontStyle.Bold)

    .Columns(16).HeaderText = "Comp. $"
    .Columns(16).Width = 110
    .Columns(16).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(16).DefaultCellStyle.Font = New Font(.DefaultFont, FontStyle.Bold)
    .Columns(16).Visible = True

    .Columns(17).HeaderText = "SlpCode"
    .Columns(17).Visible = False

    .Columns(18).HeaderText = "mes"
    .Columns(18).Visible = False

    .Columns(19).HeaderText = "Año"
    .Columns(19).Visible = False

    .Columns(20).HeaderText = "Cve Sucursal"
    .Columns(20).Visible = False

    For i = 0 To DgPorAgente.RowCount - 1
     DgPorAgente.Item(3, i).Style.BackColor = Color.LightGray
     DgPorAgente.Item(5, i).Style.BackColor = Color.LightGray
     DgPorAgente.Item(7, i).Style.BackColor = Color.LightGray
     DgPorAgente.Item(9, i).Style.BackColor = Color.LightGray
     DgPorAgente.Item(11, i).Style.BackColor = Color.LightGray
     DgPorAgente.Item(13, i).Style.BackColor = Color.LightGray
    Next

    'Pinto el renglo
    Dim numfilas As Integer
    numfilas = DgPorAgente.RowCount 'cuenta las filas del DataGrid
    For i = 0 To (numfilas - 1)
     If DgPorAgente.Item(2, i).Value.ToString().Equals("OBJETIVO") Then
      For y = 3 To 14
       Select Case y
        Case 3, 5, 7, 9, 11, 13, 14
         .Rows(i).Cells(y).Style.BackColor = Color.Yellow

        Case 4, 6, 8, 10, 12
         .Rows(i).Cells(y).Style.BackColor = Color.Orange
         .Rows(i).Cells(y).ReadOnly = False
       End Select
      Next
     Else
      For y = 3 To 13
       .Rows(i).Cells(y).ReadOnly = True
      Next
     End If
    Next

    '.AutoResizeColumns()
   Catch ex As Exception
   End Try
  End With
 End Sub

 '---Generar reporte en EXCEL de TOTALES
 Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnExportaAgentes.Click
  ExportarTotales()
 End Sub

 Sub ExportarTotales()
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
  oSheet.range("A3").value = "Mes"
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
  oSheet.range("L3").value = "Otros Servicios"
  oSheet.range("M3").value = "Score Card"
  oSheet.range("N3").value = "Comprobación %"
  oSheet.range("O3").value = "Comprobación $"

  'para poner la primera fila de los titulos en negrita
  oSheet.range("A3:O3").font.bold = True
  oSheet.Range("A3:O3").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGray)
  Dim fila_dt As Integer = 0
  Dim fila_dt_excel As Integer = 0
  Dim tanto_porcentaje As String = ""
  Dim marikona As Integer = 0

  Dim total_reg As Integer = 0

  total_reg = DgTotales.RowCount
  For fila_dt = 0 To total_reg - 1
   'para leer una celda en concreto
   'el numero es la columna
   Dim cel0 As String = IIf(IsDBNull(Me.DgTotales.Item(2, fila_dt).Value), 0, Me.DgTotales.Item(2, fila_dt).Value)
   Dim cel1 As String = IIf(IsDBNull(Me.DgTotales.Item(3, fila_dt).Value), 0, Me.DgTotales.Item(3, fila_dt).Value)
   Dim cel2 As String = IIf(IsDBNull(Me.DgTotales.Item(4, fila_dt).Value), 0, Me.DgTotales.Item(4, fila_dt).Value)
   Dim cel3 As String = IIf(IsDBNull(Me.DgTotales.Item(5, fila_dt).Value), 0, Me.DgTotales.Item(5, fila_dt).Value)
   Dim cel4 As String = IIf(IsDBNull(Me.DgTotales.Item(6, fila_dt).Value), 0, Me.DgTotales.Item(6, fila_dt).Value)
   Dim cel5 As String = IIf(IsDBNull(Me.DgTotales.Item(7, fila_dt).Value), 0, Me.DgTotales.Item(7, fila_dt).Value)
   Dim cel6 As String = IIf(IsDBNull(Me.DgTotales.Item(8, fila_dt).Value), 0, Me.DgTotales.Item(8, fila_dt).Value)
   Dim cel7 As String = IIf(IsDBNull(Me.DgTotales.Item(9, fila_dt).Value), 0, Me.DgTotales.Item(9, fila_dt).Value)
   Dim cel8 As String = IIf(IsDBNull(Me.DgTotales.Item(10, fila_dt).Value), 0, Me.DgTotales.Item(10, fila_dt).Value)
   Dim cel9 As String = IIf(IsDBNull(Me.DgTotales.Item(11, fila_dt).Value), 0, Me.DgTotales.Item(11, fila_dt).Value)
   Dim cel10 As String = IIf(IsDBNull(Me.DgTotales.Item(12, fila_dt).Value), 0, Me.DgTotales.Item(12, fila_dt).Value)
   Dim cel11 As String = IIf(IsDBNull(Me.DgTotales.Item(13, fila_dt).Value), 0, Me.DgTotales.Item(13, fila_dt).Value)
   Dim cel12 As String = IIf(IsDBNull(Me.DgTotales.Item(14, fila_dt).Value), 0, Me.DgTotales.Item(14, fila_dt).Value)
   Dim cel13 As String = IIf(IsDBNull(Me.DgTotales.Item(15, fila_dt).Value), 0, Me.DgTotales.Item(15, fila_dt).Value)
   Dim cel14 As String = IIf(IsDBNull(Me.DgTotales.Item(16, fila_dt).Value), 0, Me.DgTotales.Item(16, fila_dt).Value)

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
   oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel12, 2)
   oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel13, 4)
   oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel14, 2)
  Next

  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  oSheet.columns("A:l").entirecolumn.autofit()

  'Coloco autofiltro
  Dim objRangoFiltro As Microsoft.Office.Interop.Excel.Range = oSheet.Range("A3:O3")
  objRangoFiltro.AutoFilter(1)

  'Coloco colores en columnas igual que en la pantalla
  oSheet.Range("B4:B" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("B" & fila_dt_excel.ToString & ":B" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("C" & fila_dt_excel.ToString & ":C" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("D4:D" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("D" & fila_dt_excel.ToString & ":D" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("E" & fila_dt_excel.ToString & ":E" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("F4:F" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("F" & fila_dt_excel.ToString & ":F" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("G" & fila_dt_excel.ToString & ":G" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("H4:H" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("H" & fila_dt_excel.ToString & ":H" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("I" & fila_dt_excel.ToString & ":I" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("J4:J" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("J" & fila_dt_excel.ToString & ":J" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("K" & fila_dt_excel.ToString & ":K" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("L" & fila_dt_excel.ToString & ":L" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("M" & fila_dt_excel.ToString & ":M" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)

  'Formato numerico
  oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("I").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("K").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("N").NumberFormat = "0.00 %"

  oSheet.range("A1").value = "Reporte histórico de Ventas por totales por listas de precio al dia " & Now()

  oSheet.range("C1").value = Rangos
  oSheet.range("C2").value = Rangos2

  oExcel.visible = True
  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  GC.Collect()
  oSheet = Nothing
  oBook = Nothing
  oExcel = Nothing
 End Sub

 Sub exportaSucursales()
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
  oSheet.range("A3").value = "Sucursal"
  oSheet.range("B3").value = "Mes"
  oSheet.range("C3").value = "Importe linea 4"
  oSheet.range("D3").value = "% linea 4"
  oSheet.range("E3").value = "Importe linea 3"
  oSheet.range("F3").value = "% linea 3"
  oSheet.range("G3").value = "Importe linea 2"
  oSheet.range("H3").value = "% linea 2"
  oSheet.range("I3").value = "Importe linea 1"
  oSheet.range("J3").value = "% linea 1"
  oSheet.range("K3").value = "Importe linea 10"
  oSheet.range("L3").value = "% linea 10"
  oSheet.range("M3").value = "Otros Servicios"
  oSheet.range("N3").value = "Score Card"
  oSheet.range("O3").value = "Comprobación %"
  oSheet.range("P3").value = "Comprobación $"

  'para poner la primera fila de los titulos en negrita
  oSheet.range("A3:P3").font.bold = True
  oSheet.Range("A3:P3").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGray)
  Dim fila_dt As Integer = 0
  Dim fila_dt_excel As Integer = 0
  Dim tanto_porcentaje As String = ""
  Dim marikona As Integer = 0

  Dim total_reg As Integer = 0

  total_reg = DgPorMes.RowCount
  For fila_dt = 0 To total_reg - 1
   'para leer una celda en concreto
   'el numero es la columna
   Dim cel0 As String = IIf(IsDBNull(Me.DgPorMes.Item(1, fila_dt).Value), 0, Me.DgPorMes.Item(1, fila_dt).Value)
   Dim cel1 As String = IIf(IsDBNull(Me.DgPorMes.Item(2, fila_dt).Value), 0, Me.DgPorMes.Item(2, fila_dt).Value)
   Dim cel2 As String = IIf(IsDBNull(Me.DgPorMes.Item(3, fila_dt).Value), 0, Me.DgPorMes.Item(3, fila_dt).Value)
   Dim cel3 As String = IIf(IsDBNull(Me.DgPorMes.Item(4, fila_dt).Value), 0, Me.DgPorMes.Item(4, fila_dt).Value)
   Dim cel4 As String = IIf(IsDBNull(Me.DgPorMes.Item(5, fila_dt).Value), 0, Me.DgPorMes.Item(5, fila_dt).Value)
   Dim cel5 As String = IIf(IsDBNull(Me.DgPorMes.Item(6, fila_dt).Value), 0, Me.DgPorMes.Item(6, fila_dt).Value)
   Dim cel6 As String = IIf(IsDBNull(Me.DgPorMes.Item(7, fila_dt).Value), 0, Me.DgPorMes.Item(7, fila_dt).Value)
   Dim cel7 As String = IIf(IsDBNull(Me.DgPorMes.Item(8, fila_dt).Value), 0, Me.DgPorMes.Item(8, fila_dt).Value)
   Dim cel8 As String = IIf(IsDBNull(Me.DgPorMes.Item(9, fila_dt).Value), 0, Me.DgPorMes.Item(9, fila_dt).Value)
   Dim cel9 As String = IIf(IsDBNull(Me.DgPorMes.Item(10, fila_dt).Value), 0, Me.DgPorMes.Item(10, fila_dt).Value)
   Dim cel10 As String = IIf(IsDBNull(Me.DgPorMes.Item(11, fila_dt).Value), 0, Me.DgPorMes.Item(11, fila_dt).Value)
   Dim cel11 As String = IIf(IsDBNull(Me.DgPorMes.Item(12, fila_dt).Value), 0, Me.DgPorMes.Item(12, fila_dt).Value)
   Dim cel12 As String = IIf(IsDBNull(Me.DgPorMes.Item(13, fila_dt).Value), 0, Me.DgPorMes.Item(13, fila_dt).Value)
   Dim cel13 As String = IIf(IsDBNull(Me.DgPorMes.Item(14, fila_dt).Value), 0, Me.DgPorMes.Item(14, fila_dt).Value)
   Dim cel14 As String = IIf(IsDBNull(Me.DgPorMes.Item(15, fila_dt).Value), 0, Me.DgPorMes.Item(15, fila_dt).Value)
   Dim cel15 As String = IIf(IsDBNull(Me.DgPorMes.Item(16, fila_dt).Value), 0, Me.DgPorMes.Item(16, fila_dt).Value)

   fila_dt_excel = fila_dt + 4

   'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
   oSheet.range("A" & fila_dt_excel).value = cel0
   oSheet.range("B" & fila_dt_excel).value = cel1
   oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
   oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 4)
   oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
   oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 4)
   oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 2)
   oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 4)
   oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 2)
   oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 4)
   oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 2)
   oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 4)
   oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel12, 2)
   oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel13, 2)
   oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel14, 4)
   oSheet.range("P" & fila_dt_excel).value = FormatNumber(cel15, 2)
  Next

  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  oSheet.columns("A:l").entirecolumn.autofit()

  'Coloco autofiltro
  Dim objRangoFiltro As Microsoft.Office.Interop.Excel.Range = oSheet.Range("A1:P3")
  objRangoFiltro.AutoFilter(1)

  'Coloco colores en columnas igual que en la pantalla
  oSheet.Range("C4:C" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("C" & fila_dt_excel.ToString & ":C" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("D" & fila_dt_excel.ToString & ":D" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("E4:E" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("E" & fila_dt_excel.ToString & ":E" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("F" & fila_dt_excel.ToString & ":F" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("G4:G" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("G" & fila_dt_excel.ToString & ":G" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("H" & fila_dt_excel.ToString & ":H" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("I4:I" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("I" & fila_dt_excel.ToString & ":I" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("J" & fila_dt_excel.ToString & ":J" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("K4:K" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("K" & fila_dt_excel.ToString & ":K" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("L" & fila_dt_excel.ToString & ":L" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("M" & fila_dt_excel.ToString & ":M" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("N" & fila_dt_excel.ToString & ":N" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)

  'Formato numerico
  oExcel.Worksheets("Hoja1").Columns("D").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("H").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("J").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("L").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("O").NumberFormat = "0.00 %"

  oSheet.range("A1").value = "Reporte histórico de Ventas por listas de precio por sucursal al día " & Now()

  oSheet.range("C1").value = Rangos
  oSheet.range("C2").value = Rangos2

  oExcel.visible = True
  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  GC.Collect()
  oSheet = Nothing
  oBook = Nothing
  oExcel = Nothing
 End Sub

 Sub exportaAgentes()
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
  oSheet.range("B3").value = "Mes"
  oSheet.range("C3").value = "Importe linea 4"
  oSheet.range("D3").value = "% linea 4"
  oSheet.range("E3").value = "Importe linea 3"
  oSheet.range("F3").value = "% linea 3"
  oSheet.range("G3").value = "Importe linea 2"
  oSheet.range("H3").value = "% linea 2"
  oSheet.range("I3").value = "Importe linea 1"
  oSheet.range("J3").value = "% linea 1"
  oSheet.range("K3").value = "Importe linea 10"
  oSheet.range("L3").value = "% linea 10"
  oSheet.range("M3").value = "Otros Servicios"
  oSheet.range("N3").value = "Score Card"
  oSheet.range("O3").value = "Comprobación %"
  oSheet.range("P3").value = "Comprobación $"

  'para poner la primera fila de los titulos en negrita
  oSheet.range("A3:P3").font.bold = True
  oSheet.Range("A3:P3").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGray)
  Dim fila_dt As Integer = 0
  Dim fila_dt_excel As Integer = 0
  Dim tanto_porcentaje As String = ""
  Dim marikona As Integer = 0

  Dim total_reg As Integer = 0

  total_reg = DgPorAgente.RowCount
  For fila_dt = 0 To total_reg - 1
   'para leer una celda en concreto
   'el numero es la columna
   Dim cel0 As String = IIf(IsDBNull(Me.DgPorAgente.Item(0, fila_dt).Value), 0, Me.DgPorAgente.Item(0, fila_dt).Value)
   Dim cel1 As String = IIf(IsDBNull(Me.DgPorAgente.Item(2, fila_dt).Value), 0, Me.DgPorAgente.Item(2, fila_dt).Value)
   Dim cel2 As String = IIf(IsDBNull(Me.DgPorAgente.Item(3, fila_dt).Value), 0, Me.DgPorAgente.Item(3, fila_dt).Value)
   Dim cel3 As String = IIf(IsDBNull(Me.DgPorAgente.Item(4, fila_dt).Value), 0, Me.DgPorAgente.Item(4, fila_dt).Value)
   Dim cel4 As String = IIf(IsDBNull(Me.DgPorAgente.Item(5, fila_dt).Value), 0, Me.DgPorAgente.Item(5, fila_dt).Value)
   Dim cel5 As String = IIf(IsDBNull(Me.DgPorAgente.Item(6, fila_dt).Value), 0, Me.DgPorAgente.Item(6, fila_dt).Value)
   Dim cel6 As String = IIf(IsDBNull(Me.DgPorAgente.Item(7, fila_dt).Value), 0, Me.DgPorAgente.Item(7, fila_dt).Value)
   Dim cel7 As String = IIf(IsDBNull(Me.DgPorAgente.Item(8, fila_dt).Value), 0, Me.DgPorAgente.Item(8, fila_dt).Value)
   Dim cel8 As String = IIf(IsDBNull(Me.DgPorAgente.Item(9, fila_dt).Value), 0, Me.DgPorAgente.Item(9, fila_dt).Value)
   Dim cel9 As String = IIf(IsDBNull(Me.DgPorAgente.Item(10, fila_dt).Value), 0, Me.DgPorAgente.Item(10, fila_dt).Value)
   Dim cel10 As String = IIf(IsDBNull(Me.DgPorAgente.Item(11, fila_dt).Value), 0, Me.DgPorAgente.Item(11, fila_dt).Value)
   Dim cel11 As String = IIf(IsDBNull(Me.DgPorAgente.Item(12, fila_dt).Value), 0, Me.DgPorAgente.Item(12, fila_dt).Value)
   Dim cel12 As String = IIf(IsDBNull(Me.DgPorAgente.Item(13, fila_dt).Value), 0, Me.DgPorAgente.Item(13, fila_dt).Value)
   Dim cel13 As String = IIf(IsDBNull(Me.DgPorAgente.Item(14, fila_dt).Value), 0, Me.DgPorAgente.Item(14, fila_dt).Value)
   Dim cel14 As String = IIf(IsDBNull(Me.DgPorAgente.Item(15, fila_dt).Value), 0, Me.DgPorAgente.Item(15, fila_dt).Value)
   Dim cel15 As String = IIf(IsDBNull(Me.DgPorAgente.Item(16, fila_dt).Value), 0, Me.DgPorAgente.Item(16, fila_dt).Value)

   fila_dt_excel = fila_dt + 4

   'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
   oSheet.range("A" & fila_dt_excel).value = cel0
   oSheet.range("B" & fila_dt_excel).value = cel1
   oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
   oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 4)
   oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
   oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 4)
   oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 2)
   oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 4)
   oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel8, 2)
   oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 4)
   oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 2)
   oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 4)
   oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel12, 2)
   oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel13, 2)
   oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel14, 4)
   oSheet.range("P" & fila_dt_excel).value = FormatNumber(cel15, 2)
  Next

  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  oSheet.columns("A:l").entirecolumn.autofit()

  'Coloco autofiltro
  Dim objRangoFiltro As Microsoft.Office.Interop.Excel.Range = oSheet.Range("A1:P3")
  objRangoFiltro.AutoFilter(1)

  'Coloco colores en columnas igual que en la pantalla
  oSheet.Range("C4:C" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("C" & fila_dt_excel.ToString & ":C" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("D" & fila_dt_excel.ToString & ":D" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("E4:E" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("E" & fila_dt_excel.ToString & ":E" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("F" & fila_dt_excel.ToString & ":F" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("G4:G" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("G" & fila_dt_excel.ToString & ":G" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("H" & fila_dt_excel.ToString & ":H" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("I4:I" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("I" & fila_dt_excel.ToString & ":I" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("J" & fila_dt_excel.ToString & ":J" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("K4:K" & (fila_dt_excel - 1).ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
  oSheet.Range("K" & fila_dt_excel.ToString & ":K" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("L" & fila_dt_excel.ToString & ":L" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)

  oSheet.Range("M" & fila_dt_excel.ToString & ":M" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
  oSheet.Range("N" & fila_dt_excel.ToString & ":N" & fila_dt_excel.ToString).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)

  'Formato numerico
  oExcel.Worksheets("Hoja1").Columns("D").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("H").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("J").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("L").NumberFormat = "0.00 %"
  oExcel.Worksheets("Hoja1").Columns("O").NumberFormat = "0.00 %"

  oSheet.range("A1").value = "Reporte histórico de Ventas por listas de precio por Agente al día " & Now()

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
  exportaSucursales()
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

 Private Sub DgTotales_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgTotales.CellEnter
  'If DgTotales.Item(2, DgTotales.CurrentRow.Index).Value.ToString.Equals("SEMESTRE") Or DgTotales.Item(2, DgTotales.CurrentRow.Index).Value.ToString.Equals("CALCULOS") Then
  ' DvPorMes.RowFilter = ""
  'Else
  ' 'DvPorMes.RowFilter = "mesDesc ='" & DgTotales.Item(2, DgTotales.CurrentRow.Index).Value.ToString & "'"
  'End If

  'DiseñoPorMes()
  RealizaCalculos()
 End Sub

 Private Sub DgPorMes_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgPorMes.CellEnter
  'Try
  ' If DgPorMes.Item(1, DgPorMes.CurrentRow.Index).Value.ToString.Equals("SEMESTRE") Or DgPorMes.Item(1, DgPorMes.CurrentRow.Index).Value.ToString.Equals("CALCULOS") Then
  '  DvPorAgente.RowFilter = ""
  ' Else
  '  'DvPorAgente.RowFilter = "Sucursal = '" & DgPorMes.Item(1, DgPorMes.CurrentRow.Index).Value.ToString & "'"
  ' End If
  ' DiseñoPorAgente()

  'Catch ex As Exception
  ' MsgBox("Error:" & ex.Message)
  'End Try
  RealizaCalculos()
 End Sub

 Private Sub RealizaCalculos()
  'MsgBox(DgPorAgente.Item(e.ColumnIndex, e.RowIndex).Value)

  With Me.DgPorAgente
   'For Each Fila As DataGridViewRow In .Rows
   ' Dim strVariable As String = Fila.Cells(1).Value.ToString()
   'Next

   'Sumo por sucursal
   Dim numfilas As Integer
   numfilas = DgPorAgente.RowCount 'cuenta las filas del DataGrid

   If numfilas <= 0 Then Exit Sub

   Dim ObjetivoComp As Decimal

   Dim SumaPuebla() As Decimal
   Dim SumaMerida() As Decimal
   Dim SumaTuxtla() As Decimal
   Dim SumaTotal() As Decimal

   ReDim SumaPuebla(14) 'La posicion 13 sera para el objetivo
   ReDim SumaMerida(14) 'La posicion 13 sera para el objetivo
   ReDim SumaTuxtla(14) 'La posicion 13 sera para el objetivo
   ReDim SumaTotal(14)

   Dim Mensual_SumaPuebla() As Decimal
   Dim Mensual_SumaMerida() As Decimal
   Dim Mensual_SumaTuxtla() As Decimal
   Dim Mensual_SumaTotal() As Decimal

   ReDim Mensual_SumaPuebla(14) 'La posicion 13 sera para el objetivo
   ReDim Mensual_SumaMerida(14) 'La posicion 13 sera para el objetivo
   ReDim Mensual_SumaTuxtla(14) 'La posicion 13 sera para el objetivo
   ReDim Mensual_SumaTotal(14)

   'Recorro todas las filas del grid Copia
   Try
    For i = 0 To (CopiaDgAgente.RowCount - 1)
     If CopiaDgAgente.Item(2, i).Value.ToString().Equals("OBJETIVO") Then
      For y = 3 To 14 Step 2
       Select Case CopiaDgAgente.Item(1, i).Value.ToString()
        Case "Puebla"
         Mensual_SumaPuebla(y) = Mensual_SumaPuebla(y) + Decimal.Parse(CopiaDgAgente.Item(y, i).Value.ToString)
        Case "Mérida"
         Mensual_SumaMerida(y) = Mensual_SumaMerida(y) + Decimal.Parse(CopiaDgAgente.Item(y, i).Value.ToString)
        Case "Tuxtla Gutierrez"
         Mensual_SumaTuxtla(y) = Mensual_SumaTuxtla(y) + Decimal.Parse(CopiaDgAgente.Item(y, i).Value.ToString)
       End Select
      Next
     End If
    Next
   Catch ex As Exception
    'MsgBox(ex.Message)
   End Try

   'recorre las filas del DataGrid
   For i = 0 To (numfilas - 1)
    If DgPorAgente.Item(2, i).Value.ToString().Equals("OBJETIVO") Then
     'Repinto Celdas

     'Termino de Repintar
     For p = 0 To DgTotales.RowCount - 1
      DgPorAgente.Item(3, p).Style.BackColor = Color.LightGray
      DgPorAgente.Item(5, p).Style.BackColor = Color.LightGray
      DgPorAgente.Item(7, p).Style.BackColor = Color.LightGray
      DgPorAgente.Item(9, p).Style.BackColor = Color.LightGray
      DgPorAgente.Item(11, p).Style.BackColor = Color.LightGray
      DgPorAgente.Item(13, p).Style.BackColor = Color.LightGray
     Next

     For y = 3 To 14
      Select Case y
       Case 3, 5, 7, 9, 11, 13, 14
        DgPorAgente.Rows(i).Cells(y).Style.BackColor = Color.Yellow
       Case 4, 6, 8, 10, 12
        DgPorAgente.Rows(i).Cells(y).Style.BackColor = Color.Orange
      End Select
     Next

     For y = 3 To 14 Step 2
      If y < 12 Then
       .Rows(i).Cells(y).Value = .Rows(i).Cells(y + 1).Value * .Item(14, i).Value
      End If

      Select Case DgPorAgente.Item(1, i).Value.ToString()
       Case "Puebla"
        SumaPuebla(y) = SumaPuebla(y) + Decimal.Parse(DgPorAgente.Item(y, i).Value.ToString)
       Case "Mérida"
        SumaMerida(y) = SumaMerida(y) + Decimal.Parse(DgPorAgente.Item(y, i).Value.ToString)
       Case "Tuxtla Gutierrez"
        SumaTuxtla(y) = SumaTuxtla(y) + Decimal.Parse(DgPorAgente.Item(y, i).Value.ToString)
      End Select
     Next

     'Lleno columna de comprobacion
     Dim Comprobacion As Decimal = 0
     Dim Comprobacion2 As Decimal = 0
     For y = 4 To 12 Step 2
      Comprobacion = Comprobacion + Decimal.Parse(DgPorAgente.Item(y, i).Value.ToString)
      Comprobacion2 = Comprobacion2 + Decimal.Parse(DgPorAgente.Item(y - 1, i).Value.ToString)
     Next
     DgPorAgente.Item(15, i).Value = Comprobacion
     DgPorAgente.Item(16, i).Value = Comprobacion2

     If ((Comprobacion >= 0.99 And Comprobacion <= 1.01) Or Comprobacion = 0) Then
      DgPorAgente.Item(15, i).Style.BackColor = Color.LawnGreen
     Else
      DgPorAgente.Item(15, i).Style.BackColor = Color.OrangeRed
     End If
     If ((Comprobacion2 = DgPorAgente.Rows(i).Cells(14).Value) Or Comprobacion2 = 0) Then
      DgPorAgente.Item(16, i).Style.BackColor = Color.LawnGreen
     Else
      DgPorAgente.Item(16, i).Style.BackColor = Color.OrangeRed
     End If
    End If
   Next

   '***************************************************************************************************
   'CALCULOS PARA MESE
   '***************************************************************************************************
   'Coloca los resultados en las celdas correspondientes
   numfilas = DgPorMes.RowCount 'cuenta las filas del DataGrid
   For i = 0 To (numfilas - 1)
    If DgPorMes.Item(2, i).Value.ToString().Equals("OBJETIVO") Then
     'Repinto Sucursales
     For p = 0 To DgTotales.RowCount - 1
      DgPorMes.Item(3, p).Style.BackColor = Color.LightGray
      DgPorMes.Item(5, p).Style.BackColor = Color.LightGray
      DgPorMes.Item(7, p).Style.BackColor = Color.LightGray
      DgPorMes.Item(9, p).Style.BackColor = Color.LightGray
      DgPorMes.Item(11, p).Style.BackColor = Color.LightGray
      DgPorMes.Item(13, p).Style.BackColor = Color.LightGray
     Next

     For y = 3 To 14
      Select Case y
       Case 3, 5, 7, 9, 11, 13, 14
        DgPorMes.Rows(i).Cells(y).Style.BackColor = Color.Yellow
       Case 4, 6, 8, 10, 12
        DgPorMes.Rows(i).Cells(y).Style.BackColor = Color.Orange
      End Select
     Next

     For y = 3 To 14 Step 2
      Select Case DgPorMes.Item(1, i).Value.ToString()
       Case "Puebla"
        DgPorMes.Item(y, i).Value = Mensual_SumaPuebla(y)
       Case "Mérida"
        DgPorMes.Item(y, i).Value = Mensual_SumaMerida(y)
       Case "Tuxtla Gutierrez"
        DgPorMes.Item(y, i).Value = Mensual_SumaTuxtla(y)
      End Select
     Next

     For y = 4 To 12 Step 2
      If DgPorMes.Item(14, i).Value > 0 Then
       DgPorMes.Item(y, i).Value = DgPorMes.Item(y - 1, i).Value / DgPorMes.Item(14, i).Value
      Else
       DgPorMes.Item(y, i).Value = 0
      End If
     Next

     'Lleno columna de comprobacion
     Dim ComprobacionSuc As Decimal = 0
     Dim ComprobacionSuc2 As Decimal = 0
     For y = 4 To 12 Step 2
      ComprobacionSuc = ComprobacionSuc + Decimal.Parse(DgPorMes.Item(y, i).Value.ToString)
      ComprobacionSuc2 = ComprobacionSuc2 + Decimal.Parse(DgPorMes.Item(y - 1, i).Value.ToString)
     Next
     DgPorMes.Item(15, i).Value = ComprobacionSuc
     DgPorMes.Item(16, i).Value = ComprobacionSuc2

     If ((ComprobacionSuc >= 0.99 And ComprobacionSuc <= 1.01) Or ComprobacionSuc = 0) Then
      DgPorMes.Item(15, i).Style.BackColor = Color.LawnGreen
     Else
      DgPorMes.Item(15, i).Style.BackColor = Color.OrangeRed
     End If
     If ((ComprobacionSuc2 = DgPorMes.Rows(i).Cells(14).Value) Or ComprobacionSuc2 = 0) Then
      DgPorMes.Item(16, i).Style.BackColor = Color.LawnGreen
     Else
      DgPorMes.Item(16, i).Style.BackColor = Color.OrangeRed
     End If

    End If
   Next

   '***************************************************************************************************
   'CALCULOS PARA TOTALES
   '***************************************************************************************************
   For y = 1 To 12 Step 2
    Mensual_SumaTotal(y) = Mensual_SumaPuebla(y) + Mensual_SumaMerida(y) + Mensual_SumaTuxtla(y)
   Next

   For p = 0 To DgTotales.RowCount - 1
    DgTotales.Item(3, p).Style.BackColor = Color.LightGray
    DgTotales.Item(5, p).Style.BackColor = Color.LightGray
    DgTotales.Item(7, p).Style.BackColor = Color.LightGray
    DgTotales.Item(9, p).Style.BackColor = Color.LightGray
    DgTotales.Item(11, p).Style.BackColor = Color.LightGray
    DgTotales.Item(13, p).Style.BackColor = Color.LightGray
   Next

   'Sumo por Total
   numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid
   For i = 0 To (numfilas - 1)
    If DgTotales.Item(2, i).Value.ToString().Equals("OBJETIVO") Then
     'Repinto totales
     For y = 3 To 14
      Select Case y
       Case 3, 5, 7, 9, 11, 13, 14
        DgTotales.Rows(i).Cells(y).Style.BackColor = Color.Yellow
       Case 4, 6, 8, 10, 12
        DgTotales.Rows(i).Cells(y).Style.BackColor = Color.Orange
      End Select
     Next

     For y = 3 To 14 Step 2
      DgTotales.Item(y, i).Value = Mensual_SumaTotal(y)
     Next

     For y = 4 To 12 Step 2
      If DgTotales.Item(14, i).Value > 0 Then
       DgTotales.Item(y, i).Value = DgTotales.Item(y - 1, i).Value / DgTotales.Item(14, i).Value
      Else
       DgTotales.Item(y, i).Value = 0
      End If
     Next

     'Lleno columna de comprobacion
     Dim ComprobacionTot As Decimal = 0
     Dim ComprobacionTot2 As Decimal = 0
     For y = 4 To 12 Step 2
      ComprobacionTot = ComprobacionTot + Decimal.Parse(DgTotales.Item(y, i).Value.ToString)
      ComprobacionTot2 = ComprobacionTot2 + Decimal.Parse(DgTotales.Item(y - 1, i).Value.ToString)
     Next
     DgTotales.Item(15, i).Value = ComprobacionTot
     DgTotales.Item(16, i).Value = ComprobacionTot2
     If ((ComprobacionTot >= 0.99 And ComprobacionTot <= 1.01) Or ComprobacionTot = 0) Then
      DgTotales.Item(15, i).Style.BackColor = Color.LawnGreen
     Else
      DgTotales.Item(15, i).Style.BackColor = Color.OrangeRed
     End If
     If ((ComprobacionTot2 = DgTotales.Rows(i).Cells(14).Value) Or ComprobacionTot2 = 0) Then
      DgTotales.Item(16, i).Style.BackColor = Color.LawnGreen
     Else
      DgTotales.Item(16, i).Style.BackColor = Color.OrangeRed
     End If

    End If
   Next

  End With
 End Sub

 Private Sub DgPorAgente_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgPorAgente.CellEndEdit

  Dim Objetivo As Double = 0

  'Realizo calculos
  With Me.DgPorAgente

   Objetivo = .Item(15, e.RowIndex).Value
   'Calculo renglon
   For y = 3 To 11 Step 2
    .Rows(e.RowIndex).Cells(y).Value = .Rows(e.RowIndex).Cells(y + 1).Value * Objetivo
   Next

   '****************************************************************************************************
   'Grabo los datos en la base
   '****************************************************************************************************
   Dim Agente As Integer = DgPorAgente.Item(17, e.RowIndex).Value
   Dim Mes As Integer = DgPorAgente.Item(18, e.RowIndex - 1).Value 'Tomamos el ultimo mes o mes actual
   Dim Anio As Integer = DgPorAgente.Item(19, e.RowIndex - 1).Value 'Tomamos el ultimo año o año actual
   Dim cadena As String = ""
   Dim pLista() As Decimal
   ReDim pLista(4)

   For Each Fila As DataGridViewRow In CopiaDgAgente.Rows
    'Dim strVariable As String = Fila.Cells(1).Value.ToString()
    If Fila.Cells(17).Value = Agente And Fila.Cells(2).Value.ToString() = "OBJETIVO" Then
     For y = 4 To 12 Step 2
      Fila.Cells(y).Value = DgPorAgente.Rows(e.RowIndex).Cells(y).Value
     Next
     For y = 3 To 11 Step 2
      Fila.Cells(y).Value = Fila.Cells(y + 1).Value * Fila.Cells(14).Value
     Next

     'Fila.Cells(6).Value = DgPorAgente.Rows(e.RowIndex).Cells(6).Value
     'Fila.Cells(8).Value = DgPorAgente.Rows(e.RowIndex).Cells(8).Value
     'Fila.Cells(10).Value = DgPorAgente.Rows(e.RowIndex).Cells(10).Value
     'Fila.Cells(12).Value = DgPorAgente.Rows(e.RowIndex).Cells(12).Value

     'Fila.Cells(3).Value = Fila.Cells(12).Value * Fila.Cells(14).Value
     'Fila.Cells(3).Value = Fila.Cells(12).Value * Fila.Cells(14).Value
     'Fila.Cells(3).Value = Fila.Cells(12).Value * Fila.Cells(14).Value
     'Fila.Cells(3).Value = Fila.Cells(12).Value * Fila.Cells(14).Value
     'Fila.Cells(3).Value = Fila.Cells(12).Value * Fila.Cells(14).Value
    End If
   Next

   For y = 3 To 11 Step 2
    Select Case y
     Case 3
      pLista(0) = DgPorAgente.Item(y + 1, e.RowIndex).Value.ToString
     Case 5
      pLista(1) = DgPorAgente.Item(y + 1, e.RowIndex).Value.ToString
     Case 7
      pLista(2) = DgPorAgente.Item(y + 1, e.RowIndex).Value.ToString
     Case 9
      pLista(3) = DgPorAgente.Item(y + 1, e.RowIndex).Value.ToString
     Case 11
      pLista(4) = DgPorAgente.Item(y + 1, e.RowIndex).Value.ToString
    End Select
   Next
   cadena = "SET porcientoL4 = " & pLista(0) & ", porcientoL3 =" & pLista(1) & ", porcientoL2 =" & pLista(2) & ", porcientoL1 =" & pLista(3) & ", porcientoL10 =" & pLista(4) & ", Objetivo = " & Objetivo.ToString()
   ActualizaBD(Agente, Mes, Anio, cadena)
   '****************************************************************************************************
   '****************************************************************************************************
  End With

  RealizaCalculos()
 End Sub

 Private Sub ActualizaBD(Agente As Integer, mes As Integer, anio As Integer, cadena As String)
  Dim con As New SqlConnection
  Dim cmd As New SqlCommand
  Dim CadenaSQL As String = ""
  CadenaSQL = "UPDATE Historico_ListaPrecios " & cadena & " WHERE mes = " & mes & " AND anio = " & anio & " AND SlpCode = " & Agente

  Try
   con.ConnectionString = StrTpm
   con.Open()
   cmd.Connection = con
   cmd.CommandText = CadenaSQL
   cmd.ExecuteNonQuery()

  Catch ex As Exception
   MessageBox.Show("Error Actualizar Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
  Finally
   con.Close()
  End Try
 End Sub

 Private Sub DgPorAgente_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DgPorAgente.EditingControlShowing
  ' referencia a la celda  
  Dim validar As TextBox = CType(e.Control, TextBox)

  ' agregar el controlador de eventos para el KeyPress  
  AddHandler validar.KeyPress, AddressOf validar_Keypress
 End Sub

 Private Sub validar_Keypress(
        ByVal sender As Object,
        ByVal e As System.Windows.Forms.KeyPressEventArgs)

  ' Obtener caracter  
  Dim caracter As Char = e.KeyChar

  ' comprobar si el caracter es un número o el retroceso  
  If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False And Not (caracter = ".") Then
   'Me.Text = e.KeyChar  
   e.KeyChar = Chr(0)
  End If
 End Sub

 Private Sub DgPorAgente_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgPorAgente.CellEnter
  RealizaCalculos()
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
  If Cambios = True Then RealizaCalculos()
 End Sub

 Private Sub CmbAgteVta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAgteVta.SelectedIndexChanged
  'Filtrar Agente de acuerdo al combo
  Try
   If Me.CmbAgteVta.SelectedValue > 0 Then
    DvPorAgente.RowFilter = "SlpCode =" & Me.CmbAgteVta.SelectedValue

   End If
  Catch ex As Exception

  End Try
 End Sub

 Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
  exportaAgentes()
 End Sub


End Class