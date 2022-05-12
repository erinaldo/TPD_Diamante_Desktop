Imports System.Data.SqlClient

Public Class PrecioGaso
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim conexion As New SqlConnection(conexion_universal.CadenaSQL)
        Dim str As String = "update dbo.combustible set Precio = '" & TextBox1.Text & "' where tipodecomb = '" & ComboBox1.Text & "'"
        Dim command1 As SqlCommand = New SqlCommand(str, conexion)
        command1.Connection = conexion
        Try
            conexion.Open()
            command1.ExecuteNonQuery()
            MsgBox("Datos actualizados")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conexion.Dispose()
            command1.Dispose()
            precios()
        End Try


    End Sub

    Private Sub PrecioGaso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        precios()

    End Sub
    Public Sub precios()
        'Label1.Text = UsrTPM
        Dim sconec As String = StrTpm
        Dim objcommand As New SqlCommand
        objcommand.CommandText = "SELECT * FROM [TPM].[dbo].[Combustible]"
        objcommand.Connection = New SqlConnection(sconec)
        objcommand.Connection.Open()
        Dim a As Integer = 0
        Dim OBJDR As SqlDataReader = objcommand.ExecuteReader()
        If OBJDR.HasRows Then
            While OBJDR.Read()
                If a = 0 Then
                    Label3.Text = OBJDR.Item(2).ToString()
                End If
                If a = 1 Then
                    Label5.Text = OBJDR.Item(2).ToString()
                End If
                If a = 2 Then
                    Label7.Text = OBJDR.Item(2).ToString()
                End If
                'Label3.Text = OBJDR.Item(1).ToString()
                'Label5.Text = OBJDR.Item(2).ToString()
                'Label7.Text = OBJDR.Item(3).ToString()
                a = a + 1
            End While
        Else
            MsgBox("no hay datos que mostrar")
        End If
        OBJDR.Close()
        objcommand.Dispose()
    End Sub

End Class