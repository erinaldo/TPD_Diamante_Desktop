Option Explicit On
'Option Strict On
Imports System.Data.SqlClient
Public Class cochesalta

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
    Private Sub cochesalta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        path_form.Text = Me.Name.ToString & ".vb"
        cargar_registros("select t1.placas as Placas, t1.Marca,t1.Modelo, SlpName as Agente, t1.odometro2 as Odometro,t1.tipoauto as Tipo,t1.Estado,t2.Npoliza as Poliza,t2.Compañia,t2.plazo as Plazo,t2.fechafin as Caduca,t2.valor as 'Valor Seguro' from Cat_Agentes ca, coches t1 inner join seguro t2 on t1.Placas = t2.Placas where t1.Agente = ca.SlpCode", DataGridView1)
        LlenaComboboxs()
    End Sub
    Sub LlenaComboboxs()
        Dim Dt As DataTable
        Dim Cn As New SqlConnection(StrTpm)
        Dim Da As New SqlDataAdapter
        Dim Cmd As New SqlCommand

        'Para llenar el combobox de Agentes se utiliza la base de datos 'Cat_Agentes' de la BD TPM
        With Cmd
            .CommandType = CommandType.Text
            .CommandText = "Select * From Cat_Agentes where Status = 'Activo' order by SlpName"
            .Connection = Cn
        End With
        Da.SelectCommand = Cmd
        Dt = New DataTable
        Da.Fill(Dt)
        With ComboBox1
            .DataSource = Dt
            .DisplayMember = "SlpName"
            .ValueMember = "SlpCode"
            .SelectedIndex = -1
        End With

        'Para llenar el combobox de 'Marca' se utiliza la base de datos 'Marcas_Coches' de la BD TPM
        With Cmd
            .CommandType = CommandType.Text
            .CommandText = "select * From Marcas_Coches order by Nombre"
            .Connection = Cn
        End With
        Da.SelectCommand = Cmd
        Dt = New DataTable
        Da.Fill(Dt)
        With ComboBox6
            .DataSource = Dt
            .DisplayMember = "Nombre"
            .ValueMember = "id"
            .SelectedIndex = -1
        End With

        'Para llenar el combobox de 'Compañia' se utiliza la base de datos 'Compania_Seguros' de la BD TPM
        With Cmd
            .CommandType = CommandType.Text
            .CommandText = "select * from Compania_Seguros order by Nombre_Seguro"
            .Connection = Cn
        End With
        Da.SelectCommand = Cmd
        Dt = New DataTable
        Da.Fill(Dt)
        With ComboBox5
            .DataSource = Dt
            .DisplayMember = "Nombre_Seguro"
            .ValueMember = "id"
            .SelectedIndex = -1
        End With


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
        'dr("Slpname") = "Jorge L Ceballos"
        'dr("Slpcode") = "12"
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
        accion_boton()
    End Sub

    Public Sub accion_boton()
        'If TextBox1.Text <> "" Or TextBox2.Text <> "" Or NumericUpDown1.Value <> "" Or NumericUpDown1.Value = 0 Then
        If ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Selecciona un Tipo de Vehiculo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus()
            Return
        End If

        If ComboBox6.SelectedIndex = -1 Then
            MessageBox.Show("Selecciona una Marca de Vehiculo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox6.Focus()
            Return
        End If

        If TextBox4.Text.ToString = "" Then
            MessageBox.Show("Ingresa el año del Vehiculo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox4.Focus()
            Return
        Else
            Try
                Integer.Parse(TextBox4.Text)
            Catch ex As Exception
                MessageBox.Show("Ingresa un año del Vehiculo valido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox4.Focus()
                Return
            End Try
        End If

        If TextBox6.Text.ToString = "" Then
            MessageBox.Show("Ingresa el color del Vehiculo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox6.Focus()
            Return
        End If

        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Selecciona un Agente para el Vehiculo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Return
        End If

        If TextBox5.Text.ToString = "" Then
            MessageBox.Show("Ingresa el odometro del Vehiculo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox5.Focus()
            Return
        Else
            Try
                Integer.Parse(TextBox5.Text)
            Catch ex As Exception
                MessageBox.Show("Ingresa un odometro valido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox5.Focus()
                Return
            End Try
        End If

        If TextBox1.Text.ToString = "" Then
            MessageBox.Show("Ingresa las placas del Vehiculo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus()
            Return

        End If

        If ComboBox4.SelectedIndex = -1 Then
            MessageBox.Show("Selecciona el tipo de Placa", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus()
            Return
        End If

        If ComboBox3.SelectedIndex = -1 Then
            MessageBox.Show("Selecciona a que estado pertenece el Vehiculo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus()
            Return
        End If

        If TextBox8.Text.ToString = "" And ComboBox5.SelectedIndex = -1 And TextBox9.Text.ToString = "" And TextBox10.Text.ToString = "" And TextBox11.Text.ToString = "" Then
            MessageBox.Show("Asegurate de haber capturado la informacion de Seguro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = TabPage2
            TextBox8.Focus()
            Return
        End If

        If TextBox8.Text.ToString = "" Then
            MessageBox.Show("Ingresa el numero de poliza de seguro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus()
            Return
        End If

        If TextBox9.Text.ToString = "" Then
            MessageBox.Show("Ingresa el agente para el seguro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox9.Focus()
            Return
        End If

        If TextBox11.Text.ToString = "" Then
            MessageBox.Show("Ingresa el valor convenido del seguro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox11.Focus()
            Return
        Else
            Try
                Integer.Parse(TextBox11.Text)
            Catch ex As Exception
                MessageBox.Show("Ingresa un valor convenido valido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox11.Focus()
                Return
            End Try
        End If

        If ComboBox5.SelectedIndex = -1 Then
            MessageBox.Show("Selecciona una compañia de seguros", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox5.Focus()
            Return
        End If

        If TextBox10.Text.ToString = "" Then
            MessageBox.Show("Ingresa el registro para el seguro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Focus()
            Return
        End If

        If Integer.Parse(NumericUpDown1.Value) = 0 Then
            MessageBox.Show("El plazo para el seguro no puede ser 0", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            NumericUpDown1.Focus()
            Return
        End If

        If (MessageBox.Show("Resumen de Registro de Vehículo:" & vbCrLf & vbCrLf & "Tipo de Vehículo: " & ComboBox2.Text & vbCrLf & "Marca: " & ComboBox6.Text & vbCrLf & "Modelo: " & ComboBox7.Text & vbCrLf & "Año: " & TextBox4.Text & vbCrLf & "Color: " & TextBox6.Text & vbCrLf & "Agente: " & ComboBox1.Text & vbCrLf & "Odometro: " & TextBox5.Text & vbCrLf & "Placas: " & TextBox1.Text & vbCrLf & "Tipo de Placas: " & ComboBox4.Text & vbCrLf & "Estado: " & ComboBox3.Text & vbCrLf & "Numero de Poliza: " & TextBox8.Text & vbCrLf & "Agente(Seguro): " & TextBox9.Text & vbCrLf & "Fecha de Inicio de Seguro: " & String.Format("{0:dd/MM/yyyy}", dt1.Value) & vbCrLf & "Fecha de Vencimiento de Seguro: " & String.Format("{0:dd/MM/yyyy}", dt2.Value) & vbCrLf & "Plazo del Seguro: " & NumericUpDown1.Value & " Año(s)" & vbCrLf & "Valor Convenido del seguro: " & TextBox11.Text & vbCrLf & "Compañia del seguro: " & ComboBox5.Text & vbCrLf & "Registro del Seguro: " & TextBox10.Text & vbCrLf & vbCrLf & "¿Confirma que desea guardar estos datos?", _
                        "Por favor, verifica la información", _
                        MessageBoxButtons.YesNo, _
                       MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then
            'MsgBox("Voy a guardar")
            Dim conexion As New SqlConnection(StrTpm)
            Dim command As New SqlCommand("insrtCoche", conexion)
            Try
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@placa", TextBox1.Text)
                command.Parameters.AddWithValue("@marcar", ComboBox6.Text)
                command.Parameters.AddWithValue("@modelo", ComboBox7.Text)
                command.Parameters.AddWithValue("@año", TextBox4.Text)
                command.Parameters.AddWithValue("@agente", ComboBox1.SelectedValue.ToString)
                command.Parameters.AddWithValue("@kms", TextBox5.Text)
                command.Parameters.AddWithValue("@kms2", TextBox5.Text)
                command.Parameters.AddWithValue("@tipoauto", ComboBox2.Text)
                command.Parameters.AddWithValue("@color", TextBox6.Text)
                command.Parameters.AddWithValue("@tplaca", ComboBox4.Text)
                command.Parameters.AddWithValue("@Estado", ComboBox3.Text)
                command.Parameters.AddWithValue("@Npoliza", TextBox8.Text)
                command.Parameters.AddWithValue("@agent", TextBox9.Text)
                command.Parameters.AddWithValue("@Comp", ComboBox5.Text)
                command.Parameters.AddWithValue("@Registro", TextBox10.Text)
                command.Parameters.AddWithValue("@Fechai", String.Format("{0:yyyyMMdd}", dt1.Value))
                command.Parameters.AddWithValue("@plazo", NumericUpDown1.Value.ToString)
                command.Parameters.AddWithValue("@valor", TextBox11.Text)
                command.Parameters.AddWithValue("@fechaf", String.Format("{0:yyyyMMdd}", dt2.Value))
                conexion.Open()
                command.ExecuteNonQuery()
                ComboBox2.SelectedIndex = -1
                ComboBox6.SelectedIndex = -1
                ComboBox7.SelectedIndex = -1
                ComboBox1.SelectedIndex = -1
                ComboBox4.SelectedIndex = -1
                ComboBox3.SelectedIndex = -1
                TextBox4.Text = ""
                TextBox6.Text = ""
                TextBox5.Text = ""
                TextBox1.Text = ""
                TextBox8.Text = ""
                TextBox9.Text = ""
                TextBox11.Text = ""
                ComboBox5.SelectedIndex = -1
                TextBox10.Text = ""
                NumericUpDown1.Value = 0
                TabControl1.SelectedTab = TabPage1
                ComboBox2.Focus()


                MessageBox.Show("Datos guardados correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)

                cargar_registros("select t1.placas as Placas, t1.Marca,t1.Modelo, SlpName as Agente, t1.odometro2 as Odometro,t1.tipoauto as Tipo,t1.Estado,t2.Npoliza as Poliza,t2.Compañia,t2.plazo as Plazo,t2.fechafin as Caduca,t2.valor as 'Valor Seguro' from Cat_Agentes ca, coches t1 inner join seguro t2 on t1.Placas = t2.Placas where t1.Agente = ca.SlpCode", DataGridView1)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                conexion.Dispose()
                command.Dispose()
            End Try
        End If
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
            dv.DataSource = dt
            With dv
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .DefaultCellStyle.BackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .RowHeadersVisible = True
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Plazo").Width = 50
                .Columns("Agente").Width = 170
                .Refresh()

                ' coloca el registro arriba de todo  
                '.FirstDisplayedCell = Me.DGObjetivos.CurrentCell
            End With


            bEdit = False
        Catch exSql As SqlException
            MsgBox(exSql.Message.ToString)
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            'MsgBox("Actualmente no existen registros en la base de datos")
            'MessageBox.Show("Nn hay Informacion por mostrar", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        'String.Format("{0:yyyyMMdd}", dt1.Value) 
        dt2.Value = DateTime.Now.AddYears(NumericUpDown1.Value.ToString)
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        ComboBox7.Text = ""
        With ComboBox7
            .DataSource = Nothing
            .DisplayMember = Nothing
            .ValueMember = Nothing
            .Items.Clear()
        End With
        Try

            Dim Dt As DataTable
            Dim Cn As New SqlConnection(StrTpm)
            Dim Da As New SqlDataAdapter
            Dim Cmd As New SqlCommand

            'Para llenar el combobox de 'Modelo' se utiliza la base de datos 'Modelo_Coches' de la BD TPM
            With Cmd
                .CommandType = CommandType.Text
                .CommandText = "select * from Modelo_Coches where Marca = " & ComboBox6.SelectedValue.ToString & " order by Nombre"
                .Connection = Cn
            End With
            Da.SelectCommand = Cmd
            Dt = New DataTable
            Da.Fill(Dt)
            With ComboBox7
                .DataSource = Dt
                .DisplayMember = "Nombre"
                .ValueMember = "id"
            End With
        Catch ex As Exception
        End Try
        'If (ComboBox6.SelectedIndex <> -1) Then
        '    MsgBox("Seleccionaste " & ComboBox6.SelectedValue.ToString)
        'End If
        'MsgBox("Seleccionaste " & ComboBox6.)
        'Dim Dt As DataTable
        'Dim Cn As New SqlConnection(StrTpm)
        'Dim Da As New SqlDataAdapter
        'Dim Cmd As New SqlCommand

        ''Para llenar el combobox de Agentes se utiliza la base de datos 'Cat_Agentes' de la BD TPM
        'With Cmd
        '    .CommandType = CommandType.Text
        '    .CommandText = "Select * From Cat_Agentes where Status = 'Activo' order by SlpName"
        '    .Connection = Cn
        'End With
        'Da.SelectCommand = Cmd
        'Dt = New DataTable
        'Da.Fill(Dt)
        'With ComboBox1
        '    .DataSource = Dt
        '    .DisplayMember = "SlpName"
        '    .ValueMember = "SlpCode"
        'End With

        'If ComboBox6.Text = "Chevrolet" Then
        '    ComboBox7.Items.Clear()

        '    ComboBox7.SelectedText = "Aveo"
        '    ComboBox7.Items.Add("Aveo")
        '    ComboBox7.Items.Add("Tornado")
        '    ComboBox7.Items.Add("Suv")
        'End If
        'If ComboBox6.Text = "Nissan" Then
        '    ComboBox7.Items.Clear()
        '    ComboBox7.SelectedText = "Np 300"
        '    ComboBox7.Items.Add("NP 300")
        '    ComboBox7.Items.Add("Tsuru")
        'End If
        'If ComboBox6.Text = "Volkswagen" Then
        '    ComboBox7.Items.Clear()
        '    ComboBox7.SelectedText = "Jetta A6"
        '    ComboBox7.Items.Add("Jetta A6")
        'End If
        'If ComboBox6.Text = "Honda" Then
        '    ComboBox7.Items.Clear()
        '    ComboBox7.SelectedText = "Cargo 150"
        '    ComboBox7.Items.Add("CRV")
        '    ComboBox7.Items.Add("Cargo 150")
        'End If
        'If ComboBox6.Text = "Ford" Then
        '    ComboBox7.Items.Clear()
        '    ComboBox7.SelectedText = "Transit"
        '    ComboBox7.Items.Add("Transit")

        'End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Me.TextBox1.Text = UCase(Me.TextBox1.Text)
        Me.TextBox1.SelectionStart = Me.TextBox1.TextLength + 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        accion_boton()
    End Sub


End Class