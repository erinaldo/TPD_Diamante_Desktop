
Option Explicit On
'Option Strict On
Imports System.Data.SqlClient

Public Class TransfStock

    Public conexion As New SqlConnection(conexion_universal.CadenaSQLSAP)
    Public conexion2 As New SqlConnection(conexion_universal.CadenaSQL)

    Public DvDetalle As New DataView

    Private Sub TransfStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexion.Open()
        Dim cmd As SqlCommand = New SqlCommand("SELECT T0.DOCENTRY,t0.docnum, case when T0.cardcode is null then '' else T0.cardcode END as 'cardcode', " & _
        "case when T0.cardname is null then '' else T0.cardname end as 'cardname',case when T2.SeriesName IS NULL then '' else T2.SeriesName end as 'series', " & _
        "case when T0.Address is null then '' else T0.Address END as 'address',T0.docdate,case when T0.taxdate is null then '' else T0.taxdate end as taxdate, " & _
        "T0.Filler,T0.GroupNum,case when T1.slpname is null then '' else T1.slpname end as 'slpname',case when T0.JrnlMemo is null then '' else T0.JrnlMemo end as 'jrnlmemo', " & _
        "CASE WHEN T0.Comments IS NULL THEN '' ELSE T0.Comments END AS 'comments', CASE WHEN T3.Name is null then '' else t3.name end as 'name'" & _
        "FROM OWTR T0 LEFT JOIN OSLP T1 ON T0.SlpCode=T1.SlpCode " & _
        "LEFT JOIN OCPR T3 ON T0.CntctCode=T3.CntctCode " & _
        "LEFT JOIN NNM1 T2 ON T0.Series=T2.Series where docnum = @docnum", conexion)
        cmd.Parameters.AddWithValue("@docnum", Module1.NumDoc)

        Try

            Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                TBDocEntry.Text = CStr(row("docentry"))
                TBDocNum.Text = CStr(row("docnum"))
                TBCliente.Text = CStr(row("cardcode"))
                TBNomCli.Text = CStr(row("cardname"))
                TBPerCon.Text = CStr(row("name"))
                TBDestinatario.Text = CStr(row("address"))

                TBSerie.Text = CStr(row("series"))
                TBDocDate.Text = CStr(row("docdate"))
                TBFecDoc.Text = CStr(row("taxdate"))
                TBAlmOri.Text = CStr(row("filler"))

                If CStr(row("groupnum")) = -1 Then
                    TBListaPre.Text = "Último precio de compra"
                End If

                TBAgente.Text = CStr(row("slpname"))

                TBComentarios.Text = CStr(row("JrnlMemo"))


                TBComentarios2.Text = CStr(row("comments"))


                '--------------******************************************
                '--------------******************************************
                '--------------******************************************
                conexion2.Open()
                Dim cmd4 As SqlCommand = Nothing
                cmd4 = New SqlCommand("DetalleTransferencia", conexion2)
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


                DisenoGrid()

            Else
                MsgBox("No hay documento con este folio")
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
    End Sub

    Sub DisenoGrid()
        '-------Diseño de DATAGRID Totales
        With Me.DGDetalle
            '.DataSource = DtAgte
            .ReadOnly = True
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


                .Columns(0).HeaderText = "Número de Art."
                .Columns(0).Width = 150
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(1).HeaderText = "Descripción"
                .Columns(1).Width = 250
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(2).HeaderText = "Almacén destino"
                .Columns(2).Width = 80
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Cantidad"
                .Columns(3).Width = 80
                .Columns(3).DefaultCellStyle.Format = " ###,###"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).HeaderText = "Costo del artículo"
                .Columns(4).Width = 80
                .Columns(4).DefaultCellStyle.Format = "$ ###,##0.####"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Unidad de medida"
                .Columns(5).Width = 100
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(6).HeaderText = "Comisión"
                .Columns(6).Width = 80
                .Columns(6).DefaultCellStyle.Format = "##0.#0"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(7).HeaderText = "Lista de precios"
                .Columns(7).Width = 90
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(8).HeaderText = "Stock"
                .Columns(8).Width = 100
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(9).HeaderText = "Artículo de importación"
                .Columns(9).Width = 100
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Catch ex As Exception

            End Try


        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conexion.Open()
        Dim cmd As SqlCommand = New SqlCommand("SELECT T0.DOCENTRY,t0.docnum, case when T0.cardcode is null then '' else T0.cardcode END as 'cardcode', " & _
        "case when T0.cardname is null then '' else T0.cardname end as 'cardname',case when T2.SeriesName IS NULL then '' else T2.SeriesName end as 'series', " & _
        "case when T0.Address is null then '' else T0.Address END as 'address',T0.docdate,case when T0.taxdate is null then '' else T0.taxdate end as taxdate, " & _
        "T0.Filler,T0.GroupNum,case when T1.slpname is null then '' else T1.slpname end as 'slpname',case when T0.JrnlMemo is null then '' else T0.JrnlMemo end as 'jrnlmemo', " & _
        "CASE WHEN T0.Comments IS NULL THEN '' ELSE T0.Comments END AS 'comments', CASE WHEN T3.Name is null then '' else t3.name end as 'name'" & _
        "FROM OWTR T0 LEFT JOIN OSLP T1 ON T0.SlpCode=T1.SlpCode " & _
        "LEFT JOIN OCPR T3 ON T0.CntctCode=T3.CntctCode " & _
        "LEFT JOIN NNM1 T2 ON T0.Series=T2.Series where docnum = @docnum", conexion)
        cmd.Parameters.AddWithValue("@docnum", TBDocNum.Text)

        Try

            Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                TBDocEntry.Text = CStr(row("docentry"))
                'TBDocNum.Text = CStr(row("docnum"))
                TBCliente.Text = CStr(row("cardcode"))
                TBNomCli.Text = CStr(row("cardname"))
                TBPerCon.Text = CStr(row("name"))
                TBDestinatario.Text = CStr(row("address"))

                TBSerie.Text = CStr(row("series"))
                TBDocDate.Text = CStr(row("docdate"))
                TBFecDoc.Text = CStr(row("taxdate"))
                TBAlmOri.Text = CStr(row("filler"))

                If CStr(row("groupnum")) = -1 Then
                    TBListaPre.Text = "Último precio de compra"
                End If

                TBAgente.Text = CStr(row("slpname"))

                TBComentarios.Text = CStr(row("JrnlMemo"))


                TBComentarios2.Text = CStr(row("comments"))


                '--------------******************************************
                '--------------******************************************
                '--------------******************************************
                conexion2.Open()
                Dim cmd4 As SqlCommand = Nothing
                cmd4 = New SqlCommand("DetalleTransferencia", conexion2)
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


                DisenoGrid()

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
    End Sub
End Class