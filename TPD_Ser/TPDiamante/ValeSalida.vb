Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Text
Imports System.Net

Public Class ValeSalida

    Private dv As New DataView
    Dim NumOVta As Long
    Private AgregaArt As Integer = 0
    Dim VErrOv As Integer = 0
    Dim VErrCAd As Integer = 0
    Dim VErrClte As Integer = 0
    Dim DvClte As New DataView
    Dim oStrem As New System.IO.MemoryStream
    Dim cryRpt As New ReportDocument

    Private Sub ValeSalida_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DisenoGridVCap()

        DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

            Dim ConsutaLista As String

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

        Dim Consulta As String = ""
        Dim strcadena As String = ""
        Dim DTArts As New DataTable


        Consulta = "SELECT T0.[ItemCode] AS Articulo,T1.ItemName AS Descripcion,T2.ItmsGrpCod, "
        Consulta &= "T2.[ItmsGrpNam] AS Grupo_Articulo "
        Consulta &= "FROM ITM1 T0 INNER JOIN OITM T1 ON T0.ItemCode = T1.ItemCode AND T1.frozenFor <> 'Y' INNER JOIN OITB T2 ON T1.ItmsGrpCod = T2.ItmsGrpCod "
        Consulta &= "LEFT JOIN SPP1 T3 ON T1.ItemCode = T3.ItemCode AND ListNum = 1 AND GETDATE() >= T3.FromDate AND GETDATE() <= T3.ToDate "
        Consulta &= "WHERE T0.[PriceList] = 1 AND T0.Price > 0 ORDER BY T2.[ItmsGrpNam],T0.[ItemCode]"


        Dim CmdArts As New SqlClient.SqlCommand(Consulta)

        CmdArts.Connection = New SqlClient.SqlConnection(StrCon)
        CmdArts.Connection.Open()

        Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdArts)
        AdapMObra.Fill(DTArts)


        Dim DsVtasDet As New DataSet

        DTArts.TableName = "Articulos"

        DsVtasDet.Tables.Add(DTArts)

        CmdArts.Connection.Close()
        dv.Table = DsVtasDet.Tables("Articulos")


        'Procedimiento para obtener el número de transacción más actual
        Dim cmdCuenta As New Data.SqlClient.SqlCommand
        Dim FormatWO As String = ""
        cmdCuenta.CommandText = "SELECT MAX(Folio) FROM ValeSalida "
        cmdCuenta.CommandType = CommandType.Text
        cmdCuenta.Connection = New Data.SqlClient.SqlConnection(StrTpm)
        cmdCuenta.Connection.Open()
        'NumOVta = IIf(IsDBNull(cmdCuenta.ExecuteScalar()), 0, Val(cmdCuenta.ExecuteScalar()))

        With cmdCuenta
            NumOVta = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
            .Connection.Close()
        End With

        NumOVta += 1

        TBFolio.Text = NumOVta
        TBFolio.Text = Format(NumOVta, "0000")
        TBFolio.TextAlign = HorizontalAlignment.Right

        DisenoGridVArt()

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
            .Columns(0).Width = 120
            .Columns(0).DefaultCellStyle.Format = "###.00"

            .Columns(1).HeaderText = "Descripción"
            .Columns(1).Width = 235

            .Columns(2).HeaderText = "Cod. Línea"
            .Columns(2).Width = 50
            '.Columns(2).DefaultCellStyle.Format = "###,###,###.000"
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(3).HeaderText = "Línea"
            .Columns(3).Width = 120

            '.Columns(3).Visible = False
            '.Columns(4).Visible = False
            '.Columns(5).Visible = False
            '.Columns(7).Visible = False
        End With
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
            .Columns(0).Width = 55
            .Columns(0).DefaultCellStyle.Format = "###,###,###"
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(0).ReadOnly = False

            .Columns(1).HeaderText = "Artículo"
            .Columns(1).Width = 110

            .Columns(2).HeaderText = "Descripción"
            .Columns(2).Width = 150

            .Columns(3).HeaderText = "Línea"
            .Columns(3).Width = 120
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns(3).Visible = False

            .Columns(4).HeaderText = "Línea"
            .Columns(4).Width = 120
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns(4).Visible = False

        End With
    End Sub

    Private Sub TxtArticulo_TextChanged(sender As Object, e As EventArgs) Handles TxtArticulo.TextChanged
        BuscaArticulos()
    End Sub

    Private Sub TxtDes_TextChanged(sender As Object, e As EventArgs) Handles TxtDes.TextChanged
        BuscaArticulos()
    End Sub

    Sub BuscaArticulos()
        Try

            dv.RowFilter = "Grupo_Articulo like '%" & Me.CmbLinea.Text & "%' AND " & _
               "Articulo like '%" & Me.TxtArticulo.Text & "%' AND " & _
               "Descripcion like '%" & Me.TxtDes.Text & "%' "

        Catch exc As Exception

            MessageBox.Show("CARACTER NO VALIDO," & Chr(13) & "BORRE EL CARACTER E INTENTE NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "Alerta", _
          MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub

    Private Sub CmbLinea_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbLinea.KeyUp
        BuscaArticulos()
    End Sub

    Private Sub CmbLinea_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbLinea.SelectionChangeCommitted
        BuscaArticulos()
    End Sub


    Private Sub CmbLinea_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbLinea.Validating
        BuscaArticulos()
    End Sub

    Private Sub CmbLinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbLinea.SelectedIndexChanged
        BuscaArticulos()
    End Sub

    Private Sub BtnMas_Click(sender As Object, e As EventArgs) Handles BtnMas.Click
        AgregaArticulo()
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

            'Cantidad,Articulo,Descrpcion,
            'Precio, DescProm, Importe, Price, 
            'Expand, Linea
            Try
                Me.DGVCap.Rows.Add(0, DGVArt(0, DGVArt.CurrentRow.Index).Value.ToString(), DGVArt(1, DGVArt.CurrentRow.Index).Value.ToString(),
                DGVArt(2, DGVArt.CurrentRow.Index).Value, DGVArt(3, DGVArt.CurrentRow.Index).Value)
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
                DGVArt(2, DGVArt.CurrentRow.Index).Value, DGVArt(3, DGVArt.CurrentRow.Index).Value)

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

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
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
            vTotSIva += row.Cells(0).Value
            If row.Cells(0).Value = 0 Then      'si la cantidad en algun renglon es 0 marca error "ARTICULO SIN CANTIDAD"
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


        If MessageBox.Show("¿Confirma que desea crear el Vale de salida?", "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then

            'BtnGuardar.Enabled = False

            'LblMensaje.Visible = True
            vTotIva = vTotSIva * 0.16

            vTotDoc = vTotSIva + vTotIva

            'Procedimiento para obtener el número de transacción más actual
            Dim cmdCuenta As New Data.SqlClient.SqlCommand
            Dim FormatWO As String = ""
            cmdCuenta.CommandText = "SELECT MAX(Folio) FROM ValeSalida "
            cmdCuenta.CommandType = CommandType.Text
            cmdCuenta.Connection = New Data.SqlClient.SqlConnection(StrTpm)
            cmdCuenta.Connection.Open()
            'NumOVta = IIf(IsDBNull(cmdCuenta.ExecuteScalar()), 0, Val(cmdCuenta.ExecuteScalar()))

            With cmdCuenta
                NumOVta = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
                .Connection.Close()
            End With

            '*********************************************************************************************************************

            'NumOVta = IIf(IsDBNull(Val(cmdCuenta.ExecuteScalar())), 0, Val(cmdCuenta.ExecuteScalar()))
            'cmdCuenta.Connection.Close()
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

                Dim Valcadena As String
                For Each row As DataGridViewRow In Me.DGVCap.Rows

                    contador += 1
                    Valcadena = ""
                    'char(39), es apostrofe (')
                    'char(34), es comillas dobles (")

                    Valcadena = row.Cells(2).Value.Replace(Chr(39), " ")
                    Valcadena = Valcadena.Replace(Chr(34), " ")

                    strcadena = "INSERT INTO ValeSalida (Folio,ItemCode,ItemName,ItmsGrpNam,Cantidad,Fecha,ComentariosArt,Motivo,ComentariosGen,Entrega) "
                    strcadena &= "VALUES ("
                    strcadena &= NumOVta.ToString
                    strcadena &= ",'"
                    strcadena &= row.Cells(1).Value     'ARTICULO
                    strcadena &= "','"
                    strcadena &= row.Cells(2).Value     'DESCRIPCION
                    strcadena &= "','"
                    strcadena &= row.Cells(4).Value     'LINEA
                    strcadena &= "', "
                    strcadena &= row.Cells(0).Value     'CANTIDAD
                    strcadena &= ",'"
                    strcadena &= Format(Date.Now, "yyyyMMdd")   'FECHA
                    strcadena &= "', '"
                    strcadena &= row.Cells(5).Value     'COMENTARIOSART
                    strcadena &= "','"
                    strcadena &= row.Cells(6).Value     'MOTIVO
                    strcadena &= "','"
                    strcadena &= TxtComentario.Text     'ComentariosGen
                    strcadena &= "','"
                    strcadena &= row.Cells(7).Value     'ENTREGA
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

                MessageBox.Show("NO FUE POSIBLE CREAR VALE DE SALIDA," & Chr(13) & "POR FAVOR INTENTELO DE NUEVO..." & Chr(13) & exc.Message.ToString, "E R R O R ! ! !", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
                VErrOv = 1

            Finally
                SqlConnection.Close()

            End Try
            SqlConnection.Close()

            If VErrOv = 1 Then
                BtnGuardar.Enabled = True
                'LblMensaje.Visible = False
                TxtArticulo.Focus()
                Return
            End If

            MessageBox.Show("Registros guardados correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)

            BtnMas.Enabled = False
            BtnMenos.Enabled = False
            BtnGuardar.Enabled = False
            TxtComentario.Enabled = False
            TxtArticulo.Enabled = False
            TxtDes.Enabled = False
            CmbLinea.Enabled = False

            BtnNvo.Enabled = True

            DGVArt.Enabled = False
            DGVCap.Enabled = False


        End If
    End Sub


    Private Sub DGVArt_DoubleClick(sender As Object, e As EventArgs) Handles DGVArt.DoubleClick
        If AgregaArt = 0 Then
            AgregaArticulo()
        End If
    End Sub

    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles BtnImprimir.Click

        If DGVCap.RowCount = 0 Then
            MessageBox.Show("Capture un artículo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Dim ErrOV As Integer = 0
        Dim vTotIva As Decimal = 0
        Dim vTotSIva As Decimal = 0
        Dim vTotDoc As Decimal = 0
        Dim vSinValor As Integer = 0
        Dim Fila As Integer = 0

        'RECORRE EL GRID PARA VALIDAR QUE NO VAYA NINGUN ARTICULO SIN CANTIDAD
        For Each row As DataGridViewRow In Me.DGVCap.Rows
            Fila += 1
            'vTotSIva += row.Cells(5).Value
            If row.Cells(0).Value = 0 Then
                vSinValor = 1
                Exit For
            End If

        Next
        'VALIDA I ENCONTRO UNA ARTICULO SIN CANTIDAD
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

        'BtnImprimir.Enabled = False
        Try
            Dim DtOVta As New DataTable("OrdVenta")

            DtOVta.Columns.Add("IdOrdVta", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("FchOVta", Type.GetType("System.DateTime"))
            DtOVta.Columns.Add("UsrOVta", Type.GetType("System.String"))
            'DtOVta.Columns.Add("Agente", Type.GetType("System.String"))
            'DtOVta.Columns.Add("NomAgente", Type.GetType("System.String"))
            'DtOVta.Columns.Add("IdCliente", Type.GetType("System.String"))
            'DtOVta.Columns.Add("DesClte", Type.GetType("System.String"))
            'DtOVta.Columns.Add("IdTrnsp", Type.GetType("System.Int32"))
            'DtOVta.Columns.Add("DesTrnsp", Type.GetType("System.String"))
            'DtOVta.Columns.Add("PerCon", Type.GetType("System.String"))
            'DtOVta.Columns.Add("Direccion", Type.GetType("System.String"))
            'DtOVta.Columns.Add("Colonia", Type.GetType("System.String"))
            'DtOVta.Columns.Add("CP", Type.GetType("System.String"))
            'DtOVta.Columns.Add("Ciudad", Type.GetType("System.String"))
            'DtOVta.Columns.Add("Estado", Type.GetType("System.String"))
            'DtOVta.Columns.Add("Pais", Type.GetType("System.String"))
            'DtOVta.Columns.Add("Rfc", Type.GetType("System.String"))
            DtOVta.Columns.Add("NumLinea", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Articulo", Type.GetType("System.String"))
            DtOVta.Columns.Add("DesArt", Type.GetType("System.String"))
            'DtOVta.Columns.Add("NumLinea", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Linea", Type.GetType("System.String"))

            'DtOVta.Columns.Add("ListaP", Type.GetType("System.Int32"))
            'DtOVta.Columns.Add("Precio", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Cantidad", Type.GetType("System.Int32"))
            'DtOVta.Columns.Add("DescLin", Type.GetType("System.Decimal"))
            'DtOVta.Columns.Add("Totlinea", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("Comen", Type.GetType("System.String"))

            'DtOVta.Columns.Add("DocAntIva", Type.GetType("System.Decimal"))
            'DtOVta.Columns.Add("DocIva", Type.GetType("System.Decimal"))
            DtOVta.Columns.Add("DocTotal", Type.GetType("System.Int32"))
            DtOVta.Columns.Add("Serie", Type.GetType("System.String"))


            Dim columnas As DataColumnCollection = DtOVta.Columns


            Dim series As String = ""
            Dim _filaTemp As DataRow







            'For Each row As DataGridViewRow In Me.DGVCap.Rows
            '    Fila += 1
            '    vTotSIva += row.Cells(5).Value
            '    If row.Cells(5).Value = 0 Then
            '        vSinValor = 1
            '        Exit For
            '    End If

            'Next


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

            'vTotDoc = vTotSIva + vTotIva

            Dim contador As Integer = 0
            Try
                vTotDoc = 0
                For i = 0 To DGVCap.RowCount
                    vTotDoc = vTotDoc + DGVCap.Item(0, i).Value
                Next
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try

            For Each row As DataGridViewRow In Me.DGVCap.Rows

                contador = contador + 1

                _filaTemp = DtOVta.NewRow()
                _filaTemp(columnas(0)) = NumOVta.ToString
                '_filaTemp(columnas(1)) = "-  " & vSerie & NumOVta.ToString
                _filaTemp(columnas(1)) = Date.Now.ToString
                _filaTemp(columnas(2)) = UsrTPM
                '_filaTemp(columnas(4)) = DvClte(CmbCliente.SelectedIndex).Item("IdAgente").ToString
                '_filaTemp(columnas(5)) = DvClte(CmbCliente.SelectedIndex).Item("Agente").ToString
                '_filaTemp(columnas(6)) = DvClte(CmbCliente.SelectedIndex).Item("Cliente").ToString
                '_filaTemp(columnas(7)) = DvClte(CmbCliente.SelectedIndex).Item("Nombre").ToString
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
                _filaTemp(columnas(3)) = contador
                '_filaTemp(columnas(4)) = row.Cells(1).Value
                '_filaTemp(columnas(5)) = row.Cells(8).Value
                '_filaTemp(columnas(22)) = row.Cells(2).Value
                '_filaTemp(columnas(23)) = "1"
                _filaTemp(columnas(4)) = row.Cells(1).Value
                _filaTemp(columnas(5)) = row.Cells(2).Value
                _filaTemp(columnas(6)) = row.Cells(4).Value
                _filaTemp(columnas(7)) = row.Cells(0).Value
                _filaTemp(columnas(8)) = TxtComentario.Text
                _filaTemp(columnas(9)) = vTotDoc
                _filaTemp(columnas(10)) = " - " & TBFolio.Text
                '_filaTemp(columnas(27)) = row.Cells(5).Value
                '_filaTemp(columnas(28)) = vTotSIva.ToString
                '_filaTemp(columnas(29)) = vTotIva.ToString
                '_filaTemp(columnas(10)) = row.Cells(0).Value + row.Cells(0).Value

                DtOVta.Rows.Add(_filaTemp)

            Next

            Dim informe As New CrValeSalida

            RepComsultaP.MdiParent = Inicio
            informe.SetDataSource(DtOVta)

            RepComsultaP.CrVConsulta.ReportSource = informe


            RepComsultaP.Show()

        Catch ex As Exception
            ErrOV = 1

            'MsgBox(ex.Message)

            MessageBox.Show("No fue posible mostrar la orden de venta. " & ex.Message, "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub BtnMenos_Click(sender As Object, e As EventArgs) Handles BtnMenos.Click
        Try
            Me.DGVCap.Rows.Remove(Me.DGVCap.CurrentRow)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGVCap_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVCap.CellDoubleClick


    End Sub

    Private Sub BtnNvo_Click(sender As Object, e As EventArgs) Handles BtnNvo.Click
        VErrClte = 0
        VErrCAd = 0
        NumOVta = 0

        TxtArticulo.Enabled = True
        TxtDes.Enabled = True
        CmbLinea.Enabled = True

        TxtArticulo.Text = ""
        TxtDes.Text = ""
        CmbLinea.SelectedIndex = -1

        TxtComentario.Enabled = True
        TxtComentario.Text = ""

        BtnMas.Enabled = True
        BtnMenos.Enabled = True

        DGVArt.Enabled = True
        DGVCap.Enabled = True

        dv.RowFilter = ""


        AgregaArt = 0

        BtnNvo.Enabled = False

        BtnGuardar.Enabled = True

        'TxtCorreoAd.Enabled = True
        'TxtCorreoAd.Text = ""

        DGVCap.Rows.Clear()
        DGVArt.Enabled = True
        DGVCap.Enabled = True
        TxtArticulo.Focus()

        'Procedimiento para obtener el número de transacción más actual
        Dim cmdCuenta As New Data.SqlClient.SqlCommand
        Dim FormatWO As String = ""
        cmdCuenta.CommandText = "SELECT MAX(Folio) FROM ValeSalida "
        cmdCuenta.CommandType = CommandType.Text
        cmdCuenta.Connection = New Data.SqlClient.SqlConnection(StrTpm)
        cmdCuenta.Connection.Open()
        'NumOVta = IIf(IsDBNull(cmdCuenta.ExecuteScalar()), 0, Val(cmdCuenta.ExecuteScalar()))

        With cmdCuenta
            NumOVta = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
            .Connection.Close()
        End With

        NumOVta += 1

        TBFolio.Text = NumOVta
        TBFolio.Text = Format(NumOVta, "0000")
        TBFolio.TextAlign = HorizontalAlignment.Right


    End Sub

    Private Sub DGVCap_DoubleClick(sender As Object, e As EventArgs) Handles DGVCap.DoubleClick
        Try

            VSArticulo = DGVCap.Item(1, DGVCap.CurrentRow.Index).Value
            VSDescripcion = DGVCap.Item(2, DGVCap.CurrentRow.Index).Value
            VSLinea = DGVCap.Item(4, DGVCap.CurrentRow.Index).Value

            VSMotivo = DGVCap.Item(5, DGVCap.CurrentRow.Index).Value
            VSComentarios = DGVCap.Item(6, DGVCap.CurrentRow.Index).Value
            VSEntrega = DGVCap.Item(7, DGVCap.CurrentRow.Index).Value

            PosRen = DGVCap.CurrentRow.Index

            'MsgBox(PosRen)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        'MsgBox(VSArticulo)
        'MsgBox(VSDescripcion)
        'MsgBox(VSLinea)

        Dim form As New ValeSalidaComen
        form.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim form As New ValeSalidaConsulta
        form.MdiParent = Inicio
        'form.ShowDialog()
        form.Show()
    End Sub
End Class