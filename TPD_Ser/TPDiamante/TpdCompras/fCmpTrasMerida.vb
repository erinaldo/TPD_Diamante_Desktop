Public Class fCmpTrasMerida
    Private dvTrasMerida As New DataView

    '*****************************************************************************************************************************************************'/
    ''Nombre : Analisis Traspaso Merida 
    ''Fecha: 09/07/2015
    ''Autor: Ivan Cordero Ramos
    ''Descripcion: Pantalla que sirve para el analisis de traspaso de stock entre 
    ''             almacenes (Puebla → Merida)
    ''Departamento: Compras
    '******************************************************************************************************************************************************'/

#Region "Eventos"

    'Carga de formulario 
    Private Sub fCmpTrasMerida_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If UsrTPM = "MANAGER" Then
                Me.WindowState = FormWindowState.Maximized
            End If
            dtIni.Value = "01/01/2015"
            dtFin.Value = Date.Now
            mLineas()
        Catch ex As Exception

        End Try
    End Sub

    '' Boton consultar
    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try
            Dim sError As String
            sError = String.Empty
            If (fValidaDatos(sError)) Then
                mTraspasos()
            Else
                MsgBox("Verifique los siguientes campos: " + sError, MsgBoxStyle.Exclamation, "Tracto Partes Diamante")
            End If
        Catch ex As Exception

        End Try

    End Sub

    '' Check articulos con requerimiento
    Private Sub chcArticulosReq_CheckedChanged(sender As Object, e As EventArgs) Handles chcArticulosReq.CheckedChanged
        If chcArticulosReq.Checked = False Then
            dvTrasMerida.RowFilter = " [Requeri- miento] <> 0 "
            'dvTrasMerida.RowFilter = " [Requeri- miento] like '%' "
        End If

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Cconsulta y carga las linas al combo de lineas 
    ''' </summary>
    ''' <remarks>
    ''' Fecha 09/07/2015
    ''' Autor: Ivan Cordero
    ''' </remarks>
    Private Sub mLineas()
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
                Dim da As New SqlClient.SqlDataAdapter("SELECT ItmsGrpCod,ItmsGrpNam FROM OITB ", SqlConnection)

                Dim ds As New DataSet
                da.Fill(ds)
                ds.Tables(0).Rows.Add(0, "TODOS")
                Me.CmbLin.DataSource = ds.Tables(0)
                Me.CmbLin.DisplayMember = "ItmsGrpNam"
                Me.CmbLin.ValueMember = "ItmsGrpCod"
                Me.CmbLin.SelectedValue = 0

            End Using

        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' Metodo que realiza una consulta donde muestra la posibilidad de 
    ''' realizar traspasos de material de puebla → merida
    ''' </summary>
    ''' <remarks>
    ''' Fecha: 09/07/2015
    ''' Autor: Ivan Cordero Ramos
    ''' </remarks>
    Private Sub mTraspasos()
        Try
            Using SqlConnection As New Data.SqlClient.SqlConnection(StrCon)
                Dim cadena As String = String.Empty
                Dim da As New SqlClient.SqlDataAdapter(mCreaCadena(cadena), SqlConnection)
                Dim ds As New DataSet
                da.Fill(ds)
                dvTrasMerida.Table = ds.Tables(0)
                GridTM(DgRptTraspaso, dvTrasMerida)
                fFormatoGrid()
            End Using

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Metodo que crea la cadena de consulta y la regresa al sqlClient
    ''' </summary>
    ''' <param name="cadena"> parameto para la construccion de la cadena para TSQL</param>
    ''' <returns> cadena de consulta 
    ''' </returns>
    ''' <remarks>
    ''' Fecha:09/07/2015
    ''' Autor:Ivan Cordero Ramos
    ''' </remarks>
    Private Function mCreaCadena(ByRef cadena As String) As Object

        Dim iMes As Decimal
        iMes = DateDiff("d", dtIni.Value, dtFin.Value) / 30
        cadena = " declare @articulos table(Articulo varchar(50),Descripcion varchar(200),CodeLinea int,Linea varchar(50),VentasPue int,VentasMer int,ComprometidoPue int," +
                 "						AlmPue int,SolComprasPue int,ComprometidoMer int,AlmMer int,SolComprasMer int,[$ Precio L9] decimal (8,3)) " +
                 " declare @facturas table (Articulo varchar(50),Ventas int,almacen char(2)) " +
                 " declare @NtasCredic table (Articulo varchar(50),Ventas int,almacen char(2)) " +
                 " insert into @articulos (Articulo ,Descripcion,CodeLinea,Linea,VentasPue,VentasMer,ComprometidoPue,AlmPue,SolComprasPue,ComprometidoMer, " +
                 "						AlmMer,SolComprasMer,[$ Precio L9])" +
                 " select art.ItemCode articulo,art.ItemName descripcion,lin.ItmsGrpCod,lin.ItmsGrpNam,0,0,almP.IsCommited ComprometidoPue,almP.OnHand AlmPue,almP.Onorder SolComprasPue, " +
                 "	   almM.IsCommited ComprometidoMer,almM.OnHand AlmMer,almM.Onorder SolComprasMer,detart.Price " +
                 " from OITM art " +
                 "	inner join oitw almP on art.ItemCode=almP.ItemCode and almP.WhsCode=01 " +
                 "	inner join oitw almM on art.ItemCode=almM.ItemCode and almM.WhsCode=03 " +
                 "	inner join ITM1 detart on art.ItemCode =detart.ItemCode and detart.PriceList = 11 " +
                 "	inner join OITB lin on art.ItmsGrpCod= lin.ItmsGrpCod  	" +
                 " insert into @facturas(Articulo ,Ventas ,almacen) " +
                 " select det.ItemCode Articulo ,sum(isnull(det.Quantity,0)) Ventas,det.WhsCode almacena " +
                 " from    INV1 det " +
                 "		inner join OINV fact on det.DocEntry=fact.DocEntry " +
                 " where  fact.DocDate between '" + dtIni.Value.ToString("yyyy-MM-dd") + "' and '" + dtFin.Value.ToString("yyyy-MM-dd") + "' " +
                 " group by det.ItemCode,det.WhsCode " +
                 "        union all " +
                 " select det.ItemCode Articulo ,Sum(isnull(det.Quantity,0)) Ventas,det.WhsCode almacen " +
                 " from   [SBO-Diamante-productiva].dbo.INV1 det " +
                 "		inner join [SBO-Diamante-productiva].dbo.OINV fact on det.DocEntry=fact.DocEntry " +
                 " where  fact.DocDate between '" + dtIni.Value.ToString("yyyy-MM-dd") + "' and '" + dtFin.Value.ToString("yyyy-MM-dd") + "' " +
                 " group by det.ItemCode,det.WhsCode " +
                 " select Articulo,sum(ventas) Ventas,almacen " +
                 " into #FactConsol " +
                 " from @facturas " +
                 " group by Articulo,almacen " +
                 " insert into @NtasCredic(Articulo ,Ventas ,almacen) " +
                 " select detNc.ItemCode Articulo ,SUM(detNc.Quantity) Nc,detNc.WhsCode almacen " +
                 " from RIN1 detNc " +
                 " 	inner join ORIN Nc on Nc.DocEntry=detNc.DocEntry " +
                 " where nc.DocDate between '" + dtIni.Value.ToString("yyyy-MM-dd") + "' and '" + dtFin.Value.ToString("yyyy-MM-dd") + "' " +
                 " GROUP BY detNc.ItemCode,detNc.WhsCode " +
                 "        union all " +
                 " select detNc.ItemCode Articulo ,SUM(detNc.Quantity) Nc,detNc.WhsCode almacen " +
                 " from [SBO-Diamante-productiva].dbo.RIN1 detNc " +
                 " 	inner join [SBO-Diamante-productiva].dbo.ORIN Nc on Nc.DocEntry=detNc.DocEntry " +
                 " where nc.DocDate between '" + dtIni.Value.ToString("yyyy-MM-dd") + "' and '" + dtFin.Value.ToString("yyyy-MM-dd") + "' " +
                 " GROUP BY detNc.ItemCode,detNc.WhsCode " +
                 " select Articulo,sum(ventas) Ventas,almacen " +
                 " into #NtasCredicConsol " +
                 " from @NtasCredic " +
                 " group by Articulo,almacen " +
                 " update @articulos " +
                 "      set  VentasPue = fact.ventas " +
                 " from @articulos art 	" +
                 "		inner join #FactConsol fact on art.articulo=fact.articulo and fact.almacen=01 " +
                 " update @articulos " +
                 "      set VentasMer = fact.ventas " +
                 " from @articulos art 	" +
                 "		inner join #FactConsol fact on art.articulo=fact.articulo and fact.almacen=03 " +
                 " update @articulos " +
                 "      set VentasPue = art.VentasPue - nc.ventas " +
                 " from @articulos art 	" +
                 "		inner join #NtasCredicConsol nc on art.articulo=nc.articulo and nc.almacen=01 " +
                 " update @articulos " +
                 "      set VentasMer = art.VentasMer - nc.ventas " +
                 " from @articulos art 	" +
                 "		inner join #NtasCredicConsol nc on art.articulo=nc.articulo and nc.almacen=03 " +
                " select Articulo,Descripcion,Linea,[$ Precio L9],VentasMer [Vta Neta],convert(int,round(VentasMer/" + iMes.ToString + ",-0.01)) [Venta Mens.],ComprometidoMer [Compro- metido]," +
                "		SolComprasMer [Soli- citado],AlmMer Stock,Convert(int,Round(((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ",-0.01)) [Stock Ideal]," +
                "		convert(int,round(case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end,-0.01)) [Requeri- miento], " +
                "		VentasPue [Vta Neta],convert(int,Round(VentasPue/" + iMes.ToString + ",-0.01)) [Venta Mens.],ComprometidoPue [Compro- metido],SolComprasPue Solicitado,AlmPue Stock," +
                "		Convert(int,Round(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ",-0.01)) [Stock Ideal]," +
                "		convert(int,round(case when AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")>=0 then " +
                "							  AlmPue-(((VentasPue/" + iMes.ToString + ")/30)* " + txtDiasSP.Text + " )+ ComprometidoPue else ComprometidoPue end,-0.01)) [Cant. Trans- ferible]," +
                "		CASE when convert(int,round(case when AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")>=0 then " +
                "							  AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")+ ComprometidoPue else ComprometidoPue end,-0.01))>" +
                "		convert(int,round(case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end,-0.01)) then " +
                "		convert(int,round(case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end,-0.01)) else " +
                "		convert(int,round(case when AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")>=0 then " +
                "							  AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")+ ComprometidoPue else ComprometidoPue end,-0.01))	 end [Posi- bilidad de tras- paso]," +
                "		convert(int,round(CASE when convert(int,round(case when AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")>=0 then " +
                "							  AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")+ ComprometidoPue else ComprometidoPue end,-0.01))>" +
                "		convert(int,round(case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end,-0.01)) then " +
                "		convert(int,round(case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end,-0.01)) else " +
                "		convert(int,round(case when AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")>=0 then " +
                "							  AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")+ ComprometidoPue else ComprometidoPue end,-0.01))	 end /" +
                "							  case when convert(int,round(case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end,-0.01))=0 then 1.00 else " +
                "							 convert(int,round(case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end,-0.01)) end ,-0.01)) [Cumpli- miento]," +
                "		case when convert(int,round(case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end,-0.01))=0 then 0 else " +
                "							 convert(int,round(case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end,-0.01)) end - " +
                "		CASE when convert(int,round(case when AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")>=0 then " +
                "							  AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")+ ComprometidoPue else ComprometidoPue end,-0.01))>" +
                "							convert(int,round(case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end,-0.01)) then " +
                "							convert(int,round(case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end,-0.01)) else " +
                "							convert(int,round(case when AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")>=0 then " +
                "							  AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")+ ComprometidoPue else ComprometidoPue end,-0.01))	 end [Tras- paso BO], " +
                "							 (case when (case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end)=0 then 0 else " +
                "							 case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end end - " +
                "		CASE when case when AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")>=0 then " +
                "							  AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")+ ComprometidoPue else ComprometidoPue end>" +
                "							case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end then " +
                "							case when (((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer>=0 then " +
                "							 ((((VentasMer/" + iMes.ToString + ")/30)*" + txtDiasSM.Text + ")-AlmMer)+ ComprometidoMer else  ComprometidoMer end else " +
                "							case when AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")>=0 then " +
                "							  AlmPue-(((VentasPue/" + iMes.ToString + ")/30)*" + txtDiasSP.Text + ")+ ComprometidoPue else ComprometidoPue end	 end)*[$ Precio L9] [Monto BO] " +
                " from @articulos art"
        If CmbLin.SelectedValue <> "0" Then
            cadena &= " where art.CodeLinea= " + CmbLin.SelectedValue.ToString
        End If
        cadena &= " order by Articulo	" +
                " Drop table #FactConsol" +
                " Drop table #NtasCredicConsol"
        Return cadena
    End Function


#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion que valida los dias de inventario depuebla y merida 
    ''' </summary>
    ''' <param name="sError"> Variable declara para recopilar los error de la validacion
    ''' </param>
    ''' <returns>Mensaje de error
    ''' </returns>
    ''' <remarks>
    ''' Fecha 09/07/2015
    ''' Autor: Ivan Cordero Ramos
    ''' 
    ''' </remarks>
    Private Function fValidaDatos(ByRef sError) As Boolean
        Try
            Dim bValido As Boolean
            bValido = True
            If (txtDiasSP.Text = String.Empty Or txtDiasSP.Text = "0") Then
                sError = sError + Environment.NewLine + "-Dias de inventario Puebla."
                bValido = False
                eInvP.Visible = True
            Else
                eInvP.Visible = False
            End If


            If (txtDiasSM.Text = String.Empty Or txtDiasSM.Text = "0") Then
                sError = sError + Environment.NewLine + "-Dias de inventario Merida."
                bValido = False
                eInvM.Visible = True
            Else
                eInvM.Visible = False
            End If
            Return bValido
        Catch ex As Exception
            MsgBox("Error al realizar alguna validacion", MsgBoxStyle.Critical, "Tracto Partes Diamante")
        End Try

    End Function

    ''' <summary>
    ''' Funcion que da formato al grid 
    ''' </summary>
    ''' <remarks>
    ''' Fecha: 10/07/2015
    ''' Autor: Ivan Cordero Ramos
    ''' </remarks>
    Private Sub fFormatoGrid()
        Try
            With DgRptTraspaso
                'Articulo	
                .Columns(0).Width = 94

                'Descripcion	
                .Columns(1).HeaderText = "Descripcion"
                .Columns(1).Width = 218

                'Linea	
                .Columns(2).Width = 74

                'Precio L9
                .Columns(3).Width = 79
                .Columns(3).DefaultCellStyle.Format = "$ ###,###.00"
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Vta Neta
                .Columns(4).Width = 44
                .Columns(4).DefaultCellStyle.Format = "###,###"
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Venta Mensual
                .Columns(5).Width = 44
                .Columns(5).DefaultCellStyle.Format = "###,###"
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' Comprometido
                .Columns(6).Width = 44
                .Columns(6).DefaultCellStyle.Format = "###,###"
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ' "Solicitado"
                .Columns(7).Width = 44
                .Columns(7).DefaultCellStyle.Format = "###,###"
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'StockM
                .Columns(8).Width = 44
                .Columns(8).DefaultCellStyle.Format = "###,###"
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Stock Ideal
                .Columns(9).Width = 44
                .Columns(9).DefaultCellStyle.Format = "###,###"
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '' Requerimiento
                .Columns(10).Width = 44
                .Columns(10).DefaultCellStyle.Format = "###,###"
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '' Vta. TotalP
                .Columns(11).Width = 44
                .Columns(11).DefaultCellStyle.Format = "###,###"
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '' Vta Mens
                .Columns(12).Width = 44
                .Columns(12).DefaultCellStyle.Format = "###,###"
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '' Comprometido
                .Columns(13).Width = 44
                .Columns(13).DefaultCellStyle.Format = "###,###"
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                '' Solicitado
                .Columns(14).Width = 44
                .Columns(14).DefaultCellStyle.Format = "###,###"
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'Stock
                .Columns(15).Width = 44
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(15).DefaultCellStyle.Format = "###,###"

                'Stock Ideal                                                                 
                .Columns(16).Width = 44
                .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(16).DefaultCellStyle.Format = "###,###"

                'Cant Tranferible                                                            
                .Columns(17).Width = 44
                .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(17).DefaultCellStyle.Format = "###,###"

                'Cumplimiento                                                                
                .Columns(18).Width = 44
                .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(18).DefaultCellStyle.Format = "###,###"

                'Stock                                                                       
                .Columns(19).Width = 44
                .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(19).DefaultCellStyle.Format = "% ###"

                'Traspaso BO                                                                 
                .Columns(20).Width = 44
                .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(20).DefaultCellStyle.Format = "###,###"

                'Monto BO                                                                    
                .Columns(21).Width = 75
                .Columns(21).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(21).DefaultCellStyle.Format = "$ ###,###.00"

            End With
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Funcion que aplica el estilo y colores al grid
    ''' </summary>
    ''' <param name="Grid">nombre del grid a aplicar el formato</param>
    ''' <param name="ds"> tabla a vaciar el grid</param>
    ''' <returns></returns>
    ''' Fecha:10/07/2015
    ''' Autor: Ivan Cordero Ramos
    ''' <remarks></remarks>
    Public Function GridTM(ByVal Grid As DataGridView, ByRef ds As DataView) As Boolean

        With Grid
            .DataSource = ds
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
            .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .RowHeadersVisible = True
            .SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
            .MultiSelect = True
            .AllowUserToAddRows = False
            .ReadOnly = True
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 39
            .RowHeadersWidth = 25
        End With

    End Function

#End Region



    Private Sub DgRptTraspaso_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DgRptTraspaso.RowPrePaint
        Select Case DgRptTraspaso.Rows(e.RowIndex).Cells("Cumpli- miento").Value
            Case 0 To 0.49
                DgRptTraspaso.Rows(e.RowIndex).Cells("Cumpli- miento").Style.BackColor = Color.Red
            Case 50 To 79
                DgRptTraspaso.Rows(e.RowIndex).Cells("Cumpli- miento").Style.BackColor = Color.Yellow
            Case 80 To 100
                DgRptTraspaso.Rows(e.RowIndex).Cells("Cumpli- miento").Style.BackColor = Color.Green
        End Select
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New DiasInv()
        frm.Show()
    End Sub

    Private Sub bExcel_Click(sender As Object, e As EventArgs) Handles bExcel.Click

    End Sub
End Class