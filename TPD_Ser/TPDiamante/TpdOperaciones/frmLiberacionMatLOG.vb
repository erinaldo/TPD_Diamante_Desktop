Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading

Public Class frmLiberacionMatLOG
  Dim DTVliberacion As DataView
  Dim col1 As New DataGridViewButtonColumn
  Dim SQLEmpleado As String
  'Colores creados para uso en el grid DGVoperacionVta
  Dim rojo As Color
  Dim amarillo As Color
  Dim verde As Color
  Dim Anaranjado As Color


  Private Sub frmLiberacionMatLOG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    LlenaLiberacionMat()
    MaterialLiberado()
    'SE ASIGNA EL TIEMPO DE RECARGA DEL GRID EN MILISEGUNDOS
    Timer1.Interval = 90000
    'Activamos el evento timer para que el grid se actualize solo 
    Timer1.Enabled = True
    SQL.conectarTPM()
  End Sub
  Sub LlenaLiberacionMat()
    'llena a travez de una consulta de sql el gridview detalle
    Try
      If DgvLiberacion.RowCount > 0 Then
        'dgvOrdenes.Rows.Clear()
        DgvLiberacion.Columns().Remove("Liberar")
        DgvLiberacion.DataSource = Nothing
      End If
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes_aut As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd1 As SqlCommand
      Dim cnn1 As SqlConnection = Nothing
      Dim da1 As SqlDataAdapter
      Dim DsLiberacion = New DataSet
      'ALAMACENA LA CONSULTA
      'SQLOrdenes_aut = "Select t0.DocEntry , FORMAT(t0.PrintTime, 'yyyy-MM-dd') as PrintTime , "
      'SQLOrdenes_aut &= "CASE WHEN Len(DocTime)=4 THEN Stuff(DocTime,3,0,':') WHEN LEN(DocTime) =3 THEN Stuff(DocTime,2,0,':') END AS horaCreacion "
      'SQLOrdenes_aut &= ",t0.DocNum,t1.CardCode,t1.CardName, T0.BoxTotal, IIF(t0.Action='Empacado','Empacado',T0.Action) as Action ,t3.TrnspName "
      'SQLOrdenes_aut &= ",(SELECT  count(T11.LineNum) from SBO_TPD.dbo.DLN1 T11 LEFT JOIN SBO_TPD.dbo.OITM T22 ON T11.ItemCode=T22.ItemCode "
      'SQLOrdenes_aut &= "LEFT JOIN SBO_TPD.dbo.OITB T33 ON T22.ItmsGrpCod=T33.ItmsGrpCod WHERE T11.DocEntry=T0.DocEntry AND T22.ItmsGrpCod=150) as Fletes "
      'SQLOrdenes_aut &= " ,Address,Comments,IIF((select round(SUM(LineTotal)*0.16+SUM(LineTotal),2) from SBO_TPD.dbo.INV1 WHERE BaseEntry=t0.Docentry AND BaseType =15)=( sELECT DocTotal FROM SBO_TPD.dbo.ODLN p22 where p22.DocEntry=T0.DocEntry),'Facturado','No Facturado') as Facturado "
      'SQLOrdenes_aut &= " from Operacion_Entrega T0 LEFT JOIN SBO_TPD.dbo.ODLN T1 ON T1.DocEntry=T0.DocEntry "
      'SQLOrdenes_aut &= "LEFT JOIN Operacion_Orden T2 ON T0.DocNum=T2.DocNum LEFT JOIN SBO_TPD.dbo.OShP T3 ON T1.TrnspCode =t3.TrnspCode "
      'SQLOrdenes_aut &= "where  t0.Action='Empacado' or t0.Action='Generando Guias' ORDER BY t0.DocEntry DESC"
      cnn1 = New SqlConnection(StrTpm)
      'ALMACENA LA CONSULTA EN UN COMMAND SQL
      'Modifico Ivan Gonzalez se volvio un procedimietno almacenado
      cmd1 = New SqlCommand("TPD_Liberacion_Material", cnn1)
      'CONVIERTE EL TEXTO EN CONSULTA
      cmd1.CommandType = CommandType.StoredProcedure
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
      DTVliberacion = New DataView
      'ALMACENA EN DATA SET DE MODO TABLA
      DTVliberacion.Table = DsLiberacion.Tables(0)
      'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
      'DgvAutorizaciones.DataSource = Nothing
      'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
      DgvLiberacion.DataSource = DTVliberacion
      'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW
      MEstiloGridOrdenes()
      'VALIDA SI EL DATATABLE TRAE DATOS
      If (DTVliberacion.Count > 0) Then
        'CREA INSTANCIA DE COLUMNA CON LINK Y DA FORMATO EN ACCION
        col1.DataPropertyName = "Liberar" 'COLOCA LA PROPIEDAD
        col1.HeaderText = "Liberar" 'COLOCA EL ENCABEZADO
        col1.Name = "Liberar" 'COLOCA EL NOMBRE DE LA COLUMNA
        col1.Text = "Liberar" 'COLOCA EL TEXTO EN EL BOTON
        col1.UseColumnTextForButtonValue = True 'PERMITE QUE SE VISUALICE EL TEXTO EN EL BOTON
        col1.SortMode = DataGridViewColumnSortMode.Automatic
        col1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 'CENTRA EL CONTENIDO
        col1.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular) 'FORMATO DE LETRA
        col1.Width = 55 'ANCHO DE LA CELDA
        col1.ReadOnly = True 'QUE SOLO SEA DE LECTURA
        'COLOCA LA COLUMNA DE IMPRESION DE MANERA BOTON, EL 9 ES LA POSICIÓN EN DONDE APARECE EN EL DATA GRID, DE 
        'RECORRERSE O AGREGAR UN CAMPO MAS, ESTE NUMERO DEBERA INCREMETARSE O DISMINUIR SEGUN EL CASO.
        DgvLiberacion.Columns.Insert(13, col1)
      End If
      'ACTUALIZA LA ORDEN DE VENTA QUE SE VA A SURTIR, EN AL CUAL AGREGA EL STATUS Y QUE EMPLEADO LO SURTE          

    Catch ex As Exception
      MsgBox("Error: " + ex.ToString)
    End Try
  End Sub

  'MODIFICADO POR IVAN GONZALEZ
  Dim SQL As New Comandos_SQL()
  Sub MaterialLiberado()
    Dim SQLLiberadas As String = ""
    Dim tbLiberadas As New DataTable
    'SQLLiberadas = "Select t0.DocEntry , FORMAT(t0.PrintTime, 'yyyy-MM-dd') as PrintTime , "
    'SQLLiberadas &= "CASE WHEN Len(DocTime)=4 THEN Stuff(DocTime,3,0,':') WHEN LEN(DocTime) =3 THEN Stuff(DocTime,2,0,':') END AS horaCreacion "
    'SQLLiberadas &= ",t0.DocNum,t1.CardCode,t1.CardName, T0.BoxTotal, IIF(t0.Action='Empacado','Empacado',T0.Action) as Action ,t3.TrnspName "
    'SQLLiberadas &= ",(SELECT  count(T11.LineNum) from SBO_TPD.dbo.DLN1 T11 LEFT JOIN SBO_TPD.dbo.OITM T22 ON T11.ItemCode=T22.ItemCode "
    'SQLLiberadas &= "LEFT JOIN SBO_TPD.dbo.OITB T33 ON T22.ItmsGrpCod=T33.ItmsGrpCod WHERE T11.DocEntry=T0.DocEntry AND T22.ItmsGrpCod=150) as Fletes "
    'SQLLiberadas &= " ,Address,Comments,IIF((select round(SUM(LineTotal)*0.16+SUM(LineTotal),2) from SBO_TPD.dbo.INV1 WHERE BaseEntry=t0.Docentry AND BaseType =15)=( sELECT DocTotal FROM SBO_TPD.dbo.ODLN p22 where p22.DocEntry=T0.DocEntry),'Facturado','No Facturado') as Facturado "
    'SQLLiberadas &= " from Operacion_Entrega T0 LEFT JOIN SBO_TPD.dbo.ODLN T1 ON T1.DocEntry=T0.DocEntry "
    'SQLLiberadas &= "LEFT JOIN Operacion_Orden T2 ON T0.DocNum=T2.DocNum LEFT JOIN SBO_TPD.dbo.OShP T3 ON T1.TrnspCode =t3.TrnspCode "
    'SQLLiberadas &= "where  t0.Action='En piso' or t0.Action='Guias Generadas' ORDER BY t0.DocEntry DESC"

    Try
      SQL.conectarTPM()
      If dgvLiberadas.Rows.Count <> 0 Then
        dt = CType(Me.dgvLiberadas.DataSource, DataTable)
        dt.Rows.Clear()
      End If
      tbLiberadas = SQL.ConsultarTabla("TPD_Material_Liberado")
      dgvLiberadas.DataSource = tbLiberadas
      EstiloLiberadas()
      SQL.Cerrar()
    Catch ex As Exception
      'MsgBox("Error: " + ex.ToString())
      SQL.Cerrar()
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
      .Columns("BoxTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("BoxTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("BoxTotal").HeaderText = "Cajas"
      .Columns("BoxTotal").Width = 50
      .Columns("BoxTotal").ReadOnly = True
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
      'IMPRIMIR PACKLIST
      .Columns("Facturado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Facturado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Facturado").HeaderText = "Facturado"
      .Columns("Facturado").Width = 110
      .Columns("Facturado").ReadOnly = True
    End With
  End Sub
  '-------------------------------

  Sub MEstiloGridOrdenes()
    'ESTILOS POR COLUMNA
    With Me.DgvLiberacion
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
      .Columns("BoxTotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("BoxTotal").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("BoxTotal").HeaderText = "Cajas"
      .Columns("BoxTotal").Width = 50
      .Columns("BoxTotal").ReadOnly = True
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
      'IMPRIMIR PACKLIST
      .Columns("Facturado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Facturado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
      .Columns("Facturado").HeaderText = "Facturado"
      .Columns("Facturado").Width = 110
      .Columns("Facturado").ReadOnly = True
    End With
  End Sub

  Private Sub BtnActualizar_Click(sender As Object, e As EventArgs) Handles BtnActualizar.Click
    LlenaLiberacionMat()
  End Sub

  Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    LlenaLiberacionMat()
    MaterialLiberado()
  End Sub

  Private Sub DgvLiberacion_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvLiberacion.CellContentClick
    Dim rowC As DataGridViewRow = DgvLiberacion.CurrentRow()

    If e.RowIndex >= 0 Then
      If Me.DgvLiberacion.Columns(e.ColumnIndex).Name <> "Liberar" Then
      Else
        If Me.DgvLiberacion.Columns(e.ColumnIndex).Name = "Liberar" And rowC.Cells("Facturado").Value = "Facturado" Then

          Dim DocEntry As String = rowC.Cells("DocEntry").Value

          If (MessageBox.Show("Realmente desea liberar el material ." & vbCrLf & vbCrLf & "",
                                "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
          Else
            '-----
            'VALIDA QUE LA CELDA QUE SE ACTIVA DEBE DE SER LA DE ACCIÓN

            Dim row As DataGridViewRow = DgvLiberacion.CurrentRow()
            Dim row1 As DataGridViewRow = DgvLiberacion.CurrentRow()

            If row.Cells("action").Value = "Empacado" Then

              Dim ValidaUsr As Form = frmValidaAcceso
              ValidaUsr.ShowDialog()

              If ValidaUsuario.Id_Empleado > 0 Then
                                SQLEmpleado = "UPDATE Operacion_Entrega SET Action='En piso', horaLiberacionMaterial = GETDATE(), UserId_Libero = '" & ValidaUsuario.KeyCode & "' "
                            Else
                Exit Sub
              End If
            End If
            SQLEmpleado &= " WHERE DocEntry = '" + DocEntry + "' "
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
        Else
          MessageBox.Show("Es necesario que este 'Facturado' para poder liberar el material", "¡Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
      End If

    End If

  End Sub

  Private Sub DgvLiberacion_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvLiberacion.CellFormatting
    'Se definen los colores con paleta de coroles en RGB
    rojo = ColorTranslator.FromHtml("#FFC6C6")
    amarillo = ColorTranslator.FromHtml("#FDFF6C")
    verde = ColorTranslator.FromHtml("#A6FFA0")
    Anaranjado = ColorTranslator.FromHtml("#FFCC80")
    'Dependiendo del estado de la columna Estatus la fila se pintara de un cierto color y en algunos casos cambiara las letras a negritas
    For i As Integer = 0 To Me.DgvLiberacion.Rows.Count - 1

      If DgvLiberacion.Rows(i).Cells("Facturado").Value Is DBNull.Value Then
      ElseIf Me.DgvLiberacion.Rows(i).Cells("Facturado").Value = "No Facturado" Then
        Me.DgvLiberacion.Rows(i).DefaultCellStyle.BackColor = Color.Empty
      ElseIf Me.DgvLiberacion.Rows(i).Cells("Facturado").Value = "Facturado" Then
        Me.DgvLiberacion.Rows(i).DefaultCellStyle.BackColor = verde
      End If
    Next
  End Sub

  Private Sub DgvLiberacion_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvLiberacion.CellContentDoubleClick
    Dim rowC As DataGridViewRow = DgvLiberacion.CurrentRow()

    If e.RowIndex >= 0 Then
      If rowC.Cells("Facturado").Value = "Facturado" Then
        If (MessageBox.Show("Realmente desea liberar el material ." & vbCrLf & vbCrLf & "",
                            "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
        Else
          '-----
          Dim DocEntry As String = rowC.Cells("DocEntry").Value

          If rowC.Cells("action").Value = "Empacado" Then
            Dim ValidaUsr As Form = frmValidaAcceso
            'VALIDA QUE LA CELDA QUE SE ACTIVA DEBE DE SER LA DE ACCIÓN
            ValidaUsr.ShowDialog()
            If ValidaUsuario.Id_Empleado > 0 Then
                            SQLEmpleado = "UPDATE Operacion_Entrega  SET Action='En piso', horaLiberacionMaterial = GETDATE(), UserId_Libero = '" & ValidaUsuario.KeyCode & "' "
                        Else
              Exit Sub
            End If
          End If
          SQLEmpleado &= " WHERE DocEntry = '" + DocEntry + "' "
          Try
            Return
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
          End Try
          LlenaLiberacionMat()
        End If
      End If
    End If
  End Sub

  Private Sub bUpdateLiberadas_Click(sender As Object, e As EventArgs) Handles bUpdateLiberadas.Click
    MaterialLiberado()
  End Sub

  Private Sub DgvLiberacion_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DgvLiberacion.DataError
    Try
      e.Context.ToString()
    Catch ex As Exception

    End Try
  End Sub
End Class