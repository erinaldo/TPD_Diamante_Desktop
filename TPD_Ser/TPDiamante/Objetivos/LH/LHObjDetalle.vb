
Imports System.Data.SqlClient

Public Class LHObjDetalle

    Dim DvAgentes As New DataView

    'BindingSource  
    Private WithEvents bs As New BindingSource

    ' Adaptador de datos sql  
    Private SqlDataAdapter As SqlDataAdapter

    ' Cadena de conexión  
    Private Const cs As String = "Server=SERVIDORSAP; Database=TPM; User id=sa; Password=SD1amany3S"

    ' flag  
    Private bEdit As Boolean

    Public StrCon As String = "Data Source=SERVIDORSAP;Initial Catalog=SBO_TPD;User Id=SA;Password=SD1amany3S;"


    Private Sub Form1_Load( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Dim vDiasMes As Integer
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Dim cmd2 As SqlCommand = Nothing
        Dim vDiasTrans As Integer

        Dim cmd3 As SqlCommand = Nothing
        Dim cmd4 As SqlCommand = Nothing

        Try
            cnn = New SqlConnection(StrTpm)
            'cmd = New SqlCommand("Indicadores", cnn)
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.Add("@FechaFinal", SqlDbType.SmallDateTime).Value = LineasHalcon.DtpFechaIni.Value
            'cmd.Parameters.Add("@TipoConsulta", SqlDbType.Int).Value = 1

            cnn.Open()

            'vDiasMes = CInt(cmd.ExecuteScalar())
            ''txtDiasMes.Text = vDiasMes.ToString

            'cmd2 = New SqlCommand("Indicadores", cnn)
            'cmd2.CommandType = CommandType.StoredProcedure
            'cmd2.Parameters.Add("@FechaFinal", SqlDbType.SmallDateTime).Value = LineasHalcon.DtpFechaIni.Value
            'cmd2.Parameters.Add("@TipoConsulta", SqlDbType.Int).Value = 2

            'vDiasTrans = CInt(cmd2.ExecuteScalar())
            'txtDiasTranscurridos.Text = vDiasTrans.ToString


            cmd4 = New SqlCommand("LHObjDetalle", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            'cmd4.Parameters.Add("@FechaFinal", SqlDbType.Date).Value = LineasHalcon.DtpFechaIni.Value
            'cmd4.Parameters.Add("@DiasMes", SqlDbType.Int).Value = vDiasMes
            'cmd4.Parameters.Add("@DiasTrans", SqlDbType.Int).Value = vDiasTrans
            'cmd4.Parameters.Add("@DiasRest", SqlDbType.Int).Value = vDiasMes - vDiasTrans
            'cmd4.Parameters.Add("@PorAvanOptimo", SqlDbType.Decimal).Value = vDiasTrans / vDiasMes
            'cmd4.Parameters.Add("@Agente", SqlDbType.VarChar, 30).Value = "8"

            'Dim myForm As New LHObjDetalle()
            'myForm.Show()

            cmd4.Parameters.Add("@Agente", SqlDbType.VarChar, 100).Value = LineasHalconIngresarObjetivos.DGObjetivos.Item(0, LineasHalconIngresarObjetivos.DGObjetivos.CurrentCell.RowIndex).Value


            'LAgente.Text = LineasHalconIngresarObjetivos.DGObjetivos.Item(0, LineasHalconIngresarObjetivos.DGObjetivos.CurrentCell.RowIndex).Value
            'LAgente.Text = "EMPLEADO ..."

            'Me.CmbAgteVta.SelectedValue

            'Dim mes As Int16
            'mes = DtpFechaIni.Text.Substring(3, 2)
            ''MsgBox(mes)
            'Dim anio As Int16
            'anio = DtpFechaIni.Text.Substring(6, 4)
            ''MsgBox(anio)

            'cmd4.Parameters.Add("@MesActual", SqlDbType.Int).Value = mes
            'cmd4.Parameters.Add("@AñoActual", SqlDbType.Int).Value = anio


            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn


            ''--------------------------------------------
            Dim DvAgentes As New DataView


            Dim DsVtas As New DataSet
            da.Fill(DsVtas, "DsVtas")

            DsVtas.Tables(0).TableName = "VtaAgtes"

            DvAgentes.Table = DsVtas.Tables("VtaAgtes")

            DataGridView1.DataSource = DvAgentes


        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

        'txtDiasRestantes.Text = Convert.ToString(vDiasMes - vDiasTrans)
        'txtAvanceOptimo.Text = Format(Convert.ToString((vDiasTrans / vDiasMes) * 100), "000.00")

        'txtAvanceOptimo.Text = (vDiasTrans / vDiasMes).ToString("P1")


        With Me.DataGridView1
            Try


                '.DataSource = DtAgte
                .ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue

                DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = False
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                'Color de linea del grid

                .Columns(0).HeaderText = "Linea"
                .Columns(0).Width = 120
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Venta"
                .Columns(1).Width = 96
                .Columns(1).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(2).HeaderText = "Tendencia fin de mes($)"
                .Columns(2).Width = 96
                .Columns(2).DefaultCellStyle.Format = "$ ###,###,##0.#0"
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Catch ex As Exception

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


    Private Sub DataGridView1_CellEndEdit( _
        ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
            Handles DataGridView1.CellEndEdit
        bEdit = True

    End Sub
End Class