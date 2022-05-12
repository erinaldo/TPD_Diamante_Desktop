
Option Explicit On
'Option Strict On
Imports System.Data.SqlClient


Public Class LineasHalconIngresarObjetivos
    ' Espacios de nombres  
    ' ''''''''''''''''''''''''''''''''''''''''' 

    Public conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")

    Public command2 As New SqlCommand("LHUpdate", conexion)
    Public Rs As SqlDataReader
    Public SQL As String
    Public Com As New SqlCommand
    'Public MiConexion As New SqlConnection(conexion)
    'Dim MiConexion As New SqlConnection(conexion)
    'Dim Rs As SqlDataReader
    'Dim Com As New SqlCommand

    Dim Dvtotales As New DataView
    Dim Dvagentes As New DataView

    'BindingSource  
    Private WithEvents bs As New BindingSource

    ' Adaptador de datos sql  
    Private SqlDataAdapter As SqlDataAdapter

    ' Cadena de conexión  
    Private Const cs As String = "Server=SERVIDORSAP; Database=TPM; User id=sa; Password=SD1amany3S"

    ' flag  
    Private bEdit As Boolean

    Public StrCon As String = "Data Source=SERVIDORSAP;Initial Catalog=SBO_TPD;User Id=SA;Password=SD1amany3S;"

    ' actualizar los cambios al salir  
    ' ''''''''''''''''''''''''''''''''''''''''  
    Private Sub ScoreCardIngresarObjetivos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If bEdit Then
            'preguntar si se desea guardar  
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

            Me.CmbAgteVta.DataSource = Dvagentes
            Me.CmbAgteVta.DisplayMember = "slpname"
            Me.CmbAgteVta.ValueMember = "slpcode"
            Me.CmbAgteVta.SelectedValue = 999
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

        TBCARGO.Text = Format(0, "#,##0.00")
        TBCARGO.TextAlign = HorizontalAlignment.Right

        TBDP.Text = Format(0, "#,##0.00")
        TBDP.TextAlign = HorizontalAlignment.Right

        TBKING.Text = Format(0, "#,##0.00")
        TBKING.TextAlign = HorizontalAlignment.Right

        TBPOWERPRO.Text = Format(0, "#,##0.00")
        TBPOWERPRO.TextAlign = HorizontalAlignment.Right

        TBRODWELL.Text = Format(0, "#,##0.00")
        TBRODWELL.TextAlign = HorizontalAlignment.Right

        TBRODWELLC.Text = Format(0, "#,##0.00")
        TBRODWELLC.TextAlign = HorizontalAlignment.Right

        TBUJOINTK.Text = Format(0, "#,##0.00")
        TBUJOINTK.TextAlign = HorizontalAlignment.Right

        TBRODWELLB.Text = Format(0, "#,##0.00")
        TBRODWELLB.TextAlign = HorizontalAlignment.Right

        TBROCKWELLB.Text = Format(0, "#,##0.00")
        TBROCKWELLB.TextAlign = HorizontalAlignment.Right

        TBRODWELLB.Text = Format(0, "#,##0.00")
        TBRODWELLB.TextAlign = HorizontalAlignment.Right


        CBAño.Text = Year(Now)

    End Sub


    Private Sub Form1_Load( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Actualizar()

        LlenaCmbAgte()

        LLenaMes()

        Buscar_NotasC()


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
            'DGObjetivos.DataSource = bs
            .ReadOnly = True
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = True
            .RowHeadersWidth = 15
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'Color de linea del grid

            'centrar encabezados del datagrid 
            DGObjetivos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try

                'Catch ex As Exception

                'End Try


                .Columns(0).HeaderText = "Vendedor"
                .Columns(0).Width = 170
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                .Columns(1).HeaderText = "Mes"
                .Columns(1).Width = 60
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(2).HeaderText = "Año"
                .Columns(2).Width = 80
                '.Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Objetivo"
                .Columns(3).Width = 100
                .Columns(3).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


            Catch ex As Exception

            End Try

        End With



        ''btn_first.Text = "<<"
        ''btn_Previous.Text = "<"
        ''btn_next.Text = ">"
        ''btn_last.Text = ">>"


        '' cagar los datos  -----Select slpname, groupname,anio,mes,objetivo from SC_Objetivos
        'cargar_registros("Select SLPNAME AS Vendedor, MES AS Mes, anio AS Año, sum(objetivo) as Objetivo " +
        '                 "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO order by anio DESC, mes", DGObjetivos)

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

        'If Not bs.DataSource Is Nothing Then
        '    SqlDataAdapter.Update(CType(bs.DataSource, DataTable))
        '    If bCargar Then
        '        cargar_registros("Select SLPNAME AS Vendedor, MES AS Mes, anio AS Año, sum(objetivo) as Objetivo " +
        '                 "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO order by anio DESC, mes", DGObjetivos)
        '    End If
        'End If
    End Sub


    Sub Buscar_NotasC()
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


            cmd4 = New SqlCommand("LHMuestraObjetivos", cnn)
            cmd4.CommandType = CommandType.StoredProcedure



            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn

            ''--------------------------------------------
            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "Totales"
            'DsVtas.Tables(1).TableName = "VtaAgtes"
            'DsVtas.Tables(2).TableName = "VtaCltes"

            DvTotales.Table = DsVtas.Tables("Totales")
            'DvAgentes.Table = DsVtas.Tables("VtaAgtes")
            ''DvClientes.Table = DsVtas.Tables("VtaCltes")


            DGObjetivos.DataSource = DvTotales

            'DgVtaAgte.DataSource = DvAgentes

        Catch ex As Exception
            MsgBox(ex.Message)
            'MsgBox("No existen ventas de este día")
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

        'txtDiasRestantes.Text = Convert.ToString(vDiasMes - vDiasTrans)
        'txtAvanceOptimo.Text = Format(Convert.ToString((vDiasTrans / vDiasMes) * 100), "000.00")

        'txtAvanceOptimo.Text = (vDiasTrans / vDiasMes).ToString("P1")

    End Sub


    Private Sub btn_first_Click( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs)

    End Sub

    Private Sub DataGridView1_CellEndEdit( _
        ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        bEdit = True

    End Sub

    Private Sub GenerarObjetivos()
        Dim Mes As Integer
        If CBMES.SelectedItem = "Enero" Then
            Mes = 1
        ElseIf CBMES.SelectedItem = "Febrero" Then
            Mes = 2
        ElseIf CBMES.SelectedItem = "Marzo" Then
            Mes = 3
        ElseIf CBMES.SelectedItem = "Abril" Then
            Mes = 4
        ElseIf CBMES.SelectedItem = "Mayo" Then
            Mes = 5
        ElseIf CBMES.SelectedItem = "Junio" Then
            Mes = 6
        ElseIf CBMES.SelectedItem = "Julio" Then
            Mes = 7
        ElseIf CBMES.SelectedItem = "Agosto" Then
            Mes = 8
        ElseIf CBMES.SelectedItem = "Septiembre" Then
            Mes = 9
        ElseIf CBMES.SelectedItem = "Octubre" Then
            Mes = 10
        ElseIf CBMES.SelectedItem = "Noviembre" Then
            Mes = 11
        ElseIf CBMES.SelectedItem = "Diciembre" Then
            Mes = 12
        ElseIf CBMES.SelectedItem = "TODOS" Then
            Mes = 99
        End If



        If Mes <> 99 And CmbAgteVta.SelectedValue <> 999 Then
            '----PROCEDIMIENTO para ingresar objetivo de manera individual

            Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
            Dim command As New SqlCommand("SPLHObjInd", conexion)
            Try
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add("@slpcode", SqlDbType.Int).Value = Me.CmbAgteVta.SelectedValue
                command.Parameters.Add("@slpname", SqlDbType.NVarChar, 100).Value = Me.CmbAgteVta.SelectedValue
                command.Parameters.AddWithValue("@mes", Mes)
                command.Parameters.AddWithValue("@anio", CBAño.Text)
                command.Parameters.Add("@objetivo1", SqlDbType.Decimal).Value = Convert.ToDecimal(TBCARGO.Text)
                command.Parameters.Add("@objetivo2", SqlDbType.Decimal).Value = Convert.ToDecimal(TBDP.Text)
                command.Parameters.Add("@objetivo3", SqlDbType.Decimal).Value = Convert.ToDecimal(TBKING.Text)
                command.Parameters.Add("@objetivo4", SqlDbType.Decimal).Value = Convert.ToDecimal(TBPOWERPRO.Text)
                command.Parameters.Add("@objetivo5", SqlDbType.Decimal).Value = Convert.ToDecimal(TBRODWELL.Text)
                command.Parameters.Add("@objetivo6", SqlDbType.Decimal).Value = Convert.ToDecimal(TBRODWELLB.Text)
                command.Parameters.Add("@objetivo7", SqlDbType.Decimal).Value = Convert.ToDecimal(TBRODWELLC.Text)
                command.Parameters.Add("@objetivo8", SqlDbType.Decimal).Value = Convert.ToDecimal(TBUJOINTK.Text)
                command.Parameters.Add("@objetivo9", SqlDbType.Decimal).Value = Convert.ToDecimal(TBROCKWELLB.Text)


                conexion.Open()
                command.ExecuteNonQuery()
                cargar_registros("Select SLPNAME AS Vendedor, MES AS Mes, anio AS Año, sum(objetivo) as Objetivo " +
                         "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO order by anio DESC, mes", DGObjetivos)
            Catch ex As Exception
                'MessageBox.Show(ex.Message)
                MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Finally
                conexion.Dispose()
                command.Dispose()
            End Try


        End If




    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Actualizar()
    End Sub


    Private Sub Button4_Click_1(sender As Object, e As EventArgs)
        Actualizar()
    End Sub

    

    'CONSULTAR
    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click

        Dim Mes As Integer
        If CBMES.SelectedItem = "Enero" Then
            Mes = 1
        ElseIf CBMES.SelectedItem = "Febrero" Then
            Mes = 2
        ElseIf CBMES.SelectedItem = "Marzo" Then
            Mes = 3
        ElseIf CBMES.SelectedItem = "Abril" Then
            Mes = 4
        ElseIf CBMES.SelectedItem = "Mayo" Then
            Mes = 5
        ElseIf CBMES.SelectedItem = "Junio" Then
            Mes = 6
        ElseIf CBMES.SelectedItem = "Julio" Then
            Mes = 7
        ElseIf CBMES.SelectedItem = "Agosto" Then
            Mes = 8
        ElseIf CBMES.SelectedItem = "Septiembre" Then
            Mes = 9
        ElseIf CBMES.SelectedItem = "Octubre" Then
            Mes = 10
        ElseIf CBMES.SelectedItem = "Noviembre" Then
            Mes = 11
        ElseIf CBMES.SelectedItem = "Diciembre" Then
            Mes = 12
        ElseIf CBMES.SelectedItem = "TODOS" Then
            Mes = 99
        End If

        Try
            'Dim str As String = "SELECT * FROM R_EQUIPOS where serial LIKE @desc"
            'Dim cmd As New SqlCommand(str, con)
            Dim cmd As SqlCommand = New SqlCommand("SELECT objetivo FROM LHObjInd where slpcode= '" & CmbAgteVta.SelectedValue & "' and mes ='" & Mes & "' and anio='" & CBAño.Text & "'", conexion)

            'cmd.Parameters.AddWithValue("@desc", CStr(cbEquipo.SelectedValue))

            Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Dim row1 As DataRow = dt.Rows(1)
                Dim row2 As DataRow = dt.Rows(2)
                Dim row3 As DataRow = dt.Rows(3)
                Dim row4 As DataRow = dt.Rows(4)
                Dim row5 As DataRow = dt.Rows(5)
                Dim row6 As DataRow = dt.Rows(6)
                Dim row7 As DataRow = dt.Rows(7)
                Dim row8 As DataRow = dt.Rows(8)


                TBCARGO.Text = CStr(row("OBJETIVO"))
                TBDP.Text = CStr(row1("OBJETIVO"))
                TBKING.Text = CStr(row2("OBJETIVO"))
                TBPOWERPRO.Text = CStr(row3("OBJETIVO"))
                TBRODWELL.Text = CStr(row4("OBJETIVO"))
                TBRODWELLC.Text = CStr(row5("OBJETIVO"))
                TBUJOINTK.Text = CStr(row6("OBJETIVO"))
                TBRODWELLB.Text = CStr(row7("OBJETIVO"))
                TBROCKWELLB.Text = CStr(row8("OBJETIVO"))

            Else
                MsgBox("No hay registros para este agente")
                TBCARGO.Text = "0.00"
                TBDP.Text = "0.00"
                TBKING.Text = "0.00"
                TBPOWERPRO.Text = "0.00"
                TBRODWELL.Text = "0.00"
                TBRODWELLC.Text = "0.00"
                TBUJOINTK.Text = "0.00"
                TBRODWELLB.Text = "0.00"
                TBROCKWELLB.Text = "0.00"

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    

    'BOTON CREAR REGISTRO 
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        'If CBMES.Text <> "TODOS" And TBCARGO.Text = 0 Then

        '    MsgBox("Ingrese objetivo para continuar", _
        '           MsgBoxStyle.Exclamation, _
        '           "Ingrese Objetivo")

        'Else
        If (MessageBox.Show( _
          "¿Confirma que desea generar este registro?" + Chr(13) + Chr(13) & CmbAgteVta.Text & "   " & CBMES.Text & _
                            "   " & CBAño.Text, _
                       "Generar Registro", MessageBoxButtons.YesNo, _
                      MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

            GenerarObjetivos()
            Buscar_NotasC()
        End If
        'End If
    End Sub


    'BOTON ELIMINAR
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim i As Integer
        Dim agente As String
        Dim anio As String
        Dim mes As Integer

        Dim objetivo As Decimal
        Try


            i = DGObjetivos.CurrentRow.Index
            agente = DGObjetivos.Item(0, i).Value.ToString
            anio = DGObjetivos.Item(2, i).Value.ToString
            mes = DGObjetivos.Item(1, i).Value.ToString
            objetivo = DGObjetivos.Item(3, i).Value.ToString

            If (MessageBox.Show("¿Confirma que desea eliminar este registro?" + Chr(13) + Chr(13) & agente & "   " & mes & _
                                "   " & anio & "   $" & objetivo, _
                                "Eliminar", _
                                MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

                Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
                'Dim command As New SqlCommand("SPLHObjInd", conexion)
                Dim command As New SqlCommand("LHDelete", conexion)

                Try
                    'command = New SqlCommand("LHDelete", conexion)
                    command.CommandType = CommandType.StoredProcedure
                    'command.CommandType = CommandType.StoredProcedure
                    'command.Parameters.Add("@slpcode", SqlDbType.Int).Value = Me.CmbAgteVta.SelectedValue
                    command.Parameters.Add("@slpname", SqlDbType.NVarChar, 100).Value = agente
                    command.Parameters.AddWithValue("@mes", mes)
                    command.Parameters.AddWithValue("@anio", anio)

                    conexion.Open()
                    command.ExecuteScalar()
                    cargar_registros("SELECT slpname AS 'Vendedor', mes AS 'Mes', anio AS 'Año', SUM(Objetivo) AS 'Objetivo'  " +
                             "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO ", DGObjetivos)

                    Buscar_NotasC()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    'MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Finally
                    conexion.Dispose()
                    command.Dispose()
                End Try


                'If Not bs.Current Is Nothing Then
                '    ' eliminar  
                '    bs.RemoveCurrent()

                '    'Guardar los cambios y recargar  
                '    Actualizar()
                'Else
                '    MsgBox("No hay un registro actual para eliminar", _
                '           MsgBoxStyle.Exclamation, _
                '           "Eliminar")
                'End If

            End If

        Catch ex As Exception
            MsgBox("No hay un registro actual para eliminar", _
                       MsgBoxStyle.Exclamation, _
                       "Eliminar")
        End Try

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click


        If (MessageBox.Show("¿Confirma que desea eliminar TODO?", "Eliminar", MessageBoxButtons.YesNo, _
                                MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

            Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")

            Dim command As New SqlCommand("LHTruncate", conexion)

            Try
                'command = New SqlCommand("LHDelete", conexion)
                command.CommandType = CommandType.StoredProcedure

                conexion.Open()
                command.ExecuteScalar()
                cargar_registros("SELECT slpname AS 'Vendedor', mes AS 'Mes', anio AS 'Año', SUM(Objetivo) AS 'Objetivo'  " +
                             "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO ", DGObjetivos)

                Buscar_NotasC()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                
            Finally
                conexion.Dispose()
                command.Dispose()
            End Try
        End If


    End Sub


    Private Sub CmbAgteVta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAgteVta.SelectedIndexChanged
        If CmbAgteVta.Text = "GENERAL" Then
            Label2.Visible = True
            Label3.Visible = True
            BSave.Visible = True
            TbObjGen.Visible = True

            Button6.Visible = False
            'End If

        ElseIf CmbAgteVta.Text <> "Seleccione Agente" Then
            Button6.Visible = True

            Label2.Visible = False
            Label3.Visible = False
            BSave.Visible = False
            TbObjGen.Visible = False


        Else
            Button6.Visible = False

            Label2.Visible = False
            Label3.Visible = False
            BSave.Visible = False
            TbObjGen.Visible = False

        End If
    End Sub


    'ACTUALIZAR
    Private Sub BAct_Click(sender As Object, e As EventArgs) Handles BAct.Click



        'Dim anio As String
        'Dim mes As Integer

        Dim Mes As Integer
        If CBMES.SelectedItem = "Enero" Then
            Mes = 1
        ElseIf CBMES.SelectedItem = "Febrero" Then
            Mes = 2
        ElseIf CBMES.SelectedItem = "Marzo" Then
            Mes = 3
        ElseIf CBMES.SelectedItem = "Abril" Then
            Mes = 4
        ElseIf CBMES.SelectedItem = "Mayo" Then
            Mes = 5
        ElseIf CBMES.SelectedItem = "Junio" Then
            Mes = 6
        ElseIf CBMES.SelectedItem = "Julio" Then
            Mes = 7
        ElseIf CBMES.SelectedItem = "Agosto" Then
            Mes = 8
        ElseIf CBMES.SelectedItem = "Septiembre" Then
            Mes = 9
        ElseIf CBMES.SelectedItem = "Octubre" Then
            Mes = 10
        ElseIf CBMES.SelectedItem = "Noviembre" Then
            Mes = 11
        ElseIf CBMES.SelectedItem = "Diciembre" Then
            Mes = 12
        ElseIf CBMES.SelectedItem = "TODOS" Then
            Mes = 99
        End If

        Dim conexion As New SqlConnection("Data Source=SERVIDORSAP;Initial Catalog=TPM;User Id=SA;Password=SD1amany3S;")
        'Dim command As New SqlCommand("SPLHObjInd", conexion)
        command2 = New SqlCommand("LHUpdate", conexion)
        Try

            command2.CommandType = CommandType.StoredProcedure
            'command.CommandType = CommandType.StoredProcedure

            command2.Parameters.Add("@slpname", SqlDbType.NVarChar, 100).Value = CmbAgteVta.SelectedValue
            command2.Parameters.AddWithValue("@mes", Mes)
            command2.Parameters.AddWithValue("@anio", CBAño.Text)
            command2.Parameters.AddWithValue("@obj0", TBCARGO.Text)
            command2.Parameters.AddWithValue("@obj1", TBDP.Text)
            command2.Parameters.AddWithValue("@obj2", TBKING.Text)
            command2.Parameters.AddWithValue("@obj3", TBPOWERPRO.Text)
            command2.Parameters.AddWithValue("@obj4", TBRODWELL.Text)
            command2.Parameters.AddWithValue("@obj5", TBRODWELLC.Text)
            command2.Parameters.AddWithValue("@obj6", TBUJOINTK.Text)
            command2.Parameters.AddWithValue("@obj7", TBRODWELLB.Text)
            command2.Parameters.AddWithValue("@obj8", TBROCKWELLB.Text)


            conexion.Open()
            command2.ExecuteNonQuery()
            cargar_registros("SELECT slpname AS 'Vendedor', mes AS 'Mes', anio AS 'Año', SUM(Objetivo) AS 'Objetivo'  " +
                     "FROM LHObjInd GROUP BY SLPNAME, MES, ANIO ", DGObjetivos)

            Buscar_NotasC()

            MsgBox("Se actualizo correctamente")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'MessageBox.Show("Este registro ya existe, Verifique!", "Registro Existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Finally
            conexion.Dispose()
            command2.Dispose()
        End Try
    End Sub

End Class