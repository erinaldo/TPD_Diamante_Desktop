Imports System.Data.SqlClient
Public Class PartidasAbiertasCompras
    Dim conexion As SqlConnection

    Dim DSAll As New DataSet

    Dim DVLinea As New DataView
    Dim DVProveedor As New DataView
    Dim DVArticulo As New DataView

    Dim Resultado As New DataView

    Dim ConsutaLista As String = ""
    Dim strTemp As String = ""
    Dim strTemp_proveedor As String = ""
    Dim strTemp_toCombobox1 As String = ""

    Private Sub PartidasAbiertasCompras_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dtFecha As Date = DateSerial(Year(Date.Today), Date.Now.Month, 1)
        Me.DtpFechaIni.Value = dtFecha
        Me.DateTimePicker1.Value = Format(Date.Now, "dd/MM/yyyy")

        Try
            conexion = New SqlConnection(StrCon)
            ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam "
            ConsutaLista &= "SELECT CardCode,CardName, SlpCode, GroupCode FROM OCRD WHERE CardCode not like 'PG%' and CardType <> 'C' and CardName is not null ORDER BY CardName "
            ConsutaLista &= "SELECT ItemCode,ItemName,ItmsGrpCod FROM OITM ORDER BY ItemCode  "
            Dim daGArticulo As New SqlDataAdapter(ConsutaLista, conexion)
            daGArticulo.Fill(DSAll)
            Dim aux_row As DataRow

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            aux_row = DSAll.Tables(0).NewRow
            aux_row("ItmsGrpNam") = "--Ningun Resultado--"
            aux_row("ItmsGrpCod") = 10109
            DSAll.Tables(0).Rows.Add(aux_row)

            aux_row = DSAll.Tables(0).NewRow
            aux_row("ItmsGrpNam") = "TODOS"
            aux_row("ItmsGrpCod") = 999
            DSAll.Tables(0).Rows.Add(aux_row)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            aux_row = DSAll.Tables(1).NewRow
            aux_row("CardName") = "--Ningun Resultado--"
            aux_row("CardCode") = "7777"
            aux_row("slpcode") = 777
            aux_row("groupcode") = 777
            DSAll.Tables(1).Rows.Add(aux_row)

            aux_row = DSAll.Tables(1).NewRow
            aux_row("CardName") = "TODOS"
            aux_row("CardCode") = "TODOS"
            aux_row("slpcode") = 999
            aux_row("groupcode") = 999
            DSAll.Tables(1).Rows.Add(aux_row)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            aux_row = DSAll.Tables(2).NewRow
            aux_row("ItemName") = "--Ningun Resultado2--"
            aux_row("ItemCode") = "--Ningun Resultado2--"
            aux_row("ItmsGrpCod") = 666
            DSAll.Tables(2).Rows.Add(aux_row)

            aux_row = DSAll.Tables(2).NewRow
            aux_row("ItemName") = "TODOS"
            aux_row("ItemCode") = "TODOS"
            aux_row("ItmsGrpCod") = 999
            DSAll.Tables(2).Rows.Add(aux_row)

            DVLinea.Table = DSAll.Tables(0)
            DVLinea.RowFilter = "ItmsGrpCod <> 10109"
            DVProveedor.Table = DSAll.Tables(1)
            DVProveedor.RowFilter = "CardCode <> '7777'"
            DVArticulo.Table = DSAll.Tables(2)
            DVArticulo.RowFilter = "ItmsGrpCod <> 666"

            CmbGrupoArticulo.DataSource = DVLinea
            CmbGrupoArticulo.DisplayMember = "ItmsGrpNam"
            CmbGrupoArticulo.ValueMember = "ItmsGrpCod"
            CmbGrupoArticulo.SelectedValue = 999

            CmbCliente.DataSource = DVProveedor
            CmbCliente.DisplayMember = "CardName"
            CmbCliente.ValueMember = "CardCode"
            CmbCliente.SelectedValue = "TODOS"

            CmbArticulo.DataSource = DVArticulo
            CmbArticulo.DisplayMember = "ItemCode"
            CmbArticulo.ValueMember = "ItemCode"
            CmbArticulo.SelectedValue = "TODOS"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



    End Sub

    Private Sub CmbGrupoArticulo_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbGrupoArticulo.KeyUp
        Try
            If (e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z) Or (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
                strTemp = CmbGrupoArticulo.Text
                If strTemp.Trim.CompareTo(String.Empty) = 0 Then
                    DVLinea.RowFilter = String.Empty
                    DVLinea.RowFilter = "ItmsGrpCod <> 10109"
                Else
                    Dim strRowFilter As String = String.Concat("ItmsGrpNam LIKE '%", CmbGrupoArticulo.Text, "%' and ItmsGrpCod <> 10109 ")
                    DVLinea.RowFilter = strRowFilter
                    'MsgBox(DvLP.Count)
                    If DVLinea.Count = 0 Then
                        DVLinea.RowFilter = "ItmsGrpCod = 10109"
                    End If

                End If

                CmbGrupoArticulo.Text = ""
                CmbGrupoArticulo.Text = strTemp
                CmbGrupoArticulo.SelectionStart = strTemp.Length
                CmbGrupoArticulo.SelectedIndex = -1
                CmbGrupoArticulo.DroppedDown = True
                CmbGrupoArticulo.SelectedIndex = -1
                CmbGrupoArticulo.Text = ""
                CmbGrupoArticulo.Text = strTemp
                CmbGrupoArticulo.SelectionStart = strTemp.Length

            End If

        Catch ex As Exception
            'MsgBox("errror en filtro nuevo " & ex.Message)
        End Try
    End Sub

    Private Sub CmbGrupoArticulo_DropDown(sender As Object, e As EventArgs) Handles CmbGrupoArticulo.DropDown
        Me.Cursor = Cursors.Arrow

        If strTemp <> "" Then
            CmbGrupoArticulo.Text = strTemp
            CmbGrupoArticulo.SelectionStart = strTemp.Length
        End If
        'CBNomEmp.SelectionStart = strTemp.Length
    End Sub

    Private Sub CmbGrupoArticulo_Leave(sender As Object, e As EventArgs) Handles CmbGrupoArticulo.Leave
        'MsgBox("entre")
        Try
            If CmbGrupoArticulo.SelectedIndex.ToString = "-1" Then
                If strTemp <> "" Then
                    CmbGrupoArticulo.Text = strTemp
                    CmbGrupoArticulo.SelectionStart = strTemp.Length
                End If
                CmbGrupoArticulo.SelectedIndex = -1
                DVArticulo.RowFilter = String.Empty
                DVArticulo.RowFilter = "ItmsGrpCod <> 666"
                CmbArticulo.SelectedIndex = -1
                Return
            End If

            If CmbGrupoArticulo.SelectedValue.ToString = "10109" Then
                CmbGrupoArticulo.SelectedIndex = -1
                DVArticulo.RowFilter = String.Empty
                DVArticulo.RowFilter = "ItmsGrpCod <> 666"
                CmbArticulo.SelectedIndex = -1
                CmbGrupoArticulo.Text = strTemp
                CmbGrupoArticulo.SelectionStart = strTemp.Length
                Return
            End If
            filtraArticulos()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmbGrupoArticulo_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbGrupoArticulo.SelectionChangeCommitted
        filtraArticulos()
    End Sub

    Public Sub filtraArticulos()
        Try
            If (CmbGrupoArticulo.SelectedValue.ToString() = "999") Then
                DVArticulo.RowFilter = String.Empty
                DVArticulo.RowFilter = "ItmsGrpCod <> 666"
                CmbArticulo.SelectedValue = "TODOS"

            Else
                DVArticulo.RowFilter = "ItmsGrpCod = " & CmbGrupoArticulo.SelectedValue.ToString & " or ItemCode = 'TODOS' "
                'ComboBox1.SelectedValue = -1
                CmbArticulo.SelectedValue = "TODOS"
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CmbGrupoArticulo.SelectedIndex = -1 Then
            MessageBox.Show("Seleccione una línea", _
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbGrupoArticulo.Focus()
            Return
        End If

        If CmbCliente.SelectedIndex = -1 Then
            MessageBox.Show("Seleccione un Proveedor", _
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbCliente.Focus()
            Return
        End If

        If CmbArticulo.SelectedIndex = -1 Then
            MessageBox.Show("Seleccione una artiuclo", _
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbArticulo.Focus()
            Return
        End If

        Ejecutar_Consulta()
    End Sub

    Private Sub Ejecutar_Consulta()
        Dim cmd4 As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        Dim DsVtas As New DataSet
        Try
            cnn = New SqlConnection(StrTpm)
            cmd4 = New SqlCommand("SPPartidasAbiertasCompras", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            'cmd4.Parameters.AddWithValue("@fecha_inicio", DtpFechaIni.Value)
            'cmd4.Parameters.AddWithValue("@fecha_fin", DateTimePicker1.Value)
            cmd4.Parameters.AddWithValue("@Linea", CmbGrupoArticulo.SelectedValue.ToString)
            cmd4.Parameters.AddWithValue("@Proveedor", CmbCliente.SelectedValue.ToString)
            cmd4.Parameters.AddWithValue("@Articulo", CmbArticulo.SelectedValue.ToString)
            cnn.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 2000
            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            cnn.Close()

            da.Fill(DsVtas)
            Resultado.Table = DsVtas.Tables(0)
            DGVResultado.DataSource = Resultado
            diseno_grid()

        Catch ex As Exception
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

    End Sub

    Private Sub CmbCliente_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbCliente.KeyUp
        Try
            If (e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z) Or (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
                strTemp_proveedor = CmbCliente.Text
                If strTemp_proveedor.Trim.CompareTo(String.Empty) = 0 Then
                    DVProveedor.RowFilter = String.Empty
                    DVProveedor.RowFilter = "CardCode <> '7777'"
                Else
                    Dim strRowFilter As String = String.Concat("CardName LIKE '%", CmbCliente.Text, "%' and CardCode <> '7777' ")
                    DVProveedor.RowFilter = strRowFilter
                    'MsgBox(DvLP.Count)
                    If DVProveedor.Count = 0 Then
                        DVProveedor.RowFilter = " CardCode = '7777'"
                    End If

                End If

                CmbCliente.Text = ""
                CmbCliente.Text = strTemp_proveedor
                CmbCliente.SelectionStart = strTemp_proveedor.Length
                CmbCliente.SelectedIndex = -1
                CmbCliente.DroppedDown = True
                CmbCliente.SelectedIndex = -1
                CmbCliente.Text = ""
                CmbCliente.Text = strTemp_proveedor
                CmbCliente.SelectionStart = strTemp_proveedor.Length

            End If

        Catch ex As Exception
            'MsgBox("errror en filtro nuevo " & ex.Message)
        End Try
    End Sub

    Private Sub CmbArticulo_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbArticulo.KeyUp
        Dim strRowFilter As String = ""
        Try
            'MsgBox(e.KeyValue.ToString)
            'MsgBox(e.KeyCode.ToString)
            If (e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z) Or (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
                strTemp_toCombobox1 = CmbArticulo.Text
                If strTemp_toCombobox1.Trim.CompareTo(String.Empty) = 0 Then

                    If CmbGrupoArticulo.SelectedIndex <> -1 Then
                        If CmbGrupoArticulo.SelectedValue.ToString = "999" Then
                            DVArticulo.RowFilter = String.Empty
                            DVArticulo.RowFilter = "ItmsGrpCod <> 666"

                        Else
                            DVArticulo.RowFilter = String.Empty
                            DVArticulo.RowFilter = "(ItmsGrpCod = " & CmbGrupoArticulo.SelectedValue.ToString & " and ItmsGrpCod <> 666) or ItemCode = 'TODOS' "
                        End If
                    Else
                        DVArticulo.RowFilter = String.Empty
                        DVArticulo.RowFilter = "ItmsGrpCod <> 666"
                    End If

                Else
                    If CmbGrupoArticulo.SelectedIndex <> -1 Then
                        If CmbGrupoArticulo.SelectedValue.ToString = "999" Then
                            strRowFilter = String.Concat("(ItemCode LIKE '%", CmbArticulo.Text, "%' and ItmsGrpCod <> 666)")
                            DVArticulo.RowFilter = strRowFilter

                        Else
                            strRowFilter = String.Concat("(ItemCode LIKE '%", CmbArticulo.Text, "%' and (ItmsGrpCod = ", CmbGrupoArticulo.SelectedValue.ToString, " or ItemCode = 'TODOS') and ItmsGrpCod <> 666) ")
                            DVArticulo.RowFilter = strRowFilter
                        End If
                    Else
                        strRowFilter = String.Concat("(ItemCode LIKE '%", CmbArticulo.Text, "%' and ItmsGrpCod <> 666) ")
                        DVArticulo.RowFilter = strRowFilter
                    End If

                    If DVArticulo.Count = 0 Then
                        DVArticulo.RowFilter = "ItmsGrpCod = 666"
                        'Else
                        '    strRowFilter = String.Concat(strRowFilter, " or SlpCode = 8888 ")
                        '    DvLP2.RowFilter = strRowFilter
                    End If

                End If
                CmbArticulo.Text = ""
                CmbArticulo.Text = strTemp_toCombobox1
                CmbArticulo.SelectionStart = strTemp_toCombobox1.Length
                CmbArticulo.SelectedIndex = -1
                CmbArticulo.DroppedDown = True
                CmbArticulo.SelectedIndex = -1
                CmbArticulo.Text = ""
                CmbArticulo.Text = strTemp_toCombobox1
                CmbArticulo.SelectionStart = strTemp_toCombobox1.Length

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CmbCliente_DropDown(sender As Object, e As EventArgs) Handles CmbCliente.DropDown
        Me.Cursor = Cursors.Arrow

        If strTemp_proveedor <> "" Then
            CmbCliente.Text = strTemp_proveedor
            CmbCliente.SelectionStart = strTemp_proveedor.Length
        End If
    End Sub

    Private Sub CmbArticulo_DropDown(sender As Object, e As EventArgs) Handles CmbArticulo.DropDown
        Me.Cursor = Cursors.Arrow

        If strTemp_toCombobox1 <> "" Then
            CmbArticulo.Text = strTemp_toCombobox1
            CmbArticulo.SelectionStart = strTemp_toCombobox1.Length
        End If
    End Sub

    Private Sub CmbArticulo_Leave(sender As Object, e As EventArgs) Handles CmbArticulo.Leave
        Try
            If CmbArticulo.SelectedIndex.ToString = "-1" Then
                If strTemp_toCombobox1 <> "" Then
                    CmbArticulo.Text = strTemp_toCombobox1
                    CmbArticulo.SelectionStart = strTemp_toCombobox1.Length
                End If
                CmbArticulo.SelectedIndex = -1
                Return
            End If

            If CmbArticulo.SelectedValue.ToString = "--Ningun Resultado--" Then
                CmbArticulo.SelectedIndex = -1
                CmbArticulo.Text = strTemp_toCombobox1
                CmbArticulo.SelectionStart = strTemp_toCombobox1.Length
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmbCliente_Leave(sender As Object, e As EventArgs) Handles CmbCliente.Leave
        Try
            If CmbCliente.SelectedIndex.ToString = "-1" Then
                If strTemp_proveedor <> "" Then
                    CmbCliente.Text = strTemp_proveedor
                    CmbCliente.SelectionStart = strTemp_proveedor.Length
                End If
                CmbCliente.SelectedIndex = -1
                Return
            End If

            If CmbCliente.SelectedValue.ToString = "7777" Then
                CmbCliente.SelectedIndex = -1
                CmbCliente.Text = strTemp_proveedor
                CmbCliente.SelectionStart = strTemp_proveedor.Length
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub diseno_grid()
        With Me.DGVResultado
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try
                .Columns("DocSap").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("ClaveCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Nombre").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Articulo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Descripcion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Linea").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Solicitado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Surtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("CantidadPendiente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns("Precio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Moneda").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns("CantidadPendiente").DefaultCellStyle.Format = "###,###,##0"
                .Columns("Solicitado").DefaultCellStyle.Format = "###,###,###"
                .Columns("Surtido").DefaultCellStyle.Format = "###,###,###"
                .Columns("CantidadPendiente").DefaultCellStyle.Format = "###,###,###"

                .Columns("Precio").DefaultCellStyle.Format = "$ ###,###,###,##0.#0"
                .Columns("Precio").DefaultCellStyle.ForeColor = Color.DarkRed
                .Columns("Precio").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
                .Columns("DocSap").DefaultCellStyle.ForeColor = Color.BlueViolet
                .Columns("DocSap").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)

                .Columns("Solicitado").DefaultCellStyle.ForeColor = Color.DarkBlue
                .Columns("Solicitado").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)

                .Columns("Surtido").DefaultCellStyle.ForeColor = Color.DarkGreen
                .Columns("Surtido").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)

                .Columns("CantidadPendiente").DefaultCellStyle.ForeColor = Color.Red
                .Columns("CantidadPendiente").DefaultCellStyle.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)

                .Columns("DocSap").Width = 70
                .Columns("Fecha").Width = 90
                .Columns("ClaveCliente").Width = 70
                .Columns("NumAtCard").Width = 200
                .Columns("Nombre").Width = 230
                .Columns("Articulo").Width = 110
                .Columns("Descripcion").Width = 230
                .Columns("Linea").Width = 150
                .Columns("CantidadPendiente").Width = 60
                .Columns("Solicitado").Width = 60
                .Columns("Surtido").Width = 60
                .Columns("Precio").Width = 100
                .Columns("Moneda").Width = 70

                .Columns("ClaveCliente").HeaderText = "Proveedor"
                .Columns("CantidadPendiente").HeaderText = "Pendiente"
                .Columns("NumAtCard").HeaderText = "Num. Referencia"
                .Columns("Fecha").HeaderText = "Fecha de Entrega"



                '.Columns("").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                '.Columns("CardCode").HeaderText = "Codigo Cliente"
                '.Columns("Valor").HeaderText = "Valor Aprox. de Vta."
                '.Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("id").Visible = False
                '.Columns("Prioridad").Visible = False
                ''.Columns("ordenFecha").Visible = False
                '.Columns("ordenFecha").DefaultCellStyle.Format = "yyyy-MM-dd"
                '.Columns("ordenFecha").Visible = False
                '.Columns("Veces").Visible = False
                '.Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Hora Comienzo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Hora Fin").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Duracion de Visita").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Comentario").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Pedido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Ciudad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Coordenadas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                ''.Columns("Coordenada Y").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Estado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Valor").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("Comentario").Width = 300
                '.Columns("Nombre").Width = 250
                '.Columns("Direccion").Width = 200
                '.Columns("Coordenadas").Width = 140
                '.Columns("CardCode").Width = 68
                '.Columns("Fecha").Width = 65
                '.Columns("Pedido").Width = 125
                '.Columns("Ciudad").Width = 85
                '.Columns("Estado").Width = 60
                '.Columns("Hora Comienzo").Width = 60
                '.Columns("Hora Fin").Width = 60
                '.Columns("Duracion de Visita").Width = 60
                ''.Columns("Estado").Width = 60

                '.Columns("CardCode").Frozen = True
                '.Columns("Nombre").Frozen = True
                '.Columns("Fecha").Frozen = True
                '.Columns("Hora Comienzo").Frozen = True
                '.Columns("Hora Fin").Frozen = True
                '.Columns("Duracion de Visita").Frozen = True

                '.Columns("Numero de Visita").HeaderText = "Numero de Visita por dia"
                '.Columns("Numero de Visita").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            Catch ex As Exception
                ' MsgBox("trono" & ex.Message)
            End Try
        End With
    End Sub

    Private Sub BtnRecibos_Click(sender As Object, e As EventArgs) Handles BtnRecibos.Click
        'ExportarDatosExcel_PA(DGVResultado, "")
        mGeneraExcel()
        'GridAExcel(DGVResultado)
    End Sub

    Public Sub ExportarDatosExcel_PA(ByVal DataGridView1 As DataGridView, ByVal descripcion_tabla As String)
        Try
            Dim m_Excel As New Microsoft.Office.Interop.Excel.Application
            m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlWait
            m_Excel.Visible = True
            Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook = m_Excel.Workbooks.Add
            Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet = objLibroExcel.Worksheets(1)
            With objHojaExcel
                .Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
                .Activate()
                'Encabezado  
                .Range("A1:L1").Merge()
                .Range("A1:L1").Value = "Tracto Partes Diamante"
                .Range("A1:L1").Font.Bold = True
                .Range("A1:L1").Font.Size = 17
                'Copete  
                .Range("A2:L2").Merge()
                .Range("A2:L2").Value = descripcion_tabla
                .Range("A2:L2").Font.Bold = True
                .Range("A2:L2").Font.Size = 15

                Const primeraLetra As Char = "A"
                Const primerNumero As Short = 3
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
                    If c.Visible Then
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
                        objCelda.EntireColumn.Font.Size = 11
                        'objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format  
                        'If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
                        '    objCelda.EntireColumn.NumberFormat = "#" + sepMil + "0" + sepDec + "00"
                        'End If
                    End If
                Next

                Dim objRangoEncab As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
                objRangoEncab.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
                UltimaLetra = Letra
                Dim UltimaLetraIzq As String = LetraIzq

                'CARGA DE DATOS  
                Dim i As Integer = Numero + 1

                For Each reg As DataGridViewRow In DataGridView1.Rows
                    LetraIzq = ""
                    cod_LetraIzq = Asc(primeraLetra) - 1
                    Letra = primeraLetra
                    cod_letra = Asc(primeraLetra) - 1
                    For Each c As DataGridViewColumn In DataGridView1.Columns
                        If c.Name.ToString = "Borrar" Then
                            Continue For
                        End If
                        If c.Visible Then
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
                            ' acá debería realizarse la carga  
                            If c.Name.ToString = "validado" Then
                                .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", (IIf(reg.Cells(c.Index).Value = "1", "SI", "NO")))
                            Else
                                .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value)
                            End If

                            '.Cells(i, strColumna) = IIf(IsDBNull(reg.(c.DataPropertyName)), c.DefaultCellStyle.NullValue, reg(c.DataPropertyName))  
                            '.Range(strColumna + i, strColumna + i).In()  

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
                    If c.Name.ToString = "Borrar" Then
                        Continue For
                    End If
                    If c.Visible Then
                        objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
                        objCelda.BorderAround()
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

                'Dibujar el border exterior grueso  
                Dim objRango As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
                objRango.Select()
                objRango.Columns.AutoFit()
                objRango.Columns.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
                objHojaExcel.Range("D:D").ColumnWidth = 50
                objHojaExcel.Range("F:F").ColumnWidth = 60

            End With

            objHojaExcel.Rows.Item(3).Font.Bold = 1
            For ee As Integer = 1 To (DataGridView1.Rows.Count + 3)
                objHojaExcel.Rows.Item(ee).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter

            Next
            m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
        End Try

    End Sub

    Private Sub mGeneraExcel()
        Try
            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

            'Añadimos el Libro al programa
            exLibro = exApp.Workbooks.Add

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DGVResultado.ColumnCount
            Dim NRow As Integer = DGVResultado.RowCount

            ''Combinamos celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1:g1").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A2:g2").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A3:g3").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A4:g4").Merge(True)

            ''aplicamos un color de fondo ala celda o rango de celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A4").Interior.ColorIndex = 15

            '************
            'exLibro.Worksheets("Hoja1").Columns("E").NumberFormat = "@" 'Articulo'
            exLibro.Worksheets("Hoja1").Columns("L").NumberFormat = "$ ###,##0.00" 'Articulo'
            exLibro.Worksheets("Hoja1").Columns("J").NumberFormat = "###,###,###" 'Articulo'
            exLibro.Worksheets("Hoja1").Columns("F").NumberFormat = "@" 'Articulo'

            exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 5 'Factura'
            exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 9 'Factura'
            exLibro.Worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 5 'Fecha'
            exLibro.Worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 18 'Fecha cont'
            exLibro.Worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 18 'Articulo'
            exLibro.Worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 10 'Descripcion'
            exLibro.Worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 15 'Linea'
            exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 10 'Cantidad'
            exLibro.Worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 5 'Comentarios'
            exLibro.Worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 5 'Comentarios'
            exLibro.Worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 5 'Proveedor'
            exLibro.Worksheets("Hoja1").Columns("L").EntireColumn.ColumnWidth = 13 'Codigo Proveedor'
            exLibro.Worksheets("Hoja1").Columns("M").EntireColumn.ColumnWidth = 7 'Codigo Proveedor'

            '************

            ''Cambiamos orientacion ala hola
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Partidas Abiertas Compras"
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Linea: " + CmbGrupoArticulo.Text.ToString
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Proveedor: " + CmbCliente.Text.ToString
            exLibro.Worksheets("Hoja1").Cells.Item(4, 1) = "Articulo: " + CmbArticulo.Text.ToString

            exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(4, 1).Font.Bold = 1
            Dim i_aux As Integer = 1

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                If DGVResultado.Columns(i - 1).Visible = True Then
                    exLibro.Worksheets("Hoja1").Cells.Item(7, i_aux) = DGVResultado.Columns(i - 1).HeaderText.ToString
                    exLibro.Worksheets("Hoja1").Cells.Item(7, i_aux).Font.Size = 9
                    exLibro.Worksheets("Hoja1").Cells.Item(7, i_aux).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
                    i_aux = i_aux + 1
                End If
            Next


            For Fila As Integer = 0 To NRow - 1
                i_aux = 1
                For Col As Integer = 0 To NCol - 1
                    If DGVResultado.Columns(Col).Visible = True Then
                        exLibro.Worksheets("Hoja1").Cells.Item(Fila + 8, i_aux) = DGVResultado.Rows(Fila).Cells(Col).Value
                        'exLibro.Worksheets("Hoja1").Cells.Item(Fila + 8, i_aux).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
                        'exLibro.Worksheets("Hoja1").Cells.Item(Fila + 8, i_aux).Font.Size = 9
                        i_aux = i_aux + 1
                    End If
                Next
                Estatus.Visible = True
                ProgressBar1.Value = (Fila * 100) / NRow
            Next
            Estatus.Visible = False
            'MsgBox(NRow)
            exLibro.Worksheets("Hoja1").Cells.Range("A7:M7").WrapText = True
            exLibro.Worksheets("Hoja1").Rows.Item(7).VerticalAlignment = 2
            exLibro.Worksheets("Hoja1").Cells.Range("A8:M" & (NRow + 7).ToString).Font.Size = 9


            exLibro.Worksheets("Hoja1").Cells.Range("A7:A" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("B7:B" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("C7:C" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("D7:D" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("E7:E" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("F7:F" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("G7:G" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("H7:H" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("I7:I" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("J7:J" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("K7:K" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("L7:L" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
            exLibro.Worksheets("Hoja1").Cells.Range("M7:M" & (NRow + 7).ToString).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)

            'exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, 7).Font.Color = RGB(156, 0, 6)
            exLibro.Worksheets("Hoja1").Cells.Range("A8:C" & (NRow + 7).ToString).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Cells.Range("F8:F" & (NRow + 7).ToString).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Cells.Range("I8:K" & (NRow + 7).ToString).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Cells.Range("M8:M" & (NRow + 7).ToString).HorizontalAlignment = 3

            exLibro.Worksheets("Hoja1").Cells.Range("I8:I" & (NRow + 7).ToString).Font.Color = Color.DarkBlue
            exLibro.Worksheets("Hoja1").Cells.Range("J8:J" & (NRow + 7).ToString).Font.Color = Color.DarkGreen
            exLibro.Worksheets("Hoja1").Cells.Range("K8:K" & (NRow + 7).ToString).Font.Color = Color.DarkRed

            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exLibro.Worksheets("Hoja1").Rows.Item(7).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Rows.Item(7).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Rows.Item(7).Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").name = "Partidas Abiertas"

            'Aplicación visible
            exLibro.Worksheets.Application.Visible = True

            exLibro = Nothing
            exApp = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class