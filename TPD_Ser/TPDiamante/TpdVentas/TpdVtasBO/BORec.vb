Imports System.Data.SqlClient

Public Class BORec
  Dim DvArticulo As New DataView
    Dim DvClte As New DataView
    Dim DvAgte As New DataView

    Private Sub BackOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    Dim FchInicio As DateTime
    FchInicio = DateAdd(DateInterval.Month, -1, Date.Now)
    Me.DtpAnioIni.Value = Format(Date.Now, "dd/MM/yyyy")
    Me.DtpAnioTer.Value = Format(Date.Now, "dd/MM/yyyy")

    Me.DtpMesIni.Value = Format(Date.Now, "dd/MM/yyyy")
    Me.DtpMesTer.Value = Format(Date.Now, "dd/MM/yyyy")


    'Me.DtpAnioTer.Value = Format(Date.Now, "dd/MM/yyyy")

    Dim ConsutaLista As String


    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

      'ICR_24042015 Se agrega el metodo mllenaComboAlmacen 
      mllenaComboAlmacen(SqlConnection)

      ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
      Dim daGArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

      Dim DSetTablas As New DataSet
      daGArticulo.Fill(DSetTablas, "GArticulos")

      Dim fila As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      fila = DSetTablas.Tables("GArticulos").NewRow

      'Agregamos los valores a los campos de la tabla
      fila("ItmsGrpNam") = "TODOS"
      fila("ItmsGrpCod") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("GArticulos").Rows.Add(fila)

      Me.CmbGrupoArticulo.DataSource = DSetTablas.Tables("GArticulos")
      Me.CmbGrupoArticulo.DisplayMember = "ItmsGrpNam"
      Me.CmbGrupoArticulo.ValueMember = "ItmsGrpCod"
      Me.CmbGrupoArticulo.SelectedValue = 999

      'De acuerdo a la solicitud del Lic. Salvador quitar a los siguientes Agentes a pesar de que su status es activo
      ConsutaLista = "SELECT OSLP.slpcode,OSLP.slpname FROM OSLP WHERE U_ESTATUS = 'ACTIVO' " +
                     " AND OSLP.slpcode NOT IN (17,56)" +  'Ricardo Robles, Rolando, Victor (No se detallo cual), Esther.
                     " ORDER BY slpname"

      Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


      daAgte.Fill(DSetTablas, "Agentes")

      Dim filaAgte As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      filaAgte = DSetTablas.Tables("Agentes").NewRow

      'Agregamos los valores a los campos de la tabla
      filaAgte("slpname") = "TODOS"
      filaAgte("slpcode") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

      Me.CmbAgteVta.DataSource = DSetTablas.Tables("Agentes")
      Me.CmbAgteVta.DisplayMember = "slpname"
      Me.CmbAgteVta.ValueMember = "slpcode"
            Me.CmbAgteVta.SelectedValue = 999

            ' DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

            DvAgte.Table = DSetTablas.Tables("Agentes")
            Me.CmbAgteVta.DataSource = DSetTablas.Tables("Agentes")
            Me.CmbAgteVta.DisplayMember = "slpname"
            Me.CmbAgteVta.ValueMember = "slpcode"

            If UsrTPM = "ACASTRO" Or UsrTPM = "JSANCHEZ" Or UsrTPM = "RMERCADO" Or UsrTPM = "ABAJIO" Or UsrTPM = "ATUXTLA" _
                Or UsrTPM = "VENTAS9" Or UsrTPM = "VENTAS5" Or UsrTPM = "AVERACRUZ" _
            Or UsrTPM = "VENTAS4" Or UsrTPM = "RJIMENEZ" Or UsrTPM = "AMERIDA" Or UsrTPM = "LCEBALLOS" Or UsrTPM = "ATABASCO" Then
                'MsgBox(UsrTPM)
                'MsgBox(vCodAgte)
                Me.CmbAgteVta.SelectedValue = vCodAgte
                Me.CmbAgteVta.Enabled = False
                Me.cmbAlmacen.Enabled = False
                BuscaClientes()
                Me.CmbCliente.Focus()
                'Aqui se hizo el cambio
            Else
                Me.CmbAgteVta.SelectedValue = 999

            End If

            If UsrTPM = "MANAGER" Then
                ConsutaLista = "SELECT CardCode,CardName, SlpCode, GroupCode FROM OCRD WHERE CardType = 'C' ORDER BY CardName "
            ElseIf UsrTPM = "VENTAS8" Then
                ConsutaLista = "SELECT CardCode,CardName, SlpCode, GroupCode FROM OCRD WHERE CardType = 'C' AND SlpCode IN (52,57,8) ORDER BY CardName "

            Else

                ConsutaLista = "SELECT CardCode,CardName, SlpCode, GroupCode FROM OCRD WHERE CardType = 'C' AND SlpCode = '" & vCodAgte.ToString & "' ORDER BY CardName "
            End If

            'ConsutaLista = "SELECT CardCode,CardName, SlpCode FROM OCRD WHERE CardType = 'C' ORDER BY SlpCode,CardName"
            Dim daClientes As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

      daClientes.Fill(DSetTablas, "Clientes")

      Dim filaClientes As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      filaClientes = DSetTablas.Tables("Clientes").NewRow

      'Agregamos los valores a los campos de la tabla
      filaClientes("CardName") = "TODOS"
      filaClientes("CardCode") = "TODOS"
      filaClientes("slpcode") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Clientes").Rows.Add(filaClientes)

      DvClte.Table = DSetTablas.Tables("Clientes")

      Me.CmbCliente.DataSource = DvClte
      Me.CmbCliente.DisplayMember = "CardName"
      Me.CmbCliente.ValueMember = "CardCode"
      Me.CmbCliente.SelectedValue = "TODOS"


      '-----------------------------------------------------
      ConsutaLista = "SELECT ItemCode,ItemName,ItmsGrpCod FROM OITM ORDER BY ItemCode"
      Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


      daArticulo.Fill(DSetTablas, "Articulos")

      Dim filaArticulo As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      filaArticulo = DSetTablas.Tables("Articulos").NewRow

      'Agregamos los valores a los campos de la tabla
      filaArticulo("ItemName") = "TODOS"
      filaArticulo("ItemCode") = "TODOS"
      filaArticulo("ItmsGrpCod") = 999

      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
      DSetTablas.Tables("Articulos").Rows.Add(filaArticulo)

      DvArticulo.Table = DSetTablas.Tables("Articulos")

      Me.CmbArticulo.DataSource = DvArticulo
      Me.CmbArticulo.DisplayMember = "ItemCode"
      Me.CmbArticulo.ValueMember = "ItemCode"
      Me.CmbArticulo.SelectedValue = "TODOS"

    End Using

    'ICR_24042015 SE agrega el metodo 
    mllenaComboVtaAgente()

    'If VEsAgente = 1 Then
    '    Me.CmbAgteVta.SelectedValue = vCodAgte
    '    Me.CmbAgteVta.Enabled = False
    '    BuscaClientes()
    '    Me.CmbCliente.Focus()
    'End If

    If UsrTPM = "RROBLES" Or UsrTPM = "VVERGARA" _
        Or UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" _
        Or UsrTPM = "VENTAS5" Or UsrTPM = "ASTRIDY" Or UsrTPM = "CSANTOS" _
        Or UsrTPM = "VENTAS8" Then
      'Me.cmbAlmacen.Enabled = False
      Me.cmbAgteBo.Enabled = False

    ElseIf UsrTPM = "ACASTRO" Or UsrTPM = "JSANCHEZ" Or UsrTPM = "RMERCADO" _
        Or UsrTPM = "VENTAS4" Or UsrTPM = "RJIMENEZ" Then

      Me.CmbAgteVta.SelectedValue = vCodAgte
      Me.CmbAgteVta.Enabled = False
      Me.cmbAlmacen.Enabled = False
      BuscaClientes()
      Me.CmbCliente.Focus()
    End If

  End Sub

  Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


    If Me.DtpAnioIni.Value > Me.DtpAnioTer.Value Then
      MessageBox.Show("La fecha de inicio no pede ser mayor a la de termino", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      DtpAnioIni.Focus()
      Return
    End If


    If IsNothing(CmbAgteVta.SelectedValue) Then
      MessageBox.Show("Seleccione un agente de ventas",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbAgteVta.Focus()
      Return
    End If

    If IsNothing(CmbCliente.SelectedValue) Then
      MessageBox.Show("Seleccione un cliente",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbCliente.Focus()
      Return
    End If

    If IsNothing(CmbGrupoArticulo.SelectedValue) Then
      MessageBox.Show("Seleccione una línea",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbGrupoArticulo.Focus()
      Return
    End If

    If IsNothing(CmbArticulo.SelectedValue) Then
      MessageBox.Show("Seleccione un articulo",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbArticulo.Focus()
      Return
    End If

    Ejecutar_Consulta()
  End Sub

  Private Sub BtnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExcel.Click
    Dim oExcel As Object
    Dim oBook As Object
    Dim oSheet As Object

    'Abrimos un nuevo libro
    oExcel = CreateObject("Excel.Application")
    oBook = oExcel.workbooks.add
    oSheet = oBook.worksheets(1)

    'Declaramos el nombre de las columnas
    oSheet.range("A6").value = "Clave"
    oSheet.range("B6").value = "Nombre Cliente"
    oSheet.range("C6").value = "Agente"
    oSheet.range("D6").value = "Ord.Vta."
    oSheet.range("E6").value = "Fecha Ord. Vta"
    oSheet.range("F6").value = "Factura Back Order"
    oSheet.range("G6").value = "Fecha Factura BO"
    oSheet.range("H6").value = "Articulo"
    oSheet.range("I6").value = "Descripción"
    oSheet.range("J6").value = "Línea"
    oSheet.range("K6").value = "Pedido Cliente"
    oSheet.range("L6").value = "Facturado"
    oSheet.range("M6").value = "BO Recuperado"
    oSheet.range("N6").value = "$ Precio"
    oSheet.range("O6").value = "$ Total BO"
    oSheet.range("P6").value = "Mes"
    oSheet.range("Q6").value = "Año"
    oSheet.range("R6").value = "Fecha Actulización"
    oSheet.range("S6").value = "Usuario"
    oSheet.range("T6").value = "Almacen"
    oSheet.range("U6").value = "Observaciones"


    'para poner la primera fila de los titulos en negrita
    oSheet.range("A6:S6").font.bold = True
    Dim fila_dt As Integer = 0
    Dim fila_dt_excel As Integer = 0
    Dim tanto_porcentaje As String = ""
    Dim marikona As Integer = 0

    Dim total_reg As Integer = 0

    total_reg = Me.GrdBoRec.RowCount
    For fila_dt = 0 To total_reg - 1

      'para leer una celda en concreto
      'el numero es la columna
      Dim cel1 As String = Me.GrdBoRec.Item(0, fila_dt).Value
      Dim cel2 As String = Me.GrdBoRec.Item(1, fila_dt).Value
      Dim cel3 As String = Trim(IIf(IsDBNull(Me.GrdBoRec.Item(2, fila_dt).Value), 0, Me.GrdBoRec.Item(2, fila_dt).Value))
      Dim cel4 As String = IIf(IsDBNull(Me.GrdBoRec.Item(3, fila_dt).Value), 0, Me.GrdBoRec.Item(3, fila_dt).Value)
      Dim cel5 As Date = Trim(IIf(IsDBNull(Me.GrdBoRec.Item(4, fila_dt).Value), "12/12/1999", Me.GrdBoRec.Item(4, fila_dt).Value))
      Dim cel6 As String = IIf(IsDBNull(Me.GrdBoRec.Item(5, fila_dt).Value), 0, Me.GrdBoRec.Item(5, fila_dt).Value)
      Dim cel7 As Date = IIf(IsDBNull(Me.GrdBoRec.Item(6, fila_dt).Value), "12/12/1999", Me.GrdBoRec.Item(6, fila_dt).Value)
      Dim cel8 As String = IIf(IsDBNull(Me.GrdBoRec.Item(7, fila_dt).Value), 0, Me.GrdBoRec.Item(7, fila_dt).Value)

      Dim cel9 As String = IIf(IsDBNull(Me.GrdBoRec.Item(8, fila_dt).Value), 0, Me.GrdBoRec.Item(8, fila_dt).Value)
      Dim cel10 As String = IIf(IsDBNull(Me.GrdBoRec.Item(9, fila_dt).Value), 0, Me.GrdBoRec.Item(9, fila_dt).Value)

      Dim cel11 As String = IIf(IsDBNull(Me.GrdBoRec.Item(10, fila_dt).Value), 0, Me.GrdBoRec.Item(10, fila_dt).Value)
      Dim cel12 As String = IIf(IsDBNull(Me.GrdBoRec.Item(11, fila_dt).Value), 0, Me.GrdBoRec.Item(11, fila_dt).Value)
      Dim cel13 As String = IIf(IsDBNull(Me.GrdBoRec.Item(12, fila_dt).Value), 0, Me.GrdBoRec.Item(12, fila_dt).Value)
      Dim cel14 As String = IIf(IsDBNull(Me.GrdBoRec.Item(13, fila_dt).Value), 0, Me.GrdBoRec.Item(13, fila_dt).Value)
      Dim cel15 As String = IIf(IsDBNull(Me.GrdBoRec.Item(14, fila_dt).Value), 0, Me.GrdBoRec.Item(14, fila_dt).Value)
      Dim cel16 As String = IIf(IsDBNull(Me.GrdBoRec.Item(17, fila_dt).Value), 0, Me.GrdBoRec.Item(17, fila_dt).Value)
      Dim cel17 As String = IIf(IsDBNull(Me.GrdBoRec.Item(18, fila_dt).Value), 0, Me.GrdBoRec.Item(18, fila_dt).Value)
      Dim cel18 As Date = IIf(IsDBNull(Me.GrdBoRec.Item(19, fila_dt).Value), "12/12/1999", Me.GrdBoRec.Item(19, fila_dt).Value)
      Dim cel19 As String = IIf(IsDBNull(Me.GrdBoRec.Item(20, fila_dt).Value), 0, Me.GrdBoRec.Item(20, fila_dt).Value)
      Dim cel20 As String = IIf(IsDBNull(Me.GrdBoRec.Item(21, fila_dt).Value), 0, Me.GrdBoRec.Item(21, fila_dt).Value)
      Dim cel22 As String = IIf(IsDBNull(Me.GrdBoRec.Item(22, fila_dt).Value), 0, Me.GrdBoRec.Item(22, fila_dt).Value)

      fila_dt_excel = fila_dt + 7 'Renglón en donde se empieza a registrar el reporte

      'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
      oSheet.range("A" & fila_dt_excel).value = cel1
      oSheet.range("B" & fila_dt_excel).value = cel2
      oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
      oSheet.range("D" & fila_dt_excel).value = cel4
      oSheet.range("E" & fila_dt_excel).value = cel5
      oSheet.range("F" & fila_dt_excel).value = cel6
      oSheet.range("G" & fila_dt_excel).value = cel7
      oSheet.range("H" & fila_dt_excel).value = cel8

      oSheet.range("I" & fila_dt_excel).value = cel9
      oSheet.range("J" & fila_dt_excel).value = cel10
      oSheet.range("K" & fila_dt_excel).value = FormatNumber(cel11, 0)
      oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel12, 0)
      oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel13, 2)
      oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel14, 2)
      oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel15, 2)
      oSheet.range("P" & fila_dt_excel).value = cel16
      oSheet.range("Q" & fila_dt_excel).value = cel17
      oSheet.range("R" & fila_dt_excel).value = cel18
      oSheet.range("S" & fila_dt_excel).value = cel19
      oSheet.range("T" & fila_dt_excel).value = cel20
      oSheet.range("U" & fila_dt_excel).value = cel22

    Next

    ' para que el tamano de la columna tenga como minimo el maximo de sus textos
    oSheet.columns("A:O").entirecolumn.autofit()
    oSheet.range("A1").value = "Reporte de Back Orders Recuperado con periodo " & UCase(Format(Me.DtpMesIni.Value, " MMM ")) & " " & Me.DtpAnioIni.Value.Year.ToString & " Al " & UCase(Format(Me.DtpMesTer.Value, " MMM ")) & " " & Me.DtpAnioTer.Value.Year.ToString

    '// Display the date as "Mon 26 Feb 2001".
    'dateTimePicker1.set_CustomFormat("ddd dd MMM yyyy");


    oSheet.range("A2").value = "Agente de Ventas - " + CmbAgteVta.Text
    oSheet.range("A3").value = "Cliente - " + CmbCliente.Text
    oSheet.range("A4").value = "Línea - " + CmbGrupoArticulo.Text
    oSheet.range("A5").value = "Línea - " + CmbArticulo.Text

    oSheet.Columns("R:R").ColumnWidth = 18
    oSheet.Columns("F:F").ColumnWidth = 18

    oExcel.visible = True
    System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
    GC.Collect()
    oSheet = Nothing
    oBook = Nothing
    oExcel = Nothing

  End Sub

  Private Sub GrdConProd_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles GrdBoRec.RowPrePaint

    'If Trim(GrdVTpm.Rows(e.RowIndex).Cells("T_Critico").Value) <> "" Then
    '    GrdVTpm.Rows(e.RowIndex).Cells("T_Critico").Style.BackColor = Color.FromArgb(192, 0, 20)
    'Else
    '    GrdVTpm.Rows(e.RowIndex).Cells("T_Critico").Style.BackColor = Color.MediumSeaGreen
    'End If
    ''If GrdConProd.Rows(e.RowIndex).Cells("Ord_Vta").Value = 0 Then
    ''    GrdConProd.Rows(e.RowIndex).Cells("OrdCompra").Style.ForeColor = Color.White ' 
    ''    GrdConProd.Rows(e.RowIndex).Cells("Fecha").Style.ForeColor = Color.Transparent
    ''    GrdConProd.Rows(e.RowIndex).Cells("FchEntrega").Style.ForeColor = Color.White '
    ''    GrdConProd.Rows(e.RowIndex).Cells("Ord_Vta").Style.ForeColor = Color.White '
    ''End If

    ''GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Style.BackColor = Color.Black
    ''GrdConProd.Rows(e.RowIndex).Cells("BackOrder").Style.ForeColor = Color.White
  End Sub

  Private Sub CmbGrupoArticulo_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbGrupoArticulo.SelectionChangeCommitted
    BuscaArticulos()
  End Sub

  Private Sub CmbGrupoArticulo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CmbGrupoArticulo.Validating
    BuscaArticulos()
  End Sub

  Private Sub CmbGrupoArticulo_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbGrupoArticulo.KeyUp
    BuscaArticulos()
  End Sub

  Private Sub CmbAgteVta_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbAgteVta.SelectionChangeCommitted
    BuscaClientes()
  End Sub

  Private Sub CmbAgteVta_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbAgteVta.KeyUp
    BuscaClientes()
  End Sub

  Private Sub CmbAgteVta_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CmbAgteVta.Validating
    BuscaClientes()
  End Sub

  Private Sub mllenaComboAlmacen(ByVal conexion As SqlConnection)
    Try
      Dim da As New SqlDataAdapter("Select WhsCode,WhsName " +
                                          " from owhs " +
                                          " where WhsCode in (01,03,07) ", conexion)

      Dim ds As New DataSet
      da.Fill(ds)
      ds.Tables(0).Rows.Add(0, "TODOS")
      Me.cmbAlmacen.DataSource = ds.Tables(0)
      Me.cmbAlmacen.DisplayMember = "WhsName"
      Me.cmbAlmacen.ValueMember = "WhsCode"

      If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Or UsrTPM = "DDORANTES" Or UsrTPM = "COMERCIAL" Then

        Me.cmbAlmacen.SelectedValue = 0

      Else

        Me.cmbAlmacen.SelectedValue = AlmTPM.ToString()

      End If



    Catch ex As Exception

    End Try

  End Sub

  Private Sub mllenaComboVtaAgente()
    Try
      Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)

        Dim sCadena As String
        Select Case UsrTPM
          Case "MANAGER"
            sCadena = "select Id_Usuario,UPPER(Nombre) Nombre from Usuarios " +
                                            " where boAutorisa= 1 "

          Case "DDORANTES"
            sCadena = "select Id_Usuario,UPPER(Nombre) Nombre from Usuarios " +
                                            " where boAutorisa= 1 "

          Case "AINVEN"
            sCadena = "select Id_Usuario,UPPER(Nombre) Nombre from Usuarios " +
                                            " where boAutorisa= 1 "
          Case "COMERCIAL"
            sCadena = "select Id_Usuario,UPPER(Nombre) Nombre from Usuarios " +
                                            " where boAutorisa= 1 "

          Case Else
            sCadena = "select Id_Usuario,UPPER(Nombre) Nombre from Usuarios " +
                                            " where Id_Usuario= '" + UsrTPM + "' " +
                                            " and boAutorisa= 1 "
        End Select

        Dim da As New SqlDataAdapter(sCadena, SqlConnection)

        Dim ds As New DataSet
        da.Fill(ds)

        If UsrTPM = "MANAGER" Or UsrTPM = "AINVEN" Or UsrTPM = "PRUEBAS" Or UsrTPM = "DDORANTES" Or UsrTPM = "COMERCIAL" Then
          ds.Tables(0).Rows.Add(0, "TODOS")
        End If

        Me.cmbAgteBo.DataSource = ds.Tables(0)
        Me.cmbAgteBo.DisplayMember = "Nombre"
        Me.cmbAgteBo.ValueMember = "Id_Usuario"

        If UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS5" Or UsrTPM = "ASTRIDY" _
                Or UsrTPM = "VENTAS8" Then
          Me.cmbAgteBo.SelectedValue = UsrTPM

        Else
          Me.cmbAgteBo.SelectedValue = UsrTPM
        End If

        'Me.cmbAgteBo.SelectedValue = IIf(UsrTPM = "MANAGER" Or UsrTPM = "AINVEN" Or UsrTPM = "PRUEBAS", 0, UsrTPM)
        'Me.cmbAgteBo.SelectedValue = IIf(UsrTPM = "AINVEN", 0, UsrTPM)

      End Using
      ''cmbAgteBo.Enabled = False

    Catch ex As Exception

    End Try
  End Sub

  Sub BuscaArticulos()
    If CmbGrupoArticulo.SelectedValue Is Nothing Or CmbGrupoArticulo.SelectedValue = 999 Then
      DvArticulo.RowFilter = String.Empty
      CmbArticulo.SelectedValue = "TODOS"

    Else
      DvArticulo.RowFilter = "ItmsGrpCod = " & Trim(Me.CmbGrupoArticulo.SelectedValue.ToString) & " OR ItmsGrpCod = 999"
    End If
  End Sub

  Sub BuscaClientes()
    If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.SelectedValue = 999 Then
      DvClte.RowFilter = String.Empty
      CmbCliente.SelectedValue = "TODOS"
    Else
      DvClte.RowFilter = "SlpCode = " & Trim(Me.CmbAgteVta.SelectedValue.ToString) & " OR SlpCode = 999"
    End If
  End Sub

  Private Sub Ejecutar_Consulta()
    Try
      Dim Consulta As String = ""
      Dim Consulta2 As String = ""
      Dim CTabla As String = ""
      Dim DTMObra As New DataTable
      Dim DTProb As New DataTable

            'ICR_27042015 Se agrego el nombre del agene de ventas y el almacen 
            '--CONSULTA EL BACK ORDER RECUPERADO
            Consulta = " SELECT CodClte,Cliente,Agente,OrdVta,FchOrdVta,FacBO,FchFactBO,Articulo,Descripcion,Linea,PedClte,Facturado,CantBO,Precio,TotalBO,IdLinea,IdAgente,Mes,Anio,FchAct," +
                        " UPPER(usr.Nombre),alm.WhsName Almacen, Observaciones" +
                        " FROM BOrder bo " +
                        "  inner join SBO_TPD.dbo.OINV on SBO_TPD.dbo.OINV.Docnum=bo.FacBO " +
                  "   inner join Usuarios usr on usr.Id_Usuario=bo.VtasUsuario " +
                        "   inner join SBO_TPD.dbo.owhs alm on bo.Almacen=alm.WhsCode COLLATE Modern_Spanish_CI_AI " +
                        " WHERE SBO_TPD.dbo.OINV.InvntSttus <> 'C' AND   Mes >= " & Me.DtpMesIni.Value.Month.ToString &
                        " AND Mes <=" & Me.DtpMesTer.Value.Month &
                        " AND Anio >=" & Me.DtpAnioIni.Value.Year.ToString &
                        " AND Anio <=" & Me.DtpAnioTer.Value.Year.ToString
            If cmbAgteBo.SelectedValue <> Nothing Then
        Consulta = Consulta + " and VtasUsuario ='" + cmbAgteBo.SelectedValue.ToString() + "'"
      End If
      If cmbAlmacen.SelectedValue.ToString() <> "0" Then
        Consulta = Consulta + " and bo.Almacen ='" + cmbAlmacen.SelectedValue + "'"
      End If

      Consulta2 = "  UNION ALL "
      Consulta2 &= " SELECT '' AS CodClte,'*** MONTO TOTAL ' AS Cliente,'' AS Agente,null AS OrdVta,null AS FchOrdVta,null AS FacBO,null AS FchFactBO, "
      Consulta2 &= "'' AS Articulo,'' AS Descripcion,'' AS Linea,null AS PedClte,null AS Facturado,null AS CantBO,null AS Precio,"
      Consulta2 &= " SUM(TotalBO) AS TotalBO,null AS IdLinea,null AS IdAgente,null AS Mes,null AS Anio,null AS FchAct, null Usuario,null, null"
      Consulta2 &= " FROM BOrder Bo "
            Consulta2 &= "  inner join SBO_TPD.dbo.OINV on SBO_TPD.dbo.OINV.Docnum=bo.FacBO "
            Consulta2 &= "   inner join Usuarios usr on usr.Id_Usuario=bo.VtasUsuario "
            Consulta2 &= "   inner join SBO_TPD.dbo.owhs alm on bo.Almacen=alm.WhsCode COLLATE Modern_Spanish_CI_AI "
            Consulta2 &= "WHERE SBO_TPD.dbo.OINV.InvntSttus <> 'C' and Mes >= " & Me.DtpMesIni.Value.Month.ToString &
            " AND Mes <=" & Me.DtpMesTer.Value.Month &
            " AND Anio >=" & Me.DtpAnioIni.Value.Year.ToString &
            " AND Anio <=" & Me.DtpAnioTer.Value.Year.ToString

            If cmbAgteBo.SelectedValue <> Nothing Then
        Consulta2 = Consulta2 + " and VtasUsuario ='" + cmbAgteBo.SelectedValue.ToString() + "' "
      End If

      If cmbAlmacen.SelectedValue.ToString() <> "0" Then
        Consulta2 = Consulta2 + " and bo.Almacen ='" + cmbAlmacen.SelectedValue + "' "
      End If


      If CmbAgteVta.SelectedValue <> 999 Then
        Consulta &= " AND IdAgente = @IdAgente"
        Consulta2 &= " AND IdAgente = @IdAgente"
      End If

      If CmbCliente.SelectedValue <> "TODOS" Then
        Consulta &= " AND CodClte = @IdCliente"
        Consulta2 &= " AND CodClte = @IdCliente"
      End If

      If CmbGrupoArticulo.SelectedValue <> 999 Then
        Consulta &= " AND IdLinea = @GrupoArt"
        Consulta2 &= " AND IdLinea = @GrupoArt"
      End If

      If CmbArticulo.SelectedValue <> "TODOS" Then
        Consulta &= " AND Articulo = @IdArticulo"
        Consulta2 &= " AND Articulo = @IdArticulo"
      End If

      Consulta &= Consulta2


      Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

      If CmbAgteVta.SelectedValue <> 999 Then
        CmdMObra.Parameters.Add("@IdAgente", SqlDbType.Int)
        CmdMObra.Parameters("@IdAgente").Value = CmbAgteVta.SelectedValue
      End If

      If CmbCliente.SelectedValue <> "TODOS" Then
        CmdMObra.Parameters.Add("@IdCliente", SqlDbType.Char)
        CmdMObra.Parameters("@IdCliente").Value = CmbCliente.SelectedValue
      End If

      If CmbGrupoArticulo.SelectedValue <> 999 Then
        CmdMObra.Parameters.Add("@GrupoArt", SqlDbType.Int)
        CmdMObra.Parameters("@GrupoArt").Value = CmbGrupoArticulo.SelectedValue
      End If


      If CmbArticulo.SelectedValue <> "TODOS" Then
        CmdMObra.Parameters.Add("@IdArticulo", SqlDbType.Char)
        CmdMObra.Parameters("@IdArticulo").Value = CmbArticulo.SelectedValue
      End If

      CmdMObra.Connection = New SqlClient.SqlConnection(StrTpm)
      CmdMObra.Connection.Open()

      Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
      AdapMObra.Fill(DTMObra)
      CmdMObra.Connection.Close()



      With Me.GrdBoRec
        .DataSource = DTMObra
        .ReadOnly = True
        'Color de Renglones en Grid
        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
        .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        .DefaultCellStyle.BackColor = Color.AliceBlue
        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
        'Propiedad para no mostrar el cuadro que se encuentra en la parte
        'Superior Izquierda del gridview
        .RowHeadersVisible = False
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        .MultiSelect = True
        .AllowUserToAddRows = False

        'Color de linea del grid
        .Columns(0).HeaderText = "Clave Cliente"
        .Columns(0).Width = 45
        .Columns(1).HeaderText = "Nombre Cliente"
        .Columns(1).Width = 160

        If VEsAgente = 1 Then
          .Columns(2).Visible = False
        Else
          .Columns(2).HeaderText = "Agente"
          .Columns(2).Width = 110
        End If

        .Columns(3).HeaderText = "Ord. de Vta"
        .Columns(3).Width = 40

        .Columns(4).HeaderText = "Fecha Ord. Vta"
        .Columns(4).Width = 65


        .Columns(5).HeaderText = "Factura Back Order"
        .Columns(5).Width = 55

        .Columns(6).HeaderText = "Fecha Factura BO"
        .Columns(6).Width = 65

        .Columns(7).HeaderText = "Articulo"
        .Columns(7).Width = 90
        .Columns(7).DefaultCellStyle.Format = "###.00"

        .Columns(8).HeaderText = "Descripción"
        .Columns(8).Width = 150

        .Columns(9).HeaderText = "Línea"
        .Columns(9).Width = 80

        .Columns(10).HeaderText = "Pedido Cliente"
        .Columns(10).Width = 45
        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(10).DefaultCellStyle.Format = "###,###,###"


        .Columns(11).HeaderText = "Factu- rado"
        .Columns(11).Width = 45
        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(11).DefaultCellStyle.Format = "###,###,###"

        .Columns(12).HeaderText = "Back Order Recuperado"
        .Columns(12).Width = 70
        .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(12).DefaultCellStyle.Format = "###,###,###"

        .Columns(13).HeaderText = "$ Precio"
        .Columns(13).Width = 60
        .Columns(13).DefaultCellStyle.Format = "###,###,###.##"
        .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(14).HeaderText = "$ Total BO Recuperado"
        .Columns(14).Width = 70
        .Columns(14).DefaultCellStyle.Format = "###,###,###.##"
        .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        .Columns(15).Visible = False
        .Columns(16).Visible = False


        .Columns(17).HeaderText = "Mes"
        .Columns(17).Width = 30
        .Columns(17).DefaultCellStyle.Format = "###,###,###.##"
        '            .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        .Columns(18).HeaderText = "Año"
        .Columns(18).Width = 40

        .Columns(19).HeaderText = "Fecha Actulización"
        .Columns(19).Width = 70

        .Columns(20).HeaderText = "Usuario"
        .Columns(20).Width = 100

        .Columns(22).HeaderText = "Observaciones"
        .Columns(22).Width = 100



      End With
    Catch ex As Exception
      MessageBox.Show("ERROR EN CONSULTA SQL: " + ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try

  End Sub

End Class