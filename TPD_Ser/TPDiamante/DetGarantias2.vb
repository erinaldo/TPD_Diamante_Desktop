
Option Explicit On
'Option Strict On
Imports System.Data.SqlClient

Public Class DetGarantias2

    Dim DvArticulo As New DataView
    Dim DvClte As New DataView
    Dim DvAgte As New DataView
    Dim DvBO As New DataView

    Dim Azul As Color
    Public DVDetGar As New DataView
    Dim switch As Integer = 0
    Public conexion As New SqlConnection(conexion_universal.CadenaSQLSAP)


    ''EVENTO LOAD
    Private Sub DetGarantias_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        If UsrTPM = "ALMACEN1" Then
            Button1.Location = New Point(650, 40)
            BSave.Location = New Point(673, 90)
            Label1.Location = New Point(650, 128)
        End If
        'DGGarantias.Columns.Clear()



        If UsrTPM = "VENTAS2" Or UsrTPM = "AMERIDA" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" Or UsrTPM = "COMERCIAL" _
                    Or UsrTPM = "ASTRIDY" Or UsrTPM = "VENTAS5" Or UsrTPM = "VENTAS4" Or UsrTPM = "RROBLES" Or UsrTPM = "VENTAS8" _
                    Or UsrTPM = "VVERGARA" Or UsrTPM = "VENTAS1" Then
            Button1.Visible = False
            BSave.Visible = False
            Label1.Visible = False


        End If

        Try
            ''--------------------
            TBDocNum.BackColor = Color.Cornsilk

            CBDocumento.SelectedItem = "Factura"

            Dim ConsutaLista As String


            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                'mllenaComboAlmacen(SqlConnection)

                'LLENA COMBO LINEAS

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


                '''''*******************************
                ''''---------------------------------

                'LLENA COMBO CLIENTES

                ConsutaLista = "SELECT CardCode,CardName, SlpCode, GroupCode FROM OCRD WHERE CardType = 'C' ORDER BY CardName "


                Dim daClientes As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

                daClientes.Fill(DSetTablas, "Clientes")

                Dim filaClientes As Data.DataRow

                'Asignamos a fila la nueva Row(Fila)del Dataset
                filaClientes = DSetTablas.Tables("Clientes").NewRow

                'Agregamos los valores a los campos de la tabla
                filaClientes("CardName") = "TODOS"
                filaClientes("CardCode") = "TODOS"
                filaClientes("slpcode") = 999
                filaClientes("groupcode") = 999

                'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                DSetTablas.Tables("Clientes").Rows.Add(filaClientes)

                DvClte.Table = DSetTablas.Tables("Clientes")

                Me.CmbCliente.DataSource = DvClte
                Me.CmbCliente.DisplayMember = "CardName"
                Me.CmbCliente.ValueMember = "CardCode"
                Me.CmbCliente.SelectedValue = "TODOS"

                'LLENA COMBO ARTICULOS

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

            ''--------------------

            CBAlmacen.SelectedItem = "TODOS"


            'cargar_registros()

            'If DGGarantias.RowCount > 0 Then
            '    DisenoGarantias()
            'End If

            'DisenoGarantias()

        Catch ex As Exception

        End Try

    End Sub


    Private Sub ObtenerDiasTotales()
        'conexion.Open()
        For i = 0 To DGGarantias.RowCount - 1
            Dim cmd As SqlCommand = New SqlCommand("SELECT * FROM TPM.dbo.DetFactGar WHERE Factura=@Factura AND Id=@Id AND Itemcode=@ItemCode ", conexion)

            cmd.Parameters.AddWithValue("@Factura", DGGarantias.Item(4, i).Value)
            cmd.Parameters.AddWithValue("@ItemCode", DGGarantias.Item(12, i).Value)
            'cmd.Parameters.AddWithValue("@Descripcion", Module1.Descripcion)
            cmd.Parameters.AddWithValue("@Id", DGGarantias.Item(16, i).Value)

            Try
                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count > 0 Then

                    'TBDocNum.BackColor = Color.White
                    'TBCliente.BackColor = Color.White
                    'TBNomCli.BackColor = Color.White

                    Dim row As DataRow = dt.Rows(0)

                    ''OBTENER DIAS TOTALES TRANSCURRIDOS

                    'MsgBox(CStr(row("FecAlm")))
                    'MsgBox(CStr(row("Dictamen")))


                    If CStr(row("FecAlm")) <> "01/01/2015" Then
                        If CStr(row("Dictamen")) = "" Then
                            DGGarantias.Item(1, i).Value = DateDiff("d", CStr(row("FecAlm")), Date.Now)
                        ElseIf CStr(row("Dictamen")) = "Sí procede" Then
                            If CStr(row("RespProv")) = "Nota de crédito" Or CStr(row("RespProv")) = "Cambio físico" Or CStr(row("RespProv")) = "Otros" Then
                                If CStr(row("FecNota")) <> "01/01/2015" Then
                                    DGGarantias.Item(1, i).Value = DateDiff("d", CStr(row("FecAlm")), CStr(row("FecNota")))
                                Else
                                    DGGarantias.Item(1, i).Value = DateDiff("d", CStr(row("FecAlm")), Date.Now)
                                End If

                            Else
                                If CStr(row("FecRespProv")) <> "01/01/2015" Then
                                    DGGarantias.Item(1, i).Value = DateDiff("d", CStr(row("FecAlm")), CStr(row("FecRespProv")))
                                Else
                                    DGGarantias.Item(1, i).Value = DateDiff("d", CStr(row("FecAlm")), Date.Now)
                                End If
                            End If
                        ElseIf CStr(row("Dictamen")) = "No procede" Then
                            If CStr(row("FecRecSuc")) <> "01/01/2015" Then
                                DGGarantias.Item(1, i).Value = DateDiff("d", CStr(row("FecAlm")), CStr(row("FecRecSuc")))
                            Else
                                DGGarantias.Item(1, i).Value = DateDiff("d", CStr(row("FecAlm")), Date.Now)
                            End If

                        Else
                            DGGarantias.Item(1, i).Value = 0
                        End If
                    End If
                End If

            Catch ex As Exception

            End Try

        Next

    End Sub


    Sub cargar_registros()

        'DGGarantias.Rows.Clear()
        'DGGarantias.Columns.Clear()

        Dim DTRefacciones As New DataTable

        ' crear nueva conexión    
        Dim conexion2 As New SqlConnection(StrTpm)

        ' abrir la conexión con la base de datos   
        conexion2.Open()

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand

        Dim SQLTPD As String

        SQLTPD = "SELECT Estado,DIasTransTot,FecSuc,FecAlm,Factura,FecFac,DiasTransFecFacFecRecAlm,CardCode,CardName,Sucursal,Almacen,Cantidad,Itemcode,ItemName,ItmsGrpNam, "
        SQLTPD &= "Proveedor,Id "
        SQLTPD &= "FROM DetFactGar WHERE Factura='" & Module1.Factura & "' AND Itemcode='" & Module1.Articulo & "' ORDER BY ID, FecAlm "


        ' Nuevo objeto Dataset   
        Dim DsVtasDet As New DataSet

        DsVtasDet.Tables.Add(DTRefacciones)

        With comando
            'If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
            '    .Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
            'End If
            '.Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
            '.Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
            '' Asignar el sql para seleccionar los datos de la tabla Maestro   
            .CommandText = SQLTPD
            .Connection = conexion2
        End With


        Dim DtFactProv As New DataTable

        With Adaptador
            .SelectCommand = comando
            ' llenar el dataset   
            .Fill(DtFactProv)
        End With

        DVDetGar = DtFactProv.DefaultView

        With Me.DGGarantias
            .DataSource = DVDetGar

            .AllowUserToAddRows = False

        End With

        With conexion2
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With

    End Sub


    Private Sub DisenoGarantias()

        Try

            With DGGarantias

                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 20
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                'Color de linea del grid

                DGGarantias.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Status
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(0).ReadOnly = True

                'Dias Trans Totales
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).ReadOnly = True

                'Fecha Sucursal
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Fecha Almacén
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Folio
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Factura
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Fecha()
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Dias Trans()
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'codCliente()
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'nomCliente()
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Sucursal
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Almacén()
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Cantidad
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Artículo()
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Descripcion
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Línea()
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Proveedor
                .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Boton seguimiento
                '.Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(16).def() '= "Seguimiento"




                'If switch = 0 Then

                '    'If DGGarantias.RowCount = 0 Then
                '    ' creación de una columna de tipo botón
                '    Dim colBoton As DataGridViewButtonColumn = New DataGridViewButtonColumn()
                '    colBoton.Name = "Seguimiento"
                '    colBoton.HeaderText = "Seguimiento"
                '    colBoton.Text = "Seguimiento"
                '    colBoton.UseColumnTextForButtonValue = True
                '    colBoton.Width = 85
                '    ' añadir la columna de tipo botón al DataGridView
                '    Me.DGGarantias.Columns.Add(colBoton)

                'Else
                '    DGGarantias.Columns.RemoveAt(0)

                '    Dim colBoton As DataGridViewButtonColumn = New DataGridViewButtonColumn()
                '    colBoton.Name = "Seguimiento"
                '    colBoton.HeaderText = "Seguimiento"
                '    colBoton.Text = "Seguimiento"
                '    colBoton.UseColumnTextForButtonValue = True
                '    colBoton.Width = 85
                '    ' añadir la columna de tipo botón al DataGridView
                '    Me.DGGarantias.Columns.Add(colBoton)

                'End If

                'switch = 1

                'End If



                ''Causa
                '.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                ''Fecha entrega a compras para reclamo	
                '.Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                ''Fecha de entrega a proveedor para su revisón
                '.Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''Seguimiento
                '.Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                ''Dictamen
                '.Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                ''Fecha Dictamen
                '.Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''Fecha de Entrega de resultados a almacén	
                '.Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''Seguimineto2()
                '.Columns(22).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                ''NumGuiEnv()
                '.Columns(23).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''FecEnv()
                '.Columns(24).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''FecRecSuc()
                '.Columns(25).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''Fecha de entrega a Contabilidad		
                '.Columns(26).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''Nota de crédito	
                '.Columns(27).Name = "NotCre"
                '.Columns(27).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''Fecha Nota de Crédito	
                '.Columns(28).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''Fecha de Entrega a Compras para realizar Movimiento	
                '.Columns(29).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter



                ''Tipo Documento()
                '.Columns(30).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                ''Documento
                '.Columns(31).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''Fecha de Documento
                '.Columns(32).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''Fecha de entrada a Almacén 2
                '.Columns(33).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            End With


        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub BSave_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BSave_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub CmbGrupoArticulo_SelectionChangeCommitted(sender As Object, e As EventArgs)
        BuscaArticulos()
    End Sub

    Private Sub BuscaArticulos()
        If CmbGrupoArticulo.SelectedValue Is Nothing Or CmbGrupoArticulo.SelectedValue = 999 Then
            DvArticulo.RowFilter = String.Empty
            CmbArticulo.SelectedValue = "TODOS"

        Else
            CmbArticulo.SelectedValue = "TODOS"
            DvArticulo.RowFilter = "ItmsGrpCod = " & Trim(Me.CmbGrupoArticulo.SelectedValue.ToString) & " OR ItmsGrpCod = 999"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub DGGarantias_CellEndEdit_1(sender As Object, e As DataGridViewCellEventArgs) Handles DGGarantias.CellEndEdit

        If Me.DGGarantias.Columns(e.ColumnIndex).Name = "FecAlm" Then

            Dim difdate As Integer
            'difdate = DateDiff("d", DGGarantias.Item(1, DGGarantias.CurrentRow.Index).Value, DGGarantias.Item(2, DGGarantias.CurrentRow.Index).Value)

            'DGGarantias.Item(5, DGGarantias.CurrentRow.Index).Value = difdate

            'If DGGarantias.Item(1, DGGarantias.CurrentRow.Index).Value <> "01/01/2015" And DGGarantias.Item(2, DGGarantias.CurrentRow.Index).Value = "01/01/2015" Then
            '    difdate = DateDiff("d", DGGarantias.Item(1, DGGarantias.CurrentRow.Index).Value, Date.Now)
            '    DGGarantias.Item(5, DGGarantias.CurrentRow.Index).Value = difdate
            'Else
            difdate = DateDiff("d", DGGarantias.Item(2, DGGarantias.CurrentRow.Index).Value, DGGarantias.Item(3, DGGarantias.CurrentRow.Index).Value)
            DGGarantias.Item(6, DGGarantias.CurrentRow.Index).Value = difdate
            'End If
        End If


        If Me.DGGarantias.Columns(e.ColumnIndex).Name = "FecSuc" Then
            Dim difdate As Integer
            If DGGarantias.Item(2, DGGarantias.CurrentRow.Index).Value <> "01/01/2015" And DGGarantias.Item(3, DGGarantias.CurrentRow.Index).Value = "01/01/2015" Then
                difdate = DateDiff("d", DGGarantias.Item(2, DGGarantias.CurrentRow.Index).Value, Date.Now)
                DGGarantias.Item(6, DGGarantias.CurrentRow.Index).Value = difdate
            Else
                difdate = DateDiff("d", DGGarantias.Item(2, DGGarantias.CurrentRow.Index).Value, DGGarantias.Item(3, DGGarantias.CurrentRow.Index).Value)
                DGGarantias.Item(6, DGGarantias.CurrentRow.Index).Value = difdate
            End If
        End If

    End Sub

    Private Sub DGGarantias_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGGarantias.RowPrePaint

        Azul = ColorTranslator.FromHtml("#E8CCE8")

        If DGGarantias.Rows(e.RowIndex).Cells("Estatus").Value = "TERMINADA" Then

            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("FecSuc").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("FecAlm").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("Folio").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("Factura").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("FecFac").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("DiasTransFactRecep").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("CardCode").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("CardName").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("Sucursal").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("Almacen").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("Cantidad").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("Itemcode").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("ItemName").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("ItmsGrpNam").Style.BackColor = Color.Green
            DGGarantias.Rows(e.RowIndex).Cells("Proveedor").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("Causa").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecEntcompRec").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecEntProvRev").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("Seguimiento").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("Dictamen").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecDic").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecEntResAlm").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("Seguimiento2").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("NumGuiEnv").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecEnv").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecRecSuc").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecEntCon").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("NotCre").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecNota").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecEntComMov").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("Seguimiento2").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecEntcompRec").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("TipoDoc").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("Docto").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecDoc").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("FecEntAl").Style.BackColor = Color.Green
            '    DGGarantias.Rows(e.RowIndex).Cells("Id").Style.BackColor = Color.Green

            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("FecSuc").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("FecAlm").Style.ForeColor = Color.White
            '    DGGarantias.Rows(e.RowIndex).Cells("Folio").Style.forecolor = Color.white
            DGGarantias.Rows(e.RowIndex).Cells("Factura").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("FecFac").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("DiasTransFactRecep").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("CardCode").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("CardName").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("Sucursal").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("Almacen").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("Cantidad").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("Itemcode").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("ItemName").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("ItmsGrpNam").Style.ForeColor = Color.White
            DGGarantias.Rows(e.RowIndex).Cells("Proveedor").Style.ForeColor = Color.White


        ElseIf DGGarantias.Rows(e.RowIndex).Cells("Estatus").Value = "EN CURSO" Then

            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.BackColor = Color.Gold
            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.ForeColor = Color.Black

            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.BackColor = Color.Gold
            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.ForeColor = Color.Black

        ElseIf DGGarantias.Rows(e.RowIndex).Cells("Estatus").Value = "RECHAZADA" Then

            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.BackColor = Color.LightGray
            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.ForeColor = Color.Black

            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.BackColor = Color.LightGray
            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.ForeColor = Color.Black

        ElseIf DGGarantias.Rows(e.RowIndex).Cells("Estatus").Value = "RETRASO CON EL PROVEEDOR" Then

            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.BackColor = Color.Red
            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.ForeColor = Color.White

            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.BackColor = Color.Red
            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.ForeColor = Color.White

        ElseIf DGGarantias.Rows(e.RowIndex).Cells("Estatus").Value = "CON EL PROVEEDOR" Then

            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.BackColor = Color.LightBlue
            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.ForeColor = Color.Black

            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.BackColor = Color.LightBlue
            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.ForeColor = Color.Black

        ElseIf DGGarantias.Rows(e.RowIndex).Cells("Estatus").Value = "PENDIENTE NC " Then

            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.BackColor = Azul
            DGGarantias.Rows(e.RowIndex).Cells("Estatus").Style.ForeColor = Color.Black


            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.BackColor = Azul
            DGGarantias.Rows(e.RowIndex).Cells("DiasTot").Style.ForeColor = Color.Black
        End If



    End Sub


    Private Sub Button2_Click_2(sender As Object, e As EventArgs) Handles Button2.Click
        ''CONSULTAR 

        Dim Consulta As String = ""
        Dim strcadena As String = ""
        Dim CTabla As String = ""
        Dim DTMObra As New DataTable
        Dim DTProb As New DataTable
        Dim vAlm As Integer = 0

        '****************************************

        Consulta &= " SELECT "
        If CBEstatus.Text = "PENDIENTE NC" Then
            Consulta &= "CASE WHEN Dictamen= 'Sí procede' THEN 'PENDIENTE NC' ELSE Estado END as Estado "
        Else
            Consulta &= "Estado "
        End If
        Consulta &= ",DIASTRANSTOT,FecSuc,FecAlm,Factura,FecFac, "
        Consulta &= "CASE WHEN FecSuc='20150101' AND FecAlm='20150101' THEN 0 "
        Consulta &= "WHEN FecSuc<>'20150101' AND FecAlm='20150101' THEN DATEDIFF(D,FecSuc,GETDATE()) "
        Consulta &= "ELSE DATEDIFF(D,FecSuc,FecAlm) "
        Consulta &= "END AS 'DiasTransFecFacFecRecAlm', "
        Consulta &= "cardcode, CardName, Sucursal, Almacen, "
        Consulta &= "Cantidad, ItemCode, ItemName, ItmsGrpNam, Proveedor, Id "
        Consulta &= "FROM TPM.dbo.DetFactGar WHERE Id>0 "
        'Consulta &= "Proveedor,Id FROM TPM.dbo.DetFactGar WHERE Id>0 "

        If TBDocNum.Text <> "" Then
            If CBDocumento.Text = "Factura" Then

                Consulta &= " AND Factura = @Factura "

            ElseIf CBDocumento.Text = "Nota de crédito" Then

                Consulta &= " AND NotCre = @Factura "

            End If

            'Consulta &= " AND Factura = @Factura "
        Else

            If CBAlmacen.Text = "TODOS" And CmbCliente.Text = "TODOS" And CmbGrupoArticulo.Text = "TODOS" And CmbArticulo.Text = "TODOS" Then
                Consulta &= " AND FecAlm >='" & DtpFechaIni.Value.ToString("yyyy-MM-dd") & "' AND FecAlm <='" & DtpFechaTer.Value.ToString("yyyy-MM-dd") & "'"
            End If

            If CBAlmacen.Text <> "TODOS" Then
                Consulta &= " AND Almacen ='" & CBAlmacen.SelectedValue & "'"
            End If

            If CmbCliente.Text <> "TODOS" Then
                Consulta &= " AND cardcode ='" & CmbCliente.SelectedValue & "'"
            End If

            If CmbGrupoArticulo.Text <> "TODOS" Then
                Consulta &= " AND ItmsGrpNam ='" & CmbGrupoArticulo.Text & "'"
            End If

            If CmbArticulo.Text <> "TODOS" Then
                Consulta &= " AND Itemcode ='" & CmbArticulo.Text & "'"
            End If

            If CBEstatus.Text <> "" Then
                If CBEstatus.Text = "EN CURSO" Then
                    Consulta &= " and Dictamen<>'Sí procede' "
                End If

                If CBEstatus.Text = "PENDIENTE NC" Then
                    Consulta &= " and Dictamen='Sí procede' "
                    Consulta &= " AND Estado ='EN CURSO' "
                Else
                    Consulta &= " AND Estado ='" & CBEstatus.Text & "'"
                End If


            End If

            'Consulta &= " ORDER BY FecAlm asc"

        End If

        Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

        'CmdMObra.Parameters.Add("@FechaIni", SqlDbType.Date)
        'CmdMObra.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
        'CmdMObra.Parameters.Add("@FechaTer", SqlDbType.Date)
        'CmdMObra.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value
        'CmdMObra.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime).Value = Me.DtpFechaIni.Value
        'CmdMObra.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime).Value = Me.DtpFechaTer.Value


        CmdMObra.Parameters.Add("@Factura", SqlDbType.VarChar)
        CmdMObra.Parameters("@Factura").Value = TBDocNum.Text


        'If CmbCliente.SelectedValue <> "TODOS" Then
        '    CmdMObra.Parameters.Add("@IdCliente", SqlDbType.Char)
        '    CmdMObra.Parameters("@IdCliente").Value = CmbCliente.SelectedValue
        'End If

        'If CmbGrupoArticulo.SelectedValue <> 999 Then
        '    CmdMObra.Parameters.Add("@GrupoArt", SqlDbType.Int)
        '    CmdMObra.Parameters("@GrupoArt").Value = CmbGrupoArticulo.SelectedValue
        'End If


        'If CmbArticulo.SelectedValue <> "TODOS" Then
        '    CmdMObra.Parameters.Add("@IdArticulo", SqlDbType.Char)
        '    CmdMObra.Parameters("@IdArticulo").Value = CmbArticulo.SelectedValue
        'End If

        Dim DsVtasDet As New DataSet

        'DTMObra.TableName = "DetBO"

        DsVtasDet.Tables.Add(DTMObra)

        CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
        CmdMObra.Connection.Open()

        Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)

        AdapMObra.SelectCommand = CmdMObra
        AdapMObra.Fill(DsVtasDet, "BO")

        DsVtasDet.Tables(1).TableName = "BackOrder"
        'DsVtasDet.Tables(2).TableName = "DetBO"
        'DsVtasDet.Tables(3).TableName = "Recuperable"

        'datagridview 1
        'Se crea datatable y se asigna al datagridview
        Dim DtBackOrder As New DataTable
        DtBackOrder = DsVtasDet.Tables("BackOrder")
        Me.DGGarantias.DataSource = DtBackOrder

        DisenoGarantias()

        Try
            Dim difdate As Integer
            For i = 0 To DGGarantias.RowCount
                If DGGarantias.Item(2, i).Value <> "01/01/2015" And DGGarantias.Item(3, i).Value = "01/01/2015" Then
                    difdate = DateDiff("d", DGGarantias.Item(2, DGGarantias.CurrentRow.Index).Value, Date.Now)
                    DGGarantias.Item(6, DGGarantias.CurrentRow.Index).Value = difdate
                Else
                    difdate = DateDiff("d", DGGarantias.Item(2, DGGarantias.CurrentRow.Index).Value, DGGarantias.Item(3, DGGarantias.CurrentRow.Index).Value)
                    DGGarantias.Item(6, DGGarantias.CurrentRow.Index).Value = difdate
                End If
            Next

        Catch ex As Exception

        End Try

        'If DGGarantias.Item(1, DGGarantias.CurrentRow.Index).Value <> "01/01/2015" And DGGarantias.Item(2, DGGarantias.CurrentRow.Index).Value = "01/01/2015" Then
        '    difdate = DateDiff("d", DGGarantias.Item(1, DGGarantias.CurrentRow.Index).Value, Date.Now)
        '    DGGarantias.Item(5, DGGarantias.CurrentRow.Index).Value = difdate
        'Else
        '    difdate = DateDiff("d", DGGarantias.Item(1, DGGarantias.CurrentRow.Index).Value, DGGarantias.Item(2, DGGarantias.CurrentRow.Index).Value)
        '    DGGarantias.Item(5, DGGarantias.CurrentRow.Index).Value = difdate
        'End If

        ObtenerDiasTotales()


    End Sub

    Private Sub BSave_Click_2(sender As Object, e As EventArgs) Handles BSave.Click
        ''BOTON GUARDAR
        'ACTUALIZAR REGISTROS EN FACTGAR
        Try

            Dim cnn As SqlConnection = Nothing

            Dim cmd4 As SqlCommand = Nothing

            For i = 0 To DGGarantias.RowCount - 1

                Try

                    cnn = New SqlConnection(StrTpm)

                    'SELECT Estado 0,FecSuc 1,FecAlm 2,Factura 3,FecFac 4,DiasTransFecFacFecRecAlm 5,CardCode 6,CardName 7,
                    'Sucursal 8, Almacen 9, Cantidad 10, ItemCode 11, ItemName 12, ItmsGrpNam 13, "
                    'Proveedor 14,Id 15"


                    cmd4 = New SqlCommand("SPActualizaDetFactGar2", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@Id", DGGarantias.Item(16, i).Value)
                    cmd4.Parameters.AddWithValue("@Estado", DGGarantias.Item(0, i).Value)
                    cmd4.Parameters.AddWithValue("@FecSuc", Date.Parse(DGGarantias.Item(2, i).Value).Date.ToString("yyyyMMdd"))
                    cmd4.Parameters.AddWithValue("@FecAlm", Date.Parse(DGGarantias.Item(3, i).Value).Date.ToString("yyyyMMdd"))
                    cmd4.Parameters.AddWithValue("@Factura", DGGarantias.Item(4, i).Value)
                    cmd4.Parameters.AddWithValue("@DiasTransFecFacFecRecAlm", DGGarantias.Item(6, i).Value)
                    cmd4.Parameters.AddWithValue("@Sucursal", DGGarantias.Item(9, i).Value)
                    cmd4.Parameters.AddWithValue("@Itemcode", DGGarantias.Item(12, i).Value)

                    'cmd4.Parameters.AddWithValue("@Folio", DGGarantias.Item(3, i).Value)
                    'cmd4.Parameters.AddWithValue("@Causa", DGGarantias.Item(15, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecEntcompRec", Date.Parse(DGGarantias.Item(16, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@FecEntProvRev", Date.Parse(DGGarantias.Item(17, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@Seguimiento", DGGarantias.Item(18, i).Value)
                    'cmd4.Parameters.AddWithValue("@Dictamen", DGGarantias.Item(19, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecDic", Date.Parse(DGGarantias.Item(20, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@FecEntResAlm", Date.Parse(DGGarantias.Item(21, i).Value).Date.ToString("yyyyMMdd"))

                    'cmd4.Parameters.AddWithValue("@Seguimiento2", DGGarantias.Item(22, i).Value)
                    'cmd4.Parameters.AddWithValue("@NumGuiEnv", DGGarantias.Item(23, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecEnv", DGGarantias.Item(24, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecRecSuc", DGGarantias.Item(25, i).Value)

                    'cmd4.Parameters.AddWithValue("@FecEntCon", Date.Parse(DGGarantias.Item(26, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@NotCre", DGGarantias.Item(27, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecNota", DGGarantias.Item(28, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecEntComMov", Date.Parse(DGGarantias.Item(29, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@TipoDoc", DGGarantias.Item(30, i).Value)
                    'cmd4.Parameters.AddWithValue("@Docto", DGGarantias.Item(31, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecDoc", Date.Parse(DGGarantias.Item(32, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@FecEntAl", Date.Parse(DGGarantias.Item(33, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@Id", DGGarantias.Item(34, i).Value)

                    cnn.Open()

                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()

                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmd4
                    da.SelectCommand.Connection = cnn

                    ''--------------------------------------------
                    'Dim DsVtas As New DataSet
                    'da.Fill(DsVtas, "DsVtas")

                    ''DsVtas.Tables(0).TableName = "Inventario"

                    ''DvInventario.Table = DsVtas.Tables("Inventario")

                    ''DGInventario.DataSource = DvInventario

                Catch ex As Exception
                    'Return
                    MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            Next
            'FIN ACTUALIZAR REGISTROS EN FACTGAR

            MessageBox.Show("Datos actualizados correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

        End Try
    End Sub



    Private Sub DGGarantias_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGGarantias.CellContentClick
        'If Me.DGGarantias.Columns(e.ColumnIndex).Name = "Seguimiento" Then

        '    Dim cnn As SqlConnection = Nothing

        '    Dim cmd4 As SqlCommand = Nothing

        '    For i = 0 To DGGarantias.RowCount - 1

        '        Try

        '            cnn = New SqlConnection(StrTpm)

        '            'SELECT Estado 0,FecSuc 1,FecAlm 2,Factura 3,FecFac 4,DiasTransFecFacFecRecAlm 5,CardCode 6,CardName 7,
        '            'Sucursal 8, Almacen 9, Cantidad 10, ItemCode 11, ItemName 12, ItmsGrpNam 13, "
        '            'Proveedor 14,Id 15"


        '            cmd4 = New SqlCommand("SPActualizaDetFactGar2", cnn)
        '            cmd4.CommandType = CommandType.StoredProcedure
        '            cmd4.Parameters.AddWithValue("@Id", DGGarantias.Item(15, i).Value)
        '            cmd4.Parameters.AddWithValue("@Estado", DGGarantias.Item(0, i).Value)
        '            cmd4.Parameters.AddWithValue("@FecSuc", Date.Parse(DGGarantias.Item(1, i).Value).Date.ToString("yyyyMMdd"))
        '            cmd4.Parameters.AddWithValue("@FecAlm", Date.Parse(DGGarantias.Item(2, i).Value).Date.ToString("yyyyMMdd"))
        '            cmd4.Parameters.AddWithValue("@Factura", DGGarantias.Item(3, i).Value)
        '            cmd4.Parameters.AddWithValue("@DiasTransFecFacFecRecAlm", DGGarantias.Item(5, i).Value)
        '            cmd4.Parameters.AddWithValue("@Sucursal", DGGarantias.Item(8, i).Value)
        '            cmd4.Parameters.AddWithValue("@Itemcode", DGGarantias.Item(11, i).Value)

        '            'cmd4.Parameters.AddWithValue("@Folio", DGGarantias.Item(3, i).Value)
        '            'cmd4.Parameters.AddWithValue("@Causa", DGGarantias.Item(15, i).Value)
        '            'cmd4.Parameters.AddWithValue("@FecEntcompRec", Date.Parse(DGGarantias.Item(16, i).Value).Date.ToString("yyyyMMdd"))
        '            'cmd4.Parameters.AddWithValue("@FecEntProvRev", Date.Parse(DGGarantias.Item(17, i).Value).Date.ToString("yyyyMMdd"))
        '            'cmd4.Parameters.AddWithValue("@Seguimiento", DGGarantias.Item(18, i).Value)
        '            'cmd4.Parameters.AddWithValue("@Dictamen", DGGarantias.Item(19, i).Value)
        '            'cmd4.Parameters.AddWithValue("@FecDic", Date.Parse(DGGarantias.Item(20, i).Value).Date.ToString("yyyyMMdd"))
        '            'cmd4.Parameters.AddWithValue("@FecEntResAlm", Date.Parse(DGGarantias.Item(21, i).Value).Date.ToString("yyyyMMdd"))

        '            'cmd4.Parameters.AddWithValue("@Seguimiento2", DGGarantias.Item(22, i).Value)
        '            'cmd4.Parameters.AddWithValue("@NumGuiEnv", DGGarantias.Item(23, i).Value)
        '            'cmd4.Parameters.AddWithValue("@FecEnv", DGGarantias.Item(24, i).Value)
        '            'cmd4.Parameters.AddWithValue("@FecRecSuc", DGGarantias.Item(25, i).Value)

        '            'cmd4.Parameters.AddWithValue("@FecEntCon", Date.Parse(DGGarantias.Item(26, i).Value).Date.ToString("yyyyMMdd"))
        '            'cmd4.Parameters.AddWithValue("@NotCre", DGGarantias.Item(27, i).Value)
        '            'cmd4.Parameters.AddWithValue("@FecNota", DGGarantias.Item(28, i).Value)
        '            'cmd4.Parameters.AddWithValue("@FecEntComMov", Date.Parse(DGGarantias.Item(29, i).Value).Date.ToString("yyyyMMdd"))
        '            'cmd4.Parameters.AddWithValue("@TipoDoc", DGGarantias.Item(30, i).Value)
        '            'cmd4.Parameters.AddWithValue("@Docto", DGGarantias.Item(31, i).Value)
        '            'cmd4.Parameters.AddWithValue("@FecDoc", Date.Parse(DGGarantias.Item(32, i).Value).Date.ToString("yyyyMMdd"))
        '            'cmd4.Parameters.AddWithValue("@FecEntAl", Date.Parse(DGGarantias.Item(33, i).Value).Date.ToString("yyyyMMdd"))
        '            'cmd4.Parameters.AddWithValue("@Id", DGGarantias.Item(34, i).Value)

        '            cnn.Open()

        '            cmd4.ExecuteNonQuery()
        '            cmd4.Connection.Close()

        '            Dim da As New SqlDataAdapter
        '            da.SelectCommand = cmd4
        '            da.SelectCommand.Connection = cnn

        '            ''--------------------------------------------
        '            'Dim DsVtas As New DataSet
        '            'da.Fill(DsVtas, "DsVtas")

        '            ''DsVtas.Tables(0).TableName = "Inventario"

        '            ''DvInventario.Table = DsVtas.Tables("Inventario")

        '            ''DGInventario.DataSource = DvInventario

        '        Catch ex As Exception
        '            'Return
        '            MsgBox(ex.Message)
        '        Finally
        '            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
        '                cnn.Close()
        '            End If
        '        End Try

        '    Next


        '    Module1.Factura = DGGarantias.Item(3, DGGarantias.CurrentRow.Index).Value
        '    Module1.Articulo = DGGarantias.Item(11, DGGarantias.CurrentRow.Index).Value
        '    'Module1.Descripcion = DGGarantias.Item(12, DGGarantias.CurrentRow.Index).Value
        '    Module1.GarId = DGGarantias.Item(15, DGGarantias.CurrentRow.Index).Value
        '    Module1.NumRenglon = DGGarantias.CurrentRow.Index
        '    Module1.FecRecAlm = DGGarantias.Item(1, DGGarantias.CurrentRow.Index).Value

        '    Dim frm As New DetGarantiasSeg()
        '    'Mostramos el formulario Form2.
        '    frm.Show()

        '    'Me.Close()
        '    'Me.DGGarantias.Columns.Clear()
        '    'End If

        'End If
    End Sub

    Private Sub DGGarantias_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGGarantias.CellMouseClick
        Try
            'MsgBox(DGGarantias.CurrentCell.ColumnIndex)
            'MsgBox(DGGarantias.CurrentRow.Index)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim frm As New Garantias()
        'Mostramos el formulario Form2.
        frm.Show()
        Me.Close()
    End Sub

    Private Sub DGGarantias_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGGarantias.CellDoubleClick
        If Me.DGGarantias.Columns(e.ColumnIndex).Name = "Estatus" Then

            Dim cnn As SqlConnection = Nothing

            Dim cmd4 As SqlCommand = Nothing

            For i = 0 To DGGarantias.RowCount - 1

                Try

                    cnn = New SqlConnection(StrTpm)

                    'SELECT Estado 0,FecSuc 1,FecAlm 2,Factura 3,FecFac 4,DiasTransFecFacFecRecAlm 5,CardCode 6,CardName 7,
                    'Sucursal 8, Almacen 9, Cantidad 10, ItemCode 11, ItemName 12, ItmsGrpNam 13, "
                    'Proveedor 14,Id 15"

                    'SELECT Estado,DIASTRANSTOT,FecSuc,FecAlm,Factura,FecFac,"
                    'CASE WHEN FecSuc='20150101' AND FecAlm='20150101' THEN 0 "
                    'WHEN FecSuc<>'20150101' AND FecAlm='20150101' THEN DATEDIFF(D,FecSuc,GETDATE()) "
                    'ELSE DATEDIFF(D,FecSuc,FecAlm) "
                    'END AS 'DiasTransFecFacFecRecAlm', "
                    'cardcode, CardName, Sucursal, Almacen, "
                    'Cantidad, ItemCode, ItemName, ItmsGrpNam, Proveedor, Id "
                    'FROM TPM.dbo.DetFactGar

                    cmd4 = New SqlCommand("SPActualizaDetFactGar2", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@Id", DGGarantias.Item(16, i).Value)
                    cmd4.Parameters.AddWithValue("@Estado", DGGarantias.Item(0, i).Value)
                    cmd4.Parameters.AddWithValue("@FecSuc", Date.Parse(DGGarantias.Item(2, i).Value).Date.ToString("yyyyMMdd"))
                    cmd4.Parameters.AddWithValue("@FecAlm", Date.Parse(DGGarantias.Item(3, i).Value).Date.ToString("yyyyMMdd"))
                    cmd4.Parameters.AddWithValue("@Factura", DGGarantias.Item(4, i).Value)
                    cmd4.Parameters.AddWithValue("@DiasTransFecFacFecRecAlm", DGGarantias.Item(6, i).Value)
                    cmd4.Parameters.AddWithValue("@Sucursal", DGGarantias.Item(9, i).Value)
                    cmd4.Parameters.AddWithValue("@Itemcode", DGGarantias.Item(12, i).Value)

                    'cmd4.Parameters.AddWithValue("@Folio", DGGarantias.Item(3, i).Value)
                    'cmd4.Parameters.AddWithValue("@Causa", DGGarantias.Item(15, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecEntcompRec", Date.Parse(DGGarantias.Item(16, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@FecEntProvRev", Date.Parse(DGGarantias.Item(17, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@Seguimiento", DGGarantias.Item(18, i).Value)
                    'cmd4.Parameters.AddWithValue("@Dictamen", DGGarantias.Item(19, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecDic", Date.Parse(DGGarantias.Item(20, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@FecEntResAlm", Date.Parse(DGGarantias.Item(21, i).Value).Date.ToString("yyyyMMdd"))

                    'cmd4.Parameters.AddWithValue("@Seguimiento2", DGGarantias.Item(22, i).Value)
                    'cmd4.Parameters.AddWithValue("@NumGuiEnv", DGGarantias.Item(23, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecEnv", DGGarantias.Item(24, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecRecSuc", DGGarantias.Item(25, i).Value)

                    'cmd4.Parameters.AddWithValue("@FecEntCon", Date.Parse(DGGarantias.Item(26, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@NotCre", DGGarantias.Item(27, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecNota", DGGarantias.Item(28, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecEntComMov", Date.Parse(DGGarantias.Item(29, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@TipoDoc", DGGarantias.Item(30, i).Value)
                    'cmd4.Parameters.AddWithValue("@Docto", DGGarantias.Item(31, i).Value)
                    'cmd4.Parameters.AddWithValue("@FecDoc", Date.Parse(DGGarantias.Item(32, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@FecEntAl", Date.Parse(DGGarantias.Item(33, i).Value).Date.ToString("yyyyMMdd"))
                    'cmd4.Parameters.AddWithValue("@Id", DGGarantias.Item(34, i).Value)

                    cnn.Open()

 

                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()

                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmd4
                    da.SelectCommand.Connection = cnn

                    ''--------------------------------------------
                    'Dim DsVtas As New DataSet
                    'da.Fill(DsVtas, "DsVtas")

                    ''DsVtas.Tables(0).TableName = "Inventario"

                    ''DvInventario.Table = DsVtas.Tables("Inventario")

                    ''DGInventario.DataSource = DvInventario

                Catch ex As Exception
                    'Return
                    MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            Next


            Module1.Factura = DGGarantias.Item(4, DGGarantias.CurrentRow.Index).Value
            Module1.Articulo = DGGarantias.Item(12, DGGarantias.CurrentRow.Index).Value
            'Module1.Descripcion = DGGarantias.Item(12, DGGarantias.CurrentRow.Index).Value
            Module1.GarId = DGGarantias.Item(16, DGGarantias.CurrentRow.Index).Value
            Module1.NumRenglon = DGGarantias.CurrentRow.Index
            Module1.FecRecAlm = DGGarantias.Item(3, DGGarantias.CurrentRow.Index).Value

            Dim frm As New DetGarantiasSeg()
            'Mostramos el formulario Form2.
            frm.ShowDialog()

            'Me.Close()
            'Me.DGGarantias.Columns.Clear()
            'End If

        End If
    End Sub
End Class