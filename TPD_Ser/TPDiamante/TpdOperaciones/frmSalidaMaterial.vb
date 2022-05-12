Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.IO
Public Class frmSalidaMaterial
    Dim DTVliberacionMat As DataView
    Dim DTVDetalleMat As DataView
    Dim col1 As New DataGridViewButtonColumn
    Dim SQLEmpleado As String
    'Colores creados para uso en el grid DGVoperacionVta
    Dim rojo As Color
    Dim amarillo As Color
    Dim verde As Color
    Dim Anaranjado As Color
    Private Sub frmSalidaMaterial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LlenarSalidaMat()
    End Sub
    Public Sub LlenarSalidaMat()
        'llena a travez de una consulta de sql el gridview detalle
        Try
            'VARIABLE DE CADENA DE SQL
            Dim SQLOrdenes_aut As String
            'VARIABLES DE CONEXION DE LLENADO
            Dim cmd1 As SqlCommand
            Dim cnn1 As SqlConnection = Nothing
            Dim da1 As SqlDataAdapter
            Dim DsLiberacion = New DataSet
            'ALAMACENA LA CONSULTA
            SQLOrdenes_aut = "Select t0.DocEntry , FORMAT(t0.PrintTime, 'yyyy-MM-dd') as PrintTime , "
            SQLOrdenes_aut &= "CASE WHEN Len(DocTime)=4 THEN Stuff(DocTime,3,0,':') WHEN LEN(DocTime) =3 THEN Stuff(DocTime,2,0,':') END AS horaCreacion "
            SQLOrdenes_aut &= ",t0.DocNum,t1.CardCode,t1.CardName, BoxReal, IIF(t0.Action='Empacado','Por Liberar',T0.Action) as Action ,t3.TrnspName "
            SQLOrdenes_aut &= ",(SELECT  count(T11.LineNum) from ZPRUEBAS16ABR2019.dbo.DLN1 T11 LEFT JOIN ZPRUEBAS16ABR2019.dbo.OITM T22 ON T11.ItemCode=T22.ItemCode "
            SQLOrdenes_aut &= "LEFT JOIN ZPRUEBAS16ABR2019.dbo.OITB T33 ON T22.ItmsGrpCod=T33.ItmsGrpCod WHERE T11.DocEntry=T0.DocEntry AND T22.ItmsGrpCod=150) as Fletes "
            SQLOrdenes_aut &= ",Address,Comments from Operacion_Entrega T0 LEFT JOIN ZPRUEBAS16ABR2019.dbo.ODLN T1 ON T1.DocEntry=T0.DocEntry "
            SQLOrdenes_aut &= "LEFT JOIN Operacion_Orden T2 ON T0.DocNum=T2.DocNum LEFT JOIN ZPRUEBAS16ABR2019.dbo.OShP T3 ON T1.TrnspCode =t3.TrnspCode "
            SQLOrdenes_aut &= "where "
            SQLOrdenes_aut &= "t0.Action='En piso' or  t0.Action='Guias Generadas' "
            cnn1 = New SqlConnection(StrTpmPrueba)
            'ALMACENA LA CONSULTA EN UN COMMAND SQL
            cmd1 = New SqlCommand(SQLOrdenes_aut, cnn1)
            'CONVIERTE EL TEXTO EN CONSULTA
            cmd1.CommandType = CommandType.Text
            'APERTURA LA CONEXION
            cnn1.Open()
            'INSTANCIA UN ADAPTER
            da1 = New SqlDataAdapter
            'ALMACENA EL COMMAND DE SQL EN EL ADAPTER
            da1.SelectCommand = cmd1
            'LO EJECUTA CON LA CONEXION
            da1.SelectCommand.Connection = cnn1
            'TIEMPO DE ESPERA DE LA CONEXION
            da1.SelectCommand.CommandTimeout = 10000
            'EJECUTA LA CONSULTA
            cmd1.ExecuteNonQuery()
            'CIERRA EL COMMAND DE SQL
            cmd1.Connection.Close()
            'CIERRA LA CONEXION
            cnn1.Close()
            'LLENA EL ADAPTER A UN DATA SET
            da1.Fill(DsLiberacion)
            'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
            DTVDetalleMat = New DataView
            'ALMACENA EN DATA SET DE MODO TABLA
            DTVDetalleMat.Table = DsLiberacion.Tables(0)
            'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
            'DgvAutorizaciones.DataSource = Nothing
            'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
            DGVOrdenesLib.DataSource = DTVDetalleMat
            'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW
            MEstiloMat()
        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)
        End Try
    End Sub
    Sub MEstiloMat()
        'ESTILOS POR COLUMNA
        With Me.DGVOrdenesLib
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
            .Columns("DocEntry").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DocEntry").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            .Columns("DocEntry").DefaultCellStyle.ForeColor = Color.Red
            .Columns("DocEntry").HeaderText = "Orden de Entrega"
            .Columns("DocEntry").Width = 70
            .Columns("DocEntry").ReadOnly = True
            'FECHA DE ORDEN
            .Columns("PrintTime").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PrintTime").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("PrintTime").HeaderText = "Fecha Doc."
            .Columns("PrintTime").Width = 85
            .Columns("PrintTime").ReadOnly = True
            'HORA DE IMPRESION
            .Columns("horaCreacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("horaCreacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("horaCreacion").HeaderText = "Hora Creación"
            .Columns("horaCreacion").Width = 65
            .Columns("horaCreacion").ReadOnly = True
            'PARTIDAS POR ORDEN
            .Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DocNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            .Columns("DocNum").HeaderText = "Orden de Vta."
            .Columns("DocNum").Width = 70
            .Columns("DocNum").ReadOnly = True
            'PAQUETERIA
            .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("CardCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("CardCode").HeaderText = "Cod. Cliente"
            .Columns("CardCode").Width = 65
            .Columns("CardCode").ReadOnly = True
            'HORARIO DE PAQUEREIAS
            .Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("CardName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("CardName").HeaderText = "Cliente"
            .Columns("CardName").Width = 200
            .Columns("CardName").ReadOnly = True
            'CODIGO CLIENTE
            .Columns("BoxReal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("BoxReal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("BoxReal").HeaderText = "Cajas"
            .Columns("BoxReal").Width = 50
            .Columns("BoxReal").ReadOnly = True
            'NOMBRE CLIENTE
            .Columns("Action").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Action").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("Action").HeaderText = "Accion"
            .Columns("Action").Width = 150
            .Columns("Action").ReadOnly = True
            'COMENTARIOS
            .Columns("TrnspName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("TrnspName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("TrnspName").HeaderText = "Paqueteria"
            .Columns("TrnspName").Width = 150
            .Columns("TrnspName").ReadOnly = True
            'PERSONAL SURTIDOR Y ALMACENISTA
            .Columns("Fletes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Fletes").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("Fletes").HeaderText = "Fletes"
            .Columns("Fletes").Width = 50
            .Columns("Fletes").ReadOnly = True
            'IMPRIMIR PACKLIST
            .Columns("Address").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Address").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("Address").HeaderText = "Direccion"
            .Columns("Address").Width = 90
            .Columns("Address").ReadOnly = True
            .Columns("Address").Visible = False
            'IMPRIMIR PACKLIST
            .Columns("Address").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Address").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("Address").HeaderText = "Direccion"
            .Columns("Address").Width = 150
            .Columns("Address").ReadOnly = True
            'IMPRIMIR PACKLIST
            .Columns("Comments").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Comments").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("Comments").HeaderText = "Cometario"
            .Columns("Comments").Width = 150
            .Columns("Comments").ReadOnly = True
        End With
    End Sub
    Sub MDetalleEstiloMat()
        'ESTILOS POR COLUMNA
        With Me.DGVDetalleLib
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
            .Columns("ItemCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("ItemCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)
            .Columns("ItemCode").DefaultCellStyle.ForeColor = Color.Black
            .Columns("ItemCode").HeaderText = "Codigo"
            .Columns("ItemCode").Width = 65
            .Columns("ItemCode").ReadOnly = True
            'ORDEN DE VENTA
            .Columns("Description").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Description").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)
            .Columns("Description").DefaultCellStyle.ForeColor = Color.Black
            .Columns("Description").HeaderText = "Description"
            .Columns("Description").Width = 230
            .Columns("Description").ReadOnly = True
            'FECHA DE ORDEN
            .Columns("Surtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Surtido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)
            .Columns("Surtido").HeaderText = "Cantidad"
            .Columns("Surtido").Width = 65
            .Columns("Surtido").ReadOnly = True
        End With
    End Sub
    Sub LlenarDetalleEmpaque(ORDEN As String)
        'llena a travez de una consulta de sql el gridview detalle
        Try
            'VARIABLE DE CADENA DE SQL
            Dim SQLOrdene As String
            'VARIABLES DE CONEXION DE LLENADO
            Dim cmd As SqlCommand
            Dim cnn As SqlConnection = Nothing
            Dim DsOrdenes = New DataSet
            Dim da As SqlDataAdapter
            'ALAMACENA LA CONSULTA

            SQLOrdene &= "  Select ItemCode,Description,Surtido from  TPM09FEB2019.dbo.Operacion_Detalle_Entrega where DocEntry='" + ORDEN + "'"

            cnn = New SqlConnection(StrConPru)
            'ALMACENA LA CONSULTA EN UN COMMAND SQL
            cmd = New SqlCommand(SQLOrdene, cnn)
            'CONVIERTE EL TEXTO EN CONSULTA
            cmd.CommandType = CommandType.Text
            'APERTURA LA CONEXION
            cnn.Open()
            'INSTANCIA UN ADAPTER
            da = New SqlDataAdapter
            'ALMACENA EL COMMAND DE SQL EN EL ADAPTER
            da.SelectCommand = cmd
            'LO EJECUTA CON LA CONEXION
            da.SelectCommand.Connection = cnn
            'TIEMPO DE ESPERA DE LA CONEXION
            da.SelectCommand.CommandTimeout = 10000
            'EJECUTA LA CONSULTA
            cmd.ExecuteNonQuery()
            'CIERRA EL COMMAND DE SQL
            cmd.Connection.Close()
            'CIERRA LA CONEXION
            cnn.Close()
            'LLENA EL ADAPTER A UN DATA SET
            da.Fill(DsOrdenes)
            'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
            DTVDetalleMat = New DataView
            'ALMACENA EN DATA SET DE MODO TABLA
            DTVDetalleMat.Table = DsOrdenes.Tables(0)
            'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
            DGVDetalleLib.DataSource = Nothing
            'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
            DGVDetalleLib.DataSource = DTVDetalleMat
            'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE DETALLE ORDENES
            MDetalleEstiloMat()
        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)
        End Try
    End Sub


    Private Sub DGVOrdenesLib_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGVOrdenesLib.CellMouseDoubleClick

    End Sub

    Private Sub DGVOrdenesLib_KeyDown(sender As Object, e As KeyEventArgs) Handles DGVOrdenesLib.KeyDown

        If (MessageBox.Show("Realmente desea terminar el proceso de operacion ." & vbCrLf & vbCrLf & "",
                            "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
        Else
            DocNumEmpacar = DGVOrdenesLib.CurrentRow.Cells("DocEntry").Value.ToString

            'MANDA A LLAMAR EL FORMULARIO DE SURTIDO
            frmDetalleLiberacionvb.MdiParent = Inicio
            frmDetalleLiberacionvb.Show()

            'Dim row As DataGridViewRow = DGVOrdenesLib.CurrentRow()
            'SQLEmpleado = "UPDATE Operacion_Entrega  SET Action='Finalizado'"
            'SQLEmpleado &= "WHERE DocEntry = '" + row.Cells("DocEntry").Value.ToString + "' "
            'Try
            '    'CONECTA A LA BASE DE DATOS
            '    conexion_universal.conectar_pru()
            '    'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            '    conexion_universal.slq_s = New SqlCommand(SQLEmpleado, conexion_universal.conexion_uni_pru)
            '    'EJECUTA LA CONSULTA
            '    conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
            '    conexion_universal.cerrar_conectar_pru() 'CIERRA LA CONEXION

            'Catch ex As Exception
            '    MsgBox("Error al Actualizar el Status en la Orden: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
            '    conexion_universal.rd_s.Close() 'CIERRA EL READE
            '    conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
            '    CierraDialogAcceso = False
            '    Return
            'End Try
            'LlenarSalidaMat()
        End If


    End Sub

    Private Sub DGVOrdenesLib_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGVOrdenesLib.CellFormatting

    End Sub

    Private Sub DGVDetalleLib_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGVDetalleLib.CellFormatting

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub

    Private Sub DGVOrdenesLib_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVOrdenesLib.CellContentClick

    End Sub

    Private Sub DGVOrdenesLib_SelectionChanged(sender As Object, e As EventArgs) Handles DGVOrdenesLib.SelectionChanged
        'En este evento actualizaremos el grid detalle cuando el usuario se mueva entre celdas 
        DGVDetalleLib.Columns.Clear()
        'Valida el ordenamiento del grid para el cambio de posicion 
        If DGVOrdenesLib.CurrentRow Is Nothing Then
        Else
            Try
                Dim row As DataGridViewRow = DGVOrdenesLib.CurrentRow()
                'Llena el datagrid detalle 
                LlenarDetalleEmpaque(CStr(row.Cells("DocEntry").Value).ToString)
            Catch ex As Exception
                MsgBox("Error al obtener la Orden en llenado de Grid Detalle." + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de Dato")
            End Try
        End If
    End Sub

    Private Sub BtnActualizar_Click(sender As Object, e As EventArgs) Handles BtnActualizar.Click
        LlenarSalidaMat()
    End Sub
End Class