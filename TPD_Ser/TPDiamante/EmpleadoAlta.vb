
Imports System.Data.SqlClient

Public Class EmpleadoAlta

    Public conexion As New SqlConnection(StrTpm)
    Public conexionTPD As New SqlConnection(StrCon)
    Dim DVDetalle As DataView

    'BindingSource  
    Private WithEvents bs As New BindingSource

    Private SqlDataAdapter As SqlDataAdapter

  ' Cadena de conexión  
  Private cs As String = conexion_universal.CadenaSQL

  ' flag  
  Private bEdit As Boolean

    'Public StrCon As String = conexion_universal.CadenaSQL

    Private FolioMax As Integer


    Private Sub TBNumEmp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBNumEmp.KeyPress
        If Char.IsLower(e.KeyChar) Then

            'Convert to uppercase, and put at the caret position in the TextBox.
            TBNumEmp.SelectedText = Char.ToUpper(e.KeyChar)

            e.Handled = True
        End If
    End Sub

    Private Sub TBApePat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBApePat.KeyPress
        If Char.IsLower(e.KeyChar) Then

            'Convert to uppercase, and put at the caret position in the TextBox.
            TBApePat.SelectedText = Char.ToUpper(e.KeyChar)

            e.Handled = True
        End If
    End Sub

    Private Sub TBApeMat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBApeMat.KeyPress
        If Char.IsLower(e.KeyChar) Then

            'Convert to uppercase, and put at the caret position in the TextBox.
            TBApeMat.SelectedText = Char.ToUpper(e.KeyChar)

            e.Handled = True
        End If
    End Sub

    Private Sub TBNomEmp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBNomEmp.KeyPress
        If Char.IsLower(e.KeyChar) Then

            'Convert to uppercase, and put at the caret position in the TextBox.
            TBNomEmp.SelectedText = Char.ToUpper(e.KeyChar)

            e.Handled = True
        End If
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        Try
            If (MessageBox.Show( _
                         "¿Confirma que desea guardar este Empleado? ", _
                         "Guardar empleado.", MessageBoxButtons.YesNo, _
                         MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

                GuardarEmpleado()

                'MessageBox.Show("Registros guardados correctamente.", "Operación exitosa.", MessageBoxButtons.OK, _
                'MessageBoxIcon.Information)

                MessageBox.Show("Empleado guardado correctamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LimpiarCampos()

                Actualizar()

                cargar_registros("SELECT numemp,NomEmp+' '+AppEmp+' '+ApmMat,FechaIng,FechaIMSS,T1.GroupName " +
                                 "FROM Empleados T0 " +
                                 "LEFT JOIN SBO_TPD.dbo.OCRG T1 ON T0.Sucursal=T1.GroupCode where T0.Status = 'Activo' ORDER BY NUMEMP", DGVacaciones)

            Else

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub LimpiarCampos()
        TBNumEmp.Text = ""
        TBApePat.Text = ""
        TBApeMat.Text = ""
        TBNomEmp.Text = ""
        DTPFecIng.Value = Date.Now
        DTPFecIMSS.Value = Date.Now
        CBSucursal.SelectedValue = 100

        TBNumEmp.Focus()

    End Sub

    Private Sub GuardarEmpleado()
        Dim conexionrr As New SqlConnection(StrTpm)
        Dim command As New SqlCommand("SPGuardaEmpleado", conexionrr)
        Try
            command.CommandType = CommandType.StoredProcedure
            command.Parameters.AddWithValue("@NumEmp", TBNumEmp.Text)
            command.Parameters.AddWithValue("@ApePat", TBApePat.Text)
            command.Parameters.AddWithValue("@ApeMat", TBApeMat.Text)
            command.Parameters.AddWithValue("@NomEmp", TBNomEmp.Text)
            command.Parameters.AddWithValue("@FecIng", DTPFecIng.Value)
            command.Parameters.AddWithValue("@FecIMSS", DTPFecIMSS.Value)
            command.Parameters.AddWithValue("@Sucursal", CBSucursal.SelectedValue)

            conexionrr.Open()
            command.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'MsgBox("Estos registros ya existen")
        Finally
            conexionrr.Dispose()
            command.Dispose()
        End Try
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TBNumEmp.Text <> "" Then

            conexion.Open()
            Dim cmd As SqlCommand = New SqlCommand("SELECT NumEmp,NomEmp,AppEmp,ApmMat,FechaIng,FechaIMSS,Sucursal FROM Empleados " & _
            "where NumEmp = @NumEmp ", conexion)
            cmd.Parameters.AddWithValue("@NumEmp", TBNumEmp.Text)

            Try

                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count > 0 Then

                    Dim row As DataRow = dt.Rows(0)

                    TBApePat.Text = CStr(row("AppEmp"))

                    TBApeMat.Text = CStr(row("ApmMat"))

                    TBNomEmp.Text = CStr(row("NomEmp"))

                    DTPFecIng.Value = CStr(row("FechaIng"))

                    DTPFecIMSS.Value = CStr(row("FechaIMSS"))

                    CBSucursal.SelectedValue = CStr(row("Sucursal"))

                Else
                    MessageBox.Show("No hay Empleado con este Número", "Alerta.", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation)

                    LimpiaCampos()

                    TBNumEmp.Focus()

                End If

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

    Private Sub LimpiaCampos()

    End Sub

    Private Sub EmpleadoAlta_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '--------------******************************************
        '--------------******************************************

        Try
            Dim da As New SqlDataAdapter("SELECT GroupCode , GroupName " +
                                         "FROM OCRG with (nolock) " +
                                         "WHERE GroupType = 'C' ORDER BY GroupName ", conexionTPD)

            Dim ds As New DataSet
            da.Fill(ds)
            'ds.Tables(0).Rows.Add(0, "TODOS")
            Me.CBSucursal.DataSource = ds.Tables(0)
            Me.CBSucursal.DisplayMember = "GroupName"
            Me.CBSucursal.ValueMember = "GroupCode"
            Me.CBSucursal.SelectedValue = 100

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        '**************************
        '**************************

        Actualizar()

        cargar_registros("SELECT numemp,NomEmp+' '+AppEmp+' '+ApmMat,FechaIng,FechaIMSS,T1.GroupName " +
                         "FROM Empleados T0 " +
                         "LEFT JOIN SBO_TPD.dbo.OCRG T1 ON T0.Sucursal=T1.GroupCode where T0.Status = 'Activo' ORDER BY NUMEMP ", DGVacaciones)

        With DGVacaciones
            ' alternar color de filas  
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .DefaultCellStyle.BackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            '.RowsDefaultCellStyle = 
            ' Establecer el origen de datos para el DataGridview  
            DGVacaciones.DataSource = bs

            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = False
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'Color de linea del grid

            'centrar encabezados del datagrid 
            DGVacaciones.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            Try

                .Columns(0).HeaderText = "No. Empleado"
                .Columns(0).Width = 70
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Nombre"
                .Columns(1).Width = 210
                '.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(2).HeaderText = "Fecha Ingreso"
                .Columns(2).Width = 80
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Fecha Alta IMSS"
                .Columns(3).Width = 80
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).HeaderText = "Ubicación"
                .Columns(4).Width = 120
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


        End With

        '--------------******************************************
        '--------------******************************************
        

    End Sub

   Private Sub cargar_registros( _
         ByVal sql As String, _
         ByVal dv As DataGridView)

        Try
            ' Inicializar el SqlDataAdapter indicandole el comando y el connection string  
            SqlDataAdapter = New SqlDataAdapter(sql, StrTpm)

            Dim SqlCommandBuilder As New SqlCommandBuilder(SqlDataAdapter)

            ' llenar el DataTable  
            Dim dt As New DataTable()
            SqlDataAdapter.Fill(dt)

            ' Enlazar el BindingSource con el datatable anterior  
            ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''  
            bs.DataSource = dt

            With dv
                .Refresh()
                ' coloca el registro arriba de todo  
                '.FirstDisplayedCell = Me.DGObjetivos.CurrentCell

            End With

            bEdit = False

        Catch exSql As SqlException
            MsgBox(exSql.Message.ToString)
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            'MsgBox("Actualmente no existen registros en la base de datos")
            'MessageBox.Show("Nn hay Informacion por mostrar", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Actualizar(Optional ByVal bCargar As Boolean = True)
        ' Actualizar y guardar cambios  

        If Not bs.DataSource Is Nothing Then
            SqlDataAdapter.Update(CType(bs.DataSource, DataTable))
            If bCargar Then
                'cargar_registros("Select * from SC_Objetivos order by anio DESC, mes DESC, groupname DESC, slpname", DGObjetivos)
                cargar_registros("SELECT numemp,NomEmp+' '+AppEmp+' '+ApmMat,FechaIng,FechaIMSS,T1.GroupName " +
                         "FROM Empleados T0 " +
                         "LEFT JOIN SBO_TPD.dbo.OCRG T1 ON T0.Sucursal=T1.GroupCode where T0.Status = 'Activo' ORDER BY NUMEMP ", DGVacaciones)
            End If
        End If
    End Sub

    Private Sub TBNumEmp_Leave(sender As Object, e As EventArgs) Handles TBNumEmp.Leave
        If TBNumEmp.Text <> "" Then

            conexion.Open()
            Dim cmd As SqlCommand = New SqlCommand("SELECT NumEmp,NomEmp,AppEmp,ApmMat,FechaIng,FechaIMSS,Sucursal FROM Empleados " & _
            "where NumEmp = @NumEmp ", conexion)
            cmd.Parameters.AddWithValue("@NumEmp", TBNumEmp.Text)

            Try

                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count > 0 Then

                    Dim row As DataRow = dt.Rows(0)

                    TBApePat.Text = CStr(row("AppEmp"))

                    TBApeMat.Text = CStr(row("ApmMat"))

                    TBNomEmp.Text = CStr(row("NomEmp"))

                    DTPFecIng.Value = CStr(row("FechaIng"))

                    DTPFecIMSS.Value = CStr(row("FechaIMSS"))

                    CBSucursal.SelectedValue = CStr(row("Sucursal"))

                Else
                    TBApePat.Text = ""

                    TBApeMat.Text = ""

                    TBNomEmp.Text = ""

                    DTPFecIng.Value = Date.Now

                    DTPFecIMSS.Value = Date.Now

                    CBSucursal.SelectedValue = 100

                End If

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
End Class