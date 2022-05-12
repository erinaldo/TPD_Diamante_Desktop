Imports System.Data.SqlClient
Public Class ReciboMatSeg
    Dim DvOrdCompra As New DataView
    Dim DvRecibos As New DataView
    Dim DVRecOrdF As New DataView
    Dim DvDetOrdComp As New DataView
    Dim DvFactEnt As New DataView
    Dim DvDetFactEnt As New DataView

    Private Sub ReciboMat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''MsgBox(IdUsuario)
        Dim FchInicio As Date = DateSerial(Year(Date.Today), Date.Now.Month - 3, 1)

        'Dim FchInicio As DateTime
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
        SQLTPD &= "CASE WHEN T0.DocStatus = 'O' THEN 'Abierto' ELSE 'Cerrado' END AS EstatusDoc, T1.Recibo, T1.UsrCom "
        SQLTPD &= "INTO #T_OCREC "
        SQLTPD &= "FROM OPOR T0 "
        SQLTPD &= "LEFT JOIN TPM.dbo.RECMAT T1 ON T0.DocNum = T1.DocNum "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND T0.CANCELED = 'N' AND T0.CardCode NOT LIKE '%PIM-%' "


        If Me.CmbProv.SelectedValue <> "TODOS" Then
            SQLTPD &= "AND T0.CardCode = @Agente "
        End If

        SQLTPD &= "ORDER BY T0.DocNum  "

        SQLTPD &= "SELECT T0.OrdCompra,T1.WhsCode, T2.WhsName INTO #OrdCompAlmacen "
        SQLTPD &= "FROM #T_OCREC T0  "
        SQLTPD &= "LEFT JOIN POR1 T1 ON T0.DocEntry = T1.DocEntry "
        SQLTPD &= "LEFT JOIN OWHS T2 ON T1.WhsCode = T2.WhsCode "
        SQLTPD &= "GROUP BY T0.OrdCompra,T1.WhsCode, T2.WhsName "


        SQLTPD &= "SELECT T0.OrdCompra,T0.FchContab,T0.IdProv, T0.NombreProv, T0.TotalDoc, T0.EstatusDoc,T0.FchEnt, "
        SQLTPD &= "T0.FchRec, T0.DiasEnt, T0.DiasTrans, T0.DiasAtraso, T1.Comentario "
        SQLTPD &= "FROM #T_OCREC T0 LEFT JOIN TPM.dbo.RECCOC T1 ON T0.OrdCompra = T1.OrdCompra LEFT JOIN #OrdCompAlmacen T2 ON T0.OrdCompra = T2.OrdCompra "
        SQLTPD &= "WHERE T0.Ordena = 1 "

        If (IdUsuario.ToString = "ARAMOS") Then
            SQLTPD &= " AND T2.WhsName LIKE 'PUEBLA%' "
        ElseIf IdUsuario.ToString = "MCHABLE" Then
            SQLTPD &= " AND T2.WhsName = 'MÉRIDA' "
        ElseIf IdUsuario.ToString = "VOTNIEL" Then
            SQLTPD &= " AND T2.WhsName = 'TUXTLA GTZ' "
        End If



        SQLTPD &= "SELECT Ordena, FchRecibo, FchEnt, IdProv, NombreProv, OrdCompra, Comentario, Observacion, Recibo  FROM #T_OCREC T0 WHERE T0.FchRec IS NOT NULL "
        If (IdUsuario.ToString = "ARAMOS") Then
            SQLTPD &= " and UsrCom = 'ARAMOS' "
        ElseIf IdUsuario.ToString = "MCHABLE" Then
            SQLTPD &= " and UsrCom = 'MCHABLE' "
        ElseIf IdUsuario.ToString = "VOTNIEL" Then
            SQLTPD &= " and UsrCom = 'VOTNIEL' "
        End If



        SQLTPD &= "SELECT T1.LINENUM + 1 AS LineNum, T1.ItemCode AS Articulo,T1.Dscription AS Descripcion, T5.ItmsGrpNam AS Linea,"
        SQLTPD &= "T1.Quantity AS Cantidad,T1.Quantity - T1.OpenQty AS Recibido,T1.OpenQty AS Pendiente,T3.OnHand AS Existencia, "
        SQLTPD &= "CASE WHEN T1.Currency <> 'MXP' THEN CASE WHEN T1.LineTotal <= 0 THEN 0 ELSE T1.LineTotal / T1.Quantity END ELSE T1.Price END AS Precio,"
        SQLTPD &= "T1.LineTotal AS TotalLinea, T1.LineStatus AS Estatus, T0.OrdCompra,"
        SQLTPD &= "T1.BASEENTRY, T1.BASETYPE, T1.BASELINE, T1.DOCENTRY, T1.OBJTYPE "
        SQLTPD &= "INTO #T_DETOCREC "
        SQLTPD &= "FROM #T_OCREC T0 "
        SQLTPD &= "LEFT JOIN POR1 T1 ON T0.DocEntry = T1.DocEntry "
        SQLTPD &= "LEFT JOIN OITW T3 ON T1.ItemCode = T3.ItemCode AND T3.WhsCode = 01 "
        SQLTPD &= "LEFT JOIN OITM T4 ON T1.ItemCode = T4.ItemCode "
        SQLTPD &= "LEFT JOIN OITB T5 ON T4.ItmsGrpCod = T5.ItmsGrpCod "
        SQLTPD &= "WHERE T0.Ordena = 1 ORDER BY T1.DocEntry,T1.LineNum  "



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

        SQLTPD &= "SELECT T0.TipoDoc,T0.NumDoc,T0.FchContab, T0.TotalDoc,"
        SQLTPD &= "T0.FolioPref, T0.FolioNum, T0.OrdCompra "
        SQLTPD &= "FROM #T_DTOCFACDET T0 "
        SQLTPD &= "WHERE T0.TipoDoc Is Not NULL "
        SQLTPD &= "GROUP BY T0.OrdCompra,T0.TipoDoc,T0.NumDoc,T0.FchContab, T0.TotalDoc, T0.FolioPref, T0.FolioNum  "

        'SQLTPD &= "SELECT * FROM #T_DTOCFACDET T0 "------------

        SQLTPD &= "DROP TABLE #T_OCREC "
        SQLTPD &= "DROP TABLE #T_DETOCREC "
        SQLTPD &= "DROP TABLE #T_DTOCFACDET "
        SQLTPD &= "DROP TABLE #OrdCompAlmacen "

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
        'DvDetFactEnt.Table = DsRecMat.Tables("DetFactEnt")------------



        'DvOrdCompra = DtOrdCompra.DefaultView

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

            '.Columns(1).HeaderText = "Fecha Documento"
            '.Columns(1).Width = 68
            '.Columns(1).ReadOnly = True

            .Columns(1).Visible = False

            .Columns(2).HeaderText = "Id Prov."
            .Columns(2).Width = 40
            .Columns(2).ReadOnly = True

            .Columns(3).HeaderText = "Nombre"
            .Columns(3).Width = 190
            .Columns(3).ReadOnly = True

            .Columns(4).HeaderText = "$ Total"
            .Columns(4).Width = 70
            .Columns(4).DefaultCellStyle.Format = "###,###,###.0"
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).ReadOnly = True
            .Columns(4).Visible = False

            .Columns(5).HeaderText = "Estatus"
            .Columns(5).Width = 45
            .Columns(5).ReadOnly = True
            .Columns(5).Visible = False

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

            .Columns(11).HeaderText = "Comentario"
            .Columns(11).Width = 240
            .Columns(11).DefaultCellStyle.Format = "###,###,###"
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(11).ReadOnly = True

        End With



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
            .Columns(6).Width = 281
            .Columns(6).ReadOnly = True

            .Columns(7).HeaderText = "Observacion"
            .Columns(7).Width = 280
            .Columns(7).ReadOnly = True

            .Columns(2).Visible = False
            .Columns(3).Visible = False
            .Columns(4).Visible = False
            .Columns(5).Visible = False

            .Columns(8).Visible = False

        End With

        DVRecOrdF.Sort = "FchRecibo ASC"
        'DVRecOrdF.Sort = "FchRecibo DESC"
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
            .Columns(4).Width = 161
            .Columns(4).ReadOnly = True

            .Columns(5).HeaderText = "Ord. Comp."
            .Columns(5).Width = 35
            .Columns(5).ReadOnly = True

            .Columns(6).HeaderText = "Comentario"
            .Columns(6).Width = 178
            .Columns(6).ReadOnly = True

            .Columns(7).Visible = False

            .Columns(8).HeaderText = "#"
            .Columns(8).Width = 20
            .Columns(8).ReadOnly = True

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
            .Columns(2).Width = 340
            .Columns(2).ReadOnly = True

            .Columns(3).HeaderText = "Línea"
            .Columns(3).Width = 120
            .Columns(3).ReadOnly = True

            .Columns(4).HeaderText = "Pzas Sol."
            .Columns(4).Width = 40
            .Columns(4).DefaultCellStyle.Format = "###,###,###"
            .Columns(4).ReadOnly = True
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(5).HeaderText = "Pzas Recibo"
            .Columns(5).Width = 45
            .Columns(5).DefaultCellStyle.Format = "###,###,###"
            .Columns(5).ReadOnly = True
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(6).HeaderText = "Pzas Pendtes "
            .Columns(6).Width = 50
            .Columns(6).DefaultCellStyle.Format = "###,###,###"
            .Columns(6).ReadOnly = True
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(7).HeaderText = "Stock "
            .Columns(7).Width = 40
            .Columns(7).DefaultCellStyle.Format = "###,###,###"
            .Columns(7).ReadOnly = True
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(8).HeaderText = "$ Precio"
            .Columns(8).Width = 70
            .Columns(8).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).ReadOnly = True

            .Columns(9).HeaderText = "$ Total"
            .Columns(9).Width = 70
            .Columns(9).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(9).ReadOnly = True

            .Columns(10).HeaderText = "Estatus"
            .Columns(10).Width = 50
            .Columns(10).ReadOnly = True


            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(13).Visible = False
            .Columns(14).Visible = False
            .Columns(15).Visible = False
            .Columns(16).Visible = False

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
            .Columns(4).Width = 80
            .Columns(4).ReadOnly = True

            .Columns(5).HeaderText = "Folio 2"
            .Columns(5).Width = 80
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
            DvDetOrdComp.RowFilter = "OrdCompra = " & DgvOrdCompra.Item(0, DgvOrdCompra.CurrentRow.Index).Value.ToString
        Catch
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
            If DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("EstatusDoc").Value = "Cerrado" Then
                MessageBox.Show("No es posible agegar recibo, orden de venta cerrada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

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

        If Not IsDBNull(DgvRecibos.Rows(DgvRecibos.CurrentRow.Index).Cells("Observacion").Value) Then


            Dim vcomentario As String
            Dim con As New SqlConnection
            Dim cmd As New SqlCommand
            Dim CadenaSQL As String = ""


            vcomentario = QuitarCaracteres(DgvRecibos.Rows(DgvRecibos.CurrentRow.Index).Cells("Observacion").Value)

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
        End If
    End Sub
    Sub ActComentarioOrdenCompra()
        If Not IsDBNull(DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("Comentario").Value) Then
            Dim vcomentario As String
            Dim con As New SqlConnection
            Dim cmd As New SqlCommand
            Dim CadenaSQL As String = ""


            vcomentario = QuitarCaracteres(DgvOrdCompra.Rows(DgvOrdCompra.CurrentRow.Index).Cells("Comentario").Value)

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
        End If
    End Sub


    Private Sub DgvRecibos_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvRecibos.CellEndEdit
        ActualizaObservaciones()
    End Sub

    Private Sub DgvOrdCompra_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvOrdCompra.CellEndEdit
        ActComentarioOrdenCompra()
    End Sub

    Private Sub DgvDetOComp_RowPrePaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DgvDetOComp.RowPrePaint
        If Not IsDBNull(DgvDetOComp.Rows(e.RowIndex).Cells("Estatus").Value) Then



            'SQLTPD &= "SELECT T1.LINENUM + 1 AS LineNum, T1.ItemCode AS Articulo,T1.Dscription AS Descripcion, T5.ItmsGrpNam AS Linea,"
            'SQLTPD &= "T1.Quantity AS Cantidad,T1.OpenQty AS Pendiente,T3.OnHand AS Existencia,"
            'SQLTPD &= "CASE WHEN T1.Currency <> 'MXP' THEN T1.LineTotal / T1.Quantity ELSE T1.Price END AS Precio,"
            'SQLTPD &= "T1.LineTotal AS TotalLinea, T1.LineStatus AS Estatus, T0.OrdCompra,"
            'SQLTPD &= "T1.BASEENTRY, T1.BASETYPE, T1.BASELINE, T1.DOCENTRY, T1.OBJTYPE "
            'SQLTPD &= "INTO #T_DETOCREC "
            'SQLTPD &= "FROM #T_OCREC T0 "
            'SQLTPD &= "INNER JOIN POR1 T1 ON T0.DocEntry = T1.DocEntry "
            'SQLTPD &= "INNER JOIN OITW T3 ON T1.ItemCode = T3.ItemCode AND T3.WhsCode = 01 "
            'SQLTPD &= "INNER JOIN OITM T4 ON T1.ItemCode = T4.ItemCode "
            'SQLTPD &= "INNER JOIN OITB T5 ON T4.ItmsGrpCod = T5.ItmsGrpCod "
            'SQLTPD &= "WHERE T0.Ordena = 1 ORDER BY T1.DocEntry,T1.LineNum  "



            If DgvDetOComp.Rows(e.RowIndex).Cells("Estatus").Value = "C" Then

                DgvDetOComp.Rows(e.RowIndex).Cells("LineNum").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Articulo").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Descripcion").Style.BackColor = Color.Green
                DgvDetOComp.Rows(e.RowIndex).Cells("Linea").Style.BackColor = Color.Green
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

        'SQLTPD &= "SELECT T0.OrdCompra,T0.FchContab,T0.IdProv, T0.NombreProv, T0.TotalDoc, T0.EstatusDoc,T0.FchEnt, "
        'SQLTPD &= "T0.FchRec, T0.DiasEnt, T0.DiasTrans, T0.DiasAtraso, T1.Comentario "
        'SQLTPD &= "FROM #T_OCREC T0 LEFT JOIN TPM.dbo.RECCOC T1 ON T0.OrdCompra = T1.OrdCompra WHERE T0.Ordena = 1; "


        'Ordenes cerradas con recibo facturadas
        If DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Value = "Cerrado" And
            Not IsDBNull(DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Value) Then

            'Not IsDBNull(DgvFactEnt.Rows(DgvFactEnt.CurrentRow.Index).Cells("NumDoc").Value) Then

            'DgvRecibos.Rows(DgvRecibos.CurrentRow.Index).Cells("Ordena").Value.ToString()


            DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.Green
            DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.BackColor = Color.Green
            DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.BackColor = Color.Green
            DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.BackColor = Color.Green
            DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.BackColor = Color.Green
            DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.BackColor = Color.Green
            DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.BackColor = Color.Green
            DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.BackColor = Color.Green
            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.BackColor = Color.Green
            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.BackColor = Color.Green
            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = Color.Green
            DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.BackColor = Color.Green

            DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.ForeColor = Color.White
            DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.ForeColor = Color.White
            DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.ForeColor = Color.White
            DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.ForeColor = Color.White
            DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.ForeColor = Color.White
            DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.ForeColor = Color.White
            DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.ForeColor = Color.White
            DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.ForeColor = Color.White
            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.ForeColor = Color.White
            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.ForeColor = Color.White
            DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.ForeColor = Color.White
            DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.ForeColor = Color.White

        Else
            'Ordenes cerradas sin recibo
            If DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Value = "Cerrado" And
             IsDBNull(DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Value) Then


                DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.Red
                DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.BackColor = Color.Red
                DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.BackColor = Color.Red
                DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.BackColor = Color.Red
                DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.BackColor = Color.Red
                DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.BackColor = Color.Red
                DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.BackColor = Color.Red
                DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.BackColor = Color.Red
                DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.BackColor = Color.Red
                DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.BackColor = Color.Red
                DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = Color.Red
                DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.BackColor = Color.Red

                DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.ForeColor = Color.White
                DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.ForeColor = Color.White
                DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.ForeColor = Color.White
                DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.ForeColor = Color.White
                DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.ForeColor = Color.White
                DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.ForeColor = Color.White
                DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.ForeColor = Color.White
                DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.ForeColor = Color.White
                DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.ForeColor = Color.White
                DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.ForeColor = Color.White
                DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.ForeColor = Color.White
                DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.ForeColor = Color.White
            Else

                If DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Value = "Abierto" And
                   Not IsDBNull(DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Value) Then


                    DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.Yellow
                    DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.BackColor = Color.Yellow
                    DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.BackColor = Color.Yellow
                    DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.BackColor = Color.Yellow
                    DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.BackColor = Color.Yellow
                    DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.BackColor = Color.Yellow
                    DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.BackColor = Color.Yellow
                    DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.BackColor = Color.Yellow
                    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.BackColor = Color.Yellow
                    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.BackColor = Color.Yellow
                    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = Color.Yellow
                    DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.BackColor = Color.Yellow
                Else
                    DgvOrdCompra.Rows(e.RowIndex).Cells("OrdCompra").Style.BackColor = Color.White
                    DgvOrdCompra.Rows(e.RowIndex).Cells("FchContab").Style.BackColor = Color.White
                    DgvOrdCompra.Rows(e.RowIndex).Cells("IdProv").Style.BackColor = Color.White
                    DgvOrdCompra.Rows(e.RowIndex).Cells("NombreProv").Style.BackColor = Color.White
                    DgvOrdCompra.Rows(e.RowIndex).Cells("TotalDoc").Style.BackColor = Color.White
                    DgvOrdCompra.Rows(e.RowIndex).Cells("EstatusDoc").Style.BackColor = Color.White
                    DgvOrdCompra.Rows(e.RowIndex).Cells("FchEnt").Style.BackColor = Color.White
                    DgvOrdCompra.Rows(e.RowIndex).Cells("FchRec").Style.BackColor = Color.White
                    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasEnt").Style.BackColor = Color.White
                    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasTrans").Style.BackColor = Color.White
                    DgvOrdCompra.Rows(e.RowIndex).Cells("DiasAtraso").Style.BackColor = Color.White
                    DgvOrdCompra.Rows(e.RowIndex).Cells("Comentario").Style.BackColor = Color.White

                End If

            End If

        End If

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
End Class