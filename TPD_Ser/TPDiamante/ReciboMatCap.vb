Imports System.Data.SqlClient
Public Class ReciboMatCap
    Private Sub BtnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGuardar.Click
        Dim MaxRecibo As Integer = 0
        Dim TotRecibo As Integer = Val(TxtIdRecibo.Text)
        Dim vComent As String
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim CadenaSQL As String = ""

        vComent = QuitarCaracteres(TxtComentario.Text)

        If TotRecibo > 1 Then
            CadenaSQL = "SELECT MAX(T0.Recibo) AS NRecibo FROM RECMAT T0 WHERE T0.DocNum = " + TxtOrdCompra.Text

            con.ConnectionString = StrTpm
            con.Open()
            cmd.Connection = con
            cmd.CommandText = CadenaSQL

            With cmd

                MaxRecibo = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())

                .Connection.Close()
            End With

        End If

        MaxRecibo += 1

        CadenaSQL = "INSERT INTO RECMAT (DocNum,Recibo,FchRec,Comentario,UsrCom) VALUES ("
        CadenaSQL &= TxtOrdCompra.Text
        CadenaSQL &= ","
        CadenaSQL &= MaxRecibo.ToString
        CadenaSQL &= ","
        CadenaSQL &= "GETDATE()"
        CadenaSQL &= ",'"
        CadenaSQL &= vComent
        CadenaSQL &= "','"
        CadenaSQL &= UsrTPM
        CadenaSQL &= "')"

        Try
            con.ConnectionString = StrTpm
            con.Open()
            cmd.Connection = con

            cmd.CommandText = CadenaSQL
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Error Agregando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            con.Close()
        End Try
        If UsrTPM = "ARAMOS" Or UsrTPM = "VOTNIEL" Or UsrTPM = "MCHABLE" Then
            ReciboMatSeg.ActualizaRecibos(vComent, MaxRecibo, TotRecibo)
        Else
            ReciboMat.ActualizaRecibos(vComent, MaxRecibo, TotRecibo)
        End If

        Me.Close()
    End Sub
End Class