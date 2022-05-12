Public Class Cliente
    
    Dim DTMObra As New DataTable


    Private Sub Cliente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        
        Dim ConsutaLista As String

        ConsutaLista = "SELECT Cardcode,Cardname FROM OCRD WHERE CardType = 'C' ORDER BY Cardname"
        Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, StrCon)

        Dim dsArt As New DataSet
        daArticulo.Fill(dsArt)

        Me.CmbACob.DataSource = dsArt.Tables(0)
        Me.CmbACob.DisplayMember = "Cardname"
        Me.CmbACob.ValueMember = "Cardcode"

        CmbACob.SelectedValue = ""
        CmbACob.SelectedValue = ""


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If CmbACob.SelectedValue = "" Then
            MessageBox.Show("Sleccione un cliente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.CmbACob.Focus()
            Return
        End If

        LblCliente.Text = CmbACob.Text
        LblCliente.Visible = True
        cargar_registros()
    End Sub

    Sub cargar_registros()

        DTMObra.Clear()

        Dim Consulta As String = ""
        Dim strcadena As String = ""
        Dim CTabla As String = ""


        Consulta = "SELECT T9.DocNum AS DocSAP,T9.EDocNum AS FactFiscal,T9.CardCode,t9.CardName,T9.DocDate as FchDoc,T9.DocDueDate as FchVenci,"
        Consulta &= "CAST(0 AS numeric(19,6)) as DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,CAST(0 AS numeric(19,6)) AS SaldoPendiene,T9.NumAtCard AS FactSaldo,"
        Consulta &= "t9.Comments,CAST(0 AS int) as IdPago,T9.EDocPrefix as TipoMov,"
        Consulta &= "CASE "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'I' THEN 'CANCELACION: ' "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'S'  THEN 'DESCUENTO: ' "
        Consulta &= "WHEN T9.EDocPrefix = 'ND' THEN 'DEVOLUCION: ' "
        Consulta &= "END  + CONVERT(nvarchar(20),T10.DocNum) AS Movimiento,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS Cargo,T9.DocTotal AS Abono,CAST(3 AS int) as Orden,"
        Consulta &= "T9.DocNum as NumNota,T10.DocTotal - T10.PaidToDate as MPendiene "
        Consulta &= "into #TmpNotaCSAP"
        Consulta &= " FROM ORIN T9, OINV T10 WHERE T9.CardCode = @Cliente AND RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.DocNum AS nvarchar(100)))) AND T10.CardCode = @Cliente "

        Consulta &= " SELECT T8.DocNum AS DocSAP,T8.EDocNum AS FactFiscal,T8.CardCode,T8.CardName,T8.DocDate as FchDoc,T8.DocDueDate as FchVenci,"
        Consulta &= "T8.DocTotal,T8.PaidToDate AS Aplicado,T8.DocTotal - T8.PaidToDate as SaldoPendiene,T8.NumAtCard AS FactSaldo,CAST('' AS nvarchar(254)) AS Comments,"
        Consulta &= "CAST(0 AS int) as IdPago,CAST('' AS nvarchar(10)) AS TipoMov, 'FACTURA: ' + CAST(T8.DocNum AS  nvarchar(20)) AS Movimiento,"
        Consulta &= "T8.DocTotal AS Cargo,CAST(0 AS numeric(19,6)) AS Abono,CAST(1 AS int) as Orden,CAST(0 AS int) as NumNota,T8.DocTotal - T8.PaidToDate as MPendiene "
        Consulta &= "into #TmpFacPag"
        Consulta &= " FROM OINV T8 WHERE T8.CardCode = @Cliente"

        Consulta &= " UNION ALL "

        Consulta &= "SELECT T8.DocNum AS DocSAP,T8.EDocNum AS FactFiscal,T6.CardCode,T6.CardName,T6.DocDate as FchDoc,T6.DocDueDate as FchVenci,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,CAST(0 AS numeric(19,6)) as SaldoPendiene,CAST('' AS nvarchar(100)) AS FactSaldo,T6.Comments,"
        Consulta &= "T7.DocNum as IdPago,CAST('' AS nvarchar(10)) AS TipoMov,  'PAGO: ' + CAST(T8.DocNum AS  nvarchar(20)) AS Movimiento,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS Cargo,T7.SumApplied  AS Abono,CAST(2 AS int) as Orden,CAST(0 AS int) as NumNota,"
        Consulta &= "T8.DocTotal - T8.PaidToDate as MPendiene"
        Consulta &= " FROM ORCT T6 INNER JOIN RCT2 T7 ON T6.DocNum = T7.DocNum"
        Consulta &= " INNER JOIN OINV T8 ON T8.DocEntry = T7.DocEntry AND T7.InvType = 13"
        Consulta &= " WHERE T6.CardCode = @Cliente AND T6.Canceled = 'N' AND T6.DocTotal <> .01"

        Consulta &= " UNION ALL "

        Consulta &= "SELECT CAST(FactSaldo AS int) AS DocSAP,FactFiscal,CardCode,CardName,FchDoc,FchVenci,"
        Consulta &= "DocTotal,Aplicado,SaldoPendiene,CAST(DocSAP AS nvarchar(100))AS FactSaldo,"
        Consulta &= "Comments,IdPago,TipoMov,"
        Consulta &= "Movimiento, Cargo, Abono, Orden, NumNota, MPendiene "
        Consulta &= "FROM #TmpNotaCSAP"

        Consulta &= " UNION ALL "

        Consulta &= "SELECT t10.docnum AS DocNum,T9.EDocNum AS FactFiscal,T9.CardCode,t9.CardName,T9.DocDate as FchDoc,T9.DocDueDate as FchVenci,"
        Consulta &= "CAST(0 AS numeric(19,6)) as DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,CAST(0 AS numeric(19,6)) AS SaldoPendiene,CAST(T9.DocNum  AS nvarchar(100))AS FactSaldo,"
        Consulta &= "t9.Comments,CAST(0 AS int) as IdPago,T9.EDocPrefix as TipoMov,"
        Consulta &= "CASE "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'I' THEN 'CANCELACION: ' "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'S'  THEN 'DESCUENTO: ' "
        Consulta &= "WHEN T9.EDocPrefix = 'ND' THEN 'DEVOLUCION: ' "
        Consulta &= "END + CONVERT(nvarchar(20),T10.DocNum) AS Movimiento,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS Cargo,T9.DocTotal AS Abono,CAST(3 AS int) as Orden,"
        Consulta &= "T9.DocNum as NumNota,T10.DocTotal - T10.PaidToDate as MPendiene "
        Consulta &= "FROM ORIN T9, OINV T10 WHERE T9.CardCode = @Cliente AND RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.EDocNum AS nvarchar(100)))) AND T10.CardCode = @Cliente"

        Consulta &= " UNION ALL "

        Consulta &= "SELECT t10.docnum AS DocNum,T9.EDocNum AS FactFiscal,T9.CardCode,t9.CardName,T9.DocDate as FchDoc,T9.DocDueDate as FchVenci,"
        Consulta &= "CAST(0 AS numeric(19,6)) as DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,CAST(0 AS numeric(19,6)) AS SaldoPendiene,CAST(T9.DocNum  AS nvarchar(100))AS FactSaldo,"
        Consulta &= "t9.Comments,CAST(0 AS int) as IdPago,T9.EDocPrefix as TipoMov,"
        Consulta &= "CASE "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'I' THEN 'CANCELACION: ' "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'S'  THEN 'DESCUENTO: ' "
        Consulta &= "WHEN T9.EDocPrefix = 'ND' THEN 'DEVOLUCION: ' "
        Consulta &= "END + CONVERT(nvarchar(20),T10.DocNum) AS Movimiento,"
        Consulta &= "CAST(0 AS numeric(19,6)) AS Cargo,T9.DocTotal AS Abono,CAST(3 AS int) as Orden,"
        Consulta &= "T9.DocNum as NumNota,T10.DocTotal - T10.PaidToDate as MPendiene "
        Consulta &= "FROM ORIN T9, OINV T10 WHERE T9.CardCode = @Cliente AND RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(T10.NumAtCard)) AND T10.CardCode = @Cliente"

        Consulta &= " UNION ALL "

        Consulta &= "SELECT T9.DocNum AS DocSAP,T9.EDocNum AS FactFiscal,T9.CardCode,t9.CardName,T9.DocDate as FchDoc,T9.DocDueDate as FchVenci,"
        Consulta &= "CAST(0 AS numeric(19,6)) as DocTotal,CAST(0 AS numeric(19,6)) AS Aplicado,CAST(0 AS numeric(19,6)) AS SaldoPendiene,T9.NumAtCard AS FactSaldo,"
        Consulta &= "t9.Comments,CAST(0 AS int) as IdPago,T9.EDocPrefix as TipoMov,"
        Consulta &= "CASE "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND T9.DocType  = 'I' THEN 'CANCELACION: ' "
        Consulta &= "WHEN (T9.EDocPrefix = 'NC' or T9.EDocPrefix is null or T9.EDocPrefix = 'F') AND DocType  = 'S'  THEN 'DESCUENTO: ' "
        Consulta &= "WHEN T9.EDocPrefix = 'ND' THEN 'DEVOLUCION: ' "
        Consulta &= "END + CONVERT(nvarchar(20),T9.DocNum) AS Movimiento,CAST(0 AS numeric(19,6)) as Cargo,T9.DocTotal AS Abono,CAST(3 AS int) as Orden,T9.DocNum AS NumNota,"
        Consulta &= "CAST(0 AS numeric(19,6)) as MPendiene "
        Consulta &= "FROM ORIN T9 WHERE T9.CardCode = @Cliente AND "
        Consulta &= "T9.DocNum NOT IN (SELECT NumNota FROM #TmpNotaCSAP UNION ALL "

        Consulta &= "SELECT T9.DocNum as NumNota "
        Consulta &= "FROM ORIN T9, OINV T10 WHERE T9.CardCode = @Cliente AND RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(CAST(T10.EDocNum AS nvarchar(100)))) AND T10.CardCode = @Cliente"


        Consulta &= " UNION ALL "

        Consulta &= "SELECT T9.DocNum as NumNota "
        Consulta &= "FROM ORIN T9, OINV T10 WHERE T9.CardCode = @Cliente AND RTRIM(LTRIM(T9.NumAtCard)) = RTRIM(LTRIM(T10.NumAtCard)) AND T10.CardCode = @Cliente) "

        Consulta &= " SELECT DocSAP,FactFiscal,FchDoc,FchVenci,DocTotal,Aplicado,SaldoPendiene,Movimiento,"
        Consulta &= "Cargo,Abono,Comments,IdPago,NumNota,MPendiene FROM #TmpFacPag order by DocSAP,Orden ASC "

        'Consulta &= " SELECT * FROM #TmpFacPag "
        Consulta &= "DROP TABLE #TmpNotaCSAP "
        Consulta &= "DROP TABLE #TmpFacPag "

        Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

        CmdMObra.Parameters.Add("@Cliente", SqlDbType.NVarChar)
        CmdMObra.Parameters("@Cliente").Value = Me.CmbACob.SelectedValue


        CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
        CmdMObra.Connection.Open()

        Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
        AdapMObra.Fill(DTMObra)
        CmdMObra.Connection.Close()


        'Dim CmdSaldo As New SqlClient.SqlCommand(Consulta)
        'CmdSaldo.Parameters.Add("@Cliente", SqlDbType.NVarChar)
        'CmdSaldo.Parameters("@Cliente").Value = Me.CmbACob.SelectedValue
        'Dim AdapCliente As New SqlClient.SqlDataAdapter(CmdMObra)
        'AdapMObra.Fill(DTMObra)

        Dim vSaldo As Decimal
        Consulta = "SELECT SUM(T8.DocTotal - T8.PaidToDate) as SaldoPendiene"
        Consulta &= " FROM OINV T8 WHERE T8.CardCode = @Cliente"
        Dim CmdTotTickets As New Data.SqlClient.SqlCommand
        With CmdTotTickets
            .Parameters.AddWithValue("@Cliente", CmbACob.SelectedValue)
            .CommandText = Consulta
            .CommandType = CommandType.Text
            .Connection = New Data.SqlClient.SqlConnection(StrCon)
            .Connection.Open()
            vSaldo = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
            .Connection.Close()
        End With

        LblCliente.Text = LblCliente.Text + "  --  Saldo de Cuenta : " + Format(CDec(vSaldo), "###,###,###.00")

        With Me.GrdConProd
            .DataSource = DTMObra 'DTMObra ' 'DtUsrCob 'DtOrdenaCob 'DtPagoCob 'DtOrdenaCob 'DTRefacciones
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
            .MultiSelect = False
            .AllowUserToAddRows = False
            'Color de linea del grid
            .Columns(0).HeaderText = "Documento SAP"
            .Columns(0).Width = 80
            .Columns(1).HeaderText = "Factura Fiscal"
            .Columns(1).Width = 80
            .Columns(2).HeaderText = "Fecha Documento"
            .Columns(2).Width = 70
            .Columns(3).HeaderText = "Fecha Vencimiento"
            .Columns(3).Width = 70
            .Columns(4).HeaderText = "Total Factura"
            .Columns(4).Width = 80
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(5).HeaderText = "Monto Aplicado"
            .Columns(5).Width = 80
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(6).HeaderText = "Saldo Pendiente"
            .Columns(6).Width = 80
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(7).HeaderText = "Descripción de Movimiento"
            .Columns(7).Width = 150
            .Columns(8).HeaderText = "Cargo"
            .Columns(8).Width = 80
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(9).HeaderText = " Abono"
            .Columns(9).Width = 80
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(10).HeaderText = "Comentario Nota de Credito"
            .Columns(10).Width = 150
            .Columns(11).HeaderText = "Número de Pago"
            .Columns(11).Width = 50
            .Columns(12).HeaderText = "Nota de Credito"
            .Columns(12).Width = 80
            .Columns(13).Visible = False
        End With

    End Sub

    Private Sub BtnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExcel.Click

        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)

        'Declaramos el nombre de las columnas

        oSheet.range("A6").value = "Documento SAP"
        oSheet.range("B6").value = "Fact. Fiscal"
        oSheet.range("C6").value = "Fecha Doc."
        oSheet.range("D6").value = "Fecha Ven."
        oSheet.range("E6").value = "Total Factura"
        oSheet.range("F6").value = "Monto Aplicado"
        oSheet.range("G6").value = "Saldo Pendiente"
        oSheet.range("H6").value = "Descripción de Movimiento"
        oSheet.range("I6").value = "Cargo"
        oSheet.range("J6").value = "Abono"
        oSheet.range("K6").value = "Comentario Nota de Credito"
        oSheet.range("L6").value = "Número de Pago"
        oSheet.range("M6").value = "Nota de Credito"
        

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A6:M6").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = Me.GrdConProd.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel1 As String = Me.GrdConProd.Item(0, fila_dt).Value
            Dim cel2 As String = IIf(IsDBNull(Me.GrdConProd.Item(1, fila_dt).Value), 0, Me.GrdConProd.Item(1, fila_dt).Value)
            Dim cel3 As String = IIf(IsDBNull(Me.GrdConProd.Item(2, fila_dt).Value), 0, Me.GrdConProd.Item(2, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.GrdConProd.Item(3, fila_dt).Value), 0, Me.GrdConProd.Item(3, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.GrdConProd.Item(4, fila_dt).Value), 0, Me.GrdConProd.Item(4, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.GrdConProd.Item(5, fila_dt).Value), 0, Me.GrdConProd.Item(5, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.GrdConProd.Item(6, fila_dt).Value), 0, Me.GrdConProd.Item(6, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.GrdConProd.Item(7, fila_dt).Value), 0, Me.GrdConProd.Item(7, fila_dt).Value)

            Dim cel9 As String = IIf(IsDBNull(Me.GrdConProd.Item(8, fila_dt).Value), 0, Me.GrdConProd.Item(8, fila_dt).Value)
            Dim cel10 As String = IIf(IsDBNull(Me.GrdConProd.Item(9, fila_dt).Value), 0, Me.GrdConProd.Item(9, fila_dt).Value)

            Dim cel11 As String = IIf(IsDBNull(Me.GrdConProd.Item(10, fila_dt).Value), 0, Me.GrdConProd.Item(10, fila_dt).Value)
            Dim cel12 As String = IIf(IsDBNull(Me.GrdConProd.Item(11, fila_dt).Value), 0, Me.GrdConProd.Item(11, fila_dt).Value)
            Dim cel13 As String = IIf(IsDBNull(Me.GrdConProd.Item(12, fila_dt).Value), 0, Me.GrdConProd.Item(12, fila_dt).Value)
            

            'Dim cel11 As String = IIf(IsDBNull(Me.GrdConProd.Item(10, fila_dt).Value), 0, Me.GrdConProd.Item(10, fila_dt).Value)
            'Dim cel12 As String = IIf(IsDBNull(Me.GrdConProd.Item(11, fila_dt).Value), 0, Me.GrdConProd.Item(11, fila_dt).Value)

            fila_dt_excel = fila_dt + 7 'Renglón en donde se empieza a registrar el reporte

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = cel3
            oSheet.range("D" & fila_dt_excel).value = cel4
            oSheet.range("E" & fila_dt_excel).value = FormatNumber(cel5, 2)
            oSheet.range("F" & fila_dt_excel).value = FormatNumber(cel6, 2)
            oSheet.range("G" & fila_dt_excel).value = FormatNumber(cel7, 2)
            oSheet.range("H" & fila_dt_excel).value = cel8

            oSheet.range("I" & fila_dt_excel).value = FormatNumber(cel9, 2)
            oSheet.range("J" & fila_dt_excel).value = FormatNumber(cel10, 2)
            oSheet.range("K" & fila_dt_excel).value = cel11
            oSheet.range("L" & fila_dt_excel).value = cel12
            oSheet.range("M" & fila_dt_excel).value = cel13


        Next

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        oSheet.columns("A:M").entirecolumn.autofit()
        oSheet.range("A1").value = "Reporte de saldo de Cuenta"
        oSheet.range("A2").value = "Cliente : " + LblCliente.Text
        oSheet.range("A3").value = Format(Date.Now, " dd/MM/yyyy")


        'oSheet.range("A2").value = "Periodo de Ventas De" + Format(Me.DtpPVtaIni.Value, " dd/MM/yyyy") + " A " + Format(Me.DtpPVtaTer.Value, " dd/MM/yyyy")
        'oSheet.range("A3").value = "Periodo de Pagos De" + Format(Me.DtpPPagoIni.Value, " dd/MM/yyyy") + " A " + Format(Me.DtpPPagoTer.Value, " dd/MM/yyyy")
        'oSheet.range("A4").value = "Periodo de Devoluciones De" + Format(Me.DtpPDevIni.Value, " dd/MM/yyyy") + " A " + Format(Me.DtpPDevTer.Value, " dd/MM/yyyy")
        'oSheet.range("A5").value = "Periodo de Ventas De" + Format(Me.DtpDesIni.Value, " dd/MM/yyyy") + " A " + Format(Me.DtpDesTer.Value, " dd/MM/yyyy")

        'oSheet.range("C1").value = Rangos
        'oSheet.range("C2").value = Rangos2
        'Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim FilRef As String = "Id_Wom = '" + fila("Id_Wom") + "'"
        'Dim DTFilRef As DataTable
        'DTFilRef = DTRefacciones.Clone()
        'Dim RowsREf As DataRow()
        'RowsREf = DTRefacciones.Select(FilRef)
        'For Each ldrRow As DataRow In RowsREf
        '    DTFilRef.ImportRow(ldrRow)
        'Next

        'Dim FilRef As String = "MPendiene > 0"
        'Dim RowsREf As DataRow
        'RowsREf = DTMObra.Select(FilRef)







    End Sub

    Private Sub ChkVisualiza_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkVisualiza.CheckedChanged
        If ChkVisualiza.Checked = True Then
            Dim FilRef As String = "MPendiene > 0"
            Dim DTFilRef As DataTable
            DTFilRef = DTMObra.Clone()
            Dim RowsREf As DataRow()
            RowsREf = DTMObra.Select(FilRef)
            For Each ldrRow As DataRow In RowsREf
                DTFilRef.ImportRow(ldrRow)
            Next

            Me.GrdConProd.DataSource = DTFilRef
        Else
            Me.GrdConProd.DataSource = DTMObra
        End If
    End Sub
End Class