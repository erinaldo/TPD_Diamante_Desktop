Imports System.Data.SqlClient
Public Class Select_Vehicle
    Dim conexion As SqlConnection

    Dim DSAll As New DataSet

    Dim DVAlmacenista As New DataView
    Dim DVEmpacador As New DataView
    Dim DVHistorico As New DataView

    Dim Resultado As New DataView

    Dim ConsutaLista As String = ""
    Dim strTemp As String = ""
   
    Private Sub Select_Vehicle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conexion = New SqlConnection(StrTpm)
            ConsutaLista = "select Placas, Marca + ' - ' + Modelo + ' - ' + cast(Año as varchar) + ' - ' + Placas as 'Nombre' from Coches t0 inner join Usuarios t1 on t0.Agente = t1.CodAgte where t1.Id_Usuario = '" & UsrTPM & "'"
            Dim daGArticulo As New SqlDataAdapter(ConsutaLista, conexion)
            daGArticulo.Fill(DSAll)

            'Dim aux_row As DataRow
            'aux_row = DSAll.Tables(0).NewRow
            'aux_row("id") = 9999
            'aux_row("Nombre") = "--Ningun Resultado--"
            'DSAll.Tables(0).Rows.Add(aux_row)

            DVAlmacenista.Table = DSAll.Tables(0)
            'DVAlmacenista.RowFilter = "id <> 9999"
            'DVEmpacador.Table = DSAll.Tables(0)
            'DVEmpacador.RowFilter = "id <> 9999"

            
            CmbAlmacenista.DataSource = DVAlmacenista
            CmbAlmacenista.DisplayMember = "Nombre"
            CmbAlmacenista.ValueMember = "Placas"
            CmbAlmacenista.SelectedIndex = -1

           

            


        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If CmbAlmacenista.SelectedIndex = -1 Then
            MsgBox("Selecciona un vehiculo")
            Me.CmbAlmacenista.Focus()
        Else
            strTemp = CmbAlmacenista.SelectedValue.ToString
            band_carga_combustible = True
            For Each f As Form In Application.OpenForms
                If f.Name.ToString = "CargaCombustible" Then
                    'MsgBox("si esta avierto")
                    CargaCombustible.placa = strTemp
                    CargaCombustible.Recargar()
                    Exit For
                End If
            Next
            Me.Close()
        End If
    End Sub

    Private Sub Select_Vehicle_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'For Each f As Form In Application.OpenForms
        '    If f.Name.ToString = "CargaCombustible" Then
        '        MsgBox("si esta avierto")

        '        'MostrarOrdenes.Almacenista = CmbAlmacenista.SelectedValue.ToString
        '        'MostrarOrdenes.Empacador = CmbEmpacador.SelectedValue.ToString
        '        'MostrarOrdenes.Nombre_Almacenista = CmbAlmacenista.Text.ToString
        '        'MostrarOrdenes.Nombre_Empacador = CmbEmpacador.Text.ToString
        '        'MostrarOrdenes.pinta_labels()
        '        Exit For
        '    End If
        'Next
    End Sub

    Private Sub Select_Vehicle_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'If strTemp = "" Then
        '    band_carga_combustible = True
        'End If
        'For Each f As Form In Application.OpenForms
        '    If f.Name.ToString = "CargaCombustible" Then
        '        MsgBox("si esta avierto")
        '        f.Close()
        '        f.Close()
        '        f.Close()
        '        CargaCombustible.Close()

        '        'MostrarOrdenes.Almacenista = CmbAlmacenista.SelectedValue.ToString
        '        'MostrarOrdenes.Empacador = CmbEmpacador.SelectedValue.ToString
        '        'MostrarOrdenes.Nombre_Almacenista = CmbAlmacenista.Text.ToString
        '        'MostrarOrdenes.Nombre_Empacador = CmbEmpacador.Text.ToString
        '        'MostrarOrdenes.pinta_labels()
        '        Exit For
        '    End If
        'Next
    End Sub
End Class