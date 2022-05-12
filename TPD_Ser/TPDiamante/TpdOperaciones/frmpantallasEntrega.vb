Imports System.Data.SqlClient
Imports System.IO
Public Class frmpantallasEntrega
    Dim fi As String 'VARIABLE PARA OBTENER LA FECHA DEL DATAPICKER
    Dim DsOrdenes As DataSet 'DECLARA DATASET PARA PROCEDIMIENTO DE INSERTAR ORDENES
    Dim Resultado As DataView = New DataView 'DECLARA DATAVIEW PARA MOSTRAR EL RESULTADO EN LOS GRID
    Dim ResultadoEmpaque As DataView = New DataView 'DECLARA DATAVIEW PARA MOSTRAR EL RESULTADO EN LOS GRID
    'VARIABLES PARA BASE DE DATOS
    Dim BaseDatosTPD As String = "TPM"
    Dim BaseDatosSAP As String = "SBO_TPD"

    'Colores creados para uso en el grid DGVoperacionVta
    Dim rojo As Color
    Dim amarillo As Color
    Dim verde As Color
    Dim Anaranjado As Color

    Private Sub frmpantallasEntrega_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MLlenaOrdenesSurtir()
        LlenarEmpaque()
        Liberacion_Material()

        Timer1.Interval = 90000
        Timer1.Enabled = True
    End Sub


    Sub MLlenaOrdenesSurtir()
        Try
            dgvSurtir.DataSource = SQL.EjecutarProcedimiento("TPD_Surtir_Almacen", "@Fecha", 1, dtpFecha.Value.ToString("yyyy-MM-dd"))

            MEstiloGridOrdenes()

        Catch ex As Exception
            MsgBox("Error en el llenado de las Ordenes: " + ex.ToString, MsgBoxStyle.Exclamation, "Error en Conexión o llenado del Grid")
        Return
        End Try

    End Sub


    Sub MEstiloGridOrdenes()
        'ESTILOS POR COLUMNA
        With Me.dgvSurtir
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            Dim clr1 As Color
            clr1 = ColorTranslator.FromHtml("#deeaf6")
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.White
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = clr1
            .DefaultCellStyle.ForeColor = Color.Black
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'ORDEN DE VENTA
            .Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DocNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("DocNum").DefaultCellStyle.ForeColor = Color.Red
            .Columns("DocNum").HeaderText = "Orden Vta."
            .Columns("DocNum").Width = 80
            .Columns("DocNum").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("DocNum").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("DocNum").ReadOnly = True
            'FECHA DE ORDEN
            .Columns("DocDate").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DocDate").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("DocDate").HeaderText = "Fecha Doc."
            .Columns("DocDate").Width = 130
            .Columns("DocDate").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("DocDate").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("DocDate").ReadOnly = True
            'HORA DE IMPRESION
            .Columns("PrintTime").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PrintTime").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("PrintTime").HeaderText = "Hora Creación"
            .Columns("PrintTime").Width = 100
            .Columns("PrintTime").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("PrintTime").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("PrintTime").ReadOnly = True
            'PARTIDAS POR ORDEN
            .Columns("LineNumTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("LineNumTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("LineNumTotal").HeaderText = "Part."
            .Columns("LineNumTotal").Width = 40
            .Columns("LineNumTotal").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("LineNumTotal").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("LineNumTotal").ReadOnly = True
            'PAQUETERIA
            .Columns("TrnspName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("TrnspName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("TrnspName").HeaderText = "Paqueteria"
            .Columns("TrnspName").Width = 140
            .Columns("TrnspName").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("TrnspName").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("TrnspName").ReadOnly = True
            'HORARIO DE PAQUEREIAS
            .Columns("TrnspCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("TrnspCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("TrnspCode").HeaderText = "Horario paq."
            .Columns("TrnspCode").Width = 110
            .Columns("TrnspCode").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("TrnspCode").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("TrnspCode").ReadOnly = True
            'CODIGO CLIENTE
            .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("CardCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("CardCode").HeaderText = "Cliente"
            .Columns("CardCode").Width = 90
            .Columns("CardCode").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("CardCode").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("CardCode").ReadOnly = True
            'NOMBRE CLIENTE
            .Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("CardName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("CardName").HeaderText = "Nombre"
            .Columns("CardName").Width = 500
            .Columns("CardName").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("CardName").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("CardName").ReadOnly = True
            'COMENTARIOS
            .Columns("Comment").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Comment").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("Comment").HeaderText = "Comentario SAP"
            .Columns("Comment").Width = 300
            .Columns("Comment").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Comment").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("Comment").ReadOnly = True
            'PERSONAL SURTIDOR Y ALMACENISTA
            .Columns("Surtidor").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Surtidor").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("Surtidor").HeaderText = "Surtidor"
            .Columns("Surtidor").Width = 250
            .Columns("Surtidor").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Surtidor").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("Surtidor").ReadOnly = True
            'IMPRIMIR PACKLIST
            .Columns("Printed").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Printed").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("Printed").HeaderText = "Impresión pedido"
            .Columns("Printed").Width = 70
            .Columns("Printed").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Printed").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("Printed").ReadOnly = True
            .Columns("Printed").Visible = False
            'IMPRIMIR PACKLIST
            .Columns("Accion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Accion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("Accion").HeaderText = "Accion"
            .Columns("Accion").Width = 130
            .Columns("Accion").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Accion").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("Accion").ReadOnly = True
        End With
    End Sub
    Sub LlenarEmpaque()
        Try
      dgvEntrega.DataSource = SQL.EjecutarProcedimiento("TPD_Estatus_Empaque", "", 0, "")
      EstiloEmpaque()
        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)
        End Try
    End Sub
    Sub EstiloEmpaque()
        'Este metodo cambia el estilo del gridview detalle 
        With Me.dgvEntrega
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.ForeColor = Color.Black
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Partidas
            .Columns("OrdenEmpaque").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("OrdenEmpaque").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("OrdenEmpaque").HeaderText = "Orden Entrega"
            .Columns("OrdenEmpaque").Width = 70
            .Columns("OrdenEmpaque").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("OrdenEmpaque").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("OrdenEmpaque").ReadOnly = True
            'Partidas
            .Columns("OrdenDeVenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("OrdenDeVenta").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("OrdenDeVenta").HeaderText = "Orden De Venta"
            .Columns("OrdenDeVenta").Width = 90
            .Columns("OrdenDeVenta").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("OrdenDeVenta").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("OrdenDeVenta").ReadOnly = True
            'Articulo
            .Columns("FechaDeCreacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaDeCreacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("FechaDeCreacion").HeaderText = "Fecha De Creacion"
            .Columns("FechaDeCreacion").Width = 130
            .Columns("FechaDeCreacion").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("FechaDeCreacion").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("FechaDeCreacion").ReadOnly = True
            'Descripcion
            .Columns("horaCreacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("horaCreacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("horaCreacion").HeaderText = "Hora Creacion"
            .Columns("horaCreacion").Width = 70
            .Columns("horaCreacion").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("horaCreacion").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("horaCreacion").ReadOnly = True
            'Cantidad
            .Columns("Partidas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Partidas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("Partidas").HeaderText = "Part."
            .Columns("Partidas").Width = 40
            .Columns("Partidas").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Partidas").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("Partidas").ReadOnly = True
            'Surtido
            .Columns("CodigoCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("CodigoCliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("CodigoCliente").HeaderText = "Codigo Cliente"
            .Columns("CodigoCliente").Width = 90
            .Columns("CodigoCliente").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("CodigoCliente").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("CodigoCliente").ReadOnly = True
            'Surtido
            .Columns("NombreCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("NombreCliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("NombreCliente").HeaderText = "Nombre Cliente "
            .Columns("NombreCliente").Width = 550
            .Columns("NombreCliente").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("NombreCliente").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("NombreCliente").ReadOnly = True
            'Surtido
            .Columns("Paqueteria").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Paqueteria").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("Paqueteria").HeaderText = "Paqueteria"
            .Columns("Paqueteria").Width = 150
            .Columns("Paqueteria").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Paqueteria").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("Paqueteria").ReadOnly = True
            'Comentario
            .Columns("Comment").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Comment").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("Comment").HeaderText = "Comentario"
            .Columns("Comment").Width = 350
            .Columns("Comment").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Comment").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("Comment").ReadOnly = True
            'Surtido
            .Columns("Cajas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Cajas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("Cajas").HeaderText = "Cajas"
            .Columns("Cajas").Width = 40
            .Columns("Cajas").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Cajas").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("Cajas").ReadOnly = True
            '
            .Columns("Tipo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Tipo").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("Tipo").HeaderText = "Tipo"
            .Columns("Tipo").Width = 100
            .Columns("Tipo").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Tipo").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("Tipo").ReadOnly = True
            'Peso 
            .Columns("Peso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Peso").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("Peso").HeaderText = "Peso"
            .Columns("Peso").DefaultCellStyle.Format = "N2"
            .Columns("Peso").Width = 80
            .Columns("Peso").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Peso").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("Peso").ReadOnly = True
            'Surtido
            .Columns("Accion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Accion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("Accion").HeaderText = "Accion"
            .Columns("Accion").Width = 140
            .Columns("Accion").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Accion").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("Accion").ReadOnly = True
            'Peso 
            .Columns("TrnspCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("TrnspCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            .Columns("TrnspCode").HeaderText = "Horario Paq."
            .Columns("TrnspCode").Width = 150
            .Columns("TrnspCode").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("TrnspCode").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .Columns("TrnspCode").ReadOnly = True
            .Columns("TrnspCode").Visible = False
        End With
    End Sub

    Private Sub dgvSurtir_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvSurtir.RowPrePaint
        amarillo = ColorTranslator.FromHtml("#FDFF6C")
        Try
            'COLOCA COLORES A LAS FILAS PARA DIFERENCIAR EL SURTIDO, SURTIENDO Y PENDIENTE EN DADO CASO
            For i As Integer = 0 To dgvSurtir.Rows.Count - 1
                'COMPARA EL ESTATUS
                If dgvSurtir.Rows(i).Cells("Accion").Value.ToString = "Surtir" Then 'SE DEJA POR SI SE OCUPA
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    'dgvOrdenes.Rows(i).DefaultCellStyle.BackColor = Color.Tomato
                    'dgvOrdenes.Rows(i).DefaultCellStyle.ForeColor = Color.White
                    'dgvOrdenes.Rows(i).DefaultCellStyle.BackColor = Color.Cornsilk
                ElseIf dgvSurtir.Rows(i).Cells("Accion").Value.ToString = "Surtiendo" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    dgvSurtir.Rows(i).DefaultCellStyle.BackColor = Color.Khaki
                    dgvSurtir.Rows(i).DefaultCellStyle.ForeColor = Color.Black

                ElseIf dgvSurtir.Rows(i).Cells("Accion").Value.ToString = "En Espera" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    dgvSurtir.Rows(i).DefaultCellStyle.BackColor = amarillo
                    dgvSurtir.Rows(i).DefaultCellStyle.ForeColor = Color.Black
                ElseIf dgvSurtir.Rows(i).Cells("Accion").Value.ToString = "Cancelado" Then
                    dgvSurtir.Rows(i).DefaultCellStyle.BackColor = Color.LightCoral
                    dgvSurtir.Rows(i).DefaultCellStyle.ForeColor = Color.Black
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub dgvEntrega_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvEntrega.RowPrePaint
        rojo = ColorTranslator.FromHtml("#FFC6C6")
        amarillo = ColorTranslator.FromHtml("#FDFF6C")
        verde = ColorTranslator.FromHtml("#A6FFA0")
        Anaranjado = ColorTranslator.FromHtml("#FFCC80")
        Try
            'COLOCA COLORES A LAS FILAS PARA DIFERENCIAR EL SURTIDO, SURTIENDO Y PENDIENTE EN DADO CASO
            For i As Integer = 0 To dgvEntrega.Rows.Count - 1
                'COMPARA EL ESTATUS
                If dgvEntrega.Rows(i).Cells("Accion").Value.ToString = "Empacar" Then 'SE DEJA POR SI SE OCUPA                    
                ElseIf dgvEntrega.Rows(i).Cells("Accion").Value.ToString = "En Revision" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    dgvEntrega.Rows(i).DefaultCellStyle.BackColor = Color.SkyBlue
                ElseIf dgvEntrega.Rows(i).Cells("Accion").Value.ToString = "Revisado" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    dgvEntrega.Rows(i).DefaultCellStyle.BackColor = Color.LightPink
                ElseIf dgvEntrega.Rows(i).Cells("Accion").Value.ToString = "Empacando" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    dgvEntrega.Rows(i).DefaultCellStyle.BackColor = amarillo
                ElseIf dgvEntrega.Rows(i).Cells("Accion").Value.ToString = "Empacado" Then
                    'COLOCA COLOR A LA CELDA U LA LETRA
                    dgvEntrega.Rows(i).DefaultCellStyle.BackColor = verde
                ElseIf dgvEntrega.Rows(i).Cells("Accion").Value.ToString = "Cancelado" Then
                    dgvEntrega.Rows(i).DefaultCellStyle.BackColor = rojo
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub MEjecuta_Entrega()
        'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
        Dim cmd4 As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        DsOrdenes = New DataSet
        'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
        Try
            cnn = New SqlConnection(StrTpm) 'ORIGINAL
            'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBAS

            cmd4 = New SqlCommand("SP_Operacion_Aux_Entrega", cnn)
            cmd4.CommandType = CommandType.StoredProcedure
            cmd4.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value))
            'MsgBox(DTPFecha.Value.ToString("dddd"))
            cnn.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd4
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 10000
            cmd4.ExecuteNonQuery()
            cmd4.Connection.Close()
            cnn.Close()
        Catch ex As Exception
            MessageBox.Show("Error al insertar las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End Try
        'FIN OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
    End Sub

    Sub MEjecuta_Entrega_Ped()
        'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
        Dim cmd As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        DsOrdenes = New DataSet
        'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
        Try
            cnn = New SqlConnection(StrTpm) 'ORIGINAL
            'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBA
            cmd = New SqlCommand("SP_Operacion_Entrega", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value))
            'MsgBox(DTPFecha.Value.ToString("dddd"))
            cnn.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 10000
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
            cnn.Close()
        Catch ex As Exception
            MessageBox.Show("Error al Mostrar los datos de las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End Try
    End Sub

    Sub MEjecuta_Entrega_Ped_Det()
        'DECLARA LAS VARIABLES USADAS PARA LA CONEXION A SQL Y EL RECORRIDO
        Dim cmd As SqlCommand
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter
        DsOrdenes = New DataSet
        'OPTIENE EL ERROR DE CONEXION O DE CONSULTA O PROCEDIMIENTO
        Try
            cnn = New SqlConnection(StrTpm) 'ORIGINAL
            'cnn = New SqlConnection(StrTpmPrueba) 'PRUEBAS
            cmd = New SqlCommand("SP_Operacion_Entrega_Det", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@fecha", String.Format("{0:yyyy-MM-dd}", dtpFecha.Value))
            'MsgBox(DTPFecha.Value.ToString("dddd"))
            cnn.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            da.SelectCommand.Connection = cnn
            da.SelectCommand.CommandTimeout = 10000
            cmd.ExecuteNonQuery()
            cmd.Connection.Close()
            cnn.Close()
        Catch ex As Exception
            MessageBox.Show("Error al Insertar el detalle de las Ordenes del Día. " + ex.ToString, "Error de Conexion o Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End Try
    End Sub

    'MODIFICADO POR IVAN GONZALEZ
    Dim SQL As New Comandos_SQL()
    Sub Liberacion_Material()
        Try
            dgvLiberacionMat.DataSource = SQL.EjecutarProcedimiento("TPD_Liberacion_Material_3Estatus", "", 0, "")
            EstiloLiberadas()
        Catch ex As Exception
            MsgBox("Error: " + ex.ToString())
            SQL.Cerrar()
        End Try
    End Sub

    Sub EstiloLiberadas()
        'ESTILOS POR COLUMNA
        With Me.dgvLiberacionMat
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

            ''ORDEN DE VENTA
            '.Columns("DocEntry").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("DocEntry").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            '.Columns("DocEntry").DefaultCellStyle.ForeColor = Color.Red
            '.Columns("DocEntry").HeaderText = "Orden de Entrega"
            '.Columns("DocEntry").Width = 70
            '.Columns("DocEntry").ReadOnly = True
            '.Columns("DocEntry").Frozen = True
            ''FECHA DE ORDEN
            '.Columns("PrintTime").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("PrintTime").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            '.Columns("PrintTime").HeaderText = "Fecha Doc."
            '.Columns("PrintTime").Width = 130
            '.Columns("PrintTime").ReadOnly = True

            ''HORA DE IMPRESION
            '.Columns("horaCreacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("horaCreacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            '.Columns("horaCreacion").HeaderText = "Hora Creación"
            '.Columns("horaCreacion").Width = 100
            '.Columns("horaCreacion").ReadOnly = True
            ''PARTIDAS POR ORDEN
            '.Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("DocNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            '.Columns("DocNum").HeaderText = "No. Factura"
            '.Columns("DocNum").Width = 100
            '.Columns("DocNum").ReadOnly = True
            '.Columns("DocNum").Frozen = True

            ''CÓDIGO DEL CLIENTE
            '.Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("CardCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            '.Columns("CardCode").HeaderText = "Cod. Cliente"
            '.Columns("CardCode").Width = 100
            '.Columns("CardCode").ReadOnly = True

            ''NOMBRE DEL CLIENTE
            '.Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            '.Columns("CardName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            '.Columns("CardName").HeaderText = "Cliente"
            '.Columns("CardName").Width = 300
            '.Columns("CardName").ReadOnly = True
            ''CAJAS
            '.Columns("BoxTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("BoxTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            '.Columns("BoxTotal").HeaderText = "Cajas"
            '.Columns("BoxTotal").Width = 70
            '.Columns("BoxTotal").ReadOnly = True

            ''ACCIÓN
            '.Columns("Action").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("Action").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            '.Columns("Action").HeaderText = "Accion"
            '.Columns("Action").Width = 170
            '.Columns("Action").ReadOnly = True

            ''PAQUETERÍA
            '.Columns("TrnspName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("TrnspName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            '.Columns("TrnspName").HeaderText = "Paqueteria"
            '.Columns("TrnspName").Width = 300
            '.Columns("TrnspName").ReadOnly = True

            ''PERSONAL SURTIDOR Y ALMACENISTA
            '.Columns("Fletes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("Fletes").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            '.Columns("Fletes").HeaderText = "Fletes"
            '.Columns("Fletes").Width = 70
            '.Columns("Fletes").ReadOnly = True
            ''IMPRIMIR PACKLIST
            '.Columns("Address").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("Address").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            '.Columns("Address").HeaderText = "Direccion"
            '.Columns("Address").Width = 90
            '.Columns("Address").ReadOnly = True
            '.Columns("Address").Visible = False
            ''IMPRIMIR PACKLIST
            '.Columns("Address").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("Address").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            '.Columns("Address").HeaderText = "Direccion"
            '.Columns("Address").Width = 150
            '.Columns("Address").ReadOnly = True
            ''IMPRIMIR PACKLIST
            '.Columns("Comments").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("Comments").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 14, FontStyle.Regular)
            '.Columns("Comments").HeaderText = "Cometario"
            '.Columns("Comments").Width = 300
            '.Columns("Comments").ReadOnly = True
            ''IMPRIMIR PACKLIST
            '.Columns("Facturado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("Facturado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 16, FontStyle.Bold)
            '.Columns("Facturado").HeaderText = "Facturado"
            '.Columns("Facturado").Width = 200
            '.Columns("Facturado").ReadOnly = True


            'ORDEN DE VENTA
            .Columns("DocEntry").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DocEntry").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("DocEntry").DefaultCellStyle.ForeColor = Color.Red
            .Columns("DocEntry").HeaderText = "Orden de Entrega"
            .Columns("DocEntry").Width = 70
            .Columns("DocEntry").ReadOnly = True
            .Columns("DocEntry").Frozen = True
            'FECHA DE ORDEN
            .Columns("PrintTime").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PrintTime").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("PrintTime").HeaderText = "Fecha Doc."
            .Columns("PrintTime").Width = 130
            .Columns("PrintTime").ReadOnly = True
            'HORA DE IMPRESION
            .Columns("horaCreacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("horaCreacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("horaCreacion").HeaderText = "Hora Creación"
            .Columns("horaCreacion").Width = 95
            .Columns("horaCreacion").ReadOnly = True
            'PARTIDAS POR ORDEN
            .Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DocNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("DocNum").HeaderText = "No. Factura"
            .Columns("DocNum").Width = 100
            .Columns("DocNum").ReadOnly = True
            .Columns("DocNum").Frozen = True
            'PAQUETERIA
            .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("CardCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("CardCode").HeaderText = "Cod. Cliente"
            .Columns("CardCode").Width = 100
            .Columns("CardCode").ReadOnly = True
            'HORARIO DE PAQUEREIAS
            .Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("CardName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("CardName").HeaderText = "Cliente"
            .Columns("CardName").Width = 300
            .Columns("CardName").ReadOnly = True
            'CODIGO CLIENTE
            .Columns("BoxTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("BoxTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("BoxTotal").HeaderText = "Cajas"
            .Columns("BoxTotal").Width = 70
            .Columns("BoxTotal").ReadOnly = True
            'PESO ESTIMADO
            '.Columns("Peso Estimado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Columns("Peso Estimado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            '.Columns("Peso Estimado").HeaderText = "Peso Estimado (KG)"
            '.Columns("Peso Estimado").Width = 100
            '.Columns("Peso Estimado").ReadOnly = True
            'PESO REAL
            .Columns("Peso Real").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Peso Real").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("Peso Real").HeaderText = "Peso Real (KG)"
            .Columns("Peso Real").Width = 100
            .Columns("Peso Real").ReadOnly = True
            'Action
            .Columns("Acción").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Acción").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Regular)
            .Columns("Acción").HeaderText = "Accion"
            .Columns("Acción").Width = 150
            .Columns("Acción").ReadOnly = True
            'PAQUETERIA
            .Columns("TrnspName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("TrnspName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("TrnspName").HeaderText = "Paqueteria"
            .Columns("TrnspName").Width = 320
            .Columns("TrnspName").ReadOnly = True
            'direccion
            .Columns("Address").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Address").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("Address").HeaderText = "Direccion"
            .Columns("Address").Width = 350
            .Columns("Address").ReadOnly = True
            .Columns("Address").Visible = False

            'comentario sap
            .Columns("Comments").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Comments").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Regular)
            .Columns("Comments").HeaderText = "Comentario"
            .Columns("Comments").Width = 300
            .Columns("Comments").ReadOnly = True
            'Total
            .Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Total").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("Total").HeaderText = "Total S/IVA"
            .Columns("Total").Width = 130
            .Columns("Total").DefaultCellStyle.Format = "N2"
            .Columns("Total").ReadOnly = True
            .Columns("Total").Visible = False
            'facturado
            .Columns("Facturado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Facturado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 15, FontStyle.Bold)
            .Columns("Facturado").HeaderText = "Facturado"
            .Columns("Facturado").Width = 150
            .Columns("Facturado").ReadOnly = True
        End With
    End Sub
    '------------------------------------------------------------------------

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MEjecuta_Entrega()
        MEjecuta_Entrega_Ped()
        MEjecuta_Entrega_Ped_Det()
        MLlenaOrdenesSurtir()
        LlenarEmpaque()
        Liberacion_Material()
    End Sub

    Private Sub dgvEntrega_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEntrega.CellContentClick

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub dgvLiberacionMat_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub dtpFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha.ValueChanged

    End Sub

    Private Sub Splitter1_SplitterMoved(sender As Object, e As SplitterEventArgs)

    End Sub

    Private Sub dgvLiberacionMat_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvLiberacionMat.RowPostPaint
        'Se definen los colores con paleta de coroles en RGB
        rojo = ColorTranslator.FromHtml("#FFC6C6")
        amarillo = ColorTranslator.FromHtml("#FDFF6C")
        verde = ColorTranslator.FromHtml("#A6FFA0")
        Anaranjado = ColorTranslator.FromHtml("#FFCC80")
        'Dependiendo del estado de la columna Estatus la fila se pintara de un cierto color y en algunos casos cambiara las letras a negritas
        For i As Integer = 0 To Me.dgvLiberacionMat.Rows.Count - 1

            If dgvLiberacionMat.Rows(i).Cells("Facturado").Value = "Facturado" Then

                Me.dgvLiberacionMat.Rows(i).DefaultCellStyle.BackColor = verde

            End If
        Next
    End Sub
End Class