Public Class frmEmergente

    Public DocEntry As String
    Dim SQL As New Comandos_SQL()

    Private Sub frmEmergente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQL.conectarTPM()
        dgvFletes.DataSource = SQL.ConsultarTabla("select distinct td.ItemCode,td.Dscription,td.Quantity from SBO_TPD.dbo.OINV tf left join SBO_TPD.dbo.INV1 td on td.DocEntry = tf.DocEntry left join SBO_TPD.dbo.OITM ta on ta.ItemCode = td.ItemCode left join SBO_TPD.dbo.OITM tg on tg.ItmsGrpCod = ta.ItmsGrpCod where tf.DocNum = " + DocEntry + " and tg.ItmsGrpCod = 150")
        EstilodgvFletes()
    End Sub

    Sub EstilodgvFletes()
        'ESTILOS POR COLUMNA
        With Me.dgvFletes
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            Dim clr1 As Color
            clr1 = ColorTranslator.FromHtml("#deeaf6")
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.White
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = clr1
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'ORDEN DE VENTA
            .Columns("ItemCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("ItemCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            .Columns("ItemCode").DefaultCellStyle.ForeColor = Color.Red
            .Columns("ItemCode").HeaderText = "Articulo"
            '.Columns("ItemCode").Width = 70
            .Columns("ItemCode").ReadOnly = True
            'FECHA DE ORDEN
            .Columns("Dscription").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Dscription").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("Dscription").HeaderText = "Descripción"
            '.Columns("Dscription").Width = 85
            .Columns("Dscription").ReadOnly = True
            'Cantidad
            .Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Quantity").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("Quantity").HeaderText = "Cantidad"
            '.Columns("Quantity").Width = 60
            .Columns("Quantity").ReadOnly = True
        End With
    End Sub
End Class