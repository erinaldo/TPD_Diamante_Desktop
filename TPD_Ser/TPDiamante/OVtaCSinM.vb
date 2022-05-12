'Ordenes de Venta Creadas Con Falta de Material
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Text
Imports System.Net
Imports ClosedXML.Excel
Imports System.IO

Public Class OVtaCSinM
    Dim dvdet As New DataView
    Dim dvaux As New DataView
    Dim NumOVta As Integer
    Dim DtMArticulo As New DataTable
    Dim VErrOv As Integer = 0
    Dim oStrem As New System.IO.MemoryStream
    Dim dvenc As New DataTable
    Dim band As Boolean = False

    Dim SQL As New Comandos_SQL()

    Dim saveFileDialog1 As New SaveFileDialog()

    Dim ColorOriginal As Color
    Dim Inf_ordencompraOriginal As String
    Dim Inf_ConfirmadoOriginal As Integer
    Dim Inf_ArticuloOriginal As String
    Dim Inf_PedidoCte As String
    Dim Inf_pendientefacturar As String
    Dim Inf_stockactual As String
    Dim Inf_piezasfaltantes As String
    Dim Inf_asolicitar As String
    Dim Inf_Almacen As String
    Dim Inf_montofaltantes As String
    Dim Inf_solicitado As String
    Dim OrdenVentaRelacionada As String

    Private Sub OVtaCSinM_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim aux As String

        Button2.Visible = False
        Label9.Visible = False

        aux = sender.ToString
        Me.Text = "Solicitud de Articulos -- " & aux.Substring(aux.IndexOf(".") + 1, (aux.IndexOf(",") - aux.IndexOf(".")) - 1) & ".vb"
        If UsrTPM = "MANAGER" Then
            DGVEncOrdVta.Height = 300

            Label1.Location = New Point(5, 363)
            DGVDetOrdVta.Location = New Point(5, 378)
            DGVDetOrdVta.Height = 250

            Me.Height = 673
        End If

        panelBotones.Visible = True
        If UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" Or UsrTPM = "COMERCIAL" Then
            panelBotones.Visible = False
        End If


        Dim Consulta As String = ""

        Consulta = "SELECT T0.[ItemCode] AS Articulo,T0.ItemName AS Descripcion,"
        Consulta &= "T1.[ItmsGrpNam] AS Grupo_Articulo, T2.OnHand AS Existencia "
        Consulta &= "FROM OITM T0  INNER JOIN OITB T1 ON T0.ItmsGrpCod = T1.ItmsGrpCod "
        Consulta &= "LEFT JOIN OCRD T3 ON T0.Cardcode = T3.CardCode "
        Consulta &= "LEFT JOIN OITW T2 ON T0.ItemCode = T2.ItemCode AND T2.WhsCode = 01 "
        Consulta &= "WHERE T0.frozenFor <> 'Y' "
        Consulta &= "ORDER BY T1.[ItmsGrpNam],T0.[ItemCode] "

        Dim comando As New SqlClient.SqlCommand(Consulta)
        comando.Connection = New SqlClient.SqlConnection(StrCon)
        comando.Connection.Open()
        Dim Adaptador As New SqlClient.SqlDataAdapter(comando)
        Adaptador.Fill(DtMArticulo)
        comando.Connection.Close()

        Me.CmbArticulo.DataSource = DtMArticulo
        Me.CmbArticulo.DisplayMember = "Articulo"
        Me.CmbArticulo.ValueMember = "Articulo"
        Me.CmbArticulo.SelectedValue = "0"


        Dim dtFecha As Date = DateSerial(Year(Date.Today), Date.Now.Month, (Date.Now.Day) - 1)
        Me.DtpFechaIni.Value = dtFecha
        DtpFechaIni.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        DtpFechaIni.Format = DateTimePickerFormat.Custom


        'Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaTer.Value = Format(DateTime.Now, "dd/MM/yyyy hh:mm:ss")
        DtpFechaTer.CustomFormat = "dd/MM/yyyy"
        DtpFechaTer.Format = DateTimePickerFormat.Custom

        If UsrTPM = "COMPRAS1" Or UsrTPM = "MANAGER" Or UsrTPM = "SISTEMAS" Or UsrTPM = "ALMACEN1" _
        Or UsrTPM = "ACOMPRAS" Or UsrTPM = "MMAZZOCO" Or UsrTPM = "PRUEBAS" Then
            BtnVerSol.Enabled = True
            BtnGuardar.Enabled = True
        End If
        cargar_registros2()
        'LlenarTablas()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles BtnActualizar.Click
        'MessageBox.Show(DtpFechaIni.Value.ToString("HH:mm:ss"))
        cargar_registros2()
        BuscaOrdenes()
        'MessageBox.Show(DtpFechaTer.Value.ToString("hh:mm:ss"))
    End Sub

    Sub LlenarTablas()
        Dim DsVtasDet As New DataSet

        DsVtasDet = SQL.EjecutarProcedimientoTB("TPD_SolicitudArticulosPruebas_Pruebas", "@FechaIni,@FechaTer", 2, DtpFechaIni.Value.ToString("yyyy-MM-dd") + "," + DtpFechaTer.Value.ToString("yyyy-MM-dd"))

        DGVEncOrdVta.DataSource = DsVtasDet.Tables(1)
        EstiloDGVEncOrdVta()

        DGVDetOrdVta.DataSource = DsVtasDet.Tables(0)
        EstiloDGVDetOrdVta()

        dvaux.Table = DsVtasDet.Tables(2)
        dvaux.RowFilter = "Filtro <> 'TODOS'"
        EstiloDGVAux()

    End Sub

    Sub EstiloDGVEncOrdVta()
        With DGVEncOrdVta
            '.DataSource = dvenc 'DsVtasDet.Tables("EncOrdVta")
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .RowHeadersWidth = 35

            .MultiSelect = True
            .AllowUserToAddRows = False
            .AllowDrop = False
            .AllowUserToDeleteRows = False

            .Columns(0).HeaderText = "#"
            .Columns(0).Width = 30
            .Columns(0).ReadOnly = True

            .Columns(1).Visible = False

            .Columns(2).HeaderText = "Articulo"
            .Columns(2).Width = 100
            .Columns(2).ReadOnly = True

            .Columns(3).HeaderText = "# Fabricante"
            .Columns(3).Width = 100
            .Columns(3).ReadOnly = True

            .Columns(4).HeaderText = "Descripción"
            .Columns(4).Width = 325
            .Columns(4).ReadOnly = True

            .Columns(5).HeaderText = "Línea"
            .Columns(5).Width = 100
            .Columns(5).ReadOnly = True

            .Columns(6).HeaderText = "Proveedor"
            .Columns(6).Width = 150
            .Columns(6).ReadOnly = True

            .Columns(7).HeaderText = "Pro. Alterno"
            .Columns(7).Width = 150
            .Columns(7).ReadOnly = True

            .Columns(8).HeaderText = "Pedido del Cliente"
            .Columns(8).Width = 55
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(8).DefaultCellStyle.Format = "###,###,###"
            .Columns(8).ReadOnly = True

            .Columns(9).HeaderText = "Pendiente de Facturar"
            .Columns(9).Width = 55
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(9).DefaultCellStyle.Format = "###,###,###"
            .Columns(9).ReadOnly = True

            .Columns(10).HeaderText = "Stock Actual"
            .Columns(10).Width = 55
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(10).DefaultCellStyle.Format = "###,###,###"
            .Columns(10).ReadOnly = True

            .Columns(11).HeaderText = "Piezas Faltantes"
            .Columns(11).Width = 55
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(11).DefaultCellStyle.Format = "###,###,###"
            .Columns(11).ReadOnly = True

            .Columns(12).HeaderText = "A Solicitar"
            .Columns(12).Width = 55
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(12).DefaultCellStyle.Format = "###,###,###"
            .Columns(12).ReadOnly = False

            .Columns(13).HeaderText = "$ Precio Promedio Vta"
            .Columns(13).Width = 60
            .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(13).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(13).ReadOnly = True

            .Columns(14).HeaderText = "$ Monto Vta Pzas Faltantes"
            .Columns(14).Width = 70
            .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(14).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(14).ReadOnly = True

            .Columns(15).HeaderText = "Solicitado"
            .Columns(15).Width = 60
            .Columns(15).DefaultCellStyle.Format = "###,###,###"
            .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(15).ReadOnly = True
            '.Columns(13).Visible = False

            .Columns(16).HeaderText = "Pzas Pendientes"
            .Columns(16).Width = 65
            .Columns(16).DefaultCellStyle.Format = "###,###,###"
            .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(16).ReadOnly = True
            .Columns(16).DefaultCellStyle.ForeColor = Color.Blue
            .Columns(16).DefaultCellStyle.SelectionForeColor = Color.Yellow
            .Columns(16).Visible = False

            .Columns(17).HeaderText = "Pzas a Solicitar"
            .Columns(17).Width = 52
            .Columns(17).DefaultCellStyle.Format = "###,###,###"
            .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(17).Visible = False

            .Columns(18).Visible = False '
        End With
    End Sub

    Sub EstiloDGVDetOrdVta()
        With DGVDetOrdVta
            '.DataSource = dvdet
            .ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = True
            .AllowUserToAddRows = False
            'Color de linea del grid
            .Columns(0).HeaderText = "Usuario Ventas"
            .Columns(0).Width = 90

            .Columns(1).HeaderText = "Fecha Creación"
            .Columns(1).Width = 70

            .Columns(2).HeaderText = "Orden de Vta"
            .Columns(2).Width = 45

            .Columns(3).HeaderText = "Clave Cliente"
            .Columns(3).Width = 50

            .Columns(4).HeaderText = "Nombre Cliente"
            .Columns(4).Width = 175

            .Columns(5).HeaderText = "Total del Pedido"
            .Columns(5).Width = 60
            .Columns(5).DefaultCellStyle.Format = "###,###,###.##"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(6).Visible = False

            .Columns(7).HeaderText = "Articulo"
            .Columns(7).Width = 100

            .Columns(8).HeaderText = "# Fabricante"
            .Columns(8).Width = 100

            .Columns(9).HeaderText = "Descripción"
            .Columns(9).Width = 160

            .Columns(10).Visible = False

            .Columns(11).HeaderText = "Proveedor"
            .Columns(11).Width = 160
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(11).DefaultCellStyle.Format = "###,###,###"

            .Columns(12).HeaderText = "Pro. Alterno"
            .Columns(12).Width = 160
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(12).DefaultCellStyle.Format = "###,###,###"

            .Columns(13).HeaderText = "Pedido del Cliente"
            .Columns(13).Width = 55
            .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(13).DefaultCellStyle.Format = "###,###,###"

            .Columns(14).HeaderText = "Pendiente por Facturar"
            .Columns(14).Width = 55
            .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(14).DefaultCellStyle.Format = "###,###,###"


            .Columns(15).HeaderText = "Stock Actual"
            .Columns(15).Width = 55
            .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(15).DefaultCellStyle.Format = "###,###,###"

            .Columns(16).HeaderText = "Piezas Faltantes"
            .Columns(16).Width = 55
            .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(16).DefaultCellStyle.Format = "###,###,###"

            .Columns(17).HeaderText = "A Solicitar"
            .Columns(17).Width = 55
            .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(17).DefaultCellStyle.Format = "###,###,###"
            .Columns(17).ReadOnly = False

            .Columns(18).HeaderText = "$ Precio Vta"
            .Columns(18).Width = 60
            .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(18).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(19).HeaderText = "$ Monto Vta Pzas Faltantes"
            .Columns(19).Width = 70
            .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(19).DefaultCellStyle.Format = "###,###,###.00"

        End With
    End Sub

    Sub EstiloDGVAux()
        With DGVAux

            .Columns(0).HeaderText = "Usuario Ventas"

            .Columns(1).HeaderText = "Fecha Creación"

            .Columns(2).HeaderText = "Orden de Vta"

            .Columns(3).HeaderText = "Clave Cliente"

            .Columns(4).HeaderText = "Nombre Cliente"

            .Columns(5).Visible = False

            .Columns(6).Visible = False

            .Columns(7).HeaderText = "Articulo"

            .Columns(8).HeaderText = "# Fabricante"

            .Columns(9).HeaderText = "Descripción"

            .Columns(10).HeaderText = "Linea"

            .Columns(11).Visible = False

            .Columns(12).HeaderText = "Pedido del Cliente"

            .Columns(13).HeaderText = "Pendiente por Facturar"

            .Columns(14).HeaderText = "Stock Actual"

            .Columns(15).HeaderText = "Piezas Faltantes"

            .Columns(16).HeaderText = "Requerido"



            .Columns(17).Visible = False

            .Columns(18).Visible = False

            .Columns(19).Visible = False

            .Columns(20).Visible = False

        End With
    End Sub

    'Sub cargar_registros()
    ' 'Nuevo objeto Dataset   
    ' Dim DsVtasDet As New DataSet
    ' Dim ExisteSolicitud As Boolean = False
    ' Button2.Visible = False
    ' Label9.Visible = False

    ' 'SE EJECUTA PROCEDIMIENTO ALMACENADO Y DEVUELVE DATA SET
    ' DsVtasDet = SQL.EjecutarProcedimientoTB("TPD_SolicitudArticulosPruebas", "@FechaIni,@FechaTer", 2, DtpFechaIni.Value.ToString("yyyy-MM-dd") + "," + DtpFechaTer.Value.ToString("yyyy-MM-dd"))

    ' DsVtasDet.Tables(0).TableName = "DetOrdVta"
    ' DsVtasDet.Tables(1).TableName = "EncOrdVta"

    ' dvdet.Table = DsVtasDet.Tables("DetOrdVta")

    ' dvaux.Table = DsVtasDet.Tables(2)
    ' dvaux.RowFilter = "Filtro <> 'TODOS'"
    ' '**********************************************************************************************************************************
    ' DGVAux.DataSource = dvaux
    ' With DGVAux
    '  .Columns(0).HeaderText = "Usuario Ventas"
    '  .Columns(1).HeaderText = "Fecha Creación"
    '  .Columns(2).HeaderText = "Orden de Vta"
    '  .Columns(3).HeaderText = "Clave Cliente"
    '  .Columns(4).HeaderText = "Nombre Cliente"
    '  .Columns(5).Visible = False
    '  .Columns(6).Visible = False
    '  .Columns(7).HeaderText = "Articulo"
    '  .Columns(8).HeaderText = "# Fabricante"
    '  .Columns(9).HeaderText = "Descripción"
    '  .Columns(10).HeaderText = "Linea"
    '  .Columns(11).Visible = False
    '  .Columns(12).HeaderText = "Pedido del Cliente"
    '  .Columns(13).HeaderText = "Pendiente por Facturar"
    '  .Columns(14).HeaderText = "Stock Actual"
    '  .Columns(15).HeaderText = "Piezas Faltantes"
    '  .Columns(16).HeaderText = "Requerido"
    '  .Columns(17).Visible = False
    '  .Columns(18).Visible = False
    '  .Columns(19).Visible = False
    '  .Columns(20).Visible = False
    ' End With
    ' '**********************************************************************************************************************************

    ' dvenc = DsVtasDet.Tables("EncOrdVta")
    ' '**********************************************************************************************************************************
    ' 'Uno
    ' With DGVEncOrdVta
    '  .DataSource = dvenc 'DsVtasDet.Tables("EncOrdVta")
    '  .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    '  .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
    '  .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    '  .DefaultCellStyle.BackColor = Color.AliceBlue
    '  .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    '  .RowHeadersWidth = 35

    '  .MultiSelect = True
    '  .AllowUserToAddRows = False
    '  .AllowDrop = False
    '  .AllowUserToDeleteRows = False

    '  .Columns("Num").HeaderText = "#"
    '  .Columns("Num").Width = 30
    '  .Columns("Num").ReadOnly = True

    '  .Columns("Filtro").HeaderText = "Artículo"
    '  .Columns("Filtro").Width = 70
    '  .Columns("Filtro").ReadOnly = True

    '  .Columns("Articulo").Visible = False

    '  .Columns("U_ArticuloProveedor").HeaderText = "# Fabricante"
    '  .Columns("U_ArticuloProveedor").Width = 100
    '  .Columns("U_ArticuloProveedor").ReadOnly = True

    '  .Columns("Descripcion").HeaderText = "Descripción"
    '  .Columns("Descripcion").Width = 325
    '  .Columns("Descripcion").ReadOnly = True

    '  .Columns("Linea").HeaderText = "Linea"
    '  .Columns("Linea").Width = 80
    '  .Columns("Linea").ReadOnly = True

    '  If UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" Or UsrTPM = "COMERCIAL" Then
    '   .Columns("Articulo").Visible = False
    '  End If
    '  .Columns("Proveedor").HeaderText = "Proveedor"
    '  .Columns("Proveedor").Width = 100
    '  .Columns("Proveedor").ReadOnly = True

    '  If UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" Or UsrTPM = "COMERCIAL" Then
    '   .Columns("Pro. Alterno").Visible = False
    '  End If
    '  .Columns("Pro. Alterno").HeaderText = "Proveedor alterno"
    '  .Columns("Pro. Alterno").Width = 100
    '  .Columns("Pro. Alterno").ReadOnly = True

    '  .Columns("Solicitado").HeaderText = "Pedido del cliente"
    '  .Columns("Solicitado").Width = 55
    '  .Columns("Solicitado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns("Solicitado").DefaultCellStyle.Format = "###,###,###"
    '  .Columns("Solicitado").ReadOnly = True

    '  .Columns("PendienteFacturar").HeaderText = "Pendiente Facturar"
    '  .Columns("PendienteFacturar").Width = 55
    '  .Columns("PendienteFacturar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns("PendienteFacturar").DefaultCellStyle.Format = "###,###,###"
    '  .Columns("PendienteFacturar").ReadOnly = True

    '  .Columns("Existencia").HeaderText = "Stock Actual"
    '  .Columns("Existencia").Width = 55
    '  .Columns("Existencia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns("Existencia").DefaultCellStyle.Format = "###,###,###"
    '  .Columns("Existencia").ReadOnly = True

    '  .Columns("Solicitar").HeaderText = "Piezas faltantes"
    '  .Columns("Solicitar").Width = 55
    '  .Columns("Solicitar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns("Solicitar").DefaultCellStyle.Format = "###,###,###"
    '  .Columns("Solicitar").ReadOnly = True

    '  .Columns("Request").HeaderText = "A solicitar"
    '  .Columns("Request").Width = 45
    '  .Columns("Request").ReadOnly = False

    '  .Columns("Almacen").HeaderText = "Almacen"
    '  .Columns("Almacen").Width = 60
    '  .Columns("Almacen").ReadOnly = False

    '  .Columns("Confirmado").HeaderText = "Confirmado"
    '  .Columns("Confirmado").Width = 60
    '  .Columns("Confirmado").ReadOnly = True
    '  .Columns("Confirmado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    '  'recorre las filas del DataGrid
    '  Dim numfilas As Integer
    '  numfilas = .RowCount 'cuenta las filas del DataGrid
    '  Dim valorConfirmado As Integer
    '  If numfilas > 0 Then
    '   For i = 0 To (numfilas - 1)
    '    If DGVEncOrdVta.Item(14, i).Value Is DBNull.Value Or DGVEncOrdVta.Item(14, i).Value = 0 Then
    '     DGVEncOrdVta.Item(14, i).Value = 0
    '     valorConfirmado = 0
    '    Else
    '     valorConfirmado = CInt(DGVEncOrdVta.Item(14, i).Value)
    '    End If

    '    If valorConfirmado > 0 Then
    '     DGVEncOrdVta.Rows(i).Cells(14).Style.BackColor = Color.GreenYellow
    '    End If
    '   Next
    '  End If

    '  .Columns("PrcProm").HeaderText = "$ Precio Promedio Vta"
    '  .Columns("PrcProm").Width = 60
    '  .Columns("PrcProm").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '  .Columns("PrcProm").DefaultCellStyle.Format = "###,###,###.00"
    '  .Columns("PrcProm").ReadOnly = True

    '  .Columns("MontVtaSol").ReadOnly = True
    '  .Columns("MontVtaSol").Visible = False

    '  .Columns("Solicitado2").HeaderText = "Solicitado Histórico."
    '  .Columns("Solicitado2").Width = 60
    '  .Columns("Solicitado2").ReadOnly = True

    '  .Columns("Pendiente").Visible = False
    '  .Columns("Solicitar1").Visible = False

    '  .Columns("Comments").Visible = False
    '  .Columns("Comments").ReadOnly = True

    '  .Columns("ordencompra").HeaderText = "Orde de compra"
    '  .Columns("ordencompra").Width = 60
    '  .Columns("ordencompra").Visible = True
    '  .Columns("ordencompra").ReadOnly = True


    '  btnEnviarOC.Visible = False
    '  Label13.Visible = False
    '  For i = 0 To (numfilas - 1)
    '   'Verifico si existe alguna solicitud, si es asi entonces habilito el el boton de envio
    '   If Not DGVEncOrdVta.Item(12, i).Value Is DBNull.Value And ExisteSolicitud = False Then
    '    If Trim(DGVEncOrdVta.Item(12, i).Value) > 0 Then
    '     ExisteSolicitud = True
    '    End If
    '   End If

    '   'Reviso si existe algun renglon con orde de compra
    '   If Not DGVEncOrdVta.Item(21, i).Value Is DBNull.Value Then
    '    If Trim(DGVEncOrdVta.Item(21, i).Value) <> "" Then
    '     btnEnviarOC.Visible = True
    '     Label13.Visible = True
    '    End If
    '   End If
    '  Next
    ' End With

    ' If ExisteSolicitud Then
    '  Button2.Visible = True
    '  Label9.Visible = True
    ' End If

    ' 'MsgBox("nombre es " & DGVEncOrdVta.Columns(14).Name)
    ' ReemplazarColumna()

    ' If UsrTPM <> "COMPRAS1" And UsrTPM <> "MANAGER" Then
    '  DGVEncOrdVta.Columns("Proveedor").Visible = False
    ' End If

    ' 'If UsrTPM = "COMPRAS1" Or UsrTPM = "MANAGER" Or UsrTPM = "SISTEMAS" Or UsrTPM = "ALMACEN1" Then
    ' '    DGVEncOrdVta.Columns(13).ReadOnly = False
    ' 'Else
    ' '    DGVEncOrdVta.Columns(13).ReadOnly = True
    ' 'End If

    ' With DGVDetOrdVta
    '  .DataSource = dvdet
    '  .ReadOnly = True
    '  'Color de Renglones en Grid
    '  .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
    '  .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    '  .DefaultCellStyle.BackColor = Color.AliceBlue
    '  .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
    '  'Propiedad para no mostrar el cuadro que se encuentra en la parte
    '  'Superior Izquierda del gridview
    '  .RowHeadersVisible = False
    '  .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    '  .MultiSelect = True
    '  .AllowUserToAddRows = False
    '  'Color de linea del grid
    '  .Columns(0).HeaderText = "Usuario Ventas"
    '  .Columns(0).Width = 90

    '  .Columns(1).HeaderText = "Fecha Creación"
    '  .Columns(1).Width = 70

    '  .Columns(2).HeaderText = "Orden de Vta"
    '  .Columns(2).Width = 45

    '  .Columns(3).HeaderText = "Clave Cliente"
    '  .Columns(3).Width = 50

    '  .Columns(4).HeaderText = "Nombre Cliente"
    '  .Columns(4).Width = 175

    '  .Columns(5).HeaderText = "Total del Pedido"
    '  .Columns(5).Width = 60
    '  .Columns(5).DefaultCellStyle.Format = "###,###,###.##"
    '  .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    '  .Columns(6).Visible = False

    '  .Columns(7).HeaderText = "Articulo"
    '  .Columns(7).Width = 100

    '  .Columns(8).HeaderText = "# Fabricante"
    '  .Columns(8).Width = 100

    '  .Columns(9).HeaderText = "Descripción"
    '  .Columns(9).Width = 160

    '  .Columns(10).Visible = False

    '  .Columns(11).HeaderText = "Proveedor"
    '  .Columns(11).Width = 160
    '  .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns(11).DefaultCellStyle.Format = "###,###,###"

    '  .Columns(12).HeaderText = "Pro. Alterno"
    '  .Columns(12).Width = 160
    '  .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns(12).DefaultCellStyle.Format = "###,###,###"

    '  .Columns(13).HeaderText = "Pedido del Cliente"
    '  .Columns(13).Width = 55
    '  .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns(13).DefaultCellStyle.Format = "###,###,###"

    '  .Columns(14).HeaderText = "Pendiente por Facturar"
    '  .Columns(14).Width = 55
    '  .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns(14).DefaultCellStyle.Format = "###,###,###"

    '  .Columns(15).HeaderText = "Stock Actual"
    '  .Columns(15).Width = 55
    '  .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns(15).DefaultCellStyle.Format = "###,###,###"

    '  .Columns(16).HeaderText = "Piezas Faltantes"
    '  .Columns(16).Width = 55
    '  .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns(16).DefaultCellStyle.Format = "###,###,###"

    '  .Columns(17).HeaderText = "A Solicitar"
    '  .Columns(17).Width = 55
    '  .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns(17).DefaultCellStyle.Format = "###,###,###"
    '  .Columns(17).ReadOnly = False
    '  .Columns(17).Visible = False

    '  .Columns(18).HeaderText = "Almacen"
    '  .Columns(18).Width = 55
    '  .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    '  .Columns(18).DefaultCellStyle.Format = "###,###,###"
    '  .Columns(18).ReadOnly = False
    '  .Columns(18).Visible = False

    '  .Columns(19).HeaderText = "$ Precio Vta"
    '  .Columns(19).Width = 60
    '  .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '  .Columns(19).DefaultCellStyle.Format = "###,###,###.00"

    '  .Columns(20).HeaderText = "$ Monto Vta Pzas Faltantes"
    '  .Columns(20).Width = 70
    '  .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '  .Columns(20).DefaultCellStyle.Format = "###,###,###.00"

    '  '.Columns(16).Visible = False
    '  '.Columns(17).Visible = False
    '  '.Columns(18).Visible = False
    ' End With

    ' If UsrTPM <> "COMPRAS1" And UsrTPM <> "MANAGER" Then
    '  DGVDetOrdVta.Columns("Proveedor").Visible = False
    ' End If

    ' 'For Each row As DataGridViewRow In Me.DGVEncOrdVta.Rows
    ' '    If row.Cells("Pzas Pendientes").Value <= 0 Then
    ' '        row.Cells("Pzas a Solicitar").Value = 0
    ' '    Else
    ' '        row.Cells("Pzas a Solicitar").Value = row.Cells("Pzas Pendientes").Value
    ' '    End If
    ' 'Next


    ' 'Esta parte hacia que los historicos fuerans 0
    ' 'For Each row As DataGridViewRow In Me.DGVEncOrdVta.Rows
    ' '  If Not row.Cells(16).Value Is DBNull.Value Then
    ' '    If row.Cells(16).Value <= 0 Or row.Cells(16) Is DBNull.Value Then
    ' '      row.Cells(17).Value = 0
    ' '    Else
    ' '      row.Cells(17).Value = row.Cells(17).Value
    ' '    End If
    ' '  Else
    ' '    row.Cells(17).Value = 0
    ' '  End If
    ' 'Next

    ' 'DGVEncOrdVta.Rows(12).DefaultCellStyle.Font = New Font(DGVEncOrdVta.DefaultCellStyle.Font, FontStyle.Bold)
    ' 'DGVEncOrdVta.Rows(8).DefaultCellStyle.Font = New Font(DGVEncOrdVta.Rows(8).DefaultCellStyle.Font, FontStyle.Bold)
    ' Try
    '  DGVEncOrdVta.CurrentCell = DGVEncOrdVta.Rows(1).Cells(0)
    '  DGVEncOrdVta.CurrentCell = DGVEncOrdVta.Rows(0).Cells(0)
    ' Catch
    ' End Try

    ' 'With conexion2
    ' '    If .State = ConnectionState.Open Then
    ' '        .Close()
    ' '    End If
    ' '    .Dispose()
    ' 'End With
    'End Sub

    Private Sub DGVEncOrdVta_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles DGVEncOrdVta.SelectionChanged
        BuscaOrdenes()
    End Sub

    Sub BuscaOrdenes()
        Try
            dvdet.RowFilter = "Filtro ='" & DGVEncOrdVta.Item(1, DGVEncOrdVta.CurrentRow.Index).Value.ToString & "'"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGVEncOrdVta_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVEncOrdVta.CellEndEdit
        ActualizaOC()
        'Try
        '    DGVEncOrdVta.CurrentRow.Cells(13).Value = Convert.ToDecimal(DGVEncOrdVta.CurrentRow.Cells(13).Value)
        'Catch
        'End Try
    End Sub

    Private Sub DGVEncOrdVta_RowPrePaint(sender As System.Object, e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DGVEncOrdVta.RowPrePaint
        DGVEncOrdVta.Rows(e.RowIndex).Cells(11).Style.BackColor = Color.Gainsboro
        DGVEncOrdVta.Rows(e.RowIndex).Cells(11).Style.ForeColor = Color.Red
        DGVEncOrdVta.Rows(e.RowIndex).Cells(11).Style.Font = New Font(DGVEncOrdVta.DefaultCellStyle.Font, FontStyle.Bold)
        'row.Cells("Nombre").Style.Font = New Font(.DefaultCellStyle.Font, FontStyle.Bold)
    End Sub

    Private Sub ReemplazarColumna()
        Dim instance As DataGridViewTextBoxColumn
        instance = DGVEncOrdVta.Columns(15)
        instance.MaxInputLength = 4
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Public Sub ExportarDatosExcel_Sol(ByVal DataGridView1 As DataGridView, ByVal descripcion_tabla As String)
        Try
            Dim m_Excel As New Microsoft.Office.Interop.Excel.Application
            m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlWait
            m_Excel.Visible = True
            Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook = m_Excel.Workbooks.Add
            Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet = objLibroExcel.Worksheets(1)
            With objHojaExcel
                .Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
                .Activate()
                'Encabezado  
                .Range("A1:L1").Merge()
                .Range("A1:L1").Value = "SOLICITUD DE ARTICULOS"
                .Range("A1:L1").Font.Bold = True
                .Range("A1:L1").Font.Size = 17
                'Copete  
                .Range("A2:L2").Merge()
                .Range("A2:L2").Value = descripcion_tabla
                .Range("A2:L2").Font.Bold = True
                .Range("A2:L2").Font.Size = 15

                Const primeraLetra As Char = "A"
                Const primerNumero As Short = 3
                Dim Letra As Char, UltimaLetra As Char
                Dim Numero As Integer, UltimoNumero As Integer
                Dim cod_letra As Byte = Asc(primeraLetra) - 1
                Dim sepDec As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
                Dim sepMil As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator
                'Establecer formatos de las columnas de la hija de cálculo  
                Dim strColumna As String = ""
                Dim LetraIzq As String = ""
                Dim cod_LetraIzq As Byte = Asc(primeraLetra) - 1
                Letra = primeraLetra
                Numero = primerNumero
                Dim objCelda As Microsoft.Office.Interop.Excel.Range
                For Each c As DataGridViewColumn In DataGridView1.Columns
                    If c.Name.ToString = "Borrar" Then
                        Continue For
                    End If
                    If c.Visible Then
                        If Letra = "Z" Then
                            Letra = primeraLetra
                            cod_letra = Asc(primeraLetra)
                            cod_LetraIzq += 1
                            LetraIzq = Chr(cod_LetraIzq)
                        Else
                            cod_letra += 1
                            Letra = Chr(cod_letra)
                        End If
                        strColumna = LetraIzq + Letra + Numero.ToString
                        objCelda = .Range(strColumna, Type.Missing)
                        objCelda.Value = c.HeaderText
                        objCelda.EntireColumn.Font.Size = 11
                        objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format
                        'If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
                        '    objCelda.EntireColumn.NumberFormat = "#" + sepMil + "0" + sepDec + "00"
                        'End If
                    End If
                Next

                Dim objRangoEncab As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
                objRangoEncab.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
                UltimaLetra = Letra
                Dim UltimaLetraIzq As String = LetraIzq

                'CARGA DE DATOS  
                Dim i As Integer = Numero + 1

                For Each reg As DataGridViewRow In DataGridView1.Rows
                    LetraIzq = ""
                    cod_LetraIzq = Asc(primeraLetra) - 1
                    Letra = primeraLetra
                    cod_letra = Asc(primeraLetra) - 1
                    For Each c As DataGridViewColumn In DataGridView1.Columns
                        If c.Name.ToString = "Borrar" Then
                            Continue For
                        End If
                        If c.Visible Then
                            If Letra = "Z" Then
                                Letra = primeraLetra
                                cod_letra = Asc(primeraLetra)
                                cod_LetraIzq += 1
                                LetraIzq = Chr(cod_LetraIzq)
                            Else
                                cod_letra += 1
                                Letra = Chr(cod_letra)
                            End If
                            strColumna = LetraIzq + Letra
                            ' acá debería realizarse la carga  
                            If c.Name.ToString = "validado" Then
                                .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", (IIf(reg.Cells(c.Index).Value = "1", "SI", "NO")))
                            Else
                                .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value)
                            End If
                            '.Cells(i, strColumna) = IIf(IsDBNull(reg.(c.DataPropertyName)), c.DefaultCellStyle.NullValue, reg(c.DataPropertyName))
                            '.Range(strColumna + i, strColumna + i).In()
                        End If
                    Next
                    Dim objRangoReg As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + i.ToString, strColumna + i.ToString)
                    objRangoReg.Rows.BorderAround()
                    objRangoReg.Select()
                    i += 1
                Next
                UltimoNumero = i

                'Dibujar las líneas de las columnas  
                LetraIzq = ""
                cod_LetraIzq = Asc("A")
                cod_letra = Asc(primeraLetra)
                Letra = primeraLetra
                For Each c As DataGridViewColumn In DataGridView1.Columns
                    If c.Name.ToString = "Borrar" Then
                        Continue For
                    End If
                    If c.Visible Then
                        objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
                        objCelda.BorderAround()
                        If Letra = "Z" Then
                            Letra = primeraLetra
                            cod_letra = Asc(primeraLetra)
                            LetraIzq = Chr(cod_LetraIzq)
                            cod_LetraIzq += 1
                        Else
                            cod_letra += 1
                            Letra = Chr(cod_letra)
                        End If
                    End If
                Next

                'Dibujar el border exterior grueso  
                Dim objRango As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
                objRango.Select()

                'If (objRango.Columns.Name.ToString <> "Descripción") Then
                '    objRango.Columns.AutoFit()
                '    objRango.Columns.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
                'End If
                objRango.Columns.AutoFit()
                objRango.Columns.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
                objHojaExcel.Range("C:C").ColumnWidth = 50
                objHojaExcel.Range("E:E").ColumnWidth = 40
                objHojaExcel.Range("I:I").Font.Color = Color.Red
            End With

            objHojaExcel.Rows.Item(3).Font.Bold = 1
            For ee As Integer = 1 To (DataGridView1.Rows.Count + 3)
                objHojaExcel.Rows.Item(ee).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter

            Next
            m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Private Sub mGeneraExcel()
        Try
            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

            'Añadimos el Libro al programa
            exLibro = exApp.Workbooks.Add

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DGVAux.ColumnCount
            Dim NRow As Integer = DGVAux.RowCount


            'MsgBox(NRow)

            ''Combinamos celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1:g1").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A2:g2").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A3:g3").Merge(True)
            'exLibro.Worksheets("Hoja1").Cells.Range("A4:g4").Merge(True)

            ''aplicamos un color de fondo ala celda o rango de celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15

            '************
            'exLibro.Worksheets("Hoja1").Columns("E").NumberFormat = "@" 'Articulo'
            exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 15 'Factura'
            exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 9 'Factura'
            exLibro.Worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 5 'Fecha'
            exLibro.Worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 6 'Fecha cont'
            exLibro.Worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 15 'Articulo'
            exLibro.Worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 9 'Descripcion'
            exLibro.Worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 12 'Linea'
            exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 8 'Cantidad'
            exLibro.Worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 6 'Comentarios'
            exLibro.Worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 6 'Proveedor'
            exLibro.Worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 6 'Codigo Proveedor'

            '************

            ''Cambiamos orientacion ala hola
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Solicitud de Articulos por Vendedor"
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Del: " + DtpFechaIni.Value
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Al: " + DtpFechaTer.Value

            exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1
            Dim i_aux As Integer = 1

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                If DGVAux.Columns(i - 1).Visible = True Then
                    exLibro.Worksheets("Hoja1").Cells.Item(6, i_aux) = DGVAux.Columns(i - 1).HeaderText.ToString
                    'exLibro.Worksheets("Hoja1").Cells.Item(6, i_aux).Font.Size = 9
                    i_aux = i_aux + 1
                End If
            Next


            For Fila As Integer = 0 To NRow - 1
                i_aux = 1
                For Col As Integer = 0 To NCol - 1
                    If DGVAux.Columns(Col).Visible = True Then
                        exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, i_aux) = DGVAux.Rows(Fila).Cells(Col).Value
                        exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, i_aux).BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin)
                        'exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, i_aux).interior.color = Color.LightGreen
                        'exLibro.Worksheets("Hoja1").Cells.Item(Fila + 7, i_aux).Font.Size = 9
                        i_aux = i_aux + 1
                    End If



                Next

                If DGVAux.Rows(Fila).Cells("Color").Value.ToString = "Color.LightSkyBlue" Then
                    exLibro.Worksheets("Hoja1").Cells.Range("A" & (Fila + 7).ToString & ":L" & (Fila + 7).ToString).interior.color = Color.LightSkyBlue
                ElseIf DGVAux.Rows(Fila).Cells("Color").Value.ToString = "Color.LightPink" Then
                    exLibro.Worksheets("Hoja1").Cells.Range("A" & (Fila + 7).ToString & ":L" & (Fila + 7).ToString).interior.color = Color.LightPink
                Else
                    exLibro.Worksheets("Hoja1").Cells.Range("A" & (Fila + 7).ToString & ":L" & (Fila + 7).ToString).interior.color = Color.LightGreen
                End If
            Next
            exLibro.Worksheets("Hoja1").Cells.Range("A6:L6").WrapText = True
            exLibro.Worksheets("Hoja1").Rows.Item(6).VerticalAlignment = 2
            exLibro.Worksheets("Hoja1").Cells.Range("A6:L" & (NRow + 6).ToString).Font.Size = 9
            exLibro.Worksheets("Hoja1").Cells.Range("L7:L" & (NRow + 6).ToString).Font.Color = Color.Red
            exLibro.Worksheets("Hoja1").Cells.Range("L7:L" & (NRow + 6).ToString).Font.Bold = 1



            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exLibro.Worksheets("Hoja1").Rows.Item(6).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Rows.Item(6).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Rows.Item(6).Interior.ColorIndex = 15
            'exLibro.Worksheets("Hoja1").Columns.AutoFit()
            exLibro.Worksheets("Hoja1").name = "Solicitud de Articulos"

            'Aplicación visible
            exLibro.Worksheets.Application.Visible = True

            exLibro = Nothing
            exApp = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub mGeneraExcel_articulos()
        Try
            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

            'Añadimos el Libro al programa
            exLibro = exApp.Workbooks.Add

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = DGVEncOrdVta.ColumnCount
            Dim NRow As Integer = DGVEncOrdVta.RowCount

            ''Combinamos celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1:g1").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A2:g2").Merge(True)
            exLibro.Worksheets("Hoja1").Cells.Range("A3:g3").Merge(True)

            ''aplicamos un color de fondo ala celda o rango de celdas
            exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15

            '************
            exLibro.Worksheets("Hoja1").Columns("J").NumberFormat = "###,###,###" 'PIEZAS FALTANTES'
            exLibro.Worksheets("Hoja1").Columns("N").NumberFormat = "###,###,###"
            exLibro.Worksheets("Hoja1").Columns("N").NumberFormat = "$ ###,##0.00" 'Articulo'
            exLibro.Worksheets("Hoja1").Columns("O").NumberFormat = "$ ###,##0.00" 'Articulo'

            exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 3 'Factura'
            exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 10 'Factura'
            exLibro.Worksheets("Hoja1").Columns("B").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
            exLibro.Worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 12 'Fecha'
            exLibro.Worksheets("Hoja1").Columns("C").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
            exLibro.Worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 9 'Fecha cont'
            exLibro.Worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 12 'Articulo'
            exLibro.Worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 7 'Descripcion'
            exLibro.Worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 7 'Linea'
            exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 6 'Cantidad'
            exLibro.Worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 6 'Comentarios'
            exLibro.Worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 9 'Proveedor'
            exLibro.Worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 9 'Codigo Proveedor'
            exLibro.Worksheets("Hoja1").Columns("L").EntireColumn.ColumnWidth = 9 'Codigo Proveedor'


            '************

            ''Cambiamos orientacion ala hola
            exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Solicitud de Articulos"
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Del: " + DtpFechaIni.Value
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1) = "Al: " + DtpFechaTer.Value

            exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Cells.Item(3, 1).Font.Bold = 1
            Dim i_aux As Integer = 1

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                If DGVEncOrdVta.Columns(i - 1).Visible = True Then
                    exLibro.Worksheets("Hoja1").Cells.Item(5, i_aux) = DGVEncOrdVta.Columns(i - 1).HeaderText.ToString
                    i_aux = i_aux + 1
                End If
            Next


            For Fila As Integer = 0 To NRow - 1
                i_aux = 1
                For Col As Integer = 0 To NCol - 1
                    If DGVEncOrdVta.Columns(Col).Visible = True Then
                        exLibro.Worksheets("Hoja1").Cells.Item(Fila + 6, i_aux) = DGVEncOrdVta.Rows(Fila).Cells(Col).Value
                        i_aux = i_aux + 1
                    End If
                Next
            Next
            exLibro.Worksheets("Hoja1").Cells.Range("A5:P5").WrapText = True
            exLibro.Worksheets("Hoja1").Rows.Item(5).VerticalAlignment = 2
            exLibro.Worksheets("Hoja1").Cells.Range("A5:P" & (NRow + 5).ToString).Font.Size = 9
            exLibro.Worksheets("Hoja1").Cells.Range("J6:J" & (NRow + 5).ToString).Font.Color = Color.Red
            exLibro.Worksheets("Hoja1").Cells.Range("J6:J" & (NRow + 5).ToString).Font.Bold = 1
            '.Font.Color = Color.Red

            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exLibro.Worksheets("Hoja1").Rows.Item(5).Font.Bold = 1
            exLibro.Worksheets("Hoja1").Rows.Item(5).HorizontalAlignment = 3
            exLibro.Worksheets("Hoja1").Rows.Item(5).Interior.ColorIndex = 15
            exLibro.Worksheets("Hoja1").name = "Solicitud de Articulos"

            'Aplicación visible
            exLibro.Worksheets.Application.Visible = True

            exLibro = Nothing
            exApp = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    '-------------------------------------------------------------------------------------------------------------
    Dim strFileName As String = ""

    Sub ExportarEnviar()
        SQL.conectarTPM()
        Dim wb As New XLWorkbook()
        Dim ws = wb.Worksheets.Add("Solicitud de artículos")
        Dim index As Integer = 6

        Dim idMax As String = SQL.CampoEspecifico("select MAX(id) + 1 as 'id' from TPDSolicitudArticulos", "id")
        If idMax = "" Or IsDBNull(idMax) Then
            idMax = 1
        End If
        'DETALLE DEL REPORTE
        ws.Cell("A1").Value = "Solicitud de Articulos"
        ws.Range("A1:C1").Merge()
        ws.Cell("A1").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A1").Style.Font.Bold = True
        ws.Cell("A2").Value = "Del: " + DtpFechaIni.Value
        ws.Range("A2:C2").Merge()
        ws.Cell("A2").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A2").Style.Font.Bold = True
        ws.Cell("A3").Value = "Al: " + DtpFechaTer.Value
        ws.Range("A3:C3").Merge()
        ws.Cell("A3").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A3").Style.Font.Bold = True

        'ENCABEZADO DEL REPORTE
        ws.Cell("A5").Value = "#"
        ws.Cell("A5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A5").Style.Font.Bold = True
        ws.Cell("B5").Value = "Articulo"
        ws.Cell("B5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("B5").Style.Font.Bold = True
        ws.Cell("C5").Value = "# Fabricante"
        ws.Cell("C5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("C5").Style.Font.Bold = True
        ws.Cell("D5").Value = "Descripción"
        ws.Cell("D5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("D5").Style.Font.Bold = True
        ws.Cell("E5").Value = "Línea"
        ws.Cell("E5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("E5").Style.Font.Bold = True
        ws.Cell("F5").Value = "Piezas Faltantes"
        ws.Cell("F5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("F5").Style.Font.Bold = True
        ws.Cell("G5").Value = "A Solicitar"
        ws.Cell("G5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("G5").Style.Font.Bold = True
        ws.Cell("H5").Value = "Almacen"
        ws.Cell("H5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("H5").Style.Font.Bold = True
        ws.Cell("I5").Value = "Confirmado"
        ws.Cell("I5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("I5").Style.Font.Bold = True
        ws.Cell("J5").Value = "O.C."
        ws.Cell("J5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("J5").Style.Font.Bold = True

        For Each fila As DataGridViewRow In DGVEncOrdVta.Rows
            If (fila.Cells(12).Value.ToString() <> "" Or IsDBNull(fila.Cells(12).Value.ToString())) And fila.Cells(12).Value.ToString() > 0 Then
                ws.Cell("A" + index.ToString).Value = fila.Cells(0).Value.ToString()
                ws.Cell("B" + index.ToString).Value = fila.Cells(2).Value.ToString()
                ws.Cell("C" + index.ToString).Value = fila.Cells(3).Value.ToString()
                ws.Cell("D" + index.ToString).Value = fila.Cells(4).Value.ToString()
                ws.Cell("E" + index.ToString).Value = fila.Cells(5).Value.ToString()

                ws.Cell("F" + index.ToString).Value = fila.Cells(11).Value.ToString()
                ws.Cell("F" + index.ToString).Style.Font.FontColor = XLColor.Red
                ws.Cell("G" + index.ToString).Value = fila.Cells(12).Value.ToString()
                ws.Cell("H" + index.ToString).Value = fila.Cells(13).Value.ToString()
                ws.Cell("I" + index.ToString).Value = fila.Cells(14).Value.ToString()


                index = index + 1
            End If

            'INSERTAR DATOS EL TABLA TPDSolicitudArticulos PARA LLEVAR UN CONTROL
            If fila.Cells(2).Value.ToString() <> "" And fila.Cells(12).Value.ToString() > 0 Then
                If SQL.SiExiste("SELECT * FROM TPDSolicitudArticulos where articulo = '" + fila.Cells(2).Value.ToString() + "' and CAST(fechapedido as date) = CAST(GETDATE() as date)") <> True Then

                    'Busco las ordenes de venta del articulo
                    OrdenVentaRelacionada = ""
                    For Each item As DataRow In dvdet.Table.Rows
                        If (item.Item("Filtro").Equals(fila.Cells(2).Value.ToString())) Then
                            If (OrdenVentaRelacionada.Trim.Equals("")) Then
                                OrdenVentaRelacionada = item.Item("OrdVta")
                            Else
                                OrdenVentaRelacionada = OrdenVentaRelacionada & "," & item.Item("OrdVta")
                            End If
                        End If
                    Next

                    Dim Qry As String = "INSERT INTO TPDSolicitudArticulos (id,articulo,pedidocliente,pendientefacturar,stockactual,piezasfaltantes,asolicitar,almacen,montofaltantes,solicitado,fechapedido, OrdenVentaRelacionada, Bloquear_Surtido) VALUES " +
                                    "(" + idMax + ",'" + fila.Cells(2).Value.ToString() + "'," + fila.Cells(8).Value.ToString() + "," + fila.Cells(9).Value.ToString() + "," + fila.Cells(10).Value.ToString() + "," +
                                    fila.Cells(11).Value.ToString() + "," + If(fila.Cells(12).Value.ToString() = "", "NULL", fila.Cells(12).Value.ToString()) + ",'" + fila.Cells(13).Value.ToString() + "'," + fila.Cells(15).Value.ToString() + "," + fila.Cells(16).Value.ToString() + ",GETDATE(), '" + OrdenVentaRelacionada + "', 'SI')"
                    If SQL.EjecutarCRUD(Qry) Then

                    Else
                        MessageBox.Show("Se produjo un error al insertar los datos", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If

        Next

        'FORMATOS
        'ws.Columns("J").Style.NumberFormat.Format = "#,##0"
        'ws.Columns("N").Style.NumberFormat.Format = "#,##0"
        'ws.Columns("L").Style.NumberFormat.Format = "$ ###,##0.00"
        'ws.Columns("M").Style.NumberFormat.Format = "$ ###,##0.00"

        'Dim strFileName As String = ""
        Try
            'Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Filter = "Excel|*.xlsx"
            saveFileDialog1.Title = "Save Excel File"
            saveFileDialog1.FileName = "Solicitud de articulos de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"
            saveFileDialog1.ShowDialog()
            saveFileDialog1.InitialDirectory = "C:/"

            If saveFileDialog1.FileName <> "" Then
                Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
                fs.Close()
            End If

            strFileName = saveFileDialog1.FileName
            wb.SaveAs(strFileName)
            'EnvioMail(strFileName)


            'If MessageBox.Show("¿Desea enviar el archivo por correo electronico?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '    Dim enviar As New frmEnviarCorreo()
            '    enviar.rutaArchivo = strFileName
            '    enviar.ShowDialog()
            'End If
            'Process.Start(saveFileDialog1.FileName)
            SQL.Cerrar()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error al guardar el archivo")
        End Try
    End Sub

    Private Function CreaArchivo() As String
        SQL.conectarTPM()
        Dim wb As New XLWorkbook()
        Dim ws = wb.Worksheets.Add("Solicitud de artículos")
        Dim index As Integer = 6

        Dim idMax As String = SQL.CampoEspecifico("select MAX(id) + 1 as 'id' from TPDSolicitudArticulos", "id")
        If idMax = "" Or IsDBNull(idMax) Then
            idMax = 1
        End If
        'DETALLE DEL REPORTE
        ws.Cell("A1").Value = "Solicitud de Articulos"
        ws.Range("A1:C1").Merge()
        ws.Cell("A1").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A1").Style.Font.Bold = True
        ws.Cell("A2").Value = "Del: " + DtpFechaIni.Value
        ws.Range("A2:C2").Merge()
        ws.Cell("A2").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A2").Style.Font.Bold = True
        ws.Cell("A3").Value = "Al: " + DtpFechaTer.Value
        ws.Range("A3:C3").Merge()
        ws.Cell("A3").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A3").Style.Font.Bold = True

        'ENCABEZADO DEL REPORTE
        ws.Cell("A5").Value = "#"
        ws.Cell("A5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A5").Style.Font.Bold = True
        ws.Cell("B5").Value = "Articulo"
        ws.Cell("B5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("B5").Style.Font.Bold = True
        ws.Cell("C5").Value = "# Fabricante"
        ws.Cell("C5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("C5").Style.Font.Bold = True
        ws.Cell("D5").Value = "Descripción"
        ws.Cell("D5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("D5").Style.Font.Bold = True
        ws.Cell("E5").Value = "Línea"
        ws.Cell("E5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("E5").Style.Font.Bold = True
        ws.Cell("F5").Value = "Piezas Faltantes"
        ws.Cell("F5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("F5").Style.Font.Bold = True
        ws.Cell("G5").Value = "A Solicitar"
        ws.Cell("G5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("G5").Style.Font.Bold = True
        ws.Cell("H5").Value = "Almacen"
        ws.Cell("H5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("H5").Style.Font.Bold = True
        ws.Cell("I5").Value = "Confirmado"
        ws.Cell("I5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("I5").Style.Font.Bold = True
        ws.Cell("J5").Value = "Orden de compra"
        ws.Cell("J5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("J5").Style.Font.Bold = True

        For Each fila As DataGridViewRow In DGVEncOrdVta.Rows
            If fila.Cells(12).Value.ToString() <> "" Or IsDBNull(fila.Cells(12).Value.ToString()) Then
                ws.Cell("A" + index.ToString).Value = fila.Cells(0).Value.ToString()
                ws.Cell("B" + index.ToString).Value = fila.Cells(2).Value.ToString()
                ws.Cell("C" + index.ToString).Value = fila.Cells(3).Value.ToString()
                ws.Cell("D" + index.ToString).Value = fila.Cells(4).Value.ToString()
                ws.Cell("E" + index.ToString).Value = fila.Cells(5).Value.ToString()

                ws.Cell("F" + index.ToString).Value = fila.Cells(11).Value.ToString()
                ws.Cell("F" + index.ToString).Style.Font.FontColor = XLColor.Red
                ws.Cell("G" + index.ToString).Value = fila.Cells(12).Value.ToString()
                ws.Cell("H" + index.ToString).Value = fila.Cells(13).Value.ToString()
                ws.Cell("I" + index.ToString).Value = fila.Cells(14).Value.ToString()
                ws.Cell("J" + index.ToString).Value = fila.Cells(21).Value.ToString()

                index = index + 1
            End If

            'INSERTAR DATOS EL TABLA TPDSolicitudArticulos PARA LLEVAR UN CONTROL
            If fila.Cells(2).Value.ToString() <> "" Then
                If SQL.SiExiste("SELECT * FROM TPDSolicitudArticulos where articulo = '" + fila.Cells(2).Value.ToString() + "' and CAST(fechapedido as date) = CAST(GETDATE() as date)") <> True Then

                    'Busco las ordenes de venta del articulo
                    OrdenVentaRelacionada = ""
                    For Each item As DataRow In dvdet.Table.Rows
                        If (item.Item("Filtro").Equals(fila.Cells(2).Value.ToString())) Then
                            If (OrdenVentaRelacionada.Trim.Equals("")) Then
                                OrdenVentaRelacionada = item.Item("OrdVta")
                            Else
                                OrdenVentaRelacionada = OrdenVentaRelacionada & "," & item.Item("OrdVta")
                            End If
                        End If
                    Next


                    If SQL.EjecutarCRUD("INSERT INTO TPDSolicitudArticulos (id,articulo,pedidocliente,pendientefacturar,stockactual,piezasfaltantes,asolicitar,almacen,montofaltantes,solicitado,fechapedido, OrdenVentaRelacionada, Bloquear_Surtido) VALUES " +
                                    "(" + idMax + ",'" + fila.Cells(2).Value.ToString() + "'," + fila.Cells(8).Value.ToString() + "," + fila.Cells(9).Value.ToString() + "," + fila.Cells(10).Value.ToString() + "," +
                                    fila.Cells(11).Value.ToString() + "," + If(fila.Cells(12).Value.ToString() = "", "NULL", fila.Cells(12).Value.ToString()) + ",'" + fila.Cells(13).Value.ToString() + "'," + fila.Cells(15).Value.ToString() + "," + fila.Cells(16).Value.ToString() + ",GETDATE(), '" + OrdenVentaRelacionada + "', 'SI')") Then

                        Return ""

                    Else
                        MessageBox.Show("Se produjo un error al insertar los datos", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        Next

        Try
            saveFileDialog1.Filter = "Excel|*.xlsx"
            saveFileDialog1.Title = "Save Excel File"
            saveFileDialog1.FileName = Application.StartupPath & "\" & "Solicitud de articulos de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"

            If saveFileDialog1.FileName <> "" Then
                Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
                fs.Close()
            End If

            strFileName = saveFileDialog1.FileName
            wb.SaveAs(strFileName)
            SQL.Cerrar()
            Return strFileName
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error al guardar el archivo")
        End Try
    End Function


    Private Function CreaArchivoOC() As String
        SQL.conectarTPM()
        Dim wb As New XLWorkbook()
        Dim ws = wb.Worksheets.Add("Envío de Ordenes de compra")
        Dim index As Integer = 6

        'DETALLE DEL REPORTE
        ws.Cell("A1").Value = "Envío de Ordenes de compra"
        ws.Range("A1:C1").Merge()
        ws.Cell("A1").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A1").Style.Font.Bold = True
        ws.Cell("A2").Value = "Del: " + DtpFechaIni.Value
        ws.Range("A2:C2").Merge()
        ws.Cell("A2").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A2").Style.Font.Bold = True
        ws.Cell("A3").Value = "Al: " + DtpFechaTer.Value
        ws.Range("A3:C3").Merge()
        ws.Cell("A3").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A3").Style.Font.Bold = True

        'ENCABEZADO DEL REPORTE
        ws.Cell("A5").Value = "#"
        ws.Cell("A5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("A5").Style.Font.Bold = True
        ws.Cell("B5").Value = "Articulo"
        ws.Cell("B5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("B5").Style.Font.Bold = True
        ws.Cell("C5").Value = "# Fabricante"
        ws.Cell("C5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("C5").Style.Font.Bold = True
        ws.Cell("D5").Value = "Descripción"
        ws.Cell("D5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("D5").Style.Font.Bold = True
        ws.Cell("E5").Value = "Línea"
        ws.Cell("E5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("E5").Style.Font.Bold = True
        ws.Cell("F5").Value = "Piezas Faltantes"
        ws.Cell("F5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("F5").Style.Font.Bold = True
        ws.Cell("G5").Value = "A Solicitar"
        ws.Cell("G5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("G5").Style.Font.Bold = True
        ws.Cell("H5").Value = "Almacen"
        ws.Cell("H5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("H5").Style.Font.Bold = True
        ws.Cell("I5").Value = "Confirmado"
        ws.Cell("I5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("I5").Style.Font.Bold = True
        ws.Cell("J5").Value = "Orden de compra"
        ws.Cell("J5").Style.Fill.BackgroundColor = XLColor.DarkGray
        ws.Cell("J5").Style.Font.Bold = True

        For Each fila As DataGridViewRow In DGVEncOrdVta.Rows
            If fila.Cells(21).Value.ToString() <> "" Or IsDBNull(fila.Cells(21).Value.ToString()) Then
                ws.Cell("A" + index.ToString).Value = fila.Cells(0).Value.ToString()
                ws.Cell("B" + index.ToString).Value = fila.Cells(2).Value.ToString()
                ws.Cell("C" + index.ToString).Value = fila.Cells(3).Value.ToString()
                ws.Cell("D" + index.ToString).Value = fila.Cells(4).Value.ToString()
                ws.Cell("E" + index.ToString).Value = fila.Cells(5).Value.ToString()

                ws.Cell("F" + index.ToString).Value = fila.Cells(11).Value.ToString()
                ws.Cell("F" + index.ToString).Style.Font.FontColor = XLColor.Red
                ws.Cell("G" + index.ToString).Value = fila.Cells(12).Value.ToString()
                ws.Cell("H" + index.ToString).Value = fila.Cells(13).Value.ToString()
                ws.Cell("I" + index.ToString).Value = fila.Cells(14).Value.ToString()
                ws.Cell("J" + index.ToString).Value = fila.Cells(21).Value.ToString()

                index = index + 1
            End If
        Next

        Try
            saveFileDialog1.Filter = "Excel|*.xlsx"
            saveFileDialog1.Title = "Save Excel File"
            saveFileDialog1.FileName = Application.StartupPath & "\" & "Ordenes de compra de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"

            If saveFileDialog1.FileName <> "" Then
                Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
                fs.Close()
            End If

            wb.SaveAs(saveFileDialog1.FileName)
            SQL.Cerrar()
            Return saveFileDialog1.FileName
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error al guardar el archivo")
        End Try
    End Function

    Public Sub EnvioMail(RutaArchivo As String)
        Try
            Dim correo As New MailMessage
            Dim smtp As New SmtpClient()

            correo.From = New MailAddress("desarrollo.ti@tractopartesdiamante.com.mx", "Iván González", Encoding.UTF8)
            correo.To.Add("soporte.ti@tractopartesdiamante.com.mx")
            correo.CC.Add("desarrollo.ti@tractopartesdiamante.com.mx")
            correo.SubjectEncoding = Encoding.UTF8
            correo.Subject = "Solicitud de articulos Tracto Partes Diamante"
            Dim archivoAdjunto As New Attachment(RutaArchivo)
            correo.Attachments.Add(archivoAdjunto)
            correo.Body = "Esto es una prueba con copia"
            correo.BodyEncoding = Encoding.UTF8
            correo.IsBodyHtml = False
            correo.Priority = MailPriority.High

            smtp.UseDefaultCredentials = True
            smtp.Credentials = New NetworkCredential("desarrollo.ti@tractopartesdiamante.com.mx", "DtiTr@cto2012")
            smtp.Port = 587
            smtp.Host = "servidor3315.tl.controladordns.com"
            smtp.EnableSsl = True

            smtp.Send(correo)
            MessageBox.Show("Se envio correctamente el correo", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Error al enviar el correo")
        End Try
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub



    'Funcion para importar archivo de excel y registrar en la tabla TPDSolicitudArticulos la informacion correspondiente
    Public Sub ImportarExcel()
        Dim Correctos As Integer = 0
        Try
            SQL.conectarTPM()
            Dim filePath As String

            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                filePath = OpenFileDialog1.FileName
            End If
            If IsNothing(filePath) Or Trim(filePath) = "" Then
                Exit Sub
            End If
            Using workBook = New XLWorkbook(filePath)
                'Read the first Sheet from Excel file.
                Dim workSheet As IXLWorksheet = workBook.Worksheet(1)
                Dim FechaInicio As Date
                Dim FechaFin As Date
                Dim id As Integer = 0
                Dim Articulo As String = "0"
                Dim Fabricante As String = "0"
                Dim Descripcion As String = "0"
                Dim Linea As String = "0"
                Dim ASolicitar As Integer = 0
                Dim Almacen As String = "0"
                Dim Confirmado As Integer = 0
                Dim OrdenCompra As Integer
                Dim fecha As Date = DateTime.Now.ToString("yyyy/MM/dd")

                FechaInicio = DtpFechaIni.Value()
                FechaFin = DtpFechaTer.Value()

                'Create a new DataTable.
                'Dim dt As New DataTable()

                'Loop through the Worksheet rows.
                Dim firstRow As Boolean = False
                For Each row As IXLRow In workSheet.Rows()
                    'Use the first row to add columns to DataTable.
                    If firstRow = False Then
                        For Each cell As IXLCell In row.Cells()
                            If cell.Value.ToString() = "#" Then
                                firstRow = True
                            End If
                        Next
                    Else
                        'Add rows to DataTable.
                        'dt.Rows.Add()
                        Dim i As Integer = 0

                        For Each cell As IXLCell In row.Cells()
                            Select Case cell.Address.ColumnNumber
                                Case 2
                                    If Trim(cell.Value.ToString()) = "" Then
                                        'Salto al siguiente
                                        GoTo ContinuoSinActualizar
                                    End If
                                    Articulo = cell.Value.ToString()
                                Case 7
                                    ASolicitar = Integer.Parse(cell.Value.ToString())
                                Case 8
                                    Almacen = cell.Value.ToString()
                                Case 9
                                    Confirmado = Integer.Parse(cell.Value.ToString())
                                Case 10
                                    Dim tmp_OC As String = cell.Value.ToString().Trim()
                                    If tmp_OC.Trim.Equals("") Then
                                        OrdenCompra = 0
                                    Else
                                        OrdenCompra = Integer.Parse(tmp_OC)
                                    End If
                            End Select
                        Next

                        If Articulo <> "" And ASolicitar > 0 And Almacen <> "" And Confirmado > 0 Then
                            If SQL.SiExiste("select * from TPDSolicitudArticulos where articulo = '" + Articulo + "' AND Confirmado > 0 AND CAST(fechapedido as date) BETWEEN CAST('" & DtpFechaIni.Value.ToString("yyyy-MM-dd") & "' as date) AND CAST('" & DtpFechaTer.Value.ToString("yyyy-MM-dd") & "' as date)") Then
                                If MessageBox.Show("Al parecer ya se ha procesado este registro " & Chr(13) & " (Artículo:" & Articulo & " Solicitado: " & ASolicitar & " Confirmado:" & Confirmado & "), ¿Deseas actualizar los datos?", "Pregunta...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                                    'No atualizo informacion
                                    GoTo ContinuoSinActualizar
                                End If
                            End If
                            'Valido que en la OC indicada efectivamente exista el articulo y la cantidad confirmada
                            If SQL.SiExiste("SELECT T0.DocNum Existe FROM SBO_TPD.dbo.OPOR T0
                        INNER JOIN SBO_TPD.dbo.POR1 T1 ON T0.DocEntry = T1.DocEntry
                        WHERE DocNum = " & OrdenCompra.ToString() & "
                        AND T1.ItemCode = '" & Articulo & "' 
                        AND T1.Quantity = " & Confirmado.ToString()) Then
                                'Realizo actualizacion del confirmado y guardo OC
                                Dim Actzo As String = "UPDATE TPDSolicitudArticulos SET Confirmado = " & Confirmado & ",ordencompra = " & OrdenCompra & "  WHERE articulo = '" & Articulo & "' AND CAST(fechapedido as date) BETWEEN CAST('" & DtpFechaIni.Value.ToString("yyyy-MM-dd") & "' as date) AND CAST('" & DtpFechaTer.Value.ToString("yyyy-MM-dd") & "' as date)"
                                If Actualizo("UPDATE TPDSolicitudArticulos SET Confirmado = " & Confirmado & ",ordencompra = '" & OrdenCompra & "'  WHERE articulo = '" & Articulo & "' AND CAST(fechapedido as date) BETWEEN CAST('" & DtpFechaIni.Value.ToString("yyyy-MM-dd") & "' as date) AND CAST('" & DtpFechaTer.Value.ToString("yyyy-MM-dd") & "' as date)") Then
                                    Correctos = Correctos + 1
                                    'MessageBox.Show("¡Se Guardaron/Actualizaron correctamente!", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Else
                                    MessageBox.Show("Se presentó un error al actualizar/guardar la información", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            End If
ContinuoSinActualizar:
                        End If
                    End If
                Next

                If Correctos > 0 Then
                    If Correctos = 1 Then
                        MessageBox.Show("¡Se Guardó/Actualizó un registro correctamente!", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf Correctos > 1 Then
                        MessageBox.Show("¡Se Guardaron/Actualizaron " & Correctos & " registros correctamente!", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    cargar_registros2()
                    BuscaOrdenes()
                End If

            End Using
            SQL.Cerrar()

        Catch ex As Exception
            MessageBox.Show("Error al cargar excel: " + Environment.NewLine + ex.ToString())
        End Try

    End Sub


    Private Function Actualizo(Query As String) As Boolean
        'VARIABLES DE CONEXION A LA BASE DE DATOS
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim CadenaSQL As String = ""
        Try
            con.ConnectionString = StrTpm
            con.Open()
            cmd.Connection = con
            cmd.CommandText = Query
            cmd.ExecuteNonQuery()
            'MAND A LLAMAR EL METODO DE LLENADO DEL CONTENIDO
        Catch ex As Exception
            Return False
        Finally
            con.Close()
        End Try
        Return True
    End Function


    'Se realiza la importación del archivo devuelto por el proveedor
    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        ImportarExcel()
    End Sub

    Private Sub BtnRecibos_Click_1(sender As Object, e As EventArgs) Handles BtnRecibos.Click
        'ExportarDatosExcel_Sol(DGVEncOrdVta, "")
        mGeneraExcel_articulos()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If MessageBox.Show("¿Desea enviar el archivo por correo electronico?", "Por favor confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim enviar As New frmEnviarCorreo()

            enviar.Show()

            If Trim(strFileName <> "") Then
                If Dir(strFileName, FileAttribute.Archive) <> "" Then
                    enviar.lblRuta.Text = strFileName
                Else
                    enviar.lblRuta.Text = CreaArchivo()
                End If
            Else
                enviar.lblRuta.Text = CreaArchivo()
            End If

            enviar.lblOrigen.Text = "Pedido"
            enviar.txtCuerpo.Text = "Buenas tardes, Adjunto encuentras el archivo en Excel con el requerimiento de nuestro pedido diario ¿Me apoyas con la disponibilidad?"

            enviar.Hide()
            enviar.ShowDialog()
        End If
        'If (Trim(strFileName) <> "") Then
        '  Process.Start(saveFileDialog1.FileName)
        'End If
    End Sub

    Private Sub EnviarMailOC()
        If MessageBox.Show("¿Desea enviar el archivo por correo electronico?", "Por favor confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim enviar As New frmEnviarCorreo()

            enviar.lblRuta.Text = CreaArchivoOC()

            If Trim(enviar.lblRuta.Text) = "" Then
                MsgBox("No se pudo generar el archivo de ordenes de compra")
                Exit Sub
            End If

            enviar.Show()
            enviar.lblOrigen.Text = "OC"
            enviar.txtCuerpo.Text = "Buen día, Adjunto encuentras el archivo en Excel con las ordenes de compra de nuestros pedidos, gracias y saludos."

            enviar.Hide()
            enviar.ShowDialog()
        End If
    End Sub

    Private Sub ExportVend_Click_1(sender As Object, e As EventArgs) Handles ExportVend.Click
        mGeneraExcel()
    End Sub

    Private Sub btnGuardarEnviar_Click_1(sender As Object, e As EventArgs) Handles btnGuardarEnviar.Click
        'Valida que en el archivo de excel se ingrese almenos un datoa solicitar 
        Dim Continuar As Integer = 0
        Dim infAlm As Boolean = False
        For Each fila As DataGridViewRow In DGVEncOrdVta.Rows
            infAlm = False
            If Trim(fila.Cells(12).Value.ToString()) <> "" Then
                If IsNumeric(Trim(fila.Cells(12).Value.ToString())) = False Then
                    MsgBox("Por favor ingrese un valor numerico")
                    fila.Cells(12).Selected = True
                    Exit Sub
                End If

                If IsNumeric(fila.Cells(12).Value) And fila.Cells(12).Value > 0 Then
                    infAlm = True
                    Continuar = Continuar + 1
                Else
                    infAlm = False
                End If
            End If

            If (Trim(fila.Cells(13).Value.ToString()) = "" And infAlm = True) Then
                MsgBox("Por favor indique el almacén ")
                fila.Cells(13).Selected = True
                Exit Sub
            End If
        Next

        If Continuar > 0 Then
            ExportarEnviar()
            Button2.Visible = True
            Label9.Visible = True
        Else
            MsgBox("Para poder exportar el archivo será necesario que se solicite al menos una pieza")
            Button2.Visible = False
            Label9.Visible = False
        End If
    End Sub

    'Funcion para que solo permite el ingreso de caracteres tipo numerico
    Private Sub ValidaNro_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        ' hay que poner en e format de cada columna un formato numérico para que controle solo números

        Dim FormatoColumna As String = DGVEncOrdVta.Columns(DGVEncOrdVta.CurrentCell.ColumnIndex).DefaultCellStyle.Format.ToString
        If FormatoColumna = "" Then Exit Sub

        Select Case e.KeyChar
            Case "0" To "9", vbBack
                e.Handled = False
            Case "."
                If FormatoColumna.Contains(".") Then
                    e.Handled = CType(sender, TextBox).Text.Contains(".")   ' verifica si ya tiene un punto decimal
                Else
                    e.Handled = True
                End If
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub DGVEncOrdVta_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DGVEncOrdVta.CellMouseDown
        If e.Button = MouseButtons.Right Then
            'DGVEncOrdVta.CurrentCell = DGVEncOrdVta(e.ColumnIndex, e.RowIndex)
            Dim Confirmado As Integer

            If (e.ColumnIndex = 21) Then
                If Trim(Inf_ArticuloOriginal) = "TODOS" Then
                    Exit Sub
                End If

                'Validar que solo se puedan modificar aquellos renlgones donde se haya solicitado un pedido
                If DGVEncOrdVta(14, e.RowIndex).Value <= 0 Then
                    Exit Sub
                End If

                If (IsDBNull(DGVEncOrdVta(21, e.RowIndex).Value)) Then
                    Inf_ordencompraOriginal = ""
                Else
                    Inf_ordencompraOriginal = DGVEncOrdVta(21, e.RowIndex).Value
                End If


                Inf_ArticuloOriginal = DGVEncOrdVta(2, e.RowIndex).Value 'Articulo que sera usado para el WHERE del update

                'Cambio el color para indicar edicion
                ColorOriginal = DGVEncOrdVta.CurrentCell.Style.BackColor
                DGVEncOrdVta(21, e.RowIndex).Style.BackColor = Color.LightYellow

                DGVEncOrdVta(21, e.RowIndex).ReadOnly = False
                DGVEncOrdVta.CurrentCell = DGVEncOrdVta(21, e.RowIndex)
                DGVEncOrdVta.BeginEdit(True)
            End If

            If (e.ColumnIndex = 14) Then
                Inf_ArticuloOriginal = DGVEncOrdVta(2, e.RowIndex).Value

                If Trim(Inf_ArticuloOriginal) = "TODOS" Then
                    Exit Sub
                End If

                Inf_ConfirmadoOriginal = DGVEncOrdVta(14, e.RowIndex).Value

                Inf_PedidoCte = DGVEncOrdVta(8, e.RowIndex).Value
                Inf_pendientefacturar = DGVEncOrdVta(9, e.RowIndex).Value
                Inf_stockactual = DGVEncOrdVta(10, e.RowIndex).Value
                Inf_piezasfaltantes = DGVEncOrdVta(11, e.RowIndex).Value
                Inf_asolicitar = DGVEncOrdVta(12, e.RowIndex).Value
                Inf_Almacen = DGVEncOrdVta(13, e.RowIndex).Value
                Inf_montofaltantes = DGVEncOrdVta(15, e.RowIndex).Value
                Inf_solicitado = DGVEncOrdVta(16, e.RowIndex).Value

                DGVEncOrdVta(14, e.RowIndex).ReadOnly = False
                DGVEncOrdVta.CurrentCell = DGVEncOrdVta(14, e.RowIndex)
                DGVEncOrdVta.BeginEdit(True)

                If Confirmado > 0 Then
                    Inf_ConfirmadoOriginal = Confirmado

                End If
                'Obtengo el valor original del campo 
            End If

        End If
    End Sub

    Private Sub DGVEncOrdVta_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGVEncOrdVta.EditingControlShowing
        If DGVEncOrdVta.CurrentCell.ColumnIndex = 14 Or DGVEncOrdVta.CurrentCell.ColumnIndex = 12 Then
            AddHandler e.Control.KeyDown, AddressOf cell_KeyDown
        End If
        If DGVEncOrdVta.CurrentCell.ColumnIndex = 21 Then
            AddHandler e.Control.KeyPress, AddressOf cell_KeyPress
        End If
    End Sub

    Function Fg_OnlyNumbers(ByVal StrDigit As String) As Boolean
        Dim Dt_Integer As Integer = CInt(Asc(StrDigit))
        Dim flag As Boolean
        If Dt_Integer = 8 Then
            flag = False
        Else
            If InStr("1234567890.", StrDigit) = 0 Then
                flag = True
                'MsgBox("Por favor ingrese solo numeros en esta celda", MessageBoxButtons.OK, "Información")
            End If
        End If
        Return flag
    End Function

    Private Sub cell_KeyDown(sender As Object, e As KeyEventArgs)
        If DGVEncOrdVta.CurrentCell.ColumnIndex = 14 Or DGVEncOrdVta.CurrentCell.ColumnIndex = 12 Then
            If (e.KeyCode = Keys.Escape) Then
                DGVEncOrdVta.CurrentCell.Value = Inf_ConfirmadoOriginal
                DGVEncOrdVta.CurrentCell.ReadOnly = True
            End If

            e.Handled = Fg_OnlyNumbers(Chr(e.KeyValue))
            If e.Handled Then
                e.SuppressKeyPress = False
            End If
        End If
    End Sub

    Private Sub cell_KeyPress(sender As Object, e As KeyPressEventArgs)
        If DGVEncOrdVta.CurrentCell.ColumnIndex = 21 Then
            If Asc(e.KeyChar) = Keys.Escape Then
                DGVEncOrdVta.CurrentCell.Value = Inf_ordencompraOriginal
                DGVEncOrdVta.CurrentCell.ReadOnly = True
                DGVEncOrdVta.CurrentCell.Style.BackColor = ColorOriginal 'Regreso color normal
            End If
        End If
    End Sub

    Private Sub DGVEncOrdVta_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DGVEncOrdVta.CellValueChanged
        Dim NvoOriginal As Integer
        Dim NvaOrdenCompra As String
        Dim Query As String
        If IsNumeric(DGVEncOrdVta.CurrentCell.Value) = True And DGVEncOrdVta.CurrentCell.ColumnIndex = 14 Then
            NvoOriginal = DGVEncOrdVta.CurrentCell.Value
            If Inf_ConfirmadoOriginal <> NvoOriginal Then
                'Realizo el grabado de la informacion
                If SQL.SiExiste("SELECT * FROM TPDSolicitudArticulos WHERE articulo = '" + Inf_ArticuloOriginal + "' AND CAST(fechapedido as date) = CAST(GETDATE() as date)") <> True Then

                    'Busco las ordenes de venta del articulo
                    OrdenVentaRelacionada = ""
                    For Each item As DataRow In dvdet.Table.Rows
                        If (item.Item("Filtro").Equals(Inf_ArticuloOriginal)) Then
                            If (OrdenVentaRelacionada.Trim.Equals("")) Then
                                OrdenVentaRelacionada = item.Item("OrdVta")
                            Else
                                OrdenVentaRelacionada = OrdenVentaRelacionada & "," & item.Item("OrdVta")
                            End If
                        End If
                    Next

                    Query = "INSERT INTO TPDSolicitudArticulos (id,articulo,pedidocliente,pendientefacturar,stockactual,piezasfaltantes,asolicitar,almacen,montofaltantes,solicitado,fechapedido, OrdenVentaRelacionada, Bloquear_Surtido) VALUES " +
                                    "((SELECT MAX(id) + 1 FROM TPDSolicitudArticulos),'" + Inf_ArticuloOriginal + "'," + Inf_PedidoCte + "," + Inf_pendientefacturar + "," + Inf_stockactual + "," +
                                    Inf_piezasfaltantes + "," + If(Inf_asolicitar = "", "NULL", Inf_asolicitar) + ",'" + Inf_Almacen + "'," + Inf_montofaltantes + "," + Inf_solicitado + ",GETDATE(), '" + OrdenVentaRelacionada + "', 'SI')"
                Else
                    Query = "UPDATE TPDSolicitudArticulos SET Confirmado = " & NvoOriginal & " WHERE articulo = '" & Inf_ArticuloOriginal & "' AND CAST(fechapedido as date) = CAST(GETDATE() as date)"
                End If

                If SQL.EjecutarCRUD(Query) Then
                    DGVEncOrdVta.CurrentCell.Style.BackColor = Color.GreenYellow
                Else
                    MessageBox.Show("Se produjo un error al insertar los datos", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                'Vuelvo a bloquear la edicion de la celda
                DGVEncOrdVta.CurrentCell.ReadOnly = True
            End If
        End If

        'If DGVEncOrdVta.CurrentCell.ColumnIndex = 21 Then
        '  If IsDBNull(DGVEncOrdVta.CurrentCell.Value) Then
        '    NvaOrdenCompra = ""
        '  Else
        '    NvaOrdenCompra = DGVEncOrdVta.CurrentCell.Value
        '  End If

        '  If Inf_ordencompraOriginal <> NvaOrdenCompra Then
        '    'Realizo el grabado de la informacion
        '    If SQL.SiExiste("SELECT * FROM TPDSolicitudArticulos WHERE articulo = '" + Inf_ArticuloOriginal + "' AND CAST(fechapedido as date) BETWEEN CAST('" & DtpFechaIni.Value.ToString("yyyy-MM-dd") & "' as date) AND CAST('" & DtpFechaTer.Value.ToString("yyyy-MM-dd") & "' as date)") <> True Then
        '      MsgBox("Esta orden no puede ser grabada.")
        '    Else
        '      Query = "UPDATE TPDSolicitudArticulos SET ordencompra = '" & NvaOrdenCompra & "' WHERE articulo = '" & Inf_ArticuloOriginal & "' AND CAST(fechapedido as date) BETWEEN CAST('" & DtpFechaIni.Value.ToString("yyyy-MM-dd") & "' as date) AND CAST('" & DtpFechaTer.Value.ToString("yyyy-MM-dd") & "' as date)"

        '      If SQL.EjecutarCRUD(Query) Then
        '        DGVEncOrdVta.CurrentCell.ReadOnly = True
        '        DGVEncOrdVta.CurrentCell.Style.BackColor = ColorOriginal
        '      Else
        '        MessageBox.Show("Se produjo un error al insertar los datos", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '      End If
        '    End If
        '    'Vuelvo a bloquear la edicion de la celda
        '  End If
        'End If

    End Sub

    Private Sub ActualizaOC()
        Dim NvaOrdenCompra As String
        Dim CantOC As Integer = 0
        Dim Query As String

        If DGVEncOrdVta.CurrentCell.ColumnIndex = 21 Then
            If IsDBNull(DGVEncOrdVta.CurrentCell.Value) Then
                NvaOrdenCompra = ""
            Else
                NvaOrdenCompra = DGVEncOrdVta.CurrentCell.Value
            End If

            If Inf_ordencompraOriginal <> NvaOrdenCompra Then
                'Realizo el grabado de la informacion
                If SQL.SiExiste("SELECT * FROM TPDSolicitudArticulos WHERE articulo = '" + Inf_ArticuloOriginal + "' AND CAST(fechapedido as date) BETWEEN CAST('" & DtpFechaIni.Value.ToString("yyyy-MM-dd") & "' as date) AND CAST('" & DtpFechaTer.Value.ToString("yyyy-MM-dd") & "' as date)") <> True Then
                    MsgBox("Esta orden no puede ser grabada.")
                Else
                    Query = "UPDATE TPDSolicitudArticulos SET ordencompra = '" & NvaOrdenCompra & "' WHERE articulo = '" & Inf_ArticuloOriginal & "' AND CAST(fechapedido as date) BETWEEN CAST('" & DtpFechaIni.Value.ToString("yyyy-MM-dd") & "' as date) AND CAST('" & DtpFechaTer.Value.ToString("yyyy-MM-dd") & "' as date)"

                    If SQL.EjecutarCRUD(Query) Then
                        DGVEncOrdVta.CurrentCell.ReadOnly = True
                        DGVEncOrdVta.CurrentCell.Style.BackColor = ColorOriginal
                    Else
                        MessageBox.Show("Se produjo un error al insertar los datos", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
                'Vuelvo a bloquear la edicion de la celda
            End If
        End If

        For i = 0 To (DGVEncOrdVta.RowCount - 1)
            If Not DGVEncOrdVta.Item(21, i).Value Is DBNull.Value Then
                If Trim(DGVEncOrdVta.Item(21, i).Value) <> "" Then
                    CantOC = 1
                    Exit For
                End If
            End If
        Next

        If CantOC > 0 Then
            btnEnviarOC.Visible = True
            Label13.Visible = True
        Else
            btnEnviarOC.Visible = False
            Label13.Visible = False
        End If
    End Sub

    Private Sub btnEnviarOC_Click(sender As Object, e As EventArgs) Handles btnEnviarOC.Click
        EnviarMailOC()
    End Sub

    Private Sub LblVerSol_Click(sender As Object, e As EventArgs) Handles LblVerSol.Click

    End Sub

    Sub cargar_registros2()
        'Nuevo objeto Dataset   
        Dim DsVtasDet As New DataSet
        Dim ExisteSolicitud As Boolean = False


        Button2.Visible = False
        Label9.Visible = False

        'SE EJECUTA PROCEDIMIENTO ALMACENADO Y DEVUELVE DATA SET
        DsVtasDet = SQL.EjecutarProcedimientoTB("TPD_SolicitudArticulosPruebas_Pruebas2_20", "@FechaIni,@FechaTer,@hora", 3, DtpFechaIni.Value.ToString("yyyy-MM-dd") + "," + DtpFechaTer.Value.ToString("yyyy-MM-dd") + "," + DtpFechaIni.Value.ToString("HH:mm:ss"))

        DsVtasDet.Tables(0).TableName = "DetOrdVta"
        DsVtasDet.Tables(1).TableName = "EncOrdVta"

        dvdet.Table = DsVtasDet.Tables("DetOrdVta")

        dvaux.Table = DsVtasDet.Tables(2)
        dvaux.RowFilter = "Filtro <> 'TODOS'"
        '**********************************************************************************************************************************
        DGVAux.DataSource = dvaux
        With DGVAux
            .Columns(0).HeaderText = "Usuario Ventas"
            .Columns(1).HeaderText = "Fecha Creación"
            .Columns(2).HeaderText = "Orden de Vta"
            .Columns(3).HeaderText = "Clave Cliente"
            .Columns(4).HeaderText = "Nombre Cliente"
            .Columns(5).Visible = False
            .Columns(6).Visible = False
            .Columns(7).HeaderText = "Articulo"
            .Columns(8).HeaderText = "# Fabricante"
            .Columns(9).HeaderText = "Descripción"
            .Columns(10).HeaderText = "Linea"
            .Columns(11).Visible = False
            .Columns(12).HeaderText = "Pedido del Cliente"
            .Columns(13).HeaderText = "Pendiente por Facturar"
            .Columns(14).HeaderText = "Stock Actual"
            .Columns(15).HeaderText = "Piezas Faltantes"
            .Columns(16).HeaderText = "Requerido"
            .Columns(17).Visible = False
            .Columns(18).Visible = False
            .Columns(19).Visible = False
            .Columns(20).Visible = False
        End With
        '**********************************************************************************************************************************

        dvenc = DsVtasDet.Tables("EncOrdVta")
        '**********************************************************************************************************************************
        'Uno
        With DGVEncOrdVta
            .DataSource = dvenc 'DsVtasDet.Tables("EncOrdVta")

            '.Columns().Remove("Bloquear_Surtido")
            'Dim columnBS As New DataGridViewComboBoxColumn()
            'With columnBS
            '    .DataPropertyName = "Bloquear_Surtido"
            '    .Name = "Bloquear_Surtido"
            '    .HeaderText = "Surtido Bloqueado"
            '    .DropDownWidth = 60
            '    .Width = 60
            '    .MaxDropDownItems = 2
            '    .Items.AddRange("SI", "NO")
            '    .ReadOnly = True
            'End With
            '.Columns.Insert(23, columnBS)

            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .RowHeadersWidth = 35
            .ShowCellToolTips = True

            .MultiSelect = True
            .AllowUserToAddRows = False
            .AllowDrop = False
            .AllowUserToDeleteRows = False

            .Columns("Num").HeaderText = "#"
            .Columns("Num").Width = 30
            .Columns("Num").ReadOnly = True

            .Columns("Filtro").HeaderText = "Artículo"
            .Columns("Filtro").Width = 70
            .Columns("Filtro").ReadOnly = True

            .Columns("Articulo").Visible = False

            '.Columns("SuppCatNum").HeaderText = "# Fabricante"
            '.Columns("SuppCatNum").Width = 100
            '.Columns("SuppCatNum").ReadOnly = True

            .Columns("NumFabricante").HeaderText = "# Fabricante"
            .Columns("NumFabricante").Width = 100
            .Columns("NumFabricante").ReadOnly = True

            .Columns("Descripcion").HeaderText = "Descripción"
            .Columns("Descripcion").Width = 325
            .Columns("Descripcion").ReadOnly = True

            .Columns("Linea").HeaderText = "Linea"
            .Columns("Linea").Width = 80
            .Columns("Linea").ReadOnly = True

            If UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" Or UsrTPM = "COMERCIAL" Then
                .Columns("Articulo").Visible = False
            End If
            .Columns("Proveedor").HeaderText = "Proveedor"
            .Columns("Proveedor").Width = 100
            .Columns("Proveedor").ReadOnly = True

            If UsrTPM = "VENTAS2" Or UsrTPM = "VENTAS3" Or UsrTPM = "RLIRA" Or UsrTPM = "COMERCIAL" Then
                .Columns("Pro. Alterno").Visible = False
            End If
            .Columns("Pro. Alterno").HeaderText = "Proveedor alterno"
            .Columns("Pro. Alterno").Width = 100
            .Columns("Pro. Alterno").ReadOnly = True

            .Columns("Solicitado").HeaderText = "Pedido del cliente"
            .Columns("Solicitado").Width = 55
            .Columns("Solicitado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Solicitado").DefaultCellStyle.Format = "###,###,###"
            .Columns("Solicitado").ReadOnly = True

            .Columns("PendienteFacturar").HeaderText = "Pendiente Facturar"
            .Columns("PendienteFacturar").Width = 55
            .Columns("PendienteFacturar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("PendienteFacturar").DefaultCellStyle.Format = "###,###,###"
            .Columns("PendienteFacturar").ReadOnly = True

            .Columns("Existencia").HeaderText = "Stock Actual"
            .Columns("Existencia").Width = 55
            .Columns("Existencia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Existencia").DefaultCellStyle.Format = "###,###,###"
            .Columns("Existencia").ReadOnly = True

            .Columns("Solicitar").HeaderText = "Piezas faltantes"
            .Columns("Solicitar").Width = 55
            .Columns("Solicitar").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Solicitar").DefaultCellStyle.Format = "###,###,###"
            .Columns("Solicitar").ReadOnly = True

            .Columns("Request").HeaderText = "A solicitar"
            .Columns("Request").Width = 45
            .Columns("Request").ReadOnly = False

            .Columns("Almacen").HeaderText = "Almacen"
            .Columns("Almacen").Width = 60
            .Columns("Almacen").ReadOnly = False

            .Columns("Confirmado").HeaderText = "Confirmado"
            .Columns("Confirmado").Width = 60
            .Columns("Confirmado").ReadOnly = True
            .Columns("Confirmado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'recorre las filas del DataGrid
            Dim numfilas As Integer
            numfilas = .RowCount 'cuenta las filas del DataGrid
            Dim valorConfirmado As Integer
            If numfilas > 0 Then
                For i = 0 To (numfilas - 1)
                    If DGVEncOrdVta.Item(14, i).Value Is DBNull.Value Or DGVEncOrdVta.Item(14, i).Value = 0 Then
                        DGVEncOrdVta.Item(14, i).Value = 0
                        valorConfirmado = 0
                    Else
                        valorConfirmado = CInt(DGVEncOrdVta.Item(14, i).Value)
                    End If

                    If valorConfirmado > 0 Then
                        DGVEncOrdVta.Rows(i).Cells(14).Style.BackColor = Color.GreenYellow
                    End If
                Next
            End If

            .Columns("PrcProm").HeaderText = "$ Precio Promedio Vta"
            .Columns("PrcProm").Width = 60
            .Columns("PrcProm").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("PrcProm").DefaultCellStyle.Format = "###,###,###.00"
            .Columns("PrcProm").ReadOnly = True

            .Columns("MontVtaSol").ReadOnly = True
            .Columns("MontVtaSol").Visible = False

            .Columns("Solicitado2").HeaderText = "Solicitado Histórico."
            .Columns("Solicitado2").Width = 60
            .Columns("Solicitado2").ReadOnly = True

            .Columns("Pendiente").Visible = False
            .Columns("Solicitar1").Visible = False

            .Columns("Comments").Visible = False
            .Columns("Comments").ReadOnly = True

            .Columns("ordencompra").HeaderText = "Orde de compra"
            .Columns("ordencompra").Width = 60
            .Columns("ordencompra").Visible = True
            .Columns("ordencompra").ReadOnly = True

            'Id de la tabla de TPDSolicitudArticulos
            .Columns("id_TPDSolicitudArticulos").HeaderText = "id de TPDSolicitudArticulos"
            .Columns("id_TPDSolicitudArticulos").Width = 60
            .Columns("id_TPDSolicitudArticulos").ReadOnly = True
            .Columns("id_TPDSolicitudArticulos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("id_TPDSolicitudArticulos").Visible = False

            'Numeros de Orden de venta relacionados
            .Columns("OrdenVentaRelacionada").HeaderText = "Orden de Venta Relacionada"
            .Columns("OrdenVentaRelacionada").Width = 60
            .Columns("OrdenVentaRelacionada").ReadOnly = True
            .Columns("OrdenVentaRelacionada").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("OrdenVentaRelacionada").Visible = False

            'Indicador de bloqueado o no para surtido
            .Columns("Bloquear_Surtido").HeaderText = "Surtido Bloqueado"
            .Columns("Bloquear_Surtido").Width = 60
            .Columns("Bloquear_Surtido").ReadOnly = True
            .Columns("Bloquear_Surtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Bloquear_Surtido").ToolTipText = "Presiona doble click para desbloquear artículo"

            btnEnviarOC.Visible = False
            Label13.Visible = False
            For i = 0 To (numfilas - 1)
                'Verifico si existe alguna solicitud, si es asi entonces habilito el el boton de envio
                If Not DGVEncOrdVta.Item(12, i).Value Is DBNull.Value And ExisteSolicitud = False Then
                    If Trim(DGVEncOrdVta.Item(12, i).Value) > 0 Then
                        ExisteSolicitud = True
                    End If
                End If

                'Reviso si existe algun renglon con orde de compra
                If Not DGVEncOrdVta.Item(21, i).Value Is DBNull.Value Then
                    If Trim(DGVEncOrdVta.Item(21, i).Value) <> "" Then
                        btnEnviarOC.Visible = True
                        Label13.Visible = True
                    End If
                End If
            Next
        End With

        If ExisteSolicitud Then
            Button2.Visible = True
            Label9.Visible = True
        End If

        'MsgBox("nombre es " & DGVEncOrdVta.Columns(14).Name)
        ReemplazarColumna()

        If UsrTPM <> "COMPRAS1" And UsrTPM <> "MANAGER" Then
            DGVEncOrdVta.Columns("Proveedor").Visible = False
        End If

        'If UsrTPM = "COMPRAS1" Or UsrTPM = "MANAGER" Or UsrTPM = "SISTEMAS" Or UsrTPM = "ALMACEN1" Then
        '    DGVEncOrdVta.Columns(13).ReadOnly = False
        'Else
        '    DGVEncOrdVta.Columns(13).ReadOnly = True
        'End If

        With DGVDetOrdVta
            .DataSource = dvdet
            .ReadOnly = True
            'Color de Renglones en Grid
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            'Superior Izquierda del gridview
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = True
            .AllowUserToAddRows = False
            'Color de linea del grid
            .Columns(0).HeaderText = "Usuario Ventas"
            .Columns(0).Width = 90

            .Columns(1).HeaderText = "Fecha Creación"
            .Columns(1).Width = 70

            .Columns(2).HeaderText = "Orden de Vta"
            .Columns(2).Width = 45

            .Columns(3).HeaderText = "Clave Cliente"
            .Columns(3).Width = 50

            .Columns(4).HeaderText = "Nombre Cliente"
            .Columns(4).Width = 175

            .Columns(5).HeaderText = "Total del Pedido"
            .Columns(5).Width = 60
            .Columns(5).DefaultCellStyle.Format = "###,###,###.##"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(6).Visible = False

            .Columns(7).HeaderText = "Articulo"
            .Columns(7).Width = 100

            .Columns(8).HeaderText = "# Fabricante"
            .Columns(8).Width = 100

            .Columns(9).HeaderText = "Descripción"
            .Columns(9).Width = 160

            .Columns(10).Visible = False

            .Columns(11).HeaderText = "Proveedor"
            .Columns(11).Width = 160
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(11).DefaultCellStyle.Format = "###,###,###"

            .Columns(12).HeaderText = "Pro. Alterno"
            .Columns(12).Width = 160
            .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(12).DefaultCellStyle.Format = "###,###,###"

            .Columns(13).HeaderText = "Pedido del Cliente"
            .Columns(13).Width = 55
            .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(13).DefaultCellStyle.Format = "###,###,###"

            .Columns(14).HeaderText = "Pendiente por Facturar"
            .Columns(14).Width = 55
            .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(14).DefaultCellStyle.Format = "###,###,###"

            .Columns(15).HeaderText = "Stock Actual"
            .Columns(15).Width = 55
            .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(15).DefaultCellStyle.Format = "###,###,###"

            .Columns(16).HeaderText = "Piezas Faltantes"
            .Columns(16).Width = 55
            .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(16).DefaultCellStyle.Format = "###,###,###"

            .Columns(17).HeaderText = "A Solicitar"
            .Columns(17).Width = 55
            .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(17).DefaultCellStyle.Format = "###,###,###"
            .Columns(17).ReadOnly = False
            .Columns(17).Visible = False

            .Columns(18).HeaderText = "Almacen"
            .Columns(18).Width = 55
            .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(18).DefaultCellStyle.Format = "###,###,###"
            .Columns(18).ReadOnly = False
            .Columns(18).Visible = False

            .Columns(19).HeaderText = "$ Precio Vta"
            .Columns(19).Width = 60
            .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(19).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(19).Visible = False
            '.Columns(20).HeaderText = "$ Monto Vta Pzas Faltantes Prueba"
            '.Columns(20).Width = 70
            '.Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '.Columns(20).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(20).HeaderText = "$ Precio Vta"
            .Columns(20).Width = 70
            .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(20).DefaultCellStyle.Format = "###,###,###.00"


            .Columns(21).HeaderText = "$ Monto Vta Pzas Faltantes"
            .Columns(21).Width = 70
            .Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(21).DefaultCellStyle.Format = "###,###,###.00"

            .Columns(22).Visible = False
            '.Columns(16).Visible = False
            '.Columns(17).Visible = False
            '.Columns(18).Visible = False
        End With

        If UsrTPM <> "COMPRAS1" And UsrTPM <> "MANAGER" Then
            DGVDetOrdVta.Columns("Proveedor").Visible = False
        End If

        'For Each row As DataGridViewRow In Me.DGVEncOrdVta.Rows
        '    If row.Cells("Pzas Pendientes").Value <= 0 Then
        '        row.Cells("Pzas a Solicitar").Value = 0
        '    Else
        '        row.Cells("Pzas a Solicitar").Value = row.Cells("Pzas Pendientes").Value
        '    End If
        'Next


        'Esta parte hacia que los historicos fuerans 0
        'For Each row As DataGridViewRow In Me.DGVEncOrdVta.Rows
        '  If Not row.Cells(16).Value Is DBNull.Value Then
        '    If row.Cells(16).Value <= 0 Or row.Cells(16) Is DBNull.Value Then
        '      row.Cells(17).Value = 0
        '    Else
        '      row.Cells(17).Value = row.Cells(17).Value
        '    End If
        '  Else
        '    row.Cells(17).Value = 0
        '  End If
        'Next

        'DGVEncOrdVta.Rows(12).DefaultCellStyle.Font = New Font(DGVEncOrdVta.DefaultCellStyle.Font, FontStyle.Bold)
        'DGVEncOrdVta.Rows(8).DefaultCellStyle.Font = New Font(DGVEncOrdVta.Rows(8).DefaultCellStyle.Font, FontStyle.Bold)
        Try
            DGVEncOrdVta.CurrentCell = DGVEncOrdVta.Rows(1).Cells(0)
            DGVEncOrdVta.CurrentCell = DGVEncOrdVta.Rows(0).Cells(0)
        Catch
        End Try

        'With conexion2
        '    If .State = ConnectionState.Open Then
        '        .Close()
        '    End If
        '    .Dispose()
        'End With

    End Sub

    Private Sub DGVEncOrdVta_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVEncOrdVta.CellContentDoubleClick
        'VARIABLES DE CONEXION A LA BASE DE DATOS
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim CadenaSQL As String = ""
        'VALIDA SI HAY REGISTROS EN EL GRID
        Dim strValue As String
        If e.RowIndex >= 0 Then
            strValue = Me.DGVEncOrdVta.Item(e.ColumnIndex, e.RowIndex).Value
            If Me.DGVEncOrdVta.Columns(e.ColumnIndex).Name = "Bloquear_Surtido" And strValue = "SI" Then 'Solo modificare las que tengan SI
                If MsgBox("Esta seguro de que este artículo no será surtido?" & Chr(13) & "Al confirmar que SI es probable que la orden de venta se desbloquee! ", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Por favor confirme") = vbNo Then
                    Exit Sub
                Else
                    'Desbloqueo Orden de venta 
                    'Le quito el SI a la celda
                    DGVEncOrdVta.Rows(e.RowIndex).Cells("Bloquear_Surtido").Value = ""
                    'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
                    CadenaSQL = "UPDATE TPM.dbo.TPDSolicitudArticulos SET Bloquear_Surtido = ''"
                    CadenaSQL &= " WHERE Articulo = '" + DGVEncOrdVta.Rows(e.RowIndex).Cells("Filtro").Value.ToString + "' "
                    CadenaSQL &= " AND id = " + DGVEncOrdVta.Rows(e.RowIndex).Cells("id_TPDSolicitudArticulos").Value.ToString() + " "

                    If CadenaSQL <> "" Then
                        'EJECUTA LA CONSULTA
                        Try
                            con.ConnectionString = StrTpm
                            con.Open()
                            cmd.Connection = con
                            cmd.CommandText = CadenaSQL
                            cmd.ExecuteNonQuery()
                            'REFRESCA EL GRID DE CONTENIDO
                            Me.DGVEncOrdVta.RefreshEdit()
                            'MAND A LLAMAR EL METODO DE LLENADO DEL CONTENIDO
                        Catch ex As Exception
                            MessageBox.Show("Error al actualizar el Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        Finally
                            con.Close()
                        End Try
                    End If
                End If
            End If
        End If
    End Sub
End Class