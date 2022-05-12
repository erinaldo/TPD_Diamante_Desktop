Public Class ArticulosInventario

    Sub cargar_registros()

        Dim DTMObra As New DataTable
        Dim informe As New CrInventario


        Dim Consulta As String = ""
        Dim strcadena As String = ""

        If Me.CmbGrupoArticulo.SelectedValue <> 999 Then

            Consulta = "SELECT T1.[ItemCode] AS Codigo,T1.ItemName AS Descripcion,"
            Consulta &= "T2.[ItmsGrpNam] AS Grupo_Articulo, ROW_NUMBER() OVER(ORDER BY T2.[ItmsGrpNam]) as Lista,T3.OnHand AS Cod "
            Consulta &= "FROM OITM T1  INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod "
            Consulta &= "LEFT JOIN OITW T3 ON T1.ItemCode = T3.ItemCode AND T3.WhsCode = 01 WHERE T1.ItmsGrpCod = @Codigo "
            Consulta &= "ORDER BY T2.[ItmsGrpNam],T1.[ItemCode]"


            '          SELECT T1.[ItemCode] AS Codigo,T1.ItemName AS Descripcion,
            'T2.[ItmsGrpNam] AS Grupo_Articulo, ROW_NUMBER() OVER(ORDER BY T2.[ItmsGrpNam]) as Lista,
            'T3.OnHand AS Cod
            'FROM OITM T1  INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod 
            'LEFT JOIN OITW T3 ON T1.ItemCode = T3.ItemCode AND T3.WhsCode = 01 
            'WHERE T1.ItmsGrpCod = 100 
            'ORDER BY T2.[ItmsGrpNam],T1.[ItemCode]

        Else

            Consulta = "SELECT T1.[ItemCode] AS Codigo,T1.ItemName AS Descripcion,"
            Consulta &= "T2.[ItmsGrpNam] AS Grupo_Articulo, ROW_NUMBER() OVER(ORDER BY T2.[ItmsGrpNam]) as Lista,T3.OnHand AS Cod "
            Consulta &= "FROM OITM T1  INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod "
            Consulta &= "LEFT JOIN OITW T3 ON T1.ItemCode = T3.ItemCode AND T3.WhsCode = 01 "
            Consulta &= "ORDER BY T2.[ItmsGrpNam],T1.[ItemCode]"

        End If

        Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

        If Me.CmbGrupoArticulo.SelectedValue <> 999 Then
            CmdMObra.Parameters.Add("@Codigo", SqlDbType.SmallInt)
            CmdMObra.Parameters("@Codigo").Value = CmbGrupoArticulo.SelectedValue
        End If


        CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
        CmdMObra.Connection.Open()

        Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
        AdapMObra.Fill(DTMObra)


        'Dim Cont As Decimal = 1
        'For Each fila As DataRow In DTMObra.Rows
        '    If Cont = 1 Then
        '        fila("Cod") = 0
        '        Cont = 0
        '    Else
        '        fila("Cod") = 1
        '        Cont = 1
        '    End If
        'Next


        RepComsultaP.MdiParent = Inicio
        informe.SetDataSource(DTMObra)

        RepComsultaP.CrVConsulta.ReportSource = informe

        If UsrTPM = "MANAGER" Or UsrTPM = "COMPRAS1" Then
            RepComsultaP.CrVConsulta.ShowExportButton = True
        End If


        RepComsultaP.Show()

    End Sub

    Private Sub Pagos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)


            Dim ConsutaLista As String


            Dim ds As New DataSet

            

            ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
            Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            Dim dsArt As New DataSet
            daArticulo.Fill(dsArt)

            Dim fila As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            fila = dsArt.Tables(0).NewRow

            'Agregamos los valores a los campos de la tabla
            fila("ItmsGrpNam") = "TODOS"
            fila("ItmsGrpCod") = 999

            ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            dsArt.Tables(0).Rows.Add(fila)

            Me.CmbGrupoArticulo.DataSource = dsArt.Tables(0)
            Me.CmbGrupoArticulo.DisplayMember = "ItmsGrpNam"
            Me.CmbGrupoArticulo.ValueMember = "ItmsGrpCod"
            Me.CmbGrupoArticulo.SelectedValue = 999


            'ConsutaLista = "SELECT slpcode,slpname FROM oslp"

            ''ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
            'Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            'Dim dsArt As New DataSet
            'daArticulo.Fill(dsArt)

            'Dim fila As Data.DataRow

            ''Asignamos a fila la nueva Row(Fila)del Dataset
            'fila = dsArt.Tables(0).NewRow

            ''Agregamos los valores a los campos de la tabla
            'fila("slpname") = "TODOS"
            'fila("slpcode") = 999

            ' ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            'dsArt.Tables(0).Rows.Add(fila)

            'Me.CmbGrupoArticulo.DataSource = dsArt.Tables(0)
            'Me.CmbGrupoArticulo.DisplayMember = "slpname"
            'Me.CmbGrupoArticulo.ValueMember = "slpcode"
            'Me.CmbGrupoArticulo.SelectedValue = 999

        End Using
    End Sub

    Private Sub BtnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprimir.Click

        If IsNothing(CmbGrupoArticulo.SelectedValue) Then
            MessageBox.Show("Valor no valido, Seleccione una grupo de artículo", _
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbGrupoArticulo.Focus()
            Return
        End If
        cargar_registros()
    End Sub

    Private Sub CmbGrupoArticulo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbGrupoArticulo.KeyPress
        e.KeyChar = Char.ToUpper(e.KeyChar)
    End Sub
End Class