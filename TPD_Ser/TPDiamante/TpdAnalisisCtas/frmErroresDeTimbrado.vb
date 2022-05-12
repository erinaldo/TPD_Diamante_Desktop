Imports System.Data.SqlClient
Public Class frmErroresDeTimbrado
    Dim ResultadoError As DataView
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvErroresTimbrado.CellContentClick

    End Sub

    Private Sub frmErroresDeTimbrado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LlenarDGVError()
        EstiloErortimbrado()
        Timer1.Interval = 90000
        'Activamos el evento timer para que el grid se actualize solo 
        Timer1.Enabled = True

    End Sub
    Sub LlenarDGVError()
        'llena a travez de una consulta de sql el gridview detalle
        Try
            'VARIABLE DE CADENA DE SQL
            Dim SQLOrdenes As String
            'VARIABLES DE CONEXION DE LLENADO
            Dim cmd As SqlCommand
            Dim cnn As SqlConnection = Nothing
            Dim da As SqlDataAdapter
            Dim DsOrdenes = New DataSet

            SQLOrdenes = "select t1.DocNum as NumeroDocumento,t1.CardCode as CodigoCliente,t1.CardName as NombreCliente ,"
            SQLOrdenes &= "T1.DocDate as FechaContabilizacion ,T4.CreateDate as FechaTimbrado,t1.DocTotal as TotalSinIva,"
            SQLOrdenes &= "(select eccc2.ReportID from SBO_TPD.dbo.ECM2 eccc2 where eccc2.SrcObjAbs = t1.DocEntry and eccc2.SrcObjType=13) as Timbrado, "

            SQLOrdenes &= "IIF( t1.ObjType=13,'FACTURA','FACTURA') as TipoDocumento "
            SQLOrdenes &= "from SBO_TPD.dbo.OINV t1 "
            SQLOrdenes &= "inner JOIN SBO_TPD.   dbo.ECM2 T4 ON T1.DocEntry= T4.SrcObjAbs where DocDate >= '2019-02-01' and (EDocGenTyp = 'N' or EDocGenTyp = 'L') and DocStatus <> 'C' and ObjType = 13   "
            SQLOrdenes &= "AND (U_BXP_TIMBRADO = 'P' OR U_BXP_TIMBRADO = 'E') "
            SQLOrdenes &= "and (select eccc2.ReportID from SBO_TPD.dbo.ECM2 eccc2 where eccc2.SrcObjAbs = t1.DocEntry and eccc2.SrcObjType=13) is  null "
            SQLOrdenes &= "and T1.UserSign=26 "
            SQLOrdenes &= "UNION "

            SQLOrdenes &= "select T1.DocNum,T1.CardCode,T1.CardName, T1.DocDate,T1.TaxDate,T1.DocTotal,(select eccc2.ReportID from SBO_TPD.dbo.ECM2 eccc2 where eccc2.SrcObjAbs = t1.DocEntry and eccc2.SrcObjType=14), "
            SQLOrdenes &= "  IIF( T1.ObjType=14,'NC','NC') as TipoDocumento "
            SQLOrdenes &= " from SBO_TPD.dbo.ORIN T1 left JOIN SBO_TPD.dbo.ECM2 T4 ON T4.SrcObjAbs  = T1.DocEntry where (T1.DocDate >= '2019-02-01') and (T1.EDocGenTyp = 'N' or T1.EDocGenTyp = 'L') and (T1.ObjType = 14) AND "
            SQLOrdenes &= "(T1.U_BXP_TIMBRADO = 'P' OR T1.U_BXP_TIMBRADO = 'E') AND (t4.ReportID is null  and t4.SrcObjType =14) AND  T1.DocNum NOT IN(2027428,2027466)  and T1.UserSign=26  "

            SQLOrdenes &= "Union "


            SQLOrdenes &= "select DocNum,CardCode,CardName,DocDate,TaxDate,DocTotal, T4.ReportID,IIF( ObjType=24,'PAGOS','PAGOS') as TipoDocumento "
            SQLOrdenes &= "from SBO_TPD.dbo.ORCT T0 inner JOIN SBO_TPD.dbo.ECM2 T4 ON T0.DocEntry= T4.SrcObjAbs "
            SQLOrdenes &= "where T0.TaxDate >= '2019-02-01' and T0.DocType = 'C' and Canceled = 'N'  and  T4.SrcObjType = 24 AND T4.ReportID IS  NULL and Series = 85 "
            SQLOrdenes &= "AND (U_BXP_TIMBRAPARC = 'E' OR U_BXP_TIMBRAPARC = 'P' OR U_BXP_TIMBRAPARC = 'N')  "
            SQLOrdenes &= "and  (T0.DocEntry not in(select SrcObjAbs from  SBO_TPD.dbo.ECM2 where CreateDate >= '2019-02-01' and SrcObjType = 24) ) "

            SQLOrdenes &= "Union "


            SQLOrdenes &= "select DocNum,CardCode,CardName,DocDate,TaxDate,DocTotal,(select eccc2.ReportID from SBO_TPD.dbo.ECM2 eccc2 where eccc2.SrcObjAbs = t0.DocEntry and eccc2.SrcObjType=24 and t0.series=17),IIF( ObjType=24 and series=17,'PAGOS PRIMARIOS ',' ') as TipoDocumento  "
            SQLOrdenes &= "from SBO_TPD.dbo.ORCT T0 INNER JOIN SBO_TPD.dbo.ECM2 T1 ON T0.DocEntry=T1.SrcObjAbs "
            SQLOrdenes &= "where T1.SrcObjType = 24 AND T1.ReportID IS  null  and t0.Series = 17 and T0.TaxDate >= '2019-02-01' and T0.DocType = 'C' and Canceled = 'N' "
            SQLOrdenes &= "AND (U_BXP_TIMBRAPARC = 'E' OR U_BXP_TIMBRAPARC = 'P')  "
            SQLOrdenes &= "AND (T0.DocEntry NOT IN(select SrcObjAbs from  SBO_TPD.dbo.ECM2 where CreateDate >= '2019-02-01' and SrcObjType = 24))"



            cnn = New SqlConnection(StrCon)
            'ALMACENA LA CONSULTA EN UN COMMAND SQL
            cmd = New SqlCommand(SQLOrdenes, cnn)
            'CONVIERTE EL TEXTO EN CONSULTA
            cmd.CommandType = CommandType.Text
            'APERTURA LA CONEXION
            cnn.Open()
            'INSTANCIA UN ADAPTER
            da = New SqlDataAdapter
            'ALMACENA EL COMMAND DE SQL EN EL ADAPTER
            da.SelectCommand = cmd
            'LO EJECUTA CON LA CONEXION
            da.SelectCommand.Connection = cnn
            'TIEMPO DE ESPERA DE LA CONEXION
            'da.SelectCommand.CommandTimeout = 10000
            'EJECUTA LA CONSULTA
            cmd.ExecuteNonQuery()
            'CIERRA EL COMMAND DE SQL
            cmd.Connection.Close()
            'CIERRA LA CONEXION
            cnn.Close()
            'LLENA EL ADAPTER A UN DATA SET
            da.Fill(DsOrdenes)
            'INSTANCIA EL DATA VIEW EN VARIABLES RESULTADO
            ResultadoError = New DataView
            'ALMACENA EN DATA SET DE MODO TABLA
            ResultadoError.Table = DsOrdenes.Tables(0)
            'COLOCA EN NADA EL DATASOURCE DEL DATA GRID
            dgvErroresTimbrado.DataSource = Nothing
            'ALMACENA EN EL DATASOURCE DEL DATAGRID EL DATAVIEW
            dgvErroresTimbrado.DataSource = ResultadoError
            'MANDA A LLAMAR EL ESTILO DEL DATA GRID VIEW DE ORDENES
            EstiloErortimbrado()
        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)
        End Try
    End Sub
    Sub EstiloErortimbrado()
        'Este metodo cambia el estilo del gridview detalle 
        With Me.dgvErroresTimbrado
            'COLOCA PROPIEDADES DE COLOR ALTERNADOS
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.SteelBlue
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Numero De documento 
            .Columns("NumeroDocumento").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter
            .Columns("NumeroDocumento").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("NumeroDocumento").HeaderText = "Numero Documento"
            .Columns("NumeroDocumento").Width = 85
            .Columns("NumeroDocumento").ReadOnly = True
            'Codigo del cliente
            .Columns("CodigoCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("CodigoCliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("CodigoCliente").HeaderText = "Codigo Cliente"
            .Columns("CodigoCliente").Width = 75
            .Columns("CodigoCliente").ReadOnly = True
            'Nombre del cliente
            .Columns("NombreCliente").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns("NombreCliente").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("NombreCliente").HeaderText = "Nombre Cliente"
            .Columns("NombreCliente").Width = 150
            .Columns("NombreCliente").ReadOnly = True         
            'Fecha de contabilizacion
            .Columns("FechaContabilizacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaContabilizacion").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("FechaContabilizacion").HeaderText = "Fecha Contabilizacion"            
            .Columns("FechaContabilizacion").Width = 90
            .Columns("FechaContabilizacion").ReadOnly = True            
            'Fecha de tiembrado 
            .Columns("FechaTimbrado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("FechaTimbrado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("FechaTimbrado").HeaderText = "Fecha Timbrado"
            .Columns("FechaTimbrado").Width = 90
            .Columns("FechaTimbrado").ReadOnly = True
            .Columns("FechaTimbrado").SortMode = DataGridViewColumnSortMode.NotSortable
            'TOtal sin iva
            .Columns("TotalSinIva").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("TotalSinIva").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("TotalSinIva").HeaderText = "TotalSinIva"
            .Columns("TotalSinIva").Width = 100
            .Columns("TotalSinIva").ReadOnly = True
            .Columns("TotalSinIva").SortMode = DataGridViewColumnSortMode.NotSortable
            'Timbrado 
            .Columns("Timbrado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Timbrado").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("Timbrado").HeaderText = "Timbrado"
            .Columns("Timbrado").Width = 180
            .Columns("Timbrado").ReadOnly = True
            .Columns("Timbrado").SortMode = DataGridViewColumnSortMode.NotSortable
            'Tipo de Documento
            .Columns("TipoDocumento").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("TipoDocumento").DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            .Columns("TipoDocumento").HeaderText = "Tipo Documento"
            .Columns("TipoDocumento").Width = 120
            .Columns("TipoDocumento").ReadOnly = True
            .Columns("TipoDocumento").SortMode = DataGridViewColumnSortMode.NotSortable
        End With
    End Sub
    Private Sub btnActualizarERR_Click(sender As Object, e As EventArgs) Handles btnActualizarERR.Click
        LlenarDGVError()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            LlenarDGVError()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class