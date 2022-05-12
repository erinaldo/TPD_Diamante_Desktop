

Imports System.Data.SqlClient


Public Class ClasificacionPorPiezas
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Llenar_DataGridView()
    End Sub

    Private Sub ClasificacionPorPiezas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LlenaAlmacen()
        LlenaLinea()

    End Sub

    Private Sub Llenar_DataGridView()
        'Los argumentos de conexión a la base de datos 

        Dim args As String = conexion_universal.CadenaSQL

        Dim command As SqlCommand
        Dim adapter As SqlDataAdapter
        Dim dtTable As DataTable

        Using connection As SqlConnection = New SqlConnection(args)
            command = New SqlCommand("CladificacionAcumulado", connection)
            command.CommandType = CommandType.StoredProcedure
            adapter = New SqlDataAdapter(command)
            dtTable = New DataTable
            With command.Parameters
                'Envió los parámetros que necesito

                If cbAlmacen.Text <> "TODOS" Then
                    .Add(New SqlParameter("@Almacen", SqlDbType.Int)).Value = cbAlmacen.SelectedValue
                Else
                    .Add(New SqlParameter("@Almacen", SqlDbType.Int)).Value = 99
                End If


                If cmbLinea.Text <> "TODAS" Then
                    .Add(New SqlParameter("@Linea", SqlDbType.Int)).Value = cmbLinea.SelectedValue
                Else
                    .Add(New SqlParameter("@Linea", SqlDbType.Int)).Value = 999
                End If

            End With

            Try
                adapter.Fill(dtTable)
                DataGridView1.DataSource = dtTable
                'dgvClasificacion.AutoGenerateColumns = True
            Catch expSQL As SqlException
                MsgBox(expSQL.ToString, MsgBoxStyle.OkOnly, "SQL Exception")
                Exit Sub
            End Try
        End Using
    End Sub





    Private Sub LlenaAlmacen()

        Dim ConsutaLista As String

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)


            Dim DSetTablas As New DataSet
            ConsutaLista = "select WhsCode, WhsName from OWHS where WhsCode='01' or WhsCode='03' or WhsCode='07'"
            Dim daAlmacen As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            'Dim DSetTablas As New DataSet
            daAlmacen.Fill(DSetTablas, "Almacen")

            Dim fila As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            fila = DSetTablas.Tables("Almacen").NewRow

            'Agregamos los valores a los campos de la tabla
            fila("whsname") = "TODOS"
            fila("whscode") = 99

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Almacen").Rows.Add(fila)

            Me.cbAlmacen.DataSource = DSetTablas.Tables("Almacen")
            Me.cbAlmacen.DisplayMember = "whsname"
            Me.cbAlmacen.ValueMember = "whscode"
            Me.cbAlmacen.SelectedValue = 99


        End Using
    End Sub
    'Llenar combobox línea

    Sub LlenaLinea()
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
                Dim ConsutaLista As String
                Dim ds As New DataSet
                ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
                Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

                Dim dsArt As New DataSet
                daArticulo.Fill(dsArt)

                Dim fila As Data.DataRow

                'Asignamos a fila la nueva Row(Fila)del Dataset
                fila = dsArt.Tables(0).NewRow

                'Agregamos los valores a los campos de la tabla
                fila("ItmsGrpNam") = "TODAS"
                fila("ItmsGrpCod") = 999

                ''Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
                dsArt.Tables(0).Rows.Add(fila)

                Me.cmbLinea.DataSource = dsArt.Tables(0)
                Me.cmbLinea.DisplayMember = "ItmsGrpNam"
                Me.cmbLinea.ValueMember = "ItmsGrpCod"
                Me.cmbLinea.SelectedValue = 999

            End Using
        Catch ex As Exception
            MsgBox("Error al cargar las lineas: " + ex.Message)
        End Try
    End Sub


End Class