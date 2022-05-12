
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Public Class DiasInv

 Public StrProd As String = conexion_universal.CadenaSBO_Diamante
 Public StrTpm As String = conexion_universal.CadenaSQL
 Public StrCon As String = conexion_universal.CadenaSQLSAP

 Dim DvInventario As New DataView
 Dim DvArticulos As New DataView
 Dim DvArticulos2 As New DataView
 Dim DvLineas As New DataView



 Private Sub DisenoGrid()
  Try
   With DGDiasInv
    '.DataSource = DtAgte
    '.ReadOnly = True
    'Color de Renglones en Grid
    .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
    .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    .DefaultCellStyle.BackColor = Color.AliceBlue
    .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

    DGDiasInv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    'Propiedad para no mostrar el cuadro que se encuentra en la parte
    'Superior Izquierda del gridview
    .RowHeadersVisible = True
    .RowHeadersWidth = 25


    'Se desactiva mostrar pues a partir de aquí se desactiva la línea
    'Articulo	
    .Columns(0).HeaderText = "Mostrar"
    .Columns(0).Width = 50
    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(0).Visible = False

    'Articulo	
    .Columns(1).HeaderText = "Código"
    .Columns(1).Width = 60
    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    'Descripcion	
    .Columns(2).HeaderText = "Línea"
    .Columns(2).Width = 150

    'Linea	
    .Columns(3).HeaderText = "Código Alm."
    .Columns(3).Width = 60
    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    'Precio L9
    .Columns(4).HeaderText = "Almacén"
    .Columns(4).Width = 120
    '.Columns(4).DefaultCellStyle.Format = "$ ###,###.#0"

    'Vta Neta
    .Columns(5).HeaderText = "Días Inventario"
    .Columns(5).Width = 60
    .Columns(5).DefaultCellStyle.Format = "###,##0"
    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   End With
  Catch ex As Exception

  End Try
 End Sub

 Private Sub ActualizaLineas()
  Dim conDelete As New SqlConnection
  Dim cmdDelete As New SqlCommand
  Dim conUpdate As New SqlConnection
  Dim cmdUpdate As New SqlCommand
  Dim CadenaSQLDelete As String = ""
  Dim CadenaSQLUpdate As String = ""



  CadenaSQLDelete = "DELETE T0 FROM DiasInv2 T0
                     Left Join SBO_TPD.dbo.OITB T1 ON T0.ItmsGrpCod = T1.ItmsGrpCod
                     WHERE T1.ItmsGrpCod Is NULL"
  Try
   conDelete.ConnectionString = StrTpm
   conDelete.Open()
   cmdDelete.Connection = conDelete
   cmdDelete.CommandText = CadenaSQLDelete
   cmdDelete.ExecuteNonQuery()

  Catch ex As Exception
   MessageBox.Show("Error al actualizar lineas" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

  Finally
   conDelete.Close()
  End Try


  CadenaSQLUpdate = "INSERT INTO DiasInv2
               SELECT DISTINCT 1 Mostrar,T1.ItmsGrpCod, T1.ItmsGrpNam, T2.WhsCode, T2.WhsName, 0 DiasInv
               FROM SBO_TPD.dbo.OWHS T2, DiasInv2 T0
               RIGHT JOIN SBO_TPD.dbo.OITB T1 ON T0.ItmsGrpCod = T1.ItmsGrpCod
               WHERE T0.ItmsGrpCod IS NULL
               AND T2.WhsCode IN ('01','03','07')
               AND T1.ItmsGrpCod NOT IN (193, 200)
               ORDER BY T1.ItmsGrpCod ASC"

  Try
   conUpdate.ConnectionString = StrTpm
   conUpdate.Open()
   cmdUpdate.Connection = conUpdate
   cmdUpdate.CommandText = CadenaSQLUpdate
   cmdUpdate.ExecuteNonQuery()

  Catch ex As Exception
   MessageBox.Show("Error al actualizar lineas" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

  Finally
   conUpdate.Close()
  End Try
 End Sub

 Private Sub DiasInv_Load(sender As Object, e As EventArgs) Handles MyBase.Load

  TraspasoAlm.Button3.Enabled = False

  Try
   'Primero Actualizo la tabla de DiasInv2 ingresando las lineas que no existan en nla tabla
   ActualizaLineas()

   Dim DTRefacciones As New DataTable

   ' crear nueva conexión    
   Dim conexion2 As New SqlConnection(StrTpm)

   ' abrir la conexión con la base de datos   
   conexion2.Open()

   Dim Adaptador As New SqlDataAdapter()
   Dim comando As New SqlCommand

   Dim SQLTPD As String

   SQLTPD = "SELECT Mostrar,ItmsGrpCod,ItmsGrpNam,WhsCode,WhsName, "
   SQLTPD &= "DiasInv "
   SQLTPD &= "FROM DiasInv2 "
   SQLTPD &= "WHERE WhsCode = @AlmacenOri OR WhsCode = @Almacen "
   SQLTPD &= "ORDER BY ItmsGrpNam, WhsCode "

   ' Nuevo objeto Dataset   
   Dim DsVtasDet As New DataSet

   DsVtasDet.Tables.Add(DTRefacciones)

   With comando

    .Parameters.AddWithValue("@Almacen", TraspasoAlm.CBAlmacenDestino.SelectedValue)
    .Parameters.AddWithValue("@AlmacenOri", TraspasoAlm.cbxAlmacenOri.SelectedValue)

    ' Asignar el sql para seleccionar los datos de la tabla Maestro   
    .CommandText = SQLTPD
    .Connection = conexion2
   End With


   Dim DtFactProv As New DataTable

   With Adaptador
    .SelectCommand = comando
    ' llenar el dataset   
    .Fill(DtFactProv)
   End With

   DvInventario = DtFactProv.DefaultView

   With Me.DGDiasInv
    .DataSource = DvInventario

    .AllowUserToAddRows = False

   End With



   With conexion2
    If .State = ConnectionState.Open Then
     .Close()
    End If
    .Dispose()
   End With


   'MsgBox(TraspasoAlm.CBAlmacenDestino.SelectedValue)



   '**************-----------LLNEAR ComboBox LINEAS
   Dim ConsutaLista As String

   Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

    Dim DSetTablas As New DataSet

    ' -----------------------------------------------------
    Try

     Dim DSetTablas2 As New DataSet
     ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB WHERE ItmsGrpCod NOT IN ('193','200','150') "

     Dim daarticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


     daarticulo.Fill(DSetTablas2, "Articulos2")

     Dim filaArticulo As Data.DataRow

     'Asignamos a fila la nueva Row(Fila)del Dataset
     filaArticulo = DSetTablas2.Tables("Articulos2").NewRow

     'Agregamos los valores a los campos de la tabla
     filaArticulo("ItmsGrpNam") = "TODAS"
     filaArticulo("ItmsGrpCod") = 999

     'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
     DSetTablas2.Tables("Articulos2").Rows.Add(filaArticulo)

     DvArticulos2.Table = DSetTablas2.Tables("Articulos2")

     Me.CBLineas.DataSource = DvArticulos2
     Me.CBLineas.DisplayMember = "ItmsGrpNam"
     Me.CBLineas.ValueMember = "ItmsGrpCod"
     Me.CBLineas.SelectedValue = 999

     ' -----------------------------------------------------
     MuestraLineas()


    Catch ex As Exception
     MsgBox(ex.Message)
    End Try

   End Using


  Catch ex As Exception
   MsgBox(ex.Message)
  End Try

  DisenoGrid()

 End Sub


 Private Sub MuestraLineas()

 End Sub


 Private Sub CBLineas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBLineas.SelectedIndexChanged
  Try

   If CBLineas.SelectedItem Is Nothing Or CBLineas.Text = "TODAS" Then
    DvInventario.RowFilter = String.Empty

   Else
    DvInventario.RowFilter = "ItmsGrpCod = " & Trim(Me.CBLineas.SelectedValue.ToString)
   End If

  Catch ex As Exception
   'MsgBox(ex.Message)
  End Try

 End Sub

 'Private Sub BActualizar_Click(sender As Object, e As EventArgs) Handles BActualizar.Click
 '    UpdateInv()
 'End Sub

 'Private Sub UpdateInv()
 '    Dim cnn As SqlConnection = Nothing

 '    Dim cmd4 As SqlCommand = Nothing

 '    Try
 '        cnn = New SqlConnection(StrTpm)

 '        cmd4 = New SqlCommand("SPUpdateDiasInvCheck", cnn)
 '        cmd4.CommandType = CommandType.StoredProcedure

 '        cmd4.Parameters.AddWithValue("@ItemsGrpCod", DGDiasInv.Item(1, DGDiasInv.CurrentCell.RowIndex).Value)
 '        cmd4.Parameters.AddWithValue("@WhsCode", DGDiasInv.Item(3, DGDiasInv.CurrentCell.RowIndex).Value)
 '        cmd4.Parameters.AddWithValue("@DiasInv", DGDiasInv.Item(5, DGDiasInv.CurrentCell.RowIndex).Value)

 '        cnn.Open()

 '        cmd4.ExecuteNonQuery()
 '        cmd4.Connection.Close()
 '        Dim da As New SqlDataAdapter
 '        da.SelectCommand = cmd4
 '        da.SelectCommand.Connection = cnn

 '        ''--------------------------------------------
 '        'Dim DsUpdate As New DataSet
 '        'da.Fill(DsUpdate, "DsUpdate")

 '        'DsUpdate.Tables(0).TableName = "Articulos"

 '        'DvArticulos.Table = DsUpdate.Tables("Articulos")


 '        'DGDiasInv.DataSource = DvArticulos



 '    Catch ex As Exception
 '        MsgBox(ex.Message)
 '        'MsgBox("No existen ventas de este día")
 '    Finally
 '        If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
 '            cnn.Close()
 '        End If
 '    End Try
 'End Sub

 'Private Sub DGDiasInv_CurrentCellChanged(sender As Object, e As EventArgs)

 '    Try

 '        UpdateInv()

 '    Catch ex As Exception
 '        MsgBox(ex.Message)
 '    End Try


 'End Sub


 Private Sub bExcel_Click(sender As Object, e As EventArgs) Handles bExcel.Click
  Try

   Dim exApp As New Microsoft.Office.Interop.Excel.Application
   Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

   'Añadimos el Libro al programa
   exLibro = exApp.Workbooks.Add

   ' ¿Cuantas columnas y cuantas filas?
   Dim NCol As Integer = DGDiasInv.ColumnCount
   Dim NRow As Integer = DGDiasInv.RowCount

   'fFormatoExcel(exLibro, NRow)

   'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
   For i As Integer = 1 To NCol
    exLibro.Worksheets("Hoja1").Cells.Item(2, i) = DGDiasInv.Columns(i - 1).Name.ToString
   Next

   For Fila As Integer = 0 To NRow - 1

    For Col As Integer = 0 To NCol - 1
     exLibro.Worksheets("Hoja1").Cells.Item(Fila + 3, Col + 1) = DGDiasInv.Rows(Fila).Cells(Col).Value

    Next
    'Estatus.Visible = True
    'ProgressBar1.Value = (Fila * 100) / NRow
   Next
   'Estatus.Visible = False

   'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
   exLibro.Worksheets("Hoja1").Rows.Item(2).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Rows.Item(2).HorizontalAlignment = 3
   exLibro.Worksheets("Hoja1").Cells.Range("A2:F2").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Rows.Item(2).WrapText = True
   exLibro.Worksheets("Hoja1").Columns.AutoFit()
   exLibro.Worksheets("Hoja1").name = "Días de Inventario "

   'oSheet.range("A1").value = "Reporte de Ventas TOTALES de todos los AGENTES con FECHA "
   'Aplicación visible
   exLibro.Worksheets.Application.Visible = True

   exLibro = Nothing
   exApp = Nothing

  Catch ex As Exception

  End Try
 End Sub

 Private Sub DiasInv_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
  TraspasoAlm.Button3.Enabled = True
 End Sub

 Private Sub DGDiasInv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDiasInv.CellContentClick
  Dim VErrorG As Integer = 0
  Dim strValue As String


  If e.RowIndex >= 0 Then
   Dim row As DataGridViewRow = DGDiasInv.Rows(e.RowIndex)
   'Try

   If Me.DGDiasInv.Columns(e.ColumnIndex).Name = "Mostrar" Then
    'The user clicked on the checkbox column
    strValue = Me.DGDiasInv.Item(e.ColumnIndex, e.RowIndex).Value

    If strValue = True Then

     If MessageBox.Show("¿Confirma que desea desactivar esta línea?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

      DGDiasInv.Rows(e.RowIndex).Cells("Mostrar").Value = 0

      Dim cnn As SqlConnection = Nothing

      Dim cmd4 As SqlCommand = Nothing

      Try
       cnn = New SqlConnection(StrTpm)

       cmd4 = New SqlCommand("SPUpdateDiasInvMostrar", cnn)
       cmd4.CommandType = CommandType.StoredProcedure

       cmd4.Parameters.AddWithValue("@ItemsGrpCod", DGDiasInv.Item(1, DGDiasInv.CurrentCell.RowIndex).Value)
       cmd4.Parameters.AddWithValue("@Mostrar", DGDiasInv.Item(0, DGDiasInv.CurrentCell.RowIndex).Value)
       cmd4.Parameters.AddWithValue("@Almacen", TraspasoAlm.CBAlmacenDestino.SelectedValue)
       cmd4.Parameters.AddWithValue("@AlmacenOri", TraspasoAlm.cbxAlmacenOri.SelectedValue)

       cnn.Open()

       cmd4.ExecuteNonQuery()
       cmd4.Connection.Close()
       Dim da As New SqlDataAdapter
       da.SelectCommand = cmd4
       da.SelectCommand.Connection = cnn

       ''--------------------------------------------
       Dim DsUpdate As New DataSet
       da.Fill(DsUpdate, "DsUpdate")

       DsUpdate.Tables(0).TableName = "Articulos"

       DvInventario.Table = DsUpdate.Tables("Articulos")


       DGDiasInv.DataSource = DvInventario


      Catch ex As Exception
       MsgBox(ex.Message)
       'MsgBox("No existen ventas de este día")
      Finally
       If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
        cnn.Close()
       End If
      End Try

     Else
      DGDiasInv.Rows(e.RowIndex).Cells("Mostrar").Value = 1
      Me.DGDiasInv.RefreshEdit()

     End If

    Else

     If MessageBox.Show("¿Confirma que desea activar esta línea?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

      DGDiasInv.Rows(e.RowIndex).Cells("Mostrar").Value = 1

      Dim cnn As SqlConnection = Nothing

      Dim cmd4 As SqlCommand = Nothing

      Try
       cnn = New SqlConnection(StrTpm)

       cmd4 = New SqlCommand("SPUpdateDiasInvMostrar", cnn)
       cmd4.CommandType = CommandType.StoredProcedure

       cmd4.Parameters.AddWithValue("@ItemsGrpCod", DGDiasInv.Item(1, DGDiasInv.CurrentCell.RowIndex).Value)
       cmd4.Parameters.AddWithValue("@Mostrar", DGDiasInv.Item(0, DGDiasInv.CurrentCell.RowIndex).Value)
       cmd4.Parameters.AddWithValue("@Almacen", TraspasoAlm.CBAlmacenDestino.SelectedValue)
       cmd4.Parameters.AddWithValue("@AlmacenOri", TraspasoAlm.cbxAlmacenOri.SelectedValue)

       cnn.Open()

       cmd4.ExecuteNonQuery()
       cmd4.Connection.Close()
       Dim da As New SqlDataAdapter
       da.SelectCommand = cmd4
       da.SelectCommand.Connection = cnn

       ''--------------------------------------------
       Dim DsUpdate As New DataSet
       da.Fill(DsUpdate, "DsUpdate")

       DsUpdate.Tables(0).TableName = "Articulos"

       DvInventario.Table = DsUpdate.Tables("Articulos")


       DGDiasInv.DataSource = DvInventario

      Catch ex As Exception
       MsgBox(ex.Message)
       'MsgBox("No existen ventas de este día")
      Finally
       If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
        cnn.Close()
       End If
      End Try

     Else
      DGDiasInv.Rows(e.RowIndex).Cells("Mostrar").Value = 0
      Me.DGDiasInv.RefreshEdit()

     End If
    End If
   End If
  End If


 End Sub

 'Private Sub DGDiasInv_CellEndEdit_1(sender As Object, e As DataGridViewCellEventArgs) Handles DGDiasInv.CellEndEdit
 '    UpdateInv()
 'End Sub


 Private Sub ReinicioDiasInventario()
  Dim cnn As SqlConnection = Nothing

  Dim cmd4 As SqlCommand = Nothing

  Try
   cnn = New SqlConnection(StrTpm)

   cmd4 = New SqlCommand("SPReiniciarDiasInv", cnn)
   cmd4.CommandType = CommandType.StoredProcedure
   cmd4.Parameters.AddWithValue("@AlmacenD", AlmacenD)
   cmd4.Parameters.AddWithValue("@AlmacenOri", AlmacenOri)

   cnn.Open()

   cmd4.ExecuteNonQuery()
   cmd4.Connection.Close()
   Dim da As New SqlDataAdapter
   da.SelectCommand = cmd4
   da.SelectCommand.Connection = cnn

   ''--------------------------------------------
   Dim DsUpdate As New DataSet
   da.Fill(DsUpdate, "DsUpdate")

   DsUpdate.Tables(0).TableName = "Articulos"

   DvArticulos.Table = DsUpdate.Tables("Articulos")


   DGDiasInv.DataSource = DvArticulos


  Catch ex As Exception
   MsgBox(ex.Message)
   'MsgBox("No existen ventas de este día")
  Finally
   If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
    cnn.Close()
   End If
  End Try
 End Sub

 Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
  If (MessageBox.Show(
                           "¿Confirma que desea reiniciar los dias de inventario?",
                            "Reiniciar dias de Inventario", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
   ReinicioDiasInventario()
  End If
 End Sub

 'Private Sub CBLineas_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBLineas.SelectionChangeCommitted
 '    Try

 '        If CBLineas.SelectedItem Is Nothing Or CBLineas.Text = "TODAS" Then
 '            DvInventario.RowFilter = String.Empty

 '        Else
 '            DvInventario.RowFilter = "ItmsGrpCod = " & Trim(Me.CBLineas.SelectedValue.ToString)
 '        End If

 '    Catch ex As Exception
 '        MsgBox(ex.Message)
 '    End Try
 'End Sub

 Private Sub BGuardar_Click(sender As Object, e As EventArgs) Handles BGuardar.Click



  Dim cnn As SqlConnection = Nothing

  Dim cmd4 As SqlCommand = Nothing

  Dim NumRow As Integer

  NumRow = DGDiasInv.RowCount


  If MessageBox.Show("¿Confirma que desea guardar los dias de inventario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

   For i = 0 To NumRow - 1

    'MsgBox(DGDiasInv.Item(1, i).Value)

    Try

     cnn = New SqlConnection(StrTpm)

     cmd4 = New SqlCommand("SPUpdateDiasInvCheck", cnn)
     cmd4.CommandType = CommandType.StoredProcedure

     cmd4.Parameters.AddWithValue("@ItemsGrpCod", DGDiasInv.Item(1, i).Value)
     cmd4.Parameters.AddWithValue("@WhsCode", DGDiasInv.Item(3, i).Value)
     cmd4.Parameters.AddWithValue("@DiasInv", DGDiasInv.Item(5, i).Value)

     cnn.Open()

     cmd4.ExecuteNonQuery()
     cmd4.Connection.Close()
     Dim da As New SqlDataAdapter
     da.SelectCommand = cmd4
     da.SelectCommand.Connection = cnn

     ''--------------------------------------------
     'Dim DsUpdate As New DataSet
     'da.Fill(DsUpdate, "DsUpdate")

     'DsUpdate.Tables(0).TableName = "Articulos"

     'DvArticulos.Table = DsUpdate.Tables("Articulos")


     'DGDiasInv.DataSource = DvArticulos



    Catch ex As Exception
     MessageBox.Show("Error al Guardar" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
     'MsgBox("No existen ventas de este día")
    Finally
     If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
      cnn.Close()
     End If

    End Try
   Next

   MessageBox.Show("Registros actualizados correctamente", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)

  End If

 End Sub
End Class