Imports System.IO
Imports ClosedXML.Excel

Public Class frmFNCPSinCancelados
    'Variables para los comando en sql
    Dim SQL As New Comandos_SQL()

    Sub Consultar()

        If (dtpFechaFin.Value <= "2020-07-31") Then
            dgvFNCPSinCanceladas.DataSource = SQL.EjecutarProcedimiento("TPD_F_NC_P_Cancelados", "@FechaInicio,@FechaFin", 2, dtpFechaInicio.Value.ToString("yyyy-MM-dd") + "," + dtpFechaFin.Value.ToString("yyyy-MM-dd"))
            If dgvFNCPSinCanceladas.Rows.Count <> 0 Then
                Dim column As DataGridViewColumn = dgvFNCPSinCanceladas.Columns("MONTO TOTAL")
                column.DefaultCellStyle.Format = "C2"
                column.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)

                Dim column2 As DataGridViewColumn = dgvFNCPSinCanceladas.Columns("DocSAP")
                'column2.DefaultCellStyle.Format = "C2"
                column2.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                column2.DefaultCellStyle.ForeColor = Color.DarkRed

                Dim column3 As DataGridViewColumn = dgvFNCPSinCanceladas.Columns("Documento")
                'column3.DefaultCellStyle.Format = "C2"
                column3.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                'column3.DefaultCellStyle.ForeColor = Color.DarkRed

            End If
        Else
            dgvFNCPSinCanceladas.DataSource = SQL.EjecutarProcedimiento("TPD_F_NC_P_Cancelados2", "@FechaInicio,@FechaFin", 2, dtpFechaInicio.Value.ToString("yyyy-MM-dd") + "," + dtpFechaFin.Value.ToString("yyyy-MM-dd"))
            If dgvFNCPSinCanceladas.Rows.Count <> 0 Then
                Dim column As DataGridViewColumn = dgvFNCPSinCanceladas.Columns("MONTO TOTAL")
                column.DefaultCellStyle.Format = "C2"
                column.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)

                Dim column2 As DataGridViewColumn = dgvFNCPSinCanceladas.Columns("DocSAP")
                'column2.DefaultCellStyle.Format = "C2"
                column2.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                column2.DefaultCellStyle.ForeColor = Color.DarkRed

                Dim column3 As DataGridViewColumn = dgvFNCPSinCanceladas.Columns("Documento")
                'column3.DefaultCellStyle.Format = "C2"
                column3.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                'column3.DefaultCellStyle.ForeColor = Color.DarkRed

            End If
        End If
        'FormatoDGV()
    End Sub

    Private Sub frmFNCPSinCancelados_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub bConsultar_Click(sender As Object, e As EventArgs) Handles bConsultar.Click
        Consultar()
    End Sub

    Sub FormatoDGV()
        Try
            With Me.dgvFNCPSinCanceladas
                .DefaultCellStyle.SelectionForeColor = Color.White
                .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
                .ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionForeColor = Color.White
                .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .Columns(0).HeaderText = "RFC"
                .Columns(0).Width = 70
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(0).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                .Columns(0).ReadOnly = True

                .Columns(1).HeaderText = "Cliente"
                .Columns(1).Width = 70
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(1).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                .Columns(1).ReadOnly = True

                .Columns(2).HeaderText = "Nombre"
                .Columns(2).Width = 70
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                .Columns(2).ReadOnly = True

                .Columns(3).HeaderText = "Monto Total"
                .Columns(3).Width = 70
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                .Columns(3).ReadOnly = True

                .Columns(4).HeaderText = "Doc. SAP"
                .Columns(4).Width = 70
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                .Columns(4).DefaultCellStyle.ForeColor = Color.Red
                .Columns(4).ReadOnly = True

                .Columns(5).HeaderText = "Fecha Doc."
                .Columns(5).Width = 70
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Format = String.Format("dd-MM-yyyy", Globalization.CultureInfo.CreateSpecificCulture("en-ES"))
                .Columns(5).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                .Columns(5).ReadOnly = True

                .Columns(6).HeaderText = "UUID"
                .Columns(6).Width = 70
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                .Columns(6).ReadOnly = True

                .Columns(7).HeaderText = "Timbrado"
                .Columns(7).Width = 70
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                .Columns(7).ReadOnly = True

                .Columns(8).HeaderText = "Tipo Doc."
                .Columns(8).Width = 70
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                .Columns(8).ReadOnly = True

            End With
        Catch ex As Exception
            MessageBox.Show("Error al dar formato en DataGridView :" + ex.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub Generar_Excel()
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)

        'Declaramos el nombre de las columnas
        oSheet.range("A1").value = "RFC"
        oSheet.range("B1").value = "Cliente"
        oSheet.range("C1").value = "Nombre"
        oSheet.range("D1").value = "Monto Total"
        oSheet.range("E1").value = "Doc. SAP"
        oSheet.range("F1").value = "Fecha Doc."
        oSheet.range("G1").value = "UUID"
        oSheet.range("H1").value = "Timbrado"
        oSheet.range("I1").value = "Tipo Doc."


        'para poner la primera fila de los titulos en negrita
        oSheet.range("A1:I1").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 1

        Dim total_reg As Integer = 0

        total_reg = dgvFNCPSinCanceladas.RowCount

        For fila_dt = 0 To total_reg

            If fila_dt = total_reg Then

            Else
                fila_dt_excel += 1
                'para leer una celda en concreto
                'el numero es la columna
                Dim cel1 As String = dgvFNCPSinCanceladas.Item(0, fila_dt).Value
                Dim cel2 As String = dgvFNCPSinCanceladas.Item(1, fila_dt).Value
                Dim cel3 As String = dgvFNCPSinCanceladas.Item(2, fila_dt).Value
                Dim cel4 As String = dgvFNCPSinCanceladas.Item(3, fila_dt).Value
                Dim cel5 As String = dgvFNCPSinCanceladas.Item(4, fila_dt).Value
                Dim cel6 As String = dgvFNCPSinCanceladas.Item(5, fila_dt).Value
                Dim cel7 As String = dgvFNCPSinCanceladas.Item(6, fila_dt).Value.ToString()
                Dim cel8 As String = dgvFNCPSinCanceladas.Item(7, fila_dt).Value
                ' Dim cel9 As String = dgvFNCPSinCanceladas.Item(8, fila_dt).Value


                'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
                oSheet.range("A" & fila_dt_excel).value = cel1
                oSheet.range("B" & fila_dt_excel).value = cel2
                oSheet.range("C" & fila_dt_excel).value = cel3
                oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4)
                oSheet.range("E" & fila_dt_excel).value = cel5
                oSheet.range("F" & fila_dt_excel).value = cel6
                oSheet.range("G" & fila_dt_excel).value = cel7
                oSheet.range("H" & fila_dt_excel).value = cel8
                ' oSheet.range("I" & fila_dt_excel).value = cel9

            End If

        Next

        oSheet.columns("A:I").entirecolumn.autofit()

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing

    End Sub

    Private Sub btExportar_Click(sender As Object, e As EventArgs) Handles btExportar.Click
        'Generar_Excel()
        Exportar()
    End Sub

    Sub Exportar()
        Try
            Dim dt As New DataTable()
            For Each columns As DataGridViewColumn In dgvFNCPSinCanceladas.Columns
                dt.Columns.Add(columns.HeaderText, columns.ValueType)
            Next
            For Each row As DataGridViewRow In dgvFNCPSinCanceladas.Rows
                dt.Rows.Add()

                For Each cell As DataGridViewCell In row.Cells
                    If Not Convert.IsDBNull(cell.Value) = 0 Then
                        dt.Rows(dt.Rows.Count - 1)(cell.ColumnIndex) = IsDBNull("")
                    Else
                        If cell.Value = Nothing Then
                            dt.Rows(dt.Rows.Count - 1)(cell.ColumnIndex) = IsDBNull("")
                        Else
                            dt.Rows(dt.Rows.Count - 1)(cell.ColumnIndex) = cell.Value.ToString()
                        End If
                    End If
                Next
            Next

            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Filter = "Excel|*.xlsx"
            saveFileDialog1.Title = "Save Excel File"
            saveFileDialog1.FileName = "Export_" & dgvFNCPSinCanceladas.Name.ToString() & ".xlsx"
            saveFileDialog1.ShowDialog()
            saveFileDialog1.InitialDirectory = "C:/"

            If saveFileDialog1.FileName <> "" Then
                Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
                fs.Close()
            End If

            Dim strFileName As String = saveFileDialog1.FileName
            Dim blnFileOpen As Boolean = False

            Using wb As New XLWorkbook
                wb.Worksheets.Add(dt, "Hoja 1")
                wb.SaveAs(strFileName)
            End Using

            Process.Start(strFileName)
        Catch ex As Exception
            MessageBox.Show("¡Error al exportar archivo: " + Environment.NewLine + ex.ToString() + "!", "¡Error en ExportarSinEstilo!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Sub Filtro()
        If String.IsNullOrWhiteSpace(tbBuscador.Text) Then
        Else
            'Por defecto, indico buscar en la primera columna
            Dim indiceColumna As Integer = 4
            Dim renglon As Integer = 0

            'Recorro filas del DataGridView
            For Each row As DataGridViewRow In dgvFNCPSinCanceladas.Rows
                'Si el contenido de la columna coinside con el valor del TextBox
                If CStr(row.Cells(indiceColumna).Value).ToLower = tbBuscador.Text.ToLower Then
                    'Selecciono fila y abandono bucle
                    renglon = row.Index
                    row.Selected = True
                    Exit For
                End If
            Next
            dgvFNCPSinCanceladas.Rows(renglon).Selected = True
            dgvFNCPSinCanceladas.CurrentCell = dgvFNCPSinCanceladas.Rows(renglon).Cells(indiceColumna)
        End If
    End Sub

    Private Sub tbBuscador_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbBuscador.KeyPress
        'e.KeyChar = Char.ToUpper(e.KeyChar)
        If AscW(e.KeyChar) = CInt(Keys.Enter) Then
            Filtro()
            tbBuscador.Text = ""
        End If

    End Sub
End Class