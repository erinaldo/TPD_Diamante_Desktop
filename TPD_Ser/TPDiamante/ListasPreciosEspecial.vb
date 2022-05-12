Imports CrystalDecisions.Shared
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Text
Imports System.Net
Imports System.Data.SqlClient

Public Class ListasPreciosEspecial
  Private dv As New DataView
  Dim NumOVta As Long
  Private AgregaArt As Integer = 0
  Dim VErrOv As Integer = 0
  Dim VErrCAd As Integer = 0
  Dim VErrClte As Integer = 0
  Dim DvClte As New DataView
  Dim oStrem As New System.IO.MemoryStream
  Dim strTemp As String
  Dim cnn As SqlConnection = Nothing
  Dim cmd4 As SqlCommand = Nothing
  Dim vNombreGeneral As String
  Dim NumLetra As String = ""

  Dim SQL As New Comandos_SQL()

  Private Sub DisenoGridVCap()
    With Me.DGVCap

      'Color de Renglones en Grid
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      '.RowHeadersVisible = False
      .AllowUserToAddRows = False

      .Columns(0).HeaderText = "Cantidad"
      .Columns(0).Width = 52
      .Columns(0).DefaultCellStyle.Format = "###,###,###"
      .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      .Columns(0).ReadOnly = False

      .Columns(1).HeaderText = "Artículo"
      .Columns(1).Width = 108

      .Columns(2).HeaderText = "Descripción"
      .Columns(2).Width = 110

      .Columns(3).HeaderText = "$ Precio"
      .Columns(3).Width = 65
      .Columns(3).DefaultCellStyle.Format = "###,###,###.000"
      .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(4).HeaderText = "Desc. Prom."
      .Columns(4).Width = 45
      .Columns(4).DefaultCellStyle.Format = "###,###,###.000"
      .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(5).HeaderText = "$ Importe"
      .Columns(5).Width = 70
      .Columns(5).DefaultCellStyle.Format = "###,###,###.00"
      .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

      .Columns(6).HeaderText = "Línea"
      .Columns(6).Width = 70
      .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(6).Visible = False

      'SE AGREGA PARA LA MONEDA DEL ARTICULO: URIEL TORALVA 19/05/2018
      .Columns(7).HeaderText = "MND"
      .Columns(7).Width = 35
      .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns(7).Visible = False
      'ANTERIOR: URIEL TORALVA 19/05/2018
      '.Columns(7).HeaderText = "Línea"
      '.Columns(7).Width = 70
      '.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      '.Columns(7).Visible = False

    End With
  End Sub

  Private Sub DisenoGridVArt()
    With Me.DGVArt
      .DataSource = dv
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
      .Columns(0).HeaderText = "Artículo"
      .Columns(0).Width = 108
      .Columns(0).DefaultCellStyle.Format = "###.00"

      .Columns(1).HeaderText = "Descripción"
      .Columns(1).Width = 228

      .Columns(2).HeaderText = "$ Precio"
      .Columns(2).Width = 65
      .Columns(2).DefaultCellStyle.Format = "###,###,###.000"
      .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
      '.Columns(2).Visible = False

      .Columns(3).HeaderText = "Línea"
      .Columns(3).Width = 70

      .Columns(4).Visible = False
      .Columns(5).Visible = False
      .Columns(6).Visible = False

      'SE AGREGA PARA LA MONEDA DEL ARTICULO: URIEL TORALVA 19/05/2018
      .Columns(7).HeaderText = "MND"
      .Columns(7).Width = 35
      'ANTES 19/05/2018
      .Columns(7).Visible = False

      .Columns(8).HeaderText = "ListaPrecos"
      .Columns(8).Visible = False

    End With

  End Sub


  Private Sub CapOrdVta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    DisenoGridVCap()

    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

      'SOLO SE CREARAN 2 CLIENTES (Perfiles) ESPECIFICOS
      'Precios de compra y Precios de venta
      'Precios de compra. Traera los articulos de precio de lista lista 5 a excepcion de 2 grupos de articulos 
      'Linea BENDIX Este quedara en lista 7
      'Linea NATIONAL Este quedara en lista 6

      Dim ConsutaLista As String

      'ConsutaLista = "SELECT T0.CardCode AS Cliente,T0.CardName AS Nombre,T0.Phone1 AS Telefono,T0.CntctPrsn AS Contacto,T0.Address AS Direccion,T0.BLOCK AS Colonia,"
      'ConsutaLista &= "RTRIM(LTRIM(T0.CardCode)) + ' --> ' + RTRIM(LTRIM(T0.CardName)) + ' - ' + RTRIM(LTRIM(CASE WHEN T0.City IS NULL THEN '' ELSE T0.City END)) AS Nombre2,"
      'ConsutaLista &= "T0.ZipCode AS CP,T0.City AS Ciudad,T0.State1 AS Estado,T0.Country As Pais,"
      'ConsutaLista &= "T1.SlpCode AS IdAgente,T1.SlpName AS Agente,T0.LicTradNum AS Rfc,T0.E_Mail as Mail "
      'ConsutaLista &= "FROM OCRD T0 INNER JOIN OSLP T1 ON T0.SlpCode = T1.SlpCode "
      'ConsutaLista &= "WHERE T0.CardType = 'C' AND T0.SlpCode = '" & vCodAgte.ToString
      'ConsutaLista &= "' ORDER BY  T0.CardCode; --CAST(SUBSTRING(T0.CardCode, CHARINDEX('-', T0.CardCode) + 1, LEN(T0.CardCode)) as integer) ASC  "

      'Dim daCliente As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

      'Dim dsCte As New DataSet
      'daCliente.Fill(dsCte, "Articulos")

      'DvClte.Table = dsCte.Tables("Articulos")


      CmbPerfil.Items.Clear()
      CmbPerfil.Items.Add("Precios de compra")
      CmbPerfil.Items.Add("Precios de venta")
      CmbPerfil.SelectedIndex = 0

      ''CONSULTA PARA OBTENER LAS LINEAS
      ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam "
      Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

      Dim dsLin As New DataSet
      daArticulo.Fill(dsLin)

      Me.CmbLinea.DataSource = dsLin.Tables(0)
      Me.CmbLinea.DisplayMember = "ItmsGrpNam"
      Me.CmbLinea.ValueMember = "ItmsGrpCod"
      Me.CmbLinea.SelectedValue = 0
    End Using
    '/*********************************************************************************************************************************/

    CargaListaPrecios()

    'OBTENCION DE TIPO DE CAMBIO
    Dim sqlConnectionDolar As New SqlConnection(conexion_universal.CadenaSQLSAP)
    Dim cmmd As New SqlCommand
    'CONSULTA DEL VALOR DEL DOLAR
    cmmd.CommandText = "select Currency, Rate as Dolar FROM ORTT WHERE RateDate = CONVERT (date, GETDATE()) "
    cmmd.CommandType = CommandType.Text
    cmmd.Connection = sqlConnectionDolar
    'ABRE LA CONEXION
    sqlConnectionDolar.Open()
    'EJECUTA LA CONSULTA
    Dim reader As SqlDataReader = cmmd.ExecuteReader()
    'RECORRE LA CONSULTA
    While reader.Read()
      txttipo_cambio.Text = String.Format("{0}", reader(0)) + ": " + String.Format("{0:N4}", reader(1))
    End While
    'CIERRA LA CONEXION
    sqlConnectionDolar.Close()
  End Sub

  Private Sub CargaListaPrecios()
    Dim Consulta As String = ""
    Dim strcadena As String = ""
    Dim DTArts As New DataTable

    SQL.conectarTPM()
    Dim conec = New SqlConnection(StrTpm)
    Dim cmd = New SqlCommand("[SP_ListasEspeciales]", conec)
    cmd.CommandType = CommandType.StoredProcedure
    If CmbPerfil.SelectedItem = "Precios de compra" Then
      cmd.Parameters.Add("@Perfil", SqlDbType.VarChar).Value = "PC"
    Else
      cmd.Parameters.Add("@Perfil", SqlDbType.VarChar).Value = "PV"
    End If

    conec.Open()
    Dim adaptador = New SqlDataAdapter()
    adaptador.SelectCommand = cmd
    adaptador.SelectCommand.Connection = conec
    adaptador.SelectCommand.CommandTimeout = 10000
    cmd.ExecuteNonQuery()
    cmd.Connection.Close()
    conec.Close()
    adaptador.Fill(DTArts)

    Dim DsVtasDet As New DataSet

    DTArts.TableName = "Articulos"
    DsVtasDet.Tables.Add(DTArts)
    'CmdArts.Connection.Close()
    dv.Table = DsVtasDet.Tables("Articulos")
    DisenoGridVArt()
    SQL.Cerrar()
  End Sub

  Private Sub CmbLinea_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbLinea.SelectionChangeCommitted
    BuscaArticulos()
  End Sub

  Private Sub CmbLinea_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CmbLinea.Validating
    BuscaArticulos()
  End Sub

  Private Sub CmbLinea_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CmbLinea.KeyUp
    BuscaArticulos()
  End Sub

  Sub BuscaArticulos()
    Try

      dv.RowFilter = "Grupo_Articulo like '%" & Me.CmbLinea.Text & "%' AND " &
         "Articulo like '%" & Me.TxtArticulo.Text & "%' AND " &
         "Descripcion like '%" & Me.TxtDes.Text & "%' "

    Catch exc As Exception

      MessageBox.Show("CARACTER NO VALIDO," & Chr(13) & "BORRE EL CARACTER E INTENTE NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "Alerta",
    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

    End Try

  End Sub

  Private Sub TxtArticulo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtArticulo.TextChanged
    BuscaArticulos()
  End Sub

  Private Sub TxtDes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtDes.TextChanged
    BuscaArticulos()
  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMas.Click
    AgregaArticulo()
  End Sub

  Private Sub DGVCap_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGVCap.EditingControlShowing

    ' referencia a la celda 
    Dim validar As TextBox = CType(e.Control, TextBox)
    ' agregar el controlador de eventos para el KeyPress 
    AddHandler validar.KeyPress, AddressOf validar_Keypress

  End Sub
  Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    ' obtener indice de la columna 
    Dim columna As Integer = DGVCap.CurrentCell.ColumnIndex
    ' comprobar si la celda en edicin corresponde a la columna 0 o 1
    If columna = 0 Or columna = 1 Then
      ' Obtener caracter 
      Dim caracter As Char = e.KeyChar
      If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False Then
        'Me.Text = e.KeyChar 
        e.KeyChar = Chr(0)
      End If
    End If
    ' en la columna 3 acepto ademas de numeros un unico "." que es el separador decimal aqui
    If columna = 3 Then
      Dim caracter As Char = e.KeyChar
      ' referencia a la celda 
      Dim txt As TextBox = CType(sender, TextBox)
      ' comprobar si es un nmero con isNumber, si es el backspace, si el caracter 
      ' es el separador decimal, y que no contiene ya el separador 
      If (Char.IsNumber(caracter)) Or
       (caracter = ChrW(Keys.Back)) Or
       (caracter = ".") And
       (txt.Text.Contains(".") = False) Then

        e.Handled = False
      Else
        e.Handled = True
      End If
    End If
  End Sub

  Private Sub DGVCap_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVCap.CellEndEdit
    'If DGVArt(7, DGVArt.CurrentRow.Index).Value = "Y" Or DGVCap(7, DGVCap.CurrentRow.Index).Value = "Y" Then


    '    '******Consulta para contemplar los agentes que no tienen ventas**********************************
    '    Dim DTSinVta As New DataTable
    '    Dim strcadena As String


    '    strcadena = "SELECT TOP 1 T1.Discount,T1.Price FROM SPP1 T0 INNER JOIN SPP2 T1 ON T0.ItemCode = T1.ItemCode "
    '    strcadena &= "WHERE T0.ItemCode = '"
    '    strcadena &= DGVCap.CurrentRow.Cells(1).Value
    '    strcadena &= "' AND GETDATE() >= T0.FromDate AND GETDATE() <= T0.ToDate AND "
    '    strcadena &= DGVCap.CurrentRow.Cells(0).Value.ToString
    '    strcadena &= " >= T1.Amount ORDER BY T1.Discount DESC"

    '    Dim CmdSinVta As New SqlClient.SqlCommand(strcadena)

    '    CmdSinVta.Connection = New SqlClient.SqlConnection(StrCon)
    '    CmdSinVta.Connection.Open()

    '    Dim DTSinVta As New DataTable
    '    Dim AdapSinVta As New SqlClient.SqlDataAdapter(CmdSinVta)
    '    AdapSinVta.Fill(DTSinVta)
    '    CmdSinVta.Connection.Close()

    '    If DTSinVta.Rows.Count > 0 Then

    '        For Each fila As DataRow In DTSinVta.Rows
    '            DGVCap(4, DGVCap.CurrentRow.Index).Value = fila("Discount")
    '            DGVCap(6, DGVCap.CurrentRow.Index).Value = fila("Price")
    '            DGVCap(5, DGVCap.CurrentRow.Index).Value = fila("Price") * DGVCap.CurrentRow.Cells(0).Value
    '        Next
    '    Else
    '        DGVCap(5, DGVCap.CurrentRow.Index).Value = DGVCap(6, DGVCap.CurrentRow.Index).Value * DGVCap.CurrentRow.Cells(0).Value
    '    End If
    'Else

    '    Dim PrecioArt As Decimal
    '    Dim Strcadena As String = ""
    '    Dim vticket As String = ""

    '    Strcadena = "SELECT CASE WHEN T3.Price IS NULL OR T3.Price = 0 THEN T0.[Price] ELSE T3.Price END AS Price "
    '    Strcadena &= "FROM ITM1 T0 LEFT JOIN SPP1 T3 ON T0.ItemCode = T3.ItemCode AND T3.ListNum = 1 AND GETDATE() >= T3.FromDate AND GETDATE() <= T3.ToDate "
    '    Strcadena &= "WHERE T0.[PriceList] = 1 AND T0.ItemCode = '" & DGVCap(1, DGVCap.CurrentRow.Index).Value.ToString & "'"

    '    Dim cmd As New Data.SqlClient.SqlCommand
    '    With cmd

    '        .CommandText = Strcadena
    '        .CommandType = CommandType.Text
    '        .Connection = New Data.SqlClient.SqlConnection(StrCon)
    '        .Connection.Open()
    '        PrecioArt = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
    '        .Connection.Close()
    '    End With



    '    DGVCap(6, DGVCap.CurrentRow.Index).Value = PrecioArt
    '    DGVCap(5, DGVCap.CurrentRow.Index).Value = PrecioArt * DGVCap.CurrentRow.Cells(0).Value


    '    'DGVCap(5, DGVCap.CurrentRow.Index).Value = DGVCap(6, DGVCap.CurrentRow.Index).Value * DGVCap.CurrentRow.Cells(0).Value
    'End If
    'Dim DTSinVta As New DataTable
    Dim DTSinVta As New DataTable
    Dim cnn As SqlConnection = Nothing
    Dim cmd4 As SqlCommand = Nothing

    Try
      cnn = New SqlConnection(StrTpm)

      'LINEA ORIGINAL, MODIFICADA POR PRUEBAS, ARREGLAR AL FINAL
      cmd4 = New SqlCommand("ArticuloBoletin2", cnn)
      'cmd4 = New SqlCommand("ArticuloBoletinDolar2", cnn)

      cmd4.CommandType = CommandType.StoredProcedure
      cmd4.Parameters.AddWithValue("@Articulo", DGVCap.Item(1, DGVCap.CurrentRow.Index).Value)
      cmd4.Parameters.AddWithValue("@Cantidad", DGVCap.Item(0, DGVCap.CurrentRow.Index).Value)
      cmd4.Parameters.AddWithValue("@PriceList", DGVCap.Item(10, DGVCap.CurrentRow.Index).Value)
      If CmbPerfil.SelectedItem = "Precios de compra" Then
        cmd4.Parameters.AddWithValue("@OmitirDescuento", "1")
      End If

      cnn.Open()

      Dim da As New SqlDataAdapter
      da.SelectCommand = cmd4
      da.SelectCommand.Connection = cnn
      da.SelectCommand.CommandTimeout = 2000
      da.Fill(DTSinVta)

      cmd4.ExecuteNonQuery()
      cmd4.Connection.Close()

      ''--------------------------------------------
      If DTSinVta.Rows.Count > 0 Then

        For Each fila As DataRow In DTSinVta.Rows
          DGVCap(3, DGVCap.CurrentRow.Index).Value = fila("Price1")
          DGVCap(4, DGVCap.CurrentRow.Index).Value = fila("Discount")

          'MODIFICADO POR IVAN GONZALEZ
          Dim PrecioNeto As Decimal
          If DGVCap(7, DGVCap.CurrentRow.Index).Value = "USD" Then
            PrecioNeto = Decimal.Parse(fila("Price1")) * Decimal.Parse(fila("Dolar"))
          Else
            If Decimal.Parse(fila("Discount")) <> 0 Then
              PrecioNeto = Decimal.Parse(fila("Price1")) - ((Decimal.Parse(fila("Price1")) * Decimal.Parse(fila("Discount"))) / 100)
            Else
              PrecioNeto = Decimal.Parse(fila("Price1"))
            End If
          End If
          DGVCap(5, DGVCap.CurrentRow.Index).Value = PrecioNeto * DGVCap.CurrentRow.Cells(0).Value
          '----------------------------------

          'DGVCap(5, DGVCap.CurrentRow.Index).Value = fila("Price") * DGVCap.CurrentRow.Cells(0).Value
        Next
        'Else
        'DGVCap(5, DGVCap.CurrentRow.Index).Value = DGVCap(6, DGVCap.CurrentRow.Index).Value * DGVCap.CurrentRow.Cells(0).Value
      End If
      ''--------------------------------------------
      Dim DsVtas As New DataSet
      da.Fill(DsVtas, "DsVtas")

      DsVtas.Tables(0).TableName = "VentasCli"

      Dim DvVentasCli As New DataView

      DvVentasCli.Table = DsVtas.Tables("VentasCli")
    Catch ex As Exception
      MsgBox(ex.Message)
    Finally
      If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
        cnn.Close()
      End If
    End Try

    If DGVCap.Rows.Count > 0 Then
      BtnEmail.Enabled = True
    Else
      BtnEmail.Enabled = False
    End If

    DGVCap.CurrentRow.Cells(0).Value = Convert.ToDecimal(DGVCap.CurrentRow.Cells(0).Value)
    TxtArticulo.Focus()
    TxtArticulo.SelectAll()
  End Sub

  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMenos.Click
    Try
      Me.DGVCap.Rows.Remove(Me.DGVCap.CurrentRow)

      If DGVCap.Rows.Count > 0 Then
        BtnEmail.Enabled = True
      Else
        BtnEmail.Enabled = False
        BtnMenos.Enabled = False
      End If

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub BtnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprimir.Click

    If Me.CmbPerfil.SelectedIndex = -1 Then
      MessageBox.Show("Seleccione un Perfil", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbPerfil.Focus()
      Return
    End If

    If DGVCap.RowCount = 0 Then
      MessageBox.Show("Capture un artículo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      Return
    End If

    'CAMPOS DEL DATASET 
    Dim ErrOV As Integer = 0

    BtnImprimir.Enabled = False
    Try
      Dim DtOVta As New DataTable("OrdVenta")

      DtOVta.Columns.Add("IdOrdVta", Type.GetType("System.Int32"))
      DtOVta.Columns.Add("Serie", Type.GetType("System.String"))
      DtOVta.Columns.Add("FchOVta", Type.GetType("System.DateTime"))
      DtOVta.Columns.Add("UsrOVta", Type.GetType("System.String"))
      DtOVta.Columns.Add("Agente", Type.GetType("System.String"))
      DtOVta.Columns.Add("NomAgente", Type.GetType("System.String"))
      DtOVta.Columns.Add("IdCliente", Type.GetType("System.String"))
      DtOVta.Columns.Add("DesClte", Type.GetType("System.String"))
      DtOVta.Columns.Add("IdTrnsp", Type.GetType("System.Int32"))
      DtOVta.Columns.Add("DesTrnsp", Type.GetType("System.String"))
      DtOVta.Columns.Add("PerCon", Type.GetType("System.String"))
      DtOVta.Columns.Add("Comen", Type.GetType("System.String"))
      DtOVta.Columns.Add("Direccion", Type.GetType("System.String"))
      DtOVta.Columns.Add("Colonia", Type.GetType("System.String"))
      DtOVta.Columns.Add("CP", Type.GetType("System.String"))
      DtOVta.Columns.Add("Ciudad", Type.GetType("System.String"))
      DtOVta.Columns.Add("Estado", Type.GetType("System.String"))
      DtOVta.Columns.Add("Pais", Type.GetType("System.String"))
      DtOVta.Columns.Add("Rfc", Type.GetType("System.String"))
      DtOVta.Columns.Add("NumLinea", Type.GetType("System.Int32"))
      DtOVta.Columns.Add("Articulo", Type.GetType("System.String"))
      DtOVta.Columns.Add("Linea", Type.GetType("System.String"))
      DtOVta.Columns.Add("DesArt", Type.GetType("System.String"))
      DtOVta.Columns.Add("ListaP", Type.GetType("System.Int32"))
      DtOVta.Columns.Add("Precio", Type.GetType("System.Decimal"))
      DtOVta.Columns.Add("Cantidad", Type.GetType("System.Int32"))
      DtOVta.Columns.Add("DescLin", Type.GetType("System.Decimal"))
      DtOVta.Columns.Add("Totlinea", Type.GetType("System.Decimal"))
      DtOVta.Columns.Add("DocAntIva", Type.GetType("System.Decimal"))
      DtOVta.Columns.Add("DocIva", Type.GetType("System.Decimal"))
      DtOVta.Columns.Add("DocTotal", Type.GetType("System.Decimal"))

      'SE AGREGAN ESTOS DOS CAMBIOS PARA MONEDA Y TIPO DE DATO: URIEL TORALVA 19/05/2018
      DtOVta.Columns.Add("TipoCambio", Type.GetType("System.String"))
      DtOVta.Columns.Add("Moneda", Type.GetType("System.String"))
      DtOVta.Columns.Add("NumLetra", Type.GetType("System.String")) 'COLOCA EL NUMERO CON LETRA

      Dim columnas As DataColumnCollection = DtOVta.Columns
      Dim series As String = ""
      Dim _filaTemp As DataRow
      Dim vTotIva As Decimal = 0
      Dim vTotSIva As Decimal = 0
      Dim vTotDoc As Decimal = 0
      Dim vSinValor As Integer = 0
      Dim Fila As Integer = 0

      For Each row As DataGridViewRow In Me.DGVCap.Rows
        Fila += 1
        vTotSIva += row.Cells(5).Value
        If row.Cells(5).Value = 0 Then
          vSinValor = 1
          Exit For
        End If
      Next

      If vSinValor = 1 Then
        MessageBox.Show("Articulo sin cantidad de piezas", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        With DGVCap
          'Establecemos la celda actual
          '
          .CurrentCell = .Rows(Fila - 1).Cells(0)

          ' Y la ponemos en modo de edición.
          '
          .BeginEdit(True)
        End With
        BtnImprimir.Enabled = True
        Return
      End If

      vTotIva = vTotSIva * 0.16

      vTotDoc = vTotSIva + vTotIva

      'CONVIERTE EL IMPORTE DE NUMERO EN LETRA
      NumLetra = Numalet.ToCardinal(vTotDoc.ToString)

      Dim contador As Integer = 0

      For Each row As DataGridViewRow In Me.DGVCap.Rows
        contador += 1
        _filaTemp = DtOVta.NewRow()
        _filaTemp(columnas(0)) = NumOVta.ToString
        _filaTemp(columnas(1)) = "-  " & vSerie & NumOVta.ToString
        _filaTemp(columnas(2)) = Date.Now.ToString
        _filaTemp(columnas(3)) = UsrTPM
        '_filaTemp(columnas(4)) = DvClte(CmbPerfil.SelectedIndex).Item("IdAgente").ToString
        '_filaTemp(columnas(5)) = DvClte(CmbPerfil.SelectedIndex).Item("Agente").ToString
        '_filaTemp(columnas(6)) = DvClte(CmbPerfil.SelectedIndex).Item("Cliente").ToString
        '_filaTemp(columnas(7)) = DvClte(CmbPerfil.SelectedIndex).Item("Nombre").ToString

        _filaTemp(columnas(4)) = ""
        _filaTemp(columnas(5)) = ""
        _filaTemp(columnas(6)) = ""
        _filaTemp(columnas(7)) = ""

        '_filaTemp(columnas(8)) = CmbEnvio.SelectedValue.ToString
        '_filaTemp(columnas(9)) = CmbEnvio.Text
        '_filaTemp(columnas(10)) = DvClte(CmbPerfil.SelectedIndex).Item("Contacto").ToString
        _filaTemp(columnas(10)) = ""
        _filaTemp(columnas(11)) = TxtComentario.Text.ToString
        '_filaTemp(columnas(12)) = DvClte(CmbPerfil.SelectedIndex).Item("Direccion").ToString
        '_filaTemp(columnas(13)) = DvClte(CmbPerfil.SelectedIndex).Item("Colonia").ToString
        '_filaTemp(columnas(14)) = DvClte(CmbPerfil.SelectedIndex).Item("CP").ToString
        '_filaTemp(columnas(15)) = Trim(DvClte(CmbPerfil.SelectedIndex).Item("Ciudad").ToString) & ", " & Trim(DvClte(CmbPerfil.SelectedIndex).Item("Estado").ToString) & ", " & Trim(DvClte(CmbPerfil.SelectedIndex).Item("Pais").ToString) & ", " & Trim(DvClte(CmbPerfil.SelectedIndex).Item("CP").ToString)
        '_filaTemp(columnas(16)) = DvClte(CmbPerfil.SelectedIndex).Item("Estado").ToString
        '_filaTemp(columnas(17)) = DvClte(CmbPerfil.SelectedIndex).Item("Pais").ToString
        '_filaTemp(columnas(18)) = DvClte(CmbPerfil.SelectedIndex).Item("Rfc").ToString

        _filaTemp(columnas(12)) = ""
        _filaTemp(columnas(13)) = ""
        _filaTemp(columnas(14)) = ""
        _filaTemp(columnas(15)) = ""
        _filaTemp(columnas(16)) = ""
        _filaTemp(columnas(17)) = ""
        _filaTemp(columnas(18)) = ""

        _filaTemp(columnas(19)) = contador
        _filaTemp(columnas(20)) = row.Cells(1).Value    'ARTICULO
        _filaTemp(columnas(21)) = row.Cells(6).Value    'LINEA
        _filaTemp(columnas(22)) = row.Cells(2).Value    'DESCRPCION ARTICULO
        _filaTemp(columnas(23)) = "1"
        _filaTemp(columnas(24)) = row.Cells(3).Value    'PRECIO
        _filaTemp(columnas(25)) = row.Cells(0).Value    'CANTIDAD DE PIEZAS SOLICITADAS
        _filaTemp(columnas(26)) = row.Cells(4).Value    'DESCUENTO
        _filaTemp(columnas(27)) = row.Cells(5).Value    'IMPORTE
        _filaTemp(columnas(28)) = vTotSIva.ToString
        _filaTemp(columnas(29)) = vTotIva.ToString
        _filaTemp(columnas(30)) = vTotDoc.ToString

        'SE AGREGAN CAMPOS AL REPORTE CON EL DATASET: URIEL TORALVA 19/05/2018
        _filaTemp(columnas(31)) = txttipo_cambio.Text.ToString     'TIPO DEL CAMBIO AL DIA
        _filaTemp(columnas(32)) = row.Cells(7).Value               'MONEDA USD O MXP
        _filaTemp(columnas(33)) = NumLetra               'COLOCA EL IMPORTE EN LETRA

        DtOVta.Rows.Add(_filaTemp)

      Next

      Dim informe As New CrCotizacionEspecial

      'RepComsultaP.MdiParent = Inicio
      informe.SetDataSource(DtOVta)

      RepComsultaP.CrVConsulta.ReportSource = informe
      RepComsultaP.Show()
    Catch
      ErrOV = 1
      MessageBox.Show("No fue posible mostrar la cotización", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

    End Try

    If ErrOV = 1 Then
      BtnImprimir.Enabled = True
      Return
    End If

    BtnImprimir.Enabled = True
  End Sub

  Public Sub EnviarMail(ByVal De As String, ByVal Para As String(), ByVal Asunto As String, ByVal Cuerpo As String, Optional ByVal CC As String() = Nothing, Optional ByVal CCO As String() = Nothing)

    Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
    Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    Try

      Msg.From = New System.Net.Mail.MailAddress(De, "", System.Text.Encoding.UTF8) ' De quien se envia el Correo

      For Each From As String In Para
        If From <> "" Then Msg.To.Add(From) ' Para quien se Envia
      Next
      'Msg.To.Add("asistemas@tractopartesdiamante.com.mx")

      If CC IsNot Nothing Then
        For Each C As String In CC
          If C <> "" Then Msg.CC.Add(C)
        Next
      End If

      If CCO IsNot Nothing Then
        For Each C As String In CCO
          If C <> "" Then Msg.Bcc.Add(C)
        Next
      End If

      Dim Titulo As String
      Titulo = "COTIZACION"


      Msg.Subject = Titulo

      Dim vMensaje As String = "Estimado cliente:" + vbCrLf + vbCrLf + "Adjunto a este mensaje encontrará la cotización que usted solicitó el día de hoy." + vbCrLf + vbCrLf + "Saludos cordiales."

      Msg.SubjectEncoding = System.Text.Encoding.UTF8 ' Encriptando el Asunto del Mensaje
      Msg.Body = vMensaje ' Cuerpo del Mensaje 
      Msg.BodyEncoding = System.Text.Encoding.UTF8 ' Codificando el Cuerpo del Mensaje
      Msg.IsBodyHtml = False ' El Cuerpo del Mensaje no es HTML

      Dim thisAttachment As Attachment = New Attachment(vNombreGeneral) ' “image/jpeg”)

      Msg.Attachments.Add(thisAttachment) 'SE ADJUNTA ARCHIVO PDF
      SMTP.UseDefaultCredentials = False ' Si requiere Credenciales por Defecto



      SMTP.Credentials = New System.Net.NetworkCredential("tpd@tractopartesdiamante.com.mx", "tpTr@cto2012") ' las Credenciales para poder enviar el Mensaje
      SMTP.Port = 2525 ' El puerto que utiliza para el envio de Mensajes
      SMTP.Host = "mail.tractopartesdiamante.com.mx" ' el Servidor para el envio de Mensajes
      SMTP.EnableSsl = False ' Esto es para que vaya a través de SSL(Uso de Certificado Digital) por si usamos GMail por ejm.
      SMTP.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network ' Enviando Atravez de la red
      'mail.fng-puebla.com.mx 192.168.1.7
      SMTP.Send(Msg)
    Catch exc As Exception

      MessageBox.Show("NO FUE POSIBLE ENVIAR EMAIL DE LA COTIZACION," & Chr(13) & "INTENTE ENVIAR EMAIL NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !",
      MessageBoxButtons.OK, MessageBoxIcon.Error)
      VErrOv = 1
    Finally
      System.Windows.Forms.Cursor.Current = Cursors.Default
    End Try
  End Sub

  Sub ParMailEspecial(Destinatario As String)
    Dim CC() As String = {""}
    Dim PARA() As String = {Destinatario}
    EnviarMail("tpd@tractopartesdiamante.com.mx", PARA, "Cotización", "", CC)
    'De As String, 
    'PARA As String(), 
    'Asunto As String, 
    'Cuerpo As String
  End Sub

  Private Sub BtnNvo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNvo.Click
    Reiniciar()
  End Sub

  Private Sub Reiniciar()
    VErrClte = 0
    VErrCAd = 0
    NumOVta = 0
    CmbPerfil.Enabled = True
    TxtComentario.Enabled = True

    CmbPerfil.SelectedValue = 0
    TxtComentario.Text = ""
    BtnMenos.Enabled = False

    TxtCorreoC.Text = ""

    TxtArticulo.Text = ""
    TxtDes.Text = ""
    CmbLinea.SelectedIndex = -1

    dv.RowFilter = ""

    BtnMas.Enabled = True
    AgregaArt = 0

    BtnNvo.Enabled = False

    TxtCorreoAd.Enabled = True
    TxtCorreoAd.Text = ""

    DGVCap.Rows.Clear()
    DGVCap.Enabled = True
    CmbPerfil.Focus()

    DGVArt.Columns.Clear()

    CargaListaPrecios()
  End Sub

  'Private Sub CmbCliente_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CmbPerfil.Validating
  '  Try
  '    TxtCorreoC.Text = DvClte(CmbPerfil.SelectedIndex).Item("Mail").ToString()
  '  Catch

  '  End Try
  'End Sub

  Private Sub AgregaArticulo()
    If DGVCap.RowCount > 0 Then
      For i As Integer = 0 To DGVCap.RowCount - 1
        'MsgBox(DGVArt.Item(0, DGVArt.CurrentCell.RowIndex).Value & " = " & DGVCap.Item(1, i).Value)
        If DGVArt.Item(0, DGVArt.CurrentCell.RowIndex).Value = DGVCap.Item(1, i).Value Then
          'MsgBox("El artículo ya ha sido agregado.")

          MessageBox.Show("El artículo ya ha sido agregado.", "Error al agregar",
          MessageBoxButtons.OK, MessageBoxIcon.Information)


          DGVCap.CurrentCell = DGVCap.Rows(i).Cells(0)

          ' Y la ponemos en modo de edición.
          DGVCap.BeginEdit(True)

          Return
        End If
      Next

      'Valores DGrid1
      'Cantidad(),Articulo(0),Descripcion(1),
      'Precio(2),DescProm(6), Importe 0, Price(5), 
      'Expand(7), Linea(3), Linea(3)
      Try
        'ANTERIOR : URIEL TORALVA 19/05/2018
        'Me.DGVCap.Rows.Add(0, DGVArt(0, DGVArt.CurrentRow.Index).Value.ToString(), DGVArt(1, DGVArt.CurrentRow.Index).Value.ToString(),
        '0, 0, 0, DGVArt(3, DGVArt.CurrentRow.Index).Value.ToString(),
        '0, 0, 0)
        Me.DGVCap.Rows.Add(0, DGVArt(0, DGVArt.CurrentRow.Index).Value.ToString(), DGVArt(1, DGVArt.CurrentRow.Index).Value.ToString(),
        0, 0, 0, DGVArt(3, DGVArt.CurrentRow.Index).Value.ToString(),
        DGVArt(9, DGVArt.CurrentRow.Index).Value.ToString(), 0, 0, DGVArt(8, DGVArt.CurrentRow.Index).Value.ToString())

      Catch ex As Exception
        MsgBox(ex.Message)
      End Try

      With DGVCap
        'Establecemos la celda actual
        .CurrentCell = .Rows(Me.DGVCap.Rows.Count - 1).Cells(0)

        ' Y la ponemos en modo de edición.
        .BeginEdit(True)
      End With
    Else
      Try
        'ANTERIOR: URIEL TORALVA 19/05/2018
        'Me.DGVCap.Rows.Add(0, DGVArt(0, DGVArt.CurrentRow.Index).Value.ToString(), DGVArt(1, DGVArt.CurrentRow.Index).Value.ToString(),
        '0, 0, 0, DGVArt(3, DGVArt.CurrentRow.Index).Value.ToString(),
        '0, 0, 0)
        Me.DGVCap.Rows.Add(0, DGVArt(0, DGVArt.CurrentRow.Index).Value.ToString(), DGVArt(1, DGVArt.CurrentRow.Index).Value.ToString(),
        0, 0, 0, DGVArt(3, DGVArt.CurrentRow.Index).Value.ToString(),
        DGVArt(9, DGVArt.CurrentRow.Index).Value.ToString(), 0, 0, DGVArt(8, DGVArt.CurrentRow.Index).Value.ToString())

        With DGVCap
          'Establecemos la celda actual
          .CurrentCell = .Rows(Me.DGVCap.Rows.Count - 1).Cells(0)

          ' Y la ponemos en modo de edición.
          .BeginEdit(True)
        End With
      Catch ex As Exception
        MsgBox(ex.Message)
      End Try

      If DGVCap.Rows.Count > 0 Then
        BtnEmail.Enabled = True
        BtnMenos.Enabled = True
      Else
        BtnEmail.Enabled = False
        End If
      End If
  End Sub

  Private Sub DGVArt_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVArt.DoubleClick
    If AgregaArt = 0 Then
      AgregaArticulo()
    End If
  End Sub


  Private Sub BtnEmail_Click(sender As System.Object, e As System.EventArgs) Handles BtnEmail.Click
    If TxtCorreoAd.Text.ToString.Trim() = "" Then
      MessageBox.Show("Para poder enviar el correo por favor primero indique el correo electrónico", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
      TxtCorreoAd.Select()
      Exit Sub
    End If

    If Me.CmbPerfil.SelectedIndex = -1 Then
      MessageBox.Show("Seleccione un Perfil", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbPerfil.Focus()
      Exit Sub
    End If

    If DGVCap.RowCount = 0 Then
      MessageBox.Show("Capture un artículo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      Return
    End If

    Dim vTotIva As Decimal = 0
    Dim vTotSIva As Decimal = 0
    Dim vTotDoc As Decimal = 0
    Dim vSinValor As Integer = 0
    Dim Fila As Integer = 0

    'recorre todos los registros del grid DGVCap
    For Each row As DataGridViewRow In Me.DGVCap.Rows
      Fila += 1
      vTotSIva += row.Cells(5).Value
      If row.Cells(5).Value = 0 Then      'si la cantidad en algun renglon es 0 marca error "ARTICULO SIN CANTIDAD"
        vSinValor = 1
        Exit For
      End If
    Next

    If vSinValor = 1 Then
      MessageBox.Show("Artículo sin cantidad de piezas", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      With DGVCap
        .CurrentCell = .Rows(Fila - 1).Cells(0)
        .BeginEdit(True)
      End With

      Return
    End If

    ''ESTE CODIGO SE AGREGO
    '**********************
    Dim DTSinVta As New DataTable
      Dim cnn As SqlConnection = Nothing
      Dim cmd4 As SqlCommand = Nothing
      Try
        For i = 0 To DGVCap.RowCount - 1
          cnn = New SqlConnection(StrTpm)
          cmd4 = New SqlCommand("ArticuloBoletin2", cnn)
          cmd4.CommandType = CommandType.StoredProcedure
          cmd4.Parameters.AddWithValue("@Articulo", DGVCap.Item(1, i).Value)
        cmd4.Parameters.AddWithValue("@Cantidad", DGVCap.Item(0, i).Value)
        cmd4.Parameters.AddWithValue("@PriceList", DGVCap.Item(10, i).Value)
        If CmbPerfil.SelectedItem = "Precios de compra" Then
          cmd4.Parameters.AddWithValue("@OmitirDescuento", "1")
        End If
        cnn.Open()

          Dim da As New SqlDataAdapter
          da.SelectCommand = cmd4
          da.SelectCommand.Connection = cnn
          da.SelectCommand.CommandTimeout = 2000
          da.Fill(DTSinVta)

          cmd4.ExecuteNonQuery()
          cmd4.Connection.Close()

          ''--------------------------------------------
          If DTSinVta.Rows.Count > 0 Then
            For Each row As DataRow In DTSinVta.Rows
              DGVCap(3, i).Value = row("Price1")
              DGVCap(4, i).Value = row("Discount")

              'MODIFICADO POR IVAN GONZALEZ
              Dim PrecioNeto As Decimal

              If DGVCap.Item(7, DGVCap.CurrentRow.Index).Value = "USD" Then
                PrecioNeto = Decimal.Parse(row("Price1")) * Decimal.Parse(row("Dolar"))
              Else
                If Decimal.Parse(row("Discount")) <> 0 Then
                  PrecioNeto = Decimal.Parse(row("Price1")) - ((Decimal.Parse(row("Price1")) * Decimal.Parse(row("Discount"))) / 100)
                Else
                  PrecioNeto = Decimal.Parse(row("Price1"))
                End If
              End If
              DGVCap(5, DGVCap.CurrentRow.Index).Value = PrecioNeto * DGVCap.CurrentRow.Cells(0).Value
            Next
          End If
          ''--------------------------------------------

          Dim DsVtas As New DataSet
          da.Fill(DsVtas, "DsVtas")
          DsVtas.Tables(0).TableName = "VentasCli"
          Dim DvVentasCli As New DataView
          DvVentasCli.Table = DsVtas.Tables("VentasCli")
        Next

      Catch ex As Exception
        MsgBox(ex.Message)
      Finally
        If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
          cnn.Close()
        End If
      End Try

      'DGVCap.CurrentRow.Cells(0).Value = Convert.ToDecimal(DGVCap.CurrentRow.Cells(0).Value)
      'TxtArticulo.Focus()
      'TxtArticulo.SelectAll()
      ''***********************
      ''FIN DEL CODIGO AGREGADO

      vTotIva = vTotSIva * 0.16
      vTotDoc = vTotSIva + vTotIva

      'CONVIERTE EL IMPORTE DE NUMERO EN LETRA
      NumLetra = Numalet.ToCardinal(vTotDoc.ToString)

      If VErrOv = 1 Then
        TxtArticulo.Focus()
        Return
      End If
      '*********************************************************************************************************************

      'Try
      Dim DtOVta As New DataTable("OrdVenta")

      DtOVta.Columns.Add("IdOrdVta", Type.GetType("System.Int32"))
      DtOVta.Columns.Add("Serie", Type.GetType("System.String"))
      DtOVta.Columns.Add("FchOVta", Type.GetType("System.DateTime"))
      DtOVta.Columns.Add("UsrOVta", Type.GetType("System.String"))
      DtOVta.Columns.Add("Agente", Type.GetType("System.String"))
      DtOVta.Columns.Add("NomAgente", Type.GetType("System.String"))
      DtOVta.Columns.Add("IdCliente", Type.GetType("System.String"))
      DtOVta.Columns.Add("DesClte", Type.GetType("System.String"))
      DtOVta.Columns.Add("IdTrnsp", Type.GetType("System.Int32"))
      DtOVta.Columns.Add("DesTrnsp", Type.GetType("System.String"))
      DtOVta.Columns.Add("PerCon", Type.GetType("System.String"))
      DtOVta.Columns.Add("Comen", Type.GetType("System.String"))
      DtOVta.Columns.Add("Direccion", Type.GetType("System.String"))
      DtOVta.Columns.Add("Colonia", Type.GetType("System.String"))
      DtOVta.Columns.Add("CP", Type.GetType("System.String"))
      DtOVta.Columns.Add("Ciudad", Type.GetType("System.String"))
      DtOVta.Columns.Add("Estado", Type.GetType("System.String"))
      DtOVta.Columns.Add("Pais", Type.GetType("System.String"))
      DtOVta.Columns.Add("Rfc", Type.GetType("System.String"))
      DtOVta.Columns.Add("NumLinea", Type.GetType("System.Int32"))
      DtOVta.Columns.Add("Articulo", Type.GetType("System.String"))
      DtOVta.Columns.Add("Linea", Type.GetType("System.String"))
      DtOVta.Columns.Add("DesArt", Type.GetType("System.String"))
      DtOVta.Columns.Add("ListaP", Type.GetType("System.Int32"))
      DtOVta.Columns.Add("Precio", Type.GetType("System.Decimal"))
      DtOVta.Columns.Add("Cantidad", Type.GetType("System.Int32"))
      DtOVta.Columns.Add("DescLin", Type.GetType("System.Decimal"))
      DtOVta.Columns.Add("Totlinea", Type.GetType("System.Decimal"))
      DtOVta.Columns.Add("DocAntIva", Type.GetType("System.Decimal"))
      DtOVta.Columns.Add("DocIva", Type.GetType("System.Decimal"))
      DtOVta.Columns.Add("DocTotal", Type.GetType("System.Decimal"))

      'SE AGREGAN ESTOS DOS CAMBIOS PARA MONEDA Y TIPO DE DATO: URIEL TORALVA 19/05/2018
      DtOVta.Columns.Add("TipoCambio", Type.GetType("System.String"))
      DtOVta.Columns.Add("Moneda", Type.GetType("System.String"))
      DtOVta.Columns.Add("NumLetra", Type.GetType("System.String")) 'COLOCA EL NUMERO CON LETRA

      Dim columnas As DataColumnCollection = DtOVta.Columns


      Dim series As String = ""
      Dim _filaTemp As DataRow

      Dim contador As UInteger = 0

      For Each row As DataGridViewRow In Me.DGVCap.Rows
        contador += 1

        _filaTemp = DtOVta.NewRow()
        _filaTemp(columnas(0)) = NumOVta.ToString
        _filaTemp(columnas(1)) = "- " & vSerie & NumOVta.ToString
        _filaTemp(columnas(2)) = Date.Now.ToString
        _filaTemp(columnas(3)) = UsrTPM
        _filaTemp(columnas(4)) = ""
        _filaTemp(columnas(5)) = ""
        _filaTemp(columnas(6)) = ""
        _filaTemp(columnas(7)) = ""
      '_filaTemp(columnas(8)) = CmbEnvio.SelectedValue.ToString
      '_filaTemp(columnas(9)) = CmbEnvio.Text
      _filaTemp(columnas(10)) = ""
        _filaTemp(columnas(11)) = TxtComentario.Text.ToString
        _filaTemp(columnas(12)) = ""
        _filaTemp(columnas(13)) = ""
        _filaTemp(columnas(14)) = ""
        _filaTemp(columnas(15)) = ""
        _filaTemp(columnas(16)) = ""
        _filaTemp(columnas(17)) = ""
        _filaTemp(columnas(18)) = ""
        _filaTemp(columnas(19)) = contador
        _filaTemp(columnas(20)) = row.Cells(1).Value
        _filaTemp(columnas(21)) = row.Cells(6).Value    'Linea
        _filaTemp(columnas(22)) = row.Cells(2).Value
        _filaTemp(columnas(23)) = "1"
        _filaTemp(columnas(24)) = row.Cells(3).Value
        _filaTemp(columnas(25)) = row.Cells(0).Value
        _filaTemp(columnas(26)) = row.Cells(4).Value
        _filaTemp(columnas(27)) = row.Cells(5).Value
        _filaTemp(columnas(28)) = vTotSIva.ToString
        _filaTemp(columnas(29)) = vTotIva.ToString
        _filaTemp(columnas(30)) = vTotDoc.ToString

        'SE AGREGAN CAMPOS AL REPORTE CON EL DATASET: URIEL TORALVA 19/05/2018
        _filaTemp(columnas(31)) = txttipo_cambio.Text.ToString     'TIPO DEL CAMBIO AL DIA
        _filaTemp(columnas(32)) = row.Cells(7).Value               'MONEDA USD O MXP
        _filaTemp(columnas(33)) = NumLetra               'COLOCA EL IMPORTE EN LETRA

        DtOVta.Rows.Add(_filaTemp)

      Next

    Dim informe As New CrCotizacionEspecial

    RepComsultaP.MdiParent = Inicio
    informe.SetDataSource(DtOVta)

    RepComsultaP.CrVConsulta.ReportSource = informe
    vNombreGeneral = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Cotizacion_" & Date.Now.ToString("ddMMyyyy") & ".pdf")

    informe.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, vNombreGeneral)

    ParMailEspecial(TxtCorreoAd.Text.ToString.Trim())

    Dim vTextmail As String = ""
    If VErrOv = 1 Then
      LblError.Text = vSerie & NumOVta.ToString & " -- " & "No fue posible enviar EMails. Intentelo nuevamente"
      vTextmail = TxtCorreoC.Text
      DGVCap.Enabled = False
      TxtComentario.Enabled = False
      BtnMas.Enabled = False
      AgregaArt = 1
      BtnMenos.Enabled = False
      LblError.Visible = True
      BtnEmail.Enabled = True
      TxtCorreoC.Text = vTextmail
      Return
    End If

    MessageBox.Show("La cotización se creo exitosamente! y se envio por correo electrónico!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)

    vTextmail = TxtCorreoC.Text
    LblError.Visible = False
    BtnEmail.Enabled = False
    DGVCap.Enabled = False
    TxtComentario.Enabled = False
    BtnMas.Enabled = False
    AgregaArt = 1
    BtnMenos.Enabled = False

    BtnNvo.Enabled = True
    TxtCorreoC.Text = vTextmail
    TxtArticulo.Focus()

    If VErrCAd = 1 Then
      MessageBox.Show("No fue posible enviar la cotización al EMail indicado," & Chr(13) & "Verifique el correo electrónico e intente nuevamente..." & Chr(13), "Advertencia",
      MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      BtnEmail.Enabled = True
    Else
      TxtCorreoAd.Enabled = False
    End If
  End Sub

  Sub ParMail2()
    Dim CCO() As String = {""}
    Dim CC() As String = {""}
    Dim PARA() As String = {Trim(TxtCorreoC.Text)}
    EnviarMail2(vCorreo, PARA, "xxx", "xxxx", CC, CCO)
  End Sub

  Public Sub EnviarMail2(ByVal De As String, ByVal Para As String(), ByVal Asunto As String, ByVal Cuerpo As String, Optional ByVal CC As String() = Nothing, Optional ByVal CCO As String() = Nothing)

    Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
    Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    Try

      Msg.From = New System.Net.Mail.MailAddress(De, "", System.Text.Encoding.UTF8) ' De quien se envia el Correo

      For Each From As String In Para
        If From <> "" Then Msg.To.Add(From) ' Para quien se Envia
      Next
      'SE USA PARA PRUEBAS DE ENVIOS
      'Msg.To.Add("asistemas@tractopartesdiamante.com.mx")
      If CC IsNot Nothing Then
        For Each C As String In CC
          If C <> "" Then Msg.CC.Add(C)
        Next
      End If

      If CCO IsNot Nothing Then
        For Each C As String In CCO
          If C <> "" Then Msg.Bcc.Add(C)
        Next
      End If

      Dim Titulo As String
      'Titulo = "Orden de Venta - " & vSerie & NumOVta.ToString
      Titulo = "COTIZACION"


      Msg.Subject = Titulo

      Dim vMensaje As String = "Estimado cliente:" + vbCrLf + vbCrLf + "Adjunto a este mensaje encontrará la cotización que usted solicitó el día de hoy." + vbCrLf + vbCrLf + "Saludos cordiales."

      Msg.SubjectEncoding = System.Text.Encoding.UTF8 ' Encriptando el Asunto del Mensaje
      Msg.Body = vMensaje ' Cuerpo del Mensaje 
      Msg.BodyEncoding = System.Text.Encoding.UTF8 ' Codificando el Cuerpo del Mensaje
      Msg.IsBodyHtml = False ' El Cuerpo del Mensaje no es HTML


      Dim vNomArch As String

      vNomArch = vSerie & NumOVta.ToString & "_OrdVta.pdf"

      'Dim thisAttachment As Attachment = New Attachment(oStrem, vNomArch) ' “image/jpeg”)
      Dim thisAttachment As Attachment = New Attachment(vNombreGeneral) ' “image/jpeg”)

      Msg.Attachments.Add(thisAttachment) 'SE ADJUNTA ARCHIVO PDF


      SMTP.UseDefaultCredentials = False ' Si requiere Credenciales por Defecto
      SMTP.Credentials = New System.Net.NetworkCredential(vCorreo, vPswmail) ' las Credenciales para poder enviar el Mensaje
      SMTP.Port = 2525 ' El puerto que utiliza para el envio de Mensajes
      SMTP.Host = "mail.tractopartesdiamante.com.mx" ' el Servidor para el envio de Mensajes
      SMTP.EnableSsl = False ' Esto es para que vaya a través de SSL(Uso de Certificado Digital) por si usamos GMail por ejm.
      SMTP.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network ' Enviando Atravez de la red
      'mail.fng-puebla.com.mx 192.168.1.7
      SMTP.Send(Msg)
    Catch exc As Exception
      VErrClte = 1
    Finally
      System.Windows.Forms.Cursor.Current = Cursors.Default
    End Try
  End Sub

  Sub ParMail3()
    Dim CCO() As String = {""}
    Dim CC() As String = {""}

    Dim PARA() As String = {Trim(TxtCorreoAd.Text)}
    EnviarMail3(vCorreo, PARA, "xxx", "xxxx", CC, CCO)
  End Sub

  Public Sub EnviarMail3(ByVal De As String, ByVal Para As String(), ByVal Asunto As String, ByVal Cuerpo As String, Optional ByVal CC As String() = Nothing, Optional ByVal CCO As String() = Nothing)

    Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
    Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
    Try

      Msg.From = New System.Net.Mail.MailAddress(De, "", System.Text.Encoding.UTF8) ' De quien se envia el Correo

      For Each From As String In Para
        If From <> "" Then Msg.To.Add(From) ' Para quien se Envia
      Next

      If CC IsNot Nothing Then
        For Each C As String In CC
          If C <> "" Then Msg.CC.Add(C)
        Next
      End If

      If CCO IsNot Nothing Then
        For Each C As String In CCO
          If C <> "" Then Msg.Bcc.Add(C)
        Next
      End If

      Dim Titulo As String
      Titulo = "COTIZACION"


      Msg.Subject = Titulo

      Dim vMensaje As String = "Estimado cliente:" + vbCrLf + vbCrLf + "Adjunto a este mensaje encontrará la cotización que usted solicitó el día de hoy." + vbCrLf + vbCrLf + "Saludos cordiales."

      Msg.SubjectEncoding = System.Text.Encoding.UTF8 ' Encriptando el Asunto del Mensaje
      Msg.Body = vMensaje ' Cuerpo del Mensaje 
      Msg.BodyEncoding = System.Text.Encoding.UTF8 ' Codificando el Cuerpo del Mensaje
      Msg.IsBodyHtml = False ' El Cuerpo del Mensaje no es HTML


      Dim vNomArch As String

      vNomArch = vSerie & NumOVta.ToString & "_OrdVta.pdf"

      'Dim thisAttachment As Attachment = New Attachment(oStrem, vNomArch) ' “image/jpeg”)
      Dim thisAttachment As Attachment = New Attachment(vNombreGeneral) ' “image/jpeg”)

      Msg.Attachments.Add(thisAttachment) 'SE ADJUNTA ARCHIVO PDF


      SMTP.UseDefaultCredentials = False ' Si requiere Credenciales por Defecto
      SMTP.Credentials = New System.Net.NetworkCredential(vCorreo, vPswmail) ' las Credenciales para poder enviar el Mensaje
      SMTP.Port = 2525 ' El puerto que utiliza para el envio de Mensajes
      SMTP.Host = "mail.tractopartesdiamante.com.mx" ' el Servidor para el envio de Mensajes
      SMTP.EnableSsl = False ' Esto es para que vaya a través de SSL(Uso de Certificado Digital) por si usamos GMail por ejm.
      SMTP.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network ' Enviando Atravez de la red
      'mail.fng-puebla.com.mx 192.168.1.7
      SMTP.Send(Msg)
    Catch exc As Exception
      VErrCAd = 1
    Finally
      System.Windows.Forms.Cursor.Current = Cursors.Default
    End Try
  End Sub

  Private Sub CapOrdVta_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If BtnEmail.Enabled = True Then
      e.Cancel = True
    End If
  End Sub

  Private Sub TxtComentario_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TxtComentario.KeyPress
    'Se bloquean los caracteres ' y ""
    If e.KeyChar = Chr(34) Or e.KeyChar = Chr(39) Then
      e.Handled = True
    Else
      e.Handled = False
    End If

  End Sub

  Private Sub CmbLinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbLinea.SelectedIndexChanged
    BuscaArticulos()
  End Sub

  Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
    End
  End Sub

  Private Sub CmbPerfil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPerfil.SelectedIndexChanged
    If CmbPerfil.SelectedItem = "Precios de compra" Then
      Me.DGVCap.Columns("Descuento").Visible = False
    Else
      Me.DGVCap.Columns("Descuento").Visible = True
    End If
    CargaListaPrecios()
  End Sub

  Private Sub CmbPerfil_Validated(sender As Object, e As EventArgs) Handles CmbPerfil.Validated
    If CmbPerfil.SelectedItem = "Precios de compra" Then
      Me.DGVCap.Columns("Descuento").Visible = False
    Else
      Me.DGVCap.Columns("Descuento").Visible = True
    End If
    CargaListaPrecios()
  End Sub

  Private Sub ReiniciarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReiniciarToolStripMenuItem.Click
    Reiniciar()
  End Sub
End Class