Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel

Public Class DevolucionMat
    Dim DvArticulo As New DataView
    Dim DvClte As New DataView
    Private Sub DevolucionMat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim FchInicio As DateTime
        FchInicio = DateAdd(DateInterval.Month, -1, Date.Now)
        Me.DtpFechaIni.Value = Format(FchInicio, "dd/MM/yyyy")
        Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")


        Dim ConsutaLista As String


        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

            ConsutaLista = "SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ORDER BY ItmsGrpNam"
            Dim daGArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            Dim DSetTablas As New DataSet
            daGArticulo.Fill(DSetTablas, "GArticulos")

            Dim fila As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            fila = DSetTablas.Tables("GArticulos").NewRow

            'Agregamos los valores a los campos de la tabla
            fila("ItmsGrpNam") = "TODOS"
            fila("ItmsGrpCod") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("GArticulos").Rows.Add(fila)

            Me.CmbGrupoArticulo.DataSource = DSetTablas.Tables("GArticulos")
            Me.CmbGrupoArticulo.DisplayMember = "ItmsGrpNam"
            Me.CmbGrupoArticulo.ValueMember = "ItmsGrpCod"
            Me.CmbGrupoArticulo.SelectedValue = 999

            'SE AGREGA ESTA SENTENCIA PARA QUE A RICARDO ROBLES LE APAREZCAN LOS DATOS DE MARCO LOPEZ
            'URIEL MODIFICACION: 25/09/2018
            If vCodAgte = "17" Then
                ConsutaLista = "SELECT OSLP.slpcode,OSLP.slpname FROM OSLP where SlpCode = 17 or SlpCode = 20  ORDER BY slpname "
            Else
                ConsutaLista = "SELECT OSLP.slpcode,OSLP.slpname FROM OSLP ORDER BY slpname"
            End If

            Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            daAgte.Fill(DSetTablas, "Agentes")

            Dim filaAgte As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaAgte = DSetTablas.Tables("Agentes").NewRow

            'SE AGREGA ESTA SENTENCIA PARA QUE A RICARDO ROBLES LE APAREZCAN LOS DATOS DE MARCO LOPEZ
            'URIEL MODIFICACION: 25/09/2018
            If vCodAgte <> "17" Then
                'Agregamos los valores a los campos de la tabla
                filaAgte("slpname") = "TODOS"
                filaAgte("slpcode") = 999
            End If


            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

            Me.CmbAgteVta.DataSource = DSetTablas.Tables("Agentes")
            Me.CmbAgteVta.DisplayMember = "slpname"
            Me.CmbAgteVta.ValueMember = "slpcode"
            Me.CmbAgteVta.SelectedValue = 999





            ConsutaLista = "SELECT CardCode,CardName, SlpCode FROM OCRD WHERE CardType = 'C' ORDER BY SlpCode, CardName"
            Dim daClientes As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            daClientes.Fill(DSetTablas, "Clientes")

            Dim filaClientes As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaClientes = DSetTablas.Tables("Clientes").NewRow

            'Agregamos los valores a los campos de la tabla
            filaClientes("CardName") = "TODOS"
            filaClientes("CardCode") = "TODOS"
            filaClientes("slpcode") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Clientes").Rows.Add(filaClientes)

            DvClte.Table = DSetTablas.Tables("Clientes")

            Me.CmbCliente.DataSource = DvClte
            Me.CmbCliente.DisplayMember = "CardName"
            Me.CmbCliente.ValueMember = "CardCode"
            Me.CmbCliente.SelectedValue = "TODOS"


            '-----------------------------------------------------
            ConsutaLista = "SELECT ItemCode,ItemName,ItmsGrpCod FROM OITM ORDER BY ItemCode"
            Dim daArticulo As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


            daArticulo.Fill(DSetTablas, "Articulos")

            Dim filaArticulo As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaArticulo = DSetTablas.Tables("Articulos").NewRow

            'Agregamos los valores a los campos de la tabla
            filaArticulo("ItemName") = "TODOS"
            filaArticulo("ItemCode") = "TODOS"
            filaArticulo("ItmsGrpCod") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Articulos").Rows.Add(filaArticulo)

            DvArticulo.Table = DSetTablas.Tables("Articulos")

            Me.CmbArticulo.DataSource = DvArticulo
            Me.CmbArticulo.DisplayMember = "ItemCode"
            Me.CmbArticulo.ValueMember = "ItemCode"
            Me.CmbArticulo.SelectedValue = "TODOS"

        End Using
        CmbMotDev.SelectedText = "TODOS"
        CmbMotDev.SelectedValue = "TODOS"

        CmbMotDev.Text = "TODOS"
        CmbMotDev.SelectedValue = "TODOS"
        CmbMotDev.DisplayMember = "TODOS"
        CmbMotDev.ValueMember = "TODOS"


        If VEsAgente = 1 Then
            'SE AGREGA ESTA COMPARACIÓN PARA QUE A RICARDO ROBLES LE APAREZCAN LOS DATOS DE MARCO LOPEZ
            'URIEL MODIFICACION: 25/09/2018
            If vCodAgte <> 17 Then
                Me.CmbAgteVta.SelectedValue = vCodAgte
                Me.CmbAgteVta.Enabled = False
                'BuscaClientes()
                Me.CmbCliente.Focus()
            Else
                Me.CmbAgteVta.SelectedValue = vCodAgte
            End If
        End If

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If Me.DtpFechaIni.Value > Me.DtpFechaTer.Value Then
            MessageBox.Show("La fecha de inicio no pede ser mayor a la de termino", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DtpFechaIni.Focus()
            Return
        End If


        If IsNothing(CmbArticulo.SelectedValue) Then
            MessageBox.Show("Seleccione un articulo",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbArticulo.Focus()
            Return
        End If

        If IsNothing(CmbGrupoArticulo.SelectedValue) Then
            MessageBox.Show("Seleccione una línea",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbGrupoArticulo.Focus()
            Return
        End If

        If IsNothing(CmbAgteVta.SelectedValue) Then
            MessageBox.Show("Seleccione un agente de ventas",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbAgteVta.Focus()
            Return
        End If


        If Not (CmbMotDev.Text = "TODOS" Or CmbMotDev.Text = "DEFECTUOSO" Or CmbMotDev.Text = "FALTANTE DE MATERIAL" Or
           CmbMotDev.Text = "MAL SURTIDO" Or CmbMotDev.Text = "NO SOLICITADO") Then

            MessageBox.Show("Seleccione un motivo de devolución",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbMotDev.Focus()
            Return
        End If


        If IsNothing(CmbCliente.SelectedValue) Then
            MessageBox.Show("Seleccione un cliente",
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbCliente.Focus()
            Return
        End If


        Ejecutar_Consulta()
    End Sub
    Private Sub Ejecutar_Consulta()
        Dim Consulta As String = " "
        Dim strcadena As String = ""
        Dim CTabla As String = ""
        Dim DTMObra As New DataTable
        Dim DTProb As New DataTable

        Dim fi As String
        Dim ff As String

        fi = DtpFechaIni.Value.ToString("yyyy-MM-dd")
        ff = DtpFechaTer.Value.ToString("yyyy-MM-dd")



        If (DtpFechaIni.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
            If (DtpFechaTer.Value.ToString("yyyy-MM-dd") <= "2017-12-31") Then
                Consulta &= "select t0.DocNum, t0.DocEntry, t0.DocDate, t0.U_SYS_MOTDEV, t0.CardCode, t0.CardName, t0.U_Factura, t0.SlpCode "
                Consulta &= "into #tmp1 "
                Consulta &= "from ORIN t0  "
                Consulta &= "where t0.DocDate >= '" + fi + "' and t0.DocDate <= '" + ff + "' and t0.DocType = 'I' and "
                Consulta &= "CASE when t0.DocDate <= '2017-11-19' then t0.EDocNum else "
                Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end is not null "

            ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
                Consulta &= "select distinct t0.DocNum "
                Consulta &= "into #RR "
                Consulta &= "from ORIN t0 INNER JOIN RIN1 t1 ON t0.DocEntry = t1.DocEntry "
                Consulta &= "where t0.DocDate >= '" + fi + "' and t0.DocDate <= '" + ff + "'  "
                Consulta &= "and t1.ItemCode <> 'DESCUENTO P.P' and "
                Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14)  is not null "

                Consulta &= "select t0.DocNum, t0.DocEntry, t0.DocDate, t0.U_SYS_MOTDEV, t0.CardCode, t0.CardName, t0.U_Factura, t0.SlpCode "
                Consulta &= "into #tmp1 "
                Consulta &= "from ORIN t0  "
                Consulta &= "where t0.DocDate >= '" + fi + "' and t0.DocDate <= '" + ff + "' and t0.DocType = 'I' and "
                Consulta &= "CASE when t0.DocDate <= '2017-11-19' then t0.EDocNum else "
                Consulta &= "(select ReportID from ECM2 t1 where t1.SrcObjAbs = t0.DocEntry and t1.SrcObjType = 14) end is not null "
                Consulta &= "union all "
                Consulta &= "select t0.DocNum, t0.DocEntry, t0.DocDate, t0.U_SYS_MOTDEV, t0.CardCode, t0.CardName, t0.U_Factura, t0.SlpCode "
                Consulta &= "from #RR t1 inner join ORIN t0 on t1.DocNum = t0.DocNum "
                Consulta &= "drop table #RR "

            End If
        ElseIf (DtpFechaIni.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
            If (DtpFechaTer.Value.ToString("yyyy-MM-dd") < "2018-01-01") Then

            ElseIf (DtpFechaTer.Value.ToString("yyyy-MM-dd") >= "2018-01-01") Then
                Consulta &= "select distinct t0.DocNum "
                Consulta &= "into #RR2 "
                Consulta &= "from ORIN t0 INNER JOIN RIN1 t1 ON t0.DocEntry = t1.DocEntry "
                Consulta &= "INNER JOIN OITM T2 on T1.ItemCode = T2.ItemCode  "
                Consulta &= "LEFT JOIN ECM2 T3 on T0.DocEntry = T3.SrcObjAbs AND T3.SrcObjType = 14 "
                Consulta &= "where T0.DocDate between '" + fi + "' and '" + ff + "' AND T0.DocType <> 'S' "
                Consulta &= "AND (T2.ItmsGrpCod <> 200 OR T0.Series = 59) "
                Consulta &= "AND (T3.ReportID IS NOT NULL OR T0.U_BXP_UUID IS NOT NULL) "
        Consulta &= "AND ((T0.U_BXP_TIMBRADO = 'T' OR T0.U_BXP_TIMBRADO = 'P') OR T0.EDocGenTyp = 'G') "
        Consulta &= "AND t0.DocNum NOT IN (SELECT DocNum FROM TPM.dbo.[00ExcepDevolucion]) "

                Consulta &= "select t0.DocNum, t0.DocEntry, t0.DocDate, t0.U_SYS_MOTDEV, t0.CardCode, t0.CardName, t0.U_Factura, t0.SlpCode "
                Consulta &= "into #tmp1 "
                Consulta &= "from #RR2 t1 inner join ORIN t0 on t1.DocNum = t0.DocNum "
                Consulta &= "drop table #RR2 "
            End If
        End If


        Consulta &= "SELECT "
        Consulta &= "T0.DocDate AS Fecha, T1.ItemCode AS Articulo, T1.Dscription AS Descripcion, T4.ItmsGrpNam as Linea,"
        Consulta &= " T1.Quantity AS Cantidad, T1.Price AS Precio, T1.LineTotal AS Total,"
        Consulta &= "CASE WHEN T0.U_SYS_MOTDEV = 'D' THEN 'DEFECTUOSO' "
        Consulta &= "WHEN T0.U_SYS_MOTDEV = 'F' THEN 'FALTANTE DE MATERIAL' "
        Consulta &= "WHEN T0.U_SYS_MOTDEV = 'M' THEN 'MAL SURTIDO' "
        Consulta &= "WHEN T0.U_SYS_MOTDEV = 'N' THEN 'NO SOLICITADO' "
        Consulta &= "ELSE 'SIN ASIGNACION' END AS MotivoDev,T5.Coment AS Observacion,T5.FolioDev,T0.CardCode AS IdCliente,"
        Consulta &= "T0.CardName AS Cliente,T2.SlpName AS Agente,T0.DocNum AS DocSAP,T0.U_Factura AS Factura,T1.LineNum "

        Consulta &= "FROM #tmp1 T0 INNER JOIN RIN1 T1 ON T0.DocEntry = T1.DocEntry "

        Consulta &= "INNER JOIN OSLP T2 ON T0.SlpCode = T2.SlpCode "
        Consulta &= "INNER JOIN OITM T3 ON T1.ItemCode = T3.ItemCode "
        Consulta &= "INNER JOIN OITB T4 ON T3.ItmsGrpCod = T4.ItmsGrpCod "
        Consulta &= "LEFT JOIN TPM.dbo.COMDEV T5 ON T5.DocSAP = T0.DocNum AND T5.LineNum = T1.LineNum "

        Consulta &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer  "


        If CmbArticulo.SelectedValue <> "TODOS" Then
            Consulta &= " AND T1.ItemCode = @IdArticulo"
        End If

        If CmbGrupoArticulo.SelectedValue <> 999 Then
            Consulta &= " AND T3.ItmsGrpCod = @GrupoArt"
        End If
        'SE AGREGA ESTA SENTENCIA PARA QUE A RICARDO ROBLES LE APAREZCAN LOS DATOS DE MARCO LOPEZ
        'URIEL MODIFICACION: 25/09/2018
        If vCodAgte <> "17" Then
            If CmbAgteVta.SelectedValue <> 999 Then
                Consulta &= " AND T0.SlpCode = @IdAgente"
            End If
        Else
            Consulta &= " AND T0.SlpCode = @IdAgente"
        End If



        If CmbMotDev.Text <> "TODOS" Then

            If CmbMotDev.Text = "DEFECTUOSO" Then
                Consulta &= " AND T0.U_SYS_MOTDEV  = 'D'"
            End If

            If CmbMotDev.Text = "FALTANTE DE MATERIAL" Then
                Consulta &= " AND T0.U_SYS_MOTDEV  = 'F'"
            End If

            If CmbMotDev.Text = "MAL SURTIDO" Then
                Consulta &= " AND T0.U_SYS_MOTDEV  = 'M'"
            End If

            If CmbMotDev.Text = "NO SOLICITADO" Then
                Consulta &= " AND T0.U_SYS_MOTDEV  = 'N'"
            End If

        End If



        If CmbCliente.SelectedValue <> "TODOS" Then
            Consulta &= " AND T0.CardCode = @IdCliente"
        End If

        Consulta &= " drop table #tmp1 "


        Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

        CmdMObra.Parameters.Add("@FechaIni", SqlDbType.SmallDateTime)
        CmdMObra.Parameters("@FechaIni").Value = Me.DtpFechaIni.Value
        CmdMObra.Parameters.Add("@FechaTer", SqlDbType.SmallDateTime)
        CmdMObra.Parameters("@FechaTer").Value = Me.DtpFechaTer.Value

        'SE AGREGA ESTA SENTENCIA PARA QUE A RICARDO ROBLES LE APAREZCAN LOS DATOS DE MARCO LOPEZ
        'URIEL MODIFICACION: 25/09/2018
        If vCodAgte <> "17" Then
            If CmbAgteVta.SelectedValue <> 999 Then
                CmdMObra.Parameters.Add("@IdAgente", SqlDbType.Int)
                CmdMObra.Parameters("@IdAgente").Value = CmbAgteVta.SelectedValue
            End If
        Else
            CmdMObra.Parameters.Add("@IdAgente", SqlDbType.Int)
            CmdMObra.Parameters("@IdAgente").Value = CmbAgteVta.SelectedValue
        End If


        If CmbCliente.SelectedValue <> "TODOS" Then
            CmdMObra.Parameters.Add("@IdCliente", SqlDbType.Char)
            CmdMObra.Parameters("@IdCliente").Value = CmbCliente.SelectedValue
        End If

        If CmbGrupoArticulo.SelectedValue <> 999 Then
            CmdMObra.Parameters.Add("@GrupoArt", SqlDbType.Int)
            CmdMObra.Parameters("@GrupoArt").Value = CmbGrupoArticulo.SelectedValue
        End If


        If CmbArticulo.SelectedValue <> "TODOS" Then
            CmdMObra.Parameters.Add("@IdArticulo", SqlDbType.Char)
            CmdMObra.Parameters("@IdArticulo").Value = CmbArticulo.SelectedValue
        End If

        CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
        CmdMObra.Connection.Open()

        Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)
        AdapMObra.Fill(DTMObra)
        CmdMObra.Connection.Close()

        With Me.GrdDevMat
            .DataSource = DTMObra

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

            .Columns(0).HeaderText = "Fecha"
            .Columns(0).Width = 68
            .Columns(0).ReadOnly = True

            .Columns(1).HeaderText = "Artículo"
            .Columns(1).Width = 80
            .Columns(1).ReadOnly = True

            .Columns(2).HeaderText = "Descripción"
            .Columns(2).Width = 210
            .Columns(2).ReadOnly = True

            .Columns(3).HeaderText = "Línea"
            .Columns(3).Width = 80
            .Columns(3).ReadOnly = True

            .Columns(4).HeaderText = "Cant. Piezas"
            .Columns(4).Width = 40
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).DefaultCellStyle.Format = "###,###,###"
            .Columns(4).ReadOnly = True

            .Columns(5).HeaderText = "$ Precio"
            .Columns(5).Width = 60
            .Columns(5).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).ReadOnly = True

            .Columns(6).HeaderText = "$ Monto Total"
            .Columns(6).Width = 60
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Format = "###,###,###.00"
            .Columns(6).ReadOnly = True

            .Columns(7).HeaderText = "Motivo de la Devolución"
            .Columns(7).Width = 100
            .Columns(7).ReadOnly = True

            .Columns(8).HeaderText = "Observación"
            .Columns(8).Width = 310


            .Columns(9).HeaderText = "Folio Dev."
            .Columns(9).DefaultCellStyle.Format = "###,###,###"
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(9).Width = 50

            If UsrTPM <> "ALMACEN1" Then
                .Columns(8).ReadOnly = True
                .Columns(9).ReadOnly = True
            End If


            .Columns(10).HeaderText = "Clave Cliente"
            .Columns(10).Width = 45
            .Columns(10).ReadOnly = True

            .Columns(11).HeaderText = "Nombre Cliente"
            .Columns(11).Width = 157
            .Columns(11).ReadOnly = True

            .Columns(12).HeaderText = "Agente"
            .Columns(12).Width = 135
            .Columns(12).ReadOnly = True

            .Columns(13).HeaderText = "Nota de Crédito"
            .Columns(13).Width = 70
            .Columns(13).ReadOnly = True

            .Columns(14).HeaderText = "Factura"
            .Columns(14).Width = 80
            .Columns(14).ReadOnly = True

            .Columns(15).Visible = False


            Dim vTxtCantPiezas As Decimal = 0
            Dim vMontTot As Decimal = 0

            For Each row As DataGridViewRow In GrdDevMat.Rows

                vTxtCantPiezas += row.Cells("Cantidad").Value

                vMontTot += row.Cells("Total").Value

            Next


            TxtCantPiezas.Text = Format(vTxtCantPiezas, "##,###,###,###")
            TxtMontTot.Text = Format(vMontTot, "##,###,###,###.0")


        End With
        ReemplazarColumna()
    End Sub

    Private Sub GrdDevMat_CellBeginEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles GrdDevMat.CellBeginEdit
        'If UsrTPM <> "Ventas2" And UsrTPM <> "MANAGER" Then
        '    MessageBox.Show("No es posible capturar comentarios, usuario diferente al registrado", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    e.Cancel = True
        '    Return
        'End If
    End Sub

    Sub ActualizaValores()
        ' crear nueva conexión    
        Dim conexion As New SqlConnection(StrTpm)

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand

        Dim SQLTPD As String

        SQLTPD = "SELECT count(*) AS Num FROM COMDEV WHERE DocSAP = " + GrdDevMat.CurrentRow.Cells("DocSAP").Value.ToString
        SQLTPD &= "AND  LineNum = " + GrdDevMat.CurrentRow.Cells("LineNum").Value.ToString
        Dim DsVtasDet As New DataSet

        Dim vRegFact As Integer = 0
        With comando
            .CommandText = SQLTPD
            .Connection = conexion
            .Connection.Open()
            vRegFact = IIf(IsDBNull(.ExecuteScalar), 0, .ExecuteScalar)
            .Connection.Close()
        End With



        '' obtener indice de la columna 
        Dim columna As Integer = GrdDevMat.CurrentCell.ColumnIndex


        'se obtiene el nombre de la columna
        Dim NombreCol As String = GrdDevMat.Columns.Item(columna).Name




        If vRegFact >= 1 Then

            SQLTPD = "UPDATE COMDEV SET Fecha = @Fchact "
            SQLTPD &= ", "

            If NombreCol = "FolioDev" Then

                SQLTPD &= "FolioDev = '" + QuitarCaracteres(GrdDevMat.CurrentRow.Cells("FolioDev").Value.ToString)
            Else

                SQLTPD &= "Coment = '" + QuitarCaracteres(GrdDevMat.CurrentRow.Cells("Observacion").Value.ToString)
            End If

            SQLTPD &= "' "
            SQLTPD &= "WHERE DocSAP = " + GrdDevMat.CurrentRow.Cells("DocSAP").Value.ToString
            SQLTPD &= " AND  LineNum = " + GrdDevMat.CurrentRow.Cells("LineNum").Value.ToString

        Else

            SQLTPD = "INSERT INTO COMDEV (DocSAP, LineNum, Articulo, Coment, FolioDev, Fecha) "
            SQLTPD &= "VALUES("
            SQLTPD &= GrdDevMat.CurrentRow.Cells("DocSAP").Value.ToString
            SQLTPD &= ", "
            SQLTPD &= GrdDevMat.CurrentRow.Cells("LineNum").Value.ToString
            SQLTPD &= ", '"
            SQLTPD &= GrdDevMat.CurrentRow.Cells("Articulo").Value.ToString
            SQLTPD &= "','"
            SQLTPD &= QuitarCaracteres(GrdDevMat.CurrentRow.Cells("Observacion").Value.ToString)
            SQLTPD &= "', "
            SQLTPD &= IIf(QuitarCaracteres(GrdDevMat.CurrentRow.Cells("FolioDev").Value.ToString) = Nothing, 0, QuitarCaracteres(GrdDevMat.CurrentRow.Cells("FolioDev").Value.ToString))
            SQLTPD &= ", "
            SQLTPD &= "@Fchact" 'DgvEncOVtas.CurrentRow.Cells("Fchfact").Value
            SQLTPD &= ")"

        End If

        Dim CmdWom As Data.SqlClient.SqlCommand

        CmdWom = New Data.SqlClient.SqlCommand()
        With CmdWom

            .Parameters.AddWithValue("@Fchact", DateTime.Now)
            .Connection = New Data.SqlClient.SqlConnection(StrTpm)
            .Connection.Open()
            .CommandText = SQLTPD
            .ExecuteNonQuery()
            .Connection.Close()
        End With

    End Sub

    Private Sub GrdDevMat_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdDevMat.CellEndEdit
        ActualizaValores()
    End Sub
    Private Sub ReemplazarColumna()
        Dim instance As DataGridViewTextBoxColumn
        instance = GrdDevMat.Columns(8)
        instance.MaxInputLength = 250

        instance = GrdDevMat.Columns(9)
        instance.MaxInputLength = 7

    End Sub

    Private Sub BtnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExcel.Click
        'GridAExcel(GrdDevMat)
        ExportarNuevoDevolucion()
    End Sub

    Sub ExportarNuevoDevolucion()
        'Dim dv As DataView = DirectCast(GrdDevMat.DataSource, DataView)
        'Dim ds As DataSet = DgVtaAgte.DataSource
        Dim dt As DataTable = GrdDevMat.DataSource

        Dim wb = New XLWorkbook()
        Dim ws = wb.Worksheets.Add("Devolución de Materiales")


        Dim range = ws.Range(1, 1, dt.Rows.Count + 1, dt.Columns.Count).Merge().AddToNamed("Devolucion")
        Dim rangeWithData = ws.Cell(2, 1).InsertData(dt.AsEnumerable)

        'ws.Columns("N").Delete()

        Dim tab = range.CreateTable()
        tab.Theme = XLTableTheme.TableStyleLight8

        'DAR FOMATO A LAS CELDAS
        Dim index As Integer = 1

        For i As Integer = 0 To dt.Rows.Count

            Try
                'Encabezados dependiendo
                If index = 1 Then
                    'Dim cellA3 As String = String.Format("A{0}", 1)
                    'wb.Worksheet(1).Cells(cellA3).Value = "Nombre del encabezado "
                    'wb.Worksheet(1).Cells(cellA3).Style.Font.Bold = True

                    Dim cellA0 As String = String.Format("A{0}", index)
                    wb.Worksheet(1).Cells(cellA0).Value = "Fecha"

                    Dim cellB0 As String = String.Format("B{0}", index)
                    wb.Worksheet(1).Cells(cellB0).Value = "Articulo"

                    Dim cellC0 As String = String.Format("C{0}", index)
                    wb.Worksheet(1).Cells(cellC0).Value = "Descripcion"

                    Dim cellD0 As String = String.Format("D{0}", index)
                    wb.Worksheet(1).Cells(cellD0).Value = "Linea"

                    Dim cellE0 As String = String.Format("E{0}", index)
                    wb.Worksheet(1).Cells(cellE0).Value = "Cantidad"

                    Dim cellF0 As String = String.Format("F{0}", index)
                    wb.Worksheet(1).Cells(cellF0).Value = "Precio"

                    Dim cellG0 As String = String.Format("G{0}", index)
                    wb.Worksheet(1).Cells(cellG0).Value = "Total"

                    Dim cellH0 As String = String.Format("H{0}", index)
                    wb.Worksheet(1).Cells(cellH0).Value = "MotivoDev"

                    Dim cellI0 As String = String.Format("I{0}", index)
                    wb.Worksheet(1).Cells(cellI0).Value = "Observacion"

                    Dim cellJ0 As String = String.Format("J{0}", index)
                    wb.Worksheet(1).Cells(cellJ0).Value = "FolioDev"

                    Dim cellK0 As String = String.Format("K{0}", index)
                    wb.Worksheet(1).Cells(cellK0).Value = "IdCliente"

                    Dim cellL0 As String = String.Format("L{0}", index)
                    wb.Worksheet(1).Cells(cellL0).Value = "Cliente"

                    Dim cellM0 As String = String.Format("M{0}", index)
                    wb.Worksheet(1).Cells(cellM0).Value = "Agente"

                    Dim cellN0 As String = String.Format("N{0}", index)
                    wb.Worksheet(1).Cells(cellN0).Value = "DocSAP"

                    Dim cellO0 As String = String.Format("O{0}", index)
                    wb.Worksheet(1).Cells(cellO0).Value = "Factura"

                    Dim cellP0 As String = String.Format("P{0}", index)
                    wb.Worksheet(1).Cells(cellP0).Value = "LineNum"

                    'AGREGAR LAS CELDAS DEPENDIENDO EL REPORTE SEGUIR LA MISMA ESTRUCTURA

                    index = index + 1
                End If

                'Formato de cada una de las celdas
                Dim cellA As String = String.Format("A{0}", index)
                wb.Worksheet(1).Cells(cellA).Style.NumberFormat.Format = "dd-MM-yyyy"

                Dim cellB As String = String.Format("B{0}", index)
                'wb.Worksheet(1).Cells(cellB).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellC As String = String.Format("C{0}", index)
                'wb.Worksheet(1).Cells(cellC).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellD As String = String.Format("D{0}", index)
                'wb.Worksheet(1).Cells(cellD).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellE As String = String.Format("E{0}", index)
                wb.Worksheet(1).Cells(cellE).Style.NumberFormat.Format = "#,##0"

                Dim cellF As String = String.Format("F{0}", index)
                wb.Worksheet(1).Cells(cellF).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellG As String = String.Format("G{0}", index)
                wb.Worksheet(1).Cells(cellG).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellH As String = String.Format("H{0}", index)
                'wb.Worksheet(1).Cells(cellH).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellI As String = String.Format("I{0}", index)
                'wb.Worksheet(1).Cells(cellI).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellJ As String = String.Format("J{0}", index)
                wb.Worksheet(1).Cells(cellJ).Style.NumberFormat.Format = "#,##0"

                Dim cellK As String = String.Format("K{0}", index)
                'wb.Worksheet(1).Cells(cellK).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellL As String = String.Format("L{0}", index)
                'wb.Worksheet(1).Cells(cellL).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellM As String = String.Format("M{0}", index)
                'wb.Worksheet(1).Cells(cellM).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellN As String = String.Format("N{0}", index)
                'wb.Worksheet(1).Cells(cellN).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellO As String = String.Format("O{0}", index)
                'wb.Worksheet(1).Cells(cellO).Style.NumberFormat.Format = "$ #,##0.00"

                Dim cellP As String = String.Format("P{0}", index)
                'wb.Worksheet(1).Cells(cellP).Style.NumberFormat.Format = "$ #,##0.00"


                '"0.0\%"

            Catch ex As Exception
                MessageBox.Show(ex.ToString(), "Error al dar formato a celdas")
            End Try

            index = index + 1
        Next

        ws.Columns().Width = 15
        ws.Rows(6).Style.Alignment.WrapText = False

        Try
            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Filter = "Excel|*.xlsx"
            saveFileDialog1.Title = "Save Excel File"
            saveFileDialog1.FileName = "Devolución de Materiales de " + DtpFechaIni.Value.ToString("dd-MM-yyyy") + " al " + DtpFechaTer.Value.ToString("dd-MM-yyyy") + ".xlsx"
            saveFileDialog1.ShowDialog()
            saveFileDialog1.InitialDirectory = "C:/"

            If saveFileDialog1.FileName <> "" Then
                Dim fs As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
                fs.Close()
            End If

            Dim strFileName As String = saveFileDialog1.FileName
            wb.SaveAs(strFileName)
            Process.Start(saveFileDialog1.FileName)
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "Error al guardar el archivo")
        End Try
    End Sub

    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        ' obtener indice de la columna 
        Dim columna As Integer = GrdDevMat.CurrentCell.ColumnIndex

        'se obtiene el nombre de la columna
        Dim NombreCol As String = GrdDevMat.Columns.Item(columna).Name

        If NombreCol = "FolioDev" Then
            ' Obtener caracter 
            Dim caracter As Char = e.KeyChar
            If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False Then
                'Me.Text = e.KeyChar 
                e.KeyChar = Chr(0)
            End If
        End If
    End Sub

    Private Sub GrdDevMat_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles GrdDevMat.EditingControlShowing
        ' obtener indice de la columna 
        Dim columna As Integer = GrdDevMat.CurrentCell.ColumnIndex
        'se obtiene el nombre de la columna
        Dim NombreCol As String = GrdDevMat.Columns.Item(columna).Name

        If NombreCol = "FolioDev" Then
            ' referencia a la celda 
            Dim validar As TextBox = CType(e.Control, TextBox)
            ' agregar el controlador de eventos para el KeyPress 
            AddHandler validar.KeyPress, AddressOf validar_Keypress
        End If
    End Sub
End Class