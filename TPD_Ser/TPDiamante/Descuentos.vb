
Imports System.Data
Imports System.Data.SqlClient

Public Class Descuentos

    Public StrProd As String = conexion_universal.CadenaSBO_Diamante
    Public StrTpm As String = conexion_universal.CadenaSQL
    Public StrCon As String = conexion_universal.CadenaSQLSAP

    Dim DvTotales As New DataView
    Dim DvAgentes As New DataView
    Dim DvAgentes2 As New DataView
    Dim DvClientes As New DataView


    Dim DvFacturas As New DataView

    Dim DvDetalle As New DataView

    Private Sub Descuentos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")

        Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

        'Variable para guardar la consulta de AGENTES y SUCURSALES en los combobox
        Dim ConsutaLista As String


        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)


            Dim DSetTablas As New DataSet
            ConsutaLista = "SELECT GroupCode , GroupName FROM OCRG with (nolock) WHERE GroupType = 'C' ORDER BY GroupName "
            Dim daGSucural As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            'Dim DSetTablas As New DataSet
            daGSucural.Fill(DSetTablas, "Sucursales")

            Dim fila As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            fila = DSetTablas.Tables("Sucursales").NewRow

            'Agregamos los valores a los campos de la tabla
            fila("GroupName") = "TODAS"
            fila("GroupCode") = 99

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Sucursales").Rows.Add(fila)

            Me.CmbSucursal.DataSource = DSetTablas.Tables("Sucursales")
            Me.CmbSucursal.DisplayMember = "GroupName"
            Me.CmbSucursal.ValueMember = "GroupCode"
            Me.CmbSucursal.SelectedValue = 99


            '---------------------------------------------------------

            ConsutaLista = "SELECT T0.slpcode,T0.slpname,T1.GroupCode FROM OSLP T0 "
            ConsutaLista &= "INNER JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
            ConsutaLista &= "WHERE T1.CbrGralAdicional = 'N' AND T0.SlpCode <> 1 AND (U_ESTATUS = 'ACTIVO' OR U_ESTATUS = 'INACTIVOCC') ORDER BY slpname "


            Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


            daAgte.Fill(DSetTablas, "Agentes")

            Dim filaAgte As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaAgte = DSetTablas.Tables("Agentes").NewRow

            'Agregamos los valores a los campos de la tabla
            filaAgte("slpname") = "TODOS"
            filaAgte("slpcode") = 999
            filaAgte("GroupCode") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

            DvAgentes2.Table = DSetTablas.Tables("Agentes")

            Me.CmbAgteVta.DataSource = DvAgentes2
            Me.CmbAgteVta.DisplayMember = "slpname"
            Me.CmbAgteVta.ValueMember = "slpcode"
            Me.CmbAgteVta.SelectedValue = 999

            ' -----------------------------------------------------

        End Using

    End Sub

    Sub BuscaAgentes()

        If CmbSucursal.SelectedValue Is Nothing Or CmbSucursal.SelectedValue = 99 Then
            DvAgentes2.RowFilter = String.Empty
            Me.CmbAgteVta.SelectedValue = 999
            Me.CmbAgteVta.DataSource = DvAgentes2

        Else
            DvAgentes2.RowFilter = String.Empty
            Me.CmbAgteVta.SelectedValue = 999
            DvAgentes2.RowFilter = "GroupCode = " & Trim(Me.CmbSucursal.SelectedValue.ToString) & " OR GroupCode = 999"
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        DGFacturas.Columns.Clear()

        EjecutarConsulta()
    End Sub


    Private Sub EjecutarConsulta()
        DGFacturas.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES

        Dim Consulta As String = ""
        Dim strcadena As String = ""
        Dim CTabla As String = ""
        Dim DTMObra As New DataTable
        Dim DTProb As New DataTable
        Dim vAlm As Integer = 0

        'TxtAlmacen.Text = cmbAlmacen.Text
        'TxtAlmacen.Visible = True


        Consulta &= " SELECT T0.DocNum,T0.DocDate,T0.CardCode,T0.CardName,"
        Consulta &= " T0.SlpCode, T1.SlpName, T0.DiscPrcnt/100, T4.U_NAME"
        Consulta &= " FROM OINV T0"
        Consulta &= " LEFT JOIN OSLP T1 ON T0.SlpCode = T1.SlpCode"
        Consulta &= " LEFT JOIN OWDD T2 ON T0.DocEntry = T2.DocEntry"
        Consulta &= " LEFT JOIN WDD1 T3 ON T2.WddCode = T3.WddCode AND T3.Status='Y'"
        Consulta &= " LEFT JOIN OUSR T4 ON T3.UserID = T4.USERID"
        Consulta &= " WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaFin "
        Consulta &= " AND T0.DiscPrcnt > 0"
        'Consulta &= " ORDER BY T0.DocDate"


        If CmbSucursal.SelectedValue <> 99 Then
            If CmbSucursal.SelectedValue = 100 Then
                'Consulta &= " AND T0.SlpCode <> 17 AND T0.SlpCode <> 20 AND T0.SlpCode <> 8 AND T0.SlpCode <> 12 AND T0.SlpCode <> 26"
                Consulta &= " AND (T0.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '01') OR T0.SlpCode = -1) "

            ElseIf CmbSucursal.SelectedValue = 102 Then
                'Consulta &= " AND (T0.SlpCode = 17 OR T0.SlpCode = 20) "
                Consulta &= " AND (T0.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '03')) "

            ElseIf CmbSucursal.SelectedValue = 103 Then
                'Consulta &= " AND (T0.SlpCode = 8 OR T0.SlpCode = 12 OR T0.SlpCode = 26) "
                Consulta &= " AND (T0.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '07')) "
            End If

        End If


        If CmbAgteVta.SelectedValue <> 999 Then
            Consulta &= " AND T0.SlpCode = " & CmbAgteVta.SelectedValue
        End If

        Consulta &= " ORDER BY T0.DocDate"

        Consulta &= " SELECT T0.ItemCode,T0.Dscription,T0.Quantity,T0.U_BXP_ListaP,T0.PriceBefDi,"
        Consulta &= " T0.DiscPrcnt/100, T0.Price, T0.LineTotal, T0.U_BXP_Comision/100,T1.DocNum "
        Consulta &= " FROM INV1 T0"
        Consulta &= " LEFT JOIN OINV T1 ON T0.DocEntry = T1.DocEntry"
        Consulta &= " LEFT JOIN OITM T2 ON T0.ItemCode = T2.ItemCode"
        Consulta &= " LEFT JOIN OITB T3 ON T2.ItmsGrpCod = T3.ItmsGrpCod"
        Consulta &= " WHERE T1.DocDate >= @FechaIni AND T1.DocDate <= @FechaFin AND T1.DiscPrcnt > 0 "


        If CmbSucursal.SelectedValue <> 99 Then
            'MsgBox("entre1")
            If CmbSucursal.SelectedValue = 100 Then
                'MsgBox("entre2")
                'Consulta &= " AND T1.SlpCode <> 17 AND T1.SlpCode <> 20 AND T1.SlpCode <> 8 AND T1.SlpCode <> 12 AND T1.SlpCode <> 26"
                Consulta &= " AND (T1.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '01') OR T1.SlpCode = -1) "

            ElseIf CmbSucursal.SelectedValue = 102 Then
                'MsgBox("entre3")
                'Consulta &= " AND (T1.SlpCode = 17 OR T1.SlpCode = 20) "
                Consulta &= " AND (T1.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '03')) "

            ElseIf CmbSucursal.SelectedValue = 103 Then
                'MsgBox("entre4")
                'Consulta &= " AND (T1.SlpCode = 8 OR T1.SlpCode = 12 OR T1.SlpCode = 26) "
                Consulta &= " AND (T1.SlpCode IN (SELECT TT1.SlpCode FROM SBO_TPD.DBO.OSLP TT1 WHERE TT1.Memo = '07')) "
            End If

        End If


        If CmbAgteVta.SelectedValue <> 999 Then
            Consulta &= " AND T1.SlpCode = " & CmbAgteVta.SelectedValue
            'MsgBox("entre5")
        End If

        Consulta &= " ORDER BY T1.DocDate "

        Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

        CmdMObra.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime).Value = Me.DtpFechaIni.Value
        CmdMObra.Parameters.Add("@FechaFin", SqlDbType.SmallDateTime).Value = Me.DtpFechaTer.Value
        'MsgBox(Consulta)


        'If CmbGrupoArticulo.SelectedValue <> 999 Then
        '    CmdMObra.Parameters.Add("@GrupoArt", SqlDbType.Int)
        '    CmdMObra.Parameters("@GrupoArt").Value = CmbGrupoArticulo.SelectedValue
        'End If


        'If CmbArticulo.SelectedValue <> "TODOS" Then
        '    CmdMObra.Parameters.Add("@IdArticulo", SqlDbType.Char)
        '    CmdMObra.Parameters("@IdArticulo").Value = CmbArticulo.SelectedValue
        'End If

        Dim DsVtasDet As New DataSet

        'DTMObra.TableName = "DetBO"

        DsVtasDet.Tables.Add(DTMObra)

        CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
        CmdMObra.Connection.Open()

        Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)

        AdapMObra.SelectCommand = CmdMObra
        AdapMObra.Fill(DsVtasDet, "Descuentos")

        DsVtasDet.Tables(1).TableName = "Facturas"
        DsVtasDet.Tables(2).TableName = "Detalle"

        'Dim DtBackOrder As New DataTable
        'DtBackOrder = DsVtasDet.Tables("BackOrder")

        'Me.DGLineas.DataSource = DtBackOrder

        DvFacturas.Table = DsVtasDet.Tables("Facturas")

        With Me.DGFacturas
            .DataSource = DvFacturas
        End With

        DvDetalle.Table = DsVtasDet.Tables("Detalle")

        With Me.DGDetalle
            .DataSource = DvDetalle
        End With

        DisenoGrid()

        DisenoGridDet()

        BuscaDetalle()

    End Sub

    Private Sub CmbSucursal_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbSucursal.SelectionChangeCommitted
        BuscaAgentes()
    End Sub

    Private Sub CmbSucursal_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbSucursal.Validating
        BuscaAgentes()
    End Sub

    Private Sub CmbSucursal_KeyUp(sender As Object, e As KeyEventArgs) Handles CmbSucursal.KeyUp
        BuscaAgentes()
    End Sub


    Private Sub DisenoGrid()

        Try

            With Me.DGFacturas
                '.DataSource = DTMObra
                .ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .RowHeadersWidth = 25
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                '.RowHeadersVisible = False
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = True
                .AllowUserToAddRows = False
                '.AllowUserToAddRows = False

                .Columns(0).HeaderText = "Factura"
                .Columns(0).Width = 70
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Fecha"
                .Columns(1).Width = 100
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(2).HeaderText = "Cliente"
                .Columns(2).Width = 65
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Nombre"
                .Columns(3).Width = 240

                .Columns(4).HeaderText = "Agente"
                .Columns(4).Width = 55
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(4).DefaultCellStyle.Format = "$ ###,###,##0.#0"


                .Columns(5).HeaderText = "Nombre"
                .Columns(5).Width = 160
                '.Columns(5).DefaultCellStyle.Format = "#0.#0 %"
                '.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                .Columns(6).HeaderText = "Descuento"
                .Columns(6).Width = 70
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(6).DefaultCellStyle.Format = "#0.###0 %"


                .Columns(7).HeaderText = "Autorizado por"
                .Columns(7).Width = 100



            End With

        Catch ex As Exception
            'MsgBox("DG1")
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DisenoGridDet()

        Try

            With Me.DGDetalle
                '.DataSource = DTMObra
                .ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .RowHeadersWidth = 35
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                '.RowHeadersVisible = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = True
                .AllowUserToAddRows = False
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter



                .Columns(0).HeaderText = "Artículo"
                .Columns(0).Width = 100
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(0).Frozen = True

                .Columns(1).HeaderText = "Descripción"
                .Columns(1).Width = 200
                .Columns(1).Frozen = True

                '.Columns(2).HeaderText = "CodLin"
                '.Columns(2).Width = 140
                '.Columns(2).Visible = False

                '.Columns(3).HeaderText = "Línea"
                '.Columns(3).Width = 120
                '.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                '.Columns(5).DefaultCellStyle.Format = "##.#0 %"

                .Columns(2).HeaderText = "Cantidad"
                .Columns(2).Width = 50
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(2).DefaultCellStyle.Format = "###,###,##0"
                '.Columns(4).Visible = False

                .Columns(3).HeaderText = "Lista de precios"
                .Columns(3).Width = 55
                '.Columns(3).DefaultCellStyle.Format = "#0.#0 %"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                'T0.ItemCode,T0.Dscription,T0.Quantity,T0.U_BXP_ListaP,T0.PriceBefDi,"
                'Consulta &= " T0.DiscPrcnt, T0.Price, T0.LineTotal,T1.DocNum"

                .Columns(4).HeaderText = "Precio"
                .Columns(4).Width = 75
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).DefaultCellStyle.Format = "$ ###,##0.#0"

                .Columns(5).HeaderText = "Descuento (%)"
                .Columns(5).Width = 60
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(5).DefaultCellStyle.Format = "#0.#0 %"

                .Columns(6).HeaderText = "Precio tras descuento ($)"
                .Columns(6).Width = 75
                .Columns(6).DefaultCellStyle.Format = "$ ###,##0.#0"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(7).HeaderText = "Total"
                .Columns(7).Width = 85
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).DefaultCellStyle.Format = "$ ###,##0.#0"

                .Columns(8).HeaderText = "Comisión"
                .Columns(8).Width = 55
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(8).DefaultCellStyle.Format = "#0.#0 %"

                .Columns(9).HeaderText = "Factura"
                .Columns(9).Width = 90
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter



            End With

        Catch ex As Exception
            'MsgBox("DG2")
            'MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub BuscaDetalle()
        Try
            DvDetalle.RowFilter = "docnum = '" & DGFacturas.Item(0, DGFacturas.CurrentRow.Index).Value & "'"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGFacturas_CurrentCellChanged(sender As Object, e As EventArgs) Handles DGFacturas.CurrentCellChanged
        BuscaDetalle()
    End Sub

    Private Sub BtnExcel_Click(sender As Object, e As EventArgs) Handles BtnExcel.Click

        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)


        'COMBINAMOS CELDAS
        oSheet.Range("A1:E1").Merge(True)
        oSheet.Range("A2:E2").Merge(True)
        oSheet.Range("A3:E3").Merge(True)
        oSheet.Range("A4:E4").Merge(True)
        'oSheet.Range("A5:E5").Merge(True)

        oSheet.range("A1:E1").font.bold = True
        oSheet.range("A2:E2").font.bold = True
        oSheet.range("A3:E3").font.bold = True
        oSheet.range("A4:E4").font.bold = True
        'oSheet.range("A5:E5").font.bold = True

        'DAR COLOR DE FONDO A CELDAS
        oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
        oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15
        oSheet.Range("A3:C3").INTERIOR.COLORINDEX = 15
        oSheet.Range("A4:C4").INTERIOR.COLORINDEX = 15
        'oSheet.Range("A5:C5").INTERIOR.COLORINDEX = 15


        oSheet.Range("A6").INTERIOR.COLORINDEX = 15
        oSheet.Range("B6").INTERIOR.COLORINDEX = 15
        oSheet.Range("C6").INTERIOR.COLORINDEX = 15
        oSheet.Range("D6").INTERIOR.COLORINDEX = 15
        oSheet.Range("E6").INTERIOR.COLORINDEX = 15
        oSheet.Range("F6").INTERIOR.COLORINDEX = 15
        oSheet.Range("G6").INTERIOR.COLORINDEX = 15
        oSheet.Range("H6").INTERIOR.COLORINDEX = 15
        'oSheet.Range("7").INTERIOR.COLORINDEX = 15
        'oSheet.Range("J7").INTERIOR.COLORINDEX = 15
        'oSheet.Range("K7").INTERIOR.COLORINDEX = 15



        'Declaramos el nombre de las columnas
        oSheet.range("A6").value = "Factura"
        oSheet.range("B6").value = "Fecha"
        oSheet.range("C6").value = "Cliente"
        oSheet.range("D6").value = "Nombre"
        oSheet.range("E6").value = "Agente"
        oSheet.range("F6").value = "Nombre"
        oSheet.range("G6").value = "Descuento"
        oSheet.range("H6").value = "Autorizado por"

        'oSheet.range("I6").value = "Descripción"
        'oSheet.range("J6").value = "Linea"
        'oSheet.range("K6").value = "Pedido Cliente"
        'oSheet.range("L6").value = "Facturado"
        'oSheet.range("M6").value = "Back Order"


        'DISEÑO DE EXCEL

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A6:H6").font.bold = True


        oExcel.worksheets("Hoja1").Columns("A").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("A").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("B").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("B").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("C").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("C").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("D").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("D").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("E").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("E").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("F").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("F").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("G").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("G").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("H").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("H").Font.Size = 8
        'oExcel.worksheets("Hoja1").Columns("I").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        'oExcel.worksheets("Hoja1").Columns("I").Font.Size = 8
        'oExcel.worksheets("Hoja1").Columns("J").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        'oExcel.worksheets("Hoja1").Columns("J").Font.Size = 8
        'oExcel.worksheets("Hoja1").Columns("K").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        'oExcel.worksheets("Hoja1").Columns("K").Font.Size = 8
        'oExcel.worksheets("Hoja1").Columns("L").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        'oExcel.worksheets("Hoja1").Columns("L").Font.Size = 8


        'Cambia el alto de celda 
        oSheet.range("A:H").RowHeight = 13

        'oSheet.range("A:V").HorizontalAlignment = xlCenter

        'TAMAÑO DE COLUMNAS
        oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 12
        oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 20
        oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 8
        oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 15
        oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 15
        'oExcel.worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 11
        'oExcel.worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 12
        'oExcel.worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 6
        'oExcel.worksheets("Hoja1").Columns("L").EntireColumn.ColumnWidth = 6



        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = Me.DGFacturas.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel1 As String = Me.DGFacturas.Item(0, fila_dt).Value
            Dim cel2 As Date = IIf(IsDBNull(Me.DGFacturas.Item(1, fila_dt).Value), "12/12/1999", Me.DGFacturas.Item(1, fila_dt).Value)
            Dim cel3 As String = IIf(IsDBNull(Me.DGFacturas.Item(2, fila_dt).Value), "", Me.DGFacturas.Item(2, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DGFacturas.Item(3, fila_dt).Value), "", Me.DGFacturas.Item(3, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DGFacturas.Item(4, fila_dt).Value), "", Me.DGFacturas.Item(4, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DGFacturas.Item(5, fila_dt).Value), "", Me.DGFacturas.Item(5, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DGFacturas.Item(6, fila_dt).Value), "", Me.DGFacturas.Item(6, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DGFacturas.Item(7, fila_dt).Value), "", Me.DGFacturas.Item(7, fila_dt).Value)

            fila_dt_excel = fila_dt + 7 'Renglón en donde se empieza a registrar el reporte

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
            oSheet.range("D" & fila_dt_excel).value = cel4
            oSheet.range("E" & fila_dt_excel).value = cel5
            oSheet.range("F" & fila_dt_excel).value = cel6
            oSheet.range("G" & fila_dt_excel).value = cel7
            oSheet.range("H" & fila_dt_excel).value = cel8


            oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "##.#### %"

            Estatus.Visible = True
            ProgressBar1.Value = (fila_dt * 100) / total_reg

        Next

        Estatus.Visible = False

        'Formato de texto para la primera columna CLAVE ART
        oExcel.Worksheets("Hoja1").Columns("A").NumberFormat = "@"

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        'oSheet.columns("A:O").entirecolumn.autofit()
        oSheet.range("A1").value = "Descuentos de Facturas "
        oSheet.range("A2").value = "Sucursal: " + CmbSucursal.Text
        oSheet.range("A3").value = "Agente: " + CmbAgteVta.Text
        oSheet.range("A4").value = "Periodo de ventas del: " + DtpFechaIni.Value + " al " + DtpFechaTer.Value

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)


        'COMBINAMOS CELDAS
        oSheet.Range("A1:E1").Merge(True)
        oSheet.Range("A2:E2").Merge(True)
        oSheet.Range("A3:E3").Merge(True)
        oSheet.Range("A4:E4").Merge(True)
        'oSheet.Range("A5:E5").Merge(True)

        oSheet.range("A1:E1").font.bold = True
        oSheet.range("A2:E2").font.bold = True
        oSheet.range("A3:E3").font.bold = True
        oSheet.range("A4:E4").font.bold = True
        'oSheet.range("A5:E5").font.bold = True

        'DAR COLOR DE FONDO A CELDAS
        oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
        oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15
        oSheet.Range("A3:C3").INTERIOR.COLORINDEX = 15
        oSheet.Range("A4:C4").INTERIOR.COLORINDEX = 15
        'oSheet.Range("A5:C5").INTERIOR.COLORINDEX = 15


        oSheet.Range("A6").INTERIOR.COLORINDEX = 15
        oSheet.Range("B6").INTERIOR.COLORINDEX = 15
        oSheet.Range("C6").INTERIOR.COLORINDEX = 15
        oSheet.Range("D6").INTERIOR.COLORINDEX = 15
        oSheet.Range("E6").INTERIOR.COLORINDEX = 15
        oSheet.Range("F6").INTERIOR.COLORINDEX = 15
        oSheet.Range("G6").INTERIOR.COLORINDEX = 15
        oSheet.Range("H6").INTERIOR.COLORINDEX = 15
        oSheet.Range("I6").INTERIOR.COLORINDEX = 15
        oSheet.Range("J6").INTERIOR.COLORINDEX = 15
        'oSheet.Range("K7").INTERIOR.COLORINDEX = 15



        'Declaramos el nombre de las columnas
        oSheet.range("A6").value = "Artículo"
        oSheet.range("B6").value = "Descripción"
        oSheet.range("C6").value = "Cantidad"
        oSheet.range("D6").value = "Lista de precios"
        oSheet.range("E6").value = "Precio"
        oSheet.range("F6").value = "Descuento"
        oSheet.range("G6").value = "Precio tras descuento"
        oSheet.range("H6").value = "Total"
        oSheet.range("I6").value = "Comisión"
        oSheet.range("J6").value = "Factura"

        'oSheet.range("I6").value = "Descripción"
        'oSheet.range("J6").value = "Linea"
        'oSheet.range("K6").value = "Pedido Cliente"
        'oSheet.range("L6").value = "Facturado"
        'oSheet.range("M6").value = "Back Order"


        'DISEÑO DE EXCEL

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A6:J6").font.bold = True


        oExcel.worksheets("Hoja1").Columns("A").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("A").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("B").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("B").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("C").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("C").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("D").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("D").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("E").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("E").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("F").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("F").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("G").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("G").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("H").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("H").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("I").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("I").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("J").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("J").Font.Size = 8
        'oExcel.worksheets("Hoja1").Columns("K").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        'oExcel.worksheets("Hoja1").Columns("K").Font.Size = 8
        'oExcel.worksheets("Hoja1").Columns("L").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        'oExcel.worksheets("Hoja1").Columns("L").Font.Size = 8


        'Cambia el alto de celda 
        oSheet.range("A:H").RowHeight = 13

        'oSheet.range("A:V").HorizontalAlignment = xlCenter

        'TAMAÑO DE COLUMNAS
        oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 14
        oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 18
        oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 8
        oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 9
        oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 16
        oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 16
        oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 16
        oExcel.worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 8
        oExcel.worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 12
        'oExcel.worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 6
        'oExcel.worksheets("Hoja1").Columns("L").EntireColumn.ColumnWidth = 6



        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = Me.DGDetalle.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel1 As String = Me.DGDetalle.Item(0, fila_dt).Value
            Dim cel2 As String = Me.DGDetalle.Item(1, fila_dt).Value
            Dim cel3 As String = IIf(IsDBNull(Me.DGDetalle.Item(2, fila_dt).Value), "", Me.DGDetalle.Item(2, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DGDetalle.Item(3, fila_dt).Value), "", Me.DGDetalle.Item(3, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DGDetalle.Item(4, fila_dt).Value), "", Me.DGDetalle.Item(4, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DGDetalle.Item(5, fila_dt).Value), "", Me.DGDetalle.Item(5, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DGDetalle.Item(6, fila_dt).Value), "", Me.DGDetalle.Item(6, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DGDetalle.Item(7, fila_dt).Value), "", Me.DGDetalle.Item(7, fila_dt).Value)
            Dim cel9 As String = IIf(IsDBNull(Me.DGDetalle.Item(8, fila_dt).Value), "", Me.DGDetalle.Item(8, fila_dt).Value)
            Dim cel10 As String = IIf(IsDBNull(Me.DGDetalle.Item(9, fila_dt).Value), "", Me.DGDetalle.Item(9, fila_dt).Value)

            fila_dt_excel = fila_dt + 7 'Renglón en donde se empieza a registrar el reporte

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = cel3 'Da formato número con dos decimales a la columna C
            oSheet.range("D" & fila_dt_excel).value = cel4
            oSheet.range("E" & fila_dt_excel).value = cel5
            oSheet.range("F" & fila_dt_excel).value = cel6
            oSheet.range("G" & fila_dt_excel).value = cel7
            oSheet.range("H" & fila_dt_excel).value = cel8
            oSheet.range("I" & fila_dt_excel).value = cel9
            oSheet.range("J" & fila_dt_excel).value = cel10

            oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "##.#### %"
            oExcel.Worksheets("Hoja1").Columns("I").NumberFormat = "##.#### %"

            oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "$ ###,###,###.##"
            oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "$ ###,###,###.##"
            oExcel.Worksheets("Hoja1").Columns("H").NumberFormat = "$ ###,###,###.##"



            Estatus.Visible = True
            ProgressBar1.Value = (fila_dt * 100) / total_reg

        Next

        Estatus.Visible = False

        'Formato de texto para la primera columna CLAVE ART
        oExcel.Worksheets("Hoja1").Columns("A").NumberFormat = "@"

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        'oSheet.columns("A:O").entirecolumn.autofit()
        oSheet.range("A1").value = "Descuentos de Facturas (DETALLE)"
        oSheet.range("A2").value = "Factura:  " & DGFacturas.Item(0, DGFacturas.CurrentRow.Index).Value.ToString & "        Fecha: " & DGFacturas.Item(1, DGFacturas.CurrentRow.Index).Value.ToString
        oSheet.range("A3").value = "Cliente: " & DGFacturas.Item(2, DGFacturas.CurrentRow.Index).Value.ToString & "     " & DGFacturas.Item(3, DGFacturas.CurrentRow.Index).Value.ToString
        oSheet.range("A4").value = "Agente " & DGFacturas.Item(5, DGFacturas.CurrentRow.Index).Value.ToString & "   Autoriza: " & DGFacturas.Item(7, DGFacturas.CurrentRow.Index).Value.ToString _
             & "    " & DGFacturas.Item(6, DGFacturas.CurrentRow.Index).Value.ToString * 100 & " %"

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing
    End Sub
End Class