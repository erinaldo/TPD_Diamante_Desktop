Imports System.Net
Public Class Pagos
  Sub cargar_registros()

    Dim DTMObra As New DataTable
    Dim informe As New CrListaPrecio2

    Dim Consulta As String = ""
    Dim strcadena As String = ""



    If Me.CmbGrupoArticulo.SelectedValue <> 999 Then

      Consulta = "SELECT T0.[ItemCode] AS Codigo,T1.ItemName AS Descripcion,T0.[Price] AS Precio ,"
      Consulta &= " T2.[ItmsGrpNam] AS Grupo_Articulo,T0.[PriceList] AS Lista,CAST(0 AS int) as Cod, case when T1.QryGroup60 = 'Y' then 'USD' else 'MXP' end as moneda "
      Consulta &= "FROM ITM1 T0 INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode AND T1.frozenFor <> 'Y' INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod "
      Consulta &= "WHERE T0.[PriceList] =@lista AND T1.ItmsGrpCod = @Codigo AND T0.Price > 0 "
      Consulta &= "ORDER BY T2.[ItmsGrpNam],T0.[ItemCode]"
    Else
      Consulta = "SELECT T0.[ItemCode] AS Codigo,T1.ItemName AS Descripcion,T0.[Price] AS Precio ,"
      Consulta &= " T2.[ItmsGrpNam] AS Grupo_Articulo,T0.[PriceList] AS Lista,CAST(0 AS int) as Cod, case when T1.QryGroup60 = 'Y' then 'USD' else 'MXP' end as moneda  "
      Consulta &= "FROM ITM1 T0 INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode AND T1.frozenFor <> 'Y' INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod "
      Consulta &= "WHERE T0.[PriceList] =@lista AND T0.Price > 0 ORDER BY T2.[ItmsGrpNam],T0.[ItemCode]"
    End If

    Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

    'Obtengo la lista de precios desde la descripcion
    Dim LP As Integer = CInt(Mid(CmbListaPrecio.Text.ToString, 1, CmbListaPrecio.Text.ToString.IndexOf(" ")))

    CmdMObra.Parameters.Add("@lista", SqlDbType.SmallInt)
    CmdMObra.Parameters("@lista").Value = CmbListaPrecio.SelectedValue
    If Me.CmbGrupoArticulo.SelectedValue <> 999 Then
      CmdMObra.Parameters.Add("@Codigo", SqlDbType.SmallInt)
      CmdMObra.Parameters("@Codigo").Value = CmbGrupoArticulo.SelectedValue
    End If

    CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
    CmdMObra.Connection.Open()

    Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
    AdapMObra.Fill(DTMObra)


    Dim Cont As Decimal = 1
    For Each fila As DataRow In DTMObra.Rows
      If Cont = 1 Then
        fila("Cod") = 0
        Cont = 0
      Else
        fila("Cod") = 1
        Cont = 1
      End If
    Next

    RepComsultaP.MdiParent = Inicio
    informe.SetDataSource(DTMObra)

    informe.SetParameterValue("NumLista", LP.ToString())
    RepComsultaP.CrVConsulta.ReportSource = informe

    If UsrTPM = "MANAGER" Or UsrTPM = "COMPRAS1" Or UsrTPM = "SINERGIA" Then
      RepComsultaP.CrVConsulta.ShowExportButton = True
    End If

    RepComsultaP.Show()
  End Sub

  Private Sub Pagos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
      Dim ConsutaLista As String

      If UsrTPM = "MANAGER" Then
        ConsutaLista = "SELECT ListName,ListNum FROM OPLN"
      ElseIf UsrTPM = "ANCAR" Then
        ConsutaLista = "SELECT ListName,ListNum FROM OPLN where ListNum = 1 "
      ElseIf lblListaEspecifica.Text <> "Lista Especifica" Then
        ConsutaLista = "SELECT ListName,ListNum FROM OPLN WHERE ListNum IN (" & lblListaEspecifica.Text & ")"
      Else
        'MUESTRA EL PRECIO 1 AL 3 Y EL PRECIO 10
        ConsutaLista = "SELECT ListName,ListNum FROM OPLN where ListNum <= 3 or ListNum = 12 "
      End If

      Dim da As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

      Dim ds As New DataSet

      da.Fill(ds)
      Me.CmbListaPrecio.DataSource = ds.Tables(0)
      Me.CmbListaPrecio.DisplayMember = "ListName"
      Me.CmbListaPrecio.ValueMember = "ListNum"
      Me.CmbListaPrecio.SelectedValue = 0

      ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
      Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

      Dim dsArt As New DataSet
      daArticulo.Fill(dsArt)

      Dim fila As Data.DataRow

      'Asignamos a fila la nueva Row(Fila)del Dataset
      fila = dsArt.Tables(0).NewRow

      If UsrTPM <> "ANCAR" Then
        'Agregamos los valores a los campos de la tabla
        fila("ItmsGrpNam") = "TODOS"
        fila("ItmsGrpCod") = 999
        dsArt.Tables(0).Rows.Add(fila)
      End If
      'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet



      Me.CmbGrupoArticulo.DataSource = dsArt.Tables(0)
      Me.CmbGrupoArticulo.DisplayMember = "ItmsGrpNam"
      Me.CmbGrupoArticulo.ValueMember = "ItmsGrpCod"
      If UsrTPM <> "ANCAR" Then
        Me.CmbGrupoArticulo.SelectedValue = 999

      End If
      ' Me.CmbListaPrecio.Focus()

      ' Me.CmbListaPrecio.SelectedText = " "

      'Me.CmbListaPrecio.SelectionStart = 0

    End Using
  End Sub

  Private Sub BtnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprimir.Click
    ' Dim objDataSet As New DataTable
    If IsNothing(CmbListaPrecio.SelectedValue) Then
      MessageBox.Show("Valor no valido, Seleccione una lista de precio",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbListaPrecio.Focus()
      Return
    End If

    If IsNothing(CmbGrupoArticulo.SelectedValue) Then
      MessageBox.Show("Valor no valido, Seleccione una grupo de artículo",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      CmbGrupoArticulo.Focus()
      Return
    End If
    cargar_registros()

  End Sub

  Private Sub CmbGrupoArticulo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbGrupoArticulo.KeyPress
    e.KeyChar = Char.ToUpper(e.KeyChar)
  End Sub

  Private Sub Pagos_Closed(sender As Object, e As EventArgs) Handles Me.Closed
    If UsrTPM = "SINERGIA" Then
      End
    End If
  End Sub
End Class