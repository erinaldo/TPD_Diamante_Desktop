

Partial Public Class DsOrdVta
    Partial Public Class DtOrdVtaDataTable
        Private Sub DtOrdVtaDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.NumLetraColumn.ColumnName) Then
                'Agregar código de usuario aquí
            End If

        End Sub

    End Class
End Class
