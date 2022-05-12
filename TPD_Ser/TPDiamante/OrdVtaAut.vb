Imports System.Data.SqlClient
Public Class OrdVtaAut
  Private dvDetOAut As New DataView
  Dim DvAgentes2 As New DataView

  Private Sub OrdVtaAut_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    If UsrTPM = "MANAGER" Then
      ''Form
      Me.Width = 1070
      Me.Height = 670

      ''Grid DgvEncOVtas
      DgvEncOVtas.Width = 1038

      ''DgvDetOVtas
      'Label2.Location = New Point(80, 391)
      'DgvDetOVtas.Location = New Point(80, 405)
      DgvDetOVtas.Height = 225

    End If

    ColocaAgente()

    'BurcarOrdAut()
  End Sub

  Private Sub ColocaAgente()
    Dim ConsutaListaA As String
    Dim SQL As New Comandos_SQL()
    '*********************************************************************************************************************************************
    'Codigo para obtener informacion de los agentes que cada usuario debe ver
    '*********************************************************************************************************************************************
    SQL.conectarTPM()

    Dim GroupCode As String
    Dim slpcode As String = SQL.CampoEspecifico("SELECT AgteVentas FROM Usuarios where Id_Usuario = '" + UsrTPM + "'", "AgteVentas")
    Dim CodAgte As String = SQL.CampoEspecifico("SELECT CodAgte FROM Usuarios where Id_Usuario = '" + UsrTPM + "'", "CodAgte")

    If slpcode = "" And UsrTPM <> "MANAGER" And UsrTPM <> "COMERCIAL" And UsrTPM <> "COMPRAS1" Then
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

  Sub BurcarOrdAut()

    Dim SQLTPD As String

    'SQLTPD = "SELECT T1.ItemCode AS Articulo,T1.Dscription AS Descripcion,"
    'SQLTPD &= "T4.ItmsGrpNam as Linea,t1.WhsCode Almacen,T1.Quantity AS Solicitado,T2.OnHand AS Existencia,"
    'SQLTPD &= "CASE WHEN T1.Quantity - T2.OnHand < 0 OR T3.ItmsGrpCod = 187 THEN 0 ELSE T1.Quantity - T2.OnHand END AS Solicitar,"
    'SQLTPD &= "T1.Price AS Precio,T1.Quantity * T1.Price AS Total, "
    'SQLTPD &= "(CASE WHEN T1.Quantity - T2.OnHand < 0 OR T3.ItmsGrpCod = 187 THEN 0 ELSE T1.Quantity - T2.OnHand END) * T1.Price  AS MontVtaSol,"
    'SQLTPD &= "T0.CardCode AS CodClte,T0.CardName AS Clte,T0.DocTotal AS TotalPed,T0.DocEntry AS OrdVta,T0.DocDate AS FchDoc,"
    'SQLTPD &= "CAST(T0.DocTime as varchar(5)) AS Hora, "
    ''SQLTPD &= "SUBSTRING(CAST(T0.DocTime as varchar(5)),1,2) + ':' + SUBSTRING(CAST(T0.DocTime as varchar(5)),3,2) AS Hora, "
    'SQLTPD &= "DATEDIFF(DAY,T0.DocDate,GETDATE()) AS DiasTrans,T6.U_NAME AS UsrCaptura,T6.USER_CODE AS IdUsuario "
    'SQLTPD &= "INTO #OrdVta_Aut FROM ORDR T0 INNER JOIN RDR1 T1 ON T0.DocEntry = T1.DocEntry "
    'SQLTPD &= "LEFT JOIN OITW T2 ON T1.ItemCode = T2.ItemCode AND t2.WhsCode = t1.WhsCode " 'AND T2.WhsCode = 01 
    'SQLTPD &= "LEFT JOIN OITM T3 ON T1.ItemCode = T3.ItemCode  "
    'SQLTPD &= "LEFT JOIN OITB T4 ON T3.ItmsGrpCod = T4.ItmsGrpCod "
    'SQLTPD &= "LEFT JOIN OSLP T5 ON T0.SlpCode = T5.SlpCode "
    'SQLTPD &= "INNER JOIN OUSR T6  ON T0.UserSign = T6.USERID "
    'SQLTPD &= "WHERE T0.PaidToDate = 0 AND T0.CANCELED = 'N' AND T0.DocStatus = 'O' AND T3.ItmsGrpCod <> 150 ORDER BY T0.DocEntry  "

    'SQLTPD &= "SELECT T0.UsrCaptura, T0.OrdVta, T0.FchDoc, T0.Hora, T0.CodClte,t0.Almacen , T0.Clte, "
    'SQLTPD &= "SUM(Total) AS Total, SUM(MontVtaSol) AS Faltante, "
    'SQLTPD &= "CASE  WHEN SUM(MontVtaSol) > 0 THEN SUM(MontVtaSol) * 100 / SUM(Total) ELSE 0 END AS PorFaltante, "
    'SQLTPD &= "SUM(Total- MontVtaSol) AS Existencia, T0.DiasTrans, IdUsuario "
    'SQLTPD &= "INTO #E_OVTA FROM #OrdVta_Aut T0 GROUP BY T0.UsrCaptura, T0.OrdVta, T0.FchDoc, T0.Hora, "
    'SQLTPD &= "T0.CodClte,t0.Almacen , T0.Clte, T0.DiasTrans, T0.TotalPed,IdUsuario ORDER BY T0.DiasTrans DESC  "


    'SQLTPD &= "SELECT T0.UsrCaptura, T0.OrdVta, T0.FchDoc, T0.Hora, T0.CodClte, T0.Clte,t0.Almacen , T0.Total, T0.Faltante,"
    'SQLTPD &= "T0.PorFaltante, T0.Existencia, T0.DiasTrans, T1.Coment, T0.IdUsuario "
    'SQLTPD &= "FROM #E_OVTA T0 LEFT JOIN TPM.dbo.COVTA T1 ON T1.IdOrdVta = T0.OrdVta ORDER BY T0.FchDoc, T0.Hora;  "


    'SQLTPD &= "SELECT Articulo,Descripcion,Linea,Solicitado,Existencia,Solicitar,Precio,Total,MontVtaSol,CodClte,Clte," +
    '                "TotalPed,OrdVta,FchDoc,Hora,DiasTrans,UsrCaptura,IdUsuario FROM #OrdVta_Aut T0 ORDER BY T0.OrdVta,T0.Solicitar DESC  "

    'SQLTPD &= "DROP TABLE #OrdVta_Aut  "
    'SQLTPD &= "DROP TABLE #E_OVTA  "

    Dim DsOrdsVta As New DataSet
    Dim ConexNCFac As New SqlConnection(StrTpm)
    ConexNCFac.Open()
    Dim Adaptador As New SqlDataAdapter()
    Dim ComanNCFac As New SqlCommand("[SP_OrdVtaCreadaPorFacturar]", ConexNCFac)
    ComanNCFac.CommandType = CommandType.StoredProcedure
    ComanNCFac.Parameters.Add("@Agente", SqlDbType.Int).Value = CmbAgteVta.SelectedValue

    Adaptador.SelectCommand = ComanNCFac
    Adaptador.SelectCommand.Connection = ConexNCFac
    Adaptador.SelectCommand.CommandTimeout = 10000
    ComanNCFac.ExecuteNonQuery()
    ComanNCFac.Connection.Close()
    ConexNCFac.Close()

    Adaptador.Fill(DsOrdsVta)

    'With ComanNCFac
    '  .Connection = ConexNCFac
    '  .CommandText = SQLTPD
    '  .ExecuteNonQuery()
    'End With

    'With Adaptador
    '  .SelectCommand = ComanNCFac
    '  .Fill(DsOrdsVta)
    'End With

    DsOrdsVta.Tables(0).TableName = "OrdEnc"
    DsOrdsVta.Tables(1).TableName = "OrdDet"

    Dim DtEncOrd As New DataTable
    DtEncOrd = DsOrdsVta.Tables("OrdEnc")
    dvDetOAut.Table = DsOrdsVta.Tables("OrdDet")

    ConexNCFac.Close()

    With DgvEncOVtas
      .DataSource = DtEncOrd
      'Color de Renglones en Grid
      .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
      .ColumnHeadersHeight = 39
      '39,50
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      .RowHeadersWidth = 25

      .AllowUserToAddRows = False

      .Columns(0).HeaderText = "Usurario Ventas"
      .Columns(0).Width = 90
      .Columns(0).ReadOnly = True

      .Columns(1).HeaderText = "Orden de Vta"
      .Columns(1).Width = 45
      .Columns(1).ReadOnly = True

      .Columns(2).HeaderText = "Fecha Creación"
      .Columns(2).Width = 70
      .Columns(2).ReadOnly = True

      .Columns(3).HeaderText = "Hora"
      .Columns(3).Width = 35
      .Columns(3).ReadOnly = True

      .Columns(4).HeaderText = "Clave Cliente"
      .Columns(4).Width = 50
      .Columns(4).ReadOnly = True

      .Columns(5).HeaderText = "Nombre "
      .Columns(5).Width = 180
      .Columns(5).ReadOnly = True

      .Columns(6).HeaderText = "Clave Agente"
      .Columns(6).Width = 50
      .Columns(6).ReadOnly = True
      .Columns(6).Visible = False

      .Columns(7).HeaderText = "Agente "
      .Columns(7).Width = 180
      .Columns(7).ReadOnly = True

      .Columns(8).HeaderText = "Almacen"
      .Columns(8).Width = 55
      .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(8).ReadOnly = True

      .Columns(9).HeaderText = "$Total Ord. Vta."
      .Columns(9).Width = 65
      .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(9).ReadOnly = True

      .Columns(10).HeaderText = "$ Faltantes"
      .Columns(10).Width = 60
      .Columns(10).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(10).ReadOnly = True

      .Columns(11).HeaderText = "%Falt"
      .Columns(11).Width = 35
      .Columns(11).DefaultCellStyle.Format = "###.0"
      .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(11).ReadOnly = True

      .Columns(12).HeaderText = "$ Existencia"
      .Columns(12).Width = 65
      .Columns(12).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(12).ReadOnly = True

      .Columns(13).HeaderText = "Dias Trans."
      .Columns(13).Width = 35
      .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(13).ReadOnly = True

      .Columns(14).HeaderText = "Comentarios"
      .Columns(14).Width = 208
      .Columns(14).ReadOnly = False

      .Columns(15).Visible = False

    End With

    ReemplazarColumna()


    With DgvDetOVtas
      .DataSource = dvDetOAut
      .ReadOnly = True
      'Color de Renglones en Grid
      .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
      .ColumnHeadersHeight = 39
      '39
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      '.RowHeadersVisible = False
      .RowHeadersWidth = 25

      .AllowUserToAddRows = False
      .Columns(0).HeaderText = "Articulo"
      .Columns(0).Width = 100

      .Columns(1).HeaderText = "Descripción"
      .Columns(1).Width = 290

      .Columns(2).HeaderText = "Línea"
      .Columns(2).Width = 150

      .Columns(3).HeaderText = "Pedido del Clte"
      .Columns(3).Width = 55
      .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(3).DefaultCellStyle.Format = "###,###,###"

      .Columns(4).HeaderText = "Existencia"
      .Columns(4).Width = 60
      .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(4).DefaultCellStyle.Format = "###,###,###"

      .Columns(5).HeaderText = "Pzas Faltantes"
      .Columns(5).Width = 55
      .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(5).DefaultCellStyle.Format = "###,###,###"

      .Columns(6).HeaderText = "$ Precio Vta"
      .Columns(6).Width = 60
      .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(6).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(7).HeaderText = "$ Total"
      .Columns(7).Width = 70
      .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(7).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(8).HeaderText = "$ Monto Vta Faltantes"
      .Columns(8).Width = 80
      .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(8).DefaultCellStyle.Format = "###,###,###.00"

      .Columns(9).Visible = False
      .Columns(10).Visible = False
      .Columns(11).Visible = False
      .Columns(12).Visible = False
      .Columns(13).Visible = False
      .Columns(14).Visible = False
      .Columns(15).Visible = False
      .Columns(16).Visible = False
      .Columns(17).Visible = False

    End With


    Dim vTotOrd As Decimal = 0
    Dim vTotFal As Decimal = 0
    Dim vTotExist As Decimal = 0
    Dim vPorcent As Decimal = 0


    For Each row As DataGridViewRow In DgvEncOVtas.Rows

      If Len(row.Cells("Hora").Value) = 4 Then

        row.Cells("Hora").Value = row.Cells("Hora").Value.ToString.Substring(0, 2) & ":" & row.Cells("Hora").Value.ToString.Substring(2, 2)
      Else
        row.Cells("Hora").Value = row.Cells("Hora").Value.ToString.Substring(0, 1) & ":" & row.Cells("Hora").Value.ToString.Substring(1, 2)
      End If


      vTotOrd += row.Cells("Total").Value

      vTotFal += row.Cells("Faltante").Value

      vTotExist += row.Cells("Existencia").Value

    Next


    If vTotFal > 0 Then
      TxtPor.Text = Format(vTotFal * 100 / vTotOrd, "##,###,###,###.0")
    Else
      TxtPor.Text = Format(0, "##,###,###,###.0")
    End If

    TxtExist.Text = Format(vTotExist, "##,###,###,###.00")
    TxtFalt.Text = Format(vTotFal, "##,###,###,###.00")
    TxtTotOVta.Text = Format(vTotOrd, "##,###,###,###.00")


    filtrar_Ordvta()

  End Sub
  Sub filtrar_Ordvta()
    Try

      dvDetOAut.RowFilter = "OrdVta =" & DgvEncOVtas.Item(1, DgvEncOVtas.CurrentRow.Index).Value.ToString

    Catch ex As Exception
    End Try

  End Sub

  Private Sub DgvEncOVtas_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DgvEncOVtas.CellBeginEdit
    'If UCase(DgvEncOVtas.CurrentRow.Cells("IdUsuario").Value) <> UsrTPM Then
    'If DgvEncOVtas.CurrentRow.Cells("IdUsuario").Value <> UsrTPM Then
    '    MessageBox.Show("No es posible capturar comentarios, usuario diferente al registrado", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    e.Cancel = True
    '    Return
    'End If
  End Sub

  Private Sub DgvEncOVtas_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvEncOVtas.CellEndEdit

    ActualizaValores()

  End Sub

  Private Sub DgvEncOVtas_SelectionChanged(sender As Object, e As System.EventArgs) Handles DgvEncOVtas.SelectionChanged
    filtrar_Ordvta()
  End Sub

  Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles BtnActualizar.Click
    BtnActualizar.Enabled = False
    BurcarOrdAut()
    BtnActualizar.Enabled = True
  End Sub

  Private Sub BtnExcel_Click(sender As System.Object, e As System.EventArgs) Handles BtnExcel.Click
    GridAExcel(DgvEncOVtas)
  End Sub
  Sub ActualizaValores()
    ' crear nueva conexión    
    Dim conexion As New SqlConnection(StrTpm)

    Dim Adaptador As New SqlDataAdapter()
    Dim comando As New SqlCommand

    Dim SQLTPD As String

    SQLTPD = "SELECT count(*) AS Num FROM COVTA WHERE IdOrdVta = " + DgvEncOVtas.CurrentRow.Cells("OrdVta").Value.ToString

    Dim DsVtasDet As New DataSet

    Dim vRegFact As Integer = 0
    With comando
      .CommandText = SQLTPD
      .Connection = conexion
      .Connection.Open()
      vRegFact = IIf(IsDBNull(.ExecuteScalar), 0, .ExecuteScalar)
      .Connection.Close()
    End With


    If vRegFact >= 1 Then

      SQLTPD = "UPDATE COVTA SET Fecha = @Fchact "
      SQLTPD &= ", "
      SQLTPD &= "coment = '" + QuitarCaracteres(DgvEncOVtas.CurrentRow.Cells("Coment").Value.ToString)
      SQLTPD &= "' "
      SQLTPD &= "WHERE IdOrdVta = " + DgvEncOVtas.CurrentRow.Cells("OrdVta").Value.ToString

    Else

      SQLTPD = "INSERT INTO COVTA (IdOrdVta, Fecha, Id_Usuario, Coment) "
      SQLTPD &= "VALUES("
      SQLTPD &= DgvEncOVtas.CurrentRow.Cells("OrdVta").Value.ToString
      SQLTPD &= ", "
      SQLTPD &= "@Fchact" 'DgvEncOVtas.CurrentRow.Cells("Fchfact").Value
      SQLTPD &= ", '"
      SQLTPD &= UsrTPM
      SQLTPD &= "', '"
      SQLTPD &= QuitarCaracteres(DgvEncOVtas.CurrentRow.Cells("Coment").Value.ToString)
      SQLTPD &= "')"

    End If

    Dim CmdWom As Data.SqlClient.SqlCommand

    CmdWom = New Data.SqlClient.SqlCommand()
    With CmdWom

      .Parameters.AddWithValue("@Fchact", DateTime.Now)
      .Connection = New Data.SqlClient.SqlConnection(StrTpm)
      .Connection.Open()
      .CommandText = SQLTPD
      .ExecuteNonQuery()
      .Connection.Close()
    End With

  End Sub
  Private Sub ReemplazarColumna()
    Dim instance As DataGridViewTextBoxColumn
    instance = DgvEncOVtas.Columns(11)
    instance.MaxInputLength = 250
  End Sub

  Private Sub btnCOnsultar_Click(sender As Object, e As EventArgs) Handles btnCOnsultar.Click
    BurcarOrdAut()
  End Sub
End Class