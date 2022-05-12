Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class frmAdminBarCode

    Dim SQL As New Comandos_SQL
    Dim DvCodigos As New DataView
    Dim DvSeleccion As New DataView
    Dim bNuevo As Boolean = False
    Dim Seleccionado As Integer = 0
    Dim Consecutivo As Integer = 0
    Dim CBInternoAModificar As String = 0

    Private Sub frmAdminBarCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQL.conectarTPM()
        Limpiar()
    End Sub

    Private Sub Limpiar()
        Buscar_Codigos()

        PanelSeleccion.Visible = False
        NuevoCódigoDeBarrasToolStripMenuItem.Enabled = True
        ModificarCódigoToolStripMenuItem.Enabled = False
        GrabarToolStripMenuItem.Enabled = False

        LimpiaPanel()
        PanelDatos.Enabled = False
        Consultar_CodAlt()
    End Sub

    Private Sub LimpiaPanel()
        txtArticulo.Text = ""
        txtCategoria.Text = ""
        txtLinea.Text = ""
        txtDescripcion.Text = ""
        txtProveedor.Text = ""
        txtCBInterno.Text = ""
        txtCBPieza.Text = ""
        txtCBBolsa.Text = ""
        txtCBCaja.Text = ""
        txtCBTarima.Text = ""
        txtProveedor.Text = ""
        txtCantBolsa.Text = ""
        txtCantCaja.Text = ""
        txtCantTR.Text = ""

        txtCategoria.Enabled = False
        txtCBInterno.Enabled = False
        txtCBProveedor.Enabled = False
    End Sub

    Sub Buscar_Codigos()
        Try
            SQL.conectarTPM()

            txtCategoria.Enabled = False
            txtCBInterno.Enabled = False
            txtCBProveedor.Enabled = False

            Dim DsBC As New DataSet

            Dim Consulta As String = "SELECT t0.articulo, t0.Categoria, t0.Descripcion, t0.Linea, t0.Proveedor, 
            t0.BARCODE_INTERNO, t0.BARCODE_BLI, t0.BARCODE_PZI, t0.BARCODE_CJI, t0.BARCODE_TRI, t0.BARCODE_PZE,t0.CantPzasBL,t0.CantPzasCJ,t0.CantPzasTR
            From Barcode t0
            FULL Join Proveedores t1 ON t0.articulo = t1.ItemCode
            And t1.ItmsGrpNam Not IN ('INACTIVOS')
            WHERE t0.Categoria Is Not NULL"


            '"SELECT t1.ItemCode, t0.Categoria, t1.ItemName, t1.ItmsGrpNam, t1.CardName, 
            '                    t0.BARCODE_INTERNO, t0.BARCODE_BLI, t0.BARCODE_PZI, t0.BARCODE_CJI, t0.BARCODE_TRI, t0.BARCODE_PZE 
            '                    FROM Barcode t0
            '                    FULL JOIN Proveedores t1 ON t0.articulo = t1.ItemCode
            '                    WHERE t1.ItmsGrpNam NOT IN ('INACTIVOS')
            '                    AND t0.Categoria IS NOT NULL order by t1.ItmsGrpNam"

            Dim conec = New SqlConnection(StrTpm)
            Dim cmd = New SqlCommand(Consulta, conec)
            'cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandType = CommandType.Text

            conec.Open()
            Dim adaptador = New SqlDataAdapter()
            adaptador.SelectCommand = cmd
            adaptador.SelectCommand.Connection = conec
            adaptador.SelectCommand.CommandTimeout = 10000
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
            conec.Close()
            adaptador.Fill(DsBC)

            DsBC.Tables(0).TableName = "Codigos"

            DvCodigos.Table = DsBC.Tables("Codigos")

            DtGInfCB.DataSource = DvCodigos
            EstiloDgCB()

            SQL.Cerrar()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error al ejecutar la consulta")
        End Try
    End Sub

    Sub BuscaArticulos()
        Try

            DvCodigos.RowFilter = "articulo like '%" & Me.txtCodigo.Text & "%' AND " &
                "Descripcion like '%" & Me.txtNombreArticulo.Text & "%' "


        Catch exc As Exception

            MessageBox.Show("CARACTER NO VALIDO," & Chr(13) & "BORRE EL CARACTER E INTENTE NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "Alerta",
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub

    Sub EstiloDgCB()
        With Me.DtGInfCB
            .ReadOnly = True
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            .ReadOnly = True
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try
                .Columns("articulo").HeaderText = "Cod. Artículo"
                .Columns("articulo").Width = 120
                .Columns("articulo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("articulo").ReadOnly = True

                .Columns("Categoria").HeaderText = "Categoría"
                .Columns("Categoria").Width = 40

                .Columns("Descripcion").HeaderText = "Descripción"
                .Columns("Descripcion").Width = 200

                .Columns("Linea").HeaderText = "Línea"
                .Columns("Linea").Width = 80

                .Columns("Proveedor").HeaderText = "Proveedor"
                .Columns("Proveedor").Width = 5

                .Columns("BARCODE_INTERNO").HeaderText = "Código interno"
                .Columns("BARCODE_INTERNO").Width = 100

                .Columns("BARCODE_PZI").HeaderText = "Código de pieza"
                .Columns("BARCODE_PZI").Width = 100

                .Columns("BARCODE_CJI").HeaderText = "Código de caja"
                .Columns("BARCODE_CJI").Width = 100

                .Columns("BARCODE_TRI").HeaderText = "Código de tarima"
                .Columns("BARCODE_TRI").Width = 100

                .Columns("BARCODE_PZE").HeaderText = "Código de proveedor"
                .Columns("BARCODE_PZE").Width = 100

                .Columns("CantPzasBL").HeaderText = "Cantidad Piezas Bolsa"
                .Columns("CantPzasBL").Width = 100
                .Columns("CantPzasBL").Visible = False

                .Columns("CantPzasCJ").HeaderText = "Cantidad Piezas Caja"
                .Columns("CantPzasCJ").Width = 100
                .Columns("CantPzasCJ").Visible = False

                .Columns("CantPzasTR").HeaderText = "Cantidad Piezas Tarima"
                .Columns("CantPzasTR").Width = 100
                .Columns("CantPzasTR").Visible = False



                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            Catch ex As Exception

            End Try
        End With
    End Sub

    Private Sub Cargainf(renglon As Integer)
        If renglon >= 0 Then
            Dim row As DataGridViewRow = DtGInfCB.Rows(renglon)

            txtArticulo.Text = row.Cells("articulo").Value
            txtArt_CodAlterno.Text = row.Cells("articulo").Value
            txtCategoria.Text = row.Cells("Categoria").Value
            txtLinea.Text = row.Cells("Linea").Value
            txtDescripcion.Text = row.Cells("Descripcion").Value
            txtDesc_CodAlterno.Text = row.Cells("Descripcion").Value
            If IsDBNull(row.Cells("Proveedor").Value) Then
                txtProveedor.Text = ""
            Else
                txtProveedor.Text = row.Cells("Proveedor").Value
            End If
            txtCBInterno.Text = row.Cells("BARCODE_INTERNO").Value
            txtCBPieza.Text = row.Cells("BARCODE_PZI").Value
            txtCBBolsa.Text = row.Cells("BARCODE_BLI").Value
            txtCBCaja.Text = row.Cells("BARCODE_CJI").Value
            txtCBTarima.Text = row.Cells("BARCODE_TRI").Value
            If IsDBNull(row.Cells("BARCODE_PZE").Value) Then
                txtCBProveedor.Text = ""
            Else
                txtCBProveedor.Text = row.Cells("BARCODE_PZE").Value
            End If

            txtCantBolsa.Text = row.Cells("CantPzasBL").Value
            txtCantCaja.Text = row.Cells("CantPzasCJ").Value
            txtCantTR.Text = row.Cells("CantPzasTR").Value

            NuevoCódigoDeBarrasToolStripMenuItem.Enabled = True
            ModificarCódigoToolStripMenuItem.Enabled = True
            GrabarToolStripMenuItem.Enabled = False

            Consultar_CodAlt()
        End If
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub LlenaGridSeleccion()
        Try
            SQL.conectarTPM()
            Dim DsSeleccion As New DataSet
            Dim Consulta As String = "SELECT t1.ItemCode, t0.Categoria, t1.ItemName, t2.ItmsGrpNam, 
                                CASE WHEN t1.CardCode IS NULL THEN '' ELSE t1.CardCode END CardCode,
                                CASE WHEN t3.CardName IS NULL THEN '' ELSE t3.CardName END CardName,
                                t0.BARCODE_INTERNO, t0.BARCODE_BLI, t0.BARCODE_PZI, t0.BARCODE_CJI, t0.BARCODE_TRI, t0.BARCODE_PZE 
                                FROM Barcode t0 FULL JOIN SBO_TPD.dbo.OITM t1 ON t0.articulo = t1.ItemCode COLLATE Modern_Spanish_CI_AS
                                INNER JOIN SBO_TPD.dbo.OITB t2 ON t1.ItmsGrpCod = t2.ItmsGrpCod
                                FULL JOIN SBO_TPD.dbo.OCRD t3 ON t1.CardCode = t3.CardCode
                                WHERE t2.ItmsGrpNam NOT IN ('INACTIVOS', 'OTROS SERVICIOS')
                                AND t0.BARCODE_INTERNO IS NULL 
                                ORDER BY t2.ItmsGrpNam, t1.ItemCode"

            Dim conec = New SqlConnection(StrTpm)
            Dim cmd = New SqlCommand(Consulta, conec)
            'cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandType = CommandType.Text

            conec.Open()
            Dim adaptador = New SqlDataAdapter()
            adaptador.SelectCommand = cmd
            adaptador.SelectCommand.Connection = conec
            adaptador.SelectCommand.CommandTimeout = 10000
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
            conec.Close()
            adaptador.Fill(DsSeleccion)

            DsSeleccion.Tables(0).TableName = "CodigosNuevos"

            DvSeleccion.Table = DsSeleccion.Tables("CodigosNuevos")

            dgSeleccion.DataSource = DvSeleccion
            EstiloSeleccion()

            SQL.Cerrar()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error al ejecutar la consulta")
        End Try
    End Sub

    Sub EstiloSeleccion()
        With Me.dgSeleccion
            .ReadOnly = True
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            .ReadOnly = True
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try
                .Columns("ItemCode").HeaderText = "Cod. Artículo"
                .Columns("ItemCode").Width = 120
                .Columns("ItemCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("ItemCode").ReadOnly = True

                .Columns("ItemName").HeaderText = "Descripción"
                .Columns("ItemName").Width = 200

                .Columns("ItmsGrpNam").HeaderText = "Línea"
                .Columns("ItmsGrpNam").Width = 80

                .Columns("CardCode").HeaderText = "Cve. Proveedor"
                .Columns("CardCode").Width = 5
                .Columns("CardCode").Visible = False

                .Columns("CardName").HeaderText = "Proveedor"
                .Columns("CardName").Width = 5

                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            Catch ex As Exception
                'MessageBox.Show(ex.ToString, "Error al dar formato a DataGridView")
            End Try
        End With
    End Sub

    Private Sub Consultar_CodAlt()
        Dim ConsutaLista As String
        ' Dim codigoCliente As String

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)

            Dim dvClientes As New DataView
            Dim DSetTablas As New DataSet
            ConsutaLista = "select Barcode_alterno from Barcode_alterno where articulo = '" & txtArt_CodAlterno.Text & "'"
            Dim daClientes As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            'Dim DSetTablas As New DataSet
            daClientes.Fill(DSetTablas, "Codigos")
            dvClientes = DSetTablas.Tables("Codigos").DefaultView
            cmbCodigoAlterno.DataSource = DSetTablas.Tables("Codigos")
            cmbCodigoAlterno.DisplayMember = "Barcode_alterno"

        End Using


    End Sub




    Private Sub ObtenerArticuloAlterno()

        Try

            Dim con As New SqlConnection
            Dim cmd As New SqlCommand
            Dim CadenaSQL As String = ""

            CadenaSQL = "INSERT INTO Barcode_alterno (articulo,Barcode_alterno) VALUES ( "
            CadenaSQL = CadenaSQL & "'" & txtArt_CodAlterno.Text & "',"
            CadenaSQL = CadenaSQL & "'" & TextBox4.Text & "')"
            con.ConnectionString = SQL.StrTpm
            con.Open()
            cmd.Connection = con
            cmd.CommandText = CadenaSQL
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            MessageBox.Show("El Código está asignado a otro artículo")
        End Try

    End Sub


    Private Sub ArticuloSeleccionado(renglon As Integer, Origen As String)
        If renglon >= 0 Then
            Dim row As DataGridViewRow = dgSeleccion.Rows(renglon)
            Dim sConsecutivo As String

            If Origen = "INDIVIDUAL" Then
                'Omito la linea de abajo porque ahora se que si el articulo no tiene provedor entonces se le conloca "N000"
                If IsDBNull(row.Cells("CardCode").Value) Or IsNothing(row.Cells("CardCode").Value) Then Exit Sub
            End If

            txtArticulo.Text = row.Cells("ItemCode").Value
            txtCategoria.Text = ""
            txtLinea.Text = row.Cells("ItmsGrpNam").Value
            If IsDBNull(row.Cells("ItemName").Value) Then
                txtDescripcion.Text = ""
            Else
                txtDescripcion.Text = (row.Cells("ItemName").Value)
            End If

            'txtDescripcion.Text = row.Cells("ItemName").Value


            lblCveProveedor.Text = row.Cells("CardCode").Value 'Este solo se usa para genere el codigo de barras
            txtProveedor.Text = row.Cells("CardName").Value

            'Algoritmo para creacion de codigos de barra

            Dim reg As New Regex("[^a-zA-Z]")
            Dim CBParte1, CBParte2, CBParte3, CBCompleto As String

            'Debo omitir caracteres especiales
            CBParte1 = Mid(reg.Replace(txtLinea.Text, ""), 1, 4)
            CBParte1 = Rellena(CBParte1, "0", 4, "D")
            '--Los ultimos 5 digitos de la derecha del ItemCode
            CBParte2 = CBPaso2(txtArticulo.Text)
            ' Si es menos de 5 digitos relleno
            CBParte2 = Rellena(CBParte2, "0", 5, "I")

            'Genero la parte 3
            CBParte3 = CBPaso3(lblCveProveedor.Text)

            'Busco o reviso si ya existe un articulo 
            sConsecutivo = BuscoConsecutivo(CBParte1, CBParte2, CBParte3)

            CBCompleto = CBParte1 & sConsecutivo & CBParte2 & CBParte3

            txtCBInterno.Text = CBCompleto
            txtCBPieza.Text = CBCompleto & "PZ"
            txtCBBolsa.Text = CBCompleto & "BL"
            txtCBCaja.Text = CBCompleto & "CJ"
            txtCBTarima.Text = CBCompleto & "TR"

            If Origen = "TODOS" Then
                'Grabo la informacion en la base de datos
                Try
                    If txtArticulo.Text.ToString.Trim <> "" Then
                        Dim con As New SqlConnection
                        Dim cmd As New SqlCommand
                        Dim CadenaSQL As String = ""

                        CadenaSQL = "INSERT INTO Barcode (articulo, Categoria, Descripcion, Linea, Proveedor, Consecutivo, "
                        CadenaSQL = CadenaSQL & " BARCODE_INTERNO, BARCODE_BLI, BARCODE_PZI, BARCODE_BLI, BARCODE_CJI, BARCODE_TRI, BARCODE_PZE)"
                        CadenaSQL = CadenaSQL & " VALUES ("
                        CadenaSQL = CadenaSQL & "'" & txtArticulo.Text & "',"
                        CadenaSQL = CadenaSQL & "'" & txtCategoria.Text & "',"
                        CadenaSQL = CadenaSQL & "'" & txtDescripcion.Text & "',"
                        CadenaSQL = CadenaSQL & "'" & txtLinea.Text & "',"
                        CadenaSQL = CadenaSQL & "'" & txtProveedor.Text & "',"
                        CadenaSQL = CadenaSQL & CInt(sConsecutivo) & ","
                        CadenaSQL = CadenaSQL & "'" & txtCBInterno.Text & "',"
                        CadenaSQL = CadenaSQL & "''," 'Por el momento no se usa esto
                        CadenaSQL = CadenaSQL & "'" & txtCBPieza.Text & "',"
                        CadenaSQL = CadenaSQL & "'" & txtCBBolsa.Text & "',"
                        CadenaSQL = CadenaSQL & "'" & txtCBCaja.Text & "',"
                        CadenaSQL = CadenaSQL & "'" & txtCBTarima.Text & "',"
                        CadenaSQL = CadenaSQL & "'" & txtCBProveedor.Text & "')"

                        con.ConnectionString = SQL.StrTpm
                        con.Open()
                        cmd.Connection = con
                        cmd.CommandText = CadenaSQL
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End If
                Catch
                    'Error
                End Try
            Else
                txtCBProveedor.Text = ""
                NuevoCódigoDeBarrasToolStripMenuItem.Enabled = True
                ModificarCódigoToolStripMenuItem.Enabled = False
                GrabarToolStripMenuItem.Enabled = True
                PanelSeleccion.Visible = False
            End If
        End If
    End Sub

    Private Function BuscoConsecutivo(Parte1 As String, Parte2 As String, Parte3 As String) As String
        Try
            Dim Cons As Integer = -1
            Dim sCons As String
            Dim strSelect As String
            Dim Continua As Boolean = True

            Dim con As New SqlConnection(StrTpm)
            Dim cmd As New SqlCommand()

            SQL.conectarTPM()

            sCons = "00"

            While Continua
                '1. Inicio con 00 para el consecutivo
                strSelect = "SELECT MAX(Consecutivo) AS Ultimo FROM Barcode WHERE BARCODE_INTERNO = '" & Parte1 & sCons & Parte2 & Parte3 & "'"

                cmd.Connection = con
                cmd.CommandType = CommandType.Text
                cmd.CommandText = strSelect
                con.Open()
                Dim dReader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                While dReader.Read()
                    If IsDBNull(dReader("Ultimo")) Then
                        Continua = False
                    Else
                        Cons = CInt(dReader("Ultimo"))
                        Cons = Cons + 1
                        sCons = Rellena(CStr(Cons), "0", 2, "I")
                    End If
                End While


                ''Sumo en 1 el consecutivo y luego a la parte 2 para que continue  
                'If Cons >= 0 Then
                '  Cons = Cons + 1
                'Else
                '  Cons = 0
                'End If
                'sCons = Rellena(CStr(Cons), "0", 2, "I")
                dReader.Close()
            End While

            con.Close()
            SQL.Cerrar()

            Return CStr(sCons)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error al ejecutar la consulta")
        End Try
    End Function

    Private Function CBPaso2(ItemCode As String) As String
        'Obtener los ultimos 5 digitos de derecha a izquierda
        Dim Ciclo, cont As Integer
        Dim tmp As String = ItemCode
        Dim tmp2 As String
        Dim reg As New Regex("[^0-9]")
        tmp = reg.Replace(tmp, "")
        cont = 0
        For Ciclo = Len(tmp) To 1 Step -1
            tmp2 = Mid(tmp, Ciclo, 1) & tmp2
            cont = cont + 1
            If cont = 5 Then Exit For
        Next
        Return tmp2
    End Function

    Private Function CBPaso3(CveProveedor As String) As String
        'Obtener los ultimos 5 digitos de derecha a izquierda
        Dim Ciclo, cont As Integer
        Dim tmp As String = CveProveedor
        Dim tmp2 As String
        Dim tmp3 As String
        Dim reg As New Regex("[^0-9]")

        If (tmp = "") Then  'Se estipulo como rgla que si el proveedor es NULL o vacio se le colocara N000
            Return "N000"
        End If

        If (Mid(tmp, 1, 3) = "PIM") Then
            tmp3 = "I"
        Else
            tmp3 = "N"
        End If
        tmp = reg.Replace(tmp, "")
        cont = 0
        For Ciclo = Len(tmp) To 1 Step -1
            tmp2 = Mid(tmp, Ciclo, 1) & tmp2
            cont = cont + 1
            If cont = 3 Then Exit For
        Next
        tmp3 = tmp3 & tmp2
        Return tmp3
    End Function

    Private Function Rellena(texto As String, caracter As String, longitud As Integer, LugarDeRelleno As String) As String
        Dim Ciclo As Integer = 0
        Dim Tmp As String = texto
        For Ciclo = Len(texto) To longitud - 1
            If LugarDeRelleno = "I" Then
                Tmp = caracter & Tmp
            Else
                Tmp = Tmp & caracter
            End If
        Next
        Return Tmp
    End Function

    Private Sub NuevoCódigoDeBarrasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoCódigoDeBarrasToolStripMenuItem.Click
        Panel2.Enabled = False
        PanelSeleccion.Visible = True
        dgSeleccion.Visible = True
        bNuevo = True
        PanelDatos.Enabled = True
        LimpiaPanel()
        GrabarToolStripMenuItem.Enabled = False

        'Debo buscar los codigos que no existen actualmente
        LlenaGridSeleccion()
        PanelSeleccion.Visible = True
        DtGInfCB.Visible = False
    End Sub

    Private Sub ModificarCódigoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModificarCódigoToolStripMenuItem.Click
        bNuevo = False
        PanelDatos.Enabled = True
        GrabarToolStripMenuItem.Enabled = True
        CBInternoAModificar = txtCBInterno.Text
        txtCategoria.Enabled = True
        txtCBInterno.Enabled = True
        txtCBProveedor.Enabled = True
    End Sub

    Private Sub GrabarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GrabarToolStripMenuItem.Click
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim CadenaSQL As String = ""
        Dim iConsecutivo = 0

        'Obtengo consecutivo del codgo crado
        iConsecutivo = CInt(Mid(txtCBInterno.Text, 5, 2))
        Dim cantBolsa As Integer
        Dim cantCaja As Integer
        Dim cantTar As Integer

        If txtCantBolsa.Text = "" Then
            cantBolsa = 0
        Else
            cantBolsa = txtCantBolsa.Text

        End If

        If txtCantCaja.Text = "" Then
            cantCaja = 0
        Else
            cantCaja = txtCantCaja.Text
        End If

        If txtCantTR.Text = "" Then
            cantTar = 0
        Else
            cantTar = txtCantTR.Text
        End If

        If bNuevo Then
            CadenaSQL = "INSERT INTO Barcode (articulo, Categoria, Descripcion, Linea, Proveedor, Consecutivo,BARCODE_INTERNO, BARCODE_PZI, BARCODE_BLI, BARCODE_CJI, BARCODE_TRI, BARCODE_PZE,CantPzasBL,CantPzasCJ,CantPzasTR) "
            'CadenaSQL = " BARCODE_INTERNO, BARCODE_BLI, BARCODE_PZI, BARCODE_BLI, BARCODE_CJI, BARCODE_TRI, BARCODE_PZE,CantPzasBL)"
            CadenaSQL = CadenaSQL & " VALUES ("
            CadenaSQL = CadenaSQL & "'" & txtArticulo.Text & "',"
            CadenaSQL = CadenaSQL & "'" & txtCategoria.Text & "',"
            txtDescripcion.Text = Replace(txtDescripcion.Text, "'", "''")

            CadenaSQL = CadenaSQL & "'" & txtDescripcion.Text & "',"
            CadenaSQL = CadenaSQL & "'" & txtLinea.Text & "',"
            CadenaSQL = CadenaSQL & "'" & txtProveedor.Text & "',"
            CadenaSQL = CadenaSQL & iConsecutivo & ","
            CadenaSQL = CadenaSQL & "'" & txtCBInterno.Text & "',"
            'CadenaSQL = CadenaSQL & "''," 'Por el momento no se usa esto
            CadenaSQL = CadenaSQL & "'" & txtCBPieza.Text & "',"
            CadenaSQL = CadenaSQL & "'" & txtCBBolsa.Text & "',"
            CadenaSQL = CadenaSQL & "'" & txtCBCaja.Text & "',"
            CadenaSQL = CadenaSQL & "'" & txtCBTarima.Text & "',"
            CadenaSQL = CadenaSQL & "'" & txtCBProveedor.Text & "',"
            CadenaSQL = CadenaSQL & cantBolsa & ","
            CadenaSQL = CadenaSQL & cantCaja & ","
            CadenaSQL = CadenaSQL & cantTar & ")"

        Else
            CadenaSQL = "UPDATE Barcode"
            CadenaSQL = CadenaSQL & " SET Categoria = '" & txtCategoria.Text & "',CantPzasBL= " & cantBolsa & ",CantPzasCJ=" & cantCaja & ",CantPzasTR=" & cantTar & ","



            If CBInternoAModificar <> txtCBInterno.Text Then
                CadenaSQL = CadenaSQL & " BARCODE_INTERNO = '" & txtCBInterno.Text & "',"
                CadenaSQL = CadenaSQL & " BARCODE_PZI = '" & txtCBInterno.Text & "PZ" & "',"
                CadenaSQL = CadenaSQL & " BARCODE_CJI = '" & txtCBInterno.Text & "CJ" & "',"
                CadenaSQL = CadenaSQL & " BARCODE_TRI = '" & txtCBInterno.Text & "TR" & "',"
            End If

            CadenaSQL = CadenaSQL & " BARCODE_PZE = '" & txtCBProveedor.Text & "'"
            CadenaSQL = CadenaSQL & " WHERE BARCODE_INTERNO = '" & CBInternoAModificar & "'"
        End If

        Try
            con.ConnectionString = SQL.StrTpm
            con.Open()
            cmd.Connection = con
            cmd.CommandText = CadenaSQL
            cmd.ExecuteNonQuery()
            MessageBox.Show("El registro se grabó correctamente", "CORRECTO")
            GrabarToolStripMenuItem.Enabled = True
            ModificarCódigoToolStripMenuItem.Enabled = False
            Buscar_Codigos()
            LimpiaPanel()
            DtGInfCB.Visible = True
            PanelDatos.Enabled = False
        Catch ex As Exception
            MessageBox.Show("El registro no pudo grabarse correctamente", "ERROR AL GRABAR")
        End Try
    End Sub

    Private Sub TodosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TodosToolStripMenuItem.Click
        Panel2.Enabled = True
        DtGInfCB.Visible = True
        Try
            Dim Ciclo As Integer
            SQL.conectarTPM()
            Dim DsSeleccion As New DataSet
            Dim Consulta As String = "SELECT t1.ItemCode, t0.Categoria, t1.ItemName, t2.ItmsGrpNam, 
                                CASE WHEN t1.CardCode IS NULL THEN '' ELSE t1.CardCode END CardCode, 
                                CASE WHEN t3.CardName IS NULL THEN '' ELSE t3.CardName END CardName,
                                t0.BARCODE_INTERNO, t0.BARCODE_BLI, t0.BARCODE_PZI, t0.BARCODE_CJI, t0.BARCODE_TRI, t0.BARCODE_PZE 
                                FROM Barcode t0 FULL JOIN SBO_TPD.dbo.OITM t1 ON t0.articulo = t1.ItemCode COLLATE Modern_Spanish_CI_AS
                                INNER JOIN SBO_TPD.dbo.OITB t2 ON t1.ItmsGrpCod = t2.ItmsGrpCod
                                FULL JOIN SBO_TPD.dbo.OCRD t3 ON t1.CardCode = t3.CardCode
                                WHERE t2.ItmsGrpNam NOT IN ('INACTIVOS', 'OTROS SERVICIOS') 
                                AND t0.BARCODE_INTERNO IS NULL 
                                ORDER BY t2.ItmsGrpNam, t1.ItemCode"

            Dim conec = New SqlConnection(StrTpm)
            Dim cmd = New SqlCommand(Consulta, conec)
            'cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandType = CommandType.Text

            conec.Open()
            Dim adaptador = New SqlDataAdapter()
            adaptador.SelectCommand = cmd
            adaptador.SelectCommand.Connection = conec
            adaptador.SelectCommand.CommandTimeout = 10000
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
            conec.Close()
            adaptador.Fill(DsSeleccion)

            DsSeleccion.Tables(0).TableName = "CodigosNuevos"

            DvSeleccion.Table = DsSeleccion.Tables("CodigosNuevos")

            dgSeleccion.DataSource = DvSeleccion
            EstiloSeleccion()

            For Ciclo = 0 To DvSeleccion.Count
                ArticuloSeleccionado(Ciclo, "TODOS")
            Next
            'MessageBox.Show("Termino proceso", "Proceso correcto")
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error al ejecutar la consulta")
        End Try

        SQL.Cerrar()
        PanelSeleccion.Visible = False
        dgSeleccion.Visible = False
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        PanelSeleccion.Hide()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        ArticuloSeleccionado(Seleccionado, "INDIVIDUAL")
        DtGInfCB.Visible = True
    End Sub

    Private Sub DtGInfCB_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DtGInfCB.CellContentClick
        TextBox4.Text = ""
        cmbCodigoAlterno.Text = ""

        Cargainf(e.RowIndex)
    End Sub

    Private Sub DtGInfCB_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DtGInfCB.RowEnter
        Cargainf(e.RowIndex)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        BuscaArticulos()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub txtCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged
        BuscaArticulos()
        EstiloDgCB()
    End Sub

    Private Sub txtNombreArticulo_TextChanged(sender As Object, e As EventArgs) Handles txtNombreArticulo.TextChanged
        BuscaArticulos()
        EstiloDgCB()
    End Sub

    Private Sub ModificarCódigoProvedorToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub

    Private Sub AgregarCódigoAlternoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgregarCódigoAlternoToolStripMenuItem.Click
        txtArt_CodAlterno.Enabled = True
        TextBox4.Enabled = True
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox4.Text = "" Then
            MessageBox.Show("Ingrese un código alterno")
        Else
            ObtenerArticuloAlterno()
            TextBox4.Text = ""
            TextBox4.Enabled = False
        End If

        'ExisteCodigoAlterno()
    End Sub

    Private Sub dgSeleccion_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgSeleccion.CellContentClick
        If e.RowIndex > 0 Then
            Seleccionado = e.RowIndex
        End If
    End Sub

    Private Sub dgSeleccion_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgSeleccion.RowEnter
        If e.RowIndex > 0 Then
            Seleccionado = e.RowIndex
        End If
    End Sub
End Class