
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Text
Imports System.Net
Imports System.Data.SqlClient

Public Class Cotizacion

    Private dv As New DataView
    Dim NumOVta As Long
    Private AgregaArt As Integer = 0
    Dim VErrOv As Integer = 0
    Dim VErrCAd As Integer = 0
    Dim VErrClte As Integer = 0
    Dim DvClte As New DataView
    Dim oStrem As New System.IO.MemoryStream
    Dim cryRpt As New ReportDocument
    Dim valor_anterior As Integer = 1
    Dim strTemp As String
    Dim dt As DataTable
    Dim dr As DataRow

    Dim contador As Integer = 0

    Dim vTotIva As Decimal = 0
    Dim vTotSIva As Decimal = 0
    Dim vTotDoc As Decimal = 0

    Dim vNombreGeneral As String

    Private Sub Cotizacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MsgBox(NomUsuario)
        'MsgBox(IdUsuario)
        'MsgBox(vCorreo)
        'MsgBox(vPswmail)
        'MsgBox(vCorreoVta)
        'MsgBox(vCCorreo)

        dt = New DataTable("Tabla")
        dt.Columns.Add("Codigo")
        dt.Columns.Add("LP")
        dr = dt.NewRow()
        dr("Codigo") = 1
        dr("LP") = 1
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Codigo") = 2
        dr("LP") = 2
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Codigo") = 3
        dr("LP") = 3
        dt.Rows.Add(dr)

        If UsrTPM = "MANAGER" Or UsrTPM = "DDORANTES" Then
            dr = dt.NewRow()
            dr("Codigo") = 4
            dr("LP") = 4
            dt.Rows.Add(dr)
        End If

        dr = dt.NewRow()
        dr("Codigo") = 12
        dr("LP") = 10
        dt.Rows.Add(dr)

        ComboBox1.DataSource = dt
        ComboBox1.ValueMember = "Codigo"
        ComboBox1.DisplayMember = "LP"

        ComboBox1.SelectedIndex = 0
        'MsgBox(ComboBox1.Text.ToString)
        'ComboBox1.SelectedItem.ToString
        DisenoGridVCap()
        CkBCliente.Checked = True
        'RBCliente.Checked = True
        TBOtro.Visible = False

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

            Dim ConsutaLista As String
            'ConsutaLista &= "RTRIM(LTRIM(T0.CardName)) + ' - ' + RTRIM(LTRIM(CASE WHEN T0.CardCode IS NULL THEN '' ELSE T0.CardCode END)) + ' - ' + RTRIM(LTRIM(CASE WHEN T0.State1 IS NULL THEN '' ELSE T0.State1 END))  AS Nombre2,"
            ConsutaLista = "SELECT T0.CardCode AS Cliente,T0.CardName AS Nombre,T0.Phone1 AS Telefono,T0.CntctPrsn AS Contacto,T0.Address AS Direccion,T0.BLOCK AS Colonia,"
            ConsutaLista &= "RTRIM(LTRIM(T0.CardCode)) + ' --> ' + RTRIM(LTRIM(T0.CardName)) + ' - ' + RTRIM(LTRIM(CASE WHEN T0.City IS NULL THEN '' ELSE T0.City END)) AS Nombre2,"
            ConsutaLista &= "T0.ZipCode AS CP,T0.City AS Ciudad,T0.State1 AS Estado,T0.Country As Pais,"
            ConsutaLista &= "T1.SlpCode AS IdAgente,T1.SlpName AS Agente,T0.LicTradNum AS Rfc,T0.E_Mail as Mail "
            ConsutaLista &= "FROM OCRD T0 INNER JOIN OSLP T1 ON T0.SlpCode = T1.SlpCode "
            'ConsutaLista &= "WHERE T0.CardType = 'C' AND FROZENFOR <> 'Y' "
            ConsutaLista &= "WHERE T0.CardType = 'C' "
            ConsutaLista &= "ORDER BY CAST(SUBSTRING(T0.CardCode, CHARINDEX('-', T0.CardCode) + 1, LEN(T0.CardCode)) as integer) ASC "


            Dim daCliente As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            Dim dsCte As New DataSet
            daCliente.Fill(dsCte, "Articulos")

            DvClte.Table = dsCte.Tables("Articulos")

            Me.CmbCliente.DataSource = DvClte
            Me.CmbCliente.DisplayMember = "Nombre2"
            Me.CmbCliente.ValueMember = "Cliente"

            CmbCliente.SelectedValue = ""

            'CmbCliente.SelectedIndex = 0


            'ConsutaLista = "SELECT Trnspcode,TrnspName FROM OSHP ORDER BY TrnspName "
            'Dim daEnvio As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            'Dim dsEnvio As New DataSet
            'daEnvio.Fill(dsEnvio)

            'Me.CmbEnvio.DataSource = dsEnvio.Tables(0)
            'Me.CmbEnvio.DisplayMember = "TrnspName"
            'Me.CmbEnvio.ValueMember = "Trnspcode"

            'CmbEnvio.SelectedValue = 0

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

       


        DisenoGridVArt()

        'OBTENCION DE TIPO DE CAMBIO
        Dim sqlConnectionDolar As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=SBO_TPD;User Id=SA;Password=Pr0c3s0.12;")
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
        DGVArt.DataSource = Nothing


        'SE EDITO PARA LAS NONEDAS Y TIPO DE CAMBIO DEL DOLAR: URIEL TORALVA 19/05/2018
        Consulta = "select Rate as Dolar, Currency as MND "
        Consulta &= "into #dolar "
        Consulta &= "FROM ORTT WHERE RateDate = CONVERT (date, GETDATE()) "

        Consulta &= "SELECT T0.[ItemCode] AS Articulo,T1.ItemName AS Descripcion, "
        'Consulta &= "CASE WHEN T1.QryGroup60 = 'Y' THEN T0.Price * (select * From #dolar) ELSE T0.[Price] END AS Precio, "
        Consulta &= "CASE WHEN T1.QryGroup60 = 'Y' THEN T0.Price ELSE T0.[Price] END AS Precio, "
        Consulta &= "T2.[ItmsGrpNam] AS Grupo_Articulo, T2.ItmsGrpCod, "
        Consulta &= "CASE WHEN T3.Price IS NULL OR T3.Price = 0 THEN "
        'SE COMENTO PARA HACER PRUEBA: URIEL TORALVA 18/05/2018
        'Consulta &= "CASE WHEN T1.QryGroup60 = 'Y' THEN T0.[Price] * (select * From #dolar) ELSE T0.[Price] END "
        Consulta &= "CASE WHEN T1.QryGroup60 = 'Y' THEN T0.[Price] * (select Dolar From #dolar) ELSE T0.[Price] END "
        Consulta &= "ELSE T3.Price END AS Price, "
        Consulta &= "CASE WHEN T3.Discount IS NULL THEN 0 ELSE T3.Discount END AS Discount, "
        Consulta &= "CASE WHEN T3.Expand IS NULL THEN 'N' ELSE T3.Expand END AS Expand, "
        'SE COMENTO ESTA LINEA, PARA SOLO AGREGARLE EL DOLAR: URIEL TORALVA18/05/2018
        'Consulta &= "T0.PriceList "
        Consulta &= "T0.PriceList,  "
        'SE AGREGO ESTA LINEA: URIEL TORALVA 18/05/2018
        Consulta &= "CASE WHEN T1.QryGroup60 = 'Y' THEN (select MND From #dolar) ELSE 'MXP' END as MND "
        Consulta &= "FROM ITM1 T0 INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode AND T1.frozenFor <> 'Y' "
        Consulta &= "INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod "
        Consulta &= "LEFT JOIN SPP1 T3 ON T1.ItemCode = T3.ItemCode AND ListNum = 1 AND GETDATE() >= T3.FromDate AND GETDATE() <= T3.ToDate "
        Consulta &= "WHERE T0.Price > 0 AND T1.QryGroup64 <> 'Y' "
        Consulta &= "ORDER BY T2.[ItmsGrpNam],T0.[ItemCode] "

        Consulta &= "drop table #dolar "

        '******************************************************CONSULTA ANTERIOR A LA MODIFICACION DE LOS ARTICULOS EN DOLAR 09/05/2018
        'Consulta &= "SELECT T0.[ItemCode] AS Articulo,T1.ItemName AS Descripcion,T0.[Price] AS Precio ,"
        'Consulta &= "T2.[ItmsGrpNam] AS Grupo_Articulo,T2.ItmsGrpCod,"
        'Consulta &= "CASE WHEN T3.Price IS NULL OR T3.Price = 0 THEN T0.[Price] ELSE T3.Price END AS Price,"
        'Consulta &= "CASE WHEN T3.Discount IS NULL THEN 0 ELSE T3.Discount END AS Discount,"
        'Consulta &= "CASE WHEN T3.Expand IS NULL THEN 'N' ELSE T3.Expand END AS Expand,  T0.PriceList  "
        'Consulta &= "FROM ITM1 T0 INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode AND T1.frozenFor <> 'Y' INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod "
        'Consulta &= "LEFT JOIN SPP1 T3 ON T1.ItemCode = T3.ItemCode AND ListNum = 1 AND GETDATE() >= T3.FromDate AND GETDATE() <= T3.ToDate "
        'Consulta &= "WHERE T0.Price > 0 AND T1.QryGroup64 <> 'Y' ORDER BY T2.[ItmsGrpNam],T0.[ItemCode]"
        '******************************************************

        Dim CmdArts As New SqlClient.SqlCommand(Consulta)
        ''SE CAMBIO LA BD TEMPORALMENTE PARA HACER PRUEBA DE DOLARES
        CmdArts.Connection = New SqlClient.SqlConnection(StrCon)
        CmdArts.Connection.Open()

        Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdArts)
        AdapMObra.Fill(DTArts)

        Dim DsVtasDet As New DataSet

        DTArts.TableName = "Articulos"

        DsVtasDet.Tables.Add(DTArts)

        CmdArts.Connection.Close()
        dv.Table = DsVtasDet.Tables("Articulos")
        Try

            dv.RowFilter = "PriceList = " & ComboBox1.SelectedValue.ToString '& Me.CmbLinea.Text & "%' AND " & _
            '"Articulo like '%" & Me.TxtArticulo.Text & "%' AND " & _
            '"Descripcion like '%" & Me.TxtDes.Text & "%' "

        Catch exc As Exception

            MessageBox.Show("CARACTER NO VALIDO," & Chr(13) & "BORRE EL CARACTER E INTENTE NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "Alerta", _
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


        DisenoGridVArt()
    End Sub

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

            .Columns(3).HeaderText = "$ Precio, LP: " & ComboBox1.Text.ToString()
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
            .Columns(7).Visible = True

            'ANTERIOR URIEL TORALVA 18/05/2018
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
            .Columns(0).DefaultCellStyle.Format = "###.000"

            .Columns(1).HeaderText = "Descripción"
            .Columns(1).Width = 238

            .Columns(2).HeaderText = "$ Precio, LP: " & ComboBox1.Text.ToString()
            .Columns(2).Width = 65
            .Columns(2).DefaultCellStyle.Format = "###,###,###.000"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(3).HeaderText = "Línea"
            .Columns(3).Width = 108

            .Columns(4).Visible = False
            .Columns(5).Visible = False
            .Columns(6).Visible = False
            .Columns(7).Visible = False

            'SE AGREGA PARA LA MONEDA DEL ARTICULO: URIEL TORALVA 19/05/2018
            .Columns(9).HeaderText = "MND"
            .Columns(9).Width = 35





        End With

    End Sub



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
                'Me.DGVCap.Rows.Add(0, DGVArt(0, DGVArt.CurrentRow.Index).Value.ToString(), DGVArt(1, DGVArt.CurrentRow.Index).Value.ToString(),
                'DGVArt(2, DGVArt.CurrentRow.Index).Value, DGVArt(6, DGVArt.CurrentRow.Index).Value, 0, DGVArt(5, DGVArt.CurrentRow.Index).Value,
                'DGVArt(7, DGVArt.CurrentRow.Index).Value, DGVArt(3, DGVArt.CurrentRow.Index).Value, DGVArt(3, DGVArt.CurrentRow.Index).Value)

                Me.DGVCap.Rows.Add(0, DGVArt(0, DGVArt.CurrentRow.Index).Value.ToString(), DGVArt(1, DGVArt.CurrentRow.Index).Value.ToString(),
                0, 0, 0, DGVArt(3, DGVArt.CurrentRow.Index).Value.ToString(),
                DGVArt(9, DGVArt.CurrentRow.Index).Value.ToString(), 0, 0)
                'ANTERIOR URIEL TORALVA 18/05/2018
                'Me.DGVCap.Rows.Add(0, DGVArt(0, DGVArt.CurrentRow.Index).Value.ToString(), DGVArt(1, DGVArt.CurrentRow.Index).Value.ToString(),
                '0, 0, 0, DGVArt(3, DGVArt.CurrentRow.Index).Value.ToString(),
                '0, 0, 0)

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
                Me.DGVCap.Rows.Add(0, DGVArt(0, DGVArt.CurrentRow.Index).Value.ToString(), DGVArt(1, DGVArt.CurrentRow.Index).Value.ToString(),
                0, 0, 0, DGVArt(3, DGVArt.CurrentRow.Index).Value.ToString(),
                DGVArt(9, DGVArt.CurrentRow.Index).Value.ToString(), 0, 0)
                'ANTERIOR URIEL TORALVA 18/05/2018
                'Me.DGVCap.Rows.Add(0, DGVArt(0, DGVArt.CurrentRow.Index).Value.ToString(), DGVArt(1, DGVArt.CurrentRow.Index).Value.ToString(),
                '0, 0, 0, DGVArt(3, DGVArt.CurrentRow.Index).Value.ToString(),
                '0, 0, 0)

                With DGVCap
                    'Establecemos la celda actual
                    .CurrentCell = .Rows(Me.DGVCap.Rows.Count - 1).Cells(0)

                    ' Y la ponemos en modo de edición.
                    .BeginEdit(True)
                End With

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

    End Sub

    Private Sub DGVArt_DoubleClick(sender As Object, e As EventArgs) Handles DGVArt.DoubleClick
        'MsgBox(DGVArt(0, DGVArt.CurrentRow.Index).Value.ToString())
        'MsgBox(DGVArt(1, DGVArt.CurrentRow.Index).Value.ToString())
        'MsgBox(DGVArt(2, DGVArt.CurrentRow.Index).Value.ToString())
        'MsgBox(DGVArt(3, DGVArt.CurrentRow.Index).Value.ToString())
        If AgregaArt = 0 Then
            AgregaArticulo()
        End If
    End Sub


    Private Sub CmbCliente_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbCliente.SelectionChangeCommitted
        'VARIABLE QUE ALMACENA EL CLIENTE PARA PODER COLOCARLO EN EL CORREO DE LA COTIZACIÓN
        Dim num_cli As String
        Try
            If (CmbCliente.SelectedIndex <> -1) Then
                CargaListaPrecios()

                TxtCorreoC.Text = ""
                TxtCorreoAd.Text = ""

                'If CmbCliente.Text <> "" Then
                'TxtCorreoC.Text = ""
                TxtCorreoC.Text = DvClte(CmbCliente.SelectedIndex).Item("Mail").ToString()
                'End If
                'ALMACENA EL NUMERO DEL CLIENTE SI SE SELECCIONA EN EL COMBO DE CLIENTES
                num_cli = DvClte(CmbCliente.SelectedIndex).Item("Cliente").ToString

            End If



        Catch ex As Exception
            MsgBox(ex.Message)
            CmbCliente.SelectedIndex = -1
            CmbCliente.Text = ""
            CmbCliente.SelectedIndex = -1
            MessageBox.Show("Seleccione un cliente de la lista", _
                         "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub TxtComentario_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtComentario.KeyPress
        If Char.IsLower(e.KeyChar) Then

            'Convert to uppercase, and put at the caret position in the TextBox.
            TxtComentario.SelectedText = Char.ToUpper(e.KeyChar)

            e.Handled = True
        End If
    End Sub

    Private Sub TxtArticulo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtArticulo.KeyPress
        If Char.IsLower(e.KeyChar) Then

            'Convert to uppercase, and put at the caret position in the TextBox.
            TxtArticulo.SelectedText = Char.ToUpper(e.KeyChar)

            e.Handled = True
        End If
    End Sub

    Private Sub TxtDes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtDes.KeyPress
        If Char.IsLower(e.KeyChar) Then

            'Convert to uppercase, and put at the caret position in the TextBox.
            TxtDes.SelectedText = Char.ToUpper(e.KeyChar)

            e.Handled = True
        End If
    End Sub

    Private Sub BtnMas_Click(sender As Object, e As EventArgs) Handles BtnMas.Click
        AgregaArticulo()
    End Sub

    Private Sub BtnMenos_Click(sender As Object, e As EventArgs) Handles BtnMenos.Click
        Try
            Me.DGVCap.Rows.Remove(Me.DGVCap.CurrentRow)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub BuscaArticulos()
        Try

            dv.RowFilter = "Grupo_Articulo like '%" & Me.CmbLinea.Text & "%' AND " & _
               "Articulo like '%" & Me.TxtArticulo.Text & "%' AND " & _
               "Descripcion like '%" & Me.TxtDes.Text & "%' AND PriceList = " & ComboBox1.SelectedValue.ToString

            'dv.RowFilter = "PriceList = " & ComboBox1.SelectedItem.ToString()

        Catch exc As Exception

            MessageBox.Show("CARACTER NO VALIDO," & Chr(13) & "BORRE EL CARACTER E INTENTE NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "Alerta", _
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub CmbLinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbLinea.SelectedIndexChanged
        BuscaArticulos()
    End Sub

    Private Sub TxtArticulo_TextChanged(sender As Object, e As EventArgs) Handles TxtArticulo.TextChanged
        BuscaArticulos()
    End Sub

    Private Sub TxtDes_TextChanged(sender As Object, e As EventArgs) Handles TxtDes.TextChanged
        BuscaArticulos()
    End Sub

    Private Sub DGVCap_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVCap.CellEndEdit
        'If DGVArt(7, DGVArt.CurrentRow.Index).Value = "Y" Or DGVCap(7, DGVCap.CurrentRow.Index).Value = "Y" Then

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

        Dim DTSinVta As New DataTable

        Dim cnn As SqlConnection = Nothing

        Dim cmd4 As SqlCommand = Nothing

        Try

            cnn = New SqlConnection(StrTpm)
            'ESTA SE COMENTO POR PRUEBA, REGRESAR AL FINALIZAR
            cmd4 = New SqlCommand("ArticuloBoletin", cnn)
            'cmd4 = New SqlCommand("ArticuloBoletinDolar", cnn)
            'cmd4 = New SqlCommand("ArticuloBoletin3", cnn)

            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@Articulo", DGVCap.Item(1, DGVCap.CurrentRow.Index).Value)
            cmd4.Parameters.AddWithValue("@Cantidad", DGVCap.Item(0, DGVCap.CurrentRow.Index).Value)
            'TEMPORALMENTE
            'cmd4.Parameters.AddWithValue("@Moneda", DGVCap.Item(7, DGVCap.CurrentRow.Index).Value)
            cmd4.Parameters.AddWithValue("@ListaPrecio", ComboBox1.SelectedValue.ToString())


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
                    DGVCap(5, DGVCap.CurrentRow.Index).Value = fila("Price") * DGVCap.CurrentRow.Cells(0).Value  'MULTIPLICA LA CANTIDAD POR EL PRECIO QUE TRAIGA EL PROCEDIMIENTO
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

            'DG1.DataSource = DvVentasCli

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

        DGVCap.CurrentRow.Cells(0).Value = Convert.ToDecimal(DGVCap.CurrentRow.Cells(0).Value)
        TxtArticulo.Focus()
        TxtArticulo.SelectAll()

        DGVCap.CurrentRow.Cells(0).Value = Convert.ToDecimal(DGVCap.CurrentRow.Cells(0).Value)
        TxtArticulo.Focus()
        TxtArticulo.SelectAll()
    End Sub




    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click

        Dim vTotIva As Decimal = 0
        Dim vTotSIva As Decimal = 0
        Dim vTotDoc As Decimal = 0

        If Me.CmbCliente.SelectedValue = "" And TBOtro.Text = "" Then
            MessageBox.Show("Seleccione un cliente", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbCliente.Focus()
            Return
        End If

        If Me.TxtCorreoC.Text = "" And TxtCorreoAd.Text = "" Then
            MessageBox.Show("Agregue email", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtCorreoC.Focus()
            Return
        End If

        If DGVCap.RowCount = 0 Then
            MessageBox.Show("Capture un artículo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

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
            MessageBox.Show("Articulo sin cantidad de piezas", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            With DGVCap
                'Establecemos la celda actual
                '
                .CurrentCell = .Rows(Fila - 1).Cells(0)

                ' Y la ponemos en modo de edición.
                '
                .BeginEdit(True)
            End With

            Return
        End If

        If MessageBox.Show("¿Confirma que desea guardar y enviar esta cotización?", "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then

            ''ESTE CODIGO SE AGREGO
            '**********************
            Dim DTSinVta As New DataTable

            Dim cnn As SqlConnection = Nothing

            Dim cmd4 As SqlCommand = Nothing

            Try

                For i = 0 To DGVCap.RowCount - 1

                    cnn = New SqlConnection(StrTpm)
                    'COMENTADO TEMPORALMENTE
                    cmd4 = New SqlCommand("ArticuloBoletin", cnn)
                    'cmd4 = New SqlCommand("ArticuloBoletinDolar", cnn)

                    cmd4.CommandType = CommandType.StoredProcedure

                    cmd4.Parameters.AddWithValue("@Articulo", DGVCap.Item(1, i).Value)
                    cmd4.Parameters.AddWithValue("@Cantidad", DGVCap.Item(0, i).Value)
                    cmd4.Parameters.AddWithValue("@ListaPrecio", ComboBox1.SelectedValue.ToString())

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
                            DGVCap(5, i).Value = row("Price") * DGVCap.Item(0, i).Value
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

                    'DG1.DataSource = DvVentasCli

                Next

            Catch ex As Exception
                MsgBox("Ocurrio la siguiente excepcion: " & ex.Message)
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

            BtnGuardar.Enabled = False

            LblMensaje.Visible = True
            vTotIva = vTotSIva * 0.16

            vTotDoc = vTotSIva + vTotIva

            'Procedimiento para obtener el número de transacción más actual
            Dim cmdCuenta As New Data.SqlClient.SqlCommand
            Dim FormatWO As String = ""
            cmdCuenta.CommandText = "SELECT MAX(IdCot) FROM Cotizaciones "
            cmdCuenta.CommandType = CommandType.Text
            cmdCuenta.Connection = New Data.SqlClient.SqlConnection(StrTpm)
            cmdCuenta.Connection.Open()
            'NumOVta = IIf(IsDBNull(cmdCuenta.ExecuteScalar()), 0, Val(cmdCuenta.ExecuteScalar()))

            With cmdCuenta
                NumOVta = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
                .Connection.Close()
            End With

            '*********************************************************************************************************************

            NumOVta += 1

            '******************
            Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
            SqlConnection.Open()
            Dim command As New Data.SqlClient.SqlCommand
            Dim transactions As Data.SqlClient.SqlTransaction
            transactions = SqlConnection.BeginTransaction(IsolationLevel.ReadCommitted, "TransProduccion")
            command.Connection = SqlConnection
            command.Transaction = transactions
            Dim contador As Integer = 0
            'Dim cmdMovInv As Data.SqlClient.SqlCommand
            Dim strcadena As String = ""

            Try
                '******************
                strcadena = "INSERT INTO Cotizaciones (IdCot,Fecha,Agente,NumCliente,NomCliente,SubTotal,IVA,Total,Comentarios) VALUES ("
                strcadena &= NumOVta.ToString
                strcadena &= ","
                strcadena &= "@Fecha"
                strcadena &= ",'"
                strcadena &= NomUsuario
                strcadena &= "','"

                If CmbCliente.Text <> "" Then
                    strcadena &= DvClte(CmbCliente.SelectedIndex).Item("Cliente").ToString
                    strcadena &= "','"
                    strcadena &= DvClte(CmbCliente.SelectedIndex).Item("Nombre").ToString
                    strcadena &= "',"
                Else
                    strcadena &= "Cliente: "
                    strcadena &= "','"
                    strcadena &= TBOtro.Text
                    strcadena &= "',"
                End If

                strcadena &= vTotSIva.ToString
                strcadena &= ","
                strcadena &= vTotIva.ToString
                strcadena &= ","
                strcadena &= vTotDoc.ToString
                strcadena &= ",'"
                strcadena &= TxtComentario.Text
                strcadena &= "')"

                command.Parameters.AddWithValue("@Fecha", DateTime.Now)
                command.CommandText = strcadena
                command.ExecuteNonQuery()


                Dim Valcadena As String
                For Each row As DataGridViewRow In Me.DGVCap.Rows

                    contador += 1
                    Valcadena = ""
                    'char(39), es apostrofe (')
                    'char(34), es comillas dobles (")

                    Valcadena = row.Cells(2).Value.Replace(Chr(39), " ")
                    Valcadena = Valcadena.Replace(Chr(34), " ")
                    'MsgBox(NumOVta.ToString)
                    strcadena = "INSERT INTO CotizacionesDet (IdCot,NumLinea,Articulo,DesArt,Precio,Cantidad,DescLin,TotLinea,Linea) VALUES ("
                    strcadena &= NumOVta.ToString
                    strcadena &= ","
                    strcadena &= contador.ToString()

                    strcadena &= ", '"
                    strcadena &= row.Cells(1).Value
                    strcadena &= "',' "
                    strcadena &= Valcadena
                    strcadena &= "', "
                    strcadena &= row.Cells(3).Value
                    strcadena &= ","
                    strcadena &= row.Cells(0).Value
                    strcadena &= ",'"
                    strcadena &= row.Cells(4).Value
                    strcadena &= "',"
                    strcadena &= row.Cells(5).Value
                    strcadena &= ",'"
                    strcadena &= row.Cells(6).Value
                    strcadena &= "')"

                    command.CommandText = strcadena
                    command.ExecuteNonQuery()

                Next
                transactions.Commit()

            Catch exc As Exception
                Try
                    transactions.Rollback("TransProduccion")
                Catch exSql As SqlClient.SqlException
                    If Not transactions.Connection Is Nothing Then
                        MessageBox.Show("AL REALIZAR ROLL BACK," + exSql.Message.ToString, "SQL ERROR!", _
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Try

                MessageBox.Show("NO FUE POSIBLE CREAR COTIZACION," & Chr(13) & "POR FAVOR INTENTELO DE NUEVO..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
                VErrOv = 1

            Finally
                SqlConnection.Close()

            End Try
            SqlConnection.Close()

            If VErrOv = 1 Then
                BtnGuardar.Enabled = True
                LblMensaje.Visible = False
                TxtArticulo.Focus()
                Return
            End If


            '*********************************************************************************************************************
            'Dim contador As Integer = 0
            'Try
            Dim DtOVta As New DataTable("OrdVenta")

            DtOVta.Columns.Add("IdCot", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Fecha", Type.GetType("System.DateTime"))
            DtOVta.Columns.Add("Agente", Type.GetType("System.String"))

            DtOVta.Columns.Add("NumCliente", Type.GetType("System.String"))
            DtOVta.Columns.Add("NomCliente", Type.GetType("System.String"))

            DtOVta.Columns.Add("NumLinea", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Articulo", Type.GetType("System.String"))
            DtOVta.Columns.Add("DesArt", Type.GetType("System.String"))
            DtOVta.Columns.Add("Linea", Type.GetType("System.String"))
            DtOVta.Columns.Add("ListaP", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Precio", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Cantidad", Type.GetType("System.Int32"))

            DtOVta.Columns.Add("DescLin", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Totlinea", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Comen", Type.GetType("System.String"))


            DtOVta.Columns.Add("DocAntIva", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("DocIva", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("DocTotal", Type.GetType("System.Decimal"))

            'SE AGREGAN ESTOS DOS CAMBIOS PARA MONEDA Y TIPO DE DATO: URIEL TORALVA 19/05/2018
            DtOVta.Columns.Add("TipoCambio", Type.GetType("System.String"))
            DtOVta.Columns.Add("Moneda", Type.GetType("System.String"))


            Dim columnas As DataColumnCollection = DtOVta.Columns


            Dim series As String = ""
            Dim _filaTemp As DataRow

            contador = 0

            For Each row As DataGridViewRow In Me.DGVCap.Rows

                contador += 1


                _filaTemp = DtOVta.NewRow()
                _filaTemp(columnas(0)) = NumOVta.ToString
                '_filaTemp(columnas(1)) = "- " & vSerie & NumOVta.ToString
                _filaTemp(columnas(1)) = Date.Now.ToString
                _filaTemp(columnas(2)) = NomUsuario
                '_filaTemp(columnas(4)) = DvClte(CmbCliente.SelectedIndex).Item("IdAgente").ToString
                '_filaTemp(columnas(5)) = DvClte(CmbCliente.SelectedIndex).Item("Agente").ToString

                If CmbCliente.Text <> "" Then
                    _filaTemp(columnas(3)) = DvClte(CmbCliente.SelectedIndex).Item("Cliente").ToString
                    _filaTemp(columnas(4)) = DvClte(CmbCliente.SelectedIndex).Item("Nombre").ToString

                Else
                    _filaTemp(columnas(3)) = "Cliente: "
                    _filaTemp(columnas(4)) = TBOtro.Text
                End If

                _filaTemp(columnas(5)) = contador
                _filaTemp(columnas(6)) = row.Cells(1).Value  'ARTICULO
                _filaTemp(columnas(7)) = row.Cells(2).Value  'DESCRIPCION
                _filaTemp(columnas(8)) = row.Cells(6).Value  'LINEA
                _filaTemp(columnas(9)) = "1"
                _filaTemp(columnas(10)) = row.Cells(3).Value
                _filaTemp(columnas(11)) = row.Cells(0).Value    'CANTIDAD
                _filaTemp(columnas(12)) = row.Cells(4).Value
                _filaTemp(columnas(13)) = System.Convert.ToDecimal(row.Cells(5).Value)    'TotLinea
                _filaTemp(columnas(14)) = TxtComentario.Text.ToString
                _filaTemp(columnas(15)) = vTotSIva
                _filaTemp(columnas(16)) = vTotIva
                _filaTemp(columnas(17)) = vTotDoc
                'SE AGREGAN CAMPOS AL REPORTE CON EL DATASET: URIEL TORALVA 19/05/2018
                _filaTemp(columnas(18)) = txttipo_cambio.Text.ToString     'TIPO DEL CAMBIO AL DIA
                _filaTemp(columnas(19)) = row.Cells(7).Value               'MONEDA USD O MXP

                DtOVta.Rows.Add(_filaTemp)

            Next

            ''**CODIGO AGREGADO PARA ENVIAR CORREO a asistemas@tractopartesdiamante.com.mx
            Dim informe2 As New CrCotizacion

            RepComsultaP.MdiParent = Inicio
            informe2.SetDataSource(DtOVta)

            RepComsultaP.CrVConsulta.ReportSource = informe2

            'RepComsultaP.Show()

            'vNombreGeneral = vSerie & NumOVta.ToString & "_Cotizacion.pdf"
            vNombreGeneral = System.IO.Path.Combine(System.IO.Path.GetTempPath(), vSerie & NumOVta.ToString & "_Cotizacion.pdf")

            informe2.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, vNombreGeneral)
            'informe2.Close()

            'oStrem = CType(informe2.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), System.IO.MemoryStream)
            'informe2.ExportToDisk() esto de por si estaba comentado

            'If TxtCorreoC.Text <> "" And VErrOv = 0 Then
            ParMail()
            'End If
            'Return
            Dim informe As New CrCotizacion

            RepComsultaP.MdiParent = Inicio
            informe.SetDataSource(DtOVta)

            RepComsultaP.CrVConsulta.ReportSource = informe

            'RepComsultaP.Show()

            If System.IO.File.Exists(vNombreGeneral) = True Then
                'MsgBox("ya existe el archivo")
            Else
                informe.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, vNombreGeneral)
            End If

            'oStrem = CType(informe.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), System.IO.MemoryStream)

            ParMail2()


            If TxtCorreoAd.Text <> "" And VErrOv = 0 Then
                Dim informe3 As New CrCotizacion

                RepComsultaP.MdiParent = Inicio
                informe3.SetDataSource(DtOVta)

                RepComsultaP.CrVConsulta.ReportSource = informe3

                If System.IO.File.Exists(vNombreGeneral) = True Then
                    'MsgBox("ya existe el archivo")
                Else
                    informe3.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, vNombreGeneral)
                End If

                'oStrem = CType(informe3.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), System.IO.MemoryStream)

                ParMail3()
            End If

            'Catch exc As Exception
            '    VErrOv = 1

            '    MessageBox.Show("NO FUE POSIBLE ENVIAR EMAIL DE LA COTIZACION," & Chr(13) & "INTENTE ENVIAR EMAIL NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
            '    MessageBoxButtons.OK, MessageBoxIcon.Error)

            'End Try
            Dim vTextmail As String = ""
            If VErrOv = 1 Then

                LblError.Text = vSerie & NumOVta.ToString & " -- " & "No fue posible enviar EMails. Intentelo nuevamente"
                'vTextmail = TxtCorreoC.Text
                DGVArt.Enabled = False
                DGVCap.Enabled = False
                CmbCliente.Enabled = False
                'CmbEnvio.Enabled = False
                TxtComentario.Enabled = False
                BtnMas.Enabled = False
                AgregaArt = 1
                BtnMenos.Enabled = False

                TxtArticulo.Enabled = False
                TxtDes.Enabled = False
                CmbLinea.Enabled = False

                LblMensaje.Visible = False
                BtnGuardar.Enabled = False
                LblError.Visible = True
                BtnEmail.Enabled = True
                'TxtCorreoC.Text = vTextmail
                Return
            End If

            If VErrCAd = 1 Then
                MessageBox.Show("No fue posible enviar la cotizacion al EMail capturado," & Chr(13) & "Verifique el correo electrónico e intente nuevamente..." & Chr(13), "Advertencia", _
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                BtnEmail.Enabled = True
                Return
            Else
                TxtCorreoC.Enabled = False
                TxtCorreoAd.Enabled = False
                'RBCliente.Enabled = False
                'RBOtro.Enabled = False

                CkBCliente.Enabled = False
                CkBOtro.Enabled = False
            End If

            MessageBox.Show("La Cotizacion se creo exitosamente! y se envio por correo electrónico!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LblExito.Text = NumOVta.ToString & " -- " & "Cotizacion Creada y Enviada Exitosamente!"

            LblExito.Visible = True


            'vTextmail = TxtCorreoC.Text
            LblError.Visible = False
            BtnEmail.Enabled = False
            LblMensaje.Visible = False
            BtnGuardar.Enabled = False
            DGVCap.Enabled = False
            DGVArt.Enabled = False
            CmbCliente.Enabled = False
            'CmbEnvio.Enabled = False
            TxtComentario.Enabled = False


            BtnMas.Enabled = False

            AgregaArt = 1
            BtnMenos.Enabled = False

            BtnNvo.Enabled = True
            'TxtCorreoC.Text = vTextmail
            TxtArticulo.Focus()
            TBOtro.Enabled = False

            TxtArticulo.Enabled = False
            TxtDes.Enabled = False
            CmbLinea.Enabled = False


        End If
    End Sub

    Sub ParMail()
        'Dim CCO() As String = {vCorreoVta & "," & vCCorreo}
        Dim CCO() As String = {"tpd@tractopartesdiamante.com.mx"}
        Dim CC() As String = {""}

        Dim PARA() As String = {vCorreo}
        EnviarMail(vCorreo, PARA, "xxx", "xxxx", CC, CCO)
    End Sub

    Sub ParMail2()
        Dim CCO() As String = {""}
        Dim CC() As String = {""}

        Dim PARA() As String = {Trim(TxtCorreoC.Text)}
        EnviarMail2(vCorreo, PARA, "xxx", "xxxx", CC, CCO)
    End Sub

    Sub ParMail3()
        Dim CCO() As String = {""}
        Dim CC() As String = {""}

        Dim PARA() As String = {Trim(TxtCorreoAd.Text)}
        EnviarMail3(vCorreo, PARA, "xxx", "xxxx", CC, CCO)
    End Sub

    Public Sub EnviarMail(ByVal De As String, ByVal Para As String(), ByVal Asunto As String, ByVal Cuerpo As String, Optional ByVal CC As String() = Nothing, Optional ByVal CCO As String() = Nothing)

        Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
        Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Try

            Msg.From = New System.Net.Mail.MailAddress(De, "", System.Text.Encoding.UTF8) ' De quien se envia el Correo

            For Each From As String In Para
                If From <> "" Then Msg.To.Add(From) ' Para quien se Envia
                'Msg.To.Add("asistemas@tractopartesdiamante.com.mx")
            Next


            If CC IsNot Nothing Then
                For Each C As String In CC
                    If C <> "" Then Msg.CC.Add(C)
                    'Msg.CC.Add("asistemas@tractopartesdiamante.com.mx")
                Next
            End If

            If CCO IsNot Nothing Then
                For Each C As String In CCO
                    If C <> "" Then Msg.Bcc.Add(C)
                    'Msg.Bcc.Add("asistemas@tractopartesdiamante.com.mx")
                Next
            End If

            Dim Titulo As String
            'Titulo = "Cotizacion - " & NumOVta.ToString
            Titulo = "Cotizacion - " & NumOVta.ToString & " - Cliente: " & DvClte(CmbCliente.SelectedIndex).Item("Cliente").ToString
            Msg.Subject = Titulo

            'Dim vMensaje As String = "Estimado cliente:" + vbCrLf + vbCrLf + "Adjunto a este mensaje encontrará la cotizacion que usted solicitó el día de hoy." + vbCrLf + vbCrLf + "Saludos cordiales."

            Msg.SubjectEncoding = System.Text.Encoding.UTF8 ' Encriptando el Asunto del Mensaje
            'Msg.Body = vMensaje ' Cuerpo del Mensaje

            Dim vMensaje As String = "<html><head></head><body><h2>Cotización LP: " & ComboBox1.Text.ToString() & "</h2>"
            vMensaje &= "<p>Estimado cliente:</p><p>Adjunto a este mensaje encontrará la cotizacion que usted solicitó el día de hoy.</p><p>Saludos cordiales.</p>"
            vMensaje &= "</body></html>"
            Msg.Body = vMensaje
            'Msg.BodyEncoding = System.Text.Encoding.UTF8 ' Codificando el Cuerpo del Mensaje
            Msg.IsBodyHtml = True ' El Cuerpo del Mensaje no es HTML


            Dim vNomArch As String

            vNomArch = vSerie & NumOVta.ToString & "_Cotizacion.pdf"

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
            'System.IO.File.Delete("hola2.pdf")
            'Dim ArchivoBorrar As String
            'ArchivoBorrar = vNombreGeneral
            'MsgBox(ArchivoBorrar)
            'If System.IO.File.Exists(ArchivoBorrar) = True Then
            '    MsgBox("si existe el archivo")
            '    System.IO.File.Delete(ArchivoBorrar)
            'End If
        Catch exc As Exception

            MessageBox.Show("NO FUE POSIBLE ENVIAR EMAIL DE LA COTIZACION," & Chr(13) & "INTENTE ENVIAR EMAIL NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
            MessageBoxButtons.OK, MessageBoxIcon.Error)
            VErrOv = 1
        Finally
            System.Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub

    Public Sub EnviarMail2(ByVal De As String, ByVal Para As String(), ByVal Asunto As String, ByVal Cuerpo As String, Optional ByVal CC As String() = Nothing, Optional ByVal CCO As String() = Nothing)

        Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
        Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Try

            Msg.From = New System.Net.Mail.MailAddress(De, "", System.Text.Encoding.UTF8) ' De quien se envia el Correo

            For Each From As String In Para
                If From <> "" Then Msg.To.Add(From) ' Para quien se Envia
                'Msg.To.Add("asistemas@tractopartesdiamante.com.mx")
            Next
            'SE USA PARA PRUEBAS DE ENVIOS
            'Msg.To.Add("asistemas@tractopartesdiamante.com.mx")

            If CC IsNot Nothing Then
                For Each C As String In CC
                    If C <> "" Then Msg.CC.Add(C)
                    'Msg.CC.Add("asistemas@tractopartesdiamante.com.mx")
                Next
            End If

            If CCO IsNot Nothing Then
                For Each C As String In CCO
                    If C <> "" Then Msg.Bcc.Add(C)
                    'Msg.Bcc.Add("asistemas@tractopartesdiamante.com.mx")
                Next
            End If

            Dim Titulo As String
            'Titulo = "Cotización - " & NumOVta.ToString
            Titulo = "Cotizacion - " & NumOVta.ToString & " - Cliente: " & DvClte(CmbCliente.SelectedIndex).Item("Cliente").ToString


            Msg.Subject = Titulo

            Dim vMensaje As String = "Estimado cliente:" + vbCrLf + vbCrLf + "Adjunto a este mensaje encontrará la cotización que usted solicitó el día de hoy." + vbCrLf + vbCrLf + "Saludos cordiales."

            Msg.SubjectEncoding = System.Text.Encoding.UTF8 ' Encriptando el Asunto del Mensaje
            Msg.Body = vMensaje ' Cuerpo del Mensaje 
            Msg.BodyEncoding = System.Text.Encoding.UTF8 ' Codificando el Cuerpo del Mensaje
            Msg.IsBodyHtml = False ' El Cuerpo del Mensaje no es HTML


            Dim vNomArch As String

            vNomArch = NumOVta.ToString & "_Cotizacion.pdf"

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

    Public Sub EnviarMail3(ByVal De As String, ByVal Para As String(), ByVal Asunto As String, ByVal Cuerpo As String, Optional ByVal CC As String() = Nothing, Optional ByVal CCO As String() = Nothing)

        Dim Msg As New MailMessage ' Instancia para Manejar el Envio de Archivos
        Dim SMTP As New SmtpClient ' Uso de SMTP para el envio y codificacion de Archivos
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Try

            Msg.From = New System.Net.Mail.MailAddress(De, "", System.Text.Encoding.UTF8) ' De quien se envia el Correo

            For Each From As String In Para
                If From <> "" Then Msg.To.Add(From) ' Para quien se Envia
                'Msg.To.Add("asistemas@tractopartesdiamante.com.mx")
            Next

            If CC IsNot Nothing Then
                For Each C As String In CC
                    If C <> "" Then Msg.CC.Add(C)
                    'Msg.CC.Add("asistemas@tractopartesdiamante.com.mx")
                Next
            End If

            If CCO IsNot Nothing Then
                For Each C As String In CCO
                    If C <> "" Then Msg.Bcc.Add(C)
                    'Msg.Bcc.Add("asistemas@tractopartesdiamante.com.mx")
                Next
            End If

            Dim Titulo As String
            'Titulo = "Cotizacion - " & NumOVta.ToString
            Titulo = "Cotizacion - " & NumOVta.ToString & " - Cliente: " & DvClte(CmbCliente.SelectedIndex).Item("Cliente").ToString


            Msg.Subject = Titulo

            Dim vMensaje As String = "Estimado cliente:" + vbCrLf + vbCrLf + "Adjunto a este mensaje encontrará la cotización que usted solicitó el día de hoy." + vbCrLf + vbCrLf + "Saludos cordiales."

            Msg.SubjectEncoding = System.Text.Encoding.UTF8 ' Encriptando el Asunto del Mensaje
            Msg.Body = vMensaje ' Cuerpo del Mensaje 
            Msg.BodyEncoding = System.Text.Encoding.UTF8 ' Codificando el Cuerpo del Mensaje
            Msg.IsBodyHtml = False ' El Cuerpo del Mensaje no es HTML


            Dim vNomArch As String

            vNomArch = NumOVta.ToString & "_Cotizacion.pdf"

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

    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles BtnImprimir.Click

        If Me.CmbCliente.SelectedIndex = -1 And TBOtro.Text = "" Then
            MessageBox.Show("Seleccione un cliente", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbCliente.Focus()
            Return
        End If

        If DGVCap.RowCount = 0 Then
            MessageBox.Show("Capture un artículo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        'VARIABLE QUE ALMACENA EL TIPO DE CAMBIO DEL DIA: URIEL 2018-05-11
        Dim vTipoCambio As String = txttipo_cambio.Text.ToString
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
            MessageBox.Show("Articulo sin cantidad de piezas", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            With DGVCap
                'Establecemos la celda actual
                '
                .CurrentCell = .Rows(Fila - 1).Cells(0)

                ' Y la ponemos en modo de edición.
                '
                .BeginEdit(True)
            End With

            Return
        End If


        'If MessageBox.Show("¿Confirma que desea enviar esta COTIZACIÓN?", "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
        'BtnGuardar.Enabled = False

        'LblMensaje.Visible = True
        vTotIva = vTotSIva * 0.16

        vTotDoc = vTotSIva + vTotIva

        '*********************************************************************************************************************

        Try
            Dim DtOVta As New DataTable("OrdVenta")

            DtOVta.Columns.Add("IdCot", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Fecha", Type.GetType("System.DateTime"))
            DtOVta.Columns.Add("Agente", Type.GetType("System.String"))

            DtOVta.Columns.Add("NumCliente", Type.GetType("System.String"))
            DtOVta.Columns.Add("NomCliente", Type.GetType("System.String"))

            DtOVta.Columns.Add("NumLinea", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Articulo", Type.GetType("System.String"))
            DtOVta.Columns.Add("DesArt", Type.GetType("System.String"))
            DtOVta.Columns.Add("Linea", Type.GetType("System.String"))
            DtOVta.Columns.Add("ListaP", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Precio", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Cantidad", Type.GetType("System.Int32"))

            DtOVta.Columns.Add("DescLin", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Totlinea", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Comen", Type.GetType("System.String"))


            DtOVta.Columns.Add("DocAntIva", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("DocIva", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("DocTotal", Type.GetType("System.Decimal"))
            'SE AGREGAN ESTOS DOS CAMBIOS PARA MONEDA Y TIPO DE DATO: URIEL TORALVA 19/05/2018
            DtOVta.Columns.Add("TipoCambio", Type.GetType("System.String"))
            DtOVta.Columns.Add("Moneda", Type.GetType("System.String"))

            Dim columnas As DataColumnCollection = DtOVta.Columns


            Dim series As String = ""
            Dim _filaTemp As DataRow

            contador = 0

            For Each row As DataGridViewRow In Me.DGVCap.Rows

                contador += 1


                _filaTemp = DtOVta.NewRow()
                _filaTemp(columnas(0)) = NumOVta.ToString

                _filaTemp(columnas(1)) = Date.Now.ToString
                _filaTemp(columnas(2)) = NomUsuario

                If CmbCliente.Text <> "" Then
                    _filaTemp(columnas(3)) = DvClte(CmbCliente.SelectedIndex).Item("Cliente").ToString
                    _filaTemp(columnas(4)) = DvClte(CmbCliente.SelectedIndex).Item("Nombre").ToString

                Else
                    _filaTemp(columnas(3)) = "Cliente: "
                    _filaTemp(columnas(4)) = TBOtro.Text
                End If


                _filaTemp(columnas(5)) = contador               'NUMERO DE PARTIDAS
                _filaTemp(columnas(6)) = row.Cells(1).Value     'ARTICULO
                _filaTemp(columnas(7)) = row.Cells(2).Value     'DESCRIPCION
                _filaTemp(columnas(8)) = row.Cells(6).Value     'LINEA
                _filaTemp(columnas(9)) = "1"
                _filaTemp(columnas(10)) = row.Cells(3).Value    'PRECIO
                _filaTemp(columnas(11)) = row.Cells(0).Value    'CANTIDAD
                _filaTemp(columnas(12)) = row.Cells(4).Value    'DESCUENTO %
                _filaTemp(columnas(13)) = System.Convert.ToDecimal(row.Cells(5).Value)    'TotLinea
                _filaTemp(columnas(14)) = TxtComentario.Text.ToString
                _filaTemp(columnas(15)) = vTotSIva
                _filaTemp(columnas(16)) = vTotIva
                _filaTemp(columnas(17)) = vTotDoc
                'SE AGREGAN CAMPOS AL REPORTE CON EL DATASET: URIEL TORALVA 19/05/2018
                _filaTemp(columnas(18)) = vTipoCambio           'TIPO DEL CAMBIO AL DIA
                _filaTemp(columnas(19)) = row.Cells(7).Value    'MONEDA USD O MXP

                DtOVta.Rows.Add(_filaTemp)

            Next

            Dim informe As New CrCotizacion

            RepComsultaP.MdiParent = Inicio
            informe.SetDataSource(DtOVta)

            RepComsultaP.CrVConsulta.ReportSource = informe

            RepComsultaP.Show()

        Catch exc As Exception
            VErrOv = 1

            MessageBox.Show("NO FUE POSIBLE ENVIAR EMAIL DE LA COTIZACION," & Chr(13) & "INTENTE ENVIAR EMAIL NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
            MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
        Dim vTextmail As String = ""
        If VErrOv = 1 Then

            LblError.Text = vSerie & NumOVta.ToString & " -- " & "No fue posible enviar EMails. Intentelo nuevamente"
            'vTextmail = TxtCorreoC.Text
            DGVCap.Enabled = False
            CmbCliente.Enabled = False
            'CmbEnvio.Enabled = False
            TxtComentario.Enabled = False
            BtnMas.Enabled = False
            AgregaArt = 1
            BtnMenos.Enabled = False

            LblMensaje.Visible = False
            BtnGuardar.Enabled = False
            LblError.Visible = True
            BtnEmail.Enabled = True
            'TxtCorreoC.Text = vTextmail
            Return
        End If



        'MessageBox.Show("La orden de venta se creo exitosamente! y se envio por correo electrónico!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'LblExito.Text = vSerie & NumOVta.ToString & " -- " & "Orden de Venta Creada y Enviada Exitosamente!"

        'LblExito.Visible = True


        'vTextmail = TxtCorreoC.Text
        'LblError.Visible = False
        'BtnEmail.Enabled = False
        'LblMensaje.Visible = False
        'BtnGuardar.Enabled = False
        'DGVCap.Enabled = False

        'CmbCliente.Enabled = False
        ''CmbEnvio.Enabled = False
        'TxtComentario.Enabled = False


        'BtnMas.Enabled = False

        'AgregaArt = 1
        'BtnMenos.Enabled = False

        'BtnNvo.Enabled = True
        'TxtCorreoC.Text = vTextmail
        'TxtArticulo.Focus()

        'If VErrCAd = 1 Then
        '    MessageBox.Show("No fue posible enviar la cotizacion al EMail capturado," & Chr(13) & "Verifique el correo electrónico e intente nuevamente..." & Chr(13), "Advertencia", _
        '    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    BtnEmail.Enabled = True
        'Else
        '    'TxtCorreoAd.Enabled = False
        'End If

        'End If
    End Sub

    Private Sub TBOtro_Leave(sender As Object, e As EventArgs) Handles TBOtro.Leave
        'TxtComentario.Focus()
    End Sub

    Private Sub CmbCliente_Leave(sender As Object, e As EventArgs) Handles CmbCliente.Leave
        'TxtComentario.Focus()
        TxtCorreoC.Text = ""
        TxtCorreoAd.Text = ""
        Try
            If (CmbCliente.SelectedIndex <> -1) Then
                CargaListaPrecios()

                TxtCorreoC.Text = ""
                TxtCorreoAd.Text = ""

                'If CmbCliente.Text <> "" Then
                'TxtCorreoC.Text = ""
                TxtCorreoC.Text = DvClte(CmbCliente.SelectedIndex).Item("Mail").ToString()
                'End If
            Else
                If (CmbCliente.Text.ToString <> "") Then
                    If (CmbCliente.FindString(CmbCliente.Text.ToString).ToString <> "-1") Then
                        CmbCliente.SelectedIndex = CmbCliente.FindString(CmbCliente.Text.ToString)
                        CargaListaPrecios()

                        TxtCorreoC.Text = ""
                        TxtCorreoAd.Text = ""

                        'If CmbCliente.Text <> "" Then
                        'TxtCorreoC.Text = ""
                        TxtCorreoC.Text = DvClte(CmbCliente.SelectedIndex).Item("Mail").ToString()
                    Else
                        CmbCliente.SelectedIndex = -1
                        'CmbCliente.Text = ""
                        CmbCliente.SelectedIndex = -1
                        
                        'CmbCliente.Text = ""
                        CmbCliente.SelectedIndex = -1
                        'CmbAgteCob.Focus()
                        Return

                    End If

                End If

            End If
            


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtComentario_Leave(sender As Object, e As EventArgs) Handles TxtComentario.Leave
        'TxtCorreoC.Focus()
    End Sub

    Private Sub TxtCorreoC_Leave(sender As Object, e As EventArgs)

    End Sub

    'Private Sub TxtCorreoAd_Leave(sender As Object, e As EventArgs) Handles TxtCorreoAd.Leave
    '    TxtArticulo.Focus()
    'End Sub

    Private Sub BtnNvo_Click(sender As Object, e As EventArgs) Handles BtnNvo.Click
        VErrClte = 0
        VErrCAd = 0
        NumOVta = 0
        LblExito.Visible = False
        LblMensaje.Visible = False
        CmbCliente.Enabled = True
        'CmbEnvio.Enabled = True
        TxtComentario.Enabled = True


        CmbCliente.SelectedValue = -1
        'CmbEnvio.SelectedValue = -1
        TxtComentario.Text = ""

        TxtCorreoC.Text = ""

        TxtArticulo.Text = ""
        TxtDes.Text = ""
        CmbLinea.SelectedIndex = -1

        dv.RowFilter = ""

        BtnMas.Enabled = True
        AgregaArt = 0
        BtnMenos.Enabled = True

        BtnNvo.Enabled = False

        BtnGuardar.Enabled = True

        TxtCorreoAd.Enabled = True
        TxtCorreoC.Enabled = True
        TxtCorreoAd.Text = ""
        DGVArt.Enabled = True

        DGVCap.Rows.Clear()
        DGVCap.Enabled = True
        CmbCliente.Focus()

        TBOtro.Enabled = True

        'RBCliente.Enabled = True
        'RBOtro.Enabled = True

        CkBCliente.Enabled = True
        CkBOtro.Enabled = True

        DGVArt.Columns.Clear()
        DGVArt.Enabled = True
        CargaListaPrecios()

        TxtArticulo.Enabled = True
        TxtDes.Enabled = True
        CmbLinea.Enabled = True

        TBOtro.Text = ""
        CkBCliente.Checked = True
        CkBOtro.Checked = False

        CargaListaPrecios()

    End Sub

    Private Sub BtnEmail_Click(sender As Object, e As EventArgs) Handles BtnEmail.Click
        '*********************************************************************************************************************
        'Procedimiento para obtener el número de transacción más actual
        Dim cmdCuenta As New Data.SqlClient.SqlCommand
        Dim FormatWO As String = ""
        cmdCuenta.CommandText = "SELECT MAX(IdCot) FROM Cotizaciones "
        cmdCuenta.CommandType = CommandType.Text
        cmdCuenta.Connection = New Data.SqlClient.SqlConnection(StrTpm)
        cmdCuenta.Connection.Open()
        'NumOVta = IIf(IsDBNull(cmdCuenta.ExecuteScalar()), 0, Val(cmdCuenta.ExecuteScalar()))

        With cmdCuenta
            NumOVta = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
            .Connection.Close()
        End With

        '*********************************************************************************************************************

        NumOVta += 1

        '******************
        Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
        SqlConnection.Open()
        Dim command As New Data.SqlClient.SqlCommand
        Dim transactions As Data.SqlClient.SqlTransaction
        transactions = SqlConnection.BeginTransaction(IsolationLevel.ReadCommitted, "TransProduccion")
        command.Connection = SqlConnection
        command.Transaction = transactions


        ''ESTE CODIGO SE AGREGO
        '**********************
        Dim DTSinVta As New DataTable

        Dim cnn As SqlConnection = Nothing

        Dim cmd4 As SqlCommand = Nothing

        Try

            For i = 0 To DGVCap.RowCount - 1

                cnn = New SqlConnection(StrTpm)

                cmd4 = New SqlCommand("ArticuloBoletin", cnn)

                cmd4.CommandType = CommandType.StoredProcedure

                cmd4.Parameters.AddWithValue("@Articulo", DGVCap.Item(1, i).Value)
                cmd4.Parameters.AddWithValue("@Cantidad", DGVCap.Item(0, i).Value)
                cmd4.Parameters.AddWithValue("@ListaPrecio", ComboBox1.SelectedValue.ToString())

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
                        DGVCap(5, i).Value = row("Price") * DGVCap.Item(0, i).Value
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

                'DG1.DataSource = DvVentasCli

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


        Try
            Dim DtOVta As New DataTable("OrdVenta")

            DtOVta.Columns.Add("IdCot", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Fecha", Type.GetType("System.DateTime"))
            DtOVta.Columns.Add("Agente", Type.GetType("System.String"))

            DtOVta.Columns.Add("NumCliente", Type.GetType("System.String"))
            DtOVta.Columns.Add("NomCliente", Type.GetType("System.String"))

            DtOVta.Columns.Add("NumLinea", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Articulo", Type.GetType("System.String"))
            DtOVta.Columns.Add("DesArt", Type.GetType("System.String"))
            DtOVta.Columns.Add("Linea", Type.GetType("System.String"))
            DtOVta.Columns.Add("ListaP", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Precio", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Cantidad", Type.GetType("System.Int32"))

            DtOVta.Columns.Add("DescLin", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Totlinea", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Comen", Type.GetType("System.String"))


            DtOVta.Columns.Add("DocAntIva", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("DocIva", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("DocTotal", Type.GetType("System.Decimal"))

            'SE AGREGAN ESTOS DOS CAMBIOS PARA MONEDA Y TIPO DE DATO: URIEL TORALVA 19/05/2018
            DtOVta.Columns.Add("TipoCambio", Type.GetType("System.String"))
            DtOVta.Columns.Add("Moneda", Type.GetType("System.String"))



            Dim columnas As DataColumnCollection = DtOVta.Columns


            Dim series As String = ""
            Dim _filaTemp As DataRow

            contador = 0

            For Each row As DataGridViewRow In Me.DGVCap.Rows

                contador += 1


                _filaTemp = DtOVta.NewRow()
                _filaTemp(columnas(0)) = NumOVta.ToString
                '_filaTemp(columnas(1)) = "- " & vSerie & NumOVta.ToString
                _filaTemp(columnas(1)) = Date.Now.ToString
                _filaTemp(columnas(2)) = NomUsuario
                '_filaTemp(columnas(4)) = DvClte(CmbCliente.SelectedIndex).Item("IdAgente").ToString
                '_filaTemp(columnas(5)) = DvClte(CmbCliente.SelectedIndex).Item("Agente").ToString

                If CmbCliente.Text <> "" Then
                    _filaTemp(columnas(3)) = DvClte(CmbCliente.SelectedIndex).Item("Cliente").ToString
                    _filaTemp(columnas(4)) = DvClte(CmbCliente.SelectedIndex).Item("Nombre").ToString

                Else
                    _filaTemp(columnas(3)) = "Cliente: "
                    _filaTemp(columnas(4)) = TBOtro.Text
                End If

                '_filaTemp(columnas(8)) = CmbEnvio.SelectedValue.ToString
                '_filaTemp(columnas(9)) = CmbEnvio.Text
                '_filaTemp(columnas(10)) = DvClte(CmbCliente.SelectedIndex).Item("Contacto").ToString
                '_filaTemp(columnas(11)) = TxtComentario.Text.ToString
                '_filaTemp(columnas(12)) = DvClte(CmbCliente.SelectedIndex).Item("Direccion").ToString
                '_filaTemp(columnas(13)) = DvClte(CmbCliente.SelectedIndex).Item("Colonia").ToString
                '_filaTemp(columnas(14)) = DvClte(CmbCliente.SelectedIndex).Item("CP").ToString
                '_filaTemp(columnas(15)) = Trim(DvClte(CmbCliente.SelectedIndex).Item("Ciudad").ToString) & ", " & Trim(DvClte(CmbCliente.SelectedIndex).Item("Estado").ToString) & ", " & Trim(DvClte(CmbCliente.SelectedIndex).Item("Pais").ToString) & ", " & Trim(DvClte(CmbCliente.SelectedIndex).Item("CP").ToString)
                '_filaTemp(columnas(16)) = DvClte(CmbCliente.SelectedIndex).Item("Estado").ToString
                '_filaTemp(columnas(17)) = DvClte(CmbCliente.SelectedIndex).Item("Pais").ToString
                '_filaTemp(columnas(18)) = DvClte(CmbCliente.SelectedIndex).Item("Rfc").ToString
                _filaTemp(columnas(5)) = contador
                _filaTemp(columnas(6)) = row.Cells(1).Value
                _filaTemp(columnas(7)) = row.Cells(2).Value
                _filaTemp(columnas(8)) = row.Cells(6).Value
                _filaTemp(columnas(9)) = "1"
                _filaTemp(columnas(10)) = row.Cells(3).Value
                _filaTemp(columnas(11)) = row.Cells(0).Value    'CANTIDAD
                _filaTemp(columnas(12)) = row.Cells(4).Value
                _filaTemp(columnas(13)) = System.Convert.ToDecimal(row.Cells(5).Value)    'TotLinea
                _filaTemp(columnas(14)) = TxtComentario.Text.ToString
                _filaTemp(columnas(15)) = vTotSIva
                _filaTemp(columnas(16)) = vTotIva
                _filaTemp(columnas(17)) = vTotDoc
                'SE AGREGAN CAMPOS AL REPORTE CON EL DATASET: URIEL TORALVA 19/05/2018
                _filaTemp(columnas(18)) = txttipo_cambio.Text.ToString     'TIPO DEL CAMBIO AL DIA
                _filaTemp(columnas(19)) = row.Cells(7).Value               'MONEDA USD O MXP

                DtOVta.Rows.Add(_filaTemp)

            Next

            Dim informe As New CrCotizacion

            RepComsultaP.MdiParent = Inicio
            informe.SetDataSource(DtOVta)

            RepComsultaP.CrVConsulta.ReportSource = informe

            'RepComsultaP.Show()

            oStrem = CType(informe.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), System.IO.MemoryStream)

            ParMail2()


            If TxtCorreoAd.Text <> "" And VErrOv = 0 Then
                Dim informe3 As New CrCotizacion

                RepComsultaP.MdiParent = Inicio
                informe3.SetDataSource(DtOVta)

                RepComsultaP.CrVConsulta.ReportSource = informe3


                oStrem = CType(informe3.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), System.IO.MemoryStream)

                ParMail3()
            End If

        Catch exc As Exception
            VErrOv = 1

            MessageBox.Show("NO FUE POSIBLE ENVIAR EMAIL DE LA COTIZACION," & Chr(13) & "INTENTE ENVIAR EMAIL NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
            MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
        Dim vTextmail As String = ""
        If VErrOv = 1 Then

            LblError.Text = vSerie & NumOVta.ToString & " -- " & "No fue posible enviar EMails. Intentelo nuevamente"
            'vTextmail = TxtCorreoC.Text
            DGVCap.Enabled = False
            CmbCliente.Enabled = False
            'CmbEnvio.Enabled = False
            TxtComentario.Enabled = False
            BtnMas.Enabled = False
            AgregaArt = 1
            BtnMenos.Enabled = False

            LblMensaje.Visible = False
            BtnGuardar.Enabled = False
            LblError.Visible = True
            BtnEmail.Enabled = True
            'TxtCorreoC.Text = vTextmail
            Return
        End If

        If VErrCAd = 1 Then
            MessageBox.Show("No fue posible enviar la cotizacion al EMail capturado," & Chr(13) & "Verifique el correo electrónico e intente nuevamente..." & Chr(13), "Advertencia", _
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            BtnEmail.Enabled = True
            Return
        Else
            TxtCorreoAd.Enabled = False

            TxtCorreoC.Enabled = False
            CkBCliente.Enabled = False
            CkBOtro.Enabled = False
        End If

        MessageBox.Show("La Cotizacion se creo exitosamente! y se envio por correo electrónico!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        LblExito.Text = NumOVta.ToString & " -- " & "Cotizacion Creada y Enviada Exitosamente!"

        LblExito.Visible = True


        'vTextmail = TxtCorreoC.Text
        LblError.Visible = False
        BtnEmail.Enabled = False
        LblMensaje.Visible = False
        BtnGuardar.Enabled = False
        DGVCap.Enabled = False
        DGVArt.Enabled = False

        CmbCliente.Enabled = False
        TBOtro.Enabled = False

        'CmbEnvio.Enabled = False
        TxtComentario.Enabled = False

        'CmbEnvio.Enabled = False
        BtnMas.Enabled = False

        AgregaArt = 1
        BtnMenos.Enabled = False

        BtnNvo.Enabled = True
        'TxtCorreoC.Text = vTextmail
        TxtArticulo.Focus()
        TxtArticulo.Enabled = False
        TxtDes.Enabled = False
        CmbLinea.Enabled = False
        
    End Sub


    Private Sub RBCliente_CheckedChanged(sender As Object, e As EventArgs)

    End Sub


    Private Sub RBOtro_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    'Private Sub RBCliente_Click(sender As Object, e As EventArgs)
    '    If RBCliente.Checked = True Then
    '        CmbCliente.Visible = True

    '        'TxtCorreoC.Text = DvClte(CmbCliente.SelectedIndex).Item("Mail").ToString()
    '        'RBOtro.Checked = False
    '        TBOtro.Text = ""
    '        TBOtro.Visible = False
    '        'CmbCliente.SelectedIndex = 0
    '        'Else
    '        '    CmbCliente.Visible = False
    '        '    CmbCliente.SelectedIndex = 1
    '        '    TxtCorreoC.Text = ""
    '        '    RBOtro.Checked = True
    '        '    TBOtro.Visible = True
    '    End If
    'End Sub

    'Private Sub RBOtro_Click(sender As Object, e As EventArgs)
    '    'If RBOtro.Checked = True Then
    '    CmbCliente.Visible = False
    '    CmbCliente.SelectedValue = ""
    '    'RBOtro.Checked = True
    '    TBOtro.Visible = True
    '    'TBOtro.Text = ""
    '    'TxtCorreoC.Text = ""

    '    'Else
    '    '    CmbCliente.Visible = True
    '    '    RBOtro.Checked = False
    '    '    TBOtro.Text = ""
    '    '    TBOtro.Visible = False
    '    '    CmbCliente.SelectedIndex = 1
    '    'End If
    'End Sub

    Private Sub CBCliente_CheckedChanged(sender As Object, e As EventArgs) Handles CkBCliente.CheckedChanged
        If CkBCliente.Checked = True Then
            CmbCliente.Visible = True

            TBOtro.Text = ""
            TBOtro.Visible = False

            CkBOtro.Checked = False

            TxtCorreoC.Text = ""
            TxtCorreoAd.Text = ""
            'TxtCorreoC.Text = DvClte(CmbCliente.SelectedIndex).Item("Mail").ToString()
            'RBOtro.Checked = False
        Else

        End If
    End Sub

    Private Sub CBOtro_CheckedChanged(sender As Object, e As EventArgs) Handles CkBOtro.CheckedChanged

        If CkBOtro.Checked = True Then

            CkBCliente.Checked = False

            CmbCliente.Visible = False
            CmbCliente.SelectedValue = ""
            'RBOtro.Checked = True
            TBOtro.Visible = True
            'TBOtro.Text = ""
            TxtCorreoC.Text = ""
            TxtCorreoAd.Text = ""

            'Else
            '    CmbCliente.Visible = True
            '    RBOtro.Checked = False
            '    TBOtro.Text = ""
            '    TBOtro.Visible = False
            '    CmbCliente.SelectedIndex = 1
            'End If
        End If
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub ComboBox1_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox1.SelectionChangeCommitted

        If (Integer.Parse(ComboBox1.SelectedValue.ToString()) <> valor_anterior) Then
            'MsgBox("Cambio lista")
            If (DGVCap.RowCount <> 0) Then
                If (MessageBox.Show("Al cambiar de lista se eliminaran todos los articulos que se han cotizado. ¿Confirma que desea cambiar de lista?", _
                        "Por favor, verifica la información", _
                        MessageBoxButtons.YesNo, _
                       MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
                    DGVCap.Rows.Clear()
                    Try

                        dv.RowFilter = "PriceList = " & ComboBox1.SelectedValue.ToString() '& Me.CmbLinea.Text & "%' AND " & _
                        DGVArt.Columns("Precio").HeaderText = "$ Precio, LP: " & ComboBox1.Text.ToString()
                        DGVCap.Columns(3).HeaderText = "$ Precio, LP: " & ComboBox1.Text.ToString()
                        If (TxtArticulo.Text <> "" Or TxtDes.Text <> "" Or CmbLinea.SelectedIndex <> -1) Then
                            BuscaArticulos()

                        End If

                    Catch exc As Exception
                    End Try
                    valor_anterior = Integer.Parse(ComboBox1.SelectedValue.ToString())
                Else
                    ComboBox1.SelectedValue = valor_anterior.ToString
                    Return


                End If
            Else
                Try

                    dv.RowFilter = "PriceList = " & ComboBox1.SelectedValue.ToString() '& Me.CmbLinea.Text & "%' AND " & _
                    DGVArt.Columns("Precio").HeaderText = "$ Precio, LP: " & ComboBox1.Text.ToString()
                    DGVCap.Columns(3).HeaderText = "$ Precio, LP: " & ComboBox1.Text.ToString()
                    If (TxtArticulo.Text <> "" Or TxtDes.Text <> "" Or CmbLinea.SelectedIndex <> -1) Then
                        BuscaArticulos()

                    End If

                Catch exc As Exception
                End Try
                valor_anterior = Integer.Parse(ComboBox1.SelectedValue.ToString())
            End If

        Else
            Return
        End If


        'If (DGVCap.RowCount = 0) Then
        '    MsgBox("No tiene contenido")
        'Else
        '    MsgBox("Si tiene contenido")
        '    DGVCap.Rows.Clear()
        '    'DGVCap.DataSource = Nothing
        '    'DGVCap
        'End If

        'Try

        '    dv.RowFilter = "PriceList = " & ComboBox1.SelectedItem.ToString() '& Me.CmbLinea.Text & "%' AND " & _
        '    DGVArt.Columns("Precio").HeaderText = "$ Precio, LP: " & ComboBox1.SelectedItem.ToString()

        'Catch exc As Exception



        'End Try
    End Sub

    Private Sub CmbLinea_TextChanged(sender As Object, e As EventArgs) Handles CmbLinea.TextChanged
        BuscaArticulos()
    End Sub

    Private Sub CmbCliente_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbCliente.KeyUp
        Try
            If e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
                strTemp = CmbCliente.Text
                If strTemp.Trim.CompareTo(String.Empty) = 0 Then
                    DvClte.RowFilter = String.Empty
                Else
                    Dim strRowFilter As String = String.Concat("Nombre2 LIKE '%", CmbCliente.Text, "%'")
                    DvClte.RowFilter = strRowFilter
                End If
                'CmbCliente.SelectedIndex = -1
                'CmbCliente.Text = strTemp
                'CmbCliente.Text = strTemp
                'CmbCliente.Text = strTemp
                'CmbCliente.Text = strTemp
                'CmbCliente.SelectionStart = strTemp.Length
                'CmbCliente.SelectedIndex = -1
                'CmbCliente.DroppedDown = True
                CmbCliente.Text = ""
                CmbCliente.Text = strTemp
                CmbCliente.SelectionStart = strTemp.Length
                CmbCliente.SelectedIndex = -1
                CmbCliente.DroppedDown = True
                CmbCliente.SelectedIndex = -1
                CmbCliente.Text = ""
                CmbCliente.Text = strTemp
                CmbCliente.SelectionStart = strTemp.Length

            End If



            'DvClte.RowFilter = "Nombre2 like '%" & CmbCliente.Text & "%'"
            'CmbCliente.DroppedDown = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        
    End Sub

    Private Sub CmbCliente_DropDown(sender As Object, e As EventArgs) Handles CmbCliente.DropDown
        'Dim strTemp As String
        Me.Cursor = Cursors.Arrow
        CmbCliente.Text = strTemp
    End Sub
End Class