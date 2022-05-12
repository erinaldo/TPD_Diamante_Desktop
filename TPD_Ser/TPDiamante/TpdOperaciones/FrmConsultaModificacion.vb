Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.IO
Public Class FrmConsultaModificacion
    Dim fi As String
    Dim FF As String
    Dim ResultadoConsulta As DataView
    Dim ResultadoConsultaD As DataView
  'Colores creados para uso en el grid DGVoperacionVtaEnter
  Dim rojo As Color
    Dim amarillo As Color
    Dim verde As Color
    Dim Anaranjado As Color
    Dim GRIS As Color
    Dim Morado As Color
    Dim Azul As Color

    Dim SQL As New Comandos_SQL()
    Dim DocNum As String
    Dim DocNumP As String

    Dim bandera_edit As Boolean = False

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        MBuscarOrdenes()
    End Sub


    Private Sub FrmConsultaModificacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LlenarConsulta()
        If dgvConsulta.Rows.Count <> 0 Then
            Dim row As DataGridViewRow = dgvConsulta.CurrentRow()
            LLenarConsultaDetalle(CStr(row.Cells("DocNum").Value).ToString)
            'SE ASIGNA EL TIEMPO DE RECARGA DEL GRID EN MILISEGUNDOS
            'Timer1.Interval = 90000
            ''Activamos el evento timer para que el grid se actualize solo 
            'Timer1.Enabled = True
        End If

        Modificar = "False"
    End Sub


    Sub MBuscarOrdenes()
        Try
            'FILTRA EL RESUTADO DEL DATA VIEW
            ResultadoConsulta.RowFilter = "DocNum like '%" & CStr(txtBuscar.Text) & "%' OR CardName like '%" & CStr(txtBuscar.Text) & "%' OR  CardCode like '%" & CStr(txtBuscar.Text) & "%'"
        Catch exc As Exception
            MsgBox("Error en busqueda de la orden: " + exc.ToString)
            Return
        End Try
    End Sub

    Sub LlenarConsulta()
        Try
            dgvConsulta.DataSource = SQL.EjecutarProcedimiento("TPD_ConMod_Surtir", "@FechaInicial,@FechaFin", 2, dtpFecha.Value.ToString("yyyy-MM-dd") + "," + DTPFechaFin.Value.ToString("yyyy-MM-dd"))
            EstiloDgvConsulta()
        Catch ex As Exception
            MessageBox.Show("¡Error al LlenarConsulta: " + Environment.NewLine + ex.ToString() + "!", "¡Error en TPD!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub LLenarConsultaDetalle(ORDEN As String)
        Try
            If DocNum <> DocNumP Then
                dgvConsultaDetalle.DataSource = SQL.EjecutarProcedimiento("TPD_ConMod_Surtir_detalle", "@Orden", 1, ORDEN)
                EstiloDetalledConsulta()
                DocNumP = DocNum
            End If
        Catch ex As Exception
            'MessageBox.Show("¡Error al LLenarConsultaDetalle: " + Environment.NewLine + ex.ToString() + "!", "¡Error en TPD!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Sub EstiloDgvConsulta()
        'cambia el estilo de las columnas del gridview detalle operacion  
        'Cambiar el estido del campo Orden de Venta a Negritas
        Dim style As New DataGridViewCellStyle
        style.Font = New Font(dgvConsulta.Font, FontStyle.Bold)
        With Me.dgvConsulta
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Orden de ventta
            .Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("DocNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            .Columns("DocNum").DefaultCellStyle = style
            .Columns("DocNum").HeaderText = "Orden Venta"
            .Columns("DocNum").Width = 45
            .Columns("DocNum").ReadOnly = True
            'Fecha de documento 
            .Columns("DocDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DocDate").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("DocDate").HeaderText = "Fecha Documento"
            .Columns("DocDate").Width = 70
            .Columns("DocDate").ReadOnly = True
            'Hora de documento 
            .Columns("PrintTime").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PrintTime").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("PrintTime").HeaderText = "Hora Creacion"
            .Columns("PrintTime").Width = 50
            .Columns("PrintTime").ReadOnly = True

            'nombre del cliente
            .Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("CardName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("CardName").HeaderText = "Cliente"
            .Columns("CardName").Width = 200
            .Columns("CardName").ReadOnly = True
            'codigo del cliente
            .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("CardCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("CardCode").HeaderText = "Código"
            .Columns("CardCode").DefaultCellStyle.Format = "N0"
            .Columns("CardCode").Width = 50
            .Columns("CardCode").ReadOnly = True
            .Columns("CardCode").Frozen = True
            'estatus 
            .Columns("Estatus").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Estatus").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Estatus").HeaderText = "Estatus"
            .Columns("Estatus").Width = 65
            .Columns("Estatus").ReadOnly = True
            'estado surtiemiento
            .Columns("EstadoSurtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("EstadoSurtido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("EstadoSurtido").HeaderText = "Estado Surtido"
            .Columns("EstadoSurtido").Width = 55
            .Columns("EstadoSurtido").ReadOnly = True
            'Numero de cajas 
            .Columns("Cajas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Cajas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Cajas").HeaderText = "Cajas"
            .Columns("Cajas").DefaultCellStyle.Format = "N0"
            .Columns("Cajas").Width = 35
            .Columns("Cajas").ReadOnly = True
            'Comentario de ventas para almacen 
            .Columns("SolicitadoPor").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("SolicitadoPor").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("SolicitadoPor").HeaderText = "Solicitado Por"
            .Columns("SolicitadoPor").Width = 110
            .Columns("SolicitadoPor").ReadOnly = True
            'Observacion
            .Columns("Surtidor").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Surtidor").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Surtidor").HeaderText = "Surtidor"
            .Columns("Surtidor").Width = 80
            .Columns("Surtidor").ReadOnly = True
            'Peso
            .Columns("Peso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Peso").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Peso").HeaderText = "Peso Total"
            .Columns("Peso").DefaultCellStyle.Format = "N2"
            .Columns("Peso").Width = 50
            .Columns("Peso").ReadOnly = True

            .Columns("Peso Real").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Peso Real").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Peso Real").HeaderText = "Peso Real"
            .Columns("Peso Real").DefaultCellStyle.Format = "N2"
            .Columns("Peso Real").Width = 50
            .Columns("Peso Real").ReadOnly = True
            'Peso
            .Columns("Paqueteria").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Paqueteria").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Paqueteria").HeaderText = "Paqueteria"
            .Columns("Paqueteria").Width = 85
            .Columns("Paqueteria").ReadOnly = True
            'Peso
            .Columns("Facturado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Facturado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Facturado").HeaderText = "Facturado"
            .Columns("Facturado").Width = 85
            .Columns("Facturado").ReadOnly = True
        End With
    End Sub


    Private Sub dgvConsulta_SelectionChanged(sender As Object, e As EventArgs) Handles dgvConsulta.SelectionChanged
        'En este evento actualizaremos el grid detalle cuando el usuario se mueva entre celdas 
        'Valida el ordenamiento del grid para el cambio de posicion 
        If dgvConsulta.CurrentRow Is Nothing Then
        Else
            Try
                Dim row As DataGridViewRow = dgvConsulta.CurrentRow()
                'Llena el datagrid detalle 
                LLenarConsultaDetalle(CStr(row.Cells("DocNum").Value).ToString)
                DocNum = CStr(row.Cells("DocNum").Value).ToString
            Catch ex As Exception
                MsgBox("Error al obtener la Orden en llenado de Grid Detalle." + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de Dato")
            End Try
        End If
    End Sub



    Sub EstiloDetalledConsulta()
        'Este metodo cambia el estilo del gridview detalle 
        With Me.dgvConsultaDetalle
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Partidas
            .Columns("Partidas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("Partidas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Partidas").HeaderText = "Partidas"
            .Columns("Partidas").Width = 45
            .Columns("Partidas").ReadOnly = True
            'Bloquea el ordenamiento del gridview
            .Columns("Partidas").SortMode = DataGridViewColumnSortMode.NotSortable
            'Articulo
            .Columns("Articulo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Articulo").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Articulo").HeaderText = "Artículo"
            .Columns("Articulo").Width = 75
            .Columns("Articulo").ReadOnly = True
            'Bloquea el ordenamiento del gridview
            .Columns("Articulo").SortMode = DataGridViewColumnSortMode.NotSortable
            'Descripcion
            .Columns("Descripcion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("Descripcion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Descripcion").HeaderText = "Descripción"
            .Columns("Descripcion").Width = 240
            .Columns("Descripcion").ReadOnly = True
            'Bloquea el ordenamiento del gridview
            .Columns("Descripcion").SortMode = DataGridViewColumnSortMode.NotSortable
            'Cantidad
            .Columns("Cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Cantidad").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Cantidad").HeaderText = "Cantidad"
            .Columns("Cantidad").DefaultCellStyle.Format = "N0"
            .Columns("Cantidad").Width = 50
            .Columns("Cantidad").ReadOnly = True
            'Bloquea el ordenamiento del gridview
            .Columns("Cantidad").SortMode = DataGridViewColumnSortMode.NotSortable
            'Surtido
            .Columns("Surtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Surtido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Surtido").HeaderText = "Surtido"
            .Columns("Surtido").Width = 50
            .Columns("Surtido").ReadOnly = True
            .Columns("Surtido").SortMode = DataGridViewColumnSortMode.NotSortable
            'Peso 
            .Columns("Peso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Peso").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Peso").HeaderText = "Peso"
            .Columns("Peso").Width = 50
            .Columns("Peso").ReadOnly = True
            .Columns("Peso").SortMode = DataGridViewColumnSortMode.NotSortable
        End With
    End Sub
    Private Sub dgvConsulta_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvConsulta.CellFormatting
        'Se definen los colores con paleta de coroles en RGB
        rojo = ColorTranslator.FromHtml("#FFC6C6")
        amarillo = ColorTranslator.FromHtml("#FDFF6C")
        verde = ColorTranslator.FromHtml("#A6FFA0")
        Anaranjado = ColorTranslator.FromHtml("#FFCC80")
        GRIS = ColorTranslator.FromHtml("#DDDDDD")
        Azul = ColorTranslator.FromHtml("#BAC9FC")
        Morado = ColorTranslator.FromHtml("#E8CCE8")
        'Dependiendo del estado de la columna Estatus la fila se pintara de un cierto color y en algunos casos cambiara las letras a negritas
        For i As Integer = 0 To Me.dgvConsulta.Rows.Count - 1
            If Me.dgvConsulta.Rows(i).Cells("Estatus").Value = "En Cola" Then
                Me.dgvConsulta.Rows(i).DefaultCellStyle.BackColor = Color.Empty

            ElseIf Me.dgvConsulta.Rows(i).Cells("Estatus").Value = "Surtiendo" Then
                Me.dgvConsulta.Rows(i).DefaultCellStyle.BackColor = Anaranjado

            ElseIf Me.dgvConsulta.Rows(i).Cells("Estatus").Value = "Surtido" And Me.dgvConsulta.Rows(i).Cells("Facturado").Value = "No Facturado" Then
                Me.dgvConsulta.Rows(i).DefaultCellStyle.BackColor = verde

            ElseIf Me.dgvConsulta.Rows(i).Cells("Facturado").Value = "Cancelado" Then
                Me.dgvConsulta.Rows(i).DefaultCellStyle.BackColor = rojo

            ElseIf Me.dgvConsulta.Rows(i).Cells("Estatus").Value = "En Espera" Then
                Me.dgvConsulta.Rows(i).DefaultCellStyle.BackColor = amarillo


            ElseIf Me.dgvConsulta.Rows(i).Cells("Estatus").Value = "Empacando" Then
                Me.dgvConsulta.Rows(i).DefaultCellStyle.BackColor = Azul

            ElseIf Me.dgvConsulta.Rows(i).Cells("Estatus").Value = "Empacado" Then
                Me.dgvConsulta.Rows(i).DefaultCellStyle.BackColor = Morado

            ElseIf Me.dgvConsulta.Rows(i).Cells("Facturado").Value = "Facturado" Then
                Me.dgvConsulta.Rows(i).DefaultCellStyle.BackColor = GRIS


            End If
        Next
        'Pondremos las letras en negritas de la Columna EstadoSurtido 
        If Me.dgvConsulta.Columns(e.ColumnIndex).Name = "EstadoSurtido" Then
            Dim estado As String = CStr(e.Value)
            Select Case estado
                Case "SI"
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
                Case "NO"
                    e.CellStyle.ForeColor = Color.Red
                    e.CellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
                Case "EN ESPERA"
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            End Select
        End If
    End Sub
    Private Sub BtnActualizar_Click(sender As Object, e As EventArgs) Handles BtnActualizar.Click
        LLenarConsulta()
        'Obtiene el ARticulo seleccionado
        Dim row As DataGridViewRow = dgvConsulta.CurrentRow()
        'Llena el datagrid con el detalle 
        If dgvConsulta.RowCount > 0 Then
            LLenarConsultaDetalle(CStr(row.Cells("DocNum").Value).ToString)
        Else
            'BORRAMOS LO QUE ESTA EN EL GRID Y LO ACTUALIZAMOS
            dgvConsultaDetalle.Refresh()
        End If
    End Sub



    Private Sub dgvConsulta_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvConsulta.KeyDown
        Modificar = "False"
        If (e.KeyCode = Keys.Enter) Then
            StatusSurtido = dgvConsulta.CurrentRow.Cells("Estatus").Value.ToString
            'ALMACENA EL DOCUMENTO DE LA ORDEN PARA PASARLO AL NUEVO FORMULARIO
            DocNumSurtido = dgvConsulta.CurrentRow.Cells("DocNum").Value.ToString
            'ALMACENA EL TITULO DEL FORMULARIO DE DETALLE DE SURTIDO
            TituloSurtido = StatusSurtido + " Orden de Venta | " + DocNumSurtido
            If dgvConsulta.CurrentRow.Cells("Estatus").Value.ToString = "Surtido" And dgvConsulta.CurrentRow.Cells("Facturado").Value.ToString = "No Facturado" Then
                Modificar = "True"
                'MANDA A LLAMAR EL FORMULARIO DE SURTIDO
                frmConsultaModDetalle.MdiParent = Inicio
                frmConsultaModDetalle.Show()
                'INICIALIZA LAS VARIABLE DE TITULO DE DETALLA SURTIDO
                TituloAcceso = ""
                StatusAcceso = ""
                DocNumAcceso = ""
            Else
                Modificar = "True"
                'MANDA A LLAMAR EL FORMULARIO DE SURTIDO
                frmConsultaModDetalle.MdiParent = Inicio
                frmConsultaModDetalle.Show()
                'INICIALIZA LAS VARIABLE DE TITULO DE DETALLA SURTIDO
                TituloAcceso = ""
                StatusAcceso = ""
                DocNumAcceso = ""
            End If
        End If
    End Sub

    Private Sub dgvConsultaDetalle_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvConsultaDetalle.CellContentClick

    End Sub

    Private Sub dgvConsultaDetalle_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvConsultaDetalle.CellFormatting
        'Este metodo da color amarillo al grid view detalle siempre  y cuando exista una discrepancia entre el surtido real y lo pedido por el cliente
        'Define los colores que se mostraran en el detalle de la Orden
        amarillo = ColorTranslator.FromHtml("#FDFF6C")
        'Un ciclo para recorrer todos los datos del grid view y asi saber cuales pintaremos 
        For i As Integer = 0 To Me.dgvConsultaDetalle.Rows.Count - 1

            'Si el surtido real =Surtido  regresa valores nulos entonces no tendra color la fila 
            If dgvConsultaDetalle.Rows(i).Cells("Surtido").Value Is DBNull.Value Then
                Me.dgvConsultaDetalle.Rows(i).DefaultCellStyle.SelectionBackColor = Color.Empty
            Else
                'Si el surtido real es menos ala cantidad solicitada por el cliente  entonces se pintara de amarillo 
                If Convert.ToInt32(Me.dgvConsultaDetalle.Rows(i).Cells("Surtido").Value) < Convert.ToInt32(Me.dgvConsultaDetalle.Rows(i).Cells("Cantidad").Value) Then
                    Me.dgvConsultaDetalle.Rows(i).DefaultCellStyle.BackColor = amarillo
                    'Si el surtido real es mayor o igual ala cantidad solicitada por el cliente entonces no pintaremos la fila 
                ElseIf Convert.ToInt32(Me.dgvConsultaDetalle.Rows(i).Cells("Surtido").Value) >= Convert.ToInt32(Me.dgvConsultaDetalle.Rows(i).Cells("Cantidad").Value) Then
                    Me.dgvConsultaDetalle.Rows(i).DefaultCellStyle.BackColor = Color.Empty
                End If
            End If
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LLenarConsulta()
        Dim row As DataGridViewRow = dgvConsulta.CurrentRow()
        LLenarConsultaDetalle(CStr(row.Cells("DocNum").Value).ToString)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub dgvConsulta_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvConsulta.CellClick
        Dim row As DataGridViewRow = dgvConsulta.CurrentRow()
        LLenarConsultaDetalle(CStr(row.Cells("DocNum").Value).ToString)
        DocNum = CStr(row.Cells("DocNum").Value).ToString
    End Sub
End Class