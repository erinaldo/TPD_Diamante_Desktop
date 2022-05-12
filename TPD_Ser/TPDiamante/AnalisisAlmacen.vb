Imports System.Data.SqlClient
Public Class AnalisisAlmacen
    Dim conexion As SqlConnection

    Dim DSAll As New DataSet

    Dim DVAlmacenista As New DataView
    Dim DVEmpacador As New DataView
    Dim DVHistorico As New DataView

    Dim Resultado As New DataView

    Dim ConsutaLista As String = ""
    Dim strTemp As String = ""
    Dim strTemp_proveedor As String = ""
    Dim strTemp_toCombobox1 As String = ""
    Private Sub AnalisisAlmacen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cal As Date = Date.Now().Date
        'MsgBox(String.Format("{0:yyyy-MM-dd}", Date.Now()))
        Try
            conexion = New SqlConnection(StrTpm)
            ConsutaLista = "select * from Almacenistas order by Nombre "
            ConsutaLista &= "select t0.fecha, t0.id, t1.Nombre, t0.puesto, t0.empleado from HistoricoAlmacen t0 inner join Almacenistas t1 on t0.empleado = t1.id order by t0.fecha, puesto "
            Dim daGArticulo As New SqlDataAdapter(ConsutaLista, conexion)
            daGArticulo.Fill(DSAll)

            Dim aux_row As DataRow
            aux_row = DSAll.Tables(0).NewRow
            aux_row("id") = 9999
            aux_row("Nombre") = "--Ningun Resultado--"
            DSAll.Tables(0).Rows.Add(aux_row)

            DVAlmacenista.Table = DSAll.Tables(0)
            DVAlmacenista.RowFilter = "id <> 9999"
            DVEmpacador.Table = DSAll.Tables(0)
            DVEmpacador.RowFilter = "id <> 9999"

            DVHistorico.Table = DSAll.Tables(1)

            CmbAlmacenista.DataSource = DVAlmacenista
            CmbAlmacenista.DisplayMember = "Nombre"
            CmbAlmacenista.ValueMember = "id"
            CmbAlmacenista.SelectedIndex = -1

            CmbEmpacador.DataSource = DVEmpacador
            CmbEmpacador.DisplayMember = "Nombre"
            CmbEmpacador.ValueMember = "id"
            CmbEmpacador.SelectedIndex = -1

            DVHistorico.RowFilter = "fecha = '" & String.Format("{0:yyyy-MM-dd}", Date.Now()) & "' and puesto = 'A' "
            If (DVHistorico.Count = 1) Then
                CmbAlmacenista.SelectedValue = DVHistorico.Item(0).Item("empleado").ToString
                CmbAlmacenista.Enabled = False
            End If

            DVHistorico.RowFilter = "fecha = '" & String.Format("{0:yyyy-MM-dd}", Date.Now()) & "' and puesto = 'E' "
            If (DVHistorico.Count = 1) Then
                CmbEmpacador.SelectedValue = DVHistorico.Item(0).Item("empleado").ToString
                CmbEmpacador.Enabled = False
            End If

            If (CmbEmpacador.Enabled = False) And (CmbAlmacenista.Enabled = False) Then
                BtnGuardar.Enabled = False
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub DTP1_ValueChanged(sender As Object, e As EventArgs) Handles DTP1.ValueChanged
        If (String.Format("{0:yyyy-MM-dd}", DTP1.Value) > String.Format("{0:yyyy-MM-dd}", Date.Now())) Then
            'MsgBox("no puedes seleccionar una fecha mayor a la de hoy")
            MessageBox.Show("No puedes seleccionar una fecha mayor a la de hoy", _
                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DTP1.Value = Date.Now
            modulo()
        Else
            modulo()
        End If
    End Sub

    Public Sub modulo()
        DVAlmacenista.RowFilter = String.Empty
        DVEmpacador.RowFilter = String.Empty
        DVHistorico.RowFilter = "fecha = '" & String.Format("{0:yyyy-MM-dd}", DTP1.Value) & "' and puesto = 'A' "
        If (DVHistorico.Count = 1) Then
            CmbAlmacenista.SelectedValue = DVHistorico.Item(0).Item("empleado").ToString
            CmbAlmacenista.Enabled = False
        Else
            CmbAlmacenista.SelectedIndex = -1
            CmbAlmacenista.Enabled = True
        End If

        DVHistorico.RowFilter = "fecha = '" & String.Format("{0:yyyy-MM-dd}", DTP1.Value) & "' and puesto = 'E' "
        If (DVHistorico.Count = 1) Then
            CmbEmpacador.SelectedValue = DVHistorico.Item(0).Item("empleado").ToString
            CmbEmpacador.Enabled = False
        Else
            CmbEmpacador.SelectedIndex = -1
            CmbEmpacador.Enabled = True
        End If

        If (CmbEmpacador.Enabled = False) And (CmbAlmacenista.Enabled = False) Then
            BtnGuardar.Enabled = False
        Else
            If (CmbEmpacador.Enabled = True) And (CmbAlmacenista.Enabled = True) Then
                If CmbEmpacador.SelectedIndex <> -1 Then
                    DVAlmacenista.RowFilter = "id <> 9999 and id <> " & CmbEmpacador.SelectedValue.ToString
                Else
                    DVAlmacenista.RowFilter = "id <> 9999"
                End If

                If CmbAlmacenista.SelectedIndex <> -1 Then
                    DVEmpacador.RowFilter = "id <> 9999 and id <> " & CmbAlmacenista.SelectedValue.ToString
                Else
                    DVEmpacador.RowFilter = "id <> 9999"
                End If
                'DVAlmacenista.RowFilter = "id <> 9999"
                'DVEmpacador.RowFilter = "id <> 9999"
                CmbAlmacenista.SelectedIndex = -1
                CmbEmpacador.SelectedIndex = -1
            Else
                If CmbEmpacador.Enabled = False Then
                    DVAlmacenista.RowFilter = "id <> " & CmbEmpacador.SelectedValue.ToString & "and id <> 9999"
                    CmbAlmacenista.SelectedIndex = -1
                End If
                If CmbAlmacenista.Enabled = False Then
                    DVEmpacador.RowFilter = "id <> " & CmbAlmacenista.SelectedValue.ToString & "and id <> 9999"
                    CmbEmpacador.SelectedIndex = -1
                End If

            End If
           
            BtnGuardar.Enabled = True
        End If
    End Sub

    Private Sub CmbAlmacenista_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbAlmacenista.KeyUp
        Try
            If (e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z) Or (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
                strTemp = CmbAlmacenista.Text
                If strTemp.Trim.CompareTo(String.Empty) = 0 Then
                    DVAlmacenista.RowFilter = String.Empty
                    If CmbEmpacador.SelectedIndex <> -1 Then
                        DVAlmacenista.RowFilter = "id <> 9999 and id <>  " & CmbEmpacador.SelectedValue.ToString
                    Else
                        DVAlmacenista.RowFilter = "id <> 9999 "
                    End If
                    'DVAlmacenista.RowFilter = "id <> 9999 "
                Else
                    Dim strRowFilter As String = ""
                    If CmbEmpacador.SelectedIndex <> -1 Then
                        'DVAlmacenista.RowFilter = "id <> 9999 and id <>  " & CmbEmpacador.SelectedValue.ToString
                        strRowFilter = String.Concat("Nombre LIKE '%", CmbAlmacenista.Text, "%' and id <> 9999 and id <> ", CmbEmpacador.SelectedValue.ToString)
                    Else
                        strRowFilter = String.Concat("Nombre LIKE '%", CmbAlmacenista.Text, "%' and id <> 9999 ")
                    End If
                    'Dim strRowFilter As String = String.Concat("Nombre LIKE '%", CmbAlmacenista.Text, "%' and id <> 9999 ")
                    DVAlmacenista.RowFilter = strRowFilter
                    'MsgBox(DvLP.Count)
                    If DVAlmacenista.Count = 0 Then
                        DVAlmacenista.RowFilter = "id = 9999"
                    End If

                End If

                CmbAlmacenista.Text = ""
                CmbAlmacenista.Text = strTemp
                CmbAlmacenista.SelectionStart = strTemp.Length
                CmbAlmacenista.SelectedIndex = -1
                CmbAlmacenista.DroppedDown = True
                CmbAlmacenista.SelectedIndex = -1
                CmbAlmacenista.Text = ""
                CmbAlmacenista.Text = strTemp
                CmbAlmacenista.SelectionStart = strTemp.Length

            End If

        Catch ex As Exception
            'MsgBox("errror en filtro nuevo " & ex.Message)
        End Try
    End Sub

    Private Sub CmbAlmacenista_DropDown(sender As Object, e As EventArgs) Handles CmbAlmacenista.DropDown
        Me.Cursor = Cursors.Arrow

        If strTemp <> "" Then
            CmbAlmacenista.Text = strTemp
            CmbAlmacenista.SelectionStart = strTemp.Length
        End If
    End Sub

    Private Sub CmbAlmacenista_Leave(sender As Object, e As EventArgs) Handles CmbAlmacenista.Leave
        Try
            If CmbAlmacenista.SelectedIndex.ToString = "-1" Then
                If strTemp <> "" Then
                    CmbAlmacenista.Text = strTemp
                    CmbAlmacenista.SelectionStart = strTemp.Length
                End If
                CmbAlmacenista.SelectedIndex = -1
                If CmbEmpacador.SelectedIndex = -1 Then
                    DVEmpacador.RowFilter = String.Empty
                    DVEmpacador.RowFilter = "id <> 9999"
                    CmbEmpacador.SelectedIndex = -1
                End If
                
                Return
            End If

            If CmbAlmacenista.SelectedValue.ToString = "9999" Then
                CmbAlmacenista.SelectedIndex = -1
                If CmbEmpacador.SelectedIndex = -1 Then
                    DVEmpacador.RowFilter = String.Empty
                    DVEmpacador.RowFilter = "id <> 9999"
                    CmbEmpacador.SelectedIndex = -1
                End If
               
                CmbAlmacenista.Text = strTemp
                CmbAlmacenista.SelectionStart = strTemp.Length
                Return
            End If
            filtraArticulos()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub filtraArticulos()
        Try
            'If (CmbGrupoArticulo.SelectedValue.ToString() = "999") Then
            '    DVArticulo.RowFilter = String.Empty
            '    DVArticulo.RowFilter = "ItmsGrpCod <> 666"
            '    CmbArticulo.SelectedValue = "TODOS"

            'Else
            If CmbEmpacador.SelectedIndex = -1 Then
                DVEmpacador.RowFilter = "id <> " & CmbAlmacenista.SelectedValue.ToString & " and id <> 9999 "
                CmbEmpacador.SelectedIndex = -1
            Else
                Dim slctvalue2 As Integer = CInt(CmbEmpacador.SelectedValue.ToString)
                DVEmpacador.RowFilter = "id <> " & CmbAlmacenista.SelectedValue.ToString & " and id <> 9999 "
                CmbEmpacador.SelectedValue = slctvalue2
            End If
            
            'ComboBox1.SelectedValue = -1
            'CmbArticulo.SelectedValue = "TODOS"
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CmbAlmacenista_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbAlmacenista.SelectionChangeCommitted
        filtraArticulos()
    End Sub

    Private Sub CmbEmpacador_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbEmpacador.KeyUp
        Try
            If (e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z) Or (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
                strTemp_proveedor = CmbEmpacador.Text
                If strTemp_proveedor.Trim.CompareTo(String.Empty) = 0 Then
                    DVEmpacador.RowFilter = String.Empty
                    If CmbAlmacenista.SelectedIndex <> -1 Then
                        DVEmpacador.RowFilter = "id <> 9999 and id <>  " & CmbAlmacenista.SelectedValue.ToString
                    Else
                        DVEmpacador.RowFilter = "id <> 9999 "
                    End If
                    'DVAlmacenista.RowFilter = "id <> 9999 "
                Else
                    Dim strRowFilter As String = ""
                    If CmbAlmacenista.SelectedIndex <> -1 Then
                        'DVAlmacenista.RowFilter = "id <> 9999 and id <>  " & CmbEmpacador.SelectedValue.ToString
                        strRowFilter = String.Concat("Nombre LIKE '%", CmbEmpacador.Text, "%' and id <> 9999 and id <> ", CmbAlmacenista.SelectedValue.ToString)
                    Else
                        strRowFilter = String.Concat("Nombre LIKE '%", CmbEmpacador.Text, "%' and id <> 9999 ")
                    End If
                    'Dim strRowFilter As String = String.Concat("Nombre LIKE '%", CmbAlmacenista.Text, "%' and id <> 9999 ")
                    DVEmpacador.RowFilter = strRowFilter
                    'MsgBox(DvLP.Count)
                    If DVEmpacador.Count = 0 Then
                        DVEmpacador.RowFilter = "id = 9999"
                    End If

                End If

                CmbEmpacador.Text = ""
                CmbEmpacador.Text = strTemp_proveedor
                CmbEmpacador.SelectionStart = strTemp_proveedor.Length
                CmbEmpacador.SelectedIndex = -1
                CmbEmpacador.DroppedDown = True
                CmbEmpacador.SelectedIndex = -1
                CmbEmpacador.Text = ""
                CmbEmpacador.Text = strTemp_proveedor
                CmbEmpacador.SelectionStart = strTemp_proveedor.Length

            End If

        Catch ex As Exception
            'MsgBox("errror en filtro nuevo " & ex.Message)
        End Try
    End Sub

    Private Sub CmbEmpacador_Leave(sender As Object, e As EventArgs) Handles CmbEmpacador.Leave
        Try
            If CmbEmpacador.SelectedIndex.ToString = "-1" Then
                MsgBox("entre a primer if")
                If strTemp_proveedor <> "" Then
                    CmbEmpacador.Text = strTemp_proveedor
                    CmbEmpacador.SelectionStart = strTemp_proveedor.Length
                End If
                CmbEmpacador.SelectedIndex = -1
                If CmbAlmacenista.SelectedIndex = -1 Then
                    DVAlmacenista.RowFilter = String.Empty
                    DVAlmacenista.RowFilter = "id <> 9999"
                    CmbAlmacenista.SelectedIndex = -1
                End If

                Return
            End If

            If CmbEmpacador.SelectedValue.ToString = "9999" Then
                MsgBox("entre a segundo if")
                CmbEmpacador.SelectedIndex = -1
                If CmbAlmacenista.SelectedIndex = -1 Then
                    DVAlmacenista.RowFilter = String.Empty
                    DVAlmacenista.RowFilter = "id <> 9999"
                    CmbAlmacenista.SelectedIndex = -1
                End If

                CmbEmpacador.Text = strTemp_proveedor
                CmbEmpacador.SelectionStart = strTemp_proveedor.Length
                Return
            End If
            filtraArticulos2()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub filtraArticulos2()
        'MsgBox("entre")
        Try
            'If (CmbGrupoArticulo.SelectedValue.ToString() = "999") Then
            '    DVArticulo.RowFilter = String.Empty
            '    DVArticulo.RowFilter = "ItmsGrpCod <> 666"
            '    CmbArticulo.SelectedValue = "TODOS"

            'Else
            If CmbAlmacenista.SelectedIndex = -1 Then
                DVAlmacenista.RowFilter = "id <> " & CmbEmpacador.SelectedValue.ToString & " and id <> 9999 "
                CmbAlmacenista.SelectedIndex = -1
            Else
                Dim slctvalue As Integer = CInt(CmbAlmacenista.SelectedValue.ToString)
                'MsgBox("entre al else de filtraarticulos2")
                DVAlmacenista.RowFilter = "id <> " & CmbEmpacador.SelectedValue.ToString & " and id <> 9999 "
                CmbAlmacenista.SelectedValue = slctvalue
            End If

            'ComboBox1.SelectedValue = -1
            'CmbArticulo.SelectedValue = "TODOS"
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CmbEmpacador_DropDown(sender As Object, e As EventArgs) Handles CmbEmpacador.DropDown
        Me.Cursor = Cursors.Arrow

        If strTemp_proveedor <> "" Then
            CmbEmpacador.Text = strTemp_proveedor
            CmbEmpacador.SelectionStart = strTemp_proveedor.Length
        End If
    End Sub

    Private Sub CmbEmpacador_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbEmpacador.SelectionChangeCommitted
        filtraArticulos2()
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If CmbAlmacenista.Enabled = True Then
            If CmbAlmacenista.SelectedIndex = -1 Then
                MessageBox.Show("Seleccione un Almacenista", _
                "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbAlmacenista.Focus()
                Return
            End If
        End If

        If CmbEmpacador.Enabled = True Then
            If CmbEmpacador.SelectedIndex = -1 Then
                MessageBox.Show("Seleccione un Empacador", _
                "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbEmpacador.Focus()
                Return
            End If
        End If
        Ejecutar_Consulta()

    End Sub

    Public Sub Ejecutar_Consulta()
        Dim strcadena As String = ""
        Try
            Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
            SqlConnection.Open()
            Dim command As New Data.SqlClient.SqlCommand
            command.Connection = SqlConnection

            If (CmbAlmacenista.Enabled = True) Then
                strcadena = "insert into HistoricoAlmacen (fecha, empleado, puesto) values ('" & String.Format("{0:yyyy-MM-dd}", DTP1.Value) & "', " & CmbAlmacenista.SelectedValue.ToString & ", 'A') "
                command.CommandText = strcadena
                command.ExecuteNonQuery()
                CmbAlmacenista.Enabled = False
            End If

            If (CmbEmpacador.Enabled = True) Then
                strcadena = "insert into HistoricoAlmacen (fecha, empleado, puesto) values ('" & String.Format("{0:yyyy-MM-dd}", DTP1.Value) & "', " & CmbEmpacador.SelectedValue.ToString & ", 'E') "
                'MsgBox(strcadena)
                command.CommandText = strcadena
                command.ExecuteNonQuery()
                CmbEmpacador.Enabled = False
            End If

            If (CmbEmpacador.Enabled = False) And (CmbAlmacenista.Enabled = False) Then
                BtnGuardar.Enabled = False
            End If



            For Each f As Form In Application.OpenForms
                If f.Name.ToString = "MostrarOrdenes" Then
                    'MsgBox("si esta avierto")
                    MostrarOrdenes.Almacenista = CmbAlmacenista.SelectedValue.ToString
                    MostrarOrdenes.Empacador = CmbEmpacador.SelectedValue.ToString
                    MostrarOrdenes.Nombre_Almacenista = CmbAlmacenista.Text.ToString
                    MostrarOrdenes.Nombre_Empacador = CmbEmpacador.Text.ToString
                    MostrarOrdenes.pinta_labels()
                    Exit For
                End If
            Next

            conexion = New SqlConnection(StrTpm)
            Dim DSaux As New DataSet
            Dim s_aux As String = ""
            s_aux = "select t0.fecha, t0.id, t1.Nombre, t0.puesto, t0.empleado from HistoricoAlmacen t0 inner join Almacenistas t1 on t0.empleado = t1.id order by t0.fecha, puesto "
            Dim daGArticulo As New SqlDataAdapter(s_aux, conexion)
            daGArticulo.Fill(DSaux)
            DVHistorico.Table = Nothing
            DVHistorico.Table = DSaux.Tables(0)
            modulo()
            MessageBox.Show("Datos guardados correctamente", _
                "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MsgBox("Intentalo nuevamente " & ex.Message)
        End Try
        

        'strcadena = "i"
        'command.CommandText = strcadena
        'command.ExecuteNonQuery()
    End Sub

    Private Sub AnalisisAlmacen_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        
    End Sub

    Public Sub Refresca_Bitacora()
        For Each f As Form In Application.OpenForms
            If f.Name.ToString = "Bitacora" Then
                'MsgBox("si esta avierto")
                Dim form_bitacora As Bitacora
                form_bitacora = f
                form_bitacora.re_load()
                'MsgBox("si esta avierto")
                Exit For
            End If
        Next

    End Sub
End Class