Public Class CalendTest

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) _
        Handles Me.Load
        Me.DataGridView1.Dock = DockStyle.Fill
        Me.Controls.Add(Me.DataGridView1)
        Me.Text = "DataGridView calendar column demo"

        Dim col As New CalendarColumn()
        Me.DataGridView1.Columns.Add(col)
        Me.DataGridView1.RowCount = 5
        Dim row As DataGridViewRow
        For Each row In Me.DataGridView1.Rows
            row.Cells(0).Value = DateTime.Now
        Next row

    End Sub


End Class