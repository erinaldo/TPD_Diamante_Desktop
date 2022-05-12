

Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Data.SqlClient

Public Class ReciboMatCalendario

    Public MesSelec As Integer
    Public AñoSelec As Integer
    Public fecha As Date
    Public Almacen As String

    Public StrProd As String = conexion_universal.CadenaSBO_Diamante
    Public StrTpm As String = conexion_universal.CadenaSQL
    Public StrCon As String = conexion_universal.CadenaSQLSAP

    Dim DvTotales1 As New DataView

#Region "Procedimientos"
    Private Function PosicionPrimerDia() As Integer
        Dim MyDate As Date
        Dim Posicion As Integer
        MyDate = DateSerial(Module1.anio, Module1.aux, 1)
        Posicion = Weekday(MyDate, FirstDayOfWeek.Monday)
        Return Posicion
    End Function

    Sub Colocacion()

        'TableLayoutPanel1.Controls.Clear()
        'TableLayoutPanel1.RowStyles.Clear()

        Try

            'Dim NumeroDias() As Integer = {0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
            Dim NumeroDias() As Integer = {0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}

            Dim AñoBisiesto As Integer
            Dim ListadoExcluir() As String = {"DiaSemana1", "DiaSemana2", "DiaSemana3", "DiaSemana4", "DiaSemana5", "DiaSemana6", "DiaSemana7"}
            Dim DiasDelMes As New List(Of Label)
            Dim DiasGrid As New List(Of DataGridView)
            Dim Conta, Conta2 As Integer
            Dim Salto As Boolean = False

            AñoSelec = Module1.anio
            MesSelec = Module1.aux
            Almacen = Module1.almacenCalen

            AñoBisiesto = AñoSelec Mod 4    'instruccion mod devuelve el residuo de una division ej. 10 mod 3 = 1

            If AñoBisiesto = 0 Then
                NumeroDias(2) = 29
            End If
            For Each NombreLabel In Me.TableLayoutPanel1.Controls
                If TypeOf NombreLabel Is Label And Not Array.IndexOf(ListadoExcluir, NombreLabel.Name) > -1 Then
                    DiasDelMes.Add(NombreLabel)
                End If
            Next
            DiasDelMes.Reverse()
            For Each Dia In DiasDelMes
                Dim DiaCompa, NombreLis, NumeroDia() As String
                Dim AsociacionLista As DataGridView
                NumeroDia = Split(Dia.Name, "txtDiaMes")
                NombreLis = "lisDiaMes" & NumeroDia(1)
                AsociacionLista = Me.TableLayoutPanel1.Controls.Item(Me.TableLayoutPanel1.Controls.IndexOfKey(NombreLis))
                Conta += 1
                Conta2 += 1
                Dia.Hide()
                Dia.Text = Nothing
                AsociacionLista.Hide()
                If Conta = PosicionPrimerDia() And Salto = False Then
                    Conta = 1
                    Salto = True
                    AsociacionLista.Show()
                    Dia.Show()
                    Dia.Text = Conta

                    '*------------------------------------------------------------------------
                    '*------------------------------------------------------------------------

                    Dim cnn As SqlConnection = Nothing
                    Dim cmd4 As SqlCommand = Nothing


                    Try

                        cnn = New SqlConnection(StrTpm)
                        cnn.Open()

                        cmd4 = New SqlCommand("SPReciboMat", cnn)
                        cmd4.CommandType = CommandType.StoredProcedure

                        fecha = Conta & "/" & MesSelec & "/" & AñoSelec
                        cmd4.Parameters.Add("@Primer", SqlDbType.Date).Value = fecha

                        'Module1.almacenCalen = CBAlmacen.Text
                        cmd4.Parameters.Add("@Almacen", SqlDbType.VarChar).Value = Almacen

                        cmd4.ExecuteNonQuery()
                        cmd4.Connection.Close()
                        Dim da As New SqlDataAdapter
                        da.SelectCommand = cmd4
                        da.SelectCommand.Connection = cnn

                        ''--------------------------------------------
                        Dim DsVtas As New DataSet
                        da.Fill(DsVtas, "DsVtas")

                        DsVtas.Tables(0).TableName = "Totales"


                        DvTotales1.Table = DsVtas.Tables("Totales")

                        AsociacionLista.DataSource = DvTotales1

                        'AsociacionLista.ClearSelection()

                    Catch ex As Exception
                        'MsgBox(ex.Message)
                        'MsgBox("No existen ventas de este día")
                    Finally
                        If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                            cnn.Close()
                        End If
                    End Try

                    With AsociacionLista

                        '.CurrentRow.Selected = False

                        .ReadOnly = True
                        'Color de Renglones en Grid
                        '.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                        '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                        '.DefaultCellStyle.BackColor = Color.AliceLightSkyBlue
                        '.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue


                        'Propiedad para no mostrar el cuadro que se encuentra en la parte
                        'Superior Izquierda del gridview
                        .RowHeadersVisible = False
                        '.RowHeaders()

                        '.RowHeadersWidth = 25
                        '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                        'Color de linea del grid

                        .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                        'Orden de Compra	
                        .Columns(0).HeaderText = "Ord. Compra"
                        .Columns(0).Width = 35
                        .Columns(0).ReadOnly = True

                        'Descripcion	
                        .Columns(1).HeaderText = "Prov."
                        .Columns(1).Width = 170
                        .Columns(1).Frozen = True
                        .Columns(1).ReadOnly = True

                        'Linea	
                        .Columns(2).HeaderText = "Comentarios"
                        .Columns(2).Width = 20
                        .Columns(2).Frozen = True
                        .Columns(2).ReadOnly = True

                        .ClearSelection()

                        .AllowUserToAddRows = False



                    End With


                    '*------------------------------------------------------------------------
                    '*------------------------------------------------------------------------

                ElseIf Conta2 > PosicionPrimerDia() And Conta <= NumeroDias(MesSelec) Then
                    AsociacionLista.Show()
                    Dia.Show()
                    Dia.Text = Conta

                    '*****------------------------------------------------------------------------
                    'For Each NombreDG In Me.TableLayoutPanel1.Controls
                    'If TypeOf NombreDG Is DataGridView Then
                    '    DiasGrid.Add(NombreDG)

                    '-------*************

                    Dim cnn As SqlConnection = Nothing
                    Dim cmd4 As SqlCommand = Nothing

                    Dim DvTotales2 As New DataView

                    Try

                        'For i As Integer = 2 To NumeroDias(MesSelec)

                        cnn = New SqlConnection(StrTpm)
                        cnn.Open()

                        cmd4 = New SqlCommand("SPReciboMat2", cnn)
                        cmd4.CommandType = CommandType.StoredProcedure
                        'MsgBox(Conta)
                        fecha = Conta & "/" & MesSelec & "/" & AñoSelec
                        cmd4.Parameters.Add("@Fecha", SqlDbType.Date).Value = fecha
                        cmd4.Parameters.Add("@Almacen", SqlDbType.VarChar).Value = Almacen

                        cmd4.ExecuteNonQuery()
                        cmd4.Connection.Close()
                        Dim da As New SqlDataAdapter
                        da.SelectCommand = cmd4
                        da.SelectCommand.Connection = cnn

                        Dim DsVtas As New DataSet
                        da.Fill(DsVtas, "DsVtas" & Conta)

                        DsVtas.Tables(0).TableName = "Totales" & Conta

                        DvTotales2.Table = DsVtas.Tables("Totales" & Conta)


                        AsociacionLista.DataSource = DvTotales2



                        'Next

                        With AsociacionLista

                            '.CurrentRow.Selected = False

                            .ReadOnly = True
                            'Color de Renglones en Grid
                            '.AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                            '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                            '.DefaultCellStyle.BackColor = Color.AliceLightSkyBlue
                            '.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue


                            'Propiedad para no mostrar el cuadro que se encuentra en la parte
                            'Superior Izquierda del gridview

                            .RowHeadersVisible = False
                            '.RowHeadersWidth = 25
                            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                            'Color de linea del grid

                            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                            'Orden de Compra	
                            .Columns(0).HeaderText = "Ord. Compra"
                            .Columns(0).Width = 35
                            .Columns(0).ReadOnly = True
                            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                            'Descripcion	
                            .Columns(1).HeaderText = "Prov."
                            .Columns(1).Width = 170
                            .Columns(1).Frozen = True
                            .Columns(1).ReadOnly = True

                            'Linea	
                            .Columns(2).HeaderText = "Comentarios"
                            .Columns(2).Width = 50
                            .Columns(2).Frozen = True
                            .Columns(2).ReadOnly = True


                            .ClearSelection()
                            ' .CurrentRow.Selected = False

                            .AllowUserToAddRows = False

                        End With


                        'AsociacionLista.ClearSelection()

                        ''--------------------------------------------


                    Catch ex As Exception
                        'MsgBox(ex.Message)
                        'MsgBox("No existen ventas de este día")
                    Finally
                        If cnn IsNot Nothing AndAlso cnn.State <> ConnectionState.Closed Then
                            cnn.Close()
                        End If
                    End Try

                    '-------*************

                    'End If

                    'Next

                    'DiasGrid.Reverse()

                End If
                DiaCompa = Conta & "/" & MesSelec & "/" & AñoSelec
                If Date.Today.ToString("dd/M/yyyy") = DiaCompa Then
                    With Dia
                        .BackColor = Color.FromArgb(0, 0, 0)
                        .ForeColor = Color.White
                        .Margin = New Padding(0)
                    End With
                Else
                    With Dia
                        .BackColor = Color.FromArgb(224, 224, 224)
                        .ForeColor = Color.Black
                        .Margin = New Padding(3)
                    End With
                End If
            Next

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub
#End Region

    Private Sub MesControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.Size = New System.Drawing.Size(3000, 3500)
        'Me.WindowState = FormWindowState.Maximized

        'Variable para guardar la consulta de AGENTES y SUCURSALES en los combobox
        Dim ConsutaLista As String


        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)


            Dim DSetTablas As New DataSet
            ConsutaLista = "SELECT WhsCode,WhsName FROM OWHS WHERE WHSCODE IN (01,03,07) "
            Dim daGSucural As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            'Dim DSetTablas As New DataSet
            daGSucural.Fill(DSetTablas, "Sucursales")

            Dim fila As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            fila = DSetTablas.Tables("Sucursales").NewRow

            'Agregamos los valores a los campos de la tabla
            fila("WhsName") = "TODOS"
            fila("WhsCode") = 99

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Sucursales").Rows.Add(fila)

            Me.CBAlmacen.DataSource = DSetTablas.Tables("Sucursales")
            Me.CBAlmacen.DisplayMember = "WhsName"
            Me.CBAlmacen.ValueMember = "WhsCode"
            Me.CBAlmacen.SelectedValue = 99


            '---------------------------------------------------------

        End Using

        'MsgBox(Month(Today))

        'Maximiza el formulario al abrirlo

        'Me.WindowState = FormWindowState.Maximized


        CBAnio.SelectedText = Year(Today)

        Module1.aux = Month(Today)

        Module1.anio = CBAnio.Text

        Module1.almacenCalen = CBAlmacen.Text
        'asignamos nombre del mes al textbox

        SeleccionaMes()

        'Coloca etiquetas con numero de dia, agrega datagridviews 
        Colocacion()

        'Me.Size = New System.Drawing.Size(2000, 2500)

        'Me.Size = New Size(2000, 3000)

    End Sub


    Private Sub SeleccionaMes()

        Dim auxM As Integer

        'obtener nombre del mes
        Module1.mes = Month(Today)

        Select Case mes
            Case 1
                TBMes.Text = "Enero"
                auxM = 1
            Case 2
                TBMes.Text = "Febrero"
                auxM = 2
            Case 3
                TBMes.Text = "Marzo"
                auxM = 3
            Case 4
                TBMes.Text = "Abril"
                auxM = 4
            Case 5
                TBMes.Text = "Mayo"
                auxM = 5
            Case 6
                TBMes.Text = "Junio"
                auxM = 6
            Case 7
                auxM = 7
                TBMes.Text = "Julio"
            Case 8
                TBMes.Text = "Agosto"
                auxM = 8
            Case 9
                TBMes.Text = "Septiembre"
                auxM = 9
            Case 10
                TBMes.Text = "Octubre"
                auxM = 10
            Case 11
                TBMes.Text = "Noviembre"
                auxM = 11
            Case 12
                TBMes.Text = "Diciembre"
                auxM = 12
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Module1.aux = Module1.aux - 1

        If Module1.aux < 1 Then
            Module1.aux = 12
            Module1.anio = Module1.anio - 1
            CBAnio.Text = Module1.anio
        End If

        'MsgBox(Module1.aux)

        Select Case aux
            Case 1
                TBMes.Text = "Enero"
                aux = 1
            Case 2
                TBMes.Text = "Febrero"
                aux = 2
            Case 3
                TBMes.Text = "Marzo"
                aux = 3
            Case 4
                TBMes.Text = "Abril"
                aux = 4
            Case 5
                TBMes.Text = "Mayo"
                aux = 5
            Case 6
                TBMes.Text = "Junio"
                aux = 6
            Case 7
                aux = 7
                TBMes.Text = "Julio"
            Case 8
                TBMes.Text = "Agosto"
                aux = 8
            Case 9
                TBMes.Text = "Septiembre"
                aux = 9
            Case 10
                TBMes.Text = "Octubre"
                aux = 10
            Case 11
                TBMes.Text = "Noviembre"
                aux = 11
            Case 12
                TBMes.Text = "Diciembre"
                aux = 12
        End Select

        Colocacion()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Module1.aux = Module1.aux + 1

        If Module1.aux > 12 Then
            Module1.aux = 1
            Module1.anio = Module1.anio + 1
            CBAnio.Text = Module1.anio
        End If

        'MsgBox(Module1.aux)

        Select Case aux
            Case 1
                TBMes.Text = "Enero"
                aux = 1
            Case 2
                TBMes.Text = "Febrero"
                aux = 2
            Case 3
                TBMes.Text = "Marzo"
                aux = 3
            Case 4
                TBMes.Text = "Abril"
                aux = 4
            Case 5
                TBMes.Text = "Mayo"
                aux = 5
            Case 6
                TBMes.Text = "Junio"
                aux = 6
            Case 7
                aux = 7
                TBMes.Text = "Julio"
            Case 8
                TBMes.Text = "Agosto"
                aux = 8
            Case 9
                TBMes.Text = "Septiembre"
                aux = 9
            Case 10
                TBMes.Text = "Octubre"
                aux = 10
            Case 11
                TBMes.Text = "Noviembre"
                aux = 11
            Case 12
                TBMes.Text = "Diciembre"
                aux = 12
        End Select

        Colocacion()

    End Sub


    Private Sub lisDiaMes1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes1.RowPrePaint
        If Not IsDBNull(lisDiaMes1.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes1.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes1.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes1.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes1.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes1.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes1.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes1.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes1.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes1.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes1.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes1.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes1.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub


    Private Sub lisDiaMes2_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes2.RowPrePaint
        If Not IsDBNull(lisDiaMes2.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes2.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes2.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes2.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes2.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes2.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes2.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes2.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes2.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes2.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes2.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes2.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes2.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub


    Private Sub lisDiaMes3_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes3.RowPrePaint
        If Not IsDBNull(lisDiaMes3.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes3.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes3.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes3.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes3.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes3.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes3.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes3.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes3.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes3.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes3.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes3.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes3.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes4_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes4.RowPrePaint
        If Not IsDBNull(lisDiaMes4.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes4.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes4.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes4.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes4.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes4.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes4.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes4.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes4.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes4.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes4.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes4.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes4.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes5_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes5.RowPrePaint
        If Not IsDBNull(lisDiaMes5.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes5.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes5.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes5.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes5.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes5.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes5.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes5.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes5.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes5.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes5.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes5.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes5.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes6_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes6.RowPrePaint
        If Not IsDBNull(lisDiaMes6.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes6.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes6.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes6.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes6.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes6.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes6.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes6.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes6.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes6.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes6.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes6.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes6.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes7_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes7.RowPrePaint
        If Not IsDBNull(lisDiaMes7.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes7.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes7.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes7.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes7.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes7.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes7.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes7.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes7.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes7.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes7.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes7.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes7.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes8_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes8.RowPrePaint
        If Not IsDBNull(lisDiaMes8.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes8.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes8.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes8.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes8.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes8.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes8.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes8.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes8.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes8.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes8.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes8.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes8.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes9_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes9.RowPrePaint
        If Not IsDBNull(lisDiaMes9.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes9.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes9.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes9.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes9.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes9.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes9.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes9.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes9.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes9.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes9.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes9.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes9.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes10_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes10.RowPrePaint
        If Not IsDBNull(lisDiaMes10.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes10.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes10.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes10.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes10.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes10.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes10.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes10.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes10.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes10.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes10.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes10.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes10.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes11_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes11.RowPrePaint
        If Not IsDBNull(lisDiaMes11.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes11.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes11.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes11.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes11.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes11.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes11.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes11.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes11.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes11.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes11.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes11.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes11.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes12_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes12.RowPrePaint
        If Not IsDBNull(lisDiaMes12.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes12.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes12.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes12.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes12.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes12.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes12.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes12.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes12.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes12.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes12.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes12.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes12.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes13_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes13.RowPrePaint
        If Not IsDBNull(lisDiaMes13.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes13.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes13.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes13.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes13.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes13.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes13.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes13.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes13.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes13.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes13.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes13.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes13.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub


    Private Sub lisDiaMes14_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes14.RowPrePaint
        If Not IsDBNull(lisDiaMes14.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes14.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes14.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes14.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes14.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes14.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes14.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes14.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes14.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes14.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes14.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes14.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes14.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes15_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes15.RowPrePaint
        If Not IsDBNull(lisDiaMes15.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes15.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes15.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes15.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes15.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes15.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes15.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes15.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes15.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes15.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes15.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes15.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes15.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes16_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes16.RowPrePaint
        If Not IsDBNull(lisDiaMes16.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes16.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes16.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes16.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes16.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes16.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes16.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes16.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes16.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes16.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes16.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes16.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes16.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub


    Private Sub lisDiaMes17_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes17.RowPrePaint
        If Not IsDBNull(lisDiaMes17.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes17.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes17.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes17.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes17.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes17.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes17.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes17.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes17.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes17.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes17.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes17.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes17.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub


    Private Sub lisDiaMes18_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes18.RowPrePaint
        If Not IsDBNull(lisDiaMes18.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes18.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes18.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes18.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes18.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes18.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes18.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes18.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes18.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes18.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes18.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes18.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes18.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes19_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes19.RowPrePaint
        If Not IsDBNull(lisDiaMes19.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes19.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes19.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes19.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes19.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes19.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes19.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes19.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes19.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes19.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes19.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes19.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes19.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes20_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes20.RowPrePaint
        If Not IsDBNull(lisDiaMes20.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes20.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes20.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes20.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes20.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes20.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes20.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes20.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes20.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes20.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes20.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes20.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes20.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes21_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes21.RowPrePaint
        If Not IsDBNull(lisDiaMes21.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes21.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes21.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes21.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes21.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes21.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes21.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes21.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes21.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes21.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes21.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes21.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes21.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes22_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes22.RowPrePaint
        If Not IsDBNull(lisDiaMes22.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes22.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes22.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes22.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes22.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes22.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes22.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes22.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes22.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes22.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes22.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes22.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes22.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes23_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes23.RowPrePaint
        If Not IsDBNull(lisDiaMes23.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes23.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes23.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes23.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes23.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes23.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes23.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes23.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes23.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes23.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes23.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes23.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes23.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes24_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes24.RowPrePaint
        If Not IsDBNull(lisDiaMes24.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes24.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes24.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes24.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes24.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes24.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes24.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes24.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes24.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes24.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes24.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes24.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes24.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes25_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes25.RowPrePaint
        If Not IsDBNull(lisDiaMes25.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes25.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes25.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes25.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes25.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes25.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes25.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes25.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes25.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes25.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes25.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes25.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes25.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes26_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes26.RowPrePaint
        If Not IsDBNull(lisDiaMes26.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes26.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes26.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes26.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes26.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes26.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes26.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes26.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes26.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes26.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes26.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes26.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes26.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes27_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes27.RowPrePaint
        If Not IsDBNull(lisDiaMes27.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes27.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes27.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes27.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes27.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes27.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes27.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes27.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes27.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes27.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes27.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes27.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes27.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes28_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes28.RowPrePaint
        If Not IsDBNull(lisDiaMes28.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes28.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes28.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes28.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes28.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes28.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes28.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes28.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes28.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes28.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes28.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes28.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes28.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes29_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes29.RowPrePaint
        If Not IsDBNull(lisDiaMes29.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes29.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes29.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes29.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes29.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes29.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes29.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes29.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes29.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes29.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes29.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes29.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes29.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes30_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes30.RowPrePaint
        If Not IsDBNull(lisDiaMes30.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes30.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes30.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes30.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes30.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes30.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes30.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes30.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes30.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes30.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes30.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes30.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes30.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes31_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes31.RowPrePaint
        If Not IsDBNull(lisDiaMes31.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes31.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes31.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes31.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes31.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes31.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes31.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes31.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes31.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes31.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes31.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes31.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes31.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes32_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes32.RowPrePaint
        If Not IsDBNull(lisDiaMes32.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes32.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes32.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes32.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes32.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes32.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes32.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes32.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes32.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes32.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes32.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes32.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes32.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes33_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes33.RowPrePaint
        If Not IsDBNull(lisDiaMes33.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes33.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes33.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes33.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes33.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes33.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes33.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes33.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes33.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes33.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes33.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes33.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes33.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes34_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes34.RowPrePaint
        If Not IsDBNull(lisDiaMes34.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes34.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes34.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes34.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes34.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes34.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes34.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes34.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes34.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes34.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes34.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes34.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes34.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes35_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes35.RowPrePaint
        If Not IsDBNull(lisDiaMes35.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes35.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes35.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes35.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes35.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes35.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes35.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes35.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes35.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes35.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes35.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes35.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes35.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes36_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes36.RowPrePaint
        If Not IsDBNull(lisDiaMes36.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes36.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes36.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes36.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes36.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes36.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes36.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes36.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes36.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes36.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes36.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes36.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes36.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub


    Private Sub lisDiaMes37_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes37.RowPrePaint
        If Not IsDBNull(lisDiaMes37.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes37.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes37.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes37.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes37.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes37.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes37.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes37.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes37.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes37.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes37.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes37.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes37.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes38_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes38.RowPrePaint
        If Not IsDBNull(lisDiaMes38.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes38.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes38.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes38.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes38.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes38.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes38.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes38.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes38.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes38.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes38.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes38.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes38.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes39_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes39.RowPrePaint
        If Not IsDBNull(lisDiaMes39.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes39.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes39.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes39.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes39.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes39.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes39.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes39.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes39.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes39.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes39.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes39.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes39.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub


    Private Sub lisDiaMes40_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes40.RowPrePaint
        If Not IsDBNull(lisDiaMes40.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes40.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes40.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes40.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes40.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes40.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes40.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes40.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes40.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes40.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes40.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes40.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes40.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes41_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes41.RowPrePaint
        If Not IsDBNull(lisDiaMes41.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes41.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes41.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes41.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes41.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes41.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes41.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes41.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes41.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes41.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes41.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes41.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes41.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub

    Private Sub lisDiaMes42_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles lisDiaMes42.RowPrePaint
        If Not IsDBNull(lisDiaMes42.Rows(e.RowIndex).Cells(2).Value) Then

            If lisDiaMes42.Rows(e.RowIndex).Cells(2).Value = "PUEBLA" Then

                lisDiaMes42.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightSkyBlue
                lisDiaMes42.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightSkyBlue
                lisDiaMes42.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightSkyBlue

            ElseIf lisDiaMes42.Rows(e.RowIndex).Cells(2).Value = "MÉRIDA" Then

                lisDiaMes42.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightCoral
                lisDiaMes42.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightCoral
                lisDiaMes42.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightCoral

            ElseIf lisDiaMes42.Rows(e.RowIndex).Cells(2).Value = "TUXTLA GTZ" Then

                lisDiaMes42.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.LightGreen
                lisDiaMes42.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.LightGreen
                lisDiaMes42.Rows(e.RowIndex).Cells(2).Style.BackColor = Color.LightGreen

            End If

        End If
    End Sub


    Private Sub CBAnio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBAnio.SelectedIndexChanged
        Module1.anio = CBAnio.Text
        Colocacion()
    End Sub

    'Private Sub CBAlmacen_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBAlmacen.SelectionChangeCommitted
    '    Try
    '        Module1.almacenCalen = CBAlmacen.Text
    '        Colocacion()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub CBAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBAlmacen.SelectedIndexChanged
        Try
        Module1.almacenCalen = CBAlmacen.Text
            Colocacion()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub lisDiaMes1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes1.CellContentClick

        Module1.detalle = lisDiaMes1.Item(0, lisDiaMes1.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()

    End Sub

    Private Sub lisDiaMes2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes2.CellContentClick
        Module1.detalle = lisDiaMes2.Item(0, lisDiaMes2.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes3.CellContentClick
        Module1.detalle = lisDiaMes3.Item(0, lisDiaMes3.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes4.CellContentClick
        Module1.detalle = lisDiaMes4.Item(0, lisDiaMes4.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes5_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes5.CellContentClick
        Module1.detalle = lisDiaMes5.Item(0, lisDiaMes5.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes6_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes6.CellContentClick
        Module1.detalle = lisDiaMes6.Item(0, lisDiaMes6.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes8_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes8.CellContentClick
        Module1.detalle = lisDiaMes8.Item(0, lisDiaMes8.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes9_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes9.CellContentClick
        Module1.detalle = lisDiaMes9.Item(0, lisDiaMes9.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes10_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes10.CellContentClick
        Module1.detalle = lisDiaMes10.Item(0, lisDiaMes10.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes11_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes11.CellContentClick
        Module1.detalle = lisDiaMes11.Item(0, lisDiaMes11.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes12_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes12.CellContentClick
        Module1.detalle = lisDiaMes12.Item(0, lisDiaMes12.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes13_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes13.CellContentClick
        Module1.detalle = lisDiaMes13.Item(0, lisDiaMes13.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes15_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes15.CellContentClick
        Module1.detalle = lisDiaMes15.Item(0, lisDiaMes15.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes16_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes16.CellContentClick
        Module1.detalle = lisDiaMes16.Item(0, lisDiaMes16.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes17_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes17.CellContentClick
        Module1.detalle = lisDiaMes17.Item(0, lisDiaMes17.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes18_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes18.CellContentClick
        Module1.detalle = lisDiaMes18.Item(0, lisDiaMes18.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes19_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes19.CellContentClick
        Module1.detalle = lisDiaMes19.Item(0, lisDiaMes19.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes20_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes20.CellContentClick
        Module1.detalle = lisDiaMes20.Item(0, lisDiaMes20.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes22_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes22.CellContentClick
        Module1.detalle = lisDiaMes22.Item(0, lisDiaMes22.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes23_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes23.CellContentClick
        Module1.detalle = lisDiaMes23.Item(0, lisDiaMes23.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes24_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes24.CellContentClick
        Module1.detalle = lisDiaMes24.Item(0, lisDiaMes24.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes25_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes25.CellContentClick
        Module1.detalle = lisDiaMes25.Item(0, lisDiaMes25.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes26_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes26.CellContentClick
        Module1.detalle = lisDiaMes26.Item(0, lisDiaMes26.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes27_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes27.CellContentClick
        Module1.detalle = lisDiaMes27.Item(0, lisDiaMes27.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes29_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes29.CellContentClick
        Module1.detalle = lisDiaMes29.Item(0, lisDiaMes29.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes30_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes30.CellContentClick
        Module1.detalle = lisDiaMes30.Item(0, lisDiaMes30.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes31_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes31.CellContentClick
        Module1.detalle = lisDiaMes31.Item(0, lisDiaMes31.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes32_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes32.CellContentClick
        Module1.detalle = lisDiaMes32.Item(0, lisDiaMes32.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes33_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes33.CellContentClick
        Module1.detalle = lisDiaMes33.Item(0, lisDiaMes33.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes34_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes34.CellContentClick
        Module1.detalle = lisDiaMes34.Item(0, lisDiaMes34.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub
    
    Private Sub lisDiaMes36_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes36.CellContentClick
        Module1.detalle = lisDiaMes36.Item(0, lisDiaMes36.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub
    Private Sub lisDiaMes37_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes37.CellContentClick
        Module1.detalle = lisDiaMes37.Item(0, lisDiaMes37.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub
    Private Sub lisDiaMes38_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes38.CellContentClick
        Module1.detalle = lisDiaMes38.Item(0, lisDiaMes38.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes39_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes39.CellContentClick
        Module1.detalle = lisDiaMes39.Item(0, lisDiaMes39.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes40_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes40.CellContentClick
        Module1.detalle = lisDiaMes40.Item(0, lisDiaMes40.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub

    Private Sub lisDiaMes41_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles lisDiaMes41.CellContentClick
        Module1.detalle = lisDiaMes41.Item(0, lisDiaMes41.CurrentCell.RowIndex).Value

        ReciboMatDetalle.Show()
    End Sub




End Class
