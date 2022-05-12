
Imports System.Data.SqlClient

Public Class PagosRealizados
 Dim DvFactProv As New DataView
 Dim DvProveedor As New DataView
 'COLOR EN HEXADECIMAL
 Dim ColorGrid As Color = ColorTranslator.FromHtml("#FFFFFF")

 Sub cargar_registros()

  Dim DTRefacciones As New DataTable

  ' crear nueva conexión    
  Dim conexion2 As New SqlConnection(StrCon)

  ' abrir la conexión con la base de datos   
  conexion2.Open()

  Dim Adaptador As New SqlDataAdapter()
  Dim comando As New SqlCommand

  Dim SQLTPD As String

  SQLTPD = "SELECT CASE WHEN T3.FactCompras IS NULL OR T3.FactCompras=0 THEN 0 ELSE 1 END AS RegSel, "
  SQLTPD &= "T0.DocDate AS FchDoc, "
  SQLTPD &= "T2.PymntGroup AS DiasCred,T0.DocNum AS Factura, CASE WHEN T0.FolioPref IS NULL THEN '' ELSE T0.FolioPref END "
  SQLTPD &= "+ CASE WHEN CAST(T0.U_Factura AS nvarchar(10)) IS NULL THEN '' ELSE CAST(T0.U_Factura AS nvarchar(10)) END AS FactProv,"
  SQLTPD &= "T1.CardCode AS IdProved,T1.CardName AS Proveedor, 'a' as 'TipoGasto',"
  SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal ELSE T0.DocTotalFC END AS TotFactura,T0.DocCur AS Moneda,"
  SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.PaidToDate ELSE T0.PaidFC END AS Pagado,"
  SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal - T0.PaidToDate ELSE T0.DocTotalFC - T0.PaidFC END AS SaldoPendiente,"
  SQLTPD &= "T0.DocCur AS Moneda2,DATEDIFF(DAY,T0.DocDueDate,GETDATE()) AS DiasAtraso,T0.DocDueDate AS FchVen,"
  SQLTPD &= "T0.DocTotal - T0.PaidToDate AS SaldoPesos, t0.NumAtCard Referencia, T1.Notes AS Obrserv,T4.Coment,T3.Fecha as 'FechaPagada' "
  SQLTPD &= "FROM [SBO_TPD].dbo.OPCH T0 "
  SQLTPD &= "INNER JOIN [SBO_TPD].dbo.OCRD T1 ON T0.CardCode = T1.CardCode AND (T1.CardCode LIKE '%P-%' OR T1.CardCode LIKE '%PIM-%') "
  SQLTPD &= "LEFT JOIN [SBO_TPD].dbo.OCTG T2 ON T2.GroupNum = T1.GroupNum "
  SQLTPD &= "LEFT JOIN [TPM].dbo.FCOMP T3 ON T0.DocNum = T3.FactCompras "
  SQLTPD &= "LEFT JOIN [TPM].dbo.COMP1 T4 ON T0.DocNum = T4.FactCompras "
  SQLTPD &= "WHERE CAST(T3.Fecha as date) >= @FechaIni AND CAST(T3.Fecha as date) <= @FechaTer "
  SQLTPD &= "AND T3.Fecha IS NOT NULL "

  If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
   SQLTPD &= "AND T0.CardCode = @Agente "
  End If
  SQLTPD &= "AND T0.DocStatus = 'C'" 'Esta condicion determina si el documento esta Abierto o Cerrado
  SQLTPD &= " ORDER BY CAST(T3.Fecha as date) DESC, T0.DocNum DESC"

  ' Nuevo objeto Dataset   
  Dim DsVtasDet As New DataSet

  DsVtasDet.Tables.Add(DTRefacciones)

  With comando
   If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
    .Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
   End If
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   ' Asignar el sql para seleccionar los datos de la tabla Maestro   
   .CommandText = SQLTPD
   .Connection = conexion2
  End With

  Dim DtFactProv As New DataTable

  With Adaptador
   .SelectCommand = comando
   ' llenar el dataset   
   .Fill(DtFactProv)
  End With

  DvFactProv = DtFactProv.DefaultView

  With Me.DgFactProv
   .DataSource = DvFactProv

   .AllowUserToAddRows = False

   '.Columns(7).HeaderText = "$Total Factura"
   .Columns("TotFactura").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("TotFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   'Mnd
   '.Columns(8).HeaderText = "MND"
   .Columns("Moneda").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   'Importe Aplicado
   '.Columns(9).HeaderText = "Importe Aplicado"
   .Columns("Pagado").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("Pagado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   'Saldo Pendiente
   '.Columns(10).HeaderText = "$Saldo Pend"
   .Columns("SaldoPendiente").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("SaldoPendiente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   'Mnd
   .Columns("Moneda2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   'Dias Atraso
   .Columns("DiasAtraso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   '$ Saldo MXP
   .Columns("SaldoPesos").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("SaldoPesos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


   .Columns("Obrserv").Visible = False
   .Columns("RegSel").Visible = False
  End With

  With conexion2
   If .State = ConnectionState.Open Then
    .Close()
   End If
   .Dispose()
  End With

 End Sub

 Sub cargar_registros2()

  Dim DTRefacciones As New DataTable

  ' crear nueva conexión    
  Dim conexion2 As New SqlConnection(StrCon)

  ' abrir la conexión con la base de datos   
  conexion2.Open()

  Dim Adaptador As New SqlDataAdapter()
  Dim comando As New SqlCommand

  Dim SQLTPD As String

  SQLTPD = "SELECT CASE WHEN T3.FactCompras IS NULL OR T3.FactCompras=0 THEN 0 ELSE 1 END AS RegSel,T0.DocDate AS FchDoc,"
  SQLTPD &= "T2.PymntGroup AS DiasCred,T0.DocNum AS Factura,CASE WHEN T0.FolioPref IS NULL THEN '' ELSE T0.FolioPref END "
  SQLTPD &= "+ CASE WHEN CAST(T0.FolioNum AS nvarchar(10)) IS NULL THEN '' ELSE CAST(T0.FolioNum AS nvarchar(10)) END AS FactProv,"
  SQLTPD &= "T1.CardCode AS IdProved,T1.CardName AS Proveedor, t5.Name as 'TipoGasto', "
  SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal ELSE T0.DocTotalFC END AS TotFactura,T0.DocCur AS Moneda,"
  SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.PaidToDate ELSE T0.PaidFC END AS Pagado,"
  SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal - T0.PaidToDate ELSE T0.DocTotalFC - T0.PaidFC END AS SaldoPendiente,"
  SQLTPD &= "T0.DocCur AS Moneda2,DATEDIFF(DAY,T0.DocDueDate,GETDATE()) AS DiasAtraso,T0.DocDueDate AS FchVen,"
  SQLTPD &= "T0.DocTotal - T0.PaidToDate AS SaldoPesos,t0.NumAtCard Referencia,T1.Notes AS Obrserv,T4.Coment, T3.Fecha as 'FechaPagada' "
  SQLTPD &= "FROM OPCH t0 inner join OCRD t1 on t0.CardCode = t1.CardCode left join [@BXP_CONCEPTO] t5 on t1.U_BXP_CONCEPTO = t5.Code "
  SQLTPD &= "left join OCTG t2 on t1.GroupNum = t2.GroupNum "
  SQLTPD &= "LEFT JOIN [TPM].dbo.FCOMP T3 ON t0.DocNum = T3.FactCompras "
  SQLTPD &= "LEFT JOIN [TPM].dbo.COMP1 T4 ON T0.DocNum = T4.FactCompras "
  SQLTPD &= "WHERE t0.CardCode like 'PG%' AND CAST(T3.Fecha as date) >= @FechaIni AND CAST(T3.Fecha as date) <= @FechaTer "
  SQLTPD &= "AND T3.Fecha IS NOT NULL "
  'SQLTPD &= "WHERE T0.DocDueDate >= @FechaIni AND T0.DocDueDate <= @FechaTer "

  If CmbProveedor.SelectedIndex <> -1 Then
   If Me.CmbProveedor.SelectedValue <> "TODOS" Then
    SQLTPD &= "AND T0.CardCode = @Agente "
   ElseIf Me.CmbGasto.SelectedValue <> "TODOS" Then
    ''MsgBox("voy a vuscar todos de un solo tipo de gasto")
    ''MsgBox("seleccionaste el: " & CmbGasto.SelectedValue.ToString)
    SQLTPD &= "AND T0.CardCode in (select CardCode from OCRD where CardType = 'S' and CardCode like  'PG%' and U_BXP_CONCEPTO = @Concepto) "

   End If
  End If
  SQLTPD &= "AND T0.DocStatus = 'C'" 'Esta condicion determina si el documento esta Abierto o Cerrado
  SQLTPD &= " ORDER BY CAST(T3.Fecha as date) DESC, T0.DocNum DESC"

  ' Nuevo objeto Dataset   
  Dim DsVtasDet As New DataSet

  DsVtasDet.Tables.Add(DTRefacciones)

  With comando
   If CmbProveedor.SelectedIndex <> -1 Then
    If Me.CmbProveedor.SelectedValue <> "TODOS" Then
     .Parameters.AddWithValue("@Agente", CmbProveedor.SelectedValue)
    End If


   End If

   If CmbProveedor.SelectedIndex <> -1 Then
    If Me.CmbProveedor.SelectedValue <> "TODOS" Then

    ElseIf Me.CmbGasto.SelectedValue <> "TODOS" Then
     .Parameters.AddWithValue("@Concepto", Me.CmbGasto.SelectedValue)
    End If
   End If
   'If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
   '    .Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
   'End If
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   ' Asignar el sql para seleccionar los datos de la tabla Maestro   
   .CommandText = SQLTPD
   .Connection = conexion2
  End With


  Dim DtFactProv As New DataTable

  With Adaptador
   .SelectCommand = comando
   ' llenar el dataset   
   .Fill(DtFactProv)
  End With

  DvFactProv = DtFactProv.DefaultView

  With Me.DgFactProv
   .DataSource = DvFactProv

   .AllowUserToAddRows = False

   '$Total Fact
   '.Columns(7).HeaderText = "$Total Factura"
   .Columns("TotFactura").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("TotFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   'Mnd
   '.Columns(8).HeaderText = "MND"
   .Columns("Moneda").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   'Importe Aplicado
   '.Columns(9).HeaderText = "Importe Aplicado"
   .Columns("Pagado").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("Pagado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   'Saldo Pendiente
   '.Columns(10).HeaderText = "$Saldo Pend"
   .Columns("SaldoPendiente").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("SaldoPendiente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   'Mnd
   '.Columns(11).HeaderText = "MND"
   .Columns("Moneda2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   'Dias Atraso
   '.Columns(12).HeaderText = "ID prov"
   .Columns("DiasAtraso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   'Fch Venc.
   '.Columns(13).HeaderText = "Fch venc."

   '$ Saldo MXP
   '.Columns(14).HeaderText = "$ Saldo MXP"
   .Columns("SaldoPesos").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("SaldoPesos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   '.Columns("Referencia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

   'Observ
   .Columns("RegSel").Visible = False
   .Columns("Obrserv").Visible = False

  End With

  With conexion2
   If .State = ConnectionState.Open Then
    .Close()
   End If
   .Dispose()
  End With

 End Sub


 'BOTON CONSULTAR
 Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
  If RdBCompras.Checked = True Then
   Try
    DgFactProv.Columns("NombreGasto").Visible = False
   Catch ex As Exception

   End Try

            DgFactProv.Columns("Factura").Width = 40
            DgFactProv.Columns("U_IdLinea").Width = 80
            DgFactProv.Columns("IdProved").Width = 50
   Button1.Enabled = False
   If IsNothing(CmbAgteVta.SelectedValue) Then
    MessageBox.Show("Seleccione un proveedor de la lista",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    CmbAgteVta.Focus()
    Return
   End If
   'consulta que se mostrara
   cargar_registros()

   'muestra disa y pj
   VisualizarProv()

   Button1.Enabled = True
  Else
   Try
    DgFactProv.Columns("NombreGasto").Visible = True
   Catch ex As Exception

   End Try

            DgFactProv.Columns("Factura").Width = 50
            DgFactProv.Columns("U_IdLinea").Width = 80
            DgFactProv.Columns("IdProved").Width = 55
   'Button1.Enabled = False
   If CmbGasto.SelectedIndex = -1 Then
    MessageBox.Show("Seleccione un tipo de gasto de la lista",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    CmbGasto.Focus()
    Return
   End If

   If CmbProveedor.SelectedIndex = -1 Then
    MessageBox.Show("Seleccione un proveedor de la lista",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    CmbProveedor.Focus()
    Return
   End If
   cargar_registros2()
   VisualizarProv2()
   'VisualizarProv()

  End If
  'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
  'MFacturaBloq()
 End Sub

 'LOAD DEL FORMULARIO
 Private Sub PagosRealizados_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
  ChkVisDisa.Checked = False

  Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
  Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

  Dim ConsutaLista As String
  Dim DSetTablas As New DataSet

  Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
   ConsutaLista = "SELECT CardCode AS IdProv, CardName + ' - ' + Currency + ' - ' + CardCode  as Proveed FROM OCRD T0 WHERE T0.CardType = 'S' AND FROZENFOR <> 'Y' AND (T0.CardCode LIKE '%P-%' OR T0.CardCode LIKE '%PIM-%') ORDER BY T0.CardName "
   ConsutaLista &= "select Code, Name from [@BXP_CONCEPTO] ORDER BY Code "
   ConsutaLista &= "select CardCode, CardName, U_BXP_CONCEPTO from OCRD where CardType = 'S' and CardCode like  'PG%' order by CardName "
   Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

   daAgte.Fill(DSetTablas)

   Dim filaAgte As Data.DataRow

   'Asignamos a fila la nueva Row(Fila)del Dataset
   filaAgte = DSetTablas.Tables(0).NewRow

   'Agregamos los valores a los campos de la tabla
   filaAgte("Proveed") = "TODOS"
   filaAgte("IdProv") = "TODOS"

   'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
   DSetTablas.Tables(0).Rows.Add(filaAgte)

   Me.CmbAgteVta.DataSource = DSetTablas.Tables(0)
   Me.CmbAgteVta.DisplayMember = "Proveed"
   Me.CmbAgteVta.ValueMember = "IdProv"
   Me.CmbAgteVta.SelectedValue = "TODOS"

   filaAgte = DSetTablas.Tables(1).NewRow
   filaAgte("Code") = "TODOS"
   filaAgte("Name") = "TODOS"
   DSetTablas.Tables(1).Rows.Add(filaAgte)
   Me.CmbGasto.DataSource = DSetTablas.Tables(1)
   Me.CmbGasto.DisplayMember = "Name"
   Me.CmbGasto.ValueMember = "Code"
   Me.CmbGasto.SelectedValue = "TODOS"


   filaAgte = DSetTablas.Tables(2).NewRow
   filaAgte("CardCode") = "TODOS"
   filaAgte("CardName") = "TODOS"
   DSetTablas.Tables(2).Rows.Add(filaAgte)
   DvProveedor.Table = DSetTablas.Tables(2)
   Me.CmbProveedor.DataSource = DvProveedor
   Me.CmbProveedor.DisplayMember = "CardName"
   Me.CmbProveedor.ValueMember = "CardCode"
   Me.CmbProveedor.SelectedValue = "TODOS"
  End Using
 End Sub

 'EXPORTAR A EXCEL
 Private Sub BtnDetalle_Click(sender As System.Object, e As System.EventArgs) Handles BtnDetalle.Click

  If (RdBCompras.Checked = True) Then
   BtnDetalle.Enabled = False
   Dim oExcel As Object
   Dim oBook As Object
   Dim oSheet As Object

   'Abrimos un nuevo libro
   oExcel = CreateObject("Excel.Application")
   oBook = oExcel.workbooks.add
   oSheet = oBook.worksheets(1)

   'Declaramos el nombre de las columnas
   'oSheet.range("A3").value = "Pagado"
   'oSheet.range("B3").value = "Bloqueado"
   oSheet.range("A3").value = "Fecha del Doc."
   oSheet.range("B3").value = "Dias Cred"
   oSheet.range("C3").value = "Doc.Sap"
   oSheet.range("D3").value = "Factura"
   oSheet.range("E3").value = "IdProv"
   oSheet.range("F3").value = "Proveedor"
   oSheet.range("G3").value = "$ Total Factura"
   oSheet.range("H3").value = "Mnd"
   oSheet.range("I3").value = "$ Importe Aplicado"
   oSheet.range("J3").value = "$ Saldo Pendiente"
   oSheet.range("K3").value = "Mnd"
   oSheet.range("L3").value = "Dias Atraso"
   oSheet.range("M3").value = "Fecha Vencimiento"
   oSheet.range("N3").value = "$ Saldo MXP"
   oSheet.range("O3").value = "Referencia"
   oSheet.range("P3").value = "Observaciones"
   oSheet.range("Q3").value = "Comentarios"
   oSheet.range("R3").value = "Fecha de Pago"

   'para poner la primera fila de los titulos en negrita
   oSheet.range("A3:R3").font.bold = True
   oSheet.Range("A3:R3").Cells.Interior.Color = RGB(191, 191, 191)

   Dim fila_dt As Integer = 0
   Dim fila_dt_excel As Integer = 0
   Dim tanto_porcentaje As String = ""
   Dim vTot As String = ""

   Const xlEdgeLeft = 7
   Const xlEdgeRight = 10
   Const xlEdgeTop = 8
   Const xlEdgeBottom = 9
   Const xlInsideHorizontal = 12
   Const xlInsideVertical = 11

   Const xlContinuous = 1
   Const xlThin = 2
   Const xlAutomatic = -4105

   Dim total_reg As Integer = 0

   total_reg = DgFactProv.RowCount
   For fila_dt = 0 To total_reg - 1

    'para leer una celda en concreto
    'el numero es la columna
    Dim cel1 As Date = Me.DgFactProv.Item(1, fila_dt).Value
    Dim cel2 As String = Me.DgFactProv.Item(2, fila_dt).Value
    Dim cel3 As String = Me.DgFactProv.Item(3, fila_dt).Value
    Dim cel4 As String = IIf(IsDBNull(Me.DgFactProv.Item(4, fila_dt).Value), "", Me.DgFactProv.Item(4, fila_dt).Value)

    Dim cel5 As String = Me.DgFactProv.Item(5, fila_dt).Value
    Dim cel6 As String = Me.DgFactProv.Item(6, fila_dt).Value
    Dim cel7 As String = IIf(IsDBNull(Me.DgFactProv.Item(8, fila_dt).Value), 0, Me.DgFactProv.Item(8, fila_dt).Value)
    Dim cel8 As String = Me.DgFactProv.Item(9, fila_dt).Value

    Dim cel9 As String = IIf(IsDBNull(Me.DgFactProv.Item(10, fila_dt).Value), 0, Me.DgFactProv.Item(10, fila_dt).Value)
    Dim cel10 As String = IIf(IsDBNull(Me.DgFactProv.Item(11, fila_dt).Value), 0, Me.DgFactProv.Item(11, fila_dt).Value)
    Dim cel11 As String = Me.DgFactProv.Item(12, fila_dt).Value
    Dim cel12 As String = Me.DgFactProv.Item(13, fila_dt).Value
    Dim cel13 As Date = Me.DgFactProv.Item(14, fila_dt).Value
    Dim cel14 As String = IIf(IsDBNull(Me.DgFactProv.Item(15, fila_dt).Value), 0, Me.DgFactProv.Item(15, fila_dt).Value)
    Dim cel15 As String = IIf(IsDBNull(Me.DgFactProv.Item(16, fila_dt).Value), "", Me.DgFactProv.Item(16, fila_dt).Value)
    Dim cel16 As String = IIf(IsDBNull(Me.DgFactProv.Item(17, fila_dt).Value), "", Me.DgFactProv.Item(17, fila_dt).Value)
    Dim cel17 As String = IIf(IsDBNull(Me.DgFactProv.Item(18, fila_dt).Value), "", Me.DgFactProv.Item(18, fila_dt).Value)
    Dim cel18 As String = IIf(IsDBNull(Me.DgFactProv.Item(19, fila_dt).Value), "", Me.DgFactProv.Item(19, fila_dt).Value)

    fila_dt_excel = fila_dt + 4

    vTot = "A" + (fila_dt_excel).ToString
    vTot &= ":R" + (fila_dt_excel).ToString


    oSheet.Range(vTot).Cells.Interior.Color = ColorGrid
    'COLOCA EL COLOR DE LETRA DEPENDIENDO AL BLOQUEADO
    oSheet.Range(vTot).Cells.Font.Color = Color.Black

    'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
    oSheet.range("A" & fila_dt_excel).value = cel1
    oSheet.range("B" & fila_dt_excel).value = cel2
    oSheet.range("C" & fila_dt_excel).value = cel3
    oSheet.range("D" & fila_dt_excel).value = cel4
    oSheet.range("E" & fila_dt_excel).value = cel5
    oSheet.range("F" & fila_dt_excel).value = cel6
    oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
    oSheet.range("H" & fila_dt_excel).value = cel8
    oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
    oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel10, 2)
    oSheet.range("K" & fila_dt_excel).value = cel11
    oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel12, 2)
    oSheet.range("M" & fila_dt_excel).value = cel13
    oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel14, 2)
    oSheet.range("O" & fila_dt_excel).value = cel15
    oSheet.range("P" & fila_dt_excel).value = cel16
    oSheet.range("Q" & fila_dt_excel).value = cel17
    oSheet.range("R" & fila_dt_excel).value = cel18
   Next

   vTot = "A3"
   vTot &= ":r" + (fila_dt_excel).ToString

   'Seleccionamos la hoja
   oBook.worksheets(1).Select()
   'Seleccionamos elrango de celdas al cual le vamos a poner bordes
   oBook.worksheets(1).Range(vTot).Select()


   'Aqui mostramos los bordes Izquierdos
   With oExcel.Selection.Borders(xlEdgeLeft)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With
   'Aqui mostramos los bordes de Arriba
   With oExcel.Selection.Borders(xlEdgeTop)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With
   'Aqui mostramos los bordes de abajo
   With oExcel.Selection.Borders(xlEdgeBottom)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With
   'Aqui mostramos los bordes derechos
   With oExcel.Selection.Borders(xlEdgeRight)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With
   'Aqui mostramos los bordes interiores verticales
   With oExcel.Selection.Borders(xlInsideVertical)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With
   'Aqui mostramos los bordes interiores horizontales
   With oExcel.Selection.Borders(xlInsideHorizontal)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With


   vTot = "P" + (fila_dt_excel + 1).ToString
   oSheet.range(vTot).value = TxtTotEnPesos.Text

   oSheet.range(vTot).font.bold = True

   oSheet.columns("A:R").entirecolumn.autofit()

   vTot = "N" + (fila_dt_excel + 1).ToString
   'MsgBox(vTot)
   oSheet.range(vTot).value = LblTotal.Text

   oSheet.range(vTot).font.bold = True

   oSheet.range("A1").value = "Reporte de Pagos realizados a Proveedores Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
   oSheet.Range("A2").select()

   oExcel.visible = True
   System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
   GC.Collect()
   oSheet = Nothing
   oBook = Nothing
   oExcel = Nothing
   BtnDetalle.Enabled = True
  Else
   ''MsgBox("Entre al else")
   BtnDetalle.Enabled = False
   Dim oExcel As Object
   Dim oBook As Object
   Dim oSheet As Object

   'Abrimos un nuevo libro
   oExcel = CreateObject("Excel.Application")
   oBook = oExcel.workbooks.add
   oSheet = oBook.worksheets(1)

   'Declaramos el nombre de las columnas
   oSheet.range("A3").value = "Fecha del Doc."
   oSheet.range("B3").value = "Dias Cred"
   oSheet.range("C3").value = "Doc.Sap"
   oSheet.range("D3").value = "Factura"
   oSheet.range("E3").value = "IdProv"
   oSheet.range("F3").value = "Proveedor"
   oSheet.range("G3").value = "Tipo de Gasto"
   oSheet.range("H3").value = "$ Total Factura"
   oSheet.range("I3").value = "Mnd"
   oSheet.range("J3").value = "$ Importe Aplicado"
   oSheet.range("K3").value = "$ Saldo Pendiente"
   oSheet.range("L3").value = "Mnd"
   oSheet.range("M3").value = "Dias Atraso"
   oSheet.range("N3").value = "Fecha Vencimiento"
   oSheet.range("O3").value = "$ Saldo MXP"
   oSheet.range("P3").value = "Referencia"
   oSheet.range("Q3").value = "Comentarios"
   oSheet.range("R3").value = "Fecha de Pago"

   'para poner la primera fila de los titulos en negrita
   oSheet.range("A3:R3").font.bold = True
   oSheet.Range("A3:R3").Cells.Interior.Color = RGB(191, 191, 191)

   Dim fila_dt As Integer = 0
   Dim fila_dt_excel As Integer = 0
   Dim tanto_porcentaje As String = ""
   Dim vTot As String = ""

   Const xlEdgeLeft = 7
   Const xlEdgeRight = 10
   Const xlEdgeTop = 8
   Const xlEdgeBottom = 9
   Const xlInsideHorizontal = 12
   Const xlInsideVertical = 11

   Const xlContinuous = 1
   Const xlThin = 2
   Const xlAutomatic = -4105

   Dim total_reg As Integer = 0

   total_reg = DgFactProv.RowCount
   For fila_dt = 0 To total_reg - 1

    'para leer una celda en concreto
    'el numero es la columna
    Dim cel1 As Date = Me.DgFactProv.Item(1, fila_dt).Value
    Dim cel2 As String = Me.DgFactProv.Item(2, fila_dt).Value
    Dim cel3 As String = Me.DgFactProv.Item(3, fila_dt).Value
    Dim cel4 As String = IIf(IsDBNull(Me.DgFactProv.Item(4, fila_dt).Value), "", Me.DgFactProv.Item(4, fila_dt).Value)
    'Me.DgFactProv.Item(4, fila_dt).Value

    Dim cel5 As String = Me.DgFactProv.Item(5, fila_dt).Value
    Dim cel6 As String = Me.DgFactProv.Item(6, fila_dt).Value
    Dim cel6_1 As String = IIf(IsDBNull(Me.DgFactProv.Item(7, fila_dt).Value), "", Me.DgFactProv.Item(7, fila_dt).Value)
    Dim cel7 As String = IIf(IsDBNull(Me.DgFactProv.Item(8, fila_dt).Value), 0, Me.DgFactProv.Item(8, fila_dt).Value)
    Dim cel8 As String = Me.DgFactProv.Item(9, fila_dt).Value

    Dim cel9 As String = IIf(IsDBNull(Me.DgFactProv.Item(10, fila_dt).Value), 0, Me.DgFactProv.Item(10, fila_dt).Value)
    Dim cel10 As String = IIf(IsDBNull(Me.DgFactProv.Item(11, fila_dt).Value), 0, Me.DgFactProv.Item(11, fila_dt).Value)
    Dim cel11 As String = Me.DgFactProv.Item(12, fila_dt).Value
    Dim cel12 As String = Me.DgFactProv.Item(13, fila_dt).Value
    Dim cel13 As Date = Me.DgFactProv.Item(14, fila_dt).Value
    Dim cel14 As String = IIf(IsDBNull(Me.DgFactProv.Item(15, fila_dt).Value), 0, Me.DgFactProv.Item(15, fila_dt).Value)
    Dim cel15 As String = IIf(IsDBNull(Me.DgFactProv.Item(16, fila_dt).Value), "", Me.DgFactProv.Item(16, fila_dt).Value)
    Dim cel16 As String = IIf(IsDBNull(Me.DgFactProv.Item(18, fila_dt).Value), "", Me.DgFactProv.Item(18, fila_dt).Value)
    Dim cel17 As String = IIf(IsDBNull(Me.DgFactProv.Item(19, fila_dt).Value), "", Me.DgFactProv.Item(19, fila_dt).Value)

    fila_dt_excel = fila_dt + 4

    vTot = "A" + (fila_dt_excel).ToString
    vTot &= ":R" + (fila_dt_excel).ToString
    'AQUI
    oSheet.Range(vTot).Cells.Interior.Color = ColorGrid
    'COLOCA EL COLOR DE LETRA DEPENDIENDO AL BLOQUEADO
    oSheet.Range(vTot).Cells.Font.Color = Color.Black

    'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
    oSheet.range("A" & fila_dt_excel).value = cel1
    oSheet.range("B" & fila_dt_excel).value = cel2
    oSheet.range("C" & fila_dt_excel).value = cel3
    oSheet.range("D" & fila_dt_excel).value = cel4
    oSheet.range("E" & fila_dt_excel).value = cel5
    oSheet.range("F" & fila_dt_excel).value = cel6
    oSheet.range("G" & fila_dt_excel).value = cel6_1
    oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
    oSheet.range("I" & fila_dt_excel).value = cel8
    oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel9, 2)
    oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel10, 2)
    oSheet.range("L" & fila_dt_excel).value = cel11
    oSheet.range("M" & fila_dt_excel).value = cel12
    oSheet.range("N" & fila_dt_excel).value = cel13
    oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel14, 2)
    oSheet.range("P" & fila_dt_excel).value = cel15
    oSheet.range("Q" & fila_dt_excel).value = cel16
    oSheet.range("R" & fila_dt_excel).value = cel17

   Next

   vTot = "A3"
   vTot &= ":R" + (fila_dt_excel).ToString

   'Seleccionamos la hoja
   oBook.worksheets(1).Select()
   'Seleccionamos elrango de celdas al cual le vamos a poner bordes
   oBook.worksheets(1).Range(vTot).Select()


   'Aqui mostramos los bordes Izquierdos
   With oExcel.Selection.Borders(xlEdgeLeft)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With
   'Aqui mostramos los bordes de Arriba
   With oExcel.Selection.Borders(xlEdgeTop)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With
   'Aqui mostramos los bordes de abajo
   With oExcel.Selection.Borders(xlEdgeBottom)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With
   'Aqui mostramos los bordes derechos
   With oExcel.Selection.Borders(xlEdgeRight)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With
   'Aqui mostramos los bordes interiores verticales
   With oExcel.Selection.Borders(xlInsideVertical)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With
   'Aqui mostramos los bordes interiores horizontales
   With oExcel.Selection.Borders(xlInsideHorizontal)
    .LineStyle = xlContinuous
    .Weight = xlThin
    .ColorIndex = xlAutomatic
   End With


   vTot = "Q" + (fila_dt_excel + 1).ToString
   oSheet.range(vTot).value = TxtTotEnPesos.Text

   oSheet.range(vTot).font.bold = True

   oSheet.columns("A:R").entirecolumn.autofit()

   vTot = "O" + (fila_dt_excel + 1).ToString
   'MsgBox(vTot)
   oSheet.range(vTot).value = LblTotal.Text

   oSheet.range(vTot).font.bold = True

   oSheet.range("A1").value = "Reporte de Pagos realizados a Proveedores Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
   oSheet.Range("A2").select()

   oExcel.visible = True
   System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
   GC.Collect()
   oSheet = Nothing
   oBook = Nothing
   oExcel = Nothing
   BtnDetalle.Enabled = True

  End If

 End Sub

 'Private Sub DgFactProv_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgFactProv.CellDoubleClick
 ' If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
 '  'MessageBox.Show(Me.DgFactProv.Columns(e.ColumnIndex).Name)
 '  'MODIFICO IVAN GONZALEZ
 '  'DEPENDIENDO EL PERFIL Y EL NOMBRE DE LA COLUMNA
 '  Dim bandera As Boolean
 '  If Me.DgFactProv.Columns(e.ColumnIndex).Name = "Comentarios" And UsrTPM = "COMPRAS1" Then
 '   bandera = True
 '   'ElseIf Me.DgFactProv.Columns(e.ColumnIndex).Name = "Comentarios Direccion" And UsrTPM = "MANAGER" Then
 '  ElseIf UsrTPM = "MANAGER" Then
 '   bandera = True
 '  End If

 '  If bandera Then
 '   Dim f2 As New PagoCapCom()
 '   f2.TxtFactura1.Text = DgFactProv.Rows(e.RowIndex).Cells("Factura").Value
 '   f2.TxtIdProv.Text = DgFactProv.Rows(e.RowIndex).Cells("IdProved").Value
 '   f2.TxtNomProv.Text = DgFactProv.Rows(e.RowIndex).Cells("Proveedor").Value

 '   f2.TxtFchDoc.Text = DgFactProv.Rows(e.RowIndex).Cells("FchDoc").Value
 '   f2.TxtFchVenc.Text = DgFactProv.Rows(e.RowIndex).Cells("FchVen").Value
 '   f2.TxtSaldo.Text = Format(DgFactProv.Rows(e.RowIndex).Cells("SaldoPesos").Value, "$ ##,###,###,###.00")
 '   f2.TxtDiasAtraso.Text = DgFactProv.Rows(e.RowIndex).Cells("DiasAtraso").Value
 '   If IsDBNull(DgFactProv.Rows(e.RowIndex).Cells("Comentarios").Value) Then
 '    f2.TxtComentario.Text = ""
 '   Else
 '    f2.TxtComentario.Text = DgFactProv.Rows(e.RowIndex).Cells("Comentarios").Value
 '   End If
 '   f2.LblRow.Text = e.RowIndex.ToString
 '   f2.ShowDialog()
 '  End If

 ' End If
 'End Sub

 Private Sub DgFactProv_RowPrePaint(sender As System.Object, e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DgFactProv.RowPrePaint
  DgFactProv.Rows(e.RowIndex).Cells("FchDoc").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("DiasCred").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("Factura").Style.BackColor = ColorGrid
        DgFactProv.Rows(e.RowIndex).Cells("FactProv").Style.BackColor = ColorGrid
        DgFactProv.Rows(e.RowIndex).Cells("U_IdLinea").Style.BackColor = ColorGrid
        DgFactProv.Rows(e.RowIndex).Cells("IdProved").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("Proveedor").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("NombreGasto").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("TotFactura").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("Moneda").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("Pagado").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("SaldoPendiente").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("Moneda2").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("FchVen").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("SaldoPesos").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("Referencia").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("Obrserv").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("Comentarios").Style.BackColor = ColorGrid
  DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Style.BackColor = ColorGrid
 End Sub

 Sub VisualizarProv()
  Dim VFiltro As String = " "
  If ChkVisDisa.Checked = False Then
   VFiltro = "IdProved <> 'P-064' AND IdProved <> 'P-001' AND IdProved <> 'P-002' AND IdProved <> 'P-104' AND IdProved <> 'P-105'"
   'VFiltro = "IdProved <> 'P-064' AND IdProved <> 'P-001' AND IdProved <> 'P-002'"
  End If

  'If ChkVerPagadas.Checked = False Then
  ' If ChkVisDisa.Checked = False Then
  '  VFiltro &= " AND RegSel = 0"
  ' Else
  '  VFiltro = "RegSel = 0"
  ' End If
  'End If

  If ChkUSD.Checked = True Then
   If ChkVisDisa.Checked = False Then
    VFiltro &= " AND Moneda = 'USD' "
   Else
    VFiltro = "Moneda = 'USD'"
   End If
  End If

  If VFiltro = " " Then
   DvFactProv.RowFilter = String.Empty
  Else
   DvFactProv.RowFilter = VFiltro
  End If

  TotalFacturas()
 End Sub

 Sub VisualizarProv2()
  Dim VFiltro As String = " "

  If ChkUSD.Checked = True Then
   VFiltro &= "Moneda = 'USD' "
  End If

  If VFiltro = " " Then
   DvFactProv.RowFilter = String.Empty
  Else
   DvFactProv.RowFilter = VFiltro
  End If

  TotalFacturas()
 End Sub

 Private Sub ChkVisualizar_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkVisDisa.CheckedChanged
  VisualizarProv()
  'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
  'MFacturaBloq()
 End Sub


 Private Sub DgFactProv_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgFactProv.CellContentClick

  Dim VErrorG As Integer = 0
  Dim strValue As String
  Dim strBloq As String


  If e.RowIndex >= 0 Then
   Dim row As DataGridViewRow = DgFactProv.Rows(e.RowIndex)
   Try

    If Me.DgFactProv.Columns(e.ColumnIndex).Name = "RegSel" And UsrTPM = "MANAGER" Then
     'VALIDA SI ESTA BLOQUEADO O NO LA FACTURA Y QUE EL USUARIO SEA DE COMPRAS
     If (CInt(Me.DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value) = 0) Then
      'The user clicked on the checkbox column
      strValue = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value
      If (strValue = "1") Then
       Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value = DBNull.Value
      End If

      strValue = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value

      'MsgBox(strValue)
      If MessageBox.Show("¿Confirma que esta factura fue pagada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

       DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 1
       DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Value = DateTime.Now

       Me.DgFactProv.RefreshEdit()

       Dim con As New SqlConnection
       Dim cmd As New SqlCommand
       Dim CadenaSQL As String = ""

       'UsrTPM = "MANAGER"
       CadenaSQL = "INSERT INTO FCOMP (FactCompras,Fecha,Id_Usuario) VALUES ("
       CadenaSQL &= DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString
       CadenaSQL &= ","
       CadenaSQL &= "@fecha"
       CadenaSQL &= ",'"
       CadenaSQL &= UsrTPM
       CadenaSQL &= "')"

       Try
        con.ConnectionString = StrTpm
        con.Open()
        cmd.Connection = con
        cmd.CommandText = CadenaSQL
        cmd.Parameters.Add(New SqlParameter("@fecha", DateTime.Now))
        cmd.ExecuteNonQuery()

        Me.DgFactProv.RefreshEdit()

       Catch ex As Exception
        MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

       Finally

        con.Close()
       End Try

      Else
       DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 0
       Me.DgFactProv.RefreshEdit()

      End If
     ElseIf (CInt(Me.DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value) = 1) Then
      MessageBox.Show("La factura esta bloqueada para pago.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      Return
     End If
    ElseIf (Me.DgFactProv.Columns(e.ColumnIndex).Name = "Frozen" And UsrTPM <> "MANAGER") Then 'VALIDA SI SE VA A BLOQUEAR LA ORDEN LA FACTURA PARA PAGO Y QUE EL USUARIO NO SEA MANAGER
     strBloq = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value
     If (strBloq = "1") Then
      Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value = DBNull.Value
      DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value = 0
      'REFRESCA EL CHECK
      Me.DgFactProv.RefreshEdit()
      Dim con As New SqlConnection
      Dim cmd As New SqlCommand
      Dim CadenaSQL As String = ""

      'VALIDA SI EXISTE EL REGISTRO
      Try
       'ALAMACENA LA CONSULTA
       CadenaSQL = "SELECT * FROM COMP1 WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "
       'CONECTA A LA BASE DE DATOS
       conexion_universal.conectar()
       'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
       conexion_universal.slq_s = New SqlCommand(CadenaSQL, conexion_universal.conexion_uni)
       'EJECUTA LA CONSULTA
       conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
       'RECORRE LA CONSULTA
       If conexion_universal.rd_s.Read Then
        CadenaSQL = ""
        'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
        CadenaSQL = "UPDATE COMP1 SET Frozen = '" + DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value.ToString + "'  "
        CadenaSQL &= "WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "
        con.ConnectionString = StrTpm
        con.Open()
        cmd.Connection = con
        cmd.CommandText = CadenaSQL
        cmd.ExecuteNonQuery()

        Me.DgFactProv.RefreshEdit()
       End If
      Catch ex As Exception
       MessageBox.Show("Error al actualizar el Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Finally
       'CIERRA LAS CONEXIONES DE USO
       con.Close()
       conexion_universal.cerrar_conectar()
      End Try

      DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value = 0
      Me.DgFactProv.RefreshEdit()
     Else
      If MessageBox.Show("¿Relamente desea bloquear la factura para pago?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
       DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value = 1
       Me.DgFactProv.RefreshEdit()

       Dim con As New SqlConnection
       Dim cmd As New SqlCommand
       Dim CadenaSQL As String = ""

       'VALIDA SI EXISTE EL REGISTRO
       Try
        'ALAMACENA LA CONSULTA
        CadenaSQL = "SELECT * FROM COMP1 WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "
        'CONECTA A LA BASE DE DATOS
        conexion_universal.conectar()
        'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
        conexion_universal.slq_s = New SqlCommand(CadenaSQL, conexion_universal.conexion_uni)
        'EJECUTA LA CONSULTA
        conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
        'RECORRE LA CONSULTA
        If conexion_universal.rd_s.Read Then
         CadenaSQL = ""
         'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
         CadenaSQL = "UPDATE COMP1 SET Frozen = '" + DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value.ToString + "'  "
         CadenaSQL &= "WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "
         con.ConnectionString = StrTpm
         con.Open()
         cmd.Connection = con
         cmd.CommandText = CadenaSQL
         cmd.ExecuteNonQuery()
         Me.DgFactProv.RefreshEdit()
        Else
         CadenaSQL = ""
         'ALMACENA LA CONSULTA
         CadenaSQL = "INSERT INTO COMP1 (FactCompras,Fecha,Id_Usuario, Frozen) VALUES ("
         CadenaSQL &= DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString
         CadenaSQL &= ","
         CadenaSQL &= "@fecha"
         CadenaSQL &= ",'"
         CadenaSQL &= UsrTPM
         CadenaSQL &= "', '" + DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value.ToString + "') "
         con.ConnectionString = StrTpm
         con.Open()
         cmd.Connection = con
         cmd.CommandText = CadenaSQL
         cmd.Parameters.Add(New SqlParameter("@fecha", DateTime.Now))
         cmd.ExecuteNonQuery()

         Me.DgFactProv.RefreshEdit()
        End If
       Catch ex As Exception
        MessageBox.Show("Error al actualizar o insertar el Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
       Finally
        'CIERRA LAS CONEXIONES DE USO
        con.Close()
        conexion_universal.cerrar_conectar()
       End Try
      Else
       DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value = 0
       Me.DgFactProv.RefreshEdit()
      End If
     End If
    End If
   Catch ex As Exception
    VErrorG = 1
   End Try

   'If VErrorG = 1 Then
   ' If MessageBox.Show("¿Confirma que esta factura" + Chr(13) + "No ha sido pagada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

   '  DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 0
   '  DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Value = DBNull.Value

   '  Me.DgFactProv.RefreshEdit()
   '  Dim con As New SqlConnection
   '  Dim cmd As New SqlCommand
   '  Try
   '   con.ConnectionString = StrTpm
   '   con.Open()
   '   cmd.Connection = con
   '   cmd.CommandText = "Delete From FCOMP where FactCompras = @FactComp"
   '   cmd.Parameters.Add(New SqlParameter("@FactComp", DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString))
   '   cmd.ExecuteNonQuery()

   '  Catch ex As Exception
   '   MessageBox.Show("Error Eliminando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

   '  Finally

   '   con.Close()
   '  End Try
   ' Else
   '  DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 1
   '  'DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Value = " "
   '  Me.DgFactProv.RefreshEdit()

   ' End If

   'End If

   TotalFacturas()
  End If
 End Sub
 Private Sub DgFactProv_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles DgFactProv.CurrentCellDirtyStateChanged

  'Este codigo sirve para que se pueda identificar el proceso del checkbox dentro del datagridview junto
  'con el evento de DgFactProv_CellContentClick
  If DgFactProv.IsCurrentCellDirty Then
   DgFactProv.CommitEdit(DataGridViewDataErrorContexts.Commit)
  End If
 End Sub

 Sub TotalFacturas()
  Dim VTotFProv As Decimal = 0

  For Each row As DataGridViewRow In Me.DgFactProv.Rows
   If row.Cells("Moneda").Value = "MXP" Then
    VTotFProv += row.Cells("pagado").Value
   End If
  Next

  TxtTotEnPesos.Text = Format(VTotFProv, "$ ##,###,###,###.00")
 End Sub

 ''METODO DE BLOQUEAR FACTURAS
 'Sub MFacturaBloq()
 ' 'RECORRE EL GRID Y COMPARA CON LA BASE DE DATOS SI LA FACTURA ESTA BLOQUEADA
 ' For i As Integer = 0 To DgFactProv.Rows.Count - 1
 '  Try
 '   'APERTURA DE CONEXION
 '   conexion_universal.conexion_uni.Open()
 '   'VARIABLE DE CONSULTA
 '   Dim SQLBuscar As String = ""
 '   'ALMACENA LA CONSULTA
 '   SQLBuscar = "SELECT CASE WHEN Frozen IS NULL THEN 0 ELSE Frozen END AS Frozen "
 '   SQLBuscar &= "FROM TPM.dbo.COMP1 "
 '   SQLBuscar &= "WHERE FactCompras = " + DgFactProv.Rows(i).Cells("Factura").Value.ToString + " "
 '   'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
 '   conexion_universal.slq_s = New SqlCommand(SQLBuscar, conexion_universal.conexion_uni)
 '   'EJECUTA LA CONSULTA
 '   conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
 '   'RECORRE LA CONSULTA
 '   If conexion_universal.rd_s.Read Then
 '    'VALIDA SI LA FACTURA ESTA BLOQUEADO
 '    If conexion_universal.rd_s.Item("Frozen") = 1 Then
 '     DgFactProv.Rows(i).Cells("Frozen").Value = 1

 '     'COLOCA COLOR A TODA LA FILA
 '     DgFactProv.Rows(i).Cells(0).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(1).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(2).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(3).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(4).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(4).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(4).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(5).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(6).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(7).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(8).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(9).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(10).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(11).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(12).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(13).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(14).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(15).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(16).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(17).Style.BackColor = ColorGrid
 '     DgFactProv.Rows(i).Cells(18).Style.BackColor = ColorGrid

 '    Else
 '     DgFactProv.Rows(i).Cells("Frozen").Value = 0
 '    End If
 '   End If
 '  Catch ex As Exception
 '   MsgBox("Error al buscar la factura bloqueada: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de Busqueda")
 '   'CIERRA LA CONEXION
 '   conexion_universal.conexion_uni.Close()
 '   Return
 '  Finally
 '   'CIERRA LAS CONEXIONES DE USO
 '   conexion_universal.conexion_uni.Close()
 '  End Try
 ' Next
 'End Sub

 Private Sub ChkVerPagadas_CheckedChanged(sender As System.Object, e As System.EventArgs)
  If RdBCompras.Checked = True Then
   VisualizarProv()
   'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
   'MFacturaBloq()
  Else
   VisualizarProv2()
   'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
   'MFacturaBloq()
  End If

 End Sub

 Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
  If (RadioButton1.Checked = True) Then
   limpiarDataGrid()
   'DgFactProv.DataSource = ""
   Label1.Text = "Gasto:"
   Label1.Location = New Point(30, 98)
   ChkVisDisa.Visible = False
   CmbAgteVta.Visible = False
   DgFactProv.Height = DgFactProv.Height - 25
   DgFactProv.Location = New Point(7, 180)
   Label6.Location = New Point(7, 160)
   CmbGasto.Location = New Point(83, 97)
   CmbGasto.Visible = True
   Label8.Location = New Point(5, 131)
   Label8.Visible = True
   CmbProveedor.Location = New Point(83, 131)
   CmbProveedor.Visible = True
   TxtTotEnPesos.Text = ""
   ChkUSD.Location = New Point(790, 64)
   'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
   'MFacturaBloq()


  Else
   limpiarDataGrid()
   'DgFactProv.DataSource = ""
   Label1.Text = "Proveedor"
   Label1.Location = New Point(5, 98)
   ChkVisDisa.Visible = True
   CmbAgteVta.Visible = True
   DgFactProv.Height = DgFactProv.Height + 25
   DgFactProv.Location = New Point(7, 155)
   Label6.Location = New Point(7, 135)
   CmbGasto.Location = New Point(509, 8)
   CmbGasto.Visible = False
   Label8.Location = New Point(429, 34)
   Label8.Visible = False
   CmbProveedor.Location = New Point(509, 33)
   CmbProveedor.Visible = False
   TxtTotEnPesos.Text = ""
   ChkUSD.Location = New Point(949, 64)
   'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
   'MFacturaBloq()

  End If
 End Sub
 Public Sub limpiarDataGrid()
  For i As Integer = 0 To Me.DgFactProv.RowCount - 1
   Me.DgFactProv.Rows.Remove(Me.DgFactProv.CurrentRow)
  Next

 End Sub

 Private Sub CmbGasto_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbGasto.SelectionChangeCommitted
  If (CmbGasto.SelectedValue = "TODOS") Then
   DvProveedor.RowFilter = String.Empty
   Me.CmbProveedor.SelectedValue = "TODOS"
  Else
   DvProveedor.RowFilter = "U_BXP_CONCEPTO = '" & CmbGasto.SelectedValue.ToString & "' or CardName = 'TODOS'"
   Me.CmbProveedor.SelectedValue = "TODOS"
  End If
  ''DvProveedor.RowFilter = "U_BXP_CONCEPTO = '" & CmbGasto.SelectedValue.ToString & "'"
 End Sub

 Private Sub CmbGasto_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbGasto.Validating
  If (CmbGasto.Text <> "") Then
   If (CmbGasto.FindString(CmbGasto.Text.ToString).ToString <> "-1") Then
    CmbGasto.SelectedIndex = CmbGasto.FindString(CmbGasto.Text.ToString)
    'MsgBox(CmbGasto.SelectedValue.ToString)
    If (CmbGasto.SelectedValue = "TODOS") Then
     DvProveedor.RowFilter = String.Empty
     Me.CmbProveedor.SelectedValue = "TODOS"
    Else
     DvProveedor.RowFilter = "U_BXP_CONCEPTO = '" & CmbGasto.SelectedValue.ToString & "' or CardName = 'TODOS'"
     Me.CmbProveedor.SelectedValue = "TODOS"
    End If

   Else
    CmbGasto.SelectedIndex = -1
    DvProveedor.RowFilter = "U_BXP_CONCEPTO = 'TODOS'"
   End If
  End If
 End Sub

 Private Sub CmbProveedor_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbProveedor.Validating
  If CmbProveedor.Text <> "" Then
   If (CmbProveedor.FindString(CmbProveedor.Text.ToString).ToString <> "-1") Then
    CmbProveedor.SelectedIndex = CmbProveedor.FindString(CmbProveedor.Text.ToString)
   Else
    CmbProveedor.SelectedIndex = -1
   End If
  End If
 End Sub

 Private Sub ChkUSD_CheckedChanged(sender As Object, e As EventArgs) Handles ChkUSD.CheckedChanged
  If RdBCompras.Checked = True Then
   VisualizarProv()
   'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
   'MFacturaBloq()
  Else
   VisualizarProv2()
   'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
   'MFacturaBloq()
  End If
 End Sub

 'FALTA AUTORIZA 
 'MODIFICO IVAN GONZALEZ
 'FALTA AGREGAR CHECKBOX
 Sub cargar_FacturasCerradas()

  Dim DTRefacciones As New DataTable

  ' crear nueva conexión    
  Dim conexion2 As New SqlConnection(StrCon)

  ' abrir la conexión con la base de datos   
  conexion2.Open()

  Dim Adaptador As New SqlDataAdapter()
  Dim comando As New SqlCommand

  Dim SQLTPD As String

  SQLTPD = "SELECT CASE WHEN T3.FactCompras IS NULL OR T3.FactCompras=0 THEN 0 ELSE 1 END AS RegSel, "
  SQLTPD &= "T0.DocDate AS FchDoc, "
  SQLTPD &= "T2.PymntGroup AS DiasCred,T0.DocNum AS Factura, CASE WHEN T0.FolioPref IS NULL THEN '' ELSE T0.FolioPref END "
  SQLTPD &= "+ CASE WHEN CAST(T0.U_Factura AS nvarchar(10)) IS NULL THEN '' ELSE CAST(T0.U_Factura AS nvarchar(10)) END AS FactProv,"
  SQLTPD &= "T1.CardCode AS IdProved,T1.CardName AS Proveedor, 'a' as 'TipoGasto',"
  SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal ELSE T0.DocTotalFC END AS TotFactura,T0.DocCur AS Moneda,"
  SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.PaidToDate ELSE T0.PaidFC END AS Pagado,"
  SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal - T0.PaidToDate ELSE T0.DocTotalFC - T0.PaidFC END AS SaldoPendiente,"
  SQLTPD &= "T0.DocCur AS Moneda2,DATEDIFF(DAY,T0.DocDueDate,GETDATE()) AS DiasAtraso,T0.DocDueDate AS FchVen,"
  SQLTPD &= "T0.DocTotal - T0.PaidToDate AS SaldoPesos,t0.NumAtCard Referencia,T1.Notes AS Obrserv,T4.Coment, T3.Fecha as 'FechaPagada' "
  SQLTPD &= "FROM [SBO_TPD].dbo.OPCH T0 "
  SQLTPD &= "INNER JOIN [SBO_TPD].dbo.OCRD T1 ON T0.CardCode = T1.CardCode AND (T1.CardCode LIKE '%P-%' OR T1.CardCode LIKE '%PIM-%') "
  SQLTPD &= "LEFT JOIN [SBO_TPD].dbo.OCTG T2 ON T2.GroupNum = T1.GroupNum "
  SQLTPD &= "LEFT JOIN [TPM].dbo.FCOMP T3 ON T0.DocNum = T3.FactCompras "
  SQLTPD &= "LEFT JOIN [TPM].dbo.COMP1 T4 ON T0.DocNum = T4.FactCompras "
  SQLTPD &= "WHERE T0.DocDueDate >= @FechaIni AND T0.DocDueDate <= @FechaTer "

  If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
   SQLTPD &= "AND T0.CardCode = @Agente "
  Else
   SQLTPD &= "AND T0.DocStatus = 'C' "
  End If
  SQLTPD &= " ORDER BY T0.DocDueDate, DiasAtraso, T0.DocNum DESC"

  ' Nuevo objeto Dataset   
  Dim DsVtasDet As New DataSet

  DsVtasDet.Tables.Add(DTRefacciones)

  With comando
   If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
    .Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
   End If
   .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
   .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
   ' Asignar el sql para seleccionar los datos de la tabla Maestro   
   .CommandText = SQLTPD
   .Connection = conexion2
  End With


  Dim DtFactProv As New DataTable

  With Adaptador
   .SelectCommand = comando
   ' llenar el dataset   
   .Fill(DtFactProv)
  End With

  DvFactProv = DtFactProv.DefaultView

  With Me.DgFactProv
   .DataSource = DvFactProv

   .AllowUserToAddRows = False

   'Pagado	
   '.Columns(0).HeaderText = "Pagado"

   'Fecha del documento	
   '.Columns(1).HeaderText = "fch doc"

   'Dias de credito	
   '.Columns(2).HeaderText = "credito"

   'Docto. SAP
   '.Columns(3).HeaderText = "Doc. SAP"


   'Factura
   '.Columns(4).HeaderText = "Factura"

   'Id Prov
   '.Columns(5).HeaderText = "ID prov"

   'Proveedor
   '.Columns(6).HeaderText = "Proveedor"

   '$Total Fact
   '.Columns(7).HeaderText = "$Total Factura"
   .Columns("TotFactura").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("TotFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   'Mnd
   '.Columns(8).HeaderText = "MND"
   .Columns("Moneda").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   'Importe Aplicado
   '.Columns(9).HeaderText = "Importe Aplicado"
   .Columns("Pagado").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("Pagado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   'Saldo Pendiente
   '.Columns(10).HeaderText = "$Saldo Pend"
   .Columns("SaldoPendiente").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("SaldoPendiente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   'Mnd
   '.Columns(11).HeaderText = "MND"
   .Columns("Moneda2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   'Dias Atraso
   '.Columns(12).HeaderText = "ID prov"
   .Columns("DiasAtraso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   'Fch Venc.
   '.Columns(13).HeaderText = "Fch venc."

   '$ Saldo MXP
   '.Columns(14).HeaderText = "$ Saldo MXP"
   .Columns("SaldoPesos").DefaultCellStyle.Format = "$ ###,###.#0"
   .Columns("SaldoPesos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

   .Columns("Referencia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

   'Observ
   '.Columns(15).HeaderText = "Observaciones"
   .Columns("Obrserv").Visible = False

  End With

  With conexion2
   If .State = ConnectionState.Open Then
    .Close()
   End If
   .Dispose()
  End With
  'MFacturaBloq()
 End Sub

End Class