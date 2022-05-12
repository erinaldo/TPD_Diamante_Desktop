
Option Explicit On
'Option Strict On
Imports System.Data.SqlClient

Public Class NCConsulta

    Public conexion As New SqlConnection(conexion_universal.CadenaSQLSAP)
    Public conexion2 As New SqlConnection(conexion_universal.CadenaSQL)

    Public DvDetalle As New DataView


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conexion.Open()
        Dim cmd As SqlCommand = New SqlCommand("SELECT T0.DocEntry,T0.cardcode,T0.cardname,case when T3.name IS NULL then '' else t3.NAME end as 'cntctcode', " & _
        "CASE WHEN T0.numatcard IS NULL THEN '' ELSE T0.numatcard END AS 'numatcard',T0.cursource,T2.seriesname, " & _
        "CASE WHEN T0.docstatus='C' THEN 'Cerrado' WHEN T0.printed='N' AND T0.docstatus='O' THEN 'Abierto' WHEN T0.printed='Y' AND T0.docstatus='O' THEN 'Abrir: Imprimido' END as 'status', " & _
        "T0.docdate,T0.docduedate,T0.taxdate,T1.slpname,CASE WHEN t0.ownercode IS NULL THEN '' ELSE t0.ownercode END, " & _
        "t0.Comments, T0.DiscPrcnt, t0.doctype,T0.vatsum,T0.doctotal,t0.doctotal-t0.vatsum as 'antdesc', t0.paidtodate as 'impapli', T0.doctotalsy " & _
        "FROM ORIN T0 LEFT JOIN OSLP T1 ON T0.SlpCode=T1.SlpCode LEFT JOIN NNM1 T2 ON T0.Series=T2.Series " & _
        "LEFT JOIN OCPR T3 ON T0.CntctCode=T3.CntctCode where docnum = @docnum", conexion)
        cmd.Parameters.AddWithValue("@docnum", TBDocNum.Text)

        Try

            Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                TBDocEntry.Text = CStr(row("docentry"))
                TBCliente.Text = CStr(row("cardcode"))
                TBNomCli.Text = CStr(row("cardname"))
                TBPerCon.Text = CStr(row("cntctcode"))
                TBNumRef.Text = CStr(row("numatcard"))

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
                TBSaldoPen.Text = CStr(row("doctotalsy"))


                '--------------******************************************
                '--------------******************************************
                '--------------******************************************
                conexion2.Open()
                Dim cmd4 As SqlCommand = Nothing
                cmd4 = New SqlCommand("DetalleNC", conexion2)
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
                .Columns(0).Width = 100
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(1).HeaderText = "Descripción"
                .Columns(1).Width = 200
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(2).HeaderText = "Stock"
                .Columns(2).Width = 60
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Almacén"
                .Columns(3).Width = 50
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).HeaderText = "Cantidad"
                .Columns(4).Width = 50
                .Columns(4).DefaultCellStyle.Format = "#,##0"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Lista de Precios"
                .Columns(5).Width = 50
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(6).HeaderText = "Precio Unitario"
                .Columns(6).Width = 90
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Format = "$ #,##0.###0"


                .Columns(7).HeaderText = "% de descuento"
                .Columns(7).Width = 100
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Format = "#0.#0 %"

                .Columns(8).HeaderText = "Precio tras el descuento"
                .Columns(8).Width = 90
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Format = "$ ###,##0.#0"

                .Columns(9).HeaderText = "Moneda"
                .Columns(9).Width = 90
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(9).DefaultCellStyle.Format = "$ ###,##0.#0"

                .Columns(10).HeaderText = "Indicador de impuestos"
                .Columns(10).Width = 100
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(11).HeaderText = "Total (ML)"
                .Columns(11).Width = 100
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(11).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(12).HeaderText = "Comisión"
                .Columns(12).Width = 75
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(12).DefaultCellStyle.Format = "##0.#0"

                .Columns(13).HeaderText = "Precio bruto"
                .Columns(13).Width = 75
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(13).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(14).HeaderText = "Precio de coste ingreso bruto"
                .Columns(14).Width = 80
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(14).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(15).HeaderText = "Ingreso bruto (ML)"
                .Columns(15).Width = 75
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(15).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(16).HeaderText = "Total bruto (ML)"
                .Columns(16).Width = 80
                .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(16).DefaultCellStyle.Format = "$ ###,###,##0.#0"

            Catch ex As Exception

            End Try


        End With
    End Sub

End Class