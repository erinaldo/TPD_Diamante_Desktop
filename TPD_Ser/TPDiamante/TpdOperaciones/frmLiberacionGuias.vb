Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.IO
Public Class frmLiberacionGuias
  Dim DTVliberacionGuias As DataView
  Dim DTVGuiasDetalle As DataView
  Dim col1 As New DataGridViewButtonColumn
  Dim SQLEmpleado As String
  'Colores creados para uso en el grid DGVoperacionVta
  Dim rojo As Color
  Dim amarillo As Color
  Dim verde As Color
  Dim Anaranjado As Color


  Private Sub frmLiberacionGuias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    dtpFecha.Value = DateTime.Now
    'SE ASIGNA EL TIEMPO DE RECARGA DEL GRID EN MILISEGUNDOS
    Timer1.Interval = 120000
    'Activamos el evento timer para que el grid se actualize solo 
    Timer1.Enabled = True
    LlenaLiberacionMat()
    GuiasLiberadas()
  End Sub

  Sub LlenaLiberacionMat()
    Try
      If DGVLiberacionGuias.RowCount <> 0 Then
        DGVLiberacionGuias.Columns().Remove("Liberar")
        DGVLiberacionGuias.DataSource = Nothing
      End If

      DTVliberacionGuias = New DataView
            DTVliberacionGuias.Table = SQL.EjecutarProcedimiento("TPD_Liberacion_Guias2", "@Fecha", 1, dtpFecha.Value.ToString("yyyy-MM-dd"))
            DGVLiberacionGuias.DataSource = DTVliberacionGuias
      MEstiloGridGuias()

      If (DTVliberacionGuias.Count > 0) Then
        col1.DataPropertyName = "Liberar"
        col1.HeaderText = "Liberar"
        col1.Name = "Liberar"
        col1.Text = "Liberar"
        col1.UseColumnTextForButtonValue = True
        col1.SortMode = DataGridViewColumnSortMode.Automatic
        col1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        col1.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
        col1.Width = 55
        col1.ReadOnly = True
        DGVLiberacionGuias.Columns.Insert(13, col1)
      End If

    Catch ex As Exception
      MsgBox("Error: " + ex.ToString)
    End Try
  End Sub

  'MODIFICADO IVAN GONZALEZ
  Dim SQL As New Comandos_SQL()
  Sub GuiasLiberadas()
    Try
      If dgvLiberadas.Rows.Count <> 0 Then
        dt = CType(Me.dgvLiberadas.DataSource, DataTable)
        dt.Rows.Clear()
      End If
            dgvLiberadas.DataSource = SQL.EjecutarProcedimiento("TPD_Guias_Liberadas2", "", 0, "")
            EstiloLiberadas()
    Catch ex As Exception
    End Try
  End Sub

  Sub EstiloLiberadas()
    'ESTILOS POR COLUMNA
    With Me.dgvLiberadas
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
      .Columns("DocEntry").Frozen = True
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
      .Columns("DocNum").HeaderText = "No. Factura"
      .Columns("DocNum").Width = 70
      .Columns("DocNum").ReadOnly = True
      .Columns("DocNum").Frozen = True

            'Agente de ventas
            .Columns("Agente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Agente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("Agente").HeaderText = "Agente de Ventas"
            .Columns("Agente").Width = 200
            .Columns("Agente").ReadOnly = True

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
      .Columns("BoxTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("BoxTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("BoxTotal").HeaderText = "Cajas"
      .Columns("BoxTotal").Width = 50
      .Columns("BoxTotal").ReadOnly = True
      'PESO ESTIMADO
      .Columns("Peso Estimado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Peso Estimado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Peso Estimado").HeaderText = "Peso Estimado (KG)"
      .Columns("Peso Estimado").Width = 100
      .Columns("Peso Estimado").ReadOnly = True
      'PESO REAL
      .Columns("Peso Real").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Peso Real").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Peso Real").HeaderText = "Peso Real (KG)"
      .Columns("Peso Real").Width = 100
      .Columns("Peso Real").ReadOnly = True
      'Action
      .Columns("Action").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Action").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Action").HeaderText = "Accion"
      .Columns("Action").Width = 80
      .Columns("Action").ReadOnly = True
      'PAQUETERIA
      .Columns("TrnspName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("TrnspName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("TrnspName").HeaderText = "Paqueteria"
      .Columns("TrnspName").Width = 150
      .Columns("TrnspName").ReadOnly = True
            'direccion
            .Columns("DireccionFiscal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DireccionFiscal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("DireccionFiscal").HeaderText = "Dirección Fiscal"
            .Columns("DireccionFiscal").Width = 200
            .Columns("DireccionFiscal").ReadOnly = True



            .Columns("DireccionEntrega").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DireccionEntrega").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("DireccionEntrega").HeaderText = "Dirección Entrega"
            .Columns("DireccionEntrega").Width = 200
            .Columns("DireccionEntrega").ReadOnly = True
            'comentario sap
            .Columns("Comments").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Comments").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Comments").HeaderText = "Comentario"
      .Columns("Comments").Width = 150
      .Columns("Comments").ReadOnly = True
      'Total
      .Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Total").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Total").HeaderText = "Total S/IVA"
      .Columns("Total").Width = 80
      .Columns("Total").DefaultCellStyle.Format = "N2"
      .Columns("Total").ReadOnly = True
      'facturado
      .Columns("Facturado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Facturado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Facturado").HeaderText = "Facturado"
      .Columns("Facturado").Width = 100
      .Columns("Facturado").ReadOnly = True
    End With
  End Sub
  '--------------------------------------------------------

  Sub MEstiloGridGuias()
    'ESTILOS POR COLUMNA
    With Me.DGVLiberacionGuias
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
      .Columns("DocEntry").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("DocEntry").DefaultCellStyle.WrapMode = DataGridViewTriState.False
      .Columns("DocEntry").ReadOnly = True
      .Columns("DocEntry").Frozen = True
      'FECHA DE ORDEN
      .Columns("PrintTime").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("PrintTime").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("PrintTime").HeaderText = "Fecha Doc."
      .Columns("PrintTime").Width = 85
      .Columns("PrintTime").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("PrintTime").DefaultCellStyle.WrapMode = DataGridViewTriState.False
      .Columns("PrintTime").ReadOnly = True
      'HORA DE IMPRESION
      .Columns("horaCreacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("horaCreacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("horaCreacion").HeaderText = "Hora Creación"
      .Columns("horaCreacion").Width = 65
      .Columns("horaCreacion").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("horaCreacion").DefaultCellStyle.WrapMode = DataGridViewTriState.False
      .Columns("horaCreacion").ReadOnly = True
      'PARTIDAS POR ORDEN
      .Columns("DocNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("DocNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
      .Columns("DocNum").HeaderText = "No. Factura"
      .Columns("DocNum").Width = 70
      .Columns("DocNum").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("DocNum").DefaultCellStyle.WrapMode = DataGridViewTriState.False
      .Columns("DocNum").ReadOnly = True
      .Columns("DocNum").Frozen = True

            'AGENTE

            .Columns("Agente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns("Agente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("Agente").HeaderText = "Agente de Ventas"
            .Columns("Agente").Width = 200
            .Columns("Agente").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("Agente").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns("Agente").ReadOnly = True

            'PAQUETERIA
            .Columns("CardCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("CardCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("CardCode").HeaderText = "Cod. Cliente"
      .Columns("CardCode").Width = 65
      .Columns("CardCode").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("CardCode").DefaultCellStyle.WrapMode = DataGridViewTriState.False
      .Columns("CardCode").ReadOnly = True
      'HORARIO DE PAQUEREIAS
      .Columns("CardName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns("CardName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("CardName").HeaderText = "Cliente"
            .Columns("CardName").Width = 200
            .Columns("CardName").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("CardName").DefaultCellStyle.WrapMode = DataGridViewTriState.True
      .Columns("CardName").ReadOnly = True
      'CODIGO CLIENTE
      .Columns("BoxTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("BoxTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("BoxTotal").HeaderText = "Cajas"
      .Columns("BoxTotal").Width = 50
      .Columns("BoxTotal").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("BoxTotal").DefaultCellStyle.WrapMode = DataGridViewTriState.False
      .Columns("BoxTotal").ReadOnly = True
      'PESO ESTIMADO
      .Columns("Peso Estimado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Peso Estimado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Peso Estimado").HeaderText = "Peso Estimado (KG)"
      .Columns("Peso Estimado").Width = 100
      .Columns("Peso Estimado").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("Peso Estimado").DefaultCellStyle.WrapMode = DataGridViewTriState.False
      .Columns("Peso Estimado").ReadOnly = True
      .Columns("Peso Estimado").Visible = False
      'PESO REAL
      .Columns("Peso Real").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Peso Real").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Peso Real").HeaderText = "Peso Real (KG)"
      .Columns("Peso Real").Width = 100
      .Columns("Peso Real").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("Peso Real").DefaultCellStyle.WrapMode = DataGridViewTriState.False
      .Columns("Peso Real").ReadOnly = True
      'Action
      .Columns("Action").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Action").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Action").HeaderText = "Accion"
      .Columns("Action").Width = 80
      .Columns("Action").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("Action").DefaultCellStyle.WrapMode = DataGridViewTriState.False
      .Columns("Action").ReadOnly = True
      'PAQUETERIA
      .Columns("TrnspName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("TrnspName").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("TrnspName").HeaderText = "Paqueteria"
      .Columns("TrnspName").Width = 150
      .Columns("TrnspName").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("TrnspName").DefaultCellStyle.WrapMode = DataGridViewTriState.True
      .Columns("TrnspName").ReadOnly = True
            'direccion
            .Columns("DireccionFiscal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DireccionFiscal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("DireccionFiscal").HeaderText = "Dirección Fiscal"
            .Columns("DireccionFiscal").Width = 200
            .Columns("DireccionFiscal").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("DireccionFiscal").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns("DireccionFiscal").ReadOnly = True

            .Columns("DireccionEntrega").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("DireccionEntrega").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
            .Columns("DireccionEntrega").HeaderText = "Dirección Entrega"
            .Columns("DireccionEntrega").Width = 200
            .Columns("DireccionEntrega").HeaderCell.Style.WrapMode = DataGridViewTriState.True
            .Columns("DireccionEntrega").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .Columns("DireccionEntrega").ReadOnly = True






            'comentario sap
            .Columns("Comments").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Comments").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Comments").HeaderText = "Comentario"
      .Columns("Comments").Width = 150
      .Columns("Comments").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("Comments").DefaultCellStyle.WrapMode = DataGridViewTriState.True
      .Columns("Comments").ReadOnly = True
      'Total
      .Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Total").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Total").HeaderText = "Total S/IVA"
      .Columns("Total").Width = 80
      .Columns("Total").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("Total").DefaultCellStyle.WrapMode = DataGridViewTriState.False
      .Columns("Total").DefaultCellStyle.Format = "N2"
      .Columns("Total").ReadOnly = True
      'facturado
      .Columns("Facturado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Facturado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Facturado").HeaderText = "Facturado"
      .Columns("Facturado").Width = 100
      .Columns("Facturado").HeaderCell.Style.WrapMode = DataGridViewTriState.True
      .Columns("Facturado").DefaultCellStyle.WrapMode = DataGridViewTriState.False
      .Columns("Facturado").ReadOnly = True
    End With
  End Sub

  Private Sub DGVLiberacionGuias_SelectionChanged(sender As Object, e As EventArgs) Handles DGVLiberacionGuias.SelectionChanged
    If DGVLiberacionGuias.CurrentRow Is Nothing Then
    Else
      Try
        Dim row As DataGridViewRow = DGVLiberacionGuias.CurrentRow()
        'Llena el datagrid detalle 
        LlenarDetalleGuias(CStr(row.Cells("DocEntry").Value).ToString)
        If row.Cells("DocNum").Value.ToString <> "" Then
          LlenarFletes(CStr(row.Cells("DocNum").Value).ToString)
        Else
          dgvFetesFacturados.DataSource = Nothing
        End If

      Catch ex As Exception
        MsgBox("Error al obtener la Orden en llenado de Grid Detalle." + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de Dato")
      End Try
    End If
  End Sub



  Sub LlenarFletes(DocEntry As String)
    Try
      SQL.conectarTPM()
      dgvFetesFacturados.DataSource = SQL.ConsultarTabla("select td.ItemCode,td.Quantity from SBO_TPD.dbo.OINV tf left join SBO_TPD.dbo.INV1 td on td.DocEntry = tf.DocEntry left join SBO_TPD.dbo.OITM ta on ta.ItemCode = td.ItemCode left join SBO_TPD.dbo.OITM tg on tg.ItmsGrpCod = ta.ItmsGrpCod where tf.DocNum = " + DocEntry + " and tg.ItmsGrpCod = 150 group by td.ItemCode,td.Quantity")
      EstilodgvFletes()
    Catch ex As Exception
      MsgBox("Error: " + ex.ToString)
    End Try
  End Sub
  Sub LlenarDetalleGuias(BaseEntry As String)
    'llena a travez de una consulta de sql el gridview detalle
    Try

      DgvGuiasDetalle.DataSource = SQL.EjecutarProcedimiento("TPD_Detalle_Liberacion", "@BaseEntry", 1, BaseEntry)

      MEstiloGridGuiasDetalle()
    Catch ex As Exception
      MsgBox("Error: " + ex.ToString)
    End Try
  End Sub
  Sub EstilodgvFletes()
    'ESTILOS POR COLUMNA
    With Me.dgvFetesFacturados
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
      .Columns("ItemCode").Width = 70
      .Columns("ItemCode").ReadOnly = True
      'FECHA DE ORDEN
      '.Columns("Dscription").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      '.Columns("Dscription").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      '.Columns("Dscription").HeaderText = "Descripción"
      ''.Columns("Dscription").Width = 85
      '.Columns("Dscription").ReadOnly = True
      'Cantidad
      .Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Quantity").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Quantity").HeaderText = "Cantidad"
      .Columns("Quantity").DefaultCellStyle.Format = "N2"
      .Columns("Quantity").Width = 60
      .Columns("Quantity").ReadOnly = True
    End With
  End Sub
  Sub MEstiloGridGuiasDetalle()
    'ESTILOS POR COLUMNA
    With Me.DgvGuiasDetalle
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
      .Columns("NCaja").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("NCaja").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
      .Columns("NCaja").DefaultCellStyle.ForeColor = Color.Red
      .Columns("NCaja").HeaderText = "Caja"
      .Columns("NCaja").Width = 60
      .Columns("NCaja").ReadOnly = True
      'FECHA DE ORDEN
      .Columns("Peso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns("Peso").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Peso").HeaderText = "Peso (KG)"
      .Columns("Peso").Width = 85
      .Columns("Peso").ReadOnly = True
      'Cantidad
      '.Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      '.Columns("Quantity").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      '.Columns("Quantity").HeaderText = "Cantidad"
      '.Columns("Quantity").Width = 60
      '.Columns("Quantity").ReadOnly = True
    End With
  End Sub

  Private Sub IniciaLiberacion(DocEntry As String)
    SQL.conectarTPM()

    'ACTUALIZAR ESTATUS
    Try
        SQL.EjecutarComando("UPDATE Operacion_Entrega  SET Action='Pre Guias' WHERE DocEntry = '" + DocEntry + "'")

      Catch ex As Exception
        MsgBox("Error al Actualizar el Status en la Orden: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
        SQL.Cerrar()
        Exit Sub
      End Try

      'ACTUALIZAR TIEMPO
      Try
      'SQL.EjecutarComando("UPDATE Operacion_Analisis SET TimeEnRevision = getdate() WHERE DocNum = " + Row.Cells("OrdenDeVenta").Value.ToString())
    Catch ex As Exception
        MsgBox("Error al Actualizar el Status en la Orden: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
        SQL.Cerrar()
        Exit Sub
      End Try

      'row.Cells("Accion").Value = "En revisión"
      SQL.Cerrar()
  End Sub

  Private Sub DGVLiberacionGuias_KeyDown(sender As Object, e As KeyEventArgs) Handles DGVLiberacionGuias.KeyDown
    Dim row1 As DataGridViewRow = DGVLiberacionGuias.CurrentRow()
    Dim rowFac As DataGridViewRow = DGVLiberacionGuias.CurrentRow()
    If (e.KeyCode = Keys.Enter) Then

      If row1.Cells("Facturado").Value.ToString = "Facturado" And (row1.Cells("Action").Value.ToString = "Empacado" Or row1.Cells("Action").Value.ToString = "En piso") Then
        e.SuppressKeyPress = True
        If (MessageBox.Show("Realmente desea liberar el material para su Entrega." & vbCrLf & vbCrLf & "",
                                "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
          Else
          SQLEmpleado = "UPDATE Operacion_Entrega  SET Action='Guias Generadas',horaLiberacionGuias=GETDATE() "
          SQLEmpleado &= "WHERE DocEntry = '" + row1.Cells("DocEntry").Value.ToString + "' "
          Try
              SQL.conectarTPM()
              SQL.EjecutarComando(SQLEmpleado)
              SQL.Cerrar()
            Catch ex As Exception
              MsgBox("Error al Actualizar el Status en la Orden: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
              conexion_universal.rd_s.Close() 'CIERRA EL READE
              conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
              CierraDialogAcceso = False
              Return
            End Try
            LlenaLiberacionMat()
          End If
        Else
          MsgBox("No puedes Generar las Guias hasta que este facturado o cuando este empacado")

      End If
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER

    End If
  End Sub

  Private Sub DGVLiberacionGuias_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGVLiberacionGuias.CellFormatting
    'Se definen los colores con paleta de coroles en RGB
    rojo = ColorTranslator.FromHtml("#FFC6C6")
    amarillo = ColorTranslator.FromHtml("#FDFF6C")
    verde = ColorTranslator.FromHtml("#A6FFA0")
    Anaranjado = ColorTranslator.FromHtml("#FFCC80")

    'Dependiendo del estado de la columna Estatus la fila se pintara de un cierto color y en algunos casos cambiara las letras a negritas

    For i As Integer = 0 To Me.DGVLiberacionGuias.Rows.Count - 1

      If DGVLiberacionGuias.Rows(i).Cells("Facturado").Value = "Facturado" Then

        Me.DGVLiberacionGuias.Rows(i).DefaultCellStyle.BackColor = verde

      End If
    Next

    If Me.DGVLiberacionGuias.Columns(e.ColumnIndex).Name = "Action" Then
      Dim estado As String = CStr(e.Value)
      Select Case estado
        Case "Guias Generadas"
          e.CellStyle.ForeColor = Color.Black
          e.CellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
        Case "Generando Guias"
          e.CellStyle.ForeColor = Color.Black
          e.CellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
        Case "En piso"
          e.CellStyle.ForeColor = Color.Black
          e.CellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
      End Select
    End If

  End Sub

  Private Sub DGVLiberacionGuias_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVLiberacionGuias.CellContentClick
    Dim rowC As DataGridViewRow = DGVLiberacionGuias.CurrentRow()
    Dim row1 As DataGridViewRow = DGVLiberacionGuias.CurrentRow()

    If e.RowIndex >= 0 Then
      If Me.DGVLiberacionGuias.Columns(e.ColumnIndex).Name = "Liberar" And rowC.Cells("Facturado").Value = "Facturado" And rowC.Cells("Action").Value = "Empacado" Then
        If (MessageBox.Show("Realmente desea liberar el material ." & vbCrLf & vbCrLf & "",
                            "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
        Else
          SQLEmpleado = "UPDATE Operacion_Entrega  SET Action='Guias Generadas',horaLiberacionGuias=GETDATE() "
          SQLEmpleado &= "WHERE DocEntry = '" + row1.Cells("DocEntry").Value.ToString + "' "
          Try
            'CONECTA A LA BASE DE DATOS
            conexion_universal.conectar()
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLEmpleado, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
            conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION

          Catch ex As Exception
            MsgBox("Error al Actualizar el Status en la Orden: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
            conexion_universal.rd_s.Close() 'CIERRA EL READE
            conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
            CierraDialogAcceso = False
            Return
          End Try
          LlenaLiberacionMat()
        End If
      End If
    End If
  End Sub

  Private Sub btnActualiza_Click(sender As Object, e As EventArgs) Handles btnActualiza.Click
    LlenaLiberacionMat()
  End Sub

  Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    LlenaLiberacionMat()
    GuiasLiberadas()
  End Sub

  Private Sub bUpdateLiberadas_Click(sender As Object, e As EventArgs) Handles bUpdateLiberadas.Click
    GuiasLiberadas()
  End Sub

  Private Sub dgvLiberadas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvLiberadas.CellContentClick

  End Sub

  Private Sub DGVLiberacionGuias_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVLiberacionGuias.CellDoubleClick
    'Dim row As DataGridViewRow = DGVLiberacionGuias.CurrentRow()
    'frmEmergente.DocEntry = row.Cells("DocNum").Value.ToString
    'frmEmergente.ShowDialog()
  End Sub

  Private Sub DGVLiberacionGuias_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVLiberacionGuias.CellClick

  End Sub
End Class