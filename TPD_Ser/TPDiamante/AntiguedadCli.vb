Imports System.Data.SqlClient

Public Class AntiguedadCli

    Dim DvArticulo As New DataView
    Dim DvClte As New DataView
    Dim DvCobranza As New DataView
    Dim DvAgte As New DataView
    Dim DvBO As New DataView

    Dim DVdgagte As New DataView
    Dim DVdgclte As New DataView
    Dim DVAntCli As New DataView
    Dim DVDetCli As New DataView

    Private Sub AntiguedadCli_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBox1.Text = 30
        TextBox1.BackColor = Color.YellowGreen
        TextBox1.TextAlign = HorizontalAlignment.Center

        TextBox2.Text = 60
        TextBox2.BackColor = Color.Yellow
        TextBox2.TextAlign = HorizontalAlignment.Center

        TextBox3.Text = 90
        TextBox3.BackColor = Color.LightSalmon
        TextBox3.TextAlign = HorizontalAlignment.Center

        TextBox4.Text = 120
        TextBox4.BackColor = Color.Orange
        TextBox4.TextAlign = HorizontalAlignment.Center



        Dim ConsutaLista As String

        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)

            mllenaComboAlmacen(SqlConnection)

            Dim DSetTablas As New DataSet

            '''''LLENA CBCobranza*******************************

            ConsutaLista = "SELECT T0.IdCobranza,T0.Nombre,T1.GroupCode FROM TPM.dbo.DEPCOB T0 LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.IdCobranza=T1.IdCobranza "
            ConsutaLista &= "GROUP BY T0.IdCobranza,T0.Nombre,T1.GroupCode "
            'ConsutaLista &= "FROM SBO_TPD.dbo.OCRD WHERE CardType = 'C' ORDER BY CardName "


            Dim daCobranza As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            daCobranza.Fill(DSetTablas, "Cobranza")

            Dim filaCobranza As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaCobranza = DSetTablas.Tables("Cobranza").NewRow

            'Agregamos los valores a los campos de la tabla
            filaCobranza("Nombre") = "TODOS"
            filaCobranza("IdCobranza") = "TODOS"
            'filaClientes("slpcode") = 999
            filaCobranza("groupcode") = "999"

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Cobranza").Rows.Add(filaCobranza)

            DvCobranza.Table = DSetTablas.Tables("Cobranza")

            Me.CBCobranza.DataSource = DvCobranza
            Me.CBCobranza.DisplayMember = "Nombre"
            Me.CBCobranza.ValueMember = "IdCobranza"
            Me.CBCobranza.SelectedValue = "TODOS"

            '''''FIN LLENA CBCobranza*******************************

            'MODIFICADO POR IVAN GONZALEZ SE QUITO COBRANZ2
            If UsrTPM = "COBRANZ4" Or UsrTPM = "COBRANZ5" Then

                cmbAlmacen.Enabled = False

                CBCobranza.SelectedValue = UsrTPM
                CBCobranza.Enabled = False

                ConsutaLista = "SELECT T0.slpcode,T0.slpname,  "
                ConsutaLista &= "CASE WHEN T1.GroupCode IS NULL THEN 104 ELSE T1.GroupCode END "
                ConsutaLista &= "AS 'GroupCode',T1.IdCobranza FROM OSLP T0 "
                ConsutaLista &= "LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
                ConsutaLista &= "WHERE (T1.IdCobranza = '" & UsrTPM & "') and (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC')"
                ConsutaLista &= "ORDER BY slpname "

            ElseIf UsrTPM = "NGOMEZ" Then

                CBCobranza.Enabled = False

                CBCobranza.SelectedValue = "COBRANZ6"

                cmbAlmacen.Enabled = False

                ConsutaLista = "SELECT T0.slpcode,T0.slpname,  "
                ConsutaLista &= "CASE WHEN T1.GroupCode IS NULL THEN 104 ELSE T1.GroupCode END "
                ConsutaLista &= "AS 'GroupCode',T1.IdCobranza FROM OSLP T0 "
                ConsutaLista &= "LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
                ConsutaLista &= "WHERE (T1.IdCobranza = 'COBRANZ6') and (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC') "
                ConsutaLista &= "ORDER BY slpname "

            ElseIf UsrTPM = "VVERGARA" Then

                cmbAlmacen.Enabled = False
                DvCobranza.RowFilter = "GROUPCODE = 103 or groupcode=999 "

                ConsutaLista = "SELECT T0.slpcode,T0.slpname,  "
                ConsutaLista &= "CASE WHEN T1.GroupCode IS NULL THEN 104 ELSE T1.GroupCode END "
                ConsutaLista &= "AS 'GroupCode',T1.IdCobranza FROM OSLP T0 "
                ConsutaLista &= "LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
                ConsutaLista &= "WHERE (T1.GroupCode = '103') AND (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC') "
                ConsutaLista &= "ORDER BY slpname "


            ElseIf UsrTPM = "RROBLES" Then

                cmbAlmacen.Enabled = False
                DvCobranza.RowFilter = "GROUPCODE = 102 or groupcode=999 "

                ConsutaLista = "SELECT T0.slpcode,T0.slpname,  "
                ConsutaLista &= "CASE WHEN T1.GroupCode IS NULL THEN 104 ELSE T1.GroupCode END "
                ConsutaLista &= "AS 'GroupCode',T1.IdCobranza FROM OSLP T0 "
                ConsutaLista &= "LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
                ConsutaLista &= "WHERE (T1.GroupCode = '102') AND (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC')  "
                ConsutaLista &= "ORDER BY slpname "

            Else
                ConsutaLista = "SELECT T0.slpcode,T0.slpname,  "
                ConsutaLista &= "CASE WHEN T1.GroupCode IS NULL THEN 104 ELSE T1.GroupCode END "
                ConsutaLista &= "AS 'GroupCode',T1.IdCobranza FROM OSLP T0 "
                ConsutaLista &= "LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode = T1.SlpCode "
                ConsutaLista &= "WHERE (T1.CbrGralAdicional = 'N' "
                ConsutaLista &= "OR T0.SlpCode = -1) and (T0.U_ESTATUS = 'ACTIVO' OR T0.U_ESTATUS = 'INACTIVOCC')  ORDER BY slpname "

            End If

            Dim daAgte As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)


            daAgte.Fill(DSetTablas, "Agentes")


            'If UsrTPM <> "COBRANZ2" And UsrTPM <> "COBRANZ4" And UsrTPM <> "COBRANZ5" And UsrTPM <> "NGOMEZ" Then
            Dim filaAgte As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaAgte = DSetTablas.Tables("Agentes").NewRow


            'Agregamos los valores a los campos de la tabla
            filaAgte("slpname") = "TODOS"
            filaAgte("slpcode") = 999
            filaAgte("GroupCode") = 999
            filaAgte("IdCobranza") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Agentes").Rows.Add(filaAgte)

            DvAgte.Table = DSetTablas.Tables("Agentes")

            Me.CmbAgteVta.DataSource = DvAgte
            Me.CmbAgteVta.DisplayMember = "slpname"
            Me.CmbAgteVta.ValueMember = "slpcode"
            Me.CmbAgteVta.SelectedValue = 999

            'Else
            'DvAgte.Table = DSetTablas.Tables("Agentes")

            'Me.CmbAgteVta.DataSource = DvAgte
            'Me.CmbAgteVta.DisplayMember = "slpname"
            'Me.CmbAgteVta.ValueMember = "slpcode"
            'Me.CmbAgteVta.SelectedValue = "999"
            'End If


            ''''---------------------------------

            ConsutaLista = "SELECT T0.CardCode,T0.CardName,T0.SlpCode,T0.GroupCode,T1.IdCobranza "
            ConsutaLista &= "FROM SBO_TPD.dbo.OCRD T0 "
            ConsutaLista &= "LEFT JOIN TPM.dbo.DEPCOBR T1 ON T0.SlpCode=T1.SlpCode "
            ConsutaLista &= "WHERE T0.CardType = 'C' ORDER BY CardName "

            Dim daClientes As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

            daClientes.Fill(DSetTablas, "Clientes")

            Dim filaClientes As Data.DataRow

            'Asignamos a fila la nueva Row(Fila)del Dataset
            filaClientes = DSetTablas.Tables("Clientes").NewRow

            'Agregamos los valores a los campos de la tabla
            filaClientes("CardName") = "TODOS"
            filaClientes("CardCode") = "TODOS"
            filaClientes("slpcode") = 999
            filaClientes("groupcode") = 999
            filaClientes("IdCobranza") = 999

            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
            DSetTablas.Tables("Clientes").Rows.Add(filaClientes)

            DvClte.Table = DSetTablas.Tables("Clientes")

            Me.CmbCliente.DataSource = DvClte
            Me.CmbCliente.DisplayMember = "CardName"
            Me.CmbCliente.ValueMember = "CardCode"
            Me.CmbCliente.SelectedValue = "TODOS"


            '-----------------------------------------------------
        End Using
    End Sub


    Private Sub mllenaComboAlmacen(ByVal conexion As SqlConnection)
        Try
            Dim da As New SqlDataAdapter("SELECT GroupCode , GroupName " +
                                         "FROM OCRG with (nolock) " +
                                         "WHERE GroupType = 'C' ORDER BY Groupcode ", conexion)

            Dim ds As New DataSet
            da.Fill(ds)
            ds.Tables(0).Rows.Add(0, "TODOS")
            Me.cmbAlmacen.DataSource = ds.Tables(0)
            Me.cmbAlmacen.DisplayMember = "GroupName"
            Me.cmbAlmacen.ValueMember = "GroupCode"

            'MODIFICADO POR IVAN GONZALEZ SE AGREGO COBRANZ2
            If UsrTPM = "MANAGER" Or UsrTPM = "PRUEBAS" Or UsrTPM = "COBRANZ3" Or UsrTPM = "COBRANZ2" Or UsrTPM = "DDORANTES" Then

                Me.cmbAlmacen.SelectedValue = 0

            Else

                If AlmTPM = "01" Then
                    Me.cmbAlmacen.SelectedValue = "100"
                ElseIf AlmTPM = "03" Then
                    Me.cmbAlmacen.SelectedValue = "102"
                ElseIf AlmTPM = "07" Then
                    Me.cmbAlmacen.SelectedValue = "103"
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub


    ''CUANDO CAMBIE DE ALMACEN BUSCARA COBRANZAS
    Private Sub BuscaAgteCobranza()
        Try
            If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then
                DvCobranza.RowFilter = String.Empty
                DvAgte.RowFilter = String.Empty
                DvClte.RowFilter = String.Empty

                Me.CBCobranza.SelectedValue = "TODOS"
                Me.CmbAgteVta.SelectedValue = "999"
                Me.CmbCliente.SelectedValue = "TODOS"

            Else

                DvCobranza.RowFilter = "GroupCode='" & cmbAlmacen.SelectedValue.ToString & "' OR GroupCode ='999' "
                DvAgte.RowFilter = "groupcode ='" & cmbAlmacen.SelectedValue.ToString & "' OR groupcode ='999' "
                DvClte.RowFilter = "groupcode ='" & cmbAlmacen.SelectedValue.ToString & "' OR groupcode ='999' "

                Me.CBCobranza.SelectedValue = "TODOS"
                Me.CmbAgteVta.SelectedValue = "999"
                Me.CmbCliente.SelectedValue = "TODOS"

            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    ''CUANDO CAMBIE DE AGENTE COBRANZA BUSCARA AGENTE VENTAS
    Private Sub BuscaAgteVentas()
        Try
            If CBCobranza.SelectedValue Is Nothing Or CBCobranza.Text = "TODOS" Then
                If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then
                    DvAgte.RowFilter = String.Empty
                    DvClte.RowFilter = String.Empty

                    Me.CmbAgteVta.SelectedValue = "999"
                    Me.CmbCliente.SelectedValue = "TODOS"

                Else
                    DvAgte.RowFilter = "groupcode ='" & cmbAlmacen.SelectedValue.ToString & "' OR groupcode ='999' "
                    DvClte.RowFilter = "groupcode ='" & cmbAlmacen.SelectedValue.ToString & "' OR groupcode ='999' "

                    Me.CmbAgteVta.SelectedValue = "999"
                    Me.CmbCliente.SelectedValue = "TODOS"

                End If

            Else
                DvAgte.RowFilter = "IdCobranza ='" & CBCobranza.SelectedValue.ToString & "' OR IdCobranza ='999' "
                DvClte.RowFilter = "IdCobranza ='" & CBCobranza.SelectedValue.ToString & "' OR IdCobranza ='999' "

                    Me.CmbAgteVta.SelectedValue = "999"
                    Me.CmbCliente.SelectedValue = "TODOS"
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub


    ''CUANDO CAMBIE DE AGENTE VENTAS BUSCARA CLIENTES
    Sub BuscaClientes()
        Try
            If CmbAgteVta.SelectedValue Is Nothing Or CmbAgteVta.Text = "TODOS" Then
                If cmbAlmacen.SelectedValue Is Nothing Or cmbAlmacen.Text = "TODOS" Then
                    If CBCobranza.SelectedValue Is Nothing Or CBCobranza.Text = "TODOS" Then
                        DvClte.RowFilter = String.Empty
                        Me.CmbCliente.SelectedValue = "TODOS"
                    Else
                        DvClte.RowFilter = "idcobranza ='" & CBCobranza.SelectedValue.ToString & "' OR idcobranza ='999' "
                        Me.CmbCliente.SelectedValue = "TODOS"
                    End If

                Else
                    If CBCobranza.SelectedValue Is Nothing Or CBCobranza.Text = "TODOS" Then
                        DvClte.RowFilter = "groupcode ='" & cmbAlmacen.SelectedValue.ToString & "' OR groupcode ='999' "
                        Me.CmbCliente.SelectedValue = "TODOS"
                    Else
                        DvClte.RowFilter = "idcobranza ='" & CBCobranza.SelectedValue.ToString & "' OR idcobranza ='999' "
                        Me.CmbCliente.SelectedValue = "TODOS"
                    End If
                End If

            Else
                DvClte.RowFilter = "slpcode =" & CmbAgteVta.SelectedValue.ToString & " OR slpcode =999 "

                Me.CmbCliente.SelectedValue = "TODOS"
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub cmbAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlmacen.SelectedIndexChanged
        BuscaAgteCobranza()
    End Sub

    Private Sub CmbAgteVta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAgteVta.SelectedIndexChanged
        BuscaClientes()
    End Sub

    Private Sub CBCobranza_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBCobranza.SelectedIndexChanged
        BuscaAgteVentas()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If IsNothing(CmbAgteVta.SelectedValue) Then
            MessageBox.Show("Seleccione un agente", _
            "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbAgteVta.Focus()
            Return
        End If

        DGClientes.Columns.Clear() 'LIMPIA DE RESULTADOS ANTERIORES

        Dim Consulta As String = ""
        Dim strcadena As String = ""
        Dim CTabla As String = ""
        Dim DTMObra As New DataTable
        Dim DTProb As New DataTable
        Dim vAlm As Integer = 0

        Consulta &= "SELECT T0.CardCode, T0.CardName,  "
        Consulta &= "CASE WHEN T1.TransType = 13 THEN 'Factura' "
        Consulta &= "WHEN T1.TransType = 14 THEN 'Nota de credito' "
        Consulta &= "WHEN T1.TransType = 24 THEN 'Pago' "
        Consulta &= "ELSE 'Otros' "
        Consulta &= "END AS 'TipoDoc', "
        Consulta &= "T4.BaseRef 'Doc Interno', T1.RefDate, T1.DueDate, "
        Consulta &= "CASE "
        Consulta &= "WHEN T3.DebHab = 'D' THEN (T1.Debit-T1.Credit-T3.ReconSum) "
        Consulta &= "WHEN T3.DebHab = 'C' THEN (T1.Debit-T1.Credit+T3.ReconSum) "
        Consulta &= "ELSE (T1.Debit-T1.Credit) "
        Consulta &= "End 'Saldo', "
        Consulta &= "CASE "
        Consulta &= "WHEN (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 <= 0 AND T3.DebHab = 'D'  then T1.Debit-T1.Credit-T3.ReconSum "
        Consulta &= "WHEN (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 <= 0 AND T3.DebHab = 'C'  then T1.Debit-T1.Credit+T3.ReconSum "
        Consulta &= "WHEN (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 <= 0 then (T1.Debit-T1.Credit) end 'Abono futuro', "
        Consulta &= "CASE "
        Consulta &= "WHEN ((DATEDIFF(DD,T1.DueDate,current_timestamp))+1 > 0 AND (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 <= @Par1) and T3.DebHab = 'D'  then T1.Debit-T1.Credit-T3.ReconSum "
        Consulta &= "WHEN ((DATEDIFF(DD,T1.DueDate,current_timestamp))+1 > 0 AND (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 <= @Par1) and T3.DebHab = 'C'  then T1.Debit-T1.Credit+T3.ReconSum "
        Consulta &= "WHEN ((DATEDIFF(DD,T1.DueDate,current_timestamp))+1 > 0 AND (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 <= @Par1) then (T1.Debit-T1.Credit) end '0-30 dias', "
        Consulta &= "CASE "
        Consulta &= "WHEN (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 > @Par1 AND (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 <= @Par2 and T3.DebHab = 'D'  then T1.Debit-T1.Credit-T3.ReconSum "
        Consulta &= "WHEN (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 > @Par1 AND (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 <= @Par2 and T3.DebHab = 'C'  then T1.Debit-T1.Credit+T3.ReconSum "
        Consulta &= "WHEN (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 > @Par1 AND (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 <= @Par2 then (T1.Debit-T1.Credit) end '31-60 dias', "
        Consulta &= "CASE "
        Consulta &= "WHEN ((datediff(dd,T1.DueDate,current_timestamp))+1 > @Par2 and (datediff(dd,T1.DueDate,current_timestamp))+1 <= @Par3) and T3.DebHab = 'D'  then T1.Debit-T1.Credit-T3.ReconSum "
        Consulta &= "WHEN ((datediff(dd,T1.DueDate,current_timestamp))+1 > @Par2 and (datediff(dd,T1.DueDate,current_timestamp))+1 <= @Par3) and T3.DebHab = 'C'  then T1.Debit-T1.Credit+T3.ReconSum "
        Consulta &= "WHEN ((datediff(dd,T1.DueDate,current_timestamp))+1 > @Par2 and (datediff(dd,T1.DueDate,current_timestamp))+1 <= @Par3) then (T1.Debit-T1.Credit) end '61-90 dias', "
        Consulta &= "CASE "
        Consulta &= "WHEN ((datediff(dd,T1.DueDate,current_timestamp))+1 > @Par3 and (datediff(dd,T1.DueDate,current_timestamp))+1 <= @Par4) and T3.DebHab = 'D'  then T1.Debit-T1.Credit-T3.ReconSum "
        Consulta &= "WHEN ((datediff(dd,T1.DueDate,current_timestamp))+1 > @Par3 and (datediff(dd,T1.DueDate,current_timestamp))+1 <= @Par4) and T3.DebHab = 'C'  then T1.Debit-T1.Credit+T3.ReconSum "
        Consulta &= "WHEN ((datediff(dd,T1.DueDate,current_timestamp))+1 > @Par3 and (datediff(dd,T1.DueDate,current_timestamp))+1 <= @Par4) then (T1.Debit-T1.Credit) end '91-120 dias', "
        Consulta &= "CASE "
        Consulta &= "WHEN (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 > @Par4 and T3.DebHab = 'D'  then T1.Debit-T1.Credit-T3.ReconSum "
        Consulta &= "WHEN (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 > @Par4 and T3.DebHab = 'C'  then T1.Debit-T1.Credit+T3.ReconSum "
        Consulta &= "WHEN (DATEDIFF(DD,T1.DueDate,current_timestamp))+1 > @Par4 then (T1.Debit-T1.Credit) end '+120 dias', "
        Consulta &= "CASE "
        Consulta &= "WHEN (DATEDIFF(DD,T1.RefDate,current_timestamp))+1 BETWEEN 121 AND 365 and T3.DebHab = 'D'  then (T1.Debit-T1.Credit-T3.ReconSum)*0.75 "
        Consulta &= "WHEN (DATEDIFF(DD,T1.RefDate,current_timestamp))+1 BETWEEN 121 AND 365 and T3.DebHab = 'C'  then (T1.Debit-T1.Credit+T3.ReconSum)*0.75 "
        Consulta &= "WHEN (DATEDIFF(DD,T1.RefDate,current_timestamp))+1 BETWEEN 121 AND 365 then (T1.Debit-T1.Credit)*0.75 "
        Consulta &= "WHEN (DATEDIFF(DD,T1.RefDate,current_timestamp))+1 > 365 and T3.DebHab = 'D'  then (T1.Debit-T1.Credit-T3.ReconSum) "
        Consulta &= "WHEN (DATEDIFF(DD,T1.RefDate,current_timestamp))+1 > 365 and T3.DebHab = 'C'  then (T1.Debit-T1.Credit+T3.ReconSum) "
        Consulta &= "WHEN (DATEDIFF(DD,T1.RefDate,current_timestamp))+1 > 365 then (T1.Debit-T1.Credit) end 'Deuda Dudosa', "
        Consulta &= "Case T1.TransType "
        Consulta &= "WHEN '13' THEN (SELECT Y.Comments FROM OINV Y WHERE Y.TransId = T1.TransId) "
        Consulta &= "WHEN '14' THEN (SELECT Y.Comments FROM ORIN Y WHERE Y.TransId = T1.TransId) "
        Consulta &= "ELSE T1.LineMemo "
        Consulta &= "End 'Comentarios',Y3.SlpName,Y3.SLPCODE ,COB.IdCobranza "    ',COB.IdCobranza 
        Consulta &= "INTO #AntCli "
        Consulta &= "FROM dbo.OCRD T0 "
        Consulta &= "INNER JOIN dbo.JDT1 T1 ON T1.ShortName = T0.CardCode "
        Consulta &= "INNER JOIN dbo.OACT T2 ON T2.AcctCode = T1.Account "
        Consulta &= "INNER JOIN dbo.OJDT T4 ON T4.TransId = T1.TransId "
        Consulta &= "LEFT JOIN dbo.OINV Y1 ON Y1.TransId = T1.TransId "
        Consulta &= "LEFT JOIN dbo.ORIN Y2 ON Y2.TransId = T1.TransId "
        Consulta &= "LEFT JOIN dbo.OSLP Y3 ON Y3.SlpCode = Y1.SlpCode OR Y3.SlpCode = Y2.SlpCode "
        Consulta &= "LEFT JOIN TPM.dbo.DEPCOBR COB ON T0.SlpCode = COB.SlpCode AND COB.CbrGralAdicional='N' "
        Consulta &= "LEFT JOIN (SELECT X0.ShortName 'SN', X0.TransId 'TransId', SUM(X0.ReconSum)'ReconSum', X0.IsCredit 'DebHab', X0.TransRowId 'Linea' "
        Consulta &= "FROM dbo.ITR1 X0 "
        Consulta &= "INNER JOIN dbo.OITR X1 ON X1.ReconNum = X0.ReconNum "
        Consulta &= "WHERE X1.ReconDate <= CURRENT_TIMESTAMP AND X1.CancelAbs = '' "
        Consulta &= "GROUP BY X0.ShortName, X0.TransId, X0.IsCredit, X0.TransRowId) T3 ON T3.TransId = T1.TransId AND T3.SN = T1.ShortName AND T3.Linea = T1.Line_ID "
        Consulta &= "WHERE T0.CardType = 'C' AND T1.RefDate <= CURRENT_TIMESTAMP AND "
        Consulta &= "(CASE "
        Consulta &= "WHEN T3.DebHab = 'D' THEN (T1.Debit-T1.Credit-T3.ReconSum) "
        Consulta &= "WHEN T3.DebHab = 'C' THEN (T1.Debit-T1.Credit+T3.ReconSum) "
        Consulta &= "ELSE (T1.Debit-T1.Credit) "
        Consulta &= "END) != '0' "

        If CmbCliente.Text <> "TODOS" Then
            Consulta &= "AND T0.CardCode = '" & CmbCliente.SelectedValue & "'"

        Else    ' SI CmbCliente = TODOS

            'Else
            If CBCobranza.SelectedValue <> "TODOS" Then
                Consulta &= " AND IdCobranza ='" & CBCobranza.SelectedValue & "'"
            End If

            If CmbAgteVta.SelectedValue <> 999 Then
                Consulta &= "AND Y3.Slpcode = " & CmbAgteVta.SelectedValue

            Else    ' SI CmbCliente = TODOS y CmbAgente =TODOS

                'If cmbAlmacen.SelectedValue = 100 Then
                '    Consulta &= "AND Y3.Slpcode <> 17 AND Y3.Slpcode <> 20 AND Y3.Slpcode <> 8 AND Y3.Slpcode <> 12 AND Y3.Slpcode <> 26 "
                'ElseIf cmbAlmacen.SelectedValue = 102 Then
                '    Consulta &= "AND (Y3.Slpcode = 17 OR Y3.Slpcode = 20) "
                'ElseIf cmbAlmacen.SelectedValue = 103 Then
                '    Consulta &= "AND (Y3.Slpcode = 8 OR Y3.Slpcode = 12 OR Y3.Slpcode = 26) "

                'End If

            End If

        End If


        Consulta &= "ORDER BY '+120 dias' DESC,'91-120 dias' DESC,'61-90 dias' DESC, "
        Consulta &= "'31-60 dias' DESC,'0-30 dias' DESC,[Abono futuro] DESC "


        Consulta &= "SELECT T0.CardCode,T0.CardName,SUM(T0.Saldo) AS 'Saldo',SUM([Abono futuro]) AS 'AbonoFut', "
        Consulta &= "SUM([0-30 dias]) AS '0-30',SUM([31-60 dias]) AS '31-60',SUM([61-90 dias]) AS '61-90', "
        Consulta &= "SUM([91-120 dias]) AS '91-120',SUM([+120 dias]) AS '+120', 0 as orden,T0.IdCobranza "    ', T0.IdCobranza
        Consulta &= "FROM #AntCli T0 "

        'If cmbAlmacen.SelectedValue = 100 Then
        '    Consulta &= "WHERE Slpcode <> 17 AND Y3.Slpcode <> 20 AND Y3.Slpcode <> 8 AND Y3.Slpcode <> 12 AND Y3.Slpcode <> 26 "
        'ElseIf cmbAlmacen.SelectedValue = 102 Then
        '    Consulta &= "WHERE (Slpcode = 17 OR Slpcode = 20) "
        'ElseIf cmbAlmacen.SelectedValue = 103 Then
        '    Consulta &= "WHERE (Slpcode = 8 OR Slpcode = 12 OR Slpcode = 26) "
        'End If

        If cmbAlmacen.SelectedValue = 100 Then
            Consulta &= "WHERE T0.IdCobranza='cobranz2' OR T0.IdCobranza='cobranz4' "
        ElseIf cmbAlmacen.SelectedValue = 102 Then
            Consulta &= "WHERE T0.IdCobranza='cobranz5' "
        ElseIf cmbAlmacen.SelectedValue = 103 Then
            Consulta &= "WHERE T0.IdCobranza='cobranz6' "
        End If

        Consulta &= "GROUP BY T0.CardCode,T0.CardName,T0.IdCobranza "

        'Consulta &= "UNION ALL "
        'Consulta &= "SELECT convert(varchar(5),0),'Montos Totales',SUM(Saldo) AS 'Saldo',SUM([Abono futuro]) AS 'AbonoFut', "
        'Consulta &= "SUM([0-30 dias]) AS '0-30',SUM([31-60 dias]) AS '31-60',SUM([61-90 dias]) AS '61-90', "
        'Consulta &= "SUM([91-120 dias]) AS '91-120',SUM([+120 dias]) AS '+120', 1 as orden "
        'Consulta &= "FROM #AntCli "
        Consulta &= "ORDER BY  "
        Consulta &= "[+120] DESC,[91-120] DESC,[61-90] DESC, "
        Consulta &= "[31-60] DESC,[0-30] DESC,[Abonofut] DESC "


        Consulta &= "SELECT * FROM #AntCli "
        Consulta &= "ORDER BY DueDate,"
        Consulta &= "[+120 dias] DESC,[91-120 dias] DESC,[61-90 dias] DESC, "
        Consulta &= "[31-60 dias] DESC,[0-30 dias] DESC,[Abono futuro] DESC "



        Consulta &= "DROP TABLE #AntCli "


        Dim CmdMObra As New SqlClient.SqlCommand(Consulta)

        CmdMObra.Parameters.Add("@Par1", SqlDbType.Int)
        CmdMObra.Parameters("@Par1").Value = TextBox1.Text

        CmdMObra.Parameters.Add("@Par2", SqlDbType.Int)
        CmdMObra.Parameters("@Par2").Value = TextBox2.Text

        CmdMObra.Parameters.Add("@Par3", SqlDbType.Int)
        CmdMObra.Parameters("@Par3").Value = TextBox3.Text

        CmdMObra.Parameters.Add("@Par4", SqlDbType.Int)
        CmdMObra.Parameters("@Par4").Value = TextBox4.Text

        Dim DsVtasDet As New DataSet

        'DTMObra.TableName = "DetBO"

        DsVtasDet.Tables.Add(DTMObra)

        CmdMObra.Connection = New SqlClient.SqlConnection(StrCon)
        CmdMObra.Connection.Open()

        Dim AdapMObra As New SqlClient.SqlDataAdapter(CmdMObra)

        AdapMObra.SelectCommand = CmdMObra
        AdapMObra.Fill(DsVtasDet, "Antiguedad")

        DsVtasDet.Tables(1).TableName = "Clientes"
        DsVtasDet.Tables(2).TableName = "Detalle"


        Dim DtAntCli As New DataTable
        DtAntCli = DsVtasDet.Tables("Clientes")

        Dim DtClientes As New DataTable
        DtClientes = DsVtasDet.Tables("Detalle")


        DVAntCli.Table = DtAntCli
        DGClientes.DataSource = DVAntCli

        DVdgclte.Table = DtClientes
        DGClientes.DataSource = DVAntCli

        DVDetCli.Table = DtClientes
        DGDetalle.DataSource = DVDetCli

        DiseñoClientes()
        DiseñoDetalle()

        Dim totsaldo As Decimal
        Dim totabono As Decimal
        Dim totdias0 As Decimal
        Dim totdias31 As Decimal
        Dim totdias61 As Decimal
        Dim totdias91 As Decimal
        Dim totdias121 As Decimal

        'Dim porsaldo As Decimal
        'Dim porabono As Decimal
        'Dim pordias0 As Decimal
        'Dim pordias31 As Decimal
        'Dim pordias61 As Decimal
        'Dim pordias91 As Decimal
        'Dim pordias121 As Decimal

        For i As Integer = 0 To DGClientes.RowCount - 1
            totsaldo = totsaldo + IIf(DGClientes.Item(2, i).Value Is DBNull.Value, 0, DGClientes.Item(2, i).Value)
            totabono = totabono + IIf(DGClientes.Item(3, i).Value Is DBNull.Value, 0, DGClientes.Item(3, i).Value)
            totdias0 = totdias0 + IIf(DGClientes.Item(4, i).Value Is DBNull.Value, 0, DGClientes.Item(4, i).Value)
            totdias31 = totdias31 + IIf(DGClientes.Item(5, i).Value Is DBNull.Value, 0, DGClientes.Item(5, i).Value)
            totdias61 = totdias61 + IIf(DGClientes.Item(6, i).Value Is DBNull.Value, 0, DGClientes.Item(6, i).Value)
            totdias91 = totdias91 + IIf(DGClientes.Item(7, i).Value Is DBNull.Value, 0, DGClientes.Item(7, i).Value)
            totdias121 = totdias121 + IIf(DGClientes.Item(8, i).Value Is DBNull.Value, 0, DGClientes.Item(8, i).Value)

            'porsaldo = (porsaldo + IIf(DGClientes.Item(2, i).Value Is DBNull.Value, 0, DGClientes.Item(2, i).Value / 100))
            'porabono = (porabono + IIf(DGClientes.Item(3, i).Value Is DBNull.Value, 0, DGClientes.Item(3, i).Value / 100))
            'pordias0 = (pordias0 + IIf(DGClientes.Item(4, i).Value Is DBNull.Value, 0, DGClientes.Item(4, i).Value / 100))
            'pordias31 = (pordias31 + IIf(DGClientes.Item(5, i).Value Is DBNull.Value, 0, DGClientes.Item(5, i).Value / 100))
            'pordias61 = (pordias61 + IIf(DGClientes.Item(6, i).Value Is DBNull.Value, 0, DGClientes.Item(6, i).Value / 100))
            'pordias91 = (pordias91 + IIf(DGClientes.Item(7, i).Value Is DBNull.Value, 0, DGClientes.Item(7, i).Value / 100))
            'pordias121 = (pordias121 + IIf(DGClientes.Item(8, i).Value Is DBNull.Value, 0, DGClientes.Item(8, i).Value / 100))

        Next

        saldo.Visible = True
        abono.Visible = True
        dias0.Visible = True
        dias31.Visible = True
        dias61.Visible = True
        dias91.Visible = True
        dias121.Visible = True
        Montos.Visible = True

        '****MONTOS TOTALES
        saldo.Text = totsaldo.ToString("C2")
        saldo.TextAlign = ContentAlignment.MiddleRight
        saldo.BackColor = Color.Gainsboro

        abono.Text = totabono.ToString("C")
        abono.TextAlign = ContentAlignment.MiddleRight
        abono.BackColor = Color.Gainsboro

        dias0.Text = totdias0.ToString("C")
        dias0.TextAlign = ContentAlignment.MiddleRight
        dias0.BackColor = Color.Gainsboro

        dias31.Text = totdias31.ToString("C")
        dias31.TextAlign = ContentAlignment.MiddleRight
        dias31.BackColor = Color.Gainsboro

        dias61.Text = totdias61.ToString("C")
        dias61.TextAlign = ContentAlignment.MiddleRight
        dias61.BackColor = Color.Gainsboro

        dias91.Text = totdias91.ToString("C")
        dias91.TextAlign = ContentAlignment.MiddleRight
        dias91.BackColor = Color.Gainsboro

        dias121.Text = totdias121.ToString("C")
        dias121.TextAlign = ContentAlignment.MiddleRight
        dias121.BackColor = Color.Gainsboro

        Montos.BackColor = Color.Gainsboro
        'Montos.Font=
        ' FIN MONTOS TOTALES


        '**********PORCENTAJES
        Dim TOT As Decimal = saldo.Text

        l10.Text = "100.00 %"
        l10.TextAlign = ContentAlignment.MiddleRight

        If TOT = 0 Then
            l11.Text = Format(0, "0.00%")
        Else
            l11.Text = Format(totabono / TOT, "0.00%")
        End If
        l11.TextAlign = ContentAlignment.MiddleRight


        If TOT = 0 Then
            l12.Text = Format(0, "0.00%")
        Else
            l12.Text = Format(totdias0 / TOT, "0.00%")
        End If
        l12.TextAlign = ContentAlignment.MiddleRight

        If TOT = 0 Then
            l13.Text = Format(0, "0.00%")
        Else
            l13.Text = Format(totdias31 / TOT, "0.00%")
        End If
        l13.TextAlign = ContentAlignment.MiddleRight

        If TOT = 0 Then
            l14.Text = Format(0, "0.00%")
        Else
            l14.Text = Format(totdias61 / TOT, "0.00%")
        End If
        l14.TextAlign = ContentAlignment.MiddleRight


        If TOT = 0 Then
            l15.Text = Format(0, "0.00%")
        Else
            l15.Text = Format(totdias91 / TOT, "0.00%")
        End If
        l15.TextAlign = ContentAlignment.MiddleRight


        If TOT = 0 Then
            l16.Text = Format(0, "0.00%")
        Else
            l16.Text = Format(totdias121 / TOT, "0.00%")
        End If
        l16.TextAlign = ContentAlignment.MiddleRight

        l10.Visible = True
        l11.Visible = True
        l12.Visible = True
        l13.Visible = True
        l14.Visible = True
        l15.Visible = True
        l16.Visible = True

        l10.BackColor = Color.Gainsboro
        l11.BackColor = Color.Gainsboro
        l12.BackColor = Color.Gainsboro
        l13.BackColor = Color.Gainsboro
        l14.BackColor = Color.Gainsboro
        l15.BackColor = Color.Gainsboro
        l16.BackColor = Color.Gainsboro

        FiltraDetalle()

    End Sub

    Private Sub DiseñoClientes()
        Try

            With Me.DGClientes
                '.DataSource = DTMObra
                .ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .RowHeadersWidth = 20

                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                '.RowHeadersVisible = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = True
                .AllowUserToAddRows = False

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .ColumnHeadersHeight = 100


                .Columns(0).HeaderText = "Código"
                .Columns(0).Width = 75
                .Columns(0).Frozen = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '.Columns(1).ba
                .Columns(1).HeaderText = "Cliente"
                .Columns(1).Width = 445
                .Columns(1).Frozen = True
                '.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(2).HeaderText = "Saldo"
                .Columns(2).Width = 90
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(2).DefaultCellStyle.Format = "$ #,###,##0.#0"

                .Columns(3).HeaderText = "Abono futuro"
                .Columns(3).Width = 90
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(3).DefaultCellStyle.Format = "$ #,###,##0.#0"


                .Columns(4).HeaderText = "0 - " & TextBox1.Text
                .Columns(4).Width = 90
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(4).DefaultCellStyle.Format = "$ #,###,##0.#0"

                Dim header_style As New DataGridViewCellStyle
                header_style.BackColor = Color.GreenYellow
                .Font = New Font("Microsoft Sans Serif", 10)
                .Columns(4).HeaderCell.Style = header_style


                .Columns(5).HeaderText = TextBox1.Text + 1 & " - " & TextBox2.Text
                .Columns(5).Width = 90
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(5).DefaultCellStyle.Format = "$ #,###,##0.#0"

                Dim header_style2 As New DataGridViewCellStyle
                header_style2.BackColor = Color.Yellow
                'header_style2.ForeColor = Color.White
                .Columns(5).HeaderCell.Style = header_style2


                .Columns(6).HeaderText = TextBox2.Text + 1 & " - " & TextBox3.Text
                .Columns(6).Width = 90
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Format = "$ #,###,##0.#0"

                Dim header_style3 As New DataGridViewCellStyle
                header_style3.BackColor = Color.LightSalmon
                'header_style2.ForeColor = Color.White
                .Columns(6).HeaderCell.Style = header_style3


                .Columns(7).HeaderText = TextBox3.Text + 1 & " - " & TextBox4.Text
                .Columns(7).Width = 90
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).DefaultCellStyle.Format = "$ #,###,##0.#0"

                Dim header_style4 As New DataGridViewCellStyle
                header_style4.BackColor = Color.Orange
                'header_style2.ForeColor = Color.White
                .Columns(7).HeaderCell.Style = header_style4

                .Columns(8).HeaderText = TextBox4.Text + 1 & " +"
                .Columns(8).Width = 90
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).DefaultCellStyle.Format = "$ #,###,##0.#0"

                Dim header_style5 As New DataGridViewCellStyle
                header_style5.BackColor = Color.Red
                'header_style2.ForeColor = Color.White
                .Columns(8).HeaderCell.Style = header_style5


                .Columns(9).HeaderText = "Orden"
                .Columns(9).Visible = False

                '.Columns(10).HeaderText = "SlpCode"
                '.Columns(10).Visible = Falsec

                .Columns(10).HeaderText = "IdCobranz"
                .Columns(10).Visible = False

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DiseñoDetalle()
        Try

            With Me.DGDetalle
                '.DataSource = DTMObra
                .ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .RowHeadersWidth = 20
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                '.RowHeadersVisible = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = True
                .AllowUserToAddRows = False

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(0).HeaderText = "Código"
                .Columns(0).Width = 50
                .Columns(0).Frozen = True
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(1).HeaderText = "Cliente"
                .Columns(1).Width = 160
                .Columns(1).Frozen = True

                .Columns(2).HeaderText = "Tipo documento"
                .Columns(2).Width = 70
                '.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(3).HeaderText = "Docto. Sap"
                .Columns(3).Width = 70
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(4).HeaderText = "Fecha de contabilización"
                .Columns(4).Width = 85
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(5).HeaderText = "Fecha de vencimiento"
                .Columns(5).Width = 85
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(6).HeaderText = "Saldo"
                .Columns(6).Width = 90
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(7).HeaderText = "Abono futuro"
                .Columns(7).Width = 90
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                .Columns(8).HeaderText = "0 - " & TextBox1.Text
                .Columns(8).Width = 90
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                Dim header_style As New DataGridViewCellStyle
                header_style.BackColor = Color.GreenYellow
                'header_style2.ForeColor = Color.White
                .Columns(8).HeaderCell.Style = header_style
                .Font = New Font("Microsoft Sans Serif", 10)

                .Columns(9).HeaderText = TextBox1.Text + 1 & " - " & TextBox2.Text
                .Columns(9).Width = 90
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                Dim header_style1 As New DataGridViewCellStyle
                header_style1.BackColor = Color.Yellow
                'header_style2.ForeColor = Color.White
                .Columns(9).HeaderCell.Style = header_style1

                .Columns(10).HeaderText = TextBox2.Text + 1 & " - " & TextBox3.Text
                .Columns(10).Width = 90
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                Dim header_style2 As New DataGridViewCellStyle
                header_style2.BackColor = Color.LightSalmon
                'header_style2.ForeColor = Color.White
                .Columns(10).HeaderCell.Style = header_style2

                .Columns(11).HeaderText = TextBox3.Text + 1 & " - " & TextBox4.Text
                .Columns(11).Width = 90
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(11).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                Dim header_style3 As New DataGridViewCellStyle
                header_style3.BackColor = Color.Orange
                'header_style2.ForeColor = Color.White
                .Columns(11).HeaderCell.Style = header_style3

                .Columns(12).HeaderText = TextBox4.Text + 1 & " + "
                .Columns(12).Width = 90
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(12).DefaultCellStyle.Format = "$ ###,###,##0.#0"

                Dim header_style4 As New DataGridViewCellStyle
                header_style4.BackColor = Color.Red
                'header_style2.ForeColor = Color.White
                .Columns(12).HeaderCell.Style = header_style4

                .Columns(13).HeaderText = "Deuda dudosa"
                .Columns(13).Visible = False

                .Columns(14).HeaderText = "Comentarios"
                .Columns(14).Visible = False

                .Columns(15).HeaderText = "Agente"
                .Columns(15).Width = 120


                .Columns(16).HeaderText = "SlpCode"
                .Columns(16).Visible = False

                .Columns(17).HeaderText = "IdCobranz"
                .Columns(17).Visible = False

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub FiltraDetalle()
        Try

            If DGClientes.Item(1, DGClientes.CurrentRow.Index).Value.ToString = "Montos Totales" Then
                DVDetCli.RowFilter = String.Empty
            Else
                DVDetCli.RowFilter = "cardcode ='" & DGClientes.Item(0, DGClientes.CurrentRow.Index).Value.ToString & "'"
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub DGClientes_CurrentCellChanged(sender As Object, e As EventArgs) Handles DGClientes.CurrentCellChanged
        FiltraDetalle()
    End Sub

    Private Sub BtnClientes_Click(sender As Object, e As EventArgs) Handles BtnClientes.Click
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


        'DAR COLOR DE FONDO A CELDAS
        oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
        oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15
        oSheet.Range("A3:C3").INTERIOR.COLORINDEX = 15
        oSheet.Range("A4:C4").INTERIOR.COLORINDEX = 15

        'oSheet.Range("A5").INTERIOR.COLORINDEX = 15
        'oSheet.Range("B5").INTERIOR.COLORINDEX = 15
        'oSheet.Range("C5").INTERIOR.COLORINDEX = 15
        'oSheet.Range("D5").INTERIOR.COLORINDEX = 15
        oSheet.Range("E6").INTERIOR.COLORINDEX = 43
        oSheet.Range("F6").INTERIOR.COLORINDEX = 6
        oSheet.Range("G6").INTERIOR.COLORINDEX = 45
        oSheet.Range("H6").INTERIOR.COLORINDEX = 46
        oSheet.Range("I6").INTERIOR.COLORINDEX = 3


        'Declaramos el nombre de las columnas
        oSheet.range("A6").value = "Código"
        oSheet.range("B6").value = "Nombre Cliente"
        oSheet.range("C6").value = "Saldo"
        oSheet.range("D6").value = "Abono Futuro"
        oSheet.range("E6").value = "0 - 30"
        oSheet.range("F6").value = "31 - 60"
        oSheet.range("G6").value = "61 - 90"
        oSheet.range("H6").value = "91 - 120"
        oSheet.range("I6").value = "121 +"


        'DISEÑO DE EXCEL

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A5:I5").font.bold = True


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


        'Cambia el alto de celda 
        oSheet.range("A:I").RowHeight = 13

        'oSheet.range("A:V").HorizontalAlignment = xlCenter

        'TAMAÑO DE COLUMNAS
        oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 10
        oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 25
        oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 15
        oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 15
        oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 15
        oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 15
        oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 15
        oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 15
        oExcel.worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 15

        'oExcel.Worksheets("Hoja1").Columns("C").NumberFormat = "$ ###,###,###.##"
        'oExcel.Worksheets("Hoja1").Columns("D").NumberFormat = "$ ###,###,###.##"
        'oExcel.Worksheets("Hoja1").Columns("E").NumberFormat = "$ ###,###,###.##"
        'oExcel.Worksheets("Hoja1").Columns("F").NumberFormat = "$ ###,###,###.##"
        'oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "$ ###,###,###.##"
        'oExcel.Worksheets("Hoja1").Columns("H").NumberFormat = "$ ###,###,###.##"
        'oExcel.Worksheets("Hoja1").Columns("I").NumberFormat = "$ ###,###,###.##"



        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 0
        Dim tanto_porcentaje As String = ""
        Dim marikona As Integer = 0

        Dim total_reg As Integer = 0

        total_reg = Me.DGClientes.RowCount
        For fila_dt = 0 To total_reg - 1

            'para leer una celda en concreto
            'el numero es la columna
            Dim cel1 As String = Me.DGClientes.Item(0, fila_dt).Value
            Dim cel2 As String = Me.DGClientes.Item(1, fila_dt).Value
            Dim cel3 As String = IIf(IsDBNull(Me.DGClientes.Item(2, fila_dt).Value), 0, Me.DGClientes.Item(2, fila_dt).Value)
            Dim cel4 As String = IIf(IsDBNull(Me.DGClientes.Item(3, fila_dt).Value), 0, Me.DGClientes.Item(3, fila_dt).Value)
            Dim cel5 As String = IIf(IsDBNull(Me.DGClientes.Item(4, fila_dt).Value), 0, Me.DGClientes.Item(4, fila_dt).Value)
            Dim cel6 As String = IIf(IsDBNull(Me.DGClientes.Item(5, fila_dt).Value), 0, Me.DGClientes.Item(5, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DGClientes.Item(6, fila_dt).Value), 0, Me.DGClientes.Item(6, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DGClientes.Item(7, fila_dt).Value), 0, Me.DGClientes.Item(7, fila_dt).Value)
            Dim cel9 As String = IIf(IsDBNull(Me.DGClientes.Item(8, fila_dt).Value), 0, Me.DGClientes.Item(8, fila_dt).Value)

            fila_dt_excel = fila_dt + 7 'Renglón en donde se empieza a registrar el reporte

            'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
            oSheet.range("A" & fila_dt_excel).value = cel1
            oSheet.range("B" & fila_dt_excel).value = cel2
            oSheet.range("C" & fila_dt_excel).value = "$ " + cel3 'Da formato número con dos decimales a la columna C
            oSheet.range("D" & fila_dt_excel).value = "$ " + cel4
            oSheet.range("E" & fila_dt_excel).value = "$ " + cel5
            oSheet.range("F" & fila_dt_excel).value = "$ " + cel6
            oSheet.range("G" & fila_dt_excel).value = "$ " + cel7
            oSheet.range("H" & fila_dt_excel).value = "$ " + cel8
            oSheet.range("I" & fila_dt_excel).value = "$ " + cel9

        Next

        oSheet.range("B" & fila_dt_excel + 1).value = Montos.Text
        oSheet.range("C" & fila_dt_excel + 1).value = saldo.Text
        oSheet.range("D" & fila_dt_excel + 1).value = abono.Text
        oSheet.range("E" & fila_dt_excel + 1).value = dias0.Text
        oSheet.range("F" & fila_dt_excel + 1).value = dias31.Text
        oSheet.range("G" & fila_dt_excel + 1).value = dias61.Text
        oSheet.range("H" & fila_dt_excel + 1).value = dias91.Text
        oSheet.range("I" & fila_dt_excel + 1).value = dias121.Text

        'oSheet.range("B" & fila_dt_excel + 2).value = l10.Text
        oSheet.range("C" & fila_dt_excel + 2).value = l10.Text
        oSheet.range("D" & fila_dt_excel + 2).value = l11.Text
        oSheet.range("E" & fila_dt_excel + 2).value = l12.Text
        oSheet.range("F" & fila_dt_excel + 2).value = l13.Text
        oSheet.range("G" & fila_dt_excel + 2).value = l14.Text
        oSheet.range("H" & fila_dt_excel + 2).value = l15.Text
        oSheet.range("I" & fila_dt_excel + 2).value = l16.Text

        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        'oSheet.columns("A:O").entirecolumn.autofit()
        oSheet.range("A1").value = "Informe de Antiguedad de saldos de negocios"
        oSheet.range("A2").value = "Sucursal - " + cmbAlmacen.Text
        oSheet.range("A3").value = "Agente de Ventas - " + CmbAgteVta.Text
        oSheet.range("A4").value = "Cliente - " + CmbCliente.Text

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
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


        'DAR COLOR DE FONDO A CELDAS
        oSheet.Range("A1:C1").INTERIOR.COLORINDEX = 15
        oSheet.Range("A2:C2").INTERIOR.COLORINDEX = 15
        oSheet.Range("A3:C3").INTERIOR.COLORINDEX = 15
        oSheet.Range("A4:C4").INTERIOR.COLORINDEX = 15

        'oSheet.Range("A5").INTERIOR.COLORINDEX = 15
        'oSheet.Range("B5").INTERIOR.COLORINDEX = 15
        'oSheet.Range("C5").INTERIOR.COLORINDEX = 15
        'oSheet.Range("D5").INTERIOR.COLORINDEX = 15
        oSheet.Range("I6").INTERIOR.COLORINDEX = 43
        oSheet.Range("J6").INTERIOR.COLORINDEX = 6
        oSheet.Range("K6").INTERIOR.COLORINDEX = 45
        oSheet.Range("L6").INTERIOR.COLORINDEX = 46
        oSheet.Range("M6").INTERIOR.COLORINDEX = 3


        'Declaramos el nombre de las columnas
        oSheet.range("A6").value = "Código"
        oSheet.range("B6").value = "Nombre Cliente"
        oSheet.range("C6").value = "Tipo documento"
        oSheet.range("D6").value = "Docto. SAP"
        oSheet.range("E6").value = "Fecha de contabilizacion"
        oSheet.range("F6").value = "Fecha de vencimiento"
        oSheet.range("G6").value = "Saldo"
        oSheet.range("H6").value = "Abono futuro"
        oSheet.range("I6").value = "0 - 30"
        oSheet.range("J6").value = "31 - 60"
        oSheet.range("K6").value = "61 - 90"
        oSheet.range("L6").value = "91 - 120"
        oSheet.range("M6").value = "121 +"
        oSheet.range("N6").value = "Agente"

        'DISEÑO DE EXCEL

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A5:N5").font.bold = True


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
        oExcel.worksheets("Hoja1").Columns("K").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("K").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("L").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("L").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("M").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("M").Font.Size = 8
        oExcel.worksheets("Hoja1").Columns("N").Font.Name = "ARIAL" 'Pone tipo de letra courier al font 
        oExcel.worksheets("Hoja1").Columns("N").Font.Size = 8

        'Cambia el alto de celda 
        oSheet.range("A:N").RowHeight = 13

        'oSheet.range("A:V").HorizontalAlignment = xlCenter

        'TAMAÑO DE COLUMNAS
        oExcel.worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 8
        oExcel.worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 25
        oExcel.worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 7
        oExcel.worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 13
        oExcel.worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 13
        oExcel.worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("L").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("M").EntireColumn.ColumnWidth = 11
        oExcel.worksheets("Hoja1").Columns("N").EntireColumn.ColumnWidth = 15

        oExcel.Worksheets("Hoja1").Columns("G").NumberFormat = "$ ###,###,###.##"
        oExcel.Worksheets("Hoja1").Columns("H").NumberFormat = "$ ###,###,###.##"
        oExcel.Worksheets("Hoja1").Columns("I").NumberFormat = "$ ###,###,###.##"
        oExcel.Worksheets("Hoja1").Columns("J").NumberFormat = "$ ###,###,###.##"
        oExcel.Worksheets("Hoja1").Columns("K").NumberFormat = "$ ###,###,###.##"
        oExcel.Worksheets("Hoja1").Columns("L").NumberFormat = "$ ###,###,###.##"
        oExcel.Worksheets("Hoja1").Columns("M").NumberFormat = "$ ###,###,###.##"



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
            Dim cel3 As String = Me.DGDetalle.Item(2, fila_dt).Value
            Dim cel4 As String = Me.DGDetalle.Item(3, fila_dt).Value
            Dim cel5 As Date = Me.DGDetalle.Item(4, fila_dt).Value
            Dim cel6 As Date = IIf(IsDBNull(Me.DGDetalle.Item(5, fila_dt).Value), 0, Me.DGDetalle.Item(5, fila_dt).Value)
            Dim cel7 As String = IIf(IsDBNull(Me.DGDetalle.Item(6, fila_dt).Value), 0, Me.DGDetalle.Item(6, fila_dt).Value)
            Dim cel8 As String = IIf(IsDBNull(Me.DGDetalle.Item(7, fila_dt).Value), 0, Me.DGDetalle.Item(7, fila_dt).Value)
            Dim cel9 As String = IIf(IsDBNull(Me.DGDetalle.Item(8, fila_dt).Value), 0, Me.DGDetalle.Item(8, fila_dt).Value)
            Dim cel10 As String = IIf(IsDBNull(Me.DGDetalle.Item(9, fila_dt).Value), 0, Me.DGDetalle.Item(9, fila_dt).Value)
            Dim cel11 As String = IIf(IsDBNull(Me.DGDetalle.Item(10, fila_dt).Value), 0, Me.DGDetalle.Item(10, fila_dt).Value)
            Dim cel12 As String = IIf(IsDBNull(Me.DGDetalle.Item(11, fila_dt).Value), 0, Me.DGDetalle.Item(11, fila_dt).Value)
            Dim cel13 As String = IIf(IsDBNull(Me.DGDetalle.Item(12, fila_dt).Value), 0, Me.DGDetalle.Item(12, fila_dt).Value)
            Dim cel14 As String = IIf(IsDBNull(Me.DGDetalle.Item(15, fila_dt).Value), 0, Me.DGDetalle.Item(15, fila_dt).Value)

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
            oSheet.range("K" & fila_dt_excel).value = cel11
            oSheet.range("L" & fila_dt_excel).value = cel12
            oSheet.range("M" & fila_dt_excel).value = cel13
            oSheet.range("N" & fila_dt_excel).value = cel14


        Next


        ' para que el tamano de la columna tenga como minimo el maximo de sus textos
        'oSheet.columns("A:O").entirecolumn.autofit()
        oSheet.range("A1").value = "Informe de Antiguedad de saldos de negocios DETALLE"
        oSheet.range("A2").value = "Sucursal - " + cmbAlmacen.Text
        oSheet.range("A3").value = "Agente de Ventas - " + CmbAgteVta.Text
        oSheet.range("A4").value = "Cliente - " + DGDetalle.Item(1, DGDetalle.CurrentRow.Index).Value
        'Try

        'Catch ex As Exception
        '    'oSheet.range("A4").value = "Cliente - " + DGDetalle.Item(1, DGDetalle.Cur
        'End Try

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing
    End Sub

End Class