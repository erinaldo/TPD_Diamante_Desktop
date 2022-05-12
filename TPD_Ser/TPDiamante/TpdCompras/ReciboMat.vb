Imports System.Data.SqlClient
Public Class ReciboMat
    Dim DvOrdCompra As New DataView
    Dim DvRecibos As New DataView
    Dim DVRecOrdF As New DataView
    Dim DvDetOrdComp As New DataView
    Dim DvFactEnt As New DataView
    Dim DvDetFactEnt As New DataView

    Private Sub ReciboMat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If UsrTPM = "MANAGER" Then

        '    DgvOrdCompra.Width = 792

        '    ''Recibo de material 
        '    Label8.Location = New Point(800, 68)
        '    DgvRecibos.Location = New Point(800, 85)

        '    ''Facturas/ entradas de mercancia 
        '    Label10.Location = New Point(800, 347)
        '    DgvFactEnt.Location = New Point(800, 365)

        '    ''Detalle de la orden 
        '    DgvDetOComp.Width = 792

        '    ''Recibo de material
        '    Label13.Location = New Point(800, 548)
        '    DgvRecOrdF.Location = New Point(800, 566)

        'End If

        If UsrTPM = "AINVEN" Then
            DgvOrdCompra.ReadOnly = True
            DgvDetOComp.ReadOnly = True
            DgvRecibos.ReadOnly = True
            DgvFactEnt.ReadOnly = True
            DgvRecOrdF.ReadOnly = True
            BtnMas.Enabled = False
            BtnMenos.Enabled = False
        End If

        Dim FchInicio As Date = DateSerial(Year(Date.Today), Date.Now.Month - 3, 1)


        'Dim dtFecha As Date = DateSerial(Year(Date.Today), Date.Now.Month, 1)

        'FchInicio = DateAdd(DateInterval.Month, -3, Date.Now)
        Me.DtpFechaIni.Value = Format(FchInicio, "dd/MM/yyyy")
        Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

        Dim ConsutaLista As String
        Dim DSetTablas As New DataSet

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
            ConsutaLista = "SELECT CardCode AS IdProv, CardName + ' - ' + Currency + ' - ' + CardCode  as Proveed FROM OCRD T0 WHERE T0.CardType = 'S' AND FROZENFOR <> 'Y' AND (T0.CardCode LIKE '%P-%' OR T0.CardCode LIKE '%PIM-%') ORDER BY T0.CardName"
            Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            daAgte.Fill(DSetTablas, "Proveedores")

            Dim filaAgte As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaAgte = DSetTablas.Tables("Proveedores").NewRow

            'Agregamos los valores a los campos de la tabla
            filaAgte("Proveed") = "TODOS"
            filaAgte("IdProv") = "TODOS"

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Proveedores").Rows.Add(filaAgte)

            Me.CmbProv.DataSource = DSetTablas.Tables("Proveedores")
            Me.CmbProv.DisplayMember = "Proveed"
            Me.CmbProv.ValueMember = "IdProv"
            Me.CmbProv.SelectedValue = "TODOS"

        End Using

        ComboBox1.SelectedItem = "TODOS"
    End Sub
    Sub cargar_registros()

        ' crear nueva conexión    
        Dim conexion As New SqlConnection(StrCon)

        ' abrir la conexión con la base de datos   
        conexion.Open()

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand

        Dim SQLTPD As String

        SQLTPD = "SELECT ROW_NUMBER() OVER (PARTITION BY T0.DocNum ORDER BY T1.FchRec ASC) AS Ordena,T1.FchRec AS FchRecibo,"
        SQLTPD &= "T1.Comentario,T1.Observacion,T0.DocNum AS OrdCompra,T0.DocEntry,"
        SQLTPD &= "T0.DocDate AS FchContab,T0.DocDueDate AS FchEnt,CAST(T1.FchRec AS DATE) AS FchRec,DATEDIFF(DAY, T0.DocDate,T0.DocDueDate) AS DiasEnt,"
        SQLTPD &= "CASE WHEN T1.Recibo IS NULL THEN DATEDIFF(DAY,T0.DocDate,GETDATE()) ELSE DATEDIFF(DAY,T0.DocDate,T1.FchRec) END AS DiasTrans,"
        SQLTPD &= "CASE WHEN T1.Recibo IS NULL THEN DATEDIFF(DAY,T0.DocDate,GETDATE()) ELSE DATEDIFF(DAY,T0.DocDate,T1.FchRec) END - "
        SQLTPD &= "DATEDIFF(DAY, T0.DocDate,T0.DocDueDate) AS DiasAtraso,"
        SQLTPD &= "T0.CardCode AS IdProv,T0.CardName AS NombreProv,T0.DocTotal AS TotalDoc,"
        SQLTPD &= "CASE WHEN T0.DocStatus = 'O' THEN 'Abierto' ELSE 'Cerrado' END AS EstatusDoc, T1.Recibo, t1.UsrCom "
        SQLTPD &= "INTO #T_OCREC "
        SQLTPD &= "FROM OPOR T0 "
        SQLTPD &= "LEFT JOIN TPM.dbo.RECMAT T1 ON T0.DocNum = T1.DocNum "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND T0.CANCELED = 'N'  "


        If Me.CmbProv.SelectedValue <> "TODOS" Then
            SQLTPD &= "AND T0.CardCode = @Agente "
        End If

        SQLTPD &= "ORDER BY T0.DocNum  "


        SQLTPD &= "SELECT T0.OrdCompra,T1.WhsCode, T2.WhsName INTO #OrdCompAlmacen "
        SQLTPD &= "FROM #T_OCREC T0 "
        SQLTPD &= "LEFT JOIN POR1 T1 ON T0.DocEntry = T1.DocEntry "
        SQLTPD &= "LEFT JOIN OWHS T2 ON T1.WhsCode = T2.WhsCode "
        SQLTPD &= "GROUP BY T0.OrdCompra,T1.WhsCode, T2.WhsName "


        '***ORIGINAL*** SQLTPD &= "ORIGINAL SELECT T0.OrdCompra,T0.FchContab,T0.IdProv, T0.NombreProv, T0.TotalDoc, T0.EstatusDoc,T0.FchEnt, "
        'SQLTPD &= "T0.FchRec, T0.DiasEnt, T0.DiasTrans, T0.DiasAtraso, T1.Comentario "
        'SQLTPD &= "FROM #T_OCREC T0 LEFT JOIN TPM.dbo.RECCOC T1 ON T0.OrdCompra = T1.OrdCompra WHERE T0.Ordena = 1; "

        '***Consulta 1
        SQLTPD &= "SELECT T0.OrdCompra,T0.FchContab,T0.IdProv, T0.NombreProv, T0.TotalDoc, T0.EstatusDoc,T0.FchEnt, "
        SQLTPD &= "T0.FchRec, T0.DiasEnt, T0.DiasTrans, T0.DiasAtraso, T3.WhsName, "
        SQLTPD &= "T2.MA, T2.GR, T2.RP, T2.EX, T2.EN, T2.FC, T1.Comentario "
        SQLTPD &= "FROM #T_OCREC T0 "
        SQLTPD &= "LEFT JOIN TPM.dbo.RECCOC T1 ON T0.OrdCompra = T1.OrdCompra "
        SQLTPD &= "LEFT JOIN TPM.dbo.RECENT T2 ON T0.OrdCompra = T2.OrdCompra "
        SQLTPD &= "LEFT JOIN #OrdCompAlmacen T3 ON T0.OrdCompra = T3.OrdCompra "
        SQLTPD &= "WHERE T0.Ordena = 1 "

        If Me.ComboBox1.SelectedItem.ToString <> "TODOS" Then
            If Me.ComboBox1.SelectedItem.ToString = "PUEBLA" Then
                SQLTPD &= " AND T3.WhsName LIKE 'PUEBLA%' "
            ElseIf Me.ComboBox1.SelectedItem.ToString = "MÉRIDA" Then
                SQLTPD &= " AND T3.WhsName = 'MÉRIDA' "
            ElseIf Me.ComboBox1.SelectedItem.ToString = "TUXTLA GTZ" Then
                SQLTPD &= " AND T3.WhsName = 'TUXTLA GTZ' "
            End If
        End If


        '***Consulta 2
        SQLTPD &= "SELECT Ordena, FchRecibo, FchEnt, IdProv, NombreProv, OrdCompra, Comentario, Observacion, Recibo, UsrCom  "
        SQLTPD &= "FROM #T_OCREC T0 WHERE T0.FchRec IS NOT NULL "

        'LEFT JOIN TPM.dbo.RECCOC T1 ON T0.OrdCompra = T1.OrdCompra

        'SQLTPD &= "SELECT T0.Ordena, T0.FchRecibo, T0.FchEnt, T0.IdProv, T0.NombreProv, T0.OrdCompra, "
        'SQLTPD &= "T0.Comentario, T0.Observacion, T0.Recibo,"
        'SQLTPD &= "T1.MA, T1.GR, T1.RP, T1.EX, T1.EN, T1.FC "
        'SQLTPD &= "FROM #T_OCREC T0 "
        'SQLTPD &= "LEFT JOIN TPM.dbo.RECENT T1 ON T0.OrdCompra = T1.OrdCompra "
        'SQLTPD &= "WHERE T0.FchRec IS NOT NULL "

        SQLTPD &= "SELECT T1.LINENUM + 1 AS LineNum, T1.ItemCode AS Articulo,T1.Dscription AS Descripcion, T5.ItmsGrpNam AS Linea, T6.WhsName, "
        SQLTPD &= "T1.Quantity AS Cantidad,T1.Quantity - T1.OpenQty AS Recibido,T1.OpenQty AS Pendiente,T3.OnHand AS Existencia,"
        SQLTPD &= "CASE WHEN T1.Currency <> 'MXP' THEN CASE WHEN T1.LineTotal <= 0 THEN 0 ELSE T1.LineTotal / T1.Quantity END ELSE T1.Price END AS Precio,"
        SQLTPD &= "T1.LineTotal AS TotalLinea, T1.LineStatus AS Estatus, T0.OrdCompra, "
        SQLTPD &= "T1.BASEENTRY, T1.BASETYPE, T1.BASELINE, T1.DOCENTRY, T1.OBJTYPE "
        SQLTPD &= "INTO #T_DETOCREC "
        SQLTPD &= "FROM #T_OCREC T0 "
        SQLTPD &= "LEFT JOIN POR1 T1 ON T0.DocEntry = T1.DocEntry "
        SQLTPD &= "LEFT JOIN OITW T3 ON T1.ItemCode = T3.ItemCode AND T3.WhsCode = 01 "
        SQLTPD &= "LEFT JOIN OITM T4 ON T1.ItemCode = T4.ItemCode "
        SQLTPD &= "LEFT JOIN OITB T5 ON T4.ItmsGrpCod = T5.ItmsGrpCod "
        SQLTPD &= "LEFT JOIN #OrdCompAlmacen T6 ON t0.OrdCompra = T6.OrdCompra "
        SQLTPD &= "WHERE T0.Ordena = 1 ORDER BY T1.DocEntry,T1.LineNum "

        'SQLTPD &= "SELECT T1.LINENUM + 1 AS LineNum, T1.ItemCode AS Articulo,T1.Dscription AS Descripcion, T5.ItmsGrpNam AS Linea, "    ', T6.WhsName,
        'SQLTPD &= "T1.Quantity AS Cantidad,T1.Quantity - T1.OpenQty AS Recibido,T1.OpenQty AS Pendiente,T3.OnHand AS Existencia,"
        'SQLTPD &= "CASE WHEN T1.Currency <> 'MXP' THEN CASE WHEN T1.LineTotal <= 0 THEN 0 ELSE T1.LineTotal / T1.Quantity END ELSE T1.Price END AS Precio, "
        'SQLTPD &= "T1.LineTotal AS TotalLinea, T1.LineStatus AS Estatus, T0.OrdCompra, "
        'SQLTPD &= "T1.BASEENTRY, T1.BASETYPE, T1.BASELINE, T1.DOCENTRY, T1.OBJTYPE "
        'SQLTPD &= "INTO #T_DETOCREC "
        'SQLTPD &= "FROM #T_OCREC T0 "
        'SQLTPD &= "LEFT JOIN POR1 T1 ON T0.DocEntry = T1.DocEntry "
        'SQLTPD &= "LEFT JOIN OITW T3 ON T1.ItemCode = T3.ItemCode AND T3.WhsCode = 01 "
        'SQLTPD &= "LEFT JOIN OITM T4 ON T1.ItemCode = T4.ItemCode "
        'SQLTPD &= "LEFT JOIN OITB T5 ON T4.ItmsGrpCod = T5.ItmsGrpCod "
        'SQLTPD &= "LEFT JOIN #OrdCompAlmacen T6 ON t0.OrdCompra=T6.OrdCompra "
        'SQLTPD &= "WHERE T0.Ordena = 1  ORDER BY T1.DocEntry, T1.LineNum"


        '***Consulta 3
        SQLTPD &= "SELECT * FROM #T_DETOCREC T0 ORDER BY T0.DOCENTRY,T0.LINENUM  "


        SQLTPD &= "SELECT CASE WHEN T3.OBJTYPE = '18' AND T3.ISINS = 'Y' THEN 'Fact. Reserva Prov.' "
        SQLTPD &= "WHEN T3.OBJTYPE = '18' AND T3.ISINS = 'N' THEN 'Factura Proveedores' "
        SQLTPD &= "WHEN T5.OBJTYPE = '20' THEN 'Entrada Mercancias' "
        SQLTPD &= "ELSE NULL END AS TipoDoc,"
        SQLTPD &= "CASE WHEN T3.DocNum IS NULL THEN T5.DocNum ELSE T3.DocNum END AS NumDoc,"
        SQLTPD &= "CASE WHEN T3.DocDate IS NULL THEN T5.DocDate ELSE T3.DocDate END AS FchContab,"
        SQLTPD &= "CASE WHEN T3.DocTotal IS NULL THEN T5.DocTotal ELSE T3.DocTotal END AS TotalDoc,"
        SQLTPD &= "CASE WHEN T2.ItemCode IS NULL THEN T4.ItemCode ELSE T2.ItemCode END AS Articulo,"
        SQLTPD &= "CASE WHEN T2.Dscription IS NULL THEN T4.Dscription ELSE T2.Dscription END AS Descripcion,"
        SQLTPD &= "CASE WHEN T2.Quantity IS NULL THEN T4.Quantity ELSE T2.Quantity END AS Cantidad,"
        SQLTPD &= "CASE WHEN T2.Currency IS NULL THEN "
        SQLTPD &= "CASE WHEN T4.Currency <> 'MXP' THEN CASE WHEN T4.LineTotal <= 0 THEN 0 ELSE T4.LineTotal / T4.Quantity END ELSE T4.Price END "
        SQLTPD &= "ELSE "
        SQLTPD &= "CASE WHEN T2.Currency <> 'MXP' THEN CASE WHEN T2.LineTotal <= 0 THEN 0 ELSE T2.LineTotal / T2.Quantity END ELSE T2.Price END "
        SQLTPD &= "END AS Precio,"
        SQLTPD &= "CASE WHEN T2.LineTotal IS NULL THEN T4.LineTotal ELSE T2.LineTotal END AS TotalLinea,"
        SQLTPD &= "T3.FolioPref, T3.FolioNum, T0.OrdCompra "
        SQLTPD &= "INTO #T_DTOCFACDET "
        SQLTPD &= "FROM #T_DETOCREC T0 "
        SQLTPD &= "LEFT JOIN PCH1 T2 ON T2.BASEENTRY = T0.DOCENTRY AND T2.BASETYPE = T0.OBJTYPE AND T2.BASELINE = T0.LINENUM "
        SQLTPD &= "LEFT JOIN OPCH T3 ON T2.DOCENTRY = T3.DOCENTRY "
        SQLTPD &= "LEFT JOIN PDN1 T4 ON T4.BASEENTRY = T0.DOCENTRY AND T4.BASETYPE = T0.OBJTYPE AND T4.BASELINE = T0.LINENUM "
        SQLTPD &= "LEFT JOIN OPDN T5 ON T4.DOCENTRY = T5.DOCENTRY  "


        '***Consulta 4
        SQLTPD &= "SELECT T0.TipoDoc,T0.NumDoc,T0.FchContab, T0.TotalDoc,"
        SQLTPD &= "T0.FolioPref, T0.FolioNum, T0.OrdCompra "
        SQLTPD &= "FROM #T_DTOCFACDET T0 "
        SQLTPD &= "WHERE T0.TipoDoc Is Not NULL "
        SQLTPD &= "GROUP BY T0.OrdCompra,T0.TipoDoc,T0.NumDoc,T0.FchContab, T0.TotalDoc, T0.FolioPref, T0.FolioNum  "

        SQLTPD &= "DROP TABLE #T_OCREC "
        SQLTPD &= "DROP TABLE #T_DETOCREC "
        SQLTPD &= "DROP TABLE #T_DTOCFACDET "
        SQLTPD &= "DROP TABLE #OrdCompAlmacen"

        Dim DsRecMat As New DataSet


        With comando
            If Me.CmbProv.SelectedValue <> "TODOS" Then
                .Parameters.AddWithValue("@Agente", CmbProv.SelectedValue)
            End If
            .Parameters.AddWithValue("@FechaIni", Me.DtpFechaIni.Value)
            .Parameters.AddWithValue("@FechaTer", Me.DtpFechaTer.Value)
            ' Asignar el sql para seleccionar los datos de la tabla Maestro   
            .CommandText = SQLTPD
            .Connection = conexion
        End With


        Dim DtOrdCompra As New DataTable

        With Adaptador
            .SelectCommand = comando
            ' llenar el dataset   
            .Fill(DsRecMat)
        End With


        DsRecMat.Tables(0).TableName = "OrdCompra"
        DsRecMat.Tables(1).TableName = "Recibos"
        DsRecMat.Tables(2).TableName = "DetOrdComp"
        DsRecMat.Tables(3).TableName = "FactEnt"
        'DsRecMat.Tables(4).TableName = "DetFactEnt"------------
        '-----------------------DtOrdCompra()

        DvOrdCompra.Table = DsRecMat.Tables("OrdCompra")
        DvRecibos.Table = DsRecMat.Tables("Recibos")
        DVRecOrdF.Table = DsRecMat.Tables("Recibos")
        DvDetOrdComp.Table = DsRecMat.Tables("DetOrdComp")
        DvFactEnt.Table = DsRecMat.Tables("FactEnt")


        With Me.DgvOrdCompra
            .DataSource = DvOrdCompra

            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 39
            '39,50
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            .RowHeadersWidth = 25

            .AllowUserToAddRows = False

            .Columns(0).HeaderText = "Ord. Comp."
            .Columns(0).Width = 40
            .Columns(0).ReadOnly = True

            .Columns(1).HeaderText = "Fecha Documento"
            .Columns(1).Width = 68
            .Columns(1).ReadOnly = True

            .Columns(2).HeaderText = "Id Prov."
            .Columns(2).Width = 50
            .Columns(2).ReadOnly = True

            .Columns(3).HeaderText = "Nombre"
            .Columns(3).Width = 180
            .Columns(3).ReadOnly = True

            .Columns(4).HeaderText = "$ Total"
            .Columns(4).Width = 70
            .Columns(4).DefaultCellStyle.Format = "###,###,###.0"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).ReadOnly = True

            .Columns(5).HeaderText = "Estatus"
            .Columns(5).Width = 45
            .Columns(5).ReadOnly = True

            .Columns(6).HeaderText = "Fecha Entrega"
            .Columns(6).Width = 68
            .Columns(6).ReadOnly = True

            .Columns(7).HeaderText = "Fecha Recibo"
            .Columns(7).Width = 68
            .Columns(7).ReadOnly = True

            .Columns(8).HeaderText = "Ent."
            .Columns(8).Width = 30
            .Columns(8).DefaultCellStyle.Format = "###,###,###"
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(8).ReadOnly = True

            .Columns(9).HeaderText = "Real"
            .Columns(9).Width = 30
            .Columns(9).DefaultCellStyle.Format = "###,###,###"
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(9).ReadOnly = True

            .Columns(10).HeaderText = "Atra so"
            .Columns(10).Width = 30
            .Columns(10).DefaultCellStyle.Format = "###,###,###"
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(10).ReadOnly = True

            .Columns(11).HeaderText = "Almacen"
            .Columns(11).Width = 60
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(11).ReadOnly = False


            'If UsrTPM = "MANAGER" Or UsrTPM = "COMPRAS1" Or UsrTPM = "ACOMPRAS" Or UsrTPM = "AINVEN" Then
            ''DgvOrdCompra
            .Columns(3).Frozen = True

            .Columns(12).HeaderText = "MA"
            .Columns(12).Width = 25
            .Columns(12).Visible = False

            .Columns(13).HeaderText = "GR"
            .Columns(13).Width = 25
            .Columns(13).Visible = False

            .Columns(14).HeaderText = "RP"
            .Columns(14).Width = 25
            .Columns(14).Visible = False

            .Columns(15).HeaderText = "EX"
            .Columns(15).Width = 25
            .Columns(15).Visible = False

            .Columns(16).HeaderText = "EN"
            .Columns(16).Width = 25
            .Columns(16).Visible = False

            .Columns(17).HeaderText = "FC"
            .Columns(17).Width = 25
            .Columns(17).Visible = False

            .Columns(18).HeaderText = "Comentarios"
            .Columns(18).Width = 152
            .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            ' End If
        End With


        '**************************************************************
        ''Creamos una instancia de DataGridViewCheckBoxColumn
        'Dim ColCheckBox1 As New DataGridViewCheckBoxColumn
        ''Añadimos la instancia a nuestro DataGridView
        'DgvOrdCompra.Columns.Insert(12, ColCheckBox1)
        ''Seteamos los parámetros de la columna.
        'With ColCheckBox1
        '    'Titulo de la columna.
        '    ''.Columns(0).HeaderText = "Ord. Compra"
        '    .HeaderText = "MA"
        '    'Nombre para acceder al contenido por codigo.
        '    .Name = "MA"
        '    'Ancho
        '    .Width = 25
        '    'Valor a devolver cuando este marcada
        '    .TrueValue = 1
        '    'Valor a devolver cuando este desmarcada
        '    .FalseValue = 0
        'End With

        'Refrescamos la grid para que se vea la columna.
        'DgvOrdCompra.Refresh()
        '**************************************************************


        With Me.DgvRecibos
            .DataSource = DvRecibos

            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 39
            '39,50
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            .RowHeadersWidth = 25
            .AllowUserToAddRows = False

            .Columns(0).HeaderText = "# "
            .Columns(0).Width = 25
            .Columns(0).ReadOnly = True

            .Columns(1).HeaderText = "Fecha y Hora"
            .Columns(1).Width = 120
            .Columns(1).ReadOnly = True

            .Columns(6).HeaderText = "Comentario"
            .Columns(6).Width = 220
            .Columns(6).ReadOnly = True

            .Columns(7).HeaderText = "Observacion"
            .Columns(7).Width = 167
            .Columns(7).ReadOnly = False

            .Columns(2).Visible = False
            .Columns(3).Visible = False
            .Columns(4).Visible = False
            .Columns(5).Visible = False

            .Columns(8).Visible = False

            .Columns(9).HeaderText = "Usuario"
            .Columns(9).Width = 100
            .Columns(9).ReadOnly = True
            .Columns(9).Visible = True

        End With


        DVRecOrdF.Sort = "FchRecibo ASC"
        With Me.DgvRecOrdF
            .DataSource = DVRecOrdF

            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 39
            '39,50
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            .RowHeadersWidth = 25
            .AllowUserToAddRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowHeadersVisible = False
            .Columns(0).HeaderText = "# "
            .Columns(0).Width = 25
            .Columns(0).ReadOnly = True

            .Columns(0).Visible = False

            .Columns(1).HeaderText = "Fecha y Hora"
            .Columns(1).Width = 120
            .Columns(1).ReadOnly = True

            .Columns(2).Visible = False


            .Columns(3).HeaderText = "Id Prov."
            .Columns(3).Width = 40
            .Columns(3).ReadOnly = True

            .Columns(4).HeaderText = "Nombre"
            .Columns(4).Width = 154
            .Columns(4).ReadOnly = True

            .Columns(5).HeaderText = "Ord. Comp."
            .Columns(5).Width = 35
            .Columns(5).ReadOnly = True

            .Columns(6).HeaderText = "Comentario"
            .Columns(6).Width = 170
            .Columns(6).ReadOnly = True

            .Columns(7).Visible = False

            .Columns(8).HeaderText = "#"
            .Columns(8).Width = 20
            .Columns(8).ReadOnly = True

            .Columns(9).HeaderText = "Usuario"
            .Columns(9).Width = 100
            .Columns(9).ReadOnly = True
            .Columns(9).Visible = True

        End With

        If DgvRecOrdF.RowCount > 0 Then
            DgvRecOrdF.CurrentCell = DgvRecOrdF.Item(1, DgvRecOrdF.RowCount - 1)
        End If

        With Me.DgvDetOComp
            .DataSource = DvDetOrdComp

            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 39
            '39,50
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            .RowHeadersWidth = 25
            .AllowUserToAddRows = False

            .Columns(0).HeaderText = "#"
            .Columns(0).Width = 20
            .Columns(0).ReadOnly = True

            .Columns(1).HeaderText = "Artículo"
            .Columns(1).Width = 100
            .Columns(1).ReadOnly = True

            .Columns(2).HeaderText = "Descripción"
            .Columns(2).Width = 367
            .Columns(2).ReadOnly = True

            .Columns(3).HeaderText = "Línea"
            .Columns(3).Width = 130
            .Columns(3).ReadOnly = True

            .Columns(4).HeaderText = "Almacen"
            .Columns(4).Width = 70
            .Columns(4).ReadOnly = True
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(5).HeaderText = "Pzas Sol."
            .Columns(5).Width = 40
            .Columns(5).DefaultCellStyle.Format = "###,###,###"
            .Columns(5).ReadOnly = True
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(6).HeaderText = "Pzas Recibo"
            .Columns(6).Width = 45
            .Columns(6).DefaultCellStyle.Format = "###,###,###"
            .Columns(6).ReadOnly = True
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(7).HeaderText = "Pzas Pendtes "
            .Columns(7).Width = 50
            .Columns(7).DefaultCellStyle.Format = "###,###,###"
            .Columns(7).ReadOnly = True
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(8).HeaderText = "Stock "
            .Columns(8).Width = 40
            .Columns(8).DefaultCellStyle.Format = "###,###,###"
            .Columns(8).ReadOnly = True
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(9).HeaderText = "$ Precio"
            .Columns(9).Width = 70
            .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).ReadOnly = True

            .Columns(10).HeaderText = "$ Total"
            .Columns(10).Width = 70
            .Columns(10).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(10).ReadOnly = True

            .Columns(11).HeaderText = "Estatus"
            .Columns(11).Width = 50
            .Columns(11).ReadOnly = True

            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False
            .Columns(17).Visible = False

            If UsrTPM = "MANAGER" Then
                ''Detalle compra
                .Columns(2).Frozen = True
                .Columns(2).Width = 300
                .Columns(3).Width = 100
            End If


        End With


        With Me.DgvFactEnt
            .DataSource = DvFactEnt

            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 39
            '39,50
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            'Propiedad para no mostrar el cuadro que se encuentra en la parte
            .RowHeadersWidth = 25
            .AllowUserToAddRows = False

            .Columns(0).HeaderText = "Tipo de Documento"
            .Columns(0).Width = 110
            .Columns(0).ReadOnly = True

            .Columns(1).HeaderText = "Doc. SAP"
            .Columns(1).Width = 90
            .Columns(1).ReadOnly = True

            .Columns(2).HeaderText = "Fecha Documento"
            .Columns(2).Width = 68
            .Columns(2).ReadOnly = True

            .Columns(3).HeaderText = "Total Documento"
            .Columns(3).Width = 120
            .Columns(3).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(3).ReadOnly = True

            .Columns(4).HeaderText = "Folio 1"
            .Columns(4).Width = 72
            .Columns(4).ReadOnly = True

            .Columns(5).HeaderText = "Folio 2"
            .Columns(5).Width = 72
            .Columns(5).ReadOnly = True

            .Columns(6).Visible = False

        End With


        With conexion
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConsulta.Click
        LblMensajeCon.Visible = True
        BtnConsulta.Enabled = False
        cargar_registros()
        VerOrdCompFacturadas()
        FiltrarRecibos()
        Me.Refresh()
        BtnConsulta.Enabled = True
        LblMensajeCon.Visible = False
    End Sub

    Sub VerOrdCompFacturadas()
        Dim VFiltro As String = " "

        If ChkVerTodasOrdC.Checked = False Then
            VFiltro = "EstatusDoc = 'Abierto'"
        End If

        If ChkVerTodasOrdC.Checked = False Then
            DvOrdCompra.RowFilter = VFiltro
        Else
            DvOrdCompra.RowFilter = String.Empty
        End If

        TotalOrdCompra()

    End Sub

    Private Sub ChkVerTodasOrdC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkVerTodasOrdC.CheckedChanged
        VerOrdCompFacturadas()
    End Sub
    Sub TotalOrdCompra()
        Dim VTotFProv As Decimal = 0

        For Each row As DataGridViewRow In Me.DgvOrdCompra.Rows
            'If row.Cells("RegSel").Value = False Then
            VTotFProv += row.Cells("TotalDoc").Value
            'End If

        Next

        TxtTotEnPesos.Text = Format(VTotFProv, "##,###,###,###.0")
    End Sub
    Sub FiltrarRecibos()
        Try
            DvRecibos.RowFilter = "OrdCompra = " & DgvOrdCompra.Item(0, DgvOrdCompra.CurrentRow.Index).Value.ToString
        Catch
        End Try
    End Sub

    Private Sub DgvOrdCompra_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DgvOrdCompra.SelectionChanged
        FiltrarRecibos()
        FiltrarDetOrdComp()
        FiltrarFactEnt()
    End Sub
    Sub FiltrarDetOrdComp()
        Try
            DvDetOrdComp.RowFilter = "OrdCompra = '" & DgvOrdCompra.Item(0, DgvOrdCompra.CurrentRow.Index).Value.ToString & "' AND Whsname = '" & DgvOrdCompra.Item(11, DgvOrdCompra.CurrentRow.Index).Value.ToString & "'"
        Catch ex As Exception
        End Try
    End Sub
    Sub FiltrarFactEnt()
        Try
            DvFactEnt.RowFilter = "OrdCompra = " & DgvOrdCompra.Item(0, DgvOrdCompra.CurrentRow.Index).Value.ToString
        Catch
        End Try
    End Sub

    Private Sub BtnMas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMas.Click
        If DgvOrdCompra.RowCount > 0 Then

            Dim Llamaform As New ReciboMatCap()

            Llamaform.TxtOrdCompra.Text = DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("OrdCompra").Value
            Llamaform.TxtIdProv.Text = DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("IdProv").Value
            Llamaform.TxtNomProv.Text = DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("NombreProv").Value
            Llamaform.TxtFchDoc.Text = DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("FchContab").Value
            Llamaform.TxtFchEnt.Text = DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("FchEnt").Value

            Llamaform.TxtIdRecibo.Text = Val(DgvRecibos.RowCount + 1)

            Llamaform.ShowDialog()

        End If

    End Sub
    Sub ActualizaRecibos(ByVal Comentario As String, ByVal MaxRecibo As Integer, ByVal TotRecibo As Integer)

        Dim newRow As DataRowView = DvRecibos.AddNew()
        newRow("Ordena") = TotRecibo

        newRow("FchRecibo") = Date.Now

        newRow("Comentario") = Comentario
        newRow("Recibo") = MaxRecibo
        newRow("OrdCompra") = DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("OrdCompra").Value

        newRow("FchEnt") = DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("FchEnt").Value
      
        newRow("IdProv") = DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("IdProv").Value
        newRow("NombreProv") = DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("NombreProv").Value

        newRow.EndEdit()


    End Sub

    Private Sub BtnOCompra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOCompra.Click
        GridAExcel(DgvOrdCompra)
    End Sub

    Private Sub BtnMenos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMenos.Click

        If DgvRecibos.RowCount > 0 Then

            If DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("EstatusDoc").Value = "Cerrado" Then
                MessageBox.Show("No es posible eliminar recibo, orden de venta cerrada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
           

            If MessageBox.Show("¿Confirma que desea eliminar el recibo de material " +
               DgvRecibos.Rows(DgvRecibos.CurrentRow.Index).Cells("Ordena").Value.ToString + " ?", "Confirmación",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then

                Dim Row As DataRowView
                Dim Contador As Integer = 1
                Dim vRecibo As Integer = DgvRecibos.Rows(DgvRecibos.CurrentRow.Index).Cells("Recibo").Value
                Dim OrdCompra As Integer = DgvRecibos.Rows(DgvRecibos.CurrentRow.Index).Cells("OrdCompra").Value
                For Each Row In DvRecibos
                    Row("Ordena") = Contador

                    If Row("Recibo") = vRecibo Then
                        Row.Delete()
                    Else
                        Contador += 1
                    End If


                Next
                DvRecibos.Table.AcceptChanges()


                '---------------------------------------
                Dim con As New SqlConnection
                Dim cmd As New SqlCommand
                Dim CadenaSQL As String = ""

                Try

                    CadenaSQL = "DELETE FROM RECMAT WHERE Recibo = " + vRecibo.ToString
                    CadenaSQL &= " AND DocNum = " + OrdCompra.ToString

                    con.ConnectionString = StrTpm
                    con.Open()
                    cmd.Connection = con
                    cmd.CommandText = CadenaSQL
                    cmd.ExecuteNonQuery()

                Catch ex As Exception
                    MessageBox.Show("Error Eliminando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Finally

                    con.Close()
                End Try

            End If
        End If

    End Sub

    Sub ActualizaObservaciones()

        Dim vcomentario As String
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim CadenaSQL As String = ""

        If IsDBNull(DgvRecibos.Rows(DgvRecibos.CurrentRow.Index).Cells("Observacion").Value) Then
            vcomentario = ""
        Else
            vcomentario = QuitarCaracteres(DgvRecibos.Rows(DgvRecibos.CurrentRow.Index).Cells("Observacion").Value)
        End If


        Try

            CadenaSQL = "UPDATE RECMAT SET Observacion = '" + vcomentario
            CadenaSQL &= "', UsrObs = '" + UsrTPM
            CadenaSQL &= "', FchObs = GETDATE() "
            CadenaSQL &= "WHERE Recibo = " + DgvRecibos.Rows(DgvRecibos.CurrentRow.Index).Cells("Recibo").Value.ToString
            CadenaSQL &= " AND DocNum = " + DgvRecibos.Rows(DgvRecibos.CurrentRow.Index).Cells("OrdCompra").Value.ToString


            con.ConnectionString = StrTpm
            con.Open()
            cmd.Connection = con
            cmd.CommandText = CadenaSQL
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Error Eliminando Registro" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            con.Close()
        End Try
        'End If
    End Sub
    Sub ActComentarioOrdenCompra()
        'If Not IsDBNull(DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("Comentario").Value) Then
        Dim vcomentario As String
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim CadenaSQL As String = ""

        If IsDBNull(DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("Comentario").Value) Then
            vcomentario = ""
        Else
            vcomentario = QuitarCaracteres(DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("Comentario").Value)
        End If
        'vcomentario = QuitarCaracteres(DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("Comentario").Value)

        Try

            CadenaSQL = "DELETE FROM RECCOC WHERE OrdCompra = "
            CadenaSQL &= DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("OrdCompra").Value.ToString + "  "

            CadenaSQL &= " INSERT INTO RECCOC (OrdCompra,FchCom,Comentario,UsrCom) VALUES ("
            CadenaSQL &= DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("OrdCompra").Value.ToString
            CadenaSQL &= ","
            CadenaSQL &= "GETDATE()"
            CadenaSQL &= ",'"
            CadenaSQL &= vcomentario
            CadenaSQL &= "','"
            CadenaSQL &= UsrTPM
            CadenaSQL &= "')"

            con.ConnectionString = StrTpm
            con.Open()
            cmd.Connection = con
            cmd.CommandText = CadenaSQL
            'cmd.Parameters.Add(New SqlParameter("@fecha", DateTime.Now))
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Error Actualizando Comentario" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            con.Close()
        End Try
        'End If
    End Sub


    Private Sub DgvRecibos_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvRecibos.CellEndEdit
        ActualizaObservaciones()
    End Sub

    Private Sub DgvOrdCompra_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvOrdCompra.CellEndEdit
        ActComentarioOrdenCompra()
    End Sub

    Private Sub DgvDetOComp_RowPrePaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DgvDetOComp.RowPrePaint


        If Not IsDBNull(DgvDetOComp.Rows(e.RowIndex).Cells("Estatus").Value) Then


            If DgvDetOComp.Rows(e.RowIndex).Cells("Estatus").Value = "C" Then

                DgvDetOComp.Rows(e.RowIndex).Cells("LineNum").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Articulo").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Descripcion").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Linea").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Whsname").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Cantidad").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Pendiente").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Existencia").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Recibido").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Precio").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("TotalLinea").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Estatus").Style.BackColor = Color.Green


                DgvDetOComp.Rows(e.RowIndex).Cells("LineNum").Style.ForeColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Articulo").Style.ForeColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Descripcion").Style.ForeColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Linea").Style.ForeColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("WhsName").Style.ForeColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Cantidad").Style.ForeColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Pendiente").Style.ForeColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Existencia").Style.ForeColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Recibido").Style.ForeColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Precio").Style.ForeColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("TotalLinea").Style.ForeColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Estatus").Style.ForeColor = Color.White

            Else

                DgvDetOComp.Rows(e.RowIndex).Cells("LineNum").Style.BackColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Articulo").Style.BackColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Descripcion").Style.BackColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Linea").Style.BackColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("WhsName").Style.BackColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Cantidad").Style.BackColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Pendiente").Style.BackColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Existencia").Style.BackColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Recibido").Style.BackColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Precio").Style.BackColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("TotalLinea").Style.BackColor = Color.White
                DgvDetOComp.Rows(e.RowIndex).Cells("Estatus").Style.BackColor = Color.White

            End If
        End If
    End Sub

    Private Sub DgvOrdCompra_RowPrePaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DgvOrdCompra.RowPrePaint

        If DgvOrdCompra.Rows(e.RowIndex).Cells("whsname").Value = "PUEBLA" Then
            DgvOrdCompra.Rows(e.RowIndex).Cells("whsname").Style.BackColor = Color.LightBlue

        ElseIf DgvOrdCompra.Rows(e.RowIndex).Cells("whsname").Value = "MÉRIDA" Then
            DgvOrdCompra.Rows(e.RowIndex).Cells("whsname").Style.BackColor = Color.LightCoral

        ElseIf DgvOrdCompra.Rows(e.RowIndex).Cells("whsname").Value = "TUXTLA GTZ" Then
            DgvOrdCompra.Rows(e.RowIndex).Cells("whsname").Style.BackColor = Color.LightGreen
        End If

        ''SQLTPD &= "SELECT T0.OrdCompra,T0.FchContab,T0.IdProv, T0.NombreProv, T0.TotalDoc, T0.EstatusDoc,T0.FchEnt, "
        ''SQLTPD &= "T0.FchRec, T0.DiasEnt, T0.DiasTrans, T0.DiasAtraso, T1.Comentario "
        ''SQLTPD &= "FROM #T_OCREC T0 LEFT JOIN TPM.dbo.RECCOC T1 ON T0.OrdCompra = T1.OrdCompra WHERE T0.Ordena = 1; "


        ''Ordenes cerradas con recibo facturadas
        'If DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Value = "Cerrado" And
        '    Not IsDBNull(DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Value) Then

        '    'Not IsDBNull(DgvFactEnt.Rows(DgvFactEnt.CurrentRow.Index).Cells("NumDoc").Value) Then

        '    'DgvRecibos.Rows(DgvRecibos.CurrentRow.Index).Cells("Ordena").Value.ToString()


        '    DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.BackColor = Color.Green

        '    DgvOrdCompra.Rows(e.RowIndex).Cells("MA").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("GR").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("RP").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("EX").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("EN").Style.BackColor = Color.Green
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("FC").Style.BackColor = Color.Green



        '    DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.ForeColor = Color.White

        '    DgvOrdCompra.Rows(e.RowIndex).Cells("MA").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("GR").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("RP").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("EX").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("EN").Style.ForeColor = Color.White
        '    DgvOrdCompra.Rows(e.RowIndex).Cells("FC").Style.ForeColor = Color.White

        'Else
        '    'Ordenes cerradas sin recibo
        '    If DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Value = "Cerrado" And
        '     IsDBNull(DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Value) Then


        '        DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.BackColor = Color.Red

        '        DgvOrdCompra.Rows(e.RowIndex).Cells("MA").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("GR").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("RP").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("EX").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("EN").Style.BackColor = Color.Red
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("FC").Style.BackColor = Color.Red

        '        DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.ForeColor = Color.White

        '        DgvOrdCompra.Rows(e.RowIndex).Cells("MA").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("GR").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("RP").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("EX").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("EN").Style.ForeColor = Color.White
        '        DgvOrdCompra.Rows(e.RowIndex).Cells("FC").Style.ForeColor = Color.White


        '    Else

        '        If DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Value = "Abierto" And
        '           Not IsDBNull(DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Value) Then


        '            DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.BackColor = Color.Yellow

        '            DgvOrdCompra.Rows(e.RowIndex).Cells("MA").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("GR").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("RP").Style.BackColor = Color.Yellmow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("EX").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("EN").Style.BackColor = Color.Yellow
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("FC").Style.BackColor = Color.Yellow



        '        Else
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.BackColor = Color.White


        '            DgvOrdCompra.Rows(e.RowIndex).Cells("MA").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("GR").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("RP").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("EX").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("EN").Style.BackColor = Color.White
        '            DgvOrdCompra.Rows(e.RowIndex).Cells("FC").Style.BackColor = Color.White



        '        End If

        '    End If

        'End If

    End Sub

    Private Sub BtnDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDetalle.Click
        GridAExcel(DgvDetOComp)
    End Sub

    Private Sub BtnRecibos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRecibos.Click
        GridAExcel(DgvRecibos)
    End Sub

    Private Sub BtnFacturas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFacturas.Click
        GridAExcel(DgvFactEnt)
    End Sub

    Private Sub DgvOrdCompra_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DgvOrdCompra.CurrentCellDirtyStateChanged
        ' evento que detecta cuando se cambio o actualizo el valor de una celda
        'Este codigo sirve para que se pueda identificar el proceso del checkbox dentro del datagridview junto
        'con el evento de DgFactProv_CellContentClick

        If DgvOrdCompra.IsCurrentCellDirty Then
            DgvOrdCompra.CommitEdit(DataGridViewDataErrorContexts.Commit)
            ActualizaOcompra()
        End If

    End Sub
    Sub ActualizaOcompra()
       
        Dim Conexion As New SqlConnection(StrTpm)
        'Try
        Conexion.Open()

        Dim Adap As New SqlDataAdapter
        Dim Comando As New SqlCommand

        Dim vSQL As String
        Dim vRecReg As Integer
        vSQL = "SELECT T0.OrdCompra FROM RECENT T0 WHERE T0.OrdCompra = " & DgvOrdCompra.Item(0, DgvOrdCompra.CurrentRow.Index).Value.ToString

        With Comando

            .CommandText = vSQL
            .Connection = Conexion
            vRecReg = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())

        End With

        'DgvOrdCompra.Item(12, DgvOrdCompra.CurrentRow.Index).Value.ToString()
        'IIf(DgFactProv.CurrentRow.Cells("Entregado").Value = False, 0, 1).ToString()

        If vRecReg = 0 Then

            vSQL = "INSERT INTO RECENT (OrdCompra, MA, GR, RP, EX, EN, FC) VALUES ("
            vSQL &= DgvOrdCompra.Item(0, DgvOrdCompra.CurrentRow.Index).Value.ToString
            vSQL &= ", "
            vSQL &= IIf(IsDBNull(DgvOrdCompra.CurrentRow.Cells("MA").Value), 0, 1).ToString()
            vSQL &= ", "
            vSQL &= IIf(IsDBNull(DgvOrdCompra.CurrentRow.Cells("GR").Value), 0, 1).ToString()
            vSQL &= ", "
            vSQL &= IIf(IsDBNull(DgvOrdCompra.CurrentRow.Cells("RP").Value), 0, 1).ToString()
            vSQL &= ", "
            vSQL &= IIf(IsDBNull(DgvOrdCompra.CurrentRow.Cells("EX").Value), 0, 1).ToString()
            vSQL &= ", "
            vSQL &= IIf(IsDBNull(DgvOrdCompra.CurrentRow.Cells("EN").Value), 0, 1).ToString()
            vSQL &= ", "
            vSQL &= IIf(IsDBNull(DgvOrdCompra.CurrentRow.Cells("FC").Value), 0, 1).ToString()
            vSQL &= ") "

        Else

            Dim vMA As Integer

            Dim vGR As Integer
            Dim vRP As Integer
            Dim vEX As Integer
            Dim vEN As Integer
            Dim vFC As Integer

            If IsDBNull(DgvOrdCompra.CurrentRow.Cells("MA").Value) Then
                vMA = 0
            Else
                If DgvOrdCompra.CurrentRow.Cells("MA").Value = True Then
                    vMA = 1
                Else
                    vMA = 0
                End If
            End If

            If IsDBNull(DgvOrdCompra.CurrentRow.Cells("GR").Value) Then
                vGR = 0
            Else
                If DgvOrdCompra.CurrentRow.Cells("GR").Value = True Then
                    vGR = 1
                Else
                    vGR = 0
                End If
            End If

            If IsDBNull(DgvOrdCompra.CurrentRow.Cells("RP").Value) Then
                vRP = 0
            Else
                If DgvOrdCompra.CurrentRow.Cells("RP").Value = True Then
                    vRP = 1
                Else
                    vRP = 0
                End If
            End If

            If IsDBNull(DgvOrdCompra.CurrentRow.Cells("EX").Value) Then
                vEX = 0
            Else
                If DgvOrdCompra.CurrentRow.Cells("EX").Value = True Then
                    vEX = 1
                Else
                    vEX = 0
                End If
            End If

            If IsDBNull(DgvOrdCompra.CurrentRow.Cells("EN").Value) Then
                vEN = 0
            Else
                If DgvOrdCompra.CurrentRow.Cells("EN").Value = True Then
                    vEN = 1
                Else
                    vEN = 0
                End If
            End If

            If IsDBNull(DgvOrdCompra.CurrentRow.Cells("FC").Value) Then
                vFC = 0
            Else
                If DgvOrdCompra.CurrentRow.Cells("FC").Value = True Then
                    vFC = 1
                Else
                    vFC = 0
                End If
            End If


            'vSQL &= IIf(IsDBNull(DgvOrdCompra.CurrentRow.Cells("RP").Value) Or DgvOrdCompra.CurrentRow.Cells("RP").Value = False, "0", "1")

            vSQL = "UPDATE RECENT SET MA = "
            vSQL &= vMA.ToString
            vSQL &= ", GR = "
            vSQL &= vGR.ToString
            vSQL &= ", RP = "
            vSQL &= vRP.ToString
            vSQL &= ", EX = "
            vSQL &= vEX.ToString
            vSQL &= ", EN = "
            vSQL &= vEN.ToString
            vSQL &= ", FC = "
            vSQL &= vFC.ToString
            vSQL &= " WHERE  OrdCompra = "
            vSQL &= DgvOrdCompra.Item(0, DgvOrdCompra.CurrentRow.Index).Value.ToString

        End If

        With Comando
            .CommandText = vSQL
            .ExecuteNonQuery()
        End With
        'Catch ex As Exception
        '    MessageBox.Show("Error Actualizando CheckList" & ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error)

        'Finally

        Conexion.Close()
        'End Try
    End Sub

    Private Sub ComboBox1_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox1.SelectionChangeCommitted
        'MsgBox(ComboBox1.SelectedIndex.ToString)
        'MsgBox(ComboBox1.SelectedItem.ToString)
        'MsgBox(ComboBox1.SelectedText.ToString)
        'MsgBox(ComboBox1.SelectedValue.ToString)
    End Sub
End Class