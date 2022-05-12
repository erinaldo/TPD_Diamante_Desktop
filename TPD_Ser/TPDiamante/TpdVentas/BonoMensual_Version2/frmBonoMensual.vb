Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel

Public Class frmBonoMensual
  'Conexiones a la Base de datos
  Public StrProd As String = "Data Source=SERVIDORSAP;Initial Catalog=SBO-Diamante-productiva;User Id=SA;Password=Pr0c3s0.12;"
  Public StrTpm As String = "Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=Pr0c3s0.12;"
  Public StrCon As String = "Data Source=SERVIDORSAP;Initial Catalog=SBO_TPD;User Id=SA;Password=Pr0c3s0.12;"

  Dim DvAgentes As New DataView
  Dim DvCalculos As New DataView
  Dim DvInformacion As New DataView

  Dim DvCboAgentes As New DataView

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

    If slpcode = "" And UsrTPM <> "MANAGER" And UsrTPM <> "COMERCIAL" And UsrTPM <> "SISTEMAS" Then
      CerrarSCClientes = True
      MsgBox("Este usuario no tienen definido el valor de Agte Ventas en su registro", MsgBoxStyle.Exclamation, "Falta configuración de agente ventas")
      Exit Sub
    End If

    If slpcode <> "" Then
      GroupCode = SQL.CampoEspecifico("SELECT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " + slpcode, "GroupCode")
    Else
      GroupCode = SQL.CampoEspecifico("SELECT T1.GroupCode as 'GroupCode' FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 and t0.SlpCode = " + CodAgte, "GroupCode")
    End If

    Dim Almacen As String = SQL.CampoEspecifico("select Almacen from Usuarios where Id_Usuario = '" + UsrTPM + "'", "Almacen")

    'COMBO DE SUCURSALES
    If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Then
      ConsutaListaS = "SELECT GroupCode,GroupName FROM SBO_TPD.dbo.OCRG with (nolock) WHERE GroupType = 'C' ORDER BY GroupName"
    Else
      ConsutaListaS = "SELECT GroupCode,GroupName FROM SBO_TPD.dbo.OCRG with (nolock) WHERE GroupType = 'C' AND GroupCode = '" + GroupCode + "' ORDER BY GroupName"
    End If

    'COMBO DE AGENTES
    If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Then
      'SQL.LlenarComboBox1("SELECT T0.slpcode,T0.slpname FROM SBO_TPD.dbo.OSLP T0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 ORDER BY slpname", "slpcode,slpname", cmbAgteVta)
      ConsutaListaA = "SELECT T0.SlpCode,SlpName,T1.GroupCode FROM SBO_TPD.dbo.OSLP t0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') ORDER BY SlpName"

    ElseIf UsrTPM = "RROBLES" Or UsrTPM = "VVERGARA" Or UsrTPM = "VENTAS5" Then
      ConsutaListaA = "SELECT T0.SlpCode,SlpName,T1.GroupCode FROM SBO_TPD.dbo.OSLP t0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') AND Memo = '" + Almacen + "' AND T1.GroupCode = 102 ORDER BY slpname"
    Else
      'SI ES AGENETE DE MARKETING/VENTAS
      ConsutaListaA = "SELECT T0.SlpCode,SlpName,T1.GroupCode FROM SBO_TPD.dbo.OSLP t0 INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode WHERE U_VENTAS = '" + UsrTPM + "' AND (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') ORDER BY slpname"
    End If

    '*********************************************************************************************************************************************

    'Variable para guardar la consulta de AGENTES y SUCURSALES en los combobox

    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
      Dim DSetTablas As New DataSet

      'ConsutaLista = "SELECT GroupCode , GroupName FROM OCRG with (nolock) WHERE GroupType = 'C' "

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

      DvCboAgentes.Table = DSetTablas.Tables("Agentes")

      Me.CmbAgteVta.DataSource = DvCboAgentes
      Me.CmbAgteVta.DisplayMember = "slpname"
      Me.CmbAgteVta.ValueMember = "slpcode"
      Me.CmbAgteVta.SelectedValue = 999

    End Using

    SQL.Cerrar()
  End Sub

  Sub BuscaAgentes()
    If CmbSucursal.SelectedValue Is Nothing Or CmbSucursal.SelectedValue = 99 Then
      DvCboAgentes.RowFilter = String.Empty
      Me.CmbAgteVta.SelectedValue = 999
      Me.CmbAgteVta.DataSource = DvCboAgentes
    Else
      DvCboAgentes.RowFilter = String.Empty
      Me.CmbAgteVta.SelectedValue = 999
      DvCboAgentes.RowFilter = "GroupCode = " & Trim(Me.CmbSucursal.SelectedValue.ToString) & " OR GroupCode = 999"
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
    DgAgentes.DataSource = Nothing
    DgCalculoComisiones.DataSource = Nothing
    Buscar_Informacion()
  End Sub

  Sub Buscar_Informacion()
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
      Dim cmd = New SqlCommand("[TPD_ScoreCard_BonoMensual]", conec)
      cmd.CommandType = CommandType.StoredProcedure
      cmd.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = Me.DtpFechaIni.Value
      cmd.Parameters.Add("@DiasMes", SqlDbType.Int).Value = vDiasMes
      cmd.Parameters.Add("@DiasTrans", SqlDbType.Int).Value = vDiasTrans
      cmd.Parameters.Add("@agente", SqlDbType.Int).Value = CmbAgteVta.SelectedValue
      cmd.Parameters.Add("@Sucursal", SqlDbType.Int).Value = CmbSucursal.SelectedValue
      cmd.Parameters.Add("@DiasRest", SqlDbType.Int).Value = vDiasMes - vDiasTrans
      cmd.Parameters.Add("@AgenteVentas", SqlDbType.VarChar).Value = UsrTPM

      cmd.Parameters.Add("@ImporteBono", SqlDbType.Decimal).Value = 3000
      cmd.Parameters.Add("@PorcentajeMinimo", SqlDbType.Decimal).Value = 95

      conec.Open()
      Dim adaptador = New SqlDataAdapter()
      adaptador.SelectCommand = cmd
      adaptador.SelectCommand.Connection = conec
      adaptador.SelectCommand.CommandTimeout = 10000
      cmd.ExecuteNonQuery()
      cmd.Connection.Close()
      conec.Close()
      adaptador.Fill(DsVtas)

      DsVtas.Tables(0).TableName = "Agentes"
      DsVtas.Tables(1).TableName = "Calculos"

      DvAgentes.Table = DsVtas.Tables("Agentes")
      DvCalculos.Table = DsVtas.Tables("Calculos")

      DgAgentes.DataSource = DvAgentes
      EstiloAgentes()

      DgCalculoComisiones.DataSource = DvCalculos
      EstiloCalculos()

      SQL.Cerrar()
    Catch ex As Exception
      MessageBox.Show(ex.ToString, "Error al ejecutar los procedimientos almacenados")
    End Try
  End Sub

  '************************************************************************
  'INICIAN ESTILOS
  '************************************************************************
  Sub EstiloAgentes()
    With Me.DgAgentes
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
        .Columns("Agente").Width = 200

        .Columns("Sucursal").HeaderText = "Sucursal"
        .Columns("Sucursal").Width = 200

        .Columns("PorcentajeMinimo").HeaderText = "% Mínimo"
        .Columns("PorcentajeMinimo").Width = 90
        .Columns("PorcentajeMinimo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("ImporteBono").HeaderText = "Importe del Bono"
        .Columns("ImporteBono").Width = 105
        .Columns("ImporteBono").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("ImporteBono").DefaultCellStyle.Format = " ###,##0"
      Catch ex As Exception
        'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView")
      End Try
    End With
  End Sub

  Sub EstiloCalculos()
    With Me.DgCalculoComisiones
      .ReadOnly = True
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .RowHeadersVisible = True
      .RowHeadersWidth = 25
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try
        .Columns("Concepto").HeaderText = "Concepto"
        .Columns("Concepto").Width = 50
        .Columns("Concepto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("PorcPropocionBono").HeaderText = "propoción del bono (%)"
        .Columns("PorcPropocionBono").Width = 90
        .Columns("PorcPropocionBono").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("PorcPropocionBono").DefaultCellStyle.Format = "#0.#0 %"

        .Columns("PesosPropocionBono").HeaderText = "Propoción del bono ($)"
        .Columns("PesosPropocionBono").Width = 105
        .Columns("PesosPropocionBono").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("PesosPropocionBono").DefaultCellStyle.Format = " ###,##0"

        .Columns("Objetivo").HeaderText = "Objetivo a alcanzar"
        .Columns("Objetivo").Width = 105
        .Columns("Objetivo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '.Columns("Objetivo").DefaultCellStyle.Format = " ###,##0"

        .Columns("ObjetivoAlcanzado").HeaderText = "Objetivo alcanzado"
        .Columns("ObjetivoAlcanzado").Width = 105
        .Columns("ObjetivoAlcanzado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '.Columns("ObjetivoAlcanzado").DefaultCellStyle.Format = " ###,##0"

        .Columns("PorcentajeAlcanzado").HeaderText = "Porcentaje alcanzado (%)"
        .Columns("PorcentajeAlcanzado").Width = 90
        .Columns("PorcentajeAlcanzado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("PorcentajeAlcanzado").DefaultCellStyle.Format = "#0.#0 %"

        .Columns("BonoObtenido").HeaderText = "Bono obtenido"
        .Columns("BonoObtenido").Width = 105
        .Columns("BonoObtenido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns("BonoObtenido").DefaultCellStyle.Format = " ###,##0"

        Dim numfilas As Integer
        Dim Porcent As Decimal
        numfilas = DgCalculoComisiones.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)
          If DgCalculoComisiones.Item(5, i).Value Is DBNull.Value Then
            DgCalculoComisiones.Item(5, i).Value = 0
            Porcent = 0
          Else
            Porcent = DgCalculoComisiones.Item(5, i).Value
          End If

          If Porcent < 0.95 Then
            DgCalculoComisiones.Rows(i).Cells(5).Style.BackColor = Color.Red
          ElseIf Porcent >= 0.85 And Porcent < 1 Then
            DgCalculoComisiones.Rows(i).Cells(5).Style.BackColor = Color.Yellow
          ElseIf Porcent >= 1 Then
            DgCalculoComisiones.Rows(i).Cells(5).Style.BackColor = Color.LimeGreen
          End If

        Next

      Catch ex As Exception
        'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView")
      End Try
    End With
  End Sub

  '************************************************************************
  'TERMINAN ESTILOS
  '************************************************************************
  'Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
  '  Dim form As Form = ScoreCardCteObj
  '  form.Show()
  'End Sub

  'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
  '  Dim oExcel As Object
  '  Dim oBook As Object
  '  Dim oSheet As Object

  '  Dim Rangos As String = ""
  '  Dim Rangos2 As String = ""

  '  'MsgBox("El reporte se creara a continuación")
  '  'Abrimos un nuevo libro
  '  oExcel = CreateObject("Excel.Application")
  '  oBook = oExcel.workbooks.add
  '  oSheet = oBook.worksheets(1)


  '  'Declaramos el nombre de las columnas
  '  oSheet.range("A3").value = "Clave Agte."
  '  oSheet.range("B3").value = "Agente"
  '  oSheet.range("C3").value = "Acumulado"
  '  oSheet.range("D3").value = "Objetivo"
  '  oSheet.range("E3").value = "Acumulado Vs Objetivo (%)"
  '  oSheet.range("F3").value = "Ctes. min. requeridos a la fecha"


  '  'para poner la primera fila de los titulos en negrita
  '  oSheet.range("A3:F3").font.bold = True
  '  Dim fila_dt As Integer = 0
  '  Dim fila_dt_excel As Integer = 0
  '  Dim tanto_porcentaje As String = ""
  '  Dim marikona As Integer = 0

  '  Dim total_reg As Integer = 0

  '  total_reg = DgTotales.RowCount
  '  For fila_dt = 0 To total_reg - 1

  '    'para leer una celda en concreto
  '    'el numero es la columna
  '    Dim cel0 As String = IIf(IsDBNull(Me.DgTotales.Item(0, fila_dt).Value), 0, Me.DgTotales.Item(0, fila_dt).Value)
  '    Dim cel1 As String = IIf(IsDBNull(Me.DgTotales.Item(1, fila_dt).Value), 0, Me.DgTotales.Item(1, fila_dt).Value)
  '    Dim cel2 As String = IIf(IsDBNull(Me.DgTotales.Item(2, fila_dt).Value), 0, Me.DgTotales.Item(2, fila_dt).Value)
  '    Dim cel3 As String = IIf(IsDBNull(Me.DgTotales.Item(3, fila_dt).Value), 0, Me.DgTotales.Item(3, fila_dt).Value)
  '    Dim cel4 As String = IIf(IsDBNull(Me.DgTotales.Item(4, fila_dt).Value), 0, Me.DgTotales.Item(4, fila_dt).Value * 100)
  '    Dim cel5 As String = IIf(IsDBNull(Me.DgTotales.Item(5, fila_dt).Value), 0, Me.DgTotales.Item(5, fila_dt).Value)

  '    fila_dt_excel = fila_dt + 4

  '    'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
  '    oSheet.range("A" & fila_dt_excel).value = FormatNumber(cel0, 0)
  '    oSheet.range("B" & fila_dt_excel).value = cel1
  '    oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 0)
  '    oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 0)
  '    oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
  '    oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 0)
  '  Next

  '  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  '  oSheet.columns("A:F").entirecolumn.autofit()

  '  'ENCABEZADO DEL REPORTE GENERADO

  '  Dim sqlConnection1 As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=SBO_TPD;User Id=SA;Password=Pr0c3s0.12;")
  '  Dim cmd As New SqlCommand
  '  Dim returnValue As Object

  '  cmd.CommandText = "SELECT slpname from oslp where slpcode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString)
  '  cmd.CommandType = CommandType.Text
  '  cmd.Connection = sqlConnection1

  '  sqlConnection1.Open()

  '  returnValue = cmd.ExecuteScalar()

  '  sqlConnection1.Close()

  '  Dim cnn As SqlConnection = Nothing


  '  If CmbAgteVta.SelectedValue = 999 Then
  '    oSheet.range("A1").value = "Reporte de Score Card Clientes TOTALES de todos los AGENTES con objetivos con FECHA " + Format(Me.DtpFechaIni.Value)
  '  Else
  '    oSheet.range("A1").value = "Reporte de Score Card Clientes TOTALES del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
  '  End If

  '  oSheet.range("C1").value = Rangos
  '  oSheet.range("C2").value = Rangos2
  '  'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

  '  oExcel.visible = True
  '  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  '  GC.Collect()
  '  oSheet = Nothing
  '  oBook = Nothing
  '  oExcel = Nothing
  'End Sub

  'Private Sub ImpExcelPorCte_Click(sender As Object, e As EventArgs) Handles ImpExcelPorCte.Click
  '  Dim oExcel As Object
  '  Dim oBook As Object
  '  Dim oSheet As Object

  '  Dim Rangos As String = ""
  '  Dim Rangos2 As String = ""

  '  'MsgBox("El reporte se creara a continuación")
  '  'Abrimos un nuevo libro
  '  oExcel = CreateObject("Excel.Application")
  '  oBook = oExcel.workbooks.add
  '  oSheet = oBook.worksheets(1)


  '  'Declaramos el nombre de las columnas
  '  oSheet.range("A3").value = "Cve. Cte."
  '  oSheet.range("B3").value = "Cliente"
  '  oSheet.range("C3").value = "Objetivo"
  '  oSheet.range("D3").value = "Acumulado"
  '  oSheet.range("E3").value = "Acumulado Vs Objetivo (%)"
  '  oSheet.range("F3").value = "Faltante para objetivo"
  '  oSheet.range("G3").value = "Agente"
  '  oSheet.range("H3").value = "Sucursal"
  '  oSheet.range("I3").value = "Ruta"
  '  oSheet.range("J3").value = "Estatus"

  '  'para poner la primera fila de los titulos en negrita
  '  oSheet.range("A3:J3").font.bold = True
  '  Dim fila_dt As Integer = 0
  '  Dim fila_dt_excel As Integer = 0
  '  Dim tanto_porcentaje As String = ""
  '  Dim marikona As Integer = 0

  '  Dim total_reg As Integer = 0

  '  total_reg = DgPorCliente.RowCount
  '  For fila_dt = 0 To total_reg - 1

  '    'para leer una celda en concreto
  '    'el numero es la columna
  '    Dim cel0 As String = IIf(IsDBNull(Me.DgPorCliente.Item(0, fila_dt).Value), 0, Me.DgPorCliente.Item(0, fila_dt).Value)
  '    Dim cel1 As String = IIf(IsDBNull(Me.DgPorCliente.Item(1, fila_dt).Value), 0, Me.DgPorCliente.Item(1, fila_dt).Value)
  '    Dim cel2 As String = IIf(IsDBNull(Me.DgPorCliente.Item(2, fila_dt).Value), 0, Me.DgPorCliente.Item(2, fila_dt).Value)
  '    Dim cel3 As String = IIf(IsDBNull(Me.DgPorCliente.Item(3, fila_dt).Value), 0, Me.DgPorCliente.Item(3, fila_dt).Value)
  '    Dim cel4 As String = IIf(IsDBNull(Me.DgPorCliente.Item(4, fila_dt).Value), 0, Me.DgPorCliente.Item(4, fila_dt).Value * 100)
  '    Dim cel5 As String = IIf(IsDBNull(Me.DgPorCliente.Item(5, fila_dt).Value), 0, Me.DgPorCliente.Item(5, fila_dt).Value)
  '    Dim cel6 As String = IIf(IsDBNull(Me.DgPorCliente.Item(7, fila_dt).Value), 0, Me.DgPorCliente.Item(7, fila_dt).Value)
  '    Dim cel7 As String = IIf(IsDBNull(Me.DgPorCliente.Item(9, fila_dt).Value), 0, Me.DgPorCliente.Item(9, fila_dt).Value)
  '    Dim cel8 As String = IIf(IsDBNull(Me.DgPorCliente.Item(10, fila_dt).Value), 0, Me.DgPorCliente.Item(10, fila_dt).Value)
  '    Dim cel9 As String = IIf(IsDBNull(Me.DgPorCliente.Item(11, fila_dt).Value), 0, Me.DgPorCliente.Item(11, fila_dt).Value)

  '    fila_dt_excel = fila_dt + 4

  '    'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
  '    oSheet.range("A" & fila_dt_excel).value = cel0
  '    oSheet.range("B" & fila_dt_excel).value = cel1
  '    oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
  '    oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
  '    oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
  '    oSheet.range("F" & fila_dt_excel).value = cel5
  '    oSheet.range("G" & fila_dt_excel).value = cel6
  '    oSheet.range("H" & fila_dt_excel).value = cel7
  '    oSheet.range("I" & fila_dt_excel).value = cel8
  '    oSheet.range("J" & fila_dt_excel).value = cel9
  '  Next

  '  ' para que el tamano de la columna tenga como minimo el maximo de sus textos
  '  oSheet.columns("A:J").entirecolumn.autofit()

  '  'ENCABEZADO DEL REPORTE GENERADO

  '  Dim sqlConnection1 As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=SBO_TPD;User Id=SA;Password=Pr0c3s0.12;")
  '  Dim cmd As New SqlCommand
  '  Dim returnValue As Object

  '  cmd.CommandText = "SELECT slpname from oslp where slpcode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString)
  '  cmd.CommandType = CommandType.Text
  '  cmd.Connection = sqlConnection1

  '  sqlConnection1.Open()

  '  returnValue = cmd.ExecuteScalar()

  '  sqlConnection1.Close()

  '  Dim cnn As SqlConnection = Nothing


  '  If CmbAgteVta.SelectedValue = 999 Then
  '    oSheet.range("A1").value = "Reporte de Score Card Clientes por cliente de todos los AGENTES con FECHA " + Format(Me.DtpFechaIni.Value)
  '  Else
  '    oSheet.range("A1").value = "Reporte de Score Card Clientes por cliente del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
  '  End If

  '  oSheet.range("C1").value = Rangos
  '  oSheet.range("C2").value = Rangos2

  '  oExcel.visible = True
  '  System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
  '  GC.Collect()
  '  oSheet = Nothing
  '  oBook = Nothing
  '  oExcel = Nothing
  'End Sub

  Private Sub ScoreCardCliente_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    iniciar()
  End Sub

  'Private Sub DgTotales_CurrentCellChanged(sender As Object, e As EventArgs) Handles DgTotales.CurrentCellChanged
  '  Try
  '    If (IsNothing(DvAgentes) = False) Then
  '      If DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString = "999" Then
  '        DvParametros.RowFilter = "CveAgt =" & DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString
  '        EstiloDgParametros()
  '        DvParametros.RowFilter = "CveAgt =" & DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString
  '        EstiloDgParametros()
  '      Else
  '        DvParametros.RowFilter = "CveAgt =" & DgAgentes.Item(0, DgAgentes.CurrentRow.Index).Value.ToString
  '        EstiloDgParametros()
  '      End If
  '    End If
  '  Catch ex As Exception

  '  End Try
  'End Sub
End Class