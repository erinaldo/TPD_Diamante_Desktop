
Option Explicit On
'Option Strict On
Imports System.Data.SqlClient
Imports System.Data



Public Class LineasObjetivoIngresarObj

    ' Espacios de nombres  
    ' ''''''''''''''''''''''''''''''''''''''''' 

    Dim DvTotales As New DataView
    Dim DvAgentes As New DataView
    Dim DvAgentes2 As New DataView
    Dim DvLineas As New DataView


    'BindingSource  
    Private WithEvents bs As New BindingSource

    ' Adaptador de datos sql  
    Private SqlDataAdapter As SqlDataAdapter

    ' Cadena de conexión  
    Private Const cs As String = "Server=SERVIDORSAP; Database=TPM; User id=sa; Password=SD1amany3S"

    ' flag  
    Private bEdit As Boolean

    Public StrCon As String = "Data Source=SERVIDORSAP;Initial Catalog=SBO_TPD;User Id=SA;Password=SD1amany3S;"
    Public StrTpm As String = "Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;"
    'Public StrCon As String = "Data Source=SERVIDORSAP;Initial Catalog=SBO_TPD;User Id=SA;Password=SD1amany3S;"

    ' actualizar los cambios al salir  
    ' ''''''''''''''''''''''''''''''''''''''''  
    Private Sub LineasObjetivoIngresarObjetivos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If bEdit Then

            'preguntar si se desea guardar  

            'If (MsgBox( _
            '        "Guardar cambios ?", _
            '         MsgBoxStyle.YesNo, _
            '        "guardar")) = MsgBoxResult.Yes Then

            '    Actualizar(False)
            'End If

            If (MessageBox.Show("¿Desea guardar ambios?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
                Actualizar(False)
            End If

        End If
    End Sub


    Sub LlenaCmbLineas()
        '------Obtener LINEAS
        Dim ConsutaLista As String

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

        ConsutaLista = "select ItmsGrpCod, ItmsGrpNam from OITB "

        Dim daLineas As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)
        Dim DSetTablas As New DataSet

        daLineas.Fill(DSetTablas, "Lineas")

            'Dim filaLineas As Data.DataRow

            ''Asignamos a fila la nueva Row(Fila)del Dataset
            'filaLineas = DSetTablas.Tables("Lineas").NewRow

            ''Agregamos los valores a los campos de la tabla
            'filaLineas("ItmsGrpNam") = "TODAS"
            'filaLineas("ItmsGrpCod") = 999


            ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            'DSetTablas.Tables("Lineas").Rows.Add(filaLineas)

            

        DvLineas.Table = DSetTablas.Tables("Lineas")
        Me.CmbLineas.DataSource = DSetTablas.Tables("Lineas")
        Me.CmbLineas.DisplayMember = "ItmsGrpNam"
            Me.CmbLineas.ValueMember = "ItmsGrpCod"
            'Me.CmbLineas.SelectedValue = 999

        End Using


    End Sub


    Sub LlenaCmbAgte()
        Dim ConsutaLista As String


        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
            Dim DSetTablas As New DataSet
            ConsutaLista = "SELECT T0.slpcode,T0.slpname,T1.GroupCode FROM OSLP T0 "
            ConsutaLista &= "INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
            ConsutaLista &= "WHERE T1.CbrGralAdicional = 'N'  ORDER BY slpcode, slpname "



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

        ComboBoxMes.SelectedIndex = 0

        TBObjetivo.Text = Format(0, " 0,##0.#0")

        TextBoxAño.Text = Year(Now)

    End Sub


    Private Sub Form1_Load( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Actualizar()

        LlenaCmbAgte()

        LlenaCmbLineas()

        LLenaMes()

        ' propiedades del datagrid  
        ' '''''''''''''''''''''''''''''''''''''  
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

        End With



        btn_first.Text = "<<"
        btn_Previous.Text = "<"
        btn_next.Text = ">"
        btn_last.Text = ">>"

        ' cagar los datos  -----Select slpname, groupname,anio,mes,objetivo from SC_Objetivos
        cargar_registros("Select slpcode, slpname, groupname, mes, anio, linea, objetivo from LO_Objetivos order by anio, mes,linea,groupcode", DGObjetivos)

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
        Try
            If Not bs.DataSource Is Nothing Then
                SqlDataAdapter.Update(CType(bs.DataSource, DataTable))
                If bCargar Then
                    cargar_registros("Select slpcode, slpname, groupname, mes, anio, linea, objetivo from LO_Objetivos order by anio, mes,linea,groupcode", DGObjetivos)
                End If
            End If
        Catch exSql As SqlException
            MsgBox(exSql.Message.ToString)
        Catch ex As Exception
            MsgBox(ex.Message.ToString)

        End Try
       
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

    Private Sub DataGridView1_CellEndEdit( _
        ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
            Handles DGObjetivos.CellEndEdit
        bEdit = True

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

            'If CmbAgteVta.SelectedValue = 999 And ComboBoxMes.SelectedItem = "TODOS" And CmbLineas.SelectedValue = 99 Then
            If CmbAgteVta.SelectedValue = 999 And ComboBoxMes.SelectedItem = "TODOS" Then

                cargar_registros("Select slpcode, slpname, groupname, mes, anio, linea, objetivo from LO_Objetivos where anio = " _
                             & TextBoxAño.Text & " and linea ='" & CmbLineas.Text _
                             & "' order by anio, mes,linea,groupcode", DGObjetivos)

            ElseIf CmbAgteVta.SelectedValue = 999 And Not ComboBoxMes.SelectedItem = "TODOS" Then
                cargar_registros("Select slpcode, slpname, groupname, mes, anio, linea, objetivo from LO_Objetivos where mes =" _
                            & Mes & " and anio = " & TextBoxAño.Text & " and linea ='" & CmbLineas.Text _
                            & "' order by anio, mes,linea,groupcode", DGObjetivos)

            ElseIf CmbAgteVta.SelectedValue <> 999 And ComboBoxMes.SelectedItem <> "TODOS" Then
                cargar_registros("Select slpcode, slpname, groupname, mes, anio, linea, objetivo from LO_Objetivos where mes =" _
                                 & Mes & " and anio = " & TextBoxAño.Text & " and slpcode = " & CmbAgteVta.SelectedValue & " and linea ='" & CmbLineas.Text _
                             & "' order by anio, mes,linea,groupcode", DGObjetivos)

            ElseIf ComboBoxMes.SelectedItem = "TODOS" And CmbAgteVta.SelectedValue <> 999 Then
                cargar_registros("Select slpcode, slpname, groupname, mes, anio, linea, objetivo from LO_Objetivos where anio = " _
                                 & TextBoxAño.Text & " and slpcode = " & CmbAgteVta.SelectedValue & " and linea ='" & CmbLineas.Text _
                             & "' order by anio, mes,linea,groupcode", DGObjetivos)

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

            If (MessageBox.Show( _
                           "¿Confirma que desea generar el siguiente registro?" + Chr(13) + Chr(13) & CmbAgteVta.Text & _
                           "    " & ComboBoxMes.Text & "    " & TextBoxAño.Text & "    " & CmbLineas.Text & "    " & "    $" & TBObjetivo.Text, _
                            "Generar Registro", MessageBoxButtons.YesNo, _
                           MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
                GenerarObjetivos()
            End If
        End If

    End Sub

    'BOTON ELIMINAR
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'If (MessageBox.Show("¿Guardar Cambios?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
        '    Actualizar(False)
        'End If

        Try

        Dim i As Integer
        Dim agente As String
        Dim anio As String
            Dim mes As Integer
            Dim linea As String
        Dim objetivo As Decimal

        i = DGObjetivos.CurrentRow.Index
        agente = DGObjetivos.Item(1, i).Value.ToString
        anio = DGObjetivos.Item(5, i).Value.ToString
        mes = DGObjetivos.Item(4, i).Value.ToString
            linea = DGObjetivos.Item(5, i).Value.ToString
            objetivo = DGObjetivos.Item(6, i).Value.ToString




            If (MessageBox.Show("¿Confirma que desea eliminar este registro?" + Chr(13) + Chr(13) & agente & "   " & mes & _
                                "   " & anio & "    " & linea & "    " & "   $" & objetivo, _
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

            Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
            Dim command As New SqlCommand("LObjObjetivos", conexion)
            Try

                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Mes", Mes)
                command.Parameters.AddWithValue("@Año", TextBoxAño.Text)
                command.Parameters.AddWithValue("@Linea", CmbLineas.Text)

                conexion.Open()
                command.ExecuteNonQuery()
                cargar_registros("Select slpcode, slpname, groupname, mes, anio, linea, objetivo from LO_Objetivos order by anio, mes,linea,groupcode", DGObjetivos)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                'MsgBox("Estos registros ya existen")
            Finally
                conexion.Dispose()
                command.Dispose()
            End Try

            'MsgBox("el valor de mes es: " & Mes)

        End If

        If CmbAgteVta.SelectedValue <> 999 And Mes <> 99 And TBObjetivo.Text <> 0.0 Then
            '----PROCEDIMIENTO para ingresar objetivo de manera individual

            Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
            Dim command As New SqlCommand("InsObjLO", conexion)
            Try
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add("@slpcode", SqlDbType.Int).Value = Me.CmbAgteVta.SelectedValue
                command.Parameters.AddWithValue("@Mes", Mes)
                command.Parameters.AddWithValue("@Anio", TextBoxAño.Text)
                command.Parameters.Add("@objetivo", SqlDbType.Decimal).Value = Convert.ToDecimal(TBObjetivo.Text)
                command.Parameters.AddWithValue("@Linea", CmbLineas.Text)


                conexion.Open()
                command.ExecuteNonQuery()
                cargar_registros("Select slpcode, slpname, groupname, mes, anio, linea, objetivo from LO_Objetivos order by anio, mes,linea,groupcode", DGObjetivos)
            Catch ex As Exception
                'MessageBox.Show(ex.Message)
                MsgBox("Este registro ya existe, verifique")
            Finally
                conexion.Dispose()
                command.Dispose()
            End Try

            CmbAgteVta.SelectedValue = 999
            TBObjetivo.Text = Format(0, "#,##0.#0")


        ElseIf CmbAgteVta.SelectedValue <> 999 And Mes <> 99 And TBObjetivo.Text = 0.0 Then

            'MsgBox("Ingrese Objetivo")
            MessageBox.Show("Ingrese Objetivo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If

        If Mes = 99 And CmbAgteVta.SelectedValue = 999 Then

            Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
            Dim command As New SqlCommand("InsPorMesesAgentesLO", conexion)
            Try
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@mes", Mes)
                command.Parameters.AddWithValue("@anio", TextBoxAño.Text)
                command.Parameters.AddWithValue("@linea", CmbLineas.Text)
                conexion.Open()
                command.ExecuteNonQuery()
                cargar_registros("Select slpcode, slpname, groupname, mes, anio, linea, objetivo from LO_Objetivos order by anio, mes,linea,groupcode", DGObjetivos)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                'MsgBox("Este registro ya existe, verifique")
            Finally
                conexion.Dispose()
                command.Dispose()
            End Try

            CmbAgteVta.SelectedValue = 999
            TBObjetivo.Text = Format(0, "#,##0.#0")

        End If
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click

        'If CmbAgteVta.SelectedValue = 999 And TBObjetivo.Text = 0.0 Then
        Actualizar()
        'Else

        'End If

    End Sub



End Class
