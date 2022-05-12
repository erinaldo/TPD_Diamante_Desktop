
Imports System.Data.SqlClient

Public Class FacturasArtCli

    Dim DvArticulo As New DataView
    Dim DvConsulta As New DataView

    Private Sub FacturasArtCli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ConsutaLista As String


        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

            mllenaComboAlmacen(SqlConnection)

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

    End Sub

    Private Sub mllenaComboAlmacen(ByVal conexion As SqlConnection)
        Try
            Dim da As New SqlDataAdapter("SELECT GroupCode , GroupName " +
                                         "FROM OCRG with (nolock) " +
                                         "WHERE GroupType = 'C' ORDER BY GroupName ", conexion)

            Dim ds As New DataSet
            da.Fill(ds)
            ds.Tables(0).Rows.Add(0, "TODOS")
            Me.cmbAlmacen.DataSource = ds.Tables(0)
            Me.cmbAlmacen.DisplayMember = "GroupName"
            Me.cmbAlmacen.ValueMember = "GroupCode"

            If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Or UsrTPM = "DDORANTES" Then

                Me.cmbAlmacen.SelectedValue = 0

            Else

                If AlmTPM = "01" Then
                    Me.cmbAlmacen.SelectedValue = "100"
                ElseIf AlmTPM = "03" Then
                    Me.cmbAlmacen.SelectedValue = "102"
                ElseIf AlmTPM = "07" Then
                    Me.cmbAlmacen.SelectedValue = "103"
                End If


            End If


        Catch ex As Exception

        End Try

    End Sub

    Sub BuscaArticulos()
        Try
            CmbArticulo.SelectedValue = "TODOS"
            If CmbGrupoArticulo.SelectedValue Is Nothing Or CmbGrupoArticulo.SelectedValue = 999 Then
                DvArticulo.RowFilter = String.Empty
                CmbArticulo.SelectedValue = "TODOS"

            Else
                DvArticulo.RowFilter = "ItmsGrpCod = " & Trim(Me.CmbGrupoArticulo.SelectedValue.ToString) & " OR ItmsGrpCod = 999"
                CmbArticulo.SelectedValue = "TODOS"
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CmbGrupoArticulo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbGrupoArticulo.SelectedIndexChanged
        BuscaArticulos()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CargarRegistros()
        DisenoGrid()
    End Sub

    Private Sub CargarRegistros()
        Dim cnn As SqlConnection = Nothing

        Dim cmd4 As SqlCommand = Nothing

        Try
            cnn = New SqlConnection(StrTpm)

            cmd4 = New SqlCommand("SPFacturasArt", cnn)

            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@FechaIni", DtpFechaIni.Value)
            cmd4.Parameters.AddWithValue("@FechaFin", DtpFechaFin.Value)

            If cmbAlmacen.SelectedValue = "100" Then
                cmd4.Parameters.AddWithValue("@Almacen", "01")
            ElseIf cmbAlmacen.SelectedValue = "102" Then
                cmd4.Parameters.AddWithValue("@Almacen", "03")
            ElseIf cmbAlmacen.SelectedValue = "103" Then
                cmd4.Parameters.AddWithValue("@Almacen", "07")
            Else
                cmd4.Parameters.AddWithValue("@Almacen", "0")
            End If

            cmd4.Parameters.AddWithValue("@Linea", CmbGrupoArticulo.SelectedValue)
            cmd4.Parameters.AddWithValue("@Articulo", CmbArticulo.SelectedValue)

            cnn.Open()

            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 2000

            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()

            ''--------------------------------------------
            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "VentasCli"

            DvConsulta.Table = DsVtas.Tables("VentasCli")

            DGFacturas.DataSource = DvConsulta

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

    End Sub

    Private Sub DisenoGrid()
        With Me.DGFacturas

            .ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            '.RowHeadersVisible = False
            .RowHeadersWidth = 25
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .MultiSelect = True
            .AllowUserToAddRows = False


            .Columns(0).HeaderText = "Factura"
            .Columns(0).Width = 60
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(1).HeaderText = "Fecha"
            .Columns(1).Width = 80
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(2).HeaderText = "Cliente"
            .Columns(2).Width = 60
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(3).HeaderText = "Nombre"
            .Columns(3).Width = 220
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(4).HeaderText = "Artículo"
            .Columns(4).Width = 90
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(5).HeaderText = "Descripción"
            .Columns(5).Width = 250
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(6).HeaderText = "Clave Linea"
            .Columns(6).Width = 50
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(6).Visible = False

            .Columns(7).HeaderText = "Línea"
            .Columns(7).Width = 120
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(8).HeaderText = "Cantidad"
            .Columns(8).Width = 70
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(8).DefaultCellStyle.Format = "###,###,###"

            .Columns(9).HeaderText = "Clave Alm"
            .Columns(9).Width = 50
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(9).Visible = False

            .Columns(10).HeaderText = "Almacen"
            .Columns(10).Width = 70
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        End With
    End Sub


    Private Sub BtnExcel_Click(sender As Object, e As EventArgs) Handles BtnExcel.Click
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)


        'COMBINAMOS CELDAS
        oSheet.Range("A1:C1").Merge(True)
        oSheet.Range("A2:C2").Merge(True)



        'DAR COLOR DE FONDO A CELDAS
        oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
        oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15


        oSheet.Range("A4").INTERIOR.COLORINDEX = 15
        oSheet.Range("B4").INTERIOR.COLORINDEX = 15
        oSheet.Range("C4").INTERIOR.COLORINDEX = 15
        oSheet.Range("D4").INTERIOR.COLORINDEX = 15
        oSheet.Range("E4").INTERIOR.COLORINDEX = 15
        oSheet.Range("F4").INTERIOR.COLORINDEX = 15
        oSheet.Range("G4").INTERIOR.COLORINDEX = 15
        oSheet.Range("H4").INTERIOR.COLORINDEX = 15
        oSheet.Range("I4").INTERIOR.COLORINDEX = 15
        oSheet.Range("J4").INTERIOR.COLORINDEX = 15
        oSheet.Range("K4").INTERIOR.COLORINDEX = 15


        'Declaramos el nombre de las columnas
        oSheet.range("A4").value = "Factura"
        oSheet.range("B4").value = "Fecha"
        oSheet.range("C4").value = "Cliente"
        oSheet.range("D4").value = "Nombre"
        oSheet.range("E4").value = "Artículo"
        oSheet.range("F4").value = "Descripción"
        oSheet.range("G4").value = "Cod. Línea"
        oSheet.range("H4").value = "Línea"
        oSheet.range("I4").value = "Cantidad"
        oSheet.range("J4").value = "Cod. Almacén"
        oSheet.range("K4").value = "Almacén"



        'DISEÑO DE EXCEL

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A4:K4").font.bold = True


        oExcel.worksheets("Hoja1").Columns("A").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("A").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("B").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("B").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("C").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("C").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("D").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("D").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("E").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("E").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("F").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("F").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("G").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("G").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("H").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("H").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("I").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("I").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("J").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("J").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("K").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("K").Font.Size = 8


        'Cambia el alto de celda 
        oSheet.range("A:K").RowHeight = 13

        'oSheet.range("A:V").HorizontalAlignment = xlCenter

        'TAMAÑO DE COLUMNAS
        oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 6
        oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 8
        oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 6
        oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 14
        oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 16
        oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 5
        oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 5
        oExcel.worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 5
        oExcel.worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 10


        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = Me.DGFacturas.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel1 As String = Me.DGFacturas.Item(0, fila_dt).Value
            Dim cel2 As String = Me.DGFacturas.Item(1, fila_dt).Value
            Dim cel3 As String = IIf(IsDBNull(Me.DGFacturas.Item(2, fila_dt).Value), 0, Me.DGFacturas.Item(2, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DGFacturas.Item(3, fila_dt).Value), 0, Me.DGFacturas.Item(3, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DGFacturas.Item(4, fila_dt).Value), 0, Me.DGFacturas.Item(4, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DGFacturas.Item(5, fila_dt).Value), 0, Me.DGFacturas.Item(5, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DGFacturas.Item(6, fila_dt).Value), 0, Me.DGFacturas.Item(6, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DGFacturas.Item(7, fila_dt).Value), 0, Me.DGFacturas.Item(7, fila_dt).Value)

            Dim cel9 As String = IIf(IsDBNull(Me.DGFacturas.Item(8, fila_dt).Value), 0, Me.DGFacturas.Item(8, fila_dt).Value)
            Dim cel10 As String = IIf(IsDBNull(Me.DGFacturas.Item(9, fila_dt).Value), 0, Me.DGFacturas.Item(9, fila_dt).Value)

            'Dim cel11 As String = IIf(IsDBNull(Me.DGFacturas.Item(10, fila_dt).Value), 0, Me.DGFacturas.Item(10, fila_dt).Value)
            'Dim cel12 As String = IIf(IsDBNull(Me.DGFacturas.Item(11, fila_dt).Value), 0, Me.DGFacturas.Item(11, fila_dt).Value)
            'Dim cel13 As String = IIf(IsDBNull(Me.DGFacturas.Item(12, fila_dt).Value), 0, Me.DGFacturas.Item(12, fila_dt).Value)
            'Dim cel14 As String = IIf(IsDBNull(Me.DGFacturas.Item(13, fila_dt).Value), 0, Me.DGFacturas.Item(13, fila_dt).Value)
            'Dim cel15 As String = IIf(IsDBNull(Me.DGFacturas.Item(14, fila_dt).Value), 0, Me.DGFacturas.Item(14, fila_dt).Value)
            'Dim cel16 As String = IIf(IsDBNull(Me.DGFacturas.Item(15, fila_dt).Value), 0, Me.DGFacturas.Item(15, fila_dt).Value)
            'Dim cel17 As String = IIf(IsDBNull(Me.DGFacturas.Item(16, fila_dt).Value), 0, Me.DGFacturas.Item(16, fila_dt).Value)
            'Dim cel18 As String = IIf(IsDBNull(Me.DGFacturas.Item(17, fila_dt).Value), 0, Me.DGFacturas.Item(17, fila_dt).Value)
            'Dim cel19 As String = IIf(IsDBNull(Me.DGFacturas.Item(18, fila_dt).Value), 0, Me.DGFacturas.Item(18, fila_dt).Value)
            'Dim cel20 As String = IIf(IsDBNull(Me.DGFacturas.Item(19, fila_dt).Value), 0, Me.DGFacturas.Item(19, fila_dt).Value)


            fila_dt_excel = fila_dt + 5 'Renglón en donde se empieza a registrar el reporte

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
            oSheet.range("D" & fila_dt_excel).value = cel4
            oSheet.range("E" & fila_dt_excel).value = cel5
            oSheet.range("F" & fila_dt_excel).value = cel6
            oSheet.range("G" & fila_dt_excel).value = cel7
            oSheet.range("H" & fila_dt_excel).value = cel8
            oSheet.range("I" & fila_dt_excel).value = cel9
            oSheet.range("J" & fila_dt_excel).value = cel10
            'oSheet.range("K" & fila_dt_excel).value = cel11
            'oSheet.range("L" & fila_dt_excel).value = cel12
            'oSheet.range("M" & fila_dt_excel).value = FormatNumber(cel13, 2)
            'oSheet.range("N" & fila_dt_excel).value = cel14
            'oSheet.range("O" & fila_dt_excel).value = cel15
            'oSheet.range("P" & fila_dt_excel).value = cel16
            'oSheet.range("Q" & fila_dt_excel).value = cel17
            'oSheet.range("R" & fila_dt_excel).value = cel18
            'oSheet.range("S" & fila_dt_excel).value = FormatNumber(cel19, 2)
            'oSheet.range("T" & fila_dt_excel).value = FormatNumber(cel20, 2)

            'oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "$ ###,###,###.##"
            'oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "$ ###,###,###.##"
            'oExcel.Worksheets("Hoja1").Columns("L").NumberFormat = "$ ###,###,###.##"
            'oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "$ ###,###,###.##"

        Next


        'oExcel.Worksheets("Hoja1").Cells.Range("H5:H" + (total_reg + 4).ToString).Interior.ColorIndex = 15
        'oExcel.Worksheets("Hoja1").Cells.Range("J5:J" + (total_reg + 4).ToString).Interior.ColorIndex = 44
        'oExcel.Worksheets("Hoja1").Cells.Range("L5:L" + (total_reg + 4).ToString).Interior.ColorIndex = 6
        'oExcel.Worksheets("Hoja1").Cells.Range("Q5:Q" + (total_reg + 4).ToString).Interior.ColorIndex = 37
        'oExcel.Worksheets("Hoja1").Cells.Range("T5:T" + (total_reg + 4).ToString).Interior.ColorIndex = 22
        'oExcel.Worksheets("Hoja1").Cells.Range("R5:R" + (total_reg + 4).ToString).Interior.ColorIndex = 35
        'oExcel.Worksheets("Hoja1").Cells.Range("J5:J" + (total_reg + 4).ToString).Interior.ColorIndex = 44

        'Formato de texto para la primera columna CLAVE ART
        oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "@"

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        'oSheet.columns("A:O").entirecolumn.autofit()
        oSheet.range("A1").value = "Reporte de Facturas-Artículos"
        oSheet.range("A2").value = "Ventas del " & DtpFechaIni.Value & " al " & DtpFechaIni.Value


        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing

    End Sub

    Private Sub DGFacturas_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGFacturas.RowPrePaint
        DGFacturas.Rows(e.RowIndex).Cells("DocNum").Style.BackColor = Color.LightYellow
        DGFacturas.Rows(e.RowIndex).Cells("ItemCode").Style.BackColor = Color.LightGreen
        DGFacturas.Rows(e.RowIndex).Cells("Quantity").Style.BackColor = Color.LightBlue
    End Sub
End Class