Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Public Class AnalasisCompras
    Dim DvProveedores As New DataView
    Dim DvProveedoresT As New DataView
    Dim DvLineas As New DataView
    Dim DvLineasT As New DataView

#Region "Eventos"

    Private Sub TxtDiasInv_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtDiasInv.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        ElseIf Char.IsSymbol(e.KeyChar) Then
            e.Handled = True
        ElseIf Char.IsPunctuation(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub


    'Sub BuscaProveedores()

    '    Try

    '        If CmbLinIni.SelectedItem Is Nothing Or CmbLinIni.Text = "TODOS" Then
    '            CBProvTODOS()
    '            DvProveedoresT.RowFilter = String.Empty
    '            Me.CBProveedor.SelectedValue = 999
    '            Me.CBProveedor.DataSource = DvProveedoresT

    '        ElseIf CmbLinIni.SelectedValue <> 0 Or CmbLinIni.SelectedValue <> 1 Then
    '            CBProv()
    '            DvProveedores.RowFilter = String.Empty
    '            Me.CBProveedor.SelectedValue = 999
    '            DvProveedores.RowFilter = "ItmsGrpCod = " & Trim(Me.CmbLinIni.SelectedValue.ToString) & " OR itmsgrpcod = 999"
    '        End If
    '    Catch ex As Exception

    '    End Try


    'End Sub


    Private Sub BuscaLineas()

        Try

            If CBProveedor.SelectedItem Is Nothing Or CBProveedor.Text = "TODOS" Then
                mCargaLineaIni()
                Me.CmbLinIni.DataSource = DvLineasT
                DvLineas.RowFilter = String.Empty
                Me.CmbLinIni.SelectedValue = 0

            Else 'CmbLinIni.SelectedValue <> 0 Then
                mCargaLineaIniTodas()
                Me.CmbLinIni.DataSource = DvLineas
                DvLineas.RowFilter = String.Empty
                DvLineas.RowFilter = "cardcode = '" & Trim(Me.CBProveedor.SelectedValue.ToString) & "' OR cardcode ='999' "
                Me.CmbLinIni.SelectedValue = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    'Private Sub CmbLinIni_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbLinIni.SelectionChangeCommitted
    '    'BuscaProveedores()
    'End Sub



    Private Sub AnalasisCompras_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'If UsrTPM = "MANAGER" Then

            '    pEncabezado.Location = New Point(250, 16)
            '    DgRptCompras.Width = 1330
            '    Me.WindowState = FormWindowState.Maximized

            'End If

            Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
            Me.DtpFechaFin.Value = Format(Date.Now, "dd/MM/yyyy")

            'CBProv()

        Catch ex As Exception
            MsgBox("Error al cargar el formulario:" + Environment.NewLine + "-Analisis de Compras", MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try




    End Sub

    Private Sub CmbAlmacen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbAlmacen.SelectedIndexChanged
        Try
            mCmbAlmacen()

            BuscaLineas()

        Catch ex As Exception

            MsgBox("Error al cargar el metodo:" + Environment.NewLine + "-CmbAlmacen", MsgBoxStyle.Critical, "Tracto Partes Diamante")

        End Try
    End Sub

    Private Sub BtnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConsultar.Click
        Try
            Dim iMes As Decimal
            Dim sError As String
            Dim sAlm1, sAlm2 As String
            Dim prov As String

            sError = String.Empty
            If (fValidaDatos(sError)) Then

                If (CmbAlmacen.SelectedItem = "TODOS") Then
                    sAlm1 = "01"
                    sAlm2 = "03"
                    ckConsolidado.Enabled = True
                Else
                    sAlm1 = CmbAlmacen.SelectedItem.ToString()
                    sAlm2 = CmbAlmacen.SelectedItem.ToString()
                End If

                prov = CBProveedor.Text

                iMes = DateDiff("d", DtpFechaIni.Value, DtpFechaFin.Value) / 30
                'MsgBox("valor" & iMes)
                mLlenaGridCompras(iMes, Convert.ToInt32(TxtDiasInv.Text), DtpFechaIni.Value.Date, DtpFechaFin.Value.Date,
                                sAlm1, sAlm2, CmbLinIni.SelectedValue, prov)
                BtnExcel.Visible = True

                If (CmbAlmacen.SelectedItem = "TODOS") Then
                    ckConsolidado.Enabled = True
                Else
                    ckConsolidado.Enabled = False
                End If
            Else

                MsgBox("Verifique los siguientes campos: " + sError, MsgBoxStyle.Exclamation, "Tracto Partes Diamante")

            End If
        Catch ex As Exception

            MsgBox("Error al cargar el metodo:" + Environment.NewLine + "-BtnConsultar_Click", MsgBoxStyle.Critical)

        End Try

    End Sub

    Private Sub BtnExcel_Click(sender As Object, e As EventArgs) Handles BtnExcel.Click
        Try
            mCreaExcel("Analisis de compras", DgRptCompras, DtpFechaIni.Value.Date, DtpFechaFin.Value.Date, 2)

            ''Creamos las variables
            'Dim exApp As New Microsoft.Office.Interop.Excel.Application
            'Dim exLibro As Microsoft.Office.Interop.Excel.Workbook


            'Try
            '    'Añadimos el Libro al programa
            '    exLibro = exApp.Workbooks.Add

            '    ' ¿Cuantas columnas y cuantas filas?
            '    Dim NCol As Integer = DgRptCompras.ColumnCount
            '    Dim NRow As Integer = DgRptCompras.RowCount

            '    ''Cambiamos orientacion ala hola
            '    exLibro.Worksheets("Hoja1").PageSetup.Orientation = 2

            '    exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Reporte de Compras del: " + DtpFechaIni.Value.Date +
            '                                                   "    al: " + DtpFechaFin.Value.Date
            '    exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1

            '    'Combinamos celdas
            '    exLibro.Worksheets("Hoja1").Cells.Range("A1:B1").Merge(True)

            '    'aplicamos un color de fondo ala celda o rango de celdas
            '    'exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.Color = RGB(200, 160, 27)
            '    exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15

            '    'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            '    For i As Integer = 1 To NCol
            '        exLibro.Worksheets("Hoja1").Cells.Item(3, i) = DgRptCompras.Columns(i - 1).Name.ToString
            '    Next

            '    For Fila As Integer = 0 To NRow - 1
            '        For Col As Integer = 0 To NCol - 1
            '            exLibro.Worksheets("Hoja1").Cells.Item(Fila + 4, Col + 1) = DgRptCompras.Rows(Fila).Cells(Col).Value
            '        Next
            '    Next

            '    'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            '    exLibro.Worksheets("Hoja1").Rows.Item(3).Font.Bold = 1
            '    exLibro.Worksheets("Hoja1").Rows.Item(3).HorizontalAlignment = 3
            '    exLibro.Worksheets("Hoja1").Rows.Item(3).Interior.ColorIndex = 15
            '    exLibro.Worksheets("Hoja1").Columns.AutoFit()
            '    exLibro.Worksheets("Hoja1").name = "Reporte de compras"
            '    'exLibro.Worksheets("Hoja2").Delete()
            '    'exLibro.Worksheets("Hoja3").Delete()

            '    'Aplicación visible
            '    exLibro.Worksheets.Application.Visible = True


            '    exLibro = Nothing
            '    exApp = Nothing
        Catch ex As Exception
            MsgBox("Error al cargar el metodo:" + Environment.NewLine + "-BtnExcel_Click", MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub

    Private Sub DtpFechaIni_ValueChanged(sender As Object, e As EventArgs) Handles DtpFechaIni.ValueChanged
        Try
            If (DtpFechaIni.Value > DtpFechaFin.Value) Then
                MsgBox("La fecha final no puede ser mayor ala fecha inicial.", MsgBoxStyle.Exclamation, "Tracto Partes Diamante")
                DtpFechaIni.Value = DtpFechaFin.Value
                eIni.Visible = True
            Else
                eIni.Visible = False
            End If
        Catch ex As Exception
            MsgBox("Error al validar la fecha inicial.", MsgBoxStyle.Exclamation, "Tracto Partes Diamante")

        End Try

    End Sub

    Private Sub DtpFechaFin_ValueChanged(sender As Object, e As EventArgs) Handles DtpFechaFin.ValueChanged
        Try
            If (DtpFechaIni.Value > DtpFechaFin.Value) Then
                MsgBox("La fecha final no puede ser mayor ala fecha inicial.", MsgBoxStyle.Exclamation)
                DtpFechaIni.Value = DtpFechaFin.Value
                eFin.Visible = True
            Else
                eFin.Visible = False
            End If

        Catch ex As Exception
            MsgBox("Error al validar la fecha final.", MsgBoxStyle.Exclamation, "Tracto Partes Diamante")
        End Try

    End Sub

    Private Sub CmbLinIni_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbLinIni.SelectedIndexChanged
        DgRptCompras.DataSource = String.Empty
        'BuscaProveedores()
    End Sub

#End Region

#Region "Metodos"

    Private Sub mCmbAlmacen()
        Try
            Dim sAlm As String
            sAlm = CmbAlmacen.SelectedItem.ToString()
            If (sAlm = "TODOS") Then
                sAlm = "01,03"
                ckConsolidado.Enabled = True
                ckConsolidado.Checked = True
            Else
                ckConsolidado.Checked = False
                ckConsolidado.Enabled = False
            End If


            'Ivan 10/02/2015 Metodo que cargara los nombres de las lineas
            mCargaLineaIni()
            CBProvTODOS(sAlm)
            'Limpiamos grid 
            DgRptCompras.DataSource = String.Empty

        Catch ex As Exception

            MsgBox("Error al cargar el metodo:" + Environment.NewLine + "-CmbAlmacen", MsgBoxStyle.Critical, "Tracto Partes Diamante")

        End Try
    End Sub


    Private Sub CBProvTODOS(ByVal sAlm As String)
        Try
            Dim ConsutaLista As String


            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                Dim DSetTablas As New DataSet

                ConsutaLista = "SELECT T0.cardcode, T1.cardname FROM SBO_TPD.DBO.OITM T0 "
                ConsutaLista &= "inner JOIN SBO_TPD.DBO.OCRD T1 ON T0.CARDCODE = T1.CardCode  "
                ConsutaLista &= "GROUP BY T0.CardCode, T1.CardName "


                Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


                daAgte.Fill(DSetTablas, "Proveedores")

                Dim filaAgte As Data.DataRow

                'Asignamos a fila la nueva Row(Fila)del Dataset
                filaAgte = DSetTablas.Tables("Proveedores").NewRow

                'Agregamos los valores a los campos de la tabla
                filaAgte("cardname") = "TODOS"
                filaAgte("cardcode") = "999"
                'filaAgte("itmsgrpcod") = 999

                'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                DSetTablas.Tables("Proveedores").Rows.Add(filaAgte)

                DvProveedoresT.Table = DSetTablas.Tables("Proveedores")

                Me.CBProveedor.DataSource = DvProveedoresT
                Me.CBProveedor.DisplayMember = "cardname"
                Me.CBProveedor.ValueMember = "cardcode"
                Me.CBProveedor.SelectedValue = "999"

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
            'MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub


    Private Sub mCargaLineaIni()          '(ByVal sAlm As String)
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                Dim ConsutaLista As String
                ConsutaLista = "SELECT T0.ItmsGrpCod,T1.ItmsGrpNam, T0.CardCode FROM OITM T0 LEFT JOIN OITB T1 ON T0.ItmsGrpCod=T1.ItmsGrpCod "
                ConsutaLista &= "GROUP BY T0.ItmsGrpCod,ItmsGrpNam,T0.CardCode"

                Dim da As New SqlDataAdapter(ConsutaLista, SqlConnection)


                Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

                Dim DSetTablas As New DataSet
                daAgte.Fill(DSetTablas, "Lineas")

                Dim filaAgte As Data.DataRow

                'Asignamos a fila la nueva Row(Fila)del Dataset
                filaAgte = DSetTablas.Tables("Lineas").NewRow

                'Agregamos los valores a los campos de la tabla
                filaAgte("ItmsGrpCod") = "0"
                filaAgte("ItmsGrpNam") = "TODAS"
                filaAgte("cardcode") = 999

                'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                DSetTablas.Tables("Lineas").Rows.Add(filaAgte)

                DvLineas.Table = DSetTablas.Tables("Lineas")

                Me.CmbLinIni.DataSource = DvLineas
                Me.CmbLinIni.DisplayMember = "ItmsGrpNam"
                Me.CmbLinIni.ValueMember = "ItmsGrpCod"
                Me.CmbLinIni.SelectedValue = "0"

            End Using

        Catch ex As Exception
            MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub

    Private Sub mCargaLineaIniTodas()
        Try
            Dim ConsutaLista As String


            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

                Dim DSetTablas As New DataSet

                ConsutaLista = "SELECT ItmsGrpCod, ItmsGrpNam FROM OITB "

                Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


                daAgte.Fill(DSetTablas, "LineasT")

                Dim filaAgte As Data.DataRow

                'Asignamos a fila la nueva Row(Fila)del Dataset
                filaAgte = DSetTablas.Tables("LineasT").NewRow

                'Agregamos los valores a los campos de la tabla
                filaAgte("ItmsGrpCod") = 0
                filaAgte("ItmsGrpNam") = "TODAS"

                'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                DSetTablas.Tables("LineasT").Rows.Add(filaAgte)

                DvLineasT.Table = DSetTablas.Tables("LineasT")

                Me.CmbLinIni.DataSource = DvLineasT
                Me.CmbLinIni.DisplayMember = "ItmsGrpNam"
                Me.CmbLinIni.ValueMember = "ItmsGrpCod"
                Me.CmbLinIni.SelectedValue = 0

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
            'MsgBox("Error al realizar la consulta de la linea" + Environment.NewLine + "-mCargaLineaIni()" & Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub

    Private Sub mLlenaGridCompras(ByVal iMes As Decimal, ByVal iDiasInv As Integer, ByVal dtFechaIni As DateTime,
                                  ByVal dtFechaFin As DateTime, ByVal iAlm1 As String, ByVal iAlm2 As String, ByVal iLinIni As Integer,
                                  ByVal prov As String)
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrProd)
                Dim byConsolidado As Integer
                byConsolidado = IIf(ckConsolidado.Checked, 1, 0)

                Dim da As New SqlDataAdapter("exec CmpRptAnalisisComprasPRUEBA4 " + iMes.ToString() + "," + iDiasInv.ToString() +
                                             ",'" + dtFechaIni.ToString("yyyy-MM-dd") + "','" + dtFechaFin.ToString("yyyy-MM-dd") +
                                             "','" + iAlm1.ToString() + "','" + iAlm2.ToString() + "'," + iLinIni.ToString() + "," + byConsolidado.ToString() +
                                             ",'" + prov.ToString() + "'", SqlConnection)

                da.SelectCommand.CommandTimeout = 250

                Dim ds As New DataSet
                da.Fill(ds)
                EstiloGrid(DgRptCompras, ds)
            End Using
            fFormatoGrid(DgRptCompras, CmbAlmacen.SelectedItem.ToString())

        Catch ex As Exception
            MsgBox("Error al cargar el metodo:" + Environment.NewLine + "-mLlenaGridCompras" + Environment.NewLine + ex.Message, MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub

#End Region

#Region "Funciones"

    Private Function fValidaDatos(ByRef sError) As Boolean
        Try
            Dim bValido As Boolean
            bValido = True
            If (CmbAlmacen.Text = String.Empty) Then
                sError = sError + Environment.NewLine + "-Almacen."
                bValido = False
                eAlm.Visible = True
            Else
                eAlm.Visible = False
            End If

            If (TxtDiasInv.Text = String.Empty) Then
                sError = sError + Environment.NewLine + "-Dias de inventario."
                bValido = False
                eInv.Visible = True
            Else
                eInv.Visible = False
            End If

            If (DateDiff("m", DtpFechaIni.Value, DtpFechaFin.Value) = -1 Or (DtpFechaIni.Value > DtpFechaFin.Value)) Then
                sError = sError + Environment.NewLine + "-La fecha final debe ser mayor ala fecha inicial minimo un mes."
                bValido = False
                eFin.Visible = True
                eIni.Visible = True
            Else
                eIni.Visible = False
                eFin.Visible = False
            End If

            If (DateDiff("m", DtpFechaIni.Value, DtpFechaFin.Value) > 12) Then
                sError = sError + Environment.NewLine + "-Debes seleccionar un intervalo menor a un año."
                bValido = False
                eFin.Visible = True
                eIni.Visible = True
            Else
                eIni.Visible = False
                eFin.Visible = False
            End If

            If (CmbLinIni.SelectedValue = 1) Then
                sError = sError + Environment.NewLine + "-Debes seleccionar una linea."
                bValido = False
                eLin.Visible = True
            Else
                eLin.Visible = False
            End If

            Return bValido
        Catch ex As Exception
            MsgBox("Error al realizar alguna validacion", MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try

    End Function

    Private Sub fFormatoGrid(ByVal Grid As DataGridView, ByVal bTodAlm As String)
        Try
            With Grid
                'Articulo	
                .Columns(0).HeaderText = "Codigo Articulo"
                .Columns(0).Width = 130
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Categoria	
                .Columns(1).HeaderText = "Categoría"
                .Columns(1).Width = 60
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Descripcion	
                .Columns(2).HeaderText = "Descripcion"
                .Columns(2).Width = 280

                'Linea	
                .Columns(3).HeaderText = "Linea"
                .Columns(3).Width = 100


                'Proveedor	
                .Columns(4).HeaderText = "Proveedor"
                .Columns(4).Width = 250

                '' "Ene"
                .Columns(5).Width = 40
                .Columns(5).DefaultCellStyle.Format = "###,###"
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Febrero"
                .Columns(6).Width = 40
                .Columns(6).DefaultCellStyle.Format = "###,###"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Marzo"
                .Columns(7).Width = 40
                .Columns(7).DefaultCellStyle.Format = "###,###"
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Abril"
                .Columns(8).Width = 40
                .Columns(8).DefaultCellStyle.Format = "###,###"
    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


    ' "Mayo"
    .Columns(9).Width = 40
                .Columns(9).DefaultCellStyle.Format = "###,###"
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Junio"
                .Columns(10).Width = 40
                .Columns(10).DefaultCellStyle.Format = "###,###"
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Julio"
                .Columns(11).Width = 40
                .Columns(11).DefaultCellStyle.Format = "###,###"
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Agosto"
                .Columns(12).Width = 40
                .Columns(12).DefaultCellStyle.Format = "###,###"
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Septiembre"
                .Columns(13).Width = 40
                .Columns(13).DefaultCellStyle.Format = "###,###"
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Octubre"
                .Columns(14).Width = 40
                .Columns(14).DefaultCellStyle.Format = "###,###"
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Noviembre"
                .Columns(15).Width = 40
                .Columns(15).DefaultCellStyle.Format = "###,###"
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Diciembre"
                .Columns(16).Width = 40
                .Columns(16).DefaultCellStyle.Format = "###,###"
                .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'VntaTotal
                .Columns(17).HeaderText = "Ventas totales"
                .Columns(17).Width = 60
                .Columns(17).DefaultCellStyle.Format = "###,###"
                .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'AlmPuebla
                .Columns(18).HeaderText = "Alm Pue."
                .Columns(18).Width = 45
                .Columns(18).DefaultCellStyle.Format = "###,###"
                .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                'AlmMerida
                .Columns(19).HeaderText = "Alm Mer."
                .Columns(19).Width = 45
                .Columns(19).DefaultCellStyle.Format = "###,###"
                .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                ' "Almacen"
                .Columns(20).Width = 65
                .Columns(20).DefaultCellStyle.Format = "###,###"
                .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Existencias"
                .Columns(21).Width = 77
                .Columns(21).DefaultCellStyle.Format = "###,###"
                .Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                '"Promedio vtasmensual"
                .Columns(22).HeaderText = "Prom. Vtas. Mensual"
                .Columns(22).Width = 85
                .Columns(22).DefaultCellStyle.Format = "###,###.###0"
                .Columns(22).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '"Punto de reorden"
                .Columns(23).HeaderText = "Punto de re-orden"
                .Columns(23).Width = 85
                .Columns(23).DefaultCellStyle.Format = "###,###.###0"
                .Columns(23).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '' "Comprometido"
                .Columns(24).Width = 108
                .Columns(24).DefaultCellStyle.Format = "###,###"
                .Columns(24).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '' "Solicitado"
                .Columns(25).Width = 75
                .Columns(25).DefaultCellStyle.Format = "###,###"
                .Columns(25).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '' "Pedido"
                .Columns(26).Width = 60
                .Columns(26).DefaultCellStyle.Format = "###,###"
                .Columns(26).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '' "PrecioL8"
                .Columns(27).HeaderText = "Precio L8"
                .Columns(27).Width = 68
                .Columns(27).DefaultCellStyle.Format = "$###,###..###0"
                .Columns(27).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '' "Importe"
                .Columns(28).Width = 90
                .Columns(28).DefaultCellStyle.Format = "$###,###..###0"
                .Columns(28).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(29).HeaderText = "Dias de inventario"
                .Columns(29).Width = 78
                .Columns(29).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(29).DefaultCellStyle.Format = "###,###"

                If bTodAlm = "TODOS" And ckConsolidado.Checked <> True Then
                    .Columns(17).Visible = False
                    .Columns(18).Visible = False
                    .Columns(19).Visible = False
                    .Columns(20).Visible = True
                    .Columns(21).HeaderText = "Existencia"

                Else
                    .Columns(17).Visible = True
                    .Columns(18).Visible = True
                    .Columns(19).Visible = True
                    .Columns(20).Visible = False
                    .Columns(21).HeaderText = "Existencia  total"
                End If
                If bTodAlm <> "TODOS" Then
                    .Columns(18).Visible = False
                    .Columns(19).Visible = False
                    '.Columns(16).Visible = False
                End If

                DgRptCompras.Columns(1).Frozen = True
            End With

        Catch ex As Exception
            MsgBox("Error al aplicar algun estilo al grid", MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try
    End Sub

#End Region


    'Private Sub DgRptCompras_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DgRptCompras.CellValidating
    '    BuscaProveedores()
    'End Sub

    Private Sub CmbLinIni_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbLinIni.KeyUp
        'BuscaProveedores()
    End Sub



    Private Sub CBProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBProveedor.SelectedIndexChanged
        BuscaLineas()
    End Sub

End Class

