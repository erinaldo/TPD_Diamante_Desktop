Imports System.Data.SqlClient

Public Class SCGeneral
  'Conexiones a la Base de datos
  Public StrProd As String = conexion_universal.CadenaSBO_Diamante
  Public StrTpm As String = conexion_universal.CadenaSQL
  Public StrCon As String = conexion_universal.CadenaSQLSAP

  Dim DvTotales As New DataView
  Dim DvAgentes As New DataView
  Dim DvAgentes2 As New DataView

  Dim SQL As New Comandos_SQL()

  Private Sub SCClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")

    'Variable para guardar la consulta de AGENTES y SUCURSALES en los combobox
    Dim ConsutaLista As String
    Dim ConsutaListaS As String
    Dim ConsutaListaA As String

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

    'COMBO DE SUCURSALES
    If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Then
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

    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
      Dim DSetTablas As New DataSet
      'ConsutaLista = "SELECT GroupCode , GroupName FROM OCRG with (nolock) WHERE GroupType = 'C' ORDER BY GroupName "
      Dim daGSucural As New SqlClient.SqlDataAdapter(ConsutaListaS, SqlConnection)

      'Dim DSetTablas As New DataSet
      daGSucural.Fill(DSetTablas, "Sucursales")

      Dim fila As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      fila = DSetTablas.Tables("Sucursales").NewRow

      'Agregamos los valores a los campos de la tabla
      fila("GroupName") = "TODAS"
      fila("GroupCode") = 99

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Sucursales").Rows.Add(fila)

      Me.CmbSucursal.DataSource = DSetTablas.Tables("Sucursales")
      Me.CmbSucursal.DisplayMember = "GroupName"
      Me.CmbSucursal.ValueMember = "GroupCode"
      Me.CmbSucursal.SelectedValue = 99
      '---------------------------------------------------------
      'ConsutaLista = "SELECT T0.slpcode,T0.slpname,T1.GroupCode FROM OSLP T0 "
      'ConsutaLista &= "INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
      'ConsutaLista &= "WHERE T1.CbrGralAdicional = 'N' AND T0.SLPCODE <> 1 AND (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC') ORDER BY slpname "

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

      Dim conec = New SqlConnection(StrTpm)
      Dim cmd = New SqlCommand("[TPD_ScoreCard_Final]", conec)
      cmd.CommandType = CommandType.StoredProcedure
      cmd.Parameters.Add("@fFechaFinal", SqlDbType.Date).Value = Me.DtpFechaIni.Value
      cmd.Parameters.Add("@fDiasTrans", SqlDbType.Int).Value = vDiasTrans
      cmd.Parameters.Add("@fDiasMes", SqlDbType.Int).Value = vDiasMes
      cmd.Parameters.Add("@fPorAvanOptimo", SqlDbType.Decimal).Value = vDiasTrans / vDiasMes
      cmd.Parameters.Add("@fDiasRest", SqlDbType.Int).Value = vDiasMes - vDiasTrans
      cmd.Parameters.Add("@fAgente", SqlDbType.Int).Value = CmbAgteVta.SelectedValue
      cmd.Parameters.Add("@fSucursal", SqlDbType.Int).Value = CmbSucursal.SelectedValue
      cmd.Parameters.Add("@fFinal", SqlDbType.Int).Value = 1 'Con esto le solicito a cada SP que me aroje solo la inf que necesito

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

      DvTotales.Table = DsVtas.Tables("Totales")

      DgTotales.DataSource = DvTotales
      EstiloDgTotales()

      SQL.Cerrar()
    Catch ex As Exception
      MessageBox.Show(ex.ToString, "Error al ejecutar los procedimientos almacenados")
    End Try
  End Sub

  '************************************************************************
  'INICIAN ESTILOS
  '************************************************************************
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

        .Columns("tit").HeaderText = "KPI's"
        .Columns("tit").Width = 150
        .Columns("tit").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        .Columns("ObjToAlcanzar").HeaderText = "Objetivo a alcanzar"
        .Columns("ObjToAlcanzar").Width = 105
        .Columns("ObjToAlcanzar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns("ObjToAlcanzar").DefaultCellStyle.Format = " ###,###,##0"

        .Columns("ObjAlcanzado").HeaderText = "Objetivo alcanzado"
        .Columns("ObjAlcanzado").Width = 105
        .Columns("ObjAlcanzado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns("ObjAlcanzado").DefaultCellStyle.Format = " ###,###,##0"

        .Columns("porcAlcanzado").HeaderText = "Porcentaje alcanzado (%)"
        .Columns("porcAlcanzado").Width = 90
        .Columns("porcAlcanzado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns("porcAlcanzado").DefaultCellStyle.Format = "#0.#0 %"

        .Columns("AvanceOptimo").HeaderText = "Avance óptimo"
        .Columns("AvanceOptimo").Width = 105
        .Columns("AvanceOptimo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns("AvanceOptimo").DefaultCellStyle.Format = " ###,###,##0"

        Dim numfilas As Integer
        Dim Porcent As Decimal
        numfilas = DgTotales.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)
          If DgTotales.Item(3, i).Value Is DBNull.Value Then
            DgTotales.Item(3, i).Value = 0
            Porcent = 0
          Else
            Porcent = DgTotales.Item(3, i).Value
          End If


          If Porcent < 0.85 Then
            DgTotales.Rows(i).Cells(3).Style.BackColor = Color.Red

          ElseIf Porcent >= 0.85 And Porcent < 1 Then
            DgTotales.Rows(i).Cells(3).Style.BackColor = Color.Yellow

          ElseIf Porcent >= 1 Then
            DgTotales.Rows(i).Cells(3).Style.BackColor = Color.LimeGreen

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

  Private Sub Button3_Click(sender As Object, e As EventArgs)
    Dim form As Form = ScoreCardCteObj
    form.Show()
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
    oSheet.range("A3").value = "KPI's"
    oSheet.range("B3").value = "Objetivo a alcanzar"
    oSheet.range("C3").value = "Objetivo alcanzado"
    oSheet.range("D3").value = "Avance (%)"
    oSheet.range("E3").value = "Avance óptimo"

    'para poner la primera fila de los titulos en negrita
    oSheet.range("A3:E3").font.bold = True
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = DgTotales.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel0 As String = IIf(IsDBNull(Me.DgTotales.Item(0, fila_dt).Value), 0, Me.DgTotales.Item(0, fila_dt).Value)
      Dim cel1 As String = IIf(IsDBNull(Me.DgTotales.Item(1, fila_dt).Value), 0, Me.DgTotales.Item(1, fila_dt).Value)
      Dim cel2 As String = IIf(IsDBNull(Me.DgTotales.Item(2, fila_dt).Value), 0, Me.DgTotales.Item(2, fila_dt).Value)
      Dim cel3 As String = IIf(IsDBNull(Me.DgTotales.Item(3, fila_dt).Value), 0, Me.DgTotales.Item(3, fila_dt).Value * 100)
      Dim cel4 As String = IIf(IsDBNull(Me.DgTotales.Item(4, fila_dt).Value), 0, Me.DgTotales.Item(4, fila_dt).Value)

      fila_dt_excel = fila_dt + 4

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel0
      oSheet.range("B" & fila_dt_excel).value = FormatNumber(cel1, 2)
      oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel2, 2)
      oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel3, 2)
      oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
    Next

    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:E").entirecolumn.autofit()

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
      oSheet.range("A1").value = "Reporte general de KPI's de todos los AGENTES con FECHA " + Format(Me.DtpFechaIni.Value)
    Else
      oSheet.range("A1").value = "Reporte general de KPI's del AGENTE " + returnValue + " con FECHA " + Format(Me.DtpFechaIni.Value)
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
  End Sub

  'Private Sub DgVtaAgte_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgVtaAgte.CellClick
  '    Dim frm As New LineasHalconDetalle()
  '    frm.Show()
  'End Sub

  Private Sub DgVtaAgte_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
    Dim frm As New LineasHalconDetalle()
    frm.Show()
  End Sub

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    Buscar_NotasC()
  End Sub
End Class
