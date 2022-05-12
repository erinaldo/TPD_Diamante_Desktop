
Imports System.Data.SqlClient

'Imports excel = Microsoft.Office.Interop.Excel
Public Class AnalisisCompras

 Public StrTpm As String = conexion_universal.CadenaSQL
 Public StrCon As String = conexion_universal.CadenaSQLSAP
 Public conexion As New SqlConnection(conexion_universal.CadenaSQL)

 Public MesesCol As Integer
 Public MesesProm As Decimal

 Dim DvAnalisisC As New DataView
 Dim DvAlmacen As New DataView

 Dim DvProveedores As New DataView
 Dim DvProveedoresT As New DataView
 Dim DvPro As New DataView
 Dim DvLineas As New DataView
 Dim DvLineasT As New DataView

 'VARIABLE PARA VALIDAR SI SE CARGAN LOS DATOS DE INICIO O YA ESTA EN PROCESO
 Dim ini As Boolean
 'ALMACENA EL VALOR DE LOS COMBOS
 Dim LineaCombo As Integer
 Dim ProveedorCombo As String

 Dim SQL As New Comandos_SQL()


 Private Sub AnalisisCompras_Load(sender As Object, e As EventArgs) Handles MyBase.Load

  PBExportacion.Visible = False
  Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
  LlenaAlmacen()

  ckConsolidado.Checked = True

  Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
  Me.DtpFechaFin.Value = Format(Date.Now, "dd/MM/yyyy")

  CKVtasMes.Checked = True


  'MANDA A LLAMAR EL METODO DE LLENADO DEL PROVEEDOR
  'CBProvTODOS()

 End Sub

 Private Sub CBProvTODOS()
  Try
   Dim ConsutaLista As String

   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

    Dim DSetTablas As New DataSet

    ConsutaLista = "SELECT T0.cardcode, T1.cardname FROM SBO_TPD.DBO.OITM T0 "
    ConsutaLista &= "inner JOIN SBO_TPD.DBO.OCRD T1 ON T0.CARDCODE = T1.CardCode  "
    ConsutaLista &= "GROUP BY T0.CardCode, T1.CardName "


    Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


    daAgte.Fill(DSetTablas, "Proveedores")

    Dim filaAgte As Data.DataRow

    'Asignamos a fila la nueva Row(Fila)del Dataset
    filaAgte = DSetTablas.Tables("Proveedores").NewRow

    'Agregamos los valores a los campos de la tabla
    filaAgte("cardname") = "TODOS"
    filaAgte("cardcode") = "999"
    'filaAgte("itmsgrpcod") = 999

    'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
    DSetTablas.Tables("Proveedores").Rows.Add(filaAgte)

    DvProveedoresT.Table = DSetTablas.Tables("Proveedores")

    Me.CBProveedor.DataSource = DvProveedoresT
    Me.CBProveedor.DisplayMember = "cardname"
    Me.CBProveedor.ValueMember = "cardcode"
    Me.CBProveedor.SelectedValue = "999"

   End Using

  Catch ex As Exception
   MsgBox(ex.Message)
   'MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
  End Try
 End Sub


 Private Sub mCargaLineaIni()          '(ByVal sAlm As String)
  Try
   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

    Dim ConsutaLista As String
    ConsutaLista = "SELECT T0.ItmsGrpCod,T1.ItmsGrpNam, T0.CardCode FROM OITM T0 LEFT JOIN OITB T1 ON T0.ItmsGrpCod=T1.ItmsGrpCod "
    ConsutaLista &= "GROUP BY T0.ItmsGrpCod,ItmsGrpNam,T0.CardCode"

    Dim da As New SqlDataAdapter(ConsutaLista, SqlConnection)


    Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

    Dim DSetTablas As New DataSet
    daAgte.Fill(DSetTablas, "Lineas")

    Dim filaAgte As Data.DataRow

    'Asignamos a fila la nueva Row(Fila)del Dataset
    filaAgte = DSetTablas.Tables("Lineas").NewRow

    'Agregamos los valores a los campos de la tabla
    filaAgte("ItmsGrpCod") = "0"
    filaAgte("ItmsGrpNam") = "TODAS"
    filaAgte("cardcode") = 999

    'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
    DSetTablas.Tables("Lineas").Rows.Add(filaAgte)

    DvLineas.Table = DSetTablas.Tables("Lineas")

    Me.CmbLinIni.DataSource = DvLineas
    Me.CmbLinIni.DisplayMember = "ItmsGrpNam"
    Me.CmbLinIni.ValueMember = "ItmsGrpCod"
    Me.CmbLinIni.SelectedValue = "0"

   End Using

  Catch ex As Exception
   MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
  End Try
 End Sub

 Private Sub mCargaLineaIniTodas()
  Try
   Dim ConsutaLista As String


   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

    Dim DSetTablas As New DataSet

    ConsutaLista = "SELECT ItmsGrpCod, ItmsGrpNam FROM OITB "

    Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


    daAgte.Fill(DSetTablas, "LineasT")

    Dim filaAgte As Data.DataRow

    'Asignamos a fila la nueva Row(Fila)del Dataset
    filaAgte = DSetTablas.Tables("LineasT").NewRow

    'Agregamos los valores a los campos de la tabla
    filaAgte("ItmsGrpCod") = 0
    filaAgte("ItmsGrpNam") = "TODAS"

    'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
    DSetTablas.Tables("LineasT").Rows.Add(filaAgte)

    DvLineasT.Table = DSetTablas.Tables("LineasT")

    Me.CmbLinIni.DataSource = DvLineasT
    Me.CmbLinIni.DisplayMember = "ItmsGrpNam"
    Me.CmbLinIni.ValueMember = "ItmsGrpCod"
    Me.CmbLinIni.SelectedValue = 0

   End Using

  Catch ex As Exception
   MsgBox(ex.Message)
   'MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
  End Try
 End Sub

 Private Sub LlenaAlmacen()
  Dim ConsutaLista As String

  Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)


   Dim DSetTablas As New DataSet
   ConsutaLista = "select WhsCode, WhsName from OWHS where WhsCode='01' or WhsCode='03' or WhsCode='07'"
   Dim daAlmacen As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

   'Dim DSetTablas As New DataSet
   daAlmacen.Fill(DSetTablas, "Almacen")

   Dim fila As Data.DataRow

   'Asignamos a fila la nueva Row(Fila)del Dataset
   fila = DSetTablas.Tables("Almacen").NewRow

   'Agregamos los valores a los campos de la tabla
   fila("whsname") = "TODOS"
   fila("whscode") = 99

   'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
   DSetTablas.Tables("Almacen").Rows.Add(fila)

   Me.CBAlmacen.DataSource = DSetTablas.Tables("Almacen")
   Me.CBAlmacen.DisplayMember = "whsname"
   Me.CBAlmacen.ValueMember = "whscode"
   Me.CBAlmacen.SelectedValue = 99

   '------------------------------------------------------
   ' -----------------------------------------------------

  End Using
 End Sub

 Private Sub BuscaLineas()

  Try

   If CBProveedor.SelectedItem Is Nothing Or CBProveedor.Text = "TODOS" Then
    mCargaLineaIni()
    Me.CmbLinIni.DataSource = DvLineasT
    DvLineas.RowFilter = String.Empty
    Me.CmbLinIni.SelectedValue = 0

   Else 'CmbLinIni.SelectedValue <> 0 Then
    mCargaLineaIniTodas()
    Me.CmbLinIni.DataSource = DvLineas
    DvLineas.RowFilter = String.Empty
    DvLineas.RowFilter = "cardcode = '" & Trim(Me.CBProveedor.SelectedValue.ToString) & "' OR cardcode ='999' "
    Me.CmbLinIni.SelectedValue = 0
   End If
  Catch ex As Exception
   MsgBox(ex.Message)
  End Try

 End Sub


 Private Sub CBProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBProveedor.SelectedIndexChanged
  'ANTERIOR A MODIFICACION
  BuscaLineas()
 End Sub

 Private Sub CBAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBAlmacen.SelectedIndexChanged
  If CBAlmacen.Text = "TODOS" Then
   ckConsolidado.Checked = True
   ckConsolidado.Enabled = True
  Else
   ckConsolidado.Checked = False
   ckConsolidado.Enabled = False
  End If
 End Sub
 '-----------------------------------------Procedimientos almacenados
 Sub CargarRegistros()

  'obtener numero de meses entre fecha inicial y final, (columnas con las que se trabajara)
  MesesCol = DateDiff("m", DtpFechaIni.Value, DtpFechaFin.Value) + 1
  'MsgBox(MesesCol)
  'dias obtenidos entre fecha inicial y final, (se obtiene el promedio de meses) 
  MesesProm = DateDiff("d", DtpFechaIni.Value, DtpFechaFin.Value) / 30
  'MsgBox(MesesProm)
  If MesesProm = 0 Then
   MesesProm = 1 / 30
  End If
  Dim cnn As SqlConnection = Nothing
  Dim cmd4 As SqlCommand = Nothing


  Try
   cnn = New SqlConnection(StrTpm)
   'Using cnn As New SqlConnection(StrTpm)
   Using cnn
    If CBAlmacen.SelectedValue = "99" Then
     If ckConsolidado.Checked = True Then
      cmd4 = New SqlCommand("SPAnalisisComp", cnn)
      'cmd4.CommandTimeout = 500
      'MsgBox("SPAnalisisComp")
     Else
      cmd4 = New SqlCommand("SPAnalisisCompNoConsolidado4", cnn) ' Modificacion Julian 05-06-2019 LAS CATEGORIAS SE MANEJAN POR SUCURSALES
      'cmd4.CommandTimeout = 500
      'MsgBox("SPAnalisisCompNoConsolidado")
     End If
    ElseIf CBAlmacen.SelectedValue = "01" Then
     cmd4 = New SqlCommand("SPAnalisisPue", cnn)
     'cmd4.CommandTimeout = 500
     'MsgBox("SPAnalisisPue")
    ElseIf CBAlmacen.SelectedValue = "03" Then
     cmd4 = New SqlCommand("SPAnalisisMer", cnn)
     'cmd4.CommandTimeout = 500
     'MsgBox("SPAnalisisMer")
    ElseIf CBAlmacen.SelectedValue = "07" Then
     cmd4 = New SqlCommand("SPAnalisisTux", cnn)
     'cmd4.CommandTimeout = 500
     'MsgBox("SPAnalisisTux")
    End If

    cmd4.CommandType = CommandType.StoredProcedure
    cmd4.Parameters.AddWithValue("@FechaIni", DtpFechaIni.Value)
    cmd4.Parameters.AddWithValue("@FechaFin", DtpFechaFin.Value)
    cmd4.Parameters.AddWithValue("@MesesCol", MesesCol)
    cmd4.Parameters.AddWithValue("@PromMeses", MesesProm)
    cmd4.Parameters.AddWithValue("@DiasInv", TBDiasInv.Text)

    'ALMACEN=99-TODOS
    'PROVEEDOR=999-TODOS
    'LINEA=0-TODAS
    'COLOCA EN VARIABLE EL VALOR DE LA LINEA O EL PROVEEDOR PARA SABER QUE COMBO ES EL QUE SE USARA
    If cbp.Checked = True Then
     LineaCombo = CmbLinIni.SelectedValue
     ProveedorCombo = CBProveedor.SelectedValue
    ElseIf cbl.Checked = True Then
     LineaCombo = cmblinea.SelectedValue
     ProveedorCombo = cmbproveedor.SelectedValue
    End If
    cmd4.Parameters.AddWithValue("@Almacen", CBAlmacen.SelectedValue)
    'ANTERIOR AL CAMBIO: URIEL 18/09/2018 ===================
    'cmd4.Parameters.AddWithValue("@Proveedor", CBProveedor.SelectedValue)
    'cmd4.Parameters.AddWithValue("@Linea", CmbLinIni.SelectedValue)
    'NUEVO CAMBIO: URIEL 18/09/2018 ===================
    cmd4.Parameters.AddWithValue("@Proveedor", ProveedorCombo)
    cmd4.Parameters.AddWithValue("@Linea", LineaCombo)

    If chkAlternos.Checked = True Then
     cmd4.Parameters.AddWithValue("@PAlterno", 1)
    Else
     cmd4.Parameters.AddWithValue("@PAlterno", 0)
    End If

    If ckConsolidado.Checked = True Then
     cmd4.Parameters.AddWithValue("@Consolidado", 1)
     'MsgBox("1")
    Else
     cmd4.Parameters.AddWithValue("@Consolidado", 0)
     'MsgBox("0")
    End If

    cnn.Open()

    Dim da As New SqlDataAdapter
    da.SelectCommand = cmd4
    da.SelectCommand.Connection = cnn
    da.SelectCommand.CommandTimeout = 2000

    '***************************************************************
    'Otro proceso
    '***************************************************************
    'Dim Result As IAsyncResult = cmd4.BeginExecuteNonQuery()
    'Dim Count As Integer = 0
    'Do While Result.IsCompleted = False
    '  Count = Count + 1
    '  Console.WriteLine("Waiting ({0})", Count)
    '  System.Threading.Thread.Sleep(1000)
    'Loop
    'Console.WriteLine("Command complete. Affected {0} rows.",
    'cmd4.EndExecuteNonQuery(Result))
    '***************************************************************
    cmd4.ExecuteNonQuery()
    cmd4.Connection.Close()
    '***************************************************************

    Dim DsVtas As New DataSet
    da.Fill(DsVtas, "DsVtas")

    DsVtas.Tables(0).TableName = "Inventario"

    DvAnalisisC.Table = DsVtas.Tables("Inventario")

    DGAnalisisCompras.DataSource = DvAnalisisC

   End Using

  Catch ex As Exception
   MsgBox(ex.Message)
  Finally
   If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
    cnn.Close()
   End If
  End Try

 End Sub

 Private Sub DisenoGrid()

  'obtener numero de meses entre fecha inicial y final, (columnas con las que se trabajara)
  MesesCol = DateDiff("m", DtpFechaIni.Value, DtpFechaFin.Value) + 1
  'MsgBox("datediff" & MesesCol)

  'dias obtenidos entre fecha inicial y final, (se obtiene el promedio de meses) 
  MesesProm = DateDiff("d", DtpFechaIni.Value, DtpFechaFin.Value) / 30

  With Me.DGAnalisisCompras
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

   DGAnalisisCompras.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   Try

    .Columns(0).HeaderText = "Artículo"
    .Columns(0).Width = 110
    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(0).Frozen = True

    .Columns(1).HeaderText = "Categoría"
    .Columns(1).Width = 60
    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(1).Frozen = True

    .Columns(2).HeaderText = "Descripción"
    .Columns(2).Width = 180
    '.Columns(2).Frozen = True

    .Columns(3).HeaderText = "Línea"
    .Columns(3).Width = 120
    '.Columns(3).Frozen = True

    .Columns(4).HeaderText = "Proveedor"
    .Columns(4).Width = 160
    '.Columns(4).Frozen = True

    Dim fecha As String = DtpFechaIni.Value
    'MsgBox(DtpFechaIni.Value.ToString)
    Dim mes As String
    mes = fecha.Substring(3, 2)

    Dim anio As String
    anio = fecha.Substring(6, 4)

    Dim NumMes As Integer

    If mes = 1 Or mes = "01" Then
     mes = "Enero"
     NumMes = 1
    ElseIf mes = 2 Or mes = "02" Then
     mes = "Febrero"
     NumMes = 2
    ElseIf mes = 3 Or mes = "03" Then
     mes = "Marzo"
     NumMes = 3
    ElseIf mes = 4 Or mes = "04" Then
     mes = "Abril"
     NumMes = 4
    ElseIf mes = 5 Or mes = "05" Then
     mes = "Mayo"
     NumMes = 5
    ElseIf mes = 6 Or mes = "06" Then
     mes = "Junio"
     NumMes = 6
    ElseIf mes = 7 Or mes = "07" Then
     mes = "Julio"
     NumMes = 7
    ElseIf mes = 8 Or mes = "08" Then
     mes = "Agosto"
     NumMes = 8
    ElseIf mes = 9 Or mes = "09" Then
     mes = "Septiembre"
     NumMes = 9
    ElseIf mes = 10 Then
     mes = "Octubre"
     NumMes = 10
    ElseIf mes = 11 Then
     mes = "Noviembre"
     NumMes = 11
    ElseIf mes = 12 Then
     mes = "Diciembre"
     NumMes = 12

    End If

    .Columns(5).HeaderText = mes & " " & anio
    .Columns(5).Width = 60
    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


    For i As Integer = 1 To MesesCol
     Dim aux As Integer
     aux = NumMes + i


     If aux > 12 Then
      Dim aux2 As String
      aux2 = aux - 12

      Select Case aux2
       Case 1
        aux2 = "Enero"
       Case 2
        aux2 = "Febrero"
       Case 3
        aux2 = "Marzo"
       Case 4
        aux2 = "Abril"
       Case 5
        aux2 = "Mayo"
       Case 6
        aux2 = "Junio"
       Case 7
        aux2 = "Julio"
       Case 8
        aux2 = "Agosto"
       Case 9
        aux2 = "Septiembre"
       Case 10
        aux2 = "Octubre"
       Case 11
        aux2 = "Noviembre"
       Case 12
        aux2 = "Diciembre"
      End Select

      .Columns(5 + i).HeaderText = aux2 & " " & anio + 1
     Else

      Dim auxm As String
      auxm = NumMes + i

      Select Case auxm
       Case 1
        auxm = "Enero"
       Case 2
        auxm = "Febrero"
       Case 3
        auxm = "Marzo"
       Case 4
        auxm = "Abril"
       Case 5
        auxm = "Mayo"
       Case 6
        auxm = "Junio"
       Case 7
        auxm = "Julio"
       Case 8
        auxm = "Agosto"
       Case 9
        auxm = "Septiembre"
       Case 10
        auxm = "Octubre"
       Case 11
        auxm = "Noviembre"
       Case 12
        auxm = "Diciembre"
      End Select
      .Columns(5 + i).HeaderText = auxm & " " & anio
     End If


     .Columns(5 + i).Width = 60
     .Columns(5 + i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    Next

    'Detalle Ventas
    Dim DVentas As Integer
    DVentas = 5 + MesesCol

    .Columns(DVentas).HeaderText = "Ventas Totales"
    .Columns(DVentas).Width = 60
    .Columns(DVentas).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 1).HeaderText = "Almacen Puebla"
    .Columns(DVentas + 1).Width = 60
    .Columns(DVentas + 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 1).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 2).HeaderText = "Almacen Mérida"
    .Columns(DVentas + 2).Width = 60
    .Columns(DVentas + 2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 2).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 3).HeaderText = "Almacen Tuxtla"
    .Columns(DVentas + 3).Width = 60
    .Columns(DVentas + 3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 3).DefaultCellStyle.Format = "###,###,##0"

    If CBAlmacen.SelectedValue = "01" Then
     .Columns(DVentas + 2).Visible = False
     .Columns(DVentas + 3).Visible = False
    ElseIf CBAlmacen.SelectedValue = "03" Then
     .Columns(DVentas + 1).Visible = False
     .Columns(DVentas + 3).Visible = False
    ElseIf CBAlmacen.SelectedValue = "07" Then
     .Columns(DVentas + 1).Visible = False
     .Columns(DVentas + 2).Visible = False
    End If

    .Columns(DVentas + 4).HeaderText = "Existencia Total"
    .Columns(DVentas + 4).Width = 60
    .Columns(DVentas + 4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 4).DefaultCellStyle.Format = "###,###,##0"

    If CBAlmacen.SelectedValue = "01" Or CBAlmacen.SelectedValue = "03" Or CBAlmacen.SelectedValue = "07" Then
     .Columns(DVentas + 4).Visible = False
    End If

    .Columns(DVentas + 5).HeaderText = "Promedio Mensual"
    .Columns(DVentas + 5).Width = 60
    .Columns(DVentas + 5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 5).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 6).HeaderText = "Punto ReOrden"
    .Columns(DVentas + 6).Width = 60
    .Columns(DVentas + 6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 6).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 7).HeaderText = "Comprometido"
    .Columns(DVentas + 7).Width = 60
    .Columns(DVentas + 7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 7).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 8).HeaderText = "Solicitado"
    .Columns(DVentas + 8).Width = 60
    .Columns(DVentas + 8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 8).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 9).HeaderText = "Pedido"
    .Columns(DVentas + 9).Width = 60
    .Columns(DVentas + 9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 9).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 10).HeaderText = "Precio L8"
    .Columns(DVentas + 10).Width = 85
    .Columns(DVentas + 10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 10).DefaultCellStyle.Format = "$ ###,###,###.###0"

    .Columns(DVentas + 11).HeaderText = "Moneda"
    .Columns(DVentas + 11).Width = 65
    .Columns(DVentas + 11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(DVentas + 12).HeaderText = "Importe"
    .Columns(DVentas + 12).Width = 85
    .Columns(DVentas + 12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 12).DefaultCellStyle.Format = "$ ###,###,##0.#0"

    .Columns(DVentas + 13).HeaderText = "Días Inventario"
    .Columns(DVentas + 13).Width = 60
    .Columns(DVentas + 13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 13).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 14).HeaderText = "Cod. Línea"
    .Columns(DVentas + 14).Width = 60
    .Columns(DVentas + 14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 14).Visible = False

    .Columns(DVentas + 15).HeaderText = "Cod. Proveedor"
    .Columns(DVentas + 15).Width = 60
    .Columns(DVentas + 15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 15).Visible = False

    .Columns(DVentas + 16).HeaderText = "Cod. Proveedor Alterno"
    .Columns(DVentas + 16).Width = 60
    .Columns(DVentas + 16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    If chkAlternos.Checked Then
     .Columns(DVentas + 16).Visible = True
    Else
     .Columns(DVentas + 16).Visible = False
    End If

    .Columns(DVentas + 17).HeaderText = "Peso"
    .Columns(DVentas + 17).Width = 60
    .Columns(DVentas + 17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(DVentas + 17).DefaultCellStyle.Format = "###,##0.0000"

    .Columns(DVentas + 18).HeaderText = "Peso x Pedido"
    .Columns(DVentas + 18).Width = 60
    .Columns(DVentas + 18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(DVentas + 18).DefaultCellStyle.Format = "###,##0.0000"

    'Pinto el renglo
    Dim numfilas As Integer
    numfilas = DGAnalisisCompras.RowCount 'cuenta las filas del DataGrid

    'recorre las filas del DataGrid
    If chkAlternos.Checked Then
     For i = 0 To (numfilas - 1)
      If Not DGAnalisisCompras.Item(22, i).Value Is DBNull.Value Then
       If DGAnalisisCompras.Item(22, i).Value.ToString() = ProveedorCombo Then

        For j = 0 To 22
         .Rows(i).Cells(j).Style.BackColor = Color.LightGray
        Next
       Else
        .DefaultCellStyle.BackColor = Color.AliceBlue
       End If
      End If
     Next
    Else

    End If

   Catch ex As Exception

   End Try


  End With
 End Sub

 Private Sub DisenoGridNOConsolidado()

  'obtener numero de meses entre fecha inicial y final, (columnas con las que se trabajara)
  MesesCol = DateDiff("m", DtpFechaIni.Value, DtpFechaFin.Value) + 1
  'MsgBox(MesesCol)

  'dias obtenidos entre fecha inicial y final, (se obtiene el promedio de meses) 
  MesesProm = DateDiff("d", DtpFechaIni.Value, DtpFechaFin.Value) / 30

  With Me.DGAnalisisCompras
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

   DGAnalisisCompras.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   Try

    .Columns(0).HeaderText = "Artículo"
    .Columns(0).Width = 110
    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(0).Frozen = True

    .Columns(1).HeaderText = "Categoría"
    .Columns(1).Width = 60
    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(1).Frozen = True

    .Columns(2).HeaderText = "Descripción"
    .Columns(2).Width = 180
    '.Columns(2).Frozen = True

    .Columns(3).HeaderText = "Línea"
    .Columns(3).Width = 120
    '.Columns(3).Frozen = True

    .Columns(4).HeaderText = "Proveedor"
    .Columns(4).Width = 160
    '.Columns(4).Frozen = True

    Dim fecha As String = DtpFechaIni.Value
    Dim mes As String
    mes = fecha.Substring(3, 2)

    Dim anio As String
    anio = fecha.Substring(6, 4)

    Dim NumMes As Integer

    If mes = 1 Or mes = "01" Then
     mes = "Enero"
     NumMes = 1
    ElseIf mes = 2 Or mes = "02" Then
     mes = "Febrero"
     NumMes = 2
    ElseIf mes = 3 Or mes = "03" Then
     mes = "Marzo"
     NumMes = 3
    ElseIf mes = 4 Or mes = "04" Then
     mes = "Abril"
     NumMes = 4
    ElseIf mes = 5 Or mes = "05" Then
     mes = "Mayo"
     NumMes = 5
    ElseIf mes = 6 Or mes = "06" Then
     mes = "Junio"
     NumMes = 6
    ElseIf mes = 7 Or mes = "07" Then
     mes = "Julio"
     NumMes = 7
    ElseIf mes = 8 Or mes = "08" Then
     mes = "Agosto"
     NumMes = 8
    ElseIf mes = 9 Or mes = "09" Then
     mes = "Septiembre"
     NumMes = 9
    ElseIf mes = 10 Then
     mes = "Octubre"
     NumMes = 10
    ElseIf mes = 11 Then
     mes = "Noviembre"
     NumMes = 11
    ElseIf mes = 12 Then
     mes = "Diciembre"
     NumMes = 12

    End If

    .Columns(5).HeaderText = mes & " " & anio
    .Columns(5).Width = 60
    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


    For i As Integer = 1 To MesesCol
     Dim aux As Integer
     aux = NumMes + i


     If aux > 12 Then
      Dim aux2 As String
      aux2 = aux - 12

      Select Case aux2
       Case 1
        aux2 = "Enero"
       Case 2
        aux2 = "Febrero"
       Case 3
        aux2 = "Marzo"
       Case 4
        aux2 = "Abril"
       Case 5
        aux2 = "Mayo"
       Case 6
        aux2 = "Junio"
       Case 7
        aux2 = "Julio"
       Case 8
        aux2 = "Agosto"
       Case 9
        aux2 = "Septiembre"
       Case 10
        aux2 = "Octubre"
       Case 11
        aux2 = "Noviembre"
       Case 12
        aux2 = "Diciembre"
      End Select

      .Columns(5 + i).HeaderText = aux2 & " " & anio + 1
     Else

      Dim auxm As String
      auxm = NumMes + i

      Select Case auxm
       Case 1
        auxm = "Enero"
       Case 2
        auxm = "Febrero"
       Case 3
        auxm = "Marzo"
       Case 4
        auxm = "Abril"
       Case 5
        auxm = "Mayo"
       Case 6
        auxm = "Junio"
       Case 7
        auxm = "Julio"
       Case 8
        auxm = "Agosto"
       Case 9
        auxm = "Septiembre"
       Case 10
        auxm = "Octubre"
       Case 11
        auxm = "Noviembre"
       Case 12
        auxm = "Diciembre"
      End Select
      .Columns(5 + i).HeaderText = auxm & " " & anio
     End If


     .Columns(5 + i).Width = 60
     .Columns(5 + i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    Next

    'Detalle Ventas
    Dim DVentas As Integer
    DVentas = 5 + MesesCol

    .Columns(DVentas).HeaderText = "Almacén"
    .Columns(DVentas).Width = 75
    .Columns(DVentas).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(DVentas + 1).HeaderText = "Cod. Almacén"
    .Columns(DVentas + 1).Visible = False

    .Columns(DVentas + 2).HeaderText = "Existencia"
    .Columns(DVentas + 2).Width = 60
    .Columns(DVentas + 2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 2).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 3).HeaderText = "Total Vtas"
    .Columns(DVentas + 3).Width = 60
    .Columns(DVentas + 3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 3).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 4).HeaderText = "Prom. VtasMensual"
    .Columns(DVentas + 4).Width = 60
    .Columns(DVentas + 4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 4).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 5).HeaderText = "Punto Reorden"
    .Columns(DVentas + 5).Width = 60
    .Columns(DVentas + 5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 5).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 6).HeaderText = "Comprometido"
    .Columns(DVentas + 6).Width = 60
    .Columns(DVentas + 6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 6).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 7).HeaderText = "Solicitado"
    .Columns(DVentas + 7).Width = 60
    .Columns(DVentas + 7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 7).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 8).HeaderText = "Pedido"
    .Columns(DVentas + 8).Width = 60
    .Columns(DVentas + 8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 8).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 9).HeaderText = "Precio L8"
    .Columns(DVentas + 9).Width = 85
    .Columns(DVentas + 9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(DVentas + 9).DefaultCellStyle.Format = "$ ###,###,##0.#0"

    .Columns(DVentas + 10).HeaderText = "Moneda"
    .Columns(DVentas + 10).Width = 65
    .Columns(DVentas + 10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(DVentas + 11).HeaderText = "Importe"
    .Columns(DVentas + 11).Width = 85
    .Columns(DVentas + 11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(DVentas + 11).DefaultCellStyle.Format = "$ ###,###,##0.#0"

    .Columns(DVentas + 12).HeaderText = "Días Inventario"
    .Columns(DVentas + 12).Width = 60
    .Columns(DVentas + 12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(DVentas + 12).DefaultCellStyle.Format = "###,###,##0"

    .Columns(DVentas + 13).Visible = False
    .Columns(DVentas + 14).Visible = False

    If chkAlternos.Checked Then
     .Columns(DVentas + 15).Visible = True
    Else
     .Columns(DVentas + 15).Visible = False
    End If

    .Columns(DVentas + 16).HeaderText = "Peso"
    .Columns(DVentas + 16).Width = 60
    .Columns(DVentas + 16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(DVentas + 16).DefaultCellStyle.Format = "###,##0.0000"

    .Columns(DVentas + 17).HeaderText = "Peso x Pedido"
    .Columns(DVentas + 17).Width = 60
    .Columns(DVentas + 17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(DVentas + 17).DefaultCellStyle.Format = "###,##0.0000"

    'Pinto el renglo
    Dim numfilas As Integer
    numfilas = DGAnalisisCompras.RowCount 'cuenta las filas del DataGrid

    If chkAlternos.Checked Then
     'recorre las filas del DataGrid
     For i = 0 To (numfilas - 1)
      If Not DGAnalisisCompras.Item(20, i).Value Is DBNull.Value Then
       If DGAnalisisCompras.Item(20, i).Value.ToString() = ProveedorCombo Then

        For j = 0 To 20
         .Rows(i).Cells(j).Style.BackColor = Color.LightGray
        Next
       Else
        .DefaultCellStyle.BackColor = Color.AliceBlue
       End If
      End If
     Next
    End If

   Catch ex As Exception

   End Try


  End With
 End Sub


 Private Sub DGAnalisisCompras_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DGAnalisisCompras.RowPostPaint
  Try

   If ckConsolidado.Checked = True Then
    DGAnalisisCompras.Rows(e.RowIndex).Cells("VtasTot").Style.BackColor = Color.DarkGray
    DGAnalisisCompras.Rows(e.RowIndex).Cells("ExisTot").Style.BackColor = Color.Yellow
    DGAnalisisCompras.Rows(e.RowIndex).Cells("PromedioMens").Style.BackColor = Color.Gold

    DGAnalisisCompras.Rows(e.RowIndex).Cells("Pedido").Style.ForeColor = Color.White
    DGAnalisisCompras.Rows(e.RowIndex).Cells("Pedido").Style.BackColor = Color.Black

   ElseIf CBAlmacen.Text = "TODOS" And ckConsolidado.Checked = False Then
    DGAnalisisCompras.Rows(e.RowIndex).Cells("Existencia").Style.BackColor = Color.Yellow
    DGAnalisisCompras.Rows(e.RowIndex).Cells("VtasTot").Style.BackColor = Color.DarkGray
    DGAnalisisCompras.Rows(e.RowIndex).Cells("PromVtasMen").Style.BackColor = Color.Gold

    DGAnalisisCompras.Rows(e.RowIndex).Cells("Pedido").Style.ForeColor = Color.White
    DGAnalisisCompras.Rows(e.RowIndex).Cells("Pedido").Style.BackColor = Color.Black

   ElseIf CBAlmacen.SelectedValue = "01" Then
    DGAnalisisCompras.Rows(e.RowIndex).Cells("VtasTot").Style.BackColor = Color.DarkGray
    DGAnalisisCompras.Rows(e.RowIndex).Cells("AlmPue").Style.BackColor = Color.Yellow
    DGAnalisisCompras.Rows(e.RowIndex).Cells("PromedioMens").Style.BackColor = Color.Gold
    'DGAnalisisCompras.Rows(e.RowIndex).Cells("PuntoReorden").Style.BackColor = Color.LightBlue

    DGAnalisisCompras.Rows(e.RowIndex).Cells("Pedido").Style.ForeColor = Color.White
    DGAnalisisCompras.Rows(e.RowIndex).Cells("Pedido").Style.BackColor = Color.Black

   ElseIf CBAlmacen.SelectedValue = "03" Then
    DGAnalisisCompras.Rows(e.RowIndex).Cells("VtasTot").Style.BackColor = Color.DarkGray
    DGAnalisisCompras.Rows(e.RowIndex).Cells("AlmMer").Style.BackColor = Color.Yellow
    DGAnalisisCompras.Rows(e.RowIndex).Cells("PromedioMens").Style.BackColor = Color.Gold

    DGAnalisisCompras.Rows(e.RowIndex).Cells("Pedido").Style.ForeColor = Color.White
    DGAnalisisCompras.Rows(e.RowIndex).Cells("Pedido").Style.BackColor = Color.Black

   ElseIf CBAlmacen.SelectedValue = "07" Then
    DGAnalisisCompras.Rows(e.RowIndex).Cells("VtasTot").Style.BackColor = Color.DarkGray
    DGAnalisisCompras.Rows(e.RowIndex).Cells("AlmTux").Style.BackColor = Color.Yellow
    DGAnalisisCompras.Rows(e.RowIndex).Cells("PromedioMens").Style.BackColor = Color.Gold

    DGAnalisisCompras.Rows(e.RowIndex).Cells("Pedido").Style.ForeColor = Color.White
    DGAnalisisCompras.Rows(e.RowIndex).Cells("Pedido").Style.BackColor = Color.Black
   End If


  Catch ex As Exception

  End Try


 End Sub

 Private Sub BtnConsultar_Click_1(sender As Object, e As EventArgs) Handles BtnConsultar.Click

  DGAnalisisCompras.Columns.Clear()
  If DtpFechaIni.Value < "01/01/2015" Then
   If (MessageBox.Show(
                            "Seleccione una fecha mayor a 01/01/2015",
                              "Fecha Incorrecta", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)) = MsgBoxResult.Ok Then
    DtpFechaIni.Focus()
   End If

  ElseIf DtpFechaFin.Value > Date.Now Then
   If (MessageBox.Show(
                            "Seleccione una fecha igual o menor a la de hoy",
                              "Fecha Incorrecta", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)) = MsgBoxResult.Ok Then
    DtpFechaIni.Focus()
   End If

  ElseIf DtpFechaIni.Value > DtpFechaFin.Value Then
   If (MessageBox.Show(
                            "La Fecha Inicial no puede ser mayor a la Fecha Final",
                              "Fecha Incorrecta", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)) = MsgBoxResult.Ok Then
    DtpFechaIni.Focus()
   End If

  ElseIf TBDiasInv.Text = "" Then
   If (MessageBox.Show(
                            "Ingrese días de Inventario",
                              "Fecha Incorrecta", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)) = MsgBoxResult.Ok Then
    TBDiasInv.Focus()
   End If
  ElseIf cbl.Checked = False And cbp.Checked = False Then
   MsgBox("Favor de marcar un filtro de Proveedor o Linea.", MsgBoxStyle.Exclamation, "Alerta de filtro")
   Return
  Else
   'DISENO DEL GRID
   If CBAlmacen.Text = "TODOS" And ckConsolidado.Checked = False Then
    CargarRegistros()
    DisenoGridNOConsolidado()
   Else
    CargarRegistros()
    DisenoGrid()
   End If
  End If

  MuestraMeses()

 End Sub

 Private Sub BtnExcel_Click_1(sender As Object, e As EventArgs) Handles BtnExcel.Click
  Try
   'ExportNuevo()
   'ExportarViejoAnalisis()
   'ExportarNuevoAnalisis()

   'Exp()

   ExportarDatosExcel_Sol(DGAnalisisCompras)
  Catch ex As Exception
   MessageBox.Show(ex.ToString)
  End Try
 End Sub

 Public Sub ExportarDatosExcel_Sol(ByVal DataGridView1 As DataGridView)
  Try
   Dim m_Excel As New Microsoft.Office.Interop.Excel.Application
   m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlWait
   'm_Excel.Visible = True
   Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook = m_Excel.Workbooks.Add
   Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet = objLibroExcel.Worksheets(1)
   With objHojaExcel
    '.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
    'Encabezado
    'ENCABEZADO DEL REPORTE GENERADO
    .Range("A1:D1").Merge()
    .Range("A1:D1").Value = "Reporte de Análisis de Compras"
    .Range("A1:D1").Font.Bold = True

    .Range("A2:D2").Merge()
    .Range("A2:D2").Value = "Fecha del: " + DtpFechaIni.Value + "  Al  " + DtpFechaFin.Value
    .Range("A2:D2").Font.Bold = True

    .Range("A2:D3").Merge()
    .Range("A3:D3").Value = "Almacén: " + CBAlmacen.Text
    .Range("A3:D3").Font.Bold = True

    .Range("A4:D4").Merge()
    .Range("A4:D4").Value = "Días de Inventario: " + TBDiasInv.Text
    .Range("A4:D4").Font.Bold = True

    .Range("A5:D5").Merge()
    .Range("A5:D5").Value = "Proveedor: " + CBProveedor.Text
    .Range("A5:D5").Font.Bold = True

    .Range("A6:D6").Merge()
    .Range("A6:D6").Value = "Línea: " + CmbLinIni.Text
    .Range("A6:D6").Font.Bold = True

    .Range("A1:D6").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGray)

    Const primeraLetra As Char = "A"
    Const primerNumero As Short = 8 'Primer fila para ingresar datos, incluyendo las cabeceras
    Dim Letra As Char, UltimaLetra As Char
    Dim Numero As Integer, UltimoNumero As Integer
    Dim cod_letra As Byte = Asc(primeraLetra) - 1
    Dim sepDec As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
    Dim sepMil As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator

    'Establecer formatos de las columnas de la hija de cálculo  
    Dim strColumna As String = ""
    Dim LetraIzq As String = ""
    Dim cod_LetraIzq As Byte = Asc(primeraLetra) - 1
    Letra = primeraLetra
    Numero = primerNumero
    Dim objCelda As Microsoft.Office.Interop.Excel.Range
    For Each c As DataGridViewColumn In DataGridView1.Columns
     If c.Name.ToString = "Borrar" Then
      Continue For
     End If
     If c.Visible Or c.Name.ToString = "CardCode" Or c.Name.ToString = "ItmsGrpCod" Then
      If Letra = "Z" Then
       Letra = primeraLetra
       cod_letra = Asc(primeraLetra)
       cod_LetraIzq += 1
       LetraIzq = Chr(cod_LetraIzq)
      Else
       cod_letra += 1
       Letra = Chr(cod_letra)
      End If
      strColumna = LetraIzq + Letra + Numero.ToString
      objCelda = .Range(strColumna, Type.Missing)
      objCelda.Value = c.HeaderText
      objCelda.Font.Bold = True
      objCelda.Font.Color = Color.White
      objCelda.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
      objCelda.EntireColumn.Font.Size = 11
      objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format
      objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format
      objCelda.EntireColumn.AutoFit()
     End If
    Next

    Dim objRangoEncab As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
    objRangoEncab.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
    UltimaLetra = Letra
    Dim UltimaLetraIzq As String = LetraIzq

    'CARGA DE DATOS  
    Dim i As Integer = Numero + 1
    Dim cRows As Integer = 0

    PBExportacion.Visible = True
    PBExportacion.Minimum = 0
    PBExportacion.Maximum = DataGridView1.Rows.Count

    For Each reg As DataGridViewRow In DataGridView1.Rows
     cRows = cRows + 1
     PBExportacion.Value = cRows
     LetraIzq = ""
     cod_LetraIzq = Asc(primeraLetra) - 1
     Letra = primeraLetra
     cod_letra = Asc(primeraLetra) - 1

     For Each c As DataGridViewColumn In DataGridView1.Columns
      If c.Visible Or c.Name.ToString = "CardCode" Or c.Name.ToString = "ItmsGrpCod" Then
       If Letra = "Z" Then
        Letra = primeraLetra
        cod_letra = Asc(primeraLetra)
        cod_LetraIzq += 1
        LetraIzq = Chr(cod_LetraIzq)
       Else
        cod_letra += 1
        Letra = Chr(cod_letra)
       End If
       strColumna = LetraIzq + Letra

       'Ingresa la infomacion
       If strColumna = "A" Then
        '.Cells.HorizontalAlignment = 3
        .Cells(i, strColumna).NumberFormat = "@" 'Esto le da un formato de Text a la celda
        .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value.ToString())
       Else
        '.Cells.NumberFormat = c.DefaultCellStyle.Format
        .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value)
       End If

       If c.Name.ToString = "VtasTot" Then
        .Cells(i, strColumna).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkGray)
       End If
       If c.Name.ToString = "AlmPue" Or c.Name.ToString = "AlmMer" Or c.Name.ToString = "AlmTuxt" Or c.Name.ToString = "Existencia" Then
        .Cells(i, strColumna).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
       End If

       If c.Name.ToString = "Pedido" Then
        .Cells(i, strColumna).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DeepSkyBlue)
       End If

       If c.Name.ToString = "PromedioMens" Or c.Name.ToString = "PromVtasMen" Then
        .Cells(i, strColumna).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange)
       End If
      End If
     Next

     Dim objRangoReg As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + i.ToString, strColumna + i.ToString)
     objRangoReg.Rows.BorderAround()
     objRangoReg.Select()
     i += 1
    Next
    UltimoNumero = i

    'Dibujar las líneas de las columnas  
    LetraIzq = ""
    cod_LetraIzq = Asc("A")
    cod_letra = Asc(primeraLetra)
    Letra = primeraLetra
    For Each c As DataGridViewColumn In DataGridView1.Columns
     If c.Visible Or c.Name.ToString = "CardCode" Or c.Name.ToString = "ItmsGrpCod" Then
      objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
      objCelda.BorderAround()

      'If (c.Name.ToString <> "Precio" And c.Name.ToString <> "Importe") Then
      objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
      objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format
      'End If

      If Letra = "Z" Then
       Letra = primeraLetra
       cod_letra = Asc(primeraLetra)
       LetraIzq = Chr(cod_LetraIzq)
       cod_LetraIzq += 1
      Else
       cod_letra += 1
       Letra = Chr(cod_letra)
      End If
     End If
    Next

    If chkAlternos.Checked Then
     'Pinto lineas grises de alternos
     For j = 0 To UltimoNumero - (primerNumero + 2)
      If ckConsolidado.Checked = True Then
       If DGAnalisisCompras.Item(22, j).Value.ToString() = ProveedorCombo Then
        Dim objRango2 As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + (j + primerNumero + 1).ToString, UltimaLetraIzq + UltimaLetra + (j + primerNumero + 1).ToString())
        objRango2.Select()
        objRango2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
       End If
      Else
       If DGAnalisisCompras.Item(20, j).Value.ToString() = ProveedorCombo Then
        Dim objRango2 As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + (j + primerNumero + 1).ToString, UltimaLetraIzq + UltimaLetra + (j + primerNumero + 1).ToString())
        objRango2.Select()
        objRango2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
       End If
      End If
     Next
    End If

    'Dibujar el border exterior grueso  
    Dim objRango As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
    objRango.Select()

    objRango.Columns.AutoFit()
    objRango.Columns.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)

    'Coloco autofiltro
    Dim objRangoFiltro As Microsoft.Office.Interop.Excel.Range = objHojaExcel.Range(primeraLetra + primerNumero.ToString, UltimaLetra + primerNumero.ToString)
    objRangoFiltro.AutoFilter(1)

    m_Excel.Visible = True
    .Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
    .Activate()
   End With

   'objHojaExcel.Rows.Item(3).Font.Bold = 1
   objHojaExcel.Rows.Item(8).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter
   m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault
   PBExportacion.Visible = False
  Catch ex As Exception
   PBExportacion.Visible = False
   MsgBox(ex.Message.ToString)
  End Try
 End Sub

 Private Sub MuestraMeses()
  Try
   If CKVtasMes.Checked = True Then
    With DGAnalisisCompras
     For i = 0 To MesesCol - 1
      .Columns(i + 5).Visible = True
     Next
    End With
   Else
    With DGAnalisisCompras
     For i = 0 To MesesCol - 1
      .Columns(i + 5).Visible = False
     Next
    End With
   End If

  Catch ex As Exception

  End Try
 End Sub

 Private Sub CKVtasMes_CheckedChanged(sender As Object, e As EventArgs) Handles CKVtasMes.CheckedChanged

  MuestraMeses()

 End Sub
 'CODIGO NUEVO DE CONTROL DE CHECKED DE LINEA Y PROVEEDOR. MODIFICACION URIEL: 14/09/2018
 '======================================================================================================================================= INICIO CHECK Y CHANGE
 Private Sub cbl_CheckedChanged(sender As Object, e As EventArgs) Handles cbl.CheckedChanged
  If (cbl.Checked = True) Then
   ini = True
   'DESACTIVA EL CHECKED DE LINEAS
   cbp.Checked = False
   'VISUALIZA LOS COMBOS CORRESPONDIENTES AL FILTRO DE LINEA
   cmblinea.Visible = True
   cmbproveedor.Visible = True
   lbllinea.Visible = True
   lblp.Visible = True
   'OCULTA LOS OTROS COMBO
   CmbLinIni.Visible = False
   CBProveedor.Visible = False
   lblp1.Visible = False
   lblp1.Visible = False
   'MANDA A LLAMAR A LOS METODOS CORRESPONDIENTES
   LlenaLinea()
   LlenaProveedores()
   ini = False
  Else
   'ini = False
   'DESACTIVA EL CHECKED DE LINEAS
   cbp.Checked = True
   'VISUALIZA LOS COMBOS CORRESPONDIENTES AL FILTRO DE LINEA
   cmblinea.Visible = False
   cmbproveedor.Visible = False
   lbllinea.Visible = False
   lblp.Visible = False
   'OCULTA LOS OTROS COMBO
   CmbLinIni.Visible = True
   CBProveedor.Visible = True
   lblp1.Visible = True
   lblp1.Visible = True
   'MANDA A LLAMAR A LOS METODOS CORRESPONDIENTES

   'ini = True
  End If
 End Sub

 Private Sub cbp_CheckedChanged(sender As Object, e As EventArgs) Handles cbp.CheckedChanged
  If (cbp.Checked = True) Then
   'DESACTIVA EL CHECKED DE LINEAS
   cbl.Checked = False
   'OCULTA LOS COMBOS CORRESPONDIENTES AL FILTRO DE LINEA
   cmblinea.Visible = False
   cmbproveedor.Visible = False
   lbllinea.Visible = False
   lblp.Visible = False
   'VISUALIZA LOS OTROS COMBO
   CmbLinIni.Visible = True
   CBProveedor.Visible = True
   lblp1.Visible = True
   lblp1.Visible = True
   'MANDA A LLAMAR A LOS METODOS CORRESPONDIENTES
   CBProvTODOS()
  Else
   'DESACTIVA EL CHECKED DE LINEAS
   cbl.Checked = True
   'OCULTA LOS COMBOS CORRESPONDIENTES AL FILTRO DE LINEA
   cmblinea.Visible = True
   cmbproveedor.Visible = True
   lbllinea.Visible = True
   lblp.Visible = True
   'VISUALIZA LOS OTROS COMBO
   CmbLinIni.Visible = False
   CBProveedor.Visible = False
   lblp1.Visible = False
   lblp1.Visible = False
   'MANDA A LLAMAR A LOS METODOS CORRESPONDIENTES

  End If
 End Sub


 Private Sub cmblinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmblinea.SelectedIndexChanged
  'MANDA A LLAMAR EL METODO DE LLENADO DE PROVEEDOR POR LINEA
  BuscaProveedores()
 End Sub

 '======================================================================================================================================= FIN CHECK Y CHANGE

 'METODOS NUEVOS PARA BUSQUEDA DE PROVEEDOR Y LINEA: MODIFICACION URIEL: 14/09/2018
 '======================================================================================================================================= INICIO METODOS
 Sub LlenaProveedores()
  Try
   Dim ConsultaLista As String
   'SE USA LA CONEXION
   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
    'VARIABLE DATASET PARA REGISTROS
    Dim DSetTablas As New DataSet
    'CONSUILTA DE LOS PROVEEDORES
    ConsultaLista = "SELECT T0.cardcode, T1.cardname FROM SBO_TPD.DBO.OITM T0 "
    ConsultaLista &= "inner JOIN SBO_TPD.DBO.OCRD T1 ON T0.CARDCODE = T1.CardCode "
    'VALIDA SI SON EN TODAS LAS LINEAS O SOLO PROVEEDORES
    If cmblinea.SelectedItem Is Nothing Or cmblinea.Text = "TODAS" Then
     ConsultaLista &= "GROUP BY T0.CardCode, T1.CardName "
    Else
     ConsultaLista &= "WHERE T0.ItmsGrpCod = '" + Me.cmblinea.SelectedValue.ToString + "'"
     ConsultaLista &= "GROUP BY T0.CardCode, T1.CardName "
    End If

    'ASIGNACION DE CONSULTA
    Dim daAgte As New SqlClient.SqlDataAdapter(ConsultaLista, SqlConnection)
    'RECORRIDO DE CONSULTA
    daAgte.Fill(DSetTablas, "Proveedores")
    'VARIABLE DE ASIGNACION DE FILAS
    Dim filaAgte As Data.DataRow

    'Asignamos a fila la nueva Row(Fila)del Dataset
    filaAgte = DSetTablas.Tables("Proveedores").NewRow

    'Agregamos los valores a los campos de la tabla
    filaAgte("CardName") = "TODOS"
    filaAgte("CardCode") = "999"

    'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
    DSetTablas.Tables("Proveedores").Rows.Add(filaAgte)

    DvPro.Table = DSetTablas.Tables("Proveedores")

    Me.cmbproveedor.DataSource = DvPro
    Me.cmbproveedor.DisplayMember = "CardName"
    Me.cmbproveedor.ValueMember = "CardCode"
    Me.cmbproveedor.SelectedValue = "999"

   End Using

  Catch ex As Exception
   MsgBox("Error al cargar los proveedores: " + ex.Message)
  End Try
 End Sub
 Sub LlenaLinea()
  Try
   'VARIABLE DE CONSULTA
   Dim ConsutaLista As String
   'USO DE LA CONEXION
   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
    'VARIABLE DATASET 
    Dim DSetTablas As New DataSet
    'CONSULTA
    ConsutaLista = "SELECT ItmsGrpCod, ItmsGrpNam FROM OITB "
    'ASIGANACION DE CONSULTA
    Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)
    'RECORRIDO DE CONSULTA
    daAgte.Fill(DSetTablas, "LineasT")
    'ASIGNACVION DE FILAS DE LA CONSULTA
    Dim filaAgte As Data.DataRow
    'Asignamos a fila la nueva Row(Fila)del Dataset
    filaAgte = DSetTablas.Tables("LineasT").NewRow
    'Agregamos los valores a los campos de la tabla
    filaAgte("ItmsGrpCod") = 0
    filaAgte("ItmsGrpNam") = "TODAS"
    'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
    DSetTablas.Tables("LineasT").Rows.Add(filaAgte)
    DvLineasT.Table = DSetTablas.Tables("LineasT")
    Me.cmblinea.DataSource = DvLineasT
    Me.cmblinea.DisplayMember = "ItmsGrpNam"
    Me.cmblinea.ValueMember = "ItmsGrpCod"
    Me.cmblinea.SelectedValue = 0
   End Using
  Catch ex As Exception
   MsgBox("Error al cargar las lineas: " + ex.Message)
  End Try
 End Sub
 Private Sub BuscaProveedores()
  ''VALIDA SI SE REQUIERE BUSCAR EL PROVEEDOR AL INICIO
  If ini = False Then
   LlenaProveedores()
  End If
 End Sub

 Private Sub cmbproveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbproveedor.SelectedIndexChanged

 End Sub
 '======================================================================================================================================= FIN METODOS

End Class