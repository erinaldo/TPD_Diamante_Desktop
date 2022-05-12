Public Class frmLimiteClientes

    ''VARIABES GLOBALES
    Dim SQL As New Comandos_SQL()

    Private Sub frmLimiteClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SQL.conectarSBO_TPD()
    End Sub

    Sub Buscar()
        'CONSULTA SIN CLIENTE
        Dim tbLimiteCliente As New DataTable
        Dim ConsultaSQL As String = ""

        If dgwLimiteCliente.Rows.Count <> 0 Then
            dt = CType(Me.dgwLimiteCliente.DataSource, DataTable)
            dt.Rows.Clear()
        End If

        'QUERY PARA SACAR EL TOTAL DE LAS FACTURAS POR CADA CLIENTE
        ConsultaSQL = "/*FACTURAS*/ " +
                        " SELECT " +
                        "    TF.CardCode AS 'CardCode', " +
                        "    TF.CardName AS 'CardName', " +
                        "    SUM(TF.DocTotal) / 1.16 AS 'DocTotal', " +
                        "    SUM(TF.DocTotal) AS 'CIVA' " +
                        "    INTO #LIMITE_FACTURAS " +
                        " FROM " +
                        "    SBO_TPD.dbo.OINV TF " +
                        " WHERE " +
                        "    TF.DocDate BETWEEN '" + dtpFechaInicio.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpFechaFinal.Value.ToString("yyyy-MM-dd") + "' " +
                        "    AND TF.DocType <> 'S' " +
                        " GROUP BY " +
                        "    TF.CardCode, " +
                        "    TF.CardName "

    ConsultaSQL += "/*NOTAS DE CREDITO*/ " +
                       " SELECT DISTINCT  " +
                       "    TNC.CardCode, " +
                       "    TNC.CardName, " +
                       "    TNC.DocTotal / 1.16 AS 'DocTotal', " +
                       "    TNC.DocTotal AS 'CIVA' " +
                       "    INTO #LIMINTE_NC " +
                       " FROM  " +
                       "    SBO_TPD.dbo.ORIN TNC " +
                       "    LEFT JOIN SBO_TPD.dbo.RIN1 TNCL ON TNCL.DocEntry = TNC.DocEntry " +
                       "    INNER JOIN SBO_TPD.dbo.OITM TAR ON TAR.ItemCode = TNCL.ItemCode " +
                       " WHERE   " +
                       "    TNC.DocDate BETWEEN '" + dtpFechaInicio.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpFechaFinal.Value.ToString("yyyy-MM-dd") + "' " +
                       "    AND TNC.DocType <> 'S' " +
                       "    AND TAR.ItmsGrpCod = 200 " +
                       "    AND ((TNC.U_BXP_TIMBRADO = 'T' OR TNC.U_BXP_TIMBRADO = 'P') OR TNC.EDocGenTyp = 'G') "

    ConsultaSQL += " /*TABLA CON LAS CANCELADAS*/ " +
                        " SELECT DISTINCT  " +
                        "    TNC.CardCode, " +
                        "    TNC.CardName, " +
                        "    TNC.DocTotal / 1.16 AS 'DocTotal', " +
                        "    TNC.DocTotal AS 'CIVA' " +
                        "    INTO #LIMINTE_NC_CANCELED " +
                        " FROM  " +
                        "    SBO_TPD.dbo.ORIN TNC " +
                        "    LEFT JOIN SBO_TPD.dbo.RIN1 TNCL ON TNCL.DocEntry = TNC.DocEntry " +
                        "    INNER JOIN SBO_TPD.dbo.OITM TAR ON TAR.ItemCode = TNCL.ItemCode " +
                        "    INNER JOIN SBO_TPD.dbo.ECM2 t3 on TNC.DocEntry = t3.SrcObjAbs and t3.SrcObjType = 14 " +
                        " WHERE   " +
                        "    TNC.DocDate BETWEEN '" + dtpFechaInicio.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpFechaFinal.Value.ToString("yyyy-MM-dd") + "' " +
                        "    AND TNC.DocType <> 'S' " +
                        "    AND TNC.U_BXP_TIMBRADO <> 'T' " +
                        "    AND t3.ReportID IS NULL "

    ConsultaSQL += "/*TABLA CON TODOS LOS DATOS*/ " +
                                "SELECT " +
                                "   TC.CardCode AS 'Cliente', " +
                                "   TC.CardName AS 'Nombre', " +
                                "   CASE WHEN (SELECT SUM(TNC.DocTotal) FROM #LIMINTE_NC TNC WHERE TNC.CardCode = TC.CardCode) IS NULL AND (SELECT SUM(TNCC.DocTotal) FROM #LIMINTE_NC_CANCELED TNCC WHERE TNCC.CardCode = TC.CardCode) IS NULL THEN (TF.DocTotal) ELSE " +
                                "   CASE WHEN (SELECT SUM(TNC.DocTotal) FROM #LIMINTE_NC TNC WHERE TNC.CardCode = TC.CardCode) IS NULL AND (SELECT SUM(TNCC.DocTotal) FROM #LIMINTE_NC_CANCELED TNCC WHERE TNCC.CardCode = TC.CardCode) IS NOT NULL THEN (TF.DocTotal) - (SELECT SUM(TNCC.DocTotal) FROM #LIMINTE_NC_CANCELED TNCC WHERE TNCC.CardCode = TC.CardCode) ELSE " +
                                "   CASE WHEN (SELECT SUM(TNC.DocTotal) FROM #LIMINTE_NC TNC WHERE TNC.CardCode = TC.CardCode) IS NOT NULL AND (SELECT SUM(TNCC.DocTotal) FROM #LIMINTE_NC_CANCELED TNCC WHERE TNCC.CardCode = TC.CardCode) IS NULL THEN (TF.DocTotal) - (SELECT SUM(TNC.DocTotal) FROM #LIMINTE_NC TNC WHERE TNC.CardCode = TC.CardCode) ELSE (TF.DocTotal) - (SELECT SUM(TNC.DocTotal) FROM #LIMINTE_NC TNC WHERE TNC.CardCode = TC.CardCode) - (SELECT SUM(TNCC.DocTotal) FROM #LIMINTE_NC_CANCELED TNCC WHERE TNCC.CardCode = TC.CardCode) END END END AS 'Compra_Anual', " +
                                "   CASE WHEN (SELECT SUM(TNC.CIVA) FROM #LIMINTE_NC TNC WHERE TNC.CardCode = TC.CardCode) IS NULL AND (SELECT SUM(TNCC.CIVA) FROM #LIMINTE_NC_CANCELED TNCC WHERE TNCC.CardCode = TC.CardCode) IS NULL THEN TF.CIVA ELSE " +
                                "   CASE WHEN (SELECT SUM(TNC.CIVA) FROM #LIMINTE_NC TNC WHERE TNC.CardCode = TC.CardCode) IS NULL AND (SELECT SUM(TNCC.CIVA) FROM #LIMINTE_NC_CANCELED TNCC WHERE TNCC.CardCode = TC.CardCode) IS NOT NULL THEN TF.CIVA - (SELECT SUM(TNCC.CIVA) FROM #LIMINTE_NC_CANCELED TNCC WHERE TNCC.CardCode = TC.CardCode) ELSE " +
                                "   CASE WHEN (SELECT SUM(TNC.CIVA) FROM #LIMINTE_NC TNC WHERE TNC.CardCode = TC.CardCode) IS NOT NULL AND (SELECT SUM(TNCC.CIVA) FROM #LIMINTE_NC_CANCELED TNCC WHERE TNCC.CardCode = TC.CardCode) IS NULL THEN TF.CIVA - (SELECT SUM(TNC.CIVA) FROM #LIMINTE_NC TNC WHERE TNC.CardCode = TC.CardCode) ELSE TF.CIVA - (SELECT SUM(TNC.CIVA) FROM #LIMINTE_NC TNC WHERE TNC.CardCode = TC.CardCode) - (SELECT SUM(TNCC.CIVA) FROM #LIMINTE_NC_CANCELED TNCC WHERE TNCC.CardCode = TC.CardCode) END END END AS 'C/IVA', " +
                                "   '1' AS 'Calificacion', " +
                                "   CONVERT(INT,CONVERT(INT,REPLACE(SUBSTRING(TDC.PymntGroup, 0, 3),' ',''))) AS 'Dias_Credito', " +
                                "   IIF(DATEDIFF(YEAR,CONVERT(DATE,TC.CreateDate),GETDATE()) >= 3,1,0) AS 'Antiguedad', " +
                                "   '' AS 'Promedio de pago por mes', " +
                                "   '' AS 'sobre giro del promedio compra mensual', " +
                                "   '' AS 'Límite (suma de promedio CM + Sobregiro de CM = Nuevo Límite', " +
                                "   '' AS 'Nuevo factores sobre giro mensual por calif', " +
                                "   '' AS 'Nuevo saldo sobre giro mensual por antiguedad', " +
                                "   '' AS 'Total del saldo (nuevo limite + nuevo sobregiro)', " +
                                "   TA.SlpName AS 'Agente', " +
                                "   TLP.ListName AS 'Lista precios' " +
                                "   INTO #LimiteClienteCredito " +
                                "FROM " +
                                "   SBO_TPD.dbo.OCRD TC " +
                                "   LEFT JOIN #LIMITE_FACTURAS TF ON TC.CardCode = TF.CardCode " +
                                "   LEFT JOIN SBO_TPD.dbo.OCTG TDC ON TDC.GroupNum = TC.GroupNum " +
                                "   LEFT JOIN SBO_TPD.dbo.OSLP TA ON TA.SlpCode = TC.SlpCode " +
                                "   LEFT JOIN SBO_TPD.dbo.OPLN TLP ON TLP.ListNum = TC.ListNum " +
                                "WHERE " +
                                "   TDC.PymntGroup != 'Contado' AND TDC.PymntGroup != 'Credito Cancelado' " +
                                "GROUP BY " +
                                "   TC.CardCode, " +
                                "   TC.CardName, " +
                                "   TF.DocTotal, " +
                                "   TF.CIVA, " +
                                "   TDC.PymntGroup, " +
                                "   TC.CreateDate, " +
                                "   TA.SlpName, " +
                                "   TLP.ListName " +
                                " ORDER BY " +
                                "   REPLACE(REPLACE(TC.CardCode,'-',''),'C','') " +
                                "   SELECT * FROM #LimiteClienteCredito WHERE Compra_Anual IS NOT NULL " +
                                "DROP TABLE #LIMINTE_NC DROP TABLE #LIMITE_FACTURAS DROP TABLE #LIMINTE_NC_CANCELED DROP TABLE #LimiteClienteCredito "
        Try
            tbLimiteCliente = SQL.ConsultarTabla(ConsultaSQL)
        Catch ex As Exception
            MessageBox.Show("Erro: " + ex.ToString(), "¡Error en consultar tabla!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        dgwLimiteCliente.DataSource = tbLimiteCliente

        'VARIABLES PARA LAS OPERACIONES
        Dim TotalDias As String = SQL.CampoEspecifico("SELECT DATEDIFF(day, '" + dtpFechaInicio.Value.ToString("yyyy-MM-dd") + "', '" + dtpFechaFinal.Value.ToString("yyyy-MM-dd") + "') + 1 as 'DIAS';", "DIAS")
        Dim TotalMeses As String = SQL.CampoEspecifico("SELECT DATEDIFF(MONTH, '" + dtpFechaInicio.Value.ToString("yyyy-MM-dd") + "', '" + dtpFechaFinal.Value.ToString("yyyy-MM-dd") + "') + 1 as 'MESES';", "MESES")


        For Each fila As DataGridViewRow In dgwLimiteCliente.Rows

            'ANEXAR LAS NOTAS DE CREDITO
            Dim Compra_Anual As Decimal = fila.Cells("Compra_Anual").Value
            Dim C_IVA As Decimal = fila.Cells("C/IVA").Value

            'REASIGNAR CON LOS NUEVOS VALORES
            fila.Cells("Promedio de pago por mes").Value = Format(Decimal.Parse(fila.Cells("C/IVA").Value) / Decimal.Parse(TotalMeses), "##,##0.00")
            fila.Cells("sobre giro del promedio compra mensual").Value = Format(Decimal.Parse(fila.Cells("Dias_Credito").Value) * Decimal.Parse(fila.Cells("Promedio de pago por mes").Value) / Decimal.Parse(TotalDias), "##,##0.00")
            fila.Cells("Límite (suma de promedio CM + Sobregiro de CM = Nuevo Límite").Value = Decimal.Parse(fila.Cells("Promedio de pago por mes").Value) + Decimal.Parse(fila.Cells("sobre giro del promedio compra mensual").Value)
            If fila.Cells("Calificacion").Value = "1" Then
                fila.Cells("Nuevo factores sobre giro mensual por calif").Value = Format(Decimal.Parse(fila.Cells("sobre giro del promedio compra mensual").Value) * 0.5, "##,##0.00")
            ElseIf fila.Cells("Calificacion").Value = "2" Then
                fila.Cells("Nuevo factores sobre giro mensual por calif").Value = Format(Decimal.Parse(fila.Cells("sobre giro del promedio compra mensual").Value) * 0.4, "##,##0.00")
            ElseIf fila.Cells("Calificacion").Value = "3" Then
                fila.Cells("Nuevo factores sobre giro mensual por calif").Value = Format(Decimal.Parse(fila.Cells("sobre giro del promedio compra mensual").Value) * 0.3, "##,##0.00")
            End If
            If fila.Cells("Antiguedad").Value = "1" Then
                fila.Cells("Nuevo saldo sobre giro mensual por antiguedad").Value = Format(Decimal.Parse(fila.Cells("Nuevo factores sobre giro mensual por calif").Value) * 0.3, "##,##0.00")
            Else
                fila.Cells("Nuevo saldo sobre giro mensual por antiguedad").Value = Format(Decimal.Parse(fila.Cells("Nuevo factores sobre giro mensual por calif").Value) * 0, "##,##0.00")
            End If
            fila.Cells("Total del saldo (nuevo limite + nuevo sobregiro)").Value = Format(Decimal.Parse(fila.Cells("Límite (suma de promedio CM + Sobregiro de CM = Nuevo Límite").Value) + Decimal.Parse(fila.Cells("Nuevo factores sobre giro mensual por calif").Value) + Decimal.Parse(fila.Cells("Nuevo saldo sobre giro mensual por antiguedad").Value), "##,##0.00")


            If fila.Cells("Antiguedad").Value = 1 Then
                fila.DefaultCellStyle.BackColor = Color.LightGreen
            Else
                fila.DefaultCellStyle.BackColor = Color.LightSalmon
            End If

            Dim column As DataGridViewColumn = dgwLimiteCliente.Columns("Compra_Anual")
            column.DefaultCellStyle.Format = "C2"

            Dim column1 As DataGridViewColumn = dgwLimiteCliente.Columns("C/IVA")
            column1.DefaultCellStyle.Format = "C2"

            Dim column2 As DataGridViewColumn = dgwLimiteCliente.Columns("Promedio de pago por mes")
            column2.DefaultCellStyle.Format = "C2"

            Dim column3 As DataGridViewColumn = dgwLimiteCliente.Columns("sobre giro del promedio compra mensual")
            column3.DefaultCellStyle.Format = "C2"

            Dim column4 As DataGridViewColumn = dgwLimiteCliente.Columns("Límite (suma de promedio CM + Sobregiro de CM = Nuevo Límite")
            column4.DefaultCellStyle.Format = "C2"

            Dim column5 As DataGridViewColumn = dgwLimiteCliente.Columns("Nuevo factores sobre giro mensual por calif")
            column5.DefaultCellStyle.Format = "C2"

            Dim column6 As DataGridViewColumn = dgwLimiteCliente.Columns("Nuevo saldo sobre giro mensual por antiguedad")
            column6.DefaultCellStyle.Format = "C2"

            Dim column7 As DataGridViewColumn = dgwLimiteCliente.Columns("Total del saldo (nuevo limite + nuevo sobregiro)")
            column7.DefaultCellStyle.Format = "C2"

        Next

        If dgwLimiteCliente.Rows.Count > 0 Then
            Dim column As DataGridViewColumn = dgwLimiteCliente.Columns("Compra_Anual")
            column.DefaultCellStyle.Format = "C2"

            Dim column1 As DataGridViewColumn = dgwLimiteCliente.Columns("C/IVA")
            column1.DefaultCellStyle.Format = "C2"

            Dim column2 As DataGridViewColumn = dgwLimiteCliente.Columns("Promedio de pago por mes")
            column2.DefaultCellStyle.Format = "C2"

            Dim column3 As DataGridViewColumn = dgwLimiteCliente.Columns("sobre giro del promedio compra mensual")
            column3.DefaultCellStyle.Format = "C2"

            Dim column4 As DataGridViewColumn = dgwLimiteCliente.Columns("Límite (suma de promedio CM + Sobregiro de CM = Nuevo Límite")
            column4.DefaultCellStyle.Format = "C2"

            Dim column5 As DataGridViewColumn = dgwLimiteCliente.Columns("Nuevo factores sobre giro mensual por calif")
            column5.DefaultCellStyle.Format = "C2"

            Dim column6 As DataGridViewColumn = dgwLimiteCliente.Columns("Nuevo saldo sobre giro mensual por antiguedad")
            column6.DefaultCellStyle.Format = "C2"

            Dim column7 As DataGridViewColumn = dgwLimiteCliente.Columns("Total del saldo (nuevo limite + nuevo sobregiro)")
            column7.DefaultCellStyle.Format = "C2"
        End If

        dgwLimiteCliente.Columns(0).ReadOnly = True
        dgwLimiteCliente.Columns(1).ReadOnly = True
        dgwLimiteCliente.Columns(2).ReadOnly = True
        dgwLimiteCliente.Columns(3).ReadOnly = True
        'dgwLimiteCliente.Columns(4).ReadOnly = True
        dgwLimiteCliente.Columns(5).ReadOnly = True
        dgwLimiteCliente.Columns(6).ReadOnly = True
        dgwLimiteCliente.Columns(7).ReadOnly = True
        dgwLimiteCliente.Columns(8).ReadOnly = True
        dgwLimiteCliente.Columns(9).ReadOnly = True
        dgwLimiteCliente.Columns(10).ReadOnly = True
        dgwLimiteCliente.Columns(11).ReadOnly = True
        dgwLimiteCliente.Columns(12).ReadOnly = True
        dgwLimiteCliente.Columns(13).ReadOnly = True
        dgwLimiteCliente.Columns(14).ReadOnly = True

    End Sub

    Private Sub frmLimiteClientes_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SQL.Cerrar()
    End Sub

    Private Sub btBuscar_Click(sender As Object, e As EventArgs) Handles btBuscar.Click
        Buscar()
    End Sub

    Sub Generar_Excel()
        Dim oExcel As Object
        Dim oBook As Object
        Dim oSheet As Object

        'Abrimos un nuevo libro
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.workbooks.add
        oSheet = oBook.worksheets(1)

        'Declaramos el nombre de las columnas
        oSheet.range("A1").value = "Cliente"
        oSheet.range("B1").value = "Nombre"
        oSheet.range("C1").value = "Compra_Anual"
        oSheet.range("D1").value = "C/IVA"
        oSheet.range("E1").value = "Calificacion"
        oSheet.range("F1").value = "Dias_Credito"
        oSheet.range("G1").value = "Antiguedad"
        oSheet.range("H1").value = "Promedio de pago por mes"
        oSheet.range("I1").value = "sobre giro del promedio compra mensual"
        oSheet.range("J1").value = "Límite (suma de promedio CM + Sobregiro de CM = Nuevo Límite"
        oSheet.range("K1").value = "Nuevo factores sobre giro mensual por calif"
        oSheet.range("L1").value = "Nuevo saldo sobre giro mensual por antiguedad"
        oSheet.range("M1").value = "Total del saldo (nuevo limite + nuevo sobregiro)"
        oSheet.range("N1").value = "Agente"
        oSheet.range("O1").value = "Lista precios"

        'para poner la primera fila de los titulos en negrita
        oSheet.range("A1:o1").font.bold = True
        Dim fila_dt As Integer = 0
        Dim fila_dt_excel As Integer = 1

        Dim total_reg As Integer = 0
        Dim TotalDias As String = SQL.CampoEspecifico("SELECT DATEDIFF(day, '" + dtpFechaInicio.Value.ToString("yyyy-MM-dd") + "', '" + dtpFechaFinal.Value.ToString("yyyy-MM-dd") + "') + 1 as 'DIAS';", "DIAS")
        Dim TotalMeses As String = SQL.CampoEspecifico("SELECT DATEDIFF(MONTH, '" + dtpFechaInicio.Value.ToString("yyyy-MM-dd") + "', '" + dtpFechaFinal.Value.ToString("yyyy-MM-dd") + "') + 1 as 'MESES';", "MESES")

        total_reg = dgwLimiteCliente.RowCount

        For fila_dt = 0 To total_reg

            If fila_dt = total_reg Then

            Else
                fila_dt_excel += 1
                'para leer una celda en concreto
                'el numero es la columna
                Dim cel1 As String = dgwLimiteCliente.Item(0, fila_dt).Value
                Dim cel2 As String = dgwLimiteCliente.Item(1, fila_dt).Value
                Dim cel3 As String = dgwLimiteCliente.Item(2, fila_dt).Value
                Dim cel4 As String = dgwLimiteCliente.Item(3, fila_dt).Value
                Dim cel5 As String = dgwLimiteCliente.Item(4, fila_dt).Value
                Dim cel6 As String = dgwLimiteCliente.Item(5, fila_dt).Value
                Dim cel7 As String = dgwLimiteCliente.Item(6, fila_dt).Value
                Dim cel8 As String = "=D" + fila_dt_excel.ToString() + "/" + TotalMeses
                Dim cel9 As String = "=H" + fila_dt_excel.ToString() + "*F" + fila_dt_excel.ToString() + "/" + TotalDias
                Dim cel10 As String = "=H" + fila_dt_excel.ToString() + "+I" + fila_dt_excel.ToString()
                Dim cel11 As String = "=SI(E" + fila_dt_excel.ToString() + "=1,I" + fila_dt_excel.ToString() + "*0.5,SI(E" + fila_dt_excel.ToString() + "=2,I" + fila_dt_excel.ToString() + "*0.4,SI(E" + fila_dt_excel.ToString() + "=3,I" + fila_dt_excel.ToString() + "*0.3)))"
                Dim cel12 As String = "=SI(G" + fila_dt_excel.ToString() + "=1,K" + fila_dt_excel.ToString() + "*0.3,K" + fila_dt_excel.ToString() + "*0)"
                Dim cel13 As String = "=J" + fila_dt_excel.ToString() + "+K" + fila_dt_excel.ToString() + "+L" + fila_dt_excel.ToString()
                Dim cel14 As String = dgwLimiteCliente.Item(13, fila_dt).Value
                Dim cel15 As String = dgwLimiteCliente.Item(14, fila_dt).Value

                'ahora que ya tenemos los datos, asignamos la cel a la celda correspondiente
                oSheet.range("A" & fila_dt_excel).value = cel1
                oSheet.range("B" & fila_dt_excel).value = cel2
                oSheet.range("C" & fila_dt_excel).value = FormatNumber(cel3)
                oSheet.range("D" & fila_dt_excel).value = FormatNumber(cel4)
                oSheet.range("E" & fila_dt_excel).value = cel5
                oSheet.range("F" & fila_dt_excel).value = cel6
                oSheet.range("G" & fila_dt_excel).value = cel7
                oSheet.range("H" & fila_dt_excel).value = cel8 'FormatNumber(cel8)*/
                oSheet.range("I" & fila_dt_excel).value = cel9 'FormatNumber(cel9)
                oSheet.range("J" & fila_dt_excel).value = cel10 'FormatNumber(cel10)
                oSheet.range("K" & fila_dt_excel).value = cel11 'FormatNumber(cel11)
                oSheet.range("L" & fila_dt_excel).value = cel12 'FormatNumber(cel12)
                oSheet.range("M" & fila_dt_excel).value = cel13 'FormatNumber(cel13)
                oSheet.range("N" & fila_dt_excel).value = cel14
                oSheet.range("O" & fila_dt_excel).value = cel15
            End If


        Next

        oSheet.columns("A:O").entirecolumn.autofit()

        oExcel.visible = True
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
        GC.Collect()
        oSheet = Nothing
        oBook = Nothing
        oExcel = Nothing

    End Sub

    Private Sub btExportar_Click(sender As Object, e As EventArgs) Handles btExportar.Click
        Generar_Excel()
    End Sub

    Sub Cambiar_Calificacion(e As DataGridViewCellEventArgs)

        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then

            If dgwLimiteCliente.Columns(e.ColumnIndex).Name = "Calificacion" Then
                'VARIABLES A MODIFICAR
                Dim Calificacion As Integer = Integer.Parse(dgwLimiteCliente.Rows(e.RowIndex).Cells("Calificacion").Value.ToString())
                Dim SobreGiro As Decimal = Decimal.Parse(dgwLimiteCliente.Rows(e.RowIndex).Cells("sobre giro del promedio compra mensual").Value.ToString())

                If Calificacion = 1 Then
                    dgwLimiteCliente(10, dgwLimiteCliente.CurrentRow.Index).Value = Convert.ToString(SobreGiro * 0.5)
                ElseIf Calificacion = 2 Then
                    dgwLimiteCliente(10, dgwLimiteCliente.CurrentRow.Index).Value = Convert.ToString(SobreGiro * 0.4)
                ElseIf Calificacion Then
                    dgwLimiteCliente(10, dgwLimiteCliente.CurrentRow.Index).Value = Convert.ToString(SobreGiro * 0.3)
                End If

                Dim Antiguedad As Integer = Integer.Parse(dgwLimiteCliente.Rows(e.RowIndex).Cells("Antiguedad").Value.ToString())
                Dim NuevoFactores As Decimal = Decimal.Parse(dgwLimiteCliente.Rows(e.RowIndex).Cells("Nuevo factores sobre giro mensual por calif").Value.ToString())

                If Antiguedad = 1 Then
                    dgwLimiteCliente(11, dgwLimiteCliente.CurrentRow.Index).Value = Convert.ToString(NuevoFactores * 0.3)
                Else
                    dgwLimiteCliente(11, dgwLimiteCliente.CurrentRow.Index).Value = Convert.ToString(NuevoFactores * 0)
                End If

                Dim LimiteSuma As Decimal = Decimal.Parse(dgwLimiteCliente.Rows(e.RowIndex).Cells("Límite (suma de promedio CM + Sobregiro de CM = Nuevo Límite").Value.ToString())
                Dim NuevoSaldo As Decimal = Decimal.Parse(dgwLimiteCliente.Rows(e.RowIndex).Cells("Nuevo saldo sobre giro mensual por antiguedad").Value.ToString())

                dgwLimiteCliente(12, dgwLimiteCliente.CurrentRow.Index).Value = Convert.ToString(LimiteSuma + NuevoFactores + NuevoSaldo)

            End If

        Else

        End If
        'dgwLimiteCliente(4, dgwLimiteCliente.CurrentRow.Index).Value = "HOLA"
    End Sub

    Private Sub dgwLimiteCliente_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgwLimiteCliente.CellValueChanged
        Cambiar_Calificacion(e)
    End Sub

    Private Sub dtpFechaFinal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpFechaFinal.KeyPress
        If AscW(e.KeyChar) = CInt(Keys.Enter) Then
            Buscar()
        End If
    End Sub

    Private Sub dtpFechaInicio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpFechaInicio.KeyPress
        'If AscW(e.KeyChar) = CInt(Keys.Enter) Then
        '    Buscar()
        'End If
    End Sub

    Sub Filtro()
        If String.IsNullOrWhiteSpace(tbFiltro.Text) Then
        Else
            'Por defecto, indico buscar en la primera columna
            Dim indiceColumna As Integer = 0
            Dim renglon As Integer = 0

            'Recorro filas del DataGridView
            For Each row As DataGridViewRow In dgwLimiteCliente.Rows
                'Si el contenido de la columna coinside con el valor del TextBox
                If CStr(row.Cells(indiceColumna).Value).ToLower = tbFiltro.Text.ToLower Then
                    'Selecciono fila y abandono bucle
                    renglon = row.Index
                    row.Selected = True
                    Exit For
                End If
            Next
            dgwLimiteCliente.Rows(renglon).Selected = True
            dgwLimiteCliente.CurrentCell = dgwLimiteCliente.Rows(renglon).Cells(indiceColumna)
        End If
    End Sub

    Private Sub tbFiltro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbFiltro.KeyPress
        e.KeyChar = Char.ToUpper(e.KeyChar)
        If AscW(e.KeyChar) = CInt(Keys.Enter) Then
            Filtro()
        End If
    End Sub

    Private Sub dgwLimiteCliente_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgwLimiteCliente.ColumnHeaderMouseClick
        For Each fila As DataGridViewRow In dgwLimiteCliente.Rows
            If fila.Cells("Antiguedad").Value = 1 Then
                fila.DefaultCellStyle.BackColor = Color.LightGreen
            Else
                fila.DefaultCellStyle.BackColor = Color.LightSalmon
            End If
        Next

        If dgwLimiteCliente.Rows.Count > 0 Then
            Dim column As DataGridViewColumn = dgwLimiteCliente.Columns("Compra_Anual")
            column.DefaultCellStyle.Format = "C2"

            Dim column1 As DataGridViewColumn = dgwLimiteCliente.Columns("C/IVA")
            column1.DefaultCellStyle.Format = "C2"

            Dim column2 As DataGridViewColumn = dgwLimiteCliente.Columns("Promedio de pago por mes")
            column2.DefaultCellStyle.Format = "C2"

            Dim column3 As DataGridViewColumn = dgwLimiteCliente.Columns("sobre giro del promedio compra mensual")
            column3.DefaultCellStyle.Format = "C2"

            Dim column4 As DataGridViewColumn = dgwLimiteCliente.Columns("Límite (suma de promedio CM + Sobregiro de CM = Nuevo Límite")
            column4.DefaultCellStyle.Format = "C2"

            Dim column5 As DataGridViewColumn = dgwLimiteCliente.Columns("Nuevo factores sobre giro mensual por calif")
            column5.DefaultCellStyle.Format = "C2"

            Dim column6 As DataGridViewColumn = dgwLimiteCliente.Columns("Nuevo saldo sobre giro mensual por antiguedad")
            column6.DefaultCellStyle.Format = "C2"

            Dim column7 As DataGridViewColumn = dgwLimiteCliente.Columns("Total del saldo (nuevo limite + nuevo sobregiro)")
            column7.DefaultCellStyle.Format = "C2"
        End If

        dgwLimiteCliente.Columns(0).ReadOnly = True
        dgwLimiteCliente.Columns(1).ReadOnly = True
        dgwLimiteCliente.Columns(2).ReadOnly = True
        dgwLimiteCliente.Columns(3).ReadOnly = True
        'dgwLimiteCliente.Columns(4).ReadOnly = True
        dgwLimiteCliente.Columns(5).ReadOnly = True
        dgwLimiteCliente.Columns(6).ReadOnly = True
        dgwLimiteCliente.Columns(7).ReadOnly = True
        dgwLimiteCliente.Columns(8).ReadOnly = True
        dgwLimiteCliente.Columns(9).ReadOnly = True
        dgwLimiteCliente.Columns(10).ReadOnly = True
        dgwLimiteCliente.Columns(11).ReadOnly = True
        dgwLimiteCliente.Columns(12).ReadOnly = True
        dgwLimiteCliente.Columns(13).ReadOnly = True
        dgwLimiteCliente.Columns(14).ReadOnly = True
    End Sub
End Class