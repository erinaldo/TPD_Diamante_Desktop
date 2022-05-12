Imports System.Data.SqlClient
Public Class CtrlEntregas

    Dim vActvalor As Integer = 0
    Dim DvFletes As New DataView
    Dim vCant As Integer = 0
    Dim vIni As DateTime 'Fecha de Inicio
    Dim vFin As Date 'Fecha de Termino
    Dim vFchComp As Date 'Fecha comprometida
    Dim vGarantia As String = ""
    Sub cargar_registros()

        ' crear nueva conexión    
        Dim conexion2 As New SqlConnection(StrCon)

        ' abrir la conexión con la base de datos   
        conexion2.Open()

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand

        Dim SQLTPD As String

        SQLTPD = "SELECT T8.Entregado, T8.Factura COLLATE Modern_Spanish_CI_AI AS Factura, T8.Fchfact,T8.Flete, "
        SQLTPD &= "T8.Codclte COLLATE Modern_Spanish_CI_AI AS Codclte,T8.Cliente COLLATE Modern_Spanish_CI_AI AS Cliente, "
        SQLTPD &= "T8.Ciudad COLLATE Modern_Spanish_CI_AI AS Ciudad,T8.Estado COLLATE Modern_Spanish_CI_AI AS Estado,"
        SQLTPD &= "T8.Telefono COLLATE Modern_Spanish_CI_AI AS Telefono,T8.Contacto COLLATE Modern_Spanish_CI_AI AS Contacto, "
        SQLTPD &= "T8.Fletera COLLATE Modern_Spanish_CI_AI AS Fletera,T8.Articulo COLLATE Modern_Spanish_CI_AI AS Articulo, T8.Cantidad,"
        SQLTPD &= "T8.Fchemb, T8.crastreo,T8.Recibeguia COLLATE Modern_Spanish_CI_AI AS Recibeguia, T8.Horarec, T8.Fchrec,T8.Pdeclarado,"
        SQLTPD &= "T8.vguiadec,T8.Pesocob, T8.vguiacob,T8.Difpeso,T8.Difvalor,T8.LineTotal, T8.Fchent,"
        SQLTPD &= "T8.Matrecpor COLLATE Modern_Spanish_CI_AI AS Matrecpor, T8.Diasent, T8.Garantia,"
        SQLTPD &= " T8.comentarios, T8.Idfletera,T8.Usuariocap, T8.Descripcion COLLATE Modern_Spanish_CI_AI AS Descripcion,CASE WHEN T8.Excluir IS NULL THEN 'False' ELSE T8.Excluir END AS Excluir,CASE WHEN T8.Cortesia IS NULL THEN 'False' ELSE T8.Cortesia END AS Cortesia "
        SQLTPD &= "FROM [TPM].dbo.EMBENT T8 "
        'SQLTPD &= "WHERE T8.Fchfact >= @FechaIni AND T8.Fchfact <= @FechaTer "


        If RadBEmb.Checked = True Then
            SQLTPD &= "WHERE T8.Fchemb >= @FechaIni AND T8.Fchemb <= @FechaTer "
        Else
            SQLTPD &= "WHERE T8.Fchfact >= @FechaIni AND T8.Fchfact <= @FechaTer "
        End If


        If Me.CmbEnvio.SelectedValue <> 0 Then
            SQLTPD &= "AND T8.Idfletera = " + Me.CmbEnvio.SelectedValue.ToString + " "
        End If


        'FilaFlete("ItemCode") = "SERVLOG"
        'FilaFlete("ItemName") = "SERVICIOS LOGISTICOS EN TARIMA"

        If Me.CmbFlete.SelectedValue = "SERVLOG" Then
            SQLTPD &= "AND T8.Articulo NOT IN ('FL-001','FL-002','FL-003','FL-004','FL-010','FL-018') "
        ElseIf Me.CmbFlete.SelectedValue = "FL-001" Then
            SQLTPD &= "AND (T8.Articulo = 'FL-001' OR T8.Articulo = 'FL-018') "
        ElseIf Me.CmbFlete.SelectedValue <> "TODOS" Then
            SQLTPD &= "AND T8.Articulo = '" + Me.CmbFlete.SelectedValue + "' "
        End If


        'If Me.CmbFlete.SelectedValue <> "TODOS" Then
        '    SQLTPD &= "AND T8.Articulo = '" + Me.CmbFlete.SelectedValue + "' "
        'End If


        SQLTPD &= "UNION ALL "
        SQLTPD &= "SELECT 'False' as Entregado,T0.EDocNum as Factura,T0.[DocDate] as Fchfact,CAST(1 AS int) AS Flete,T0.[CardCode] as Codclte,"
        SQLTPD &= "T0.[CardName] as 'Cliente',T2.City AS Ciudad,T2.State1 AS Estado,T2.Phone1 AS Telefono,T2.CntctPrsn AS Contacto,"
        SQLTPD &= "TrnspName AS Fletera,T1.ItemCode AS Articulo,T1.Quantity AS Cantidad, T0.[DocDate] as Fchemb,CAST(0 AS varchar(13)) AS crastreo,"
        SQLTPD &= "CAST('' AS nvarchar(150)) AS Recibeguia,CAST('' AS varchar(5)) AS Horarec,CAST('' AS datetime) AS Fchrec,"
        SQLTPD &= "CAST('' AS int) AS Pdeclarado,CAST(0 AS numeric(19, 6)) AS vguiadec,CAST('' AS int) AS Pesocob,"
        SQLTPD &= "CAST(0 AS numeric(19, 6)) AS vguiacob,CAST('' AS int) AS Difpeso,CAST(0 AS numeric(19, 6)) AS Difvalor,T1.LineTotal / T1.Quantity AS LineTotal,"
        SQLTPD &= "CAST('' AS datetime) AS Fchent,CAST('' AS nvarchar(150)) AS Matrecpor,CAST('' AS int) AS Diasent,CAST('' AS varchar(250)) AS Garantia,"
        SQLTPD &= "CAST('' AS varchar(250)) AS comentarios,T3.Trnspcode AS Idfletera,CAST('' AS varchar(10)) AS Usuariocap, T1.Dscription AS Descripcion,'False' AS Excluir,'False' AS Cortesia "
        SQLTPD &= "FROM [SBO_TPD].dbo.OINV T0 "
        SQLTPD &= "INNER JOIN [SBO_TPD].dbo.INV1 T1 ON T0.DocEntry = T1.DocEntry "
        SQLTPD &= "LEFT JOIN [SBO_TPD].dbo.OCRD T2 ON T0.CardCode = T2.CardCode "
        SQLTPD &= "LEFT JOIN [SBO_TPD].dbo.OSHP T3 ON T0.Trnspcode = T3.Trnspcode "
        SQLTPD &= "WHERE T0.DocDate >= @FechaIni AND T0.DocDate <= @FechaTer AND T0.EDocCancel = 'N' AND T0.DocType = 'I' "


        If Me.CmbEnvio.SelectedValue <> 0 Then
            SQLTPD &= "AND T3.Trnspcode = " + Me.CmbEnvio.SelectedValue.ToString + " "
        End If



        If Me.CmbFlete.SelectedValue = "SERVLOG" Then
            SQLTPD &= "AND T1.ItemCode NOT IN ('FL-001','FL-002','FL-003','FL-004','FL-010','FL-018') "
        ElseIf Me.CmbFlete.SelectedValue = "FL-001" Then
            SQLTPD &= "AND (T1.ItemCode = 'FL-001' OR T1.ItemCode = 'FL-018') "
        ElseIf Me.CmbFlete.SelectedValue <> "TODOS" Then
            SQLTPD &= "AND T1.ItemCode = '" + Me.CmbFlete.SelectedValue + "' "
        End If

        'If Me.CmbFlete.SelectedValue <> "TODOS" Then
        '    SQLTPD &= "AND T1.ItemCode = '" + Me.CmbFlete.SelectedValue + "' "
        'End If


        SQLTPD &= "AND T1.ItemCode IN "
        SQLTPD &= "(SELECT T4.ItemCode FROM [SBO_TPD].dbo.OITM T4 WHERE T4.ItmsGrpCod = 187 "
        SQLTPD &= "AND T4.ItemCode <> 'FL-002' AND T4.ItemCode <> 'FL-003' AND T4.ItemCode <> 'FL-004') "
        SQLTPD &= "AND T0.EDocNum NOT IN "
        SQLTPD &= "(SELECT T9.Factura COLLATE Modern_Spanish_CI_AI AS Factura FROM [TPM].dbo.EMBENT T9 "
        SQLTPD &= "WHERE T9.Fchfact >= @FechaIni AND T9.Fchfact <= @FechaTer)"


        'If Me.CmbEnvio.SelectedValue <> "TODOS" Then
        '    SQLTPD &= "AND T0.CardCode = @Agente "
        'End If


        With comando
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


        'Clona la estructura del datatable pero sin registros
        Dim DtFormatFlete As DataTable = DtFactProv.Clone()
        ''Dim DtFormatFlete As New DataTable

        'DtFormatFlete = DtFactProv
        Dim vFlete As Integer = 1

        For Each row As DataRow In DtFactProv.Rows
            vCant = 0
            vIni = row("Fchemb")
            vFchComp = row("Fchemb")
            vFin = row("Fchent")

            vGarantia = ""

            If vFin.Year <> 1900 Then
                DiasHab()
                row("Diasent") = vCant
                row("Garantia") = vGarantia
            End If
            If row("Cantidad") > 1 And row("Usuariocap").ToString = "" Then
                For i = 0 To row("Cantidad") - 1

                    'dt1.ImportRow(dr1)
                    'DtFactProv.ImportRow(DtFormatFlete)
                    'DtFormatFlete()
                    row("Flete") = vFlete

                    DtFormatFlete.ImportRow(row)
                    vFlete += 1

                Next
            Else
                DtFormatFlete.ImportRow(row)
            End If
            'row("Flete") = 1
            vFlete = 1
        Next

        DvFletes = DtFormatFlete.DefaultView

        'DvFactProv = DtFactProv.DefaultView

        With Me.DgFactProv
            .DataSource = DvFletes 'DtFactProv 'DtFormatFlete    
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            '.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            '.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue


            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With

        DgFactProv.Sort(DgFactProv.Columns("Fchemb"), System.ComponentModel.ListSortDirection.Ascending)

        With conexion2
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .Dispose()
        End With

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button1.Enabled = False
        If IsNothing(CmbFlete.SelectedValue) Then
            MessageBox.Show("Seleccione un agente de ventas", _
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbFlete.Focus()
            Return
        End If

        cargar_registros()
        VerEntregados()

        Button1.Enabled = True

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadBEmb.Checked = True
        Me.DtpFechaIni.Value = Format(Date.Now, "dd/MM/yyyy")
        Me.DtpFechaTer.Value = Format(Date.Now, "dd/MM/yyyy")

        Dim Consulta As String
        Dim DsetFletes As New DataSet

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

            'Consulta = "SELECT ItemCode,ItemCode + ' ' + ItemName AS ItemName  FROM OITM T0 WHERE T0.ItmsGrpCod = 187 "
            'Consulta &= "AND ItemCode <> 'FL-002' AND ItemCode <> 'FL-003' AND ItemCode <> 'FL-004'"

            Consulta = "SELECT ItemCode,ItemCode + ' ' + ItemName AS ItemName FROM OITM T0 "
            Consulta &= "WHERE T0.ItmsGrpCod = 187 AND  (ItemCode = 'FL-001' OR ItemCode = 'FL-010')  "


            Dim DadFlete As New Data.SqlClient.SqlDataAdapter(Consulta, StrCon)

            DadFlete.Fill(DsetFletes, "Fletes")

            Dim FilaFlete As Data.DataRow

            FilaFlete = DsetFletes.Tables("Fletes").NewRow

            FilaFlete("ItemCode") = "SERVLOG"
            FilaFlete("ItemName") = "SERVICIOS LOGISTICOS EN TARIMA"

            DsetFletes.Tables("Fletes").Rows.Add(FilaFlete)


            FilaFlete = DsetFletes.Tables("Fletes").NewRow

            FilaFlete("ItemCode") = "TODOS"
            FilaFlete("ItemName") = "TODOS"

            DsetFletes.Tables("Fletes").Rows.Add(FilaFlete)
            Me.CmbFlete.DataSource = DsetFletes.Tables("Fletes")
            Me.CmbFlete.DisplayMember = "ItemName"
            Me.CmbFlete.ValueMember = "ItemCode"
            Me.CmbFlete.SelectedValue = "TODOS"


            Consulta = "SELECT Trnspcode,TrnspName FROM OSHP ORDER BY TrnspName"
            Dim daEnvio As New SqlClient.SqlDataAdapter(Consulta, SqlConnection)

            Dim FilaPaqueteria As Data.DataRow

            daEnvio.Fill(DsetFletes, "Paqueteria")

            FilaPaqueteria = DsetFletes.Tables("Paqueteria").NewRow

            FilaPaqueteria("Trnspcode") = 0
            FilaPaqueteria("TrnspName") = "TODOS"

            DsetFletes.Tables("Paqueteria").Rows.Add(FilaPaqueteria)

            Me.CmbEnvio.DataSource = DsetFletes.Tables("Paqueteria")
            Me.CmbEnvio.DisplayMember = "TrnspName"
            Me.CmbEnvio.ValueMember = "Trnspcode"

            CmbEnvio.SelectedValue = 0

        End Using


        If UsrTPM = "RLIRA" Then
            'TxtTotFletes.Text = Format(DgFactProv.RowCount, "##,###,###,###")


            'TxtPesoDec.Text = Format(VPesoDec, "##,###,###,###")
            TxtValorDec.Visible = False
            LblValorDec1.Visible = False
            LblValorDec2.Visible = False

            'TxtPesoCob.Text = Format(VPesoCob, "##,###,###,###")
            TxtValorCob.Visible = False
            LblValorCob1.Visible = False
            LblValorCob2.Visible = False

            'TxtDifPeso.Text = Format(VDifPeso, "##,###,###,###")
            TxtDifValor.Visible = False
            LblDifValor1.Visible = False
            LblDifValor2.Visible = False


            TxtCostoSap.Visible = False
            LblCostoSap1.Visible = False
            LblCostoSap2.Visible = False

            DgFactProv.Columns.Item("vguiadec").Visible = False
            DgFactProv.Columns.Item("vguiacob").Visible = False
            DgFactProv.Columns.Item("Difvalor").Visible = False
            DgFactProv.Columns.Item("LineTotal").Visible = False


        End If



    End Sub

    Private Sub DgFactProv_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgFactProv.CellEndEdit

        'DgFactProv_CellValidating(Me, Nothing)

        If vActvalor = 1 Then

            '' obtener indice de la columna 
            Dim columna As Integer = DgFactProv.CurrentCell.ColumnIndex


            'se obtiene el nombre de la columna
            Dim NombreCol As String = DgFactProv.Columns.Item(columna).Name

            If vActvalor = 1 And (NombreCol = "Recibeguia" Or NombreCol = "Horarec" Or NombreCol = "Fchrec") Then

                If DgFactProv.CurrentRow.Cells("crastreo").Value = 0 Then
                    MessageBox.Show("Antes capture el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    Return
                End If
            End If

            If NombreCol = "Fchrec" And vActvalor = 1 Then
                If DgFactProv.CurrentRow.Cells("Recibeguia").Value = "" Then
                    MessageBox.Show("Antes capture el nombre de la persona que recibio el el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    Return
                End If

                If DgFactProv.CurrentRow.Cells("Horarec").Value = "" Then
                    MessageBox.Show("Antes capture la hora en la que se confirmo el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    Return
                End If
            End If


            If NombreCol = "Horarec" Then

                If DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue <> "" Then

                    Dim vcadena As String

                    vcadena = Replace(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue, ":", "")

                    If Len(vcadena) < 4 Then
                        MessageBox.Show("capture los 4 digitos de la hora", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        DgFactProv.CurrentRow.Cells("Horarec").Value = ""

                        Return
                    End If

                    If Val(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue.SubString(0, 2)) > 23 Then
                        MessageBox.Show("La hora no puede ser mayor a 23", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Return
                    End If

                    If Val(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue.SubString(2, 2)) > 59 Then
                        MessageBox.Show("Los minutos no pueden ser mayor a 59", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Return
                    End If
                End If
            End If


            If NombreCol = "Pdeclarado" And vActvalor = 1 Then
                If DgFactProv.CurrentRow.Cells("Pdeclarado").Value = 0 Then
                    MessageBox.Show("Primero capture el peso declarado", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If
            End If

            If NombreCol = "crastreo" And vActvalor = 1 Then
                If Len(DgFactProv.CurrentRow.Cells("crastreo").EditedFormattedValue) < 10 And DgFactProv.CurrentRow.Cells("crastreo").EditedFormattedValue <> "0" Then
                    MessageBox.Show("El codigo de rastreo no puede ser menor a 10 caracteres", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If
            End If


            If NombreCol = "Fchent" And vActvalor = 1 Then

                Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
                Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchent").EditedFormattedValue


                If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
                    MessageBox.Show("La fecha de entrega no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If
            End If


            If NombreCol = "Fchemb" And vActvalor = 1 Then

                Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
                Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchent").EditedFormattedValue

                If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
                    MessageBox.Show("La fecha de entrega no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return

                End If
            End If


            If NombreCol = "Fchrec" And vActvalor = 1 Then

                Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
                Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchrec").EditedFormattedValue

                If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
                    MessageBox.Show("El día de confirmación de guía no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return

                End If
            End If


            'UsrTPM = "MANAGER"
            '***********************************************************************************************************UsrTPM = "MANAGER"
            Dim vpuntos As Int16 = 0
            Dim i As Integer

            Dim nCadena As String
            'On Error Resume Next
            'Asignamos valor a la cadena de trabajo para
            'no modificar la que envía el cliente.
            nCadena = DgFactProv.CurrentRow.Cells("Horarec").Value
            For i = 1 To Len(nCadena)
                If Mid(nCadena, i, 1) = ":" Then
                    vpuntos = 1
                End If
            Next i

            Dim vHora As String = ""

            If DgFactProv.CurrentRow.Cells("Horarec").Value <> "" Then
                If vpuntos = 1 Then
                    vHora = DgFactProv.CurrentRow.Cells("Horarec").Value.SubString(0, 2) + ":" + DgFactProv.CurrentRow.Cells("Horarec").Value.SubString(3, 2)
                Else
                    vHora = DgFactProv.CurrentRow.Cells("Horarec").Value.SubString(0, 2) + ":" + DgFactProv.CurrentRow.Cells("Horarec").Value.SubString(2, 2)
                End If
                DgFactProv.CurrentRow.Cells("Horarec").Value = vHora
            End If

            Dim ValorDec As Decimal

            If Val(DgFactProv.CurrentRow.Cells("Pdeclarado").Value) > 0 Then

                ValorDec = 49.5 + 7.47 + (1.37 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1))
                'If DgFactProv.CurrentRow.Cells("Pdeclarado").Value = 1 Then
                '    ValorDec = 52.79
                'Else
                '    ValorDec = 52.79 + (1.1 * (DgFactProv.CurrentRow.Cells("Pdeclarado").Value - 1))
                'End If
                DgFactProv.CurrentRow.Cells("vguiadec").Value = ValorDec.ToString
            End If

            If Val(DgFactProv.CurrentRow.Cells("Pesocob").Value) > 0 Then
                ValorDec = 49.5 + 7.47 + (1.37 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1))
                'If DgFactProv.CurrentRow.Cells("Pesocob").Value = 1 Then
                '    ValorDec = 52.79
                'Else
                '    ValorDec = 52.79 + (1.1 * (DgFactProv.CurrentRow.Cells("Pesocob").Value - 1))
                'End If
                DgFactProv.CurrentRow.Cells("vguiacob").Value = ValorDec.ToString
            End If

            If Val(DgFactProv.CurrentRow.Cells("Pdeclarado").Value) = 0 Then
                DgFactProv.CurrentRow.Cells("vguiadec").Value = 0
            End If

            If Val(DgFactProv.CurrentRow.Cells("Pesocob").Value) = 0 Then
                DgFactProv.CurrentRow.Cells("vguiacob").Value = 0
            End If


            If Val(DgFactProv.CurrentRow.Cells("Pesocob").Value) = 0 Then
                DgFactProv.CurrentRow.Cells("Difpeso").Value = 0
                DgFactProv.CurrentRow.Cells("Difvalor").Value = 0
            Else
                DgFactProv.CurrentRow.Cells("Difpeso").Value = DgFactProv.CurrentRow.Cells("Pesocob").Value - DgFactProv.CurrentRow.Cells("Pdeclarado").Value
                DgFactProv.CurrentRow.Cells("Difvalor").Value = DgFactProv.CurrentRow.Cells("vguiacob").Value - DgFactProv.CurrentRow.Cells("vguiadec").Value

            End If


            vIni = DgFactProv.CurrentRow.Cells("Fchemb").Value
            vFin = DgFactProv.CurrentRow.Cells("Fchent").Value

            If vIni.Year > 1900 And vFin.Year > 1900 Then

                vCant = 0
                vIni = DgFactProv.CurrentRow.Cells("Fchemb").Value
                vFin = DgFactProv.CurrentRow.Cells("Fchent").Value

                DiasHab()
                DgFactProv.CurrentRow.Cells("Diasent").Value = vCant

            End If


            vpuntos = 0

            ActualizaValores()

            vActvalor = 0
            TotalFacturas()
        End If

    End Sub

    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        ' obtener indice de la columna 
        Dim columna As Integer = DgFactProv.CurrentCell.ColumnIndex

        'se obtiene el nombre de la columna
        Dim NombreCol As String = DgFactProv.Columns.Item(columna).Name

        If NombreCol = "crastreo" Or NombreCol = "Pdeclarado" Or NombreCol = "Pesocob" Or NombreCol = "Horarec" Then
            ' Obtener caracter 
            Dim caracter As Char = e.KeyChar
            If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False Then
                'Me.Text = e.KeyChar 
                e.KeyChar = Chr(0)
            End If
        End If
    End Sub

    Private Sub DgFactProv_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DgFactProv.EditingControlShowing
        ' obtener indice de la columna 
        Dim columna As Integer = DgFactProv.CurrentCell.ColumnIndex
        'se obtiene el nombre de la columna
        Dim NombreCol As String = DgFactProv.Columns.Item(columna).Name

        If NombreCol = "crastreo" Or NombreCol = "Pdeclarado" Or NombreCol = "Pesocob" Or NombreCol = "Horarec" Then
            ' referencia a la celda 
            Dim validar As TextBox = CType(e.Control, TextBox)
            ' agregar el controlador de eventos para el KeyPress 
            AddHandler validar.KeyPress, AddressOf validar_Keypress
        End If

    End Sub

    Private Sub DgFactProv_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DgFactProv.CellValidating

        ' obtener indice de la columna 
        Dim columna As Integer = DgFactProv.CurrentCell.ColumnIndex

        'se obtiene el nombre de la columna
        Dim NombreCol As String = DgFactProv.Columns.Item(columna).Name


        If NombreCol = "Pdeclarado" And vActvalor = 1 Then
            If DgFactProv.CurrentRow.Cells("Pdeclarado").Value = 0 Then
                MessageBox.Show("Primero capture el peso declarado", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
                Return
            End If
        End If

        If NombreCol = "crastreo" And vActvalor = 1 Then
            If Len(DgFactProv.CurrentRow.Cells("crastreo").EditedFormattedValue) < 10 And DgFactProv.CurrentRow.Cells("crastreo").EditedFormattedValue <> "0" Then
                MessageBox.Show("El codigo de rastreo no puede ser menor a 10 caracteres", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
                Return
            End If
        End If


        If NombreCol = "Fchent" And vActvalor = 1 Then

            Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
            Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchent").EditedFormattedValue


            If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
                MessageBox.Show("La fecha de entrega no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
                Return
            End If
        End If

        '****************************************************************
        If NombreCol = "Fchemb" And vActvalor = 1 Then

            Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
            Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchent").EditedFormattedValue

            If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
                MessageBox.Show("La fecha de entrega no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
                Return

            End If
        End If


        If NombreCol = "Fchrec" And vActvalor = 1 Then

            Dim VFchEmb As Date = DgFactProv.CurrentRow.Cells("Fchemb").Value
            Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchrec").EditedFormattedValue

            If DateDiff(DateInterval.Day, VFchEmb, VFchEnt) < 0 Then
                MessageBox.Show("El día de confirmación de guía no puede ser menor a la fecha de embarque", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
                Return

            End If
        End If


        '------------------------------
        '     'Horarec,Recibeguia
        If vActvalor = 1 And (NombreCol = "Recibeguia" Or NombreCol = "Horarec" Or NombreCol = "Fchrec") Then

            If DgFactProv.CurrentRow.Cells("crastreo").Value = 0 Then
                MessageBox.Show("Antes capture el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
                Return
            End If
        End If

        If NombreCol = "Fchrec" And vActvalor = 1 Then
            If DgFactProv.CurrentRow.Cells("Recibeguia").Value = "" Then
                MessageBox.Show("Antes capture el nombre de la persona que recibio el el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
                Return
            End If

            If DgFactProv.CurrentRow.Cells("Horarec").Value = "" Then
                MessageBox.Show("Antes capture la hora en la que se confirmo el código de rastreo", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Cancel = True
                Return
            End If
        End If

        'Fchrec


        If NombreCol = "Horarec" Then
            Dim vpuntos As Int16 = 0
            Dim i As Integer
            Dim nCadena As String
            On Error Resume Next
            'Asignamos valor a la cadena de trabajo para
            'no modificar la que envía el cliente.
            nCadena = DgFactProv.CurrentRow.Cells("Horarec").Value
            For i = 1 To Len(nCadena)
                If Mid(nCadena, i, 1) = ":" Then
                    vpuntos = 1
                End If
            Next i


            Dim vHora As String = ""


            If DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue <> "" Then

                Dim vcadena As String
                'DgFactProv.CurrentCell = DgFactProv.Item("ColumnName", 5)
                vcadena = Replace(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue, ":", "")

                If Len(vcadena) < 4 Then
                    MessageBox.Show("capture los 4 digitos de la hora", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    DgFactProv.CurrentRow.Cells("Horarec").Value = ""
                    e.Cancel = True
                    Return
                End If

                If Val(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue.SubString(0, 2)) > 23 Then
                    MessageBox.Show("La hora no puede ser mayor a 23", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    e.Cancel = True
                    Return
                End If

                If Val(DgFactProv.CurrentRow.Cells("Horarec").EditedFormattedValue.SubString(2, 2)) > 59 Then
                    MessageBox.Show("Los minutos no pueden ser mayor a 59", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    e.Cancel = True
                    Return
                End If
            End If
        End If
    End Sub

    Private Sub DgFactProv_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DgFactProv.CurrentCellDirtyStateChanged
        ' evento que detecta cuando se cambio o actualizo el valor de una celda
        'Este codigo sirve para que se pueda identificar el proceso del checkbox dentro del datagridview junto
        'con el evento de DgFactProv_CellContentClick

        If DgFactProv.IsCurrentCellDirty Then
            vActvalor = 1
            DgFactProv.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub

    Private Sub DgFactProv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgFactProv.CellContentClick
        Dim VErrorG As Integer = 0
        Dim strValue As String


        If e.ColumnIndex = 0 And vActvalor = 1 Then
            Dim VFchEnt As Date = DgFactProv.CurrentRow.Cells("Fchent").Value
            If VFchEnt.Year = 1900 Then
                MessageBox.Show("Primero capture la fecha de entrega", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = 0
                Me.DgFactProv.RefreshEdit()
                Return
            End If

        End If

        'e.RowIndex >= 0
        If e.ColumnIndex = 0 And vActvalor = 1 Then
            Dim row As DataGridViewRow = DgFactProv.Rows(e.RowIndex)
            Try

                If Me.DgFactProv.Columns(e.ColumnIndex).Name = "Entregado" And DgFactProv.Rows(e.RowIndex).Cells("Entregado").EditedFormattedValue = True Then
                    'The user clicked on the checkbox column
                    strValue = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value
                    If MessageBox.Show("¿Confirma que este flete fue entregado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = 1
                        ActualizaValores()
                        Me.DgFactProv.RefreshEdit()

                    Else
                        DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = 0
                        Me.DgFactProv.RefreshEdit()

                    End If

                Else

                    If MessageBox.Show("¿Confirma que este flete" + Chr(13) + "No fue entregado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = 0
                        ActualizaValores()
                        Me.DgFactProv.RefreshEdit()
                    Else
                        DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = 1
                        Me.DgFactProv.RefreshEdit()

                    End If

                End If


            Catch ex As Exception
                VErrorG = 1
            End Try

            If VErrorG = 1 Then
                MessageBox.Show("Error al actualizar registros", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        End If

        '---------------------------------------------------------------------------------------------------------------

        Try
            If DgFactProv.Columns(e.ColumnIndex).Name = "Excluir" And vActvalor = 1 And DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = True Then
                MessageBox.Show("Primero desmarque Cortesia", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = 0
                Me.DgFactProv.RefreshEdit()
                Return
            End If
        Catch
        End Try


        If DgFactProv.Columns(e.ColumnIndex).Name = "Excluir" And vActvalor = 1 Then
            Dim row As DataGridViewRow = DgFactProv.Rows(e.RowIndex)
            Try

                If Me.DgFactProv.Columns(e.ColumnIndex).Name = "Excluir" And DgFactProv.Rows(e.RowIndex).Cells("Excluir").EditedFormattedValue = True Then
                    'The user clicked on the checkbox column
                    strValue = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value
                    If MessageBox.Show("¿Confirma que este flete se va a Excluir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = 1
                        ActualizaValores()
                        Me.DgFactProv.RefreshEdit()

                    Else
                        DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = 0
                        Me.DgFactProv.RefreshEdit()

                    End If

                Else

                    If MessageBox.Show("¿Confirma que este flete" + Chr(13) + "No se va a Excluir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = 0
                        ActualizaValores()
                        Me.DgFactProv.RefreshEdit()
                    Else
                        DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = 1
                        Me.DgFactProv.RefreshEdit()

                    End If

                End If


            Catch ex As Exception
                VErrorG = 1
            End Try

            If VErrorG = 1 Then
                MessageBox.Show("Error al actualizar registros", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        End If



        '--------------------------------------------------------------------------------------------------------------------------------------

        Try
            If DgFactProv.Columns(e.ColumnIndex).Name = "Cortesia" And vActvalor = 1 And DgFactProv.Rows(e.RowIndex).Cells("Excluir").Value = True Then
                MessageBox.Show("Primero desmarque Excluir", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = 0
                Me.DgFactProv.RefreshEdit()
                Return
            End If
        Catch
        End Try


        If DgFactProv.Columns(e.ColumnIndex).Name = "Cortesia" And vActvalor = 1 Then
            Dim row As DataGridViewRow = DgFactProv.Rows(e.RowIndex)
            Try

                If Me.DgFactProv.Columns(e.ColumnIndex).Name = "Cortesia" And DgFactProv.Rows(e.RowIndex).Cells("Cortesia").EditedFormattedValue = True Then
                    'The user clicked on the checkbox column
                    strValue = Me.DgFactProv.Item(e.ColumnIndex, e.RowIndex).Value
                    If MessageBox.Show("¿Confirma que este flete es Cortesia?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = 1
                        ActualizaValores()
                        Me.DgFactProv.RefreshEdit()

                    Else
                        DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = 0
                        Me.DgFactProv.RefreshEdit()

                    End If

                Else

                    If MessageBox.Show("¿Confirma que este flete" + Chr(13) + "No es Cortesia?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = 0
                        ActualizaValores()
                        Me.DgFactProv.RefreshEdit()
                    Else
                        DgFactProv.Rows(e.RowIndex).Cells("Cortesia").Value = 1
                        Me.DgFactProv.RefreshEdit()

                    End If

                End If


            Catch ex As Exception
                VErrorG = 1
            End Try

            If VErrorG = 1 Then
                MessageBox.Show("Error al actualizar registros", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        End If


        vActvalor = 0
        TotalFacturas()
    End Sub

    Private Sub DgFactProv_RowPrePaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DgFactProv.RowPrePaint
        If Not IsDBNull(DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value) Then

            If DgFactProv.Rows(e.RowIndex).Cells("Entregado").Value = True Then

                DgFactProv.Rows(e.RowIndex).Cells("Entregado").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Entregado").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Factura").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Factura").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Fchfact").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Fchfact").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Flete").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Flete").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Codclte").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Codclte").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Cliente").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Cliente").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Ciudad").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Ciudad").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Estado").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Estado").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Fletera").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Fletera").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Articulo").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Articulo").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Descripcion").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Descripcion").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Cantidad").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Cantidad").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Fchemb").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Fchemb").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("crastreo").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("crastreo").Style.ForeColor = Color.Black


                DgFactProv.Rows(e.RowIndex).Cells("vguiadec").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("vguiadec").Style.ForeColor = Color.White


                DgFactProv.Rows(e.RowIndex).Cells("Pesocob").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Pesocob").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("vguiacob").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("vguiacob").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Difpeso").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Difpeso").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Difvalor").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Difvalor").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("LineTotal").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("LineTotal").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Fchent").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Fchent").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Matrecpor").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Matrecpor").Style.ForeColor = Color.Black

                If Not IsDBNull(DgFactProv.Rows(e.RowIndex).Cells("Garantia").Value) Then
                    If DgFactProv.Rows(e.RowIndex).Cells("Garantia").Value = "RECLAMAR" Then
                        DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.BackColor = Color.Yellow
                        DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.ForeColor = Color.Black
                    Else
                        DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.BackColor = Color.Red
                        DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.ForeColor = Color.White
                    End If
                Else
                    DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.BackColor = Color.Red
                    DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.ForeColor = Color.White
                End If

                DgFactProv.Rows(e.RowIndex).Cells("Diasent").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Diasent").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Recibeguia").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Recibeguia").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Horarec").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Horarec").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Fchrec").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Fchrec").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Pdeclarado").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Pdeclarado").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Telefono").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Telefono").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("Contacto").Style.BackColor = Color.Red
                DgFactProv.Rows(e.RowIndex).Cells("Contacto").Style.ForeColor = Color.White

                DgFactProv.Rows(e.RowIndex).Cells("comentarios").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("comentarios").Style.ForeColor = Color.Black

            Else

                DgFactProv.Rows(e.RowIndex).Cells("Fchemb").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Fchemb").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("crastreo").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("crastreo").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Pesocob").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Pesocob").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Fchent").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Fchent").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Matrecpor").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Matrecpor").Style.ForeColor = Color.Black

                If Not IsDBNull(DgFactProv.Rows(e.RowIndex).Cells("Garantia").Value) Then
                    If DgFactProv.Rows(e.RowIndex).Cells("Garantia").Value = "RECLAMAR" Then
                        DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.BackColor = Color.Yellow
                        DgFactProv.Rows(e.RowIndex).Cells("Garantia").Style.ForeColor = Color.Black
                    End If
                End If

                DgFactProv.Rows(e.RowIndex).Cells("Recibeguia").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Recibeguia").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Horarec").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Horarec").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Fchrec").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Fchrec").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("Pdeclarado").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("Pdeclarado").Style.ForeColor = Color.Black

                DgFactProv.Rows(e.RowIndex).Cells("comentarios").Style.BackColor = Color.Gainsboro
                DgFactProv.Rows(e.RowIndex).Cells("comentarios").Style.ForeColor = Color.Black

            End If
        End If

    End Sub

    Sub VerEntregados()
        Dim VFiltro As String = " "

        If ChkFEntregado.Checked = False Then
            VFiltro = "Excluir = 'False' AND Cortesia = 'False' AND Entregado = 0"
        Else
            VFiltro = "Excluir = 'False' AND Cortesia = 'False'"
        End If


        If ChkExcCortesia.Checked = True Then
            If ChkFEntregado.Checked = False Then

                VFiltro = "Excluir = 'True' OR Cortesia = 'True' AND Entregado  = 0"
            Else
                VFiltro = "Excluir = 'True' OR Cortesia = 'True'"

            End If
        End If


        If VFiltro = " " Then
            DvFletes.RowFilter = String.Empty
        Else
            DvFletes.RowFilter = VFiltro
        End If


        TotalFacturas()
    End Sub

    Private Sub ChkFEntregado_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkFEntregado.CheckedChanged
        VerEntregados()
    End Sub

    Sub TotalFacturas()
        Dim VPesoDec As Decimal = 0
        Dim VValorDec As Decimal = 0
        Dim VPesoCob As Decimal = 0
        Dim VValorCob As Decimal = 0
        Dim VDifPeso As Decimal = 0
        Dim VDifValor As Decimal = 0
        Dim VLineTotal As Decimal = 0


        For Each row As DataGridViewRow In Me.DgFactProv.Rows

            If row.Cells("Excluir").Value = False Then

                If row.Cells("Cortesia").Value = False Then

                    VPesoDec += row.Cells("Pdeclarado").Value
                    VValorDec += row.Cells("vguiadec").Value
                    VPesoCob += row.Cells("Pesocob").Value
                    VValorCob += row.Cells("vguiacob").Value
                    VDifPeso += row.Cells("Difpeso").Value
                    VDifValor += row.Cells("Difvalor").Value
                End If

                VLineTotal += IIf(IsDBNull(row.Cells("LineTotal").Value), 0, row.Cells("LineTotal").Value)
            End If

        Next

        TxtTotFletes.Text = Format(DgFactProv.RowCount, "##,###,###,###")


        TxtPesoDec.Text = Format(VPesoDec, "##,###,###,###")
        TxtValorDec.Text = Format(VValorDec, "$ ##,###,###,###.00")
        TxtPesoCob.Text = Format(VPesoCob, "##,###,###,###")
        TxtValorCob.Text = Format(VValorCob, "$ ##,###,###,###.00")
        TxtDifPeso.Text = Format(VDifPeso, "##,###,###,###")
        TxtDifValor.Text = Format(VDifValor, "$ ##,###,###,###.00")
        TxtCostoSap.Text = Format(VLineTotal, "$ ##,###,###,###.00")

    End Sub

    Private Sub BtnDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDetalle.Click
        GridAExcel(DgFactProv)
    End Sub

    Sub ActualizaValores()
        ' crear nueva conexión    
        Dim conexion As New SqlConnection(StrTpm)

        '' abrir la conexión con la base de datos   
        'conexion.Open()

        Dim Adaptador As New SqlDataAdapter()
        Dim comando As New SqlCommand

        Dim SQLTPD As String

        SQLTPD = "SELECT count(*) AS Num FROM EMBENT WHERE Factura = '" + DgFactProv.CurrentRow.Cells("Factura").Value + "'"

        Dim DsVtasDet As New DataSet

        'DsVtasDet.Tables.Add(DTRefacciones)

        Dim vRegFact As Integer = 0
        With comando
            .CommandText = SQLTPD
            .Connection = conexion
            .Connection.Open()
            vRegFact = IIf(IsDBNull(.ExecuteScalar), 0, .ExecuteScalar)
            .Connection.Close()
        End With


        If vRegFact >= 1 Then

            SQLTPD = "UPDATE EMBENT SET Entregado = "
            SQLTPD &= IIf(DgFactProv.CurrentRow.Cells("Entregado").Value = False, 0, 1).ToString
            SQLTPD &= ", "
            SQLTPD &= "Fchemb = @Fchemb" 'DgFactProv.CurrentRow.Cells("Fchemb").Value
            SQLTPD &= ", "
            SQLTPD &= "crastreo = '" + DgFactProv.CurrentRow.Cells("crastreo").Value.ToString
            SQLTPD &= "', "
            SQLTPD &= "Pdeclarado = " + DgFactProv.CurrentRow.Cells("Pdeclarado").Value.ToString
            SQLTPD &= ", "
            SQLTPD &= "vguiadec = " + DgFactProv.CurrentRow.Cells("vguiadec").Value.ToString
            SQLTPD &= ", "
            SQLTPD &= "Pesocob = " + DgFactProv.CurrentRow.Cells("Pesocob").Value.ToString
            SQLTPD &= ", "
            SQLTPD &= "vguiacob = " + DgFactProv.CurrentRow.Cells("vguiacob").Value.ToString
            SQLTPD &= ", "
            SQLTPD &= "Difpeso = " + DgFactProv.CurrentRow.Cells("Difpeso").Value.ToString
            SQLTPD &= ", "
            SQLTPD &= "Difvalor = " + DgFactProv.CurrentRow.Cells("Difvalor").Value.ToString
            SQLTPD &= ", "
            SQLTPD &= "LineTotal = " + IIf(IsDBNull(DgFactProv.CurrentRow.Cells("LineTotal").Value), 0, DgFactProv.CurrentRow.Cells("LineTotal").Value).ToString
            SQLTPD &= ", "
            SQLTPD &= "Fchent = @Fchent" 'DgFactProv.CurrentRow.Cells("Fchent").Value
            SQLTPD &= ", "
            SQLTPD &= "Matrecpor = '" + DgFactProv.CurrentRow.Cells("Matrecpor").Value + "'"
            SQLTPD &= ", "
            SQLTPD &= "Diasent = " + DgFactProv.CurrentRow.Cells("Diasent").Value.ToString
            SQLTPD &= ", "
            SQLTPD &= "Garantia = '" + IIf(IsDBNull(DgFactProv.CurrentRow.Cells("Garantia").Value), "", DgFactProv.CurrentRow.Cells("Garantia").Value).ToString + "'"
            SQLTPD &= ", "
            SQLTPD &= "Recibeguia = '" + DgFactProv.CurrentRow.Cells("Recibeguia").Value + "'"
            SQLTPD &= ", "
            SQLTPD &= "Horarec = '" + DgFactProv.CurrentRow.Cells("Horarec").Value + "'"
            SQLTPD &= ", "
            SQLTPD &= "Fchrec = @Fchrec" 'DgFactProv.CurrentRow.Cells("Fchrec").Value
            SQLTPD &= ", "
            SQLTPD &= "comentarios = '" + QuitarCaracteres(DgFactProv.CurrentRow.Cells("comentarios").Value) + "'"
            'SQLTPD &= "comentarios = '" + DgFactProv.CurrentRow.Cells("comentarios").Value + "'"
            SQLTPD &= ", "
            SQLTPD &= "Usuarioact = '" + UsrTPM + "'"
            SQLTPD &= ", "
            SQLTPD &= "Fchact = @Fchact "
            SQLTPD &= ", "
            SQLTPD &= "Excluir = " + IIf(DgFactProv.CurrentRow.Cells("Excluir").Value = False, 0, 1).ToString
            SQLTPD &= ", "
            SQLTPD &= "Cortesia = " + IIf(DgFactProv.CurrentRow.Cells("Cortesia").Value = False, 0, 1).ToString + " "
            'False' AS Excluir,'False' AS Cortesia


            SQLTPD &= "WHERE Factura = '" + DgFactProv.CurrentRow.Cells("Factura").Value + "' "
            SQLTPD &= "AND Flete = " + DgFactProv.CurrentRow.Cells("Flete").Value.ToString
            'DgFactProv.CurrentRow.Cells("Flete").Value

            Dim CmdActFlet As Data.SqlClient.SqlCommand

            CmdActFlet = New Data.SqlClient.SqlCommand()
            With CmdActFlet
                .Parameters.AddWithValue("@Fchemb", DgFactProv.CurrentRow.Cells("Fchemb").Value)
                .Parameters.AddWithValue("@Fchent", DgFactProv.CurrentRow.Cells("Fchent").Value)
                .Parameters.AddWithValue("@Fchrec", DgFactProv.CurrentRow.Cells("Fchrec").Value)
                .Parameters.AddWithValue("@Fchact", DateTime.Now)
                .Connection = New Data.SqlClient.SqlConnection(StrTpm)
                .Connection.Open()
                .CommandText = SQLTPD
                .ExecuteNonQuery()
                .Connection.Close()
            End With

        Else
            For i = 0 To Val(DgFactProv.CurrentRow.Cells("Cantidad").Value) - 1
              
                SQLTPD = "INSERT INTO EMBENT (Entregado, Factura, Fchfact, Flete, Codclte, Cliente, Ciudad, Estado, Fletera, Articulo, Descripcion,"
                SQLTPD &= "Cantidad, Fchemb, crastreo, Pdeclarado, vguiadec, Pesocob, vguiacob, Difpeso, Difvalor,LineTotal, Fchent, Matrecpor, Diasent,"
                SQLTPD &= "Garantia, Recibeguia, Horarec, Fchrec, Telefono, Contacto, comentarios, Idfletera, Usuariocap, Fchcap, Usuarioact, Fchact, Excluir, Cortesia) "
                SQLTPD &= "VALUES("

                If i + 1 = DgFactProv.CurrentRow.Cells("Flete").Value Then

                    SQLTPD &= IIf(DgFactProv.CurrentRow.Cells("Entregado").Value = False, 0, 1).ToString
                    SQLTPD &= ",'"

                    SQLTPD &= DgFactProv.CurrentRow.Cells("Factura").Value
                    SQLTPD &= "',"
                    SQLTPD &= "@Fchfact" 'DgFactProv.CurrentRow.Cells("Fchfact").Value
                    SQLTPD &= ","
                    SQLTPD &= (i + 1).ToString 'DgFactProv.CurrentRow.Cells("Flete").Value
                    SQLTPD &= ",'"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Codclte").Value
                    SQLTPD &= "','"
                    SQLTPD &= QuitarCaracteres(DgFactProv.CurrentRow.Cells("Cliente").Value.ToString)
                    'SQLTPD &= DgFactProv.CurrentRow.Cells("Cliente").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Ciudad").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Estado").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Fletera").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Articulo").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Descripcion").Value
                    SQLTPD &= "',"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Cantidad").Value.ToString
                    SQLTPD &= ","
                    SQLTPD &= "@Fchemb" 'DgFactProv.CurrentRow.Cells("Fchemb").Value
                    SQLTPD &= ",'"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("crastreo").Value
                    SQLTPD &= "',"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Pdeclarado").Value
                    SQLTPD &= ","
                    SQLTPD &= DgFactProv.CurrentRow.Cells("vguiadec").Value
                    SQLTPD &= ","
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Pesocob").Value
                    SQLTPD &= ","
                    SQLTPD &= DgFactProv.CurrentRow.Cells("vguiacob").Value
                    SQLTPD &= ","
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Difpeso").Value
                    SQLTPD &= ","
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Difvalor").Value
                    SQLTPD &= ","
                    SQLTPD &= IIf(IsDBNull(DgFactProv.CurrentRow.Cells("LineTotal").Value), 0, DgFactProv.CurrentRow.Cells("LineTotal").Value).ToString
                    SQLTPD &= ","
                    SQLTPD &= "@Fchent" 'DgFactProv.CurrentRow.Cells("Fchent").Value
                    SQLTPD &= ",'"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Matrecpor").Value
                    SQLTPD &= "',"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Diasent").Value
                    SQLTPD &= ",'"
                    SQLTPD &= IIf(IsDBNull(DgFactProv.CurrentRow.Cells("Garantia").Value), "", DgFactProv.CurrentRow.Cells("Garantia").Value).ToString
                    SQLTPD &= "',' "
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Recibeguia").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Horarec").Value
                    SQLTPD &= "',"
                    SQLTPD &= "@Fchrec" 'DgFactProv.CurrentRow.Cells("Fchrec").Value
                    SQLTPD &= ",'"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Telefono").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Contacto").Value
                    SQLTPD &= "','"
                    SQLTPD &= QuitarCaracteres(DgFactProv.CurrentRow.Cells("comentarios").Value)
                    'SQLTPD &= DgFactProv.CurrentRow.Cells("comentarios").Value
                    SQLTPD &= "',"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Idfletera").Value
                    SQLTPD &= ",'"
                    SQLTPD &= UsrTPM
                    SQLTPD &= "',"
                    SQLTPD &= "@Fchcap" 'DgFactProv.CurrentRow.Cells("Fchcap").Value
                    SQLTPD &= ",'"
                    SQLTPD &= UsrTPM
                    SQLTPD &= "',"
                    SQLTPD &= "@Fchact" 'DgFactProv.CurrentRow.Cells("Fchact").Value
                    SQLTPD &= ","
                    SQLTPD &= IIf(DgFactProv.CurrentRow.Cells("Excluir").Value = False, 0, 1).ToString
                    SQLTPD &= ","
                    SQLTPD &= IIf(DgFactProv.CurrentRow.Cells("Cortesia").Value = False, 0, 1).ToString


                    SQLTPD &= ")"
                Else
                    SQLTPD &= "0"
                    SQLTPD &= ",'"

                    SQLTPD &= DgFactProv.CurrentRow.Cells("Factura").Value
                    SQLTPD &= "',"
                    SQLTPD &= "@Fchfact" 'DgFactProv.CurrentRow.Cells("Fchfact").Value
                    SQLTPD &= ","
                    SQLTPD &= (i + 1).ToString 'DgFactProv.CurrentRow.Cells("Flete").Value
                    SQLTPD &= ",'"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Codclte").Value
                    SQLTPD &= "','"
                    SQLTPD &= QuitarCaracteres(DgFactProv.CurrentRow.Cells("Cliente").Value.ToString)
                    'SQLTPD &= DgFactProv.CurrentRow.Cells("Cliente").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Ciudad").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Estado").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Fletera").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Articulo").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Descripcion").Value
                    SQLTPD &= "',"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Cantidad").Value
                    SQLTPD &= ","
                    SQLTPD &= "@Fchemb" 'DgFactProv.CurrentRow.Cells("Fchemb").Value
                    SQLTPD &= ","
                    SQLTPD &= "0"
                    SQLTPD &= ","
                    SQLTPD &= "0"
                    SQLTPD &= ","
                    SQLTPD &= "0"
                    SQLTPD &= ","
                    SQLTPD &= "0"
                    SQLTPD &= ","
                    SQLTPD &= "0"
                    SQLTPD &= ","
                    SQLTPD &= "0"
                    SQLTPD &= ","
                    SQLTPD &= "0"
                    SQLTPD &= ","
                    SQLTPD &= IIf(IsDBNull(DgFactProv.CurrentRow.Cells("LineTotal").Value), 0, DgFactProv.CurrentRow.Cells("LineTotal").Value).ToString
                    SQLTPD &= ","
                    SQLTPD &= "@Fchent" 'DgFactProv.CurrentRow.Cells("Fchent").Value
                    SQLTPD &= ",'"
                    SQLTPD &= ""
                    SQLTPD &= "',"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Diasent").Value
                    SQLTPD &= ",'"
                    SQLTPD &= IIf(IsDBNull(DgFactProv.CurrentRow.Cells("Garantia").Value), "", DgFactProv.CurrentRow.Cells("Garantia").Value).ToString
                    SQLTPD &= "',' "
                    SQLTPD &= ""
                    SQLTPD &= "','"
                    SQLTPD &= ""
                    SQLTPD &= "',"
                    SQLTPD &= "@Fchrec" 'DgFactProv.CurrentRow.Cells("Fchrec").Value
                    SQLTPD &= ",'"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Telefono").Value
                    SQLTPD &= "','"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Contacto").Value
                    SQLTPD &= "','"
                    SQLTPD &= ""
                    SQLTPD &= "',"
                    SQLTPD &= DgFactProv.CurrentRow.Cells("Idfletera").Value
                    SQLTPD &= ",'"
                    SQLTPD &= UsrTPM
                    SQLTPD &= "',"
                    SQLTPD &= "@Fchcap" 'DgFactProv.CurrentRow.Cells("Fchcap").Value
                    SQLTPD &= ",'"
                    SQLTPD &= UsrTPM
                    SQLTPD &= "',"
                    SQLTPD &= "@Fchact" 'DgFactProv.CurrentRow.Cells("Fchact").Value
                    SQLTPD &= ","
                    SQLTPD &= "0"
                    SQLTPD &= ","
                    SQLTPD &= "0"
                    SQLTPD &= ")"
                End If


                'Try
                Dim CmdWom As Data.SqlClient.SqlCommand

                CmdWom = New Data.SqlClient.SqlCommand()
                With CmdWom

                    .Parameters.AddWithValue("@Fchfact", DgFactProv.CurrentRow.Cells("Fchfact").Value)
                    .Parameters.AddWithValue("@Fchcap", DateTime.Now)
                    .Parameters.AddWithValue("@Fchact", DateTime.Now)
                    .Parameters.AddWithValue("@Fchemb", DgFactProv.CurrentRow.Cells("Fchemb").Value)
                    If i + 1 = DgFactProv.CurrentRow.Cells("Flete").Value Then
                        .Parameters.AddWithValue("@Fchent", DgFactProv.CurrentRow.Cells("Fchent").Value)
                        .Parameters.AddWithValue("@Fchrec", DgFactProv.CurrentRow.Cells("Fchrec").Value)
                    Else
                        .Parameters.AddWithValue("@Fchent", "1900/01/01")
                        .Parameters.AddWithValue("@Fchrec", "1900/01/01")
                    End If
                    '.Parameters.AddWithValue("@Fecha", DateTime.Now)
                    .Connection = New Data.SqlClient.SqlConnection(StrTpm)
                    .Connection.Open()
                    .CommandText = SQLTPD
                    .ExecuteNonQuery()
                    .Connection.Close()
                End With
                'Catch exSql As SqlClient.SqlException
                '    MessageBox.Show("INSTRUCCION SQL AL MODIFICAR DATOS" + exSql.Message.ToString, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Catch ex As Exception
                '    MessageBox.Show("AL MODIFICAR DATOS" + Convert.ToString(ex), " E R R O R ! ! !", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Finally
                'End Try

            Next

        End If

    End Sub

    Sub DiasHab()
        While vFin <> vIni
            vIni = vIni.AddDays(1)
            If Not (vIni.DayOfWeek = DayOfWeek.Sunday Or vIni.DayOfWeek = DayOfWeek.Saturday) Then
                vCant = vCant + 1
            End If
        End While

        If vFchComp.DayOfWeek = DayOfWeek.Friday Then
            vFchComp = vFchComp.AddDays(3)

        ElseIf vFchComp.DayOfWeek = DayOfWeek.Saturday Then
            vFchComp = vFchComp.AddDays(2)
        Else
            vFchComp = vFchComp.AddDays(1)
        End If

        If vFin > vFchComp Then
            vGarantia = "RECLAMAR"
        End If

    End Sub

    Private Sub DgFactProv_CancelRowEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.QuestionEventArgs) Handles DgFactProv.CancelRowEdit
        vActvalor = 0
    End Sub

    Private Sub ChkComsitex_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        VerEntregados()
    End Sub

    Private Sub ChkExcCortesia_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkExcCortesia.CheckedChanged
        VerEntregados()
    End Sub
End Class