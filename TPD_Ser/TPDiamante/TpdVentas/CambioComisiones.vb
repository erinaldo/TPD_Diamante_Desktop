Option Explicit On
'Option Strict On
Imports System.Data.SqlClient

Public Class CambioComisiones
  Public conexion As New SqlConnection(conexion_universal.CadenaSQLSAP)
  Public conexion2 As New SqlConnection(conexion_universal.CadenaSQL)

  Public DvDetalle As New DataView

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '***********BUSCAR POR*********** 
    '********NUMERO DE FACTURA*******
    '
    If TBDocNum.Text <> "" Then

RepiteConValor:

      If conexion Is Nothing AndAlso conexion.State <> ConnectionState.Open Then
        conexion.Open()
      End If
      Dim cmd As SqlCommand = New SqlCommand("SELECT T0.DocEntry, T0.cardcode,T0.cardname,case when T3.name IS NULL then '' else t3.NAME end as 'cntctcode', " &
            "CASE WHEN T0.numatcard IS NULL THEN '' ELSE T0.numatcard END AS 'numatcard',CASE WHEN T0.cursource IS NULL THEN '' ELSE T0.cursource END AS cursource, " &
            "CASE WHEN T0.series IS NULL THEN '' ELSE T2.seriesname END AS seriesname, " &
            "CASE WHEN T0.docstatus='C' THEN 'Cerrado' WHEN T0.printed='N' AND T0.docstatus='O' THEN 'Abierto' " &
            "WHEN (T0.printed='Y' OR T0.printed='A') AND T0.docstatus='O' THEN 'Abrir: Imprimido' ELSE '' END as 'status', " &
            "T0.docdate,T0.docduedate,T0.taxdate,T1.slpname,CASE WHEN t0.ownercode IS NULL THEN '' ELSE t0.ownercode END, " &
            "CASE WHEN t0.Comments IS NULL THEN '' ELSE t0.Comments END AS Comments, CASE WHEN T0.DiscPrcnt IS NULL THEN 0 ELSE T0.DiscPrcnt END AS DiscPrcnt, t0.doctype, " &
            "CASE WHEN T0.DocCur = 'MXP' THEN T0.vatsum ELSE T0.VatSumFC END AS 'VATSUM', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal ELSE T0.DocTotalFC END AS 'doctotal', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN t0.doctotal-t0.vatsum ELSE T0.DocTotalFC - T0.VatSumFC END as 'antdesc', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN t0.paidtodate ELSE T0.PaidFC end as 'impapli', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal - T0.PaidToDate ELSE T0.DocTotalFC - T0.PaidFC END AS 'saldopendiente', " &
            "CASE WHEN T4.TrnspName IS NULL THEN '' ELSE T4.TrnspName END FormaEnvio " &
            "FROM OINV T0 " &
            "LEFT JOIN OSLP T1 ON T0.SlpCode=T1.SlpCode " &
            "LEFT JOIN NNM1 T2 ON T0.Series=T2.Series " &
            "LEFT JOIN OCPR T3 ON T0.CntctCode=T3.CntctCode " &
            "LEFT JOIN OSHP T4 ON T0.TrnspCode=T4.TrnspCode " &
            "WHERE docnum = @docnum ", conexion)
      cmd.Parameters.AddWithValue("@docnum", TBDocNum.Text)

      Try

        Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        If dt.Rows.Count > 0 Then

          TBDocNum.BackColor = Color.White
          TBCliente.BackColor = Color.White
          TBNomCli.BackColor = Color.White

          Dim row As DataRow = dt.Rows(0)

          TBDocEntry.Text = CStr(row("docentry"))
          TBCliente.Text = CStr(row("cardcode"))
          TBNomCli.Text = CStr(row("cardname"))
          TBPerCon.Text = CStr(row("cntctcode"))

          txtFormaEnvio.Text = row("FormaEnvio")
          'IIf(row(1) Is DbNull.Value, "", Convert.ToString(row(1)))         

          TBNumRef.Text = IIf(CStr(row("numatcard")) Is DBNull.Value, "", Convert.ToString(CStr(row("numatcard"))))

          If CStr(row("cursource")) = "L" Then
            TBCurSource.Text = "Moneda local"
          Else
            TBCurSource.Text = "Moneda del sistema"
          End If

          TBSeries.Text = CStr(row("seriesname"))
          TBEstado.Text = CStr(row("status"))
          TBDocDate.Text = CStr(row("docdate"))
          TBFecVen.Text = CStr(row("docduedate"))
          TBFecDoc.Text = CStr(row("taxdate"))

          If CStr(row("doctype")) = "I" Then
            TBDocType.Text = "Artículo"
          Else
            TBDocType.Text = "Servicio"
          End If

          TBAgente.Text = CStr(row("slpname"))

          TBComentarios.Text = CStr(row("comments"))
          TBDescuento.Text = CStr(row("DiscPrcnt"))
          TBAntesDesc.Text = CStr(row("antdesc"))
          TBImpuesto.Text = CStr(row("vatsum"))
          TBTotalDoc.Text = CStr(row("doctotal"))
          TBImporteApli.Text = CStr(row("impapli"))
          TBSaldoPen.Text = CStr(row("saldopendiente"))



          '--------------******************************************
          '--------------******************************************
          '--------------******************************************
          conexion2.Open()
          Dim cmd4 As SqlCommand = Nothing
          cmd4 = New SqlCommand("DetalleFactComisiones", conexion2)
          cmd4.CommandType = CommandType.StoredProcedure
          cmd4.Parameters.Add("@docentry", SqlDbType.Int).Value = TBDocEntry.Text


          cmd4.ExecuteNonQuery()
          cmd4.Connection.Close()
          Dim da2 As New SqlDataAdapter
          da2.SelectCommand = cmd4
          da2.SelectCommand.Connection = conexion2


          ''--------------------------------------------
          Dim DsVtas As New DataSet
          da2.Fill(DsVtas, "DsVtas")

          DsVtas.Tables(0).TableName = "Detalle"


          DvDetalle.Table = DsVtas.Tables("Detalle")

          DGDetalle.DataSource = DvDetalle


          DisenoGrid()

        Else
          MsgBox("No hay factura con este folio")
        End If


      Catch ex As Exception
        MsgBox(ex.Message)
      Finally
        If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
          conexion.Close()
        End If
        If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
          conexion2.Close()
        End If
      End Try

      '***********************************************
      '***********************************************
      '***************CLAVE DEL CLIENTE***************
      '***********************************************
      '***********************************************

    ElseIf TBCliente.Text <> "" Then

      conexion.Open()
      Dim cmd As SqlCommand = New SqlCommand("SELECT T0.DocEntry,T0.DocNum,T0.cardname,case when T3.name IS NULL then '' else t3.NAME end as 'cntctcode', " &
            "CASE WHEN T0.numatcard IS NULL THEN '' ELSE T0.numatcard END AS 'numatcard',CASE WHEN T0.cursource IS NULL THEN '' ELSE T0.cursource END AS cursource, " &
            "CASE WHEN T0.series IS NULL THEN '' ELSE T2.seriesname END AS seriesname, " &
            "CASE WHEN T0.docstatus='C' THEN 'Cerrado' WHEN T0.printed='N' AND T0.docstatus='O' THEN 'Abierto' " &
            "WHEN (T0.printed='Y' OR T0.printed='A') AND T0.docstatus='O' THEN 'Abrir: Imprimido' ELSE '' END as 'status', " &
            "T0.docdate,T0.docduedate,T0.taxdate,T1.slpname,CASE WHEN t0.ownercode IS NULL THEN '' ELSE t0.ownercode END, " &
            "t0.Comments, CASE WHEN T0.DiscPrcnt IS NULL THEN 0 ELSE T0.DiscPrcnt END AS DiscPrcnt, t0.doctype, " &
            "CASE WHEN T0.DocCur = 'MXP' THEN T0.vatsum ELSE T0.VatSumFC END AS 'VATSUM', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal ELSE T0.DocTotalFC END AS 'doctotal', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN t0.doctotal-t0.vatsum ELSE T0.DocTotalFC - T0.VatSumFC END as 'antdesc', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN t0.paidtodate ELSE T0.PaidFC end as 'impapli', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal - T0.PaidToDate ELSE T0.DocTotalFC - T0.PaidFC END AS 'saldopendiente', " &
            "CASE WHEN T4.TrnspName IS NULL THEN '' ELSE T4.TrnspName END FormaEnvio " &
            "FROM OINV T0 " &
            "LEFT JOIN OSLP T1 ON T0.SlpCode=T1.SlpCode " &
            "LEFT JOIN NNM1 T2 ON T0.Series=T2.Series " &
            "LEFT JOIN OCPR T3 ON T0.CntctCode=T3.CntctCode " &
            "LEFT JOIN OSHP T4 ON T0.TrnspCode=T4.TrnspCode " &
            "WHERE T0.CardCode = @CardCode ", conexion)

      cmd.Parameters.AddWithValue("@CardCode", TBCliente.Text)

      Try

        Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        If dt.Rows.Count = 1 Then

          TBDocNum.BackColor = Color.White
          TBCliente.BackColor = Color.White
          TBNomCli.BackColor = Color.White

          Dim row As DataRow = dt.Rows(0)

          TBDocEntry.Text = CStr(row("docentry"))
          TBDocNum.Text = CStr(row("DocNum"))
          TBNomCli.Text = CStr(row("cardname"))
          TBPerCon.Text = CStr(row("cntctcode"))
          'IIf(row(1) Is DbNull.Value, "", Convert.ToString(row(1)))         

          TBNumRef.Text = IIf(CStr(row("numatcard")) Is DBNull.Value, "", Convert.ToString(CStr(row("numatcard"))))

          If CStr(row("cursource")) = "L" Then
            TBCurSource.Text = "Moneda local"
          Else
            TBCurSource.Text = "Moneda del sistema"
          End If

          TBSeries.Text = CStr(row("seriesname"))
          TBEstado.Text = CStr(row("status"))
          TBDocDate.Text = CStr(row("docdate"))
          TBFecVen.Text = CStr(row("docduedate"))
          TBFecDoc.Text = CStr(row("taxdate"))

          If CStr(row("doctype")) = "I" Then
            TBDocType.Text = "Artículo"
          Else
            TBDocType.Text = "Servicio"
          End If

          TBAgente.Text = CStr(row("slpname"))

          TBComentarios.Text = CStr(row("comments"))
          TBDescuento.Text = CStr(row("DiscPrcnt"))
          TBAntesDesc.Text = CStr(row("antdesc"))
          TBImpuesto.Text = CStr(row("vatsum"))
          TBTotalDoc.Text = CStr(row("doctotal"))
          TBImporteApli.Text = CStr(row("impapli"))
          TBSaldoPen.Text = CStr(row("saldopendiente"))

          '--------------******************************************
          '--------------******************************************
          '--------------******************************************
          conexion2.Open()
          Dim cmd4 As SqlCommand = Nothing
          cmd4 = New SqlCommand("DetalleFactComisiones", conexion2)
          cmd4.CommandType = CommandType.StoredProcedure
          cmd4.Parameters.Add("@docentry", SqlDbType.Int).Value = TBDocEntry.Text


          cmd4.ExecuteNonQuery()
          cmd4.Connection.Close()
          Dim da2 As New SqlDataAdapter
          da2.SelectCommand = cmd4
          da2.SelectCommand.Connection = conexion2


          ''--------------------------------------------
          Dim DsVtas As New DataSet
          da2.Fill(DsVtas, "DsVtas")

          DsVtas.Tables(0).TableName = "Detalle"


          DvDetalle.Table = DsVtas.Tables("Detalle")

          DGDetalle.DataSource = DvDetalle


          DisenoGrid()

        ElseIf dt.Rows.Count > 1 Then
          'MsgBox("PARTE PARA PROGRAMAR")
          Module1.NumCli = TBCliente.Text
          FacturasDetalleCliente.Origen = "CambioComisiones"
          FacturasDetalleCliente.ShowDialog()
          If TBDocNum.Text.Trim <> "" Then
            GoTo RepiteConValor
          End If
        Else
          MsgBox("No hay factura con este folio")
        End If


      Catch ex As Exception
        MsgBox(ex.Message)
      Finally
        If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
          conexion.Close()
        End If
        If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
          conexion2.Close()
        End If
      End Try

      '***********************************************
      '***********************************************
      '**************NOMBRE DEL CLIENTE***************
      '***********************************************
      '***********************************************

    ElseIf TBNomCli.Text <> "" Then


      conexion.Open()
      Dim cmd As SqlCommand = New SqlCommand("SELECT T0.DocEntry,T0.DocNum,T0.cardcode,case when T3.name IS NULL then '' else t3.NAME end as 'cntctcode', " &
            "CASE WHEN T0.numatcard IS NULL THEN '' ELSE T0.numatcard END AS 'numatcard',CASE WHEN T0.cursource IS NULL THEN '' ELSE T0.cursource END AS cursource, " &
            "CASE WHEN T0.series IS NULL THEN '' ELSE T2.seriesname END AS seriesname, " &
            "CASE WHEN T0.docstatus='C' THEN 'Cerrado' WHEN T0.printed='N' AND T0.docstatus='O' THEN 'Abierto' " &
            "WHEN (T0.printed='Y' OR T0.printed='A') AND T0.docstatus='O' THEN 'Abrir: Imprimido' ELSE '' END as 'status', " &
            "T0.docdate,T0.docduedate,T0.taxdate,T1.slpname,CASE WHEN t0.ownercode IS NULL THEN '' ELSE t0.ownercode END, " &
            "t0.Comments, CASE WHEN T0.DiscPrcnt IS NULL THEN 0 ELSE T0.DiscPrcnt END AS DiscPrcnt, t0.doctype, " &
            "CASE WHEN T0.DocCur = 'MXP' THEN T0.vatsum ELSE T0.VatSumFC END AS 'VATSUM', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal ELSE T0.DocTotalFC END AS 'doctotal', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN t0.doctotal-t0.vatsum ELSE T0.DocTotalFC - T0.VatSumFC END as 'antdesc', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN t0.paidtodate ELSE T0.PaidFC end as 'impapli', " &
            "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal - T0.PaidToDate ELSE T0.DocTotalFC - T0.PaidFC END AS 'saldopendiente', " &
            "CASE WHEN T4.TrnspName IS NULL THEN '' ELSE T4.TrnspName END FormaEnvio " &
            "FROM OINV T0 " &
            "LEFT JOIN OSLP T1 ON T0.SlpCode=T1.SlpCode " &
            "LEFT JOIN NNM1 T2 ON T0.Series=T2.Series " &
            "LEFT JOIN OCPR T3 ON T0.CntctCode=T3.CntctCode " &
            "LEFT JOIN OSHP T4 ON T0.TrnspCode=T4.TrnspCode " &
            "WHERE T0.CardName LIKE @CardName + '%' ", conexion)
      cmd.Parameters.AddWithValue("@CardName", TBNomCli.Text)

      Try

        Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        If dt.Rows.Count = 1 Then
          Dim row As DataRow = dt.Rows(0)

          TBDocEntry.Text = CStr(row("docentry"))
          TBDocNum.Text = CStr(row("DocNum"))
          TBNomCli.Text = CStr(row("CardCode"))
          TBPerCon.Text = CStr(row("cntctcode"))
          'IIf(row(1) Is DbNull.Value, "", Convert.ToString(row(1)))         

          TBNumRef.Text = IIf(CStr(row("numatcard")) Is DBNull.Value, "", Convert.ToString(CStr(row("numatcard"))))

          If CStr(row("cursource")) = "L" Then
            TBCurSource.Text = "Moneda local"
          Else
            TBCurSource.Text = "Moneda del sistema"
          End If

          TBSeries.Text = CStr(row("seriesname"))
          TBEstado.Text = CStr(row("status"))
          TBDocDate.Text = CStr(row("docdate"))
          TBFecVen.Text = CStr(row("docduedate"))
          TBFecDoc.Text = CStr(row("taxdate"))

          If CStr(row("doctype")) = "I" Then
            TBDocType.Text = "Artículo"
          Else
            TBDocType.Text = "Servicio"
          End If

          TBAgente.Text = CStr(row("slpname"))

          TBComentarios.Text = CStr(row("comments"))
          TBDescuento.Text = CStr(row("DiscPrcnt"))
          TBAntesDesc.Text = CStr(row("antdesc"))
          TBImpuesto.Text = CStr(row("vatsum"))
          TBTotalDoc.Text = CStr(row("doctotal"))
          TBImporteApli.Text = CStr(row("impapli"))
          TBSaldoPen.Text = CStr(row("saldopendiente"))

          '--------------******************************************
          '--------------******************************************
          '--------------******************************************
          conexion2.Open()
          Dim cmd4 As SqlCommand = Nothing
          cmd4 = New SqlCommand("DetalleFactComisiones", conexion2)
          cmd4.CommandType = CommandType.StoredProcedure
          cmd4.Parameters.Add("@docentry", SqlDbType.Int).Value = TBDocEntry.Text


          cmd4.ExecuteNonQuery()
          cmd4.Connection.Close()
          Dim da2 As New SqlDataAdapter
          da2.SelectCommand = cmd4
          da2.SelectCommand.Connection = conexion2


          ''--------------------------------------------
          Dim DsVtas As New DataSet
          da2.Fill(DsVtas, "DsVtas")

          DsVtas.Tables(0).TableName = "Detalle"
          DvDetalle.Table = DsVtas.Tables("Detalle")
          DGDetalle.DataSource = DvDetalle

          DisenoGrid()

        ElseIf dt.Rows.Count > 1 Then
          'MsgBox("PARTE PARA PROGRAMAR")
          Module1.NomCli = TBNomCli.Text
          Form3.Origen = "CambioComisiones"
          Form3.ShowDialog()
          If TBDocNum.Text.Trim <> "" Then
            GoTo RepiteConValor
          End If

          'Form3.Show()
        Else
          MsgBox("No hay factura con este folio")
        End If


      Catch ex As Exception
        MsgBox(ex.Message)
      Finally
        If conexion IsNot Nothing AndAlso conexion.State <> ConnectionState.Closed Then
          conexion.Close()
        End If
        If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
          conexion2.Close()
        End If
      End Try

    End If  'TBDocNum.Text <> ""

  End Sub

  Private Sub DisenoGrid()
    '-------Diseño de DATAGRID Totales
    With Me.DGDetalle
      '.DataSource = DtAgte
      .ReadOnly = False
      'Color de Renglones en Grid
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
      .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      .DefaultCellStyle.BackColor = Color.AliceBlue
      .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
      'Propiedad para no mostrar el cuadro que se encuentra en la parte
      'Superior Izquierda del gridview
      .RowHeadersVisible = True
      .RowHeadersWidth = 25
      '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
      'Color de linea del grid

      DGDetalle.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

      Try
        'Catch ex As Exception
        'End Try
        .Columns(0).HeaderText = "Número de Art."
        .Columns(0).Width = 100
        .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        .Columns(0).ReadOnly = True

        .Columns(1).HeaderText = "Descripción"
        .Columns(1).Width = 200
        .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        .Columns(1).ReadOnly = True

        .Columns(2).HeaderText = "Línea"
        .Columns(2).Width = 100
        .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        .Columns(2).ReadOnly = True

        .Columns(3).HeaderText = "Stock"
        .Columns(3).Width = 60
        .Columns(3).DefaultCellStyle.Format = "###,###,###"
        .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(3).ReadOnly = True

        .Columns(4).HeaderText = "Almacén"
        .Columns(4).Width = 50
        .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(4).ReadOnly = True

        .Columns(5).HeaderText = "Cantidad"
        .Columns(5).Width = 50
        .Columns(5).DefaultCellStyle.Format = "#,##0"
        .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(5).ReadOnly = True

        .Columns(6).HeaderText = "Lista de Precios"
        .Columns(6).Width = 50
        .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(6).ReadOnly = True

        .Columns(7).HeaderText = "Precio Unitario"
        .Columns(7).Width = 90
        .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(7).DefaultCellStyle.Format = "$ #,##0.###0"
        .Columns(7).ReadOnly = True

        .Columns(7).Frozen = True

        .Columns(8).HeaderText = "% de descuento"
        .Columns(8).Width = 100
        .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(8).DefaultCellStyle.Format = "#0.#0 %"
        .Columns(8).ReadOnly = True

        .Columns(9).HeaderText = "Precio tras el descuento"
        .Columns(9).Width = 90
        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(9).DefaultCellStyle.Format = "$ ###,##0.#0"
        .Columns(9).ReadOnly = True

        .Columns(10).HeaderText = "Moneda"
        .Columns(10).Width = 90
        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(10).DefaultCellStyle.Format = "$ ###,##0.#0"
        .Columns(10).ReadOnly = True

        .Columns(11).HeaderText = "Indicador de impuestos"
        .Columns(11).Width = 100
        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(11).ReadOnly = True

        .Columns(12).HeaderText = "Total (ML)"
        .Columns(12).Width = 100
        .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(12).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(12).ReadOnly = True

        .Columns(13).HeaderText = "Comisión"
        .Columns(13).Width = 75
        .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(13).DefaultCellStyle.Format = "##0.#0"
        If UsrTPM = "MANAGER" Then
          .Columns(13).ReadOnly = False
        Else
          .Columns(13).ReadOnly = True
        End If

        Dim numfilas As Integer
        numfilas = DGDetalle.RowCount 'cuenta las filas del DataGrid

        'recorre las filas del DataGrid
        For i = 0 To (numfilas - 1)
          If UsrTPM = "MANAGER" Then
            DGDetalle.Rows(i).Cells(13).Style.BackColor = Color.Beige
          End If
        Next

        .Columns(14).HeaderText = "Precio bruto"
        .Columns(14).Width = 75
        .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(14).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(14).ReadOnly = True

        .Columns(15).HeaderText = "Precio de coste ingreso bruto"
        .Columns(15).Width = 80
        .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(15).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(15).ReadOnly = True

        .Columns(16).HeaderText = "Ingreso bruto (ML)"
        .Columns(16).Width = 75
        .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(16).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(16).ReadOnly = True

        .Columns(17).HeaderText = "Total bruto (ML)"
        .Columns(17).Width = 80
        .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(17).DefaultCellStyle.Format = "$ ###,###,##0.#0"
        .Columns(17).ReadOnly = True


        .Columns(18).HeaderText = "Numero de Linea"
        .Columns(18).Width = 80
        .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        .Columns(18).ReadOnly = True
        .Columns(18).Visible = True

      Catch ex As Exception

      End Try
    End With
  End Sub

  Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
    DGDetalle.Columns.Clear()

    'Recorremos todos los controles del formulario que enviamos  
    For Each control As Control In Me.Controls

      'Filtramos solo aquellos de tipo TextBox 
      If TypeOf control Is TextBox Then
        control.Text = "" ' eliminar el texto  
      End If
    Next

    TBDocNum.BackColor = Color.Cornsilk
    TBCliente.BackColor = Color.Cornsilk
    TBNomCli.BackColor = Color.Cornsilk

  End Sub

  Private Sub FacturaConsulta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    TBDocNum.BackColor = Color.Cornsilk
    TBCliente.BackColor = Color.Cornsilk
    TBNomCli.BackColor = Color.Cornsilk
    If UsrTPM = "MANAGER" Then
      btn_ActualizarComisiones.Visible = True
    Else
      btn_ActualizarComisiones.Visible = False
    End If
  End Sub

  Private Sub GrabarComisiones(DocEntry As String)
    If UsrTPM = "MANAGER" Then 'Por cualquier cosa revalido que sea solo el usuario MANAGER
      Dim SqlConnection As New SqlConnection(StrTpm)
      SqlConnection.Open()
      Dim command As New SqlCommand
      Dim transactions As SqlTransaction
      transactions = SqlConnection.BeginTransaction(IsolationLevel.ReadCommitted, "PostComisiones")
      command.Connection = SqlConnection
      command.Transaction = transactions
      Dim contador As Integer = 0
      Dim strcadena As String = ""
      Try
        'valores a modificar
        For Each fila As DataGridViewRow In DGDetalle.Rows
          strcadena = "UPDATE SBO_TPD.dbo.INV1 SET U_BXP_Comision = " & fila.Cells("comision").Value & " WHERE LineNum = " & fila.Cells("LineNum").Value & " AND DocEntry = " & DocEntry
          command.CommandText = strcadena
          command.ExecuteNonQuery()
        Next
        transactions.Commit()
        MsgBox("Actualización de comsiones aplicadas correctamente", MsgBoxStyle.OkOnly, "Actualización correcta")
      Catch exc As Exception
        Try
          transactions.Rollback("PostComisiones")
        Catch exSql As SqlClient.SqlException
          If Not transactions.Connection Is Nothing Then
            MessageBox.Show("AL REALIZAR ROLL BACK," + exSql.Message.ToString, "SQL ERROR!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
          End If
        End Try
      Finally
        SqlConnection.Close()

      End Try
    End If
  End Sub

  Private Sub btn_ActualizarComisiones_Click(sender As Object, e As EventArgs) Handles btn_ActualizarComisiones.Click
    If TBDocEntry.Text.Trim <> "" Then
      GrabarComisiones(TBDocEntry.Text)
    End If
  End Sub

  Private Sub deshacer()
    Dim x As Control
    For Each x In Me.Controls
      If TypeOf x Is TextBox Then x.Text = ""
    Next
    TBDocType.Text = ""
    DGDetalle.DataSource = Nothing
    DGDetalle.Refresh()
    TBDocNum.BackColor = Color.Cornsilk
    TBCliente.BackColor = Color.Cornsilk
    TBNomCli.BackColor = Color.Cornsilk
    If UsrTPM = "MANAGER" Then
      btn_ActualizarComisiones.Visible = True
    Else
      btn_ActualizarComisiones.Visible = False
    End If
    TBDocNum.Select()
  End Sub

  Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
    deshacer()
  End Sub
End Class