
Option Explicit On
'Option Strict On
Imports System.Data.SqlClient

Public Class LOIngObj
    Public conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
    Dim Dvtotales As New DataView

    'data view para mostrar combobox
    Dim Dvagentes As New DataView

    'data view para mostrar datagrid
    Dim DvagentesObj As New DataView
    Sub LlenaCmbAgte()
        Dim ConsutaLista As String


        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
            Dim DSetTablas As New DataSet
            ConsutaLista = "SELECT T0.slpcode,T0.slpname,T1.GroupCode FROM OSLP T0 "
            ConsutaLista &= "INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
            ConsutaLista &= "WHERE T1.CbrGralAdicional = 'N' AND T0.SLPCODE <> 1 ORDER BY slpcode, slpname "



            Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


            daAgte.Fill(DSetTablas, "Agentes")

            Dim filaAgte As Data.DataRow
            Dim filaAgte1 As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaAgte = DSetTablas.Tables("Agentes").NewRow
            filaAgte1 = DSetTablas.Tables("Agentes").NewRow


            'Agregamos los valores a los campos de la tabla

            filaAgte1("slpname") = "GENERAL"
            filaAgte1("slpcode") = 998
            filaAgte1("GroupCode") = 998

            filaAgte("slpname") = "Seleccione Agente"
            filaAgte("slpcode") = 999
            filaAgte("GroupCode") = 999



            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet

            DSetTablas.Tables("Agentes").Rows.Add(filaAgte1)
            DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

            Dvagentes.Table = DSetTablas.Tables("Agentes")

            Me.CBAgente.DataSource = Dvagentes
            Me.CBAgente.DisplayMember = "slpname"
            Me.CBAgente.ValueMember = "slpcode"
            Me.CBAgente.SelectedValue = 999
        End Using
    End Sub
    Sub LLenaMes()

        'elementos del combobox
        CBMES.Items.Add("Enero")
        CBMES.Items.Add("Febrero")
        CBMES.Items.Add("Marzo")
        CBMES.Items.Add("Abril")
        CBMES.Items.Add("Mayo")
        CBMES.Items.Add("Junio")
        CBMES.Items.Add("Julio")
        CBMES.Items.Add("Agosto")
        CBMES.Items.Add("Septiembre")
        CBMES.Items.Add("Octubre")
        CBMES.Items.Add("Noviembre")
        CBMES.Items.Add("Diciembre")
        'ComboBoxMes.Items.Add("Seleccione mes")

        CBMES.SelectedIndex = 0

        TBobjetivo.Text = Format(0, " #,##0.00")
        TBobjetivo.TextAlign = HorizontalAlignment.Right



        CBAño.Text = Year(Now)

    End Sub


    Private Sub LOIngObj_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LLenaMes()
        LlenaCmbAgte()
        Buscar_NotasC()

        'If CheckedListBox1.CheckedItems.Count > 0 And CBAgente.Text <> "GENERAL" Or CBAgente.Text <> "Seleccione Agente" Then

        '    BIngObj.Visible = True

        'End If
        'CBTB()

        'Dim DSetTablas As New DataSet

    End Sub

    Private Sub Checked_Changed(ByVal sender As Object, ByVal e As EventArgs)

        If CStr(CType(sender, CheckBox).CheckState) = True Then
            MsgBox("Check Activado " & CStr(CType(sender, CheckBox).Name))

        End If

    End Sub

    Sub Buscar_NotasC()

        Dim Mes As Integer
        If CBMes.SelectedItem = "Enero" Then
            Mes = 1
        ElseIf CBMes.SelectedItem = "Febrero" Then
            Mes = 2
        ElseIf CBMes.SelectedItem = "Marzo" Then
            Mes = 3
        ElseIf CBMes.SelectedItem = "Abril" Then
            Mes = 4
        ElseIf CBMes.SelectedItem = "Mayo" Then
            Mes = 5
        ElseIf CBMes.SelectedItem = "Junio" Then
            Mes = 6
        ElseIf CBMes.SelectedItem = "Julio" Then
            Mes = 7
        ElseIf CBMes.SelectedItem = "Agosto" Then
            Mes = 8
        ElseIf CBMes.SelectedItem = "Septiembre" Then
            Mes = 9
        ElseIf CBMes.SelectedItem = "Octubre" Then
            Mes = 10
        ElseIf CBMes.SelectedItem = "Noviembre" Then
            Mes = 11
        ElseIf CBMes.SelectedItem = "Diciembre" Then
            Mes = 12
        ElseIf CBMes.SelectedItem = "TODOS" Then
            Mes = 99
        End If

        'Dim vDiasMes As Integer
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Dim cmd2 As SqlCommand = Nothing
        'Dim vDiasTrans As Integer

        Dim cmd3 As SqlCommand = Nothing
        Dim cmd4 As SqlCommand = Nothing

        Try
            cnn = New SqlConnection(StrTpm)
            cnn.Open()


            cmd4 = New SqlCommand("LOMuestraObjetivos", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@anio", CBAño.Text)
            cmd4.Parameters.AddWithValue("@mes", Mes)



            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn

            ''--------------------------------------------
            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "Agentes"
            DsVtas.Tables(1).TableName = "Totales"
            'DsVtas.Tables(2).TableName = "VtaCltes"

            DvagentesObj.Table = DsVtas.Tables("Agentes")
            Dvtotales.Table = DsVtas.Tables("Totales")
            ''DvClientes.Table = DsVtas.Tables("VtaCltes")


            DGObjAge.DataSource = DvagentesObj

            DGLOIngGen.DataSource = Dvtotales
            '.DataSource = Dvtotales


            With DGObjAge
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .DefaultCellStyle.BackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                '.RowsDefaultCellStyle = 
                ' Establecer el origen de datos para el DataGridview  
                'DGObjetivos.DataSource = bs
                .ReadOnly = True
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 15
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                'Color de linea del grid

                'centrar encabezados del datagrid 
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                Try

                    'Catch ex As Exception

                    'End Try


                    .Columns(0).HeaderText = "Vendedor"
                    .Columns(0).Width = 160
                    .Columns(1).HeaderText = "Mes"
                    .Columns(1).Width = 30
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(2).HeaderText = "Año"
                    .Columns(2).Width = 60
                    '.Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(3).HeaderText = "Objetivo"
                    .Columns(3).Width = 80
                    .Columns(3).DefaultCellStyle.Format = "$ ###,##0,##0"
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                Catch ex As Exception

                End Try
            End With

            With DGLOIngGen
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .DefaultCellStyle.BackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                '.RowsDefaultCellStyle = 
                ' Establecer el origen de datos para el DataGridview  
                'DGObjetivos.DataSource = bs
                .ReadOnly = True
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = True
                .RowHeadersWidth = 15
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                'Color de linea del grid

                'centrar encabezados del datagrid 
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                Try

                    .Columns(0).HeaderText = "Mes"
                    .Columns(0).Width = 30
                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(1).HeaderText = "Año"
                    .Columns(1).Width = 60
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(2).HeaderText = "Objetivo"
                    .Columns(2).Width = 80
                    .Columns(2).DefaultCellStyle.Format = "$ ###,##0,##0"
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                Catch ex As Exception

                End Try
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
            'MsgBox("No existen ventas de este día")
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

    End Sub

    Private Sub DGLOIngGen_CurrentCellChanged(sender As Object, e As EventArgs)
        Try
            DvagentesObj.RowFilter = "Mes = " & DGLOIngGen.Item(0, DGLOIngGen.CurrentRow.Index).Value.ToString & " AND Año = " & DGLOIngGen.Item(1, DGLOIngGen.CurrentRow.Index).Value.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BSave_Click_1(sender As Object, e As EventArgs) Handles BSave.Click
        Dim i As Integer

        Dim Mes As Integer
        If CBMes.SelectedItem = "Enero" Then
            Mes = 1
        ElseIf CBMes.SelectedItem = "Febrero" Then
            Mes = 2
        ElseIf CBMes.SelectedItem = "Marzo" Then
            Mes = 3
        ElseIf CBMes.SelectedItem = "Abril" Then
            Mes = 4
        ElseIf CBMes.SelectedItem = "Mayo" Then
            Mes = 5
        ElseIf CBMes.SelectedItem = "Junio" Then
            Mes = 6
        ElseIf CBMes.SelectedItem = "Julio" Then
            Mes = 7
        ElseIf CBMes.SelectedItem = "Agosto" Then
            Mes = 8
        ElseIf CBMes.SelectedItem = "Septiembre" Then
            Mes = 9
        ElseIf CBMes.SelectedItem = "Octubre" Then
            Mes = 10
        ElseIf CBMes.SelectedItem = "Noviembre" Then
            Mes = 11
        ElseIf CBMes.SelectedItem = "Diciembre" Then
            Mes = 12
        ElseIf CBMes.SelectedItem = "TODOS" Then
            Mes = 99
        End If

        If CBAgente.Text = "Seleccione Agente" Then

            MsgBox("No ha seleccionado ningun agente.")

        ElseIf CBAgente.Text = "GENERAL" Then



            If CheckedListBox1.CheckedItems.Count > 0 Then

                If (MessageBox.Show( _
                "¿Confirma que desea generar este registro?" + Chr(13) + Chr(13) & "General" & "   " & CBMes.Text & _
                "   " & CBAño.Text & "    $" & TBobjetivo.Text, _
                "Generar Registro", MessageBoxButtons.YesNo, _
                MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

                    Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
                    Dim command As New SqlCommand("SPLOObjGen", conexion)
                    Try

                        command.CommandType = CommandType.StoredProcedure

                        command.Parameters.AddWithValue("@mes", Mes)
                        command.Parameters.AddWithValue("@anio", CBAño.Text)
                        'command.Parameters.AddWithValue("@linea", CheckedListBox1.Items(i))
                        command.Parameters.AddWithValue("@Objetivo", TBobjetivo.Text)


                        conexion.Open()
                        command.ExecuteNonQuery()
                        'cargar_registros("SELECT anio, mes, objetivo FROM LHObjGen ORDER BY anio DESC,mes ASC ", DGObjGen)
                        Buscar_NotasC()

                        For i = 0 To CheckedListBox1.Items.Count - 1
                            If (CheckedListBox1.GetItemChecked(i)) Then
                                'MessageBox.Show(CheckedListBox1.Items(i))
                                Dim conexion2 As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
                                Dim command2 As New SqlCommand("SPLOObjGen2", conexion2)

                                command2.CommandType = CommandType.StoredProcedure

                                command2.Parameters.AddWithValue("@mes", Mes)
                                command2.Parameters.AddWithValue("@anio", CBAño.Text)
                                command2.Parameters.AddWithValue("@linea", CheckedListBox1.Items(i))
                                'command.Parameters.AddWithValue("@Objetivo", TBobjetivo.Text)


                                conexion2.Open()
                                command2.ExecuteNonQuery()
                                'cargar_registros("SELECT anio, mes, objetivo FROM LHObjGen ORDER BY anio DESC,mes ASC ", DGObjGen)
                                Buscar_NotasC()
                            End If
                        Next
                        CBAgente.SelectedValue = 999
                        TBobjetivo.Text = "0.0"

                        For i = 0 To CheckedListBox1.Items.Count - 1
                            CheckedListBox1.SetItemChecked(i, False)
                        Next


                    Catch ex As Exception
                        'MessageBox.Show(ex.Message)
                        MessageBox.Show("Este periodo ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    Finally
                        conexion.Dispose()
                        command.Dispose()
                    End Try
                    'Else
                    '    MsgBox("Seleccione al menos una linea.")
                End If
            Else
                MsgBox("Seleccione al menos una linea.")

            End If
    

        ElseIf CBAgente.Text <> "GENERAL" Or CBAgente.Text <> "Seleccione Agente" And TBobjetivo.Text <> "0.0" And CheckedListBox1.CheckedItems.Count > 0 Then

            If (MessageBox.Show( _
            "¿Confirma que desea generar este registro?" + Chr(13) + Chr(13) & CBAgente.Text & "   " & CBMes.Text & _
            "   " & CBAño.Text & "    $" & TBobjetivo.Text, _
            "Generar Registro", MessageBoxButtons.YesNo, _
            MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then


                

                'Dim objetivo As String
                'Dim a As Integer
                'For a = 0 To ListBox1.Items.Count - 1
                '    objetivo = ListBox1.Items(a)
                '    MsgBox(objetivo)
                'Next
                Dim cont As Integer = 0
                For i = 0 To CheckedListBox1.Items.Count - 1

                    If (CheckedListBox1.GetItemChecked(i)) Then

                        Dim linea As String
                        Dim objetivo As String

                        linea = CheckedListBox1.Items(i)
                        'MsgBox(linea)
                        objetivo = ListBox1.Items(cont)
                        'MsgBox(objetivo)

                        Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
                        'Dim command As New SqlCommand("SPLHObjInd", conexion)
                        Dim command As New SqlCommand("LOObjAgte", conexion)

                        Try
                            command.CommandType = CommandType.StoredProcedure
                            command.Parameters.Add("@slpcode", SqlDbType.Int).Value = CBAgente.SelectedValue
                            command.Parameters.Add("@slpname", SqlDbType.NVarChar, 100).Value = CBAgente.Text
                            command.Parameters.AddWithValue("@mes", Mes)
                            command.Parameters.AddWithValue("@anio", CBAño.Text)
                            command.Parameters.AddWithValue("@linea", linea)
                            command.Parameters.AddWithValue("@objetivo", objetivo)

                            conexion.Open()
                            command.ExecuteScalar()

                            Buscar_NotasC()

                            cont = cont + 1

                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            'MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Finally
                            conexion.Dispose()
                            command.Dispose()
                        End Try

                    End If
                Next
                
                CBAgente.SelectedValue = 999
                TBobjetivo.Text = "0.0"

                For i = 0 To CheckedListBox1.Items.Count - 1
                    CheckedListBox1.SetItemChecked(i, False)
                Next

                ListBox1.Items.Clear()
                ''Try

                ''    'For i = 0 To CheckedListBox1.Items.Count - 1
                ''    '    If (CheckedListBox1.GetItemChecked(i)) Then
                ''    '        'MessageBox.Show(CheckedListBox1.Items(i))

                ''    '        Dim numer As String
                ''    '        numer = InputBox("Ingrese objetivo para " & CheckedListBox1.Items(i), "Objetivos")


                ''    For i = 0 To CheckedListBox1.Items.Count - 1
                ''        If (CheckedListBox1.GetItemChecked(i)) Then
                ''            'MessageBox.Show(CheckedListBox1.Items(i))
                ''            'Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
                ''            'Dim command3 As New SqlCommand("LOObjAgte", conexion)

                ''            Dim linea As String
                ''            Dim obj As Integer

                ''            linea = CheckedListBox1.Items(i)

                ''            obj = ListBox1.Items(i)

                ''            MsgBox("objetivo de " & linea & obj)

                ''            'command3.CommandType = CommandType.StoredProcedure


                ''            'command3.Parameters.AddWithValue("@slpcode", CBAgente.SelectedValue)
                ''            'command3.Parameters.AddWithValue("@slpname", CBAgente.SelectedItem)
                ''            'command3.Parameters.AddWithValue("@mes", CBMes.Text)
                ''            'command3.Parameters.AddWithValue("@anio", CBAño.Text)
                ''            'command3.Parameters.AddWithValue("@linea", linea)
                ''            'command3.Parameters.AddWithValue("@Objetivo", obj)


                ''            'conexion.Open()
                ''            'command3.ExecuteNonQuery()
                ''            ''cargar_registros("SELECT anio, mes, objetivo FROM LHObjGen ORDER BY anio DESC,mes ASC ", DGObjGen)
                ''            'Buscar_NotasC()
                ''        End If
                ''    Next

                ''Catch ex As Exception
                ''    MessageBox.Show(ex.Message)
                ''End Try
                ' ''Else
                ' ''    MsgBox("Seleccione al menos una linea")

            End If
            End If
    End Sub


    Private Sub DGLOIngGen_CurrentCellChanged_1(sender As Object, e As EventArgs) Handles DGLOIngGen.CurrentCellChanged
        Try

            DvagentesObj.RowFilter = "Mes = " & DGLOIngGen.Item(0, DGLOIngGen.CurrentRow.Index).Value.ToString & " AND Año = " & DGLOIngGen.Item(1, DGLOIngGen.CurrentRow.Index).Value.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGLOIngGen_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGLOIngGen.CellDoubleClick
        Dim frm As New LODetalleTotales()
        frm.Show()
    End Sub

    Private Sub CBAgente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBAgente.SelectedIndexChanged
        If CBAgente.Text = "GENERAL" Then
            BSave.Visible = True
            TBobjetivo.Visible = True
            Label4.Visible = True
            Label5.Visible = True
            BIngObj.Visible = False

        ElseIf CBAgente.Text = "Seleccione Agente" Then
            BIngObj.Visible = False

        Else
            TBobjetivo.Visible = False
            Label4.Visible = False
            Label5.Visible = False
            BIngObj.Visible = True
        End If

    End Sub

    Sub EliminarRegistroGeneral()
        Dim i As Integer
        'Dim agente As String
        Dim anio As String
        Dim mes As Integer

        Dim objetivo As Decimal
        Try


            i = DGLOIngGen.CurrentRow.Index
            anio = DGLOIngGen.Item(1, i).Value.ToString
            mes = DGLOIngGen.Item(0, i).Value.ToString
            objetivo = DGLOIngGen.Item(2, i).Value.ToString

            If (MessageBox.Show("¿Confirma que desea eliminar este registro?" + Chr(13) + Chr(13) & mes & _
                                "   " & anio & "   $" & objetivo, _
                                "Eliminar", _
                                MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

                Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
                'Dim command As New SqlCommand("SPLHObjInd", conexion)
                Dim command As New SqlCommand("LODeleteGen", conexion)

                Try
                    'command = New SqlCommand("LHDelete", conexion)
                    command.CommandType = CommandType.StoredProcedure
                    'command.CommandType = CommandType.StoredProcedure
                    'command.Parameters.Add("@slpcode", SqlDbType.Int).Value = Me.CmbAgteVta.SelectedValue
                    'command.Parameters.Add("@slpname", SqlDbType.NVarChar, 100).Value = agente
                    command.Parameters.AddWithValue("@mes", mes)
                    command.Parameters.AddWithValue("@anio", anio)

                    conexion.Open()
                    command.ExecuteScalar()
                    'cargar_registros("SELECT slpname AS 'Vendedor', mes AS 'Mes', anio AS 'Año', SUM(Objetivo) AS 'Objetivo'  " +
                    '         "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO ", DGObjetivos)

                    Buscar_NotasC()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    'MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Finally
                    conexion.Dispose()
                    command.Dispose()
                End Try

            End If

        Catch ex As Exception
            MsgBox("No hay un registro actual para eliminar", _
                       MsgBoxStyle.Exclamation, _
                       "Eliminar")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        EliminarRegistroGeneral()
    End Sub

    Sub EliminarRegistroAgente()
        Dim i As Integer
        Dim agente As String
        Dim anio As String
        Dim mes As Integer

        Dim objetivo As Decimal
        Try


            i = DGObjAge.CurrentRow.Index
            agente = DGObjAge.Item(0, i).Value.ToString
            anio = DGObjAge.Item(2, i).Value.ToString
            mes = DGObjAge.Item(1, i).Value.ToString
            objetivo = DGObjAge.Item(3, i).Value.ToString

            If (MessageBox.Show("¿Confirma que desea eliminar este registro?" + Chr(13) + Chr(13) & agente & "   " & mes & _
                                "   " & anio & "   $" & objetivo, _
                                "Eliminar", _
                                MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

                Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
                'Dim command As New SqlCommand("SPLHObjInd", conexion)
                Dim command As New SqlCommand("LODelete", conexion)

                Try
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.Add("@slpname", SqlDbType.NVarChar, 100).Value = agente
                    command.Parameters.AddWithValue("@mes", mes)
                    command.Parameters.AddWithValue("@anio", anio)

                    conexion.Open()
                    command.ExecuteScalar()

                    Buscar_NotasC()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    'MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Finally
                    conexion.Dispose()
                    command.Dispose()
                End Try

            End If

        Catch ex As Exception
            MsgBox("No hay un registro actual para eliminar", _
                       MsgBoxStyle.Exclamation, _
                       "Eliminar")
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        EliminarRegistroAgente()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles BVerObj.Click
        ListBox1.Visible = True
    End Sub

    Private Sub BIngObj_Click(sender As Object, e As EventArgs) Handles BIngObj.Click
        If CheckedListBox1.CheckedItems.Count = 0 Then
            MsgBox("Seleccione al menos una linea")

        ElseIf CBAgente.Text = "Seleccione Agente" Then
            MsgBox("No ha seleccionado Agente")
        Else
            Try
                For i = 0 To CheckedListBox1.Items.Count - 1
                    If (CheckedListBox1.GetItemChecked(i)) Then
                        'MessageBox.Show(CheckedListBox1.Items(i))

                        Dim numer As String
                        numer = InputBox("Ingrese objetivo para " & CheckedListBox1.Items(i), "Objetivos")
                        TBobjetivo.Text = TBobjetivo.Text + CInt(numer)
                        'TBobjetivo.Visible = True
                        ListBox1.Items.Add(numer)
                        'BVerObj.Visible = True
                        'MsgBox("Ingrese objetivo para " & CheckedListBox1.Items(i))
                    End If

                Next
                TBobjetivo.Visible = True
                'BVerObj.Visible = True
                Label4.Visible = True
                Label5.Visible = True

                BIngObj.Visible = False
                BVerObj.Visible = True

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub DGObjAge_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGObjAge.CellDoubleClick
        Dim frm As New LineasObjetivoDetalle()
        frm.Show()
    End Sub

    Private Sub DGObjAge_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGObjAge.CellContentClick

    End Sub
End Class