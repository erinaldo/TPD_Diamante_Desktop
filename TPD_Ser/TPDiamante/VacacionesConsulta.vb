
Imports System.Data.SqlClient

Public Class VacacionesConsulta

 Public conexion2 As New SqlConnection(StrTpm)
 Public Consulta As String
 Public DvDetalle As New DataView
 Dim AdapMObra As SqlClient.SqlDataAdapter
 Dim DataSetX As DataSet
 Dim DvLP As New DataView
 Dim strTemp As String = ""
 Dim empleado As String
 Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
  Try
   'MsgBox(CBNomEmp.SelectedValue.ToString)
   If CBNomEmp.SelectedIndex.ToString = "-1" Or CBNomEmp.SelectedValue = 1010 Then
    MessageBox.Show("Por favor elige a un empleado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    Return
   End If

   'If CBPeriodo.SelectedIndex.ToString = "-1" Then
   '    MessageBox.Show("Por favor elige a un periodo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
   '    Return
   'End If
   'MsgBox(CBNomEmp.SelectedValue.ToString)
   'MsgBox(CBPeriodo.SelectedValue.ToString)
   'Return
   'MsgBox(cmbAlmacen.SelectedValue.ToString)
   empleado = CBNomEmp.SelectedValue.ToString
   DvLP.RowFilter = "NumEmp = " & CBNomEmp.SelectedValue.ToString
   Dim row_azu As DataRowView
   row_azu = DvLP.Item(0)
   'MsgBox(row_azu.Item("Tipo"))
   Consulta = " "
   Consulta &= "update Empleados set Antiguedad = DATEDIFF(day,FechaIMSS, GETDATE())/365  "
   Consulta &= "update Empleados set DiasVacaCorres = "

   If row_azu.Item("Tipo").ToString = "I" Then
    Consulta &= "case when Antiguedad = 0 then 0 "
    Consulta &= "when Antiguedad = 1 then 3  "
    Consulta &= "when Antiguedad = 2 then 4 "
    Consulta &= "when Antiguedad = 3 then 4 "
    Consulta &= "when Antiguedad = 4 then 5 "
    Consulta &= "when Antiguedad >= 5 and Antiguedad <= 9 then 6 "
    Consulta &= "when Antiguedad >= 10 and Antiguedad <= 14 then 7 "
    Consulta &= "when Antiguedad >= 15 and Antiguedad <= 19 then 8 "
    Consulta &= "when Antiguedad >= 20 and Antiguedad <= 24 then 9 "
    Consulta &= "when Antiguedad >= 25 and Antiguedad <= 29 then 10 "
    Consulta &= "when Antiguedad >= 30 and Antiguedad <= 34 then 11 "
    Consulta &= "when Antiguedad >= 35 and Antiguedad <= 39 then 12 "
    Consulta &= "end "
   Else
    Consulta &= "case when Antiguedad = 0 then 0 "
    Consulta &= "when Antiguedad = 1 then 6  "
    Consulta &= "when Antiguedad = 2 then 8 "
    Consulta &= "when Antiguedad = 3 then 10 "
    Consulta &= "when Antiguedad = 4 then 12 "
    Consulta &= "when Antiguedad >= 5 and Antiguedad <= 9 then 14 "
    Consulta &= "when Antiguedad >= 10 and Antiguedad <= 14 then 16 "
    Consulta &= "when Antiguedad >= 15 and Antiguedad <= 19 then 18 "
    Consulta &= "when Antiguedad >= 20 and Antiguedad <= 24 then 20 "
    Consulta &= "when Antiguedad >= 25 and Antiguedad <= 29 then 22 "
    Consulta &= "when Antiguedad >= 30 and Antiguedad <= 34 then 24 "
    Consulta &= "when Antiguedad >= 35 and Antiguedad <= 39 then 26 "
    Consulta &= "end "
   End If

   Consulta &= "Create Table #relacion (NumEmp int, NomEmp varchar(50), FechaIng date, Periodo varchar(20), FechaInicioPeriodo date, FechaFinPeriodo date, AntiguedadParaPeriodo int, PeriodoKey varchar(20))  "
   Consulta &= "DECLARE @indice INT  "
   Consulta &= "DECLARE @fin_indice INT  "
   Consulta &= "DECLARE @inicio_fin_indice INT "
   Consulta &= "set @inicio_fin_indice = (select Anio_band from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & " )  "
   Consulta &= "if((select DATEADD(YEAR, DATEDIFF(YEAR, FechaIMSS, GETDATE()), FechaIMSS )  from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & " ) <= GETDATE() )  "
   Consulta &= "set @fin_indice = YEAR(GETDATE());  "
   Consulta &= "else  "
   Consulta &= "set @fin_indice = (YEAR(GETDATE())) - 1;  "
   Consulta &= "set @indice = (select YEAR(FechaIMSS) from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & " )  "
   Consulta &= "while (@indice <= @fin_indice)  "
   Consulta &= "begin  "
   Consulta &= "insert into #relacion(NumEmp, NomEmp, FechaIng, Periodo, FechaInicioPeriodo, FechaFinPeriodo, AntiguedadParaPeriodo, PeriodoKey)  "
   Consulta &= "values ((select NumEmp from Empleados where NumEmp =  " & CBNomEmp.SelectedValue.ToString & "  ),  "
   Consulta &= "((select NomEmp + ' ' + AppEmp + ' ' + ApmMat from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & "  )),  "
   Consulta &= "(select FechaIMSS from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & "), "
   Consulta &= "CAST(@indice as varchar) + ' - ' + CAST((@indice + 1) as varchar),  "
   Consulta &= "(select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)), FechaIMSS ) from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & "  ), "
   Consulta &= "(select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)) + 1, FechaIMSS ) from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & "  ), "
   Consulta &= "case when (select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)), FechaIMSS ) from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & ") > GETDATE()  "
   Consulta &= "then (select (DATEDIFF(DAY, (select FechaIMSS from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & "), (select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)), FechaIMSS ) from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & ") )/365) - 1)  "
   Consulta &= "when (select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)), FechaIMSS ) from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & ") <= GETDATE()  "
   Consulta &= "then (select (DATEDIFF(DAY, (select FechaIMSS from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & "), (select DATEADD(YEAR, (@indice - YEAR(FechaIMSS)), FechaIMSS ) from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & ") )/365)) "
   Consulta &= "end, @indice ) "
   Consulta &= "set @indice = @indice + 1  "
   Consulta &= "end "
   Consulta &= "select *,   "
   If row_azu.Item("Tipo").ToString = "I" Then
    Consulta &= "case when AntiguedadParaPeriodo = 0 then 0  "
    Consulta &= "when AntiguedadParaPeriodo = 1 then 3   "
    Consulta &= "when AntiguedadParaPeriodo = 2 then 4  "
    Consulta &= "when AntiguedadParaPeriodo = 3 then 4  "
    Consulta &= "when AntiguedadParaPeriodo = 4 then 5  "
    Consulta &= "when AntiguedadParaPeriodo >= 5 and AntiguedadParaPeriodo <= 9 then 6  "
    Consulta &= "when AntiguedadParaPeriodo >= 10 and AntiguedadParaPeriodo <= 14 then 7  "
    Consulta &= "when AntiguedadParaPeriodo >= 15 and AntiguedadParaPeriodo <= 19 then 8  "
    Consulta &= "when AntiguedadParaPeriodo >= 20 and AntiguedadParaPeriodo <= 24 then 9  "
    Consulta &= "when AntiguedadParaPeriodo >= 25 and AntiguedadParaPeriodo <= 29 then 10  "
    Consulta &= "when AntiguedadParaPeriodo >= 30 and AntiguedadParaPeriodo <= 34 then 11  "
    Consulta &= "when AntiguedadParaPeriodo >= 35 and AntiguedadParaPeriodo <= 39 then 12  "
    Consulta &= "end as 'DiasVacaParaPeriodo',  "
   Else
    Consulta &= "case when AntiguedadParaPeriodo = 0 then 0  "
    Consulta &= "when AntiguedadParaPeriodo = 1 then 6   "
    Consulta &= "when AntiguedadParaPeriodo = 2 then 8  "
    Consulta &= "when AntiguedadParaPeriodo = 3 then 10  "
    Consulta &= "when AntiguedadParaPeriodo = 4 then 12  "
    Consulta &= "when AntiguedadParaPeriodo >= 5 and AntiguedadParaPeriodo <= 9 then 14  "
    Consulta &= "when AntiguedadParaPeriodo >= 10 and AntiguedadParaPeriodo <= 14 then 16  "
    Consulta &= "when AntiguedadParaPeriodo >= 15 and AntiguedadParaPeriodo <= 19 then 18  "
    Consulta &= "when AntiguedadParaPeriodo >= 20 and AntiguedadParaPeriodo <= 24 then 20  "
    Consulta &= "when AntiguedadParaPeriodo >= 25 and AntiguedadParaPeriodo <= 29 then 22  "
    Consulta &= "when AntiguedadParaPeriodo >= 30 and AntiguedadParaPeriodo <= 34 then 24  "
    Consulta &= "when AntiguedadParaPeriodo >= 35 and AntiguedadParaPeriodo <= 39 then 26  "
    Consulta &= "end as 'DiasVacaParaPeriodo',  "
   End If


   Consulta &= "(select COUNT(*) from SolVacacionesHistorico t0 where t0.NumEmpleado = " & CBNomEmp.SelectedValue.ToString & " and t0.Periodo = PeriodoKey) as 'DiasTomados', "
   Consulta &= "0 as 'DiasRestantes' "
   Consulta &= "into #relacion2  "
   Consulta &= "from #relacion  "
   Consulta &= "DECLARE @indice2 INT  "
   Consulta &= "DECLARE @fin_indice2 INT  "
   Consulta &= "if((select DATEADD(YEAR, DATEDIFF(YEAR, FechaIMSS, GETDATE()), FechaIMSS )  from Empleados where NumEmp =   " & CBNomEmp.SelectedValue.ToString & "  ) <= GETDATE() )  "
   Consulta &= "set @fin_indice2 = YEAR(GETDATE());  "
   Consulta &= "else  "
   Consulta &= "set @fin_indice2 = (YEAR(GETDATE())) - 1;  "
   Consulta &= "set @indice2 = (select YEAR(FechaIMSS) from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & " )  "
   Consulta &= "while (@indice2 <= @fin_indice2)  "
   Consulta &= "begin "
   Consulta &= "if @indice2 = (select YEAR(FechaIMSS) from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & " )  "
   Consulta &= "update #relacion2 set DiasRestantes = DiasVacaParaPeriodo - DiasTomados where PeriodoKey = @indice2; "
   Consulta &= "else  "
   Consulta &= "if (select r0.DiasRestantes from #relacion2 r0 where r0.PeriodoKey = @indice2 - 1) < 0 "
   Consulta &= "update #relacion2 set DiasRestantes = DiasVacaParaPeriodo - DiasTomados + (select r0.DiasRestantes from #relacion2 r0 where r0.PeriodoKey = @indice2 - 1) where PeriodoKey = @indice2; "
   Consulta &= "else "
   Consulta &= "update #relacion2 set DiasRestantes = DiasVacaParaPeriodo - DiasTomados where PeriodoKey = @indice2; "
   Consulta &= "set @indice2 = @indice2 + 1  "
   Consulta &= "end "
   Consulta &= "select *, (DiasRestantes * -1) + (DiasVacaParaPeriodo - DiasTomados) as 'DiasXAdelantado'  "
   Consulta &= "into #Header "
   Consulta &= "from #relacion2 "
   Consulta &= "select ROW_NUMBER() OVER(PARTITION BY t0.Periodo order by t0.DiaSol) AS Row, "
   Consulta &= "t0.NumEmpleado, t0.Periodo, t0.DiaSol, (t1.DiasVacaParaPeriodo - t1.DiasXAdelantado) as 'DiasDisponibles', t0.folio "
   Consulta &= "into #tmp1 "
   Consulta &= "from SolVacacionesHistorico t0 inner join #Header t1 on t0.Periodo = t1.PeriodoKey  "
   Consulta &= "where t0.NumEmpleado = " & CBNomEmp.SelectedValue.ToString & " "
   Consulta &= "select NumEmp, PeriodoKey, Periodo,  Convert(varchar(15),cast(FechaInicioPeriodo as DATE),105) as 'InicioPeriodo',  "
   Consulta &= "Convert(varchar(15),cast(FechaFinPeriodo as DATE),105) as 'FinPeriodo',  "
   Consulta &= "cast(AntiguedadParaPeriodo as varchar) as 'Antiguedad', cast(DiasVacaParaPeriodo as varchar) as 'DiasVacaciones',  "
   Consulta &= "cast((DiasVacaParaPeriodo - DiasXAdelantado) as varchar) as 'DiasDisponibles', '' as 'DiaSolicitado', 0 as 'DiasRestantes', "
   Consulta &= "cast(DiasTomados as varchar) as 'DiasGozados', cast(DiasRestantes as varchar) as 'DiasPendientes', 1 as 'Priori', 999 as 'folio', '' as 'Accion' "
   Consulta &= "into #pre "
   Consulta &= "from #Header  "
   Consulta &= "union all "
   Consulta &= "select NumEmpleado as 'NumEmp', Periodo as 'PeriodoKey', '' as 'Periodo', '' as 'InicioPeriodo', '' as 'FinPeriodo',  "
   Consulta &= "'' as 'Antiguedad', '' as 'DiasVacaciones', '' as 'DiasDisponibles', Convert(varchar(15),cast(DiaSol as DATE),105) as 'DiaSolicitado',  "
   Consulta &= "(DiasDisponibles - Row) as 'DiasRestantes', '' as 'DiasGozados', '' as 'DiasPendientes', 2 as 'Priori', folio, 'Eliminar' as 'Accion' "
   Consulta &= "from #tmp1 "
   Consulta &= "order by NumEmp, PeriodoKey DESC, Periodo DESC, DiasRestantes DESC "

   Consulta &= "select NumEmp, NomEmp + ' ' + AppEmp + ' ' + ApmMat as 'NomEmp', FechaIng, FechaIMSS, Antiguedad, DiasVacaCorres from Empleados where NumEmp = " & CBNomEmp.SelectedValue.ToString & " "

   'If CBPeriodo.SelectedValue.ToString <> "9999" And CBPeriodo.SelectedValue.ToString <> "8888" Then
   '    Consulta &= "select NumEmp, NomEmp, FechaIng, Periodo, FechaInicioPeriodo, FechaFinPeriodo, DiasVacaParaPeriodo, "
   '    Consulta &= "PeriodoKey as 'Periodo2', DiasTomados as 'DiasGozados', DiasRestantes as 'DiasReales', DiasXAdelantado as 'Adelantado' "
   '    Consulta &= "from #Header where PeriodoKey = " & CBPeriodo.SelectedValue.ToString & " "
   'Else
   '    Consulta &= "select NumEmp, NomEmp, FechaIng, Periodo, FechaInicioPeriodo, FechaFinPeriodo, DiasVacaParaPeriodo, "
   '    Consulta &= "PeriodoKey as 'Periodo2', DiasTomados as 'DiasGozados', DiasRestantes as 'DiasReales', DiasXAdelantado as 'Adelantado' "
   '    Consulta &= "from #Header where PeriodoKey = @fin_indice "
   'End If

   Consulta &= "select NumEmp, NomEmp, FechaIng, Periodo, FechaInicioPeriodo, FechaFinPeriodo, DiasVacaParaPeriodo, "
   Consulta &= "PeriodoKey as 'Periodo2', DiasTomados as 'DiasGozados', DiasRestantes as 'DiasReales', DiasXAdelantado as 'Adelantado', (DiasTomados + DiasXAdelantado) as 'DiasTotalesTomados' "
   Consulta &= "from #Header where PeriodoKey = @fin_indice "

   If (CBNomEmp.SelectedValue.ToString = "8" Or CBNomEmp.SelectedValue.ToString = "53") Then
    Consulta &= " select Periodo, InicioPeriodo as 'Inicio Periodo', FinPeriodo as 'Fin Periodo', Antiguedad,  "
    Consulta &= "DiasVacaciones as 'Dias Vacaciones', DiasDisponibles, DiaSolicitado as 'Dia Solicitado', "
    Consulta &= "case when Priori = 1 then '' else CAST(DiasRestantes as varchar) end as 'Dias Restantes' , "
    Consulta &= "DiasGozados as 'Dias Gozados', DiasPendientes as 'Dias Pendientes', PeriodoKey as 'Periodo2', Priori, "
    Consulta &= "case when DiasPendientes = '' then 'NA' "
    Consulta &= "else case when CAST(DiasPendientes AS integer) < 0 then 'Negativo' "
    Consulta &= "else 'Positivo' end "
    Consulta &= "end as 'Estado', folio, Accion "
    Consulta &= "from #pre where PeriodoKey >= @inicio_fin_indice and PeriodoKey <= @fin_indice "
   Else
    Consulta &= " select Periodo, InicioPeriodo as 'Inicio Periodo', FinPeriodo as 'Fin Periodo', Antiguedad,  "
    Consulta &= "DiasVacaciones as 'Dias Vacaciones', DiasDisponibles, DiaSolicitado as 'Dia Solicitado', "
    Consulta &= "case when Priori = 1 then '' else CAST(DiasRestantes as varchar) end as 'Dias Restantes' , "
    Consulta &= "DiasGozados as 'Dias Gozados', DiasPendientes as 'Dias Pendientes', PeriodoKey as 'Periodo2', Priori, "
    Consulta &= "case when DiasPendientes = '' then 'NA' "
    Consulta &= "else case when CAST(DiasPendientes AS integer) < 0 then 'Negativo' "
    Consulta &= "else 'Positivo' end "
    Consulta &= "end as 'Estado', folio, Accion "
    Consulta &= "from #pre where PeriodoKey >= @inicio_fin_indice and PeriodoKey <= @fin_indice "
   End If



   'If CBPeriodo.SelectedValue.ToString <> "9999" And CBPeriodo.SelectedValue.ToString <> "8888" Then
   '    Consulta &= "where PeriodoKey = " & CBPeriodo.SelectedValue.ToString
   'Else
   '    If CBPeriodo.SelectedValue.ToString = "8888" Then
   '        Consulta &= "where PeriodoKey = @fin_indice or PeriodoKey = (@fin_indice - 1) "
   '    End If

   'End If

   Consulta &= " drop table #Header "
   Consulta &= "drop table #pre "
   Consulta &= "drop table #relacion "
   Consulta &= "drop table #relacion2 "
   Consulta &= "drop table #tmp1 "



   Using conexion2 As New SqlConnection(StrTpm)
    Using cmd As New SqlCommand(Consulta, conexion2)
     conexion2.Open()
     AdapMObra = New SqlClient.SqlDataAdapter(cmd)
     DataSetX = New DataSet
     AdapMObra.Fill(DataSetX)
     'MsgBox(DataSetX.Tables(0).Rows(0)(0).ToString())
     Label10.Text = DataSetX.Tables(0).Rows(0)("NumEmp").ToString()
     Label14.Text = DataSetX.Tables(0).Rows(0)("NomEmp").ToString()
     Label9.Text = DataSetX.Tables(0).Rows(0)("FechaIng")
     Label28.Text = DataSetX.Tables(0).Rows(0)("FechaIMSS")
     Label11.Text = DataSetX.Tables(0).Rows(0)("Antiguedad").ToString() & " año(s)"
     Label15.Text = DataSetX.Tables(0).Rows(0)("DiasVacaCorres").ToString() & " dias"

     'If CBPeriodo.SelectedValue.ToString <> "9999" And CBPeriodo.SelectedValue.ToString <> "8888" Then
     '    Label22.Text = CBPeriodo.SelectedValue.ToString & " - " & (Integer.Parse(CBPeriodo.SelectedValue.ToString) + 1).ToString

     'Else
     '    If CBPeriodo.SelectedValue.ToString = "9999" Then
     '        Label22.Text = "TODOS (Historico)"
     '    Else
     '        Label22.Text = "Ulitmos dos periodos"
     '    End If

     'End If

     Label22.Text = DataSetX.Tables(1).Rows(0)("Periodo")
     Label23.Text = DataSetX.Tables(1).Rows(0)("FechaInicioPeriodo")
     Label24.Text = DataSetX.Tables(1).Rows(0)("FechaFinPeriodo")
     Label21.Text = DataSetX.Tables(1).Rows(0)("DiasVacaParaPeriodo").ToString() & " dias"
     Label25.Text = DataSetX.Tables(1).Rows(0)("DiasTotalesTomados").ToString() & " dia(s)"
     Label26.Text = DataSetX.Tables(1).Rows(0)("DiasReales").ToString() & " dia(s)"
     Label27.Text = DataSetX.Tables(1).Rows(0)("Adelantado").ToString() & " dia(s)"


     GroupBox1.Visible = True
     GroupBox2.Visible = True

     'DsVtas.Tables(0).TableName = "Detalle"


     DvDetalle.Table = DataSetX.Tables(2)

     DGVacaciones.DataSource = DvDetalle
     DGVacaciones.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
     DGVacaciones.ClearSelection()
     DGVacaciones.Columns("Antiguedad").DefaultCellStyle.Font = New Font(DGVacaciones.DefaultCellStyle.Font, FontStyle.Bold)
     DGVacaciones.Columns("Dias Vacaciones").DefaultCellStyle.Font = New Font(DGVacaciones.DefaultCellStyle.Font, FontStyle.Bold)
     DGVacaciones.Columns("DiasDisponibles").DefaultCellStyle.Font = New Font(DGVacaciones.DefaultCellStyle.Font, FontStyle.Bold)
     DGVacaciones.Columns("DiasDisponibles").HeaderText = "Dias Disponibles"
     DGVacaciones.Columns("Dias Gozados").DefaultCellStyle.Font = New Font(DGVacaciones.DefaultCellStyle.Font, FontStyle.Bold)
     DGVacaciones.Columns("Dias Pendientes").DefaultCellStyle.Font = New Font(DGVacaciones.DefaultCellStyle.Font, FontStyle.Bold)

     DGVacaciones.Columns().Remove("Accion")
     Dim col As New DataGridViewLinkColumn
     col.DataPropertyName = "Accion"
     col.HeaderText = "Accion"
     col.Name = "Accion"
     col.SortMode = DataGridViewColumnSortMode.Automatic
     DGVacaciones.Columns.Insert(14, col)
     DGVacaciones.Columns("Accion").DefaultCellStyle.Font = New Font(DGVacaciones.DefaultCellStyle.Font, FontStyle.Bold)
     DGVacaciones.Columns("Accion").Width = 70
     DGVacaciones.Columns("Accion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

     'DGVacaciones.Columns("Dias Pendientes").DefaultCellStyle.ForeColor = Color.Red
     DGVacaciones.Columns("Dias Gozados").DefaultCellStyle.ForeColor = Color.Blue
     DGVacaciones.Columns("Dias Vacaciones").DefaultCellStyle.ForeColor = Color.DarkGreen
     DGVacaciones.Columns("DiasDisponibles").DefaultCellStyle.ForeColor = Color.DarkMagenta
     For Each row As DataGridViewRow In DGVacaciones.Rows
      If (row.Cells("Priori").Value = 1) Then
       'MsgBox("es uno")
       For Each cel As DataGridViewCell In row.Cells
        cel.Style.BackColor = Color.LightBlue
       Next
       'row.Cells("AntiguedadParaPeriodo").Style.BackColor = Color.LightGreen
       'row.Cells("DiasVaca").Style.BackColor = Color.LightGreen
      Else
       'MsgBox("es dos")
       For Each cel As DataGridViewCell In row.Cells
        cel.Style.BackColor = Color.AliceBlue
       Next
      End If

      If (row.Cells("Estado").Value.ToString = "Positivo") Then
       row.Cells("Dias Pendientes").Style.ForeColor = Color.Green
      ElseIf (row.Cells("Estado").Value.ToString = "Negativo") Then
       row.Cells("Dias Pendientes").Style.ForeColor = Color.Red
      End If


      'If row.Cells("DiasGozados").Value.ToString <> "" Then
      '    row.Cells("DiasGozados").Style.BackColor = Color.Gold
      'End If

      'If row.Cells("Pendiente").Value.ToString <> "" Then
      '    row.Cells("Pendiente").Style.BackColor = Color.DarkRed
      'End If
     Next
     DGVacaciones.Columns("Periodo2").Visible = False
     DGVacaciones.Columns("Priori").Visible = False
     DGVacaciones.Columns("Estado").Visible = False
     DGVacaciones.Columns("folio").Visible = False

     If UsrTPM.ToString <> "RHUMANOS" And UsrTPM.ToString <> "MANAGER" Then
      DGVacaciones.Columns("Accion").Visible = False
     End If

     'DGVacaciones.Columns("")



     conexion2.Close()
    End Using
   End Using

  Catch ex As Exception
   MsgBox(ex.Message)
  Finally
   If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
    conexion2.Close()
   End If
  End Try
 End Sub

 Private Sub DisenoGrid()
  With Me.DGVacaciones

   '.DataSource = DtAgte
   .ReadOnly = True
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

   .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   Try
    .Columns(0).Name = "No. Empleado"
    .Columns(0).HeaderText = "No. Empleado"
    .Columns(0).Width = 50
    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(0).Frozen = True

    .Columns(1).Name = "Nombre"
    .Columns(1).HeaderText = "Nombre"
    .Columns(1).Width = 160
    .Columns(1).Frozen = True

    .Columns(2).Name = "Fecha de Ing."
    .Columns(2).HeaderText = "Fecha de Ingreso"
    .Columns(2).Width = 75
    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(2).Frozen = True

    .Columns(3).Name = "Antiguedad"
    .Columns(3).HeaderText = "Antiguedad"
    .Columns(3).Width = 70
    .Columns(3).DefaultCellStyle.Format = "##0.##"
    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(3).Frozen = True

    .Columns(4).Name = "Días de Vac."
    .Columns(4).HeaderText = "Días de Vacaciones"
    .Columns(4).Width = 70
    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(4).DefaultCellStyle.Format = "#0"
    .Columns(4).Frozen = True

    .Columns(5).Name = "Periodo Comp."
    .Columns(5).HeaderText = "Periodo Comprendido"
    .Columns(5).Width = 70
    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(5).Frozen = True
    '.Columns(5).DefaultCellStyle.Format = "##0.#0 %"

    .Columns(6).Name = "Días Gozados"
    .Columns(6).HeaderText = "Días Gozados"
    .Columns(6).Width = 70
    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(6).DefaultCellStyle.Format = "##0"
    .Columns(6).Frozen = True

    .Columns(7).Name = "Días Pendientes"
    .Columns(7).HeaderText = "Días Pendientes"
    .Columns(7).Width = 70
    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(7).DefaultCellStyle.Format = "##0"
    .Columns(7).Frozen = True

    .Columns(8).Name = "Inicio Vac."
    .Columns(8).HeaderText = "Inicio Vac."
    .Columns(8).Width = 70
    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(8).Frozen = True

    .Columns(9).Name = "Caducidad Vac."
    .Columns(9).HeaderText = "Caducidad Vac."
    .Columns(9).Width = 70
    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    .Columns(9).Frozen = True

    .Columns(10).Name = "Día 1"
    .Columns(10).HeaderText = "Día 1"
    .Columns(10).Width = 70
    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(11).Name = "Día 2"
    .Columns(11).HeaderText = "Día 2"
    .Columns(11).Width = 70
    .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(12).Name = "Día 3"
    .Columns(12).HeaderText = "Día 3"
    .Columns(12).Width = 70
    .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(13).Name = "Día 4"
    .Columns(13).HeaderText = "Día 4"
    .Columns(13).Width = 70
    .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(14).Name = "Día 5"
    .Columns(14).HeaderText = "Día 5"
    .Columns(14).Width = 70
    .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(15).Name = "Día 6"
    .Columns(15).HeaderText = "Día 6"
    .Columns(15).Width = 70
    .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(16).Name = "Día 7"
    .Columns(16).HeaderText = "Día 7"
    .Columns(16).Width = 70
    .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(17).Name = "Día 8"
    .Columns(17).HeaderText = "Día 8"
    .Columns(17).Width = 70
    .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(18).Name = "Día 9"
    .Columns(18).HeaderText = "Día 9"
    .Columns(18).Width = 70
    .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(19).Name = "Día 10"
    .Columns(19).HeaderText = "Día 10"
    .Columns(19).Width = 70
    .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(20).Name = "Día 11"
    .Columns(20).HeaderText = "Día 11"
    .Columns(20).Width = 70
    .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(21).Name = "Día 12"
    .Columns(21).HeaderText = "Día 12"
    .Columns(21).Width = 70
    .Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(22).Name = "Día 13"
    .Columns(22).HeaderText = "Día 13"
    .Columns(22).Width = 70
    .Columns(22).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(23).Name = "Día 14"
    .Columns(23).HeaderText = "Día 14"
    .Columns(23).Width = 70
    .Columns(23).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(24).Name = "Día 15"
    .Columns(24).HeaderText = "Día 15"
    .Columns(24).Width = 70
    .Columns(24).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(25).Name = "Día 16"
    .Columns(25).HeaderText = "Día 16"
    .Columns(25).Width = 70
    .Columns(25).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    .Columns(26).Name = "Ubicación"
    .Columns(26).HeaderText = "Ubicación"
    .Columns(26).Width = 90
    .Columns(26).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

   Catch ex As Exception

   End Try

  End With
 End Sub

 Private Sub VacacionesConsulta_Load(sender As Object, e As EventArgs) Handles MyBase.Load

  'CBPeriodo.SelectedIndex = 1

  Dim ConsutaLista As String

  Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
   mllenaComboAlmacen(SqlConnection)
  End Using

  Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)

   Dim DSetTablas As New DataSet
   ConsutaLista = "SELECT NumEmp,NomEmp+' '+AppEmp+' '+ApmMat AS 'NomEmp', YEAR(FechaIMSS) as 'AnioIngreso', "
   ConsutaLista &= "case when (DATEADD(YEAR, DATEDIFF(YEAR, FechaIMSS, GETDATE()), FechaIMSS )) <= GETDATE() then YEAR(GETDATE()) "
   ConsutaLista &= "else ( YEAR(GETDATE()) - 1) end as 'Tope', Tipo "
   ConsutaLista &= "FROM Empleados where Status = 'Activo' and Vacaciones = 'Si' ORDER BY NomEmp "
   Dim daGEmpleado As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

   'Dim DSetTablas As New DataSet
   daGEmpleado.Fill(DSetTablas, "Empleados")


   'AGREGAR FILA
   Dim fila As Data.DataRow
   'Asignamos a fila la nueva Row(Fila)del Dataset
   fila = DSetTablas.Tables("Empleados").NewRow
   'Agregamos los valores a los campos de la tabla
   fila("NomEmp") = "--Ningun Resultado--"
   fila("NumEmp") = 1010
   'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
   DSetTablas.Tables("Empleados").Rows.Add(fila)

   DvLP.Table = DSetTablas.Tables("Empleados")
   DvLP.RowFilter = "NumEmp <> 1010"


   Me.CBNomEmp.DataSource = DvLP
   Me.CBNomEmp.DisplayMember = "NomEmp"
   Me.CBNomEmp.ValueMember = "NumEmp"
   'Me.CBNomEmp.SelectedValue = 9999
   Me.CBNomEmp.SelectedIndex = -1

  End Using
 End Sub


 Private Sub mllenaComboAlmacen(ByVal conexion As SqlConnection)
  Try
   Dim da As New SqlDataAdapter("SELECT GroupCode , GroupName " +
                                         "FROM OCRG with (nolock) " +
                                         "WHERE GroupType = 'C' ORDER BY GroupName ", conexion)

   Dim ds As New DataSet
   da.Fill(ds)
   ds.Tables(0).Rows.Add(0, "TODAS")
   ''Me.cmbAlmacen.DataSource = ds.Tables(0)
   ''Me.cmbAlmacen.DisplayMember = "GroupName"
   ''Me.cmbAlmacen.ValueMember = "GroupCode"

   ''Me.cmbAlmacen.SelectedValue = 0

  Catch ex As Exception

  End Try

 End Sub

 Private Sub DGVacaciones_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGVacaciones.RowPrePaint
  'If DGVacaciones.Rows(e.RowIndex).Cells("Priori").Value = 1 Then
  '    For Each cel As DataGridViewCell In DGVacaciones.Rows(e.RowIndex).Cells
  '        cel.Style.BackColor = Color.FloralWhite
  '    Next
  '    DGVacaciones.Rows(e.RowIndex).Cells("AntiguedadParaPeriodo").Style.BackColor = Color.LightGreen
  '    DGVacaciones.Rows(e.RowIndex).Cells("DiasVaca").Style.BackColor = Color.LightGreen
  '    ' DGVacaciones.Rows(e.RowIndex).sty()
  'Else
  '    For Each cel As DataGridViewCell In DGVacaciones.Rows(e.RowIndex).Cells
  '        cel.Style.BackColor = Color.AliceBlue
  '    Next
  'End If

  ''MsgBox(DGVacaciones.Rows(e.RowIndex).Cells("DiasGozados").Value.ToString)
  'If DGVacaciones.Rows(e.RowIndex).Cells("DiasGozados").Value.ToString <> "" Then
  '    DGVacaciones.Rows(e.RowIndex).Cells("DiasGozados").Style.BackColor = Color.Gold

  'End If

  'If DGVacaciones.Rows(e.RowIndex).Cells("Pendiente").Value.ToString <> "" Then
  '    DGVacaciones.Rows(e.RowIndex).Cells("Pendiente").Style.BackColor = Color.DarkRed
  'End If

  'DGVacaciones.Rows(e.RowIndex).Cells("DiasGozados").Style.BackColor = Color.Gold
  'DGVacaciones.Rows(e.RowIndex).Cells("Pendiente").Style.BackColor = Color.Red


  'DGVacaciones.Rows(e.RowIndex).Cells("Días de Vac.").Style.BackColor = Color.Gold
  'DGVacaciones.Rows(e.RowIndex).Cells("Días Pendientes").Style.BackColor = Color.Yellow

  'DGVacaciones.Rows(e.RowIndex).Cells("Inicio Vac.").Style.BackColor = Color.LightGray
  'DGVacaciones.Rows(e.RowIndex).Cells("Caducidad Vac.").Style.BackColor = Color.LightGray

 End Sub

 Private Sub bExcel_Click(sender As Object, e As EventArgs) Handles bExcel.Click
  Try

   Dim exApp As New Microsoft.Office.Interop.Excel.Application
   Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

   'Añadimos el Libro al programa
   exLibro = exApp.Workbooks.Add

   ' ¿Cuantas columnas y cuantas filas?
   Dim NCol As Integer = DGVacaciones.ColumnCount
   Dim NRow As Integer = DGVacaciones.RowCount

   fFormatoExcel(exLibro, NRow)

   'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
   For i As Integer = 1 To NCol
    exLibro.Worksheets("Hoja1").Cells.Item(4, i) = DGVacaciones.Columns(i - 1).Name.ToString
   Next

   For Fila As Integer = 0 To NRow - 1

    For Col As Integer = 0 To NCol - 1
     exLibro.Worksheets("Hoja1").Cells.Item(Fila + 5, Col + 1) = DGVacaciones.Rows(Fila).Cells(Col).Value

    Next
    Estatus.Visible = True
    ProgressBar1.Value = (Fila * 100) / NRow
   Next
   Estatus.Visible = False

   'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
   exLibro.Worksheets("Hoja1").Rows.Item(4).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Rows.Item(4).HorizontalAlignment = 3
   exLibro.Worksheets("Hoja1").Cells.Range("A4:AA4").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Rows.Item(4).WrapText = True
   'exLibro.Worksheets("Hoja1").Columns.AutoFit()
   exLibro.Worksheets("Hoja1").name = "Reporte de vacaciones"


   'Aplicación visible
   exLibro.Worksheets.Application.Visible = True

   exLibro = Nothing
   exApp = Nothing

  Catch ex As Exception

  End Try
 End Sub

 Private Sub fFormatoExcel(exLibro As Microsoft.Office.Interop.Excel.Workbook, NRow As Integer)
  Try
   ''Combinamos celdas
   exLibro.Worksheets("Hoja1").Cells.Range("A1:B1").Merge(True)
   exLibro.Worksheets("Hoja1").Cells.Range("A2:B2").Merge(True)
   'exLibro.Worksheets("Hoja1").Cells.Range("A3:B3").Merge(True)

   ''aplicamos un color de fondo ala celda o rango de celdas
   exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
   exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
   'exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15

   ''Cambiamos orientacion ala hola
   exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Reporte de Vacaciones"
   'exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Linea: " + cmbLinea.Text
   exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Fecha: " + Date.Now.ToShortDateString


   exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
   exLibro.Worksheets("Hoja1").Cells.Item(4, 1).Font.Bold = 1
   'exLibro.Worksheets("Hoja1").Cells.Item(5, 1).Font.Bold = 1

   'exLibro.Worksheets("Hoja1").Columns(11).NumberFormat = "###.0000"
   'exLibro.Worksheets("Hoja1").Columns("L:U").NumberFormat = "###.0000"
   'exLibro.Worksheets("Hoja1").Columns("D:G").NumberFormat = "###,###,###"
   'exLibro.Worksheets("Hoja1").Columns("H").NumberFormat = "$ ###,###.00"
   'exLibro.Worksheets("Hoja1").Columns("J").NumberFormat = "$ ###,###.00"
   'exLibro.Worksheets("Hoja1").Columns("L").NumberFormat = "$ ###,###.00"
   exLibro.Worksheets("Hoja1").Columns("F").NumberFormat = "@"

   exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 9
   exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 28
   exLibro.Worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 11
   exLibro.Worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 11
   exLibro.Worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 10
   exLibro.Worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 9
   exLibro.Worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 10

   exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 11
   exLibro.Worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 11
   exLibro.Worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 11

   exLibro.Worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 10


   exLibro.Worksheets("Hoja1").Cells.Range("B5:B" + (NRow + 4).ToString).Interior.Color = RGB(198, 224, 180)
   exLibro.Worksheets("Hoja1").Cells.Range("D5:D" + (NRow + 4).ToString).Interior.ColorIndex = 6
   exLibro.Worksheets("Hoja1").Cells.Range("E5:E" + (NRow + 4).ToString).Interior.ColorIndex = 44
   exLibro.Worksheets("Hoja1").Cells.Range("H5:H" + (NRow + 4).ToString).Interior.Color = RGB(189, 215, 238)

   'oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.color = RGB(169, 208, 142)

   'exLibro.Worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 6

   'exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 8
  Catch ex As Exception

  End Try

 End Sub

 Private Sub CBNomEmp_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CBNomEmp.SelectionChangeCommitted
  'MsgBox("Cambie")
  'Dim dt As DataTable
  'Dim dr As DataRow
  'Dim currentyear As Integer
  'Dim tope As Integer
  ''MsgBox(CBNomEmp.SelectedValue.ToString)
  'CBPeriodo.DataSource = Nothing
  'CBPeriodo.ValueMember = Nothing
  'CBPeriodo.DisplayMember = Nothing
  'CBPeriodo.Items.Clear()
  'If (CBNomEmp.SelectedValue.ToString = "9999") Then
  '    dt = New DataTable("Tabla")
  '    dt.Columns.Add("Codigo")
  '    dt.Columns.Add("Descripcion")

  '    currentyear = Year(Date.Today)
  '    For year As Integer = currentyear - 3 To currentyear
  '        dr = dt.NewRow()
  '        dr("Codigo") = year
  '        dr("Descripcion") = year & " - " & year + 1
  '        dt.Rows.Add(dr)
  '    Next

  '    CBPeriodo.DataSource = dt
  '    CBPeriodo.ValueMember = "Codigo"
  '    CBPeriodo.DisplayMember = "Descripcion"
  '    CBPeriodo.Enabled = True
  'Else
  '    DvLP.RowFilter = "NumEmp = '" & CBNomEmp.SelectedValue.ToString & "'"
  '    'MsgBox(DvLP(0)("AnioIngreso"))
  '    dt = New DataTable("Tabla")
  '    dt.Columns.Add("Codigo")
  '    dt.Columns.Add("Descripcion")
  '    currentyear = Integer.Parse(DvLP(0)("AnioIngreso"))
  '    tope = Integer.Parse(DvLP(0)("Tope"))
  '    For y As Integer = currentyear To tope Step 1
  '        dr = dt.NewRow()
  '        dr("Codigo") = y
  '        dr("Descripcion") = y & " - " & y + 1
  '        dt.Rows.Add(dr)
  '    Next
  '    dr = dt.NewRow
  '    dr("Codigo") = 9999
  '    dr("Descripcion") = "TODOS(Historico)"
  '    dt.Rows.Add(dr)

  '    dr = dt.NewRow
  '    dr("Codigo") = 8888
  '    dr("Descripcion") = "Ultimos dos periodos"
  '    dt.Rows.Add(dr)

  '    CBPeriodo.DataSource = dt
  '    CBPeriodo.ValueMember = "Codigo"
  '    CBPeriodo.DisplayMember = "Descripcion"
  '    CBPeriodo.Enabled = True
  'End If
  'DvLP.RowFilter = String.Empty
  ' ''CBPeriodo.SelectedIndex = -1
  'CBPeriodo.SelectedValue = "8888"

 End Sub

 Private Sub Label2_Click(sender As Object, e As EventArgs)

 End Sub


 Private Sub CBNomEmp_KeyUp(sender As Object, e As KeyEventArgs) Handles CBNomEmp.KeyUp
  Try
   If e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z Or e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
    strTemp = CBNomEmp.Text
    If strTemp.Trim.CompareTo(String.Empty) = 0 Then
     DvLP.RowFilter = String.Empty
     DvLP.RowFilter = "NumEmp <> 1010"
    Else
     Dim strRowFilter As String = String.Concat("NomEmp LIKE '%", CBNomEmp.Text, "%' and NumEmp <> 1010 ")
     DvLP.RowFilter = strRowFilter
     'MsgBox(DvLP.Count)
     If DvLP.Count = 0 Then
      DvLP.RowFilter = "NumEmp = 1010"
     End If

    End If


    CBNomEmp.Text = ""
    CBNomEmp.Text = strTemp
    CBNomEmp.SelectionStart = strTemp.Length
    CBNomEmp.SelectedIndex = -1
    CBNomEmp.DroppedDown = True
    CBNomEmp.SelectedIndex = -1
    CBNomEmp.Text = ""
    CBNomEmp.Text = strTemp
    CBNomEmp.SelectionStart = strTemp.Length

   End If



   'DvClte.RowFilter = "Nombre2 like '%" & CmbCliente.Text & "%'"
   'CmbCliente.DroppedDown = True
  Catch ex As Exception
   'MsgBox("errror en filtro nuevo " & ex.Message)
  End Try
 End Sub

 Private Sub CBNomEmp_DropDown(sender As Object, e As EventArgs) Handles CBNomEmp.DropDown
  Me.Cursor = Cursors.Arrow

  If strTemp <> "" Then
   CBNomEmp.Text = strTemp
   CBNomEmp.SelectionStart = strTemp.Length
  End If

 End Sub

 Private Sub DGVacaciones_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVacaciones.CellContentClick
  If e.RowIndex >= 0 Then
   Try
    If Me.DGVacaciones.Columns(e.ColumnIndex).Name = "Accion" Then

     If (DGVacaciones.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Eliminar") Then
      'MsgBox("voy a surtir")
      If (MessageBox.Show("¿Esta seguro que desea eliminar el dia seleccionado?",
                                "Advertencia",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
       actualizar_registro(DGVacaciones.Rows(e.RowIndex))


       'Me.DGVResultado.Rows.Remove(DGVResultado.Rows(e.RowIndex))
       'TBDiasRest.Text = TBDiasRest.Text + 1
      End If
      'ElseIf (DGVacaciones.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Guardar") Then
      '    'MsgBox("voy a guardar")
      '    If (MessageBox.Show("¿Esta seguro que desea registrar termino de surtido de la orden " & DGVResultado.Rows(e.RowIndex).Cells("DocNum").Value.ToString & "?", _
      '            "Advertencia", _
      '            MessageBoxButtons.YesNo, _
      '            MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
      '        actualizar_registro(DGVacaciones.Rows(e.RowIndex))

      '        'Me.DGVResultado.Rows.Remove(DGVResultado.Rows(e.RowIndex))
      '        'TBDiasRest.Text = TBDiasRest.Text + 1
      '    End If
      'ElseIf DGVacaciones.Rows(e.RowIndex).Cells("Accion").Value.ToString = "Descartar" Then
      '    'MsgBox("descartar")
      '    If (MessageBox.Show("¿Esta seguro que desea descartar la orden " & DGVResultado.Rows(e.RowIndex).Cells("DocNum").Value.ToString & "?", _
      '            "Advertencia", _
      '            MessageBoxButtons.YesNo, _
      '            MessageBoxIcon.Question)) = MsgBoxResult.Yes Then
      '        If DGVacaciones.Rows(e.RowIndex).Cells("band").Value.ToString = "0" Then
      '            descartar_registro(DGVacaciones.Rows(e.RowIndex))
      '        Else
      '            descartar_registro2(DGVacaciones.Rows(e.RowIndex))
      '        End If


      '        'Me.DGVResultado.Rows.Remove(DGVResultado.Rows(e.RowIndex))
      '        'TBDiasRest.Text = TBDiasRest.Text + 1
      '    End If
     End If



     'MsgBox("voy a borrar el dia " & row.Cells("DiaSol").Value.ToString)

    End If
   Catch ex As Exception

   End Try
  End If
 End Sub

 Public Sub actualizar_registro(ByVal fila As DataGridViewRow)
  Dim d_aux As System.DateTime
  d_aux = fila.Cells("Dia Solicitado").Value
  Dim text As String = d_aux.ToString("yyyy-MM-dd")


  Dim strcadena As String = ""
  Try
   Dim SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)
   SqlConnection.Open()
   Dim command As New Data.SqlClient.SqlCommand
   command.Connection = SqlConnection
   'Dim dia_vac As Date = fila.Cells("Dia Solicitado").Value
   'MsgBox(fila.Cells("Dia Solicitado").Value.ToString() + " -> " + fila.Cells("Periodo2").Value.ToString + " -> " + fila.Cells("folio").Value.ToString)
   'MsgBox(text)


   strcadena = "delete from SolVacacionesHistorico where folio = " & fila.Cells("folio").Value.ToString & " and NumEmpleado = " & empleado
   strcadena &= " and Periodo = " & fila.Cells("Periodo2").Value.ToString & " and DiaSol = '" & text & "'"
   'strcadena = "update Analisis_Almac set HoraSurtido = GETDATE(), Status = 'Surtido' where DocNum = '" & fila.Cells("DocNum").Value.ToString & "'"
   command.CommandText = strcadena
   command.ExecuteNonQuery()

   MessageBox.Show("Dia eliminado correctamente",
                                 "Aviso.", MessageBoxButtons.OK,
                                 MessageBoxIcon.Information)


   Try
    Me.DGVacaciones.Rows.Remove(fila)

    'DGVResultado.CurrentCell = DGVResultado.Rows(0).Cells(0) 'aca entra'
    'DGVResultado.CurrentCell = DGVResultado.Rows(selected_row1).Cells(selected_column1) 'aca entra'
    'FiltraDetalle()

    'If Me.DGVResultado.Rows.Count = 0 Then
    '    Me.DGVDetalle.DataSource = Nothing
    'End If
   Catch ex As Exception

   End Try
  Catch ex As Exception
   MessageBox.Show("Ocurrio un Error: " & ex.Message,
                                 "ERROR.", MessageBoxButtons.OK,
                                 MessageBoxIcon.Error)
  End Try
 End Sub
End Class

'Imports System.Data.SqlClient

'Public Class VacacionesConsulta

'    Public conexion2 As New SqlConnection(StrTpm)

'    Public DvDetalle As New DataView
'    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
'        Try

'            conexion2.Open()
'            Dim cmd4 As SqlCommand = Nothing
'            cmd4 = New SqlCommand("SPConsultaVaca", conexion2)
'            cmd4.CommandType = CommandType.StoredProcedure

'            If TBNumEmp.Text = "" Then
'                cmd4.Parameters.Add("@NumEmp", SqlDbType.Int).Value = 0
'            Else
'                cmd4.Parameters.Add("@NumEmp", SqlDbType.Int).Value = TBNumEmp.Text
'            End If

'            cmd4.Parameters.Add("@NomEmp", SqlDbType.Int).Value = CBNomEmp.SelectedValue
'            cmd4.Parameters.Add("@Sucursal", SqlDbType.Int).Value = cmbAlmacen.SelectedValue
'            cmd4.Parameters.Add("@Periodo", SqlDbType.Int).Value = CBPeriodo.Text
'            'cmd4.Parameters.Add("@FechaFin", SqlDbType.Date).Value = DtpFechaTer.Value


'            cmd4.ExecuteNonQuery()
'            cmd4.Connection.Close()
'            Dim da2 As New SqlDataAdapter
'            da2.SelectCommand = cmd4
'            da2.SelectCommand.Connection = conexion2


'            ''--------------------------------------------
'            Dim DsVtas As New DataSet
'            da2.Fill(DsVtas, "DsVtas")

'            DsVtas.Tables(0).TableName = "Detalle"


'            DvDetalle.Table = DsVtas.Tables("Detalle")

'            DGVacaciones.DataSource = DvDetalle


'            'DisenoGrid()

'            '        Else
'            'MsgBox("No hay factura con este folio")
'            '        End If

'            DisenoGrid()
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        Finally
'            If conexion2 IsNot Nothing AndAlso conexion2.State <> ConnectionState.Closed Then
'                conexion2.Close()
'            End If
'        End Try
'    End Sub

'    Private Sub DisenoGrid()
'        With Me.DGVacaciones

'            '.DataSource = DtAgte
'            .ReadOnly = True
'            'Color de Renglones en Grid
'            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
'            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
'            .DefaultCellStyle.BackColor = Color.AliceBlue
'            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue


'            'Propiedad para no mostrar el cuadro que se encuentra en la parte
'            'Superior Izquierda del gridview
'            .RowHeadersVisible = True
'            .RowHeadersWidth = 25
'            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
'            'Color de linea del grid

'            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'            Try
'                .Columns(0).Name = "No. Empleado"
'                .Columns(0).HeaderText = "No. Empleado"
'                .Columns(0).Width = 50
'                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
'                .Columns(0).Frozen = True

'                .Columns(1).Name = "Nombre"
'                .Columns(1).HeaderText = "Nombre"
'                .Columns(1).Width = 160
'                .Columns(1).Frozen = True

'                .Columns(2).Name = "Fecha de Ing."
'                .Columns(2).HeaderText = "Fecha de Ingreso"
'                .Columns(2).Width = 75
'                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
'                .Columns(2).Frozen = True

'                .Columns(3).Name = "Antiguedad"
'                .Columns(3).HeaderText = "Antiguedad"
'                .Columns(3).Width = 70
'                .Columns(3).DefaultCellStyle.Format = "##0.##"
'                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
'                .Columns(3).Frozen = True

'                .Columns(4).Name = "Días de Vac."
'                .Columns(4).HeaderText = "Días de Vacaciones"
'                .Columns(4).Width = 70
'                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
'                .Columns(4).DefaultCellStyle.Format = "#0"
'                .Columns(4).Frozen = True

'                .Columns(5).Name = "Periodo Comp."
'                .Columns(5).HeaderText = "Periodo Comprendido"
'                .Columns(5).Width = 70
'                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
'                .Columns(5).Frozen = True
'                '.Columns(5).DefaultCellStyle.Format = "##0.#0 %"

'                .Columns(6).Name = "Días Gozados"
'                .Columns(6).HeaderText = "Días Gozados"
'                .Columns(6).Width = 70
'                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
'                .Columns(6).DefaultCellStyle.Format = "##0"
'                .Columns(6).Frozen = True

'                .Columns(7).Name = "Días Pendientes"
'                .Columns(7).HeaderText = "Días Pendientes"
'                .Columns(7).Width = 70
'                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
'                .Columns(7).DefaultCellStyle.Format = "##0"
'                .Columns(7).Frozen = True

'                .Columns(8).Name = "Inicio Vac."
'                .Columns(8).HeaderText = "Inicio Vac."
'                .Columns(8).Width = 70
'                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
'                .Columns(8).Frozen = True

'                .Columns(9).Name = "Caducidad Vac."
'                .Columns(9).HeaderText = "Caducidad Vac."
'                .Columns(9).Width = 70
'                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
'                .Columns(9).Frozen = True

'                .Columns(10).Name = "Día 1"
'                .Columns(10).HeaderText = "Día 1"
'                .Columns(10).Width = 70
'                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(11).Name = "Día 2"
'                .Columns(11).HeaderText = "Día 2"
'                .Columns(11).Width = 70
'                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(12).Name = "Día 3"
'                .Columns(12).HeaderText = "Día 3"
'                .Columns(12).Width = 70
'                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(13).Name = "Día 4"
'                .Columns(13).HeaderText = "Día 4"
'                .Columns(13).Width = 70
'                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(14).Name = "Día 5"
'                .Columns(14).HeaderText = "Día 5"
'                .Columns(14).Width = 70
'                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(15).Name = "Día 6"
'                .Columns(15).HeaderText = "Día 6"
'                .Columns(15).Width = 70
'                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(16).Name = "Día 7"
'                .Columns(16).HeaderText = "Día 7"
'                .Columns(16).Width = 70
'                .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(17).Name = "Día 8"
'                .Columns(17).HeaderText = "Día 8"
'                .Columns(17).Width = 70
'                .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(18).Name = "Día 9"
'                .Columns(18).HeaderText = "Día 9"
'                .Columns(18).Width = 70
'                .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(19).Name = "Día 10"
'                .Columns(19).HeaderText = "Día 10"
'                .Columns(19).Width = 70
'                .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(20).Name = "Día 11"
'                .Columns(20).HeaderText = "Día 11"
'                .Columns(20).Width = 70
'                .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(21).Name = "Día 12"
'                .Columns(21).HeaderText = "Día 12"
'                .Columns(21).Width = 70
'                .Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(22).Name = "Día 13"
'                .Columns(22).HeaderText = "Día 13"
'                .Columns(22).Width = 70
'                .Columns(22).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(23).Name = "Día 14"
'                .Columns(23).HeaderText = "Día 14"
'                .Columns(23).Width = 70
'                .Columns(23).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(24).Name = "Día 15"
'                .Columns(24).HeaderText = "Día 15"
'                .Columns(24).Width = 70
'                .Columns(24).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(25).Name = "Día 16"
'                .Columns(25).HeaderText = "Día 16"
'                .Columns(25).Width = 70
'                .Columns(25).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'                .Columns(26).Name = "Ubicación"
'                .Columns(26).HeaderText = "Ubicación"
'                .Columns(26).Width = 90
'                .Columns(26).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

'            Catch ex As Exception

'            End Try

'        End With
'    End Sub

'    Private Sub VacacionesConsulta_Load(sender As Object, e As EventArgs) Handles MyBase.Load

'        CBPeriodo.SelectedIndex = 1

'        Dim ConsutaLista As String

'        Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
'            mllenaComboAlmacen(SqlConnection)
'        End Using

'        Using SqlConnection As New Data.SqlClient.SqlConnection(StrTpm)

'            Dim DSetTablas As New DataSet
'            ConsutaLista = "SELECT NumEmp,NomEmp+' '+AppEmp+' '+ApmMat AS 'NomEmp' FROM Empleados ORDER BY NomEmp "
'            Dim daGEmpleado As New SqlClient.SqlDataAdapter(ConsutaLista, SqlConnection)

'            'Dim DSetTablas As New DataSet
'            daGEmpleado.Fill(DSetTablas, "Empleados")


'            'AGREGAR FILA
'            Dim fila As Data.DataRow

'            'Asignamos a fila la nueva Row(Fila)del Dataset
'            fila = DSetTablas.Tables("Empleados").NewRow

'            'Agregamos los valores a los campos de la tabla
'            fila("NomEmp") = ""
'            fila("NumEmp") = 9999

'            'Agregamos la fila que acabamos de crear a nuestra tabla del DataSet
'            DSetTablas.Tables("Empleados").Rows.Add(fila)

'            Me.CBNomEmp.DataSource = DSetTablas.Tables("Empleados")
'            Me.CBNomEmp.DisplayMember = "NomEmp"
'            Me.CBNomEmp.ValueMember = "NumEmp"
'            Me.CBNomEmp.SelectedValue = 9999

'        End Using
'    End Sub


'    Private Sub mllenaComboAlmacen(ByVal conexion As SqlConnection)
'        Try
'            Dim da As New SqlDataAdapter("SELECT GroupCode , GroupName " +
'                                         "FROM OCRG with (nolock) " +
'                                         "WHERE GroupType = 'C' ORDER BY GroupName ", conexion)

'            Dim ds As New DataSet
'            da.Fill(ds)
'            ds.Tables(0).Rows.Add(0, "TODAS")
'            Me.cmbAlmacen.DataSource = ds.Tables(0)
'            Me.cmbAlmacen.DisplayMember = "GroupName"
'            Me.cmbAlmacen.ValueMember = "GroupCode"

'            Me.cmbAlmacen.SelectedValue = 0

'        Catch ex As Exception

'        End Try

'    End Sub

'    Private Sub DGVacaciones_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DGVacaciones.RowPrePaint
'        DGVacaciones.Rows(e.RowIndex).Cells("Nombre").Style.BackColor = Color.LightGreen
'        DGVacaciones.Rows(e.RowIndex).Cells("Días de Vac.").Style.BackColor = Color.Gold
'        DGVacaciones.Rows(e.RowIndex).Cells("Días Pendientes").Style.BackColor = Color.Yellow

'        DGVacaciones.Rows(e.RowIndex).Cells("Inicio Vac.").Style.BackColor = Color.LightGray
'        DGVacaciones.Rows(e.RowIndex).Cells("Caducidad Vac.").Style.BackColor = Color.LightGray

'    End Sub

'    Private Sub bExcel_Click(sender As Object, e As EventArgs) Handles bExcel.Click
'        Try

'            Dim exApp As New Microsoft.Office.Interop.Excel.Application
'            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

'            'Añadimos el Libro al programa
'            exLibro = exApp.Workbooks.Add

'            ' ¿Cuantas columnas y cuantas filas?
'            Dim NCol As Integer = DGVacaciones.ColumnCount
'            Dim NRow As Integer = DGVacaciones.RowCount

'            fFormatoExcel(exLibro, NRow)

'            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
'            For i As Integer = 1 To NCol
'                exLibro.Worksheets("Hoja1").Cells.Item(4, i) = DGVacaciones.Columns(i - 1).Name.ToString
'            Next

'            For Fila As Integer = 0 To NRow - 1

'                For Col As Integer = 0 To NCol - 1
'                    exLibro.Worksheets("Hoja1").Cells.Item(Fila + 5, Col + 1) = DGVacaciones.Rows(Fila).Cells(Col).Value

'                Next
'                Estatus.Visible = True
'                ProgressBar1.Value = (Fila * 100) / NRow
'            Next
'            Estatus.Visible = False

'            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
'            exLibro.Worksheets("Hoja1").Rows.Item(4).Font.Bold = 1
'            exLibro.Worksheets("Hoja1").Rows.Item(4).HorizontalAlignment = 3
'            exLibro.Worksheets("Hoja1").Cells.Range("A4:AA4").Interior.ColorIndex = 15
'            exLibro.Worksheets("Hoja1").Rows.Item(4).WrapText = True
'            'exLibro.Worksheets("Hoja1").Columns.AutoFit()
'            exLibro.Worksheets("Hoja1").name = "Reporte de vacaciones"


'            'Aplicación visible
'            exLibro.Worksheets.Application.Visible = True

'            exLibro = Nothing
'            exApp = Nothing

'        Catch ex As Exception

'        End Try
'    End Sub

'    Private Sub fFormatoExcel(exLibro As Microsoft.Office.Interop.Excel.Workbook, NRow As Integer)
'        Try
'            ''Combinamos celdas
'            exLibro.Worksheets("Hoja1").Cells.Range("A1:B1").Merge(True)
'            exLibro.Worksheets("Hoja1").Cells.Range("A2:B2").Merge(True)
'            'exLibro.Worksheets("Hoja1").Cells.Range("A3:B3").Merge(True)

'            ''aplicamos un color de fondo ala celda o rango de celdas
'            exLibro.Worksheets("Hoja1").Cells.Range("A1").Interior.ColorIndex = 15
'            exLibro.Worksheets("Hoja1").Cells.Range("A2").Interior.ColorIndex = 15
'            'exLibro.Worksheets("Hoja1").Cells.Range("A3").Interior.ColorIndex = 15

'            ''Cambiamos orientacion ala hola
'            exLibro.Worksheets("Hoja1").Cells.Item(1, 1) = "Reporte de Vacaciones"
'            'exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Linea: " + cmbLinea.Text
'            exLibro.Worksheets("Hoja1").Cells.Item(2, 1) = "Fecha: " + Date.Now.ToShortDateString


'            exLibro.Worksheets("Hoja1").Cells.Item(1, 1).Font.Bold = 1
'            exLibro.Worksheets("Hoja1").Cells.Item(2, 1).Font.Bold = 1
'            exLibro.Worksheets("Hoja1").Cells.Item(4, 1).Font.Bold = 1
'            'exLibro.Worksheets("Hoja1").Cells.Item(5, 1).Font.Bold = 1

'            'exLibro.Worksheets("Hoja1").Columns(11).NumberFormat = "###.0000"
'            'exLibro.Worksheets("Hoja1").Columns("L:U").NumberFormat = "###.0000"
'            'exLibro.Worksheets("Hoja1").Columns("D:G").NumberFormat = "###,###,###"
'            'exLibro.Worksheets("Hoja1").Columns("H").NumberFormat = "$ ###,###.00"
'            'exLibro.Worksheets("Hoja1").Columns("J").NumberFormat = "$ ###,###.00"
'            'exLibro.Worksheets("Hoja1").Columns("L").NumberFormat = "$ ###,###.00"
'            exLibro.Worksheets("Hoja1").Columns("F").NumberFormat = "@"

'            exLibro.Worksheets("Hoja1").Columns("A").EntireColumn.ColumnWidth = 9
'            exLibro.Worksheets("Hoja1").Columns("B").EntireColumn.ColumnWidth = 28
'            exLibro.Worksheets("Hoja1").Columns("C").EntireColumn.ColumnWidth = 11
'            exLibro.Worksheets("Hoja1").Columns("D").EntireColumn.ColumnWidth = 11
'            exLibro.Worksheets("Hoja1").Columns("E").EntireColumn.ColumnWidth = 10
'            exLibro.Worksheets("Hoja1").Columns("F").EntireColumn.ColumnWidth = 9
'            exLibro.Worksheets("Hoja1").Columns("G").EntireColumn.ColumnWidth = 10

'            exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 11
'            exLibro.Worksheets("Hoja1").Columns("I").EntireColumn.ColumnWidth = 11
'            exLibro.Worksheets("Hoja1").Columns("J").EntireColumn.ColumnWidth = 11

'            exLibro.Worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 10


'            exLibro.Worksheets("Hoja1").Cells.Range("B5:B" + (NRow + 4).ToString).Interior.Color = RGB(198, 224, 180)
'            exLibro.Worksheets("Hoja1").Cells.Range("D5:D" + (NRow + 4).ToString).Interior.ColorIndex = 6
'            exLibro.Worksheets("Hoja1").Cells.Range("E5:E" + (NRow + 4).ToString).Interior.ColorIndex = 44
'            exLibro.Worksheets("Hoja1").Cells.Range("H5:H" + (NRow + 4).ToString).Interior.Color = RGB(189, 215, 238)

'            'oSheet.Range("A" & Con + 8 & ":" & "V" & Con + 8).INTERIOR.color = RGB(169, 208, 142)

'            'exLibro.Worksheets("Hoja1").Columns("K").EntireColumn.ColumnWidth = 6

'            'exLibro.Worksheets("Hoja1").Columns("H").EntireColumn.ColumnWidth = 8
'        Catch ex As Exception

'        End Try

'    End Sub

'End Class