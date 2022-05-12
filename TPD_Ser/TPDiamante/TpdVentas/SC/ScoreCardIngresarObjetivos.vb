
Option Explicit On
'Option Strict On
Imports System.Data.SqlClient
Public Class ScoreCardIngresarObjetivos


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

    ' actualizar los cambios al salir  
    ' ''''''''''''''''''''''''''''''''''''''''  
    Private Sub ScoreCardIngresarObjetivos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If bEdit Then
            If (MessageBox.Show("¿Desea guardar cambios?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
                Actualizar(False)
            End If
        End If
    End Sub


    Sub LlenaCmbAgte()
        Dim ConsutaLista As String


        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
            Dim DSetTablas As New DataSet
            ConsutaLista = "SELECT T0.slpcode,T0.slpname,T1.GroupCode FROM OSLP T0 "
            ConsutaLista &= "INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
            ConsutaLista &= "WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 AND (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') ORDER BY slpname "



            Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


            daAgte.Fill(DSetTablas, "Agentes")

            Dim filaAgte As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaAgte = DSetTablas.Tables("Agentes").NewRow

            'Agregamos los valores a los campos de la tabla
            filaAgte("slpname") = "TODOS"
            filaAgte("slpcode") = 999
            filaAgte("GroupCode") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

            DvAgentes.Table = DSetTablas.Tables("Agentes")

            Me.CmbAgteVta.DataSource = DvAgentes
            Me.CmbAgteVta.DisplayMember = "slpname"
            Me.CmbAgteVta.ValueMember = "slpcode"
            Me.CmbAgteVta.SelectedValue = 999
        End Using
    End Sub

    Sub LLenaMes()

        'elementos del combobox
        ComboBoxMes.Items.Add("Enero")
        ComboBoxMes.Items.Add("Febrero")
        ComboBoxMes.Items.Add("Marzo")
        ComboBoxMes.Items.Add("Abril")
        ComboBoxMes.Items.Add("Mayo")
        ComboBoxMes.Items.Add("Junio")
        ComboBoxMes.Items.Add("Julio")
        ComboBoxMes.Items.Add("Agosto")
        ComboBoxMes.Items.Add("Septiembre")
        ComboBoxMes.Items.Add("Octubre")
        ComboBoxMes.Items.Add("Noviembre")
        ComboBoxMes.Items.Add("Diciembre")
        ComboBoxMes.Items.Add("TODOS")

        ComboBoxMes.SelectedIndex = Month(Now) - 1

        TBObjetivo.Text = Format(0, "#,##0.00")

        TextBoxAño.Text = Year(Now)

    End Sub


    Private Sub Form1_Load( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load


        Actualizar()

        LlenaCmbAgte()

        LLenaMes()

        ' propiedades del datagrid  
        ' '''''''''''''''''''''''''''''''''''''  

        btn_first.Text = "<<"
        btn_Previous.Text = "<"
        btn_next.Text = ">"
        btn_last.Text = ">>"

        'ComboBoxMes.SelectedIndex = Month(Now) - 1

        ' cagar los datos  -----Select slpname, groupname,anio,mes,objetivo from SC_Objetivos
        cargar_registros("Select * from SC_Objetivos where mes = " & Month(Now) & " order by anio DESC, mes DESC, groupcode DESC, slpname", DGObjetivos)


        With DGObjetivos
            ' alternar color de filas  
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .DefaultCellStyle.BackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            '.RowsDefaultCellStyle = 
            ' Establecer el origen de datos para el DataGridview  
            DGObjetivos.DataSource = bs

            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = False
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'Color de linea del grid

            'centrar encabezados del datagrid 
            DGObjetivos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            Try

                .Columns(0).HeaderText = "Cod. Agente"
                .Columns(0).Width = 60
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Agente"
                .Columns(1).Width = 200
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft

                .Columns(2).HeaderText = "Cod. Sucursal"
                .Columns(2).Width = 60
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Sucursal"
                .Columns(3).Width = 100
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).HeaderText = "Año"
                .Columns(4).Width = 60
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Mes"
                .Columns(5).Width = 60
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(6).HeaderText = "Objetivo"
                .Columns(6).Width = 115
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Format = "$ #,###,##0"

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


        End With


    End Sub

    Private Sub cargar_registros( _
        ByVal sql As String, _
        ByVal dv As DataGridView)

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

    ' botón para guardar los cambios y llenar la grilla  
    Private Sub Button1_Click( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs)

        Actualizar()

    End Sub


    ' Eliminar el elemento actual del BindingSource y actualizar  
    Private Sub btn_delete_Click( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs)

        If Not bs.Current Is Nothing Then
            ' eliminar  
            bs.RemoveCurrent()

            'Guardar los cambios y recargar  
            Actualizar()
        Else
            MsgBox("No hay un registro actual para eliminar", _
                   MsgBoxStyle.Exclamation, _
                   "Eliminar")
        End If


    End Sub

    Private Sub Actualizar(Optional ByVal bCargar As Boolean = True)
        ' Actualizar y guardar cambios  

        If Not bs.DataSource Is Nothing Then
            SqlDataAdapter.Update(CType(bs.DataSource, DataTable))
            If bCargar Then
                'cargar_registros("Select * from SC_Objetivos order by anio DESC, mes DESC, groupname DESC, slpname", DGObjetivos)
                cargar_registros("Select * from SC_Objetivos where mes = " & Month(Now) & " order by anio DESC, mes DESC, groupcode DESC, slpname", DGObjetivos)
            End If
        End If
    End Sub

    Private Sub btn_first_Click( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs) _
            Handles btn_first.Click, btn_last.Click, btn_next.Click, btn_Previous.Click

        ' Botones para moverse por los registros  
        ' '''''''''''''''''''''''''''''''''''''''''''''  

        If sender Is btn_Previous Then
            bs.MovePrevious()
        ElseIf sender Is btn_first Then
            bs.MoveFirst()
        ElseIf sender Is btn_next Then
            bs.MoveNext()
        ElseIf sender Is btn_last Then
            bs.MoveLast()
        End If

    End Sub

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGObjetivos.CellEndEdit
        bEdit = True

        BActualizar.Visible = True

        'If bEdit Then
        '    Actualizar(False)
        'End If
        'Actualizar(False)
    End Sub

    ' nuevo registro  
    Private Sub btn_new_Click( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs)
        bs.AddNew()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles BtnConsultar.Click


        'variable para guardar como numero y no como texto el valor ingresado en mes
        Dim Mes As Integer

        'if para determinar el numero de mes
        If ComboBoxMes.SelectedItem = "Enero" Then
            Mes = 1
        ElseIf ComboBoxMes.SelectedItem = "Febrero" Then
            Mes = 2
        ElseIf ComboBoxMes.SelectedItem = "Marzo" Then
            Mes = 3
        ElseIf ComboBoxMes.SelectedItem = "Abril" Then
            Mes = 4
        ElseIf ComboBoxMes.SelectedItem = "Mayo" Then
            Mes = 5
        ElseIf ComboBoxMes.SelectedItem = "Junio" Then
            Mes = 6
        ElseIf ComboBoxMes.SelectedItem = "Julio" Then
            Mes = 7
        ElseIf ComboBoxMes.SelectedItem = "Agosto" Then
            Mes = 8
        ElseIf ComboBoxMes.SelectedItem = "Septiembre" Then
            Mes = 9
        ElseIf ComboBoxMes.SelectedItem = "Octubre" Then
            Mes = 10
        ElseIf ComboBoxMes.SelectedItem = "Noviembre" Then
            Mes = 11
        ElseIf ComboBoxMes.SelectedItem = "Diciembre" Then
            Mes = 12
        ElseIf ComboBoxMes.SelectedItem = "TODOS" Then
            Mes = 99
        End If

        'Muestra resultado de consulta segun parametros ingresados
        Try

            If CmbAgteVta.SelectedValue = 999 And ComboBoxMes.SelectedItem = "TODOS" Then
                cargar_registros("Select * from SC_Objetivos where anio = " & TextBoxAño.Text _
                             & "order by anio DESC, mes DESC, groupname DESC, slpname", DGObjetivos)

            ElseIf CmbAgteVta.SelectedValue = 999 And Not ComboBoxMes.SelectedItem = "TODOS" Then
                cargar_registros("Select * from SC_Objetivos where mes =" & Mes & " and anio = " & TextBoxAño.Text _
                            & "order by anio DESC, mes DESC, groupname DESC, slpname", DGObjetivos)

            ElseIf CmbAgteVta.SelectedValue <> 999 And ComboBoxMes.SelectedItem <> "TODOS" Then
                cargar_registros("Select * from SC_Objetivos where mes =" & Mes & " and anio = " & TextBoxAño.Text & " and slpcode = " & CmbAgteVta.SelectedValue _
                             & "order by anio DESC, mes DESC, groupname DESC, slpname", DGObjetivos)

            ElseIf ComboBoxMes.SelectedItem = "TODOS" And CmbAgteVta.SelectedValue <> 999 Then
                cargar_registros("Select * from SC_Objetivos where anio = " & TextBoxAño.Text & " and slpcode = " & CmbAgteVta.SelectedValue _
                             & "order by anio DESC, mes DESC, groupname DESC, slpname", DGObjetivos)

            End If

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            MsgBox("No hay registros de este periodo")
        End Try

    End Sub


    'BOTON CREAR REGISTRO 
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles BtnAgregar.Click


        If CmbAgteVta.SelectedValue <> 999 And ComboBoxMes.Text <> "TODOS" And (TBObjetivo.Text = "" Or TBObjetivo.Text = 0.0) Then

            MsgBox("Ingrese objetivo para continuar", _
                   MsgBoxStyle.Exclamation, _
                   "Ingrese Objetivo")

        Else

            'If (MessageBox.Show( _
            '                "¿Confirma que desea generar el siguiente registro?" + Chr(13) + Chr(13) & "Agente: " & CmbAgteVta.Text & _
            '                "   Mes: " & ComboBoxMes.Text & "   Año: " & TextBoxAño.Text & "   Objetivo: $" & TBObjetivo.Text, _
            '                 "Generar Registro", MessageBoxButtons.YesNo, _
            '                MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

            If (MessageBox.Show( _
                            "¿Confirma que desea generar el siguiente registro?" + Chr(13) + Chr(13) & CmbAgteVta.Text & _
                            "    " & ComboBoxMes.Text & "    " & TextBoxAño.Text & "    $" & TBObjetivo.Text, _
                             "Generar Registro", MessageBoxButtons.YesNo, _
                            MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
                GenerarObjetivos()
            End If
        End If

    End Sub

    ''BOTON ELIMINAR
    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    '    'If (MessageBox.Show("¿Guardar Cambios?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
    '    '    Actualizar(False)
    '    'End If
    '    Try

    '        Dim i As Integer
    '        Dim agente As String
    '        Dim anio As String
    '        Dim mes As Integer

    '        Dim objetivo As Decimal

    '        i = DGObjetivos.CurrentRow.Index
    '        agente = DGObjetivos.Item(1, i).Value.ToString
    '        anio = DGObjetivos.Item(5, i).Value.ToString
    '        mes = DGObjetivos.Item(4, i).Value.ToString
    '        objetivo = DGObjetivos.Item(6, i).Value.ToString

    '        If (MessageBox.Show("¿Confirma que desea eliminar este registro?" + Chr(13) + Chr(13) & agente & "   " & mes & _
    '                            "   " & anio & "   $" & objetivo, _
    '                            "Eliminar", _
    '                            MessageBoxButtons.YesNo, _
    '                           MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

    '            If Not bs.Current Is Nothing Then
    '                ' eliminar  
    '                bs.RemoveCurrent()

    '                'Guardar los cambios y recargar  
    '                Actualizar()
    '            Else
    '                MsgBox("No hay un registro actual para eliminar", _
    '                       MsgBoxStyle.Exclamation, _
    '                       "Eliminar")
    '            End If

    '        End If

    '    Catch ex As Exception
    '        MsgBox("No hay un registro actual para eliminar", _
    '                   MsgBoxStyle.Exclamation, _
    '                   "Eliminar")
    '    End Try

    'End Sub


    Private Sub GenerarObjetivos()
        Dim Mes As Integer
        If ComboBoxMes.SelectedItem = "Enero" Then
            Mes = 1
        ElseIf ComboBoxMes.SelectedItem = "Febrero" Then
            Mes = 2
        ElseIf ComboBoxMes.SelectedItem = "Marzo" Then
            Mes = 3
        ElseIf ComboBoxMes.SelectedItem = "Abril" Then
            Mes = 4
        ElseIf ComboBoxMes.SelectedItem = "Mayo" Then
            Mes = 5
        ElseIf ComboBoxMes.SelectedItem = "Junio" Then
            Mes = 6
        ElseIf ComboBoxMes.SelectedItem = "Julio" Then
            Mes = 7
        ElseIf ComboBoxMes.SelectedItem = "Agosto" Then
            Mes = 8
        ElseIf ComboBoxMes.SelectedItem = "Septiembre" Then
            Mes = 9
        ElseIf ComboBoxMes.SelectedItem = "Octubre" Then
            Mes = 10
        ElseIf ComboBoxMes.SelectedItem = "Noviembre" Then
            Mes = 11
        ElseIf ComboBoxMes.SelectedItem = "Diciembre" Then
            Mes = 12
        ElseIf ComboBoxMes.SelectedItem = "TODOS" Then
            Mes = 99
        End If

        If CmbAgteVta.SelectedValue = 999 And Mes <> 99 Then
            '---Inserta objetivos en 0 de todos los AGENTES 
            '----PROCEDIMIENTO

            Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
            Dim command As New SqlCommand("SCObjetivos", conexion)
            Try
        command.CommandType = CommandType.StoredProcedure
        command.Parameters.AddWithValue("@Mes", Mes)
                command.Parameters.AddWithValue("@Año", TextBoxAño.Text)
                command.Parameters.AddWithValue("@Objetivo", TBObjetivo.Text)

        conexion.Open()
                command.ExecuteNonQuery()
        cargar_registros("Select * from SC_Objetivos order by anio DESC, mes DESC, groupname desc, SLPNAME", DGObjetivos)
      Catch ex As Exception
                'MessageBox.Show(ex.Message)
                MsgBox("Estos registros ya existen")
            Finally
                conexion.Dispose()
                command.Dispose()
            End Try

            'MsgBox("el valor de mes es: " & Mes)


        ElseIf CmbAgteVta.SelectedValue <> 999 And Mes <> 99 And TBObjetivo.Text <> 0.0 Then
            '----PROCEDIMIENTO para ingresar objetivo de manera individual

            Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
            Dim command As New SqlCommand("InsObjSC", conexion)
            Try
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add("@slpcode", SqlDbType.Int).Value = Me.CmbAgteVta.SelectedValue
                command.Parameters.AddWithValue("@Mes", Mes)
                command.Parameters.AddWithValue("@Año", TextBoxAño.Text)
                command.Parameters.Add("@objetivo", SqlDbType.Decimal).Value = Convert.ToDecimal(TBObjetivo.Text)



                conexion.Open()
                command.ExecuteNonQuery()
                cargar_registros("Select * from SC_Objetivos order by anio DESC, mes DESC, groupname desc, SLPNAME", DGObjetivos)
            Catch ex As Exception
                'MessageBox.Show(ex.Message)
                'MessageBox.Show("Este registro ya existe, verifique")
                MessageBox.Show("Este registro ya existe!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                conexion.Dispose()
                command.Dispose()
            End Try

            CmbAgteVta.SelectedValue = 999
            TBObjetivo.Text = Format(0, "#,##0.00")



        ElseIf Mes = 99 And CmbAgteVta.SelectedValue = 999 Then

            Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
            Dim command As New SqlCommand("InsPorMesesAgentes", conexion)
            Try
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@mes", Mes)
                command.Parameters.AddWithValue("@anio", TextBoxAño.Text)
                conexion.Open()
                command.ExecuteNonQuery()
                cargar_registros("Select * from SC_Objetivos order by anio DESC, mes DESC, groupname desc, SLPNAME", DGObjetivos)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                'MsgBox("Este registro ya existe, verifique")
            Finally
                conexion.Dispose()
                command.Dispose()
            End Try

            CmbAgteVta.SelectedValue = 999
            TBObjetivo.Text = Format(0, "#,##0.00")




        ElseIf Mes = 99 And CmbAgteVta.SelectedValue <> 999 Then

            Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
            Dim command As New SqlCommand("InsMesesAgente", conexion)
            Try
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@mes", Mes)
                command.Parameters.AddWithValue("@anio", TextBoxAño.Text)
                command.Parameters.AddWithValue("@agente", CmbAgteVta.SelectedValue)
                conexion.Open()
                command.ExecuteNonQuery()
                cargar_registros("Select * from SC_Objetivos order by anio DESC, mes DESC, groupname desc,SLPNAME", DGObjetivos)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                'MsgBox("Este registro ya existe, verifique")
            Finally
                conexion.Dispose()
                command.Dispose()
            End Try

            CmbAgteVta.SelectedValue = 999
            TBObjetivo.Text = Format(0, "#,##0.00")

        End If


    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles BActualizar.Click

        'If CmbAgteVta.SelectedValue = 999 And TBObjetivo.Text = 0.0 Then
        Actualizar()
        'Else

        'End If

    End Sub



    'Private Sub Button3_Click_2(sender As Object, e As EventArgs) Handles Button3.Click

    '    If (MessageBox.Show( _
    '                        "¿Confirma que desea ELIMINAR TODO?", _
    '                         "Eliminar Todo", MessageBoxButtons.YesNo, _
    '                        MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

    '        Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
    '        Dim command As New SqlCommand("SPSCTruncate", conexion)
    '        Try
    '            command.CommandType = CommandType.StoredProcedure
    '            conexion.Open()
    '            command.ExecuteNonQuery()
    '            cargar_registros("Select * from SC_Objetivos order by anio DESC, mes DESC, groupname desc,SLPNAME", DGObjetivos)
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '            'MsgBox("Este registro ya existe, verifique")
    '        Finally
    '            conexion.Dispose()
    '            command.Dispose()
    '        End Try
    '    End If
    'End Sub

    Private Sub SMBorrarLinea_Click(sender As Object, e As EventArgs) Handles SMBorrarLinea.Click
        'If (MessageBox.Show("¿Guardar Cambios?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
        '    Actualizar(False)
        'End If
        Try

            Dim i As Integer
            Dim agente As String
            Dim anio As String
            Dim mes As Integer

            Dim objetivo As Decimal

            i = DGObjetivos.CurrentRow.Index
            agente = DGObjetivos.Item(1, i).Value.ToString
            anio = DGObjetivos.Item(5, i).Value.ToString
            mes = DGObjetivos.Item(4, i).Value.ToString
            objetivo = DGObjetivos.Item(6, i).Value.ToString

            If (MessageBox.Show("¿Confirma que desea eliminar este registro?" + Chr(13) + Chr(13) & agente & "   " & mes & _
                                "   " & anio & "   $" & objetivo, _
                                "Eliminar", _
                                MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

                If Not bs.Current Is Nothing Then
                    ' eliminar  
                    bs.RemoveCurrent()

                    'Guardar los cambios y recargar  
                    Actualizar()
                Else
                    MsgBox("No hay un registro actual para eliminar", _
                           MsgBoxStyle.Exclamation, _
                           "Eliminar")
                End If

            End If

        Catch ex As Exception
            MsgBox("No hay un registro actual para eliminar", _
                       MsgBoxStyle.Exclamation, _
                       "Eliminar")
        End Try
    End Sub

    Private Sub SMBorrarTodo_Click(sender As Object, e As EventArgs) Handles SMBorrarTodo.Click
        If (MessageBox.Show( _
                          "¿Confirma que desea ELIMINAR TODO?", _
                           "Eliminar Todo", MessageBoxButtons.YesNo, _
                          MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

            Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
            Dim command As New SqlCommand("SPSCTruncate", conexion)
            Try
                command.CommandType = CommandType.StoredProcedure
                conexion.Open()
                command.ExecuteNonQuery()
                cargar_registros("Select * from SC_Objetivos order by anio DESC, mes DESC, groupname desc,SLPNAME", DGObjetivos)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                'MsgBox("Este registro ya existe, verifique")
            Finally
                conexion.Dispose()
                command.Dispose()
            End Try
        End If
    End Sub

    Private Sub DGObjetivos_CurrentCellChanged(sender As Object, e As EventArgs) Handles DGObjetivos.CurrentCellChanged
        'BActualizar.Visible = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            Dim i As Integer
            Dim agente As String
            Dim anio As String
            Dim mes As Integer

            Dim objetivo As Decimal

            i = DGObjetivos.CurrentRow.Index
            agente = DGObjetivos.Item(1, i).Value.ToString
            anio = DGObjetivos.Item(5, i).Value.ToString
            mes = DGObjetivos.Item(4, i).Value.ToString
            objetivo = DGObjetivos.Item(6, i).Value.ToString

            If (MessageBox.Show("¿Confirma que desea eliminar este registro?" + Chr(13) + Chr(13) & agente & "   " & mes & _
                                "   " & anio & "   $" & objetivo, _
                                "Eliminar", _
                                MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

                If Not bs.Current Is Nothing Then
                    ' eliminar  
                    bs.RemoveCurrent()

                    'Guardar los cambios y recargar  
                    Actualizar()
                Else
                    MsgBox("No hay un registro actual para eliminar", _
                           MsgBoxStyle.Exclamation, _
                           "Eliminar")
                End If

            End If

        Catch ex As Exception
            MsgBox("No hay un registro actual para eliminar", _
                       MsgBoxStyle.Exclamation, _
                       "Eliminar")
        End Try
    End Sub
End Class



