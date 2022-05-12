
'Imports System.IO 'esta libreria nos va a servir para poder activar el commandialog
'Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class Inventario

 Public StrProd As String = conexion_universal.CadenaSBO_Diamante
 Public StrTpm As String = conexion_universal.CadenaSQL
 Public StrCon As String = conexion_universal.CadenaSQLSAP

 Dim DvInventario As New DataView
 Dim DvAgentes As New DataView
 Dim DvLineas As New DataView
 Dim DvArticulo As New DataView
 Dim DvClientes As New DataView

 Dim DvInventarioRec As New DataView

 Dim conexion As SqlConnection = New SqlConnection(StrTpm)

 Dim valDiseno As Integer

 Dim valMetodo As Integer

 Private Sub Inventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load

  Me.WindowState = FormWindowState.Maximized

  'DisenoGrid()

  Dim ConsutaLista As String

  Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)


   Dim DSetTablas As New DataSet
   ConsutaLista = "select WhsCode, WhsName from OWHS where WhsCode='01' or WhsCode='03' or WhsCode='07' or WhsCode='02' or WhsCode = '08' "
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
   ConsutaLista = "SELECT ItemCode,ItemName,ItmsGrpCod FROM OITM ORDER BY ItemCode"
   Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

   daArticulo.Fill(DSetTablas, "Articulos")

   Dim filaArticulo As DataRow
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
   Me.CmbArticulo.SelectedIndex = 999
  End Using
 End Sub

 'Private Sub Button2_Click(sender As Object, e As EventArgs)
 '    DGInventario.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES
 '    CargarRegistros()
 '    DisenoGrid()
 '    BSave.Visible = False
 '    BExcel.Visible = True

 '    Dif()
 'End Sub

 Sub CargarRegistros()

  Try

   DGInventario.Columns.Clear()
   Dim cnn As SqlConnection = Nothing

   Dim cmd4 As SqlCommand = Nothing

   If CBAlmacen.Text = "TODOS" And CBLinea.Text = "TODAS" Then
    Try
     cnn = New SqlConnection(StrTpm)

     cmd4 = New SqlCommand("SPInventarioPesos2", cnn)
     cmd4.CommandType = CommandType.StoredProcedure
     cmd4.Parameters.AddWithValue("@tipoconsulta", 1)
     cmd4.Parameters.AddWithValue("@linea", String.Empty)
     cmd4.Parameters.AddWithValue("@almacen", String.Empty)

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

   ElseIf CBAlmacen.Text <> "TODOS" And CBLinea.Text <> "TODAS" Then
    Try
     cnn = New SqlConnection(StrTpm)

     cmd4 = New SqlCommand("SPInventarioPesos2", cnn)
     cmd4.CommandType = CommandType.StoredProcedure
     cmd4.Parameters.AddWithValue("@tipoconsulta", 2)
     cmd4.Parameters.AddWithValue("@linea", CBLinea.SelectedValue)
     cmd4.Parameters.AddWithValue("@almacen", CBAlmacen.SelectedValue)

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

   ElseIf CBAlmacen.Text <> "TODOS" And CBLinea.Text = "TODAS" Then
    Try
     cnn = New SqlConnection(StrTpm)

     cmd4 = New SqlCommand("SPInventarioPesos2", cnn)
     cmd4.CommandType = CommandType.StoredProcedure
     cmd4.Parameters.AddWithValue("@tipoconsulta", 3)
     cmd4.Parameters.AddWithValue("@linea", String.Empty)
     cmd4.Parameters.AddWithValue("@almacen", CBAlmacen.SelectedValue)

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

   ElseIf CBAlmacen.Text = "TODOS" And CBLinea.Text <> "TODAS" Then
    Try
     cnn = New SqlConnection(StrTpm)

     cmd4 = New SqlCommand("SPInventarioPesos2", cnn)
     cmd4.CommandType = CommandType.StoredProcedure
     cmd4.Parameters.AddWithValue("@tipoconsulta", 4)
     cmd4.Parameters.AddWithValue("@linea", CBLinea.SelectedValue)
     cmd4.Parameters.AddWithValue("@almacen", String.Empty)

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


   '5 StockFis '6 DIFERENCIA   '8 Lista09 '9 CostoTot  '10 Lista01 '11 ImporteTot

   For i = 0 To (numfilas - 1)

    If DGInventario.Item(6, i).Value Is DBNull.Value Or DGInventario.Item(9, i).Value Is DBNull.Value Then
     DGInventario.Item(6, i).Value = 0
     DGInventario.Item(9, i).Value = 0
     DGInventario.Item(11, i).Value = 0

    Else
     If DGInventario.Item(6, i).Value < 0 Then
      DGInventario.Rows(i).Cells(6).Style.ForeColor = Color.Red
     Else
      DGInventario.Rows(i).Cells(6).Style.ForeColor = Color.Black
     End If

     If DGInventario.Item(9, i).Value < 0 Then
      DGInventario.Rows(i).Cells(9).Style.ForeColor = Color.Red
     Else
      DGInventario.Rows(i).Cells(9).Style.ForeColor = Color.Black
     End If

     If DGInventario.Item(11, i).Value < 0 Then
      DGInventario.Rows(i).Cells(11).Style.ForeColor = Color.Red
     Else
      DGInventario.Rows(i).Cells(11).Style.ForeColor = Color.Black
     End If
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
    .AllowUserToAddRows = False

    DGInventario.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    'Propiedad para no mostrar el cuadro que se encuentra en la parte
    'Superior Izquierda del gridview
    .RowHeadersVisible = True
    .RowHeadersWidth = 25
    '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    'Color de linea del grid

    .Columns(0).ReadOnly = True
    .Columns(0).HeaderText = "ID Articulo"
    .Columns(0).Width = 100
    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(1).ReadOnly = True
    .Columns(1).HeaderText = "Descripcion"
    .Columns(1).Width = 150

    .Columns(2).ReadOnly = True
    .Columns(2).HeaderText = "Linea"
    .Columns(2).Width = 95
    .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(3).ReadOnly = True
    .Columns(3).HeaderText = "Almacen"
    .Columns(3).Width = 95
    .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(4).Name = "Stock Sist"
    .Columns(4).ReadOnly = True
    .Columns(4).HeaderText = "Stock SAP"
    .Columns(4).Width = 60
    .Columns(4).DefaultCellStyle.Format = "#0"
    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(5).Name = "Stock Fisico"
    .Columns(5).HeaderText = "Stock Fis."
    .Columns(5).Width = 60
    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(5).DefaultCellStyle.Format = "#0"

    .Columns(6).ReadOnly = True
    .Columns(6).HeaderText = "Diferencia"
    .Columns(6).Width = 60
    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(6).DefaultCellStyle.Format = "##0"

    .Columns(7).ReadOnly = True
    .Columns(7).HeaderText = "Peso"
    .Columns(7).Width = 60
    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(7).DefaultCellStyle.Format = "N4"

    .Columns(8).ReadOnly = True
    .Columns(8).HeaderText = "Precio Promedio"
    .Columns(8).Width = 85
    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(8).DefaultCellStyle.Format = "$##0.###0"

    .Columns(9).ReadOnly = True
    .Columns(9).HeaderText = "Costo Total"
    .Columns(9).Width = 85
    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(9).DefaultCellStyle.Format = "$#,##0.###0"

    .Columns(10).ReadOnly = True
    .Columns(10).HeaderText = "Lista01"
    .Columns(10).Width = 85
    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(10).DefaultCellStyle.Format = "$#,##0.###0"

    .Columns(11).ReadOnly = True
    .Columns(11).HeaderText = "Importe Total"
    .Columns(11).Width = 85
    .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    .Columns(11).DefaultCellStyle.Format = "$#,##0.###0"


    .Columns(12).ReadOnly = True
    .Columns(12).HeaderText = "Bloque"
    .Columns(12).Width = 60
    .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '.Columns(9).DefaultCellStyle.Format = "$ #,##0.#0"

    .Columns(13).ReadOnly = True
    .Columns(13).HeaderText = "Seccion"
    .Columns(13).Width = 60
    .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(14).ReadOnly = True
    .Columns(14).HeaderText = "Rack"
    .Columns(14).Width = 60
    .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(15).ReadOnly = True
    .Columns(15).HeaderText = "Nivel"
    .Columns(15).Width = 60
    .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(16).ReadOnly = True
    .Columns(16).HeaderText = "Espacio"
    .Columns(16).Width = 60
    .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(17).ReadOnly = True
    .Columns(17).HeaderText = "Orden"
    .Columns(17).Width = 40
    .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(17).Visible = False

    If valDiseno = 1 Then
     .Columns(18).ReadOnly = True
     .Columns(18).HeaderText = "Última Fech. Modificación"
     .Columns(18).Width = 90
     .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

     .Columns(19).Visible = False

    End If


    Dim numfilas As Integer

    numfilas = DGInventario.RowCount 'cuenta las filas del DataGrid

    'recorre las filas del DataGrid
    For i = 0 To (numfilas - 1)



     If DGInventario.Item(6, i).Value < 0 Then
      DGInventario.Rows(i).Cells(6).Style.ForeColor = Color.Red
     Else
      DGInventario.Rows(i).Cells(6).Style.ForeColor = Color.Black
     End If

     If DGInventario.Item(9, i).Value < 0 Then
      DGInventario.Rows(i).Cells(9).Style.ForeColor = Color.Red
     Else
      DGInventario.Rows(i).Cells(9).Style.ForeColor = Color.Black

     End If

    Next

   Catch ex As Exception

   End Try

  End With
 End Sub

 Private Sub DGInventario_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGInventario.CellEndEdit

  Dim cnn As SqlConnection = Nothing

  Dim cmd4 As SqlCommand = Nothing

  If valMetodo = 0 Then

   Try
    cnn = New SqlConnection(StrTpm)

    cmd4 = New SqlCommand("UpdateInvPRUEBA", cnn)
    cmd4.CommandType = CommandType.StoredProcedure
    cmd4.Parameters.AddWithValue("@itemcode", DGInventario.Item(0, DGInventario.CurrentCell.RowIndex).Value)
    cmd4.Parameters.AddWithValue("@whscode", DGInventario.Item(3, DGInventario.CurrentCell.RowIndex).Value)
    cmd4.Parameters.AddWithValue("@invfis", DGInventario.Item(5, DGInventario.CurrentCell.RowIndex).Value)

    cnn.Open()

    cmd4.ExecuteNonQuery()
    cmd4.Connection.Close()

    Dim da As New SqlDataAdapter
    da.SelectCommand = cmd4
    da.SelectCommand.Connection = cnn

   Catch ex As Exception
    MsgBox(ex.Message)
   Finally
    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
     cnn.Close()
    End If
   End Try

  ElseIf valMetodo = 1 Then
   Try
    cnn = New SqlConnection(StrTpm)

    cmd4 = New SqlCommand("UpdateInvRecuperado", cnn)
    cmd4.CommandType = CommandType.StoredProcedure
    cmd4.Parameters.AddWithValue("@itemcode", DGInventario.Item(0, DGInventario.CurrentCell.RowIndex).Value)
    cmd4.Parameters.AddWithValue("@whsname", DGInventario.Item(3, DGInventario.CurrentCell.RowIndex).Value)
    cmd4.Parameters.AddWithValue("@invfis", DGInventario.Item(5, DGInventario.CurrentCell.RowIndex).Value)
    cmd4.Parameters.AddWithValue("@UltFecMod", DGInventario.Item(18, DGInventario.CurrentCell.RowIndex).Value)

    cnn.Open()

    cmd4.ExecuteNonQuery()
    cmd4.Connection.Close()

    Dim da As New SqlDataAdapter
    da.SelectCommand = cmd4
    da.SelectCommand.Connection = cnn

    DGInventario.Item(18, DGInventario.CurrentCell.RowIndex).Value = Format(Date.Now, "dd/MM/yyyy")

   Catch ex As Exception
    MsgBox(ex.Message)
   Finally
    If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
     cnn.Close()
    End If
   End Try

  End If

  'Calculo de diferencia
  Try
   '5 StockFis '6 DIFERENCIA   '8 Lista09 '9 CostoTot  '10 Lista01 '11 ImporteTot

   DGInventario.Item(6, DGInventario.CurrentCell.RowIndex).Value = DGInventario.Item(5, DGInventario.CurrentCell.RowIndex).Value - DGInventario.Item(4, DGInventario.CurrentCell.RowIndex).Value
   DGInventario.Item(9, DGInventario.CurrentCell.RowIndex).Value = DGInventario.Item(6, DGInventario.CurrentCell.RowIndex).Value * DGInventario.Item(8, DGInventario.CurrentCell.RowIndex).Value

   DGInventario.Item(11, DGInventario.CurrentCell.RowIndex).Value = DGInventario.Item(10, DGInventario.CurrentCell.RowIndex).Value * DGInventario.Item(6, DGInventario.CurrentCell.RowIndex).Value

   Dim SumCosto As Decimal = 0

   For i = 0 To DGInventario.RowCount - 2
    SumCosto = SumCosto + DGInventario.Item(9, i).Value
   Next

   DGInventario.Item(9, DGInventario.RowCount - 1).Value = SumCosto


   Dim SumImporte As Decimal = 0

   For i = 0 To DGInventario.RowCount - 2
    SumImporte = SumImporte + DGInventario.Item(11, i).Value
   Next

   DGInventario.Item(11, DGInventario.RowCount - 1).Value = SumImporte

   DisenoGrid()

  Catch ex As Exception
   MsgBox(ex.Message)
  End Try

 End Sub

 'Private Sub Button1_Click(sender As Object, e As EventArgs)

 '    Try
 '        Dim exApp As New Microsoft.Office.Interop.Excel.Application
 '        Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
 '        Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

 '        'Añadimos el Libro al programa, y la hoja al libro
 '        exLibro = exApp.Workbooks.Add
 '        exHoja = exLibro.Worksheets.Add()

 '        ' ¿Cuantas columnas y cuantas filas?
 '        Dim NCol As Integer = DGInventario.ColumnCount
 '        Dim NRow As Integer = DGInventario.RowCount

 '        'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
 '        For i As Integer = 1 To NCol
 '            exHoja.Cells.Item(4, i) = DGInventario.Columns(i - 1).Name.ToString
 '            'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
 '        Next

 '        For Fila As Integer = 0 To NRow - 1
 '            For Col As Integer = 0 To NCol - 1
 '                exHoja.Cells.Item(Fila + 5, Col + 1).NumberFormat = "@"
 '                exHoja.Cells.Item(Fila + 5, Col + 1) = DGInventario.Rows(Fila).Cells(Col).Value
 '            Next
 '            Estatus.Visible = True
 '            ProgressBar1.Value = (Fila * 100) / NRow
 '        Next

 '        Estatus.Visible = False

 '        fFormatoExcel(exLibro, NRow)

 '        'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
 '        exHoja.Rows.Item(3).Font.Bold = 1
 '        exHoja.Rows.Item(3).HorizontalAlignment = 3
 '        exHoja.Columns.AutoFit()
 '        'Aplicación visible
 '        exApp.Application.Visible = True


 '        ''Cambiamos orientacion ala hola
 '        exHoja.Cells.Item(1, 1) = "Reporte de Inventario"
 '        exHoja.Cells.Item(2, 1) = "Fecha: " + Date.Now.ToShortDateString

 '        exHoja = Nothing
 '        exLibro = Nothing
 '        exApp = Nothing
 '    Catch ex As Exception
 '        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")

 '    End Try

 'End Sub



 'Private Sub ButtonCARGAR_Click(sender As Object, e As EventArgs)
 '    'DGInventario.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES
 '    DGInventario.DataSource = Nothing
 '    BSave.Visible = False
 '    BExcel.Visible = False
 'End Sub


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



 'Private Sub Button3_Click(sender As Object, e As EventArgs)

 '    DGInventario.DataSource = Nothing
 '    importarExcel(DGInventario)

 '    If DGInventario.RowCount > 0 Then
 '        BSave.Visible = True

 '    End If

 'End Sub

 'Private Sub BSave_Click(sender As Object, e As EventArgs)

 '    If (MessageBox.Show("¿Confirma que desea actualizar estos datos?", _
 '                            "TPD", _
 '                            MessageBoxButtons.YesNo, _
 '                           MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

 '        Dim cnn As SqlConnection = Nothing

 '        Dim cmd4 As SqlCommand = Nothing
 '        For I = 0 To DGInventario.RowCount - 2

 '            Try
 '                cnn = New SqlConnection(StrTpm)

 '                cmd4 = New SqlCommand("UpdateInvExcel", cnn)
 '                cmd4.CommandType = CommandType.StoredProcedure
 '                cmd4.Parameters.AddWithValue("@itemcode", DGInventario.Item(0, I).Value)
 '                cmd4.Parameters.AddWithValue("@whscode", DGInventario.Item(3, I).Value)
 '                cmd4.Parameters.AddWithValue("@invfis", DGInventario.Item(5, I).Value)

 '                cnn.Open()

 '                cmd4.ExecuteNonQuery()
 '                cmd4.Connection.Close()

 '                Dim da As New SqlDataAdapter
 '                da.SelectCommand = cmd4
 '                da.SelectCommand.Connection = cnn

 '            Catch ex As Exception
 '                MsgBox(ex.Message)
 '            Finally
 '                If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
 '                    cnn.Close()
 '                End If
 '            End Try
 '        Next
 '        MsgBox("Registros actualizados correctamente")
 '    End If
 'End Sub

 Private Sub DGInventario_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGInventario.ColumnHeaderMouseClick

  Try

   Dim numfilas As Integer

   numfilas = DGInventario.RowCount 'cuenta las filas del DataGrid

   '5 StockFis '6 DIFERENCIA   '8 Lista09 '9 CostoTot  '10 Lista01 '11 ImporteTot
   For i = 0 To (numfilas - 1)

    If DGInventario.Item(6, i).Value Is DBNull.Value Or DGInventario.Item(9, i).Value Is DBNull.Value Then
     DGInventario.Item(6, i).Value = 0
     DGInventario.Item(9, i).Value = 0
     DGInventario.Item(11, i).Value = 0

    Else
     If DGInventario.Item(6, i).Value < 0 Then
      DGInventario.Rows(i).Cells(6).Style.ForeColor = Color.Red
     Else
      DGInventario.Rows(i).Cells(6).Style.ForeColor = Color.Black
     End If

     If DGInventario.Item(9, i).Value < 0 Then
      DGInventario.Rows(i).Cells(9).Style.ForeColor = Color.Red
     Else
      DGInventario.Rows(i).Cells(9).Style.ForeColor = Color.Black
     End If

     If DGInventario.Item(11, i).Value < 0 Then
      DGInventario.Rows(i).Cells(11).Style.ForeColor = Color.Red
     Else
      DGInventario.Rows(i).Cells(11).Style.ForeColor = Color.Black
     End If

    End If


   Next
  Catch ex As Exception

  End Try
 End Sub

 Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click

  valDiseno = 0

  valMetodo = 0

  DGInventario.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES
  CargarRegistros()
  DisenoGrid()
  BSave.Visible = False
  BExcel.Visible = True

  Dif()
 End Sub

 Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
  DGInventario.DataSource = Nothing
  importarExcel(DGInventario)

  If DGInventario.RowCount > 0 Then
   BSave.Visible = True

  End If
 End Sub

 Private Sub ButtonCARGAR_Click_1(sender As Object, e As EventArgs) Handles ButtonCARGAR.Click

  If (MessageBox.Show("¿Confirma que desea VACIAR estos datos?",
                                "Advertencia",
                                MessageBoxButtons.YesNo,
                               MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

   Dim cnn As SqlConnection = Nothing

   Dim cmd4 As SqlCommand = Nothing
   For i = 0 To DGInventario.RowCount - 1

    Try
     cnn = New SqlConnection(StrTpm)

     cmd4 = New SqlCommand("UpdateInvLIMPIARPRUEBA", cnn)
     cmd4.CommandType = CommandType.StoredProcedure
     cmd4.Parameters.AddWithValue("@itemcode", DGInventario.Item(0, i).Value)
     cmd4.Parameters.AddWithValue("@whscode", DGInventario.Item(3, i).Value)

     'cmd4.Parameters.AddWithValue("@invfis", String.Empty)

     cnn.Open()

     cmd4.ExecuteNonQuery()
     cmd4.Connection.Close()

     Dim da As New SqlDataAdapter
     da.SelectCommand = cmd4
     da.SelectCommand.Connection = cnn

    Catch ex As Exception
     MsgBox(ex.Message)
    Finally
     If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
      cnn.Close()
     End If
    End Try
   Next

   MessageBox.Show("Registros actualizados correctamente.",
                                "Operación exitosa",
                                MessageBoxButtons.OK,
                               MessageBoxIcon.Information)

  End If


  DGInventario.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES
  CargarRegistros()
  DisenoGrid()
 End Sub

 Private Sub BSave_Click_1(sender As Object, e As EventArgs) Handles BSave.Click
  If (MessageBox.Show("¿Confirma que desea actualizar estos datos?",
                                "TPD",
                                MessageBoxButtons.YesNo,
                               MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

   Dim cnn As SqlConnection = Nothing

   Dim cmd4 As SqlCommand = Nothing
   For I = 0 To DGInventario.RowCount - 1

    Try
     cnn = New SqlConnection(StrTpm)

     cmd4 = New SqlCommand("UpdateInvExcelPRUEBA", cnn)
     cmd4.CommandType = CommandType.StoredProcedure
     cmd4.Parameters.AddWithValue("@itemcode", DGInventario.Item(0, I).Value)
     cmd4.Parameters.AddWithValue("@whscode", DGInventario.Item(3, I).Value)
     cmd4.Parameters.AddWithValue("@invfis", DGInventario.Item(5, I).Value)

     cnn.Open()

     cmd4.ExecuteNonQuery()
     cmd4.Connection.Close()

     Dim da As New SqlDataAdapter
     da.SelectCommand = cmd4
     da.SelectCommand.Connection = cnn

    Catch ex As Exception
     MsgBox(ex.Message)
    Finally
     If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
      cnn.Close()
     End If
    End Try
   Next
   MsgBox("Registros actualizados correctamente")
  End If
 End Sub

 Private Sub BExcel_Click(sender As Object, e As EventArgs) Handles BExcel.Click
  Try
   Dim exApp As New Microsoft.Office.Interop.Excel.Application
   Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
   Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

   'Añadimos el Libro al programa, y la hoja al libro
   exLibro = exApp.Workbooks.Add
   exHoja = exLibro.Worksheets.Add()

   ' ¿Cuantas columnas y cuantas filas?
   Dim NCol As Integer = DGInventario.ColumnCount
   Dim NRow As Integer = DGInventario.RowCount

   'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
   For i As Integer = 1 To NCol
    exHoja.Cells.Item(4, i) = DGInventario.Columns(i - 1).Name.ToString
    'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
   Next

   For Fila As Integer = 0 To NRow - 1
    For Col As Integer = 0 To NCol - 1
     exHoja.Cells.Item(Fila + 5, Col + 1).NumberFormat = "@"
     exHoja.Cells.Item(Fila + 5, Col + 1) = DGInventario.Rows(Fila).Cells(Col).Value
    Next
    Estatus.Visible = True
    ProgressBar1.Value = (Fila * 100) / NRow
   Next

   Estatus.Visible = False

   fFormatoExcel(exLibro, NRow)

   'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
   exHoja.Rows.Item(3).Font.Bold = 1
   exHoja.Rows.Item(3).HorizontalAlignment = 3
   exHoja.Columns.AutoFit()
   'Aplicación visible
   exApp.Application.Visible = True


   ''Cambiamos orientacion ala hola
   exHoja.Cells.Item(1, 1) = "Reporte de Inventario"
   exHoja.Cells.Item(2, 1) = "Fecha: " + Date.Now.ToShortDateString

   exLibro.Worksheets("Hoja2").Columns(18).HIDDEN = True

   exHoja = Nothing
   exLibro = Nothing
   exApp = Nothing
  Catch ex As Exception
   MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")

  End Try

 End Sub

 Private Sub fFormatoExcel(exLibro As Microsoft.Office.Interop.Excel.Workbook, NRow As Integer)
  Try

   exLibro.Worksheets("Hoja2").Cells.Range("A1:B1").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja2").Cells.Range("A2:B2").Interior.ColorIndex = 15

   exLibro.Worksheets("Hoja2").Cells.Range("A1:B1").FONT.BOLD = True
   exLibro.Worksheets("Hoja2").Cells.Range("A1:B1").FONT.BOLD = True

   exLibro.Worksheets("Hoja2").Cells.Range("A4:S4").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja2").Cells.Range("A4:S4").FONT.BOLD = True

   'exLibro.Worksheets("Hoja1").Columns(11).NumberFormat = "###.0000"

   exLibro.Worksheets("Hoja2").Columns("E:I").NumberFormat = "###,###,###"


   For i As Integer = 5 To NRow + 5 - 1
    exLibro.Worksheets("Hoja2").Cells.Item(i, 5).INTERIOR.COLORINDEX = 15     '6 GRIS CLARO
    exLibro.Worksheets("Hoja2").Cells.Item(i, 6).INTERIOR.COLORINDEX = 19     '6 YELLOW
   Next


  Catch ex As Exception
   MsgBox(ex.Message)
  End Try

 End Sub

 Private Sub DGInventario_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGInventario.RowPrePaint
  Try

   DGInventario.Rows(e.RowIndex).Cells("Stock Fisico").Style.BackColor = Color.LightGray

   DGInventario.Rows(e.RowIndex).Cells("Stock Sist").Style.BackColor = Color.LightYellow

  Catch ex As Exception

  End Try
 End Sub


 Private Sub CKGuardar_CheckedChanged(sender As Object, e As EventArgs) Handles CKGuardar.CheckedChanged

  If CKGuardar.Checked = True Then
   BtnRecuperar.Visible = False
   CKRecuperar.Checked = False

   If DGInventario.RowCount > 0 Then
    If (MessageBox.Show(
                                "¿Confirma que desea GUARDAR el reporte con los siguientes datos?" + Chr(13) + Chr(13) & "Almacén: " & CBAlmacen.Text &
                                "" + Chr(13) + Chr(13) & "Línea: " & CBLinea.Text & "    " + Chr(13) + Chr(13) & "Fecha de modificación: " & Date.Now,
                                 "Guardar datos", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

     Dim cnn As SqlConnection = Nothing

     Dim cmd4 As SqlCommand = Nothing
     For I = 0 To DGInventario.RowCount - 1

      Try
       cnn = New SqlConnection(StrTpm)

       cmd4 = New SqlCommand("SPAuditoriaInvGuardaRep", cnn)
       cmd4.CommandType = CommandType.StoredProcedure
       cmd4.Parameters.AddWithValue("@itemcode", DGInventario.Item(0, I).Value)
       cmd4.Parameters.AddWithValue("@ItemName", DGInventario.Item(1, I).Value)
       cmd4.Parameters.AddWithValue("@ItmsGrpNam", DGInventario.Item(2, I).Value)
       cmd4.Parameters.AddWithValue("@WhsName", DGInventario.Item(3, I).Value)
       cmd4.Parameters.AddWithValue("@StockSist", DGInventario.Item(4, I).Value)
       cmd4.Parameters.AddWithValue("@StockFis", DGInventario.Item(5, I).Value)
       cmd4.Parameters.AddWithValue("@Diferencia", DGInventario.Item(6, I).Value)
       cmd4.Parameters.AddWithValue("@peso", DGInventario.Item(7, I).Value)
       cmd4.Parameters.AddWithValue("@lista9", DGInventario.Item(8, I).Value)
       cmd4.Parameters.AddWithValue("@Total", DGInventario.Item(9, I).Value)
       cmd4.Parameters.AddWithValue("@lista1", DGInventario.Item(10, I).Value)
       cmd4.Parameters.AddWithValue("@ImporteT", DGInventario.Item(11, I).Value)
       cmd4.Parameters.AddWithValue("@Bloque", DGInventario.Item(12, I).Value)
       cmd4.Parameters.AddWithValue("@Seccion", DGInventario.Item(13, I).Value)
       cmd4.Parameters.AddWithValue("@Rack", DGInventario.Item(14, I).Value)
       cmd4.Parameters.AddWithValue("@Nivel", DGInventario.Item(15, I).Value)
       cmd4.Parameters.AddWithValue("@Espacio", DGInventario.Item(16, I).Value)
       cmd4.Parameters.AddWithValue("@Orden", DGInventario.Item(17, I).Value)
       cmd4.Parameters.AddWithValue("@UltFecMod", Date.Now)

       cnn.Open()

       cmd4.ExecuteNonQuery()
       cmd4.Connection.Close()

       Dim da As New SqlDataAdapter
       da.SelectCommand = cmd4
       da.SelectCommand.Connection = cnn

      Catch ex As Exception
       MsgBox(ex.Message)
      Finally
       If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
        cnn.Close()
       End If
      End Try
     Next

     CKGuardar.Checked = False
     MessageBox.Show("Datos guardados exitosamente", "Well Done", MessageBoxButtons.OK, MessageBoxIcon.Question)

    End If

   Else
    CKGuardar.Checked = False
    MessageBox.Show("No hay registros que guardar.", "Verifique", MessageBoxButtons.OK, MessageBoxIcon.Question)
   End If

  End If

 End Sub


 Private Sub CKRecuperar_CheckedChanged(sender As Object, e As EventArgs) Handles CKRecuperar.CheckedChanged
  If CKRecuperar.Checked = True Then
   BtnRecuperar.Visible = True
   LabelFecMod.Visible = True
   CBFecMod.Visible = True

   CKGuardar.Checked = False

   Dim ConsutaLista As String

   Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)

    Dim DSetTablas As New DataSet

    If CBAlmacen.Text = "TODOS" Then
     If CBLinea.Text = "TODAS" Then
      ConsutaLista = "SELECT UltFecMod FROM AuditoriaInv GROUP BY UltFecMod ORDER BY UltFecMod "
     Else
      ConsutaLista = "SELECT UltFecMod FROM AuditoriaInv WHERE ItmsGrpNam='" & CBLinea.Text & "' GROUP BY UltFecMod ORDER BY UltFecMod "
     End If
    Else
     ConsutaLista = "SELECT UltFecMod FROM AuditoriaInv WHERE ItmsGrpNam='" & CBLinea.Text & "' AND WHSNAME='" & CBAlmacen.Text & "'  GROUP BY UltFecMod ORDER BY UltFecMod "
    End If



    Dim daAlmacen As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

    'Dim DSetTablas As New DataSet
    daAlmacen.Fill(DSetTablas, "Almacen")

    ''Dim fila As Data.DataRow

    ' ''Asignamos a fila la nueva Row(Fila)del Dataset
    ''fila = DSetTablas.Tables("Almacen").NewRow

    ' ''Agregamos los valores a los campos de la tabla
    ''fila("UltFecMod") = "12/12/1999"

    ' ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
    ''DSetTablas.Tables("Almacen").Rows.Add(fila)

    Me.CBFecMod.DataSource = DSetTablas.Tables("Almacen")
    Me.CBFecMod.DisplayMember = "UltFecMod"
    'Me.CBFecMod.SelectedItem = "12/12/1999"
    'Me.CBAlmacen.SelectedValue = 99

   End Using

  Else
   BtnRecuperar.Visible = False
   LabelFecMod.Visible = False
   CBFecMod.Visible = False
  End If
 End Sub


 Private Sub CBAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBAlmacen.SelectedIndexChanged

  CBFecMod.Text = ""
  Try
   Dim ConsutaLista As String

   Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)

    Dim DSetTablas As New DataSet

    If CBAlmacen.Text = "TODOS" Then
     If CBLinea.Text = "TODAS" Then
      ConsutaLista = "SELECT UltFecMod FROM AuditoriaInv GROUP BY UltFecMod ORDER BY UltFecMod "
     Else
      ConsutaLista = "SELECT UltFecMod FROM AuditoriaInv WHERE ItmsGrpNam='" & CBLinea.Text & "' GROUP BY UltFecMod ORDER BY UltFecMod "
     End If
    Else
     If CBLinea.Text = "TODAS" Then
      ConsutaLista = "SELECT UltFecMod FROM AuditoriaInv WHERE WHSNAME='" & CBAlmacen.Text & "'  GROUP BY UltFecMod ORDER BY UltFecMod "
     Else
      ConsutaLista = "SELECT UltFecMod FROM AuditoriaInv WHERE ItmsGrpNam='" & CBLinea.Text & "' AND WHSNAME='" & CBAlmacen.Text & "'  GROUP BY UltFecMod ORDER BY UltFecMod "
     End If
    End If



    Dim daAlmacen As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

    'Dim DSetTablas As New DataSet
    daAlmacen.Fill(DSetTablas, "Almacen")

    'Dim fila As Data.DataRow

    ''Asignamos a fila la nueva Row(Fila)del Dataset
    'fila = DSetTablas.Tables("Almacen").NewRow

    ''Agregamos los valores a los campos de la tabla
    'fila("whsname") = "TODOS"
    'fila("whscode") = 99

    ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
    'DSetTablas.Tables("Almacen").Rows.Add(fila)

    Me.CBFecMod.DataSource = DSetTablas.Tables("Almacen")
    Me.CBFecMod.DisplayMember = "UltFecMod"
    'Me.CBAlmacen.ValueMember = "whscode"
    'Me.CBAlmacen.SelectedValue = 99

   End Using

  Catch ex As Exception

  End Try
 End Sub

 Private Sub CBLinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBLinea.SelectedIndexChanged

  CBFecMod.Text = ""
  Try
   Dim ConsutaLista As String

   Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)

    Dim DSetTablas As New DataSet

    If CBAlmacen.Text = "TODOS" Then
     If CBLinea.Text = "TODAS" Then
      ConsutaLista = "SELECT UltFecMod FROM AuditoriaInv GROUP BY UltFecMod ORDER BY UltFecMod "
     Else
      ConsutaLista = "SELECT UltFecMod FROM AuditoriaInv WHERE ItmsGrpNam='" & CBLinea.Text & "' GROUP BY UltFecMod ORDER BY UltFecMod "
     End If
    Else
     If CBLinea.Text = "TODAS" Then
      ConsutaLista = "SELECT UltFecMod FROM AuditoriaInv WHERE WHSNAME='" & CBAlmacen.Text & "'  GROUP BY UltFecMod ORDER BY UltFecMod "
     Else
      ConsutaLista = "SELECT UltFecMod FROM AuditoriaInv WHERE ItmsGrpNam='" & CBLinea.Text & "' AND WHSNAME='" & CBAlmacen.Text & "'  GROUP BY UltFecMod ORDER BY UltFecMod "
     End If
    End If

    Dim daAlmacen As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

    'Dim DSetTablas As New DataSet
    daAlmacen.Fill(DSetTablas, "Almacen")

    'Dim fila As Data.DataRow

    ''Asignamos a fila la nueva Row(Fila)del Dataset
    'fila = DSetTablas.Tables("Almacen").NewRow

    ''Agregamos los valores a los campos de la tabla
    'fila("whsname") = "TODOS"
    'fila("whscode") = 99

    ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
    'DSetTablas.Tables("Almacen").Rows.Add(fila)

    Me.CBFecMod.DataSource = DSetTablas.Tables("Almacen")
    Me.CBFecMod.DisplayMember = "UltFecMod"
    'Me.CBAlmacen.ValueMember = "whscode"
    'Me.CBAlmacen.SelectedValue = 99

   End Using

  Catch ex As Exception

  End Try

 End Sub

 Private Sub BtnRecuperar_Click(sender As Object, e As EventArgs) Handles BtnRecuperar.Click
  valDiseno = 1

  valMetodo = 1

  DGInventario.Columns.Clear()

  conexion.Open()

  Dim cmd4 As SqlCommand = Nothing
  cmd4 = New SqlCommand("SPInventarioRec", conexion)
  cmd4.CommandType = CommandType.StoredProcedure

  cmd4.Parameters.AddWithValue("@ALMACEN", CBAlmacen.Text)
  cmd4.Parameters.AddWithValue("@LINEA", CBLinea.Text)

  If CBFecMod.Text = "" Then
   cmd4.Parameters.AddWithValue("@FECMOD", Date.Parse("01/01/2015").Date.ToString("yyyyMMdd"))
  Else
   cmd4.Parameters.AddWithValue("@FECMOD", Date.Parse(CBFecMod.Text).Date.ToString("yyyyMMdd"))
  End If

  cmd4.ExecuteNonQuery()
  cmd4.Connection.Close()
  Dim da2 As New SqlDataAdapter
  da2.SelectCommand = cmd4
  da2.SelectCommand.Connection = conexion

  ''--------------------------------------------
  Dim DsVtas As New DataSet
  da2.Fill(DsVtas, "DsVtas")
  DsVtas.Tables(0).TableName = "Detalle"
  DvInventarioRec.Table = DsVtas.Tables("Detalle")
  DGInventario.DataSource = DvInventarioRec
  DisenoGrid()
  'CKRecuperar
 End Sub

End Class