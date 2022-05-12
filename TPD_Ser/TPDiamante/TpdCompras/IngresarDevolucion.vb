
Option Explicit On
'Option Strict On
Imports System.Data.SqlClient




Public Class IngresarDevolucion

    Public conexion As New SqlConnection(conexion_universal.CadenaSQLSAP)
    Public conexion2 As New SqlConnection(conexion_universal.CadenaSQL)

    Public DvDetalle As New DataView

    Public StrTpm As String = conexion_universal.CadenaSQL

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        DGDetalle.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES

        '***********BUSCAR POR*********** 
        '********NUMERO DE FACTURA*******
        '
        If TBDocNum.Text <> "" Then

            conexion.Open()
            Dim cmd As SqlCommand = New SqlCommand("SELECT T0.DocEntry,T0.cardcode,T0.cardname,case when T3.name IS NULL then '' else t3.NAME end as 'cntctcode', " &
            "CASE WHEN T0.numatcard IS NULL THEN '' ELSE T0.numatcard END AS 'numatcard',CASE WHEN T0.cursource IS NULL THEN '' ELSE T0.cursource END AS cursource, " &
            "CASE WHEN T0.series IS NULL THEN '' ELSE T2.seriesname END AS seriesname, " &
            "CASE WHEN T0.docstatus='C' THEN 'Cerrado' WHEN T0.printed='N' AND T0.docstatus='O' THEN 'Abierto' " &
            "WHEN (T0.printed='Y' OR T0.printed='A') AND T0.docstatus='O' THEN 'Abrir: Imprimido' ELSE '' END as 'status', " &
            "T0.docdate,T0.docduedate,T0.taxdate,T1.slpname,CASE WHEN t0.ownercode IS NULL THEN '' ELSE t0.ownercode END, " &
            "case when t0.Comments is null then '' else t0.Comments end as Comments, CASE WHEN T0.DiscPrcnt IS NULL THEN 0 ELSE CONVERT(DECIMAL(5,2),T0.DiscPrcnt) END AS DiscPrcnt, t0.doctype, " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),T0.vatsum) ELSE CONVERT(DECIMAL(9,2),T0.VatSumFC) END AS 'VATSUM', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),T0.DocTotal) ELSE CONVERT(DECIMAL(9,2),T0.DocTotalFC) END AS 'doctotal', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),t0.doctotal-t0.vatsum) ELSE CONVERT(DECIMAL(9,2),T0.DocTotalFC - T0.VatSumFC) END as 'antdesc', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),t0.paidtodate) ELSE CONVERT(DECIMAL(9,2),T0.PaidFC) end as 'impapli', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),T0.DocTotal - T0.PaidToDate) ELSE CONVERT(DECIMAL(9,2),T0.DocTotalFC - T0.PaidFC) END AS 'saldopendiente' " &
            "FROM OINV T0 " &
            "LEFT JOIN OSLP T1 ON T0.SlpCode=T1.SlpCode " &
            "LEFT JOIN NNM1 T2 ON T0.Series=T2.Series " &
            "LEFT JOIN OCPR T3 ON T0.CntctCode=T3.CntctCode where docnum = @docnum ", conexion)
            cmd.Parameters.AddWithValue("@docnum", TBDocNum.Text)

            Try

                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count > 0 Then

                    TBDocNum.BackColor = Color.White
                    TBCliente.BackColor = Color.White
                    TBNomCli.BackColor = Color.White

                    Dim row As DataRow = dt.Rows(0)

                    TBDocEntry.Text = CStr(row("docentry"))
                    TBCliente.Text = CStr(row("cardcode"))
                    TBNomCli.Text = CStr(row("cardname"))
                    TBPerCon.Text = CStr(row("cntctcode"))
                    'IIf(row(1) Is DbNull.Value, "", Convert.ToString(row(1)))         

                    TBNumRef.Text = IIf(CStr(row("numatcard")) Is DBNull.Value, "", Convert.ToString(CStr(row("numatcard"))))

                    If CStr(row("cursource")) = "L" Then
                        TBCurSource.Text = "Moneda local"
                    Else
                        TBCurSource.Text = "Moneda del sistema"
                    End If

                    TBSeries.Text = CStr(row("seriesname"))
                    TBEstado.Text = CStr(row("status"))
                    TBDocDate.Text = CStr(row("docdate"))
                    TBFecVen.Text = CStr(row("docduedate"))
                    TBFecDoc.Text = CStr(row("taxdate"))

                    If CStr(row("doctype")) = "I" Then
                        TBDocType.Text = "Artículo"
                    Else
                        TBDocType.Text = "Servicio"
                    End If

                    TBAgente.Text = CStr(row("slpname"))

                    TBComentarios.Text = IIf(CStr(row("comments")) Is DBNull.Value, "", Convert.ToString(CStr(row("comments"))))




                    TBDescuento.Text = CStr(row("DiscPrcnt"))
                    TBDescuento.TextAlign = HorizontalAlignment.Right

                    TBAntesDesc.Text = CStr(row("antdesc"))
                    TBAntesDesc.TextAlign = HorizontalAlignment.Right

                    TBImpuesto.Text = CStr(row("vatsum"))
                    TBImpuesto.TextAlign = HorizontalAlignment.Right

                    TBTotalDoc.Text = CStr(row("doctotal"))
                    TBTotalDoc.TextAlign = HorizontalAlignment.Right

                    TBImporteApli.Text = CStr(row("impapli"))
                    TBImporteApli.TextAlign = HorizontalAlignment.Right

                    TBSaldoPen.Text = CStr(row("saldopendiente"))
                    TBSaldoPen.TextAlign = HorizontalAlignment.Right

                    '--------------******************************************
                    '--------------******************************************
                    '--------------******************************************
                    conexion2.Open()
                    Dim cmd4 As SqlCommand = Nothing
                    cmd4 = New SqlCommand("DetalleDevoluciones", conexion2)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.Add("@docentry", SqlDbType.Int).Value = TBDocEntry.Text


                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()
                    Dim da2 As New SqlDataAdapter
                    da2.SelectCommand = cmd4
                    da2.SelectCommand.Connection = conexion2


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da2.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Detalle"


                    DvDetalle.Table = DsVtas.Tables("Detalle")

                    DGDetalle.DataSource = DvDetalle

                Else
                    MsgBox("No hay factura con este folio")
                End If


            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
                    conexion.Close()
                End If
                If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
                    conexion2.Close()
                End If
            End Try

            '***********************************************
            '***********************************************
            '***************CLAVE DEL CLIENTE***************
            '***********************************************
            '***********************************************

        ElseIf TBCliente.Text <> "" Then



            conexion.Open()
            Dim cmd As SqlCommand = New SqlCommand("SELECT T0.DocEntry,T0.DocNum,T0.cardname,case when T3.name IS NULL then '' else t3.NAME end as 'cntctcode', " &
            "CASE WHEN T0.numatcard IS NULL THEN '' ELSE T0.numatcard END AS 'numatcard',CASE WHEN T0.cursource IS NULL THEN '' ELSE T0.cursource END AS cursource, " &
            "CASE WHEN T0.series IS NULL THEN '' ELSE T2.seriesname END AS seriesname, " &
            "CASE WHEN T0.docstatus='C' THEN 'Cerrado' WHEN T0.printed='N' AND T0.docstatus='O' THEN 'Abierto' " &
            "WHEN (T0.printed='Y' OR T0.printed='A') AND T0.docstatus='O' THEN 'Abrir: Imprimido' ELSE '' END as 'status', " &
            "T0.docdate,T0.docduedate,T0.taxdate,T1.slpname,CASE WHEN t0.ownercode IS NULL THEN '' ELSE t0.ownercode END, " &
            "t0.Comments, CASE WHEN T0.DiscPrcnt IS NULL THEN 0 ELSE CONVERT(DECIMAL(9,2),T0.DiscPrcnt) END AS DiscPrcnt, t0.doctype, " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),T0.vatsum) ELSE CONVERT(DECIMAL(9,2),T0.VatSumFC) END AS 'VATSUM', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),T0.DocTotal) ELSE CONVERT(DECIMAL(9,2),T0.DocTotalFC) END AS 'doctotal', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),t0.doctotal-t0.vatsum) ELSE CONVERT(DECIMAL(9,2),T0.DocTotalFC - T0.VatSumFC) END as 'antdesc', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),t0.paidtodate) ELSE CONVERT(DECIMAL(9,2),T0.PaidFC) end as 'impapli', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),T0.DocTotal - T0.PaidToDate) ELSE CONVERT(DECIMAL(9,2),T0.DocTotalFC - T0.PaidFC) END AS 'saldopendiente' " &
            "FROM OINV T0 " &
            "LEFT JOIN OSLP T1 ON T0.SlpCode=T1.SlpCode " &
            "LEFT JOIN NNM1 T2 ON T0.Series=T2.Series " &
            "LEFT JOIN OCPR T3 ON T0.CntctCode=T3.CntctCode where T0.CardCode = @CardCode ", conexion)
            cmd.Parameters.AddWithValue("@CardCode", TBCliente.Text)

            Try

                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count = 1 Then

                    TBDocNum.BackColor = Color.White
                    TBCliente.BackColor = Color.White
                    TBNomCli.BackColor = Color.White

                    Dim row As DataRow = dt.Rows(0)

                    TBDocEntry.Text = CStr(row("docentry"))
                    TBDocNum.Text = CStr(row("DocNum"))
                    TBNomCli.Text = CStr(row("cardname"))
                    TBPerCon.Text = CStr(row("cntctcode"))
                    'IIf(row(1) Is DbNull.Value, "", Convert.ToString(row(1)))         

                    TBNumRef.Text = IIf(CStr(row("numatcard")) Is DBNull.Value, "", Convert.ToString(CStr(row("numatcard"))))

                    If CStr(row("cursource")) = "L" Then
                        TBCurSource.Text = "Moneda local"
                    Else
                        TBCurSource.Text = "Moneda del sistema"
                    End If

                    TBSeries.Text = CStr(row("seriesname"))
                    TBEstado.Text = CStr(row("status"))
                    TBDocDate.Text = CStr(row("docdate"))
                    TBFecVen.Text = CStr(row("docduedate"))
                    TBFecDoc.Text = CStr(row("taxdate"))

                    If CStr(row("doctype")) = "I" Then
                        TBDocType.Text = "Artículo"
                    Else
                        TBDocType.Text = "Servicio"
                    End If

                    TBAgente.Text = CStr(row("slpname"))

                    TBComentarios.Text = CStr(row("comments"))
                    TBDescuento.Text = CStr(row("DiscPrcnt"))
                    TBAntesDesc.Text = CStr(row("antdesc"))
                    TBImpuesto.Text = CStr(row("vatsum"))
                    TBTotalDoc.Text = CStr(row("doctotal"))
                    TBImporteApli.Text = CStr(row("impapli"))
                    TBSaldoPen.Text = CStr(row("saldopendiente"))


                    '--------------******************************************
                    '--------------******************************************
                    '--------------******************************************
                    conexion2.Open()
                    Dim cmd4 As SqlCommand = Nothing
                    cmd4 = New SqlCommand("DetalleDevoluciones", conexion2)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.Add("@docentry", SqlDbType.Int).Value = TBDocEntry.Text


                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()
                    Dim da2 As New SqlDataAdapter
                    da2.SelectCommand = cmd4
                    da2.SelectCommand.Connection = conexion2


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da2.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Detalle"


                    DvDetalle.Table = DsVtas.Tables("Detalle")

                    DGDetalle.DataSource = DvDetalle

                ElseIf dt.Rows.Count > 1 Then
                    'MsgBox("PARTE PARA PROGRAMAR")
                    Module1.NumCli = TBCliente.Text
                    FacturasDetalleCliente.Show()

                Else
                    MsgBox("No hay factura con este folio")
                End If


            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
                    conexion.Close()
                End If
                If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
                    conexion2.Close()
                End If
            End Try

            '***********************************************
            '***********************************************
            '**************NOMBRE DEL CLIENTE***************
            '***********************************************
            '***********************************************

        ElseIf TBNomCli.Text <> "" Then


            conexion.Open()
            Dim cmd As SqlCommand = New SqlCommand("SELECT T0.DocEntry,T0.DocNum,T0.cardcode,case when T3.name IS NULL then '' else t3.NAME end as 'cntctcode', " &
            "CASE WHEN T0.numatcard IS NULL THEN '' ELSE T0.numatcard END AS 'numatcard',CASE WHEN T0.cursource IS NULL THEN '' ELSE T0.cursource END AS cursource, " &
            "CASE WHEN T0.series IS NULL THEN '' ELSE T2.seriesname END AS seriesname, " &
            "CASE WHEN T0.docstatus='C' THEN 'Cerrado' WHEN T0.printed='N' AND T0.docstatus='O' THEN 'Abierto' " &
            "WHEN (T0.printed='Y' OR T0.printed='A') AND T0.docstatus='O' THEN 'Abrir: Imprimido' ELSE '' END as 'status', " &
            "T0.docdate,T0.docduedate,T0.taxdate,T1.slpname,CASE WHEN t0.ownercode IS NULL THEN '' ELSE t0.ownercode END, " &
            "t0.Comments, CASE WHEN T0.DiscPrcnt IS NULL THEN 0 ELSE CONVERT(DECIMAL(9,2),T0.DiscPrcnt) END AS DiscPrcnt, t0.doctype, " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),T0.vatsum) ELSE CONVERT(DECIMAL(9,2),T0.VatSumFC) END AS 'VATSUM', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),T0.DocTotal) ELSE CONVERT(DECIMAL(9,2),T0.DocTotalFC) END AS 'doctotal', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),t0.doctotal-t0.vatsum) ELSE CONVERT(DECIMAL(9,2),T0.DocTotalFC - T0.VatSumFC) END as 'antdesc', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),t0.paidtodate) ELSE CONVERT(DECIMAL(9,2),T0.PaidFC) end as 'impapli', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN CONVERT(DECIMAL(9,2),T0.DocTotal - T0.PaidToDate) ELSE CONVERT(DECIMAL(9,2),T0.DocTotalFC - T0.PaidFC) END AS 'saldopendiente' " &
            "FROM OINV T0 " &
            "LEFT JOIN OSLP T1 ON T0.SlpCode=T1.SlpCode " &
            "LEFT JOIN NNM1 T2 ON T0.Series=T2.Series " &
            "LEFT JOIN OCPR T3 ON T0.CntctCode=T3.CntctCode where T0.CardName LIKE @CardName + '%' ", conexion)
            cmd.Parameters.AddWithValue("@CardName", TBNomCli.Text)

            Try

                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count = 1 Then
                    Dim row As DataRow = dt.Rows(0)

                    TBDocEntry.Text = CStr(row("docentry"))
                    TBDocNum.Text = CStr(row("DocNum"))
                    TBCliente.Text = CStr(row("CardCode"))
                    TBPerCon.Text = CStr(row("cntctcode"))
                    'IIf(row(1) Is DbNull.Value, "", Convert.ToString(row(1)))         

                    TBNumRef.Text = IIf(CStr(row("numatcard")) Is DBNull.Value, "", Convert.ToString(CStr(row("numatcard"))))

                    If CStr(row("cursource")) = "L" Then
                        TBCurSource.Text = "Moneda local"
                    Else
                        TBCurSource.Text = "Moneda del sistema"
                    End If

                    TBSeries.Text = CStr(row("seriesname"))
                    TBEstado.Text = CStr(row("status"))
                    TBDocDate.Text = CStr(row("docdate"))
                    TBFecVen.Text = CStr(row("docduedate"))
                    TBFecDoc.Text = CStr(row("taxdate"))

                    If CStr(row("doctype")) = "I" Then
                        TBDocType.Text = "Artículo"
                    Else
                        TBDocType.Text = "Servicio"
                    End If

                    TBAgente.Text = CStr(row("slpname"))

                    TBComentarios.Text = CStr(row("comments"))
                    TBDescuento.Text = CStr(row("DiscPrcnt"))
                    TBAntesDesc.Text = CStr(row("antdesc"))
                    TBImpuesto.Text = CStr(row("vatsum"))
                    TBTotalDoc.Text = CStr(row("doctotal"))
                    TBImporteApli.Text = CStr(row("impapli"))
                    TBSaldoPen.Text = CStr(row("saldopendiente"))

                    '--------------******************************************
                    '--------------******************************************
                    '--------------******************************************
                    conexion2.Open()
                    Dim cmd4 As SqlCommand = Nothing
                    cmd4 = New SqlCommand("DetalleDevoluciones", conexion2)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.Add("@docentry", SqlDbType.Int).Value = TBDocEntry.Text


                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()
                    Dim da2 As New SqlDataAdapter
                    da2.SelectCommand = cmd4
                    da2.SelectCommand.Connection = conexion2


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da2.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Detalle"


                    DvDetalle.Table = DsVtas.Tables("Detalle")

                    DGDetalle.DataSource = DvDetalle


                    'DisenoGrid()

                ElseIf dt.Rows.Count > 1 Then
                    'MsgBox("PARTE PARA PROGRAMAR")
                    Module1.NomCli = TBNomCli.Text
                    Form3.Show()

                Else
                    MsgBox("No hay factura con este folio")
                End If


            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
                    conexion.Close()
                End If
                If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
                    conexion2.Close()
                End If
            End Try

        End If  'TBDocNum.Text <> ""

        DisenoGrid()
    End Sub

    Private Sub DisenoGrid()
        '-------Diseño de DATAGRID Totales
        With Me.DGDetalle
            '.DataSource = DtAgte
            '.ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue


            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'Color de linea del grid

            DGDetalle.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try

                'Catch ex As Exception

                'End Try

                .Columns(0).HeaderText = "Artículo"
                .Columns(0).Width = 100
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(0).ReadOnly = True

                .Columns(1).HeaderText = "Descripción"
                .Columns(1).Width = 190
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(1).ReadOnly = True

                .Columns(2).HeaderText = "Línea"
                .Columns(2).Width = 120
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(2).ReadOnly = True

                .Columns(3).HeaderText = "Proveedor"
                .Columns(3).Width = 140
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(3).ReadOnly = True
                .Columns(3).Visible = False

                .Columns(4).HeaderText = "Stock"
                .Columns(4).Width = 50
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).ReadOnly = True
                .Columns(4).Visible = False

                .Columns(5).HeaderText = "Almacén"
                .Columns(5).Width = 50
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).ReadOnly = True

                .Columns(6).HeaderText = "Cantidad"
                .Columns(6).Width = 50
                .Columns(6).DefaultCellStyle.Format = "#,##0"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).ReadOnly = True

                .Columns(7).HeaderText = "Lista de Precios"
                .Columns(7).Width = 50
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).ReadOnly = True

                .Columns(8).HeaderText = "Precio Unitario"
                .Columns(8).Width = 90
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).DefaultCellStyle.Format = "$ #,##0.###0"
                .Columns(8).ReadOnly = True

                .Columns(9).HeaderText = "% de descuento"
                .Columns(9).Width = 80
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).DefaultCellStyle.Format = "#0.#0 %"
                .Columns(9).ReadOnly = True

                .Columns(10).HeaderText = "Precio tras el descuento"
                .Columns(10).Width = 90
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).DefaultCellStyle.Format = "$ ###,##0.#0"
                .Columns(10).ReadOnly = True

                .Columns(11).HeaderText = "Moneda"
                .Columns(11).Width = 90
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(11).DefaultCellStyle.Format = "$ ###,##0.#0"
                .Columns(11).Visible = False


                .Columns(12).HeaderText = "Indicador de impuestos"
                .Columns(12).Width = 100
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(12).Visible = False
                .Columns(12).ReadOnly = True

                .Columns(13).HeaderText = "Total (ML)"
                .Columns(13).Width = 100
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(13).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(13).ReadOnly = True

                .Columns(14).HeaderText = "Comisión"
                .Columns(14).Width = 70
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(14).DefaultCellStyle.Format = "##0.#0"
                .Columns(14).ReadOnly = True

                .Columns(15).HeaderText = "Precio bruto"
                .Columns(15).Width = 75
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(15).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(15).Visible = False

                .Columns(16).HeaderText = "Precio de coste ingreso bruto"
                .Columns(16).Width = 80
                .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(16).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(16).Visible = False

                .Columns(17).HeaderText = "Ingreso bruto (ML)"
                .Columns(17).Width = 75
                .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(17).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(17).Visible = False

                .Columns(18).HeaderText = "Total bruto (ML)"
                .Columns(18).Width = 80
                .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(18).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(18).Visible = False

                .Columns(19).Name = "CantidadGar"
                .Columns(19).HeaderText = "Cantidad"
                .Columns(19).Width = 60
                .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(19).DefaultCellStyle.Format = "###,###,###"
                '.Columns(19).ReadOnly = True

                .Columns(20).HeaderText = "DocEntry"
                .Columns(20).Width = 60
                .Columns(20).Visible = False

                ' creación de una columna de tipo botón
                Dim colBoton2 As DataGridViewButtonColumn = New DataGridViewButtonColumn()
                colBoton2.Name = "Agregar"
                colBoton2.HeaderText = "+"
                colBoton2.Text = "+"
                colBoton2.UseColumnTextForButtonValue = True
                colBoton2.Width = 30
                colBoton2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                ' añadir la columna de tipo botón al DataGridView
                Me.DGDetalle.Columns.Add(colBoton2)

                ' creación de una columna de tipo botón
                Dim colBoton As DataGridViewButtonColumn = New DataGridViewButtonColumn()
                colBoton.Name = "detalle"
                colBoton.HeaderText = "Detalle"
                colBoton.Text = "Ver detalle"
                colBoton.UseColumnTextForButtonValue = True
                colBoton.Width = 70
                ' añadir la columna de tipo botón al DataGridView
                Me.DGDetalle.Columns.Add(colBoton)

            Catch ex As Exception

            End Try


        End With
    End Sub








    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Module1.FacturaDev = String.Empty
        Module1.ArticuloDev = String.Empty

        Dim frm As New Devoluciones()
        'Mostramos el formulario Form2.
        frm.Show()

        Me.Close()
        'End If
    End Sub

    Private Sub IngresarDevolucion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TBDocNum.BackColor = Color.Cornsilk
    End Sub

    Private Sub DGDetalle_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalle.CellEndEdit
        If Me.DGDetalle.Columns(e.ColumnIndex).Name = "CantidadGar" Then

            'MsgBox("entro al procedimiento")


            ''OBTENER CANTIDAD 
            Dim Aux As Integer

            conexion2.Open()
            Dim cmd As SqlCommand = New SqlCommand("SELECT CASE WHEN CANTIDAD IS NULL THEN 0 ELSE CANTIDAD END AS 'Cantidad' FROM TPM.DBO.FacturaDevolucion WHERE Factura=@Factura AND Itemcode=@Itemcode ", conexion2)
            cmd.Parameters.AddWithValue("@Factura", TBDocNum.Text)
            'cmd.Parameters.AddWithValue("@Fecha", Date.Parse(TBDocDate.Text).Date.ToString("yyyyMMdd"))
            cmd.Parameters.AddWithValue("@Itemcode", DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value)
            'cmd.Parameters.AddWithValue("@Cantidad", TBDocNum.Text)

            Try

                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count > 0 Then

                    Dim row As DataRow = dt.Rows(0)

                    Aux = CStr(row("cantidad"))

                    'Else
                    '    MsgBox("No hay factura con este folio")
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
                    conexion2.Close()
                End If
            End Try
            ''FIN CANTIDAD



            'MsgBox(DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value + Aux)

            'If Aux > 0 Then

            'End If

            If DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value + Aux > DGDetalle.Item(6, DGDetalle.CurrentRow.Index).Value Then
                MessageBox.Show("Número de devoluciones sobrepasa el total de Artículos.",
                                               "Advertencia",
                                               MessageBoxButtons.OK,
                                              MessageBoxIcon.Exclamation)

                DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value = Aux

            ElseIf DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value > 0 And DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value <= DGDetalle.Item(6, DGDetalle.CurrentRow.Index).Value Then

                Dim cant As Integer = DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value
                Dim art As String = DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value.ToString

                If (MessageBox.Show("¿Confirma que desea agregar " & cant & " devolucion(s) del artículo " & art & "?",
                                   "Advertencia",
                                   MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

                    Try

                        Dim cnn As SqlConnection = Nothing

                        Dim cmd4 As SqlCommand = Nothing



                        Try

                            cnn = New SqlConnection(StrTpm)

                            'SELECT Estado 0,FecSuc 1,FecAlm 2,Factura 3,FecFac 4,DiasTransFecFacFecRecAlm 5,CardCode 6,CardName 7,
                            'Sucursal 8, Almacen 9, Cantidad 10, ItemCode 11, ItemName 12, ItmsGrpNam 13, "
                            'Proveedor 14,Id 15"


                            cmd4 = New SqlCommand("SPActualizaDetFactDevolucion2", cnn)
                            cmd4.CommandType = CommandType.StoredProcedure
                            cmd4.Parameters.AddWithValue("@Factura", TBDocNum.Text)
                            cmd4.Parameters.AddWithValue("@Fecha", Date.Parse(TBDocDate.Text).Date.ToString("yyyyMMdd"))
                            cmd4.Parameters.AddWithValue("@Itemcode", DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value)
                            cmd4.Parameters.AddWithValue("@Cantidad", DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value + Aux)

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


                        'Next
                        'FIN ACTUALIZAR REGISTROS EN FACTGAR


                        ''OBTENER ID 
                        Dim ValMax As Integer

                        conexion2.Open()
                        Dim cmd3 As SqlCommand = New SqlCommand("SELECT case when MAX(Id) IS NULL then 0 else max(id) end as 'id' FROM TPM.DBO.DetalleFacturaDevolucion WHERE Factura=@Factura AND Itemcode=@Itemcode ", conexion2)
                        cmd3.Parameters.AddWithValue("@Factura", TBDocNum.Text)
                        'cmd.Parameters.AddWithValue("@Fecha", Date.Parse(TBDocDate.Text).Date.ToString("yyyyMMdd"))
                        cmd3.Parameters.AddWithValue("@Itemcode", DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value)
                        'cmd.Parameters.AddWithValue("@Cantidad", TBDocNum.Text)

                        Try

                            Dim da3 As SqlDataAdapter = New SqlDataAdapter(cmd3)
                            Dim dt3 As New DataTable
                            da3.Fill(dt3)

                            If dt3.Rows.Count > 0 Then

                                Dim row As DataRow = dt3.Rows(0)

                                ValMax = CStr(row("Id")) + 1

                                'Else
                                '    MsgBox("No hay factura con este folio")
                            End If

                        Catch ex As Exception
                            MsgBox(ex.Message)
                        Finally
                            If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
                                conexion2.Close()
                            End If
                        End Try
                        ''FIN ID

                        'GUARDA REGISTROS EN DETFACTGAR
                        'For i As Integer = 1 To Module1.CantidadGar

                        Dim cnn2 As SqlConnection = Nothing

                        Dim cmd2 As SqlCommand = Nothing

                        Try
                            cnn2 = New SqlConnection(StrTpm)

                            cmd2 = New SqlCommand("SPGuardaDetFactDevolucion", cnn2)
                            cmd2.CommandType = CommandType.StoredProcedure

                            cmd2.Parameters.AddWithValue("@Estado", "NO EMPEZADA")
                            'cmd2.Parameters.AddWithValue("@FecSuc", "20150101")
                            'cmd2.Parameters.AddWithValue("@FecAlm", "20150101")
                            cmd2.Parameters.AddWithValue("@Folio", "")
                            cmd2.Parameters.AddWithValue("@Factura", TBDocNum.Text)
                            cmd2.Parameters.AddWithValue("@FecFac", Date.Parse(TBDocDate.Text).Date.ToString("yyyyMMdd"))
                            cmd2.Parameters.AddWithValue("@CardCode", TBCliente.Text)
                            cmd2.Parameters.AddWithValue("@CardName", TBNomCli.Text)
                            cmd2.Parameters.AddWithValue("@Sucursal", "")
                            cmd2.Parameters.AddWithValue("@Almacen", DGDetalle.Item(5, DGDetalle.CurrentRow.Index).Value)
                            cmd2.Parameters.AddWithValue("@Cantidad", DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value)
                            cmd2.Parameters.AddWithValue("@Itemcode", DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value)
                            cmd2.Parameters.AddWithValue("@ItemName", DGDetalle.Item(1, DGDetalle.CurrentRow.Index).Value)
                            cmd2.Parameters.AddWithValue("@ItmsGrpNam", DGDetalle.Item(2, DGDetalle.CurrentRow.Index).Value)
                            cmd2.Parameters.AddWithValue("@Proveedor", DGDetalle.Item(3, DGDetalle.CurrentRow.Index).Value)
                            cmd2.Parameters.AddWithValue("@Causa", "")
                            'cmd2.Parameters.AddWithValue("@FecEntcompRec", "20150101")
                            'cmd2.Parameters.AddWithValue("@FecEntProvRev", "20150101")
                            cmd2.Parameters.AddWithValue("@Seguimiento", "")
                            cmd2.Parameters.AddWithValue("@Dictamen", "")
                            'cmd2.Parameters.AddWithValue("@FecDic", "20150101")
                            'cmd2.Parameters.AddWithValue("@FecEntResAlm", "20150101")
                            cmd2.Parameters.AddWithValue("@Seguimiento2", "")
                            cmd2.Parameters.AddWithValue("@NumGuiEnv", "")
                            'cmd2.Parameters.AddWithValue("@FecEnv", "")
                            'cmd2.Parameters.AddWithValue("@FecEntCon", "20150101")
                            cmd2.Parameters.AddWithValue("@NotCre", "")
                            'cmd2.Parameters.AddWithValue("@FecNota", "20150101")
                            'cmd2.Parameters.AddWithValue("@FecEntComMov", "20150101")
                            cmd2.Parameters.AddWithValue("@TipoDoc", "")
                            cmd2.Parameters.AddWithValue("@Docto", 0)
                            'cmd2.Parameters.AddWithValue("@FecDoc", "20150101")
                            'cmd2.Parameters.AddWithValue("@FecEntAl", "20150101")

                            cmd2.Parameters.AddWithValue("@Id", ValMax)

                            cmd2.Parameters.AddWithValue("@DiasTransFecFacFecRecAlm", 0)
                            cmd2.Parameters.AddWithValue("@DiasTransFecRecCompFecProv", 0)
                            cmd2.Parameters.AddWithValue("@RespProv", "")
                            cmd2.Parameters.AddWithValue("@NumRespProv", "")
                            cmd2.Parameters.AddWithValue("@FecRespProv", "20150101")
                            cmd2.Parameters.AddWithValue("@DiasTransEnvio", 0)
                            cnn2.Open()

                            cmd2.ExecuteNonQuery()
                            cmd2.Connection.Close()

                            Dim da As New SqlDataAdapter
                            da.SelectCommand = cmd2
                            da.SelectCommand.Connection = cnn2


                            ''--------------------------------------------
                            'Dim DsVtas As New DataSet
                            'da.Fill(DsVtas, "DsVtas")

                            ''DsVtas.Tables(0).TableName = "Inventario"

                            ''DvInventario.Table = DsVtas.Tables("Inventario")

                            ''DGInventario.DataSource = DvInventario

                        Catch ex As Exception
                            MsgBox(ex.Message)
                        Finally
                            If cnn2 IsNot Nothing AndAlso cnn2.State <> ConnectionState.Closed Then
                                cnn2.Close()
                            End If
                        End Try
                        ''FIN GUARDAR REGISTROS EN DETFACTGAR
                        DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value = DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value + Aux

                        MessageBox.Show("Datos actualizados correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Catch ex As Exception

                    End Try

                Else
                    DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value = Aux
                End If
            End If

        End If
    End Sub


    Private Sub AgregaArticulo()

        Try

            'MsgBox("SI LLEGA")

            If DGDetalle.RowCount > 0 Then
                If DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value <> 0 Or
                    DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value.ToString <> "" Then

                    Dim Anio As String = Mid(TBDocDate.Text, 7, 4)
                    Dim Mes As String = Mid(TBDocDate.Text, 4, 2)
                    Dim Dia As String = Mid(TBDocDate.Text, 1, 2)

                    Dim Fechafact As String = Anio & Mes & Dia

                    'MsgBox(Fechafact)

                    'GUARDA REGISTROS EN FACTGAR

                    Dim cnn As SqlConnection = Nothing

                    Dim cmd4 As SqlCommand = Nothing

                    Try
                        cnn = New SqlConnection(StrTpm)

                        cmd4 = New SqlCommand("SPGuardaFactDevolucion", cnn)
                        cmd4.CommandType = CommandType.StoredProcedure
                        cmd4.Parameters.AddWithValue("@Factura", TBDocNum.Text)
                        cmd4.Parameters.AddWithValue("@Fecha", Fechafact)
                        cmd4.Parameters.AddWithValue("@Itemcode", DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value)
                        cmd4.Parameters.AddWithValue("@Cantidad", DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value)

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
                        MsgBox(ex.Message)
                    Finally
                        If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                            cnn.Close()
                        End If
                    End Try
                    'FIN GUARDAR REGISTROS EN FACTGAR

                    Module1.FacturaDev = TBDocNum.Text
                    Module1.FecFactDev = TBDocDate.Text
                    Module1.CodClienteDev = TBCliente.Text
                    Module1.NomClienteDev = TBNomCli.Text
                    Module1.GarAlmDev = DGDetalle.Item(5, DGDetalle.CurrentRow.Index).Value
                    Module1.ArticuloDev = DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value
                    Module1.DescripcionDev = DGDetalle.Item(1, DGDetalle.CurrentRow.Index).Value
                    Module1.LineaDev = DGDetalle.Item(2, DGDetalle.CurrentRow.Index).Value
                    Module1.ProveedorDev = DGDetalle.Item(3, DGDetalle.CurrentRow.Index).Value
                    Module1.CantidadGarDev = 1

                    'GUARDA REGISTROS EN DETFACTGAR
                    For i As Integer = 1 To Module1.CantidadGarDev

                        Dim cnn2 As SqlConnection = Nothing

                        Dim cmd2 As SqlCommand = Nothing

                        Try
                            cnn2 = New SqlConnection(StrTpm)

                            cmd2 = New SqlCommand("SPGuardaDetFactDevolucion", cnn2)
                            cmd2.CommandType = CommandType.StoredProcedure

                            '"Status", Date.Now, Date.Now, "Folio", Module1.Factura, Module1.FecFact, Module1.CodCliente, Module1.NomCliente, "Sucursal",
                            'Module1.GarAlm, Module1.CantidadGar, Module1.Articulo, Module1.Descripcion, Module1.Linea, Module1.Proveedor, "Causa",
                            'Date.Now, Date.Now, "Seguimiento", "", Date.Now, Date.Now, Date.Now, "NC", Date.Now, Date.Now, "Seguimiento",
                            '"", "NumDoc", Date.Now, Date.Now


                            'Dim Anio As String = Mid(Date.Now, 7, 4)
                            'Dim Mes As String = Mid(Date.Now, 4, 2)
                            'Dim Dia As String = Mid(Date.Now, 1, 2)
                            'Dim Fechafact As String = Anio & Mes & Dia

                            cmd2.Parameters.AddWithValue("@Estado", "NO EMPEZADA")
                            'cmd2.Parameters.AddWithValue("@FecSuc", "20150101")
                            'cmd2.Parameters.AddWithValue("@FecAlm", "20150101")
                            cmd2.Parameters.AddWithValue("@Folio", "")
                            cmd2.Parameters.AddWithValue("@Factura", Module1.FacturaDev)
                            cmd2.Parameters.AddWithValue("@FecFac", Fechafact)
                            cmd2.Parameters.AddWithValue("@CardCode", Module1.CodClienteDev)
                            cmd2.Parameters.AddWithValue("@CardName", Module1.NomClienteDev)
                            cmd2.Parameters.AddWithValue("@Sucursal", "")
                            cmd2.Parameters.AddWithValue("@Almacen", Module1.GarAlmDev)
                            cmd2.Parameters.AddWithValue("@Cantidad", 1)
                            cmd2.Parameters.AddWithValue("@Itemcode", Module1.ArticuloDev)
                            cmd2.Parameters.AddWithValue("@ItemName", Module1.DescripcionDev)
                            cmd2.Parameters.AddWithValue("@ItmsGrpNam", Module1.LineaDev)
                            cmd2.Parameters.AddWithValue("@Proveedor", Module1.ProveedorDev)
                            cmd2.Parameters.AddWithValue("@Causa", "")
                            'cmd2.Parameters.AddWithValue("@FecEntcompRec", "20150101")
                            'cmd2.Parameters.AddWithValue("@FecEntProvRev", "20150101")
                            cmd2.Parameters.AddWithValue("@Seguimiento", "")
                            cmd2.Parameters.AddWithValue("@Dictamen", "")
                            'cmd2.Parameters.AddWithValue("@FecDic", "20150101")
                            'cmd2.Parameters.AddWithValue("@FecEntResAlm", "20150101")
                            cmd2.Parameters.AddWithValue("@Seguimiento2", "")
                            cmd2.Parameters.AddWithValue("@NumGuiEnv", 0)
                            'cmd2.Parameters.AddWithValue("@FecEnv", "")
                            'cmd2.Parameters.AddWithValue("@FecRecSuc", "")
                            'cmd2.Parameters.AddWithValue("@FecEntCon", "20150101")
                            cmd2.Parameters.AddWithValue("@NotCre", "")
                            'cmd2.Parameters.AddWithValue("@FecNota", "20150101")
                            'cmd2.Parameters.AddWithValue("@FecEntComMov", "20150101")
                            cmd2.Parameters.AddWithValue("@TipoDoc", "")
                            cmd2.Parameters.AddWithValue("@Docto", "")
                            'cmd2.Parameters.AddWithValue("@FecDoc", "20150101")
                            'cmd2.Parameters.AddWithValue("@FecEntAl", "20150101")
                            cmd2.Parameters.AddWithValue("@Id", i)

                            cmd2.Parameters.AddWithValue("@DiasTransFecFacFecRecAlm", 0)
                            cmd2.Parameters.AddWithValue("@DiasTransFecRecCompFecProv", 0)
                            cmd2.Parameters.AddWithValue("@RespProv", "")
                            cmd2.Parameters.AddWithValue("@NumRespProv", "")
                            cmd2.Parameters.AddWithValue("@FecRespProv", "20150101")
                            cmd2.Parameters.AddWithValue("@DiasTransEnvio", 0)


                            cnn2.Open()

                            cmd2.ExecuteNonQuery()
                            cmd2.Connection.Close()

                            Dim da As New SqlDataAdapter
                            da.SelectCommand = cmd2
                            da.SelectCommand.Connection = cnn2


                            ''--------------------------------------------
                            'Dim DsVtas As New DataSet
                            'da.Fill(DsVtas, "DsVtas")

                            ''DsVtas.Tables(0).TableName = "Inventario"

                            ''DvInventario.Table = DsVtas.Tables("Inventario")

                            ''DGInventario.DataSource = DvInventario

                        Catch ex As Exception
                            MsgBox(ex.Message)
                        Finally
                            If cnn2 IsNot Nothing AndAlso cnn2.State <> ConnectionState.Closed Then
                                cnn2.Close()
                            End If
                        End Try
                    Next
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ActualizaArticulo()

        Try


            If DGDetalle.RowCount > 0 Then
                If DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value <> 0 Or
                    DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value.ToString <> "" Then

                    Dim Anio As String = Mid(TBDocDate.Text, 7, 4)
                    Dim Mes As String = Mid(TBDocDate.Text, 4, 2)
                    Dim Dia As String = Mid(TBDocDate.Text, 1, 2)

                    Dim Fechafact As String = Anio & Mes & Dia

                    Dim cnn As SqlConnection = Nothing

                    Dim cmd4 As SqlCommand = Nothing

                    Try
                        cnn = New SqlConnection(StrTpm)

                        cmd4 = New SqlCommand("SPActualizaDetFactDevolucion3", cnn)
                        cmd4.CommandType = CommandType.StoredProcedure
                        cmd4.Parameters.AddWithValue("@Factura", TBDocNum.Text)
                        cmd4.Parameters.AddWithValue("@Fecha", Fechafact)
                        cmd4.Parameters.AddWithValue("@Itemcode", DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value)
                        cmd4.Parameters.AddWithValue("@Cantidad", DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value)

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
                        MsgBox(ex.Message)
                    Finally
                        If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                            cnn.Close()
                        End If
                    End Try
                    'FIN GUARDAR REGISTROS EN FACTGAR

                    Module1.FacturaDev = TBDocNum.Text
                    Module1.FecFactDev = TBDocDate.Text
                    Module1.CodClienteDev = TBCliente.Text
                    Module1.NomClienteDev = TBNomCli.Text
                    Module1.GarAlmDev = DGDetalle.Item(5, DGDetalle.CurrentRow.Index).Value
                    Module1.ArticuloDev = DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value
                    Module1.DescripcionDev = DGDetalle.Item(1, DGDetalle.CurrentRow.Index).Value
                    Module1.LineaDev = DGDetalle.Item(2, DGDetalle.CurrentRow.Index).Value
                    Module1.ProveedorDev = DGDetalle.Item(3, DGDetalle.CurrentRow.Index).Value
                    Module1.CantidadGarDev = DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value

                    'GUARDA REGISTROS EN DETFACTGAR
                    'For i As Integer = 1 To Module1.CantidadGar

                    Dim cnn2 As SqlConnection = Nothing

                    Dim cmd2 As SqlCommand = Nothing

                    Try
                        cnn2 = New SqlConnection(StrTpm)

                        cmd2 = New SqlCommand("SPGuardaDetFactDevolucion", cnn2)
                        cmd2.CommandType = CommandType.StoredProcedure

                        '"Status", Date.Now, Date.Now, "Folio", Module1.Factura, Module1.FecFact, Module1.CodCliente, Module1.NomCliente, "Sucursal",
                        'Module1.GarAlm, Module1.CantidadGar, Module1.Articulo, Module1.Descripcion, Module1.Linea, Module1.Proveedor, "Causa",
                        'Date.Now, Date.Now, "Seguimiento", "", Date.Now, Date.Now, Date.Now, "NC", Date.Now, Date.Now, "Seguimiento",
                        '"", "NumDoc", Date.Now, Date.Now


                        'Dim Anio As String = Mid(Date.Now, 7, 4)
                        'Dim Mes As String = Mid(Date.Now, 4, 2)
                        'Dim Dia As String = Mid(Date.Now, 1, 2)
                        'Dim Fechafact As String = Anio & Mes & Dia

                        cmd2.Parameters.AddWithValue("@Estado", "NO EMPEZADA")
                        'cmd2.Parameters.AddWithValue("@FecSuc", "20150101")
                        'cmd2.Parameters.AddWithValue("@FecAlm", "20150101")
                        cmd2.Parameters.AddWithValue("@Folio", "")
                        cmd2.Parameters.AddWithValue("@Factura", Module1.FacturaDev)
                        cmd2.Parameters.AddWithValue("@FecFac", Fechafact)
                        cmd2.Parameters.AddWithValue("@CardCode", Module1.CodClienteDev)
                        cmd2.Parameters.AddWithValue("@CardName", Module1.NomClienteDev)
                        cmd2.Parameters.AddWithValue("@Sucursal", "")
                        cmd2.Parameters.AddWithValue("@Almacen", Module1.GarAlmDev)
                        cmd2.Parameters.AddWithValue("@Cantidad", 1)
                        cmd2.Parameters.AddWithValue("@Itemcode", Module1.ArticuloDev)
                        cmd2.Parameters.AddWithValue("@ItemName", Module1.DescripcionDev)
                        cmd2.Parameters.AddWithValue("@ItmsGrpNam", Module1.LineaDev)
                        cmd2.Parameters.AddWithValue("@Proveedor", Module1.ProveedorDev)
                        cmd2.Parameters.AddWithValue("@Causa", "")
                        'cmd2.Parameters.AddWithValue("@FecEntcompRec", "20150101")
                        'cmd2.Parameters.AddWithValue("@FecEntProvRev", "20150101")
                        cmd2.Parameters.AddWithValue("@Seguimiento", "")
                        cmd2.Parameters.AddWithValue("@Dictamen", "")
                        'cmd2.Parameters.AddWithValue("@FecDic", "20150101")
                        'cmd2.Parameters.AddWithValue("@FecEntResAlm", "20150101")
                        cmd2.Parameters.AddWithValue("@Seguimiento2", "")
                        cmd2.Parameters.AddWithValue("@NumGuiEnv", "")
                        'cmd2.Parameters.AddWithValue("@FecEnv", "")
                        'cmd2.Parameters.AddWithValue("@FecEntCon", "20150101")
                        cmd2.Parameters.AddWithValue("@NotCre", "")
                        'cmd2.Parameters.AddWithValue("@FecNota", "20150101")
                        'cmd2.Parameters.AddWithValue("@FecEntComMov", "20150101")
                        cmd2.Parameters.AddWithValue("@TipoDoc", "")
                        cmd2.Parameters.AddWithValue("@Docto", 0)
                        'cmd2.Parameters.AddWithValue("@FecDoc", "20150101")
                        'cmd2.Parameters.AddWithValue("@FecEntAl", "20150101")
                        cmd2.Parameters.AddWithValue("@Id", Module1.CantidadGarDev)

                        cmd2.Parameters.AddWithValue("@DiasTransFecFacFecRecAlm", 0)
                        cmd2.Parameters.AddWithValue("@DiasTransFecRecCompFecProv", 0)
                        cmd2.Parameters.AddWithValue("@RespProv", "")
                        cmd2.Parameters.AddWithValue("@NumRespProv", "")
                        cmd2.Parameters.AddWithValue("@FecRespProv", "20150101")
                        cmd2.Parameters.AddWithValue("@DiasTransEnvio", 0)
                        cnn2.Open()

                        cmd2.ExecuteNonQuery()
                        cmd2.Connection.Close()

                        Dim da As New SqlDataAdapter
                        da.SelectCommand = cmd2
                        da.SelectCommand.Connection = cnn2


                        ''--------------------------------------------
                        'Dim DsVtas As New DataSet
                        'da.Fill(DsVtas, "DsVtas")

                        ''DsVtas.Tables(0).TableName = "Inventario"

                        ''DvInventario.Table = DsVtas.Tables("Inventario")

                        ''DGInventario.DataSource = DvInventario

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        If cnn2 IsNot Nothing AndAlso cnn2.State <> ConnectionState.Closed Then
                            cnn2.Close()
                        End If
                    End Try


                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub DGDetalle_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalle.CellContentClick
        ''If String.IsNullOrEmpty(DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value.ToString) Then
        'Module1.Factura = TBDocNum.Text
        'Module1.Articulo = DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value
        Try

            ''AGREGAR GARANTIAS	
            If Me.DGDetalle.Columns(e.ColumnIndex).Name = "Agregar" Then

                Try
                    Dim cant As Integer = DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value
                    Dim art As String = DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value.ToString

                    If String.IsNullOrEmpty(DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value) Then
                        'DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value = 1
                        If (MessageBox.Show("¿Confirma que desea agregar 1 devolución del artículo " & DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value & "?",
                                               "Advertencia",
                                               MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
                            DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value = 1

                            AgregaArticulo()

                        End If

                    ElseIf DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value = 0 Then
                        If (MessageBox.Show("¿Confirma que desea agregar 1 devolución del artículo " & DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value & "?",
                                               "Advertencia",
                                               MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
                            DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value = 1

                            AgregaArticulo()

                        End If

                    ElseIf DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value > 0 And DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value < DGDetalle.Item(6, DGDetalle.CurrentRow.Index).Value Then


                        If (MessageBox.Show("¿Confirma que desea agregar 1 devolución del artículo " & art & "?",
                                           "Advertencia",
                                           MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

                            ''-------------------
                            DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value = DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value + 1

                            ActualizaArticulo()

                        End If

                    ElseIf cant > DGDetalle.Item(6, DGDetalle.CurrentRow.Index).Value Then
                        MessageBox.Show("Ingrese valor válido, número de devoluciones mayor a cantidad de artículos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                Catch ex As Exception
                    If (MessageBox.Show("¿Confirma que desea agregar 1 devolución del artículo " & DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value & "?",
                                               "Advertencia",
                                               MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
                        DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value = 1

                        AgregaArticulo()

                    End If

                End Try

            End If




            If Me.DGDetalle.Columns(e.ColumnIndex).Name = "detalle" And DGDetalle.Item(19, DGDetalle.CurrentRow.Index).Value > 0 Then

                Module1.FacturaDev = TBDocNum.Text
                Module1.ArticuloDev = DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value



                Dim frm As New Devoluciones()
                'Mostramos el formulario Form2.
                frm.Show()

                Me.Close()

                'End If

            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGDetalle_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DGDetalle.CellPainting
        Try

            If DGDetalle.Columns(e.ColumnIndex).Name = "Agregar" Then

                e.Paint(e.CellBounds, DataGridViewPaintParts.All)

                Dim celBoton As DataGridViewButtonCell = TryCast(Me.DGDetalle.Rows(e.RowIndex).Cells("Agregar"), DataGridViewButtonCell)
        'Dim icoAtomico As New Icon(Environment.CurrentDirectory + "C:\Users\Juan\Downloads\ICONOS VB.NET\1000-iconos-formato-png.png")
        Dim icoAtomico As New Icon("\\" & conexion_universal.RutaReportes & "\b1_shr\TPD\Icons\add.ico")  'C:\Users\Juan\Downloads\add.ico
        'C:\Users\Juan\Desktop\TPD\TPD10102016\Proyecto Sin c#_sinPJ-UsrsCompras-Inv-Emb\TPDiamante\TPDiamante\Icons
        e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 6, e.CellBounds.Top + 3)

                'Me.DGDetalle.Rows(e.RowIndex).Height = icoAtomico.Height + 10
                'Me.DGDetalle.Columns(e.ColumnIndex).Width = icoAtomico.Width + 10

                e.Handled = True

            End If


        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub
End Class