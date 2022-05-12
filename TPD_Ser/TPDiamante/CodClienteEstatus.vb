
Imports System.Data.SqlClient

Public Class CodClienteEstatus

  Public conexion As New SqlConnection(conexion_universal.CadenaSQLSAP)

  Private Sub CodClienteEstatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
                Dim cadena As String
                cadena = " SELECT CardName + ' ('+ CardCode+ ')' Nombre,CardCode Clave from OCRD clt " &
                         " WHERE clt.CardType = 'C' AND CardName IS NOT NULL " +
                         " ORDER BY clt.CardName "

                Dim da As New SqlDataAdapter(cadena, SqlConnection)
                Dim ds As New DataSet
                da.Fill(ds)
                ''ds.Tables(0).Rows.Add("TODOS", "0")
                Me.CBCliente.DataSource = ds.Tables(0)
                Me.CBCliente.DisplayMember = "Nombre"
                Me.CBCliente.ValueMember = "Clave"
                Me.CBCliente.SelectedValue = "0"
            End Using
        Catch ex As Exception

        End Try
    End Sub



    Private Sub CBCliente_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBCliente.SelectionChangeCommitted

        Try
            Dim cmd As SqlCommand = New SqlCommand("SELECT CardCode FROM OCRD WHERE CardCode = @Clave ", conexion)

            cmd.Parameters.AddWithValue("@Clave", CStr(CBCliente.SelectedValue))

            Dim da2 As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt2 As New DataTable
            da2.Fill(dt2)

            If dt2.Rows.Count > 0 Then

                Dim row As DataRow = dt2.Rows(0)
                'Dim row1 As DataRow = dt2.Rows(1)

                TBCliente.Text = CStr(row("CardCode"))

            Else
                TBCliente.Text = ""
            End If

            'Else
            'MsgBox("No hay Objetivo de este periodo")
            'End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CBCliente.Text = "" Then
            Module1.sCliente = TBCliente.Text
        Else
            Module1.sCliente = CBCliente.SelectedValue
        End If

        Dim frm As New EstatusCliente()
        frm.Show()
    End Sub

End Class