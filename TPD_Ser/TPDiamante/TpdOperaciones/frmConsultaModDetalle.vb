Imports System.Data.SqlClient

Public Class frmConsultaModDetalle
  'VARIABLE DEL DATA VIEW GLOBAL A NIVEL FORMULARIO
  Dim DvStatus As DataView
  'VARIABLE QUE ALMACENA AL SURTIDOR
  Dim SurtidorCode As String
  'VARIABLE DE CIERRE
  Dim CerrarDescartar As Boolean = False
  'VARIABLE DE QUE TODOS LOS DATOS SE HAYAN ACTUALIZADO CORRECTAMENTE
  Dim Datos_Ok As Boolean = False
  'VARIABLE QUE ALMACENA BOOLEANO SI LAS CANTIDADES SON CORRECTAS
  Dim Cantidades_Ok As Boolean = True
  'VARIABLE QUE VALIDA SI LA CANTIDAD ES MENOR A LO SURTIDO
  Dim Pos As Integer = 0
  'VARIABLE DE TAMAÑO DE LETRA DEL GRID
  Dim LenghtGrid As Integer = 0
  'VARIABLE QUE ALMACENA LA PAQUETERIA 
  Dim TrnspCode As Integer = 0
  'VARIABLE QUE ALMACENA EL PESO
  Dim PesoSug As Double = 0
  Private Sub frmConsultaModDetalle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    'COLOCA EL TITULO DEL FORMULARIO
    Me.Text = TituloSurtido
    'COLOCA EL VALOR POR DEFECTO DEL COMPO DE PAQUETE (CAJA O TARIMA)
    cmbPack.SelectedIndex = 0
    'COLCOA EL NUMERO DE DOCUMENTO EN LA ETIQUETA DE LA ORDEN DE VENTA
    lblDocNum.Text = DocNumSurtido
    'MANDA A LLAMAR EL METODO DE OBTENCION DEL NOMBRE DE QUIEN SURTE
    MObtieneSurtidor()
    'MANDA A LLAMAR EL METODO DE OBTENCION DEL ENCABEZADO DE LA ORDEN
    MObtieneEncOrden()
    'MANDA A LLAMAR EL ESTILO DEL DATA GRID DEL DETALLE
    MEstiloGridDetalle()
    'MANDA A LLAMAR EL METODO DE OBTENCION DE STATUS CORRESPONDIENTES
    MObtieneStatus()
    'MANADA A LLAMAR EL METODO DE LLENADO DEL DETALLE DEL PEDIDO
    MLlenaOrdenesDetalle(DocNumSurtido)
    'POSICIONA EL CURSOR EN LA CELDA PRIMERA DEL DATA GRID VIEW
    'dgvDetalle.CurrentCell = dgvDetalle.Rows(0).Cells(0)

    txtCommentStatus.Enabled = Modificar
    txtQuantity.Enabled = Modificar
    cmbPack.Enabled = Modificar
    btnGuardar.Enabled = Modificar


  End Sub
#Region "Metodos"
  'METODO QUE OBTIENE EL NOMBRE DEL SURTIDOR
  Sub MObtieneSurtidor()
    'VARIABLE QUE ALMACENA CONSULTA DE STATUS
    Dim SQLSurtidor As String = ""
    '-----
    'ALAMACENA LA CONSULTA DE SOLO OBTENER EL STATUS QUE CORRESPONDA
    SQLSurtidor = "SELECT T0.UserId_Surtido, T1.Name "
    SQLSurtidor &= "FROM Operacion_Orden T0 INNER JOIN Operacion_Empleado T1 ON T0.UserId_Surtido = T1.KeyCode "
    SQLSurtidor &= "WHERE DocNum = '" + lblDocNum.Text + "' AND T1.Frozen = 'Y' "

    Try
      'CONECTA A LA BASE DE DATOS
      conexion_universal.conectar()
      'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
      conexion_universal.slq_s = New SqlCommand(SQLSurtidor, conexion_universal.conexion_uni)
      'EJECUTA LA CONSULTA
      conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
      'RECORRE LA CONSULTA
      If (conexion_universal.rd_s.Read) Then
        'ALMACENA EN LA ETIQUETA EL NOMBRE DEL SURTIDOR
        lblSurtidor.Text = rd_s.Item("Name")
        SurtidorCode = rd_s.Item("UserId_Surtido")
      End If
      conexion_universal.rd_s.Close() 'CIERRA EL READE
      conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
    Catch ex As Exception
      MsgBox("Error en busqueda del Surtidor: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexioón")
      conexion_universal.rd_s.Close() 'CIERRA EL READE
      conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
      Return
    End Try
  End Sub

  'METODO QUE OBTIENE EL ENCABEZADO DE LA ORDEN
  Sub MObtieneEncOrden()
    'VARIABLE QUE ALMACENA CONSULTA DE ENCABEZADO DE ORDEN
    Dim SQLEncabezado As String = ""

    '-----
    'ALAMACENA LA CONSULTA DE SOLO OBTENER EL ENCABEZADO DE ORDENES QUE CORRESPONDA

    SQLEncabezado = "SELECT t0.CardCode, t0.CardName, FORMAT(T0.DocDate, 'yyyy-MM-dd') AS DocDate, "
    SQLEncabezado &= "FORMAT(DocDueDate, 'yyyy-MM-dd') AS DocDueDate, Comments, T0.TrnspCode, "
    'CAMBIAR LINEAS AQUI
    SQLEncabezado &= "IIF((SELECT TrnspName FROM SBO_TPD.dbo.OSHP WHERE TrnspCode = T0.TrnspCode) IS NULL, '', (SELECT TrnspName FROM SBO_TPD.dbo.OSHP WHERE TrnspCode = T0.TrnspCode)) AS TrnspName,t1.BoxTotal "
    SQLEncabezado &= "FROM SBO_TPD.dbo.ORDR T0 "
    SQLEncabezado &= "INNER JOIN TPM.DBO.Operacion_Orden T1 ON T0.DocEntry=T1.DocEntry "
    'SQLEncabezado &= "IIF((SELECT TrnspName FROM ZPRUEBAS31OCT18.dbo.OSHP WHERE TrnspCode = T0.TrnspCode) IS NULL, '', (SELECT TrnspName FROM ZPRUEBAS31OCT18.dbo.OSHP WHERE TrnspCode = T0.TrnspCode)) AS TrnspName "
    'SQLEncabezado &= "FROM ZPRUEBAS31OCT18.dbo.ORDR T0 "


    SQLEncabezado &= "WHERE t0.DocNum = '" + lblDocNum.Text + "' "

    Try
      'CONECTA A LA BASE DE DATOS
      conexion_universal.conectar()
      'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
      conexion_universal.slq_s = New SqlCommand(SQLEncabezado, conexion_universal.conexion_uni)
      'EJECUTA LA CONSULTA
      conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
      'RECORRE LA CONSULTA
      If (conexion_universal.rd_s.Read) Then
        'ALMACENA LOS DATOS OBTENIDOS EN LOS TEXTBOX
        txtCardCode.Text = rd_s.Item("CardCode").ToString
        txtCardName.Text = rd_s.Item("CardName").ToString
        txtDocDate.Text = rd_s.Item("DocDate").ToString
        txtDocDueDate.Text = rd_s.Item("DocDueDate").ToString
        txtTrnspName.Text = rd_s.Item("TrnspName")
        txtComment.Text = rd_s.Item("Comments").ToString
        txtQuantity.Text = rd_s.Item("BoxTotal").ToString
      End If
      conexion_universal.rd_s.Close() 'CIERRA EL READE
      conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
    Catch ex As Exception
      MsgBox("Error en busqueda del Encabezado de Ordenes: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexioón")
      conexion_universal.rd_s.Close() 'CIERRA EL READE
      conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
      Return
    End Try
  End Sub

  'METODO QUE OBTIENE LOS STATUS CORRESPONDIENTES
  Sub MObtieneStatus()
    Try
      Dim SQLStatus As String
      'SE USA LA CONEXION
      Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
        'VARIABLE DATASET PARA REGISTROS
        Dim DSetTablas As New DataSet
        'INICIALIZA LA VARIABLE DEL DATA VIEW
        DvStatus = New DataView

        'CONSUILTA DE LOS PROVEEDORES
        SQLStatus = "select * from TPM.dbo.Operacion_Status WHERE (Status = 'ST' OR Status = 'P') "

        'ASIGNACION DE CONSULTA
        Dim daStatus As New SqlClient.SqlDataAdapter(SQLStatus, SqlConnection)
        'RECORRIDO DE CONSULTA
        daStatus.Fill(DSetTablas, "Status")
        'VARIABLE DE ASIGNACION DE FILAS
        Dim filaStatus As Data.DataRow

        'Asignamos a fila la nueva Row(Fila)del Dataset
        filaStatus = DSetTablas.Tables("Status").NewRow

        'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
        DSetTablas.Tables("Status").Rows.Add(filaStatus)

        DvStatus.Table = DSetTablas.Tables("Status")

        Me.cmbStatus.DataSource = DvStatus
        Me.cmbStatus.DisplayMember = "StatusName2"
        Me.cmbStatus.ValueMember = "Status"

      End Using

    Catch ex As Exception
      MsgBox("Error en llenado de Status: " + ex.Message, MsgBoxStyle.Critical, "Error de consulta o Conexión")
    End Try
  End Sub

  'METODO LLENADO DEL DETALLE
  Sub MLlenaOrdenesDetalle(ByVal DocNum As Integer)
    'VARIABLE DE CADENA DE SQL
    Dim SQLDetalle As String
    Dim SQLSugerencia As String
    'ALMACENA CONTADOR DE PARTIDAS
    Dim Line As Integer = 0
    'VARIABLE QUE ALMENA EL PESO TOTAL DE LOS ARTICULOS EN PRIMERA INSTANCIA
    Dim PesoNeto As Double = 0
    'REFRESCA EL DATA GRID VIEW DE RESULTADO
    If dgvDetalle.RowCount > 0 Then
      dgvDetalle.Rows.Clear()
    End If
    Try 'CAPTURA EL ERROR DE CONSULTA O CONEXION DE LA BASE DE DATOS DEL TPD
      'CONECTA A LA BASE DE DATOS
      conexion_universal.conectar()
      'ALAMACENA LA CONSULTA
      SQLDetalle = "SELECT DISTINCT T0.LineNum, T0.ItemCode, T0.Description, T3.ItmsGrpNam AS Linea, IIF(T2.SWeight1 IS NULL, 0, CAST(T2.SWeight1 AS DECIMAL(19,4))) AS Peso, "
      SQLDetalle &= "CAST(IIF(T2.SWeight1 IS NULL, 0, T2.SWeight1) * (CASE WHEN T4.OnHand >= T0.Quantity THEN t0.Surtido WHEN T4.OnHand = 0 THEN  t0.Surtido WHEN T4.OnHand < T0.Quantity THEN t0.Surtido ELSE 0 END) AS decimal(19,4)) AS PesoxUnidad, "
      SQLDetalle &= "T4.OnHand AS Stock, T0.Quantity, "
      SQLDetalle &= " t0.Surtido AS Surtido, "
      SQLDetalle &= "T1.TrnspCode, T0.DocNum "
      SQLDetalle &= "FROM Operacion_Detalle T0 INNER JOIN Operacion_Orden T1 ON T0.DocNum = T1.DocNum "

      'CAMBIAR LINEA AQUI
      SQLDetalle &= "INNER JOIN SBO_TPD.dbo.OITM T2  ON T2.ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS = T0.ItemCode "
      SQLDetalle &= "INNER JOIN SBO_TPD.dbo.OITB T3 ON T3.ItmsGrpCod = T2.ItmsGrpCod "
      SQLDetalle &= "INNER JOIN SBO_TPD.dbo.OITW T4 ON T4.ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS = T0.ItemCode AND T4.WhsCode = '01' "
      'SQLDetalle &= "INNER JOIN ZPRUEBAS31OCT18.dbo.OITM T2  ON T2.ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS = T0.ItemCode "
      'SQLDetalle &= "INNER JOIN ZPRUEBAS31OCT18.dbo.OITB T3 ON T3.ItmsGrpCod = T2.ItmsGrpCod "
      'SQLDetalle &= "INNER JOIN ZPRUEBAS31OCT18.dbo.OITW T4 ON T4.ItemCode COLLATE SQL_Latin1_General_CP850_CI_AS = T0.ItemCode AND T4.WhsCode = '01' "



      SQLDetalle &= "WHERE T1.DocNum =  '" + lblDocNum.Text + "' AND T3.ItmsGrpCod <> 150 "
      ' SQLDetalle &= " group by T0.DocNum,T0.LineNum, T0.ItemCode, T0.Description, T3.ItmsGrpNam ,T2.SWeight1,T4.OnHand,T0.Quantity ,t0.Surtido ,T1.TrnspCode "
      SQLDetalle &= "ORDER BY T0.DocNum DESC "

      'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
      conexion_universal.slq_s = New SqlCommand(SQLDetalle, conexion_universal.conexion_uni)
      'EJECUTA LA CONSULTA
      conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
      'RECORRE LA CONSULTA
      While conexion_universal.rd_s.Read
        TrnspCode = rd_s.Item("TrnspCode")
        'INCREMENTA CONTADOR
        Line = Line + 1
        If dgvDetalle.RowCount > 0 Then
          'MANDA LOS RESULTADOS
          Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            Me.dgvDetalle.Rows.Add(Line, rd_s.Item("ItemCode").ToString, rd_s.Item("Description"), rd_s.Item("Linea").ToString,
                        rd_s.Item("Peso").ToString, rd_s.Item("PesoxUnidad").ToString,
                        CInt(rd_s.Item("Stock")).ToString, CInt(rd_s.Item("Quantity")).ToString,
                        CInt(rd_s.Item("Surtido")).ToString)
            'RECORRE EL DATA GRID VIEW
            With dgvDetalle
              'ESTABLECE LA CELDA ACTUAL
              .CurrentCell = .Rows(Me.dgvDetalle.Rows.Count - 1).Cells(0)
            End With
          Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            'MANDA EL MENSAJE DE ERROR
            MsgBox("Error al agregar el resultado: " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
            Return
          End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
        Else
          'MANDA LOS RESULTADOS
          Try 'CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            Me.dgvDetalle.Rows.Add(Line, rd_s.Item("ItemCode").ToString, rd_s.Item("Description"), rd_s.Item("Linea").ToString,
                        rd_s.Item("Peso").ToString, rd_s.Item("PesoxUnidad").ToString,
                        CInt(rd_s.Item("Stock")).ToString, CInt(rd_s.Item("Quantity")).ToString,
                        CInt(rd_s.Item("Surtido")).ToString)
            'RECORRE EL DATA GRID VIEW
            With dgvDetalle
              'ESTABLECE LA CELDA ACTUAL
              .CurrentCell = .Rows(Me.dgvDetalle.Rows.Count - 1).Cells(0)
            End With
          Catch ex As Exception 'CATCH CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
            'MANDA EL MENSAJE DE ERROR
            MsgBox("Error al agregar el resultado en Ordenes : " & ex.Message, MsgBoxStyle.Critical, "Error en el GRID")
            Return
          End Try 'FIN CAPTURA EL ERROR ANTES DE INSERTAR EN EL DATAGRID VIEW
        End If
      End While
      conexion_universal.cerrar_conectar()

      '-----

      'RECORRE EL DATA GRID EN LA COLUMNA DE SURTIDO PARA HACER LA SUMATORIA DEL PESO TOTAL Y MOSTRARLO EN EL TEXBOX DE PESO NETO
      If dgvDetalle.Rows.Count > 0 Then
        For Fila As Integer = 0 To dgvDetalle.Rows.Count - 1
          'SUMA LOS VALORES DE CADA CELDA DE EL PESO POR UNIDADES
          PesoNeto = PesoNeto + CDbl(dgvDetalle.Rows(Fila).Cells("PesoxUnidad").Value)
        Next
        'ALMACENA EN EL CAMPO DE TEXTO EL VALOR DE KILOS EN TOTAL
        txtPeso.Text = PesoNeto
        'CAPTURA EL ERROR
        Try
          'VALIDA SI SE RECOMIENDA TARIMA O CAJAS
          If (PesoNeto >= 400) Then 'POR TRIMA
            'COLOCA VISIBLE LA ETIQUETA DE TARIMA E INVISIBLE LA DE CAJAS
            lbltarimas.Visible = True
            lblcajas.Visible = False
            'APERTURA LA CONEXION
            conexion_universal.conexion_uni.Open()
            'ALMACENA LA CONSULTA
            SQLSugerencia = "SELECT Tarimas "
            SQLSugerencia &= "FROM Operacion_Paquetes "
            SQLSugerencia &= "WHERE TrnspCode = '" + TrnspCode.ToString + "' "
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLSugerencia, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If conexion_universal.rd_s.Read Then
              'VALIDA SI LOS KILOS SON 0
              If CInt(rd_s.Item("Tarimas").ToString) = 0 Then
                txtSugCajas.Text = "Paq. sin Tarima"
              ElseIf CInt(rd_s.Item("Tarimas").ToString) > 0 Then
                'CALCULA EL SUGERIDO DE CAJAS
                txtSugCajas.Text = Math.Ceiling(PesoNeto / CInt(rd_s.Item("Tarimas")))
                'COLOCA EL COMBO DE SUGERENCIA DE PAUETE EN TARIMA
                cmbPack.SelectedIndex = 1
              End If
            End If
            'CIERRA LA CONEXION
            conexion_universal.conexion_uni.Close()
          Else 'POR CAJAS
            'COLOCA VISIBLE LA ETIQUETA DE CAJA E INVISIBLE LA DE TARIMAS
            lbltarimas.Visible = False
            lblcajas.Visible = True
            'APERTURA LA CONEXION
            conexion_universal.conexion_uni.Open()
            'ALMACENA LA CONSULTA
            SQLSugerencia = "SELECT PaquetesCajas "
            SQLSugerencia &= "FROM Operacion_Paquetes "
            SQLSugerencia &= "WHERE TrnspCode = " + TrnspCode.ToString + " "
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLSugerencia, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If conexion_universal.rd_s.Read Then
              'VALIDA SI LOS KILOS SON 0
              If CInt(rd_s.Item("PaquetesCajas").ToString) = 0 Then
                txtSugCajas.Text = "Paq. sin Caja"
              ElseIf CInt(rd_s.Item("PaquetesCajas").ToString) > 0 Then
                'CALCULA EL SUGERIDO DE CAJAS
                txtSugCajas.Text = Math.Ceiling(PesoNeto / CInt(rd_s.Item("PaquetesCajas")))
                'COLOCA EL COMBO DE SUGERENCIA DE PAUETE EN CAJAS
                cmbPack.SelectedIndex = 0
              End If
            End If
            'CIERRA LA CONEXION
            conexion_universal.conexion_uni.Close()
          End If
        Catch ex As Exception
          'CIERRA LA CONEXION
          conexion_universal.conexion_uni.Close()
          MsgBox("Sugerencia no agregada: " & ex.Message, MsgBoxStyle.Exclamation, "Alerta de calculo")
        End Try

      End If

      '-----
    Catch ex As Exception
      MsgBox("Error de consulta o conexión TPD en llenado de GRID de Ordenes: " & ex.Message, MsgBoxStyle.Critical)
      conexion_universal.cerrar_conectar()
      Return
    End Try 'FIN CAPTURA EL ERROR
  End Sub

  'METODO ESTILO DEL GRID DETALLE
  Sub MEstiloGridDetalle()
    'COLOCA EL TAMÑO DE LETRA DEL GRID
    LenghtGrid = 8
    'ESTILOS POR COLUMNA
    With Me.dgvDetalle
      'COLOCA PROPIEDADES DE COLOR ALTERNADOS
      Dim clr1 As Color
      clr1 = ColorTranslator.FromHtml("#deeaf6")
      .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
      '.AlternatingRowsDefaultCellStyle.BackColor = Color.White
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
      .DefaultCellStyle.BackColor = clr1
      .DefaultCellStyle.SelectionForeColor = Color.White
      .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      'PARTIDAS
      .Columns("LineNum").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("LineNum").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", LenghtGrid, FontStyle.Regular)
      '.Columns("LineNum").DefaultCellStyle.ForeColor = Color.Red
      'ARTICULO
      .Columns("ItemCode").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns("ItemCode").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", LenghtGrid, FontStyle.Regular)
      'DESCRIPTION
      .Columns("Dscription").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns("Dscription").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", LenghtGrid, FontStyle.Regular)
      'LINEA
      .Columns("ItmsGrpCod").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
      .Columns("ItmsGrpCod").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", LenghtGrid, FontStyle.Regular)
      'PESO
      .Columns("Peso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Peso").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", LenghtGrid, FontStyle.Regular)
      .Columns("Peso").DefaultCellStyle.Format = "N2"
      'PESO X UNIDAD
      .Columns("PesoxUnidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("PesoxUnidad").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", LenghtGrid, FontStyle.Regular)
      .Columns("PesoxUnidad").DefaultCellStyle.Format = "N2"
      'STOCK
      .Columns("OnHand").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("OnHand").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", LenghtGrid, FontStyle.Regular)
      'CANTIDAD
      .Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Quantity").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", LenghtGrid, FontStyle.Bold)
      '.Columns("Quantity").DefaultCellStyle.ForeColor = Color.Red
      'SURTIDO (REAL)
      .Columns("Surtido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      .Columns("Surtido").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", LenghtGrid, FontStyle.Bold)
      .Columns("Surtido").DefaultCellStyle.ForeColor = Color.Blue
      .Columns("Surtido").DefaultCellStyle.BackColor = Color.Silver
      If Modificar = "False" Then
        .Columns("Surtido").ReadOnly = False
      End If



    End With
  End Sub

  'METODO QUE GUARDA EL DETALLE DE LO SURTIDO EN LA TABLA Operacion_Detalle
  Sub MGuardaDetalle()
    'VARIABLES BANDERAS PARA CONOCER SI LOS DATOS SON CORRECTOS
    Dim Datos_Detalle_Ok As Boolean = False
    Dim Datos_Orden_Ok As Boolean = False
    'VARIABLE QUE ALMACENA STATUS DE ACCIÓN GUARDAR
    Dim SaveStatus As String = "ST"

    'RECORRE EL DATA GRID DE DETALLE PARA OBTENCIÓN DE DATOS DE SURTIDO
    For i As Integer = 0 To dgvDetalle.Rows.Count - 1

      '----- ACTUALIZA LA TABLA DE Operacion_Detalle
      Try
        'APERTURA DE CONEXION
        conexion_universal.conexion_uni.Open()
        'VARIABLE DE CONSULTA
        Dim SQLGuardaDetalle As String = ""
        'ALMACENA LA CONSULTA
        SQLGuardaDetalle = "UPDATE Operacion_Detalle "
        SQLGuardaDetalle &= "SET Surtido = " + dgvDetalle.Rows(i).Cells("Surtido").Value + ", "
        SQLGuardaDetalle &= "PesoBox = " + dgvDetalle.Rows(i).Cells("PesoxUnidad").Value + " "
        SQLGuardaDetalle &= "WHERE DocNum = '" + lblDocNum.Text + "' "
        SQLGuardaDetalle &= "AND ItemCode = '" + dgvDetalle.Rows(i).Cells("ItemCode").Value.ToString + "' "
        'CREA EL COMMAND
        conexion_universal.sql_up = New SqlCommand(SQLGuardaDetalle, conexion_universal.conexion_uni)
        'EJECUTA LA CONSULTA
        conexion_universal.rd_up = conexion_universal.sql_up.ExecuteScalar
        'CIERRA LA CONEXION ACTUAL
        conexion_universal.conexion_uni.Close()
        Datos_Detalle_Ok = True
      Catch ex As Exception
        Datos_Ok = False
        MsgBox("Error al Actualizar el Detalle de la Orden de venta: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de Actualziación")
        'CIERRA LA CONEXION
        conexion_universal.conexion_uni.Close()
        Return
      End Try
    Next

    '----- ACTUALIZA LA TABLA DE Operacion_Orden

    'VALIDA SI EL DETALLE SE ACTUALIZO
    If (Datos_Detalle_Ok = True) Then
      Try
        'APERTURA LA CONEXION
        conexion_universal.conexion_uni.Open()
        'VARIABLE DE CONSULTA
        Dim SQLGuardaStatus As String = ""
        'ALMACENA LA CCONSULTA
        SQLGuardaStatus = "UPDATE Operacion_Orden "
        SQLGuardaStatus &= "SET BoxName = '" + cmbPack.Text + "', BoxTotal = " + txtQuantity.Text + ", Peso = " + txtPeso.Text + ", "
        SQLGuardaStatus &= "Status = '" + SaveStatus + "', Observacion = '" + txtCommentStatus.Text + "', "
        SQLGuardaStatus &= "UserId_Update = '" + SurtidorCode + "', DateUpdate = GETDATE() "
        SQLGuardaStatus &= "WHERE DocNum = '" + lblDocNum.Text + "' "
        'CREA EL COMMAND
        conexion_universal.sql_up = New SqlCommand(SQLGuardaStatus, conexion_universal.conexion_uni)
        'EJECUTA LA CONSULTA
        conexion_universal.rd_up = conexion_universal.sql_up.ExecuteScalar
        'CIERRA LA CONEXION
        conexion_universal.conexion_uni.Close()
        Datos_Orden_Ok = True
      Catch ex As Exception
        Datos_Ok = False
        MsgBox("Error al Actualizar el Status de la Orden de venta: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de Actualziación")
        'CIERRA LA CONEXION
        conexion_universal.conexion_uni.Close()
        Return
      End Try
    End If

    '----- ACTUALIZA LA TABLA DE Operacion_Analisis

    'VALIDA SI LA ORDEN SE ACTUALIZO
    If (Datos_Orden_Ok = True) Then
      Try
        'APERTURA LA CONEXION
        conexion_universal.conexion_uni.Open()
        'VARIABLE DE CONSULTA
        Dim SQLGuardaAnalisis As String = ""
        'ALMACENA LA CONSULTA
        SQLGuardaAnalisis = "UPDATE Operacion_Analisis "
        SQLGuardaAnalisis &= "SET TimeSurtido = GETDATE(), TimeFree = GETDATE(), Status = '" + SaveStatus + "', "
        SQLGuardaAnalisis &= "BoxName = '" + cmbPack.Text + "', BoxTotal = " + txtQuantity.Text + ", Peso = " + txtPeso.Text + " "
        SQLGuardaAnalisis &= "WHERE DocNum = '" + lblDocNum.Text + "'"
        'CREA EL COMMAND
        conexion_universal.sql_up = New SqlCommand(SQLGuardaAnalisis, conexion_universal.conexion_uni)
        'EJECUTA LA CONSULTA
        conexion_universal.rd_up = conexion_universal.sql_up.ExecuteScalar
        'CIERRA LA CONEXION
        conexion_universal.conexion_uni.Close()
        Datos_Ok = True
      Catch ex As Exception
        Datos_Ok = False
        MsgBox("Error al Actualizar el Analisis de la Orden de venta.", MsgBoxStyle.Exclamation, "Alerta de Actualziación")
        'CIERRA LA CONEXION
        conexion_universal.conexion_uni.Close()
        Return
      End Try
    End If
  End Sub

  'METODO VALIDA SI LAS CANTIDADES INTRODUCIDAS EN EL DATA GRID SON CORRECTAS
  Sub MValidaCantidades()
    'COLOCA COLORES A LAS FILAS PARA DIFERENCIAR DEL SURTIDO A LA CANTIDAD REAL, O DEPENDIENDO EL CASO
    For i As Integer = 0 To dgvDetalle.Rows.Count - 1
      'VALIDA  QUE STOCK SEA MAYOR A SURTIDO Y QUE CANTIDAD SEA MAYOR A SURTIDO NO PERMITA ESTA ACCIÓN
      If CInt(dgvDetalle.Rows(i).Cells("OnHand").Value) > CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) And
                CInt(dgvDetalle.Rows(i).Cells("Quantity").Value) > CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) Then
        Dim confirma As Integer
        'MANDA LA CONFIRMACIÓN SI REALMENTE REQUIEREN SURTIR LA ORDEN
        confirma = MessageBox.Show("La validación detectó que existe diferencia en la cantidad de surtido real, Aún así desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
        'SI SE ELIJE QUE SI, ENTRA EN LA PRIMERA OPCIÓN.
        If confirma = 7 Then
          Cantidades_Ok = False
          Return
        End If

        'ElseIf CInt(dgvDetalle.Rows(i).Cells("Quantity").Value) > CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) And
        'CInt(dgvDetalle.Rows(i).Cells("OnHand").Value) = CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) Then
        'MsgBox("Existen diferencias en la cantidad de Surtido Real, favor de validar los valores", MsgBoxStyle.Exclamation, "Alerta de captura")
        'Cantidades_Ok = False
        'Return
      ElseIf CInt(dgvDetalle.Rows(i).Cells("Quantity").Value) < CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) Then
        'MANDA LA CONFIRMACIÓN SI REALMENTE REQUIEREN SURTIR LA ORDEN
        MessageBox.Show("La validación detectó que existe diferencia en la cantidad de surtido real, favor de validar los datos", "Alerta de Captura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'SI SE ELIJE QUE SI, ENTRA EN LA PRIMERA OPCIÓN.
        Cantidades_Ok = False
        Return
      ElseIf CInt(dgvDetalle.Rows(i).Cells("OnHand").Value) < CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) Then
        Dim confirma As Integer
        'MANDA LA CONFIRMACIÓN SI REALMENTE REQUIEREN SURTIR LA ORDEN
        confirma = MessageBox.Show("La validación detectó que existe diferencia en la cantidad de surtido real, Aún así desea continuar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
        'SI SE ELIJE QUE SI, ENTRA EN LA PRIMERA OPCIÓN.
        If confirma = 7 Then
          Cantidades_Ok = False
          Return
        End If
      End If
    Next
  End Sub
#End Region

#Region "EVENTOS"

  Private Sub dgvDetalle_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetalle.CellValueChanged
    'VARIABLE QUE ALMACENA EL VALOR DEL CALCULO DEL PESO
    Dim PesoxCantidad As Double
    Dim SQLSugerencia As String
    'VALIDA QUE SEA LA COLUMNA SURTIDO
    If (dgvDetalle.Columns(e.ColumnIndex).Name = "Surtido") Then
      'VARIABLE QUE OBTIENE EL TOTAL DE PESOS DE TODOS LOS ARTICULOS
      Dim PesoNeto As Double = 0
      'TEMPORALMENTE COMENTADO, DE OCUPARSE SE HABILITA **********************************
      ''SE TOMA LA FILA SELECCIONADA
      'Dim Row As DataGridViewRow = dgvDetalle.Rows(e.RowIndex)
      ''SE SELECCIONA LA FILA DEL CHECK BOX
      'Dim cellSelecion As DataGridViewCheckBoxCell = Row.Cells("CheckList")
      ''SE COLOCA CHECKED EN EL CHECKBOX
      'cellSelecion.Value = True
      'FIN TEMPORALMENTE COMENTADO, DE OCUPARSE SE HABILITA ******************************

      'CALCULA EL PESO TOTAL POR PARTIDAS REALES SURTIDAS
      PesoxCantidad = CDbl(dgvDetalle.CurrentRow.Cells("Peso").Value) * dgvDetalle.CurrentRow.Cells(e.ColumnIndex).Value
      'COLOCA EN LA CELDA DE PESO POR CANTIDAD EL VALOR DE LA MULTIPLICACION
      dgvDetalle.Rows(e.RowIndex).Cells("PesoxUnidad").Value = PesoxCantidad.ToString("N2")

      'RECORRE EL DATA GRID EN LA COLUMNA DE SURTIDO PARA HACER LA SUMATORIA DEL PESO TOTAL Y MOSTRARLO EN EL TEXBOX DE PESO NETO
      If dgvDetalle.Rows.Count > 0 Then
        For Fila As Integer = 0 To dgvDetalle.Rows.Count - 1
          'SUMA LOS VALORES DE CADA CELDA DE EL PESO POR UNIDADES
          PesoNeto = PesoNeto + CDbl(dgvDetalle.Rows(Fila).Cells("PesoxUnidad").Value)
        Next
        'ALMACENA EN EL CAMPO DE TEXTO EL VALOR DE KILOS EN TOTAL
        txtPeso.Text = PesoNeto

        'CAPTURA EL ERROR
        Try
          'VALIDA SI SE RECOMIENDA TARIMA O CAJAS
          If (PesoNeto >= 400) Then 'POR TRIMA
            'COLOCA VISIBLE LA ETIQUETA DE TARIMA E INVISIBLE LA DE CAJAS
            lbltarimas.Visible = True
            lblcajas.Visible = False
            lblBaseK.ForeColor = Color.Blue
            lblPaquete.ForeColor = Color.Blue
            'APERTURA LA CONEXION
            conexion_universal.conexion_uni.Open()
            'ALMACENA LA CONSULTA
            SQLSugerencia = "SELECT Tarimas "
            SQLSugerencia &= "FROM Operacion_Paquetes "
            SQLSugerencia &= "WHERE TrnspCode = " + TrnspCode.ToString + " "
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLSugerencia, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If conexion_universal.rd_s.Read Then
              'VALIDA SI LOS KILOS SON 0
              If CInt(rd_s.Item("Tarimas").ToString) = 0 Then
                txtSugCajas.Text = "Paq. sin Tarima"
              ElseIf CInt(rd_s.Item("Tarimas").ToString) > 0 Then
                'CALCULA EL SUGERIDO DE CAJAS
                txtSugCajas.Text = Math.Ceiling(PesoNeto / CInt(rd_s.Item("Tarimas")))
              End If
            End If
            'CIERRA LA CONEXION
            conexion_universal.conexion_uni.Close()
          Else 'POR CAJAS
            'COLOCA VISIBLE LA ETIQUETA DE CAJAS E INVISIBLE LA DE TARIMAS
            lbltarimas.Visible = False
            lblcajas.Visible = True
            lblBaseK.ForeColor = Color.Black
            lblPaquete.ForeColor = Color.Black
            'APERTURA LA CONEXION
            conexion_universal.conexion_uni.Open()
            'ALMACENA LA CONSULTA
            SQLSugerencia = "SELECT PaquetesCajas "
            SQLSugerencia &= "FROM Operacion_Paquetes "
            SQLSugerencia &= "WHERE TrnspCode = '" + TrnspCode.ToString + "' "
            'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
            conexion_universal.slq_s = New SqlCommand(SQLSugerencia, conexion_universal.conexion_uni)
            'EJECUTA LA CONSULTA
            conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
            'RECORRE LA CONSULTA
            If conexion_universal.rd_s.Read Then
              'VALIDA SI LOS KILOS SON 0
              If CInt(rd_s.Item("PaquetesCajas").ToString) = 0 Then
                txtSugCajas.Text = "Paq. sin Caja"
              ElseIf CInt(rd_s.Item("PaquetesCajas").ToString) > 0 Then
                'CALCULA EL SUGERIDO DE CAJAS
                txtSugCajas.Text = Math.Ceiling(PesoNeto / CInt(rd_s.Item("PaquetesCajas")))
              End If
            End If
            'CIERRA LA CONEXION
            conexion_universal.conexion_uni.Close()
          End If
        Catch ex As Exception
          'CIERRA LA CONEXION
          conexion_universal.conexion_uni.Close()
          MsgBox("Sugerencia no agregada: " & ex.Message, MsgBoxStyle.Exclamation, "Alerta de calculo")
        End Try
      End If
    End If
  End Sub

  Private Sub dgvDetalle_EditingControlShowing(
        ByVal sender As Object,
        ByVal e As DataGridViewEditingControlShowingEventArgs) _
            Handles dgvDetalle.EditingControlShowing
    'HACE REFERENCIA A LA CELDA
    Dim validar As TextBox = CType(e.Control, TextBox)
    'AGREGA EL CONTROLADOR DE EVENTO PARA EL KYEPRESS
    AddHandler validar.KeyPress, AddressOf validar_Keypress
  End Sub

  Private Sub validar_Keypress(
        ByVal sender As Object,
        ByVal e As System.Windows.Forms.KeyPressEventArgs)
    'OBTIENE EL INDICE DE LA COLUMNA
    Dim columna As Integer = dgvDetalle.CurrentCell.ColumnIndex
    'COMPRUEBA SI LA CELDA EN EDICIÓN CORRESPONDE A LA COLUMNA 7 O 8
    If (columna = 7 Or columna = 8) Then
      'OBTIENE EL CARACTER INTRODUCIDO
      Dim caracter As Char = e.KeyChar
      'COMPRUBA SI EL CARACTER ES UN NÚMERO O EL RETROCESO
      If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False Then
        'SI EL CARACTER ES DIFERENTE A NULO ENTRA
        If (caracter <> vbNullChar) Then
          MsgBox("Solo se permiten Números.", MsgBoxStyle.Exclamation, "Alerta de caracter")
        End If
        'COLOCA EL VALOR EN EL DIGITO QUE ESTABA
        e.KeyChar = Chr(0)
      End If
    End If 'FIN VALIDA SI ES COLUMNA 8 O 9
  End Sub

  Private Sub frmDetalleSurtir_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    'VALIDA SI ES CIERRE NORMAL O POR DESCARTAR
    If (CerrarDescartar = False) Then
      If (MessageBox.Show("Al cerrar la ventana, los posibles datos modificados se perderán." & vbCrLf & vbCrLf & "¿Esta seguro que desea cerrar la ventana?",
                            "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
        e.Cancel = True
      End If
    End If
  End Sub

  Private Sub frmDetalleSurtir_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    'INSTANCIA OBJECTO DE TIPO FORMULARIO DE MOSTRAR ORDENES PARA REFRESCARLO
    Dim Form As frmMostrarOrdenes = Application.OpenForms.OfType(Of frmMostrarOrdenes)().FirstOrDefault()
    'VALIDA SI ENCUENTRA LA INSTANCIA (FORMULARIO) ABIERTA
    If (Form IsNot Nothing) Then
      'ACTIVA EL FORMULARIO DE MOSTRAR ORDENES
      Form.Activate()
      'EJECUTA LOS METODOS DEL FORMULARIO DE MOSTRAR ORDENES
      'Form.MEjecuta_Orden()
      'Form.MEjecuta_Orden_Ped()
      'Form.MEjecuta_Orden_Ped_Det()
      Form.MLlenaOrdenes()
      'REFRESCA EL FORMULARIO
      Form.Refresh()
    End If
  End Sub

  Private Sub dgvDetalle_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvDetalle.RowPrePaint
    Try
      'COLOCA COLORES A LAS FILAS PARA DIFERENCIAR DEL SURTIDO A LA CANTIDAD REAL, O DEPENDIENDO EL CASO
      For i As Integer = 0 To dgvDetalle.Rows.Count - 1
        'COMPARA LA CANTIDAD REAL CON LO SURTIDO QUE NOSEA A= 0
        If CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) = 0 And CInt(dgvDetalle.Rows(i).Cells("OnHand").Value) = 0 Then
          'COLOCA COLOR A LA CELDA U LA LETRA
          dgvDetalle.Rows(i).DefaultCellStyle.BackColor = Color.LightCoral

          'VALIDA  QUE STOCK SEA MAYOR A SURTIDO Y QUE CANTIDAD SEA MAYOR A SURTIDO NO PERMITA ESTA ACCIÓN
        ElseIf CInt(dgvDetalle.Rows(i).Cells("OnHand").Value) > CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) And
                    CInt(dgvDetalle.Rows(i).Cells("Quantity").Value) > CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) Then
          'COLOCA COLOR A LA CELDA U LA LETRA
          dgvDetalle.Rows(i).DefaultCellStyle.BackColor = Color.Orange
          'POSICIONA EN LA CELDA QUE TIENE EL VALOR MAYOR
          'dgvDetalle.CurrentCell = dgvDetalle.Rows(Pos).Cells("Surtido") '(*********SE COMENTA TEMPORALMENTE************)

          'VALIDA QUE LA CANTIDAD CON SURTIDO ES IGUAL Y QUE STOCK SEA MAYOR QUE LO SURTIDO
        ElseIf CInt(dgvDetalle.Rows(i).Cells("Quantity").Value) = CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) And
                    CInt(dgvDetalle.Rows(i).Cells("OnHand").Value) >= CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) Then
          'COLOCA COLOR A LA CELDA U LA LETRA
          dgvDetalle.Rows(i).DefaultCellStyle.BackColor = Nothing

        ElseIf CInt(dgvDetalle.Rows(i).Cells("Quantity").Value) > CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) And
                    CInt(dgvDetalle.Rows(i).Cells("OnHand").Value) = CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) Then
          'COLOCA COLOR A LA CELDA U LA LETRA
          dgvDetalle.Rows(i).DefaultCellStyle.BackColor = Color.Khaki

        ElseIf CInt(dgvDetalle.Rows(i).Cells("Quantity").Value) < CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) Then
          'COLOCA COLOR A LA CELDA U LA LETRA
          dgvDetalle.Rows(i).DefaultCellStyle.BackColor = Color.Orange
          'POSICIONA EN LA CELDA QUE TIENE EL VALOR MAYOR
          dgvDetalle.CurrentCell = dgvDetalle.Rows(Pos).Cells("Surtido")

        ElseIf CInt(dgvDetalle.Rows(i).Cells("OnHand").Value) < CInt(dgvDetalle.Rows(i).Cells("Surtido").Value) Then
          'COLOCA COLOR A LA CELDA U LA LETRA
          dgvDetalle.Rows(i).DefaultCellStyle.BackColor = Color.Orange
          'POSICIONA EN LA CELDA QUE TIENE EL VALOR MAYOR
          'dgvDetalle.CurrentCell = dgvDetalle.Rows(Pos).Cells("Surtido") '(******SE COMENTA TEMPORALMENTE*******)
        End If
      Next
    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try
  End Sub

  Private Sub dgvDetalle_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetalle.CellEndEdit
    'VARIABLES QUE ALMACENAN LOS VLORES DE CANTIDADES DE LAS PIEZAS

    Dim Cantidad As Integer
    Dim Surtido As Integer
    Dim Stock As Integer
    'OBTIENE LA FILA SELECCIONADA
    Dim row As DataGridViewRow = dgvDetalle.CurrentRow
    'OBTIENE EL VALOR POR CADA CELDA
    Stock = CStr(row.Cells("OnHand").Value)
    Cantidad = CStr(row.Cells("Quantity").Value)
    Surtido = CStr(row.Cells("Surtido").Value)
    'VALIDA SI CANTIDAD ES MENOR A SURTIDO
    If Cantidad < Surtido Then
      'ALMACENA EL VALOR DE LA FILA PARA SU POSICION 
      Pos = e.RowIndex
      MsgBox("El valor de Surtido no Puede ser mayor a la Cantidad.", MsgBoxStyle.Exclamation, "Alerta de Captura")
    ElseIf Stock < Surtido Then
      'ALMACENA EL VALOR DE LA FILA PARA SU POSICION 
      'Pos = e.RowIndex (***** SE COMENTA TEMPORALMENTE ******)
      MsgBox("El valor de Surtido no Puede ser mayor al Stock.", MsgBoxStyle.Exclamation, "Alerta de Captura")
    ElseIf (Stock > Surtido) And (Cantidad > Surtido) Then
      'ALMACENA EL VALOR DE LA FILA PARA SU POSICION 
      'Pos = e.RowIndex (***** SE COMENTA TEMPORALMENTE ******)
      MsgBox("El valor de Surtido no Puede ser menor al Stock con cantidad mayor solicitada.", MsgBoxStyle.Exclamation, "Alerta de Captura")

    End If

  End Sub

  Private Sub txtQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQuantity.KeyPress
    'OBTIENE EL CARACTER INTRODUCIDO
    Dim caracter As Char = e.KeyChar
    'COMPRUBA SI EL CARACTER ES UN NÚMERO O EL RETROCESO
    If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False Then
      'SI EL CARACTER ES DIFERENTE A NULO ENTRA
      If (caracter <> vbNullChar) Then
        MsgBox("Solo se permiten Números.", MsgBoxStyle.Exclamation, "Alerta de caracter")
      End If
      'COLOCA EL VALOR EN EL DIGITO QUE ESTABA
      e.KeyChar = Chr(0)
    End If


  End Sub
#End Region
#Region "Botones"
  Private Sub Actualiza_Peso()
    'ACTUALIZA EL ESTATUS A EMPACADO
    Try
      'VARIABLE DE CADENA DE SQL
      Dim SQLOrdenes As String
      'VARIABLES DE CONEXION DE LLENADO
      Dim cmd As SqlCommand
      Dim cnn As SqlConnection = Nothing
      SQLOrdenes = "Update Operacion_Orden SET Peso=" + txtPeso.Text + " WHERE DocEntry='" + lblDocNum.Text + "' "
      cnn = New SqlConnection(StrTpm)
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
  End Sub
  Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
    Actualiza_Peso()
    'CIERRA EL FORMULARIO
    Me.Close()
  End Sub

  Private Sub btnDescartar_Click(sender As Object, e As EventArgs) Handles btnDescartar.Click
    'VARIABLE DE CONFIRMACIÓN SI REALMENTE DESE DESCARTAR LA ORDEN DE PEDIDO
    Dim Confirma As Integer
    'MANDA LA CONFIRMACIÓN SI REALMENTE REQUIEREN SURTIR LA ORDEN
    Confirma = MessageBox.Show("Al descartar la orden de Venta los datos se perderán, así como también, la Orden regresará a estatus Surtir." & vbCrLf & vbCrLf & "Realmente desea Descartar la Orden [ " + lblDocNum.Text + " ] ?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
    'SI SE ELIJE QUE SI, ENTRA EN LA PRIMERA OPCIÓN.
    If Confirma = 6 Then
      'VARIABLES PARA ALMCENAR LA CONSULTA
      Dim SQLUpdateOrden As String = ""
      Dim SQLUpdateAnalisis As String = ""
      Dim SQLUpdateDetalle As String = ""
      Dim SQLSearchDetalle As String = ""

      'CAPTURA EL ERROR
      Try
        'ABRE LA CONEXION
        conexion_universal.conectar()

        'ALMACENA LA CONSULTA
        SQLUpdateOrden = "UPDATE Operacion_Orden SET Observacion = '" + txtCommentStatus.Text + "', UserId_Surtido = NULL, UserId_Update = '" + SurtidorCode + "', "
        SQLUpdateOrden &= "DateUpdate = GETDATE(), BoxTotal = 0, BoxName = NULL, Peso = 0 "
        SQLUpdateOrden &= "WHERE DocNum = '" + lblDocNum.Text + "' "
        'CONVIERTE LA CONSULTA A UN COMMAND
        conexion_universal.slq_s = New SqlCommand(SQLUpdateOrden, conexion_universal.conexion_uni)
        'EJECUTA LA CONSULTA
        conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
        conexion_universal.conexion_uni.Close() 'CIERRA LA CONEXION

        'APERTURA NUEVAMENTE LA CONEXION
        conexion_universal.conexion_uni.Open()

        'ALMACENA LA CONSULTA
        SQLUpdateAnalisis = "DELETE FROM Operacion_Analisis WHERE DocNum = '" + lblDocNum.Text + "' "
        'CONVIERTE LA CONSULTA A UN COMMAND
        conexion_universal.slq_s = New SqlCommand(SQLUpdateAnalisis, conexion_universal.conexion_uni)
        'EJECUTA LA CONSULTA
        conexion_universal.rd_s = conexion_universal.slq_s.ExecuteScalar
        conexion_universal.conexion_uni.Close() 'CIERRA LA CONEXION

        'APERTURA LA CONEXION DE BUSQUEDA
        conexion_universal.conexion_uni.Open()

        'ALMACENA LA CONSULTA
        SQLSearchDetalle = "SELECT DocNum, ItemCode, Quantity FROM Operacion_Detalle WHERE DocNum = '" + lblDocNum.Text + "' "
        'CONVIERTE LA CONSULTA A UN COMMAND
        conexion_universal.sql_tem = New SqlCommand(SQLSearchDetalle, conexion_universal.conexion_uni)
        'EJECUTA LA CONSULTA
        conexion_universal.rd_tem = conexion_universal.sql_tem.ExecuteReader
        'RECORRE LA CONSULTA
        While (conexion_universal.rd_tem.Read())
          SQLUpdateDetalle = ""
          'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
          SQLUpdateDetalle = "UPDATE TPM.dbo.Operacion_Detalle SET Surtido = '" + conexion_universal.rd_tem.Item("Quantity").ToString + "', "
          SQLUpdateDetalle &= "Box = NULL, PesoBox = NULL WHERE DocNum = '" + lblDocNum.Text + "' AND ItemCode = '" + conexion_universal.rd_tem.Item("ItemCode") + "' "
          'APERTURA LA CONEXION
          conexion_universal.conectar_sap()
          'CONVIERTE LA CONSULTA A UN COMMAND
          conexion_universal.sql_up = New SqlCommand(SQLUpdateDetalle, conexion_universal.conexion_uni_sap)
          'EJECUTA LA CONSULTA
          conexion_universal.rd_up = conexion_universal.sql_up.ExecuteScalar
          'CIERRA LA CONEXION GENERAL
          conexion_universal.cerrar_conectar_sap()
        End While
        'CIERRA EL READER
        conexion_universal.rd_tem.Close()
        'CIERRA LA CONEXION GENERAL
        conexion_universal.cerrar_conectar()

        'MANDA MENSAJE DE SATISFACCION
        MsgBox("La Orden de Venta se descarto de manera Exitosa.", MsgBoxStyle.Information, "Operación exitosa")
        'ALMACENA VARIABLE EN VERDADERO PARA PODER VALIDAR QUE SE CIERRA POR DESCARTAR
        CerrarDescartar = True
        'CIERRA EL FORMULARIO ACTUAL
        Me.Close()

      Catch ex As Exception
        'MANDA MENSAJE DE ERROR
        MsgBox("Error en consulta o conexión: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de consulta o conexión")
        conexion_universal.cerrar_conectar() 'CIERRA LA CONEXION
        Return
      End Try
      'COLOCA EN NADA LA VARIABLE GLOBAL DE ACCESO A SURTIR
      TituloSurtido = ""
      DocNumSurtido = ""
      StatusSurtido = ""
    End If
  End Sub

  Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
    'VALIDA QUE LA SUGERENCIA DE PAQUETE NO VAYA VACIO O LA CANTIDAD DE CAJAS 
    If (cmbPack.SelectedIndex = -1) Then
      MsgBox("Favor de seleccionar que tipo de paquete se requiere.", MsgBoxStyle.Exclamation, "Alerta de captura")
      cmbPack.Focus()
      Return
    End If
    If (txtQuantity.Text Is Nothing Or txtQuantity.Text = "" Or txtQuantity.Text = " ") Then
      MsgBox("Favor de insertar el total de cajas o tarimas requeridas.", MsgBoxStyle.Exclamation, "Alerta de captura")
      txtQuantity.Focus()
      Return
    End If

    'VARIABLE DE CONFIRMACIÓN SI REALMENTE DESE DESCARTAR LA ORDEN DE PEDIDO
    Dim Confirma As Integer
    'MANDA LA CONFIRMACIÓN SI REALMENTE REQUIEREN SURTIR LA ORDEN
    Confirma = MessageBox.Show("Se Modificara la Orden de Venta." & vbCrLf & vbCrLf & "Realmente desea Continuar ?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    'SI SE ELIJE QUE SI, ENTRA EN LA PRIMERA OPCIÓN.
    If Confirma = 6 Then
      'MANDA A LLAMAR EL METODO DE VALIDAR VALORES DE CANTIDAD
      MValidaCantidades()
      'VALIDA SI LAS CANTIDADES SON CORRECTAS
      If (Cantidades_Ok = True) Then
        'MANDA A LLAMAR METODO DE ACTUALIZACIÓN DEL DETALLE
        MGuardaDetalle()
      Else
        Cantidades_Ok = True
        Return
      End If
      'VALIDA QUE TODOS LOS DATOS SE HAYAN ACTUALIZADO DE MANERA CORRECTA
      If (Datos_Ok = True) Then
        MsgBox("La Orden de Venta [ " + lblDocNum.Text + " ] se ha Modificado de manera Satisfactoria.", MsgBoxStyle.Information, "Proceso finalizado")
        'ALMACENA VARIABLE EN VERDADERO PARA PODER VALIDAR QUE SE CIERRA POR DESCARTAR O POR QUE SE CONCLUYO EL SURTIDO DE LA ORDEN
        CerrarDescartar = True
        'CIERRA EL FORMULARIO ACTUAL
        Me.Close()
      Else
        'MANDA MENSAJE DE ERROR Y QUEDA EN LA MISMA PANTALLA
        MsgBox("Sucedio un error al finalizar el proceso de modificacion surtido, intente nuevamente el proceso, si el inconveniente persiste, favor de contactar al Área de Sistemas.", MsgBoxStyle.Exclamation)
      End If
    End If
  End Sub

  Private Sub dgvDetalle_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetalle.CellContentClick

  End Sub
#End Region
  'FIN BOTONES =====================


End Class