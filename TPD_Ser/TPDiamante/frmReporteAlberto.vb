Imports System.IO
Imports ClosedXML.Excel

Public Class frmReporteAlberto

    Dim SQL As New Comandos_SQL()

    Private Sub frmReporteAlberto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReporteLF()
    End Sub

    Sub ReporteLF()
        SQL.conectarTPM()
        Dim agente As String = SQL.CampoEspecifico("select CodAgte From Usuarios where Id_Usuario = '" + UsrTPM + "'", "CodAgte")

        SQL.Cerrar()
        If UsrTPM = "MANAGER" Or UsrTPM = "COMERCIAL" Then
            dgvDatos.DataSource = SQL.EjecutarProcedimiento("rpt_LimFact_Alberto", "@FechaInicio,@FechaFin,@Agente,@Ventas", 4, dtfechainicio.Value.ToString("yyyy-MM-dd") + "," + dtfechafin.Value.ToString("yyyy-MM-dd") + ",99,'No ventas'")
        Else
            'Todos los agentes de marketing
            If agente = "" Then
                'Marketing
                dgvDatos.DataSource = SQL.EjecutarProcedimiento("rpt_LimFact_Alberto", "@FechaInicio,@FechaFin,@Agente,@Ventas", 4, dtfechainicio.Value.ToString("yyyy-MM-dd") + "," + dtfechafin.Value.ToString("yyyy-MM-dd") + ",666," + UsrTPM)
            Else
                dgvDatos.DataSource = SQL.EjecutarProcedimiento("rpt_LimFact_Alberto", "@FechaInicio,@FechaFin,@Agente,@Ventas", 4, dtfechainicio.Value.ToString("yyyy-MM-dd") + "," + dtfechafin.Value.ToString("yyyy-MM-dd") + "," + agente + ",'No ventas'")
            End If
        End If

        EstilodgvDatos()
        lbUltimaCons.Text = "Última consulta: " + DateTime.Now
    End Sub

    Sub EstilodgvDatos()
        With Me.dgvDatos
            Try
                .ReadOnly = True
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .RowHeadersVisible = True
                .RowHeadersWidth = 25

                .Columns("# Cliente").HeaderText = "# Cliente"
                .Columns("# Cliente").Width = 70
                .Columns("# Cliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns("Cliente").HeaderText = "Cliente"
                .Columns("Cliente").Width = 250
                .Columns("Cliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns("Limite").HeaderText = "Limite"
                .Columns("Limite").Width = 100
                .Columns("Limite").DefaultCellStyle.Format = "$ ###,###,###.00"
                .Columns("Limite").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns("Acumulado").HeaderText = "Acumulado"
                .Columns("Acumulado").Width = 100
                .Columns("Acumulado").DefaultCellStyle.Format = "$ ###,###,###.00"
                .Columns("Acumulado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns("Entregas").HeaderText = "Entregas"
                .Columns("Entregas").Width = 100
                .Columns("Entregas").DefaultCellStyle.Format = "$ ###,###,###.00"
                .Columns("Entregas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns("Disponible al dia").HeaderText = "Disponible al dia"
                .Columns("Disponible al dia").Width = 100
                .Columns("Disponible al dia").DefaultCellStyle.Format = "$ ###,###,###.00"
                .Columns("Disponible al dia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns("Agente").HeaderText = "Agente"
                .Columns("Agente").Width = 200
                .Columns("Agente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            Catch ex As Exception

            End Try
        End With
    End Sub

    Private Sub bGenerar_Click(sender As Object, e As EventArgs) Handles bGenerar.Click
        ReporteLF()
    End Sub

    Sub ExportNuevo()
        'Creating DataTable.
        Dim dt As New DataTable()

        'Adding the Columns.
        For Each column As DataGridViewColumn In dgvDatos.Columns
            dt.Columns.Add(column.HeaderText, column.ValueType)
        Next

        'Adding the Rows.
        For Each row As DataGridViewRow In dgvDatos.Rows
            dt.Rows.Add()
            For Each cell As DataGridViewCell In row.Cells
                dt.Rows(dt.Rows.Count - 1)(cell.ColumnIndex) = cell.Value.ToString()
            Next
        Next

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(dt, "Cred. Disp. por cliente al dia")

            'wb.Worksheet(1).Cells("A1:H1").Style.Fill.BackgroundColor = XLColor.blue
            wb.Worksheet(1).Cells("A1:H1").Style.Font.FontColor = XLColor.Black
            Dim index As Integer = 2

            For i As Integer = 0 To dt.Rows.Count

                Try
                    Dim row As DataRow = dt.Rows(i)

                    Dim cellC As String = String.Format("C{0}", index)
                    wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

                    Dim cellD As String = String.Format("D{0}", index)
                    wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "$ #,##0.00"

                    Dim cellE As String = String.Format("E{0}", index)
                    wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "$ #,##0.00"

                    Dim cellF As String = String.Format("F{0}", index)
                    wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "$ #,##0.00"

                Catch ex As Exception

                End Try

                index = index + 1
            Next

            wb.Worksheet(1).Columns().AdjustToContents()

            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Filter = "Excel|*.xlsx"
            saveFileDialog1.Title = "Save Excel File"
            saveFileDialog1.FileName = "Export_" + dgvDatos.Name.ToString() + ".xlsx"
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

    Private Sub bExportarExcel_Click(sender As Object, e As EventArgs) Handles bExportarExcel.Click
        ExportNuevo()
    End Sub
End Class