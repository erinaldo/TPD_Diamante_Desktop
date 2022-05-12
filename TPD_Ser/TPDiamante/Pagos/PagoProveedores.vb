Imports System.Data.SqlClient

Public Class PagoProveedores
    Dim DvFactProv As New DataView
    Dim DvFactProv2 As New DataView
    Dim DvProveedor As New DataView
    Dim amarillo As Color

    Sub cargar_registros()

        Dim DTRefacciones As New DataTable

        ' crear nueva conexión    
        Dim conexion2 As New SqlConnection(StrCon)

        ' abrir la conexión con la base de datos   
        conexion2.Open()

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand

        Dim SQLTPD As String

        SQLTPD = "SELECT CASE WHEN T3.FactCompras IS NULL OR T3.FactCompras=0 THEN 0 ELSE 1 END AS RegSel, "
        SQLTPD &= "T0.DocDate AS FchDoc, "
        SQLTPD &= "T2.PymntGroup AS DiasCred,t1.U_BXP_BancoOrigen as BancoOrigen,t1.U_BXP_BancoDestino as BancoDestino,t1.U_BXP_CtaDestino as CuentaDestino,T0.DocNum AS Factura, CASE WHEN T0.FolioPref IS NULL THEN '' ELSE T0.FolioPref END "
        SQLTPD &= "+ CASE WHEN CAST(T0.U_Factura AS nvarchar(10)) IS NULL THEN '' ELSE CAST(T0.U_Factura AS nvarchar(10)) END AS FactProv,"
        SQLTPD &= "CASE WHEN U_IdLinea IS NULL THEN '' ELSE U_IdLinea END U_IdLinea,"
        SQLTPD &= "T1.CardCode AS IdProved,T1.CardName AS Proveedor, 'a' as 'TipoGasto',"
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal ELSE T0.DocTotalFC END AS TotFactura,T0.DocCur AS Moneda,"
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.PaidToDate ELSE T0.PaidFC END AS Pagado,"
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal - T0.PaidToDate ELSE T0.DocTotalFC - T0.PaidFC END AS SaldoPendiente,"
        SQLTPD &= "T0.DocCur AS Moneda2,DATEDIFF(DAY,T0.DocDueDate,GETDATE()) AS DiasAtraso,T0.DocDueDate AS FchVen,"
        SQLTPD &= "case when T0.u_bxp_descvarios < 0 or T0.u_bxp_descvarios is null then (convert(varchar,convert(int,round(t1.U_BXP_Descuentospp,0))))+'%' else case when t1.U_BXP_Descuentospp  < 0 or T0.u_bxp_descvarios is null then (convert(varchar,0))+'%' else (convert(varchar,convert(int,round(t1.U_BXP_Descuentospp,0))))+'%' end end as 'Descuento',"
        SQLTPD &= "T0.DocTotal - T0.PaidToDate AS SaldoPesos, t0.NumAtCard Referencia, T1.Notes AS Obrserv,T4.Coment,T3.Fecha as 'FechaPagada' "
        SQLTPD &= "FROM [SBO_TPD].dbo.OPCH T0 "
        SQLTPD &= "INNER JOIN [SBO_TPD].dbo.OCRD T1 ON T0.CardCode = T1.CardCode AND (T1.CardCode LIKE '%P-%' OR T1.CardCode LIKE '%PIM-%') "
        SQLTPD &= "LEFT JOIN [SBO_TPD].dbo.OCTG T2 ON T2.GroupNum = T1.GroupNum "
        SQLTPD &= "LEFT JOIN [TPM].dbo.FCOMP T3 ON T0.DocNum = T3.FactCompras "
        SQLTPD &= "LEFT JOIN [TPM].dbo.COMP1 T4 ON T0.DocNum = T4.FactCompras "
        SQLTPD &= "WHERE T0.DocDueDate >= @FechaIni AND T0.DocDueDate <= @FechaTer "

        If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
            SQLTPD &= "AND T0.CardCode = @Agente "
        End If
        SQLTPD &= "AND T0.DocStatus = 'O' ORDER BY T0.DocDueDate, DiasAtraso, T0.DocNum DESC"

        ' Nuevo objeto Dataset   
        Dim DsVtasDet As New DataSet

        DsVtasDet.Tables.Add(DTRefacciones)

        With comando
            If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
                .Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
            End If
            .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
            .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
            ' Asignar el sql para seleccionar los datos de la tabla Maestro   
            .CommandText = SQLTPD
            .Connection = conexion2
        End With


        Dim DtFactProv As New DataTable

        With Adaptador
            .SelectCommand = comando
            ' llenar el dataset   
            .Fill(DtFactProv)
        End With

        DvFactProv = DtFactProv.DefaultView

        With Me.DgFactProv
            .DataSource = DvFactProv

            .AllowUserToAddRows = False

            '.Columns(7).HeaderText = "$Total Factura"
            .Columns("TotFactura").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("TotFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Mnd
            '.Columns(8).HeaderText = "MND"
            .Columns("Moneda").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Importe Aplicado
            '.Columns(9).HeaderText = "Importe Aplicado"
            .Columns("Pagado").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("Pagado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Saldo Pendiente
            '.Columns(10).HeaderText = "$Saldo Pend"
            .Columns("SaldoPendiente").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("SaldoPendiente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Mnd
            '.Columns(11).HeaderText = "MND"
            .Columns("Moneda2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Dias Atraso
            '.Columns(12).HeaderText = "ID prov"
            .Columns("DiasAtraso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Fch Venc.
            '.Columns(13).HeaderText = "Fch venc."

            '$ Saldo MXP
            '.Columns(14).HeaderText = "$ Saldo MXP"
            .Columns("SaldoPesos").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("SaldoPesos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            '.Columns("Referencia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            'Observ
            '.Columns(15).HeaderText = "Observaciones"
            .Columns("Obrserv").Visible = False
        End With



        With conexion2
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With

    End Sub

    Sub cargar_registros2()

        Dim DTRefacciones As New DataTable

        ' crear nueva conexión    
        Dim conexion2 As New SqlConnection(StrCon)

        ' abrir la conexión con la base de datos   
        conexion2.Open()

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand

        Dim SQLTPD As String

        SQLTPD = "SELECT CASE WHEN T3.FactCompras IS NULL OR T3.FactCompras=0 THEN 0 ELSE 1 END AS RegSel,T0.DocDate AS FchDoc,"
        SQLTPD &= "T2.PymntGroup AS DiasCred,t1.U_BXP_BancoOrigen as BancoOrigen,t1.U_BXP_BancoDestino as BancoDestino,t1.U_BXP_CtaDestino as CuentaDestino,T0.DocNum AS Factura,CASE WHEN T0.FolioPref IS NULL THEN '' ELSE T0.FolioPref END "
        SQLTPD &= "+ CASE WHEN CAST(T0.FolioNum AS nvarchar(10)) IS NULL THEN '' ELSE CAST(T0.FolioNum AS nvarchar(10)) END AS FactProv,"
        SQLTPD &= "CASE WHEN U_IdLinea IS NULL THEN '' ELSE U_IdLinea END U_IdLinea,"
        SQLTPD &= "T1.CardCode AS IdProved,T1.CardName AS Proveedor, t5.Name as 'TipoGasto', "
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal ELSE T0.DocTotalFC END AS TotFactura,T0.DocCur AS Moneda,"
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.PaidToDate ELSE T0.PaidFC END AS Pagado,"
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal - T0.PaidToDate ELSE T0.DocTotalFC - T0.PaidFC END AS SaldoPendiente,"
        SQLTPD &= "T0.DocCur AS Moneda2,DATEDIFF(DAY,T0.DocDueDate,GETDATE()) AS DiasAtraso,T0.DocDueDate AS FchVen,"
        SQLTPD &= "T0.DocTotal - T0.PaidToDate AS SaldoPesos,t0.NumAtCard Referencia,T1.Notes AS Obrserv,T4.Coment, T3.Fecha as 'FechaPagada' "
        'SQLTPD &= ", '' Descuento"
        SQLTPD &= "FROM OPCH t0 inner join OCRD t1 on t0.CardCode = t1.CardCode left join [@BXP_CONCEPTO] t5 on t1.U_BXP_CONCEPTO = t5.Code "
        SQLTPD &= "left join OCTG t2 on t1.GroupNum = t2.GroupNum "
        SQLTPD &= "LEFT JOIN [TPM].dbo.FCOMP T3 ON t0.DocNum = T3.FactCompras "
        SQLTPD &= "LEFT JOIN [TPM].dbo.COMP1 T4 ON T0.DocNum = T4.FactCompras "
        SQLTPD &= "where t0.CardCode like 'PG%' and t0.DocDueDate >= @FechaIni and t0.DocDueDate <= @FechaTer "
        'SQLTPD &= "WHERE T0.DocDueDate >= @FechaIni AND T0.DocDueDate <= @FechaTer "

        If CmbProveedor.SelectedIndex <> -1 Then
            If Me.CmbProveedor.SelectedValue <> "TODOS" Then
                SQLTPD &= "AND T0.CardCode = @Agente "
            ElseIf Me.CmbGasto.SelectedValue <> "TODOS" Then
                ''MsgBox("voy a vuscar todos de un solo tipo de gasto")
                ''MsgBox("seleccionaste el: " & CmbGasto.SelectedValue.ToString)
                SQLTPD &= "AND T0.CardCode in (select CardCode from OCRD where CardType = 'S' and CardCode like  'PG%' and U_BXP_CONCEPTO = @Concepto )  "
            Else
                SQLTPD &= "AND T0.CardCode in (select CardCode from OCRD where CardType = 'S' and CardCode like  'PG%' )"
            End If

        End If
        SQLTPD &= "AND T0.DocStatus = 'O' ORDER BY T0.DocDueDate, DiasAtraso, T0.DocNum DESC "
        ' MsgBox(SQLTPD)

        ' Nuevo objeto Dataset   
        Dim DsVtasDet As New DataSet

        DsVtasDet.Tables.Add(DTRefacciones)

        With comando
            If CmbProveedor.SelectedIndex <> -1 Then
                If Me.CmbProveedor.SelectedValue <> "TODOS" Then
                    .Parameters.AddWithValue("@Agente", CmbProveedor.SelectedValue)
                End If


            End If

            If CmbProveedor.SelectedIndex <> -1 Then
                If Me.CmbProveedor.SelectedValue = "TODOS" Then
                    .Parameters.AddWithValue("@Concepto", Me.CmbGasto.SelectedValue)
                End If
            End If
            'If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
            '    .Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
            'End If
            .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
            .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
            ' Asignar el sql para seleccionar los datos de la tabla Maestro   
            .CommandText = SQLTPD
            .Connection = conexion2
        End With


        Dim DtFactProv As New DataTable

        With Adaptador
            .SelectCommand = comando
            ' llenar el dataset   
            .Fill(DtFactProv)
        End With

        DvFactProv = DtFactProv.DefaultView

        With Me.DgFactProv
            .DataSource = DvFactProv

            .AllowUserToAddRows = False

            '.Columns(7).HeaderText = "$Total Factura"
            .Columns("TotFactura").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("TotFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Mnd
            '.Columns(8).HeaderText = "MND"
            .Columns("Moneda").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Importe Aplicado
            '.Columns(9).HeaderText = "Importe Aplicado"
            .Columns("Pagado").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("Pagado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Saldo Pendiente
            '.Columns(10).HeaderText = "$Saldo Pend"
            .Columns("SaldoPendiente").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("SaldoPendiente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Mnd
            '.Columns(11).HeaderText = "MND"
            .Columns("Moneda2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Dias Atraso
            '.Columns(12).HeaderText = "ID prov"
            .Columns("DiasAtraso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Fch Venc.
            '.Columns(13).HeaderText = "Fch venc."

            '$ Saldo MXP
            '.Columns(14).HeaderText = "$ Saldo MXP"
            .Columns("SaldoPesos").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("SaldoPesos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            '.Columns("Referencia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            'Observ
            '.Columns(15).HeaderText = "Observaciones"
            .Columns("Obrserv").Visible = False

        End With

        With conexion2
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With

    End Sub


    'BOTON CONSULTAR
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If RdBCompras.Checked = True Then
            Try
                DgFactProv.Columns("NombreGasto").Visible = False
            Catch ex As Exception

            End Try

            DgFactProv.Columns("Factura").Width = 40
            DgFactProv.Columns("U_IdLinea").Width = 80
            DgFactProv.Columns("IdProved").Width = 50
            Button1.Enabled = False
            If IsNothing(CmbAgteVta.SelectedValue) Then
                MessageBox.Show("Seleccione un proveedor de la lista",
                "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbAgteVta.Focus()
                Return
            End If
            'consulta que se mostrara
            cargar_registros()
            'muestra disa y pj
            VisualizarProv()

            Button1.Enabled = True
        Else
            Try
                DgFactProv.Columns("NombreGasto").Visible = True
            Catch ex As Exception

            End Try

            DgFactProv.Columns("Factura").Width = 50
            DgFactProv.Columns("U_IdLinea").Width = 80
            DgFactProv.Columns("IdProved").Width = 55
            'Button1.Enabled = False
            If CmbGasto.SelectedIndex = -1 Then
                MessageBox.Show("Seleccione un tipo de gasto de la lista",
                "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbGasto.Focus()
                Return
            End If

            If CmbProveedor.SelectedIndex = -1 Then
                MessageBox.Show("Seleccione un proveedor de la lista",
                "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbProveedor.Focus()
                Return
            End If
            cargar_registros2()
            VisualizarProv2()
            'VisualizarProv()

        End If
        'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
        MFacturaBloq()
        Liberadas()
    End Sub

    'LOAD DEL FORMULARIO
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ChkVisDisa.Checked = False

        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

        Dim ConsutaLista As String
        Dim DSetTablas As New DataSet

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
            ConsutaLista = "SELECT CardCode AS IdProv, CardName + ' - ' + Currency + ' - ' + CardCode  as Proveed FROM OCRD T0 WHERE T0.CardType = 'S' AND FROZENFOR <> 'Y' AND (T0.CardCode LIKE '%P-%' OR T0.CardCode LIKE '%PIM-%') ORDER BY T0.CardName "
            ConsutaLista &= "select Code, Name from [@BXP_CONCEPTO] ORDER BY Code "
            ConsutaLista &= "select CardCode, CardName, U_BXP_CONCEPTO from OCRD where CardType = 'S' and CardCode like  'PG%' order by CardName "
            Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            daAgte.Fill(DSetTablas)

            Dim filaAgte As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaAgte = DSetTablas.Tables(0).NewRow

            'Agregamos los valores a los campos de la tabla
            filaAgte("Proveed") = "TODOS"
            filaAgte("IdProv") = "TODOS"

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables(0).Rows.Add(filaAgte)

            Me.CmbAgteVta.DataSource = DSetTablas.Tables(0)
            Me.CmbAgteVta.DisplayMember = "Proveed"
            Me.CmbAgteVta.ValueMember = "IdProv"
            Me.CmbAgteVta.SelectedValue = "TODOS"

            filaAgte = DSetTablas.Tables(1).NewRow
            filaAgte("Code") = "TODOS"
            filaAgte("Name") = "TODOS"
            DSetTablas.Tables(1).Rows.Add(filaAgte)
            Me.CmbGasto.DataSource = DSetTablas.Tables(1)
            Me.CmbGasto.DisplayMember = "Name"
            Me.CmbGasto.ValueMember = "Code"
            Me.CmbGasto.SelectedValue = "TODOS"


            filaAgte = DSetTablas.Tables(2).NewRow
            filaAgte("CardCode") = "TODOS"
            filaAgte("CardName") = "TODOS"
            DSetTablas.Tables(2).Rows.Add(filaAgte)
            DvProveedor.Table = DSetTablas.Tables(2)
            Me.CmbProveedor.DataSource = DvProveedor
            Me.CmbProveedor.DisplayMember = "CardName"
            Me.CmbProveedor.ValueMember = "CardCode"
            Me.CmbProveedor.SelectedValue = "TODOS"
        End Using
    End Sub

    'EXPORTAR A EXCEL
    Private Sub BtnDetalle_Click(sender As System.Object, e As System.EventArgs) Handles BtnDetalle.Click
        'COLOR EN HEXADECIMAL
        amarillo = ColorTranslator.FromHtml("#FDFF6C")

        If (RdBCompras.Checked = True) Then
            BtnDetalle.Enabled = False
            Dim oExcel As Object
            Dim oBook As Object
            Dim oSheet As Object

            'Abrimos un nuevo libro
            oExcel = CreateObject("Excel.Application")
            oBook = oExcel.workbooks.add
            oSheet = oBook.worksheets(1)
            'Declaramos el nombre de las columnas
            oSheet.range("A3").value = "Bloqueado"
            oSheet.range("B3").value = "Liberado"
            oSheet.range("C3").value = "Pagado"
            oSheet.range("D3").value = "Fecha del Doc."
            oSheet.range("E3").value = "Dias Cred"
            oSheet.range("F3").value = "Doc.Sap"

            oSheet.range("G3").value = "Factura"
            oSheet.range("H3").value = "Id. Línea"
            oSheet.range("I3").value = "IdProv"
            oSheet.range("J3").value = "Proveedor"
            oSheet.range("K3").value = "$ Total Factura"
            oSheet.range("L3").value = "Mnd"
            oSheet.range("M3").value = "$ Importe Aplicado"

            oSheet.range("N3").value = "$ Saldo Pendiente"
            oSheet.range("O3").value = "Mnd"
            oSheet.range("P3").value = "Dias Atraso"
            oSheet.range("Q3").value = "Fecha Vencimiento" 'Col 17
            oSheet.range("R3").value = "$ Saldo MXP"
            oSheet.range("S3").value = "Referencia"
            'oSheet.range("S3").value = "Observaciones"
            oSheet.range("T3").value = "Comentarios"
            oSheet.range("U3").value = "Banco Origen"
            oSheet.range("V3").value = "Banco Destino"
            oSheet.range("W3").value = "Cuenta Destino"
            oSheet.range("X3").value = "Descuento"

            'oSheet.range(R3").value = "Comentarios Direccion"
            oSheet.range("Y3").value = "FechPago"

            'para poner la primera fila de los titulos en negrita
            oSheet.range("A3:Y3").font.bold = True
            oSheet.Range("A3:Y3").Cells.Interior.Color = RGB(191, 191, 191)

            Dim fila_dt As Integer = 0
            Dim fila_dt_excel As Integer = 0
            Dim tanto_porcentaje As String = ""
            Dim vTot As String = ""

            Const xlEdgeLeft = 7
            Const xlEdgeRight = 10
            Const xlEdgeTop = 8
            Const xlEdgeBottom = 9
            Const xlInsideHorizontal = 12
            Const xlInsideVertical = 11

            Const xlContinuous = 1
            Const xlThin = 2
            Const xlAutomatic = -4105

            Dim total_reg As Integer = 0

            total_reg = DgFactProv.RowCount
            For fila_dt = 0 To total_reg - 1


                'para leer una celda en concreto
                'el numero es la columna
                'Bloqueado
                Dim cel1 As String = IIf(Me.DgFactProv.Item(0, fila_dt).Value = 0, "No", "Sí")
                'Liberado
                Dim cel2 As String = IIf(Me.DgFactProv.Item(1, fila_dt).Value = 0, "No", "Sí")
                'Pagado
                Dim cel3 As String = IIf(Me.DgFactProv.Item(2, fila_dt).Value = 0, "No", "Sí")
                'Fecha del documento
                Dim cel4 As Date = Me.DgFactProv.Item(3, fila_dt).Value
                'Días de crédito
                Dim cel5 As String = Me.DgFactProv.Item(4, fila_dt).Value
                'Doc. SAP
                Dim cel6 As String = Me.DgFactProv.Item(8, fila_dt).Value
                'Factura
                Dim cel7 As String = IIf(IsDBNull(Me.DgFactProv.Item(9, fila_dt).Value), "", Me.DgFactProv.Item(9, fila_dt).Value)
                'U_IdLinea
                Dim cel8 As String = Me.DgFactProv.Item(10, fila_dt).Value
                ''Me.DgFctProv.Item(4, fila_dt).Value
                'Proveedor
                Dim cel9 As String = Me.DgFactProv.Item(11, fila_dt).Value
                'Proveedor
                Dim cel10 As String = IIf(IsDBNull(Me.DgFactProv.Item(12, fila_dt).Value), 0, Me.DgFactProv.Item(12, fila_dt).Value)
                'Total factura
                Dim cel11 As String = Me.DgFactProv.Item(14, fila_dt).Value
                'Moneda
                Dim cel12 As String = Me.DgFactProv.Item(15, fila_dt).Value
                'Importe aplicado
                Dim cel13 As String = IIf(IsDBNull(Me.DgFactProv.Item(16, fila_dt).Value), 0, Me.DgFactProv.Item(16, fila_dt).Value)
                'Tipo de gasto
                Dim cel14 As String = IIf(IsDBNull(Me.DgFactProv.Item(13, fila_dt).Value), 0, Me.DgFactProv.Item(13, fila_dt).Value)
                'Saldo pendiente
                Dim cel15 As String = Me.DgFactProv.Item(17, fila_dt).Value
                'Moneda
                Dim cel16 As String = Me.DgFactProv.Item(15, fila_dt).Value
                'Días atrasados
                Dim cel17 As String = Me.DgFactProv.Item(19, fila_dt).Value
                'Fecha de vencimiento
                Dim cel18 As Date = IIf(IsDBNull(Me.DgFactProv.Item(20, fila_dt).Value), "", Me.DgFactProv.Item(20, fila_dt).Value)
                'Saldo MXP
                Dim cel19 As String = IIf(IsDBNull(Me.DgFactProv.Item(22, fila_dt).Value), "", Me.DgFactProv.Item(22, fila_dt).Value)
                'Referencia
                Dim cel20 As String = IIf(IsDBNull(Me.DgFactProv.Item(23, fila_dt).Value), "", Me.DgFactProv.Item(23, fila_dt).Value)
                'Observaciones
                Dim cel21 As String = IIf(IsDBNull(Me.DgFactProv.Item(24, fila_dt).Value), "", Me.DgFactProv.Item(24, fila_dt).Value)
                'Banco Origen
                Dim cel22 As String = IIf(IsDBNull(Me.DgFactProv.Item(5, fila_dt).Value), "", Me.DgFactProv.Item(5, fila_dt).Value)
                'Banco Destino
                Dim cel23 As String = IIf(IsDBNull(Me.DgFactProv.Item(6, fila_dt).Value), "", Me.DgFactProv.Item(6, fila_dt).Value)
                'Cuenta destino
                Dim cel24 As String = IIf(IsDBNull(Me.DgFactProv.Item(7, fila_dt).Value), "", Me.DgFactProv.Item(7, fila_dt).Value)
                'Comentarios
                Dim cel25 As String = IIf(IsDBNull(Me.DgFactProv.Item(25, fila_dt).Value), "", Me.DgFactProv.Item(25, fila_dt).Value)
                'Fecha pagada
                Dim cel26 As String = IIf(IsDBNull(Me.DgFactProv.Item(26, fila_dt).Value), "", Me.DgFactProv.Item(26, fila_dt).Value)
                'Descuento
                Dim cel27 As String = IIf(IsDBNull(Me.DgFactProv.Item(21, fila_dt).Value), "", Me.DgFactProv.Item(21, fila_dt).Value)

                fila_dt_excel = fila_dt + 4

                vTot = "A" + (fila_dt_excel).ToString
                vTot &= ":Y" + (fila_dt_excel).ToString

                If Me.DgFactProv.Item(1, fila_dt).Value = 1 Then
                    oSheet.Range(vTot).Cells.Interior.Color = RGB(255, 0, 0)
                    oSheet.Range(vTot).Cells.Font.Color = RGB(255, 255, 255)
                    '.Exterior.Color = RGB(255, 255, 255)
                ElseIf Me.DgFactProv.Item(0, fila_dt).Value = 1 Then
                    'COLOCA COLOR AMARILLO
                    oSheet.Range(vTot).Cells.Interior.Color = amarillo
                    'COLOCA EL COLOR DE LETRA DEPENDIENDO AL BLOQUEADO
                    oSheet.Range(vTot).Cells.Font.Color = Color.Black
                End If

                'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
                oSheet.range("A" & fila_dt_excel).value = cel1
                oSheet.range("B" & fila_dt_excel).value = cel2
                oSheet.range("C" & fila_dt_excel).value = cel3
                oSheet.range("D" & fila_dt_excel).value = cel4
                oSheet.range("E" & fila_dt_excel).value = cel5
                'Doc Num
                oSheet.range("F" & fila_dt_excel).value = cel6
                'Factura
                oSheet.range("G" & fila_dt_excel).value = cel7
                'ID Linea
                oSheet.range("H" & fila_dt_excel).value = cel8
                'Id Poveedor
                oSheet.range("I" & fila_dt_excel).value = cel9
                oSheet.range("J" & fila_dt_excel).value = cel10
                oSheet.range("K" & fila_dt_excel).value = cel11
                oSheet.range("L" & fila_dt_excel).value = cel12
                oSheet.range("M" & fila_dt_excel).value = cel13
                'Saldo pendiente
                oSheet.range("N" & fila_dt_excel).value = cel15
                oSheet.range("O" & fila_dt_excel).value = cel16
                oSheet.range("P" & fila_dt_excel).value = cel17
                oSheet.range("Q" & fila_dt_excel).value = cel18
                oSheet.range("R" & fila_dt_excel).value = cel19
                oSheet.range("S" & fila_dt_excel).value = cel20
                oSheet.range("T" & fila_dt_excel).value = cel25
                oSheet.range("U" & fila_dt_excel).value = cel22
                oSheet.range("V" & fila_dt_excel).value = cel23
                oSheet.range("W" & fila_dt_excel).value = cel24
                oSheet.range("X" & fila_dt_excel).value = cel27
                oSheet.range("Y" & fila_dt_excel).value = cel26
            Next

            vTot = "A3"
            vTot &= "X" + (fila_dt_excel).ToString

            'Seleccionamos la hoja
            oBook.worksheets(1).Select()
            'Seleccionamos elrango de celdas al cual le vamos a poner bordes
            'oBook.worksheets(1).Range(vTot).Select()

            'Aqui mostramos los bordes Izquierdos
            With oExcel.Selection.Borders(xlEdgeLeft)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With
            'Aqui mostramos los bordes de Arriba
            With oExcel.Selection.Borders(xlEdgeTop)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With
            'Aqui mostramos los bordes de abajo
            With oExcel.Selection.Borders(xlEdgeBottom)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With
            'Aqui mostramos los bordes derechos
            With oExcel.Selection.Borders(xlEdgeRight)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With
            'Aqui mostramos los bordes interiores verticales
            With oExcel.Selection.Borders(xlInsideVertical)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With
            'Aqui mostramos los bordes interiores horizontales
            With oExcel.Selection.Borders(xlInsideHorizontal)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With


            vTot = "R" + (fila_dt_excel + 1).ToString
            oSheet.range(vTot).value = TxtTotEnPesos.Text

            oSheet.range(vTot).font.bold = True

            oSheet.columns("A:X").entirecolumn.autofit()

            vTot = "P" + (fila_dt_excel + 1).ToString
            'MsgBox(vTot)
            oSheet.range(vTot).value = LblTotal.Text

            oSheet.range(vTot).font.bold = True

            oSheet.range("A1").value = "Reporte de Pagos a Proveedores Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
            oSheet.Range("A2").select()

            oExcel.visible = True
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
            GC.Collect()
            oSheet = Nothing
            oBook = Nothing
            oExcel = Nothing
            BtnDetalle.Enabled = True
        Else
            ''MsgBox("Entre al else")
            BtnDetalle.Enabled = False
            Dim oExcel As Object
            Dim oBook As Object
            Dim oSheet As Object

            'Abrimos un nuevo libro
            oExcel = CreateObject("Excel.Application")
            oBook = oExcel.workbooks.add
            oSheet = oBook.worksheets(1)

            ''Declaramos el nombre de las columnas
            'oSheet.range("A3").value = "Pagado"
            'oSheet.range("B3").value = "Fecha del Doc."
            'oSheet.range("C3").value = "Dias Cred"
            'oSheet.range("D3").value = "Factura"
            'oSheet.range("E3").value = "IdProv"
            'oSheet.range("F3").value = "Proveedor"
            'oSheet.range("G3").value = "$ Total Factura"
            'oSheet.range("H3").value = "Mnd"
            'oSheet.range("I3").value = "$ Importe Aplicado"

            'oSheet.range("J3").value = "$ Saldo Pendiente"
            'oSheet.range("K3").value = "Mnd"
            'oSheet.range("L3").value = "Dias Atraso"
            'oSheet.range("M3").value = "Fecha Vencimiento"
            'oSheet.range("N3").value = "$ Saldo MXP"
            'oSheet.range("O3").value = "Observaciones"
            'oSheet.range("P3").value = "Comentarios"


            'Declaramos el nombre de las columnas
            oSheet.range("A3").value = "Bloqueado"
            oSheet.range("B3").value = "Liberado"
            oSheet.range("C3").value = "Pagado"
            oSheet.range("D3").value = "Fecha del Doc."
            oSheet.range("E3").value = "Dias Cred"
            oSheet.range("F3").value = "Doc.Sap"

            oSheet.range("G3").value = "Factura"
            oSheet.range("H3").value = "Id. Línea"
            oSheet.range("I3").value = "IdProv"
            oSheet.range("J3").value = "Proveedor"
            oSheet.range("K3").value = "Tipo de Gasto"
            oSheet.range("L3").value = "$ Total Factura"
            oSheet.range("M3").value = "Mnd"
            oSheet.range("N3").value = "$ Importe Aplicado"

            oSheet.range("O3").value = "$ Saldo Pendiente"
            oSheet.range("P3").value = "Mnd"
            oSheet.range("Q3").value = "Dias Atraso"
            oSheet.range("R3").value = "Fecha Vencimiento"
            oSheet.range("S3").value = "$ Saldo MXP"
            oSheet.range("T3").value = "Referencia"
            oSheet.range("U3").value = "Comentarios"
            'oSheet.range("V3").value = "Banco Origen"
            'oSheet.range("W3").value = "Banco Destino"
            'oSheet.range("X3").value = "Cuenta Destino"
            'oSheet.range("X3").value = "Descuento"
            'oSheet.range("S3").value = "Comentarios Direccion"
            oSheet.range("V3").value = "FechPago"

            'para poner la primera fila de los titulos en negrita
            oSheet.range("A3:V3").font.bold = True
            oSheet.Range("A3:V3").Cells.Interior.Color = RGB(191, 191, 191)

            Dim fila_dt As Integer = 0
            Dim fila_dt_excel As Integer = 0
            Dim tanto_porcentaje As String = ""
            Dim vTot As String = ""

            Const xlEdgeLeft = 7
            Const xlEdgeRight = 10
            Const xlEdgeTop = 8
            Const xlEdgeBottom = 9
            Const xlInsideHorizontal = 12
            Const xlInsideVertical = 11

            Const xlContinuous = 1
            Const xlThin = 2
            Const xlAutomatic = -4105

            Dim total_reg As Integer = 0

            total_reg = DgFactProv.RowCount
            For fila_dt = 0 To total_reg - 1

                ''para leer una celda en concreto
                ''el numero es la columna
                'Dim cel1 As String = IIf(Me.DgFactProv.Item(0, fila_dt).Value = 0, "No", "Sí")
                'Dim cel2 As String = Me.DgFactProv.Item(1, fila_dt).Value
                'Dim cel3 As String = Me.DgFactProv.Item(2, fila_dt).Value
                'Dim cel4 As String = Me.DgFactProv.Item(3, fila_dt).Value
                'Dim cel5 As String = Me.DgFactProv.Item(4, fila_dt).Value
                'Dim cel6 As String = Me.DgFactProv.Item(5, fila_dt).Value
                'Dim cel7 As String = IIf(IsDBNull(Me.DgFactProv.Item(6, fila_dt).Value), 0, Me.DgFactProv.Item(6, fila_dt).Value)
                'Dim cel8 As String = Me.DgFactProv.Item(7, fila_dt).Value

                'Dim cel9 As String = IIf(IsDBNull(Me.DgFactProv.Item(8, fila_dt).Value), 0, Me.DgFactProv.Item(8, fila_dt).Value)
                'Dim cel10 As String = IIf(IsDBNull(Me.DgFactProv.Item(9, fila_dt).Value), 0, Me.DgFactProv.Item(9, fila_dt).Value)
                'Dim cel11 As String = Me.DgFactProv.Item(10, fila_dt).Value
                'Dim cel12 As String = Me.DgFactProv.Item(11, fila_dt).Value
                'Dim cel13 As String = Me.DgFactProv.Item(12, fila_dt).Value
                'Dim cel14 As String = IIf(IsDBNull(Me.DgFactProv.Item(13, fila_dt).Value), 0, Me.DgFactProv.Item(13, fila_dt).Value)
                'Dim cel15 As String = IIf(IsDBNull(Me.DgFactProv.Item(14, fila_dt).Value), "", Me.DgFactProv.Item(14, fila_dt).Value)
                'Dim cel16 As String = IIf(IsDBNull(Me.DgFactProv.Item(15, fila_dt).Value), "", Me.DgFactProv.Item(15, fila_dt).Value)

                'para leer una celda en concreto
                'el numero es la columna
                'Liberado
                Dim cel1 As String = IIf(Me.DgFactProv.Item(1, fila_dt).Value = 0, "No", "Sí")
                'Bloqueado
                Dim cel2 As String = IIf(Me.DgFactProv.Item(0, fila_dt).Value = 0, "No", "Sí")
                'Pagado
                Dim cel3 As String = IIf(Me.DgFactProv.Item(2, fila_dt).Value = 0, "No", "Sí")
                'Fecha del documento
                Dim cel4 As Date = Me.DgFactProv.Item(3, fila_dt).Value
                'Días de crédito
                Dim cel5 As String = Me.DgFactProv.Item(4, fila_dt).Value
                'Doc.Sap
                Dim cel6 As String = Me.DgFactProv.Item(8, fila_dt).Value
                'Factura
                Dim cel7 As String = IIf(IsDBNull(Me.DgFactProv.Item(9, fila_dt).Value), "", Me.DgFactProv.Item(8, fila_dt).Value)
                'U_IdLinea
                Dim cel8 As String = Me.DgFactProv.Item(10, fila_dt).Value
                'IdProv
                Dim cel9 As String = IIf(IsDBNull(Me.DgFactProv.Item(11, fila_dt).Value), "", Me.DgFactProv.Item(11, fila_dt).Value)
                Dim cel10 As String = Me.DgFactProv.Item(12, fila_dt).Value
                'Tipo Gasto
                Dim cel10_1 As String = IIf(IsDBNull(Me.DgFactProv.Item(13, fila_dt).Value), "", Me.DgFactProv.Item(13, fila_dt).Value)
                'Total Factura
                Dim cel11 As String = IIf(IsDBNull(Me.DgFactProv.Item(14, fila_dt).Value), 0, Me.DgFactProv.Item(14, fila_dt).Value)
                'Moneda
                Dim cel12 As String = Me.DgFactProv.Item(15, fila_dt).Value
                'Pagado
                Dim cel13 As String = IIf(IsDBNull(Me.DgFactProv.Item(16, fila_dt).Value), 0, Me.DgFactProv.Item(16, fila_dt).Value)
                'Saldo pendiente
                Dim cel14 As String = IIf(IsDBNull(Me.DgFactProv.Item(17, fila_dt).Value), 0, Me.DgFactProv.Item(17, fila_dt).Value)
                'Dias atraso
                Dim cel15 As String = Me.DgFactProv.Item(19, fila_dt).Value
                'Fecha vencimiento
                Dim cel16 As Date = Me.DgFactProv.Item(20, fila_dt).Value
                'Saldo en pesos
                Dim cel17 As String = IIf(IsDBNull(Me.DgFactProv.Item(21, fila_dt).Value), 0, Me.DgFactProv.Item(21, fila_dt).Value)
                'Referencia
                Dim cel18 As String = IIf(IsDBNull(Me.DgFactProv.Item(22, fila_dt).Value), "", Me.DgFactProv.Item(22, fila_dt).Value)
                'Observaciones
                Dim cel19 As String = IIf(IsDBNull(Me.DgFactProv.Item(23, fila_dt).Value), "", Me.DgFactProv.Item(23, fila_dt).Value)
                'Comentario
                Dim cel20 As String = IIf(IsDBNull(Me.DgFactProv.Item(24, fila_dt).Value), "", Me.DgFactProv.Item(24, fila_dt).Value)
                'Fecha pagada
                Dim cel21 As String = IIf(IsDBNull(Me.DgFactProv.Item(25, fila_dt).Value), "", Me.DgFactProv.Item(25, fila_dt).Value)

                'Dim cel22 As String = IIf(IsDBNull(Me.DgFactProv.Item(21, fila_dt).Value), "", Me.DgFactProv.Item(21, fila_dt).Value)

                'Dim cel23 As String = IIf(IsDBNull(Me.DgFactProv.Item(21, fila_dt).Value), "", Me.DgFactProv.Item(21, fila_dt).Value)

                fila_dt_excel = fila_dt + 4

                vTot = "A" + (fila_dt_excel).ToString
                vTot &= ":V" + (fila_dt_excel).ToString
                'AQUI
                If Me.DgFactProv.Item(1, fila_dt).Value = 1 Then
                    oSheet.Range(vTot).Cells.Interior.Color = RGB(255, 0, 0)
                    oSheet.Range(vTot).Cells.Font.Color = RGB(255, 255, 255)
                    '.Exterior.Color = RGB(255, 255, 255)
                ElseIf Me.DgFactProv.Item(0, fila_dt).Value = 1 Then
                    'COLOCA COLOR AMARILLO
                    oSheet.Range(vTot).Cells.Interior.Color = amarillo
                    'COLOCA EL COLOR DE LETRA DEPENDIENDO AL BLOQUEADO
                    oSheet.Range(vTot).Cells.Font.Color = Color.Black
                End If

                'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
                oSheet.range("A" & fila_dt_excel).value = cel1
                oSheet.range("B" & fila_dt_excel).value = cel2
                oSheet.range("C" & fila_dt_excel).value = cel3
                oSheet.range("D" & fila_dt_excel).value = cel4

                oSheet.range("E" & fila_dt_excel).value = cel5

                oSheet.range("F" & fila_dt_excel).value = cel6
                oSheet.range("G" & fila_dt_excel).value = cel7
                oSheet.range("H" & fila_dt_excel).value = cel8
                oSheet.range("I" & fila_dt_excel).value = cel9
                oSheet.range("J" & fila_dt_excel).value = cel10
                oSheet.range("K" & fila_dt_excel).value = cel10_1
                oSheet.range("L" & fila_dt_excel).value = FormatNumber(cel11, 2)
                oSheet.range("M" & fila_dt_excel).value = cel12
                oSheet.range("N" & fila_dt_excel).value = FormatNumber(cel13, 2)
                oSheet.range("O" & fila_dt_excel).value = FormatNumber(cel14, 2)
                oSheet.range("P" & fila_dt_excel).value = cel12
                oSheet.range("Q" & fila_dt_excel).value = cel15
                oSheet.range("R" & fila_dt_excel).value = cel16
                oSheet.range("S" & fila_dt_excel).value = FormatNumber(cel17, 2)
                oSheet.range("T" & fila_dt_excel).value = cel18
                oSheet.range("U" & fila_dt_excel).value = cel19
                oSheet.range("V" & fila_dt_excel).value = cel21
            Next

            vTot = "A3"
            vTot &= ":V" + (fila_dt_excel).ToString

            'Seleccionamos la hoja
            oBook.worksheets(1).Select()
            'Seleccionamos elrango de celdas al cual le vamos a poner bordes
            oBook.worksheets(1).Range(vTot).Select()

            'Aqui mostramos los bordes Izquierdos
            With oExcel.Selection.Borders(xlEdgeLeft)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With
            'Aqui mostramos los bordes de Arriba
            With oExcel.Selection.Borders(xlEdgeTop)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With
            'Aqui mostramos los bordes de abajo
            With oExcel.Selection.Borders(xlEdgeBottom)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With
            'Aqui mostramos los bordes derechos
            With oExcel.Selection.Borders(xlEdgeRight)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With
            'Aqui mostramos los bordes interiores verticales
            With oExcel.Selection.Borders(xlInsideVertical)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With
            'Aqui mostramos los bordes interiores horizontales
            With oExcel.Selection.Borders(xlInsideHorizontal)
                .LineStyle = xlContinuous
                .Weight = xlThin
                .ColorIndex = xlAutomatic
            End With

            vTot = "S" + (fila_dt_excel + 1).ToString
            oSheet.range(vTot).value = TxtTotEnPesos.Text

            oSheet.range(vTot).font.bold = True

            oSheet.columns("A:V").entirecolumn.autofit()

            vTot = "Q" + (fila_dt_excel + 1).ToString
            'MsgBox(vTot)
            oSheet.range(vTot).value = LblTotal.Text

            oSheet.range(vTot).font.bold = True

            oSheet.range("A1").value = "Reporte de Pagos a Proveedores Del Periodo " + Format(Me.DtpFechaIni.Value, " dd/MM/yyyy") + " Al " + Format(Me.DtpFechaTer.Value, " dd/MM/yyyy")
            oSheet.Range("A2").select()

            oExcel.visible = True
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
            GC.Collect()
            oSheet = Nothing
            oBook = Nothing
            oExcel = Nothing
            BtnDetalle.Enabled = True

        End If

    End Sub

    Private Sub DgFactProv_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgFactProv.CellDoubleClick

        'Se colocan los comentarios para la factura
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            'MessageBox.Show(Me.DgFactProv.Columns(e.ColumnIndex).Name)
            'MODIFICO IVAN GONZALEZ
            'DEPENDIENDO EL PERFIL Y EL NOMBRE DE LA COLUMNA
            Dim bandera As Boolean
            If Me.DgFactProv.Columns(e.ColumnIndex).Name = "Comentarios" And UsrTPM = "COMPRAS1" Then
                bandera = True
                'ElseIf Me.DgFactProv.Columns(e.ColumnIndex).Name = "Comentarios Direccion" And UsrTPM = "MANAGER" Then
            ElseIf UsrTPM = "MANAGER" Then
                bandera = True
            End If

            If bandera Then
                Dim f2 As New PagoCapCom()
                f2.TxtFactura1.Text = DgFactProv.Rows(e.RowIndex).Cells("Factura").Value
                f2.TxtIdProv.Text = DgFactProv.Rows(e.RowIndex).Cells("IdProved").Value
                f2.TxtNomProv.Text = DgFactProv.Rows(e.RowIndex).Cells("Proveedor").Value

                f2.TxtFchDoc.Text = DgFactProv.Rows(e.RowIndex).Cells("FchDoc").Value
                f2.TxtFchVenc.Text = DgFactProv.Rows(e.RowIndex).Cells("FchVen").Value
                f2.TxtSaldo.Text = Format(DgFactProv.Rows(e.RowIndex).Cells("SaldoPesos").Value, "$ ##,###,###,###.00")
                f2.TxtDiasAtraso.Text = DgFactProv.Rows(e.RowIndex).Cells("DiasAtraso").Value
                If IsDBNull(DgFactProv.Rows(e.RowIndex).Cells("Comentarios").Value) Then
                    f2.TxtComentario.Text = ""
                Else
                    f2.TxtComentario.Text = DgFactProv.Rows(e.RowIndex).Cells("Comentarios").Value
                End If
                f2.LblRow.Text = e.RowIndex.ToString
                f2.ShowDialog()
            End If

        End If
    End Sub





    'Rojo --> Facturas pagadas
    'Amarillo -->Facturas Bloqueadas
    'Verde --> Facturas Liberadas
    Private Sub DgFactProv_RowPrePaint(sender As System.Object, e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DgFactProv.RowPrePaint
        'COLOR EN HEXADECIMAL
        amarillo = ColorTranslator.FromHtml("#FDFF6C")

        If Not IsDBNull(DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value) Then


            'Colorea facturas pagadas
            If DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 1 And DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value <> 1 Then

                DgFactProv.Rows(e.RowIndex).Cells("RegSel").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("RegSel").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Liberado").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Liberado").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Frozen").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Frozen").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("FchDoc").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("FchDoc").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("DiasCred").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("DiasCred").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Factura").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Factura").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("FactProv").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("FactProv").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("U_IdLinea").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("U_IdLinea").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("IdProved").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("IdProved").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Proveedor").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Proveedor").Style.ForeColor = Color.White

                Try
                    DgFactProv.Rows(e.RowIndex).Cells("NombreGasto").Style.BackColor = Color.Red
                    DgFactProv.Rows(e.RowIndex).Cells("NombreGasto").Style.ForeColor = Color.White
                Catch ex As Exception

                End Try

                DgFactProv.Rows(e.RowIndex).Cells("TotFactura").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("TotFactura").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Moneda").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Moneda").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Pagado").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Pagado").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("SaldoPendiente").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("SaldoPendiente").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Moneda2").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Moneda2").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("DiasAtraso").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("FchVen").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("FchVen").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("SaldoPesos").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("SaldoPesos").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Referencia").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Referencia").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Obrserv").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Obrserv").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("BancoOrigen").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("BancoOrigen").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("BancoDestino").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("BancoDestino").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("CuentaDestino").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("CuentaDestino").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Comentarios").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Comentarios").Style.ForeColor = Color.White

                'DgFactProv.Rows(e.RowIndex).Cells("Descuento").Style.BackColor = Color.Red
                'DgFactProv.Rows(e.RowIndex).Cells("Descuento").Style.ForeColor = Color.White

                'DgFactProv.Rows(e.RowIndex).Cells("Comentarios Direccion").Style.BackColor = Color.Red
                'DgFactProv.Rows(e.RowIndex).Cells("Comentarios Direccion").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Style.ForeColor = Color.White

                'Sin pagar,sin liberar y sin bloquear
            ElseIf (DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value <> 1 And DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value <> 1) Then
                DgFactProv.Rows(e.RowIndex).Cells("Frozen").Style.BackColor = Color.White
                DgFactProv.Rows(e.RowIndex).Cells("Frozen").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("RegSel").Style.BackColor = Color.White
                DgFactProv.Rows(e.RowIndex).Cells("RegSel").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Liberado").Style.BackColor = Color.White
                DgFactProv.Rows(e.RowIndex).Cells("Liberado").Style.ForeColor = Color.Black


                DgFactProv.Rows(e.RowIndex).Cells("FchDoc").Style.BackColor = Color.White
                DgFactProv.Rows(e.RowIndex).Cells("FchDoc").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("DiasCred").Style.BackColor = Color.White
                DgFactProv.Rows(e.RowIndex).Cells("DiasCred").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Factura").Style.BackColor = Color.White
                DgFactProv.Rows(e.RowIndex).Cells("Factura").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("FactProv").Style.BackColor = Color.White
                DgFactProv.Rows(e.RowIndex).Cells("FactProv").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("U_IdLinea").Style.BackColor = Color.White
                DgFactProv.Rows(e.RowIndex).Cells("U_IdLinea").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("IdProved").Style.BackColor = Color.White
                DgFactProv.Rows(e.RowIndex).Cells("IdProved").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Proveedor").Style.BackColor = Color.White
                DgFactProv.Rows(e.RowIndex).Cells("Proveedor").Style.ForeColor = Color.Black

                Try
                    DgFactProv.Rows(e.RowIndex).Cells("NombreGasto").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("NombreGasto").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("TotFactura").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("TotFactura").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("Moneda").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("Moneda").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("Pagado").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("Pagado").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("SaldoPendiente").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("SaldoPendiente").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("Moneda2").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("Moneda2").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("DiasAtraso").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("FchVen").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("FchVen").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("SaldoPesos").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("SaldoPesos").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("Referencia").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("Referencia").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("Obrserv").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("Obrserv").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("BancoOrigen").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("BancoOrigen").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("BancoDestino").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("BancoDestino").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("CuentaDestino").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("CuentaDestino").Style.ForeColor = Color.Black

                    'DgFactProv.Rows(e.RowIndex).Cells("Descuento").Style.BackColor = Color.White
                    'DgFactProv.Rows(e.RowIndex).Cells("Descuento").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("Comentarios").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("Comentarios").Style.ForeColor = Color.Black

                    'DgFactProv.Rows(e.RowIndex).Cells("Comentarios Direccion").Style.BackColor = Color.White
                    'DgFactProv.Rows(e.RowIndex).Cells("Comentarios Direccion").Style.ForeColor = Color.Black

                    DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Style.BackColor = Color.White
                    DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Style.ForeColor = Color.Black
                Catch ex As Exception
                    MsgBox("Error " & ex.Message)
                End Try

            ElseIf DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value <> 1 And DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value = 1 Then
                'COLOCA COLOR A TODA LA FILA
                DgFactProv.Rows(e.RowIndex).Cells("RegSel").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("Liberado").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("Frozen").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("FchDoc").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("DiasCred").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("Factura").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("FactProv").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("U_IdLinea").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("IdProved").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("Proveedor").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("NombreGasto").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("TotFactura").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("Moneda").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("Pagado").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("SaldoPendiente").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("Moneda2").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("FchVen").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("SaldoPesos").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("Referencia").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("Obrserv").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("BancoOrigen").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("BancoDestino").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("CuentaDestino").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("Descuento").Style.BackColor = amarillo

                DgFactProv.Rows(e.RowIndex).Cells("Comentarios").Style.BackColor = amarillo
                'DgFactProv.Rows(e.RowIndex).Cells("Comentarios Direccion").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Style.BackColor = amarillo

            ElseIf DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value = 1 And DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 0 And DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value = 0 Then
                DgFactProv.Rows(e.RowIndex).Cells("RegSel").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("Liberado").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("Frozen").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("FchDoc").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("DiasCred").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("Factura").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("FactProv").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("U_IdLinea").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("IdProved").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("Proveedor").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("NombreGasto").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("TotFactura").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("Moneda").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("Pagado").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("SaldoPendiente").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("Moneda2").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("FchVen").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("SaldoPesos").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("Referencia").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("Obrserv").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("BancoOrigen").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("BancoDestino").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("CuentaDestino").Style.BackColor = Color.GreenYellow
                'DgFactProv.Rows(e.RowIndex).Cells("Descuento").Style.BackColor = Color.GreenYellow
                DgFactProv.Rows(e.RowIndex).Cells("Comentarios").Style.BackColor = Color.GreenYellow
                'DgFactProv.Rows(e.RowIndex).Cells("Comentarios Direccion").Style.BackColor = amarillo
                DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Style.BackColor = Color.GreenYellow
            End If


        End If
    End Sub

    Sub VisualizarProv()
        Dim VFiltro As String = " "
        If ChkVisDisa.Checked = False Then
            VFiltro = "IdProved <> 'P-064' AND IdProved <> 'P-001' AND IdProved <> 'P-002' AND IdProved <> 'P-104' AND IdProved <> 'P-105'"
            'VFiltro = "IdProved <> 'P-064' AND IdProved <> 'P-001' AND IdProved <> 'P-002'"
        End If

        If ChkVerPagadas.Checked = False Then
            If ChkVisDisa.Checked = False Then
                VFiltro &= " AND RegSel = 0"
            Else
                VFiltro = "RegSel = 0"
            End If
        End If

        If ChkUSD.Checked = True Then
            If ChkVisDisa.Checked = False Or ChkVerPagadas.Checked = False Then
                VFiltro &= " AND Moneda = 'USD' "
            Else
                VFiltro = "Moneda = 'USD'"
            End If
        End If

        If VFiltro = " " Then
            DvFactProv.RowFilter = String.Empty
        Else
            DvFactProv.RowFilter = VFiltro
        End If

        TotalFacturas()
    End Sub

    Sub VisualizarProv2()
        Dim VFiltro As String = " "

        If ChkVerPagadas.Checked = False Then
            VFiltro = "RegSel = 0"
        End If

        'If CmbProveedor.SelectedValue.ToString <> "TODOS" Then
        '    If (CmbProveedor.SelectedIndex <> -1) Then
        '        MsgBox("voy a buscar por proveedor")
        '        VFiltro = "IdProved = '" & CmbProveedor.SelectedValue.ToString & "'"
        '    End If
        'End If

        If ChkUSD.Checked = True Then
            If ChkVerPagadas.Checked = False Then
                VFiltro &= " AND Moneda = 'USD' "
            Else
                VFiltro = "Moneda = 'USD'"
            End If
        End If




        'If ChkVerPagadas.Checked = False Then
        '    If ChkVisDisa.Checked = False Then
        '        VFiltro &= " AND RegSel = 0"
        '    Else
        '        VFiltro = "RegSel = 0"
        '    End If
        'End If

        If VFiltro = " " Then
            DvFactProv.RowFilter = String.Empty
        Else
            DvFactProv.RowFilter = VFiltro
        End If

        TotalFacturas()
    End Sub

    Private Sub ChkVisualizar_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkVisDisa.CheckedChanged
        VisualizarProv()
        'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
        MFacturaBloq()
        Liberadas()
    End Sub




    Private Sub DgFactProv_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgFactProv.CellContentClick

        Dim VErrorG As Integer = 0
        Dim strValue As String
        Dim strBloq As String



        If e.RowIndex >= 0 Then

            If UsrTPM <> "COMPRAS1" Then

                If Me.DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value = 0 Then




                    If Me.DgFactProv.Columns(e.ColumnIndex).Name = "Liberado" And UsrTPM = "MANAGER" Then

                        Dim auxliberado As Integer = Me.DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value

                        Dim con2 As New SqlConnection
                        Dim cmd2 As New SqlCommand
                        Dim CadenaSQL2 As String = ""

                        If auxliberado = 0 Then



                            ' If MessageBox.Show("¿Confirma que desea liberar está factura para ser pagada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            'SEPARADOR
                            If MessageBox.Show("¿Realmente desea liberar la factura para pago?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value = 1
                                Me.DgFactProv.RefreshEdit()


                                'VALIDA SI EXISTE EL REGISTRO
                                Try
                                    'ALAMACENA LA CONSULTA
                                    CadenaSQL2 = "SELECT * FROM FactLiberadas WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "
                                    'CONECTA A LA BASE DE DATOS
                                    conexion_universal.conectar()
                                    'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
                                    conexion_universal.slq_s = New SqlCommand(CadenaSQL2, conexion_universal.conexion_uni)
                                    'EJECUTA LA CONSULTA
                                    conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
                                    'RECORRE LA CONSULTA
                                    If conexion_universal.rd_s.Read Then
                                        CadenaSQL2 = ""
                                        'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
                                        CadenaSQL2 = "UPDATE FactLiberadas SET Liberado = '" + DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value.ToString + "'  "
                                        CadenaSQL2 &= "WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "
                                        con2.ConnectionString = StrTpm
                                        con2.Open()
                                        cmd2.Connection = con2
                                        cmd2.CommandText = CadenaSQL2
                                        cmd2.ExecuteNonQuery()
                                        Me.DgFactProv.RefreshEdit()
                                    Else
                                        CadenaSQL2 = ""
                                        'ALMACENA LA CONSULTA
                                        'CadenaSQL2 = "INSERT INTO COMP1 (FactCompras,Fecha,Id_Usuario, Frozen) VALUES ("
                                        'CadenaSQL2 &= DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString
                                        'CadenaSQL2 &= ","
                                        'CadenaSQL2 &= "@fecha"
                                        'CadenaSQL2 &= ",'"
                                        'CadenaSQL2 &= UsrTPM
                                        'CadenaSQL2 &= "', '" + DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value.ToString + "') "

                                        CadenaSQL2 = "INSERT INTO FactLiberadas (FactCompras,Fecha,id_Usuario,Liberado) VALUES ("
                                        CadenaSQL2 &= DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString
                                        CadenaSQL2 &= ","
                                        CadenaSQL2 &= "@fecha"
                                        CadenaSQL2 &= ",'"
                                        CadenaSQL2 &= UsrTPM
                                        CadenaSQL2 &= "', '" + DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value.ToString + "') "






                                        con2.ConnectionString = StrTpm
                                        con2.Open()
                                        cmd2.Connection = con2
                                        cmd2.CommandText = CadenaSQL2
                                        cmd2.Parameters.Add(New SqlParameter("@fecha", DateTime.Now))
                                        cmd2.ExecuteNonQuery()

                                        Me.DgFactProv.RefreshEdit()
                                    End If
                                Catch ex As Exception
                                    MessageBox.Show("Error al actualizar o insertar el Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Finally
                                    'CIERRA LAS CONEXIONES DE USO
                                    con2.Close()
                                    conexion_universal.cerrar_conectar()
                                End Try
                            Else
                                DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value = 0
                                Me.DgFactProv.RefreshEdit()
                            End If

                            'Aqui


                            '                        DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value = 1

                            '                        Dim con As New SqlConnection
                            'Dim cmd As New SqlCommand
                            'Dim CadenaSQL As String = ""


                            'UsrTPM = "MANAGER"
                            '                        CadenaSQL = "INSERT INTO FactLiberadas (FactCompras,Fecha,id_Usuario,Liberado) VALUES ("
                            'CadenaSQL &= DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString
                            'CadenaSQL &= ","
                            'CadenaSQL &= "@fecha"
                            'CadenaSQL &= ",'"
                            'CadenaSQL &= UsrTPM
                            'CadenaSQL &= "',"
                            'CadenaSQL &= 1
                            'CadenaSQL &= ")"

                            '                        Try
                            '                            con.ConnectionString = StrTpm
                            '                            con.Open()
                            '                            cmd.Connection = con
                            '                            cmd.CommandText = CadenaSQL
                            '                            cmd.Parameters.Add(New SqlParameter("@fecha", DateTime.Now))
                            '                            cmd.ExecuteNonQuery()

                            '                            Me.DgFactProv.RefreshEdit()

                            '                        Catch ex As Exception
                            '                            MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                            '                        Finally


                            '                        End Try
                            '                        con.Close()
                            '                    End If

                            'DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value = 0
                            '                      Else
                            ' Dim con As New SqlConnection
                            ' Dim cmd As New SqlCommand
                            ' If MessageBox.Show("¿Confirma que no desea liberar está factura para ser pagada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                            '  DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value = 0


                            '  CadenaSQL = "UPDATE FactLiberadas SET Liberado = 0  "
                            '  CadenaSQL &= "WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "

                            '                          Try

                            '                              con.ConnectionString = StrTpm
                            '                              con.Open()
                            '                              cmd.Connection = con
                            '                              cmd.CommandText = CadenaSQL
                            '                              cmd.Parameters.Add(New SqlParameter("@fecha", DateTime.Now))
                            '                              cmd.ExecuteNonQuery()

                            '                              Me.DgFactProv.RefreshEdit()

                            '                          Catch ex As Exception
                            '                              MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                            '                          Finally


                            '                          End Try
                            '                          con.Close()
                            'End If
                        Else
                            CadenaSQL2 = ""
                            'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
                            CadenaSQL2 = "UPDATE FactLiberadas SET Liberado = '" + DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value.ToString + "'  "
                            CadenaSQL2 &= "WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "
                            con2.ConnectionString = StrTpm
                            con2.Open()
                            cmd2.Connection = con2
                            cmd2.CommandText = CadenaSQL2
                            cmd2.ExecuteNonQuery()
                            Me.DgFactProv.RefreshEdit()
                            'Try
                            '    Dim con As New SqlConnection
                            '    Dim cmd As New SqlCommand
                            '    If MessageBox.Show("¿Confirma que no desea liberar está factura para ser pagada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            '        CadenaSQL = ""
                            '        DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value = 0


                            '        CadenaSQL = "UPDATE FactLiberadas SET Liberado = 0  "
                            '        CadenaSQL &= "WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "



                            '        con.ConnectionString = StrTpm
                            '        con.Open()
                            '        cmd.Connection = con
                            '        cmd.CommandText = CadenaSQL
                            '        cmd.Parameters.Add(New SqlParameter("@fecha", DateTime.Now))
                            '        cmd.ExecuteNonQuery()

                            '        Me.DgFactProv.RefreshEdit()
                            '    End If
                            '    con.Close()
                            'Catch ex As Exception
                            '    MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                            'Finally


                            'End Try


                            'Termina If de liberado

                        End If

                    End If



                    Dim aux As Integer = Me.DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value

                    If aux = 1 And UsrTPM = "MANAGER" Then

                        Dim row As DataGridViewRow = DgFactProv.Rows(e.RowIndex)

                        Try
                            If Me.DgFactProv.Columns(e.ColumnIndex).Name = "RegSel" And (UsrTPM = "MANAGER" Or UsrTPM = "TESORERIA") Then
                                'VALIDA SI ESTA BLOQUEADO O NO LA FACTURA Y QUE EL USUARIO SEA DE COMPRAS


                                If (CInt(Me.DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value) = 0) Then
                                    'The user clicked on the checkbox column
                                    strValue = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value
                                    If (strValue = "1") Then
                                        Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value = DBNull.Value
                                    End If

                                    strValue = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value

                                    'MsgBox(strValue)
                                    If MessageBox.Show("¿Confirma que esta factura fue pagada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                        DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 1
                                        DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value = 0
                                        DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Value = DateTime.Now

                                        Me.DgFactProv.RefreshEdit()

                                        Dim con As New SqlConnection
                                        Dim cmd As New SqlCommand
                                        Dim CadenaSQL As String = ""
                                        'Dim CadenaSQL2 As String = ""

                                        'UsrTPM = "MANAGER"
                                        CadenaSQL = "INSERT INTO FCOMP (FactCompras,Fecha,Id_Usuario) VALUES ("
                                        CadenaSQL &= DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString
                                        CadenaSQL &= ","
                                        CadenaSQL &= "@fecha"
                                        CadenaSQL &= ",'"
                                        CadenaSQL &= UsrTPM
                                        CadenaSQL &= "')"



                                        Try
                                            con.ConnectionString = StrTpm
                                            con.Open()
                                            cmd.Connection = con
                                            cmd.CommandText = CadenaSQL
                                            cmd.Parameters.Add(New SqlParameter("@fecha", DateTime.Now))
                                            cmd.ExecuteNonQuery()

                                            Me.DgFactProv.RefreshEdit()

                                        Catch ex As Exception
                                            MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                        Finally

                                            con.Close()
                                        End Try
                                        'Actualizar(DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString)

                                    Else

                                        DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 0
                                        Me.DgFactProv.RefreshEdit()

                                    End If
                                ElseIf (CInt(Me.DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value) = 1) Then
                                    MessageBox.Show("La factura esta bloqueada para pago.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Return
                                End If
                            End If
                        Catch ex As Exception
                            VErrorG = 1
                        End Try

                    ElseIf DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 1 Then

                        DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 0
                        VErrorG = 1
                        'Me.DgFactProv.RefreshEdit()
                        'VErrorG = 1
                    Else
                        MessageBox.Show("La factura debe ser liberada para su pago")
                    End If

                    If VErrorG = 1 And DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 0 Then
                        If MessageBox.Show("¿Confirma que esta factura" + Chr(13) + "No ha sido pagada?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                            DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 0
                            DgFactProv.Rows(e.RowIndex).Cells("FechaPagada").Value = DBNull.Value

                            Me.DgFactProv.RefreshEdit()
                            Dim con As New SqlConnection
                            Dim cmd As New SqlCommand
                            Try
                                con.ConnectionString = StrTpm
                                con.Open()
                                cmd.Connection = con
                                cmd.CommandText = "Delete From FCOMP where FactCompras = @FactComp"
                                cmd.Parameters.Add(New SqlParameter("@FactComp", DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString))
                                cmd.ExecuteNonQuery()

                            Catch ex As Exception
                                MessageBox.Show("Error Eliminando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                            Finally

                                con.Close()
                            End Try
                            DgFactProv.Rows(e.RowIndex).Cells("Liberado").Value = 1
                        Else
                            DgFactProv.Rows(e.RowIndex).Cells("RegSel").Value = 1

                            Me.DgFactProv.RefreshEdit()

                        End If

                    End If

                Else
                    MessageBox.Show("La factura está bloqueada para su pago")
                End If


            Else

                If UsrTPM = "COMPRAS1" Then

                    If (Me.DgFactProv.Columns(e.ColumnIndex).Name = "Frozen" And UsrTPM <> "MANAGER") Then 'VALIDA SI SE VA A BLOQUEAR LA ORDEN LA FACTURA PARA PAGO Y QUE EL USUARIO NO SEA MANAGER
                        strBloq = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value
                        If (strBloq = "1") Then
                            Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value = DBNull.Value
                            DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value = 0
                            'REFRESCA EL CHECK
                            Me.DgFactProv.RefreshEdit()
                            Dim con As New SqlConnection
                            Dim cmd As New SqlCommand
                            Dim CadenaSQL As String = ""

                            'VALIDA SI EXISTE EL REGISTRO
                            Try
                                'ALAMACENA LA CONSULTA
                                CadenaSQL = "SELECT * FROM COMP1 WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "
                                'CONECTA A LA BASE DE DATOS
                                conexion_universal.conectar()
                                'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
                                conexion_universal.slq_s = New SqlCommand(CadenaSQL, conexion_universal.conexion_uni)
                                'EJECUTA LA CONSULTA
                                conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
                                'RECORRE LA CONSULTA
                                If conexion_universal.rd_s.Read Then
                                    CadenaSQL = ""
                                    'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
                                    CadenaSQL = "UPDATE COMP1 SET Frozen = '" + DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value.ToString + "'  "
                                    CadenaSQL &= "WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "
                                    con.ConnectionString = StrTpm
                                    con.Open()
                                    cmd.Connection = con
                                    cmd.CommandText = CadenaSQL
                                    cmd.ExecuteNonQuery()

                                    Me.DgFactProv.RefreshEdit()
                                End If
                            Catch ex As Exception
                                MessageBox.Show("Error al actualizar el Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Finally
                                'CIERRA LAS CONEXIONES DE USO
                                con.Close()
                                conexion_universal.cerrar_conectar()
                            End Try

                            DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value = 0
                            Me.DgFactProv.RefreshEdit()
                        Else
                            If MessageBox.Show("¿Relamente desea bloquear la factura para pago?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value = 1
                                Me.DgFactProv.RefreshEdit()

                                Dim con As New SqlConnection
                                Dim cmd As New SqlCommand
                                Dim CadenaSQL As String = ""

                                'VALIDA SI EXISTE EL REGISTRO
                                Try
                                    'ALAMACENA LA CONSULTA
                                    CadenaSQL = "SELECT * FROM COMP1 WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "
                                    'CONECTA A LA BASE DE DATOS
                                    conexion_universal.conectar()
                                    'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
                                    conexion_universal.slq_s = New SqlCommand(CadenaSQL, conexion_universal.conexion_uni)
                                    'EJECUTA LA CONSULTA
                                    conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
                                    'RECORRE LA CONSULTA
                                    If conexion_universal.rd_s.Read Then
                                        CadenaSQL = ""
                                        'ALMACENA LA CONSULTA DE ACTUALIZACIÓN
                                        CadenaSQL = "UPDATE COMP1 SET Frozen = '" + DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value.ToString + "'  "
                                        CadenaSQL &= "WHERE FactCompras = " + DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString + " "
                                        con.ConnectionString = StrTpm
                                        con.Open()
                                        cmd.Connection = con
                                        cmd.CommandText = CadenaSQL
                                        cmd.ExecuteNonQuery()
                                        Me.DgFactProv.RefreshEdit()
                                    Else
                                        CadenaSQL = ""
                                        'ALMACENA LA CONSULTA
                                        CadenaSQL = "INSERT INTO COMP1 (FactCompras,Fecha,Id_Usuario, Frozen) VALUES ("
                                        CadenaSQL &= DgFactProv.Rows(e.RowIndex).Cells("Factura").Value.ToString
                                        CadenaSQL &= ","
                                        CadenaSQL &= "@fecha"
                                        CadenaSQL &= ",'"
                                        CadenaSQL &= UsrTPM
                                        CadenaSQL &= "', '" + DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value.ToString + "') "
                                        con.ConnectionString = StrTpm
                                        con.Open()
                                        cmd.Connection = con
                                        cmd.CommandText = CadenaSQL
                                        cmd.Parameters.Add(New SqlParameter("@fecha", DateTime.Now))
                                        cmd.ExecuteNonQuery()

                                        Me.DgFactProv.RefreshEdit()
                                    End If
                                Catch ex As Exception
                                    MessageBox.Show("Error al actualizar o insertar el Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Finally
                                    'CIERRA LAS CONEXIONES DE USO
                                    con.Close()
                                    conexion_universal.cerrar_conectar()
                                End Try
                            Else
                                DgFactProv.Rows(e.RowIndex).Cells("Frozen").Value = 0
                                Me.DgFactProv.RefreshEdit()
                            End If
                        End If

                    End If


                Else
                    MessageBox.Show("La factura debe ser liberada para su pago")
                End If




                TotalFacturas()


            End If
        End If
    End Sub
    Private Sub DgFactProv_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles DgFactProv.CurrentCellDirtyStateChanged

        'Este codigo sirve para que se pueda identificar el proceso del checkbox dentro del datagridview junto
        'con el evento de DgFactProv_CellContentClick
        If DgFactProv.IsCurrentCellDirty Then
            DgFactProv.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Sub TotalFacturas()
        Dim VTotFProv As Decimal = 0

        For Each row As DataGridViewRow In Me.DgFactProv.Rows
            If row.Cells("RegSel").Value = False Then
                VTotFProv += row.Cells("SaldoPesos").Value
            End If

        Next

        TxtTotEnPesos.Text = Format(VTotFProv, "$ ##,###,###,###.00")
    End Sub

    'METODO DE BLOQUEAR FACTURAS
    Sub MFacturaBloq()
        'RECORRE EL GRID Y COMPARA CON LA BASE DE DATOS SI LA FACTURA ESTA BLOQUEADA
        For i As Integer = 0 To DgFactProv.Rows.Count - 1
            Try
                'APERTURA DE CONEXION
                conexion_universal.conexion_uni.Open()
                'VARIABLE DE CONSULTA
                Dim SQLBuscar As String = ""
                'ALMACENA LA CONSULTA
                SQLBuscar = "SELECT CASE WHEN Frozen IS NULL THEN 0 ELSE Frozen END AS Frozen "
                SQLBuscar &= "FROM TPM.dbo.COMP1 "
                SQLBuscar &= "WHERE FactCompras = " + DgFactProv.Rows(i).Cells("Factura").Value.ToString + " "
                'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
                conexion_universal.slq_s = New SqlCommand(SQLBuscar, conexion_universal.conexion_uni)
                'EJECUTA LA CONSULTA
                conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
                'RECORRE LA CONSULTA
                If conexion_universal.rd_s.Read Then
                    'VALIDA SI LA FACTURA ESTA BLOQUEADO
                    If conexion_universal.rd_s.Item("Frozen") = 1 Then
                        DgFactProv.Rows(i).Cells("Frozen").Value = 1

                        'COLOCA COLOR A TODA LA FILA
                        DgFactProv.Rows(i).Cells(0).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(1).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(2).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(3).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(4).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(4).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(4).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(5).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(6).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(7).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(8).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(9).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(10).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(11).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(12).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(13).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(14).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(15).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(16).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(17).Style.BackColor = amarillo
                        DgFactProv.Rows(i).Cells(18).Style.BackColor = amarillo

                    Else
                        DgFactProv.Rows(i).Cells("Frozen").Value = 0
                    End If
                End If
            Catch ex As Exception
                MsgBox("Error al buscar la factura bloqueada: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de Busqueda")
                'CIERRA LA CONEXION
                conexion_universal.conexion_uni.Close()
                Return
            Finally
                'CIERRA LAS CONEXIONES DE USO
                conexion_universal.conexion_uni.Close()
            End Try
        Next
    End Sub

    Private Sub ChkVerPagadas_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkVerPagadas.CheckedChanged

        If RdBCompras.Checked = True Then
            VisualizarProv()
            'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
            MFacturaBloq()
        Else
            VisualizarProv2()
            'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
            MFacturaBloq()
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If (RadioButton1.Checked = True) Then
            limpiarDataGrid()
            'DgFactProv.DataSource = ""
            Label1.Text = "Gasto:"
            Label1.Location = New Point(30, 98)
            ChkVisDisa.Visible = False
            ChkVerPagadas.Location = New Point(618, 64)
            CmbAgteVta.Visible = False
            DgFactProv.Height = DgFactProv.Height - 25
            DgFactProv.Location = New Point(7, 180)
            Label6.Location = New Point(7, 160)
            CmbGasto.Location = New Point(83, 97)
            CmbGasto.Visible = True
            Label8.Location = New Point(5, 131)
            Label8.Visible = True
            CmbProveedor.Location = New Point(83, 131)
            CmbProveedor.Visible = True
            TxtTotEnPesos.Text = ""
            ChkUSD.Location = New Point(790, 64)
            'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
            MFacturaBloq()


        Else
            limpiarDataGrid()
            'DgFactProv.DataSource = ""
            Label1.Text = "Proveedor"
            Label1.Location = New Point(5, 98)
            ChkVisDisa.Visible = True
            ChkVerPagadas.Location = New Point(775, 64)
            CmbAgteVta.Visible = True
            DgFactProv.Height = DgFactProv.Height + 25
            DgFactProv.Location = New Point(7, 155)
            Label6.Location = New Point(7, 135)
            CmbGasto.Location = New Point(509, 8)
            CmbGasto.Visible = False
            Label8.Location = New Point(429, 34)
            Label8.Visible = False
            CmbProveedor.Location = New Point(509, 33)
            CmbProveedor.Visible = False
            TxtTotEnPesos.Text = ""
            ChkUSD.Location = New Point(949, 64)
            'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
            MFacturaBloq()

        End If
    End Sub
    Public Sub limpiarDataGrid()
        For i As Integer = 0 To Me.DgFactProv.RowCount - 1
            Me.DgFactProv.Rows.Remove(Me.DgFactProv.CurrentRow)
        Next

    End Sub

    Private Sub CmbGasto_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CmbGasto.SelectionChangeCommitted
        If (CmbGasto.SelectedValue = "TODOS") Then
            DvProveedor.RowFilter = String.Empty
            Me.CmbProveedor.SelectedValue = "TODOS"
        Else
            DvProveedor.RowFilter = "U_BXP_CONCEPTO = '" & CmbGasto.SelectedValue.ToString & "' or CardName = 'TODOS'"
            Me.CmbProveedor.SelectedValue = "TODOS"
        End If
        ''DvProveedor.RowFilter = "U_BXP_CONCEPTO = '" & CmbGasto.SelectedValue.ToString & "'"
    End Sub

    Private Sub CmbGasto_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbGasto.Validating
        If (CmbGasto.Text <> "") Then
            If (CmbGasto.FindString(CmbGasto.Text.ToString).ToString <> "-1") Then
                CmbGasto.SelectedIndex = CmbGasto.FindString(CmbGasto.Text.ToString)
                'MsgBox(CmbGasto.SelectedValue.ToString)
                If (CmbGasto.SelectedValue = "TODOS") Then
                    DvProveedor.RowFilter = String.Empty
                    Me.CmbProveedor.SelectedValue = "TODOS"
                Else
                    DvProveedor.RowFilter = "U_BXP_CONCEPTO = '" & CmbGasto.SelectedValue.ToString & "' or CardName = 'TODOS'"
                    Me.CmbProveedor.SelectedValue = "TODOS"
                End If

            Else
                CmbGasto.SelectedIndex = -1
                DvProveedor.RowFilter = "U_BXP_CONCEPTO = 'TODOS'"
            End If
        End If
    End Sub

    Private Sub CmbProveedor_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbProveedor.Validating
        If CmbProveedor.Text <> "" Then
            If (CmbProveedor.FindString(CmbProveedor.Text.ToString).ToString <> "-1") Then
                CmbProveedor.SelectedIndex = CmbProveedor.FindString(CmbProveedor.Text.ToString)
            Else
                CmbProveedor.SelectedIndex = -1
            End If
        End If
    End Sub

    Private Sub ChkUSD_CheckedChanged(sender As Object, e As EventArgs) Handles ChkUSD.CheckedChanged
        If RdBCompras.Checked = True Then
            VisualizarProv()
            'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
            MFacturaBloq()
        Else
            VisualizarProv2()
            'MANDA A LLAMAR EL METODO DE COLOCAR EL CHECK EN ACTIVADO
            MFacturaBloq()
        End If
    End Sub

    'FALTA AUTORIZA 
    'MODIFICO IVAN GONZALEZ
    'FALTA AGREGAR CHECKBOX
    Sub cargar_FacturasCerradas()

        Dim DTRefacciones As New DataTable

        ' crear nueva conexión    
        Dim conexion2 As New SqlConnection(StrCon)

        ' abrir la conexión con la base de datos   
        conexion2.Open()

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand

        Dim SQLTPD As String

        SQLTPD = "SELECT CASE WHEN T3.FactCompras IS NULL OR T3.FactCompras=0 THEN 0 ELSE 1 END AS RegSel, "
        SQLTPD &= "T0.DocDate AS FchDoc, "
        SQLTPD &= "T2.PymntGroup AS DiasCred,T0.DocNum AS Factura, CASE WHEN T0.FolioPref IS NULL THEN '' ELSE T0.FolioPref END "
        SQLTPD &= "+ CASE WHEN CAST(T0.U_Factura AS nvarchar(10)) IS NULL THEN '' ELSE CAST(T0.U_Factura AS nvarchar(10)) END AS FactProv,"
        SQLTPD &= "CASE WHEN U_IdLinea IS NULL THEN '' ELSE U_IdLinea END U_IdLinea,"
        SQLTPD &= "T1.CardCode AS IdProved,T1.CardName AS Proveedor, 'a' as 'TipoGasto',"
        SQLTPD &= "case when T0.u_bxp_descvarios < 0 or T0.u_bxp_descvarios is null  then t1.U_BXP_Descuentospp else case when t1.U_BXP_Descuentospp  < 0 or T0.u_bxp_descvarios is null then 0 else t1.U_BXP_Descuentospp end end  as 'Descuento',"
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal ELSE T0.DocTotalFC END AS TotFactura,T0.DocCur AS Moneda,"
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.PaidToDate ELSE T0.PaidFC END AS Pagado,"
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal - T0.PaidToDate ELSE T0.DocTotalFC - T0.PaidFC END AS SaldoPendiente,"
        SQLTPD &= "T0.DocCur AS Moneda2,DATEDIFF(DAY,T0.DocDueDate,GETDATE()) AS DiasAtraso,T0.DocDueDate AS FchVen,"
        SQLTPD &= "T0.DocTotal - T0.PaidToDate AS SaldoPesos,t0.NumAtCard Referencia,T1.Notes AS Obrserv,T4.Coment, T3.Fecha as 'FechaPagada' "
        SQLTPD &= "FROM [SBO_TPD].dbo.OPCH T0 "
        SQLTPD &= "INNER JOIN [SBO_TPD].dbo.OCRD T1 ON T0.CardCode = T1.CardCode AND (T1.CardCode LIKE '%P-%' OR T1.CardCode LIKE '%PIM-%') "
        SQLTPD &= "LEFT JOIN [SBO_TPD].dbo.OCTG T2 ON T2.GroupNum = T1.GroupNum "
        SQLTPD &= "LEFT JOIN [TPM].dbo.FCOMP T3 ON T0.DocNum = T3.FactCompras "
        SQLTPD &= "LEFT JOIN [TPM].dbo.COMP1 T4 ON T0.DocNum = T4.FactCompras "
        SQLTPD &= "WHERE T0.DocDueDate >= @FechaIni AND T0.DocDueDate <= @FechaTer "

        If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
            SQLTPD &= "AND T0.CardCode = @Agente "
        Else
            SQLTPD &= "AND T0.DocStatus = 'C' "
        End If
        SQLTPD &= " ORDER BY T0.DocDueDate, DiasAtraso, T0.DocNum DESC"

        ' Nuevo objeto Dataset   
        Dim DsVtasDet As New DataSet

        DsVtasDet.Tables.Add(DTRefacciones)

        With comando
            If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
                .Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
            End If
            .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
            .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
            ' Asignar el sql para seleccionar los datos de la tabla Maestro   
            .CommandText = SQLTPD
            .Connection = conexion2
        End With


        Dim DtFactProv As New DataTable

        With Adaptador
            .SelectCommand = comando
            ' llenar el dataset   
            .Fill(DtFactProv)
        End With

        DvFactProv = DtFactProv.DefaultView

        With Me.DgFactProv
            .DataSource = DvFactProv

            .AllowUserToAddRows = False

            'Pagado	
            '.Columns(0).HeaderText = "Pagado"

            'Fecha del documento	
            '.Columns(1).HeaderText = "fch doc"

            'Dias de credito	
            '.Columns(2).HeaderText = "credito"

            'Docto. SAP
            '.Columns(3).HeaderText = "Doc. SAP"


            'Factura
            '.Columns(4).HeaderText = "Factura"

            'Id Prov
            '.Columns(5).HeaderText = "ID prov"

            'Proveedor
            '.Columns(6).HeaderText = "Proveedor"

            '$Total Fact
            '.Columns(7).HeaderText = "$Total Factura"
            .Columns("TotFactura").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("TotFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Mnd
            '.Columns(8).HeaderText = "MND"
            .Columns("Moneda").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Importe Aplicado
            '.Columns(9).HeaderText = "Importe Aplicado"
            .Columns("Pagado").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("Pagado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Saldo Pendiente
            '.Columns(10).HeaderText = "$Saldo Pend"
            .Columns("SaldoPendiente").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("SaldoPendiente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Mnd
            '.Columns(11).HeaderText = "MND"
            .Columns("Moneda2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Dias Atraso
            '.Columns(12).HeaderText = "ID prov"
            .Columns("DiasAtraso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Fch Venc.
            '.Columns(13).HeaderText = "Fch venc."

            '$ Saldo MXP
            '.Columns(14).HeaderText = "$ Saldo MXP"
            .Columns("SaldoPesos").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("SaldoPesos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Referencia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            'Observ
            '.Columns(15).HeaderText = "Observaciones"
            .Columns("Obrserv").Visible = False

        End With



        With conexion2
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With
        MFacturaBloq()
    End Sub


    'Método para mostrar liberadas

    Sub Liberadas()
        'RECORRE EL GRID Y COMPARA CON LA BASE DE DATOS SI LA FACTURA ESTA BLOQUEADA
        For i As Integer = 0 To DgFactProv.Rows.Count - 1
            Try
                'APERTURA DE CONEXION
                conexion_universal.conexion_uni.Open()
                'VARIABLE DE CONSULTA
                Dim SQLBuscar As String = ""
                'ALMACENA LA CONSULTA
                SQLBuscar = "SELECT CASE WHEN Liberado IS NULL THEN 0 ELSE Liberado END AS Liberado "
                SQLBuscar &= "FROM TPM.dbo.FactLiberadas "
                SQLBuscar &= "WHERE FactCompras = " + DgFactProv.Rows(i).Cells("Factura").Value.ToString + " and Liberado=1"
                'CONSULTA DE OBTENCIÓN DEL NOMBRE DEL USUARIO
                conexion_universal.slq_s = New SqlCommand(SQLBuscar, conexion_universal.conexion_uni)
                'EJECUTA LA CONSULTA
                conexion_universal.rd_s = conexion_universal.slq_s.ExecuteReader()
                'RECORRE LA CONSULTA
                If conexion_universal.rd_s.Read Then
                    'VALIDA SI LA FACTURA ESTA BLOQUEADO
                    If conexion_universal.rd_s.Item("Liberado") = 1 Then
                        DgFactProv.Rows(i).Cells("Liberado").Value = 1

                        'COLOCA COLOR A TODA LA FILA
                        DgFactProv.Rows(i).Cells(0).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(1).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(2).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(3).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(4).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(4).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(4).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(5).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(6).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(7).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(8).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(9).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(10).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(11).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(12).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(13).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(14).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(15).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(16).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(17).Style.BackColor = Color.GreenYellow
                        DgFactProv.Rows(i).Cells(18).Style.BackColor = Color.GreenYellow

                    Else
                        DgFactProv.Rows(i).Cells("Liberado").Value = 0
                    End If
                End If
            Catch ex As Exception
                MsgBox("Error al buscar la factura bloqueada: " + ex.ToString, MsgBoxStyle.Exclamation, "Alerta de Busqueda")
                'CIERRA LA CONEXION
                conexion_universal.conexion_uni.Close()
                Return
            Finally
                'CIERRA LAS CONEXIONES DE USO
                conexion_universal.conexion_uni.Close()
            End Try
        Next
    End Sub


    Private Sub ckbFacturasCerradas_CheckedChanged(sender As Object, e As EventArgs) Handles ckbFacturasCerradas.CheckedChanged
        If ckbFacturasCerradas.Checked Then
            cargar_FacturasCerradas()
        Else
            limpiarDataGrid()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            cargar_FacturasLiberadas()
            Liberadas()


        End If

    End Sub


    Sub cargar_FacturasLiberadas()

        Dim DTRefacciones As New DataTable

        ' crear nueva conexión    
        Dim conexion2 As New SqlConnection(StrCon)

        ' abrir la conexión con la base de datos   
        conexion2.Open()

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand

        Dim SQLTPD As String

        SQLTPD = "SELECT CASE WHEN T3.FactCompras IS NULL OR T3.FactCompras=0 THEN 0 ELSE 1 END AS RegSel,"
        'SQLTPD &= "1 as Liberado,"
        SQLTPD &= "T0.DocDate AS FchDoc,"
        SQLTPD &= "T2.PymntGroup AS DiasCred,t1.U_BXP_BancoOrigen as BancoOrigen,t1.U_BXP_BancoDestino as BancoDestino,t1.U_BXP_CtaDestino as CuentaDestino,T0.DocNum AS Factura, CASE WHEN T0.FolioPref IS NULL THEN '' ELSE T0.FolioPref END "
        SQLTPD &= "+ CASE WHEN CAST(T0.U_Factura AS nvarchar(10)) IS NULL THEN '' ELSE CAST(T0.U_Factura AS nvarchar(10)) END AS FactProv,"
        SQLTPD &= "CASE WHEN U_IdLinea IS NULL THEN '' ELSE U_IdLinea END U_IdLinea,"
        SQLTPD &= "T1.CardCode AS IdProved,T1.CardName AS Proveedor, 'a' as 'TipoGasto',"
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal ELSE T0.DocTotalFC END AS TotFactura,T0.DocCur AS Moneda,"
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.PaidToDate ELSE T0.PaidFC END AS Pagado,"
        SQLTPD &= "CASE WHEN T0.DocCur = 'MXP' THEN T0.DocTotal - T0.PaidToDate ELSE T0.DocTotalFC - T0.PaidFC END AS SaldoPendiente,"
        SQLTPD &= "T0.DocCur AS Moneda2,DATEDIFF(DAY,T0.DocDueDate,GETDATE()) AS DiasAtraso,T0.DocDueDate AS FchVen,"
        SQLTPD &= "T0.DocTotal - T0.PaidToDate AS SaldoPesos,t0.NumAtCard Referencia,T1.Notes AS Obrserv,T4.Coment, T3.Fecha as 'FechaPagada' "
        SQLTPD &= "FROM [SBO_TPD].dbo.OPCH T0 "
        SQLTPD &= "INNER JOIN [SBO_TPD].dbo.OCRD T1 ON T0.CardCode = T1.CardCode AND (T1.CardCode LIKE '%P-%' OR T1.CardCode LIKE '%PIM-%') "
        SQLTPD &= "LEFT JOIN [SBO_TPD].dbo.OCTG T2 ON T2.GroupNum = T1.GroupNum "
        SQLTPD &= "LEFT JOIN [TPM].dbo.FCOMP T3 ON T0.DocNum = T3.FactCompras "
        SQLTPD &= "LEFT JOIN [TPM].dbo.COMP1 T4 ON T0.DocNum = T4.FactCompras "
        SQLTPD &= "LEFT JOIN [TPM].dbo.FactLiberadas T5 ON T0.DocNum = T5.FactCompras "
        SQLTPD &= "WHERE T0.DocDueDate >= @FechaIni AND T0.DocDueDate <= @FechaTer and T5.Liberado = 1 and T3.FactCompras=0  "

        If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
            SQLTPD &= "AND T0.CardCode = @Agente "
        Else
            SQLTPD &= "AND T0.DocStatus = 'O' "
        End If
        'SQLTPD2 &= " and T5.Liberado = 1 "
        SQLTPD &= " ORDER BY T0.DocDueDate, DiasAtraso, T0.DocNum DESC"

        ' Nuevo objeto Dataset   
        Dim DsVtasDet As New DataSet

        DsVtasDet.Tables.Add(DTRefacciones)

        With comando
            If Me.CmbAgteVta.SelectedValue <> "TODOS" Then
                .Parameters.AddWithValue("@Agente", CmbAgteVta.SelectedValue)
            End If
            .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
            .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
            ' Asignar el sql para seleccionar los datos de la tabla Maestro   
            .CommandText = SQLTPD
            .Connection = conexion2
        End With


        Dim DtFactProv As New DataTable

        With Adaptador
            .SelectCommand = comando
            ' llenar el dataset   
            .Fill(DtFactProv)
        End With

        DvFactProv = DtFactProv.DefaultView

        With Me.DgFactProv
            .DataSource = DvFactProv

            .AllowUserToAddRows = False

            'Pagado	
            '.Columns(0).HeaderText = "Pagado"

            'Fecha del documento	
            '.Columns(1).HeaderText = "fch doc"

            'Dias de credito	
            '.Columns(2).HeaderText = "credito"

            'Docto. SAP
            '.Columns(3).HeaderText = "Doc. SAP"


            'Factura
            '.Columns(4).HeaderText = "Factura"

            'Id Prov
            '.Columns(5).HeaderText = "ID prov"

            'Proveedor
            '.Columns(6).HeaderText = "Proveedor"

            '$Total Fact
            '.Columns(7).HeaderText = "$Total Factura"
            .Columns("TotFactura").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("TotFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Mnd
            '.Columns(8).HeaderText = "MND"
            .Columns("Moneda").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Importe Aplicado
            '.Columns(9).HeaderText = "Importe Aplicado"
            .Columns("Pagado").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("Pagado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Saldo Pendiente
            '.Columns(10).HeaderText = "$Saldo Pend"
            .Columns("SaldoPendiente").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("SaldoPendiente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'Mnd
            '.Columns(11).HeaderText = "MND"
            .Columns("Moneda2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Dias Atraso
            '.Columns(12).HeaderText = "ID prov"
            .Columns("DiasAtraso").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'Fch Venc.
            '.Columns(13).HeaderText = "Fch venc."

            '$ Saldo MXP
            '.Columns(14).HeaderText = "$ Saldo MXP"
            .Columns("SaldoPesos").DefaultCellStyle.Format = "$ ###,###.#0"
            .Columns("SaldoPesos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns("Referencia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            'Observ
            '.Columns(15).HeaderText = "Observaciones"
            .Columns("Obrserv").Visible = False

        End With



        With conexion2
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With



    End Sub

    Sub Actualizar(ByVal aux As Integer)

        Dim con2 As New SqlConnection
        Dim cmd2 As New SqlCommand
        Dim CadenaSQL2 As String = ""

        'UsrTPM = "MANAGER"


        CadenaSQL2 = "Update  FactLiberadas SET Liberado = 2"
        CadenaSQL2 &= "WHERE FactCompras = " + aux + " "


        ' Try
        con2.ConnectionString = StrTpm
        con2.Open()
        cmd2.Connection = con2
        cmd2.CommandText = CadenaSQL2
        'cmd2.Parameters.Add(New SqlParameter("@fecha", DateTime.Now))
        cmd2.ExecuteNonQuery()

        Me.DgFactProv.RefreshEdit()
        MessageBox.Show("Actualizado")
        'Catch ex As Exception
        'MessageBox.Show("Error Insertando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        'Finally

        con2.Close()
        'End Try
    End Sub

    Private Sub DgFactProv_Sorted(sender As Object, e As EventArgs) Handles DgFactProv.Sorted
        MFacturaBloq()
    End Sub
End Class
