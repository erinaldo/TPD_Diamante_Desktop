Imports System.Data.SqlClient
Imports System.Data
Public Class CtrlBO

    Private Sub BtnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGuardar.Click
        If TxtFactura.Text.Trim = "" Then
            MessageBox.Show("")
        End If
        VerificaBackOrder()

    End Sub
    Private Sub VerificaBackOrder()
        Dim VinsBorder As Integer = 0 'Si se insertan registros en la tabla BOrder su valor es 1 de lo contrario 0
        'Conexión TPM
        Dim ConTPM As New SqlConnection(StrTpm)

        'SE AGREGO ESTA LINEA 16/08/2018 : URIEL ==============================INICIO
        'CONEXION TPD PARA OBTENER EL USUARIO Y ALMACEN PARA REGISTRAR BO
        Dim Whscode As String
        Dim ConTPM2 As New SqlConnection(StrTpm)
        Dim ComTPD2 As New SqlCommand
        With ComTPD2
            .CommandText = "select Almacen from Usuarios where boAutorisa = 1 and Id_Usuario = '" & UsrTPM.ToString() & "'"
            .CommandType = CommandType.Text
            .Connection = ConTPM2
            .Connection.Open()
            Whscode = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
            .Connection.Close()
        End With
        'SE AGREGO ESTA LINEA 16/08/2018 : URIEL ============================== FIN


        Dim ComTPM As New SqlCommand
        Dim EncFact As Integer
        'VALIDA QUE NO EXISTA UNA FACTURA YA REGISTRADA
        With ComTPM
            .CommandText = "SELECT count(*) AS Registro FROM BOrder Where FacBO ='" & TxtFactura.Text.Trim & "'"
            .CommandType = CommandType.Text
            'New Data.SqlClient.SqlConnection(StrTpm)
            .Connection = ConTPM
            .Connection.Open()
            EncFact = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
            .Connection.Close()
        End With

        If EncFact > 0 Then
            MessageBox.Show("La factura ya existe", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        'SE AGREGA LINEAS PARA VALIDAR QUE EL ALMACEN DEL USUARIO REGISTRANTE SEA EL MISMO QUE EL PEDIDO ===================================================INICIO
        'URIEL: MODIFICACIÓN 25/09/2018
        'Dim conexionTemp As New SqlConnection(StrCon)
        'Dim AdaptadorTemp As New SqlDataAdapter()
        'Dim CmdTemp As New SqlCommand
        'Dim DtTemp As New DataTable
        'Dim DrTemp As Data.SqlClient.SqlDataReader
        'Dim SQLTemp As String

        'SQLTemp = "Select Case Top 1 T0.DocNum As Factura, T1.BaseEntry As OrdenVta, T2.WhsCode As Almacen "
        'SQLTemp &= "From OINV T0 INNER Join INV1 T1 ON T0.DocEntry = T1.DocEntry "
        'SQLTemp &= "Left Join RDR1 T2 ON T1.BaseEntry = T2.DocEntry "
        'SQLTemp &= "WHERE T0.DocNum = '" + TxtFactura.Text + "' "
        'With CmdTemp
        '    .CommandText = SQLTemp
        '    .Connection = conexionTemp
        '    DrTemp = .ExecuteReader()
        'End With
        'With AdaptadorTemp
        '    .SelectCommand = CmdTemp
        '    .Fill(DtTemp)
        'End With
        ''SI ENCONTRO RESULTADOS ENTRA
        'If DtTemp.Rows.Count > 0 Then
        '    'VALIDA SI EL PEDIDO TIENE EL MISMO ALMACÉN QUE EL USUARIO
        '    If DrTemp.Item("Almacen") <> Whscode Then
        '        'MANDA MENSAJE DE ERROR
        '        MsgBox("El almacén del Pedido no corresponde al Usuario actual.", MsgBoxStyle.Exclamation, "Almacén Invalido")
        '        Return
        '    End If
        'End If
        'SE AGREGA LINEAS PARA VALIDAR QUE EL ALMACEN DEL USUARIO REGISTRANTE SEA EL MISMO QUE EL PEDIDO ===================================================FIN

        ' crear nueva conexión    
        Dim conexion2 As New SqlConnection(StrCon)

        ' abrir la conexión con la base de datos   
        conexion2.Open()

        Dim Adaptador As New SqlDataAdapter()

        Dim SQLTPD As String
        'ICR_27042015 Se agrega el Id_usuario de la sesion  y el alamacen de la orden de venta
        'Se buscan los articulos de la factura y se obtiene la fecha de la orden de venta de los artículos
        SQLTPD = "SELECT T0.CardCode AS CodClte,T0.CardName AS Cliente,SlpName AS Agente,t5.Docentry AS OrdVta,"
        SQLTPD &= "T5.DocDate as FchOrdVta,T0.DocNum AS FacBO,T0.DocDate AS FchFactBO,T1.ItemCode AS Articulo,T1.Dscription AS Descripcion,"
        SQLTPD &= "T4.ItmsGrpNam AS Linea,T5.Quantity AS PedClte,"
        SQLTPD &= "(SELECT SUM(T8.Quantity) AS Facturado FROM INV1 T8 INNER JOIN OINV T9 ON  T8.DocEntry = T9.DocEntry "
        SQLTPD &= "WHERE T8.BaseEntry = T1.BaseEntry AND t9.DocNum <> '" & TxtFactura.Text.Trim & "' "
        SQLTPD &= "AND T8.ItemCode = T1.ItemCode) AS Facturado,"
        SQLTPD &= "T1.Quantity AS CantBO,T1.Price AS Precio,T1.LineTotal AS TotalBO, 0 AS NoBO,T3.ItmsGrpCod AS IdLinea,"
        SQLTPD &= "T0.SlpCode as IdAgente,MONTH(GETDATE()) AS Mes,YEAR(GETDATE()) AS Anio,GETDATE() AS FchAct,"
        SQLTPD &= " '" + UsrTPM.ToString() + "' as VtasUsuario, T1.WhsCode Almacen "
        SQLTPD &= " FROM OINV T0 "
        SQLTPD &= "INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry INNER JOIN OSLP T2 ON T2.SlpCode = T0.SlpCode "
        SQLTPD &= "INNER JOIN OITM T3 ON T1.ItemCode = T3.ItemCode "
        SQLTPD &= "INNER JOIN OITB T4 ON T3.ItmsGrpCod = T4.ItmsGrpCod "
        SQLTPD &= "LEFT JOIN RDR1 T5 ON (T1.BaseEntry = T5.TrgetEntry OR T1.DocEntry = T5.TrgetEntry) AND T1.ItemCode = T5.ItemCode "
        'SE CAMBIA ESTA LINEA POR LA EXCEPCIÓN DE LA LINEA DE OTROS SERVICIOS: MODIFICADO URIEL 19/09/2018
        'SQLTPD &= "WHERE T0.DocNum = '" & TxtFactura.Text.Trim & "'" & " AND T1.ItemCode <> 'FL-001' AND T1.ItemCode <> 'FL-002'"
        SQLTPD &= "WHERE T0.DocNum = '" & TxtFactura.Text.Trim & "'" & " AND T3.ItmsGrpCod <> 150 "

        Dim comando As New SqlCommand


        With comando
            ' Asignar el sql para seleccionar los datos
            .CommandText = SQLTPD
            .Connection = conexion2
        End With

        Dim DtArtBo As New DataTable


        With Adaptador
            .SelectCommand = comando
            .Fill(DtArtBo)
        End With

        If DtArtBo.Rows.Count > 0 Then
            'Se recorren cada uno de los articulos de la factura y se busca la última transacción del articulo en el inventario
            'de acuerdo a la fecha menor o igual a la fecha de la orden de venta
            'odenando de forma descendente las fechas para obtener el último movimiento del inventario mas cercano a la fecha de la orden de venta

            For Each fila As DataRow In DtArtBo.Rows

                SQLTPD = " SELECT TOP 1 T0.DocDate AS FchBalance,T0.Balance,T0.ItemCode,T0.TransNum FROM oinm T0 "
                SQLTPD &= " WHERE T0.ItemCode = '"
                SQLTPD &= fila("Articulo") & "' "
                ' ICR 08052015 Se agrega el filtro por almacen 
                'SE AGREGA UNA LINEA Y SE COMENTA LA ORIGINARL 16/08/2018: URIEL =============================== INICIO
                SQLTPD &= " and T0.Warehouse= '" & Whscode & "'"
                'SQLTPD &= " and T0.Warehouse= '" & fila("Almacen") & "'"
                'SE AGREGA UNA LINEA Y SE COMENTA LA ORIGINARL 16/08/2018: URIEL =============================== FIN
                SQLTPD &= " AND T0.DocDate <= @Fecha AND TransType <> 18 AND TransType <> 69 ORDER BY T0.DocDate DESC"

                Dim DrValBO As Data.SqlClient.SqlDataReader

        With comando
          .Parameters.Clear()
          ' Asignar el sql para seleccionar los datos de la tabla Maestro   
          .Parameters.AddWithValue("@Fecha", fila("FchOrdVta"))
          .CommandText = SQLTPD
          .Connection = conexion2
          DrValBO = .ExecuteReader()
        End With


        'Adaptador TPM
        Dim AdapTPM As New SqlDataAdapter()

                DrValBO.Read()

                Dim ValReader As Decimal

                If DrValBO.HasRows = True Then
                    ValReader = DrValBO.Item("Balance")
                Else
                    ValReader = 0
                End If


                'Se busca si existio alguna transacción de inventario cero en la fecha más cercana a la fecha del pedido 
                If ValReader > 0 Then
                    SQLTPD = "SELECT TOP 1 T0.DocDate,T0.Balance,T0.ItemCode,T0.TransNum FROM oinm T0 "
                    SQLTPD &= "WHERE T0.ItemCode = '"
                    SQLTPD &= fila("Articulo") & "'"
                    ' ICR 08052015 Se agrega el filtro por almacen 
                    'SE AGREGA UNA LINEA Y SE COMENTA LA ORIGINARL 16/08/2018: URIEL =============================== INICIO
                    SQLTPD &= " and T0.Warehouse= '" & Whscode & "'"
                    'SQLTPD &= " and T0.Warehouse= '" & fila("Almacen") & "'"
                    'SE AGREGA UNA LINEA Y SE COMENTA LA ORIGINARL 16/08/2018: URIEL =============================== FIN
                    SQLTPD &= " AND T0.DocDate = @Fecha AND T0.Balance <= 0 AND TransType <> 18 AND TransType <> 69 ORDER BY T0.DocDate DESC"

                    Dim VarFchInv As Date

                    VarFchInv = DrValBO.Item("FchBalance")

                    DrValBO.Close()
                    With comando
                        .Parameters.Clear()
                        ' Asignar el sql para seleccionar los datos de la tabla Maestro   
                        .Parameters.AddWithValue("@Fecha", VarFchInv)
                        .CommandText = SQLTPD
                        .Connection = conexion2
                        DrValBO = .ExecuteReader()
                    End With


                    DrValBO.Read()

                    If DrValBO.HasRows = True Then
                        VinsBorder = 1
                        SQLTPD = "INSERT INTO BORDER VALUES("
                        SQLTPD &= "'" & fila("CodClte").ToString & "',"
                        SQLTPD &= "'" & fila("Cliente").ToString & "',"
                        SQLTPD &= "'" & fila("Agente").ToString & "',"
                        SQLTPD &= IIf(IsDBNull(fila("OrdVta")), "0",
                              fila("OrdVta").ToString) & ","
                        SQLTPD &= "@FchOVta,"
                        SQLTPD &= "'" & fila("FacBO").ToString & "',"
                        SQLTPD &= "@FchFBo,"
                        SQLTPD &= "'" & fila("Articulo").ToString & "',"
                        SQLTPD &= "'" & fila("Descripcion").ToString & "',"
                        SQLTPD &= "'" & fila("Linea").ToString & "',"
                        SQLTPD &= fila("PedClte").ToString & ","
                        SQLTPD &= IIf(IsDBNull(fila("Facturado")), "0",
                                 fila("Facturado").ToString) & ","
                        SQLTPD &= fila("CantBO").ToString & ","
                        SQLTPD &= fila("Precio").ToString & ","
                        SQLTPD &= fila("TotalBO").ToString & ","
                        SQLTPD &= fila("IdLinea").ToString & ","
                        SQLTPD &= fila("IdAgente").ToString & ","
                        SQLTPD &= fila("mes").ToString & ","
                        SQLTPD &= fila("anio").ToString & ","
                        SQLTPD &= "@FchAct, "
                        'ICR_27042015 Se agrega el Id_usuario de la sesion  y el alamacen de la orden de venta
                        SQLTPD &= "'" & fila("VtasUsuario").ToString() & "',"
                        SQLTPD &= "'" & fila("Almacen").ToString() + "', '')"
                        DrValBO.Close()
                        With ComTPM
                            '.Connection = ConTPM
                            .Connection.Open()
                            .Parameters.Clear()
                            .Parameters.AddWithValue("@FchOVta", fila("FchOrdVta"))
                            .Parameters.AddWithValue("@FchFBo", fila("FchFactBO"))
                            .Parameters.AddWithValue("@FchAct", fila("FchAct"))
                            ' Asignar el sql para seleccionar los datos de la tabla Maestro   
                            .CommandText = SQLTPD
                            .ExecuteNonQuery()
                            .Connection.Close()
                        End With
                    Else
                        DrValBO.Close()

                        'Revisa el inventario del articulo cuando se realizo la primer factura del pedido
                        'que sea diferente a la factura del articulo
                        SQLTPD = "SELECT COUNT(*) FROM oinm T6 "
                        SQLTPD &= "WHERE T6.ItemCode = '" & fila("Articulo") & "' "
                        ' ICR 08052015 Se agrega el filtro por almacen 
                        'SQLTPD &= " And T0.Warehouse= '" & fila("Almacen") & "'"
                        SQLTPD &= " AND T6.DocDate IN "
                        SQLTPD &= "(SELECT TOP 1 T0.DocDate AS FchFactBO "
                        SQLTPD &= "FROM OINV T0 "
                        SQLTPD &= "INNER JOIN INV1 T1 ON T0.DocEntry = T1.DocEntry INNER JOIN OSLP T2 ON T2.SlpCode = T0.SlpCode "
                        SQLTPD &= "LEFT JOIN RDR1 T5 ON T1.BaseEntry = T5.DocEntry AND T1.ItemCode = T5.ItemCode "
                        SQLTPD &= "WHERE t1.BaseEntry = " & fila("OrdVta") & " AND T0.DocNum <> '" & fila("FacBO") & "' "
                        SQLTPD &= "ORDER BY FchFactBO ASC) "
                        SQLTPD &= "AND T6.Balance <= 0 "



                        Dim ValFchFact As Integer
                        'Busca el inventario del articulo de acuerdo a la fecha de factura más reciente del pedido.

                        With comando
                            .Parameters.Clear()
                            .CommandText = SQLTPD
                            .Connection = conexion2
                            ValFchFact = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar)
                        End With

                        If ValFchFact <> 0 Then
                            VinsBorder = 1
                            SQLTPD = "INSERT INTO BORDER VALUES("
                            SQLTPD &= "'" & fila("CodClte").ToString & "',"
                            SQLTPD &= "'" & fila("Cliente").ToString & "',"
                            SQLTPD &= "'" & fila("Agente").ToString & "',"
                            SQLTPD &= IIf(IsDBNull(fila("OrdVta")), "0",
                                  fila("OrdVta").ToString) & ","
                            SQLTPD &= "@FchOVta,"
                            SQLTPD &= "'" & fila("FacBO").ToString & "',"
                            SQLTPD &= "@FchFBo,"
                            SQLTPD &= "'" & fila("Articulo").ToString & "',"
                            SQLTPD &= "'" & fila("Descripcion").ToString & "',"
                            SQLTPD &= "'" & fila("Linea").ToString & "',"
                            SQLTPD &= fila("PedClte").ToString & ","
                            SQLTPD &= IIf(IsDBNull(fila("Facturado")), "0",
                                     fila("Facturado").ToString) & ","
                            SQLTPD &= fila("CantBO").ToString & ","
                            SQLTPD &= fila("Precio").ToString & ","
                            SQLTPD &= fila("TotalBO").ToString & ","
                            SQLTPD &= fila("IdLinea").ToString & ","
                            SQLTPD &= fila("IdAgente").ToString & ","
                            SQLTPD &= fila("mes").ToString & ","
                            SQLTPD &= fila("anio").ToString & ","
                            SQLTPD &= "@FchAct, "
                            'ICR_27042015 Se agrega el Id_usuario de la sesion  y el alamacen de la orden de venta
                            SQLTPD &= "'" & fila("VtasUsuario").ToString() & "',"
                            SQLTPD &= "'" & fila("Almacen").ToString() + "', '')"

                            DrValBO.Close()
                            With ComTPM

                                '.Connection = ConTPM
                                .Connection.Open()
                                .Parameters.Clear()
                                .Parameters.AddWithValue("@FchOVta", fila("FchOrdVta"))
                                .Parameters.AddWithValue("@FchFBo", fila("FchFactBO"))
                                .Parameters.AddWithValue("@FchAct", fila("FchAct"))
                                ' Asignar el sql para seleccionar los datos de la tabla Maestro   
                                .CommandText = SQLTPD

                                .ExecuteNonQuery()
                                .Connection.Close()
                            End With

                        Else

                            Dim ValDifInv As Integer 'Valida si el articulo de la orden de venta se reporto como con inventario diferente de cero

                            'Se valida que el pedido y articulo no este en la lista de excepciones
                            With ComTPM
                                .CommandText = "SELECT count(*) AS Registro FROM BODifInv Where Ordvta =" & fila("OrdVta") & " AND Articulo ='" & fila("Articulo") & "'"

                                .CommandType = CommandType.Text
                                '.Connection = New Data.SqlClient.SqlConnection(StrTpm)
                                .Connection.Open()
                                ValDifInv = IIf(IsDBNull(.ExecuteScalar()), 0, .ExecuteScalar())
                                .Connection.Close()
                            End With

                            If ValDifInv > 0 Then
                                VinsBorder = 1
                                SQLTPD = "INSERT INTO BORDER VALUES("
                                SQLTPD &= "'" & fila("CodClte").ToString & "',"
                                SQLTPD &= "'" & fila("Cliente").ToString & "',"
                                SQLTPD &= "'" & fila("Agente").ToString & "',"
                                SQLTPD &= IIf(IsDBNull(fila("OrdVta")), "0",
                                      fila("OrdVta").ToString) & ","
                                SQLTPD &= "@FchOVta,"
                                SQLTPD &= "'" & fila("FacBO").ToString & "',"
                                SQLTPD &= "@FchFBo,"
                                SQLTPD &= "'" & fila("Articulo").ToString & "',"
                                SQLTPD &= "'" & fila("Descripcion").ToString & "',"
                                SQLTPD &= "'" & fila("Linea").ToString & "',"
                                SQLTPD &= fila("PedClte").ToString & ","
                                SQLTPD &= IIf(IsDBNull(fila("Facturado")), "0",
                                         fila("Facturado").ToString) & ","
                                SQLTPD &= fila("CantBO").ToString & ","
                                SQLTPD &= fila("Precio").ToString & ","
                                SQLTPD &= fila("TotalBO").ToString & ","
                                SQLTPD &= fila("IdLinea").ToString & ","
                                SQLTPD &= fila("IdAgente").ToString & ","
                                SQLTPD &= fila("mes").ToString & ","
                                SQLTPD &= fila("anio").ToString & ","
                                SQLTPD &= "@FchAct, "
                                'ICR_27042015 Se agrega el Id_usuario de la sesion  y el alamacen de la orden de venta
                                SQLTPD &= "'" & fila("VtasUsuario").ToString() & "',"
                                SQLTPD &= "'" & fila("Almacen").ToString() + "','')"

                                DrValBO.Close()
                                With ComTPM
                                    .Parameters.Clear()
                                    .Connection.Open()
                                    .Parameters.AddWithValue("@FchOVta", fila("FchOrdVta"))
                                    .Parameters.AddWithValue("@FchFBo", fila("FchFactBO"))
                                    .Parameters.AddWithValue("@FchAct", fila("FchAct"))
                                    ' Asignar el sql para seleccionar los datos de la tabla Maestro   
                                    .CommandText = SQLTPD
                                    '.Connection = ConTPM
                                    .ExecuteNonQuery()
                                    .Connection.Close()
                                End With
                            Else
                                fila("NoBO") = 1
                                'ComTPM.Connection.Close()
                            End If
                            DrValBO.Close()
                        End If

                        '/***********************************

                    End If
                Else
                    Dim vDes As String
                    vDes = QuitarCaracteres(fila("Descripcion").ToString)

                    VinsBorder = 1
                    SQLTPD = "INSERT INTO BORDER VALUES("
                    SQLTPD &= "'" & fila("CodClte").ToString & "',"
                    SQLTPD &= "'" & fila("Cliente").ToString & "',"
                    SQLTPD &= "'" & fila("Agente").ToString & "',"
                    SQLTPD &= IIf(IsDBNull(fila("OrdVta")), "0",
                          fila("OrdVta").ToString) & ","
                    SQLTPD &= "@FchOVta,"
                    SQLTPD &= "'" & fila("FacBO").ToString & "',"
                    SQLTPD &= "@FchFBo,"
                    SQLTPD &= "'" & fila("Articulo").ToString & "',"
                    'SQLTPD &= "'" & fila("Descripcion").ToString & "',"
                    SQLTPD &= "'" & vDes & "',"
                    SQLTPD &= "'" & fila("Linea").ToString & "',"
                    SQLTPD &= fila("PedClte").ToString & ","
                    SQLTPD &= IIf(IsDBNull(fila("Facturado")), "0",
                             fila("Facturado").ToString) & ","
                    SQLTPD &= fila("CantBO").ToString & ","
                    SQLTPD &= fila("Precio").ToString & ","
                    SQLTPD &= fila("TotalBO").ToString & ","
                    SQLTPD &= fila("IdLinea").ToString & ","
                    SQLTPD &= fila("IdAgente").ToString & ","
                    SQLTPD &= fila("mes").ToString & ","
                    SQLTPD &= fila("anio").ToString & ","
                    SQLTPD &= "@FchAct, "
                    'ICR_27042015 Se agrega el Id_usuario de la sesion  y el alamacen de la orden de venta
                    SQLTPD &= "'" & fila("VtasUsuario").ToString() & "',"
                    SQLTPD &= "'" & fila("Almacen").ToString() + "', '')"

                    DrValBO.Close()
                    With ComTPM
                        .Connection.Open()
                        .Parameters.Clear()
                        .Parameters.AddWithValue("@FchOVta", fila("FchOrdVta"))
                        .Parameters.AddWithValue("@FchFBo", fila("FchFactBO"))
                        .Parameters.AddWithValue("@FchAct", fila("FchAct"))
                        ' Asignar el sql para seleccionar los datos de la tabla Maestro   
                        .CommandText = SQLTPD
                        '.Connection = ConTPM
                        .ExecuteNonQuery()
                        .Connection.Close()
                    End With
                End If

            Next

            Dim DvBONoAct As DataView

            'Asignar datatable a dataview
            DvBONoAct = DtArtBo.DefaultView

            DvBONoAct.RowFilter = "NoBO = 1"

            ComTPM.Connection.Close()
            conexion2.Close()


            With Me.DgvNoIdentificados
                .DataSource = DvBONoAct
                .ReadOnly = True
                'Color de Renglones en Grid
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FloralWhite
                .AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
                'Propiedad para no mostrar el cuadro que se encuentra en la parte
                'Superior Izquierda del gridview
                .RowHeadersVisible = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = True
                .AllowUserToAddRows = False
                'Color de linea del grid
                .Columns(0).HeaderText = "Codigo"
                .Columns(0).Width = 60
                .Columns(1).HeaderText = "Cliente"
                .Columns(1).Width = 180

                .Columns(2).HeaderText = "Agente"
                .Columns(2).Width = 140

                .Columns(3).HeaderText = "Ord. de Vta"
                .Columns(3).Width = 40

                .Columns(4).HeaderText = "Fecha Ord. Vta."
                .Columns(4).Width = 70

                .Columns(5).HeaderText = "Factura Back Order"
                .Columns(5).Width = 45

                .Columns(6).HeaderText = "Fecha BO"
                .Columns(6).Width = 75

                .Columns(7).HeaderText = "Articulo"
                .Columns(7).Width = 110

                .Columns(8).HeaderText = "Descripción"
                .Columns(8).Width = 150

                .Columns(9).HeaderText = "Línea"
                .Columns(9).Width = 100

                .Columns(10).HeaderText = "Pedido Cliente"
                .Columns(10).Width = 45
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(10).DefaultCellStyle.Format = "###,###,###"

                .Columns(11).HeaderText = "Factu- rado"
                .Columns(11).Width = 45
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(11).DefaultCellStyle.Format = "###,###,###"

                .Columns(12).HeaderText = "Back Order"
                .Columns(12).Width = 45
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(12).DefaultCellStyle.Format = "###,###,###"

                .Columns(13).HeaderText = "$ Precio"
                .Columns(13).Width = 50
                .Columns(13).DefaultCellStyle.Format = "###,###,###.##"
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(14).HeaderText = "$ Total BO"
                .Columns(14).Width = 60
                .Columns(14).DefaultCellStyle.Format = "###,###,###.##"
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(15).Visible = False

            End With


            If VinsBorder = 1 Then
                MessageBox.Show("Se registro el back order recuperado exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("No se registro ningún back order recuperado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Else
            MessageBox.Show("La factura no existe, por favor verifique el número", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End If
    End Sub


End Class