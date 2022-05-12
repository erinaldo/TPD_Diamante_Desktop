
Imports System.Data.SqlClient

Public Class InventarioStockDetalle

    Private Sub InventarioStockDetalle_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Label1.Text = "Detalle del artículo " & Module1.ItemcodeInv

        DGDetalleInv.DataSource = Nothing

        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing


        Try

            cnn = New SqlConnection(StrTpm)

            cnn.Open()

            cmd = New SqlCommand("AuditoriaStockMDetalle", cnn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@itemcode", Module1.ItemcodeInv)
            cmd.Parameters.AddWithValue("@almacen", Module1.Almacen)


            Label1.Text = "Detalle del artículo: " & Module1.ItemcodeInv & ",  Almacen: " & Module1.Almacen

            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd
            da.SelectCommand.Connection = cnn


            ''--------------------------------------------
            Dim DvAgentes As New DataView


            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "VtaAgtes"

            DvAgentes.Table = DsVtas.Tables("VtaAgtes")

            DGDetalleInv.DataSource = DvAgentes



            With DGDetalleInv

                '.DataSource = DtAgte
                .ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DGDetalleInv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = False
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                'Color de linea del grid

                .Columns(0).HeaderText = "Fecha de contabilización"
                .Columns(0).Width = 100
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Tipo de documento"
                .Columns(1).Width = 180
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(2).HeaderText = "Num. Documento"
                .Columns(2).Width = 80
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Almacén"
                .Columns(3).Width = 60
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).HeaderText = "Cantidad"
                .Columns(4).Width = 60
                .Columns(4).DefaultCellStyle.Format = "#,##0"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Costos"
                .Columns(5).Width = 90
                .Columns(5).DefaultCellStyle.Format = "$ #,###,##0.##"
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '.Columns(6).HeaderText = "Moneda"
                '.Columns(6).Width = 60
                '.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(6).HeaderText = "Valor transf."
                .Columns(6).Width = 120
                .Columns(6).DefaultCellStyle.Format = "$ ###,##0.##"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(7).HeaderText = "Cantidad acum."
                .Columns(7).Width = 120
                .Columns(7).DefaultCellStyle.Format = "###,##0"
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(8).HeaderText = "Valor acum."
                .Columns(8).Width = 120
                .Columns(8).DefaultCellStyle.Format = "$ ###,###,##0.## "
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(9).HeaderText = "Tipo de transaccion"
                .Columns(9).Width = 50
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                'Dim canacum As New DataGridViewTextBoxColumn

                'canacum.Name = "canacum"
                'canacum.HeaderText = "Cantidad acumulada"
                'canacum.Width = 60
                'canacum.DefaultCellStyle.Format = "#,##0"
                'canacum.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'DGDetalleInv.Columns.Add(canacum)


                'Dim valacum As New DataGridViewTextBoxColumn

                'valacum.Name = "valacum"
                'valacum.HeaderText = "Valor acumulado"
                'valacum.DefaultCellStyle.Format = "$ ###,##0.##"
                'valacum.Width = 120
                'valacum.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DGDetalleInv.Columns.Add(valacum)


                'Dim numfilas As Integer

                'numfilas = DGDetalleInv.RowCount - 1
                ''MsgBox(numfilas)

                'If numfilas = 1 Then
                '    'DGDetalleInv.Item(7, 0).Value = DGDetalleInv.Item(4, 0).Value
                '    'DGDetalleInv.Item(8, 0).Value = DGDetalleInv.Item(6, 0).Value

                'Else

                '    For i = 1 To numfilas - 1

                '        DGDetalleInv.Item(7, 0).Value = DGDetalleInv.Item(4, 0).Value
                '        DGDetalleInv.Item(8, 0).Value = DGDetalleInv.Item(6, 0).Value

                '        DGDetalleInv.Item(7, i).Value = DGDetalleInv.Item(4, i).Value + DGDetalleInv.Item(7, i - 1).Value

                '        DGDetalleInv.Item(8, i).Value = DGDetalleInv.Item(6, i).Value + DGDetalleInv.Item(8, i - 1).Value

                '    Next

                'End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try


        
    End Sub

    Private Sub DisenoGrid()
        With Me.DGDetalleInv
            
        End With
    End Sub


    Private Sub DGDetalleInv_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalleInv.CellContentDoubleClick
    End Sub



    Private Sub DGDetalleInv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalleInv.CellDoubleClick
        Dim tipotrans As Integer
        tipotrans = DGDetalleInv.Item(9, DGDetalleInv.CurrentCell.RowIndex).Value

        Module1.NumDoc = DGDetalleInv.Item(2, DGDetalleInv.CurrentCell.RowIndex).Value

        If tipotrans = 13 Then
            Dim frm As New FacturaAuditoria()
            frm.Show()
        End If

        If tipotrans = 14 Then
            Dim frm As New NCAuditoria()
            frm.Show()
        End If

        If tipotrans = 67 Then
            Dim frm As New TransfStock()
            frm.Show()
        End If

    End Sub

End Class