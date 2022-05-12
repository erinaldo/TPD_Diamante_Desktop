Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient


Public Class InventarioM

    Public StrProd As String = conexion_universal.CadenaSBO_Diamante
    Public StrTpm As String = conexion_universal.CadenaSQL
    Public StrCon As String = conexion_universal.CadenaSQLSAP

    Dim DvInventario As New DataView
    Dim DvArticulos As New DataView
    Dim DvLineas As New DataView
    Dim DvClientes As New DataView

    Private Sub Inventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DisenoGrid()

        Dim ConsutaLista As String

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)



            Dim DSetTablas As New DataSet





            'If UsrTPM = "MANAGER" Or UsrTPM = "AINVEN" Or UsrTPM = "PCOMPRAS" Or UsrTPM = "COMPRAS1" Then
            ConsutaLista = "select WhsCode, WhsName from OWHS where WhsCode='01' OR WhsCode = '03' OR WhsCode = '07' "

                Dim daAlmacen As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)
                'Dim DSetTablas As New DataSet
                daAlmacen.Fill(DSetTablas, "Almacen")

                Dim fila As Data.DataRow

                'Asignamos a fila la nueva Row(Fila)del Dataset
                fila = DSetTablas.Tables("Almacen").NewRow

                'Agregamos los valores a los campos de la tabla
                fila("whsname") = "TODOS"
                fila("whscode") = 99

                'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                DSetTablas.Tables("Almacen").Rows.Add(fila)


            'Else
            '    ConsutaLista = "select WhsCode, WhsName from OWHS where WhsCode='01' OR WhsCode = '03'"


            '    Dim daAlmacen As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)
            '    'Dim DSetTablas As New DataSet
            '    daAlmacen.Fill(DSetTablas, "Almacen")

            'Dim fila As Data.DataRow

            ''Asignamos a fila la nueva Row(Fila)del Dataset
            'fila = DSetTablas.Tables("Almacen").NewRow

            'Agregamos los valores a los campos de la tabla
            'fila("whsname") = "TODOS"
            'fila("whscode") = 99

            ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            'DSetTablas.Tables("Almacen").Rows.Add(fila)


            'End If


            Me.CBAlmacen.DataSource = DSetTablas.Tables("Almacen")
            Me.CBAlmacen.DisplayMember = "whsname"
            Me.CBAlmacen.ValueMember = "whscode"
            Me.CBAlmacen.SelectedValue = 99


            '---------------------------------------------------------

            ConsutaLista = "select ItmsGrpCod, ItmsGrpNam from OITB "

            Dim daLinea As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


            daLinea.Fill(DSetTablas, "Lineas")

            Dim filaLinea As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaLinea = DSetTablas.Tables("Lineas").NewRow

            'Agregamos los valores a los campos de la tabla
            filaLinea("ItmsGrpNam") = "TODAS"
            filaLinea("ItmsGrpCod") = 999
            'filaLinea("GroupCode") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Lineas").Rows.Add(filaLinea)

            DvLineas.Table = DSetTablas.Tables("Lineas")

            Me.CBLinea.DataSource = DvLineas
            Me.CBLinea.DisplayMember = "ItmsGrpNam"
            Me.CBLinea.ValueMember = "ItmsGrpCod"
            Me.CBLinea.SelectedValue = 999

            ' -----------------------------------------------------
            Try


                ConsutaLista = "select itemcode, itemcode + '  -  ' + itemname as itemname, ITMSGRPCOD from OITM "

                Dim daarticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


                daarticulo.Fill(DSetTablas, "Articulos")

                Dim filaArticulo As Data.DataRow

                'Asignamos a fila la nueva Row(Fila)del Dataset
                filaArticulo = DSetTablas.Tables("Articulos").NewRow

                'Agregamos los valores a los campos de la tabla
                filaArticulo("ItemName") = "TODOS"
                filaArticulo("ItemCode") = 999
                filaArticulo("ItmsGrpCod") = 999

                'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                DSetTablas.Tables("Articulos").Rows.Add(filaArticulo)

                DvArticulos.Table = DSetTablas.Tables("Articulos")

                Me.CBArticulo.DataSource = DvArticulos
                Me.CBArticulo.DisplayMember = "ItemName"
                Me.CBArticulo.ValueMember = "ItemCode"
                Me.CBArticulo.SelectedValue = 999

                ' -----------------------------------------------------

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DGInventario.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES
        CargarRegistros()
        DisenoGrid()

        BExcel.Visible = True
        Dif()

        If CBAlmacen.Text Is Nothing Then
            MsgBox("Seleccione almacen para continuar", _
       MsgBoxStyle.Exclamation, _
       "Error almacen")
        End If

        If CBLinea.Text Is Nothing Then
            MsgBox("Seleccione línea para continuar", _
       MsgBoxStyle.Exclamation, _
       "Error linea")
        End If

        If CBArticulo.Text Is Nothing Then
            MsgBox("Seleccione Articulo para continuar", _
       MsgBoxStyle.Exclamation, _
       "Error articulo")
        End If

    End Sub

    Sub CargarRegistros()

        Try

            DGInventario.Columns.Clear()
            Dim cnn As SqlConnection = Nothing

            Dim cmd4 As SqlCommand = Nothing

            If CBAlmacen.Text = "TODOS" And CBLinea.Text = "TODAS" And CBArticulo.Text = "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("SPInventarioM", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@tipoconsulta", 1)
                    cmd4.Parameters.AddWithValue("@linea", String.Empty)
                    cmd4.Parameters.AddWithValue("@almacen", String.Empty)
                    cmd4.Parameters.AddWithValue("@itemcode", String.Empty)

                    cnn.Open()

                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()

                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmd4
                    da.SelectCommand.Connection = cnn


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Inventario"

                    DvInventario.Table = DsVtas.Tables("Inventario")

                    DGInventario.DataSource = DvInventario

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            ElseIf CBAlmacen.Text = "TODOS" And CBLinea.Text <> "TODAS" And CBArticulo.Text = "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("SPInventarioM", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@tipoconsulta", 2)
                    cmd4.Parameters.AddWithValue("@linea", CBLinea.Text)
                    cmd4.Parameters.AddWithValue("@almacen", String.Empty)
                    cmd4.Parameters.AddWithValue("@itemcode", String.Empty)

                    cnn.Open()

                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()

                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmd4
                    da.SelectCommand.Connection = cnn


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Inventario"

                    DvInventario.Table = DsVtas.Tables("Inventario")

                    DGInventario.DataSource = DvInventario

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            ElseIf CBAlmacen.Text = "TODOS" And CBArticulo.Text <> "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("SPInventarioM", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@tipoconsulta", 3)
                    cmd4.Parameters.AddWithValue("@almacen", CBAlmacen.SelectedValue)
                    cmd4.Parameters.AddWithValue("@linea", String.Empty)
                    cmd4.Parameters.AddWithValue("@itemcode", CBArticulo.SelectedValue)

                    cnn.Open()

                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()

                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmd4
                    da.SelectCommand.Connection = cnn


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Inventario"

                    DvInventario.Table = DsVtas.Tables("Inventario")

                    DGInventario.DataSource = DvInventario

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If

                End Try

            ElseIf CBAlmacen.Text <> "TODOS" And CBArticulo.Text <> "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("SPInventarioM", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@tipoconsulta", 4)
                    cmd4.Parameters.AddWithValue("@linea", String.Empty)
                    cmd4.Parameters.AddWithValue("@almacen", CBAlmacen.SelectedValue)
                    cmd4.Parameters.AddWithValue("@ITEMCODE", CBArticulo.SelectedValue)
                    cnn.Open()

                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()

                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmd4
                    da.SelectCommand.Connection = cnn


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Inventario"

                    DvInventario.Table = DsVtas.Tables("Inventario")

                    DGInventario.DataSource = DvInventario

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            ElseIf CBAlmacen.Text <> "TODOS" And CBLinea.Text <> "TODAS" And CBArticulo.Text = "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("SPInventarioM", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@tipoconsulta", 5)
                    cmd4.Parameters.AddWithValue("@linea", CBLinea.Text)
                    cmd4.Parameters.AddWithValue("@almacen", CBAlmacen.SelectedValue)
                    cmd4.Parameters.AddWithValue("@ITEMCODE", String.Empty)
                    cnn.Open()

                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()

                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmd4
                    da.SelectCommand.Connection = cnn


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Inventario"

                    DvInventario.Table = DsVtas.Tables("Inventario")

                    DGInventario.DataSource = DvInventario

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try


            ElseIf CBAlmacen.Text <> "TODOS" And CBLinea.Text = "TODAS" And CBArticulo.Text = "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("SPInventarioM", cnn)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.AddWithValue("@tipoconsulta", 6)
                    cmd4.Parameters.AddWithValue("@linea", String.Empty)
                    cmd4.Parameters.AddWithValue("@almacen", CBAlmacen.SelectedValue)
                    cmd4.Parameters.AddWithValue("@ITEMCODE", String.Empty)
                    cnn.Open()

                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()

                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmd4
                    da.SelectCommand.Connection = cnn


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Inventario"

                    DvInventario.Table = DsVtas.Tables("Inventario")

                    DGInventario.DataSource = DvInventario

                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            End If

        Catch ex As Exception

        End Try


    End Sub

    Sub Dif()

        Try

            Dim numfilas As Integer

            numfilas = DGInventario.RowCount 'cuenta las filas del DataGrid

            'recorre las filas del DataGrid
            For i = 0 To (numfilas - 1)

                If DGInventario.Item(7, i).Value < 0 Then
                    DGInventario.Rows(i).Cells(7).Style.ForeColor = Color.Red
                Else
                    DGInventario.Rows(i).Cells(7).Style.ForeColor = Color.Black
                End If


            Next
        Catch ex As Exception

        End Try

    End Sub


    Private Sub DisenoGrid()
        '-------Diseño de DATAGRID Agentes
        With Me.DGInventario
            Try


                '.DataSource = DtAgte
                '.ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DGInventario.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 25
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                'Color de linea del grid

                .Columns(0).ReadOnly = True
                .Columns(0).HeaderText = "Clave Art."
                .Columns(0).Width = 100
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(1).ReadOnly = True
                .Columns(1).HeaderText = "Descripcion"
                .Columns(1).Width = 200
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(2).ReadOnly = True
                .Columns(2).HeaderText = "Linea"
                .Columns(2).Width = 100

                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(3).ReadOnly = True
                .Columns(3).HeaderText = "Almacen"
                .Columns(3).Width = 95
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).ReadOnly = True
                .Columns(4).HeaderText = "Stock"
                .Columns(4).Width = 90
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Format = "#0"

                .Columns(5).ReadOnly = True
                .Columns(5).HeaderText = "Comprometido"
                .Columns(5).Width = 90
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Format = "###,###,###"

                .Columns(6).ReadOnly = True
                .Columns(6).HeaderText = "Solicitado"
                .Columns(6).Width = 90
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Format = "###,###,###"

                .Columns(7).ReadOnly = True
                .Columns(7).HeaderText = "Disponible"
                .Columns(7).Width = 90
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(7).DefaultCellStyle.Format = "###,###,###"

            Catch ex As Exception

            End Try

        End With
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BExcel.Click

        Try

            Dim oExcel As Object
            Dim oBook As Object
            Dim oSheet As Object

            Dim Rangos As String = ""
            Dim Rangos2 As String = ""

            'MsgBox("El reporte se creara a continuación")

            'Abrimos un nuevo libro
            oExcel = CreateObject("Excel.Application")
            oBook = oExcel.workbooks.add
            oSheet = oBook.worksheets(1)


            'Declaramos el nombre de las columnas
            oSheet.range("A3").value = "Clave Artículo"
            oSheet.range("B3").value = "Descripción"
            oSheet.range("C3").value = "Línea"
            oSheet.range("D3").value = "Almacén"
            oSheet.range("E3").value = "Stock"
            oSheet.range("F3").value = "Comprometido"
            oSheet.range("G3").value = "Solicitado"
            oSheet.range("H3").value = "Disponible"


            'para poner la primera fila de los titulos en negrita
            oSheet.range("A3:H3").font.bold = True
            Dim fila_dt As Integer = 0
            Dim fila_dt_excel As Integer = 0
            Dim tanto_porcentaje As String = ""
            Dim marikona As Integer = 0

            Dim total_reg As Integer = 0

            total_reg = DGInventario.RowCount
            For fila_dt = 0 To total_reg - 1

                'para leer una celda en concreto
                'el numero es la columna

                Dim cel0 As String = IIf(IsDBNull(Me.DGInventario.Item(0, fila_dt).Value), 0, Me.DGInventario.Item(0, fila_dt).Value)
                Dim cel1 As String = IIf(IsDBNull(Me.DGInventario.Item(1, fila_dt).Value), 0, Me.DGInventario.Item(1, fila_dt).Value)
                Dim cel2 As String = IIf(IsDBNull(Me.DGInventario.Item(2, fila_dt).Value), 0, Me.DGInventario.Item(2, fila_dt).Value)
                Dim cel3 As String = IIf(IsDBNull(Me.DGInventario.Item(3, fila_dt).Value), 0, Me.DGInventario.Item(3, fila_dt).Value)
                Dim cel4 As String = IIf(IsDBNull(Me.DGInventario.Item(4, fila_dt).Value), 0, Me.DGInventario.Item(4, fila_dt).Value)
                Dim cel5 As String = IIf(IsDBNull(Me.DGInventario.Item(5, fila_dt).Value), 0, Me.DGInventario.Item(5, fila_dt).Value)
                Dim cel6 As String = IIf(IsDBNull(Me.DGInventario.Item(6, fila_dt).Value), 0, Me.DGInventario.Item(6, fila_dt).Value)
                Dim cel7 As String = IIf(IsDBNull(Me.DGInventario.Item(7, fila_dt).Value), 0, Me.DGInventario.Item(7, fila_dt).Value)


                fila_dt_excel = fila_dt + 4

                'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
                oSheet.range("A" & fila_dt_excel).value = cel0
                oSheet.range("B" & fila_dt_excel).value = cel1
                oSheet.range("C" & fila_dt_excel).value = cel2
                oSheet.range("D" & fila_dt_excel).value = cel3
                oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel4, 2)
                oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel5, 2)
                oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel6, 2)
                oSheet.range("H" & fila_dt_excel).value = FormatNumber(cel7, 2)
 

            Next

            ' para que el tamano de la columna tenga como minimo el maximo de sus textos
            oSheet.columns("A:H").entirecolumn.autofit()

            oSheet.range("C1").value = Rangos
            oSheet.range("C2").value = Rangos2
            'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

            oExcel.visible = True
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
            GC.Collect()
            oSheet = Nothing
            oBook = Nothing
            oExcel = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    'Private Sub importarExcel(ByVal tabla As DataGridView)
    '    Dim myFileDialog As New OpenFileDialog()
    '    Dim xSheet As String = ""

    '    With myFileDialog
    '        .Filter = "Excel Files |*.xlsx"
    '        .Title = "Open File"
    '        .ShowDialog()
    '    End With
    '    If myFileDialog.FileName.ToString <> "" Then
    '        Dim ExcelFile As String = myFileDialog.FileName.ToString

    '        Dim ds As New DataSet
    '        Dim da As OleDbDataAdapter
    '        Dim dt As DataTable
    '        Dim conn As OleDbConnection

    '        xSheet = InputBox("Digite el nombre de la Hoja que desea importar", "Complete")
    '        conn = New OleDbConnection( _
    '                          "Provider=Microsoft.ACE.OLEDB.12.0;" & _
    '                          "data source=" & ExcelFile & "; " & _
    '                         "Extended Properties='Excel 12.0 Xml;HDR=Yes'")

    '        Try
    '            da = New OleDbDataAdapter("SELECT * FROM  [" & xSheet & "$]", conn)

    '            conn.Open()
    '            da.Fill(ds, "MyData")
    '            dt = ds.Tables("MyData")
    '            tabla.DataSource = ds
    '            tabla.DataMember = "MyData"
    '        Catch ex As Exception
    '            MsgBox("Inserte un nombre valido de la Hoja que desea importar", MsgBoxStyle.Information, "Informacion")
    '        Finally
    '            conn.Close()
    '        End Try
    '    End If
    '    MsgBox("Se ha cargado la importacion correctamente", MsgBoxStyle.Information, "Importado con exito")
    'End Sub

    Private Sub DGInventario_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGInventario.ColumnHeaderMouseClick
        Dif()
    End Sub

    Private Sub CBLinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBLinea.SelectedIndexChanged
        Try

            If CBLinea.SelectedItem Is Nothing Or CBLinea.Text = "TODAS" Then
                'CBProvTODOS()
                DvArticulos.RowFilter = String.Empty
                Me.CBArticulo.SelectedValue = 999
                'Me.CBArticulo.DataSource = DvProveedoresT

            Else
                'DvArticulos.RowFilter = String.Empty
                DvArticulos.RowFilter = "ItmsGrpCod = " & Trim(Me.CBLinea.SelectedValue.ToString) & " OR itmsgrpcod = 999"
                Me.CBArticulo.SelectedValue = 999

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CBArticulo_KeyUp(sender As Object, e As KeyEventArgs) Handles CBArticulo.KeyUp

    End Sub

    'Private Sub CBArticulo_TextChanged(sender As Object, e As EventArgs) Handles CBArticulo.TextChanged
    '    Try

    '        DvLineas.RowFilter = "itemcode like '%" & Me.CBLinea.SelectedValue & "%' AND " & _
    '           "itemname like '%" & Me.CBArticulo.Text & "%' " '& _
    '        '"itmsgrpcod like '%" & Me.CBArticulo.Text & "%' "

    '    Catch exc As Exception

    '        MessageBox.Show("CARACTER NO VALIDO," & Chr(13) & "BORRE EL CARACTER E INTENTE NUEVAMENTE..." & Chr(13) & exc.Message.ToString, "Alerta", _
    '      MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

    '    End Try
    'End Sub
End Class
