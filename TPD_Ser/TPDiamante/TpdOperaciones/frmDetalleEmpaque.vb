Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.IO
'LIBRERIA REQUERIDA PARA CARGAR EL CRYSTAL
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmDetalleEmpaque
  'VARIABLE DEL DATA VIEW GLOBAL A NIVEL FORMULARIO
  Dim DvDetalle_Estatus As DataView
  Dim DvDetalle_Paking As DataView
  Dim Resultadopaking As DataView
  'VARIABLES PARA BASE DE DATOS
  Dim BaseDatosTPD As String = "TPM"
  Dim BaseDatosSAP As String = "SBO_TPD"
  Dim BaseDatosTPDPru As String = "TPM09FEB2019"
  Dim BaseDatosSAPPru As String = "ZPRUEBAS16ABR2019"
  'Colores creados para uso en el grid DGVoperacionVta
  Dim rojo As Color
  Dim amarillo As Color
  Dim verde As Color
  Dim Anaranjado As Color
  Dim indice As Integer
  Dim CerrarDescartar As Boolean = False
  Dim Caj As Integer = 0
  Dim Cerrar As Boolean

  Private Sub frmDetalleEmpaque_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    txtNumCajas.Select()
    lblOrden_Entrega.Text = DocNumEmpacar
    LlenarDetalleEmpaque(DocNumEmpacar)
    Dim row As DataGridViewRow = dgvDetalleEmpaque.CurrentRow()
    lblnumcajas.Text = row.Cells("BoxTotal").Value.ToString
    txtNumCajas.Text = row.Cells("BoxTotal").Value.ToString
    txtcontenedor.Text = "Caja"
    Cerrar = False
  End Sub

  Sub insertaEntregaP(Linenum As Integer, itemcode As String, SurtidoBox As Integer, BoxNum As Integer)
    Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      SQLOrdenes = "insert into TPM09FEB2019.dbo.Operacion_Paking_List(DocEntry,LineNum,ItemCode,SurtidoBox,BoxNum) values(" + lblOrden_Entrega.Text + ", " + Linenum.ToString + ",'" + itemcode.ToString + "'," + SurtidoBox.ToString + "," + BoxNum.ToString + " )"
      cnn = New SqlConnection(StrConPru)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      cmd = New SqlCommand(SQLOrdenes, cnn)
      'CONVIERTE EL TEXTO EN CONSULTA
      cmd.CommandType = CommandType.Text
      'APERTURA LA CONEXION
      cnn.Open()
      cmd.ExecuteNonQuery()
      'CIERRA EL COMMAND DE SQL
      cmd.Connection.Close()
      'CIERRA LA CONEXION
      cnn.Close()
    Catch ex As Exception
      MsgBox("Error: " + ex.ToString)
    End Try
  End Sub
  Sub LlenarDetalleEmpaque(ORDEN As String)
    'llena a travez de una consulta de sql el gridview detalle
    Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      Dim da As SqlDataAdapter
      Dim DsOrdenes = New DataSet
      SQLOrdenes = "Select ROW_NUMBER() OVER(ORDER  BY ItmsGrpNam ASC) AS 'Partidas',ItmsGrpNam ,T0.ItemCode,T0.Description,T0.Surtido,BoxTotal,LineNum,  "
      SQLOrdenes &= "  IIF(( Select sum(SurtidoBox) from TPM09FEB2019.dbo.Operacion_Paking_List TT1 where  DocEntry=" + ORDEN + " and TT1.Linenum=t0.Linenum )=T0.Surtido,'Empacado','Empacando') as Estatus, "
      SQLOrdenes &= "(Select Top(1) Surtido From TPM09FEB2019.dbo.Operacion_Detalle_Entrega tt2 where DocEntry= " + ORDEN + " and tt2.LineNum=t0.LineNum) - IIF((Select Sum(SurtidoBox) from TPM09FEB2019.dbo.Operacion_Paking_List TT1 where DocEntry= " + ORDEN + " and TT1.Linenum=t0.Linenum)IS NULL,0,(Select Sum(SurtidoBox) from TPM09FEB2019.dbo.Operacion_Paking_List TT1 where DocEntry= " + ORDEN + " and TT1.Linenum=t0.Linenum)) as SurtidoRestante "
      SQLOrdenes &= ",t0.Box as Caja "
      SQLOrdenes &= "From TPM09FEB2019.dbo.Operacion_Detalle_Entrega T0 INNER JOIN TPM09FEB2019.dbo.Operacion_Entrega T3 on t3.DocEntry=t0.DocEntry "
      SQLOrdenes &= "LEFT JOIN ZPRUEBAS16ABR2019.dbo.OITM T1 ON T0.ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS=T1.ItemCode "
      SQLOrdenes &= "LEFT JOIN ZPRUEBAS16ABR2019.dbo.OITB T2 ON T1.ItmsGrpCod=T2.ItmsGrpCod "
      SQLOrdenes &= " Where  T0.DocEntry= " + ORDEN + " and T2.ItmsGrpCod<>150 Order by ItmsGrpNam "
      cnn = New SqlConnection(StrConPru)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      cmd = New SqlCommand(SQLOrdenes, cnn)
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
      DvDetalle_Estatus = New DataView
      'ALMACENA EN DATA SET DE MODO TABLA
      DvDetalle_Estatus.Table = DsOrdenes.Tables(0)
      'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
      dgvDetalleEmpaque.DataSource = Nothing
      'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
      dgvDetalleEmpaque.DataSource = DvDetalle_Estatus
      'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE DETALLE ORDENES
      EstiloDetalleEmpaque()
    Catch ex As Exception
      MsgBox("Error: " + ex.ToString)
    End Try
  End Sub
  Sub LlenarDetallepaking(ORDEN As String, LineNum As String)
    'llena a travez de una consulta de sql el gridview detalle
    Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      Dim da As SqlDataAdapter
      Dim DsPAKING = New DataSet
      SQLOrdenes = "Select Distinct t0.ItemCode as Codigo,Description as Descripcion,SurtidoBox As Surtido_Caja, BoxNum as Numero_Caja "
      SQLOrdenes &= ",IIF((Select SUM(SurtidoBox) From TPM09FEB2019.dbo.Operacion_Paking_List TT1 where TT1.DocEntry=T0.DocEntry and TT1.LineNum=T0.LineNum)=T1.Surtido,'Empacado','Empacando') "
      SQLOrdenes &= "AS Estado from TPM09FEB2019.dbo.Operacion_Paking_List T0 "
      SQLOrdenes &= " LEFT JOIN TPM09FEB2019.dbo.Operacion_Detalle_Entrega T1 ON T0.DocEntry=T1.DocEntry and T0.LineNum=t1.LineNum "
      SQLOrdenes &= " Where  T0.DocEntry= " + ORDEN + " AND T0.LineNum=" + LineNum + ""
      cnn = New SqlConnection(StrConPru)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      cmd = New SqlCommand(SQLOrdenes, cnn)
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
      da.Fill(DsPAKING)
      'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
      DvDetalle_Paking = New DataView
      'ALMACENA EN DATA SET DE MODO TABLA
      DvDetalle_Paking.Table = DsPAKING.Tables(0)
      'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
      dgv_Paking_Detalle.DataSource = Nothing
      'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
      dgv_Paking_Detalle.DataSource = DvDetalle_Paking
      'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE DETALLE ORDENES
      ESTILO_paking()
    Catch ex As Exception
      MsgBox("Error: " + ex.ToString)
    End Try
  End Sub
  Private Sub lblOrden_VTA_Click(sender As Object, e As EventArgs) Handles lblOrden_VTA.Click

  End Sub
  Sub EstiloDetalleEmpaque()
    'Este metodo cambia el estilo del gridview detalle 
    With Me.dgvDetalleEmpaque
      'COLOCA PROPIEDADES DE COLOR ALTERNADOS
      .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionForeColor = Color.White
      .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      'Articulo
      .Columns("Partidas").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
      .Columns("Partidas").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("Partidas").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("Partidas").HeaderText = "Partidas"
      .Columns("Partidas").Width = 65
      .Columns("Partidas").ReadOnly = True
      'LINEA
      .Columns("ItmsGrpNam").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
      .Columns("ItmsGrpNam").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("ItmsGrpNam").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("ItmsGrpNam").HeaderText = "Linea"
      .Columns("ItmsGrpNam").Width = 100
      .Columns("ItmsGrpNam").ReadOnly = True
      'Descripcion
      .Columns("ItemCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
      .Columns("ItemCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("ItemCode").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("ItemCode").HeaderText = "ItemCode"
      .Columns("ItemCode").Width = 95
      .Columns("ItemCode").ReadOnly = True
      'Cantidad
      .Columns("Description").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
      .Columns("Description").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("Description").Width = 335
      .Columns("Description").ReadOnly = True
      .Columns("Description").SortMode = DataGridViewColumnSortMode.NotSortable
      'Descripcion
      .Columns("Surtido").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("Surtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Surtido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("Surtido").Width = 70
      .Columns("Surtido").ReadOnly = True
      .Columns("Surtido").SortMode = DataGridViewColumnSortMode.NotSortable
      'Descripcion
      .Columns("BoxTotal").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("BoxTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("BoxTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("BoxTotal").Width = 80
      .Columns("BoxTotal").ReadOnly = True
      .Columns("BoxTotal").Visible = False
      'Descripcion
      .Columns("Estatus").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("Estatus").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Estatus").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("Estatus").Width = 100
      .Columns("Estatus").ReadOnly = True
      .Columns("Estatus").Visible = False
      'Descripcion
      .Columns("SurtidoRestante").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("SurtidoRestante").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("SurtidoRestante").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("SurtidoRestante").Width = 80
      .Columns("SurtidoRestante").ReadOnly = True
      .Columns("SurtidoRestante").Visible = False
      ''Line Num
      .Columns("LineNum").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("LineNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("LineNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("LineNum").Width = 80
      .Columns("LineNum").ReadOnly = True
      .Columns("LineNum").Visible = False
      'Line Num
      .Columns("Caja").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("Caja").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Caja").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
      .Columns("Caja").Width = 80
      .Columns("Caja").ReadOnly = False
      .Columns("Caja").Visible = True
    End With
  End Sub
  Sub ESTILO_paking()
    'Este metodo cambia el estilo del gridview detalle 
    With Me.dgv_Paking_Detalle
      'COLOCA PROPIEDADES DE COLOR ALTERNADOS
      .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionForeColor = Color.White
      .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      'Articulo
      .Columns("Codigo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
      .Columns("Codigo").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("Codigo").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("Codigo").HeaderText = "Codigo"
      .Columns("Codigo").Width = 75
      .Columns("Codigo").ReadOnly = True
      'LINEA
      .Columns("Descripcion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
      .Columns("Descripcion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("Descripcion").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("Descripcion").HeaderText = "Descripcion"
      .Columns("Descripcion").Width = 350
      .Columns("Descripcion").ReadOnly = True
      'Descripcion
      .Columns("Surtido_Caja").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
      .Columns("Surtido_Caja").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("Surtido_Caja").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("Surtido_Caja").HeaderText = "Surtido "
      .Columns("Surtido_Caja").Width = 95
      .Columns("Surtido_Caja").ReadOnly = True
      'Descripcion
      .Columns("Numero_Caja").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
      .Columns("Numero_Caja").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("Numero_Caja").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("Numero_Caja").HeaderText = "Numero Caja"
      .Columns("Numero_Caja").Width = 115
      .Columns("Numero_Caja").ReadOnly = True
      'TOTAL Total
      .Columns("Estado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
      .Columns("Estado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
      .Columns("Estado").SortMode = DataGridViewColumnSortMode.NotSortable
      .Columns("Estado").HeaderText = "Estado"
      .Columns("Estado").Width = 115
      .Columns("Estado").Visible = False
    End With
  End Sub


  Private Sub dgvDetalleEmpaque_SelectionChanged(sender As Object, e As EventArgs) Handles dgvDetalleEmpaque.SelectionChanged
    Try
      Dim row As DataGridViewRow = dgvDetalleEmpaque.CurrentRow()
      LlenarDetallepaking(lblOrden_Entrega.Text, row.Cells("LineNum").Value)
      lblproducto.Text = row.Cells("Description").Value.ToString
      lblcantidad.Text = row.Cells("SurtidoRestante").Value.ToString

      If row.Cells("SurtidoRestante").Value.ToString = "0" Then

        txtcantidad.Enabled = False
      Else
        txtcantidad.Enabled = True

      End If
      txtcantidad.Text = row.Cells("SurtidoRestante").Value.ToString

    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try
  End Sub
  Private Sub txtNumCajas_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNumCajas.KeyPress

    'OBTIENE EL CARACTER INTRODUCIDO
    Dim caracter As Char = e.KeyChar
    'COMPRUBA SI EL CARACTER ES UN NÚMERO O EL RETROCESO
    If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False Then
      'SI EL CARACTER ES DIFERENTE A NULO ENTRA
      If (caracter = ChrW(Keys.Enter)) = True Then
        'OBTIENE EL CARACTER INTRODUCIDO
        e.Handled = True
        SendKeys.Send("{TAB}")
      ElseIf (caracter <> vbNullChar) Then
        MsgBox("Solo se permiten Números.", MsgBoxStyle.Exclamation, "Alerta de caracter")
      End If
      'COLOCA EL VALOR EN EL DIGITO QUE ESTABA
      e.KeyChar = Chr(0)
    Else

    End If
  End Sub
  Private Sub txtNumCajas_Leave(sender As Object, e As EventArgs) Handles txtNumCajas.Leave
    Me.dgvDetalleEmpaque.CurrentCell =
    Me.dgvDetalleEmpaque("Caja", dgvDetalleEmpaque.CurrentRow.Index)

    txtNumCajas.Enabled = False
    Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      SQLOrdenes = "Update TPM09FEB2019.dbo.Operacion_Entrega SET BoxReal='" + txtNumCajas.Text + "' WHERE DocNum=" + DocNumSurtido + " and DocEntry=" + lblOrden_Entrega.Text + ""
      cnn = New SqlConnection(StrConPru)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      cmd = New SqlCommand(SQLOrdenes, cnn)
      'CONVIERTE EL TEXTO EN CONSULTA
      cmd.CommandType = CommandType.Text
      'APERTURA LA CONEXION
      cnn.Open()
      cmd.ExecuteNonQuery()
      'CIERRA EL COMMAND DE SQL
      cmd.Connection.Close()
      'CIERRA LA CONEXION
      cnn.Close()
    Catch ex As Exception
      MsgBox("Error:al insertar contenedor " + ex.ToString)
    End Try
    'OBTIENE EL CARACTER INTRODUCIDO       

    Me.dgvDetalleEmpaque.Select()
  End Sub

  Private Sub txtcantidad_Leave(sender As Object, e As EventArgs) Handles txtcantidad.Leave
    Dim row As DataGridViewRow = dgvDetalleEmpaque.CurrentRow()
    If txtcantidad.Text <> "" Then
      If txtcantidad.Text > row.Cells("Surtido").Value Or txtcantidad.Text < 0 Then
        MsgBox("No puedes colocar un numero mayor o menor al Surtido.", MsgBoxStyle.Exclamation, "Alerta de caracter")
        If lblcantidad.Text = 0 Then
          Me.dgvDetalleEmpaque.CurrentCell = Me.dgvDetalleEmpaque("Caja", dgvDetalleEmpaque.CurrentRow.Index)
          Me.dgvDetalleEmpaque.Select()
        Else
          txtcajas.Select()

        End If
      End If
    End If
  End Sub

  Private Sub txtcantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcantidad.KeyPress
    Dim row As DataGridViewRow = dgvDetalleEmpaque.CurrentRow()
    'OBTIENE EL CARACTER INTRODUCIDO
    Dim caracter As Char = e.KeyChar
    'COMPRUBA SI EL CARACTER ES UN NÚMERO O EL RETROCESO
    If (caracter = ChrW(Keys.Enter)) = True Then


      insertaEntregaP(row.Cells("LineNum").Value, row.Cells("ItemCode").Value, txtcantidad.Text, txtcajas.Text)
      LlenarDetallepaking(lblOrden_Entrega.Text, row.Cells("LineNum").Value)
      lblcantidad.Text = (lblcantidad.Text - txtcantidad.Text)
      If lblcantidad.Text = 0 Then
        Me.dgvDetalleEmpaque.CurrentCell = Me.dgvDetalleEmpaque("Caja", indice)
        e.Handled = True
        Me.dgvDetalleEmpaque.Select()
      Else
        txtcajas.Select()
      End If
    ElseIf Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False Then
      'SI EL CARACTER ES DIFERENTE A NULO ENTRA
      If (caracter <> vbNullChar) Then
        MsgBox("Solo se permiten Números.", MsgBoxStyle.Exclamation, "Alerta de caracter")
      End If
    End If
  End Sub

  Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
    Dim row As DataGridViewRow = dgvDetalleEmpaque.CurrentRow()

    For i As Integer = 0 To Me.dgvDetalleEmpaque.Rows.Count - 1

      If dgvDetalleEmpaque.Rows(i).Cells("Caja").Value = "Varias" Then

      Else
        'ACTUALIZA EL ESTATUS A EMPACADO
        Try
          'VARIABLE DE CADENA DE SQL
          Dim SQLOrdenes As String
          'VARIABLES DE CONEXION DE LLENADO
          Dim cmd As SqlCommand
          Dim cnn As SqlConnection = Nothing
          SQLOrdenes = "Insert into TPM09FEB2019.dbo.Operacion_Paking_List(DocEntry,LineNum,ItemCode,SurtidoBox,Boxnum) values( "
          SQLOrdenes &= "" + lblOrden_Entrega.Text + "," + dgvDetalleEmpaque.Rows(i).Cells("LineNum").Value.ToString + ",'" + dgvDetalleEmpaque.Rows(i).Cells("ItemCode").Value.ToString + "'," + dgvDetalleEmpaque.Rows(i).Cells("Surtido").Value.ToString + "," + dgvDetalleEmpaque.Rows(i).Cells("Caja").Value.ToString + ") "
          cnn = New SqlConnection(StrConPru)
          'ALMACENA LA CONSULTA EN UN COMMAND SQL
          cmd = New SqlCommand(SQLOrdenes, cnn)
          'CONVIERTE EL TEXTO EN CONSULTA
          cmd.CommandType = CommandType.Text
          'APERTURA LA CONEXION
          cnn.Open()
          cmd.ExecuteNonQuery()
          'CIERRA EL COMMAND DE SQL
          cmd.Connection.Close()
          'CIERRA LA CONEXION
          cnn.Close()
        Catch ex As Exception
          MsgBox("Error: Ocurrio un Error al Terminar el Empacado " + ex.ToString)
        End Try
      End If
    Next
    MsgBox("El empacado Termino Exitosamente")
    'ACTUALIZA EL ESTATUS A EMPACADO
    Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      SQLOrdenes = "Update TPM09FEB2019.dbo.Operacion_Entrega SET Status='EMP', Action='Empacado' WHERE DocNum=" + DocNumSurtido + " and DocEntry=" + lblOrden_Entrega.Text + ""
      cnn = New SqlConnection(StrConPru)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      cmd = New SqlCommand(SQLOrdenes, cnn)
      'CONVIERTE EL TEXTO EN CONSULTA
      cmd.CommandType = CommandType.Text
      'APERTURA LA CONEXION
      cnn.Open()
      cmd.ExecuteNonQuery()
      'CIERRA EL COMMAND DE SQL
      cmd.Connection.Close()
      'CIERRA LA CONEXION
      cnn.Close()
    Catch ex As Exception
      MsgBox("Error: Ocurrio un Error al Actualizar el estatus a Empacado " + ex.ToString)
    End Try
    'DESCOMENTAR CUANDO SE PASE A PRODUCTIVO
    'Try
    '    'VARIABLE DE CADENA DE SQL
    '    Dim SQLOrdenes As String
    '    'VARIABLES DE CONEXION DE LLENADO
    '    Dim cmd As SqlCommand
    '    Dim cnn As SqlConnection = Nothing
    '    SQLOrdenes = "Update Operacion_Orden  SET Status='EMP', Action='Empacado' WHERE DocNum=" + DocNumSurtido + ""
    '    cnn = New SqlConnection(StrConPru)
    '    'ALMACENA LA CONSULTA EN UN COMMAND SQL
    '    cmd = New SqlCommand(SQLOrdenes, cnn)
    '    'CONVIERTE EL TEXTO EN CONSULTA
    '    cmd.CommandType = CommandType.Text
    '    'APERTURA LA CONEXION
    '    cnn.Open()
    '    cmd.ExecuteNonQuery()
    '    'CIERRA EL COMMAND DE SQL
    '    cmd.Connection.Close()
    '    'CIERRA LA CONEXION
    '    cnn.Close()
    'Catch ex As Exception
    '    MsgBox("Error: Ocurrio un Error al Actualizar el estatus a Empacado " + ex.ToString)
    'End Try
    MImprimeFormato(lblOrden_Entrega.Text)
    MsgBox("El empaque se termino con Exito.", MsgBoxStyle.Information, "Termino Empaque")
    Cerrar = True
    Me.Close()
  End Sub
  Sub MImprimeFormato(paking As String)

    'VARIABLE DE CADENA DE SQL
    Dim SQLOrdenes As String
    'VARIABLES DE CONEXION DE LLENADO
    Dim cmd As SqlCommand
    Dim cnn As SqlConnection = Nothing
    Dim DsOrdenes = New DataSet
    Dim da As SqlDataAdapter
    'ALAMACENA LA CONSULTA
    SQLOrdenes = "Select Distinct ItmsGrpNam ,T5.DocNum,t5.CardCode,T5.CardName,T5.DocDate,t5.LicTradNum , t5.Address, SlpName, T1.BaseRef, "
    SQLOrdenes &= "T1.ItemCode AS Codigo,Dscription AS Descripcion,(Select Surtido From  TPM09FEB2019.dbo.Operacion_Detalle_Entrega where T1.VisOrder=LineNum and DocEntry=t2.DocEntry )AS Cantidad, "
    SQLOrdenes &= "Round(t3.SWeight1,2,0) as PesoxUni ,ROUND(((Select Surtido From  TPM09FEB2019.dbo.Operacion_Detalle_Entrega where T1.VisOrder=LineNum and DocEntry=t2.DocEntry )*t3.SWeight1),2,1)as PesoxArt,t7.TrnspName  "
    'SQLOrdenes &= " , (((Select SurtidoBox From  TPM09FEB2019.dbo.Operacion_Paking_List where T1.VisOrder=LineNum and DocEntry=t2.DocEntry)*t3.SWeight1)*1.03) as PESO"
    SQLOrdenes &= ",(Select Name From TPM09FEB2019.dbo.Operacion_Empleado TT1 INNER JOIN TPM09FEB2019.DBO.Operacion_Entrega TT2 on TT2.UserId_Surtido =TT1.KeyCode where tt2.DocEntry=" + paking + ") AS Surtidor "
    SQLOrdenes &= ",(Select Name From TPM09FEB2019.dbo.Operacion_Empleado TT1 INNER JOIN TPM09FEB2019.DBO.Operacion_Entrega TT2 on TT2.UserId_Empacado =TT1.KeyCode where tt2.DocEntry=" + paking + ") AS Empacador "
    SQLOrdenes &= "  ,T9.SurtidoBox as Surtido_Caja,BoxNum as Caja "
    SQLOrdenes &= " From DLN1 T1 LEFT JOIN TPM09FEB2019.dbo.Operacion_Detalle_Entrega T2 ON T1.DocEntry=T2.DocEntry "
    SQLOrdenes &= "LEFT JOIN OITM T3 on T1.ItemCode=T3.ItemCode   LEFT JOIN OITB T4 on T3.ItmsGrpCod=T4.ItmsGrpCod  LEFT JOIN ODLN t5 ON T1.DocEntry=T5.DocEntry  "
    SQLOrdenes &= " LEFT JOIN OSLP T6 ON T1.SlpCode=T6.SlpCode LEFT JOIN OSHP T7 ON T7.TrnspCode=t5.TrnspCode "
    SQLOrdenes &= "  LEFT Join TPM09FEB2019.dbo.Operacion_Entrega  T8 ON T8.DocEntry=T2.DocEntry AND T8.DocNum=T2.DocNum "
    SQLOrdenes &= "  INNER JOIN TPM09FEB2019.DBO.Operacion_Paking_List T9 ON T1.DocEntry=T9.DocEntry  AND T1.ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS=T9.ItemCode "
    SQLOrdenes &= " LEFT JOIN ODLN T10 ON T1.DocEntry=T10.DocEntry"
    SQLOrdenes &= " WHERE  T2.Docentry=" + paking + " AND T4.ItmsGrpCod<>150  Order By BoxNum, ItmsGrpNam ,T1.ItemCode"
    cnn = New SqlConnection(StrConPru)
    'ALMACENA LA CONSULTA EN UN COMMAND SQL
    cmd = New SqlCommand(SQLOrdenes, cnn)
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
    Resultadopaking = New DataView
    'ALMACENA EN DATA SET DE MODO TABLA
    Resultadopaking.Table = DsOrdenes.Tables(0)
    'Crea un nuevo DataView         
    DvDetalle_Estatus = New DataView
    'ALMACENA EN DATA SET DE MODO TABLA
    DvDetalle_Estatus.Table = DsOrdenes.Tables(0)
    'Se crea un informe de cristal Reports
    Dim informe As New CR_Paking_List
    RepComsultaP.MdiParent = Inicio
    informe.SetDataSource(DvDetalle_Estatus)
    RepComsultaP.CrVConsulta.ReportSource = informe
    informe.PrintToPrinter(1, False, 0, 0)

  End Sub

  Private Sub dgvDetalleEmpaque_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvDetalleEmpaque.CellFormatting
    'Se definen los colores con paleta de coroles en RGB
    rojo = ColorTranslator.FromHtml("#FFC6C6")
    amarillo = ColorTranslator.FromHtml("#FDFF6C")
    verde = ColorTranslator.FromHtml("#A6FFA0")
    Anaranjado = ColorTranslator.FromHtml("#FFCC80")
    'Dependiendo del estado de la columna Estatus la fila se pintara de un cierto color y en algunos casos cambiara las letras a negritas
    For i As Integer = 0 To Me.dgvDetalleEmpaque.Rows.Count - 1
      If dgvDetalleEmpaque.Rows(i).Cells("Caja").Value Is DBNull.Value Then
        Me.dgvDetalleEmpaque.Rows(i).DefaultCellStyle.BackColor = Color.Empty
      ElseIf Me.dgvDetalleEmpaque.Rows(i).Cells("Caja").Value = "Varias" And lblcantidad.Text <> 0 Then
        Me.dgvDetalleEmpaque.Rows(i).DefaultCellStyle.BackColor = amarillo
      ElseIf Me.dgvDetalleEmpaque.Rows(i).Cells("Caja").Value <> "" Then
        Me.dgvDetalleEmpaque.Rows(i).DefaultCellStyle.BackColor = verde
      ElseIf Me.dgvDetalleEmpaque.Rows(i).Cells("Caja").Value = "Varias" And lblcantidad.Text = 0 Then
        Me.dgvDetalleEmpaque.Rows(i).DefaultCellStyle.BackColor = verde
      End If
    Next
  End Sub

  Private Sub dgvDetalleEmpaque_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetalleEmpaque.CellContentClick

  End Sub

  Private Sub btnTerminoEmpaque_Click(sender As Object, e As EventArgs) Handles btnTerminoEmpaque.Click
    If (CerrarDescartar = False) Then
      If (MessageBox.Show("Al cerrar la ventana, los datos modificados se perderán." & vbCrLf & vbCrLf & "¿Esta seguro que desea cerrar la ventana?",
                            "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
      Else
        'Elimina los registros creados en Operacion_Paking_List
        Try
          'VARIABLE DE CADENA DE SQL
          Dim SQLOrdenes As String
          'VARIABLES DE CONEXION DE LLENADO
          Dim cmd As SqlCommand
          Dim cnn As SqlConnection = Nothing
          SQLOrdenes = "Delete TPM09FEB2019.dbo.Operacion_Paking_List  where DocEntry=" + lblOrden_Entrega.Text + " "
          cnn = New SqlConnection(StrConPru)
          'ALMACENA LA CONSULTA EN UN COMMAND SQL
          cmd = New SqlCommand(SQLOrdenes, cnn)
          'CONVIERTE EL TEXTO EN CONSULTA
          cmd.CommandType = CommandType.Text
          'APERTURA LA CONEXION
          cnn.Open()
          cmd.ExecuteNonQuery()
          'CIERRA EL COMMAND DE SQL
          cmd.Connection.Close()
          'CIERRA LA CONEXION
          cnn.Close()
        Catch ex As Exception
          MsgBox("Error: Ocurrio un Error al Descartar el Empacado " + ex.ToString)
        End Try
      End If
    End If
    Me.Close()
  End Sub

  Private Sub frmDetalleEmpaque_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    'INSTANCIA OBJECTO DE TIPO FORMULARIO DE MOSTRAR ORDENES PARA REFRESCARLO
    Dim Form As frmEstatusEmpaque = Application.OpenForms.OfType(Of frmEstatusEmpaque)().FirstOrDefault()
    'VALIDA SI ENCUENTRA LA INSTANCIA (FORMULARIO) ABIERTA        
    If (Form IsNot Nothing) Then
      Form.Refresh()
      'ACTIVA EL FORMULARIO DE MOSTRAR ORDENES
      Form.Activate()
      'EJECUTA LOS METODOS DEL FORMULARIO DE MOSTRAR ORDENES
      Form.MEjecuta_Full_Empaque()
      'Form.MEjecuta_Entrega()
      'Form.MEjecuta_Entrega_Ped()
      'Form.MEjecuta_Entrega_Ped_Det()
      Form.LlenarEmpaque()
      'REFRESCA EL FORMULARIO
      Form.Refresh()
    End If
  End Sub

  Private Sub txtcontenedor_Leave(sender As Object, e As EventArgs) Handles txtcontenedor.Leave
    Me.dgvDetalleEmpaque.CurrentCell =
    Me.dgvDetalleEmpaque("Caja", dgvDetalleEmpaque.CurrentRow.Index)
    'ACTUALIZA EL ESTATUS A EMPACADO
    Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      SQLOrdenes = "Update TPM09FEB2019.dbo.Operacion_Entrega SET BoxName='" + txtcontenedor.Text + "' WHERE DocNum=" + DocNumSurtido + " and DocEntry=" + lblOrden_Entrega.Text + ""
      cnn = New SqlConnection(StrConPru)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      cmd = New SqlCommand(SQLOrdenes, cnn)
      'CONVIERTE EL TEXTO EN CONSULTA
      cmd.CommandType = CommandType.Text
      'APERTURA LA CONEXION
      cnn.Open()
      cmd.ExecuteNonQuery()
      'CIERRA EL COMMAND DE SQL
      cmd.Connection.Close()
      'CIERRA LA CONEXION
      cnn.Close()
    Catch ex As Exception
      MsgBox("Error:al insertar contenedor " + ex.ToString)
    End Try
  End Sub
  Private Sub dgvDetalleEmpaque_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetalleEmpaque.CellContentDoubleClick

  End Sub

  Private Sub dgvDetalleEmpaque_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvDetalleEmpaque.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      dgvDetalleEmpaque.Rows(dgvDetalleEmpaque.CurrentRow.Index).Cells("Caja").Value = "Varias"
      indice = dgvDetalleEmpaque.CurrentRow.Index
      txtcajas.Select()
    End If

  End Sub

  Private Sub txtcajas_Leave(sender As Object, e As EventArgs) Handles txtcajas.Leave
    txtcantidad.Select()

  End Sub

  Private Sub frmDetalleEmpaque_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    If (Cerrar = False) Then
      If (MessageBox.Show("Al cerrar la ventana, los datos modificados se perderán." & vbCrLf & vbCrLf & "¿Esta seguro que desea cerrar la ventana?",
                            "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
        e.Cancel = True
      Else
        'Elimina los registros creados en Operacion_Paking_List
        Try
          'VARIABLE DE CADENA DE SQL
          Dim SQLOrdenes As String
          'VARIABLES DE CONEXION DE LLENADO
          Dim cmd As SqlCommand
          Dim cnn As SqlConnection = Nothing
          SQLOrdenes = "Delete TPM09FEB2019.dbo.Operacion_Paking_List  where DocEntry=" + lblOrden_Entrega.Text + " "
          cnn = New SqlConnection(StrConPru)
          'ALMACENA LA CONSULTA EN UN COMMAND SQL
          cmd = New SqlCommand(SQLOrdenes, cnn)
          'CONVIERTE EL TEXTO EN CONSULTA
          cmd.CommandType = CommandType.Text
          'APERTURA LA CONEXION
          cnn.Open()
          cmd.ExecuteNonQuery()
          'CIERRA EL COMMAND DE SQL
          cmd.Connection.Close()
          'CIERRA LA CONEXION
          cnn.Close()
        Catch ex As Exception
          MsgBox("Error: Ocurrio un Error al Descartar el Empacado " + ex.ToString)
        End Try
      End If
    End If
  End Sub

  Private Sub txtcantidad_TextChanged(sender As Object, e As EventArgs) Handles txtcantidad.TextChanged

  End Sub

  Private Sub txtcontenedor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcontenedor.KeyPress
    'OBTIENE EL CARACTER INTRODUCIDO
    Dim caracter As Char = e.KeyChar
    If (caracter = ChrW(Keys.Enter)) = True Then
      e.Handled = True
      SendKeys.Send("{TAB}")
    End If

  End Sub

  Private Sub dgvDetalleEmpaque_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetalleEmpaque.CellEndEdit
    Dim row As DataGridViewRow = dgvDetalleEmpaque.CurrentRow
    Try
      If CStr(row.Cells("Caja").Value) > txtNumCajas.Text And row.Cells("Caja").Value <> "Varias" Then
        If (MessageBox.Show("No puedes poner un numero mayor de cajas al que as puesto anteriormente o vacio " & vbCrLf & vbCrLf & "",
                              "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
          dgvDetalleEmpaque.Rows(dgvDetalleEmpaque.CurrentRow.Index).Cells("Caja").Value = Nothing
        Else
          dgvDetalleEmpaque.Rows(dgvDetalleEmpaque.CurrentRow.Index).Cells("Caja").Value = Nothing
        End If
      ElseIf row.Cells("Caja").Value = "Varias" Then
        dgvDetalleEmpaque.Item("Caja", dgvDetalleEmpaque.CurrentRow.Index).ReadOnly = True

      End If
    Catch
    End Try
  End Sub

  Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

  End Sub

  Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
    Try
      Dim row As DataGridViewRow = dgvDetalleEmpaque.CurrentRow()
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      Dim da As SqlDataAdapter
      Dim DsPAKING = New DataSet
      SQLOrdenes = "Delete TPM09FEB2019.dbo.Operacion_Paking_List where DocEntry=" + lblOrden_Entrega.Text + " and LineNum=" + row.Cells("LineNum").Value.ToString + " "
      cnn = New SqlConnection(StrConPru)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      cmd = New SqlCommand(SQLOrdenes, cnn)
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
      MsgBox("Se a descartado la partida", MsgBoxStyle.Information, "")
    Catch ex As Exception
      MsgBox("Error: Ocurrio un Error al actualizar " + ex.ToString)
    End Try

  End Sub

  Private Sub txtcajas_TextChanged(sender As Object, e As EventArgs) Handles txtcajas.TextChanged

  End Sub

  Private Sub txtcajas_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcajas.KeyDown
    If (e.KeyCode = Keys.Enter) Then
      ''QUITA EL SALTO DE FILA SI ES PRESIONADA EL ENTER
      e.SuppressKeyPress = True
      txtcantidad.Select()
    End If
  End Sub

  Private Sub dgvDetalleEmpaque_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetalleEmpaque.CellLeave
    indice = dgvDetalleEmpaque.CurrentRow.Index
  End Sub

  Private Sub txtcontenedor_TextChanged(sender As Object, e As EventArgs) Handles txtcontenedor.TextChanged

  End Sub
End Class