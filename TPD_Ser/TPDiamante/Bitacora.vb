Imports System.Data.SqlClient
Public Class Bitacora
    Dim DvAgentes As New DataView
    'BindingSource  
    Private WithEvents bs As New BindingSource
    ' Adaptador de datos sql  
    Private SqlDataAdapter As SqlDataAdapter
  ' Cadena de conexión  
  Private cs As String = conexion_universal.cConstanteTPM
  ' flag  
  Private bEdit As Boolean
    'Public StrCon As String = conexion_universal.CadenaSQLSAP
    'PENDIENTE PARA VER SI SE LLOEGARIA A USAR
    'Public StrCon As String = StrPru
    Public cell As DataGridViewCell
    Public chkCell As DataGridViewCheckBoxCell
    Public band As Boolean = False
    Public band2 As Boolean = False



    Private Sub Bitacora_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MsgBox("Tu codigo es " & vCodAgte)
        Dim aux As String
        aux = sender.ToString
        path_form.Text = "Form: " & aux.Substring(aux.IndexOf(".") + 1, (aux.IndexOf(",") - aux.IndexOf(".")) - 1) & ".vb"
        Dim dtFecha As Date = DateSerial(Year(Date.Today), Date.Now.Month, 1)
        Me.DTP1.Value = dtFecha
        Me.DTP2.Value = Date.Today
        'cargar_registros("exec SC_RegsitroC", DataGridView1)
        LlenaCmbAgte()
        'MsgBox(vCodAgte)
    End Sub
    Private Sub cargar_registros(ByVal sql As String, ByVal dv As DataGridView)
        Try
      ' Inicializar el SqlDataAdapter indicandole el comando y el connection string  
      SqlDataAdapter = New SqlDataAdapter(sql, cs)

      Dim SqlCommandBuilder As New SqlCommandBuilder(SqlDataAdapter)
            ' llenar el DataTable  
            Dim dt As New DataTable()
            SqlDataAdapter.Fill(dt)

            ' Enlazar el BindingSource con el datatable anterior  
            ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''  
            bs.DataSource = dt
            dv.DataSource = dt

            With dv
                If band <> True Then
                    band = True
                    dv.Columns.Remove("validado")
                    Dim col As New DataGridViewCheckBoxColumn
                    col.DataPropertyName = "validado"
                    col.HeaderText = "validado"
                    col.Name = "validado"
                    col.SortMode = DataGridViewColumnSortMode.Automatic
                    dv.Columns.Add(col)

                    Dim col_delete As New DataGridViewLinkColumn
                    col_delete.DataPropertyName = "Borrar"
                    col_delete.HeaderText = "Borrar"
                    col_delete.Name = "Borrar"
                    'dv.Columns.Insert(12, col_delete)
                    dv.Columns.Add(col_delete)



                    .Refresh()
                End If

                For i As Integer = 0 To dv.Rows.Count - 1
                    dv.Rows(i).Cells("Borrar").Value = "Eliminar Registro"

                Next

            End With

            bEdit = False
        Catch exSql As SqlException
            MsgBox(exSql.Message.ToString & "Error sql excepcion 1")
        Catch ex As Exception
            MsgBox(ex.Message.ToString & "Error excepcion 2")
            'MsgBox("Actualmente no existen registros en la base de datos")
            'MessageBox.Show("Nn hay Informacion por mostrar", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Sub LlenaCmbAgte()
        Dim Dt As DataTable
        Dim Cn As New SqlConnection(StrTpm)
        Dim Da As New SqlDataAdapter
        Dim Cmd As New SqlCommand
        If IdUsuario = "MANAGER" Or IdUsuario = "CONTAB1" Or IdUsuario = "ACONTABLE" Or IdUsuario = "COMERCIAL" Then
            With Cmd
                .CommandType = CommandType.Text
                '.CommandText = "Select * From Cat_Agentes where Status = 'Activo' order by SlpName"
                .CommandText = "select Placas, Nombre, Nombre + ' - ' + Marca + ' - ' + Modelo as 'Name', Id_Usuario from Coches t0 left join Usuarios t1 on t0.Agente = t1.CodAgte where t0.Status = 'Activo' AND NOMBRE IS NOT NULL  order by Nombre"
                .Connection = Cn
            End With
            Da.SelectCommand = Cmd
            Dt = New DataTable
            Da.Fill(Dt)
            With ComboBox1
                .DataSource = Dt
                .DisplayMember = "Name"
                .ValueMember = "Placas"
                '.SelectedIndex = -1
            End With
        Else
            With Cmd
                .CommandType = CommandType.Text
                '.CommandText = "Select * From Cat_Agentes where Status = 'Activo' order by SlpName"
                .CommandText = "select Placas, Nombre, Nombre + ' - ' + Marca + ' - ' + Modelo as 'Name', Id_Usuario from Coches t0 left join Usuarios t1 on t0.Agente = t1.CodAgte where t0.Status = 'Activo' and t1.Id_Usuario = '" & UsrTPM & "' order by Nombre"
                .Connection = Cn
            End With
            Da.SelectCommand = Cmd
            Dt = New DataTable
            Da.Fill(Dt)
            With ComboBox1
                .DataSource = Dt
                .DisplayMember = "Name"
                .ValueMember = "Placas"
                '.SelectedIndex = -1
            End With
            'Dt = New DataTable
            'Dt.Columns.Add("Slpname")
            'Dt.Columns.Add("Slpcode")
            'Dim dr As DataRow
            'dr = Dt.NewRow()
            'dr("Slpname") = NomUsuario
            'dr("Slpcode") = vCodAgte
            'Dt.Rows.Add(dr)
            'Me.ComboBox1.DataSource = Dt
            'Me.ComboBox1.ValueMember = "Slpcode"
            'Me.ComboBox1.DisplayMember = "Slpname"
            'ComboBox1.SelectedValue = vCodAgte
            'ComboBox1.Enabled = False

        End If
        'Dim dt As New DataTable("Tabla")
        'dt.Columns.Add("Slpname")
        'dt.Columns.Add("Slpcode")
        'Dim dr As DataRow
        'dr = dt.NewRow()
        'dr("Slpname") = "Aurelio Castro"
        'dr("Slpcode") = "13"
        'dt.Rows.Add(dr)
        'dr = dt.NewRow()
        'dr("Slpname") = "Jaime Sanchez"
        'dr("Slpcode") = "15"
        'dt.Rows.Add(dr)
        'dr = dt.NewRow()
        'dr("Slpname") = "Carlos Santos"
        'dr("Slpcode") = "30"
        'dt.Rows.Add(dr)
        'dr = dt.NewRow()
        'dr("Slpname") = "Ricardo Robles"
        'dr("Slpcode") = "17"
        'dt.Rows.Add(dr)
        'dr = dt.NewRow()
        'dr("Slpname") = "Marco Lopez"
        'dr("Slpcode") = "20"
        'dt.Rows.Add(dr)
        'dr = dt.NewRow()
        'dr("Slpname") = "Rafael Jimenez"
        'dr("Slpcode") = "26"
        'dt.Rows.Add(dr)
        'dr = dt.NewRow()
        'dr("Slpname") = "Rodolfo Mercado"
        'dr("Slpcode") = "10"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Slpname") = "Victor Vergara"
        'dr("Slpcode") = "8"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Slpname") = "Salvador Diaz"
        'dr("Slpcode") = "50"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Slpname") = "Nelly Gomez"
        'dr("Slpcode") = "51"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Slpname") = "Reina UC"
        'dr("Slpcode") = "52"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Slpname") = "Miriam Hernandez"
        'dr("Slpcode") = "53"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Slpname") = "Eduardo Texis"
        'dr("Slpcode") = "54"
        'dt.Rows.Add(dr)
        'Me.ComboBox1.DataSource = dt
        'Me.ComboBox1.ValueMember = "Slpcode"
        'Me.ComboBox1.DisplayMember = "Slpname"
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim objcommand As New SqlCommand

        cargar_registros("exec  SC_RGe @agnt = '" & ComboBox1.SelectedValue.ToString & "',@f1 = '" & String.Format("{0:yyyyMMdd}", DTP1.Value) & "',@f2 = '" & String.Format("{0:yyyyMMdd}", DTP2.Value) & "'", DataGridView1)
        objcommand.CommandText = "SELECT convert(decimal(19,2),SUM(T1.costocarga)), SUM(t1.kmsrecord)," +
                           "convert(decimal(19,2),SUM(t1.cantidad)),count(t1.fecha),AVG(t1.kmsrecord), " +
                           "convert(decimal(19,2),SUM(t1.kmsrecord)/SUM(t1.cantidad)),t2.odometro2,avg(T1.costocarga), " +
                           "AVG(t1.cantidad),SUM(t1.kmsrecord) / DATEDIFF(DD,'" & String.Format("{0:yyyyMMdd}", DTP1.Value) & "'," +
                           "'" & String.Format("{0:yyyyMMdd}", DTP2.Value) & "'),convert(decimal(19,2)," +
                           "SUM(T1.costocarga)/DATEDIFF(DD,'" & String.Format("{0:yyyyMMdd}", DTP1.Value) & "'," +
                           "'" & String.Format("{0:yyyyMMdd}", DTP2.Value) & "'))," +
                           "case when DATEDIFF(MONTH,'" & String.Format("{0:yyyyMMdd}", DTP1.Value) & "'," +
                           "'" & String.Format("{0:yyyyMMdd}", DTP2.Value) & "') = 0 then " +
                           "SUM(t1.costocarga)else SUM(t1.kmsrecord)/ DATEDIFF(MONTH,'" & String.Format("{0:yyyyMMdd}", DTP1.Value) & "'," +
                           "'" & String.Format("{0:yyyyMMdd}", DTP2.Value) & "')end, MAX(t1.cantidad),MIN(t1.cantidad)," +
                           "t2.tipoauto,t2.Marca,t2.modelo,t2.odometro2,t2.Placas FROM Carga2 " +
                           "T1 INNER JOIN  COCHES T2 ON T1.placas = T2.Placas " +
                           "WHERE T2.Placas = '" & ComboBox1.SelectedValue.ToString & "' AND T1.FECHA >= '" & String.Format("{0:yyyyMMdd}", DTP1.Value) & "'AND T1.FECHA <='" & String.Format("{0:yyyyMMdd}", DTP2.Value) & "'group by t2.odometro2,t2.tipoauto,t2.Marca,t2.modelo,t2.odometro2,t2.Placas"

        band2 = False
        Dim sconec As String = StrTpm
        objcommand.Connection = New SqlConnection(sconec)
        objcommand.Connection.Open()
        'MsgBox(objcommand.CommandText.ToString)
        Dim OBJDR As SqlDataReader = objcommand.ExecuteReader()
        If OBJDR.HasRows Then
            While OBJDR.Read()
                Label10.Text = OBJDR.Item(0).ToString()
                Label7.Text = OBJDR.Item(1).ToString()
                Label9.Text = OBJDR.Item(2)
                Label13.Text = OBJDR.Item(3)
                Label5.Text = OBJDR.Item(4).ToString()
                Label15.Text = OBJDR.Item(5).ToString()
                Label17.Text = OBJDR.Item(6).ToString()
                Label19.Text = OBJDR.Item(7).ToString()
                Label21.Text = OBJDR.Item(8).ToString()
                Label23.Text = OBJDR.Item(9).ToString()
                Label25.Text = OBJDR.Item(10).ToString()
                Label26.Text = OBJDR.Item(11).ToString()
                Label31.Text = OBJDR.Item(12).ToString()
                Label28.Text = OBJDR.Item(13).ToString()
                Label41.Text = OBJDR.Item(14).ToString()
                Label43.Text = OBJDR.Item(15).ToString()
                Label45.Text = OBJDR.Item(16).ToString()
                Label49.Text = OBJDR.Item(17).ToString()
                Label48.Text = OBJDR.Item(18).ToString()
                't2.tipoauto,t2.Marca,t2.modelo,t2.odometro2,t2.Placas 
            End While
        Else
            'MsgBox("no hay datos que mostrar")
            MessageBox.Show("No hay cargas de combustible registradas para el intervalo de fecha seleccionado", "No hay Registros", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
        OBJDR.Close()
        objcommand.Dispose()

        With DataGridView1
            ' alternar color de filas  
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .DefaultCellStyle.BackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            '.RowsDefaultCellStyle = 
            ' Establecer el origen de datos para el DataGridview  
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'Color de linea del grid

            'centrar encabezados del datagrid 
            DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Try

                '.Columns(0).HeaderText = "Cod. Agente" fecha
                .Columns("Fecha").Width = 80
                .Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Fecha").DefaultCellStyle.Format = "dd/MMM/yyyy"

                '.Columns(0).HeaderText = "Cod. Agente" odometro
                .Columns("Odometro").Width = 80
                .Columns("Odometro").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '.Columns(0).HeaderText = "Cod. Agente" precio
                .Columns("Precio").Width = 60
                .Columns("Precio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Precio").DefaultCellStyle.Format = "$ #,###,##0.#0"

                '.Columns(0).HeaderText = "Cod. Agente" litros
                .Columns("Litros").Width = 60
                .Columns("Litros").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(3).DefaultCellStyle.Format = "$ #,###,##0"

                '.Columns(0).HeaderText = "Cod. Agente" combustible
                .Columns("Combustible").Width = 80
                .Columns("Combustible").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '.Columns(0).HeaderText = "Cod. Agente" costo de carga
                .Columns("Costo de Carga").Width = 100
                .Columns("Costo de Carga").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Costo de Carga").DefaultCellStyle.Format = "$ #,###,##0.#0"

                '.Columns(0).HeaderText = "Cod. Agente" km recorridos
                .Columns("KM Recorridos").Width = 100
                .Columns("KM Recorridos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '.Columns(0).HeaderText = "Cod. Agente" kms/l
                .Columns("KMS/L").Width = 100
                .Columns("KMS/L").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                '.Columns(0).HeaderText = "Cod. Agente" importe acumulado
                .Columns("Importe Acumulado").Width = 115
                .Columns("Importe Acumulado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Importe Acumulado").DefaultCellStyle.Format = "$ #,###,##0.#0"

                '.Columns(0).HeaderText = "Cod. Agente" acumulado litros
                .Columns("Acumulado Litros").Width = 115
                .Columns("Acumulado Litros").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '.Columns(0).HeaderText = "Cod. Agente" comentarios
                .Columns("comentarios").Width = 115
                .Columns("comentarios").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns("comentarios").DefaultCellStyle.Format = "$ #,###,##0"

                .Columns("Borrar").Width = 150
                .Columns("Borrar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Catch ex As Exception
                MsgBox(ex.Message & " Error excepcion numero 3")
            End Try


        End With

        If DataGridView1.Rows.Count <> 0 Then
            GroupBox3.Visible = True
        Else
            GroupBox3.Visible = False
        End If


    End Sub


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Try
                If Me.DataGridView1.Columns(e.ColumnIndex).Name = "validado" Then  'Si dio click en la celda del checkbox'
                    If IdUsuario = "MANAGER" Or IdUsuario = "CONTAB1" And IdUsuario = "ACONTABLE" Then
                        If Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = 0 Then 'Si el checklist esta en 0 (no-checked)'
                            If (MessageBox.Show("¿Esta seguro que desea validar este registro?", _
                        "Advertencia", _
                        MessageBoxButtons.YesNo, _
                       MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
                                Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = 1
                                Me.DataGridView1.RefreshEdit()
                                Dim con As New SqlConnection
                                Dim cmd As New SqlCommand
                                Dim CadenaSQL As String = ""
                                Dim resultado = Format(row.Cells("Fecha").Value, "yyyy-MM-dd")
                                CadenaSQL = "UPDATE Carga2 set validado = 1 where (Select cast(fecha as date) as fecha2) = '" & resultado.ToString & "' and placas= '" & Label48.Text & "' and odometro = " & row.Cells("Odometro").Value & " and precio = " & row.Cells("Precio").Value

                                Try
                                    con.ConnectionString = StrTpm
                                    con.Open()
                                    cmd.Connection = con
                                    cmd.CommandText = CadenaSQL
                                    cmd.ExecuteNonQuery()
                                    Me.DataGridView1.RefreshEdit()

                                Catch ex As Exception
                                    MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                Finally
                                    con.Close()
                                End Try
                            End If


                        Else 'Si ya esta validado (checklist = 1)'
                            If (MessageBox.Show("¿Esta seguro que desea desvalidar este registro?", _
                        "Advertencia", _
                        MessageBoxButtons.YesNo, _
                       MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
                                Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = 0
                                Me.DataGridView1.RefreshEdit()
                                Dim con As New SqlConnection
                                Dim cmd As New SqlCommand
                                Dim CadenaSQL As String = ""
                                Dim resultado = Format(row.Cells("Fecha").Value, "yyyy-MM-dd")
                                CadenaSQL = "UPDATE Carga2 set validado = 0 where (Select cast(fecha as date) as fecha2) = '" & resultado.ToString & "' and placas= '" & Label48.Text & "' and odometro = " & row.Cells("Odometro").Value & " and precio = " & row.Cells("Precio").Value

                                Try
                                    con.ConnectionString = StrTpm
                                    con.Open()
                                    cmd.Connection = con
                                    cmd.CommandText = CadenaSQL
                                    cmd.ExecuteNonQuery()
                                    Me.DataGridView1.RefreshEdit()

                                Catch ex As Exception
                                    MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                Finally
                                    con.Close()
                                End Try
                            End If
                        End If
                    Else
                        MessageBox.Show("Usted no tiene permiso para cambiar esta opcion", "Permiso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If

                If Me.DataGridView1.Columns(e.ColumnIndex).Name = "Borrar" Then 'si dio click en la celda de borrar registro

                    If band2 Then
                        If DataGridView1.SortedColumn.Name.ToString <> "Fecha" Or DataGridView1.SortOrder = Windows.Forms.SortOrder.Descending Then
                            MsgBox("Antes de eliminar, ordena los registros por fecha de manera Ascendente")
                            Return
                        End If
                    End If

                    If IdUsuario <> "MANAGER" And IdUsuario <> "CONTAB1" And IdUsuario <> "ACONTABLE" Then 'Verifica si el usuario no es administrador'

                        If Me.DataGridView1.Rows(e.RowIndex).Cells("validado").Value = 1 Then
                            MessageBox.Show("No puede eliminar un registro que ya ha sido validado", "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            If (e.RowIndex - 1) <> -1 Then
                                Dim row_aux As DataGridViewRow = DataGridView1.Rows(e.RowIndex - 1)
                                If (MessageBox.Show("Al borrar el registro se eliminaran todos los registros posteriores a la fecha: " & Format(row.Cells("Fecha").Value, "dd-MMM-yyyy") & "." & vbCrLf & "Tu ultima carga sera la del dia " & Format(row_aux.Cells("Fecha").Value, "dd-MMM-yyyy") & " con un odometro de " & row_aux.Cells("Odometro").Value & vbCrLf & vbCrLf & "¿Confirma que desea ejecutar esta acción?", _
                                "Advertencia", _
                                MessageBoxButtons.YesNo, _
                                MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
                                    Dim con As New SqlConnection
                                    Dim cmd As New SqlCommand
                                    Dim CadenaSQL As String = ""
                                    Dim CadenaSQL2 As String = ""
                                    Dim resultado = Format(row.Cells("Fecha").Value, "yyyy-MM-dd")
                                    CadenaSQL = "DELETE FROM Carga2 where (Select cast(fecha as date) as fecha2) >= '" & Format(row.Cells("Fecha").Value, "yyyy-MM-dd") & "' and placas='" & Label48.Text & "' and odometro >= " & row.Cells("Odometro").Value
                                    CadenaSQL2 = "UPDATE Coches set odometro2 = " & row_aux.Cells("Odometro").Value & " WHERE Placas='" & Label48.Text & "' AND Agente= " & vCodAgte
                                    'MsgBox(CadenaSQL)
                                    'MsgBox(CadenaSQL2)
                                    Try
                                        con.ConnectionString = StrTpm
                                        con.Open()
                                        cmd.Connection = con
                                        cmd.CommandText = CadenaSQL
                                        cmd.ExecuteNonQuery()
                                        cmd.CommandText = CadenaSQL2
                                        cmd.ExecuteNonQuery()
                                        Me.DataGridView1.RefreshEdit()
                                        'MsgBox("Registros Eliminados")
                                        MessageBox.Show("Registros Eliminados Correctamente", "Accion Ejecutada", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                        Button1.PerformClick()
                                        Refresca_cargaCombustible()
                                    Catch ex As Exception
                                        MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                    Finally
                                        con.Close()
                                    End Try
                                End If
                            Else
                                If (MessageBox.Show("Al borrar el registro se eliminaran todos los registros posteriores a la fecha: " & Format(row.Cells("Fecha").Value, "dd-MMM-yyyy") & "." & vbCrLf & "Tu ultima carga sera la ultima del mes anterior" & vbCrLf & vbCrLf & "¿Confirma que desea ejecutar esta acción?", _
                               "Advertencia", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
                                    Dim odometro_nuevo As Integer
                                    Dim con As New SqlConnection
                                    Dim cmd As New SqlCommand
                                    Dim CadenaSQL As String = ""
                                    Dim CadenaSQL2 As String = ""
                                    Dim resultado = Format(row.Cells("Fecha").Value, "yyyy-MM-dd")
                                    CadenaSQL = "DELETE FROM Carga2 where (Select cast(fecha as date) as fecha2) >= '" & Format(row.Cells("Fecha").Value, "yyyy-MM-dd") & "' and placas='" & Label48.Text & "' and odometro >= " & row.Cells("Odometro").Value
                                    'CadenaSQL2 = "UPDATE Coches set odometro2 = " & row_aux.Cells("Odometro").Value & " WHERE Placas='" & Label48.Text & "' AND Agente= " & vCodAgte
                                    Try
                                        con.ConnectionString = StrTpm
                                        con.Open()
                                        cmd.Connection = con
                                        cmd.CommandText = CadenaSQL
                                        cmd.ExecuteNonQuery()
                                        Me.DataGridView1.RefreshEdit()


                                        Dim sconec As String = StrTpm
                                        Dim objcommand As New SqlCommand
                                        objcommand.CommandText = "select * from Carga2 where id in (select MAX(id) from Carga2 where placas='" & Label48.Text & "')"
                                        objcommand.Connection = New SqlConnection(sconec)
                                        objcommand.Connection.Open()
                                        Dim OBJDR As SqlDataReader = objcommand.ExecuteReader()
                                        If OBJDR.HasRows Then
                                            While OBJDR.Read()
                                                odometro_nuevo = Integer.Parse(OBJDR.Item("odometro").ToString())
                                                'MsgBox("odometro ultimo: " & OBJDR.Item("odometro").ToString())
                                            End While
                                            CadenaSQL2 = "UPDATE Coches set odometro2 = " & odometro_nuevo & " WHERE Placas='" & Label48.Text & "' AND Agente= " & vCodAgte
                                            cmd.CommandText = CadenaSQL2
                                            cmd.ExecuteNonQuery()
                                            Me.DataGridView1.RefreshEdit()


                                        Else
                                            MsgBox("no hay datos que mostrar")
                                        End If
                                        MessageBox.Show("Registros Eliminados Correctamente", "Accion Ejecutada", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        OBJDR.Close()
                                        objcommand.Dispose()
                                        Dim mes As Integer
                                        mes = Integer.Parse(DTP1.Value.Month)
                                        If (mes - 1) <> 0 Then
                                            DTP1.Value = New DateTime(Integer.Parse(DTP1.Value.Year), (Integer.Parse(DTP1.Value.Month) - 1), 1)
                                        Else
                                            DTP1.Value = New DateTime(Integer.Parse(DTP1.Value.Year) - 1, 12, 1)
                                        End If

                                        Button1.PerformClick()
                                        Refresca_cargaCombustible()
                                    Catch ex As Exception
                                        MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                    Finally
                                        con.Close()
                                    End Try
                                End If

                            End If


                        End If
                    Else 'Si el usuario es administrador'
                        If (e.RowIndex - 1) <> -1 Then
                            Dim row_aux As DataGridViewRow = DataGridView1.Rows(e.RowIndex - 1)
                            If (MessageBox.Show("Al borrar el registro se eliminaran todos los registros posteriores a la fecha: " & Format(row.Cells("Fecha").Value, "dd-MMM-yyyy") & "." & vbCrLf & "La ultima carga sera la del dia " & Format(row_aux.Cells("Fecha").Value, "dd-MMM-yyyy") & " con un odometro de " & row_aux.Cells("Odometro").Value & vbCrLf & vbCrLf & "¿Confirma que desea ejecutar esta acción?", _
                        "Advertencia", _
                        MessageBoxButtons.YesNo, _
                       MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
                                Dim con As New SqlConnection
                                Dim cmd As New SqlCommand
                                Dim CadenaSQL As String = ""
                                Dim CadenaSQL2 As String = ""
                                Dim resultado = Format(row.Cells("Fecha").Value, "yyyy-MM-dd")
                                CadenaSQL = "DELETE FROM Carga2 where (Select cast(fecha as date) as fecha2) >= '" & Format(row.Cells("Fecha").Value, "yyyy-MM-dd") & "' and placas='" & Label48.Text & "' and odometro >= " & row.Cells("Odometro").Value
                                'CadenaSQL2 = "UPDATE Coches set odometro2 = " & row_aux.Cells("Odometro").Value & " WHERE Placas='" & Label48.Text & "' AND Agente in (select SlpCode from [SBO_TPD].[dbo].[oslp] where SlpName like '%" & ComboBox1.SelectedValue.ToString & "%')"
                                CadenaSQL2 = "UPDATE Coches set odometro2 = " & row_aux.Cells("Odometro").Value & " WHERE Placas='" & Label48.Text & "' "
                                'MsgBox(CadenaSQL2)
                                Try
                                    con.ConnectionString = StrTpm
                                    con.Open()
                                    cmd.Connection = con
                                    cmd.CommandText = CadenaSQL
                                    cmd.ExecuteNonQuery()
                                    cmd.CommandText = CadenaSQL2
                                    cmd.ExecuteNonQuery()
                                    Me.DataGridView1.RefreshEdit()
                                    'MsgBox("Registros Eliminados")
                                    MessageBox.Show("Registros Eliminados Correctamente", "Accion Ejecutada", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                    Button1.PerformClick()

                                Catch ex As Exception
                                    MessageBox.Show("Error Insertando RegistrAAAo" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                Finally
                                    con.Close()
                                End Try
                            End If
                        Else
                            If (MessageBox.Show("Al borrar el registro se eliminaran todos los registros posteriores a la fecha: " & Format(row.Cells("Fecha").Value, "dd-MMM-yyyy") & "." & vbCrLf & "La ultima carga sera la ultima del mes anterior" & vbCrLf & vbCrLf & "¿Confirma que desea ejecutar esta acción?", _
                            "Advertencia", _
                            MessageBoxButtons.YesNo, _
                            MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
                                Dim odometro_nuevo As Integer
                                Dim con As New SqlConnection
                                Dim cmd As New SqlCommand
                                Dim CadenaSQL As String = ""
                                Dim CadenaSQL2 As String = ""
                                Dim resultado = Format(row.Cells("Fecha").Value, "yyyy-MM-dd")
                                CadenaSQL = "DELETE FROM Carga2 where (Select cast(fecha as date) as fecha2) >= '" & Format(row.Cells("Fecha").Value, "yyyy-MM-dd") & "' and placas='" & Label48.Text & "' and odometro >= " & row.Cells("Odometro").Value
                                'CadenaSQL2 = "UPDATE Coches set odometro2 = " & row_aux.Cells("Odometro").Value & " WHERE Placas='" & Label48.Text & "' AND Agente= " & vCodAgte
                                Try
                                    con.ConnectionString = StrTpm
                                    con.Open()
                                    cmd.Connection = con
                                    cmd.CommandText = CadenaSQL
                                    cmd.ExecuteNonQuery()
                                    Me.DataGridView1.RefreshEdit()


                                    Dim sconec As String = StrTpm
                                    Dim objcommand As New SqlCommand
                                    objcommand.CommandText = "select * from Carga2 where id in (select MAX(id) from Carga2 where placas='" & Label48.Text & "')"
                                    objcommand.Connection = New SqlConnection(sconec)
                                    objcommand.Connection.Open()
                                    Dim OBJDR As SqlDataReader = objcommand.ExecuteReader()
                                    If OBJDR.HasRows Then
                                        While OBJDR.Read()
                                            odometro_nuevo = Integer.Parse(OBJDR.Item("odometro").ToString())
                                            'MsgBox("odometro ultimo: " & OBJDR.Item("odometro").ToString())
                                        End While
                                        'CadenaSQL2 = "UPDATE Coches set odometro2 = " & odometro_nuevo & " WHERE Placas='" & Label48.Text & "' AND Agente in (select SlpCode from [SBO_TPD].[dbo].[oslp] where SlpName like '%" & ComboBox1.SelectedValue.ToString & "%')"
                                        CadenaSQL2 = "UPDATE Coches set odometro2 = " & odometro_nuevo & " WHERE Placas='" & Label48.Text & "' "

                                        cmd.CommandText = CadenaSQL2
                                        cmd.ExecuteNonQuery()
                                        Me.DataGridView1.RefreshEdit()


                                    Else
                                        MsgBox("no hay datos que mostrar")
                                    End If
                                    MessageBox.Show("Registros Eliminados Correctamente", "Accion Ejecutada", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    OBJDR.Close()
                                    objcommand.Dispose()
                                    Dim mes As Integer
                                    mes = Integer.Parse(DTP1.Value.Month)
                                    If (mes - 1) <> 0 Then
                                        DTP1.Value = New DateTime(Integer.Parse(DTP1.Value.Year), (Integer.Parse(DTP1.Value.Month) - 1), 1)
                                    Else
                                        DTP1.Value = New DateTime(Integer.Parse(DTP1.Value.Year) - 1, 12, 1)
                                    End If

                                    Button1.PerformClick()
                                Catch ex As Exception
                                    MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                Finally
                                    con.Close()
                                End Try

                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                MsgBox("Algo salio mal: " & ex.Message)

            End Try

        End If
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        'MsgBox("Presionaste en ordenar")
        band2 = True
        Pinta_letras()

        
    End Sub

    Public Sub re_load()
        Me.Button1.PerformClick()
    End Sub

    Public Sub Pinta_letras()
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(i).Cells("Borrar").Value = "Eliminar Registro"
        Next

    End Sub

    Public Sub Refresca_cargaCombustible()
        For Each f As Form In Application.OpenForms
            If f.Name.ToString = "CargaCombustible" Then
                Dim form_aux As CargaCombustible
                form_aux = f
                form_aux.Recargar()
                Exit For
            End If
        Next
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'GridAExcel(Me.DataGridView1)
        If Me.DataGridView1.Rows.Count <> 0 Then
            ExportarDatosExcel(Me.DataGridView1, "Bitacora de Combustible del Agente: " & ComboBox1.Text.ToString)
        Else
            MessageBox.Show("No hay datos que almacenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class