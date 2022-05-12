
Option Explicit On
'Option Strict On
Imports System.Data.SqlClient

Public Class Packin


    Public conexion As New SqlConnection(conexion_universal.CadenaSQLSAP)
    Public conexion2 As New SqlConnection(conexion_universal.CadenaSQL)

    Public DvDetalle As New DataView
    Public DvPeso As New DataView

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        '***********BUSCAR POR*********** 
        '********NUMERO DE FACTURA*******
        '
        If TBDocNum.Text <> "" Then


            conexion.Open()
            Dim cmd As SqlCommand = New SqlCommand("SELECT T0.DocEntry, t0.DocNum, T0.CardCode, T0.CardName, t0.InvntSttus, t0.DocType, t0.DocStatus, " & _
            "T0.Printed, CASE WHEN T0.Series IS NULL THEN '' ELSE T2.SeriesName END AS SeriesName, " & _
            "T0.docdate,T1.slpname, 'Nombre_Telemarketing' as 'Telemarketing' " & _
            "FROM OINV T0 " & _
            "LEFT JOIN OSLP T1 ON T0.SlpCode=T1.SlpCode " & _
            "LEFT JOIN NNM1 T2 ON T0.Series=T2.Series " & _
            "where docnum = @docnum ", conexion)
            cmd.Parameters.AddWithValue("@docnum", TBDocNum.Text)

            Try

                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count > 0 Then

                    Dim row As DataRow = dt.Rows(0)
                    If (CStr(row("InvntSttus")).ToString = "C" And CStr(row("DocType")).ToString = "I") Then
                        DGDetalle.DataSource = Nothing
                        Label26.Visible = False
                        ComboBox1.Visible = False
                        TBCliente.Text = ""
                        TBNomCli.Text = ""
                        TBPerCon.Text = ""
                        TBDocDate.Text = ""
                        BSave.Enabled = False
                        MessageBox.Show("La factura se encuentra cancelada. Ingresa otra factura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return

                    End If

                    If (CStr(row("DocType")).ToString = "S") Then
                        DGDetalle.DataSource = Nothing
                        Label26.Visible = False
                        ComboBox1.Visible = False
                        TBCliente.Text = ""
                        TBNomCli.Text = ""
                        TBPerCon.Text = ""
                        TBDocDate.Text = ""
                        BSave.Enabled = False
                        MessageBox.Show("Esta factura es de servicio. Ingresa otra factura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If

                    TBDocNum.BackColor = Color.White
                    'TBCliente.BackColor = Color.White
                    'TBNomCli.BackColor = Color.White



                    BSave.Enabled = True
                    TBCliente.Text = CStr(row("CardCode"))
                    TBNomCli.Text = CStr(row("CardName"))
                    TBPerCon.Text = CStr(row("Telemarketing"))
                    'IIf(row(1) Is DbNull.Value, "", Convert.ToString(row(1)))         



                    TBDocDate.Text = CStr(row("docdate"))
                    TBDocEntry.Text = CStr(row("DocEntry"))







                    '--------------******************************************

                    '--------------******************************************
                    '--------------******************************************
                    conexion2.Open()
                    Dim cmd4 As SqlCommand = Nothing
                    cmd4 = New SqlCommand("ControlEnvios2", conexion2)
                    cmd4.CommandType = CommandType.StoredProcedure
                    cmd4.Parameters.Add("@docentry", SqlDbType.Int).Value = TBDocEntry.Text


                    cmd4.ExecuteNonQuery()
                    cmd4.Connection.Close()
                    Dim da2 As New SqlDataAdapter
                    da2.SelectCommand = cmd4
                    da2.SelectCommand.Connection = conexion2


                    ''--------------------------------------------
                    Dim DsVtas As New DataSet
                    da2.Fill(DsVtas, "DsVtas")

                    DsVtas.Tables(0).TableName = "Detalle"
                    DsVtas.Tables(1).TableName = "Peso"

                    DvDetalle.Table = DsVtas.Tables("Detalle")

                    DGDetalle.DataSource = DvDetalle

                    DataGridView1.DataSource = DsVtas.Tables("Peso")

                    Label24.Text = DataGridView1.Item(0, 0).Value & " Kg"
                    'Label24.FontSize = 18 'Tamaño
                    'Label24.FontName = "Symbol" 'Tipo
                    Label26.Visible = True
                    ComboBox1.Visible = True
                    DisenoGrid()

                Else
                    Label26.Visible = False
                    ComboBox1.Visible = False
                    DGDetalle.DataSource = Nothing
                    TBCliente.Text = ""
                    TBNomCli.Text = ""
                    TBPerCon.Text = ""
                    TBDocDate.Text = ""
                    BSave.Enabled = False
                    MsgBox("No hay factura con este folio")
                End If


            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
                    conexion.Close()
                End If
                If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
                    conexion2.Close()
                End If
            End Try



            '--------------******************************************
            ' ''ESTE CODIGO SE AGREGO EL SABADO
            conexion.Open()
            Dim cmd2 As SqlCommand = New SqlCommand("SELECT *, Convert(varchar,CONVERT(Time, DocDate),8) as 'Hora' FROM TPM.DBO.ControlEnviosHora " & _
            " WHERE Factura = @docnum ", conexion)
            cmd2.Parameters.AddWithValue("@docnum", TBDocNum.Text)

            'Dim dtFin As New DataTable
            Try

                Dim da3 As SqlDataAdapter = New SqlDataAdapter(cmd2)
                Dim dt2 As New DataTable
                da3.Fill(dt2)

                If dt2.Rows.Count > 0 Then

                    Dim row2 As DataRow = dt2.Rows(0)
                    TextBox1.Text = ""
                    TextBox1.Text = CStr(row2("Hora"))
                    'TextBox1.TextAlign = HorizontalAlignment.Center

                    ComboBox1.Text = CStr(row2("Empacador"))

                    Timer1.Enabled = False
                Else

                    TextBox1.Text = ""
                    ComboBox1.Text = ""
                    Timer1.Enabled = True
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
                    conexion.Close()
                End If
            End Try
            'Label26.Visible = True
            'ComboBox1.Visible = True




        End If  'TBDocNum.Text <> ""

        Try

            'Dim peso As Decimal = 0
            'For i = 0 To DGDetalle.RowCount
            '    peso = peso + DGDetalle.Item(17, i).Value
            'Next

            'Label24.Text = peso

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DisenoGrid()
        '-------Diseño de DATAGRID Totales
        With Me.DGDetalle
            '.DataSource = DtAgte
            '.ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .RowsDefaultCellStyle.Font = New Font("Arial", 9.5, FontStyle.Regular)

            For Each row As DataGridViewRow In Me.DGDetalle.Rows
                row.Height = 28
            Next






            '{
            '    row.Height =5;
            '}


            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'Color de linea del grid

            DGDetalle.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Try

                'Catch ex As Exception

                'End Try


                .Columns(0).HeaderText = "Número de Art."
                .Columns(0).Width = 150
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(0).ReadOnly = True

                .Columns(1).HeaderText = "Descripción"
                .Columns(1).Width = 500
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(1).ReadOnly = True

                .Columns(2).HeaderText = "Almacén"
                .Columns(2).Width = 70
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).ReadOnly = True

                .Columns(3).HeaderText = "Cantidad"
                .Columns(3).Width = 70
                .Columns(3).DefaultCellStyle.Format = "#,##0"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(3).ReadOnly = True

                .Columns(4).HeaderText = "Peso kg"
                .Columns(4).Width = 90
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(4).DefaultCellStyle.Format = "###,##0.#0"
                .Columns(4).ReadOnly = True

                .Columns(5).HeaderText = "Peso Total"
                .Columns(5).Width = 90
                .Columns(5).DefaultCellStyle.Format = "###,##0.#0"
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(6).HeaderText = "Caja"
                .Columns(6).Width = 70
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .Columns(7).HeaderText = "DocEntry"
                .Columns(7).Width = 60
                .Columns(7).Visible = False

            Catch ex As Exception

            End Try

        End With
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        DGDetalle.Columns.Clear()

        'Recorremos todos los controles del formulario que enviamos  
        For Each control As Control In Me.Controls

            'Filtramos solo aquellos de tipo TextBox 
            If TypeOf control Is TextBox Then
                control.Text = "" ' eliminar el texto  
            End If
        Next

        'TBDocNum.BackColor = Color.Cornsilk
        'TBCliente.BackColor = Color.Cornsilk
        'TBNomCli.BackColor = Color.Cornsilk

    End Sub

    Private Sub FacturaConsulta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TextBox1.Text = Now.ToString("hh:mm:ss")
        'Timer1.Enabled = True

        TBDocNum.BackColor = Color.Cornsilk
        TBCliente.BackColor = Color.White
        TBNomCli.BackColor = Color.White
        TBDocDate.BackColor = Color.White
        TBPerCon.BackColor = Color.White
    End Sub

    Private Sub DGDetalle_CurrentCellChanged(sender As Object, e As EventArgs) Handles DGDetalle.CurrentCellChanged

    End Sub

    Private Sub DGDetalle_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalle.CellEndEdit
        'If Me.DGDetalle.Columns(e.ColumnIndex).Name = "Seguimiento" Then

        Dim cnn As SqlConnection = Nothing

        Dim cmd4 As SqlCommand = Nothing

        'For i = 0 To DGGarantias.RowCount - 1

        Try

            cnn = New SqlConnection(StrTpm)

            'SELECT Estado 0,FecSuc 1,FecAlm 2,Factura 3,FecFac 4,DiasTransFecFacFecRecAlm 5,CardCode 6,CardName 7,
            'Sucursal 8, Almacen 9, Cantidad 10, ItemCode 11, ItemName 12, ItmsGrpNam 13, "
            'Proveedor 14,Id 15"


            cmd4 = New SqlCommand("UpdateControlEnvios", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@docentry", TBDocNum.Text)
            cmd4.Parameters.AddWithValue("@ITEMCODE", DGDetalle.Item(0, DGDetalle.CurrentRow.Index).Value)
            cmd4.Parameters.AddWithValue("@NUMCAJA", DGDetalle.Item(6, DGDetalle.CurrentRow.Index).Value)

            cnn.Open()

            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()

            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn

            ''--------------------------------------------
            'Dim DsVtas As New DataSet
            'da.Fill(DsVtas, "DsVtas")

            ''DsVtas.Tables(0).TableName = "Inventario"

            ''DvInventario.Table = DsVtas.Tables("Inventario")

            ''DGInventario.DataSource = DvInventario

        Catch ex As Exception
            'Return
            'MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try

        'Next

        'End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TextBox1.Text = Now.ToString("hh:mm:ss")
        TextBox1.TextAlign = HorizontalAlignment.Center
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        Dim cnn As SqlConnection = Nothing

        Dim cmd4 As SqlCommand = Nothing

        'For i = 0 To DGGarantias.RowCount - 1

        Try

            cnn = New SqlConnection(StrTpm)

            'SELECT Estado 0,FecSuc 1,FecAlm 2,Factura 3,FecFac 4,DiasTransFecFacFecRecAlm 5,CardCode 6,CardName 7,
            'Sucursal 8, Almacen 9, Cantidad 10, ItemCode 11, ItemName 12, ItmsGrpNam 13, "
            'Proveedor 14,Id 15"


            cmd4 = New SqlCommand("UpdateControlEnviosHora", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@docentry", TBDocNum.Text)
            cmd4.Parameters.AddWithValue("@Hora", TextBox1.Text)
            cmd4.Parameters.AddWithValue("@Empacador", ComboBox1.Text)

            cnn.Open()

            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()

            Dim da As New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn

            ''--------------------------------------------
            'Dim DsVtas As New DataSet
            'da.Fill(DsVtas, "DsVtas")

            ''DsVtas.Tables(0).TableName = "Inventario"

            ''DvInventario.Table = DsVtas.Tables("Inventario")

            ''DGInventario.DataSource = DvInventario

        Catch ex As Exception
            'Return
            MsgBox(ex.Message)
        Finally
            If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                cnn.Close()
            End If
        End Try
    End Sub



End Class
