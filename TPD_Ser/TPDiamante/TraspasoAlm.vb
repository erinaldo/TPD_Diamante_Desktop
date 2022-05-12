
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO

Public Class TraspasoAlm

    Public StrProd As String = conexion_universal.CadenaSBO_Diamante
    Public StrTpm As String = conexion_universal.CadenaSQL
    Public StrCon As String = conexion_universal.CadenaSQLSAP

    Dim DvTraspaso As New DataView
    Dim DvAlmacen As New DataView
    Dim DvAlmacenOri As New DataView

    Public conexion As New SqlConnection(conexion_universal.CadenaSQL)

    Private Sub TraspasoAlm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.Location = Screen.PrimaryScreen.WorkingArea.Location
        'Me.Size = Screen.PrimaryScreen.WorkingArea.Size

        Try
            CBGeneral.Checked = True
            TBDiasInvP.Enabled = False
            TBDiasInvDestino.Enabled = False
            If UsrTPM = "MANAGER" Or UsrTPM = "COMPRAS1" Or UsrTPM = "ACOMPRAS" Or UsrTPM = "PRUEBAS" Then
                Me.WindowState = FormWindowState.Maximized
            End If
            If UsrTPM = "PRUEBAS" Then
                Button3.Visible = True
            End If

            dtIni.Value = Date.Now      '   "01/01/2015"
            dtFin.Value = Date.Now

            Me.dtIni.Value = Format(Date.Now, "dd/MM/yyyy")
            Me.dtFin.Value = Format(Date.Now, "dd/MM/yyyy")

            mLineas()
            'MuestraLineas()
            Dim ConsutaLista As String
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
                Dim DSetTablas As New DataSet
                ' -----------------------------------------------------
                Try
                    Dim DSetTablas2 As New DataSet
                    ConsutaLista = "SELECT whscode,whsname FROM owhs WHERE whscode in ('01', '03', '07')"
                    Dim daarticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)
                    daarticulo.Fill(DSetTablas2, "Almacenes")
                    'Dim filaArticulo As Data.DataRow
                    ''Asignamos a fila la nueva Row(Fila)del Dataset
                    'filaArticulo = DSetTablas2.Tables("Almacenes").NewRow
                    ''Agregamos los valores a los campos de la tabla
                    'filaArticulo("whsname") = "TODOS"
                    'filaArticulo("whscode") = 999
                    'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                    'DSetTablas2.Tables("Almacenes").Rows.Add(filaArticulo)
                    DvAlmacen.Table = DSetTablas2.Tables("Almacenes")
                    Me.CBAlmacenDestino.DataSource = DvAlmacen
                    Me.CBAlmacenDestino.DisplayMember = "whsname"
                    Me.CBAlmacenDestino.ValueMember = "whscode"
                    Me.CBAlmacenDestino.SelectedValue = "03"
                    ' -----------------------------------------------------
                    'DisenoGrid()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
        Catch ex As Exception
        End Try

        'MANDA A LLAMAR EL LLENADO DE COMBO DE ALMACEN ORIGEN
        LlenaAlmacenOri()
    End Sub


    'Llenar líneas
    Private Sub mLineas()
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
                Dim da As New SqlClient.SqlDataAdapter("SELECT ItmsGrpCod,ItmsGrpNam FROM OITB WHERE ItmsGrpCod NOT IN ('193','200','150')", SqlConnection)

                Dim ds As New DataSet
                da.Fill(ds)
                ds.Tables(0).Rows.Add(0, "TODAS")
                Me.CmbLin.DataSource = ds.Tables(0)
                Me.CmbLin.DisplayMember = "ItmsGrpNam"
                Me.CmbLin.ValueMember = "ItmsGrpCod"
                Me.CmbLin.SelectedValue = 0

            End Using

        Catch ex As Exception

        End Try

    End Sub

    'METODO QUE LLENA EL COMBO DE ALMACEN ORIGEN
    Sub LlenaAlmacenOri()
        'VARIABLE QUE ALMACENA LA CONSULTA
        Dim SQLAlmacen As String
        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
            'VARIABLE PARA ALMACENAR ALAMCEN EL DATASET
            Dim DSAlmacenOri As New DataSet
            Try
                'CONSULTA DE ALMACENES
                SQLAlmacen = "SELECT whscode,whsname FROM owhs WHERE whscode in ('01', '03', '07')"
                'COLOCA ADAPTER QUE ADQUIERE LA CONSULTA
                Dim ADAlmacenOri As New SqlClient.SqlDataAdapter(SQLAlmacen, SqlConnection)
                'LLENA EL ADAPTER CON LA CONSULTA OBTENIDA
                ADAlmacenOri.Fill(DSAlmacenOri, "AlmacenesOri")
                'COLOCA LA TABLA DE LA CONSULTA OBTENDIA CON NOMBRE ALMACENESORI
                DvAlmacenOri.Table = DSAlmacenOri.Tables("AlmacenesOri")
                'LLENA EL COMBO BOX DE ALMACENES ORIGEN
                Me.cbxAlmacenOri.DataSource = DvAlmacenOri
                Me.cbxAlmacenOri.DisplayMember = "whsname"
                Me.cbxAlmacenOri.ValueMember = "whscode"
                Me.cbxAlmacenOri.SelectedValue = "01"
            Catch ex As Exception
                MsgBox("Error en el llenado de Almacen Origen:" + ex.Message, MsgBoxStyle.Critical, "Alerta de conexión")
            End Try
        End Using
    End Sub

    Private Sub CargarRegistros()

        Dim cnn As SqlConnection = Nothing

        Try

            cnn = New SqlConnection(StrTpm)

            Dim cmd4 As SqlCommand = Nothing

            cmd4 = New SqlCommand("SPTraspasoGeneral", cnn)
            'cmd4 = New SqlCommand("SPTraspasoGeneral2", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@FechaInicial", dtIni.Value)
            cmd4.Parameters.AddWithValue("@FechaFinal", dtFin.Value)

            Dim NumMes As Decimal

            NumMes = DateDiff("d", Me.dtIni.Value, Me.dtFin.Value) / 30

            If NumMes = 0 Then
                NumMes = 1
            End If
            'MsgBox(NumMes)
            cmd4.Parameters.AddWithValue("@NumMeses", NumMes)
            cmd4.Parameters.AddWithValue("@DiasInvP", TBDiasInvP.Text)
            cmd4.Parameters.AddWithValue("@DiasInvM", TBDiasInvDestino.Text)
            cmd4.Parameters.AddWithValue("@Linea", CmbLin.Text)
            cmd4.Parameters.AddWithValue("@AlmacenDes", CBAlmacenDestino.SelectedValue)
            cmd4.Parameters.AddWithValue("@AlmacenOri", cbxAlmacenOri.SelectedValue)

            If CheckReq.Checked = True Then
                cmd4.Parameters.AddWithValue("@Requerimiento", 1)
            Else
                cmd4.Parameters.AddWithValue("@Requerimiento", 0)
            End If


            cnn.Open()

            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()

            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn


            ''--------------------------------------------
            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "Traspaso"

            DvTraspaso.Table = DsVtas.Tables("Traspaso")

            DGTraspaso.DataSource = DvTraspaso

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try


    End Sub

    Private Sub CargarRegistrosInd()
        'MsgBox("CARGA EL PROCESO SPTraspasoInd")
        Dim cnn As SqlConnection = Nothing

        Try

            cnn = New SqlConnection(StrTpm)

            Dim cmd4 As SqlCommand = Nothing

            cmd4 = New SqlCommand("SPTraspasoInd", cnn)
            'cmd4 = New SqlCommand("SPTraspasoInd2", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@FechaInicial", dtIni.Value)
            cmd4.Parameters.AddWithValue("@FechaFinal", dtFin.Value)

            Dim NumMes As Decimal

            NumMes = DateDiff("d", Me.dtIni.Value, Me.dtFin.Value) / 30

            If NumMes = 0 Then
                NumMes = 1
            End If
            'MsgBox(CmbLin.Text)
            cmd4.Parameters.AddWithValue("@NumMeses", NumMes)
            'cmd4.Parameters.AddWithValue("@DiasInvP", TBDiasInvP.Text)
            'cmd4.Parameters.AddWithValue("@DiasInv", TBDiasInvDestino.Text)
            cmd4.Parameters.AddWithValue("@Linea", CmbLin.Text)
            cmd4.Parameters.AddWithValue("@AlmacenDes", CBAlmacenDestino.SelectedValue)
            cmd4.Parameters.AddWithValue("@AlmacenOri", cbxAlmacenOri.SelectedValue)

            If CheckReq.Checked = True Then
                cmd4.Parameters.AddWithValue("@Requerimiento", 1)
            Else
                cmd4.Parameters.AddWithValue("@Requerimiento", 0)
            End If


            cnn.Open()

            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()

            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn


            ''--------------------------------------------
            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "Traspaso"

            DvTraspaso.Table = DsVtas.Tables("Traspaso")

            DGTraspaso.DataSource = DvTraspaso

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try


    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try
            'VALIDA QUE EL TRASPASO SEA DISTINTO DE ALMACENES
            If cbxAlmacenOri.SelectedValue = CBAlmacenDestino.SelectedValue Then
                'MANDA MENSAJE DE ALERTA POR SER IGUALES LOS ALMACENES
                MsgBox("Acción no permitida, los almacenes deben de ser distintos para el traspaso.", MsgBoxStyle.Exclamation, "Alerta de captura")
                cbxAlmacenOri.Focus()
                Return
            End If
            If CBGeneral.Checked = True Then
                CargarRegistrosInd()
            ElseIf CBIndividual.Checked = True Then
                CargarRegistros()
            End If
            If CheckReq.Checked = True Then
                DvTraspaso.RowFilter = "ReqMer > 0 "
                'dvTrasMerida.RowFilter = " [Requeri- miento] like '%' "
            Else
                DvTraspaso.RowFilter = String.Empty
            End If
            DisenoGrid()
        Catch ex As Exception

        End Try

    End Sub


    Private Function fValidaDatos(ByRef sError) As Boolean
        Try
            Dim bValido As Boolean
            bValido = True
            If (TBDiasInvP.Text = String.Empty Or TBDiasInvP.Text = "0") Then
                sError = sError + Environment.NewLine + "-Dias de inventario Puebla."
                bValido = False
                eInvP.Visible = True
            Else
                eInvP.Visible = False
            End If


            If (TBDiasInvDestino.Text = String.Empty Or TBDiasInvDestino.Text = "0") Then
                sError = sError + Environment.NewLine + "-Dias de inventario Merida."
                bValido = False
                eInvM.Visible = True
            Else
                eInvM.Visible = False
            End If
            Return bValido
        Catch ex As Exception
            MsgBox("Error al realizar alguna validacion", MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try

    End Function

    Private Sub CheckReq_CheckedChanged(sender As Object, e As EventArgs) Handles CheckReq.CheckedChanged
        'If CheckReq.Checked = True Then
        '    DvTraspaso.RowFilter = "ReqMer > 0 "
        'Else
        '    DvTraspaso.RowFilter = String.Empty
        'End If
    End Sub


    Private Sub DisenoGrid()
        Dim Almacen As String = ""
        Dim AlmacenOri As String = ""
        Try
            With DGTraspaso
                '.DataSource = DtAgte
                '.ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DGTraspaso.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25


                'Articulo	
                .Columns(0).HeaderText = "Clave Art."
                .Columns(0).Width = 100
                .Columns(0).Frozen = True
                .Columns(0).ReadOnly = True

                'Descripcion	
                .Columns(1).HeaderText = "Descripción"
                .Columns(1).Width = 220
                .Columns(1).Frozen = True
                .Columns(1).ReadOnly = True

                'Linea	
                .Columns(2).HeaderText = "Línea"
                .Columns(2).Width = 130
                .Columns(2).Frozen = True
                .Columns(2).ReadOnly = True

                'VALIDA QUE NOMBRE DE ALMACEN ORIGEN PONER
                If cbxAlmacenOri.Text = "MÉRIDA" Then
                    AlmacenOri = "Mér"
                ElseIf cbxAlmacenOri.Text = "TUXTLA GTZ" Then
                    AlmacenOri = "Tux.Gtz."
                Else : cbxAlmacenOri.Text = "PUEBLA"
                    AlmacenOri = "Pue"
                End If

                '' Vta. TotalP
                '.Columns(3).HeaderText = "Vta Neta Puebla"
                .Columns(3).HeaderText = "Vta Neta " & AlmacenOri
                .Columns(3).Width = 55
                .Columns(3).DefaultCellStyle.Format = "###,##0"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).ReadOnly = True


                '' Vta Mens
                .Columns(4).HeaderText = "Vta Mensual " & AlmacenOri
                .Columns(4).Width = 55
                .Columns(4).DefaultCellStyle.Format = "###,##0"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).ReadOnly = True

                '' Comprometido
                .Columns(5).HeaderText = "Comprometido " & AlmacenOri
                .Columns(5).Width = 55
                .Columns(5).DefaultCellStyle.Format = "###,##0"
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).ReadOnly = True

                '' Solicitado
                .Columns(6).HeaderText = "Solicitado " & AlmacenOri
                .Columns(6).Width = 55
                .Columns(6).DefaultCellStyle.Format = "###,##0"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).ReadOnly = True

                'Stock
                .Columns(7).HeaderText = "Stock " & AlmacenOri
                .Columns(7).Width = 55
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Format = "###,##0"
                .Columns(7).ReadOnly = True

                'DiasInvP
                .Columns(8).HeaderText = "Dias Inv. " & AlmacenOri
                .Columns(8).Width = 55
                .Columns(8).DefaultCellStyle.Format = "###,##0"
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).Visible = True

                'Stock Ideal   
                .Columns(9).HeaderText = "Stock Ideal " & AlmacenOri
                .Columns(9).Width = 55
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).DefaultCellStyle.Format = "###,##0"
                .Columns(9).ReadOnly = True

                'NOMBRE DE ALMACEN DESTINO
                If CBAlmacenDestino.Text = "MÉRIDA" Then
                    Almacen = "Mérida"
                ElseIf CBAlmacenDestino.Text = "TUXTLA GTZ" Then
                    Almacen = "Tuxtla Gtz"
                Else : CBAlmacenDestino.Text = "PUEBLA"
                    Almacen = "Puebla"
                End If


                'Vta Neta
                .Columns(10).HeaderText = "Vta Neta " & Almacen
                .Columns(10).Width = 55
                .Columns(10).DefaultCellStyle.Format = "###,##0"
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(10).ReadOnly = True

                'Venta Mensual
                .Columns(11).HeaderText = "Vta Mensual " & Almacen
                .Columns(11).Width = 55
                .Columns(11).DefaultCellStyle.Format = "###,##0"
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(11).ReadOnly = True

                ' Comprometido
                .Columns(12).HeaderText = "Comprometido " & Almacen
                .Columns(12).Width = 55
                .Columns(12).DefaultCellStyle.Format = "###,##0"
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(12).ReadOnly = True

                ' "Solicitado"
                .Columns(13).HeaderText = "Solicitado " & Almacen
                .Columns(13).Width = 55
                .Columns(13).DefaultCellStyle.Format = "###,##0"
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(13).ReadOnly = True

                'StockM
                .Columns(14).HeaderText = "Stock " & Almacen
                .Columns(14).Width = 55
                .Columns(14).DefaultCellStyle.Format = "###,##0"
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(14).ReadOnly = True

                'DiasInv
                .Columns(15).HeaderText = "Dias Inv " & Almacen
                .Columns(15).Width = 55
                .Columns(15).DefaultCellStyle.Format = "###,##0"
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(15).Visible = True

                'Stock Ideal
                .Columns(16).HeaderText = "Stock Ideal " & Almacen
                .Columns(16).Width = 55
                .Columns(16).DefaultCellStyle.Format = "###,##0"
                .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(16).ReadOnly = True

                '' Requerimiento
                .Columns(17).HeaderText = "Requerido " & Almacen
                .Columns(17).Width = 55
                .Columns(17).DefaultCellStyle.Format = "###,##0"
                .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(17).ReadOnly = True


                'Cant Tranferible     
                .Columns(18).HeaderText = "Cant. Transferible"
                .Columns(18).Width = 55
                .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(18).DefaultCellStyle.Format = "###,##0"
                .Columns(18).ReadOnly = True


                'Precio L9
                .Columns(19).HeaderText = "Precio L9"
                .Columns(19).Width = 80
                .Columns(19).DefaultCellStyle.Format = "$ ###,###.#0"
                .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(19).ReadOnly = True



                ''Cumplimiento                                                                
                '.Columns(18).Width = 44
                '.Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(18).DefaultCellStyle.Format = "###,###"

                ''Stock                                                                       
                '.Columns(19).Width = 44
                '.Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(19).DefaultCellStyle.Format = "% ###"

                'Traspaso BO        
                .Columns(20).HeaderText = "Traspaso"
                .Columns(20).Width = 55
                .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(20).DefaultCellStyle.Format = "###,##0"
                .Columns(20).ReadOnly = False

                .Columns(21).HeaderText = "Traspaso ($)"
                .Columns(21).Width = 80
                .Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(21).DefaultCellStyle.Format = "$ ###,##0.#0"

                'Monto BO                                                                    
                '.Columns(21).Width = 75
                '.Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '.Columns(21).DefaultCellStyle.Format = "$ ###,###.00"

                'SE COLOCA LA UBICACION DEL PRODUCTO Y EL PESO
                'PESO
                .Columns("Peso").HeaderText = "Peso Pza."
                .Columns("Peso").Width = 55
                .Columns("Peso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("Peso").Frozen = False
                .Columns("Peso").ReadOnly = True
                'PESO POR CANTIDAD TRANSFERIBLE
                .Columns("PesoxCantTras").HeaderText = "Peso x Traspaso."
                .Columns("PesoxCantTras").Width = 55
                .Columns("PesoxCantTras").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns("PesoxCantTras").Frozen = False
                .Columns("PesoxCantTras").ReadOnly = True
                'BLOQUE
                .Columns("Bloque").HeaderText = "Bloque"
                .Columns("Bloque").Width = 40
                .Columns("Bloque").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Bloque").Frozen = False
                .Columns("Bloque").ReadOnly = True
                'SECCION
                .Columns("Seccion").HeaderText = "Seccion"
                .Columns("Seccion").Width = 45
                .Columns("Seccion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Seccion").Frozen = False
                .Columns("Seccion").ReadOnly = True
                'RACK
                .Columns("Rack").HeaderText = "Rack"
                .Columns("Rack").Width = 40
                .Columns("Rack").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Rack").Frozen = False
                .Columns("Rack").ReadOnly = True
                'NIVEL
                .Columns("Nivel").HeaderText = "Nivel"
                .Columns("Nivel").Width = 40
                .Columns("Nivel").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Nivel").Frozen = False
                .Columns("Nivel").ReadOnly = True
                'ESPACIO
                .Columns("Espacio").HeaderText = "Espacio"
                .Columns("Espacio").Width = 45
                .Columns("Espacio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Espacio").Frozen = False
                .Columns("Espacio").ReadOnly = True
            End With
        Catch ex As Exception
            MsgBox("Error estilo Grid: " + ex.ToString)
        End Try

    End Sub


    Private Sub bExcel_Click(sender As Object, e As EventArgs) Handles bExcel.Click
        Try
            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
            Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

            'Añadimos el Libro al programa, y la hoja al libro
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets.Add()

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DGTraspaso.ColumnCount
            Dim NRow As Integer = DGTraspaso.RowCount


            fFormatoExcel(exLibro, NRow)


            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                'exHoja.Cells.Item(6, i) = DGTraspaso.Columns(i - 1).Name.ToString
                exHoja.Cells.Item(6, i) = DGTraspaso.Columns(i - 1).HeaderText.ToString
                'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
            Next

            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    'exHoja.Cells.Item(Fila + 7, Col + 1).NumberFormat = "@"
                    exHoja.Cells.Item(Fila + 7, Col + 1) = DGTraspaso.Rows(Fila).Cells(Col).Value
                Next
                Estatus.Visible = True
                ProgressBar1.Value = (Fila * 100) / NRow
            Next

            Estatus.Visible = False


            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exHoja.Rows.Item(5).Font.Bold = 1
            exHoja.Rows.Item(5).HorizontalAlignment = 3
            exHoja.Columns.AutoFit()
            'Aplicación visible
            exApp.Application.Visible = True


            ''Cambiamos orientacion ala hoja

            If CBGeneral.Checked = True Then
                exHoja.Cells.Item(1, 1) = "Reporte General de Traspaso de Almacen"
                exHoja.Cells.Item(2, 1) = "Fecha del: " + dtIni.Value + "  Al  " + dtFin.Value
                exHoja.Cells.Item(3, 1) = "Línea: " + CmbLin.Text
                'exHoja.Cells.Item(4, 1) = "Días de Inventario "
                'exHoja.Cells.Item(4, 2) = "Puebla: " + TBDiasInvP.Text + "     " + CBAlmacenDestino.Text + ": " + TBDiasInvDestino.Text

            Else
                exHoja.Cells.Item(1, 1) = "Reporte Individual de Traspaso de Almacen"
                exHoja.Cells.Item(2, 1) = "Fecha del: " + dtIni.Value + "  Al  " + dtFin.Value
                exHoja.Cells.Item(3, 1) = "Línea: " + CmbLin.Text
                exHoja.Cells.Item(4, 1) = "Días de Inventario "
                exHoja.Cells.Item(4, 2) = "Puebla: " + TBDiasInvP.Text + "     " + CBAlmacenDestino.Text + ": " + TBDiasInvDestino.Text
            End If

            exHoja = Nothing
            exLibro = Nothing
            exApp = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")
        End Try
    End Sub


    Private Sub fFormatoExcel(exLibro As Microsoft.Office.Interop.Excel.Workbook, NRow As Integer)
        Try

            exLibro.Worksheets("Hoja2").Columns("A").NumberFormat = "@"

            exLibro.Worksheets("Hoja2").Cells.Range("E6:E" + (NRow + 6).ToString).Interior.ColorIndex = 44

            exLibro.Worksheets("Hoja2").Cells.Range("H6:H" + (NRow + 6).ToString).Interior.ColorIndex = 6

            exLibro.Worksheets("Hoja2").Cells.Range("L6:L" + (NRow + 6).ToString).Interior.ColorIndex = 44

            exLibro.Worksheets("Hoja2").Cells.Range("O6:O" + (NRow + 6).ToString).Interior.ColorIndex = 6

            exLibro.Worksheets("Hoja2").Cells.Range("R6:R" + (NRow + 6).ToString).Interior.ColorIndex = 37

            exLibro.Worksheets("Hoja2").Cells.Range("U6:U" + (NRow + 6).ToString).Interior.ColorIndex = 15

            '*COLORES
            '44 GOLD
            '6 YELLOW   
            '37 BLUE
            '15 GRAY

        Catch ex As Exception

        End Try


    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Module1.AlmacenD = CBAlmacenDestino.SelectedValue
        Module1.AlmacenOri = cbxAlmacenOri.SelectedValue

        'MsgBox(CBAlmacenDestino.SelectedValue)
        Dim frm As New DiasInv()
        'Mostramos el formulario Form2.
        frm.Show()

        Module1.AlmacenD = CBAlmacenDestino.SelectedValue
        Module1.AlmacenOri = cbxAlmacenOri.SelectedValue

        'DiasInv.MdiParent = Me

    End Sub

    Private Sub DGTraspaso_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs)
        'DGTraspaso.Rows(e.RowIndex).Cells("MerVtaNet").Style.BackColor = Color.LightGreen
        'DGTraspaso.Rows(e.RowIndex).Cells("PueVtaNet").Style.BackColor = Color.LightGreen

        DGTraspaso.Rows(e.RowIndex).Cells("VtaMensMer").Style.BackColor = Color.Gold
        DGTraspaso.Rows(e.RowIndex).Cells("VtaMensPue").Style.BackColor = Color.Gold

        'DGTraspaso.Rows(e.RowIndex).Cells("MerComprometido").Style.BackColor = Color.LightBlue
        'DGTraspaso.Rows(e.RowIndex).Cells("PueComprometido").Style.BackColor = Color.LightBlue

        'DGTraspaso.Rows(e.RowIndex).Cells("MerSolicitado").Style.BackColor = Color.MistyRose
        'DGTraspaso.Rows(e.RowIndex).Cells("PueSolicitado").Style.BackColor = Color.MistyRose

        DGTraspaso.Rows(e.RowIndex).Cells("AlmMerida").Style.BackColor = Color.Yellow
        DGTraspaso.Rows(e.RowIndex).Cells("AlmPue").Style.BackColor = Color.Yellow

        'DGTraspaso.Rows(e.RowIndex).Cells("StockIdealM").Style.BackColor = Color.LawnGreen
        'DGTraspaso.Rows(e.RowIndex).Cells("StockIdealP").Style.BackColor = Color.LawnGreen

        DGTraspaso.Rows(e.RowIndex).Cells("ReqMer").Style.BackColor = Color.LightBlue

        'DGTraspaso.Rows(e.RowIndex).Cells("CanTransf").Style.BackColor = Color.LightBlue

        DGTraspaso.Rows(e.RowIndex).Cells("Traspaso").Style.BackColor = Color.Black
        DGTraspaso.Rows(e.RowIndex).Cells("Traspaso").Style.ForeColor = Color.White

        'DGTraspaso.Rows(e.RowIndex).Cells("MontodeTraspaso").Style.BackColor = Color.Gainsboro

    End Sub

    Private Sub CmbLin_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbLin.SelectionChangeCommitted

        'If CmbLin.Text <> "TODAS" Then
        'conexion.Open()
        Try

            Dim cmd As SqlCommand = New SqlCommand("SELECT CASE WHEN DiasInv IS NULL THEN 0 ELSE DiasInv END as DiasInv FROM DiasInv2 WHERE itmsgrpcod=@Linea and (WhsCode='01' OR WhsCode=@AlmDestino) ", conexion)

            cmd.Parameters.AddWithValue("@Linea", CStr(CmbLin.SelectedValue))
            cmd.Parameters.AddWithValue("@AlmDestino", CStr(CBAlmacenDestino.SelectedValue))

            Dim da2 As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt2 As New DataTable
            da2.Fill(dt2)

            If dt2.Rows.Count > 0 Then

                Dim row As DataRow = dt2.Rows(0)
                Dim row1 As DataRow = dt2.Rows(1)

                TBDiasInvP.Text = CStr(row("DiasInv"))
                TBDiasInvDestino.Text = CStr(row1("DiasInv"))

            Else
                TBDiasInvP.Text = 0
                TBDiasInvDestino.Text = 0

            End If
            'Else
            'MsgBox("No hay Objetivo de este periodo")
            'End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'Else
        'TBDiasInvP.Text = 0
        'TBDiasInvDestino.Text = 0
        'End If
    End Sub

    Private Sub DGTraspaso_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)

        Try

            Dim row As DataGridViewRow = DGTraspaso.CurrentRow


            DGTraspaso.Item(21, DGTraspaso.CurrentCell.RowIndex).Value = DGTraspaso.Item(20, DGTraspaso.CurrentCell.RowIndex).Value * DGTraspaso.Item(19, DGTraspaso.CurrentCell.RowIndex).Value

        Catch ex As Exception
            MsgBox("Ingrese número válido")
        End Try

    End Sub

    Private Sub CBGeneral_CheckedChanged(sender As Object, e As EventArgs) Handles CBGeneral.CheckedChanged
        If CBGeneral.Checked = True Then
            CBIndividual.Checked = False

            TBDiasInvP.Text = ""
            TBDiasInvDestino.Text = ""

            TBDiasInvP.Enabled = False
            TBDiasInvDestino.Enabled = False

        Else

            CBIndividual.Checked = True

            TBDiasInvP.Enabled = False
            TBDiasInvDestino.Enabled = False


        End If

    End Sub

    Private Sub CBIndividual_CheckedChanged(sender As Object, e As EventArgs) Handles CBIndividual.CheckedChanged
        If CBIndividual.Checked = True Then
            CBGeneral.Checked = False

            TBDiasInvP.Enabled = True
            TBDiasInvDestino.Enabled = True
        Else

            CBGeneral.Checked = True

            TBDiasInvP.Enabled = True
            TBDiasInvDestino.Enabled = True

        End If
    End Sub


    Private Sub TraspasoAlm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        'Variable global
        Dim f As Form

        'En tu código para mostrarlo
        f = New DiasInv

        'En tu código para cerrarlo
        f.Close()

    End Sub

    Private Sub DGTraspaso_RowPrePaint_1(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGTraspaso.RowPrePaint
        'DGTraspaso.Rows(e.RowIndex).Cells("MerVtaNet").Style.BackColor = Color.LightGreen
        'DGTraspaso.Rows(e.RowIndex).Cells("PueVtaNet").Style.BackColor = Color.LightGreen

        DGTraspaso.Rows(e.RowIndex).Cells("VtaMensMer").Style.BackColor = Color.Gold
        DGTraspaso.Rows(e.RowIndex).Cells("VtaMensPue").Style.BackColor = Color.Gold

        'DGTraspaso.Rows(e.RowIndex).Cells("MerComprometido").Style.BackColor = Color.LightBlue
        'DGTraspaso.Rows(e.RowIndex).Cells("PueComprometido").Style.BackColor = Color.LightBlue

        'DGTraspaso.Rows(e.RowIndex).Cells("MerSolicitado").Style.BackColor = Color.MistyRose
        'DGTraspaso.Rows(e.RowIndex).Cells("PueSolicitado").Style.BackColor = Color.MistyRose

        DGTraspaso.Rows(e.RowIndex).Cells("AlmMerida").Style.BackColor = Color.Yellow
        DGTraspaso.Rows(e.RowIndex).Cells("AlmPue").Style.BackColor = Color.Yellow

        'DGTraspaso.Rows(e.RowIndex).Cells("StockIdealM").Style.BackColor = Color.LawnGreen
        'DGTraspaso.Rows(e.RowIndex).Cells("StockIdealP").Style.BackColor = Color.LawnGreen

        DGTraspaso.Rows(e.RowIndex).Cells("ReqMer").Style.BackColor = Color.LightBlue

        'DGTraspaso.Rows(e.RowIndex).Cells("CanTransf").Style.BackColor = Color.LightBlue

        DGTraspaso.Rows(e.RowIndex).Cells("Traspaso").Style.BackColor = Color.Black
        DGTraspaso.Rows(e.RowIndex).Cells("Traspaso").Style.ForeColor = Color.White

        'DGTraspaso.Rows(e.RowIndex).Cells("MontodeTraspaso").Style.BackColor = Color.Gainsboro
    End Sub

    Private Sub DGTraspaso_CellEndEdit_1(sender As Object, e As DataGridViewCellEventArgs) Handles DGTraspaso.CellEndEdit
        Try

            Dim row As DataGridViewRow = DGTraspaso.CurrentRow


            DGTraspaso.Item(21, DGTraspaso.CurrentCell.RowIndex).Value = DGTraspaso.Item(20, DGTraspaso.CurrentCell.RowIndex).Value * DGTraspaso.Item(19, DGTraspaso.CurrentCell.RowIndex).Value

        Catch ex As Exception
            MsgBox("Ingrese número válido")
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            ExportNuevo()
        Catch ex As Exception
            MessageBox.Show("Error al Exportar Nuevo: " + ex.ToString)
        End Try
    End Sub

    Sub ExportNuevo()
        'Validar Almacen Origen
        If cbxAlmacenOri.Text = "MÉRIDA" Then
            AlmacenOri = "Mér"
        ElseIf cbxAlmacenOri.Text = "TUXTLA GTZ" Then
            AlmacenOri = "Tux.Gtz."
        Else : cbxAlmacenOri.Text = "PUEBLA"
            AlmacenOri = "Pue"
        End If

        'Validar almacen destino
        If CBAlmacenDestino.Text = "MÉRIDA" Then
            Almacen = "Mérida"
        ElseIf CBAlmacenDestino.Text = "TUXTLA GTZ" Then
            Almacen = "Tuxtla Gtz"
        Else : CBAlmacenDestino.Text = "PUEBLA"
            Almacen = "Puebla"
        End If


        Dim dv As DataView = DirectCast(DGTraspaso.DataSource, DataView)
        Dim dt As DataTable = dv.Table

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(dt, "Rep. Gen. Traspaso de Alm.")

            Dim index As Integer = 2

            For i As Integer = 0 To dt.Rows.Count

                Try
                    Dim row As DataRow = dt.Rows(i)

                    'Encabezados dependiendo sucursal
                    If i = 1 Then
                        Dim cellA0 As String = String.Format("A{0}", i)
                        wb.Worksheet(1).Cells(cellA0).Value = "Clave Art."

                        Dim cellB0 As String = String.Format("B{0}", i)
                        wb.Worksheet(1).Cells(cellB0).Value = "Descripción"

                        Dim cellC0 As String = String.Format("C{0}", i)
                        wb.Worksheet(1).Cells(cellC0).Value = "Línea"

                        Dim cellD0 As String = String.Format("D{0}", i)
                        wb.Worksheet(1).Cells(cellD0).Value = "Vta Neta " + AlmacenOri

                        Dim cellE0 As String = String.Format("E{0}", i)
                        wb.Worksheet(1).Cells(cellE0).Value = "Vta Mensual " + AlmacenOri

                        Dim cellF0 As String = String.Format("F{0}", i)
                        wb.Worksheet(1).Cells(cellF0).Value = "Comprometido " + AlmacenOri

                        Dim cellG0 As String = String.Format("G{0}", i)
                        wb.Worksheet(1).Cells(cellG0).Value = "Solicitado " + AlmacenOri

                        Dim cellH0 As String = String.Format("H{0}", i)
                        wb.Worksheet(1).Cells(cellH0).Value = "Stock " + AlmacenOri

                        Dim cellI0 As String = String.Format("I{0}", i)
                        wb.Worksheet(1).Cells(cellI0).Value = "Dias Inv. " + AlmacenOri

                        Dim cellJ0 As String = String.Format("J{0}", i)
                        wb.Worksheet(1).Cells(cellJ0).Value = "Stock Ideal " + AlmacenOri

                        Dim cellK0 As String = String.Format("K{0}", i)
                        wb.Worksheet(1).Cells(cellK0).Value = "Vta Neta " + Almacen

                        Dim cellL0 As String = String.Format("L{0}", i)
                        wb.Worksheet(1).Cells(cellL0).Value = "Vta Mensual " + Almacen

                        Dim cellM0 As String = String.Format("M{0}", i)
                        wb.Worksheet(1).Cells(cellM0).Value = "Comprometido " + Almacen

                        Dim cellN0 As String = String.Format("N{0}", i)
                        wb.Worksheet(1).Cells(cellN0).Value = "Solicitado " + Almacen

                        Dim cellO0 As String = String.Format("O{0}", i)
                        wb.Worksheet(1).Cells(cellO0).Value = "Stock " + Almacen

                        Dim cellP0 As String = String.Format("P{0}", i)
                        wb.Worksheet(1).Cells(cellP0).Value = "Dias Inv " + Almacen

                        Dim cellQ0 As String = String.Format("Q{0}", i)
                        wb.Worksheet(1).Cells(cellQ0).Value = "Stock Ideal " + Almacen

                        Dim cellR0 As String = String.Format("R{0}", i)
                        wb.Worksheet(1).Cells(cellR0).Value = "Requerido" + Almacen

                        Dim cellS0 As String = String.Format("S{0}", i)
                        wb.Worksheet(1).Cells(cellS0).Value = "Cant. Transferible"

                        Dim cellT0 As String = String.Format("T{0}", i)
                        wb.Worksheet(1).Cells(cellT0).Value = "Precio L9"

                        Dim cellU0 As String = String.Format("U{0}", i)
                        wb.Worksheet(1).Cells(cellU0).Value = "Traspaso"

                        Dim cellV0 As String = String.Format("V{0}", i)
                        wb.Worksheet(1).Cells(cellV0).Value = "Traspaso ($)"

                        Dim cellW0 As String = String.Format("W{0}", i)
                        wb.Worksheet(1).Cells(cellW0).Value = "Peso Pza."

                        Dim cellX0 As String = String.Format("X{0}", i)
                        wb.Worksheet(1).Cells(cellX0).Value = "Peso x Traspaso."

                        Dim cellY0 As String = String.Format("Y{0}", i)
                        wb.Worksheet(1).Cells(cellY0).Value = "Bloque"

                        Dim cellZ0 As String = String.Format("Z{0}", i)
                        wb.Worksheet(1).Cells(cellZ0).Value = "Sección"

                        Dim cellAA0 As String = String.Format("AA{0}", i)
                        wb.Worksheet(1).Cells(cellAA0).Value = "Rack"

                        Dim cellAB0 As String = String.Format("AB{0}", i)
                        wb.Worksheet(1).Cells(cellAB0).Value = "Nivel"

                        Dim cellAC0 As String = String.Format("AC{0}", i)
                        wb.Worksheet(1).Cells(cellAC0).Value = "Espacio"

                    End If

                    'Formato de cada una de las celdas
                    Dim cellA As String = String.Format("A{0}", index)
                    'wb.Worksheet(1).Cells(cellA).Value = CStr(row(0))

                    Dim cellB As String = String.Format("B{0}", index)

                    Dim cellC As String = String.Format("C{0}", index)
                    'wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0"

                    Dim cellD As String = String.Format("D{0}", index)

                    Dim cellE As String = String.Format("E{0}", index)
                    wb.Worksheet(1).Cells(cellE).Style.Fill.BackgroundColor = XLColor.Gold

                    Dim cellF As String = String.Format("F{0}", index)

                    Dim cellG As String = String.Format("G{0}", index)

                    Dim cellH As String = String.Format("H{0}", index)
                    wb.Worksheet(1).Cells(cellH).Style.Fill.BackgroundColor = XLColor.Yellow

                    Dim cellI As String = String.Format("I{0}", index)
                    'wb.Worksheet(1).Cells(cellI).Style.Fill.BackgroundColor = XLColor.LightGray

                    Dim cellJ As String = String.Format("J{0}", index)
                    'wb.Worksheet(1).Cells(cellJ).Style.Fill.BackgroundColor = XLColor.Maroon

                    Dim cellK As String = String.Format("K{0}", index)

                    Dim cellL As String = String.Format("L{0}", index)
                    wb.Worksheet(1).Cells(cellL).Style.Fill.BackgroundColor = XLColor.Gold

                    Dim cellM As String = String.Format("M{0}", index)

                    Dim cellN As String = String.Format("N{0}", index)
                    'wb.Worksheet(1).Cells(cellN).Style.Fill.BackgroundColor = XLColor.LightSkyBlue

                    Dim cellO As String = String.Format("O{0}", index)
                    wb.Worksheet(1).Cells(cellO).Style.Fill.BackgroundColor = XLColor.Yellow

                    Dim cellP As String = String.Format("P{0}", index)

                    Dim cellQ As String = String.Format("Q{0}", index)

                    Dim cellR As String = String.Format("R{0}", index)
                    wb.Worksheet(1).Cells(cellR).Style.Fill.BackgroundColor = XLColor.LightBlue

                    Dim cellS As String = String.Format("S{0}", index)

                    Dim cellT As String = String.Format("T{0}", index)

                    Dim cellU As String = String.Format("U{0}", index)
                    wb.Worksheet(1).Cells(cellU).Style.Fill.BackgroundColor = XLColor.Black
                    wb.Worksheet(1).Cells(cellU).Style.Font.FontColor = XLColor.White

                    Dim cellV As String = String.Format("V{0}", index)

                    Dim cellW As String = String.Format("W{0}", index)

                    Dim cellX As String = String.Format("X{0}", index)

                    Dim cellY As String = String.Format("Y{0}", index)

                    Dim cellZ As String = String.Format("Z{0}", index)

                    Dim cellAA As String = String.Format("AA{0}", index)

                    Dim cellAB As String = String.Format("AB{0}", index)

                    Dim cellAC As String = String.Format("AC{0}", index)

                Catch ex As Exception

                End Try

                index = index + 1
            Next

            wb.Worksheet(1).Columns().AdjustToContents()

            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Filter = "Excel|*.xlsx"
            saveFileDialog1.Title = "Save Excel File"
            saveFileDialog1.FileName = "Rep. Gen. Traspaso de Alm. " + AlmacenOri + "-" + Almacen + ".xlsx"
            saveFileDialog1.ShowDialog()
            saveFileDialog1.InitialDirectory = "C:/"

            If saveFileDialog1.FileName <> "" Then
                Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
                fs.Close()
            End If

            Dim strFileName As String = saveFileDialog1.FileName
            wb.SaveAs(strFileName)
            Process.Start(saveFileDialog1.FileName)
        End Using
    End Sub

End Class