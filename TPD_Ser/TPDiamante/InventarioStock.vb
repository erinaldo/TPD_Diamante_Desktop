
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class InventarioStock

    Public StrProd As String = conexion_universal.CadenaSBO_Diamante
    Public StrTpm As String = conexion_universal.CadenaSQL
    Public StrCon As String = conexion_universal.CadenaSQLSAP

    Dim DvInventario As New DataView
    Dim DvArticulos As New DataView
    Dim DvLineas As New DataView
    Dim DvClientes As New DataView

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DGInventario.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES
        CargarRegistros()
        DisenoGrid()

        BExcel.Visible = True
        'Dif()

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

            If DGInventario.Columns.Count > 0 Then
                DGInventario.DataSource = Nothing
                DGInventario.Columns.RemoveAt(0)
            End If

            Dim cnn As SqlConnection = Nothing

            Dim cmd4 As SqlCommand = Nothing

            If CBAlmacen.Text = "TODOS" And CBLinea.Text = "TODAS" And CBArticulo.Text = "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("AuditoriaStock", cnn)
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
                    'MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            ElseIf CBAlmacen.Text = "TODOS" And CBLinea.Text <> "TODAS" And CBArticulo.Text = "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("AuditoriaStock", cnn)
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
                    'MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            ElseIf CBAlmacen.Text = "TODOS" And CBArticulo.Text <> "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("AuditoriaStock", cnn)
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
                    'MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If

                End Try

            ElseIf CBAlmacen.Text <> "TODOS" And CBArticulo.Text <> "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("AuditoriaStock", cnn)
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
                    ' MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            ElseIf CBAlmacen.Text <> "TODOS" And CBLinea.Text <> "TODAS" And CBArticulo.Text = "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("AuditoriaStock", cnn)
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
                    'MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try


            ElseIf CBAlmacen.Text <> "TODOS" And CBLinea.Text = "TODAS" And CBArticulo.Text = "TODOS" Then
                Try
                    cnn = New SqlConnection(StrTpm)

                    cmd4 = New SqlCommand("AuditoriaStock", cnn)
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
                    'MsgBox(ex.Message)
                Finally
                    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                        cnn.Close()
                    End If
                End Try

            End If

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
                .Columns(1).Width = 250
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(2).ReadOnly = True
                .Columns(2).HeaderText = "Linea"
                .Columns(2).Width = 130

                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(3).ReadOnly = True
                .Columns(3).HeaderText = "Almacen"
                .Columns(3).Width = 95
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).ReadOnly = True
                .Columns(4).HeaderText = "Cantidad Acumulada"
                .Columns(4).Width = 90
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Format = "#0"

                .Columns(5).ReadOnly = True
                .Columns(5).HeaderText = "Valor Acumulado"
                .Columns(5).Width = 90
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                ' creación de una columna de tipo botón
                Dim colBoton As DataGridViewButtonColumn = New DataGridViewButtonColumn()
                colBoton.Name = "detalle"
                colBoton.HeaderText = "Detalle"
                colBoton.Text = "Ver detalle"
                colBoton.UseColumnTextForButtonValue = True
                ' añadir la columna de tipo botón al DataGridView
                Me.DGInventario.Columns.Add(colBoton)

            Catch ex As Exception

            End Try

        End With
    End Sub


    Private Sub InventarioStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisenoGrid()

        Dim ConsutaLista As String

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)


            Dim DSetTablas As New DataSet
            ConsutaLista = "select WhsCode, WhsName from OWHS where WhsCode='01' or WhsCode='03' OR WhsCode = 07"
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
                'MsgBox(ex.Message)
            End Try

        End Using
    End Sub

    Private Sub DGInventario_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGInventario.CellContentDoubleClick

        Module1.ItemcodeInv = DGInventario.Item(0, DGInventario.CurrentCell.RowIndex).Value
        Module1.Almacen = DGInventario.Item(3, DGInventario.CurrentCell.RowIndex).Value

        'Dim frm As New InventarioStockDetalle()
        'frm.Show()

        'InventarioStockDetalle.MdiParent = Me
        InventarioStockDetalle.Show()
    End Sub

End Class