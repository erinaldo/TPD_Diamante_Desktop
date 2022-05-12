Public Class ValeSalidaComen


    Private Sub ValeSalidaComen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TBArticulo.Text = VSArticulo
        TBDescripcion.Text = VSDescripcion
        TBLinea.Text = VSLinea

        TBMotivo.Text = VSMotivo
        TBComen.Text = VSComentarios
        TBEntrega.Text = VSEntrega
    End Sub

    Private Sub ValeSalidaComen_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            VSMotivo = TBMotivo.Text
            VSComentarios = TBComen.Text
            VSEntrega = TBEntrega.Text


            'MsgBox(PosRen)

            'With ValeSalida
            ValeSalida.DGVCap.Item(5, PosRen).Value = VSMotivo
            ValeSalida.DGVCap.Item(6, PosRen).Value = VSComentarios
            ValeSalida.DGVCap.Item(7, PosRen).Value = VSEntrega
            'End With


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class