Imports System.Data.SqlClient

'Imports System.filesys

Public Class FormatoVacaciones
 'Public StrTpm As String = conexion_universal.CadenaSQL
 Public conexion As New SqlConnection(StrTpm)
 'Public conexion2 As New SqlConnection(conexion_universal.CadenaSQL)

 Public DvDetalle As New DataView
 Dim DvLP As New DataView
 Dim strTemp As String = ""
 Dim NumOVta As Long
 Dim NumAuto As Integer

 Dim Antiguedad As Decimal
 Dim NumDiasVac As Integer
 Dim AñosTrascurridos As Integer
 Dim DiaVac As Integer

 Dim Modificado As Integer = 0

 Dim DiasSol As Integer
 Dim FechaIngreso As String
 Dim TopeGlobal As Integer

 Dim dt As DataSet


 ' Espacios de nombres  
 ' ''''''''''''''''''''''''''''''''''''''''' 

 Dim DvAgentes As New DataView

 'BindingSource  
 Private WithEvents bs As New BindingSource

 ' Adaptador de datos sql  
 Private SqlDataAdapter As SqlDataAdapter

 ' Cadena de conexión  
 Private cs As String = conexion_universal.cConstanteTPM

 ' flag  
 Private bEdit As Boolean

 Public StrCon As String = conexion_universal.CadenaSQLSAP

 Private FolioMax As Integer

 Private Sub FormatoVacaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
  Dim ConsutaLista As String


  Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)


   Dim DSetTablas As New DataSet
   ConsutaLista = "SELECT NumEmp,NomEmp+' '+AppEmp+' '+ApmMat AS 'NomEmp' FROM Empleados where Status = 'Activo' and Vacaciones = 'Si' ORDER BY NomEmp "
   Dim daGEmpleado As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

   'Dim DSetTablas As New DataSet
   daGEmpleado.Fill(DSetTablas, "Empleados")

   'AGREGAR FILA
   Dim fila As Data.DataRow
   'Asignamos a fila la nueva Row(Fila)del Dataset
   fila = DSetTablas.Tables("Empleados").NewRow
   'Agregamos los valores a los campos de la tabla
   fila("NomEmp") = "--Ningun Resultado--"
   fila("NumEmp") = 1010
   'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
   DSetTablas.Tables("Empleados").Rows.Add(fila)


   DvLP.Table = DSetTablas.Tables("Empleados")
   DvLP.RowFilter = "NumEmp <> 1010"

   Me.CBNomEmp.DataSource = DvLP
   Me.CBNomEmp.DisplayMember = "NomEmp"
   Me.CBNomEmp.ValueMember = "NumEmp"

   Me.CBNomEmp.SelectedIndex = -1

  End Using

  'Procedimiento para obtener el número de transacción más actual
  Dim cmdCuenta As New Data.SqlClient.SqlCommand
  Dim FormatWO As String = ""
  cmdCuenta.CommandText = "SELECT MAX(Folio) FROM SolicitudVacaciones "
  cmdCuenta.CommandType = CommandType.Text
  cmdCuenta.Connection = New Data.SqlClient.SqlConnection(StrTpm)
  cmdCuenta.Connection.Open()
  'NumOVta = IIf(IsDBNull(cmdCuenta.ExecuteScalar()), 0, Val(cmdCuenta.ExecuteScalar()))

  With cmdCuenta
   NumOVta = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
   .Connection.Close()
  End With

  FolioMax = NumOVta

  If FolioMax = 0 Then
   FolioMax = 1
  End If

  NumOVta += 1

  TBFolio.Text = NumOVta
  TBFolio.Text = Format(NumOVta, "0000")
  TBFolio.TextAlign = HorizontalAlignment.Right

  'DisenoGridVArt()

  CBDiasSol.SelectedIndex = 0

  DTPFec1.Value = Date.Now
  DTPFec2.Value = Date.Now
  DTPFec3.Value = Date.Now
  DTPFec4.Value = Date.Now
  DTPFec5.Value = Date.Now

  CBNomEmp.Focus()

  TBDiasRest.Text = ""


  If TBFolio.Text = 1 Then
   PictureBox1.Enabled = False
   PictureBox2.Enabled = False
  End If

 End Sub

 Private Sub TBNumEmp_Leave(sender As Object, e As EventArgs) Handles TBNumEmp.Leave

 End Sub

 Private Sub LimpiaCampos()
  Try

   TBNumEmp.Text = ""
   CBDiasSol.SelectedValue = 99
   DTPFecIng.Value = Date.Now
   TBAntiguedad.Text = ""
   TBDiasVac.Text = ""
   TBFecIniVac.Text = ""
   TBFecCadVac.Text = ""
   TBDiasRest.Text = ""
   TextBox1.Text = ""
   TextBox2.Text = ""

   DTPFec1.Value = Date.Now
   DTPFec2.Value = Date.Now
   DTPFec3.Value = Date.Now
   DTPFec4.Value = Date.Now
   DTPFec5.Value = Date.Now

   CKAut1.Checked = False
   CKAut2.Checked = False
   CKAut3.Checked = False
   CKAut4.Checked = False
   CKAut5.Checked = False

   'DGVCap.DataSource = ""
   DGVCap.Rows.Clear()

  Catch ex As Exception

  End Try

  'DGVCap.DataSource = String.Empty
 End Sub


 Private Sub CBNomEmp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBNomEmp.SelectedIndexChanged
  'Try
  '    Rutina()
  'Catch ex As Exception

  'End Try

 End Sub


 Private Sub CBDiasSol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBDiasSol.SelectedIndexChanged

  'If CBDiasSol.Text = "" Then
  '    DTPFec1.Visible = False
  '    DTPFec2.Visible = False
  '    DTPFec3.Visible = False
  '    DTPFec4.Visible = False
  '    DTPFec5.Visible = False

  '    CKAut1.Visible = False
  '    CKAut2.Visible = False
  '    CKAut3.Visible = False
  '    CKAut4.Visible = False
  '    CKAut5.Visible = False

  '    'regresar DTPicker a DIA ACTUAL
  '    DTPFec1.Value = Date.Now
  '    DTPFec2.Value = Date.Now
  '    DTPFec3.Value = Date.Now
  '    DTPFec4.Value = Date.Now
  '    DTPFec5.Value = Date.Now
  '    'desactivar checked
  '    CKAut1.Checked = False
  '    CKAut2.Checked = False
  '    CKAut3.Checked = False
  '    CKAut4.Checked = False
  '    CKAut5.Checked = False



  'ElseIf CBDiasSol.Text = "1" Then
  '    DTPFec1.Visible = True
  '    DTPFec2.Visible = False
  '    DTPFec3.Visible = False
  '    DTPFec4.Visible = False
  '    DTPFec5.Visible = False

  '    CKAut1.Visible = True
  '    CKAut2.Visible = False
  '    CKAut3.Visible = False
  '    CKAut4.Visible = False
  '    CKAut5.Visible = False

  '    'regresar DTPicker a DIA ACTUAL
  '    DTPFec2.Value = Date.Now
  '    DTPFec3.Value = Date.Now
  '    DTPFec4.Value = Date.Now
  '    DTPFec5.Value = Date.Now
  '    'desactivar checked
  '    CKAut2.Checked = False
  '    CKAut3.Checked = False
  '    CKAut4.Checked = False
  '    CKAut5.Checked = False

  'ElseIf CBDiasSol.Text = "2" Then
  '    DTPFec1.Visible = True
  '    DTPFec2.Visible = True
  '    DTPFec3.Visible = False
  '    DTPFec4.Visible = False
  '    DTPFec5.Visible = False

  '    CKAut1.Visible = True
  '    CKAut2.Visible = True
  '    CKAut3.Visible = False
  '    CKAut4.Visible = False
  '    CKAut5.Visible = False

  '    'regresar DTPicker a DIA ACTUAL
  '    DTPFec3.Value = Date.Now
  '    DTPFec4.Value = Date.Now
  '    DTPFec5.Value = Date.Now
  '    'desactivar checked
  '    CKAut3.Checked = False
  '    CKAut4.Checked = False
  '    CKAut5.Checked = False

  'ElseIf CBDiasSol.Text = "3" Then
  '    DTPFec1.Visible = True
  '    DTPFec2.Visible = True
  '    DTPFec3.Visible = True
  '    DTPFec4.Visible = False
  '    DTPFec5.Visible = False

  '    CKAut1.Visible = True
  '    CKAut2.Visible = True
  '    CKAut3.Visible = True
  '    CKAut4.Visible = False
  '    CKAut5.Visible = False

  '    'regresar DTPicker a DIA ACTUAL
  '    DTPFec4.Value = Date.Now
  '    DTPFec5.Value = Date.Now
  '    'desactivar checked
  '    CKAut4.Checked = False
  '    CKAut5.Checked = False

  'ElseIf CBDiasSol.Text = "4" Then
  '    DTPFec1.Visible = True
  '    DTPFec2.Visible = True
  '    DTPFec3.Visible = True
  '    DTPFec4.Visible = True

  '    CKAut1.Visible = True
  '    CKAut2.Visible = True
  '    CKAut3.Visible = True
  '    CKAut4.Visible = True

  '    'regresar DTPicker a DIA ACTUAL
  '    DTPFec5.Value = Date.Now
  '    'desactivar checked
  '    CKAut5.Checked = False

  'ElseIf CBDiasSol.Text = "5" Then
  '    DTPFec1.Visible = True
  '    DTPFec2.Visible = True
  '    DTPFec3.Visible = True
  '    DTPFec4.Visible = True
  '    DTPFec5.Visible = True

  '    CKAut1.Visible = True
  '    CKAut2.Visible = True
  '    CKAut3.Visible = True
  '    CKAut4.Visible = True
  '    CKAut5.Visible = True

  'End If



 End Sub




 Private Sub Actualizar(Optional ByVal bCargar As Boolean = True)
  ' Actualizar y guardar cambios  
  'If Not bs.DataSource Is Nothing Then
  '    SqlDataAdapter.Update(CType(bs.DataSource, DataTable))
  '    If bCargar Then
  '        'cargar_registros("Select * from SC_Objetivos order by anio DESC, mes DESC, groupname DESC, slpname", DGObjetivos)
  '        'cargar_registros("Select * from SC_Objetivos where mes = " & Month(Now) & " order by anio DESC, mes DESC, groupcode DESC, slpname", DGObjetivos)

  '        cargar_registros("SELECT '',0,NULL,NULL," & TBDiasVac.Text & " UNION ALL SELECT *,1,0 FROM SolVacacionesHistorico WHERE NumEmpleado=" & TBNumEmp.Text & "AND Periodo=" & TBPeriodoCom.Text & "", DGVacaciones)

  '        Disenogrid()

  '        'If DGVacaciones.RowCount > 1 Then
  '        '    For i = 1 To DGVacaciones.RowCount - 1
  '        '        DGVacaciones.Item(4, i).Value = TBDiasVac.Text - i
  '        '    Next
  '        'End If

  '    End If
  'End If
 End Sub

 'Private Sub Disenogrid()
 '    With DGVacaciones
 '        ' Establecer el origen de datos para el DataGridview  
 '        DGVacaciones.DataSource = bs

 '        ' alternar color de filas  
 '        .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
 '        .DefaultCellStyle.BackColor = Color.CornflowerBlue
 '        .DefaultCellStyle.BackColor = Color.AliceBlue
 '        .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
 '        '.RowsDefaultCellStyle = 

 '        'Propiedad para no mostrar el cuadro que se encuentra en la parte
 '        'Superior Izquierda del gridview
 '        .RowHeadersVisible = False
 '        '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
 '        'Color de linea del grid

 '        'centrar encabezados del datagrid 
 '        .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


 '        Try
 '            .Columns(0).Visible = False
 '            .Columns(0).HeaderText = "No. Empleado"
 '            .Columns(0).Width = 40
 '            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

 '            .Columns(1).Visible = False
 '            .Columns(1).HeaderText = "Ejercicio"
 '            .Columns(1).Width = 40
 '            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


 '            .Columns(2).HeaderText = "Histórico"
 '            .Columns(2).Width = 100
 '            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

 '            .Columns(3).HeaderText = "Días Tomados"
 '            .Columns(3).Width = 70
 '            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

 '            .Columns(4).HeaderText = "Días restantes"
 '            .Columns(4).Width = 70
 '            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

 '            'If DGVacaciones.RowCount > 1 Then
 '            '    If DGVacaciones.RowCount > 1 Then
 '            '        For i = 1 To DGVacaciones.RowCount - 1
 '            '            DGVacaciones.Item(4, i).Value = TBDiasVac.Text - i
 '            '        Next
 '            '    End If
 '            'End If

 '        Catch ex As Exception
 '            'MsgBox(ex.Message)
 '        End Try

 '    End With
 'End Sub


 Private Sub GenerarDiasVac(ByVal dia As String, ByVal periodo As String)
  'MsgBox("entre " & Convert.ToDateTime(dia).ToString & "--" & periodo)
  'If CmbAgteVta.SelectedValue = 999 And Mes <> 99 Then
  '---Inserta objetivos en 0 de todos los AGENTES 
  '----PROCEDIMIENTO

  Dim conexion As New SqlConnection(StrTpm)
  Dim command As New SqlCommand("SPInsertDiasAtzado", conexion)

  Try

   command.CommandType = CommandType.StoredProcedure
   command.Parameters.AddWithValue("@Folio", TBFolio.Text)
   command.Parameters.AddWithValue("@NumEmpleado", TBNumEmp.Text)
   command.Parameters.AddWithValue("@Periodo", periodo)
   command.Parameters.AddWithValue("@DiaSol", Convert.ToDateTime(dia))
   command.Parameters.AddWithValue("@DiasRest", 0)


   'If DiaVac = 1 Then
   '    'MsgBox(DGVCap.Item("Restantes", 0).Value.ToString)
   '    command.Parameters.AddWithValue("@DiaSol", DTPFec1.Value)
   '    command.Parameters.AddWithValue("@DiasRest", DGVCap.Item("Restantes", 0).Value.ToString)
   'ElseIf DiaVac = 2 Then
   '    command.Parameters.AddWithValue("@DiaSol", DTPFec2.Value)
   '    command.Parameters.AddWithValue("@DiasRest", DGVCap.Item("Restantes", 1).Value.ToString)
   'ElseIf DiaVac = 3 Then
   '    command.Parameters.AddWithValue("@DiaSol", DTPFec3.Value)
   '    command.Parameters.AddWithValue("@DiasRest", DGVCap.Item("Restantes", 2).Value.ToString)
   'ElseIf DiaVac = 4 Then
   '    command.Parameters.AddWithValue("@DiaSol", DTPFec4.Value)
   '    command.Parameters.AddWithValue("@DiasRest", DGVCap.Item("Restantes", 3).Value.ToString)
   'ElseIf DiaVac = 5 Then
   '    command.Parameters.AddWithValue("@DiaSol", DTPFec5.Value)
   '    command.Parameters.AddWithValue("@DiasRest", DGVCap.Item("Restantes", 4).Value.ToString)
   'End If

   conexion.Open()
   command.ExecuteNonQuery()
   'cargar_registros("SELECT ''  AS 'NumEmpleado',0 'Periodo',NULL 'DiaSol',NULL 'DiasTomados'," & TBDiasVac.Text & _
   '                         " 'DiasRest' UNION ALL SELECT NumEmpleado,Periodo,DiaSol,1,0 FROM SolVacacionesHistorico WHERE NumEmpleado='" & _
   '                         TBNumEmp.Text & "' AND Periodo=" & TBPeriodoCom.SelectedValue.ToString & " ORDER BY NumEmpleado,DiaSol DESC", DGVacaciones)

   'If DGVacaciones.RowCount > 1 Then
   '    For i = 1 To DGVacaciones.RowCount - 2
   '        DGVacaciones.Item(4, i).Value = TBDiasVac.Text - i
   '    Next
   'End If

   'TBDiasRest.Text = TBDiasRest.Text - 1

   Modificado = 1


   'If Antiguedad < 1 And Convert.ToInt32(TBDiasRest.Text) = -6 Then
   '    'TBPeriodoCom.Text = TBPeriodoCom.Text + 1
   'ElseIf Convert.ToInt32(TBDiasVac.Text) < Convert.ToInt32(TBDiasRest.Text) Then
   '    'TBPeriodoCom.Text = TBPeriodoCom.Text + 1
   'End If

  Catch ex As Exception
   MessageBox.Show(ex.Message)
   'MsgBox("Estos registros ya existen")
  Finally
   conexion.Dispose()
   command.Dispose()
  End Try

  'MsgBox("el valor de mes es: " & Mes)

  'End If
 End Sub

 Private Sub EliminarDiasVac()

  'If CmbAgteVta.SelectedValue = 999 And Mes <> 99 Then
  '---Inserta objetivos en 0 de todos los AGENTES 
  '----PROCEDIMIENTO
  'For i As Integer = 0 To DGVCap.RowCount - 1
  Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
  Dim command As New SqlCommand("SPEliminaDiasAtzado", conexion)
  Try



   command.CommandType = CommandType.StoredProcedure
   command.Parameters.AddWithValue("@Folio", TBFolio.Text)
   command.Parameters.AddWithValue("@NumEmpleado", TBNumEmp.Text)
   command.Parameters.AddWithValue("@Periodo", TBPeriodoCom.SelectedValue.ToString)

   If DiaVac = 1 Then
    command.Parameters.AddWithValue("@DiaSol", DTPFec1.Value)
   ElseIf DiaVac = 2 Then
    command.Parameters.AddWithValue("@DiaSol", DTPFec2.Value)
   ElseIf DiaVac = 3 Then
    command.Parameters.AddWithValue("@DiaSol", DTPFec3.Value)
   ElseIf DiaVac = 4 Then
    command.Parameters.AddWithValue("@DiaSol", DTPFec4.Value)
   ElseIf DiaVac = 5 Then
    command.Parameters.AddWithValue("@DiaSol", DTPFec5.Value)
   End If

   conexion.Open()
   command.ExecuteNonQuery()

  Catch ex As Exception
   MessageBox.Show(ex.Message)
   'MsgBox("Estos registros ya existen")
  Finally
   conexion.Dispose()
   command.Dispose()
  End Try

  'Next

  'cargar_registros("SELECT ''  AS 'NumEmpleado',0 'Periodo',NULL 'DiaSol',NULL 'DiasTomados'," & TBDiasVac.Text & _
  '                         " 'DiasRest' UNION ALL SELECT NumEmpleado,Periodo,DiaSol,1,0 FROM SolVacacionesHistorico WHERE NumEmpleado='" & _
  '                         TBNumEmp.Text & "' AND Periodo=" & TBPeriodoCom.SelectedValue.ToString & " ORDER BY NumEmpleado,DiaSol DESC", DGVacaciones)

  'If DGVacaciones.RowCount > 1 Then
  '    For i = 1 To DGVacaciones.RowCount - 2
  '        DGVacaciones.Item(4, i).Value = TBDiasVac.Text - i
  '    Next
  'End If


  TBDiasRest.Text = TBDiasRest.Text + 1


  'MsgBox("el valor de mes es: " & Mes)

  'End If
 End Sub

 Private Sub CKAut1_Click(sender As Object, e As EventArgs) Handles CKAut1.Click
  'MsgBox(TBDiasRest.Text.ToString)
  If CBNomEmp.Text <> "" Then

   If TBDiasAut.Text = "" Then
    TBDiasAut.Text = 0
   End If

   If CKAut1.Checked = True Then

    'If DGVacaciones.RowCount > 1 Then

    '    'MsgBox(DTPFec1.Text)

    '    For i As Integer = 1 To DGVacaciones.RowCount - 1
    '        'MsgBox(DGVArt.Item(0, DGVArt.CurrentCell.RowIndex).Value & " = " & DGVCap.Item(1, i).Value)
    '        If DTPFec1.Text = DGVacaciones.Item(2, i).Value Then
    '            'MsgBox("El artículo ya ha sido agregado.")

    '            MessageBox.Show("Este día ya ha sido asignado anteriormente, " & _
    '                            "seleccione otro para continuar.", "Error al agregar.",
    '            MessageBoxButtons.OK, MessageBoxIcon.Information)

    '            CKAut1.Checked = False
    '            Return
    '        End If
    '    Next

    'End If

    If (MessageBox.Show(
                         "¿Confirma autorización del día " & DTPFec1.Text & "? ",
                         "Proceso de autorización.", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

     DiaVac = 1

     'GenerarDiasVac()

     NumAuto = TBDiasAut.Text + 1

     Try
      TBDiasRest.Text = TBDiasRest.Text - 1
      DGVCap.Rows.Add(TBFolio.Text, TBNumEmp.Text, TBPeriodoCom.Text, DTPFec1.Text.ToString, TBDiasRest.Text)
     Catch ex As Exception
      'MsgBox(ex.Message)
     End Try

    Else
     CKAut1.Checked = False
    End If

   Else 'si checked = false
    If (MessageBox.Show(
                        "¿Confirma que desea quitar autorización del día " & DTPFec1.Text & "? ",
                        "Proceso de autorización.", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

     DiaVac = 1

     EliminarDiasVac()

     NumAuto = TBDiasAut.Text - 1

     Try
      For i As Integer = 0 To DGVCap.RowCount - 1
       'MsgBox(DTPFec1.Text)
       'MsgBox(DGVCap.Item(3, i).Value)
       'Dim auxfec1 As String = DTPFec1.Text.ToString

       If DTPFec1.Text = DGVCap.Item(3, i).Value Then
        'MsgBox("verdadero")

        DGVCap.CurrentCell = DGVCap.Rows(i).Cells(0)

        DGVCap.Rows(i).Selected = True

        Me.DGVCap.Rows.Remove(DGVCap.CurrentRow)
       End If
      Next
     Catch ex As Exception
      'MsgBox(ex.Message)
     End Try

    Else
     CKAut1.Checked = True
    End If

   End If

   TBDiasAut.Text = NumAuto

  End If

 End Sub

 Private Sub CKAut2_Click(sender As Object, e As EventArgs) Handles CKAut2.Click

  If CBNomEmp.Text <> "" Then

   If TBDiasAut.Text = "" Then
    TBDiasAut.Text = 0
   End If

   If CKAut2.Checked = True Then

    'If DGVacaciones.RowCount > 1 Then

    '    'MsgBox(DTPFec1.Text)

    '    For i As Integer = 1 To DGVacaciones.RowCount - 1
    '        'MsgBox(DGVArt.Item(0, DGVArt.CurrentCell.RowIndex).Value & " = " & DGVCap.Item(1, i).Value)
    '        If DTPFec2.Text = DGVacaciones.Item(2, i).Value Then
    '            'MsgBox("El artículo ya ha sido agregado.")

    '            MessageBox.Show("Este día ya ha sido asignado anteriormente, " & _
    '                            "seleccione otro para continuar.", "Error al agregar.",
    '            MessageBoxButtons.OK, MessageBoxIcon.Information)

    '            CKAut2.Checked = False
    '            Return
    '        End If
    '    Next

    'End If

    If (MessageBox.Show(
                         "¿Confirma autorización del día " & DTPFec2.Text & "? ",
                         "Proceso de autorización.", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

     DiaVac = 2

     'GenerarDiasVac()

     NumAuto = TBDiasAut.Text + 1

     Try
      TBDiasRest.Text = TBDiasRest.Text - 1
      Me.DGVCap.Rows.Add(TBFolio.Text, TBNumEmp.Text, TBPeriodoCom.Text, DTPFec2.Text, TBDiasRest.Text)
     Catch ex As Exception
      'MsgBox(ex.Message)
     End Try

    Else
     CKAut2.Checked = False
    End If

   Else 'si checked = false
    If (MessageBox.Show(
                        "¿Confirma que desea quitar autorización del día " & DTPFec2.Text & "? ",
                        "Proceso de autorización.", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

     DiaVac = 2

     EliminarDiasVac()

     NumAuto = TBDiasAut.Text - 1

     Try
      For i As Integer = 0 To DGVCap.RowCount - 1
       'MsgBox(DTPFec1.Text)
       'MsgBox(DGVCap.Item(3, i).Value)
       'Dim auxfec1 As String = DTPFec1.Text.ToString

       If DTPFec2.Text = DGVCap.Item(3, i).Value Then
        'MsgBox("verdadero")
        DGVCap.CurrentCell = DGVCap.Rows(i).Cells(0)
        Me.DGVCap.Rows.Remove(DGVCap.CurrentRow)
       End If
      Next
     Catch ex As Exception
      'MsgBox(ex.Message)
     End Try

    Else
     CKAut2.Checked = True
    End If

   End If

   TBDiasAut.Text = NumAuto

  End If
 End Sub


 Private Sub CKAut3_Click(sender As Object, e As EventArgs) Handles CKAut3.Click

  If CBNomEmp.Text <> "" Then

   If TBDiasAut.Text = "" Then
    TBDiasAut.Text = 0
   End If

   If CKAut3.Checked = True Then

    'If DGVacaciones.RowCount > 1 Then

    '    'MsgBox(DTPFec1.Text)

    '    For i As Integer = 1 To DGVacaciones.RowCount - 1
    '        'MsgBox(DGVArt.Item(0, DGVArt.CurrentCell.RowIndex).Value & " = " & DGVCap.Item(1, i).Value)
    '        If DTPFec3.Text = DGVacaciones.Item(2, i).Value Then
    '            'MsgBox("El artículo ya ha sido agregado.")

    '            MessageBox.Show("Este día ya ha sido asignado anteriormente, " & _
    '                            "seleccione otro para continuar.", "Error al agregar.",
    '            MessageBoxButtons.OK, MessageBoxIcon.Information)

    '            CKAut3.Checked = False
    '            Return
    '        End If
    '    Next

    'End If

    If (MessageBox.Show(
                         "¿Confirma autorización del día " & DTPFec3.Text & "? ",
                         "Proceso de autorización.", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

     DiaVac = 3

     'GenerarDiasVac()

     NumAuto = TBDiasAut.Text + 1

     Try
      TBDiasRest.Text = TBDiasRest.Text - 1
      Me.DGVCap.Rows.Add(TBFolio.Text, TBNumEmp.Text, TBPeriodoCom.Text, DTPFec3.Text, TBDiasRest.Text)
     Catch ex As Exception
      MsgBox(ex.Message)
     End Try

    Else
     CKAut3.Checked = False
    End If

   Else 'si checked = false
    If (MessageBox.Show(
                        "¿Confirma que desea quitar autorización del día " & DTPFec3.Text & "? ",
                        "Proceso de autorización.", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

     DiaVac = 3

     EliminarDiasVac()

     NumAuto = TBDiasAut.Text - 1

     Try
      For i As Integer = 0 To DGVCap.RowCount - 1
       'MsgBox(DTPFec1.Text)
       'MsgBox(DGVCap.Item(3, i).Value)
       'Dim auxfec1 As String = DTPFec1.Text.ToString

       If DTPFec3.Text = DGVCap.Item(3, i).Value Then
        'MsgBox("verdadero")
        DGVCap.CurrentCell = DGVCap.Rows(i).Cells(0)

        Me.DGVCap.Rows.Remove(DGVCap.CurrentRow)
       End If
      Next
     Catch ex As Exception
      'MsgBox(ex.Message)
     End Try

    Else
     CKAut3.Checked = True
    End If

   End If

   TBDiasAut.Text = NumAuto

  End If

 End Sub

 Private Sub CKAut4_Click(sender As Object, e As EventArgs) Handles CKAut4.Click

  If CBNomEmp.Text <> "" Then

   If TBDiasAut.Text = "" Then
    TBDiasAut.Text = 0
   End If

   If CKAut4.Checked = True Then

    'If DGVacaciones.RowCount > 1 Then

    '    'MsgBox(DTPFec1.Text)

    '    For i As Integer = 1 To DGVacaciones.RowCount - 1
    '        'MsgBox(DGVArt.Item(0, DGVArt.CurrentCell.RowIndex).Value & " = " & DGVCap.Item(1, i).Value)
    '        If DTPFec4.Text = DGVacaciones.Item(2, i).Value Then
    '            'MsgBox("El artículo ya ha sido agregado.")

    '            MessageBox.Show("Este día ya ha sido asignado anteriormente, " & _
    '                            "seleccione otro para continuar.", "Error al agregar.",
    '            MessageBoxButtons.OK, MessageBoxIcon.Information)

    '            CKAut4.Checked = False
    '            Return
    '        End If
    '    Next

    'End If

    If (MessageBox.Show(
                         "¿Confirma autorización del día " & DTPFec4.Text & "? ",
                         "Proceso de autorización.", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

     DiaVac = 4

     'GenerarDiasVac()

     NumAuto = TBDiasAut.Text + 1

     Try
      TBDiasRest.Text = TBDiasRest.Text - 1
      Me.DGVCap.Rows.Add(TBFolio.Text, TBNumEmp.Text, TBPeriodoCom.Text, DTPFec4.Text, TBDiasRest.Text)
     Catch ex As Exception
      MsgBox(ex.Message)
     End Try

    Else
     CKAut4.Checked = False
    End If

   Else 'si checked = false
    If (MessageBox.Show(
                        "¿Confirma que desea quitar autorización del día " & DTPFec4.Text & "? ",
                        "Proceso de autorización.", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

     DiaVac = 4

     EliminarDiasVac()

     NumAuto = TBDiasAut.Text - 1

     Try
      For i As Integer = 0 To DGVCap.RowCount - 1
       'MsgBox(DTPFec1.Text)
       'MsgBox(DGVCap.Item(3, i).Value)
       'Dim auxfec1 As String = DTPFec1.Text.ToString

       If DTPFec4.Text = DGVCap.Item(3, i).Value Then
        'MsgBox("verdadero")
        DGVCap.CurrentCell = DGVCap.Rows(i).Cells(0)
        Me.DGVCap.Rows.Remove(DGVCap.CurrentRow)
       End If
      Next
     Catch ex As Exception
      'MsgBox(ex.Message)
     End Try

    Else
     CKAut4.Checked = True
    End If

   End If

   TBDiasAut.Text = NumAuto

  End If

 End Sub

 Private Sub CKAut5_Click(sender As Object, e As EventArgs) Handles CKAut5.Click

  If CBNomEmp.Text <> "" Then

   If TBDiasAut.Text = "" Then
    TBDiasAut.Text = 0
   End If

   If CKAut5.Checked = True Then

    'If DGVacaciones.RowCount > 1 Then

    '    'MsgBox(DTPFec1.Text)

    '    For i As Integer = 1 To DGVacaciones.RowCount - 1
    '        'MsgBox(DGVArt.Item(0, DGVArt.CurrentCell.RowIndex).Value & " = " & DGVCap.Item(1, i).Value)
    '        If DTPFec5.Text = DGVacaciones.Item(2, i).Value Then
    '            'MsgBox("El artículo ya ha sido agregado.")

    '            MessageBox.Show("Este día ya ha sido asignado anteriormente, " & _
    '                            "seleccione otro para continuar.", "Error al agregar.",
    '            MessageBoxButtons.OK, MessageBoxIcon.Information)

    '            CKAut5.Checked = False
    '            Return
    '        End If
    '    Next

    'End If

    If (MessageBox.Show(
                         "¿Confirma autorización del día " & DTPFec5.Text & "? ",
                         "Proceso de autorización.", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

     DiaVac = 5

     'GenerarDiasVac()

     NumAuto = TBDiasAut.Text + 1

     Try
      TBDiasRest.Text = TBDiasRest.Text - 1
      Me.DGVCap.Rows.Add(TBFolio.Text, TBNumEmp.Text, TBPeriodoCom.Text, DTPFec5.Text, TBDiasRest.Text)
     Catch ex As Exception
      'MsgBox(ex.Message)
     End Try

    Else
     CKAut5.Checked = False
    End If

   Else 'si checked = false
    If (MessageBox.Show(
                        "¿Confirma que desea quitar autorización del día " & DTPFec5.Text & "? ",
                        "Proceso de autorización.", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

     DiaVac = 5

     EliminarDiasVac()

     NumAuto = TBDiasAut.Text - 1

     Try
      For i As Integer = 0 To DGVCap.RowCount - 1
       'MsgBox(DTPFec1.Text)
       'MsgBox(DGVCap.Item(3, i).Value)
       'Dim auxfec1 As String = DTPFec1.Text.ToString

       If DTPFec5.Text = DGVCap.Item(3, i).Value Then
        'MsgBox("verdadero")
        DGVCap.CurrentCell = DGVCap.Rows(i).Cells(0)
        Me.DGVCap.Rows.Remove(DGVCap.CurrentRow)
       End If
      Next
     Catch ex As Exception
      'MsgBox(ex.Message)
     End Try

    Else
     CKAut5.Checked = True
    End If

   End If

   TBDiasAut.Text = NumAuto

  End If
 End Sub

 Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click

  Dim vSinValor As Integer = 0

  If (CBNomEmp.SelectedIndex.ToString = "-1") Then
   MessageBox.Show("Selecciona un Empleado", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

   'BtnImprimir.Enabled = True
   Return
  End If


  If DGVCap.RowCount < 1 Then
   vSinValor = 1
  End If

  If vSinValor = 1 Then
   MessageBox.Show("No hay días de vacaciones autorizados. Verifique.", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

   'BtnImprimir.Enabled = True
   Return
  End If

  Try
   If CBNomEmp.Text <> "" Then

    If (MessageBox.Show(
                            "¿Confirma que desea guardar esta solicitud de vacaciones? ",
                            "Guardar solicitud.", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then



     'If CKAut1.Checked = True Then
     '    iteracion = 1
     'End If
     'If CKAut2.Checked = True Then
     '    iteracion = 2
     'End If
     'If CKAut3.Checked = True Then
     '    iteracion = 3
     'End If
     'If CKAut4.Checked = True Then
     '    iteracion = 4
     'End If
     'If CKAut5.Checked = True Then
     '    iteracion = 5
     'End If

     'MsgBox(iteracion)
     'Dim row As DataGridViewRow = DGVCap.Rows(e.RowIndex)

     Dim row As DataGridViewRow
     For it As Integer = 0 To (DGVCap.RowCount) - 1
      row = DGVCap.Rows(it)
      'MsgBox(row.Cells("Periodo").Value.ToString.Substring(0, 4) & " -- " & row.Cells("DiaSol").Value.ToString)
      GenerarDiasVac(row.Cells("DiaSol").Value.ToString, row.Cells("Periodo").Value.ToString.Substring(0, 4))
     Next

     'For it As Integer = 0 To iteracion - 1
     '    DiaVac = it + 1
     '    GenerarDiasVac()
     'Next



     'Return

     GenerarSolicitudVac()
     Me.DGVCap.Columns("Borrar").Visible = False
     MessageBox.Show("Registros guardados correctamente.", "Operación exitosa.", MessageBoxButtons.OK,
                   MessageBoxIcon.Information)

     Modificado = 0

     TBNumEmp.Enabled = False
     CBNomEmp.Enabled = False
     CBDiasSol.Enabled = False

     BtnImprimir.Enabled = True
     BtnNvo.Enabled = True
     BSave.Enabled = False

    Else
     Modificado = 1
    End If

   Else

    'If CKAut1.Checked = False And CKAut2.Checked = False And CKAut3.Checked = False And CKAut4.Checked = False And CKAut5.Checked = False Then

    '    MessageBox.Show("No hay días autorizados. Verifique. ", _
    '                         "Guardar solicitud.", MessageBoxButtons.OK, _
    '                         MessageBoxIcon.Exclamation)
    '    Return
    'End If

    'If (MessageBox.Show( _
    '             "¿Confirma que desea asignar este día de vacaciones para todos los empleados? ", _
    '             "Guardar solicitud.", MessageBoxButtons.YesNo, _
    '             MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

    '    GenerarSolicitudVacTODOS()

    '    BSave.Enabled = False
    '    BtnNvo.Enabled = True
    '    BtnImprimir.Enabled = False

    '    MessageBox.Show("Registros guardados correctamente.", "Operación exitosa.", MessageBoxButtons.OK, _
    '    MessageBoxIcon.Information)

    'Else
    '    Modificado = 1
    'End If
   End If

  Catch ex As Exception
   MsgBox(ex.Message)
  End Try

  'Me.Enabled = False
  'BtnImprimir.Enabled = True
 End Sub

 Private Sub FormatoVacaciones_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

  If Modificado = 1 Then

   If DGVCap.RowCount > 0 Then
    Try
     If (MessageBox.Show(
                                "¿Desea guardar los cambios realizados? ",
                                "Guardar solicitud.", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

      GenerarSolicitudVac()

      MessageBox.Show("Registros guardados correctamente.", "Operación exitosa.", MessageBoxButtons.OK,
                       MessageBoxIcon.Information)

     Else

      EliminarHistorico()

     End If

    Catch ex As Exception
     MsgBox(ex.Message)
    End Try
   End If

  End If
 End Sub

 Private Sub GenerarSolicitudVac()
  '----PROCEDIMIENTO 1
  Dim conexion As New SqlConnection(StrTpm)
  Dim command As New SqlCommand("SPGuardaSol", conexion)
  Try
   command.CommandType = CommandType.StoredProcedure
   command.Parameters.AddWithValue("@Folio", TBFolio.Text)
   command.Parameters.AddWithValue("@FechaSol", DTPFecSol.Value)
   command.Parameters.AddWithValue("@NumEmpleado", TBNumEmp.Text)
   command.Parameters.AddWithValue("@NomEmpleado", CBNomEmp.Text)
   command.Parameters.AddWithValue("@FechaIMSS", Convert.ToDateTime(TextBox1.Text.ToString))
   command.Parameters.AddWithValue("@Antiguedad", TBAntiguedad.Text)
   command.Parameters.AddWithValue("@DiasVac", TBDiasVac.Text)
   command.Parameters.AddWithValue("@FecIniVac", Date.Parse(TBFecIniVac.Text).Date.ToString("yyyyMMdd"))
   command.Parameters.AddWithValue("@FecCadVac", Date.Parse(TBFecCadVac.Text).Date.ToString("yyyyMMdd"))
   command.Parameters.AddWithValue("@Periodo", TextBox2.Text.ToString.Substring(0, 4))
   command.Parameters.AddWithValue("@DiasRest", TBDiasRest.Text)
   command.Parameters.AddWithValue("@DiasSol", 0)
   command.Parameters.AddWithValue("@DiasAut", 0)

   conexion.Open()
   command.ExecuteNonQuery()
  Catch ex As Exception
   MessageBox.Show(ex.Message)
   'MsgBox("Estos registros ya existen")
  Finally
   conexion.Dispose()
   command.Dispose()
  End Try

 End Sub

 Private Sub GenerarSolicitudVacTODOS()
  'MsgBox("ey2")
  '----PROCEDIMIENTO 1
  If CKAut1.Checked = True Then
   Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
   Dim command As New SqlCommand("SPGuardaSolTODOS", conexion)
   Try
    command.CommandType = CommandType.StoredProcedure

    command.Parameters.AddWithValue("@FechaSol", DTPFec1.Value)

    conexion.Open()
    command.ExecuteNonQuery()
   Catch ex As Exception
    MessageBox.Show(ex.Message)
    'MsgBox("Estos registros ya existen")
   Finally
    conexion.Dispose()
    command.Dispose()
   End Try
  End If

  If CKAut2.Checked = True Then
   Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
   Dim command As New SqlCommand("SPGuardaSolTODOS", conexion)
   Try
    command.CommandType = CommandType.StoredProcedure

    command.Parameters.AddWithValue("@FechaSol", DTPFec2.Value)

    conexion.Open()
    command.ExecuteNonQuery()
   Catch ex As Exception
    MessageBox.Show(ex.Message)
    'MsgBox("Estos registros ya existen")
   Finally
    conexion.Dispose()
    command.Dispose()
   End Try
  End If

  If CKAut3.Checked = True Then
   Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
   Dim command As New SqlCommand("SPGuardaSolTODOS", conexion)
   Try
    command.CommandType = CommandType.StoredProcedure

    command.Parameters.AddWithValue("@FechaSol", DTPFec3.Value)

    conexion.Open()
    command.ExecuteNonQuery()
   Catch ex As Exception
    MessageBox.Show(ex.Message)
    'MsgBox("Estos registros ya existen")
   Finally
    conexion.Dispose()
    command.Dispose()
   End Try
  End If

  If CKAut4.Checked = True Then
   Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
   Dim command As New SqlCommand("SPGuardaSolTODOS", conexion)
   Try
    command.CommandType = CommandType.StoredProcedure

    command.Parameters.AddWithValue("@FechaSol", DTPFec4.Value)

    conexion.Open()
    command.ExecuteNonQuery()
   Catch ex As Exception
    MessageBox.Show(ex.Message)
    'MsgBox("Estos registros ya existen")
   Finally
    conexion.Dispose()
    command.Dispose()
   End Try
  End If

  If CKAut5.Checked = True Then
   Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
   Dim command As New SqlCommand("SPGuardaSolTODOS", conexion)
   Try
    command.CommandType = CommandType.StoredProcedure

    command.Parameters.AddWithValue("@FechaSol", DTPFec5.Value)

    conexion.Open()
    command.ExecuteNonQuery()
   Catch ex As Exception
    MessageBox.Show(ex.Message)
    'MsgBox("Estos registros ya existen")
   Finally
    conexion.Dispose()
    command.Dispose()
   End Try
  End If




 End Sub

 Private Sub EliminarSolicitudVac()
  '----PROCEDIMIENTO 1
  Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
  Dim command As New SqlCommand("SPEliminaDias", conexion)
  Try
   command.CommandType = CommandType.StoredProcedure
   command.Parameters.AddWithValue("@Folio", TBFolio.Text)
   command.Parameters.AddWithValue("@FechaSol", DTPFecSol.Value)
   command.Parameters.AddWithValue("@NumEmpleado", TBNumEmp.Text)
   command.Parameters.AddWithValue("@FecIniVac", Date.Parse(TBFecIniVac.Text).Date.ToString("yyyyMMdd"))
   command.Parameters.AddWithValue("@FecCadVac", Date.Parse(TBFecCadVac.Text).Date.ToString("yyyyMMdd"))


   conexion.Open()
   command.ExecuteNonQuery()
  Catch ex As Exception
   MessageBox.Show(ex.Message)
   'MsgBox("Estos registros ya existen")
  Finally
   conexion.Dispose()
   command.Dispose()
  End Try

 End Sub

 Private Sub CBDiasSol_Click(sender As Object, e As EventArgs) Handles CBDiasSol.Click
  'If TBNumEmp.Text = "" Then
  '    MessageBox.Show( _
  '              "Ingrese número o nombre de empleado.", _
  '              "Seleccione empleado.", MessageBoxButtons.OK, _
  '              MessageBoxIcon.Exclamation)

  '    TBNumEmp.Focus()

  'End If
 End Sub


 Private Sub EliminarHistorico()

  'If CmbAgteVta.SelectedValue = 999 And Mes <> 99 Then
  '---Inserta objetivos en 0 de todos los AGENTES 
  '----PROCEDIMIENTO
  For i As Integer = 0 To DGVCap.RowCount - 1
   Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
   Dim command As New SqlCommand("SPEliminaDiasAtzado", conexion)
   Try



    command.CommandType = CommandType.StoredProcedure
    command.Parameters.AddWithValue("@Folio", DGVCap.Item(0, i).Value)
    command.Parameters.AddWithValue("@NumEmpleado", DGVCap.Item(1, i).Value)
    command.Parameters.AddWithValue("@Periodo", DGVCap.Item(2, i).Value)
    command.Parameters.AddWithValue("@DiaSol", Date.Parse(DGVCap.Item(3, i).Value).Date.ToString("yyyyMMdd"))

    conexion.Open()
    command.ExecuteNonQuery()

   Catch ex As Exception
    MessageBox.Show(ex.Message)
    'MsgBox("Estos registros ya existen")
   Finally
    conexion.Dispose()
    command.Dispose()
   End Try

  Next
 End Sub

 Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles BtnImprimir.Click

  Dim vSinValor As Integer = 0

  'Try
  Dim DTVacaciones As New DataTable("SolVacaciones")

  DTVacaciones.Columns.Add("IdSolicitud", Type.GetType("System.String"))
  DTVacaciones.Columns.Add("FechSol", Type.GetType("System.String"))
  DTVacaciones.Columns.Add("NumEmp", Type.GetType("System.Int32"))
  DTVacaciones.Columns.Add("NomEmp", Type.GetType("System.String"))
  DTVacaciones.Columns.Add("FecIng", Type.GetType("System.String"))
  DTVacaciones.Columns.Add("DiasVac", Type.GetType("System.Int32"))
  DTVacaciones.Columns.Add("FecIniVac", Type.GetType("System.String"))
  DTVacaciones.Columns.Add("FecCadVac", Type.GetType("System.String"))
  DTVacaciones.Columns.Add("Periodo", Type.GetType("System.String"))
  DTVacaciones.Columns.Add("DiasRest", Type.GetType("System.Int32"))
  DTVacaciones.Columns.Add("DiasSol", Type.GetType("System.Int32"))
  DTVacaciones.Columns.Add("DiasAut", Type.GetType("System.Int32"))
  DTVacaciones.Columns.Add("DiaSol", Type.GetType("System.DateTime"))

  DTVacaciones.Columns.Add("Tomados", Type.GetType("System.Int32"))
  DTVacaciones.Columns.Add("Restantes", Type.GetType("System.Int32"))

  Dim columnas As DataColumnCollection = DTVacaciones.Columns

  Dim series As String = ""
  Dim _filaTemp As DataRow

  'For Each row As DataGridViewRow In Me.DGVCap.Rows
  '    Fila += 1
  '    vTotSIva += row.Cells(5).Value
  '    If row.Cells(5).Value = 0 Then
  '        vSinValor = 1
  '        Exit For
  '    End If

  'Next

  If DGVCap.RowCount < 1 Then
   vSinValor = 1
  End If

  If vSinValor = 1 Then
   MessageBox.Show("No hay días de vacaciones autorizados. Verifique.", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

   'BtnImprimir.Enabled = True
   Return
  End If

  Dim con As Integer

  For Each row As DataGridViewRow In Me.DGVCap.Rows

   'contador = contador + 1
   _filaTemp = DTVacaciones.NewRow()

   _filaTemp(columnas(0)) = TBFolio.Text
   _filaTemp(columnas(1)) = DTPFecSol.Text
   _filaTemp(columnas(2)) = TBNumEmp.Text
   _filaTemp(columnas(3)) = CBNomEmp.Text
   _filaTemp(columnas(4)) = TextBox1.Text
   _filaTemp(columnas(5)) = TBDiasVac.Text
   _filaTemp(columnas(6)) = TBFecIniVac.Text
   _filaTemp(columnas(7)) = TBFecCadVac.Text
   _filaTemp(columnas(8)) = TextBox2.Text
   _filaTemp(columnas(9)) = TBDiasRest.Text
   '_filaTemp(columnas(10)) = CBDiasSol.Text
   _filaTemp(columnas(10)) = 1

   If TBDiasAut.Text = "" Then
    TBDiasAut.Text = CBDiasSol.Text
   End If
   '_filaTemp(columnas(11)) = TBDiasAut.Text
   _filaTemp(columnas(11)) = 1

   _filaTemp(columnas(12)) = row.Cells(3).Value.ToString

   _filaTemp(columnas(13)) = 1                 'tomados
   '_filaTemp(columnas(14)) = row.Cells(4).Value.ToString   'restantes
   _filaTemp(columnas(14)) = 1   'restantes
   '_filaTemp(columnas(29)) = vTotIva.ToString
   '_filaTemp(columnas(10)) = row.Cells(0).Value + row.Cells(0).Value

   DTVacaciones.Rows.Add(_filaTemp)

   con = con + 1

  Next

  Dim informe As New CRSolVacaciones

  RepComsultaP.MdiParent = Inicio
  informe.SetDataSource(DTVacaciones)

  RepComsultaP.CrVConsulta.ReportSource = informe


  RepComsultaP.Show()

  'Catch ex As Exception
  '    'ErrOV = 1

  '    'MsgBox(ex.Message)

  '    MessageBox.Show("No fue posible mostrar la orden de venta. " & ex.Message, "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

  'End Try
 End Sub


 Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
  TBNumEmp.Enabled = False
  CBNomEmp.Enabled = False
  CBDiasSol.Enabled = False
  BSave.Enabled = False
  BtnNvo.Enabled = True

  VerFolioAnterior()

  CKAut1.Enabled = False
  CKAut2.Enabled = False
  CKAut3.Enabled = False
  CKAut4.Enabled = False
  CKAut5.Enabled = False
 End Sub

 Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
  TBNumEmp.Enabled = False
  CBNomEmp.Enabled = False
  CBDiasSol.Enabled = False
  BSave.Enabled = False
  BtnNvo.Enabled = True

  VerFolioSiguiente()
  PanelFechas.Enabled = False

  CKAut1.Enabled = False
  CKAut2.Enabled = False
  CKAut3.Enabled = False
  CKAut4.Enabled = False
  CKAut5.Enabled = False
 End Sub

 Private Sub VerFolioSiguiente()
  LimpiaCampos()

  'MsgBox(TBFolio.Text)
  'MsgBox(FolioMax)

  If TBFolio.Text >= FolioMax Then

   conexion.Open()
   Dim cmd As SqlCommand = New SqlCommand("SELECT folio,FechaSol,NumEmpleado,NomEmpleado,FechaIMSS, " &
                                                  "antiguedad,DiasVac,FecIniVac,FecCadVac,Periodo,DiasRest," &
                                                  "DiasSol,DiasAut FROM SolicitudVacaciones " &
                                                  "where folio = @folio ", conexion)
   cmd.Parameters.AddWithValue("@folio", 1)

   Dim cmd2 As SqlCommand = New SqlCommand("SELECT DiaSol,DiasRest FROM SolVacacionesHistorico " &
                                                   "where folio = @folio ORDER BY DiasRest DESC ", conexion)
   cmd2.Parameters.AddWithValue("@folio", 1)


   Try

    Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
    Dim dt As New DataTable
    da.Fill(dt)

    If dt.Rows.Count > 0 Then

     Dim row As DataRow = dt.Rows(0)

     TBFolio.Text = CStr(row("folio"))

     DTPFecSol.Value = CStr(row("FechaSol"))

     CBNomEmp.Text = ""
     CBNomEmp.SelectedText = CStr(row("NomEmpleado"))

     DTPFecIng.Value = CStr(row("FechaIMSS"))

     TBAntiguedad.Text = CStr(row("antiguedad")) 'Format(CStr(row("antiguedad")), "##.#0")

     TBDiasVac.Text = CStr(row("DiasVac"))

     TBFecIniVac.Text = CStr(row("FecIniVac"))

     TBFecCadVac.Text = CStr(row("FecCadVac"))

     TBPeriodoCom.Text = CStr(row("Periodo"))

     TBDiasRest.Text = CStr(row("DiasRest"))

     CBDiasSol.Text = CStr(row("DiasSol"))

     TBDiasAut.Text = CStr(row("DiasAut"))

     TBNumEmp.Text = CStr(row("NumEmpleado"))
    End If

    '--------------******************************************
    '--------------******************************************

    Dim da2 As SqlDataAdapter = New SqlDataAdapter(cmd2)
    Dim dt2 As New DataTable
    da2.Fill(dt2)

    DGVCap.DataSource = dt2

    If dt2.Rows.Count = 1 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True

    ElseIf dt2.Rows.Count = 2 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True

    ElseIf dt2.Rows.Count = 3 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True

    ElseIf dt2.Rows.Count = 4 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True
     Dim row5 As DataRow = dt2.Rows(3)
     DTPFec4.Value = CStr(row5("DiaSol"))
     CKAut4.Checked = True

    ElseIf dt2.Rows.Count = 5 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True
     Dim row5 As DataRow = dt2.Rows(3)
     DTPFec4.Value = CStr(row5("DiaSol"))
     CKAut4.Checked = True
     Dim row6 As DataRow = dt2.Rows(4)
     DTPFec5.Value = CStr(row6("DiaSol"))
     CKAut5.Checked = True

    End If

    '-----------------------------
    '-----------------------------

   Catch exsql As SqlException
    'MsgBox(exsql.Message)
   Catch ex As Exception
    'MsgBox(ex.Message)
   Finally
    If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
     conexion.Close()
    End If
   End Try

  Else

   conexion.Open()
   Dim cmd As SqlCommand = New SqlCommand("SELECT folio,FechaSol,NumEmpleado,NomEmpleado,FechaIMSS, " &
                                                  "antiguedad,DiasVac,FecIniVac,FecCadVac,Periodo,DiasRest," &
                                                  "DiasSol,DiasAut FROM SolicitudVacaciones " &
                                                  "where folio = @folio ", conexion)
   cmd.Parameters.AddWithValue("@folio", TBFolio.Text + 1)

   Dim cmd2 As SqlCommand = New SqlCommand("SELECT DiaSol,DiasRest FROM SolVacacionesHistorico " &
                                                   "where folio = @folio ORDER BY DiasRest DESC", conexion)
   cmd2.Parameters.AddWithValue("@folio", TBFolio.Text + 1)

   Try

    Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
    Dim dt As New DataTable
    da.Fill(dt)

    If dt.Rows.Count > 0 Then

     Dim row As DataRow = dt.Rows(0)

     TBFolio.Text = CStr(row("folio"))

     DTPFecSol.Value = CStr(row("FechaSol"))

     CBNomEmp.Text = ""
     CBNomEmp.SelectedText = CStr(row("NomEmpleado"))

     DTPFecIng.Value = CStr(row("FechaIMSS"))

     TBAntiguedad.Text = CStr(row("antiguedad")) 'Format(CStr(row("antiguedad")), "##.#0")

     TBDiasVac.Text = CStr(row("DiasVac"))

     TBFecIniVac.Text = CStr(row("FecIniVac"))

     TBFecCadVac.Text = CStr(row("FecCadVac"))

     TBPeriodoCom.Text = CStr(row("Periodo"))

     TBDiasRest.Text = CStr(row("DiasRest"))

     CBDiasSol.Text = CStr(row("DiasSol"))

     TBDiasAut.Text = CStr(row("DiasAut"))

     TBNumEmp.Text = CStr(row("NumEmpleado"))
    End If

    '--------------******************************************
    '--------------******************************************

    Dim da2 As SqlDataAdapter = New SqlDataAdapter(cmd2)
    Dim dt2 As New DataTable
    da2.Fill(dt2)

    DGVCap.DataSource = dt2

    If dt2.Rows.Count = 1 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True

    ElseIf dt2.Rows.Count = 2 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True

    ElseIf dt2.Rows.Count = 3 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True

    ElseIf dt2.Rows.Count = 4 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True
     Dim row5 As DataRow = dt2.Rows(3)
     DTPFec4.Value = CStr(row5("DiaSol"))
     CKAut4.Checked = True

    ElseIf dt2.Rows.Count = 5 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True
     Dim row5 As DataRow = dt2.Rows(3)
     DTPFec4.Value = CStr(row5("DiaSol"))
     CKAut4.Checked = True
     Dim row6 As DataRow = dt2.Rows(4)
     DTPFec5.Value = CStr(row6("DiaSol"))
     CKAut5.Checked = True

    End If

    '-----------------------------
    '-----------------------------

   Catch exsql As SqlException
    'MsgBox(exsql.Message)
   Catch ex As Exception
    'MsgBox(ex.Message)
   Finally
    If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
     conexion.Close()
    End If
   End Try

  End If

 End Sub

 Private Sub VerFolioAnterior()
  LimpiaCampos()

  'MsgBox(TBFolio.Text)
  'MsgBox(FolioMax)

  If TBFolio.Text = 1 Then

   conexion.Open()
   Dim cmd As SqlCommand = New SqlCommand("SELECT folio,FechaSol,NumEmpleado,NomEmpleado,FechaIMSS, " &
                                                  "antiguedad,DiasVac,FecIniVac,FecCadVac,Periodo,DiasRest," &
                                                  "DiasSol,DiasAut FROM SolicitudVacaciones " &
                                                  "where folio = @folio ", conexion)
   cmd.Parameters.AddWithValue("@folio", FolioMax)

   Dim cmd2 As SqlCommand = New SqlCommand("SELECT DiaSol,DiasRest FROM SolVacacionesHistorico " &
                                                   "where folio = @folio ORDER BY DiasRest DESC ", conexion)
   cmd2.Parameters.AddWithValue("@folio", FolioMax)

   Try
    '-----------------------------
    '-----------------------------
    Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
    Dim dt As New DataTable
    da.Fill(dt)

    DGVCap.DataSource = dt

    If dt.Rows.Count > 0 Then

     Dim row As DataRow = dt.Rows(0)

     TBFolio.Text = CStr(row("folio"))

     DTPFecSol.Value = CStr(row("FechaSol"))

     CBNomEmp.Text = ""
     CBNomEmp.SelectedText = CStr(row("NomEmpleado"))

     DTPFecIng.Value = CStr(row("FechaIMSS"))

     TBAntiguedad.Text = CStr(row("antiguedad")) 'Format(CStr(row("antiguedad")), "##.#0")

     TBDiasVac.Text = CStr(row("DiasVac"))

     TBFecIniVac.Text = CStr(row("FecIniVac"))

     TBFecCadVac.Text = CStr(row("FecCadVac"))

     TBPeriodoCom.Text = CStr(row("Periodo"))

     TBDiasRest.Text = CStr(row("DiasRest"))

     CBDiasSol.Text = CStr(row("DiasSol"))

     TBDiasAut.Text = CStr(row("DiasAut"))

     TBNumEmp.Text = CStr(row("NumEmpleado"))

    End If

    '-----------------------------
    '-----------------------------
    Dim da2 As SqlDataAdapter = New SqlDataAdapter(cmd2)
    Dim dt2 As New DataTable
    da2.Fill(dt2)

    DGVCap.DataSource = dt2

    If dt2.Rows.Count = 1 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True

    ElseIf dt2.Rows.Count = 2 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True

    ElseIf dt2.Rows.Count = 3 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True

    ElseIf dt2.Rows.Count = 4 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True
     Dim row5 As DataRow = dt2.Rows(3)
     DTPFec4.Value = CStr(row5("DiaSol"))
     CKAut4.Checked = True

    ElseIf dt2.Rows.Count = 5 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True
     Dim row5 As DataRow = dt2.Rows(3)
     DTPFec4.Value = CStr(row5("DiaSol"))
     CKAut4.Checked = True
     Dim row6 As DataRow = dt2.Rows(4)
     DTPFec5.Value = CStr(row6("DiaSol"))
     CKAut5.Checked = True

    End If

    '-----------------------------
    '-----------------------------

   Catch exsql As SqlException
    'MsgBox(exsql.Message)
   Catch ex As Exception
    'MsgBox(ex.Message)
   Finally
    If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
     conexion.Close()
    End If
   End Try

  Else

   conexion.Open()
   Dim cmd As SqlCommand = New SqlCommand("SELECT folio,FechaSol,NumEmpleado,NomEmpleado,FechaIMSS, " &
                                                  "antiguedad,DiasVac,FecIniVac,FecCadVac,Periodo,DiasRest," &
                                                  "DiasSol,DiasAut FROM SolicitudVacaciones " &
                                                  "where folio = @folio ", conexion)
   cmd.Parameters.AddWithValue("@folio", TBFolio.Text - 1)

   Dim cmd2 As SqlCommand = New SqlCommand("SELECT DiaSol,DiasRest " &
                                                   "FROM SolVacacionesHistorico where folio = @folio ORDER BY DiasRest DESC", conexion)
   cmd2.Parameters.AddWithValue("@folio", TBFolio.Text - 1)

   Try

    Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
    Dim dt As New DataTable
    da.Fill(dt)

    If dt.Rows.Count > 0 Then

     Dim row As DataRow = dt.Rows(0)

     TBFolio.Text = CStr(row("folio"))

     DTPFecSol.Value = CStr(row("FechaSol"))

     TBNumEmp.Text = CStr(row("NumEmpleado"))

     CBNomEmp.Text = ""
     CBNomEmp.SelectedText = CStr(row("NomEmpleado"))

     DTPFecIng.Value = CStr(row("FechaIMSS"))

     TBAntiguedad.Text = CStr(row("antiguedad")) 'Format(CStr(row("antiguedad")), "##.#0")

     TBDiasVac.Text = CStr(row("DiasVac"))

     TBFecIniVac.Text = CStr(row("FecIniVac"))

     TBFecCadVac.Text = CStr(row("FecCadVac"))

     TBPeriodoCom.Text = CStr(row("Periodo"))

     TBDiasRest.Text = CStr(row("DiasRest"))

     CBDiasSol.Text = CStr(row("DiasSol"))

     TBDiasAut.Text = CStr(row("DiasAut"))

     TBNumEmp.Text = CStr(row("NumEmpleado"))
     '--------------******************************************

    End If

    '-----------------------------
    '-----------------------------
    Dim da2 As SqlDataAdapter = New SqlDataAdapter(cmd2)
    Dim dt2 As New DataTable
    da2.Fill(dt2)

    DGVCap.DataSource = dt2

    If dt2.Rows.Count = 1 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True

    ElseIf dt2.Rows.Count = 2 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True

    ElseIf dt2.Rows.Count = 3 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True

    ElseIf dt2.Rows.Count = 4 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True
     Dim row5 As DataRow = dt2.Rows(3)
     DTPFec4.Value = CStr(row5("DiaSol"))
     CKAut4.Checked = True

    ElseIf dt2.Rows.Count = 5 Then
     Dim row2 As DataRow = dt2.Rows(0)
     DTPFec1.Value = CStr(row2("DiaSol"))
     CKAut1.Checked = True
     Dim row3 As DataRow = dt2.Rows(1)
     DTPFec2.Value = CStr(row3("DiaSol"))
     CKAut2.Checked = True
     Dim row4 As DataRow = dt2.Rows(2)
     DTPFec3.Value = CStr(row4("DiaSol"))
     CKAut3.Checked = True
     Dim row5 As DataRow = dt2.Rows(3)
     DTPFec4.Value = CStr(row5("DiaSol"))
     CKAut4.Checked = True
     Dim row6 As DataRow = dt2.Rows(4)
     DTPFec5.Value = CStr(row6("DiaSol"))
     CKAut5.Checked = True

    End If

    '-----------------------------
    '-----------------------------


   Catch exsql As SqlException
    'MsgBox(exsql.Message)
   Catch ex As Exception
    'MsgBox(ex.Message)
   Finally
    If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
     conexion.Close()
    End If
   End Try

  End If

 End Sub

 Private Sub BtnNvo_Click(sender As Object, e As EventArgs) Handles BtnNvo.Click

  Me.Close()

  Dim frm As New FormatoVacaciones
  'frm.Show()

  frm.MdiParent = Inicio

  frm.Show()

  'Dim frm As New FormatoVacaciones
  'frm.Show()

  'BSave.Enabled = True

  'Me.Refresh()

  ''Try
  ''    ''MsgBox(DGVCap.RowCount)
  ''    'For i As Integer = 0 To DGVCap.RowCount - 1
  ''    '    'DGVCap.Rows.RemoveAt(i)
  ''    '    Me.DGVCap.Rows.RemoveAt(i)
  ''    'Next

  ''    For i As Integer = 0 To DGVCap.RowCount - 1
  ''        'MsgBox(DTPFec1.Text)
  ''        'MsgBox(DGVCap.Item(3, i).Value)
  ''        'Dim auxfec1 As String = DTPFec1.Text.ToString

  ''        DGVCap.CurrentCell = DGVCap.Rows(0).Cells(0)

  ''        DGVCap.Rows(0).Selected = True

  ''        Me.DGVCap.Rows.Remove(DGVCap.CurrentRow)

  ''    Next
  ''    'DGVCap.DataSource = DBNull.Value
  ''    DGVCap.Refresh()

  ''    'Me.DGVCap.EndEdit()

  ''Catch ex As Exception
  ''    MsgBox(ex.Message)
  ''End Try

  ''TBNumEmp.Enabled = True
  ''CBNomEmp.Enabled = True
  ''CBNomEmp.SelectedValue = 99

  ''CBDiasSol.Enabled = True

  ' ''Procedimiento para obtener el número de transacción más actual
  ''Dim cmdCuenta As New Data.SqlClient.SqlCommand
  ''Dim FormatWO As String = ""
  ''cmdCuenta.CommandText = "SELECT MAX(Folio) FROM SolicitudVacaciones "
  ''cmdCuenta.CommandType = CommandType.Text
  ''cmdCuenta.Connection = New Data.SqlClient.SqlConnection(StrTpm)
  ''cmdCuenta.Connection.Open()
  ' ''NumOVta = IIf(IsDBNull(cmdCuenta.ExecuteScalar()), 0, Val(cmdCuenta.ExecuteScalar()))

  ''With cmdCuenta
  ''    NumOVta = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
  ''    .Connection.Close()
  ''End With

  ''FolioMax = NumOVta

  ''If FolioMax = 0 Then
  ''    FolioMax = 1
  ''End If

  ''NumOVta += 1

  ''TBFolio.Text = NumOVta
  ''TBFolio.Text = Format(NumOVta, "0000")
  ''TBFolio.TextAlign = HorizontalAlignment.Right

  ' ''DisenoGridVArt()

  ''CBDiasSol.SelectedIndex = 0

  ''DTPFec1.Value = Date.Now
  ''DTPFec2.Value = Date.Now
  ''DTPFec3.Value = Date.Now
  ''DTPFec4.Value = Date.Now
  ''DTPFec5.Value = Date.Now

  CBNomEmp.Focus()

  CKAut1.Enabled = True
  CKAut2.Enabled = True
  CKAut3.Enabled = True
  CKAut4.Enabled = True
  CKAut5.Enabled = True
 End Sub



 Private Sub TBPeriodoCom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TBPeriodoCom.SelectedIndexChanged
  'MsgBox("seleccionaste")
  ' cargar los datos  
  'cargar_registros("SELECT ''  AS 'NumEmpleado',0 'Periodo',NULL 'DiaSol',NULL 'DiasTomados'," & TBDiasVac.Text & _
  '    " 'DiasRest' UNION ALL SELECT NumEmpleado,Periodo,DiaSol,1,0 FROM SolVacacionesHistorico WHERE NumEmpleado='" & _
  '                 TBNumEmp.Text & "' AND Periodo=" & TBPeriodoCom.SelectedValue.ToString & " ORDER BY NumEmpleado,DiaSol DESC", DGVacaciones)



  ''Disenogrid()

  ''contar dias de vacaciones
  'Dim diasRestantes As Integer

  'diasRestantes = TBDiasVac.Text - DGVacaciones.RowCount + 2

  'TBDiasRest.Text = diasRestantes


  'If DGVacaciones.RowCount > 1 Then
  '    For i = 1 To DGVacaciones.RowCount - 2
  '        DGVacaciones.Item(4, i).Value = TBDiasVac.Text - i
  '    Next
  'End If
 End Sub

 Private Sub BtnEmpleados_Click(sender As Object, e As EventArgs) Handles BtnEmpleados.Click

 End Sub

 Private Sub TBPeriodoCom_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles TBPeriodoCom.SelectionChangeCommitted
  'MsgBox("periodo")
  'Dim diasRestantes As Integer
  Dim bandera_entero As Integer = 123
  ''MsgBox(FechaIngreso)
  Dim SumaAnios As Integer
  SumaAnios = 0
  If (TopeGlobal = TBPeriodoCom.SelectedValue) Then
   'MsgBox("estas seleccionando el ultimo")


   SumaAnios = Integer.Parse(TBPeriodoCom.SelectedValue.ToString) - DatePart("yyyy", FechaIngreso)
   Antiguedad = DateDiff("d", FechaIngreso, Date.Now()) / 365
   'Antiguedad = DateDiff("d", CStr(row("FechaIMSS")), Date.Now) / 365
   TBAntiguedad.Text = Format(Antiguedad, "##.#0")
   If Antiguedad < 1 Then
    NumDiasVac = 0
   ElseIf Antiguedad >= 1 And Antiguedad < 2 Then
    NumDiasVac = 3
   ElseIf Antiguedad >= 2 And Antiguedad < 3 Then
    NumDiasVac = 4
   ElseIf Antiguedad >= 3 And Antiguedad < 4 Then
    NumDiasVac = 4
   ElseIf Antiguedad >= 4 And Antiguedad < 5 Then
    NumDiasVac = 5
   ElseIf Antiguedad >= 5 And Antiguedad < 10 Then
    NumDiasVac = 6
   ElseIf Antiguedad >= 10 Then
    NumDiasVac = 7
   End If

   TBDiasVac.Text = NumDiasVac
   TBFecIniVac.Text = Me.DTPFecIng.Value.Date.AddYears(SumaAnios)
   TBFecCadVac.Text = Me.DTPFecIng.Value.Date.AddYears(SumaAnios + 1)

   'Dim diasRestantes As Integer

   'diasRestantes = TBDiasVac.Text - DGVacaciones.RowCount + 2

   'TBDiasRest.Text = diasRestantes
  Else
   'MsgBox(Integer.Parse(TBPeriodoCom.SelectedValue.ToString) & "-" & DatePart("yyyy", FechaIngreso))
   SumaAnios = Integer.Parse(TBPeriodoCom.SelectedValue.ToString) - DatePart("yyyy", FechaIngreso)
   'MsgBox(SumaAnios)

   Antiguedad = DateDiff("d", FechaIngreso, Me.DTPFecIng.Value.Date.AddYears(SumaAnios)) / 365

   'MsgBox(Antiguedad)
   TBAntiguedad.Text = Format(Antiguedad, "##.#0")
   If Antiguedad < 1 Then
    NumDiasVac = 0
   ElseIf Antiguedad >= 1 And Antiguedad < 2 Then
    NumDiasVac = 6
   ElseIf Antiguedad >= 2 And Antiguedad < 3 Then
    NumDiasVac = 8
   ElseIf Antiguedad >= 3 And Antiguedad < 4 Then
    NumDiasVac = 10
   ElseIf Antiguedad >= 4 And Antiguedad < 5 Then
    NumDiasVac = 12
   ElseIf Antiguedad >= 5 And Antiguedad < 10 Then
    NumDiasVac = 14
   ElseIf Antiguedad >= 10 Then
    NumDiasVac = 16
   End If

   TBDiasVac.Text = NumDiasVac
   TBFecIniVac.Text = Me.DTPFecIng.Value.Date.AddYears(SumaAnios)
   TBFecCadVac.Text = Me.DTPFecIng.Value.Date.AddYears(SumaAnios + 1)



   'Dim diasRestantes As Integer

   'diasRestantes = TBDiasVac.Text - DGVacaciones.RowCount + 2

   'TBDiasRest.Text = diasRestantes

  End If

  For indice2 As Integer = 0 To dt.Tables(1).Rows.Count - 1
   If (CStr(dt.Tables(1).Rows(indice2)(0)) = TBPeriodoCom.SelectedValue.ToString) Then
    bandera_entero = Integer.Parse(dt.Tables(1).Rows(indice2)(1))
   End If

  Next

  If (bandera_entero = 123) Then
   TBDiasRest.Text = TBDiasVac.Text
  Else
   TBDiasRest.Text = Integer.Parse(TBDiasVac.Text.ToString) - bandera_entero
  End If


 End Sub

 Private Sub CBNomEmp_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBNomEmp.SelectionChangeCommitted
  Rutina()
 End Sub

 Private Sub CBNomEmp_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CBNomEmp.Validating
  'If (CBNomEmp.Text.ToString <> "") Then
  '    If (CBNomEmp.FindString(CBNomEmp.Text.ToString).ToString <> "-1") Then
  '        CBNomEmp.SelectedIndex = CBNomEmp.FindString(CBNomEmp.Text.ToString)
  '        Rutina()

  '    Else
  '        TBPeriodoCom.DataSource = Nothing
  '        TBPeriodoCom.ValueMember = Nothing
  '        TBPeriodoCom.DisplayMember = Nothing
  '        LimpiaCampos()
  '        Return

  '    End If

  'End If
 End Sub

 Public Sub Rutina()
  'MsgBox("entre")
  Dim tope As Integer
  Dim consulta As String = ""
  'consulta &= "SELECT NumEmp AS 'NumEmp', FechaIMSS,  case when (DATEADD(YEAR, DATEDIFF(YEAR, FechaIMSS, GETDATE()), FechaIMSS )) <= GETDATE() then YEAR(GETDATE()) "
  'CAMBIO: SE AGREGA EL CAMPO TIPO PARA PODER HACER OPERACIONES ABAJO
  consulta &= "SELECT NumEmp AS 'NumEmp', Tipo, FechaIMSS,  case when (DATEADD(YEAR, DATEDIFF(YEAR, FechaIMSS, GETDATE()), FechaIMSS )) <= GETDATE() then YEAR(GETDATE()) "
  consulta &= "else ( YEAR(GETDATE()) - 1) end as 'Tope' FROM Empleados  where NumEmp = @NumEmp "
  consulta &= "update Empleados set Antiguedad = DATEDIFF(day,FechaIMSS, GETDATE())/365   "
  consulta &= "update Empleados set DiasVacaCorres =  "
  consulta &= "case when Antiguedad = 0 then 0  "
  consulta &= "when Antiguedad = 1 then 6   "
  consulta &= "when Antiguedad = 2 then 8  "
  consulta &= "when Antiguedad = 3 then 10  "
  consulta &= "when Antiguedad = 4 then 12  "
  consulta &= "when Antiguedad >= 5 and Antiguedad <= 9 then 14  "
  consulta &= "when Antiguedad >= 10 and Antiguedad <= 14 then 16  "
  consulta &= "when Antiguedad >= 15 and Antiguedad <= 19 then 18  "
  consulta &= "when Antiguedad >= 20 and Antiguedad <= 24 then 20  "
  consulta &= "when Antiguedad >= 25 and Antiguedad <= 29 then 22  "
  consulta &= "when Antiguedad >= 30 and Antiguedad <= 34 then 24  "
  consulta &= "when Antiguedad >= 35 and Antiguedad <= 39 then 26  "
  consulta &= "end  "
  'consulta &= "Create Table #relacion (NumEmp int, NomEmp varchar(50), FechaIng date, Periodo varchar(20), FechaInicioPeriodo date, FechaFinPeriodo date, AntiguedadParaPeriodo int, PeriodoKey varchar(20))   "
  'CAMBIO: SE AGRAG EL CAMPO TIPO PARA CALCULAR LOS DATOS DE VACACIONES
  consulta &= "Create Table #relacion (NumEmp int, tipo varchar(2), NomEmp varchar(50), FechaIng date, Periodo varchar(20), FechaInicioPeriodo date, FechaFinPeriodo date, AntiguedadParaPeriodo int, PeriodoKey varchar(20))   "
  consulta &= "DECLARE @indice INT   "
  consulta &= "DECLARE @fin_indice INT   "
  consulta &= "if((select DATEADD(YEAR, DATEDIFF(YEAR, FechaIMSS, GETDATE()), FechaIMSS )  from Empleados where NumEmp =   @NumEmp   ) <= GETDATE() )   "
  consulta &= "set @fin_indice = YEAR(GETDATE());   "
  consulta &= "else   "
  consulta &= "set @fin_indice = (YEAR(GETDATE())) - 1;   "
  consulta &= "set @indice = (select YEAR(FechaIMSS) from Empleados where NumEmp =   @NumEmp   )   "
  consulta &= "while (@indice <= @fin_indice)   "
  consulta &= "begin   "
  'consulta &= "insert into #relacion(NumEmp, NomEmp, FechaIng, Periodo, FechaInicioPeriodo, FechaFinPeriodo, AntiguedadParaPeriodo, PeriodoKey)   "
  'CAMBIO: SE AGRAGA LA COLUMNA TIPO PARA CALCULAR LOS DATOS DE EMPLEADOS DE 3 DIAS
  consulta &= "insert into #relacion(NumEmp, Tipo, NomEmp, FechaIng, Periodo, FechaInicioPeriodo, FechaFinPeriodo, AntiguedadParaPeriodo, PeriodoKey)   "

  consulta &= "values ((select NumEmp from Empleados where NumEmp =    @NumEmp    ),   "
  'CAMBIO: AGREGA EL TIPO DE EMPLEADO PARA SABER SACAR LOS DATOS DE VACACIONES
  consulta &= "(select Tipo from Empleados where NumEmp =    @NumEmp    ),   "

  consulta &= "((select NomEmp + ' ' + AppEmp + ' ' + ApmMat from Empleados where NumEmp =   @NumEmp    )),   "
  consulta &= "(select FechaIMSS from Empleados where NumEmp =   @NumEmp  ),  "
  consulta &= "CAST(@indice as varchar) + ' - ' + CAST((@indice + 1) as varchar),   "
  consulta &= "(select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)), FechaIMSS ) from Empleados where NumEmp =   @NumEmp    ),  "
  consulta &= "(select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)) + 1, FechaIMSS ) from Empleados where NumEmp =   @NumEmp    ),  "
  consulta &= "case when (select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)), FechaIMSS ) from Empleados where NumEmp =   @NumEmp  ) > GETDATE()   "
  consulta &= "then (select (DATEDIFF(DAY, (select FechaIMSS from Empleados where NumEmp =   @NumEmp  ), (select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)), FechaIMSS ) from Empleados where NumEmp =   @NumEmp  ) )/365) - 1)   "
  consulta &= "when (select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)), FechaIMSS ) from Empleados where NumEmp =   @NumEmp  ) <= GETDATE()   "
  consulta &= "then (select (DATEDIFF(DAY, (select FechaIMSS from Empleados where NumEmp =   @NumEmp  ), (select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)), FechaIMSS ) from Empleados where NumEmp =   @NumEmp  ) )/365))  "
  consulta &= "end, @indice )  "
  consulta &= "set @indice = @indice + 1   "
  consulta &= "end  "
  consulta &= "select *,    "
  consulta &= "case when AntiguedadParaPeriodo = 0 then 0   "
  consulta &= "when AntiguedadParaPeriodo = 1 then 6    "
  consulta &= "when AntiguedadParaPeriodo = 2 then 8   "
  consulta &= "when AntiguedadParaPeriodo = 3 then 10   "
  consulta &= "when AntiguedadParaPeriodo = 4 then 12   "
  consulta &= "when AntiguedadParaPeriodo >= 5 and AntiguedadParaPeriodo <= 9 then 14   "
  consulta &= "when AntiguedadParaPeriodo >= 10 and AntiguedadParaPeriodo <= 14 then 16   "
  consulta &= "when AntiguedadParaPeriodo >= 15 and AntiguedadParaPeriodo <= 19 then 18   "
  consulta &= "when AntiguedadParaPeriodo >= 20 and AntiguedadParaPeriodo <= 24 then 20   "
  consulta &= "when AntiguedadParaPeriodo >= 25 and AntiguedadParaPeriodo <= 29 then 22   "
  consulta &= "when AntiguedadParaPeriodo >= 30 and AntiguedadParaPeriodo <= 34 then 24   "
  consulta &= "when AntiguedadParaPeriodo >= 35 and AntiguedadParaPeriodo <= 39 then 26   "
  consulta &= "end as 'DiasVacaParaPeriodo',   "

  consulta &= "(select COUNT(*) from SolVacacionesHistorico t0 where t0.NumEmpleado =   @NumEmp   and t0.Periodo = PeriodoKey) as 'DiasTomados',  "
  consulta &= "0 as 'DiasRestantes'  "

  consulta &= "into #relacion2   "
  consulta &= "from #relacion   "
  consulta &= "DECLARE @indice2 INT   "
  consulta &= "DECLARE @fin_indice2 INT   "
  consulta &= "if((select DATEADD(YEAR, DATEDIFF(YEAR, FechaIMSS, GETDATE()), FechaIMSS )  from Empleados where NumEmp =     @NumEmp    ) <= GETDATE() )   "
  consulta &= "set @fin_indice2 = YEAR(GETDATE());   "
  consulta &= "else   "
  consulta &= "set @fin_indice2 = (YEAR(GETDATE())) - 1;   "
  consulta &= "set @indice2 = (select YEAR(FechaIMSS) from Empleados where NumEmp =   @NumEmp   )   "
  consulta &= "while (@indice2 <= @fin_indice2)   "
  consulta &= "begin  "
  consulta &= "if @indice2 = (select YEAR(FechaIMSS) from Empleados where NumEmp =   @NumEmp   )   "
  consulta &= "update #relacion2 set DiasRestantes = DiasVacaParaPeriodo - DiasTomados where PeriodoKey = @indice2;  "
  consulta &= "else   "
  consulta &= "if (select r0.DiasRestantes from #relacion2 r0 where r0.PeriodoKey = @indice2 - 1) < 0  "
  consulta &= "update #relacion2 set DiasRestantes = DiasVacaParaPeriodo - DiasTomados + (select r0.DiasRestantes from #relacion2 r0 where r0.PeriodoKey = @indice2 - 1) where PeriodoKey = @indice2;  "
  consulta &= "else  "
  consulta &= "update #relacion2 set DiasRestantes = DiasVacaParaPeriodo - DiasTomados where PeriodoKey = @indice2;  "
  consulta &= "set @indice2 = @indice2 + 1   "
  consulta &= "end  "
  consulta &= "select *, (DiasRestantes * -1) + (DiasVacaParaPeriodo - DiasTomados) as 'DiasXAdelantado'   "
  consulta &= "into #Header  "
  consulta &= "from #relacion2 "

  consulta &= "select PeriodoKey as 'Periodo', (DiasTomados + DiasXAdelantado) as 'DiasTomados'    "
  consulta &= "from #Header "

  consulta &= "drop table #Header "
  consulta &= "drop table #relacion "
  consulta &= "drop table #relacion2 "



  Try
   LimpiaCampos()
   dt = New DataSet
   conexion.Open()


   'Dim cmd As SqlCommand = New SqlCommand("SELECT NumEmp AS 'NumEmp',FechaIMSS, " & _
   '                                       "case when (DATEADD(YEAR, DATEDIFF(YEAR, FechaIMSS, GETDATE()), FechaIMSS )) <= GETDATE() then YEAR(GETDATE()) " & _
   '                                       "else ( YEAR(GETDATE()) - 1) end as 'Tope' " & _
   '                                       "FROM Empleados " & _
   '"where NumEmp = @NumEmp  select Periodo, COUNT(DiaSol) as 'DiasTomados' from SolVacacionesHistorico where NumEmpleado = @NumEmp group by Periodo", conexion)


   Dim cmd As SqlCommand = New SqlCommand(consulta, conexion)

   cmd.Parameters.AddWithValue("@NumEmp", CBNomEmp.SelectedValue)

   Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
   da.Fill(dt)

   If dt.Tables(0).Rows.Count > 0 Then

    Dim row As DataRow = dt.Tables(0).Rows(0)
    TBNumEmp.Text = CStr(row("NumEmp"))
    DTPFecIng.Value = CStr(row("FechaIMSS"))
    TextBox1.Text = CStr(row("FechaIMSS"))
    FechaIngreso = CStr(row("FechaIMSS"))
    Antiguedad = DateDiff("d", CStr(row("FechaIMSS")), Date.Now) / 365
    TBAntiguedad.Text = Format(Antiguedad, "##.#0")
    txttipo.Text = CStr(row("Tipo"))

    'VALIDA QUE SEA DE TIPO INTENDENCIA
    If txttipo.Text = "I" Then
     'MsgBox("El tipo es: " & txttipo.Text)
     If Antiguedad < 1 Then
      NumDiasVac = 0
     ElseIf Antiguedad >= 1 And Antiguedad < 2 Then
      NumDiasVac = 3
     ElseIf Antiguedad >= 2 And Antiguedad < 3 Then
      NumDiasVac = 4
     ElseIf Antiguedad >= 3 And Antiguedad < 4 Then
      NumDiasVac = 4
     ElseIf Antiguedad >= 4 And Antiguedad < 5 Then
      NumDiasVac = 5
     ElseIf Antiguedad >= 5 And Antiguedad < 10 Then
      NumDiasVac = 6
     ElseIf Antiguedad >= 10 Then
      NumDiasVac = 7
     End If
     'ELSE VALIDA QUE SEA DE TIPO INTENDENCIA
    Else
     If Antiguedad < 1 Then
      NumDiasVac = 0
     ElseIf Antiguedad >= 1 And Antiguedad < 2 Then
      NumDiasVac = 6
     ElseIf Antiguedad >= 2 And Antiguedad < 3 Then
      NumDiasVac = 8
     ElseIf Antiguedad >= 3 And Antiguedad < 4 Then
      NumDiasVac = 10
     ElseIf Antiguedad >= 4 And Antiguedad < 5 Then
      NumDiasVac = 12
     ElseIf Antiguedad >= 5 And Antiguedad < 10 Then
      NumDiasVac = 14
     ElseIf Antiguedad >= 10 Then
      NumDiasVac = 16
     End If
    End If

    tope = Integer.Parse(row("Tope"))

    TBDiasVac.Text = NumDiasVac

    'If DatePart("m", Date.Now) < DatePart("m", DTPFecIng.Value) Then
    '    AñosTrascurridos = DatePart("yyyy", Date.Now) - DatePart("yyyy", DTPFecIng.Value) - 1

    'ElseIf DatePart("m", Date.Now) = DatePart("m", DTPFecIng.Value) Then
    '    If DatePart("d", Date.Now) < DatePart("d", DTPFecIng.Value) Then
    '        AñosTrascurridos = DatePart("yyyy", Date.Now) - DatePart("yyyy", DTPFecIng.Value) - 1
    '    Else
    '        AñosTrascurridos = DatePart("yyyy", Date.Now) - DatePart("yyyy", DTPFecIng.Value)
    '    End If

    'Else
    '    AñosTrascurridos = DatePart("yyyy", Date.Now) - DatePart("yyyy", DTPFecIng.Value)
    'End If


    'If AñosTrascurridos = 0 Then
    '    AñosTrascurridos = 1
    'End If

    'Dim FecImaginaria As Date = Me.DTPFecIng.Value.Date.AddYears(AñosTrascurridos)

    AñosTrascurridos = tope - DTPFecIng.Value.Year
    'MsgBox(AñosTrascurridos)



    TBFecIniVac.Text = Me.DTPFecIng.Value.Date.AddYears(AñosTrascurridos)
    TBFecCadVac.Text = Me.DTPFecIng.Value.Date.AddYears(AñosTrascurridos + 1)

    Dim dt2 As DataTable
    Dim dr2 As DataRow
    Dim currentyear As Integer

    Dim bandera_entero As Integer = 123
    'Dim diasRestantes As Integer
    dt2 = New DataTable("Tabla")
    dt2.Columns.Add("Codigo")
    dt2.Columns.Add("Descripcion")
    currentyear = Integer.Parse(DatePart("yyyy", DTPFecIng.Value))
    'If (Antiguedad < 1) Then
    '    tope = Integer.Parse(row("Tope")) + 1
    'Else
    '    tope = Integer.Parse(row("Tope"))
    'End If


    'MsgBox("TOPE " & tope)
    TextBox2.Text = tope.ToString & " - " & (tope + 1).ToString
    TopeGlobal = Integer.Parse(row("Tope"))
    For y As Integer = currentyear To tope Step 1
     dr2 = dt2.NewRow()
     dr2("Codigo") = y
     dr2("Descripcion") = y & " - " & y + 1
     dt2.Rows.Add(dr2)
    Next

    TBPeriodoCom.DataSource = dt2
    TBPeriodoCom.ValueMember = "Codigo"
    TBPeriodoCom.DisplayMember = "Descripcion"
    TBPeriodoCom.SelectedValue = TopeGlobal.ToString
    TBDiasRest.Text = "0"
    For indice2 As Integer = 0 To dt.Tables(1).Rows.Count - 1
     If (CStr(dt.Tables(1).Rows(indice2)(0)) = TBPeriodoCom.SelectedValue.ToString) Then
      bandera_entero = Integer.Parse(dt.Tables(1).Rows(indice2)(1))
     End If

    Next

    If (bandera_entero = 123) Then
     TBDiasRest.Text = TBDiasVac.Text
    Else
     TBDiasRest.Text = Integer.Parse(TBDiasVac.Text.ToString) - bandera_entero
    End If
    'DTPFec1.Focus()

   Else
    'MsgBox("No hay Empleado")
    'LimpiaCampos()
   End If
   conexion.Close()

  Catch exsql As SqlException
   'MsgBox("eror sql: " & exsql.Message)
  Catch ex As Exception
   'MsgBox("exception: " & ex.Message)
  Finally
   If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
    conexion.Close()
   End If
  End Try

 End Sub


 Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
  'MsgBox(TBDiasRest.Text.ToString)
  If CBNomEmp.Text <> "" Then

   'If TBDiasAut.Text = "" Then
   '    TBDiasAut.Text = 0
   'End If

   'If CKAut1.Checked = True Then

   'If DGVacaciones.RowCount > 1 Then

   '    'MsgBox(DTPFec1.Text)

   '    For i As Integer = 1 To DGVacaciones.RowCount - 1
   '        'MsgBox(DGVArt.Item(0, DGVArt.CurrentCell.RowIndex).Value & " = " & DGVCap.Item(1, i).Value)
   '        If DTPFec1.Text = DGVacaciones.Item(2, i).Value Then
   '            'MsgBox("El artículo ya ha sido agregado.")

   '            MessageBox.Show("Este día ya ha sido asignado anteriormente, " & _
   '                            "seleccione otro para continuar.", "Error al agregar.",
   '            MessageBoxButtons.OK, MessageBoxIcon.Information)

   '            CKAut1.Checked = False
   '            Return
   '        End If
   '    Next

   'End If
   'MsgBox(DGVCap.RowCount)
   If DGVCap.RowCount >= 1 Then

    'MsgBox(DTPFec1.Text)

    For i As Integer = 0 To DGVCap.RowCount - 1
     'MsgBox(DGVArt.Item(0, DGVArt.CurrentCell.RowIndex).Value & " = " & DGVCap.Item(1, i).Value)
     If DTPFec1.Text = DGVCap.Item(3, i).Value Then
      'MsgBox("El artículo ya ha sido agregado.")

      MessageBox.Show("Este día ya ha sido asignado anteriormente, " &
                                       "seleccione otro para continuar.", "Error al agregar.",
                       MessageBoxButtons.OK, MessageBoxIcon.Information)

      'CKAut1.Checked = False
      Return
     End If
    Next

   End If

   If (MessageBox.Show(
                     "¿Confirma autorización del día " & DTPFec1.Text & "? ",
                     "Proceso de autorización.", MessageBoxButtons.YesNo,
                     MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

    DiaVac = 1

    'GenerarDiasVac()

    'NumAuto = TBDiasAut.Text + 1

    Try
     TBDiasRest.Text = TBDiasRest.Text - 1
     DGVCap.Rows.Add(TBFolio.Text, TBNumEmp.Text, TextBox2.Text, DTPFec1.Text.ToString, "Borrar")
    Catch ex As Exception
     'MsgBox(ex.Message)
    End Try

   Else
    CKAut1.Checked = False
   End If

   'Else 'si checked = false
   '    If (MessageBox.Show( _
   '             "¿Confirma que desea quitar autorización del día " & DTPFec1.Text & "? ", _
   '             "Proceso de autorización.", MessageBoxButtons.YesNo, _
   '             MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

   '        DiaVac = 1

   '        EliminarDiasVac()

   '        NumAuto = TBDiasAut.Text - 1

   '        Try
   '            For i As Integer = 0 To DGVCap.RowCount - 1
   '                'MsgBox(DTPFec1.Text)
   '                'MsgBox(DGVCap.Item(3, i).Value)
   '                'Dim auxfec1 As String = DTPFec1.Text.ToString

   '                If DTPFec1.Text = DGVCap.Item(3, i).Value Then
   '                    'MsgBox("verdadero")

   '                    DGVCap.CurrentCell = DGVCap.Rows(i).Cells(0)

   '                    DGVCap.Rows(i).Selected = True

   '                    Me.DGVCap.Rows.Remove(DGVCap.CurrentRow)
   '                End If
   '            Next
   '        Catch ex As Exception
   '            'MsgBox(ex.Message)
   '        End Try

   '    Else
   '        CKAut1.Checked = True
   '    End If

   'End If

   TBDiasAut.Text = NumAuto

  End If
 End Sub

 Private Sub DGVCap_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVCap.CellContentClick
  If e.RowIndex >= 0 Then
   Dim row As DataGridViewRow = DGVCap.Rows(e.RowIndex)
   Try
    If Me.DGVCap.Columns(e.ColumnIndex).Name = "Borrar" Then
     If (MessageBox.Show("¿Esta seguro que sea borrar el dia " & row.Cells("DiaSol").Value.ToString & "?",
                              "Advertencia",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
      Me.DGVCap.Rows.Remove(row)
      TBDiasRest.Text = TBDiasRest.Text + 1
     End If
     'MsgBox("voy a borrar el dia " & row.Cells("DiaSol").Value.ToString)

    End If
   Catch ex As Exception

   End Try
  End If
 End Sub

 Private Sub CBNomEmp_KeyUp(sender As Object, e As KeyEventArgs) Handles CBNomEmp.KeyUp
  Try
   If e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
    strTemp = CBNomEmp.Text
    If strTemp.Trim.CompareTo(String.Empty) = 0 Then
     DvLP.RowFilter = String.Empty
     DvLP.RowFilter = "NumEmp <> 1010"
    Else
     Dim strRowFilter As String = String.Concat("NomEmp LIKE '%", CBNomEmp.Text, "%' and NumEmp <> 1010 ")
     DvLP.RowFilter = strRowFilter
     'MsgBox(DvLP.Count)
     If DvLP.Count = 0 Then
      DvLP.RowFilter = "NumEmp = 1010"
     End If

    End If


    CBNomEmp.Text = ""
    CBNomEmp.Text = strTemp
    CBNomEmp.SelectionStart = strTemp.Length
    CBNomEmp.SelectedIndex = -1
    CBNomEmp.DroppedDown = True
    CBNomEmp.SelectedIndex = -1
    CBNomEmp.Text = ""
    CBNomEmp.Text = strTemp
    CBNomEmp.SelectionStart = strTemp.Length

   End If



   'DvClte.RowFilter = "Nombre2 like '%" & CmbCliente.Text & "%'"
   'CmbCliente.DroppedDown = True
  Catch ex As Exception
   'MsgBox("errror en filtro nuevo " & ex.Message)
  End Try
 End Sub

 Private Sub CBNomEmp_DropDown(sender As Object, e As EventArgs) Handles CBNomEmp.DropDown
  Me.Cursor = Cursors.Arrow

  If strTemp <> "" Then
   CBNomEmp.Text = strTemp
   CBNomEmp.SelectionStart = strTemp.Length
  End If
  'CBNomEmp.SelectionStart = strTemp.Length
 End Sub

 Private Sub CBNomEmp_Leave(sender As Object, e As EventArgs) Handles CBNomEmp.Leave
  Try
   If CBNomEmp.SelectedIndex.ToString = "-1" Or CBNomEmp.SelectedValue = 1010 Then
    If strTemp <> "" Then
     CBNomEmp.Text = strTemp
     CBNomEmp.SelectionStart = strTemp.Length
    End If
    'MessageBox.Show("Por favor elige a un empleado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    CBNomEmp.SelectedIndex = -1
    LimpiaCampos()
    Return
   End If
   Rutina()
  Catch ex As Exception

  End Try
 End Sub
End Class