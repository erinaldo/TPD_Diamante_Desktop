Imports System.Data.SqlClient

Public Class CargaCombustible

    Dim odometro_anterior, odometro_nuevo As Integer
    Dim band As Boolean = False
    Dim aux As String
    Dim sconec As String = StrTpm
    Dim objcommand As SqlCommand
    Dim OBJDR As SqlDataReader
    Dim count_row As Integer = 0
    Public placa As String = ""

    Private Sub CargaCombustible_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim numero As Integer
        'numero = 118000
        'MsgBox(numero.ToString(("###,##0")))


        aux = sender.ToString
        path_form.Text = "Form: " & aux.Substring(aux.IndexOf(".") + 1, (aux.IndexOf(",") - aux.IndexOf(".")) - 1) & ".vb"
        TextBox5.Focus()
        ComboBox1.SelectedIndex = 0
        dt1.Value = Date.Now
        Label1.Text = UsrTPM

        objcommand = New SqlCommand

        'objcommand.CommandText = "SELECT T1.Placas,T1.Marca,T1.Modelo,T1.Año,T1.Agente,odometro2, " &
        '                        "convert(DATE,T2.fecha) from usuarios t0 inner join Coches t1 on t0.CodAgte = t1.Agente " &
        '                        "INNER JOIN Carga2 T2 ON T1.Placas = T2.placas where t0.Id_Usuario = '" & UsrTPM & "' ORDER BY T2.Fecha ASC "


        objcommand.CommandText = "select COUNT(*) from Coches t0 inner join Usuarios t1 on t0.Agente = t1.CodAgte where t1.Id_Usuario = '" & UsrTPM & "'"
        objcommand.Connection = New SqlConnection(sconec)
        objcommand.Connection.Open()
        OBJDR = objcommand.ExecuteReader()
        OBJDR.Read()

        If OBJDR.Item(0).ToString() = "1" Then
            band_carga_combustible = True

            '******************************************************ANTERIOR
            'objcommand.CommandText = "select t0.Placas, t0.Marca, t0.Modelo, t0.Año, t0.Agente, t0.odometro2, " &
            '                   "convert(DATE,(select MAX(fecha) from Carga2 t4 where t4.placas = t0.Placas)) from Coches t0 " &
            '                   "inner join Usuarios t1 on t0.Agente = t1.CodAgte where t1.Id_Usuario = '" & UsrTPM & "'"
            '******************************************************ANTERIOR
            'MODIFICADO POR URIEL
            objcommand.CommandText = "select t0.Placas, t0.Marca, t0.Modelo, t0.Año, t0.Agente, t0.odometro2, " &
                               "convert(DATE,(select MAX(fecha) from Carga2 t4 where t4.placas = t0.Placas)) from Coches t0 " &
                               "inner join Usuarios t1 on t0.Agente = t1.CodAgte where t1.Id_Usuario = '" & UsrTPM & "'"
            objcommand.Connection = New SqlConnection(sconec)
            objcommand.Connection.Open()
            OBJDR = objcommand.ExecuteReader()
            'MsgBox(OBJDR.)
            If OBJDR.HasRows Then
                While OBJDR.Read()
                    Label2.Text = OBJDR.Item(0).ToString()
                    placa = OBJDR.Item(0).ToString()
                    Label4.Text = OBJDR.Item(2).ToString
                    Label6.Text = OBJDR.Item(3).ToString
                    'Label17.Text = OBJDR.Item(5).ToString
                    Label17.Text = (CInt(OBJDR.Item(5))).ToString(("###,##0"))
                    'MsgBox(OBJDR.Item(6).ToString)
                    If OBJDR.Item(6).ToString <> "" Then
                        Label12.Text = OBJDR.Item(6)
                    Else
                        Label12.Text = ""

                    End If


                End While
                odometro_anterior = CInt(Label17.Text)
            Else
                MsgBox("no hay datos que mostrar")

            End If
        ElseIf OBJDR.Item(0).ToString() > "1" Then
            Dim frm As New Select_Vehicle
            frm.ShowDialog()
        End If
        

        'MsgBox(OBJDR.Item(0).ToString)

        'Return
        OBJDR.Close()
        objcommand.Dispose()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim a, b, c As Integer
        If Verifica_Campos() = True Then 'If para verificar que todos los campos esten capturados correctamente'
            'MessageBox.Show("todos los campos son correctos", "todo bien", MessageBoxButtons.OK, MessageBoxIcon.None)
            If (MessageBox.Show("Resumen de Captura:" & vbCrLf & vbCrLf & "Fecha de Carga: " & String.Format("{0:dd/MM/yyyy}", dt1.Value) & vbCrLf & "Odometro nuevo: " & (CInt(TextBox4.Text)).ToString(("###,##0")) & " Km" & vbCrLf & "Precio Litro: $ " & TextBox2.Text & vbCrLf & "Costo de la Carga: $ " & TextBox5.Text & vbCrLf & vbCrLf & "¿Confirma que desea guardar estos datos?", _
                         "Por favor, verifica la información", _
                         MessageBoxButtons.YesNo, _
                        MessageBoxIcon.Exclamation)) = MsgBoxResult.Yes Then

                If Label17.Text = "" Then
                    Dim sconec As String = StrTpm
                    Dim objcommand As New SqlCommand
                    objcommand.CommandText = "SELECT T1.kmsinicial from usuarios t0 inner join Coches t1 on t0.CodAgte = t1.Agente where t0.Id_Usuario = '" & UsrTPM & "' and t1.Placas = '" & placa & "'"
                    objcommand.Connection = New SqlConnection(sconec)
                    objcommand.Connection.Open()
                    Dim OBJDR As SqlDataReader = objcommand.ExecuteReader()
                    If OBJDR.HasRows Then
                        While OBJDR.Read()
                            Label17.Text = OBJDR.Item(0).ToString

                        End While
                    Else
                        MsgBox("no hay datos que mostrar")
                    End If
                    OBJDR.Close()
                    objcommand.Dispose()
                End If
                a = CInt(Label17.Text)
                b = CInt(TextBox4.Text)
                c = b - a
                Dim conexion As New SqlConnection(StrTpm)
                Dim str As String = "insert into carga2([fecha],[placas],[cantidad],[precio],[costocarga],[odometro],[tipogaso],[comentarios],kmsrecord) values('" & String.Format("{0:yyyyMMdd}", dt1.Value) & "','" & Label2.Text & "','" & TextBox1.Text & "'," & CDec(TextBox2.Text) & ",'" & CDec(TextBox5.Text) & "','" & CInt(TextBox4.Text) & "','" & ComboBox1.Text & "','" & TextBox3.Text & "'," & c & " )"
                Dim command1 As SqlCommand = New SqlCommand(str, conexion)
                command1.Connection = conexion
                Try
                    conexion.Open()
                    command1.ExecuteNonQuery()


                Catch ex As Exception
                    MessageBox.Show(ex.Message)

                Finally
                    conexion.Dispose()
                    command1.Dispose()
                End Try

                'Label16.Text = "  DATOS GUARDADOS SATISFACTORIAMENTE"

                Dim conexion2 As New SqlConnection(StrTpm)
                Dim str2 As String = "UPDATE Coches SET ODOMETRO2 = '" & CInt(TextBox4.Text) & "' WHERE Placas = '" & Label2.Text & "'"
                Dim command2 As SqlCommand = New SqlCommand(str2, conexion2)
                command2.Connection = conexion2
                conexion2.Open()
                command2.ExecuteNonQuery()
                conexion2.Dispose()
                command2.Dispose()

                MessageBox.Show("Datos guardados correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Recargar()
                Refresca_Bitacora()
                'Me.Close()

                'Dim frm As New CargaCombustible
                'frm.Show()

            End If
        Else


            'MessageBox.Show("hubo un error en los campos de datos", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End If 'Fin de if para verificar los campos'
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim sconec As String = StrTpm
        Dim objcommand As New SqlCommand
        objcommand.CommandText = "select precio from dbo.combustible where tipodecomb = '" & ComboBox1.Text & "'"
        objcommand.Connection = New SqlConnection(sconec)
        objcommand.Connection.Open()
        Dim OBJDR As SqlDataReader = objcommand.ExecuteReader()
        If OBJDR.HasRows Then
            While OBJDR.Read()
                TextBox2.Text = OBJDR.Item("precio").ToString
            End While
        Else
            MsgBox("no hay datos que mostrar")
        End If
        OBJDR.Close()
        objcommand.Dispose()
        operacion()
    End Sub



    Public Sub operacion()
        Dim a, b As Decimal
        If TextBox2.Text <> "" Then

            If Decimal.Parse(TextBox2.Text) <> 0 Then
                If TextBox5.Text <> "" Then
                    If Decimal.Parse(TextBox5.Text) <> 0 Then
                        a = Decimal.Parse(TextBox5.Text)
                        b = a / Decimal.Parse(TextBox2.Text)
                        'b = Format((a / Decimal.Parse(TextBox2.Text)), "#0.00")
                        TextBox1.Text = b.ToString(("#0.00"))
                    End If
                End If
            End If



        End If
    End Sub



    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        verifica_precio_litro()
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If band = False Then
            Dim od As Integer

            If TextBox4.Text <> "" Then
                Try
                    od = CInt(TextBox4.Text)

                    If od > 270000 Then
                        band = True
                        TextBox4.Text = Format(od, "#,###,##0")
                        TextBox4.SelectionStart = TextBox4.TextLength
                        MessageBox.Show("El odometro no deberia ser mayor a 250,000 Km", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Exit Sub
                    Else
                        TextBox4.Text = Format(od, "#,###,##0")
                        'verifica_odometro()
                        TextBox4.SelectionStart = TextBox4.TextLength
                    End If


                Catch ex As Exception
                    MessageBox.Show("Ingresa un odometro valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try
            End If
        Else
            band = False
        End If

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text <> "" Then
            'Dim od As Decimal
            Try
                'od = CDec(TextBox5.Text)
                'TextBox5.Text = Format(od, "#,###,##0.#0")
                'TextBox5.SelectionStart = TextBox4.TextLength
                operacion()
            Catch ex As Exception
                TextBox5.Focus()
                MessageBox.Show("Ingresa un Carga Valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

        End If
    End Sub

    Private Sub TextBox5_Leave(sender As Object, e As EventArgs) Handles TextBox5.Leave
        If TextBox5.Text <> "" Then
            Dim od As Decimal
            Try
                od = CDec(TextBox5.Text)
                TextBox5.Text = Format(od, "#,###,##0.#0")
                TextBox5.SelectionStart = TextBox4.TextLength
                operacion()
            Catch ex As Exception
                'TextBox5.Focus()
                'MessageBox.Show("Ingresa un Carga Valida2", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

        End If
    End Sub


    Public Sub verifica_precio_litro()
        If TextBox2.Text <> "" Then
            Try
                If Decimal.Parse(TextBox2.Text) > 40 Then
                    MessageBox.Show("Precio del litro no debe ser mayor a $40.00", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    Try
                        operacion()
                    Catch ex As Exception

                    End Try

                End If

            Catch ex As Exception
                MessageBox.Show("Ingresa un numero de litros valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

        End If

    End Sub

    Public Sub verifica_odometro()
        If TextBox4.Text <> "" Then
            Try
                If CDec(TextBox4.Text) > 270000 Then
                    MessageBox.Show("El odometro no deberia ser mayor a 250,000 Km", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Catch ex As Exception
                MessageBox.Show("Ingresa un odometro valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End If

    End Sub

    Public Sub Recargar()
        ComboBox1.SelectedIndex = 0
        dt1.Value = Date.Now
        Label1.Text = UsrTPM

        objcommand = New SqlCommand

        'objcommand.CommandText = "SELECT T1.Placas,T1.Marca,T1.Modelo,T1.Año,T1.Agente,odometro2, " &
        '                        "convert(DATE,T2.fecha) from usuarios t0 inner join Coches t1 on t0.CodAgte = t1.Agente " &
        '                        "INNER JOIN Carga2 T2 ON T1.Placas = T2.placas where t0.Id_Usuario = '" & UsrTPM & "' and t1.Placas = '" & placa & "' ORDER BY T2.Fecha ASC "

        objcommand.CommandText = "select t0.Placas, t0.Marca, t0.Modelo, t0.Año, t0.Agente, t0.odometro2, " &
                               "convert(DATE,(select MAX(fecha) from Carga2 t4 where t4.placas = t0.Placas)) from Coches t0 " &
                               "inner join Usuarios t1 on t0.Agente = t1.CodAgte where t1.Id_Usuario = '" & UsrTPM & "' and t0.Placas = '" & placa & "' " 
        objcommand.Connection = New SqlConnection(sconec)
        objcommand.Connection.Open()
        OBJDR = objcommand.ExecuteReader()
        If OBJDR.HasRows Then
            While OBJDR.Read()
                Label2.Text = OBJDR.Item(0).ToString()
                placa = OBJDR.Item(0).ToString()
                Label4.Text = OBJDR.Item(2).ToString
                Label6.Text = OBJDR.Item(3).ToString
                'Label17.Text = OBJDR.Item(5).ToString
                Label17.Text = (CInt(OBJDR.Item(5))).ToString(("###,##0"))
                If OBJDR.Item(6).ToString <> "" Then
                    Label12.Text = OBJDR.Item(6)
                Else
                    Label12.Text = ""

                End If

            End While

            odometro_anterior = CInt(Label17.Text)
        Else
            MsgBox("no hay datos que mostrar")
        End If
        OBJDR.Close()
        objcommand.Dispose()
        TextBox4.Text = "0"
        TextBox1.Text = "0"
        TextBox2.Text = "0"
        TextBox5.Text = "0"
        TextBox3.Text = ""
        TextBox4.Focus()

    End Sub

    Public Sub Refresca_Bitacora()
        For Each f As Form In Application.OpenForms
            If f.Name.ToString = "Bitacora" Then
                'MsgBox("si esta avierto")
                Dim form_bitacora As Bitacora
                form_bitacora = f
                form_bitacora.re_load()
                'MsgBox("si esta avierto")
                Exit For
            End If
        Next

    End Sub
    Public Function Verifica_Campos() As Boolean
        '-------VALIDA EL CAMPO DE TEXTO ODOMETRO---------'
        If TextBox4.Text <> "" Then
            Try
                odometro_nuevo = CInt(TextBox4.Text)
                If odometro_nuevo <> 0 Then
                    If odometro_nuevo < 270000 And odometro_nuevo > odometro_anterior Then
                        Verifica_Campos = True
                    Else
                        If odometro_nuevo > 270000 Then
                            MessageBox.Show("Verifica que el odometro nuevo no sea mayor a 270,000 Km", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Verifica_Campos = False
                            TextBox4.Focus()
                            Exit Function

                        End If
                        If odometro_nuevo < odometro_anterior Then
                            MessageBox.Show("Verifica que el odometro nuevo sea mayor a " & (CInt(Label17.Text)).ToString(("###,##0")) & " Km", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Verifica_Campos = False
                            TextBox4.Focus()
                            Exit Function

                        End If
                    End If
                Else
                    MessageBox.Show("Verifica que el campo de odometro no sea 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Verifica_Campos = False
                    TextBox4.Focus()
                    Exit Function
                End If

            Catch ex As Exception
                MessageBox.Show("Ingresa un odometro valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Verifica_Campos = False
                TextBox4.Focus()
                Exit Function
            End Try

        Else
            MessageBox.Show("Verifica que el campo de odometro no este vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Verifica_Campos = False
            TextBox4.Focus()
            Exit Function
        End If

        '--------VALIDA QUE LA FECHA NO SEA MAYOR A LA DE DIA ACTUAL '
        If dt1.Value > Date.Now Then
            If (MessageBox.Show( _
                            "Verifica que la fecha sea igual o menor a la de hoy", _
                             "Fecha Incorrecta", MessageBoxButtons.OK, _
                            MessageBoxIcon.Exclamation)) = MsgBoxResult.Ok Then
                dt1.Focus()
                Verifica_Campos = False
                Exit Function
            End If
        Else
            Verifica_Campos = True
        End If

        '-----------------------------------------------------------'

        '--------VALIDA QUE LA FECHA NO SEA MENOR A LA DE LA ULTIMA CARGA'
        Dim fecha_anterior As Date
        'fecha_anterior = "01/01/1999"
        If Label12.Text.ToString <> "" Then
            fecha_anterior = Label12.Text.ToString
        Else
            fecha_anterior = "1999-01-01"
        End If

        'MsgBox(fecha_anterior)
        If dt1.Value < fecha_anterior Then
            If (MessageBox.Show( _
                            "Verifica que la fecha sea mayor a la fecha de tu ultima carga que fue el " & Label12.Text.ToString, _
                             "Fecha Incorrecta", MessageBoxButtons.OK, _
                            MessageBoxIcon.Exclamation)) = MsgBoxResult.Ok Then
                dt1.Focus()
                Verifica_Campos = False
                Exit Function
            End If
        Else
            Verifica_Campos = True

        End If
        '-------------------------------------------------------


        '--------------------------------------------'
        '--------VALIDA EL CAMPO DE TEXTO PRECIO POR LITRO--------'
        If TextBox2.Text <> "" Then
            Try
                If Decimal.Parse(TextBox2.Text) < 40 And Decimal.Parse(TextBox2.Text) <> 0 And Decimal.Parse(TextBox2.Text) > 9 Then
                    Verifica_Campos = True
                Else
                    If Decimal.Parse(TextBox2.Text) >= 40 Then
                        MessageBox.Show("Verifica que el precio por litro sea menor a $40.00", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Verifica_Campos = False
                        TextBox2.Focus()
                        Exit Function
                    End If

                    If Decimal.Parse(TextBox2.Text) = 0 Then
                        MessageBox.Show("Verifica que el precio por litro no sea 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Verifica_Campos = False
                        TextBox2.Focus()
                        Exit Function
                    End If

                    If Decimal.Parse(TextBox2.Text) <= 9 Then
                        MessageBox.Show("Verifica que el precio por litro sea mayor $10.00", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Verifica_Campos = False
                        TextBox2.Focus()
                        Exit Function
                    End If

                End If
            Catch ex As Exception
                MessageBox.Show("Ingresa un numero de litros valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Verifica_Campos = False
                TextBox2.Focus()
                Exit Function
            End Try

        Else
            MessageBox.Show("Verifica que el campo 'Precio por litro' no este vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Verifica_Campos = False
            TextBox2.Focus()
            Exit Function
        End If
        '------------------------------------------'
        '---------VALIDA EL CAMPO DE TEXTO COSTO DE CARGA---------'
        If TextBox5.Text <> "" Then
            Try
                If Decimal.Parse(TextBox5.Text) <> 0 Then
                    Verifica_Campos = True
                Else
                    MessageBox.Show("Verifica que el costo de la carga no sea 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Verifica_Campos = False
                    TextBox5.Focus()
                    Exit Function
                End If
            Catch ex As Exception
                MessageBox.Show("Ingresa un Carga Valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Verifica_Campos = False
                TextBox5.Focus()
                Exit Function
            End Try


        Else
            MessageBox.Show("Verifica que el campo 'Costo Carga' no este vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Verifica_Campos = False
            TextBox5.Focus()
            Exit Function
        End If
        '--------------------------------------------------------'
    End Function


End Class